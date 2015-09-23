Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Interfaces
Namespace BusinessLogic.Services.Implementations
    Public Class CourseService
        Implements ICourseService


        Public Function GetCourses() As IQueryable(Of Course) Implements ICourseService.GetCourses
            Return DataContext.DBEntities.Courses
        End Function
    End Class
End Namespace
