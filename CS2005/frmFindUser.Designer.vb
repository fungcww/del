<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFindUser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlUpdateEmail = New System.Windows.Forms.Panel()
        Me.pnlChangeUsername = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnGoChangeUsername = New System.Windows.Forms.Button()
        Me.btnGoLinkCustomerId = New System.Windows.Forms.Button()
        Me.lblCustomerID = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblRefNo = New System.Windows.Forms.Label()
        Me.txtHKID = New System.Windows.Forms.TextBox()
        Me.btnGotoResetPassword = New System.Windows.Forms.Button()
        Me.btnBack4 = New System.Windows.Forms.Button()
        Me.btnClear4 = New System.Windows.Forms.Button()
        Me.btnChangeUsername = New System.Windows.Forms.Button()
        Me.txtNewUsername = New System.Windows.Forms.TextBox()
        Me.lblOldUsername = New System.Windows.Forms.Label()
        Me.txtOldUsername = New System.Windows.Forms.TextBox()
        Me.lblNewUsername = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblUsername2 = New System.Windows.Forms.Label()
        Me.txtUsername2 = New System.Windows.Forms.TextBox()
        Me.btnResetPassword = New System.Windows.Forms.Button()
        Me.btnClearInput = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.pnlLinkCustomerId = New System.Windows.Forms.Panel()
        Me.txtUsername3 = New System.Windows.Forms.TextBox()
        Me.btnBack3 = New System.Windows.Forms.Button()
        Me.btnLinkCustomerId = New System.Windows.Forms.Button()
        Me.btnClearInput3 = New System.Windows.Forms.Button()
        Me.txtCustomerId3 = New System.Windows.Forms.TextBox()
        Me.lblCustomerId3 = New System.Windows.Forms.Label()
        Me.cbxCustomerId = New System.Windows.Forms.ComboBox()
        Me.lblCustomerId3b = New System.Windows.Forms.Label()
        Me.lblUsername3 = New System.Windows.Forms.Label()
        Me.LCustCom = New System.Windows.Forms.Label()
        Me.CBCustCom = New System.Windows.Forms.ComboBox()
        Me.pnlUpdateEmail.SuspendLayout()
        Me.pnlChangeUsername.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLinkCustomerId.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlUpdateEmail
        '
        Me.pnlUpdateEmail.Controls.Add(Me.pnlChangeUsername)
        Me.pnlUpdateEmail.Controls.Add(Me.btnBack)
        Me.pnlUpdateEmail.Controls.Add(Me.lblEmail)
        Me.pnlUpdateEmail.Controls.Add(Me.txtEmail)
        Me.pnlUpdateEmail.Controls.Add(Me.lblUsername2)
        Me.pnlUpdateEmail.Controls.Add(Me.txtUsername2)
        Me.pnlUpdateEmail.Controls.Add(Me.btnResetPassword)
        Me.pnlUpdateEmail.Controls.Add(Me.btnClearInput)
        Me.pnlUpdateEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUpdateEmail.Location = New System.Drawing.Point(0, 0)
        Me.pnlUpdateEmail.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlUpdateEmail.Name = "pnlUpdateEmail"
        Me.pnlUpdateEmail.Size = New System.Drawing.Size(955, 454)
        Me.pnlUpdateEmail.TabIndex = 0
        '
        'pnlChangeUsername
        '
        Me.pnlChangeUsername.Controls.Add(Me.pnlSearch)
        Me.pnlChangeUsername.Controls.Add(Me.btnBack4)
        Me.pnlChangeUsername.Controls.Add(Me.btnClear4)
        Me.pnlChangeUsername.Controls.Add(Me.btnChangeUsername)
        Me.pnlChangeUsername.Controls.Add(Me.txtNewUsername)
        Me.pnlChangeUsername.Controls.Add(Me.lblOldUsername)
        Me.pnlChangeUsername.Controls.Add(Me.txtOldUsername)
        Me.pnlChangeUsername.Controls.Add(Me.lblNewUsername)
        Me.pnlChangeUsername.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlChangeUsername.Location = New System.Drawing.Point(0, 0)
        Me.pnlChangeUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlChangeUsername.Name = "pnlChangeUsername"
        Me.pnlChangeUsername.Size = New System.Drawing.Size(955, 454)
        Me.pnlChangeUsername.TabIndex = 15
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.btnSearch)
        Me.pnlSearch.Controls.Add(Me.btnGoChangeUsername)
        Me.pnlSearch.Controls.Add(Me.btnGoLinkCustomerId)
        Me.pnlSearch.Controls.Add(Me.lblCustomerID)
        Me.pnlSearch.Controls.Add(Me.txtCustomerID)
        Me.pnlSearch.Controls.Add(Me.lblUsername)
        Me.pnlSearch.Controls.Add(Me.dgvUsers)
        Me.pnlSearch.Controls.Add(Me.txtUsername)
        Me.pnlSearch.Controls.Add(Me.btnExit)
        Me.pnlSearch.Controls.Add(Me.lblRefNo)
        Me.pnlSearch.Controls.Add(Me.txtHKID)
        Me.pnlSearch.Controls.Add(Me.btnGotoResetPassword)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(955, 454)
        Me.pnlSearch.TabIndex = 3
        '
        'btnClear
        '
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClear.Location = New System.Drawing.Point(821, 75)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 28)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "&Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(701, 75)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 28)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnGoChangeUsername
        '
        Me.btnGoChangeUsername.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGoChangeUsername.Location = New System.Drawing.Point(132, 386)
        Me.btnGoChangeUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGoChangeUsername.Name = "btnGoChangeUsername"
        Me.btnGoChangeUsername.Size = New System.Drawing.Size(221, 28)
        Me.btnGoChangeUsername.TabIndex = 11
        Me.btnGoChangeUsername.Text = "Proceed to Change Username"
        Me.btnGoChangeUsername.UseVisualStyleBackColor = True
        '
        'btnGoLinkCustomerId
        '
        Me.btnGoLinkCustomerId.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGoLinkCustomerId.Location = New System.Drawing.Point(371, 386)
        Me.btnGoLinkCustomerId.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGoLinkCustomerId.Name = "btnGoLinkCustomerId"
        Me.btnGoLinkCustomerId.Size = New System.Drawing.Size(224, 28)
        Me.btnGoLinkCustomerId.TabIndex = 10
        Me.btnGoLinkCustomerId.Text = "Proceed to &Link Customer ID"
        Me.btnGoLinkCustomerId.UseVisualStyleBackColor = True
        '
        'lblCustomerID
        '
        Me.lblCustomerID.AutoSize = True
        Me.lblCustomerID.Location = New System.Drawing.Point(661, 37)
        Me.lblCustomerID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerID.Name = "lblCustomerID"
        Me.lblCustomerID.Size = New System.Drawing.Size(80, 16)
        Me.lblCustomerID.TabIndex = 9
        Me.lblCustomerID.Text = "Customer ID"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.Location = New System.Drawing.Point(748, 33)
        Me.txtCustomerID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerID.MaxLength = 500
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(172, 22)
        Me.txtCustomerID.TabIndex = 2
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(375, 37)
        Me.lblUsername.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(56, 16)
        Me.lblUsername.TabIndex = 2
        Me.lblUsername.Text = "Login ID"
        '
        'dgvUsers
        '
        Me.dgvUsers.AllowUserToAddRows = False
        Me.dgvUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Location = New System.Drawing.Point(20, 116)
        Me.dgvUsers.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvUsers.MultiSelect = False
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.ReadOnly = True
        Me.dgvUsers.RowHeadersWidth = 51
        Me.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsers.Size = New System.Drawing.Size(899, 235)
        Me.dgvUsers.TabIndex = 5
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(449, 33)
        Me.txtUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsername.MaxLength = 500
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(172, 22)
        Me.txtUsername.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(819, 386)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 28)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblRefNo
        '
        Me.lblRefNo.AutoSize = True
        Me.lblRefNo.Location = New System.Drawing.Point(16, 33)
        Me.lblRefNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRefNo.Name = "lblRefNo"
        Me.lblRefNo.Size = New System.Drawing.Size(145, 16)
        Me.lblRefNo.TabIndex = 0
        Me.lblRefNo.Text = "HKID/Passport/Ref. No"
        '
        'txtHKID
        '
        Me.txtHKID.Location = New System.Drawing.Point(180, 30)
        Me.txtHKID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtHKID.MaxLength = 500
        Me.txtHKID.Name = "txtHKID"
        Me.txtHKID.Size = New System.Drawing.Size(172, 22)
        Me.txtHKID.TabIndex = 0
        '
        'btnGotoResetPassword
        '
        Me.btnGotoResetPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGotoResetPassword.Location = New System.Drawing.Point(607, 386)
        Me.btnGotoResetPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGotoResetPassword.Name = "btnGotoResetPassword"
        Me.btnGotoResetPassword.Size = New System.Drawing.Size(192, 28)
        Me.btnGotoResetPassword.TabIndex = 6
        Me.btnGotoResetPassword.Text = "&Proceed to reset password"
        Me.btnGotoResetPassword.UseVisualStyleBackColor = True
        '
        'btnBack4
        '
        Me.btnBack4.Location = New System.Drawing.Point(575, 171)
        Me.btnBack4.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBack4.Name = "btnBack4"
        Me.btnBack4.Size = New System.Drawing.Size(100, 28)
        Me.btnBack4.TabIndex = 14
        Me.btnBack4.Text = "<< &Back"
        Me.btnBack4.UseVisualStyleBackColor = True
        '
        'btnClear4
        '
        Me.btnClear4.Location = New System.Drawing.Point(455, 171)
        Me.btnClear4.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear4.Name = "btnClear4"
        Me.btnClear4.Size = New System.Drawing.Size(100, 28)
        Me.btnClear4.TabIndex = 13
        Me.btnClear4.Text = "&Clear"
        Me.btnClear4.UseVisualStyleBackColor = True
        '
        'btnChangeUsername
        '
        Me.btnChangeUsername.Location = New System.Drawing.Point(196, 171)
        Me.btnChangeUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.btnChangeUsername.Name = "btnChangeUsername"
        Me.btnChangeUsername.Size = New System.Drawing.Size(231, 28)
        Me.btnChangeUsername.TabIndex = 12
        Me.btnChangeUsername.Text = "Change &Username"
        Me.btnChangeUsername.UseVisualStyleBackColor = True
        '
        'txtNewUsername
        '
        Me.txtNewUsername.Location = New System.Drawing.Point(209, 106)
        Me.txtNewUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNewUsername.Name = "txtNewUsername"
        Me.txtNewUsername.Size = New System.Drawing.Size(464, 22)
        Me.txtNewUsername.TabIndex = 11
        '
        'lblOldUsername
        '
        Me.lblOldUsername.AutoSize = True
        Me.lblOldUsername.Location = New System.Drawing.Point(89, 65)
        Me.lblOldUsername.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOldUsername.Name = "lblOldUsername"
        Me.lblOldUsername.Size = New System.Drawing.Size(94, 16)
        Me.lblOldUsername.TabIndex = 8
        Me.lblOldUsername.Text = "Old Username"
        '
        'txtOldUsername
        '
        Me.txtOldUsername.BackColor = System.Drawing.SystemColors.Window
        Me.txtOldUsername.Location = New System.Drawing.Point(209, 65)
        Me.txtOldUsername.Margin = New System.Windows.Forms.Padding(4)
        Me.txtOldUsername.Name = "txtOldUsername"
        Me.txtOldUsername.Size = New System.Drawing.Size(464, 22)
        Me.txtOldUsername.TabIndex = 9
        '
        'lblNewUsername
        '
        Me.lblNewUsername.AutoSize = True
        Me.lblNewUsername.Location = New System.Drawing.Point(89, 110)
        Me.lblNewUsername.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNewUsername.Name = "lblNewUsername"
        Me.lblNewUsername.Size = New System.Drawing.Size(100, 16)
        Me.lblNewUsername.TabIndex = 15
        Me.lblNewUsername.Text = "New Username"
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(575, 171)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(100, 28)
        Me.btnBack.TabIndex = 14
        Me.btnBack.Text = "<< &Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(128, 106)
        Me.lblEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(41, 16)
        Me.lblEmail.TabIndex = 10
        Me.lblEmail.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(209, 106)
        Me.txtEmail.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(464, 22)
        Me.txtEmail.TabIndex = 11
        '
        'lblUsername2
        '
        Me.lblUsername2.AutoSize = True
        Me.lblUsername2.Location = New System.Drawing.Point(128, 65)
        Me.lblUsername2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUsername2.Name = "lblUsername2"
        Me.lblUsername2.Size = New System.Drawing.Size(70, 16)
        Me.lblUsername2.TabIndex = 8
        Me.lblUsername2.Text = "Username"
        '
        'txtUsername2
        '
        Me.txtUsername2.BackColor = System.Drawing.SystemColors.Info
        Me.txtUsername2.Location = New System.Drawing.Point(209, 65)
        Me.txtUsername2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsername2.Name = "txtUsername2"
        Me.txtUsername2.ReadOnly = True
        Me.txtUsername2.Size = New System.Drawing.Size(464, 22)
        Me.txtUsername2.TabIndex = 9
        '
        'btnResetPassword
        '
        Me.btnResetPassword.Location = New System.Drawing.Point(132, 171)
        Me.btnResetPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.btnResetPassword.Name = "btnResetPassword"
        Me.btnResetPassword.Size = New System.Drawing.Size(299, 28)
        Me.btnResetPassword.TabIndex = 12
        Me.btnResetPassword.Text = "&Reset password and send email"
        Me.btnResetPassword.UseVisualStyleBackColor = True
        '
        'btnClearInput
        '
        Me.btnClearInput.Location = New System.Drawing.Point(455, 171)
        Me.btnClearInput.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearInput.Name = "btnClearInput"
        Me.btnClearInput.Size = New System.Drawing.Size(100, 28)
        Me.btnClearInput.TabIndex = 13
        Me.btnClearInput.Text = "&Clear"
        Me.btnClearInput.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblMessage.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblMessage.Location = New System.Drawing.Point(0, 436)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(955, 18)
        Me.lblMessage.TabIndex = 2
        '
        'pnlLinkCustomerId
        '
        Me.pnlLinkCustomerId.Controls.Add(Me.pnlUpdateEmail)
        Me.pnlLinkCustomerId.Controls.Add(Me.txtUsername3)
        Me.pnlLinkCustomerId.Controls.Add(Me.btnBack3)
        Me.pnlLinkCustomerId.Controls.Add(Me.btnLinkCustomerId)
        Me.pnlLinkCustomerId.Controls.Add(Me.btnClearInput3)
        Me.pnlLinkCustomerId.Controls.Add(Me.txtCustomerId3)
        Me.pnlLinkCustomerId.Controls.Add(Me.lblCustomerId3)
        Me.pnlLinkCustomerId.Controls.Add(Me.cbxCustomerId)
        Me.pnlLinkCustomerId.Controls.Add(Me.lblCustomerId3b)
        Me.pnlLinkCustomerId.Controls.Add(Me.lblUsername3)
        Me.pnlLinkCustomerId.Controls.Add(Me.LCustCom)
        Me.pnlLinkCustomerId.Controls.Add(Me.CBCustCom)
        Me.pnlLinkCustomerId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLinkCustomerId.Location = New System.Drawing.Point(0, 0)
        Me.pnlLinkCustomerId.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlLinkCustomerId.Name = "pnlLinkCustomerId"
        Me.pnlLinkCustomerId.Size = New System.Drawing.Size(955, 454)
        Me.pnlLinkCustomerId.TabIndex = 4
        '
        'txtUsername3
        '
        Me.txtUsername3.BackColor = System.Drawing.SystemColors.Window
        Me.txtUsername3.Location = New System.Drawing.Point(257, 65)
        Me.txtUsername3.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsername3.Name = "txtUsername3"
        Me.txtUsername3.Size = New System.Drawing.Size(261, 22)
        Me.txtUsername3.TabIndex = 9
        '
        'btnBack3
        '
        Me.btnBack3.Location = New System.Drawing.Point(728, 154)
        Me.btnBack3.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBack3.Name = "btnBack3"
        Me.btnBack3.Size = New System.Drawing.Size(100, 28)
        Me.btnBack3.TabIndex = 14
        Me.btnBack3.Text = "<< &Back"
        Me.btnBack3.UseVisualStyleBackColor = True
        '
        'btnLinkCustomerId
        '
        Me.btnLinkCustomerId.Location = New System.Drawing.Point(364, 154)
        Me.btnLinkCustomerId.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLinkCustomerId.Name = "btnLinkCustomerId"
        Me.btnLinkCustomerId.Size = New System.Drawing.Size(231, 28)
        Me.btnLinkCustomerId.TabIndex = 12
        Me.btnLinkCustomerId.Text = "&Link Customer ID"
        Me.btnLinkCustomerId.UseVisualStyleBackColor = True
        '
        'btnClearInput3
        '
        Me.btnClearInput3.Location = New System.Drawing.Point(615, 154)
        Me.btnClearInput3.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearInput3.Name = "btnClearInput3"
        Me.btnClearInput3.Size = New System.Drawing.Size(100, 28)
        Me.btnClearInput3.TabIndex = 13
        Me.btnClearInput3.Text = "&Clear"
        Me.btnClearInput3.UseVisualStyleBackColor = True
        '
        'txtCustomerId3
        '
        Me.txtCustomerId3.Location = New System.Drawing.Point(257, 106)
        Me.txtCustomerId3.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerId3.Name = "txtCustomerId3"
        Me.txtCustomerId3.Size = New System.Drawing.Size(261, 22)
        Me.txtCustomerId3.TabIndex = 11
        Me.txtCustomerId3.Visible = False
        '
        'lblCustomerId3
        '
        Me.lblCustomerId3.AutoSize = True
        Me.lblCustomerId3.Location = New System.Drawing.Point(115, 114)
        Me.lblCustomerId3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerId3.Name = "lblCustomerId3"
        Me.lblCustomerId3.Size = New System.Drawing.Size(115, 16)
        Me.lblCustomerId3.TabIndex = 10
        Me.lblCustomerId3.Text = "Other Customer ID"
        Me.lblCustomerId3.Visible = False
        '
        'cbxCustomerId
        '
        Me.cbxCustomerId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxCustomerId.FormattingEnabled = True
        Me.cbxCustomerId.Location = New System.Drawing.Point(682, 66)
        Me.cbxCustomerId.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCustomerId.Name = "cbxCustomerId"
        Me.cbxCustomerId.Size = New System.Drawing.Size(184, 24)
        Me.cbxCustomerId.TabIndex = 15
        '
        'lblCustomerId3b
        '
        Me.lblCustomerId3b.AutoSize = True
        Me.lblCustomerId3b.Location = New System.Drawing.Point(548, 69)
        Me.lblCustomerId3b.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerId3b.Name = "lblCustomerId3b"
        Me.lblCustomerId3b.Size = New System.Drawing.Size(80, 16)
        Me.lblCustomerId3b.TabIndex = 16
        Me.lblCustomerId3b.Text = "Customer ID"
        '
        'lblUsername3
        '
        Me.lblUsername3.AutoSize = True
        Me.lblUsername3.Location = New System.Drawing.Point(167, 65)
        Me.lblUsername3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUsername3.Name = "lblUsername3"
        Me.lblUsername3.Size = New System.Drawing.Size(70, 16)
        Me.lblUsername3.TabIndex = 8
        Me.lblUsername3.Text = "Username"
        '
        'LCustCom
        '
        Me.LCustCom.AutoSize = True
        Me.LCustCom.Location = New System.Drawing.Point(548, 109)
        Me.LCustCom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LCustCom.Name = "LCustCom"
        Me.LCustCom.Size = New System.Drawing.Size(125, 16)
        Me.LCustCom.TabIndex = 18
        Me.LCustCom.Text = "Customer Company"
        '
        'CBCustCom
        '
        Me.CBCustCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBCustCom.FormattingEnabled = True
        Me.CBCustCom.Items.AddRange(New Object() {"Assurance", "Bermuda"})
        Me.CBCustCom.Location = New System.Drawing.Point(681, 106)
        Me.CBCustCom.Margin = New System.Windows.Forms.Padding(4)
        Me.CBCustCom.Name = "CBCustCom"
        Me.CBCustCom.Size = New System.Drawing.Size(184, 24)
        Me.CBCustCom.TabIndex = 17
        Me.CBCustCom.SelectedIndex = 0
        '
        'frmFindUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 454)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pnlLinkCustomerId)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmFindUser"
        Me.Text = "Find Username"
        Me.pnlUpdateEmail.ResumeLayout(False)
        Me.pnlUpdateEmail.PerformLayout()
        Me.pnlChangeUsername.ResumeLayout(False)
        Me.pnlChangeUsername.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLinkCustomerId.ResumeLayout(False)
        Me.pnlLinkCustomerId.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlUpdateEmail As System.Windows.Forms.Panel
    Friend WithEvents btnClearInput As System.Windows.Forms.Button
    Friend WithEvents btnResetPassword As System.Windows.Forms.Button
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblUsername2 As System.Windows.Forms.Label
    Friend WithEvents txtUsername2 As System.Windows.Forms.TextBox
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lblCustomerID As System.Windows.Forms.Label
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblRefNo As System.Windows.Forms.Label
    Friend WithEvents txtHKID As System.Windows.Forms.TextBox
    Friend WithEvents btnGotoResetPassword As System.Windows.Forms.Button
    Friend WithEvents pnlLinkCustomerId As System.Windows.Forms.Panel
    Friend WithEvents btnBack3 As System.Windows.Forms.Button
    Friend WithEvents btnClearInput3 As System.Windows.Forms.Button
    Friend WithEvents btnLinkCustomerId As System.Windows.Forms.Button
    Friend WithEvents lblCustomerId3 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerId3 As System.Windows.Forms.TextBox
    Friend WithEvents lblUsername3 As System.Windows.Forms.Label
    Friend WithEvents txtUsername3 As System.Windows.Forms.TextBox
    Friend WithEvents btnGoLinkCustomerId As System.Windows.Forms.Button
    Friend WithEvents pnlChangeUsername As System.Windows.Forms.Panel
    Friend WithEvents lblNewUsername As System.Windows.Forms.Label
    Friend WithEvents btnBack4 As System.Windows.Forms.Button
    Friend WithEvents btnClear4 As System.Windows.Forms.Button
    Friend WithEvents btnChangeUsername As System.Windows.Forms.Button
    Friend WithEvents txtNewUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblOldUsername As System.Windows.Forms.Label
    Friend WithEvents txtOldUsername As System.Windows.Forms.TextBox
    Friend WithEvents btnGoChangeUsername As System.Windows.Forms.Button
    Friend WithEvents lblCustomerId3b As System.Windows.Forms.Label
    Friend WithEvents cbxCustomerId As System.Windows.Forms.ComboBox
    Friend WithEvents CBCustCom As ComboBox
    Friend WithEvents LCustCom As Label
End Class
