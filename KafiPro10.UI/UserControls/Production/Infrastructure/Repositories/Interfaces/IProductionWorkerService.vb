Namespace KafiPro.Production.Application.Interfaces

    Public Interface IProductionWorkerService
        Function GetAll() As List(Of DTOs.ProductionWorkerDto)
        Function GetSupervisors() As List(Of DTOs.ProductionWorkerDto)
    End Interface

End Namespace
