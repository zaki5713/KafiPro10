Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProStockBalanceUC
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
        Me.lblWarehouse = New System.Windows.Forms.Label()
        Me.cboWarehouse = New System.Windows.Forms.ComboBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.dgvStock = New System.Windows.Forms.DataGridView()

        Me.pnlFilter.SuspendLayout()
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' ═══════════════════════════════════════════════════════
        '  لوحة الفلتر
        ' ═══════════════════════════════════════════════════════
        Me.pnlFilter.BackColor = UITheme.Surface
        Me.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(1200, 60)

        SetupLabel(Me.lblWarehouse, "المخزن:", 1000, 18, 80)
        SetupComboBox(Me.cboWarehouse, 850, 16, 145)

        SetupLabel(Me.lblSearch, "🔍 بحث:", 715, 18, 70)
        SetupTextBox(Me.txtSearch, 515, 16, 195)

        SetupButton(Me.btnSearch, UITheme.Primary, "🔍  بحث", 415, 16, 95, 30, 0)
        SetupButton(Me.btnClear, UITheme.TextMuted, "✖  مسح", 310, 16, 95, 30, 1)
        SetupButton(Me.btnRefresh, UITheme.Success, "🔄  تحديث", 200, 16, 100, 30, 2)

        Me.lblCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCount.AutoSize = False
        Me.lblCount.Font = UITheme.FontBold
        Me.lblCount.ForeColor = UITheme.Primary
        Me.lblCount.Location = New System.Drawing.Point(10, 18)
        Me.lblCount.Size = New System.Drawing.Size(180, 24)
        Me.lblCount.Text = "📋  الأرصدة: 0"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.pnlFilter.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblWarehouse, Me.cboWarehouse,
            Me.lblSearch, Me.txtSearch,
            Me.btnSearch, Me.btnClear, Me.btnRefresh,
            Me.lblCount
        })

        ' ═══════════════════════════════════════════════════════
        '  الجدول
        ' ═══════════════════════════════════════════════════════
        Me.dgvStock.AllowUserToAddRows = False
        Me.dgvStock.AllowUserToDeleteRows = False
        Me.dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvStock.BackgroundColor = UITheme.Surface
        Me.dgvStock.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvStock.ColumnHeadersHeight = 40
        Me.dgvStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvStock.Font = UITheme.FontNormal
        Me.dgvStock.Location = New System.Drawing.Point(0, 60)
        Me.dgvStock.MultiSelect = False
        Me.dgvStock.Name = "dgvStock"
        Me.dgvStock.ReadOnly = True
        Me.dgvStock.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvStock.RowHeadersVisible = False
        Me.dgvStock.RowTemplate.Height = 32
        Me.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvStock.Size = New System.Drawing.Size(1200, 540)

        ' ═══════════════════════════════════════════════════════
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = UITheme.Background
        Me.Controls.Add(Me.dgvStock)
        Me.Controls.Add(Me.pnlFilter)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "ProStockBalanceUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(1200, 600)

        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).EndInit()
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

    Private Sub SetupTextBox(txt As TextBox, x As Integer, y As Integer, w As Integer)
        txt.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        txt.BackColor = UITheme.Surface
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.Font = UITheme.FontNormal
        txt.Location = New Point(x, y)
        txt.Size = New Size(w, 26)
        txt.RightToLeft = RightToLeft.Yes
        txt.TextAlign = HorizontalAlignment.Right
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
    Friend WithEvents lblWarehouse As System.Windows.Forms.Label
    Friend WithEvents cboWarehouse As System.Windows.Forms.ComboBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents dgvStock As System.Windows.Forms.DataGridView
End Class