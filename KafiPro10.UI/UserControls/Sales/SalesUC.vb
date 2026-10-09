Imports KafiPro10.Solution.KafiPro.UI.Shared.Helpers

Public Class SalesUC
    Inherits BaseUC

    Private currentActiveButton As Button = Nothing

    Private Sub SalesUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.HeaderTitle = "إدارة المبيعات"

        SubNavHelper.WireClickHandler(AllButtons(), AddressOf SubNav_Click)
        currentActiveButton = SubNavHelper.Initialize(btnMasterData, AllButtons())

        LoadModuleContent(New SaMasterDataUC())
    End Sub

    Private Function AllButtons() As Button()
        Return {
            btnMasterData,
            btnSalesOrder,
            btnSalesInvoice,
            btnSalesReturn,
            btnCollection,
            btnDashboard,
            btnReports
        }
    End Function

    Private Sub SubNav_Click(sender As Object, e As EventArgs)
        Dim clicked As Button = CType(sender, Button)
        SubNavHelper.SetActive(AllButtons(), clicked)
        currentActiveButton = clicked

        Select Case clicked.Name
            Case "btnMasterData" : LoadModuleContent(New SaMasterDataUC())
                'Case "btnSalesOrder" : LoadModuleContent(New SaSalesOrderUC())
                'Case "btnSalesInvoice" : LoadModuleContent(New SaSalesInvoiceUC())
                'Case "btnSalesReturn" : LoadModuleContent(New SaSalesReturnUC())
                'Case "btnCollection" : LoadModuleContent(New SaCollectionUC())
                'Case "btnDashboard" : LoadModuleContent(New SaDashboardUC())
                'Case "btnReports" : LoadModuleContent(New SaReportsUC())
        End Select
    End Sub

    Private Sub LoadModuleContent(uc As UserControl)
        pnlModuleContent.Controls.Clear()
        uc.Dock = DockStyle.Fill
        pnlModuleContent.Controls.Add(uc)
    End Sub
End Class