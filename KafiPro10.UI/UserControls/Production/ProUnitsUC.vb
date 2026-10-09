Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories
Imports KafiPro.UI.Shared.Themes

Public Class ProUnitsUC

    Private ReadOnly _repo As New UnitOfMeasureRepository()
    Private _currentId As Integer = 0
    Private _loading As Boolean = False

    ' ═══════════════════════════════════════════════════════
    '  Load
    ' ═══════════════════════════════════════════════════════
    Private Sub ProUnitsUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGrid()
        LoadData()
        ResetForm()
        UITheme.ApplyGridTheme(dgvUnits)
    End Sub

    Private Sub InitializeGrid()
        With dgvUnits
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitOfMeasureID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitNameAr", .HeaderText = "الاسم (عربي)", .FillWeight = 50})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitNameEn", .HeaderText = "الاسم (إنجليزي)", .FillWeight = 50})
        End With
    End Sub

    Private Sub LoadData(Optional ByVal selectId As Integer = 0)
        Try
            _loading = True
            Dim items As List(Of UnitOfMeasureDto) = _repo.GetAll()
            dgvUnits.Rows.Clear()
            For Each u As UnitOfMeasureDto In items
                dgvUnits.Rows.Add(u.UnitOfMeasureID, u.UnitNameAr, u.UnitNameEn)
            Next

            If selectId > 0 Then
                For Each row As DataGridViewRow In dgvUnits.Rows
                    If CInt(row.Cells("UnitOfMeasureID").Value) = selectId Then row.Selected = True : Exit For
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الوحدات: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loading = False
        End Try
    End Sub

    Private Sub LoadIntoForm(id As Integer)
        Try
            Dim u As UnitOfMeasureDto = _repo.GetById(id)
            If u Is Nothing Then Return

            _currentId = u.UnitOfMeasureID
            txtID.Text = u.UnitOfMeasureID.ToString()
            txtNameAr.Text = u.UnitNameAr
            txtNameEn.Text = u.UnitNameEn

            lblFormMode.Text = "✏️  وضع التعديل — ID: " & id
            lblFormMode.BackColor = UITheme.Primary
            lblFormMode.ForeColor = UITheme.TextWhite
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetForm()
        _currentId = 0
        txtID.Clear() : txtNameAr.Clear() : txtNameEn.Clear()

        lblFormMode.Text = "➕  وضع الإضافة"
        lblFormMode.BackColor = UITheme.Success
        lblFormMode.ForeColor = UITheme.TextWhite

        dgvUnits.ClearSelection()
        txtNameAr.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Events
    ' ═══════════════════════════════════════════════════════
    Private Sub dgvUnits_SelectionChanged(sender As Object, e As EventArgs) Handles dgvUnits.SelectionChanged
        If _loading Then Return
        If dgvUnits.SelectedRows.Count = 0 Then Return
        LoadIntoForm(CInt(dgvUnits.SelectedRows(0).Cells("UnitOfMeasureID").Value))
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ResetForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtNameAr.Text) Then
            Msg("الاسم العربي مطلوب.", txtNameAr) : Return
        End If
        If String.IsNullOrWhiteSpace(txtNameEn.Text) Then
            Msg("الاسم الإنجليزي مطلوب.", txtNameEn) : Return
        End If

        Dim model As New UnitOfMeasureDto With {
            .UnitOfMeasureID = _currentId,
            .UnitNameAr = txtNameAr.Text.Trim(),
            .UnitNameEn = txtNameEn.Text.Trim()
        }

        Try
            If _currentId = 0 Then
                Dim newId As Integer = _repo.Insert(model)
                MessageBox.Show("تم إضافة الوحدة بنجاح.", "تم",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData(newId) : LoadIntoForm(newId)
            Else
                _repo.Update(model)
                MessageBox.Show("تم تعديل الوحدة بنجاح.", "تم",
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
        If _currentId = 0 Then Msg("اختر وحدة من الجدول أولاً.") : Return

        If MessageBox.Show($"هل أنت متأكد من حذف الوحدة: {txtNameAr.Text}؟",
                           "تأكيد الحذف", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            _repo.Delete(_currentId)
            MessageBox.Show("تم حذف الوحدة بنجاح.", "تم",
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