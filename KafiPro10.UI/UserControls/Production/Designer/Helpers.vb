Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Partial Class ProMasterDataUC

    ' ═══════════════════════════════════════════════════════
    '  Helpers — دوال مساعدة لتصميم عناصر التابات
    ' ═══════════════════════════════════════════════════════

    Private Sub SetupTabPage(tp As TabPage, name As String, tabIdx As Integer, text As String)
        tp.BackColor = UITheme.Background
        tp.Location = New Point(4, 30)
        tp.Name = name
        tp.Padding = New Padding(3)
        tp.Size = New Size(792, 566)
        tp.TabIndex = tabIdx
        tp.Text = text
    End Sub

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

    Private Sub SetupCheckBox(chk As CheckBox, text As String, x As Integer, y As Integer, w As Integer)
        chk.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
        chk.AutoSize = False
        chk.CheckAlign = ContentAlignment.MiddleRight
        chk.Checked = True
        chk.Font = UITheme.FontBold
        chk.ForeColor = UITheme.Success
        chk.Location = New Point(x, y)
        chk.Size = New Size(w, 24)
        chk.RightToLeft = RightToLeft.Yes
        chk.Text = text
        chk.TextAlign = ContentAlignment.MiddleRight
        chk.UseVisualStyleBackColor = True
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

    Private Sub SetupBadge(lbl As Label, text As String, x As Integer, y As Integer,
                           w As Integer, h As Integer, bgColor As Color)
        lbl.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
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

End Class