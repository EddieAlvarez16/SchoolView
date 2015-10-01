Imports Modules.OfficeAsignament.ViewModels
Imports Modules.Courses.ViewModels
Imports Modules.OnlineCourses.ViewModels
Imports Modules.Departments.ViewModels
Imports Modules.OnsiteCourses.ViewModels
Imports Modules.StudentGrade.ViewModels
Imports Modules.Person.ViewModels
Class MainWindow
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.CoursesControl.DataContext = New CoursesViewModel
        Me.DepartmentControl.DataContext = New DepartmentsViewModel
        Me.OfficeControl.DataContext = New OfficeAsignamentViewModel
        Me.OnlineCoursesControl.DataContext = New OnlineCoursesViewModels
        Me.OnsiteCoursesControl.DataContext = New OnsiteCoursesViewModel
        Me.PersonControl.DataContext = New PersonViewModel
        Me.StudentControl.DataContext = New StudentGradeViewModel
    End Sub
End Class
