Imports System.ComponentModel

<ToolboxItem(True)>
<DesignerCategory("Code")>
Public Class BaseUC
    Inherits UserControl

    ' خاصية لتغيير عنوان الهيدر من الشاشات الفرعية
    Public Property HeaderTitle As String
        Get
            Return lblHeaderTitle.Text
        End Get
        Set(value As String)
            lblHeaderTitle.Text = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub

    ' دالة لاستقبال محتوى الشاشة الفرعية (مثل لوحة التحكم أو الجداول)
    Public Sub SetContent(content As Control)
        pnlContentContainer.Controls.Clear()
        content.Dock = DockStyle.Fill
        pnlContentContainer.Controls.Add(content)
    End Sub
End Class
