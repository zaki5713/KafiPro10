Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class ProStockBalanceUC

    Private _loading As Boolean = False
    Private _allData As DataTable

    Private Sub ProStockBalanceUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGrid()
        LoadWarehouses()
        LoadData()
        UITheme.ApplyGridTheme(dgvStock)
    End Sub

    Private Sub InitializeGrid()
        With dgvStock
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseCode", .HeaderText = "كود المخزن", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseName", .HeaderText = "المخزن", .FillWeight = 13})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemCode", .HeaderText = "كود الصنف", .FillWeight = 13})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemName", .HeaderText = "الصنف", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitName", .HeaderText = "الوحدة", .FillWeight = 9})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "OnHand", .HeaderText = "الرصيد", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Reserved", .HeaderText = "محجوز", .FillWeight = 8})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Available", .HeaderText = "المتاح", .FillWeight = 12})
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

    Private Sub LoadData()
        Try
            _loading = True
            Cursor = Cursors.WaitCursor

            Dim whId As Integer? = Nothing
            If cboWarehouse.SelectedValue IsNot Nothing Then
                Dim v As Integer = CInt(cboWarehouse.SelectedValue)
                If v > 0 Then whId = v
            End If

            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Inventory].[usp_StockBalance_GetAll]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@WarehouseID", If(whId.HasValue, CObj(whId.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ItemID", DBNull.Value)

                    Dim dt As New DataTable()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using

                    _allData = dt
                    ApplyFilter()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الأرصدة: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            _loading = False
        End Try
    End Sub

    Private Sub ApplyFilter()
        If _allData Is Nothing Then Return

        Dim search As String = txtSearch.Text.Trim().ToLower()
        dgvStock.Rows.Clear()

        For Each row As DataRow In _allData.Rows
            Dim code As String = row("ItemCode").ToString().ToLower()
            Dim nameAr As String = row("ItemNameAr").ToString().ToLower()
            Dim nameEn As String = If(row("ItemNameEn") Is DBNull.Value, "", row("ItemNameEn").ToString().ToLower())

            If search = "" OrElse code.Contains(search) OrElse nameAr.Contains(search) OrElse nameEn.Contains(search) Then
                dgvStock.Rows.Add(
                    row("WarehouseCode").ToString(),
                    row("WarehouseNameAr").ToString(),
                    row("ItemCode").ToString(),
                    row("ItemNameAr").ToString(),
                    row("UnitName").ToString(),
                    CDec(row("OnHandQty")).ToString("N3"),
                    CDec(row("ReservedQty")).ToString("N3"),
                    CDec(row("AvailableQty")).ToString("N3")
                )
            End If
        Next

        lblCount.Text = $"📋  الأرصدة: {dgvStock.Rows.Count}"
    End Sub

    Private Sub cboWarehouse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWarehouse.SelectedIndexChanged
        If _loading Then Return
        LoadData()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplyFilter()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtSearch.Clear()
        If cboWarehouse.Items.Count > 0 Then cboWarehouse.SelectedIndex = 0
        ApplyFilter()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadData()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _loading Then Return
        ApplyFilter()
    End Sub

End Class