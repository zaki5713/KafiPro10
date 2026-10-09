Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports KafiPro.UI.Shared.Themes
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories
Imports Microsoft.Data.SqlClient

Partial Public Class ProMasterDataUC

    ' ═══════════════════════════════════════════════════════════
    '  Repos
    ' ═══════════════════════════════════════════════════════════
    Private ReadOnly _itemRepo As New ItemRepository()
    Private ReadOnly _lineRepo As New ProductionLineRepository()
    Private ReadOnly _workerRepo As New ProductionWorkerRepository()
    Private ReadOnly _bomRepo As New BOMRepository()

    ' ═══════════════════════════════════════════════════════════
    '  حالة الصفحات
    ' ═══════════════════════════════════════════════════════════
    Private _currentItemId As Integer = 0
    Private _loadingItems As Boolean = False
    Private _itemLookupsLoaded As Boolean = False

    Private _currentLineId As Integer = 0
    Private _loadingLines As Boolean = False

    Private _currentWorkerId As Integer = 0
    Private _loadingWorkers As Boolean = False

    Private _currentBOMId As Integer = 0
    Private _currentBOMDetailId As Integer = 0
    Private _loadingBOMs As Boolean = False
    Private _loadingBOMDetails As Boolean = False
    Private _loadingSelector As Boolean = False
    Private _bomLookupsLoaded As Boolean = False

    Private _currentUnitID As Integer = 0
    Private _loadingUnits As Boolean = False

    ' بحث الأصناف
    Private _allItems As List(Of ItemDto) = New List(Of ItemDto)()

    ' ═══════════════════════════════════════════════════════════
    '  التحميل الأولي
    ' ═══════════════════════════════════════════════════════════
    Private Sub ProMasterDataUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' الأصناف
        InitializeItemsGrid() : LoadItemLookups() : LoadItems() : ResetItemForm()

        ' الخطوط
        InitializeLinesGrid() : LoadLines() : ResetLineForm()

        ' العمالة
        InitializeWorkersGrid() : LoadWorkerRoles() : LoadWorkers() : ResetWorkerForm()

        ' الخلطات (Header + Details)
        InitializeBOMGrid()
        InitializeBOMDetailsGrid()
        LoadBOMLookups()
        LoadBOMs()
        LoadBOMSelectorCombo()
        ResetBOMForm()
        ResetBOMDetailForm()

        ' الوحدات
        InitializeUnitsGrid() : LoadUnits() : ResetUnitForm()

        ApplyThemeToGrids()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  ═══════════════════ الأصناف ═══════════════════════════════
    ' ═══════════════════════════════════════════════════════════
    Private Sub InitializeItemsGrid()
        With dgvItems
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemCode", .HeaderText = "كود الصنف", .FillWeight = 13})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemNameAr", .HeaderText = "الاسم عربي", .FillWeight = 24})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemNameEn", .HeaderText = "الاسم إنجليزي", .FillWeight = 20})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ItemTypeNameAr", .HeaderText = "النوع", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "InventoryUnitName", .HeaderText = "وحدة المخزون", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "FinancialUnitName", .HeaderText = "الوحدة المالية", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "NetWeight", .HeaderText = "الوزن الصافي", .FillWeight = 8})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "PackageWeight", .HeaderText = "وزن التغليف", .FillWeight = 8})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "GrossWeight", .HeaderText = "الوزن القائم", .FillWeight = 8})
        End With
    End Sub

    Private Sub LoadItemLookups()
        If _itemLookupsLoaded Then Return
        Try
            cboItemType.DataSource = _itemRepo.GetItemTypes()
            cboItemType.DisplayMember = "ItemTypeNameAr"
            cboItemType.ValueMember = "ItemTypeID"

            cboInvUnit.DataSource = _itemRepo.GetUnits()
            cboInvUnit.DisplayMember = "UnitNameAr" : cboInvUnit.ValueMember = "UnitOfMeasureID"

            cboFinUnit.DataSource = _itemRepo.GetUnits()
            cboFinUnit.DisplayMember = "UnitNameAr" : cboFinUnit.ValueMember = "UnitOfMeasureID"

            ' ═══ بحث: قائمة أنواع الأصناف ═══
            Dim types As New List(Of KeyValuePair(Of Integer, String))()
            types.Add(New KeyValuePair(Of Integer, String)(0, "— الكل —"))

            Dim itemTypes = _itemRepo.GetItemTypes()
            For Each it In itemTypes
                types.Add(New KeyValuePair(Of Integer, String)(it.ItemTypeID, it.ItemTypeNameAr))
            Next

            cboSearchType.DataSource = types
            cboSearchType.DisplayMember = "Value"
            cboSearchType.ValueMember = "Key"
            cboSearchType.SelectedIndex = 0

            _itemLookupsLoaded = True
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل القوائم: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadItems(Optional ByVal selectItemId As Integer = 0)
        Try
            _loadingItems = True
            Cursor = Cursors.WaitCursor

            ' تحميل القائمة الكاملة وحفظها
            _allItems = _itemRepo.GetAll()

            ' عرض القائمة (مع تطبيق الفلاتر)
            ApplyItemsFilter(selectItemId)

        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الأصناف: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            _loadingItems = False
        End Try
    End Sub

    ' ═══════ تطبيق الفلتر على القائمة ═══════
    Private Sub ApplyItemsFilter(Optional ByVal selectItemId As Integer = 0)
        Try
            ' قراءة معايير البحث
            Dim searchText As String = ""
            If txtSearch IsNot Nothing AndAlso txtSearch.Text IsNot Nothing Then
                searchText = txtSearch.Text.Trim().ToLower()
            End If

            Dim typeId As Integer = 0
            If cboSearchType IsNot Nothing AndAlso cboSearchType.SelectedValue IsNot Nothing Then
                Try
                    typeId = CInt(cboSearchType.SelectedValue)
                Catch
                    typeId = 0
                End Try
            End If

            ' فلترة القائمة
            Dim filtered As IEnumerable(Of ItemDto) = _allItems

            ' فلترة بالنص
            If Not String.IsNullOrEmpty(searchText) Then
                filtered = filtered.Where(Function(x) _
                    (x.ItemCode IsNot Nothing AndAlso x.ItemCode.ToLower().Contains(searchText)) OrElse
                    (x.ItemNameAr IsNot Nothing AndAlso x.ItemNameAr.ToLower().Contains(searchText)) OrElse
                    (x.ItemNameEn IsNot Nothing AndAlso x.ItemNameEn.ToLower().Contains(searchText)))
            End If

            ' فلترة بالنوع
            If typeId > 0 Then
                filtered = filtered.Where(Function(x) x.ItemTypeID = typeId)
            End If

            ' تحديث الجدول
            dgvItems.Rows.Clear()
            For Each it As ItemDto In filtered
                dgvItems.Rows.Add(
                    it.ItemID, it.ItemCode, it.ItemNameAr, it.ItemNameEn,
                    it.ItemTypeNameAr, it.InventoryUnitName, it.FinancialUnitName,
                    If(it.NetWeight.HasValue, it.NetWeight.Value.ToString("N3"), "0.000"),
                    If(it.PackageWeight.HasValue, it.PackageWeight.Value.ToString("N3"), "0.000"),
                    If(it.GrossWeight.HasValue, it.GrossWeight.Value.ToString("N3"), "0.000")
                )
            Next

            ' تحديث عدد النتائج
            If lblResultCount IsNot Nothing Then
                lblResultCount.Text = $"📋  النتائج: {dgvItems.Rows.Count}"
            End If

            ' تحديد الصنف المطلوب
            If selectItemId > 0 Then
                For Each row As DataGridViewRow In dgvItems.Rows
                    If CInt(row.Cells("ItemID").Value) = selectItemId Then row.Selected = True : Exit For
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في الفلترة: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadItemIntoForm(itemId As Integer)
        Try
            Dim item As ItemDto = _itemRepo.GetById(itemId)
            If item Is Nothing Then Return
            _currentItemId = item.ItemID
            txtItemID.Text = item.ItemID.ToString()
            txtItemCode.Text = item.ItemCode
            txtItemNameAr.Text = item.ItemNameAr
            txtItemNameEn.Text = item.ItemNameEn
            cboItemType.SelectedValue = item.ItemTypeID
            cboInvUnit.SelectedValue = item.InventoryUnitID
            cboFinUnit.SelectedValue = item.FinancialUnitID
            chkItemActive.Checked = item.IsActive

            ' الأوزان
            If item.NetWeight.HasValue Then txtNetWeight.Text = item.NetWeight.Value.ToString("0.###") Else txtNetWeight.Text = ""
            If item.PackageWeight.HasValue Then txtPackagingWeight.Text = item.PackageWeight.Value.ToString("0.###") Else txtPackagingWeight.Text = ""
            If item.GrossWeight.HasValue Then txtGrossWeight.Text = item.GrossWeight.Value.ToString("0.###") Else txtGrossWeight.Text = ""

            lblItemFormMode.Text = "✏️  وضع التعديل — ID: " & itemId
            lblItemFormMode.BackColor = UITheme.Primary : lblItemFormMode.ForeColor = UITheme.TextWhite
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetItemForm()
        _currentItemId = 0
        txtItemID.Clear()
        txtItemCode.Clear()
        txtItemNameAr.Clear()
        txtItemNameEn.Clear()
        txtNetWeight.Clear()
        txtPackagingWeight.Clear()
        txtGrossWeight.Clear()
        If cboItemType.Items.Count > 0 Then cboItemType.SelectedIndex = 0
        If cboInvUnit.Items.Count > 0 Then cboInvUnit.SelectedIndex = 0
        If cboFinUnit.Items.Count > 0 Then cboFinUnit.SelectedIndex = 0
        chkItemActive.Checked = True
        lblItemFormMode.Text = "➕  وضع الإضافة"
        lblItemFormMode.BackColor = UITheme.Success : lblItemFormMode.ForeColor = UITheme.TextWhite
        dgvItems.ClearSelection() : txtItemCode.Focus()
    End Sub

    Private Sub dgvItems_SelectionChanged(sender As Object, e As EventArgs) Handles dgvItems.SelectionChanged
        If _loadingItems Then Return
        If dgvItems.SelectedRows.Count = 0 Then Return
        LoadItemIntoForm(CInt(dgvItems.SelectedRows(0).Cells("ItemID").Value))
    End Sub

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        ResetItemForm()
    End Sub

    Private Sub btnSaveItem_Click(sender As Object, e As EventArgs) Handles btnSaveItem.Click
        If String.IsNullOrWhiteSpace(txtItemCode.Text) Then Msg("كود الصنف مطلوب.", txtItemCode) : Return
        If String.IsNullOrWhiteSpace(txtItemNameAr.Text) Then Msg("الاسم العربي مطلوب.", txtItemNameAr) : Return
        If String.IsNullOrWhiteSpace(txtItemNameEn.Text) Then Msg("الاسم الإنجليزي مطلوب.", txtItemNameEn) : Return
        If cboItemType.SelectedValue Is Nothing Then Msg("نوع الصنف مطلوب.") : Return
        If cboInvUnit.SelectedValue Is Nothing Then Msg("وحدة المخزون مطلوبة.") : Return
        If cboFinUnit.SelectedValue Is Nothing Then Msg("الوحدة المالية مطلوبة.") : Return

        ' ═══ تحليل الأوزان ═══
        Dim netW As Decimal? = Nothing
        If Not String.IsNullOrWhiteSpace(txtNetWeight.Text) Then
            Dim parsed As Decimal
            If Not Decimal.TryParse(txtNetWeight.Text, parsed) Then Msg("الوزن الصافي يجب أن يكون رقمًا.", txtNetWeight) : Return
            If parsed < 0 Then Msg("الوزن الصافي لا يمكن أن يكون بالسالب.", txtNetWeight) : Return
            netW = parsed
        End If

        Dim packW As Decimal? = Nothing
        If Not String.IsNullOrWhiteSpace(txtPackagingWeight.Text) Then
            Dim parsed As Decimal
            If Not Decimal.TryParse(txtPackagingWeight.Text, parsed) Then Msg("وزن التغليف يجب أن يكون رقمًا.", txtPackagingWeight) : Return
            If parsed < 0 Then Msg("وزن التغليف لا يمكن أن يكون بالسالب.", txtPackagingWeight) : Return
            packW = parsed
        End If

        Dim model As New ItemDto With {
            .ItemID = _currentItemId,
            .ItemCode = txtItemCode.Text.Trim(),
            .ItemNameAr = txtItemNameAr.Text.Trim(),
            .ItemNameEn = txtItemNameEn.Text.Trim(),
            .ItemTypeID = CInt(cboItemType.SelectedValue),
            .InventoryUnitID = CInt(cboInvUnit.SelectedValue),
            .FinancialUnitID = CInt(cboFinUnit.SelectedValue),
            .IsActive = chkItemActive.Checked,
            .NetWeight = netW,
            .PackageWeight = packW
        }

        Try
            If _currentItemId = 0 Then
                Dim newId As Integer = _itemRepo.Insert(model, CurrentUserID())
                MessageBox.Show("تم إضافة الصنف بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadItems(newId) : LoadItemIntoForm(newId)
            Else
                _itemRepo.Update(model, CurrentUserID())
                MessageBox.Show("تم تعديل الصنف بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadItems(_currentItemId) : LoadItemIntoForm(_currentItemId)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══ حساب الوزن القائم تلقائيًا ═══
    Private Sub Weight_TextChanged(sender As Object, e As EventArgs) _
        Handles txtNetWeight.TextChanged, txtPackagingWeight.TextChanged

        Try
            Dim nw As Decimal = 0
            Dim pw As Decimal = 0

            If Not String.IsNullOrWhiteSpace(txtNetWeight.Text) Then
                Decimal.TryParse(txtNetWeight.Text, nw)
            End If

            If Not String.IsNullOrWhiteSpace(txtPackagingWeight.Text) Then
                Decimal.TryParse(txtPackagingWeight.Text, pw)
            End If

            Dim gross As Decimal = nw + pw
            If gross > 0 Then
                txtGrossWeight.Text = gross.ToString("0.###")
            Else
                txtGrossWeight.Text = ""
            End If
        Catch
            ' تجاهل
        End Try
    End Sub

    Private Sub btnCancelItem_Click(sender As Object, e As EventArgs) Handles btnCancelItem.Click
        ResetItemForm()
    End Sub

    Private Sub btnDeleteItem_Click(sender As Object, e As EventArgs) Handles btnDeleteItem.Click
        If _currentItemId = 0 Then Msg("اختر صنفًا من الجدول أولاً.") : Return
        If MessageBox.Show($"هل أنت متأكد من حذف الصنف: {txtItemNameAr.Text}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            _itemRepo.Delete(_currentItemId, CurrentUserID())
            MessageBox.Show("تم حذف الصنف بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadItems() : ResetItemForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshItem_Click(sender As Object, e As EventArgs) Handles btnRefreshItem.Click
        Dim keepId As Integer = _currentItemId
        LoadItems(keepId)
        If keepId > 0 Then LoadItemIntoForm(keepId) Else ResetItemForm()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  أحداث البحث
    ' ═══════════════════════════════════════════════════════════

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ApplyItemsFilter(_currentItemId)
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Clear()
        If cboSearchType.Items.Count > 0 Then cboSearchType.SelectedIndex = 0
        ApplyItemsFilter(_currentItemId)
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _loadingItems Then Return
        If Not _itemLookupsLoaded Then Return
        ApplyItemsFilter(_currentItemId)
    End Sub

    Private Sub cboSearchType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearchType.SelectedIndexChanged
        If _loadingItems Then Return
        If Not _itemLookupsLoaded Then Return
        ApplyItemsFilter(_currentItemId)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            ApplyItemsFilter(_currentItemId)
            e.SuppressKeyPress = True
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  ═══════════════════ خطوط الإنتاج ══════════════════════════
    ' ═══════════════════════════════════════════════════════════
    Private Sub InitializeLinesGrid()
        With dgvProductionLines
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineCode", .HeaderText = "كود الخط", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineNameAr", .HeaderText = "الاسم عربي", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineNameEn", .HeaderText = "الاسم إنجليزي", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "CycleTime", .HeaderText = "زمن الدورة", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Active", .HeaderText = "نشط", .FillWeight = 10})
        End With
    End Sub

    Private Sub LoadLines(Optional ByVal selectLineId As Integer = 0)
        Try
            _loadingLines = True
            Dim lines As List(Of ProductionLineDto) = _lineRepo.GetAll()
            dgvProductionLines.Rows.Clear()
            For Each ln As ProductionLineDto In lines
                Dim cycleText As String = "-"
                If ln.IdealCycleTime.HasValue Then cycleText = ln.IdealCycleTime.Value.ToString("N3")
                Dim activeText As String = "✗"
                If ln.IsActive Then activeText = "✓"
                dgvProductionLines.Rows.Add(ln.ProductionLineID, ln.LineCode, ln.LineNameAr, ln.LineNameEn, cycleText, activeText)
            Next
            If selectLineId > 0 Then
                For Each row As DataGridViewRow In dgvProductionLines.Rows
                    If CInt(row.Cells("LineID").Value) = selectLineId Then row.Selected = True : Exit For
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الخطوط: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingLines = False
        End Try
    End Sub

    Private Sub LoadLineIntoForm(lineId As Integer)
        Try
            Dim ln As ProductionLineDto = _lineRepo.GetById(lineId)
            If ln Is Nothing Then Return
            _currentLineId = ln.ProductionLineID
            txtLineID.Text = ln.ProductionLineID.ToString()
            txtLineCode.Text = ln.LineCode : txtLineNameAr.Text = ln.LineNameAr : txtLineNameEn.Text = ln.LineNameEn
            If ln.IdealCycleTime.HasValue Then txtCycleTime.Text = ln.IdealCycleTime.Value.ToString("0.###") Else txtCycleTime.Text = ""
            chkLineActive.Checked = ln.IsActive
            lblLineFormMode.Text = "✏️  وضع التعديل — ID: " & lineId
            lblLineFormMode.BackColor = UITheme.Primary : lblLineFormMode.ForeColor = UITheme.TextWhite
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetLineForm()
        _currentLineId = 0
        txtLineID.Clear() : txtLineCode.Clear() : txtLineNameAr.Clear() : txtLineNameEn.Clear() : txtCycleTime.Clear()
        chkLineActive.Checked = True
        lblLineFormMode.Text = "➕  وضع الإضافة"
        lblLineFormMode.BackColor = UITheme.Success : lblLineFormMode.ForeColor = UITheme.TextWhite
        dgvProductionLines.ClearSelection() : txtLineCode.Focus()
    End Sub

    Private Sub dgvProductionLines_SelectionChanged(sender As Object, e As EventArgs) Handles dgvProductionLines.SelectionChanged
        If _loadingLines Then Return
        If dgvProductionLines.SelectedRows.Count = 0 Then Return
        LoadLineIntoForm(CInt(dgvProductionLines.SelectedRows(0).Cells("LineID").Value))
    End Sub

    Private Sub btnAddLine_Click(sender As Object, e As EventArgs) Handles btnAddLine.Click
        ResetLineForm()
    End Sub

    Private Sub btnSaveLine_Click(sender As Object, e As EventArgs) Handles btnSaveLine.Click
        If String.IsNullOrWhiteSpace(txtLineCode.Text) Then Msg("كود الخط مطلوب.", txtLineCode) : Return
        If String.IsNullOrWhiteSpace(txtLineNameAr.Text) Then Msg("الاسم العربي مطلوب.", txtLineNameAr) : Return

        Dim cycleTime As Decimal? = Nothing
        If Not String.IsNullOrWhiteSpace(txtCycleTime.Text) Then
            Dim parsed As Decimal
            If Not Decimal.TryParse(txtCycleTime.Text, parsed) Then Msg("زمن الدورة يجب أن يكون رقمًا.", txtCycleTime) : Return
            cycleTime = parsed
        End If

        Dim model As New ProductionLineDto With {
            .ProductionLineID = _currentLineId,
            .LineCode = txtLineCode.Text.Trim(),
            .LineNameAr = txtLineNameAr.Text.Trim(),
            .LineNameEn = txtLineNameEn.Text.Trim(),
            .IdealCycleTime = cycleTime,
            .IsActive = chkLineActive.Checked
        }

        Try
            If _currentLineId = 0 Then
                Dim newId As Integer = _lineRepo.Insert(model, CurrentUserID())
                MessageBox.Show("تم إضافة الخط بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadLines(newId) : LoadLineIntoForm(newId)
            Else
                _lineRepo.Update(model, CurrentUserID())
                MessageBox.Show("تم تعديل الخط بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadLines(_currentLineId) : LoadLineIntoForm(_currentLineId)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancelLine_Click(sender As Object, e As EventArgs) Handles btnCancelLine.Click
        ResetLineForm()
    End Sub

    Private Sub btnDeleteLine_Click(sender As Object, e As EventArgs) Handles btnDeleteLine.Click
        If _currentLineId = 0 Then Msg("اختر خطًا من الجدول أولاً.") : Return
        If MessageBox.Show($"هل أنت متأكد من حذف الخط: {txtLineNameAr.Text}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            _lineRepo.Delete(_currentLineId, CurrentUserID())
            MessageBox.Show("تم حذف الخط بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadLines() : ResetLineForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshLine_Click(sender As Object, e As EventArgs) Handles btnRefreshLine.Click
        Dim keepId As Integer = _currentLineId
        LoadLines(keepId)
        If keepId > 0 Then LoadLineIntoForm(keepId) Else ResetLineForm()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  ═══════════════════ العمالة ═══════════════════════════════
    ' ═══════════════════════════════════════════════════════════
    Private Sub InitializeWorkersGrid()
        With dgvLabor
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WorkerID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WorkerCode", .HeaderText = "كود العامل", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WorkerNameAr", .HeaderText = "الاسم عربي", .FillWeight = 22})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "WorkerNameEn", .HeaderText = "الاسم إنجليزي", .FillWeight = 22})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Role", .HeaderText = "الوظيفة", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Phone", .HeaderText = "الهاتف", .FillWeight = 16})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Active", .HeaderText = "نشط", .FillWeight = 10})
        End With
    End Sub

    Private Sub LoadWorkerRoles()
        ' ═══ إنشاء قائمة الأدوار ═══
        Dim roles As New List(Of KeyValuePair(Of String, String))()
        roles.Add(New KeyValuePair(Of String, String)("Supervisor", "مشرف"))
        roles.Add(New KeyValuePair(Of String, String)("Operator", "مشغل"))
        roles.Add(New KeyValuePair(Of String, String)("Technician", "فني"))
        roles.Add(New KeyValuePair(Of String, String)("Worker", "عامل"))
        roles.Add(New KeyValuePair(Of String, String)("Helper", "مساعد"))

        ' ═══ ربط الـ ComboBox بـ DataSource ═══
        cboWorkerRole.DataSource = Nothing
        cboWorkerRole.DisplayMember = "Value"
        cboWorkerRole.ValueMember = "Key"
        cboWorkerRole.DataSource = roles

        cboWorkerRole.DropDownStyle = ComboBoxStyle.DropDownList
        cboWorkerRole.SelectedIndex = 0
    End Sub

    Private Function GetRoleArabicName(role As String) As String
        Select Case role
            Case "Supervisor" : Return "مشرف"
            Case "Operator" : Return "مشغل"
            Case "Technician" : Return "فني"
            Case "Worker" : Return "عامل"
            Case "Helper" : Return "مساعد"
            Case Else : Return role
        End Select
    End Function

    Private Sub LoadWorkers(Optional ByVal selectWorkerId As Integer = 0)
        Try
            _loadingWorkers = True
            Dim workers As List(Of ProductionWorkerDto) = _workerRepo.GetAll()
            dgvLabor.Rows.Clear()
            For Each w As ProductionWorkerDto In workers
                Dim activeText As String = "✗"
                If w.IsActive Then activeText = "✓"
                dgvLabor.Rows.Add(w.WorkerID, w.WorkerCode, w.WorkerNameAr, w.WorkerNameEn,
                                  GetRoleArabicName(w.ProductionRole),
                                  If(String.IsNullOrEmpty(w.PhoneNumber), "-", w.PhoneNumber), activeText)
            Next
            If selectWorkerId > 0 Then
                For Each row As DataGridViewRow In dgvLabor.Rows
                    If CInt(row.Cells("WorkerID").Value) = selectWorkerId Then row.Selected = True : Exit For
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل العمالة: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingWorkers = False
        End Try
    End Sub

    Private Sub LoadWorkerIntoForm(workerId As Integer)
        Try
            Dim w As ProductionWorkerDto = _workerRepo.GetById(workerId)
            If w Is Nothing Then Return
            _currentWorkerId = w.WorkerID
            txtWorkerID.Text = w.WorkerID.ToString()
            txtWorkerCode.Text = w.WorkerCode : txtWorkerNameAr.Text = w.WorkerNameAr : txtWorkerNameEn.Text = w.WorkerNameEn
            cboWorkerRole.SelectedValue = w.ProductionRole
            txtWorkerPhone.Text = w.PhoneNumber
            chkWorkerActive.Checked = w.IsActive
            lblWorkerFormMode.Text = "✏️  وضع التعديل — ID: " & workerId
            lblWorkerFormMode.BackColor = UITheme.Primary : lblWorkerFormMode.ForeColor = UITheme.TextWhite
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetWorkerForm()
        _currentWorkerId = 0
        txtWorkerID.Clear() : txtWorkerCode.Clear() : txtWorkerNameAr.Clear() : txtWorkerNameEn.Clear() : txtWorkerPhone.Clear()
        If cboWorkerRole.Items.Count > 0 Then cboWorkerRole.SelectedIndex = 0
        chkWorkerActive.Checked = True
        lblWorkerFormMode.Text = "➕  وضع الإضافة"
        lblWorkerFormMode.BackColor = UITheme.Success : lblWorkerFormMode.ForeColor = UITheme.TextWhite
        dgvLabor.ClearSelection() : txtWorkerCode.Focus()
    End Sub

    Private Sub dgvLabor_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLabor.SelectionChanged
        If _loadingWorkers Then Return
        If dgvLabor.SelectedRows.Count = 0 Then Return
        LoadWorkerIntoForm(CInt(dgvLabor.SelectedRows(0).Cells("WorkerID").Value))
    End Sub

    Private Sub btnAddLabor_Click(sender As Object, e As EventArgs) Handles btnAddLabor.Click
        ResetWorkerForm()
    End Sub

    Private Sub btnSaveWorker_Click(sender As Object, e As EventArgs) Handles btnSaveWorker.Click
        If String.IsNullOrWhiteSpace(txtWorkerCode.Text) Then Msg("كود العامل مطلوب.", txtWorkerCode) : Return
        If String.IsNullOrWhiteSpace(txtWorkerNameAr.Text) Then Msg("الاسم العربي مطلوب.", txtWorkerNameAr) : Return
        If cboWorkerRole.SelectedValue Is Nothing Then Msg("الوظيفة مطلوبة.") : Return

        Dim model As New ProductionWorkerDto With {
            .WorkerID = _currentWorkerId,
            .WorkerCode = txtWorkerCode.Text.Trim(),
            .WorkerNameAr = txtWorkerNameAr.Text.Trim(),
            .WorkerNameEn = txtWorkerNameEn.Text.Trim(),
            .ProductionRole = CStr(cboWorkerRole.SelectedValue),
            .PhoneNumber = txtWorkerPhone.Text.Trim(),
            .IsActive = chkWorkerActive.Checked
        }

        Try
            If _currentWorkerId = 0 Then
                Dim newId As Integer = _workerRepo.Insert(model, CurrentUserID())
                MessageBox.Show("تم إضافة العامل بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadWorkers(newId) : LoadWorkerIntoForm(newId)
            Else
                _workerRepo.Update(model, CurrentUserID())
                MessageBox.Show("تم تعديل العامل بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadWorkers(_currentWorkerId) : LoadWorkerIntoForm(_currentWorkerId)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancelWorker_Click(sender As Object, e As EventArgs) Handles btnCancelWorker.Click
        ResetWorkerForm()
    End Sub

    Private Sub btnDeleteLabor_Click(sender As Object, e As EventArgs) Handles btnDeleteLabor.Click
        If _currentWorkerId = 0 Then Msg("اختر عاملًا من الجدول أولاً.") : Return
        If MessageBox.Show($"هل أنت متأكد من حذف العامل: {txtWorkerNameAr.Text}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            _workerRepo.Delete(_currentWorkerId, CurrentUserID())
            MessageBox.Show("تم حذف العامل بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadWorkers() : ResetWorkerForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshLabor_Click(sender As Object, e As EventArgs) Handles btnRefreshLabor.Click
        Dim keepId As Integer = _currentWorkerId
        LoadWorkers(keepId)
        If keepId > 0 Then LoadWorkerIntoForm(keepId) Else ResetWorkerForm()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  ═══════════════════ الخلطات (BOM Header) ═════════════════
    ' ═══════════════════════════════════════════════════════════

    Private Sub InitializeBOMGrid()
        With dgvBOM
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BOMID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BOMCode", .HeaderText = "كود الخلطة", .FillWeight = 12})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "FinishedItemNameAr", .HeaderText = "المنتج النهائي", .FillWeight = 25})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "OutputQty", .HeaderText = "الكمية", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "OutputUnitName", .HeaderText = "الوحدة", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Wastage", .HeaderText = "الفقد %", .FillWeight = 10})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "DetailsCount", .HeaderText = "عدد المكونات", .FillWeight = 13})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Active", .HeaderText = "نشط", .FillWeight = 10})
        End With
    End Sub

    Private Sub LoadBOMs(Optional ByVal selectBomId As Integer = 0)
        Try
            _loadingBOMs = True
            Dim boms As List(Of BOMDto) = _bomRepo.GetAll()
            dgvBOM.Rows.Clear()
            For Each b As BOMDto In boms
                Dim activeText As String = "✗"
                If b.IsActive Then activeText = "✓"
                dgvBOM.Rows.Add(b.BOMID, b.BOMCode, b.FinishedItemNameAr, b.OutputQty.ToString("N3"),
                                b.OutputUnitName, b.WastagePercent.ToString("N2") & " %",
                                b.DetailsCount, activeText)
            Next
            If selectBomId > 0 Then
                For Each row As DataGridViewRow In dgvBOM.Rows
                    If CInt(row.Cells("BOMID").Value) = selectBomId Then row.Selected = True : Exit For
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الخلطات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingBOMs = False
        End Try
    End Sub

    Private Sub LoadBOMIntoForm(bomId As Integer)
        Try
            Dim b As BOMDto = _bomRepo.GetById(bomId)
            If b Is Nothing Then Return
            _currentBOMId = b.BOMID
            txtBOMID.Text = b.BOMID.ToString()
            txtBOMCode.Text = b.BOMCode
            cboFinishedItem.SelectedValue = b.FinishedItemID
            txtOutputQty.Text = b.OutputQty.ToString("0.###")
            cboOutputUnit.SelectedValue = b.OutputUnitID
            txtWastage.Text = b.WastagePercent.ToString("0.##")
            chkBOMActive.Checked = b.IsActive

            lblBOMFormMode.Text = "✏️  وضع التعديل — ID: " & bomId
            lblBOMFormMode.BackColor = UITheme.Primary : lblBOMFormMode.ForeColor = UITheme.TextWhite
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetBOMForm()
        _currentBOMId = 0
        txtBOMID.Clear() : txtBOMCode.Clear() : txtOutputQty.Clear() : txtWastage.Clear()
        If cboFinishedItem.Items.Count > 0 Then cboFinishedItem.SelectedIndex = 0
        If cboOutputUnit.Items.Count > 0 Then cboOutputUnit.SelectedIndex = 0
        chkBOMActive.Checked = True
        lblBOMFormMode.Text = "➕  وضع الإضافة"
        lblBOMFormMode.BackColor = UITheme.Success : lblBOMFormMode.ForeColor = UITheme.TextWhite
        dgvBOM.ClearSelection() : txtBOMCode.Focus()
    End Sub

    Private Sub dgvBOM_SelectionChanged(sender As Object, e As EventArgs) Handles dgvBOM.SelectionChanged
        If _loadingBOMs Then Return
        If dgvBOM.SelectedRows.Count = 0 Then Return
        LoadBOMIntoForm(CInt(dgvBOM.SelectedRows(0).Cells("BOMID").Value))
    End Sub

    Private Sub btnAddBOM_Click(sender As Object, e As EventArgs) Handles btnAddBOM.Click
        ResetBOMForm()
    End Sub

    Private Sub btnSaveBOM_Click(sender As Object, e As EventArgs) Handles btnSaveBOM.Click
        If String.IsNullOrWhiteSpace(txtBOMCode.Text) Then Msg("كود الخلطة مطلوب.", txtBOMCode) : Return
        If cboFinishedItem.SelectedValue Is Nothing Then Msg("المنتج النهائي مطلوب.") : Return
        If String.IsNullOrWhiteSpace(txtOutputQty.Text) Then Msg("الكمية الناتجة مطلوبة.", txtOutputQty) : Return

        Dim outputQty As Decimal
        If Not Decimal.TryParse(txtOutputQty.Text, outputQty) Then Msg("الكمية الناتجة يجب أن تكون رقمًا.", txtOutputQty) : Return
        If outputQty <= 0 Then Msg("الكمية الناتجة يجب أن تكون أكبر من صفر.", txtOutputQty) : Return

        Dim wastage As Decimal = 0
        If Not String.IsNullOrWhiteSpace(txtWastage.Text) Then
            If Not Decimal.TryParse(txtWastage.Text, wastage) Then Msg("نسبة الفقد يجب أن تكون رقمًا.", txtWastage) : Return
        End If

        Dim model As New BOMDto With {
            .BOMID = _currentBOMId,
            .BOMCode = txtBOMCode.Text.Trim(),
            .FinishedItemID = CInt(cboFinishedItem.SelectedValue),
            .OutputQty = outputQty,
            .OutputUnitID = CInt(cboOutputUnit.SelectedValue),
            .WastagePercent = wastage,
            .IsActive = chkBOMActive.Checked
        }

        Try
            If _currentBOMId = 0 Then
                Dim newId As Integer = _bomRepo.Insert(model, CurrentUserID())
                MessageBox.Show("تم إضافة الخلطة بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadBOMs(newId)
                LoadBOMSelectorCombo()
                LoadBOMIntoForm(newId)
            Else
                _bomRepo.Update(model, CurrentUserID())
                MessageBox.Show("تم تعديل الخلطة بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadBOMs(_currentBOMId)
                LoadBOMSelectorCombo()
                LoadBOMIntoForm(_currentBOMId)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancelBOM_Click(sender As Object, e As EventArgs) Handles btnCancelBOM.Click
        ResetBOMForm()
    End Sub

    Private Sub btnDeleteBOM_Click(sender As Object, e As EventArgs) Handles btnDeleteBOM.Click
        If _currentBOMId = 0 Then Msg("اختر خلطة من الجدول أولاً.") : Return
        If MessageBox.Show($"هل أنت متأكد من حذف الخلطة: {txtBOMCode.Text}؟" & vbCrLf &
                           "سيتم حذف كل المكونات المرتبطة بها.", "تأكيد الحذف",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            _bomRepo.Delete(_currentBOMId, CurrentUserID())
            MessageBox.Show("تم حذف الخلطة بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadBOMs()
            LoadBOMSelectorCombo()
            ResetBOMForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshBOM_Click(sender As Object, e As EventArgs) Handles btnRefreshBOM.Click
        Dim keepId As Integer = _currentBOMId
        LoadBOMs(keepId)
        LoadBOMSelectorCombo()
        If keepId > 0 Then LoadBOMIntoForm(keepId) Else ResetBOMForm()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  ═══════════════════ تفاصيل الخلطات ═══════════════════════
    ' ═══════════════════════════════════════════════════════════

    Private Sub InitializeBOMDetailsGrid()
        With dgvBOMDetails
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "DetailID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MaterialItemCode", .HeaderText = "كود المادة", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MaterialItemNameAr", .HeaderText = "اسم المادة", .FillWeight = 35})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Quantity", .HeaderText = "الكمية", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitName", .HeaderText = "الوحدة", .FillWeight = 15})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Notes", .HeaderText = "ملاحظات", .FillWeight = 20})
        End With
    End Sub

    Private Sub LoadBOMLookups()
        If _bomLookupsLoaded Then Return
        Try
            cboFinishedItem.DataSource = _itemRepo.GetFinishedGoods()
            cboFinishedItem.DisplayMember = "ItemNameAr"
            cboFinishedItem.ValueMember = "ItemID"

            cboMaterial.DataSource = _itemRepo.GetMaterials()
            cboMaterial.DisplayMember = "ItemNameAr"
            cboMaterial.ValueMember = "ItemID"

            cboOutputUnit.DataSource = _itemRepo.GetUnits()
            cboOutputUnit.DisplayMember = "UnitNameAr"
            cboOutputUnit.ValueMember = "UnitOfMeasureID"

            _bomLookupsLoaded = True
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل قوائم الخلطات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadBOMSelectorCombo()
        Try
            _loadingSelector = True
            Dim boms As List(Of BOMDto) = _bomRepo.GetAll()

            Dim dt As New DataTable()
            dt.Columns.Add("BOMID", GetType(Integer))
            dt.Columns.Add("Display", GetType(String))
            For Each b As BOMDto In boms
                dt.Rows.Add(b.BOMID, $"{b.BOMCode} — {b.FinishedItemNameAr}")
            Next

            cboBOMSelector.DataSource = dt
            cboBOMSelector.DisplayMember = "Display"
            cboBOMSelector.ValueMember = "BOMID"

            If _currentBOMId > 0 Then
                Try
                    cboBOMSelector.SelectedValue = _currentBOMId
                Catch
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل قائمة الخلطات: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingSelector = False
        End Try
    End Sub

    Private Sub cboBOMSelector_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboBOMSelector.SelectedIndexChanged

        If _loadingSelector Then Return
        If cboBOMSelector.SelectedValue Is Nothing Then Return
        If Not TypeOf cboBOMSelector.SelectedValue Is Integer Then Return

        Dim bomId As Integer = CInt(cboBOMSelector.SelectedValue)
        If bomId <= 0 Then Return

        _currentBOMId = bomId
        LoadBOMDetails(bomId)
        ResetBOMDetailForm()
    End Sub

    Private Sub tabMasterData_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles tabMasterData.SelectedIndexChanged

        If tabMasterData.SelectedTab Is tabBOMDetails Then
            LoadBOMSelectorCombo()

            If _currentBOMId > 0 Then
                Try
                    cboBOMSelector.SelectedValue = _currentBOMId
                Catch
                    If cboBOMSelector.Items.Count > 0 Then cboBOMSelector.SelectedIndex = 0
                End Try
            Else
                If cboBOMSelector.Items.Count > 0 Then cboBOMSelector.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub LoadBOMDetails(bomId As Integer)
        Try
            _loadingBOMDetails = True
            Dim details As List(Of BOMDetailDto) = _bomRepo.GetDetails(bomId)
            dgvBOMDetails.Rows.Clear()
            For Each d As BOMDetailDto In details
                dgvBOMDetails.Rows.Add(d.DetailID, d.MaterialItemCode, d.MaterialItemNameAr,
                                       d.Quantity.ToString("N3"), d.UnitName, d.Notes)
            Next
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل المكونات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingBOMDetails = False
        End Try
    End Sub

    Private Sub LoadBOMDetailIntoForm(detailId As Integer)
        Try
            Dim d As BOMDetailDto = _bomRepo.GetDetailById(detailId)
            If d Is Nothing Then Return
            _currentBOMDetailId = d.DetailID
            txtBOMDetailID.Text = d.DetailID.ToString()
            cboMaterial.SelectedValue = d.MaterialItemID
            txtQty.Text = d.Quantity.ToString("0.###")
            lblBOMDetailFormMode.Text = "✏️  وضع التعديل — ID: " & detailId
            lblBOMDetailFormMode.ForeColor = UITheme.Primary
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetBOMDetailForm()
        _currentBOMDetailId = 0
        txtBOMDetailID.Clear() : txtQty.Clear()
        If cboMaterial.Items.Count > 0 Then cboMaterial.SelectedIndex = 0
        lblBOMDetailFormMode.Text = "➕  وضع الإضافة"
        lblBOMDetailFormMode.ForeColor = UITheme.TextSecondary
        dgvBOMDetails.ClearSelection()
    End Sub

    Private Sub dgvBOMDetails_SelectionChanged(sender As Object, e As EventArgs) _
        Handles dgvBOMDetails.SelectionChanged

        If _loadingBOMDetails Then Return
        If dgvBOMDetails.SelectedRows.Count = 0 Then Return
        LoadBOMDetailIntoForm(CInt(dgvBOMDetails.SelectedRows(0).Cells("DetailID").Value))
    End Sub

    Private Sub btnAddBOMDetail_Click(sender As Object, e As EventArgs) Handles btnAddBOMDetail.Click
        If _currentBOMId = 0 Then
            Msg("اختر خلطة من القائمة أولاً.")
            Return
        End If
        ResetBOMDetailForm()
        cboMaterial.Focus()
    End Sub

    Private Sub btnSaveBOMDetail_Click(sender As Object, e As EventArgs) Handles btnSaveBOMDetail.Click
        If _currentBOMId = 0 Then Msg("اختر خلطة أولاً.") : Return
        If cboMaterial.SelectedValue Is Nothing Then Msg("المادة مطلوبة.") : Return
        If String.IsNullOrWhiteSpace(txtQty.Text) Then Msg("الكمية مطلوبة.", txtQty) : Return

        Dim qty As Decimal
        If Not Decimal.TryParse(txtQty.Text, qty) Then Msg("الكمية يجب أن تكون رقمًا.", txtQty) : Return
        If qty <= 0 Then Msg("الكمية يجب أن تكون أكبر من صفر.", txtQty) : Return

        Dim model As New BOMDetailDto With {
            .DetailID = _currentBOMDetailId,
            .BOMID = _currentBOMId,
            .MaterialItemID = CInt(cboMaterial.SelectedValue),
            .Quantity = qty
        }

        Try
            If _currentBOMDetailId = 0 Then
                _bomRepo.InsertDetail(model, CurrentUserID())
                MessageBox.Show("تم إضافة المكوّن بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                _bomRepo.UpdateDetail(model, CurrentUserID())
                MessageBox.Show("تم تعديل المكوّن بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            LoadBOMDetails(_currentBOMId)
            ResetBOMDetailForm()
            LoadBOMs(_currentBOMId)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancelBOMDetail_Click(sender As Object, e As EventArgs) Handles btnCancelBOMDetail.Click
        ResetBOMDetailForm()
    End Sub

    Private Sub btnDeleteBOMDetail_Click(sender As Object, e As EventArgs) Handles btnDeleteBOMDetail.Click
        If _currentBOMDetailId = 0 Then Msg("اختر مكوّنًا من الجدول أولاً.") : Return
        If MessageBox.Show("هل أنت متأكد من حذف هذا المكوّن؟", "تأكيد الحذف",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            _bomRepo.DeleteDetail(_currentBOMDetailId)
            MessageBox.Show("تم حذف المكوّن بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadBOMDetails(_currentBOMId)
            ResetBOMDetailForm()
            LoadBOMs(_currentBOMId)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshBOMDetail_Click(sender As Object, e As EventArgs) Handles btnRefreshBOMDetail.Click
        If _currentBOMId = 0 Then Msg("اختر خلطة أولاً.") : Return
        LoadBOMDetails(_currentBOMId)
        ResetBOMDetailForm()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  ═══════════════════ الوحدات ═══════════════════════════════
    ' ═══════════════════════════════════════════════════════════

    Private Sub InitializeUnitsGrid()
        With dgvUnits
            .Columns.Clear() : .AutoGenerateColumns = False
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitID", .Visible = False})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Serial", .HeaderText = "م", .FillWeight = 8})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitNameAr", .HeaderText = "الاسم عربي", .FillWeight = 46})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitNameEn", .HeaderText = "الاسم إنجليزي", .FillWeight = 46})
        End With
    End Sub

    Private Sub LoadUnits(Optional ByVal selectUnitId As Integer = 0)
        Try
            _loadingUnits = True
            dgvUnits.Rows.Clear()

            Using conn As New SqlConnection(DBConnection.GetConnectionString())
                conn.Open()
                Dim q As String =
                    "SELECT UnitOfMeasureID, UnitNameAr, UnitNameEn " &
                    "FROM Core.UnitOfMeasure " &
                    "ORDER BY UnitOfMeasureID DESC"

                Using cmd As New SqlCommand(q, conn)
                    Using r As SqlDataReader = cmd.ExecuteReader()
                        While r.Read()
                            dgvUnits.Rows.Add(
                                r("UnitOfMeasureID"),
                                r("UnitOfMeasureID"),
                                r("UnitNameAr"),
                                r("UnitNameEn"))
                        End While
                    End Using
                End Using
            End Using

            If selectUnitId > 0 Then
                For Each row As DataGridViewRow In dgvUnits.Rows
                    If CInt(row.Cells("UnitID").Value) = selectUnitId Then
                        row.Selected = True
                        Exit For
                    End If
                Next
            Else
                btnDeleteUnit.Enabled = False
                _currentUnitID = 0
            End If
        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل الوحدات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _loadingUnits = False
        End Try
    End Sub

    Private Sub ResetUnitForm()
        _currentUnitID = 0
        txtUnitNameAr.Clear() : txtUnitNameEn.Clear()
        dgvUnits.ClearSelection()
        btnDeleteUnit.Enabled = False
        lblUnitFormMode.Text = "➕  إضافة وحدة جديدة"
        lblUnitFormMode.BackColor = UITheme.Success
        lblUnitFormMode.ForeColor = UITheme.TextWhite
        txtUnitNameAr.Focus()
    End Sub

    Private Sub btnAddUnit_Click(sender As Object, e As EventArgs) Handles btnAddUnit.Click
        ResetUnitForm()
    End Sub

    Private Sub btnSaveUnit_Click(sender As Object, e As EventArgs) Handles btnSaveUnit.Click
        If String.IsNullOrWhiteSpace(txtUnitNameAr.Text) Then Msg("الاسم العربي مطلوب.", txtUnitNameAr) : Return
        If String.IsNullOrWhiteSpace(txtUnitNameEn.Text) Then Msg("الاسم الإنجليزي مطلوب.", txtUnitNameEn) : Return

        Try
            Using conn As New SqlConnection(DBConnection.GetConnectionString())
                conn.Open()

                Dim chkQ As String =
                    "SELECT COUNT(*) FROM Core.UnitOfMeasure " &
                    "WHERE UnitNameAr = @Ar OR UnitNameEn = @En"

                Using chk As New SqlCommand(chkQ, conn)
                    chk.Parameters.AddWithValue("@Ar", txtUnitNameAr.Text.Trim())
                    chk.Parameters.AddWithValue("@En", txtUnitNameEn.Text.Trim())
                    If CInt(chk.ExecuteScalar()) > 0 Then
                        MessageBox.Show("هذه الوحدة موجودة بالفعل!", "تنبيه",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                Dim insQ As String =
                    "INSERT INTO Core.UnitOfMeasure (UnitNameAr, UnitNameEn) " &
                    "VALUES (@Ar, @En)"

                Using cmd As New SqlCommand(insQ, conn)
                    cmd.Parameters.AddWithValue("@Ar", txtUnitNameAr.Text.Trim())
                    cmd.Parameters.AddWithValue("@En", txtUnitNameEn.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("تمت إضافة الوحدة بنجاح.", "تم",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ResetUnitForm()
            LoadUnits()
        Catch ex As Exception
            MessageBox.Show("خطأ في الإضافة: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancelUnit_Click(sender As Object, e As EventArgs) Handles btnCancelUnit.Click
        ResetUnitForm()
    End Sub

    Private Sub btnDeleteUnit_Click(sender As Object, e As EventArgs) Handles btnDeleteUnit.Click
        If _currentUnitID = 0 Then Msg("اختر وحدة من الجدول أولاً.") : Return

        Dim unitName As String = dgvUnits.SelectedRows(0).Cells("UnitNameAr").Value.ToString()

        If MessageBox.Show($"هل أنت متأكد من حذف الوحدة: {unitName}؟", "تأكيد الحذف",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        Try
            Using conn As New SqlConnection(DBConnection.GetConnectionString())
                conn.Open()
                Dim delQ As String =
                    "DELETE FROM Core.UnitOfMeasure WHERE UnitOfMeasureID = @ID"
                Using cmd As New SqlCommand(delQ, conn)
                    cmd.Parameters.AddWithValue("@ID", _currentUnitID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("تم حذف الوحدة بنجاح.", "تم",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ResetUnitForm()
            LoadUnits()
        Catch ex As SqlException When ex.Number = 547
            MessageBox.Show("لا يمكن حذف الوحدة لأنها مستخدمة في جداول أخرى.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("خطأ في الحذف: " & ex.Message, "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshUnit_Click(sender As Object, e As EventArgs) Handles btnRefreshUnit.Click
        LoadUnits(_currentUnitID)
    End Sub

    Private Sub dgvUnits_SelectionChanged(sender As Object, e As EventArgs) Handles dgvUnits.SelectionChanged
        If _loadingUnits Then Return
        If dgvUnits.SelectedRows.Count = 0 Then
            _currentUnitID = 0
            btnDeleteUnit.Enabled = False
            Return
        End If
        _currentUnitID = CInt(dgvUnits.SelectedRows(0).Cells("UnitID").Value)
        btnDeleteUnit.Enabled = True
    End Sub

    Private Sub txtUnitNameEn_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUnitNameEn.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSaveUnit_Click(sender, e)
            e.SuppressKeyPress = True
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  أدوات مشتركة
    ' ═══════════════════════════════════════════════════════════
    Private Sub ApplyThemeToGrids()
        UITheme.ApplyGridTheme(dgvItems)
        UITheme.ApplyGridTheme(dgvProductionLines)
        UITheme.ApplyGridTheme(dgvLabor)
        UITheme.ApplyGridTheme(dgvBOM)
        UITheme.ApplyGridTheme(dgvBOMDetails)
        UITheme.ApplyGridTheme(dgvShifts)
        UITheme.ApplyGridTheme(dgvUnits)
    End Sub

    Private Function CurrentUserID() As Integer
        Return 1
    End Function

    Private Sub Msg(text As String, Optional focus As Control = Nothing)
        MessageBox.Show(text, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        If focus IsNot Nothing Then focus.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  الورديات (Placeholders)
    ' ═══════════════════════════════════════════════════════════
    Private Sub btnAddShift_Click(sender As Object, e As EventArgs) Handles btnAddShift.Click
        MessageBox.Show("قيد التطوير — الورديات", "تنبيه")
    End Sub
    Private Sub btnEditShift_Click(sender As Object, e As EventArgs) Handles btnEditShift.Click
        MessageBox.Show("قيد التطوير", "تنبيه")
    End Sub
    Private Sub btnDeleteShift_Click(sender As Object, e As EventArgs) Handles btnDeleteShift.Click
        MessageBox.Show("قيد التطوير", "تنبيه")
    End Sub
    Private Sub btnRefreshShift_Click(sender As Object, e As EventArgs) Handles btnRefreshShift.Click
        MessageBox.Show("قيد التطوير", "تنبيه")
    End Sub

End Class