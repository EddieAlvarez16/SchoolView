Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.StudentGrade.ViewModels
    Public Class StudentGradeViewModel
        Inherits ViewModelBase

        Private _Student As ObservableCollection(Of Global.StudentGrade)
        Private dataAccess As IStudentGrade

        Public Property Students As ObservableCollection(Of Global.StudentGrade)
            Get
                Return Me._Student
            End Get
            Set(value As ObservableCollection(Of Global.StudentGrade))
                Me._Student = value
                OnPropertyChanged("Students")
            End Set
        End Property

        Private Function GetGrade() As IQueryable(Of Global.StudentGrade)
            Return Me.dataAccess.GetGrade
        End Function

        Sub New()
            Me._Student = New ObservableCollection(Of Global.StudentGrade)
            ServiceLocator.RegisterService(Of IStudentGrade)(New StudentGradeService)
            Me.dataAccess = GetService(Of IStudentGrade)()
            For Each element In Me.GetGrade
                Me._Student.Add(element)
            Next
        End Sub
    End Class
End Namespace
