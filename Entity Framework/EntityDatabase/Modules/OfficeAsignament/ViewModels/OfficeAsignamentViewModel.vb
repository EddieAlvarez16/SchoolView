Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Imports Modules.OfficeAsignament.Views
Namespace Modules.OfficeAsignament.ViewModels
    Public Class OfficeAsignamentViewModel
        Inherits ViewModelBase
        Private _Asignaments As ObservableCollection(Of OfficeAssignment)
        Private dataAccess As IOfficeAsignament
        Private addCommand As ICommand
        Private modCommand As ICommand
        Private deleteCommand As ICommand
        Private _Select As OfficeAssignment
        Private Shadows View As Add_OfficeAsignament

        Public Property Asignaments As ObservableCollection(Of OfficeAssignment)
            Get
                Return Me._Asignaments
            End Get
            Set(value As ObservableCollection(Of OfficeAssignment))
                Me._Asignaments = value
                OnPropertyChanged("Asignaments")
            End Set
        End Property

        Public Property Selected As OfficeAssignment
            Get
                Return _Select
            End Get
            Set(value As OfficeAssignment)
                _Select = value
            End Set
        End Property

        Public ReadOnly Property AddButton As ICommand
            Get
                If Me.addCommand Is Nothing Then
                    Me.addCommand = New RelayComand(AddressOf AddExecute)
                End If
                Return Me.addCommand
            End Get
        End Property

        Public ReadOnly Property ModButton As ICommand
            Get
                If Me.modCommand Is Nothing Then
                    Me.modCommand = New RelayComand(AddressOf ModExecute)
                End If
                Return Me.modCommand
            End Get
        End Property

        Public ReadOnly Property DeleteButton As ICommand
            Get
                If Me.deleteCommand Is Nothing Then
                    Me.deleteCommand = New RelayComand(AddressOf DeleteExecute)
                End If
                Return Me.deleteCommand
            End Get
        End Property

        Private Function GetAsignaments() As IQueryable(Of OfficeAssignment)
            Return Me.dataAccess.GetAsignament
        End Function

        Sub New()
            Me._Asignaments = New ObservableCollection(Of OfficeAssignment)
            Refresh()
        End Sub

        Sub Refresh()
            Me._Asignaments.Clear()
            ServiceLocator.RegisterService(Of IOfficeAsignament)(New OfficeAsignamentService)
            Me.dataAccess = GetService(Of IOfficeAsignament)()
            For Each element In Me.GetAsignaments
                Me._Asignaments.Add(element)
            Next
        End Sub

        Sub AddExecute()
            Using Context As New SchoolEntities
                View = New Add_OfficeAsignament
                View.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub ModExecute()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    View = New Add_OfficeAsignament(Selected)
                    View.ShowDialog()
                    Refresh()
                End Using
            Else
                MessageBox.Show("Selec a Office Asignament")
            End If
        End Sub

        Sub DeleteExecute()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.OfficeAssignments.Remove((From item In DataContext.DBEntities.OfficeAssignments
                                                                 Where item.InstructorID = Selected.InstructorID
                                                                 Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            Else
                MessageBox.Show("Select a Office Asignament")
            End If
        End Sub
    End Class
End Namespace
