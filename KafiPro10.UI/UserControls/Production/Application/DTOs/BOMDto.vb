Namespace KafiPro.Production.Application.DTOs

    ' ══════════════════════════════════════════════════
    '  رأس الخلطة (BOM Header)
    ' ══════════════════════════════════════════════════
    Public Class BOMDto
        Public Property BOMID As Integer
        Public Property BOMCode As String
        Public Property FinishedItemID As Integer
        Public Property FinishedItemCode As String
        Public Property FinishedItemNameAr As String
        Public Property OutputQty As Decimal
        Public Property OutputUnitID As Integer
        Public Property OutputUnitName As String
        Public Property WastagePercent As Decimal
        Public Property Notes As String
        Public Property IsActive As Boolean
        Public Property DetailsCount As Integer
        Public Property CreatedAt As DateTime
    End Class

    ' ══════════════════════════════════════════════════
    '  مكوّن في الخلطة (BOM Detail)
    ' ══════════════════════════════════════════════════
    Public Class BOMDetailDto
        Public Property DetailID As Integer
        Public Property BOMID As Integer
        Public Property MaterialItemID As Integer
        Public Property MaterialItemCode As String
        Public Property MaterialItemNameAr As String
        Public Property MaterialItemNameEn As String
        Public Property Quantity As Decimal
        Public Property InventoryUnitID As Integer
        Public Property UnitName As String
        Public Property Notes As String
    End Class

End Namespace