Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IUnitOfMeasureRepository
        Function GetAll() As List(Of UnitOfMeasureDto)
        Function GetById(id As Integer) As UnitOfMeasureDto
        Function Insert(item As UnitOfMeasureDto) As Integer
        Sub Update(item As UnitOfMeasureDto)
        Sub Delete(id As Integer)
    End Interface

End Namespace