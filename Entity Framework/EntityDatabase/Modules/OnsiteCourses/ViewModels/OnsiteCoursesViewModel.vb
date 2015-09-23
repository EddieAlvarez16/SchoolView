Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.OnsiteCourses.ViewModels
    Public Class OnsiteCoursesViewModel
        Inherits ViewModelBase

        Private _Courses As ObservableCollection(Of OnsiteCourse)
        Private dataAccess As IOnsiteCourse

        Public Property CoursesOnsite As ObservableCollection(Of OnsiteCourse)
            Get
                Return Me._Courses
            End Get
            Set(value As ObservableCollection(Of OnsiteCourse))
                Me._Courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Private Function GetOnsiteCourses() As IQueryable(Of OnsiteCourse)
            Return Me.dataAccess.GetOnsiteCourses
        End Function

        Sub New()
            Me._Courses = New ObservableCollection(Of OnsiteCourse)
            ServiceLocator.RegisterService(Of IOnsiteCourse)(New OnsiteCoursesService)
            Me.dataAccess = GetService(Of IOnsiteCourse)()
            For Each element In Me.GetOnsiteCourses
                Me._Courses.Add(element)
            Next
        End Sub
    End Class
End Namespace
