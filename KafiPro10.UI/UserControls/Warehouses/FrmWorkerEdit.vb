Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class FrmWorkerEdit
    Inherits System.Windows.Forms.Form

    Private ReadOnly _existing As WorkerDto
    Private ReadOnly _svc As WarehouseMasterService
    Private ReadOnly CurrentUserID As Integer = 1

    Private WithEvents txtCode As New TextBox()
    Private WithEvents txtNameAr As New TextBox()
    Private WithEvents txtNameEn As New TextBox()
    Private WithEvents cboRole As New ComboBox()
    Private WithEvents txtPhone As New TextBox()
    Private WithEvents txtNationalID As New TextBox()
    Private WithEvents chkHasHireDate As New CheckBox()
    Private WithEvents dtpHire As New DateTimePicker()
    Private WithEvents cboDefaultWh As New ComboBox()
    Private WithEvents txtNotes As New TextBox()
    Private WithEvents chkActive As New CheckBox()
    Private WithEvents btnSave As New Button()
    Private WithEvents btnCancel As New Button()

    Public Sub New(existing As WorkerDto, svc As WarehouseMasterService)
        _existing = existing
        _svc = svc
        InitializeForm()
        LoadWarehouses()
        If existing IsNot Nothing Then LoadExisting()
    End Sub

    Private Sub InitializeForm()
        Me.Text = If(_existing Is Nothing, "إضافة عامل جديد", "تعديل بيانات العامل")
        Me.Size = New Size(550, 600)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.RightToLeft = RightToLeft.No
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.BackColor = Color.Wheat

        Dim lblTitle As New Label With {
            .Text = If(_existing Is Nothing, "👷  إضافة عامل جديد", "✏️  تعديل بيانات العامل"),
            .Dock = DockStyle.Top,
            .Height = 35,
            .BackColor = Color.FromArgb(52, 73, 94),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 15, 0)
        }

        Dim pnl As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(20), .BackColor = Color.Wheat}

        Dim y As Integer = 20
        AddField(pnl, "الكود:", txtCode, y) : y += 45
        AddField(pnl, "الاسم (عربي):", txtNameAr, y) : y += 45
        AddField(pnl, "الاسم (إنجليزي):", txtNameEn, y) : y += 45

        ' الوظيفة
        AddLabel(pnl, "الوظيفة:", 420, y)
        cboRole.Location = New Point(50, y - 3)
        cboRole.Size = New Size(330, 28)
        cboRole.DropDownStyle = ComboBoxStyle.DropDownList
        cboRole.Items.Add(New ComboItem("عامل", "WORKER"))
        cboRole.Items.Add(New ComboItem("أمين مخزن", "STOREKEEPER"))
        cboRole.Items.Add(New ComboItem("سائق كلارك", "FORKLIFT_DRIVER"))
        cboRole.Items.Add(New ComboItem("مدير مخزن", "MANAGER"))
        pnl.Controls.Add(cboRole)
        y += 45

        AddField(pnl, "التليفون:", txtPhone, y) : y += 45
        AddField(pnl, "الرقم القومي:", txtNationalID, y) : y += 45

        ' تاريخ التعيين
        chkHasHireDate.Text = "تحديد تاريخ التعيين"
        chkHasHireDate.Location = New Point(420, y)
        chkHasHireDate.AutoSize = True
        pnl.Controls.Add(chkHasHireDate)

        dtpHire.Location = New Point(50, y - 3)
        dtpHire.Size = New Size(330, 28)
        dtpHire.Format = DateTimePickerFormat.Short
        dtpHire.Enabled = False
        dtpHire.Value = DateTime.Today
        pnl.Controls.Add(dtpHire)
        y += 45

        ' المخزن الافتراضي
        AddLabel(pnl, "المخزن الافتراضي", 420, y)
        cboDefaultWh.Location = New Point(50, y - 3)
        cboDefaultWh.Size = New Size(330, 28)
        cboDefaultWh.DropDownStyle = ComboBoxStyle.DropDownList
        pnl.Controls.Add(cboDefaultWh)
        y += 45

        ' الملاحظات
        AddLabel(pnl, "الملاحظات:", 420, y)
        txtNotes.Location = New Point(50, y - 3)
        txtNotes.Size = New Size(330, 55)
        txtNotes.Multiline = True
        txtNotes.ScrollBars = ScrollBars.Vertical
        pnl.Controls.Add(txtNotes)
        y += 65

        ' نشط
        chkActive.Text = "نشط"
        chkActive.Location = New Point(430, y)
        chkActive.AutoSize = True
        chkActive.Checked = True
        pnl.Controls.Add(chkActive)

        ' أزرار
        btnSave.Text = "💾 حفظ"
        btnSave.Location = New Point(360, y + 30)
        btnSave.Size = New Size(120, 30)
        btnSave.BackColor = Color.FromArgb(46, 204, 113)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnSave.Cursor = Cursors.Hand
        pnl.Controls.Add(btnSave)

        btnCancel.Text = "❌ إلغاء"
        btnCancel.Location = New Point(230, y + 30)
        btnCancel.Size = New Size(120, 30)
        btnCancel.BackColor = Color.FromArgb(149, 165, 166)
        btnCancel.ForeColor = Color.White
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnCancel.Cursor = Cursors.Hand
        btnCancel.DialogResult = DialogResult.Cancel
        pnl.Controls.Add(btnCancel)

        Me.Controls.Add(pnl)
        Me.Controls.Add(lblTitle)
    End Sub

    Private Sub AddLabel(parent As Panel, text As String, x As Integer, y As Integer)
        parent.Controls.Add(New Label With {
            .Text = text, .Location = New Point(x, y + 3),
            .AutoSize = True, .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        })
    End Sub

    Private Sub AddField(parent As Panel, label As String, txt As TextBox, y As Integer)
        AddLabel(parent, label, 420, y)
        txt.Location = New Point(50, y)
        txt.Size = New Size(330, 28)
        parent.Controls.Add(txt)
    End Sub

    Private Sub LoadWarehouses()
        Try
            Dim whs = _svc.GetWarehouses(True)
            cboDefaultWh.Items.Clear()
            cboDefaultWh.Items.Add(New ComboItem("(بدون)", ""))
            For Each w In whs
                cboDefaultWh.Items.Add(New ComboItem(w.WarehouseNameAr, w.WarehouseID.ToString()))
            Next
            cboDefaultWh.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل المخازن: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadExisting()
        txtCode.Text = _existing.WorkerCode
        txtNameAr.Text = _existing.WorkerNameAr
        txtNameEn.Text = _existing.WorkerNameEn
        txtPhone.Text = _existing.PhoneNumber
        txtNationalID.Text = _existing.NationalID
        txtNotes.Text = _existing.Notes
        chkActive.Checked = _existing.IsActive

        ' الوظيفة
        For i = 0 To cboRole.Items.Count - 1
            If DirectCast(cboRole.Items(i), ComboItem).Value = _existing.WarehouseRole Then
                cboRole.SelectedIndex = i
                Exit For
            End If
        Next

        ' تاريخ التعيين
        If _existing.HireDate.HasValue Then
            chkHasHireDate.Checked = True
            dtpHire.Value = _existing.HireDate.Value
        End If

        ' المخزن الافتراضي
        If _existing.DefaultWarehouseID.HasValue Then
            For i = 0 To cboDefaultWh.Items.Count - 1
                If DirectCast(cboDefaultWh.Items(i), ComboItem).Value = _existing.DefaultWarehouseID.Value.ToString() Then
                    cboDefaultWh.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub ChkHasHireDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasHireDate.CheckedChanged
        dtpHire.Enabled = chkHasHireDate.Checked
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If String.IsNullOrWhiteSpace(txtCode.Text) Then
                MessageBox.Show("الكود مطلوب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCode.Focus() : Return
            End If

            If String.IsNullOrWhiteSpace(txtNameAr.Text) Then
                MessageBox.Show("الاسم مطلوب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNameAr.Focus() : Return
            End If

            If cboRole.SelectedItem Is Nothing Then
                MessageBox.Show("اختر الوظيفة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim role = DirectCast(cboRole.SelectedItem, ComboItem).Value
            Dim defaultWhID As Integer? = Nothing
            If cboDefaultWh.SelectedItem IsNot Nothing Then
                Dim v = DirectCast(cboDefaultWh.SelectedItem, ComboItem).Value
                If Not String.IsNullOrEmpty(v) Then defaultWhID = Integer.Parse(v)
            End If

            Dim hireDate As Date? = Nothing
            If chkHasHireDate.Checked Then hireDate = dtpHire.Value.Date

            If _existing Is Nothing Then
                _svc.InsertWorker(txtCode.Text.Trim(), txtNameAr.Text.Trim(), txtNameEn.Text.Trim(),
                                  role, txtPhone.Text.Trim(), txtNationalID.Text.Trim(),
                                  hireDate, defaultWhID, txtNotes.Text.Trim(), chkActive.Checked, CurrentUserID)
            Else
                _svc.UpdateWorker(_existing.WorkerID, txtCode.Text.Trim(), txtNameAr.Text.Trim(), txtNameEn.Text.Trim(),
                                  role, txtPhone.Text.Trim(), txtNationalID.Text.Trim(),
                                  hireDate, defaultWhID, txtNotes.Text.Trim(), chkActive.Checked, CurrentUserID)
            End If

            MessageBox.Show("✅ تم الحفظ بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class