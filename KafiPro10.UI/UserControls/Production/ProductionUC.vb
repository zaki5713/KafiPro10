Imports KafiPro10.Solution.KafiPro.UI.Shared.Helpers
Imports System.Windows.Forms.DataVisualization.Charting
Public Class ProductionUC
    Inherits BaseUC


    Private currentActiveButton As Button = Nothing

    Private Sub ProductionUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.HeaderTitle = "إدارة الإنتاج"

        SubNavHelper.WireClickHandler(AllButtons(), AddressOf SubNav_Click)
        currentActiveButton = SubNavHelper.Initialize(btnMasterData, AllButtons())

        LoadModuleContent(New ProMasterDataUC())
    End Sub

    Private Function AllButtons() As Button()
        Return {
            btnMasterData,
            btnDailyProduction,
            btnFinalProduct,
            btnWithdrawalOrder,
            btnOrder,
            btnDashboard,
            btnReports
        }
    End Function

    Private Sub SubNav_Click(sender As Object, e As EventArgs)
        Dim clicked As Button = CType(sender, Button)
        SubNavHelper.SetActive(AllButtons(), clicked)
        currentActiveButton = clicked

        Select Case clicked.Name
            Case "btnMasterData" : LoadModuleContent(New ProMasterDataUC())
            Case "btnDailyProduction" : LoadModuleContent(New ProDailyProductionUC())
            Case "btnFinalProduct" : LoadModuleContent(New ProFinalProductUC())
            Case "btnWithdrawalOrder" : LoadModuleContent(New ProWithdrawalOrderUC())
            Case "btnOrder" : LoadModuleContent(New ProOrderUC())
            Case "btnDashboard" : LoadModuleContent(New ProDashboardUC())
            Case "btnReports" : LoadModuleContent(New ProductionReportsUC())
        End Select
    End Sub

    Private Sub LoadModuleContent(uc As UserControl)
        pnlModuleContent.Controls.Clear()
        uc.Dock = DockStyle.Fill
        pnlModuleContent.Controls.Add(uc)
    End Sub

End Class