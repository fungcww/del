<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctrl_TapNGo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrl_TapNGo))
        Dim PolicyData1 As POSCommCtrl.Ctrl_POS_Scrn_Head.PolicyData = New POSCommCtrl.Ctrl_POS_Scrn_Head.PolicyData()
        Me.Ctrl_POS_Scrn_Head1 = New POSCommCtrl.Ctrl_POS_Scrn_Head()
        Me.dgvCSView = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnUnlock = New System.Windows.Forms.Button()
        Me.lblLockDate = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblLockStatus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvCSView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Ctrl_POS_Scrn_Head1
        '
        Me.Ctrl_POS_Scrn_Head1.iInsuredCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse = "0"
        Me.Ctrl_POS_Scrn_Head1.Location = New System.Drawing.Point(3, 3)
        Me.Ctrl_POS_Scrn_Head1.ModeInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.Name = "Ctrl_POS_Scrn_Head1"
        Me.Ctrl_POS_Scrn_Head1.PolicyDataInuse = PolicyData1
        Me.Ctrl_POS_Scrn_Head1.policyInuse = ""
        Me.Ctrl_POS_Scrn_Head1.PTDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.RCDInuse = "--"
        Me.Ctrl_POS_Scrn_Head1.sCIWInsuredEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.sCIWOwnerChnNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.sCIWOwnerEngNameInuse = ""
        Me.Ctrl_POS_Scrn_Head1.Size = New System.Drawing.Size(1024, 118)
        Me.Ctrl_POS_Scrn_Head1.sOwnerUseChiIndInuse = "N"
        Me.Ctrl_POS_Scrn_Head1.SystemInuse = ""
        Me.Ctrl_POS_Scrn_Head1.TabIndex = 0
        '
        'dgvCSView
        '
        Me.dgvCSView.AllowUserToAddRows = False
        Me.dgvCSView.AllowUserToDeleteRows = False
        Me.dgvCSView.AllowUserToResizeColumns = False
        Me.dgvCSView.AllowUserToResizeRows = False
        Me.dgvCSView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCSView.Location = New System.Drawing.Point(12, 127)
        Me.dgvCSView.Name = "dgvCSView"
        Me.dgvCSView.ReadOnly = True
        Me.dgvCSView.Size = New System.Drawing.Size(1007, 167)
        Me.dgvCSView.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnUnlock)
        Me.GroupBox1.Controls.Add(Me.lblLockDate)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblLockStatus)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 317)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(214, 119)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Unlock Tap and Go Functions"
        '
        'btnUnlock
        '
        Me.btnUnlock.Location = New System.Drawing.Point(62, 79)
        Me.btnUnlock.Name = "btnUnlock"
        Me.btnUnlock.Size = New System.Drawing.Size(75, 23)
        Me.btnUnlock.TabIndex = 7
        Me.btnUnlock.Text = "Unlock"
        Me.btnUnlock.UseVisualStyleBackColor = True
        '
        'lblLockDate
        '
        Me.lblLockDate.AutoSize = True
        Me.lblLockDate.Location = New System.Drawing.Point(119, 53)
        Me.lblLockDate.Name = "lblLockDate"
        Me.lblLockDate.Size = New System.Drawing.Size(75, 13)
        Me.lblLockDate.TabIndex = 3
        Me.lblLockDate.Text = "YYYY-MM-DD"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Locked Date"
        '
        'lblLockStatus
        '
        Me.lblLockStatus.AutoSize = True
        Me.lblLockStatus.Location = New System.Drawing.Point(119, 26)
        Me.lblLockStatus.Name = "lblLockStatus"
        Me.lblLockStatus.Size = New System.Drawing.Size(76, 13)
        Me.lblLockStatus.TabIndex = 1
        Me.lblLockStatus.Text = "NOT LOCKED"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Locked Status"
        '
        'ctrl_TapNGo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgvCSView)
        Me.Controls.Add(Me.Ctrl_POS_Scrn_Head1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ctrl_TapNGo"
        Me.Size = New System.Drawing.Size(1033, 725)
        CType(Me.dgvCSView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ctrl_POS_Scrn_Head1 As POSCommCtrl.Ctrl_POS_Scrn_Head
    Friend WithEvents dgvCSView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblLockDate As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblLockStatus As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnUnlock As System.Windows.Forms.Button

End Class
