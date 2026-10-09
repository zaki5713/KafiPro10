<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblUserInfo = New System.Windows.Forms.Label()
        Me.lblAppName = New System.Windows.Forms.Label()
        Me.pnlSidebar = New System.Windows.Forms.Panel()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        ' pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblUserInfo)
        Me.pnlHeader.Controls.Add(Me.lblAppName)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1008, 45)
        Me.pnlHeader.TabIndex = 0
        '
        ' lblAppName
        '
        Me.lblAppName.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblAppName.Location = New System.Drawing.Point(808, 0)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.lblAppName.Size = New System.Drawing.Size(200, 45)
        Me.lblAppName.TabIndex = 0
        Me.lblAppName.Text = "KafiPro ERP"
        Me.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        ' lblUserInfo
        '
        Me.lblUserInfo.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblUserInfo.Location = New System.Drawing.Point(0, 0)
        Me.lblUserInfo.Name = "lblUserInfo"
        Me.lblUserInfo.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.lblUserInfo.Size = New System.Drawing.Size(400, 45)
        Me.lblUserInfo.TabIndex = 1
        Me.lblUserInfo.Text = "👤 admin   |   📅 2026-09-14"
        Me.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        ' pnlSidebar
        '
        Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSidebar.Location = New System.Drawing.Point(768, 45)
        Me.pnlSidebar.Name = "pnlSidebar"
        Me.pnlSidebar.Size = New System.Drawing.Size(240, 516)
        Me.pnlSidebar.TabIndex = 1
        '
        ' pnlContent
        '
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 45)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(768, 516)
        Me.pnlContent.TabIndex = 2
        '
        ' frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 561)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlSidebar)
        Me.Controls.Add(Me.pnlHeader)
        Me.MinimumSize = New System.Drawing.Size(1024, 600)
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KafiPro ERP"
        Me.pnlHeader.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblAppName As System.Windows.Forms.Label
    Friend WithEvents lblUserInfo As System.Windows.Forms.Label
    Friend WithEvents pnlSidebar As System.Windows.Forms.Panel
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
End Class