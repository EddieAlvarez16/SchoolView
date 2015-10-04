Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Namespace Modules.CoursesCrud.ViewModels
    Public Class CoursesCrudViewModel
        Inherits ViewModelBase
        Private _Course As New Course
        Public _newView As CoursesCrusView
        Private _courses As String
        Private _departments As ObservableCollection(Of Department)
        Private _selectedDepartment As Department
        Private _credits As Integer
        Private _aceptCommand As ICommand
        Private _courseEdit As Course
        Private _Id As Integer
        Private _cancelCommand As ICommand

        Public Property Title As String
            Get
                Return Me._courses
            End Get
            Set(value As String)
                Me._courses = value
                _Course.Title = _courses
                OnPropertyChanged("Title")
            End Set
        End Property

        Public Property Id As Integer
            Get
                Return Me._Id
            End Get
            Set(value As Integer)
                Me._Id = value
                _Course.CourseID = _Id
                OnPropertyChanged("Id")
            End Set
        End Property

        Public ReadOnly Property AceptButton As ICommand
            Get
                If Me._aceptCommand Is Nothing Then
                    Me._aceptCommand = New RelayComand(AddressOf AceptComand)
                End If
                Return Me._aceptCommand
            End Get
        End Property

        Public ReadOnly Property CancelButton As ICommand
            Get
                If Me._cancelCommand Is Nothing Then
                    Me._cancelCommand = New RelayComand(AddressOf CancelAction)
                End If
                Return Me._cancelCommand
            End Get
        End Property

        Public Property Credit As Integer
            Get
                Return Me._credits
            End Get
            Set(value As Integer)
                Me._credits = value
                _Course.Credits = _credits
                OnPropertyChanged("Credit")
            End Set
        End Property

        Sub AceptComand()
            Try
                If _courseEdit Is Nothing Then
                    DataContext.DBEntities.Courses.Add(_Course)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim course As Course = (From item In DataContext.DBEntities.Courses Where item.CourseID = _Course.CourseID
                             Select item).FirstOrDefault()

                    course.CourseID = _Id
                    course.Title = _courses
                    course.Credits = _credits
                    course.Department = _selectedDepartment
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("Cant add the Course")
            End Try
        End Sub

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return _departments
            End Get
            Set(value As ObservableCollection(Of Department))
                _departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        Public Property Department As Department
            Get
                Return _selectedDepartment
            End Get
            Set(value As Department)
                _Course.Department = value
                _selectedDepartment = value
                OnPropertyChanged("Department")
            End Set
        End Property
        Sub CancelAction()
            _newView.Close()
        End Sub

        Sub New(ByRef newView As CoursesCrusView)
            Me._newView = newView
            _departments = New ObservableCollection(Of Department)
            Dim departments As IQueryable(Of Department) = DataContext.DBEntities.Departments
            For Each element In departments
                _departments.Add(element)
            Next
        End Sub
    End Class
End Namespace
