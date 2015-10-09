Imports Modules.OnlineCourses.ViewModels
Namespace Modules.OnlineCourses.Views
    Public Class Add_OnlineCourses
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.MainGrid.DataContext = New Add_OnlineCoursesViewModel(Me)
        End Sub

        Sub New(ByVal Online As OnlineCourse)
            InitializeComponent()
            Me.MainGrid.DataContext = New Add_OnlineCoursesViewModel(Me, Online)
        End Sub
    End Class
End Namespace
