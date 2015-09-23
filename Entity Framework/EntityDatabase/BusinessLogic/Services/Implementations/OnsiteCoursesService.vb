Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Interfaces
Namespace BusinessLogic.Services.Implementations
    Public Class OnsiteCoursesService
        Implements IOnsiteCourse

        Public Function GetOnsiteCourses() As IQueryable(Of OnsiteCourse) Implements IOnsiteCourse.GetOnsiteCourses
            Return DataContext.DBEntities.OnsiteCourses
        End Function
    End Class
End Namespace
