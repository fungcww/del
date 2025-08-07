'****************************************************************
' Description : Search GI customer
' Date		  : 7/30/2007
' Author	  : Eric Shu (ES01)
'******************************************************************
' Description : Refactor clsJSONBusinessObject to CRS_Util
' Date		  : 11 Nov 2016
' Author	  : Kay Tsang KT20161111
'******************************************************************
' Description : Customer Level Search Issue
' Display policy for all IdCards instead of just CustomerId
' Distinguish  all  displayed Service log base on whether the customer contains an agentcode field to filter
' Date		  : 13 Sep 2023
' Author	  : oliver ou 22834
'******************************************************************
' Description :
' CRS Enhancement(General Enhance Ph4) Point A-10
' 1. Customer alert maintenance
' 2. Enable boardcast prompt up message under customer level + all policies under same customer (PH Or PI)
' Date		  : 1 Nov 2023
' Author	  : Oliver Ou 222834
'******************************************************************
Imports System.Data.SqlClient
Imports System.Collections.Specialized
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports System.IO
Imports System.Configuration
Imports Microsoft.Win32

Public Class frmCustomer_Asur
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Dim dtCust_Asur, dtCust_Ext_Asur As DataTable

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If Not CheckUPSAccess("Service Log") Then
            tcCustomer.TabPages.Remove(tabServiceLog)
            tcCustomer.SelectedTab = Me.tabPolicyAc
        End If
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
    Private ReadOnly STAT_TABS = {"tabPolicyAc"}
    Private ReadOnly ASUR_TABS = {"tabAsurPersonal", "tabAsurServiceLog"}

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents tcCustomer As System.Windows.Forms.TabControl
    Friend WithEvents tabPolicyAc As System.Windows.Forms.TabPage
    Friend WithEvents tabServiceLog As System.Windows.Forms.TabPage
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents grdPolicy As System.Windows.Forms.DataGrid
    Friend WithEvents UclServiceLog1 As CS2005.uclServiceLog_Asur
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtChiName As System.Windows.Forms.TextBox
    Friend WithEvents txtCoName As System.Windows.Forms.TextBox
    Friend WithEvents txtCCoName As System.Windows.Forms.TextBox
    Friend WithEvents tabAddress As System.Windows.Forms.TabPage
    Friend WithEvents AddressSelect1 As CS2005.AddressSelect
    Friend WithEvents tabGI As System.Windows.Forms.TabPage
    Friend WithEvents tabSurvey As System.Windows.Forms.TabPage
    Friend WithEvents tabCampaign As System.Windows.Forms.TabPage
    Friend WithEvents tabMisc As System.Windows.Forms.TabPage
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents UclCampaignTracking1 As CS2005.uclCampaignTracking
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents custListView As System.Windows.Forms.ListView
    Friend WithEvents cmdSDetails As System.Windows.Forms.Button
    Friend WithEvents UclSurvey1 As CS2005.uclSurvey
    Friend WithEvents campListView As System.Windows.Forms.ListView
    Friend WithEvents UclAdditional1 As CS2005.uclAdditional
    Friend WithEvents UclGI1 As CS2005.uclGI
    Friend WithEvents UclCustomerRel1 As CS2005.uclCustomerRel
    Friend WithEvents tabRel As System.Windows.Forms.TabPage
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdEBPolicy As System.Windows.Forms.DataGrid
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdGIPolicy As System.Windows.Forms.DataGrid
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grdMPFPolicy As System.Windows.Forms.DataGrid
    Friend WithEvents tabMPF As System.Windows.Forms.TabPage
    Friend WithEvents UclMPF1 As CS2005.uclMPF
    Friend WithEvents tabPersonal As System.Windows.Forms.TabPage
    Friend WithEvents Personal1 As CS2005.Personal_Asur
    Friend WithEvents lblYearlyAggregateCashValue As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblYearlyAggregateCashValueAsur As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtRemind As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents grdAsurPolicy As System.Windows.Forms.DataGrid
    Friend WithEvents tabAsurPersonal As System.Windows.Forms.TabPage
    Friend WithEvents Personal_Asur1 As CS2005.uclPersonal_Asur_WorkAround
    Friend WithEvents tabAsurServiceLog As System.Windows.Forms.TabPage
    Friend WithEvents UclServiceLog2 As CS2005.uclServiceLog_Asur_WorkAround
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.tcCustomer = New System.Windows.Forms.TabControl()
        Me.tabPolicyAc = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtRemind = New System.Windows.Forms.TextBox()
        Me.grdAsurPolicy = New System.Windows.Forms.DataGrid()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblYearlyAggregateCashValue = New System.Windows.Forms.Label()
        Me.lblYearlyAggregateCashValueAsur = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.grdMPFPolicy = New System.Windows.Forms.DataGrid()
        Me.grdGIPolicy = New System.Windows.Forms.DataGrid()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdEBPolicy = New System.Windows.Forms.DataGrid()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdPolicy = New System.Windows.Forms.DataGrid()
        Me.txtCCoName = New System.Windows.Forms.TextBox()
        Me.txtCoName = New System.Windows.Forms.TextBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtChiName = New System.Windows.Forms.TextBox()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.tabMPF = New System.Windows.Forms.TabPage()
        Me.tabPersonal = New System.Windows.Forms.TabPage()
        Me.tabAddress = New System.Windows.Forms.TabPage()
        Me.tabSurvey = New System.Windows.Forms.TabPage()
        Me.tabCampaign = New System.Windows.Forms.TabPage()
        Me.campListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabMisc = New System.Windows.Forms.TabPage()
        Me.tabServiceLog = New System.Windows.Forms.TabPage()
        Me.tabRel = New System.Windows.Forms.TabPage()
        Me.tabAsurPersonal = New System.Windows.Forms.TabPage()
        Me.tabAsurServiceLog = New System.Windows.Forms.TabPage()
        Me.tabGI = New System.Windows.Forms.TabPage()
        Me.cmdSDetails = New System.Windows.Forms.Button()
        Me.custListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label20 = New System.Windows.Forms.Label()
        Me.UclMPF1 = New CS2005.uclMPF()
        Me.Personal1 = New CS2005.Personal_Asur()
        Me.AddressSelect1 = New CS2005.AddressSelect()
        Me.UclSurvey1 = New CS2005.uclSurvey()
        Me.UclCampaignTracking1 = New CS2005.uclCampaignTracking()
        Me.UclAdditional1 = New CS2005.uclAdditional()
        Me.UclServiceLog1 = New CS2005.uclServiceLog_Asur()
        Me.UclCustomerRel1 = New CS2005.uclCustomerRel()
        Me.Personal_Asur1 = New CS2005.uclPersonal_Asur_WorkAround()
        Me.UclServiceLog2 = New CS2005.uclServiceLog_Asur_WorkAround()
        Me.UclGI1 = New CS2005.uclGI()
        Me.tcCustomer.SuspendLayout()
        Me.tabPolicyAc.SuspendLayout()
        CType(Me.grdAsurPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMPFPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdGIPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdEBPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMPF.SuspendLayout()
        Me.tabPersonal.SuspendLayout()
        Me.tabAddress.SuspendLayout()
        Me.tabSurvey.SuspendLayout()
        Me.tabCampaign.SuspendLayout()
        Me.tabMisc.SuspendLayout()
        Me.tabServiceLog.SuspendLayout()
        Me.tabRel.SuspendLayout()
        Me.tabAsurPersonal.SuspendLayout()
        Me.tabAsurServiceLog.SuspendLayout()
        Me.tabGI.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcCustomer
        '
        Me.tcCustomer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcCustomer.Controls.Add(Me.tabPolicyAc)
        Me.tcCustomer.Controls.Add(Me.tabMPF)
        Me.tcCustomer.Controls.Add(Me.tabPersonal)
        Me.tcCustomer.Controls.Add(Me.tabAddress)
        Me.tcCustomer.Controls.Add(Me.tabSurvey)
        Me.tcCustomer.Controls.Add(Me.tabCampaign)
        Me.tcCustomer.Controls.Add(Me.tabMisc)
        Me.tcCustomer.Controls.Add(Me.tabServiceLog)
        Me.tcCustomer.Controls.Add(Me.tabRel)
        Me.tcCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcCustomer.HotTrack = True
        Me.tcCustomer.Location = New System.Drawing.Point(0, 128)
        Me.tcCustomer.Name = "tcCustomer"
        Me.tcCustomer.SelectedIndex = 0
        Me.tcCustomer.Size = New System.Drawing.Size(2309, 1366)
        Me.tcCustomer.TabIndex = 0
        Me.tcCustomer.Tag = "Customer Information"
        '
        'tabPolicyAc
        '
        Me.tabPolicyAc.AutoScroll = True
        Me.tabPolicyAc.Controls.Add(Me.Label13)
        Me.tabPolicyAc.Controls.Add(Me.txtRemind)
        Me.tabPolicyAc.Controls.Add(Me.grdAsurPolicy)
        Me.tabPolicyAc.Controls.Add(Me.Label12)
        Me.tabPolicyAc.Controls.Add(Me.Label11)
        Me.tabPolicyAc.Controls.Add(Me.Label10)
        Me.tabPolicyAc.Controls.Add(Me.Label9)
        Me.tabPolicyAc.Controls.Add(Me.Label8)
        Me.tabPolicyAc.Controls.Add(Me.Label7)
        Me.tabPolicyAc.Controls.Add(Me.Label14)
        Me.tabPolicyAc.Controls.Add(Me.Label6)
        Me.tabPolicyAc.Controls.Add(Me.lblYearlyAggregateCashValue)
        Me.tabPolicyAc.Controls.Add(Me.lblYearlyAggregateCashValueAsur)
        Me.tabPolicyAc.Controls.Add(Me.Label5)
        Me.tabPolicyAc.Controls.Add(Me.grdMPFPolicy)
        Me.tabPolicyAc.Controls.Add(Me.grdGIPolicy)
        Me.tabPolicyAc.Controls.Add(Me.Label2)
        Me.tabPolicyAc.Controls.Add(Me.grdEBPolicy)
        Me.tabPolicyAc.Controls.Add(Me.Label3)
        Me.tabPolicyAc.Controls.Add(Me.Label1)
        Me.tabPolicyAc.Controls.Add(Me.grdPolicy)
        Me.tabPolicyAc.Controls.Add(Me.txtCCoName)
        Me.tabPolicyAc.Controls.Add(Me.txtCoName)
        Me.tabPolicyAc.Controls.Add(Me.txtTitle)
        Me.tabPolicyAc.Controls.Add(Me.Label4)
        Me.tabPolicyAc.Controls.Add(Me.txtLastName)
        Me.tabPolicyAc.Controls.Add(Me.txtFirstName)
        Me.tabPolicyAc.Controls.Add(Me.txtChiName)
        Me.tabPolicyAc.Controls.Add(Me.cmdOpen)
        Me.tabPolicyAc.Location = New System.Drawing.Point(4, 22)
        Me.tabPolicyAc.Name = "tabPolicyAc"
        Me.tabPolicyAc.Size = New System.Drawing.Size(2301, 1340)
        Me.tabPolicyAc.TabIndex = 0
        Me.tabPolicyAc.Text = "Policy"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(714, 447)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(118, 19)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "Assurance Life"
        '
        'txtRemind
        '
        Me.txtRemind.Location = New System.Drawing.Point(716, 320)
        Me.txtRemind.Multiline = True
        Me.txtRemind.Name = "txtRemind"
        Me.txtRemind.ReadOnly = True
        Me.txtRemind.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRemind.Size = New System.Drawing.Size(311, 124)
        Me.txtRemind.TabIndex = 45
        '
        'grdAsurPolicy
        '
        Me.grdAsurPolicy.AlternatingBackColor = System.Drawing.Color.White
        Me.grdAsurPolicy.BackColor = System.Drawing.Color.White
        Me.grdAsurPolicy.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAsurPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAsurPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAsurPolicy.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAsurPolicy.CaptionVisible = False
        Me.grdAsurPolicy.DataMember = ""
        Me.grdAsurPolicy.FlatMode = True
        Me.grdAsurPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdAsurPolicy.ForeColor = System.Drawing.Color.Black
        Me.grdAsurPolicy.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAsurPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdAsurPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdAsurPolicy.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAsurPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAsurPolicy.Location = New System.Drawing.Point(713, 469)
        Me.grdAsurPolicy.Name = "grdAsurPolicy"
        Me.grdAsurPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAsurPolicy.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAsurPolicy.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAsurPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAsurPolicy.Size = New System.Drawing.Size(597, 127)
        Me.grdAsurPolicy.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(716, 280)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(311, 17)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "Note: ^ Calendar Year = 1 Jan - 31 Dec (both dates inclusive)"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(713, 236)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(311, 35)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "2) The policyholder and life insured are treated as the same in customer level fo" &
    "r the purpose of controlling payment in cash."
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(716, 199)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(311, 27)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "New Business/Renewal Premium Payment/Repayment of Policy Loan/Repayment of APL/ R" &
    "einstatement of policy"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(716, 153)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(311, 46)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "1) For non-local and local policy owner, the cash limit accepted per calendar yea" &
    "r^ is USD 50,000 or its equivalent in HKD or RMB. This limit is applicable to th" &
    "e following combination:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(716, 135)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Remarks"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(713, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Bermuda HKD"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(713, 92)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 13)
        Me.Label14.TabIndex = 38
        Me.Label14.Text = "Assurance HKD"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(710, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(268, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Yearly aggregate cash received amount (all life policies)"
        '
        'lblYearlyAggregateCashValue
        '
        Me.lblYearlyAggregateCashValue.AutoSize = True
        Me.lblYearlyAggregateCashValue.Location = New System.Drawing.Point(799, 72)
        Me.lblYearlyAggregateCashValue.Name = "lblYearlyAggregateCashValue"
        Me.lblYearlyAggregateCashValue.Size = New System.Drawing.Size(0, 13)
        Me.lblYearlyAggregateCashValue.TabIndex = 39
        '
        'lblYearlyAggregateCashValueAsur
        '
        Me.lblYearlyAggregateCashValueAsur.AutoSize = True
        Me.lblYearlyAggregateCashValueAsur.Location = New System.Drawing.Point(799, 92)
        Me.lblYearlyAggregateCashValueAsur.Name = "lblYearlyAggregateCashValueAsur"
        Me.lblYearlyAggregateCashValueAsur.Size = New System.Drawing.Size(0, 13)
        Me.lblYearlyAggregateCashValueAsur.TabIndex = 39
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 447)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 19)
        Me.Label5.TabIndex = 36
        Me.Label5.Tag = ""
        Me.Label5.Text = "MPF"
        '
        'grdMPFPolicy
        '
        Me.grdMPFPolicy.AlternatingBackColor = System.Drawing.Color.White
        Me.grdMPFPolicy.BackColor = System.Drawing.Color.White
        Me.grdMPFPolicy.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdMPFPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdMPFPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdMPFPolicy.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdMPFPolicy.CaptionVisible = False
        Me.grdMPFPolicy.DataMember = ""
        Me.grdMPFPolicy.FlatMode = True
        Me.grdMPFPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdMPFPolicy.ForeColor = System.Drawing.Color.Black
        Me.grdMPFPolicy.GridLineColor = System.Drawing.Color.Wheat
        Me.grdMPFPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdMPFPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdMPFPolicy.HeaderForeColor = System.Drawing.Color.Black
        Me.grdMPFPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdMPFPolicy.Location = New System.Drawing.Point(17, 469)
        Me.grdMPFPolicy.Name = "grdMPFPolicy"
        Me.grdMPFPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdMPFPolicy.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdMPFPolicy.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdMPFPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdMPFPolicy.Size = New System.Drawing.Size(680, 127)
        Me.grdMPFPolicy.TabIndex = 35
        '
        'grdGIPolicy
        '
        Me.grdGIPolicy.AlternatingBackColor = System.Drawing.Color.White
        Me.grdGIPolicy.BackColor = System.Drawing.Color.White
        Me.grdGIPolicy.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdGIPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdGIPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGIPolicy.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdGIPolicy.CaptionVisible = False
        Me.grdGIPolicy.DataMember = ""
        Me.grdGIPolicy.FlatMode = True
        Me.grdGIPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdGIPolicy.ForeColor = System.Drawing.Color.Black
        Me.grdGIPolicy.GridLineColor = System.Drawing.Color.Wheat
        Me.grdGIPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdGIPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdGIPolicy.HeaderForeColor = System.Drawing.Color.Black
        Me.grdGIPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGIPolicy.Location = New System.Drawing.Point(17, 171)
        Me.grdGIPolicy.Name = "grdGIPolicy"
        Me.grdGIPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdGIPolicy.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdGIPolicy.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdGIPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGIPolicy.Size = New System.Drawing.Size(680, 124)
        Me.grdGIPolicy.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 19)
        Me.Label2.TabIndex = 29
        Me.Label2.Tag = ""
        Me.Label2.Text = "GI"
        '
        'grdEBPolicy
        '
        Me.grdEBPolicy.AlternatingBackColor = System.Drawing.Color.White
        Me.grdEBPolicy.BackColor = System.Drawing.Color.White
        Me.grdEBPolicy.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdEBPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdEBPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdEBPolicy.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdEBPolicy.CaptionVisible = False
        Me.grdEBPolicy.DataMember = ""
        Me.grdEBPolicy.FlatMode = True
        Me.grdEBPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdEBPolicy.ForeColor = System.Drawing.Color.Black
        Me.grdEBPolicy.GridLineColor = System.Drawing.Color.Wheat
        Me.grdEBPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdEBPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdEBPolicy.HeaderForeColor = System.Drawing.Color.Black
        Me.grdEBPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdEBPolicy.Location = New System.Drawing.Point(17, 320)
        Me.grdEBPolicy.Name = "grdEBPolicy"
        Me.grdEBPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdEBPolicy.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdEBPolicy.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdEBPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdEBPolicy.Size = New System.Drawing.Size(680, 124)
        Me.grdEBPolicy.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 298)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 19)
        Me.Label3.TabIndex = 29
        Me.Label3.Tag = ""
        Me.Label3.Text = "Employee Benefit"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(162, 19)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Bermuda/Private Life"
        '
        'grdPolicy
        '
        Me.grdPolicy.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPolicy.BackColor = System.Drawing.Color.White
        Me.grdPolicy.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPolicy.CaptionVisible = False
        Me.grdPolicy.DataMember = ""
        Me.grdPolicy.FlatMode = True
        Me.grdPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPolicy.ForeColor = System.Drawing.Color.Black
        Me.grdPolicy.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPolicy.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.Location = New System.Drawing.Point(17, 22)
        Me.grdPolicy.Name = "grdPolicy"
        Me.grdPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPolicy.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPolicy.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.Size = New System.Drawing.Size(680, 124)
        Me.grdPolicy.TabIndex = 0
        '
        'txtCCoName
        '
        Me.txtCCoName.Location = New System.Drawing.Point(378, 115)
        Me.txtCCoName.Name = "txtCCoName"
        Me.txtCCoName.Size = New System.Drawing.Size(248, 20)
        Me.txtCCoName.TabIndex = 19
        Me.txtCCoName.Visible = False
        '
        'txtCoName
        '
        Me.txtCoName.Location = New System.Drawing.Point(126, 115)
        Me.txtCoName.Name = "txtCoName"
        Me.txtCoName.Size = New System.Drawing.Size(248, 20)
        Me.txtCoName.TabIndex = 18
        Me.txtCoName.Visible = False
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Location = New System.Drawing.Point(170, 105)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(80, 20)
        Me.txtTitle.TabIndex = 17
        Me.txtTitle.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Serving Customer"
        Me.Label4.Visible = False
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Location = New System.Drawing.Point(256, 114)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.Size = New System.Drawing.Size(176, 20)
        Me.txtLastName.TabIndex = 15
        Me.txtLastName.Visible = False
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Location = New System.Drawing.Point(438, 112)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.Size = New System.Drawing.Size(188, 20)
        Me.txtFirstName.TabIndex = 14
        Me.txtFirstName.Visible = False
        '
        'txtChiName
        '
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Location = New System.Drawing.Point(464, 114)
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.Size = New System.Drawing.Size(92, 20)
        Me.txtChiName.TabIndex = 13
        Me.txtChiName.Visible = False
        '
        'cmdOpen
        '
        Me.cmdOpen.Enabled = False
        Me.cmdOpen.Location = New System.Drawing.Point(698, 8)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(48, 23)
        Me.cmdOpen.TabIndex = 1
        Me.cmdOpen.Text = "&Open"
        Me.cmdOpen.Visible = False
        '
        'tabMPF
        '
        Me.tabMPF.Controls.Add(Me.UclMPF1)
        Me.tabMPF.Location = New System.Drawing.Point(4, 22)
        Me.tabMPF.Name = "tabMPF"
        Me.tabMPF.Size = New System.Drawing.Size(2301, 1340)
        Me.tabMPF.TabIndex = 5
        Me.tabMPF.Text = "Pension"
        '
        'tabPersonal
        '
        Me.tabPersonal.AutoScroll = True
        Me.tabPersonal.Controls.Add(Me.Personal1)
        Me.tabPersonal.Location = New System.Drawing.Point(4, 22)
        Me.tabPersonal.Name = "tabPersonal"
        Me.tabPersonal.Size = New System.Drawing.Size(2301, 1340)
        Me.tabPersonal.TabIndex = 2
        Me.tabPersonal.Text = "Personal"
        '
        'tabAddress
        '
        Me.tabAddress.Controls.Add(Me.AddressSelect1)
        Me.tabAddress.Location = New System.Drawing.Point(4, 22)
        Me.tabAddress.Name = "tabAddress"
        Me.tabAddress.Size = New System.Drawing.Size(2301, 1340)
        Me.tabAddress.TabIndex = 3
        Me.tabAddress.Text = "Address"
        '
        'tabSurvey
        '
        Me.tabSurvey.Controls.Add(Me.UclSurvey1)
        Me.tabSurvey.Location = New System.Drawing.Point(4, 22)
        Me.tabSurvey.Name = "tabSurvey"
        Me.tabSurvey.Size = New System.Drawing.Size(2301, 1340)
        Me.tabSurvey.TabIndex = 6
        Me.tabSurvey.Text = "Survey"
        '
        'tabCampaign
        '
        Me.tabCampaign.Controls.Add(Me.UclCampaignTracking1)
        Me.tabCampaign.Controls.Add(Me.campListView)
        Me.tabCampaign.Location = New System.Drawing.Point(4, 22)
        Me.tabCampaign.Name = "tabCampaign"
        Me.tabCampaign.Size = New System.Drawing.Size(2301, 1340)
        Me.tabCampaign.TabIndex = 7
        Me.tabCampaign.Text = "Campaign"
        '
        'campListView
        '
        Me.campListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.campListView.FullRowSelect = True
        Me.campListView.HideSelection = False
        Me.campListView.Location = New System.Drawing.Point(4, 4)
        Me.campListView.Name = "campListView"
        Me.campListView.Size = New System.Drawing.Size(644, 88)
        Me.campListView.TabIndex = 30
        Me.campListView.UseCompatibleStateImageBehavior = False
        Me.campListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Campaign ID"
        Me.ColumnHeader7.Width = 127
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Campaign Name"
        Me.ColumnHeader8.Width = 198
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Start Date"
        Me.ColumnHeader9.Width = 102
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "End Date"
        Me.ColumnHeader10.Width = 102
        '
        'tabMisc
        '
        Me.tabMisc.Controls.Add(Me.UclAdditional1)
        Me.tabMisc.Location = New System.Drawing.Point(4, 22)
        Me.tabMisc.Name = "tabMisc"
        Me.tabMisc.Size = New System.Drawing.Size(2301, 1340)
        Me.tabMisc.TabIndex = 8
        Me.tabMisc.Text = "Misc. Information"
        '
        'tabServiceLog
        '
        Me.tabServiceLog.AutoScroll = True
        Me.tabServiceLog.Controls.Add(Me.UclServiceLog1)
        Me.tabServiceLog.Location = New System.Drawing.Point(4, 22)
        Me.tabServiceLog.Name = "tabServiceLog"
        Me.tabServiceLog.Size = New System.Drawing.Size(2301, 1340)
        Me.tabServiceLog.TabIndex = 1
        Me.tabServiceLog.Text = "Service Log"
        '
        'tabRel
        '
        Me.tabRel.Controls.Add(Me.UclCustomerRel1)
        Me.tabRel.Location = New System.Drawing.Point(4, 22)
        Me.tabRel.Name = "tabRel"
        Me.tabRel.Size = New System.Drawing.Size(2301, 1340)
        Me.tabRel.TabIndex = 9
        Me.tabRel.Text = "Customer Relationship"
        '
        'tabAsurPersonal
        '
        Me.tabAsurPersonal.BackColor = System.Drawing.SystemColors.Control
        Me.tabAsurPersonal.Controls.Add(Me.Personal_Asur1)
        Me.tabAsurPersonal.Location = New System.Drawing.Point(4, 26)
        Me.tabAsurPersonal.Name = "tabAsurPersonal"
        Me.tabAsurPersonal.Size = New System.Drawing.Size(1532, 626)
        Me.tabAsurPersonal.TabIndex = 10
        Me.tabAsurPersonal.Text = "Personal_Asur (Assurance)"
        '
        'tabAsurServiceLog
        '
        Me.tabAsurServiceLog.BackColor = System.Drawing.SystemColors.Control
        Me.tabAsurServiceLog.Controls.Add(Me.UclServiceLog2)
        Me.tabAsurServiceLog.Location = New System.Drawing.Point(4, 26)
        Me.tabAsurServiceLog.Name = "tabAsurServiceLog"
        Me.tabAsurServiceLog.Size = New System.Drawing.Size(1532, 720)
        Me.tabAsurServiceLog.TabIndex = 11
        Me.tabAsurServiceLog.Text = "Service Log (Assurance)"
        '
        'tabGI
        '
        Me.tabGI.Controls.Add(Me.UclGI1)
        Me.tabGI.Location = New System.Drawing.Point(4, 22)
        Me.tabGI.Name = "tabGI"
        Me.tabGI.Size = New System.Drawing.Size(748, 310)
        Me.tabGI.TabIndex = 4
        Me.tabGI.Text = "GI Policy"
        '
        'cmdSDetails
        '
        Me.cmdSDetails.Location = New System.Drawing.Point(696, 16)
        Me.cmdSDetails.Name = "cmdSDetails"
        Me.cmdSDetails.Size = New System.Drawing.Size(52, 36)
        Me.cmdSDetails.TabIndex = 6
        Me.cmdSDetails.Text = "Show Details"
        '
        'custListView
        '
        Me.custListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader11, Me.ColumnHeader6, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader12})
        Me.custListView.FullRowSelect = True
        Me.custListView.HideSelection = False
        Me.custListView.Location = New System.Drawing.Point(4, 16)
        Me.custListView.MultiSelect = False
        Me.custListView.Name = "custListView"
        Me.custListView.Size = New System.Drawing.Size(688, 108)
        Me.custListView.TabIndex = 5
        Me.custListView.UseCompatibleStateImageBehavior = False
        Me.custListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "CustomerID"
        Me.ColumnHeader1.Width = 90
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Last Name"
        Me.ColumnHeader2.Width = 90
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "First Name"
        Me.ColumnHeader11.Width = 103
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Chinese Name"
        Me.ColumnHeader6.Width = 83
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Relationship"
        Me.ColumnHeader3.Width = 93
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "HKID"
        Me.ColumnHeader4.Width = 84
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Date of Birth"
        Me.ColumnHeader5.Width = 80
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Gender"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(8, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(85, 13)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "Family Members:"
        '
        'UclMPF1
        '
        Me.UclMPF1.Location = New System.Drawing.Point(0, 0)
        Me.UclMPF1.Name = "UclMPF1"
        Me.UclMPF1.Size = New System.Drawing.Size(680, 296)
        Me.UclMPF1.TabIndex = 0
        '
        'Personal1
        '
        Me.Personal1.CompanyID = "ING"
        Me.Personal1.Location = New System.Drawing.Point(0, 0)
        Me.Personal1.Name = "Personal1"
        Me.Personal1.Size = New System.Drawing.Size(975, 453)
        Me.Personal1.TabIndex = 0
        '
        'AddressSelect1
        '
        Me.AddressSelect1.Location = New System.Drawing.Point(0, 0)
        Me.AddressSelect1.Name = "AddressSelect1"
        Me.AddressSelect1.Size = New System.Drawing.Size(720, 256)
        Me.AddressSelect1.TabIndex = 2
        '
        'UclSurvey1
        '
        Me.UclSurvey1.CustID = Nothing
        Me.UclSurvey1.Location = New System.Drawing.Point(0, 8)
        Me.UclSurvey1.Name = "UclSurvey1"
        Me.UclSurvey1.Size = New System.Drawing.Size(730, 290)
        Me.UclSurvey1.TabIndex = 0
        '
        'UclCampaignTracking1
        '
        Me.UclCampaignTracking1.Location = New System.Drawing.Point(4, 96)
        Me.UclCampaignTracking1.Name = "UclCampaignTracking1"
        Me.UclCampaignTracking1.ReadOnlyMode = False
        Me.UclCampaignTracking1.Size = New System.Drawing.Size(644, 263)
        Me.UclCampaignTracking1.TabIndex = 31
        '
        'UclAdditional1
        '
        Me.UclAdditional1.CustID = Nothing
        Me.UclAdditional1.Location = New System.Drawing.Point(0, 4)
        Me.UclAdditional1.Name = "UclAdditional1"
        Me.UclAdditional1.Size = New System.Drawing.Size(736, 320)
        Me.UclAdditional1.TabIndex = 0
        '
        'UclServiceLog1
        '
        Me.UclServiceLog1.CompanyID = Nothing
        Me.UclServiceLog1.CustomerID = Nothing
        Me.UclServiceLog1.IDNumber = Nothing
        Me.UclServiceLog1.IsNBMPolicy = False
        Me.UclServiceLog1.Location = New System.Drawing.Point(5, 2)
        Me.UclServiceLog1.Name = "UclServiceLog1"
        Me.UclServiceLog1.PolicyType = Nothing
        Me.UclServiceLog1.ReturnDataTableRetentionCampaignEnquiry = Nothing
        Me.UclServiceLog1.Size = New System.Drawing.Size(2500, 1250)
        Me.UclServiceLog1.TabIndex = 0
        '
        'UclCustomerRel1
        '
        Me.UclCustomerRel1.Location = New System.Drawing.Point(4, 4)
        Me.UclCustomerRel1.Name = "UclCustomerRel1"
        Me.UclCustomerRel1.Size = New System.Drawing.Size(648, 216)
        Me.UclCustomerRel1.TabIndex = 0
        '
        'Personal_Asur1
        '
        Me.Personal_Asur1.Location = New System.Drawing.Point(0, 0)
        Me.Personal_Asur1.Margin = New System.Windows.Forms.Padding(4)
        Me.Personal_Asur1.Name = "Personal_Asur1"
        Me.Personal_Asur1.Size = New System.Drawing.Size(864, 282)
        Me.Personal_Asur1.TabIndex = 0
        '
        'UclServiceLog2
        '
        Me.UclServiceLog2.AutoSize = True
        Me.UclServiceLog2.CustomerID = Nothing
        Me.UclServiceLog2.Location = New System.Drawing.Point(0, 6)
        Me.UclServiceLog2.Margin = New System.Windows.Forms.Padding(4)
        Me.UclServiceLog2.Name = "UclServiceLog2"
        Me.UclServiceLog2.PolicyType = Nothing
        Me.UclServiceLog2.Size = New System.Drawing.Size(1604, 844)
        Me.UclServiceLog2.TabIndex = 1
        '
        'UclGI1
        '
        Me.UclGI1.CustID = Nothing
        Me.UclGI1.Location = New System.Drawing.Point(0, 8)
        Me.UclGI1.Name = "UclGI1"
        Me.UclGI1.Size = New System.Drawing.Size(720, 296)
        Me.UclGI1.TabIndex = 0
        '
        'frmCustomer_Asur
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1924, 1197)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.cmdSDetails)
        Me.Controls.Add(Me.custListView)
        Me.Controls.Add(Me.tcCustomer)
        Me.Name = "frmCustomer_Asur"
        Me.Text = "Customer-centric view"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tcCustomer.ResumeLayout(False)
        Me.tabPolicyAc.ResumeLayout(False)
        Me.tabPolicyAc.PerformLayout()
        CType(Me.grdAsurPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMPFPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdGIPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdEBPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMPF.ResumeLayout(False)
        Me.tabPersonal.ResumeLayout(False)
        Me.tabAddress.ResumeLayout(False)
        Me.tabSurvey.ResumeLayout(False)
        Me.tabCampaign.ResumeLayout(False)
        Me.tabMisc.ResumeLayout(False)
        Me.tabServiceLog.ResumeLayout(False)
        Me.tabRel.ResumeLayout(False)
        Me.tabAsurPersonal.ResumeLayout(False)
        Me.tabAsurServiceLog.ResumeLayout(False)
        Me.tabAsurServiceLog.PerformLayout()
        Me.tabGI.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private strCustID, strClientID, strUCID, strCID(), strCompanyID, strIDNumber As String
    Private ds As DataSet = New DataSet("PolicyAccount")
    Private ds_A As DataSet = New DataSet("PolicyList")
    Private sqldt, sqldt1 As DataTable
    Private bm, bm_A As BindingManagerBase
    Private bmGI As BindingManagerBase
    Private bmEB As BindingManagerBase
    Private bmMPF As BindingManagerBase
    Private lngErr As Long = 0
    Private strErr As String = ""
    Private blnShowSrvLog, blnCovSmoker As Boolean
    Private blnGI, blnIsBothComp As Boolean    ' ES01
    Private SearchResponse As New CRS_Util.clsJSONBusinessObj.clsSearchResponse 'KT20161111
    Private cstDateFormat As String = "yyyy-MM-dd"
    Private blnUHNWCustomer As Boolean
    'added at 2023-9-13 by oliver for Customer Level Search Issue
    Private blnIsAgent As Boolean
    'oliver 2024-8-6 added for Com 6
    Private blnIsHnwPolicy As Boolean = False
    'Private aWeb As frmWebView


    ' ES01 begin
    Public WriteOnly Property IsGI() As Boolean
        Set(ByVal Value As Boolean)
            blnGI = Value
        End Set
    End Property
    ' ES01 end

    Public Property CustID(ByVal ClientID As String, Optional ByVal blnShow As Boolean = False) As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                strCustID = Value
                strClientID = ClientID
                blnShowSrvLog = blnShow
            End If
        End Set
    End Property

    Public Property UCID() As String
        Get
            Return strUCID
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                strUCID = Value
            End If
        End Set
    End Property

    Public WriteOnly Property isUHNWCustomer() As Boolean
        Set(ByVal Value As Boolean)
            blnUHNWCustomer = Value
        End Set
    End Property

    Public Property CompanyID() As String
        Get
            Return strCompanyID
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                strCompanyID = Value
            End If
        End Set
    End Property

    Public Property IDNumber() As String
        Get
            Return strIDNumber
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                strIDNumber = Value
            End If
        End Set
    End Property

    Public WriteOnly Property IsBothComp() As Boolean
        Set(ByVal Value As Boolean)
            blnIsBothComp = Value
        End Set
    End Property

    'added at 2023-9-13 by oliver for Customer Level Search Issue
    Public WriteOnly Property IsAgent() As Boolean
        Set(ByVal Value As Boolean)
            blnIsAgent = Value
        End Set
    End Property

    'oliver 2024-8-6 added for Com 6
    Public WriteOnly Property IsHnwPolicy() As Boolean
        Set(ByVal Value As Boolean)
            blnIsHnwPolicy = Value
        End Set
    End Property

    Private Sub SearchPolicy(Optional ByVal strCompanyID As String = "ING")

        Dim sqlconnect As New SqlConnection
        Dim strSQL, strConn As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim tempDS As DataSet = New DataSet
        Dim strCustCriteria As String = String.Empty

        If blnGI = False Then       ' ES01

            If blnIsBothComp Then
                Dim count As Integer
                ' single customer flow
                If (Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX")) Then
                    strCustCriteria = " select customerid from customer where (GovernmentIDCard = '" & strIDNumber & "' or PassportNumber = '" & strIDNumber & "') "
                    ' Get Bermuda policy list XXXXX
                    'oliver 2024-8-6 updated for Com 6
                    If blnIsHnwPolicy AndAlso Not isHNWMember Then
                        sqldt = GetAsurPolicyListCIW(strCustID, "", "", "POLST", lngErr, strErr, lngCnt, False, "ING", "", strCustCriteria, "ING")
                    Else
                        sqldt = GetAsurPolicyListCIW(strCustID, "", "", "POLST", lngErr, strErr, lngCnt, False, "ING", "", strCustCriteria)
                    End If
                    HandleGetPolicy("ING", strCIWConn)

                    ' Get Assurance policy list XXXXX
                    sqldt = GetAsurPolicyListCIW(strCustID, "", "", "POLST", lngErr, strErr, lngCnt, False, "LAC", "", strCustCriteria)
                    HandleGetPolicy("LAC", strLIDZConn)
                    count = bm.Count + bm_A.Count
                Else
                    sqldt = GetAsurPolicyListCIW(strCustID, "", "", "POLST", lngErr, strErr, lngCnt, False, strCompanyID, "", strCustCriteria)
                    HandleGetPolicy(strCompanyID, If(strCompanyID = "ING", strCIWConn, strLIDZConn))
                    If strCompanyID = "ING" Then
                        count = bm.Count
                    Else
                        count = bm_A.Count
                    End If
                End If

                wndMain.StatusBarPanel1.Text = count & " records selected"
                If count > 0 Then
                    cmdOpen.Enabled = True
                Else
                    cmdOpen.Enabled = False
                End If

                Dim strErrMsg As String
                Dim dtCAP, dtCIWPersonalInfo As DataTable
                If Not GetPersonalInfoAddress4Customer(strCustID, strClientID, strErrMsg, blnCovSmoker, dtCAP, sqldt1, dtCIWPersonalInfo, False, strCompanyID) Then
                    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                    Exit Sub
                End If
                'harry
                If dtCAP IsNot Nothing And dtCIWPersonalInfo IsNot Nothing Then
                    Personal1.CompanyID = strCompanyID
                    Personal1.CustID(strClientID, dtCAP, dtCIWPersonalInfo, blnCovSmoker) = strCustID
                End If
                AddressSelect1.srcDTAddr = sqldt1

                If dtCAP IsNot Nothing Then
                    If dtCAP.Rows.Count > 0 AndAlso Not IsDBNull(dtCAP.Rows(0).Item("GOVERNMENTIDCARD")) Then
                        Me.UclMPF1.CustID(strCustID) = Trim(dtCAP.Rows(0).Item("GOVERNMENTIDCARD"))
                    End If
                End If

                Me.UclGI1.CustID = strCustID
                Me.UclSurvey1.CustID = strCustID
                Me.UclCampaignTracking1.CustID("") = strCustID
                Me.UclAdditional1.CustID = strCustID
                'added at 2023-9-12 by oliver for Customer Level Search Issue
                Me.UclServiceLog1.IDNumber = strIDNumber
                Me.UclServiceLog1.IsAgent = blnIsAgent
                Me.UclServiceLog1.CompanyID = strCompanyID
                Me.UclServiceLog1.IsParallelMode = True
                Me.UclServiceLog1.IsBothComp = blnIsBothComp
                Me.UclServiceLog1.IsCustLevel = True
                'oliver 2024-8-6 added for Com 6
                Me.UclServiceLog1.IsHnwPolicy = blnIsHnwPolicy
                Me.UclServiceLog1.CustomerID = strCustID
            Else
                strConn = IIf(strCompanyID.Equals("ING"), strCIWConn, strLIDZConn)

                ' normal flow
                ' updated at 2023-9-12 by oliver for Customer Level Search Issue
                If (Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX")) Then
                    strCustCriteria = " select customerid from customer where (GovernmentIDCard = '" & strIDNumber & "' or PassportNumber = '" & strIDNumber & "') "
                End If
                'oliver 2024-8-6 updated for Com 6
                sqldt = GetAsurPolicyListCIW(strCustID, "", "", "POLST", lngErr, strErr, lngCnt, False, strCompanyID, "", strCustCriteria, If(blnIsHnwPolicy AndAlso isHNWMember, "BMU", ""))

                If lngErr <> 0 Then
                    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If

                tempDS.Tables.Add(sqldt)

                ' Check if in any coverage, the insured is marked as smoker
                For i As Integer = 0 To sqldt.Rows.Count - 1
                    If sqldt.Rows(i).Item("PolicyRelateCode") = "PI" And sqldt.Rows(i).Item("SMCODE") = "S" Then
                        blnCovSmoker = True
                        Exit For
                    End If
                Next

                strSQL = "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
                strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes; "

                sqlconnect.ConnectionString = strConn

                sqlda = New SqlDataAdapter(strSQL, sqlconnect)
                sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
                sqlda.MissingMappingAction = MissingMappingAction.Passthrough

                sqlda.TableMappings.Add("PolicyAccountRelationCodes1", "AccountStatusCodes")

                Try
                    sqlda.Fill(tempDS, "PolicyAccountRelationCodes")
                Catch sqlex As SqlClient.SqlException
                    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                End Try

                Dim relAccRel As New Data.DataRelation("AccRel", tempDS.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationCode"),
                    tempDS.Tables("POLST").Columns("PolicyRelateCode"), True)
                Dim relAccSts As New Data.DataRelation("AccSts", tempDS.Tables("AccountStatusCodes").Columns("AccountStatusCode"),
                    tempDS.Tables("POLST").Columns("AccountStatusCode"), True)

                Try
                    tempDS.Relations.Add(relAccRel)
                Catch sqlex As SqlClient.SqlException
                    'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Catch ex As Exception
                    'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                End Try

                Try
                    tempDS.Relations.Add(relAccSts)
                Catch sqlex As SqlClient.SqlException
                    'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Catch ex As Exception
                    'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                End Try

                Dim strErrMsg As String
                Dim dtCAP, dtCIWPersonalInfo As DataTable
                If Not GetPersonalInfoAddress4Customer(strCustID, strClientID, strErrMsg, blnCovSmoker, dtCAP, sqldt1, dtCIWPersonalInfo, False, strCompanyID) Then
                    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                    Exit Sub
                End If
                'harry
                If dtCAP IsNot Nothing And dtCIWPersonalInfo IsNot Nothing Then
                    Personal1.CompanyID = strCompanyID
                    Personal1.CustID(strClientID, dtCAP, dtCIWPersonalInfo, blnCovSmoker) = strCustID
                End If
                'Updated at 2023-8-21 by oliver for fixed the bug which is Address Not Found Under Customer Search  
                AddressSelect1.srcDTAddr = sqldt1

                '***End Modify Get Customer Info***

                tempDS.Tables("POLST").DefaultView.Sort = "PolicyRelateCode, AccountStatusCode, PolicyAccountID"
                If strCompanyID.Equals("ING") Then
                    grdPolicy.DataSource = tempDS.Tables("POLST")
                    bm = Me.BindingContext(tempDS.Tables("POLST"))
                    wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
                    cmdOpen.Enabled = bm.Count > 0
                Else
                    grdAsurPolicy.DataSource = tempDS.Tables("POLST")
                    bm_A = Me.BindingContext(tempDS.Tables("POLST"))
                    wndMain.StatusBarPanel1.Text = bm_A.Count & " records selected"
                    cmdOpen.Enabled = bm_A.Count > 0
                End If

                'If bm.Count > 0 Or bm_A.Count > 0 Then
                '    cmdOpen.Enabled = True
                'Else
                '    cmdOpen.Enabled = False
                'End If

                Me.UclGI1.CustID = strCustID
                Me.UclSurvey1.CustID = strCustID
                Me.UclCampaignTracking1.CustID("") = strCustID
                Me.UclAdditional1.CustID = strCustID
                'added at 2023-9-12 by oliver for Customer Level Search Issue
                Me.UclServiceLog1.IDNumber = strIDNumber
                Me.UclServiceLog1.IsAgent = blnIsAgent
                Me.UclServiceLog1.CompanyID = strCompanyID
                Me.UclServiceLog1.IsCustLevel = True
                'oliver 2024-8-6 added for Com 6
                Me.UclServiceLog1.IsHnwPolicy = blnIsHnwPolicy
                Me.UclServiceLog1.CustomerID = strCustID

                'If strCompanyID.Equals("Bermuda") Then
                '    AddressSelect1.srcDTAddr = sqldt1

                '    Me.UclGI1.CustID = strCustID
                '    Me.UclSurvey1.CustID = strCustID
                '    Me.UclCampaignTracking1.CustID("") = strCustID
                '    Me.UclAdditional1.CustID = strCustID
                '    Me.UclServiceLog1.CustomerID = strCustID

                'Else
                '    Me.UclServiceLog2.CompanyID = strCompanyID
                '    Me.UclServiceLog2.CustomerID = strUCID
                'End If

                'harry
                If dtCAP IsNot Nothing Then
                    If dtCAP.Rows.Count > 0 AndAlso Not IsDBNull(dtCAP.Rows(0).Item("GOVERNMENTIDCARD")) Then
                        Me.UclMPF1.CustID(strCustID) = Trim(dtCAP.Rows(0).Item("GOVERNMENTIDCARD"))
                    End If
                End If

                If blnShowSrvLog Then tcCustomer.SelectedTab = tabServiceLog

                If strCompanyID.Equals("ING") Then
                    ds = tempDS
                Else
                    ds_A = tempDS
                End If

            End If

        Else
            ' GI only
            Me.UclGI1.CustID = strCustID
            Me.UclServiceLog1.CustomerID = strCustID
            If blnShowSrvLog Then tcCustomer.SelectedTab = tabServiceLog

        End If      ' ES01

    End Sub

    Private Sub HandleGetPolicy(ByVal strComp As String, ByVal strConn As String)

        Dim sqlconnect As New SqlConnection
        'Dim strSQL, strCompany, strConn As String
        'Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim strSQL As String
        Dim tempDS As DataSet = New DataSet

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        tempDS.Tables.Add(sqldt)

        ' Check if in any coverage, the insured is marked as smoker
        For i As Integer = 0 To sqldt.Rows.Count - 1
            If sqldt.Rows(i).Item("PolicyRelateCode") = "PI" And sqldt.Rows(i).Item("SMCODE") = "S" Then
                blnCovSmoker = True
                Exit For
            End If
        Next

        strSQL = "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
        strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes; "

        sqlconnect.ConnectionString = strConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        sqlda.TableMappings.Add("PolicyAccountRelationCodes1", "AccountStatusCodes")

        Try
            sqlda.Fill(tempDS, "PolicyAccountRelationCodes")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        Dim relAccRel As New Data.DataRelation("AccRel", tempDS.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationCode"),
            tempDS.Tables("POLST").Columns("PolicyRelateCode"), True)
        Dim relAccSts As New Data.DataRelation("AccSts", tempDS.Tables("AccountStatusCodes").Columns("AccountStatusCode"),
            tempDS.Tables("POLST").Columns("AccountStatusCode"), True)

        Try
            tempDS.Relations.Add(relAccRel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            tempDS.Relations.Add(relAccSts)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        '***End Modify Get Customer Info***

        tempDS.Tables("POLST").DefaultView.Sort = "PolicyRelateCode, AccountStatusCode, PolicyAccountID"
        If strComp.Equals("ING") Then
            grdPolicy.DataSource = tempDS.Tables("POLST")
            bm = Me.BindingContext(tempDS.Tables("POLST"))
        Else
            grdAsurPolicy.DataSource = tempDS.Tables("POLST")
            bm_A = Me.BindingContext(tempDS.Tables("POLST"))
        End If


        'wndMain.StatusBarPanel1.Text = bm.Count + bm_A.Count & " records selected"

        'If bm.Count > 0 Or bm_A.Count > 0 Then
        '    cmdOpen.Enabled = True
        'Else
        '    cmdOpen.Enabled = False
        'End If

        'Me.UclServiceLog1.CompanyID = IIf(strComp.Equals("Bermuda"), "ING", "LAC")
        'Me.UclServiceLog1.CustomerID = strCustID

        If blnShowSrvLog Then tcCustomer.SelectedTab = tabServiceLog

        If strComp.Equals("ING") Then
            ds = tempDS
        Else
            ds_A = tempDS
        End If

    End Sub


    Private Sub SearchPOnCUST()

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'objCS.Env = giEnv
        'End If
        'AC - Change to use configuration setting - end

        If blnGI = False Then       ' ES01

            '' Temp add to handle duplicate clientid
            'Dim strClient() As String
            'Dim ar() As Object

            'strClient = GetClientList(strCustID)

            'If Not strClient Is Nothing AndAlso strClient.Length > 0 Then

            '    'sqldt = objCS.GetPolicyList(strClient(0), "", "", "POLST", lngErr, strErr, lngCnt)
            '    sqldt = GetPolicyListCIW(strClient(0), "", "", "POLST", lngErr, strErr, lngCnt)

            '    If lngErr <> 0 Then
            '        MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Error")
            '        wndMain.Cursor = Cursors.Default
            '        Exit Sub
            '    End If

            '    For i As Integer = 1 To strClient.Length - 1
            '        'sqldt1 = objCS.GetPolicyList(strClient(i), "", "", "POLST", lngErr, strErr, lngCnt)
            '        sqldt1 = GetPolicyListCIW(strClient(i), "", "", "POLST", lngErr, strErr, lngCnt)

            '        If lngErr <> 0 Then
            '            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Error")
            '            wndMain.Cursor = Cursors.Default
            '            Exit Sub
            '        End If

            '        For k As Integer = 0 To sqldt1.Rows.Count - 1
            '            ar = sqldt1.Rows(k).ItemArray
            '            sqldt.Rows.Add(ar)
            '        Next
            '    Next

            '    ds.Tables.Add(sqldt)
            'Else
            'sqldt = objCS.GetPolicyList(strClientID, "", "", "POLST", lngErr, strErr, lngCnt)
            sqldt = GetAsurPolicyListCIW(strCustID, "", "", "POLST", lngErr, strErr, lngCnt)

            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                wndMain.Cursor = Cursors.Default
                Exit Sub
            End If

            ds.Tables.Add(sqldt)
            'End If

            ''sqldt = objCS.GetPolicyList(strClientID, "", "", "POLST", lngErr, strErr, lngCnt)

            ''If lngErr <> 0 Then
            ''    MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Error")
            ''    wndMain.Cursor = Cursors.Default
            ''    Exit Sub
            ''End If

            ''ds.Tables.Add(sqldt)

            ' Check if in any coverage, the insured is marked as smoker
            For i As Integer = 0 To sqldt.Rows.Count - 1
                If sqldt.Rows(i).Item("PolicyRelateCode") = "PI" And sqldt.Rows(i).Item("SMCODE") = "S" Then
                    blnCovSmoker = True
                    Exit For
                End If
            Next
            'strSQL = "Select a.productid, c.PolicyAccountid, c.PolicyRelateCode, PolicyEffDate, a.AccountStatusCode, " & _
            '    "            Policyaccountid, PolicyEffDate, PolicyCurrency " & _
            '    " From csw_poli_rel c, policyaccount a " & _
            '    " where c.policyaccountid = a.policyaccountid " & _
            '    " and c.customerid = '" & strCustID & "' " & _
            '    " order by c.customerid; "
            strSQL = "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
            strSQL &= "Select AccountStatusCode, AccountStatus from AccountStatusCodes; "
            'strSQL &= "Select ProductID, Description from Product"

            sqlconnect.ConnectionString = IIf(strCompanyID.Equals("ING"), strCIWConn, strLIDZConn)

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            'sqlda.TableMappings.Add("Result1", "PolicyAccountRelationCodes")
            'sqlda.TableMappings.Add("Result2", "AccountStatusCodes")
            'sqlda.TableMappings.Add("Result3", "Product")
            sqlda.TableMappings.Add("PolicyAccountRelationCodes1", "AccountStatusCodes")
            'sqlda.TableMappings.Add("PolicyAccountRelationCodes2", "Product")

            Try
                sqlda.Fill(ds, "PolicyAccountRelationCodes")
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            Dim relAccRel As New Data.DataRelation("AccRel", ds.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationCode"),
                ds.Tables("POLST").Columns("PolicyRelateCode"), True)
            Dim relAccSts As New Data.DataRelation("AccSts", ds.Tables("AccountStatusCodes").Columns("AccountStatusCode"),
                ds.Tables("POLST").Columns("AccountStatusCode"), True)


            'Dim relProduct As New Data.DataRelation("Product", ds.Tables("Product").Columns("ProductID"), _
            '    ds.Tables("POLST").Columns("ProductID"), True)

            Try
                ds.Relations.Add(relAccRel)
            Catch sqlex As SqlClient.SqlException
                'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Catch ex As Exception
                'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            End Try

            Try
                ds.Relations.Add(relAccSts)
            Catch sqlex As SqlClient.SqlException
                'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Catch ex As Exception
                'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            End Try

            'Try
            '    ds.Relations.Add(relProduct)
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            'Catch sqlex As SqlClient.SqlException
            '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            'End Try

            '***Begin Modify Get Customer Info***
            '''        ' Use CIW
            '''        strSQL = "Select c.CustomerID, NamePrefix, FirstName, NameSuffix, ChiLstNm+ChiFstNm as ChiName, CoName, CoCName, " & _
            '''            " DateOfBirth, Gender, MaritalStatusCode, SmokerFlag, LanguageCode, GovernmentIDCard, OptOutFlag, " & _
            '''            " EmailAddr, UseChiInd, g.AgentCode, ISNULL(g.UnitCode,'') as Agency, " & _
            '''            " AddressTypeCode, AddressLine1, AddressLine2, AddressLine3, AddressCity, " & _
            '''            " AddressPostalCode, PhoneNumber1, PhoneNumber2, FaxNumber1, FaxNumber2, EMailID, BadAddress, c.CountryCode, " & _
            '''            " CustomerStatusCode, CustomerTypeCode, PhoneMobile, PhonePager, PassportNumber, Occupation, BirthPlace " & _
            '''            " From CustomerAddress a, Customer c LEFT JOIN AgentCodes g " & _
            '''            " ON c.AgentCode = g.AgentCode " & _
            '''            " Where addresstypecode in ('R','B','I','J') and c.CustomerID = '" & strCustID & "' " & _
            '''            " And c.CustomerID = a.CustomerID"
            '''        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            '''        sqldt1 = New DataTable("CustomerAddress")
            '''        Try
            '''            sqlda.Fill(sqldt1)
            '''        Catch sqlex As SqlClient.SqlException
            '''            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            '''        Catch ex As Exception
            '''            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            '''        End Try

            '''        sqlconnect.Dispose()

            '''        'Use CAPSIL
            '''        Dim lngErrNo As Long
            '''        Dim strErrMsg, strAType As String
            '''        Dim drs As DataRowView
            '''        Dim dtCAP As DataTable

            '''        lngErrNo = 0
            '''        strErrMsg = ""
            '''#If UAT <> 0 Then
            '''        objCS.Env = giEnv
            '''#End If
            '''        dtCAP = objCS.GetORDUNA("'" & strClientID & "'", lngErrNo, strErrMsg)
            '''        GetChiAddr(dtCAP)
            '''        dtCAP.TableName = "CustomerAddress"

            '''        If Not dtCAP Is Nothing AndAlso Not sqldt1 Is Nothing Then
            '''            If dtCAP.Rows.Count > 0 AndAlso sqldt1.Rows.Count > 0 Then
            '''                dtCAP.Rows(0).Item("CustomerStatusCode") = sqldt1.Rows(0).Item("CustomerStatusCode")
            '''                dtCAP.Rows(0).Item("OptOutFlag") = sqldt1.Rows(0).Item("OptOutFlag")
            '''                dtCAP.Rows(0).Item("MaritalStatusCode") = sqldt1.Rows(0).Item("MaritalStatusCode")
            '''                dtCAP.Rows(0).Item("PhoneMobile") = sqldt1.Rows(0).Item("PhoneMobile")
            '''                dtCAP.Rows(0).Item("PhonePager") = sqldt1.Rows(0).Item("PhonePager")
            '''                dtCAP.Rows(0).Item("PassportNumber") = sqldt1.Rows(0).Item("PassportNumber")
            '''                dtCAP.Rows(0).Item("Occupation") = sqldt1.Rows(0).Item("Occupation")
            '''                dtCAP.Rows(0).Item("BirthPlace") = sqldt1.Rows(0).Item("BirthPlace")
            '''            End If
            '''        End If

            '''        '''strAType = ""
            '''        '''If Not sqldt1 Is Nothing Then
            '''        '''    If sqldt1.Rows.Count > 0 Then
            '''        '''        sqldt1.DefaultView.Sort = "CNAEAT DESC"
            '''        '''        sqldt1.DefaultView.RowFilter = "CNAEAT LIKE 'R%'"
            '''        '''        If sqldt1.DefaultView.Count > 0 Then
            '''        '''            drs = sqldt1.DefaultView.Item(0)
            '''        '''            strAType = "CNAEAT = '" & drs.Item("CNAEAT") & "' or CNACAT = '" & drs.Item("CNACAT") & "'"
            '''        '''        End If

            '''        '''        sqldt1.DefaultView.RowFilter = "CNAEAT LIKE 'B%'"
            '''        '''        If sqldt1.DefaultView.Count > 0 Then
            '''        '''            drs = sqldt1.DefaultView.Item(0)
            '''        '''            If strAType = "" Then
            '''        '''                strAType = "CNAEAT = '" & drs.Item("CNAEAT") & "' or CNACAT = '" & drs.Item("CNACAT") & "'"
            '''        '''            Else
            '''        '''                strAType &= "or CNAEAT = '" & drs.Item("CNAEAT") & "' or CNACAT = '" & drs.Item("CNACAT") & "'"
            '''        '''            End If
            '''        '''        End If
            '''        '''    End If
            '''        '''End If

            '''        '''sqldt1.DefaultView.RowFilter = strAType
            '''        ds.Tables("POLST").DefaultView.Sort = "PolicyRelateCode, AccountStatusCode, PolicyAccountID"
            '''        grdPolicy.DataSource = ds.Tables("POLST")
            '''        bm = Me.BindingContext(ds.Tables("POLST"))
            '''        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

            '''        If bm.Count > 0 Then
            '''            cmdOpen.Enabled = True
            '''        Else
            '''            cmdOpen.Enabled = False
            '''        End If

            '''        'If Not sqldt1 Is Nothing Then
            '''        '    If sqldt1.Rows.Count > 0 Then
            '''        '        With sqldt1.Rows(0)
            '''        '            txtTitle.Text = IIf(IsDBNull(.Item("NamePrefix")), "", .Item("NamePrefix"))
            '''        '            txtLastName.Text = IIf(IsDBNull(.Item("NameSuffix")), "", .Item("NameSuffix"))
            '''        '            txtFirstName.Text = IIf(IsDBNull(.Item("FirstName")), "", .Item("FirstName"))
            '''        '            txtChiName.Text = IIf(IsDBNull(.Item("ChiName")), "", .Item("ChiName"))
            '''        '        End With
            '''        '    End If
            '''        'End If
            '''        If Not dtCAP Is Nothing Then
            '''            If dtCAP.Rows.Count > 0 Then
            '''                With dtCAP.Rows(0)
            '''                    If .Item("Gender") = "C" Then
            '''                        txtCoName.Text = IIf(IsDBNull(.Item("CoName")), "", .Item("CoName"))
            '''                        txtCCoName.Text = IIf(IsDBNull(.Item("CoCName")), "", .Item("CoCName"))
            '''                        txtCoName.Visible = True
            '''                        txtCCoName.Visible = True
            '''                        txtTitle.Visible = False
            '''                        txtLastName.Visible = False
            '''                        txtFirstName.Visible = False
            '''                        txtChiName.Visible = False
            '''                    Else
            '''                        txtTitle.Text = IIf(IsDBNull(.Item("NamePrefix")), "", .Item("NamePrefix"))
            '''                        txtLastName.Text = IIf(IsDBNull(.Item("NameSuffix")), "", .Item("NameSuffix"))
            '''                        txtFirstName.Text = IIf(IsDBNull(.Item("FirstName")), "", .Item("FirstName"))
            '''                        txtChiName.Text = IIf(IsDBNull(.Item("ChiName")), "", .Item("ChiName"))
            '''                        txtCoName.Visible = False
            '''                        txtCCoName.Visible = False
            '''                        txtTitle.Visible = True
            '''                        txtLastName.Visible = True
            '''                        txtFirstName.Visible = True
            '''                        txtChiName.Visible = True
            '''                    End If
            '''                End With
            '''            End If
            '''        End If
            '''        'Personal1.CustID(strClientID, sqldt1.Copy) = strCustID
            '''        Personal1.CustID(strClientID, dtCAP, blnCovSmoker) = strCustID
            Dim strErrMsg As String
            Dim dtCAP, dtCIWPersonalInfo As DataTable
            If Not GetPersonalInfoAddress4Customer(strCustID, strClientID, strErrMsg, blnCovSmoker, dtCAP, sqldt1, dtCIWPersonalInfo, False) Then
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If
            'harry
            If dtCAP IsNot Nothing And dtCIWPersonalInfo IsNot Nothing Then
                Personal1.CompanyID = strCompanyID
                Personal1.CustID(strClientID, dtCAP, dtCIWPersonalInfo, blnCovSmoker) = strCustID
            End If

            '***End Modify Get Customer Info***

            ds.Tables("POLST").DefaultView.Sort = "PolicyRelateCode, AccountStatusCode, PolicyAccountID"
            grdPolicy.DataSource = ds.Tables("POLST")
            bm = Me.BindingContext(ds.Tables("POLST"))
            wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

            If bm.Count > 0 Then
                cmdOpen.Enabled = True
            Else
                cmdOpen.Enabled = False
            End If

            AddressSelect1.srcDTAddr = sqldt1
            Me.UclGI1.CustID = strCustID
            Me.UclSurvey1.CustID = strCustID

            Me.UclCampaignTracking1.CustID("") = strCustID

            'harry
            If dtCAP IsNot Nothing Then
                If dtCAP.Rows.Count > 0 AndAlso Not IsDBNull(dtCAP.Rows(0).Item("GOVERNMENTIDCARD")) Then
                    Me.UclMPF1.CustID(strCustID) = Trim(dtCAP.Rows(0).Item("GOVERNMENTIDCARD"))
                End If
            End If

            Me.UclAdditional1.CustID = strCustID
            Me.UclServiceLog1.IsCustLevel = True
            Me.UclServiceLog1.CustomerID = strCustID
            'UclServiceLog1.PolicyAccountID = "2000000002"

            If blnShowSrvLog Then tcCustomer.SelectedTab = tabServiceLog

        Else
            ' GI only
            Me.UclGI1.CustID = strCustID
            Me.UclServiceLog1.IsCustLevel = True
            Me.UclServiceLog1.CustomerID = strCustID
            If blnShowSrvLog Then tcCustomer.SelectedTab = tabServiceLog

        End If      ' ES01

    End Sub

    Private Sub buildUI(ByVal strCompanyID As String)

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn
        Dim tempDs As DataSet = New DataSet

        If strCompanyID.Equals("ING") Then
            With ds.Tables("POLST")
                .Columns.Add("PolicyAccountRelationDesc", GetType(String))
                .Columns.Add("AccountStatus", GetType(String))
            End With
            tempDs = ds
        Else
            With ds_A.Tables("POLST")
                .Columns.Add("PolicyAccountRelationDesc", GetType(String))
                .Columns.Add("AccountStatus", GetType(String))
            End With
            tempDs = ds_A
        End If

        cs = New DataGridTextBoxColumn
        cs.Width = 160
        cs.MappingName = "Description"
        cs.HeaderText = "Product Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PolicyAccountID"
        cs.HeaderText = "Policy No."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PolicyRelateCode"
        cs.HeaderText = "Relate Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("AccRel", tempDs.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationDesc"))
        cs.Width = 80
        cs.MappingName = "PolicyAccountRelationDesc"
        cs.HeaderText = "Role"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PolicyEffDate"
        cs.HeaderText = "Eff. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "ModalPremium"
        cs.HeaderText = "Modal Prem."
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PaidToDate"
        cs.HeaderText = "PTD"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("AccSts", tempDs.Tables("AccountStatusCodes").Columns("AccountStatus"))
        cs.Width = 100
        cs.MappingName = "AccountStatus"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "POAGCY"
        cs.HeaderText = "Servicing Agent"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLST"

        If strCompanyID.Equals("ING") Then
            grdPolicy.TableStyles.Add(ts)
            grdPolicy.DataSource = tempDs.Tables("POLST")
            grdPolicy.AllowDrop = False
            grdPolicy.ReadOnly = True
        Else
            grdAsurPolicy.TableStyles.Add(ts)
            grdAsurPolicy.DataSource = tempDs.Tables("POLST")
            grdAsurPolicy.AllowDrop = False
            grdAsurPolicy.ReadOnly = True
        End If

        If blnIsBothComp Then
            Dim strBerCustID As String = If(Not String.IsNullOrEmpty(strIDNumber), GetCustIDByID(strIDNumber, "B"), strCustID)
            Dim strAsurCustID As String = If(Not String.IsNullOrEmpty(strIDNumber), GetCustIDByID(strIDNumber, "A"), strCustID)

            Me.lblYearlyAggregateCashValue.Text = fetchYearlyCashValue(strIDNumber, "B") 'KT20180726
            If Me.lblYearlyAggregateCashValue.Text >= 320000 Then
                MsgBox("Yearly aggregate cash payment for (Bermuda) customer " + strBerCustID + " is >= HKD320,000")
            End If

            Me.lblYearlyAggregateCashValueAsur.Text = fetchYearlyCashValue(strIDNumber, "A")
            If Me.lblYearlyAggregateCashValueAsur.Text >= 320000 Then
                MsgBox("Yearly aggregate cash payment for (Assurance) customer " + strAsurCustID + " is >= HKD320,000")
            End If
        Else
            If strCompanyID.Equals("ING") Then
                Me.lblYearlyAggregateCashValue.Text = fetchYearlyCashValue(strIDNumber, "B")
                If Me.lblYearlyAggregateCashValue.Text >= 320000 Then
                    MsgBox("Yearly aggregate cash payment for (Bermuda) customer " + strCustID + " is >= HKD320,000")
                End If
            Else
                Me.lblYearlyAggregateCashValueAsur.Text = fetchYearlyCashValue(strIDNumber, "A")
                If Me.lblYearlyAggregateCashValueAsur.Text >= 320000 Then
                    MsgBox("Yearly aggregate cash payment for (Assurance) customer " + strCustID + " is >= HKD320,000")
                End If
            End If
        End If

    End Sub

    'Private Sub buildUI()

    '    Dim ts As New clsDataGridTableStyle(True)
    '    Dim cs As DataGridTextBoxColumn



    '    With ds.Tables("POLST")
    '        '.Columns.Add("Description", GetType(String))
    '        .Columns.Add("PolicyAccountRelationDesc", GetType(String))
    '        .Columns.Add("AccountStatus", GetType(String))
    '    End With

    '    'cs = New JoinTextBoxColumn("Product", ds.Tables("Product").Columns("Description"))
    '    'cs.Width = 220
    '    'cs.MappingName = "Description"
    '    'cs.HeaderText = "Product Description"
    '    'ts.GridColumnStyles.Add(cs)

    '    cs = New DataGridTextBoxColumn
    '    cs.Width = 160
    '    cs.MappingName = "Description"
    '    cs.HeaderText = "Product Description"
    '    cs.NullText = gNULLText
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New DataGridTextBoxColumn
    '    cs.Width = 80
    '    cs.MappingName = "PolicyAccountID"
    '    cs.HeaderText = "Policy No."
    '    cs.NullText = gNULLText
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New DataGridTextBoxColumn
    '    cs.Width = 80
    '    cs.MappingName = "PolicyRelateCode"
    '    cs.HeaderText = "Relate Code"
    '    cs.NullText = gNULLText
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New JoinTextBoxColumn("AccRel", ds.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationDesc"))
    '    cs.Width = 80
    '    cs.MappingName = "PolicyAccountRelationDesc"
    '    cs.HeaderText = "Role"
    '    cs.NullText = gNULLText
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New DataGridTextBoxColumn
    '    cs.Width = 80
    '    cs.MappingName = "PolicyEffDate"
    '    cs.HeaderText = "Eff. Date"
    '    cs.NullText = gNULLText
    '    cs.Format = gDateFormat
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New DataGridTextBoxColumn
    '    cs.Width = 90
    '    cs.MappingName = "ModalPremium"
    '    cs.HeaderText = "Modal Prem."
    '    cs.NullText = gNULLText
    '    cs.Format = gNumFormat
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New DataGridTextBoxColumn
    '    cs.Width = 80
    '    cs.MappingName = "PaidToDate"
    '    cs.HeaderText = "PTD"
    '    cs.NullText = gNULLText
    '    cs.Format = gDateFormat
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New JoinTextBoxColumn("AccSts", ds.Tables("AccountStatusCodes").Columns("AccountStatus"))
    '    cs.Width = 100
    '    cs.MappingName = "AccountStatus"
    '    cs.HeaderText = "Status"
    '    cs.NullText = gNULLText
    '    ts.GridColumnStyles.Add(cs)

    '    cs = New DataGridTextBoxColumn
    '    cs.Width = 100
    '    cs.MappingName = "POAGCY"
    '    cs.HeaderText = "Servicing Agent"
    '    cs.NullText = gNULLText
    '    ts.GridColumnStyles.Add(cs)

    '    ts.MappingName = "POLST"
    '    grdPolicy.TableStyles.Add(ts)

    '    grdPolicy.DataSource = ds.Tables("POLST")
    '    grdPolicy.AllowDrop = False
    '    grdPolicy.ReadOnly = True

    '    If blnIsBothComp Then
    '        Dim strBerCustID As String = GetCustIDByID(strIDNumber, "B")
    '        Dim strAsurCustID As String = GetCustIDByID(strIDNumber, "A")

    '        Me.lblYearlyAggregateCashValue.Text = fetchYearlyCashValue(strIDNumber, "B") 'KT20180726
    '        If Me.lblYearlyAggregateCashValue.Text >= 320000 Then
    '            MsgBox("Yearly aggregate cash payment for (Bermuda) customer " + strBerCustID + " is >= HKD320,000")
    '        End If

    '        Me.lblYearlyAggregateCashValueAsur.Text = fetchYearlyCashValue(strIDNumber, "A") 
    '        If Me.lblYearlyAggregateCashValueAsur.Text >= 320000 Then
    '            MsgBox("Yearly aggregate cash payment for (Assurance) customer " + strAsurCustID + " is >= HKD320,000")
    '        End If
    '    Else 
    '        If strCompanyID.Equals("ING") 
    '            Me.lblYearlyAggregateCashValue.Text = fetchYearlyCashValue(strIDNumber, "B") 'KT20180726
    '            If Me.lblYearlyAggregateCashValue.Text >= 320000 Then
    '                MsgBox("Yearly aggregate cash payment for (Bermuda) customer " + strCustID + " is >= HKD320,000")
    '            End If
    '        Else
    '            Me.lblYearlyAggregateCashValueAsur.Text = fetchYearlyCashValue(strIDNumber, "A") 
    '            If Me.lblYearlyAggregateCashValueAsur.Text >= 320000 Then
    '                MsgBox("Yearly aggregate cash payment for (Assurance) customer " + strCustID + " is >= HKD320,000")
    '            End If
    '        End If
    '    End If

    'End Sub

    Private Function fetchYearlyCashValue(ByVal strIDNumber As String, ByVal strType As String) As Decimal
        Dim strCCSConn As String = Utility.Utility.GetDbNameByCompanyEnv_CCS(If(strType = "B", g_Comp, g_LacComp), If(strType = "B", g_Env, g_LacEnv)) & ".."

        Dim decCashValue As Decimal

        Dim sqlConn As New SqlConnection
        sqlConn.ConnectionString = IIf(strType.Equals("B"), strCIWConn, strLIDZConn)
        sqlConn.Open()
        Dim strCompCode As String = IIf(strType.Equals("B"), "'ING','EAA','BMU'", "'LAC','LAH'")
        Dim sqlCmd As New SqlCommand
        If (Not String.IsNullOrEmpty(strIDNumber)) Then
            sqlCmd = New SqlCommand("select isnull(sum(pol.PPHD_POLAMTHK),0.00) as [amount] from csw_poli_rel rel  (nolock) " &
            "inner join PolicyAccount pa (nolock) on rel.PolicyAccountID=pa.PolicyAccountID and rel.PolicyRelateCode='PH' and pa.CompanyID in (" & strCompCode & ") " &
            "and rel.CustomerID in ( select customerid from customer where (GovernmentIDCard = '" & strIDNumber & "' or PassportNumber = '" & strIDNumber & "')) " &
            " left join " & strCCSConn & "CCSPOLDET_HIST pol (nolock) on pa.PolicyAccountID=pol.PPHD_POLICY  and LEN(pol.PPHD_RECEIPT)=7 and YEAR(pol.PPHD_LSTUPDDTE)=YEAR(GETDATE()) " &
            "inner join " & strCCSConn & "ccspaydet_hist pay (nolock) on pay.PHD_PAYTYPE='1' and pol.PPHD_RECEIPT=pay.PHD_RECEIPT", sqlConn)
        Else
            sqlCmd = New SqlCommand("select isnull(sum(pol.PPHD_POLAMTHK),0.00) as [amount] from csw_poli_rel rel  (nolock) " &
            "inner join PolicyAccount pa (nolock) on rel.PolicyAccountID=pa.PolicyAccountID and rel.PolicyRelateCode='PH' and pa.CompanyID in (" & strCompCode & ") " &
            "and rel.CustomerID=" & Trim(strCustID) &
            " left join " & strCCSConn & "CCSPOLDET_HIST pol (nolock) on pa.PolicyAccountID=pol.PPHD_POLICY  and LEN(pol.PPHD_RECEIPT)=7 and YEAR(pol.PPHD_LSTUPDDTE)=YEAR(GETDATE()) " &
            "inner join " & strCCSConn & "ccspaydet_hist pay (nolock) on pay.PHD_PAYTYPE='1' and pol.PPHD_RECEIPT=pay.PHD_RECEIPT", sqlConn)
        End If

        Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
        If sqlReader.Read() Then
            Try
                If Not sqlReader.IsDBNull(0) Then
                    decCashValue = sqlReader.GetDecimal(0)
                End If
            Catch sqlEx As SqlException
                MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
            Catch ex As Exception
                MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
            Finally
                sqlReader.Close()
                sqlConn.Close()
            End Try
        End If
        Return decCashValue
    End Function

    Private Function GetCustIDByID(ByVal strIDNumber As String, ByVal strType As String) As String
        Dim strRtnCustID As String = String.Empty

        Dim sqlConn As New SqlConnection
        sqlConn.ConnectionString = IIf(strType.Equals("B"), strCIWConn, strLIDZConn)
        sqlConn.Open()

        Dim sqlCmd As New SqlCommand("select customerid from customer where (GovernmentIDCard = '" & strIDNumber & "' or PassportNumber = '" & strIDNumber & "')", sqlConn)
        Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
        If sqlReader.Read() Then
            Try
                If Not sqlReader.IsDBNull(0) Then
                    strRtnCustID = DirectCast(sqlReader.GetSqlValue(0), System.Data.SqlTypes.SqlDecimal).Data(0).ToString()
                End If
            Catch sqlEx As SqlException
                MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
            Catch ex As Exception
                MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
            Finally
                sqlReader.Close()
                sqlConn.Close()
            End Try
        End If
        Return strRtnCustID
    End Function

    Private Sub PromptBdayAlert()
        Dim dtBDay As DateTime
        Dim dtAlertBday As DateTime

        dtBDay = FindBirthday(strCustID)

        Try
            dtAlertBday = New DateTime(Year(DateTime.Now), Month(dtBDay), Microsoft.VisualBasic.DateAndTime.Day(dtBDay))
        Catch ex As Exception
            dtAlertBday = New DateTime(Year(DateTime.Now), Month(dtBDay), Microsoft.VisualBasic.DateAndTime.Day(dtBDay) - 1)
        End Try


        If DateTime.Equals(DateTime.Today, dtAlertBday) Then
            MsgBox("Client's Birthday was on " & Format(dtBDay, "dd MMM yyyy") & ". ", MsgBoxStyle.Information, "Birthday Alert")
        End If

    End Sub


    'FindBirthday() - Find the birthday of client with specific customer id
    Private Function FindBirthday(ByVal strCustomerid As String) As DateTime
        Dim dtBday As New DateTime
        Dim sqlConn As New SqlConnection

        sqlConn.ConnectionString = strCIWConn
        sqlConn.Open()
        'Initialize birthday
        dtBday = New DateTime(1800, 1, 1)
        Dim sqlCmd As New SqlCommand("Select DateOfBirth From Customer Where Customerid = " & Trim(strCustomerid), sqlConn)
        Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
        If sqlReader.Read() Then
            Try
                If Not sqlReader.IsDBNull(0) Then
                    dtBday = sqlReader.GetDateTime(0)
                End If
            Catch sqlEx As SqlException
                MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
            Catch ex As Exception
                MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
            Finally
                sqlReader.Close()
                sqlConn.Close()
            End Try
        End If
        Return dtBday
        sqlConn.Dispose()
        sqlCmd.Dispose()
    End Function

    Private Sub grdPolicy_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPolicy.MouseUp

        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdPolicy.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdPolicy.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdPolicy.Select(hti.Row)
        End If

    End Sub

    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sRelateToCustomer As String = String.Empty
        Me.lblYearlyAggregateCashValue.Text = "0.00" 'KT20180726 label for yearly aggreate cash value
        Me.lblYearlyAggregateCashValueAsur.Text = "0.00"

        Try
            Try
                wndMain.Cursor = Cursors.WaitCursor
                strCompany = IIf(CompanyID.Equals("Bermuda"), "ING", "LAC")

                If blnIsBothComp Or (CompanyID.Equals("Bermuda") Or CompanyID.Equals("Assurance")) Then

                    'Oliver 2023-11-01 Added for CRS Enhancement(General Enhance Ph4) Point A - 10
                    CheckIsExistCustomerAlertTableByCustomerIDToPrompt(strCustID)

                    If blnUHNWCustomer And strCustID <> "" Then
                        InsertVVIPLog("", strCustID, "", "Customer Information (HK)", isUHNWMember)
                    End If

                    'Call SearchPolicy(IIf(blnIsBothComp, "", CompanyID))
                    Call SearchPolicy(strCompany)

                    Try
                        SearchResponse = CRS_Util.clsJSONTool.CallWSByCustID(strCustID) 'KT20161111
                        'select customerid from customer where (GovernmentIDCard = '" & strIDNumber & "' or PassportNumber = '" & strIDNumber & "')
                        'Dim EBCust As String = ""
                        'ifContainsEBpolicyByCustID(strCustID, EBCust)

                        'If String.IsNullOrEmpty(EBCust) Then
                        '    SearchResponse = CRS_Util.clsJSONTool.CallWSByCustID(strCustID) 'KT20161111
                        'Else
                        '    SearchResponse = CRS_Util.clsJSONTool.CallWSByCustID(EBCust)
                        'End If

                    Catch ex As Exception
                        'MsgBox("Loading Customer - " & ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error") 'Temp Comment 
                        appSettings.Logger.logger.Error("Loading Customer - " & ex.ToString)
                    End Try
                    'TODO: Fill GI, EB and MPF depends on CompanyID
                    'Call FillGI() 'Temp Comment
                    'Call FillEB() 'Temp Comment
                    'Call FillMPF() 'Temp Comment

                    Call FillGI()
                    Call FillEB()
                    Call FillMPF()

                    'Find EB EE related policy, 20160617
                    sRelateToCustomer = getRelateCustomer()
                    If sRelateToCustomer <> "" Then
                        FillEBByRelateToCustomer(sRelateToCustomer)
                    End If

                    If blnGI = False Then       ' ES01
                        'TODO: Build  GI, EB and MPF UIs depends on CompanyID
                        If blnIsBothComp Then
                            If (Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX")) Then
                                Call buildUI("ING")
                                Call buildUI("LAC")
                            Else
                                Call buildUI(strCompany)
                            End If
                        Else
                            Call buildUI(strCompany)
                        End If

                        Call buildGIUI() 'Temp Comment
                        Call buildEBUI() 'Temp Comment
                        Call buildMPFUI() 'Temp Comment
                        'Prompt birthday alert if today is client's birthday
                        PromptBdayAlert()

                        Call FillCust(strCompany)
                        Call FillCampaign()
                        tcCustomer.TabPages.Remove(tabMPF)

                    Else
                        Me.cmdSDetails.Enabled = False
                    End If

                    'Alex Th Lee 20200220 [ITSR1497 - GL29]
                    Call FillRemindTxt()
                Else

                    For Each t As TabPage In tcCustomer.TabPages
                        If Array.IndexOf(STAT_TABS, t.Name) < 0 AndAlso Array.IndexOf(ASUR_TABS, t.Name) < 0 Then
                            tcCustomer.TabPages.Remove(t)
                        End If
                    Next
                    cmdSDetails.Enabled = False
                    grdPolicy.Enabled = False
                    grdAsurPolicy.Enabled = False
                    grdGIPolicy.Enabled = False
                    grdEBPolicy.Enabled = False
                    grdMPFPolicy.Enabled = False
                End If

                'If CompanyID.Equals("Assurance") Then
                '    'Call SearchPOnCUST_Asur()
                '    'Call buildUI_Asur()
                '    Call SearchPolicy(CompanyID)
                '    Call buildUI(CompanyID)
                '    Call FillCust(CompanyID)
                'Else
                '    For Each t As TabPage In tcCustomer.TabPages
                '        If Array.IndexOf(STAT_TABS, t.Name) < 0 AndAlso Array.IndexOf(ASUR_TABS, t.Name) >= 0 Then
                '            tcCustomer.TabPages.Remove(t)
                '        End If
                '    Next
                '    grdAsurPolicy.Enabled = False
                'End If
                'aWeb = New frmWebView
            Catch ex As Exception
                MsgBox(ex.Message)
                'If InStr(LCase(ex.Message), "upgrade") Then
                '    End
                'End If
            End Try

        Finally
            wndMain.StatusBarPanel1.Text = ""
            wndMain.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal strComp As String = "Bermuda") Handles cmdOpen.Click

        'Dim strPolicy As String = ds.Tables("Result").Rows.Item(bm.Position).Item("PolicyAccountID")
        'ds.Tables("Result").Rows.Item(bm.Position)
        'Dim drI As DataRow = CType(IIF(strComp = "Bermuda", bm.Current, bm_A.Current), DataRowView).Row
        Dim drI As DataRow
        If strComp = "Bermuda" Then
            drI = CType(bm.Current, DataRowView).Row
        Else
            drI = CType(bm_A.Current, DataRowView).Row
        End If

        Dim strPolicy = UCase(RTrim(drI.Item("PolicyAccountID")))
        'Dim strCompany = IIf(CompanyID.Equals("Bermuda"), "ING", "LAC")
        Dim strCompanyID = UCase(RTrim(drI.Item("CompanyID")))
        Dim strRelateCode = UCase(RTrim(drI.Item("PolicyRelateCode")))

        If strPolicy <> "" Then

            'Dim w As New frmPolicy
            'w.PolicyAccountID = strPolicy
            'w.Text = "Policy " & strPolicy

            'If ShowWindow(w, wndMain, strPolicy) Then
            '    If w.NoRecord Then
            '        w.Close()
            '        w.Dispose()
            '    End If
            'End If
            ShowPolicy_Assurance(strPolicy, strCompanyID, blnUHNWCustomer, strCustID, strRelateCode)

        End If

    End Sub

    Private Sub grdPolicy_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPolicy.DoubleClick
        Call cmdOpen_Click(sender, e, "Bermuda")
    End Sub

    Private Sub frmCustomer_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Me.UclServiceLog1.PendingSave = True Then
            MsgBox("There are unsaved service log records, please save it first.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gSystem)
            e.Cancel = True
        ElseIf Me.UclServiceLog2.PendingSave = True Then
            MsgBox("There are unsaved service log (Assurance) records, please save it first.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gSystem)
            e.Cancel = True
        Else
            wndMain.StatusBarPanel1.Text = ""
            wndMain.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub FillCust(Optional ByVal strCompanyID As String = "ING")
        'Fill the customer relationship list
        Dim sqlconnect As New SqlConnection
        Dim strSQL, strConn As String
        Dim sqlda As SqlDataAdapter
        Dim custDT As New DataTable
        'oliver added 2024-6-27 for Assurance Production Version hot fix
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        If strCompanyID = "LAC" OrElse strCompanyID = "LAH" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
        ElseIf strCompanyID = "MCU" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        End If

        strConn = IIf(strCompanyID.Equals("ING"), strCIWConn, strLIDZConn)

        strSQL = "Select customerid, namesuffix, firstName, ChiLstNm+chifstnm as ChiName, 'Self' as Relationship, governmentidcard, convert(varchar,dateofbirth,101) as DOB, Gender, 0 as CustOrd " &
                 "From customer where customerid = '" & strCustID & "' " &
                 "UNION " &
                 "Select customerid, namesuffix, firstName as Name, ChiLstNm+chifstnm as ChiName, cswrld_desc as Relationship, governmentidcard, convert(varchar,dateofbirth,101) as DOB, Gender, 1 " &
                 "From " & serverPrefix & "csw_customer_rel, " & serverPrefix & "csw_customer_rel_code, customer where cswcrl_person1_id = '" & strCustID & "' " &
                 "And cswcrl_relationship = cswrld_relationship and customerid = cswcrl_person2_id " &
                 "UNION " &
                 "Select customerid, namesuffix, firstName as Name, ChiLstNm+chifstnm as ChiName, cswrld_desc_revr as Relationship, governmentidcard, convert(varchar,dateofbirth,101) as DOB, Gender, 1 " &
                 "From " & serverPrefix & "csw_customer_rel, " & serverPrefix & "csw_customer_rel_code, customer where cswcrl_person2_id = '" & strCustID & "' " &
                 "And cswcrl_relationship = cswrld_relationship and customerid = cswcrl_person1_id " &
                 "Order by CustOrd"

        sqlconnect.ConnectionString = strConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough
        sqlda.Fill(custDT)

        'add the records to the customer list
        custListView.Items.Clear()
        Me.custListView.Columns.Add("Customer ID", -2, HorizontalAlignment.Left)
        Me.custListView.Columns.Add("Last Name", -2, HorizontalAlignment.Left)
        Me.custListView.Columns.Add("First Name", -2, HorizontalAlignment.Left)
        Me.custListView.Columns.Add("Chinese Name", -2, HorizontalAlignment.Left)
        Me.custListView.Columns.Add("Relationship", -2, HorizontalAlignment.Left)
        Me.custListView.Columns.Add("HKID", -2, HorizontalAlignment.Left)
        Me.custListView.Columns.Add("Date of Birth", -2, HorizontalAlignment.Left)
        Me.custListView.Columns.Add("Gender", -2, HorizontalAlignment.Left)

        If Not custDT Is Nothing Then
            If custDT.Rows.Count > 0 Then
                For Each row As DataRow In custDT.Rows
                    Dim vItem As New ListViewItem(row.Item(0).ToString)
                    vItem.SubItems.Add(row.Item(1).ToString)
                    vItem.SubItems.Add(row.Item(2).ToString)
                    vItem.SubItems.Add(row.Item(3).ToString)
                    vItem.SubItems.Add(row.Item(4).ToString)
                    ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                    ''CRS 7x24 Changes - Start
                    'If ExternalUser And Not String.IsNullOrEmpty(row.Item(5)) Then
                    '    vItem.SubItems.Add(MaskExternalUserData(MaskData.HKID, row.Item(5).ToString))
                    'Else
                    '    vItem.SubItems.Add(row.Item(5).ToString)
                    'End If
                    vItem.SubItems.Add(row.Item(5).ToString)
                    vItem.SubItems.Add(row.Item(6).ToString)
                    vItem.SubItems.Add(row.Item(7).ToString)
                    custListView.Items.Add(vItem)
                Next
                custListView.Items(0).Selected = True
            End If
        End If

    End Sub

    Private Sub FillCampaign()
        'Fill the Campaign list
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim campDT As New DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        'oliver added 2024-6-27 for Assurance Production Version hot fix
        If strCompanyID.Equals("Bermuda") Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
        End If

        Me.campListView.Items.Clear()
        If Not campDT Is Nothing Then
            campDT.Clear()
        End If

        'select the campaign for the specified user 
        strSQL = "select crmcmp_campaign_id,crmcmp_campaign_name, " &
                 "convert(varchar,crmcmp_start_date,106),convert(varchar,crmcmp_end_date ,106) " &
                 "from " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_sales_leads " &
                 "where crmcsl_customer_id = '" & strCustID & "' and crmcmp_campaign_id = crmcsl_campaign_id "

        sqlconnect.ConnectionString = IIf(strCompanyID.Equals("Bermuda"), strCIWConn, strLIDZConn)

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        sqlda.Fill(campDT)

        'Add the campaign records to the Campaign List
        If Me.campListView.Columns.Count = 0 Then
            Me.campListView.Columns.Add("Campaign ID", -2, HorizontalAlignment.Left)
            Me.campListView.Columns.Add("Campaign Name", -2, HorizontalAlignment.Left)
            Me.campListView.Columns.Add("Start Date", -2, HorizontalAlignment.Left)
            Me.campListView.Columns.Add("End Date", -2, HorizontalAlignment.Left)
        End If

        If Not campDT Is Nothing Then
            If campDT.Rows.Count > 0 Then
                For Each row As DataRow In campDT.Rows
                    Dim vItem As New ListViewItem(row.Item(0).ToString)
                    vItem.SubItems.Add(row.Item(1).ToString)
                    vItem.SubItems.Add(row.Item(2).ToString)
                    vItem.SubItems.Add(row.Item(3).ToString)

                    Me.campListView.Items.Add(vItem)
                Next
            End If
        End If

        If Me.campListView.Items.Count > 0 Then
            Me.campListView.Items(0).Selected = True
        End If

    End Sub

    Private Sub cmdSDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSDetails.Click
        'Show details for the selected person

        If custListView.SelectedItems.Count <= 0 Then
            MsgBox("Please select a target Customer.")
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor
        Me.IDNumber = Me.custListView.SelectedItems(0).SubItems(5).Text
        Me.CustID(Me.custListView.SelectedItems(0).SubItems(5).Text) = Me.custListView.SelectedItems(0).SubItems(0).Text
        Me.UCID = ""
        Me.Text = $"Customer {Trim(Me.custListView.SelectedItems(0).SubItems(1).Text)} {Trim(Me.custListView.SelectedItems(0).SubItems(2).Text)} ({Me.custListView.SelectedItems(0).SubItems(0).Text}-Bermuda)"
        'reload all the data control information for the selected person
        reloadPerson()
        wndMain.Cursor = Cursors.Default

    End Sub

    Private Sub reloadPerson()
        Dim sRelateToCustomer As String = String.Empty
        Try
            If blnIsBothComp Or (CompanyID.Equals("Bermuda") Or CompanyID.Equals("Assurance")) Then
                'reload all the data control for the selected person
                resetDS()

                Call SearchPolicy(strCompany)
                'Temp Comment, TODO: Implement logic for differnet CompanyID
                SearchResponse = CRS_Util.clsJSONTool.CallWSByCustID(strCustID) 'KT20161111
                Call FillGI() 'Temp Comment
                Call FillMPF() 'Temp Comment
                Call FillEB() 'Temp Comment

                'Find EB EE related policy, 20160617
                sRelateToCustomer = getRelateCustomer()
                If sRelateToCustomer <> "" Then
                    FillEBByRelateToCustomer(sRelateToCustomer)
                End If

                If blnGI = False Then       ' ES01
                    If blnIsBothComp Then
                        If (Not String.IsNullOrEmpty(strIDNumber) AndAlso Not strIDNumber.ToUpper.StartsWith("XXX")) Then
                            Call buildUI("ING")
                            Call buildUI("LAC")
                        Else
                            Call buildUI(strCompany)
                        End If
                    Else
                        Call buildUI(strCompany)
                    End If
                    Call buildGIUI()
                    Call buildEBUI()
                    Call buildMPFUI()
                    'Prompt birthday alert if today is client's birthday
                    PromptBdayAlert()
                    wndMain.StatusBarPanel1.Text = ""
                    wndMain.Cursor = Cursors.Default

                    Call FillCampaign()
                End If

                'Alex Th Lee 20200220 [ITSR1497 - GL29]
                Call FillRemindTxt()

            Else
                For Each t As TabPage In tcCustomer.TabPages
                    If Array.IndexOf(STAT_TABS, t.Name) < 0 AndAlso Array.IndexOf(ASUR_TABS, t.Name) < 0 Then
                        tcCustomer.TabPages.Remove(t)
                    End If
                Next
                cmdSDetails.Enabled = False
                grdPolicy.Enabled = False
                grdAsurPolicy.Enabled = False
                grdGIPolicy.Enabled = False
                grdEBPolicy.Enabled = False
                grdMPFPolicy.Enabled = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message) 'Temp Comment
        End Try
    End Sub

    Private Sub resetDS()
        ds = Nothing
        ds = New DataSet("PolicyAccount")
        SearchResponse = Nothing

        Me.lblYearlyAggregateCashValue.Text = "0.00" 'KT20180726
        Me.lblYearlyAggregateCashValueAsur.Text = "0.00"

        'reset properties of User Controls
        Me.grdPolicy.TableStyles.Clear()
        Me.grdAsurPolicy.TableStyles.Clear()
        Me.UclGI1.resetDS()
        Me.UclSurvey1.resetDS()
        Me.UclAdditional1.resetDS()
        Me.UclMPF1.resetDS()
        Me.AddressSelect1.resetDS()
        Me.UclServiceLog1.resetDS()
        Me.UclServiceLog2.resetDS()
        Me.grdEBPolicy.TableStyles.Clear()
        Me.grdGIPolicy.TableStyles.Clear()
        Me.grdMPFPolicy.TableStyles.Clear()
    End Sub

    Private Sub campListView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles campListView.SelectedIndexChanged
        If Me.campListView.SelectedItems.Count > 0 Then
            'Me.UclCampaignTracking1.resetDS()
            Me.UclCampaignTracking1.CustID(campListView.SelectedItems(0).SubItems(0).Text) = strCustID
            Me.UclCampaignTracking1.ReadOnlyMode = True
        End If
    End Sub

    Private Sub custListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles custListView.SelectedIndexChanged
        If custListView.Items.Count > 0 Then
            If custListView.SelectedItems.Count > 0 Then
                UclCustomerRel1.HolderID(RTrim(Me.custListView.Items(0).SubItems(1).Text) & " " & Me.custListView.Items(0).SubItems(2).Text,
                    Me.custListView.SelectedItems(0).SubItems(0).Text,
                    Me.custListView.SelectedItems(0).SubItems(1).Text,
                    Me.custListView.SelectedItems(0).SubItems(2).Text,
                    Me.custListView.SelectedItems(0).SubItems(3).Text,
                    Me.custListView.SelectedItems(0).SubItems(4).Text,
                    Me.custListView.SelectedItems(0).SubItems(5).Text,
                    IIf(Trim(Me.custListView.SelectedItems(0).SubItems(6).Text) = "", #1/1/1900#, Me.custListView.SelectedItems(0).SubItems(6).Text),
                    Me.custListView.SelectedItems(0).SubItems(7).Text) = strCustID
            End If
        End If
    End Sub

    Private Sub custListView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles custListView.DoubleClick
        wndMain.Cursor = Cursors.WaitCursor
        Me.IDNumber = Me.custListView.SelectedItems(0).SubItems(5).Text
        Me.CustID(Me.custListView.SelectedItems(0).SubItems(4).Text) = Me.custListView.SelectedItems(0).SubItems(0).Text
        Me.UCID = ""
        'reload all the data control information for the selected person
        reloadPerson()
        wndMain.Cursor = Cursors.Default
    End Sub


    Private Sub buildGIUI()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 160
        cs.MappingName = "productDesc"
        cs.HeaderText = "Product Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "PolicyAccountID"
        cs.HeaderText = "Policy No."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "companyID"
        'cs.HeaderText = "Company ID"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "customerID"
        'cs.HeaderText = "Customer ID"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "productID"
        'cs.HeaderText = "Product ID"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("Product", ds.Tables("Product").Columns("Description"))
        'cs.Width = 220
        'cs.MappingName = "productDesc"
        'cs.HeaderText = "Product Description"
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "policyCurrency"
        'cs.HeaderText = "Product Currency"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "policyEffectiveDate"
        cs.HeaderText = "Eff. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "policyEndDate"
        cs.HeaderText = "Expiry Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("GLAccSts", ds.Tables("AccountStatusCodes").Columns("AccountStatus"))
        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "AccountStatus"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 90
        'cs.MappingName = "sumInsured"
        'cs.HeaderText = "Sum Insured"
        'cs.NullText = gNULLText
        'cs.Format = gNumFormat
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 90
        'cs.MappingName = "policyPremium"
        'cs.HeaderText = "Policy Premium"
        'cs.NullText = gNULLText
        'cs.Format = gNumFormat
        'ts.GridColumnStyles.Add(cs)




        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "group"
        ''cs.HeaderText = "Group"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "viewType"
        ''cs.HeaderText = "View Type"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        ts.MappingName = "GI"
        grdGIPolicy.TableStyles.Add(ts)

        grdGIPolicy.DataSource = ds.Tables("GI")
        grdGIPolicy.AllowDrop = False
        grdGIPolicy.ReadOnly = True

    End Sub


    Private Sub buildEBUI()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PolicyAccountID"
        cs.HeaderText = "Policy No."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 250
        cs.MappingName = "productDesc"
        cs.HeaderText = "Product Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "employeeCode"
        cs.HeaderText = "Employee Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "employeeName"
        cs.HeaderText = "Employee Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "InsuredName"
        cs.HeaderText = "Insured Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "RelateCode"
        cs.HeaderText = "Relationship"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "Gender"
        cs.HeaderText = "Sex"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "companyID"
        'cs.HeaderText = "Company ID"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "customerID"
        'cs.HeaderText = "Customer ID"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "productID"
        'cs.HeaderText = "Product ID"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)      


        'cs = New JoinTextBoxColumn("Product", ds.Tables("Product").Columns("Description"))
        'cs.Width = 220
        'cs.MappingName = "productDesc"
        'cs.HeaderText = "Product Description"
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "policyCurrency"
        'cs.HeaderText = "Product Currency"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "accountStatusCode"
        ''cs.HeaderText = "Account Status Code"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "policyEffectiveDate"
        cs.HeaderText = "Effective Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "policyEndDate"
        cs.HeaderText = "Expiry Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        'cs = New JoinTextBoxColumn("EBAccSts", ds.Tables("AccountStatusCodes").Columns("AccountStatus"))

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "planCode"
        cs.HeaderText = "Plan Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "AccountStatus"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 90
        'cs.MappingName = "sumInsured"
        'cs.HeaderText = "Sum Insured"
        'cs.NullText = gNULLText
        'cs.Format = gNumFormat
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 90
        'cs.MappingName = "policyPremium"
        'cs.HeaderText = "Policy Premium"
        'cs.NullText = gNULLText
        'cs.Format = gNumFormat
        'ts.GridColumnStyles.Add(cs)



        'cs = New DataGridTextBoxColumn
        'cs.Width = 80
        'cs.MappingName = "paidToDate"
        'cs.HeaderText = "Paid To Date"
        'cs.NullText = gNULLText
        'cs.Format = gDateFormat
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 80
        'cs.MappingName = "billToDate"
        'cs.HeaderText = "Bill To Date"
        'cs.NullText = gNULLText
        'cs.Format = gDateFormat
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "payModeCode"
        'cs.HeaderText = "Pay Mode Code"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        'cs.Width = 160
        'cs.MappingName = "payMode"
        'cs.HeaderText = "Pay Mode"
        'cs.NullText = gNULLText
        'ts.GridColumnStyles.Add(cs)

        ts.MappingName = "EB"
        grdEBPolicy.TableStyles.Add(ts)

        grdEBPolicy.DataSource = ds.Tables("EB")
        grdEBPolicy.AllowDrop = False
        grdEBPolicy.ReadOnly = True

    End Sub

    Private Sub buildMPFUI()
        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn


        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "memberType"
        cs.HeaderText = "Member Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "schemeType"
        cs.HeaderText = "scheme Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "accountNumber"
        cs.HeaderText = "Account No."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "joinDate"
        cs.HeaderText = "Date Joined Scheme"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "memberStatus"
        cs.HeaderText = "Member Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "fundBalance"
        cs.HeaderText = "Fund Balance"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 80
        ''cs.MappingName = "fundBalanceDate"
        ''cs.HeaderText = "Fund Balance Date"
        ''cs.NullText = gNULLText
        ''cs.Format = gDateFormat
        ''ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "lastName"
        ''cs.HeaderText = "Last Name"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "firstName"
        ''cs.HeaderText = "First Name"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "fullNamezh"
        ''cs.HeaderText = "Full Name"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "docId"
        ''cs.HeaderText = "Doc ID"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 160
        ''cs.MappingName = "gender"
        ''cs.HeaderText = "Gender"
        ''cs.NullText = gNULLText
        ''ts.GridColumnStyles.Add(cs)

        ''cs = New DataGridTextBoxColumn
        ''cs.Width = 90
        ''cs.MappingName = "dateOfBirth"
        ''cs.HeaderText = "Date Of Birth"
        ''cs.NullText = gNULLText
        ''cs.Format = gDateFormat
        ''ts.GridColumnStyles.Add(cs)

        ts.MappingName = "MPF"
        grdMPFPolicy.TableStyles.Add(ts)

        grdMPFPolicy.DataSource = ds.Tables("MPF")
        grdMPFPolicy.AllowDrop = False
        grdMPFPolicy.ReadOnly = True

    End Sub

    Private Sub FillGI()
        Dim _drGI As DataRow

        Try
            BuildGIDT()

            ds.Tables("GI").Clear()
            If SearchResponse.giPolicies IsNot Nothing Then
                If SearchResponse.giPolicies.Count > 0 Then
                    ConvertGIBO2DT(SearchResponse.giPolicies)
                End If
            End If

            'With ds.Tables("GI")
            '    _drGI = .NewRow()
            '    _drGI("policyAccountId") = "100092235    003"
            '    _drGI("group") = "F"
            '    .Rows.Add(_drGI)

            '    _drGI = .NewRow()
            '    _drGI("policyAccountId") = "400071220    000"
            '    _drGI("group") = "C"
            '    .Rows.Add(_drGI)

            'End With
            bmGI = Me.BindingContext(ds.Tables("GI"))
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub


    Private Sub ConvertGIBO2DT(ByVal aGIList As List(Of CRS_Util.clsJSONBusinessObj.clsGIPolicy)) 'KT20161111
        If ds.Tables("GI") Is Nothing Then Throw New Exception("GI Data Table Not Found")

        Dim _drGI As DataRow

        Try
            For Each objGI As CRS_Util.clsJSONBusinessObj.clsGIPolicy In aGIList 'KT20161111
                With ds.Tables("GI")
                    _drGI = .NewRow()

                    _drGI("policyAccountId") = objGI.policyAccountId
                    _drGI("companyID") = objGI.companyID
                    _drGI("customerId") = objGI.customerId
                    _drGI("productID") = objGI.productID
                    _drGI("productDesc") = objGI.productDescEn
                    _drGI("policyCurrency") = objGI.policyCurrency
                    _drGI("accountStatusCode") = objGI.accountStatusCode
                    _drGI("accountStatus") = objGI.accountStatus
                    _drGI("sumInsured") = IIf(objGI.sumInsured.HasValue, objGI.sumInsured, DBNull.Value)
                    _drGI("policyPremium") = IIf(objGI.policyPremium.HasValue, objGI.policyPremium, DBNull.Value)
                    _drGI("policyEffectiveDate") = IIf(objGI.policyEffectiveDate.HasValue, objGI.policyEffectiveDate, DBNull.Value)
                    _drGI("policyEndDate") = IIf(objGI.policyEndDate.HasValue, objGI.policyEndDate, DBNull.Value)
                    _drGI("group") = objGI.group
                    _drGI("viewType") = objGI.viewType
                    _drGI("type") = objGI.type

                    .Rows.Add(_drGI)
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub BuildGIDT()
        If ds.Tables("GI") IsNot Nothing Then Exit Sub

        Dim dt As New DataTable("GI")

        With dt
            .Columns.Add(New DataColumn("policyAccountId", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("companyID", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("customerId", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("productID", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("productDesc", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("policyCurrency", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("accountStatusCode", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("accountStatus", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("sumInsured", System.Type.[GetType]("System.Decimal")))
            .Columns.Add(New DataColumn("policyPremium", System.Type.[GetType]("System.Decimal")))
            .Columns.Add(New DataColumn("policyEffectiveDate", System.Type.[GetType]("System.DateTime")))
            .Columns.Add(New DataColumn("policyEndDate", System.Type.[GetType]("System.DateTime")))
            .Columns.Add(New DataColumn("group", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("viewType", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("type", System.Type.[GetType]("System.String")))
        End With

        ds.Tables.Add(dt)

        'If ds.Tables("AccountStatusCodes") IsNot Nothing Then
        '    Dim relGLAccSts As New Data.DataRelation("GLAccSts", ds.Tables("AccountStatusCodes").Columns("AccountStatusCode"), _
        '    ds.Tables("GI").Columns("AccountStatusCode"), True)

        '    Try
        '        ds.Relations.Add(relGLAccSts)
        '    Catch sqlex As SqlClient.SqlException
        '        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    End Try
        'End If


    End Sub

    Private Sub FillEB()
        Try
            BuildEBDT()

            ds.Tables("EB").Clear()
            If SearchResponse.ebPolicies IsNot Nothing Then
                If SearchResponse.ebPolicies.Count > 0 Then
                    ConvertEBBO2DT(SearchResponse.ebPolicies)
                End If
            End If

            bmEB = Me.BindingContext(ds.Tables("EB"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub ConvertEBBO2DT(ByVal aEBList As List(Of CRS_Util.clsJSONBusinessObj.clsEBPolicy)) 'KT20161111
        If ds.Tables("EB") Is Nothing Then Throw New Exception("EB Data Table Not Found")

        Dim _drEB As DataRow

        Try
            For Each objEB As CRS_Util.clsJSONBusinessObj.clsEBPolicy In aEBList 'KT20161111
                'If BlockPolicy(Microsoft.VisualBasic.Left(objEB.policyAccountId, 12).Trim) = True Then 'remove policy in block list , 2016.5.26
                With ds.Tables("EB")
                    _drEB = .NewRow()

                    _drEB("policyAccountId") = objEB.policyAccountId
                    _drEB("productDesc") = objEB.productDesc
                    _drEB("employeeCode") = objEB.employeeCode
                    _drEB("employeeName") = objEB.employeeName
                    _drEB("insuredName") = objEB.insuredName
                    _drEB("relateCode") = objEB.relateCode

                    _drEB("companyID") = objEB.companyID
                    _drEB("customerId") = objEB.customerId
                    _drEB("productID") = objEB.productID
                    _drEB("productDesc") = objEB.productDesc
                    _drEB("productDescZh") = objEB.productDescZh

                    '_drEB("policyCurrency") = objEB.policyCurrency
                    '_drEB("accountStatusCode") = objEB.accountStatusCode
                    _drEB("accountStatus") = objEB.accountStatus
                    '_drEB("sumInsured") = IIf(objEB.sumInsured.HasValue, objEB.sumInsured, DBNull.Value)
                    '_drEB("policyPremium") = IIf(objEB.policyPremium.HasValue, objEB.policyPremium, DBNull.Value)
                    _drEB("policyEffectiveDate") = IIf(objEB.policyEffectiveDate.HasValue, objEB.policyEffectiveDate, DBNull.Value)
                    _drEB("policyEndDate") = IIf(objEB.policyEndDate.HasValue, objEB.policyEndDate, DBNull.Value)
                    '_drEB("paidToDate") = IIf(objEB.paidToDate.HasValue, objEB.paidToDate, DBNull.Value)
                    '_drEB("billToDate") = IIf(objEB.billToDate.HasValue, objEB.billToDate, DBNull.Value)
                    '_drEB("payModeCode") = objEB.payModeCode
                    '_drEB("payMode") = objEB.payMode

                    _drEB("planCode") = objEB.planCode
                    _drEB("docId") = objEB.docId
                    _drEB("relateToCustomer") = objEB.relateToCustomer
                    _drEB("gender") = objEB.gender
                    _drEB("type") = objEB.type

                    .Rows.Add(_drEB)
                End With
                'End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub BuildEBDT()
        If ds.Tables("EB") IsNot Nothing Then Exit Sub

        Dim dt As New DataTable("EB")

        With dt
            .Columns.Add(New DataColumn("policyAccountId", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("companyID", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("customerId", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("productID", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("productDesc", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("productDescZh", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("employeeName", System.Type.[GetType]("System.String")))
            '.Columns.Add(New DataColumn("policyCurrency", System.Type.[GetType]("System.String")))
            '.Columns.Add(New DataColumn("accountStatusCode", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("accountStatus", System.Type.[GetType]("System.String")))
            '.Columns.Add(New DataColumn("sumInsured", System.Type.[GetType]("System.Decimal")))
            '.Columns.Add(New DataColumn("policyPremium", System.Type.[GetType]("System.Decimal")))
            .Columns.Add(New DataColumn("policyEffectiveDate", System.Type.[GetType]("System.DateTime")))
            .Columns.Add(New DataColumn("policyEndDate", System.Type.[GetType]("System.DateTime")))
            '.Columns.Add(New DataColumn("paidToDate", System.Type.[GetType]("System.DateTime")))
            '.Columns.Add(New DataColumn("billToDate", System.Type.[GetType]("System.DateTime")))
            '.Columns.Add(New DataColumn("payModeCode", System.Type.[GetType]("System.String")))
            '.Columns.Add(New DataColumn("payMode", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("relateToCustomer", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("relateCode", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("insuredName", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("gender", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("type", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("docId", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("employeeCode", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("planCode", System.Type.[GetType]("System.String")))
        End With

        ds.Tables.Add(dt)

        'If ds.Tables("AccountStatusCodes") IsNot Nothing Then
        '    Dim relEBAccSts As New Data.DataRelation("EBAccSts", ds.Tables("AccountStatusCodes").Columns("AccountStatusCode"), _
        '    ds.Tables("EB").Columns("AccountStatusCode"), True)

        '    Try
        '        ds.Relations.Add(relEBAccSts)
        '    Catch sqlex As SqlClient.SqlException
        '        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    End Try
        'End If
    End Sub


    Private Sub FillMPF()
        Try
            BuildMPFDT()

            ds.Tables("MPF").Clear()

            If SearchResponse.pensions IsNot Nothing Then
                If SearchResponse.pensions.Count > 0 Then
                    ConvertMPFBO2DT(SearchResponse.pensions)
                End If
            End If

            bmMPF = Me.BindingContext(ds.Tables("MPF"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub ConvertMPFBO2DT(ByVal aMPFList As List(Of CRS_Util.clsJSONBusinessObj.clsPensionPolicy)) 'KT20161111
        If ds.Tables("MPF") Is Nothing Then Throw New Exception("MPF Data Table Not Found")

        Dim _drMPF As DataRow

        Try
            For Each objMPF As CRS_Util.clsJSONBusinessObj.clsPensionPolicy In aMPFList 'KT20161111
                With ds.Tables("MPF")
                    _drMPF = .NewRow()

                    _drMPF("memberType") = objMPF.memberType
                    _drMPF("schemeType") = objMPF.schemeType
                    _drMPF("accountNumber") = objMPF.accountNumber
                    _drMPF("joinDate") = IIf(objMPF.joinDate.HasValue, objMPF.joinDate, DBNull.Value)
                    _drMPF("memberStatus") = objMPF.memberStatus
                    _drMPF("fundBalance") = IIf(objMPF.fundBalance.HasValue, objMPF.fundBalance, DBNull.Value)
                    _drMPF("fundBalanceDate") = IIf(objMPF.fundBalanceDate.HasValue, objMPF.fundBalanceDate, DBNull.Value)
                    _drMPF("lastName") = objMPF.lastName
                    _drMPF("firstName") = objMPF.firstName
                    _drMPF("fullnameZh") = objMPF.fullnameZh
                    _drMPF("docId") = objMPF.docId
                    _drMPF("gender") = objMPF.gender
                    _drMPF("dateOfBirth") = IIf(objMPF.dateOfBirth.HasValue, objMPF.dateOfBirth, DBNull.Value)

                    .Rows.Add(_drMPF)
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub BuildMPFDT()
        If ds.Tables("MPF") IsNot Nothing Then Exit Sub

        Dim dt As New DataTable("MPF")

        With dt

            .Columns.Add(New DataColumn("memberType", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("schemeType", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("accountNumber", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("joinDate", System.Type.[GetType]("System.DateTime")))
            .Columns.Add(New DataColumn("memberStatus", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("fundBalance", System.Type.[GetType]("System.Decimal")))
            .Columns.Add(New DataColumn("fundBalanceDate", System.Type.[GetType]("System.DateTime")))
            .Columns.Add(New DataColumn("lastName", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("firstName", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("fullnameZh", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("docId", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("gender", System.Type.[GetType]("System.String")))
            .Columns.Add(New DataColumn("dateOfBirth", System.Type.[GetType]("System.DateTime")))

        End With

        ds.Tables.Add(dt)
    End Sub


    Private Sub grdGIPolicy_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdGIPolicy.MouseUp

        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdGIPolicy.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdGIPolicy.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdGIPolicy.Select(hti.Row)
        End If

    End Sub

    Private Sub grdGIPolicy_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdGIPolicy.DoubleClick
        ''open web       
        If bmGI.Count = 0 Then Exit Sub
        'Dim aweb As New frmWebView
        Dim aCursor = Me.Cursor
        Try
            Try
                Dim drI As DataRow = CType(bmGI.Current, DataRowView).Row
                Dim strPolicy = RTrim(drI.Item("PolicyAccountID"))
                Dim strType = RTrim(drI.Item("type"))

                Me.Cursor = Cursors.WaitCursor

                Try
                    IE.Quit()
                    IE = Nothing
                    CREATE_IE()
                Catch ex As Exception
                    CREATE_IE()
                End Try



                IE.Top = 100
                IE.Left = 100

                IE.Navigate(CRS_Util.clsJSONTool.GetGIDetailPageURL(strCustID, strPolicy, strType), Nothing, Nothing, Nothing, Nothing) 'KT20161111


                IE.Height = 800
                IE.Width = 1000
                IE.AddressBar = False
                IE.StatusBar = False
                IE.ToolBar = False
                IE.MenuBar = False
                'IE.Visible = False
                IE.Visible = True

                BringWindowToTop(IE.HWND)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Finally
            Me.Cursor = aCursor
        End Try

    End Sub

    Private Sub grdEBPolicy_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdEBPolicy.MouseUp

        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdEBPolicy.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdEBPolicy.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdEBPolicy.Select(hti.Row)
        End If
    End Sub

    Private Sub grdEBPolicy_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdEBPolicy.DoubleClick
        'open web 
        If bmEB.Count = 0 Then Exit Sub

        Dim aCursor = Me.Cursor
        'Dim aWeb As New frmWebView
        Try
            Try
                Dim drI As DataRow = CType(bmEB.Current, DataRowView).Row
                Dim strPolicy = RTrim(drI.Item("PolicyAccountID"))
                'Dim strType = RTrim(drI.Item("Type"))
                'Dim strGroup As String = String.Empty
                Dim strType = RTrim(drI.Item("CompanyID"))
                Dim strRelateCode = RTrim(drI.Item("relateCode"))

                'If Not BlockPolicy(Microsoft.VisualBasic.Left(strPolicy, 12).Trim) Then
                '    MsgBox("FWD staff / special handle policy ¡V please contact EB.", MsgBoxStyle.Information)
                '    Exit Sub
                'End If

                'If strType <> "" And strRelateCode = "PI" Then
                '    strGroup = "insuredEE"
                'ElseIf strType <> "" And strRelateCode <> "PI" Then
                '    strGroup = "companyHR"
                'Else
                '    MsgBox("Invalid EB Type")
                '    Exit Sub
                'End If
                Me.Cursor = Cursors.WaitCursor
                Try
                    IE.Quit()
                    IE = Nothing
                    CREATE_IE()
                Catch ex As Exception
                    CREATE_IE()
                End Try
                IE.Top = 100
                IE.Left = 100
                IE.Height = 800
                IE.Width = 1000



                IE.Navigate(CRS_Util.clsJSONTool.GetEBDetailPageURL(strCustID, strPolicy, strType)) 'KT20161111
                IE.AddressBar = False
                IE.StatusBar = False
                IE.ToolBar = False
                IE.MenuBar = False
                'IE.Visible = False
                IE.Visible = True

                BringWindowToTop(IE.HWND)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Finally
            Me.Cursor = aCursor
        End Try
    End Sub

    Private Sub grdMPFPolicy_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMPFPolicy.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdMPFPolicy.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdMPFPolicy.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdMPFPolicy.Select(hti.Row)
        End If
    End Sub

    Private Function getRelateCustomer() As String
        Dim dtResult As New DataTable
        Dim strErr As String = ""

        Try

            Dim sSQL As String = "select RelateToCustomer from PolicyCustomerInfo where CustomerID='{0}' and RelateCode<>'EE' "

            sSQL = String.Format(sSQL, strCustID)
            If GetDT(sSQL, IIf(strCompanyID.Equals("Bermuda"), strCIWConn, strLIDZConn), dtResult, strErr) Then
                If Not dtResult Is Nothing AndAlso dtResult.Rows.Count > 0 Then
                    getRelateCustomer = dtResult.Rows(0)("RelateToCustomer")
                Else
                    getRelateCustomer = ""
                End If
            End If

            Return getRelateCustomer

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function

    Private Sub FillEBByRelateToCustomer(ByVal sCustomerID As String)
        Try
            SearchResponse = CRS_Util.clsJSONTool.CallWSByCustID(sCustomerID) 'KT20161111

            If SearchResponse.ebPolicies IsNot Nothing Then
                If SearchResponse.ebPolicies.Count > 0 Then
                    ConvertEBBO2DT(SearchResponse.ebPolicies)
                End If
            End If

            bmEB = Me.BindingContext(ds.Tables("EB"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    'oliver 2024-5-3 added for Table_Relocate_Sprint13
    Private Function CheckAcknowledgement(ByVal sPolicy As String) As Boolean

        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(If(strCompanyID.Equals("Bermuda"), g_Comp, g_McuComp), "FRM_CUST_ACKNOWLEDGEMENT",
             New Dictionary(Of String, String)() From {
            {"sPolicy1", sPolicy},
            {"sPolicy2", sPolicy}
            })
        Dim dt As DataTable = retDs.Tables(0).Copy
        Try
            ds.Tables.Remove("Acknowledgement")
        Catch ex As Exception

        End Try
        ds.Tables.Add(dt)

        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Alex Th Lee 20200220 [ITSR1497 - GL29]
    'Private Function CheckAcknowledgement(ByVal sPolicy As String) As Boolean
    '    Dim sqlconnect As New SqlConnection
    '    Dim strSQL As String
    '    Dim lngCnt As Long
    '    Dim sqlda As SqlDataAdapter

    '    sqlconnect.ConnectionString = IIf(strCompanyID.Equals("Bermuda"), strCIWConn, strLIDZConn)
    '    Dim command = New SqlCommand("select ciwpdf_policy_no as PolicyNo from ciw_policydoc_delivery_form where ciwpdf_receive_date is not null and ciwpdf_policy_no = @sPolicy1 UNION select Ciwpn_policy_no from ciw_policy_notes where ciwpn_type = 'PolRcpt' and Ciwpn_policy_no = @sPolicy2", sqlconnect)
    '    command.Parameters.AddWithValue("@sPolicy1", sPolicy)
    '    command.Parameters.AddWithValue("@sPolicy2", sPolicy)
    '    sqlda = New SqlDataAdapter(command)
    '    Try
    '        ds.Tables.Remove("Acknowledgement")
    '    Catch ex As Exception

    '    End Try
    '    sqlda.Fill(ds, "Acknowledgement")

    '    If ds.Tables("Acknowledgement").Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    'Alex Th Lee 20200220 [ITSR1497 - GL29]
    Private Sub FillRemindTxt()
        Try
            Dim sPolicy As String
            sPolicy = ""
            'Life
            If Not ds.Tables("POLST") Is Nothing Then
                If ds.Tables("POLST").Rows.Count > 0 Then
                    For Each row As DataRow In ds.Tables("POLST").Rows
                        If Not CheckAcknowledgement(row.Item(0).ToString) Then
                            If Not sPolicy.Contains(row.Item(0).ToString) Then
                                sPolicy &= row.Item(0).ToString & Environment.NewLine
                            End If
                        End If
                    Next
                End If
            End If
            'GI
            If Not ds.Tables("GI") Is Nothing Then
                If ds.Tables("GI").Rows.Count > 0 Then
                    For Each row As DataRow In ds.Tables("GI").Rows
                        If Not CheckAcknowledgement(row.Item(0).ToString) Then
                            If Not sPolicy.Contains(row.Item(0).ToString) Then
                                sPolicy &= row.Item(0).ToString & Environment.NewLine
                            End If
                        End If
                    Next
                End If
            End If
            If sPolicy.Length > 0 Then
                txtRemind.Text = "Reminding that following policy were not yet Acknowledgement" & Environment.NewLine & sPolicy
            Else
                txtRemind.Text = ""
            End If
        Catch ex As Exception
            txtRemind.Text = "Get Remind Error"
        End Try
    End Sub

    Private Sub SearchPOnCust_Asur()
        Dim strErrMsg As String

        ds_A = GetPolicyList_Asur(strUCID, "POLSTINF", True, lngErr, strErr)
        ds_A.Tables("POLSTINF").DefaultView.Sort = "RELATE_CODE, Status, POLICY"
        grdAsurPolicy.DataSource = ds_A.Tables("POLSTINF")
        bm_A = Me.BindingContext(ds_A.Tables("POLSTINF"))


        dtCust_Asur = New DataTable("Customer")
        dtCust_Ext_Asur = New DataTable("Customer_Ext")
        If Not GetPersonalInfo_Asur(strUCID, dtCust_Asur, dtCust_Ext_Asur, strErrMsg) Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        If dtCust_Asur IsNot Nothing Then
            Personal_Asur1.CustID(dtCust_Asur, dtCust_Ext_Asur) = UCID
        End If
    End Sub

    Private Sub buildUI_Asur()
        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn


        cs = New DataGridTextBoxColumn
        cs.Width = 160
        cs.MappingName = "PRODUCT"
        cs.HeaderText = "Product Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "POLICY"
        cs.HeaderText = "Policy No."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "RELATE_CODE"
        cs.HeaderText = "Relate Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Role"
        cs.HeaderText = "Role"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "EFFDATE"
        cs.HeaderText = "Eff. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "SINSTAMT06"
        cs.HeaderText = "Modal Prem."
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PTDATE"
        cs.HeaderText = "PTD"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Status"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "AGNTNUM"
        cs.HeaderText = "Servicing Agent"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLSTINF"
        grdAsurPolicy.TableStyles.Add(ts)

        grdAsurPolicy.DataSource = ds_A.Tables("POLSTINF")
        grdAsurPolicy.AllowDrop = False
        grdAsurPolicy.ReadOnly = True

    End Sub



    Private Function GetPersonalInfo_Asur(ByVal strUCID As String, ByRef dtCustInfo As DataTable, ByRef dtCustInfo_Ext As DataTable, ByRef strErr As String)
        Dim sqlconnect As New SqlConnection
        Dim sqlDA As New SqlDataAdapter
        Dim dsCustInfo As DataSet

        lngErr = 0
        strErr = ""

        dsCustInfo = GetPersonalDetail_Asur(strUCID, "Customer", lngErr, strErr)
        dtCustInfo = dsCustInfo.Tables(0)
        dsCustInfo.Tables.RemoveAt(0)

        Dim strSQL As String
        Try
            sqlconnect.ConnectionString = strCIWConn
            sqlconnect.Open()

            strSQL = "select UCID, Rating, Remark from [Cust_Ext_Asur] where UCID='" & strUCID & "'"

            sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
            sqlDA.Fill(dtCustInfo_Ext)

        Catch err As SqlException
            lngErr = -1
            strErr = "GetPersonalInfo_Asur - " & err.ToString()
        Catch ex As Exception
            lngErr = -1
            strErr = "GetPersonalInfo_Asur - " & ex.ToString()

        Finally
            sqlconnect.Dispose()
        End Try

        Return lngErr >= 0

    End Function

    ''' <summary>
    ''' Open assurance policy when double clicking
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-13
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub grdAsurPolicy_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAsurPolicy.DoubleClick
        'Call cmdOpenAsur_Click(sender, e)
        Call cmdOpen_Click(sender, e, "Assurance")
    End Sub

    Private Sub ifContainsEBpolicyByCustID(ByVal custid As String, ByRef EBcustid As String)
        Dim sqlconnect As New SqlConnection
        Dim sqlDA As New SqlDataAdapter
        Dim strSQL As String
        Dim dtcustinfo As DataTable = New DataTable()

        'select customerid from customer where (GovernmentIDCard = '" & strIDNumber & "' or PassportNumber = '" & strIDNumber & "')
        sqlconnect.ConnectionString = strCIWConn

        Using sqlconnect
            sqlconnect.Open()
            strSQL = "SELECT DISTINCT c.customerid
FROM customer c
INNER JOIN csw_poli_rel r ON r.customerid = c.customerid AND r.policyrelatecode = 'PI'
INNER JOIN policyaccount p ON p.policyaccountid = r.policyaccountid AND p.CompanyID = 'EB'
INNER JOIN customer c2 ON c.GovernmentIDCard = c2.GovernmentIDCard OR c.PassportNumber = c2.GovernmentIDCard
WHERE c2.customerid = " & custid

            sqlDA = New SqlDataAdapter(strSQL, sqlconnect)
            sqlDA.Fill(dtcustinfo)
            If dtcustinfo.Rows.Count > 0 Then
                EBcustid = dtcustinfo.Rows(0)(0).ToString()
            Else
                EBcustid = ""
            End If

        End Using


    End Sub

    ''' <summary>
    ''' Enable selecting row when clicking assurance policy grid control
    ''' </summary>
    ''' <remarks>
    ''' Added by Hugo Chan on 2021-05-31
    ''' Project: CRS - First Level of Access
    ''' </remarks>
    Private Sub grdAsurPolicy_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAsurPolicy.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAsurPolicy.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAsurPolicy.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAsurPolicy.Select(hti.Row)
        End If
    End Sub
End Class
