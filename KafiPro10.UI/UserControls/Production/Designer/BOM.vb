Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Partial Class ProMasterDataUC

    ' ═══════ عناصر تاب رأس الخلطات (Header) ═══════
    Friend WithEvents pnlBOMFields As Panel
    Friend WithEvents lblBOMFormMode As Label
    Friend WithEvents lblBOMID As Label
    Friend WithEvents txtBOMID As TextBox
    Friend WithEvents lblBOMCode As Label
    Friend WithEvents txtBOMCode As TextBox
    Friend WithEvents lblFinishedItem As Label
    Friend WithEvents cboFinishedItem As ComboBox
    Friend WithEvents lblOutputQty As Label
    Friend WithEvents txtOutputQty As TextBox
    Friend WithEvents lblOutputUnit As Label
    Friend WithEvents cboOutputUnit As ComboBox
    Friend WithEvents lblWastage As Label
    Friend WithEvents txtWastage As TextBox
    Friend WithEvents chkBOMActive As CheckBox
    Friend WithEvents btnSaveBOM As Button
    Friend WithEvents btnCancelBOM As Button
    Friend WithEvents btnAddBOM As Button
    Friend WithEvents btnDeleteBOM As Button
    Friend WithEvents btnRefreshBOM As Button
    Friend WithEvents dgvBOM As DataGridView

    ' ═══════ عناصر تاب تفاصيل الخلطات (Details) ═══════
    Friend WithEvents pnlBOMDetailsTop As Panel
    Friend WithEvents lblBOMSelector As Label
    Friend WithEvents cboBOMSelector As ComboBox
    Friend WithEvents pnlBOMDetailFields As Panel
    Friend WithEvents lblBOMDetailFormMode As Label
    Friend WithEvents lblBOMDetailID As Label
    Friend WithEvents txtBOMDetailID As TextBox
    Friend WithEvents lblMaterial As Label
    Friend WithEvents cboMaterial As ComboBox
    Friend WithEvents lblQty As Label
    Friend WithEvents txtQty As TextBox
    Friend WithEvents btnSaveBOMDetail As Button
    Friend WithEvents btnCancelBOMDetail As Button
    Friend WithEvents btnAddBOMDetail As Button
    Friend WithEvents btnDeleteBOMDetail As Button
    Friend WithEvents btnRefreshBOMDetail As Button
    Friend WithEvents dgvBOMDetails As DataGridView

    ' ═══════════════════════════════════════════════════════
    '  تهيئة تاب "توصيف الخلطات" (Header)
    ' ═══════════════════════════════════════════════════════
    Private Sub InitBOMHeaderTab()
        Me.pnlBOMFields = New Panel()
        Me.lblBOMFormMode = New Label()
        Me.lblBOMID = New Label()
        Me.txtBOMID = New TextBox()
        Me.lblBOMCode = New Label()
        Me.txtBOMCode = New TextBox()
        Me.lblFinishedItem = New Label()
        Me.cboFinishedItem = New ComboBox()
        Me.lblOutputQty = New Label()
        Me.txtOutputQty = New TextBox()
        Me.lblOutputUnit = New Label()
        Me.cboOutputUnit = New ComboBox()
        Me.lblWastage = New Label()
        Me.txtWastage = New TextBox()
        Me.chkBOMActive = New CheckBox()
        Me.btnSaveBOM = New Button()
        Me.btnCancelBOM = New Button()
        Me.btnAddBOM = New Button()
        Me.btnDeleteBOM = New Button()
        Me.btnRefreshBOM = New Button()
        Me.dgvBOM = New DataGridView()

        Me.pnlBOMFields.SuspendLayout()
        CType(Me.dgvBOM, System.ComponentModel.ISupportInitialize).BeginInit()

        ' ════ لوحة حقول الرأس ════
        Me.pnlBOMFields.BackColor = UITheme.Surface
        Me.pnlBOMFields.BorderStyle = BorderStyle.FixedSingle
        Me.pnlBOMFields.Dock = DockStyle.Top
        Me.pnlBOMFields.Location = New Point(3, 3)
        Me.pnlBOMFields.Name = "pnlBOMFields"
        Me.pnlBOMFields.Size = New Size(786, 90)

        SetupLabel(Me.lblBOMID, "الرقم", 736, 8, 40)
        SetupTextBox(Me.txtBOMID, 736, 26, 40, True, True)

        SetupLabel(Me.lblBOMCode, "كود الخلطة *", 620, 8, 110)
        SetupTextBox(Me.txtBOMCode, 620, 26, 110)

        SetupLabel(Me.lblFinishedItem, "المنتج النهائي *", 365, 8, 250)
        SetupComboBox(Me.cboFinishedItem, 365, 26, 250)

        SetupLabel(Me.lblOutputQty, "الكمية *", 285, 8, 75)
        SetupTextBox(Me.txtOutputQty, 285, 26, 75)

        SetupLabel(Me.lblOutputUnit, "الوحدة *", 205, 8, 75)
        SetupComboBox(Me.cboOutputUnit, 205, 26, 75)

        SetupLabel(Me.lblWastage, "الفقد %", 130, 8, 70)
        SetupTextBox(Me.txtWastage, 130, 26, 70)

        SetupCheckBox(Me.chkBOMActive, "نشط", 80, 27, 45)

        ' ════ صف الأزرار ════
        SetupButton(Me.btnSaveBOM, UITheme.Success, "💾  حفظ", 15, 52, 100, 30, 0)
        SetupButton(Me.btnCancelBOM, UITheme.TextMuted, "✖  إلغاء", 120, 52, 100, 30, 1)
        SetupButton(Me.btnAddBOM, UITheme.Success, "➕  خلطة جديدة", 225, 52, 100, 30, 2)
        SetupButton(Me.btnDeleteBOM, UITheme.Danger, "🗑  حذف", 330, 52, 90, 30, 3)
        SetupButton(Me.btnRefreshBOM, UITheme.TextMuted, "🔄  تحديث", 425, 52, 90, 30, 4)
        SetupBadge(Me.lblBOMFormMode, "➕  وضع الإضافة", 530, 51, 240, 32, UITheme.Success)

        Me.pnlBOMFields.Controls.AddRange(New Control() {
            Me.lblBOMID, Me.txtBOMID,
            Me.lblBOMCode, Me.txtBOMCode,
            Me.lblFinishedItem, Me.cboFinishedItem,
            Me.lblOutputQty, Me.txtOutputQty,
            Me.lblOutputUnit, Me.cboOutputUnit,
            Me.lblWastage, Me.txtWastage,
            Me.chkBOMActive,
            Me.btnSaveBOM, Me.btnCancelBOM,
            Me.btnAddBOM, Me.btnDeleteBOM, Me.btnRefreshBOM,
            Me.lblBOMFormMode
        })

        ' ════ جدول الخلطات — يأخذ باقي المساحة كاملة ════
        With Me.dgvBOM
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .BackgroundColor = UITheme.Surface
            .BorderStyle = BorderStyle.None
            .ColumnHeadersHeight = 40
            .Dock = DockStyle.Fill
            .Font = UITheme.FontNormal
            .MultiSelect = False
            .Name = "dgvBOM"
            .ReadOnly = True
            .RightToLeft = RightToLeft.Yes
            .RowHeadersVisible = False
            .RowTemplate.Height = 34
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With

        Me.tabBOMHeader.Controls.Add(Me.dgvBOM)
        Me.tabBOMHeader.Controls.Add(Me.pnlBOMFields)

        Me.pnlBOMFields.ResumeLayout(False)
        Me.pnlBOMFields.PerformLayout()
        CType(Me.dgvBOM, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  تهيئة تاب "تفاصيل الخلطات" (Details)
    ' ═══════════════════════════════════════════════════════
    Private Sub InitBOMDetailsTab()
        Me.pnlBOMDetailsTop = New Panel()
        Me.lblBOMSelector = New Label()
        Me.cboBOMSelector = New ComboBox()
        Me.pnlBOMDetailFields = New Panel()
        Me.lblBOMDetailFormMode = New Label()
        Me.lblBOMDetailID = New Label()
        Me.txtBOMDetailID = New TextBox()
        Me.lblMaterial = New Label()
        Me.cboMaterial = New ComboBox()
        Me.lblQty = New Label()
        Me.txtQty = New TextBox()
        Me.btnSaveBOMDetail = New Button()
        Me.btnCancelBOMDetail = New Button()
        Me.btnAddBOMDetail = New Button()
        Me.btnDeleteBOMDetail = New Button()
        Me.btnRefreshBOMDetail = New Button()
        Me.dgvBOMDetails = New DataGridView()

        Me.pnlBOMDetailsTop.SuspendLayout()
        Me.pnlBOMDetailFields.SuspendLayout()
        CType(Me.dgvBOMDetails, System.ComponentModel.ISupportInitialize).BeginInit()

        ' ════ لوحة اختيار الخلطة ════
        Me.pnlBOMDetailsTop.BackColor = UITheme.ButtonNormal
        Me.pnlBOMDetailsTop.Dock = DockStyle.Top
        Me.pnlBOMDetailsTop.Location = New Point(3, 3)
        Me.pnlBOMDetailsTop.Name = "pnlBOMDetailsTop"
        Me.pnlBOMDetailsTop.Padding = New Padding(8, 6, 8, 6)
        Me.pnlBOMDetailsTop.Size = New Size(786, 46)

        Me.lblBOMSelector.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        Me.lblBOMSelector.AutoSize = False
        Me.lblBOMSelector.Font = UITheme.FontBold
        Me.lblBOMSelector.ForeColor = UITheme.TextPrimary
        Me.lblBOMSelector.Location = New Point(586, 12)
        Me.lblBOMSelector.Name = "lblBOMSelector"
        Me.lblBOMSelector.Size = New Size(190, 22)
        Me.lblBOMSelector.Text = "📋 اختر الخلطة:"
        Me.lblBOMSelector.TextAlign = ContentAlignment.MiddleRight

        Me.cboBOMSelector.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        Me.cboBOMSelector.BackColor = UITheme.Surface
        Me.cboBOMSelector.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboBOMSelector.FlatStyle = FlatStyle.Standard
        Me.cboBOMSelector.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
        Me.cboBOMSelector.FormattingEnabled = True
        Me.cboBOMSelector.Location = New Point(286, 9)
        Me.cboBOMSelector.Name = "cboBOMSelector"
        Me.cboBOMSelector.RightToLeft = RightToLeft.Yes
        Me.cboBOMSelector.Size = New Size(294, 26)

        Me.pnlBOMDetailsTop.Controls.AddRange(New Control() {Me.cboBOMSelector, Me.lblBOMSelector})

        ' ════ لوحة حقول المكوّن ════
        Me.pnlBOMDetailFields.BackColor = UITheme.Surface
        Me.pnlBOMDetailFields.BorderStyle = BorderStyle.FixedSingle
        Me.pnlBOMDetailFields.Dock = DockStyle.Top
        Me.pnlBOMDetailFields.Location = New Point(3, 49)
        Me.pnlBOMDetailFields.Name = "pnlBOMDetailFields"
        Me.pnlBOMDetailFields.Size = New Size(786, 90)

        ' ════ الصف الأول ════
        SetupLabel(Me.lblBOMDetailID, "الرقم", 736, 8, 40)
        SetupTextBox(Me.txtBOMDetailID, 736, 26, 40, True, True)

        SetupLabel(Me.lblMaterial, "المادة *", 276, 8, 455)
        SetupComboBox(Me.cboMaterial, 276, 26, 455)

        SetupLabel(Me.lblQty, "الكمية (كجم) *", 161, 8, 110)
        SetupTextBox(Me.txtQty, 161, 26, 110)

        ' ════ صف الأزرار ════
        SetupButton(Me.btnSaveBOMDetail, UITheme.Success, "💾  حفظ", 15, 52, 95, 30, 0)
        SetupButton(Me.btnCancelBOMDetail, UITheme.TextMuted, "✖  إلغاء", 115, 52, 95, 30, 1)
        SetupButton(Me.btnAddBOMDetail, UITheme.Success, "➕  مكوّن جديد", 215, 52, 105, 30, 2)
        SetupButton(Me.btnDeleteBOMDetail, UITheme.Danger, "🗑  حذف", 325, 52, 85, 30, 3)
        SetupButton(Me.btnRefreshBOMDetail, UITheme.TextMuted, "🔄  تحديث", 415, 52, 85, 30, 4)

        Me.lblBOMDetailFormMode.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        Me.lblBOMDetailFormMode.AutoSize = False
        Me.lblBOMDetailFormMode.Font = UITheme.FontBold
        Me.lblBOMDetailFormMode.ForeColor = UITheme.TextSecondary
        Me.lblBOMDetailFormMode.Location = New Point(505, 54)
        Me.lblBOMDetailFormMode.Name = "lblBOMDetailFormMode"
        Me.lblBOMDetailFormMode.RightToLeft = RightToLeft.Yes
        Me.lblBOMDetailFormMode.Size = New Size(266, 26)
        Me.lblBOMDetailFormMode.Text = "➕  وضع الإضافة"
        Me.lblBOMDetailFormMode.TextAlign = ContentAlignment.MiddleRight

        Me.pnlBOMDetailFields.Controls.AddRange(New Control() {
            Me.lblBOMDetailID, Me.txtBOMDetailID,
            Me.lblMaterial, Me.cboMaterial,
            Me.lblQty, Me.txtQty,
            Me.btnSaveBOMDetail, Me.btnCancelBOMDetail,
            Me.btnAddBOMDetail, Me.btnDeleteBOMDetail, Me.btnRefreshBOMDetail,
            Me.lblBOMDetailFormMode
        })

        ' ════ جدول المكونات ════
        With Me.dgvBOMDetails
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .BackgroundColor = UITheme.Surface
            .BorderStyle = BorderStyle.None
            .ColumnHeadersHeight = 40
            .Dock = DockStyle.Fill
            .Font = UITheme.FontNormal
            .MultiSelect = False
            .Name = "dgvBOMDetails"
            .ReadOnly = True
            .RightToLeft = RightToLeft.Yes
            .RowHeadersVisible = False
            .RowTemplate.Height = 32
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With

        Me.tabBOMDetails.Controls.Add(Me.dgvBOMDetails)
        Me.tabBOMDetails.Controls.Add(Me.pnlBOMDetailFields)
        Me.tabBOMDetails.Controls.Add(Me.pnlBOMDetailsTop)

        Me.pnlBOMDetailsTop.ResumeLayout(False)
        Me.pnlBOMDetailFields.ResumeLayout(False)
        Me.pnlBOMDetailFields.PerformLayout()
        CType(Me.dgvBOMDetails, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

End Class