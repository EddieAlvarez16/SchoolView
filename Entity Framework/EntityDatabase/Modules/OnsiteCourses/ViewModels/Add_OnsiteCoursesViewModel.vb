Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports Modules.OnsiteCourses.Views
Imports System.Collections.ObjectModel
Namespace Modules.OnsiteCourses.Views
    Public Class Add_OnsiteCoursesViewModel
        Private _newView As Add_OnsiteCourses
        Private _Courses As ObservableCollection(Of Course)

        Sub New(ByRef newView As Add_OnsiteCourses)
            _newView = newView
        End Sub
    End Class
End Namespace
