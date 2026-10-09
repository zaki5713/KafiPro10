Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Public Class ProOtherIssueUC
    Inherits System.Windows.Forms.UserControl

    Private Sub ProOtherIssueUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes

        Dim lbl As New Label With {
            .Text = "شاشة الصرف الأخرى (إعدام / عينات / داخلية)" & vbCrLf & vbCrLf & "(تحت الإنشاء)",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.FromArgb(120, 120, 120)
        }
        Me.Controls.Add(lbl)
    End Sub

End Class