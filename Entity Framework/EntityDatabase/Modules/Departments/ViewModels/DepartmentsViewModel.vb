Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.Departments.Views

Namespace Modules.Departments.ViewModels
    Public Class DepartmentsViewModel
        Inherits ViewModelBase

        Private _departments As ObservableCollection(Of Department)
        Private dataAccess As IDepartmentService
        Private Shadows View As Add_Department
        Private _AddCommand As ICommand
        Private _ModCommand As ICommand
        Private _DeleteCommand As ICommand
        Private _Select As Department


        Public Property Selected As Department
            Get
                Return _Select
            End Get
            Set(value As Department)
                _Select = value
            End Set
        End Property
        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return Me._departments
            End Get
            Set(value As ObservableCollection(Of Department))
                Me._departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        Public ReadOnly Property AddButton As ICommand
            Get
                If Me._AddCommand Is Nothing Then
                    Me._AddCommand = New RelayComand(AddressOf AddExecute)
                End If
                Return Me._AddCommand
            End Get
        End Property

        Public ReadOnly Property ModButton As ICommand
            Get
                If Me._ModCommand Is Nothing Then
                    Me._ModCommand = New RelayComand(AddressOf ModExecute)
                End If
                Return Me._ModCommand
            End Get
        End Property

        Public ReadOnly Property DeleteButton As ICommand
            Get
                If Me._DeleteCommand Is Nothing Then
                    Me._DeleteCommand = New RelayComand(AddressOf DeleteExecute)
                End If
                Return Me._DeleteCommand
            End Get
        End Property

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.dataAccess.GetAllDepartments
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._departments = New ObservableCollection(Of Department)
            Refresh()
        End Sub

        Sub Refresh()
            Me._departments.Clear()
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            Me.dataAccess = GetService(Of IDepartmentService)()
            For Each element In Me.GetAllDepartments
                Me._departments.Add(element)
            Next
        End Sub

        Sub AddExecute()
            Using Contentx As New SchoolEntities
                View = New Add_Department
                View.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub ModExecute()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    View = New Add_Department(Selected)
                    View.ShowDialog()
                    Refresh()
                End Using
            Else
                MessageBox.Show("Select a Department")
            End If
        End Sub

        Sub DeleteExecute()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.Departments.Remove((From item In DataContext.DBEntities.Departments
                                                          Where item.DepartmentID = Selected.DepartmentID
                                                          Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            Else
                MessageBox.Show("Select a Department")
            End If
        End Sub
    End Class
End Namespace

