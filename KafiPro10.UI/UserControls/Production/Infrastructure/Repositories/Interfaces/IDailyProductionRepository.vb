Namespace KafiPro.Production.Infrastructure.Repositories.Interfaces

    Public Interface IDailyProductionRepository

        ''' <summary>
        ''' جلب كل سجلات الإنتاج
        ''' </summary>
        Function GetAll() As List(Of Application.DTOs.DailyProductionViewDto)

        ''' <summary>
        ''' بحث مع فلتر
        ''' </summary>
        Function Search(fromDate As Date?,
                        toDate As Date?,
                        shiftTypeID As Integer?,
                        productionLineID As Integer?,
                        itemID As Integer?) As List(Of Application.DTOs.DailyProductionViewDto)

        ''' <summary>
        ''' جلب سجل للتعديل
        ''' </summary>
        Function GetById(logID As Long) As Application.DTOs.DailyProductionDto

        ''' <summary>
        ''' إضافة سجل جديد
        ''' </summary>
        Function Insert(dto As Application.DTOs.DailyProductionDto) _
            As Application.DTOs.DailyProductionSaveResult

        ''' <summary>
        ''' تعديل سجل
        ''' </summary>
        Function Update(dto As Application.DTOs.DailyProductionDto) _
            As Application.DTOs.DailyProductionSaveResult

        ''' <summary>
        ''' حذف سجل (Soft Delete)
        ''' </summary>
        Function Delete(logID As Long, deletedBy As Integer?) _
            As Application.DTOs.DailyProductionSaveResult

    End Interface

End Namespace
