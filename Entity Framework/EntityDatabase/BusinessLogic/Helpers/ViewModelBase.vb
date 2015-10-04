Imports System.ComponentModel
Imports BusinessLogic.Services
Namespace BusinessLogic.Helpers
    Public MustInherit Class ViewModelBase
        Implements INotifyPropertyChanged

        Dim myServiceLocator As New ServiceLocator

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(ByVal strPropertyName As String)

            If Me.PropertyChangedEvent IsNot Nothing Then
                RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(strPropertyName))
            End If

        End Sub

        Private privateThrowOnInvalidPropertyName As Boolean
        Protected Overridable Property ThrowOnInvalidPropertyName() As Boolean
            Get
                Return privateThrowOnInvalidPropertyName
            End Get
            Set(ByVal value As Boolean)
                privateThrowOnInvalidPropertyName = value
            End Set
        End Property

        <Conditional("DEBUG"), DebuggerStepThrough()> _
        Public Sub VerifyPropertyName(ByVal propertyName As String)
            ' Verify that the property name matches a real,   
            ' public, instance property on this object. 
            If TypeDescriptor.GetProperties(Me)(propertyName) Is Nothing Then
                Dim msg As String = "Invalid property name: " & propertyName

                If Me.ThrowOnInvalidPropertyName Then
                    Throw New Exception(msg)
                Else
                    Debug.Fail(msg)
                End If
            End If
        End Sub

        Private privateDisplayName As String
        Public Overridable Property DisplayName() As String
            Get
                Return privateDisplayName
            End Get
            Protected Set(ByVal value As String)
                privateDisplayName = value
            End Set
        End Property

        Public Function ServiceLocator() As ServiceLocator
            Return Me.myServiceLocator
        End Function

        Public Function GetService(Of T)() As T
            Return myServiceLocator.GetService(Of T)()
        End Function
    End Class

    Public Class RelayComand
        Implements ICommand
#Region "Declarations"
        Private ReadOnly _CanExecute As Func(Of Boolean)
        Private ReadOnly _Execute As Action(Of Object)
#End Region
#Region "Constructors"
        Public Sub New(ByVal execute As Action(Of Object))
            Me.New(execute, Nothing)
        End Sub
        Public Sub New(ByVal execute As Action(Of Object), ByVal canExecute As Func(Of Boolean))
            If execute Is Nothing Then
                Throw New ArgumentNullException("execute")
            End If
            _Execute = execute
            _CanExecute = canExecute
        End Sub
#End Region
#Region "ICommand"
        Public Custom Event CanExecuteChanged As EventHandler Implements System.Windows.Input.ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                If _CanExecute IsNot Nothing Then
                    AddHandler CommandManager.RequerySuggested, value
                End If
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                If _CanExecute IsNot Nothing Then
                    RemoveHandler CommandManager.RequerySuggested, value
                End If
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
                CommandManager.InvalidateRequerySuggested()
            End RaiseEvent
        End Event
        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
            If _CanExecute Is Nothing Then
                Return True
            Else
                Return _CanExecute.Invoke
            End If
        End Function
        Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
            _Execute(parameter)
        End Sub
#End Region
    End Class
End Namespace
