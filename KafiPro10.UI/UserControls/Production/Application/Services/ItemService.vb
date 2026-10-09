
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Application.Interfaces
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces

Namespace KafiPro.Production.Application.Services

    Public Class ItemService
        Implements IItemService

        Private ReadOnly _repo As IItemRepository

        Public Sub New(repo As IItemRepository)
            _repo = repo
        End Sub

        Public Function GetAll() As List(Of ItemDto) _
            Implements IItemService.GetAll

            Return _repo.GetAll()

        End Function

    End Class

End Namespace
