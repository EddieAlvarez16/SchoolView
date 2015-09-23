Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.Person.ViewModels
    Public Class PersonViewModel
        Inherits ViewModelBase

        Private _Persons As ObservableCollection(Of Global.Person)
        Private dataAccess As IPerson

        Public Property Persons As ObservableCollection(Of Global.Person)
            Get
                Return Me._Persons
            End Get
            Set(value As ObservableCollection(Of Global.Person))
                Me._Persons = value
                OnPropertyChanged("Persons")
            End Set
        End Property

        Private Function GetPerson() As IQueryable(Of Global.Person)
            Return Me.dataAccess.GetPerson
        End Function

        Sub New()
            Me._Persons = New ObservableCollection(Of Global.Person)
            ServiceLocator.RegisterService(Of IPerson)(New PersonService)
            Me.dataAccess = GetService(Of IPerson)()
            For Each element In Me.GetPerson
                Me._Persons.Add(element)
            Next
        End Sub
    End Class
End Namespace
