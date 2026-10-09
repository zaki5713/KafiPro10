Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs

Namespace KafiPro.Production.Application.Services

    Public Class ProductionReportService

        Private ReadOnly _repo As ProductionReportRepository

        Public Sub New()
            _repo = New ProductionReportRepository()
        End Sub

        Public Function GetReport(filter As ProductionReportFilterDto) As ProductionReportResult
            Dim rows = _repo.GetDailyDetail(filter)

            For Each r In rows
                ComputeRowMetrics(r, If(r.LineIdealCycleTime, 0@)
)
            Next

            Dim result As New ProductionReportResult With {
                .Rows = rows,
                .Summary = BuildSummary(filter, rows),
                .Daily = BuildDailySummary(rows),
                .Shifts = BuildShiftSummary(rows),
                .Lines = BuildLineSummary(rows),
                .Supervisors = BuildSupervisorSummary(rows),
                .Items = BuildItemSummary(rows)
            }

            Return result
        End Function

        ' ═══════════════════════════════════════════════════════
        '  ComputeRowMetrics
        ' ═══════════════════════════════════════════════════════
        Private Shared Sub ComputeRowMetrics(r As ProductionDailyRowDto, cyc As Decimal)

            ' 1. PlannedSeconds
            Dim planned As Decimal = 28800@
            If r.ShiftStartTime.HasValue AndAlso r.ShiftEndTime.HasValue Then
                Dim diff As TimeSpan = r.ShiftEndTime.Value - r.ShiftStartTime.Value
                If diff.TotalSeconds <= 0 Then diff = diff.Add(TimeSpan.FromHours(24))
                planned = CDec(diff.TotalSeconds)
            End If
            r.PlannedSeconds = planned

            ' 2. OperatingSeconds
            Dim op As Decimal = planned - r.DowntimeSeconds
            If op < 0@ Then op = 0@
            r.OperatingSeconds = op

            ' 3. Availability
            r.Availability = If(planned > 0@, op / planned, 0@)

            If cyc > 0@ AndAlso op > 0@ Then
                Dim perf As Decimal = (r.TotalOutputQty * cyc) / op
                If perf > 1@ Then perf = 1@
                If perf < 0@ Then perf = 0@
                r.Performance = perf
            Else
                r.Performance = 0@
            End If

            ' 5. Quality
            r.Quality = If(r.TotalOutputQty > 0@, r.ProducedQty / r.TotalOutputQty, 0@)

            ' 6. OEE
            r.OEE = r.Availability * r.Performance * r.Quality

            ' 7. Achievement %
            If cyc > 0@ AndAlso op > 0@ Then
                Dim theoMax As Decimal = op / cyc
                r.AchievementPct = If(theoMax > 0@, r.ProducedQty / theoMax, 0@)
            Else
                r.AchievementPct = 0@
            End If

            ' 8. Scrap %
            r.ScrapPct = If(r.ProducedQty > 0@, r.TotalScrapQty / r.ProducedQty, 0@)
        End Sub

        ' ═══════════════════════════════════════════════════════
        '  BuildSummary
        ' ═══════════════════════════════════════════════════════
        Private Shared Function BuildSummary(f As ProductionReportFilterDto,
                                             rows As List(Of ProductionDailyRowDto)) As ProductionSummaryDto

            Dim s As New ProductionSummaryDto With {
                .FromDate = f.FromDate,
                .ToDate = f.ToDate,
                .DaysCount = (f.ToDate - f.FromDate).Days + 1,
                .RecordsCount = rows.Count
            }

            If rows.Count = 0 Then Return s

            s.TotalProduced = rows.Sum(Function(x) x.ProducedQty)
            s.TotalRejectedFG = rows.Sum(Function(x) x.RejectedFGQty)
            s.TotalRawScrap = rows.Sum(Function(x) x.RawMaterialScrapQty)
            s.TotalPackScrap = rows.Sum(Function(x) x.PackagingScrapQty)
            s.TotalScrap = s.TotalRejectedFG + s.TotalRawScrap + s.TotalPackScrap
            s.TotalOutput = s.TotalProduced + s.TotalRejectedFG
            s.TotalDowntimeMinutes = rows.Sum(Function(x) x.DowntimeMinutes)
            s.TotalDowntimeSeconds = s.TotalDowntimeMinutes * 60@
            s.TotalPlannedSeconds = rows.Sum(Function(x) x.PlannedSeconds)
            s.TotalOperatingSeconds = rows.Sum(Function(x) x.OperatingSeconds)

            s.Availability = If(s.TotalPlannedSeconds > 0@, s.TotalOperatingSeconds / s.TotalPlannedSeconds, 0@)
            s.Quality = If(s.TotalOutput > 0@, s.TotalProduced / s.TotalOutput, 0@)

            Dim perfNum As Decimal = rows.Sum(Function(x) x.TotalOutputQty * x.LineIdealCycleTime.GetValueOrDefault())
            If s.TotalOperatingSeconds > 0@ Then
                Dim p As Decimal = perfNum / s.TotalOperatingSeconds
                s.Performance = Math.Min(1@, Math.Max(0@, p))
            Else
                s.Performance = 0@
            End If

            s.OEE = s.Availability * s.Performance * s.Quality

            Dim theoTotal As Decimal = 0@
            For Each r In rows
                If r.LineIdealCycleTime.HasValue AndAlso r.LineIdealCycleTime.Value > 0@ Then
                    theoTotal += r.OperatingSeconds / r.LineIdealCycleTime.Value
                End If
            Next
            s.AchievementPct = If(theoTotal > 0@, s.TotalProduced / theoTotal, 0@)
            s.ScrapPct = If(s.TotalProduced > 0@, s.TotalScrap / s.TotalProduced, 0@)

            Return s
        End Function

        ' ═══════════════════════════════════════════════════════
        '  BuildDailySummary
        ' ═══════════════════════════════════════════════════════
        Private Shared Function BuildDailySummary(rows As List(Of ProductionDailyRowDto)) As List(Of ProductionDailySummaryDto)
            Return rows.GroupBy(Function(x) x.ProductionDate) _
                       .Select(Function(g)
                                   Dim r As New ProductionDailySummaryDto With {
                                       .ProductionDate = g.Key,
                                       .ProducedQty = g.Sum(Function(x) x.ProducedQty),
                                       .RejectedFG = g.Sum(Function(x) x.RejectedFGQty),
                                       .TotalScrap = g.Sum(Function(x) x.TotalScrapQty),
                                       .DowntimeMinutes = g.Sum(Function(x) x.DowntimeMinutes),
                                       .OperatingSeconds = g.Sum(Function(x) x.OperatingSeconds),
                                       .RecordsCount = g.Count()
                                   }
                                   Dim planned As Decimal = g.Sum(Function(x) x.PlannedSeconds)
                                   Dim op As Decimal = r.OperatingSeconds
                                   Dim av As Decimal = If(planned > 0@, op / planned, 0@)
                                   Dim perfNum As Decimal = g.Sum(Function(x) x.TotalOutputQty * x.LineIdealCycleTime.GetValueOrDefault())
                                   Dim perf As Decimal = If(op > 0@, Math.Min(1@, perfNum / op), 0@)
                                   Dim totalOut As Decimal = r.ProducedQty + r.RejectedFG
                                   Dim q As Decimal = If(totalOut > 0@, r.ProducedQty / totalOut, 0@)
                                   r.OEE = av * perf * q
                                   Dim theo As Decimal = g.Sum(Function(x) If(x.LineIdealCycleTime.GetValueOrDefault() > 0@,
                                                                              x.OperatingSeconds / x.LineIdealCycleTime.Value,
                                                                              0@))
                                   r.AchievementPct = If(theo > 0@, r.ProducedQty / theo, 0@)
                                   Return r
                               End Function) _
                       .OrderBy(Function(x) x.ProductionDate) _
                       .ToList()
        End Function

        ' ═══════════════════════════════════════════════════════
        '  BuildShiftSummary
        ' ═══════════════════════════════════════════════════════
        Private Shared Function BuildShiftSummary(rows As List(Of ProductionDailyRowDto)) As List(Of ProductionShiftSummaryDto)
            Return rows.GroupBy(Function(x) x.ShiftTypeID) _
                       .Select(Function(g)
                                   Dim first = g.First()
                                   Dim r As New ProductionShiftSummaryDto With {
                                       .ShiftTypeID = g.Key,
                                       .ShiftCode = first.ShiftCode,
                                       .ShiftNameAr = first.ShiftNameAr,
                                       .ProducedQty = g.Sum(Function(x) x.ProducedQty),
                                       .RejectedFG = g.Sum(Function(x) x.RejectedFGQty),
                                       .TotalScrap = g.Sum(Function(x) x.TotalScrapQty),
                                       .DowntimeMinutes = g.Sum(Function(x) x.DowntimeMinutes),
                                       .RecordsCount = g.Count()
                                   }
                                   Dim planned As Decimal = g.Sum(Function(x) x.PlannedSeconds)
                                   Dim op As Decimal = g.Sum(Function(x) x.OperatingSeconds)
                                   Dim av As Decimal = If(planned > 0@, op / planned, 0@)
                                   Dim perfNum As Decimal = g.Sum(Function(x) x.TotalOutputQty * x.LineIdealCycleTime.GetValueOrDefault())
                                   Dim perf As Decimal = If(op > 0@, Math.Min(1@, perfNum / op), 0@)
                                   Dim totalOut As Decimal = r.ProducedQty + r.RejectedFG
                                   Dim q As Decimal = If(totalOut > 0@, r.ProducedQty / totalOut, 0@)
                                   r.OEE = av * perf * q
                                   Dim theo As Decimal = g.Sum(Function(x) If(x.LineIdealCycleTime.GetValueOrDefault() > 0@,
                                                                              x.OperatingSeconds / x.LineIdealCycleTime.Value,
                                                                              0@))
                                   r.AchievementPct = If(theo > 0@, r.ProducedQty / theo, 0@)
                                   Return r
                               End Function) _
                       .OrderBy(Function(x) x.ShiftTypeID) _
                       .ToList()
        End Function

        ' ═══════════════════════════════════════════════════════
        '  BuildLineSummary
        ' ═══════════════════════════════════════════════════════
        Private Shared Function BuildLineSummary(rows As List(Of ProductionDailyRowDto)) As List(Of ProductionLineSummaryDto)
            Return rows.GroupBy(Function(x) x.ProductionLineID) _
                       .Select(Function(g)
                                   Dim first = g.First()
                                   Dim r As New ProductionLineSummaryDto With {
                                       .ProductionLineID = g.Key,
                                       .LineCode = first.LineCode,
                                       .LineNameAr = first.LineNameAr,
                                       .ProducedQty = g.Sum(Function(x) x.ProducedQty),
                                       .RejectedFG = g.Sum(Function(x) x.RejectedFGQty),
                                       .TotalScrap = g.Sum(Function(x) x.TotalScrapQty),
                                       .DowntimeMinutes = g.Sum(Function(x) x.DowntimeMinutes),
                                       .RecordsCount = g.Count()
                                   }
                                   Dim planned As Decimal = g.Sum(Function(x) x.PlannedSeconds)
                                   Dim op As Decimal = g.Sum(Function(x) x.OperatingSeconds)
                                   Dim av As Decimal = If(planned > 0@, op / planned, 0@)
                                   Dim perfNum As Decimal = g.Sum(Function(x) x.TotalOutputQty * x.LineIdealCycleTime.GetValueOrDefault())
                                   Dim perf As Decimal = If(op > 0@, Math.Min(1@, perfNum / op), 0@)
                                   Dim totalOut As Decimal = r.ProducedQty + r.RejectedFG
                                   Dim q As Decimal = If(totalOut > 0@, r.ProducedQty / totalOut, 0@)
                                   r.OEE = av * perf * q
                                   Dim theo As Decimal = g.Sum(Function(x) If(x.LineIdealCycleTime.GetValueOrDefault() > 0@,
                                                                              x.OperatingSeconds / x.LineIdealCycleTime.Value,
                                                                              0@))
                                   r.AchievementPct = If(theo > 0@, r.ProducedQty / theo, 0@)
                                   Return r
                               End Function) _
                       .OrderByDescending(Function(x) x.ProducedQty) _
                       .ToList()
        End Function

        ' ═══════════════════════════════════════════════════════
        '  BuildSupervisorSummary
        ' ═══════════════════════════════════════════════════════
        Private Shared Function BuildSupervisorSummary(rows As List(Of ProductionDailyRowDto)) As List(Of ProductionSupervisorSummaryDto)
            Return rows.GroupBy(Function(x) x.SupervisorID) _
                       .Select(Function(g)
                                   Dim first = g.First()
                                   Dim r As New ProductionSupervisorSummaryDto With {
                                       .SupervisorID = g.Key,
                                       .SupervisorNameAr = first.SupervisorNameAr,
                                       .ProducedQty = g.Sum(Function(x) x.ProducedQty),
                                       .RejectedFG = g.Sum(Function(x) x.RejectedFGQty),
                                       .TotalScrap = g.Sum(Function(x) x.TotalScrapQty),
                                       .DowntimeMinutes = g.Sum(Function(x) x.DowntimeMinutes),
                                       .RecordsCount = g.Count()
                                   }
                                   Dim planned As Decimal = g.Sum(Function(x) x.PlannedSeconds)
                                   Dim op As Decimal = g.Sum(Function(x) x.OperatingSeconds)
                                   Dim av As Decimal = If(planned > 0@, op / planned, 0@)
                                   Dim perfNum As Decimal = g.Sum(Function(x) x.TotalOutputQty * x.LineIdealCycleTime.GetValueOrDefault())
                                   Dim perf As Decimal = If(op > 0@, Math.Min(1@, perfNum / op), 0@)
                                   Dim totalOut As Decimal = r.ProducedQty + r.RejectedFG
                                   Dim q As Decimal = If(totalOut > 0@, r.ProducedQty / totalOut, 0@)
                                   r.OEE = av * perf * q
                                   Dim theo As Decimal = g.Sum(Function(x) If(x.LineIdealCycleTime.GetValueOrDefault() > 0@,
                                                                              x.OperatingSeconds / x.LineIdealCycleTime.Value,
                                                                              0@))
                                   r.AchievementPct = If(theo > 0@, r.ProducedQty / theo, 0@)
                                   Return r
                               End Function) _
                       .OrderByDescending(Function(x) x.OEE) _
                       .ToList()
        End Function

        ' ═══════════════════════════════════════════════════════
        '  BuildItemSummary
        ' ═══════════════════════════════════════════════════════
        Private Shared Function BuildItemSummary(rows As List(Of ProductionDailyRowDto)) As List(Of ProductionItemSummaryDto)
            Return rows.GroupBy(Function(x) x.ItemID) _
                       .Select(Function(g)
                                   Dim first = g.First()
                                   Dim r As New ProductionItemSummaryDto With {
                                       .ItemID = g.Key,
                                       .ItemCode = first.ItemCode,
                                       .ItemNameAr = first.ItemNameAr,
                                       .ItemUnitName = first.ItemUnitName,
                                       .ProducedQty = g.Sum(Function(x) x.ProducedQty),
                                       .RejectedFG = g.Sum(Function(x) x.RejectedFGQty),
                                       .TotalScrap = g.Sum(Function(x) x.TotalScrapQty),
                                       .RecordsCount = g.Count()
                                   }
                                   r.ScrapPct = If(r.ProducedQty > 0@, r.TotalScrap / r.ProducedQty, 0@)
                                   Dim op As Decimal = g.Sum(Function(x) x.OperatingSeconds)
                                   Dim perfNum As Decimal = g.Sum(Function(x) x.TotalOutputQty * x.LineIdealCycleTime.GetValueOrDefault())
                                   Dim perf As Decimal = If(op > 0@, Math.Min(1@, perfNum / op), 0@)
                                   Dim totalOut As Decimal = r.ProducedQty + r.RejectedFG
                                   Dim q As Decimal = If(totalOut > 0@, r.ProducedQty / totalOut, 0@)
                                   Dim planned As Decimal = g.Sum(Function(x) x.PlannedSeconds)
                                   Dim av As Decimal = If(planned > 0@, op / planned, 0@)
                                   r.OEE = av * perf * q
                                   Dim theo As Decimal = g.Sum(Function(x) If(x.LineIdealCycleTime.GetValueOrDefault() > 0@,
                                                                              x.OperatingSeconds / x.LineIdealCycleTime.Value,
                                                                              0@))
                                   r.AchievementPct = If(theo > 0@, r.ProducedQty / theo, 0@)
                                   Return r
                               End Function) _
                       .OrderByDescending(Function(x) x.ProducedQty) _
                       .ToList()
        End Function

    End Class

    Public Class ProductionReportResult
        Public Property Rows As List(Of ProductionDailyRowDto)
        Public Property Summary As ProductionSummaryDto
        Public Property Daily As List(Of ProductionDailySummaryDto)
        Public Property Shifts As List(Of ProductionShiftSummaryDto)
        Public Property Lines As List(Of ProductionLineSummaryDto)
        Public Property Supervisors As List(Of ProductionSupervisorSummaryDto)
        Public Property Items As List(Of ProductionItemSummaryDto)
    End Class

End Namespace