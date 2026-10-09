Imports System
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Public Class FrmSalesShortageEdit
    Inherits Form

    Private ReadOnly _dto As SalesShortageDTO
    Private ReadOnly _repo As SalesShortageRepository

    Private _cboItem As ComboBox
    Private _cboWarehouse As ComboBox
    Private _numRequired As NumericUpDown
    Private _numAvailable As NumericUpDown
    Private _chkHasDate As CheckBox
    Private _dtRequired As DateTimePicker
    Private _txtRef As TextBox
    Private _cboStatus As ComboBox
    Private _cboSource As ComboBox
    Private _txtNotes As TextBox
    Private _btnSave As Button
    Private _btnCancel As Button

    Public Sub New(dto As SalesShortageDTO)
        _dto = dto
        _repo = New SalesShortageRepository()
        BuildUI()
        LoadLookups()
        If dto IsNot Nothing Then LoadDto()
    End Sub

    Private Sub BuildUI()
        Me.Text = If(_dto Is Nothing, "إضافة نقص جديد", "تعديل نقص")
        Me.ClientSize = New Size(520, 500)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.BackColor = Color.White

        Dim lblX As Integer = 400
        Dim ctrlX As Integer = 20
        Dim ctrlW As Integer = 360
        Dim y As Integer = 20

        _cboItem = New ComboBox()
        AddRow("الصنف:", lblX, ctrlX, ctrlW, y, _cboItem, True)

        _cboWarehouse = New ComboBox()
        AddRow("المخزن:", lblX, ctrlX, ctrlW, y, _cboWarehouse, True)

        _numRequired = New NumericUpDown() With {.DecimalPlaces = 2, .Maximum = 100000000, .TextAlign = HorizontalAlignment.Center}
        AddRow("الكمية المطلوبة:", lblX, ctrlX, ctrlW, y, _numRequired)

        _numAvailable = New NumericUpDown() With {.DecimalPlaces = 2, .Maximum = 100000000, .TextAlign = HorizontalAlignment.Center}
        AddRow("الكمية المتاحة:", lblX, ctrlX, ctrlW, y, _numAvailable)

        ' تاريخ الاحتياج
        _chkHasDate = New CheckBox With {.Text = "محدد", .Location = New Point(ctrlX + ctrlW - 60, y + 3), .AutoSize = True}
        _dtRequired = New DateTimePicker With {
            .Format = DateTimePickerFormat.Short, .Enabled = False,
            .Location = New Point(ctrlX, y - 2), .Width = ctrlW - 70
        }
        AddHandler _chkHasDate.CheckedChanged, Sub() _dtRequired.Enabled = _chkHasDate.Checked
        AddLabel("تاريخ الاحتياج:", lblX, y)
        Me.Controls.Add(_chkHasDate)
        Me.Controls.Add(_dtRequired)
        y += 34

        _txtRef = New TextBox()
        AddRow("المرجع:", lblX, ctrlX, ctrlW, y, _txtRef)

        _cboStatus = New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
        _cboStatus.Items.AddRange({"مفتوح", "تحت الإنتاج", "مغلق", "ملغي"})
        _cboStatus.SelectedIndex = 0
        AddRow("الحالة:", lblX, ctrlX, ctrlW, y, _cboStatus, True)

        _cboSource = New ComboBox() With {.DropDownStyle = ComboBoxStyle.DropDownList}
        _cboSource.Items.AddRange({"يدوي", "آلي (مخزون)", "مبيعات"})
        _cboSource.SelectedIndex = 0
        AddRow("المصدر:", lblX, ctrlX, ctrlW, y, _cboSource, True)

        AddLabel("ملاحظات:", lblX, y)
        _txtNotes = New TextBox With {
            .Multiline = True, .ScrollBars = ScrollBars.Vertical,
            .Location = New Point(ctrlX, y - 2), .Width = ctrlW, .Height = 60
        }
        Me.Controls.Add(_txtNotes)
        y += 70

        _btnSave = MakeBtn("حفظ", ctrlX + 120, y)
        _btnCancel = MakeBtn("إلغاء", ctrlX, y)
        AddHandler _btnSave.Click, AddressOf OnSave
        AddHandler _btnCancel.Click, Sub() Me.DialogResult = DialogResult.Cancel
        Me.Controls.Add(_btnSave)
        Me.Controls.Add(_btnCancel)

        Me.AcceptButton = _btnSave
        Me.CancelButton = _btnCancel
    End Sub

    Private Sub AddLabel(text As String, x As Integer, y As Integer)
        Me.Controls.Add(New Label With {
            .Text = text, .AutoSize = True,
            .Location = New Point(x, y + 3), .ForeColor = UITheme.TextPrimary
        })
    End Sub

    Private Sub AddRow(label As String, lblX As Integer, ctrlX As Integer, ctrlW As Integer,
                       ByRef y As Integer, ctrl As Control, Optional isCombo As Boolean = False)
        If isCombo Then
            Dim c = DirectCast(ctrl, ComboBox)
            c.DropDownStyle = ComboBoxStyle.DropDownList
        End If
        AddLabel(label, lblX, y)
        ctrl.Location = New Point(ctrlX, y - 2)
        ctrl.Width = ctrlW
        Me.Controls.Add(ctrl)
        y += 34
    End Sub

    Private Function MakeBtn(text As String, x As Integer, y As Integer) As Button
        Dim b As New Button With {
            .Text = text, .Location = New Point(x, y), .Size = New Size(100, 32),
            .BackColor = UITheme.ButtonNormal, .ForeColor = UITheme.TextPrimary,
            .Font = UITheme.FontBold, .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand
        }
        b.FlatAppearance.BorderSize = 0
        b.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Return b
    End Function

    ' ============================================================
    ' ✅ التحميل — كل قسم منفصل بـ Try/Catch عشان لو واحد فشل مش يأثر على التاني
    ' ============================================================
    Private Sub LoadLookups()

        ' ------- 1) الأصناف (منتج تام) -------
        Try
            Dim dtItems As DataTable = _repo.GetItemsLookup()

            Dim dti As New DataTable()
            dti.Columns.Add("ItemID", GetType(Integer))
            dti.Columns.Add("DisplayText", GetType(String))

            For Each r As DataRow In dtItems.Rows
                dti.Rows.Add(CInt(r("ItemID")), CStr(r("ItemName")))
            Next

            _cboItem.DataSource = Nothing
            _cboItem.DataSource = dti
            _cboItem.DisplayMember = "DisplayText"
            _cboItem.ValueMember = "ItemID"
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الأصناف:" & vbCrLf & ex.Message,
                            "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        ' ------- 2) المخازن -------
        Try
            Dim dtWh As DataTable = _repo.GetWarehousesLookup()

            Dim dtw As New DataTable()
            dtw.Columns.Add("WarehouseID", GetType(Integer))
            dtw.Columns.Add("DisplayText", GetType(String))

            ' صف "بدون مخزن"
            dtw.Rows.Add(0, "- بدون -")

            For Each r As DataRow In dtWh.Rows
                Dim whId As Integer = CInt(r("WarehouseID"))
                Dim nameAr As String = If(r("WarehouseNameAr") Is DBNull.Value, "", CStr(r("WarehouseNameAr")))
                Dim nameEn As String = If(r("WarehouseNameEn") Is DBNull.Value, "", CStr(r("WarehouseNameEn")))

                Dim display As String
                If Not String.IsNullOrWhiteSpace(nameAr) Then
                    display = nameAr
                ElseIf Not String.IsNullOrWhiteSpace(nameEn) Then
                    display = nameEn
                Else
                    display = whId.ToString()
                End If

                dtw.Rows.Add(whId, display)
            Next

            _cboWarehouse.DataSource = Nothing
            _cboWarehouse.DataSource = dtw
            _cboWarehouse.DisplayMember = "DisplayText"
            _cboWarehouse.ValueMember = "WarehouseID"
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل المخازن:" & vbCrLf & ex.Message,
                            "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub LoadDto()
        Try
            If _dto.ItemID > 0 Then _cboItem.SelectedValue = _dto.ItemID
        Catch
        End Try

        Try
            If _dto.WarehouseID.HasValue AndAlso _dto.WarehouseID.Value > 0 Then
                _cboWarehouse.SelectedValue = _dto.WarehouseID.Value
            Else
                _cboWarehouse.SelectedValue = 0
            End If
        Catch
        End Try

        _numRequired.Value = _dto.RequiredQty
        _numAvailable.Value = _dto.AvailableQty

        If _dto.RequiredDate.HasValue Then
            _chkHasDate.Checked = True
            _dtRequired.Value = _dto.RequiredDate.Value
        End If

        _txtRef.Text = _dto.ReferenceNo

        If _dto.Status >= 1 AndAlso _dto.Status <= 4 Then _cboStatus.SelectedIndex = _dto.Status - 1
        If _dto.SourceType >= 1 AndAlso _dto.SourceType <= 3 Then _cboSource.SelectedIndex = _dto.SourceType - 1

        _txtNotes.Text = _dto.Notes
    End Sub

    Private Sub OnSave(sender As Object, e As EventArgs)
        If _cboItem.SelectedValue Is Nothing Then
            MessageBox.Show("اختر الصنف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If
        If _numRequired.Value <= 0 Then
            MessageBox.Show("أدخل الكمية المطلوبة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If

        Dim d As SalesShortageDTO = If(_dto, New SalesShortageDTO())
        d.ItemID = CInt(_cboItem.SelectedValue)

        Dim whId As Integer = 0
        If _cboWarehouse.SelectedValue IsNot Nothing Then
            Integer.TryParse(_cboWarehouse.SelectedValue.ToString(), whId)
        End If
        d.WarehouseID = If(whId = 0, CType(Nothing, Integer?), whId)

        d.RequiredQty = _numRequired.Value
        d.AvailableQty = _numAvailable.Value
        d.RequiredDate = If(_chkHasDate.Checked, CType(_dtRequired.Value, DateTime?), Nothing)
        d.ReferenceNo = _txtRef.Text.Trim()
        d.Status = CByte(_cboStatus.SelectedIndex + 1)
        d.SourceType = CByte(_cboSource.SelectedIndex + 1)
        d.Notes = _txtNotes.Text.Trim()

        Try
            If _dto Is Nothing Then
                _repo.Insert(d, 1)
            Else
                _repo.Update(d, 1)
            End If
            Me.DialogResult = DialogResult.OK
        Catch ex As Exception
            MessageBox.Show("خطأ في الحفظ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class