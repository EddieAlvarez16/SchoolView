Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Interfaces
Namespace BusinessLogic.Services.Implementations
    Public Class StudentGradeService
        Implements IStudentGrade

        Public Function GetGrade() As IQueryable(Of StudentGrade) Implements IStudentGrade.GetGrade
            Return DataContext.DBEntities.StudentGrades
        End Function
    End Class
End Namespace
