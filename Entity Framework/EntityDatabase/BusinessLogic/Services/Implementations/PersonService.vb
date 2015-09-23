Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Interfaces
Namespace BusinessLogic.Services.Implementations
    Public Class PersonService
        Implements IPerson

        Public Function GetPerson() As IQueryable(Of Person) Implements IPerson.GetPerson
            Return DataContext.DBEntities.People
        End Function
    End Class
End Namespace
