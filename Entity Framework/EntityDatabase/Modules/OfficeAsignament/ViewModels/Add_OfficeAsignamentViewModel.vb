Imports BusinessLogic.Helpers
Imports Modules.OfficeAsignament.Views
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Namespace Modules.OfficeAsignament.ViewModels
    Public Class Add_OfficeAsignamentViewModel
        Inherits ViewModelBase
        Private _newView As Add_OfficeAsignament
        Private _Office As New OfficeAssignment
        Private officeEdit As OfficeAssignment
        Private _Instructors As ObservableCollection(Of Global.Person)
        Private _selectedItem As Global.Person
        Private _Location As String
        Private _Timestap As Byte()
        Private acceptCommand As ICommand
        Private cancelCommand As ICommand

        Public Property Instructors As ObservableCollection(Of Global.Person)
            Get
                Return _Instructors
            End Get
            Set(value As ObservableCollection(Of Global.Person))
                _Instructors = value
                OnPropertyChanged("Instructors")
            End Set
        End Property

        Public Property SelectInstructor As Global.Person
            Get
                Return _selectedItem
            End Get
            Set(value As Global.Person)
                _Office.Person = value
                _selectedItem = value
                OnPropertyChanged("SelectInstructor")
            End Set
        End Property

        Public Property Location As String
            Get
                Return Me._Location
            End Get
            Set(value As String)
                Me._Location = value
                _Office.Location = _Location
                OnPropertyChanged("Location")
            End Set
        End Property

        Public Property Timestap As Byte()
            Get
                Return Me._Timestap
            End Get
            Set(value As Byte())
                Me._Timestap = value
                _Office.Timestamp = _Timestap
                OnPropertyChanged("Timestap")
            End Set
        End Property

        Public ReadOnly Property AcceptButton As ICommand
            Get
                If Me.acceptCommand Is Nothing Then
                    Me.acceptCommand = New RelayComand(AddressOf AceptExecute)
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
        Sub New(ByRef newView As Add_OfficeAsignament)
            Me._newView = newView
            _Instructors = New ObservableCollection(Of Global.Person)
            Dim Person As IQueryable(Of Global.Person) = DataContext.DBEntities.People
            For Each element In Person
                _Instructors.Add(element)
            Next
        End Sub

        Sub New(ByRef newView As Add_OfficeAsignament, Office As OfficeAssignment)
            Me._newView = newView
            officeEdit = Office
            _Instructors = New ObservableCollection(Of Global.Person)
            Dim Person As IQueryable(Of Global.Person) = DataContext.DBEntities.People
            For Each element In Person
                _Instructors.Add(element)
            Next
            If Office Is Nothing Then
                Exit Sub
            End If
            _selectedItem = officeEdit.Person
            _Location = officeEdit.Location
            _Timestap = officeEdit.Timestamp
        End Sub

        Sub AceptExecute()
            Try
                If officeEdit Is Nothing Then
                    DataContext.DBEntities.OfficeAssignments.Add(_Office)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim Office As OfficeAssignment = (From item In DataContext.DBEntities.OfficeAssignments Where item.InstructorID = _Office.InstructorID
                             Select item).FirstOrDefault()
                    Office.Person = _selectedItem
                    Office.Location = _Location
                    Office.Timestamp = _Timestap
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("Cant add OfficeAsignament")
            End Try
        End Sub

        Sub CancelExecute()
            _newView.Close()
        End Sub
    End Class
End Namespace
