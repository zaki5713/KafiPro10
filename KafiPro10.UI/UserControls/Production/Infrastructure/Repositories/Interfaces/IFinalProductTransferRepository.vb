Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IFinalProductTransferRepository

        ' ═══ Lookups ═══
        Function GetStatuses() As List(Of FinalProductTransferStatusDto)
        Function GetNextTransferNo() As String

        ' ═══ Header ═══
        Function GetAll(Optional fromDate As Date? = Nothing,
                        Optional toDate As Date? = Nothing,
                        Optional statusId As Integer? = Nothing) As List(Of FinalProductTransferDto)

        Function GetById(transferId As Integer) As FinalProductTransferDto

        Function Insert(item As FinalProductTransferDto, userId As Integer) As Integer
        Sub Update(item As FinalProductTransferDto, userId As Integer)
        Sub Delete(transferId As Integer, userId As Integer)

        ' ═══ Workflow ═══
        Sub Approve(transferId As Integer, approvedBy As Integer)
        Sub Receive(transferId As Integer, receivedBy As Integer)

        ' ═══ Details ═══
        Function GetDetails(transferId As Integer) As List(Of FinalProductTransferDetailDto)
        Function GetDetailById(detailId As Integer) As FinalProductTransferDetailDto

        Function InsertDetail(item As FinalProductTransferDetailDto, userId As Integer) As Integer
        Sub UpdateDetail(item As FinalProductTransferDetailDto, userId As Integer)
        Sub DeleteDetail(detailId As Integer)
        Sub CancelTransfer(transferId As Integer, userId As Integer)
        ' ═══ الكميات المتاحة ═══
        Function GetAvailableProductions(Optional fromDate As Date? = Nothing,
                                          Optional toDate As Date? = Nothing,
                                          Optional shiftTypeId As Integer? = Nothing,
                                          Optional productionLineId As Integer? = Nothing,
                                          Optional itemId As Integer? = Nothing) As List(Of AvailableProductionDto)

    End Interface

End Namespace