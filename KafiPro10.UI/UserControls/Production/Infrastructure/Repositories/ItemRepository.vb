Imports System.Data
Imports System.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class ItemRepository
        Inherits BaseRepository
        Implements IItemRepository

        ' ============================================================
        '  Get All
        ' ============================================================
        Public Function GetAll() As List(Of ItemDto) _
            Implements IItemRepository.GetAll

            Dim result As New List(Of ItemDto)()

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_Item_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapItem(reader))
                        End While
                    End Using
                End Using
            End Using

            Return result
        End Function

        ' ============================================================
        '  Get By Id
        ' ============================================================
        Public Function GetById(itemId As Integer) As ItemDto _
            Implements IItemRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_Item_GetById")
                    cmd.Parameters.AddWithValue("@ItemID", itemId)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim nw As Decimal? = Nothing
                            Dim pw As Decimal? = Nothing
                            Dim gw As Decimal? = Nothing

                            If Not reader.IsDBNull(8) Then nw = reader.GetDecimal(8)
                            If Not reader.IsDBNull(9) Then pw = reader.GetDecimal(9)
                            If Not reader.IsDBNull(10) Then gw = reader.GetDecimal(10)

                            Return New ItemDto With {
                                .ItemID = reader.GetInt32(0),
                                .ItemCode = GetString(reader, 1),
                                .ItemNameAr = GetString(reader, 2),
                                .ItemNameEn = GetString(reader, 3),
                                .ItemTypeID = reader.GetInt32(4),
                                .InventoryUnitID = reader.GetInt32(5),
                                .FinancialUnitID = reader.GetInt32(6),
                                .IsActive = reader.GetBoolean(7),
                                .NetWeight = nw,
                                .PackageWeight = pw,
                                .GrossWeight = gw
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ============================================================
        '  Insert  → يرجّع ItemID الجديد
        ' ============================================================
        Public Function Insert(item As ItemDto, userId As Integer) As Integer _
            Implements IItemRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_Item_Insert")
                    With cmd.Parameters
                        .AddWithValue("@ItemCode", item.ItemCode)
                        .AddWithValue("@ItemNameAr", item.ItemNameAr)
                        .AddWithValue("@ItemNameEn", item.ItemNameEn)
                        .AddWithValue("@ItemTypeID", item.ItemTypeID)
                        .AddWithValue("@InventoryUnitID", item.InventoryUnitID)
                        .AddWithValue("@FinancialUnitID", item.FinancialUnitID)
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@NetWeight", If(item.NetWeight.HasValue, CObj(item.NetWeight.Value), CObj(DBNull.Value)))
                        .AddWithValue("@PackageWeight", If(item.PackageWeight.HasValue, CObj(item.PackageWeight.Value), CObj(DBNull.Value)))
                        .AddWithValue("@CreatedBy", userId)
                    End With

                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ============================================================
        '  Update
        ' ============================================================
        Public Sub Update(item As ItemDto, userId As Integer) _
            Implements IItemRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_Item_Update")
                    With cmd.Parameters
                        .AddWithValue("@ItemID", item.ItemID)
                        .AddWithValue("@ItemCode", item.ItemCode)
                        .AddWithValue("@ItemNameAr", item.ItemNameAr)
                        .AddWithValue("@ItemNameEn", item.ItemNameEn)
                        .AddWithValue("@ItemTypeID", item.ItemTypeID)
                        .AddWithValue("@InventoryUnitID", item.InventoryUnitID)
                        .AddWithValue("@FinancialUnitID", item.FinancialUnitID)
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@NetWeight", If(item.NetWeight.HasValue, CObj(item.NetWeight.Value), CObj(DBNull.Value)))
                        .AddWithValue("@PackageWeight", If(item.PackageWeight.HasValue, CObj(item.PackageWeight.Value), CObj(DBNull.Value)))
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ============================================================
        '  Delete (Soft)
        ' ============================================================
        Public Sub Delete(itemId As Integer, userId As Integer) _
            Implements IItemRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_Item_Delete")
                    cmd.Parameters.AddWithValue("@ItemID", itemId)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ============================================================
        '  Lookups
        ' ============================================================
        Public Function GetItemTypes() As List(Of ItemTypeDto) _
            Implements IItemRepository.GetItemTypes

            Dim result As New List(Of ItemTypeDto)()

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_ItemType_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(New ItemTypeDto With {
                                .ItemTypeID = reader.GetInt32(0),
                                .ItemTypeCode = GetString(reader, 1),
                                .ItemTypeNameAr = GetString(reader, 2),
                                .ItemTypeNameEn = GetString(reader, 3)
                            })
                        End While
                    End Using
                End Using
            End Using

            Return result
        End Function

        Public Function GetUnits() As List(Of UnitOfMeasureDto) _
            Implements IItemRepository.GetUnits

            Dim result As New List(Of UnitOfMeasureDto)()

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_UnitOfMeasure_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(New UnitOfMeasureDto With {
                                .UnitOfMeasureID = reader.GetInt32(0),
                                .UnitNameAr = GetString(reader, 1),
                                .UnitNameEn = GetString(reader, 2)
                            })
                        End While
                    End Using
                End Using
            End Using

            Return result
        End Function
        Public Function GetFinishedGoods() As List(Of ItemDto) _
            Implements IItemRepository.GetFinishedGoods

            Dim result As New List(Of ItemDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_Item_GetFinishedGoods")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapItem(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        Public Function GetMaterials() As List(Of ItemDto) _
            Implements IItemRepository.GetMaterials

            Dim result As New List(Of ItemDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_Item_GetMaterials")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapItem(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function
        ' ============================================================
        '  Helper: تحويل صف الـ Reader → ItemDto
        '  ⚠️ لازم يطابق ترتيب الأعمدة في [Core].[usp_Item_GetAll] بالظبط
        ' ============================================================
        Private Function MapItem(reader As SqlDataReader) As ItemDto
            Dim nw As Decimal? = Nothing
            Dim pw As Decimal? = Nothing
            Dim gw As Decimal? = Nothing

            If reader.FieldCount > 11 AndAlso Not reader.IsDBNull(11) Then nw = reader.GetDecimal(11)
            If reader.FieldCount > 12 AndAlso Not reader.IsDBNull(12) Then pw = reader.GetDecimal(12)
            If reader.FieldCount > 13 AndAlso Not reader.IsDBNull(13) Then gw = reader.GetDecimal(13)

            Return New ItemDto With {
                .ItemID = reader.GetInt32(0),
                .ItemCode = GetString(reader, 1),
                .ItemNameAr = GetString(reader, 2),
                .ItemNameEn = GetString(reader, 3),
                .ItemTypeID = reader.GetInt32(4),
                .ItemTypeNameAr = GetString(reader, 5),
                .InventoryUnitID = reader.GetInt32(6),
                .InventoryUnitName = GetString(reader, 7),
                .FinancialUnitID = reader.GetInt32(8),
                .FinancialUnitName = GetString(reader, 9),
                .IsActive = reader.GetBoolean(10),
                .NetWeight = nw,
                .PackageWeight = pw,
                .GrossWeight = gw
            }
        End Function
    End Class

End Namespace