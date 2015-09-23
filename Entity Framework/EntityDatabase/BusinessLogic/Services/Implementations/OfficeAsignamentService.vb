Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Interfaces
Namespace BusinessLogic.Services.Implementations
    Public Class OfficeAsignamentService
        Implements IOfficeAsignament

        Public Function GetAsignament() As IQueryable(Of OfficeAssignment) Implements IOfficeAsignament.GetAsignament
            Return DataContext.DBEntities.OfficeAssignments
        End Function
    End Class
End Namespace
