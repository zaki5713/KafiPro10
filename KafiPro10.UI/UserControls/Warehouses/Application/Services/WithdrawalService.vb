Imports System.Collections.Generic

Public Class WithdrawalService

    Private ReadOnly _repo As New WithdrawalRepository()

    ' ═══════════════════════════════════════════════════════════
    '  Create / Update
    ' ═══════════════════════════════════════════════════════════
    Public Function Create(dto As CreateWithdrawalDto) As (WithdrawalID As Long, WithdrawalNo As String)
        ValidateCreate(dto)
        Return _repo.Create(dto)
    End Function

    Public Sub Update(WithdrawalID As Long, dto As CreateWithdrawalDto)
        If WithdrawalID <= 0 Then Throw New Exception("رقم الطلب مطلوب.")
        ValidateCreate(dto)
        _repo.Update(WithdrawalID, dto)
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Workflow
    ' ═══════════════════════════════════════════════════════════
    Public Sub Submit(WithdrawalID As Long, UserID As Integer)
        _repo.Submit(WithdrawalID, UserID)
    End Sub

    Public Sub Issue(dto As IssueWithdrawalDto)
        If dto.WithdrawalID <= 0 Then Throw New Exception("رقم الطلب مطلوب.")
        If dto.Details Is Nothing OrElse dto.Details.Count = 0 Then
            Throw New Exception("لا توجد أصناف للصرف.")
        End If
        If Not dto.Details.Exists(Function(x) x.IssuedQty > 0) Then
            Throw New Exception("يجب إدخال كمية مصروفة في صنف واحد على الأقل.")
        End If

        _repo.Issue(dto)
    End Sub

    Public Sub Receive(WithdrawalID As Long, UserID As Integer)
        _repo.Receive(WithdrawalID, UserID)
    End Sub

    Public Sub Approve(WithdrawalID As Long, UserID As Integer)
        _repo.Approve(WithdrawalID, UserID)
    End Sub

    Public Sub Reject(WithdrawalID As Long, Reason As String, UserID As Integer)
        If String.IsNullOrWhiteSpace(Reason) Then Throw New Exception("سبب الرفض مطلوب.")
        _repo.Reject(WithdrawalID, Reason, UserID)
    End Sub

    Public Sub Cancel(WithdrawalID As Long, Reason As String, UserID As Integer)
        If String.IsNullOrWhiteSpace(Reason) Then Throw New Exception("سبب الإلغاء مطلوب.")
        _repo.Cancel(WithdrawalID, Reason, UserID)
    End Sub

    Public Sub Delete(WithdrawalID As Long, UserID As Integer)
        _repo.Delete(WithdrawalID, UserID)
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Get
    ' ═══════════════════════════════════════════════════════════
    Public Function GetAll(Optional fromDate As Date? = Nothing,
                           Optional toDate As Date? = Nothing,
                           Optional statusID As Integer? = Nothing,
                           Optional warehouseID As Integer? = Nothing) As List(Of WithdrawalOrderDto)
        Return _repo.GetAll(fromDate, toDate, statusID, warehouseID)
    End Function

    Public Function GetById(WithdrawalID As Long) As WithdrawalOrderDto
        Return _repo.GetById(WithdrawalID)
    End Function

    Public Function GetHistory(WithdrawalID As Long) As List(Of WithdrawalAuditLogDto)
        Return _repo.GetHistory(WithdrawalID)
    End Function

    Public Function GetStatusLookup() As List(Of WithdrawalStatusLookup)
        Return _repo.GetStatusLookup()
    End Function

    Public Function GetNextNo() As String
        Return _repo.GetNextNo()
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Validation
    ' ═══════════════════════════════════════════════════════════
    Private Sub ValidateCreate(dto As CreateWithdrawalDto)
        If dto.WarehouseID <= 0 Then Throw New Exception("يجب اختيار المخزن.")
        If dto.WithdrawalDate = Date.MinValue Then Throw New Exception("تاريخ الطلب مطلوب.")
        If dto.Details Is Nothing OrElse dto.Details.Count = 0 Then
            Throw New Exception("يجب إضافة صنف واحد على الأقل.")
        End If
        If Not dto.Details.Exists(Function(x) x.RequestedQty > 0) Then
            Throw New Exception("يجب إدخال كمية موجبة لصنف واحد على الأقل.")
        End If
    End Sub

End Class