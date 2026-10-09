Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class FrmWithdrawalOrderIssue

    Private ReadOnly _svc As New WithdrawalService()
    Private ReadOnly _order As WithdrawalOrderDto
    Private ReadOnly CurrentUserID As Integer = 1

    Private WithEvents Grid As New DataGridView()
    Private WithEvents TxtRejectReason As New TextBox()
    Private WithEvents BtnIssue As New Button()
    Private WithEvents BtnReject As New Button()
    Private WithEvents BtnCancel As New Button()

    Public Sub New(order As WithdrawalOrderDto)
        _order = order
        InitializeForm()
        LoadData()
    End Sub

    Private Sub InitializeForm()
        Me.Text = "صرف الطلب " & _order.WithdrawalNo
        Me.Size = New Size(1100, 720)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.RightToLeft = RightToLeft.Yes
        Me.Font = New Font("Segoe UI", 9.0!)
        Me.BackColor = Color.WhiteSmoke

        Dim pnlHeader As New Panel With {
            .Dock = DockStyle.Top, .Height = 75,
            .BackColor = Color.FromArgb(230, 126, 34)
        }
        pnlHeader.Controls.Add(New Label With {
            .Text = "📦  صرف الطلب " & _order.WithdrawalNo & vbCrLf &
                    "المخزن: " & _order.WarehouseName & "  |  التاريخ: " & _order.WithdrawalDate.ToString("yyyy-MM-dd"),
            .Dock = DockStyle.Fill, .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 20, 0)
        })

        Dim pnl As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(15), .BackColor = Color.White}

        Dim lblHint As New Label With {
            .Text = "✏️ عدّل عمود 'الكمية المصروفة' — الأصناف التي عليها Hold ستُرفض تلقائياً.",
            .Location = New Point(20, 10), .AutoSize = True,
            .ForeColor = Color.FromArgb(100, 100, 100),
            .Font = New Font("Segoe UI", 9, FontStyle.Italic)
        }
        pnl.Controls.Add(lblHint)

        Grid.Location = New Point(20, 40)
        Grid.Size = New Size(990, 420)
        Grid.BackgroundColor = Color.White
        Grid.BorderStyle = BorderStyle.FixedSingle
        Grid.RightToLeft = RightToLeft.Yes
        Grid.AllowUserToAddRows = False
        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Grid.ColumnHeadersHeight = 38
        Grid.RowTemplate.Height = 32
        Grid.EnableHeadersVisualStyles = False
        Grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 126, 34)
        Grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Grid.EditMode = DataGridViewEditMode.EditOnEnter
        pnl.Controls.Add(Grid)

        Dim lblRej As New Label With {
            .Text = "سبب الرفض (لو هيتم رفض الطلب):",
            .Location = New Point(850, 475), .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        }
        pnl.Controls.Add(lblRej)

        TxtRejectReason.Location = New Point(20, 470)
        TxtRejectReason.Size = New Size(815, 30)
        pnl.Controls.Add(TxtRejectReason)

        BtnIssue.Text = "✅ صرف الأصناف"
        BtnIssue.Location = New Point(860, 515)
        BtnIssue.Size = New Size(150, 45)
        StyleBtn(BtnIssue, Color.FromArgb(230, 126, 34))
        pnl.Controls.Add(BtnIssue)

        BtnReject.Text = "❌ رفض الطلب"
        BtnReject.Location = New Point(700, 515)
        BtnReject.Size = New Size(150, 45)
        StyleBtn(BtnReject, Color.FromArgb(192, 57, 43))
        pnl.Controls.Add(BtnReject)

        BtnCancel.Text = "❌ إغلاق"
        BtnCancel.Location = New Point(580, 515)
        BtnCancel.Size = New Size(110, 45)
        StyleBtn(BtnCancel, Color.FromArgb(149, 165, 166))
        BtnCancel.DialogResult = DialogResult.Cancel
        pnl.Controls.Add(BtnCancel)

        Me.Controls.Add(pnl)
        Me.Controls.Add(pnlHeader)
    End Sub

    Private Sub StyleBtn(b As Button, c As Color)
        b.BackColor = c : b.ForeColor = Color.White
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        b.Cursor = Cursors.Hand
    End Sub

    Private Sub LoadData()
        Dim dt As New DataTable()
        dt.Columns.Add("DetailID", GetType(Long))
        dt.Columns.Add("ItemID", GetType(Integer))
        dt.Columns.Add("UnitID", GetType(Integer))
        dt.Columns.Add("كود الصنف", GetType(String))
        dt.Columns.Add("الصنف", GetType(String))
        dt.Columns.Add("الوحدة", GetType(String))
        dt.Columns.Add("المطلوب", GetType(Decimal))
        dt.Columns.Add("المتاح", GetType(Decimal))
        dt.Columns.Add("عليه Hold", GetType(String))
        dt.Columns.Add("الكمية المصروفة", GetType(Decimal))

        For Each d In _order.Details
            Dim avail As Decimal = 0
            Dim held As String = "لا"

            Using conn = DBConnection.GetConnection()
                Using cmd As New SqlCommand(
                    "SELECT ISNULL(SUM(AvailableQty),0), ISNULL(SUM(HeldQty),0) FROM [Inventory].[StockBalance] WHERE WarehouseID=@W AND ItemID=@I", conn)
                    cmd.Parameters.AddWithValue("@W", _order.WarehouseID)
                    cmd.Parameters.AddWithValue("@I", d.ItemID)
                    Using rd = cmd.ExecuteReader()
                        If rd.Read() Then
                            avail = CDec(rd.GetValue(0))
                            If CDec(rd.GetValue(1)) > 0 Then held = "نعم"
                        End If
                    End Using
                End Using
            End Using

            dt.Rows.Add(d.DetailID, d.ItemID, If(d.UnitID, 0), d.ItemCode, d.ItemName, d.UnitName,
                        d.RequestedQty, avail, held, d.RequestedQty)
        Next

        Grid.DataSource = dt
        If Grid.Columns.Contains("DetailID") Then Grid.Columns("DetailID").Visible = False
        If Grid.Columns.Contains("ItemID") Then Grid.Columns("ItemID").Visible = False
        If Grid.Columns.Contains("UnitID") Then Grid.Columns("UnitID").Visible = False

        Grid.Columns("كود الصنف").ReadOnly = True
        Grid.Columns("الصنف").ReadOnly = True
        Grid.Columns("الوحدة").ReadOnly = True
        Grid.Columns("المطلوب").ReadOnly = True
        Grid.Columns("المتاح").ReadOnly = True
        Grid.Columns("عليه Hold").ReadOnly = True
        Grid.Columns("الكمية المصروفة").ReadOnly = False

        For Each row As DataGridViewRow In Grid.Rows
            If row.Cells("عليه Hold").Value?.ToString() = "نعم" Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235)
                row.Cells("الكمية المصروفة").Value = 0
                row.Cells("الكمية المصروفة").ReadOnly = True
            End If
        Next
    End Sub

    Private Sub BtnIssue_Click(sender As Object, e As EventArgs) Handles BtnIssue.Click
        Try
            Dim dto As New IssueWithdrawalDto With {
                .WithdrawalID = _order.WithdrawalID,
                .IssuedBy = CurrentUserID
            }

            For Each row As DataGridViewRow In Grid.Rows
                Dim issuedQty As Decimal
                If Not Decimal.TryParse(row.Cells("الكمية المصروفة").Value?.ToString(), issuedQty) Then issuedQty = 0
                If issuedQty < 0 Then
                    MessageBox.Show("الكمية المصروفة لا يمكن أن تكون سالبة.", "تنبيه",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim requested = CDec(row.Cells("المطلوب").Value)
                If issuedQty > requested Then
                    MessageBox.Show("الكمية المصروفة أكبر من المطلوبة في الصنف: " & row.Cells("الصنف").Value.ToString(),
                                    "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                dto.Details.Add(New IssueWithdrawalDetailDto With {
                    .ItemID = CInt(row.Cells("ItemID").Value),
                    .UnitID = CInt(row.Cells("UnitID").Value),
                    .RequestedQty = requested,
                    .IssuedQty = issuedQty
                })
            Next

            If Not dto.Details.Exists(Function(x) x.IssuedQty > 0) Then
                MessageBox.Show("أدخل كمية مصروفة في صنف واحد على الأقل.", "تنبيه",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            _svc.Issue(dto)
            MessageBox.Show("✅ تم الصرف بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnReject_Click(sender As Object, e As EventArgs) Handles BtnReject.Click
        If String.IsNullOrWhiteSpace(TxtRejectReason.Text) Then
            MessageBox.Show("أدخل سبب الرفض.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtRejectReason.Focus() : Return
        End If

        Try
            _svc.Reject(_order.WithdrawalID, TxtRejectReason.Text.Trim(), CurrentUserID)
            MessageBox.Show("✅ تم رفض الطلب.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class