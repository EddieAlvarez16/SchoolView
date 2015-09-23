Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.OfficeAsignament.ViewModels
    Public Class OfficeAsignamentViewModel
        Inherits ViewModelBase
        Private _Asignaments As ObservableCollection(Of OfficeAssignment)
        Private dataAccess As IOfficeAsignament

        Public Property Asignaments As ObservableCollection(Of OfficeAssignment)
            Get
                Return Me._Asignaments
            End Get
            Set(value As ObservableCollection(Of OfficeAssignment))
                Me._Asignaments = value
                OnPropertyChanged("Asignaments")
            End Set
        End Property

        Private Function GetAsignaments() As IQueryable(Of OfficeAssignment)
            Return Me.dataAccess.GetAsignament
        End Function

        Sub New()
            Me._Asignaments = New ObservableCollection(Of OfficeAssignment)
            ServiceLocator.RegisterService(Of IOfficeAsignament)(New OfficeAsignamentService)
            Me.dataAccess = GetService(Of IOfficeAsignament)()
            For Each element In Me.GetAsignaments
                Me._Asignaments.Add(element)
            Next
        End Sub
    End Class
End Namespace
