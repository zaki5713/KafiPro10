
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Public NotInheritable Class NavigationService

    Private Sub New()
    End Sub

#Region "Fields"

    Private Shared _hostPanel As Panel

    Private Shared ReadOnly _cache As New Dictionary(Of ModuleType, UserControl)

    Private Shared _currentModule As ModuleType

#End Region

#Region "Events"

    Public Shared Event ModuleChanged(moduleType As ModuleType)

#End Region

#Region "Initialize"

    Public Shared Sub Initialize(hostPanel As Panel)

        If hostPanel Is Nothing Then
            Throw New ArgumentNullException(NameOf(hostPanel))
        End If

        _hostPanel = hostPanel

    End Sub

#End Region

#Region "Properties"

    Public Shared ReadOnly Property CurrentModule As ModuleType
        Get
            Return _currentModule
        End Get
    End Property

#End Region

#Region "Navigation"

    Public Shared Sub Navigate(moduleType As ModuleType)

        If _hostPanel Is Nothing Then
            Throw New InvalidOperationException(
                "NavigationService has not been initialized.")
        End If

        'لا تعيد تحميل نفس الموديول
        If moduleType = _currentModule Then Exit Sub

        Dim item As NavigationItem =
            NavigationRegistry.GetItem(moduleType)

        Dim view As UserControl

        If _cache.ContainsKey(moduleType) Then

            view = _cache(moduleType)

        Else

            view = CType(Activator.CreateInstance(item.ViewType), UserControl)

            view.Dock = DockStyle.Fill

            _cache.Add(moduleType, view)

        End If

        _hostPanel.SuspendLayout()

        Try

            _hostPanel.Controls.Clear()

            _hostPanel.Controls.Add(view)

            view.BringToFront()

        Finally

            _hostPanel.ResumeLayout()

        End Try

        _currentModule = moduleType

        RaiseEvent ModuleChanged(moduleType)

    End Sub

#End Region

#Region "Cache"

    Public Shared Function IsCached(moduleType As ModuleType) As Boolean

        Return _cache.ContainsKey(moduleType)

    End Function

    Public Shared Function GetCachedView(moduleType As ModuleType) As UserControl

        If _cache.ContainsKey(moduleType) Then
            Return _cache(moduleType)
        End If

        Return Nothing

    End Function

    Public Shared Sub Remove(moduleType As ModuleType)

        If _cache.ContainsKey(moduleType) Then

            _cache(moduleType).Dispose()

            _cache.Remove(moduleType)

        End If

    End Sub

    Public Shared Sub ClearCache()

        For Each view As UserControl In _cache.Values
            view.Dispose()
        Next

        _cache.Clear()

    End Sub

#End Region

End Class