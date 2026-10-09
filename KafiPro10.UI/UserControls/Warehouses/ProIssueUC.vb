Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Public Class ProIssueUC
    Inherits System.Windows.Forms.UserControl

    Private WithEvents tabMain As New TabControl()

    Private Sub ProIssueUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
    End Sub

    Private Sub BuildUI()
        Me.BackColor = UITheme.Background
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        ' ═══ Header ═══
        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top, .Height = 45,
            .BackColor = Color.FromArgb(230, 126, 34)
        }
        pnlHeader.Controls.Add(New Label With {
            .Text = "الصرف",
            .Dock = DockStyle.Fill,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 20, 0)
        })

        ' ═══ Tabs ═══
        tabMain.Dock = DockStyle.Fill
        tabMain.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        tabMain.ItemSize = New Size(150, 35)
        tabMain.SizeMode = TabSizeMode.Fixed
        tabMain.Padding = New Point(20, 6)
        tabMain.RightToLeft = RightToLeft.Yes

        ' Tab 1: صرف للإنتاج
        Dim tabProd As New TabPage("صرف للإنتاج")
        tabProd.BackColor = Color.White
        tabProd.Padding = New Padding(6)
        Dim ucProd As New ProWarehouseWithdrawalUC()
        ucProd.Dock = DockStyle.Fill
        tabProd.Controls.Add(ucProd)

        ' Tab 2: صرف للعملاء
        Dim tabCust As New TabPage("صرف للعملاء")
        tabCust.BackColor = Color.White
        tabCust.Padding = New Padding(6)
        Dim ucCust As New ProSalesIssueUC()
        ucCust.Dock = DockStyle.Fill
        tabCust.Controls.Add(ucCust)

        ' Tab 3: صرف أخرى
        Dim tabOther As New TabPage("صرف أخرى")
        tabOther.BackColor = Color.White
        tabOther.Padding = New Padding(6)
        Dim ucOther As New ProOtherIssueUC()
        ucOther.Dock = DockStyle.Fill
        tabOther.Controls.Add(ucOther)

        tabMain.TabPages.Add(tabProd)
        tabMain.TabPages.Add(tabCust)
        tabMain.TabPages.Add(tabOther)

        Me.Controls.Add(tabMain)
        Me.Controls.Add(pnlHeader)
    End Sub

End Class