
Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessLogic.Infrastructure
Imports BusinessObjects.Helpers
Namespace Modules.Courses.ViewModels
    Public Class CoursesViewModel
        Inherits ViewModelBase

        Public Shadows View As CoursesCrusView
        Private _courses As ObservableCollection(Of Course)
        Private dataAccess As ICourseService
        Private addComand As ICommand
        Private _select As Course
        Private _editCommand As ICommand
        Private _deleteCommand As ICommand

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public ReadOnly Property AddCourse
            Get
                If Me.addComand Is Nothing Then
                    Me.addComand = New RelayComand(AddressOf AddCourseExecute)
                End If
                Return Me.addComand
            End Get
        End Property

        Public Property Selected As Course
            Get
                Return _select
            End Get
            Set(value As Course)
                _select = value
            End Set
        End Property

        Private Function GetCourses() As IQueryable(Of Course)
            Return Me.dataAccess.GetCourses
        End Function

        Sub New()
            Me._courses = New ObservableCollection(Of Course)
            Refresh()
        End Sub

        Sub AddCourseExecute()
            Using context As New SchoolEntities
                View = New CoursesCrusView
                View.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub Refresh()
            Me._courses.Clear()
            ServiceLocator.RegisterService(Of ICourseService)(New CourseService)
            Me.dataAccess = GetService(Of ICourseService)()
            For Each elements In Me.GetCourses
                Me._courses.Add(elements)
            Next
        End Sub

        Public ReadOnly Property EditButton As ICommand
            Get
                If Me._editCommand Is Nothing Then
                    Me._editCommand = New RelayComand(AddressOf EditCourse)
                End If
                Return Me._editCommand
            End Get
        End Property

        Sub EditCourse()
            If Selected IsNot Nothing Then
                Using context As New SchoolEntities
                    View = New CoursesCrusView(Selected)
                    View.ShowDialog()
                    Refresh()
                End Using
            Else
                MessageBox.Show("You must select a course")
            End If
        End Sub

        Public ReadOnly Property DeleteButton As ICommand
            Get
                If Me._deleteCommand Is Nothing Then
                    Me._deleteCommand = New RelayComand(AddressOf DeleteCourse)
                End If
                Return Me._deleteCommand
            End Get
        End Property

        Sub DeleteCourse()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.Courses.Remove((From item In DataContext.DBEntities.Courses
                                                       Where item.CourseID = Selected.CourseID
                                                       Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            Else
                MessageBox.Show("You must select a course")
            End If
        End Sub
    End Class
End Namespace
