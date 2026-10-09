Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProUnitsUC
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
        Me.lblNameAr = New System.Windows.Forms.Label()
        Me.txtNameAr = New System.Windows.Forms.TextBox()
        Me.lblNameEn = New System.Windows.Forms.Label()
        Me.txtNameEn = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvUnits = New System.Windows.Forms.DataGridView()

        Me.pnlFields.SuspendLayout()
        CType(Me.dgvUnits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' ═══════════════════════════════════════════════════════
        '  لوحة الحقول
        ' ═══════════════════════════════════════════════════════
        Me.pnlFields.BackColor = UITheme.Surface
        Me.pnlFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFields.Location = New System.Drawing.Point(0, 0)
        Me.pnlFields.Name = "pnlFields"
        Me.pnlFields.Size = New System.Drawing.Size(800, 110)

        ' [الرقم]
        SetupLabel(Me.lblID, "الرقم", 736, 8, 45)
        SetupTextBox(Me.txtID, 736, 26, 45, True, True)

        ' [الاسم عربي]
        SetupLabel(Me.lblNameAr, "الاسم (عربي) *", 466, 8, 260)
        SetupTextBox(Me.txtNameAr, 466, 26, 260)

        ' [الاسم إنجليزي]
        SetupLabel(Me.lblNameEn, "الاسم (إنجليزي) *", 196, 8, 260)
        SetupTextBox(Me.txtNameEn, 196, 26, 260)

        ' [الأزرار]
        SetupButton(Me.btnSave, UITheme.Success, "💾  حفظ", 15, 62, 100, 32, 0)
        SetupButton(Me.btnCancel, UITheme.TextMuted, "✖  إلغاء", 120, 62, 100, 32, 1)
        SetupButton(Me.btnAdd, UITheme.Success, "➕  جديد", 225, 62, 90, 32, 2)
        SetupButton(Me.btnDelete, UITheme.Danger, "🗑  حذف", 320, 62, 85, 32, 3)
        SetupButton(Me.btnRefresh, UITheme.TextMuted, "🔄  تحديث", 410, 62, 100, 32, 4)

        SetupBadge(Me.lblFormMode, "➕  وضع الإضافة", 515, 61, 271, 34, UITheme.Success)

        Me.pnlFields.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblID, Me.txtID,
            Me.lblNameAr, Me.txtNameAr,
            Me.lblNameEn, Me.txtNameEn,
            Me.btnSave, Me.btnCancel,
            Me.btnAdd, Me.btnDelete, Me.btnRefresh,
            Me.lblFormMode
        })

        ' ═══════════════════════════════════════════════════════
        '  الجدول
        ' ═══════════════════════════════════════════════════════
        Me.dgvUnits.AllowUserToAddRows = False
        Me.dgvUnits.AllowUserToDeleteRows = False
        Me.dgvUnits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUnits.BackgroundColor = UITheme.Surface
        Me.dgvUnits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUnits.ColumnHeadersHeight = 40
        Me.dgvUnits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUnits.Font = UITheme.FontNormal
        Me.dgvUnits.Location = New System.Drawing.Point(0, 110)
        Me.dgvUnits.MultiSelect = False
        Me.dgvUnits.Name = "dgvUnits"
        Me.dgvUnits.ReadOnly = True
        Me.dgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvUnits.RowHeadersVisible = False
        Me.dgvUnits.RowTemplate.Height = 34
        Me.dgvUnits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUnits.Size = New System.Drawing.Size(800, 490)

        ' ═══════════════════════════════════════════════════════
        '  ProUnitsUC
        ' ═══════════════════════════════════════════════════════
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = UITheme.Background
        Me.Controls.Add(Me.dgvUnits)
        Me.Controls.Add(Me.pnlFields)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "ProUnitsUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(800, 600)

        Me.pnlFields.ResumeLayout(False)
        Me.pnlFields.PerformLayout()
        CType(Me.dgvUnits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Helpers
    ' ═══════════════════════════════════════════════════════
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

    ' ═══════════════════════════════════════════════════════
    '  التعريفات
    ' ═══════════════════════════════════════════════════════
    Friend WithEvents pnlFields As System.Windows.Forms.Panel
    Friend WithEvents lblFormMode As System.Windows.Forms.Label
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lblNameAr As System.Windows.Forms.Label
    Friend WithEvents txtNameAr As System.Windows.Forms.TextBox
    Friend WithEvents lblNameEn As System.Windows.Forms.Label
    Friend WithEvents txtNameEn As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dgvUnits As System.Windows.Forms.DataGridView
End Class