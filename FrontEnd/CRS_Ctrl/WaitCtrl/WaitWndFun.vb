Imports System.ComponentModel
Imports System.Threading
''' <summary>
''' Wait windows Call API
''' Lubin 2022-11-1 Created
''' </summary>
Public Class WaitWndFun
    Private Shared _clsthread As Thread
    Private Shared _runthread As Thread
   
    Private Shared _isRunning As Boolean = False

    Private Shared _isShowTime As Boolean = True

    Private shared _message As String = ""
    Private _checkForIllegalCrossThreadCalls = False

    Private shared _lockObj As New Object


    Private Shared Property LatestWaitFrm As WaitForm
    Private Shared Property Loadthread As Thread
    Private Shared Property LatestParentFrm As Form

    Private shared Sub Show()
        If (Not IsNothing(LatestParentFrm)) Then
            Loadthread = New Thread(New ThreadStart(AddressOf LoadingProcessEx))
            Loadthread.Start()
        Else
            Throw New Exception("Please set _parentFrm at first!")
        End If
    End Sub

    Private Shared _executeAction As Action
    ''' <summary>
    ''' A wait dialog will show after this function be invoke.
    ''' And it will close after the action execute finished.
    ''' </summary>
    ''' <param name="parent">Parent Form</param>
    ''' <param name="message">Message</param>
    ''' <param name="action">action which should be invoke.</param>
    ''' <param name="isShowTime">is show time.</param>
    Public shared Sub Execute(ByVal parent As Form, ByVal message As String, ByVal action As Action, Optional isShowTime As Boolean = True)
        If Not IsNothing(LatestWaitFrm) AndAlso _isRunning Then 
            ShowMessage(message)
            action()
            exit sub
        End If
        _isShowTime = isShowTime
        _isRunning = True
        LatestParentFrm = parent
        _executeAction = action
        _thrExp = Nothing

        '_checkForIllegalCrossThreadCalls = System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Show()
        ShowMessage(message)
        _runthread = New Thread(New ThreadStart(AddressOf DoWork))
        _runthread.Start()
        Try
            WaitFinished()
        Finally
            _isRunning = False
            Close()
        End Try

        'System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = _checkForIllegalCrossThreadCalls
    End Sub
     Private Shared Sub DoWork()
        Try
            _executeAction()
            '_parentFrm _executeAction()
        Catch ex As Exception
            _thrExp = ex
        End Try
    End Sub

    Private shared Sub WaitFinished()
        While ((_runthread.IsAlive AndAlso _isRunning) OrElse (Not IsNothing(_clsthread) AndAlso _clsthread.IsAlive))
            Application.DoEvents()
            Thread.Sleep(1000)
        End While
        If Not IsNothing(_thrExp) Then Throw _thrExp
    End Sub

    Private Shared _thrExp As Exception
    

    ''' <summary>
    ''' Show message
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    ''' <param name="parent">Parent form</param>
    ''' <param name="message">message</param>
    Public Shared Sub ShowMessage(parent As Form, ByVal message As String)
         _message = message
        If Not IsNothing(LatestWaitFrm) Then 
            LatestWaitFrm.ShowMessage(_message)
        else
            LatestParentFrm = parent
            LatestWaitFrm= New WaitForm(LatestParentFrm, _isShowTime)
            LatestWaitFrm.ShowMessage(_message)
        End If
       
        
    End Sub
     ''' <summary>
    ''' Show message
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    ''' <param name="message">message</param>
    Public Shared Sub ShowMessage(ByVal message As String)
        _message = message
        If LatestWaitFrm IsNot Nothing Then
            LatestWaitFrm.ShowMessage(_message)
            LatestWaitFrm.TopMost=True
        End If
    End Sub

    ''' <summary>
    ''' Close the dialog.
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    Public Shared Sub Close()
        If LatestWaitFrm IsNot Nothing Then
            _clsthread = New Thread(New ThreadStart(AddressOf LatestWaitFrm.CloseLoadingForm))
            _clsthread.Start()
            LatestWaitFrm = Nothing
            Loadthread = Nothing
            _thrExp = Nothing
        End If
    End Sub

    Private shared Sub LoadingProcessEx()
        SyncLock _lockObj
            Try
                If Not _isRunning Then Exit Sub
                LatestWaitFrm = New WaitForm(LatestParentFrm, _isShowTime)
                LatestWaitFrm.ShowMessage(_message)
                LatestWaitFrm.ShowDialog()
                if Not Isnothing(LatestWaitFrm) Then LatestWaitFrm.TopMost=True
            Catch ex As Exception
                Console.WriteLine(ex.Message & "/" & ex.StackTrace)
            End Try
        End SyncLock
    End Sub
    ''' <summary>
    ''' Check is running.
    ''' </summary>
    ''' <returns>boolean </returns>
    Public shared Readonly Property IsRunning As Boolean
        Get 
            Return _isRunning
        End Get
    End property
End Class
