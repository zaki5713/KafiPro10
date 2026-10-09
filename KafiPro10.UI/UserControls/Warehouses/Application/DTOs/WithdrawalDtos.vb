Imports System.Collections.Generic

' ═══════════════════════════════════════════════════════════
'  DTOs — Withdrawal Order (طلب الصرف للإنتاج)
' ═══════════════════════════════════════════════════════════

''' <summary>رأس طلب الصرف</summary>
Public Class WithdrawalOrderDto
    Public Property WithdrawalID As Long
    Public Property WithdrawalNo As String
    Public Property WithdrawalDate As Date
    Public Property WarehouseID As Integer
    Public Property WarehouseName As String
    Public Property ShiftID As Integer?
    Public Property ShiftName As String
    Public Property ProductionLineID As Integer?
    Public Property StatusID As Integer
    Public Property StatusCode As String
    Public Property StatusNameAr As String
    Public Property Notes As String

    ' Workflow
    Public Property SubmittedAt As DateTime?
    Public Property SubmittedBy As Integer?
    Public Property SubmittedByName As String
    Public Property IssuedAt As DateTime?
    Public Property IssuedBy As Integer?
    Public Property IssuedByName As String
    Public Property ReceivedAt As DateTime?
    Public Property ReceivedBy As Integer?
    Public Property ReceivedByName As String
    Public Property ApprovedAt As DateTime?
    Public Property ApprovedBy As Integer?
    Public Property ApprovedByName As String
    Public Property RejectReason As String
    Public Property CancelReason As String

    ' Audit
    Public Property CreatedBy As Integer?
    Public Property CreatedByName As String
    Public Property CreatedAt As DateTime

    ' Totals
    Public Property ItemsCount As Integer
    Public Property TotalRequestedQty As Decimal
    Public Property TotalIssuedQty As Decimal
    Public Property TotalReceivedQty As Decimal

    ' Details
    Public Property Details As New List(Of WithdrawalOrderDetailDto)

    ' Helper
    Public ReadOnly Property IsEditable As Boolean
        Get
            Return StatusID = 1 ' DRAFT
        End Get
    End Property

    Public ReadOnly Property CanSubmit As Boolean
        Get
            Return StatusID = 1
        End Get
    End Property

    Public ReadOnly Property CanIssue As Boolean
        Get
            Return StatusID = 2 ' SUBMITTED
        End Get
    End Property

    Public ReadOnly Property CanReceive As Boolean
        Get
            Return StatusID = 3 ' ISSUED
        End Get
    End Property

    Public ReadOnly Property CanApprove As Boolean
        Get
            Return StatusID = 4 ' RECEIVED
        End Get
    End Property

    Public ReadOnly Property CanCancel As Boolean
        Get
            Return StatusID <> 5 AndAlso StatusID <> 6 AndAlso StatusID <> 7
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return StatusID = 1
        End Get
    End Property
End Class

''' <summary>تفصيلة طلب الصرف</summary>
Public Class WithdrawalOrderDetailDto
    Public Property DetailID As Long
    Public Property WithdrawalID As Long
    Public Property ItemID As Integer
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property UnitID As Integer?
    Public Property UnitName As String
    Public Property RequestedQty As Decimal
    Public Property IssuedQty As Decimal
    Public Property ReceivedQty As Decimal
    Public Property Notes As String

    ' رصيد متاح (للعرض)
    Public Property AvailableQty As Decimal
    Public Property OnHandQty As Decimal
    Public Property HeldQty As Decimal
End Class

''' <summary>لإنشاء طلب جديد</summary>
Public Class CreateWithdrawalDto
    Public Property WithdrawalDate As Date
    Public Property WarehouseID As Integer
    Public Property ShiftID As Integer?
    Public Property ProductionLineID As Integer?
    Public Property Notes As String
    Public Property CreatedBy As Integer
    Public Property Details As New List(Of CreateWithdrawalDetailDto)
End Class

Public Class CreateWithdrawalDetailDto
    Public Property ItemID As Integer
    Public Property UnitID As Integer?
    Public Property RequestedQty As Decimal
    Public Property Notes As String
End Class

''' <summary>للصرف من المخزن</summary>
Public Class IssueWithdrawalDto
    Public Property WithdrawalID As Long
    Public Property IssuedBy As Integer
    Public Property Details As New List(Of IssueWithdrawalDetailDto)
End Class

Public Class IssueWithdrawalDetailDto
    Public Property ItemID As Integer
    Public Property UnitID As Integer?
    Public Property RequestedQty As Decimal
    Public Property IssuedQty As Decimal
    Public Property Notes As String
End Class

''' <summary>سجل التدقيق</summary>
Public Class WithdrawalAuditLogDto
    Public Property LogID As Long
    Public Property WithdrawalID As Long
    Public Property Action As String
    Public Property FromStatusID As Integer?
    Public Property FromStatusName As String
    Public Property ToStatusID As Integer?
    Public Property ToStatusName As String
    Public Property UserID As Integer
    Public Property UserName As String
    Public Property Notes As String
    Public Property CreatedAt As DateTime
End Class

''' <summary>عناصر ComboBox للفلترة</summary>
Public Class WithdrawalStatusLookup
    Public Property StatusID As Integer
    Public Property StatusCode As String
    Public Property StatusNameAr As String
End Class