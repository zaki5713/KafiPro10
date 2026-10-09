Imports System.Data
Imports System.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class ProductionLineRepository
        Inherits BaseRepository
        Implements IProductionLineRepository

        ' ============ Get All ============
        Public Function GetAll() As List(Of ProductionLineDto) _
            Implements IProductionLineRepository.GetAll

            Dim result As New List(Of ProductionLineDto)()

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionLine_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapLine(reader))
                        End While
                    End Using
                End Using
            End Using

            Return result
        End Function

        ' ============ Get By Id ============
        Public Function GetById(id As Integer) As ProductionLineDto _
            Implements IProductionLineRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionLine_GetById")
                    cmd.Parameters.AddWithValue("@ProductionLineID", id)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then Return MapLine(reader)
                    End Using
                End Using
            End Using

            Return Nothing
        End Function

        ' ============ Insert ============
        Public Function Insert(item As ProductionLineDto, userId As Integer) As Integer _
            Implements IProductionLineRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionLine_Insert")
                    With cmd.Parameters
                        .AddWithValue("@LineCode", item.LineCode)
                        .AddWithValue("@LineNameAr", item.LineNameAr)
                        .AddWithValue("@LineNameEn", If(String.IsNullOrEmpty(item.LineNameEn), CObj(DBNull.Value), item.LineNameEn))
                        .AddWithValue("@IdealCycleTime", If(item.IdealCycleTime.HasValue, CObj(item.IdealCycleTime.Value), CObj(DBNull.Value)))
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@CreatedBy", userId)
                    End With

                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ============ Update ============
        Public Sub Update(item As ProductionLineDto, userId As Integer) _
            Implements IProductionLineRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionLine_Update")
                    With cmd.Parameters
                        .AddWithValue("@ProductionLineID", item.ProductionLineID)
                        .AddWithValue("@LineCode", item.LineCode)
                        .AddWithValue("@LineNameAr", item.LineNameAr)
                        .AddWithValue("@LineNameEn", If(String.IsNullOrEmpty(item.LineNameEn), CObj(DBNull.Value), item.LineNameEn))
                        .AddWithValue("@IdealCycleTime", If(item.IdealCycleTime.HasValue, CObj(item.IdealCycleTime.Value), CObj(DBNull.Value)))
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ============ Delete ============
        Public Sub Delete(id As Integer, userId As Integer) _
            Implements IProductionLineRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionLine_Delete")
                    cmd.Parameters.AddWithValue("@ProductionLineID", id)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ============ Helper ============
        Private Function MapLine(reader As SqlDataReader) As ProductionLineDto
            Return New ProductionLineDto With {
                .ProductionLineID = reader.GetInt32(0),
                .LineCode = GetString(reader, 1),
                .LineNameAr = GetString(reader, 2),
                .LineNameEn = GetString(reader, 3),
                .IdealCycleTime = If(reader.IsDBNull(4), CType(Nothing, Decimal?), reader.GetDecimal(4)),
                .IsActive = reader.GetBoolean(5)
            }
        End Function

    End Class

End Namespace