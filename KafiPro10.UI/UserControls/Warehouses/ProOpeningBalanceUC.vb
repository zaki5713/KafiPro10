Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro       ' ← Namespace بس

Partial Public Class ProOpeningBalanceUC
    Inherits System.Windows.Forms.UserControl
    Private Const COL_ITEMID As String = "ItemID"
    Private Const COL_ITEMCODE As String = "ItemCode"
    Private Const COL_ITEMNAME As String = "ItemName"
    Private Const COL_UNIT As String = "UnitName"
    Private Const COL_QTY As String = "OpeningQty"
    Private Const COL_ONHAND As String = "CurrentOnHand"

    Private _dt As DataTable

    Private Sub ProOpeningBalanceUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpOpenDate.Value = Date.Today
        LoadWarehouses()
        BuildGrid()
    End Sub

    Private Sub LoadWarehouses()
        Using cn = KafiPro.Infrastructure.Persistence.DBConnection.GetConnection()   ' ← GetConnection مش Create
            Using cmd As New SqlCommand("Core.usp_Warehouse_GetAll", cn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim dt As New DataTable()
                dt.Load(cmd.ExecuteReader())
                cboWarehouse.DataSource = dt
                cboWarehouse.DisplayMember = "WarehouseName"
                cboWarehouse.ValueMember = "WarehouseID"
            End Using
        End Using
    End Sub

    Private Sub BuildGrid()
        dgvItems.AutoGenerateColumns = False
        dgvItems.Columns.Clear()

        dgvItems.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = COL_ITEMID, .DataPropertyName = COL_ITEMID, .Visible = False})

        dgvItems.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = COL_ITEMCODE, .HeaderText = "الكود",
            .DataPropertyName = COL_ITEMCODE, .ReadOnly = True, .FillWeight = 70})

        dgvItems.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = COL_ITEMNAME, .HeaderText = "الصنف",
            .DataPropertyName = COL_ITEMNAME, .ReadOnly = True, .FillWeight = 220})

        dgvItems.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = COL_UNIT, .HeaderText = "الوحدة",
            .DataPropertyName = COL_UNIT, .ReadOnly = True, .FillWeight = 60})

        dgvItems.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = COL_QTY, .HeaderText = "الكمية الافتتاحية",
            .DataPropertyName = COL_QTY,
            .DefaultCellStyle = New DataGridViewCellStyle With {
                .Format = "N2", .BackColor = Color.LightYellow}})

        dgvItems.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = COL_ONHAND, .HeaderText = "الرصيد الحالي",
            .DataPropertyName = COL_ONHAND, .ReadOnly = True,
            .DefaultCellStyle = New DataGridViewCellStyle With {
                .Format = "N2", .ForeColor = Color.DarkSlateBlue}})
    End Sub

    Private Sub BtnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If cboWarehouse.SelectedValue Is Nothing Then Return

        Using cn = KafiPro.Infrastructure.Persistence.DBConnection.GetConnection()
            Using cmd As New SqlCommand("Inventory.usp_OpeningBalance_GetItems", cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WarehouseID", CInt(cboWarehouse.SelectedValue))
                _dt = New DataTable()
                _dt.Load(cmd.ExecuteReader())
                dgvItems.DataSource = _dt
            End Using
        End Using

        RecalcTotals()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If _dt Is Nothing Then Return
        For Each r As DataRow In _dt.Rows
            r(COL_QTY) = 0D
        Next
        RecalcTotals()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If _dt Is Nothing OrElse _dt.Rows.Count = 0 Then
            MessageBox.Show("حمّل الأصناف أولاً.", "تنبيه")
            Return
        End If

        For i As Integer = 0 To _dt.Rows.Count - 1
            Dim r = _dt.Rows(i)
            Dim q As Decimal = If(IsDBNull(r(COL_QTY)), 0D, CDec(r(COL_QTY)))
            If q < 0 Then
                MessageBox.Show($"السطر {i + 1}: الكمية لا يمكن أن تكون سالبة.",
                                "تحقق", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        If MessageBox.Show("سيتم استبدال أي رصيد افتتاحي سابق لهذا المخزن. متابعة؟",
                           "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Dim tvp As New DataTable()
        tvp.Columns.Add("ItemID", GetType(Integer))
        tvp.Columns.Add("Qty", GetType(Decimal))

        For Each r As DataRow In _dt.Rows
            Dim q As Decimal = If(IsDBNull(r(COL_QTY)), 0D, CDec(r(COL_QTY)))
            If q > 0 Then
                tvp.Rows.Add(CInt(r(COL_ITEMID)), q)
            End If
        Next

        If tvp.Rows.Count = 0 Then
            MessageBox.Show("لا توجد كميات موجبة للحفظ.", "تنبيه")
            Return
        End If

        Try
            Dim refNo As String
            Using cn = KafiPro.Infrastructure.Persistence.DBConnection.GetConnection()
                Using cmd As New SqlCommand("Inventory.usp_OpeningBalance_Save", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@WarehouseID", CInt(cboWarehouse.SelectedValue))
                    cmd.Parameters.AddWithValue("@OpenDate", dtpOpenDate.Value.Date)
                    cmd.Parameters.AddWithValue("@CreatedBy", 1)

                    Dim p = cmd.Parameters.AddWithValue("@Details", tvp)
                    p.SqlDbType = SqlDbType.Structured
                    p.TypeName = "Inventory.OpeningBalanceType"

                    refNo = CStr(cmd.ExecuteScalar())
                End Using
            End Using

            MessageBox.Show($"تم حفظ الأرصدة الافتتاحية. المرجع: {refNo}",
                            "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnLoad.PerformClick()
        Catch ex As Exception
            MessageBox.Show("فشل الحفظ: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvItems_CellEndEdit(sender As Object, e As EventArgs) _
        Handles dgvItems.CellEndEdit
        RecalcTotals()
    End Sub

    Private Sub RecalcTotals()
        If _dt Is Nothing Then Return
        Dim cnt As Integer = 0
        Dim totalQty As Decimal = 0
        For Each r As DataRow In _dt.Rows
            Dim q As Decimal = If(IsDBNull(r(COL_QTY)), 0D, CDec(r(COL_QTY)))
            If q > 0 Then
                cnt += 1
                totalQty += q
            End If
        Next
        lblTotal.Text = $"أصناف لها رصيد: {cnt}  |  إجمالي الكميات: {totalQty:N2}"
    End Sub

End Class