Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OnsiteCourses.Views
Namespace Modules.OnsiteCourses.ViewModels
    Public Class OnsiteCoursesViewModel
        Inherits ViewModelBase

        Private _Courses As ObservableCollection(Of OnsiteCourse)
        Private dataAccess As IOnsiteCourse
        Private Shadows View As Add_OnsiteCourses
        Private addCommand As ICommand
        Private modCommand As ICommand
        Private deleteCommand As ICommand
        Private _select As OnsiteCourse

        Public Property CoursesOnsite As ObservableCollection(Of OnsiteCourse)
            Get
                Return Me._Courses
            End Get
            Set(value As ObservableCollection(Of OnsiteCourse))
                Me._Courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public Property Selected As OnsiteCourse
            Get
                Return _select
            End Get
            Set(value As OnsiteCourse)
                _select = value
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
                    Me.deleteCommand = New RelayComand(AddressOf DeleteExevute)
                End If
                Return Me.deleteCommand
            End Get
        End Property

        Private Function GetOnsiteCourses() As IQueryable(Of OnsiteCourse)
            Return Me.dataAccess.GetOnsiteCourses
        End Function

        Sub New()
            Me._Courses = New ObservableCollection(Of OnsiteCourse)
            Refresh()
        End Sub

        Sub Refresh()
            _Courses.Clear()
            ServiceLocator.RegisterService(Of IOnsiteCourse)(New OnsiteCoursesService)
            Me.dataAccess = GetService(Of IOnsiteCourse)()
            For Each element In Me.GetOnsiteCourses
                Me._Courses.Add(element)
            Next
        End Sub

        Sub AddExecute()
            Using Context As New SchoolEntities
                View = New Add_OnsiteCourses
                View.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub ModExecute()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    View = New Add_OnsiteCourses(Selected)
                    View.ShowDialog()
                    Refresh()
                End Using
            Else
                MessageBox.Show("Select a Course")
            End If
        End Sub

        Sub DeleteExevute()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.OnsiteCourses.Remove((From item In DataContext.DBEntities.OnsiteCourses
                                                             Where item.CourseID = Selected.CourseID
                                                             Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            Else
                MessageBox.Show("Select a Course")
            End If
        End Sub
    End Class
End Namespace
