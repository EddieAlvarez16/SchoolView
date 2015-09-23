Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.Courses.ViewModels
    Public Class CoursesViewModel
        Inherits ViewModelBase

        Private _courses As ObservableCollection(Of Course)
        Private dataAccess As ICourseService

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Private Function GetCourses() As IQueryable(Of Course)
            Return Me.dataAccess.GetCourses
        End Function

        Sub New()
            Me._courses = New ObservableCollection(Of Course)
            ServiceLocator.RegisterService(Of ICourseService)(New CourseService)
            Me.dataAccess = GetService(Of ICourseService)()
            For Each elements In Me.GetCourses
                Me._courses.Add(elements)
            Next
        End Sub
    End Class
End Namespace
