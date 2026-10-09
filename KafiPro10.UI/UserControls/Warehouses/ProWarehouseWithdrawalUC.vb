Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence
Imports KafiPro.UI.Shared.Themes

Partial Public Class ProWarehouseWithdrawalUC

    Private ReadOnly _svc As New WithdrawalService()
    Private ReadOnly CurrentUserID As Integer = 1

    Private _cboStatus As ComboBox
    Private _cboWarehouse As ComboBox
    Private _txtSearch As TextBox
    Private _btnRefresh As Button
    Private _btnIssue As Button
    Private _btnReject As Button
    Private _btnDetails As Button
    Private _grid As DataGridView

    Private _orders As List(Of WithdrawalOrderDto)
    Private _loaded As Boolean = False

    Public Sub New()
        InitializeComponent()
        BuildUI()
    End Sub

    Private Sub ProWarehouseWithdrawalUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _loaded Then Return
        _loaded = True
        LoadLookups()
        LoadData()
    End Sub

    Private Sub BuildUI()
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        ' ═══ Header ═══
        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top, .Height = 45,
            .BackColor = Color.FromArgb(230, 126, 34)
        }
        pnlHeader.Controls.Add(New Label With {
            .Text = "📦   طلبات الصرف الواردة من الإنتاج",
            .Dock = DockStyle.Fill,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 20, 0)
        })

        ' ═══ Filter Bar ═══
        Dim pnlFilter As New Panel With {
            .Dock = DockStyle.Top, .Height = 58,
            .BackColor = Color.FromArgb(245, 247, 250),
            .Padding = New Padding(10)
        }

        AddLbl(pnlFilter, "الحالة:", 1050, 20)
        _cboStatus = New ComboBox With {
            .Location = New Point(900, 16), .Size = New Size(140, 28),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }
        pnlFilter.Controls.Add(_cboStatus)

        AddLbl(pnlFilter, "المخزن:", 800, 20)
        _cboWarehouse = New ComboBox With {
            .Location = New Point(620, 16), .Size = New Size(170, 28),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }
        pnlFilter.Controls.Add(_cboWarehouse)

        AddLbl(pnlFilter, "بحث:", 570, 20)
        _txtSearch = New TextBox With {
            .Location = New Point(440, 16), .Size = New Size(120, 28)
        }
        pnlFilter.Controls.Add(_txtSearch)

        _btnRefresh = New Button With {
            .Text = "🔄", .Location = New Point(380, 13), .Size = New Size(50, 32)
        }
        StyleBtn(_btnRefresh, Color.FromArgb(149, 165, 166))
        pnlFilter.Controls.Add(_btnRefresh)

        ' ═══ Action Bar ═══
        Dim pnlActions As New Panel With {
            .Dock = DockStyle.Top, .Height = 62,
            .BackColor = Color.White, .Padding = New Padding(10)
        }

        _btnIssue = New Button With {
            .Text = "📦 صرف الأصناف", .Location = New Point(10, 15), .Size = New Size(155, 34)
        }
        StyleBtn(_btnIssue, Color.FromArgb(230, 126, 34))

        _btnReject = New Button With {
            .Text = "❌ رفض الطلب", .Location = New Point(175, 15), .Size = New Size(150, 34)
        }
        StyleBtn(_btnReject, Color.FromArgb(192, 57, 43))

        _btnDetails = New Button With {
            .Text = "🔍 تفاصيل", .Location = New Point(335, 15), .Size = New Size(130, 34)
        }
        StyleBtn(_btnDetails, Color.FromArgb(41, 128, 185))

        pnlActions.Controls.AddRange({_btnIssue, _btnReject, _btnDetails})

        ' ═══ Grid ═══
        _grid = New DataGridView With {
            .Dock = DockStyle.Fill,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .RightToLeft = RightToLeft.Yes,
            .AllowUserToAddRows = False,
            .ReadOnly = True,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .ColumnHeadersHeight = 40,
            .EnableHeadersVisualStyles = False
        }
        _grid.RowTemplate.Height = 32
        _grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 126, 34)
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        _grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)

        ' ═══ Layout ═══
        Me.Controls.Add(_grid)
        Me.Controls.Add(pnlActions)
        Me.Controls.Add(pnlFilter)
        Me.Controls.Add(pnlHeader)

        ' ═══ Events ═══
        AddHandler _btnRefresh.Click, AddressOf OnRefresh
        AddHandler _btnIssue.Click, AddressOf OnIssue
        AddHandler _btnReject.Click, AddressOf OnReject
        AddHandler _btnDetails.Click, AddressOf OnDetails
        AddHandler _cboStatus.SelectedIndexChanged, AddressOf OnFilterChanged
        AddHandler _cboWarehouse.SelectedIndexChanged, AddressOf OnFilterChanged
        AddHandler _txtSearch.KeyDown, AddressOf OnSearchKeyDown
        AddHandler _grid.DoubleClick, AddressOf OnDetails
    End Sub

    Private Sub AddLbl(parent As Control, text As String, x As Integer, y As Integer)
        parent.Controls.Add(New Label With {
            .Text = text, .Location = New Point(x, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        })
    End Sub

    Private Sub StyleBtn(b As Button, c As Color)
        b.BackColor = c
        b.ForeColor = Color.White
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        b.Cursor = Cursors.Hand
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Lookups
    ' ═══════════════════════════════════════════════════════════
    Private Sub LoadLookups()
        Try
            ' الحالات — "الكل" افتراضي
            _cboStatus.Items.Clear()
            _cboStatus.Items.Add(New ComboItem("الكل", ""))
            _cboStatus.Items.Add(New ComboItem("مسودة", "1"))
            _cboStatus.Items.Add(New ComboItem("بانتظار الصرف", "2"))
            _cboStatus.Items.Add(New ComboItem("تم الصرف", "3"))
            _cboStatus.Items.Add(New ComboItem("تم الاستلام", "4"))
            _cboStatus.Items.Add(New ComboItem("معتمد", "5"))
            _cboStatus.Items.Add(New ComboItem("ملغي", "6"))
            _cboStatus.Items.Add(New ComboItem("مرفوض", "7"))
            _cboStatus.SelectedIndex = 0

            ' المخازن
            _cboWarehouse.Items.Clear()
            _cboWarehouse.Items.Add(New ComboItem("الكل", ""))
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand(
                    "SELECT WarehouseID, WarehouseNameAr FROM [Core].[Warehouse] WHERE IsDeleted=0 ORDER BY WarehouseCode", conn)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            _cboWarehouse.Items.Add(New ComboItem(
                                CStr(rd("WarehouseNameAr")),
                                CInt(rd("WarehouseID")).ToString()))
                        End While
                    End Using
                End Using
            End Using
            _cboWarehouse.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل القوائم: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Load Data
    ' ═══════════════════════════════════════════════════════════
    Public Sub LoadData()
        Try
            Dim statusID As Integer? = Nothing
            If _cboStatus.SelectedItem IsNot Nothing Then
                Dim v = DirectCast(_cboStatus.SelectedItem, ComboItem).Value
                If Not String.IsNullOrEmpty(v) Then statusID = Integer.Parse(v)
            End If

            Dim whID As Integer? = Nothing
            If _cboWarehouse.SelectedItem IsNot Nothing Then
                Dim v = DirectCast(_cboWarehouse.SelectedItem, ComboItem).Value
                If Not String.IsNullOrEmpty(v) Then whID = Integer.Parse(v)
            End If

            _orders = _svc.GetAll(Nothing, Nothing, statusID, whID)

            Dim search = _txtSearch.Text.Trim()
            If Not String.IsNullOrEmpty(search) Then
                _orders = _orders.FindAll(Function(o)
                                              Return o.WithdrawalNo.Contains(search) OrElse
                                                     (o.Notes IsNot Nothing AndAlso o.Notes.Contains(search))
                                          End Function)
            End If

            Dim dt As New DataTable()
            dt.Columns.Add("WithdrawalID", GetType(Long))
            dt.Columns.Add("رقم الطلب", GetType(String))
            dt.Columns.Add("التاريخ", GetType(String))
            dt.Columns.Add("المخزن", GetType(String))
            dt.Columns.Add("الحالة", GetType(String))
            dt.Columns.Add("عدد الأصناف", GetType(Integer))
            dt.Columns.Add("المطلوب", GetType(Decimal))
            dt.Columns.Add("المصروف", GetType(Decimal))
            dt.Columns.Add("ملاحظات", GetType(String))

            For Each o In _orders
                dt.Rows.Add(o.WithdrawalID,
                            o.WithdrawalNo,
                            o.WithdrawalDate.ToString("yyyy-MM-dd"),
                            o.WarehouseName,
                            o.StatusNameAr,
                            o.ItemsCount,
                            o.TotalRequestedQty,
                            o.TotalIssuedQty,
                            If(o.Notes, ""))
            Next

            _grid.DataSource = dt
            If _grid.Columns.Contains("WithdrawalID") Then _grid.Columns("WithdrawalID").Visible = False

            ' تلوين حسب الحالة
            For Each row As DataGridViewRow In _grid.Rows
                Dim status = row.Cells("الحالة").Value?.ToString()
                If status = "مُرسل للمخزن" Then
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220)
                ElseIf status = "تم الصرف" Then
                    row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233)
                ElseIf status = "معتمد" Then
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 237, 200)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("خطأ في التحميل: " & ex.Message & vbCrLf & ex.StackTrace,
                            "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetSelected() As WithdrawalOrderDto
        If _grid.CurrentRow Is Nothing Then
            MessageBox.Show("اختر طلباً أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
        Dim id = CLng(_grid.CurrentRow.Cells("WithdrawalID").Value)
        Return _orders.Find(Function(x) x.WithdrawalID = id)
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Events
    ' ═══════════════════════════════════════════════════════════
    Private Sub OnRefresh(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Private Sub OnFilterChanged(sender As Object, e As EventArgs)
        If IsHandleCreated Then LoadData()
    End Sub

    Private Sub OnSearchKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then LoadData()
    End Sub

    Private Sub OnIssue(sender As Object, e As EventArgs)
        Dim o = GetSelected() : If o Is Nothing Then Return

        If o.StatusID <> 2 Then
            MessageBox.Show("يمكن صرف الطلبات المُرسلة فقط." & vbCrLf &
                            "حالة الطلب الحالية: " & o.StatusNameAr,
                            "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim full = _svc.GetById(o.WithdrawalID)
        If full Is Nothing Then
            MessageBox.Show("فشل تحميل تفاصيل الطلب.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using frm As New FrmWithdrawalOrderIssue(full)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub OnReject(sender As Object, e As EventArgs)
        Dim o = GetSelected() : If o Is Nothing Then Return
        If o.StatusID <> 2 Then
            MessageBox.Show("يمكن رفض الطلبات المُرسلة فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim reason = InputBox("سبب الرفض:", "رفض الطلب " & o.WithdrawalNo)
        If String.IsNullOrWhiteSpace(reason) Then Return

        Try
            _svc.Reject(o.WithdrawalID, reason, CurrentUserID)
            MessageBox.Show("✅ تم رفض الطلب.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnDetails(sender As Object, e As EventArgs)
        Dim o = GetSelected() : If o Is Nothing Then Return
        Dim full = _svc.GetById(o.WithdrawalID)
        If full Is Nothing Then Return

        Using frm As New FrmWithdrawalOrderDetails(full)
            frm.ShowDialog(Me)
        End Using
    End Sub

End Class