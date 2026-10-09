Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Data
Imports KafiPro.UI.Shared.Themes

Public Class ProBranchesTabUC

    Public Property CurrentUserID As Integer = 1

    Private ReadOnly _svc As New WarehouseMasterService()

    Private WithEvents toolbar As New Panel()
    Private WithEvents btnAdd As New Button()
    Private WithEvents btnEdit As New Button()
    Private WithEvents btnDelete As New Button()
    Private WithEvents btnRefresh As New Button()

    Private WithEvents grid As New DataGridView()
    Private _branches As List(Of BranchDto)

    Private Sub ProBranchesTabUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadData()
    End Sub

    Private Sub BuildUI()
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        toolbar.Dock = DockStyle.Top
        toolbar.Height = 60
        toolbar.BackColor = Color.FromArgb(245, 247, 250)
        toolbar.Padding = New Padding(8)

        btnAdd.Text = "➕ إضافة فرع"
        btnAdd.Location = New Point(12, 14)
        btnAdd.Size = New Size(120, 32)
        StyleButton(btnAdd, Color.FromArgb(46, 204, 113))

        btnEdit.Text = "✏️ تعديل"
        btnEdit.Location = New Point(140, 14)
        btnEdit.Size = New Size(100, 32)
        StyleButton(btnEdit, Color.FromArgb(52, 152, 219))

        btnDelete.Text = "🗑️ حذف"
        btnDelete.Location = New Point(248, 14)
        btnDelete.Size = New Size(100, 32)
        StyleButton(btnDelete, Color.FromArgb(231, 76, 60))

        btnRefresh.Text = "🔄"
        btnRefresh.Location = New Point(356, 14)
        btnRefresh.Size = New Size(50, 32)
        StyleButton(btnRefresh, Color.FromArgb(149, 165, 166))

        toolbar.Controls.AddRange({btnAdd, btnEdit, btnDelete, btnRefresh})

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
            _branches = _svc.GetBranches()

            Dim dt As New DataTable()
            dt.Columns.Add("BranchID", GetType(Integer))
            dt.Columns.Add("الشركة", GetType(String))
            dt.Columns.Add("الكود", GetType(String))
            dt.Columns.Add("الاسم (عربي)", GetType(String))
            dt.Columns.Add("الاسم (إنجليزي)", GetType(String))
            dt.Columns.Add("نشط", GetType(String))

            For Each b In _branches
                dt.Rows.Add(b.BranchID, b.CompanyName, b.BranchCode,
                            b.BranchNameAr, b.BranchNameEn,
                            If(b.IsActive, "✔", "✘"))
            Next

            grid.DataSource = dt
            If grid.Columns.Contains("BranchID") Then grid.Columns("BranchID").Visible = False
        Catch ex As Exception
            MessageBox.Show("خطأ في التحميل: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Using frm As New FrmBranchEdit(Nothing, _svc)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim br = GetSelected()
        If br Is Nothing Then Return

        Using frm As New FrmBranchEdit(br, _svc)
            If frm.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim br = GetSelected()
        If br Is Nothing Then Return

        If MessageBox.Show($"هل أنت متأكد من حذف الفرع: {br.BranchNameAr}؟",
                           "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            _svc.DeleteBranch(br.BranchID, CurrentUserID)
            MessageBox.Show("✅ تم الحذف بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetSelected() As BranchDto
        If grid.CurrentRow Is Nothing Then
            MessageBox.Show("اختر فرعاً أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
        Dim id = CInt(grid.CurrentRow.Cells("BranchID").Value)
        Return _branches.Find(Function(x) x.BranchID = id)
    End Function

End Class