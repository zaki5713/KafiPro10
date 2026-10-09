
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Application.Interfaces
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces

Namespace KafiPro.Production.Application.Services

    Public Class ProductionLineService
        Implements IProductionLineService

        Private ReadOnly _repo As IProductionLineRepository

        Public Sub New(repo As IProductionLineRepository)
            _repo = repo
        End Sub

        Public Function GetAll() As List(Of ProductionLineDto) _
            Implements IProductionLineService.GetAll

            Return _repo.GetAll()

        End Function

    End Class

End Namespace
