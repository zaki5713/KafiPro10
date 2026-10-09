Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProDailyProductionUC
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

        Me.pnlFields = New System.Windows.Forms.Panel()
        Me.lblFormMode = New System.Windows.Forms.Label()

        Me.lblID = New System.Windows.Forms.Label()
        Me.txtID = New System.Windows.Forms.TextBox()

        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()

        Me.lblShift = New System.Windows.Forms.Label()
        Me.cboShift = New System.Windows.Forms.ComboBox()

        Me.lblLine = New System.Windows.Forms.Label()
        Me.cboLine = New System.Windows.Forms.ComboBox()

        Me.lblItem = New System.Windows.Forms.Label()
        Me.cboItem = New System.Windows.Forms.ComboBox()

        Me.lblSupervisor = New System.Windows.Forms.Label()
        Me.cboSupervisor = New System.Windows.Forms.ComboBox()

        Me.lblProducedQty = New System.Windows.Forms.Label()
        Me.txtProducedQty = New System.Windows.Forms.TextBox()

        Me.lblRejected = New System.Windows.Forms.Label()
        Me.txtRejected = New System.Windows.Forms.TextBox()

        Me.lblRawScrap = New System.Windows.Forms.Label()
        Me.txtRawScrap = New System.Windows.Forms.TextBox()

        Me.lblPackScrap = New System.Windows.Forms.Label()
        Me.txtPackScrap = New System.Windows.Forms.TextBox()

        Me.lblDowntime = New System.Windows.Forms.Label()
        Me.txtDowntime = New System.Windows.Forms.TextBox()

        Me.lblNotes = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()

        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()

        Me.dgvLogs = New System.Windows.Forms.DataGridView()

        Me.pnlFields.SuspendLayout()
        CType(Me.dgvLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' ============================================================
        ' pnlFields
        ' ============================================================

        Me.pnlFields.BackColor = UITheme.Surface
        Me.pnlFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFields.Location = New System.Drawing.Point(0, 0)
        Me.pnlFields.Name = "pnlFields"
        Me.pnlFields.Size = New System.Drawing.Size(800, 120)

        ' ============================================================
        ' الصف الأول
        '
        ' الرقم | التاريخ | الوردية | خط الإنتاج | المنتج |
        ' المشرف | الكمية | المرفوض | خام | تغليف | توقف
        '
        ' يتم توزيعها ديناميكيًا بواسطة ArrangeProductionFields()
        ' ============================================================

        SetupLabel(Me.lblID, "الرقم", 0, 0, 40)
        SetupTextBox(Me.txtID, 0, 0, 40, True, True)

        SetupLabel(Me.lblDate, "التاريخ *", 0, 0, 60)
        SetupDateTime(Me.dtpDate, 0, 0, 60)

        SetupLabel(Me.lblShift, "الوردية *", 0, 0, 55)
        SetupComboBox(Me.cboShift, 0, 0, 60)

        SetupLabel(Me.lblLine, "خط الإنتاج *", 0, 0, 85)
        SetupComboBox(Me.cboLine, 0, 0, 85)

        SetupLabel(Me.lblItem, "المنتج *", 0, 0, 85)
        SetupComboBox(Me.cboItem, 0, 0, 85)

        SetupLabel(Me.lblSupervisor, "المشرف *", 0, 0, 80)
        SetupComboBox(Me.cboSupervisor, 0, 0, 80)

        SetupLabel(Me.lblProducedQty, "الكمية *", 0, 0, 70)
        SetupTextBox(Me.txtProducedQty, 0, 0, 70)

        SetupLabel(Me.lblRejected, "المرفوض", 0, 0, 60)
        SetupTextBox(Me.txtRejected, 0, 0, 60)

        SetupLabel(Me.lblRawScrap, "خام", 0, 0, 60)
        SetupTextBox(Me.txtRawScrap, 0, 0, 60)

        SetupLabel(Me.lblPackScrap, "تغليف", 0, 0, 60)
        SetupTextBox(Me.txtPackScrap, 0, 0, 60)

        SetupLabel(Me.lblDowntime, "التوقف", 0, 0, 50)
        SetupTextBox(Me.txtDowntime, 0, 0, 55)


        ' ============================================================
        ' الصف الثاني
        ' الملاحظات + حالة النموذج + الأزرار
        ' ============================================================

        SetupLabel(Me.lblNotes, "ملاحظات", 5, 60, 75)

        Me.txtNotes.Anchor =
            CType(System.Windows.Forms.AnchorStyles.Top Or
                  System.Windows.Forms.AnchorStyles.Left,
                  System.Windows.Forms.AnchorStyles)

        Me.txtNotes.BackColor = UITheme.Surface
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Font = UITheme.FontNormal
        Me.txtNotes.Location = New System.Drawing.Point(20, 70)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.RightToLeft = System.Windows.Forms.RightToLeft.Yes

        Me.txtNotes.Size = New System.Drawing.Size(500, 30)
        Me.txtNotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right


        ' ============================================================
        ' حالة النموذج
        ' ============================================================
        ' ============================================================
        ' الصف الثاني
        ' وضع النموذج + الأزرار
        '
        ' الترتيب:
        ' وضع الإضافة | حفظ | إلغاء | جديد | حذف | تحديث
        ' ============================================================
        SetupBadge(Me.lblFormMode, "➕  وضع الإضافة", 1000, 60, 150, 30, UITheme.Success)
        ' ============================================================
        ' الأزرار
        ' ============================================================
        SetupButton(Me.btnSave, UITheme.Success, "💾 حفظ", 820, 62, 75, 28, 0)
        SetupButton(Me.btnCancel, UITheme.TextMuted, "✖ إلغاء", 740, 62, 75, 28, 1)
        SetupButton(Me.btnAdd, UITheme.Success, "➕ جديد", 660, 62, 75, 28, 2)
        SetupButton(Me.btnDelete, UITheme.Danger, "🗑 حذف", 580, 62, 75, 28, 3)
        SetupButton(Me.btnRefresh, UITheme.TextMuted, "🔄 تحديث", 485, 62, 85, 28, 4)
        SetupButton(Me.btnRefresh, UITheme.TextMuted, "🔄 تحديث", 900, 62, 85, 28, 4)
        ' ============================================================
        ' إضافة العناصر إلى pnlFields
        ' ============================================================
        Me.pnlFields.Controls.AddRange(
            New System.Windows.Forms.Control() {
        Me.lblID,
                Me.txtID,
                Me.lblDate,
                Me.dtpDate,
                Me.lblShift,
                Me.cboShift,
                Me.lblLine,
                Me.cboLine,
                Me.lblItem,
                Me.cboItem,
                Me.lblSupervisor,
                Me.cboSupervisor,
                Me.lblProducedQty,
                Me.txtProducedQty,
                Me.lblRejected,
                Me.txtRejected,
                Me.lblRawScrap,
                Me.txtRawScrap,
                Me.lblPackScrap,
                Me.txtPackScrap,
                Me.lblDowntime,
                Me.txtDowntime,
                Me.lblNotes, Me.txtNotes, Me.lblFormMode,
                Me.btnSave,
                Me.btnCancel,
                Me.btnAdd,
                Me.btnDelete,
                Me.btnRefresh})
        ' ============================================================
        ' الجدول
        ' ============================================================

        Me.dgvLogs.AllowUserToAddRows = False
        Me.dgvLogs.AllowUserToDeleteRows = False

        Me.dgvLogs.AutoSizeColumnsMode =
            System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill

        Me.dgvLogs.BackgroundColor = UITheme.Surface
        Me.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvLogs.ColumnHeadersHeight = 40
        Me.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLogs.Font = UITheme.FontNormal
        Me.dgvLogs.MultiSelect = False
        Me.dgvLogs.Name = "dgvLogs"
        Me.dgvLogs.ReadOnly = True
        Me.dgvLogs.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvLogs.RowHeadersVisible = False
        Me.dgvLogs.RowTemplate.Height = 32

        Me.dgvLogs.SelectionMode =
            System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect


        ' ============================================================
        ' ProDailyProductionUC
        ' ============================================================

        Me.AutoScaleDimensions =
            New System.Drawing.SizeF(7.0!, 15.0!)

        Me.AutoScaleMode =
            System.Windows.Forms.AutoScaleMode.Font

        Me.BackColor = UITheme.Background

        Me.Controls.Add(Me.dgvLogs)
        Me.Controls.Add(Me.pnlFields)

        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)

        Me.Name = "ProDailyProductionUC"

        Me.RightToLeft =
            System.Windows.Forms.RightToLeft.Yes

        Me.Size = New System.Drawing.Size(800, 600)


        ' ============================================================
        ' ربط حدث تغيير الحجم
        ' ============================================================

        AddHandler Me.Resize, AddressOf ProDailyProductionUC_Resize


        Me.pnlFields.ResumeLayout(False)
        Me.pnlFields.PerformLayout()

        CType(Me.dgvLogs, System.ComponentModel.ISupportInitialize).EndInit()

        Me.ResumeLayout(False)


        ' ============================================================
        ' الترتيب الأول
        ' ============================================================

        ArrangeProductionFields()

    End Sub


    ' ============================================================
    ' التوزيع الديناميكي للـ 11 حقل
    ' ============================================================

    Private Sub ArrangeProductionFields()

        If Me.pnlFields Is Nothing Then Return

        Dim panelWidth As Integer =
            Me.pnlFields.ClientSize.Width

        If panelWidth <= 0 Then Return
        ' ========================================================
        ' المسافة بين الحقول
        ' ========================================================
        Dim gap As Integer = 4
        ' هامش يمين ويسار
        Dim margin As Integer = 8
        ' ========================================================
        ' الأحجام الأساسية
        ' الوردية والمنتج أصغر حسب طلبك
        ' ========================================================
        Dim wID As Integer = 40
        Dim wDate As Integer = 65
        Dim wShift As Integer = 65
        Dim wLine As Integer = 80
        Dim wItem As Integer = 80
        Dim wSupervisor As Integer = 80

        Dim wProduced As Integer = 70
        Dim wRejected As Integer = 60
        Dim wRaw As Integer = 60
        Dim wPack As Integer = 60
        Dim wDowntime As Integer = 60
        ' ========================================================
        ' إجمالي العرض الأساسي
        ' ========================================================
        Dim totalWidth As Integer =
            wID +
            wDate +
            wShift +
            wLine +
            wItem +
            wSupervisor +
            wProduced +
            wRejected +
            wRaw +
            wPack +
            wDowntime +
            (gap * 10) +
            (margin * 2)
        ' ========================================================
        ' لو عندنا مساحة إضافية
        ' نوزعها على:
        ' التاريخ / الخط / المنتج / المشرف
        ' ========================================================
        Dim extra As Integer =
            panelWidth - totalWidth

        If extra > 0 Then

            Dim addEach As Integer =
                extra \ 4

            wDate += addEach
            wLine += addEach
            wItem += addEach
            wSupervisor += addEach

            Dim usedExtra As Integer =
                addEach * 4

            Dim remainder As Integer =
                extra - usedExtra

            wItem += remainder

        End If


        ' ========================================================
        ' بداية التوزيع من اليمين
        ' ========================================================

        Dim x As Integer =
            panelWidth - margin - wID


        ' ========================================================
        ' 1 - الرقم
        ' ========================================================

        SetFieldPosition(
            Me.lblID,
            Me.txtID,
            x,
            wID)

        x -= gap + wDate


        ' ========================================================
        ' 2 - التاريخ
        ' ========================================================

        SetFieldPosition(
            Me.lblDate,
            Me.dtpDate,
            x,
            wDate)

        x -= gap + wShift


        ' ========================================================
        ' 3 - الوردية
        ' ========================================================

        SetFieldPosition(
            Me.lblShift,
            Me.cboShift,
            x,
            wShift)

        x -= gap + wLine


        ' ========================================================
        ' 4 - خط الإنتاج
        ' ========================================================

        SetFieldPosition(
            Me.lblLine,
            Me.cboLine,
            x,
            wLine)

        x -= gap + wItem


        ' ========================================================
        ' 5 - المنتج
        ' ========================================================

        SetFieldPosition(
            Me.lblItem,
            Me.cboItem,
            x,
            wItem)

        x -= gap + wSupervisor


        ' ========================================================
        ' 6 - المشرف
        ' ========================================================

        SetFieldPosition(
            Me.lblSupervisor,
            Me.cboSupervisor,
            x,
            wSupervisor)

        x -= gap + wProduced


        ' ========================================================
        ' 7 - الكمية المنتجة
        ' ========================================================

        SetFieldPosition(
            Me.lblProducedQty,
            Me.txtProducedQty,
            x,
            wProduced)

        x -= gap + wRejected


        ' ========================================================
        ' 8 - المرفوض
        ' ========================================================

        SetFieldPosition(Me.lblRejected, Me.txtRejected, x, wRejected)
        x -= gap + wRaw
        ' ========================================================
        ' 9 - هوالك خام
        ' ========================================================

        SetFieldPosition(Me.lblRawScrap, Me.txtRawScrap, x, wRaw)
        x -= gap + wPack
        ' ========================================================
        ' 10 - هوالك تغليف
        ' ========================================================
        SetFieldPosition(Me.lblPackScrap, Me.txtPackScrap, x, wPack)
        x -= gap + wDowntime
        ' ========================================================
        ' 11 - وقت التوقف
        ' ========================================================

        SetFieldPosition(Me.lblDowntime, Me.txtDowntime, x, wDowntime)

    End Sub
    ' ============================================================
    ' ضبط Label + Control
    ' ============================================================
    Private Sub SetFieldPosition(lbl As Control, ctrl As Control, x As Integer, w As Integer)
        lbl.Location = New Point(x, 6)
        lbl.Size = New Size(w, 16)
        ctrl.Location = New Point(x, 25)
        ctrl.Size = New Size(w, 24)
    End Sub
    ' ============================================================
    ' إعادة التوزيع عند تغيير حجم UserControl
    ' ============================================================
    Private Sub ProDailyProductionUC_Resize(sender As Object, e As EventArgs)
        ArrangeProductionFields()
    End Sub
    ' ============================================================
    ' Helper - Label
    ' ============================================================
    Private Sub SetupLabel(lbl As Label, text As String, x As Integer, y As Integer, w As Integer)

        lbl.Anchor = CType(AnchorStyles.Top Or AnchorStyles.Right, AnchorStyles)

        lbl.AutoSize = False

        lbl.Font = UITheme.FontBold

        lbl.ForeColor = UITheme.TextPrimary

        lbl.Location = New Point(x, y)

        lbl.Size = New Size(w, 16)

        lbl.RightToLeft = RightToLeft.Yes

        lbl.Text = text

        lbl.TextAlign = ContentAlignment.MiddleRight

    End Sub
    ' ============================================================
    ' Helper - TextBox
    ' ============================================================
    Private Sub SetupTextBox(txt As TextBox, x As Integer, y As Integer,
        w As Integer, Optional isReadOnly As Boolean = False, Optional centered As Boolean = False)

        txt.Anchor =
          CType(AnchorStyles.Top Or AnchorStyles.Right, AnchorStyles)

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
            txt.TextAlign = HorizontalAlignment.Center
        End If
        If isReadOnly Then
            txt.TabStop = False
        End If
    End Sub
    ' ============================================================
    ' Helper - ComboBox
    ' ============================================================
    Private Sub SetupComboBox(cbo As ComboBox, x As Integer, y As Integer, w As Integer)

        cbo.Anchor = CType(AnchorStyles.Top Or AnchorStyles.Right, AnchorStyles)

        cbo.BackColor = UITheme.Surface
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        cbo.FlatStyle = FlatStyle.Standard
        cbo.Font = UITheme.FontNormal
        cbo.FormattingEnabled = True
        cbo.Location = New Point(x, y)
        cbo.Size = New Size(w, 24)
        cbo.RightToLeft = RightToLeft.Yes
    End Sub
    ' ============================================================
    ' Helper - DateTimePicker
    ' ============================================================
    Private Sub SetupDateTime(dtp As DateTimePicker, x As Integer, y As Integer, w As Integer)
        dtp.Anchor =
            CType(AnchorStyles.Top Or AnchorStyles.Right, AnchorStyles)

        dtp.Font = UITheme.FontNormal
        dtp.Format = DateTimePickerFormat.Short

        dtp.Location = New Point(x, y)

        dtp.Size = New Size(w, 24)
        dtp.RightToLeft = RightToLeft.Yes
        dtp.RightToLeftLayout = True
    End Sub


    ' ============================================================
    ' Helper - Button
    ' ============================================================

    Private Sub SetupButton(
        btn As Button,
        bgColor As Color,
        text As String,
        x As Integer,
        y As Integer,
        w As Integer,
        h As Integer,
        tabIdx As Integer)

        btn.BackColor =
            bgColor

        btn.Cursor =
            Cursors.Hand

        btn.FlatAppearance.BorderSize =
            0

        btn.FlatStyle =
            FlatStyle.Flat

        btn.Font =
            UITheme.FontBold

        btn.ForeColor =
            UITheme.TextWhite

        btn.Location =
            New Point(x, y)

        btn.Size =
            New Size(w, h)

        btn.RightToLeft =
            RightToLeft.Yes

        btn.Text =
            text

        btn.TextAlign =
            ContentAlignment.MiddleCenter

        btn.UseVisualStyleBackColor =
            False

        btn.TabIndex =
            tabIdx

    End Sub


    ' ============================================================
    ' Helper - Badge
    ' ============================================================

    Private Sub SetupBadge(lbl As Label, text As String, x As Integer, y As Integer, w As Integer, h As Integer, bgColor As Color)

        lbl.Anchor = CType(AnchorStyles.Top Or AnchorStyles.Left, AnchorStyles)

        lbl.AutoSize = False

        lbl.BackColor = bgColor

        lbl.Font = UITheme.FontTitle

        lbl.ForeColor = UITheme.TextWhite

        lbl.Location = New Point(x, y)

        lbl.Size = New Size(w, h)

        lbl.RightToLeft = RightToLeft.Yes

        lbl.Text = text

        lbl.TextAlign = ContentAlignment.MiddleCenter
    End Sub


    ' ============================================================
    ' التعريفات
    ' ============================================================

    Friend WithEvents pnlFields As System.Windows.Forms.Panel

    Friend WithEvents lblFormMode As System.Windows.Forms.Label

    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox

    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker

    Friend WithEvents lblShift As System.Windows.Forms.Label
    Friend WithEvents cboShift As System.Windows.Forms.ComboBox

    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents cboLine As System.Windows.Forms.ComboBox

    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents cboItem As System.Windows.Forms.ComboBox

    Friend WithEvents lblSupervisor As System.Windows.Forms.Label
    Friend WithEvents cboSupervisor As System.Windows.Forms.ComboBox

    Friend WithEvents lblProducedQty As System.Windows.Forms.Label
    Friend WithEvents txtProducedQty As System.Windows.Forms.TextBox

    Friend WithEvents lblRejected As System.Windows.Forms.Label
    Friend WithEvents txtRejected As System.Windows.Forms.TextBox

    Friend WithEvents lblRawScrap As System.Windows.Forms.Label
    Friend WithEvents txtRawScrap As System.Windows.Forms.TextBox

    Friend WithEvents lblPackScrap As System.Windows.Forms.Label
    Friend WithEvents txtPackScrap As System.Windows.Forms.TextBox

    Friend WithEvents lblDowntime As System.Windows.Forms.Label
    Friend WithEvents txtDowntime As System.Windows.Forms.TextBox

    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox

    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dgvLogs As System.Windows.Forms.DataGridView
End Class