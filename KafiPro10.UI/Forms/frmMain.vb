Imports System.Linq
Imports System.Drawing
Imports KafiPro.UI.Shared.Themes

Public Class FrmMain

    Private sidebarUC As SidebarControlUC
    Private originalMaximizedBounds As Rectangle

    ' ============================================================
    '  تحميل الفورم الرئيسي
    ' ============================================================
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' ✅ فتح الفورم ماكسمايز مع الحفاظ على شريط المهام السفلي
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.MaximizedBounds = Screen.FromControl(Me).WorkingArea
        Me.WindowState = FormWindowState.Maximized

        ApplyTheme()

        lblUserInfo.Text = $"👤 admin   |   📅 {DateTime.Now:yyyy-MM-dd}"

        sidebarUC = New SidebarControlUC() With {
            .Dock = DockStyle.Fill
        }
        pnlSidebar.Controls.Add(sidebarUC)

        AddHandler sidebarUC.ModuleSelected, AddressOf OnModuleSelected
        pnlSidebar.BackColor = UITheme.SidebarBackground

        LoadModule(ModuleType.Production)
    End Sub

    ' ============================================================
    '  تطبيق الثيم الموحد
    ' ============================================================
    Private Sub ApplyTheme()
        Me.BackColor = UITheme.Background
        Me.Font = UITheme.FontNormal
        Me.ForeColor = UITheme.TextPrimary
        Me.DoubleBuffered = True

        ' الهيدر
        pnlHeader.BackColor = UITheme.Primary
        pnlHeader.Padding = New Padding(15, 0, 15, 0)
        pnlHeader.Height = 55

        lblAppName.Font = New Font("Segoe UI Semibold", 13.5F, FontStyle.Bold)
        lblAppName.ForeColor = UITheme.TextWhite

        lblUserInfo.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        lblUserInfo.ForeColor = UITheme.TextWhite

        ' السايدبار – تصغير العرض
        pnlSidebar.BackColor = UITheme.SidebarBackground
        pnlSidebar.Width = 190   ' ✅ كان افتراضي (220 أو أكبر) – صغّرناه

        ' المحتوى
        pnlContent.BackColor = UITheme.Background
        pnlContent.Padding = New Padding(10)
    End Sub

    ' ============================================================
    '  عند اختيار موديول من السايدبار
    ' ============================================================
    Private Sub OnModuleSelected(sender As Object, e As ModuleSelectedEventArgs)
        LoadModule(e.SelectedModule)
    End Sub

    ' ============================================================
    '  تحميل الموديول داخل pnlContent
    ' ============================================================
    Private Sub LoadModule(selectedModule As ModuleType)
        Try
            Dim uc As UserControl = CreateModule(selectedModule)
            If uc Is Nothing Then Return

            ClearContent()
            uc.Dock = DockStyle.Fill
            pnlContent.Controls.Add(uc)
        Catch ex As Exception
            MessageBox.Show($"حدث خطأ أثناء تحميل الموديول: {ex.Message}",
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearContent()
        Dim oldControls As Control() = pnlContent.Controls.Cast(Of Control)().ToArray()
        For Each ctrl As Control In oldControls
            ctrl.Dispose()
        Next
        pnlContent.Controls.Clear()
    End Sub

    Private Function CreateModule(selectedModule As ModuleType) As UserControl
        Select Case selectedModule
            Case ModuleType.Production
                Return New ProductionUC()
            Case ModuleType.Warehouses          ' ← جديد
                Return New WarehousesUC()
            Case ModuleType.Dashboard
                ShowNotImplemented("الداشبورد")
            Case ModuleType.Quality
                ShowNotImplemented("الجودة")
            Case ModuleType.Finance
                ShowNotImplemented("المالية")
            Case ModuleType.Sales
                Return New SalesUC()
            Case ModuleType.Transportation
                ShowNotImplemented("النقل")
            Case ModuleType.Settings
                ShowNotImplemented("الإعدادات")
            Case Else
                ShowNotImplemented("هذا الموديول")
        End Select
        Return Nothing
    End Function

    Private Sub ShowNotImplemented(moduleName As String)
        MessageBox.Show($"موديول {moduleName} قيد التطوير",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
    End Sub

    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If sidebarUC IsNot Nothing Then
            RemoveHandler sidebarUC.ModuleSelected, AddressOf OnModuleSelected
            sidebarUC.Dispose()
            sidebarUC = Nothing
        End If
    End Sub

End Class