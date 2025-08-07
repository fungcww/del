Imports System.data.sqlclient

Public Class frmAgentInfo
    Inherits System.Windows.Forms.Form

    Private ds As DataSet = New DataSet("AgentList")
    Private dr, dr1 As DataRow
    Private bm As BindingManagerBase
    Friend WithEvents txtBrokerPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtAgtGrade As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Private strAgent As String
    Private strComp As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents txtDateLeft As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtHKID As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents grdLicense As System.Windows.Forms.DataGrid
    Friend WithEvents txtDM As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtBus As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtDateJoin As System.Windows.Forms.TextBox
    Friend WithEvents txtAccNo As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPosition As System.Windows.Forms.TextBox
    Friend WithEvents txtDivision As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtCommPay As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDateLeft = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtHKID = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.grdLicense = New System.Windows.Forms.DataGrid
        Me.txtDM = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.txtBus = New System.Windows.Forms.TextBox
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtDateJoin = New System.Windows.Forms.TextBox
        Me.txtAccNo = New System.Windows.Forms.TextBox
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtPosition = New System.Windows.Forms.TextBox
        Me.txtDivision = New System.Windows.Forms.TextBox
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.txtUnit = New System.Windows.Forms.TextBox
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.txtCommPay = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBrokerPwd = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtAgtGrade = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        CType(Me.grdLicense, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDateLeft
        '
        Me.txtDateLeft.AcceptsReturn = True
        Me.txtDateLeft.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateLeft.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDateLeft.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDateLeft.Location = New System.Drawing.Point(288, 60)
        Me.txtDateLeft.MaxLength = 0
        Me.txtDateLeft.Name = "txtDateLeft"
        Me.txtDateLeft.ReadOnly = True
        Me.txtDateLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateLeft.Size = New System.Drawing.Size(81, 20)
        Me.txtDateLeft.TabIndex = 119
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(192, 64)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(88, 13)
        Me.Label22.TabIndex = 120
        Me.Label22.Text = "Termination Date"
        '
        'txtHKID
        '
        Me.txtHKID.BackColor = System.Drawing.SystemColors.Window
        Me.txtHKID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHKID.Location = New System.Drawing.Point(456, 60)
        Me.txtHKID.Name = "txtHKID"
        Me.txtHKID.ReadOnly = True
        Me.txtHKID.Size = New System.Drawing.Size(116, 20)
        Me.txtHKID.TabIndex = 118
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(380, 64)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(73, 13)
        Me.Label21.TabIndex = 117
        Me.Label21.Text = "HKID/BR No."
        '
        'grdLicense
        '
        Me.grdLicense.AlternatingBackColor = System.Drawing.Color.White
        Me.grdLicense.BackColor = System.Drawing.Color.White
        Me.grdLicense.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdLicense.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdLicense.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdLicense.CaptionVisible = False
        Me.grdLicense.DataMember = ""
        Me.grdLicense.FlatMode = True
        Me.grdLicense.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdLicense.ForeColor = System.Drawing.Color.Black
        Me.grdLicense.GridLineColor = System.Drawing.Color.Wheat
        Me.grdLicense.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdLicense.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdLicense.HeaderForeColor = System.Drawing.Color.Black
        Me.grdLicense.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdLicense.Location = New System.Drawing.Point(96, 184)
        Me.grdLicense.Name = "grdLicense"
        Me.grdLicense.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdLicense.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdLicense.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdLicense.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdLicense.Size = New System.Drawing.Size(584, 112)
        Me.grdLicense.TabIndex = 116
        '
        'txtDM
        '
        Me.txtDM.BackColor = System.Drawing.SystemColors.Window
        Me.txtDM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDM.Location = New System.Drawing.Point(528, 36)
        Me.txtDM.Name = "txtDM"
        Me.txtDM.ReadOnly = True
        Me.txtDM.Size = New System.Drawing.Size(180, 20)
        Me.txtDM.TabIndex = 115
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmail.Location = New System.Drawing.Point(96, 84)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.Size = New System.Drawing.Size(228, 20)
        Me.txtEmail.TabIndex = 114
        '
        'txtFax
        '
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFax.Location = New System.Drawing.Point(240, 132)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.ReadOnly = True
        Me.txtFax.Size = New System.Drawing.Size(81, 20)
        Me.txtFax.TabIndex = 113
        '
        'txtBus
        '
        Me.txtBus.BackColor = System.Drawing.SystemColors.Window
        Me.txtBus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBus.Location = New System.Drawing.Point(96, 132)
        Me.txtBus.Name = "txtBus"
        Me.txtBus.ReadOnly = True
        Me.txtBus.Size = New System.Drawing.Size(81, 20)
        Me.txtBus.TabIndex = 112
        '
        'txtMobile
        '
        Me.txtMobile.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMobile.Location = New System.Drawing.Point(240, 108)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReadOnly = True
        Me.txtMobile.Size = New System.Drawing.Size(81, 20)
        Me.txtMobile.TabIndex = 111
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(408, 40)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(111, 13)
        Me.Label20.TabIndex = 110
        Me.Label20.Text = "Direct Manager Name"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(56, 88)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(32, 13)
        Me.Label19.TabIndex = 109
        Me.Label19.Text = "Email"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(212, 136)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(24, 13)
        Me.Label18.TabIndex = 108
        Me.Label18.Text = "Fax"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(40, 136)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(49, 13)
        Me.Label17.TabIndex = 107
        Me.Label17.Text = "Business"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(196, 112)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(38, 13)
        Me.Label16.TabIndex = 106
        Me.Label16.Text = "Mobile"
        '
        'txtDateJoin
        '
        Me.txtDateJoin.AcceptsReturn = True
        Me.txtDateJoin.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateJoin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateJoin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDateJoin.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDateJoin.Location = New System.Drawing.Point(96, 60)
        Me.txtDateJoin.MaxLength = 0
        Me.txtDateJoin.Name = "txtDateJoin"
        Me.txtDateJoin.ReadOnly = True
        Me.txtDateJoin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateJoin.Size = New System.Drawing.Size(81, 20)
        Me.txtDateJoin.TabIndex = 93
        '
        'txtAccNo
        '
        Me.txtAccNo.AcceptsReturn = True
        Me.txtAccNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAccNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAccNo.Location = New System.Drawing.Point(308, 156)
        Me.txtAccNo.MaxLength = 0
        Me.txtAccNo.Name = "txtAccNo"
        Me.txtAccNo.ReadOnly = True
        Me.txtAccNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccNo.Size = New System.Drawing.Size(81, 20)
        Me.txtAccNo.TabIndex = 92
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStatus.Location = New System.Drawing.Point(96, 12)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(81, 20)
        Me.txtStatus.TabIndex = 90
        '
        'txtPosition
        '
        Me.txtPosition.AcceptsReturn = True
        Me.txtPosition.BackColor = System.Drawing.SystemColors.Window
        Me.txtPosition.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPosition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPosition.Location = New System.Drawing.Point(280, 12)
        Me.txtPosition.MaxLength = 0
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.ReadOnly = True
        Me.txtPosition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPosition.Size = New System.Drawing.Size(173, 20)
        Me.txtPosition.TabIndex = 89
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDivision.Location = New System.Drawing.Point(96, 36)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.ReadOnly = True
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(60, 20)
        Me.txtDivision.TabIndex = 85
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(228, 36)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(60, 20)
        Me.txtLocation.TabIndex = 84
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnit.Location = New System.Drawing.Point(332, 36)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.ReadOnly = True
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(60, 20)
        Me.txtUnit.TabIndex = 83
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone.Location = New System.Drawing.Point(96, 108)
        Me.txtPhone.MaxLength = 0
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.ReadOnly = True
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(81, 20)
        Me.txtPhone.TabIndex = 82
        '
        'txtCommPay
        '
        Me.txtCommPay.AcceptsReturn = True
        Me.txtCommPay.BackColor = System.Drawing.SystemColors.Window
        Me.txtCommPay.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCommPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCommPay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCommPay.Location = New System.Drawing.Point(96, 156)
        Me.txtCommPay.MaxLength = 0
        Me.txtCommPay.Name = "txtCommPay"
        Me.txtCommPay.ReadOnly = True
        Me.txtCommPay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCommPay.Size = New System.Drawing.Size(81, 20)
        Me.txtCommPay.TabIndex = 81
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(20, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 104
        Me.Label13.Text = "Agent Status"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(196, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(75, 13)
        Me.Label12.TabIndex = 103
        Me.Label12.Text = "Agent Position"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(48, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 101
        Me.Label10.Text = "Division"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(176, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 100
        Me.Label9.Text = "Location"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(304, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 99
        Me.Label8.Text = "Unit"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(68, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(25, 13)
        Me.Label7.TabIndex = 98
        Me.Label7.Text = "Tel."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(16, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 97
        Me.Label6.Text = "Contract Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 160)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 96
        Me.Label5.Text = "Comm. Payment"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(192, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(108, 13)
        Me.Label4.TabIndex = 95
        Me.Label4.Text = "Cheque/Autopay No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(48, 188)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 94
        Me.Label2.Text = "License"
        '
        'txtBrokerPwd
        '
        Me.txtBrokerPwd.BackColor = System.Drawing.SystemColors.Window
        Me.txtBrokerPwd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBrokerPwd.ForeColor = System.Drawing.Color.Blue
        Me.txtBrokerPwd.Location = New System.Drawing.Point(456, 85)
        Me.txtBrokerPwd.Name = "txtBrokerPwd"
        Me.txtBrokerPwd.ReadOnly = True
        Me.txtBrokerPwd.Size = New System.Drawing.Size(58, 20)
        Me.txtBrokerPwd.TabIndex = 122
        Me.txtBrokerPwd.Text = "P123456"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label23.ForeColor = System.Drawing.Color.Blue
        Me.Label23.Location = New System.Drawing.Point(357, 87)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(93, 13)
        Me.Label23.TabIndex = 121
        Me.Label23.Text = "Password (Broker)"
        '
        'txtAgtGrade
        '
        Me.txtAgtGrade.AcceptsReturn = True
        Me.txtAgtGrade.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgtGrade.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAgtGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAgtGrade.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgtGrade.Location = New System.Drawing.Point(528, 10)
        Me.txtAgtGrade.MaxLength = 0
        Me.txtAgtGrade.Name = "txtAgtGrade"
        Me.txtAgtGrade.ReadOnly = True
        Me.txtAgtGrade.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAgtGrade.Size = New System.Drawing.Size(113, 20)
        Me.txtAgtGrade.TabIndex = 123
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(461, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(67, 13)
        Me.Label27.TabIndex = 124
        Me.Label27.Text = "Agent Grade"
        '
        'frmAgentInfo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(720, 321)
        Me.Controls.Add(Me.txtAgtGrade)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.txtBrokerPwd)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.txtDateLeft)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtHKID)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.grdLicense)
        Me.Controls.Add(Me.txtDM)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.txtBus)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtDateJoin)
        Me.Controls.Add(Me.txtAccNo)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtPosition)
        Me.Controls.Add(Me.txtDivision)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.txtUnit)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtCommPay)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Name = "frmAgentInfo"
        Me.Text = "Agent Details"
        CType(Me.grdLicense, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property AgentCode() As String
        Set(ByVal Value As String)
            strAgent = Value
        End Set
    End Property

    Public WriteOnly Property Company() As String
        Set(ByVal Value As String)
            strComp = Value
        End Set
    End Property

    Private Sub frmAgentInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim sqlconnect As New SqlConnection
        'Dim strSQL As String
        'Dim sqlda As SqlDataAdapter

        'Try
        '    strSQL = "select camaid_agent_no as cswagi_agent_code, camaid_sort_key as cswagi_unit_code, Camrrt_loc_code as cswagi_loc_code, " & _
        '            " camaid_date_join as cswagi_contract_date, camaid_date_left as cswagi_date_left, camaid_grade as cswagi_grade, " & _
        '            " camgpr_desc as cswagi_desc, bm.camaib_disp_name as cswagi_mgr_name, camaia_res_phone as cswagi_res_phone, " & _
        '            " camaia_mob_phone as cswagi_mob_phone, camaia_bus_phone as cswagi_bus_phone, camaia_fax as cswagi_fax, " & _
        '            " camaia_email as cswagi_email, ba.camaib_idno as cswagi_idno " & _
        '            " from " & cSQL3 & "." & g_CAM_Database & ".dbo.cam_agent_info_dirmgr, " & cSQL3 & "." & g_CAM_Database & ".dbo.cam_grade_pkg_ref, " & _
        '            cSQL3 & "." & g_CAM_Database & ".dbo.cam_agent_info_package, " & cSQL3 & "." & g_CAM_Database & ".dbo.cam_Agent_info_address, " & _
        '            cSQL3 & "." & g_CAM_Database & ".dbo.cam_rdbu_rel_tab, " & cSQL3 & "." & g_CAM_Database & ".dbo.cam_agent_info_basic ba, " & cSQL3 & "." & g_CAM_Database & ".dbo.cam_agent_info_basic bm " & _
        '            " Where camaid_Agent_no = camaip_agent_no " & _
        '            " and char(camaip_per_pkg) = camgpr_per_pkg " & _
        '            " and camaid_grade = camgpr_grade_no " & _
        '            " and camaid_Agent_no = camaia_agent_no " & _
        '            " and camaid_sort_key = camrrt_agency_code " & _
        '            " and camrrt_section_no = '00000' " & _
        '            " and bm.camaib_agent_no = case when camaid_grade between 4 and 12 then camrrt_dir_agtno else camrrt_agent_no end " & _
        '            " and camaid_agent_no = ba.camaib_agent_no " & _
        '            " and camaid_agent_no in ('" & strAgent & "'); "
        '    strSQL &= "Select * from cswvw_agent_license Where camalt_agent_no in ('" & strAgent & "'); "
        '    strSQL &= "Select AgentCode, CommPayType, CommAcctNo, PhoneNumber from AgentCodes where AgentCode in ('" & strAgent & "'); "

        '    sqlconnect.ConnectionString = strCIWConn
        '    sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        '    sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        '    sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        '    sqlda.TableMappings.Add("cswvw_agent_info1", "cswvw_agent_license")
        '    sqlda.TableMappings.Add("cswvw_agent_info2", "AgentCodes")
        '    sqlda.Fill(ds, "cswvw_agent_info")

        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        'sqlconnect.Dispose()

        'strPolicy = ds.Tables("POLINF").Rows(0).Item("PolicyAccountID")
        Dim strErr As String
        Dim tempDsAgentInfoRelatedTable As DataSet = New DataSet()
        Dim strInAgent = "'" + strAgent + "'"
        
        If GetAgentInfoRelatedTables("", strInAgent, tempDsAgentInfoRelatedTable, strErr, strComp)
            If Not strErr Is Nothing
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Else
                For Each dt As DataTable In tempDsAgentInfoRelatedTable.Tables
                    ds.Tables.Add(dt.Copy)
                Next
            End If
        End If

        Dim ts1 As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "camalt_license_no"
        cs.HeaderText = "License No."
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "camlrt_license_name"
        cs.HeaderText = "License Name"
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "camalt_eff_date"
        cs.HeaderText = "Effective Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "camalt_exp_date"
        cs.HeaderText = "Expiry Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs)

        ts1.MappingName = "cswvw_agent_license"
        grdLicense.TableStyles.Add(ts1)

        grdLicense.DataSource = ds.Tables("cswvw_agent_license")
        grdLicense.AllowDrop = False

        grdLicense.ReadOnly = True

        ' ES0001 begin
        If g_Comp = "HKL" Then
            Me.txtBrokerPwd.Visible = False
            Me.Label23.Visible = False
        End If
        ' ES0001 end

        If ds.Tables("cswvw_agent_info").Rows.Count > 0 Then

            Dim drI As DataRow
            drI = ds.Tables("cswvw_agent_info").Rows(0)

            txtLocation.Text = drI.Item("cswagi_loc_code")
            txtUnit.Text = drI.Item("cswagi_unit_code")
            txtDivision.Text = Microsoft.VisualBasic.Strings.Left(txtUnit.Text, 2)
            txtMobile.Text = drI.Item("cswagi_mob_phone")
            txtBus.Text = drI.Item("cswagi_bus_phone")
            txtFax.Text = drI.Item("cswagi_fax")
            txtEmail.Text = drI.Item("cswagi_email")
            txtDM.Text = drI.Item("cswagi_mgr_name")
            txtPosition.Text = drI.Item("cswagi_desc")
            txtHKID.Text = drI.Item("cswagi_idno")
            txtDateJoin.Text = Format(drI.Item("cswagi_contract_date"), gDateFormat)

            'txtLastName.Text = GetRelationValue(drI, "NARel", "NameSuffix")
            'txtFirstName.Text = GetRelationValue(drI, "NARel", "FirstName")
            'txtChiName.Text = GetRelationValue(drI, "NARel", "ChiName")

            If ds.Tables("AgentCodes").Rows.Count > 0 Then
                txtPhone.Text = ds.Tables("AgentCodes").Rows(0).Item("PhoneNumber")
                txtCommPay.Text = ds.Tables("AgentCodes").Rows(0).Item("CommPayType")
                txtAccNo.Text = ds.Tables("AgentCodes").Rows(0).Item("CommAcctNo")
            End If

            Dim strLeft As String
            Dim datLeft As Date

            strLeft = drI.Item("cswagi_date_left")
            datLeft = IIf(IsDate(strLeft), strLeft, #1/1/1900#)
            If datLeft = #1/1/1900# OrElse datLeft > Today Then
                txtStatus.Text = "Active"
                txtDateLeft.Text = ""
            Else
                txtStatus.Text = "Inactive"
                txtDateLeft.Text = Format(datLeft, gDateFormat)
            End If

            If g_Comp = "ING" Then txtBrokerPwd.Text = GetBrokerVerificationKey(strAgent) ' ES0001

            ' Get elite/noble agent start
            txtAgtGrade.Text = GetEliteAgent(strAgent)
            If txtAgtGrade.Text <> "STANDARD" Then txtAgtGrade.BackColor = Color.Orange
            ' Get elite/noble agent end

        End If

    End Sub

    Private Sub grdLicense_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdLicense.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdLicense.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdLicense.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdLicense.Select(hti.Row)
        End If
    End Sub

End Class
