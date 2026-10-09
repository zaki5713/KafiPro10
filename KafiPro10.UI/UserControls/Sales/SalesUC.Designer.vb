Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SalesUC
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
        Me.btnSalesOrder = New System.Windows.Forms.Button()
        Me.btnSalesInvoice = New System.Windows.Forms.Button()
        Me.btnSalesReturn = New System.Windows.Forms.Button()
        Me.btnCollection = New System.Windows.Forms.Button()
        Me.btnDashboard = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()

        Me.hostMasterData = New System.Windows.Forms.ToolStripControlHost(Me.btnMasterData)
        Me.hostSalesOrder = New System.Windows.Forms.ToolStripControlHost(Me.btnSalesOrder)
        Me.hostSalesInvoice = New System.Windows.Forms.ToolStripControlHost(Me.btnSalesInvoice)
        Me.hostSalesReturn = New System.Windows.Forms.ToolStripControlHost(Me.btnSalesReturn)
        Me.hostCollection = New System.Windows.Forms.ToolStripControlHost(Me.btnCollection)
        Me.hostDashboard = New System.Windows.Forms.ToolStripControlHost(Me.btnDashboard)
        Me.hostReports = New System.Windows.Forms.ToolStripControlHost(Me.btnReports)

        Me.pnlModuleContent = New System.Windows.Forms.Panel()
        Me.tsSubNav.SuspendLayout()
        Me.SuspendLayout()
        '
        ' tsSubNav
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
        Me.tsSubNav.Items.Add(Me.hostSalesOrder)
        Me.tsSubNav.Items.Add(Me.hostSalesInvoice)
        Me.tsSubNav.Items.Add(Me.hostSalesReturn)
        Me.tsSubNav.Items.Add(Me.hostCollection)
        Me.tsSubNav.Items.Add(Me.hostDashboard)
        Me.tsSubNav.Items.Add(Me.hostReports)
        '
        ' btnMasterData (Active افتراضياً)
        '
        Me.btnMasterData.BackColor = UITheme.ActiveBackground
        Me.btnMasterData.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMasterData.FlatAppearance.BorderSize = 0
        Me.btnMasterData.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnMasterData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMasterData.Font = UITheme.FontBold
        Me.btnMasterData.ForeColor = UITheme.ActiveText
        Me.btnMasterData.Name = "btnMasterData"
        Me.btnMasterData.Size = New System.Drawing.Size(130, 34)
        Me.btnMasterData.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnMasterData.Text = "البيانات الأساسية"
        Me.btnMasterData.UseVisualStyleBackColor = False
        '
        ' btnSalesOrder
        '
        Me.btnSalesOrder.BackColor = UITheme.ButtonNormal
        Me.btnSalesOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalesOrder.FlatAppearance.BorderSize = 0
        Me.btnSalesOrder.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnSalesOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalesOrder.Font = UITheme.FontBold
        Me.btnSalesOrder.ForeColor = UITheme.TextPrimary
        Me.btnSalesOrder.Name = "btnSalesOrder"
        Me.btnSalesOrder.Size = New System.Drawing.Size(130, 34)
        Me.btnSalesOrder.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnSalesOrder.Text = "أوامر البيع"
        Me.btnSalesOrder.UseVisualStyleBackColor = False
        '
        ' btnSalesInvoice
        '
        Me.btnSalesInvoice.BackColor = UITheme.ButtonNormal
        Me.btnSalesInvoice.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalesInvoice.FlatAppearance.BorderSize = 0
        Me.btnSalesInvoice.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnSalesInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalesInvoice.Font = UITheme.FontBold
        Me.btnSalesInvoice.ForeColor = UITheme.TextPrimary
        Me.btnSalesInvoice.Name = "btnSalesInvoice"
        Me.btnSalesInvoice.Size = New System.Drawing.Size(130, 34)
        Me.btnSalesInvoice.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnSalesInvoice.Text = "فواتير البيع"
        Me.btnSalesInvoice.UseVisualStyleBackColor = False
        '
        ' btnSalesReturn
        '
        Me.btnSalesReturn.BackColor = UITheme.ButtonNormal
        Me.btnSalesReturn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalesReturn.FlatAppearance.BorderSize = 0
        Me.btnSalesReturn.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnSalesReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalesReturn.Font = UITheme.FontBold
        Me.btnSalesReturn.ForeColor = UITheme.TextPrimary
        Me.btnSalesReturn.Name = "btnSalesReturn"
        Me.btnSalesReturn.Size = New System.Drawing.Size(130, 34)
        Me.btnSalesReturn.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnSalesReturn.Text = "مرتجعات البيع"
        Me.btnSalesReturn.UseVisualStyleBackColor = False
        '
        ' btnCollection
        '
        Me.btnCollection.BackColor = UITheme.ButtonNormal
        Me.btnCollection.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCollection.FlatAppearance.BorderSize = 0
        Me.btnCollection.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCollection.Font = UITheme.FontBold
        Me.btnCollection.ForeColor = UITheme.TextPrimary
        Me.btnCollection.Name = "btnCollection"
        Me.btnCollection.Size = New System.Drawing.Size(130, 34)
        Me.btnCollection.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnCollection.Text = "التحصيل"
        Me.btnCollection.UseVisualStyleBackColor = False
        '
        ' btnDashboard
        '
        Me.btnDashboard.BackColor = UITheme.ButtonNormal
        Me.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDashboard.FlatAppearance.BorderSize = 0
        Me.btnDashboard.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDashboard.Font = UITheme.FontBold
        Me.btnDashboard.ForeColor = UITheme.TextPrimary
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(130, 34)
        Me.btnDashboard.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnDashboard.Text = "لوحة التحكم"
        Me.btnDashboard.UseVisualStyleBackColor = False
        '
        ' btnReports
        '
        Me.btnReports.BackColor = UITheme.ButtonNormal
        Me.btnReports.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReports.FlatAppearance.BorderSize = 0
        Me.btnReports.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Me.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReports.Font = UITheme.FontBold
        Me.btnReports.ForeColor = UITheme.TextPrimary
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(130, 34)
        Me.btnReports.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.btnReports.Text = "التقارير"
        Me.btnReports.UseVisualStyleBackColor = False
        '
        ' pnlModuleContent
        '
        Me.pnlModuleContent.BackColor = UITheme.Surface
        Me.pnlModuleContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlModuleContent.Location = New System.Drawing.Point(0, 46)
        Me.pnlModuleContent.Name = "pnlModuleContent"
        Me.pnlModuleContent.Size = New System.Drawing.Size(800, 554)
        Me.pnlModuleContent.TabIndex = 1
        '
        ' SalesUC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.pnlContentContainer.Controls.Add(Me.pnlModuleContent)
        Me.pnlContentContainer.Controls.Add(Me.tsSubNav)
        Me.Name = "SalesUC"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.tsSubNav.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents tsSubNav As System.Windows.Forms.ToolStrip
    Friend WithEvents btnMasterData As System.Windows.Forms.Button
    Friend WithEvents btnSalesOrder As System.Windows.Forms.Button
    Friend WithEvents btnSalesInvoice As System.Windows.Forms.Button
    Friend WithEvents btnSalesReturn As System.Windows.Forms.Button
    Friend WithEvents btnCollection As System.Windows.Forms.Button
    Friend WithEvents btnDashboard As System.Windows.Forms.Button
    Friend WithEvents btnReports As System.Windows.Forms.Button

    Friend WithEvents hostMasterData As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostSalesOrder As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostSalesInvoice As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostSalesReturn As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostCollection As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostDashboard As System.Windows.Forms.ToolStripControlHost
    Friend WithEvents hostReports As System.Windows.Forms.ToolStripControlHost

    Friend WithEvents pnlModuleContent As System.Windows.Forms.Panel
End Class