Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Data
Imports KafiPro.UI.Shared.Themes

Public Class ProWarehouseTypesTabUC

    Private ReadOnly _svc As New WarehouseMasterService()

    Private WithEvents toolbar As New Panel()
    Private WithEvents btnRefresh As New Button()

    Private WithEvents grid As New DataGridView()
    Private _types As List(Of WarehouseTypeDto)

    Private Sub ProWarehouseTypesTabUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildUI()
        LoadData()
    End Sub

    Private Sub BuildUI()
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)

        toolbar.Dock = DockStyle.Top
        toolbar.Height = 60
        toolbar.BackColor = Color.FromArgb(245, 247, 250)
        toolbar.Padding = New Padding(8)

        btnRefresh.Text = "🔄 تحديث"
        btnRefresh.Location = New Point(12, 14)
        btnRefresh.Size = New Size(110, 32)
        btnRefresh.BackColor = Color.FromArgb(149, 165, 166)
        btnRefresh.ForeColor = Color.White
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnRefresh.Cursor = Cursors.Hand

        Dim lblInfo As New Label With {
            .Text = "ℹ️ أنواع المخازن ثابتة في النظام (Lookup)",
            .Location = New Point(150, 20),
            .AutoSize = True,
            .ForeColor = Color.FromArgb(100, 100, 100),
            .Font = New Font("Segoe UI", 9, FontStyle.Italic)
        }

        toolbar.Controls.AddRange({btnRefresh, lblInfo})

        grid.Dock = DockStyle.Fill
        grid.BackgroundColor = Color.White
        grid.BorderStyle = BorderStyle.None
        grid.RightToLeft = RightToLeft.Yes
        grid.AllowUserToAddRows = False
        grid.ReadOnly = True
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.ColumnHeadersHeight = 40
        grid.RowTemplate.Height = 30
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = UITheme.Primary
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250)

        Me.Controls.Add(grid)
        Me.Controls.Add(toolbar)
    End Sub

    Public Sub LoadData()
        Try
            _types = _svc.GetWarehouseTypes()

            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("الكود", GetType(String))
            dt.Columns.Add("الاسم (عربي)", GetType(String))
            dt.Columns.Add("الاسم (إنجليزي)", GetType(String))

            For Each t In _types
                dt.Rows.Add(t.WarehouseTypeID, t.TypeCode, t.TypeNameAr, t.TypeNameEn)
            Next

            grid.DataSource = dt
            If grid.Columns.Contains("ID") Then grid.Columns("ID").Visible = False
        Catch ex As Exception
            MessageBox.Show("خطأ في التحميل: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

End Class