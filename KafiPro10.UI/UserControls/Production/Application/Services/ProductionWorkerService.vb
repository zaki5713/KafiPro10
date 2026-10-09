
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Application.Interfaces
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces

Namespace KafiPro.Production.Application.Services

    Public Class ProductionWorkerService
        Implements IProductionWorkerService

        Private ReadOnly _repo As IProductionWorkerRepository

        Public Sub New(repo As IProductionWorkerRepository)
            _repo = repo
        End Sub

        Public Function GetAll() As List(Of ProductionWorkerDto) _
            Implements IProductionWorkerService.GetAll

            Return _repo.GetAll()

        End Function

        Public Function GetSupervisors() As List(Of ProductionWorkerDto) _
            Implements IProductionWorkerService.GetSupervisors

            Return _repo.GetSupervisors()

        End Function

    End Class

End Namespace
