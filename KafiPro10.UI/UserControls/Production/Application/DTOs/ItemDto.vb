Namespace KafiPro.Production.Application.DTOs

    Public Class ItemDto
        Public Property ItemID As Integer
        Public Property ItemCode As String
        Public Property ItemNameAr As String
        Public Property ItemNameEn As String
        Public Property ItemTypeID As Integer
        Public Property ItemTypeNameAr As String
        Public Property InventoryUnitID As Integer
        Public Property InventoryUnitName As String
        Public Property FinancialUnitID As Integer
        Public Property FinancialUnitName As String
        Public Property IsActive As Boolean

        ' ═══ الأوزان (جديد) ═══
        Public Property NetWeight As Decimal?
        Public Property PackageWeight As Decimal?
        Public Property GrossWeight As Decimal?

        Public ReadOnly Property DisplayName As String
            Get
                If String.IsNullOrWhiteSpace(ItemCode) Then Return ItemNameAr
                Return $"{ItemCode} - {ItemNameAr}"
            End Get
        End Property
    End Class

End Namespace