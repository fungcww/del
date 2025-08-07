Imports System.Data.SqlClient

Public Class DDACCDRMcu
    Inherits System.Windows.Forms.UserControl
    'oliver 2024-7-5 added comment for Table_Relocate_Sprint 14,It will not call if life asia policy

    Private bm As BindingManagerBase
    Private bm2 As BindingManagerBase
    Dim ds As DataSet = New DataSet("DDACCDR")
    Dim dr, dr1 As DataRow
    Friend WithEvents pnlCUPDDA As System.Windows.Forms.Panel
    Friend WithEvents txtCUPPayerId As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtCUPAccNum As System.Windows.Forms.TextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtCUPBranch As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPCity As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtCUPProvince As System.Windows.Forms.TextBox
    Friend WithEvents pnlDDAR As System.Windows.Forms.Panel
    Friend WithEvents Label45 As System.Windows.Forms.Label



    Public Const DDAIND_HSBCNET As String = "H"
    Friend WithEvents txtCUPPayerIdType As System.Windows.Forms.TextBox
    Friend WithEvents txtCUPBankName As System.Windows.Forms.TextBox
    Public Const DDAIND_CUP As String = "C"

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
    Friend WithEvents txtStatusChgDate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks1 As System.Windows.Forms.TextBox
    Friend WithEvents txtComments1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBankerName1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCardHolder As System.Windows.Forms.TextBox
    Friend WithEvents txtFollowOper1 As System.Windows.Forms.TextBox
    Friend WithEvents txtOperator1 As System.Windows.Forms.TextBox
    Friend WithEvents txtFollowDate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtEffDate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLstMaintDate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSubmitDate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtEndDate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDrawDate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLastStatus1 As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSeqNo1 As System.Windows.Forms.TextBox
    Friend WithEvents txtStatusChgDate As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents txtBankerName As System.Windows.Forms.TextBox
    Friend WithEvents txtAC3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAC2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAC1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPayorInfo As System.Windows.Forms.TextBox
    Friend WithEvents txtFollowOper As System.Windows.Forms.TextBox
    Friend WithEvents txtOperator As System.Windows.Forms.TextBox
    Friend WithEvents txtFollowDate As System.Windows.Forms.TextBox
    Friend WithEvents txtEffDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLstMintDate As System.Windows.Forms.TextBox
    Friend WithEvents txtSubmitDate As System.Windows.Forms.TextBox
    Friend WithEvents txtEndDate As System.Windows.Forms.TextBox
    Friend WithEvents txtDrawDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLastStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtSTatus As System.Windows.Forms.TextBox
    Friend WithEvents txtSeqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
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
    Friend WithEvents grdCard As System.Windows.Forms.DataGrid
    Friend WithEvents grdAC As System.Windows.Forms.DataGrid
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCardNo As System.Windows.Forms.TextBox
    Friend WithEvents txtExpDt As System.Windows.Forms.TextBox
    Friend WithEvents txtIniRej As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtSimpDDA As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtDebitLim As System.Windows.Forms.TextBox
    Friend WithEvents txtIDType As System.Windows.Forms.TextBox
    Friend WithEvents txtPayerID As System.Windows.Forms.TextBox
    Friend WithEvents txtRefNo As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtStatusChgDate1 = New System.Windows.Forms.TextBox
        Me.txtRemarks1 = New System.Windows.Forms.TextBox
        Me.txtComments1 = New System.Windows.Forms.TextBox
        Me.txtBankerName1 = New System.Windows.Forms.TextBox
        Me.txtCardNo = New System.Windows.Forms.TextBox
        Me.txtCardHolder = New System.Windows.Forms.TextBox
        Me.txtFollowOper1 = New System.Windows.Forms.TextBox
        Me.txtOperator1 = New System.Windows.Forms.TextBox
        Me.txtFollowDate1 = New System.Windows.Forms.TextBox
        Me.txtEffDate1 = New System.Windows.Forms.TextBox
        Me.txtLstMaintDate1 = New System.Windows.Forms.TextBox
        Me.txtSubmitDate1 = New System.Windows.Forms.TextBox
        Me.txtEndDate1 = New System.Windows.Forms.TextBox
        Me.txtDrawDate1 = New System.Windows.Forms.TextBox
        Me.txtLastStatus1 = New System.Windows.Forms.TextBox
        Me.txtStatus1 = New System.Windows.Forms.TextBox
        Me.txtSeqNo1 = New System.Windows.Forms.TextBox
        Me.txtStatusChgDate = New System.Windows.Forms.TextBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.txtBankerName = New System.Windows.Forms.TextBox
        Me.txtAC3 = New System.Windows.Forms.TextBox
        Me.txtAC2 = New System.Windows.Forms.TextBox
        Me.txtAC1 = New System.Windows.Forms.TextBox
        Me.txtPayorInfo = New System.Windows.Forms.TextBox
        Me.txtFollowOper = New System.Windows.Forms.TextBox
        Me.txtOperator = New System.Windows.Forms.TextBox
        Me.txtFollowDate = New System.Windows.Forms.TextBox
        Me.txtEffDate = New System.Windows.Forms.TextBox
        Me.txtLstMintDate = New System.Windows.Forms.TextBox
        Me.txtSubmitDate = New System.Windows.Forms.TextBox
        Me.txtEndDate = New System.Windows.Forms.TextBox
        Me.txtDrawDate = New System.Windows.Forms.TextBox
        Me.txtLastStatus = New System.Windows.Forms.TextBox
        Me.txtSTatus = New System.Windows.Forms.TextBox
        Me.txtSeqNo = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
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
        Me.grdCard = New System.Windows.Forms.DataGrid
        Me.grdAC = New System.Windows.Forms.DataGrid
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtExpDt = New System.Windows.Forms.TextBox
        Me.txtIniRej = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtSimpDDA = New System.Windows.Forms.TextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.txtDebitLim = New System.Windows.Forms.TextBox
        Me.txtIDType = New System.Windows.Forms.TextBox
        Me.txtPayerID = New System.Windows.Forms.TextBox
        Me.txtRefNo = New System.Windows.Forms.TextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.pnlCUPDDA = New System.Windows.Forms.Panel
        Me.Label64 = New System.Windows.Forms.Label
        Me.txtCUPBranch = New System.Windows.Forms.TextBox
        Me.txtCUPCity = New System.Windows.Forms.TextBox
        Me.Label46 = New System.Windows.Forms.Label
        Me.txtCUPProvince = New System.Windows.Forms.TextBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.txtCUPPayerId = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.txtCUPAccNum = New System.Windows.Forms.TextBox
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.pnlDDAR = New System.Windows.Forms.Panel
        Me.txtCUPBankName = New System.Windows.Forms.TextBox
        Me.txtCUPPayerIdType = New System.Windows.Forms.TextBox
        CType(Me.grdCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCUPDDA.SuspendLayout()
        Me.pnlDDAR.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtStatusChgDate1
        '
        Me.txtStatusChgDate1.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatusChgDate1.Location = New System.Drawing.Point(632, 116)
        Me.txtStatusChgDate1.Name = "txtStatusChgDate1"
        Me.txtStatusChgDate1.ReadOnly = True
        Me.txtStatusChgDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtStatusChgDate1.TabIndex = 147
        '
        'txtRemarks1
        '
        Me.txtRemarks1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks1.Location = New System.Drawing.Point(448, 432)
        Me.txtRemarks1.Multiline = True
        Me.txtRemarks1.Name = "txtRemarks1"
        Me.txtRemarks1.ReadOnly = True
        Me.txtRemarks1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks1.Size = New System.Drawing.Size(264, 48)
        Me.txtRemarks1.TabIndex = 145
        '
        'txtComments1
        '
        Me.txtComments1.BackColor = System.Drawing.SystemColors.Window
        Me.txtComments1.Location = New System.Drawing.Point(448, 380)
        Me.txtComments1.Multiline = True
        Me.txtComments1.Name = "txtComments1"
        Me.txtComments1.ReadOnly = True
        Me.txtComments1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments1.Size = New System.Drawing.Size(264, 48)
        Me.txtComments1.TabIndex = 143
        '
        'txtBankerName1
        '
        Me.txtBankerName1.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankerName1.Location = New System.Drawing.Point(448, 356)
        Me.txtBankerName1.Name = "txtBankerName1"
        Me.txtBankerName1.ReadOnly = True
        Me.txtBankerName1.Size = New System.Drawing.Size(264, 20)
        Me.txtBankerName1.TabIndex = 141
        '
        'txtCardNo
        '
        Me.txtCardNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCardNo.Location = New System.Drawing.Point(448, 332)
        Me.txtCardNo.Name = "txtCardNo"
        Me.txtCardNo.ReadOnly = True
        Me.txtCardNo.Size = New System.Drawing.Size(124, 20)
        Me.txtCardNo.TabIndex = 137
        '
        'txtCardHolder
        '
        Me.txtCardHolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtCardHolder.Location = New System.Drawing.Point(448, 308)
        Me.txtCardHolder.Name = "txtCardHolder"
        Me.txtCardHolder.ReadOnly = True
        Me.txtCardHolder.Size = New System.Drawing.Size(264, 20)
        Me.txtCardHolder.TabIndex = 135
        '
        'txtFollowOper1
        '
        Me.txtFollowOper1.BackColor = System.Drawing.SystemColors.Window
        Me.txtFollowOper1.Location = New System.Drawing.Point(632, 260)
        Me.txtFollowOper1.Name = "txtFollowOper1"
        Me.txtFollowOper1.ReadOnly = True
        Me.txtFollowOper1.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowOper1.TabIndex = 133
        '
        'txtOperator1
        '
        Me.txtOperator1.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperator1.Location = New System.Drawing.Point(448, 260)
        Me.txtOperator1.Name = "txtOperator1"
        Me.txtOperator1.ReadOnly = True
        Me.txtOperator1.Size = New System.Drawing.Size(80, 20)
        Me.txtOperator1.TabIndex = 131
        '
        'txtFollowDate1
        '
        Me.txtFollowDate1.BackColor = System.Drawing.SystemColors.Window
        Me.txtFollowDate1.Location = New System.Drawing.Point(632, 236)
        Me.txtFollowDate1.Name = "txtFollowDate1"
        Me.txtFollowDate1.ReadOnly = True
        Me.txtFollowDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowDate1.TabIndex = 129
        '
        'txtEffDate1
        '
        Me.txtEffDate1.BackColor = System.Drawing.SystemColors.Window
        Me.txtEffDate1.Location = New System.Drawing.Point(448, 236)
        Me.txtEffDate1.Name = "txtEffDate1"
        Me.txtEffDate1.ReadOnly = True
        Me.txtEffDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtEffDate1.TabIndex = 127
        '
        'txtLstMaintDate1
        '
        Me.txtLstMaintDate1.BackColor = System.Drawing.SystemColors.Window
        Me.txtLstMaintDate1.Location = New System.Drawing.Point(632, 212)
        Me.txtLstMaintDate1.Name = "txtLstMaintDate1"
        Me.txtLstMaintDate1.ReadOnly = True
        Me.txtLstMaintDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtLstMaintDate1.TabIndex = 125
        '
        'txtSubmitDate1
        '
        Me.txtSubmitDate1.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubmitDate1.Location = New System.Drawing.Point(448, 212)
        Me.txtSubmitDate1.Name = "txtSubmitDate1"
        Me.txtSubmitDate1.ReadOnly = True
        Me.txtSubmitDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtSubmitDate1.TabIndex = 123
        '
        'txtEndDate1
        '
        Me.txtEndDate1.BackColor = System.Drawing.SystemColors.Window
        Me.txtEndDate1.Location = New System.Drawing.Point(632, 188)
        Me.txtEndDate1.Name = "txtEndDate1"
        Me.txtEndDate1.ReadOnly = True
        Me.txtEndDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtEndDate1.TabIndex = 121
        '
        'txtDrawDate1
        '
        Me.txtDrawDate1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrawDate1.Location = New System.Drawing.Point(448, 188)
        Me.txtDrawDate1.Name = "txtDrawDate1"
        Me.txtDrawDate1.ReadOnly = True
        Me.txtDrawDate1.Size = New System.Drawing.Size(80, 20)
        Me.txtDrawDate1.TabIndex = 119
        '
        'txtLastStatus1
        '
        Me.txtLastStatus1.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastStatus1.Location = New System.Drawing.Point(448, 164)
        Me.txtLastStatus1.Name = "txtLastStatus1"
        Me.txtLastStatus1.ReadOnly = True
        Me.txtLastStatus1.Size = New System.Drawing.Size(264, 20)
        Me.txtLastStatus1.TabIndex = 117
        '
        'txtStatus1
        '
        Me.txtStatus1.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus1.Location = New System.Drawing.Point(448, 140)
        Me.txtStatus1.Name = "txtStatus1"
        Me.txtStatus1.ReadOnly = True
        Me.txtStatus1.Size = New System.Drawing.Size(264, 20)
        Me.txtStatus1.TabIndex = 115
        '
        'txtSeqNo1
        '
        Me.txtSeqNo1.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeqNo1.Location = New System.Drawing.Point(448, 116)
        Me.txtSeqNo1.Name = "txtSeqNo1"
        Me.txtSeqNo1.ReadOnly = True
        Me.txtSeqNo1.Size = New System.Drawing.Size(64, 20)
        Me.txtSeqNo1.TabIndex = 113
        '
        'txtStatusChgDate
        '
        Me.txtStatusChgDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatusChgDate.Location = New System.Drawing.Point(271, 115)
        Me.txtStatusChgDate.Name = "txtStatusChgDate"
        Me.txtStatusChgDate.ReadOnly = True
        Me.txtStatusChgDate.Size = New System.Drawing.Size(80, 20)
        Me.txtStatusChgDate.TabIndex = 111
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.Location = New System.Drawing.Point(87, 533)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReadOnly = True
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(264, 48)
        Me.txtRemarks.TabIndex = 109
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.SystemColors.Window
        Me.txtComments.Location = New System.Drawing.Point(87, 481)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(264, 48)
        Me.txtComments.TabIndex = 107
        '
        'txtBankerName
        '
        Me.txtBankerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankerName.Location = New System.Drawing.Point(86, 26)
        Me.txtBankerName.Name = "txtBankerName"
        Me.txtBankerName.ReadOnly = True
        Me.txtBankerName.Size = New System.Drawing.Size(264, 20)
        Me.txtBankerName.TabIndex = 105
        '
        'txtAC3
        '
        Me.txtAC3.BackColor = System.Drawing.SystemColors.Window
        Me.txtAC3.Location = New System.Drawing.Point(166, 2)
        Me.txtAC3.Name = "txtAC3"
        Me.txtAC3.ReadOnly = True
        Me.txtAC3.Size = New System.Drawing.Size(184, 20)
        Me.txtAC3.TabIndex = 103
        '
        'txtAC2
        '
        Me.txtAC2.Location = New System.Drawing.Point(126, 2)
        Me.txtAC2.Name = "txtAC2"
        Me.txtAC2.Size = New System.Drawing.Size(40, 20)
        Me.txtAC2.TabIndex = 102
        '
        'txtAC1
        '
        Me.txtAC1.Location = New System.Drawing.Point(86, 2)
        Me.txtAC1.Name = "txtAC1"
        Me.txtAC1.Size = New System.Drawing.Size(40, 20)
        Me.txtAC1.TabIndex = 101
        '
        'txtPayorInfo
        '
        Me.txtPayorInfo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayorInfo.Location = New System.Drawing.Point(87, 307)
        Me.txtPayorInfo.Name = "txtPayorInfo"
        Me.txtPayorInfo.ReadOnly = True
        Me.txtPayorInfo.Size = New System.Drawing.Size(264, 20)
        Me.txtPayorInfo.TabIndex = 99
        '
        'txtFollowOper
        '
        Me.txtFollowOper.BackColor = System.Drawing.SystemColors.Window
        Me.txtFollowOper.Location = New System.Drawing.Point(271, 259)
        Me.txtFollowOper.Name = "txtFollowOper"
        Me.txtFollowOper.ReadOnly = True
        Me.txtFollowOper.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowOper.TabIndex = 97
        '
        'txtOperator
        '
        Me.txtOperator.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperator.Location = New System.Drawing.Point(87, 259)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.ReadOnly = True
        Me.txtOperator.Size = New System.Drawing.Size(80, 20)
        Me.txtOperator.TabIndex = 95
        '
        'txtFollowDate
        '
        Me.txtFollowDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtFollowDate.Location = New System.Drawing.Point(271, 235)
        Me.txtFollowDate.Name = "txtFollowDate"
        Me.txtFollowDate.ReadOnly = True
        Me.txtFollowDate.Size = New System.Drawing.Size(80, 20)
        Me.txtFollowDate.TabIndex = 93
        '
        'txtEffDate
        '
        Me.txtEffDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEffDate.Location = New System.Drawing.Point(87, 235)
        Me.txtEffDate.Name = "txtEffDate"
        Me.txtEffDate.ReadOnly = True
        Me.txtEffDate.Size = New System.Drawing.Size(80, 20)
        Me.txtEffDate.TabIndex = 91
        '
        'txtLstMintDate
        '
        Me.txtLstMintDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtLstMintDate.Location = New System.Drawing.Point(271, 211)
        Me.txtLstMintDate.Name = "txtLstMintDate"
        Me.txtLstMintDate.ReadOnly = True
        Me.txtLstMintDate.Size = New System.Drawing.Size(80, 20)
        Me.txtLstMintDate.TabIndex = 89
        '
        'txtSubmitDate
        '
        Me.txtSubmitDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubmitDate.Location = New System.Drawing.Point(87, 211)
        Me.txtSubmitDate.Name = "txtSubmitDate"
        Me.txtSubmitDate.ReadOnly = True
        Me.txtSubmitDate.Size = New System.Drawing.Size(80, 20)
        Me.txtSubmitDate.TabIndex = 87
        '
        'txtEndDate
        '
        Me.txtEndDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEndDate.Location = New System.Drawing.Point(271, 187)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.ReadOnly = True
        Me.txtEndDate.Size = New System.Drawing.Size(80, 20)
        Me.txtEndDate.TabIndex = 85
        '
        'txtDrawDate
        '
        Me.txtDrawDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrawDate.Location = New System.Drawing.Point(87, 187)
        Me.txtDrawDate.Name = "txtDrawDate"
        Me.txtDrawDate.ReadOnly = True
        Me.txtDrawDate.Size = New System.Drawing.Size(80, 20)
        Me.txtDrawDate.TabIndex = 83
        '
        'txtLastStatus
        '
        Me.txtLastStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastStatus.Location = New System.Drawing.Point(87, 163)
        Me.txtLastStatus.Name = "txtLastStatus"
        Me.txtLastStatus.ReadOnly = True
        Me.txtLastStatus.Size = New System.Drawing.Size(264, 20)
        Me.txtLastStatus.TabIndex = 81
        '
        'txtSTatus
        '
        Me.txtSTatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTatus.Location = New System.Drawing.Point(87, 139)
        Me.txtSTatus.Name = "txtSTatus"
        Me.txtSTatus.ReadOnly = True
        Me.txtSTatus.Size = New System.Drawing.Size(264, 20)
        Me.txtSTatus.TabIndex = 79
        '
        'txtSeqNo
        '
        Me.txtSeqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeqNo.Location = New System.Drawing.Point(87, 115)
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.ReadOnly = True
        Me.txtSeqNo.Size = New System.Drawing.Size(64, 20)
        Me.txtSeqNo.TabIndex = 77
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(536, 120)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(85, 13)
        Me.Label18.TabIndex = 146
        Me.Label18.Text = "Status Chg Date"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(364, 436)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(49, 13)
        Me.Label19.TabIndex = 144
        Me.Label19.Text = "Remarks"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(364, 384)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 13)
        Me.Label20.TabIndex = 142
        Me.Label20.Text = "Comments"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(364, 360)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(72, 13)
        Me.Label21.TabIndex = 140
        Me.Label21.Text = "Banker Name"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(364, 336)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(49, 13)
        Me.Label22.TabIndex = 136
        Me.Label22.Text = "Card No."
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(364, 312)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(63, 13)
        Me.Label23.TabIndex = 134
        Me.Label23.Text = "Card Holder"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(364, 264)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(48, 13)
        Me.Label24.TabIndex = 130
        Me.Label24.Text = "Operator"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(536, 240)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(78, 13)
        Me.Label25.TabIndex = 128
        Me.Label25.Text = "Follow-up Date"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(364, 240)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(75, 13)
        Me.Label26.TabIndex = 126
        Me.Label26.Text = "Effective Date"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(536, 216)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(85, 13)
        Me.Label27.TabIndex = 124
        Me.Label27.Text = "Last Maint. Date"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(364, 216)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(65, 13)
        Me.Label28.TabIndex = 122
        Me.Label28.Text = "Submit Date"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(536, 192)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(52, 13)
        Me.Label29.TabIndex = 120
        Me.Label29.Text = "End Date"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(364, 192)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(58, 13)
        Me.Label30.TabIndex = 118
        Me.Label30.Text = "Draw Date"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(364, 168)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(60, 13)
        Me.Label31.TabIndex = 116
        Me.Label31.Text = "Last Status"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(364, 144)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(37, 13)
        Me.Label32.TabIndex = 114
        Me.Label32.Text = "Status"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label33.Location = New System.Drawing.Point(364, 120)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(76, 13)
        Me.Label33.TabIndex = 112
        Me.Label33.Text = "Sequence No."
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(536, 264)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(96, 13)
        Me.Label34.TabIndex = 132
        Me.Label34.Text = "Follow-up Operator"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(175, 119)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 13)
        Me.Label17.TabIndex = 110
        Me.Label17.Text = "Status Chg Date"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 537)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(49, 13)
        Me.Label16.TabIndex = 108
        Me.Label16.Text = "Remarks"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 485)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 13)
        Me.Label15.TabIndex = 106
        Me.Label15.Text = "Comments"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(2, 30)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 13)
        Me.Label14.TabIndex = 104
        Me.Label14.Text = "Banker Name"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(2, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Bank A/C No."
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 311)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 13)
        Me.Label12.TabIndex = 98
        Me.Label12.Text = "Payor Info."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 259)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 13)
        Me.Label10.TabIndex = 94
        Me.Label10.Text = "Operator"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(175, 239)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 13)
        Me.Label9.TabIndex = 92
        Me.Label9.Text = "Follow-up Date"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 235)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 90
        Me.Label8.Text = "Effective Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(175, 215)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 88
        Me.Label7.Text = "Last Maint. Date"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 211)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "Submit Date"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(175, 191)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 84
        Me.Label5.Text = "End Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 187)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "Draw Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 163)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "Last Status"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "Status"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Location = New System.Drawing.Point(3, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 76
        Me.Label1.Text = "Sequence No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.grdCard.Location = New System.Drawing.Point(360, 4)
        Me.grdCard.Name = "grdCard"
        Me.grdCard.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCard.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCard.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCard.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCard.Size = New System.Drawing.Size(352, 104)
        Me.grdCard.TabIndex = 75
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
        Me.grdAC.Location = New System.Drawing.Point(4, 4)
        Me.grdAC.Name = "grdAC"
        Me.grdAC.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAC.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAC.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAC.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAC.Size = New System.Drawing.Size(352, 104)
        Me.grdAC.TabIndex = 74
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(175, 263)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 13)
        Me.Label11.TabIndex = 96
        Me.Label11.Text = "Follow-up Operator"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExpDt
        '
        Me.txtExpDt.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpDt.Location = New System.Drawing.Point(656, 332)
        Me.txtExpDt.Name = "txtExpDt"
        Me.txtExpDt.ReadOnly = True
        Me.txtExpDt.Size = New System.Drawing.Size(56, 20)
        Me.txtExpDt.TabIndex = 148
        '
        'txtIniRej
        '
        Me.txtIniRej.BackColor = System.Drawing.SystemColors.Window
        Me.txtIniRej.Location = New System.Drawing.Point(448, 284)
        Me.txtIniRej.Name = "txtIniRej"
        Me.txtIniRej.ReadOnly = True
        Me.txtIniRej.Size = New System.Drawing.Size(80, 20)
        Me.txtIniRej.TabIndex = 149
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(364, 288)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(65, 13)
        Me.Label35.TabIndex = 150
        Me.Label35.Text = "Initial Reject"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(588, 336)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(61, 13)
        Me.Label36.TabIndex = 151
        Me.Label36.Text = "Expiry Date"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(2, 54)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(77, 13)
        Me.Label37.TabIndex = 152
        Me.Label37.Text = "Simplified DDA"
        '
        'txtSimpDDA
        '
        Me.txtSimpDDA.Location = New System.Drawing.Point(86, 50)
        Me.txtSimpDDA.Name = "txtSimpDDA"
        Me.txtSimpDDA.Size = New System.Drawing.Size(24, 20)
        Me.txtSimpDDA.TabIndex = 153
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(118, 78)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(56, 13)
        Me.Label38.TabIndex = 154
        Me.Label38.Text = "Debit Limit"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(2, 78)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(45, 13)
        Me.Label39.TabIndex = 155
        Me.Label39.Text = "ID Type"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(130, 54)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(48, 13)
        Me.Label40.TabIndex = 156
        Me.Label40.Text = "Payer ID"
        '
        'txtDebitLim
        '
        Me.txtDebitLim.Location = New System.Drawing.Point(182, 74)
        Me.txtDebitLim.Name = "txtDebitLim"
        Me.txtDebitLim.Size = New System.Drawing.Size(100, 20)
        Me.txtDebitLim.TabIndex = 157
        '
        'txtIDType
        '
        Me.txtIDType.Location = New System.Drawing.Point(86, 74)
        Me.txtIDType.Name = "txtIDType"
        Me.txtIDType.Size = New System.Drawing.Size(24, 20)
        Me.txtIDType.TabIndex = 158
        '
        'txtPayerID
        '
        Me.txtPayerID.Location = New System.Drawing.Point(182, 50)
        Me.txtPayerID.Name = "txtPayerID"
        Me.txtPayerID.Size = New System.Drawing.Size(168, 20)
        Me.txtPayerID.TabIndex = 159
        '
        'txtRefNo
        '
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.Location = New System.Drawing.Point(87, 283)
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReadOnly = True
        Me.txtRefNo.Size = New System.Drawing.Size(264, 20)
        Me.txtRefNo.TabIndex = 161
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(3, 287)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(47, 13)
        Me.Label41.TabIndex = 160
        Me.Label41.Text = "Ref. No."
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCUPDDA
        '
        Me.pnlCUPDDA.Controls.Add(Me.txtCUPPayerIdType)
        Me.pnlCUPDDA.Controls.Add(Me.txtCUPBankName)
        Me.pnlCUPDDA.Controls.Add(Me.Label64)
        Me.pnlCUPDDA.Controls.Add(Me.txtCUPBranch)
        Me.pnlCUPDDA.Controls.Add(Me.txtCUPCity)
        Me.pnlCUPDDA.Controls.Add(Me.Label46)
        Me.pnlCUPDDA.Controls.Add(Me.txtCUPProvince)
        Me.pnlCUPDDA.Controls.Add(Me.Label45)
        Me.pnlCUPDDA.Controls.Add(Me.txtCUPPayerId)
        Me.pnlCUPDDA.Controls.Add(Me.Label43)
        Me.pnlCUPDDA.Controls.Add(Me.Label44)
        Me.pnlCUPDDA.Controls.Add(Me.txtCUPAccNum)
        Me.pnlCUPDDA.Controls.Add(Me.Label50)
        Me.pnlCUPDDA.Controls.Add(Me.Label51)
        Me.pnlCUPDDA.Location = New System.Drawing.Point(0, 332)
        Me.pnlCUPDDA.Name = "pnlCUPDDA"
        Me.pnlCUPDDA.Size = New System.Drawing.Size(354, 149)
        Me.pnlCUPDDA.TabIndex = 163
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(3, 128)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(69, 13)
        Me.Label64.TabIndex = 168
        Me.Label64.Text = "Bank Branch"
        '
        'txtCUPBranch
        '
        Me.txtCUPBranch.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUPBranch.Location = New System.Drawing.Point(87, 125)
        Me.txtCUPBranch.Name = "txtCUPBranch"
        Me.txtCUPBranch.ReadOnly = True
        Me.txtCUPBranch.Size = New System.Drawing.Size(263, 20)
        Me.txtCUPBranch.TabIndex = 167
        '
        'txtCUPCity
        '
        Me.txtCUPCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUPCity.Location = New System.Drawing.Point(239, 100)
        Me.txtCUPCity.Name = "txtCUPCity"
        Me.txtCUPCity.ReadOnly = True
        Me.txtCUPCity.Size = New System.Drawing.Size(111, 20)
        Me.txtCUPCity.TabIndex = 166
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(185, 103)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(24, 13)
        Me.Label46.TabIndex = 165
        Me.Label46.Text = "City"
        '
        'txtCUPProvince
        '
        Me.txtCUPProvince.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUPProvince.Location = New System.Drawing.Point(87, 100)
        Me.txtCUPProvince.Name = "txtCUPProvince"
        Me.txtCUPProvince.ReadOnly = True
        Me.txtCUPProvince.Size = New System.Drawing.Size(92, 20)
        Me.txtCUPProvince.TabIndex = 164
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(3, 103)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(49, 13)
        Me.Label45.TabIndex = 163
        Me.Label45.Text = "Province"
        '
        'txtCUPPayerId
        '
        Me.txtCUPPayerId.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUPPayerId.Location = New System.Drawing.Point(87, 76)
        Me.txtCUPPayerId.Name = "txtCUPPayerId"
        Me.txtCUPPayerId.ReadOnly = True
        Me.txtCUPPayerId.Size = New System.Drawing.Size(263, 20)
        Me.txtCUPPayerId.TabIndex = 159
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(3, 79)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(48, 13)
        Me.Label43.TabIndex = 156
        Me.Label43.Text = "Payer ID"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(3, 53)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(45, 13)
        Me.Label44.TabIndex = 155
        Me.Label44.Text = "ID Type"
        '
        'txtCUPAccNum
        '
        Me.txtCUPAccNum.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUPAccNum.Location = New System.Drawing.Point(87, 2)
        Me.txtCUPAccNum.Name = "txtCUPAccNum"
        Me.txtCUPAccNum.ReadOnly = True
        Me.txtCUPAccNum.Size = New System.Drawing.Size(264, 20)
        Me.txtCUPAccNum.TabIndex = 103
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(3, 30)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(72, 13)
        Me.Label50.TabIndex = 104
        Me.Label50.Text = "Banker Name"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(3, 6)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(74, 13)
        Me.Label51.TabIndex = 100
        Me.Label51.Text = "Bank A/C No."
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDDAR
        '
        Me.pnlDDAR.Controls.Add(Me.Label13)
        Me.pnlDDAR.Controls.Add(Me.Label14)
        Me.pnlDDAR.Controls.Add(Me.txtAC1)
        Me.pnlDDAR.Controls.Add(Me.txtAC2)
        Me.pnlDDAR.Controls.Add(Me.txtPayerID)
        Me.pnlDDAR.Controls.Add(Me.txtAC3)
        Me.pnlDDAR.Controls.Add(Me.txtBankerName)
        Me.pnlDDAR.Controls.Add(Me.txtIDType)
        Me.pnlDDAR.Controls.Add(Me.Label37)
        Me.pnlDDAR.Controls.Add(Me.txtSimpDDA)
        Me.pnlDDAR.Controls.Add(Me.txtDebitLim)
        Me.pnlDDAR.Controls.Add(Me.Label38)
        Me.pnlDDAR.Controls.Add(Me.Label39)
        Me.pnlDDAR.Controls.Add(Me.Label40)
        Me.pnlDDAR.Location = New System.Drawing.Point(1, 332)
        Me.pnlDDAR.Name = "pnlDDAR"
        Me.pnlDDAR.Size = New System.Drawing.Size(354, 97)
        Me.pnlDDAR.TabIndex = 164
        '
        'txtCUPBankName
        '
        Me.txtCUPBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUPBankName.Location = New System.Drawing.Point(86, 27)
        Me.txtCUPBankName.Name = "txtCUPBankName"
        Me.txtCUPBankName.ReadOnly = True
        Me.txtCUPBankName.Size = New System.Drawing.Size(264, 20)
        Me.txtCUPBankName.TabIndex = 169
        '
        'txtCUPPayerIdType
        '
        Me.txtCUPPayerIdType.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUPPayerIdType.Location = New System.Drawing.Point(86, 51)
        Me.txtCUPPayerIdType.Name = "txtCUPPayerIdType"
        Me.txtCUPPayerIdType.ReadOnly = True
        Me.txtCUPPayerIdType.Size = New System.Drawing.Size(264, 20)
        Me.txtCUPPayerIdType.TabIndex = 170
        '
        'DDACCDR
        '
        Me.Controls.Add(Me.txtRefNo)
        Me.Controls.Add(Me.pnlCUPDDA)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.txtIniRej)
        Me.Controls.Add(Me.txtExpDt)
        Me.Controls.Add(Me.txtStatusChgDate1)
        Me.Controls.Add(Me.txtRemarks1)
        Me.Controls.Add(Me.txtComments1)
        Me.Controls.Add(Me.txtBankerName1)
        Me.Controls.Add(Me.txtStatusChgDate)
        Me.Controls.Add(Me.txtCardNo)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.txtCardHolder)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.txtFollowOper1)
        Me.Controls.Add(Me.txtOperator1)
        Me.Controls.Add(Me.txtFollowDate1)
        Me.Controls.Add(Me.txtEffDate1)
        Me.Controls.Add(Me.txtLstMaintDate1)
        Me.Controls.Add(Me.txtPayorInfo)
        Me.Controls.Add(Me.txtSubmitDate1)
        Me.Controls.Add(Me.txtFollowOper)
        Me.Controls.Add(Me.txtEndDate1)
        Me.Controls.Add(Me.txtOperator)
        Me.Controls.Add(Me.txtDrawDate1)
        Me.Controls.Add(Me.txtFollowDate)
        Me.Controls.Add(Me.txtLastStatus1)
        Me.Controls.Add(Me.txtEffDate)
        Me.Controls.Add(Me.txtStatus1)
        Me.Controls.Add(Me.txtLstMintDate)
        Me.Controls.Add(Me.txtSeqNo1)
        Me.Controls.Add(Me.txtSubmitDate)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtEndDate)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtDrawDate)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtLastStatus)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtSTatus)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtSeqNo)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.grdCard)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.grdAC)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.pnlDDAR)
        Me.Name = "DDACCDR"
        Me.Size = New System.Drawing.Size(724, 589)
        CType(Me.grdCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCUPDDA.ResumeLayout(False)
        Me.pnlCUPDDA.PerformLayout()
        Me.pnlDDAR.ResumeLayout(False)
        Me.pnlDDAR.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property srcDTDDACCDR(ByVal dt As DataTable)
        Set(ByVal Value)
            If Not Value Is Nothing Then
                ds.Tables.Add(Value)
            End If
            If Not dt Is Nothing Then
                ds.Tables.Add(dt)
            End If
            Call buildUI()
        End Set
    End Property

    'Add by ITDSCH on 2016-12-15 Begin
    Private _dtPriv As DataTable
    Public Property dtPriv() As DataTable
        Get
            Return _dtPriv
        End Get
        Set(ByVal value As DataTable)
            _dtPriv = value
        End Set
    End Property

    Private _strUPSMenuCtrl As String
    Public Property strUPSMenuCtrl() As String
        Get
            Return _strUPSMenuCtrl
        End Get
        Set(ByVal value As String)
            _strUPSMenuCtrl = value
        End Set
    End Property
    'Add by ITDSCH on 2016-12-15 End

    Private Sub buildUI()
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable
        Dim strErr As String

        'Try
        '    strSQL = "Select * from CCDRStatusCodes; "
        '    strSQL &= "Select * from DDAStatusCodes"

        '    sqlconnect.ConnectionString = strCIWConn
        '    sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        '    sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        '    sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        '    sqlda.TableMappings.Add("CCDRStatusCodes1", "DDAStatusCodes")
        '    sqlda.Fill(ds, "CCDRStatusCodes")

        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'End Try

        'sqlconnect.Dispose()

        Using wsCRS As New CRSWS.CRSWS()

            wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
            If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
                wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
            End If
            'wsCRS.DBSOAPHeaderValue = GetCRSWSDBHeader()
            'wsCRS.MQSOAPHeaderValue = GetCRSWSMQHeader()

            wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsCRS.Timeout = 10000000

            ds = wsCRS.getMacauDDACCDRStatusCode(getCompanyCode(), getEnvCode(), strErr)
            ' For Each _item As String In wsCRS.CheckUserPermission(gobjDBHeader.UserID, strErr)


            'Next

        End Using


        Dim relCCDRel As New Data.DataRelation("CCDRel", ds.Tables("CCDRStatusCodes").Columns("CCDRStatusCode"), _
            ds.Tables("CCDR").Columns("CCDRStatus"), True)
        Dim relLCCDRel As New Data.DataRelation("LCCDRel", ds.Tables("CCDRStatusCodes").Columns("CCDRStatusCode"), _
            ds.Tables("CCDR").Columns("CCDRLastStatus"), True)

        Dim relDDARel As New Data.DataRelation("DDARel", ds.Tables("DDAStatusCodes").Columns("DDAStatusCode"), _
            ds.Tables("DDAR").Columns("DDAStatus"), True)
        Dim relLDDARel As New Data.DataRelation("LDDARel", ds.Tables("DDAStatusCodes").Columns("DDAStatusCode"), _
            ds.Tables("DDAR").Columns("DDALastStatus"), True)

        Try
            ds.Relations.Add(relCCDRel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relLCCDRel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relDDARel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relLDDARel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        If ds.Tables("DDAR").Rows.Count > 0 Then

            LoadCodeTables()

            ds.Tables.Add(dtCUPBankList.Copy())
            ds.Tables.Add(dtCUPPayerIdTypeList.Copy())

            Dim relCUPBankCode As New Data.DataRelation("Rel_CUPBankCode_DDAR", ds.Tables("CUPBanks").Columns("BankCode"), ds.Tables("DDAR").Columns("DDACUPBankCode"), False)
            ds.Relations.Add(relCUPBankCode)

            Dim relCUPPayerIdType As New Data.DataRelation("Rel_CUPPayerIdType_DDAR", ds.Tables("CUPPayerIdTypes").Columns("code_value"), ds.Tables("DDAR").Columns("DDAPAYERIDTYPE"), False)
            ds.Relations.Add(relCUPPayerIdType)


            With ds.Tables("DDAR").Rows(0)

                Dim ts As New clsDataGridTableStyle
                Dim cs As DataGridTextBoxColumn

                cs = New DataGridTextBoxColumn
                cs.Width = 40
                cs.MappingName = "DDACollectPath"
                cs.HeaderText = "Type"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "DDABankAccountNo"
                cs.HeaderText = "A/C No."
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "DDABankerName"
                cs.HeaderText = "Bank"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "DDACUPAccNum"
                cs.HeaderText = "CUP A/C No."
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "DDACUPBankName"
                cs.HeaderText = "CUP Bank"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "DDAR"
                grdAC.TableStyles.Add(ts)

                ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                ''CRS 7x24 Changes - Start
                'If ExternalUser Then
                '    MaskDTsource(ds.Tables("DDAR"), "DDABankAccountNo", MaskData.BANK_ACCOUNT_NO)
                '    MaskDTsource(ds.Tables("DDAR"), "DDACUPAccNum", MaskData.BANK_ACCOUNT_NO)

                '    grdAC.DataSource = ds.Tables("DDAR")
                'Else
                '    grdAC.DataSource = ds.Tables("DDAR")
                'End If
                ''CRS 7x24 Changes - End
                grdAC.DataSource = ds.Tables("DDAR")

                grdAC.AllowDrop = False
                grdAC.ReadOnly = True

            End With
            bm = Me.BindingContext(ds.Tables("DDAR"))
            ds.Tables("DDAR").DefaultView.Sort = "DDASeqNo DESC"

            txtSeqNo.DataBindings.Add("Text", ds.Tables("DDAR"), "DDASeqNo")
            'txtStatusChgDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAStsChgDate")
            Dim bStsCDate As Binding = New Binding("Text", ds.Tables("DDAR"), "DDAStsChgDate")
            Me.txtStatusChgDate.DataBindings.Add(bStsCDate)
            AddHandler bStsCDate.Format, AddressOf FormatDate

            txtDrawDate.DataBindings.Add("Text", ds.Tables("DDAR"), "DDADrawDate")
            'txtEndDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAEndDate")
            Dim bEndDate As Binding = New Binding("Text", ds.Tables("DDAR"), "DDAEndDate")
            Me.txtEndDate.DataBindings.Add(bEndDate)
            AddHandler bEndDate.Format, AddressOf FormatDate

            'txtSubmitDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDASubmitDate")
            Dim bSubDate As Binding = New Binding("Text", ds.Tables("DDAR"), "DDASubmitDate")
            Me.txtSubmitDate.DataBindings.Add(bSubDate)
            AddHandler bSubDate.Format, AddressOf FormatDate

            'txtLstMintDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDALastMaintDate")
            Dim bLMDate As Binding = New Binding("Text", ds.Tables("DDAR"), "DDALastMaintDate")
            Me.txtLstMintDate.DataBindings.Add(bLMDate)
            AddHandler bLMDate.Format, AddressOf FormatDate

            'txtEffDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAEffectiveDate")
            Dim bEffDate As Binding = New Binding("Text", ds.Tables("DDAR"), "DDAEffectiveDate")
            Me.txtEffDate.DataBindings.Add(bEffDate)
            AddHandler bEffDate.Format, AddressOf FormatDate

            'txtFollowDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAFollowUpDate")
            Dim bFolDate As Binding = New Binding("Text", ds.Tables("DDAR"), "DDAFollowUpDate")
            Me.txtFollowDate.DataBindings.Add(bFolDate)
            AddHandler bFolDate.Format, AddressOf FormatDate

            txtOperator.DataBindings.Add("Text", ds.Tables("DDAR"), "DDAOperator")
            txtFollowOper.DataBindings.Add("Text", ds.Tables("DDAR"), "DDAFollowUpOpr")
            txtPayorInfo.DataBindings.Add("Text", ds.Tables("DDAR"), "DDAPayorInfo")
            txtAC1.DataBindings.Add("Text", ds.Tables("DDAR"), "DDABankCode")
            txtAC2.DataBindings.Add("Text", ds.Tables("DDAR"), "DDABranchCode")
            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            'If ExternalUser Then
            '    txtAC3.DataBindings.Add("Text", MaskDTsource(ds.Tables("DDAR"), "DDABankAccountNo", MaskData.BANK_ACCOUNT_NO), "DDABankAccountNo")
            'Else
            '    txtAC3.DataBindings.Add("Text", ds.Tables("DDAR"), "DDABankAccountNo")
            'End If
            ''CRS 7x24 Changes - End
            txtAC3.DataBindings.Add("Text", ds.Tables("DDAR"), "DDABankAccountNo")
            txtBankerName.DataBindings.Add("Text", ds.Tables("DDAR"), "DDABankerName")
            txtComments.DataBindings.Add("Text", ds.Tables("DDAR"), "DDAComments")
            txtRemarks.DataBindings.Add("Text", ds.Tables("DDAR"), "DDARemarks")
            txtSimpDDA.DataBindings.Add("Text", ds.Tables("DDAR"), "DDASIMPIND")
            txtPayerID.DataBindings.Add("Text", ds.Tables("DDAR"), "DDAPAYERID")
            txtIDType.DataBindings.Add("Text", ds.Tables("DDAR"), "DDAPAYERIDTYPE")
            txtDebitLim.DataBindings.Add("Text", ds.Tables("DDAR"), "DDADEBITLIMIT")
            txtRefNo.DataBindings.Add("Text", ds.Tables("DDAR"), "DDARefNo")

            txtCUPProvince.DataBindings.Add("Text", ds.Tables("DDAR"), "DDACUPProvince")
            txtCUPCity.DataBindings.Add("Text", ds.Tables("DDAR"), "DDACUPCity")
            txtCUPBranch.DataBindings.Add("Text", ds.Tables("DDAR"), "DDACUPBranch")
            txtCUPAccNum.DataBindings.Add("Text", ds.Tables("DDAR"), "DDACUPAccNum")
            txtCUPPayerId.DataBindings.Add("Text", ds.Tables("DDAR"), "DDAPAYERID")

            ' Fill in CUP Bank Name of the datagrid
            Dim drBank As DataRow()

            For Each drDDAR As DataRow In ds.Tables("DDAR").Rows
                If drDDAR("DDACUPBankCode").ToString().Trim() <> "" Then
                    drBank = dtCUPBankList.Select("BankCode='" & drDDAR("DDACUPBankCode").ToString().Trim() & "'")
                    If drBank.Length > 0 Then
                        drDDAR("DDACUPBankName") = drBank(0)("BankName").ToString().Trim()
                    End If
                End If
            Next

            ' CUP DDA - End


            Call UpdatePT(1)

        End If

        If ds.Tables("CCDR").Rows.Count > 0 Then
            With ds.Tables("CCDR").Rows(0)

                Dim ts As New clsDataGridTableStyle
                Dim cs As DataGridTextBoxColumn

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "CCDRCardNumber"
                cs.HeaderText = "Card No."
                cs.NullText = gNULLText
                cs.Format = gCCFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "CCDRCardHolderName"
                cs.HeaderText = "Card Holder Name"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "CCDR"
                grdCard.TableStyles.Add(ts)

                'Update by ITDSCH on 2016-12-15 Begin
                'CRS 7x24 Changes - Start
                'If ExternalUser Then
                '    grdCard.DataSource = MaskDTsource(ds.Tables("CCDR"), "CCDRCardNumber", MaskData.CREDIT_CARAD_NO)
                'Else
                '    grdCard.DataSource = ds.Tables("CCDR")
                'End If
                'CRS 7x24 Changes - End

                If CheckUPSAccess("CRS_PCIDSS_CREDITCARD") Then
                    grdCard.DataSource = ds.Tables("CCDR")
                Else
                    grdCard.DataSource = MaskDTCreditCard(ds.Tables("CCDR"), "CCDRCardNumber", MaskData.CREDIT_CARAD_NO)
                End If

                'Update by ITDSCH on 2016-12-15 End

                grdCard.AllowDrop = False
                grdCard.ReadOnly = True

            End With
            bm2 = Me.BindingContext(ds.Tables("CCDR"))
            ds.Tables("CCDR").DefaultView.Sort = "CCDRSeqNo DESC"

            txtSeqNo1.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRSeqNo")
            'txtStatusChgDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAStsChgDate")
            Dim bStsCDate As Binding = New Binding("Text", ds.Tables("CCDR"), "CCDRStsChgDate")
            Me.txtStatusChgDate1.DataBindings.Add(bStsCDate)
            AddHandler bStsCDate.Format, AddressOf FormatDate

            txtDrawDate1.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRDrawDate")
            'txtEndDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAEndDate")
            Dim bEndDate As Binding = New Binding("Text", ds.Tables("CCDR"), "CCDREndDate")
            Me.txtEndDate1.DataBindings.Add(bEndDate)
            AddHandler bEndDate.Format, AddressOf FormatDate

            'txtSubmitDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDASubmitDate")
            Dim bSubDate As Binding = New Binding("Text", ds.Tables("CCDR"), "CCDRSubmitDate")
            Me.txtSubmitDate1.DataBindings.Add(bSubDate)
            AddHandler bSubDate.Format, AddressOf FormatDate

            'txtLstMintDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDALastMaintDate")
            Dim bLMDate As Binding = New Binding("Text", ds.Tables("CCDR"), "CCDRLastMaintDate")
            Me.txtLstMaintDate1.DataBindings.Add(bLMDate)
            AddHandler bLMDate.Format, AddressOf FormatDate

            'txtEffDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAEffectiveDate")
            Dim bEffDate As Binding = New Binding("Text", ds.Tables("CCDR"), "CCDREffectiveDate")
            Me.txtEffDate1.DataBindings.Add(bEffDate)
            AddHandler bEffDate.Format, AddressOf FormatDate

            'txtFollowDate.DataBindings.Add("Text", ds.Tables("DDA"), "DDAFollowUpDate")
            Dim bFolDate As Binding = New Binding("Text", ds.Tables("CCDR"), "CCDRFollowUpDate")
            Me.txtFollowDate1.DataBindings.Add(bFolDate)
            AddHandler bFolDate.Format, AddressOf FormatDate

            txtOperator1.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDROperator")
            txtFollowOper1.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRFollowUpOpr")
            txtCardHolder.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRCardHolderName")    '** Max 15 characters
            txtComments1.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRComments")
            txtRemarks1.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRRemarks")

            'Update by ITDSCH on 2016-12-15 Begin
            'CRS 7x24 Changes - Start
            'If ExternalUser Then
            'txtCardNo.DataBindings.Add("Text", MaskDTsource(ds.Tables("CCDR"), "CCDRCardNumber", MaskData.CREDIT_CARAD_NO), "CCDRCardNumber")
            'Else
            '    txtCardNo.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRCardNumber")
            'End If
            'CRS 7x24 Changes - End

            If CheckUPSAccess("CRS_PCIDSS_CREDITCARD") Then
                txtCardNo.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRCardNumber")
            Else
                txtCardNo.DataBindings.Add("Text", MaskDTCreditCard(ds.Tables("CCDR"), "CCDRCardNumber", MaskData.CREDIT_CARAD_NO), "CCDRCardNumber")
            End If
            'Update by ITDSCH on 2016-12-15 End

            txtIniRej.DataBindings.Add("Text", ds.Tables("CCDR"), "CCDRIniRej")

            Call UpdatePT(2)

        End If

    End Sub

    Private Sub grdAC_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAC.CurrentCellChanged
        Call UpdatePT(1)
    End Sub

    Private Sub grdCard_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCard.CurrentCellChanged
        Call UpdatePT(2)
    End Sub

    Private Sub UpdatePT(ByVal intType As Integer)

        Dim drI As DataRow
        If intType = 1 Then
            drI = CType(bm.Current, DataRowView).Row()
            If Not drI Is Nothing Then
                txtSTatus.Text = GetRelationValue(drI, "DDARel", "DDAStatusDesc")
                txtLastStatus.Text = GetRelationValue(drI, "LDDARel", "DDAStatusDesc")
                txtCUPBankName.Text = GetRelationValue(drI, "Rel_CUPBankCode_DDAR", "BankDesc")
                txtCUPPayerIdType.Text = GetRelationValue(drI, "Rel_CUPPayerIdType_DDAR", "description")
            End If

            ' Display CUP or DDA
            If drI("DDACollectPath").ToString() = DDAIND_CUP Then
                pnlCUPDDA.Visible = True
                pnlDDAR.Visible = False
            ElseIf drI("DDACollectPath").ToString() = DDAIND_HSBCNET Then
                pnlCUPDDA.Visible = False
                pnlDDAR.Visible = True
            End If

        Else
            drI = CType(bm2.Current, DataRowView).Row()
            If Not drI Is Nothing Then
                txtStatus1.Text = GetRelationValue(drI, "CCDRel", "CCDRStatusDesc")
                txtLastStatus1.Text = GetRelationValue(drI, "LCCDRel", "CCDRStatusDesc")
                txtBankerName1.Text = GetCCDRBanker(drI.Item("CCDRCardNumber"))
                'txtCardNo.Text = drI.Item("CCDRCardNumber")
                txtExpDt.Text = Microsoft.VisualBasic.Strings.Left(drI.Item("CCDRExpiryDate"), 2) & "/" & Microsoft.VisualBasic.Strings.Right(drI.Item("CCDRExpiryDate"), 2)
            End If
        End If

    End Sub

    Private Sub grdAC_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAC.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAC.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAC.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAC.Select(hti.Row)
        End If
    End Sub

    Private Sub grdCard_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCard.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdCard.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdCard.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdCard.Select(hti.Row)
        End If
    End Sub

    Private dtCUPBankList As DataTable = Nothing
    Private dtCUPPayerIdTypeList As DataTable = Nothing

    Private Sub LoadCodeTables()

        Dim dsCUPBankList As New DataSet
        Dim dsCUPPayerIdType As New DataSet
        Dim strErr As String = Nothing

        Using wsCCR As New CCRWS.Service()
            wsCCR.DBSOAPHeaderValue = GetCCRWSDBHeader()
            wsCCR.MQSOAPHeaderValue = GetCCRWSMQHeader()
            wsCCR.Url = Utility.Utility.GetWebServiceURL("CCRWS", gobjDBHeader, gobjMQQueHeader)
            wsCCR.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsCCR.Timeout = 10000000

            If dtCUPBankList Is Nothing Then
                If Not wsCCR.GetCUPBankList("", "", dsCUPBankList, strErr) Then
                    Throw New Exception("Failed to get CUP Bank List." & vbCrLf & strErr)
                End If

                dtCUPBankList = dsCUPBankList.Tables(0)
                dtCUPBankList.TableName = "CUPBanks"

            End If


            If dtCUPPayerIdTypeList Is Nothing Then
                If Not wsCCR.GetCUPPayerIDType(dsCUPPayerIdType, strErr) Then
                    Throw New Exception("Failed to get CUP Payer ID Type List." & vbCrLf & strErr)
                End If

                dtCUPPayerIdTypeList = dsCUPPayerIdType.Tables(0)
                dtCUPPayerIdTypeList.TableName = "CUPPayerIdTypes"

            End If


        End Using

    End Sub

End Class
