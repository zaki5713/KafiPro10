Imports Microsoft.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class WarehouseMasterRepository

    ' ═══════════════════════════════════════════════════════════
    '  1) WAREHOUSES
    ' ═══════════════════════════════════════════════════════════
    Public Function GetWarehouses(Optional onlyActive As Boolean = False) As List(Of WarehouseDto)
        Dim result As New List(Of WarehouseDto)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Core].[usp_Warehouse_GetAll]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@OnlyActive", If(onlyActive, 1, 0))
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(New WarehouseDto With {
                            .WarehouseID = CInt(rd("WarehouseID")),
                            .WarehouseCode = CStr(rd("WarehouseCode")),
                            .WarehouseNameAr = CStr(rd("WarehouseNameAr")),
                            .WarehouseNameEn = If(IsDBNull(rd("WarehouseNameEn")), "", CStr(rd("WarehouseNameEn"))),
                            .WarehouseTypeID = CInt(rd("WarehouseTypeID")),
                            .WarehouseTypeName = If(IsDBNull(rd("WarehouseTypeNameAr")), "", CStr(rd("WarehouseTypeNameAr"))),
                            .IsDefault = CBool(rd("IsDefault")),
                            .IsActive = CBool(rd("IsActive"))
                        })
                    End While
                End Using
            End Using
        End Using
        Return result
    End Function

    Public Sub InsertWarehouse(code As String, nameAr As String, nameEn As String,
                               typeID As Integer, isDefault As Boolean, isActive As Boolean, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Core].[usp_Warehouse_Insert]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WarehouseCode", code)
                cmd.Parameters.AddWithValue("@WarehouseNameAr", nameAr)
                cmd.Parameters.AddWithValue("@WarehouseNameEn", If(String.IsNullOrEmpty(nameEn), CObj(DBNull.Value), nameEn))
                cmd.Parameters.AddWithValue("@WarehouseTypeID", typeID)
                cmd.Parameters.AddWithValue("@IsDefault", If(isDefault, 1, 0))
                cmd.Parameters.AddWithValue("@IsActive", If(isActive, 1, 0))
                cmd.Parameters.AddWithValue("@CreatedBy", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub UpdateWarehouse(id As Integer, code As String, nameAr As String, nameEn As String,
                               typeID As Integer, isDefault As Boolean, isActive As Boolean, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Core].[usp_Warehouse_Update]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WarehouseID", id)
                cmd.Parameters.AddWithValue("@WarehouseCode", code)
                cmd.Parameters.AddWithValue("@WarehouseNameAr", nameAr)
                cmd.Parameters.AddWithValue("@WarehouseNameEn", If(String.IsNullOrEmpty(nameEn), CObj(DBNull.Value), nameEn))
                cmd.Parameters.AddWithValue("@WarehouseTypeID", typeID)
                cmd.Parameters.AddWithValue("@IsDefault", If(isDefault, 1, 0))
                cmd.Parameters.AddWithValue("@IsActive", If(isActive, 1, 0))
                cmd.Parameters.AddWithValue("@ModifiedBy", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub DeleteWarehouse(id As Integer, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Core].[usp_Warehouse_Delete]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WarehouseID", id)
                cmd.Parameters.AddWithValue("@DeletedBy", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  2) WAREHOUSE TYPES
    ' ═══════════════════════════════════════════════════════════
    Public Function GetWarehouseTypes() As List(Of WarehouseTypeDto)
        Dim result As New List(Of WarehouseTypeDto)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("SELECT WarehouseTypeID, TypeCode, TypeNameAr, TypeNameEn FROM [Core].[WarehouseType] ORDER BY TypeCode", conn)
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(New WarehouseTypeDto With {
                            .WarehouseTypeID = CInt(rd("WarehouseTypeID")),
                            .TypeCode = CStr(rd("TypeCode")),
                            .TypeNameAr = CStr(rd("TypeNameAr")),
                            .TypeNameEn = CStr(rd("TypeNameEn"))
                        })
                    End While
                End Using
            End Using
        End Using
        Return result
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  3) BRANCHES
    ' ═══════════════════════════════════════════════════════════
    Public Function GetBranches() As List(Of BranchDto)
        Dim result As New List(Of BranchDto)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand(
                "SELECT b.BranchID, b.CompanyID, c.CompanyNameAr, b.BranchCode, b.BranchNameAr, b.BranchNameEn, b.IsActive, b.CreatedAt
                 FROM [Core].[Branch] b
                 INNER JOIN [Core].[Company] c ON c.CompanyID = b.CompanyID
                 WHERE b.IsDeleted = 0 ORDER BY b.BranchCode", conn)
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(New BranchDto With {
                            .BranchID = CInt(rd("BranchID")),
                            .CompanyID = CInt(rd("CompanyID")),
                            .CompanyName = CStr(rd("CompanyNameAr")),
                            .BranchCode = CStr(rd("BranchCode")),
                            .BranchNameAr = CStr(rd("BranchNameAr")),
                            .BranchNameEn = If(IsDBNull(rd("BranchNameEn")), "", CStr(rd("BranchNameEn"))),
                            .IsActive = CBool(rd("IsActive")),
                            .CreatedAt = CDate(rd("CreatedAt"))
                        })
                    End While
                End Using
            End Using
        End Using
        Return result
    End Function

    Public Sub InsertBranch(companyID As Integer, code As String, nameAr As String, nameEn As String, isActive As Boolean, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand(
                "INSERT INTO [Core].[Branch] (CompanyID, BranchCode, BranchNameAr, BranchNameEn, IsActive, IsDeleted, CreatedBy, CreatedAt)
                 VALUES (@CompanyID, @Code, @NameAr, @NameEn, @IsActive, 0, @UserID, SYSDATETIME())", conn)
                cmd.Parameters.AddWithValue("@CompanyID", companyID)
                cmd.Parameters.AddWithValue("@Code", code)
                cmd.Parameters.AddWithValue("@NameAr", nameAr)
                cmd.Parameters.AddWithValue("@NameEn", If(String.IsNullOrEmpty(nameEn), CObj(DBNull.Value), nameEn))
                cmd.Parameters.AddWithValue("@IsActive", If(isActive, 1, 0))
                cmd.Parameters.AddWithValue("@UserID", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub UpdateBranch(id As Integer, companyID As Integer, code As String, nameAr As String, nameEn As String, isActive As Boolean, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand(
                "UPDATE [Core].[Branch]
                 SET CompanyID=@CompanyID, BranchCode=@Code, BranchNameAr=@NameAr, BranchNameEn=@NameEn,
                     IsActive=@IsActive, ModifiedBy=@UserID, ModifiedAt=SYSDATETIME()
                 WHERE BranchID=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", id)
                cmd.Parameters.AddWithValue("@CompanyID", companyID)
                cmd.Parameters.AddWithValue("@Code", code)
                cmd.Parameters.AddWithValue("@NameAr", nameAr)
                cmd.Parameters.AddWithValue("@NameEn", If(String.IsNullOrEmpty(nameEn), CObj(DBNull.Value), nameEn))
                cmd.Parameters.AddWithValue("@IsActive", If(isActive, 1, 0))
                cmd.Parameters.AddWithValue("@UserID", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub DeleteBranch(id As Integer, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand(
                "UPDATE [Core].[Branch] SET IsDeleted=1, ModifiedBy=@UserID, ModifiedAt=SYSDATETIME() WHERE BranchID=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", id)
                cmd.Parameters.AddWithValue("@UserID", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  4) WORKERS
    ' ═══════════════════════════════════════════════════════════
    Public Function GetWorkers(Optional onlyActive As Boolean = False) As List(Of WorkerDto)
        Dim result As New List(Of WorkerDto)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Warehouses].[usp_Worker_GetAll]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@OnlyActive", If(onlyActive, 1, 0))
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(New WorkerDto With {
                            .WorkerID = CInt(rd("WorkerID")),
                            .WorkerCode = CStr(rd("WorkerCode")),
                            .WorkerNameAr = CStr(rd("WorkerNameAr")),
                            .WorkerNameEn = If(IsDBNull(rd("WorkerNameEn")), "", CStr(rd("WorkerNameEn"))),
                            .WarehouseRole = CStr(rd("WarehouseRole")),
                            .WarehouseRoleAr = CStr(rd("WarehouseRoleAr")),
                            .PhoneNumber = If(IsDBNull(rd("PhoneNumber")), "", CStr(rd("PhoneNumber"))),
                            .NationalID = If(IsDBNull(rd("NationalID")), "", CStr(rd("NationalID"))),
                            .HireDate = If(IsDBNull(rd("HireDate")), Nothing, CDate(rd("HireDate"))),
                            .DefaultWarehouseID = If(IsDBNull(rd("DefaultWarehouseID")), Nothing, CInt(rd("DefaultWarehouseID"))),
                            .DefaultWarehouseName = If(IsDBNull(rd("DefaultWarehouseName")), "", CStr(rd("DefaultWarehouseName"))),
                            .Notes = If(IsDBNull(rd("Notes")), "", CStr(rd("Notes"))),
                            .IsActive = CBool(rd("IsActive")),
                            .CreatedAt = CDate(rd("CreatedAt"))
                        })
                    End While
                End Using
            End Using
        End Using
        Return result
    End Function

    Public Sub InsertWorker(code As String, nameAr As String, nameEn As String, role As String,
                            phone As String, nationalID As String, hireDate As Date?,
                            defaultWhID As Integer?, notes As String, isActive As Boolean, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Warehouses].[usp_Worker_Insert]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WorkerCode", code)
                cmd.Parameters.AddWithValue("@WorkerNameAr", nameAr)
                cmd.Parameters.AddWithValue("@WorkerNameEn", If(String.IsNullOrEmpty(nameEn), CObj(DBNull.Value), nameEn))
                cmd.Parameters.AddWithValue("@WarehouseRole", role)
                cmd.Parameters.AddWithValue("@PhoneNumber", If(String.IsNullOrEmpty(phone), CObj(DBNull.Value), phone))
                cmd.Parameters.AddWithValue("@NationalID", If(String.IsNullOrEmpty(nationalID), CObj(DBNull.Value), nationalID))
                cmd.Parameters.AddWithValue("@HireDate", If(hireDate.HasValue, CObj(hireDate.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@DefaultWarehouseID", If(defaultWhID.HasValue, CObj(defaultWhID.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(notes), CObj(DBNull.Value), notes))
                cmd.Parameters.AddWithValue("@IsActive", If(isActive, 1, 0))
                cmd.Parameters.AddWithValue("@CreatedBy", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub UpdateWorker(id As Integer, code As String, nameAr As String, nameEn As String, role As String,
                            phone As String, nationalID As String, hireDate As Date?,
                            defaultWhID As Integer?, notes As String, isActive As Boolean, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Warehouses].[usp_Worker_Update]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WorkerID", id)
                cmd.Parameters.AddWithValue("@WorkerCode", code)
                cmd.Parameters.AddWithValue("@WorkerNameAr", nameAr)
                cmd.Parameters.AddWithValue("@WorkerNameEn", If(String.IsNullOrEmpty(nameEn), CObj(DBNull.Value), nameEn))
                cmd.Parameters.AddWithValue("@WarehouseRole", role)
                cmd.Parameters.AddWithValue("@PhoneNumber", If(String.IsNullOrEmpty(phone), CObj(DBNull.Value), phone))
                cmd.Parameters.AddWithValue("@NationalID", If(String.IsNullOrEmpty(nationalID), CObj(DBNull.Value), nationalID))
                cmd.Parameters.AddWithValue("@HireDate", If(hireDate.HasValue, CObj(hireDate.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@DefaultWarehouseID", If(defaultWhID.HasValue, CObj(defaultWhID.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(notes), CObj(DBNull.Value), notes))
                cmd.Parameters.AddWithValue("@IsActive", If(isActive, 1, 0))
                cmd.Parameters.AddWithValue("@ModifiedBy", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub DeleteWorker(id As Integer, userID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Warehouses].[usp_Worker_Delete]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WorkerID", id)
                cmd.Parameters.AddWithValue("@DeletedBy", userID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class