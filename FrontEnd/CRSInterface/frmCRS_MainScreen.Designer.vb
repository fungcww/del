<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCRS_MainScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCRS_MainScreen))
        Me.tcCRS = New System.Windows.Forms.TabControl
        Me.tabFinInfo = New System.Windows.Forms.TabPage
        Me.Ctrl_FinancialDtl1 = New CRS_Ctrl.ctrl_FinancialDtl
        Me.Ctrl_FinancialInfo1 = New CRS_Ctrl.ctrl_FinancialInfo
        Me.tabDirDebitEnq = New System.Windows.Forms.TabPage
        Me.Ctrl_DirectDebitEnq1 = New CRS_Ctrl.ctrl_DirectDebitEnq
        Me.tabTranHist = New System.Windows.Forms.TabPage
        Me.Ctrl_TranHist1 = New CRS_Ctrl.ctrl_TranHist
        Me.tabPolicyGenInfo = New System.Windows.Forms.TabPage
        Me.lblPolicyNo = New System.Windows.Forms.Label
        Me.Ctrl_CRSPolicyGeneral_Information1 = New CRS_Ctrl.ctrl_CRSPolicyGeneral_Information
        Me.tabPaymentHist = New System.Windows.Forms.TabPage
        Me.Ctrl_BillingInf1 = New POSCommCtrl.Ctrl_BillingInf
        Me.Ctrl_PaymentHist1 = New CRS_Ctrl.ctrl_PaymentHist
        Me.tabComponent = New System.Windows.Forms.TabPage
        Me.Ctrl_ChgComponent1 = New POSCommCtrl.Ctrl_ChgComponent
        Me.tcCRS.SuspendLayout()
        Me.tabFinInfo.SuspendLayout()
        Me.tabDirDebitEnq.SuspendLayout()
        Me.tabTranHist.SuspendLayout()
        Me.tabPolicyGenInfo.SuspendLayout()
        Me.tabPaymentHist.SuspendLayout()
        Me.tabComponent.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcCRS
        '
        Me.tcCRS.Controls.Add(Me.tabFinInfo)
        Me.tcCRS.Controls.Add(Me.tabDirDebitEnq)
        Me.tcCRS.Controls.Add(Me.tabTranHist)
        Me.tcCRS.Controls.Add(Me.tabPolicyGenInfo)
        Me.tcCRS.Controls.Add(Me.tabPaymentHist)
        Me.tcCRS.Controls.Add(Me.tabComponent)
        Me.tcCRS.Location = New System.Drawing.Point(2, -1)
        Me.tcCRS.Name = "tcCRS"
        Me.tcCRS.SelectedIndex = 0
        Me.tcCRS.Size = New System.Drawing.Size(1128, 776)
        Me.tcCRS.TabIndex = 0
        '
        'tabFinInfo
        '
        Me.tabFinInfo.Controls.Add(Me.Ctrl_FinancialDtl1)
        Me.tabFinInfo.Controls.Add(Me.Ctrl_FinancialInfo1)
        Me.tabFinInfo.Location = New System.Drawing.Point(4, 22)
        Me.tabFinInfo.Name = "tabFinInfo"
        Me.tabFinInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFinInfo.Size = New System.Drawing.Size(1120, 750)
        Me.tabFinInfo.TabIndex = 0
        Me.tabFinInfo.Text = "Financial Information"
        Me.tabFinInfo.UseVisualStyleBackColor = True
        '
        'Ctrl_FinancialDtl1
        '
        Me.Ctrl_FinancialDtl1.Location = New System.Drawing.Point(19, 491)
        Me.Ctrl_FinancialDtl1.modeinuse = CRS_Ctrl.ctrl_FinancialDtl.ModeName.Enquiry
        Me.Ctrl_FinancialDtl1.Name = "Ctrl_FinancialDtl1"
        Me.Ctrl_FinancialDtl1.Size = New System.Drawing.Size(920, 255)
        Me.Ctrl_FinancialDtl1.TabIndex = 4
        '
        'Ctrl_FinancialInfo1
        '
        Me.Ctrl_FinancialInfo1.Location = New System.Drawing.Point(6, 6)
        Me.Ctrl_FinancialInfo1.Name = "Ctrl_FinancialInfo1"
        Me.Ctrl_FinancialInfo1.PolicyNoInUse = ""
        Me.Ctrl_FinancialInfo1.Size = New System.Drawing.Size(779, 489)
        Me.Ctrl_FinancialInfo1.TabIndex = 2
        '
        'tabDirDebitEnq
        '
        Me.tabDirDebitEnq.Controls.Add(Me.Ctrl_DirectDebitEnq1)
        Me.tabDirDebitEnq.Location = New System.Drawing.Point(4, 22)
        Me.tabDirDebitEnq.Name = "tabDirDebitEnq"
        Me.tabDirDebitEnq.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDirDebitEnq.Size = New System.Drawing.Size(1120, 750)
        Me.tabDirDebitEnq.TabIndex = 1
        Me.tabDirDebitEnq.Text = "Direct Debit Enquiry"
        Me.tabDirDebitEnq.UseVisualStyleBackColor = True
        '
        'Ctrl_DirectDebitEnq1
        '
        Me.Ctrl_DirectDebitEnq1.ClientNoInUse = ""
        Me.Ctrl_DirectDebitEnq1.Location = New System.Drawing.Point(1, 6)
        Me.Ctrl_DirectDebitEnq1.Name = "Ctrl_DirectDebitEnq1"
        Me.Ctrl_DirectDebitEnq1.PolicyNoInUse = ""
        Me.Ctrl_DirectDebitEnq1.Size = New System.Drawing.Size(779, 509)
        Me.Ctrl_DirectDebitEnq1.TabIndex = 0
        '
        'tabTranHist
        '
        Me.tabTranHist.Controls.Add(Me.Ctrl_TranHist1)
        Me.tabTranHist.Location = New System.Drawing.Point(4, 22)
        Me.tabTranHist.Name = "tabTranHist"
        Me.tabTranHist.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTranHist.Size = New System.Drawing.Size(1120, 750)
        Me.tabTranHist.TabIndex = 2
        Me.tabTranHist.Text = "Transaction History"
        Me.tabTranHist.UseVisualStyleBackColor = True
        '
        'Ctrl_TranHist1
        '
        Me.Ctrl_TranHist1.Location = New System.Drawing.Point(2, 6)
        Me.Ctrl_TranHist1.Name = "Ctrl_TranHist1"
        Me.Ctrl_TranHist1.PolicyNoInUse = ""
        Me.Ctrl_TranHist1.Size = New System.Drawing.Size(779, 489)
        Me.Ctrl_TranHist1.TabIndex = 0
        '
        'tabPolicyGenInfo
        '
        Me.tabPolicyGenInfo.Controls.Add(Me.lblPolicyNo)
        Me.tabPolicyGenInfo.Controls.Add(Me.Ctrl_CRSPolicyGeneral_Information1)
        Me.tabPolicyGenInfo.Location = New System.Drawing.Point(4, 22)
        Me.tabPolicyGenInfo.Name = "tabPolicyGenInfo"
        Me.tabPolicyGenInfo.Size = New System.Drawing.Size(1120, 750)
        Me.tabPolicyGenInfo.TabIndex = 3
        Me.tabPolicyGenInfo.Text = "Policy General Inoformation"
        Me.tabPolicyGenInfo.UseVisualStyleBackColor = True
        '
        'lblPolicyNo
        '
        Me.lblPolicyNo.AutoSize = True
        Me.lblPolicyNo.Location = New System.Drawing.Point(50, 36)
        Me.lblPolicyNo.Name = "lblPolicyNo"
        Me.lblPolicyNo.Size = New System.Drawing.Size(62, 13)
        Me.lblPolicyNo.TabIndex = 1
        Me.lblPolicyNo.Text = "StrPolicyNo"
        '
        'Ctrl_CRSPolicyGeneral_Information1
        '
        Me.Ctrl_CRSPolicyGeneral_Information1.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_CRSPolicyGeneral_Information1.Name = "Ctrl_CRSPolicyGeneral_Information1"
        Me.Ctrl_CRSPolicyGeneral_Information1.PolicyNoInUse = ""
        Me.Ctrl_CRSPolicyGeneral_Information1.Size = New System.Drawing.Size(1053, 736)
        Me.Ctrl_CRSPolicyGeneral_Information1.TabIndex = 0
        '
        'tabPaymentHist
        '
        Me.tabPaymentHist.Controls.Add(Me.Ctrl_BillingInf1)
        Me.tabPaymentHist.Controls.Add(Me.Ctrl_PaymentHist1)
        Me.tabPaymentHist.Location = New System.Drawing.Point(4, 22)
        Me.tabPaymentHist.Name = "tabPaymentHist"
        Me.tabPaymentHist.Size = New System.Drawing.Size(1120, 750)
        Me.tabPaymentHist.TabIndex = 4
        Me.tabPaymentHist.Text = "Payment History"
        Me.tabPaymentHist.UseVisualStyleBackColor = True
        '
        'Ctrl_BillingInf1
        '
        Me.Ctrl_BillingInf1.BillTypeInUse = ""
        Me.Ctrl_BillingInf1.CurrdsInUse = Nothing
        Me.Ctrl_BillingInf1.EffDateInUse = New Date(CType(0, Long))
        Me.Ctrl_BillingInf1.Location = New System.Drawing.Point(6, 274)
        Me.Ctrl_BillingInf1.PModeInUse = POSCommCtrl.Ctrl_BillingInf.ModeName.Enquiry
        Me.Ctrl_BillingInf1.Name = "Ctrl_BillingInf1"
        Me.Ctrl_BillingInf1.PModeInUse = ""
        Me.Ctrl_BillingInf1.PolicyNoInUse = ""
        Me.Ctrl_BillingInf1.Size = New System.Drawing.Size(1024, 258)
        Me.Ctrl_BillingInf1.TabIndex = 1
        '
        'Ctrl_PaymentHist1
        '
        Me.Ctrl_PaymentHist1.Location = New System.Drawing.Point(6, 14)
        Me.Ctrl_PaymentHist1.Name = "Ctrl_PaymentHist1"
        Me.Ctrl_PaymentHist1.PolicyNoInUse = Nothing
        Me.Ctrl_PaymentHist1.Size = New System.Drawing.Size(872, 234)
        Me.Ctrl_PaymentHist1.TabIndex = 0
        '
        'tabComponent
        '
        Me.tabComponent.Controls.Add(Me.Ctrl_ChgComponent1)
        Me.tabComponent.Location = New System.Drawing.Point(4, 22)
        Me.tabComponent.Name = "tabComponent"
        Me.tabComponent.Size = New System.Drawing.Size(1120, 750)
        Me.tabComponent.TabIndex = 5
        Me.tabComponent.Text = "Component"
        Me.tabComponent.UseVisualStyleBackColor = True
        '
        'Ctrl_ChgComponent1
        '
        Me.Ctrl_ChgComponent1.BillTypeInUse = ""
        Me.Ctrl_ChgComponent1.CovNoInuse = ""
        Me.Ctrl_ChgComponent1.CurrdsInUse = Nothing
        Me.Ctrl_ChgComponent1.EffDateInUse = New Date(CType(0, Long))
        Me.Ctrl_ChgComponent1.LifeNoInUse = ""
        Me.Ctrl_ChgComponent1.Location = New System.Drawing.Point(6, 3)
        'Me.Ctrl_ChgComponent1.ModeInUse = Utility.Utility.ModeName.Change
        Me.Ctrl_ChgComponent1.Name = "Ctrl_ChgComponent1"
        Me.Ctrl_ChgComponent1.PModeInUse = ""
        Me.Ctrl_ChgComponent1.PolicyNoInuse = ""
        Me.Ctrl_ChgComponent1.RiderInuse = ""
        Me.Ctrl_ChgComponent1.Size = New System.Drawing.Size(967, 542)
        Me.Ctrl_ChgComponent1.TabIndex = 0
        '
        'frmCRS_MainScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1141, 780)
        Me.Controls.Add(Me.tcCRS)
        Me.Name = "frmCRS_MainScreen"
        Me.Text = "frmCRS_MainScreen"
        Me.tcCRS.ResumeLayout(False)
        Me.tabFinInfo.ResumeLayout(False)
        Me.tabDirDebitEnq.ResumeLayout(False)
        Me.tabTranHist.ResumeLayout(False)
        Me.tabPolicyGenInfo.ResumeLayout(False)
        Me.tabPolicyGenInfo.PerformLayout()
        Me.tabPaymentHist.ResumeLayout(False)
        Me.tabComponent.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tcCRS As System.Windows.Forms.TabControl
    Friend WithEvents tabFinInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabDirDebitEnq As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_DirectDebitEnq1 As CRS_Ctrl.ctrl_DirectDebitEnq
    Friend WithEvents tabTranHist As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_TranHist1 As CRS_Ctrl.ctrl_TranHist
    Friend WithEvents tabPolicyGenInfo As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_CRSPolicyGeneral_Information1 As CRS_Ctrl.ctrl_CRSPolicyGeneral_Information
    Friend WithEvents Ctrl_FinancialInfo1 As CRS_Ctrl.ctrl_FinancialInfo
    Friend WithEvents Ctrl_FinancialDtl1 As CRS_Ctrl.ctrl_FinancialDtl
    Friend WithEvents tabPaymentHist As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_PaymentHist1 As CRS_Ctrl.ctrl_PaymentHist
    Friend WithEvents Ctrl_BillingInf1 As POSCommCtrl.Ctrl_BillingInf
    Friend WithEvents tabComponent As System.Windows.Forms.TabPage
    Friend WithEvents Ctrl_ChgComponent1 As POSCommCtrl.Ctrl_ChgComponent
    Friend WithEvents lblPolicyNo As System.Windows.Forms.Label
End Class
