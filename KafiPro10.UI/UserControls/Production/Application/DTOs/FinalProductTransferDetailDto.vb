Namespace KafiPro.Production.Application.DTOs

    ''' <summary>
    ''' سطر في مستند المنتج النهائي
    ''' </summary>
    Public Class FinalProductTransferDetailDto
        Public Property DetailID As Integer
        Public Property TransferID As Integer
        Public Property DailyLogID As Long         ' BIGINT
        Public Property ItemID As Integer

        ' المنتج
        Public Property ItemCode As String
        Public Property ItemNameAr As String
        Public Property ItemNameEn As String
        Public Property UnitName As String

        ' الكميات
        Public Property TransferredQty As Decimal
        Public Property RejectedQty As Decimal

        ' معلومات من الإنتاج اليومي
        Public Property ProductionDate As Date?
        Public Property ProducedQty As Decimal?

        ' تتبع الدفعة
        Public Property BatchNo As String
        Public Property ExpiryDate As Date?
        Public Property Notes As String
    End Class

End Namespace