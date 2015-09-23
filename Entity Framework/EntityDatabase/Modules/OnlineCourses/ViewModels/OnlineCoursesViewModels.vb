Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.OnlineCourses.ViewModels
    Public Class OnlineCoursesViewModels
        Inherits ViewModelBase

        Private _Courses As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCourse

        Public Property Courses As ObservableCollection(Of OnlineCourse)
            Get
                Return Me._Courses
            End Get
            Set(value As ObservableCollection(Of OnlineCourse))
                Me._Courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Private Function GetCourses() As IQueryable(Of OnlineCourse)
            Return Me.dataAccess.GetOnlineCourses
        End Function

        Sub New()
            Me._Courses = New ObservableCollection(Of OnlineCourse)
            ServiceLocator.RegisterService(Of IOnlineCourse)(New OnlineCourseService)
            Me.dataAccess = GetService(Of IOnlineCourse)()
            For Each element In Me.GetCourses
                Me._Courses.Add(element)
            Next
        End Sub
    End Class
End Namespace
