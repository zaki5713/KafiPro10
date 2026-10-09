Imports System.Collections.Generic

Public Class SalesShortageService
    Private ReadOnly _repo As SalesShortageRepository

    Public Sub New(repo As SalesShortageRepository)
        _repo = repo
    End Sub

    Public Function GenerateFromStock(userId As Integer) As Integer
        Dim low As List(Of SalesShortageDTO) = _repo.ComputeLowStock()
        Dim added As Integer = 0
        Dim stamp As String = "AUTO-" & Date.Now.ToString("yyMMddHHmm")
        For Each s In low
            If Not _repo.HasOpenForItem(s.ItemID) Then
                s.SourceType = 2
                s.Status = 1
                s.ReferenceNo = stamp
                _repo.Insert(s, userId)
                added += 1
            End If
        Next
        Return added
    End Function
End Class