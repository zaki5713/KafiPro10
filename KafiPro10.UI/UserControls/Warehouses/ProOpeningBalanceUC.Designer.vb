Imports System.Drawing
Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProOpeningBalanceUC
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

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblDate As Label
    Friend WithEvents dtpOpenDate As DateTimePicker
    Friend WithEvents lblWH As Label
    Friend WithEvents cboWarehouse As ComboBox
    Friend WithEvents btnLoad As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents pnlGrid As Panel
    Friend WithEvents dgvItems As DataGridView
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents lblTotal As Label

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpOpenDate = New System.Windows.Forms.DateTimePicker()
        Me.lblWH = New System.Windows.Forms.Label()
        Me.cboWarehouse = New System.Windows.Forms.ComboBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dgvItems = New System.Windows.Forms.DataGridView()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.lblTotal = New System.Windows.Forms.Label()

        Me.pnlHeader.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()

        ' ─── pnlHeader ───
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(52, 73, 94)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1400, 45)

        ' ─── lblTitle ───
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(0, 0, 20, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1400, 45)
        Me.lblTitle.Text = "📅  الأرصدة الافتتاحية"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight

        ' ─── pnlTop ───
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
        Me.pnlTop.Controls.Add(Me.lblDate)
        Me.pnlTop.Controls.Add(Me.dtpOpenDate)
        Me.pnlTop.Controls.Add(Me.lblWH)
        Me.pnlTop.Controls.Add(Me.cboWarehouse)
        Me.pnlTop.Controls.Add(Me.btnLoad)
        Me.pnlTop.Controls.Add(Me.btnClear)
        Me.pnlTop.Controls.Add(Me.btnSave)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 45)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1400, 65)

        ' ─── lblDate ───
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDate.Location = New System.Drawing.Point(900, 22)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(50, 15)
        Me.lblDate.Text = "التاريخ:"

        ' ─── dtpOpenDate ───
        Me.dtpOpenDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpOpenDate.Location = New System.Drawing.Point(700, 20)
        Me.dtpOpenDate.Name = "dtpOpenDate"
        Me.dtpOpenDate.Size = New System.Drawing.Size(140, 27)

        ' ─── lblWH ───
        Me.lblWH.AutoSize = True
        Me.lblWH.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblWH.Location = New System.Drawing.Point(450, 22)
        Me.lblWH.Name = "lblWH"
        Me.lblWH.Size = New System.Drawing.Size(50, 15)
        Me.lblWH.Text = "المخزن:"

        ' ─── cboWarehouse ───
        Me.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWarehouse.Location = New System.Drawing.Point(450, 20)
        Me.cboWarehouse.Name = "cboWarehouse"
        Me.cboWarehouse.Size = New System.Drawing.Size(240, 27)

        ' ─── btnLoad ───
        Me.btnLoad.BackColor = System.Drawing.Color.FromArgb(52, 152, 219)
        Me.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLoad.FlatAppearance.BorderSize = 0
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(350, 18)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(110, 32)
        Me.btnLoad.Text = "📥 تحميل"
        Me.btnLoad.UseVisualStyleBackColor = False

        ' ─── btnClear ───
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(241, 196, 15)
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(200, 18)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(110, 32)
        Me.btnClear.Text = "🧹 تفريغ"
        Me.btnClear.UseVisualStyleBackColor = False

        ' ─── btnSave ───
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113)
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(50, 18)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 32)
        Me.btnSave.Text = "💾 حفظ"
        Me.btnSave.UseVisualStyleBackColor = False

        ' ─── pnlGrid ───
        Me.pnlGrid.Controls.Add(Me.dgvItems)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 200)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlGrid.Size = New System.Drawing.Size(1400, 520)

        ' ─── dgvItems ───
        Me.dgvItems.BackgroundColor = System.Drawing.Color.White
        Me.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvItems.ColumnHeadersHeight = 38
        Me.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItems.EnableHeadersVisualStyles = False
        Me.dgvItems.Location = New System.Drawing.Point(8, 8)
        Me.dgvItems.Name = "dgvItems"
        Me.dgvItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvItems.RowTemplate.Height = 30
        Me.dgvItems.Size = New System.Drawing.Size(1384, 504)

        ' ─── pnlBottom ───
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
        Me.pnlBottom.Controls.Add(Me.lblTotal)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 630)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1400, 40)

        ' ─── lblTotal ───
        Me.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94)
        Me.lblTotal.Location = New System.Drawing.Point(0, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Padding = New System.Windows.Forms.Padding(0, 0, 20, 0)
        Me.lblTotal.Size = New System.Drawing.Size(1400, 40)
        Me.lblTotal.Text = "أصناف لها رصيد: 0  |  إجمالي الكميات: 0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight

        ' ─── ProOpeningBalanceUC ───
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "ProOpeningBalanceUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(1400, 670)

        Me.pnlHeader.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

End Class