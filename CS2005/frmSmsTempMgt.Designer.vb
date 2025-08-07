<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSmsTempMgt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.lblnewengmsg = New System.Windows.Forms.Label
        Me.txtNewEngContent = New System.Windows.Forms.TextBox
        Me.txtNewChiContent = New System.Windows.Forms.TextBox
        Me.lblnewchimsg = New System.Windows.Forms.Label
        Me.btnaddmsg = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnresetnewmsg = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btndeletemsg = New System.Windows.Forms.Button
        Me.btnmodifymsg = New System.Windows.Forms.Button
        Me.txtEditUuid = New System.Windows.Forms.TextBox
        Me.lbluuid = New System.Windows.Forms.Label
        Me.txtEditEngContent = New System.Windows.Forms.TextBox
        Me.lblditengmsg = New System.Windows.Forms.Label
        Me.txtEditChiContent = New System.Windows.Forms.TextBox
        Me.lbleditchimsg = New System.Windows.Forms.Label
        Me.dgvSmsTemp = New System.Windows.Forms.DataGridView
        Me.Uuid = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EngText = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ChiText = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvSmsTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblnewengmsg
        '
        Me.lblnewengmsg.AutoSize = True
        Me.lblnewengmsg.Location = New System.Drawing.Point(26, 3)
        Me.lblnewengmsg.Name = "lblnewengmsg"
        Me.lblnewengmsg.Size = New System.Drawing.Size(41, 13)
        Me.lblnewengmsg.TabIndex = 0
        Me.lblnewengmsg.Text = "English"
        '
        'txtNewEngContent
        '
        Me.txtNewEngContent.Location = New System.Drawing.Point(29, 19)
        Me.txtNewEngContent.MaxLength = 500
        Me.txtNewEngContent.Multiline = True
        Me.txtNewEngContent.Name = "txtNewEngContent"
        Me.txtNewEngContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNewEngContent.Size = New System.Drawing.Size(552, 120)
        Me.txtNewEngContent.TabIndex = 1
        '
        'txtNewChiContent
        '
        Me.txtNewChiContent.Location = New System.Drawing.Point(29, 174)
        Me.txtNewChiContent.MaxLength = 500
        Me.txtNewChiContent.Multiline = True
        Me.txtNewChiContent.Name = "txtNewChiContent"
        Me.txtNewChiContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNewChiContent.Size = New System.Drawing.Size(552, 120)
        Me.txtNewChiContent.TabIndex = 3
        '
        'lblnewchimsg
        '
        Me.lblnewchimsg.AutoSize = True
        Me.lblnewchimsg.Location = New System.Drawing.Point(26, 154)
        Me.lblnewchimsg.Name = "lblnewchimsg"
        Me.lblnewchimsg.Size = New System.Drawing.Size(45, 13)
        Me.lblnewchimsg.TabIndex = 2
        Me.lblnewchimsg.Text = "Chinese"
        '
        'btnaddmsg
        '
        Me.btnaddmsg.Location = New System.Drawing.Point(496, 325)
        Me.btnaddmsg.Name = "btnaddmsg"
        Me.btnaddmsg.Size = New System.Drawing.Size(85, 38)
        Me.btnaddmsg.TabIndex = 4
        Me.btnaddmsg.Text = "Add"
        Me.btnaddmsg.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(610, 557)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnresetnewmsg)
        Me.TabPage1.Controls.Add(Me.btnaddmsg)
        Me.TabPage1.Controls.Add(Me.lblnewengmsg)
        Me.TabPage1.Controls.Add(Me.txtNewChiContent)
        Me.TabPage1.Controls.Add(Me.txtNewEngContent)
        Me.TabPage1.Controls.Add(Me.lblnewchimsg)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(602, 531)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Add"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnresetnewmsg
        '
        Me.btnresetnewmsg.Location = New System.Drawing.Point(378, 325)
        Me.btnresetnewmsg.Name = "btnresetnewmsg"
        Me.btnresetnewmsg.Size = New System.Drawing.Size(85, 38)
        Me.btnresetnewmsg.TabIndex = 5
        Me.btnresetnewmsg.Text = "Reset"
        Me.btnresetnewmsg.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btndeletemsg)
        Me.TabPage2.Controls.Add(Me.btnmodifymsg)
        Me.TabPage2.Controls.Add(Me.txtEditUuid)
        Me.TabPage2.Controls.Add(Me.lbluuid)
        Me.TabPage2.Controls.Add(Me.txtEditEngContent)
        Me.TabPage2.Controls.Add(Me.lblditengmsg)
        Me.TabPage2.Controls.Add(Me.txtEditChiContent)
        Me.TabPage2.Controls.Add(Me.lbleditchimsg)
        Me.TabPage2.Controls.Add(Me.dgvSmsTemp)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(602, 531)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Edit / Delete"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btndeletemsg
        '
        Me.btndeletemsg.Location = New System.Drawing.Point(19, 469)
        Me.btndeletemsg.Name = "btndeletemsg"
        Me.btndeletemsg.Size = New System.Drawing.Size(85, 38)
        Me.btndeletemsg.TabIndex = 11
        Me.btndeletemsg.Text = "Delete"
        Me.btndeletemsg.UseVisualStyleBackColor = True
        '
        'btnmodifymsg
        '
        Me.btnmodifymsg.Location = New System.Drawing.Point(486, 469)
        Me.btnmodifymsg.Name = "btnmodifymsg"
        Me.btnmodifymsg.Size = New System.Drawing.Size(85, 38)
        Me.btnmodifymsg.TabIndex = 10
        Me.btnmodifymsg.Text = "Modify"
        Me.btnmodifymsg.UseVisualStyleBackColor = True
        '
        'txtEditUuid
        '
        Me.txtEditUuid.Enabled = False
        Me.txtEditUuid.Location = New System.Drawing.Point(514, 159)
        Me.txtEditUuid.Name = "txtEditUuid"
        Me.txtEditUuid.Size = New System.Drawing.Size(57, 20)
        Me.txtEditUuid.TabIndex = 9
        '
        'lbluuid
        '
        Me.lbluuid.AutoSize = True
        Me.lbluuid.Location = New System.Drawing.Point(484, 163)
        Me.lbluuid.Name = "lbluuid"
        Me.lbluuid.Size = New System.Drawing.Size(35, 13)
        Me.lbluuid.TabIndex = 8
        Me.lbluuid.Text = "Uuid: "
        '
        'txtEditEngContent
        '
        Me.txtEditEngContent.Location = New System.Drawing.Point(19, 186)
        Me.txtEditEngContent.MaxLength = 500
        Me.txtEditEngContent.Multiline = True
        Me.txtEditEngContent.Name = "txtEditEngContent"
        Me.txtEditEngContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEditEngContent.Size = New System.Drawing.Size(552, 120)
        Me.txtEditEngContent.TabIndex = 7
        '
        'lblditengmsg
        '
        Me.lblditengmsg.AutoSize = True
        Me.lblditengmsg.Location = New System.Drawing.Point(16, 166)
        Me.lblditengmsg.Name = "lblditengmsg"
        Me.lblditengmsg.Size = New System.Drawing.Size(41, 13)
        Me.lblditengmsg.TabIndex = 6
        Me.lblditengmsg.Text = "English"
        '
        'txtEditChiContent
        '
        Me.txtEditChiContent.Location = New System.Drawing.Point(19, 330)
        Me.txtEditChiContent.MaxLength = 500
        Me.txtEditChiContent.Multiline = True
        Me.txtEditChiContent.Name = "txtEditChiContent"
        Me.txtEditChiContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEditChiContent.Size = New System.Drawing.Size(552, 120)
        Me.txtEditChiContent.TabIndex = 5
        '
        'lbleditchimsg
        '
        Me.lbleditchimsg.AutoSize = True
        Me.lbleditchimsg.Location = New System.Drawing.Point(16, 310)
        Me.lbleditchimsg.Name = "lbleditchimsg"
        Me.lbleditchimsg.Size = New System.Drawing.Size(45, 13)
        Me.lbleditchimsg.TabIndex = 4
        Me.lbleditchimsg.Text = "Chinese"
        '
        'dgvSmsTemp
        '
        Me.dgvSmsTemp.AllowUserToAddRows = False
        Me.dgvSmsTemp.AllowUserToDeleteRows = False
        Me.dgvSmsTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSmsTemp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Uuid, Me.EngText, Me.ChiText})
        Me.dgvSmsTemp.Location = New System.Drawing.Point(7, 7)
        Me.dgvSmsTemp.Name = "dgvSmsTemp"
        Me.dgvSmsTemp.ReadOnly = True
        Me.dgvSmsTemp.Size = New System.Drawing.Size(589, 150)
        Me.dgvSmsTemp.TabIndex = 0
        '
        'Uuid
        '
        Me.Uuid.DataPropertyName = "Uuid"
        Me.Uuid.FillWeight = 50.0!
        Me.Uuid.Frozen = True
        Me.Uuid.HeaderText = "Uuid"
        Me.Uuid.MinimumWidth = 50
        Me.Uuid.Name = "Uuid"
        Me.Uuid.ReadOnly = True
        Me.Uuid.Width = 50
        '
        'EngText
        '
        Me.EngText.DataPropertyName = "EngText"
        Me.EngText.FillWeight = 400.0!
        Me.EngText.HeaderText = "Eng Content"
        Me.EngText.Name = "EngText"
        Me.EngText.ReadOnly = True
        Me.EngText.Width = 250
        '
        'ChiText
        '
        Me.ChiText.DataPropertyName = "ChiText"
        Me.ChiText.FillWeight = 400.0!
        Me.ChiText.HeaderText = "Chi Content"
        Me.ChiText.Name = "ChiText"
        Me.ChiText.ReadOnly = True
        Me.ChiText.Width = 250
        '
        'frmSmsTempMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 572)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmSmsTempMgt"
        Me.Text = "Message Template Managment"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgvSmsTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblnewengmsg As System.Windows.Forms.Label
    Friend WithEvents txtNewChiContent As System.Windows.Forms.TextBox
    Friend WithEvents lblnewchimsg As System.Windows.Forms.Label
    Friend WithEvents txtNewEngContent As System.Windows.Forms.TextBox
    Friend WithEvents btnaddmsg As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnresetnewmsg As System.Windows.Forms.Button
    Friend WithEvents dgvSmsTemp As System.Windows.Forms.DataGridView
    Friend WithEvents txtEditEngContent As System.Windows.Forms.TextBox
    Friend WithEvents lblditengmsg As System.Windows.Forms.Label
    Friend WithEvents txtEditChiContent As System.Windows.Forms.TextBox
    Friend WithEvents lbleditchimsg As System.Windows.Forms.Label
    Friend WithEvents Uuid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EngText As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChiText As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtEditUuid As System.Windows.Forms.TextBox
    Friend WithEvents lbluuid As System.Windows.Forms.Label
    Friend WithEvents btndeletemsg As System.Windows.Forms.Button
    Friend WithEvents btnmodifymsg As System.Windows.Forms.Button
End Class
