<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFundRiskLevel
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
        Me.components = New System.ComponentModel.Container
        Me.dgvFund = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFundCodeFilter = New System.Windows.Forms.TextBox
        Me.cmdFilter = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtFundName = New System.Windows.Forms.TextBox
        Me.txtFundCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboRiskLevel = New System.Windows.Forms.ComboBox
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.bsFund = New System.Windows.Forms.BindingSource(Me.components)
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCurrency = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRisk = New System.Windows.Forms.DataGridViewComboBoxColumn
        CType(Me.dgvFund, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.bsFund, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvFund
        '
        Me.dgvFund.AllowUserToAddRows = False
        Me.dgvFund.AllowUserToDeleteRows = False
        Me.dgvFund.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFund.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.colCurrency, Me.Column2, Me.colRisk})
        Me.dgvFund.Location = New System.Drawing.Point(19, 88)
        Me.dgvFund.Name = "dgvFund"
        Me.dgvFund.ReadOnly = True
        Me.dgvFund.Size = New System.Drawing.Size(634, 193)
        Me.dgvFund.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Investment Fund"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(416, 379)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(497, 379)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(578, 379)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtFundCodeFilter)
        Me.GroupBox1.Controls.Add(Me.cmdFilter)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(634, 45)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Find"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Fund Code"
        '
        'txtFundCodeFilter
        '
        Me.txtFundCodeFilter.Location = New System.Drawing.Point(91, 17)
        Me.txtFundCodeFilter.Name = "txtFundCodeFilter"
        Me.txtFundCodeFilter.Size = New System.Drawing.Size(100, 20)
        Me.txtFundCodeFilter.TabIndex = 1
        '
        'cmdFilter
        '
        Me.cmdFilter.Location = New System.Drawing.Point(197, 15)
        Me.cmdFilter.Name = "cmdFilter"
        Me.cmdFilter.Size = New System.Drawing.Size(75, 23)
        Me.cmdFilter.TabIndex = 0
        Me.cmdFilter.Text = "Filter"
        Me.cmdFilter.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtFundName)
        Me.GroupBox2.Controls.Add(Me.txtFundCode)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cboRiskLevel)
        Me.GroupBox2.Location = New System.Drawing.Point(19, 287)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(634, 86)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Risk Level"
        '
        'txtFundName
        '
        Me.txtFundName.Location = New System.Drawing.Point(151, 41)
        Me.txtFundName.Name = "txtFundName"
        Me.txtFundName.ReadOnly = True
        Me.txtFundName.Size = New System.Drawing.Size(309, 20)
        Me.txtFundName.TabIndex = 7
        '
        'txtFundCode
        '
        Me.txtFundCode.Location = New System.Drawing.Point(41, 41)
        Me.txtFundCode.Name = "txtFundCode"
        Me.txtFundCode.ReadOnly = True
        Me.txtFundCode.Size = New System.Drawing.Size(100, 20)
        Me.txtFundCode.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(148, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Fund Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Fund Code"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(463, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Risk Level"
        '
        'cboRiskLevel
        '
        Me.cboRiskLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRiskLevel.FormattingEnabled = True
        Me.cboRiskLevel.Location = New System.Drawing.Point(466, 41)
        Me.cboRiskLevel.Name = "cboRiskLevel"
        Me.cboRiskLevel.Size = New System.Drawing.Size(121, 21)
        Me.cboRiskLevel.TabIndex = 0
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(335, 379)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(75, 23)
        Me.cmdEdit.TabIndex = 7
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "mpfinv_code"
        Me.Column1.HeaderText = "Fund Code"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'colCurrency
        '
        Me.colCurrency.DataPropertyName = "mpfinv_curr"
        Me.colCurrency.HeaderText = "Currency"
        Me.colCurrency.Name = "colCurrency"
        Me.colCurrency.ReadOnly = True
        Me.colCurrency.Width = 70
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "mpfinv_chi_desc"
        Me.Column2.HeaderText = "Fund Name"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 300
        '
        'colRisk
        '
        Me.colRisk.DataPropertyName = "mpfinv_risk_level"
        Me.colRisk.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.colRisk.HeaderText = "Risk Level"
        Me.colRisk.Name = "colRisk"
        Me.colRisk.ReadOnly = True
        Me.colRisk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'frmFundRiskLevel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 425)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvFund)
        Me.Name = "frmFundRiskLevel"
        Me.Text = "Fund Risk Level Maintenance"
        CType(Me.dgvFund, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.bsFund, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvFund As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFundCodeFilter As System.Windows.Forms.TextBox
    Friend WithEvents cmdFilter As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboRiskLevel As System.Windows.Forms.ComboBox
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents bsFund As System.Windows.Forms.BindingSource
    Friend WithEvents txtFundName As System.Windows.Forms.TextBox
    Friend WithEvents txtFundCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCurrency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRisk As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
