Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProWarehouseMasterUC
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
        Me.pnlFields = New System.Windows.Forms.Panel()
        Me.lblFormMode = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.lblNameAr = New System.Windows.Forms.Label()
        Me.txtNameAr = New System.Windows.Forms.TextBox()
        Me.lblNameEn = New System.Windows.Forms.Label()
        Me.txtNameEn = New System.Windows.Forms.TextBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.chkDefault = New System.Windows.Forms.CheckBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvWarehouses = New System.Windows.Forms.DataGridView()

        Me.pnlFields.SuspendLayout()
        CType(Me.dgvWarehouses, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' ═══════════════════════════════════════════════════════
        '  لوحة الحقول
        ' ═══════════════════════════════════════════════════════
        Me.pnlFields.BackColor = UITheme.Surface
        Me.pnlFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFields.Location = New System.Drawing.Point(0, 0)
        Me.pnlFields.Name = "pnlFields"
        Me.pnlFields.Size = New System.Drawing.Size(1200, 110)

        SetupLabel(Me.lblID, "الرقم", 1135, 8, 45)
        SetupTextBox(Me.txtID, 1135, 26, 45, True, True)

        SetupLabel(Me.lblCode, "كود المخزن *", 975, 8, 155)
        SetupTextBox(Me.txtCode, 975, 26, 155)

        SetupLabel(Me.lblNameAr, "الاسم (عربي) *", 730, 8, 240)
        SetupTextBox(Me.txtNameAr, 730, 26, 240)

        SetupLabel(Me.lblNameEn, "الاسم (إنجليزي)", 485, 8, 240)
        SetupTextBox(Me.txtNameEn, 485, 26, 240)

        SetupLabel(Me.lblType, "نوع المخزن *", 290, 8, 190)
        SetupComboBox(Me.cboType, 290, 26, 190)

        SetupCheckBox(Me.chkDefault, "افتراضي", 200, 27, 85)
        SetupCheckBox(Me.chkActive, "نشط", 115, 27, 80)

        SetupButton(Me.btnSave, UITheme.Success, "💾  حفظ", 15, 62, 95, 32, 0)
        SetupButton(Me.btnCancel, UITheme.TextMuted, "✖  إلغاء", 115, 62, 95, 32, 1)
        SetupButton(Me.btnNew, UITheme.Success, "➕  جديد", 215, 62, 90, 32, 2)
        SetupButton(Me.btnDelete, UITheme.Danger, "🗑  حذف", 310, 62, 85, 32, 3)
        SetupButton(Me.btnRefresh, UITheme.TextMuted, "🔄  تحديث", 400, 62, 95, 32, 4)

        SetupBadge(Me.lblFormMode, "➕  وضع الإضافة", 700, 60, 480, 38, UITheme.Success)

        Me.pnlFields.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblID, Me.txtID,
            Me.lblCode, Me.txtCode,
            Me.lblNameAr, Me.txtNameAr,
            Me.lblNameEn, Me.txtNameEn,
            Me.lblType, Me.cboType,
            Me.chkDefault, Me.chkActive,
            Me.btnSave, Me.btnCancel, Me.btnNew, Me.btnDelete, Me.btnRefresh,
            Me.lblFormMode
        })

        ' ═══════════════════════════════════════════════════════
        '  الجدول
        ' ═══════════════════════════════════════════════════════
        Me.dgvWarehouses.AllowUserToAddRows = False
        Me.dgvWarehouses.AllowUserToDeleteRows = False
        Me.dgvWarehouses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvWarehouses.BackgroundColor = UITheme.Surface
        Me.dgvWarehouses.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvWarehouses.ColumnHeadersHeight = 40
        Me.dgvWarehouses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvWarehouses.Font = UITheme.FontNormal
        Me.dgvWarehouses.Location = New System.Drawing.Point(0, 110)
        Me.dgvWarehouses.MultiSelect = False
        Me.dgvWarehouses.Name = "dgvWarehouses"
        Me.dgvWarehouses.ReadOnly = True
        Me.dgvWarehouses.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvWarehouses.RowHeadersVisible = False
        Me.dgvWarehouses.RowTemplate.Height = 32
        Me.dgvWarehouses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvWarehouses.Size = New System.Drawing.Size(1200, 490)

        ' ═══════════════════════════════════════════════════════
        '  ProWarehouseMasterUC
        ' ═══════════════════════════════════════════════════════
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = UITheme.Background
        Me.Controls.Add(Me.dgvWarehouses)
        Me.Controls.Add(Me.pnlFields)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "ProWarehouseMasterUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(1200, 600)

        Me.pnlFields.ResumeLayout(False)
        Me.pnlFields.PerformLayout()
        CType(Me.dgvWarehouses, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    ' ═══ Helpers ═══
    Private Sub SetupLabel(lbl As Label, text As String, x As Integer, y As Integer, w As Integer)
        lbl.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        lbl.AutoSize = False
        lbl.Font = UITheme.FontBold
        lbl.ForeColor = UITheme.TextPrimary
        lbl.Location = New Point(x, y)
        lbl.Size = New Size(w, 16)
        lbl.RightToLeft = RightToLeft.Yes
        lbl.Text = text
        lbl.TextAlign = ContentAlignment.MiddleRight
    End Sub

    Private Sub SetupTextBox(txt As TextBox, x As Integer, y As Integer, w As Integer,
                             Optional isReadOnly As Boolean = False,
                             Optional centered As Boolean = False)
        txt.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        txt.BackColor = If(isReadOnly, Color.FromArgb(240, 240, 240), UITheme.Surface)
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.Font = UITheme.FontNormal
        txt.Location = New Point(x, y)
        txt.Size = New Size(w, 24)
        txt.ReadOnly = isReadOnly
        If centered Then
            txt.RightToLeft = RightToLeft.No
            txt.TextAlign = HorizontalAlignment.Center
        Else
            txt.RightToLeft = RightToLeft.Yes
            txt.TextAlign = HorizontalAlignment.Right
        End If
        If isReadOnly Then txt.TabStop = False
    End Sub

    Private Sub SetupComboBox(cbo As ComboBox, x As Integer, y As Integer, w As Integer)
        cbo.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        cbo.BackColor = UITheme.Surface
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        cbo.FlatStyle = FlatStyle.Standard
        cbo.Font = UITheme.FontNormal
        cbo.FormattingEnabled = True
        cbo.Location = New Point(x, y)
        cbo.Size = New Size(w, 24)
        cbo.RightToLeft = RightToLeft.Yes
    End Sub

    Private Sub SetupCheckBox(chk As CheckBox, text As String, x As Integer, y As Integer, w As Integer)
        chk.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        chk.AutoSize = False
        chk.CheckAlign = ContentAlignment.MiddleRight
        chk.Font = UITheme.FontBold
        chk.ForeColor = UITheme.Success
        chk.Location = New Point(x, y)
        chk.Size = New Size(w, 24)
        chk.RightToLeft = RightToLeft.Yes
        chk.Text = text
        chk.TextAlign = ContentAlignment.MiddleRight
        chk.UseVisualStyleBackColor = True
    End Sub

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

    Private Sub SetupBadge(lbl As Label, text As String, x As Integer, y As Integer,
                           w As Integer, h As Integer, bgColor As Color)
        lbl.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        lbl.AutoSize = False
        lbl.BackColor = bgColor
        lbl.Font = UITheme.FontTitle
        lbl.ForeColor = UITheme.TextWhite
        lbl.Location = New Point(x, y)
        lbl.Size = New Size(w, h)
        lbl.RightToLeft = RightToLeft.Yes
        lbl.Text = text
        lbl.TextAlign = ContentAlignment.MiddleCenter
    End Sub

    ' ═══ التعريفات ═══
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
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents chkDefault As System.Windows.Forms.CheckBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dgvWarehouses As System.Windows.Forms.DataGridView
End Class