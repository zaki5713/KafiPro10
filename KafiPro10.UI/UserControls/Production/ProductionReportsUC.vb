Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Application.Interfaces
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms.DataVisualization
Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro.Production.Application.Services
Imports System.Windows.Forms.DataVisualization.Charting
Public Class ProductionReportsUC

    Private _loaded As Boolean = False
    Private _service As ProductionReportService
    Private _filter As New ProductionReportFilterDto()
    Private _result As ProductionReportResult

    Private _cboQuick As ComboBox
    Private _dtFrom As DateTimePicker
    Private _dtTo As DateTimePicker
    Private _cboShift As ComboBox
    Private _cboLine As ComboBox
    Private _cboItem As ComboBox
    Private _cboSupervisor As ComboBox

    Private _lblKpiProduced As Label
    Private _lblKpiRejected As Label
    Private _lblKpiScrap As Label
    Private _lblKpiDowntime As Label
    Private _lblKpiOEE As Label
    Private _lblKpiAchieve As Label

    Private _tabs As TabControl
    Private _gridSummaryDaily As DataGridView
    Private _gridDaily As DataGridView
    Private _gridShifts As DataGridView
    Private _gridOEE As DataGridView
    Private _gridDowntime As DataGridView
    Private _gridScrap As DataGridView
    Private _gridSupervisors As DataGridView
    Private _gridDetail As DataGridView

    Private _chartDaily As Chart
    Private _chartShifts As Chart
    Private _chartOEE As Chart
    Private _chartDowntime As Chart
    Private _chartScrap As Chart
    Private _chartSupervisors As Chart

    Private _lblStatus As Label

    ' ═══════════════════════════════════════════════════════════
    '  Load
    ' ═══════════════════════════════════════════════════════════
    Private Sub ProductionReportsUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _loaded Then Return
        _loaded = True

        _service = New ProductionReportService()
        Me.Dock = DockStyle.Fill
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        BuildUI()
        LoadLookups()
        ApplyQuickRange(0)
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  BuildUI
    ' ═══════════════════════════════════════════════════════════
    Private Sub BuildUI()

        ' ─── Toolbar ───
        Dim pnlTop As New Panel With {.Dock = DockStyle.Top, .Height = 40, .BackColor = UITheme.SidebarHover}
        Dim flowTop As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False,
            .Padding = New Padding(6, 4, 6, 4)
        }
        pnlTop.Controls.Add(flowTop)

        flowTop.Controls.Add(New Label With {
            .Text = "تقارير الإنتاج",
            .Font = New Font("Segoe UI", 11.0!, FontStyle.Bold),
            .ForeColor = UITheme.TextWhite,
            .AutoSize = False, .Width = 100, .Height = 34,
            .TextAlign = ContentAlignment.MiddleRight,
            .Margin = New Padding(4, 0, 12, 0)
        })

        Dim bPrint = MakeButton("🖨 طباعة", 90)
        Dim bExport = MakeButton("⬇ تصدير Excel", 120)
        Dim bRefresh = MakeButton("🔄 تحديث", 90)
        Dim bReset = MakeButton("✖ تصفير الفلاتر", 130)

        AddHandler bPrint.Click, Sub() DoPrint()
        AddHandler bExport.Click, Sub() OnExport()
        AddHandler bRefresh.Click, Sub() LoadReport()
        AddHandler bReset.Click, Sub() OnResetFilters()

        flowTop.Controls.Add(bReset)
        flowTop.Controls.Add(bRefresh)
        flowTop.Controls.Add(bExport)
        flowTop.Controls.Add(bPrint)

        ' ─── Filters ───
        Dim pnlFilter As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 52,
            .BackColor = Color.FromArgb(248, 248, 248)
        }
        Dim flowF As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False,
            .Padding = New Padding(6, 8, 6, 8)
        }
        pnlFilter.Controls.Add(flowF)

        flowF.Controls.Add(MakeLabel("الفترة:"))
        _cboQuick = MakeCombo(70)
        _cboQuick.Items.AddRange(New Object() {"اليوم", "أمس", "آخر 7 أيام", "آخر 30 يوم", "الشهر الحالي", "مخصص"})
        _cboQuick.SelectedIndex = 0
        AddHandler _cboQuick.SelectedIndexChanged, Sub() ApplyQuickRange(_cboQuick.SelectedIndex)
        flowF.Controls.Add(_cboQuick)

        flowF.Controls.Add(MakeLabel("من:"))
        _dtFrom = MakeDate(120)
        AddHandler _dtFrom.ValueChanged, Sub() If _cboQuick.SelectedIndex = 5 Then LoadReport()
        flowF.Controls.Add(_dtFrom)

        flowF.Controls.Add(MakeLabel("إلى:"))
        _dtTo = MakeDate(120)
        AddHandler _dtTo.ValueChanged, Sub() If _cboQuick.SelectedIndex = 5 Then LoadReport()
        flowF.Controls.Add(_dtTo)

        flowF.Controls.Add(MakeLabel("وردية:"))
        _cboShift = MakeCombo(100)
        AddHandler _cboShift.SelectedIndexChanged, Sub() If _cboShift.SelectedIndex >= 0 Then LoadReport()
        flowF.Controls.Add(_cboShift)

        flowF.Controls.Add(MakeLabel("خط:"))
        _cboLine = MakeCombo(120)
        AddHandler _cboLine.SelectedIndexChanged, Sub() If _cboLine.SelectedIndex >= 0 Then LoadReport()
        flowF.Controls.Add(_cboLine)

        flowF.Controls.Add(MakeLabel("صنف:"))
        _cboItem = MakeCombo(120)
        AddHandler _cboItem.SelectedIndexChanged, Sub() If _cboItem.SelectedIndex >= 0 Then LoadReport()
        flowF.Controls.Add(_cboItem)

        flowF.Controls.Add(MakeLabel("مشرف:"))
        _cboSupervisor = MakeCombo(100)
        AddHandler _cboSupervisor.SelectedIndexChanged, Sub() If _cboSupervisor.SelectedIndex >= 0 Then LoadReport()
        flowF.Controls.Add(_cboSupervisor)

        ' ─── KPI Cards ───
        Dim pnlKpi As New Panel With {.Dock = DockStyle.Top, .Height = 92, .BackColor = Color.White, .Padding = New Padding(8, 8, 8, 8)}
        Dim flowK As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False
        }
        pnlKpi.Controls.Add(flowK)

        _lblKpiProduced = New Label()
        _lblKpiRejected = New Label()
        _lblKpiScrap = New Label()
        _lblKpiDowntime = New Label()
        _lblKpiOEE = New Label()
        _lblKpiAchieve = New Label()

        flowK.Controls.Add(MakeKpiCard("إجمالي الإنتاج", _lblKpiProduced, Color.FromArgb(33, 150, 243)))
        flowK.Controls.Add(MakeKpiCard("مرفوض FG", _lblKpiRejected, Color.FromArgb(244, 67, 54)))
        flowK.Controls.Add(MakeKpiCard("إجمالي الهوالك", _lblKpiScrap, Color.FromArgb(255, 152, 0)))
        flowK.Controls.Add(MakeKpiCard("دقائق التوقف", _lblKpiDowntime, Color.FromArgb(156, 39, 176)))
        flowK.Controls.Add(MakeKpiCard("OEE", _lblKpiOEE, Color.FromArgb(76, 175, 80)))
        flowK.Controls.Add(MakeKpiCard("نسبة الإنجاز", _lblKpiAchieve, Color.FromArgb(0, 150, 136)))

        ' ─── Tabs ───
        _tabs = New TabControl With {
            .Dock = DockStyle.Fill,
            .Font = New Font("Segoe UI", 9.0!),
            .RightToLeft = RightToLeft.Yes,
            .RightToLeftLayout = True
        }

        Dim tpSummary As New TabPage("📊 ملخص الفترة") With {.BackColor = Color.Wheat}
        _gridSummaryDaily = MakeGrid()
        AddDailySummaryColumns(_gridSummaryDaily)
        tpSummary.Controls.Add(_gridSummaryDaily)
        _tabs.TabPages.Add(tpSummary)

        Dim tpDaily As New TabPage("📈 اتجاه يومي") With {.BackColor = Color.White}
        _chartDaily = MakeChart()
        _gridDaily = MakeGrid()
        AddDailySummaryColumns(_gridDaily)
        tpDaily.Controls.Add(MakeSplit(_chartDaily, _gridDaily, 280))
        _tabs.TabPages.Add(tpDaily)

        Dim tpShifts As New TabPage("⚙️ مقارنة الورديات") With {.BackColor = Color.Wheat}
        _chartShifts = MakeChart()
        _gridShifts = MakeGrid()
        AddShiftColumns(_gridShifts)
        tpShifts.Controls.Add(MakeSplit(_chartShifts, _gridShifts, 280))
        _tabs.TabPages.Add(tpShifts)

        Dim tpOEE As New TabPage("🎯 تحليل OEE") With {.BackColor = Color.White}
        _chartOEE = MakeChart()
        _gridOEE = MakeGrid()
        AddOeeColumns(_gridOEE)
        tpOEE.Controls.Add(MakeSplit(_chartOEE, _gridOEE, 300))
        _tabs.TabPages.Add(tpOEE)

        Dim tpDown As New TabPage("⚠️ التوقفات") With {.BackColor = Color.Wheat}
        _chartDowntime = MakeChart()
        _gridDowntime = MakeGrid()
        AddDowntimeColumns(_gridDowntime)
        tpDown.Controls.Add(MakeSplit(_chartDowntime, _gridDowntime, 280))
        _tabs.TabPages.Add(tpDown)

        Dim tpScrap As New TabPage("🗑️ الهوالك") With {.BackColor = Color.White}
        _chartScrap = MakeChart()
        _gridScrap = MakeGrid()
        AddScrapColumns(_gridScrap)
        tpScrap.Controls.Add(MakeSplit(_chartScrap, _gridScrap, 300))
        _tabs.TabPages.Add(tpScrap)

        Dim tpSup As New TabPage("👤 أداء المشرفين") With {.BackColor = Color.Wheat}
        _chartSupervisors = MakeChart()
        _gridSupervisors = MakeGrid()
        AddSupervisorColumns(_gridSupervisors)
        tpSup.Controls.Add(MakeSplit(_chartSupervisors, _gridSupervisors, 280))
        _tabs.TabPages.Add(tpSup)

        Dim tpDetail As New TabPage("📋 التفصيلي") With {.BackColor = Color.White}
        _gridDetail = MakeGrid()
        AddDetailColumns(_gridDetail)
        tpDetail.Controls.Add(_gridDetail)
        _tabs.TabPages.Add(tpDetail)

        ' ─── Bottom ───
        Dim pnlBottom As New Panel With {.Dock = DockStyle.Bottom, .Height = 30, .BackColor = Color.FromArgb(248, 248, 248)}
        _lblStatus = New Label With {
            .AutoSize = False,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleRight,
            .ForeColor = UITheme.TextPrimary,
            .Padding = New Padding(0, 0, 10, 0),
            .Text = "جاهز"
        }
        pnlBottom.Controls.Add(_lblStatus)

        Me.Controls.Add(_tabs)
        Me.Controls.Add(pnlKpi)
        Me.Controls.Add(pnlFilter)
        Me.Controls.Add(pnlTop)
        Me.Controls.Add(pnlBottom)
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Helpers
    ' ═══════════════════════════════════════════════════════════
    Private Function MakeButton(text As String, width As Integer) As Button
        Dim b As New Button With {
            .Text = text,
            .BackColor = UITheme.ButtonNormal,
            .ForeColor = UITheme.TextPrimary,
            .Font = UITheme.FontBold,
            .FlatStyle = FlatStyle.Popup,
            .Width = 120,
            .Height = 34,
            .Margin = New Padding(2, 0, 2, 0),
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = True
        }
        b.FlatAppearance.BorderSize = 0
        b.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Return b
    End Function

    Private Function MakeLabel(text As String) As Label
        Return New Label With {
            .Text = text,
            .AutoSize = True,
            .Margin = New Padding(8, 8, 2, 0),
            .ForeColor = UITheme.TextPrimary
        }
    End Function

    Private Function MakeCombo(width As Integer) As ComboBox
        Return New ComboBox With {
            .Width = width,
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .Margin = New Padding(4, 3, 4, 3),
            .Font = New Font("Segoe UI", 9.0!)
        }
    End Function

    Private Function MakeDate(width As Integer) As DateTimePicker
        Return New DateTimePicker With {
            .Width = width,
            .Format = DateTimePickerFormat.Custom,
            .CustomFormat = "yyyy-MM-dd",
            .Margin = New Padding(4, 3, 4, 3),
            .Font = New Font("Segoe UI", 9.0!)
        }
    End Function

    Private Function MakeKpiCard(title As String, valueLabel As Label, color As Color) As Panel
        Dim pnl As New Panel With {
            .Width = 170,
            .Height = 70,
            .BackColor = Color.Wheat,
            .Margin = New Padding(4),
            .Padding = New Padding(6)
        }

        Dim lblTitle As New Label With {
            .Text = title,
            .Dock = DockStyle.Top,
            .Height = 22,
            .Font = New Font("Segoe UI", 8.5!, FontStyle.Regular),
            .ForeColor = Color.Black,
            .TextAlign = ContentAlignment.MiddleCenter
        }

        valueLabel.Dock = DockStyle.Fill
        valueLabel.Font = New Font("Segoe UI", 15.0!, FontStyle.Bold)
        valueLabel.ForeColor = color
        valueLabel.TextAlign = ContentAlignment.MiddleCenter
        valueLabel.Text = "—"

        pnl.Controls.Add(valueLabel)
        pnl.Controls.Add(lblTitle)

        AddHandler pnl.Paint, Sub(s, e2)
                                  Using br As New SolidBrush(color)
                                      e2.Graphics.FillRectangle(br, 0, 0, pnl.Width, 3)
                                  End Using
                                  Using pen As New Pen(Color.FromArgb(225, 225, 225))
                                      e2.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1)
                                  End Using
                              End Sub

        Return pnl
    End Function

    Private Function MakeSplit(topControl As Control, bottomControl As Control, topHeight As Integer) As SplitContainer
        Dim sc As New SplitContainer With {
            .Dock = DockStyle.Fill,
            .Orientation = Orientation.Horizontal,
            .SplitterWidth = 6,
            .BackColor = Color.FromArgb(230, 230, 230)
        }
        topControl.Dock = DockStyle.Fill
        bottomControl.Dock = DockStyle.Fill
        sc.Panel1.Controls.Add(topControl)
        sc.Panel2.Controls.Add(bottomControl)
        ' SplitterDistance بعد الإضافة لتجنب Exception
        AddHandler sc.HandleCreated, Sub()
                                         Try
                                             sc.SplitterDistance = topHeight
                                         Catch
                                         End Try
                                     End Sub
        Return sc
    End Function

    Private Function MakeGrid() As DataGridView
        Dim g As New DataGridView With {
            .Dock = DockStyle.Fill,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = True,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False,
            .RowHeadersVisible = False,
            .RightToLeft = RightToLeft.Yes,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
            .ScrollBars = ScrollBars.Both,
            .EnableHeadersVisualStyles = False,
            .ColumnHeadersHeight = 36,
            .RowTemplate = New DataGridViewRow With {.Height = 28},
            .AllowUserToResizeRows = False,
            .AllowUserToResizeColumns = True,
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
            .GridColor = Color.FromArgb(230, 230, 230)
        }
        g.ColumnHeadersDefaultCellStyle.BackColor = UITheme.ButtonNormal
        g.ColumnHeadersDefaultCellStyle.ForeColor = UITheme.TextPrimary
        g.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        g.DefaultCellStyle.Font = New Font("Segoe UI", 9.0!)
        g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 250)
        g.DefaultCellStyle.SelectionForeColor = Color.Black
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252)
        Return g
    End Function

    Private Function MakeChart() As Chart
        Dim c As New Chart With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.White,
            .Palette = ChartColorPalette.SeaGreen
        }
        Dim ca As New ChartArea("main") With {.BackColor = Color.White}
        ca.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
        ca.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
        ca.AxisX.LabelStyle.Font = New Font("Segoe UI", 8.5!)
        ca.AxisY.LabelStyle.Font = New Font("Segoe UI", 8.5!)
        ca.AxisX.Interval = 1
        c.ChartAreas.Add(ca)

        Dim lg As New Legend With {
            .Docking = Docking.Bottom,
            .Font = New Font("Segoe UI", 8.5!),
            .BackColor = Color.Transparent
        }
        c.Legends.Add(lg)
        Return c
    End Function

    Private Shared Function NumStyle() As DataGridViewCellStyle
        Return New DataGridViewCellStyle With {.Format = "N2", .Alignment = DataGridViewContentAlignment.MiddleCenter}
    End Function

    Private Shared Function PctStyle() As DataGridViewCellStyle
        Return New DataGridViewCellStyle With {.Format = "P2", .Alignment = DataGridViewContentAlignment.MiddleCenter}
    End Function

    ''' <summary>عمود مع دعم Style</summary>
    Private Shared Function Col(name As String, header As String, width As Integer,
                                Optional align As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleCenter,
                                Optional style As DataGridViewCellStyle = Nothing) As DataGridViewTextBoxColumn
        Dim c As New DataGridViewTextBoxColumn With {
            .Name = name, .HeaderText = header, .Width = width
        }
        c.DefaultCellStyle.Alignment = align
        If style IsNot Nothing Then c.DefaultCellStyle = style
        Return c
    End Function

    ''' <summary>إنشاء Series مع تعيين ChartType</summary>
    Private Shared Function NewSeries(name As String, type As SeriesChartType, color As Color) As Series
        Dim s As New Series(name) With {
            .ChartType = type,
            .color = color
        }
        Return s
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Columns
    ' ═══════════════════════════════════════════════════════════
    Private Sub AddDailySummaryColumns(g As DataGridView)
        g.Columns.Clear()
        g.Columns.Add(Col("Date", "التاريخ", 110))
        g.Columns.Add(Col("Produced", "الإنتاج", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Rejected", "مرفوض FG", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Scrap", "هوالك", 90, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Downtime", "توقف (د)", 90, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("OEE", "OEE", 90, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Ach", "إنجاز", 90, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Recs", "سجلات", 70))
    End Sub

    Private Sub AddShiftColumns(g As DataGridView)
        g.Columns.Clear()
        g.Columns.Add(Col("Shift", "الوردية", 140, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Produced", "الإنتاج", 110, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Rejected", "مرفوض", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Scrap", "هوالك", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Downtime", "توقف (د)", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("OEE", "OEE", 90, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Ach", "إنجاز", 90, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Recs", "سجلات", 70))
    End Sub

    Private Sub AddOeeColumns(g As DataGridView)
        g.Columns.Clear()
        g.Columns.Add(Col("Date", "التاريخ", 110))
        g.Columns.Add(Col("Line", "الخط", 140, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Item", "الصنف", 200, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Produced", "الإنتاج", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Planned", "مخطط (ث)", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Op", "تشغيل (ث)", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("A", "A%", 70, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("P", "P%", 70, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Q", "Q%", 70, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("OEE", "OEE", 80, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
    End Sub

    Private Sub AddDowntimeColumns(g As DataGridView)
        g.Columns.Clear()
        g.Columns.Add(Col("Date", "التاريخ", 110))
        g.Columns.Add(Col("Shift", "الوردية", 120, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Line", "الخط", 140, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Downtime", "دقائق التوقف", 120, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Planned", "دقائق مخطط", 120, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Pct", "نسبة التوقف", 100, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
    End Sub

    Private Sub AddScrapColumns(g As DataGridView)
        g.Columns.Clear()
        g.Columns.Add(Col("ItemCode", "كود الصنف", 110))
        g.Columns.Add(Col("ItemName", "اسم الصنف", 220, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Produced", "الإنتاج", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Rejected", "مرفوض FG", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Raw", "خام", 90, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Pack", "تعبئة", 90, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Total", "إجمالي", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Pct", "نسبة %", 90, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
    End Sub

    Private Sub AddSupervisorColumns(g As DataGridView)
        g.Columns.Clear()
        g.Columns.Add(Col("Name", "المشرف", 200, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Produced", "الإنتاج", 110, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Rejected", "مرفوض", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Downtime", "توقف (د)", 100, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("OEE", "OEE", 90, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Ach", "إنجاز", 90, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Recs", "سجلات", 70))
    End Sub

    Private Sub AddDetailColumns(g As DataGridView)
        g.Columns.Clear()
        g.Columns.Add(Col("Date", "التاريخ", 100))
        g.Columns.Add(Col("Shift", "الوردية", 110, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Line", "الخط", 130, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("ItemCode", "كود", 100))
        g.Columns.Add(Col("ItemName", "الصنف", 180, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Supervisor", "المشرف", 140, DataGridViewContentAlignment.MiddleRight))
        g.Columns.Add(Col("Produced", "إنتاج", 90, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Rejected", "مرفوض", 90, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Raw", "خام", 80, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Pack", "تعبئة", 80, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("Downtime", "توقف (د)", 90, DataGridViewContentAlignment.MiddleCenter, NumStyle()))
        g.Columns.Add(Col("A", "A%", 70, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("P", "P%", 70, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Q", "Q%", 70, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("OEE", "OEE", 80, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Ach", "إنجاز", 80, DataGridViewContentAlignment.MiddleCenter, PctStyle()))
        g.Columns.Add(Col("Notes", "ملاحظات", 200, DataGridViewContentAlignment.MiddleRight))
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Lookups
    ' ═══════════════════════════════════════════════════════════
    Private Sub LoadLookups()
        _cboShift.Items.Clear()
        _cboShift.Items.Add(New ComboItem("الكل", Nothing))
        _cboLine.Items.Clear()
        _cboLine.Items.Add(New ComboItem("الكل", Nothing))
        _cboItem.Items.Clear()
        _cboItem.Items.Add(New ComboItem("الكل", Nothing))
        _cboSupervisor.Items.Clear()
        _cboSupervisor.Items.Add(New ComboItem("الكل", Nothing))

        Try
            Dim f As New ProductionReportFilterDto With {
                .FromDate = Date.Today.AddDays(-90),
                .ToDate = Date.Today
            }
            Dim rows = _service.GetReport(f).Rows

            For Each s In rows.GroupBy(Function(x) x.ShiftTypeID).
                              Select(Function(g) New With {.ID = g.Key, .Name = g.First().ShiftNameAr}).
                              OrderBy(Function(x) x.ID)
                _cboShift.Items.Add(New ComboItem(s.Name, s.ID))
            Next

            For Each l In rows.GroupBy(Function(x) x.ProductionLineID).
                              Select(Function(g) New With {.ID = g.Key, .Name = g.First().LineNameAr}).
                              OrderBy(Function(x) x.Name)
                _cboLine.Items.Add(New ComboItem(l.Name, l.ID))
            Next

            For Each i In rows.GroupBy(Function(x) x.ItemID).
                              Select(Function(g) New With {.ID = g.Key, .Name = g.First().ItemCode & " — " & g.First().ItemNameAr}).
                              OrderBy(Function(x) x.Name)
                _cboItem.Items.Add(New ComboItem(i.Name, i.ID))
            Next

            For Each w In rows.GroupBy(Function(x) x.SupervisorID).
                              Select(Function(g) New With {.ID = g.Key, .Name = g.First().SupervisorNameAr}).
                              OrderBy(Function(x) x.Name)
                _cboSupervisor.Items.Add(New ComboItem(w.Name, w.ID))
            Next
        Catch
            ' تجاهل
        End Try

        If _cboShift.Items.Count > 0 Then _cboShift.SelectedIndex = 0
        If _cboLine.Items.Count > 0 Then _cboLine.SelectedIndex = 0
        If _cboItem.Items.Count > 0 Then _cboItem.SelectedIndex = 0
        If _cboSupervisor.Items.Count > 0 Then _cboSupervisor.SelectedIndex = 0
    End Sub

    Private Class ComboItem
        Public Property Text As String
        Public Property Value As Integer?
        Public Sub New(t As String, v As Integer?)
            Text = t : Value = v
        End Sub
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    ' ═══════════════════════════════════════════════════════════
    '  Quick ranges
    ' ═══════════════════════════════════════════════════════════
    Private Sub ApplyQuickRange(idx As Integer)
        Dim today = Date.Today
        Select Case idx
            Case 0
                _dtFrom.Value = today : _dtTo.Value = today
            Case 1
                _dtFrom.Value = today.AddDays(-1) : _dtTo.Value = today.AddDays(-1)
            Case 2
                _dtFrom.Value = today.AddDays(-6) : _dtTo.Value = today
            Case 3
                _dtFrom.Value = today.AddDays(-29) : _dtTo.Value = today
            Case 4
                _dtFrom.Value = New Date(today.Year, today.Month, 1) : _dtTo.Value = today
            Case 5
                ' مخصص
        End Select

        Dim custom As Boolean = (idx = 5)
        _dtFrom.Enabled = custom
        _dtTo.Enabled = custom

        If Not custom Then LoadReport()
    End Sub

    Private Sub OnResetFilters()
        If _cboQuick.Items.Count > 0 Then _cboQuick.SelectedIndex = 0
        If _cboShift.Items.Count > 0 Then _cboShift.SelectedIndex = 0
        If _cboLine.Items.Count > 0 Then _cboLine.SelectedIndex = 0
        If _cboItem.Items.Count > 0 Then _cboItem.SelectedIndex = 0
        If _cboSupervisor.Items.Count > 0 Then _cboSupervisor.SelectedIndex = 0
        LoadReport()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  LoadReport
    ' ═══════════════════════════════════════════════════════════
    Private Sub LoadReport()
        Try
            Cursor = Cursors.WaitCursor
            _lblStatus.Text = "جاري التحميل..."

            _filter.FromDate = _dtFrom.Value.Date
            _filter.ToDate = _dtTo.Value.Date
            _filter.ShiftTypeID = GetComboValue(_cboShift)
            _filter.ProductionLineID = GetComboValue(_cboLine)
            _filter.ItemID = GetComboValue(_cboItem)
            _filter.SupervisorID = GetComboValue(_cboSupervisor)

            _result = _service.GetReport(_filter)

            UpdateKpiCards()
            BindSummaryDaily()
            BindDaily()
            BindShifts()
            BindOEE()
            BindDowntime()
            BindScrap()
            BindSupervisors()
            BindDetail()

            _lblStatus.Text = $"تم التحميل — {_result.Rows.Count} سجل • {DateTime.Now:HH:mm:ss}"
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل التقرير: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            _lblStatus.Text = "خطأ في التحميل"
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function GetComboValue(cbo As ComboBox) As Integer?
        If cbo.SelectedItem Is Nothing Then Return Nothing
        Dim item = TryCast(cbo.SelectedItem, ComboItem)
        If item Is Nothing Then Return Nothing
        Return item.Value
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  KPI
    ' ═══════════════════════════════════════════════════════════
    Private Sub UpdateKpiCards()
        Dim s = _result.Summary
        _lblKpiProduced.Text = s.TotalProduced.ToString("N0")
        _lblKpiRejected.Text = s.TotalRejectedFG.ToString("N0")
        _lblKpiScrap.Text = s.TotalScrap.ToString("N0")
        _lblKpiDowntime.Text = s.TotalDowntimeMinutes.ToString("N0")
        _lblKpiOEE.Text = s.OEE.ToString("P1")
        _lblKpiAchieve.Text = s.AchievementPct.ToString("P1")
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Binds
    ' ═══════════════════════════════════════════════════════════
    Private Sub BindSummaryDaily()
        _gridSummaryDaily.Rows.Clear()
        For Each d In _result.Daily
            _gridSummaryDaily.Rows.Add(
                d.ProductionDate.ToString("yyyy-MM-dd"),
                d.ProducedQty, d.RejectedFG, d.TotalScrap, d.DowntimeMinutes,
                d.OEE, d.AchievementPct, d.RecordsCount)
        Next
    End Sub

    Private Sub BindDaily()
        _gridDaily.Rows.Clear()
        For Each d In _result.Daily
            _gridDaily.Rows.Add(
                d.ProductionDate.ToString("yyyy-MM-dd"),
                d.ProducedQty, d.RejectedFG, d.TotalScrap, d.DowntimeMinutes,
                d.OEE, d.AchievementPct, d.RecordsCount)
        Next

        _chartDaily.Series.Clear()
        Dim s1 = NewSeries("الإنتاج", SeriesChartType.Column, Color.FromArgb(33, 150, 243))
        Dim s2 = NewSeries("الهوالك", SeriesChartType.Column, Color.FromArgb(244, 67, 54))
        Dim s3 = NewSeries("OEE %", SeriesChartType.Line, Color.FromArgb(76, 175, 80))
        s3.BorderWidth = 3
        s3.YAxisType = AxisType.Secondary
        s3.MarkerStyle = MarkerStyle.Circle
        s3.MarkerSize = 7

        _chartDaily.Series.Add(s1)
        _chartDaily.Series.Add(s2)
        _chartDaily.Series.Add(s3)

        _chartDaily.ChartAreas(0).AxisY2.Enabled = AxisEnabled.True
        _chartDaily.ChartAreas(0).AxisY2.LabelStyle.Format = "P0"

        For Each d In _result.Daily
            s1.Points.AddXY(d.ProductionDate.ToString("MM-dd"), d.ProducedQty)
            s2.Points.AddXY(d.ProductionDate.ToString("MM-dd"), d.TotalScrap)
            s3.Points.AddXY(d.ProductionDate.ToString("MM-dd"), d.OEE)
        Next
    End Sub

    Private Sub BindShifts()
        _gridShifts.Rows.Clear()
        For Each d In _result.Shifts
            _gridShifts.Rows.Add(d.ShiftNameAr, d.ProducedQty, d.RejectedFG,
                                 d.TotalScrap, d.DowntimeMinutes, d.OEE,
                                 d.AchievementPct, d.RecordsCount)
        Next

        _chartShifts.Series.Clear()
        Dim s1 = NewSeries("الإنتاج", SeriesChartType.Column, Color.FromArgb(33, 150, 243))
        s1.IsValueShownAsLabel = True
        Dim s2 = NewSeries("OEE %", SeriesChartType.Line, Color.FromArgb(76, 175, 80))
        s2.BorderWidth = 3
        s2.YAxisType = AxisType.Secondary
        s2.MarkerStyle = MarkerStyle.Diamond
        s2.MarkerSize = 9

        _chartShifts.Series.Add(s1)
        _chartShifts.Series.Add(s2)

        _chartShifts.ChartAreas(0).AxisY2.Enabled = AxisEnabled.True
        _chartShifts.ChartAreas(0).AxisY2.LabelStyle.Format = "P0"

        For Each d In _result.Shifts
            s1.Points.AddXY(d.ShiftNameAr, d.ProducedQty)
            s2.Points.AddXY(d.ShiftNameAr, d.OEE)
        Next
    End Sub

    Private Sub BindOEE()
        _gridOEE.Rows.Clear()
        For Each r In _result.Rows
            _gridOEE.Rows.Add(
                r.ProductionDate.ToString("yyyy-MM-dd"),
                r.LineNameAr, r.ItemNameAr,
                r.ProducedQty, r.PlannedSeconds, r.OperatingSeconds,
                r.Availability, r.Performance, r.Quality, r.OEE)
        Next

        _chartOEE.Series.Clear()
        _chartOEE.ChartAreas(0).AxisY.Maximum = 1.0R
        _chartOEE.ChartAreas(0).AxisY.Minimum = 0.0R
        _chartOEE.ChartAreas(0).AxisY.LabelStyle.Format = "P0"

        Dim radar = NewSeries("متوسط OEE", SeriesChartType.Radar, Color.FromArgb(76, 175, 80))
        radar.BorderWidth = 3
        _chartOEE.Series.Add(radar)

        Dim avgA As Double = If(_result.Rows.Count > 0, _result.Rows.Average(Function(x) CDbl(x.Availability)), 0.0R)
        Dim avgP As Double = If(_result.Rows.Count > 0, _result.Rows.Average(Function(x) CDbl(x.Performance)), 0.0R)
        Dim avgQ As Double = If(_result.Rows.Count > 0, _result.Rows.Average(Function(x) CDbl(x.Quality)), 0.0R)
        Dim avgO As Double = If(_result.Rows.Count > 0, _result.Rows.Average(Function(x) CDbl(x.OEE)), 0.0R)

        radar.Points.AddXY("Availability", avgA)
        radar.Points.AddXY("Performance", avgP)
        radar.Points.AddXY("Quality", avgQ)
        radar.Points.AddXY("OEE", avgO)
    End Sub

    Private Sub BindDowntime()
        _gridDowntime.Rows.Clear()
        For Each r In _result.Rows.Where(Function(x) x.DowntimeMinutes > 0@).OrderByDescending(Function(x) x.DowntimeMinutes)
            Dim plannedMin As Decimal = If(r.PlannedSeconds > 0@, r.PlannedSeconds / 60@, 0@)
            Dim pct As Decimal = If(plannedMin > 0@, r.DowntimeMinutes / plannedMin, 0@)
            _gridDowntime.Rows.Add(
                r.ProductionDate.ToString("yyyy-MM-dd"),
                r.ShiftNameAr, r.LineNameAr,
                r.DowntimeMinutes, plannedMin, pct)
        Next

        _chartDowntime.Series.Clear()
        Dim s1 = NewSeries("دقائق التوقف", SeriesChartType.Bar, Color.FromArgb(244, 67, 54))
        s1.IsValueShownAsLabel = True
        _chartDowntime.Series.Add(s1)

        For Each l In _result.Lines.OrderByDescending(Function(x) x.DowntimeMinutes).Take(10)
            s1.Points.AddXY(l.LineNameAr, l.DowntimeMinutes)
        Next
    End Sub

    Private Sub BindScrap()
        _gridScrap.Rows.Clear()
        For Each i In _result.Items.OrderByDescending(Function(x) x.TotalScrap)
            Dim raw = _result.Rows.Where(Function(x) x.ItemID = i.ItemID).Sum(Function(x) x.RawMaterialScrapQty)
            Dim pack = _result.Rows.Where(Function(x) x.ItemID = i.ItemID).Sum(Function(x) x.PackagingScrapQty)
            _gridScrap.Rows.Add(
                i.ItemCode, i.ItemNameAr, i.ProducedQty,
                i.RejectedFG, raw, pack, i.TotalScrap, i.ScrapPct)
        Next

        _chartScrap.Series.Clear()
        Dim pie = NewSeries("توزيع الهوالك", SeriesChartType.Doughnut, Color.Empty)
        pie.IsValueShownAsLabel = True
        _chartScrap.Series.Add(pie)

        pie.Points.AddXY("مرفوض FG", _result.Summary.TotalRejectedFG)
        pie.Points.AddXY("خام", _result.Summary.TotalRawScrap)
        pie.Points.AddXY("تعبئة", _result.Summary.TotalPackScrap)
    End Sub

    Private Sub BindSupervisors()
        _gridSupervisors.Rows.Clear()
        For Each s In _result.Supervisors
            _gridSupervisors.Rows.Add(s.SupervisorNameAr, s.ProducedQty, s.RejectedFG,
                                      s.DowntimeMinutes, s.OEE, s.AchievementPct, s.RecordsCount)
        Next

        _chartSupervisors.Series.Clear()
        Dim s1 = NewSeries("OEE", SeriesChartType.Bar, Color.FromArgb(76, 175, 80))
        s1.IsValueShownAsLabel = True
        s1.LabelFormat = "P0"
        _chartSupervisors.Series.Add(s1)

        _chartSupervisors.ChartAreas(0).AxisX.LabelStyle.Format = "P0"
        _chartSupervisors.ChartAreas(0).AxisX.Maximum = 1.0R

        For Each s In _result.Supervisors.Take(15)
            s1.Points.AddXY(s.SupervisorNameAr, s.OEE)
        Next
    End Sub

    Private Sub BindDetail()
        _gridDetail.Rows.Clear()
        For Each r In _result.Rows
            _gridDetail.Rows.Add(
                r.ProductionDate.ToString("yyyy-MM-dd"),
                r.ShiftNameAr, r.LineNameAr,
                r.ItemCode, r.ItemNameAr, r.SupervisorNameAr,
                r.ProducedQty, r.RejectedFGQty, r.RawMaterialScrapQty, r.PackagingScrapQty,
                r.DowntimeMinutes, r.Availability, r.Performance, r.Quality, r.OEE, r.AchievementPct,
                r.Notes)
        Next
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Export
    ' ═══════════════════════════════════════════════════════════
    Private Sub OnExport()
        If _result Is Nothing OrElse _result.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV (*.csv)|*.csv"
                sfd.FileName = $"ProductionReport_{_filter.FromDate:yyyyMMdd}_{_filter.ToDate:yyyyMMdd}.csv"
                If sfd.ShowDialog() <> DialogResult.OK Then Return

                Dim sb As New StringBuilder()
                sb.AppendLine("التاريخ,الوردية,الخط,كود الصنف,الصنف,المشرف,الإنتاج,مرفوض FG,هوالك خام,هوالك تعبئة,توقف (د),A%,P%,Q%,OEE,إنجاز%,ملاحظات")

                For Each r In _result.Rows
                    sb.AppendLine(String.Join(",", {
                        r.ProductionDate.ToString("yyyy-MM-dd"),
                        Csv(r.ShiftNameAr), Csv(r.LineNameAr), Csv(r.ItemCode), Csv(r.ItemNameAr),
                        Csv(r.SupervisorNameAr),
                        r.ProducedQty.ToString("0.##"),
                        r.RejectedFGQty.ToString("0.##"),
                        r.RawMaterialScrapQty.ToString("0.##"),
                        r.PackagingScrapQty.ToString("0.##"),
                        r.DowntimeMinutes.ToString("0.##"),
                        r.Availability.ToString("P2"),
                        r.Performance.ToString("P2"),
                        r.Quality.ToString("P2"),
                        r.OEE.ToString("P2"),
                        r.AchievementPct.ToString("P2"),
                        Csv(r.Notes)
                    }))
                Next

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8)
                MessageBox.Show("تم التصدير:" & vbCrLf & sfd.FileName, "تم",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("خطأ في التصدير: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Function Csv(s As String) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If s.Contains(","c) OrElse s.Contains(""""c) OrElse s.Contains(vbCr) OrElse s.Contains(vbLf) Then
            Return """" & s.Replace("""", """""") & """"
        End If
        Return s
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Print
    ' ═══════════════════════════════════════════════════════════
    Private Sub DoPrint()
        If _result Is Nothing OrElse _result.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim pd As New PrintDocument()
            pd.DefaultPageSettings.Landscape = True
            Dim currentRow As Integer = 0
            Const rowsPerPage As Integer = 25

            AddHandler pd.PrintPage,
                Sub(s As Object, e As PrintPageEventArgs)
                    DrawPrintPage(e, currentRow, rowsPerPage)
                    currentRow += rowsPerPage
                    e.HasMorePages = currentRow < _result.Rows.Count
                End Sub

            Using ppd As New PrintPreviewDialog()
                ppd.Document = pd
                ppd.Width = 1000 : ppd.Height = 700
                ppd.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("خطأ في الطباعة: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DrawPrintPage(e As PrintPageEventArgs, startRow As Integer, maxRows As Integer)
        Dim g = e.Graphics
        Dim fTitle As New Font("Segoe UI", 16, FontStyle.Bold)
        Dim fSub As New Font("Segoe UI", 10, FontStyle.Regular)
        Dim fHead As New Font("Segoe UI", 9, FontStyle.Bold)
        Dim fCell As New Font("Segoe UI", 8.5, FontStyle.Regular)

        Dim y As Single = CSng(e.MarginBounds.Top)
        Dim sfR As New StringFormat() With {
            .Alignment = StringAlignment.Far,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        g.DrawString("تقرير الإنتاج", fTitle, Brushes.Black,
                     New RectangleF(CSng(e.MarginBounds.Left), y, CSng(e.MarginBounds.Width), 30.0F), sfR)
        y += 32.0F

        g.DrawString($"الفترة: {_filter.FromDate:yyyy-MM-dd} → {_filter.ToDate:yyyy-MM-dd}",
                     fSub, Brushes.Black,
                     New RectangleF(CSng(e.MarginBounds.Left), y, CSng(e.MarginBounds.Width), 20.0F), sfR)
        y += 22.0F

        Dim s = _result.Summary
        g.DrawString($"الإنتاج: {s.TotalProduced:N0} | مرفوض: {s.TotalRejectedFG:N0} | هوالك: {s.TotalScrap:N0} | توقف: {s.TotalDowntimeMinutes:N0}د | OEE: {s.OEE:P1} | إنجاز: {s.AchievementPct:P1}",
                     fSub, Brushes.DarkSlateGray,
                     New RectangleF(CSng(e.MarginBounds.Left), y, CSng(e.MarginBounds.Width), 20.0F), sfR)
        y += 26.0F

        Dim cols = {"التاريخ", "الوردية", "الخط", "الصنف", "المشرف", "إنتاج", "مرفوض", "هوالك", "توقف", "OEE", "إنجاز"}
        Dim colW As Single = CSng(e.MarginBounds.Width) / cols.Length

        Dim hy As Single = y
        For i = 0 To cols.Length - 1
            Dim rx As Single = CSng(e.MarginBounds.Right) - ((i + 1) * colW)
            g.FillRectangle(Brushes.LightSteelBlue, rx, hy, colW, 22.0F)
            g.DrawRectangle(Pens.Gray, rx, hy, colW, 22.0F)
            g.DrawString(cols(i), fHead, Brushes.Black,
                         New RectangleF(rx, hy + 3.0F, colW, 18.0F), sfR)
        Next
        y += 22.0F

        Dim endRow As Integer = Math.Min(startRow + maxRows, _result.Rows.Count)
        For i = startRow To endRow - 1
            Dim r = _result.Rows(i)
            Dim vals = {
                r.ProductionDate.ToString("yyyy-MM-dd"),
                r.ShiftNameAr, r.LineNameAr, r.ItemNameAr, r.SupervisorNameAr,
                r.ProducedQty.ToString("N0"), r.RejectedFGQty.ToString("N0"),
                r.TotalScrapQty.ToString("N0"), r.DowntimeMinutes.ToString("N0"),
                r.OEE.ToString("P1"), r.AchievementPct.ToString("P1")
            }
            For j = 0 To vals.Length - 1
                Dim rx As Single = CSng(e.MarginBounds.Right) - ((j + 1) * colW)
                g.DrawRectangle(Pens.LightGray, rx, y, colW, 18.0F)
                g.DrawString(vals(j), fCell, Brushes.Black,
                             New RectangleF(rx + 2.0F, y + 1.0F, colW - 4.0F, 16.0F), sfR)
            Next
            y += 18.0F
        Next

        g.DrawString($"صفحة — {DateTime.Now:yyyy-MM-dd HH:mm}", fCell, Brushes.Gray,
                     New RectangleF(CSng(e.MarginBounds.Left),
                                    CSng(e.MarginBounds.Bottom) - 20.0F,
                                    CSng(e.MarginBounds.Width), 18.0F), sfR)
    End Sub

End Class

