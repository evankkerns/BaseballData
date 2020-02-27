Imports Microsoft.VisualBasic

Public Class Pitcher
    Public PitcherID As Integer
    Public PitcherName As String
    Public Throws As String
    Public Games As New List(Of Game)
    'Public GameAvg As New List(Of Game)
    Public SeasonAvg As New Game

    Sub New()

    End Sub

    Function CalcGameAverages() As List(Of Game)
        Return Calculations.CalcGameAverages(Games)
    End Function

    Function CalcSeasonAverages() As Game
        Return Calculations.CalcSeasonAverages(Games)
    End Function
End Class
