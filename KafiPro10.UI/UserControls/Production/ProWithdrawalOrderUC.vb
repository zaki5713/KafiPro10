Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence
Imports KafiPro.UI.Shared.Themes

Partial Public Class ProWithdrawalOrderUC

    Private ReadOnly _svc As New WithdrawalService()
    Private ReadOnly CurrentUserID As Integer = 1

    ' ═══════════════════════════════════════════════════════════
    '  Controls
    ' ═══════════════════════════════════════════════════════════
    Private WithEvents _cboStatus As New ComboBox()
    Private WithEvents _cboWarehouse As New ComboBox()
    Private WithEvents _dtpFrom As New DateTimePicker()
    Private WithEvents _dtpTo As New DateTimePicker()
    Private WithEvents _txtSearch As New TextBox()
    Private WithEvents _btnRefresh As New Button()
    Private WithEvents _btnNew As New Button()
    Private WithEvents _btnEdit As New Button()
    Private WithEvents _btnSubmit As New Button()
    Private WithEvents _btnIssue As New Button()
    Private WithEvents _btnReceive As New Button()
    Private WithEvents _btnApprove As New Button()
    Private WithEvents _btnReject As New Button()
    Private WithEvents _btnCancel As New Button()
    Private WithEvents _btnDelete As New Button()
    Private WithEvents _btnDetails As New Button()
    Private WithEvents _grid As New DataGridView()

    Private _orders As List(Of WithdrawalOrderDto)
    Private _loaded As Boolean = False

    ' ═══════════════════════════════════════════════════════════
    '  Constructor
    ' ═══════════════════════════════════════════════════════════
    Public Sub New()
        InitializeComponent()
        BuildUI()
    End Sub

    Private Sub ProWithdrawalOrderUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _loaded Then Return
        _loaded = True

        LoadLookups()
        LoadData()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Build UI
    ' ═══════════════════════════════════════════════════════════
    Private Sub BuildUI()
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        ' ═══ Header ═══
        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top, .Height = 45,
            .BackColor = Color.FromArgb(52, 73, 94)
        }
        pnlHeader.Controls.Add(New Label With {
            .Text = "📤   طلبات الصرف للإنتاج",
            .Dock = DockStyle.Fill,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 20, 0)
        })

        ' ═══ Filter Bar ═══
        Dim pnlTop As New Panel With {
            .Dock = DockStyle.Top, .Height = 62,
            .BackColor = Color.FromArgb(245, 247, 250),
            .Padding = New Padding(10)
        }

        AddLabel(pnlTop, "الحالة:", 1050, 22)
        _cboStatus.Location = New Point(910, 18)
        _cboStatus.Size = New Size(130, 28)
        _cboStatus.DropDownStyle = ComboBoxStyle.DropDownList
        pnlTop.Controls.Add(_cboStatus)

        AddLabel(pnlTop, "المخزن:", 810, 22)
        _cboWarehouse.Location = New Point(630, 18)
        _cboWarehouse.Size = New Size(170, 28)
        _cboWarehouse.DropDownStyle = ComboBoxStyle.DropDownList
        pnlTop.Controls.Add(_cboWarehouse)

        AddLabel(pnlTop, "من:", 580, 22)
        _dtpFrom.Location = New Point(485, 18)
        _dtpFrom.Size = New Size(90, 28)
        _dtpFrom.Format = DateTimePickerFormat.Short
        _dtpFrom.Value = DateTime.Today.AddDays(-30)
        pnlTop.Controls.Add(_dtpFrom)

        AddLabel(pnlTop, "إلى:", 440, 22)
        _dtpTo.Location = New Point(345, 18)
        _dtpTo.Size = New Size(90, 28)
        _dtpTo.Format = DateTimePickerFormat.Short
        _dtpTo.Value = DateTime.Today
        pnlTop.Controls.Add(_dtpTo)

        AddLabel(pnlTop, "بحث:", 300, 22)
        _txtSearch.Location = New Point(160, 18)
        _txtSearch.Size = New Size(135, 28)
        pnlTop.Controls.Add(_txtSearch)

        _btnRefresh.Text = "🔄"
        _btnRefresh.Location = New Point(100, 15)
        _btnRefresh.Size = New Size(50, 32)
        StyleButton(_btnRefresh, Color.FromArgb(149, 165, 166))
        pnlTop.Controls.Add(_btnRefresh)

        ' ═══ Action Bar ═══
        Dim pnlActions As New Panel With {
            .Dock = DockStyle.Top, .Height = 62,
            .BackColor = Color.White, .Padding = New Padding(10)
        }

        _btnNew.Text = "➕ جديد"
        _btnNew.Location = New Point(10, 15)
        _btnNew.Size = New Size(105, 34)
        StyleButton(_btnNew, Color.FromArgb(46, 204, 113))

        _btnEdit.Text = "✏️ تعديل"
        _btnEdit.Location = New Point(120, 15)
        _btnEdit.Size = New Size(105, 34)
        StyleButton(_btnEdit, Color.FromArgb(52, 152, 219))

        _btnSubmit.Text = "📤 إرسال"
        _btnSubmit.Location = New Point(230, 15)
        _btnSubmit.Size = New Size(105, 34)
        StyleButton(_btnSubmit, Color.FromArgb(155, 89, 182))


        _btnReceive.Text = "📥 استلام"
        _btnReceive.Location = New Point(450, 15)
        _btnReceive.Size = New Size(105, 34)
        StyleButton(_btnReceive, Color.FromArgb(26, 188, 156))

        _btnApprove.Text = "✅ اعتماد"
        _btnApprove.Location = New Point(560, 15)
        _btnApprove.Size = New Size(105, 34)
        StyleButton(_btnApprove, Color.FromArgb(39, 174, 96))


        _btnCancel.Text = "🚫 إلغاء"
        _btnCancel.Location = New Point(780, 15)
        _btnCancel.Size = New Size(105, 34)
        StyleButton(_btnCancel, Color.FromArgb(127, 140, 141))

        _btnDelete.Text = "🗑️ حذف"
        _btnDelete.Location = New Point(890, 15)
        _btnDelete.Size = New Size(105, 34)
        StyleButton(_btnDelete, Color.FromArgb(231, 76, 60))

        _btnDetails.Text = "🔍 تفاصيل"
        _btnDetails.Location = New Point(1000, 15)
        _btnDetails.Size = New Size(105, 34)
        StyleButton(_btnDetails, Color.FromArgb(41, 128, 185))

        pnlActions.Controls.AddRange({_btnNew, _btnEdit, _btnSubmit, _btnReceive,
                               _btnApprove, _btnCancel, _btnDelete, _btnDetails})

        ' ═══ Grid ═══
        _grid.Dock = DockStyle.Fill
        _grid.BackgroundColor = Color.White
        _grid.BorderStyle = BorderStyle.None
        _grid.RightToLeft = RightToLeft.Yes
        _grid.AllowUserToAddRows = False
        _grid.ReadOnly = True
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        _grid.MultiSelect = False
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        _grid.ColumnHeadersHeight = 40
        _grid.RowTemplate.Height = 32
        _grid.EnableHeadersVisualStyles = False
        _grid.ColumnHeadersDefaultCellStyle.BackColor = UITheme.Primary
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        _grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250)

        ' ═══ Layout ═══
        Me.Controls.Add(_grid)
        Me.Controls.Add(pnlActions)
        Me.Controls.Add(pnlTop)
        Me.Controls.Add(pnlHeader)
    End Sub

    Private Sub AddLabel(parent As Control, text As String, x As Integer, y As Integer)
        parent.Controls.Add(New Label With {
            .Text = text, .Location = New Point(x, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        })
    End Sub

    Private Sub StyleButton(b As Button, c As Color)
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
            ' الحالات
            _cboStatus.Items.Clear()
            _cboStatus.Items.Add(New ComboItem("الكل", ""))
            For Each s In _svc.GetStatusLookup()
                _cboStatus.Items.Add(New ComboItem(s.StatusNameAr, s.StatusID.ToString()))
            Next
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
            MessageBox.Show("خطأ في تحميل القوائم: " & ex.Message & vbCrLf & ex.StackTrace,
                            "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            _orders = _svc.GetAll(_dtpFrom.Value.Date, _dtpTo.Value.Date, statusID, whID)

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
        Catch ex As Exception
            MessageBox.Show("خطأ في التحميل: " & ex.Message & vbCrLf & ex.StackTrace,
                            "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetSelectedOrder() As WithdrawalOrderDto
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
    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles _btnRefresh.Click
        LoadData()
    End Sub

    Private Sub CboFilter_Changed(sender As Object, e As EventArgs) Handles _cboStatus.SelectedIndexChanged,
                                                                       _cboWarehouse.SelectedIndexChanged
        If IsHandleCreated Then LoadData()
    End Sub

    Private Sub TxtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles _txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then LoadData()
    End Sub

    Private Sub Grid_DoubleClick(sender As Object, e As EventArgs) Handles _grid.DoubleClick
        OnDetails(Nothing, Nothing)
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles _btnNew.Click
        Using frm As New FrmWithdrawalOrderEdit(Nothing)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles _btnEdit.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If o.StatusID <> 1 Then
            MessageBox.Show("يمكن تعديل المسودات فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Using frm As New FrmWithdrawalOrderEdit(o)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles _btnSubmit.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If o.StatusID <> 1 Then
            MessageBox.Show("يمكن إرسال المسودات فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If MessageBox.Show($"إرسال الطلب {o.WithdrawalNo} للمخزن؟", "تأكيد",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            _svc.Submit(o.WithdrawalID, CurrentUserID)
            MessageBox.Show("✅ تم الإرسال.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnIssue_Click(sender As Object, e As EventArgs) Handles _btnIssue.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If o.StatusID <> 2 Then
            MessageBox.Show("يمكن صرف الطلبات المُرسلة فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim full = _svc.GetById(o.WithdrawalID)
        If full Is Nothing Then Return
        Using frm As New FrmWithdrawalOrderIssue(full)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnReceive_Click(sender As Object, e As EventArgs) Handles _btnReceive.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If o.StatusID <> 3 Then
            MessageBox.Show("يمكن استلام الطلبات المصروفة فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If MessageBox.Show($"استلام الطلب {o.WithdrawalNo}؟", "تأكيد",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            _svc.Receive(o.WithdrawalID, CurrentUserID)
            MessageBox.Show("✅ تم الاستلام.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnApprove_Click(sender As Object, e As EventArgs) Handles _btnApprove.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If o.StatusID <> 4 Then
            MessageBox.Show("يمكن اعتماد الطلبات المستلمة فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If MessageBox.Show($"اعتماد الطلب {o.WithdrawalNo}؟", "تأكيد",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            _svc.Approve(o.WithdrawalID, CurrentUserID)
            MessageBox.Show("✅ تم الاعتماد.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnReject_Click(sender As Object, e As EventArgs) Handles _btnReject.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If o.StatusID <> 2 Then
            MessageBox.Show("يمكن رفض الطلبات المُرسلة فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim reason = InputBox("سبب الرفض:", "رفض الطلب")
        If String.IsNullOrWhiteSpace(reason) Then Return
        Try
            _svc.Reject(o.WithdrawalID, reason, CurrentUserID)
            MessageBox.Show("✅ تم الرفض.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles _btnCancel.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If Not o.CanCancel Then
            MessageBox.Show("لا يمكن إلغاء هذا الطلب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim reason = InputBox("سبب الإلغاء:", "إلغاء الطلب")
        If String.IsNullOrWhiteSpace(reason) Then Return
        Try
            _svc.Cancel(o.WithdrawalID, reason, CurrentUserID)
            MessageBox.Show("✅ تم الإلغاء.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles _btnDelete.Click
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        If o.StatusID <> 1 Then
            MessageBox.Show("يمكن حذف المسودات فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If MessageBox.Show($"حذف الطلب {o.WithdrawalNo}؟", "تأكيد",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            _svc.Delete(o.WithdrawalID, CurrentUserID)
            MessageBox.Show("✅ تم الحذف.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDetails_Click(sender As Object, e As EventArgs) Handles _btnDetails.Click
        OnDetails(sender, e)
    End Sub

    Private Sub OnDetails(sender As Object, e As EventArgs)
        Dim o = GetSelectedOrder() : If o Is Nothing Then Return
        Dim full = _svc.GetById(o.WithdrawalID)
        If full Is Nothing Then Return
        Using frm As New FrmWithdrawalOrderDetails(full)
            frm.ShowDialog(Me)
        End Using
    End Sub

End Class