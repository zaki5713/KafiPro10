Imports System.Collections.Generic

' ═══════════════════════════════════════════════════════════
'  DTOs — Warehouses Master Data
' ═══════════════════════════════════════════════════════════

' ═══════════ 1) Warehouses ═══════════
Public Class WarehouseDto
    Public Property WarehouseID As Integer
    Public Property WarehouseCode As String
    Public Property WarehouseNameAr As String
    Public Property WarehouseNameEn As String
    Public Property WarehouseTypeID As Integer
    Public Property WarehouseTypeName As String
    Public Property BranchID As Integer?
    Public Property BranchName As String
    Public Property IsDefault As Boolean
    Public Property IsActive As Boolean
    Public Property CreatedAt As DateTime
End Class

' ═══════════ 2) Warehouse Types ═══════════
Public Class WarehouseTypeDto
    Public Property WarehouseTypeID As Integer
    Public Property TypeCode As String
    Public Property TypeNameAr As String
    Public Property TypeNameEn As String
End Class

' ═══════════ 3) Branches ═══════════
Public Class BranchDto
    Public Property BranchID As Integer
    Public Property CompanyID As Integer
    Public Property CompanyName As String
    Public Property BranchCode As String
    Public Property BranchNameAr As String
    Public Property BranchNameEn As String
    Public Property IsActive As Boolean
    Public Property CreatedAt As DateTime
    Public Property BranchName As String
    Public Property RegionName As String
End Class

' ═══════════ 4) Workers ═══════════
Public Class WorkerDto
    Public Property WorkerID As Integer
    Public Property WorkerCode As String
    Public Property WorkerNameAr As String
    Public Property WorkerNameEn As String
    Public Property WarehouseRole As String
    Public Property WarehouseRoleAr As String
    Public Property PhoneNumber As String
    Public Property NationalID As String
    Public Property HireDate As Date?
    Public Property DefaultWarehouseID As Integer?
    Public Property DefaultWarehouseName As String
    Public Property Notes As String
    Public Property IsActive As Boolean
    Public Property CreatedAt As DateTime
End Class

' ═══════════ Lookup Helpers ═══════════
Public Class SimpleLookup
    Public Property ID As Integer
    Public Property Code As String
    Public Property Name As String
    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class