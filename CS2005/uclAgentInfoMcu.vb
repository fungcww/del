Imports System.Data.SqlClient
Imports System.Configuration

Public Class AgentInfoMcu
    Inherits System.Windows.Forms.UserControl

    Private ds As DataSet = New DataSet("AgentList")
    Private strPolicy As String
    Private dr, dr1 As DataRow
    Private bm As BindingManagerBase
    Private dtCSRF As DataTable
    Private strCurCSR, strCurRmk, strSA, strSAB As String
    Private blnRecFound, blnCanViewCSA As Boolean
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtBrokerPwd As System.Windows.Forms.TextBox
    Friend WithEvents txtHKLBank As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtHKLBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtHKLAgent As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtAgtGrade As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Private blnLoading As Boolean = True

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        grbCSR.Enabled = True 'CheckUPSAccess("Assign Servicing CSR")
        blnCanViewCSA = True 'CheckUPSAccess("View Servicing CSR")

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
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdAgent As System.Windows.Forms.DataGrid
    Friend WithEvents txtRole As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPosition As System.Windows.Forms.TextBox
    Friend WithEvents txtAccNo As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtChiName As System.Windows.Forms.TextBox
    Friend WithEvents txtDivision As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtCommPay As System.Windows.Forms.TextBox
    Friend WithEvents txtLicFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtLicTo As System.Windows.Forms.TextBox
    Friend WithEvents txtLicType As System.Windows.Forms.TextBox
    Friend WithEvents txtLicNo As System.Windows.Forms.TextBox
    Friend WithEvents txtDateJoin As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents txtBus As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtDM As System.Windows.Forms.TextBox
    Friend WithEvents grdLicense As System.Windows.Forms.DataGrid
    Friend WithEvents txtHKID As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtDateLeft As System.Windows.Forms.TextBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cboCSR As System.Windows.Forms.ComboBox
    Friend WithEvents grbCSR As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDateJoin = New System.Windows.Forms.TextBox
        Me.txtAccNo = New System.Windows.Forms.TextBox
        Me.txtRole = New System.Windows.Forms.TextBox
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtPosition = New System.Windows.Forms.TextBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.txtChiName = New System.Windows.Forms.TextBox
        Me.txtDivision = New System.Windows.Forms.TextBox
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.txtUnit = New System.Windows.Forms.TextBox
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.txtCommPay = New System.Windows.Forms.TextBox
        Me.txtLicFrom = New System.Windows.Forms.TextBox
        Me.txtLicTo = New System.Windows.Forms.TextBox
        Me.txtLicType = New System.Windows.Forms.TextBox
        Me.txtLicNo = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdAgent = New System.Windows.Forms.DataGrid
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.txtBus = New System.Windows.Forms.TextBox
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.txtDM = New System.Windows.Forms.TextBox
        Me.grdLicense = New System.Windows.Forms.DataGrid
        Me.txtHKID = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtDateLeft = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.grbCSR = New System.Windows.Forms.GroupBox
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cboCSR = New System.Windows.Forms.ComboBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtBrokerPwd = New System.Windows.Forms.TextBox
        Me.txtHKLBank = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtHKLBranch = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtHKLAgent = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtAgtGrade = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        CType(Me.grdAgent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLicense, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbCSR.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDateJoin
        '
        Me.txtDateJoin.AcceptsReturn = True
        Me.txtDateJoin.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateJoin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateJoin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDateJoin.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDateJoin.Location = New System.Drawing.Point(96, 217)
        Me.txtDateJoin.MaxLength = 0
        Me.txtDateJoin.Name = "txtDateJoin"
        Me.txtDateJoin.ReadOnly = True
        Me.txtDateJoin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateJoin.Size = New System.Drawing.Size(81, 20)
        Me.txtDateJoin.TabIndex = 49
        '
        'txtAccNo
        '
        Me.txtAccNo.AcceptsReturn = True
        Me.txtAccNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAccNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAccNo.Location = New System.Drawing.Point(308, 313)
        Me.txtAccNo.MaxLength = 0
        Me.txtAccNo.Name = "txtAccNo"
        Me.txtAccNo.ReadOnly = True
        Me.txtAccNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccNo.Size = New System.Drawing.Size(81, 20)
        Me.txtAccNo.TabIndex = 48
        '
        'txtRole
        '
        Me.txtRole.AcceptsReturn = True
        Me.txtRole.BackColor = System.Drawing.SystemColors.Window
        Me.txtRole.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRole.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRole.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRole.Location = New System.Drawing.Point(96, 116)
        Me.txtRole.MaxLength = 0
        Me.txtRole.Name = "txtRole"
        Me.txtRole.ReadOnly = True
        Me.txtRole.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRole.Size = New System.Drawing.Size(128, 20)
        Me.txtRole.TabIndex = 47
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStatus.Location = New System.Drawing.Point(308, 116)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(81, 20)
        Me.txtStatus.TabIndex = 46
        '
        'txtPosition
        '
        Me.txtPosition.AcceptsReturn = True
        Me.txtPosition.BackColor = System.Drawing.SystemColors.Window
        Me.txtPosition.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPosition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPosition.Location = New System.Drawing.Point(492, 116)
        Me.txtPosition.MaxLength = 0
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.ReadOnly = True
        Me.txtPosition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPosition.Size = New System.Drawing.Size(173, 20)
        Me.txtPosition.TabIndex = 45
        '
        'txtLastName
        '
        Me.txtLastName.AcceptsReturn = True
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLastName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastName.Location = New System.Drawing.Point(96, 140)
        Me.txtLastName.MaxLength = 0
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastName.Size = New System.Drawing.Size(137, 20)
        Me.txtLastName.TabIndex = 44
        '
        'txtFirstName
        '
        Me.txtFirstName.AcceptsReturn = True
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFirstName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFirstName.Location = New System.Drawing.Point(236, 140)
        Me.txtFirstName.MaxLength = 0
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFirstName.Size = New System.Drawing.Size(217, 20)
        Me.txtFirstName.TabIndex = 43
        '
        'txtChiName
        '
        Me.txtChiName.AcceptsReturn = True
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChiName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtChiName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChiName.Location = New System.Drawing.Point(456, 140)
        Me.txtChiName.MaxLength = 0
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChiName.Size = New System.Drawing.Size(165, 20)
        Me.txtChiName.TabIndex = 42
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDivision.Location = New System.Drawing.Point(96, 164)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.ReadOnly = True
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(60, 20)
        Me.txtDivision.TabIndex = 41
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(228, 164)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(60, 20)
        Me.txtLocation.TabIndex = 40
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnit.Location = New System.Drawing.Point(332, 164)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.ReadOnly = True
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(60, 20)
        Me.txtUnit.TabIndex = 39
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone.Location = New System.Drawing.Point(96, 265)
        Me.txtPhone.MaxLength = 0
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.ReadOnly = True
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(81, 20)
        Me.txtPhone.TabIndex = 38
        '
        'txtCommPay
        '
        Me.txtCommPay.AcceptsReturn = True
        Me.txtCommPay.BackColor = System.Drawing.SystemColors.Window
        Me.txtCommPay.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCommPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCommPay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCommPay.Location = New System.Drawing.Point(96, 313)
        Me.txtCommPay.MaxLength = 0
        Me.txtCommPay.Name = "txtCommPay"
        Me.txtCommPay.ReadOnly = True
        Me.txtCommPay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCommPay.Size = New System.Drawing.Size(81, 20)
        Me.txtCommPay.TabIndex = 37
        '
        'txtLicFrom
        '
        Me.txtLicFrom.AcceptsReturn = True
        Me.txtLicFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtLicFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLicFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLicFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLicFrom.Location = New System.Drawing.Point(156, 389)
        Me.txtLicFrom.MaxLength = 0
        Me.txtLicFrom.Name = "txtLicFrom"
        Me.txtLicFrom.ReadOnly = True
        Me.txtLicFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLicFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtLicFrom.TabIndex = 36
        Me.txtLicFrom.Visible = False
        '
        'txtLicTo
        '
        Me.txtLicTo.AcceptsReturn = True
        Me.txtLicTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLicTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLicTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLicTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLicTo.Location = New System.Drawing.Point(256, 389)
        Me.txtLicTo.MaxLength = 0
        Me.txtLicTo.Name = "txtLicTo"
        Me.txtLicTo.ReadOnly = True
        Me.txtLicTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLicTo.Size = New System.Drawing.Size(81, 20)
        Me.txtLicTo.TabIndex = 35
        Me.txtLicTo.Visible = False
        '
        'txtLicType
        '
        Me.txtLicType.AcceptsReturn = True
        Me.txtLicType.BackColor = System.Drawing.SystemColors.Window
        Me.txtLicType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLicType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLicType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLicType.Location = New System.Drawing.Point(260, 361)
        Me.txtLicType.MaxLength = 0
        Me.txtLicType.Name = "txtLicType"
        Me.txtLicType.ReadOnly = True
        Me.txtLicType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLicType.Size = New System.Drawing.Size(185, 20)
        Me.txtLicType.TabIndex = 34
        Me.txtLicType.Visible = False
        '
        'txtLicNo
        '
        Me.txtLicNo.AcceptsReturn = True
        Me.txtLicNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLicNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLicNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLicNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLicNo.Location = New System.Drawing.Point(520, 361)
        Me.txtLicNo.MaxLength = 0
        Me.txtLicNo.Name = "txtLicNo"
        Me.txtLicNo.ReadOnly = True
        Me.txtLicNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLicNo.Size = New System.Drawing.Size(185, 20)
        Me.txtLicNo.TabIndex = 33
        Me.txtLicNo.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(244, 393)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(10, 13)
        Me.Label15.TabIndex = 64
        Me.Label15.Text = "-"
        Me.Label15.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(64, 120)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(29, 13)
        Me.Label14.TabIndex = 63
        Me.Label14.Text = "Role"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(232, 120)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 62
        Me.Label13.Text = "Agent Status"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(408, 120)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(75, 13)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "Agent Position"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(56, 144)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 60
        Me.Label11.Text = "Agent"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(48, 168)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 59
        Me.Label10.Text = "Division"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(176, 168)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "Location"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(304, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "Unit"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(68, 269)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(25, 13)
        Me.Label7.TabIndex = 56
        Me.Label7.Text = "Tel."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(16, 221)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Contract Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 317)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "Comm. Payment"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(192, 317)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(108, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Cheque/Autopay No."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(72, 393)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "License Period"
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(48, 341)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "License"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(452, 365)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "License No."
        Me.Label1.Visible = False
        '
        'grdAgent
        '
        Me.grdAgent.AlternatingBackColor = System.Drawing.Color.White
        Me.grdAgent.BackColor = System.Drawing.Color.White
        Me.grdAgent.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAgent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAgent.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAgent.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAgent.CaptionVisible = False
        Me.grdAgent.DataMember = ""
        Me.grdAgent.FlatMode = True
        Me.grdAgent.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdAgent.ForeColor = System.Drawing.Color.Black
        Me.grdAgent.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAgent.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdAgent.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdAgent.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAgent.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAgent.Location = New System.Drawing.Point(4, 4)
        Me.grdAgent.Name = "grdAgent"
        Me.grdAgent.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAgent.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAgent.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAgent.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAgent.Size = New System.Drawing.Size(684, 104)
        Me.grdAgent.TabIndex = 65
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(196, 269)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(38, 13)
        Me.Label16.TabIndex = 66
        Me.Label16.Text = "Mobile"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(40, 293)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(49, 13)
        Me.Label17.TabIndex = 67
        Me.Label17.Text = "Business"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(212, 293)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(24, 13)
        Me.Label18.TabIndex = 68
        Me.Label18.Text = "Fax"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(56, 245)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(32, 13)
        Me.Label19.TabIndex = 69
        Me.Label19.Text = "Email"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(408, 168)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(111, 13)
        Me.Label20.TabIndex = 70
        Me.Label20.Text = "Direct Manager Name"
        '
        'txtMobile
        '
        Me.txtMobile.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMobile.Location = New System.Drawing.Point(240, 265)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReadOnly = True
        Me.txtMobile.Size = New System.Drawing.Size(81, 20)
        Me.txtMobile.TabIndex = 71
        '
        'txtBus
        '
        Me.txtBus.BackColor = System.Drawing.SystemColors.Window
        Me.txtBus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBus.Location = New System.Drawing.Point(96, 289)
        Me.txtBus.Name = "txtBus"
        Me.txtBus.ReadOnly = True
        Me.txtBus.Size = New System.Drawing.Size(81, 20)
        Me.txtBus.TabIndex = 72
        '
        'txtFax
        '
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFax.Location = New System.Drawing.Point(240, 289)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.ReadOnly = True
        Me.txtFax.Size = New System.Drawing.Size(81, 20)
        Me.txtFax.TabIndex = 73
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmail.Location = New System.Drawing.Point(96, 241)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.Size = New System.Drawing.Size(228, 20)
        Me.txtEmail.TabIndex = 74
        '
        'txtDM
        '
        Me.txtDM.BackColor = System.Drawing.SystemColors.Window
        Me.txtDM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDM.Location = New System.Drawing.Point(528, 164)
        Me.txtDM.Name = "txtDM"
        Me.txtDM.ReadOnly = True
        Me.txtDM.Size = New System.Drawing.Size(189, 20)
        Me.txtDM.TabIndex = 75
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
        Me.grdLicense.Location = New System.Drawing.Point(96, 337)
        Me.grdLicense.Name = "grdLicense"
        Me.grdLicense.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdLicense.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdLicense.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdLicense.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdLicense.Size = New System.Drawing.Size(584, 68)
        Me.grdLicense.TabIndex = 76
        '
        'txtHKID
        '
        Me.txtHKID.BackColor = System.Drawing.SystemColors.Window
        Me.txtHKID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHKID.Location = New System.Drawing.Point(439, 217)
        Me.txtHKID.Name = "txtHKID"
        Me.txtHKID.ReadOnly = True
        Me.txtHKID.Size = New System.Drawing.Size(116, 20)
        Me.txtHKID.TabIndex = 78
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(363, 221)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(73, 13)
        Me.Label21.TabIndex = 77
        Me.Label21.Text = "HKID/BR No."
        '
        'txtDateLeft
        '
        Me.txtDateLeft.AcceptsReturn = True
        Me.txtDateLeft.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateLeft.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDateLeft.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDateLeft.Location = New System.Drawing.Point(277, 217)
        Me.txtDateLeft.MaxLength = 0
        Me.txtDateLeft.Name = "txtDateLeft"
        Me.txtDateLeft.ReadOnly = True
        Me.txtDateLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateLeft.Size = New System.Drawing.Size(81, 20)
        Me.txtDateLeft.TabIndex = 79
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(183, 221)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(88, 13)
        Me.Label22.TabIndex = 80
        Me.Label22.Text = "Termination Date"
        '
        'grbCSR
        '
        Me.grbCSR.Controls.Add(Me.txtRemark)
        Me.grbCSR.Controls.Add(Me.cmdCancel)
        Me.grbCSR.Controls.Add(Me.cmdSave)
        Me.grbCSR.Controls.Add(Me.cboCSR)
        Me.grbCSR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbCSR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grbCSR.Location = New System.Drawing.Point(404, 249)
        Me.grbCSR.Name = "grbCSR"
        Me.grbCSR.Size = New System.Drawing.Size(300, 80)
        Me.grbCSR.TabIndex = 85
        Me.grbCSR.TabStop = False
        Me.grbCSR.Text = "Servicing CSR"
        '
        'txtRemark
        '
        Me.txtRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.Location = New System.Drawing.Point(172, 20)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(120, 20)
        Me.txtRemark.TabIndex = 89
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdCancel.Location = New System.Drawing.Point(92, 48)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 88
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(12, 48)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 87
        Me.cmdSave.Text = "Save"
        '
        'cboCSR
        '
        Me.cboCSR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCSR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cboCSR.Location = New System.Drawing.Point(8, 20)
        Me.cboCSR.Name = "cboCSR"
        Me.cboCSR.Size = New System.Drawing.Size(160, 21)
        Me.cboCSR.TabIndex = 85
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label23.ForeColor = System.Drawing.Color.Blue
        Me.Label23.Location = New System.Drawing.Point(560, 221)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(93, 13)
        Me.Label23.TabIndex = 86
        Me.Label23.Text = "Password (Broker)"
        '
        'txtBrokerPwd
        '
        Me.txtBrokerPwd.BackColor = System.Drawing.SystemColors.Window
        Me.txtBrokerPwd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtBrokerPwd.ForeColor = System.Drawing.Color.Blue
        Me.txtBrokerPwd.Location = New System.Drawing.Point(659, 216)
        Me.txtBrokerPwd.Name = "txtBrokerPwd"
        Me.txtBrokerPwd.ReadOnly = True
        Me.txtBrokerPwd.Size = New System.Drawing.Size(58, 20)
        Me.txtBrokerPwd.TabIndex = 87
        Me.txtBrokerPwd.Text = "P123456"
        '
        'txtHKLBank
        '
        Me.txtHKLBank.AcceptsReturn = True
        Me.txtHKLBank.BackColor = System.Drawing.SystemColors.Window
        Me.txtHKLBank.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHKLBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtHKLBank.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHKLBank.Location = New System.Drawing.Point(330, 190)
        Me.txtHKLBank.MaxLength = 0
        Me.txtHKLBank.Name = "txtHKLBank"
        Me.txtHKLBank.ReadOnly = True
        Me.txtHKLBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHKLBank.Size = New System.Drawing.Size(60, 20)
        Me.txtHKLBank.TabIndex = 88
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(261, 193)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(60, 13)
        Me.Label24.TabIndex = 89
        Me.Label24.Text = "Bank Code"
        '
        'txtHKLBranch
        '
        Me.txtHKLBranch.AcceptsReturn = True
        Me.txtHKLBranch.BackColor = System.Drawing.SystemColors.Window
        Me.txtHKLBranch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHKLBranch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtHKLBranch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHKLBranch.Location = New System.Drawing.Point(469, 190)
        Me.txtHKLBranch.MaxLength = 0
        Me.txtHKLBranch.Name = "txtHKLBranch"
        Me.txtHKLBranch.ReadOnly = True
        Me.txtHKLBranch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHKLBranch.Size = New System.Drawing.Size(60, 20)
        Me.txtHKLBranch.TabIndex = 90
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(396, 194)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(69, 13)
        Me.Label25.TabIndex = 91
        Me.Label25.Text = "Branch Code"
        '
        'txtHKLAgent
        '
        Me.txtHKLAgent.AcceptsReturn = True
        Me.txtHKLAgent.BackColor = System.Drawing.SystemColors.Window
        Me.txtHKLAgent.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHKLAgent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtHKLAgent.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHKLAgent.Location = New System.Drawing.Point(604, 190)
        Me.txtHKLAgent.MaxLength = 0
        Me.txtHKLAgent.Name = "txtHKLAgent"
        Me.txtHKLAgent.ReadOnly = True
        Me.txtHKLAgent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHKLAgent.Size = New System.Drawing.Size(113, 20)
        Me.txtHKLAgent.TabIndex = 92
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(537, 194)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(63, 13)
        Me.Label26.TabIndex = 93
        Me.Label26.Text = "Agent Code"
        '
        'txtAgtGrade
        '
        Me.txtAgtGrade.AcceptsReturn = True
        Me.txtAgtGrade.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgtGrade.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAgtGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAgtGrade.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgtGrade.Location = New System.Drawing.Point(96, 191)
        Me.txtAgtGrade.MaxLength = 0
        Me.txtAgtGrade.Name = "txtAgtGrade"
        Me.txtAgtGrade.ReadOnly = True
        Me.txtAgtGrade.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAgtGrade.Size = New System.Drawing.Size(113, 20)
        Me.txtAgtGrade.TabIndex = 94
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(29, 195)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(67, 13)
        Me.Label27.TabIndex = 95
        Me.Label27.Text = "Agent Grade"
        '
        'AgentInfo
        '
        Me.Controls.Add(Me.txtAgtGrade)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.txtHKLAgent)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.txtHKLBranch)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtHKLBank)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtBrokerPwd)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.grbCSR)
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
        Me.Controls.Add(Me.grdAgent)
        Me.Controls.Add(Me.txtDateJoin)
        Me.Controls.Add(Me.txtAccNo)
        Me.Controls.Add(Me.txtRole)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtPosition)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.txtChiName)
        Me.Controls.Add(Me.txtDivision)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.txtUnit)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtCommPay)
        Me.Controls.Add(Me.txtLicFrom)
        Me.Controls.Add(Me.txtLicTo)
        Me.Controls.Add(Me.txtLicType)
        Me.Controls.Add(Me.txtLicNo)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AgentInfo"
        Me.Size = New System.Drawing.Size(720, 408)
        CType(Me.grdAgent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLicense, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbCSR.ResumeLayout(False)
        Me.grbCSR.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property srcDTPolSum(ByVal dtNA As DataTable)
        Set(ByVal Value)
            If Not Value Is Nothing Then
                ds.Tables.Add(dtNA)
                ds.Tables.Add(Value)
                Call buildUI()
            End If
        End Set
    End Property

    Public Sub buildUI()

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable
        Dim strAgtList As String
        Dim lngErrNo As Integer
        Dim strErrMsg As String
        Dim strErr As String


        wndMain.Cursor = Cursors.WaitCursor

        strCurCSR = ""
        strCurRmk = ""
        strSA = ""
        strSAB = ""
        blnRecFound = False

        'strSQL = "select space(10) as csrid, space(25) as name union all Select distinct csrid, name From CSR where active = 'Y' order by name"
        'Lubin 2022-11-11 Created/Move from HK Report logics to Macau.
        'Change query by sql to business id.
        LoadComboBox(dtCSRF, cboCSR, "csrid", "name", "FRM_CRS_COMBOBOX")

        'ds.Tables("POLINF").DefaultView.RowFilter = "PolicyRelateCode IN ('SA','WA','PA')"
        'With ds.Tables("POLINF")
        '    If ds.Tables("POLINF").Rows.Count > 0 Then
        '        strAgtList = "'" & .Rows(0).Item("AgentCode") & "'"
        '    End If
        '    For Each dr In .Rows
        '        strAgtList &= ",'" & dr.Item("AgentCode") & "'"
        '    Next
        'End With

        Dim dtAgt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        dtAgt.TableName = "AgentList"
        dc = New DataColumn
        dc.ColumnName = "PolicyRelateCode"
        dc.DataType = Type.GetType("System.String")
        dtAgt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "AgentCode"
        dc.DataType = Type.GetType("System.String")
        dtAgt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "ClientID"
        dc.DataType = Type.GetType("System.String")
        dtAgt.Columns.Add(dc)

        With ds.Tables("POLINF").Rows(0)
            If Not IsDBNull(.Item("POAGCY")) AndAlso .Item("POAGCY") <> "" Then
                dr = dtAgt.NewRow
                dr.Item("PolicyRelateCode") = "SA"
                dr.Item("AgentCode") = Microsoft.VisualBasic.Strings.Right(.Item("POAGCY"), 5)
                dr.Item("ClientID") = "00000" & Microsoft.VisualBasic.Strings.Right(.Item("POAGCY"), 5)
                dtAgt.Rows.Add(dr)

                strSA = dr.Item("AgentCode")
            End If

            If Not IsDBNull(.Item("POPAGT")) AndAlso .Item("POPAGT") <> "" Then
                dr = dtAgt.NewRow
                dr.Item("PolicyRelateCode") = "PA"
                dr.Item("AgentCode") = Microsoft.VisualBasic.Strings.Right(.Item("POPAGT"), 5)
                dr.Item("ClientID") = "00000" & Microsoft.VisualBasic.Strings.Right(.Item("POPAGT"), 5)
                dtAgt.Rows.Add(dr)
            End If

            If Not IsDBNull(.Item("POWAGT")) AndAlso .Item("POWAGT") <> "" Then
                dr = dtAgt.NewRow
                dr.Item("PolicyRelateCode") = "WA"
                dr.Item("AgentCode") = Microsoft.VisualBasic.Strings.Right(.Item("POWAGT"), 5)
                dr.Item("ClientID") = "00000" & Microsoft.VisualBasic.Strings.Right(.Item("POWAGT"), 5)
                dtAgt.Rows.Add(dr)
            End If

            ds.Tables.Add(dtAgt)

            strAgtList = "'" & Microsoft.VisualBasic.Strings.Right(.Item("POAGCY"), 5) & "'"
            strAgtList &= ",'" & Microsoft.VisualBasic.Strings.Right(.Item("POPAGT"), 5) & "'"
            strAgtList &= ",'" & Microsoft.VisualBasic.Strings.Right(.Item("POWAGT"), 5) & "'"

            Dim strSecAg As String
            For i As Integer = 1 To 3
                strSecAg = Strings.Right(.Item("PAGCO" & i), 5)
                If strSecAg <> "" AndAlso InStr(strAgtList, strSecAg) = 0 Then
                    strAgtList &= ",'" & strSecAg & "'"
                    dr = dtAgt.NewRow
                    dr.Item("PolicyRelateCode") = "DA"
                    dr.Item("AgentCode") = strSecAg
                    dr.Item("ClientID") = "00000" & strSecAg
                    dtAgt.Rows.Add(dr)
                End If
            Next

        End With

        ' Start HKL001 - Change ING Agt to HKL
        If g_Comp = "HKL" Then
            UpdHKLAgtDT(dtAgt, "AgentCode", "HKLAgentCode")
        End If
        ' End HKL001


        'Dim strCSU() As String
        'strCSU = CStr(ConfigurationSettings.AppSettings.Item("CSUNIT")).Split(",")

        ' Visible for unit 00802 only
        strSAB = ds.Tables("POLINF").Rows(0).Item("Unit")
        'MsgBox(strSAB)
        'MsgBox(ds.Tables("POLINF").Rows(0).Item("Unit"))
        ' And gsUser Like "CSR*"



        If Not IsDBNull(strSAB) AndAlso strSAB Like "008*" AndAlso blnCanViewCSA Then
            'If blnCanViewCSA Then
            grbCSR.Visible = True
        Else
            grbCSR.Visible = False
        End If

        'Try
        'strSQL = "Select * from AgentStatusCodes s, AgentCodes a " & _
        '        "LEFT JOIN cswvw_agent_info ON AgentCode = cswagi_agent_code " & _
        '        "Where AgentCode in (" & strAgtList & ") and a.AgentStatusCode = s.AgentStatusCode; "
        'strSQL &= "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
        'strSQL &= "Select * from cswvw_agent_license Where camalt_agent_no in (" & strAgtList & ")"
        'strSQL = "Select * From cswvw_cam_agent_info Where cswagi_agent_code in (" & strAgtList & "); "


        'Dim crsWS As New CRSWS.CRSWS


        'crsWS.getAgentAccountList(g_Comp, cSQL3, strAgtList, CAM_HKL_AGENT_MAPPING, g_CAM_Database, lngErrNo, strErrMsg, ds)


        'Using wsCRS As New CRSWS.CRSWS


        'Using wsCRS As New CRSWS.CRSWS

        '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
        '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
        '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
        '    End If
        '    'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
        '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
        '    wsCRS.Timeout = 10000000

        '    ds = wsCRS.getMcuAgentInfoList(getCompanyCode(getCompanyCode(g_McuComp)), getEnvCode(), g_Comp, cSQL3_Mcu, strAgtList, CAM_HKL_AGENT_MAPPING, g_McuCAM_Database, ds, strErrMsg)

        'End Using
        strPolicy = ds.Tables("POLINF").Rows(0).Item("PolicyAccountID")
        Dim tempDsAgentInfoRelatedTable As DataSet = New DataSet()
        If GetAgentInfoRelatedTables(strPolicy, strAgtList, tempDsAgentInfoRelatedTable, strErr, g_McuComp)
            If Not strErr Is Nothing
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Else
                For Each dt As DataTable In tempDsAgentInfoRelatedTable.Tables
                    ds.Tables.Add(dt.Copy)
                Next
            End If
        End If
        


        '    If g_Comp = "HKL" Then
        '        strSQL = "select camaid_agent_no as cswagi_agent_code, camaid_sort_key as cswagi_unit_code, Camrrt_loc_code as cswagi_loc_code, " & _
        '                " camaid_date_join as cswagi_contract_date, camaid_date_left as cswagi_date_left, camaid_grade as cswagi_grade, " & _
        '                " camgpr_desc as cswagi_desc, bm.camaib_disp_name as cswagi_mgr_name, camaia_res_phone as cswagi_res_phone, " & _
        '                " camaia_mob_phone as cswagi_mob_phone, camaia_bus_phone as cswagi_bus_phone, camaia_fax as cswagi_fax, " & _
        '                " camaia_email as cswagi_email, ba.camaib_idno as cswagi_idno " & _
        '                ", camham_HKL_Bank+camham_HKL_AgentNo as camham_HKL_AgentNo, camham_HKL_Branch, camham_HKL_Bank " & _
        '                " from " & cSQL3 & ".hklcam.dbo.cam_agent_info_dirmgr, " & cSQL3 & ".hklcam.dbo.cam_grade_pkg_ref, " & _
        '                cSQL3 & ".hklcam.dbo.cam_agent_info_package, " & cSQL3 & ".hklcam.dbo.cam_Agent_info_address, " & _
        '                cSQL3 & ".hklcam.dbo.cam_rdbu_rel_tab, " & cSQL3 & ".hklcam.dbo.cam_agent_info_basic ba, " & cSQL3 & ".hklcam.dbo.cam_agent_info_basic bm " & _
        '                ", " & CAM_HKL_AGENT_MAPPING & _
        '                " Where camaid_Agent_no = camaip_agent_no " & _
        '                " and char(camaip_per_pkg) = camgpr_per_pkg " & _
        '                " and camaid_grade = camgpr_grade_no " & _
        '                " and camaid_Agent_no = camaia_agent_no " & _
        '                " and camaid_sort_key = camrrt_agency_code " & _
        '                " and camrrt_section_no = '00000' " & _
        '                " and bm.camaib_agent_no = case when camaid_grade between 4 and 12 then camrrt_dir_agtno else camrrt_agent_no end " & _
        '                " and camaid_agent_no = ba.camaib_agent_no " & _
        '                " and camaid_Agent_no = camham_ING_AgentNo " & _
        '                " and camaid_agent_no in (" & strAgtList & "); "
        '    Else
        '        strSQL = "select camaid_agent_no as cswagi_agent_code, camaid_sort_key as cswagi_unit_code, Camrrt_loc_code as cswagi_loc_code, " & _
        '                " camaid_date_join as cswagi_contract_date, camaid_date_left as cswagi_date_left, camaid_grade as cswagi_grade, " & _
        '                " camgpr_desc as cswagi_desc, bm.camaib_disp_name as cswagi_mgr_name, camaia_res_phone as cswagi_res_phone, " & _
        '                " camaia_mob_phone as cswagi_mob_phone, camaia_bus_phone as cswagi_bus_phone, camaia_fax as cswagi_fax, " & _
        '                " camaia_email as cswagi_email, ba.camaib_idno as cswagi_idno " & _
        '                " from " & cSQL3 & "." & g_McuCAM_Database & ".dbo.cam_agent_info_dirmgr, " & cSQL3 & "." & g_McuCAM_Database & ".dbo.cam_grade_pkg_ref, " & _
        '                cSQL3 & "." & g_McuCAM_Database & ".dbo.cam_agent_info_package, " & cSQL3 & "." & g_McuCAM_Database & ".dbo.cam_Agent_info_address, " & _
        '                cSQL3 & "." & g_McuCAM_Database & ".dbo.cam_rdbu_rel_tab, " & cSQL3 & "." & g_McuCAM_Database & ".dbo.cam_agent_info_basic ba, " & cSQL3 & "." & g_McuCAM_Database & ".dbo.cam_agent_info_basic bm " & _
        '                " Where camaid_Agent_no = camaip_agent_no " & _
        '                " and char(camaip_per_pkg) = camgpr_per_pkg " & _
        '                " and camaid_grade = camgpr_grade_no " & _
        '                " and camaid_Agent_no = camaia_agent_no " & _
        '                " and camaid_sort_key = camrrt_agency_code " & _
        '                " and camrrt_section_no = '00000' " & _
        '                " and bm.camaib_agent_no = case when camaid_grade between 4 and 12 then camrrt_dir_agtno else camrrt_agent_no end " & _
        '                " and camaid_agent_no = ba.camaib_agent_no " & _
        '                " and camaid_agent_no in (" & strAgtList & "); "
        '    End If

        '    strSQL &= "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
        '    strSQL &= "Select * from cswvw_agent_license Where camalt_agent_no in (" & strAgtList & "); "

        '    ' Get commission info.
        '    strSQL &= "Select AgentCode, CommPayType, CommAcctNo, PhoneNumber from AgentCodes where AgentCode in (" & strAgtList & "); "

        '    ' Load servicing CSR
        '    strPolicy = ds.Tables("POLINF").Rows(0).Item("PolicyAccountID")
        '    strSQL &= "Select * from csw_cs_policy_list where cswcpl_policy_no = '" & strPolicy & "'"

        '    sqlconnect.ConnectionString = strCIWMcuConn
        '    sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        '    sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        '    sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        '    'sqlda.TableMappings.Add("AgentStatusCodes1", "PolicyAccountRelationCodes")
        '    'sqlda.TableMappings.Add("AgentStatusCodes2", "cswvw_agent_license")
        '    sqlda.TableMappings.Add("cswvw_agent_info1", "PolicyAccountRelationCodes")
        '    sqlda.TableMappings.Add("cswvw_agent_info2", "cswvw_agent_license")
        '    sqlda.TableMappings.Add("cswvw_agent_info3", "AgentCodes")
        '    sqlda.TableMappings.Add("cswvw_agent_info4", "csw_cs_policy_list")
        '    sqlda.Fill(ds, "cswvw_agent_info")

        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'End Try

        'sqlconnect.Dispose()

        Dim drSecAg As DataRow = ds.Tables("PolicyAccountRelationCodes").NewRow
        drSecAg.Item(0) = "DA"
        drSecAg.Item(1) = "Secondary Agent"
        drSecAg.Item(2) = 90
        ds.Tables("PolicyAccountRelationCodes").Rows.Add(drSecAg)

        'Dim relPolicyAcRel As New Data.DataRelation("PolicyAcRel", ds.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationCode"), _
        '    ds.Tables("POLINF").Columns("PolicyRelateCode"), True)

        'Dim relAgent As New Data.DataRelation("AgentRel", ds.Tables("AgentStatusCodes").Columns("AgentCode"), _
        '    ds.Tables("POLINF").Columns("AgentCode"), True)

        Dim relPolicyAcRel As New Data.DataRelation("PolicyAcRel", ds.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationCode"), _
            ds.Tables("AgentList").Columns("PolicyRelateCode"), True)

        Dim relNA As New Data.DataRelation("NARel", ds.Tables("ORDUNA").Columns("ClientID"), _
            ds.Tables("AgentList").Columns("ClientID"), True)

        Dim relAgtInfo As New Data.DataRelation("AgtInfoRel", ds.Tables("cswvw_agent_info").Columns("cswagi_agent_code"), _
            ds.Tables("AgentList").Columns("AgentCode"), True)

        Try
            ds.Relations.Add(relPolicyAcRel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        'Try
        '    ds.Relations.Add(relAgent)
        'Catch sqlex As SqlClient.SqlException
        '    'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        Try
            ds.Relations.Add(relNA)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relAgtInfo)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try


        Dim relCommInfo As New Data.DataRelation("CommInfo", ds.Tables("AgentCodes").Columns("AgentCode"), _
            ds.Tables("AgentList").Columns("AgentCode"), True)

        Try
            ds.Relations.Add(relCommInfo)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        blnLoading = False
        If ds.Tables("csw_cs_policy_list").Rows.Count > 0 Then
            strCurCSR = Trim(ds.Tables("csw_cs_policy_list").Rows(0).Item("cswcpl_csr"))
            strCurRmk = Trim(ds.Tables("csw_cs_policy_list").Rows(0).Item("cswcpl_remark"))
            cboCSR.SelectedValue = strCurCSR
            txtRemark.Text = strCurRmk
            blnRecFound = True

            ' If servicing branch is not 00802 but record exist in csw_cs_policy_list
            'gsUser Like "CSR*"
            'If strCurCSR <> "" AndAlso blnCanViewCSA Then
            'grbCSR.Visible = True
            'End If
        Else
            cboCSR.SelectedIndex = -1
            strCurCSR = ""
            strCurRmk = ""
            txtRemark.Text = ""
        End If

        ' ES0001 begin
        If g_Comp = "HKL" Then
            Me.txtBrokerPwd.Visible = False
            Me.Label23.Visible = False
        Else
            txtHKLBank.Visible = False
            txtHKLBranch.Visible = False
            txtHKLAgent.Visible = False
            Label24.Visible = False
            Label25.Visible = False
            Label26.Visible = False
        End If
        ' ES0001 end

        If ds.Tables("AgentList").Rows.Count > 0 Then
            With ds.Tables("AgentList").Rows(0)

                'With ds.Tables("POLINF")
                '    .Columns.Add("AgentStatus", GetType(String))
                'End With

                With ds.Tables("AgentList")
                    .Columns.Add("PolicyAccountRelationDesc", GetType(String))
                    .Columns.Add("NamePrefix", GetType(String))
                    .Columns.Add("NameSuffix", GetType(String))
                    .Columns.Add("FirstName", GetType(String))
                    .Columns.Add("ChiName", GetType(String))
                End With

                Dim ts As New clsDataGridTableStyle
                Dim cs As DataGridTextBoxColumn

                cs = New JoinTextBoxColumn("PolicyAcRel", ds.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationDesc"))
                cs.Width = 150
                cs.MappingName = "PolicyAccountRelationDesc"
                cs.HeaderText = "Role"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "AgentCode"
                cs.HeaderText = "Agent Code"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                'cs = New JoinTextBoxColumn("AgentRel", ds.Tables("AgentStatusCodes").Columns("AgentStatus"))
                'cs.Width = 150
                'cs.MappingName = "AgentStatus"
                'cs.HeaderText = "Agent Status"
                'cs.NullText = gNULLText
                'ts.GridColumnStyles.Add(cs)

                cs = New JoinTextBoxColumn("NARel", ds.Tables("ORDUNA").Columns("NamePrefix"))
                cs.Width = 50
                cs.MappingName = "NamePrefix"
                cs.HeaderText = "Title"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New JoinTextBoxColumn("NARel", ds.Tables("ORDUNA").Columns("NameSuffix"))
                cs.Width = 100
                cs.MappingName = "NameSuffix"
                cs.HeaderText = "Last Name"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New JoinTextBoxColumn("NARel", ds.Tables("ORDUNA").Columns("FirstName"))
                cs.Width = 100
                cs.MappingName = "FirstName"
                cs.HeaderText = "First Name"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New JoinTextBoxColumn("NARel", ds.Tables("ORDUNA").Columns("ChiName"))
                cs.Width = 100
                cs.MappingName = "ChiName"
                cs.HeaderText = "Chinese Name"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "AgentList"
                grdAgent.TableStyles.Add(ts)

                'grdAgent.DataSource = ds.Tables("Product")
                grdAgent.DataSource = ds.Tables("AgentList")
                grdAgent.AllowDrop = False
                grdAgent.ReadOnly = True

                ' License Info.
                ds.Tables("cswvw_agent_license").DefaultView.RowFilter() = "camalt_agent_no = '" & Trim(.Item("AgentCode")) & "'"
                Dim ts1 As New clsDataGridTableStyle

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

            End With

            'bm = Me.BindingContext(ds.Tables("POLINF"))

            'txtLastName.DataBindings.Add("Text", ds.Tables("POLINF"), "NameSuffix")
            'txtFirstName.DataBindings.Add("Text", ds.Tables("POLINF"), "FirstName")
            'txtChiName.DataBindings.Add("Text", ds.Tables("POLINF"), "ChiName")

            bm = Me.BindingContext(ds.Tables("AgentList"))

            Call UpdatePT()



        End If
        wndMain.Cursor = Cursors.Default
    End Sub

    Private Sub grdAgent_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAgent.CurrentCellChanged
        Call UpdatePT()
    End Sub

    Private Sub UpdatePT()

        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()
        Dim datLeft As Date
        Dim strLeft As String

        If Not drI Is Nothing Then

            ds.Tables("cswvw_agent_license").DefaultView.RowFilter() = "camalt_agent_no = '" & Trim(drI.Item("AgentCode")) & "'"
            'txtStatus.Text = GetRelationValue(drI, "AgentRel", "AgentStatus")
            txtRole.Text = GetRelationValue(drI, "PolicyAcRel", "PolicyAccountRelationDesc")

            'txtDivision.Text = GetRelationValue(drI, "AgentRel", "DivisionCode")
            'txtLocation.Text = GetRelationValue(drI, "AgentRel", "LocationCode")
            'txtUnit.Text = GetRelationValue(drI, "AgentRel", "UnitCode")
            'txtPhone.Text = GetRelationValue(drI, "AgentRel", "cswagi_res_phone")
            'txtMobile.Text = GetRelationValue(drI, "AgentRel", "cswagi_mob_phone")
            'txtBus.Text = GetRelationValue(drI, "AgentRel", "cswagi_bus_phone")
            'txtFax.Text = GetRelationValue(drI, "AgentRel", "cswagi_fax")
            'txtEmail.Text = GetRelationValue(drI, "AgentRel", "cswagi_email")
            'txtDM.Text = GetRelationValue(drI, "AgentRel", "cswagi_mgr_name")

            'txtDateJoin.Text = Format(GetRelationValue(drI, "AgentRel", "cswagi_contract_date"), gDateFormat)
            'txtCommPay.Text = GetRelationValue(drI, "AgentRel", "CommPayType")
            'txtAccNo.Text = GetRelationValue(drI, "AgentRel", "CommAcctNo")
            'txtLicFrom.Text = Format(GetRelationValue(drI, "AgentRel", "camalt_eff_date"), gDateFormat)
            'txtLicTo.Text = Format(GetRelationValue(drI, "AgentRel", "camalt_exp_date"), gDateFormat)
            'txtLicType.Text = GetRelationValue(drI, "AgentRel", "camlrt_license_name")
            'txtLicNo.Text = GetRelationValue(drI, "AgentRel", "camalt_license_no")
            'txtPosition.Text = GetRelationValue(drI, "AgentRel", "cswagi_desc")

            'If g_Comp = "ING" Then txtBrokerPwd.Text = GetBrokerVerificationKey(drI.Item("AgentCode")) ' ES0001
            ' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
            ' Add company parameter
            If g_Comp = "MCU" Then txtBrokerPwd.Text = GetBrokerVerificationKey(drI.Item("AgentCode"),g_McuComp) ' ES0001

            ' Get elite/noble agent start
            'If g_Comp = "ING" Then
            If g_Comp = "MCU" Then
                txtAgtGrade.Text = GetEliteAgent(drI.Item("AgentCode"))
                If txtAgtGrade.Text <> "STANDARD" Then
                    txtAgtGrade.BackColor = Color.Orange
                Else
                    txtAgtGrade.BackColor = Color.White
                End If
            End If
            ' Get elite/noble agent end

            txtLocation.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_loc_code")
            txtUnit.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_unit_code")
            txtDivision.Text = Microsoft.VisualBasic.Strings.Left(txtUnit.Text, 2)
            ' Show branch phone #
            ''txtPhone.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_res_phone")
            txtPhone.Text = GetRelationValue(drI, "CommInfo", "PhoneNumber")
            txtMobile.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_mob_phone")
            txtBus.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_bus_phone")
            txtFax.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_fax")
            txtEmail.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_email")
            txtDM.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_mgr_name")
            txtPosition.Text = GetRelationValue(drI, "AgtInfoRel", "cswagi_desc")

            Dim strHKIDtmp As String = GetRelationValue(drI, "AgtInfoRel", "cswagi_idno")
            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            'If ExternalUser And Not String.IsNullOrEmpty(strHKIDtmp) Then
            '    txtHKID.Text = MaskExternalUserData(MaskData.HKID, strHKIDtmp.Trim)
            'Else
            '    txtHKID.Text = strHKIDtmp
            'End If
            ''CRS 7x24 Changes - End
            txtHKID.Text = strHKIDtmp

            'txtHKID.Text = GetRelationValue(drI, "NARel", "GovernmentIDCard")
            txtDateJoin.Text = Format(GetRelationValue(drI, "AgtInfoRel", "cswagi_contract_date"), gDateFormat)
            'txtDateLeft.Text = Format(GetRelationValue(drI, "AgtInfoRel", "cswagi_date_left"), gDateFormat)

            txtLastName.Text = GetRelationValue(drI, "NARel", "NameSuffix")
            txtFirstName.Text = GetRelationValue(drI, "NARel", "FirstName")
            txtChiName.Text = GetRelationValue(drI, "NARel", "ChiName")

            txtCommPay.Text = GetRelationValue(drI, "CommInfo", "CommPayType")
            txtAccNo.Text = GetRelationValue(drI, "CommInfo", "CommAcctNo")

            strLeft = GetRelationValue(drI, "AgtInfoRel", "cswagi_date_left")
            datLeft = IIf(IsDate(strLeft), strLeft, #1/1/1900#)
            If datLeft = #1/1/1900# OrElse datLeft > Today Then
                txtStatus.Text = "Active"
                txtDateLeft.Text = ""
            Else
                txtStatus.Text = "Inactive"
                txtDateLeft.Text = Format(datLeft, gDateFormat)
            End If

            ' Start HKL001 - Map HKL agent to ING agent
            If g_Comp = "HKL" Then
                txtHKLBank.Text = GetRelationValue(drI, "AgtInfoRel", "camham_HKL_Bank")
                txtHKLBranch.Text = GetRelationValue(drI, "AgtInfoRel", "camham_HKL_Branch")
                txtHKLAgent.Text = GetRelationValue(drI, "AgtInfoRel", "camham_HKL_AgentNo")
            End If
            ' End HKL001

        End If

    End Sub

    Private Sub grdAgent_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAgent.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAgent.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAgent.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAgent.Select(hti.Row)
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
    ''' <summary>
    ''' Load ComboBox by business
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As Object, ByVal strCode As String, ByVal strName As String, ByVal busiId As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean

        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim blnLoad As Boolean

        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            dt.Clear()
           Dim ds As DataSet= APIServiceBL.CallAPIBusi(g_Comp,busiId,New Dictionary(Of String,String))
            dt=ds.Tables(0)
            blnLoad = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
            blnLoad = False
        End Try

        If blnLoad Then
            cbo.DataSource = dt
            cbo.DisplayMember = strName
            cbo.ValueMember = strCode
            Return True
        Else
            Return False
        End If

    End Function

    'oliver 2024-5-2 commented for Table_Relocate_Sprint13
    'Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

    '    Dim strSQL As String
    '    Dim intCnt As Integer
    '    Dim sqlConn As New SqlConnection
    '    Dim sqlCmd As New SqlCommand

    '    sqlConn.ConnectionString = strCIWMcuConn
    '    sqlConn.Open()
    '    sqlCmd.Connection = sqlConn

    '    Try
    '        ' New
    '        If blnRecFound = False Then
    '            If Trim(cboCSR.SelectedValue) <> "" OrElse Trim(txtRemark.Text) <> "" Then
    '                strSQL = "Insert csw_cs_policy_list " & _
    '                    " (cswcpl_policy_no, cswcpl_agent_no, cswcpl_csr, cswcpl_unit_code, cswcpl_remark, cswcpl_create_user, cswcpl_create_date, cswcpl_update_user, cswcpl_update_date) " & _
    '                    " Select '" & strPolicy & "','" & strSA & "','" & Trim(cboCSR.SelectedValue) & "','" & strSAB & "','" & Trim(txtRemark.Text) & "','" & gsUser & "',GETDATE(),'" & gsUser & "', GETDATE()"
    '            End If
    '        Else
    '            If Trim(cboCSR.SelectedValue) <> strCurCSR OrElse Trim(txtRemark.Text) <> strCurRmk Then
    '                strSQL = "Update csw_cs_policy_list " & _
    '                    " Set cswcpl_csr = '" & Trim(cboCSR.SelectedValue) & "', " & _
    '                    "     cswcpl_remark = '" & Trim(txtRemark.Text) & "', " & _
    '                    "     cswcpl_update_user = '" & gsUser & "', " & _
    '                    "     cswcpl_update_date = GETDATE() " & _
    '                    " Where cswcpl_policy_no = '" & strPolicy & "'"
    '            End If
    '        End If

    '        If strSQL <> "" Then
    '            sqlCmd.CommandText = strSQL
    '            intCnt = sqlCmd.ExecuteNonQuery()
    '        End If

    '    Catch sqlex As SqlClient.SqlException
    '        MsgBox(sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    End Try

    '    If intCnt > 0 AndAlso blnRecFound = False Then
    '        blnRecFound = True
    '        strCurCSR = cboCSR.SelectedValue
    '        strCurRmk = Trim(txtRemark.Text)
    '    End If

    '    cmdCancel.Enabled = False

    '    sqlConn.Close()
    '    sqlConn.Dispose()

    'End Sub

    'oliver 2024-5-2 added for Table_Relocate_Sprint13
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim isUpdate As Boolean = False
        If blnRecFound = False Then
            If Trim(cboCSR.SelectedValue) <> "" OrElse Trim(txtRemark.Text) <> "" Then
                isUpdate = InsertCSRRecord(strPolicy, strSA, Trim(cboCSR.SelectedValue), strSAB, Trim(txtRemark.Text))
            End If
        Else
            If Trim(cboCSR.SelectedValue) <> strCurCSR OrElse Trim(txtRemark.Text) <> strCurRmk Then
                isUpdate = UpdateCSRRecord(strPolicy, Trim(cboCSR.SelectedValue), Trim(txtRemark.Text))
            End If
        End If

        If isUpdate AndAlso blnRecFound = False Then
            blnRecFound = True
            strCurCSR = cboCSR.SelectedValue
            strCurRmk = Trim(txtRemark.Text)
        End If

        cmdCancel.Enabled = False

    End Sub

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function InsertCSRRecord(ByVal strPolicy As String, ByVal strSA As String, ByVal cboCSR As String, ByVal strSAB As String, ByVal txtRemark As String) As Boolean
        Dim isInsert As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(g_McuComp), "INSERT_CSR_RECORD",
                                New Dictionary(Of String, String) From {
                                {"strPolicy", strPolicy},
                                {"strSA", strSA},
                                {"cboCSR", If(Not String.IsNullOrEmpty(cboCSR), cboCSR, " ")},
                                {"strSAB", strSAB},
                                {"txtRemark", If(Not String.IsNullOrEmpty(txtRemark), txtRemark, " ")},
                                {"gsUser", gsUser}
                                })
            isInsert = True
        Catch ex As Exception
            HandleGlobalException(ex, "Error occurs when inserting the comments. Error:  " & ex.Message)
        End Try
        Return isInsert
    End Function

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function UpdateCSRRecord(ByVal strPolicy As String, ByVal cboCSR As String, ByVal txtRemark As String) As Boolean
        Dim isUpdate As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(g_McuComp), "UPDATE_CSR_RECORD",
                                New Dictionary(Of String, String) From {
                                {"strPolicy", strPolicy},
                                {"cboCSR", If(Not String.IsNullOrEmpty(cboCSR), cboCSR, " ")},
                                {"txtRemark", If(Not String.IsNullOrEmpty(txtRemark), txtRemark, " ")},
                                {"gsUser", gsUser}
                                })
            isUpdate = True
        Catch ex As Exception
            HandleGlobalException(ex, "Error occurs when updating the comments. Error:  " & ex.Message)
        End Try
        Return isUpdate
    End Function

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        cboCSR.SelectedValue = strCurCSR
        txtRemark.Text = strCurRmk
        cmdCancel.Enabled = False
    End Sub

    Private Sub cboCSR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCSR.SelectedIndexChanged
        If Not blnLoading AndAlso strCurCSR <> Trim(cboCSR.SelectedValue) Then
            cmdCancel.Enabled = True
        End If
    End Sub

    Private Sub txtRemark_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemark.TextChanged
        If Not blnLoading AndAlso strCurRmk <> Trim(txtRemark.Text) Then
            cmdCancel.Enabled = True
        End If
    End Sub

End Class
