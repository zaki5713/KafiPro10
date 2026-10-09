Namespace KafiPro.Production.Application.DTOs

    ''' <summary>
    ''' نتيجة حفظ سجل الإنتاج
    ''' </summary>
    Public Class DailyProductionSaveResult

        Public Property Success As Boolean
        Public Property Message As String
        Public Property LogID As Long?
        Public Property Errors As List(Of String)

        Public Sub New()
            Success = False
            Message = String.Empty
            LogID = Nothing
            Errors = New List(Of String)()
        End Sub

        Public Shared Function Ok(message As String, logID As Long?) As DailyProductionSaveResult
            Return New DailyProductionSaveResult With {
                .Success = True,
                .Message = message,
                .LogID = logID
            }
        End Function

        Public Shared Function Fail(message As String) As DailyProductionSaveResult
            Return New DailyProductionSaveResult With {
                .Success = False,
                .Message = message
            }
        End Function

        Public Function HasErrors() As Boolean
            Return Errors IsNot Nothing AndAlso Errors.Count > 0
        End Function

        Public Function GetFullMessage() As String
            If HasErrors() Then
                Return String.Join(Environment.NewLine, Errors)
            End If
            Return Message
        End Function

    End Class

End Namespace
