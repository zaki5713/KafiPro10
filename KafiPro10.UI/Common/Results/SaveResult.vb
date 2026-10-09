Namespace KafiPro.Common.Results

    ''' <summary>
    ''' نتيجة عملية حفظ (Insert/Update/Delete)
    ''' </summary>
    Public Class SaveResult

        Public Property Success As Boolean
        Public Property Message As String
        Public Property ID As Long?
        Public Property Errors As List(Of String)

        Public Sub New()
            Success = False
            Message = String.Empty
            ID = Nothing
            Errors = New List(Of String)()
        End Sub

        Public Sub New(success As Boolean, message As String, id As Long?)
            Me.Success = success
            Me.Message = If(message, String.Empty)
            Me.ID = id
            Me.Errors = New List(Of String)()
        End Sub

        Public Shared Function Ok(message As String, id As Long?) As SaveResult
            Return New SaveResult(True, message, id)
        End Function

        Public Shared Function Fail(message As String) As SaveResult
            Return New SaveResult(False, message, Nothing)
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
