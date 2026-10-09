Namespace KafiPro.Production.Application.Interfaces

    Public Interface IDailyProductionService

        Function GetAll() As List(Of DTOs.DailyProductionViewDto)

        Function Search(fromDate As Date?,
                        toDate As Date?,
                        shiftTypeID As Integer?,
                        productionLineID As Integer?,
                        itemID As Integer?) As List(Of DTOs.DailyProductionViewDto)

        Function GetById(logID As Long) As DTOs.DailyProductionDto

        Function Insert(dto As DTOs.DailyProductionDto) As DTOs.DailyProductionSaveResult

        Function Update(dto As DTOs.DailyProductionDto) As DTOs.DailyProductionSaveResult

        Function Delete(logID As Long, deletedBy As Integer?) As DTOs.DailyProductionSaveResult

    End Interface

End Namespace
