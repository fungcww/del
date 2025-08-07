<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_TranHist
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
        Me.dgvTranHist = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgvTranPost = New System.Windows.Forms.DataGridView
        Me.GLCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubAcctCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubAcctType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OrigAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OriCurrency = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AccountingAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AccountCurrency = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdTop = New System.Windows.Forms.Button
        Me.cmdNext = New System.Windows.Forms.Button
        Me.TransactionNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TransactionDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EffDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TranCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TranDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Reversal = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvTranHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvTranPost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvTranHist
        '
        Me.dgvTranHist.AllowUserToAddRows = False
        Me.dgvTranHist.AllowUserToDeleteRows = False
        Me.dgvTranHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTranHist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TransactionNo, Me.TransactionDate, Me.EffDate, Me.TranCode, Me.TranDesc, Me.Reversal})
        Me.dgvTranHist.Location = New System.Drawing.Point(3, 3)
        Me.dgvTranHist.Name = "dgvTranHist"
        Me.dgvTranHist.ReadOnly = True
        Me.dgvTranHist.Size = New System.Drawing.Size(692, 200)
        Me.dgvTranHist.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvTranPost)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 210)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(773, 272)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transaction Posting"
        '
        'dgvTranPost
        '
        Me.dgvTranPost.AllowUserToAddRows = False
        Me.dgvTranPost.AllowUserToDeleteRows = False
        Me.dgvTranPost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTranPost.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GLCode, Me.SubAcctCode, Me.SubAcctType, Me.OrigAmount, Me.OriCurrency, Me.AccountingAmount, Me.AccountCurrency})
        Me.dgvTranPost.Location = New System.Drawing.Point(6, 19)
        Me.dgvTranPost.Name = "dgvTranPost"
        Me.dgvTranPost.ReadOnly = True
        Me.dgvTranPost.Size = New System.Drawing.Size(759, 247)
        Me.dgvTranPost.TabIndex = 2
        '
        'GLCode
        '
        Me.GLCode.HeaderText = "GL Code"
        Me.GLCode.Name = "GLCode"
        Me.GLCode.ReadOnly = True
        '
        'SubAcctCode
        '
        Me.SubAcctCode.HeaderText = "Sub Account Code"
        Me.SubAcctCode.Name = "SubAcctCode"
        Me.SubAcctCode.ReadOnly = True
        '
        'SubAcctType
        '
        Me.SubAcctType.HeaderText = "Sub Account Type"
        Me.SubAcctType.Name = "SubAcctType"
        Me.SubAcctType.ReadOnly = True
        '
        'OrigAmount
        '
        Me.OrigAmount.HeaderText = "Original Amount"
        Me.OrigAmount.Name = "OrigAmount"
        Me.OrigAmount.ReadOnly = True
        '
        'OriCurrency
        '
        Me.OriCurrency.HeaderText = "Currency"
        Me.OriCurrency.Name = "OriCurrency"
        Me.OriCurrency.ReadOnly = True
        '
        'AccountingAmount
        '
        Me.AccountingAmount.HeaderText = "Accounting Amount"
        Me.AccountingAmount.Name = "AccountingAmount"
        Me.AccountingAmount.ReadOnly = True
        '
        'AccountCurrency
        '
        Me.AccountCurrency.HeaderText = "Account Currency"
        Me.AccountCurrency.Name = "AccountCurrency"
        Me.AccountCurrency.ReadOnly = True
        '
        'cmdTop
        '
        Me.cmdTop.Location = New System.Drawing.Point(703, 3)
        Me.cmdTop.Name = "cmdTop"
        Me.cmdTop.Size = New System.Drawing.Size(65, 23)
        Me.cmdTop.TabIndex = 3
        Me.cmdTop.Text = "Refresh"
        Me.cmdTop.UseVisualStyleBackColor = True
        '
        'cmdNext
        '
        Me.cmdNext.Location = New System.Drawing.Point(703, 180)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(65, 23)
        Me.cmdNext.TabIndex = 4
        Me.cmdNext.Text = "Next Page"
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'TransactionNo
        '
        Me.TransactionNo.HeaderText = "Transaction No."
        Me.TransactionNo.Name = "TransactionNo"
        Me.TransactionNo.ReadOnly = True
        Me.TransactionNo.Width = 80
        '
        'TransactionDate
        '
        Me.TransactionDate.HeaderText = "Transaction Date"
        Me.TransactionDate.Name = "TransactionDate"
        Me.TransactionDate.ReadOnly = True
        Me.TransactionDate.Width = 80
        '
        'EffDate
        '
        Me.EffDate.HeaderText = "Effective Date"
        Me.EffDate.Name = "EffDate"
        Me.EffDate.ReadOnly = True
        Me.EffDate.Width = 80
        '
        'TranCode
        '
        Me.TranCode.FillWeight = 80.0!
        Me.TranCode.HeaderText = "Tran. Code"
        Me.TranCode.Name = "TranCode"
        Me.TranCode.ReadOnly = True
        Me.TranCode.Width = 80
        '
        'TranDesc
        '
        Me.TranDesc.HeaderText = "Tran. Description"
        Me.TranDesc.Name = "TranDesc"
        Me.TranDesc.ReadOnly = True
        Me.TranDesc.Width = 250
        '
        'Reversal
        '
        Me.Reversal.FillWeight = 80.0!
        Me.Reversal.HeaderText = "Reversal"
        Me.Reversal.Name = "Reversal"
        Me.Reversal.ReadOnly = True
        Me.Reversal.Width = 80
        '
        'ctrl_TranHist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdTop)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvTranHist)
        Me.Name = "ctrl_TranHist"
        Me.Size = New System.Drawing.Size(779, 489)
        CType(Me.dgvTranHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvTranPost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvTranHist As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvTranPost As System.Windows.Forms.DataGridView
    Friend WithEvents GLCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubAcctCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubAcctType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrigAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriCurrency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccountingAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccountCurrency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdTop As System.Windows.Forms.Button
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents TransactionNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TransactionDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EffDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TranCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TranDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reversal As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
