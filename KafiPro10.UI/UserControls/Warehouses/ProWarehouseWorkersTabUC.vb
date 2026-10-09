Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports KafiPro.UI.Shared.Themes

Public Class ProWarehouseWorkersTabUC

    Public Property CurrentUserID As Integer = 1

    Private ReadOnly _svc As New WarehouseMasterService()

    Private WithEvents toolbar As New Panel()
    Private WithEvents btnAdd As New Button()
    Private WithEvents btnEdit As New Button()
    Private WithEvents btnDelete As New Button()
    Private WithEvents btnRefresh As New Button()
    Private WithEvents cboRoleFilter As New ComboBox()
    Private WithEvents chkOnlyActive As New CheckBox()
    Private WithEvents txtSearch As New TextBox()

    Private WithEvents grid As New DataGridView()
    Private _workers As List(Of WorkerDto)

    Private Sub ProWarehouseWorkersTabUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadData()
    End Sub

    Private Sub BuildUI()
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes

        ' ─── Toolbar ───
        toolbar.Dock = DockStyle.Top
        toolbar.Height = 60
        toolbar.BackColor = Color.FromArgb(245, 247, 250)
        toolbar.Padding = New Padding(8)

        btnAdd.Text = "➕ إضافة"
        btnAdd.Location = New Point(12, 14)
        btnAdd.Size = New Size(100, 32)
        StyleButton(btnAdd, Color.FromArgb(46, 204, 113))

        btnEdit.Text = "✏️ تعديل"
        btnEdit.Location = New Point(120, 14)
        btnEdit.Size = New Size(100, 32)
        StyleButton(btnEdit, Color.FromArgb(52, 152, 219))

        btnDelete.Text = "🗑️ حذف"
        btnDelete.Location = New Point(228, 14)
        btnDelete.Size = New Size(100, 32)
        StyleButton(btnDelete, Color.FromArgb(231, 76, 60))

        btnRefresh.Text = "🔄"
        btnRefresh.Location = New Point(336, 14)
        btnRefresh.Size = New Size(50, 32)
        StyleButton(btnRefresh, Color.FromArgb(149, 165, 166))

        ' فلتر الوظيفة
        Dim lblRole As New Label With {.Text = "الوظيفة:", .Location = New Point(700, 20), .AutoSize = True,
                                       .Font = New Font("Segoe UI", 9, FontStyle.Bold)}
        cboRoleFilter.Location = New Point(520, 17)
        cboRoleFilter.Size = New Size(170, 28)
        cboRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList

        ' بحث
        Dim lblSearch As New Label With {.Text = "بحث:", .Location = New Point(480, 20), .AutoSize = True,
                                         .Font = New Font("Segoe UI", 9, FontStyle.Bold)}
        txtSearch.Location = New Point(400, 17)
        txtSearch.Size = New Size(75, 28)

        chkOnlyActive.Text = "النشط فقط"
        chkOnlyActive.Location = New Point(800, 20)
        chkOnlyActive.AutoSize = True
        chkOnlyActive.Font = New Font("Segoe UI", 9)

        toolbar.Controls.AddRange({btnAdd, btnEdit, btnDelete, btnRefresh, lblRole, cboRoleFilter,
                                    lblSearch, txtSearch, chkOnlyActive})

        ' ─── Grid ───
        grid.Dock = DockStyle.Fill
        grid.BackgroundColor = Color.White
        grid.BorderStyle = BorderStyle.None
        grid.RightToLeft = RightToLeft.Yes
        grid.AllowUserToAddRows = False
        grid.ReadOnly = True
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.ColumnHeadersHeight = 40
        grid.RowTemplate.Height = 30
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = UITheme.Primary
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250)

        Me.Controls.Add(grid)
        Me.Controls.Add(toolbar)

        ' ─── Fill Role Filter ───
        cboRoleFilter.Items.Clear()
        cboRoleFilter.Items.Add(New ComboItem("كل الوظائف", ""))
        cboRoleFilter.Items.Add(New ComboItem("عامل", "WORKER"))
        cboRoleFilter.Items.Add(New ComboItem("أمين مخزن", "STOREKEEPER"))
        cboRoleFilter.Items.Add(New ComboItem("سائق كلارك", "FORKLIFT_DRIVER"))
        cboRoleFilter.Items.Add(New ComboItem("مدير مخزن", "MANAGER"))
        cboRoleFilter.SelectedIndex = 0
    End Sub

    Private Sub StyleButton(b As Button, color As Color)
        b.BackColor = color
        b.ForeColor = Color.White
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        b.Cursor = Cursors.Hand
    End Sub

    Public Sub LoadData()
        Try
            _workers = _svc.GetWorkers(chkOnlyActive.Checked)

            ' فلترة بالوظيفة
            Dim roleFilter = ""
            If cboRoleFilter.SelectedItem IsNot Nothing Then
                roleFilter = DirectCast(cboRoleFilter.SelectedItem, ComboItem).Value
            End If

            If Not String.IsNullOrEmpty(roleFilter) Then
                _workers = _workers.FindAll(Function(w) w.WarehouseRole = roleFilter)
            End If

            ' فلترة بالبحث
            Dim search = txtSearch.Text.Trim()
            If Not String.IsNullOrEmpty(search) Then
                _workers = _workers.FindAll(Function(w)
                                                Return w.WorkerCode.Contains(search) OrElse
                                                       w.WorkerNameAr.Contains(search) OrElse
                                                       (w.PhoneNumber IsNot Nothing AndAlso w.PhoneNumber.Contains(search))
                                            End Function)
            End If

            ' بناء DataTable
            Dim dt As New DataTable()
            dt.Columns.Add("WorkerID", GetType(Integer))
            dt.Columns.Add("الكود", GetType(String))
            dt.Columns.Add("الاسم", GetType(String))
            dt.Columns.Add("الوظيفة", GetType(String))
            dt.Columns.Add("التليفون", GetType(String))
            dt.Columns.Add("المخزن الافتراضي", GetType(String))
            dt.Columns.Add("تاريخ التعيين", GetType(String))
            dt.Columns.Add("نشط", GetType(String))

            For Each w In _workers
                dt.Rows.Add(w.WorkerID, w.WorkerCode, w.WorkerNameAr,
                            w.WarehouseRoleAr, w.PhoneNumber,
                            w.DefaultWarehouseName,
                            If(w.HireDate.HasValue, w.HireDate.Value.ToString("yyyy-MM-dd"), ""),
                            If(w.IsActive, "✔", "✘"))
            Next

            grid.DataSource = dt
            If grid.Columns.Contains("WorkerID") Then grid.Columns("WorkerID").Visible = False

        Catch ex As Exception
            MessageBox.Show("خطأ في التحميل: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

    Private Sub Filters_Changed(sender As Object, e As EventArgs) Handles chkOnlyActive.CheckedChanged,
                                                                       cboRoleFilter.SelectedIndexChanged
        If IsHandleCreated Then LoadData()
    End Sub

    Private Sub TxtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then LoadData()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Using frm As New FrmWorkerEdit(Nothing, _svc)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim w = GetSelected()
        If w Is Nothing Then Return

        Using frm As New FrmWorkerEdit(w, _svc)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim w = GetSelected()
        If w Is Nothing Then Return

        If MessageBox.Show($"هل أنت متأكد من حذف: {w.WorkerNameAr}؟",
                           "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            _svc.DeleteWorker(w.WorkerID, CurrentUserID)
            MessageBox.Show("✅ تم الحذف بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetSelected() As WorkerDto
        If grid.CurrentRow Is Nothing Then
            MessageBox.Show("اختر عاملاً أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
        Dim id = CInt(grid.CurrentRow.Cells("WorkerID").Value)
        Return _workers.Find(Function(x) x.WorkerID = id)
    End Function

End Class