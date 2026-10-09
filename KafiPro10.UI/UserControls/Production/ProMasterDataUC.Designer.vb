Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq                                    ' ← جديد
Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories
Imports Microsoft.Data.SqlClient



<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProMasterDataUC
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
        Me.tabMasterData = New System.Windows.Forms.TabControl()
        Me.tabItems = New System.Windows.Forms.TabPage()
        Me.tabProductionLines = New System.Windows.Forms.TabPage()
        Me.tabLabor = New System.Windows.Forms.TabPage()
        Me.tabBOMHeader = New System.Windows.Forms.TabPage()
        Me.tabBOMDetails = New System.Windows.Forms.TabPage()
        Me.tabShifts = New System.Windows.Forms.TabPage()
        Me.tabUnits = New System.Windows.Forms.TabPage()

        Me.tabMasterData.SuspendLayout()
        Me.tabItems.SuspendLayout()
        Me.tabProductionLines.SuspendLayout()
        Me.tabLabor.SuspendLayout()
        Me.tabBOMHeader.SuspendLayout()
        Me.tabBOMDetails.SuspendLayout()
        Me.tabShifts.SuspendLayout()
        Me.tabUnits.SuspendLayout()
        Me.SuspendLayout()

        ' ═══════════════════════════════════════════════════════
        '  TabControl
        ' ═══════════════════════════════════════════════════════
        Me.tabMasterData.Controls.Add(Me.tabItems)
        Me.tabMasterData.Controls.Add(Me.tabProductionLines)
        Me.tabMasterData.Controls.Add(Me.tabLabor)
        Me.tabMasterData.Controls.Add(Me.tabBOMHeader)
        Me.tabMasterData.Controls.Add(Me.tabBOMDetails)
        Me.tabMasterData.Controls.Add(Me.tabShifts)
        Me.tabMasterData.Controls.Add(Me.tabUnits)
        Me.tabMasterData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMasterData.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.tabMasterData.Location = New System.Drawing.Point(0, 0)
        Me.tabMasterData.Name = "tabMasterData"
        Me.tabMasterData.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tabMasterData.RightToLeftLayout = True
        Me.tabMasterData.SelectedIndex = 0
        Me.tabMasterData.Size = New System.Drawing.Size(800, 600)
        Me.tabMasterData.TabIndex = 0

        ' ═══════════════════════════════════════════════════════
        '  TabPages Setup
        ' ═══════════════════════════════════════════════════════
        SetupTabPage(Me.tabItems, "tabItems", 0, "توصيف الأصناف")
        SetupTabPage(Me.tabProductionLines, "tabProductionLines", 1, "توصيف خطوط الإنتاج")
        SetupTabPage(Me.tabLabor, "tabLabor", 2, "العمالة")
        SetupTabPage(Me.tabBOMHeader, "tabBOMHeader", 3, "توصيف الخلطات")
        SetupTabPage(Me.tabBOMDetails, "tabBOMDetails", 4, "تفاصيل الخلطات")
        SetupTabPage(Me.tabShifts, "tabShifts", 5, "الورديات")
        SetupTabPage(Me.tabUnits, "tabUnits", 6, "الوحدات")

        ' ═══════════════════════════════════════════════════════
        '  استدعاء تهيئة كل تاب
        ' ═══════════════════════════════════════════════════════
        InitItemsTab()
        InitLinesTab()
        InitLaborTab()
        InitBOMHeaderTab()
        InitBOMDetailsTab()
        InitShiftsTab()
        InitUnitsTab()

        ' ═══════════════════════════════════════════════════════
        '  Main UserControl
        ' ═══════════════════════════════════════════════════════
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = UITheme.Background
        Me.Controls.Add(Me.tabMasterData)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "ProMasterDataUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(800, 600)

        Me.tabMasterData.ResumeLayout(False)
        Me.tabItems.ResumeLayout(False)
        Me.tabProductionLines.ResumeLayout(False)
        Me.tabLabor.ResumeLayout(False)
        Me.tabBOMHeader.ResumeLayout(False)
        Me.tabBOMDetails.ResumeLayout(False)
        Me.tabShifts.ResumeLayout(False)
        Me.tabUnits.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  تهيئة تاب الأصناف
    ' ═══════════════════════════════════════════════════════
    Private Sub InitItemsTab()
        ' ═══════ إنشاء العناصر ═══════
        Me.pnlToolbarItems = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.btnEditItem = New System.Windows.Forms.Button()
        Me.btnDeleteItem = New System.Windows.Forms.Button()
        Me.btnRefreshItem = New System.Windows.Forms.Button()
        Me.dgvItems = New System.Windows.Forms.DataGridView()
        Me.pnlItemFields = New System.Windows.Forms.Panel()
        Me.lblItemFormMode = New System.Windows.Forms.Label()
        Me.lblItemID = New System.Windows.Forms.Label()
        Me.txtItemID = New System.Windows.Forms.TextBox()
        Me.lblItemCode = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.lblItemNameAr = New System.Windows.Forms.Label()
        Me.txtItemNameAr = New System.Windows.Forms.TextBox()
        Me.lblItemNameEn = New System.Windows.Forms.Label()
        Me.txtItemNameEn = New System.Windows.Forms.TextBox()
        Me.lblItemType = New System.Windows.Forms.Label()
        Me.cboItemType = New System.Windows.Forms.ComboBox()
        Me.lblInvUnit = New System.Windows.Forms.Label()
        Me.cboInvUnit = New System.Windows.Forms.ComboBox()
        Me.lblFinUnit = New System.Windows.Forms.Label()
        Me.cboFinUnit = New System.Windows.Forms.ComboBox()
        Me.chkItemActive = New System.Windows.Forms.CheckBox()

        Me.lblNetWeight = New System.Windows.Forms.Label()
        Me.txtNetWeight = New System.Windows.Forms.TextBox()
        Me.lblPackagingWeight = New System.Windows.Forms.Label()
        Me.txtPackagingWeight = New System.Windows.Forms.TextBox()
        Me.lblGrossWeight = New System.Windows.Forms.Label()
        Me.txtGrossWeight = New System.Windows.Forms.TextBox()

        Me.btnSaveItem = New System.Windows.Forms.Button()
        Me.btnCancelItem = New System.Windows.Forms.Button()

        ' بحث
        Me.pnlItemSearch = New System.Windows.Forms.Panel()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearchType = New System.Windows.Forms.Label()
        Me.cboSearchType = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnClearSearch = New System.Windows.Forms.Button()
        Me.lblResultCount = New System.Windows.Forms.Label()

        ' ToolTip للأوزان
        Me.toolTipItems = New System.Windows.Forms.ToolTip()

        Me.pnlItemFields.SuspendLayout()
        Me.pnlItemSearch.SuspendLayout()

        ' ═══════════════════════════════════════════════════════
        '  ① لوحة البحث
        ' ═══════════════════════════════════════════════════════
        Me.pnlItemSearch.BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
        Me.pnlItemSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlItemSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlItemSearch.Location = New System.Drawing.Point(3, 3)
        Me.pnlItemSearch.Name = "pnlItemSearch"
        Me.pnlItemSearch.Size = New System.Drawing.Size(786, 62)

        ' ============================================================
        ' صف البحث بالكامل - RTL
        ' ============================================================

        ' [1] النوع
        SetupLabel(Me.lblSearchType, "النوع:", 715, 8, 55)
        SetupComboBox(Me.cboSearchType, 575, 6, 130)

        ' [2] عنوان البحث
        SetupLabel(Me.lblSearch, "🔍 بحث (كود/اسم):", 420, 8, 145)

        ' [3] مربع البحث
        SetupTextBox(Me.txtSearch, 180, 6, 230)

        ' [4] زر البحث
        SetupButton(Me.btnSearch,
            UITheme.Primary,
            "🔍  بحث",
            90, 6, 80, 26, 0)

        ' [5] زر المسح
        SetupButton(Me.btnClearSearch,
            UITheme.TextMuted,
            "✖  مسح",
            0, 6, 80, 26, 1)

        ' [6] عدد النتائج
        Me.lblResultCount.Anchor =
    CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)

        Me.lblResultCount.AutoSize = False
        Me.lblResultCount.Font = UITheme.FontBold
        Me.lblResultCount.ForeColor = UITheme.Primary
        Me.lblResultCount.Location = New System.Drawing.Point(0, 38)
        Me.lblResultCount.Name = "lblResultCount"
        Me.lblResultCount.Size = New System.Drawing.Size(80, 20)
        Me.lblResultCount.Text = "📋  0"
        Me.lblResultCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.pnlItemSearch.Controls.AddRange(
    New System.Windows.Forms.Control() {
        Me.lblSearchType,
        Me.cboSearchType,
        Me.lblSearch,
        Me.txtSearch,
        Me.btnSearch,
        Me.btnClearSearch,
        Me.lblResultCount
    }
)

        ' ═══════════════════════════════════════════════════════
        '  ② لوحة الحقول — صف واحد للحقول + صف للأزرار
        ' ═══════════════════════════════════════════════════════
        Me.pnlItemFields.BackColor = UITheme.Surface
        Me.pnlItemFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlItemFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlItemFields.Location = New System.Drawing.Point(3, 65)
        Me.pnlItemFields.Name = "pnlItemFields"
        Me.pnlItemFields.Size = New System.Drawing.Size(1100, 95)

        ' ═══════════════════════════════════════════════════════
        '  الصف الأول — 10 حقول في صف واحد (موسّعة)
        '  من اليمين: الرقم | كود | عربي | إنجليزي | النوع | وحدة مخزون |
        '           وحدة مالية | صافي | تغليف | قائم
        ' ═══════════════════════════════════════════════════════

        ' [1] الرقم
        SetupLabel(Me.lblItemID, "الرقم", 1020, 6, 50)
        SetupTextBox(Me.txtItemID, 1020, 24, 50, True, True)

        ' [2] كود الصنف
        SetupLabel(Me.lblItemCode, "كود الصنف *", 910, 6, 100)
        SetupTextBox(Me.txtItemCode, 910, 24, 100)

        ' [3] الاسم عربي
        SetupLabel(Me.lblItemNameAr, "الاسم (عربي) *", 770, 6, 130)
        SetupTextBox(Me.txtItemNameAr, 770, 24, 130)

        ' [4] الاسم إنجليزي
        SetupLabel(Me.lblItemNameEn, "الاسم (إنجليزي) *", 630, 6, 130)
        SetupTextBox(Me.txtItemNameEn, 630, 24, 130)

        ' [5] النوع
        SetupLabel(Me.lblItemType, "النوع *", 530, 6, 90)
        SetupComboBox(Me.cboItemType, 530, 24, 90)

        ' [6] وحدة المخزون
        SetupLabel(Me.lblInvUnit, "وحدة المخزون *", 420, 6, 100)
        SetupComboBox(Me.cboInvUnit, 420, 24, 100)

        ' [7] الوحدة المالية
        SetupLabel(Me.lblFinUnit, "الوحدة المالية *", 305, 6, 105)
        SetupComboBox(Me.cboFinUnit, 305, 24, 105)

        ' [8] الوزن الصافي
        SetupLabel(Me.lblNetWeight, "الصافي (كجم)", 220, 6, 75)
        SetupTextBox(Me.txtNetWeight, 220, 24, 75)

        ' [9] وزن التغليف
        SetupLabel(Me.lblPackagingWeight, "التغليف (كجم)", 130, 6, 80)
        SetupTextBox(Me.txtPackagingWeight, 130, 24, 80)

        ' [10] الوزن القائم
        SetupLabel(Me.lblGrossWeight, "الوزن القائم", 40, 6, 80)
        SetupTextBox(Me.txtGrossWeight, 40, 24, 80, True, False)

        ' ═══════════════════════════════════════════════════════
        '  الصف الثاني — Checkbox + الأزرار + Badge (مُقرّب)
        ' ═══════════════════════════════════════════════════════

        ' [☑ نشط]
        SetupCheckBox(Me.chkItemActive, "نشط", 500, 58, 80)

        ' [💾 حفظ]
        SetupButton(Me.btnSaveItem, UITheme.Success, "💾  حفظ", 600, 56, 100, 30, 0)

        ' [✖ إلغاء]
        SetupButton(Me.btnCancelItem, UITheme.TextMuted, "✖  إلغاء", 720, 56, 100, 30, 1)

        ' [Badge — وضع الإضافة] — مُقرّب للأزرار
        SetupBadge(Me.lblItemFormMode, "➕  وضع الإضافة", 800, 54, 300, 34, UITheme.Success)

        ' ═══ إضافة العناصر ═══
        Me.pnlItemFields.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblItemID, Me.txtItemID,
            Me.lblItemCode, Me.txtItemCode,
            Me.lblItemNameAr, Me.txtItemNameAr,
            Me.lblItemNameEn, Me.txtItemNameEn,
            Me.lblItemType, Me.cboItemType,
            Me.lblInvUnit, Me.cboInvUnit,
            Me.lblFinUnit, Me.cboFinUnit,
            Me.lblNetWeight, Me.txtNetWeight,
            Me.lblPackagingWeight, Me.txtPackagingWeight,
            Me.lblGrossWeight, Me.txtGrossWeight,
            Me.chkItemActive,
            Me.btnSaveItem, Me.btnCancelItem,
            Me.lblItemFormMode
        })

        ' ═══ ToolTips للأوزان ═══
        Me.toolTipItems.SetToolTip(Me.txtNetWeight, "الوزن الصافي للصنف بدون التغليف (كجم)")
        Me.toolTipItems.SetToolTip(Me.txtPackagingWeight, "وزن مواد التغليف للصنف (كجم)")
        Me.toolTipItems.SetToolTip(Me.txtGrossWeight, "الوزن القائم = الوزن الصافي + وزن التغليف (يُحسب تلقائيًا)")

        ' ═══════════════════════════════════════════════════════
        '  ③ Toolbar
        ' ═══════════════════════════════════════════════════════
        Me.pnlToolbarItems.BackColor = UITheme.ButtonNormal
        Me.pnlToolbarItems.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolbarItems.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlToolbarItems.Location = New System.Drawing.Point(3, 160)
        Me.pnlToolbarItems.Name = "pnlToolbarItems"
        Me.pnlToolbarItems.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlToolbarItems.Size = New System.Drawing.Size(786, 48)

        SetupButton(Me.btnAddItem, UITheme.Success, "➕  جديد", 681, 9, 95, 32, 0)
        SetupButton(Me.btnDeleteItem, UITheme.Danger, "🗑  حذف", 578, 9, 95, 32, 1)
        SetupButton(Me.btnRefreshItem, UITheme.TextMuted, "🔄  تحديث", 475, 9, 95, 32, 2)

        Me.pnlToolbarItems.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.btnAddItem, Me.btnDeleteItem, Me.btnRefreshItem
        })

        ' ═══════════════════════════════════════════════════════
        '  ④ الجدول
        ' ═══════════════════════════════════════════════════════
        Me.dgvItems.AllowUserToAddRows = False
        Me.dgvItems.AllowUserToDeleteRows = False
        Me.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvItems.BackgroundColor = UITheme.Surface
        Me.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvItems.ColumnHeadersHeight = 42
        Me.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItems.Font = UITheme.FontNormal
        Me.dgvItems.Location = New System.Drawing.Point(3, 208)
        Me.dgvItems.MultiSelect = False
        Me.dgvItems.Name = "dgvItems"
        Me.dgvItems.ReadOnly = True
        Me.dgvItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvItems.RowHeadersVisible = False
        Me.dgvItems.RowTemplate.Height = 34
        Me.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvItems.Size = New System.Drawing.Size(786, 355)

        Me.tabItems.Controls.Add(Me.dgvItems)
        Me.tabItems.Controls.Add(Me.pnlToolbarItems)
        Me.tabItems.Controls.Add(Me.pnlItemFields)
        Me.tabItems.Controls.Add(Me.pnlItemSearch)

        Me.pnlItemFields.ResumeLayout(False)
        Me.pnlItemFields.PerformLayout()
        Me.pnlItemSearch.ResumeLayout(False)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  تهيئة تاب خطوط الإنتاج
    ' ═══════════════════════════════════════════════════════
    Private Sub InitLinesTab()
        Me.pnlToolbarLines = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAddLine = New System.Windows.Forms.Button()
        Me.btnEditLine = New System.Windows.Forms.Button()
        Me.btnDeleteLine = New System.Windows.Forms.Button()
        Me.btnRefreshLine = New System.Windows.Forms.Button()
        Me.dgvProductionLines = New System.Windows.Forms.DataGridView()
        Me.pnlLineFields = New System.Windows.Forms.Panel()
        Me.lblLineFormMode = New System.Windows.Forms.Label()
        Me.lblLineID = New System.Windows.Forms.Label()
        Me.txtLineID = New System.Windows.Forms.TextBox()
        Me.lblLineCode = New System.Windows.Forms.Label()
        Me.txtLineCode = New System.Windows.Forms.TextBox()
        Me.lblLineNameAr = New System.Windows.Forms.Label()
        Me.txtLineNameAr = New System.Windows.Forms.TextBox()
        Me.lblLineNameEn = New System.Windows.Forms.Label()
        Me.txtLineNameEn = New System.Windows.Forms.TextBox()
        Me.lblCycleTime = New System.Windows.Forms.Label()
        Me.txtCycleTime = New System.Windows.Forms.TextBox()
        Me.chkLineActive = New System.Windows.Forms.CheckBox()
        Me.btnSaveLine = New System.Windows.Forms.Button()
        Me.btnCancelLine = New System.Windows.Forms.Button()

        Me.pnlLineFields.SuspendLayout()

        Me.pnlLineFields.BackColor = UITheme.Surface
        Me.pnlLineFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLineFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLineFields.Location = New System.Drawing.Point(3, 3)
        Me.pnlLineFields.Name = "pnlLineFields"
        Me.pnlLineFields.Size = New System.Drawing.Size(786, 108)

        SetupLabel(Me.lblLineID, "الرقم", 736, 10, 40)
        SetupTextBox(Me.txtLineID, 736, 28, 40, True, True)
        SetupLabel(Me.lblLineCode, "كود الخط *", 571, 10, 160)
        SetupTextBox(Me.txtLineCode, 571, 28, 160)
        SetupLabel(Me.lblLineNameAr, "الاسم (عربي) *", 436, 10, 130)
        SetupTextBox(Me.txtLineNameAr, 436, 28, 130)
        SetupLabel(Me.lblLineNameEn, "الاسم (إنجليزي)", 301, 10, 130)
        SetupTextBox(Me.txtLineNameEn, 301, 28, 130)
        SetupLabel(Me.lblCycleTime, "زمن الدورة (ثانية)", 171, 10, 120)
        SetupTextBox(Me.txtCycleTime, 171, 28, 120)
        SetupCheckBox(Me.chkLineActive, "نشط", 110, 29, 50)
        SetupButton(Me.btnSaveLine, UITheme.Success, "💾  حفظ", 15, 62, 110, 34, 0)
        SetupButton(Me.btnCancelLine, UITheme.TextMuted, "✖  إلغاء", 133, 62, 110, 34, 1)
        SetupBadge(Me.lblLineFormMode, "➕  وضع الإضافة", 521, 60, 250, 38, UITheme.Success)

        Me.pnlLineFields.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblLineID, Me.txtLineID,
            Me.lblLineCode, Me.txtLineCode,
            Me.lblLineNameAr, Me.txtLineNameAr,
            Me.lblLineNameEn, Me.txtLineNameEn,
            Me.lblCycleTime, Me.txtCycleTime,
            Me.chkLineActive,
            Me.btnSaveLine, Me.btnCancelLine,
            Me.lblLineFormMode
        })

        Me.pnlToolbarLines.BackColor = UITheme.ButtonNormal
        Me.pnlToolbarLines.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolbarLines.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlToolbarLines.Location = New System.Drawing.Point(3, 111)
        Me.pnlToolbarLines.Name = "pnlToolbarLines"
        Me.pnlToolbarLines.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlToolbarLines.Size = New System.Drawing.Size(786, 48)

        SetupButton(Me.btnAddLine, UITheme.Success, "➕  جديد", 681, 9, 95, 32, 0)
        SetupButton(Me.btnDeleteLine, UITheme.Danger, "🗑  حذف", 578, 9, 95, 32, 1)
        SetupButton(Me.btnRefreshLine, UITheme.TextMuted, "🔄  تحديث", 475, 9, 95, 32, 2)

        Me.pnlToolbarLines.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.btnAddLine, Me.btnDeleteLine, Me.btnRefreshLine
        })

        Me.dgvProductionLines.AllowUserToAddRows = False
        Me.dgvProductionLines.AllowUserToDeleteRows = False
        Me.dgvProductionLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvProductionLines.BackgroundColor = UITheme.Surface
        Me.dgvProductionLines.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvProductionLines.ColumnHeadersHeight = 38
        Me.dgvProductionLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProductionLines.Font = UITheme.FontNormal
        Me.dgvProductionLines.Location = New System.Drawing.Point(3, 159)
        Me.dgvProductionLines.MultiSelect = False
        Me.dgvProductionLines.Name = "dgvProductionLines"
        Me.dgvProductionLines.ReadOnly = True
        Me.dgvProductionLines.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvProductionLines.RowHeadersVisible = False
        Me.dgvProductionLines.RowTemplate.Height = 30
        Me.dgvProductionLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProductionLines.Size = New System.Drawing.Size(786, 404)

        Me.tabProductionLines.Controls.Add(Me.dgvProductionLines)
        Me.tabProductionLines.Controls.Add(Me.pnlToolbarLines)
        Me.tabProductionLines.Controls.Add(Me.pnlLineFields)

        Me.pnlLineFields.ResumeLayout(False)
        Me.pnlLineFields.PerformLayout()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  تهيئة تاب العمالة
    ' ═══════════════════════════════════════════════════════
    Private Sub InitLaborTab()
        Me.pnlToolbarLabor = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAddLabor = New System.Windows.Forms.Button()
        Me.btnEditLabor = New System.Windows.Forms.Button()
        Me.btnDeleteLabor = New System.Windows.Forms.Button()
        Me.btnRefreshLabor = New System.Windows.Forms.Button()
        Me.dgvLabor = New System.Windows.Forms.DataGridView()
        Me.pnlWorkerFields = New System.Windows.Forms.Panel()
        Me.lblWorkerFormMode = New System.Windows.Forms.Label()
        Me.lblWorkerID = New System.Windows.Forms.Label()
        Me.txtWorkerID = New System.Windows.Forms.TextBox()
        Me.lblWorkerCode = New System.Windows.Forms.Label()
        Me.txtWorkerCode = New System.Windows.Forms.TextBox()
        Me.lblWorkerNameAr = New System.Windows.Forms.Label()
        Me.txtWorkerNameAr = New System.Windows.Forms.TextBox()
        Me.lblWorkerNameEn = New System.Windows.Forms.Label()
        Me.txtWorkerNameEn = New System.Windows.Forms.TextBox()
        Me.lblWorkerRole = New System.Windows.Forms.Label()
        Me.cboWorkerRole = New System.Windows.Forms.ComboBox()
        Me.lblWorkerPhone = New System.Windows.Forms.Label()
        Me.txtWorkerPhone = New System.Windows.Forms.TextBox()
        Me.chkWorkerActive = New System.Windows.Forms.CheckBox()
        Me.btnSaveWorker = New System.Windows.Forms.Button()
        Me.btnCancelWorker = New System.Windows.Forms.Button()

        Me.pnlWorkerFields.SuspendLayout()

        Me.pnlWorkerFields.BackColor = UITheme.Surface
        Me.pnlWorkerFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlWorkerFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlWorkerFields.Location = New System.Drawing.Point(3, 3)
        Me.pnlWorkerFields.Name = "pnlWorkerFields"
        Me.pnlWorkerFields.Size = New System.Drawing.Size(786, 108)

        SetupLabel(Me.lblWorkerID, "الرقم", 740, 10, 40)
        SetupTextBox(Me.txtWorkerID, 740, 28, 40, True, True)
        SetupLabel(Me.lblWorkerCode, "كود العامل *", 575, 10, 160)
        SetupTextBox(Me.txtWorkerCode, 575, 28, 160)
        SetupLabel(Me.lblWorkerNameAr, "الاسم (عربي) *", 440, 10, 130)
        SetupTextBox(Me.txtWorkerNameAr, 440, 28, 130)
        SetupLabel(Me.lblWorkerNameEn, "الاسم (إنجليزي)", 305, 10, 130)
        SetupTextBox(Me.txtWorkerNameEn, 305, 28, 130)
        SetupLabel(Me.lblWorkerRole, "الوظيفة *", 180, 10, 120)
        SetupComboBox(Me.cboWorkerRole, 180, 28, 120)
        SetupLabel(Me.lblWorkerPhone, "رقم الهاتف", 55, 10, 120)
        SetupTextBox(Me.txtWorkerPhone, 55, 28, 120)
        SetupCheckBox(Me.chkWorkerActive, "نشط", 6, 29, 45)
        SetupButton(Me.btnSaveWorker, UITheme.Success, "💾  حفظ", 15, 62, 110, 34, 0)
        SetupButton(Me.btnCancelWorker, UITheme.TextMuted, "✖  إلغاء", 133, 62, 110, 34, 1)
        SetupBadge(Me.lblWorkerFormMode, "➕  وضع الإضافة", 521, 60, 250, 38, UITheme.Success)

        Me.pnlWorkerFields.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblWorkerID, Me.txtWorkerID,
            Me.lblWorkerCode, Me.txtWorkerCode,
            Me.lblWorkerNameAr, Me.txtWorkerNameAr,
            Me.lblWorkerNameEn, Me.txtWorkerNameEn,
            Me.lblWorkerRole, Me.cboWorkerRole,
            Me.lblWorkerPhone, Me.txtWorkerPhone,
            Me.chkWorkerActive,
            Me.btnSaveWorker, Me.btnCancelWorker,
            Me.lblWorkerFormMode
        })

        Me.pnlToolbarLabor.BackColor = UITheme.ButtonNormal
        Me.pnlToolbarLabor.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolbarLabor.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlToolbarLabor.Location = New System.Drawing.Point(3, 111)
        Me.pnlToolbarLabor.Name = "pnlToolbarLabor"
        Me.pnlToolbarLabor.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlToolbarLabor.Size = New System.Drawing.Size(786, 48)

        SetupButton(Me.btnAddLabor, UITheme.Success, "➕  جديد", 681, 9, 95, 32, 0)
        SetupButton(Me.btnDeleteLabor, UITheme.Danger, "🗑  حذف", 578, 9, 95, 32, 1)
        SetupButton(Me.btnRefreshLabor, UITheme.TextMuted, "🔄  تحديث", 475, 9, 95, 32, 2)

        Me.pnlToolbarLabor.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.btnAddLabor, Me.btnDeleteLabor, Me.btnRefreshLabor
        })

        Me.dgvLabor.AllowUserToAddRows = False
        Me.dgvLabor.AllowUserToDeleteRows = False
        Me.dgvLabor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLabor.BackgroundColor = UITheme.Surface
        Me.dgvLabor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvLabor.ColumnHeadersHeight = 38
        Me.dgvLabor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLabor.Font = UITheme.FontNormal
        Me.dgvLabor.Location = New System.Drawing.Point(3, 159)
        Me.dgvLabor.MultiSelect = False
        Me.dgvLabor.Name = "dgvLabor"
        Me.dgvLabor.ReadOnly = True
        Me.dgvLabor.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvLabor.RowHeadersVisible = False
        Me.dgvLabor.RowTemplate.Height = 30
        Me.dgvLabor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLabor.Size = New System.Drawing.Size(786, 404)

        Me.tabLabor.Controls.Add(Me.dgvLabor)
        Me.tabLabor.Controls.Add(Me.pnlToolbarLabor)
        Me.tabLabor.Controls.Add(Me.pnlWorkerFields)

        Me.pnlWorkerFields.ResumeLayout(False)
        Me.pnlWorkerFields.PerformLayout()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  تهيئة تاب الورديات
    ' ═══════════════════════════════════════════════════════
    Private Sub InitShiftsTab()
        ' تحميل UserControl الورديات
        Dim shiftsUC As New ProShiftsUC()
        shiftsUC.Dock = DockStyle.Fill
        Me.tabShifts.Controls.Add(shiftsUC)
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  ⭐ تهيئة تاب الوحدات (جديد - إضافة + حذف فقط)
    ' ═══════════════════════════════════════════════════════
    Private Sub InitUnitsTab()
        Me.pnlToolbarUnits = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAddUnit = New System.Windows.Forms.Button()
        Me.btnDeleteUnit = New System.Windows.Forms.Button()
        Me.btnRefreshUnit = New System.Windows.Forms.Button()
        Me.dgvUnits = New System.Windows.Forms.DataGridView()
        Me.pnlUnitFields = New System.Windows.Forms.Panel()
        Me.lblUnitFormMode = New System.Windows.Forms.Label()
        Me.lblUnitNameAr = New System.Windows.Forms.Label()
        Me.txtUnitNameAr = New System.Windows.Forms.TextBox()
        Me.lblUnitNameEn = New System.Windows.Forms.Label()
        Me.txtUnitNameEn = New System.Windows.Forms.TextBox()
        Me.btnSaveUnit = New System.Windows.Forms.Button()
        Me.btnCancelUnit = New System.Windows.Forms.Button()

        Me.pnlUnitFields.SuspendLayout()

        ' ── Panel إدخال البيانات ──
        Me.pnlUnitFields.BackColor = UITheme.Surface
        Me.pnlUnitFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlUnitFields.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnitFields.Location = New System.Drawing.Point(3, 3)
        Me.pnlUnitFields.Name = "pnlUnitFields"
        Me.pnlUnitFields.Size = New System.Drawing.Size(786, 108)

        SetupLabel(Me.lblUnitNameAr, "الاسم (عربي) *", 441, 10, 300)
        SetupTextBox(Me.txtUnitNameAr, 441, 28, 300)
        SetupLabel(Me.lblUnitNameEn, "الاسم (إنجليزي) *", 131, 10, 300)
        SetupTextBox(Me.txtUnitNameEn, 131, 28, 300)
        SetupButton(Me.btnSaveUnit, UITheme.Success, "💾  حفظ", 15, 62, 110, 34, 0)
        SetupButton(Me.btnCancelUnit, UITheme.TextMuted, "✖  تفريغ", 133, 62, 110, 34, 1)
        SetupBadge(Me.lblUnitFormMode, "➕  إضافة وحدة جديدة", 441, 60, 300, 38, UITheme.Success)

        Me.pnlUnitFields.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.lblUnitNameAr, Me.txtUnitNameAr,
            Me.lblUnitNameEn, Me.txtUnitNameEn,
            Me.btnSaveUnit, Me.btnCancelUnit,
            Me.lblUnitFormMode
        })

        ' ── Toolbar (إضافة / حذف / تحديث) ──
        Me.pnlToolbarUnits.BackColor = UITheme.ButtonNormal
        Me.pnlToolbarUnits.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolbarUnits.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlToolbarUnits.Location = New System.Drawing.Point(3, 111)
        Me.pnlToolbarUnits.Name = "pnlToolbarUnits"
        Me.pnlToolbarUnits.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlToolbarUnits.Size = New System.Drawing.Size(786, 48)

        SetupButton(Me.btnAddUnit, UITheme.Success, "➕  جديد", 681, 9, 95, 32, 0)
        SetupButton(Me.btnDeleteUnit, UITheme.Danger, "🗑  حذف", 578, 9, 95, 32, 1)
        SetupButton(Me.btnRefreshUnit, UITheme.TextMuted, "🔄  تحديث", 475, 9, 95, 32, 2)

        Me.pnlToolbarUnits.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.btnAddUnit, Me.btnDeleteUnit, Me.btnRefreshUnit
        })

        ' ── DataGridView ──
        Me.dgvUnits.AllowUserToAddRows = False
        Me.dgvUnits.AllowUserToDeleteRows = False
        Me.dgvUnits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUnits.BackgroundColor = UITheme.Surface
        Me.dgvUnits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUnits.ColumnHeadersHeight = 38
        Me.dgvUnits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUnits.Font = UITheme.FontNormal
        Me.dgvUnits.Location = New System.Drawing.Point(3, 159)
        Me.dgvUnits.MultiSelect = False
        Me.dgvUnits.Name = "dgvUnits"
        Me.dgvUnits.ReadOnly = True
        Me.dgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvUnits.RowHeadersVisible = False
        Me.dgvUnits.RowTemplate.Height = 30
        Me.dgvUnits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUnits.Size = New System.Drawing.Size(786, 404)

        ' تعريف الأعمدة
        Dim colUnitID As New System.Windows.Forms.DataGridViewTextBoxColumn() With {
            .Name = "colUnitID", .HeaderText = "م", .FillWeight = 10
        }
        Dim colUnitNameAr As New System.Windows.Forms.DataGridViewTextBoxColumn() With {
            .Name = "colUnitNameAr", .HeaderText = "الاسم (عربي)", .FillWeight = 45
        }
        Dim colUnitNameEn As New System.Windows.Forms.DataGridViewTextBoxColumn() With {
            .Name = "colUnitNameEn", .HeaderText = "الاسم (إنجليزي)", .FillWeight = 45
        }
        Me.dgvUnits.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {
            colUnitID, colUnitNameAr, colUnitNameEn
        })

        ' إضافة العناصر للتاب
        Me.tabUnits.Controls.Add(Me.dgvUnits)
        Me.tabUnits.Controls.Add(Me.pnlToolbarUnits)
        Me.tabUnits.Controls.Add(Me.pnlUnitFields)

        Me.pnlUnitFields.ResumeLayout(False)
        Me.pnlUnitFields.PerformLayout()
    End Sub

    ' ═══════════════════════════════════════════════════════
    '  تعريفات العناصر الرئيسية
    ' ═══════════════════════════════════════════════════════
    Friend WithEvents tabMasterData As System.Windows.Forms.TabControl
    Friend WithEvents tabItems As System.Windows.Forms.TabPage
    Friend WithEvents tabProductionLines As System.Windows.Forms.TabPage
    Friend WithEvents tabLabor As System.Windows.Forms.TabPage
    Friend WithEvents tabBOMHeader As System.Windows.Forms.TabPage
    Friend WithEvents tabBOMDetails As System.Windows.Forms.TabPage
    Friend WithEvents tabShifts As System.Windows.Forms.TabPage
    Friend WithEvents tabUnits As System.Windows.Forms.TabPage

    ' ═══════ الأصناف ═══════
    Friend WithEvents pnlToolbarItems As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnAddItem As System.Windows.Forms.Button
    Friend WithEvents btnEditItem As System.Windows.Forms.Button
    Friend WithEvents btnDeleteItem As System.Windows.Forms.Button
    Friend WithEvents btnRefreshItem As System.Windows.Forms.Button
    Friend WithEvents dgvItems As System.Windows.Forms.DataGridView
    Friend WithEvents pnlItemFields As System.Windows.Forms.Panel
    Friend WithEvents lblItemFormMode As System.Windows.Forms.Label
    Friend WithEvents lblItemID As System.Windows.Forms.Label
    Friend WithEvents txtItemID As System.Windows.Forms.TextBox
    Friend WithEvents lblItemCode As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents lblItemNameAr As System.Windows.Forms.Label
    Friend WithEvents txtItemNameAr As System.Windows.Forms.TextBox
    Friend WithEvents lblItemNameEn As System.Windows.Forms.Label
    Friend WithEvents txtItemNameEn As System.Windows.Forms.TextBox
    Friend WithEvents lblItemType As System.Windows.Forms.Label
    Friend WithEvents cboItemType As System.Windows.Forms.ComboBox
    Friend WithEvents lblInvUnit As System.Windows.Forms.Label
    Friend WithEvents cboInvUnit As System.Windows.Forms.ComboBox
    Friend WithEvents lblFinUnit As System.Windows.Forms.Label
    Friend WithEvents cboFinUnit As System.Windows.Forms.ComboBox
    Friend WithEvents chkItemActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveItem As System.Windows.Forms.Button
    Friend WithEvents btnCancelItem As System.Windows.Forms.Button

    ' ═══════ خطوط الإنتاج ═══════
    Friend WithEvents pnlToolbarLines As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnAddLine As System.Windows.Forms.Button
    Friend WithEvents btnEditLine As System.Windows.Forms.Button
    Friend WithEvents btnDeleteLine As System.Windows.Forms.Button
    Friend WithEvents btnRefreshLine As System.Windows.Forms.Button
    Friend WithEvents dgvProductionLines As System.Windows.Forms.DataGridView
    Friend WithEvents pnlLineFields As System.Windows.Forms.Panel
    Friend WithEvents lblLineFormMode As System.Windows.Forms.Label
    Friend WithEvents lblLineID As System.Windows.Forms.Label
    Friend WithEvents txtLineID As System.Windows.Forms.TextBox
    Friend WithEvents lblLineCode As System.Windows.Forms.Label
    Friend WithEvents txtLineCode As System.Windows.Forms.TextBox
    Friend WithEvents lblLineNameAr As System.Windows.Forms.Label
    Friend WithEvents txtLineNameAr As System.Windows.Forms.TextBox
    Friend WithEvents lblLineNameEn As System.Windows.Forms.Label
    Friend WithEvents txtLineNameEn As System.Windows.Forms.TextBox
    Friend WithEvents lblCycleTime As System.Windows.Forms.Label
    Friend WithEvents txtCycleTime As System.Windows.Forms.TextBox
    Friend WithEvents chkLineActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveLine As System.Windows.Forms.Button
    Friend WithEvents btnCancelLine As System.Windows.Forms.Button

    ' ═══════ العمالة ═══════
    Friend WithEvents pnlToolbarLabor As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnAddLabor As System.Windows.Forms.Button
    Friend WithEvents btnEditLabor As System.Windows.Forms.Button
    Friend WithEvents btnDeleteLabor As System.Windows.Forms.Button
    Friend WithEvents btnRefreshLabor As System.Windows.Forms.Button
    Friend WithEvents dgvLabor As System.Windows.Forms.DataGridView
    Friend WithEvents pnlWorkerFields As System.Windows.Forms.Panel
    Friend WithEvents lblWorkerFormMode As System.Windows.Forms.Label
    Friend WithEvents lblWorkerID As System.Windows.Forms.Label
    Friend WithEvents txtWorkerID As System.Windows.Forms.TextBox
    Friend WithEvents lblWorkerCode As System.Windows.Forms.Label
    Friend WithEvents txtWorkerCode As System.Windows.Forms.TextBox
    Friend WithEvents lblWorkerNameAr As System.Windows.Forms.Label
    Friend WithEvents txtWorkerNameAr As System.Windows.Forms.TextBox
    Friend WithEvents lblWorkerNameEn As System.Windows.Forms.Label
    Friend WithEvents txtWorkerNameEn As System.Windows.Forms.TextBox
    Friend WithEvents lblWorkerRole As System.Windows.Forms.Label
    Friend WithEvents cboWorkerRole As System.Windows.Forms.ComboBox
    Friend WithEvents lblWorkerPhone As System.Windows.Forms.Label
    Friend WithEvents txtWorkerPhone As System.Windows.Forms.TextBox
    Friend WithEvents chkWorkerActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveWorker As System.Windows.Forms.Button
    Friend WithEvents btnCancelWorker As System.Windows.Forms.Button

    ' ═══════ الورديات ═══════
    Friend WithEvents pnlToolbarShifts As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnAddShift As System.Windows.Forms.Button
    Friend WithEvents btnEditShift As System.Windows.Forms.Button
    Friend WithEvents btnDeleteShift As System.Windows.Forms.Button
    Friend WithEvents btnRefreshShift As System.Windows.Forms.Button
    Friend WithEvents dgvShifts As System.Windows.Forms.DataGridView

    ' ═══════ الوحدات (جديد) ═══════
    Friend WithEvents pnlToolbarUnits As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnAddUnit As System.Windows.Forms.Button
    Friend WithEvents btnDeleteUnit As System.Windows.Forms.Button
    Friend WithEvents btnRefreshUnit As System.Windows.Forms.Button
    Friend WithEvents dgvUnits As System.Windows.Forms.DataGridView
    Friend WithEvents pnlUnitFields As System.Windows.Forms.Panel
    Friend WithEvents lblUnitFormMode As System.Windows.Forms.Label
    Friend WithEvents lblUnitNameAr As System.Windows.Forms.Label
    Friend WithEvents txtUnitNameAr As System.Windows.Forms.TextBox
    Friend WithEvents lblUnitNameEn As System.Windows.Forms.Label
    Friend WithEvents txtUnitNameEn As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveUnit As System.Windows.Forms.Button
    Friend WithEvents btnCancelUnit As System.Windows.Forms.Button
    ' حقول الأوزان (جديد)
    Friend WithEvents lblNetWeight As System.Windows.Forms.Label
    Friend WithEvents txtNetWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblPackagingWeight As System.Windows.Forms.Label
    Friend WithEvents txtPackagingWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblGrossWeight As System.Windows.Forms.Label
    Friend WithEvents toolTipItems As System.Windows.Forms.ToolTip
    Friend WithEvents txtGrossWeight As System.Windows.Forms.TextBox

    ' ═══════ لوحة البحث في الأصناف (جديد) ═══════
    Friend WithEvents pnlItemSearch As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearchType As System.Windows.Forms.Label
    Friend WithEvents cboSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClearSearch As System.Windows.Forms.Button
    Friend WithEvents lblResultCount As System.Windows.Forms.Label

End Class