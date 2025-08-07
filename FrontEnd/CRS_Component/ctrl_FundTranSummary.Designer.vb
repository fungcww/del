<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_FundTranSummary
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnSelectCov = New System.Windows.Forms.Button
        Me.Ctrl_Sel_CO_UTRS = New POSCommCtrl.Ctrl_Sel_CO
        Me.FundSummary1 = New ComCtl.FundSummary
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnSelectCov)
        Me.Panel1.Controls.Add(Me.Ctrl_Sel_CO_UTRS)
        Me.Panel1.Controls.Add(Me.FundSummary1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1035, 587)
        Me.Panel1.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(935, 61)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSelectCov
        '
        Me.btnSelectCov.Location = New System.Drawing.Point(935, 32)
        Me.btnSelectCov.Name = "btnSelectCov"
        Me.btnSelectCov.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectCov.TabIndex = 1
        Me.btnSelectCov.Text = "Select"
        Me.btnSelectCov.UseVisualStyleBackColor = True
        '
        'Ctrl_Sel_CO_UTRS
        '
        Me.Ctrl_Sel_CO_UTRS.BasicPlan = ""
        Me.Ctrl_Sel_CO_UTRS.ClientNameInuse = ""
        Me.Ctrl_Sel_CO_UTRS.ClientNoInuse = ""
        Me.Ctrl_Sel_CO_UTRS.CovCodeInuse = ""
        Me.Ctrl_Sel_CO_UTRS.CovDescInuse = ""
        Me.Ctrl_Sel_CO_UTRS.CovNoInuse = ""
        Me.Ctrl_Sel_CO_UTRS.currdsInuse = Nothing
        Me.Ctrl_Sel_CO_UTRS.LifeNoInUse = ""
        Me.Ctrl_Sel_CO_UTRS.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_Sel_CO_UTRS.Name = "Ctrl_Sel_CO_UTRS"
        Me.Ctrl_Sel_CO_UTRS.PolicyNoInuse = ""
        Me.Ctrl_Sel_CO_UTRS.PStsInuse = ""
        Me.Ctrl_Sel_CO_UTRS.RCDDateInUse = ""
        Me.Ctrl_Sel_CO_UTRS.RiderInuse = ""
        Me.Ctrl_Sel_CO_UTRS.RiskStsInuse = ""
        Me.Ctrl_Sel_CO_UTRS.Size = New System.Drawing.Size(1024, 102)
        Me.Ctrl_Sel_CO_UTRS.TabIndex = 2
        '
        'FundSummary1
        '
        Me.FundSummary1.AutoScroll = True
        Me.FundSummary1.AutoSize = True
        Me.FundSummary1.Coverage = Nothing
        Me.FundSummary1.CoverageCurrency = Nothing
        Me.FundSummary1.CoverageDT = Nothing
        Me.FundSummary1.EstimatedSurrenderAmount = Nothing
        Me.FundSummary1.FundIAAccountDT = Nothing
        Me.FundSummary1.Life = Nothing
        Me.FundSummary1.Location = New System.Drawing.Point(12, 111)
        Me.FundSummary1.Name = "FundSummary1"
        Me.FundSummary1.Rider = Nothing
        Me.FundSummary1.ShowAllFund = True
        Me.FundSummary1.Size = New System.Drawing.Size(998, 374)
        Me.FundSummary1.TabIndex = 0
        Me.FundSummary1.TotalInvestmentPreimumPaid = Nothing
        '
        'ctrl_FundTranSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ctrl_FundTranSummary"
        Me.Size = New System.Drawing.Size(1035, 587)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FundSummary1 As ComCtl.FundSummary
    Friend WithEvents btnSelectCov As System.Windows.Forms.Button
    Friend WithEvents Ctrl_Sel_CO_UTRS As POSCommCtrl.Ctrl_Sel_CO
    Friend WithEvents btnClear As System.Windows.Forms.Button

End Class
