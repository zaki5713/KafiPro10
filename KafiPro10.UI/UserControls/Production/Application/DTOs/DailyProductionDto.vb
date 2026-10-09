Namespace KafiPro.Production.Application.DTOs

    ''' <summary>
    ''' DTO لإدخال/تعديل سجل الإنتاج اليومي
    ''' </summary>
    Public Class DailyProductionDto

        ' ============ المفاتيح ============
        Public Property LogID As Long?

        ' ============ البيانات الأساسية ============
        Public Property ProductionDate As Date
        Public Property ShiftTypeID As Integer
        Public Property ProductionLineID As Integer
        Public Property ItemID As Integer
        Public Property SupervisorID As Integer

        ' ============ الكميات ============
        Public Property ProducedQty As Decimal
        Public Property RawMaterialScrapQty As Decimal
        Public Property PackagingScrapQty As Decimal
        Public Property RejectedFGQty As Decimal
        Public Property DowntimeMinutes As Decimal

        ' ============ إضافات ============
        Public Property Notes As String

        ' ============ Audit ============
        Public Property CreatedBy As Integer?
        Public Property CreatedAt As DateTime?
        Public Property ModifiedBy As Integer?
        Public Property ModifiedAt As DateTime?

    End Class

End Namespace
