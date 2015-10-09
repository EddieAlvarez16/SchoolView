Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OnlineCourses.Views
Imports BusinessObjects.Helpers
Namespace Modules.OnlineCourses.ViewModels
    Public Class OnlineCoursesViewModels
        Inherits ViewModelBase

        Private _Courses As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCourse
        Private _selected As OnlineCourse
        Private addCommand As ICommand
        Private modCommand As ICommand
        Private deleteCommand As ICommand
        Private Shadows View As Add_OnlineCourses

        Public Property Courses As ObservableCollection(Of OnlineCourse)
            Get
                Return Me._Courses
            End Get
            Set(value As ObservableCollection(Of OnlineCourse))
                Me._Courses = value
                OnPropertyChanged("Courses")
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

        Public Property Selected As OnlineCourse
            Get
                Return _selected
            End Get
            Set(value As OnlineCourse)
                _selected = value
            End Set
        End Property

        Private Function GetCourses() As IQueryable(Of OnlineCourse)
            Return Me.dataAccess.GetOnlineCourses
        End Function

        Sub New()
            Me._Courses = New ObservableCollection(Of OnlineCourse)
            Refresh()
        End Sub

        Sub Refresh()
            Me._Courses.Clear()
            ServiceLocator.RegisterService(Of IOnlineCourse)(New OnlineCourseService)
            Me.dataAccess = GetService(Of IOnlineCourse)()
            For Each element In Me.GetCourses
                Me._Courses.Add(element)
            Next
        End Sub

        Sub AddExecute()
            Using Context As New SchoolEntities
                View = New Add_OnlineCourses
                View.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub ModExecute()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    View = New Add_OnlineCourses(Selected)
                    View.ShowDialog()
                    Refresh()
                End Using
            Else
                MessageBox.Show("Select a Course")
            End If
        End Sub

        Sub DeleteExecute()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.OnlineCourses.Remove((From item In DataContext.DBEntities.OnlineCourses
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
