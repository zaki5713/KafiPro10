Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IBOMRepository

        ' ══════════════════════════════════════════════════
        '  الرأس
        ' ══════════════════════════════════════════════════
        Function GetAll() As List(Of BOMDto)
        Function GetById(bomId As Integer) As BOMDto
        Function Insert(item As BOMDto, userId As Integer) As Integer
        Sub Update(item As BOMDto, userId As Integer)
        Sub Delete(bomId As Integer, userId As Integer)

        ' ══════════════════════════════════════════════════
        '  التفاصيل
        ' ══════════════════════════════════════════════════
        Function GetDetails(bomId As Integer) As List(Of BOMDetailDto)
        Function GetDetailById(detailId As Integer) As BOMDetailDto
        Function InsertDetail(item As BOMDetailDto, userId As Integer) As Integer
        Sub UpdateDetail(item As BOMDetailDto, userId As Integer)
        Sub DeleteDetail(detailId As Integer)

    End Interface

End Namespace