Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic

Public Class FormatScripts
    Sub New()

    End Sub

    Shared Function SeasonToCsvScript() As String
        Dim sb As New StringBuilder
        Dim theData As New LoadData

        '=====================
        'Build new csv string
        '=====================
        sb.Append("Pitcher,IP,K,ER,H,BB,TB,ERA,WHIP,oAVG,oOBP,oSLUG,kPerc,bbPerc,kbb,fbV,chV,slV,crvV,snkV,cutV,splV" + vbCrLf)
        For Each Pitcher In theData.PitcherList
            sb.Append(Pitcher.PitcherName + ",")
            sb.Append(Pitcher.SeasonAvg.InningsPitched.ToString + ",")
            sb.Append(Pitcher.SeasonAvg.Strikeouts.ToString + ",")
            sb.Append(Pitcher.SeasonAvg.ERunsAllowed.ToString + ",")
            sb.Append(Pitcher.SeasonAvg.HitsAllowed.ToString + ",")
            sb.Append(Pitcher.SeasonAvg.WalksAllowed.ToString + ",")
            sb.Append(Pitcher.SeasonAvg.TotalBasesAllowed.ToString + ",")
            sb.Append(String.Format("{0:0.00}", Pitcher.SeasonAvg.ERA) + ",")
            sb.Append(String.Format("{0:0.00}", Pitcher.SeasonAvg.WHIP) + ",")
            sb.Append(String.Format("{0:.000}", Pitcher.SeasonAvg.oAVG) + ",")
            sb.Append(String.Format("{0:.000}", Pitcher.SeasonAvg.oOBP) + ",")
            sb.Append(String.Format("{0:.000}", Pitcher.SeasonAvg.oSLUG) + ",")
            sb.Append(String.Format("{0:0.0}", Pitcher.SeasonAvg.kPerc) + ",")
            sb.Append(String.Format("{0:0.0}", Pitcher.SeasonAvg.bbPerc) + ",")
            'Wasn't sure how to calculate infinity, so if there were no walks, K/BB = K
            If Pitcher.SeasonAvg.WalksAllowed = 0 Then
                sb.Append(String.Format("{0:0.00}", Pitcher.SeasonAvg.Strikeouts) + ",")
            Else
                sb.Append(String.Format("{0:0.00}", Pitcher.SeasonAvg.Strikeouts / Pitcher.SeasonAvg.WalksAllowed) + ",")
            End If
            For Each pitchV In Pitcher.SeasonAvg.PitchVelocity
                If pitchV.ToString = "NaN" Then
                    sb.Append(",")
                Else
                    sb.Append(pitchV.ToString + ",")
                End If
            Next
            sb.Remove(sb.Length - 1, 1)
            sb.Append(vbCrLf)
        Next

        Dim testStr As String = sb.ToString
        Return testStr
    End Function

    Shared Function GamesToCsvScript() As String
        Dim sb As New StringBuilder
        Dim theData As New LoadData

        '=====================
        'Build new csv string
        '=====================
        sb.Append("Pitcher,GameID,GameDate,IP,K,ER,H,BB,TB,ERA,WHIP,oAVG,oOBP,oSLUG,kPerc,bbPerc,kbb,fbV,chV,slV,crvV,snkV,cutV,splV" + vbCrLf)
        For Each Pitcher In theData.PitcherList
            For Each Game In Pitcher.Games
                sb.Append(Pitcher.PitcherName + ",")
                sb.Append(Game.GameID + ",")
                sb.Append(Game.GameDate + ",")
                sb.Append(Game.InningsPitched.ToString + ",")
                sb.Append(Game.Strikeouts.ToString + ",")
                sb.Append(Game.ERunsAllowed.ToString + ",")
                sb.Append(Game.HitsAllowed.ToString + ",")
                sb.Append(Game.WalksAllowed.ToString + ",")
                sb.Append(Game.TotalBasesAllowed.ToString + ",")
                sb.Append(String.Format("{0:0.00}", Game.ERA) + ",")
                sb.Append(String.Format("{0:0.00}", Game.WHIP) + ",")
                sb.Append(String.Format("{0:.000}", Game.oAVG) + ",")
                sb.Append(String.Format("{0:.000}", Game.oOBP) + ",")
                sb.Append(String.Format("{0:.000}", Game.oSLUG) + ",")
                sb.Append(String.Format("{0:0.0}", Game.kPerc) + ",")
                sb.Append(String.Format("{0:0.0}", Game.bbPerc) + ",")
                'Wasn't sure how to calculate infinity, so if there were no walks, K/BB = K
                If Game.WalksAllowed = 0 Then
                    sb.Append(String.Format("{0:0.00}", Game.Strikeouts) + ",")
                Else
                    sb.Append(String.Format("{0:0.00}", Game.Strikeouts / Game.WalksAllowed) + ",")
                End If
                For Each pitchV In Game.PitchVelocity
                    If pitchV.ToString = "NaN" Then
                        sb.Append(",")
                    Else
                        sb.Append(pitchV.ToString + ",")
                    End If
                Next
                sb.Remove(sb.Length - 1, 1)
                sb.Append(vbCrLf)
            Next
        Next

        Dim testStr As String = sb.ToString
        Return testStr
    End Function

    Shared Function RunScript() As String
        'Return object
        Dim sb As New StringBuilder
        'List of unique pitcherid's in data
        Dim thePitchers As New List(Of Integer)
        'List of unique gameid's in data
        Dim theGames As New List(Of String)

        'Deserialize json into an Object containing a list of all pitches
        Dim allPitches As AllPitches = New JavaScriptSerializer().Deserialize(Of AllPitches)(System.IO.File.ReadAllText(System.IO.Path.Combine("C:\Users\Monster\source\repos\BaseballData\BaseballData\JSON_Data", "PitcherData.json")))

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

        '=====================
        'Build new json string
        '=====================
        sb.Append("{" + vbCrLf + """all"": [" + vbCrLf)
        'Look through each pitcher -> loop through each one of their games -> loop through each one of that game's pitches
        For Each PitcherID In thePitchers
            sb.Append("{" + vbCrLf + """pitcherid"":" + PitcherID.ToString + "," + vbCrLf + """games"": [" + vbCrLf)
            For Each GameID In theGames
                'Counter to check if it's the first pitch added / check if there was any pitches in this game
                Dim PitchesInGameCounter As Integer = 0
                For Each CheckEachPitch In allPitches.all
                    'Only add the pitch if it was for the current pitcher and the current game
                    If CheckEachPitch.pitcherid = PitcherID And CheckEachPitch.gameid = GameID Then
                        'Add game header and list of pitches header if it's the first pitch to be added
                        If PitchesInGameCounter = 0 Then
                            sb.Append("{" + vbCrLf + """gameid"":" + """" + GameID + """," + vbCrLf + """pitches"": [" + vbCrLf)
                        End If
                        'Function to add all the data from the Pitch class back into json
                        sb.Append(BuildJsonForSinglePitch(CheckEachPitch) + ",")

                        PitchesInGameCounter += 1
                    End If
                Next

                'If there was a pitch in this game for the current pitcher, remove extra comma and close the list
                If PitchesInGameCounter > 0 Then
                    'Remove last comma
                    sb.Remove(sb.Length - 1, 1)
                    'Close the list of pitches
                    sb.Append(vbCrLf + "]" + vbCrLf + "}" + vbCrLf)
                End If
            Next
            sb.Append(vbCrLf + "]" + vbCrLf + "}" + vbCrLf)
        Next
        sb.Append("]" + vbCrLf + "}")

        Dim testStr As String = sb.ToString
        Return testStr
    End Function

    Shared Function BuildJsonForSinglePitch(thePitch As Pitch) As String
        Dim sb As New StringBuilder

        sb.Append("{" + vbCrLf)
        sb.Append("""gameid"":" + """" + thePitch.gameid + """," + vbCrLf)
        sb.Append("""game_date"":" + """" + thePitch.game_date + """," + vbCrLf)
        sb.Append("""stadium"":" + """" + thePitch.stadium + """," + vbCrLf)
        sb.Append("""pitchno"":" + CheckNull(thePitch.pitchno.ToString) + "," + vbCrLf)
        sb.Append("""time"":" + """" + thePitch.time + """," + vbCrLf)
        sb.Append("""inning"":" + CheckNull(thePitch.inning.ToString) + "," + vbCrLf)
        sb.Append("""topbottom"":" + """" + thePitch.topbottom + """," + vbCrLf)
        sb.Append("""outs"":" + CheckNull(thePitch.outs.ToString) + "," + vbCrLf)
        sb.Append("""strikes"":" + CheckNull(thePitch.strikes.ToString) + "," + vbCrLf)
        sb.Append("""balls"":" + CheckNull(thePitch.balls.ToString) + "," + vbCrLf)
        sb.Append("""pa_of_inning"":" + CheckNull(thePitch.pa_of_inning.ToString) + "," + vbCrLf)
        sb.Append("""pitch_of_pa"":" + CheckNull(thePitch.pitch_of_pa.ToString) + "," + vbCrLf)
        sb.Append("""pitcher"":" + """" + thePitch.pitcher + """," + vbCrLf)
        sb.Append("""pitcherid"":" + CheckNull(thePitch.pitcherid.ToString) + "," + vbCrLf)
        sb.Append("""pitcherthrows"":" + """" + thePitch.pitcherthrows + """," + vbCrLf)
        sb.Append("""pitcherset"":" + """" + thePitch.pitcherset + """," + vbCrLf)
        sb.Append("""batterside"":" + """" + thePitch.batterside + """," + vbCrLf)
        sb.Append("""pitchtype"":" + """" + thePitch.pitchtype + """," + vbCrLf)
        sb.Append("""pitchcall"":" + """" + thePitch.pitchcall + """," + vbCrLf)
        sb.Append("""korbb"":" + """" + thePitch.korbb + """," + vbCrLf)
        sb.Append("""hittype"":" + """" + thePitch.hittype + """," + vbCrLf)
        sb.Append("""playresult"":" + """" + thePitch.playresult + """," + vbCrLf)
        sb.Append("""outsonplay"":" + CheckNull(thePitch.outsonplay.ToString) + "," + vbCrLf)
        sb.Append("""runsscored"":" + CheckNull(thePitch.runsscored.ToString) + "," + vbCrLf)
        sb.Append("""relspeed"":" + CheckNull(thePitch.relspeed.ToString) + "," + vbCrLf)
        sb.Append("""zonespeed"":" + CheckNull(thePitch.zonespeed.ToString) + "," + vbCrLf)
        sb.Append("""relheight"":" + CheckNull(thePitch.relheight.ToString) + "," + vbCrLf)
        sb.Append("""relside"":" + CheckNull(thePitch.relside.ToString) + "," + vbCrLf)
        sb.Append("""vertrelangle"":" + CheckNull(thePitch.vertrelangle.ToString) + "," + vbCrLf)
        sb.Append("""horzrelangle"":" + CheckNull(thePitch.horzrelangle.ToString) + "," + vbCrLf)
        sb.Append("""spinrate"":" + CheckNull(thePitch.spinrate.ToString) + "," + vbCrLf)
        sb.Append("""spinaxis"":" + CheckNull(thePitch.spinaxis.ToString) + "," + vbCrLf)
        sb.Append("""tilt"":" + """" + thePitch.tilt + """," + vbCrLf)
        sb.Append("""extension"":" + CheckNull(thePitch.extension.ToString) + "," + vbCrLf)
        sb.Append("""vertbreak"":" + CheckNull(thePitch.vertbreak.ToString) + "," + vbCrLf)
        sb.Append("""horzbreak"":" + CheckNull(thePitch.horzbreak.ToString) + "," + vbCrLf)
        sb.Append("""platelocheight"":" + CheckNull(thePitch.platelocheight.ToString) + "," + vbCrLf)
        sb.Append("""platelocside"":" + CheckNull(thePitch.platelocside.ToString) + "," + vbCrLf)
        sb.Append("""zonetime"":" + CheckNull(thePitch.zonetime.ToString) + "," + vbCrLf)
        sb.Append("""exitspeed"":" + CheckNull(thePitch.exitspeed.ToString) + "," + vbCrLf)
        sb.Append("""hitangle"":" + CheckNull(thePitch.hitangle.ToString) + "," + vbCrLf)
        sb.Append("""hitdirection"":" + CheckNull(thePitch.hitdirection.ToString) + "," + vbCrLf)
        sb.Append("""distance"":" + CheckNull(thePitch.distance.ToString) + "," + vbCrLf)
        sb.Append("""bearing"":" + CheckNull(thePitch.bearing.ToString) + "," + vbCrLf)
        sb.Append("""hangtime"":" + CheckNull(thePitch.bearing.ToString) + vbCrLf)
        sb.Append(vbCrLf + "}")

        sb.Replace(":,", ":""""")
        sb.Replace(":" + vbCrLf + "}", ":""""")

        Return sb.ToString
    End Function

    Shared Function CheckNull(value As String) As String
        If Not value Is Nothing Then
            Return """" + """"
        End If

        Return value
    End Function

    Private Shared Function null() As String
        Throw New NotImplementedException()
    End Function
End Class
