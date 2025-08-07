<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_FinancialInfo
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
        Me.dgvSubAcctPosting = New System.Windows.Forms.DataGridView
        Me.EffDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TranNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GLCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OriginalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AccountAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgvSubAcctBal = New System.Windows.Forms.DataGridView
        Me.Entity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PolicyCompLev = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubAcctType1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubAcctCurr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubAcctBal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvSubAcctPosting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSubAcctBal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvSubAcctPosting)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 212)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(773, 272)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sub Account Posting"
        '
        'dgvSubAcctPosting
        '
        Me.dgvSubAcctPosting.AllowUserToAddRows = False
        Me.dgvSubAcctPosting.AllowUserToDeleteRows = False
        Me.dgvSubAcctPosting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSubAcctPosting.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EffDate, Me.TranNo, Me.GLCode, Me.OriginalAmount, Me.AccountAmount})
        Me.dgvSubAcctPosting.Location = New System.Drawing.Point(6, 19)
        Me.dgvSubAcctPosting.Name = "dgvSubAcctPosting"
        Me.dgvSubAcctPosting.ReadOnly = True
        Me.dgvSubAcctPosting.Size = New System.Drawing.Size(759, 247)
        Me.dgvSubAcctPosting.TabIndex = 2
        '
        'EffDate
        '
        Me.EffDate.HeaderText = "Effective Date"
        Me.EffDate.Name = "EffDate"
        Me.EffDate.ReadOnly = True
        '
        'TranNo
        '
        Me.TranNo.HeaderText = "Transaction No."
        Me.TranNo.Name = "TranNo"
        Me.TranNo.ReadOnly = True
        '
        'GLCode
        '
        Me.GLCode.HeaderText = "G/L Code"
        Me.GLCode.Name = "GLCode"
        Me.GLCode.ReadOnly = True
        '
        'OriginalAmount
        '
        Me.OriginalAmount.HeaderText = "Original Amount"
        Me.OriginalAmount.Name = "OriginalAmount"
        Me.OriginalAmount.ReadOnly = True
        '
        'AccountAmount
        '
        Me.AccountAmount.HeaderText = "Accounting Amount"
        Me.AccountAmount.Name = "AccountAmount"
        Me.AccountAmount.ReadOnly = True
        '
        'dgvSubAcctBal
        '
        Me.dgvSubAcctBal.AllowUserToAddRows = False
        Me.dgvSubAcctBal.AllowUserToDeleteRows = False
        Me.dgvSubAcctBal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSubAcctBal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Entity, Me.PolicyCompLev, Me.SubAcctType1, Me.SubAcctCurr, Me.SubAcctBal})
        Me.dgvSubAcctBal.Location = New System.Drawing.Point(3, 5)
        Me.dgvSubAcctBal.Name = "dgvSubAcctBal"
        Me.dgvSubAcctBal.ReadOnly = True
        Me.dgvSubAcctBal.Size = New System.Drawing.Size(773, 200)
        Me.dgvSubAcctBal.TabIndex = 3
        '
        'Entity
        '
        Me.Entity.HeaderText = "Entity"
        Me.Entity.Name = "Entity"
        Me.Entity.ReadOnly = True
        '
        'PolicyCompLev
        '
        Me.PolicyCompLev.HeaderText = "Policy/Component Level"
        Me.PolicyCompLev.Name = "PolicyCompLev"
        Me.PolicyCompLev.ReadOnly = True
        Me.PolicyCompLev.Width = 150
        '
        'SubAcctType1
        '
        Me.SubAcctType1.HeaderText = "Sub Account Type"
        Me.SubAcctType1.Name = "SubAcctType1"
        Me.SubAcctType1.ReadOnly = True
        '
        'SubAcctCurr
        '
        Me.SubAcctCurr.HeaderText = "Sub Account Currency"
        Me.SubAcctCurr.Name = "SubAcctCurr"
        Me.SubAcctCurr.ReadOnly = True
        Me.SubAcctCurr.Width = 200
        '
        'SubAcctBal
        '
        Me.SubAcctBal.HeaderText = "Sub Account Balance"
        Me.SubAcctBal.Name = "SubAcctBal"
        Me.SubAcctBal.ReadOnly = True
        '
        'ctrl_FinancialInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvSubAcctBal)
        Me.Name = "ctrl_FinancialInfo"
        Me.Size = New System.Drawing.Size(779, 489)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvSubAcctPosting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSubAcctBal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvSubAcctPosting As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSubAcctBal As System.Windows.Forms.DataGridView
    Friend WithEvents EffDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TranNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GLCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriginalAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccountAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Entity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PolicyCompLev As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubAcctType1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubAcctCurr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubAcctBal As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
