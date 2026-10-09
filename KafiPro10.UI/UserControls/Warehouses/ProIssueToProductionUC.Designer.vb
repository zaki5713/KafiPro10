Imports System.Drawing
Imports System.Windows.Forms

Partial Class ProIssueToProductionUC
    Inherits UserControl

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents dgvLines As DataGridView

    Friend WithEvents lblWarehouse As Label
    Friend WithEvents lblShift As Label
    Friend WithEvents lblRefNo As Label
    Friend WithEvents lblNotes As Label
    Friend WithEvents lblTotal As Label

    Friend WithEvents cboWarehouse As ComboBox
    Friend WithEvents cboShift As ComboBox
    Friend WithEvents txtRefNo As TextBox
    Friend WithEvents txtNotes As TextBox

    Friend WithEvents btnAddRow As Button
    Friend WithEvents btnRemoveRow As Button
    Friend WithEvents btnIssue As Button
    Friend WithEvents btnCancel As Button

    Private Sub InitializeComponent()
        Me.pnlHeader = New Panel()
        Me.pnlFooter = New Panel()
        Me.dgvLines = New DataGridView()
        Me.cboWarehouse = New ComboBox()
        Me.cboShift = New ComboBox()
        Me.txtRefNo = New TextBox()
        Me.txtNotes = New TextBox()
        Me.lblWarehouse = New Label()
        Me.lblShift = New Label()
        Me.lblRefNo = New Label()
        Me.lblNotes = New Label()
        Me.lblTotal = New Label()
        Me.btnAddRow = New Button()
        Me.btnRemoveRow = New Button()
        Me.btnIssue = New Button()
        Me.btnCancel = New Button()

        ' pnlHeader
        Me.pnlHeader.Dock = DockStyle.Top
        Me.pnlHeader.Height = 110
        Me.pnlHeader.Padding = New Padding(10)
        Me.pnlHeader.BackColor = Color.White

        ' الصف الأول
        Me.lblWarehouse.Text = "المخزن:"
        Me.lblWarehouse.SetBounds(10, 15, 60, 22)
        Me.cboWarehouse.SetBounds(75, 12, 220, 24)
        Me.cboWarehouse.DropDownStyle = ComboBoxStyle.DropDownList

        Me.lblShift.Text = "الوردية:"
        Me.lblShift.SetBounds(310, 15, 60, 22)
        Me.cboShift.SetBounds(375, 12, 180, 24)
        Me.cboShift.DropDownStyle = ComboBoxStyle.DropDownList

        ' الصف الثاني
        Me.lblRefNo.Text = "رقم مرجعي:"
        Me.lblRefNo.SetBounds(10, 55, 70, 22)
        Me.txtRefNo.SetBounds(85, 52, 210, 24)

        Me.lblNotes.Text = "ملاحظات:"
        Me.lblNotes.SetBounds(310, 55, 60, 22)
        Me.txtNotes.SetBounds(375, 52, 400, 24)

        ' dgvLines
        Me.dgvLines.Dock = DockStyle.Fill
        Me.dgvLines.AllowUserToAddRows = False
        Me.dgvLines.AllowUserToDeleteRows = False
        Me.dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvLines.RowHeadersVisible = False
        Me.dgvLines.BackgroundColor = Color.White
        Me.dgvLines.BorderStyle = BorderStyle.None
        Me.dgvLines.EditMode = DataGridViewEditMode.EditOnEnter

        ' pnlFooter
        Me.pnlFooter.Dock = DockStyle.Bottom
        Me.pnlFooter.Height = 56
        Me.pnlFooter.Padding = New Padding(10)
        Me.pnlFooter.BackColor = Color.White

        Me.btnAddRow.Text = "+ سطر"
        Me.btnAddRow.SetBounds(10, 12, 100, 32)
        Me.btnRemoveRow.Text = "− حذف السطر"
        Me.btnRemoveRow.SetBounds(120, 12, 110, 32)
        Me.btnIssue.Text = "ترحيل الصرف"
        Me.btnIssue.SetBounds(240, 12, 140, 32)
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.SetBounds(390, 12, 100, 32)

        Me.lblTotal.SetBounds(510, 18, 600, 22)
        Me.lblTotal.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        Me.pnlFooter.Controls.Add(Me.btnAddRow)
        Me.pnlFooter.Controls.Add(Me.btnRemoveRow)
        Me.pnlFooter.Controls.Add(Me.btnIssue)
        Me.pnlFooter.Controls.Add(Me.btnCancel)
        Me.pnlFooter.Controls.Add(Me.lblTotal)

        Me.pnlHeader.Controls.Add(Me.lblWarehouse)
        Me.pnlHeader.Controls.Add(Me.cboWarehouse)
        Me.pnlHeader.Controls.Add(Me.lblShift)
        Me.pnlHeader.Controls.Add(Me.cboShift)
        Me.pnlHeader.Controls.Add(Me.lblRefNo)
        Me.pnlHeader.Controls.Add(Me.txtRefNo)
        Me.pnlHeader.Controls.Add(Me.lblNotes)
        Me.pnlHeader.Controls.Add(Me.txtNotes)

        Me.Controls.Add(Me.dgvLines)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.pnlHeader)
        Me.Size = New Size(1150, 640)
    End Sub
End Class