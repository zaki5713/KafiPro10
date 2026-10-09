Namespace KafiPro.Production.Application.DTOs

    ''' <summary>
    ''' DTO لعرض سجل الإنتاج في الجريد (بيانات مسطحة بأسماء عربية)
    ''' </summary>
    Public Class DailyProductionViewDto

        ' ============ الهوية ============
        Public Property LogID As Long
        Public Property ProductionDate As Date

        ' ============ الشيفت ============
        Public Property ShiftTypeID As Integer
        Public Property ShiftCode As String
        Public Property ShiftNameAr As String

        ' ============ الخط ============
        Public Property ProductionLineID As Integer
        Public Property LineCode As String
        Public Property ProductionLineNameAr As String

        ' ============ المنتج ============
        Public Property ItemID As Integer
        Public Property ItemCode As String
        Public Property ItemNameAr As String

        ' ============ المشرف ============
        Public Property SupervisorID As Integer
        Public Property SupervisorNameAr As String

        ' ============ الكميات ============
        Public Property ProducedQty As Decimal
        Public Property RawMaterialScrapQty As Decimal
        Public Property PackagingScrapQty As Decimal
        Public Property RejectedFGQty As Decimal
        Public Property DowntimeMinutes As Decimal

        ' ============ إضافات ============
        Public Property Notes As String
        Public Property CreatedAt As DateTime

    End Class

End Namespace
