Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IDailyProductionLogRepository
        Function GetAll() As List(Of DailyProductionLogDto)
        Function GetById(logId As Long) As DailyProductionLogDto
        Function Insert(item As DailyProductionLogDto, userId As Integer) As Long
        Sub Update(item As DailyProductionLogDto, userId As Integer)
        Sub Delete(logId As Long, userId As Integer)

        Function Search(fromDate As Date?,
                        toDate As Date?,
                        shiftTypeId As Integer?,
                        lineId As Integer?,
                        itemId As Integer?) As List(Of DailyProductionLogDto)
    End Interface

End Namespace