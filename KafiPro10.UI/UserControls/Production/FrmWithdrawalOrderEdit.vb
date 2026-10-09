Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class FrmWithdrawalOrderEdit

    Private ReadOnly _svc As New WithdrawalService()
    Private ReadOnly _existing As WithdrawalOrderDto
    Private ReadOnly CurrentUserID As Integer = 1

    Private WithEvents DtpDate As New DateTimePicker()
    Private WithEvents CboWarehouse As New ComboBox()
    Private WithEvents CboShift As New ComboBox()
    Private WithEvents CboLine As New ComboBox()
    Private WithEvents TxtNotes As New TextBox()
    Private WithEvents CboItem As New ComboBox()
    Private WithEvents TxtQty As New TextBox()
    Private WithEvents BtnAddItem As New Button()
    Private WithEvents BtnRemoveItem As New Button()
    Private WithEvents GridItems As New DataGridView()
    Private WithEvents BtnSaveDraft As New Button()
    Private WithEvents BtnSaveSubmit As New Button()
    Private WithEvents BtnCancel As New Button()

    Private ReadOnly _items As New List(Of CreateWithdrawalDetailDto)
    Private ReadOnly _allItems As New List(Of ItemLookup)

    Private Class ItemLookup
        Public Property ItemID As Integer
        Public Property ItemCode As String
        Public Property ItemName As String
        Public Property InventoryUnitID As Integer
        Public Property UnitName As String
        Public Overrides Function ToString() As String
            Return ItemCode & " — " & ItemName
        End Function
    End Class

    Public Sub New(existing As WithdrawalOrderDto)
        _existing = existing
        InitializeForm()
        LoadLookups()
        If _existing IsNot Nothing Then LoadExisting()
    End Sub

    Private Sub InitializeForm()
        Me.Text = If(_existing Is Nothing, "إنشاء طلب صرف جديد", "تعديل طلب الصرف")
        Me.Size = New Size(1050, 700)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.BackColor = Color.WhiteSmoke

        Dim lblTitle As New Label With {
            .Text = If(_existing Is Nothing, "➕  إنشاء طلب صرف جديد", "✏️  تعديل الطلب"),
            .Dock = DockStyle.Top, .Height = 45,
            .BackColor = Color.FromArgb(52, 73, 94), .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 15, 0)
        }

        Dim pnl As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(15), .BackColor = Color.White}

        AddLabel(pnl, "التاريخ:", 960, 20)
        dtpDate.Location = New Point(840, 17)
        dtpDate.Size = New Size(115, 27)
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Value = DateTime.Today
        pnl.Controls.Add(dtpDate)

        AddLabel(pnl, "المخزن:", 720, 20)
        cboWarehouse.Location = New Point(520, 17)
        cboWarehouse.Size = New Size(195, 27)
        cboWarehouse.DropDownStyle = ComboBoxStyle.DropDownList
        pnl.Controls.Add(cboWarehouse)

        AddLabel(pnl, "الوردية:", 460, 20)
        cboShift.Location = New Point(310, 17)
        cboShift.Size = New Size(145, 27)
        cboShift.DropDownStyle = ComboBoxStyle.DropDownList
        pnl.Controls.Add(cboShift)

        AddLabel(pnl, "خط الإنتاج:", 260, 20)
        cboLine.Location = New Point(100, 17)
        cboLine.Size = New Size(155, 27)
        cboLine.DropDownStyle = ComboBoxStyle.DropDownList
        pnl.Controls.Add(cboLine)

        AddLabel(pnl, "ملاحظات:", 960, 65)
        txtNotes.Location = New Point(400, 62)
        txtNotes.Size = New Size(555, 27)
        pnl.Controls.Add(txtNotes)

        Dim lblItems As New Label With {
            .Text = "📦 الأصناف المطلوبة",
            .Location = New Point(20, 105), .AutoSize = True,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .ForeColor = Color.FromArgb(52, 73, 94)
        }
        pnl.Controls.Add(lblItems)

        Dim pnlAdd As New Panel With {.Location = New Point(20, 135), .Size = New Size(980, 40)}

        pnlAdd.Controls.Add(New Label With {.Text = "الصنف:", .Location = New Point(920, 10), .AutoSize = True})
        cboItem.Location = New Point(500, 7)
        cboItem.Size = New Size(415, 27)
        cboItem.DropDownStyle = ComboBoxStyle.DropDownList
        cboItem.Location = New Point(500, 7)
        cboItem.Size = New Size(415, 27)
        cboItem.DropDownStyle = ComboBoxStyle.DropDownList
        pnlAdd.Controls.Add(cboItem)
        pnlAdd.Controls.Add(cboItem)

        pnlAdd.Controls.Add(New Label With {.Text = "الكمية:", .Location = New Point(445, 10), .AutoSize = True})
        txtQty.Location = New Point(365, 7)
        txtQty.Size = New Size(75, 27)
        pnlAdd.Controls.Add(txtQty)

        btnAddItem.Text = "➕ إضافة"
        btnAddItem.Location = New Point(250, 5)
        btnAddItem.Size = New Size(105, 32)
        StyleBtn(btnAddItem, Color.FromArgb(46, 204, 113))
        pnlAdd.Controls.Add(btnAddItem)

        btnRemoveItem.Text = "🗑️ حذف المحدد"
        btnRemoveItem.Location = New Point(120, 5)
        btnRemoveItem.Size = New Size(120, 32)
        StyleBtn(btnRemoveItem, Color.FromArgb(231, 76, 60))
        pnlAdd.Controls.Add(btnRemoveItem)

        pnl.Controls.Add(pnlAdd)

        gridItems.Location = New Point(20, 185)
        gridItems.Size = New Size(980, 340)
        gridItems.BackgroundColor = Color.White
        gridItems.BorderStyle = BorderStyle.FixedSingle
        gridItems.RightToLeft = RightToLeft.Yes
        gridItems.AllowUserToAddRows = False
        gridItems.ReadOnly = True
        gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        gridItems.ColumnHeadersHeight = 38
        gridItems.RowTemplate.Height = 30
        gridItems.EnableHeadersVisualStyles = False
        gridItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94)
        gridItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        gridItems.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        pnl.Controls.Add(gridItems)

        btnSaveDraft.Text = "💾 حفظ كمسودة"
        btnSaveDraft.Location = New Point(760, 545)
        btnSaveDraft.Size = New Size(150, 40)
        StyleBtn(btnSaveDraft, Color.FromArgb(52, 152, 219))
        pnl.Controls.Add(btnSaveDraft)

        btnSaveSubmit.Text = "📤 حفظ وإرسال"
        btnSaveSubmit.Location = New Point(600, 545)
        btnSaveSubmit.Size = New Size(150, 40)
        StyleBtn(btnSaveSubmit, Color.FromArgb(155, 89, 182))
        pnl.Controls.Add(btnSaveSubmit)

        btnCancel.Text = "❌ إلغاء"
        btnCancel.Location = New Point(480, 545)
        btnCancel.Size = New Size(110, 40)
        StyleBtn(btnCancel, Color.FromArgb(149, 165, 166))
        btnCancel.DialogResult = DialogResult.Cancel
        pnl.Controls.Add(btnCancel)

        Me.Controls.Add(pnl)
        Me.Controls.Add(lblTitle)
    End Sub

    Private Sub AddLabel(parent As Control, text As String, x As Integer, y As Integer)
        parent.Controls.Add(New Label With {
            .Text = text, .Location = New Point(x, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        })
    End Sub

    Private Sub StyleBtn(b As Button, c As Color)
        b.BackColor = c : b.ForeColor = Color.White
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        b.Cursor = Cursors.Hand
    End Sub

    Private Sub LoadLookups()
        Try
            cboWarehouse.Items.Clear()
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand(
                    "SELECT WarehouseID, WarehouseCode, WarehouseNameAr FROM [Core].[Warehouse] WHERE IsDeleted=0 AND IsActive=1 ORDER BY WarehouseCode", conn)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            cboWarehouse.Items.Add(New ComboItem(
                                CStr(rd("WarehouseCode")) & " — " & CStr(rd("WarehouseNameAr")),
                                CInt(rd("WarehouseID")).ToString()))
                        End While
                    End Using
                End Using

                cboShift.Items.Clear()
                cboShift.Items.Add(New ComboItem("(بدون)", ""))
                Using cmd As New SqlCommand(
                    "SELECT ShiftTypeID, ShiftNameAr FROM [Production].[ShiftType] WHERE IsDeleted=0 ORDER BY ShiftCode", conn)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            cboShift.Items.Add(New ComboItem(CStr(rd("ShiftNameAr")),
                                                              CInt(rd("ShiftTypeID")).ToString()))
                        End While
                    End Using
                End Using

                cboLine.Items.Clear()
                cboLine.Items.Add(New ComboItem("(بدون)", ""))
                Using cmd As New SqlCommand(
                    "SELECT ProductionLineID, LineNameAr FROM [Production].[ProductionLine] WHERE IsDeleted=0 ORDER BY LineCode", conn)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            cboLine.Items.Add(New ComboItem(CStr(rd("LineNameAr")),
                                                             CInt(rd("ProductionLineID")).ToString()))
                        End While
                    End Using
                End Using

                _allItems.Clear()
                cboItem.Items.Clear()
                Using cmd As New SqlCommand(
    "SELECT i.ItemID, i.ItemCode, i.ItemNameAr, i.InventoryUnitID, u.UnitNameAr
     FROM [Core].[Item] i
     LEFT JOIN [Core].[UnitOfMeasure] u ON u.UnitOfMeasureID = i.InventoryUnitID
     INNER JOIN [Core].[ItemType] it ON it.ItemTypeID = i.ItemTypeID
     WHERE i.IsDeleted = 0
       AND i.IsActive = 1
       AND it.ItemTypeCode IN ('RAW_MATERIAL', 'PACKAGING', 'CONSUMABLE', 'SPARE_PART')
     ORDER BY i.ItemCode", conn)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            Dim it As New ItemLookup With {
                                .ItemID = CInt(rd("ItemID")),
                                .ItemCode = CStr(rd("ItemCode")),
                                .ItemName = CStr(rd("ItemNameAr")),
                                .InventoryUnitID = CInt(rd("InventoryUnitID")),
                                .UnitName = If(IsDBNull(rd("UnitNameAr")), "", CStr(rd("UnitNameAr")))
                            }
                            _allItems.Add(it)
                            cboItem.Items.Add(it)
                        End While
                    End Using
                End Using
            End Using

            If cboShift.Items.Count > 0 Then cboShift.SelectedIndex = 0
            If cboLine.Items.Count > 0 Then cboLine.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل القوائم: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadExisting()
        dtpDate.Value = _existing.WithdrawalDate
        txtNotes.Text = _existing.Notes

        For i = 0 To cboWarehouse.Items.Count - 1
            If DirectCast(cboWarehouse.Items(i), ComboItem).Value = _existing.WarehouseID.ToString() Then
                cboWarehouse.SelectedIndex = i : Exit For
            End If
        Next

        If _existing.ShiftID.HasValue Then
            For i = 0 To cboShift.Items.Count - 1
                If DirectCast(cboShift.Items(i), ComboItem).Value = _existing.ShiftID.Value.ToString() Then
                    cboShift.SelectedIndex = i : Exit For
                End If
            Next
        End If

        If _existing.ProductionLineID.HasValue Then
            For i = 0 To cboLine.Items.Count - 1
                If DirectCast(cboLine.Items(i), ComboItem).Value = _existing.ProductionLineID.Value.ToString() Then
                    cboLine.SelectedIndex = i : Exit For
                End If
            Next
        End If

        _items.Clear()
        For Each d In _existing.Details
            _items.Add(New CreateWithdrawalDetailDto With {
                .ItemID = d.ItemID,
                .UnitID = d.UnitID,
                .RequestedQty = d.RequestedQty,
                .Notes = d.Notes
            })
        Next
        RefreshGrid()
    End Sub

    Private Sub BtnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        If cboItem.SelectedItem Is Nothing Then
            MessageBox.Show("اختر صنفاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim qty As Decimal
        If Not Decimal.TryParse(txtQty.Text, qty) OrElse qty <= 0 Then
            MessageBox.Show("أدخل كمية صحيحة موجبة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQty.Focus() : Return
        End If

        Dim it = DirectCast(cboItem.SelectedItem, ItemLookup)

        If _items.Exists(Function(x) x.ItemID = it.ItemID) Then
            MessageBox.Show("الصنف مضاف بالفعل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        _items.Add(New CreateWithdrawalDetailDto With {
            .ItemID = it.ItemID,
            .UnitID = it.InventoryUnitID,
            .RequestedQty = qty
        })

        RefreshGrid()
        txtQty.Clear()
        cboItem.SelectedIndex = -1
        cboItem.Focus()
    End Sub

    Private Sub BtnRemoveItem_Click(sender As Object, e As EventArgs) Handles btnRemoveItem.Click
        If gridItems.CurrentRow Is Nothing Then Return
        Dim itemId = CInt(gridItems.CurrentRow.Cells("ItemID").Value)
        _items.RemoveAll(Function(x) x.ItemID = itemId)
        RefreshGrid()
    End Sub

    Private Sub RefreshGrid()
        Dim dt As New DataTable()
        dt.Columns.Add("ItemID", GetType(Integer))
        dt.Columns.Add("كود الصنف", GetType(String))
        dt.Columns.Add("الصنف", GetType(String))
        dt.Columns.Add("الوحدة", GetType(String))
        dt.Columns.Add("الكمية المطلوبة", GetType(Decimal))

        For Each d In _items
            Dim it = _allItems.Find(Function(x) x.ItemID = d.ItemID)
            dt.Rows.Add(d.ItemID, If(it?.ItemCode, ""), If(it?.ItemName, ""),
                        If(it?.UnitName, ""), d.RequestedQty)
        Next

        gridItems.DataSource = dt
        If gridItems.Columns.Contains("ItemID") Then gridItems.Columns("ItemID").Visible = False
    End Sub

    Private Sub BtnSaveDraft_Click(sender As Object, e As EventArgs) Handles btnSaveDraft.Click
        SaveOrder(False)
    End Sub

    Private Sub BtnSaveSubmit_Click(sender As Object, e As EventArgs) Handles btnSaveSubmit.Click
        SaveOrder(True)
    End Sub

    Private Sub SaveOrder(submitAfterSave As Boolean)
        Try
            If cboWarehouse.SelectedItem Is Nothing Then
                MessageBox.Show("اختر المخزن.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If _items.Count = 0 Then
                MessageBox.Show("أضف صنفاً واحداً على الأقل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dto As New CreateWithdrawalDto With {
                .WithdrawalDate = dtpDate.Value.Date,
                .WarehouseID = Integer.Parse(DirectCast(cboWarehouse.SelectedItem, ComboItem).Value),
                .ShiftID = GetIntOrNothing(cboShift),
                .ProductionLineID = GetIntOrNothing(cboLine),
                .Notes = If(String.IsNullOrWhiteSpace(txtNotes.Text), Nothing, txtNotes.Text.Trim()),
                .CreatedBy = CurrentUserID,
                .Details = _items
            }

            Dim wid As Long
            If _existing Is Nothing Then
                Dim r = _svc.Create(dto)
                wid = r.WithdrawalID
            Else
                _svc.Update(_existing.WithdrawalID, dto)
                wid = _existing.WithdrawalID
            End If

            If submitAfterSave Then
                _svc.Submit(wid, CurrentUserID)
                MessageBox.Show("✅ تم الحفظ والإرسال.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("✅ تم الحفظ كمسودة.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetIntOrNothing(cbo As ComboBox) As Integer?
        If cbo.SelectedItem Is Nothing Then Return Nothing
        Dim v = DirectCast(cbo.SelectedItem, ComboItem).Value
        If String.IsNullOrEmpty(v) Then Return Nothing
        Return Integer.Parse(v)
    End Function

End Class