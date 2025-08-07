Imports System.Data.SqlClient

Public Class uclCampaign
    Inherits System.Windows.Forms.UserControl

    Private dsCampaign As DataSet = New DataSet("Campaign")
    'Private dtCampaign, dtChannel As DataTable
    Private blnAdmin As Boolean
    Private blnNew, blnNewChannel As Boolean
    Private blnChange, blnChangeChannel As Boolean
    Private WithEvents bm As BindingManagerBase
    Private WithEvents bmChannel As BindingManagerBase

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
    Friend WithEvents grdCampaign As System.Windows.Forms.DataGrid
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grdChannel As System.Windows.Forms.DataGrid
    Friend WithEvents lblAdditionalInfo As System.Windows.Forms.Label
    Friend WithEvents lblOffer As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents lblManPower As System.Windows.Forms.Label
    Friend WithEvents lblOwner As System.Windows.Forms.Label
    Friend WithEvents lblObjective As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents tabCampaign As System.Windows.Forms.TabControl
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents txtObjective As System.Windows.Forms.TextBox
    Friend WithEvents txtCampName As System.Windows.Forms.TextBox
    Friend WithEvents txtCampID As System.Windows.Forms.TextBox
    Friend WithEvents txtOffer As System.Windows.Forms.TextBox
    Friend WithEvents cboOwner As System.Windows.Forms.ComboBox
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTarget As System.Windows.Forms.Label
    Friend WithEvents cboTarget As System.Windows.Forms.ComboBox
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblBgtCost As System.Windows.Forms.Label
    Friend WithEvents txtBgtCost As System.Windows.Forms.TextBox
    Friend WithEvents txtManPower As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents lblActCost As System.Windows.Forms.Label
    Friend WithEvents txtActCost As System.Windows.Forms.TextBox
    Friend WithEvents cboChannel As System.Windows.Forms.ComboBox
    Friend WithEvents txtChannelOffer As System.Windows.Forms.TextBox
    Friend WithEvents txtChannelBgtCost As System.Windows.Forms.TextBox
    Friend WithEvents txtChannelManPower As System.Windows.Forms.TextBox
    Friend WithEvents txtChannelDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtChannelActCost As System.Windows.Forms.TextBox
    Friend WithEvents txtChannelCallScript As System.Windows.Forms.TextBox
    Friend WithEvents cboChannelStatus As System.Windows.Forms.ComboBox
    Friend WithEvents tabCampInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabCampChannel As System.Windows.Forms.TabPage
    Friend WithEvents cmdChannelEdit As System.Windows.Forms.Button
    Friend WithEvents cmdChannelSave As System.Windows.Forms.Button
    Friend WithEvents cmdChannelCancel As System.Windows.Forms.Button
    Friend WithEvents cmdChannelDel As System.Windows.Forms.Button
    Friend WithEvents cmdChannelAdd As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblChannelStatus As System.Windows.Forms.Label
    Friend WithEvents lblChannelOffer As System.Windows.Forms.Label
    Friend WithEvents lblChannelManpower As System.Windows.Forms.Label
    Friend WithEvents lblChannelCallScript As System.Windows.Forms.LinkLabel
    Friend WithEvents lblChannelChannels As System.Windows.Forms.Label
    Friend WithEvents lblChannelBgtCost As System.Windows.Forms.Label
    Friend WithEvents lblChannelDesc As System.Windows.Forms.Label
    Friend WithEvents lblChannelActCost As System.Windows.Forms.Label
    Friend WithEvents cmdUpdRating As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdCampaign = New System.Windows.Forms.DataGrid
        Me.tabCampaign = New System.Windows.Forms.TabControl
        Me.tabCampInfo = New System.Windows.Forms.TabPage
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.txtOffer = New System.Windows.Forms.TextBox
        Me.cboOwner = New System.Windows.Forms.ComboBox
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker
        Me.lblAdditionalInfo = New System.Windows.Forms.Label
        Me.lblOffer = New System.Windows.Forms.Label
        Me.lblTarget = New System.Windows.Forms.Label
        Me.cboTarget = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.cboStatus = New System.Windows.Forms.ComboBox
        Me.lblEndDate = New System.Windows.Forms.Label
        Me.lblStartDate = New System.Windows.Forms.Label
        Me.lblBgtCost = New System.Windows.Forms.Label
        Me.txtBgtCost = New System.Windows.Forms.TextBox
        Me.lblManPower = New System.Windows.Forms.Label
        Me.txtManPower = New System.Windows.Forms.TextBox
        Me.lblOwner = New System.Windows.Forms.Label
        Me.lblDesc = New System.Windows.Forms.Label
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.lblActCost = New System.Windows.Forms.Label
        Me.txtActCost = New System.Windows.Forms.TextBox
        Me.lblObjective = New System.Windows.Forms.Label
        Me.txtObjective = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCampName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCampID = New System.Windows.Forms.TextBox
        Me.tabCampChannel = New System.Windows.Forms.TabPage
        Me.cmdChannelEdit = New System.Windows.Forms.Button
        Me.cmdChannelSave = New System.Windows.Forms.Button
        Me.cmdChannelCancel = New System.Windows.Forms.Button
        Me.cmdChannelDel = New System.Windows.Forms.Button
        Me.cmdChannelAdd = New System.Windows.Forms.Button
        Me.cboChannelStatus = New System.Windows.Forms.ComboBox
        Me.lblChannelStatus = New System.Windows.Forms.Label
        Me.grdChannel = New System.Windows.Forms.DataGrid
        Me.cboChannel = New System.Windows.Forms.ComboBox
        Me.txtChannelOffer = New System.Windows.Forms.TextBox
        Me.lblChannelOffer = New System.Windows.Forms.Label
        Me.lblChannelBgtCost = New System.Windows.Forms.Label
        Me.txtChannelBgtCost = New System.Windows.Forms.TextBox
        Me.lblChannelManpower = New System.Windows.Forms.Label
        Me.txtChannelManPower = New System.Windows.Forms.TextBox
        Me.lblChannelDesc = New System.Windows.Forms.Label
        Me.txtChannelDesc = New System.Windows.Forms.TextBox
        Me.lblChannelActCost = New System.Windows.Forms.Label
        Me.txtChannelActCost = New System.Windows.Forms.TextBox
        Me.lblChannelCallScript = New System.Windows.Forms.LinkLabel
        Me.txtChannelCallScript = New System.Windows.Forms.TextBox
        Me.lblChannelChannels = New System.Windows.Forms.Label
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.cmdUpdRating = New System.Windows.Forms.Button
        CType(Me.grdCampaign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCampaign.SuspendLayout()
        Me.tabCampInfo.SuspendLayout()
        Me.tabCampChannel.SuspendLayout()
        CType(Me.grdChannel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdCampaign
        '
        Me.grdCampaign.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCampaign.BackColor = System.Drawing.Color.White
        Me.grdCampaign.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCampaign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCampaign.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCampaign.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCampaign.CaptionVisible = False
        Me.grdCampaign.DataMember = ""
        Me.grdCampaign.FlatMode = True
        Me.grdCampaign.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCampaign.ForeColor = System.Drawing.Color.Black
        Me.grdCampaign.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCampaign.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCampaign.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCampaign.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCampaign.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCampaign.Location = New System.Drawing.Point(4, 4)
        Me.grdCampaign.Name = "grdCampaign"
        Me.grdCampaign.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCampaign.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCampaign.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCampaign.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCampaign.Size = New System.Drawing.Size(704, 160)
        Me.grdCampaign.TabIndex = 0
        '
        'tabCampaign
        '
        Me.tabCampaign.Controls.Add(Me.tabCampInfo)
        Me.tabCampaign.Controls.Add(Me.tabCampChannel)
        Me.tabCampaign.Location = New System.Drawing.Point(4, 168)
        Me.tabCampaign.Name = "tabCampaign"
        Me.tabCampaign.SelectedIndex = 0
        Me.tabCampaign.Size = New System.Drawing.Size(704, 276)
        Me.tabCampaign.TabIndex = 91
        '
        'tabCampInfo
        '
        Me.tabCampInfo.Controls.Add(Me.cmdUpdRating)
        Me.tabCampInfo.Controls.Add(Me.cmdEdit)
        Me.tabCampInfo.Controls.Add(Me.cmdExit)
        Me.tabCampInfo.Controls.Add(Me.cmdSave)
        Me.tabCampInfo.Controls.Add(Me.cmdCancel)
        Me.tabCampInfo.Controls.Add(Me.cmdDelete)
        Me.tabCampInfo.Controls.Add(Me.cmdAdd)
        Me.tabCampInfo.Controls.Add(Me.txtOffer)
        Me.tabCampInfo.Controls.Add(Me.cboOwner)
        Me.tabCampInfo.Controls.Add(Me.dtpStartDate)
        Me.tabCampInfo.Controls.Add(Me.dtpEndDate)
        Me.tabCampInfo.Controls.Add(Me.lblAdditionalInfo)
        Me.tabCampInfo.Controls.Add(Me.lblOffer)
        Me.tabCampInfo.Controls.Add(Me.lblTarget)
        Me.tabCampInfo.Controls.Add(Me.cboTarget)
        Me.tabCampInfo.Controls.Add(Me.Label13)
        Me.tabCampInfo.Controls.Add(Me.lblStatus)
        Me.tabCampInfo.Controls.Add(Me.cboStatus)
        Me.tabCampInfo.Controls.Add(Me.lblEndDate)
        Me.tabCampInfo.Controls.Add(Me.lblStartDate)
        Me.tabCampInfo.Controls.Add(Me.lblBgtCost)
        Me.tabCampInfo.Controls.Add(Me.txtBgtCost)
        Me.tabCampInfo.Controls.Add(Me.lblManPower)
        Me.tabCampInfo.Controls.Add(Me.txtManPower)
        Me.tabCampInfo.Controls.Add(Me.lblOwner)
        Me.tabCampInfo.Controls.Add(Me.lblDesc)
        Me.tabCampInfo.Controls.Add(Me.txtDesc)
        Me.tabCampInfo.Controls.Add(Me.lblActCost)
        Me.tabCampInfo.Controls.Add(Me.txtActCost)
        Me.tabCampInfo.Controls.Add(Me.lblObjective)
        Me.tabCampInfo.Controls.Add(Me.txtObjective)
        Me.tabCampInfo.Controls.Add(Me.Label1)
        Me.tabCampInfo.Controls.Add(Me.txtCampName)
        Me.tabCampInfo.Controls.Add(Me.Label5)
        Me.tabCampInfo.Controls.Add(Me.txtCampID)
        Me.tabCampInfo.Location = New System.Drawing.Point(4, 22)
        Me.tabCampInfo.Name = "tabCampInfo"
        Me.tabCampInfo.Size = New System.Drawing.Size(696, 250)
        Me.tabCampInfo.TabIndex = 0
        Me.tabCampInfo.Text = "Campaign Information"
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(292, 220)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.TabIndex = 15
        Me.cmdEdit.Text = "&Edit"
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(612, 220)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.TabIndex = 19
        Me.cmdExit.Text = "&Quit"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(452, 220)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.TabIndex = 17
        Me.cmdSave.Text = "&Save"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(532, 220)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 18
        Me.cmdCancel.Text = "&Cancel"
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(372, 220)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.TabIndex = 16
        Me.cmdDelete.Text = "&Delete"
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(212, 220)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.TabIndex = 14
        Me.cmdAdd.Text = "&Add"
        '
        'txtOffer
        '
        Me.txtOffer.Location = New System.Drawing.Point(100, 104)
        Me.txtOffer.MaxLength = 50
        Me.txtOffer.Name = "txtOffer"
        Me.txtOffer.Size = New System.Drawing.Size(328, 20)
        Me.txtOffer.TabIndex = 8
        Me.txtOffer.Text = ""
        '
        'cboOwner
        '
        Me.cboOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOwner.Location = New System.Drawing.Point(308, 80)
        Me.cboOwner.Name = "cboOwner"
        Me.cboOwner.Size = New System.Drawing.Size(120, 21)
        Me.cboOwner.TabIndex = 6
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Location = New System.Drawing.Point(500, 56)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(188, 20)
        Me.dtpStartDate.TabIndex = 4
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Location = New System.Drawing.Point(500, 80)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(188, 20)
        Me.dtpEndDate.TabIndex = 7
        '
        'lblAdditionalInfo
        '
        Me.lblAdditionalInfo.BackColor = System.Drawing.Color.Gainsboro
        Me.lblAdditionalInfo.Location = New System.Drawing.Point(4, 136)
        Me.lblAdditionalInfo.Name = "lblAdditionalInfo"
        Me.lblAdditionalInfo.Size = New System.Drawing.Size(688, 20)
        Me.lblAdditionalInfo.TabIndex = 110
        Me.lblAdditionalInfo.Text = "Additional Information"
        Me.lblAdditionalInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOffer
        '
        Me.lblOffer.AutoSize = True
        Me.lblOffer.Location = New System.Drawing.Point(59, 108)
        Me.lblOffer.Name = "lblOffer"
        Me.lblOffer.Size = New System.Drawing.Size(38, 16)
        Me.lblOffer.TabIndex = 109
        Me.lblOffer.Text = "Offers:"
        Me.lblOffer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTarget
        '
        Me.lblTarget.Location = New System.Drawing.Point(5, 84)
        Me.lblTarget.Name = "lblTarget"
        Me.lblTarget.Size = New System.Drawing.Size(92, 16)
        Me.lblTarget.TabIndex = 108
        Me.lblTarget.Text = "Target Customer:"
        Me.lblTarget.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTarget
        '
        Me.cboTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTarget.Location = New System.Drawing.Point(100, 80)
        Me.cboTarget.Name = "cboTarget"
        Me.cboTarget.Size = New System.Drawing.Size(148, 21)
        Me.cboTarget.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Gainsboro
        Me.Label13.Location = New System.Drawing.Point(4, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(688, 20)
        Me.Label13.TabIndex = 106
        Me.Label13.Text = "Key Campaign Information"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(455, 108)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(39, 16)
        Me.lblStatus.TabIndex = 103
        Me.lblStatus.Text = "Status:"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Location = New System.Drawing.Point(500, 104)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(121, 21)
        Me.cboStatus.TabIndex = 9
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(440, 84)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(54, 16)
        Me.lblEndDate.TabIndex = 101
        Me.lblEndDate.Text = "End Date:"
        Me.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(436, 60)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(58, 16)
        Me.lblStartDate.TabIndex = 100
        Me.lblStartDate.Text = "Start Date:"
        Me.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBgtCost
        '
        Me.lblBgtCost.AutoSize = True
        Me.lblBgtCost.Location = New System.Drawing.Point(280, 168)
        Me.lblBgtCost.Name = "lblBgtCost"
        Me.lblBgtCost.Size = New System.Drawing.Size(82, 16)
        Me.lblBgtCost.TabIndex = 99
        Me.lblBgtCost.Text = "Budgeted Cost:"
        Me.lblBgtCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBgtCost
        '
        Me.txtBgtCost.Location = New System.Drawing.Point(368, 164)
        Me.txtBgtCost.MaxLength = 12
        Me.txtBgtCost.Name = "txtBgtCost"
        Me.txtBgtCost.Size = New System.Drawing.Size(116, 20)
        Me.txtBgtCost.TabIndex = 11
        Me.txtBgtCost.Text = ""
        '
        'lblManPower
        '
        Me.lblManPower.AutoSize = True
        Me.lblManPower.Location = New System.Drawing.Point(37, 168)
        Me.lblManPower.Name = "lblManPower"
        Me.lblManPower.Size = New System.Drawing.Size(60, 16)
        Me.lblManPower.TabIndex = 97
        Me.lblManPower.Text = "Manpower:"
        Me.lblManPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtManPower
        '
        Me.txtManPower.Location = New System.Drawing.Point(100, 164)
        Me.txtManPower.MaxLength = 2
        Me.txtManPower.Name = "txtManPower"
        Me.txtManPower.TabIndex = 10
        Me.txtManPower.Text = ""
        '
        'lblOwner
        '
        Me.lblOwner.AutoSize = True
        Me.lblOwner.Location = New System.Drawing.Point(260, 84)
        Me.lblOwner.Name = "lblOwner"
        Me.lblOwner.Size = New System.Drawing.Size(41, 16)
        Me.lblOwner.TabIndex = 95
        Me.lblOwner.Text = "Owner:"
        Me.lblOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDesc
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.Location = New System.Drawing.Point(33, 192)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(64, 16)
        Me.lblDesc.TabIndex = 94
        Me.lblDesc.Text = "Description:"
        Me.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(100, 190)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(588, 20)
        Me.txtDesc.TabIndex = 13
        Me.txtDesc.Text = ""
        '
        'lblActCost
        '
        Me.lblActCost.AutoSize = True
        Me.lblActCost.Location = New System.Drawing.Point(500, 168)
        Me.lblActCost.Name = "lblActCost"
        Me.lblActCost.Size = New System.Drawing.Size(65, 16)
        Me.lblActCost.TabIndex = 92
        Me.lblActCost.Text = "Actual Cost:"
        Me.lblActCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtActCost
        '
        Me.txtActCost.Location = New System.Drawing.Point(572, 164)
        Me.txtActCost.MaxLength = 12
        Me.txtActCost.Name = "txtActCost"
        Me.txtActCost.Size = New System.Drawing.Size(116, 20)
        Me.txtActCost.TabIndex = 12
        Me.txtActCost.Text = ""
        '
        'lblObjective
        '
        Me.lblObjective.AutoSize = True
        Me.lblObjective.Location = New System.Drawing.Point(42, 60)
        Me.lblObjective.Name = "lblObjective"
        Me.lblObjective.Size = New System.Drawing.Size(55, 16)
        Me.lblObjective.TabIndex = 90
        Me.lblObjective.Text = "Objective:"
        Me.lblObjective.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtObjective
        '
        Me.txtObjective.Location = New System.Drawing.Point(100, 56)
        Me.txtObjective.MaxLength = 50
        Me.txtObjective.Name = "txtObjective"
        Me.txtObjective.Size = New System.Drawing.Size(328, 20)
        Me.txtObjective.TabIndex = 3
        Me.txtObjective.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(224, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 16)
        Me.Label1.TabIndex = 88
        Me.Label1.Text = "Campaign Name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCampName
        '
        Me.txtCampName.Location = New System.Drawing.Point(320, 32)
        Me.txtCampName.MaxLength = 50
        Me.txtCampName.Name = "txtCampName"
        Me.txtCampName.Size = New System.Drawing.Size(368, 20)
        Me.txtCampName.TabIndex = 2
        Me.txtCampName.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 16)
        Me.Label5.TabIndex = 86
        Me.Label5.Text = "Campaign ID:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCampID
        '
        Me.txtCampID.Location = New System.Drawing.Point(100, 32)
        Me.txtCampID.MaxLength = 15
        Me.txtCampID.Name = "txtCampID"
        Me.txtCampID.Size = New System.Drawing.Size(108, 20)
        Me.txtCampID.TabIndex = 1
        Me.txtCampID.Text = ""
        '
        'tabCampChannel
        '
        Me.tabCampChannel.Controls.Add(Me.cmdChannelEdit)
        Me.tabCampChannel.Controls.Add(Me.cmdChannelSave)
        Me.tabCampChannel.Controls.Add(Me.cmdChannelCancel)
        Me.tabCampChannel.Controls.Add(Me.cmdChannelDel)
        Me.tabCampChannel.Controls.Add(Me.cmdChannelAdd)
        Me.tabCampChannel.Controls.Add(Me.cboChannelStatus)
        Me.tabCampChannel.Controls.Add(Me.lblChannelStatus)
        Me.tabCampChannel.Controls.Add(Me.grdChannel)
        Me.tabCampChannel.Controls.Add(Me.cboChannel)
        Me.tabCampChannel.Controls.Add(Me.txtChannelOffer)
        Me.tabCampChannel.Controls.Add(Me.lblChannelOffer)
        Me.tabCampChannel.Controls.Add(Me.lblChannelBgtCost)
        Me.tabCampChannel.Controls.Add(Me.txtChannelBgtCost)
        Me.tabCampChannel.Controls.Add(Me.lblChannelManpower)
        Me.tabCampChannel.Controls.Add(Me.txtChannelManPower)
        Me.tabCampChannel.Controls.Add(Me.lblChannelDesc)
        Me.tabCampChannel.Controls.Add(Me.txtChannelDesc)
        Me.tabCampChannel.Controls.Add(Me.lblChannelActCost)
        Me.tabCampChannel.Controls.Add(Me.txtChannelActCost)
        Me.tabCampChannel.Controls.Add(Me.lblChannelCallScript)
        Me.tabCampChannel.Controls.Add(Me.txtChannelCallScript)
        Me.tabCampChannel.Controls.Add(Me.lblChannelChannels)
        Me.tabCampChannel.Location = New System.Drawing.Point(4, 22)
        Me.tabCampChannel.Name = "tabCampChannel"
        Me.tabCampChannel.Size = New System.Drawing.Size(696, 250)
        Me.tabCampChannel.TabIndex = 1
        Me.tabCampChannel.Text = "Campaign Channels"
        '
        'cmdChannelEdit
        '
        Me.cmdChannelEdit.Location = New System.Drawing.Point(368, 216)
        Me.cmdChannelEdit.Name = "cmdChannelEdit"
        Me.cmdChannelEdit.TabIndex = 30
        Me.cmdChannelEdit.Text = "&Edit"
        '
        'cmdChannelSave
        '
        Me.cmdChannelSave.Location = New System.Drawing.Point(528, 216)
        Me.cmdChannelSave.Name = "cmdChannelSave"
        Me.cmdChannelSave.TabIndex = 32
        Me.cmdChannelSave.Text = "&Save"
        '
        'cmdChannelCancel
        '
        Me.cmdChannelCancel.Location = New System.Drawing.Point(608, 216)
        Me.cmdChannelCancel.Name = "cmdChannelCancel"
        Me.cmdChannelCancel.TabIndex = 33
        Me.cmdChannelCancel.Text = "&Cancel"
        '
        'cmdChannelDel
        '
        Me.cmdChannelDel.Location = New System.Drawing.Point(448, 216)
        Me.cmdChannelDel.Name = "cmdChannelDel"
        Me.cmdChannelDel.TabIndex = 31
        Me.cmdChannelDel.Text = "&Delete"
        '
        'cmdChannelAdd
        '
        Me.cmdChannelAdd.Location = New System.Drawing.Point(288, 216)
        Me.cmdChannelAdd.Name = "cmdChannelAdd"
        Me.cmdChannelAdd.TabIndex = 29
        Me.cmdChannelAdd.Text = "&Add"
        '
        'cboChannelStatus
        '
        Me.cboChannelStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChannelStatus.Location = New System.Drawing.Point(568, 108)
        Me.cboChannelStatus.Name = "cboChannelStatus"
        Me.cboChannelStatus.Size = New System.Drawing.Size(116, 21)
        Me.cboChannelStatus.TabIndex = 22
        '
        'lblChannelStatus
        '
        Me.lblChannelStatus.AutoSize = True
        Me.lblChannelStatus.Location = New System.Drawing.Point(523, 110)
        Me.lblChannelStatus.Name = "lblChannelStatus"
        Me.lblChannelStatus.Size = New System.Drawing.Size(39, 16)
        Me.lblChannelStatus.TabIndex = 136
        Me.lblChannelStatus.Text = "Status:"
        Me.lblChannelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdChannel
        '
        Me.grdChannel.AlternatingBackColor = System.Drawing.Color.White
        Me.grdChannel.BackColor = System.Drawing.Color.White
        Me.grdChannel.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdChannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdChannel.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdChannel.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdChannel.CaptionVisible = False
        Me.grdChannel.DataMember = ""
        Me.grdChannel.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdChannel.FlatMode = True
        Me.grdChannel.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdChannel.ForeColor = System.Drawing.Color.Black
        Me.grdChannel.GridLineColor = System.Drawing.Color.Wheat
        Me.grdChannel.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdChannel.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdChannel.HeaderForeColor = System.Drawing.Color.Black
        Me.grdChannel.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdChannel.Location = New System.Drawing.Point(0, 0)
        Me.grdChannel.Name = "grdChannel"
        Me.grdChannel.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdChannel.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdChannel.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdChannel.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdChannel.Size = New System.Drawing.Size(696, 104)
        Me.grdChannel.TabIndex = 20
        '
        'cboChannel
        '
        Me.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChannel.Location = New System.Drawing.Point(76, 108)
        Me.cboChannel.Name = "cboChannel"
        Me.cboChannel.Size = New System.Drawing.Size(200, 21)
        Me.cboChannel.TabIndex = 21
        '
        'txtChannelOffer
        '
        Me.txtChannelOffer.Location = New System.Drawing.Point(76, 134)
        Me.txtChannelOffer.MaxLength = 50
        Me.txtChannelOffer.Name = "txtChannelOffer"
        Me.txtChannelOffer.Size = New System.Drawing.Size(392, 20)
        Me.txtChannelOffer.TabIndex = 23
        Me.txtChannelOffer.Text = ""
        '
        'lblChannelOffer
        '
        Me.lblChannelOffer.AutoSize = True
        Me.lblChannelOffer.Location = New System.Drawing.Point(32, 136)
        Me.lblChannelOffer.Name = "lblChannelOffer"
        Me.lblChannelOffer.Size = New System.Drawing.Size(38, 16)
        Me.lblChannelOffer.TabIndex = 132
        Me.lblChannelOffer.Text = "Offers:"
        Me.lblChannelOffer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblChannelBgtCost
        '
        Me.lblChannelBgtCost.AutoSize = True
        Me.lblChannelBgtCost.Location = New System.Drawing.Point(480, 162)
        Me.lblChannelBgtCost.Name = "lblChannelBgtCost"
        Me.lblChannelBgtCost.Size = New System.Drawing.Size(82, 16)
        Me.lblChannelBgtCost.TabIndex = 130
        Me.lblChannelBgtCost.Text = "Budgeted Cost:"
        Me.lblChannelBgtCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChannelBgtCost
        '
        Me.txtChannelBgtCost.Location = New System.Drawing.Point(568, 160)
        Me.txtChannelBgtCost.MaxLength = 12
        Me.txtChannelBgtCost.Name = "txtChannelBgtCost"
        Me.txtChannelBgtCost.Size = New System.Drawing.Size(116, 20)
        Me.txtChannelBgtCost.TabIndex = 26
        Me.txtChannelBgtCost.Text = ""
        '
        'lblChannelManpower
        '
        Me.lblChannelManpower.AutoSize = True
        Me.lblChannelManpower.Location = New System.Drawing.Point(502, 136)
        Me.lblChannelManpower.Name = "lblChannelManpower"
        Me.lblChannelManpower.Size = New System.Drawing.Size(60, 16)
        Me.lblChannelManpower.TabIndex = 128
        Me.lblChannelManpower.Text = "Manpower:"
        Me.lblChannelManpower.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChannelManPower
        '
        Me.txtChannelManPower.Location = New System.Drawing.Point(568, 134)
        Me.txtChannelManPower.MaxLength = 2
        Me.txtChannelManPower.Name = "txtChannelManPower"
        Me.txtChannelManPower.Size = New System.Drawing.Size(116, 20)
        Me.txtChannelManPower.TabIndex = 24
        Me.txtChannelManPower.Text = ""
        '
        'lblChannelDesc
        '
        Me.lblChannelDesc.AutoSize = True
        Me.lblChannelDesc.Location = New System.Drawing.Point(6, 162)
        Me.lblChannelDesc.Name = "lblChannelDesc"
        Me.lblChannelDesc.Size = New System.Drawing.Size(64, 16)
        Me.lblChannelDesc.TabIndex = 126
        Me.lblChannelDesc.Text = "Description:"
        Me.lblChannelDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChannelDesc
        '
        Me.txtChannelDesc.Location = New System.Drawing.Point(76, 160)
        Me.txtChannelDesc.MaxLength = 100
        Me.txtChannelDesc.Multiline = True
        Me.txtChannelDesc.Name = "txtChannelDesc"
        Me.txtChannelDesc.Size = New System.Drawing.Size(392, 20)
        Me.txtChannelDesc.TabIndex = 25
        Me.txtChannelDesc.Text = ""
        '
        'lblChannelActCost
        '
        Me.lblChannelActCost.AutoSize = True
        Me.lblChannelActCost.Location = New System.Drawing.Point(497, 188)
        Me.lblChannelActCost.Name = "lblChannelActCost"
        Me.lblChannelActCost.Size = New System.Drawing.Size(65, 16)
        Me.lblChannelActCost.TabIndex = 124
        Me.lblChannelActCost.Text = "Actual Cost:"
        Me.lblChannelActCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChannelActCost
        '
        Me.txtChannelActCost.Location = New System.Drawing.Point(568, 186)
        Me.txtChannelActCost.MaxLength = 12
        Me.txtChannelActCost.Name = "txtChannelActCost"
        Me.txtChannelActCost.Size = New System.Drawing.Size(116, 20)
        Me.txtChannelActCost.TabIndex = 28
        Me.txtChannelActCost.Text = ""
        '
        'lblChannelCallScript
        '
        Me.lblChannelCallScript.AutoSize = True
        Me.lblChannelCallScript.DisabledLinkColor = System.Drawing.SystemColors.ControlText
        Me.lblChannelCallScript.Location = New System.Drawing.Point(11, 188)
        Me.lblChannelCallScript.Name = "lblChannelCallScript"
        Me.lblChannelCallScript.Size = New System.Drawing.Size(59, 16)
        Me.lblChannelCallScript.TabIndex = 122
        Me.lblChannelCallScript.TabStop = True
        Me.lblChannelCallScript.Text = "Call Script:"
        Me.lblChannelCallScript.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChannelCallScript
        '
        Me.txtChannelCallScript.Location = New System.Drawing.Point(76, 186)
        Me.txtChannelCallScript.MaxLength = 50
        Me.txtChannelCallScript.Name = "txtChannelCallScript"
        Me.txtChannelCallScript.Size = New System.Drawing.Size(392, 20)
        Me.txtChannelCallScript.TabIndex = 27
        Me.txtChannelCallScript.Text = ""
        '
        'lblChannelChannels
        '
        Me.lblChannelChannels.AutoSize = True
        Me.lblChannelChannels.Location = New System.Drawing.Point(15, 112)
        Me.lblChannelChannels.Name = "lblChannelChannels"
        Me.lblChannelChannels.Size = New System.Drawing.Size(55, 16)
        Me.lblChannelChannels.TabIndex = 120
        Me.lblChannelChannels.Text = "Channels:"
        Me.lblChannelChannels.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.DefaultExt = "txt"
        Me.OpenFileDialog.Filter = "Text files|*.txt|All files|*.*"
        '
        'cmdUpdRating
        '
        Me.cmdUpdRating.Location = New System.Drawing.Point(8, 220)
        Me.cmdUpdRating.Name = "cmdUpdRating"
        Me.cmdUpdRating.Size = New System.Drawing.Size(88, 23)
        Me.cmdUpdRating.TabIndex = 112
        Me.cmdUpdRating.Text = "&Update Rating"
        '
        'uclCampaign
        '
        Me.Controls.Add(Me.tabCampaign)
        Me.Controls.Add(Me.grdCampaign)
        Me.Name = "uclCampaign"
        Me.Size = New System.Drawing.Size(712, 448)
        CType(Me.grdCampaign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCampaign.ResumeLayout(False)
        Me.tabCampInfo.ResumeLayout(False)
        Me.tabCampChannel.ResumeLayout(False)
        CType(Me.grdChannel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Public Sub InitControl()
        'dtCampaign = srcDTCampaign
        'dtChannel = srcDTChannel
        blnAdmin = True
        blnChange = False
        blnChangeChannel = False
        Me.OpenFileDialog.InitialDirectory = Application.StartupPath
        Me.OpenFileDialog.Filter = "Word Document (*.doc)|*.doc|PDF files (*.pdf)|*.pdf"

        Call buildUI()
    End Sub

    Private Sub buildUI()

        Call EnableControls(False)
        Call EnableChannelControls(False)
        Call InitButtons()

        Call FillCboTarget()
        Call FillCboOwner()
        Call FillCboStatus()
        Call FillCboChannel()

        Call BindCampaignGrid()

    End Sub

    Private Sub BindCampaignGrid()
        Dim dtData As DataTable
        Dim strErrMsg As String

        If GetCampaign(dtData, strErrMsg) Then
            If dsCampaign.Tables.Contains(dtData.TableName) Then
                dsCampaign.Tables(dtData.TableName).Constraints.Clear()
                dsCampaign.Relations.Clear()
                dsCampaign.Tables.Remove(dtData.TableName)
            End If
            dsCampaign.Tables.Add(dtData)
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

        'If GetCampaignChannel(dtData, strErrMsg) Then
        '    If dsCampaign.Tables.Contains(dtData.TableName) Then
        '        dsCampaign.Tables(dtData.TableName).Constraints.Clear()
        '        dsCampaign.Tables.Remove(dtData.TableName)
        '    End If
        '    dsCampaign.Tables.Add(dtData)
        'Else
        '    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End If

        'Dim relActRel As New Data.DataRelation("CampaignRel", dsCampaign.Tables("Campaign").Columns("CampaignID"), _
        '    dsCampaign.Tables("CampaignChannel").Columns("CampaignID"), True)

        'Try
        '    dsCampaign.Relations.Add(relActRel)
        'Catch sqlex As SqlClient.SqlException
        '    'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try


        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "OwnerDesc"
        cs.HeaderText = "Owner"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CampaignID"
        cs.HeaderText = "Campaign ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 180
        cs.MappingName = "CampaignName"
        cs.HeaderText = "Campaign Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "StartDate"
        cs.HeaderText = "Start Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "EndDate"
        cs.HeaderText = "End Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "Objective"
        cs.HeaderText = "Objective"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)


        ts.MappingName = "Campaign"
        grdCampaign.TableStyles.Clear()
        grdCampaign.TableStyles.Add(ts)

        grdCampaign.DataSource = dsCampaign.Tables("Campaign")
        dsCampaign.Tables("Campaign").DefaultView.Sort = "StartDate DESC"
        grdCampaign.AllowDrop = False
        grdCampaign.ReadOnly = True

        bm = Me.BindingContext(dsCampaign.Tables("Campaign"))

        If bm.Count = 0 Then
            cmdEdit.Enabled = False
            cmdDelete.Enabled = False
        End If

        Call UpdatePT()

        Call BindChannelGrid(Me.txtCampID.Text)

    End Sub

    Private Sub UpdatePT()

        Dim drI As DataRow

        If bm.Count > 0 Then
            drI = CType(bm.Current, DataRowView).Row()
        End If

        If Not drI Is Nothing Then
            Me.txtCampID.Text = drI.Item("CampaignID")
            Me.txtCampName.Text = drI.Item("CampaignName")
            Me.txtObjective.Text = drI.Item("Objective")
            Me.dtpStartDate.Value = drI.Item("StartDate")
            Me.dtpEndDate.Value = drI.Item("EndDate")
            Me.cboTarget.SelectedValue = drI.Item("TargetCustomer")
            Me.cboOwner.SelectedValue = drI.Item("OwnerID")
            Me.cboStatus.SelectedValue = drI.Item("Status")
            Me.txtOffer.Text = drI.Item("Offer")

            Me.txtManPower.Text = IIf(drI.IsNull("ManPower"), gNULLText, drI.Item("ManPower"))
            If drI.IsNull("BudgetCost") Then
                Me.txtBgtCost.Text = gNULLText
            Else
                Me.txtBgtCost.Text = Format(drI.Item("BudgetCost"), "#,##0.00")
            End If
            If drI.IsNull("ActualCost") Then
                Me.txtActCost.Text = gNULLText
            Else
                Me.txtActCost.Text = Format(drI.Item("ActualCost"), "#,##0.00")
            End If
            Me.txtDesc.Text = IIf(drI.IsNull("Description"), gNULLText, drI.Item("Description"))
        Else
            Call ClearTextBox()
        End If

        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        'If ExternalUser Then
        '    cmdUpdRating.Enabled = False
        '    cmdAdd.Enabled = False
        '    cmdEdit.Enabled = False
        '    cmdDelete.Enabled = False
        '    cmdSave.Enabled = False
        '    cmdCancel.Enabled = False
        '    cmdExit.Enabled = False

        '    cmdChannelAdd.Enabled = False
        '    cmdChannelEdit.Enabled = False
        '    cmdChannelDel.Enabled = False
        '    cmdChannelSave.Enabled = False
        '    cmdChannelCancel.Enabled = False
        'End If
    End Sub

    Private Sub BindChannelGrid(ByVal strCampaignID As String)
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqlDT As DataTable
        Dim strSQL As String
        Dim lngCnt As Long

        Dim dtData As DataTable
        Dim strErrMsg As String

        If GetCampaignChannel(dtData, strErrMsg, strCampaignID) Then
            If dsCampaign.Tables.Contains(dtData.TableName) Then
                dsCampaign.Tables(dtData.TableName).Constraints.Clear()
                dsCampaign.Tables.Remove(dtData.TableName)
            End If
            dsCampaign.Tables.Add(dtData)
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If


        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "ChannelName"
        cs.HeaderText = "Channel"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "ChannelDesc"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CampaignChannel"
        Me.grdChannel.TableStyles.Clear()
        Me.grdChannel.TableStyles.Add(ts)

        grdChannel.DataSource = dsCampaign.Tables("CampaignChannel")
        dsCampaign.Tables("CampaignChannel").DefaultView.Sort = "ChannelID"
        grdChannel.AllowDrop = False
        grdChannel.ReadOnly = True

        bmChannel = Me.BindingContext(dsCampaign.Tables("CampaignChannel"))

        If bmChannel.Count = 0 Then
            cmdChannelEdit.Enabled = False
            cmdChannelDel.Enabled = False
        End If


        Call UpdateChannelTextBox()

    End Sub

    Private Sub UpdateChannelTextBox()

        Dim drTemp As DataRow

        If bmChannel.Count > 0 Then
            drTemp = CType(bmChannel.Current, DataRowView).Row()
        End If

        If Not drTemp Is Nothing Then
            Me.cboChannel.SelectedValue = drTemp.Item("ChannelID")
            Me.cboChannelStatus.SelectedValue = drTemp.Item("ChannelStatus")
            Me.txtChannelOffer.Text = IIf(drTemp.IsNull("ChannelOffer"), gNULLText, drTemp.Item("ChannelOffer"))
            Me.txtChannelManPower.Text = IIf(drTemp.IsNull("ChannelManPower"), gNULLText, drTemp.Item("ChannelManPower"))
            Me.txtChannelDesc.Text = IIf(drTemp.IsNull("ChannelDesc"), gNULLText, drTemp.Item("ChannelDesc"))
            If drTemp.IsNull("ChannelBudgetCost") Then
                Me.txtChannelBgtCost.Text = gNULLText
            Else
                Me.txtChannelBgtCost.Text = Format(drTemp.Item("ChannelBudgetCost"), "#,##0.00")
            End If
            Me.txtChannelCallScript.Text = IIf(drTemp.IsNull("ChannelCallScript"), gNULLText, drTemp.Item("ChannelCallScript"))
            If drTemp.IsNull("ChannelActualCost") Then
                Me.txtChannelActCost.Text = gNULLText
            Else
                Me.txtChannelActCost.Text = Format(drTemp.Item("ChannelActualCost"), "#,##0.00")
            End If
        Else
            Call ClearChannelTextBox()
        End If

    End Sub



#Region "Fill Combox"
    Private Sub FillCboTarget()
        Dim dtData As DataTable
        Dim drTemp As DataRow

        Try
            dtData = New DataTable("CampaignTarget")
            dtData.Columns.Add("TargetID", GetType(String))
            dtData.Columns("TargetID").MaxLength = 2
            dtData.Columns.Add("TargetDesc", GetType(String))
            dtData.Columns("TargetDesc").MaxLength = 50

            drTemp = dtData.NewRow
            drTemp.Item("TargetID") = "EC"
            drTemp.Item("TargetDesc") = "Existing Customer"
            dtData.Rows.Add(drTemp)
            drTemp = dtData.NewRow
            drTemp.Item("TargetID") = "PC"
            drTemp.Item("TargetDesc") = "Potential Customer"
            dtData.Rows.Add(drTemp)
            drTemp = dtData.NewRow
            drTemp.Item("TargetID") = "BO"
            drTemp.Item("TargetDesc") = "Both"
            dtData.Rows.Add(drTemp)

            dtData.DefaultView.Sort = "TargetID"

            'Me.cboTarget.Items.Clear()
            Me.cboTarget.DataSource = dtData.DefaultView
            Me.cboTarget.DisplayMember = "TargetDesc"
            Me.cboTarget.ValueMember = "TargetID"
            Me.cboTarget.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub FillCboOwner()
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqlDS As DataSet = New DataSet("Campaign")
        Dim sqlDV As DataView
        Dim strSQL As String
        Dim I As Integer
        Dim strOwnerID As String
        Dim strOwnerName As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Try
            sqlConn.ConnectionString = strCIWConn

            strSQL = "Select crmcpo_owner_id as OwnerID, crmcpo_owner_desc as OwnerDesc from " & serverPrefix & "crm_campaign_owner"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(sqlDS, "CampaignOwner")

            sqlDV = sqlDS.Tables("CampaignOwner").DefaultView
            sqlDV.Sort = "OwnerDesc"

            'Me.cboOwner.Items.Clear()
            Me.cboOwner.DataSource = sqlDV
            Me.cboOwner.DisplayMember = "OwnerDesc"
            Me.cboOwner.ValueMember = "OwnerID"
            Me.cboOwner.SelectedIndex = -1
            'For I = 0 To sqlDV.Count - 1
            '    strOwnerID = sqlDV.Item(I).Item("crmcpo_owner_id")
            '    strOwnerName = sqlDV.Item(I).Item("crmcpo_owner_desc")
            '    Me.cboOwner.Items.Add(strOwnerName)
            'Next
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            sqlDA.Dispose()
            sqlDS.Dispose()
            sqlConn.Dispose()
        End Try

    End Sub

    Private Sub FillCboStatus()
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqlDS As New DataSet("Campaign")
        Dim sqlDV As DataView
        Dim strSQL As String
        Dim I As Integer
        Dim strOwnerID As String
        Dim strOwnerName As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Try
            sqlConn.ConnectionString = strCIWConn

            strSQL = "Select crmcps_status_id as StatusID, crmcps_status_desc as StatusDesc from " & serverPrefix & "crm_campaign_status"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(sqlDS, "CampaignStatus")

            sqlDV = sqlDS.Tables("CampaignStatus").Copy.DefaultView
            sqlDV.Sort = "StatusID"

            'Set Campaign Status
            'Me.cboStatus.Items.Clear()
            Me.cboStatus.DataSource = sqlDV
            Me.cboStatus.DisplayMember = "StatusDesc"
            Me.cboStatus.ValueMember = "StatusID"
            Me.cboStatus.SelectedIndex = -1

            sqlDV = sqlDS.Tables("CampaignStatus").Copy.DefaultView
            sqlDV.Sort = "StatusID"

            'Set Campaign Channel Status
            Me.cboChannelStatus.Items.Clear()
            Me.cboChannelStatus.DataSource = sqlDV
            Me.cboChannelStatus.DisplayMember = "StatusDesc"
            Me.cboChannelStatus.ValueMember = "StatusID"
            Me.cboChannelStatus.SelectedIndex = -1

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            sqlDA.Dispose()
            sqlDS.Dispose()
            sqlConn.Dispose()
        End Try

    End Sub

    Private Sub FillCboChannel()
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqlDT As New DataTable("TableChannelDesc")
        Dim sqlDV As DataView
        Dim strSQL As String
        Dim I As Integer
        Dim strOwnerID As String
        Dim strOwnerName As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Try
            sqlConn.ConnectionString = strCIWConn

            strSQL = "Select crmcpt_channel_id as ChannelID, crmcpt_channel_desc as ChannelDesc from " & serverPrefix & "crm_campaign_channel_type"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(sqlDT)

            sqlDV = sqlDT.DefaultView
            sqlDV.Sort = "ChannelID"

            'Add into DataSet for using later
            If dsCampaign.Tables.Contains(sqlDT.TableName) Then
                dsCampaign.Tables(sqlDT.TableName).Constraints.Clear()
                dsCampaign.Relations.Clear()
                dsCampaign.Tables.Remove(sqlDT.TableName)
            End If
            dsCampaign.Tables.Add(sqlDT.Copy)

            'Me.cboChannel.Items.Clear()
            Me.cboChannel.DataSource = sqlDV
            Me.cboChannel.DisplayMember = "ChannelDesc"
            Me.cboChannel.ValueMember = "ChannelID"
            Me.cboChannel.SelectedIndex = -1
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            sqlDA.Dispose()
            sqlDT.Dispose()
            sqlConn.Dispose()
        End Try

    End Sub
#End Region

#Region "Get Data"
    Private Function GetCampaign(ByRef dtData As DataTable, ByRef strErrMsg As String, Optional ByVal strCampaignID As String = "ALL") As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim aryDCPrimary(0) As DataColumn
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        GetCampaign = False

        Try
            sqlConn.ConnectionString = strCIWConn

            dtData = New DataTable("Campaign")

            'Get Campaigns
            strSQL = "Select crmcmp_campaign_id as CampaignID, crmcmp_campaign_name as CampaignName,"
            strSQL &= " crmcmp_campaign_objective as Objective, crmcmp_start_date as StartDate,"
            strSQL &= " crmcmp_end_date as EndDate, crmcmp_target_customer as TargetCustomer,"
            strSQL &= " crmcmp_owner_id as OwnerID, crmcmp_offer as Offer, crmcmp_status_id as Status,"
            strSQL &= " crmcmp_manpower as ManPower, crmcmp_budget_cost as BudgetCost,"
            strSQL &= " crmcmp_actual_cost as ActualCost, crmcmp_description as Description,"
            strSQL &= " crmcpo_owner_desc as OwnerDesc"
            strSQL &= " From " & serverPrefix & "crm_campaign Inner Join " & serverPrefix & "crm_campaign_owner"
            strSQL &= " ON crm_campaign.crmcmp_owner_id = crm_campaign_owner.crmcpo_owner_id"
            If strCampaignID <> "ALL" Then
                strSQL &= " Where crmcmp_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            End If
            strSQL &= " Order by crmcmp_campaign_id"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            'Set Primary Key
            aryDCPrimary(0) = dtData.Columns("CampaignID")
            dtData.PrimaryKey = aryDCPrimary

            GetCampaign = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlConn.Close()
            sqlDA.Dispose()
            sqlConn.Dispose()
        End Try

    End Function

    Private Function GetCampaignChannel(ByRef dtData As DataTable, ByRef strErrMsg As String, _
                                Optional ByVal strCampaignID As String = "ALL", Optional ByVal strChannelID As String = "ALL") As Boolean
        Dim sqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim aryDCPrimary(1) As DataColumn
        Dim strSQL As String
        Dim strWhere As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        GetCampaignChannel = False

        Try
            sqlConn.ConnectionString = strCIWConn

            dtData = New DataTable("CampaignChannel")

            'Get Campaign Channels
            strSQL = "Select crmcpc_campaign_id as CampaignID, crmcpc_channel_id as ChannelID,"
            strSQL &= " crmcpc_offer as ChannelOffer, crmcpc_status_id as ChannelStatus,"
            strSQL &= " crmcpc_manpower as ChannelManPower, crmcpc_budget_cost as ChannelBudgetCost,"
            strSQL &= " crmcpc_actual_cost as ChannelActualCost, crmcpc_description as ChannelDesc,"
            strSQL &= " crmcpc_call_script as ChannelCallScript, crmcpt_channel_desc as ChannelName"
            strSQL &= " From " & serverPrefix & "crm_campaign_channel Inner Join " & serverPrefix & "crm_campaign_channel_type"
            strSQL &= " ON crm_campaign_channel.crmcpc_channel_id = crm_campaign_channel_type.crmcpt_channel_id"
            If strCampaignID <> "ALL" Then
                strWhere &= " Where crmcpc_campaign_id = '" & strCampaignID.Trim.Replace("'", "''") & "'"
            End If
            If strChannelID <> "ALL" Then
                If strWhere = "" Then
                    strWhere &= " Where"
                Else
                    strWhere &= " And"
                End If
                strWhere &= " crmcpc_channel_id = '" & strChannelID.Trim.Replace("'", "''") & "'"
            End If
            strSQL &= strWhere & " Order by crmcpc_campaign_id, crmcpc_channel_id"

            sqlDA = New SqlDataAdapter(strSQL, sqlConn)

            sqlDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.Fill(dtData)

            'Set Primary Key
            aryDCPrimary(0) = dtData.Columns("CampaignID")
            aryDCPrimary(1) = dtData.Columns("ChannelID")
            dtData.PrimaryKey = aryDCPrimary

            GetCampaignChannel = True

        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.Number & sqlex.ToString
        Catch ex As Exception
            strErrMsg = ex.ToString
        Finally
            sqlConn.Close()
            sqlDA.Dispose()
            sqlConn.Dispose()
        End Try

    End Function
#End Region

    Private Sub EnableControls(ByVal blnEnabled As Boolean)
        'Disable Primary Key
        Me.txtCampID.Enabled = False
        Me.txtCampName.Enabled = blnEnabled
        Me.txtObjective.Enabled = blnEnabled
        Me.txtOffer.Enabled = blnEnabled
        Me.txtManPower.Enabled = blnEnabled
        Me.txtBgtCost.Enabled = blnEnabled
        Me.txtActCost.Enabled = blnEnabled
        Me.txtDesc.Enabled = blnEnabled
        Me.dtpStartDate.Enabled = blnEnabled
        Me.dtpEndDate.Enabled = blnEnabled
        Me.cboTarget.Enabled = blnEnabled
        Me.cboOwner.Enabled = blnEnabled
        Me.cboStatus.Enabled = blnEnabled
    End Sub

    Private Sub EnableChannelControls(ByVal blnEnabled As Boolean)
        Me.txtChannelOffer.Enabled = blnEnabled
        Me.txtChannelManPower.Enabled = blnEnabled
        Me.txtChannelDesc.Enabled = blnEnabled
        Me.txtChannelBgtCost.Enabled = blnEnabled
        Me.txtChannelActCost.Enabled = blnEnabled
        Me.lblChannelCallScript.Enabled = blnEnabled
        Me.txtChannelCallScript.Enabled = blnEnabled
        Me.cboChannel.Enabled = blnEnabled
        Me.cboChannelStatus.Enabled = blnEnabled
    End Sub

    Private Sub InitButtons()
        Me.cmdAdd.Enabled = blnAdmin
        Me.cmdUpdRating.Enabled = blnAdmin
        Me.cmdEdit.Enabled = blnAdmin
        Me.cmdDelete.Enabled = blnAdmin
        Me.cmdSave.Enabled = False
        Me.cmdCancel.Enabled = False
        Me.cmdExit.Enabled = True
        Me.cmdChannelAdd.Enabled = False
        Me.cmdChannelEdit.Enabled = False
        Me.cmdChannelDel.Enabled = False
        Me.cmdChannelSave.Enabled = False
        Me.cmdChannelCancel.Enabled = False
    End Sub

    Private Sub ClearTextBox()
        Me.txtCampID.Text = gNULLText
        Me.txtCampName.Text = gNULLText
        Me.txtObjective.Text = gNULLText
        Me.txtOffer.Text = gNULLText
        Me.txtManPower.Text = gNULLText
        Me.txtBgtCost.Text = gNULLText
        Me.txtActCost.Text = gNULLText
        Me.txtDesc.Text = gNULLText
        Me.dtpStartDate.Value = Now.Today
        Me.dtpEndDate.Value = Now.Today
        If Me.cboTarget.Items.Count > 0 Then
            Me.cboTarget.SelectedIndex = 0
        End If
        If Me.cboOwner.Items.Count > 0 Then
            Me.cboOwner.SelectedIndex = 0
        End If
        If Me.cboStatus.Items.Count > 0 Then
            Me.cboStatus.SelectedIndex = 0
        End If
    End Sub

    Private Sub ClearChannelTextBox()
        Me.txtChannelOffer.Text = gNULLText
        Me.txtChannelManPower.Text = gNULLText
        Me.txtChannelDesc.Text = gNULLText
        Me.txtChannelBgtCost.Text = gNULLText
        Me.txtChannelActCost.Text = gNULLText
        Me.txtChannelCallScript.Text = gNULLText
        If Me.cboChannel.Items.Count > 0 Then
            Me.cboChannel.SelectedIndex = 0
        End If
        If Me.cboChannelStatus.Items.Count > 0 Then
            Me.cboChannelStatus.SelectedIndex = 0
        End If
    End Sub

    Private Function GetChannelName(ByVal strChannelID As String) As String
        Dim dtData As DataTable
        Dim drData() As DataRow

        GetChannelName = ""
        dtData = dsCampaign.Tables("TableChannelDesc").Copy

        drData = dtData.Select("ChannelID = '" & strChannelID.Replace("'", "''") & "'")
        If drData.GetUpperBound(0) >= 0 Then
            GetChannelName = drData(0).Item("ChannelDesc")
        End If

    End Function

    Private Function Validation() As Boolean
        Dim strErrMsg As String

        Validation = False

        'Check Key Campaign Information
        'If Me.txtCampID.Text.Trim.Length = 0 Then
        '    strErrMsg = "Please input Campaign ID."
        '    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        '    Me.txtCampID.Focus()
        '    Exit Function
        'End If
        If Me.txtCampName.Text.Trim.Length = 0 Then
            strErrMsg = "Please input Campaign Name."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.txtCampName.Focus()
            Exit Function
        End If
        If Me.txtObjective.Text.Trim.Length = 0 Then
            strErrMsg = "Please input Objective."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.txtObjective.Focus()
            Exit Function
        End If
        If Me.cboTarget.SelectedIndex = -1 Then
            strErrMsg = "Please select Target Customer."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.cboTarget.Focus()
            Exit Function
        End If
        If Me.cboOwner.SelectedIndex = -1 Then
            strErrMsg = "Please select Owner."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.cboOwner.Focus()
            Exit Function
        End If
        'If Me.txtOffer.Text.Trim.Length = 0 Then
        '    strErrMsg = "Please input Offer."
        '    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        '    Me.txtOffer.Focus()
        '    Exit Function
        'End If
        If Me.cboStatus.SelectedIndex = -1 Then
            strErrMsg = "Please select Status."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.cboStatus.Focus()
            Exit Function
        End If

        'Check Input Type
        If Me.txtManPower.Text.Trim.Length > 0 Then
            If Not IsNumeric(Me.txtManPower.Text.Trim) Then
                strErrMsg = "ManPower should be a number."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtManPower.Focus()
                Exit Function
            ElseIf CInt(Me.txtManPower.Text.Trim) < 0 Then
                strErrMsg = "ManPower cannot be less than zero."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtManPower.Focus()
                Exit Function
            ElseIf CInt(Me.txtManPower.Text.Trim) > 99 Then
                strErrMsg = "ManPower cannot be more than 99."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtManPower.Focus()
                Exit Function
            End If
        End If
        If Me.txtBgtCost.Text.Trim.Length > 0 Then
            If Not IsNumeric(Me.txtBgtCost.Text.Trim) Then
                strErrMsg = "Budgeted Cost should be a number."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtBgtCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtBgtCost.Text.Trim) < 0 Then
                strErrMsg = "Budgeted Cost cannot be less than 0."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtBgtCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtBgtCost.Text.Trim) > 9999999.99 Then
                strErrMsg = "Budgeted Cost cannot be more than 9999999.99."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtBgtCost.Focus()
                Exit Function
            End If
        End If
        If Me.txtActCost.Text.Trim.Length > 0 Then
            If Not IsNumeric(Me.txtActCost.Text.Trim) Then
                strErrMsg = "Actual  Cost should be a number."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtActCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtActCost.Text.Trim) < 0 Then
                strErrMsg = "Actual Cost cannot be less than 0."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtActCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtActCost.Text.Trim) > 9999999.99 Then
                strErrMsg = "Actual Cost cannot be more than 9999999.99."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtActCost.Focus()
                Exit Function
            End If
        End If

        If Me.dtpStartDate.Value > Me.dtpEndDate.Value Then
            strErrMsg = "Start Date cannot be later than End Date."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.dtpStartDate.Focus()
            Exit Function
        End If

        Validation = True

    End Function

    Private Function ValidationChannel() As Boolean
        Dim strErrMsg As String

        ValidationChannel = False

        If Me.cboChannel.SelectedIndex = -1 Then
            strErrMsg = "Please select Channel."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.cboChannel.Focus()
            Exit Function
        End If
        If Me.cboChannelStatus.SelectedIndex = -1 Then
            strErrMsg = "Please select Channel Status."
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Me.cboChannelStatus.Focus()
            Exit Function
        End If

        'Check Input Type
        If Me.txtChannelManPower.Text.Trim.Length > 0 Then
            If Not IsNumeric(Me.txtChannelManPower.Text.Trim) Then
                strErrMsg = "ManPower should be a number."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelManPower.Focus()
                Exit Function
            ElseIf CInt(Me.txtChannelManPower.Text.Trim) < 0 Then
                strErrMsg = "ManPower cannot be less than zero."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelManPower.Focus()
                Exit Function
            ElseIf CInt(Me.txtChannelManPower.Text.Trim) > 99 Then
                strErrMsg = "ManPower cannot be more than 99."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelManPower.Focus()
                Exit Function
            End If
        End If
        If Me.txtChannelBgtCost.Text.Trim.Length > 0 Then
            If Not IsNumeric(Me.txtChannelBgtCost.Text.Trim) Then
                strErrMsg = "Budgeted Cost should be a number."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelBgtCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtChannelBgtCost.Text.Trim) < 0 Then
                strErrMsg = "Budgeted Cost cannot be less than 0."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelBgtCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtChannelBgtCost.Text.Trim) > 9999999.99 Then
                strErrMsg = "Budgeted Cost cannot be more than 9999999.99."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelBgtCost.Focus()
                Exit Function
            End If
        End If
        If Me.txtChannelActCost.Text.Trim.Length > 0 Then
            If Not IsNumeric(Me.txtChannelActCost.Text.Trim) Then
                strErrMsg = "Actual  Cost should be a number."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelActCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtChannelActCost.Text.Trim) < 0 Then
                strErrMsg = "Actual Cost cannot be less than 0."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelActCost.Focus()
                Exit Function
            ElseIf CDbl(Me.txtChannelActCost.Text.Trim) > 9999999.99 Then
                strErrMsg = "Actual Cost cannot be more than 9999999.99."
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Me.txtChannelActCost.Focus()
                Exit Function
            End If
        End If

        ValidationChannel = True

    End Function

    Private Sub SaveCampaign()
        Dim sqlConn As New SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sqlTran As SqlTransaction
        Dim dtData As DataTable
        Dim strErrMsg As String

        Dim strCampID As String
        Dim strCampName As String
        Dim strObjective As String
        Dim datStartDate As Date
        Dim datEndDate As Date
        Dim strTarget As String
        Dim strOwner As String
        Dim strOffer As String
        Dim strStatus As String
        Dim strManPower As String
        Dim strBgtCost As String
        Dim strActCost As String
        Dim strDesc As String

        Dim strSQL As String
        Dim arySQLs() As String
        Dim I, J As Integer

        strCampID = Me.txtCampID.Text.Trim
        strCampName = Me.txtCampName.Text.Trim
        strObjective = Me.txtObjective.Text.Trim
        datStartDate = Me.dtpStartDate.Value.Date
        datEndDate = Me.dtpEndDate.Value.Date
        strTarget = Me.cboTarget.SelectedValue
        strOwner = Me.cboOwner.SelectedValue
        strOffer = Me.txtOffer.Text.Trim
        'If Campaign mark completed, all channels mark completed?
        strStatus = Me.cboStatus.SelectedValue
        strManPower = Me.txtManPower.Text.Trim
        strBgtCost = Me.txtBgtCost.Text.Trim
        strActCost = Me.txtActCost.Text.Trim
        strDesc = Me.txtDesc.Text.Trim
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If blnNew Then
            'strCampID = Me.cboOwner.SelectedValue & "_" & Format(Me.dtpStartDate.Value, "yyyyMM") & "_" & Me.txtDesc.Text.Trim.Substring(0, Math.Min(5, Me.txtDesc.Text.Trim.Length))
            Dim strObj As String
            'strObj = Replace(txtObjective.Text, " ", "")
            'strObj = Trim(Mid(strObj, 1, Math.Min(5, strObj.Length)))
            strObj = Format(Now, "fffff")
            strCampID = Me.cboOwner.SelectedValue & "_" & Format(Me.dtpStartDate.Value, "yyyyMM") & "_" & strObj
