Imports System.Drawing
Imports System.Windows.Forms

Namespace Global.KafiPro.UI.Shared.Themes

    ''' <summary>
    ''' ثيم موحد للتطبيق — ألوان وخطوط
    ''' </summary>
    Public NotInheritable Class UITheme

        Private Sub New()
        End Sub

        ' ==========================================
        ' الألوان الأساسية
        ' ==========================================
        ' 🎨 لوحة ألوان عصرية (Deep Blue + Teal)
        Public Shared ReadOnly Primary As Color = Color.FromArgb(37, 99, 235)     ' أزرق ملكي
        Public Shared ReadOnly PrimaryDark As Color = Color.FromArgb(29, 78, 216)
        Public Shared ReadOnly Accent As Color = Color.FromArgb(20, 184, 166)    ' Teal
        Public Shared ReadOnly SidebarBackground As Color = Color.FromArgb(15, 23, 42)      ' كحلي داكن
        Public Shared ReadOnly SidebarHover As Color = Color.FromArgb(30, 41, 59)
        Public Shared ReadOnly SidebarActive As Color = Color.FromArgb(37, 99, 235)
        Public Shared ReadOnly Background As Color = Color.FromArgb(248, 250, 252)   ' أبيض فاتح
        Public Shared ReadOnly CardBackground As Color = Color.White
        Public Shared ReadOnly Border As Color = Color.FromArgb(226, 232, 240)
        Public Shared ReadOnly TextPrimary As Color = Color.FromArgb(15, 23, 42)
        Public Shared ReadOnly TextSecondary As Color = Color.FromArgb(100, 116, 139)
        Public Shared ReadOnly TextWhite As Color = Color.White
        Public Shared ReadOnly Success As Color = Color.FromArgb(34, 197, 94)
        Public Shared ReadOnly Warning As Color = Color.FromArgb(245, 158, 11)
        Public Shared ReadOnly Danger As Color = Color.FromArgb(239, 68, 68)

        Public Shared ReadOnly FontNormal As Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        Public Shared ReadOnly FontBold As Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        ' ==========================================
        ' الأسطح والخلفيات
        ' ==========================================
        Public Shared ReadOnly Surface As Color = Color.White
        'Public Shared ReadOnly Background As Color = Color.FromArgb(245, 247, 250)
        'Public Shared ReadOnly Border As Color = Color.FromArgb(220, 225, 230)

        ' ==========================================
        ' النصوص
        ' ==========================================
        'Public Shared ReadOnly TextPrimary As Color = Color.FromArgb(44, 62, 80)
        'Public Shared ReadOnly TextSecondary As Color = Color.FromArgb(127, 140, 141)
        'Public Shared ReadOnly TextWhite As Color = Color.White
        Public Shared ReadOnly TextMuted As Color = Color.FromArgb(149, 165, 166)

        ' ==========================================
        ' أزرار
        ' ==========================================
        Public Shared ReadOnly ButtonNormal As Color = Color.FromArgb(236, 240, 241)
        Public Shared ReadOnly HoverBackground As Color = Color.FromArgb(214, 234, 248)
        Public Shared ReadOnly HoverText As Color = Color.FromArgb(41, 128, 185)
        Public Shared ReadOnly ActiveBackground As Color = Color.FromArgb(41, 128, 185)
        Public Shared ReadOnly ActiveText As Color = Color.White

        ' ==========================================
        ' الخطوط
        ' ==========================================
        'Public Shared ReadOnly FontNormal As New Font("Segoe UI", 9.0F, FontStyle.Regular)
        'Public Shared ReadOnly FontBold As New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Public Shared ReadOnly FontTitle As New Font("Segoe UI", 11.0F, FontStyle.Bold)

        'Public Shared ReadOnly SidebarBackground As Color = Color.FromArgb(44, 62, 80)
        'Public Shared ReadOnly SidebarText As Color = Color.FromArgb(236, 240, 241)
        'Public Shared ReadOnly SidebarHover As Color = Color.FromArgb(52, 73, 94)
        'Public Shared ReadOnly SidebarActive As Color = Color.FromArgb(41, 128, 185)

        Public Shared ReadOnly Property FontRegular As Font
            Get
                Return New Font("Segoe UI", 10, FontStyle.Regular)
            End Get
        End Property

        Public Shared ReadOnly Property SurfaceAlt As Color
            Get
                Return Color.FromArgb(248, 250, 252)
            End Get
        End Property

        ' ==========================================================
        ' ============== دوال مساعدة للتطبيق السريع ================
        ' ==========================================================

        ''' <summary>يطبّق RTL على TextBox — النص على اليمين مع مؤشر يمين</summary>
        Public Shared Sub ApplyRTLTextBox(txt As TextBox)
            If txt Is Nothing Then Return
            txt.RightToLeft = RightToLeft.Yes
            txt.TextAlign = HorizontalAlignment.Right
        End Sub

        ''' <summary>يطبّق RTL على ComboBox — Border واضح + نص على اليمين</summary>
        Public Shared Sub ApplyRTLComboBox(cbo As ComboBox)
            If cbo Is Nothing Then Return
            cbo.RightToLeft = RightToLeft.Yes
            cbo.FlatStyle = FlatStyle.Standard
            cbo.DropDownStyle = ComboBoxStyle.DropDownList
        End Sub

        ''' <summary>يطبّق RTL على Button — النص على اليمين</summary>
        Public Shared Sub ApplyRTLButton(btn As Button)
            If btn Is Nothing Then Return
            btn.RightToLeft = RightToLeft.Yes
            btn.TextAlign = ContentAlignment.MiddleRight
        End Sub

        ''' <summary>يطبّق RTL على CheckBox — المربع على اليمين</summary>
        Public Shared Sub ApplyRTLCheckBox(chk As CheckBox)
            If chk Is Nothing Then Return
            chk.RightToLeft = RightToLeft.Yes
            chk.CheckAlign = ContentAlignment.MiddleRight
        End Sub

        ''' <summary>يطبّق RTL على Label — النص على اليمين</summary>
        Public Shared Sub ApplyRTLLabel(lbl As Label)
            If lbl Is Nothing Then Return
            lbl.RightToLeft = RightToLeft.Yes
            lbl.TextAlign = ContentAlignment.MiddleRight
        End Sub

        ''' <summary>يطبّق الثيم الكامل على DataGridView</summary>
        Public Shared Sub ApplyGridTheme(dgv As DataGridView)
            If dgv Is Nothing Then Return

            dgv.RightToLeft = RightToLeft.Yes
            dgv.EnableHeadersVisualStyles = False

            With dgv.ColumnHeadersDefaultCellStyle
                .BackColor = Primary
                .ForeColor = TextWhite
                .Font = FontBold
                .Alignment = DataGridViewContentAlignment.MiddleCenter
                .SelectionBackColor = Primary
                .SelectionForeColor = TextWhite
            End With

            With dgv.DefaultCellStyle
                .BackColor = Surface
                .ForeColor = TextPrimary
                .SelectionBackColor = HoverBackground
                .SelectionForeColor = Primary
                .Font = FontRegular
                .Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            dgv.AlternatingRowsDefaultCellStyle.BackColor = SurfaceAlt
            dgv.GridColor = Border
            dgv.BorderStyle = BorderStyle.None
            dgv.BackgroundColor = Surface
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        End Sub

        ''' <summary>يطبّق الثيم على Toolbar (FlowLayoutPanel)</summary>
        Public Shared Sub ApplyToolbarTheme(flp As FlowLayoutPanel)
            If flp Is Nothing Then Return
            flp.BackColor = ButtonNormal
            flp.Padding = New Padding(8, 6, 8, 6)
            flp.FlowDirection = FlowDirection.RightToLeft
        End Sub

    End Class

End Namespace