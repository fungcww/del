<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_FundTranHist
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.dgHist = New System.Windows.Forms.DataGridView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblSWI = New System.Windows.Forms.Label
        Me.lblSWO = New System.Windows.Forms.Label
        Me.dgSwitchIn = New System.Windows.Forms.DataGridView
        Me.dgSwitchOut = New System.Windows.Forms.DataGridView
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgSwitchIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgSwitchOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.dgHist)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(763, 167)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Fund Transaction History"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(675, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dgHist
        '
        Me.dgHist.AllowUserToAddRows = False
        Me.dgHist.AllowUserToDeleteRows = False
        Me.dgHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgHist.Location = New System.Drawing.Point(6, 24)
        Me.dgHist.Name = "dgHist"
        Me.dgHist.Size = New System.Drawing.Size(663, 137)
        Me.dgHist.TabIndex = 15
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblSWI)
        Me.GroupBox2.Controls.Add(Me.lblSWO)
        Me.GroupBox2.Controls.Add(Me.dgSwitchIn)
        Me.GroupBox2.Controls.Add(Me.dgSwitchOut)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 167)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(870, 388)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        '
        'lblSWI
        '
        Me.lblSWI.AutoSize = True
        Me.lblSWI.Location = New System.Drawing.Point(3, 194)
        Me.lblSWI.Name = "lblSWI"
        Me.lblSWI.Size = New System.Drawing.Size(86, 13)
        Me.lblSWI.TabIndex = 20
        Me.lblSWI.Text = "Switch In Details"
        '
        'lblSWO
        '
        Me.lblSWO.AutoSize = True
        Me.lblSWO.Location = New System.Drawing.Point(3, 13)
        Me.lblSWO.Name = "lblSWO"
        Me.lblSWO.Size = New System.Drawing.Size(94, 13)
        Me.lblSWO.TabIndex = 19
        Me.lblSWO.Text = "Switch Out Details"
        '
        'dgSwitchIn
        '
        Me.dgSwitchIn.AllowUserToAddRows = False
        Me.dgSwitchIn.AllowUserToDeleteRows = False
        Me.dgSwitchIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSwitchIn.Location = New System.Drawing.Point(6, 210)
        Me.dgSwitchIn.Name = "dgSwitchIn"
        Me.dgSwitchIn.Size = New System.Drawing.Size(858, 171)
        Me.dgSwitchIn.TabIndex = 18
        '
        'dgSwitchOut
        '
        Me.dgSwitchOut.AllowUserToAddRows = False
        Me.dgSwitchOut.AllowUserToDeleteRows = False
        Me.dgSwitchOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSwitchOut.Location = New System.Drawing.Point(6, 29)
        Me.dgSwitchOut.Name = "dgSwitchOut"
        Me.dgSwitchOut.Size = New System.Drawing.Size(856, 159)
        Me.dgSwitchOut.TabIndex = 17
        '
        'ctrl_FundTranHist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ctrl_FundTranHist"
        Me.Size = New System.Drawing.Size(875, 561)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgSwitchIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgSwitchOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dgHist As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSWI As System.Windows.Forms.Label
    Friend WithEvents lblSWO As System.Windows.Forms.Label
    Friend WithEvents dgSwitchIn As System.Windows.Forms.DataGridView
    Friend WithEvents dgSwitchOut As System.Windows.Forms.DataGridView

End Class
