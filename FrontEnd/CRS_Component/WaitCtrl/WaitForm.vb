Imports System.ComponentModel
Imports System.Drawing.Drawing2D
''' <summary>
''' WaitFrom , a waiting dialog.
''' Show some tip message with this dialog.
''' Lubin 2022-11-1 Created
''' </summary>
Partial Public Class WaitForm
    Inherits Form

    Private _parent As Form
    Private _timeWatcher As New Stopwatch
    Private _isShowTime As Boolean = False
    Public Sub New()
        InitializeComponent()
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub
    ''' <summary>
    ''' New method.
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    ''' <param name="parent">parent form</param>
    ''' <param name="isShowTime">Is show the time tip</param>
    Public Sub New(ByVal parent As Form, Optional isShowTime As Boolean = False)
        InitializeComponent()
        _parent = parent
        _isShowTime = isShowTime
        If parent IsNot Nothing Then
            Me.StartPosition = FormStartPosition.Manual
            Me.Location = New Point(parent.Location.X + parent.Width / 2 - Me.Width / 2, parent.Location.Y + parent.Height / 2 - Me.Height / 2)
        Else
            Me.StartPosition = FormStartPosition.CenterParent
        End If
        localTimer.Start()
    End Sub
    ''' <summary>
    ''' Change the windows style.
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    Public Sub SetWindowRegion()
        Dim FormPath As System.Drawing.Drawing2D.GraphicsPath
        FormPath = New System.Drawing.Drawing2D.GraphicsPath()
        Dim rect As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
        FormPath = GetRoundedRectPath(rect, 45)
        Me.Region = New Region(FormPath)
    End Sub

    Private Function GetRoundedRectPath(ByVal rect As Rectangle, ByVal radius As Integer) As GraphicsPath
        Dim diameter As Integer = radius
        Dim arcRect As Rectangle = New Rectangle(rect.Location, New Size(diameter, diameter))
        Dim path As GraphicsPath = New GraphicsPath()
        path.AddArc(arcRect, 180, 90)
        arcRect.X = rect.Right - diameter
        path.AddArc(arcRect, 270, 90)
        arcRect.Y = rect.Bottom - diameter
        path.AddArc(arcRect, 0, 90)
        arcRect.X = rect.Left
        path.AddArc(arcRect, 90, 90)
        path.CloseFigure()
        Return path
    End Function
    ''' <summary>
    ''' Change message.
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    ''' <param name="message">tip message</param>
    Public Sub ShowMessage(ByVal message As String)
        Me.BringToFront()
        Me.Activate()
        If Not String.IsNullOrEmpty(message) AndAlso Not IsNothing(_parent) Then
            ShowOrHideTime()
            '            _timeWatcher.Restart()
            _timeWatcher.Reset()
            _timeWatcher.Start()
            _parent.Invoke(Sub() tipMessageLab.Text = message)
        End If
        
    End Sub

#Region "moveing form"
    'API → Form1_load→Form1 Class
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (
                                                  ByVal hwnd As IntPtr,
                                                  ByVal wMsg As Integer,
                                                  ByVal wParam As Integer,
                                                  ByVal lParam As Integer) _
                                                  As Boolean
    Declare Function ReleaseCapture Lib "user32" Alias "ReleaseCapture" () As Boolean
    Const WM_SYSCOMMAND = &H112
    Const SC_MOVE = &HF010&
    Const HTCAPTION = 2
    Dim isMouseDown As Boolean = False
    Dim direction As MouseDirection = MouseDirection.None
    Dim mouseOff As Point
    ''' <summary>
    ''' Mouse enum
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    Public Enum MouseDirection
        Herizontal
        Vertical
        Declining
        None
    End Enum

     Private Sub mainPanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mainPanel.MouseMove, label1.MouseDown, tipTitleLab.MouseDown, tipMessageLab.MouseDown
        mouseOff = New Point(-e.X, -e.Y)
        If e.Location.X >= Me.Width - 5 AndAlso e.Location.Y > Me.Height - 5 Then
            isMouseDown = True
        ElseIf e.Location.X >= Me.Width - 5 Then
            isMouseDown = True
        ElseIf e.Location.Y >= Me.Height - 5 Then
            isMouseDown = True
        Else
            Me.Cursor = Cursors.Arrow
            'Change the cursor style
            isMouseDown = False
            'Moving event
            ReleaseCapture()
            SendMessage(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
        End If
    End Sub
    Private Sub mainPanel_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mainPanel.MouseMove, label1.MouseUp, tipTitleLab.MouseUp, tipMessageLab.MouseUp
        Console.WriteLine("release mouse")
        isMouseDown = False
        direction = MouseDirection.None
        If isMouseDown Then
            isMouseDown = False
        End If
    End Sub

    Private Sub mainPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)  Handles mainPanel.MouseMove, label1.MouseMove, tipTitleLab.MouseMove, tipMessageLab.MouseMove
        'ReleaseCapture()
        'SendMessage(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
        'Moveing
        If e.Location.X >= Me.Width - 5 AndAlso e.Location.Y > Me.Height - 5 Then
            Me.Cursor = Cursors.SizeNWSE
            direction = MouseDirection.Declining
        ElseIf e.Location.X >= Me.Width - 5 Then
            Me.Cursor = Cursors.SizeWE
            direction = MouseDirection.Herizontal
        ElseIf e.Location.Y >= Me.Height - 5 Then
            Me.Cursor = Cursors.SizeNS
            direction = MouseDirection.Vertical
        Else
            'otherwise outof the windows             
            Me.Cursor = Cursors.Arrow
        End If
        If e.Location.X >= (Me.Width + Me.Left + 10) OrElse (e.Location.Y > Me.Height + Me.Top + 10) Then
            isMouseDown = False
        End If
    End Sub

#End Region
    ''' <summary>
    ''' Close this form
    ''' Lubin 2022-11-1 Created
    ''' </summary>
    Public Sub CloseLoadingForm()
        Dim checkForIllegalCrossThreadCalls = System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls
        Try
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
            Me.DialogResult = DialogResult.OK
            Me.Close()

            If label1.Image IsNot Nothing Then
                label1.Image.Dispose()
            End If
            ReleaseCapture()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            'System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = checkForIllegalCrossThreadCalls
        End Try


    End Sub

    Private Sub WaitForm_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        SetWindowRegion()
    End Sub

    Private Sub WaitForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub
    Private Sub ShowOrHideTime()
        If (_isShowTime) Then
            timePassedLab.Show()
        Else
            timePassedLab.Hide()
        End If
    End Sub
    Private Sub localTimer_Tick(sender As Object, e As EventArgs) Handles localTimer.Tick
        If Not WaitWndFun.IsRunning Then
            Me.Close
            Exit sub
        End If
        ShowOrHideTime()
        Dim timePassed As Integer= Convert.ToInt32(_timeWatcher.Elapsed.TotalSeconds)
        timePassedLab.Text = timePassed.ToString
        Me.BringToFront()
        If timePassed Mod 6 =0 Then  Me.Activate()

    End Sub

End Class