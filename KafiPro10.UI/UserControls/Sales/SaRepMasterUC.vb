Imports System.ComponentModel
Imports System.Linq
Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro10.Solution.KafiPro.Sales.Application.DTOs
Imports KafiPro10.Solution.KafiPro10.Solution.KafiPro.Sales.Infrastructure.Repositories

Public Class SaRepMasterUC
    Inherits BaseUC

    Private _repo As SalesRepRepository
    Private _branches As List(Of BranchDto)
    Private _routes As List(Of RouteDto)
    Private _editing As SalesRepDto

    ' ⚠️ عدّل هذا حسب مصدر الـ UserID في مشروعك
    Private ReadOnly CurrentUserID As Integer = 1

#Region "Load"
    Private Sub SaRepMasterUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.HeaderTitle = "المناديب"
        _repo = New SalesRepRepository()
        LoadLookups()
        BindGrid()
    End Sub

    Private Sub LoadLookups()
        _branches = _repo.GetBranches()
        _routes = _repo.GetRoutes()

        cboBranch.DataSource = _branches
        cboBranch.DisplayMember = "BranchName"
        cboBranch.ValueMember = "BranchID"

        cboRoute.DataSource = _routes
        cboRoute.DisplayMember = "RouteName"
        cboRoute.ValueMember = "RouteID"
    End Sub

    Private Sub BindGrid()
        dgv.DataSource = New BindingList(Of SalesRepDto)(_repo.GetAll())
        ConfigureGrid()
    End Sub

    Private Sub ConfigureGrid()
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False

        If dgv.Columns.Contains("RepID") Then dgv.Columns("RepID").Visible = False
        If dgv.Columns.Contains("BranchID") Then dgv.Columns("BranchID").Visible = False
        If dgv.Columns.Contains("RouteID") Then dgv.Columns("RouteID").Visible = False
        If dgv.Columns.Contains("RepCode") Then dgv.Columns("RepCode").HeaderText = "الكود"
        If dgv.Columns.Contains("RepName") Then dgv.Columns("RepName").HeaderText = "اسم المندوب"
        If dgv.Columns.Contains("Phone") Then dgv.Columns("Phone").HeaderText = "الهاتف"
        If dgv.Columns.Contains("BranchName") Then dgv.Columns("BranchName").HeaderText = "الفرع"
        If dgv.Columns.Contains("RouteName") Then dgv.Columns("RouteName").HeaderText = "خط السير"
        If dgv.Columns.Contains("CommissionRate") Then dgv.Columns("CommissionRate").HeaderText = "نسبة العمولة %"
        If dgv.Columns.Contains("MonthlyTarget") Then dgv.Columns("MonthlyTarget").HeaderText = "التارجت الشهري"
        If dgv.Columns.Contains("IsActive") Then dgv.Columns("IsActive").HeaderText = "نشط"
    End Sub
#End Region

#Region "Toolbar Events"
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        _editing = New SalesRepDto With {.IsActive = True}
        BindEditor(_editing)
        pnlEditor.Visible = True
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgv.CurrentRow Is Nothing Then Return
        _editing = CType(dgv.CurrentRow.DataBoundItem, SalesRepDto)
        BindEditor(_editing)
        pnlEditor.Visible = True
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgv.CurrentRow Is Nothing Then Return
        Dim rep = CType(dgv.CurrentRow.DataBoundItem, SalesRepDto)
        If MessageBox.Show($"هل تريد حذف المندوب [{rep.RepName}]؟", "تأكيد",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            _repo.Delete(rep.RepID, CurrentUserID)
            BindGrid()
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateEditor() Then Return
        ReadEditor(_editing)

        If _editing.RepID = 0 Then
            _repo.Insert(_editing, CurrentUserID)
        Else
            _repo.Update(_editing, CurrentUserID)
        End If

        pnlEditor.Visible = False
        BindGrid()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        pnlEditor.Visible = False
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        BindGrid()
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim term = txtSearch.Text.Trim()
        If term = "" Then BindGrid() : Return

        Dim all = _repo.GetAll()
        Dim filtered = all.Where(Function(r) _
            (r.RepName IsNot Nothing AndAlso r.RepName.Contains(term)) OrElse
            (r.RepCode IsNot Nothing AndAlso r.RepCode.Contains(term))).ToList()

        dgv.DataSource = New BindingList(Of SalesRepDto)(filtered)
        ConfigureGrid()
    End Sub
#End Region

#Region "Editor Binding"
    Private Sub BindEditor(rep As SalesRepDto)
        txtCode.Text = rep.RepCode
        txtName.Text = rep.RepName
        txtPhone.Text = rep.Phone
        If rep.BranchID > 0 Then cboBranch.SelectedValue = rep.BranchID
        If rep.RouteID > 0 Then cboRoute.SelectedValue = rep.RouteID
        nudCommission.Value = rep.CommissionRate
        nudTarget.Value = rep.MonthlyTarget
        chkActive.Checked = rep.IsActive
    End Sub

    Private Sub ReadEditor(rep As SalesRepDto)
        rep.RepCode = txtCode.Text.Trim()
        rep.RepName = txtName.Text.Trim()
        rep.Phone = txtPhone.Text.Trim()
        rep.BranchID = If(cboBranch.SelectedValue Is Nothing, 0, CInt(cboBranch.SelectedValue))
        rep.RouteID = If(cboRoute.SelectedValue Is Nothing, 0, CInt(cboRoute.SelectedValue))
        rep.CommissionRate = nudCommission.Value
        rep.MonthlyTarget = nudTarget.Value
        rep.IsActive = chkActive.Checked
    End Sub

    Private Function ValidateEditor() As Boolean
        If txtCode.Text.Trim() = "" Then
            MessageBox.Show("كود المندوب مطلوب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus() : Return False
        End If
        If txtName.Text.Trim() = "" Then
            MessageBox.Show("اسم المندوب مطلوب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus() : Return False
        End If
        Return True
    End Function
#End Region
End Class