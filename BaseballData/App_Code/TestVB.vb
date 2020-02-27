Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic
Imports System.Security.Permissions

Public Class TestVB
    Public theString As String = ""

    Sub New()

        'Dim allPitches As AllPitches = New JavaScriptSerializer().Deserialize(Of AllPitches)(System.IO.File.ReadAllText(System.IO.Path.Combine("C:\Users\Monster\source\repos\BaseballData\BaseballData\JSON_Data", "PitcherData.json")))

        ''List of unique gameid's in data
        'Dim theGames As New List(Of String)
        'For Each SinglePitch In allPitches.all
        '    Dim addGame As Boolean = True
        '    'Loop through game list, then if that game exists already, set addGame value to false
        '    For Each CountedGames In theGames
        '        If SinglePitch.gameid = CountedGames Then
        '            addGame = False
        '        End If
        '    Next
        '    'If current game did not exist in the list, add it
        '    If addGame Then theGames.Add(SinglePitch.gameid)
        'Next

        'For Each g In theGames
        '    theString += g + "<br/>"
        'Next

        'Dim theData As New LoadData
        'For Each p In theData.PitcherList
        '    theString += p.PitcherName + "<br/>"
        '    Dim theGames As List(Of Game) = p.CalcGameAverages
        '    For Each g In theGames
        '        theString += g.GameID + " (" + g.GameDate + ")<br/>" 'String.Format("String = {0:0.0%}", ratio)
        '        theString += "Fastball: " + String.Format("{0:0.00%}", g.PitchPercentage(0)) + "<br/>"
        '        theString += "Changeup: " + String.Format("{0:0.00%}", g.PitchPercentage(1)) + "<br/>"
        '        theString += "Slider: " + String.Format("{0:0.00%}", g.PitchPercentage(2)) + "<br/>"
        '        theString += "Curve: " + String.Format("{0:0.00%}", g.PitchPercentage(3)) + "<br/>"
        '        theString += "Sinker: " + String.Format("{0:0.00%}", g.PitchPercentage(4)) + "<br/>"
        '        theString += "Other: " + String.Format("{0:0.00%}", g.PitchPercentage(5)) + "<br/><br/>"
        '    Next
        '    theString += "-------------------------------<br/><br/>"
        'Next


        'Dim allPitches As AllPitches = New JavaScriptSerializer().Deserialize(Of AllPitches)(System.IO.File.ReadAllText(System.IO.Path.Combine("C:\Users\Monster\source\repos\BaseballData\BaseballData\JSON_Data", "PitcherData.json")))

        ''List of unique pitcherid's in data
        'Dim thePitchers As New List(Of Integer)
        ''List of unique gameid's in data
        'Dim theGames As New List(Of String)

        ''Loop through each pitch object to get each unique pitcher and each unique gameid
        'For Each SinglePitch In allPitches.all
        '    Dim addPitcher As Boolean = True
        '    'Loop through pitcher list, then if that pitcher exists already, set addPitcher value to false
        '    For Each CountedPitchers In thePitchers
        '        If SinglePitch.pitcherid = CountedPitchers Then
        '            addPitcher = False
        '        End If
        '    Next
        '    'If current pitcher did not exist in the list, add it
        '    If addPitcher Then thePitchers.Add(SinglePitch.pitcherid)

        '    Dim addGame As Boolean = True
        '    'Loop through game list, then if that game exists already, set addGame value to false
        '    For Each CountedGames In theGames
        '        If SinglePitch.gameid = CountedGames Then
        '            addGame = False
        '        End If
        '    Next
        '    'If current game did not exist in the list, add it
        '    If addGame Then theGames.Add(SinglePitch.gameid)
        'Next

        'For Each PitcherID In thePitchers
        '    Dim PitcherObject As New Pitcher()
        '    PitcherObject.PitcherID = PitcherID

        '    For Each GameID In theGames
        '        Dim GameObject As New Game()
        '        GameObject.GameID = GameID

        '        For Each CheckEachPitch In allPitches.all
        '            If CheckEachPitch.pitcherid = PitcherID And CheckEachPitch.gameid = GameID Then
        '                Dim addPitch As Pitch = CheckEachPitch
        '                GameObject.Pitches.Add(addPitch)

        '                PitcherObject.PitcherName = CheckEachPitch.pitcher
        '                GameObject.GameDate = CheckEachPitch.game_date
        '                GameObject.Stadium = CheckEachPitch.stadium
        '            End If
        '        Next

        '        If GameObject.Pitches.Count > 0 Then
        '            PitcherObject.Games.Add(GameObject)
        '        End If
        '    Next

        '    theString += "Pitcher: " + PitcherObject.PitcherName + "<br/>"
        '    theString += "Total Games: " + PitcherObject.Games.Count.ToString + "<br/>"
        '    theString += "Total Pitches in Game 1: " + PitcherObject.Games(0).Pitches.Count.ToString + "<br/><br/>"
        'Next

        'theString = allPitches.all(0).zonespeed.ToString
    End Sub

    Public Shared Function ChooseChart(value As String) As String
        Dim theString = ""
        Select Case UCase(value)
            Case "VW"
                Dim theData As New LoadData

                theString = "<script>" + vbCrLf
                For Each Pitcher In theData.PitcherList
                    For Each Game In Pitcher.Games
                        theString += GamePitchPercentage(Pitcher, Game)
                    Next
                Next

                'theString += GamePitchPercentage(theData.PitcherList(0), theData.PitcherList(0).CalcGameAverages(0))
                'theString += GamePitchPercentage(theData.PitcherList(0), theData.PitcherList(0).CalcGameAverages(1))
                theString += "</script>" + vbCrLf
        End Select

        Return theString
    End Function

    Public Shared Function GamePitchPercentage(thePitcher As Pitcher, theGame As Game) As String
        Dim sb As New StringBuilder

        'sb.Append("<script>" + vbCrLf)

        ' set the dimensions And margins of the graph
        sb.Append("var width = 250" + vbCrLf)
        sb.Append("var height = 250" + vbCrLf)
        sb.Append("var margin = 5" + vbCrLf)

        ' The radius of the pieplot Is half the width Or half the height (smallest one). I subtract a bit of margin.
        sb.Append("var radius = Math.min(width, height) / 2 - margin" + vbCrLf)

        ' append the svg object to the div called 'my_dataviz'
        sb.Append("var svg = d3.select(""#my_dataviz"").append(""svg"").attr(""width"", width).attr(""height"", height).append(""g"").attr(""transform"", ""translate("" + width / 2 + "", "" + height / 2 + "")"");" + vbCrLf)

        'Add data
        sb.Append("var data = {")
        If theGame.PitchPercentage(0) > 0 Then sb.Append("FB: " + theGame.PitchPercentage(0).ToString + ", ")
        If theGame.PitchPercentage(1) > 0 Then sb.Append("CH: " + theGame.PitchPercentage(1).ToString + ", ")
        If theGame.PitchPercentage(2) > 0 Then sb.Append("SLD: " + theGame.PitchPercentage(2).ToString + ", ")
        If theGame.PitchPercentage(3) > 0 Then sb.Append("CRV: " + theGame.PitchPercentage(3).ToString + ", ")
        If theGame.PitchPercentage(4) > 0 Then sb.Append("SNK: " + theGame.PitchPercentage(4).ToString + ", ")
        If theGame.PitchPercentage(5) > 0 Then sb.Append("CUT: " + theGame.PitchPercentage(5).ToString + ", ")
        If theGame.PitchPercentage(6) > 0 Then sb.Append("SPL: " + theGame.PitchPercentage(4).ToString + ", ")
        If theGame.PitchPercentage(7) > 0 Then sb.Append("Other: " + theGame.PitchPercentage(5).ToString + ", ")
        'Cut off last ", "
        sb.Remove(sb.Length - 2, 2)
        sb.Append("}" + vbCrLf)

        ' set the color scale
        sb.Append("var color = d3.scaleOrdinal().domain(data).range([")
        If theGame.PitchPercentage(0) > 0 Then sb.Append("""#473729"", ")
        If theGame.PitchPercentage(1) > 0 Then sb.Append("""#041E42"", ")
        If theGame.PitchPercentage(2) > 0 Then sb.Append("""#A2AAAD"", ")
        If theGame.PitchPercentage(3) > 0 Then sb.Append("""#FFC72C"", ")
        If theGame.PitchPercentage(4) > 0 Then sb.Append("""#462425"", ")
        If theGame.PitchPercentage(5) > 0 Then sb.Append("""#002D62"", ")
        If theGame.PitchPercentage(6) > 0 Then sb.Append("""#E35205"", ")
        If theGame.PitchPercentage(7) > 0 Then sb.Append("""#ffffff"", ")
        'Cut off last ", "
        sb.Remove(sb.Length - 2, 2)
        sb.Append("])" + vbCrLf)

        ' Compute the position of each group on the pie
        sb.Append("var pie = d3.pie().value(function(d) {return d.value; })" + vbCrLf)
        sb.Append("var data_ready = pie(d3.entries(data))" + vbCrLf)

        ' shape helper to build arcs
        sb.Append("var arcGenerator = d3.arc().innerRadius(0).outerRadius(radius)" + vbCrLf)

        'Add Pitcher Name (Game Date) as the Title
        sb.Append("svg.append(""text"").attr(""x"", 0).attr(""y"", 0).attr(""text-anchor"", ""middle"").style(""font-size"", ""12px"").text(""" + thePitcher.PitcherName + " (" + theGame.GameDate + ")"");")

        ' Build the pie chart Basically, each part of the pie Is a path that we build using the arc function.
        sb.Append("svg.selectAll('showPitches').data(data_ready).enter().append('path').attr('d', arcGenerator).attr('fill', function(d){ return(color(d.data.key)) }).attr(""stroke"", ""black"").style(""stroke-width"", ""2px"").style(""opacity"", 0.7)" + vbCrLf)

        ' Now add the annotation. Use the centroid method to get the best coordinates
        sb.Append("svg.selectAll('showPitches').data(data_ready).enter().append('text').text(function(d) { return d.data.key}).attr(""transform"", function(d) { return ""translate("" + arcGenerator.centroid(d) + "")"";  }).style(""text-anchor"", ""middle"").style(""font-size"", 10)" + vbCrLf)

        'sb.Append("</script>" + vbCrLf)

        Return sb.ToString
    End Function
End Class
