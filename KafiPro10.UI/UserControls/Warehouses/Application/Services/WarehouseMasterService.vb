Imports System.Collections.Generic

Public Class WarehouseMasterService

    Private ReadOnly _repo As New WarehouseMasterRepository()

    ' Warehouses
    Public Function GetWarehouses(Optional onlyActive As Boolean = False) As List(Of WarehouseDto)
        Return _repo.GetWarehouses(onlyActive)
    End Function
    Public Sub InsertWarehouse(code As String, nameAr As String, nameEn As String, typeID As Integer, isDefault As Boolean, isActive As Boolean, userID As Integer)
        If String.IsNullOrWhiteSpace(code) Then Throw New Exception("كود المخزن مطلوب.")
        If String.IsNullOrWhiteSpace(nameAr) Then Throw New Exception("اسم المخزن مطلوب.")
        If typeID <= 0 Then Throw New Exception("نوع المخزن مطلوب.")
        _repo.InsertWarehouse(code, nameAr, nameEn, typeID, isDefault, isActive, userID)
    End Sub
    Public Sub UpdateWarehouse(id As Integer, code As String, nameAr As String, nameEn As String, typeID As Integer, isDefault As Boolean, isActive As Boolean, userID As Integer)
        If id <= 0 Then Throw New Exception("رقم المخزن مطلوب.")
        _repo.UpdateWarehouse(id, code, nameAr, nameEn, typeID, isDefault, isActive, userID)
    End Sub
    Public Sub DeleteWarehouse(id As Integer, userID As Integer)
        _repo.DeleteWarehouse(id, userID)
    End Sub

    ' Warehouse Types
    Public Function GetWarehouseTypes() As List(Of WarehouseTypeDto)
        Return _repo.GetWarehouseTypes()
    End Function

    ' Branches
    Public Function GetBranches() As List(Of BranchDto)
        Return _repo.GetBranches()
    End Function
    Public Sub InsertBranch(companyID As Integer, code As String, nameAr As String, nameEn As String, isActive As Boolean, userID As Integer)
        If companyID <= 0 Then Throw New Exception("الشركة مطلوبة.")
        If String.IsNullOrWhiteSpace(code) Then Throw New Exception("كود الفرع مطلوب.")
        If String.IsNullOrWhiteSpace(nameAr) Then Throw New Exception("اسم الفرع مطلوب.")
        _repo.InsertBranch(companyID, code, nameAr, nameEn, isActive, userID)
    End Sub
    Public Sub UpdateBranch(id As Integer, companyID As Integer, code As String, nameAr As String, nameEn As String, isActive As Boolean, userID As Integer)
        _repo.UpdateBranch(id, companyID, code, nameAr, nameEn, isActive, userID)
    End Sub
    Public Sub DeleteBranch(id As Integer, userID As Integer)
        _repo.DeleteBranch(id, userID)
    End Sub

    ' Workers
    Public Function GetWorkers(Optional onlyActive As Boolean = False) As List(Of WorkerDto)
        Return _repo.GetWorkers(onlyActive)
    End Function
    Public Sub InsertWorker(code As String, nameAr As String, nameEn As String, role As String,
                            phone As String, nationalID As String, hireDate As Date?,
                            defaultWhID As Integer?, notes As String, isActive As Boolean, userID As Integer)
        If String.IsNullOrWhiteSpace(code) Then Throw New Exception("كود العامل مطلوب.")
        If String.IsNullOrWhiteSpace(nameAr) Then Throw New Exception("اسم العامل مطلوب.")
        If String.IsNullOrWhiteSpace(role) Then Throw New Exception("الوظيفة مطلوبة.")
        _repo.InsertWorker(code, nameAr, nameEn, role, phone, nationalID, hireDate, defaultWhID, notes, isActive, userID)
    End Sub
    Public Sub UpdateWorker(id As Integer, code As String, nameAr As String, nameEn As String, role As String,
                            phone As String, nationalID As String, hireDate As Date?,
                            defaultWhID As Integer?, notes As String, isActive As Boolean, userID As Integer)
        _repo.UpdateWorker(id, code, nameAr, nameEn, role, phone, nationalID, hireDate, defaultWhID, notes, isActive, userID)
    End Sub
    Public Sub DeleteWorker(id As Integer, userID As Integer)
        _repo.DeleteWorker(id, userID)
    End Sub

End Class