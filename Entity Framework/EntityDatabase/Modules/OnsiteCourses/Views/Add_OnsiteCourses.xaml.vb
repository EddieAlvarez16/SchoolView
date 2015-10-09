Imports Modules.OnsiteCourses.ViewModels
Namespace Modules.OnsiteCourses.Views
    Public Class Add_OnsiteCourses
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.Maingrid.DataContext = New Add_OnsiteCoursesViewModel(Me)
        End Sub
    End Class
End Namespace
