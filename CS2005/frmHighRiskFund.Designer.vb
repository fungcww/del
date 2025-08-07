<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHighRiskFund
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
        Me.dgvFund = New System.Windows.Forms.DataGridView
        Me.ChkFund = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.FundCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FundDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FundChiDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnSubmit = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        CType(Me.dgvFund, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvFund
        '
        Me.dgvFund.AllowUserToAddRows = False
        Me.dgvFund.AllowUserToDeleteRows = False
        Me.dgvFund.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFund.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ChkFund, Me.FundCode, Me.FundDescription, Me.FundChiDesc})
        Me.dgvFund.Location = New System.Drawing.Point(1, 12)
        Me.dgvFund.Name = "dgvFund"
        Me.dgvFund.Size = New System.Drawing.Size(625, 326)
        Me.dgvFund.TabIndex = 0
        '
        'ChkFund
        '
        Me.ChkFund.HeaderText = ""
        Me.ChkFund.Name = "ChkFund"
        Me.ChkFund.Width = 30
        '
        'FundCode
        '
        Me.FundCode.DataPropertyName = "FundCode"
        Me.FundCode.HeaderText = "Fund Code"
        Me.FundCode.Name = "FundCode"
        '
        'FundDescription
        '
        Me.FundDescription.DataPropertyName = "FundDescription"
        Me.FundDescription.HeaderText = "Fund Description"
        Me.FundDescription.Name = "FundDescription"
        Me.FundDescription.Width = 450
        '
        'FundChiDesc
        '
        Me.FundChiDesc.DataPropertyName = "FundChiDesc"
        Me.FundChiDesc.HeaderText = "FundChiDesc"
        Me.FundChiDesc.Name = "FundChiDesc"
        Me.FundChiDesc.Visible = False
        '
        'btnSubmit
        '
        Me.btnSubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSubmit.Location = New System.Drawing.Point(451, 344)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(92, 25)
        Me.btnSubmit.TabIndex = 1
        Me.btnSubmit.Text = "&OK"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(550, 344)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 25)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmHighRiskFund
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(626, 372)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.dgvFund)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHighRiskFund"
        Me.Text = "High Risk Fund Selection"
        CType(Me.dgvFund, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvFund As System.Windows.Forms.DataGridView
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents ChkFund As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FundCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FundDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FundChiDesc As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
