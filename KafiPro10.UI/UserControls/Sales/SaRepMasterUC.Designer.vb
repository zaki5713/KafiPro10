Imports KafiPro.UI.Shared.Themes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SaRepMasterUC
    Inherits BaseUC

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlToolbar = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.pnlEditor = New System.Windows.Forms.Panel()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.lblBranch = New System.Windows.Forms.Label()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.lblRoute = New System.Windows.Forms.Label()
        Me.cboRoute = New System.Windows.Forms.ComboBox()
        Me.lblCommission = New System.Windows.Forms.Label()
        Me.nudCommission = New System.Windows.Forms.NumericUpDown()
        Me.lblTarget = New System.Windows.Forms.Label()
        Me.nudTarget = New System.Windows.Forms.NumericUpDown()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()

        Me.pnlToolbar.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEditor.SuspendLayout()
        CType(Me.nudCommission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudTarget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        ' pnlToolbar
        '
        Me.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolbar.Height = 50
        Me.pnlToolbar.BackColor = UITheme.Surface
        Me.pnlToolbar.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlToolbar.Controls.Add(Me.btnAdd)
        Me.pnlToolbar.Controls.Add(Me.btnEdit)
        Me.pnlToolbar.Controls.Add(Me.btnDelete)
        Me.pnlToolbar.Controls.Add(Me.btnRefresh)
        Me.pnlToolbar.Controls.Add(Me.lblSearch)
        Me.pnlToolbar.Controls.Add(Me.txtSearch)
        '
        ' btnAdd
        '
        Me.btnAdd.Text = "إضافة"
        Me.btnAdd.Size = New System.Drawing.Size(90, 32)
        Me.btnAdd.Location = New System.Drawing.Point(10, 9)
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.BackColor = UITheme.Primary
        Me.btnAdd.ForeColor = System.Drawing.Color.White
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.Font = UITheme.FontBold
        Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        '
        ' btnEdit
        '
        Me.btnEdit.Text = "تعديل"
        Me.btnEdit.Size = New System.Drawing.Size(90, 32)
        Me.btnEdit.Location = New System.Drawing.Point(106, 9)
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.BackColor = UITheme.ButtonNormal
        Me.btnEdit.ForeColor = UITheme.TextPrimary
        Me.btnEdit.FlatAppearance.BorderSize = 0
        Me.btnEdit.Font = UITheme.FontBold
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        '
        ' btnDelete
        '
        Me.btnDelete.Text = "حذف"
        Me.btnDelete.Size = New System.Drawing.Size(90, 32)
        Me.btnDelete.Location = New System.Drawing.Point(202, 9)
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69)
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.Font = UITheme.FontBold
        Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        '
        ' btnRefresh
        '
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.Size = New System.Drawing.Size(90, 32)
        Me.btnRefresh.Location = New System.Drawing.Point(298, 9)
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.BackColor = UITheme.ButtonNormal
        Me.btnRefresh.ForeColor = UITheme.TextPrimary
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.Font = UITheme.FontBold
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        '
        ' lblSearch
        '
        Me.lblSearch.Text = "بحث:"
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = UITheme.FontBold
        Me.lblSearch.ForeColor = UITheme.TextPrimary
        Me.lblSearch.Location = New System.Drawing.Point(620, 17)
        Me.lblSearch.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        '
        ' txtSearch
        '
        Me.txtSearch.Size = New System.Drawing.Size(200, 26)
        Me.txtSearch.Location = New System.Drawing.Point(410, 12)
        Me.txtSearch.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Font = UITheme.FontBold
        '
        ' dgv
        '
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.BackgroundColor = UITheme.Surface
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgv.Font = UITheme.FontBold
        Me.dgv.RowHeadersVisible = False
        Me.dgv.EnableHeadersVisualStyles = False
        Me.dgv.ColumnHeadersDefaultCellStyle.BackColor = UITheme.Primary
        Me.dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersDefaultCellStyle.Font = UITheme.FontBold
        Me.dgv.ColumnHeadersHeight = 36
        Me.dgv.RowTemplate.Height = 30
        Me.dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
        '
        ' pnlEditor
        '
        Me.pnlEditor.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlEditor.Height = 130
        Me.pnlEditor.BackColor = UITheme.Surface
        Me.pnlEditor.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlEditor.Visible = False
        Me.pnlEditor.Controls.Add(Me.lblCode)
        Me.pnlEditor.Controls.Add(Me.txtCode)
        Me.pnlEditor.Controls.Add(Me.lblName)
        Me.pnlEditor.Controls.Add(Me.txtName)
        Me.pnlEditor.Controls.Add(Me.lblPhone)
        Me.pnlEditor.Controls.Add(Me.txtPhone)
        Me.pnlEditor.Controls.Add(Me.lblBranch)
        Me.pnlEditor.Controls.Add(Me.cboBranch)
        Me.pnlEditor.Controls.Add(Me.lblRoute)
        Me.pnlEditor.Controls.Add(Me.cboRoute)
        Me.pnlEditor.Controls.Add(Me.lblCommission)
        Me.pnlEditor.Controls.Add(Me.nudCommission)
        Me.pnlEditor.Controls.Add(Me.lblTarget)
        Me.pnlEditor.Controls.Add(Me.nudTarget)
        Me.pnlEditor.Controls.Add(Me.chkActive)
        Me.pnlEditor.Controls.Add(Me.btnSave)
        Me.pnlEditor.Controls.Add(Me.btnCancel)
        '
        ' Editor fields (صف 1)
        '
        Me.lblCode.Text = "الكود:" : Me.lblCode.Location = New System.Drawing.Point(730, 20) : Me.lblCode.AutoSize = True : Me.lblCode.Font = UITheme.FontBold
        Me.txtCode.Location = New System.Drawing.Point(620, 17) : Me.txtCode.Size = New System.Drawing.Size(100, 26)

        Me.lblName.Text = "الاسم:" : Me.lblName.Location = New System.Drawing.Point(560, 20) : Me.lblName.AutoSize = True : Me.lblName.Font = UITheme.FontBold
        Me.txtName.Location = New System.Drawing.Point(400, 17) : Me.txtName.Size = New System.Drawing.Size(150, 26)

        Me.lblPhone.Text = "الهاتف:" : Me.lblPhone.Location = New System.Drawing.Point(340, 20) : Me.lblPhone.AutoSize = True : Me.lblPhone.Font = UITheme.FontBold
        Me.txtPhone.Location = New System.Drawing.Point(220, 17) : Me.txtPhone.Size = New System.Drawing.Size(110, 26)
        '
        ' Editor fields (صف 2)
        '
        Me.lblBranch.Text = "الفرع:" : Me.lblBranch.Location = New System.Drawing.Point(730, 60) : Me.lblBranch.AutoSize = True : Me.lblBranch.Font = UITheme.FontBold
        Me.cboBranch.Location = New System.Drawing.Point(580, 57) : Me.cboBranch.Size = New System.Drawing.Size(140, 26) : Me.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList

        Me.lblRoute.Text = "خط السير:" : Me.lblRoute.Location = New System.Drawing.Point(490, 60) : Me.lblRoute.AutoSize = True : Me.lblRoute.Font = UITheme.FontBold
        Me.cboRoute.Location = New System.Drawing.Point(350, 57) : Me.cboRoute.Size = New System.Drawing.Size(130, 26) : Me.cboRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList

        Me.lblCommission.Text = "العمولة %:" : Me.lblCommission.Location = New System.Drawing.Point(260, 60) : Me.lblCommission.AutoSize = True : Me.lblCommission.Font = UITheme.FontBold
        Me.nudCommission.Location = New System.Drawing.Point(180, 57) : Me.nudCommission.Size = New System.Drawing.Size(70, 26)
        Me.nudCommission.DecimalPlaces = 2 : Me.nudCommission.Maximum = 100

        Me.lblTarget.Text = "التارجت:" : Me.lblTarget.Location = New System.Drawing.Point(110, 60) : Me.lblTarget.AutoSize = True : Me.lblTarget.Font = UITheme.FontBold
        Me.nudTarget.Location = New System.Drawing.Point(20, 57) : Me.nudTarget.Size = New System.Drawing.Size(85, 26)
        Me.nudTarget.Maximum = 999999999 : Me.nudTarget.ThousandsSeparator = True
        '
        ' chkActive
        '
        Me.chkActive.Text = "نشط"
        Me.chkActive.Location = New System.Drawing.Point(20, 20)
        Me.chkActive.AutoSize = True
        Me.chkActive.Font = UITheme.FontBold
        Me.chkActive.ForeColor = UITheme.TextPrimary
        '
        ' btnSave
        '
        Me.btnSave.Text = "حفظ"
        Me.btnSave.Size = New System.Drawing.Size(100, 32)
        Me.btnSave.Location = New System.Drawing.Point(700, 95)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.BackColor = UITheme.Primary
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Font = UITheme.FontBold
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        '
        ' btnCancel
        '
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.Size = New System.Drawing.Size(100, 32)
        Me.btnCancel.Location = New System.Drawing.Point(595, 95)
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.BackColor = UITheme.ButtonNormal
        Me.btnCancel.ForeColor = UITheme.TextPrimary
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.Font = UITheme.FontBold
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        '
        ' SaRepMasterUC
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.pnlContentContainer.Controls.Add(Me.dgv)
        Me.pnlContentContainer.Controls.Add(Me.pnlEditor)
        Me.pnlContentContainer.Controls.Add(Me.pnlToolbar)
        Me.Name = "SaRepMasterUC"
        Me.Size = New System.Drawing.Size(900, 600)
        Me.pnlToolbar.ResumeLayout(False)
        Me.pnlToolbar.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEditor.ResumeLayout(False)
        Me.pnlEditor.PerformLayout()
        CType(Me.nudCommission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudTarget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlToolbar As System.Windows.Forms.Panel
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents pnlEditor As System.Windows.Forms.Panel
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents lblBranch As System.Windows.Forms.Label
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents lblRoute As System.Windows.Forms.Label
    Friend WithEvents cboRoute As System.Windows.Forms.ComboBox
    Friend WithEvents lblCommission As System.Windows.Forms.Label
    Friend WithEvents nudCommission As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblTarget As System.Windows.Forms.Label
    Friend WithEvents nudTarget As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button

End Class