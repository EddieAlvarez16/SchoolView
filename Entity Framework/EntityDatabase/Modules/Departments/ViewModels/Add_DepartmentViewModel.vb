Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports Modules.Departments.Views

Namespace Modules.Departments.ViewModels
    Public Class Add_DepartmentViewModel
        Inherits ViewModelBase
        Private _Department As New Department
        Private _newView As Add_Department
        Private _Name As String
        Private _Budget As Decimal
        Private _Date As Date
        Private _Admin As Integer
        Private departEdit As Department
        Private _acceptCommand As ICommand
        Private _cancelCommand As ICommand

        Public Property Name As String
            Get
                Return Me._Name
            End Get
            Set(value As String)
                Me._Name = value
                _Department.Name = _Name
                OnPropertyChanged("Name")
            End Set
        End Property

        Public Property Budget As Decimal
            Get
                Return Me._Budget
            End Get
            Set(value As Decimal)
                Me._Budget = value
                _Department.Budget = _Budget
                OnPropertyChanged("Budget")
            End Set
        End Property

        Public Property StarDate As Date
            Get
                Return Me._Date
            End Get
            Set(value As Date)
                Me._Date = value
                _Department.StartDate = _Date
                OnPropertyChanged("StarDate")
            End Set
        End Property

        Public Property Administrator As Integer
            Get
                Return Me._Admin
            End Get
            Set(value As Integer)
                Me._Admin = value
                _Department.Administrator = _Admin
                OnPropertyChanged("Administrator")
            End Set
        End Property
        Public ReadOnly Property AcceptButton As ICommand
            Get
                If Me._acceptCommand Is Nothing Then
                    Me._acceptCommand = New RelayComand(AddressOf AcceptExecute)
                End If
                Return Me._acceptCommand
            End Get
        End Property

        Public ReadOnly Property CancelButton As ICommand
            Get
                If Me._cancelCommand Is Nothing Then
                    Me._cancelCommand = New RelayComand(AddressOf CancelExecute)
                End If
                Return Me._cancelCommand
            End Get
        End Property
        Sub New(ByRef newView As Add_Department)
            Me._newView = newView
        End Sub

        Sub New(ByRef newView As Add_Department, Department As Department)
            Me._newView = newView
            departEdit = Department
            If Department Is Nothing Then
                Exit Sub
            End If
            _Name = departEdit.Name
            _Budget = departEdit.Budget
            _Date = departEdit.StartDate
            _Admin = departEdit.Administrator
        End Sub

        Sub AcceptExecute()
            Try
                If departEdit Is Nothing Then
                    DataContext.DBEntities.Departments.Add(_Department)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim Department As Department = (From item In DataContext.DBEntities.Departments Where item.DepartmentID = _Department.DepartmentID
                             Select item).FirstOrDefault()
                    Department.Name = _Name
                    Department.Budget = _Budget
                    Department.StartDate = _Date
                    Department.Administrator = _Admin
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("Cant Add Department")
            End Try
        End Sub

        Sub CancelExecute()
            _newView.Close()
        End Sub
    End Class
End Namespace
