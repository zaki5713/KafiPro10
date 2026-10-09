Imports FontAwesome.Sharp

Public NotInheritable Class NavigationRegistry

	Private Sub New()
	End Sub



#Region "Fields"

	Private Shared ReadOnly _modules As New List(Of NavigationItem)





#End Region

#Region "Constructor"

	Shared Sub New()

		RegisterModules()

	End Sub





#End Region

#Region "Register"

	Private Shared Sub RegisterModules()

		_modules.Clear()

		'=========================================
		' Dashboard
		'=========================================
		Register(New NavigationItem(
			ModuleType.Dashboard,
			"الداشبورد",
			IconChar.ChartColumn,
			GetType(ProDashboardUC)))

		'=========================================
		' Production
		'=========================================
		Register(New NavigationItem(
			ModuleType.Production,
			"الإنتاج",
			IconChar.Industry,
			GetType(ProductionUC)))

		'=========================================
		' Quality
		'=========================================
		Register(New NavigationItem(
			ModuleType.Quality,
			"الجودة",
			IconChar.ChartLine,
			GetType(QualityUC)))

		'=========================================
		' Warehouses
		'=========================================
		Register(New NavigationItem(
			ModuleType.Warehouses,
			"المخازن",
			IconChar.ClipboardList,
			GetType(WarehousesUC)))

		'=========================================
		' Finance
		'=========================================
		Register(New NavigationItem(
			ModuleType.Finance,
			"المالية",
			IconChar.Coins,
			GetType(FinanceUC)))

		'=========================================
		' Sales
		'=========================================
		Register(New NavigationItem(
			ModuleType.Sales,
			"المبيعات",
			IconChar.ShoppingCart,
			GetType(SalesUC)))

		'=========================================
		' Transportation
		'=========================================
		Register(New NavigationItem(
			ModuleType.Transportation,
			"النقل",
			IconChar.Truck,
			GetType(TransportUC)))

		'=========================================
		' Settings
		'=========================================
		Register(New NavigationItem(
			ModuleType.Settings,
			"الإعدادات",
			IconChar.Gears,
			GetType(SettingsUC)))   ' ✅ تم إزالة parentModule

	End Sub





#End Region

#Region "Methods"

	Private Shared Sub Register(item As NavigationItem)

		If item Is Nothing Then
			Throw New ArgumentNullException(NameOf(item))
		End If

		If _modules.Any(Function(x) x.ModuleType = item.ModuleType) Then

			Throw New InvalidOperationException(
				$"Module '{item.ModuleType}' is already registered.")

		End If

		_modules.Add(item)

	End Sub

	Public Shared Function GetItem(moduleType As ModuleType) As NavigationItem

		Dim item =
			_modules.FirstOrDefault(
				Function(x) x.ModuleType = moduleType)

		If item Is Nothing Then

			Throw New KeyNotFoundException(
				$"Module '{moduleType}' is not registered.")

		End If

		Return item

	End Function

	Public Shared Function IsRegistered(moduleType As ModuleType) As Boolean

		Return _modules.Any(Function(x) x.ModuleType = moduleType)

	End Function





#End Region

#Region "Properties"

	Public Shared ReadOnly Property Modules As IReadOnlyList(Of NavigationItem)

		Get
			Return _modules
		End Get

	End Property



#End Region

End Class