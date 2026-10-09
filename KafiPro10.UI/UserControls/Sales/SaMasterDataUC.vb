Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro.UI.Shared.Helpers

Public Class SaMasterDataUC
    Inherits BaseUC

    Private Sub SaMasterDataUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.HeaderTitle = "البيانات الأساسية - المبيعات"
        LoadDefaultTab()
    End Sub

    Private Shared Sub LoadDefaultTab()
        ' تحميل تاب المناديب افتراضياً
        'LoadTabContent(tabReps, New SaRepMasterUC())
    End Sub

    Private Sub tabControl_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles tabControl.SelectedIndexChanged

        Select Case TabControl.SelectedTab.Name
            Case "tabReps" : LoadTabContent(tabReps, New SaRepMasterUC())
                'Case "tabBranches" : LoadTabContent(tabBranches, New SaBranchesUC())
                'Case "tabRoutes" : LoadTabContent(tabRoutes, New SaRoutesUC())
                'Case "tabSalesTarget" : LoadTabContent(tabSalesTarget, New SaSalesTargetUC())
                'Case "tabRepTarget" : LoadTabContent(tabRepTarget, New SaRepTargetUC())
                'Case "tabCommission" : LoadTabContent(tabCommission, New SaCommissionUC())
                'Case "tabOrderLimit" : LoadTabContent(tabOrderLimit, New SaOrderLimitUC())
        End Select
    End Sub

    Private Sub LoadTabContent(tab As TabPage, uc As UserControl)
        ' تحميل مرة واحدة فقط لكل تاب (Lazy Load)
        If tab.Controls.Count > 0 Then Return
        uc.Dock = DockStyle.Fill
        tab.Controls.Add(uc)
    End Sub
End Class