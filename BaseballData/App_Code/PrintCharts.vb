Imports Microsoft.VisualBasic

Public Class PrintCharts
    Sub New()

    End Sub

    Public Shared Function ChooseChart(stat As String, SoG As String, SelP As String) As String
        Dim theData As New LoadData
        Dim theString = ""
        Dim cntr As Integer = 0

        Select Case stat
            Case "ERA"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("ERA", "0", "6")
                Else
                    theString += PrintGameBarGraph("ERA", "0", "12")
                End If

            Case "WHIP"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("WHIP", "0", "2")
                Else
                    theString = PrintGameBarGraph("WHIP", "0", "2")
                End If

            Case "INNINGS"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("IP", "0", "30")
                Else
                    theString = PrintGameBarGraph("IP", "0", "10")
                End If

            Case "K"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("K", "0", "30")
                Else
                    theString = PrintGameBarGraph("K", "0", "15")
                End If

            Case "KPERC"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("kPerc", "0", "50")
                Else
                    theString = PrintGameBarGraph("kPerc", "0", "50")
                End If

            Case "BBPERC"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("bbPerc", "0", "25")
                Else
                    theString = PrintGameBarGraph("bbPerc", "0", "25")
                End If

            Case "KBB"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("kbb", "0", "10")
                Else
                    theString = PrintGameBarGraph("kbb", "0", "10")
                End If

            Case "AVG"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("oAVG", "0", ".5")
                Else
                    theString = PrintGameBarGraph("oAVG", "0", ".5")
                End If

            Case "OBP"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("oOBP", "0", ".5")
                Else
                    theString = PrintGameBarGraph("oOBP", "0", ".5")
                End If

            Case "SLUG"
                If SoG = "SEASON" Then
                    theString = PrintSeasonBarGraph("oSLUG", "0", ".8")
                Else
                    theString = PrintGameBarGraph("oSLUG", "0", ".8")
                End If

            Case "VELOCITY"
                If SoG = "SEASON" Then
                    theString = PrintPitchesBarGraph("Data_SeasonAvg.csv", "15")
                Else
                    theString = PrintPitchesBarGraph("Data_GameAvg.csv", "17")
                End If

            Case "PITCHPERC"
                theString = "<script>" + vbCrLf
                If SoG = "SEASON" Then
                    For Each pitcher In theData.PitcherList
                        theString += PitchPercentage(pitcher, pitcher.SeasonAvg)
                    Next
                Else
                    For Each pitcher In theData.PitcherList
                        For Each Game In pitcher.Games
                            theString += PitchPercentage(pitcher, Game)
                        Next
                    Next
                End If
                theString += "</script>" + vbCrLf

            Case "ZONELOC"
                If SoG = "SEASON" Then
                    For Each pitcher In theData.PitcherList
                        If SelP = "ALL" Then 'If All Pitchers are selected
                            theString += ZoneLocation(pitcher.PitcherID.ToString, "", "", cntr.ToString)
                            cntr += 1
                        ElseIf pitcher.PitcherID.ToString = SelP Then 'If a specific pitcher is selected
                            theString += ZoneLocation(pitcher.PitcherID.ToString, "", "", cntr.ToString)
                            cntr += 1
                        End If
                    Next
                Else
                    For Each pitcher In theData.PitcherList
                        If SelP = "ALL" Then 'If All Pitchers are selected
                            For Each Game In pitcher.Games
                                theString += ZoneLocation(pitcher.PitcherID.ToString, " && d.gameid == """ + Game.GameID + """", " (" + Game.GameDate + ")", cntr.ToString)
                                cntr += 1
                            Next
                        ElseIf pitcher.PitcherID.ToString = SelP Then 'If a specific pitcher is selected
                            For Each Game In pitcher.Games
                                theString += ZoneLocation(pitcher.PitcherID.ToString, " && d.gameid == """ + Game.GameID + """", " (" + Game.GameDate + ")", cntr.ToString)
                                cntr += 1
                            Next
                        End If
                    Next
                End If

            Case "RELLOC"
                If SoG = "SEASON" Then
                    For Each pitcher In theData.PitcherList
                        If SelP = "ALL" Then 'If All Pitchers are selected
                            theString += ReleaseLocation(pitcher.PitcherID.ToString, "", "", cntr.ToString, pitcher.Throws)
                            cntr += 1
                        ElseIf pitcher.PitcherID.ToString = SelP Then 'If a specific pitcher is selected
                            theString += ReleaseLocation(pitcher.PitcherID.ToString, "", "", cntr.ToString, pitcher.Throws)
                            cntr += 1
                        End If
                    Next
                Else
                    For Each pitcher In theData.PitcherList
                        If SelP = "ALL" Then 'If All Pitchers are selected
                            For Each Game In pitcher.Games
                                theString += ReleaseLocation(pitcher.PitcherID.ToString, " && d.gameid == """ + Game.GameID + """", " (" + Game.GameDate + ")", cntr.ToString, pitcher.Throws)
                                cntr += 1
                            Next
                        ElseIf pitcher.PitcherID.ToString = SelP Then 'If a specific pitcher is selected
                            For Each Game In pitcher.Games
                                theString += ReleaseLocation(pitcher.PitcherID.ToString, " && d.gameid == """ + Game.GameID + """", " (" + Game.GameDate + ")", cntr.ToString, pitcher.Throws)
                                cntr += 1
                            Next
                        End If
                    Next
                End If

            Case "STATS"
                If SoG = "SEASON" Then
                    For Each pitcher In theData.PitcherList
                        If SelP = "ALL" Then 'If All Pitchers are selected
                            theString += "<b>" + pitcher.PitcherName + "</b><br/>"
                            theString += PrintStats(pitcher.SeasonAvg, False)
                            theString += "----------------------------------<br/>"
                        ElseIf pitcher.PitcherID.ToString = SelP Then 'If a specific pitcher is selected
                            theString += "<b>" + pitcher.PitcherName + "</b><br/>"
                            theString += PrintStats(pitcher.SeasonAvg, False)
                        End If
                    Next
                Else
                    For Each pitcher In theData.PitcherList
                        If SelP = "ALL" Then 'If All Pitchers are selected
                            theString += "<b>" + pitcher.PitcherName + "</b><br/>"
                            For Each Game In pitcher.Games
                                theString += PrintStats(Game, True)
                                theString += "----------------------------------<br/>"
                            Next
                        ElseIf pitcher.PitcherID.ToString = SelP Then 'If a specific pitcher is selected
                            theString += "<b>" + pitcher.PitcherName + "</b><br/>"
                            For Each Game In pitcher.Games
                                theString += PrintStats(Game, True)
                                theString += "----------------------------------<br/>"
                            Next
                        End If
                    Next
                End If
        End Select

        Return theString
    End Function

    Public Shared Function PrintStats(game As Game, showGameStats As Boolean) As String
        Dim sb As New StringBuilder

        If showGameStats Then
            sb.Append("Game Date: " + game.GameDate + "<br/>")
        End If
        sb.Append("IP: " + game.InningsPitched.ToString + "<br/>")
        sb.Append("K: " + game.Strikeouts.ToString + "<br/>")
        sb.Append("ER: " + game.ERunsAllowed.ToString + "<br/>")
        sb.Append("H: " + game.HitsAllowed.ToString + "<br/>")
        sb.Append("BB: " + game.WalksAllowed.ToString + "<br/>")
        sb.Append("TB: " + game.TotalBasesAllowed.ToString + "<br/>")
        sb.Append("ERA: " + String.Format("{0:0.00}", game.ERA) + "<br/>")
        sb.Append("WHIP: " + String.Format("{0:0.00}", game.WHIP) + "<br/>")
        sb.Append("AVG: " + String.Format("{0:.000}", game.oAVG) + "<br/>")
        sb.Append("OBP: " + String.Format("{0:.000}", game.oOBP) + "<br/>")
        sb.Append("SLUG: " + String.Format("{0:.000}", game.oSLUG) + "<br/>")
        sb.Append("K%: " + String.Format("{0:0.0}", game.kPerc) + "<br/>")
        sb.Append("BB%: " + String.Format("{0:0.0}", game.bbPerc) + "<br/>")
        sb.Append("Fastball Avg Velocity: " + game.PitchVelocity(0).ToString + "<br/>")
        sb.Append("Changeup Avg Velocity: " + game.PitchVelocity(1).ToString + "<br/>")
        sb.Append("Slider Avg Velocity: " + game.PitchVelocity(2).ToString + "<br/>")
        sb.Append("Curve Avg Velocity: " + game.PitchVelocity(3).ToString + "<br/>")
        sb.Append("Sinker Avg Velocity: " + game.PitchVelocity(4).ToString + "<br/>")
        sb.Append("Cutter Avg Velocity: " + game.PitchVelocity(5).ToString + "<br/>")
        sb.Append("Splitter Avg Velocity: " + game.PitchVelocity(6).ToString + "<br/>")

        Return sb.ToString
    End Function

    Public Shared Function PrintPitchesBarGraph(filename As String, startIndex As String) As String
        Dim sb As New StringBuilder

        sb.Append("<script>" + vbCrLf)

        ' set the dimensions and margins of the graph
        sb.Append("var margin = { top: 10, right: 30, bottom: 20, left: 50 }," + vbCrLf)
        sb.Append("width = 460 - margin.left - margin.right," + vbCrLf)
        sb.Append("height = 400 - margin.top - margin.bottom;" + vbCrLf)

        ' append the svg object to the body of the page
        sb.Append("var svg = d3.select(""#my_dataviz"").append(""svg"").attr(""width"", width + margin.left + margin.right).attr(""height"", height + margin.top + margin.bottom).append(""g"").attr(""transform"",""translate("" + margin.left + "","" + margin.top + "")"");" + vbCrLf)

        ' Parse the Data
        sb.Append("d3.csv(""/" + filename + """, function (data) {" + vbCrLf)

        ' List of subgroups = header of the csv files = soil condition here
        sb.Append("var subgroups = data.columns.slice(" + startIndex + ")" + vbCrLf)

        ' List of groups = species here = value of the first column called group -> I show them on the X axis
        sb.Append("var groups = d3.map(data, function (d) { return (d.Pitcher) }).keys()" + vbCrLf)

        ' Add X axis
        sb.Append("var x = d3.scaleBand().domain(groups).range([0, width]).padding([0.2])" + vbCrLf)
        sb.Append("svg.append(""g"").attr(""transform"", ""translate(0,"" + height + "")"").call(d3.axisBottom(x).tickSize(0));" + vbCrLf)

        ' Add Y axis
        sb.Append("var y = d3.scaleLinear().domain([60, 100]).range([height, 0]);" + vbCrLf)
        sb.Append("svg.append(""g"").call(d3.axisLeft(y));" + vbCrLf)

        ' Another scale for subgroup position?
        sb.Append("var xSubgroup = d3.scaleBand().domain(subgroups).range([0, x.bandwidth()]).padding([0.05])" + vbCrLf)

        ' color palette = one color per subgroup
        sb.Append("var color = d3.scaleOrdinal().domain(subgroups)")
        sb.Append(".range([""#ba140c"", ""#ffc100"", ""#09a703"", ""#0c67ba"", ""#7817b0"", ""#ea61d6"", ""#000000"", ""#c9d240""])" + vbCrLf)

        ' Show the bars
        sb.Append("svg.append(""g"")" + vbCrLf)
        sb.Append(".selectAll(""g"")" + vbCrLf)
        ' Enter in data = loop group per group
        sb.Append(".data(data)" + vbCrLf)
        sb.Append(".enter()" + vbCrLf)
        sb.Append(".append(""g"")" + vbCrLf)
        sb.Append(".attr(""transform"", function (d) { return ""translate("" + x(d.Pitcher) + "",0)""; })" + vbCrLf)
        sb.Append(".selectAll(""rect"")" + vbCrLf)
        sb.Append(".data(function (d) { return subgroups.map(function (key) { return { key: key, value: d[key] }; }); })" + vbCrLf)
        sb.Append(".enter().append(""rect"")" + vbCrLf)
        sb.Append(".attr(""x"", function (d) { return xSubgroup(d.key); })" + vbCrLf)
        sb.Append(".attr(""y"", function (d) { return y(d.value); })" + vbCrLf)
        sb.Append(".attr(""width"", xSubgroup.bandwidth())" + vbCrLf)
        sb.Append(".attr(""height"", function (d) { return height - y(d.value); })" + vbCrLf)
        sb.Append(".attr(""fill"", function (d) { return color(d.key); });" + vbCrLf)

        sb.Append("})" + vbCrLf)

        sb.Append("</script>" + vbCrLf)

        Return sb.ToString
    End Function

    Public Shared Function PrintSeasonBarGraph(variable As String, yLow As String, yHigh As String) As String
        Dim sb As New StringBuilder

        sb.Append("<script>" + vbCrLf)
        ' set the dimensions and margins of the graph
        sb.Append("var margin = {top: 30, right: 30, bottom: 70, left: 60}," + vbCrLf)
        sb.Append("width = 460 - margin.left - margin.right," + vbCrLf)
        sb.Append("height = 400 - margin.top - margin.bottom;" + vbCrLf)

        ' append the svg object to the body of the page
        sb.Append("var svg = d3.select(""#my_dataviz"").append(""svg"").attr(""width"", width + margin.left + margin.right).attr(""height"", height + margin.top + margin.bottom).append(""g"").attr(""transform"",""translate("" + margin.left + "","" + margin.top + "")"");" + vbCrLf)

        ' Parse the Data
        sb.Append("d3.csv(""/Data_SeasonAvg.csv"", function(data) {" + vbCrLf)

        ' X axis
        sb.Append("var x = d3.scaleBand().range([ 0, width ]).domain(data.map(function(d) { return d.Pitcher; })).padding(0.2);" + vbCrLf)
        sb.Append("svg.append(""g"").attr(""transform"", ""translate(0,"" + height + "")"").call(d3.axisBottom(x)).selectAll(""text"").attr(""transform"", ""translate(-10,0)rotate(-45)"").style(""text-anchor"", ""end"");" + vbCrLf)

        ' Add Y axis
        sb.Append("var y = d3.scaleLinear().domain([" + yLow + ", " + yHigh + "]).range([ height, 0]);" + vbCrLf)
        sb.Append("svg.append(""g"").call(d3.axisLeft(y));" + vbCrLf)

        ' Bars
        sb.Append("svg.selectAll(""mybar"").data(data).enter().append(""rect"").attr(""x"", function(d) { return x(d.Pitcher); }).attr(""y"", function(d) { return y(d." + variable + "); }).attr(""width"", x.bandwidth()).attr(""height"", function(d) { return height - y(d." + variable + "); }).attr(""fill"", ""#473729"")})" + vbCrLf)

        sb.Append("</script>" + vbCrLf)

        Return sb.ToString
    End Function

    Public Shared Function PrintGameBarGraph(variable As String, yLow As String, yHigh As String) As String
        Dim sb As New StringBuilder

        sb.Append("<script>" + vbCrLf)
        ' set the dimensions and margins of the graph
        sb.Append("var margin = {top: 30, right: 30, bottom: 70, left: 60}," + vbCrLf)
        sb.Append("width = 460 - margin.left - margin.right," + vbCrLf)
        sb.Append("height = 400 - margin.top - margin.bottom;" + vbCrLf)

        ' append the svg object to the body of the page
        sb.Append("var svg = d3.select(""#my_dataviz"").append(""svg"").attr(""width"", width + margin.left + margin.right).attr(""height"", height + margin.top + margin.bottom).append(""g"").attr(""transform"",""translate("" + margin.left + "","" + margin.top + "")"");" + vbCrLf)

        ' Parse the Data
        sb.Append("d3.csv(""/Data_GameAvg.csv"", function(data) {" + vbCrLf)

        ' X axis
        sb.Append("var x = d3.scaleBand().range([ 0, width ]).domain(data.map(function(d) { return d.GameID; })).padding(0.2);" + vbCrLf)
        sb.Append("svg.append(""g"").attr(""transform"", ""translate(0,"" + height + "")"").call(d3.axisBottom(x)).selectAll(""text"").attr(""transform"", ""translate(-10,0)rotate(-45)"").style(""text-anchor"", ""end"");" + vbCrLf)

        ' Add Y axis
        sb.Append("var y = d3.scaleLinear().domain([" + yLow + ", " + yHigh + "]).range([ height, 0]);" + vbCrLf)
        sb.Append("svg.append(""g"").call(d3.axisLeft(y));" + vbCrLf)

        'Color scale
        sb.Append("var color = d3.scaleOrdinal()" + vbCrLf)
        sb.Append(".domain([""Pitcher 1"", ""Pitcher 2"", ""Pitcher 3"", ""Pitcher 4""])" + vbCrLf)
        sb.Append(".range([""#473729"", ""#FFC72C"", ""#041E42"", ""#E35205""])" + vbCrLf)

        ' Bars
        sb.Append("svg.selectAll(""mybar"").data(data).enter().append(""rect"").attr(""x"", function(d) { return x(d.GameID); }).attr(""y"", function(d) { return y(d." + variable + "); }).attr(""width"", x.bandwidth()).attr(""height"", function(d) { return height - y(d." + variable + "); }).style(""fill"", function (d) { return color(d.Pitcher) })})" + vbCrLf)

        sb.Append("</script>" + vbCrLf)

        Return sb.ToString
    End Function

    Public Shared Function ZoneLocation(PitcherID As String, IfGameString As String, IfPrintGame As String, cntr As String) As String
        Dim sb As New StringBuilder

        sb.Append("<script>" + vbCrLf)

        'set the dimensions and margins of the graph
        sb.Append("var margin = { top: 10, right: 50, bottom: 40, left: 50 }," + vbCrLf)
        sb.Append("width = 520 - margin.left - margin.right," + vbCrLf)
        sb.Append("height = 520 - margin.top - margin.bottom;" + vbCrLf)

        'append the svg object to the body of the page -> cntr is there so if there a multiple plot graphs, they won't combine into one graph, creates unique svg variable and there's enough unique div's
        sb.Append("var svg" + cntr + " = d3.select(""#my_dataviz" + cntr + """)" + vbCrLf)
        sb.Append(".append(""svg"")" + vbCrLf)
        sb.Append(".attr(""width"", width + margin.left + margin.right)" + vbCrLf)
        sb.Append(".attr(""height"", height + margin.top + margin.bottom)" + vbCrLf)
        sb.Append(".append(""g"")" + vbCrLf)
        sb.Append(".attr(""transform"",""translate("" + margin.left + "","" + margin.top + "")"")" + vbCrLf)

        'Read the data
        sb.Append("d3.csv(""/pitches.csv"", function (data) {" + vbCrLf)

        'Add X axis
        sb.Append("var x = d3.scaleLinear().domain([-4, 4]).range([0, width])" + vbCrLf)
        sb.Append("svg" + cntr + ".append(""g"")" + vbCrLf)
        sb.Append(".attr(""transform"", ""translate(0,"" + height + "")"")" + vbCrLf)
        sb.Append(".call(d3.axisBottom(x).tickSize(-height * 1.3).ticks(10))" + vbCrLf)
        sb.Append(".select("".domain"").remove()" + vbCrLf)

        'Add Y axis
        sb.Append("var y = d3.scaleLinear().domain([-2, 6]).range([height, 0]).nice()" + vbCrLf)
        sb.Append("svg" + cntr + ".append(""g"").call(d3.axisLeft(y).tickSize(-width * 1.3).ticks(7)).select("".domain"").remove()" + vbCrLf)

        'Customization
        sb.Append("svg" + cntr + ".selectAll("".tick line"").attr(""stroke"", ""black"")" + vbCrLf)

        'Add X axis label:
        sb.Append("svg" + cntr + ".append(""text"").attr(""text-anchor"", ""end"").attr(""x"", width / 2 + margin.left).attr(""y"", height + margin.top + 20).text(""Pitcher " + PitcherID + IfPrintGame + """);" + vbCrLf)

        'Color scale
        sb.Append("var color = d3.scaleOrdinal()" + vbCrLf)
        sb.Append(".domain([""BallCalled"", ""StrikeCalled"", ""StrikeSwinging"", ""FoulBall"", ""InPlay""])" + vbCrLf)
        sb.Append(".range([""#00BA38"", ""#F8766D"", ""#F8766D"", ""#F8766D"", ""#619CFF""])" + vbCrLf)

        'Add dots
        sb.Append("svg" + cntr + ".append('g')" + vbCrLf)
        sb.Append(".selectAll(""dot"")" + vbCrLf)
        sb.Append(".data(data)" + vbCrLf)
        sb.Append(".enter()" + vbCrLf)
        sb.Append(".append(""circle"")" + vbCrLf)
        'Only print dots if data is from correct pitcher and plate data is not nothing (and if game is correct, if supplied)
        sb.Append(".attr(""cx"", function (d) { if (d.pitcherid == " + PitcherID + " && d.platelocheight != """" && d.platelocside != """"" + IfGameString + ") { return x(d.platelocside); }})" + vbCrLf)
        sb.Append(".attr(""cy"", function (d) { if (d.pitcherid == " + PitcherID + " && d.platelocheight != """" && d.platelocside != """"" + IfGameString + ") { return y(d.platelocheight); }})" + vbCrLf)
        sb.Append(".attr(""r"", 4)" + vbCrLf)
        sb.Append(".style(""fill"", function (d) { return color(d.pitchcall) })" + vbCrLf)
        sb.Append("})" + vbCrLf)

        sb.Append("</script>" + vbCrLf)

        Return sb.ToString
    End Function

    Public Shared Function ReleaseLocation(PitcherID As String, IfGameString As String, IfPrintGame As String, cntr As String, throws As String) As String
        Dim sb As New StringBuilder

        'X Range values - Default = Right-handed pitcher
        Dim xStart As String = "1.2"
        Dim xEnd As String = "2.8"

        If UCase(throws) = "LEFT" Then
            xStart = "-1.8"
            xEnd = "-0.2"
        End If

        sb.Append("<script>" + vbCrLf)

        'set the dimensions and margins of the graph
        sb.Append("var margin = { top: 10, right: 50, bottom: 40, left: 50 }," + vbCrLf)
        sb.Append("width = 520 - margin.left - margin.right," + vbCrLf)
        sb.Append("height = 520 - margin.top - margin.bottom;" + vbCrLf)

        'append the svg object to the body of the page
        sb.Append("var svg" + cntr + " = d3.select(""#my_dataviz" + cntr + """)" + vbCrLf)
        sb.Append(".append(""svg"")" + vbCrLf)
        sb.Append(".attr(""width"", width + margin.left + margin.right)" + vbCrLf)
        sb.Append(".attr(""height"", height + margin.top + margin.bottom)" + vbCrLf)
        sb.Append(".append(""g"")" + vbCrLf)
        sb.Append(".attr(""transform"",""translate("" + margin.left + "","" + margin.top + "")"")" + vbCrLf)

        'Read the data
        sb.Append("d3.csv(""/pitches.csv"", function (data) {" + vbCrLf)

        'Add X axis
        sb.Append("var x = d3.scaleLinear().domain([" + xStart + ", " + xEnd + "]).range([0, width])" + vbCrLf)
        sb.Append("svg" + cntr + ".append(""g"")" + vbCrLf)
        sb.Append(".attr(""transform"", ""translate(0,"" + height + "")"")" + vbCrLf)
        sb.Append(".call(d3.axisBottom(x).tickSize(-height * 1.3).ticks(10))" + vbCrLf)
        sb.Append(".select("".domain"").remove()" + vbCrLf)

        'Add Y axis
        sb.Append("var y = d3.scaleLinear().domain([5.2, 6.8]).range([height, 0]).nice()" + vbCrLf)
        sb.Append("svg" + cntr + ".append(""g"").call(d3.axisLeft(y).tickSize(-width * 1.3).ticks(7)).select("".domain"").remove()" + vbCrLf)

        'Customization
        sb.Append("svg" + cntr + ".selectAll("".tick line"").attr(""stroke"", ""black"")" + vbCrLf)

        'Add X axis label:
        sb.Append("svg" + cntr + ".append(""text"").attr(""text-anchor"", ""end"").attr(""x"", width / 2 + margin.left).attr(""y"", height + margin.top + 20).text(""Pitcher " + PitcherID + IfPrintGame + """);" + vbCrLf)

        'Color scale
        '0=Fastball (Red), 1=Changeup (Orange), 2=Slider (Green), 3=Curveball (Blue), 4=Sinker (Purple), 5=Cutter (Pink), 6=Splitter (Black), 7=Other (Yellow)
        sb.Append("var color = d3.scaleOrdinal()" + vbCrLf)
        sb.Append(".domain([""Fastball"", ""Changeup"", ""Slider"", ""Curveball"", ""Sinker"", ""Cutter"", ""Splitter"", ""Other""])" + vbCrLf)
        sb.Append(".range([""#ba140c"", ""#ffc100"", ""#09a703"", ""#0c67ba"", ""#7817b0"", ""#ea61d6"", ""#000000"", ""#c9d240""])" + vbCrLf)

        'Add dots
        sb.Append("svg" + cntr + ".append('g')" + vbCrLf)
        sb.Append(".selectAll(""dot"")" + vbCrLf)
        sb.Append(".data(data)" + vbCrLf)
        sb.Append(".enter()" + vbCrLf)
        sb.Append(".append(""circle"")" + vbCrLf)
        'Only print dots if data is from correct pitcher and rel data is not nothing (and if game is correct, if supplied)
        sb.Append(".attr(""cx"", function (d) { if (d.pitcherid == " + PitcherID + " && d.relheight != """" && d.relside != """"" + IfGameString + ") { return x(d.relside); }})" + vbCrLf)
        sb.Append(".attr(""cy"", function (d) { if (d.pitcherid == " + PitcherID + " && d.relheight != """" && d.relside != """"" + IfGameString + ") { return y(d.relheight); }})" + vbCrLf)
        sb.Append(".attr(""r"", 4)" + vbCrLf)
        sb.Append(".style(""fill"", function (d) { return color(d.pitchcall) })" + vbCrLf)
        sb.Append("})" + vbCrLf)

        sb.Append("</script>" + vbCrLf)

        Return sb.ToString
    End Function

    Public Shared Function PitchPercentage(thePitcher As Pitcher, theGame As Game) As String
        Dim sb As New StringBuilder

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
        If theGame.PitchPercentage(0) > 0 Then sb.Append("""#ba140c"", ")
        If theGame.PitchPercentage(1) > 0 Then sb.Append("""#ffc100"", ")
        If theGame.PitchPercentage(2) > 0 Then sb.Append("""#09a703"", ")
        If theGame.PitchPercentage(3) > 0 Then sb.Append("""#0c67ba"", ")
        If theGame.PitchPercentage(4) > 0 Then sb.Append("""#7817b0"", ")
        If theGame.PitchPercentage(5) > 0 Then sb.Append("""#ea61d6"", ")
        If theGame.PitchPercentage(6) > 0 Then sb.Append("""#000000"", ")
        If theGame.PitchPercentage(7) > 0 Then sb.Append("""#c9d240"", ")
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

        Return sb.ToString
    End Function
End Class
