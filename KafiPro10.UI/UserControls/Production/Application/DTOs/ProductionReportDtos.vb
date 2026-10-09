Imports KafiPro10.Solution.KafiPro.Production

Namespace KafiPro.Production.Application.DTOs

    ' ═══════════════════════════════════════════════════════════
    '  Filter
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionReportFilterDto
        Public Property FromDate As Date = Date.Today
        Public Property ToDate As Date = Date.Today
        Public Property ShiftTypeID As Integer?
        Public Property ProductionLineID As Integer?
        Public Property ItemID As Integer?
        Public Property SupervisorID As Integer?
    End Class


    ' ═══════════════════════════════════════════════════════════
    '  Row DTO — سطر واحد من DailyProductionLog مع كل الحقول
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionDailyRowDto
        Public Property LogID As Long
        Public Property ProductionDate As Date
        Public Property ShiftTypeID As Integer
        Public Property ShiftCode As String
        Public Property ShiftNameAr As String
        Public Property ShiftStartTime As TimeSpan?
        Public Property ShiftEndTime As TimeSpan?
        Public Property IsOvertime As Boolean

        Public Property ProductionLineID As Integer
        Public Property LineCode As String
        Public Property LineNameAr As String
        Public Property LineIdealCycleTime As Decimal?

        Public Property ItemID As Integer
        Public Property ItemCode As String
        Public Property ItemNameAr As String
        Public Property ItemUnitName As String

        Public Property SupervisorID As Integer
        Public Property SupervisorNameAr As String

        Public Property ProducedQty As Decimal
        Public Property RejectedFGQty As Decimal
        Public Property RawMaterialScrapQty As Decimal
        Public Property PackagingScrapQty As Decimal
        Public Property DowntimeMinutes As Decimal
        Public Property Notes As String
        Public Property CreatedAt As DateTime?

        ' ─── Computed by Service ───
        Public Property PlannedSeconds As Decimal
        Public Property OperatingSeconds As Decimal
        Public Property Availability As Decimal     ' 0..1
        Public Property Performance As Decimal      ' 0..1
        Public Property Quality As Decimal          ' 0..1
        Public Property OEE As Decimal              ' 0..1
        Public Property AchievementPct As Decimal   ' 0..1+
        Public Property ScrapPct As Decimal         ' 0..1+

        Public ReadOnly Property TotalOutputQty As Decimal
            Get
                Return ProducedQty + RejectedFGQty
            End Get
        End Property

        Public ReadOnly Property TotalScrapQty As Decimal
            Get
                Return RejectedFGQty + RawMaterialScrapQty + PackagingScrapQty
            End Get
        End Property

        Public ReadOnly Property DowntimeSeconds As Decimal
            Get
                Return DowntimeMinutes * 60D
            End Get
        End Property
    End Class


    ' ═══════════════════════════════════════════════════════════
    '  Summary — إجماليات الفترة
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionSummaryDto
        Public Property FromDate As Date
        Public Property ToDate As Date
        Public Property DaysCount As Integer
        Public Property RecordsCount As Integer

        Public Property TotalProduced As Decimal
        Public Property TotalRejectedFG As Decimal
        Public Property TotalRawScrap As Decimal
        Public Property TotalPackScrap As Decimal
        Public Property TotalScrap As Decimal
        Public Property TotalOutput As Decimal

        Public Property TotalPlannedSeconds As Decimal
        Public Property TotalOperatingSeconds As Decimal
        Public Property TotalDowntimeSeconds As Decimal
        Public Property TotalDowntimeMinutes As Decimal

        Public Property Availability As Decimal
        Public Property Performance As Decimal
        Public Property Quality As Decimal
        Public Property OEE As Decimal
        Public Property AchievementPct As Decimal
        Public Property ScrapPct As Decimal
    End Class


    ' ═══════════════════════════════════════════════════════════
    '  Daily summary — لكل يوم
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionDailySummaryDto
        Public Property ProductionDate As Date
        Public Property ProducedQty As Decimal
        Public Property RejectedFG As Decimal
        Public Property TotalScrap As Decimal
        Public Property DowntimeMinutes As Decimal
        Public Property OperatingSeconds As Decimal
        Public Property OEE As Decimal
        Public Property AchievementPct As Decimal
        Public Property RecordsCount As Integer
    End Class


    ' ═══════════════════════════════════════════════════════════
    '  Shift summary — لكل وردية
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionShiftSummaryDto
        Public Property ShiftTypeID As Integer
        Public Property ShiftCode As String
        Public Property ShiftNameAr As String
        Public Property ProducedQty As Decimal
        Public Property RejectedFG As Decimal
        Public Property TotalScrap As Decimal
        Public Property DowntimeMinutes As Decimal
        Public Property OEE As Decimal
        Public Property AchievementPct As Decimal
        Public Property RecordsCount As Integer
    End Class


    ' ═══════════════════════════════════════════════════════════
    '  Line summary — لكل خط
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionLineSummaryDto
        Public Property ProductionLineID As Integer
        Public Property LineCode As String
        Public Property LineNameAr As String
        Public Property ProducedQty As Decimal
        Public Property RejectedFG As Decimal
        Public Property TotalScrap As Decimal
        Public Property DowntimeMinutes As Decimal
        Public Property OEE As Decimal
        Public Property AchievementPct As Decimal
        Public Property RecordsCount As Integer
    End Class


    ' ═══════════════════════════════════════════════════════════
    '  Supervisor summary — لكل مشرف
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionSupervisorSummaryDto
        Public Property SupervisorID As Integer
        Public Property SupervisorNameAr As String
        Public Property ProducedQty As Decimal
        Public Property RejectedFG As Decimal
        Public Property TotalScrap As Decimal
        Public Property DowntimeMinutes As Decimal
        Public Property OEE As Decimal
        Public Property AchievementPct As Decimal
        Public Property RecordsCount As Integer
    End Class


    ' ═══════════════════════════════════════════════════════════
    '  Item summary — لكل صنف
    ' ═══════════════════════════════════════════════════════════
    Public Class ProductionItemSummaryDto
        Public Property ItemID As Integer
        Public Property ItemCode As String
        Public Property ItemNameAr As String
        Public Property ItemUnitName As String
        Public Property ProducedQty As Decimal
        Public Property RejectedFG As Decimal
        Public Property TotalScrap As Decimal
        Public Property ScrapPct As Decimal
        Public Property OEE As Decimal
        Public Property AchievementPct As Decimal
        Public Property RecordsCount As Integer
    End Class

End Namespace