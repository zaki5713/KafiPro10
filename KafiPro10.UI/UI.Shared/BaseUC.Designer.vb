<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BaseUC
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeaderTitle = New System.Windows.Forms.Label()
        Me.pnlContentContainer = New System.Windows.Forms.Panel()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader (شريط العنوان العلوي)
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(800, 40)
        Me.pnlHeader.TabIndex = 0

        'lblHeaderTitle (عنوان الشاشة)
        '
        Me.lblHeaderTitle.AutoSize = True
        Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeaderTitle.ForeColor = System.Drawing.Color.White
        Me.lblHeaderTitle.Location = New System.Drawing.Point(15, 8)
        Me.lblHeaderTitle.Name = "lblHeaderTitle"
        Me.lblHeaderTitle.Size = New System.Drawing.Size(165, 30)
        Me.lblHeaderTitle.TabIndex = 0
        Me.lblHeaderTitle.Text = "عنوان الشاشة"
        '
        'pnlContentContainer (حاوية المحتوى)
        '
        Me.pnlContentContainer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlContentContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContentContainer.Location = New System.Drawing.Point(0, 40)
        Me.pnlContentContainer.Name = "pnlContentContainer"
        Me.pnlContentContainer.Size = New System.Drawing.Size(800, 560)
        Me.pnlContentContainer.TabIndex = 1
        '
        'BaseUC (إعدادات الـ UserControl الأساسي)
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlContentContainer)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "BaseUC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes ' لدعم اللغة العربية
        Me.Size = New System.Drawing.Size(800, 600)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
    Friend WithEvents pnlContentContainer As System.Windows.Forms.Panel
End Class
