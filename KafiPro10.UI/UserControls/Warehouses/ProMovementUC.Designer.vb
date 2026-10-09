Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProMovementUC
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblWarehouse = New System.Windows.Forms.Label()
        Me.cboWarehouse = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.dgvMovements = New System.Windows.Forms.DataGridView()

        Me.pnlFilter.SuspendLayout()
        CType(Me.dgvMovements, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' ═══ لوحة الفلتر ═══
        Me.pnlFilter.BackColor = UITheme.Surface
        Me.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(1200, 60)

        SetupLabel(Me.lblFrom, "من:", 1100, 18, 40)
        SetupDateTime(Me.dtpFrom, 985, 16, 110)

        SetupLabel(Me.lblTo, "إلى:", 925, 18, 45)
        SetupDateTime(Me.dtpTo, 810, 16, 110)

        SetupLabel(Me.lblWarehouse, "المخزن:", 715, 18, 80)
        SetupComboBox(Me.cboWarehouse, 565, 16, 145)

        SetupLabel(Me.lblType, "نوع الحركة:", 440, 18, 100)
        SetupComboBox(Me.cboType, 250, 16, 185)

        SetupButton(Me.btnSearch, UITheme.Primary, "🔍  بحث", 150, 16, 95, 30, 0)
        SetupButton(Me.btnClear, UITheme.TextMuted, "✖  مسح", 45, 16, 95, 30, 1)

        Me.lblCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCount.AutoSize = False
        Me.lblCount.Font = UITheme.FontBold
        Me.lblCount.ForeColor = UITheme.Primary
        Me.lblCount.Location = New System.Drawing.Point(1180, 18)
        Me.lblCount.Size = New System.Drawing.Size(15, 24)
        Me.lblCount.Text = ""

        ' ═══ الجدول ═══
        Me.dgvMovements.AllowUserToAddRows = False
        Me.dgvMovements.AllowUserToDeleteRows = False
        Me.dgvMovements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMovements.BackgroundColor = UITheme.Surface
        Me.dgvMovements.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvMovements.ColumnHeadersHeight = 40
        Me.dgvMovements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovements.Font = UITheme.FontNormal
        Me.dgvMovements.Location = New System.Drawing.Point(0, 60)
        Me.dgvMovements.MultiSelect = False
        Me.dgvMovements.Name = "dgvMovements"
        Me.dgvMovements.ReadOnly = True
        Me.dgvMovements.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvMovements.RowHeadersVisible = False
        Me.dgvMovements.RowTemplate.Height = 32
        Me.dgvMovements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovements.Size = New System.Drawing.Size(1200, 540)

        ' ═══ ProMovementUC ═══
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = UITheme.Background
        Me.Controls.Add(Me.dgvMovements)
        Me.Controls.Add(Me.pnlFilter)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "ProMovementUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(1200, 600)

        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        CType(Me.dgvMovements, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Private Sub SetupLabel(lbl As Label, text As String, x As Integer, y As Integer, w As Integer)
        lbl.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        lbl.AutoSize = False
        lbl.Font = UITheme.FontBold
        lbl.ForeColor = UITheme.TextPrimary
        lbl.Location = New Point(x, y)
        lbl.Size = New Size(w, 18)
        lbl.RightToLeft = RightToLeft.Yes
        lbl.Text = text
        lbl.TextAlign = ContentAlignment.MiddleRight
    End Sub

    Private Sub SetupComboBox(cbo As ComboBox, x As Integer, y As Integer, w As Integer)
        cbo.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        cbo.BackColor = UITheme.Surface
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        cbo.FlatStyle = FlatStyle.Standard
        cbo.Font = UITheme.FontNormal
        cbo.Location = New Point(x, y)
        cbo.Size = New Size(w, 26)
        cbo.RightToLeft = RightToLeft.Yes
    End Sub

    Private Sub SetupDateTime(dtp As DateTimePicker, x As Integer, y As Integer, w As Integer)
        dtp.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        dtp.Font = UITheme.FontNormal
        dtp.Format = DateTimePickerFormat.Short
        dtp.Location = New Point(x, y)
        dtp.Size = New Size(w, 26)
        dtp.RightToLeft = RightToLeft.Yes
        dtp.RightToLeftLayout = True
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

    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblWarehouse As System.Windows.Forms.Label
    Friend WithEvents cboWarehouse As System.Windows.Forms.ComboBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents dgvMovements As System.Windows.Forms.DataGridView
End Class