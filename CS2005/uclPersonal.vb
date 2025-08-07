Imports System.Data.SqlClient

Public Class Personal
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If CheckUPSAccess("Save Demographic Data") = False Then
            Me.cmdSave.Visible = False
            Me.cmdCancel.Visible = False
            Me.cboOptCall.Enabled = False
            Me.cboOptEmail.Enabled = False
            Me.chkOptOutNPS.Enabled = False     'ES10
            Me.txtRemark.ReadOnly = True
            Me.txtRating.ReadOnly = True
        Else
            Me.chkOptOutNPS.Enabled = True     'ES10
        End If

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
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtChiName As System.Windows.Forms.TextBox
    Friend WithEvents txtIDCard As System.Windows.Forms.TextBox
    Friend WithEvents txtPassport As System.Windows.Forms.TextBox
    Friend WithEvents txtDOB As System.Windows.Forms.TextBox
    Friend WithEvents txtAge As System.Windows.Forms.TextBox
    Friend WithEvents txtMisc As System.Windows.Forms.TextBox
    Friend WithEvents txtAgtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDivision As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtSex As System.Windows.Forms.TextBox
    Friend WithEvents txtMarital As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustType As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtLang As System.Windows.Forms.TextBox
    Friend WithEvents txtOccup As System.Windows.Forms.TextBox
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtECoName As System.Windows.Forms.TextBox
    Friend WithEvents txtCCoName As System.Windows.Forms.TextBox
    Friend WithEvents chkChi As System.Windows.Forms.CheckBox
    Friend WithEvents lblCo As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblLast As System.Windows.Forms.Label
    Friend WithEvents lblFirst As System.Windows.Forms.Label
    Friend WithEvents lblChi As System.Windows.Forms.Label
    Friend WithEvents txtPOB As System.Windows.Forms.TextBox
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cboOptCall As System.Windows.Forms.ComboBox
    Friend WithEvents cboOptEmail As System.Windows.Forms.ComboBox
    Friend WithEvents chkOptMail As System.Windows.Forms.CheckBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents txtHouseholdIncome As System.Windows.Forms.TextBox
    Friend WithEvents txtPersonalIncome As System.Windows.Forms.TextBox
    Friend WithEvents txtDependNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEduLevel As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRating As System.Windows.Forms.TextBox
    Friend WithEvents lnkAgent As System.Windows.Forms.LinkLabel
    Friend WithEvents chkAgeAdm As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptOutNPS As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptOutOther As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtEBMember As TextBox
    Friend WithEvents txtEBID As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents txtEBIdType As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkVIP As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtChiName = New System.Windows.Forms.TextBox()
        Me.txtTel = New System.Windows.Forms.TextBox()
        Me.txtIDCard = New System.Windows.Forms.TextBox()
        Me.txtPassport = New System.Windows.Forms.TextBox()
        Me.txtDOB = New System.Windows.Forms.TextBox()
        Me.txtPOB = New System.Windows.Forms.TextBox()
        Me.txtAge = New System.Windows.Forms.TextBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.txtMisc = New System.Windows.Forms.TextBox()
        Me.txtAgtCode = New System.Windows.Forms.TextBox()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.lblLast = New System.Windows.Forms.Label()
        Me.lblFirst = New System.Windows.Forms.Label()
        Me.lblChi = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.txtSex = New System.Windows.Forms.TextBox()
        Me.txtMarital = New System.Windows.Forms.TextBox()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtCustType = New System.Windows.Forms.TextBox()
        Me.txtECoName = New System.Windows.Forms.TextBox()
        Me.txtCCoName = New System.Windows.Forms.TextBox()
        Me.lblCo = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtLang = New System.Windows.Forms.TextBox()
        Me.txtOccup = New System.Windows.Forms.TextBox()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.chkChi = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtHouseholdIncome = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPersonalIncome = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDependNo = New System.Windows.Forms.TextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboOptCall = New System.Windows.Forms.ComboBox()
        Me.cboOptEmail = New System.Windows.Forms.ComboBox()
        Me.chkOptMail = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEduLevel = New System.Windows.Forms.TextBox()
        Me.txtRating = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lnkAgent = New System.Windows.Forms.LinkLabel()
        Me.chkAgeAdm = New System.Windows.Forms.CheckBox()
        Me.chkVIP = New System.Windows.Forms.CheckBox()
        Me.chkOptOutNPS = New System.Windows.Forms.CheckBox()
        Me.chkOptOutOther = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEBMember = New System.Windows.Forms.TextBox()
        Me.txtEBID = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtEBIdType = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtFirstName
        '
        Me.txtFirstName.AcceptsReturn = True
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFirstName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFirstName.Location = New System.Drawing.Point(200, 20)
        Me.txtFirstName.MaxLength = 0
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFirstName.Size = New System.Drawing.Size(192, 26)
        Me.txtFirstName.TabIndex = 2
        '
        'txtChiName
        '
        Me.txtChiName.AcceptsReturn = True
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChiName.Font = New System.Drawing.Font("MingLiU_HKSCS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtChiName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChiName.Location = New System.Drawing.Point(396, 20)
        Me.txtChiName.MaxLength = 0
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChiName.Size = New System.Drawing.Size(92, 31)
        Me.txtChiName.TabIndex = 3
        '
        'txtTel
        '
        Me.txtTel.AcceptsReturn = True
        Me.txtTel.BackColor = System.Drawing.SystemColors.Window
        Me.txtTel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTel.Location = New System.Drawing.Point(424, 44)
        Me.txtTel.MaxLength = 0
        Me.txtTel.Name = "txtTel"
        Me.txtTel.ReadOnly = True
        Me.txtTel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTel.Size = New System.Drawing.Size(88, 26)
        Me.txtTel.TabIndex = 5
        '
        'txtIDCard
        '
        Me.txtIDCard.AcceptsReturn = True
        Me.txtIDCard.BackColor = System.Drawing.SystemColors.Window
        Me.txtIDCard.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIDCard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtIDCard.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIDCard.Location = New System.Drawing.Point(76, 44)
        Me.txtIDCard.MaxLength = 0
        Me.txtIDCard.Name = "txtIDCard"
        Me.txtIDCard.ReadOnly = True
        Me.txtIDCard.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIDCard.Size = New System.Drawing.Size(104, 26)
        Me.txtIDCard.TabIndex = 6
        '
        'txtPassport
        '
        Me.txtPassport.AcceptsReturn = True
        Me.txtPassport.BackColor = System.Drawing.SystemColors.Window
        Me.txtPassport.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPassport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPassport.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPassport.Location = New System.Drawing.Point(256, 44)
        Me.txtPassport.MaxLength = 0
        Me.txtPassport.Name = "txtPassport"
        Me.txtPassport.ReadOnly = True
        Me.txtPassport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPassport.Size = New System.Drawing.Size(88, 26)
        Me.txtPassport.TabIndex = 9
        '
        'txtDOB
        '
        Me.txtDOB.AcceptsReturn = True
        Me.txtDOB.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOB.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDOB.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDOB.Location = New System.Drawing.Point(236, 68)
        Me.txtDOB.MaxLength = 0
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.ReadOnly = True
        Me.txtDOB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOB.Size = New System.Drawing.Size(68, 26)
        Me.txtDOB.TabIndex = 12
        '
        'txtPOB
        '
        Me.txtPOB.AcceptsReturn = True
        Me.txtPOB.BackColor = System.Drawing.SystemColors.Window
        Me.txtPOB.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPOB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPOB.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPOB.Location = New System.Drawing.Point(420, 68)
        Me.txtPOB.MaxLength = 0
        Me.txtPOB.Name = "txtPOB"
        Me.txtPOB.ReadOnly = True
        Me.txtPOB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPOB.Size = New System.Drawing.Size(97, 26)
        Me.txtPOB.TabIndex = 14
        '
        'txtAge
        '
        Me.txtAge.AcceptsReturn = True
        Me.txtAge.BackColor = System.Drawing.SystemColors.Window
        Me.txtAge.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAge.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAge.Location = New System.Drawing.Point(308, 68)
        Me.txtAge.MaxLength = 0
        Me.txtAge.Name = "txtAge"
        Me.txtAge.ReadOnly = True
        Me.txtAge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAge.Size = New System.Drawing.Size(32, 26)
        Me.txtAge.TabIndex = 13
        '
        'txtMobile
        '
        Me.txtMobile.AcceptsReturn = True
        Me.txtMobile.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMobile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMobile.Location = New System.Drawing.Point(588, 44)
        Me.txtMobile.MaxLength = 0
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReadOnly = True
        Me.txtMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobile.Size = New System.Drawing.Size(104, 26)
        Me.txtMobile.TabIndex = 16
        '
        'txtMisc
        '
        Me.txtMisc.AcceptsReturn = True
        Me.txtMisc.BackColor = System.Drawing.SystemColors.Window
        Me.txtMisc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMisc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMisc.Location = New System.Drawing.Point(692, 0)
        Me.txtMisc.MaxLength = 0
        Me.txtMisc.Name = "txtMisc"
        Me.txtMisc.ReadOnly = True
        Me.txtMisc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMisc.Size = New System.Drawing.Size(228, 26)
        Me.txtMisc.TabIndex = 15
        Me.txtMisc.Visible = False
        '
        'txtAgtCode
        '
        Me.txtAgtCode.AcceptsReturn = True
        Me.txtAgtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAgtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAgtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgtCode.Location = New System.Drawing.Point(76, 140)
        Me.txtAgtCode.MaxLength = 0
        Me.txtAgtCode.Name = "txtAgtCode"
        Me.txtAgtCode.ReadOnly = True
        Me.txtAgtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAgtCode.Size = New System.Drawing.Size(49, 26)
        Me.txtAgtCode.TabIndex = 17
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDivision.Location = New System.Drawing.Point(184, 140)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.ReadOnly = True
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(49, 26)
        Me.txtDivision.TabIndex = 18
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(296, 140)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(49, 26)
        Me.txtLocation.TabIndex = 19
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnit.Location = New System.Drawing.Point(384, 140)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.ReadOnly = True
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(49, 26)
        Me.txtUnit.TabIndex = 20
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtEmail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmail.Location = New System.Drawing.Point(312, 92)
        Me.txtEmail.MaxLength = 0
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(200, 26)
        Me.txtEmail.TabIndex = 25
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(24, 4)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(41, 20)
        Me.Label35.TabIndex = 143
        Me.Label35.Text = "Title"
        '
        'lblLast
        '
        Me.lblLast.AutoSize = True
        Me.lblLast.BackColor = System.Drawing.SystemColors.Control
        Me.lblLast.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblLast.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLast.Location = New System.Drawing.Point(80, 4)
        Me.lblLast.Name = "lblLast"
        Me.lblLast.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLast.Size = New System.Drawing.Size(91, 20)
        Me.lblLast.TabIndex = 142
        Me.lblLast.Text = "Last Name"
        '
        'lblFirst
        '
        Me.lblFirst.AutoSize = True
        Me.lblFirst.BackColor = System.Drawing.SystemColors.Control
        Me.lblFirst.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblFirst.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFirst.Location = New System.Drawing.Point(200, 4)
        Me.lblFirst.Name = "lblFirst"
        Me.lblFirst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFirst.Size = New System.Drawing.Size(92, 20)
        Me.lblFirst.TabIndex = 141
        Me.lblFirst.Text = "First Name"
        '
        'lblChi
        '
        Me.lblChi.AutoSize = True
        Me.lblChi.BackColor = System.Drawing.SystemColors.Control
        Me.lblChi.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblChi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblChi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChi.Location = New System.Drawing.Point(396, 4)
        Me.lblChi.Name = "lblChi"
        Me.lblChi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblChi.Size = New System.Drawing.Size(119, 20)
        Me.lblChi.TabIndex = 140
        Me.lblChi.Text = "Chinese Name"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(492, 4)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(123, 20)
        Me.Label31.TabIndex = 139
        Me.Label31.Text = "Customer Type"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(352, 48)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(99, 20)
        Me.Label30.TabIndex = 138
        Me.Label30.Text = "Contact Tel."
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(28, 48)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(67, 20)
        Me.Label29.TabIndex = 137
        Me.Label29.Text = "ID Card"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(48, 72)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(37, 20)
        Me.Label28.TabIndex = 136
        Me.Label28.Text = "Sex"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(188, 48)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(98, 20)
        Me.Label26.TabIndex = 134
        Me.Label26.Text = "Passport ID"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(632, 4)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(116, 20)
        Me.Label24.TabIndex = 132
        Me.Label24.Text = "Miscellaneous"
        Me.Label24.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(164, 72)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(105, 20)
        Me.Label23.TabIndex = 131
        Me.Label23.Text = "Date of Birth"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(344, 72)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(111, 20)
        Me.Label19.TabIndex = 127
        Me.Label19.Text = "Place of Birth"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(524, 48)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(88, 20)
        Me.Label18.TabIndex = 126
        Me.Label18.Text = "Mobile No."
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(136, 144)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(69, 20)
        Me.Label17.TabIndex = 125
        Me.Label17.Text = "Division"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(244, 144)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(73, 20)
        Me.Label16.TabIndex = 124
        Me.Label16.Text = "Location"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(356, 144)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(39, 20)
        Me.Label15.TabIndex = 123
        Me.Label15.Text = "Unit"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(524, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(113, 20)
        Me.Label14.TabIndex = 122
        Me.Label14.Text = "Marital Status"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(272, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(51, 20)
        Me.Label10.TabIndex = 118
        Me.Label10.Text = "Email"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Location = New System.Drawing.Point(24, 20)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(48, 26)
        Me.txtTitle.TabIndex = 0
        '
        'txtSex
        '
        Me.txtSex.BackColor = System.Drawing.SystemColors.Window
        Me.txtSex.Location = New System.Drawing.Point(76, 68)
        Me.txtSex.Name = "txtSex"
        Me.txtSex.ReadOnly = True
        Me.txtSex.Size = New System.Drawing.Size(72, 26)
        Me.txtSex.TabIndex = 7
        '
        'txtMarital
        '
        Me.txtMarital.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarital.Location = New System.Drawing.Point(604, 68)
        Me.txtMarital.Name = "txtMarital"
        Me.txtMarital.ReadOnly = True
        Me.txtMarital.Size = New System.Drawing.Size(88, 26)
        Me.txtMarital.TabIndex = 21
        '
        'txtLastName
        '
        Me.txtLastName.AcceptsReturn = True
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLastName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastName.Location = New System.Drawing.Point(76, 20)
        Me.txtLastName.MaxLength = 0
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastName.Size = New System.Drawing.Size(120, 26)
        Me.txtLastName.TabIndex = 1
        '
        'txtCustType
        '
        Me.txtCustType.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustType.Location = New System.Drawing.Point(492, 20)
        Me.txtCustType.Name = "txtCustType"
        Me.txtCustType.ReadOnly = True
        Me.txtCustType.Size = New System.Drawing.Size(92, 26)
        Me.txtCustType.TabIndex = 4
        '
        'txtECoName
        '
        Me.txtECoName.BackColor = System.Drawing.SystemColors.Window
        Me.txtECoName.Location = New System.Drawing.Point(76, 20)
        Me.txtECoName.Name = "txtECoName"
        Me.txtECoName.ReadOnly = True
        Me.txtECoName.Size = New System.Drawing.Size(204, 26)
        Me.txtECoName.TabIndex = 150
        '
        'txtCCoName
        '
        Me.txtCCoName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCCoName.Location = New System.Drawing.Point(284, 20)
        Me.txtCCoName.Name = "txtCCoName"
        Me.txtCCoName.ReadOnly = True
        Me.txtCCoName.Size = New System.Drawing.Size(176, 26)
        Me.txtCCoName.TabIndex = 151
        '
        'lblCo
        '
        Me.lblCo.AutoSize = True
        Me.lblCo.Location = New System.Drawing.Point(80, 4)
        Me.lblCo.Name = "lblCo"
        Me.lblCo.Size = New System.Drawing.Size(122, 20)
        Me.lblCo.TabIndex = 152
        Me.lblCo.Text = "Company Name"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Location = New System.Drawing.Point(588, 20)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(104, 26)
        Me.txtStatus.TabIndex = 153
        '
        'txtLang
        '
        Me.txtLang.AcceptsReturn = True
        Me.txtLang.BackColor = System.Drawing.SystemColors.Window
        Me.txtLang.Location = New System.Drawing.Point(272, 116)
        Me.txtLang.MaxLength = 0
        Me.txtLang.Name = "txtLang"
        Me.txtLang.ReadOnly = True
        Me.txtLang.Size = New System.Drawing.Size(100, 26)
        Me.txtLang.TabIndex = 154
        '
        'txtOccup
        '
        Me.txtOccup.BackColor = System.Drawing.SystemColors.Window
        Me.txtOccup.Location = New System.Drawing.Point(76, 92)
        Me.txtOccup.Name = "txtOccup"
        Me.txtOccup.ReadOnly = True
        Me.txtOccup.Size = New System.Drawing.Size(188, 26)
        Me.txtOccup.TabIndex = 155
        '
        'txtCountry
        '
        Me.txtCountry.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountry.Location = New System.Drawing.Point(76, 116)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.ReadOnly = True
        Me.txtCountry.Size = New System.Drawing.Size(116, 26)
        Me.txtCountry.TabIndex = 156
        '
        'chkChi
        '
        Me.chkChi.AutoCheck = False
        Me.chkChi.Location = New System.Drawing.Point(588, 90)
        Me.chkChi.Name = "chkChi"
        Me.chkChi.Size = New System.Drawing.Size(104, 24)
        Me.chkChi.TabIndex = 159
        Me.chkChi.Text = "Prefer Chinese"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(588, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 20)
        Me.Label11.TabIndex = 160
        Me.Label11.Text = "Status"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(204, 118)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 20)
        Me.Label12.TabIndex = 161
        Me.Label12.Text = "Language"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 96)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 20)
        Me.Label13.TabIndex = 162
        Me.Label13.Text = "Occupation"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(28, 118)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 20)
        Me.Label21.TabIndex = 163
        Me.Label21.Text = "Country"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(22, 215)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 20)
        Me.Label5.TabIndex = 252
        Me.Label5.Text = "Remark"
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemark.Location = New System.Drawing.Point(76, 213)
        Me.txtRemark.MaxLength = 50
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(452, 26)
        Me.txtRemark.TabIndex = 251
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(236, 191)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(143, 20)
        Me.Label8.TabIndex = 250
        Me.Label8.Text = "Household Income"
        '
        'txtHouseholdIncome
        '
        Me.txtHouseholdIncome.BackColor = System.Drawing.SystemColors.Window
        Me.txtHouseholdIncome.Location = New System.Drawing.Point(344, 189)
        Me.txtHouseholdIncome.Name = "txtHouseholdIncome"
        Me.txtHouseholdIncome.ReadOnly = True
        Me.txtHouseholdIncome.Size = New System.Drawing.Size(116, 26)
        Me.txtHouseholdIncome.TabIndex = 249
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 191)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(128, 20)
        Me.Label7.TabIndex = 248
        Me.Label7.Text = "Personal Income"
        '
        'txtPersonalIncome
        '
        Me.txtPersonalIncome.BackColor = System.Drawing.SystemColors.Window
        Me.txtPersonalIncome.Location = New System.Drawing.Point(104, 189)
        Me.txtPersonalIncome.Name = "txtPersonalIncome"
        Me.txtPersonalIncome.ReadOnly = True
        Me.txtPersonalIncome.Size = New System.Drawing.Size(128, 26)
        Me.txtPersonalIncome.TabIndex = 247
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(508, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(143, 20)
        Me.Label6.TabIndex = 246
        Me.Label6.Text = "No. of Dependents"
        '
        'txtDependNo
        '
        Me.txtDependNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDependNo.Location = New System.Drawing.Point(616, 165)
        Me.txtDependNo.Name = "txtDependNo"
        Me.txtDependNo.ReadOnly = True
        Me.txtDependNo.Size = New System.Drawing.Size(84, 26)
        Me.txtDependNo.TabIndex = 245
        '
        'cmdCancel
        '
        Me.cmdCancel.ForeColor = System.Drawing.Color.Red
        Me.cmdCancel.Location = New System.Drawing.Point(616, 212)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 244
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdSave
        '
        Me.cmdSave.ForeColor = System.Drawing.Color.Red
        Me.cmdSave.Location = New System.Drawing.Point(536, 212)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 243
        Me.cmdSave.Text = "Save"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(12, 167)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 20)
        Me.Label2.TabIndex = 242
        Me.Label2.Text = "Optout Call"
        Me.Label2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(192, 167)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 20)
        Me.Label1.TabIndex = 241
        Me.Label1.Text = "OptOut Email"
        Me.Label1.Visible = False
        '
        'cboOptCall
        '
        Me.cboOptCall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOptCall.Items.AddRange(New Object() {"Yes", "No"})
        Me.cboOptCall.Location = New System.Drawing.Point(76, 165)
        Me.cboOptCall.Name = "cboOptCall"
        Me.cboOptCall.Size = New System.Drawing.Size(76, 28)
        Me.cboOptCall.TabIndex = 240
        Me.cboOptCall.Visible = False
        '
        'cboOptEmail
        '
        Me.cboOptEmail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOptEmail.Items.AddRange(New Object() {"Yes", "No"})
        Me.cboOptEmail.Location = New System.Drawing.Point(272, 165)
        Me.cboOptEmail.Name = "cboOptEmail"
        Me.cboOptEmail.Size = New System.Drawing.Size(76, 28)
        Me.cboOptEmail.TabIndex = 239
        Me.cboOptEmail.Visible = False
        '
        'chkOptMail
        '
        Me.chkOptMail.AutoCheck = False
        Me.chkOptMail.Location = New System.Drawing.Point(77, 163)
        Me.chkOptMail.Name = "chkOptMail"
        Me.chkOptMail.Size = New System.Drawing.Size(152, 24)
        Me.chkOptMail.TabIndex = 238
        Me.chkOptMail.Text = "Opt-Out Direct Marketing"
        Me.chkOptMail.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(397, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 20)
        Me.Label3.TabIndex = 253
        Me.Label3.Text = "Education"
        '
        'txtEduLevel
        '
        Me.txtEduLevel.BackColor = System.Drawing.SystemColors.Window
        Me.txtEduLevel.Location = New System.Drawing.Point(457, 116)
        Me.txtEduLevel.Name = "txtEduLevel"
        Me.txtEduLevel.ReadOnly = True
        Me.txtEduLevel.Size = New System.Drawing.Size(235, 26)
        Me.txtEduLevel.TabIndex = 254
        '
        'txtRating
        '
        Me.txtRating.AcceptsReturn = True
        Me.txtRating.BackColor = System.Drawing.SystemColors.Window
        Me.txtRating.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRating.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRating.Location = New System.Drawing.Point(524, 188)
        Me.txtRating.MaxLength = 9
        Me.txtRating.Name = "txtRating"
        Me.txtRating.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRating.Size = New System.Drawing.Size(49, 26)
        Me.txtRating.TabIndex = 255
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(480, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 20)
        Me.Label4.TabIndex = 256
        Me.Label4.Text = "Rating"
        '
        'lnkAgent
        '
        Me.lnkAgent.AutoSize = True
        Me.lnkAgent.Location = New System.Drawing.Point(4, 144)
        Me.lnkAgent.Name = "lnkAgent"
        Me.lnkAgent.Size = New System.Drawing.Size(94, 20)
        Me.lnkAgent.TabIndex = 257
        Me.lnkAgent.TabStop = True
        Me.lnkAgent.Text = "Agent Code"
        '
        'chkAgeAdm
        '
        Me.chkAgeAdm.AutoCheck = False
        Me.chkAgeAdm.Location = New System.Drawing.Point(460, 140)
        Me.chkAgeAdm.Name = "chkAgeAdm"
        Me.chkAgeAdm.Size = New System.Drawing.Size(104, 24)
        Me.chkAgeAdm.TabIndex = 258
        Me.chkAgeAdm.Text = "Age Adm Ind"
        '
        'chkVIP
        '
        Me.chkVIP.AutoCheck = False
        Me.chkVIP.Location = New System.Drawing.Point(576, 140)
        Me.chkVIP.Name = "chkVIP"
        Me.chkVIP.Size = New System.Drawing.Size(104, 24)
        Me.chkVIP.TabIndex = 259
        Me.chkVIP.Text = "VIP Customer"
        '
        'chkOptOutNPS
        '
        Me.chkOptOutNPS.AutoSize = True
        Me.chkOptOutNPS.ForeColor = System.Drawing.Color.Red
        Me.chkOptOutNPS.Location = New System.Drawing.Point(584, 190)
        Me.chkOptOutNPS.Name = "chkOptOutNPS"
        Me.chkOptOutNPS.Size = New System.Drawing.Size(175, 24)
        Me.chkOptOutNPS.TabIndex = 260
        Me.chkOptOutNPS.Text = "OptOut NPS Survey"
        Me.chkOptOutNPS.UseVisualStyleBackColor = True
        Me.chkOptOutNPS.Visible = False
        '
        'chkOptOutOther
        '
        Me.chkOptOutOther.AutoCheck = False
        Me.chkOptOutOther.Location = New System.Drawing.Point(236, 162)
        Me.chkOptOutOther.Name = "chkOptOutOther"
        Me.chkOptOutOther.Size = New System.Drawing.Size(197, 24)
        Me.chkOptOutOther.TabIndex = 261
        Me.chkOptOutOther.Text = "Opt-out 3rd party Direct Marketing"
        Me.chkOptOutOther.UseVisualStyleBackColor = True
        Me.chkOptOutOther.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 36)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(186, 30)
        Me.Label9.TabIndex = 262
        Me.Label9.Text = "Opt In/Out Form"
        '
        'txtEBMember
        '
        Me.txtEBMember.BackColor = System.Drawing.SystemColors.Window
        Me.txtEBMember.Location = New System.Drawing.Point(123, 33)
        Me.txtEBMember.Name = "txtEBMember"
        Me.txtEBMember.ReadOnly = True
        Me.txtEBMember.Size = New System.Drawing.Size(48, 26)
        Me.txtEBMember.TabIndex = 263
        '
        'txtEBID
        '
        Me.txtEBID.BackColor = System.Drawing.SystemColors.Window
        Me.txtEBID.Location = New System.Drawing.Point(293, 33)
        Me.txtEBID.Name = "txtEBID"
        Me.txtEBID.ReadOnly = True
        Me.txtEBID.Size = New System.Drawing.Size(147, 26)
        Me.txtEBID.TabIndex = 265
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(184, 36)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(143, 20)
        Me.Label20.TabIndex = 264
        Me.Label20.Text = "HKID/Passport No."
        '
        'txtEBIdType
        '
        Me.txtEBIdType.BackColor = System.Drawing.SystemColors.Window
        Me.txtEBIdType.Location = New System.Drawing.Point(513, 33)
        Me.txtEBIdType.Name = "txtEBIdType"
        Me.txtEBIdType.ReadOnly = True
        Me.txtEBIdType.Size = New System.Drawing.Size(139, 26)
        Me.txtEBIdType.TabIndex = 267
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(452, 36)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(64, 20)
        Me.Label22.TabIndex = 266
        Me.Label22.Text = "ID Type"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtEBIdType)
        Me.GroupBox1.Controls.Add(Me.txtEBMember)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtEBID)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 258)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(680, 75)
        Me.GroupBox1.TabIndex = 268
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "EB Opt In/Out Info"
        '
        'Personal
        '
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkOptMail)
        Me.Controls.Add(Me.chkOptOutOther)
        Me.Controls.Add(Me.chkOptOutNPS)
        Me.Controls.Add(Me.chkVIP)
        Me.Controls.Add(Me.chkAgeAdm)
        Me.Controls.Add(Me.lnkAgent)
        Me.Controls.Add(Me.txtRating)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtEduLevel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtHouseholdIncome)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPersonalIncome)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtDependNo)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboOptCall)
        Me.Controls.Add(Me.cboOptEmail)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.chkChi)
        Me.Controls.Add(Me.txtCountry)
        Me.Controls.Add(Me.txtOccup)
        Me.Controls.Add(Me.txtLang)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtCustType)
        Me.Controls.Add(Me.txtMarital)
        Me.Controls.Add(Me.txtSex)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.txtChiName)
        Me.Controls.Add(Me.txtTel)
        Me.Controls.Add(Me.txtIDCard)
        Me.Controls.Add(Me.txtPassport)
        Me.Controls.Add(Me.txtDOB)
        Me.Controls.Add(Me.txtPOB)
        Me.Controls.Add(Me.txtAge)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.txtMisc)
        Me.Controls.Add(Me.txtAgtCode)
        Me.Controls.Add(Me.txtDivision)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.txtUnit)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.lblLast)
        Me.Controls.Add(Me.lblFirst)
        Me.Controls.Add(Me.lblChi)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblCo)
        Me.Controls.Add(Me.txtCCoName)
        Me.Controls.Add(Me.txtECoName)
        Me.Name = "Personal"
        Me.Size = New System.Drawing.Size(719, 360)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private ds As DataSet = New DataSet("Personal")
    Private strCustID, strClientID As String
    Private dr, dr1 As DataRow
    Private blnEditable As Boolean
    Private blnCovSmoker As Boolean

    Public Property CustID(ByVal ClientID As String, ByVal srcDT As DataTable, ByVal scrCIWDT As DataTable, ByVal CovSmoker As Boolean) As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                If Value.Length > 0 Then
                    strCustID = Value
                    strClientID = ClientID
                    blnCovSmoker = CovSmoker
                    srcDT.TableName = "Customer"
                    If ds.Tables.Contains(srcDT.TableName) Then
                        ds.Tables(srcDT.TableName).Constraints.Clear()
                        ds.Relations.Clear()
                        ds.Tables.Remove(srcDT.TableName)
                    End If
                    ds.Tables.Add(srcDT)
                    scrCIWDT.TableName = "CIWCustomer"
                    If ds.Tables.Contains(scrCIWDT.TableName) Then
                        ds.Tables(scrCIWDT.TableName).Constraints.Clear()
                        ds.Relations.Clear()
                        ds.Tables.Remove(scrCIWDT.TableName)
                    End If
                    ds.Tables.Add(scrCIWDT)
                    Call buildUI()
                End If
            End If
        End Set
    End Property

    'Clear All Data in the control
    Public Sub ClearTextBox()
        Dim ctl As Control

        For Each ctl In Me.Controls
            Select Case TypeName(ctl).ToUpper
                Case "TEXTBOX"
                    CType(ctl, TextBox).Text = ""
                Case "COMBOBOX"
                    CType(ctl, ComboBox).SelectedIndex = -1
                Case "CHECKBOX"
                    CType(ctl, CheckBox).Checked = False
            End Select
        Next
    End Sub

    Public Sub EnableButtons(ByVal blnCanEdit As Boolean)
        blnEditable = blnCanEdit
        Me.cmdSave.Enabled = blnEditable
        Me.cmdCancel.Enabled = blnEditable
    End Sub

    'oliver 2024-5-3 added for Table_Relocate_Sprint13
    Public Function GetCountryAndCustomerCodes() As DataSet
        Dim ds As DataSet = New DataSet()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_COUNTRY_CUSTOMER_CODES",
                        New Dictionary(Of String, String))
            If ds.Tables.Count > 0 Then
                Return ds
            End If

        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try
        Return ds
    End Function

    Private Function buildUI()

        'oliver 2024-5-3 commented for Table_Relocate_Sprint13
        'Dim sqlconnect As New SqlConnection
        'Dim strSQL As String
        'Dim lngCnt As Long
        'Dim sqlda As SqlDataAdapter

        'Try
        '    'strSQL = "Select *, rtrim(ChiLstNm)  + rtrim(ChiFstNm) as ChiName from Customer c left join AgentCodes a " & _
        '    '    " ON c.agentcode = a.agentcode " & _
        '    '    " Where c.customerid = '" & strCustID & "'; "
        '    strSQL &= "select CountryCode, Country from CountryCodes; "
        '    strSQL &= "select CustomerStatusCode, CustomerStatus from CustomerStatusCodes; "
        '    strSQL &= "select CustomerTypeCode, CustomerType from CustomerTypeCodes; "
        '    strSQL &= "select MaritalStatusCode, MaritalStatusDesc from MaritalStatusCodes; "
        '    strSQL &= "select LanguageCode, Language from LanguageCodes; "
        '    strSQL &= "select cswdgc_type, cswdgc_id, cswdgc_desc from csw_demographic_codes; "

        '    sqlconnect.ConnectionString = strCIWConn

        '    sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        '    sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        '    sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        '    'sqlda.TableMappings.Add("Customer1", "CountryCodes")
        '    'sqlda.TableMappings.Add("Customer2", "CustomerStatusCodes")
        '    'sqlda.TableMappings.Add("Customer3", "CustomerTypeCodes")
        '    'sqlda.TableMappings.Add("Customer4", "MaritalStatusCodes")
        '    'sqlda.TableMappings.Add("Customer5", "LanguageCodes")
        '    'sqlda.TableMappings.Add("Customer6", "CustomerAddress")
        '    'sqlda.TableMappings.Add("Customer7", "AddressTypeCodes")
        '    'sqlda.Fill(ds, "Customer")
        '    sqlda.TableMappings.Add("CountryCodes1", "CustomerStatusCodes")
        '    sqlda.TableMappings.Add("CountryCodes2", "CustomerTypeCodes")
        '    sqlda.TableMappings.Add("CountryCodes3", "MaritalStatusCodes")
        '    sqlda.TableMappings.Add("CountryCodes4", "LanguageCodes")
        '    'sqlda.TableMappings.Add("CountryCodes5", "CustomerAddress")
        '    'sqlda.TableMappings.Add("CountryCodes6", "AddressTypeCodes")
        '    sqlda.TableMappings.Add("CountryCodes5", "csw_demographic_codes")
        '    sqlda.Fill(ds, "CountryCodes")

        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'End Try
        'sqlconnect.Dispose()

        'oliver 2024-5-3 added for Table_Relocate_Sprint13
        Dim countryAndCustomerCodesDataSet As DataSet = GetCountryAndCustomerCodes()
        For Each dt As DataTable In countryAndCustomerCodesDataSet.Tables
            ds.Tables.Add(dt.Copy)
        Next

        Dim relCountry As New Data.DataRelation("Country", ds.Tables("CountryCodes").Columns("CountryCode"),
            ds.Tables("Customer").Columns("CountryCode"), True)
        Dim relStatus As New Data.DataRelation("Status", ds.Tables("CustomerStatusCodes").Columns("CustomerStatusCode"),
            ds.Tables("Customer").Columns("CustomerStatusCode"), True)
        Dim relCustType As New Data.DataRelation("CustType", ds.Tables("CustomerTypeCodes").Columns("CustomerTypeCode"),
            ds.Tables("Customer").Columns("CustomerTypeCode"), True)
        Dim relMarital As New Data.DataRelation("Marital", ds.Tables("MaritalStatusCodes").Columns("MaritalStatusCode"),
            ds.Tables("Customer").Columns("MaritalStatusCode"), True)
        Dim relLanguage As New Data.DataRelation("Language", ds.Tables("LanguageCodes").Columns("LanguageCode"),
            ds.Tables("Customer").Columns("LanguageCode"), True)

        Try
            ds.Relations.Add(relCountry)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relStatus)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relCustType)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relMarital)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relLanguage)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Call UpdatePT()

    End Function

    Private Sub UpdatePT()

        If ds.Tables("Customer").Rows.Count > 0 Then
            With ds.Tables("Customer").Rows(0)

                If Not IsDBNull(.Item("NamePrefix")) Then Me.txtTitle.Text = .Item("NamePrefix")

                If Not IsDBNull(.Item("Gender")) Then
                    If .Item("Gender") = "C" Then
                        lblLast.Visible = False
                        lblFirst.Visible = False
                        lblChi.Visible = False
                        Me.txtLastName.Visible = False
                        Me.txtFirstName.Visible = False
                        Me.txtChiName.Visible = False

                        If Not IsDBNull(.Item("CoName")) Then Me.txtECoName.Text = .Item("CoName")
                        If Not IsDBNull(.Item("CoCName")) Then Me.txtCCoName.Text = .Item("CoCName")

                        ' Add 11/29/2006 - VIP flag
                        If GetVIPStatus(strCustID) = "1" Then
                            Me.chkVIP.Checked = True
                            Me.txtECoName.BackColor = System.Drawing.Color.Orange
                            Me.txtCCoName.BackColor = System.Drawing.Color.Orange
                        End If
                        ' End Add
                    Else
                        lblCo.Visible = False
                        Me.txtECoName.Visible = False
                        Me.txtCCoName.Visible = False

                        If Not IsDBNull(.Item("NameSuffix")) Then Me.txtLastName.Text = .Item("NameSuffix")
                        If Not IsDBNull(.Item("FirstName")) Then Me.txtFirstName.Text = .Item("FirstName")
                        If Not IsDBNull(.Item("ChiName")) Then Me.txtChiName.Text = .Item("ChiName")

                        ' Add 11/29/2006 - VIP flag
                        If GetVIPStatus(strCustID) = "1" Then
                            Me.chkVIP.Checked = True
                            Me.txtLastName.BackColor = System.Drawing.Color.Orange
                            Me.txtFirstName.BackColor = System.Drawing.Color.Orange
                            Me.txtChiName.BackColor = System.Drawing.Color.Orange
                        End If
                        ' End Add
                    End If
                End If

                'If Not IsDBNull(.Item("PINNumber")) Then Me.txtPIN.Text = .Item("PINNumber")
                'CRS 7x24 Changes - Start
                If Not IsDBNull(.Item("GovernmentIDCard")) Then
                    ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                    'If ExternalUser Then
                    '    Me.txtIDCard.Text = MaskExternalUserData(MaskData.HKID, .Item("GovernmentIDCard"))
                    'Else
                    '    Me.txtIDCard.Text = .Item("GovernmentIDCard")
                    'End If
                    Me.txtIDCard.Text = .Item("GovernmentIDCard")
                End If
                'CRS 7x24 Changes - End

                If Not IsDBNull(.Item("Gender")) Then
                    Select Case .Item("Gender")
                        Case "M"
                            Me.txtSex.Text = "Male"
                        Case "F"
                            Me.txtSex.Text = "Female"
                        Case "C"
                            Me.txtSex.Text = "Company"
                        Case "U"
                            Me.txtSex.Text = "Unclassified"
                    End Select
                End If

                'If Not IsDBNull(.Item("SmokerFlag")) Then
                '    If .Item("SmokerFlag") = "S" Then Me.chkSmoker.Checked = True
                'End If

                '' If NA mark as Non-smoker, but this person is insured of a policy marked as smoker, turn on the flag too
                'If chkSmoker.Checked = False And blnCovSmoker = True Then
                '    Me.chkSmoker.Checked = True
                'End If

                ''If Not IsDBNull(.Item("PassportNumber")) Then Me.txtPassport.Text = .Item("PassportNumber")
                If Not IsDBNull(.Item("OptOutFlag")) Then
                    If .Item("OptOutFlag") = "1" Then Me.chkOptMail.Checked = True
                End If

                ' Enhancement for PDPO Start
                If Not IsDBNull(.Item("OptOutOtherFlag")) Then
                    If .Item("OptOutOtherFlag") = "1" Then Me.chkOptOutOther.Checked = True
                End If
                ' Enhancement for PDPO End

                If Not IsDBNull(.Item("UseChiInd")) Then
                    If .Item("UseChiInd") = "Y" Then Me.chkChi.Checked = True
                End If

                ' Add 6/20 - new underwriting rule
                'start remark by kit for checking work flow
                If Not IsDBNull(.Item("AgeAdmInd")) Then
                    If .Item("AgeAdmInd") = "Y" Then Me.chkAgeAdm.Checked = True
                End If
                'end remark by kit for checking work flow
                ' End Add

                Dim dtDOB As DateTime

                If Not IsDBNull(.Item("DateOfBirth")) Then
                    dtDOB = .Item("DateOfBirth")
                    Me.txtDOB.Text = Format(dtDOB, gDateFormat)

                    If Now >= DateSerial(Year(Now), dtDOB.Month, dtDOB.Day) Then
                        txtAge.Text = System.Math.Abs(DateDiff("yyyy", Now, dtDOB)) + 1
                    Else
                        txtAge.Text = System.Math.Abs(DateDiff("yyyy", Now, dtDOB))
                    End If

                End If
                ''If Not IsDBNull(.Item("Nationality")) Then Me.txtNationality.Text = .Item("Nationality")
                ''If Not IsDBNull(.Item("DriverLicenceNo")) Then Me.txtDriveLic.Text = .Item("DriverLicenceNo")
                ''If Not IsDBNull(.Item("Remarks")) Then Me.txtMisc.Text = .Item("Remarks")

                ''' Temp. solution, update COM next time
                'Me.txtCustType.Text = "Client"
                If Not IsDBNull(.Item("AgentCode")) Then
                    If Trim(.Item("AgentCode")) <> "" Then
                        'Me.txtCustType.Text = "Agent"
                        Me.txtAgtCode.Text = .Item("AgentCode")
                        'If Not IsDBNull(.Item("DivisionCode")) Then Me.txtDivision.Text = .Item("DivisionCode")
                        'If Not IsDBNull(.Item("LocationCode")) Then Me.txtLocation.Text = .Item("LocationCode")
                        'If Not IsDBNull(.Item("UnitCode")) Then Me.txtUnit.Text = .Item("UnitCode")
                        Dim strLoc As String = GetLocation(.Item("AgentCode"))
                        If Not strLoc Is Nothing AndAlso strLoc.Length = 9 Then
                            Me.txtUnit.Text = strLoc.Substring(0, 5)
                            Me.txtDivision.Text = strLoc.Substring(0, 2)
                            Me.txtLocation.Text = strLoc.Substring(5, 4)
                        End If
                    End If
                End If

                If Not IsDBNull(.Item("EmailAddr")) Then Me.txtEmail.Text = .Item("EmailAddr")
                ' CRS Ph.2 remove
                'If Not IsDBNull(.Item("Occupation")) Then Me.txtOccup.Text = .Item("Occupation")
                If Not IsDBNull(.Item("BirthPlace")) Then Me.txtPOB.Text = .Item("BirthPlace")
                If Not IsDBNull(.Item("PhoneMobile")) Then Me.txtMobile.Text = .Item("PhoneMobile")
                If Not IsDBNull(.Item("PhonePager")) Then Me.txtTel.Text = .Item("PhonePager")
                If Not IsDBNull(.Item("PassportNumber")) Then Me.txtPassport.Text = .Item("PassportNumber")

                dr = ds.Tables("Customer").Rows(0)
                'dr1 = dr.GetParentRow("Country")
                'Me.txtCountry.Text = dr1.Item("Country")

                'dr1 = dr.GetParentRow("Status")
                'Me.txtStatus.Text = dr1.Item("CustomerStatus")

                'dr1 = dr.GetParentRow("CustType")
                'Me.txtCustType.Text = dr1.Item("CustomerType")

                'dr1 = dr.GetParentRow("Marital")
                'Me.txtMarital.Text = dr1.Item("MaritalStatusDesc")

                'dr1 = dr.GetParentRow("Language")
                'Me.txtLang.Text = dr1.Item("Language")

                Me.txtCountry.Text = GetRelationValue(dr, "Country", "Country")
                Me.txtStatus.Text = GetRelationValue(dr, "Status", "CustomerStatus")
                ''' Always = "AG"
                Me.txtCustType.Text = GetRelationValue(dr, "CustType", "CustomerType")
                Me.txtMarital.Text = GetRelationValue(dr, "Marital", "MaritalStatusDesc")
                Me.txtLang.Text = GetRelationValue(dr, "Language", "Language")

                'dr1 = ds.Tables("CountryCodes").Select("CountryCode = '" & .Item("CountryCode") & "'")
                'If dr1.Length > 0 Then Me.txtCountry.Text = dr1(0).Item("Country")

                'dr1 = ds.Tables("CustomerStatusCodes").Select("CustomerStatusCode = '" & .Item("CustomerStatusCode") & "'")
                'If dr1.Length > 0 Then Me.txtStatus.Text = dr1(0).Item("CustomerStatus")

                'dr1 = ds.Tables("CustomerTypeCodes").Select("CustomerTypeCode = '" & .Item("CustomerTypeCode") & "'")
                'If dr1.Length > 0 Then Me.txtCustType.Text = dr1(0).Item("CustomerType")

                'dr1 = ds.Tables("MaritalStatusCodes").Select("MaritalStatusCode = '" & .Item("MaritalStatusCode") & "'")
                'If dr1.Length > 0 Then Me.txtMarital.Text = dr1(0).Item("MaritalStatusDesc")

                'dr1 = ds.Tables("LanguageCodes").Select("LanguageCode = '" & .Item("LanguageCode") & "'")
                'If dr1.Length > 0 Then Me.txtLang.Text = dr1(0).Item("Language")

            End With

        Else
            Call ClearTextBox()
        End If

        'Add for Phase2
        If ds.Tables("CIWCustomer").Rows.Count > 0 Then
            With ds.Tables("CIWCustomer").Rows(0)
                If .IsNull("OptEmail") Then
                    Me.cboOptEmail.SelectedIndex = -1
                ElseIf .Item("OptEmail") = "Y" Then
                    Me.cboOptEmail.Text = "Yes"
                Else
                    Me.cboOptEmail.Text = "No"
                End If
                If .IsNull("OptCall") Then
                    Me.cboOptCall.SelectedIndex = -1
                ElseIf .Item("OptCall") = "Y" Then
                    Me.cboOptCall.Text = "Yes"
                Else
                    Me.cboOptCall.Text = "No"
                End If

                Me.txtRating.Text = IIf(.IsNull("Rating"), gNULLText, .Item("Rating"))
                Me.txtRemark.Text = IIf(.IsNull("Remarks"), gNULLText, .Item("Remarks"))

                'Me.txtDependNo.Text = IIf(.IsNull("DependNo"), gNULLText, .Item("DependNo"))
                'Me.txtEduLevel.Text = IIf(IsDBNull(.Item("EduLevel")), gNULLText, MapDemographic("E", .Item("EduLevel")))
                'Me.txtPersonalIncome.Text = IIf(.IsNull("PersonalIncome"), gNULLText, MapDemographic("P", .Item("PersonalIncome")))
                'Me.txtHouseholdIncome.Text = IIf(.IsNull("HouseholdIncome"), gNULLText, MapDemographic("H", .Item("HouseholdIncome")))
                'Me.txtOccup.Text = IIf(.IsNull("Occup"), gNULLText, MapDemographic("O", .Item("Occup")))

                If IsDBNull(.Item("DependNo")) Then
                    Me.txtDependNo.Text = gNULLText
                Else
                    Me.txtDependNo.Text = MapDemographic("D", .Item("DependNo"))
                End If

                If IsDBNull(.Item("EduLevel")) Then
                    Me.txtEduLevel.Text = gNULLText
                Else
                    Me.txtEduLevel.Text = MapDemographic("E", .Item("EduLevel"))
                End If

                If .IsNull("PersonalIncome") Then
                    Me.txtPersonalIncome.Text = gNULLText
                Else
                    Me.txtPersonalIncome.Text = MapDemographic("P", .Item("PersonalIncome"))
                End If

                If .IsNull("HouseholdIncome") Then
                    Me.txtHouseholdIncome.Text = gNULLText
                Else
                    Me.txtHouseholdIncome.Text = MapDemographic("H", .Item("HouseholdIncome"))
                End If

                If .IsNull("Occup") Then
                    Me.txtOccup.Text = gNULLText
                Else
                    Me.txtOccup.Text = MapDemographic("O", .Item("Occup"))
                End If

                '**** ES04 begin ****
                If .IsNull("cswdgm_optout_nps") Then
                    Me.chkOptOutNPS.Checked = False
                Else
                    If .Item("cswdgm_optout_nps") = "Y" Then
                        Me.chkOptOutNPS.Checked = True
                    Else
                        Me.chkOptOutNPS.Checked = False
                    End If
                End If
                '**** ES04 end ****

            End With
        Else
            Me.cboOptEmail.SelectedIndex = -1
            Me.cboOptCall.SelectedIndex = -1
            Me.txtRating.Text = gNULLText
            Me.txtDependNo.Text = gNULLText
            Me.txtEduLevel.Text = gNULLText
            Me.txtPersonalIncome.Text = gNULLText
            Me.txtHouseholdIncome.Text = gNULLText
            Me.txtRemark.Text = gNULLText
            Me.chkOptOutNPS.Checked = False     'ES04
        End If

        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    cmdSave.Enabled = False
        '    cmdCancel.Enabled = False
        'End If
        ''CRS 7x24 Changes - End

        'Add For eService Otp in out
        Dim strCondition = " and CustomerID = '" & strCustID & "' "
        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_Comp, "EBMEMBER_RECORD", New Dictionary(Of String, String)() From {
                {"condition", strCondition}
                })

        If Not retDs.Tables Is Nothing Then
            If retDs.Tables(0).Rows.Count > 0 Then
                txtEBMember.Text = "Y"
                txtEBID.Text = retDs.Tables(0).Rows(0).Item("HKID").ToString()
                txtEBIdType.Text = retDs.Tables(0).Rows(0).Item("IdType").ToString()
            End If
        End If

    End Sub

    Private Function MapDemographic(ByVal strType As String, ByVal strVal As String) As String

        Dim dr As DataRow()

        Try
            dr = ds.Tables("csw_demographic_codes").Select("cswdgc_type = '" & strType & "' AND cswdgc_id = '" & strVal & "'")
        Catch ex As Exception
            Return gNULLText
        End Try

        Return dr(0).Item("cswdgc_desc")

    End Function

    Private Function Validation() As Boolean
        Dim strErrMsg As String
        Dim strRating As String

        Validation = False

        'Check Input
        strRating = Me.txtRating.Text.Trim
        If strRating.Length > 0 Then
            If Not IsNumeric(strRating) Then
                MsgBox("Rating should be a integer.", MsgBoxStyle.Critical, "Error")
                txtRating.Focus()
                Exit Function
            ElseIf strRating > 999999999 Then
                MsgBox("Rating is out of range.", MsgBoxStyle.Critical, "Error")
                txtRating.Focus()
                Exit Function
            ElseIf Int(strRating) <> strRating Then
                MsgBox("Rating should be a integer.", MsgBoxStyle.Critical, "Error")
                txtRating.Focus()
                Exit Function
            End If
        End If

        Validation = True

    End Function

    'oliver 2024-5-3 added for Table_Relocate_Sprint13
    Private Sub SaveOptFlags()
        Dim drData As DataRow

        Dim strRating As String
        Dim strOptEmail As String
        Dim strOptCall As String
        Dim strRemark As String

        If Me.txtRating.Text.Trim.Length > 0 Then
            strRating = Me.txtRating.Text.Trim
        Else
            strRating = ""
        End If
        If Me.cboOptEmail.Text.Length > 0 Then
            strOptEmail = "'" & Me.cboOptEmail.Text.Substring(0, 1).Replace("'", "''") & "'"
        Else
            strOptEmail = ""
        End If
        If Me.cboOptCall.Text.Length > 0 Then
            strOptCall = "'" & Me.cboOptCall.Text.Substring(0, 1).Replace("'", "''") & "'"
        Else
            strOptCall = ""
        End If
        strRemark = Me.txtRemark.Text.Trim

        ' **** ES010 begin ****
        Dim strNPS As String = "N"
        If chkOptOutNPS.Checked = True Then
            strNPS = "Y"
        End If

        Dim ciwCustomerDatatable As New DataTable
        ciwCustomerDatatable = GetCIWCustomer(strCustID)
        Dim isUpdateCIWCustomer As Boolean = False
        If ciwCustomerDatatable IsNot Nothing AndAlso ciwCustomerDatatable.Rows.Count > 0 Then
            isUpdateCIWCustomer = UpdateCIWCustomer(strOptEmail, strOptCall, strRating, strRemark, strNPS, strCustID)
        Else
            isUpdateCIWCustomer = InsertCIWCustomer(strOptEmail, strOptCall, strRating, strRemark, strNPS, strCustID)
        End If

        If Not isUpdateCIWCustomer Then
            Exit Sub
        End If

        MsgBox("Data is saved.", MsgBoxStyle.Information, "Information")

        Try

            'Update DataTable for Cancel Button
            If ds.Tables("CIWCustomer").Rows.Count = 0 Then
                drData = ds.Tables("CIWCustomer").NewRow
                drData.Item("CustomerID") = strCustID
            Else
                drData = ds.Tables("CIWCustomer").Rows(0)
            End If
            If Me.cboOptEmail.Text.Length > 0 Then
                drData.Item("OptEmail") = Me.cboOptEmail.Text.Substring(0, 1)
            Else
                drData.Item("OptEmail") = DBNull.Value
            End If
            If Me.cboOptCall.Text.Length > 0 Then
                drData.Item("OptCall") = Me.cboOptCall.Text.Substring(0, 1)
            Else
                drData.Item("OptCall") = DBNull.Value
            End If
            If Me.txtRating.Text.Trim.Length > 0 Then
                drData.Item("Rating") = Me.txtRating.Text.Trim
            Else
                drData.Item("Rating") = DBNull.Value
            End If
            drData.Item("Remarks") = strRemark

            ' **** ES010 begin ****
            If Me.chkOptOutNPS.Checked = True Then
                drData.Item("cswdgm_optout_nps") = "Y"
            Else
                drData.Item("cswdgm_optout_nps") = "N"
            End If
            ' **** ES010 end ****

            ds.Tables("CIWCustomer").LoadDataRow(drData.ItemArray, True)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function UpdateCIWCustomer(ByVal strOptEmail As String, ByVal strOptCall As String, ByVal strRating As String, ByVal strRemark As String, ByVal strNPS As String, ByVal strCustID As String) As Boolean
        Dim isUpdate As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(), "UPDATE_CIW_CUSTOMER",
                                New Dictionary(Of String, String) From {
                                {"cswdgm_optout_email", strOptEmail},
                                {"cswdgm_optout_call", strOptCall},
                                {"cswdgm_rating", strRating},
                                {"cswdgm_remark", strRemark},
                                {"cswdgm_optout_NPS", strNPS},
                                {"cswdgm_cust_id", strCustID}
                                })
            isUpdate = True
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Update Error." & vbCrLf & ex.Message)
        End Try
        Return isUpdate
    End Function

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function InsertCIWCustomer(ByVal strOptEmail As String, ByVal strOptCall As String, ByVal strRating As String, ByVal strRemark As String, ByVal strNPS As String, ByVal strCustID As String) As Boolean
        Dim isInsert As Boolean = False
        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(), "INSERT_CIW_CUSTOMER",
                                New Dictionary(Of String, String) From {
                                {"cswdgm_optout_email", strOptEmail},
                                {"cswdgm_optout_call", strOptCall},
                                {"cswdgm_rating", strRating},
                                {"cswdgm_remark", strRemark},
                                {"cswdgm_optout_NPS", strNPS},
                                {"cswdgm_cust_id", strCustID}
                                })
            isInsert = True
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Insert Error." & vbCrLf & ex.Message)
        End Try
        Return isInsert
    End Function

    'oliver 2024-5-3 commented for Table_Relocate_Sprint13
    'Private Sub SaveOptFlags()
    '    Dim sqlConn As New SqlConnection
    '    Dim sqlCmd As SqlCommand
    '    Dim dtData As DataTable
    '    Dim drData As DataRow
    '    Dim strErrMsg As String

    '    Dim strRating As String
    '    Dim strOptEmail As String
    '    Dim strOptCall As String
    '    Dim strRemark As String

    '    Dim strSQL As String
    '    Dim strInsertSQL As String
    '    Dim strUpdSQL As String

    '    If Me.txtRating.Text.Trim.Length > 0 Then
    '        strRating = Me.txtRating.Text.Trim
    '    Else
    '        strRating = "Null"
    '    End If
    '    If Me.cboOptEmail.Text.Length > 0 Then
    '        strOptEmail = "'" & Me.cboOptEmail.Text.Substring(0, 1).Replace("'", "''") & "'"
    '    Else
    '        strOptEmail = "Null"
    '    End If
    '    If Me.cboOptCall.Text.Length > 0 Then
    '        strOptCall = "'" & Me.cboOptCall.Text.Substring(0, 1).Replace("'", "''") & "'"
    '    Else
    '        strOptCall = "Null"
    '    End If
    '    strRemark = Me.txtRemark.Text.Trim

    '    ' **** ES010 begin ****
    '    Dim strNPS As String = "N"
    '    If chkOptOutNPS.Checked = True Then
    '        strNPS = "Y"
    '    End If

    '    'strInsertSQL = "Insert Into csw_demographic(cswdgm_cust_id, cswdgm_optout_email, cswdgm_optout_call, cswdgm_rating, cswdgm_remark)"
    '    'strInsertSQL &= " Values('" & strCustID.Replace("'", "''") & "', " & strOptEmail & ", " & strOptCall & ","
    '    'strInsertSQL &= strRating & ", '" & strRemark.Replace("'", "''") & "')"

    '    'strUpdSQL = "Update csw_demographic Set cswdgm_optout_email = " & strOptEmail & ","
    '    'strUpdSQL &= " cswdgm_optout_call = " & strOptCall & ","
    '    'strUpdSQL &= " cswdgm_rating = " & strRating & ","
    '    'strUpdSQL &= " cswdgm_remark = '" & strRemark.Replace("'", "''") & "'"
    '    'strUpdSQL &= " Where cswdgm_cust_id = '" & strCustID.Replace("'", "''") & "'"
    '    'SQL2008
    '    strInsertSQL = "Insert Into csw_demographic(cswdgm_cust_id, cswdgm_optout_email, cswdgm_optout_call, cswdgm_rating, cswdgm_remark, cswdgm_optout_NPS)"
    '    strInsertSQL &= " Values('" & strCustID.Replace("'", "''") & "', " & strOptEmail & ", " & strOptCall & ","
    '    strInsertSQL &= strRating & ", N'" & strRemark.Replace("'", "''") & "','" & strNPS & "')"
    '    'SQL2008
    '    strUpdSQL = "Update csw_demographic Set cswdgm_optout_email = " & strOptEmail & ","
    '    strUpdSQL &= " cswdgm_optout_call = " & strOptCall & ","
    '    strUpdSQL &= " cswdgm_rating = " & strRating & ","
    '    strUpdSQL &= " cswdgm_remark = N'" & strRemark.Replace("'", "''") & "',"
    '    strUpdSQL &= " cswdgm_optout_NPS = '" & strNPS & "'"
    '    strUpdSQL &= " Where cswdgm_cust_id = '" & strCustID.Replace("'", "''") & "'"
    '    ' **** ES010 end ****

    '    strSQL = "If exists (Select cswdgm_cust_id From csw_demographic Where cswdgm_cust_id = '" & strCustID.Replace("'", "''") & "')"
    '    strSQL &= " " & strUpdSQL & " else " & strInsertSQL

    '    Try
    '        sqlConn.ConnectionString = strCIWConn
    '        sqlConn.Open()
    '        sqlCmd = sqlConn.CreateCommand
    '        sqlCmd.Connection = sqlConn

    '        'Insert/Update Campaign Tracking
    '        sqlCmd.CommandText = strSQL
    '        sqlCmd.ExecuteNonQuery()

    '        MsgBox("Data is saved.", MsgBoxStyle.Information, "Information")

    '        'Update DataTable for Cancel Button
    '        If ds.Tables("CIWCustomer").Rows.Count = 0 Then
    '            drData = ds.Tables("CIWCustomer").NewRow
    '            drData.Item("CustomerID") = strCustID
    '        Else
    '            drData = ds.Tables("CIWCustomer").Rows(0)
    '        End If
    '        If Me.cboOptEmail.Text.Length > 0 Then
    '            drData.Item("OptEmail") = Me.cboOptEmail.Text.Substring(0, 1)
    '        Else
    '            drData.Item("OptEmail") = DBNull.Value
    '        End If
    '        If Me.cboOptCall.Text.Length > 0 Then
    '            drData.Item("OptCall") = Me.cboOptCall.Text.Substring(0, 1)
    '        Else
    '            drData.Item("OptCall") = DBNull.Value
    '        End If
    '        If Me.txtRating.Text.Trim.Length > 0 Then
    '            drData.Item("Rating") = Me.txtRating.Text.Trim
    '        Else
    '            drData.Item("Rating") = DBNull.Value
    '        End If
    '        drData.Item("Remarks") = strRemark

    '        ' **** ES010 begin ****
    '        If Me.chkOptOutNPS.Checked = True Then
    '            drData.Item("cswdgm_optout_nps") = "Y"
    '        Else
    '            drData.Item("cswdgm_optout_nps") = "N"
    '        End If
    '        ' **** ES010 end ****

    '        ds.Tables("CIWCustomer").LoadDataRow(drData.ItemArray, True)

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        sqlConn.Close()
    '        sqlConn.Dispose()
    '        sqlCmd.Dispose()
    '    End Try

    'End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try
            If Validation() Then
                Call SaveOptFlags()
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try
            Call UpdatePT()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub lnkAgent_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAgent.LinkClicked
        If txtAgtCode.Text <> "" Then
            Dim f As New frmAgentInfo
            f.AgentCode = txtAgtCode.Text
            f.ShowDialog()
        End If
    End Sub
End Class
