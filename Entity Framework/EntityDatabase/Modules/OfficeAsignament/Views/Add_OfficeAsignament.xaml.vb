Imports Modules.OfficeAsignament.ViewModels
Namespace Modules.OfficeAsignament.Views
    Public Class Add_OfficeAsignament
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.MainGrid.DataContext = New Add_OfficeAsignamentViewModel(Me)
        End Sub

        Sub New(ByVal Office As OfficeAssignment)
            InitializeComponent()
            Me.MainGrid.DataContext = New Add_OfficeAsignamentViewModel(Me, Office)
        End Sub
    End Class
End Namespace
