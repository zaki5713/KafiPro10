Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProShiftsUC
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlFields = New Panel()
        lblID = New Label()
        txtID = New TextBox()
        lblCode = New Label()
        txtCode = New TextBox()
        lblNameAr = New Label()
        txtNameAr = New TextBox()
        lblNameEn = New Label()
        txtNameEn = New TextBox()
        chkActive = New CheckBox()
        lblStartTime = New Label()
        dtpStartTime = New DateTimePicker()
        lblEndTime = New Label()
        dtpEndTime = New DateTimePicker()
        chkOvertime = New CheckBox()
        btnSave = New Button()
        btnCancel = New Button()
        btnAdd = New Button()
        btnDelete = New Button()
        btnRefresh = New Button()
        lblFormMode = New Label()
        dgvShifts = New DataGridView()
        pnlFields.SuspendLayout()
        CType(dgvShifts, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlFields
        ' 
        pnlFields.BackColor = Color.White
        pnlFields.BorderStyle = BorderStyle.FixedSingle
        pnlFields.Controls.Add(lblID)
        pnlFields.Controls.Add(txtID)
        pnlFields.Controls.Add(lblCode)
        pnlFields.Controls.Add(txtCode)
        pnlFields.Controls.Add(lblNameAr)
        pnlFields.Controls.Add(txtNameAr)
        pnlFields.Controls.Add(lblNameEn)
        pnlFields.Controls.Add(txtNameEn)
        pnlFields.Controls.Add(chkActive)
        pnlFields.Controls.Add(lblStartTime)
        pnlFields.Controls.Add(dtpStartTime)
        pnlFields.Controls.Add(lblEndTime)
        pnlFields.Controls.Add(dtpEndTime)
        pnlFields.Controls.Add(chkOvertime)
        pnlFields.Controls.Add(btnSave)
        pnlFields.Controls.Add(btnCancel)
        pnlFields.Controls.Add(btnAdd)
        pnlFields.Controls.Add(btnDelete)
        pnlFields.Controls.Add(btnRefresh)
        pnlFields.Controls.Add(lblFormMode)
        pnlFields.Dock = DockStyle.Top
        pnlFields.Location = New Point(0, 0)
        pnlFields.Name = "pnlFields"
        pnlFields.Size = New Size(918, 120)
        pnlFields.TabIndex = 1
        ' 
        ' lblID
        ' 
        lblID.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblID.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblID.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblID.Location = New Point(860, 10)
        lblID.Name = "lblID"
        lblID.Size = New Size(45, 16)
        lblID.TabIndex = 0
        lblID.Text = "الرقم"
        lblID.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtID
        ' 
        txtID.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtID.BackColor = Color.FromArgb(CByte(240), CByte(240), CByte(240))
        txtID.BorderStyle = BorderStyle.FixedSingle
        txtID.Font = New Font("Segoe UI", 9F)
        txtID.Location = New Point(860, 28)
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.RightToLeft = RightToLeft.No
        txtID.Size = New Size(45, 23)
        txtID.TabIndex = 1
        txtID.TabStop = False
        txtID.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblCode
        ' 
        lblCode.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCode.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCode.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblCode.Location = New Point(718, 10)
        lblCode.Name = "lblCode"
        lblCode.Size = New Size(130, 16)
        lblCode.TabIndex = 2
        lblCode.Text = "كود الوردية *"
        lblCode.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtCode
        ' 
        txtCode.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCode.BackColor = Color.White
        txtCode.BorderStyle = BorderStyle.FixedSingle
        txtCode.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        txtCode.Location = New Point(718, 28)
        txtCode.Name = "txtCode"
        txtCode.RightToLeft = RightToLeft.Yes
        txtCode.Size = New Size(130, 25)
        txtCode.TabIndex = 3
        txtCode.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblNameAr
        ' 
        lblNameAr.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblNameAr.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblNameAr.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblNameAr.Location = New Point(533, 10)
        lblNameAr.Name = "lblNameAr"
        lblNameAr.Size = New Size(175, 16)
        lblNameAr.TabIndex = 4
        lblNameAr.Text = "الاسم (عربي) *"
        lblNameAr.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtNameAr
        ' 
        txtNameAr.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtNameAr.BackColor = Color.White
        txtNameAr.BorderStyle = BorderStyle.FixedSingle
        txtNameAr.Font = New Font("Segoe UI", 9F)
        txtNameAr.Location = New Point(533, 28)
        txtNameAr.Name = "txtNameAr"
        txtNameAr.RightToLeft = RightToLeft.Yes
        txtNameAr.Size = New Size(175, 23)
        txtNameAr.TabIndex = 5
        txtNameAr.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblNameEn
        ' 
        lblNameEn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblNameEn.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblNameEn.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblNameEn.Location = New Point(348, 10)
        lblNameEn.Name = "lblNameEn"
        lblNameEn.Size = New Size(175, 16)
        lblNameEn.TabIndex = 6
        lblNameEn.Text = "الاسم (إنجليزي) *"
        lblNameEn.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtNameEn
        ' 
        txtNameEn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtNameEn.BackColor = Color.White
        txtNameEn.BorderStyle = BorderStyle.FixedSingle
        txtNameEn.Font = New Font("Segoe UI", 9F)
        txtNameEn.Location = New Point(348, 28)
        txtNameEn.Name = "txtNameEn"
        txtNameEn.RightToLeft = RightToLeft.Yes
        txtNameEn.Size = New Size(175, 23)
        txtNameEn.TabIndex = 7
        txtNameEn.TextAlign = HorizontalAlignment.Right
        ' 
        ' chkActive
        ' 
        chkActive.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chkActive.CheckAlign = ContentAlignment.MiddleRight
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        chkActive.ForeColor = Color.FromArgb(CByte(39), CByte(174), CByte(96))
        chkActive.Location = New Point(155, 55)
        chkActive.Name = "chkActive"
        chkActive.Size = New Size(75, 24)
        chkActive.TabIndex = 8
        chkActive.Text = "نشط"
        chkActive.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblStartTime
        ' 
        lblStartTime.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblStartTime.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblStartTime.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblStartTime.Location = New Point(860, 60)
        lblStartTime.Name = "lblStartTime"
        lblStartTime.Size = New Size(45, 16)
        lblStartTime.TabIndex = 9
        lblStartTime.Text = "البداية"
        lblStartTime.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dtpStartTime
        ' 
        dtpStartTime.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        dtpStartTime.Font = New Font("Segoe UI", 9F)
        dtpStartTime.Format = DateTimePickerFormat.Time
        dtpStartTime.Location = New Point(93, 29)
        dtpStartTime.Name = "dtpStartTime"
        dtpStartTime.ShowUpDown = True
        dtpStartTime.Size = New Size(110, 23)
        dtpStartTime.TabIndex = 10
        ' 
        ' lblEndTime
        ' 
        lblEndTime.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblEndTime.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblEndTime.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblEndTime.Location = New Point(738, 60)
        lblEndTime.Name = "lblEndTime"
        lblEndTime.Size = New Size(50, 16)
        lblEndTime.TabIndex = 11
        lblEndTime.Text = "النهاية"
        lblEndTime.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dtpEndTime
        ' 
        dtpEndTime.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        dtpEndTime.Font = New Font("Segoe UI", 9F)
        dtpEndTime.Format = DateTimePickerFormat.Time
        dtpEndTime.Location = New Point(218, 28)
        dtpEndTime.Name = "dtpEndTime"
        dtpEndTime.ShowUpDown = True
        dtpEndTime.Size = New Size(110, 23)
        dtpEndTime.TabIndex = 12
        ' 
        ' chkOvertime
        ' 
        chkOvertime.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chkOvertime.CheckAlign = ContentAlignment.MiddleRight
        chkOvertime.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        chkOvertime.ForeColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        chkOvertime.Location = New Point(155, 89)
        chkOvertime.Name = "chkOvertime"
        chkOvertime.Size = New Size(100, 24)
        chkOvertime.TabIndex = 13
        chkOvertime.Text = "أوفرتيم"
        chkOvertime.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(827, 89)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(75, 23)
        btnSave.TabIndex = 14
        btnSave.Text = "حفظ"
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(741, 89)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(75, 23)
        btnCancel.TabIndex = 15
        btnCancel.Text = "إلغاء"
        ' 
        ' btnAdd
        ' 
        btnAdd.Location = New Point(655, 89)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(75, 23)
        btnAdd.TabIndex = 16
        btnAdd.Text = "اضافة "
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(569, 89)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(75, 23)
        btnDelete.TabIndex = 17
        btnDelete.Text = "حذف"
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Location = New Point(483, 89)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(75, 23)
        btnRefresh.TabIndex = 18
        btnRefresh.Text = "تحديث"
        ' 
        ' lblFormMode
        ' 
        lblFormMode.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblFormMode.BackColor = Color.FromArgb(CByte(39), CByte(174), CByte(96))
        lblFormMode.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblFormMode.ForeColor = Color.White
        lblFormMode.Location = New Point(2, 82)
        lblFormMode.Name = "lblFormMode"
        lblFormMode.Size = New Size(147, 30)
        lblFormMode.TabIndex = 19
        lblFormMode.Text = "➕  وضع الإضافة"
        lblFormMode.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' dgvShifts
        ' 
        dgvShifts.AllowUserToAddRows = False
        dgvShifts.AllowUserToDeleteRows = False
        dgvShifts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvShifts.BackgroundColor = Color.White
        dgvShifts.BorderStyle = BorderStyle.None
        dgvShifts.ColumnHeadersHeight = 40
        dgvShifts.Dock = DockStyle.Fill
        dgvShifts.Font = New Font("Segoe UI", 9F)
        dgvShifts.Location = New Point(0, 120)
        dgvShifts.MultiSelect = False
        dgvShifts.Name = "dgvShifts"
        dgvShifts.ReadOnly = True
        dgvShifts.RightToLeft = RightToLeft.Yes
        dgvShifts.RowHeadersVisible = False
        dgvShifts.RowTemplate.Height = 34
        dgvShifts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvShifts.Size = New Size(918, 480)
        dgvShifts.TabIndex = 0
        ' 
        ' ProShiftsUC
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(247), CByte(250))
        Controls.Add(dgvShifts)
        Controls.Add(pnlFields)
        Font = New Font("Segoe UI", 9F)
        Name = "ProShiftsUC"
        RightToLeft = RightToLeft.Yes
        Size = New Size(918, 600)
        pnlFields.ResumeLayout(False)
        pnlFields.PerformLayout()
        CType(dgvShifts, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Helpers
    ' ═══════════════════════════════════════════════════════
    Private Sub SetupButton(btn As Button, bgColor As Color, text As String,
                            x As Integer, y As Integer, w As Integer, h As Integer, tabIdx As Integer)
        btn.BackColor = bgColor
        btn.Cursor = Cursors.Hand
        btn.FlatAppearance.BorderSize = 0
        btn.FlatStyle = FlatStyle.Flat
        btn.Font = UITheme.FontBold
        btn.ForeColor = UITheme.TextWhite
        btn.Location = New Point(x, y)
        btn.Size = New Size(w, h)
        btn.RightToLeft = RightToLeft.Yes
        btn.Text = text
        btn.TextAlign = ContentAlignment.MiddleRight
        btn.UseVisualStyleBackColor = False
        btn.TabIndex = tabIdx
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  التعريفات
    ' ═══════════════════════════════════════════════════════
    Friend WithEvents pnlFields As System.Windows.Forms.Panel
    Friend WithEvents lblFormMode As System.Windows.Forms.Label
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblNameAr As System.Windows.Forms.Label
    Friend WithEvents txtNameAr As System.Windows.Forms.TextBox
    Friend WithEvents lblNameEn As System.Windows.Forms.Label
    Friend WithEvents txtNameEn As System.Windows.Forms.TextBox
    Friend WithEvents lblStartTime As System.Windows.Forms.Label
    Friend WithEvents dtpStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEndTime As System.Windows.Forms.Label
    Friend WithEvents dtpEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkOvertime As System.Windows.Forms.CheckBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dgvShifts As System.Windows.Forms.DataGridView
End Class