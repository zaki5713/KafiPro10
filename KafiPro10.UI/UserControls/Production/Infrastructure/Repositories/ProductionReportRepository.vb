Imports System.Collections.Generic
Imports KafiPro10.Solution.KafiPro.Production
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class ProductionReportRepository
        Inherits BaseRepository
        Implements IProductionReportRepository

        Private Const SP_DAILY_DETAIL As String = "Production.usp_ProductionReport_DailyDetail"

        Public Function GetDailyDetail(filter As ProductionReportFilterDto) As List(Of ProductionDailyRowDto) _
            Implements IProductionReportRepository.GetDailyDetail

            Dim list As New List(Of ProductionDailyRowDto)()

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, SP_DAILY_DETAIL)

                    cmd.Parameters.AddWithValue("@FromDate", filter.FromDate.Date)
                    cmd.Parameters.AddWithValue("@ToDate", filter.ToDate.Date)
                    cmd.Parameters.AddWithValue("@ShiftTypeID",
                        If(filter.ShiftTypeID.HasValue, CObj(filter.ShiftTypeID.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ProductionLineID",
                        If(filter.ProductionLineID.HasValue, CObj(filter.ProductionLineID.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ItemID",
                        If(filter.ItemID.HasValue, CObj(filter.ItemID.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@SupervisorID",
                        If(filter.SupervisorID.HasValue, CObj(filter.SupervisorID.Value), CObj(DBNull.Value)))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReader(reader))
                        End While
                    End Using
                End Using
            End Using

            Return list
        End Function

        Private Shared Function MapReader(r As SqlDataReader) As ProductionDailyRowDto
            Dim d As New ProductionDailyRowDto()

            d.LogID = GetLong(r, r.GetOrdinal("LogID")).GetValueOrDefault()
            d.ProductionDate = GetDate(r, r.GetOrdinal("ProductionDate")).GetValueOrDefault()

            d.ShiftTypeID = GetInt(r, r.GetOrdinal("ShiftTypeID")).GetValueOrDefault()
            d.ShiftCode = GetString(r, r.GetOrdinal("ShiftCode"))
            d.ShiftNameAr = GetString(r, r.GetOrdinal("ShiftNameAr"))
            d.ShiftStartTime = GetTimeSpan(r, r.GetOrdinal("ShiftStartTime"))
            d.ShiftEndTime = GetTimeSpan(r, r.GetOrdinal("ShiftEndTime"))
            d.IsOvertime = If(r.IsDBNull(r.GetOrdinal("IsOvertime")), False, r.GetBoolean(r.GetOrdinal("IsOvertime")))

            d.ProductionLineID = GetInt(r, r.GetOrdinal("ProductionLineID")).GetValueOrDefault()
            d.LineCode = GetString(r, r.GetOrdinal("LineCode"))
            d.LineNameAr = GetString(r, r.GetOrdinal("LineNameAr"))
            d.LineIdealCycleTime = GetDecimal(r, r.GetOrdinal("LineIdealCycleTime"))

            d.ItemID = GetInt(r, r.GetOrdinal("ItemID")).GetValueOrDefault()
            d.ItemCode = GetString(r, r.GetOrdinal("ItemCode"))
            d.ItemNameAr = GetString(r, r.GetOrdinal("ItemNameAr"))
            d.ItemUnitName = GetString(r, r.GetOrdinal("ItemUnitName"))

            d.SupervisorID = GetInt(r, r.GetOrdinal("SupervisorID")).GetValueOrDefault()
            d.SupervisorNameAr = GetString(r, r.GetOrdinal("SupervisorNameAr"))

            d.ProducedQty = GetDecimal(r, r.GetOrdinal("ProducedQty")).GetValueOrDefault()
            d.RawMaterialScrapQty = GetDecimal(r, r.GetOrdinal("RawMaterialScrapQty")).GetValueOrDefault()
            d.PackagingScrapQty = GetDecimal(r, r.GetOrdinal("PackagingScrapQty")).GetValueOrDefault()
            d.RejectedFGQty = GetDecimal(r, r.GetOrdinal("RejectedFGQty")).GetValueOrDefault()
            d.DowntimeMinutes = GetDecimal(r, r.GetOrdinal("DowntimeMinutes")).GetValueOrDefault()

            d.Notes = GetString(r, r.GetOrdinal("Notes"))
            d.CreatedAt = GetDate(r, r.GetOrdinal("CreatedAt"))

            Return d
        End Function

    End Class

End Namespace