<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDirectDebitEnq
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDirectDebitEnq))
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtClientNo = New System.Windows.Forms.TextBox
        Me.Ctrl_DirectDebitEnq1 = New CRS_Ctrl.ctrl_DirectDebitEnq
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(648, 553)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 25)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtClientNo
        '
        Me.txtClientNo.Location = New System.Drawing.Point(74, 12)
        Me.txtClientNo.Name = "txtClientNo"
        Me.txtClientNo.Size = New System.Drawing.Size(100, 20)
        Me.txtClientNo.TabIndex = 2
        '
        'Ctrl_DirectDebitEnq1
        '
        Me.Ctrl_DirectDebitEnq1.ClientNoInUse = ""
        Me.Ctrl_DirectDebitEnq1.Location = New System.Drawing.Point(3, 38)
        Me.Ctrl_DirectDebitEnq1.Name = "Ctrl_DirectDebitEnq1"
        Me.Ctrl_DirectDebitEnq1.Size = New System.Drawing.Size(779, 509)
        Me.Ctrl_DirectDebitEnq1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Client No.:"
        '
        'frmDirectDebitEnq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 583)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtClientNo)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Ctrl_DirectDebitEnq1)
        Me.Name = "frmDirectDebitEnq"
        Me.Text = "Direct Debit Enquiry"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Ctrl_DirectDebitEnq1 As CRS_Ctrl.ctrl_DirectDebitEnq
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtClientNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
