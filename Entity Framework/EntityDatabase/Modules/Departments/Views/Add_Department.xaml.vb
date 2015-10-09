Imports Modules.Departments.ViewModels
Namespace Modules.Departments.Views
    Public Class Add_Department
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.MainGrid.DataContext = New Add_DepartmentViewModel(Me)
        End Sub

        Sub New(ByVal Department As Department)

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.MainGrid.DataContext = New Add_DepartmentViewModel(Me, Department)
        End Sub
    End Class
End Namespace
