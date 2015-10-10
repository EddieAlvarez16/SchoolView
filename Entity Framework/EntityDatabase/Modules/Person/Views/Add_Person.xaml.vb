Imports Modules.Person.ViewModels
Namespace Modules.Person.Views
    Public Class Add_Person
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.MainGrid.DataContext = New Add_PersonViewModel(Me)
            Me.cb.Items.Add("Instructor")
            Me.cb.Items.Add("Student")
        End Sub

        Sub New(ByVal Person As Global.Person)
            InitializeComponent()
            Me.MainGrid.DataContext = New Add_PersonViewModel(Me, Person)
            Me.cb.Items.Add("Instructor")
            Me.cb.Items.Add("Student")
        End Sub
    End Class
End Namespace
