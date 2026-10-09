
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Application.Interfaces
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient
Namespace KafiPro10.Solution.KafiPro.Production.Application.Services

    Public Class DailyProductionService
        Implements IDailyProductionService

        Private ReadOnly _repo As IDailyProductionRepository

        Public Sub New(repo As IDailyProductionRepository)
            _repo = repo
        End Sub

        Public Function GetAll() As List(Of DailyProductionViewDto) _
            Implements IDailyProductionService.GetAll

            Return _repo.GetAll()

        End Function

        Public Function Search(fromDate As Date?,
                               toDate As Date?,
                               shiftTypeID As Integer?,
                               productionLineID As Integer?,
                               itemID As Integer?) As List(Of DailyProductionViewDto) _
            Implements IDailyProductionService.Search

            Return _repo.Search(fromDate, toDate, shiftTypeID, productionLineID, itemID)

        End Function

        Public Function GetById(logID As Long) As DailyProductionDto _
            Implements IDailyProductionService.GetById

            Return _repo.GetById(logID)

        End Function

        Public Function Insert(dto As DailyProductionDto) As DailyProductionSaveResult _
            Implements IDailyProductionService.Insert

            ' تحقق من الصلاحيات (إن وجد)
            If dto Is Nothing Then
                Return DailyProductionSaveResult.Fail("البيانات المرسلة فارغة.")
            End If

            Return _repo.Insert(dto)

        End Function

        Public Function Update(dto As DailyProductionDto) As DailyProductionSaveResult _
            Implements IDailyProductionService.Update

            If dto Is Nothing Then
                Return DailyProductionSaveResult.Fail("البيانات المرسلة فارغة.")
            End If

            If Not dto.LogID.HasValue OrElse dto.LogID.Value <= 0 Then
                Return DailyProductionSaveResult.Fail("رقم السجل مطلوب للتعديل.")
            End If

            Return _repo.Update(dto)

        End Function

        Public Function Delete(logID As Long, deletedBy As Integer?) As DailyProductionSaveResult _
            Implements IDailyProductionService.Delete

            If logID <= 0 Then
                Return DailyProductionSaveResult.Fail("رقم السجل غير صحيح.")
            End If

            Return _repo.Delete(logID, deletedBy)

        End Function

    End Class

End Namespace
