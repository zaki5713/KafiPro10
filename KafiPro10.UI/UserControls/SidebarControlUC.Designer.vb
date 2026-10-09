<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SidebarControlUC
    Inherits UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        flpButtons = New FlowLayoutPanel()
        pnlLogo = New Panel()
        lblTitle = New Label()
        pnlSeparator = New Panel()
        btnDashboard = New Button()
        btnProduction = New Button()
        btnQuality = New Button()
        btnWarehouses = New Button()
        btnFinance = New Button()
        btnSales = New Button()
        btnTransport = New Button()
        btnSettings = New Button()
        btnLogout = New Button()
        btnToggle = New Button()
        flpButtons.SuspendLayout()
        pnlLogo.SuspendLayout()
        SuspendLayout()
        ' 
        ' flpButtons
        ' 
        flpButtons.Controls.Add(pnlLogo)
        flpButtons.Controls.Add(pnlSeparator)
        flpButtons.Controls.Add(btnDashboard)
        flpButtons.Controls.Add(btnProduction)
        flpButtons.Controls.Add(btnQuality)
        flpButtons.Controls.Add(btnWarehouses)
        flpButtons.Controls.Add(btnFinance)
        flpButtons.Controls.Add(btnSales)
        flpButtons.Controls.Add(btnTransport)
        flpButtons.Controls.Add(btnSettings)
        flpButtons.Controls.Add(btnLogout)
        flpButtons.Controls.Add(btnToggle)
        flpButtons.Dock = DockStyle.Fill
        flpButtons.FlowDirection = FlowDirection.TopDown
        flpButtons.Location = New Point(0, 0)
        flpButtons.Margin = New Padding(0)
        flpButtons.Name = "flpButtons"
        flpButtons.Padding = New Padding(10, 10, 10, 10)
        flpButtons.Size = New Size(200, 589)
        flpButtons.TabIndex = 0
        flpButtons.WrapContents = False
        ' 
        ' pnlLogo
        ' 
        pnlLogo.Controls.Add(lblTitle)
        pnlLogo.Location = New Point(13, 13)
        pnlLogo.Name = "pnlLogo"
        pnlLogo.Size = New Size(174, 50)
        pnlLogo.TabIndex = 0
        ' 
        ' lblTitle
        ' 
        lblTitle.Dock = DockStyle.Fill
        lblTitle.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.Wheat
        lblTitle.Location = New Point(0, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(174, 50)
        lblTitle.TabIndex = 0
        lblTitle.Text = "System Vienna ERP"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlSeparator
        ' 
        pnlSeparator.BackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        pnlSeparator.Location = New Point(13, 69)
        pnlSeparator.Name = "pnlSeparator"
        pnlSeparator.Size = New Size(174, 2)
        pnlSeparator.TabIndex = 1
        ' 
        ' btnDashboard
        ' 
        btnDashboard.BackColor = Color.Transparent
        btnDashboard.FlatAppearance.BorderSize = 0
        btnDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnDashboard.FlatStyle = FlatStyle.Flat
        btnDashboard.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnDashboard.ForeColor = Color.Wheat
        btnDashboard.ImageAlign = ContentAlignment.MiddleLeft
        btnDashboard.Location = New Point(13, 77)
        btnDashboard.Margin = New Padding(3, 3, 3, 5)
        btnDashboard.Name = "btnDashboard"
        btnDashboard.Padding = New Padding(35, 0, 0, 0)
        btnDashboard.Size = New Size(174, 40)
        btnDashboard.TabIndex = 2
        btnDashboard.Text = "داشـبورد"
        btnDashboard.TextAlign = ContentAlignment.MiddleLeft
        btnDashboard.UseVisualStyleBackColor = False
        ' 
        ' btnProduction
        ' 
        btnProduction.BackColor = Color.Transparent
        btnProduction.FlatAppearance.BorderSize = 0
        btnProduction.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnProduction.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnProduction.FlatStyle = FlatStyle.Flat
        btnProduction.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnProduction.ForeColor = Color.White
        btnProduction.ImageAlign = ContentAlignment.MiddleLeft
        btnProduction.Location = New Point(13, 125)
        btnProduction.Margin = New Padding(3, 3, 3, 5)
        btnProduction.Name = "btnProduction"
        btnProduction.Padding = New Padding(35, 0, 0, 0)
        btnProduction.Size = New Size(174, 40)
        btnProduction.TabIndex = 3
        btnProduction.Text = "الإنتاج"
        btnProduction.TextAlign = ContentAlignment.MiddleLeft
        btnProduction.UseVisualStyleBackColor = False
        ' 
        ' btnQuality
        ' 
        btnQuality.BackColor = Color.Transparent
        btnQuality.FlatAppearance.BorderSize = 0
        btnQuality.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnQuality.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnQuality.FlatStyle = FlatStyle.Flat
        btnQuality.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnQuality.ForeColor = Color.White
        btnQuality.ImageAlign = ContentAlignment.MiddleLeft
        btnQuality.Location = New Point(13, 173)
        btnQuality.Margin = New Padding(3, 3, 3, 5)
        btnQuality.Name = "btnQuality"
        btnQuality.Padding = New Padding(35, 0, 0, 0)
        btnQuality.Size = New Size(174, 40)
        btnQuality.TabIndex = 4
        btnQuality.Text = "الجودة"
        btnQuality.TextAlign = ContentAlignment.MiddleLeft
        btnQuality.UseVisualStyleBackColor = False
        ' 
        ' btnWarehouses
        ' 
        btnWarehouses.BackColor = Color.Transparent
        btnWarehouses.FlatAppearance.BorderSize = 0
        btnWarehouses.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnWarehouses.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnWarehouses.FlatStyle = FlatStyle.Flat
        btnWarehouses.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnWarehouses.ForeColor = Color.White
        btnWarehouses.ImageAlign = ContentAlignment.MiddleLeft
        btnWarehouses.Location = New Point(13, 221)
        btnWarehouses.Margin = New Padding(3, 3, 3, 5)
        btnWarehouses.Name = "btnWarehouses"
        btnWarehouses.Padding = New Padding(35, 0, 0, 0)
        btnWarehouses.Size = New Size(174, 40)
        btnWarehouses.TabIndex = 5
        btnWarehouses.Text = "المخازن"
        btnWarehouses.TextAlign = ContentAlignment.MiddleLeft
        btnWarehouses.UseVisualStyleBackColor = False
        ' 
        ' btnFinance
        ' 
        btnFinance.BackColor = Color.Transparent
        btnFinance.FlatAppearance.BorderSize = 0
        btnFinance.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnFinance.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnFinance.FlatStyle = FlatStyle.Flat
        btnFinance.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnFinance.ForeColor = Color.White
        btnFinance.ImageAlign = ContentAlignment.MiddleLeft
        btnFinance.Location = New Point(13, 269)
        btnFinance.Margin = New Padding(3, 3, 3, 5)
        btnFinance.Name = "btnFinance"
        btnFinance.Padding = New Padding(35, 0, 0, 0)
        btnFinance.Size = New Size(174, 40)
        btnFinance.TabIndex = 6
        btnFinance.Text = "المالية"
        btnFinance.TextAlign = ContentAlignment.MiddleLeft
        btnFinance.UseVisualStyleBackColor = False
        ' 
        ' btnSales
        ' 
        btnSales.BackColor = Color.Transparent
        btnSales.FlatAppearance.BorderSize = 0
        btnSales.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnSales.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnSales.FlatStyle = FlatStyle.Flat
        btnSales.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnSales.ForeColor = Color.White
        btnSales.ImageAlign = ContentAlignment.MiddleLeft
        btnSales.Location = New Point(13, 317)
        btnSales.Margin = New Padding(3, 3, 3, 5)
        btnSales.Name = "btnSales"
        btnSales.Padding = New Padding(35, 0, 0, 0)
        btnSales.Size = New Size(174, 40)
        btnSales.TabIndex = 7
        btnSales.Text = "المبيعات"
        btnSales.TextAlign = ContentAlignment.MiddleLeft
        btnSales.UseVisualStyleBackColor = False
        ' 
        ' btnTransport
        ' 
        btnTransport.BackColor = Color.Transparent
        btnTransport.FlatAppearance.BorderSize = 0
        btnTransport.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnTransport.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnTransport.FlatStyle = FlatStyle.Flat
        btnTransport.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnTransport.ForeColor = Color.White
        btnTransport.ImageAlign = ContentAlignment.MiddleLeft
        btnTransport.Location = New Point(13, 365)
        btnTransport.Margin = New Padding(3, 3, 3, 5)
        btnTransport.Name = "btnTransport"
        btnTransport.Padding = New Padding(35, 0, 0, 0)
        btnTransport.Size = New Size(174, 40)
        btnTransport.TabIndex = 8
        btnTransport.Text = "النقل"
        btnTransport.TextAlign = ContentAlignment.MiddleLeft
        btnTransport.UseVisualStyleBackColor = False
        ' 
        ' btnSettings
        ' 
        btnSettings.BackColor = Color.Transparent
        btnSettings.FlatAppearance.BorderSize = 0
        btnSettings.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnSettings.FlatStyle = FlatStyle.Flat
        btnSettings.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnSettings.ForeColor = Color.White
        btnSettings.ImageAlign = ContentAlignment.MiddleLeft
        btnSettings.Location = New Point(13, 413)
        btnSettings.Margin = New Padding(3, 3, 3, 5)
        btnSettings.Name = "btnSettings"
        btnSettings.Padding = New Padding(35, 0, 0, 0)
        btnSettings.Size = New Size(174, 40)
        btnSettings.TabIndex = 9
        btnSettings.Text = "الإعدادات"
        btnSettings.TextAlign = ContentAlignment.MiddleLeft
        btnSettings.UseVisualStyleBackColor = False
        ' 
        ' btnLogout
        ' 
        btnLogout.BackColor = Color.Transparent
        btnLogout.FlatAppearance.BorderSize = 0
        btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnLogout.FlatStyle = FlatStyle.Flat
        btnLogout.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnLogout.ForeColor = Color.White
        btnLogout.ImageAlign = ContentAlignment.MiddleLeft
        btnLogout.Location = New Point(13, 461)
        btnLogout.Margin = New Padding(3, 3, 3, 5)
        btnLogout.Name = "btnLogout"
        btnLogout.Padding = New Padding(35, 0, 0, 0)
        btnLogout.Size = New Size(174, 40)
        btnLogout.TabIndex = 10
        btnLogout.Text = "تسجيل خروج"
        btnLogout.TextAlign = ContentAlignment.MiddleLeft
        btnLogout.UseVisualStyleBackColor = False
        ' 
        ' btnToggle
        ' 
        btnToggle.BackColor = Color.Transparent
        btnToggle.FlatAppearance.BorderSize = 0
        btnToggle.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        btnToggle.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnToggle.FlatStyle = FlatStyle.Flat
        btnToggle.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        btnToggle.ForeColor = Color.White
        btnToggle.ImageAlign = ContentAlignment.MiddleLeft
        btnToggle.Location = New Point(13, 509)
        btnToggle.Margin = New Padding(3, 3, 3, 5)
        btnToggle.Name = "btnToggle"
        btnToggle.Padding = New Padding(35, 0, 0, 0)
        btnToggle.Size = New Size(174, 40)
        btnToggle.TabIndex = 11
        btnToggle.Text = "طيّ ◀"
        btnToggle.TextAlign = ContentAlignment.MiddleLeft
        btnToggle.UseVisualStyleBackColor = False
        ' 
        ' SidebarControlUC
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(17), CByte(24), CByte(39))
        Controls.Add(flpButtons)
        Name = "SidebarControlUC"
        Size = New Size(200, 589)
        flpButtons.ResumeLayout(False)
        pnlLogo.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents flpButtons As FlowLayoutPanel
    Friend WithEvents pnlLogo As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlSeparator As Panel
    Friend WithEvents btnDashboard As Button
    Friend WithEvents btnProduction As Button
    Friend WithEvents btnQuality As Button
    Friend WithEvents btnWarehouses As Button
    Friend WithEvents btnFinance As Button
    Friend WithEvents btnSales As Button
    Friend WithEvents btnTransport As Button
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnToggle As Button

End Class
