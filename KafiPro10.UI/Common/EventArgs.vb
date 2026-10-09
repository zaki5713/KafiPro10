' ملف: Common\EventArgs.vb
' ملف: Common\EventArgs.vb
' ✅ لا تضع Namespace هنا، دع المشروع يتعرف عليه تلقائياً

Public Class ModuleSelectedEventArgs
    Inherits EventArgs

    Public ReadOnly Property SelectedModule As ModuleType

    Public Sub New(selectedModule As ModuleType)
        Me.SelectedModule = selectedModule
    End Sub
End Class