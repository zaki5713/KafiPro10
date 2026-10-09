
Imports KafiPro10.Solution.KafiPro10.Solution.KafiPro.Sales.Application.DTOs

Namespace KafiPro10.Solution.KafiPro.Sales.Infrastructure.Repositories.Interfaces

    Public Interface ISalesRepRepository
        Function GetAll() As List(Of SalesRepDto)
        Function GetById(repId As Integer) As SalesRepDto
        Function Insert(rep As SalesRepDto, userId As Integer) As Integer
        Sub Update(rep As SalesRepDto, userId As Integer)
        Sub Delete(repId As Integer, userId As Integer)
        Function GetBranches() As List(Of BranchDto)
        Function GetRoutes() As List(Of RouteDto)
    End Interface

End Namespace