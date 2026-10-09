Imports System.ComponentModel
Imports System.Drawing
Imports FontAwesome.Sharp
Imports System.Windows.Forms

<ToolboxItem(True)>
<DesignerCategory("Code")>
Public Class SidebarControlUC
    Inherits UserControl

    ' ═══════════════════════════════════════════════════════════
    '  المتغيرات
    ' ═══════════════════════════════════════════════════════════
    Private isCollapsed As Boolean = False
    Private activeButton As Button = Nothing

    Private Const EXPANDED_WIDTH As Integer = 210
    Private Const COLLAPSED_WIDTH As Integer = 68
    Private Const ICON_SIZE As Integer = 28

    ' 🎨 خلفيات السايدبار
    Private ReadOnly ColBg As Color = Color.FromArgb(15, 23, 42)
    Private ReadOnly ColHover As Color = Color.FromArgb(30, 41, 59)
    Private ReadOnly ColActive As Color = Color.FromArgb(37, 99, 235)
    Private ReadOnly ColText As Color = Color.FromArgb(203, 213, 225)
    Private ReadOnly ColTextActive As Color = Color.White

    ' 🌈 ألوان الأيقونات المميزة (كل أيقونة بلون مختلف)
    Private ReadOnly ColorDashboard As Color = Color.FromArgb(168, 85, 247)   ' 🟣 بنفسجي
    Private ReadOnly ColorProduction As Color = Color.FromArgb(59, 130, 246)  ' 🔵 أزرق
    Private ReadOnly ColorQuality As Color = Color.FromArgb(34, 197, 94)      ' 🟢 أخضر
    Private ReadOnly ColorWarehouses As Color = Color.FromArgb(249, 115, 22)  ' 🟠 برتقالي
    Private ReadOnly ColorFinance As Color = Color.FromArgb(234, 179, 8)      ' 🟡 ذهبي
    Private ReadOnly ColorSales As Color = Color.FromArgb(236, 72, 153)       ' 🩷 وردي
    Private ReadOnly ColorTransport As Color = Color.FromArgb(6, 182, 212)    ' 🩵 سماوي
    Private ReadOnly ColorSettings As Color = Color.FromArgb(148, 163, 184)   ' ⚪ رمادي فاتح
    Private ReadOnly ColorLogout As Color = Color.FromArgb(248, 113, 113)     ' 🔴 أحمر

    Public Event ModuleSelected As EventHandler(Of ModuleSelectedEventArgs)

    ' ═══════════════════════════════════════════════════════════
    Public Sub New()
        InitializeComponent()
        ApplySidebarStyle()
        SetupIcons()
        SetupButtonAppearance()
        WireEvents()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    Private Sub ApplySidebarStyle()
        Me.BackColor = ColBg
        Me.Width = EXPANDED_WIDTH
        flpButtons.BackColor = ColBg
        flpButtons.AutoSize = False
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  🎨 الأيقونات الملوّنة
    ' ═══════════════════════════════════════════════════════════
    Private Sub SetupIcons()
        btnDashboard.Image = IconChar.ChartPie.ToBitmap(ICON_SIZE, ICON_SIZE, ColorDashboard)
        btnProduction.Image = IconChar.Industry.ToBitmap(ICON_SIZE, ICON_SIZE, ColorProduction)
        btnQuality.Image = IconChar.ClipboardCheck.ToBitmap(ICON_SIZE, ICON_SIZE, ColorQuality)
        btnWarehouses.Image = IconChar.Warehouse.ToBitmap(ICON_SIZE, ICON_SIZE, ColorWarehouses)
        btnFinance.Image = IconChar.Coins.ToBitmap(ICON_SIZE, ICON_SIZE, ColorFinance)
        btnSales.Image = IconChar.ShoppingCart.ToBitmap(ICON_SIZE, ICON_SIZE, ColorSales)
        btnTransport.Image = IconChar.Truck.ToBitmap(ICON_SIZE, ICON_SIZE, ColorTransport)
        btnSettings.Image = IconChar.Gear.ToBitmap(ICON_SIZE, ICON_SIZE, ColorSettings)
        btnLogout.Image = IconChar.SignOutAlt.ToBitmap(ICON_SIZE, ICON_SIZE, ColorLogout)
        btnToggle.Image = IconChar.ChevronLeft.ToBitmap(20, 20, ColorSettings)
    End Sub

    ' ═══════════════════════════════════════════════════════════
    Private Sub SetupButtonAppearance()
        Dim buttons As Button() = {
            btnDashboard, btnProduction, btnQuality, btnWarehouses,
            btnFinance, btnSales, btnTransport, btnSettings,
            btnLogout, btnToggle
        }

        For Each btn As Button In buttons
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = ColHover
            btn.FlatAppearance.MouseDownBackColor = ColActive

            btn.TextImageRelation = TextImageRelation.ImageBeforeText
            btn.ImageAlign = ContentAlignment.MiddleLeft
            btn.TextAlign = ContentAlignment.MiddleLeft
            btn.Padding = New Padding(15, 0, 0, 0)
            btn.Height = 48
            btn.Width = EXPANDED_WIDTH - 10
            btn.Margin = New Padding(5, 2, 5, 2)
            btn.Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Regular)
            btn.ForeColor = ColText
            btn.BackColor = ColBg
            btn.Cursor = Cursors.Hand
            btn.TabStop = False
        Next

        btnToggle.Height = 36
        btnToggle.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)

        ' النصوص
        btnDashboard.Text = "  الداشبورد"
        btnProduction.Text = "  الإنتاج"
        btnQuality.Text = "  الجودة"
        btnWarehouses.Text = "  المخازن"
        btnFinance.Text = "  المالية"
        btnSales.Text = "  المبيعات"
        btnTransport.Text = "  النقل"
        btnSettings.Text = "  الإعدادات"
        btnLogout.Text = "  تسجيل خروج"
        btnToggle.Text = "  طي"
    End Sub

    ' ═══════════════════════════════════════════════════════════
    Private Sub WireEvents()
        AddHandler btnDashboard.Click, AddressOf BtnModule_Click
        AddHandler btnProduction.Click, AddressOf BtnModule_Click
        AddHandler btnQuality.Click, AddressOf BtnModule_Click
        AddHandler btnWarehouses.Click, AddressOf BtnModule_Click
        AddHandler btnFinance.Click, AddressOf BtnModule_Click
        AddHandler btnSales.Click, AddressOf BtnModule_Click
        AddHandler btnTransport.Click, AddressOf BtnModule_Click
        AddHandler btnSettings.Click, AddressOf BtnModule_Click
        AddHandler btnLogout.Click, AddressOf BtnLogout_Click
        AddHandler btnToggle.Click, AddressOf BtnToggle_Click

        Dim hoverButtons As Button() = {
            btnDashboard, btnProduction, btnQuality, btnWarehouses,
            btnFinance, btnSales, btnTransport, btnSettings, btnLogout
        }
        For Each btn As Button In hoverButtons
            AddHandler btn.MouseEnter, AddressOf Btn_MouseEnter
            AddHandler btn.MouseLeave, AddressOf Btn_MouseLeave
        Next
    End Sub

    Private Sub Btn_MouseEnter(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        If btn Is activeButton Then Return
        btn.BackColor = ColHover
    End Sub

    Private Sub Btn_MouseLeave(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        If btn Is activeButton Then Return
        btn.BackColor = ColBg
    End Sub

    ' ═══════════════════════════════════════════════════════════
    Private Sub BtnModule_Click(sender As Object, e As EventArgs)
        Dim clickedButton As Button = DirectCast(sender, Button)
        Dim selectedModule As ModuleType

        Select Case clickedButton.Name
            Case "btnDashboard" : selectedModule = ModuleType.Dashboard
            Case "btnProduction" : selectedModule = ModuleType.Production
            Case "btnQuality" : selectedModule = ModuleType.Quality
            Case "btnWarehouses" : selectedModule = ModuleType.Warehouses
            Case "btnFinance" : selectedModule = ModuleType.Finance
            Case "btnSales" : selectedModule = ModuleType.Sales
            Case "btnTransport" : selectedModule = ModuleType.Transportation
            Case "btnSettings" : selectedModule = ModuleType.Settings
            Case Else : Return
        End Select

        SetActiveButton(clickedButton)
        RaiseEvent ModuleSelected(Me, New ModuleSelectedEventArgs(selectedModule))
    End Sub

    Private Sub SetActiveButton(btn As Button)
        Dim allButtons As Button() = {
            btnDashboard, btnProduction, btnQuality, btnWarehouses,
            btnFinance, btnSales, btnTransport, btnSettings
        }
        For Each b As Button In allButtons
            b.BackColor = ColBg
            b.ForeColor = ColText
        Next

        btn.BackColor = ColActive
        btn.ForeColor = ColTextActive
        activeButton = btn
    End Sub

    ' ═══════════════════════════════════════════════════════════
    Private Sub BtnLogout_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("هل أنت متأكد من تسجيل الخروج؟", "تأكيد الخروج",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════
    Private Sub BtnToggle_Click(sender As Object, e As EventArgs)
        If isCollapsed Then
            isCollapsed = False
            Me.Width = EXPANDED_WIDTH
            btnToggle.Text = "  طي"
            btnToggle.Image = IconChar.ChevronLeft.ToBitmap(20, 20, ColorSettings)
            ShowButtonTexts()
        Else
            isCollapsed = True
            Me.Width = COLLAPSED_WIDTH
            btnToggle.Text = ""
            btnToggle.Image = IconChar.ChevronRight.ToBitmap(20, 20, ColorSettings)
            HideButtonTexts()
        End If
    End Sub

    Private Sub ShowButtonTexts()
        btnDashboard.Text = "  الداشبورد"
        btnProduction.Text = "  الإنتاج"
        btnQuality.Text = "  الجودة"
        btnWarehouses.Text = "  المخازن"
        btnFinance.Text = "  المالية"
        btnSales.Text = "  المبيعات"
        btnTransport.Text = "  النقل"
        btnSettings.Text = "  الإعدادات"
        btnLogout.Text = "  تسجيل خروج"

        For Each ctrl As Control In flpButtons.Controls
            If TypeOf ctrl Is Button Then
                Dim b As Button = DirectCast(ctrl, Button)
                b.TextAlign = ContentAlignment.MiddleLeft
                b.ImageAlign = ContentAlignment.MiddleLeft
                b.TextImageRelation = TextImageRelation.ImageBeforeText
                b.Padding = New Padding(15, 0, 0, 0)
                b.Width = EXPANDED_WIDTH - 10
            End If
        Next
    End Sub

    Private Sub HideButtonTexts()
        btnDashboard.Text = ""
        btnProduction.Text = ""
        btnQuality.Text = ""
        btnWarehouses.Text = ""
        btnFinance.Text = ""
        btnSales.Text = ""
        btnTransport.Text = ""
        btnSettings.Text = ""
        btnLogout.Text = ""

        For Each ctrl As Control In flpButtons.Controls
            If TypeOf ctrl Is Button Then
                Dim b As Button = DirectCast(ctrl, Button)
                b.TextAlign = ContentAlignment.MiddleCenter
                b.ImageAlign = ContentAlignment.MiddleCenter
                b.Padding = New Padding(0)
                b.Width = COLLAPSED_WIDTH - 10
            End If
        Next
    End Sub

End Class