Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class ProMovementUC

    Private _loading As Boolean = False

    Private Sub ProMovementUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGrid()
        dtpFrom.Value = Date.Today.AddMonths(-1)
        dtpTo.Value = Date.Today
        LoadWarehouses()
        LoadTypes()
        LoadData()
        UITheme.ApplyGridTheme(dgvMovements)
    End Sub

    Private Sub InitializeGrid()
        With dgvMovements
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MovementID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MovementDate", .HeaderText = "التاريخ", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TypeName", .HeaderText = "نوع الحركة", .FillWeight = 14})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Direction", .HeaderText = "الاتجاه", .FillWeight = 6})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseName", .HeaderText = "المخزن", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemCode", .HeaderText = "كود الصنف", .FillWeight = 11})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemName", .HeaderText = "الصنف", .FillWeight = 20})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Qty", .HeaderText = "الكمية", .FillWeight = 9})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RefNo", .HeaderText = "المستند المرجعي", .FillWeight = 16})
        End With
    End Sub

    Private Sub LoadWarehouses()
        Try
            Dim list As New List(Of KeyValuePair(Of Integer, String))()
            list.Add(New KeyValuePair(Of Integer, String)(0, "— كل المخازن —"))

            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Core].[usp_Warehouse_GetAll]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@OnlyActive", 1)
                    Using r As SqlDataReader = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New KeyValuePair(Of Integer, String)(r.GetInt32(0), r.GetString(2)))
                        End While
                    End Using
                End Using
            End Using

            cboWarehouse.DataSource = Nothing
            cboWarehouse.DisplayMember = "Value"
            cboWarehouse.ValueMember = "Key"
            cboWarehouse.DataSource = list
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل المخازن: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadTypes()
        Try
            Dim list As New List(Of KeyValuePair(Of Integer, String))()
            list.Add(New KeyValuePair(Of Integer, String)(0, "— كل الأنواع —"))

            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Inventory].[usp_MovementType_GetAll]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    Using r As SqlDataReader = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New KeyValuePair(Of Integer, String)(r.GetInt32(0), r.GetString(2)))
                        End While
                    End Using
                End Using
            End Using

            cboType.DataSource = Nothing
            cboType.DisplayMember = "Value"
            cboType.ValueMember = "Key"
            cboType.DataSource = list
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الأنواع: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            _loading = True
            Cursor = Cursors.WaitCursor

            Dim whId As Integer? = Nothing
            If cboWarehouse.SelectedValue IsNot Nothing Then
                Dim v As Integer = CInt(cboWarehouse.SelectedValue)
                If v > 0 Then whId = v
            End If

            Dim typeId As Integer? = Nothing
            If cboType.SelectedValue IsNot Nothing Then
                Dim v As Integer = CInt(cboType.SelectedValue)
                If v > 0 Then typeId = v
            End If

            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Inventory].[usp_Movement_GetAll]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@FromDate", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@ToDate", dtpTo.Value.Date)
                    cmd.Parameters.AddWithValue("@WarehouseID", If(whId.HasValue, CObj(whId.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ItemID", DBNull.Value)
                    cmd.Parameters.AddWithValue("@MovementTypeID", If(typeId.HasValue, CObj(typeId.Value), CObj(DBNull.Value)))

                    Using r As SqlDataReader = cmd.ExecuteReader()
                        dgvMovements.Rows.Clear()
                        While r.Read()
                            Dim dir As String = r("Direction").ToString()
                            Dim dirAr As String = If(dir = "+", "وارد", If(dir = "-", "صادر", "تحويل"))

                            dgvMovements.Rows.Add(
                                r("MovementID"),
                                CDate(r("MovementDate")).ToString("yyyy-MM-dd HH:mm"),
                                r("MovementTypeNameAr").ToString(),
                                dirAr,
                                r("WarehouseNameAr").ToString(),
                                r("ItemCode").ToString(),
                                r("ItemNameAr").ToString(),
                                CDec(r("Qty")).ToString("N3"),
                                If(r("ReferenceNo") Is DBNull.Value, "-", r("ReferenceNo").ToString())
                            )
                        End While
                    End Using
                End Using
            End Using

            lblCount.Text = $"📋  الحركات: {dgvMovements.Rows.Count}"
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الحركات: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            _loading = False
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        dtpFrom.Value = Date.Today.AddMonths(-1)
        dtpTo.Value = Date.Today
        If cboWarehouse.Items.Count > 0 Then cboWarehouse.SelectedIndex = 0
        If cboType.Items.Count > 0 Then cboType.SelectedIndex = 0
        LoadData()
    End Sub

End Class