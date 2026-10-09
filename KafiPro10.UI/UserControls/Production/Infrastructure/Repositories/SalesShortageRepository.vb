Imports System
Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class SalesShortageRepository

    ' ⚠️ لو DBConnection عندك مختلف، عدّل السطر ده بس
    Private Shared Function ConnStr() As String
        Return DBConnection.GetConnectionString
    End Function

    ' ---------- Query ----------
   Public Function GetAll(Optional statusFilter As Integer = 0,
                       Optional sourceFilter As Integer = 0,
                       Optional searchText As String = Nothing) As List(Of SalesShortageDTO)

    Dim list As New List(Of SalesShortageDTO)()
    Dim sql As String =
"SELECT s.ShortageID, s.ItemID,
        i.ItemCode,
        i.ItemNameAr AS ItemName,
        ISNULL(u.UnitNameAr,'') AS UnitName,
        s.WarehouseID,
        ISNULL(w.WarehouseNameAr,'') AS WarehouseName,
        s.RequiredQty, s.AvailableQty,
        s.SourceType, s.ReferenceNo, s.RequiredDate,
        s.Status, s.Notes, s.CreatedAt
 FROM   Production.SalesShortage s
        LEFT JOIN Core.Item i           ON i.ItemID          = s.ItemID
        LEFT JOIN Core.UnitOfMeasure u  ON u.UnitOfMeasureID = i.InventoryUnitID
        LEFT JOIN Core.Warehouse w      ON w.WarehouseID     = s.WarehouseID
 WHERE  (@Status = 0 OR s.Status = @Status)
    AND (@Source = 0 OR s.SourceType = @Source)
    AND (@Search IS NULL OR @Search = ''
         OR i.ItemCode     LIKE '%' + @Search + '%'
         OR i.ItemNameAr   LIKE '%' + @Search + '%'
         OR i.ItemNameEn   LIKE '%' + @Search + '%'
         OR s.ReferenceNo  LIKE '%' + @Search + '%')
 ORDER BY s.CreatedAt DESC"

    Using cn As New SqlConnection(ConnStr())
        Using cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@Status", statusFilter)
            cmd.Parameters.AddWithValue("@Source", sourceFilter)
            cmd.Parameters.AddWithValue("@Search", If(searchText, ""))
            cn.Open()
            Using rd As SqlDataReader = cmd.ExecuteReader()
                While rd.Read()
                    list.Add(MapRow(rd))
                End While
            End Using
        End Using
    End Using
    Return list
End Function

   Public Function GetById(id As Integer) As SalesShortageDTO
    Dim sql As String =
"SELECT s.ShortageID, s.ItemID,
        i.ItemCode,
        i.ItemNameAr AS ItemName,
        ISNULL(u.UnitNameAr,'') AS UnitName,
        s.WarehouseID,
        ISNULL(w.WarehouseNameAr,'') AS WarehouseName,
        s.RequiredQty, s.AvailableQty,
        s.SourceType, s.ReferenceNo, s.RequiredDate,
        s.Status, s.Notes, s.CreatedAt
 FROM   Production.SalesShortage s
        LEFT JOIN Core.Item i           ON i.ItemID          = s.ItemID
        LEFT JOIN Core.UnitOfMeasure u  ON u.UnitOfMeasureID = i.InventoryUnitID
        LEFT JOIN Core.Warehouse w      ON w.WarehouseID     = s.WarehouseID
 WHERE  s.ShortageID = @ID"
    Using cn As New SqlConnection(ConnStr())
        Using cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ID", id)
            cn.Open()
            Using rd As SqlDataReader = cmd.ExecuteReader()
                If rd.Read() Then Return MapRow(rd)
            End Using
        End Using
    End Using
    Return Nothing
End Function

    ' ---------- Insert / Update / Delete ----------
    Public Function Insert(d As SalesShortageDTO, userId As Integer) As Integer
        Dim sql As String =
"INSERT INTO Production.SalesShortage
 (ItemID, WarehouseID, RequiredQty, AvailableQty, SourceType,
  ReferenceNo, RequiredDate, Status, Notes, CreatedBy, CreatedAt)
 VALUES
 (@ItemID, @WhID, @Req, @Avail, @Src,
  @Ref, @ReqDate, @Status, @Notes, @User, SYSDATETIME());
 SELECT CAST(SCOPE_IDENTITY() AS INT);"
        Using cn As New SqlConnection(ConnStr())
            Using cmd As New SqlCommand(sql, cn)
              cmd.Parameters.AddWithValue("@ItemID",  d.ItemID)
