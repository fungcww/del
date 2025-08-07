Imports System.Data.SqlClient

Public Class CoverageMcu
    Inherits System.Windows.Forms.UserControl
    'oliver 2024-7-5 added comment for Table_Relocate_Sprint 14,It will not call if life asia policy

#Region " Windows Form Designer generated code "

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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents grdInsured As System.Windows.Forms.DataGrid
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents grdCov As System.Windows.Forms.DataGrid
    Friend WithEvents txtExhibitDt As System.Windows.Forms.TextBox
    Friend WithEvents txtPCDt As System.Windows.Forms.TextBox
    Friend WithEvents txtExpDt As System.Windows.Forms.TextBox
    Friend WithEvents txtIssDt As System.Windows.Forms.TextBox
    Friend WithEvents txtFlatExtra As System.Windows.Forms.TextBox
    Friend WithEvents txtRateScale As System.Windows.Forms.TextBox
    Friend WithEvents txtSumInsured As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtMultiple As System.Windows.Forms.TextBox
    Friend WithEvents txtCov As System.Windows.Forms.TextBox
    Friend WithEvents txtParticipate As System.Windows.Forms.TextBox
    Friend WithEvents txtModalPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtChiName As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtSex As System.Windows.Forms.TextBox
    Friend WithEvents txtDOB As System.Windows.Forms.TextBox
    Friend WithEvents txtAAge As System.Windows.Forms.TextBox
    Friend WithEvents txtIAge As System.Windows.Forms.TextBox
    Friend WithEvents txtCFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtCTo As System.Windows.Forms.TextBox
    Friend WithEvents txtDrvExp As System.Windows.Forms.TextBox
    Friend WithEvents txtOccupCls As System.Windows.Forms.TextBox
    Friend WithEvents chkSmoker As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOL As System.Windows.Forms.CheckBox
    Friend WithEvents chkIND As System.Windows.Forms.CheckBox
    Friend WithEvents chkGIO As System.Windows.Forms.CheckBox
    Friend WithEvents chkADD As System.Windows.Forms.CheckBox
    Friend WithEvents chkWOP As System.Windows.Forms.CheckBox
    Friend WithEvents chkWPP As System.Windows.Forms.CheckBox
    Friend WithEvents txtWPPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtADDPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtWOPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtCOLPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtWPPSI As System.Windows.Forms.TextBox
    Friend WithEvents txtINDPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtADDSI As System.Windows.Forms.TextBox
    Friend WithEvents txtWOPSI As System.Windows.Forms.TextBox
    Friend WithEvents txtGIOPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtCOLSI As System.Windows.Forms.TextBox
    Friend WithEvents txtINDSI As System.Windows.Forms.TextBox
    Friend WithEvents txtGIOSI As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtFEDur As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMRDur As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMRFactor As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtADM As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtWPM As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAnnlPrem As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPolFee As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtADPrem As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtWPPrem1 As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtFRR As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMRR As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtFaceAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtCCI As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents grdPOLIA As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdCov = New System.Windows.Forms.DataGrid
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.txtExhibitDt = New System.Windows.Forms.TextBox
        Me.txtPCDt = New System.Windows.Forms.TextBox
        Me.txtExpDt = New System.Windows.Forms.TextBox
        Me.txtIssDt = New System.Windows.Forms.TextBox
        Me.txtFlatExtra = New System.Windows.Forms.TextBox
        Me.txtRateScale = New System.Windows.Forms.TextBox
        Me.txtSumInsured = New System.Windows.Forms.TextBox
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtMultiple = New System.Windows.Forms.TextBox
        Me.txtParticipate = New System.Windows.Forms.TextBox
        Me.txtModalPrem = New System.Windows.Forms.TextBox
        Me.txtCov = New System.Windows.Forms.TextBox
        Me.chkCOL = New System.Windows.Forms.CheckBox
        Me.chkIND = New System.Windows.Forms.CheckBox
        Me.chkGIO = New System.Windows.Forms.CheckBox
        Me.chkADD = New System.Windows.Forms.CheckBox
        Me.chkWOP = New System.Windows.Forms.CheckBox
        Me.chkWPP = New System.Windows.Forms.CheckBox
        Me.txtWPPrem = New System.Windows.Forms.TextBox
        Me.txtADDPrem = New System.Windows.Forms.TextBox
        Me.txtWOPrem = New System.Windows.Forms.TextBox
        Me.txtCOLPrem = New System.Windows.Forms.TextBox
        Me.txtWPPSI = New System.Windows.Forms.TextBox
        Me.txtINDPrem = New System.Windows.Forms.TextBox
        Me.txtADDSI = New System.Windows.Forms.TextBox
        Me.txtWOPSI = New System.Windows.Forms.TextBox
        Me.txtGIOPrem = New System.Windows.Forms.TextBox
        Me.txtCOLSI = New System.Windows.Forms.TextBox
        Me.txtINDSI = New System.Windows.Forms.TextBox
        Me.txtGIOSI = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.grdInsured = New System.Windows.Forms.DataGrid
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.txtChiName = New System.Windows.Forms.TextBox
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtSex = New System.Windows.Forms.TextBox
        Me.txtDOB = New System.Windows.Forms.TextBox
        Me.txtAAge = New System.Windows.Forms.TextBox
        Me.txtIAge = New System.Windows.Forms.TextBox
        Me.txtCFrom = New System.Windows.Forms.TextBox
        Me.txtCTo = New System.Windows.Forms.TextBox
        Me.txtDrvExp = New System.Windows.Forms.TextBox
        Me.txtOccupCls = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.chkSmoker = New System.Windows.Forms.CheckBox
        Me.txtFEDur = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMRDur = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtMRFactor = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtADM = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtWPM = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtAnnlPrem = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtPolFee = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtADPrem = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtWPPrem1 = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtFRR = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtMRR = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtFaceAmt = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtCCI = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.grdPOLIA = New System.Windows.Forms.DataGrid
        CType(Me.grdCov, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInsured, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPOLIA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdCov
        '
        Me.grdCov.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCov.BackColor = System.Drawing.Color.White
        Me.grdCov.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCov.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCov.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCov.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCov.CaptionVisible = False
        Me.grdCov.DataMember = ""
        Me.grdCov.FlatMode = True
        Me.grdCov.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdCov.ForeColor = System.Drawing.Color.Black
        Me.grdCov.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCov.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCov.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCov.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCov.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCov.Location = New System.Drawing.Point(4, 4)
        Me.grdCov.Name = "grdCov"
        Me.grdCov.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCov.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCov.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCov.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCov.Size = New System.Drawing.Size(656, 116)
        Me.grdCov.TabIndex = 0
        '
        'txtDesc
        '
        Me.txtDesc.AcceptsReturn = True
        Me.txtDesc.AutoSize = False
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesc.Location = New System.Drawing.Point(88, 152)
        Me.txtDesc.MaxLength = 0
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesc.Size = New System.Drawing.Size(252, 44)
        Me.txtDesc.TabIndex = 106
        Me.txtDesc.Text = ""
        Me.txtDesc.WordWrap = False
        '
        'txtExhibitDt
        '
        Me.txtExhibitDt.AcceptsReturn = True
        Me.txtExhibitDt.AutoSize = False
        Me.txtExhibitDt.BackColor = System.Drawing.SystemColors.Window
        Me.txtExhibitDt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExhibitDt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtExhibitDt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExhibitDt.Location = New System.Drawing.Point(632, 152)
        Me.txtExhibitDt.MaxLength = 0
        Me.txtExhibitDt.Name = "txtExhibitDt"
        Me.txtExhibitDt.ReadOnly = True
        Me.txtExhibitDt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExhibitDt.Size = New System.Drawing.Size(80, 20)
        Me.txtExhibitDt.TabIndex = 105
        Me.txtExhibitDt.Text = ""
        '
        'txtPCDt
        '
        Me.txtPCDt.AcceptsReturn = True
        Me.txtPCDt.AutoSize = False
        Me.txtPCDt.BackColor = System.Drawing.SystemColors.Window
        Me.txtPCDt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPCDt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPCDt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPCDt.Location = New System.Drawing.Point(632, 200)
        Me.txtPCDt.MaxLength = 0
        Me.txtPCDt.Name = "txtPCDt"
        Me.txtPCDt.ReadOnly = True
        Me.txtPCDt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPCDt.Size = New System.Drawing.Size(80, 20)
        Me.txtPCDt.TabIndex = 104
        Me.txtPCDt.Text = ""
        '
        'txtExpDt
        '
        Me.txtExpDt.AcceptsReturn = True
        Me.txtExpDt.AutoSize = False
        Me.txtExpDt.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpDt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpDt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtExpDt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpDt.Location = New System.Drawing.Point(632, 224)
        Me.txtExpDt.MaxLength = 0
        Me.txtExpDt.Name = "txtExpDt"
        Me.txtExpDt.ReadOnly = True
        Me.txtExpDt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpDt.Size = New System.Drawing.Size(80, 20)
        Me.txtExpDt.TabIndex = 103
        Me.txtExpDt.Text = ""
        '
        'txtIssDt
        '
        Me.txtIssDt.AcceptsReturn = True
        Me.txtIssDt.AutoSize = False
        Me.txtIssDt.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssDt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIssDt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtIssDt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIssDt.Location = New System.Drawing.Point(632, 176)
        Me.txtIssDt.MaxLength = 0
        Me.txtIssDt.Name = "txtIssDt"
        Me.txtIssDt.ReadOnly = True
        Me.txtIssDt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIssDt.Size = New System.Drawing.Size(80, 20)
        Me.txtIssDt.TabIndex = 102
        Me.txtIssDt.Text = ""
        '
        'txtFlatExtra
        '
        Me.txtFlatExtra.AcceptsReturn = True
        Me.txtFlatExtra.AutoSize = False
        Me.txtFlatExtra.BackColor = System.Drawing.SystemColors.Window
        Me.txtFlatExtra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFlatExtra.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFlatExtra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFlatExtra.Location = New System.Drawing.Point(88, 224)
        Me.txtFlatExtra.MaxLength = 0
        Me.txtFlatExtra.Name = "txtFlatExtra"
        Me.txtFlatExtra.ReadOnly = True
        Me.txtFlatExtra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFlatExtra.Size = New System.Drawing.Size(72, 20)
        Me.txtFlatExtra.TabIndex = 101
        Me.txtFlatExtra.Text = ""
        '
        'txtRateScale
        '
        Me.txtRateScale.AcceptsReturn = True
        Me.txtRateScale.AutoSize = False
        Me.txtRateScale.BackColor = System.Drawing.SystemColors.Window
        Me.txtRateScale.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRateScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRateScale.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRateScale.Location = New System.Drawing.Point(412, 200)
        Me.txtRateScale.MaxLength = 0
        Me.txtRateScale.Name = "txtRateScale"
        Me.txtRateScale.ReadOnly = True
        Me.txtRateScale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRateScale.Size = New System.Drawing.Size(32, 20)
        Me.txtRateScale.TabIndex = 100
        Me.txtRateScale.Text = ""
        '
        'txtSumInsured
        '
        Me.txtSumInsured.AcceptsReturn = True
        Me.txtSumInsured.AutoSize = False
        Me.txtSumInsured.BackColor = System.Drawing.SystemColors.Window
        Me.txtSumInsured.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSumInsured.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSumInsured.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSumInsured.Location = New System.Drawing.Point(412, 152)
        Me.txtSumInsured.MaxLength = 0
        Me.txtSumInsured.Name = "txtSumInsured"
        Me.txtSumInsured.ReadOnly = True
        Me.txtSumInsured.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSumInsured.TabIndex = 99
        Me.txtSumInsured.Text = ""
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.AutoSize = False
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStatus.Location = New System.Drawing.Point(412, 128)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(152, 20)
        Me.txtStatus.TabIndex = 98
        Me.txtStatus.Text = ""
        '
        'txtMultiple
        '
        Me.txtMultiple.AcceptsReturn = True
        Me.txtMultiple.AutoSize = False
        Me.txtMultiple.BackColor = System.Drawing.SystemColors.Window
        Me.txtMultiple.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMultiple.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMultiple.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMultiple.Location = New System.Drawing.Point(88, 248)
        Me.txtMultiple.MaxLength = 0
        Me.txtMultiple.Name = "txtMultiple"
        Me.txtMultiple.ReadOnly = True
        Me.txtMultiple.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMultiple.Size = New System.Drawing.Size(72, 20)
        Me.txtMultiple.TabIndex = 97
        Me.txtMultiple.Text = ""
        '
        'txtParticipate
        '
        Me.txtParticipate.AcceptsReturn = True
        Me.txtParticipate.AutoSize = False
        Me.txtParticipate.BackColor = System.Drawing.SystemColors.Window
        Me.txtParticipate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtParticipate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtParticipate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtParticipate.Location = New System.Drawing.Point(632, 128)
        Me.txtParticipate.MaxLength = 0
        Me.txtParticipate.Name = "txtParticipate"
        Me.txtParticipate.ReadOnly = True
        Me.txtParticipate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtParticipate.Size = New System.Drawing.Size(20, 20)
        Me.txtParticipate.TabIndex = 96
        Me.txtParticipate.Text = ""
        '
        'txtModalPrem
        '
        Me.txtModalPrem.AcceptsReturn = True
        Me.txtModalPrem.AutoSize = False
        Me.txtModalPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtModalPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModalPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtModalPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModalPrem.Location = New System.Drawing.Point(88, 200)
        Me.txtModalPrem.MaxLength = 0
        Me.txtModalPrem.Name = "txtModalPrem"
        Me.txtModalPrem.ReadOnly = True
        Me.txtModalPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModalPrem.Size = New System.Drawing.Size(92, 20)
        Me.txtModalPrem.TabIndex = 95
        Me.txtModalPrem.Text = ""
        '
        'txtCov
        '
        Me.txtCov.AcceptsReturn = True
        Me.txtCov.AutoSize = False
        Me.txtCov.BackColor = System.Drawing.SystemColors.Window
        Me.txtCov.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCov.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCov.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCov.Location = New System.Drawing.Point(88, 128)
        Me.txtCov.MaxLength = 0
        Me.txtCov.Name = "txtCov"
        Me.txtCov.ReadOnly = True
        Me.txtCov.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCov.Size = New System.Drawing.Size(252, 20)
        Me.txtCov.TabIndex = 94
        Me.txtCov.Text = ""
        '
        'chkCOL
        '
        Me.chkCOL.BackColor = System.Drawing.SystemColors.Control
        Me.chkCOL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCOL.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCOL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkCOL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCOL.Location = New System.Drawing.Point(336, 336)
        Me.chkCOL.Name = "chkCOL"
        Me.chkCOL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCOL.Size = New System.Drawing.Size(81, 33)
        Me.chkCOL.TabIndex = 93
        Me.chkCOL.Text = "COL"
        '
        'chkIND
        '
        Me.chkIND.BackColor = System.Drawing.SystemColors.Control
        Me.chkIND.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkIND.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkIND.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkIND.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIND.Location = New System.Drawing.Point(336, 360)
        Me.chkIND.Name = "chkIND"
        Me.chkIND.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIND.Size = New System.Drawing.Size(81, 32)
        Me.chkIND.TabIndex = 92
        Me.chkIND.Text = "IND"
        '
        'chkGIO
        '
        Me.chkGIO.BackColor = System.Drawing.SystemColors.Control
        Me.chkGIO.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkGIO.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGIO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkGIO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGIO.Location = New System.Drawing.Point(336, 312)
        Me.chkGIO.Name = "chkGIO"
        Me.chkGIO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGIO.Size = New System.Drawing.Size(81, 33)
        Me.chkGIO.TabIndex = 91
        Me.chkGIO.Text = "GIO"
        '
        'chkADD
        '
        Me.chkADD.BackColor = System.Drawing.SystemColors.Control
        Me.chkADD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkADD.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkADD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkADD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkADD.Location = New System.Drawing.Point(16, 360)
        Me.chkADD.Name = "chkADD"
        Me.chkADD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkADD.Size = New System.Drawing.Size(81, 33)
        Me.chkADD.TabIndex = 90
        Me.chkADD.Text = "ADD"
        '
        'chkWOP
        '
        Me.chkWOP.BackColor = System.Drawing.SystemColors.Control
        Me.chkWOP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkWOP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWOP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkWOP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWOP.Location = New System.Drawing.Point(16, 336)
        Me.chkWOP.Name = "chkWOP"
        Me.chkWOP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWOP.Size = New System.Drawing.Size(81, 33)
        Me.chkWOP.TabIndex = 89
        Me.chkWOP.Text = "WOP"
        '
        'chkWPP
        '
        Me.chkWPP.BackColor = System.Drawing.SystemColors.Control
        Me.chkWPP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkWPP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWPP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkWPP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWPP.Location = New System.Drawing.Point(16, 312)
        Me.chkWPP.Name = "chkWPP"
        Me.chkWPP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWPP.Size = New System.Drawing.Size(81, 33)
        Me.chkWPP.TabIndex = 88
        Me.chkWPP.Text = "WP+"
        '
        'txtWPPrem
        '
        Me.txtWPPrem.AcceptsReturn = True
        Me.txtWPPrem.AutoSize = False
        Me.txtWPPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtWPPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWPPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtWPPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWPPrem.Location = New System.Drawing.Point(116, 316)
        Me.txtWPPrem.MaxLength = 0
        Me.txtWPPrem.Name = "txtWPPrem"
        Me.txtWPPrem.ReadOnly = True
        Me.txtWPPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWPPrem.Size = New System.Drawing.Size(81, 20)
        Me.txtWPPrem.TabIndex = 87
        Me.txtWPPrem.Text = ""
        '
        'txtADDPrem
        '
        Me.txtADDPrem.AcceptsReturn = True
        Me.txtADDPrem.AutoSize = False
        Me.txtADDPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtADDPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtADDPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtADDPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtADDPrem.Location = New System.Drawing.Point(116, 364)
        Me.txtADDPrem.MaxLength = 0
        Me.txtADDPrem.Name = "txtADDPrem"
        Me.txtADDPrem.ReadOnly = True
        Me.txtADDPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtADDPrem.Size = New System.Drawing.Size(81, 20)
        Me.txtADDPrem.TabIndex = 86
        Me.txtADDPrem.Text = ""
        '
        'txtWOPrem
        '
        Me.txtWOPrem.AcceptsReturn = True
        Me.txtWOPrem.AutoSize = False
        Me.txtWOPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtWOPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWOPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtWOPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWOPrem.Location = New System.Drawing.Point(116, 340)
        Me.txtWOPrem.MaxLength = 0
        Me.txtWOPrem.Name = "txtWOPrem"
        Me.txtWOPrem.ReadOnly = True
        Me.txtWOPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWOPrem.Size = New System.Drawing.Size(81, 20)
        Me.txtWOPrem.TabIndex = 85
        Me.txtWOPrem.Text = ""
        '
        'txtCOLPrem
        '
        Me.txtCOLPrem.AcceptsReturn = True
        Me.txtCOLPrem.AutoSize = False
        Me.txtCOLPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtCOLPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCOLPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCOLPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCOLPrem.Location = New System.Drawing.Point(436, 340)
        Me.txtCOLPrem.MaxLength = 0
        Me.txtCOLPrem.Name = "txtCOLPrem"
        Me.txtCOLPrem.ReadOnly = True
        Me.txtCOLPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCOLPrem.Size = New System.Drawing.Size(81, 20)
        Me.txtCOLPrem.TabIndex = 84
        Me.txtCOLPrem.Text = ""
        '
        'txtWPPSI
        '
        Me.txtWPPSI.AcceptsReturn = True
        Me.txtWPPSI.AutoSize = False
        Me.txtWPPSI.BackColor = System.Drawing.SystemColors.Window
        Me.txtWPPSI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWPPSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtWPPSI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWPPSI.Location = New System.Drawing.Point(204, 316)
        Me.txtWPPSI.MaxLength = 0
        Me.txtWPPSI.Name = "txtWPPSI"
        Me.txtWPPSI.ReadOnly = True
        Me.txtWPPSI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWPPSI.Size = New System.Drawing.Size(81, 20)
        Me.txtWPPSI.TabIndex = 83
        Me.txtWPPSI.Text = ""
        '
        'txtINDPrem
        '
        Me.txtINDPrem.AcceptsReturn = True
        Me.txtINDPrem.AutoSize = False
        Me.txtINDPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtINDPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtINDPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtINDPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtINDPrem.Location = New System.Drawing.Point(436, 364)
        Me.txtINDPrem.MaxLength = 0
        Me.txtINDPrem.Name = "txtINDPrem"
        Me.txtINDPrem.ReadOnly = True
        Me.txtINDPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtINDPrem.Size = New System.Drawing.Size(81, 20)
        Me.txtINDPrem.TabIndex = 82
        Me.txtINDPrem.Text = ""
        '
        'txtADDSI
        '
        Me.txtADDSI.AcceptsReturn = True
        Me.txtADDSI.AutoSize = False
        Me.txtADDSI.BackColor = System.Drawing.SystemColors.Window
        Me.txtADDSI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtADDSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtADDSI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtADDSI.Location = New System.Drawing.Point(204, 364)
        Me.txtADDSI.MaxLength = 0
        Me.txtADDSI.Name = "txtADDSI"
        Me.txtADDSI.ReadOnly = True
        Me.txtADDSI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtADDSI.Size = New System.Drawing.Size(81, 20)
        Me.txtADDSI.TabIndex = 81
        Me.txtADDSI.Text = ""
        '
        'txtWOPSI
        '
        Me.txtWOPSI.AcceptsReturn = True
        Me.txtWOPSI.AutoSize = False
        Me.txtWOPSI.BackColor = System.Drawing.SystemColors.Window
        Me.txtWOPSI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWOPSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtWOPSI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWOPSI.Location = New System.Drawing.Point(204, 340)
        Me.txtWOPSI.MaxLength = 0
        Me.txtWOPSI.Name = "txtWOPSI"
        Me.txtWOPSI.ReadOnly = True
        Me.txtWOPSI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWOPSI.Size = New System.Drawing.Size(81, 20)
        Me.txtWOPSI.TabIndex = 80
        Me.txtWOPSI.Text = ""
        '
        'txtGIOPrem
        '
        Me.txtGIOPrem.AcceptsReturn = True
        Me.txtGIOPrem.AutoSize = False
        Me.txtGIOPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtGIOPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGIOPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtGIOPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGIOPrem.Location = New System.Drawing.Point(436, 316)
        Me.txtGIOPrem.MaxLength = 0
        Me.txtGIOPrem.Name = "txtGIOPrem"
        Me.txtGIOPrem.ReadOnly = True
        Me.txtGIOPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGIOPrem.Size = New System.Drawing.Size(81, 20)
        Me.txtGIOPrem.TabIndex = 79
        Me.txtGIOPrem.Text = ""
        '
        'txtCOLSI
        '
        Me.txtCOLSI.AcceptsReturn = True
        Me.txtCOLSI.AutoSize = False
        Me.txtCOLSI.BackColor = System.Drawing.SystemColors.Window
        Me.txtCOLSI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCOLSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCOLSI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCOLSI.Location = New System.Drawing.Point(524, 340)
        Me.txtCOLSI.MaxLength = 0
        Me.txtCOLSI.Name = "txtCOLSI"
        Me.txtCOLSI.ReadOnly = True
        Me.txtCOLSI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCOLSI.Size = New System.Drawing.Size(81, 20)
        Me.txtCOLSI.TabIndex = 78
        Me.txtCOLSI.Text = ""
        '
        'txtINDSI
        '
        Me.txtINDSI.AcceptsReturn = True
        Me.txtINDSI.AutoSize = False
        Me.txtINDSI.BackColor = System.Drawing.SystemColors.Window
        Me.txtINDSI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtINDSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtINDSI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtINDSI.Location = New System.Drawing.Point(524, 364)
        Me.txtINDSI.MaxLength = 0
        Me.txtINDSI.Name = "txtINDSI"
        Me.txtINDSI.ReadOnly = True
        Me.txtINDSI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtINDSI.Size = New System.Drawing.Size(81, 20)
        Me.txtINDSI.TabIndex = 77
        Me.txtINDSI.Text = ""
        '
        'txtGIOSI
        '
        Me.txtGIOSI.AcceptsReturn = True
        Me.txtGIOSI.AutoSize = False
        Me.txtGIOSI.BackColor = System.Drawing.SystemColors.Window
        Me.txtGIOSI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGIOSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtGIOSI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGIOSI.Location = New System.Drawing.Point(524, 316)
        Me.txtGIOSI.MaxLength = 0
        Me.txtGIOSI.Name = "txtGIOSI"
        Me.txtGIOSI.ReadOnly = True
        Me.txtGIOSI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGIOSI.Size = New System.Drawing.Size(81, 20)
        Me.txtGIOSI.TabIndex = 76
        Me.txtGIOSI.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(568, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(61, 16)
        Me.Label4.TabIndex = 123
        Me.Label4.Text = "Policy Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(516, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(119, 16)
        Me.Label5.TabIndex = 122
        Me.Label5.Text = "Premium Change Date"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(568, 228)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(63, 16)
        Me.Label6.TabIndex = 121
        Me.Label6.Text = "Expiry Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(520, 180)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(114, 16)
        Me.Label7.TabIndex = 120
        Me.Label7.Text = "Commencement Date"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(28, 228)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(53, 16)
        Me.Label19.TabIndex = 119
        Me.Label19.Text = "Flat Extra"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(352, 204)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(60, 16)
        Me.Label20.TabIndex = 118
        Me.Label20.Text = "Rate Scale"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(344, 156)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(68, 16)
        Me.Label22.TabIndex = 116
        Me.Label22.Text = "Sum Insured"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(372, 132)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(36, 16)
        Me.Label23.TabIndex = 115
        Me.Label23.Text = "Status"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(0, 204)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(83, 16)
        Me.Label24.TabIndex = 114
        Me.Label24.Text = "Modal Premium"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(572, 132)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(58, 16)
        Me.Label25.TabIndex = 113
        Me.Label25.Text = "Participate"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(28, 132)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(53, 16)
        Me.Label26.TabIndex = 112
        Me.Label26.Text = "Coverage"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(352, 360)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(81, 33)
        Me.Label29.TabIndex = 111
        Me.Label29.Text = "Label29"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(216, 300)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(68, 16)
        Me.Label27.TabIndex = 110
        Me.Label27.Text = "Sum Insured"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(132, 300)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(50, 16)
        Me.Label28.TabIndex = 109
        Me.Label28.Text = "Premium"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(456, 300)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(50, 16)
        Me.Label30.TabIndex = 108
        Me.Label30.Text = "Premium"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(536, 300)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(68, 16)
        Me.Label31.TabIndex = 107
        Me.Label31.Text = "Sum Insured"
        '
        'grdInsured
        '
        Me.grdInsured.AlternatingBackColor = System.Drawing.Color.White
        Me.grdInsured.BackColor = System.Drawing.Color.White
        Me.grdInsured.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdInsured.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdInsured.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInsured.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdInsured.CaptionVisible = False
        Me.grdInsured.DataMember = ""
        Me.grdInsured.FlatMode = True
        Me.grdInsured.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdInsured.ForeColor = System.Drawing.Color.Black
        Me.grdInsured.GridLineColor = System.Drawing.Color.Wheat
        Me.grdInsured.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdInsured.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdInsured.HeaderForeColor = System.Drawing.Color.Black
        Me.grdInsured.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInsured.Location = New System.Drawing.Point(4, 392)
        Me.grdInsured.Name = "grdInsured"
        Me.grdInsured.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdInsured.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdInsured.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdInsured.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInsured.Size = New System.Drawing.Size(656, 68)
        Me.grdInsured.TabIndex = 124
        '
        'txtTitle
        '
        Me.txtTitle.AcceptsReturn = True
        Me.txtTitle.AutoSize = False
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTitle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTitle.Location = New System.Drawing.Point(84, 468)
        Me.txtTitle.MaxLength = 0
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTitle.Size = New System.Drawing.Size(53, 20)
        Me.txtTitle.TabIndex = 137
        Me.txtTitle.Text = ""
        '
        'txtLastName
        '
        Me.txtLastName.AcceptsReturn = True
        Me.txtLastName.AutoSize = False
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLastName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastName.Location = New System.Drawing.Point(140, 468)
        Me.txtLastName.MaxLength = 0
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastName.Size = New System.Drawing.Size(189, 20)
        Me.txtLastName.TabIndex = 136
        Me.txtLastName.Text = ""
        '
        'txtFirstName
        '
        Me.txtFirstName.AcceptsReturn = True
        Me.txtFirstName.AutoSize = False
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFirstName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFirstName.Location = New System.Drawing.Point(332, 468)
        Me.txtFirstName.MaxLength = 0
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFirstName.Size = New System.Drawing.Size(189, 20)
        Me.txtFirstName.TabIndex = 135
        Me.txtFirstName.Text = ""
        '
        'txtChiName
        '
        Me.txtChiName.AcceptsReturn = True
        Me.txtChiName.AutoSize = False
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChiName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtChiName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChiName.Location = New System.Drawing.Point(524, 468)
        Me.txtChiName.MaxLength = 0
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChiName.Size = New System.Drawing.Size(81, 20)
        Me.txtChiName.TabIndex = 134
        Me.txtChiName.Text = ""
        '
        'txtID
        '
        Me.txtID.AcceptsReturn = True
        Me.txtID.AutoSize = False
        Me.txtID.BackColor = System.Drawing.SystemColors.Window
        Me.txtID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(84, 492)
        Me.txtID.MaxLength = 0
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtID.Size = New System.Drawing.Size(105, 20)
        Me.txtID.TabIndex = 133
        Me.txtID.Text = ""
        '
        'txtSex
        '
        Me.txtSex.AcceptsReturn = True
        Me.txtSex.AutoSize = False
        Me.txtSex.BackColor = System.Drawing.SystemColors.Window
        Me.txtSex.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSex.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSex.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSex.Location = New System.Drawing.Point(228, 492)
        Me.txtSex.MaxLength = 0
        Me.txtSex.Name = "txtSex"
        Me.txtSex.ReadOnly = True
        Me.txtSex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSex.Size = New System.Drawing.Size(45, 20)
        Me.txtSex.TabIndex = 132
        Me.txtSex.Text = ""
        '
        'txtDOB
        '
        Me.txtDOB.AcceptsReturn = True
        Me.txtDOB.AutoSize = False
        Me.txtDOB.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOB.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDOB.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDOB.Location = New System.Drawing.Point(392, 492)
        Me.txtDOB.MaxLength = 0
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.ReadOnly = True
        Me.txtDOB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOB.Size = New System.Drawing.Size(81, 20)
        Me.txtDOB.TabIndex = 131
        Me.txtDOB.Text = ""
        '
        'txtAAge
        '
        Me.txtAAge.AcceptsReturn = True
        Me.txtAAge.AutoSize = False
        Me.txtAAge.BackColor = System.Drawing.SystemColors.Window
        Me.txtAAge.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAAge.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAAge.Location = New System.Drawing.Point(84, 516)
        Me.txtAAge.MaxLength = 0
        Me.txtAAge.Name = "txtAAge"
        Me.txtAAge.ReadOnly = True
        Me.txtAAge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAAge.Size = New System.Drawing.Size(41, 20)
        Me.txtAAge.TabIndex = 130
        Me.txtAAge.Text = ""
        '
        'txtIAge
        '
        Me.txtIAge.AcceptsReturn = True
        Me.txtIAge.AutoSize = False
        Me.txtIAge.BackColor = System.Drawing.SystemColors.Window
        Me.txtIAge.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtIAge.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIAge.Location = New System.Drawing.Point(212, 516)
        Me.txtIAge.MaxLength = 0
        Me.txtIAge.Name = "txtIAge"
        Me.txtIAge.ReadOnly = True
        Me.txtIAge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIAge.Size = New System.Drawing.Size(45, 20)
        Me.txtIAge.TabIndex = 129
        Me.txtIAge.Text = ""
        '
        'txtCFrom
        '
        Me.txtCFrom.AcceptsReturn = True
        Me.txtCFrom.AutoSize = False
        Me.txtCFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtCFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCFrom.Location = New System.Drawing.Point(392, 516)
        Me.txtCFrom.MaxLength = 0
        Me.txtCFrom.Name = "txtCFrom"
        Me.txtCFrom.ReadOnly = True
        Me.txtCFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtCFrom.TabIndex = 128
        Me.txtCFrom.Text = ""
        '
        'txtCTo
        '
        Me.txtCTo.AcceptsReturn = True
        Me.txtCTo.AutoSize = False
        Me.txtCTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCTo.Location = New System.Drawing.Point(492, 516)
        Me.txtCTo.MaxLength = 0
        Me.txtCTo.Name = "txtCTo"
        Me.txtCTo.ReadOnly = True
        Me.txtCTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCTo.Size = New System.Drawing.Size(81, 20)
        Me.txtCTo.TabIndex = 127
        Me.txtCTo.Text = ""
        '
        'txtDrvExp
        '
        Me.txtDrvExp.AcceptsReturn = True
        Me.txtDrvExp.AutoSize = False
        Me.txtDrvExp.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrvExp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDrvExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDrvExp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDrvExp.Location = New System.Drawing.Point(84, 540)
        Me.txtDrvExp.MaxLength = 0
        Me.txtDrvExp.Name = "txtDrvExp"
        Me.txtDrvExp.ReadOnly = True
        Me.txtDrvExp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDrvExp.Size = New System.Drawing.Size(81, 20)
        Me.txtDrvExp.TabIndex = 126
        Me.txtDrvExp.Text = ""
        Me.txtDrvExp.Visible = False
        '
        'txtOccupCls
        '
        Me.txtOccupCls.AcceptsReturn = True
        Me.txtOccupCls.AutoSize = False
        Me.txtOccupCls.BackColor = System.Drawing.SystemColors.Window
        Me.txtOccupCls.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOccupCls.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOccupCls.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOccupCls.Location = New System.Drawing.Point(392, 540)
        Me.txtOccupCls.MaxLength = 0
        Me.txtOccupCls.Name = "txtOccupCls"
        Me.txtOccupCls.ReadOnly = True
        Me.txtOccupCls.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOccupCls.Size = New System.Drawing.Size(181, 20)
        Me.txtOccupCls.TabIndex = 125
        Me.txtOccupCls.Text = ""
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(480, 520)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(8, 16)
        Me.Label32.TabIndex = 149
        Me.Label32.Text = "-"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(4, 472)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(75, 16)
        Me.Label33.TabIndex = 148
        Me.Label33.Text = "Insured Name"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(36, 496)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(43, 16)
        Me.Label34.TabIndex = 147
        Me.Label34.Text = "ID Card"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(8, 520)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(69, 16)
        Me.Label35.TabIndex = 146
        Me.Label35.Text = "Attained Age"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(12, 544)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(65, 16)
        Me.Label36.TabIndex = 145
        Me.Label36.Text = "Driving Exp."
        Me.Label36.Visible = False
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(200, 496)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(24, 16)
        Me.Label37.TabIndex = 144
        Me.Label37.Text = "Sex"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(152, 520)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(54, 16)
        Me.Label38.TabIndex = 143
        Me.Label38.Text = "Issue Age"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(296, 520)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(89, 16)
        Me.Label39.TabIndex = 142
        Me.Label39.Text = "Coverage Period"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(292, 544)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(93, 16)
        Me.Label40.TabIndex = 141
        Me.Label40.Text = "Occupation Class"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(320, 496)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(67, 16)
        Me.Label42.TabIndex = 139
        Me.Label42.Text = "Date of Birth"
        '
        'chkSmoker
        '
        Me.chkSmoker.Location = New System.Drawing.Point(492, 492)
        Me.chkSmoker.Name = "chkSmoker"
        Me.chkSmoker.TabIndex = 150
        Me.chkSmoker.Text = "Smoker"
        '
        'txtFEDur
        '
        Me.txtFEDur.AcceptsReturn = True
        Me.txtFEDur.AutoSize = False
        Me.txtFEDur.BackColor = System.Drawing.SystemColors.Window
        Me.txtFEDur.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFEDur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFEDur.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFEDur.Location = New System.Drawing.Point(192, 224)
        Me.txtFEDur.MaxLength = 0
        Me.txtFEDur.Name = "txtFEDur"
        Me.txtFEDur.ReadOnly = True
        Me.txtFEDur.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFEDur.Size = New System.Drawing.Size(32, 20)
        Me.txtFEDur.TabIndex = 151
        Me.txtFEDur.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(164, 228)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(22, 16)
        Me.Label1.TabIndex = 152
        Me.Label1.Text = "Dur"
        '
        'txtMRDur
        '
        Me.txtMRDur.AcceptsReturn = True
        Me.txtMRDur.AutoSize = False
        Me.txtMRDur.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRDur.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRDur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMRDur.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMRDur.Location = New System.Drawing.Point(372, 248)
        Me.txtMRDur.MaxLength = 0
        Me.txtMRDur.Name = "txtMRDur"
        Me.txtMRDur.ReadOnly = True
        Me.txtMRDur.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRDur.Size = New System.Drawing.Size(32, 20)
        Me.txtMRDur.TabIndex = 155
        Me.txtMRDur.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(344, 252)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(22, 16)
        Me.Label2.TabIndex = 156
        Me.Label2.Text = "Dur"
        '
        'txtMRFactor
        '
        Me.txtMRFactor.AcceptsReturn = True
        Me.txtMRFactor.AutoSize = False
        Me.txtMRFactor.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRFactor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRFactor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMRFactor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMRFactor.Location = New System.Drawing.Point(268, 248)
        Me.txtMRFactor.MaxLength = 0
        Me.txtMRFactor.Name = "txtMRFactor"
        Me.txtMRFactor.ReadOnly = True
        Me.txtMRFactor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRFactor.Size = New System.Drawing.Size(72, 20)
        Me.txtMRFactor.TabIndex = 153
        Me.txtMRFactor.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(164, 252)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(99, 16)
        Me.Label3.TabIndex = 154
        Me.Label3.Text = "Multi Rating Factor"
        '
        'txtADM
        '
        Me.txtADM.AcceptsReturn = True
        Me.txtADM.AutoSize = False
        Me.txtADM.BackColor = System.Drawing.SystemColors.Window
        Me.txtADM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtADM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtADM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtADM.Location = New System.Drawing.Point(88, 272)
        Me.txtADM.MaxLength = 0
        Me.txtADM.Name = "txtADM"
        Me.txtADM.ReadOnly = True
        Me.txtADM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtADM.Size = New System.Drawing.Size(32, 20)
        Me.txtADM.TabIndex = 157
        Me.txtADM.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(48, 276)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(33, 16)
        Me.Label8.TabIndex = 158
        Me.Label8.Text = "AD-M"
        '
        'txtWPM
        '
        Me.txtWPM.AcceptsReturn = True
        Me.txtWPM.AutoSize = False
        Me.txtWPM.BackColor = System.Drawing.SystemColors.Window
        Me.txtWPM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtWPM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWPM.Location = New System.Drawing.Point(328, 272)
        Me.txtWPM.MaxLength = 0
        Me.txtWPM.Name = "txtWPM"
        Me.txtWPM.ReadOnly = True
        Me.txtWPM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWPM.Size = New System.Drawing.Size(32, 20)
        Me.txtWPM.TabIndex = 159
        Me.txtWPM.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(288, 276)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(36, 16)
        Me.Label9.TabIndex = 160
        Me.Label9.Text = "WP-M"
        '
        'txtAnnlPrem
        '
        Me.txtAnnlPrem.AcceptsReturn = True
        Me.txtAnnlPrem.AutoSize = False
        Me.txtAnnlPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnlPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAnnlPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAnnlPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAnnlPrem.Location = New System.Drawing.Point(248, 200)
        Me.txtAnnlPrem.MaxLength = 0
        Me.txtAnnlPrem.Name = "txtAnnlPrem"
        Me.txtAnnlPrem.ReadOnly = True
        Me.txtAnnlPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnnlPrem.Size = New System.Drawing.Size(92, 20)
        Me.txtAnnlPrem.TabIndex = 161
        Me.txtAnnlPrem.Text = ""
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(184, 204)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(57, 16)
        Me.Label10.TabIndex = 162
        Me.Label10.Text = "Annl.Prem"
        '
        'txtPolFee
        '
        Me.txtPolFee.AcceptsReturn = True
        Me.txtPolFee.AutoSize = False
        Me.txtPolFee.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolFee.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPolFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPolFee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPolFee.Location = New System.Drawing.Point(412, 224)
        Me.txtPolFee.MaxLength = 0
        Me.txtPolFee.Name = "txtPolFee"
        Me.txtPolFee.ReadOnly = True
        Me.txtPolFee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPolFee.Size = New System.Drawing.Size(92, 20)
        Me.txtPolFee.TabIndex = 163
        Me.txtPolFee.Text = ""
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(352, 228)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(57, 16)
        Me.Label11.TabIndex = 164
        Me.Label11.Text = "Policy Fee"
        '
        'txtADPrem
        '
        Me.txtADPrem.AcceptsReturn = True
        Me.txtADPrem.AutoSize = False
        Me.txtADPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtADPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtADPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtADPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtADPrem.Location = New System.Drawing.Point(184, 272)
        Me.txtADPrem.MaxLength = 0
        Me.txtADPrem.Name = "txtADPrem"
        Me.txtADPrem.ReadOnly = True
        Me.txtADPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtADPrem.Size = New System.Drawing.Size(92, 20)
        Me.txtADPrem.TabIndex = 165
        Me.txtADPrem.Text = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(128, 276)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(51, 16)
        Me.Label12.TabIndex = 166
        Me.Label12.Text = "AD-Prem"
        '
        'txtWPPrem1
        '
        Me.txtWPPrem1.AcceptsReturn = True
        Me.txtWPPrem1.AutoSize = False
        Me.txtWPPrem1.BackColor = System.Drawing.SystemColors.Window
        Me.txtWPPrem1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWPPrem1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtWPPrem1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWPPrem1.Location = New System.Drawing.Point(428, 272)
        Me.txtWPPrem1.MaxLength = 0
        Me.txtWPPrem1.Name = "txtWPPrem1"
        Me.txtWPPrem1.ReadOnly = True
        Me.txtWPPrem1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWPPrem1.Size = New System.Drawing.Size(92, 20)
        Me.txtWPPrem1.TabIndex = 167
        Me.txtWPPrem1.Text = ""
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(368, 276)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(53, 16)
        Me.Label13.TabIndex = 168
        Me.Label13.Text = "WP-Prem"
        '
        'txtFRR
        '
        Me.txtFRR.AcceptsReturn = True
        Me.txtFRR.AutoSize = False
        Me.txtFRR.BackColor = System.Drawing.SystemColors.Window
        Me.txtFRR.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFRR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFRR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFRR.Location = New System.Drawing.Point(268, 224)
        Me.txtFRR.MaxLength = 0
        Me.txtFRR.Name = "txtFRR"
        Me.txtFRR.ReadOnly = True
        Me.txtFRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFRR.Size = New System.Drawing.Size(32, 20)
        Me.txtFRR.TabIndex = 169
        Me.txtFRR.Text = ""
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(236, 228)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(27, 16)
        Me.Label14.TabIndex = 170
        Me.Label14.Text = "FRR"
        '
        'txtMRR
        '
        Me.txtMRR.AcceptsReturn = True
        Me.txtMRR.AutoSize = False
        Me.txtMRR.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRR.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMRR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMRR.Location = New System.Drawing.Point(448, 248)
        Me.txtMRR.MaxLength = 0
        Me.txtMRR.Name = "txtMRR"
        Me.txtMRR.ReadOnly = True
        Me.txtMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRR.Size = New System.Drawing.Size(32, 20)
        Me.txtMRR.TabIndex = 171
        Me.txtMRR.Text = ""
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(412, 252)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(30, 16)
        Me.Label15.TabIndex = 172
        Me.Label15.Text = "MRR"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(24, 252)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(58, 16)
        Me.Label16.TabIndex = 174
        Me.Label16.Text = "Multi Extra"
        '
        'txtFaceAmt
        '
        Me.txtFaceAmt.AcceptsReturn = True
        Me.txtFaceAmt.AutoSize = False
        Me.txtFaceAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtFaceAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFaceAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFaceAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFaceAmt.Location = New System.Drawing.Point(412, 176)
        Me.txtFaceAmt.MaxLength = 0
        Me.txtFaceAmt.Name = "txtFaceAmt"
        Me.txtFaceAmt.ReadOnly = True
        Me.txtFaceAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFaceAmt.TabIndex = 175
        Me.txtFaceAmt.Text = ""
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(340, 180)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(71, 16)
        Me.Label17.TabIndex = 176
        Me.Label17.Text = "Face Amount"
        '
        'txtCCI
        '
        Me.txtCCI.AcceptsReturn = True
        Me.txtCCI.AutoSize = False
        Me.txtCCI.BackColor = System.Drawing.SystemColors.Window
        Me.txtCCI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCCI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCCI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCCI.Location = New System.Drawing.Point(632, 248)
        Me.txtCCI.MaxLength = 0
        Me.txtCCI.Name = "txtCCI"
        Me.txtCCI.ReadOnly = True
        Me.txtCCI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCCI.Size = New System.Drawing.Size(20, 20)
        Me.txtCCI.TabIndex = 177
        Me.txtCCI.Text = ""
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(500, 252)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(126, 16)
        Me.Label18.TabIndex = 178
        Me.Label18.Text = "Credit Card Installments"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdPOLIA
        '
        Me.grdPOLIA.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPOLIA.BackColor = System.Drawing.Color.White
        Me.grdPOLIA.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPOLIA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPOLIA.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPOLIA.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPOLIA.CaptionVisible = False
        Me.grdPOLIA.DataMember = ""
        Me.grdPOLIA.FlatMode = True
        Me.grdPOLIA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdPOLIA.ForeColor = System.Drawing.Color.Black
        Me.grdPOLIA.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPOLIA.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPOLIA.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPOLIA.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPOLIA.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPOLIA.Location = New System.Drawing.Point(8, 584)
        Me.grdPOLIA.Name = "grdPOLIA"
        Me.grdPOLIA.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPOLIA.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPOLIA.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPOLIA.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPOLIA.Size = New System.Drawing.Size(656, 124)
        Me.grdPOLIA.TabIndex = 179
        '
        'Coverage
        '
        Me.Controls.Add(Me.grdPOLIA)
        Me.Controls.Add(Me.txtCCI)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtFaceAmt)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.grdInsured)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtMRR)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtFRR)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtWPPrem1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtADPrem)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtPolFee)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtAnnlPrem)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtWPM)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtADM)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtMRDur)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtMRFactor)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtFEDur)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkADD)
        Me.Controls.Add(Me.chkIND)
        Me.Controls.Add(Me.chkSmoker)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.txtChiName)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.txtSex)
        Me.Controls.Add(Me.txtDOB)
        Me.Controls.Add(Me.txtAAge)
        Me.Controls.Add(Me.txtIAge)
        Me.Controls.Add(Me.txtCFrom)
        Me.Controls.Add(Me.txtCTo)
        Me.Controls.Add(Me.txtDrvExp)
        Me.Controls.Add(Me.txtOccupCls)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.Label42)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtExhibitDt)
        Me.Controls.Add(Me.txtPCDt)
        Me.Controls.Add(Me.txtExpDt)
        Me.Controls.Add(Me.txtIssDt)
        Me.Controls.Add(Me.txtFlatExtra)
        Me.Controls.Add(Me.txtRateScale)
        Me.Controls.Add(Me.txtSumInsured)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtMultiple)
        Me.Controls.Add(Me.txtParticipate)
        Me.Controls.Add(Me.txtModalPrem)
        Me.Controls.Add(Me.txtCov)
        Me.Controls.Add(Me.chkCOL)
        Me.Controls.Add(Me.chkGIO)
        Me.Controls.Add(Me.chkWOP)
        Me.Controls.Add(Me.chkWPP)
        Me.Controls.Add(Me.txtWPPrem)
        Me.Controls.Add(Me.txtADDPrem)
        Me.Controls.Add(Me.txtWOPrem)
        Me.Controls.Add(Me.txtCOLPrem)
        Me.Controls.Add(Me.txtWPPSI)
        Me.Controls.Add(Me.txtINDPrem)
        Me.Controls.Add(Me.txtADDSI)
        Me.Controls.Add(Me.txtWOPSI)
        Me.Controls.Add(Me.txtGIOPrem)
        Me.Controls.Add(Me.txtCOLSI)
        Me.Controls.Add(Me.txtINDSI)
        Me.Controls.Add(Me.txtGIOSI)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.grdCov)
        Me.Name = "Coverage"
        Me.Size = New System.Drawing.Size(720, 716)
        CType(Me.grdCov, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdInsured, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPOLIA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private ds As DataSet = New DataSet("CoverageInfo")
    Private strPolicy, strProd, strAcSts, strInsuredID As String
    Private dr, dr1 As DataRow
    Private dvCoInsured As DataView
    Private sCurModeFactor As Single
    Private blnWPRider As Boolean
    Private bm As BindingManagerBase

    Public Property PolicyAccountID() As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            strPolicy = Value
        End Set
    End Property

    Public WriteOnly Property srcDTCov(ByVal sModeFtr As Single, ByVal dtNA As DataTable, ByVal dtMisc As DataTable, ByVal dtPOLIA As DataTable)
        Set(ByVal Value)

            If Not Value Is Nothing Then

                sCurModeFactor = sModeFtr
                ds.Tables.Add(Value)
                ds.Tables.Add(dtNA)
                ds.Tables.Add(dtMisc)
                ds.Tables.Add(dtPOLIA)

                Dim i As Integer
                Dim drF As DataRow
                Dim strProdID As String
                If ds.Tables("Coverage").Rows.Count > 0 Then
                    drF = ds.Tables("Coverage").Rows(0)
                    strProd = "IN ('" & drF("ProductID") & "'"
                    strAcSts = "IN ('" & drF("CoverageStatus") & "'"

                    strProdID = drF.Item("ProductID")
                    If (Mid(strProdID, 1, 5) = "URWP+" Or Mid(strProdID, 1, 5) = "HRWP+") And _
                            (strAcSts = "1" OrElse strAcSts = "2" OrElse strAcSts = "3" OrElse strAcSts = "4" OrElse strAcSts = "V" OrElse strAcSts = "X") Then
                        blnWPRider = True
                    End If

                    Dim intCurRider As Integer
                    dtPOLIA.Columns.Add("Cov", GetType(UInt16))
                    intCurRider = 0

                    For j As Integer = 0 To ds.Tables("Coverage").Rows(0).Item("AgentCount") - 1
                        dtPOLIA.Rows(intCurRider).Item("Cov") = ds.Tables("Coverage").Rows(0).Item("RiderNumber")
                        intCurRider += 1
                    Next

                    For i = 1 To ds.Tables("Coverage").Rows.Count - 1
                        drF = ds.Tables("Coverage").Rows(i)
                        strProd &= ", '" & Trim(drF("ProductID")) & "'"
                        strAcSts &= ", '" & Trim(drF("CoverageStatus")) & "'"

                        strProdID = drF.Item("ProductID")
                        If (Mid(strProdID, 1, 5) = "URWP+" Or Mid(strProdID, 1, 5) = "HRWP+") And _
                                (strAcSts = "1" OrElse strAcSts = "2" OrElse strAcSts = "3" OrElse strAcSts = "4" OrElse strAcSts = "V" OrElse strAcSts = "X") Then
                            blnWPRider = True
                        End If

                        For j As Integer = 0 To ds.Tables("Coverage").Rows(i).Item("AgentCount") - 1
                            dtPOLIA.Rows(intCurRider).Item("Cov") = ds.Tables("Coverage").Rows(i).Item("RiderNumber")
                            intCurRider += 1
                        Next
                    Next
                    dtPOLIA.AcceptChanges()
                    strProd &= ")"
                    strAcSts &= ")"
                Else
                    strProd = "IN ('')"
                    strAcSts = "IN ('')"
                End If
                'dvCoInsured = New DataView(ds.Tables("Coverage"))

                Call BuildUI()

            End If
        End Set
    End Property

    Private Sub BuildUI()

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable

        Dim dcParent(1), dcChild(1) As DataColumn

        For i As Integer = 0 To ds.Tables("POLMISC").Rows.Count - 1
            If ds.Tables("POLMISC").Rows(i).Item("PolicyRelateCode") <> "PH" Then
                ds.Tables("POLMISC").Rows(i).Delete()
            End If
        Next
        ds.Tables("POLMISC").AcceptChanges()

        Try
            'strSQL = "Select ParticipationCode, ParticipationDesc from ParticipationCodes; "
            strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes where AccountStatusCode " & strAcSts & "; "
            strSQL &= "Select Left(ProductID,5) as ProductID, SUBSTRING(ProductID,6,1) as RateScale, Description from Product where Left(ProductID,5) " & strProd & " ; "
            strSQL &= "Select OccupationClassCode, OccupationClass from OccupationClassCodes; "

            ''' Display extra info, use CIW temp.
            ''strSQL &= "Select * from Coverage where PolicyAccountID = '" & strPolicy & "'"

            sqlconnect.ConnectionString = strCIWMcuConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            'sqlda.TableMappings.Add("ParticipationCodes1", "AccountStatusCodes")
            sqlda.TableMappings.Add("AccountStatusCodes1", "Product")
            'sqlda.TableMappings.Add("ParticipationCodes3", "OccupationClassCodes")
            sqlda.TableMappings.Add("AccountStatusCodes2", "OccupationClassCodes")
            ''sqlda.TableMappings.Add("ParticipationCodes3", "CIWCO")
            sqlda.Fill(ds, "AccountStatusCodes")

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        sqlconnect.Dispose()

        'Dim relParticipate As New Data.DataRelation("Participate", ds.Tables("ParticipationCodes").Columns("ParticipationCode"), _
        '    ds.Tables("Coverage").Columns("Participate"), True)
        Dim relAccStatus As New Data.DataRelation("AccStatus", ds.Tables("AccountStatusCodes").Columns("AccountStatusCode"), _
            ds.Tables("Coverage").Columns("CoverageStatus"), True)

        dcParent(0) = ds.Tables("Product").Columns("ProductID")
        dcParent(1) = ds.Tables("Product").Columns("RateScale")
        dcChild(0) = ds.Tables("Coverage").Columns("ProductID")
        dcChild(1) = ds.Tables("Coverage").Columns("RateScale")

        Dim relProduct As New Data.DataRelation("Product", dcParent, dcChild, True)

        Dim relMisc As New Data.DataRelation("Misc", ds.Tables("POLMISC").Columns("COTRAI"), _
            ds.Tables("Coverage").Columns("RiderNumber"), True)

        'Try
        '    ds.Relations.Add(relParticipate)
        'Catch sqlex As SqlClient.SqlException
        '    'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        Try
            ds.Relations.Add(relAccStatus)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relMisc)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relProduct)
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        ''' Display extra info, use CIW temp.
        ''Dim relCO As New Data.DataRelation("CO", ds.Tables("CIWCO").Columns("Trailer"), _
        ''    ds.Tables("Coverage").Columns("Trailer"), True)
        ''Try
        ''    ds.Relations.Add(relCO)
        ''Catch sqlex As SqlClient.SqlException
        ''    'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        ''Catch ex As Exception
        ''    'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        ''End Try

        If ds.Tables("Coverage").Rows.Count > 0 Then
            With ds.Tables("Coverage")
                .Columns.Add("Description", GetType(String))
                .Columns.Add("AccountStatus", GetType(String))
                '.Columns.Add("ParticipationDesc", GetType(String))
                .Columns.Add("Multiple", GetType(String))
            End With

            ' Format Coverage Grid
            Dim ts As New clsDataGridTableStyle
            Dim cs As DataGridTextBoxColumn

            'cs = New JoinTextBoxColumn("Product", ds.Tables("Product").Columns("Description"))
            'cs.Width = 220
            'cs.MappingName = "Description"
            'cs.HeaderText = "Coverage"
            'cs.NullText = gNULLText
            'ts.GridColumnStyles.Add(cs)
            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "ProductID"
            cs.HeaderText = "Product ID"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            'cs = New DataGridTextBoxColumn
            'cs.Width = 220
            'cs.MappingName = "Description"
            'cs.HeaderText = "Coverage"
            'cs.NullText = gNULLText
            'ts.GridColumnStyles.Add(cs)

            'cs = New JoinTextBoxColumn("Misc", ds.Tables("POLMISC").Columns("Description"))
            cs = New JoinTextBoxColumn("Product", ds.Tables("Product").Columns("Description"))
            cs.Width = 220
            cs.MappingName = "Description"
            cs.HeaderText = "Coverage"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New JoinTextBoxColumn("AccStatus", ds.Tables("AccountStatusCodes").Columns("AccountStatus"))
            cs.Width = 100
            cs.MappingName = "AccountStatus"
            cs.HeaderText = "Status"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "RateScale"
            cs.HeaderText = "Rate Scale"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            'cs = New JoinTextBoxColumn("Participate", ds.Tables("ParticipationCodes").Columns("ParticipationDesc"))
            'cs.Width = 100
            'cs.MappingName = "ParticipationDesc"
            'cs.HeaderText = "Participate"
            'cs.NullText = gNULLText
            'ts.GridColumnStyles.Add(cs)
            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "Participate"
            cs.HeaderText = "Participate"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 50
            cs.MappingName = "InsuredAge"
            cs.HeaderText = "Age"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 50
            cs.MappingName = "SmokerFlag"
            cs.HeaderText = "Smoker"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "SumInsured"
            cs.HeaderText = "Sum Insured"
            cs.NullText = gNULLText
            cs.Format = gNumFormat
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "CovFaceAmt"
            cs.HeaderText = "Face Amount"
            cs.NullText = gNULLText
            cs.Format = gNumFormat
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "IssueDate"
            cs.HeaderText = "Issue Date"
            cs.Format = gDateFormat
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "ExpiryDate"
            cs.HeaderText = "Mat/Exp. Date"
            cs.Format = gDateFormat
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "ModalPremium"
            cs.HeaderText = "Modal Premium"
            cs.NullText = gNULLText
            cs.Format = gNumFormat
            ts.GridColumnStyles.Add(cs)

            ''cs = New DataGridTextBoxColumn
            ''cs.Width = 100
            ''cs.MappingName = "FlatExtra"
            ''cs.HeaderText = "Flat Extra"
            ''cs.NullText = gNULLText
            ''cs.Format = gNumFormat
            ''ts.GridColumnStyles.Add(cs)

            'cs = New DataGridTextBoxColumn
            'cs.Width = 100
            'cs.MappingName = "Multiple"
            'cs.HeaderText = "Multiple"
            'cs.NullText = gNULLText
            'cs.Format = gNumFormat
            'ts.GridColumnStyles.Add(cs)

            ''cs = New JoinTextBoxColumn("Misc", ds.Tables("POLMISC").Columns("Multiple"))
            ''cs.Width = 100
            ''cs.MappingName = "Multiple"
            ''cs.HeaderText = "Multiple"
            ''cs.NullText = gNULLText
            ''cs.Format = gNumFormat
            ''ts.GridColumnStyles.Add(cs)

            ts.MappingName = "Coverage"
            grdCov.TableStyles.Add(ts)

            grdCov.DataSource = ds.Tables("Coverage")
            grdCov.AllowDrop = False
            grdCov.ReadOnly = True

            'Me.txtIssDt.DataBindings.Add("Text", ds.Tables("Coverage"), "IssueDate")
            Dim bIssDt As Binding = New Binding("Text", ds.Tables("Coverage"), "IssueDate")
            Me.txtIssDt.DataBindings.Add(bIssDt)
            AddHandler bIssDt.Format, AddressOf FormatDate

            ''Me.txtCov.DataBindings.Add("Text", ds.Tables("Coverage"), "Description")
            Me.txtRateScale.DataBindings.Add("Text", ds.Tables("Coverage"), "RateScale")
            'Me.txtSumInsured.DataBindings.Add("Text", ds.Tables("Coverage"), "SumInsured")
            'Me.txtMultiple.DataBindings.Add("Text", ds.Tables("Coverage"), "Multiple")
            'Me.txtFlatExtra.DataBindings.Add("Text", ds.Tables("Coverage"), "FlatExtra")
            'Me.txtModalPrem.DataBindings.Add("Text", ds.Tables("Coverage"), "ModalPremium")
            Me.txtParticipate.DataBindings.Add("Text", ds.Tables("Coverage"), "Participate")
            Me.txtCCI.DataBindings.Add("Text", ds.Tables("Coverage"), "CCI")

            'Me.txtExpDt.DataBindings.Add("Text", ds.Tables("Coverage"), "ExpiryDate")
            Dim bExpDt As Binding = New Binding("Text", ds.Tables("Coverage"), "ExpiryDate")
            Me.txtExpDt.DataBindings.Add(bExpDt)
            AddHandler bExpDt.Format, AddressOf FormatDate

            'Me.txtPCDt.DataBindings.Add("Text", ds.Tables("Coverage"), "PremiumChangeDate")
            Dim bPCDt As Binding = New Binding("Text", ds.Tables("Coverage"), "PremiumChangeDate")
            Me.txtPCDt.DataBindings.Add(bPCDt)
            AddHandler bPCDt.Format, AddressOf FormatDate

            'Me.txtExhibitDt.DataBindings.Add("Text", ds.Tables("Coverage"), "ExhibitInforceDate")
            Dim bExDt As Binding = New Binding("Text", ds.Tables("Coverage"), "ExhibitInforceDate")
            Me.txtExhibitDt.DataBindings.Add(bExDt)
            AddHandler bExDt.Format, AddressOf FormatDate

            '            ' Format Insured Grid
            '            Dim i, j As Integer
            '            Dim strErrMsg As String
            '            Dim dt1 As DataTable
            '            Dim lngErrNo As Long
            '            strInsuredID = "'-'"
            '            For i = 0 To ds.Tables("Coverage").Rows.Count - 1
            '                For j = 1 To 5
            '                    With ds.Tables("Coverage").Rows(i)
            '                        If .Item("RCLNU" & j) <> "" Then
            '                            strInsuredID &= ",'" & ds.Tables("Coverage").Rows(i).Item("RCLNU" & j) & "'"
            '                        End If
            '                    End With
            '                Next
            '            Next
            '#If UAT <> 0 Then
            '            objCS.Env = giEnv
            '#End If
            '            dt1 = objCS.GetORDUNA(strInsuredID, lngErrNo, strErrMsg)

            '            If lngErrNo <> 0 Then
            '                MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Error")
            '                Exit Sub
            '            Else
            '                If dt1.Rows.Count > 1 Then
            '                    For i = 1 To dt1.Rows.Count - 1
            '                        dt1.Rows(i).Delete()
            '                    Next
            '                End If
            '                ds.Tables.Add(dt1)
            '            End If

            Dim ts1 As New clsDataGridTableStyle
            Dim cs1 As DataGridTextBoxColumn

            cs1 = New DataGridTextBoxColumn
            cs1.Width = 120
            cs1.MappingName = "NameSuffix"
            cs1.HeaderText = "Insured Last Name"
            cs1.NullText = gNULLText
            ts1.GridColumnStyles.Add(cs1)

            cs1 = New DataGridTextBoxColumn
            cs1.Width = 100
            cs1.MappingName = "FirstName"
            cs1.HeaderText = "First Name"
            cs1.NullText = gNULLText
            ts1.GridColumnStyles.Add(cs1)

            cs1 = New DataGridTextBoxColumn
            cs1.Width = 100
            cs1.MappingName = "ChiName"
            cs1.HeaderText = "Chinese Name"
            cs1.NullText = gNULLText
            ts1.GridColumnStyles.Add(cs1)

            cs1 = New DataGridTextBoxColumn
            cs1.Width = 100
            cs1.MappingName = "GovernmentIDCard"
            cs1.HeaderText = "ID Card"
            cs1.NullText = gNULLText
            ts1.GridColumnStyles.Add(cs1)

            cs1 = New DataGridTextBoxColumn
            cs1.Width = 50
            cs1.MappingName = "Gender"
            cs1.HeaderText = "Sex"
            cs1.NullText = gNULLText
            ts1.GridColumnStyles.Add(cs1)

            'cs1 = New DataGridTextBoxColumn
            'cs1.Width = 70
            'cs1.MappingName = "SmokerFlag"
            'cs1.HeaderText = "Smoker"
            'cs.NullText = gNULLText
            'ts1.GridColumnStyles.Add(cs1)

            '***
            'cs1 = New DataGridTextBoxColumn
            'cs1.Width = 100
            'cs1.MappingName = "InsuredAge"
            'cs1.HeaderText = "Attained Age"
            'cs.NullText = gNULLText
            'ts1.GridColumnStyles.Add(cs1)

            'cs1 = New DataGridTextBoxColumn
            'cs1.Width = 100
            'cs1.MappingName = "Trailer"
            'cs1.HeaderText = "Issue Age"
            'cs.NullText = gNULLText
            'ts1.GridColumnStyles.Add(cs1)
            '***

            cs1 = New DataGridTextBoxColumn
            cs1.Width = 100
            cs1.MappingName = "DateOfBirth"
            cs1.HeaderText = "Date of Birth"
            cs1.Format = gDateFormat
            cs1.NullText = gNULLText
            ts1.GridColumnStyles.Add(cs1)

            'cs1 = New DataGridTextBoxColumn
            'cs1.Width = 100
            'cs1.MappingName = "DrivingExp"
            'cs1.HeaderText = "Driving Exp."
            'cs.NullText = gNULLText
            'ts1.GridColumnStyles.Add(cs1)

            ts1.MappingName = "ORDUNA"
            ts1.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
            grdInsured.TableStyles.Add(ts1)
            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            'If ExternalUser Then
            '    grdInsured.DataSource = MaskDTsource(ds.Tables("ORDUNA"), "GovernmentIDCard", MaskData.HKID)
            'Else
            '    grdInsured.DataSource = ds.Tables("ORDUNA")
            'End If
            ''CRS 7x24 Changes - End
            grdInsured.DataSource = ds.Tables("ORDUNA")
            'dvCoInsured.RowFilter = "Trailer=" & ds.Tables("Coverage").Rows(grdCov.CurrentRowIndex).Item("Trailer")
            'grdInsured.DataSource = dvCoInsured
            bm = Me.BindingContext(ds.Tables("ORDUNA"))
            grdInsured.AllowDrop = False
            grdInsured.ReadOnly = True


            ' Bind POLI-A grid
            Dim ts2 As New clsDataGridTableStyle
            Dim cs2 As DataGridTextBoxColumn

            cs2 = New DataGridTextBoxColumn
            cs2.Width = 40
            cs2.MappingName = "Cov"
            cs2.HeaderText = "C#"
            cs2.NullText = gNULLText
            ts2.GridColumnStyles.Add(cs2)

            cs2 = New DataGridTextBoxColumn
            cs2.Width = 40
            cs2.MappingName = "AgentSeq"
            cs2.HeaderText = "A#"
            cs2.NullText = gNULLText
            ts2.GridColumnStyles.Add(cs2)

            cs2 = New DataGridTextBoxColumn
            cs2.Width = 100
            cs2.MappingName = "AgentCode"
            cs2.HeaderText = "COMM-Agent#"
            cs2.NullText = gNULLText
            ts2.GridColumnStyles.Add(cs2)

            cs2 = New DataGridTextBoxColumn
            cs2.Width = 150
            cs2.MappingName = "AgentName"
            cs2.HeaderText = "Comm-Agent Name"
            cs2.NullText = gNULLText
            ts2.GridColumnStyles.Add(cs2)

            cs2 = New DataGridTextBoxColumn
            cs2.Width = 50
            cs2.MappingName = "AgentShare"
            cs2.HeaderText = "Share"
            cs2.NullText = gNULLText
            cs2.Format = gNumFormat
            ts2.GridColumnStyles.Add(cs2)

            cs2 = New DataGridTextBoxColumn
            cs2.Width = 100
            cs2.MappingName = "CommPremium"
            cs2.HeaderText = "COMM-PREM"
            cs2.NullText = gNULLText
            cs2.Format = gNumFormat
            ts2.GridColumnStyles.Add(cs2)

            cs2 = New DataGridTextBoxColumn
            cs2.Width = 100
            cs2.MappingName = "FYCComm"
            cs2.HeaderText = "FYR-COMM"
            cs2.NullText = gNULLText
            cs2.Format = gNumFormat
            ts2.GridColumnStyles.Add(cs2)

            ts2.MappingName = "POLIA"
            ts2.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
            grdPOLIA.TableStyles.Add(ts2)
            grdPOLIA.DataSource = ds.Tables("POLIA")
            grdPOLIA.AllowDrop = False
            grdPOLIA.ReadOnly = True
            ' End POLI-A

            Me.txtTitle.DataBindings.Add("Text", ds.Tables("ORDUNA"), "NamePrefix")
            Me.txtLastName.DataBindings.Add("Text", ds.Tables("ORDUNA"), "NameSuffix")
            Me.txtFirstName.DataBindings.Add("Text", ds.Tables("ORDUNA"), "FirstName")
            Me.txtChiName.DataBindings.Add("Text", ds.Tables("ORDUNA"), "ChiName")
            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            'If ExternalUser Then
            '    Me.txtID.DataBindings.Add("Text", MaskDTsource(ds.Tables("ORDUNA"), "GovernmentIDCard", MaskData.HKID), "GovernmentIDCard")
            'Else
            '    Me.txtID.DataBindings.Add("Text", ds.Tables("ORDUNA"), "GovernmentIDCard")
            'End If
            ''CRS 7x24 Changes - End
            Me.txtID.DataBindings.Add("Text", ds.Tables("ORDUNA"), "GovernmentIDCard")
            Me.txtSex.DataBindings.Add("Text", ds.Tables("ORDUNA"), "Gender")
            'Me.txtDOB.DataBindings.Add("Text", dvCoInsured, "DateOfBirth")
            Dim bDOB As Binding = New Binding("Text", ds.Tables("ORDUNA"), "DateOfBirth")
            Me.txtDOB.DataBindings.Add(bDOB)
            AddHandler bDOB.Format, AddressOf FormatDate
            '***
            'Me.txtAAge.DataBindings.Add("Text", ds.Tables("ORDUNA"), "InsuredAge")
            'Me.txtIAge.DataBindings.Add("Text", ds.Tables("ORDUNA"), "InsuredAge")
            '***
            'Me.txtCFrom.DataBindings.Add("Text", dvCoInsured, "IssueDate")
            'Dim bCFrom As Binding = New Binding("Text", ds.Tables("ORDUNA"), "IssueDate")
            Dim bCFrom As Binding = New Binding("Text", ds.Tables("Coverage"), "IssueDate")
            Me.txtCFrom.DataBindings.Add(bCFrom)
            AddHandler bCFrom.Format, AddressOf FormatDate

            'Me.txtCTo.DataBindings.Add("Text", dvCoInsured, "ExpiryDate")
            'Dim bCTo As Binding = New Binding("Text", ds.Tables("ORDUNA"), "ExpiryDate")
            Dim bCTo As Binding = New Binding("Text", ds.Tables("Coverage"), "ExpiryDate")
            Me.txtCTo.DataBindings.Add(bCTo)
            AddHandler bCTo.Format, AddressOf FormatDate

            'Me.txtDrvExp.DataBindings.Add("Text", ds.Tables("ORDUNA"), "DrivingExp")
            'Me.txtOccupCls.DataBindings.Add("Text", dvCoInsured, "OccpatClsCode")

            'Call corider()
            Call updateCO()
            Call updateINS()

        End If

    End Sub

    Private Sub updateCO()

        If grdCov.CurrentRowIndex >= 0 Then
            dr = ds.Tables("Coverage").Rows(grdCov.CurrentRowIndex)
            'dr1 = dr.GetParentRow("Product")
            'Me.txtCov.Text = dr1.Item("Description")

            'dr1 = dr.GetParentRow("AccStatus")
            'Me.txtStatus.Text = dr1.Item("AccountStatus")

            'dr1 = dr.GetParentRow("Participate")
            'Me.txtParticipate.Text = dr1.Item("ParticipationDesc")

            'Me.txtCov.Text = GetRelationValue(dr, "Misc", "Description")
            Me.txtCov.Text = GetRelationValue(dr, "Product", "Description")
            Me.txtStatus.Text = GetRelationValue(dr, "AccStatus", "AccountStatus")
            'Me.txtParticipate.Text = GetRelationValue(dr, "Participate", "ParticipationDesc")
            'Me.txtParticipate.Text = dr.Item("Participate")

            Me.txtSumInsured.Text = Format(dr.Item("SumInsured"), gNumFormat)
            Me.txtFaceAmt.Text = Format(dr.Item("CovFaceAmt"), gNumFormat)
            'Me.txtMultiple.Text = Format(dr.Item("Multiple"), gNumFormat)
            Me.txtMultiple.Text = Format(GetRelationValue(dr, "Misc", "Multiple"), gNumFormat)
            Me.txtMRFactor.Text = Format(dr.Item("MRFactor"), gNumFormat)
            Me.txtMRDur.Text = dr.Item("MRDuration")
            Me.txtMRR.Text = GetRelationValue(dr, "Misc", "MRR")

            Me.txtFlatExtra.Text = Format(dr.Item("FlatExtra"), gNumFormat)
            Me.txtFEDur.Text = dr.Item("FlatDuration")
            Me.txtFRR.Text = GetRelationValue(dr, "Misc", "FRR")

            Me.txtModalPrem.Text = Format(dr.Item("ModalPremium"), gNumFormat)
            Me.txtAnnlPrem.Text = Format(GetRelationValue(dr, "Misc", "AnnPrem"), gNumFormat)
            Me.txtPolFee.Text = Format(GetRelationValue(dr, "Misc", "PolFee"), gNumFormat)

            Me.txtADM.Text = Format(dr.Item("ADM"), gNumFormat)
            Me.txtADPrem.Text = Format(GetRelationValue(dr, "Misc", "ADPremium"), gNumFormat)
            Me.txtWPM.Text = Format(dr.Item("WPM"), gNumFormat)
            Me.txtWPPrem1.Text = Format(GetRelationValue(dr, "Misc", "WPPremium"), gNumFormat)

            Call corider(dr)

            ' Display extra info, use CIW temp.
            Dim strTypeIns, strTBL1, strTBL2 As String
            Dim strDesc1, strDesc2 As String
            'strTypeIns = Trim(GetRelationValue(dr, "CO", "TypeOfInsurance"))
            'strTBL1 = Trim(GetRelationValue(dr, "CO", "TBL1"))
            'strTBL2 = Trim(GetRelationValue(dr, "CO", "TBL2"))
            strTypeIns = Trim(dr.Item("InsuranceType"))
            strTBL1 = Trim(dr.Item("SubTable1"))
            strTBL2 = Trim(dr.Item("SubTable2"))

            If strTBL1 <> "" Then
                'Add 7 & C
                'If strTypeIns = "A" Then
                If strTypeIns = "A" Or strTypeIns = "7" Or strTypeIns = "C" Then

                    If strTypeIns = "A" Then
                        strDesc1 = GetDIRemark(strTypeIns, strTBL1)
                        strDesc2 = GetDIRemark(strTypeIns, strTBL2)
                    Else
                        strDesc1 = GetDIRemark("A", strTBL1)
                        strDesc2 = GetDIRemark("A", strTBL2)

                        If strDesc1 = "" Then strDesc1 = strTBL1
                        If strDesc2 = "" Then strDesc2 = strTBL2

                    End If

                    txtDesc.Text = strDesc1 + vbNewLine + strDesc2

                End If

                '****** TypeOfInsurance = "1" (DI Riders) ****************
                If strTypeIns = "1" And (strTBL1 <> "01" And strTBL1 <> "02" And strTBL1 <> "05" And strTBL1 <> "65") Then
                    strDesc1 = "Benefit Period (to age " + strTBL1 + ")"
                    strDesc2 = "Waiting Period (" + strTBL2 + " days)"

                    txtDesc.Text = strDesc1 + vbNewLine + strDesc2

                End If

                '****** TypeOfInsurance = '1' (DI Basic Plan) *****************
                If strTypeIns = "1" And (strTBL1 = "01" Or strTBL1 = "02" Or strTBL1 = "05" Or strTBL1 = "65") Then

                    strDesc1 = GetDIRemark(strTypeIns, strTBL1)
                    strDesc2 = "Waiting Period (" + strTBL2 + " days)"
                    txtDesc.Text = strDesc1 + vbNewLine + strDesc2

                End If
            Else
                txtDesc.Text = ""
            End If

            ''dvCoInsured.Table = ds.Tables("Coverage")

            'dvCoInsured.RowFilter = "Trailer=" & dr.Item("Trailer")
            'grdInsured.DataSource = dvCoInsured
            Dim i As Integer
            'strInsuredID = "'" & dr.Item("RCLNU1") & "'"
            strInsuredID = "'" & GetRelationValue(dr, "Misc", "RCLNU1") & "'"
            For i = 2 To 5
                'strInsuredID &= ",'" & dr.Item("RCLNU" & i) & "'"
                strInsuredID &= ",'" & GetRelationValue(dr, "Misc", "RCLNU" & i) & "'"
            Next
            ds.Tables("ORDUNA").DefaultView.RowFilter = "ClientID IN (" & strInsuredID & ")"

            ''If IsDBNull(dr.Item("OccpatClsCode")) Then
            ''    Me.txtOccupCls.Text = ""
            ''Else
            ''    Dim drs() As DataRow
            ''    drs = ds.Tables("OccupationClassCodes").Select("OccupationClassCode = '" & Trim(dr.Item("OccpatClsCode")) & "'")
            ''    If drs.Length > 0 Then Me.txtOccupCls.Text = drs(0).Item("OccupationClass")
            ''End If

            ' Display occupation class from product code
            Dim strProd, strOccClass As String
            strProd = dr.Item("ProductID")
            strOccClass = ""
            ' DI Plan
            If Mid(strProd, 2, 1) = "D" Then
                If Mid(strProd, 3, 1) = "A" Then
                    strOccClass = Mid(strProd, 3, 2)
                Else
                    strOccClass = Mid(strProd, 4, 2)
                End If
            End If
            ' PA Plan
            If Mid(strProd, 2, 3) = "RPA" Or Mid(strProd, 2, 3) = "RAI" Or Mid(strProd, 2, 3) = "RHS" Or Mid(strProd, 2, 3) = "RHI" Or Mid(strProd, 2, 3) = "RHP" Then
                strOccClass = Mid(strProd, 5, 1)
            End If
            If strOccClass <> "" Then
                Dim drs() As DataRow
                drs = ds.Tables("OccupationClassCodes").Select("OccupationClassCode = '" & strOccClass & "'")
                If drs.Length > 0 Then Me.txtOccupCls.Text = drs(0).Item("OccupationClass")
            Else
                Me.txtOccupCls.Text = ""
            End If

            ' Check smoker code
            If Not IsDBNull(dr.Item("SmokerFlag")) Then
                If dr.Item("SmokerFlag") = "S" Then Me.chkSmoker.Checked = True
            End If

        End If
    End Sub

    Private Sub grdCov_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCov.CurrentCellChanged
        Call updateCO()
    End Sub

    Private Sub grdInsured_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdInsured.CurrentCellChanged
        Call updateINS()
    End Sub

    Private Sub updateINS()
        Dim datIssdt, datDOB As Date
        Dim dr, drc As DataRow

        If grdInsured.CurrentRowIndex >= 0 Then
            Dim drI As DataRow = CType(bm.Current, DataRowView).Row()
            Dim strProdID As String

            If Not drI Is Nothing Then
                If Not IsDBNull(drI.Item("DateOfBirth")) Then
                    datDOB = drI.Item("DateOfBirth")
                End If
            End If
            'dr = ds.Tables("ORDUNA").Rows(grdInsured.CurrentRowIndex)
            drc = ds.Tables("Coverage").Rows(grdCov.CurrentRowIndex)
            datIssdt = Today
            'datDOB = dr.Item("DateOfBirth")
            ' Calculate Attained Age
            If InStr(strPolicy, "%.IF%") Then
                txtAAge.Text = drc.Item("InsuredAge")
            Else
                txtAAge.Text = CalAge(datIssdt, datDOB)
            End If

            ' Calculate Issue Age
            datIssdt = drc.Item("IssueDate")
            If InStr(strPolicy, "%.IF%") Then
                txtIAge.Text = drc.Item("InsuredAge")
            Else
                txtIAge.Text = CalAge(datIssdt, datDOB)
            End If
        End If
    End Sub

    Private Function CalAge(ByVal datIssdt As Date, ByVal datDOB As Date)

        Dim intIssAge As Integer

        intIssAge = DateDiff(DateInterval.Year, datDOB, datIssdt) + 1
        If DatePart(DateInterval.Month, datIssdt) < DatePart(DateInterval.Month, datDOB) Then
            intIssAge -= 1
        Else
            If DatePart(DateInterval.Month, datIssdt) = DatePart(DateInterval.Month, datDOB) And _
                    DatePart(DateInterval.Day, datIssdt) < DatePart(DateInterval.Day, datDOB) Then
                intIssAge -= 1
            End If
        End If

        Return intIssAge

    End Function

    Private Sub grdCov_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCov.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdCov.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdCov.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdCov.Select(hti.Row)
        End If
    End Sub

    Private Sub grdInsured_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdInsured.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdInsured.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdInsured.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdInsured.Select(hti.Row)
        End If
    End Sub

    Private Sub corider(ByVal drRider As DataRow)

        Dim sWPMultiplier, sWPPremium, sADMultiplier, sADPremium, sADFaceAmount As Single
        Dim strProdID As String
        Dim strCovSts As Char
        Dim blnInForce As Boolean
        ''Dim drRider As DataRow

        chkWPP.Checked = False
        txtWPPrem.Text = ""
        txtWPPSI.Text = ""
        chkWOP.Checked = False
        txtWOPrem.Text = ""
        txtWOPSI.Text = ""
        chkADD.Checked = False
        txtADDPrem.Text = ""
        txtADDSI.Text = ""
        chkGIO.Checked = False
        txtGIOPrem.Text = ""
        txtGIOSI.Text = ""
        chkCOL.Checked = False
        txtCOLPrem.Text = ""
        txtCOLSI.Text = ""
        chkIND.Checked = False
        txtINDPrem.Text = ""
        txtINDSI.Text = ""

        Dim sngWPPrem, sngWOPrem, sngADDPrem, sngADDSI, sngGIOPrem, sngCOLPrem, sngINDPrem As Single
        Dim blnWPP, blnWOP, blnADD, blnGIO, blnCOL, blnIND As Boolean

        'For Each drRider In ds.Tables("Coverage").Rows
        'With drRider
        '    sWPMultiplier = .Item("WPMultiplier")
        '    sWPPremium = .Item("WPPremium")
        '    sADMultiplier = .Item("ADMultiplier")
        '    sADPremium = .Item("ADPremium")
        '    sADFaceAmount = .Item("ADFaceAmount")
        '    strProdID = .Item("ProductID")
        '    strCovSts = .Item("CoverageStatus")
        'End With
        With drRider
            sWPMultiplier = .Item("WPM")
            sWPPremium = GetRelationValue(drRider, "Misc", "WPPremium")
            sADMultiplier = .Item("ADM")
            sADPremium = GetRelationValue(drRider, "Misc", "ADPremium")
            sADFaceAmount = GetRelationValue(drRider, "Misc", "ADFaceAmount")
            strProdID = .Item("ProductID")
            strCovSts = .Item("CoverageStatus")
        End With

        ' Show WP-Prem regardless of coverage status
        'If strCovSts = "1" OrElse strCovSts = "2" OrElse strCovSts = "3" OrElse strCovSts = "4" OrElse strCovSts = "V" OrElse strCovSts = "X" Then
        '    blnInForce = True
        'End If
        blnInForce = True

        If sWPMultiplier <> 0 Then
            If Mid(strProdID, 2, 1) = "D" Then  ' GIO
                'chkGIO.Checked = True
                blnGIO = True
                If blnInForce Then
                    sngGIOPrem += sWPPremium * sCurModeFactor
                End If
            Else
                If sWPMultiplier > 0 AndAlso sWPMultiplier < 5.0 Then
                    'chkWOP.Checked = True
                    blnWOP = True
                    If blnInForce Then
                        sngWOPrem += sWPPremium * sCurModeFactor
                    End If
                End If

                If sWPMultiplier >= 5.0 Then
                    'chkWPP.Checked = True
                    blnWPP = True
                    If blnInForce Then
                        sngWPPrem += sWPPremium * sCurModeFactor
                    End If
                End If
            End If
        End If

        If sADMultiplier <> 0 Then
            If Mid(strProdID, 2, 1) = "D" Then
                'chkCOL.Checked = True
                blnCOL = True
                If blnInForce Then
                    sngCOLPrem += sADPremium * sCurModeFactor
                End If
            Else
                'If drRider.Item("Trailer") = 1 Then
                If drRider.Item("RiderNumber") = 1 Then
                    'chkADD.Checked = True
                    blnADD = True
                    If blnInForce Then
                        sngADDPrem += sADPremium * sCurModeFactor
                        sngADDSI += sADFaceAmount
                    End If
                End If

                If Mid(strProdID, 2, 3) = "RPA" OrElse Mid(strProdID, 2, 3) = "RAI" OrElse Mid(strProdID, 2, 3) = "RFA" Then
                    'chkIND.Checked = True
                    blnIND = True
                    If blnInForce Then
                        sngINDPrem = sADPremium * sCurModeFactor
                    End If
                End If
            End If
        End If
        'Next

        If blnWPP = True And blnWPRider = True Then
            blnWPP = False
        End If

        If blnWOP = True And blnWPRider = True Then
            blnWOP = False
        End If

        If blnWPP Then
            chkWPP.Checked = blnWPP
            txtWPPrem.Text = Format(sngWPPrem, gNumFormat)
            txtWPPSI.Text = ""

            Me.txtModalPrem.Text = Format(dr.Item("ModalPremium") - sngWPPrem, gNumFormat)
        End If

        If blnWOP Then
            chkWOP.Checked = blnWOP
            txtWOPrem.Text = Format(sngWOPrem, gNumFormat)
            txtWOPSI.Text = ""

            Me.txtModalPrem.Text = Format(dr.Item("ModalPremium") - sngWOPrem, gNumFormat)
        End If

        If blnADD Then
            chkADD.Checked = blnADD
            txtADDPrem.Text = Format(sngADDPrem, gNumFormat)
            txtADDSI.Text = Format(sngADDSI, gNumFormat)
        End If

        If blnGIO Then
            chkGIO.Checked = blnGIO
            txtGIOPrem.Text = Format(sngGIOPrem, gNumFormat)
            txtGIOSI.Text = ""
        End If

        If blnCOL Then
            chkCOL.Checked = blnCOL
            txtCOLPrem.Text = Format(sngCOLPrem, gNumFormat)
            txtCOLSI.Text = ""
        End If

        If blnIND Then
            chkIND.Checked = blnIND
            txtINDPrem.Text = Format(sngINDPrem, gNumFormat)
            txtINDSI.Text = ""
        End If

    End Sub

    ''Private Sub corider1()

    ''    Dim sWPMultiplier, sWPPremium, sADMultiplier, sADPremium, sADFaceAmount As Single
    ''    Dim strProdID As String
    ''    Dim strCovSts As Char
    ''    Dim blnInForce As Boolean
    ''    Dim drRider As DataRow

    ''    chkWPP.Checked = False
    ''    txtWPPrem.Text = ""
    ''    txtWPPSI.Text = ""
    ''    chkWOP.Checked = False
    ''    txtWOPrem.Text = ""
    ''    txtWOPSI.Text = ""
    ''    chkADD.Checked = False
    ''    txtADDPrem.Text = ""
    ''    txtADDSI.Text = ""
    ''    chkGIO.Checked = False
    ''    txtGIOPrem.Text = ""
    ''    txtGIOSI.Text = ""
    ''    chkCOL.Checked = False
    ''    txtCOLPrem.Text = ""
    ''    txtCOLSI.Text = ""
    ''    chkIND.Checked = False
    ''    txtINDPrem.Text = ""
    ''    txtINDSI.Text = ""

    ''    Dim sngWPPrem, sngWOPrem, sngADDPrem, sngADDSI, sngGIOPrem, sngCOLPrem, sngINDPrem As Single
    ''    Dim blnWPP, blnWOP, blnADD, blnGIO, blnCOL, blnIND As Boolean

    ''    For Each drRider In ds.Tables("Coverage").Rows
    ''        'With drRider
    ''        '    sWPMultiplier = .Item("WPMultiplier")
    ''        '    sWPPremium = .Item("WPPremium")
    ''        '    sADMultiplier = .Item("ADMultiplier")
    ''        '    sADPremium = .Item("ADPremium")
    ''        '    sADFaceAmount = .Item("ADFaceAmount")
    ''        '    strProdID = .Item("ProductID")
    ''        '    strCovSts = .Item("CoverageStatus")
    ''        'End With
    ''        With drRider
    ''            sWPMultiplier = .Item("WPM")
    ''            sWPPremium = GetRelationValue(drRider, "Misc", "WPPremium")
    ''            sADMultiplier = .Item("ADM")
    ''            sADPremium = GetRelationValue(drRider, "Misc", "ADPremium")
    ''            sADFaceAmount = GetRelationValue(drRider, "Misc", "ADFaceAmount")
    ''            strProdID = .Item("ProductID")
    ''            strCovSts = .Item("CoverageStatus")
    ''        End With

    ''        If strCovSts = "1" OrElse strCovSts = "2" OrElse strCovSts = "3" OrElse strCovSts = "4" OrElse strCovSts = "V" OrElse strCovSts = "X" Then
    ''            blnInForce = True
    ''        End If

    ''        If sWPMultiplier <> 0 Then
    ''            If Mid(strProdID, 2, 1) = "D" Then  ' GIO
    ''                'chkGIO.Checked = True
    ''                blnGIO = True
    ''                If blnInForce Then
    ''                    sngGIOPrem += sWPPremium * sCurModeFactor
    ''                End If
    ''            Else
    ''                If sWPMultiplier > 0 AndAlso sWPMultiplier < 5.0 Then
    ''                    'chkWOP.Checked = True
    ''                    blnWOP = True
    ''                    If blnInForce Then
    ''                        sngWOPrem += sWPPremium * sCurModeFactor
    ''                    End If
    ''                End If

    ''                If sWPMultiplier >= 5.0 Then
    ''                    'chkWPP.Checked = True
    ''                    blnWPP = True
    ''                    If blnInForce Then
    ''                        sngWPPrem += sWPPremium * sCurModeFactor
    ''                    End If
    ''                End If
    ''            End If
    ''        End If

    ''        If sADMultiplier <> 0 Then
    ''            If Mid(strProdID, 2, 1) = "D" Then
    ''                'chkCOL.Checked = True
    ''                blnCOL = True
    ''                If blnInForce Then
    ''                    sngCOLPrem += sADPremium * sCurModeFactor
    ''                End If
    ''            Else
    ''                'If drRider.Item("Trailer") = 1 Then
    ''                If drRider.Item("RiderNumber") = 1 Then
    ''                    'chkADD.Checked = True
    ''                    blnADD = True
    ''                    If blnInForce Then
    ''                        sngADDPrem += sADPremium * sCurModeFactor
    ''                        sngADDSI += sADFaceAmount
    ''                    End If
    ''                End If

    ''                If Mid(strProdID, 2, 3) = "RPA" OrElse Mid(strProdID, 2, 3) = "RAI" OrElse Mid(strProdID, 2, 3) = "RFA" Then
    ''                    'chkIND.Checked = True
    ''                    blnIND = True
    ''                    If blnInForce Then
    ''                        sngINDPrem = sADPremium * sCurModeFactor
    ''                    End If
    ''                End If
    ''            End If
    ''        End If
    ''    Next

    ''    If blnWPP = True And blnWPRider = True Then
    ''        blnWPP = False
    ''    End If

    ''    If blnWOP = True And blnWPRider = True Then
    ''        blnWOP = False
    ''    End If

    ''    If blnWPP Then
    ''        chkWPP.Checked = blnWPP
    ''        txtWPPrem.Text = Format(sngWPPrem, gNumFormat)
    ''        txtWPPSI.Text = ""
    ''    End If

    ''    If blnWOP Then
    ''        chkWOP.Checked = blnWOP
    ''        txtWOPrem.Text = Format(sngWOPrem, gNumFormat)
    ''        txtWOPSI.Text = ""
    ''    End If

    ''    If blnADD Then
    ''        chkADD.Checked = blnADD
    ''        txtADDPrem.Text = Format(sngADDPrem, gNumFormat)
    ''        txtADDSI.Text = Format(sngADDSI, gNumFormat)
    ''    End If

    ''    If blnGIO Then
    ''        chkGIO.Checked = blnGIO
    ''        txtGIOPrem.Text = Format(sngGIOPrem, gNumFormat)
    ''        txtGIOSI.Text = ""
    ''    End If

    ''    If blnCOL Then
    ''        chkCOL.Checked = blnCOL
    ''        txtCOLPrem.Text = Format(sngCOLPrem, gNumFormat)
    ''        txtCOLSI.Text = ""
    ''    End If

    ''    If blnIND Then
    ''        chkIND.Checked = blnIND
    ''        txtINDPrem.Text = Format(sngINDPrem, gNumFormat)
    ''        txtINDSI.Text = ""
    ''    End If

    ''End Sub


End Class
