<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrl_PaymentHist
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
        Me.dgvPaymentHist = New System.Windows.Forms.DataGridView
        Me.paymentDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PaymentType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Currency = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ReceivedAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TransactionDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubAcctCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SubAcctType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpFrDate = New System.Windows.Forms.DateTimePicker
        CType(Me.dgvPaymentHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPaymentHist
        '
        Me.dgvPaymentHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPaymentHist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.paymentDate, Me.PaymentType, Me.Currency, Me.ReceivedAmount, Me.TransactionDesc, Me.SubAcctCode, Me.SubAcctType})
        Me.dgvPaymentHist.Location = New System.Drawing.Point(3, 33)
        Me.dgvPaymentHist.Name = "dgvPaymentHist"
        Me.dgvPaymentHist.Size = New System.Drawing.Size(866, 198)
        Me.dgvPaymentHist.TabIndex = 0
        '
        'paymentDate
        '
        Me.paymentDate.HeaderText = "Date"
        Me.paymentDate.Name = "paymentDate"
        Me.paymentDate.ReadOnly = True
        Me.paymentDate.Width = 80
        '
        'PaymentType
        '
        Me.PaymentType.HeaderText = "Payment Type"
        Me.PaymentType.Name = "PaymentType"
        Me.PaymentType.ReadOnly = True
        '
        'Currency
        '
        Me.Currency.HeaderText = "Currency"
        Me.Currency.Name = "Currency"
        Me.Currency.ReadOnly = True
        Me.Currency.Width = 80
        '
        'ReceivedAmount
        '
        Me.ReceivedAmount.HeaderText = "Received Amount"
        Me.ReceivedAmount.Name = "ReceivedAmount"
        Me.ReceivedAmount.ReadOnly = True
        Me.ReceivedAmount.Width = 120
        '
        'TransactionDesc
        '
        Me.TransactionDesc.HeaderText = "Transaction Description"
        Me.TransactionDesc.Name = "TransactionDesc"
        Me.TransactionDesc.ReadOnly = True
        Me.TransactionDesc.Width = 180
        '
        'SubAcctCode
        '
        Me.SubAcctCode.HeaderText = "Sub Acct Code"
        Me.SubAcctCode.Name = "SubAcctCode"
        Me.SubAcctCode.Width = 120
        '
        'SubAcctType
        '
        Me.SubAcctType.HeaderText = "Sub Acct Type"
        Me.SubAcctType.Name = "SubAcctType"
        Me.SubAcctType.ReadOnly = True
        Me.SubAcctType.Width = 120
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Date:"
        '
        'dtpFrDate
        '
        Me.dtpFrDate.Location = New System.Drawing.Point(77, 7)
        Me.dtpFrDate.Name = "dtpFrDate"
        Me.dtpFrDate.Size = New System.Drawing.Size(187, 20)
        Me.dtpFrDate.TabIndex = 2
        '
        'ctrl_PaymentHist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dtpFrDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvPaymentHist)
        Me.Name = "ctrl_PaymentHist"
        Me.Size = New System.Drawing.Size(872, 234)
        CType(Me.dgvPaymentHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvPaymentHist As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFrDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents paymentDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Currency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReceivedAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TransactionDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubAcctCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubAcctType As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
