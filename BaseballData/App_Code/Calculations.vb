Imports Microsoft.VisualBasic

Public Class Calculations
    Sub New()

    End Sub

    Public Shared Function CalcSeasonAverages(theGames As List(Of Game)) As Game
        Dim newAvg As New Game
        Dim totalPitches As Integer

        For Each SingleGame In theGames
            totalPitches += SingleGame.Pitches.Count
            newAvg.InningsPitched += SingleGame.InningsPitched
            newAvg.Strikeouts += SingleGame.Strikeouts
            newAvg.WalksAllowed += SingleGame.WalksAllowed
            newAvg.HitsAllowed += SingleGame.HitsAllowed
            newAvg.ERunsAllowed += SingleGame.ERunsAllowed
            newAvg.TotalBasesAllowed += SingleGame.TotalBasesAllowed
            newAvg.PitchesInWindup += SingleGame.PitchesInWindup
            newAvg.PitchesInStretch += SingleGame.PitchesInStretch
            newAvg.FacedLefties += SingleGame.FacedLefties
            newAvg.FacedRighties += SingleGame.FacedRighties
            newAvg.TotalPlateApp += SingleGame.TotalPlateApp
            newAvg.TotalAtBats += SingleGame.TotalAtBats

            newAvg.PitchTypeCounter(0) += SingleGame.PitchTypeCounter(0)
            newAvg.PitchTypeCounter(1) += SingleGame.PitchTypeCounter(1)
            newAvg.PitchTypeCounter(2) += SingleGame.PitchTypeCounter(2)
            newAvg.PitchTypeCounter(3) += SingleGame.PitchTypeCounter(3)
            newAvg.PitchTypeCounter(4) += SingleGame.PitchTypeCounter(4)
            newAvg.PitchTypeCounter(5) += SingleGame.PitchTypeCounter(5)
            newAvg.PitchTypeCounter(6) += SingleGame.PitchTypeCounter(6)
            newAvg.PitchTypeCounter(7) += SingleGame.PitchTypeCounter(7)

            newAvg.PitchVelocityTotal(0) += SingleGame.PitchVelocityTotal(0)
            newAvg.PitchVelocityTotal(1) += SingleGame.PitchVelocityTotal(1)
            newAvg.PitchVelocityTotal(2) += SingleGame.PitchVelocityTotal(2)
            newAvg.PitchVelocityTotal(3) += SingleGame.PitchVelocityTotal(3)
            newAvg.PitchVelocityTotal(4) += SingleGame.PitchVelocityTotal(4)
            newAvg.PitchVelocityTotal(5) += SingleGame.PitchVelocityTotal(5)
            newAvg.PitchVelocityTotal(6) += SingleGame.PitchVelocityTotal(6)
            newAvg.PitchVelocityTotal(7) += SingleGame.PitchVelocityTotal(7)

            newAvg.PitchVelocityCounter(0) += SingleGame.PitchVelocityCounter(0)
            newAvg.PitchVelocityCounter(1) += SingleGame.PitchVelocityCounter(1)
            newAvg.PitchVelocityCounter(2) += SingleGame.PitchVelocityCounter(2)
            newAvg.PitchVelocityCounter(3) += SingleGame.PitchVelocityCounter(3)
            newAvg.PitchVelocityCounter(4) += SingleGame.PitchVelocityCounter(4)
            newAvg.PitchVelocityCounter(5) += SingleGame.PitchVelocityCounter(5)
            newAvg.PitchVelocityCounter(6) += SingleGame.PitchVelocityCounter(6)
            newAvg.PitchVelocityCounter(7) += SingleGame.PitchVelocityCounter(7)
        Next

        'ERA
        newAvg.ERA = (newAvg.ERunsAllowed / newAvg.InningsPitched) * 9

        'WHIP
        newAvg.WHIP = (newAvg.HitsAllowed + newAvg.WalksAllowed) / newAvg.InningsPitched

        'Opponent AVG
        newAvg.oAVG = newAvg.HitsAllowed / newAvg.TotalAtBats

        'Opponent OBP
        Dim TopOBP As Integer = newAvg.HitsAllowed + newAvg.WalksAllowed + newAvg.HBPAllowed + newAvg.Interference + newAvg.DroppedThird + newAvg.FieldersChoice
        Dim BotOBP As Integer = newAvg.TotalAtBats + newAvg.HitsAllowed + newAvg.WalksAllowed + newAvg.HBPAllowed + newAvg.Interference + newAvg.DroppedThird + newAvg.FieldersChoice + newAvg.SacAllowed
        newAvg.oOBP = TopOBP / BotOBP

        'Opponent SLUG
        newAvg.oSLUG = newAvg.TotalBasesAllowed / newAvg.TotalAtBats

        'K Percentage
        newAvg.kPerc = (newAvg.Strikeouts / newAvg.TotalPlateApp) * 100

        'BB Percentage
        newAvg.bbPerc = (newAvg.WalksAllowed / newAvg.TotalPlateApp) * 100

        'Pitch Percentage
        'Process:
        'PitchTypeCounter / Total Pitches ->
        'Format into a string with 2 decimal places and a percent sign (ex: 20.35%) ->
        'Remove the percent sign and parse back into a double
        newAvg.PitchPercentage(0) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(0) / totalPitches).Replace("%", ""))
        newAvg.PitchPercentage(1) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(1) / totalPitches).Replace("%", ""))
        newAvg.PitchPercentage(2) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(2) / totalPitches).Replace("%", ""))
        newAvg.PitchPercentage(3) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(3) / totalPitches).Replace("%", ""))
        newAvg.PitchPercentage(4) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(4) / totalPitches).Replace("%", ""))
        newAvg.PitchPercentage(5) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(5) / totalPitches).Replace("%", ""))
        newAvg.PitchPercentage(6) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(6) / totalPitches).Replace("%", ""))
        newAvg.PitchPercentage(7) = Double.Parse(String.Format("{0:0.00%}", newAvg.PitchTypeCounter(7) / totalPitches).Replace("%", ""))

        'Pitch Velocity
        newAvg.PitchVelocity(0) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(0) / newAvg.PitchVelocityCounter(0)))
        newAvg.PitchVelocity(1) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(1) / newAvg.PitchVelocityCounter(1)))
        newAvg.PitchVelocity(2) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(2) / newAvg.PitchVelocityCounter(2)))
        newAvg.PitchVelocity(3) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(3) / newAvg.PitchVelocityCounter(3)))
        newAvg.PitchVelocity(4) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(4) / newAvg.PitchVelocityCounter(4)))
        newAvg.PitchVelocity(5) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(5) / newAvg.PitchVelocityCounter(5)))
        newAvg.PitchVelocity(6) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(6) / newAvg.PitchVelocityCounter(6)))
        newAvg.PitchVelocity(7) = Double.Parse(String.Format("{0:000.0}", newAvg.PitchVelocityTotal(7) / newAvg.PitchVelocityCounter(7)))

        Return newAvg
    End Function

    Public Shared Function CalcGameAverages(theGames As List(Of Game)) As List(Of Game)
        Dim newAvg As New List(Of Game)

        For Each SingleGame In theGames
            Dim tempGame As New Game
            tempGame = SingleGame

            For Each SinglePitch In SingleGame.Pitches
                'Innings Pitched
                Dim IP As Double = SinglePitch.inning - 1
                If SinglePitch.outs = 1 Then IP += 0.3333
                If SinglePitch.outs = 2 Then IP += 0.6667
                If UCase(SinglePitch.korbb) = "STRIKEOUT" Then IP += 0.3333
                If SinglePitch.outsonplay = 1 Then IP += 0.3333
                If SinglePitch.outsonplay = 2 Then IP += 0.6667
                If SinglePitch.outsonplay = 3 Then IP += 1
                If IP > tempGame.InningsPitched Then
                    tempGame.InningsPitched = IP 'If current pitch is greater than saved greatest
                End If

                'Strikeouts
                If UCase(SinglePitch.korbb) = "STRIKEOUT" Then tempGame.Strikeouts += 1

                'Walks
                If UCase(SinglePitch.korbb) = "WALK" Then tempGame.WalksAllowed += 1

                'Runs Allowed
                If UCase(SinglePitch.playresult) <> "ERROR" Then tempGame.ERunsAllowed += SinglePitch.runsscored

                'Hits / Total Bases / HBP / Sac / FieldersChoice
                Select Case UCase(SinglePitch.playresult)
                    Case "SINGLE"
                        tempGame.HitsAllowed += 1
                        tempGame.TotalBasesAllowed += 1
                    Case "DOUBLE"
                        tempGame.HitsAllowed += 1
                        tempGame.TotalBasesAllowed += 2
                    Case "TRIPLE"
                        tempGame.HitsAllowed += 1
                        tempGame.TotalBasesAllowed += 3
                    Case "HOMERUN"
                        tempGame.HitsAllowed += 1
                        tempGame.TotalBasesAllowed += 4
                    Case "HITBYPITCH"
                        tempGame.HBPAllowed += 1
                    Case "SACRIFICE"
                        tempGame.SacAllowed += 1
                    Case "FIELDERSCHOICE"
                        tempGame.FieldersChoice += 1
                End Select

                'Pitcher Set
                Select Case UCase(SinglePitch.pitcherset)
                    Case "WINDUP"
                        tempGame.PitchesInWindup += 1
                    Case "STRETCH"
                        tempGame.PitchesInStretch += 1
                End Select

                'Batter Side
                Select Case UCase(SinglePitch.batterside)
                    Case "LEFT"
                        tempGame.FacedLefties += 1
                    Case "RIGHT"
                        tempGame.FacedRighties += 1
                End Select

                'Pitch Type Counters
                Select Case UCase(SinglePitch.pitchtype)
                    Case "FASTBALL"
                        tempGame.PitchTypeCounter(0) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(0) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(0) += 1
                        End If
                    Case "CHANGEUP"
                        tempGame.PitchTypeCounter(1) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(1) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(1) += 1
                        End If
                    Case "SLIDER"
                        tempGame.PitchTypeCounter(2) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(2) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(2) += 1
                        End If
                    Case "CURVEBALL"
                        tempGame.PitchTypeCounter(3) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(3) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(3) += 1
                        End If
                    Case "SINKER"
                        tempGame.PitchTypeCounter(4) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(4) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(4) += 1
                        End If
                    Case "CUTTER"
                        tempGame.PitchTypeCounter(5) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(5) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(5) += 1
                        End If
                    Case "SPLITTER"
                        tempGame.PitchTypeCounter(6) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(6) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(6) += 1
                        End If
                    Case Else
                        tempGame.PitchTypeCounter(7) += 1
                        If SinglePitch.relspeed.ToString <> "" Then
                            tempGame.PitchVelocityTotal(7) += SinglePitch.relspeed
                            tempGame.PitchVelocityCounter(7) += 1
                        End If
                End Select

                'Total Plate Appearances
                If SinglePitch.pitch_of_pa = 1 Then
                    tempGame.TotalPlateApp += 1
                End If

                'Total At Bats
                tempGame.TotalAtBats = tempGame.TotalPlateApp - tempGame.WalksAllowed - tempGame.HBPAllowed - tempGame.SacAllowed

            Next

            'ERA
            tempGame.ERA = (tempGame.ERunsAllowed / tempGame.InningsPitched) * 9

            'WHIP
            tempGame.WHIP = (tempGame.HitsAllowed + tempGame.WalksAllowed) / tempGame.InningsPitched

            'Opponent AVG
            tempGame.oAVG = tempGame.HitsAllowed / tempGame.TotalAtBats

            'Opponent OBP
            Dim TopOBP As Integer = tempGame.HitsAllowed + tempGame.WalksAllowed + tempGame.HBPAllowed + tempGame.Interference + tempGame.DroppedThird + tempGame.FieldersChoice
            Dim BotOBP As Integer = tempGame.TotalAtBats + tempGame.HitsAllowed + tempGame.WalksAllowed + tempGame.HBPAllowed + tempGame.Interference + tempGame.DroppedThird + tempGame.FieldersChoice + tempGame.SacAllowed
            tempGame.oOBP = TopOBP / BotOBP

            'Opponent SLUG
            tempGame.oSLUG = tempGame.TotalBasesAllowed / tempGame.TotalAtBats

            'K Percentage
            tempGame.kPerc = (tempGame.Strikeouts / tempGame.TotalPlateApp) * 100

            'BB Percentage
            tempGame.bbPerc = (tempGame.WalksAllowed / tempGame.TotalPlateApp) * 100

            'Pitch Percentage
            'Process:
            'PitchTypeCounter / Total Pitches ->
            'Format into a string with 2 decimal places and a percent sign (ex: 20.35%) ->
            'Remove the percent sign and parse back into a double
            tempGame.PitchPercentage(0) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(0) / SingleGame.Pitches.Count).Replace("%", ""))
            tempGame.PitchPercentage(1) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(1) / SingleGame.Pitches.Count).Replace("%", ""))
            tempGame.PitchPercentage(2) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(2) / SingleGame.Pitches.Count).Replace("%", ""))
            tempGame.PitchPercentage(3) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(3) / SingleGame.Pitches.Count).Replace("%", ""))
            tempGame.PitchPercentage(4) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(4) / SingleGame.Pitches.Count).Replace("%", ""))
            tempGame.PitchPercentage(5) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(5) / SingleGame.Pitches.Count).Replace("%", ""))
            tempGame.PitchPercentage(6) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(6) / SingleGame.Pitches.Count).Replace("%", ""))
            tempGame.PitchPercentage(7) = Double.Parse(String.Format("{0:0.00%}", tempGame.PitchTypeCounter(7) / SingleGame.Pitches.Count).Replace("%", ""))

            'Pitch Velocity
            tempGame.PitchVelocity(0) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(0) / tempGame.PitchVelocityCounter(0)))
            tempGame.PitchVelocity(1) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(1) / tempGame.PitchVelocityCounter(1)))
            tempGame.PitchVelocity(2) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(2) / tempGame.PitchVelocityCounter(2)))
            tempGame.PitchVelocity(3) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(3) / tempGame.PitchVelocityCounter(3)))
            tempGame.PitchVelocity(4) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(4) / tempGame.PitchVelocityCounter(4)))
            tempGame.PitchVelocity(5) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(5) / tempGame.PitchVelocityCounter(5)))
            tempGame.PitchVelocity(6) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(6) / tempGame.PitchVelocityCounter(6)))
            tempGame.PitchVelocity(7) = Double.Parse(String.Format("{0:000.0}", tempGame.PitchVelocityTotal(7) / tempGame.PitchVelocityCounter(7)))

            newAvg.Add(tempGame)
        Next

        Return newAvg
    End Function
End Class
