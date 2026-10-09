Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IProductionWorkerRepository
        Function GetAll() As List(Of ProductionWorkerDto)
        Function GetById(id As Integer) As ProductionWorkerDto
        Function GetSupervisors() As List(Of ProductionWorkerDto)
        Function Insert(item As ProductionWorkerDto, userId As Integer) As Integer
        Sub Update(item As ProductionWorkerDto, userId As Integer)
        Sub Delete(id As Integer, userId As Integer)
    End Interface

End Namespace