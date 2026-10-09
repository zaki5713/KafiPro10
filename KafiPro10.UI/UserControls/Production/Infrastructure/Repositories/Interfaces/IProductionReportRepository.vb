Imports System.Collections.Generic

Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IProductionReportRepository
        Function GetDailyDetail(filter As ProductionReportFilterDto) As List(Of ProductionDailyRowDto)
    End Interface

End Namespace