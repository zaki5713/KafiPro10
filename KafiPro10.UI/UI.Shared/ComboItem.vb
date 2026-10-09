Public Class ComboItem
    Public Property Text As String
    Public Property Value As String
    Public Sub New(t As String, v As String)
        Text = t
        Value = v
    End Sub
    Public Sub New(t As String, v As Integer)
        Text = t
        Value = v.ToString()
    End Sub
    Public Overrides Function ToString() As String
        Return Text
    End Function
End Class