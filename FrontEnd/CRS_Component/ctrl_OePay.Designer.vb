<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_OePay
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrl_OePay))
        Me.Ctrl_POS_Scrn_Head1 = New POSCommCtrl.Ctrl_POS_Scrn_Head
        Me.dgvCSView = New System.Windows.Forms.DataGridView
        Me.StartDatePicker = New System.Windows.Forms.DateTimePicker
        Me.EndDatePicker = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SearchButton = New System.Windows.Forms.Button
        Me.policyCcyVal = New System.Windows.Forms.Label
        Me.policyBalVal = New System.Windows.Forms.Label
        Me.policyAsOfDateVal = New System.Windows.Forms.Label
        Me.lblPolicyBal = New System.Windows.Forms.Label
        Me.lblPolicyBalAsOfDate = New System.Windows.Forms.Label
        Me.ResetBtn = New System.Windows.Forms.Button
        CType(Me.dgvCSView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ctrl_POS_Scrn_Head1
        '
        Me.Ctrl_POS_Scrn_Head1.iInsuredCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_POS_Scrn_Head1.ModeInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.Name = "Ctrl_POS_Scrn_Head1"
        Me.Ctrl_POS_Scrn_Head1.policyInuse = ""
        Me.Ctrl_POS_Scrn_Head1.PTDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.RCDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.sCIWInsuredEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.sCIWOwnerChnNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.sCIWOwnerEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.Size = New System.Drawing.Size(1024, 97)
        Me.Ctrl_POS_Scrn_Head1.sOwnerUseChiIndInuse = "N"
        Me.Ctrl_POS_Scrn_Head1.TabIndex = 0
        '
        'dgvCSView
        '
        Me.dgvCSView.AllowUserToAddRows = False
        Me.dgvCSView.AllowUserToDeleteRows = False
        Me.dgvCSView.AllowUserToResizeColumns = False
        Me.dgvCSView.AllowUserToResizeRows = False
        Me.dgvCSView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCSView.Location = New System.Drawing.Point(12, 160)
        Me.dgvCSView.Name = "dgvCSView"
        Me.dgvCSView.ReadOnly = True
        Me.dgvCSView.Size = New System.Drawing.Size(1003, 224)
        Me.dgvCSView.TabIndex = 1
        '
        'StartDatePicker
        '
        Me.StartDatePicker.Location = New System.Drawing.Point(100, 116)
        Me.StartDatePicker.Name = "StartDatePicker"
        Me.StartDatePicker.Size = New System.Drawing.Size(200, 20)
        Me.StartDatePicker.TabIndex = 2
        '
        'EndDatePicker
        '
        Me.EndDatePicker.Location = New System.Drawing.Point(391, 117)
        Me.EndDatePicker.Name = "EndDatePicker"
        Me.EndDatePicker.Size = New System.Drawing.Size(200, 20)
        Me.EndDatePicker.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Start Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(333, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "End Date"
        '
        'SearchButton
        '
        Me.SearchButton.Location = New System.Drawing.Point(607, 112)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(75, 23)
        Me.SearchButton.TabIndex = 6
        Me.SearchButton.Text = "Search"
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'policyCcyVal
        '
        Me.policyCcyVal.AutoSize = True
        Me.policyCcyVal.ForeColor = System.Drawing.Color.Blue
        Me.policyCcyVal.Location = New System.Drawing.Point(884, 106)
        Me.policyCcyVal.Name = "policyCcyVal"
        Me.policyCcyVal.Size = New System.Drawing.Size(39, 13)
        Me.policyCcyVal.TabIndex = 7
        Me.policyCcyVal.Text = "Label3"
        Me.policyCcyVal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'policyBalVal
        '
        Me.policyBalVal.AutoSize = True
        Me.policyBalVal.ForeColor = System.Drawing.Color.Blue
        Me.policyBalVal.Location = New System.Drawing.Point(929, 106)
        Me.policyBalVal.Name = "policyBalVal"
        Me.policyBalVal.Size = New System.Drawing.Size(39, 13)
        Me.policyBalVal.TabIndex = 8
        Me.policyBalVal.Text = "Label4"
        '
        'policyAsOfDateVal
        '
        Me.policyAsOfDateVal.AutoSize = True
        Me.policyAsOfDateVal.ForeColor = System.Drawing.Color.Blue
        Me.policyAsOfDateVal.Location = New System.Drawing.Point(884, 124)
        Me.policyAsOfDateVal.Name = "policyAsOfDateVal"
        Me.policyAsOfDateVal.Size = New System.Drawing.Size(39, 13)
        Me.policyAsOfDateVal.TabIndex = 9
        Me.policyAsOfDateVal.Text = "Label5"
        '
        'lblPolicyBal
        '
        Me.lblPolicyBal.AutoSize = True
        Me.lblPolicyBal.Location = New System.Drawing.Point(801, 106)
        Me.lblPolicyBal.Name = "lblPolicyBal"
        Me.lblPolicyBal.Size = New System.Drawing.Size(77, 13)
        Me.lblPolicyBal.TabIndex = 10
        Me.lblPolicyBal.Text = "Policy Balance"
        '
        'lblPolicyBalAsOfDate
        '
        Me.lblPolicyBalAsOfDate.AutoSize = True
        Me.lblPolicyBalAsOfDate.Location = New System.Drawing.Point(804, 124)
        Me.lblPolicyBalAsOfDate.Name = "lblPolicyBalAsOfDate"
        Me.lblPolicyBalAsOfDate.Size = New System.Drawing.Size(57, 13)
        Me.lblPolicyBalAsOfDate.TabIndex = 11
        Me.lblPolicyBalAsOfDate.Text = "As of Date"
        '
        'ResetBtn
        '
        Me.ResetBtn.Location = New System.Drawing.Point(704, 112)
        Me.ResetBtn.Name = "ResetBtn"
        Me.ResetBtn.Size = New System.Drawing.Size(75, 23)
        Me.ResetBtn.TabIndex = 12
        Me.ResetBtn.Text = "Reset"
        Me.ResetBtn.UseVisualStyleBackColor = True
        '
        'ctrl_OePay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ResetBtn)
        Me.Controls.Add(Me.lblPolicyBalAsOfDate)
        Me.Controls.Add(Me.lblPolicyBal)
        Me.Controls.Add(Me.policyAsOfDateVal)
        Me.Controls.Add(Me.policyBalVal)
        Me.Controls.Add(Me.policyCcyVal)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.EndDatePicker)
        Me.Controls.Add(Me.StartDatePicker)
        Me.Controls.Add(Me.dgvCSView)
        Me.Controls.Add(Me.Ctrl_POS_Scrn_Head1)
        Me.Name = "ctrl_OePay"
        Me.Size = New System.Drawing.Size(1033, 725)
        CType(Me.dgvCSView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Ctrl_POS_Scrn_Head1 As POSCommCtrl.Ctrl_POS_Scrn_Head
    Friend WithEvents dgvCSView As System.Windows.Forms.DataGridView
    Friend WithEvents StartDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents EndDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents policyCcyVal As System.Windows.Forms.Label
    Friend WithEvents policyBalVal As System.Windows.Forms.Label
    Friend WithEvents policyAsOfDateVal As System.Windows.Forms.Label
    Friend WithEvents lblPolicyBal As System.Windows.Forms.Label
    Friend WithEvents lblPolicyBalAsOfDate As System.Windows.Forms.Label
    Friend WithEvents ResetBtn As System.Windows.Forms.Button

End Class
