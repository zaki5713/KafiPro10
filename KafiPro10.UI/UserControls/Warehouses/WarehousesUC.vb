Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Public Class WarehousesUC

    ' ═══════════════════════════════════════════════════════════
    '  Nav Keys
    ' ═══════════════════════════════════════════════════════════
    Private Const NAV_BALANCE As String = "BALANCE"
    Private Const NAV_RECEIPT As String = "RECEIPT"
    Private Const NAV_ISSUE As String = "ISSUE"
    Private Const NAV_TRANSFER As String = "TRANSFER"
    Private Const NAV_HOLDS As String = "HOLDS"
    Private Const NAV_COUNT As String = "COUNT"
    Private Const NAV_SETTINGS As String = "SETTINGS"
    Private Const NAV_MOVEMENT As String = "MOVEMENT"
    Private Const NAV_REPORTS As String = "REPORTS"
    Private Const NAV_NOTIF As String = "NOTIF"

    ' ─── Controls ───
    Private _tsSubNav As ToolStrip
    Private _contentHost As Panel
    Private _currentUC As UserControl

    ' ─── Tracking ───
    Private _navButtons As New Dictionary(Of String, Button)
    Private _currentActiveKey As String = Nothing

    ' ═══════════════════════════════════════════════════════════
    '  Load
    ' ═══════════════════════════════════════════════════════════
    Private Sub WarehousesUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _contentHost IsNot Nothing Then Return
        BuildLayout()
        BuildSubNav()
        NavigateTo(NAV_BALANCE)
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Layout
    ' ═══════════════════════════════════════════════════════════
    Private Sub BuildLayout()
        Me.BackColor = UITheme.Background
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.Controls.Clear()

        ' ═══ 1. Content Host ═══
        _contentHost = New Panel With {
            .Dock = DockStyle.Fill,
            .Padding = New Padding(8),
            .BackColor = UITheme.Background
        }

        ' ═══ 2. Sub Nav ═══
        _tsSubNav = New ToolStrip With {
            .BackColor = UITheme.ButtonNormal,
            .GripStyle = ToolStripGripStyle.Hidden,
            .RenderMode = ToolStripRenderMode.System,
            .RightToLeft = RightToLeft.Yes,
            .Dock = DockStyle.Top,
            .Padding = New Padding(6, 4, 6, 4),
            .AutoSize = False,
            .Height = 46,
            .Name = "tsSubNav"
        }

        ' ═══ 3. Header ═══
        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 50,
            .BackColor = UITheme.Primary
        }
        pnlHeader.Controls.Add(New Label With {
            .Text = "إدارة المخازن",
            .Dock = DockStyle.Fill,
            .ForeColor = UITheme.TextWhite,
            .Font = New Font("Segoe UI Semibold", 15, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 20, 0)
        })

        ' ═══ 4. Add to Form (الترتيب مهم) ═══
        Me.Controls.Add(_contentHost)   ' Fill
        Me.Controls.Add(_tsSubNav)      ' Top
        Me.Controls.Add(pnlHeader)      ' Top (فوق)
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Sub Nav — الأزرار
    ' ═══════════════════════════════════════════════════════════
    Private Sub BuildSubNav()
        AddNavButton("الأرصدة", NAV_BALANCE)
        AddNavButton("الاستلام", NAV_RECEIPT)
        AddNavButton("الصرف", NAV_ISSUE)
        AddNavButton("التحويل", NAV_TRANSFER)
        AddNavButton("الهولدات", NAV_HOLDS)
        AddNavButton("الجرد", NAV_COUNT)
        AddNavButton("إعدادات المخازن", NAV_SETTINGS)
        AddNavButton("الحركات", NAV_MOVEMENT)
        AddNavButton("التقارير", NAV_REPORTS)
    End Sub

    Private Sub AddNavButton(text As String, key As String)
        ' ⚠️ تأكد إن _tsSubNav موجود
        If _tsSubNav Is Nothing Then Return

        Dim b As New Button With {
            .Text = text,
            .Tag = key,
            .AutoSize = False,
            .Width = 120,
            .Height = 34,
            .Margin = New Padding(2, 0, 2, 0),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = UITheme.ButtonNormal,
            .ForeColor = UITheme.TextPrimary,
            .Font = UITheme.FontBold,
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False,
            .RightToLeft = RightToLeft.Yes
        }
        b.FlatAppearance.BorderSize = 0
        b.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        AddHandler b.Click, AddressOf OnNavClick

        Dim host As New ToolStripControlHost(b)
        _tsSubNav.Items.Add(host)

        _navButtons(key) = b
    End Sub

    Private Sub OnNavClick(sender As Object, e As EventArgs)
        Dim b = DirectCast(sender, Button)
        NavigateTo(CStr(b.Tag))
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Navigate
    ' ═══════════════════════════════════════════════════════════
    Private Sub NavigateTo(key As String)
        _currentActiveKey = key
        ApplyActiveStyle()

        If _currentUC IsNot Nothing Then
            _contentHost.Controls.Remove(_currentUC)
            _currentUC.Dispose()
            _currentUC = Nothing
        End If

        Dim uc As UserControl = CreateScreen(key)
        If uc Is Nothing Then Return

        uc.Dock = DockStyle.Fill
        _contentHost.Controls.Add(uc)
        _currentUC = uc
    End Sub

    Private Sub ApplyActiveStyle()
        For Each kv In _navButtons
            If kv.Key = _currentActiveKey Then
                kv.Value.BackColor = UITheme.ActiveBackground
                kv.Value.ForeColor = UITheme.ActiveText
            Else
                kv.Value.BackColor = UITheme.ButtonNormal
                kv.Value.ForeColor = UITheme.TextPrimary
            End If
        Next
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  CreateScreen
    ' ═══════════════════════════════════════════════════════════
    Private Function CreateScreen(key As String) As UserControl
        Select Case key
            Case NAV_BALANCE : Return New ProStockBalanceUC()
            Case NAV_MOVEMENT : Return New ProMovementUC()
            Case NAV_TRANSFER : Return New ProTransferUC()
            Case NAV_COUNT : Return New ProStockCountUC()
            Case NAV_REPORTS : Return New ProInventoryReportsUC()
            Case NAV_RECEIPT : Return New ProReceiptUC()
            Case NAV_ISSUE : Return New ProIssueUC()
            Case NAV_SETTINGS : Return New ProWarehouseSettingsUC()
            Case NAV_NOTIF : Return New ProNotificationsUC()
            Case NAV_HOLDS : Return Nothing
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub WarehousesUC_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If _currentUC IsNot Nothing Then _currentUC.Dispose()
    End Sub

End Class