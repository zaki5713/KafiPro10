Imports System.Drawing
Imports System.Windows.Forms

Public Class FrmWarehouseEdit
    Inherits System.Windows.Forms.Form

    Private ReadOnly _existing As WarehouseDto
    Private ReadOnly _svc As WarehouseMasterService
    Private ReadOnly CurrentUserID As Integer = 1

    Private WithEvents txtCode As New TextBox()
    Private WithEvents txtNameAr As New TextBox()
    Private WithEvents txtNameEn As New TextBox()
    Private WithEvents cboType As New ComboBox()
    Private WithEvents chkDefault As New CheckBox()
    Private WithEvents chkActive As New CheckBox()
    Private WithEvents btnSave As New Button()
    Private WithEvents btnCancel As New Button()

    Public Sub New(existing As WarehouseDto, svc As WarehouseMasterService)
        _existing = existing
        _svc = svc
        InitializeForm()
        LoadTypes()
        If existing IsNot Nothing Then LoadExisting()
    End Sub

    Private Sub InitializeForm()
        Me.Text = If(_existing Is Nothing, "إضافة مخزن جديد", "تعديل مخزن")
        Me.Size = New Size(550, 450)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.RightToLeft = RightToLeft.No
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.BackColor = Color.WhiteSmoke

        Dim lblTitle As New Label With {
            .Text = If(_existing Is Nothing, "🏢  إضافة مخزن جديد", "✏️  تعديل بيانات المخزن"),
            .Dock = DockStyle.Top,
            .Height = 35,
            .BackColor = Color.FromArgb(52, 73, 94),
            .ForeColor = Color.Wheat,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 15, 0)
        }

        Dim pnl As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(20), .BackColor = Color.White}

        AddLabel(pnl, "الكود:", 450, 25)
        txtCode.Location = New Point(100, 22)
        txtCode.Size = New Size(320, 28)
        pnl.Controls.Add(txtCode)

        AddLabel(pnl, "الاسم (عربي):", 20, 65)
        txtNameAr.Location = New Point(140, 62)
        txtNameAr.Size = New Size(320, 28)
        pnl.Controls.Add(txtNameAr)

        AddLabel(pnl, "الاسم (إنجليزي):", 20, 105)
        txtNameEn.Location = New Point(140, 102)
        txtNameEn.Size = New Size(320, 28)
        pnl.Controls.Add(txtNameEn)

        AddLabel(pnl, "النوع:", 20, 145)
        cboType.Location = New Point(140, 142)
        cboType.Size = New Size(320, 28)
        cboType.DropDownStyle = ComboBoxStyle.DropDownList
        pnl.Controls.Add(cboType)

        chkDefault.Text = "مخزن افتراضي"
        chkDefault.Location = New Point(380, 190)
        chkDefault.AutoSize = True
        pnl.Controls.Add(chkDefault)

        chkActive.Text = "نشط"
        chkActive.Location = New Point(260, 190)
        chkActive.AutoSize = True
        chkActive.Checked = True
        pnl.Controls.Add(chkActive)

        btnSave.Text = "💾 حفظ"
        btnSave.Location = New Point(360, 240)
        btnSave.Size = New Size(100, 38)
        btnSave.BackColor = Color.FromArgb(46, 204, 113)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnSave.Cursor = Cursors.Hand
        pnl.Controls.Add(btnSave)

        btnCancel.Text = "❌ إلغاء"
        btnCancel.Location = New Point(250, 240)
        btnCancel.Size = New Size(100, 38)
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

    Private Sub LoadTypes()
        Try
            Dim types = _svc.GetWarehouseTypes()
            cboType.DataSource = types
            cboType.DisplayMember = "TypeNameAr"
            cboType.ValueMember = "WarehouseTypeID"
            cboType.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الأنواع: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadExisting()
        txtCode.Text = _existing.WarehouseCode
        txtNameAr.Text = _existing.WarehouseNameAr
        txtNameEn.Text = _existing.WarehouseNameEn
        chkDefault.Checked = _existing.IsDefault
        chkActive.Checked = _existing.IsActive

        If cboType.DataSource IsNot Nothing Then
            For i = 0 To cboType.Items.Count - 1
                Dim t = DirectCast(cboType.Items(i), WarehouseTypeDto)
                If t.WarehouseTypeID = _existing.WarehouseTypeID Then
                    cboType.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If cboType.SelectedItem Is Nothing Then
                MessageBox.Show("اختر نوع المخزن.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim typeID = CInt(cboType.SelectedValue)

            If _existing Is Nothing Then
                _svc.InsertWarehouse(txtCode.Text.Trim(), txtNameAr.Text.Trim(), txtNameEn.Text.Trim(),
                                     typeID, chkDefault.Checked, chkActive.Checked, CurrentUserID)
            Else
                _svc.UpdateWarehouse(_existing.WarehouseID, txtCode.Text.Trim(), txtNameAr.Text.Trim(),
                                     txtNameEn.Text.Trim(), typeID, chkDefault.Checked,
                                     chkActive.Checked, CurrentUserID)
            End If

            MessageBox.Show("✅ تم الحفظ بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class