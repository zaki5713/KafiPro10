Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces

Public Class ProDailyProductionUC

    ' ═══════════════════════════════════════════════════════
    '  Repos
    ' ═══════════════════════════════════════════════════════

    Private ReadOnly _logRepo As New DailyProductionLogRepository()
    Private ReadOnly _shiftRepo As New ShiftTypeRepository()
    Private ReadOnly _lineRepo As New ProductionLineRepository()
    Private ReadOnly _itemRepo As New ItemRepository()
    Private ReadOnly _workerRepo As New ProductionWorkerRepository()

    ' ═══════════════════════════════════════════════════════
    '  State
    ' ═══════════════════════════════════════════════════════
    Private _currentLogId As Long = 0
    Private _loadingGrid As Boolean = False
    Private _lookupsLoaded As Boolean = False

    ' ═══════════════════════════════════════════════════════
    '  Load
    ' ═══════════════════════════════════════════════════════
    Private Sub ProDailyProductionUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGrid()
        LoadLookups()
        LoadData()
        ResetForm()
        UITheme.ApplyGridTheme(dgvLogs)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Grid
    ' ═══════════════════════════════════════════════════════
    Private Sub InitializeGrid()
        With dgvLogs
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LogID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ProductionDate", .HeaderText = "التاريخ", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ShiftNameAr", .HeaderText = "الوردية", .FillWeight = 9})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineNameAr", .HeaderText = "الخط", .FillWeight = 11})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemNameAr", .HeaderText = "المنتج", .FillWeight = 20})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "SupervisorNameAr", .HeaderText = "المشرف", .FillWeight = 13})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ProducedQty", .HeaderText = "المنتَج", .FillWeight = 8})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Rejected", .HeaderText = "المرفوض", .FillWeight = 7})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RawScrap", .HeaderText = "هوالك خام", .FillWeight = 7})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "PackScrap", .HeaderText = "هوالك تغليف", .FillWeight = 7})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Downtime", .HeaderText = "توقف (د)", .FillWeight = 8})
        End With
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Lookups
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadLookups()
        If _lookupsLoaded Then Return
        Try
            ' الورديات
            cboShift.DataSource = _shiftRepo.GetAll()
            cboShift.DisplayMember = "ShiftNameAr"
            cboShift.ValueMember = "ShiftTypeID"

            ' خطوط الإنتاج
            cboLine.DataSource = _lineRepo.GetAll()
            cboLine.DisplayMember = "LineNameAr"
            cboLine.ValueMember = "ProductionLineID"

            ' المنتجات التامة
            cboItem.DataSource = _itemRepo.GetFinishedGoods()
            cboItem.DisplayMember = "ItemNameAr"
            cboItem.ValueMember = "ItemID"

            ' المشرفون
            cboSupervisor.DataSource = _workerRepo.GetSupervisors()
            cboSupervisor.DisplayMember = "WorkerNameAr"
            cboSupervisor.ValueMember = "WorkerID"

            _lookupsLoaded = True
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل القوائم: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Data
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadData(Optional ByVal selectLogId As Long = 0)
        Try
            _loadingGrid = True
            Dim logs As List(Of DailyProductionLogDto) = _logRepo.GetAll()

            dgvLogs.Rows.Clear()
            For Each l As DailyProductionLogDto In logs
                dgvLogs.Rows.Add(l.LogID,
                                 l.ProductionDate.ToString("yyyy-MM-dd"),
                                 l.ShiftNameAr,
                                 l.ProductionLineNameAr,
                                 l.ItemNameAr,
                                 l.SupervisorNameAr,
                                 l.ProducedQty.ToString("N3"),
                                 l.RejectedFGQty.ToString("N3"),
                                 l.RawMaterialScrapQty.ToString("N3"),
                                 l.PackagingScrapQty.ToString("N3"),
                                 l.DowntimeMinutes.ToString("N2"))
            Next

            If selectLogId > 0 Then
                For Each row As DataGridViewRow In dgvLogs.Rows
                    If CLng(row.Cells("LogID").Value) = selectLogId Then
                        row.Selected = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل البيانات: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingGrid = False
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Into Form
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadIntoForm(logId As Long)
        Try
            Dim l As DailyProductionLogDto = _logRepo.GetById(logId)
            If l Is Nothing Then Return

            _currentLogId = l.LogID

            txtID.Text = l.LogID.ToString()
            dtpDate.Value = l.ProductionDate
            cboShift.SelectedValue = l.ShiftTypeID
            cboLine.SelectedValue = l.ProductionLineID
            cboItem.SelectedValue = l.ItemID
            cboSupervisor.SelectedValue = l.SupervisorID
            txtProducedQty.Text = l.ProducedQty.ToString("0.###")
            txtRejected.Text = l.RejectedFGQty.ToString("0.###")
            txtRawScrap.Text = l.RawMaterialScrapQty.ToString("0.###")
            txtPackScrap.Text = l.PackagingScrapQty.ToString("0.###")
            txtDowntime.Text = l.DowntimeMinutes.ToString("0.##")
            txtNotes.Text = l.Notes

            lblFormMode.Text = "✏️  وضع التعديل — ID: " & logId
            lblFormMode.BackColor = UITheme.Primary
            lblFormMode.ForeColor = UITheme.TextWhite
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Reset Form
    ' ═══════════════════════════════════════════════════════
    Private Sub ResetForm()
        _currentLogId = 0
        txtID.Clear()
        dtpDate.Value = Date.Today
        If cboShift.Items.Count > 0 Then cboShift.SelectedIndex = 0
        If cboLine.Items.Count > 0 Then cboLine.SelectedIndex = 0
        If cboItem.Items.Count > 0 Then cboItem.SelectedIndex = 0
        If cboSupervisor.Items.Count > 0 Then cboSupervisor.SelectedIndex = 0
        txtProducedQty.Clear()
        txtRejected.Clear() : txtRejected.Text = "0"
        txtRawScrap.Clear() : txtRawScrap.Text = "0"
        txtPackScrap.Clear() : txtPackScrap.Text = "0"
        txtDowntime.Clear() : txtDowntime.Text = "0"
        txtNotes.Clear()

        lblFormMode.Text = "➕  وضع الإضافة"
        lblFormMode.BackColor = UITheme.Success
        lblFormMode.ForeColor = UITheme.TextWhite

        dgvLogs.ClearSelection()
        txtProducedQty.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Events
    ' ═══════════════════════════════════════════════════════
    Private Sub dgvLogs_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLogs.SelectionChanged
        If _loadingGrid Then Return
        If dgvLogs.SelectedRows.Count = 0 Then Return
        LoadIntoForm(CLng(dgvLogs.SelectedRows(0).Cells("LogID").Value))
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ResetForm()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ResetForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' التحقق
        If cboShift.SelectedValue Is Nothing Then Msg("الوردية مطلوبة.") : Return
        If cboLine.SelectedValue Is Nothing Then Msg("خط الإنتاج مطلوب.") : Return
        If cboItem.SelectedValue Is Nothing Then Msg("المنتج مطلوب.") : Return
        If cboSupervisor.SelectedValue Is Nothing Then Msg("المشرف مطلوب.") : Return
        If String.IsNullOrWhiteSpace(txtProducedQty.Text) Then Msg("الكمية المنتجة مطلوبة.", txtProducedQty) : Return

        Dim produced As Decimal
        If Not Decimal.TryParse(txtProducedQty.Text, produced) Then Msg("الكمية المنتجة يجب أن تكون رقمًا.", txtProducedQty) : Return
        If produced <= 0 Then Msg("الكمية المنتجة يجب أن تكون أكبر من صفر.", txtProducedQty) : Return

        Dim rejected As Decimal = 0
        If Not String.IsNullOrWhiteSpace(txtRejected.Text) Then
            If Not Decimal.TryParse(txtRejected.Text, rejected) Then Msg("المرفوض يجب أن يكون رقمًا.", txtRejected) : Return
        End If
        If rejected > produced Then Msg("المنتج المرفوض لا يمكن أن يتجاوز الكمية المنتجة.", txtRejected) : Return

        Dim rawScrap As Decimal = 0
        If Not String.IsNullOrWhiteSpace(txtRawScrap.Text) Then
            If Not Decimal.TryParse(txtRawScrap.Text, rawScrap) Then Msg("هوالك الخام يجب أن تكون رقمًا.", txtRawScrap) : Return
        End If

        Dim packScrap As Decimal = 0
        If Not String.IsNullOrWhiteSpace(txtPackScrap.Text) Then
            If Not Decimal.TryParse(txtPackScrap.Text, packScrap) Then Msg("هوالك التغليف يجب أن تكون رقمًا.", txtPackScrap) : Return
        End If

        Dim downtime As Decimal = 0
        If Not String.IsNullOrWhiteSpace(txtDowntime.Text) Then
            If Not Decimal.TryParse(txtDowntime.Text, downtime) Then Msg("وقت التوقف يجب أن يكون رقمًا.", txtDowntime) : Return
        End If

        Dim model As New DailyProductionLogDto With {
            .LogID = _currentLogId,
            .ProductionDate = dtpDate.Value.Date,
            .ShiftTypeID = CInt(cboShift.SelectedValue),
            .ProductionLineID = CInt(cboLine.SelectedValue),
            .ItemID = CInt(cboItem.SelectedValue),
            .SupervisorID = CInt(cboSupervisor.SelectedValue),
            .ProducedQty = produced,
            .RejectedFGQty = rejected,
            .RawMaterialScrapQty = rawScrap,
            .PackagingScrapQty = packScrap,
            .DowntimeMinutes = downtime,
            .Notes = txtNotes.Text.Trim()
        }

        Try
            If _currentLogId = 0 Then
                Dim newId As Long = _logRepo.Insert(model, 1)
                MessageBox.Show("تم إضافة سجل الإنتاج بنجاح.", "تم",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData(newId)
                LoadIntoForm(newId)
            Else
                _logRepo.Update(model, 1)
                MessageBox.Show("تم تعديل سجل الإنتاج بنجاح.", "تم",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData(_currentLogId)
                LoadIntoForm(_currentLogId)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _currentLogId = 0 Then Msg("اختر سجلًا من الجدول أولاً.") : Return

        If MessageBox.Show($"هل أنت متأكد من حذف السجل؟" & vbCrLf &
                           "لن يمكن التراجع عن هذه العملية.",
                           "تأكيد الحذف", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            _logRepo.Delete(_currentLogId, 1)
            MessageBox.Show("تم حذف السجل بنجاح.", "تم",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ResetForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim keepId As Long = _currentLogId
        LoadData(keepId)
        If keepId > 0 Then LoadIntoForm(keepId) Else ResetForm()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Helpers
    ' ═══════════════════════════════════════════════════════
    Private Sub Msg(text As String, Optional focus As Control = Nothing)
        MessageBox.Show(text, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        If focus IsNot Nothing Then focus.Focus()
    End Sub

End Class