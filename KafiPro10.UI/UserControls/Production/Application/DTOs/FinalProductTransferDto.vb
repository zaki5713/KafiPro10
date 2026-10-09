Namespace KafiPro.Production.Application.DTOs

    ''' <summary>
    ''' رأس مستند المنتج النهائي
    ''' </summary>
    Public Class FinalProductTransferDto
        Public Property TransferID As Integer
        Public Property TransferNo As String
        Public Property TransferDate As Date

        ' الوردية
        Public Property ShiftTypeID As Integer
        Public Property ShiftNameAr As String

        ' خط الإنتاج
        Public Property ProductionLineID As Integer
        Public Property ProductionLineNameAr As String

        ' المشرف (المُسلِّم)
        Public Property SupervisorID As Integer
        Public Property SupervisorNameAr As String

        ' أمين المخزن (المُستلِم)
        Public Property StorekeeperID As Integer?

        ' الحالة
        Public Property StatusID As Integer
        Public Property StatusCode As String
        Public Property StatusNameAr As String

        Public Property Notes As String

        ' تواريخ الاعتماد والاستلام
        Public Property ApprovedAt As DateTime?
        Public Property ApprovedBy As Integer?
        Public Property ReceivedAt As DateTime?
        Public Property ReceivedBy As Integer?

        Public Property CreatedBy As Integer?
        Public Property CreatedAt As DateTime
        Public Property ModifiedBy As Integer?
        Public Property ModifiedAt As DateTime?

        ' حقول محسوبة (من GetAll)
        Public Property DetailsCount As Integer
        Public Property TotalTransferredQty As Decimal
    End Class

End Namespace