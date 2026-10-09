Imports System.Drawing
Imports System.Windows.Forms
Imports KafiPro.UI.Shared.Themes

Namespace KafiPro.UI.Shared.Helpers

    ''' <summary>
    ''' Helper موحّد لإدارة أزرار الشريط العلوي (SubNav) في كل الموديولات
    ''' </summary>
    Public Module SubNavHelper

        ' ═══════════════════════════════════════════════════════
        '  Public API — الأصلي (بيستخدمه موديول الإنتاج)
        ' ═══════════════════════════════════════════════════════

        ''' <summary>تهيئة الأزرار — ستايل افتراضي + تفعيل الزر الأول</summary>
        Public Function Initialize(defaultButton As Button,
                                   ParamArray buttons() As Button) As Button
            If buttons Is Nothing OrElse buttons.Length = 0 Then
                Throw New ArgumentException("لازم تمرر زر واحد على الأقل", NameOf(buttons))
            End If

            For Each btn In buttons
                If btn Is Nothing Then Continue For
                ApplyDefaultStyle(btn)
            Next

            If defaultButton IsNot Nothing Then
                SetActive(buttons, defaultButton)
            End If

            Return defaultButton
        End Function

        ''' <summary>تعيين الزر النشط — مع تلوين الأيقونات لغير النشط</summary>
        Public Sub SetActive(allButtons As Button(), activeBtn As Button)
            If allButtons Is Nothing Then Return

            For Each btn As Button In allButtons
                If btn Is Nothing Then Continue For

                If btn Is activeBtn Then
                    ApplyActiveStyle(btn)
                Else
                    ApplyDefaultStyle(btn)
                    ApplyIconColor(btn)
                End If
            Next
        End Sub

        ''' <summary>ربط الأحداث لكل الأزرار (مع إزالة الربط القديم أولاً)</summary>
        Public Sub WireClickHandler(buttons() As Button, handler As EventHandler)
            If buttons Is Nothing OrElse handler Is Nothing Then Return

            For Each btn In buttons
                If btn Is Nothing Then Continue For
                RemoveHandler btn.Click, handler
                AddHandler btn.Click, handler
            Next
        End Sub

        ' ═══════════════════════════════════════════════════════
        '  Public API — الإضافات الجديدة (لموديول المخازن)
        ' ═══════════════════════════════════════════════════════

        ''' <summary>ستايل زر (نشط / غير نشط) — يُستخدم مباشرة</summary>
        Public Sub StyleButton(btn As Button, isActive As Boolean)
            If btn Is Nothing Then Return

            If isActive Then
                ApplyActiveStyle(btn)
            Else
                ApplyDefaultStyle(btn)
                ApplyIconColor(btn)
            End If
        End Sub

        ''' <summary>ينشّط زراً واحداً فقط داخل الحاوية (Panel/FlowLayoutPanel)</summary>
        Public Sub Activate(container As Control, active As Button)
            If container Is Nothing Then Return

            For Each c As Control In container.Controls
                If TypeOf c Is Button Then
                    StyleButton(DirectCast(c, Button), c Is active)
                End If
            Next
        End Sub

        ''' <summary>ينشئ زر SubNav جاهزاً مربوطاً بحدث Click</summary>
        Public Function CreateButton(text As String, tag As String,
                                     onClick As EventHandler) As Button
            Dim b As New Button With {
                .Text = text,
                .Tag = tag,
                .Width = 130,
                .Height = 40,
                .Margin = New Padding(2),
                .Name = "btnNav_" & tag,
                .FlatStyle = FlatStyle.Flat
            }
            b.FlatAppearance.BorderSize = 0

            AddHandler b.Click, onClick
            StyleButton(b, False)
            Return b
        End Function

        ' ═══════════════════════════════════════════════════════
        '  Private Helpers
        ' ═══════════════════════════════════════════════════════

        Private Sub ApplyDefaultStyle(btn As Button)
            btn.BackColor = UITheme.ButtonNormal
            btn.ForeColor = UITheme.TextPrimary
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = UITheme.HoverBackground
            btn.Cursor = Cursors.Hand
            btn.Font = UITheme.FontBold
            btn.UseVisualStyleBackColor = False
        End Sub

        Private Sub ApplyActiveStyle(btn As Button)
            btn.BackColor = UITheme.ActiveBackground
            btn.ForeColor = UITheme.ActiveText
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = UITheme.PrimaryDark
            btn.Cursor = Cursors.Hand
            btn.Font = UITheme.FontBold
            btn.UseVisualStyleBackColor = False
        End Sub

        Private Sub ApplyIconColor(btn As Button)
            Select Case btn.Name
                ' ═══ موديول المخازن ═══
                Case "btnWarehouses", "btnNav_MASTER" : btn.ForeColor = Color.FromArgb(41, 128, 185)
                Case "btnStockBalance", "btnNav_BALANCE" : btn.ForeColor = Color.FromArgb(39, 174, 96)
                Case "btnMovements", "btnNav_MOVEMENT" : btn.ForeColor = Color.FromArgb(155, 89, 182)
                Case "btnTransfer", "btnNav_TRANSFER" : btn.ForeColor = Color.FromArgb(230, 126, 34)
                Case "btnSalesIssue", "btnNav_ISSUE_SALE" : btn.ForeColor = Color.FromArgb(192, 57, 43)
                Case "btnStockCount", "btnNav_COUNT" : btn.ForeColor = Color.FromArgb(22, 160, 133)
                Case "btnReports", "btnNav_REPORTS" : btn.ForeColor = Color.FromArgb(52, 73, 94)
                Case "btnNav_ISSUE_PROD" : btn.ForeColor = Color.FromArgb(241, 196, 15)

                ' ═══ موديول الإنتاج ═══
                Case "btnMasterData" : btn.ForeColor = Color.FromArgb(41, 128, 185)
                Case "btnDailyProduction" : btn.ForeColor = Color.FromArgb(39, 174, 96)
                Case "btnFinalProduct" : btn.ForeColor = Color.FromArgb(230, 126, 34)
                Case "btnWithdrawalOrder" : btn.ForeColor = Color.FromArgb(155, 89, 182)
                Case "btnOrder" : btn.ForeColor = Color.FromArgb(22, 160, 133)
                Case "btnDashboard" : btn.ForeColor = Color.FromArgb(241, 196, 15)
                Case "btnReportsProd" : btn.ForeColor = Color.FromArgb(52, 73, 94)
            End Select
        End Sub

    End Module

End Namespace