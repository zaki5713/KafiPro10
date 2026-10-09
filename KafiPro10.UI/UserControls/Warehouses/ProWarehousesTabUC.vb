Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Data
Imports KafiPro.UI.Shared.Themes

Public Class ProWarehousesTabUC

    Public Property CurrentUserID As Integer = 1

    Private ReadOnly _svc As New WarehouseMasterService()

    Private WithEvents toolbar As New Panel()
    Private WithEvents btnAdd As New Button()
    Private WithEvents btnEdit As New Button()
    Private WithEvents btnDelete As New Button()
    Private WithEvents btnRefresh As New Button()
    Private WithEvents chkOnlyActive As New CheckBox()

    Private WithEvents grid As New DataGridView()
    Private _warehouses As List(Of WarehouseDto)

    Private Sub ProWarehousesTabUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadData()
    End Sub

    Private Sub BuildUI()
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        ' ─── Toolbar ───
        toolbar.Dock = DockStyle.Top
        toolbar.Height = 60
        toolbar.BackColor = Color.FromArgb(245, 247, 250)
        toolbar.Padding = New Padding(8)

        btnAdd.Text = "➕ إضافة مخزن"
        btnAdd.Location = New Point(12, 14)
        btnAdd.Size = New Size(130, 32)
        StyleButton(btnAdd, Color.FromArgb(46, 204, 113))

        btnEdit.Text = "✏️ تعديل"
        btnEdit.Location = New Point(150, 14)
        btnEdit.Size = New Size(100, 32)
        StyleButton(btnEdit, Color.FromArgb(52, 152, 219))

        btnDelete.Text = "🗑️ حذف"
        btnDelete.Location = New Point(258, 14)
        btnDelete.Size = New Size(100, 32)
        StyleButton(btnDelete, Color.FromArgb(231, 76, 60))

        btnRefresh.Text = "🔄"
        btnRefresh.Location = New Point(366, 14)
        btnRefresh.Size = New Size(50, 32)
        StyleButton(btnRefresh, Color.FromArgb(149, 165, 166))

        chkOnlyActive.Text = "النشط فقط"
        chkOnlyActive.Location = New Point(440, 20)
        chkOnlyActive.AutoSize = True
        chkOnlyActive.Font = New Font("Segoe UI", 9)

        toolbar.Controls.AddRange({btnAdd, btnEdit, btnDelete, btnRefresh, chkOnlyActive})

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
            _warehouses = _svc.GetWarehouses(chkOnlyActive.Checked)

            Dim dt As New DataTable()
            dt.Columns.Add("WarehouseID", GetType(Integer))
            dt.Columns.Add("الكود", GetType(String))
            dt.Columns.Add("الاسم (عربي)", GetType(String))
            dt.Columns.Add("الاسم (إنجليزي)", GetType(String))
            dt.Columns.Add("النوع", GetType(String))
            dt.Columns.Add("افتراضي", GetType(String))
            dt.Columns.Add("نشط", GetType(String))

            For Each w In _warehouses
                dt.Rows.Add(w.WarehouseID, w.WarehouseCode, w.WarehouseNameAr,
                            w.WarehouseNameEn, w.WarehouseTypeName,
                            If(w.IsDefault, "✔", ""), If(w.IsActive, "✔", "✘"))
            Next

            grid.DataSource = dt
            If grid.Columns.Contains("WarehouseID") Then grid.Columns("WarehouseID").Visible = False
        Catch ex As Exception
            MessageBox.Show("خطأ في التحميل: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click, chkOnlyActive.CheckedChanged
        LoadData()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Using frm As New FrmWarehouseEdit(Nothing, _svc)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim wh = GetSelected()
        If wh Is Nothing Then Return

        Using frm As New FrmWarehouseEdit(wh, _svc)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim wh = GetSelected()
        If wh Is Nothing Then Return

        If MessageBox.Show($"هل أنت متأكد من حذف المخزن: {wh.WarehouseNameAr}؟",
                           "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            _svc.DeleteWarehouse(wh.WarehouseID, CurrentUserID)
            MessageBox.Show("✅ تم الحذف بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetSelected() As WarehouseDto
        If grid.CurrentRow Is Nothing Then
            MessageBox.Show("اختر مخزناً أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
        Dim id = CInt(grid.CurrentRow.Cells("WarehouseID").Value)
        Return _warehouses.Find(Function(x) x.WarehouseID = id)
    End Function

End Class