<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class uclParSur
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uclParSur))
        Dim PolicyData1 As POSCommCtrl.Ctrl_POS_Scrn_Head.PolicyData = New POSCommCtrl.Ctrl_POS_Scrn_Head.PolicyData()
        Me.Ctrl_POS_Scrn_Head = New POSCommCtrl.Ctrl_POS_Scrn_Head()
        Me.Ctrl_POS_Fund_SWOut = New POSCommCtrl.Ctrl_POS_Fund_SWOut()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RB_NA = New System.Windows.Forms.RadioButton()
        Me.RB_F = New System.Windows.Forms.RadioButton()
        Me.RB_P = New System.Windows.Forms.RadioButton()
        Me.btn_Quote = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dtpEffDate = New System.Windows.Forms.DateTimePicker()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Ctrl_POS_Scrn_Head
        '
        Me.Ctrl_POS_Scrn_Head.iInsuredCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head.iOwnerCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head.Location = New System.Drawing.Point(0, 0)
        Me.Ctrl_POS_Scrn_Head.ModeInuse = "--"
        Me.Ctrl_POS_Scrn_Head.Name = "Ctrl_POS_Scrn_Head"
        Me.Ctrl_POS_Scrn_Head.PolicyDataInuse = PolicyData1
        Me.Ctrl_POS_Scrn_Head.policyInuse = ""
        Me.Ctrl_POS_Scrn_Head.PTDInuse = "--"
        Me.Ctrl_POS_Scrn_Head.RCDInuse = "--"
        Me.Ctrl_POS_Scrn_Head.sCIWInsuredEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head.sCIWOwnerChnNameInuse = ""
        Me.Ctrl_POS_Scrn_Head.sCIWOwnerEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head.Size = New System.Drawing.Size(1023, 122)
        Me.Ctrl_POS_Scrn_Head.sOwnerUseChiIndInuse = "N"
        Me.Ctrl_POS_Scrn_Head.SystemInuse = ""
        Me.Ctrl_POS_Scrn_Head.TabIndex = 0
        '
        'Ctrl_POS_Fund_SWOut
        '
        Me.Ctrl_POS_Fund_SWOut.FundListdsInuse = Nothing
        Me.Ctrl_POS_Fund_SWOut.Interest = 0R
        Me.Ctrl_POS_Fund_SWOut.IsTransferByUnit = False
        Me.Ctrl_POS_Fund_SWOut.Location = New System.Drawing.Point(17, 157)
        Me.Ctrl_POS_Fund_SWOut.modeinuse = POSCommCtrl.Ctrl_POS_Fund_SWOut.ModeName.Able
        Me.Ctrl_POS_Fund_SWOut.Name = "Ctrl_POS_Fund_SWOut"
        Me.Ctrl_POS_Fund_SWOut.NewSI = 0R
        Me.Ctrl_POS_Fund_SWOut.policyInuse = ""
        Me.Ctrl_POS_Fund_SWOut.Principle = 0R
        Me.Ctrl_POS_Fund_SWOut.ProductTypeInuse = Nothing
        Me.Ctrl_POS_Fund_SWOut.Size = New System.Drawing.Size(728, 299)
        Me.Ctrl_POS_Fund_SWOut.SurCharge = 0R
        Me.Ctrl_POS_Fund_SWOut.TabIndex = 1
        Me.Ctrl_POS_Fund_SWOut.TotalCalmableAmt = 0R
        Me.Ctrl_POS_Fund_SWOut.TotalCalmAmt = 0R
        Me.Ctrl_POS_Fund_SWOut.TotalNewBal = 0R
        Me.Ctrl_POS_Fund_SWOut.TotalSurrAmt = 0R
        Me.Ctrl_POS_Fund_SWOut.WDMethod = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RB_NA)
        Me.GroupBox1.Controls.Add(Me.RB_F)
        Me.GroupBox1.Controls.Add(Me.RB_P)
        Me.GroupBox1.Location = New System.Drawing.Point(523, 114)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(222, 37)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Withdrawal Method"
        '
        'RB_NA
        '
        Me.RB_NA.AutoSize = True
        Me.RB_NA.Checked = True
        Me.RB_NA.Location = New System.Drawing.Point(11, 14)
        Me.RB_NA.Name = "RB_NA"
        Me.RB_NA.Size = New System.Drawing.Size(94, 17)
        Me.RB_NA.TabIndex = 2
        Me.RB_NA.TabStop = True
        Me.RB_NA.Text = "Not Applicable"
        Me.RB_NA.UseVisualStyleBackColor = True
        '
        'RB_F
        '
        Me.RB_F.AutoSize = True
        Me.RB_F.Location = New System.Drawing.Point(166, 14)
        Me.RB_F.Name = "RB_F"
        Me.RB_F.Size = New System.Drawing.Size(50, 17)
        Me.RB_F.TabIndex = 1
        Me.RB_F.Text = "Fixed"
        Me.RB_F.UseVisualStyleBackColor = True
        '
        'RB_P
        '
        Me.RB_P.AutoSize = True
        Me.RB_P.Location = New System.Drawing.Point(111, 14)
        Me.RB_P.Name = "RB_P"
        Me.RB_P.Size = New System.Drawing.Size(49, 17)
        Me.RB_P.TabIndex = 0
        Me.RB_P.Text = "Propl"
        Me.RB_P.UseVisualStyleBackColor = True
        '
        'btn_Quote
        '
        Me.btn_Quote.Location = New System.Drawing.Point(269, 462)
        Me.btn_Quote.Name = "btn_Quote"
        Me.btn_Quote.Size = New System.Drawing.Size(75, 23)
        Me.btn_Quote.TabIndex = 38
        Me.btn_Quote.Text = "Quote"
        Me.btn_Quote.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(80, 462)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 37
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dtpEffDate
        '
        Me.dtpEffDate.CustomFormat = "MMMMdd, yyyy"
        Me.dtpEffDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEffDate.Location = New System.Drawing.Point(92, 121)
        Me.dtpEffDate.Name = "dtpEffDate"
        Me.dtpEffDate.Size = New System.Drawing.Size(138, 20)
        Me.dtpEffDate.TabIndex = 36
        '
        'btnChange
        '
        Me.btnChange.Enabled = False
        Me.btnChange.Location = New System.Drawing.Point(175, 462)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(75, 23)
        Me.btnChange.TabIndex = 35
        Me.btnChange.Text = "Change"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Effective Date:"
        '
        'uclParSur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btn_Quote)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dtpEffDate)
        Me.Controls.Add(Me.btnChange)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Ctrl_POS_Fund_SWOut)
        Me.Controls.Add(Me.Ctrl_POS_Scrn_Head)
        Me.Name = "uclParSur"
        Me.Size = New System.Drawing.Size(1024, 508)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Ctrl_POS_Scrn_Head As POSCommCtrl.Ctrl_POS_Scrn_Head
    Friend WithEvents Ctrl_POS_Fund_SWOut As POSCommCtrl.Ctrl_POS_Fund_SWOut
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RB_NA As System.Windows.Forms.RadioButton
    Friend WithEvents RB_F As System.Windows.Forms.RadioButton
    Friend WithEvents RB_P As System.Windows.Forms.RadioButton
    Friend WithEvents btn_Quote As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dtpEffDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
