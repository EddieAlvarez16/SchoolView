Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism.UnityExtensions
Imports Microsoft.Practices.Prism.Commands
Imports Microsoft.Practices.Prism.Events
Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessLogic.Infrastructure
Namespace Modules.Courses.ViewModels
    Public Class CoursesViewModel
        Inherits ViewModelBase

        Public Shadows View As CoursesCrusView
        Private _courses As ObservableCollection(Of Course)
        Private dataAccess As ICourseService
        Private _regionManager As IRegionManager
        Private _container As IUnityContainer
        Private addComand As ICommand
        Private _select As Course

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

        Sub New(ByVal regionManager As IRegionManager, ByVal container As IUnityContainer)

            _regionManager = regionManager
            _container = container

        End Sub

        Sub New()
            Me._courses = New ObservableCollection(Of Course)
            ServiceLocator.RegisterService(Of ICourseService)(New CourseService)
            Me.dataAccess = GetService(Of ICourseService)()
            For Each elements In Me.GetCourses
                Me._courses.Add(elements)
            Next
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
    End Class
End Namespace
