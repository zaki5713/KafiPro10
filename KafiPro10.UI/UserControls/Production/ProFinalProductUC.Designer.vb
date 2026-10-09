Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProFinalProductUC
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.btnCancelTransfer = New System.Windows.Forms.Button()
        Me.tabAvailable = New System.Windows.Forms.TabPage()
        Me.tabDocuments = New System.Windows.Forms.TabPage()

        ' ═══════════ التاب 1: الإنتاج المتاح ═══════════
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblDocNo = New System.Windows.Forms.Label()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblShift = New System.Windows.Forms.Label()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.lblLine = New System.Windows.Forms.Label()
        Me.cboLine = New System.Windows.Forms.ComboBox()
        Me.lblSupervisor = New System.Windows.Forms.Label()
        Me.cboSupervisor = New System.Windows.Forms.ComboBox()
        Me.lblStorekeeper = New System.Windows.Forms.Label()
        Me.cboStorekeeper = New System.Windows.Forms.ComboBox()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()

        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnClearSearch = New System.Windows.Forms.Button()
        Me.lblSelectedCount = New System.Windows.Forms.Label()

        Me.dgvAvailable = New System.Windows.Forms.DataGridView()

        Me.pnlConvert = New System.Windows.Forms.Panel()
        Me.btnConvert = New System.Windows.Forms.Button()
        Me.lblConvertHint = New System.Windows.Forms.Label()

        ' ═══════════ التاب 2: المستندات المحوّلة ═══════════
        Me.pnlDocSearch = New System.Windows.Forms.Panel()
        Me.lblDocFrom = New System.Windows.Forms.Label()
        Me.dtpDocFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblDocTo = New System.Windows.Forms.Label()
        Me.dtpDocTo = New System.Windows.Forms.DateTimePicker()
        Me.btnDocSearch = New System.Windows.Forms.Button()
        Me.btnDocClear = New System.Windows.Forms.Button()
        Me.lblDocCount = New System.Windows.Forms.Label()

        Me.splitDocs = New System.Windows.Forms.SplitContainer()

        Me.pnlDocActions = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDetailTitle = New System.Windows.Forms.Label()
        Me.btnApprove = New System.Windows.Forms.Button()
        Me.btnReceive = New System.Windows.Forms.Button()
        Me.btnRefreshDoc = New System.Windows.Forms.Button()

        Me.dgvTransfers = New System.Windows.Forms.DataGridView()
        Me.dgvTransferDetails = New System.Windows.Forms.DataGridView()

        Me.tabMain.SuspendLayout()
        Me.tabAvailable.SuspendLayout()
        Me.tabDocuments.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlConvert.SuspendLayout()
        Me.pnlDocSearch.SuspendLayout()
        Me.pnlDocActions.SuspendLayout()
        CType(Me.splitDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitDocs.Panel1.SuspendLayout()
        Me.splitDocs.Panel2.SuspendLayout()
        Me.splitDocs.SuspendLayout()
        CType(Me.dgvAvailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTransferDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' ═══════════════════════════════════════════════════════
        '  TabControl
        ' ═══════════════════════════════════════════════════════
        Me.tabMain.Controls.Add(Me.tabAvailable)
        Me.tabMain.Controls.Add(Me.tabDocuments)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.tabMain.Location = New System.Drawing.Point(0, 0)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tabMain.RightToLeftLayout = True
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(800, 600)
        Me.tabMain.TabIndex = 0

        ' ═══════════════════════════════════════════════════════
        '  التاب 1: الإنتاج المتاح للتحويل
        ' ═══════════════════════════════════════════════════════
        Me.tabAvailable.BackColor = UITheme.Background
        Me.tabAvailable.Controls.Add(Me.dgvAvailable)
        Me.tabAvailable.Controls.Add(Me.pnlConvert)
        Me.tabAvailable.Controls.Add(Me.pnlSearch)
        Me.tabAvailable.Controls.Add(Me.pnlHeader)
        Me.tabAvailable.Location = New System.Drawing.Point(4, 30)
        Me.tabAvailable.Name = "tabAvailable"
        Me.tabAvailable.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAvailable.Size = New System.Drawing.Size(792, 566)
        Me.tabAvailable.TabIndex = 0
        Me.tabAvailable.Text = "📋  الإنتاج المتاح للتحويل"

        ' ── لوحة الرأس (بيانات المستند) ──
        Me.pnlHeader.BackColor = UITheme.Surface
        Me.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(3, 3)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(786, 110)

        SetupLabel(Me.lblDocNo, "رقم المستند", 671, 8, 110)
        SetupTextBox(Me.txtDocNo, 671, 26, 110, True, False)

        SetupLabel(Me.lblDate, "التاريخ *", 546, 8, 120)
        SetupDateTime(Me.dtpDate, 546, 26, 120)

        SetupLabel(Me.lblShift, "الوردية *", 421, 8, 120)
        SetupComboBox(Me.cboShift, 421, 26, 120)

        SetupLabel(Me.lblLine, "خط الإنتاج *", 291, 8, 125)
        SetupComboBox(Me.cboLine, 291, 26, 125)

        SetupLabel(Me.lblSupervisor, "المشرف *", 161, 8, 125)
        SetupComboBox(Me.cboSupervisor, 161, 26, 125)

        SetupLabel(Me.lblStorekeeper, "أمين المخزن *", 31, 8, 125)
        SetupComboBox(Me.cboStorekeeper, 31, 26, 125)

        SetupLabel(Me.lblNotes, "ملاحظات", 656, 60, 125)
        SetupTextBox(Me.txtNotes, 421, 58, 360)

        Me.pnlHeader.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblDocNo, Me.txtDocNo,
            Me.lblDate, Me.dtpDate,
            Me.lblShift, Me.cboShift,
            Me.lblLine, Me.cboLine,
            Me.lblSupervisor, Me.cboSupervisor,
            Me.lblStorekeeper, Me.cboStorekeeper,
            Me.lblNotes, Me.txtNotes
        })

        ' ── شريط البحث ──
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(3, 113)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(786, 50)

        SetupLabel(Me.lblFrom, "من:", 706, 14, 40)
        SetupDateTime(Me.dtpFrom, 596, 12, 110)

        SetupLabel(Me.lblTo, "إلى:", 536, 14, 40)
        SetupDateTime(Me.dtpTo, 426, 12, 110)

        SetupButton(Me.btnSearch, UITheme.Primary, "🔍  بحث", 341, 12, 80, 26, 0)
        SetupButton(Me.btnClearSearch, UITheme.TextMuted, "✖  مسح", 256, 12, 80, 26, 1)

        Me.lblSelectedCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedCount.AutoSize = False
        Me.lblSelectedCount.Font = UITheme.FontBold
        Me.lblSelectedCount.ForeColor = UITheme.Success
        Me.lblSelectedCount.Location = New System.Drawing.Point(10, 14)
        Me.lblSelectedCount.Name = "lblSelectedCount"
        Me.lblSelectedCount.Size = New System.Drawing.Size(240, 22)
        Me.lblSelectedCount.Text = "✅  المحدد: 0 / الإجمالي: 0"
        Me.lblSelectedCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.pnlSearch.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblFrom, Me.dtpFrom,
            Me.lblTo, Me.dtpTo,
            Me.btnSearch, Me.btnClearSearch,
            Me.lblSelectedCount
        })

        ' ── الجدول ──
        Me.dgvAvailable.AllowUserToAddRows = False
        Me.dgvAvailable.AllowUserToDeleteRows = False
        Me.dgvAvailable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAvailable.BackgroundColor = UITheme.Surface
        Me.dgvAvailable.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvAvailable.ColumnHeadersHeight = 40
        Me.dgvAvailable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAvailable.Font = UITheme.FontNormal
        Me.dgvAvailable.Location = New System.Drawing.Point(3, 163)
        Me.dgvAvailable.MultiSelect = False
        Me.dgvAvailable.Name = "dgvAvailable"
        Me.dgvAvailable.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvAvailable.RowHeadersVisible = False
        Me.dgvAvailable.RowTemplate.Height = 32
        Me.dgvAvailable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAvailable.Size = New System.Drawing.Size(786, 334)

        ' ── لوحة زر التحويل ──
        Me.pnlConvert.BackColor = UITheme.Surface
        Me.pnlConvert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlConvert.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlConvert.Location = New System.Drawing.Point(3, 497)
        Me.pnlConvert.Name = "pnlConvert"
        Me.pnlConvert.Size = New System.Drawing.Size(786, 66)

        SetupButton(Me.btnConvert, UITheme.Success, "✅  تحويل المختار إلى المخزن", 15, 14, 280, 38, 0)

        Me.lblConvertHint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConvertHint.AutoSize = False
        Me.lblConvertHint.Font = UITheme.FontNormal
        Me.lblConvertHint.ForeColor = UITheme.TextSecondary
        Me.lblConvertHint.Location = New System.Drawing.Point(310, 22)
        Me.lblConvertHint.Name = "lblConvertHint"
        Me.lblConvertHint.Size = New System.Drawing.Size(460, 22)
        Me.lblConvertHint.Text = "💡 حدد السجلات بعلامة ☑ ثم اضغط الزر — سيتم إنشاء مستند واحد يضم كل السجلات المختارة"
        Me.lblConvertHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight

        Me.pnlConvert.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.btnConvert, Me.lblConvertHint
        })

        ' ═══════════════════════════════════════════════════════
        '  التاب 2: المستندات المحوّلة
        ' ═══════════════════════════════════════════════════════
        Me.tabDocuments.BackColor = UITheme.Background
        Me.tabDocuments.Controls.Add(Me.splitDocs)
        Me.tabDocuments.Controls.Add(Me.pnlDocSearch)
        Me.tabDocuments.Location = New System.Drawing.Point(4, 30)
        Me.tabDocuments.Name = "tabDocuments"
        Me.tabDocuments.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDocuments.Size = New System.Drawing.Size(792, 566)
        Me.tabDocuments.TabIndex = 1
        Me.tabDocuments.Text = "📄  المستندات المحوّلة"

        ' ── بحث المستندات ──
        Me.pnlDocSearch.BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
        Me.pnlDocSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDocSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDocSearch.Location = New System.Drawing.Point(3, 3)
        Me.pnlDocSearch.Name = "pnlDocSearch"
        Me.pnlDocSearch.Size = New System.Drawing.Size(786, 50)

        SetupLabel(Me.lblDocFrom, "من:", 706, 14, 40)
        SetupDateTime(Me.dtpDocFrom, 596, 12, 110)

        SetupLabel(Me.lblDocTo, "إلى:", 536, 14, 40)
        SetupDateTime(Me.dtpDocTo, 426, 12, 110)

        SetupButton(Me.btnDocSearch, UITheme.Primary, "🔍  بحث", 341, 12, 80, 26, 0)
        SetupButton(Me.btnDocClear, UITheme.TextMuted, "✖  مسح", 256, 12, 80, 26, 1)

        Me.lblDocCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDocCount.AutoSize = False
        Me.lblDocCount.Font = UITheme.FontBold
        Me.lblDocCount.ForeColor = UITheme.Primary
        Me.lblDocCount.Location = New System.Drawing.Point(10, 14)
        Me.lblDocCount.Name = "lblDocCount"
        Me.lblDocCount.Size = New System.Drawing.Size(240, 22)
        Me.lblDocCount.Text = "📋  المستندات: 0"
        Me.lblDocCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.pnlDocSearch.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblDocFrom, Me.dtpDocFrom,
            Me.lblDocTo, Me.dtpDocTo,
            Me.btnDocSearch, Me.btnDocClear,
            Me.lblDocCount
        })

        ' ── SplitContainer: مستندات فوق، سطور تحت ──
        Me.splitDocs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitDocs.Location = New System.Drawing.Point(3, 53)
        Me.splitDocs.Name = "splitDocs"
        Me.splitDocs.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.splitDocs.Size = New System.Drawing.Size(786, 510)
        Me.splitDocs.SplitterDistance = 180
        Me.splitDocs.SplitterWidth = 5
        Me.splitDocs.Panel1MinSize = 100
        Me.splitDocs.Panel2MinSize = 150

        ' Panel1 — جدول المستندات
        Me.splitDocs.Panel1.BackColor = UITheme.Background

        Me.dgvTransfers.AllowUserToAddRows = False
        Me.dgvTransfers.AllowUserToDeleteRows = False
        Me.dgvTransfers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTransfers.BackgroundColor = UITheme.Surface
        Me.dgvTransfers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTransfers.ColumnHeadersHeight = 38
        Me.dgvTransfers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTransfers.Font = UITheme.FontNormal
        Me.dgvTransfers.MultiSelect = False
        Me.dgvTransfers.Name = "dgvTransfers"
        Me.dgvTransfers.ReadOnly = True
        Me.dgvTransfers.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvTransfers.RowHeadersVisible = False
        Me.dgvTransfers.RowTemplate.Height = 30
        Me.dgvTransfers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect

        Me.splitDocs.Panel1.Controls.Add(Me.dgvTransfers)

        ' Panel2 — شريط الإجراءات + جدول السطور
        Me.splitDocs.Panel2.BackColor = UITheme.Background

        Me.pnlDocActions.BackColor = UITheme.ButtonNormal
        Me.pnlDocActions.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDocActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlDocActions.Location = New System.Drawing.Point(0, 0)
        Me.pnlDocActions.Name = "pnlDocActions"
        Me.pnlDocActions.Padding = New System.Windows.Forms.Padding(6, 4, 6, 4)
        Me.pnlDocActions.Size = New System.Drawing.Size(786, 40)

        Me.lblDetailTitle.AutoSize = False
        Me.lblDetailTitle.Font = UITheme.FontBold
        Me.lblDetailTitle.ForeColor = UITheme.Primary
        Me.lblDetailTitle.Location = New System.Drawing.Point(530, 4)
        Me.lblDetailTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDetailTitle.Name = "lblDetailTitle"
        Me.lblDetailTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblDetailTitle.Size = New System.Drawing.Size(250, 30)
        Me.lblDetailTitle.Text = "📦  سطور المستند: (اختر مستند)"
        Me.lblDetailTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight

        ' ✅ زر الاعتماد
        SetupButton(Me.btnApprove, UITheme.Primary, "✅  اعتماد", 425, 5, 100, 30, 0)

        ' 📥 زر الاستلام
        SetupButton(Me.btnReceive, UITheme.Success, "📥  استلام", 320, 5, 100, 30, 1)

        ' ❌ زر إلغاء التحويل
        SetupButton(Me.btnCancelTransfer, UITheme.Danger, "❌  إلغاء التحويل", 215, 5, 100, 30, 2)

        ' 🔄 زر التحديث
        SetupButton(Me.btnRefreshDoc, UITheme.TextMuted, "🔄  تحديث", 125, 5, 85, 30, 3)
        Me.pnlDocActions.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblDetailTitle,
            Me.btnApprove,
            Me.btnReceive,
            Me.btnCancelTransfer,
            Me.btnRefreshDoc
        })

        ' جدول السطور
        Me.dgvTransferDetails.AllowUserToAddRows = False
        Me.dgvTransferDetails.AllowUserToDeleteRows = False
        Me.dgvTransferDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTransferDetails.BackgroundColor = UITheme.Surface
        Me.dgvTransferDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTransferDetails.ColumnHeadersHeight = 34
        Me.dgvTransferDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTransferDetails.Font = UITheme.FontNormal
        Me.dgvTransferDetails.MultiSelect = False
        Me.dgvTransferDetails.Name = "dgvTransferDetails"
        Me.dgvTransferDetails.ReadOnly = True
        Me.dgvTransferDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvTransferDetails.RowHeadersVisible = False
        Me.dgvTransferDetails.RowTemplate.Height = 28
        Me.dgvTransferDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect

        Me.splitDocs.Panel2.Controls.Add(Me.dgvTransferDetails)
        Me.splitDocs.Panel2.Controls.Add(Me.pnlDocActions)

        ' ═══════════════════════════════════════════════════════
        '  UserControl
        ' ═══════════════════════════════════════════════════════
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = UITheme.Background
        Me.Controls.Add(Me.tabMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "ProFinalProductUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(800, 600)

        Me.tabMain.ResumeLayout(False)
        Me.tabAvailable.ResumeLayout(False)
        Me.tabDocuments.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlConvert.ResumeLayout(False)
        Me.pnlDocSearch.ResumeLayout(False)
        Me.pnlDocActions.ResumeLayout(False)
        Me.splitDocs.Panel1.ResumeLayout(False)
        Me.splitDocs.Panel2.ResumeLayout(False)
        CType(Me.splitDocs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitDocs.ResumeLayout(False)
        CType(Me.dgvAvailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTransferDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  Helpers
    ' ═══════════════════════════════════════════════════════
    Private Sub SetupLabel(lbl As Label, text As String, x As Integer, y As Integer, w As Integer)
        lbl.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        lbl.AutoSize = False
        lbl.Font = UITheme.FontBold
        lbl.ForeColor = UITheme.TextPrimary
        lbl.Location = New Point(x, y)
        lbl.Size = New Size(w, 16)
        lbl.RightToLeft = RightToLeft.Yes
        lbl.Text = text
        lbl.TextAlign = ContentAlignment.MiddleRight
    End Sub

    Private Sub SetupTextBox(txt As TextBox, x As Integer, y As Integer, w As Integer,
                             Optional isReadOnly As Boolean = False,
                             Optional centered As Boolean = False)
        txt.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        txt.BackColor = If(isReadOnly, Color.FromArgb(240, 240, 240), UITheme.Surface)
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.Font = UITheme.FontNormal
        txt.Location = New Point(x, y)
        txt.Size = New Size(w, 24)
        txt.ReadOnly = isReadOnly
        If centered Then
            txt.RightToLeft = RightToLeft.No
            txt.TextAlign = HorizontalAlignment.Center
        Else
            txt.RightToLeft = RightToLeft.Yes
            txt.TextAlign = HorizontalAlignment.Right
        End If
        If isReadOnly Then txt.TabStop = False
    End Sub

    Private Sub SetupComboBox(cbo As ComboBox, x As Integer, y As Integer, w As Integer)
        cbo.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        cbo.BackColor = UITheme.Surface
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        cbo.FlatStyle = FlatStyle.Standard
        cbo.Font = UITheme.FontNormal
        cbo.FormattingEnabled = True
        cbo.Location = New Point(x, y)
        cbo.Size = New Size(w, 24)
        cbo.RightToLeft = RightToLeft.Yes
    End Sub

    Private Sub SetupDateTime(dtp As DateTimePicker, x As Integer, y As Integer, w As Integer)
        dtp.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        dtp.Font = UITheme.FontNormal
        dtp.Format = DateTimePickerFormat.Short
        dtp.Location = New Point(x, y)
        dtp.Size = New Size(w, 24)
        dtp.RightToLeft = RightToLeft.Yes
        dtp.RightToLeftLayout = True
    End Sub

    Private Sub SetupButton(btn As Button, bgColor As Color, text As String,
                            x As Integer, y As Integer, w As Integer, h As Integer, tabIdx As Integer)
        btn.BackColor = bgColor
        btn.Cursor = Cursors.Hand
        btn.FlatAppearance.BorderSize = 0
        btn.FlatStyle = FlatStyle.Flat
        btn.Font = UITheme.FontBold
        btn.ForeColor = UITheme.TextWhite
        btn.Location = New Point(x, y)
        btn.Size = New Size(w, h)
        btn.RightToLeft = RightToLeft.Yes
        btn.Text = text
        btn.TextAlign = ContentAlignment.MiddleRight
        btn.UseVisualStyleBackColor = False
        btn.TabIndex = tabIdx
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  التعريفات
    ' ═══════════════════════════════════════════════════════
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tabAvailable As System.Windows.Forms.TabPage
    Friend WithEvents tabDocuments As System.Windows.Forms.TabPage

    ' التاب 1
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblDocNo As System.Windows.Forms.Label
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblShift As System.Windows.Forms.Label
    Friend WithEvents cboShift As System.Windows.Forms.ComboBox
    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents cboLine As System.Windows.Forms.ComboBox
    Friend WithEvents lblSupervisor As System.Windows.Forms.Label
    Friend WithEvents cboSupervisor As System.Windows.Forms.ComboBox
    Friend WithEvents lblStorekeeper As System.Windows.Forms.Label
    Friend WithEvents cboStorekeeper As System.Windows.Forms.ComboBox
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox

    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClearSearch As System.Windows.Forms.Button
    Friend WithEvents lblSelectedCount As System.Windows.Forms.Label

    Friend WithEvents dgvAvailable As System.Windows.Forms.DataGridView

    Friend WithEvents pnlConvert As System.Windows.Forms.Panel
    Friend WithEvents btnConvert As System.Windows.Forms.Button
    Friend WithEvents lblConvertHint As System.Windows.Forms.Label

    ' التاب 2
    Friend WithEvents pnlDocSearch As System.Windows.Forms.Panel
    Friend WithEvents lblDocFrom As System.Windows.Forms.Label
    Friend WithEvents dtpDocFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDocTo As System.Windows.Forms.Label
    Friend WithEvents dtpDocTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnDocSearch As System.Windows.Forms.Button
    Friend WithEvents btnDocClear As System.Windows.Forms.Button
    Friend WithEvents lblDocCount As System.Windows.Forms.Label
    Friend WithEvents btnCancelTransfer As System.Windows.Forms.Button

    Friend WithEvents splitDocs As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvTransfers As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDocActions As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblDetailTitle As System.Windows.Forms.Label
    Friend WithEvents btnApprove As System.Windows.Forms.Button
    Friend WithEvents btnReceive As System.Windows.Forms.Button
   
    Friend WithEvents btnRefreshDoc As System.Windows.Forms.Button
    Friend WithEvents dgvTransferDetails As System.Windows.Forms.DataGridView
End Class