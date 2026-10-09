Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IShiftTypeRepository
        Function GetAll() As List(Of ShiftTypeDto)
        Function GetById(id As Integer) As ShiftTypeDto
        Function Insert(item As ShiftTypeDto, userId As Integer) As Integer
        Sub Update(item As ShiftTypeDto, userId As Integer)
        Sub Delete(id As Integer, userId As Integer)
    End Interface

End Namespace