<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_LevyHistory
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
        Me.dgvLevyHeader = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.dgvLevyBillingRecord = New System.Windows.Forms.DataGridView
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.dgvLevyReceviceRecord = New System.Windows.Forms.DataGridView
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.dgvLevyOutstandingRecord = New System.Windows.Forms.DataGridView
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        CType(Me.dgvLevyHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvLevyBillingRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvLevyReceviceRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgvLevyOutstandingRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvLevyHeader
        '
        Me.dgvLevyHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLevyHeader.Location = New System.Drawing.Point(6, 19)
        Me.dgvLevyHeader.Name = "dgvLevyHeader"
        Me.dgvLevyHeader.Size = New System.Drawing.Size(816, 153)
        Me.dgvLevyHeader.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvLevyHeader)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(837, 183)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Levy Header"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvLevyBillingRecord)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 188)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(837, 183)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Levy Billing Record History"
        '
        'dgvLevyBillingRecord
        '
        Me.dgvLevyBillingRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLevyBillingRecord.Location = New System.Drawing.Point(6, 19)
        Me.dgvLevyBillingRecord.Name = "dgvLevyBillingRecord"
        Me.dgvLevyBillingRecord.Size = New System.Drawing.Size(816, 153)
        Me.dgvLevyBillingRecord.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dgvLevyReceviceRecord)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 374)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(834, 183)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Levy Receive Record History"
        '
        'dgvLevyReceviceRecord
        '
        Me.dgvLevyReceviceRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLevyReceviceRecord.Location = New System.Drawing.Point(6, 19)
        Me.dgvLevyReceviceRecord.Name = "dgvLevyReceviceRecord"
        Me.dgvLevyReceviceRecord.Size = New System.Drawing.Size(813, 153)
        Me.dgvLevyReceviceRecord.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.dgvLevyOutstandingRecord)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 563)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(834, 183)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Levy Overdue follow Up"
        '
        'dgvLevyOutstandingRecord
        '
        Me.dgvLevyOutstandingRecord.AllowUserToAddRows = False
        Me.dgvLevyOutstandingRecord.AllowUserToDeleteRows = False
        Me.dgvLevyOutstandingRecord.AllowUserToResizeColumns = False
        Me.dgvLevyOutstandingRecord.AllowUserToResizeRows = False
        Me.dgvLevyOutstandingRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLevyOutstandingRecord.Location = New System.Drawing.Point(6, 19)
        Me.dgvLevyOutstandingRecord.Name = "dgvLevyOutstandingRecord"
        Me.dgvLevyOutstandingRecord.ReadOnly = True
        Me.dgvLevyOutstandingRecord.Size = New System.Drawing.Size(813, 153)
        Me.dgvLevyOutstandingRecord.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(694, 752)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(146, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Modify Outstanding Record"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(544, 752)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Add Outstanding Record"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(462, 752)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(76, 23)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "Refresh"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ctrl_LevyHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ctrl_LevyHistory"
        Me.Size = New System.Drawing.Size(854, 796)
        CType(Me.dgvLevyHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvLevyBillingRecord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.dgvLevyReceviceRecord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.dgvLevyOutstandingRecord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvLevyHeader As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvLevyBillingRecord As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvLevyReceviceRecord As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvLevyOutstandingRecord As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button

End Class
