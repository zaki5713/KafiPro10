Imports System.Drawing
Imports System.Windows.Forms
Imports FontAwesome.Sharp
Imports KafiPro.UI.Shared.Themes

Namespace Global.KafiPro.UI.Shared.Controls

    ''' <summary>
    ''' زر مخصص بتصميم احترافي مع دعم الأيقونات و Hover
    ''' </summary>
    Public Class KafiProButton
        Inherits BaseIconControl

        ' ============ الحالات ============
        Private _normalBackColor As Color = UITheme.ButtonNormal
        Private _normalForeColor As Color = UITheme.TextPrimary
        Private _normalIconColor As Color = UITheme.TextPrimary

        Private _hoverBackColor As Color = UITheme.HoverBackground
        Private _hoverForeColor As Color = UITheme.HoverText
        Private _hoverIconColor As Color = UITheme.HoverText

        Private _activeBackColor As Color = UITheme.ActiveBackground
        Private _activeForeColor As Color = UITheme.ActiveText
        Private _activeIconColor As Color = UITheme.ActiveText

        Private _isHovering As Boolean = False

        ' ============ Public Properties ============

        Public Property NormalBackColor As Color
            Get
                Return _normalBackColor
            End Get
            Set(value As Color)
                _normalBackColor = value
                ApplyNormalState()
            End Set
        End Property

        Public Property NormalForeColor As Color
            Get
                Return _normalForeColor
            End Get
            Set(value As Color)
                _normalForeColor = value
                ApplyNormalState()
            End Set
        End Property

        Public Property NormalIconColor As Color
            Get
                Return _normalIconColor
            End Get
            Set(value As Color)
                _normalIconColor = value
                If Not _isHovering Then
                    Me.IconColor = _normalIconColor
                End If
            End Set
        End Property

        Public Property HoverBackColor As Color
            Get
                Return _hoverBackColor
            End Get
            Set(value As Color)
                _hoverBackColor = value
            End Set
        End Property

        Public Property HoverForeColor As Color
            Get
                Return _hoverForeColor
            End Get
            Set(value As Color)
                _hoverForeColor = value
            End Set
        End Property

        Public Property HoverIconColor As Color
            Get
                Return _hoverIconColor
            End Get
            Set(value As Color)
                _hoverIconColor = value
            End Set
        End Property

        Public Property ActiveBackColor As Color
            Get
                Return _activeBackColor
            End Get
            Set(value As Color)
                _activeBackColor = value
            End Set
        End Property

        Public Property ActiveForeColor As Color
            Get
                Return _activeForeColor
            End Get
            Set(value As Color)
                _activeForeColor = value
            End Set
        End Property

        Public Property ActiveIconColor As Color
            Get
                Return _activeIconColor
            End Get
            Set(value As Color)
                _activeIconColor = value
            End Set
        End Property

        ' ============ Constructor ============
        Public Sub New()
            MyBase.New()

            Me.FlatStyle = FlatStyle.Flat
            Me.FlatAppearance.BorderSize = 0
            Me.UseVisualStyleBackColor = False
            Me.TextImageRelation = TextImageRelation.ImageBeforeText
            Me.ImageAlign = ContentAlignment.MiddleLeft
            Me.TextAlign = ContentAlignment.MiddleCenter
            Me.Font = UITheme.FontBold
            Me.Cursor = Cursors.Hand

            ApplyNormalState()
        End Sub

        ' ============ Handlers ============
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            _isHovering = True
            Me.BackColor = _hoverBackColor
            Me.ForeColor = _hoverForeColor
            Me.IconColor = _hoverIconColor
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            _isHovering = False
            ApplyNormalState()
        End Sub

        Protected Overrides Sub OnMouseDown(mevent As MouseEventArgs)
            MyBase.OnMouseDown(mevent)
            Me.BackColor = _activeBackColor
            Me.ForeColor = _activeForeColor
            Me.IconColor = _activeIconColor
        End Sub

        Protected Overrides Sub OnMouseUp(mevent As MouseEventArgs)
            MyBase.OnMouseUp(mevent)
            If _isHovering Then
                Me.BackColor = _hoverBackColor
                Me.ForeColor = _hoverForeColor
                Me.IconColor = _hoverIconColor
            Else
                ApplyNormalState()
            End If
        End Sub

        Private Sub ApplyNormalState()
            Me.BackColor = _normalBackColor
            Me.ForeColor = _normalForeColor
            Me.IconColor = _normalIconColor
        End Sub

    End Class

End Namespace
