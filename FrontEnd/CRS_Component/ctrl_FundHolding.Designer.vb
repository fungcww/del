<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_FundHolding
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrl_FundHolding))
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpFrDate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSelect = New System.Windows.Forms.Button
        Me.Ctrl_Sel_CO = New POSCommCtrl.Ctrl_Sel_CO
        Me.FundHoldCom1 = New ComCtl.FundHoldCom
        Me.Ctrl_POS_Scrn_Head = New POSCommCtrl.Ctrl_POS_Scrn_Head
        Me.lblPolicy = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "MMM dd, yyyy"
        Me.dtpToDate.Location = New System.Drawing.Point(421, 106)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(187, 20)
        Me.dtpToDate.TabIndex = 38
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(356, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "To Date:"
        '
        'dtpFrDate
        '
        Me.dtpFrDate.CustomFormat = "MMM dd, yyyy"
        Me.dtpFrDate.Location = New System.Drawing.Point(91, 106)
        Me.dtpFrDate.Name = "dtpFrDate"
        Me.dtpFrDate.Size = New System.Drawing.Size(187, 20)
        Me.dtpFrDate.TabIndex = 36
        Me.dtpFrDate.Value = New Date(2008, 3, 17, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "From Date:"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(928, 176)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(65, 26)
        Me.btnSelect.TabIndex = 34
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'Ctrl_Sel_CO
        '
        Me.Ctrl_Sel_CO.BasicPlan = ""
        Me.Ctrl_Sel_CO.ClientNameInuse = ""
        Me.Ctrl_Sel_CO.ClientNoInuse = ""
        Me.Ctrl_Sel_CO.CovCodeInuse = ""
        Me.Ctrl_Sel_CO.CovDescInuse = ""
        Me.Ctrl_Sel_CO.CovNoInuse = ""
        Me.Ctrl_Sel_CO.currdsInuse = Nothing
        Me.Ctrl_Sel_CO.LifeNoInUse = ""
        Me.Ctrl_Sel_CO.Location = New System.Drawing.Point(11, 143)
        Me.Ctrl_Sel_CO.Name = "Ctrl_Sel_CO"
        Me.Ctrl_Sel_CO.PolicyNoInuse = ""
        Me.Ctrl_Sel_CO.PStsInuse = ""
        Me.Ctrl_Sel_CO.RiderInuse = ""
        Me.Ctrl_Sel_CO.RiskStsInuse = ""
        Me.Ctrl_Sel_CO.Size = New System.Drawing.Size(1024, 102)
        Me.Ctrl_Sel_CO.TabIndex = 33
        '
        'FundHoldCom1
        '
        Me.FundHoldCom1.coveragenoinuse = Nothing
        Me.FundHoldCom1.DetailDS = Nothing
        Me.FundHoldCom1.HeaderDS = Nothing
        Me.FundHoldCom1.lifenoinuse = Nothing
        Me.FundHoldCom1.Location = New System.Drawing.Point(11, 279)
        Me.FundHoldCom1.modeinuse = Utility.Utility.ModeName.Add
        Me.FundHoldCom1.Name = "FundHoldCom1"
        Me.FundHoldCom1.Size = New System.Drawing.Size(924, 284)
        Me.FundHoldCom1.TabIndex = 32
        '
        'Ctrl_POS_Scrn_Head
        '
        Me.Ctrl_POS_Scrn_Head.Location = New System.Drawing.Point(0, 3)
        Me.Ctrl_POS_Scrn_Head.ModeInuse = "--"
        Me.Ctrl_POS_Scrn_Head.Name = "Ctrl_POS_Scrn_Head"
        Me.Ctrl_POS_Scrn_Head.policyInuse = ""
        Me.Ctrl_POS_Scrn_Head.PTDInuse = "--"
        Me.Ctrl_POS_Scrn_Head.RCDInuse = "--"
        Me.Ctrl_POS_Scrn_Head.Size = New System.Drawing.Size(1024, 97)
        Me.Ctrl_POS_Scrn_Head.TabIndex = 31
        '
        'lblPolicy
        '
        Me.lblPolicy.AutoSize = True
        Me.lblPolicy.Location = New System.Drawing.Point(50, 32)
        Me.lblPolicy.Name = "lblPolicy"
        Me.lblPolicy.Size = New System.Drawing.Size(39, 13)
        Me.lblPolicy.TabIndex = 39
        Me.lblPolicy.Text = "Label3"
        '
        'ctrl_FundHolding
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblPolicy)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpFrDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.Ctrl_Sel_CO)
        Me.Controls.Add(Me.FundHoldCom1)
        Me.Controls.Add(Me.Ctrl_POS_Scrn_Head)
        Me.Name = "ctrl_FundHolding"
        Me.Size = New System.Drawing.Size(1035, 587)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFrDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents Ctrl_Sel_CO As POSCommCtrl.Ctrl_Sel_CO
    Friend WithEvents FundHoldCom1 As ComCtl.FundHoldCom
    Friend WithEvents Ctrl_POS_Scrn_Head As POSCommCtrl.Ctrl_POS_Scrn_Head
    Friend WithEvents lblPolicy As System.Windows.Forms.Label

End Class