cmd.Parameters.AddWithValue("@WhID",    If(d.WarehouseID.HasValue, CObj(d.WarehouseID.Value), DBNull.Value))
cmd.Parameters.AddWithValue("@Req",     d.RequiredQty)
cmd.Parameters.AddWithValue("@Avail",   d.AvailableQty)
cmd.Parameters.AddWithValue("@Src",     d.SourceType)
cmd.Parameters.AddWithValue("@Ref",     If(String.IsNullOrWhiteSpace(d.ReferenceNo), CObj(DBNull.Value), d.ReferenceNo))
cmd.Parameters.AddWithValue("@ReqDate", If(d.RequiredDate.HasValue, CObj(d.RequiredDate.Value), DBNull.Value))
cmd.Parameters.AddWithValue("@Status",  d.Status)
cmd.Parameters.AddWithValue("@Notes",   If(String.IsNullOrWhiteSpace(d.Notes), CObj(DBNull.Value), d.Notes))
cmd.Parameters.AddWithValue("@User",    If(userId > 0, CObj(userId), DBNull.Value))
                cn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub Update(d As SalesShortageDTO, userId As Integer)
        Dim sql As String =
"UPDATE Production.SalesShortage SET
   ItemID = @ItemID, WarehouseID = @WhID,
   RequiredQty = @Req, AvailableQty = @Avail,
   SourceType = @Src, ReferenceNo = @Ref,
   RequiredDate = @ReqDate, Status = @Status,
   Notes = @Notes, UpdatedBy = @User, UpdatedAt = SYSDATETIME()
 WHERE ShortageID = @ID"
        Using cn As New SqlConnection(ConnStr())
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", d.ShortageID)
                cmd.Parameters.AddWithValue("@ItemID", d.ItemID)
                cmd.Parameters.AddWithValue("@WhID", If(CType(d.WarehouseID, Object), DBNull.Value))
                cmd.Parameters.AddWithValue("@Req", d.RequiredQty)
                cmd.Parameters.AddWithValue("@Avail", d.AvailableQty)
                cmd.Parameters.AddWithValue("@Src", d.SourceType)
                cmd.Parameters.AddWithValue("@Ref", If(CStr(d.ReferenceNo), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@ReqDate", If(d.RequiredDate.HasValue, CObj(d.RequiredDate.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@Status", d.Status)
                cmd.Parameters.AddWithValue("@Notes", If(CStr(d.Notes), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@User", userId)
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(id As Integer)
        Using cn As New SqlConnection(ConnStr())
            Using cmd As New SqlCommand("DELETE FROM Production.SalesShortage WHERE ShortageID=@ID", cn)
                cmd.Parameters.AddWithValue("@ID", id)
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub SetStatus(id As Integer, newStatus As Integer, userId As Integer)
        Dim sql As String =
"UPDATE Production.SalesShortage
 SET Status = @S, UpdatedBy = @U, UpdatedAt = SYSDATETIME()
 WHERE ShortageID = @ID"
        Using cn As New SqlConnection(ConnStr())
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@S", newStatus)
                cmd.Parameters.AddWithValue("@U", userId)
                cmd.Parameters.AddWithValue("@ID", id)
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function HasOpenForItem(itemId As Integer) As Boolean
        Using cn As New SqlConnection(ConnStr())
            Using cmd As New SqlCommand(
                "SELECT COUNT(1) FROM Production.SalesShortage WHERE ItemID=@I AND Status IN (1,2)", cn)
                cmd.Parameters.AddWithValue("@I", itemId)
                cn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    ' ---------- Lookups ----------
    ' ⚠️ منتج تام = FINISHED_GOOD (ItemTypeID = 3)
Public Function GetItemsLookup() As DataTable
    Return Fill(
        "SELECT ItemID, ItemCode, ItemNameAr AS ItemName, ItemNameEn
         FROM Core.Item
         WHERE ISNULL(IsActive,1)=1
           AND ISNULL(IsDeleted,0)=0
           AND ItemTypeID = 3
         ORDER BY ItemNameAr")
End Function

    Public Function GetWarehousesLookup() As DataTable
    Return Fill(
        "SELECT WarehouseID, WarehouseNameAr, WarehouseNameEn
         FROM Core.Warehouse
         WHERE ISNULL(IsActive,1)=1 AND ISNULL(IsDeleted,0)=0
         ORDER BY WarehouseID")
End Function

    ' ---------- آلي من المخزون ----------
    ' ⚠️ ASSUMPTION: Core.Item فيها عمود ReorderPoint — لو مش موجود غيّر الشرط
    Public Function ComputeLowStock() As List(Of SalesShortageDTO)
        Dim list As New List(Of SalesShortageDTO)()
        Dim sql As String =
"SELECT i.ItemID, i.ItemCode,
        i.ItemNameAr AS ItemName,
        ISNULL(u.UnitNameAr,'') AS UnitName,
        r.WarehouseID,
        r.ReorderPoint,
        ISNULL(sb.AvailableQty, 0) AS AvailableQty
 FROM   Sales.ItemReorderPoint r
        INNER JOIN Core.Item i           ON i.ItemID          = r.ItemID
        LEFT  JOIN Core.UnitOfMeasure u  ON u.UnitOfMeasureID = i.InventoryUnitID
        LEFT  JOIN Inventory.StockBalance sb
                ON sb.ItemID = r.ItemID
               AND (r.WarehouseID IS NULL OR sb.WarehouseID = r.WarehouseID)
 WHERE  r.IsActive = 1
    AND ISNULL(i.IsActive,1)=1
    AND ISNULL(i.IsDeleted,0)=0
    AND ISNULL(sb.AvailableQty, 0) < r.ReorderPoint
 ORDER BY i.ItemNameAr"

        Using cn As New SqlConnection(ConnStr())
            Using cmd As New SqlCommand(sql, cn)
                cn.Open()
                Using rd As SqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        Dim d As New SalesShortageDTO()
                        d.ItemID = CInt(rd("ItemID"))
                        d.ItemCode = CStr(rd("ItemCode"))
                        d.ItemName = CStr(rd("ItemName"))
                        d.UnitName = CStr(rd("UnitName"))
                        d.WarehouseID = If(rd("WarehouseID") Is DBNull.Value, CType(Nothing, Integer?), CInt(rd("WarehouseID")))
                        d.AvailableQty = CDec(rd("AvailableQty"))
                        d.RequiredQty = CDec(rd("ReorderPoint"))
                        d.SourceType = 2
                        d.Status = 1
                        list.Add(d)
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    ' ---------- Helpers ----------
    Private Function Fill(sql As String) As DataTable
        Dim dt As New DataTable()
        Using conn As New SqlConnection(ConnStr())
            Using cmd As New SqlCommand(sql, conn)
                conn.Open()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Private Function MapRow(rd As SqlDataReader) As SalesShortageDTO
        Return New SalesShortageDTO With {
            .ShortageID = CInt(rd("ShortageID")),
            .ItemID = CInt(rd("ItemID")),
            .ItemCode = If(rd("ItemCode") Is DBNull.Value, "", CStr(rd("ItemCode"))),
            .ItemName = If(rd("ItemName") Is DBNull.Value, "", CStr(rd("ItemName"))),
            .UnitName = If(rd("UnitName") Is DBNull.Value, "", CStr(rd("UnitName"))),
            .WarehouseID = If(rd("WarehouseID") Is DBNull.Value, CType(Nothing, Integer?), CInt(rd("WarehouseID"))),
            .WarehouseName = If(rd("WarehouseName") Is DBNull.Value, "", CStr(rd("WarehouseName"))),
            .RequiredQty = CDec(rd("RequiredQty")),
            .AvailableQty = CDec(rd("AvailableQty")),
            .SourceType = CByte(rd("SourceType")),
            .ReferenceNo = If(rd("ReferenceNo") Is DBNull.Value, "", CStr(rd("ReferenceNo"))),
            .RequiredDate = If(rd("RequiredDate") Is DBNull.Value, CType(Nothing, DateTime?), CDate(rd("RequiredDate"))),
            .Status = CByte(rd("Status")),
            .Notes = If(rd("Notes") Is DBNull.Value, "", CStr(rd("Notes"))),
            .CreatedAt = If(rd("CreatedAt") Is DBNull.Value, CType(Nothing, DateTime?), CDate(rd("CreatedAt")))
        }
    End Function
End Class