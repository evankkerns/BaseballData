Imports Microsoft.VisualBasic

Public Class Game
    Public GameID As String
    Public GameDate As String
    Public Stadium As String
    Public Pitches As New List(Of Pitch)

    Public InningsPitched As Double
    Public Strikeouts As Integer
    Public ERunsAllowed As Integer
    Public HitsAllowed As Integer
    Public WalksAllowed As Integer
    Public HBPAllowed As Integer
    Public SacAllowed As Integer
    Public FieldersChoice As Integer
    Public DroppedThird As Integer 'Does not exist in sample data
    Public Interference As Integer 'Does not exist in sample data
    Public TotalBasesAllowed As Integer
    Public TotalAtBats As Integer
    Public TotalPlateApp As Integer
    Public ERA As Double
    Public WHIP As Double
    Public oAVG As Double
    Public oOBP As Double
    Public oSLUG As Double
    Public kPerc As Double
    Public bbPerc As Double

    '0=Fastball, 1=Changeup, 2=Slider, 3=Curveball, 4=Sinker, 5=Cutter, 6=Splitter, 7=Other
    Public PitchPercentage(7) As Double
    Public PitchTypeCounter(7) As Integer
    Public PitchVelocity(7) As Double
    Public PitchVelocityTotal(7) As Double
    Public PitchVelocityCounter(7) As Integer

    Public FacedRighties As Integer
    Public FacedLefties As Integer

    Public PitchesInWindup As Integer
    Public PitchesInStretch As Integer

    Sub New()

    End Sub
End Class
