Imports System.Data.SqlClient

Public Class FinancialInfo
    Inherits System.Windows.Forms.UserControl

    Private dt, dtMisc, dtPolVal() As DataTable
    Private strPolicy As String
    Friend WithEvents txtLevySusp As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtOSLevy As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Public isLA As Boolean = False
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Private objMQQueHeader As Utility.Utility.MQHeader      'MQQueHeader includes MQ conn. parameters
    Private objDBHeader As Utility.Utility.ComHeader         'DBHeader includes MSSQL conn. pararmeters
    

#Region "MQ Properties"
    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
        End Set
    End Property
#End Region
#Region " DBLogon Properties"
    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
#End Region


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
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
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
    Friend WithEvents txtSurrVal As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxLoan As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscFact As System.Windows.Forms.TextBox
    Friend WithEvents txtBasicCSV As System.Windows.Forms.TextBox
    Friend WithEvents txtCSVPUA As System.Windows.Forms.TextBox
    Friend WithEvents txtDivAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtDivInt As System.Windows.Forms.TextBox
    Friend WithEvents txtCoupAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtPremRef As System.Windows.Forms.TextBox
    Friend WithEvents txtLoanAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtCoupInt As System.Windows.Forms.TextBox
    Friend WithEvents txtAssetBuilder As System.Windows.Forms.TextBox
    Friend WithEvents txtLoanInt As System.Windows.Forms.TextBox
    Friend WithEvents txtPDFAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtPDFInt As System.Windows.Forms.TextBox
    Friend WithEvents txtAPLAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtAPLInt As System.Windows.Forms.TextBox
    Friend WithEvents txtPremSusp As System.Windows.Forms.TextBox
    Friend WithEvents txtDivOpt As System.Windows.Forms.TextBox
    Friend WithEvents txtMiscSusp As System.Windows.Forms.TextBox
    Friend WithEvents txtReinAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtCoupOpt As System.Windows.Forms.TextBox
    Friend WithEvents txtOSPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtAPLStartDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlDec As System.Windows.Forms.TextBox
    Friend WithEvents txtCurDec As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlAddDB As System.Windows.Forms.TextBox
    Friend WithEvents txtDisb As System.Windows.Forms.TextBox
    Friend WithEvents txtDVY As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtBaseLoan As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtDDUR As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtCurPD As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtSubSts As System.Windows.Forms.TextBox
    Friend WithEvents txtAnnSTD As System.Windows.Forms.TextBox
    Friend WithEvents txtAnnEND As System.Windows.Forms.TextBox
    Friend WithEvents txtLAnnP As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlAnnP As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtValDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdGO As System.Windows.Forms.Button
    Friend WithEvents txtLAnnPaidD As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtSurrVal = New System.Windows.Forms.TextBox
        Me.txtMaxLoan = New System.Windows.Forms.TextBox
        Me.txtDiscFact = New System.Windows.Forms.TextBox
        Me.txtBasicCSV = New System.Windows.Forms.TextBox
        Me.txtCSVPUA = New System.Windows.Forms.TextBox
        Me.txtDivAmt = New System.Windows.Forms.TextBox
        Me.txtDivInt = New System.Windows.Forms.TextBox
        Me.txtCoupAmt = New System.Windows.Forms.TextBox
        Me.txtPremRef = New System.Windows.Forms.TextBox
        Me.txtLoanAmt = New System.Windows.Forms.TextBox
        Me.txtCoupInt = New System.Windows.Forms.TextBox
        Me.txtAssetBuilder = New System.Windows.Forms.TextBox
        Me.txtLoanInt = New System.Windows.Forms.TextBox
        Me.txtPDFAmt = New System.Windows.Forms.TextBox
        Me.txtPDFInt = New System.Windows.Forms.TextBox
        Me.txtAPLAmt = New System.Windows.Forms.TextBox
        Me.txtAPLInt = New System.Windows.Forms.TextBox
        Me.txtPremSusp = New System.Windows.Forms.TextBox
        Me.txtDivOpt = New System.Windows.Forms.TextBox
        Me.txtMiscSusp = New System.Windows.Forms.TextBox
        Me.txtReinAmt = New System.Windows.Forms.TextBox
        Me.txtCoupOpt = New System.Windows.Forms.TextBox
        Me.txtOSPrem = New System.Windows.Forms.TextBox
        Me.txtAPLStartDate = New System.Windows.Forms.TextBox
        Me.txtTtlDec = New System.Windows.Forms.TextBox
        Me.txtCurDec = New System.Windows.Forms.TextBox
        Me.txtTtlAddDB = New System.Windows.Forms.TextBox
        Me.txtDisb = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
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
        Me.txtDVY = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtBaseLoan = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtDDUR = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtCurPD = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtSubSts = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtAnnSTD = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtAnnEND = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtLAnnPaidD = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.txtLAnnP = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtTtlAnnP = New System.Windows.Forms.TextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.txtValDate = New System.Windows.Forms.DateTimePicker
        Me.cmdGO = New System.Windows.Forms.Button
        Me.txtLevySusp = New System.Windows.Forms.TextBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.txtOSLevy = New System.Windows.Forms.TextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtSurrVal
        '
        Me.txtSurrVal.AcceptsReturn = True
        Me.txtSurrVal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurrVal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurrVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSurrVal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSurrVal.Location = New System.Drawing.Point(124, 36)
        Me.txtSurrVal.MaxLength = 0
        Me.txtSurrVal.Name = "txtSurrVal"
        Me.txtSurrVal.ReadOnly = True
        Me.txtSurrVal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurrVal.Size = New System.Drawing.Size(70, 20)
        Me.txtSurrVal.TabIndex = 83
        '
        'txtMaxLoan
        '
        Me.txtMaxLoan.AcceptsReturn = True
        Me.txtMaxLoan.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxLoan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxLoan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMaxLoan.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMaxLoan.Location = New System.Drawing.Point(316, 36)
        Me.txtMaxLoan.MaxLength = 0
        Me.txtMaxLoan.Name = "txtMaxLoan"
        Me.txtMaxLoan.ReadOnly = True
        Me.txtMaxLoan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxLoan.Size = New System.Drawing.Size(70, 20)
        Me.txtMaxLoan.TabIndex = 82
        '
        'txtDiscFact
        '
        Me.txtDiscFact.AcceptsReturn = True
        Me.txtDiscFact.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiscFact.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDiscFact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDiscFact.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDiscFact.Location = New System.Drawing.Point(480, 36)
        Me.txtDiscFact.MaxLength = 0
        Me.txtDiscFact.Name = "txtDiscFact"
        Me.txtDiscFact.ReadOnly = True
        Me.txtDiscFact.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDiscFact.Size = New System.Drawing.Size(70, 20)
        Me.txtDiscFact.TabIndex = 81
        '
        'txtBasicCSV
        '
        Me.txtBasicCSV.AcceptsReturn = True
        Me.txtBasicCSV.BackColor = System.Drawing.SystemColors.Window
        Me.txtBasicCSV.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBasicCSV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBasicCSV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBasicCSV.Location = New System.Drawing.Point(124, 60)
        Me.txtBasicCSV.MaxLength = 0
        Me.txtBasicCSV.Name = "txtBasicCSV"
        Me.txtBasicCSV.ReadOnly = True
        Me.txtBasicCSV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBasicCSV.Size = New System.Drawing.Size(70, 20)
        Me.txtBasicCSV.TabIndex = 80
        '
        'txtCSVPUA
        '
        Me.txtCSVPUA.AcceptsReturn = True
        Me.txtCSVPUA.BackColor = System.Drawing.SystemColors.Window
        Me.txtCSVPUA.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCSVPUA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCSVPUA.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCSVPUA.Location = New System.Drawing.Point(316, 60)
        Me.txtCSVPUA.MaxLength = 0
        Me.txtCSVPUA.Name = "txtCSVPUA"
        Me.txtCSVPUA.ReadOnly = True
        Me.txtCSVPUA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCSVPUA.Size = New System.Drawing.Size(70, 20)
        Me.txtCSVPUA.TabIndex = 79
        '
        'txtDivAmt
        '
        Me.txtDivAmt.AcceptsReturn = True
        Me.txtDivAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDivAmt.Location = New System.Drawing.Point(480, 60)
        Me.txtDivAmt.MaxLength = 0
        Me.txtDivAmt.Name = "txtDivAmt"
        Me.txtDivAmt.ReadOnly = True
        Me.txtDivAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivAmt.Size = New System.Drawing.Size(70, 20)
        Me.txtDivAmt.TabIndex = 78
        '
        'txtDivInt
        '
        Me.txtDivInt.AcceptsReturn = True
        Me.txtDivInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivInt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivInt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDivInt.Location = New System.Drawing.Point(644, 60)
        Me.txtDivInt.MaxLength = 0
        Me.txtDivInt.Name = "txtDivInt"
        Me.txtDivInt.ReadOnly = True
        Me.txtDivInt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivInt.Size = New System.Drawing.Size(70, 20)
        Me.txtDivInt.TabIndex = 77
        '
        'txtCoupAmt
        '
        Me.txtCoupAmt.AcceptsReturn = True
        Me.txtCoupAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtCoupAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCoupAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCoupAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCoupAmt.Location = New System.Drawing.Point(124, 84)
        Me.txtCoupAmt.MaxLength = 0
        Me.txtCoupAmt.Name = "txtCoupAmt"
        Me.txtCoupAmt.ReadOnly = True
        Me.txtCoupAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCoupAmt.Size = New System.Drawing.Size(70, 20)
        Me.txtCoupAmt.TabIndex = 76
        '
        'txtPremRef
        '
        Me.txtPremRef.AcceptsReturn = True
        Me.txtPremRef.BackColor = System.Drawing.SystemColors.Window
        Me.txtPremRef.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPremRef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPremRef.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPremRef.Location = New System.Drawing.Point(124, 108)
        Me.txtPremRef.MaxLength = 0
        Me.txtPremRef.Name = "txtPremRef"
        Me.txtPremRef.ReadOnly = True
        Me.txtPremRef.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPremRef.Size = New System.Drawing.Size(70, 20)
        Me.txtPremRef.TabIndex = 75
        '
        'txtLoanAmt
        '
        Me.txtLoanAmt.AcceptsReturn = True
        Me.txtLoanAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoanAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoanAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLoanAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLoanAmt.Location = New System.Drawing.Point(124, 132)
        Me.txtLoanAmt.MaxLength = 0
        Me.txtLoanAmt.Name = "txtLoanAmt"
        Me.txtLoanAmt.ReadOnly = True
        Me.txtLoanAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoanAmt.Size = New System.Drawing.Size(70, 20)
        Me.txtLoanAmt.TabIndex = 74
        '
        'txtCoupInt
        '
        Me.txtCoupInt.AcceptsReturn = True
        Me.txtCoupInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtCoupInt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCoupInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCoupInt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCoupInt.Location = New System.Drawing.Point(316, 84)
        Me.txtCoupInt.MaxLength = 0
        Me.txtCoupInt.Name = "txtCoupInt"
        Me.txtCoupInt.ReadOnly = True
        Me.txtCoupInt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCoupInt.Size = New System.Drawing.Size(70, 20)
        Me.txtCoupInt.TabIndex = 73
        '
        'txtAssetBuilder
        '
        Me.txtAssetBuilder.AcceptsReturn = True
        Me.txtAssetBuilder.BackColor = System.Drawing.SystemColors.Window
        Me.txtAssetBuilder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAssetBuilder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAssetBuilder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAssetBuilder.Location = New System.Drawing.Point(316, 108)
        Me.txtAssetBuilder.MaxLength = 0
        Me.txtAssetBuilder.Name = "txtAssetBuilder"
        Me.txtAssetBuilder.ReadOnly = True
        Me.txtAssetBuilder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAssetBuilder.Size = New System.Drawing.Size(70, 20)
        Me.txtAssetBuilder.TabIndex = 72
        '
        'txtLoanInt
        '
        Me.txtLoanInt.AcceptsReturn = True
        Me.txtLoanInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoanInt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoanInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLoanInt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLoanInt.Location = New System.Drawing.Point(316, 132)
        Me.txtLoanInt.MaxLength = 0
        Me.txtLoanInt.Name = "txtLoanInt"
        Me.txtLoanInt.ReadOnly = True
        Me.txtLoanInt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoanInt.Size = New System.Drawing.Size(70, 20)
        Me.txtLoanInt.TabIndex = 71
        '
        'txtPDFAmt
        '
        Me.txtPDFAmt.AcceptsReturn = True
        Me.txtPDFAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtPDFAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPDFAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPDFAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPDFAmt.Location = New System.Drawing.Point(480, 84)
        Me.txtPDFAmt.MaxLength = 0
        Me.txtPDFAmt.Name = "txtPDFAmt"
        Me.txtPDFAmt.ReadOnly = True
        Me.txtPDFAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPDFAmt.Size = New System.Drawing.Size(70, 20)
        Me.txtPDFAmt.TabIndex = 70
        '
        'txtPDFInt
        '
        Me.txtPDFInt.AcceptsReturn = True
        Me.txtPDFInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtPDFInt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPDFInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPDFInt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPDFInt.Location = New System.Drawing.Point(644, 84)
        Me.txtPDFInt.MaxLength = 0
        Me.txtPDFInt.Name = "txtPDFInt"
        Me.txtPDFInt.ReadOnly = True
        Me.txtPDFInt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPDFInt.Size = New System.Drawing.Size(70, 20)
        Me.txtPDFInt.TabIndex = 69
        '
        'txtAPLAmt
        '
        Me.txtAPLAmt.AcceptsReturn = True
        Me.txtAPLAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtAPLAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAPLAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAPLAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAPLAmt.Location = New System.Drawing.Point(480, 132)
        Me.txtAPLAmt.MaxLength = 0
        Me.txtAPLAmt.Name = "txtAPLAmt"
        Me.txtAPLAmt.ReadOnly = True
        Me.txtAPLAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAPLAmt.Size = New System.Drawing.Size(70, 20)
        Me.txtAPLAmt.TabIndex = 68
        '
        'txtAPLInt
        '
        Me.txtAPLInt.AcceptsReturn = True
        Me.txtAPLInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtAPLInt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAPLInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAPLInt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAPLInt.Location = New System.Drawing.Point(644, 132)
        Me.txtAPLInt.MaxLength = 0
        Me.txtAPLInt.Name = "txtAPLInt"
        Me.txtAPLInt.ReadOnly = True
        Me.txtAPLInt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAPLInt.Size = New System.Drawing.Size(70, 20)
        Me.txtAPLInt.TabIndex = 67
        '
        'txtPremSusp
        '
        Me.txtPremSusp.AcceptsReturn = True
        Me.txtPremSusp.BackColor = System.Drawing.SystemColors.Window
        Me.txtPremSusp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPremSusp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPremSusp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPremSusp.Location = New System.Drawing.Point(124, 180)
        Me.txtPremSusp.MaxLength = 0
        Me.txtPremSusp.Name = "txtPremSusp"
        Me.txtPremSusp.ReadOnly = True
        Me.txtPremSusp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPremSusp.Size = New System.Drawing.Size(70, 20)
        Me.txtPremSusp.TabIndex = 66
        '
        'txtDivOpt
        '
        Me.txtDivOpt.AcceptsReturn = True
        Me.txtDivOpt.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivOpt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivOpt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivOpt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDivOpt.Location = New System.Drawing.Point(124, 252)
        Me.txtDivOpt.MaxLength = 0
        Me.txtDivOpt.Name = "txtDivOpt"
        Me.txtDivOpt.ReadOnly = True
        Me.txtDivOpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivOpt.Size = New System.Drawing.Size(264, 20)
        Me.txtDivOpt.TabIndex = 65
        '
        'txtMiscSusp
        '
        Me.txtMiscSusp.AcceptsReturn = True
        Me.txtMiscSusp.BackColor = System.Drawing.SystemColors.Window
        Me.txtMiscSusp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMiscSusp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMiscSusp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMiscSusp.Location = New System.Drawing.Point(124, 204)
        Me.txtMiscSusp.MaxLength = 0
        Me.txtMiscSusp.Name = "txtMiscSusp"
        Me.txtMiscSusp.ReadOnly = True
        Me.txtMiscSusp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMiscSusp.Size = New System.Drawing.Size(70, 20)
        Me.txtMiscSusp.TabIndex = 64
        '
        'txtReinAmt
        '
        Me.txtReinAmt.AcceptsReturn = True
        Me.txtReinAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtReinAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReinAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtReinAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReinAmt.Location = New System.Drawing.Point(316, 204)
        Me.txtReinAmt.MaxLength = 0
        Me.txtReinAmt.Name = "txtReinAmt"
        Me.txtReinAmt.ReadOnly = True
        Me.txtReinAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReinAmt.Size = New System.Drawing.Size(70, 20)
        Me.txtReinAmt.TabIndex = 63
        '
        'txtCoupOpt
        '
        Me.txtCoupOpt.AcceptsReturn = True
        Me.txtCoupOpt.BackColor = System.Drawing.SystemColors.Window
        Me.txtCoupOpt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCoupOpt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCoupOpt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCoupOpt.Location = New System.Drawing.Point(480, 204)
        Me.txtCoupOpt.MaxLength = 0
        Me.txtCoupOpt.Name = "txtCoupOpt"
        Me.txtCoupOpt.ReadOnly = True
        Me.txtCoupOpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCoupOpt.Size = New System.Drawing.Size(70, 20)
        Me.txtCoupOpt.TabIndex = 62
        '
        'txtOSPrem
        '
        Me.txtOSPrem.AcceptsReturn = True
        Me.txtOSPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtOSPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOSPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOSPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOSPrem.Location = New System.Drawing.Point(124, 228)
        Me.txtOSPrem.MaxLength = 0
        Me.txtOSPrem.Name = "txtOSPrem"
        Me.txtOSPrem.ReadOnly = True
        Me.txtOSPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOSPrem.Size = New System.Drawing.Size(70, 20)
        Me.txtOSPrem.TabIndex = 61
        '
        'txtAPLStartDate
        '
        Me.txtAPLStartDate.AcceptsReturn = True
        Me.txtAPLStartDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAPLStartDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAPLStartDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAPLStartDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAPLStartDate.Location = New System.Drawing.Point(481, 228)
        Me.txtAPLStartDate.MaxLength = 0
        Me.txtAPLStartDate.Name = "txtAPLStartDate"
        Me.txtAPLStartDate.ReadOnly = True
        Me.txtAPLStartDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAPLStartDate.Size = New System.Drawing.Size(70, 20)
        Me.txtAPLStartDate.TabIndex = 60
        Me.txtAPLStartDate.Visible = False
        '
        'txtTtlDec
        '
        Me.txtTtlDec.AcceptsReturn = True
        Me.txtTtlDec.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlDec.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlDec.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlDec.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlDec.Location = New System.Drawing.Point(124, 276)
        Me.txtTtlDec.MaxLength = 0
        Me.txtTtlDec.Name = "txtTtlDec"
        Me.txtTtlDec.ReadOnly = True
        Me.txtTtlDec.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlDec.Size = New System.Drawing.Size(70, 20)
        Me.txtTtlDec.TabIndex = 59
        '
        'txtCurDec
        '
        Me.txtCurDec.AcceptsReturn = True
        Me.txtCurDec.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurDec.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurDec.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCurDec.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCurDec.Location = New System.Drawing.Point(316, 276)
        Me.txtCurDec.MaxLength = 0
        Me.txtCurDec.Name = "txtCurDec"
        Me.txtCurDec.ReadOnly = True
        Me.txtCurDec.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurDec.Size = New System.Drawing.Size(70, 20)
        Me.txtCurDec.TabIndex = 58
        '
        'txtTtlAddDB
        '
        Me.txtTtlAddDB.AcceptsReturn = True
        Me.txtTtlAddDB.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlAddDB.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlAddDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlAddDB.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlAddDB.Location = New System.Drawing.Point(480, 276)
        Me.txtTtlAddDB.MaxLength = 0
        Me.txtTtlAddDB.Name = "txtTtlAddDB"
        Me.txtTtlAddDB.ReadOnly = True
        Me.txtTtlAddDB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlAddDB.Size = New System.Drawing.Size(70, 20)
        Me.txtTtlAddDB.TabIndex = 57
        '
        'txtDisb
        '
        Me.txtDisb.AcceptsReturn = True
        Me.txtDisb.BackColor = System.Drawing.SystemColors.Window
        Me.txtDisb.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDisb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDisb.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDisb.Location = New System.Drawing.Point(644, 252)
        Me.txtDisb.MaxLength = 0
        Me.txtDisb.Name = "txtDisb"
        Me.txtDisb.ReadOnly = True
        Me.txtDisb.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDisb.Size = New System.Drawing.Size(70, 20)
        Me.txtDisb.TabIndex = 56
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(4, 40)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(83, 13)
        Me.Label28.TabIndex = 111
        Me.Label28.Text = "Surrender Value"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(200, 40)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(57, 13)
        Me.Label27.TabIndex = 110
        Me.Label27.Text = "Max. Loan"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(392, 40)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(64, 13)
        Me.Label26.TabIndex = 109
        Me.Label26.Text = "Disc. Factor"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(4, 64)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(57, 13)
        Me.Label25.TabIndex = 108
        Me.Label25.Text = "Basic CSV"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(200, 64)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(112, 13)
        Me.Label24.TabIndex = 107
        Me.Label24.Text = "CSV of PUA/RB/EBR"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(392, 64)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(73, 13)
        Me.Label23.TabIndex = 106
        Me.Label23.Text = "Dividend Amt."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(556, 64)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(87, 13)
        Me.Label22.TabIndex = 105
        Me.Label22.Text = "Dividend Interest"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(4, 88)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(68, 13)
        Me.Label21.TabIndex = 104
        Me.Label21.Text = "Coupon Amt."
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(200, 88)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(82, 13)
        Me.Label20.TabIndex = 103
        Me.Label20.Text = "Coupon Interest"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(392, 88)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(67, 13)
        Me.Label19.TabIndex = 102
        Me.Label19.Text = "PDF Amount"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(556, 88)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 101
        Me.Label18.Text = "PDF Interest"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(4, 112)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(85, 13)
        Me.Label17.TabIndex = 100
        Me.Label17.Text = "Premium Refund"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(200, 112)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(68, 13)
        Me.Label16.TabIndex = 99
        Me.Label16.Text = "Asset Builder"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(4, 136)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(70, 13)
        Me.Label15.TabIndex = 98
        Me.Label15.Text = "Loan Amount"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(200, 136)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(69, 13)
        Me.Label14.TabIndex = 97
        Me.Label14.Text = "Loan Interest"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(392, 136)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(66, 13)
        Me.Label13.TabIndex = 96
        Me.Label13.Text = "APL Amount"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(556, 136)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 95
        Me.Label12.Text = "APL Interest"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(4, 184)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(97, 13)
        Me.Label11.TabIndex = 94
        Me.Label11.Text = "Premium Suspense"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(4, 256)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(83, 13)
        Me.Label10.TabIndex = 93
        Me.Label10.Text = "Dividend Option"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 208)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 92
        Me.Label9.Text = "Misc. Suspense"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(200, 208)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(99, 13)
        Me.Label8.TabIndex = 91
        Me.Label8.Text = "Reinstatement Amt."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(398, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 90
        Me.Label7.Text = "Coupon Option"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(4, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 89
        Me.Label6.Text = "O/S Premium"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(396, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 88
        Me.Label5.Text = "APL Start Date"
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 280)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 87
        Me.Label4.Text = "Total Declare Value"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(200, 280)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(111, 13)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Current Declare Value"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(396, 280)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "Total Paid Up"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(556, 256)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 84
        Me.Label1.Text = "Disbursement"
        '
        'txtDVY
        '
        Me.txtDVY.AcceptsReturn = True
        Me.txtDVY.BackColor = System.Drawing.SystemColors.Window
        Me.txtDVY.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDVY.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDVY.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDVY.Location = New System.Drawing.Point(480, 252)
        Me.txtDVY.MaxLength = 0
        Me.txtDVY.Name = "txtDVY"
        Me.txtDVY.ReadOnly = True
        Me.txtDVY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDVY.Size = New System.Drawing.Size(70, 20)
        Me.txtDVY.TabIndex = 112
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(396, 256)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(74, 13)
        Me.Label29.TabIndex = 113
        Me.Label29.Text = "Dividend Year"
        '
        'txtBaseLoan
        '
        Me.txtBaseLoan.AcceptsReturn = True
        Me.txtBaseLoan.BackColor = System.Drawing.SystemColors.Window
        Me.txtBaseLoan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBaseLoan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBaseLoan.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBaseLoan.Location = New System.Drawing.Point(644, 36)
        Me.txtBaseLoan.MaxLength = 0
        Me.txtBaseLoan.Name = "txtBaseLoan"
        Me.txtBaseLoan.ReadOnly = True
        Me.txtBaseLoan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBaseLoan.Size = New System.Drawing.Size(70, 20)
        Me.txtBaseLoan.TabIndex = 114
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(556, 40)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(58, 13)
        Me.Label30.TabIndex = 115
        Me.Label30.Text = "Base Loan"
        '
        'txtDDUR
        '
        Me.txtDDUR.AcceptsReturn = True
        Me.txtDDUR.BackColor = System.Drawing.SystemColors.Window
        Me.txtDDUR.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDDUR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDDUR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDDUR.Location = New System.Drawing.Point(644, 204)
        Me.txtDDUR.MaxLength = 0
        Me.txtDDUR.Name = "txtDDUR"
        Me.txtDDUR.ReadOnly = True
        Me.txtDDUR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDDUR.Size = New System.Drawing.Size(70, 20)
        Me.txtDDUR.TabIndex = 116
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(556, 208)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(87, 13)
        Me.Label31.TabIndex = 117
        Me.Label31.Text = "Coupon Duration"
        '
        'txtCurPD
        '
        Me.txtCurPD.AcceptsReturn = True
        Me.txtCurPD.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurPD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurPD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCurPD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCurPD.Location = New System.Drawing.Point(644, 276)
        Me.txtCurPD.MaxLength = 0
        Me.txtCurPD.Name = "txtCurPD"
        Me.txtCurPD.ReadOnly = True
        Me.txtCurPD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurPD.Size = New System.Drawing.Size(70, 20)
        Me.txtCurPD.TabIndex = 118
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(556, 280)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(82, 13)
        Me.Label32.TabIndex = 119
        Me.Label32.Text = "Current Paid Up"
        '
        'txtSubSts
        '
        Me.txtSubSts.AcceptsReturn = True
        Me.txtSubSts.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubSts.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubSts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSubSts.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubSts.Location = New System.Drawing.Point(124, 316)
        Me.txtSubSts.MaxLength = 0
        Me.txtSubSts.Name = "txtSubSts"
        Me.txtSubSts.ReadOnly = True
        Me.txtSubSts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubSts.Size = New System.Drawing.Size(24, 20)
        Me.txtSubSts.TabIndex = 120
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(4, 320)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(59, 13)
        Me.Label33.TabIndex = 121
        Me.Label33.Text = "Sub-Status"
        '
        'txtAnnSTD
        '
        Me.txtAnnSTD.AcceptsReturn = True
        Me.txtAnnSTD.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnSTD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAnnSTD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAnnSTD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAnnSTD.Location = New System.Drawing.Point(344, 316)
        Me.txtAnnSTD.MaxLength = 0
        Me.txtAnnSTD.Name = "txtAnnSTD"
        Me.txtAnnSTD.ReadOnly = True
        Me.txtAnnSTD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnnSTD.Size = New System.Drawing.Size(70, 20)
        Me.txtAnnSTD.TabIndex = 122
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(216, 320)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(117, 13)
        Me.Label34.TabIndex = 123
        Me.Label34.Text = "Annuity Paid Start Date"
        '
        'txtAnnEND
        '
        Me.txtAnnEND.AcceptsReturn = True
        Me.txtAnnEND.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnEND.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAnnEND.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAnnEND.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAnnEND.Location = New System.Drawing.Point(548, 316)
        Me.txtAnnEND.MaxLength = 0
        Me.txtAnnEND.Name = "txtAnnEND"
        Me.txtAnnEND.ReadOnly = True
        Me.txtAnnEND.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnnEND.Size = New System.Drawing.Size(70, 20)
        Me.txtAnnEND.TabIndex = 124
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(424, 320)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(114, 13)
        Me.Label35.TabIndex = 125
        Me.Label35.Text = "Annuity Paid End Date"
        '
        'txtLAnnPaidD
        '
        Me.txtLAnnPaidD.AcceptsReturn = True
        Me.txtLAnnPaidD.BackColor = System.Drawing.SystemColors.Window
        Me.txtLAnnPaidD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLAnnPaidD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLAnnPaidD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLAnnPaidD.Location = New System.Drawing.Point(124, 340)
        Me.txtLAnnPaidD.MaxLength = 0
        Me.txtLAnnPaidD.Name = "txtLAnnPaidD"
        Me.txtLAnnPaidD.ReadOnly = True
        Me.txtLAnnPaidD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLAnnPaidD.Size = New System.Drawing.Size(70, 20)
        Me.txtLAnnPaidD.TabIndex = 126
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(4, 344)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(115, 13)
        Me.Label36.TabIndex = 127
        Me.Label36.Text = "Last Annuity Paid Date"
        '
        'txtLAnnP
        '
        Me.txtLAnnP.AcceptsReturn = True
        Me.txtLAnnP.BackColor = System.Drawing.SystemColors.Window
        Me.txtLAnnP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLAnnP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLAnnP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLAnnP.Location = New System.Drawing.Point(344, 340)
        Me.txtLAnnP.MaxLength = 0
        Me.txtLAnnP.Name = "txtLAnnP"
        Me.txtLAnnP.ReadOnly = True
        Me.txtLAnnP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLAnnP.Size = New System.Drawing.Size(70, 20)
        Me.txtLAnnP.TabIndex = 128
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(204, 344)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(128, 13)
        Me.Label37.TabIndex = 129
        Me.Label37.Text = "Last Annuity Paid Amount"
        '
        'txtTtlAnnP
        '
        Me.txtTtlAnnP.AcceptsReturn = True
        Me.txtTtlAnnP.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlAnnP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlAnnP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlAnnP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlAnnP.Location = New System.Drawing.Point(548, 340)
        Me.txtTtlAnnP.MaxLength = 0
        Me.txtTtlAnnP.Name = "txtTtlAnnP"
        Me.txtTtlAnnP.ReadOnly = True
        Me.txtTtlAnnP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlAnnP.Size = New System.Drawing.Size(70, 20)
        Me.txtTtlAnnP.TabIndex = 130
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(448, 344)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(93, 13)
        Me.Label38.TabIndex = 131
        Me.Label38.Text = "Total Annuity Paid"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(4, 12)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(30, 13)
        Me.Label39.TabIndex = 133
        Me.Label39.Text = "Date"
        '
        'txtValDate
        '
        Me.txtValDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtValDate.Location = New System.Drawing.Point(36, 8)
        Me.txtValDate.Name = "txtValDate"
        Me.txtValDate.Size = New System.Drawing.Size(132, 20)
        Me.txtValDate.TabIndex = 135
        '
        'cmdGO
        '
        Me.cmdGO.Location = New System.Drawing.Point(172, 8)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(37, 20)
        Me.cmdGO.TabIndex = 134
        Me.cmdGO.Text = "Go"
        '
        'txtLevySusp
        '
        Me.txtLevySusp.AcceptsReturn = True
        Me.txtLevySusp.BackColor = System.Drawing.SystemColors.Window
        Me.txtLevySusp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLevySusp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLevySusp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLevySusp.Location = New System.Drawing.Point(317, 179)
        Me.txtLevySusp.MaxLength = 0
        Me.txtLevySusp.Name = "txtLevySusp"
        Me.txtLevySusp.ReadOnly = True
        Me.txtLevySusp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLevySusp.Size = New System.Drawing.Size(70, 20)
        Me.txtLevySusp.TabIndex = 136
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(201, 183)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(80, 13)
        Me.Label40.TabIndex = 137
        Me.Label40.Text = "Levy Suspense"
        '
        'txtOSLevy
        '
        Me.txtOSLevy.AcceptsReturn = True
        Me.txtOSLevy.BackColor = System.Drawing.SystemColors.Window
        Me.txtOSLevy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOSLevy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOSLevy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOSLevy.Location = New System.Drawing.Point(316, 228)
        Me.txtOSLevy.MaxLength = 0
        Me.txtOSLevy.Name = "txtOSLevy"
        Me.txtOSLevy.ReadOnly = True
        Me.txtOSLevy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOSLevy.Size = New System.Drawing.Size(70, 20)
        Me.txtOSLevy.TabIndex = 138
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(203, 232)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(53, 13)
        Me.Label41.TabIndex = 139
        Me.Label41.Text = "O/S Levy"
        '
        'FinancialInfo
        '
        Me.Controls.Add(Me.txtOSLevy)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.txtLevySusp)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.txtValDate)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.txtTtlAnnP)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.txtLAnnP)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.txtLAnnPaidD)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.txtAnnEND)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.txtAnnSTD)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.txtSubSts)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.txtCurPD)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.txtDDUR)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.txtBaseLoan)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.txtDVY)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.txtSurrVal)
        Me.Controls.Add(Me.txtMaxLoan)
        Me.Controls.Add(Me.txtDiscFact)
        Me.Controls.Add(Me.txtBasicCSV)
        Me.Controls.Add(Me.txtCSVPUA)
        Me.Controls.Add(Me.txtDivAmt)
        Me.Controls.Add(Me.txtDivInt)
        Me.Controls.Add(Me.txtCoupAmt)
        Me.Controls.Add(Me.txtPremRef)
        Me.Controls.Add(Me.txtLoanAmt)
        Me.Controls.Add(Me.txtCoupInt)
        Me.Controls.Add(Me.txtAssetBuilder)
        Me.Controls.Add(Me.txtLoanInt)
        Me.Controls.Add(Me.txtPDFAmt)
        Me.Controls.Add(Me.txtPDFInt)
        Me.Controls.Add(Me.txtAPLAmt)
        Me.Controls.Add(Me.txtAPLInt)
        Me.Controls.Add(Me.txtPremSusp)
        Me.Controls.Add(Me.txtDivOpt)
        Me.Controls.Add(Me.txtMiscSusp)
        Me.Controls.Add(Me.txtReinAmt)
        Me.Controls.Add(Me.txtCoupOpt)
        Me.Controls.Add(Me.txtOSPrem)
        Me.Controls.Add(Me.txtAPLStartDate)
        Me.Controls.Add(Me.txtTtlDec)
        Me.Controls.Add(Me.txtCurDec)
        Me.Controls.Add(Me.txtTtlAddDB)
        Me.Controls.Add(Me.txtDisb)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
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
        Me.Name = "FinancialInfo"
        Me.Size = New System.Drawing.Size(724, 368)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Property PolicyAccountID(ByVal dtPolSum As DataTable, ByVal dtPolMisc As DataTable)
        Get
            PolicyAccountID = strPolicy
        End Get
        Set(ByVal Value)
            If Not Value Is Nothing Then
                dt = dtPolSum
                dtMisc = dtPolMisc
                strPolicy = Value
                Call buildUI()
            End If
        End Set
    End Property

    Private Sub buildUI()

        Dim lngErr As Long
        Dim strErr As String

        wndMain.Cursor = Cursors.WaitCursor

        ' ES007 begin
        If isLA = True Then
            Dim dtVal As DataTable
            ' oliver 2023-12-15 updated for Switch Over Code from Assurance to Bermuda
            'dtVal = GetLAPolicyValue(strPolicy, "P", 0, strErr, txtValDate.Value)
            dtVal = GetLAPolicyValue(g_Comp, strPolicy, "P", 0, strErr, txtValDate.Value)

            If strErr = "" AndAlso dtVal IsNot Nothing AndAlso dtVal.Rows.Count > 0 Then
                With dtVal.Rows(0)
                    Me.txtSurrVal.Text = .Item("TotalSurVal")
                    Me.txtBasicCSV.Text = .Item("BaseCashValue")
                    Me.txtCSVPUA.Text = .Item("PUACashValue")
                    Me.txtCoupAmt.Text = .Item("Coupon")
                    Me.txtPremRef.Text = .Item("PremRefund")
                    Me.txtLoanAmt.Text = .Item("Loan")
                    Me.txtCoupInt.Text = .Item("CouponInt")
                    Me.txtLoanInt.Text = .Item("LoanInt")
                    Me.txtPDFInt.Text = .Item("PDFInt")
                    Me.txtAPLInt.Text = .Item("APLInt")
                    Me.txtDivInt.Text = .Item("DivDepositInt")

                    If Not IsDBNull(.Item("PremSuspense")) Then Me.txtPremSusp.Text = .Item("PremSuspense")
                    If Not IsDBNull(.Item("MiscSuspense")) Then Me.txtMiscSusp.Text = .Item("MiscSuspense")
                    If Not IsDBNull(.Item("DivOpt")) Then Me.txtDivOpt.Text = GetDivOption(.Item("DivOpt"))
                    If Not IsDBNull(.Item("CouponOpt")) Then Me.txtCoupOpt.Text = GetCouponOption(.Item("CouponOpt"))
                    If Not IsDBNull(.Item("DivYear")) Then
                        txtDVY.Text = .Item("DivYear")
                    End If
                    If Not IsDBNull(.Item("CurrentPaidUp")) Then
                        txtCurPD.Text = .Item("CurrentPaidUp")
                    End If
                    Me.txtAPLAmt.Text = .Item("APL")
                    Me.txtPDFAmt.Text = .Item("PDFAmount")
                    Me.txtDivAmt.Text = .Item("DivOnDeposit")

                    ' No Mapping
                    ''Me.txtMaxLoan.Text = Format(.Item("MaxLoan"), gNumFormat)
                    ''Me.txtDiscFact.Text = Format(.Item("DiscFactor"), gNumFormat)
                    ''Me.txtReinAmt.Text = Format(.Item("ReinstateAmount"), gNumFormat)
                    ''Me.txtOSPrem.Text = Format(.Item("OutstandPrem"), gNumFormat)
                    ''Me.txtBaseLoan.Text = Format(.Item("BaseLoan"), gNumFormat)
                    ''If Not IsDBNull(.Item("TotalDeclareValue")) Then Me.txtTtlDec.Text = Format(.Item("TotalDeclareValue"), gNumFormat)
                    ''If Not IsDBNull(.Item("CurrDeclVal")) Then Me.txtCurDec.Text = Format(.Item("CurrDeclVal"), gNumFormat)
                    ''If Not IsDBNull(.Item("PODVPU")) Then Me.txtTtlAddDB.Text = Format(.Item("PODVPU"), gNumFormat)
                    ''If Not IsDBNull(.Item("Disbursement")) Then Me.txtDisb.Text = Format(.Item("Disbursement"), gNumFormat)
                    ''If Not IsDBNull(.Item("POCPDR")) Then
                    ''    txtDDUR.Text = Format(.Item("POCPDR"), gNumFormat)
                    ''End If

                    '20171205 Levy
                    SetLevyColumns()

                End With
            End If

        End If
        ' ES007 end

        If isLA = False Then

            dtPolVal = objCS.GetPolicyVal(strPolicy, txtValDate.Value, lngErr, strErr)

            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Else
                If Not dtPolVal Is Nothing Then
                    If dtPolVal(2).Rows.Count > 0 Then
                        With dtPolVal(2).Rows(0)
                            ''Me.txtSurrVal.Text = Format(.Item(4), gNumFormat)
                            ''Me.txtMaxLoan.Text = Format(.Item(16), gNumFormat)
                            ''Me.txtDiscFact.Text = Format(.Item(19), gNumFormat)
                            ''Me.txtBasicCSV.Text = Format(.Item(5), gNumFormat)
                            ''Me.txtCSVPUA.Text = Format(.Item(6), gNumFormat)
                            ''Me.txtDivAmt.Text = Format(.Item(7), gNumFormat)
                            ''Me.txtDivInt.Text = Format(.Item(8), gNumFormat)
                            ''Me.txtCoupAmt.Text = Format(.Item(22) - .Item(23), gNumFormat)
                            ''Me.txtPremRef.Text = Format(.Item(11), gNumFormat)
                            ''Me.txtLoanAmt.Text = Format(.Item(12), gNumFormat)
                            ''Me.txtCoupInt.Text = Format(.Item(23), gNumFormat)
                            ''Me.txtLoanInt.Text = Format(.Item(13), gNumFormat)
                            ''Me.txtPDFAmt.Text = Format(.Item(9), gNumFormat)
                            ''Me.txtPDFInt.Text = Format(.Item(10), gNumFormat)
                            ''Me.txtAPLAmt.Text = Format(.Item(14), gNumFormat)
                            ''Me.txtAPLInt.Text = Format(.Item(15), gNumFormat)
                            ''Me.txtReinAmt.Text = Format(.Item(26), gNumFormat)
                            ''Me.txtOSPrem.Text = Format(.Item(25), gNumFormat)
                            ''Me.txtBaseLoan.Text = Format(.Item(18), gNumFormat)

                            ' **** ES009 begin ****
                            Dim ClaimAmt As Double = 0
                            ClaimAmt = MSClaimAmount(strPolicy, txtValDate.Value)
                            If IsDBNull(ClaimAmt) Then ClaimAmt = 0
                            'Me.txtSurrVal.Text = Format(.Item("CashValue"), gNumFormat)
                            Me.txtSurrVal.Text = Format(.Item("CashValue") - ClaimAmt, gNumFormat)
                            ' **** ES009 end ****

                            Me.txtMaxLoan.Text = Format(.Item("MaxLoan"), gNumFormat)
                            Me.txtDiscFact.Text = Format(.Item("DiscFactor"), gNumFormat)
                            Me.txtBasicCSV.Text = Format(.Item("BaseCashValue"), gNumFormat)
                            Me.txtCSVPUA.Text = Format(.Item("ValueofPUA"), gNumFormat)
                            Me.txtCoupAmt.Text = Format(.Item("CouponDep"), gNumFormat)
                            Me.txtPremRef.Text = Format(.Item("PremiumRefund"), gNumFormat)
                            Me.txtLoanAmt.Text = Format(.Item("LoanAmount"), gNumFormat)
                            Me.txtCoupInt.Text = Format(.Item("CouponInt"), gNumFormat)
                            Me.txtLoanInt.Text = Format(.Item("LoanInt"), gNumFormat)
                            Me.txtPDFInt.Text = Format(.Item("PDFINF"), gNumFormat)
                            Me.txtAPLInt.Text = Format(.Item("APLInt"), gNumFormat)
                            Me.txtReinAmt.Text = Format(.Item("ReinstateAmount"), gNumFormat)
                            Me.txtOSPrem.Text = Format(.Item("OutstandPrem"), gNumFormat)
                            Me.txtBaseLoan.Text = Format(.Item("BaseLoan"), gNumFormat)
                            Me.txtDivInt.Text = Format(.Item("DepositINT"), gNumFormat)

                            '20171205 Levy
                            SetLevyColumns()



                        End With
                    End If
                End If



            End If

            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    If Not IsDBNull(.Item("PremiumSuspense")) Then Me.txtPremSusp.Text = Format(.Item("PremiumSuspense"), gNumFormat)
                    If Not IsDBNull(.Item("MiscSuspense")) Then Me.txtMiscSusp.Text = Format(.Item("MiscSuspense"), gNumFormat)
                    ''If Not IsDBNull(.Item("APLStartDate")) Then Me.txtAPLStartDate.Text = Format(.Item("APLStartDate"), gDateFormat)
                    If Not IsDBNull(.Item("TotalDeclareValue")) Then Me.txtTtlDec.Text = Format(.Item("TotalDeclareValue"), gNumFormat)
                    If Not IsDBNull(.Item("CurrDeclVal")) Then Me.txtCurDec.Text = Format(.Item("CurrDeclVal"), gNumFormat)
                    ''If Not IsDBNull(.Item("AdditionDeathCvr")) Then Me.txtTtlAddDB.Text = Format(.Item("AdditionDeathCvr"), gNumFormat)
                    If Not IsDBNull(.Item("PODVPU")) Then Me.txtTtlAddDB.Text = Format(.Item("PODVPU"), gNumFormat)
                    If Not IsDBNull(.Item("Disbursement")) Then Me.txtDisb.Text = Format(.Item("Disbursement"), gNumFormat)
                    Me.txtAssetBuilder.Text = Format(0, gNumFormat)
                    If Not IsDBNull(.Item("POFR15")) Then
                        If Mid(.Item("ProductID"), 2, 2) = "AB" Then
                            Me.txtAssetBuilder.Text = Format(.Item("POFR15"), gNumFormat)
                        End If
                    End If
                    If Not IsDBNull(.Item("DividendOption")) Then Me.txtDivOpt.Text = GetDivOption(.Item("DividendOption"))
                    If Not IsDBNull(.Item("CouponOption")) Then Me.txtCoupOpt.Text = GetCouponOption(.Item("CouponOption"))
                    If Not IsDBNull(.Item("PODVDR")) Then
                        'txtDVY.Text = .Item("PODVDR") + .Item("POIYY") + 1800
                        txtDVY.Text = .Item("PODVDR") + 1800
                    End If
                    If Not IsDBNull(.Item("POCPDR")) Then
                        txtDDUR.Text = Format(.Item("POCPDR"), gNumFormat)
                    End If
                    If Not IsDBNull(.Item("PODVCP")) Then
                        txtCurPD.Text = Format(.Item("PODVCP"), gNumFormat)
                    End If
                    Me.txtDivAmt.Text = Format(.Item("AccumulatedDividend"), gNumFormat)
                    'Me.txtDivInt.Text = Format(.Item("DividendInterest"), gNumFormat)
                    Me.txtAPLAmt.Text = Format(.Item("LoanAmt3"), gNumFormat)
                    Me.txtPDFAmt.Text = Format(.Item("PDFBalance"), gNumFormat)

                    'Dim strProductID As String
                    'strProductID = .Item("ProductID")
                    'If strProductID <> "UYC  U" And strProductID <> "HYC  U" And strProductID <> "UDC  U" And _
                    '        strProductID <> "HDC  U" And strProductID <> "UGC  U" And strProductID <> "HGC  U" And _
                    '        strProductID <> "UIL  U" And strProductID <> "HIL  U" And strProductID <> "HSP3M1" And _
                    '        strProductID <> "HIUL U" And strProductID <> "UIUL U" Then

                    '    'suppress surrender value for moneysaver
                    '    If Strings.Left(strProductID, 4) = "HM05" Or Strings.Left(strProductID, 4) = "UM05" Then
                    '        Me.txtSurrVal.Text = "N/A"
                    '    End If
                    'Else
                    '    Me.txtSurrVal.Text = "N/A"
                    '    Me.txtMaxLoan.Text = "N/A"
                    '    Me.txtDivAmt.Text = "N/A"
                    'End If

                    ' Add IE20 new fields
                    If blnIE20 Then
                        txtSubSts.Text = .Item("SubStatus")
                        If txtSubSts.Text = "A" Then
                            Me.txtAnnSTD.Text = Format(.Item("AnnPaidStDate"), gDateFormat)
                            Me.txtAnnEND.Text = Format(.Item("AnnPaidEdDate"), gDateFormat)
                            Me.txtLAnnPaidD.Text = Format(.Item("LAnnPaidDate"), gDateFormat)
                            Me.txtLAnnP.Text = Format(.Item("LAnnPaidAmt"), gNumFormat)
                            Me.txtTtlAnnP.Text = Format(.Item("TtlAnnPaid"), gNumFormat)
                        End If
                    End If
                    ' End Add
                    '20171205 Levy
                    SetLevyColumns()

                End With
            End If

           
        End If

        wndMain.Cursor = Cursors.Default

    End Sub

    Private Sub SetLevyColumns()

        '20171205
        Dim levyOutstanding As Double = 0
        Dim levySuspense As Double = 0
        Dim strErrGetLevyAmountSuspense As String = ""
        Dim strErrGetLevyAmountOutstanding As String = ""

        clsPOS.MQQueuesHeader = Me.objMQQueHeader
        clsPOS.DBHeader = Me.objDBHeader
        clsPOS.CiwHeader = Me.objDBHeader

        clsPOS.GetLevyAmountSuspense(strPolicy, levySuspense, strErrGetLevyAmountSuspense)
        clsPOS.GetLevyAmountOutstanding(strPolicy, levyOutstanding, strErrGetLevyAmountOutstanding)

        Me.txtOSLevy.Text = levyOutstanding.ToString("0.00")
        Me.txtLevySusp.Text = levySuspense.ToString("0.00")

    End Sub

    Private Sub FinancialInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtValDate.Value = Today
    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click
        Call buildUI()
    End Sub

    ' **** ES009 Begin ****
    Private Function MSClaimAmount(ByVal PolicyAccID As String, ByVal EffDate As Date, _
        Optional ByVal objDBS_400 As Object = Nothing) As Double

        Dim strSql As String
        Dim rstTemp As ADODB.Recordset
        Dim curAmt As Double
        Dim ModeFactor As Integer
        Dim PaidToDate As Date
        Dim sDate As String
        Dim blnNewCon As Boolean
        Dim intDate As Long

        MSClaimAmount = 0

        objDBS_400 = CreateObject("Dbsecurity.Database")
        objDBS_400.Connect(gsUser, "POS", "CPSDB")

        'intDate = (Year(EffDate) - 1800) * 10000 + Month(EffDate) * 100 + Day(EffDate)
        strSql = "SELECT SUM(W1AMT) as W1AMT FROM EAADTA.pos159w1 WHERE W1POLN ='" & PolicyAccID & "' and W1DATH < " & intDate

        rstTemp = objDBS_400.ExecuteStatement(strSql)

        If Not rstTemp.EOF Then
            rstTemp.MoveFirst()
        End If
        Do While Not rstTemp.EOF
            If Not IsDBNull(rstTemp.Fields(0).Value) Then
                curAmt = curAmt + rstTemp.Fields(0).Value
            End If
            rstTemp.MoveNext()
        Loop

        If blnNewCon Then
            objDBS_400 = Nothing
        End If
        rstTemp.Close()
        rstTemp = Nothing
        MSClaimAmount = curAmt

    End Function
    ' **** ES009 end ****

End Class
