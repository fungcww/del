<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCrmOptInOut
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
        Me.lblEngName = New System.Windows.Forms.Label()
        Me.txtEngName = New System.Windows.Forms.TextBox()
        Me.txtHkid = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtUpdUsr = New System.Windows.Forms.TextBox()
        Me.lblHkid = New System.Windows.Forms.Label()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.lblUpdUsr = New System.Windows.Forms.Label()
        Me.cbCom = New System.Windows.Forms.ComboBox()
        Me.lblComp = New System.Windows.Forms.Label()
        Me.lblChiName = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblProspect = New System.Windows.Forms.Label()
        Me.lblCustId = New System.Windows.Forms.Label()
        Me.cbProspect = New System.Windows.Forms.ComboBox()
        Me.txtChiName = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtCustId = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dgvCust = New System.Windows.Forms.DataGridView()
        Me.UclCrmOptInOutDtl1 = New CS2005.uclCrmOptInOutDtl()
        CType(Me.dgvCust, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblEngName
        '
        Me.lblEngName.AutoSize = True
        Me.lblEngName.Location = New System.Drawing.Point(12, 15)
        Me.lblEngName.Name = "lblEngName"
        Me.lblEngName.Size = New System.Drawing.Size(72, 13)
        Me.lblEngName.TabIndex = 0
        Me.lblEngName.Text = "English Name"
        '
        'txtEngName
        '
        Me.txtEngName.Location = New System.Drawing.Point(114, 12)
        Me.txtEngName.Name = "txtEngName"
        Me.txtEngName.Size = New System.Drawing.Size(203, 20)
        Me.txtEngName.TabIndex = 1
        '
        'txtHkid
        '
        Me.txtHkid.Location = New System.Drawing.Point(114, 38)
        Me.txtHkid.Name = "txtHkid"
        Me.txtHkid.Size = New System.Drawing.Size(203, 20)
        Me.txtHkid.TabIndex = 2
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(114, 64)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(203, 20)
        Me.txtPhone.TabIndex = 3
        '
        'txtUpdUsr
        '
        Me.txtUpdUsr.Location = New System.Drawing.Point(114, 90)
        Me.txtUpdUsr.Name = "txtUpdUsr"
        Me.txtUpdUsr.Size = New System.Drawing.Size(203, 20)
        Me.txtUpdUsr.TabIndex = 4
        '
        'lblHkid
        '
        Me.lblHkid.AutoSize = True
        Me.lblHkid.Location = New System.Drawing.Point(12, 41)
        Me.lblHkid.Name = "lblHkid"
        Me.lblHkid.Size = New System.Drawing.Size(96, 13)
        Me.lblHkid.TabIndex = 5
        Me.lblHkid.Text = "HKID/Passport No"
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Location = New System.Drawing.Point(12, 67)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(38, 13)
        Me.lblPhone.TabIndex = 6
        Me.lblPhone.Text = "Phone"
        '
        'lblUpdUsr
        '
        Me.lblUpdUsr.AutoSize = True
        Me.lblUpdUsr.Location = New System.Drawing.Point(12, 93)
        Me.lblUpdUsr.Name = "lblUpdUsr"
        Me.lblUpdUsr.Size = New System.Drawing.Size(65, 13)
        Me.lblUpdUsr.TabIndex = 7
        Me.lblUpdUsr.Text = "Update user"
        '
        'cbCom
        '
        Me.cbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCom.FormattingEnabled = True
        Me.cbCom.Items.AddRange(New Object() {"All", "Assurance", "Bermuda"})
        Me.cbCom.Location = New System.Drawing.Point(114, 116)
        Me.cbCom.Name = "cbCom"
        Me.cbCom.Size = New System.Drawing.Size(203, 21)
        Me.cbCom.TabIndex = 8
        '
        'lblComp
        '
        Me.lblComp.AutoSize = True
        Me.lblComp.Location = New System.Drawing.Point(12, 119)
        Me.lblComp.Name = "lblComp"
        Me.lblComp.Size = New System.Drawing.Size(51, 13)
        Me.lblComp.TabIndex = 9
        Me.lblComp.Text = "Company"
        '
        'lblChiName
        '
        Me.lblChiName.AutoSize = True
        Me.lblChiName.Location = New System.Drawing.Point(323, 15)
        Me.lblChiName.Name = "lblChiName"
        Me.lblChiName.Size = New System.Drawing.Size(76, 13)
        Me.lblChiName.TabIndex = 10
        Me.lblChiName.Text = "Chinese Name"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(323, 41)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(32, 13)
        Me.lblEmail.TabIndex = 11
        Me.lblEmail.Text = "Email"
        '
        'lblProspect
        '
        Me.lblProspect.AutoSize = True
        Me.lblProspect.Location = New System.Drawing.Point(323, 67)
        Me.lblProspect.Name = "lblProspect"
        Me.lblProspect.Size = New System.Drawing.Size(86, 13)
        Me.lblProspect.TabIndex = 12
        Me.lblProspect.Text = "Prospect Source"
        '
        'lblCustId
        '
        Me.lblCustId.AutoSize = True
        Me.lblCustId.Location = New System.Drawing.Point(323, 93)
        Me.lblCustId.Name = "lblCustId"
        Me.lblCustId.Size = New System.Drawing.Size(65, 13)
        Me.lblCustId.TabIndex = 13
        Me.lblCustId.Text = "Customer ID"
        '
        'cbProspect
        '
        Me.cbProspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProspect.FormattingEnabled = True
        Me.cbProspect.Location = New System.Drawing.Point(415, 64)
        Me.cbProspect.Name = "cbProspect"
        Me.cbProspect.Size = New System.Drawing.Size(203, 21)
        Me.cbProspect.TabIndex = 14
        '
        'txtChiName
        '
        Me.txtChiName.Location = New System.Drawing.Point(415, 12)
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.Size = New System.Drawing.Size(203, 20)
        Me.txtChiName.TabIndex = 15
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(415, 38)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(203, 20)
        Me.txtEmail.TabIndex = 16
        '
        'txtCustId
        '
        Me.txtCustId.Location = New System.Drawing.Point(415, 90)
        Me.txtCustId.Name = "txtCustId"
        Me.txtCustId.Size = New System.Drawing.Size(203, 20)
        Me.txtCustId.TabIndex = 17
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(624, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 18
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'dgvCust
        '
        Me.dgvCust.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCust.Location = New System.Drawing.Point(15, 143)
        Me.dgvCust.Name = "dgvCust"
        Me.dgvCust.Size = New System.Drawing.Size(684, 116)
        Me.dgvCust.TabIndex = 19
        '
        'UclCrmOptInOutDtl1
        '
        Me.UclCrmOptInOutDtl1.Location = New System.Drawing.Point(-3, 265)
        Me.UclCrmOptInOutDtl1.Name = "UclCrmOptInOutDtl1"
        Me.UclCrmOptInOutDtl1.Size = New System.Drawing.Size(889, 625)
        Me.UclCrmOptInOutDtl1.TabIndex = 20
        Me.UclCrmOptInOutDtl1.Visible = False
        '
        'frmCrmOptInOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 633)
        Me.Controls.Add(Me.UclCrmOptInOutDtl1)
        Me.Controls.Add(Me.dgvCust)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtCustId)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtChiName)
        Me.Controls.Add(Me.cbProspect)
        Me.Controls.Add(Me.lblCustId)
        Me.Controls.Add(Me.lblProspect)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblChiName)
        Me.Controls.Add(Me.lblComp)
        Me.Controls.Add(Me.cbCom)
        Me.Controls.Add(Me.lblUpdUsr)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.lblHkid)
        Me.Controls.Add(Me.txtUpdUsr)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtHkid)
        Me.Controls.Add(Me.txtEngName)
        Me.Controls.Add(Me.lblEngName)
        Me.Name = "frmCrmOptInOut"
        Me.Text = "CRM Opt-in/out"
        CType(Me.dgvCust, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblEngName As Label
    Friend WithEvents txtEngName As TextBox
    Friend WithEvents txtHkid As TextBox
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents txtUpdUsr As TextBox
    Friend WithEvents lblHkid As Label
    Friend WithEvents lblPhone As Label
    Friend WithEvents lblUpdUsr As Label
    Friend WithEvents cbCom As ComboBox
    Friend WithEvents lblComp As Label
    Friend WithEvents lblChiName As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents lblProspect As Label
    Friend WithEvents lblCustId As Label
    Friend WithEvents cbProspect As ComboBox
    Friend WithEvents txtChiName As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtCustId As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents dgvCust As DataGridView
    Friend WithEvents UclCrmOptInOutDtl1 As uclCrmOptInOutDtl
End Class
