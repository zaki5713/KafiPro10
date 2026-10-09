Namespace KafiPro.Common.Results

    ''' <summary>
    ''' نتيجة عملية عامة (نجاح/فشل + رسائل)
    ''' </summary>
    Public Class OperationResult

        Public Property Success As Boolean
        Public Property Message As String
        Public Property Errors As List(Of String)

        Public Sub New()
            Success = False
            Message = String.Empty
            Errors = New List(Of String)()
        End Sub

        Public Sub New(success As Boolean, message As String)
            Me.Success = success
            Me.Message = If(message, String.Empty)
            Me.Errors = New List(Of String)()
        End Sub

        Public Shared Function Ok(message As String) As OperationResult
            Return New OperationResult(True, message)
        End Function

        Public Shared Function Fail(message As String) As OperationResult
            Return New OperationResult(False, message)
        End Function

        Public Shared Function Fail(errors As IEnumerable(Of String)) As OperationResult
            Dim result As New OperationResult(False, "فشلت العملية.")
            If errors IsNot Nothing Then
                result.Errors.AddRange(errors)
            End If
            Return result
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
