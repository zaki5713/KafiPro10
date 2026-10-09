Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence
Imports Microsoft.Data.SqlClient
Imports System.Data

Public Class FrmBranchEdit
    Inherits System.Windows.Forms.Form

    Private ReadOnly _existing As BranchDto
    Private ReadOnly _svc As WarehouseMasterService
    Private ReadOnly CurrentUserID As Integer = 1

    Private WithEvents cboCompany As New ComboBox()
    Private WithEvents txtCode As New TextBox()
    Private WithEvents txtNameAr As New TextBox()
    Private WithEvents txtNameEn As New TextBox()
    Private WithEvents chkActive As New CheckBox()
    Private WithEvents btnSave As New Button()
    Private WithEvents btnCancel As New Button()

    Public Sub New(existing As BranchDto, svc As WarehouseMasterService)
        _existing = existing
        _svc = svc
        InitializeForm()
        LoadCompanies()
        If existing IsNot Nothing Then LoadExisting()
    End Sub

    Private Sub InitializeForm()
        Me.Text = If(_existing Is Nothing, "إضافة فرع جديد", "تعديل بيانات الفرع")
        Me.Size = New Size(520, 400)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.RightToLeft = RightToLeft.No
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.BackColor = Color.WhiteSmoke

        Dim lblTitle As New Label With {
            .Text = If(_existing Is Nothing, "🏬  إضافة فرع جديد", "✏️  تعديل بيانات الفرع"),
            .Dock = DockStyle.Top,
            .Height = 40,
            .BackColor = Color.FromArgb(52, 73, 94),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleLeft,
            .Padding = New Padding(0, 0, 15, 0)
        }

        Dim pnl As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(20), .BackColor = Color.White}

        AddLabel(pnl, "الشركة:", 20, 25)
        cboCompany.Location = New Point(140, 22)
        cboCompany.Size = New Size(320, 28)
        cboCompany.DropDownStyle = ComboBoxStyle.DropDownList
        pnl.Controls.Add(cboCompany)

        AddLabel(pnl, "الكود:", 20, 140)
        txtCode.Location = New Point(30, 62)
        txtCode.Size = New Size(320, 28)
        pnl.Controls.Add(txtCode)

        AddLabel(pnl, "الاسم (عربي):", 20, 105)
        txtNameAr.Location = New Point(140, 102)
        txtNameAr.Size = New Size(320, 28)
        pnl.Controls.Add(txtNameAr)

        AddLabel(pnl, "الاسم (إنجليزي):", 20, 145)
        txtNameEn.Location = New Point(140, 142)
        txtNameEn.Size = New Size(320, 28)
        pnl.Controls.Add(txtNameEn)

        chkActive.Text = "نشط"
        chkActive.Location = New Point(400, 190)
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

    Private Sub LoadCompanies()
        Try
            cboCompany.Items.Clear()
            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand(
                    "SELECT CompanyID, CompanyNameAr FROM [Core].[Company] WHERE IsDeleted = 0 AND IsActive = 1 ORDER BY CompanyCode", conn)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            cboCompany.Items.Add(New ComboItem(
                                CStr(rd("CompanyNameAr")),
                                CInt(rd("CompanyID")).ToString()))
                        End While
                    End Using
                End Using
            End Using

            If cboCompany.Items.Count > 0 Then cboCompany.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الشركات: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadExisting()
        txtCode.Text = _existing.BranchCode
        txtNameAr.Text = _existing.BranchNameAr
        txtNameEn.Text = _existing.BranchNameEn
        chkActive.Checked = _existing.IsActive

        For i = 0 To cboCompany.Items.Count - 1
            If DirectCast(cboCompany.Items(i), ComboItem).Value = _existing.CompanyID.ToString() Then
                cboCompany.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If cboCompany.SelectedItem Is Nothing Then
                MessageBox.Show("اختر الشركة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If String.IsNullOrWhiteSpace(txtCode.Text) Then
                MessageBox.Show("الكود مطلوب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCode.Focus() : Return
            End If

            If String.IsNullOrWhiteSpace(txtNameAr.Text) Then
                MessageBox.Show("الاسم مطلوب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNameAr.Focus() : Return
            End If

            Dim companyID = Integer.Parse(DirectCast(cboCompany.SelectedItem, ComboItem).Value)

            If _existing Is Nothing Then
                _svc.InsertBranch(companyID, txtCode.Text.Trim(), txtNameAr.Text.Trim(),
                                  txtNameEn.Text.Trim(), chkActive.Checked, CurrentUserID)
            Else
                _svc.UpdateBranch(_existing.BranchID, companyID, txtCode.Text.Trim(), txtNameAr.Text.Trim(),
                                  txtNameEn.Text.Trim(), chkActive.Checked, CurrentUserID)
            End If

            MessageBox.Show("✅ تم الحفظ بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class