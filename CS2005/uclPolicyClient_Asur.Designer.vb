<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uclPolicyClient_Asur
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.cboRelation = New System.Windows.Forms.ComboBox()
        Me.txtLAClientNo = New System.Windows.Forms.TextBox()
        Me.txtDOB = New System.Windows.Forms.TextBox()
        Me.txtSex = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtHKID = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtBeneRemark = New System.Windows.Forms.TextBox()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.txtCustomerNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGVRole = New System.Windows.Forms.DataGridView()
        Me.txtTrustee = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboAddrProof = New System.Windows.Forms.ComboBox()
        Me.lblAddrProof = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtOldCustNo = New System.Windows.Forms.TextBox()
        Me.txtSmokerFlag = New System.Windows.Forms.TextBox()
        Me.txtShare = New System.Windows.Forms.TextBox()
        Me.txtLAClientNo_Alt = New System.Windows.Forms.TextBox()
        Me.chkShare = New System.Windows.Forms.CheckBox()
        Me.txtLAClientNo_Client = New System.Windows.Forms.TextBox()
        Me.GrpBene = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboRole = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.DGVRole, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBene.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(649, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "DOB:"
        '
        'txtAddress
        '
        Me.txtAddress.Font = New System.Drawing.Font("PMingLiU", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(351, 195)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.Size = New System.Drawing.Size(407, 20)
        Me.txtAddress.TabIndex = 23
        Me.txtAddress.Text = "99999999"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(271, 198)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 13)
        Me.lblAddress.TabIndex = 22
        Me.lblAddress.Text = "Address:"
        '
        'cboRelation
        '
        Me.cboRelation.Enabled = False
        Me.cboRelation.FormattingEnabled = True
        Me.cboRelation.Location = New System.Drawing.Point(76, 15)
        Me.cboRelation.Name = "cboRelation"
        Me.cboRelation.Size = New System.Drawing.Size(177, 21)
        Me.cboRelation.TabIndex = 40
        '
        'txtLAClientNo
        '
        Me.txtLAClientNo.Location = New System.Drawing.Point(146, 251)
        Me.txtLAClientNo.Name = "txtLAClientNo"
        Me.txtLAClientNo.ReadOnly = True
        Me.txtLAClientNo.Size = New System.Drawing.Size(90, 20)
        Me.txtLAClientNo.TabIndex = 26
        Me.txtLAClientNo.Visible = False
        '
        'txtDOB
        '
        Me.txtDOB.Location = New System.Drawing.Point(688, 140)
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.ReadOnly = True
        Me.txtDOB.Size = New System.Drawing.Size(72, 20)
        Me.txtDOB.TabIndex = 25
        Me.txtDOB.Text = "01/01/1900"
        '
        'txtSex
        '
        Me.txtSex.Location = New System.Drawing.Point(624, 142)
        Me.txtSex.Name = "txtSex"
        Me.txtSex.ReadOnly = True
        Me.txtSex.Size = New System.Drawing.Size(19, 20)
        Me.txtSex.TabIndex = 21
        Me.txtSex.Text = "99999999"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(590, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Sex:"
        '
        'txtHKID
        '
        Me.txtHKID.Location = New System.Drawing.Point(517, 142)
        Me.txtHKID.Name = "txtHKID"
        Me.txtHKID.ReadOnly = True
        Me.txtHKID.Size = New System.Drawing.Size(67, 20)
        Me.txtHKID.TabIndex = 19
        Me.txtHKID.Text = "99999999"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(475, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "HKID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(271, 172)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Name:"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Font = New System.Drawing.Font("PMingLiU", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(351, 169)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(407, 21)
        Me.txtCustomerName.TabIndex = 17
        Me.txtCustomerName.Text = "99999999"
        '
        'txtBeneRemark
        '
        Me.txtBeneRemark.Enabled = False
        Me.txtBeneRemark.Location = New System.Drawing.Point(75, 68)
        Me.txtBeneRemark.Name = "txtBeneRemark"
        Me.txtBeneRemark.ReadOnly = True
        Me.txtBeneRemark.Size = New System.Drawing.Size(178, 20)
        Me.txtBeneRemark.TabIndex = 37
        '
        'cmdSelect
        '
        Me.cmdSelect.Enabled = False
        Me.cmdSelect.Location = New System.Drawing.Point(421, 140)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(48, 23)
        Me.cmdSelect.TabIndex = 16
        Me.cmdSelect.Text = "Select"
        Me.cmdSelect.UseVisualStyleBackColor = True
        '
        'txtCustomerNo
        '
        Me.txtCustomerNo.Location = New System.Drawing.Point(351, 143)
        Me.txtCustomerNo.Name = "txtCustomerNo"
        Me.txtCustomerNo.ReadOnly = True
        Me.txtCustomerNo.Size = New System.Drawing.Size(64, 20)
        Me.txtCustomerNo.TabIndex = 14
        Me.txtCustomerNo.Text = "99999999"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(271, 140)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 27)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Agent / Customer No.:"
        '
        'DGVRole
        '
        Me.DGVRole.AllowUserToAddRows = False
        Me.DGVRole.AllowUserToDeleteRows = False
        Me.DGVRole.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVRole.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGVRole.Location = New System.Drawing.Point(15, 19)
        Me.DGVRole.MultiSelect = False
        Me.DGVRole.Name = "DGVRole"
        Me.DGVRole.RowHeadersWidth = 36
        Me.DGVRole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVRole.Size = New System.Drawing.Size(743, 113)
        Me.DGVRole.TabIndex = 2
        '
        'txtTrustee
        '
        Me.txtTrustee.Enabled = False
        Me.txtTrustee.Location = New System.Drawing.Point(75, 42)
        Me.txtTrustee.Name = "txtTrustee"
        Me.txtTrustee.ReadOnly = True
        Me.txtTrustee.Size = New System.Drawing.Size(178, 20)
        Me.txtTrustee.TabIndex = 35
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(-56, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 13)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Life No"
        '
        'cboAddrProof
        '
        Me.cboAddrProof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAddrProof.Enabled = False
        Me.cboAddrProof.Font = New System.Drawing.Font("PMingLiU", 8.25!)
        Me.cboAddrProof.FormattingEnabled = True
        Me.cboAddrProof.Items.AddRange(New Object() {"", "N", "Y"})
        Me.cboAddrProof.Location = New System.Drawing.Point(717, 244)
        Me.cboAddrProof.Name = "cboAddrProof"
        Me.cboAddrProof.Size = New System.Drawing.Size(41, 19)
        Me.cboAddrProof.TabIndex = 41
        '
        'lblAddrProof
        '
        Me.lblAddrProof.AutoSize = True
        Me.lblAddrProof.Location = New System.Drawing.Point(655, 247)
        Me.lblAddrProof.Name = "lblAddrProof"
        Me.lblAddrProof.Size = New System.Drawing.Size(57, 13)
        Me.lblAddrProof.TabIndex = 40
        Me.lblAddrProof.Text = "Addr Proof"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(7, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 13)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Remark"
        '
        'txtOldCustNo
        '
        Me.txtOldCustNo.Location = New System.Drawing.Point(700, 218)
        Me.txtOldCustNo.Name = "txtOldCustNo"
        Me.txtOldCustNo.ReadOnly = True
        Me.txtOldCustNo.Size = New System.Drawing.Size(58, 20)
        Me.txtOldCustNo.TabIndex = 39
        Me.txtOldCustNo.Visible = False
        '
        'txtSmokerFlag
        '
        Me.txtSmokerFlag.Location = New System.Drawing.Point(593, 218)
        Me.txtSmokerFlag.Name = "txtSmokerFlag"
        Me.txtSmokerFlag.ReadOnly = True
        Me.txtSmokerFlag.Size = New System.Drawing.Size(100, 20)
        Me.txtSmokerFlag.TabIndex = 38
        Me.txtSmokerFlag.Visible = False
        '
        'txtShare
        '
        Me.txtShare.Location = New System.Drawing.Point(49, 16)
        Me.txtShare.Name = "txtShare"
        Me.txtShare.ReadOnly = True
        Me.txtShare.Size = New System.Drawing.Size(46, 20)
        Me.txtShare.TabIndex = 8
        '
        'txtLAClientNo_Alt
        '
        Me.txtLAClientNo_Alt.Location = New System.Drawing.Point(487, 237)
        Me.txtLAClientNo_Alt.Name = "txtLAClientNo_Alt"
        Me.txtLAClientNo_Alt.ReadOnly = True
        Me.txtLAClientNo_Alt.Size = New System.Drawing.Size(100, 20)
        Me.txtLAClientNo_Alt.TabIndex = 37
        Me.txtLAClientNo_Alt.Visible = False
        '
        'chkShare
        '
        Me.chkShare.AutoSize = True
        Me.chkShare.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkShare.Enabled = False
        Me.chkShare.Location = New System.Drawing.Point(101, 19)
        Me.chkShare.Name = "chkShare"
        Me.chkShare.Size = New System.Drawing.Size(84, 17)
        Me.chkShare.TabIndex = 9
        Me.chkShare.Text = "Same Share"
        Me.chkShare.UseVisualStyleBackColor = True
        '
        'txtLAClientNo_Client
        '
        Me.txtLAClientNo_Client.Location = New System.Drawing.Point(488, 218)
        Me.txtLAClientNo_Client.Name = "txtLAClientNo_Client"
        Me.txtLAClientNo_Client.ReadOnly = True
        Me.txtLAClientNo_Client.Size = New System.Drawing.Size(100, 20)
        Me.txtLAClientNo_Client.TabIndex = 36
        Me.txtLAClientNo_Client.Visible = False
        '
        'GrpBene
        '
        Me.GrpBene.Controls.Add(Me.cboRelation)
        Me.GrpBene.Controls.Add(Me.txtBeneRemark)
        Me.GrpBene.Controls.Add(Me.txtTrustee)
        Me.GrpBene.Controls.Add(Me.Label12)
        Me.GrpBene.Controls.Add(Me.Label11)
        Me.GrpBene.Controls.Add(Me.Label3)
        Me.GrpBene.Location = New System.Drawing.Point(8, 163)
        Me.GrpBene.Name = "GrpBene"
        Me.GrpBene.Size = New System.Drawing.Size(260, 102)
        Me.GrpBene.TabIndex = 35
        Me.GrpBene.TabStop = False
        Me.GrpBene.Text = "Beneficiary"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(7, 45)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(43, 13)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Trustee"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 21)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Relationship"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtShare)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.chkShare)
        Me.GroupBox2.Location = New System.Drawing.Point(274, 220)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(195, 45)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Agent/Beneficiary Share"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Share"
        '
        'cboRole
        '
        Me.cboRole.Enabled = False
        Me.cboRole.FormattingEnabled = True
        Me.cboRole.Location = New System.Drawing.Point(88, 138)
        Me.cboRole.Name = "cboRole"
        Me.cboRole.Size = New System.Drawing.Size(173, 21)
        Me.cboRole.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 138)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Role:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboAddrProof)
        Me.GroupBox1.Controls.Add(Me.lblAddrProof)
        Me.GroupBox1.Controls.Add(Me.txtOldCustNo)
        Me.GroupBox1.Controls.Add(Me.txtSmokerFlag)
        Me.GroupBox1.Controls.Add(Me.txtLAClientNo_Alt)
        Me.GroupBox1.Controls.Add(Me.txtLAClientNo_Client)
        Me.GroupBox1.Controls.Add(Me.GrpBene)
        Me.GroupBox1.Controls.Add(Me.txtLAClientNo)
        Me.GroupBox1.Controls.Add(Me.txtDOB)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.lblAddress)
        Me.GroupBox1.Controls.Add(Me.txtSex)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtHKID)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtCustomerName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmdSelect)
        Me.GroupBox1.Controls.Add(Me.txtCustomerNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboRole)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.DGVRole)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(779, 271)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Owner / Insured / Assignee / Beneficiary / Payor Role/ Agent Summary"
        '
        'uclPolicyClient_Asur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "uclPolicyClient_Asur"
        Me.Size = New System.Drawing.Size(787, 277)
        CType(Me.DGVRole, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBene.ResumeLayout(False)
        Me.GrpBene.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents cboRelation As System.Windows.Forms.ComboBox
    Friend WithEvents txtLAClientNo As System.Windows.Forms.TextBox
    Public WithEvents txtDOB As System.Windows.Forms.TextBox
    Public WithEvents txtSex As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtHKID As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtBeneRemark As System.Windows.Forms.TextBox
    Friend WithEvents cmdSelect As System.Windows.Forms.Button
    Public WithEvents txtCustomerNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents DGVRole As System.Windows.Forms.DataGridView
    Friend WithEvents txtTrustee As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboAddrProof As System.Windows.Forms.ComboBox
    Friend WithEvents lblAddrProof As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents txtOldCustNo As System.Windows.Forms.TextBox
    Public WithEvents txtSmokerFlag As System.Windows.Forms.TextBox
    Friend WithEvents txtShare As System.Windows.Forms.TextBox
    Public WithEvents txtLAClientNo_Alt As System.Windows.Forms.TextBox
    Friend WithEvents chkShare As System.Windows.Forms.CheckBox
    Public WithEvents txtLAClientNo_Client As System.Windows.Forms.TextBox
    Friend WithEvents GrpBene As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cboRole As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
