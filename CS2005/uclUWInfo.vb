Imports System.Data.SqlClient

Public Class UWInfo

    Inherits System.Windows.Forms.UserControl

    Private ds As DataSet = New DataSet("UnderWriteInfo")
    Private strPolicy As String
    Private dr, dr1 As DataRow
    Private bm As BindingManagerBase
    Friend WithEvents txtUWS As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Private datFLDate As DateTime
	Friend WithEvents TextBoxChequeDate As TextBox
	Friend WithEvents TextBoxChequeDestination As TextBox
	Friend WithEvents LabelChequeDate As Label
	Friend WithEvents LabelChequeDestination As Label
	Dim IsCNB As Boolean    ' ES009

#Region " Windows Form Designer generated code "
    Public Sub New(init As Boolean)
        MyBase.New()
    End Sub
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdUW As System.Windows.Forms.DataGrid
    Friend WithEvents txtAppDate As System.Windows.Forms.TextBox
    Friend WithEvents txtRecvDate As System.Windows.Forms.TextBox
    Friend WithEvents txtInforce As System.Windows.Forms.TextBox
    Friend WithEvents txtRCD As System.Windows.Forms.TextBox
    Friend WithEvents txtDelDate As System.Windows.Forms.TextBox
    Friend WithEvents txtFLDate As System.Windows.Forms.TextBox
    Friend WithEvents txtFLD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtOutstand As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.txtAppDate = New System.Windows.Forms.TextBox()
		Me.txtRecvDate = New System.Windows.Forms.TextBox()
		Me.txtInforce = New System.Windows.Forms.TextBox()
		Me.txtRCD = New System.Windows.Forms.TextBox()
		Me.txtDelDate = New System.Windows.Forms.TextBox()
		Me.txtFLDate = New System.Windows.Forms.TextBox()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.grdUW = New System.Windows.Forms.DataGrid()
		Me.txtFLD = New System.Windows.Forms.TextBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.txtOutstand = New System.Windows.Forms.TextBox()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.txtStatus = New System.Windows.Forms.TextBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.txtRemark = New System.Windows.Forms.TextBox()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.txtUWS = New System.Windows.Forms.TextBox()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.TextBoxChequeDate = New System.Windows.Forms.TextBox()
		Me.TextBoxChequeDestination = New System.Windows.Forms.TextBox()
		Me.LabelChequeDate = New System.Windows.Forms.Label()
		Me.LabelChequeDestination = New System.Windows.Forms.Label()
		CType(Me.grdUW,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'txtAppDate
		'
		Me.txtAppDate.AcceptsReturn = True
		Me.txtAppDate.BackColor = System.Drawing.SystemColors.Window
		Me.txtAppDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAppDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtAppDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAppDate.Location = New System.Drawing.Point(109, 4)
		Me.txtAppDate.MaxLength = 0
		Me.txtAppDate.Name = "txtAppDate"
		Me.txtAppDate.ReadOnly = True
		Me.txtAppDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAppDate.Size = New System.Drawing.Size(81, 20)
		Me.txtAppDate.TabIndex = 23
		'
		'txtRecvDate
		'
		Me.txtRecvDate.AcceptsReturn = True
		Me.txtRecvDate.BackColor = System.Drawing.SystemColors.Window
		Me.txtRecvDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRecvDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtRecvDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRecvDate.Location = New System.Drawing.Point(109, 28)
		Me.txtRecvDate.MaxLength = 0
		Me.txtRecvDate.Name = "txtRecvDate"
		Me.txtRecvDate.ReadOnly = True
		Me.txtRecvDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRecvDate.Size = New System.Drawing.Size(81, 20)
		Me.txtRecvDate.TabIndex = 22
		'
		'txtInforce
		'
		Me.txtInforce.AcceptsReturn = True
		Me.txtInforce.BackColor = System.Drawing.SystemColors.Window
		Me.txtInforce.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtInforce.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtInforce.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtInforce.Location = New System.Drawing.Point(286, 28)
		Me.txtInforce.MaxLength = 0
		Me.txtInforce.Name = "txtInforce"
		Me.txtInforce.ReadOnly = True
		Me.txtInforce.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtInforce.Size = New System.Drawing.Size(81, 20)
		Me.txtInforce.TabIndex = 21
		'
		'txtRCD
		'
		Me.txtRCD.AcceptsReturn = True
		Me.txtRCD.BackColor = System.Drawing.SystemColors.Window
		Me.txtRCD.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRCD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtRCD.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRCD.Location = New System.Drawing.Point(286, 4)
		Me.txtRCD.MaxLength = 0
		Me.txtRCD.Name = "txtRCD"
		Me.txtRCD.ReadOnly = True
		Me.txtRCD.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRCD.Size = New System.Drawing.Size(81, 20)
		Me.txtRCD.TabIndex = 20
		'
		'txtDelDate
		'
		Me.txtDelDate.AcceptsReturn = True
		Me.txtDelDate.BackColor = System.Drawing.SystemColors.Window
		Me.txtDelDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDelDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtDelDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDelDate.Location = New System.Drawing.Point(486, 4)
		Me.txtDelDate.MaxLength = 0
		Me.txtDelDate.Name = "txtDelDate"
		Me.txtDelDate.ReadOnly = True
		Me.txtDelDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDelDate.Size = New System.Drawing.Size(81, 20)
		Me.txtDelDate.TabIndex = 19
		'
		'txtFLDate
		'
		Me.txtFLDate.AcceptsReturn = True
		Me.txtFLDate.BackColor = System.Drawing.SystemColors.Window
		Me.txtFLDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtFLDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtFLDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtFLDate.Location = New System.Drawing.Point(486, 28)
		Me.txtFLDate.MaxLength = 0
		Me.txtFLDate.Name = "txtFLDate"
		Me.txtFLDate.ReadOnly = True
		Me.txtFLDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtFLDate.Size = New System.Drawing.Size(81, 20)
		Me.txtFLDate.TabIndex = 18
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.BackColor = System.Drawing.SystemColors.Control
		Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label6.Location = New System.Drawing.Point(18, 7)
		Me.Label6.Name = "Label6"
		Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label6.Size = New System.Drawing.Size(85, 13)
		Me.Label6.TabIndex = 17
		Me.Label6.Text = "Application Date"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.BackColor = System.Drawing.SystemColors.Control
		Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label5.Location = New System.Drawing.Point(250, 8)
		Me.Label5.Name = "Label5"
		Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label5.Size = New System.Drawing.Size(30, 13)
		Me.Label5.TabIndex = 16
		Me.Label5.Text = "RCD"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Location = New System.Drawing.Point(374, 8)
		Me.Label4.Name = "Label4"
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.Size = New System.Drawing.Size(102, 13)
		Me.Label4.TabIndex = 15
		Me.Label4.Text = "Policy Delivery Date"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Location = New System.Drawing.Point(25, 32)
		Me.Label3.Name = "Label3"
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.Size = New System.Drawing.Size(78, 13)
		Me.Label3.TabIndex = 14
		Me.Label3.Text = "App. Received"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Location = New System.Drawing.Point(214, 32)
		Me.Label2.Name = "Label2"
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.Size = New System.Drawing.Size(66, 13)
		Me.Label2.TabIndex = 13
		Me.Label2.Text = "Inforce Date"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Location = New System.Drawing.Point(378, 32)
		Me.Label1.Name = "Label1"
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.Size = New System.Drawing.Size(100, 13)
		Me.Label1.TabIndex = 12
		Me.Label1.Text = "Free Look Deadline"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'grdUW
		'
		Me.grdUW.AlternatingBackColor = System.Drawing.Color.White
		Me.grdUW.BackColor = System.Drawing.Color.White
		Me.grdUW.BackgroundColor = System.Drawing.Color.Ivory
		Me.grdUW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.grdUW.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
		Me.grdUW.CaptionForeColor = System.Drawing.Color.Lavender
		Me.grdUW.CaptionVisible = False
		Me.grdUW.DataMember = ""
		Me.grdUW.FlatMode = True
		Me.grdUW.Font = New System.Drawing.Font("Tahoma", 8!)
		Me.grdUW.ForeColor = System.Drawing.Color.Black
		Me.grdUW.GridLineColor = System.Drawing.Color.Wheat
		Me.grdUW.HeaderBackColor = System.Drawing.Color.CadetBlue
		Me.grdUW.HeaderFont = New System.Drawing.Font("Tahoma", 8!, System.Drawing.FontStyle.Bold)
		Me.grdUW.HeaderForeColor = System.Drawing.Color.Black
		Me.grdUW.LinkColor = System.Drawing.Color.DarkSlateBlue
		Me.grdUW.Location = New System.Drawing.Point(14, 89)
		Me.grdUW.Name = "grdUW"
		Me.grdUW.ParentRowsBackColor = System.Drawing.Color.Ivory
		Me.grdUW.ParentRowsForeColor = System.Drawing.Color.Black
		Me.grdUW.SelectionBackColor = System.Drawing.Color.Wheat
		Me.grdUW.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
		Me.grdUW.Size = New System.Drawing.Size(684, 126)
		Me.grdUW.TabIndex = 24
		'
		'txtFLD
		'
		Me.txtFLD.AcceptsReturn = True
		Me.txtFLD.BackColor = System.Drawing.SystemColors.Window
		Me.txtFLD.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtFLD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtFLD.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtFLD.Location = New System.Drawing.Point(662, 4)
		Me.txtFLD.MaxLength = 0
		Me.txtFLD.Name = "txtFLD"
		Me.txtFLD.ReadOnly = True
		Me.txtFLD.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtFLD.Size = New System.Drawing.Size(81, 20)
		Me.txtFLD.TabIndex = 26
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.BackColor = System.Drawing.SystemColors.Control
		Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label7.Location = New System.Drawing.Point(574, 8)
		Me.Label7.Name = "Label7"
		Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label7.Size = New System.Drawing.Size(89, 13)
		Me.Label7.TabIndex = 25
		Me.Label7.Text = "Policy Issue Date"
		'
		'txtOutstand
		'
		Me.txtOutstand.AcceptsReturn = True
		Me.txtOutstand.BackColor = System.Drawing.SystemColors.Window
		Me.txtOutstand.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtOutstand.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtOutstand.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtOutstand.Location = New System.Drawing.Point(94, 221)
		Me.txtOutstand.MaxLength = 0
		Me.txtOutstand.Name = "txtOutstand"
		Me.txtOutstand.ReadOnly = True
		Me.txtOutstand.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtOutstand.Size = New System.Drawing.Size(316, 20)
		Me.txtOutstand.TabIndex = 28
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.BackColor = System.Drawing.SystemColors.Control
		Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label8.Location = New System.Drawing.Point(18, 225)
		Me.Label8.Name = "Label8"
		Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label8.Size = New System.Drawing.Size(64, 13)
		Me.Label8.TabIndex = 27
		Me.Label8.Text = "Outstanding"
		'
		'txtStatus
		'
		Me.txtStatus.AcceptsReturn = True
		Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
		Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtStatus.Location = New System.Drawing.Point(466, 221)
		Me.txtStatus.MaxLength = 0
		Me.txtStatus.Name = "txtStatus"
		Me.txtStatus.ReadOnly = True
		Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtStatus.Size = New System.Drawing.Size(81, 20)
		Me.txtStatus.TabIndex = 30
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.BackColor = System.Drawing.SystemColors.Control
		Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label9.Location = New System.Drawing.Point(422, 225)
		Me.Label9.Name = "Label9"
		Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label9.Size = New System.Drawing.Size(37, 13)
		Me.Label9.TabIndex = 29
		Me.Label9.Text = "Status"
		'
		'txtRemark
		'
		Me.txtRemark.AcceptsReturn = True
		Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
		Me.txtRemark.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRemark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtRemark.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRemark.Location = New System.Drawing.Point(94, 245)
		Me.txtRemark.MaxLength = 0
		Me.txtRemark.Name = "txtRemark"
		Me.txtRemark.ReadOnly = True
		Me.txtRemark.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.txtRemark.Size = New System.Drawing.Size(604, 20)
		Me.txtRemark.TabIndex = 32
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.BackColor = System.Drawing.SystemColors.Control
		Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label10.Location = New System.Drawing.Point(18, 249)
		Me.Label10.Name = "Label10"
		Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label10.Size = New System.Drawing.Size(44, 13)
		Me.Label10.TabIndex = 31
		Me.Label10.Text = "Remark"
		'
		'txtUWS
		'
		Me.txtUWS.AcceptsReturn = True
		Me.txtUWS.BackColor = System.Drawing.SystemColors.Window
		Me.txtUWS.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtUWS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.txtUWS.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtUWS.Location = New System.Drawing.Point(94, 271)
		Me.txtUWS.MaxLength = 0
		Me.txtUWS.Multiline = True
		Me.txtUWS.Name = "txtUWS"
		Me.txtUWS.ReadOnly = True
		Me.txtUWS.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtUWS.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.txtUWS.Size = New System.Drawing.Size(604, 271)
		Me.txtUWS.TabIndex = 33
		'
		'Label11
		'
		Me.Label11.BackColor = System.Drawing.SystemColors.Control
		Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label11.Location = New System.Drawing.Point(18, 274)
		Me.Label11.Name = "Label11"
		Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label11.Size = New System.Drawing.Size(70, 121)
		Me.Label11.TabIndex = 34
		Me.Label11.Text = "Underwriting Worksheet"
		'
		'TextBoxChequeDate
		'
		Me.TextBoxChequeDate.AcceptsReturn = true
		Me.TextBoxChequeDate.BackColor = System.Drawing.SystemColors.Window
		Me.TextBoxChequeDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TextBoxChequeDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.TextBoxChequeDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TextBoxChequeDate.Location = New System.Drawing.Point(286, 54)
		Me.TextBoxChequeDate.MaxLength = 0
		Me.TextBoxChequeDate.Name = "TextBoxChequeDate"
		Me.TextBoxChequeDate.ReadOnly = true
		Me.TextBoxChequeDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TextBoxChequeDate.Size = New System.Drawing.Size(81, 20)
		Me.TextBoxChequeDate.TabIndex = 38
		'
		'TextBoxChequeDestination
		'
		Me.TextBoxChequeDestination.AcceptsReturn = true
		Me.TextBoxChequeDestination.BackColor = System.Drawing.SystemColors.Window
		Me.TextBoxChequeDestination.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TextBoxChequeDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.TextBoxChequeDestination.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TextBoxChequeDestination.Location = New System.Drawing.Point(110, 54)
		Me.TextBoxChequeDestination.MaxLength = 0
		Me.TextBoxChequeDestination.Name = "TextBoxChequeDestination"
		Me.TextBoxChequeDestination.ReadOnly = true
		Me.TextBoxChequeDestination.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TextBoxChequeDestination.Size = New System.Drawing.Size(81, 20)
		Me.TextBoxChequeDestination.TabIndex = 37
		'
		'LabelChequeDate
		'
		Me.LabelChequeDate.AutoSize = true
		Me.LabelChequeDate.BackColor = System.Drawing.SystemColors.Control
		Me.LabelChequeDate.Cursor = System.Windows.Forms.Cursors.Default
		Me.LabelChequeDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.LabelChequeDate.ForeColor = System.Drawing.SystemColors.ControlText
		Me.LabelChequeDate.Location = New System.Drawing.Point(210, 57)
		Me.LabelChequeDate.Name = "LabelChequeDate"
		Me.LabelChequeDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.LabelChequeDate.Size = New System.Drawing.Size(70, 13)
		Me.LabelChequeDate.TabIndex = 36
		Me.LabelChequeDate.Text = "Cheque Date"
		'
		'LabelChequeDestination
		'
		Me.LabelChequeDestination.AutoSize = true
		Me.LabelChequeDestination.BackColor = System.Drawing.SystemColors.Control
		Me.LabelChequeDestination.Cursor = System.Windows.Forms.Cursors.Default
		Me.LabelChequeDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
		Me.LabelChequeDestination.ForeColor = System.Drawing.SystemColors.ControlText
		Me.LabelChequeDestination.Location = New System.Drawing.Point(4, 56)
		Me.LabelChequeDestination.Name = "LabelChequeDestination"
		Me.LabelChequeDestination.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.LabelChequeDestination.Size = New System.Drawing.Size(100, 13)
		Me.LabelChequeDestination.TabIndex = 35
		Me.LabelChequeDestination.Text = "Cheque Destination"
		'
		'UWInfo
		'
		Me.Controls.Add(Me.TextBoxChequeDate)
		Me.Controls.Add(Me.TextBoxChequeDestination)
		Me.Controls.Add(Me.LabelChequeDate)
		Me.Controls.Add(Me.LabelChequeDestination)
		Me.Controls.Add(Me.Label11)
		Me.Controls.Add(Me.txtUWS)
		Me.Controls.Add(Me.txtRemark)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.txtStatus)
		Me.Controls.Add(Me.Label9)
		Me.Controls.Add(Me.txtOutstand)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.txtFLD)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.grdUW)
		Me.Controls.Add(Me.txtAppDate)
		Me.Controls.Add(Me.txtRecvDate)
		Me.Controls.Add(Me.txtInforce)
		Me.Controls.Add(Me.txtRCD)
		Me.Controls.Add(Me.txtDelDate)
		Me.Controls.Add(Me.txtFLDate)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Name = "UWInfo"
		Me.Size = New System.Drawing.Size(815, 584)
		CType(Me.grdUW,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub

#End Region

    Public WriteOnly Property srcDTUWInf(ByVal datFLD As DateTime)
        Set(ByVal Value)
            If Not Value Is Nothing Then
                datFLDate = datFLD
                strPolicy = Value
                'ds.Tables.Add(Value)
                Call buildUI()
            End If
        End Set
    End Property

    ' **** ES009 begin ****
    Private Function GetWebServiceClient() As CommonWS.Service
        Dim ws As New CommonWS.Service()
        Dim header As New CommonWS.DBSOAPHeader()
        Dim strUrl As String = ""

        Dim CNBComHeader As New Utility.Utility.ComHeader
        CNBComHeader.UserID = gsUser
        CNBComHeader.CompanyID = g_Comp
        CNBComHeader.EnvironmentUse = g_Env
        CNBComHeader.ProjectAlias = "NBS"

        'AC - Change to use configuration setting - start
        '#If UAT = 0 Then
        '        CNBComHeader.ConnectionAlias = "INGCNBPRD01"
        '#Else
        '        CNBComHeader.ConnectionAlias = "INGCNBUAT01"
        '#End If
        'If gUAT = False Then
        '    CNBComHeader.ConnectionAlias = "INGCNBPRD01"
        'Else
        '    CNBComHeader.ConnectionAlias = g_Connection_CNB
        'End If
        'AC - Change to use configuration setting - end


        CNBComHeader.UserType = "UPDATE"
        CNBComHeader.UseLocalWS = "N"

        strUrl = Utility.Utility.GetWebServiceURL("COMMONWS", CNBComHeader, gobjMQQueHeader)
        If strUrl.Trim.Length = 0 Then
            Throw New Exception("UseLocalWS:" & CNBComHeader.UseLocalWS & ";EnvironmentUse:" & CNBComHeader.EnvironmentUse)
        Else
            ws.Url = strUrl
        End If
        ws.Credentials = System.Net.CredentialCache.DefaultCredentials

        header.Project = CNBComHeader.ProjectAlias
        header.ConnectionAlias = CNBComHeader.ConnectionAlias
        header.UserType = CNBComHeader.UserType
        header.Comp = CNBComHeader.CompanyID
        header.Env = CNBComHeader.EnvironmentUse
        header.User = CNBComHeader.UserID

        'the default time out value is 2 minutes, change if needed
        ws.Timeout = 180000
        ws.DBSOAPHeaderValue = header
        Return ws
    End Function

    Private Function getPendingRequestItems(ByVal strAgentNo As String, ByRef PendingItems As DataTable, ByRef UWComment As DataTable, ByRef strError As String, Optional ByVal strPolicyNo As String = "") As Boolean
        Dim ws As CommonWS.Service = Nothing
        Dim ds As New DataSet
        'Dim dtRece As New DataTable
        Try
            ws = Me.GetWebServiceClient()
            If Not ws.getPendingRequestItems(strAgentNo, ds, strError, strPolicyNo) Then
                Return False
            Else
                PendingItems = ds.Tables(0)
            End If

            ds = New DataSet
            If Not ws.getUWComment(strAgentNo, strPolicyNo, ds, strError) Then
                Return False
            Else
                UWComment = ds.Tables(0)
            End If

            'ITSR933 FG R5 Policy Number Change by Gary Lei Start
            'Get uw comment of both Capsil and LA policy
            If UWComment Is Nothing OrElse UWComment.Rows.Count = 0 Then
                Dim strPolicyCap As String = GetCapsilPolicyNo(strPolicyNo)
                If strPolicyCap.TrimEnd <> "" And strPolicyCap.TrimEnd <> strPolicyNo.TrimEnd Then
                    ds = New DataSet
                    If Not ws.getUWComment(strAgentNo, strPolicyCap, ds, strError) Then
                        Return False
                    Else
                        UWComment = ds.Tables(0)
                    End If
                End If
            End If
            'ITSR933 FG R5 Policy Number Change by Gary Lei End

            Return True
        Catch ex As Exception
            strError = ex.ToString
            Return False
        Finally
            If ds IsNot Nothing Then ds.Dispose()
            'If dtRece IsNot Nothing Then dtRece.Dispose()
        End Try
        ' **** ES009 end ****
    End Function

    Private Sub buildUI()

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable

        ' inner join may have problem, change temp.
        ''Dim dt As DataTable
        ''Dim lngErrNo As Long
        ''Dim strErrMsg As String
        ''lngErrNo = 0
        ''strErrMsg = ""
        ''dt = objCS.GetUWInfo(strPolicy, lngErrNo, strErrMsg)

        ''If lngErrNo <> 0 Then
        ''    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        ''    Exit Sub
        ''End If

        ''If Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
        ''    Exit Sub
        ''Else
        ''    ds.Tables.Add(dt)
        ''End If

        ' **** ES009 begin ****
        Dim PendingItems As New DataTable
        Dim UWComment As DataTable
        Dim UWRow As DataRow
        Dim strErr As String
        Dim PendingAgent As DataTable
        Dim PendingAgtCd As String

        strsql = "select agentcode from csw_poli_rel r, customer c " & _
            " where r.policyrelatecode = 'WA'" & _
            " and r.customerid = c.customerid " & _
            " and r.policyaccountid = '" & strPolicy & "'"
        If GetDT(strsql, strCIWConn, PendingAgent, strErr) Then
            If Not PendingAgent Is Nothing AndAlso PendingAgent.Rows.Count > 0 Then
                PendingAgtCd = PendingAgent.Rows(0).Item("AgentCode")
            End If
        End If

        If GetNBRPolicy(strPolicy, strErr) Then
            IsCNB = True
            getPendingRequestItems(PendingAgtCd, PendingItems, UWComment, strErr, strPolicy)
            If strErr <> "" Then
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            Else
                PendingItems.TableName = "UWInfo"
                ds.Tables.Add(PendingItems.Copy)
            End If
        End If

        If IsCNB Then

            Try
                strSQL = "Select * from csw_policy_uw " & _
                    " Where cswpuw_poli_id = '" & strPolicy & "'; "
                sqlconnect.ConnectionString = GetConnectionStringByCompanyID(strCompany)
                sqlda = New SqlDataAdapter(strSQL, sqlconnect)
                sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough
                sqlda.Fill(ds, "CswUWInfo")

            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try
            sqlconnect.Dispose()

            If ds.Tables("UWInfo").Rows.Count > 0 Then
                With ds.Tables("UWInfo").Rows(0)

                    Dim ts As New clsDataGridTableStyle
                    Dim cs As DataGridTextBoxColumn

                    cs = New DataGridTextBoxColumn
                    cs.Width = 200
                    cs.MappingName = "description"
                    cs.HeaderText = "Outstanding"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    cs = New DataGridTextBoxColumn
                    cs.Width = 50
                    cs.MappingName = "outstandingStatus"
                    cs.HeaderText = "Status"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    cs = New DataGridTextBoxColumn
                    cs.Width = 1200
                    cs.MappingName = "outstandingRemark"
                    cs.HeaderText = "Remark"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    ts.MappingName = "UWInfo"
                    grdUW.TableStyles.Add(ts)

                    grdUW.DataSource = ds.Tables("UWInfo")
                    grdUW.AllowDrop = False
                    grdUW.ReadOnly = True

                End With
                bm = Me.BindingContext(ds.Tables("UWInfo"))
            End If

            If Not UWComment Is Nothing Then
                txtUWS.Text = ""
                For Each UWdr As DataRow In UWComment.Rows
                    txtUWS.Text &= UWdr.Item(2) & vbCrLf
                Next
                'If UWComment.Rows.Count > 0 Then
                '    txtUWS.Text = UWComment.Rows(0).Item(2)
                'End If
            End If
        Else
            ' **** ES009 end ****

            Try
                strSQL = "Select * from nbrvw_outstanding_req; "
                strSQL &= "Select * from nbrvw_uw_advice " & _
                    " Where nbruad_policy_no = '" & strPolicy & "'; "
                strSQL &= "Select * from csw_policy_uw " & _
                    " Where cswpuw_poli_id = '" & strPolicy & "'; "

                ' **** ES006 begin ****
                If g_Comp <> "HKL" Then
                    strSQL &= "Select * from " & gcNBR & "nbr_uw_worksheet " & _
                        " Where nbruwk_new_policy_no = '" & strPolicy & "'"
                Else
                    Label1.Visible = False
                    txtUWS.Visible = False
                End If
                ' **** ES006 end ****

                sqlconnect.ConnectionString = GetConnectionStringByCompanyID(strCompany)
                sqlda = New SqlDataAdapter(strSQL, sqlconnect)
                sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough
                sqlda.TableMappings.Add("nbrvw_outstanding_req1", "UWInfo")
                sqlda.TableMappings.Add("nbrvw_outstanding_req2", "CswUWInfo")

                ' **** ES006 begin ****
                If g_Comp <> "HKL" Then
                    sqlda.TableMappings.Add("nbrvw_outstanding_req3", "UWSheet")
                End If
                ' **** ES006 end ****

                sqlda.Fill(ds, "nbrvw_outstanding_req")

            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            sqlconnect.Dispose()

            Dim relOutRel As New Data.DataRelation("OutRel", ds.Tables("nbrvw_outstanding_req").Columns("nbrosr_requirement_cd"), _
                ds.Tables("UWInfo").Columns("nbruad_outstanding_code"), True)

            Try
                ds.Relations.Add(relOutRel)
            Catch sqlex As SqlClient.SqlException
                'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Catch ex As Exception
                'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            End Try

            If ds.Tables("UWInfo").Rows.Count > 0 Then
                With ds.Tables("UWInfo").Rows(0)

                    With ds.Tables("UWInfo")
                        .Columns.Add("nbrosr_english_desc", GetType(String))
                    End With

                    Dim ts As New clsDataGridTableStyle
                    Dim cs As DataGridTextBoxColumn

                    cs = New JoinTextBoxColumn("OutRel", ds.Tables("nbrvw_outstanding_req").Columns("nbrosr_english_desc"))
                    cs.Width = 200
                    cs.MappingName = "nbrosr_english_desc"
                    cs.HeaderText = "Outstanding"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    cs = New DataGridTextBoxColumn
                    cs.Width = 50
                    cs.MappingName = "nbruad_outstanding_stat"
                    cs.HeaderText = "Status"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    cs = New DataGridTextBoxColumn
                    cs.Width = 1200
                    cs.MappingName = "nbruad_outstanding_rmk"
                    cs.HeaderText = "Remark"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    ts.MappingName = "UWInfo"
                    grdUW.TableStyles.Add(ts)

                    grdUW.DataSource = ds.Tables("UWInfo")
                    grdUW.AllowDrop = False
                    grdUW.ReadOnly = True

                End With
                bm = Me.BindingContext(ds.Tables("UWInfo"))

                ''Dim bAppDt As Binding = New Binding("Text", ds.Tables("UWInfo"), "cswpuw_appl_date")
                ''Me.txtAppDate.DataBindings.Add(bAppDt)
                ''AddHandler bAppDt.Format, AddressOf FormatDate

                ''Dim bAppRDt As Binding = New Binding("Text", ds.Tables("UWInfo"), "cswpuw_aprc_date")
                ''Me.txtRecvDate.DataBindings.Add(bAppRDt)
                ''AddHandler bAppRDt.Format, AddressOf FormatDate

                ''Dim bRCDt As Binding = New Binding("Text", ds.Tables("UWInfo"), "cswpuw_rcd")
                ''Me.txtRCD.DataBindings.Add(bRCDt)
                ''AddHandler bRCDt.Format, AddressOf FormatDate

                ''Dim bInfDt As Binding = New Binding("Text", ds.Tables("UWInfo"), "cswpuw_infor_date")
                ''Me.txtInforce.DataBindings.Add(bInfDt)
                ''AddHandler bInfDt.Format, AddressOf FormatDate

                ''Dim bDelDt As Binding = New Binding("Text", ds.Tables("UWInfo"), "cswpuw_podl_date")
                ''Me.txtDelDate.DataBindings.Add(bDelDt)
                ''AddHandler bDelDt.Format, AddressOf FormatDate

                ''Dim bFLDt As Binding = New Binding("Text", ds.Tables("UWInfo"), "cswpuw_flook_dline")
                ''Me.txtFLDate.DataBindings.Add(bFLDt)
                ''AddHandler bFLDt.Format, AddressOf FormatDate

            End If
        End If  ' not CNB

        If ds.Tables("CswUWInfo").Rows.Count > 0 Then

            Dim bAppDt As Binding = New Binding("Text", ds.Tables("CswUWInfo"), "cswpuw_appl_date")
            Me.txtAppDate.DataBindings.Add(bAppDt)
            AddHandler bAppDt.Format, AddressOf FormatDate

            Dim bAppRDt As Binding = New Binding("Text", ds.Tables("CswUWInfo"), "cswpuw_aprc_date")
            Me.txtRecvDate.DataBindings.Add(bAppRDt)
            AddHandler bAppRDt.Format, AddressOf FormatDate

            Dim bRCDt As Binding = New Binding("Text", ds.Tables("CswUWInfo"), "cswpuw_rcd")
            Me.txtRCD.DataBindings.Add(bRCDt)
            AddHandler bRCDt.Format, AddressOf FormatDate

            Dim bInfDt As Binding = New Binding("Text", ds.Tables("CswUWInfo"), "cswpuw_infor_date")
            Me.txtInforce.DataBindings.Add(bInfDt)
            AddHandler bInfDt.Format, AddressOf FormatDate

            Dim bDelDt As Binding = New Binding("Text", ds.Tables("CswUWInfo"), "cswpuw_podl_date")
            Me.txtDelDate.DataBindings.Add(bDelDt)
            AddHandler bDelDt.Format, AddressOf FormatDate

            Dim bFLDt As Binding = New Binding("Text", ds.Tables("CswUWInfo"), "cswpuw_flook_dline")
            Me.txtFLDate.DataBindings.Add(bFLDt)
            AddHandler bFLDt.Format, AddressOf FormatDate

        End If

		#Region "Cheque outsourcing"
		
		Dim dtChequeInfo As New DataTable("ChequeInfo")
		If GetChequeInfo(strPolicy, dtChequeInfo, strErr) Then
                    
                       Dim bChequeDt As Binding = New Binding("Text", dtChequeInfo, "Date")
						Me.TextBoxChequeDate.DataBindings.Add(bChequeDt)
						AddHandler bChequeDt.Format, AddressOf FormatDate

						Dim bChequeDestDt As Binding = New Binding("Text", dtChequeInfo, "posvhe_chq_delivery_method")
						Me.TextBoxChequeDestination.DataBindings.Add(bChequeDestDt)
						'AddHandler bChequeDestDt.Format, AddressOf FormatDate 
                    
                    
                End If
		#End Region

        '---- ITDLSW 001 - Syntax fix
        If Not IsDBNull(datFLDate) Then txtFLD.Text = iif(datFLDate = Nothing, "", Format(datFLDate, gDateFormat))

        ' **** ES006 begin ****
        If g_Comp <> "HKL" And Not IsCNB Then       ' ES0009 
            If ds.Tables("UWSheet").Rows.Count > 0 Then
                txtUWS.Text = ds.Tables("UWSheet").Rows(0).Item("nbruwk_uw_comments")
            End If
        End If
        ' **** ES006 end ****

    End Sub

    Private Sub grdUW_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUW.CurrentCellChanged
        Call UpdatePT()
    End Sub

    Private Sub UpdatePT()
        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            ' ES009 begin
            If IsCNB Then
                If Not IsDBNull(drI.Item("description")) Then
                    Me.txtOutstand.Text = drI.Item("description")
                Else
                    Me.txtOutstand.Text = ""
                End If

                If Not IsDBNull(drI.Item("outstandingStatus")) Then
                    Me.txtStatus.Text = drI.Item("outstandingStatus")
                Else
                    Me.txtStatus.Text = ""
                End If

                If Not IsDBNull(drI.Item("outstandingRemark")) Then
                    Me.txtRemark.Text = drI.Item("outstandingRemark")
                Else
                    Me.txtRemark.Text = ""
                End If
            Else
                ' ES009 end
                Me.txtOutstand.Text = GetRelationValue(drI, "OutRel", "nbrosr_english_desc")

                If Not IsDBNull(drI.Item("nbruad_outstanding_stat")) Then
                    Me.txtStatus.Text = drI.Item("nbruad_outstanding_stat")
                Else
                    Me.txtStatus.Text = ""
                End If

                If Not IsDBNull(drI.Item("nbruad_outstanding_rmk")) Then
                    Me.txtRemark.Text = drI.Item("nbruad_outstanding_rmk")
                Else
                    Me.txtRemark.Text = ""
                End If
            End If
        End If 'ES009

    End Sub

    Private Sub grdUW_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdUW.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdUW.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdUW.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdUW.Select(hti.Row)
        End If
    End Sub

End Class
