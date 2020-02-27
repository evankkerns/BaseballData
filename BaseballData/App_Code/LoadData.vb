Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic

Public Class LoadData
    Public PitcherList As New List(Of Pitcher)

    Sub New()
        Dim fileLoc As New SetupProjectLocation
        'JSON deserializer for the data -> inputs it directly into the class
        Dim allPitches As AllPitches = New JavaScriptSerializer().Deserialize(Of AllPitches)(System.IO.File.ReadAllText(System.IO.Path.Combine(fileLoc.ProjectFileLocation + "\BaseballData\BaseballData\JSON_Data", "PitcherData.json")))
        'Dim allPitches As AllPitches = New JavaScriptSerializer().Deserialize(Of AllPitches)(System.IO.File.ReadAllText(System.IO.Path.Combine("\JSON_Data", "PitcherData.json")))

        'List of unique pitcherid's in data
        Dim thePitchers As New List(Of Integer)
        'List of unique gameid's in data
        Dim theGames As New List(Of String)

        'Loop through each pitch object to get each unique pitcher and each unique gameid
        For Each SinglePitch In allPitches.all
            Dim addPitcher As Boolean = True
            'Loop through pitcher list, then if that pitcher exists already, set addPitcher value to false
            For Each CountedPitchers In thePitchers
                If SinglePitch.pitcherid = CountedPitchers Then
                    addPitcher = False
                End If
            Next
            'If current pitcher did not exist in the list, add it
            If addPitcher Then thePitchers.Add(SinglePitch.pitcherid)

            Dim addGame As Boolean = True
            'Loop through game list, then if that game exists already, set addGame value to false
            For Each CountedGames In theGames
                If SinglePitch.gameid = CountedGames Then
                    addGame = False
                End If
            Next
            'If current game did not exist in the list, add it
            If addGame Then theGames.Add(SinglePitch.gameid)
        Next

        'Loop through each unique pitcher id
        For Each PitcherID In thePitchers
            'Make and set the Pitcher Object
            Dim PitcherObject As New Pitcher()
            PitcherObject.PitcherID = PitcherID

            'Loop through each unique game id
            For Each GameID In theGames
                'Make and set the Game Object
                Dim GameObject As New Game()
                GameObject.GameID = GameID

                'Loop through every pitch, add the pitches that match to Pitches list in the Game Object
                For Each CheckEachPitch In allPitches.all
                    'Checks to make sure the Pitch Object's pitcherid and gameid match the current Pitcher and Game in the loop
                    If CheckEachPitch.pitcherid = PitcherID And CheckEachPitch.gameid = GameID Then
                        'Add Pitch to the Game
                        GameObject.Pitches.Add(CheckEachPitch)

                        'Set the variables that are also in the Pitcher and Game Object (this data will still be in the Pitch object, this is just for simplicity)
                        PitcherObject.PitcherName = CheckEachPitch.pitcher
                        PitcherObject.Throws = CheckEachPitch.pitcherthrows
                        GameObject.GameDate = CheckEachPitch.game_date
                        GameObject.Stadium = CheckEachPitch.stadium
                    End If
                Next

                'If there was a pitch in the current game by the current pitcher, add the Game Object to the Pitcher Object
                If GameObject.Pitches.Count > 0 Then
                    PitcherObject.Games.Add(GameObject)
                End If
            Next
            'Calculate Extra Data for the Pitcher
            PitcherObject.Games = Calculations.CalcGameAverages(PitcherObject.Games)
            PitcherObject.SeasonAvg = Calculations.CalcSeasonAverages(PitcherObject.Games)

            'Add each Pitcher to the List of Pitchers that can be accessed outside this class
            PitcherList.Add(PitcherObject)
        Next
    End Sub
End Class
