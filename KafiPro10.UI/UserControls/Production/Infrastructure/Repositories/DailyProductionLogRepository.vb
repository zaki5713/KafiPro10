Imports System.Data
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports KafiPro10.Solution.KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    ''' <summary>
    ''' Repository: سجل الإنتاج اليومي
    ''' </summary>
    Public Class DailyProductionLogRepository
        Inherits BaseRepository
        Implements IDailyProductionLogRepository

        ' ═══════════════════════════════════════════════════════
        '  SPs Names
        ' ═══════════════════════════════════════════════════════
        Private Const SP_GET_ALL As String = "Production.usp_DailyProductionLog_GetAll"
        Private Const SP_GET_BYID As String = "Production.usp_DailyProductionLog_GetById"
        Private Const SP_INSERT As String = "Production.usp_DailyProductionLog_Insert"
        Private Const SP_UPDATE As String = "Production.usp_DailyProductionLog_Update"
        Private Const SP_DELETE As String = "Production.usp_DailyProductionLog_Delete"
        Private Const SP_SEARCH As String = "Production.usp_DailyProductionLog_Search"

        ' ═══════════════════════════════════════════════════════
        '  GetAll
        ' ═══════════════════════════════════════════════════════
        Public Function GetAll() As List(Of DailyProductionLogDto) _
            Implements IDailyProductionLogRepository.GetAll

            Dim list As New List(Of DailyProductionLogDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, SP_GET_ALL)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReader(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ' ═══════════════════════════════════════════════════════
        '  GetById
        ' ═══════════════════════════════════════════════════════
        Public Function GetById(logId As Long) As DailyProductionLogDto _
            Implements IDailyProductionLogRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, SP_GET_BYID)
                    cmd.Parameters.AddWithValue("@LogID", logId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then Return MapReader(reader)
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ═══════════════════════════════════════════════════════
        '  Insert  →  يرجع LogID الجديد
        ' ═══════════════════════════════════════════════════════
        Public Function Insert(item As DailyProductionLogDto, userId As Integer) As Long _
            Implements IDailyProductionLogRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, SP_INSERT)
                    AddCommonParams(cmd, item)
                    cmd.Parameters.AddWithValue("@CreatedBy", userId)

                    Dim result As Object = cmd.ExecuteScalar()
                    If result Is Nothing OrElse result Is DBNull.Value Then Return 0L
                    Return Convert.ToInt64(result)
                End Using
            End Using
        End Function


        ' ═══════════════════════════════════════════════════════
        '  Update
        ' ═══════════════════════════════════════════════════════
        Public Sub Update(item As DailyProductionLogDto, userId As Integer) _
            Implements IDailyProductionLogRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, SP_UPDATE)
                    cmd.Parameters.AddWithValue("@LogID", item.LogID)
                    AddCommonParams(cmd, item)
                    cmd.Parameters.AddWithValue("@ModifiedBy", userId)   ' ← التصحيح
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ═══════════════════════════════════════════════════════
        '  Delete
        ' ═══════════════════════════════════════════════════════
        Public Sub Delete(logId As Long, userId As Integer) _
            Implements IDailyProductionLogRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, SP_DELETE)
                    cmd.Parameters.AddWithValue("@LogID", logId)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ═══════════════════════════════════════════════════════
        '  Search
        ' ═══════════════════════════════════════════════════════
        Public Function Search(fromDate As Date?,
                               toDate As Date?,
                               shiftTypeId As Integer?,
                               lineId As Integer?,
                               itemId As Integer?) As List(Of DailyProductionLogDto) _
            Implements IDailyProductionLogRepository.Search

            Dim list As New List(Of DailyProductionLogDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, SP_SEARCH)

                    cmd.Parameters.AddWithValue("@FromDate",
                        If(fromDate.HasValue, CObj(fromDate.Value.Date), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ToDate",
                        If(toDate.HasValue, CObj(toDate.Value.Date), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ShiftTypeID",
                        If(shiftTypeId.HasValue, CObj(shiftTypeId.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ProductionLineID",
                        If(lineId.HasValue, CObj(lineId.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ItemID",
                        If(itemId.HasValue, CObj(itemId.Value), CObj(DBNull.Value)))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReader(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ' ═══════════════════════════════════════════════════════
        '  Helpers
        ' ═══════════════════════════════════════════════════════
        Private Shared Function MapReader(reader As SqlDataReader) As DailyProductionLogDto
            Dim dto As New DailyProductionLogDto()

            dto.LogID = GetLong(reader, reader.GetOrdinal("LogID")).GetValueOrDefault()
            dto.ProductionDate = GetDate(reader, reader.GetOrdinal("ProductionDate")).GetValueOrDefault()

            dto.ShiftTypeID = GetInt(reader, reader.GetOrdinal("ShiftTypeID")).GetValueOrDefault()
            dto.ShiftCode = GetString(reader, reader.GetOrdinal("ShiftCode"))
            dto.ShiftNameAr = GetString(reader, reader.GetOrdinal("ShiftNameAr"))

            dto.ProductionLineID = GetInt(reader, reader.GetOrdinal("ProductionLineID")).GetValueOrDefault()
            dto.LineCode = GetString(reader, reader.GetOrdinal("LineCode"))
            dto.ProductionLineNameAr = GetString(reader, reader.GetOrdinal("ProductionLineNameAr"))

            dto.ItemID = GetInt(reader, reader.GetOrdinal("ItemID")).GetValueOrDefault()
            dto.ItemCode = GetString(reader, reader.GetOrdinal("ItemCode"))
            dto.ItemNameAr = GetString(reader, reader.GetOrdinal("ItemNameAr"))

            dto.SupervisorID = GetInt(reader, reader.GetOrdinal("SupervisorID")).GetValueOrDefault()
            dto.SupervisorNameAr = GetString(reader, reader.GetOrdinal("SupervisorNameAr"))

            dto.ProducedQty = GetDecimal(reader, reader.GetOrdinal("ProducedQty")).GetValueOrDefault()
            dto.RawMaterialScrapQty = GetDecimal(reader, reader.GetOrdinal("RawMaterialScrapQty")).GetValueOrDefault()
            dto.PackagingScrapQty = GetDecimal(reader, reader.GetOrdinal("PackagingScrapQty")).GetValueOrDefault()
            dto.RejectedFGQty = GetDecimal(reader, reader.GetOrdinal("RejectedFGQty")).GetValueOrDefault()
            dto.DowntimeMinutes = GetDecimal(reader, reader.GetOrdinal("DowntimeMinutes")).GetValueOrDefault()

            dto.Notes = GetString(reader, reader.GetOrdinal("Notes"))
            dto.CreatedAt = GetDate(reader, reader.GetOrdinal("CreatedAt")).GetValueOrDefault()

            Return dto
        End Function

        Private Shared Sub AddCommonParams(cmd As SqlCommand, item As DailyProductionLogDto)
            cmd.Parameters.AddWithValue("@ProductionDate", item.ProductionDate.Date)
            cmd.Parameters.AddWithValue("@ShiftTypeID", item.ShiftTypeID)
            cmd.Parameters.AddWithValue("@ProductionLineID", item.ProductionLineID)
            cmd.Parameters.AddWithValue("@ItemID", item.ItemID)
            cmd.Parameters.AddWithValue("@SupervisorID", item.SupervisorID)
            cmd.Parameters.AddWithValue("@ProducedQty", item.ProducedQty)
            cmd.Parameters.AddWithValue("@RejectedFGQty", item.RejectedFGQty)
            cmd.Parameters.AddWithValue("@RawMaterialScrapQty", item.RawMaterialScrapQty)
            cmd.Parameters.AddWithValue("@PackagingScrapQty", item.PackagingScrapQty)
            cmd.Parameters.AddWithValue("@DowntimeMinutes", item.DowntimeMinutes)
            cmd.Parameters.AddWithValue("@Notes",
                If(String.IsNullOrWhiteSpace(item.Notes), CObj(DBNull.Value), item.Notes.Trim()))
        End Sub

    End Class

End Namespace