Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports Modules.OnsiteCourses.Views
Imports System.Collections.ObjectModel
Namespace Modules.OnsiteCourses.Views
    Public Class Add_OnsiteCoursesViewModel
        Inherits ViewModelBase
        Private _newView As Add_OnsiteCourses
        Private _Courses As ObservableCollection(Of Course)
        Private _selectedItem As Course
        Private _Onsite As New OnsiteCourse
        Private onsiteEdit As OnsiteCourse
        Private _Location As String
        Private _Days As String
        Private _Time As Date
        Private aceptCommand As ICommand
        Private cancelCommand As ICommand

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return _Courses
            End Get
            Set(value As ObservableCollection(Of Course))
                _Courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public Property SelectCourse As Course
            Get
                Return _selectedItem
            End Get
            Set(value As Course)
                _Onsite.Course = value
                _selectedItem = value
                OnPropertyChanged("SelectCourse")
            End Set
        End Property

        Public Property Location As String
            Get
                Return Me._Location
            End Get
            Set(value As String)
                Me._Location = value
                _Onsite.Location = _Location
                OnPropertyChanged("Location")
            End Set
        End Property

        Public Property Days As String
            Get
                Return Me._Days
            End Get
            Set(value As String)
                Me._Days = value
                _Onsite.Days = _Days
                OnPropertyChanged("Days")
            End Set
        End Property

        Public Property Time As Date
            Get
                Return Me._Time
            End Get
            Set(value As Date)
                Me._Time = value
                _Onsite.Time = _Time
                OnPropertyChanged("Time")
            End Set
        End Property

        Public ReadOnly Property AceptButton As ICommand
            Get
                If Me.aceptCommand Is Nothing Then
                    Me.aceptCommand = New RelayComand(AddressOf AceptExecute)
                End If
                Return Me.aceptCommand
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

        Sub AceptExecute()
            Try
                If onsiteEdit Is Nothing Then
                    DataContext.DBEntities.OnsiteCourses.Add(_Onsite)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim Onsite As OnsiteCourse = (From item In DataContext.DBEntities.OnsiteCourses Where item.CourseID = onsiteEdit.CourseID
                             Select item).FirstOrDefault()
                    Onsite.Course = _selectedItem
                    Onsite.Location = _Location
                    Onsite.Days = _Days
                    Onsite.Time = _Time
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("Error: Cant add Course")
            End Try
        End Sub

        Sub CancelExecute()
            _newView.Close()
        End Sub

        Sub New(ByRef newView As Add_OnsiteCourses)
            _newView = newView
            _Courses = New ObservableCollection(Of Course)
            Dim Course As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Course
                _Courses.Add(element)
            Next
        End Sub

        Sub New(ByRef newView As Add_OnsiteCourses, Onsite As OnsiteCourse)
            _newView = newView
            onsiteEdit = Onsite
            _Courses = New ObservableCollection(Of Course)
            Dim Course As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Course
                _Courses.Add(element)
            Next
            If Onsite Is Nothing Then
                Exit Sub
            End If
            _selectedItem = Onsite.Course
            _Location = Onsite.Location
            _Days = Onsite.Days
            _Time = Onsite.Time
        End Sub
    End Class
End Namespace
