Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports Modules.Person.Views
Namespace Modules.Person.ViewModels
    Public Class Add_PersonViewModel
        Inherits ViewModelBase
        Private _newView As Add_Person
        Private _Person As New Global.Person
        Private personEdit As Global.Person
        Private _First As String
        Private _Last As String
        Private _hireDate As Date
        Private _Enrollment As Date
        Private acceptCommand As ICommand
        Private cancelCommand As ICommand

        Public Property First As String
            Get
                Return Me._First
            End Get
            Set(value As String)
                Me._First = value
                _Person.FirstName = _First
                OnPropertyChanged("First")
            End Set
        End Property

        Public Property Last As String
            Get
                Return Me._Last
            End Get
            Set(value As String)
                Me._Last = value
                _Person.LastName = _Last
                OnPropertyChanged("Last")
            End Set
        End Property

        Public Property HireDate As Date
            Get
                Return Me._hireDate
            End Get
            Set(value As Date)
                Me._hireDate = value
                _Person.HireDate = _hireDate
                OnPropertyChanged("HireDate")
            End Set
        End Property

        Public Property Enrollment As Date
            Get
                Return Me._Enrollment
            End Get
            Set(value As Date)
                Me._Enrollment = value
                _Person.EnrollmentDate = _Enrollment
                OnPropertyChanged("Enrollment")
            End Set
        End Property

        Public ReadOnly Property AceptButton As ICommand
            Get
                If Me.acceptCommand Is Nothing Then
                    Me.acceptCommand = New RelayComand(AddressOf AcceptExecute)
                End If
                Return Me.acceptCommand
            End Get
        End Property

        Public ReadOnly Property CancelButton As ICommand
            Get
                If Me.cancelCommand Is Nothing Then
                    Me.cancelCommand = New RelayComand(AddressOf CancelExecute)
                End If
                Return Me.cancelCommand
            End Get
        End Property


        Sub New(ByRef newView As Add_Person)
            Me._newView = newView
        End Sub

        Sub New(ByRef newView As Add_Person, Person As Global.Person)
            Me._newView = newView
            personEdit = Person
            If Person Is Nothing Then
                Exit Sub
            End If
            _First = personEdit.FirstName
            _Last = personEdit.LastName
            If personEdit.HireDate IsNot Nothing Then
                _hireDate = personEdit.HireDate
            End If
            If personEdit.EnrollmentDate IsNot Nothing Then
                _Enrollment = personEdit.EnrollmentDate
            End If
        End Sub

        Sub AcceptExecute()
            Try
                If personEdit Is Nothing Then
                    DataContext.DBEntities.People.Add(_Person)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim Person As Global.Person = (From item In DataContext.DBEntities.People Where item.PersonID = _Person.PersonID
                             Select item).FirstOrDefault()
                    Person.FirstName = _First
                    Person.LastName = _Last
                    Person.HireDate = _hireDate
                    Person.EnrollmentDate = _Enrollment
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("Error: Cant add Person")
            End Try
        End Sub

        Sub CancelExecute()
            _newView.Close()
        End Sub
    End Class
End Namespace
