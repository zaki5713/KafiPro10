Imports System.Drawing
Imports System.Windows.Forms
Imports FontAwesome.Sharp

Namespace Global.KafiPro.UI.Shared.Controls

    ''' <summary>
    ''' كلاس أساسي للأزرار مع أيقونات
    ''' </summary>
    Public MustInherit Class BaseIconControl
        Inherits Button

        Private _iconChar As IconChar = IconChar.None
        Private _iconSize As Integer = 16
        Private _iconColor As Color = Color.Black

        Public Property IconChar As IconChar
            Get
                Return _iconChar
            End Get
            Set(value As IconChar)
                _iconChar = value
                UpdateImage()
            End Set
        End Property

        Public Property IconSize As Integer
            Get
                Return _iconSize
            End Get
            Set(value As Integer)
                _iconSize = value
                UpdateImage()
            End Set
        End Property

        Public Property IconColor As Color
            Get
                Return _iconColor
            End Get
            Set(value As Color)
                _iconColor = value
                UpdateImage()
            End Set
        End Property

        Private Sub UpdateImage()
            If _iconChar = IconChar.None Then
                Me.Image = Nothing
                Return
            End If

            Try
                Me.Image = _iconChar.ToBitmap(_iconColor, _iconSize)
            Catch
                Me.Image = Nothing
            End Try
        End Sub

    End Class

End Namespace
