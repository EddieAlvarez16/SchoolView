Imports Modules.CoursesCrud.ViewModels
    Public Class CoursesCrusView
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.MainGrid.DataContext = New CoursesCrudViewModel(Me)
    End Sub
End Class
