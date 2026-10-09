Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories

Public Class ProFinalProductUC

    ' ═══════════════════════════════════════════════════════
    '  Repos
    ' ═══════════════════════════════════════════════════════
    Private ReadOnly _fptRepo As New FinalProductTransferRepository()
    Private ReadOnly _lineRepo As New ProductionLineRepository()
    Private ReadOnly _workerRepo As New ProductionWorkerRepository()
    Private ReadOnly _shiftRepo As New ShiftTypeRepository()

    ' ═══════════════════════════════════════════════════════
    '  State
    ' ═══════════════════════════════════════════════════════
    Private _currentTransferId As Integer = 0
    Private _currentStatusId As Integer = 0
    Private _loadingAvail As Boolean = False
    Private _loadingTransfers As Boolean = False
    Private _loadingDetails As Boolean = False
    Private _lookupsLoaded As Boolean = False

    Private _allAvailable As List(Of AvailableProductionDto) = New List(Of AvailableProductionDto)()

    ' ═══════════════════════════════════════════════════════
    '  Load
    ' ═══════════════════════════════════════════════════════
    Private Sub ProFinalProductUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeAvailableGrid()
        InitializeTransfersGrid()
        InitializeDetailsGrid()
        LoadLookups()
        LoadAvailableProductions()
        LoadTransfers()
        ResetDocumentForm()
        UITheme.ApplyGridTheme(dgvAvailable)
        UITheme.ApplyGridTheme(dgvTransfers)
        UITheme.ApplyGridTheme(dgvTransferDetails)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Initialize Grids
    ' ═══════════════════════════════════════════════════════
    Private Sub InitializeAvailableGrid()
        With dgvAvailable
            .Columns.Clear() : .AutoGenerateColumns = False

            ' عمود الاختيار Checkbox
            Dim chkCol As New DataGridViewCheckBoxColumn()
            chkCol.Name = "Selected"
            chkCol.HeaderText = "☑"
            chkCol.FillWeight = 5
            chkCol.ReadOnly = False
            .Columns.Add(chkCol)

            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LogID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ProductionDate", .HeaderText = "التاريخ", .FillWeight = 11})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ShiftName", .HeaderText = "الوردية", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineName", .HeaderText = "الخط", .FillWeight = 13})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemCode", .HeaderText = "كود المنتج", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemName", .HeaderText = "المنتج", .FillWeight = 22})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ProducedQty", .HeaderText = "المنتَج", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitName", .HeaderText = "الوحدة", .FillWeight = 8})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "SupervisorName", .HeaderText = "المشرف", .FillWeight = 9})
        End With
    End Sub

    Private Sub InitializeTransfersGrid()
        With dgvTransfers
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TransferID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TransferNo", .HeaderText = "رقم المستند", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TransferDate", .HeaderText = "التاريخ", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ShiftName", .HeaderText = "الوردية", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineName", .HeaderText = "الخط", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "SupervisorName", .HeaderText = "المشرف", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "StatusName", .HeaderText = "الحالة", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TotalQty", .HeaderText = "إجمالي الكمية", .FillWeight = 13})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "DetailsCount", .HeaderText = "السطور", .FillWeight = 10})
        End With
    End Sub

    Private Sub InitializeDetailsGrid()
        With dgvTransferDetails
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "DetailID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemCode", .HeaderText = "كود المنتج", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemName", .HeaderText = "اسم المنتج", .FillWeight = 30})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TransferredQty", .HeaderText = "الكمية المحولة", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RejectedQty", .HeaderText = "المرفوض", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitName", .HeaderText = "الوحدة", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BatchNo", .HeaderText = "التشغيلة", .FillWeight = 16})
        End With
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Lookups
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadLookups()
        If _lookupsLoaded Then Return
        Try
            cboShift.DataSource = _shiftRepo.GetAll()
            cboShift.DisplayMember = "ShiftNameAr"
            cboShift.ValueMember = "ShiftTypeID"

            cboLine.DataSource = _lineRepo.GetAll()
            cboLine.DisplayMember = "LineNameAr"
            cboLine.ValueMember = "ProductionLineID"

            cboSupervisor.DataSource = _workerRepo.GetSupervisors()
            cboSupervisor.DisplayMember = "WorkerNameAr"
            cboSupervisor.ValueMember = "WorkerID"

            cboStorekeeper.DataSource = _workerRepo.GetAll()
            cboStorekeeper.DisplayMember = "WorkerNameAr"
            cboStorekeeper.ValueMember = "WorkerID"

            _lookupsLoaded = True
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل القوائم: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Available
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadAvailableProductions()
        Try
            _loadingAvail = True
            Cursor = Cursors.WaitCursor

            _allAvailable = _fptRepo.GetAvailableProductions()

            dgvAvailable.Rows.Clear()
            For Each a In _allAvailable
                dgvAvailable.Rows.Add(
                    False,                                       ' ☑ غير محدد
                    a.LogID,
                    a.ProductionDate.ToString("yyyy-MM-dd"),
                    a.ShiftNameAr,
                    a.ProductionLineNameAr,
                    a.ItemCode,
                    a.ItemNameAr,
                    a.ProducedQty.ToString("N3"),
                    a.UnitName,
                    a.SupervisorNameAr
                )
            Next

            UpdateSelectedCount()

            If _allAvailable.Count = 0 Then
                MessageBox.Show("لا يوجد إنتاج متاح للتحويل حاليًا." & vbCrLf &
                                "قد تكون كل السجلات تم تحويلها بالفعل.",
                                "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الإنتاج المتاح: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            _loadingAvail = False
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Update Selected Count
    ' ═══════════════════════════════════════════════════════
    Private Sub UpdateSelectedCount()
        Dim selected As Integer = 0
        For Each row As DataGridViewRow In dgvAvailable.Rows
            If CBool(row.Cells("Selected").Value) Then selected += 1
        Next
        lblSelectedCount.Text = $"✅  المحدد: {selected} / الإجمالي: {dgvAvailable.Rows.Count}"
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Transfers
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadTransfers(Optional ByVal selectId As Integer = 0)
        Try
            _loadingTransfers = True
            Cursor = Cursors.WaitCursor

            Dim transfers As List(Of FinalProductTransferDto) = _fptRepo.GetAll()

            dgvTransfers.Rows.Clear()
            For Each t In transfers
                dgvTransfers.Rows.Add(
                    t.TransferID,
                    t.TransferNo,
                    t.TransferDate.ToString("yyyy-MM-dd"),
                    t.ShiftNameAr,
                    t.ProductionLineNameAr,
                    t.SupervisorNameAr,
                    t.StatusNameAr,
                    t.TotalTransferredQty.ToString("N3"),
                    t.DetailsCount
                )
            Next

            lblDocCount.Text = $"📋  المستندات: {dgvTransfers.Rows.Count}"

            If selectId > 0 Then
                For Each row As DataGridViewRow In dgvTransfers.Rows
                    If CInt(row.Cells("TransferID").Value) = selectId Then
                        row.Selected = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل المستندات: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            _loadingTransfers = False

            ' ⭐ الحل: بعد ما _loadingTransfers يبقى False — نستدعي الاختيار يدويًا
            If selectId > 0 AndAlso dgvTransfers.SelectedRows.Count > 0 Then
                ApplyTransferSelection(selectId)
            End If
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Transfer Details
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadTransferDetails(transferId As Integer)
        Try
            _loadingDetails = True
            Dim details As List(Of FinalProductTransferDetailDto) = _fptRepo.GetDetails(transferId)

            dgvTransferDetails.Rows.Clear()
            For Each d In details
                dgvTransferDetails.Rows.Add(
                    d.DetailID,
                    d.ItemCode,
                    d.ItemNameAr,
                    d.TransferredQty.ToString("N3"),
                    d.RejectedQty.ToString("N3"),
                    d.UnitName,
                    If(String.IsNullOrEmpty(d.BatchNo), "-", d.BatchNo)
                )
            Next

            lblDetailTitle.Text = $"📦  سطور المستند: {dgvTransferDetails.Rows.Count} سطر"
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل السطور: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingDetails = False
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Reset Document Form
    ' ═══════════════════════════════════════════════════════
    Private Sub ResetDocumentForm()
        Try
            txtDocNo.Text = _fptRepo.GetNextTransferNo()
        Catch
            txtDocNo.Text = "FPT-" & Date.Now.ToString("yyyyMM") & "-0001"
        End Try

        dtpDate.Value = Date.Today
        txtNotes.Clear()
        If cboShift.Items.Count > 0 Then cboShift.SelectedIndex = 0
        If cboLine.Items.Count > 0 Then cboLine.SelectedIndex = 0
        If cboSupervisor.Items.Count > 0 Then cboSupervisor.SelectedIndex = 0
        If cboStorekeeper.Items.Count > 0 Then cboStorekeeper.SelectedIndex = 0
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Selected Row Getter
    ' ═══════════════════════════════════════════════════════
    Private Function GetSelectedAvailable() As List(Of AvailableProductionDto)
        Dim result As New List(Of AvailableProductionDto)()
        For Each row As DataGridViewRow In dgvAvailable.Rows
            If CBool(row.Cells("Selected").Value) Then
                Dim logId As Long = CLng(row.Cells("LogID").Value)
                Dim found = _allAvailable.FirstOrDefault(Function(x) x.LogID = logId)
                If found IsNot Nothing Then result.Add(found)
            End If
        Next
        Return result
    End Function

    ' ═══════════════════════════════════════════════════════
    '  Events — dgvAvailable
    ' ═══════════════════════════════════════════════════════
    Private Sub DgvAvailable_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) _
        Handles dgvAvailable.CurrentCellDirtyStateChanged

        If dgvAvailable.IsCurrentCellDirty Then
            dgvAvailable.CommitEdit(DataGridViewDataErrorContexts.Commit)
            UpdateSelectedCount()
        End If
    End Sub

    Private Sub DgvAvailable_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) _
        Handles dgvAvailable.CellValueChanged

        If e.ColumnIndex = dgvAvailable.Columns("Selected").Index Then
            UpdateSelectedCount()
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Events — Search Tab 1
    ' ═══════════════════════════════════════════════════════
    ' ═══════════════════════════════════════════════════════
    '  Events — Cancel Transfer
    ' ═══════════════════════════════════════════════════════
    Private Sub BtnCancelTransfer_Click(sender As Object, e As EventArgs) _
        Handles btnCancelTransfer.Click

        If _currentTransferId = 0 Then
            MessageBox.Show("اختر مستندًا أولاً.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If

        ' ═══ بناء رسالة التأكيد ═══
        Dim transferNo As String = ""
        Dim totalQty As Decimal = 0
        Dim lineCount As Integer = 0

        If dgvTransfers.SelectedRows.Count > 0 Then
            transferNo = dgvTransfers.SelectedRows(0).Cells("TransferNo").Value.ToString()
            Decimal.TryParse(dgvTransfers.SelectedRows(0).Cells("TotalQty").Value.ToString(), totalQty)
            Integer.TryParse(dgvTransfers.SelectedRows(0).Cells("DetailsCount").Value.ToString(), lineCount)
        End If

        Dim msg As String =
            $"هل أنت متأكد من إلغاء التحويل: {transferNo}؟" & vbCrLf & vbCrLf &
            $"📋 عدد السطور: {lineCount}" & vbCrLf &
            $"📦 إجمالي الكمية: {totalQty:N3}" & vbCrLf & vbCrLf &
            "⚠️ بعد الإلغاء:" & vbCrLf &
            "✓ الكميات ترجع إلى (الإنتاج المتاح للتحويل)" & vbCrLf &
            "✓ المستند يفضل في السجل (للأرشيف)" & vbCrLf &
            "✓ بيانات الإنتاج اليومي لا تتأثر"

        If MessageBox.Show(msg, "تأكيد إلغاء التحويل",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning,
                           MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then Return

        ' ═══ التنفيذ ═══
        Try
            Cursor = Cursors.WaitCursor

            _fptRepo.CancelTransfer(_currentTransferId, CurrentUserID())

            MessageBox.Show("تم إلغاء التحويل بنجاح." & vbCrLf &
                            "الكميات رجعت للإنتاج المتاح.",
                            "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ═══ إعادة تحميل الواجهات ═══
            LoadTransfers()                        ' تحديث المستندات (بدون إلغاء)
            LoadAvailableProductions()             ' تحديث الإنتاج المتاح (الكميات رجعت)
            _currentTransferId = 0
            _currentStatusId = 0
            dgvTransferDetails.Rows.Clear()
            lblDetailTitle.Text = "📦  سطور المستند: (اختر مستند)"
            EnableDisableButtons(0)

            ' ═══ الانتقال للتاب 1 ═══
            tabMain.SelectedTab = tabAvailable

        Catch ex As Exception
            MessageBox.Show("خطأ في الإلغاء: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim result = _fptRepo.GetAvailableProductions(dtpFrom.Value.Date, dtpTo.Value.Date)
            _allAvailable = result

            dgvAvailable.Rows.Clear()
            For Each a In result
                dgvAvailable.Rows.Add(
                    False, a.LogID,
                    a.ProductionDate.ToString("yyyy-MM-dd"),
                    a.ShiftNameAr, a.ProductionLineNameAr,
                    a.ItemCode, a.ItemNameAr,
                    a.ProducedQty.ToString("N3"),
                    a.UnitName, a.SupervisorNameAr
                )
            Next
            UpdateSelectedCount()
        Catch ex As Exception
            MessageBox.Show("خطأ في البحث: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        dtpFrom.Value = Date.Today.AddMonths(-1)
        dtpTo.Value = Date.Today
        LoadAvailableProductions()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Convert Button (الأهم)
    ' ═══════════════════════════════════════════════════════
    Private Sub BtnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        ' ① التحقق
        Dim selected = GetSelectedAvailable()
        If selected.Count = 0 Then
            MessageBox.Show("الرجاء تحديد سجل واحد على الأقل.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtDocNo.Text) Then
            MessageBox.Show("رقم المستند مطلوب.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If
        If cboShift.SelectedValue Is Nothing Then
            MessageBox.Show("الوردية مطلوبة.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If
        If cboLine.SelectedValue Is Nothing Then
            MessageBox.Show("خط الإنتاج مطلوب.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If
        If cboSupervisor.SelectedValue Is Nothing Then
            MessageBox.Show("المشرف مطلوب.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If
        If cboStorekeeper.SelectedValue Is Nothing Then
            MessageBox.Show("أمين المخزن مطلوب.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If

        ' ② تأكيد
        Dim confirmMsg As String = $"سيتم تحويل {selected.Count} سجل إلى المخزن:" & vbCrLf & vbCrLf
        For Each s In selected
            confirmMsg &= $"• {s.ItemNameAr}  ({s.ProducedQty:N3} {s.UnitName})" & vbCrLf
        Next
        confirmMsg &= vbCrLf & "هل أنت متأكد؟"

        If MessageBox.Show(confirmMsg, "تأكيد التحويل",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        ' ③ إنشاء المستند
        Try
            Cursor = Cursors.WaitCursor

            ' 3.1 — إنشاء الرأس
            Dim header As New FinalProductTransferDto With {
                .TransferNo = txtDocNo.Text.Trim(),
                .TransferDate = dtpDate.Value.Date,
                .ShiftTypeID = CInt(cboShift.SelectedValue),
                .ProductionLineID = CInt(cboLine.SelectedValue),
                .SupervisorID = CInt(cboSupervisor.SelectedValue),
                .StorekeeperID = CInt(cboStorekeeper.SelectedValue),
                .Notes = txtNotes.Text.Trim()
            }

            Dim transferId As Integer = _fptRepo.Insert(header, CurrentUserID())

            ' 3.2 — إضافة السطور
            For Each s In selected
                Dim detail As New FinalProductTransferDetailDto With {
                    .TransferID = transferId,
                    .DailyLogID = s.LogID,
                    .ItemID = s.ItemID,
                    .TransferredQty = s.ProducedQty,
                    .RejectedQty = s.RejectedFGQty,
                    .BatchNo = Nothing,
                    .ExpiryDate = Nothing,
                    .Notes = Nothing
                }
                _fptRepo.InsertDetail(detail, CurrentUserID())
            Next

            ' 3.3 — الاعتماد مباشرة (اختياري)
            _fptRepo.Approve(transferId, CurrentUserID())

            MessageBox.Show($"تم إنشاء المستند: {header.TransferNo}" & vbCrLf &
                            $"عدد السطور: {selected.Count}" & vbCrLf &
                            "الحالة: معتمد — جاهز للاستلام من المخزن.",
                            "تم بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ④ تحديث الواجهات
            LoadTransfers(transferId)
            LoadAvailableProductions()
            ResetDocumentForm()

            ' ⑤ الانتقال لتاب المستندات
            tabMain.SelectedTab = tabDocuments

        Catch ex As Exception
            MessageBox.Show("خطأ في التحويل: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Events — dgvTransfers
    ' ═══════════════════════════════════════════════════════
    ' ═══════════════════════════════════════════════════════
    '  Selection Handler + Extracted Logic
    ' ═══════════════════════════════════════════════════════
    Private Sub dgvTransfers_SelectionChanged(sender As Object, e As EventArgs) _
        Handles dgvTransfers.SelectionChanged

        If _loadingTransfers Then Return
        If dgvTransfers.SelectedRows.Count = 0 Then Return

        Dim id As Integer = CInt(dgvTransfers.SelectedRows(0).Cells("TransferID").Value)
        ApplyTransferSelection(id)
    End Sub

    ''' <summary>
    ''' تطبيق الاختيار — يُستدعى من:
    ''' - SelectionChanged (عند اختيار يدوي)
    ''' - LoadTransfers (بعد التحويل مباشرة)
    ''' </summary>
    Private Sub ApplyTransferSelection(transferId As Integer)
        If dgvTransfers.SelectedRows.Count = 0 Then Return

        Dim statusName As String = dgvTransfers.SelectedRows(0).Cells("StatusName").Value.ToString()

        _currentTransferId = transferId

        ' تحديد الحالة
        _currentStatusId = 0
        If statusName = "مسودة" Then _currentStatusId = 1
        If statusName = "معتمد" Then _currentStatusId = 2
        If statusName = "مستلم" Then _currentStatusId = 3
        If statusName = "ملغي" Then _currentStatusId = 4

        ' تفعيل/تعطيل الأزرار
        EnableDisableButtons(_currentStatusId)

        ' تحميل السطور
        LoadTransferDetails(transferId)
    End Sub

    Private Sub EnableDisableButtons(statusId As Integer)
        btnApprove.Enabled = (statusId = 1)
        btnReceive.Enabled = (statusId = 2)

        ' ⭐ الإلغاء متاح للمعتمد (قبل الاستلام) فقط
        btnCancelTransfer.Enabled = (statusId = 2)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Events — Approve / Receive
    ' ═══════════════════════════════════════════════════════
    Private Sub BtnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If _currentTransferId = 0 Then
            MessageBox.Show("اختر مستندًا أولاً.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If

        If MessageBox.Show("هل أنت متأكد من اعتماد المستند؟" & vbCrLf &
                           "بعد الاعتماد — لن تتمكن من التعديل.",
                           "تأكيد الاعتماد", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            _fptRepo.Approve(_currentTransferId, CurrentUserID())
            MessageBox.Show("تم اعتماد المستند بنجاح.", "تم",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTransfers(_currentTransferId)
            LoadTransferDetails(_currentTransferId)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnReceive_Click(sender As Object, e As EventArgs) Handles btnReceive.Click
        If _currentTransferId = 0 Then
            MessageBox.Show("اختر مستندًا أولاً.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        End If

        If MessageBox.Show("هل أنت متأكد من استلام المستند؟" & vbCrLf &
                           "بعد الاستلام — ينتهي المسار.",
                           "تأكيد الاستلام", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            _fptRepo.Receive(_currentTransferId, CurrentUserID())
            MessageBox.Show("تم استلام المستند بنجاح.", "تم",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTransfers(_currentTransferId)
            LoadTransferDetails(_currentTransferId)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRefreshDoc_Click(sender As Object, e As EventArgs) Handles btnRefreshDoc.Click
        Dim keepId As Integer = _currentTransferId
        LoadTransfers(keepId)

        ' لو مفيش اختيار حالي، اختر الصف الأول
        If keepId = 0 AndAlso dgvTransfers.Rows.Count > 0 Then
            dgvTransfers.Rows(0).Selected = True
            ApplyTransferSelection(CInt(dgvTransfers.Rows(0).Cells("TransferID").Value))
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Events — Search Tab 2
    ' ═══════════════════════════════════════════════════════
    Private Sub BtnDocSearch_Click(sender As Object, e As EventArgs) Handles btnDocSearch.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim result = _fptRepo.GetAll(dtpDocFrom.Value.Date, dtpDocTo.Value.Date)

            dgvTransfers.Rows.Clear()
            For Each t In result
                dgvTransfers.Rows.Add(
                    t.TransferID, t.TransferNo,
                    t.TransferDate.ToString("yyyy-MM-dd"),
                    t.ShiftNameAr, t.ProductionLineNameAr,
                    t.SupervisorNameAr, t.StatusNameAr,
                    t.TotalTransferredQty.ToString("N3"),
                    t.DetailsCount
                )
            Next
            lblDocCount.Text = $"📋  المستندات: {dgvTransfers.Rows.Count}"
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnDocClear_Click(sender As Object, e As EventArgs) Handles btnDocClear.Click
        dtpDocFrom.Value = Date.Today.AddMonths(-1)
        dtpDocTo.Value = Date.Today
        LoadTransfers()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Helpers
    ' ═══════════════════════════════════════════════════════
    Private Function CurrentUserID() As Integer
        Return 1
    End Function

End Class