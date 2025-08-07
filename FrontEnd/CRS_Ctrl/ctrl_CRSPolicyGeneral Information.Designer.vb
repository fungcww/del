<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctrl_CRSPolicyGeneral_Information
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrl_CRSPolicyGeneral_Information))
        Dim PolicyData2 As POSCommCtrl.Ctrl_POS_Scrn_Head.PolicyData = New POSCommCtrl.Ctrl_POS_Scrn_Head.PolicyData()
        Me.Ctrl_POS_PolicyClient1 = New POSCommCtrl.Ctrl_POS_PolicyClient
        Me.Ctrl_POS_Scrn_Head1 = New POSCommCtrl.Ctrl_POS_Scrn_Head
        Me.Ctrl_POS_MRP1 = New POSCommCtrl.Ctrl_POS_MRP
        Me.Ctrl_BillingInfo1 = New CRS_Ctrl.ctrl_BillingInfo
        Me.Ctrl_VisitCS1 = New CRS_Ctrl.ctrl_VisitCS
        Me.btnSLHistory = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Ctrl_POS_PolicyClient1
        '
        Me.Ctrl_POS_PolicyClient1.CurrencyInUse = ""
        Me.Ctrl_POS_PolicyClient1.EffDateInuse = New Date(CType(0, Long))
        Me.Ctrl_POS_PolicyClient1.IsAssignmentPolicy = False
        Me.Ctrl_POS_PolicyClient1.IsCIESPolicy = False
        Me.Ctrl_POS_PolicyClient1.IsPos = False
        Me.Ctrl_POS_PolicyClient1.Location = New System.Drawing.Point(13, 119)
        Me.Ctrl_POS_PolicyClient1.modeinuse = POSCommCtrl.Ctrl_POS_PolicyClient.ModeName.Enquiry
        Me.Ctrl_POS_PolicyClient1.Name = "Ctrl_POS_PolicyClient1"
        Me.Ctrl_POS_PolicyClient1.PlanCodeInUse = ""
        Me.Ctrl_POS_PolicyClient1.PolicyChannelInuse = ""
        Me.Ctrl_POS_PolicyClient1.PolicyInUse = Nothing
        Me.Ctrl_POS_PolicyClient1.PolicyNoInuse = Nothing
        Me.Ctrl_POS_PolicyClient1.ProductTypeInuse = ""
        Me.Ctrl_POS_PolicyClient1.RCDDateInuse = ""
        Me.Ctrl_POS_PolicyClient1.Size = New System.Drawing.Size(818, 310)
        Me.Ctrl_POS_PolicyClient1.SystemInUse = Nothing
        Me.Ctrl_POS_PolicyClient1.TabIndex = 1
        '
        'Ctrl_POS_Scrn_Head1
        '
        Me.Ctrl_POS_Scrn_Head1.iInsuredCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_POS_Scrn_Head1.ModeInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.Name = "Ctrl_POS_Scrn_Head1"
        Me.Ctrl_POS_Scrn_Head1.PolicyDataInuse = PolicyData2
        Me.Ctrl_POS_Scrn_Head1.policyInuse = ""
        Me.Ctrl_POS_Scrn_Head1.PTDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.RCDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.sCIWInsuredEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.sCIWOwnerChnNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.sCIWOwnerEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.Size = New System.Drawing.Size(1024, 111)
        Me.Ctrl_POS_Scrn_Head1.sOwnerUseChiIndInuse = "N"
        Me.Ctrl_POS_Scrn_Head1.SystemInuse = ""
        Me.Ctrl_POS_Scrn_Head1.TabIndex = 0
        '
        'Ctrl_BillingInfo1
        '
        Me.Ctrl_BillingInfo1.Location = New System.Drawing.Point(9, 425)
        Me.Ctrl_BillingInfo1.Name = "Ctrl_BillingInfo1"
        Me.Ctrl_BillingInfo1.PolicyNoInUse = Nothing
        Me.Ctrl_BillingInfo1.Size = New System.Drawing.Size(1024, 310)
        Me.Ctrl_BillingInfo1.TabIndex = 2
        '
        'Ctrl_VisitCS1
        '
        Me.Ctrl_VisitCS1.CSFlag = "Y"
        Me.Ctrl_VisitCS1.CustomerID = ""
        Me.Ctrl_VisitCS1.Lang = "en"
        Me.Ctrl_VisitCS1.Location = New System.Drawing.Point(830, 249)
        Me.Ctrl_VisitCS1.Name = "Ctrl_VisitCS1"
        Me.Ctrl_VisitCS1.PolicyNo = ""
        Me.Ctrl_VisitCS1.PolicyNoInUse = ""
        Me.Ctrl_VisitCS1.Size = New System.Drawing.Size(225, 154)
        Me.Ctrl_VisitCS1.TabIndex = 4
        Me.Ctrl_VisitCS1.UpdateDate = ""
        Me.Ctrl_VisitCS1.UpdateUser = ""
        Me.Ctrl_VisitCS1.UpdateUser2 = ""
        '
        'Ctrl_POS_MRP1
        '
        Me.Ctrl_POS_MRP1.EffectiveDate = New Date(2016, 1, 12, 20, 2, 2, 606)
        Me.Ctrl_POS_MRP1.Location = New System.Drawing.Point(830, 127)
        Me.Ctrl_POS_MRP1.Name = "Ctrl_POS_MRP1"
        Me.Ctrl_POS_MRP1.PolicyNo = ""
        Me.Ctrl_POS_MRP1.Size = New System.Drawing.Size(197, 124)
        Me.Ctrl_POS_MRP1.TabIndex = 3
        '
        'btnSLHistory
        '
        Me.btnSLHistory.Location = New System.Drawing.Point(837, 398)
        Me.btnSLHistory.Name = "btnSLHistory"
        Me.btnSLHistory.Size = New System.Drawing.Size(124, 23)
        Me.btnSLHistory.TabIndex = 5
        Me.btnSLHistory.Text = "Smart Legacy History"
        Me.btnSLHistory.UseVisualStyleBackColor = True
        '
        'ctrl_CRSPolicyGeneral_Information
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnSLHistory)
        Me.Controls.Add(Me.Ctrl_POS_MRP1)
        Me.Controls.Add(Me.Ctrl_VisitCS1)
        Me.Controls.Add(Me.Ctrl_BillingInfo1)
        Me.Controls.Add(Me.Ctrl_POS_PolicyClient1)
        Me.Controls.Add(Me.Ctrl_POS_Scrn_Head1)
        Me.Name = "ctrl_CRSPolicyGeneral_Information"
        Me.Size = New System.Drawing.Size(1056, 746)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ctrl_POS_Scrn_Head1 As POSCommCtrl.Ctrl_POS_Scrn_Head
    Friend WithEvents Ctrl_POS_PolicyClient1 As POSCommCtrl.Ctrl_POS_PolicyClient
    Friend WithEvents Ctrl_BillingInfo1 As CRS_Ctrl.ctrl_BillingInfo
    Friend WithEvents Ctrl_POS_MRP1 As POSCommCtrl.Ctrl_POS_MRP
    Friend WithEvents Ctrl_VisitCS1 As CRS_Ctrl.ctrl_VisitCS
    Friend WithEvents btnSLHistory As Button
End Class
