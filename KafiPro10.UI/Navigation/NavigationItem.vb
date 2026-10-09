Imports FontAwesome.Sharp
Public Class NavigationItem

    Public Property ModuleType As ModuleType
    Public Property DisplayName As String
    Public Property Icon As IconChar
    Public Property ViewType As Type
    Public Property ParentModule As ModuleType?

    Public Sub New()
    End Sub

    Public Sub New(moduleType As ModuleType,
                   displayName As String,
                   icon As IconChar,
                   viewType As Type,
                   Optional parentModule As ModuleType? = Nothing)

        Me.ModuleType = moduleType
        Me.DisplayName = displayName
        Me.Icon = icon
        Me.ViewType = viewType
        Me.ParentModule = parentModule

    End Sub

End Class