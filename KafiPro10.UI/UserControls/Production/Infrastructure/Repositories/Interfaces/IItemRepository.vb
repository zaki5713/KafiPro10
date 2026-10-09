Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IItemRepository

        Function GetAll() As List(Of ItemDto)
        Function GetById(itemId As Integer) As ItemDto
        Function Insert(item As ItemDto, userId As Integer) As Integer
        Sub Update(item As ItemDto, userId As Integer)
        Sub Delete(itemId As Integer, userId As Integer)

        Function GetItemTypes() As List(Of ItemTypeDto)
        Function GetUnits() As List(Of UnitOfMeasureDto)
        Function GetFinishedGoods() As List(Of ItemDto)
        Function GetMaterials() As List(Of ItemDto)

    End Interface

End Namespace