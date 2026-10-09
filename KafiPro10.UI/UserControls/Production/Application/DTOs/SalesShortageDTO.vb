Imports System

Public Class SalesShortageDTO
    Public Property ShortageID As Integer
    Public Property ItemID As Integer
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property UnitName As String
    Public Property WarehouseID As Integer?
    Public Property WarehouseName As String
    Public Property RequiredQty As Decimal
    Public Property AvailableQty As Decimal
    Public Property SourceType As Byte
    Public Property ReferenceNo As String
    Public Property RequiredDate As DateTime?
    Public Property Status As Byte
    Public Property Notes As String
    Public Property CreatedAt As DateTime?

    Public ReadOnly Property ShortageQty As Decimal
        Get
            Dim q As Decimal = RequiredQty - AvailableQty
            If q < 0D Then Return 0D
            Return q
        End Get
    End Property

    Public ReadOnly Property StatusText As String
        Get
            Select Case Status
                Case 1 : Return "مفتوح"
                Case 2 : Return "تحت الإنتاج"
                Case 3 : Return "مغلق"
                Case 4 : Return "ملغي"
                Case Else : Return "-"
            End Select
        End Get
    End Property

    Public ReadOnly Property SourceText As String
        Get
            Select Case SourceType
                Case 1 : Return "يدوي"
                Case 2 : Return "آلي (مخزون)"
                Case 3 : Return "مبيعات"
                Case Else : Return "-"
            End Select
        End Get
    End Property
End Class