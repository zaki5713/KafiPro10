Imports System.Data
Imports System.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class BOMRepository
        Inherits BaseRepository
        Implements IBOMRepository

        ' ══════════════════════════════════════════════════
        '  HEADER — GetAll
        ' ══════════════════════════════════════════════════
        Public Function GetAll() As List(Of BOMDto) _
            Implements IBOMRepository.GetAll

            Dim result As New List(Of BOMDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOM_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapBOM(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        ' ══════════════════════════════════════════════════
        '  HEADER — GetById
        ' ══════════════════════════════════════════════════
        Public Function GetById(bomId As Integer) As BOMDto _
            Implements IBOMRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOM_GetById")
                    cmd.Parameters.AddWithValue("@BOMID", bomId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New BOMDto With {
                                .BOMID = reader.GetInt32(0),
                                .BOMCode = GetString(reader, 1),
                                .FinishedItemID = reader.GetInt32(2),
                                .OutputQty = reader.GetDecimal(3),
                                .OutputUnitID = reader.GetInt32(4),
                                .WastagePercent = reader.GetDecimal(5),
                                .Notes = GetString(reader, 6),
                                .IsActive = reader.GetBoolean(7)
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ══════════════════════════════════════════════════
        '  HEADER — Insert
        ' ══════════════════════════════════════════════════
        Public Function Insert(item As BOMDto, userId As Integer) As Integer _
            Implements IBOMRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOM_Insert")
                    With cmd.Parameters
                        .AddWithValue("@BOMCode", item.BOMCode)
                        .AddWithValue("@FinishedItemID", item.FinishedItemID)
                        .AddWithValue("@OutputQty", item.OutputQty)
                        .AddWithValue("@OutputUnitID", item.OutputUnitID)
                        .AddWithValue("@WastagePercent", item.WastagePercent)
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@CreatedBy", userId)
                    End With
                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ══════════════════════════════════════════════════
        '  HEADER — Update
        ' ══════════════════════════════════════════════════
        Public Sub Update(item As BOMDto, userId As Integer) _
            Implements IBOMRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOM_Update")
                    With cmd.Parameters
                        .AddWithValue("@BOMID", item.BOMID)
                        .AddWithValue("@BOMCode", item.BOMCode)
                        .AddWithValue("@FinishedItemID", item.FinishedItemID)
                        .AddWithValue("@OutputQty", item.OutputQty)
                        .AddWithValue("@OutputUnitID", item.OutputUnitID)
                        .AddWithValue("@WastagePercent", item.WastagePercent)
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════
        '  HEADER — Delete (Soft)
        ' ══════════════════════════════════════════════════
        Public Sub Delete(bomId As Integer, userId As Integer) _
            Implements IBOMRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOM_Delete")
                    cmd.Parameters.AddWithValue("@BOMID", bomId)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════
        '  DETAILS — GetByBOMId
        ' ══════════════════════════════════════════════════
        Public Function GetDetails(bomId As Integer) As List(Of BOMDetailDto) _
            Implements IBOMRepository.GetDetails

            Dim result As New List(Of BOMDetailDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOMDetail_GetByBOMId")
                    cmd.Parameters.AddWithValue("@BOMID", bomId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(New BOMDetailDto With {
                                .DetailID = reader.GetInt32(0),
                                .BOMID = reader.GetInt32(1),
                                .MaterialItemID = reader.GetInt32(2),
                                .MaterialItemCode = GetString(reader, 3),
                                .MaterialItemNameAr = GetString(reader, 4),
                                .MaterialItemNameEn = GetString(reader, 5),
                                .Quantity = reader.GetDecimal(6),
                                .InventoryUnitID = reader.GetInt32(7),
                                .UnitName = GetString(reader, 8),
                                .Notes = GetString(reader, 9)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        ' ══════════════════════════════════════════════════
        '  DETAILS — GetById
        ' ══════════════════════════════════════════════════
        Public Function GetDetailById(detailId As Integer) As BOMDetailDto _
            Implements IBOMRepository.GetDetailById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOMDetail_GetById")
                    cmd.Parameters.AddWithValue("@DetailID", detailId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New BOMDetailDto With {
                                .DetailID = reader.GetInt32(0),
                                .BOMID = reader.GetInt32(1),
                                .MaterialItemID = reader.GetInt32(2),
                                .Quantity = reader.GetDecimal(3),
                                .Notes = GetString(reader, 4)
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ══════════════════════════════════════════════════
        '  DETAILS — Insert
        ' ══════════════════════════════════════════════════
        Public Function InsertDetail(item As BOMDetailDto, userId As Integer) As Integer _
            Implements IBOMRepository.InsertDetail

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOMDetail_Insert")
                    With cmd.Parameters
                        .AddWithValue("@BOMID", item.BOMID)
                        .AddWithValue("@MaterialItemID", item.MaterialItemID)
                        .AddWithValue("@Quantity", item.Quantity)
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@CreatedBy", userId)
                    End With
                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ══════════════════════════════════════════════════
        '  DETAILS — Update
        ' ══════════════════════════════════════════════════
        Public Sub UpdateDetail(item As BOMDetailDto, userId As Integer) _
            Implements IBOMRepository.UpdateDetail

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOMDetail_Update")
                    With cmd.Parameters
                        .AddWithValue("@DetailID", item.DetailID)
                        .AddWithValue("@MaterialItemID", item.MaterialItemID)
                        .AddWithValue("@Quantity", item.Quantity)
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════
        '  DETAILS — Delete
        ' ══════════════════════════════════════════════════
        Public Sub DeleteDetail(detailId As Integer) _
            Implements IBOMRepository.DeleteDetail

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_BOMDetail_Delete")
                    cmd.Parameters.AddWithValue("@DetailID", detailId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════
        '  Helper — Map BOM row
        '  ⚠️ الترتيب يطابق usp_BOM_GetAll
        ' ══════════════════════════════════════════════════
        Private Function MapBOM(reader As SqlDataReader) As BOMDto
            Return New BOMDto With {
                .BOMID = reader.GetInt32(0),
                .BOMCode = GetString(reader, 1),
                .FinishedItemID = reader.GetInt32(2),
                .FinishedItemCode = GetString(reader, 3),
                .FinishedItemNameAr = GetString(reader, 4),
                .OutputQty = reader.GetDecimal(5),
                .OutputUnitID = reader.GetInt32(6),
                .OutputUnitName = GetString(reader, 7),
                .WastagePercent = reader.GetDecimal(8),
                .Notes = GetString(reader, 9),
                .IsActive = reader.GetBoolean(10),
                .CreatedAt = reader.GetDateTime(11),
                .DetailsCount = reader.GetInt32(12)
            }
        End Function

    End Class

End Namespace