Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Interfaces
Namespace BusinessLogic.Services.Implementations
    Public Class OnlineCourseService
        Implements IOnlineCourse

        Public Function GetOnlineCourses() As IQueryable(Of OnlineCourse) Implements IOnlineCourse.GetOnlineCourses
            Return DataContext.DBEntities.OnlineCourses
        End Function
    End Class
End Namespace
