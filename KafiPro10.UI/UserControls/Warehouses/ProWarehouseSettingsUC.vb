Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Public Class ProWarehouseSettingsUC
    Inherits System.Windows.Forms.UserControl

    Private WithEvents tabMain As New TabControl()

    Private Sub ProWarehouseSettingsUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
    End Sub

    Private Sub BuildUI()
        Me.BackColor = UITheme.Background
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        ' ═══ Header ═══
        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top, .Height = 45,
            .BackColor = Color.FromArgb(52, 73, 94)
        }
        pnlHeader.Controls.Add(New Label With {
            .Text = "إعدادات المخازن",
            .Dock = DockStyle.Fill,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 20, 0)
        })

        ' ═══ Tabs ═══
        tabMain.Dock = DockStyle.Fill
        tabMain.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        tabMain.ItemSize = New Size(180, 42)
        tabMain.SizeMode = TabSizeMode.Fixed
        tabMain.Padding = New Point(20, 6)

        ' Tab 1: توصيف المخازن
        Dim tabMaster As New TabPage("توصيف المخازن")
        tabMaster.BackColor = Color.White
        tabMaster.Padding = New Padding(6)
        Dim ucMaster As New ProWarehouseMasterDataUC()
        ucMaster.Dock = DockStyle.Fill
        tabMaster.Controls.Add(ucMaster)

        ' Tab 2: أرصدة افتتاحية
        Dim tabOpening As New TabPage("أرصدة افتتاحية")
        tabOpening.BackColor = Color.White
        tabOpening.Padding = New Padding(6)
        Dim ucOpening As New ProOpeningBalanceUC()
        ucOpening.Dock = DockStyle.Fill
        tabOpening.Controls.Add(ucOpening)

        tabMain.TabPages.Add(tabMaster)
        tabMain.TabPages.Add(tabOpening)

        Me.Controls.Add(tabMain)
        Me.Controls.Add(pnlHeader)
    End Sub

End Class