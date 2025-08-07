Imports System.Data.SqlClient

Public Class CustHist
    Inherits System.Windows.Forms.UserControl
    'oliver 2024-7-5 added comment for Table_Relocate_Sprint 14,Its policy reference Visible = False

    Private ds As DataSet = New DataSet("CustomerHistory")
    Private strPolicy, strPolicy2, strMode, strErr As String
    Private blnAdmin, blnLA As Boolean
    Private dr As DataRow
    Private datFirst As Date
    Private bm As BindingManagerBase

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
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtDept As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtActivity As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtChiName As System.Windows.Forms.TextBox
    Friend WithEvents grdCustHist As System.Windows.Forms.DataGrid
    Friend WithEvents txtRemarks1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks2 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks3 As System.Windows.Forms.TextBox
    Friend WithEvents lblLimit As System.Windows.Forms.Label
    Friend WithEvents txtFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdGO As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtActCode As System.Windows.Forms.TextBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents txtSeqNo As System.Windows.Forms.TextBox
    Friend WithEvents txtClientID As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.txtDept = New System.Windows.Forms.TextBox
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.txtActivity = New System.Windows.Forms.TextBox
        Me.txtRemarks1 = New System.Windows.Forms.TextBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.txtChiName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdCustHist = New System.Windows.Forms.DataGrid
        Me.txtRemarks2 = New System.Windows.Forms.TextBox
        Me.txtRemarks3 = New System.Windows.Forms.TextBox
        Me.lblLimit = New System.Windows.Forms.Label
        Me.txtFDate = New System.Windows.Forms.DateTimePicker
        Me.cmdGO = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtActCode = New System.Windows.Forms.TextBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.txtSeqNo = New System.Windows.Forms.TextBox
        Me.txtClientID = New System.Windows.Forms.TextBox
        CType(Me.grdCustHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDate.Location = New System.Drawing.Point(96, 204)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(81, 20)
        Me.txtDate.TabIndex = 28
        '
        'txtID
        '
        Me.txtID.AcceptsReturn = True
        Me.txtID.BackColor = System.Drawing.SystemColors.Window
        Me.txtID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(240, 204)
        Me.txtID.MaxLength = 0
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtID.Size = New System.Drawing.Size(81, 20)
        Me.txtID.TabIndex = 27
        '
        'txtUser
        '
        Me.txtUser.AcceptsReturn = True
        Me.txtUser.BackColor = System.Drawing.SystemColors.Window
        Me.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtUser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUser.Location = New System.Drawing.Point(384, 204)
        Me.txtUser.MaxLength = 0
        Me.txtUser.Name = "txtUser"
        Me.txtUser.ReadOnly = True
        Me.txtUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUser.Size = New System.Drawing.Size(81, 20)
        Me.txtUser.TabIndex = 26
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDept.Location = New System.Drawing.Point(524, 204)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReadOnly = True
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(81, 20)
        Me.txtDept.TabIndex = 25
        '
        'txtTitle
        '
        Me.txtTitle.AcceptsReturn = True
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTitle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTitle.Location = New System.Drawing.Point(96, 228)
        Me.txtTitle.MaxLength = 0
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTitle.Size = New System.Drawing.Size(49, 20)
        Me.txtTitle.TabIndex = 24
        '
        'txtActivity
        '
        Me.txtActivity.AcceptsReturn = True
        Me.txtActivity.BackColor = System.Drawing.SystemColors.Window
        Me.txtActivity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActivity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtActivity.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtActivity.Location = New System.Drawing.Point(148, 252)
        Me.txtActivity.MaxLength = 0
        Me.txtActivity.Name = "txtActivity"
        Me.txtActivity.ReadOnly = True
        Me.txtActivity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActivity.Size = New System.Drawing.Size(456, 20)
        Me.txtActivity.TabIndex = 23
        '
        'txtRemarks1
        '
        Me.txtRemarks1.AcceptsReturn = True
        Me.txtRemarks1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRemarks1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks1.Location = New System.Drawing.Point(96, 276)
        Me.txtRemarks1.MaxLength = 50
        Me.txtRemarks1.Name = "txtRemarks1"
        Me.txtRemarks1.ReadOnly = True
        Me.txtRemarks1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks1.Size = New System.Drawing.Size(509, 20)
        Me.txtRemarks1.TabIndex = 2
        '
        'txtLastName
        '
        Me.txtLastName.AcceptsReturn = True
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLastName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastName.Location = New System.Drawing.Point(148, 228)
        Me.txtLastName.MaxLength = 0
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastName.Size = New System.Drawing.Size(145, 20)
        Me.txtLastName.TabIndex = 21
        '
        'txtFirstName
        '
        Me.txtFirstName.AcceptsReturn = True
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFirstName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFirstName.Location = New System.Drawing.Point(296, 228)
        Me.txtFirstName.MaxLength = 0
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFirstName.Size = New System.Drawing.Size(205, 20)
        Me.txtFirstName.TabIndex = 20
        '
        'txtChiName
        '
        Me.txtChiName.AcceptsReturn = True
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChiName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtChiName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChiName.Location = New System.Drawing.Point(504, 228)
        Me.txtChiName.MaxLength = 0
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChiName.Size = New System.Drawing.Size(101, 20)
        Me.txtChiName.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 280)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Remarks"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(4, 256)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Activity"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Customer Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(488, 208)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Dept."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(336, 208)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "User ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(192, 208)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "ID Card"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 208)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Date"
        '
        'grdCustHist
        '
        Me.grdCustHist.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCustHist.BackColor = System.Drawing.Color.White
        Me.grdCustHist.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCustHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCustHist.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCustHist.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCustHist.CaptionVisible = False
        Me.grdCustHist.DataMember = ""
        Me.grdCustHist.FlatMode = True
        Me.grdCustHist.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCustHist.ForeColor = System.Drawing.Color.Black
        Me.grdCustHist.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCustHist.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCustHist.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCustHist.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCustHist.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCustHist.Location = New System.Drawing.Point(4, 28)
        Me.grdCustHist.Name = "grdCustHist"
        Me.grdCustHist.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCustHist.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCustHist.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCustHist.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCustHist.Size = New System.Drawing.Size(704, 172)
        Me.grdCustHist.TabIndex = 36
        '
        'txtRemarks2
        '
        Me.txtRemarks2.AcceptsReturn = True
        Me.txtRemarks2.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRemarks2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks2.Location = New System.Drawing.Point(96, 300)
        Me.txtRemarks2.MaxLength = 50
        Me.txtRemarks2.Name = "txtRemarks2"
        Me.txtRemarks2.ReadOnly = True
        Me.txtRemarks2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks2.Size = New System.Drawing.Size(509, 20)
        Me.txtRemarks2.TabIndex = 3
        '
        'txtRemarks3
        '
        Me.txtRemarks3.AcceptsReturn = True
        Me.txtRemarks3.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRemarks3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks3.Location = New System.Drawing.Point(96, 324)
        Me.txtRemarks3.MaxLength = 50
        Me.txtRemarks3.Name = "txtRemarks3"
        Me.txtRemarks3.ReadOnly = True
        Me.txtRemarks3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks3.Size = New System.Drawing.Size(509, 20)
        Me.txtRemarks3.TabIndex = 4
        '
        'lblLimit
        '
        Me.lblLimit.ForeColor = System.Drawing.Color.Red
        Me.lblLimit.Location = New System.Drawing.Point(277, 5)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(440, 16)
        Me.lblLimit.TabIndex = 84
        '
        'txtFDate
        '
        Me.txtFDate.Enabled = False
        Me.txtFDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFDate.Location = New System.Drawing.Point(104, 4)
        Me.txtFDate.Name = "txtFDate"
        Me.txtFDate.Size = New System.Drawing.Size(132, 20)
        Me.txtFDate.TabIndex = 83
        '
        'cmdGO
        '
        Me.cmdGO.Enabled = False
        Me.cmdGO.Location = New System.Drawing.Point(240, 4)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(32, 20)
        Me.cmdGO.TabIndex = 82
        Me.cmdGO.Text = "Go"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(97, 13)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "Enquiry From Date:"
        '
        'txtActCode
        '
        Me.txtActCode.AcceptsReturn = True
        Me.txtActCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtActCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtActCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtActCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtActCode.Location = New System.Drawing.Point(96, 252)
        Me.txtActCode.MaxLength = 5
        Me.txtActCode.Name = "txtActCode"
        Me.txtActCode.ReadOnly = True
        Me.txtActCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActCode.Size = New System.Drawing.Size(49, 20)
        Me.txtActCode.TabIndex = 1
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(4, 356)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdAdd.TabIndex = 5
        Me.cmdAdd.Text = "Add"
        '
        'cmdEdit
        '
        Me.cmdEdit.Enabled = False
        Me.cmdEdit.Location = New System.Drawing.Point(84, 356)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(75, 23)
        Me.cmdEdit.TabIndex = 6
        Me.cmdEdit.Text = "Edit"
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(164, 356)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "Save"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(244, 356)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "Cancel"
        '
        'txtSeqNo
        '
        Me.txtSeqNo.Location = New System.Drawing.Point(608, 356)
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.Size = New System.Drawing.Size(100, 20)
        Me.txtSeqNo.TabIndex = 90
        Me.txtSeqNo.Visible = False
        '
        'txtClientID
        '
        Me.txtClientID.Location = New System.Drawing.Point(608, 332)
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.Size = New System.Drawing.Size(100, 20)
        Me.txtClientID.TabIndex = 91
        Me.txtClientID.Visible = False
        '
        'CustHist
        '
        Me.Controls.Add(Me.txtClientID)
        Me.Controls.Add(Me.txtSeqNo)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.txtActCode)
        Me.Controls.Add(Me.lblLimit)
        Me.Controls.Add(Me.txtFDate)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRemarks3)
        Me.Controls.Add(Me.txtRemarks2)
        Me.Controls.Add(Me.grdCustHist)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtDept)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.txtActivity)
        Me.Controls.Add(Me.txtRemarks1)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.txtChiName)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "CustHist"
        Me.Size = New System.Drawing.Size(720, 388)
        CType(Me.grdCustHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property srcDTCustHist(ByVal srcDTNA As DataTable, ByVal strPol As String, Optional ByVal Admin As Boolean = False, Optional ByVal isLA As Boolean = False)
        Set(ByVal Value)
            If Me.DesignMode Then
                Return
            End If
            If isLA Then

                ' **** ES006 begin ****
                ds.Tables.Add(srcDTNA)
                strPolicy = strPol
                blnAdmin = Admin
                blnLA = isLA

                Dim Policy2, strErr As String
                Policy2 = ""
                strErr = ""

                If GetPolicyMap(strPolicy, Policy2, strErr) Then
                    If strPolicy <> Policy2 Then
                        strPolicy2 = Policy2
                    Else
                        strPolicy2 = strPolicy
                    End If
                End If
                ' **** ES006 end ****

                Call buildUI()
            Else
                If Not Value Is Nothing Then
                    ds.Tables.Add(Value)
                    ds.Tables.Add(srcDTNA)
                    strPolicy = strPol
                    blnAdmin = Admin
                    Call buildUI()
                End If
            End If
        End Set
    End Property

    Private Sub buildUI()

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable

        Try
            strSQL = "Select * from ActivityCodes "

            ' **** ES006 begin ****
            If blnLA = True Then
                strSQL &= "Select 'N' as ContFlag, Date, SeqNo, PolicyAccountid, Convert(char(8),CustomerID) as ClientID, Dept, UserID, Space(1) as Type, Activity," &
                    " convert(datetime,'19000101') as FollowUpDate, Space(3) as FollowUpUser, space(5) as FollowUpAction, " &
                    " isnull(left(Remarks,50),space(50)) as Comment1, isnull(substring(Remarks, 51, 100),space(50)) as Comment2, isnull(substring(Remarks, 101, 150),space(50)) as Comment3 " &
                    " from ClientHistory Where (policyaccountid = '" & strPolicy & "' or policyaccountid = '" & strPolicy2 & "') " &
                    " and policyaccountid <> ''"
            End If
            ' **** ES006 end ****

            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough

            ' **** ES006 begin ****
            If blnLA = True Then
                sqlda.TableMappings.Add("ActivityCodes1", "CustHist")
            End If
            ' **** ES006 end ****

            sqlda.Fill(ds, "ActivityCodes")

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        sqlconnect.Dispose()

        Dim relActRel As New Data.DataRelation("ActRel", ds.Tables("ActivityCodes").Columns("ActivityCode"),
            ds.Tables("CustHist").Columns("Activity"), True)
        Dim relClientRel As New Data.DataRelation("ClientRel", ds.Tables("ORDUNA").Columns("ClientID"),
            ds.Tables("CustHist").Columns("ClientID"), True)

        Try
            ds.Relations.Add(relActRel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relClientRel)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        'If ds.Tables("CustHist").Rows.Count > 0 Then
        'With ds.Tables("CustHist").Rows(0)

        With ds.Tables("CustHist")
            .Columns.Add("ActivityDesc", GetType(String))
            .Columns.Add("FirstName", GetType(String))
            .Columns.Add("NameSuffix", GetType(String))
            .Columns.Add("ChiName", GetType(String))
            .Columns.Add("GovernmentIDCard", GetType(String))
            .Columns.Add("PassportNumber", GetType(String))
        End With

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Date"
        cs.HeaderText = "Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        'cs = New DataGridTextBoxColumn
        cs = New clsDataGridTextBoxColumn("REDA")
        cs.Width = 100
        cs.MappingName = "Activity"
        cs.HeaderText = "Activity Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("ActRel", ds.Tables("ActivityCodes").Columns("ActivityDesc"))
        cs.Width = 150
        cs.MappingName = "ActivityDesc"
        cs.HeaderText = "Activity"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("ClientRel", ds.Tables("ORDUNA").Columns("GovernmentIDCard"))
        cs.Width = 100
        cs.MappingName = "GovernmentIDCard"
        cs.HeaderText = "ID Card"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("ClientRel", ds.Tables("ORDUNA").Columns("NameSuffix"))
        cs.Width = 80
        cs.MappingName = "NameSuffix"
        cs.HeaderText = "Last Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("ClientRel", ds.Tables("ORDUNA").Columns("FirstName"))
        cs.Width = 80
        cs.MappingName = "FirstName"
        cs.HeaderText = "First Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("ClientRel", ds.Tables("ORDUNA").Columns("ChiName"))
        cs.Width = 80
        cs.MappingName = "ChiName"
        cs.HeaderText = "Chinese Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "UserID"
        cs.HeaderText = "User ID."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Dept"
        cs.HeaderText = "Dept."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CustHist"
        grdCustHist.TableStyles.Add(ts)

        grdCustHist.DataSource = ds.Tables("CustHist")
        ds.Tables("CustHist").DefaultView.Sort = "Date DESC"
        grdCustHist.AllowDrop = False
        grdCustHist.ReadOnly = True

        'End With
        bm = Me.BindingContext(ds.Tables("CustHist"))
        lblLimit.Text = CheckLimit(ds.Tables("CustHist"))

        If bm.Count > 0 Then
            txtFDate.Enabled = True
            cmdGO.Enabled = True
            datFirst = ds.Tables("CustHist").Rows(0).Item("Date")
        Else
            datFirst = Today
        End If

        If blnAdmin Then
            'cmdAdd.Enabled = True
            cmdEdit.Enabled = True
            cmdSave.Enabled = False
            cmdCancel.Enabled = False
        End If

        If bm.Count = 0 Then cmdEdit.Enabled = False

        ' **** ES006 begin ****
        If blnLA Then
            txtFDate.Enabled = False
            cmdGO.Enabled = False
            lblLimit.Text = "***CAPSIL Client History for reference only****"
            lblLimit.Visible = True

            cmdAdd.Enabled = False
            cmdEdit.Enabled = False
            cmdSave.Enabled = False
            cmdCancel.Enabled = False
        End If
        ' **** ES006 end ****

        'txtDate.DataBindings.Add("Text", ds.Tables("CustHist"), "Date")
        'Dim bDate As Binding = New Binding("Text", ds.Tables("CustHist"), "Date")
        'Me.txtDate.DataBindings.Add(bDate)
        'AddHandler bDate.Format, AddressOf FormatDate

        'txtUser.DataBindings.Add("Text", ds.Tables("CustHist"), "UserID")
        'txtDept.DataBindings.Add("Text", ds.Tables("CustHist"), "Dept")
        'txtActCode.DataBindings.Add("Text", ds.Tables("CustHist"), "Activity")
        'txtRemarks1.DataBindings.Add("Text", ds.Tables("CustHist"), "Comment1")
        'txtRemarks2.DataBindings.Add("Text", ds.Tables("CustHist"), "Comment2")
        'txtRemarks3.DataBindings.Add("Text", ds.Tables("CustHist"), "Comment3")

        Call UpdatePT()

        'End If

    End Sub

    Private Sub grdCustHist_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCustHist.CurrentCellChanged
        Call UpdatePT()
    End Sub

    Private Sub UpdatePT()

        If bm.Count > 0 Then
            Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

            If Not drI Is Nothing Then
                txtClientID.Text = GetRelationValue(drI, "ClientRel", "ClientID")
                txtID.Text = GetRelationValue(drI, "ClientRel", "GovernmentIDCard")
                txtTitle.Text = GetRelationValue(drI, "ClientRel", "NamePrefix")
                txtLastName.Text = GetRelationValue(drI, "ClientRel", "NameSuffix")
                txtFirstName.Text = GetRelationValue(drI, "ClientRel", "FirstName")
                txtChiName.Text = GetRelationValue(drI, "ClientRel", "ChiName")
                txtActivity.Text = GetRelationValue(drI, "ActRel", "ActivityDesc")
                txtFDate.Text = drI.Item("Date")
                txtSeqNo.Text = Format(drI.Item("SeqNo"), "00")

                txtDate.Text = Format(drI.Item("Date"), gDateFormat)
                txtUser.Text = drI.Item("UserID")
                txtDept.Text = drI.Item("Dept")
                txtActCode.Text = Trim(drI.Item("Activity"))
                txtRemarks1.Text = Trim(drI.Item("Comment1"))
                txtRemarks2.Text = Trim(drI.Item("Comment2"))
                txtRemarks3.Text = Trim(drI.Item("Comment3"))
            End If
        End If

    End Sub

    Private Sub grdCustHist_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCustHist.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdCustHist.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdCustHist.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdCustHist.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click

        Call ReloadRecord(CDate(txtFDate.Text))

    End Sub

    Private Sub ReloadRecord(ByVal datEnq As Date)

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dt As DataTable

        wndMain.Cursor = Cursors.WaitCursor

        dt = objCS.GetClientHistory(strPolicy, "", "", datEnq, "", "", "", "", "B", "", #1/1/1900#, "", "", "", "", "", lngErrNo, strErrMsg)

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Or (lngErrNo = -1 And InStr(UCase(strErrMsg), "RECORD DOES NOT EXIST") > 0) Then
            ds.Tables("CustHist").Rows.Clear()

            Dim dr As DataRow
            Dim ar() As Object
            For Each dr In dt.Rows
                ar = dr.ItemArray
                ds.Tables("CustHist").Rows.Add(ar)
            Next
            lblLimit.Text = CheckLimit(ds.Tables("CustHist"))
            Call UpdatePT()
        End If

        If lngErrNo = -1 Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        strMode = "C"
        Call EnableFields()
        txtDate.Text = Format(Today, gDateFormat)
        txtUser.Text = Microsoft.VisualBasic.Strings.Right(gsUser, 3)
        txtDept.Text = Microsoft.VisualBasic.Strings.Left(gsUser, 3)
        txtActivity.Text = ""
        txtActCode.Text = ""
        txtRemarks1.Text = ""
        txtRemarks2.Text = ""
        txtRemarks3.Text = ""
        txtActCode.Focus()

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        strMode = "M"
        Call EnableFields()
        txtActCode.Focus()

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim strSeq As String
        Dim datCur As Date
        Dim strErrMsg As String
        Dim lngErrNo As Long

        datCur = Today

        If strMode = "M" Then
            strSeq = Trim(txtSeqNo.Text)
            datCur = CDate(txtFDate.Text)
        End If

        If strMode = "C" Then
            strSeq = "00"
        End If

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    datCur = #9/30/2005#
        '    datFirst = datCur
        'End If
        'AC - Change to use configuration setting - end


        wndMain.Cursor = Cursors.WaitCursor

        Call objCS.GetClientHistory(strPolicy, "H", txtClientID.Text, datCur,
            strSeq, Microsoft.VisualBasic.Strings.Right(gsUser, 3), txtDept.Text,
            txtUser.Text, strMode, txtActCode.Text, #1/1/1900#, "", "",
            txtRemarks1.Text, txtRemarks2.Text, txtRemarks3.Text, lngErrNo, strErrMsg)

        wndMain.Cursor = Cursors.Default

        If lngErrNo <> 0 Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Client History")
            Call UpdatePT()
        Else
            MsgBox("Record saved successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Client History")
            ' Refresh grid if new record added
            Call ReloadRecord(datFirst)
            bm.Position = 0
        End If

        Call DisableFields()

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Call DisableFields()
        Call UpdatePT()

    End Sub

    Private Sub EnableFields()

        txtActCode.ReadOnly = False
        txtRemarks1.ReadOnly = False
        txtRemarks2.ReadOnly = False
        txtRemarks3.ReadOnly = False
        cmdAdd.Enabled = False
        cmdEdit.Enabled = False
        cmdSave.Enabled = True
        cmdCancel.Enabled = True
        txtFDate.Enabled = False
        cmdGO.Enabled = False

    End Sub

    Private Sub DisableFields()

        strMode = ""
        txtActCode.ReadOnly = True
        txtRemarks1.ReadOnly = True
        txtRemarks2.ReadOnly = True
        txtRemarks3.ReadOnly = True

        cmdAdd.Enabled = True
        If blnAdmin Then cmdEdit.Enabled = True
        If bm.Count = 0 Then cmdEdit.Enabled = False
        cmdSave.Enabled = False
        cmdCancel.Enabled = False

        txtFDate.Enabled = True
        cmdGO.Enabled = True

    End Sub

    Private Sub txtActCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtActCode.LostFocus
        If Me.DesignMode Then
            Return
        End If

        Dim dr() As DataRow

        dr = ds.Tables("ActivityCodes").Select("ActivityCode = '" & Trim(txtActCode.Text) & "'")
        If dr.Length > 0 Then
            txtActivity.Text = dr(0).Item("ActivityDesc")
        Else
            txtActivity.Text = ""
        End If

    End Sub

End Class
