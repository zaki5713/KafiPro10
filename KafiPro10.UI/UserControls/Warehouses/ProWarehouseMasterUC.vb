Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing
Imports KafiPro.UI.Shared.Themes
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class ProWarehouseMasterUC

    ' ═══════════════════════════════════════════════════════
    '  Model
    ' ═══════════════════════════════════════════════════════
    Private Class WarehouseDto
        Public Property WarehouseID As Integer
        Public Property WarehouseCode As String
        Public Property WarehouseNameAr As String
        Public Property WarehouseNameEn As String
        Public Property WarehouseTypeID As Integer
        Public Property WarehouseTypeNameAr As String
        Public Property IsDefault As Boolean
        Public Property IsActive As Boolean
    End Class

    ' ═══════════════════════════════════════════════════════
    '  State
    ' ═══════════════════════════════════════════════════════
    Private _currentId As Integer = 0
    Private _loading As Boolean = False

    ' ═══════════════════════════════════════════════════════
    '  Load
    ' ═══════════════════════════════════════════════════════
    Private Sub ProWarehouseMasterUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGrid()
        LoadTypes()
        LoadData()
        ResetForm()
        UITheme.ApplyGridTheme(dgvWarehouses)
    End Sub

    Private Sub InitializeGrid()
        With dgvWarehouses
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseCode", .HeaderText = "كود المخزن", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseNameAr", .HeaderText = "الاسم عربي", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseNameEn", .HeaderText = "الاسم إنجليزي", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WarehouseTypeNameAr", .HeaderText = "النوع", .FillWeight = 18})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "IsDefault", .HeaderText = "افتراضي", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "IsActive", .HeaderText = "نشط", .FillWeight = 10})
        End With
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Types (Lookup)
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadTypes()
        Try
            Dim types As New List(Of KeyValuePair(Of Integer, String))()
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Core].[usp_WarehouseType_GetAll]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    Using r As SqlDataReader = cmd.ExecuteReader()
                        While r.Read()
                            types.Add(New KeyValuePair(Of Integer, String)(
                                r.GetInt32(0), r.GetString(2)))
                        End While
                    End Using
                End Using
            End Using

            cboType.DataSource = Nothing
            cboType.DisplayMember = "Value"
            cboType.ValueMember = "Key"
            cboType.DataSource = types
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل أنواع المخازن: " & ex.Message,
                            "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Data
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadData(Optional ByVal selectId As Integer = 0)
        Try
            _loading = True
            Cursor = Cursors.WaitCursor

            Dim list As New List(Of WarehouseDto)()
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Core].[usp_Warehouse_GetAll]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@OnlyActive", 0)

                    Using r As SqlDataReader = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New WarehouseDto With {
                                .WarehouseID = r.GetInt32(0),
                                .WarehouseCode = r.GetString(1),
                                .WarehouseNameAr = r.GetString(2),
                                .WarehouseNameEn = If(r.IsDBNull(3), "", r.GetString(3)),
                                .WarehouseTypeID = r.GetInt32(4),
                                .WarehouseTypeNameAr = r.GetString(5),
                                .IsDefault = r.GetBoolean(6),
                                .IsActive = r.GetBoolean(7)
                            })
                        End While
                    End Using
                End Using
            End Using

            dgvWarehouses.Rows.Clear()
            For Each w In list
                dgvWarehouses.Rows.Add(
                    w.WarehouseID, w.WarehouseCode, w.WarehouseNameAr, w.WarehouseNameEn,
                    w.WarehouseTypeNameAr,
                    If(w.IsDefault, "✓", "✗"),
                    If(w.IsActive, "✓", "✗")
                )
            Next

            If selectId > 0 Then
                For Each row As DataGridViewRow In dgvWarehouses.Rows
                    If CInt(row.Cells("WarehouseID").Value) = selectId Then
                        row.Selected = True : Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل المخازن: " & ex.Message,
                            "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            _loading = False
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Into Form
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadIntoForm(id As Integer)
        Try
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Core].[usp_Warehouse_GetById]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@WarehouseID", id)

                    Using r As SqlDataReader = cmd.ExecuteReader()
                        If r.Read() Then
                            _currentId = r.GetInt32(0)
                            txtID.Text = _currentId.ToString()
                            txtCode.Text = r.GetString(1)
                            txtNameAr.Text = r.GetString(2)
                            txtNameEn.Text = If(r.IsDBNull(3), "", r.GetString(3))
                            cboType.SelectedValue = r.GetInt32(4)
                            chkDefault.Checked = r.GetBoolean(6)
                            chkActive.Checked = r.GetBoolean(7)

                            lblFormMode.Text = "✏️  وضع التعديل — ID: " & id
                            lblFormMode.BackColor = UITheme.Primary
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Reset Form
    ' ═══════════════════════════════════════════════════════
    Private Sub ResetForm()
        _currentId = 0
        txtID.Clear() : txtCode.Clear() : txtNameAr.Clear() : txtNameEn.Clear()
        If cboType.Items.Count > 0 Then cboType.SelectedIndex = 0
        chkDefault.Checked = False
        chkActive.Checked = True

        lblFormMode.Text = "➕  وضع الإضافة"
        lblFormMode.BackColor = UITheme.Success
        lblFormMode.ForeColor = UITheme.TextWhite

        dgvWarehouses.ClearSelection()
        txtCode.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Events
    ' ═══════════════════════════════════════════════════════
    Private Sub dgvWarehouses_SelectionChanged(sender As Object, e As EventArgs) _
        Handles dgvWarehouses.SelectionChanged

        If _loading Then Return
        If dgvWarehouses.SelectedRows.Count = 0 Then Return
        LoadIntoForm(CInt(dgvWarehouses.SelectedRows(0).Cells("WarehouseID").Value))
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ResetForm()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ResetForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtCode.Text) Then Msg("كود المخزن مطلوب.", txtCode) : Return
        If String.IsNullOrWhiteSpace(txtNameAr.Text) Then Msg("الاسم العربي مطلوب.", txtNameAr) : Return
        If cboType.SelectedValue Is Nothing Then Msg("نوع المخزن مطلوب.") : Return

        Dim sp As String = If(_currentId = 0, "[Core].[usp_Warehouse_Insert]", "[Core].[usp_Warehouse_Update]")

        Try
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand(sp, conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    If _currentId > 0 Then
                        cmd.Parameters.AddWithValue("@WarehouseID", _currentId)
                        cmd.Parameters.AddWithValue("@ModifiedBy", 1)
                    Else
                        cmd.Parameters.AddWithValue("@CreatedBy", 1)
                    End If

                    cmd.Parameters.AddWithValue("@WarehouseCode", txtCode.Text.Trim())
                    cmd.Parameters.AddWithValue("@WarehouseNameAr", txtNameAr.Text.Trim())
                    cmd.Parameters.AddWithValue("@WarehouseNameEn", If(String.IsNullOrEmpty(txtNameEn.Text.Trim()), CObj(DBNull.Value), txtNameEn.Text.Trim()))
                    cmd.Parameters.AddWithValue("@WarehouseTypeID", CInt(cboType.SelectedValue))
                    cmd.Parameters.AddWithValue("@IsDefault", chkDefault.Checked)
                    cmd.Parameters.AddWithValue("@IsActive", chkActive.Checked)

                    If _currentId = 0 Then
                        Dim newId As Object = cmd.ExecuteScalar()
                        MessageBox.Show("تم إضافة المخزن بنجاح.", "تم",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadData(CInt(newId)) : LoadIntoForm(CInt(newId))
                    Else
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("تم تعديل المخزن بنجاح.", "تم",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadData(_currentId) : LoadIntoForm(_currentId)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _currentId = 0 Then Msg("اختر مخزنًا من الجدول أولاً.") : Return

        If MessageBox.Show($"هل أنت متأكد من حذف المخزن: {txtNameAr.Text}؟",
                           "تأكيد الحذف", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand("[Core].[usp_Warehouse_Delete]", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@WarehouseID", _currentId)
                    cmd.Parameters.AddWithValue("@DeletedBy", 1)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("تم حذف المخزن بنجاح.", "تم",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData() : ResetForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim keepId As Integer = _currentId
        LoadData(keepId)
        If keepId > 0 Then LoadIntoForm(keepId) Else ResetForm()
    End Sub

    Private Sub Msg(text As String, Optional focus As Control = Nothing)
        MessageBox.Show(text, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        If focus IsNot Nothing Then focus.Focus()
    End Sub

End Class