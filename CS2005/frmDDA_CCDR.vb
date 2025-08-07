Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class frmDDA_CCDR
    Inherits System.Windows.Forms.Form
    Private bm As BindingManagerBase
    Private bm2 As BindingManagerBase
    Dim objDS As DataSet = New DataSet("CS2005")
    Dim dt As DataTable
    Dim dr, dr1 As DataRow

    Private sqldt As DataTable
    Private sqldt2 As DataTable

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
    Friend WithEvents grdAC As System.Windows.Forms.DataGrid
    Friend WithEvents grdCard As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSeqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSTatus As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLastStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDrawDate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEndDate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSubmitDate As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtLstMintDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtEffDate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtFollowDate As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtOperator As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtFollowOper As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPayorInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtAC1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAC2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAC3 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtBankerName As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtStatusChgDate As System.Windows.Forms.TextBox
    Friend WithEvents txtStatusChgDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks1 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtComments1 As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtBankerName1 As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtCardNo3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCardNo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCardNo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtCardHolder As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtFollowOper1 As System.Windows.Forms.TextBox
    Friend WithEvents txtOperator1 As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtFollowDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtEffDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtLstMaintDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtSubmitDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtEndDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtDrawDate1 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtLastStatus1 As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtStatus1 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtSeqNo1 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdAC = New System.Windows.Forms.DataGrid
        Me.grdCard = New System.Windows.Forms.DataGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSeqNo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSTatus = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtLastStatus = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDrawDate = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEndDate = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSubmitDate = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtLstMintDate = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtEffDate = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtFollowDate = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtOperator = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtFollowOper = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtPayorInfo = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtAC1 = New System.Windows.Forms.TextBox
        Me.txtAC2 = New System.Windows.Forms.TextBox
        Me.txtAC3 = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtBankerName = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtStatusChgDate = New System.Windows.Forms.TextBox
        Me.txtStatusChgDate1 = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtRemarks1 = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtComments1 = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtBankerName1 = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtCardNo3 = New System.Windows.Forms.TextBox
        Me.txtCardNo2 = New System.Windows.Forms.TextBox
        Me.txtCardNo1 = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtCardHolder = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtFollowOper1 = New System.Windows.Forms.TextBox
        Me.txtOperator1 = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtFollowDate1 = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtEffDate1 = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtLstMaintDate1 = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtSubmitDate1 = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtEndDate1 = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtDrawDate1 = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtLastStatus1 = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtStatus1 = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtSeqNo1 = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        CType(Me.grdAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdAC
        '
        Me.grdAC.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAC.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAC.CaptionFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAC.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAC.CaptionVisible = False
        Me.grdAC.DataMember = ""
        Me.grdAC.FlatMode = True
        Me.grdAC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAC.ForeColor = System.Drawing.Color.Black
        Me.grdAC.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAC.HeaderBackColor = System.Drawing.Color.Teal
        Me.grdAC.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAC.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAC.Location = New System.Drawing.Point(8, 24)
        Me.grdAC.Name = "grdAC"
        Me.grdAC.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAC.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAC.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAC.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAC.Size = New System.Drawing.Size(352, 104)
        Me.grdAC.TabIndex = 0
        '
        'grdCard
        '
        Me.grdCard.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCard.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCard.CaptionFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCard.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCard.CaptionVisible = False
        Me.grdCard.DataMember = ""
        Me.grdCard.FlatMode = True
        Me.grdCard.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCard.ForeColor = System.Drawing.Color.Black
        Me.grdCard.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCard.HeaderBackColor = System.Drawing.Color.Teal
        Me.grdCard.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCard.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCard.Location = New System.Drawing.Point(376, 24)
        Me.grdCard.Name = "grdCard"
        Me.grdCard.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCard.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCard.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCard.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCard.Size = New System.Drawing.Size(352, 104)
        Me.grdCard.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Location = New System.Drawing.Point(8, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 24)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Sequence No."
        '
        'txtSeqNo
        '
        Me.txtSeqNo.Location = New System.Drawing.Point(96, 136)
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.Size = New System.Drawing.Size(64, 20)
        Me.txtSeqNo.TabIndex = 3
        Me.txtSeqNo.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 23)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Status"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSTatus
        '
        Me.txtSTatus.Location = New System.Drawing.Point(96, 160)
        Me.txtSTatus.Name = "txtSTatus"
        Me.txtSTatus.Size = New System.Drawing.Size(264, 20)
        Me.txtSTatus.TabIndex = 5
        Me.txtSTatus.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 184)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Last Status"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLastStatus
        '
        Me.txtLastStatus.Location = New System.Drawing.Point(96, 184)
        Me.txtLastStatus.Name = "txtLastStatus"
        Me.txtLastStatus.Size = New System.Drawing.Size(264, 20)
        Me.txtLastStatus.TabIndex = 7
        Me.txtLastStatus.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 208)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 16)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Draw Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDrawDate
        '
        Me.txtDrawDate.Location = New System.Drawing.Point(96, 208)
        Me.txtDrawDate.Name = "txtDrawDate"
        Me.txtDrawDate.Size = New System.Drawing.Size(80, 20)
        Me.txtDrawDate.TabIndex = 9
        Me.txtDrawDate.Text = ""
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(208, 208)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "End Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtEndDate
        '
        Me.txtEndDate.Location = New System.Drawing.Point(280, 208)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(80, 20)
        Me.txtEndDate.TabIndex = 11
        Me.txtEndDate.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 16)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Submit Date"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSubmitDate
        '
        Me.txtSubmitDate.Location = New System.Drawing.Point(96, 232)
        Me.txtSubmitDate.Name = "txtSubmitDate"
        Me.txtSubmitDate.Size = New System.Drawing.Size(80, 20)
        Me.txtSubmitDate.TabIndex = 13
        Me.txtSubmitDate.Text = ""
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(184, 232)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 16)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Last Maint. Date"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLstMintDate
        '
        Me.txtLstMintDate.Location = New System.Drawing.Point(280, 232)
        Me.txtLstMintDate.Name = "txtLstMintDate"
        Me.txtLstMintDate.Size = New System.Drawing.Size(80, 20)
        Me.txtLstMintDate.TabIndex = 15
        Me.txtLstMintDate.Text = ""
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(8, 256)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Effective Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtEffDate
        '
        Me.txtEffDate.Location = New System.Drawing.Point(96, 256)
        Me.txtEffDate.Name = "txtEffDate"
        Me.txtEffDate.Size = New System.Drawing.Size(80, 20)
        Me.txtEffDate.TabIndex = 17
        Me.txtEffDate.Text = ""
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(184, 256)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 16)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Follow-up Date"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFollowDate
        '
        Me.txtFollowDate.Location = New System.Drawing.Point(280, 256)
        Me.txtFollowDate.Name = "txtFollowDate"
        Me.txtFollowDate.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowDate.TabIndex = 19
        Me.txtFollowDate.Text = ""
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(16, 280)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 16)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Operator"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtOperator
        '
        Me.txtOperator.Location = New System.Drawing.Point(96, 280)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.Size = New System.Drawing.Size(80, 20)
        Me.txtOperator.TabIndex = 21
        Me.txtOperator.Text = ""
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(168, 280)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 16)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Follow-up Operator"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFollowOper
        '
        Me.txtFollowOper.Location = New System.Drawing.Point(280, 280)
        Me.txtFollowOper.Name = "txtFollowOper"
        Me.txtFollowOper.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowOper.TabIndex = 23
        Me.txtFollowOper.Text = ""
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(16, 304)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 16)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Payor Info."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPayorInfo
        '
        Me.txtPayorInfo.Location = New System.Drawing.Point(96, 304)
        Me.txtPayorInfo.Name = "txtPayorInfo"
        Me.txtPayorInfo.Size = New System.Drawing.Size(264, 20)
        Me.txtPayorInfo.TabIndex = 25
        Me.txtPayorInfo.Text = ""
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(8, 328)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 16)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Bank A/C No."
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAC1
        '
        Me.txtAC1.Location = New System.Drawing.Point(96, 328)
        Me.txtAC1.Name = "txtAC1"
        Me.txtAC1.Size = New System.Drawing.Size(40, 20)
        Me.txtAC1.TabIndex = 27
        Me.txtAC1.Text = ""
        '
        'txtAC2
        '
        Me.txtAC2.Location = New System.Drawing.Point(136, 328)
        Me.txtAC2.Name = "txtAC2"
        Me.txtAC2.Size = New System.Drawing.Size(40, 20)
        Me.txtAC2.TabIndex = 28
        Me.txtAC2.Text = ""
        '
        'txtAC3
        '
        Me.txtAC3.Location = New System.Drawing.Point(176, 328)
        Me.txtAC3.Name = "txtAC3"
        Me.txtAC3.Size = New System.Drawing.Size(184, 20)
        Me.txtAC3.TabIndex = 29
        Me.txtAC3.Text = ""
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(16, 352)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 16)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Banker Name"
        '
        'txtBankerName
        '
        Me.txtBankerName.Location = New System.Drawing.Point(96, 352)
        Me.txtBankerName.Name = "txtBankerName"
        Me.txtBankerName.Size = New System.Drawing.Size(264, 20)
        Me.txtBankerName.TabIndex = 31
        Me.txtBankerName.Text = ""
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(16, 376)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 16)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "Comments"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(96, 376)
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(264, 20)
        Me.txtComments.TabIndex = 33
        Me.txtComments.Text = ""
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(16, 400)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 16)
        Me.Label16.TabIndex = 34
        Me.Label16.Text = "Remarks"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(96, 400)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(264, 20)
        Me.txtRemarks.TabIndex = 35
        Me.txtRemarks.Text = ""
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(176, 136)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(96, 16)
        Me.Label17.TabIndex = 36
        Me.Label17.Text = "Status Chg Date"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusChgDate
        '
        Me.txtStatusChgDate.Location = New System.Drawing.Point(280, 136)
        Me.txtStatusChgDate.Name = "txtStatusChgDate"
        Me.txtStatusChgDate.Size = New System.Drawing.Size(80, 20)
        Me.txtStatusChgDate.TabIndex = 37
        Me.txtStatusChgDate.Text = ""
        '
        'txtStatusChgDate1
        '
        Me.txtStatusChgDate1.Location = New System.Drawing.Point(648, 136)
        Me.txtStatusChgDate1.Name = "txtStatusChgDate1"
        Me.txtStatusChgDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtStatusChgDate1.TabIndex = 73
        Me.txtStatusChgDate1.Text = ""
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(544, 136)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(96, 16)
        Me.Label18.TabIndex = 72
        Me.Label18.Text = "Status Chg Date"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRemarks1
        '
        Me.txtRemarks1.Location = New System.Drawing.Point(464, 400)
        Me.txtRemarks1.Name = "txtRemarks1"
        Me.txtRemarks1.Size = New System.Drawing.Size(264, 20)
        Me.txtRemarks1.TabIndex = 71
        Me.txtRemarks1.Text = ""
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(384, 400)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 16)
        Me.Label19.TabIndex = 70
        Me.Label19.Text = "Remarks"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtComments1
        '
        Me.txtComments1.Location = New System.Drawing.Point(464, 376)
        Me.txtComments1.Name = "txtComments1"
        Me.txtComments1.Size = New System.Drawing.Size(264, 20)
        Me.txtComments1.TabIndex = 69
        Me.txtComments1.Text = ""
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(384, 376)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 16)
        Me.Label20.TabIndex = 68
        Me.Label20.Text = "Comments"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBankerName1
        '
        Me.txtBankerName1.Location = New System.Drawing.Point(464, 352)
        Me.txtBankerName1.Name = "txtBankerName1"
        Me.txtBankerName1.Size = New System.Drawing.Size(264, 20)
        Me.txtBankerName1.TabIndex = 67
        Me.txtBankerName1.Text = ""
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(384, 352)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(80, 16)
        Me.Label21.TabIndex = 66
        Me.Label21.Text = "Banker Name"
        '
        'txtCardNo3
        '
        Me.txtCardNo3.Location = New System.Drawing.Point(544, 328)
        Me.txtCardNo3.Name = "txtCardNo3"
        Me.txtCardNo3.Size = New System.Drawing.Size(184, 20)
        Me.txtCardNo3.TabIndex = 65
        Me.txtCardNo3.Text = ""
        '
        'txtCardNo2
        '
        Me.txtCardNo2.Location = New System.Drawing.Point(504, 328)
        Me.txtCardNo2.Name = "txtCardNo2"
        Me.txtCardNo2.Size = New System.Drawing.Size(40, 20)
        Me.txtCardNo2.TabIndex = 64
        Me.txtCardNo2.Text = ""
        '
        'txtCardNo1
        '
        Me.txtCardNo1.Location = New System.Drawing.Point(464, 328)
        Me.txtCardNo1.Name = "txtCardNo1"
        Me.txtCardNo1.Size = New System.Drawing.Size(40, 20)
        Me.txtCardNo1.TabIndex = 63
        Me.txtCardNo1.Text = ""
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(376, 328)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(80, 16)
        Me.Label22.TabIndex = 62
        Me.Label22.Text = "Card No."
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCardHolder
        '
        Me.txtCardHolder.Location = New System.Drawing.Point(464, 304)
        Me.txtCardHolder.Name = "txtCardHolder"
        Me.txtCardHolder.Size = New System.Drawing.Size(264, 20)
        Me.txtCardHolder.TabIndex = 61
        Me.txtCardHolder.Text = ""
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(384, 304)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(72, 16)
        Me.Label23.TabIndex = 60
        Me.Label23.Text = "Card Holder"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFollowOper1
        '
        Me.txtFollowOper1.Location = New System.Drawing.Point(648, 280)
        Me.txtFollowOper1.Name = "txtFollowOper1"
        Me.txtFollowOper1.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowOper1.TabIndex = 59
        Me.txtFollowOper1.Text = ""
        '
        'txtOperator1
        '
        Me.txtOperator1.Location = New System.Drawing.Point(464, 280)
        Me.txtOperator1.Name = "txtOperator1"
        Me.txtOperator1.Size = New System.Drawing.Size(80, 20)
        Me.txtOperator1.TabIndex = 57
        Me.txtOperator1.Text = ""
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(384, 280)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(72, 16)
        Me.Label24.TabIndex = 56
        Me.Label24.Text = "Operator"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFollowDate1
        '
        Me.txtFollowDate1.Location = New System.Drawing.Point(648, 256)
        Me.txtFollowDate1.Name = "txtFollowDate1"
        Me.txtFollowDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowDate1.TabIndex = 55
        Me.txtFollowDate1.Text = ""
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(552, 256)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(88, 16)
        Me.Label25.TabIndex = 54
        Me.Label25.Text = "Follow-up Date"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtEffDate1
        '
        Me.txtEffDate1.Location = New System.Drawing.Point(464, 256)
        Me.txtEffDate1.Name = "txtEffDate1"
        Me.txtEffDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtEffDate1.TabIndex = 53
        Me.txtEffDate1.Text = ""
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(376, 256)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(80, 16)
        Me.Label26.TabIndex = 52
        Me.Label26.Text = "Effective Date"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLstMaintDate1
        '
        Me.txtLstMaintDate1.Location = New System.Drawing.Point(648, 232)
        Me.txtLstMaintDate1.Name = "txtLstMaintDate1"
        Me.txtLstMaintDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtLstMaintDate1.TabIndex = 51
        Me.txtLstMaintDate1.Text = ""
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(552, 232)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(88, 16)
        Me.Label27.TabIndex = 50
        Me.Label27.Text = "Last Maint. Date"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSubmitDate1
        '
        Me.txtSubmitDate1.Location = New System.Drawing.Point(464, 232)
        Me.txtSubmitDate1.Name = "txtSubmitDate1"
        Me.txtSubmitDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtSubmitDate1.TabIndex = 49
        Me.txtSubmitDate1.Text = ""
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(376, 232)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(80, 16)
        Me.Label28.TabIndex = 48
        Me.Label28.Text = "Submit Date"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtEndDate1
        '
        Me.txtEndDate1.Location = New System.Drawing.Point(648, 208)
        Me.txtEndDate1.Name = "txtEndDate1"
        Me.txtEndDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtEndDate1.TabIndex = 47
        Me.txtEndDate1.Text = ""
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(576, 208)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 16)
        Me.Label29.TabIndex = 46
        Me.Label29.Text = "End Date"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDrawDate1
        '
        Me.txtDrawDate1.Location = New System.Drawing.Point(464, 208)
        Me.txtDrawDate1.Name = "txtDrawDate1"
        Me.txtDrawDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtDrawDate1.TabIndex = 45
        Me.txtDrawDate1.Text = ""
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(376, 208)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(80, 16)
        Me.Label30.TabIndex = 44
        Me.Label30.Text = "Draw Date"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLastStatus1
        '
        Me.txtLastStatus1.Location = New System.Drawing.Point(464, 184)
        Me.txtLastStatus1.Name = "txtLastStatus1"
        Me.txtLastStatus1.Size = New System.Drawing.Size(264, 20)
        Me.txtLastStatus1.TabIndex = 43
        Me.txtLastStatus1.Text = ""
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(376, 184)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(72, 16)
        Me.Label31.TabIndex = 42
        Me.Label31.Text = "Last Status"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatus1
        '
        Me.txtStatus1.Location = New System.Drawing.Point(464, 160)
        Me.txtStatus1.Name = "txtStatus1"
        Me.txtStatus1.Size = New System.Drawing.Size(264, 20)
        Me.txtStatus1.TabIndex = 41
        Me.txtStatus1.Text = ""
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(376, 160)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(72, 23)
        Me.Label32.TabIndex = 40
        Me.Label32.Text = "Status"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSeqNo1
        '
        Me.txtSeqNo1.Location = New System.Drawing.Point(464, 136)
        Me.txtSeqNo1.Name = "txtSeqNo1"
        Me.txtSeqNo1.Size = New System.Drawing.Size(64, 20)
        Me.txtSeqNo1.TabIndex = 39
        Me.txtSeqNo1.Text = ""
        '
        'Label33
        '
        Me.Label33.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label33.Location = New System.Drawing.Point(376, 136)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(80, 24)
        Me.Label33.TabIndex = 38
        Me.Label33.Text = "Sequence No."
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(536, 280)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(104, 16)
        Me.Label34.TabIndex = 58
        Me.Label34.Text = "Follow-up Operator"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmDDA_CCDR
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(768, 453)
        Me.Controls.Add(Me.txtStatusChgDate1)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtRemarks1)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtComments1)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtBankerName1)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtCardNo3)
        Me.Controls.Add(Me.txtCardNo2)
        Me.Controls.Add(Me.txtCardNo1)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtCardHolder)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.txtFollowOper1)
        Me.Controls.Add(Me.txtOperator1)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtFollowDate1)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtEffDate1)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.txtLstMaintDate1)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.txtSubmitDate1)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.txtEndDate1)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.txtDrawDate1)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.txtLastStatus1)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.txtStatus1)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.txtSeqNo1)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.txtStatusChgDate)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtBankerName)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtAC3)
        Me.Controls.Add(Me.txtAC2)
        Me.Controls.Add(Me.txtAC1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtPayorInfo)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtFollowOper)
        Me.Controls.Add(Me.txtOperator)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtFollowDate)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtEffDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtLstMintDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtSubmitDate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtEndDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDrawDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtLastStatus)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSTatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSeqNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdCard)
        Me.Controls.Add(Me.grdAC)
        Me.Controls.Add(Me.Label11)
        Me.Name = "frmDDA_CCDR"
        Me.Text = "DDA / CCDR"
        CType(Me.grdAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDDA_CCDR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strSQL As String
        Dim strSQLCCDR As String
        Dim sqlconnect As New SqlConnection
        
        Dim sqlcmd As New SqlCommand
        Dim sqlcmd2 As New SqlCommand
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqlda2 As SqlDataAdapter

        'Load DDA data
        strSQL = "select * from DDA"
        strSQLCCDR = "select * from CCDR"
        sqlconnect.ConnectionString = objUtl.ConnStr("CSW", "CS2005", "CIW")
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        sqlcmd2.CommandText = strSQLCCDR
        sqlcmd2.Connection = sqlconnect

        Try
            lngCnt = sqlcmd.ExecuteScalar()
            lngCnt = sqlcmd2.ExecuteScalar()
            'MsgBox(lngCnt)
        Catch ex As Exception
        Catch sqlex As SqlException

        End Try

        If False Then
            'If lngCnt > gSearchLimit Then
            MsgBox("Over " & gSearchLimit & " records returned, please re-define your criteria.")
            Exit Sub
        Else
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda2 = New SqlDataAdapter(strSQLCCDR, sqlconnect)

            sqldt = New DataTable("DDA")
            sqldt2 = New DataTable("CCDR")

            bm = Me.BindingContext(sqldt)
            bm2 = Me.BindingContext(sqldt2)

            AddHandler bm.PositionChanged, AddressOf RowChanged
            AddHandler bm2.PositionChanged, AddressOf RowChangedCCDR

            Try
                sqlda.Fill(sqldt)
                sqlda2.Fill(sqldt2)

            Catch ex As Exception
            Catch sqlex As SqlException
            End Try
        End If

        sqlconnect.Close()
        grdAC.DataSource = sqldt
        grdCard.DataSource = sqldt2

    End Sub

    Private Sub RowChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Call UpdatePT("1")
    End Sub
    Private Sub RowChangedCCDR(ByVal sender As Object, ByVal e As System.EventArgs)
        Call UpdatePT("2")
    End Sub

    Private Sub UpdatePT(ByVal strOpt As String)
        Dim varAC As String()
        Dim varCardNo As String()
        Select Case strOpt
            Case "1" 'for DDA
                With sqldt.Rows.Item(bm.Position)
                    txtSeqNo.Text = .Item("DDASeqNo")
                    txtSTatus.Text = .Item("DDAStatus")
                    txtStatusChgDate.Text = .Item("DDAStsChgDate")
                    txtLastStatus.Text = .Item("DDALastStatus")
                    txtDrawDate.Text = .Item("DDADrawDate")
                    txtSubmitDate.Text = .Item("DDASubmitDate")
                    txtEffDate.Text = .Item("DDAEffectiveDate")
                    txtEndDate.Text = .Item("DDAEndDate")
                    txtLstMintDate.Text = .Item("DDALastMaintDate")
                    txtComments.Text = .Item("DDAComments")
                    txtOperator.Text = .Item("DDAOperator")
                    txtFollowDate.Text = .Item("DDAFollowUpDate")
                    txtFollowOper.Text = .Item("DDAFollowUpOpr")
                    txtRemarks.Text = .Item("DDARemarks")
                    txtPayorInfo.Text = .Item("DDAPayorInfo")
                    varAC = Split(.Item("DDABankAccountNo"), "-")
                    txtAC1.Text = varAC(0)
                    txtAC2.Text = varAC(1)
                    txtAC3.Text = varAC(2)
                    txtBankerName.Text = .Item("DDABankerName")

                End With
            Case "2" 'for CCDR
                With sqldt2.Rows.Item(bm2.Position)
                    txtSeqNo1.Text = .Item("CCDRSeqNo")
                    txtStatus1.Text = .Item("CCDRStatus")
                    txtStatusChgDate1.Text = .Item("CCDRStsChgDate")
                    txtLastStatus1.Text = .Item("CCDRLastStatus")
                    txtDrawDate1.Text = .Item("CCDRDrawDate")
                    txtSubmitDate1.Text = .Item("CCDRSubmitDate")
                    txtEffDate1.Text = .Item("CCDREffectiveDate")
                    txtEndDate1.Text = .Item("CCDREndDate")
                    txtLstMaintDate1.Text = .Item("CCDRLastMaintDate")
                    txtComments1.Text = .Item("CCDRComments")
                    txtOperator1.Text = .Item("CCDROperator")
                    txtFollowDate1.Text = .Item("CCDRFollowUpDate")
                    txtFollowOper1.Text = .Item("CCDRFollowUpOpr")
                    txtRemarks1.Text = .Item("CCDRRemarks")
                    txtCardHolder.Text = .Item("CCDRCardHolderName")
                    varCardNo = Split(.Item("CCDRCardNumber"), "-")
                    txtCardNo1.Text = varCardNo(0)
                    txtCardNo2.Text = varCardNo(1)
                    txtCardNo3.Text = varCardNo(2)
                    'txtBankerName.Text = .Item("DDABankerName")

                End With
        End Select
    End Sub
End Class