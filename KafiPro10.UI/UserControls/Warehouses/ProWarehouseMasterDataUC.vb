Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Public Class ProWarehouseMasterDataUC

    Private ReadOnly CurrentUserID As Integer = 1 ' مؤقت

    Private WithEvents tabMain As New TabControl()

    Private _ucWarehouses As ProWarehousesTabUC
    Private _ucTypes As ProWarehouseTypesTabUC
    Private _ucBranches As ProBranchesTabUC
    Private _ucWorkers As ProWarehouseWorkersTabUC

    Private Sub ProWarehouseMasterDataUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildLayout()
        LoadTabs()
    End Sub

    Private Sub BuildLayout()
        Me.BackColor = UITheme.Background
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        ' ─── Header ───
        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 45,
            .BackColor = Color.FromArgb(52, 73, 94)
        }

        Dim lblTitle As New Label With {
            .Text = "⚙️   توصيف المخازن — Master Data",
            .Dock = DockStyle.Fill,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 20, 0)
        }
        pnlHeader.Controls.Add(lblTitle)

        ' ─── Tabs ───
        tabMain.Dock = DockStyle.Fill
        tabMain.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        tabMain.ItemSize = New Size(160, 42)
        tabMain.SizeMode = TabSizeMode.Fixed
        tabMain.Padding = New Point(20, 6)

        ' Tab 1: المخازن
        Dim tabWh As New TabPage("🏢  المخازن")
        tabWh.BackColor = Color.White
        tabWh.Padding = New Padding(8)
        tabWh.UseVisualStyleBackColor = False

        ' Tab 2: الأنواع
        Dim tabTypes As New TabPage("📁  أنواع المخازن")
        tabTypes.BackColor = Color.White
        tabTypes.Padding = New Padding(8)
        tabTypes.UseVisualStyleBackColor = False

        ' Tab 3: الفروع
        Dim tabBranches As New TabPage("🏬  الفروع")
        tabBranches.BackColor = Color.White
        tabBranches.Padding = New Padding(8)
        tabBranches.UseVisualStyleBackColor = False

        ' Tab 4: العمالة
        Dim tabWorkers As New TabPage("👷  العمالة")
        tabWorkers.BackColor = Color.White
        tabWorkers.Padding = New Padding(8)
        tabWorkers.UseVisualStyleBackColor = False

        tabMain.TabPages.Add(tabWh)
        tabMain.TabPages.Add(tabTypes)
        tabMain.TabPages.Add(tabBranches)
        tabMain.TabPages.Add(tabWorkers)

        ' ─── Load UC's ───
        _ucWarehouses = New ProWarehousesTabUC() With {.Dock = DockStyle.Fill, .CurrentUserID = CurrentUserID}
        tabWh.Controls.Add(_ucWarehouses)

        _ucTypes = New ProWarehouseTypesTabUC() With {.Dock = DockStyle.Fill}
        tabTypes.Controls.Add(_ucTypes)

        _ucBranches = New ProBranchesTabUC() With {.Dock = DockStyle.Fill, .CurrentUserID = CurrentUserID}
        tabBranches.Controls.Add(_ucBranches)

        _ucWorkers = New ProWarehouseWorkersTabUC() With {.Dock = DockStyle.Fill, .CurrentUserID = CurrentUserID}
        tabWorkers.Controls.Add(_ucWorkers)

        Me.Controls.Add(tabMain)
        Me.Controls.Add(pnlHeader)
    End Sub

    Private Sub LoadTabs()
        ' مش محتاجين حاجة هنا — اتحملت في BuildLayout
    End Sub

End Class