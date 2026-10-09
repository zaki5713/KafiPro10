Imports System.Collections.Generic
Imports System.Drawing
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories

Public Class ProShiftsUC

    Private ReadOnly _repo As New ShiftTypeRepository()
    Private _currentId As Integer = 0
    Private _loading As Boolean = False

    ' ═══════════════════════════════════════════════════════
    '  Load
    ' ═══════════════════════════════════════════════════════
    Private Sub ProShiftsUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGrid()
        LoadData()
        ResetForm()
        UITheme.ApplyGridTheme(dgvShifts)
    End Sub

    Private Sub InitializeGrid()
        With dgvShifts
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ShiftTypeID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ShiftCode", .HeaderText = "كود الوردية", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ShiftNameAr", .HeaderText = "الاسم عربي", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ShiftNameEn", .HeaderText = "الاسم إنجليزي", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "StartTime", .HeaderText = "البداية", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "EndTime", .HeaderText = "النهاية", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Overtime", .HeaderText = "أوفرتيم", .FillWeight = 8})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Active", .HeaderText = "نشط", .FillWeight = 7})
        End With
    End Sub

    Private Sub LoadData(Optional ByVal selectId As Integer = 0)
        Try
            _loading = True
            Dim items As List(Of ShiftTypeDto) = _repo.GetAll()
            dgvShifts.Rows.Clear()
            For Each s As ShiftTypeDto In items
                Dim startTxt As String = "-"
                Dim endTxt As String = "-"
                If s.StartTime.HasValue Then startTxt = s.StartTime.Value.ToString("hh\:mm")
                If s.EndTime.HasValue Then endTxt = s.EndTime.Value.ToString("hh\:mm")

                Dim overtimeTxt As String = "✗"
                If s.IsOvertime Then overtimeTxt = "✓"

                Dim activeTxt As String = "✗"
                If s.IsActive Then activeTxt = "✓"

                dgvShifts.Rows.Add(s.ShiftTypeID, s.ShiftCode, s.ShiftNameAr, s.ShiftNameEn,
                                   startTxt, endTxt, overtimeTxt, activeTxt)
            Next

            If selectId > 0 Then
                For Each row As DataGridViewRow In dgvShifts.Rows
                    If CInt(row.Cells("ShiftTypeID").Value) = selectId Then row.Selected = True : Exit For
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الورديات: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loading = False
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Load Into Form
    ' ═══════════════════════════════════════════════════════
    Private Sub LoadIntoForm(id As Integer)
        Try
            Dim s As ShiftTypeDto = _repo.GetById(id)
            If s Is Nothing Then Return

            _currentId = s.ShiftTypeID
            txtID.Text = s.ShiftTypeID.ToString()
            txtCode.Text = s.ShiftCode
            txtNameAr.Text = s.ShiftNameAr
            txtNameEn.Text = s.ShiftNameEn

            If s.StartTime.HasValue Then
                dtpStartTime.Value = Date.Today.Add(s.StartTime.Value)
            Else
                dtpStartTime.Value = Date.Today
            End If

            If s.EndTime.HasValue Then
                dtpEndTime.Value = Date.Today.Add(s.EndTime.Value)
            Else
                dtpEndTime.Value = Date.Today
            End If

            chkOvertime.Checked = s.IsOvertime
            chkActive.Checked = s.IsActive

            lblFormMode.Text = "✏️  وضع التعديل — ID: " & id
            lblFormMode.BackColor = UITheme.Primary
            lblFormMode.ForeColor = UITheme.TextWhite
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Reset
    ' ═══════════════════════════════════════════════════════
    Private Sub ResetForm()
        _currentId = 0
        txtID.Clear() : txtCode.Clear() : txtNameAr.Clear() : txtNameEn.Clear()
        dtpStartTime.Value = Date.Today.AddHours(8)
        dtpEndTime.Value = Date.Today.AddHours(16)
        chkOvertime.Checked = False
        chkActive.Checked = True

        lblFormMode.Text = "➕  وضع الإضافة"
        lblFormMode.BackColor = UITheme.Success
        lblFormMode.ForeColor = UITheme.TextWhite

        dgvShifts.ClearSelection()
        txtCode.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Grid Selection
    ' ═══════════════════════════════════════════════════════
    Private Sub dgvShifts_SelectionChanged(sender As Object, e As EventArgs) Handles dgvShifts.SelectionChanged
        If _loading Then Return
        If dgvShifts.SelectedRows.Count = 0 Then Return
        LoadIntoForm(CInt(dgvShifts.SelectedRows(0).Cells("ShiftTypeID").Value))
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Buttons
    ' ═══════════════════════════════════════════════════════
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ResetForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtCode.Text) Then
            Msg("كود الوردية مطلوب.", txtCode) : Return
        End If
        If String.IsNullOrWhiteSpace(txtNameAr.Text) Then
            Msg("الاسم العربي مطلوب.", txtNameAr) : Return
        End If
        If String.IsNullOrWhiteSpace(txtNameEn.Text) Then
            Msg("الاسم الإنجليزي مطلوب.", txtNameEn) : Return
        End If

        Dim model As New ShiftTypeDto With {
            .ShiftTypeID = _currentId,
            .ShiftCode = txtCode.Text.Trim(),
            .ShiftNameAr = txtNameAr.Text.Trim(),
            .ShiftNameEn = txtNameEn.Text.Trim(),
            .StartTime = dtpStartTime.Value.TimeOfDay,
            .EndTime = dtpEndTime.Value.TimeOfDay,
            .IsOvertime = chkOvertime.Checked,
            .IsActive = chkActive.Checked
        }

        Try
            If _currentId = 0 Then
                Dim newId As Integer = _repo.Insert(model, 1)
                MessageBox.Show("تم إضافة الوردية بنجاح.", "تم",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData(newId) : LoadIntoForm(newId)
            Else
                _repo.Update(model, 1)
                MessageBox.Show("تم تعديل الوردية بنجاح.", "تم",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData(_currentId) : LoadIntoForm(_currentId)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ResetForm()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _currentId = 0 Then Msg("اختر وردية من الجدول أولاً.") : Return

        If MessageBox.Show($"هل أنت متأكد من حذف الوردية: {txtNameAr.Text}؟",
                           "تأكيد الحذف", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            _repo.Delete(_currentId, 1)
            MessageBox.Show("تم حذف الوردية بنجاح.", "تم",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData() : ResetForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim keepId As Integer = _currentId
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