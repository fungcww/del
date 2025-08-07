<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCRSPolicyGeneralInformation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCRSPolicyGeneralInformation))
        Me.Ctrl_POS_PolicyClient1 = New POSCommCtrl.Ctrl_POS_PolicyClient
        Me.Ctrl_POS_Scrn_Head1 = New POSCommCtrl.Ctrl_POS_Scrn_Head
        Me.Ctrl_BillingInfo1 = New CRS_Ctrl.ctrl_BillingInfo
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Ctrl_POS_PolicyClient1
        '
        Me.Ctrl_POS_PolicyClient1.EffDateInuse = New Date(CType(0, Long))
        Me.Ctrl_POS_PolicyClient1.Location = New System.Drawing.Point(2, 101)
        Me.Ctrl_POS_PolicyClient1.modeinuse = POSCommCtrl.Ctrl_POS_PolicyClient.ModeName.Enquiry
        Me.Ctrl_POS_PolicyClient1.Name = "Ctrl_POS_PolicyClient1"
        Me.Ctrl_POS_PolicyClient1.PolicyNoInuse = Nothing
        Me.Ctrl_POS_PolicyClient1.Size = New System.Drawing.Size(818, 297)
        Me.Ctrl_POS_PolicyClient1.SystemInUse = Nothing
        Me.Ctrl_POS_PolicyClient1.TabIndex = 1
        '
        'Ctrl_POS_Scrn_Head1
        '
        Me.Ctrl_POS_Scrn_Head1.iInsuredCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.Location = New System.Drawing.Point(-11, 0)
        Me.Ctrl_POS_Scrn_Head1.ModeInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.Name = "Ctrl_POS_Scrn_Head1"
        Me.Ctrl_POS_Scrn_Head1.policyInuse = ""
        Me.Ctrl_POS_Scrn_Head1.PTDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.RCDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.sCIWInsuredEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.sCIWOwnerEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.Size = New System.Drawing.Size(1024, 97)
        Me.Ctrl_POS_Scrn_Head1.TabIndex = 0
        '
        'Ctrl_BillingInfo1
        '
        Me.Ctrl_BillingInfo1.Location = New System.Drawing.Point(7, 383)
        Me.Ctrl_BillingInfo1.Name = "Ctrl_BillingInfo1"
        Me.Ctrl_BillingInfo1.PolicyNoInUse = Nothing
        Me.Ctrl_BillingInfo1.Size = New System.Drawing.Size(1024, 96)
        Me.Ctrl_BillingInfo1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(938, 649)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Exit"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FrmCRSPolicyGeneralInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 702)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Ctrl_BillingInfo1)
        Me.Controls.Add(Me.Ctrl_POS_PolicyClient1)
        Me.Controls.Add(Me.Ctrl_POS_Scrn_Head1)
        Me.Name = "FrmCRSPolicyGeneralInformation"
        Me.Text = "Policy General Information"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ctrl_POS_Scrn_Head1 As POSCommCtrl.Ctrl_POS_Scrn_Head
    Friend WithEvents Ctrl_POS_PolicyClient1 As POSCommCtrl.Ctrl_POS_PolicyClient
    Friend WithEvents Ctrl_BillingInfo1 As CRS_Ctrl.ctrl_BillingInfo
    'Friend WithEvents Ctrl_PolicyDateInformation1 As CRS_Ctrl.ctrl_PolicyDateInformation
    'Friend WithEvents Ctrl_PolicyOptionandOtherInfo1 As CRS_Ctrl.ctrl_PolicyOptionandOtherInfo
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
