Namespace KafiPro.Production.Application.DTOs

    ''' <summary>
    ''' سجل إنتاج فيه كمية متاحة للتحويل
    ''' </summary>
    Public Class AvailableProductionDto
        Public Property LogID As Long              ' BIGINT
        Public Property ProductionDate As Date

        ' الوردية
        Public Property ShiftTypeID As Integer
        Public Property ShiftNameAr As String

        ' خط الإنتاج
        Public Property ProductionLineID As Integer
        Public Property ProductionLineNameAr As String

        ' المنتج
        Public Property ItemID As Integer
        Public Property ItemCode As String
        Public Property ItemNameAr As String
        Public Property ItemNameEn As String
        Public Property UnitName As String

        ' الكميات
        Public Property ProducedQty As Decimal
        Public Property TotalTransferredQty As Decimal
        Public Property AvailableQty As Decimal
        Public Property RejectedFGQty As Decimal

        ' المشرف
        Public Property SupervisorNameAr As String

        ''' <summary>
        ''' للعرض في ComboBox
        ''' </summary>
        Public ReadOnly Property DisplayName As String
            Get
                Return $"{ProductionDate:yyyy-MM-dd} | {ItemNameAr} | متاح: {AvailableQty:N3}"
            End Get
        End Property
    End Class

End Namespace