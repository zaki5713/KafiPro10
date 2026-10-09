Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IProductionLineRepository
        Function GetAll() As List(Of ProductionLineDto)
        Function GetById(id As Integer) As ProductionLineDto
        Function Insert(item As ProductionLineDto, userId As Integer) As Integer
        Sub Update(item As ProductionLineDto, userId As Integer)
        Sub Delete(id As Integer, userId As Integer)
    End Interface

End Namespace