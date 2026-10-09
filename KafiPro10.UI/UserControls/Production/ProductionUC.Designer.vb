Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProductionUC
    Inherits BaseUC

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
        Me.tsSubNav = New System.Windows.Forms.ToolStrip()
        Me.btnMasterData = New System.Windows.Forms.Button()
        Me.btnDailyProduction = New System.Windows.Forms.Button()
        Me.btnFinalProduct = New System.Windows.Forms.Button()
        Me.btnWithdrawalOrder = New System.Windows.Forms.Button()
        Me.btnOrder = New System.Windows.Forms.Button()
        Me.btnDashboard = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()

        Me.hostMasterData = New System.Windows.Forms.ToolStripControlHost(Me.btnMasterData)
        Me.hostDailyProduction = New System.Windows.Forms.ToolStripControlHost(Me.btnDailyProduction)
        Me.hostFinalProduct = New System.Windows.Forms.ToolStripControlHost(Me.btnFinalProduct)
        Me.hostWithdrawalOrder = New System.Windows.Forms.ToolStripControlHost(Me.btnWithdrawalOrder)
        Me.hostOrder = New System.Windows.Forms.ToolStripControlHost(Me.btnOrder)
        Me.hostDashboard = New System.Windows.Forms.ToolStripControlHost(Me.btnDashboard)
        Me.hostReports = New System.Windows.Forms.ToolStripControlHost(Me.btnReports)

        Me.pnlModuleContent = New System.Windows.Forms.Panel()
        Me.tsSubNav.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsSubNav
        '
        Me.tsSubNav.BackColor = UITheme.ButtonNormal
        Me.tsSubNav.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsSubNav.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsSubNav.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tsSubNav.Dock = System.Windows.Forms.DockStyle.Top
        Me.tsSubNav.Padding = New System.Windows.Forms.Padding(6, 4, 6, 4)
        Me.tsSubNav.AutoSize = False
        Me.tsSubNav.Size = New System.Drawing.Size(800, 46)
        Me.tsSubNav.Name = "tsSubNav"
        Me.tsSubNav.TabIndex = 0

        Me.tsSubNav.Items.Add(Me.hostMasterData)
        Me.tsSubNav.Items.Add(Me.hostDailyProduction)
        Me.tsSubNav.Items.Add(Me.hostFinalProduct)
        Me.tsSubNav.Items.Add(Me.hostWithdrawalOrder)
        Me.tsSubNav.Items.Add(Me.hostOrder)
        Me.tsSubNav.Items.Add(Me.hostDashboard)
        Me.tsSubNav.Items.Add(Me.hostReports)
        '
        'btnMasterData (Active افتراضياً)
        '
        Me.btnMasterData.BackColor = UITheme.ActiveBackground
        Me.btnMasterData.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMasterData.FlatAppearance.BorderSize = 0
        Me.btnMasterData.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnMasterData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasterData.Font = UITheme.FontBold
        Me.btnMasterData.ForeColor = UITheme.ActiveText
        Me.btnMasterData.Name = "btnMasterData"
        Me.btnMasterData.Size = New System.Drawing.Size(120, 34)
        Me.btnMasterData.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnMasterData.Text = "البيانات الأساسية"
        Me.btnMasterData.UseVisualStyleBackColor = False
        '
        'btnDailyProduction
        '
        Me.btnDailyProduction.BackColor = UITheme.ButtonNormal
        Me.btnDailyProduction.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDailyProduction.FlatAppearance.BorderSize = 0
        Me.btnDailyProduction.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnDailyProduction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDailyProduction.Font = UITheme.FontBold
        Me.btnDailyProduction.ForeColor = UITheme.TextPrimary
        Me.btnDailyProduction.Name = "btnDailyProduction"
        Me.btnDailyProduction.Size = New System.Drawing.Size(120, 34)
        Me.btnDailyProduction.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnDailyProduction.Text = "الإنتاج اليومي"
        Me.btnDailyProduction.UseVisualStyleBackColor = False
        '
        'btnFinalProduct
        '
        Me.btnFinalProduct.BackColor = UITheme.ButtonNormal
        Me.btnFinalProduct.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFinalProduct.FlatAppearance.BorderSize = 0
        Me.btnFinalProduct.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnFinalProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinalProduct.Font = UITheme.FontBold
        Me.btnFinalProduct.ForeColor = UITheme.TextPrimary
        Me.btnFinalProduct.Name = "btnFinalProduct"
        Me.btnFinalProduct.Size = New System.Drawing.Size(120, 34)
        Me.btnFinalProduct.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnFinalProduct.Text = "المنتج النهائي"
        Me.btnFinalProduct.UseVisualStyleBackColor = False
        '
        'btnWithdrawalOrder
        '
        Me.btnWithdrawalOrder.BackColor = UITheme.ButtonNormal
        Me.btnWithdrawalOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnWithdrawalOrder.FlatAppearance.BorderSize = 0
        Me.btnWithdrawalOrder.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnWithdrawalOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWithdrawalOrder.Font = UITheme.FontBold
        Me.btnWithdrawalOrder.ForeColor = UITheme.TextPrimary
        Me.btnWithdrawalOrder.Name = "btnWithdrawalOrder"
        Me.btnWithdrawalOrder.Size = New System.Drawing.Size(120, 34)
        Me.btnWithdrawalOrder.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnWithdrawalOrder.Text = "طلب الصرف"
        Me.btnWithdrawalOrder.UseVisualStyleBackColor = False
        '
        'btnOrder
        '
        Me.btnOrder.BackColor = UITheme.ButtonNormal
        Me.btnOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOrder.FlatAppearance.BorderSize = 0
        Me.btnOrder.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrder.Font = UITheme.FontBold
        Me.btnOrder.ForeColor = UITheme.TextPrimary
        Me.btnOrder.Name = "btnOrder"
        Me.btnOrder.Size = New System.Drawing.Size(120, 34)
        Me.btnOrder.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnOrder.Text = "نواقص المبيعات"
        Me.btnOrder.UseVisualStyleBackColor = False
        '
        'btnDashboard
        '
        Me.btnDashboard.BackColor = UITheme.ButtonNormal
        Me.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDashboard.FlatAppearance.BorderSize = 0
        Me.btnDashboard.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDashboard.Font = UITheme.FontBold
        Me.btnDashboard.ForeColor = UITheme.TextPrimary
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(120, 34)
        Me.btnDashboard.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnDashboard.Text = "لوحة التحكم"
        Me.btnDashboard.UseVisualStyleBackColor = False
        '
        'btnReports  (جديد)
        '
        Me.btnReports.BackColor = UITheme.ButtonNormal
        Me.btnReports.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReports.FlatAppearance.BorderSize = 0
        Me.btnReports.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReports.Font = UITheme.FontBold
        Me.btnReports.ForeColor = UITheme.TextPrimary
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(120, 34)
        Me.btnReports.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnReports.Text = "التقارير"
        Me.btnReports.UseVisualStyleBackColor = False
        '
        'pnlModuleContent
        '
        Me.pnlModuleContent.BackColor = UITheme.Surface
        Me.pnlModuleContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlModuleContent.Location = New System.Drawing.Point(0, 46)
        Me.pnlModuleContent.Name = "pnlModuleContent"
        Me.pnlModuleContent.Size = New System.Drawing.Size(800, 554)
        Me.pnlModuleContent.TabIndex = 1
        '
        'ProductionUC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.pnlContentContainer.Controls.Add(Me.pnlModuleContent)
        Me.pnlContentContainer.Controls.Add(Me.tsSubNav)
        Me.Name = "ProductionUC"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.tsSubNav.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents tsSubNav As System.Windows.Forms.ToolStrip
    Friend WithEvents btnMasterData As System.Windows.Forms.Button
    Friend WithEvents btnDailyProduction As System.Windows.Forms.Button
    Friend WithEvents btnFinalProduct As System.Windows.Forms.Button
    Friend WithEvents btnWithdrawalOrder As System.Windows.Forms.Button
    Friend WithEvents btnOrder As System.Windows.Forms.Button
    Friend WithEvents btnDashboard As System.Windows.Forms.Button
    Friend WithEvents btnReports As System.Windows.Forms.Button

    Friend WithEvents hostMasterData As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostDailyProduction As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostFinalProduct As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostWithdrawalOrder As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostOrder As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostDashboard As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostReports As System.Windows.Forms.ToolStripControlHost

    Friend WithEvents pnlModuleContent As System.Windows.Forms.Panel
End Class