'                Me.txtObjective.Text.Trim.Substring(0, Math.Min(5, Me.txtObjective.Text.Trim.Length))

            'Check Campaign is existing or not
            If GetCampaign(dtData, strErrMsg, strCampID) Then
                If dtData.Rows.Count > 0 Then
                    MsgBox("Campaign is existing.", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Sub
                End If
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End If

            strSQL = "Insert into " & serverPrefix & "crm_campaign (crmcmp_campaign_id, crmcmp_campaign_name, crmcmp_campaign_objective, crmcmp_start_date,"
            strSQL &= " crmcmp_end_date, crmcmp_target_customer, crmcmp_owner_id, crmcmp_offer, crmcmp_status_id, crmcmp_manpower,"
            strSQL &= " crmcmp_budget_cost, crmcmp_actual_cost, crmcmp_description, crmcmp_create_user, crmcmp_create_date,"
            strSQL &= " crmcmp_update_user, crmcmp_update_date)"
            strSQL &= " Values ('" & strCampID.Replace("'", "''") & "', '" & strCampName.Replace("'", "''") & "', '"
            strSQL &= strObjective.Replace("'", "''") & "', '" & datStartDate.ToString("yyyy-MM-dd") & "', '" & datEndDate.ToString("yyyy-MM-dd") & "', '"
            strSQL &= strTarget.Replace("'", "''") & "', '" & strOwner.Replace("'", "''") & "', '" & strOffer.Replace("'", "''") & "', '"
            strSQL &= strStatus.Replace("'", "''") & "', "
            If strManPower.Length = 0 Then
                strSQL &= "Null" & ", "
            Else
                strSQL &= CInt(strManPower) & ", "
            End If
            If strBgtCost.Length = 0 Then
                strSQL &= "Null" & ", "
            Else
                strSQL &= CSng(strBgtCost) & ", "
            End If
            If strActCost.Length = 0 Then
                strSQL &= "Null" & ", "
            Else
                strSQL &= CSng(strActCost) & ", "
            End If
            strSQL &= "'" & strDesc.Replace("'", "''") & "', '" & gsUser.Replace("'", "''") & "', GetDate(), '" & gsUser.Replace("'", "''") & "', GetDate())"

            ReDim arySQLs(0)
            arySQLs(0) = ""
        Else

            strSQL = "Update " & serverPrefix & "crm_campaign Set crmcmp_campaign_name = '" & strCampName.Replace("'", "''") & "',"
            strSQL &= " crmcmp_campaign_objective = '" & strObjective.Replace("'", "''") & "',"
            strSQL &= " crmcmp_start_date = '" & datStartDate.ToString("yyyy-MM-dd") & "',"
            strSQL &= " crmcmp_end_date = '" & datEndDate.ToString("yyyy-MM-dd") & "',"
            strSQL &= " crmcmp_target_customer = '" & strTarget.Replace("'", "''") & "',"
            strSQL &= " crmcmp_owner_id = '" & strOwner.Replace("'", "''") & "',"
            strSQL &= " crmcmp_offer = '" & strOffer.Replace("'", "''") & "',"
            strSQL &= " crmcmp_status_id = '" & strStatus.Replace("'", "''") & "',"
            If strManPower.Length = 0 Then
                strSQL &= " crmcmp_manpower = " & "Null" & ","
            Else
                strSQL &= " crmcmp_manpower = " & CInt(strManPower) & ", "
            End If
            If strBgtCost.Length = 0 Then
                strSQL &= " crmcmp_budget_cost = " & "Null" & ","
            Else
                strSQL &= " crmcmp_budget_cost = " & CSng(strBgtCost) & ","
            End If
            If strActCost.Length = 0 Then
                strSQL &= " crmcmp_actual_cost = " & "Null" & ","
            Else
                strSQL &= " crmcmp_actual_cost = " & CSng(strActCost) & ","
            End If
            strSQL &= " crmcmp_description = '" & strDesc.Replace("'", "''") & "',"
            strSQL &= " crmcmp_update_user = '" & gsUser.Replace("'", "''") & "',"
            strSQL &= " crmcmp_update_date = GetDate()"
            strSQL &= " Where crmcmp_campaign_id = '" & strCampID.Replace("'", "''") & "'"

            ReDim arySQLs(0)
            arySQLs(0) = "Delete " & serverPrefix & "crm_campaign_channel Where crmcpc_campaign_id = '" & strCampID.Replace("'", "''") & "'"
        End If

        'Add Campaign Channel
        I = arySQLs.GetUpperBound(0)
        dtData = dsCampaign.Tables("CampaignChannel")
        If Not dtData Is Nothing Then
            ReDim Preserve arySQLs(I + dtData.Rows.Count)
            For J = 0 To dtData.Rows.Count - 1
                arySQLs(J + I + 1) = "Insert into " & serverPrefix & "crm_campaign_channel (crmcpc_campaign_id, crmcpc_channel_id, crmcpc_offer, crmcpc_status_id, crmcpc_manpower,"
                arySQLs(J + I + 1) &= " crmcpc_budget_cost, crmcpc_actual_cost, crmcpc_description, crmcpc_call_script, crmcpc_create_user, crmcpc_create_date,"
                arySQLs(J + I + 1) &= " crmcpc_update_user, crmcpc_update_date)"
                arySQLs(J + I + 1) &= " Values ('" & strCampID.Replace("'", "''") & "', '" & dtData.Rows(J).Item("ChannelID").ToString.Replace("'", "''") & "',"
                arySQLs(J + I + 1) &= " '" & dtData.Rows(J).Item("ChannelOffer").ToString.Replace("'", "''") & "',"
                'If the campaign is marked completed, all channels under this campaign should be compeleted
                If Me.cboStatus.Text.Trim.ToUpper = "COMPLETED" Then
                    arySQLs(J + I + 1) &= " '" & strStatus.Replace("'", "''") & "',"
                Else
                    arySQLs(J + I + 1) &= " '" & dtData.Rows(J).Item("ChannelStatus").ToString.Replace("'", "''") & "',"
                End If
                If dtData.Rows(J).IsNull("ChannelManPower") Then
                    arySQLs(J + I + 1) &= " Null,"
                Else
                    arySQLs(J + I + 1) &= " " & dtData.Rows(J).Item("ChannelManPower") & ","
                End If
                If dtData.Rows(J).IsNull("ChannelBudgetCost") Then
                    arySQLs(J + I + 1) &= " Null,"
                Else
                    arySQLs(J + I + 1) &= " " & dtData.Rows(J).Item("ChannelBudgetCost") & ","
                End If
                If dtData.Rows(J).IsNull("ChannelActualCost") Then
                    arySQLs(J + I + 1) &= " Null,"
                Else
                    arySQLs(J + I + 1) &= " " & dtData.Rows(J).Item("ChannelActualCost") & ","
                End If
                arySQLs(J + I + 1) &= " '" & dtData.Rows(J).Item("ChannelDesc").ToString.Replace("'", "''") & "',"
                arySQLs(J + I + 1) &= " '" & dtData.Rows(J).Item("ChannelCallScript").ToString.Replace("'", "''") & "',"
                arySQLs(J + I + 1) &= " '" & gsUser.Replace("'", "''") & "', GetDate(), '" & gsUser.Replace("'", "''") & "', GetDate())"
            Next
        End If


        Try
            sqlConn.ConnectionString = strCIWConn
            sqlConn.Open()
            sqlCmd = sqlConn.CreateCommand
            sqlTran = sqlConn.BeginTransaction("AddCampaign")
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTran

            'Insert/Update Campaign
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()

            'Insert/Update/Delete Campaign Channels
            For I = arySQLs.GetLowerBound(0) To arySQLs.GetUpperBound(0)
                If arySQLs(I).Trim.Length > 0 Then
                    sqlCmd.CommandText = arySQLs(I)
                    sqlCmd.ExecuteNonQuery()
                End If
            Next
            sqlTran.Commit()

            blnNew = False
            blnChange = False
            gCrmMode = ""
            Me.grdCampaign.Enabled = True

            Call EnableControls(False)
            Call EnableChannelControls(False)
            Call InitButtons()
            Call BindCampaignGrid()

        Catch ex As Exception
            Try
                sqlTran.Rollback("AddCampaign")
            Catch ex1 As Exception
                'Throw ex1
            End Try
            Throw ex
        Finally
            sqlConn.Close()
            sqlConn.Dispose()
            sqlCmd.Dispose()
            sqlTran.Dispose()
        End Try

    End Sub

    Private Sub DelCampaign()
        Dim sqlConn As New SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sqlTran As SqlTransaction
        dim sqlDA as SqlDataAdapter 
        Dim dtData As DataTable

        Dim strCampID As String
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strCampID = Me.txtCampID.Text.Trim

        Try
            sqlConn.ConnectionString = strCIWConn
            sqlConn.Open()
            'Check Campaign Exists in other tables or not
            strSQL = "Select Count(*) From " & serverPrefix & "crm_campaign_sales_leads Where crmcsl_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            dtData = New DataTable
            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            sqlDA.Fill(dtData)
            If dtData.Rows(0).Item(0) > 0 Then
                MsgBox("This Campaign is existing in Sales Leads and cannot be deleted.")
                Exit Try
            End If

            sqlCmd = sqlConn.CreateCommand
            sqlTran = sqlConn.BeginTransaction("DelCampaign")
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTran
            'Delete Campaign Channels
            strSQL = "Delete " & serverPrefix & "crm_campaign_channel Where crmcpc_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()
            'Delete Campaign
            strSQL = "Delete " & serverPrefix & "crm_campaign Where crmcmp_campaign_id = '" & strCampID.Replace("'", "''") & "'"
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()
            sqlTran.Commit()

            Call BindCampaignGrid()

        Catch ex As Exception
            Try
                sqlTran.Rollback("DelCampaign")
            Catch ex1 As Exception
                'Throw ex1
            End Try
            Throw ex
        Finally
            sqlConn.Close()
            sqlConn.Dispose()
            If Not sqlCmd Is Nothing Then sqlCmd.Dispose()
            If Not sqlTran Is Nothing Then sqlTran.Dispose()
            If Not sqlDA Is Nothing Then sqlDA.Dispose()
        End Try

    End Sub

    Private Sub SaveCampaignChannel()
        Dim dtData As DataTable
        Dim drData As DataRow
        Dim drTemp() As DataRow

        Dim strCampID As String
        Dim strChannelID As String
        Dim strChannelStatus As String
        Dim strChannelOffer As String
        Dim strChannelDesc As String
        Dim strChannelCallScript As String
        Dim strChannelManPower As String
        Dim strChannelBgtCost As String
        Dim strChannelActCost As String

        strCampID = Me.txtCampID.Text.Trim
        strChannelID = Me.cboChannel.SelectedValue
        strChannelStatus = Me.cboChannelStatus.SelectedValue
        strChannelOffer = Me.txtChannelOffer.Text.Trim
        strChannelDesc = Me.txtChannelDesc.Text.Trim
        strChannelCallScript = Me.txtChannelCallScript.Text.Trim
        strChannelManPower = Me.txtChannelManPower.Text.Trim
        strChannelBgtCost = Me.txtChannelBgtCost.Text.Trim
        strChannelActCost = Me.txtChannelActCost.Text.Trim

        dtData = dsCampaign.Tables("CampaignChannel")

        drTemp = dtData.Select("CampaignID = '" & strCampID.Replace("'", "''") & "' And ChannelID = '" & strChannelID.Replace("'", "''") & "'")

        If blnNewChannel Then
            If drTemp.GetUpperBound(0) < 0 Then
                drData = dtData.NewRow
                drData.Item("CampaignID") = strCampID
                drData.Item("ChannelID") = strChannelID
                drData.Item("ChannelOffer") = strChannelOffer
                drData.Item("ChannelStatus") = strChannelStatus
                If strChannelManPower.Length = 0 Then
                    drData.Item("ChannelManPower") = System.DBNull.Value
                Else
                    drData.Item("ChannelManPower") = CInt(strChannelManPower)
                End If
                If strChannelBgtCost.Length = 0 Then
                    drData.Item("ChannelBudgetCost") = System.DBNull.Value
                Else
                    drData.Item("ChannelBudgetCost") = CInt(strChannelBgtCost)
                End If
                If strChannelActCost.Length = 0 Then
                    drData.Item("ChannelActualCost") = System.DBNull.Value
                Else
                    drData.Item("ChannelActualCost") = CInt(strChannelActCost)
                End If
                drData.Item("ChannelDesc") = strChannelDesc
                drData.Item("ChannelCallScript") = strChannelCallScript
                drData.Item("ChannelName") = GetChannelName(strChannelID)
                dtData.Rows.Add(drData)
                dtData.AcceptChanges()
            Else
                MsgBox("Channel " & Me.cboChannel.Text & " is existing, please select another channel.", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End If
        Else
            If drTemp.GetUpperBound(0) >= 0 Then
                drData = drTemp(0)
                drData.Item("ChannelOffer") = strChannelOffer
                drData.Item("ChannelStatus") = strChannelStatus
                If strChannelManPower.Length = 0 Then
                    drData.Item("ChannelManPower") = System.DBNull.Value
                Else
                    drData.Item("ChannelManPower") = CInt(strChannelManPower)
                End If
                If strChannelBgtCost.Length = 0 Then
                    drData.Item("ChannelBudgetCost") = System.DBNull.Value
                Else
                    drData.Item("ChannelBudgetCost") = CInt(strChannelBgtCost)
                End If
                If strChannelActCost.Length = 0 Then
                    drData.Item("ChannelActualCost") = System.DBNull.Value
                Else
                    drData.Item("ChannelActualCost") = CInt(strChannelActCost)
                End If
                drData.Item("ChannelDesc") = strChannelDesc
                drData.Item("ChannelCallScript") = strChannelCallScript
                dtData.LoadDataRow(drData.ItemArray, True)
            End If
        End If

        blnNewChannel = False
        blnChangeChannel = False
        Me.grdChannel.Enabled = True

        Call EnableChannelControls(False)
        Me.cmdChannelAdd.Enabled = True
        Me.cmdChannelEdit.Enabled = True
        Me.cmdChannelDel.Enabled = True
        Me.cmdChannelSave.Enabled = False
        Me.cmdChannelCancel.Enabled = False

    End Sub

    Private Sub DelCampaignChannel()
        Dim sqlConn As New SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim strSQL As String

        Dim dtData As DataTable
        Dim drData() As DataRow
        Dim I As Integer

        Dim strCampID As String
        Dim strChannelID As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strCampID = Me.txtCampID.Text.Trim
        strChannelID = Me.cboChannel.SelectedValue

        Try
            'Check Campaign Exists in other tables or not
            sqlConn.ConnectionString = strCIWConn
            sqlConn.Open()
            strSQL = "Select Count(*) From " & serverPrefix & "crm_campaign_sales_leads Where crmcsl_campaign_id = '" & strCampID.Replace("'", "''") & "' And crmcsl_channel_id = '" & strChannelID.Replace("'", "''") & "'"
            dtData = New DataTable
            sqlDA = New SqlDataAdapter(strSQL, sqlConn)
            sqlDA.Fill(dtData)
            If dtData.Rows(0).Item(0) > 0 Then
                MsgBox("This Campaign Channel is existing in Sales Leads and cannot be deleted.")
                Exit Try
            End If

            dtData = dsCampaign.Tables("CampaignChannel")

            drData = dtData.Select("CampaignID = '" & strCampID.Replace("'", "''") & "' And ChannelID = '" & strChannelID.Replace("'", "''") & "'")
            For I = drData.GetLowerBound(0) To drData.GetUpperBound(0)
                dtData.Rows.Remove(drData(I))
                dtData.AcceptChanges()
            Next

            If dtData.Rows.Count = 0 Then
                Call ClearChannelTextBox()
                Me.cmdChannelEdit.Enabled = False
                Me.cmdChannelDel.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sqlConn.Close()
            sqlConn.Dispose()
            sqlDA.Dispose()
        End Try

    End Sub

    Private Sub bm_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bm.CurrentChanged
        Try
            blnNew = False
            blnNewChannel = False
            blnChange = False
            gCrmMode = ""
            blnChangeChannel = False
            Call EnableControls(False)
            Call EnableChannelControls(False)
            Call InitButtons()
            Call UpdatePT()
            Call BindChannelGrid(Me.txtCampID.Text)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub grdCampaign_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCampaign.MouseUp
        Try
            Dim pt = New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = grdCampaign.HitTest(pt)

            If hti.Type = DataGrid.HitTestType.Cell Then
                grdCampaign.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                grdCampaign.Select(hti.Row)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub bmChannel_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bmChannel.CurrentChanged
        Try
            blnNewChannel = False
            blnChangeChannel = False
            Call EnableChannelControls(False)
            'Initiate Channel Buttons base on Campaing status
            If Me.cmdSave.Enabled = False Then
                Me.cmdChannelAdd.Enabled = False
                Me.cmdChannelEdit.Enabled = False
                Me.cmdChannelDel.Enabled = False
                Me.cmdChannelSave.Enabled = False
                Me.cmdChannelCancel.Enabled = False
            Else
                Me.cmdChannelAdd.Enabled = True
                Me.cmdChannelEdit.Enabled = (bmChannel.Count > 0)
                Me.cmdChannelDel.Enabled = (bmChannel.Count > 0)
                Me.cmdChannelSave.Enabled = False
                Me.cmdChannelCancel.Enabled = False
            End If
            Call UpdateChannelTextBox()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub grdChannel_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdChannel.MouseUp
        Try
            Dim pt = New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = grdChannel.HitTest(pt)

            If hti.Type = DataGrid.HitTestType.Cell Then
                grdChannel.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                grdChannel.Select(hti.Row)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        Try
            blnNew = True
            blnChange = True
            gCrmMode = "CampaignDetail"
            Me.grdCampaign.Enabled = False

            'Enabel all controls for add new record
            Call EnableControls(True)
            Me.cmdAdd.Enabled = False
            Me.cmdUpdRating.Enabled = False
            Me.cmdEdit.Enabled = False
            Me.cmdDelete.Enabled = False
            Me.cmdSave.Enabled = True
            Me.cmdCancel.Enabled = True
            Me.cmdExit.Enabled = False
            Me.cmdChannelAdd.Enabled = True
            Me.cmdChannelEdit.Enabled = False
            Me.cmdChannelDel.Enabled = False
            Me.cmdChannelSave.Enabled = False
            Me.cmdChannelCancel.Enabled = False

            Call ClearTextBox()

            Call BindChannelGrid("")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try
            If Validation() Then
                Call SaveCampaign()
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            Me.ParentForm.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try
            blnNew = False
            blnNewChannel = False
            blnChange = False
            gCrmMode = ""
            blnChangeChannel = False
            Me.grdCampaign.Enabled = True

            Call EnableControls(False)
            Call EnableChannelControls(False)
            Me.cmdAdd.Enabled = True
            Me.cmdUpdRating.Enabled = True
            Me.cmdEdit.Enabled = (Me.bm.Count > 0)
            Me.cmdDelete.Enabled = (Me.bm.Count > 0)
            Me.cmdSave.Enabled = False
            Me.cmdCancel.Enabled = False
            Me.cmdExit.Enabled = True
            Me.cmdChannelAdd.Enabled = False
            Me.cmdChannelEdit.Enabled = False
            Me.cmdChannelDel.Enabled = False
            Me.cmdChannelSave.Enabled = False
            Me.cmdChannelCancel.Enabled = False

            Call UpdatePT()
            Call BindChannelGrid(Me.txtCampID.Text)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try
            blnChange = True
            gCrmMode = "CampaignDetail"
            Me.grdCampaign.Enabled = False
            If Me.cboStatus.SelectedValue = "03" Then
                Me.txtActCost.Enabled = True
            Else
                Call EnableControls(True)
            End If
            Me.cmdAdd.Enabled = False
            Me.cmdUpdRating.Enabled = False
            Me.cmdEdit.Enabled = False
            Me.cmdDelete.Enabled = False
            Me.cmdSave.Enabled = True
            Me.cmdCancel.Enabled = True
            Me.cmdExit.Enabled = False
            Me.cmdChannelAdd.Enabled = True
            Me.cmdChannelEdit.Enabled = (Me.bmChannel.Count > 0)
            Me.cmdChannelDel.Enabled = (Me.bmChannel.Count > 0)
            Me.cmdChannelSave.Enabled = False
            Me.cmdChannelCancel.Enabled = False
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try
            If MsgBox("Are you sure to delete this campaign record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Delete Campaign") = MsgBoxResult.Yes Then
                Call DelCampaign()
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdChannelAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannelAdd.Click

        Try
            blnNewChannel = True
            blnChangeChannel = True
            Me.grdChannel.Enabled = False

            Call EnableChannelControls(True)
            Me.cmdChannelAdd.Enabled = False
            Me.cmdChannelEdit.Enabled = False
            Me.cmdChannelDel.Enabled = False
            Me.cmdChannelSave.Enabled = True
            Me.cmdChannelCancel.Enabled = True

            Call ClearChannelTextBox()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdChannelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannelEdit.Click

        Try
            blnChangeChannel = True
            Me.grdChannel.Enabled = False
            If Me.cboChannelStatus.SelectedValue = "03" Then
                Me.txtChannelActCost.Enabled = True
            Else
                Call EnableChannelControls(True)
                Me.cboChannel.Enabled = False
            End If
            Me.cmdChannelAdd.Enabled = False
            Me.cmdChannelEdit.Enabled = False
            Me.cmdChannelDel.Enabled = False
            Me.cmdChannelSave.Enabled = True
            Me.cmdChannelCancel.Enabled = True
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdChannelDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannelDel.Click

        Try
            If MsgBox("Are you sure to delete this campaign channel record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Delete Campaign Channel") = MsgBoxResult.Yes Then
                Call DelCampaignChannel()
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdChannelSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannelSave.Click

        Try
            If ValidationChannel() Then
                Call SaveCampaignChannel()
            End If
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdChannelCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannelCancel.Click

        Try
            blnNewChannel = False
            blnChangeChannel = False
            Me.grdChannel.Enabled = True

            Call EnableChannelControls(False)
            Me.cmdChannelAdd.Enabled = True
            Me.cmdChannelEdit.Enabled = (Me.bmChannel.Count > 0)
            Me.cmdChannelDel.Enabled = (Me.bmChannel.Count > 0)
            Me.cmdChannelSave.Enabled = False
            Me.cmdChannelCancel.Enabled = False

            Call UpdateChannelTextBox()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblChannelCallScript.LinkClicked

        Try
            Call Me.OpenFileDialog.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub OpenFileDialog_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog.FileOk

        Try
            If Me.OpenFileDialog.FileName.Length > 0 Then
                Me.txtChannelCallScript.Text = Me.OpenFileDialog.FileName.Trim
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

    Private Sub cmdUpdRating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdRating.Click

        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL, strCampaign, strChannel, strActivity As String
        Dim daCampCall As SqlDataAdapter
        Dim dtRating As New DataTable
        Dim sqlConn As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        sqlConn.ConnectionString = strCIWConn

        frmParam.Text = "Update Customer Rating"
        frmParam.LoadCampData = True
        frmParam.cboCampaign.SelectedValue = txtCampID.Text
        frmParam.cboCampaign.Enabled = False
        frmParam.dtFrom.Value = DateAdd(DateInterval.Month, -1, Today)
        frmParam.dtTo.Value = Today

        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            strCampaign = frmParam.cboCampaign.SelectedValue
            strChannel = frmParam.cboChannel.SelectedValue
            strActivity = frmParam.txtActivity.Text
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        ' +/- Rider
        strSQL = "Select DISTINCT CustomerID " &
            " From " & serverPrefix & "crm_campaign_sales_leads, " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_channel_type, " & serverPrefix & " clienthistory h " &
            " Where crmcsl_campaign_id = '" & strCampaign & "' " &
            " And crmcsl_campaign_id = crmcmp_campaign_id " &
            " And crmcsl_channel_id = crmcpt_channel_id " &
            " And crmcsl_customer_id = h.CustomerID " &
            " And Activity in (" & strActivity & ") " &
            " And h.Remarks like '%ADD%' " &
            " And date between '" & strFromDate & "' AND '" & strToDate & "'"

        If strChannel <> "ALL" Then
            strSQL &= " And crmcsl_channel_id = '" & strChannel & "' "
        End If

        ' New Business
        strSQL &= " UNION " & _
            " Select DISTINCT r.CustomerID " & _
            " From " & serverPrefix & "crm_campaign_sales_leads, " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_channel_type, policyaccount p, csw_poli_rel r, customer c " & _
            " Where crmcsl_campaign_id = '" & strCampaign & "' " & _
            " And crmcsl_campaign_id = crmcmp_campaign_id " & _
            " And crmcsl_channel_id = crmcpt_channel_id " & _
            " And crmcsl_customer_id = r.customerid " & _
            " And r.CustomerID = c.CustomerID " & _
            " And r.policyrelatecode = 'PH' " & _
            " And r.policyaccountid = p.policyaccountid " & _
            " And p.CommencementDate between '" & strFromDate & "' AND '" & strToDate & "'"

        If strChannel <> "ALL" Then
            strSQL &= " And crmcsl_channel_id = '" & strChannel & "' "
        End If

        Try
            daCampCall = New SqlDataAdapter(strSQL, sqlConn)
            daCampCall.Fill(dtRating)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Dim strRate As String
        Dim dr As DataRow
        Dim lngErrNo As Long
        Dim intCnt As Integer
        Dim strErrMsg As String

        If Not dtRating Is Nothing AndAlso dtRating.Rows.Count > 0 Then

            strRate = InputBox("Please input the rate to add to customer " & vbCrLf & "(Customer count: " & dtRating.Rows.Count & "):", "Update Rating")
            If IsNumeric(strRate) Then

                For Each dr In dtRating.Rows

                    lngErrNo = 0
                    'oliver 2024-5-3 added for Table_Relocate_Sprint13
                    strSQL = "Update " & serverPrefix & " csw_demographic " &
                        " Set cswdgm_rating = isnull(cswdgm_rating,0) + " & strRate &
                        " Where cswdgm_cust_id = '" & dr.Item("CustomerID") & "'"
                    intCnt = objCS.UpdateData(strSQL, "N", "CIW", lngErrNo, strErrMsg)

                    If lngErrNo <> 0 Then
                        MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
                        Exit Sub
                    Else
                        If intCnt = 0 Then
                            'oliver 2024-5-3 added for Table_Relocate_Sprint13
                            strSQL = "Insert " & serverPrefix & " csw_demographic " &
                                "   (cswdgm_cust_id, cswdgm_email_addr, cswdgm_mar_stat, cswdgm_edu_level, cswdgm_no_of_dep, " &
                                "    cswdgm_ann_sal, cswdgm_optout_email, cswdgm_optout_call, cswdgm_rating, cswdgm_household_income, cswdgm_remark, cswdgm_occupation) " &
                                " Values ('" & dr.Item("CustomerID") & "'," &
                                "'', '', 0, 0, 0, 'N', 'N', " & strRate & ", 0, '', 0) "
                            intCnt = objCS.UpdateData(strSQL, "N", "CIW", lngErrNo, strErrMsg)
                            If lngErrNo <> 0 Then
                                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, gSystem)
                                Exit Sub
                            End If
                        End If
                    End If
                Next
                MsgBox(dtRating.Rows.Count & " Customer Rating update completed successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Else
                MsgBox("Please input a numeric customer rate for update.", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
            End If
        Else
            MsgBox("No record to update.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
        End If

        daCampCall = Nothing
        sqlConn = Nothing

    End Sub

End Class
