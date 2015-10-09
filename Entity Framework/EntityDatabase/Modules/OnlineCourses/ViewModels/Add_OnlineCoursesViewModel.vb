Imports Modules.OnlineCourses.Views
Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Namespace Modules.OnlineCourses.ViewModels
    Public Class Add_OnlineCoursesViewModel
        Inherits ViewModelBase

        Private _newView As Add_OnlineCourses
        Private _Online As New OnlineCourse
        Private onlineEdit As OnlineCourse
        Private _Courses As ObservableCollection(Of Course)
        Private _selectItem As Course
        Private _Url As String
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
                Return _selectItem
            End Get
            Set(value As Course)
                _Online.Course = value
                _selectItem = value
                OnPropertyChanged("SelectCourse")
            End Set
        End Property

        Public Property Url As String
            Get
                Return Me._Url
            End Get
            Set(value As String)
                Me._Url = value
                _Online.URL = _Url
                OnPropertyChanged("Url")
            End Set
        End Property

        Public ReadOnly Property AcceptButton As ICommand
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
                    Me.cancelCommand = New RelayComand(AddressOf CanccelExecute)
                End If
                Return Me.cancelCommand
            End Get
        End Property

        Sub New(ByRef newView As Add_OnlineCourses)
            Me._newView = newView
            _Courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _Courses.Add(element)
            Next
        End Sub

        Sub New(ByRef newView As Add_OnlineCourses, Online As OnlineCourse)
            Me._newView = newView
            onlineEdit = Online
            _Courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _Courses.Add(element)
            Next
            If Online Is Nothing Then
                Exit Sub
            End If
            _selectItem = onlineEdit.Course
            _Url = onlineEdit.URL
        End Sub

        Sub AceptExecute()
            Try
                If onlineEdit Is Nothing Then
                    DataContext.DBEntities.OnlineCourses.Add(_Online)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim Online As OnlineCourse = (From item In DataContext.DBEntities.OnlineCourses Where item.CourseID = onlineEdit.CourseID
                                   Select item).FirstOrDefault()
                    Online.Course = _selectItem
                    Online.URL = _Url
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("Error: Cant add Online Course")
            End Try
        End Sub

        Sub CanccelExecute()
            _newView.Close()
        End Sub
    End Class
End Namespace
