Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Modules.Person.Views
Namespace Modules.Person.ViewModels
    Public Class PersonViewModel
        Inherits ViewModelBase

        Private _Persons As ObservableCollection(Of Global.Person)
        Private dataAccess As IPerson
        Private Shadows View As Add_Person
        Private _select As Global.Person
        Private addCommand As ICommand
        Private modCommand As ICommand
        Private deleteCommand As ICommand

        Public Property Persons As ObservableCollection(Of Global.Person)
            Get
                Return Me._Persons
            End Get
            Set(value As ObservableCollection(Of Global.Person))
                Me._Persons = value
                OnPropertyChanged("Persons")
            End Set
        End Property

        Public Property Selected As Global.Person
            Get
                Return _select
            End Get
            Set(value As Global.Person)
                _select = value
            End Set
        End Property

        Public ReadOnly Property AddButton As ICommand
            Get
                If Me.addCommand Is Nothing Then
                    Me.addCommand = New RelayComand(AddressOf AddExecute)
                End If
                Return Me.addCommand
            End Get
        End Property

        Public ReadOnly Property ModButton As ICommand
            Get
                If Me.modCommand Is Nothing Then
                    Me.modCommand = New RelayComand(AddressOf ModExecute)
                End If
                Return Me.modCommand
            End Get
        End Property

        Public ReadOnly Property DeleteButton As ICommand
            Get
                If Me.deleteCommand Is Nothing Then
                    Me.deleteCommand = New RelayComand(AddressOf DeleteExecute)
                End If
                Return Me.deleteCommand
            End Get
        End Property

        Private Function GetPerson() As IQueryable(Of Global.Person)
            Return Me.dataAccess.GetPerson
        End Function

        Sub New()
            Me._Persons = New ObservableCollection(Of Global.Person)
            Refresh()
        End Sub

        Sub AddExecute()
            Using Context As New SchoolEntities
                View = New Add_Person
                View.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub ModExecute()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    View = New Add_Person(Selected)
                    View.ShowDialog()
                    Refresh()
                End Using
            Else
                MessageBox.Show("Select a Person")
            End If
        End Sub

        Sub DeleteExecute()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.People.Remove((From item In DataContext.DBEntities.People
                                                      Where item.PersonID = Selected.PersonID
                                                      Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            Else
                MessageBox.Show("Select a Person")
            End If
        End Sub

        Sub Refresh()
            _Persons.Clear()
            ServiceLocator.RegisterService(Of IPerson)(New PersonService)
            Me.dataAccess = GetService(Of IPerson)()
            For Each element In Me.GetPerson
                Me._Persons.Add(element)
            Next
        End Sub
    End Class
End Namespace
