Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

Public Class FrmWithdrawalOrderDetails

    Private ReadOnly _svc As New WithdrawalService()
    Private ReadOnly _order As WithdrawalOrderDto

    Private WithEvents TabMain As New TabControl()
    Private WithEvents GridItems As New DataGridView()
    Private WithEvents GridHistory As New DataGridView()
    Private WithEvents BtnClose As New Button()

    Public Sub New(order As WithdrawalOrderDto)
        _order = order
        InitializeForm()
        LoadData()
    End Sub

    Private Sub InitializeForm()
        Me.Text = "أدارة الانتاج - تفاصيل طلب الصرف " & _order.WithdrawalNo
        Me.Size = New Size(1000, 640)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.BackColor = Color.WhiteSmoke

        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top, .Height = 35,
            .BackColor = Color.FromArgb(41, 128, 185)
        }
        pnlHeader.Controls.Add(New Label With {
            .Text = "🔍  تفاصيل الطلب " & _order.WithdrawalNo & "  |  " & _order.WarehouseName,
            .Dock = DockStyle.Fill, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Padding = New Padding(0, 0, 20, 0)
        })

        Dim pnl As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(15), .BackColor = Color.White}

        Dim pnlInfo As New Panel With {
            .Location = New Point(15, 15), .Size = New Size(955, 130),
            .BackColor = Color.FromArgb(248, 249, 250),
            .BorderStyle = BorderStyle.FixedSingle
        }

        AddInfo(pnlInfo, "رقم الطلب:", _order.WithdrawalNo, 20, 15)
        AddInfo(pnlInfo, "التاريخ:", _order.WithdrawalDate.ToString("yyyy-MM-dd"), 480, 15)
        AddInfo(pnlInfo, "المخزن:", _order.WarehouseName, 20, 45)
        AddInfo(pnlInfo, "الوردية:", If(_order.ShiftName, "—"), 480, 45)
        AddInfo(pnlInfo, "الحالة:", _order.StatusNameAr, 20, 75)
        AddInfo(pnlInfo, "عدد الأصناف:", _order.Details.Count.ToString(), 480, 75)
        AddInfo(pnlInfo, "إجمالي المطلوب:", _order.TotalRequestedQty.ToString("N2"), 20, 105)
        AddInfo(pnlInfo, "إجمالي المصروف:", _order.TotalIssuedQty.ToString("N2"), 480, 105)

        pnl.Controls.Add(pnlInfo)

        tabMain.Location = New Point(15, 155)
        tabMain.Size = New Size(955, 390)
        tabMain.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        Dim tabItems As New TabPage("📦 الأصناف") With {
            .BackColor = Color.White,
            .Padding = New Padding(8)
        }

        GridItems.Dock = DockStyle.Fill
        gridItems.BackgroundColor = Color.White
        gridItems.BorderStyle = BorderStyle.None
        gridItems.RightToLeft = RightToLeft.Yes
        gridItems.AllowUserToAddRows = False
        gridItems.ReadOnly = True
        gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        gridItems.ColumnHeadersHeight = 36
        gridItems.RowTemplate.Height = 30
        gridItems.EnableHeadersVisualStyles = False
        gridItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185)
        gridItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        gridItems.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        tabItems.Controls.Add(gridItems)

        Dim tabHistory As New TabPage("📜 رحلة المستند") With {
            .BackColor = Color.White,
            .Padding = New Padding(8)
        }

        GridHistory.Dock = DockStyle.Fill
        gridHistory.BackgroundColor = Color.White
        gridHistory.BorderStyle = BorderStyle.None
        gridHistory.RightToLeft = RightToLeft.Yes
        gridHistory.AllowUserToAddRows = False
        gridHistory.ReadOnly = True
        gridHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gridHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        gridHistory.ColumnHeadersHeight = 36
        gridHistory.RowTemplate.Height = 30
        gridHistory.EnableHeadersVisualStyles = False
        gridHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185)
        gridHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        gridHistory.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        tabHistory.Controls.Add(gridHistory)

        tabMain.TabPages.Add(tabItems)
        tabMain.TabPages.Add(tabHistory)
        pnl.Controls.Add(tabMain)

        btnClose.Text = "❌ إغلاق"
        btnClose.Location = New Point(860, 555)
        btnClose.Size = New Size(110, 40)
        btnClose.BackColor = Color.FromArgb(149, 165, 166)
        btnClose.ForeColor = Color.White
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnClose.Cursor = Cursors.Hand
        btnClose.DialogResult = DialogResult.Cancel
        pnl.Controls.Add(btnClose)

        Me.Controls.Add(pnl)
        Me.Controls.Add(pnlHeader)
    End Sub

    Private Sub AddInfo(parent As Panel, label As String, value As String, x As Integer, y As Integer)
        parent.Controls.Add(New Label With {
            .Text = label, .Location = New Point(x + 250, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(100, 100, 100)
        })
        parent.Controls.Add(New Label With {
            .Text = value, .Location = New Point(x, y), .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Regular)
        })
    End Sub

    Private Sub LoadData()
        Dim dtItems As New DataTable()
        dtItems.Columns.Add("كود الصنف", GetType(String))
        dtItems.Columns.Add("الصنف", GetType(String))
        dtItems.Columns.Add("الوحدة", GetType(String))
        dtItems.Columns.Add("المطلوب", GetType(Decimal))
        dtItems.Columns.Add("المصروف", GetType(Decimal))
        dtItems.Columns.Add("المستلم", GetType(Decimal))

        For Each d In _order.Details
            dtItems.Rows.Add(d.ItemCode, d.ItemName, d.UnitName,
                             d.RequestedQty, d.IssuedQty, d.ReceivedQty)
        Next

        gridItems.DataSource = dtItems

        Dim history = _svc.GetHistory(_order.WithdrawalID)

        Dim dtHist As New DataTable()
        dtHist.Columns.Add("التاريخ", GetType(String))
        dtHist.Columns.Add("الإجراء", GetType(String))
        dtHist.Columns.Add("من حالة", GetType(String))
        dtHist.Columns.Add("إلى حالة", GetType(String))
        dtHist.Columns.Add("بواسطة", GetType(String))
        dtHist.Columns.Add("ملاحظات", GetType(String))

        For Each h In history
            dtHist.Rows.Add(h.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
                            TranslateAction(h.Action),
                            If(h.FromStatusName, "—"),
                            If(h.ToStatusName, "—"),
                            h.UserName,
                            If(h.Notes, ""))
        Next

        gridHistory.DataSource = dtHist
    End Sub

    Private Function TranslateAction(a As String) As String
        Select Case a
            Case "CREATED" : Return "إنشاء"
            Case "UPDATED" : Return "تعديل"
            Case "SUBMITTED" : Return "إرسال"
            Case "ISSUED" : Return "صرف"
            Case "RECEIVED" : Return "استلام"
            Case "APPROVED" : Return "اعتماد"
            Case "REJECTED" : Return "رفض"
            Case "CANCELLED" : Return "إلغاء"
            Case "DELETED" : Return "حذف"
            Case Else : Return a
        End Select
    End Function

End Class