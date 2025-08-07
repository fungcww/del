<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uclFundTxHist
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
        Me.dgSwitchIn = New System.Windows.Forms.DataGridView
        Me.dgSwitchOut = New System.Windows.Forms.DataGridView
        Me.dgHist = New System.Windows.Forms.DataGridView
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSWO = New System.Windows.Forms.Label
        Me.lblSWI = New System.Windows.Forms.Label
        CType(Me.dgSwitchIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgSwitchOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgSwitchIn
        '
        Me.dgSwitchIn.AllowUserToAddRows = False
        Me.dgSwitchIn.AllowUserToDeleteRows = False
        Me.dgSwitchIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSwitchIn.Location = New System.Drawing.Point(3, 358)
        Me.dgSwitchIn.Name = "dgSwitchIn"
        Me.dgSwitchIn.Size = New System.Drawing.Size(858, 171)
        Me.dgSwitchIn.TabIndex = 5
        '
        'dgSwitchOut
        '
        Me.dgSwitchOut.AllowUserToAddRows = False
        Me.dgSwitchOut.AllowUserToDeleteRows = False
        Me.dgSwitchOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSwitchOut.Location = New System.Drawing.Point(3, 176)
        Me.dgSwitchOut.Name = "dgSwitchOut"
        Me.dgSwitchOut.Size = New System.Drawing.Size(856, 159)
        Me.dgSwitchOut.TabIndex = 4
        '
        'dgHist
        '
        Me.dgHist.AllowUserToAddRows = False
        Me.dgHist.AllowUserToDeleteRows = False
        Me.dgHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgHist.Location = New System.Drawing.Point(6, 21)
        Me.dgHist.Name = "dgHist"
        Me.dgHist.Size = New System.Drawing.Size(510, 137)
        Me.dgHist.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(522, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Fund Transaction History"
        '
        'lblSWO
        '
        Me.lblSWO.AutoSize = True
        Me.lblSWO.Location = New System.Drawing.Point(3, 161)
        Me.lblSWO.Name = "lblSWO"
        Me.lblSWO.Size = New System.Drawing.Size(94, 13)
        Me.lblSWO.TabIndex = 8
        Me.lblSWO.Text = "Switch Out Details"
        '
        'lblSWI
        '
        Me.lblSWI.AutoSize = True
        Me.lblSWI.Location = New System.Drawing.Point(3, 342)
        Me.lblSWI.Name = "lblSWI"
        Me.lblSWI.Size = New System.Drawing.Size(86, 13)
        Me.lblSWI.TabIndex = 9
        Me.lblSWI.Text = "Switch In Details"
        '
        'uclFundTxHist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblSWI)
        Me.Controls.Add(Me.lblSWO)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dgSwitchIn)
        Me.Controls.Add(Me.dgSwitchOut)
        Me.Controls.Add(Me.dgHist)
        Me.Name = "uclFundTxHist"
        Me.Size = New System.Drawing.Size(866, 535)
        CType(Me.dgSwitchIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgSwitchOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgSwitchIn As System.Windows.Forms.DataGridView
    Friend WithEvents dgSwitchOut As System.Windows.Forms.DataGridView
    Friend WithEvents dgHist As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSWO As System.Windows.Forms.Label
    Friend WithEvents lblSWI As System.Windows.Forms.Label

End Class
