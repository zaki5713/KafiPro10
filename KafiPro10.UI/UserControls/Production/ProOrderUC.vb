Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Public Class ProOrderUC

    Private _loaded As Boolean = False
    Private _repo As SalesShortageRepository
    Private _service As SalesShortageService
    Private _list As New List(Of SalesShortageDTO)()

    Private _grid As DataGridView
    Private _txtSearch As TextBox
    Private _cboStatus As ComboBox
    Private _cboSource As ComboBox
    Private _lblCount As Label
    Private _lblTotal As Label

    Private Sub ProOrderUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _loaded Then Return
        _loaded = True
        _repo = New SalesShortageRepository()
        _service = New SalesShortageService(_repo)
        BuildUI()
        LoadData()
    End Sub

    ' ---------------- UI ----------------
    Private Sub BuildUI()
        Me.BackColor = Color.White
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.Dock = DockStyle.Fill

        ' ============ Toolbar ============
        Dim pnlTop As New Panel With {.Dock = DockStyle.Top, .Height = 46, .BackColor = UITheme.ButtonNormal}

        Dim flow As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.RightToLeft,
            .WrapContents = False,
            .Padding = New Padding(6, 4, 6, 4)
        }
        pnlTop.Controls.Add(flow)

        flow.Controls.Add(New Label With {
            .Text = "نواقص المبيعات",
            .Font = UITheme.FontBold,
            .ForeColor = UITheme.TextPrimary,
            .AutoSize = False,
            .Width = 160,
            .Height = 34,
            .TextAlign = ContentAlignment.MiddleRight,
            .Margin = New Padding(4, 0, 12, 0)
        })

        Dim bAdd = MakeButton("إضافة")
        Dim bEdit = MakeButton("تعديل")
        Dim bDel = MakeButton("حذف")
        Dim bGen = MakeButton("توليد من المخزون")
        Dim bStart = MakeButton("بدء إنتاج")
        Dim bClose = MakeButton("إغلاق")
        Dim bExp = MakeButton("تصدير Excel")
        Dim bRef = MakeButton("تحديث")

        AddHandler bAdd.Click, Sub() OnAdd()
        AddHandler bEdit.Click, Sub() OnEdit()
        AddHandler bDel.Click, Sub() OnDelete()
        AddHandler bGen.Click, Sub() OnGenerateFromStock()
        AddHandler bStart.Click, Sub() OnStartProduction()
        AddHandler bClose.Click, Sub() OnCloseShortage()
        AddHandler bExp.Click, Sub() OnExport()
        AddHandler bRef.Click, Sub() LoadData()

        flow.Controls.Add(bAdd) : flow.Controls.Add(bEdit) : flow.Controls.Add(bDel)
        flow.Controls.Add(bGen) : flow.Controls.Add(bStart) : flow.Controls.Add(bClose)
        flow.Controls.Add(bExp) : flow.Controls.Add(bRef)

        ' ============ Filters ============
        Dim pnlFilter As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 46,
            .BackColor = Color.FromArgb(248, 248, 248)
        }

        Dim fflow As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.RightToLeft,
            .WrapContents = False,
            .Padding = New Padding(6, 6, 6, 6)
        }
        pnlFilter.Controls.Add(fflow)

        fflow.Controls.Add(New Label With {.Text = "بحث:", .AutoSize = True, .Margin = New Padding(8, 8, 4, 0)})
        _txtSearch = New TextBox With {.Width = 220, .Margin = New Padding(4, 3, 4, 3)}
        AddHandler _txtSearch.KeyDown, Sub(s, ev)
                                           If ev.KeyCode = Keys.Enter Then LoadData()
                                       End Sub
        fflow.Controls.Add(_txtSearch)

        fflow.Controls.Add(New Label With {.Text = "الحالة:", .AutoSize = True, .Margin = New Padding(12, 8, 2, 0)})
        _cboStatus = New ComboBox With {.Width = 130, .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(4, 3, 4, 3)}
        _cboStatus.Items.AddRange({"الكل", "مفتوح", "تحت الإنتاج", "مغلق", "ملغي"})
        _cboStatus.SelectedIndex = 1
        AddHandler _cboStatus.SelectedIndexChanged, Sub() LoadData()
        fflow.Controls.Add(_cboStatus)

        fflow.Controls.Add(New Label With {.Text = "المصدر:", .AutoSize = True, .Margin = New Padding(12, 8, 2, 0)})
        _cboSource = New ComboBox With {.Width = 140, .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(4, 3, 4, 3)}
        _cboSource.Items.AddRange({"الكل", "يدوي", "آلي (مخزون)", "مبيعات"})
        _cboSource.SelectedIndex = 0
        AddHandler _cboSource.SelectedIndexChanged, Sub() LoadData()
        fflow.Controls.Add(_cboSource)

        ' ============ Bottom ============
        Dim pnlBottom As New Panel With {.Dock = DockStyle.Bottom, .Height = 30, .BackColor = Color.FromArgb(248, 248, 248)}

        _lblCount = New Label With {
            .AutoSize = True,
            .ForeColor = UITheme.TextPrimary,
            .Location = New Point(600, 7),
            .Text = "عدد السجلات: 0"
        }
        _lblTotal = New Label With {
            .AutoSize = True,
            .ForeColor = Color.Firebrick,
            .Font = UITheme.FontBold,
            .Location = New Point(300, 7),
            .Text = "إجمالي النواقص: 0"
        }
        AddHandler pnlBottom.Resize, Sub()
                                         _lblTotal.Left = Math.Max(10, pnlBottom.Width - 400)
                                         _lblCount.Left = Math.Max(10, pnlBottom.Width - 180)
                                     End Sub
        pnlBottom.Controls.Add(_lblCount)
        pnlBottom.Controls.Add(_lblTotal)

        ' ============ Grid ============
        _grid = New DataGridView With {
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
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            .ColumnHeadersHeight = 38,
            .RowTemplate = New DataGridViewRow With {.Height = 32},
            .AllowUserToResizeRows = False,
            .AllowUserToResizeColumns = True,
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
            .GridColor = Color.FromArgb(230, 230, 230)
        }
        _grid.ColumnHeadersDefaultCellStyle.BackColor = UITheme.ButtonNormal
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = UITheme.TextPrimary
        _grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        _grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        _grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
        _grid.DefaultCellStyle.Font = New Font("Segoe UI", 9.0!)
        _grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 250)
        _grid.DefaultCellStyle.SelectionForeColor = Color.Black
        _grid.DefaultCellStyle.Padding = New Padding(4, 0, 4, 0)
        _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252)

        ' ✅ الأعمدة BEFORE الإضافة
        AddColumns()

        ' ✅ ترتيب الـ Docking — Fill الأول، بعدين Bottom، بعدين Top
        Me.Controls.Add(_grid)
        Me.Controls.Add(pnlBottom)
        Me.Controls.Add(pnlFilter)
        Me.Controls.Add(pnlTop)
    End Sub

    Private Function MakeButton(text As String) As Button
        Dim b As New Button With {
            .Text = text,
            .BackColor = UITheme.ButtonNormal,
            .ForeColor = UITheme.TextPrimary,
            .Font = UITheme.FontBold,
            .FlatStyle = FlatStyle.Flat,
            .Width = 115,
            .Height = 34,
            .Margin = New Padding(2, 0, 2, 0),
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False
        }
        b.FlatAppearance.BorderSize = 0
        b.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
        Return b
    End Function

    Private Sub AddColumns()
        _grid.Columns.Clear()

        Dim c As DataGridViewTextBoxColumn

        c = New DataGridViewTextBoxColumn With {.Name = "ShortageID", .Visible = False}
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "ItemCode", .HeaderText = "كود الصنف", .Width = 110}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "ItemName", .HeaderText = "اسم الصنف", .Width = 240}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "UnitName", .HeaderText = "الوحدة", .Width = 70}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "WarehouseName", .HeaderText = "المخزن", .Width = 130}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "RequiredQty", .HeaderText = "المطلوب", .Width = 95}
        c.DefaultCellStyle = NumCell()
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "AvailableQty", .HeaderText = "المتاح", .Width = 95}
        c.DefaultCellStyle = NumCell()
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "ShortageQty", .HeaderText = "النقص", .Width = 95}
        c.DefaultCellStyle = ShortCell()
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "StatusText", .HeaderText = "الحالة", .Width = 100}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "SourceText", .HeaderText = "المصدر", .Width = 120}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "RequiredDate", .HeaderText = "تاريخ الاحتياج", .Width = 110}
        c.DefaultCellStyle = DateCell()
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "ReferenceNo", .HeaderText = "المرجع", .Width = 120}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        _grid.Columns.Add(c)

        c = New DataGridViewTextBoxColumn With {.Name = "Notes", .HeaderText = "ملاحظات", .Width = 220}
        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        _grid.Columns.Add(c)
    End Sub

    Private Function NumCell() As DataGridViewCellStyle
        Return New DataGridViewCellStyle With {.Format = "N2", .Alignment = DataGridViewContentAlignment.MiddleCenter}
    End Function

    Private Function ShortCell() As DataGridViewCellStyle
        Return New DataGridViewCellStyle With {
            .Format = "N2",
            .Alignment = DataGridViewContentAlignment.MiddleCenter,
            .ForeColor = Color.Firebrick,
            .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        }
    End Function

    Private Function DateCell() As DataGridViewCellStyle
        Return New DataGridViewCellStyle With {.Format = "yyyy-MM-dd", .Alignment = DataGridViewContentAlignment.MiddleCenter}
    End Function

    ' ---------------- Data ----------------
    Private Sub LoadData()
        Try
            Dim status As Integer = _cboStatus.SelectedIndex
            Dim source As Integer = _cboSource.SelectedIndex
            _list = _repo.GetAll(status, source, _txtSearch.Text.Trim())
            BindGrid()
        Catch ex As Exception
            MessageBox.Show("خطأ في التحميل: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindGrid()
        ' ✅ حماية إضافية
        If _grid.Columns.Count = 0 Then AddColumns()

        _grid.Rows.Clear()
        Dim total As Decimal = 0D
        For Each d In _list
            Dim i As Integer = _grid.Rows.Add()
            Dim r As DataGridViewRow = _grid.Rows(i)
            r.Cells("ShortageID").Value = d.ShortageID
            r.Cells("ItemCode").Value = d.ItemCode
            r.Cells("ItemName").Value = d.ItemName
            r.Cells("UnitName").Value = d.UnitName
            r.Cells("WarehouseName").Value = d.WarehouseName
            r.Cells("RequiredQty").Value = d.RequiredQty
            r.Cells("AvailableQty").Value = d.AvailableQty
            r.Cells("ShortageQty").Value = d.ShortageQty
            r.Cells("StatusText").Value = d.StatusText
            r.Cells("SourceText").Value = d.SourceText
            r.Cells("RequiredDate").Value = If(d.RequiredDate.HasValue, CObj(d.RequiredDate.Value), DBNull.Value)
            r.Cells("ReferenceNo").Value = d.ReferenceNo
            r.Cells("Notes").Value = d.Notes
            total += d.ShortageQty
        Next
        _lblCount.Text = "عدد السجلات: " & _list.Count
        _lblTotal.Text = "إجمالي النواقص: " & total.ToString("N2")
    End Sub

    Private Function SelectedId() As Integer
        If _grid.CurrentRow Is Nothing Then Return 0
        Dim v = _grid.CurrentRow.Cells("ShortageID").Value
        If v Is Nothing OrElse v Is DBNull.Value Then Return 0
        Return Convert.ToInt32(v)
    End Function

    Private Function SelectedDto() As SalesShortageDTO
        Dim id = SelectedId()
        If id = 0 Then Return Nothing
        Return _list.Find(Function(x) x.ShortageID = id)
    End Function

    ' ---------------- Actions ----------------
    Private Sub OnAdd()
        Using f As New FrmSalesShortageEdit(Nothing)
            If f.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub OnEdit()
        Dim d = SelectedDto()
        If d Is Nothing Then
            MessageBox.Show("اختر سجلاً أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        Using f As New FrmSalesShortageEdit(d)
            If f.ShowDialog(Me) = DialogResult.OK Then LoadData()
        End Using
    End Sub

    Private Sub OnDelete()
        Dim id = SelectedId()
        If id = 0 Then Return
        If MessageBox.Show("تأكيد حذف السجل؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            _repo.Delete(id)
            LoadData()
        Catch ex As Exception
            MessageBox.Show("خطأ في الحذف: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnGenerateFromStock()
        If MessageBox.Show("توليد نواقص آلياً من الأصناف اللي متاحها أقل من حد الطلب (Reorder Point)؟",
                           "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            Dim n = _service.GenerateFromStock(1)
            MessageBox.Show("تم إضافة " & n & " صنف جديد", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnStartProduction()
        Dim id = SelectedId()
        If id = 0 Then Return
        Try
            _repo.SetStatus(id, 2, 1)
            LoadData()
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnCloseShortage()
        Dim id = SelectedId()
        If id = 0 Then Return
        If MessageBox.Show("إغلاق السجل كـ 'مغلق'؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            _repo.SetStatus(id, 3, 1)
            LoadData()
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnExport()
        Try
            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel CSV (*.csv)|*.csv"
                sfd.FileName = "SalesShortages_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"
                If sfd.ShowDialog() <> DialogResult.OK Then Return
                ExportCsv(sfd.FileName)
                MessageBox.Show("تم التصدير:" & vbCrLf & sfd.FileName, "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("خطأ في التصدير: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportCsv(path As String)
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("كود الصنف,اسم الصنف,الوحدة,المخزن,المطلوب,المتاح,النقص,الحالة,المصدر,تاريخ الاحتياج,المرجع,ملاحظات")
        For Each d In _list
            sb.AppendLine(String.Join(",", {
                Csv(d.ItemCode), Csv(d.ItemName), Csv(d.UnitName), Csv(d.WarehouseName),
                d.RequiredQty.ToString("N2"), d.AvailableQty.ToString("N2"), d.ShortageQty.ToString("N2"),
                Csv(d.StatusText), Csv(d.SourceText),
                If(d.RequiredDate.HasValue, d.RequiredDate.Value.ToString("yyyy-MM-dd"), ""),
                Csv(d.ReferenceNo), Csv(d.Notes)
            }))
        Next
        System.IO.File.WriteAllText(path, sb.ToString(), System.Text.Encoding.UTF8)
    End Sub

    Private Function Csv(s As String) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If s.Contains(",") OrElse s.Contains("""") OrElse s.Contains(vbCr) OrElse s.Contains(vbLf) Then
            Return """" & s.Replace("""", """""") & """"
        End If
        Return s
    End Function

End Class