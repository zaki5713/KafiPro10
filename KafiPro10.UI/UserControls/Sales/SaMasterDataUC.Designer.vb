Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SaMasterDataUC
    Inherits BaseUC

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
        Me.tabControl = New System.Windows.Forms.TabControl()
        Me.tabReps = New System.Windows.Forms.TabPage()
        Me.tabBranches = New System.Windows.Forms.TabPage()
        Me.tabRoutes = New System.Windows.Forms.TabPage()
        Me.tabSalesTarget = New System.Windows.Forms.TabPage()
        Me.tabRepTarget = New System.Windows.Forms.TabPage()
        Me.tabCommission = New System.Windows.Forms.TabPage()
        Me.tabOrderLimit = New System.Windows.Forms.TabPage()

        Me.tabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        ' tabControl
        '
        Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl.Font = UITheme.FontBold
        Me.tabControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tabControl.RightToLeftLayout = True
        Me.tabControl.Name = "tabControl"
        Me.tabControl.Padding = New System.Drawing.Point(18, 6)
        '
        Me.tabReps.Text = "المناديب"
        Me.tabBranches.Text = "الفروع"
        Me.tabRoutes.Text = "خطوط السير"
        Me.tabSalesTarget.Text = "تارجت المبيعات"
        Me.tabRepTarget.Text = "تارجت المناديب"
        Me.tabCommission.Text = "العمولة"
        Me.tabOrderLimit.Text = "حد الطلب"

        Me.tabReps.Name = "tabReps"
        Me.tabBranches.Name = "tabBranches"
        Me.tabRoutes.Name = "tabRoutes"
        Me.tabSalesTarget.Name = "tabSalesTarget"
        Me.tabRepTarget.Name = "tabRepTarget"
        Me.tabCommission.Name = "tabCommission"
        Me.tabOrderLimit.Name = "tabOrderLimit"

        Me.tabControl.Controls.Add(Me.tabReps)
        Me.tabControl.Controls.Add(Me.tabBranches)
        Me.tabControl.Controls.Add(Me.tabRoutes)
        Me.tabControl.Controls.Add(Me.tabSalesTarget)
        Me.tabControl.Controls.Add(Me.tabRepTarget)
        Me.tabControl.Controls.Add(Me.tabCommission)
        Me.tabControl.Controls.Add(Me.tabOrderLimit)
        '
        ' SaMasterDataUC
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.pnlContentContainer.Controls.Add(Me.tabControl)
        Me.Name = "SaMasterDataUC"
        Me.Size = New System.Drawing.Size(900, 600)
        Me.tabControl.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents tabControl As System.Windows.Forms.TabControl
    Friend WithEvents tabReps As System.Windows.Forms.TabPage
    Friend WithEvents tabBranches As System.Windows.Forms.TabPage
    Friend WithEvents tabRoutes As System.Windows.Forms.TabPage
    Friend WithEvents tabSalesTarget As System.Windows.Forms.TabPage
    Friend WithEvents tabRepTarget As System.Windows.Forms.TabPage
    Friend WithEvents tabCommission As System.Windows.Forms.TabPage
    Friend WithEvents tabOrderLimit As System.Windows.Forms.TabPage
End Class