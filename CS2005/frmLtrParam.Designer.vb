<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLtrParam
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPolicy = New System.Windows.Forms.TextBox()
        Me.txtFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtTo = New System.Windows.Forms.DateTimePicker()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grpPrint = New System.Windows.Forms.GroupBox()
        Me.radChi = New System.Windows.Forms.RadioButton()
        Me.radEng = New System.Windows.Forms.RadioButton()
        Me.grpExport = New System.Windows.Forms.GroupBox()
        Me.radExcel = New System.Windows.Forms.RadioButton()
        Me.radWord = New System.Windows.Forms.RadioButton()
        Me.radPdf = New System.Windows.Forms.RadioButton()
        Me.grpPrint.SuspendLayout()
        Me.grpExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Policy No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Date From"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Date To"
        '
        'txtPolicy
        '
        Me.txtPolicy.Location = New System.Drawing.Point(118, 32)
        Me.txtPolicy.Name = "txtPolicy"
        Me.txtPolicy.Size = New System.Drawing.Size(153, 22)
        Me.txtPolicy.TabIndex = 3
        '
        'txtFrom
        '
        Me.txtFrom.CustomFormat = "MM/dd/yyyy HH:mm:ss"
        Me.txtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFrom.Location = New System.Drawing.Point(118, 66)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(153, 22)
        Me.txtFrom.TabIndex = 7
        '
        'txtTo
        '
        Me.txtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtTo.Location = New System.Drawing.Point(118, 103)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(153, 22)
        Me.txtTo.TabIndex = 8
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(31, 298)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(90, 27)
        Me.btnOK.TabIndex = 14
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(180, 298)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 27)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        '
        'grpPrint
        '
        Me.grpPrint.Controls.Add(Me.radChi)
        Me.grpPrint.Controls.Add(Me.radEng)
        Me.grpPrint.Location = New System.Drawing.Point(30, 135)
        Me.grpPrint.Name = "grpPrint"
        Me.grpPrint.Size = New System.Drawing.Size(241, 71)
        Me.grpPrint.TabIndex = 16
        Me.grpPrint.TabStop = False
        Me.grpPrint.Text = "Format"
        '
        'radChi
        '
        Me.radChi.AutoSize = True
        Me.radChi.Location = New System.Drawing.Point(148, 30)
        Me.radChi.Name = "radChi"
        Me.radChi.Size = New System.Drawing.Size(86, 21)
        Me.radChi.TabIndex = 12
        Me.radChi.Text = "Print Chi."
        Me.radChi.UseVisualStyleBackColor = True
        '
        'radEng
        '
        Me.radEng.AutoSize = True
        Me.radEng.Checked = True
        Me.radEng.Location = New System.Drawing.Point(28, 30)
        Me.radEng.Name = "radEng"
        Me.radEng.Size = New System.Drawing.Size(91, 21)
        Me.radEng.TabIndex = 11
        Me.radEng.TabStop = True
        Me.radEng.Text = "Print Eng."
        Me.radEng.UseVisualStyleBackColor = True
        '
        'grpExport
        '
        Me.grpExport.Controls.Add(Me.radExcel)
        Me.grpExport.Controls.Add(Me.radWord)
        Me.grpExport.Controls.Add(Me.radPdf)
        Me.grpExport.Location = New System.Drawing.Point(30, 212)
        Me.grpExport.Name = "grpExport"
        Me.grpExport.Size = New System.Drawing.Size(241, 71)
        Me.grpExport.TabIndex = 17
        Me.grpExport.TabStop = False
        Me.grpExport.Text = "Export Format"
        '
        'radExcel
        '
        Me.radExcel.AutoSize = True
        Me.radExcel.Location = New System.Drawing.Point(171, 31)
        Me.radExcel.Name = "radExcel"
        Me.radExcel.Size = New System.Drawing.Size(62, 21)
        Me.radExcel.TabIndex = 16
        Me.radExcel.Text = "Excel"
        Me.radExcel.UseVisualStyleBackColor = True
        '
        'radWord
        '
        Me.radWord.AutoSize = True
        Me.radWord.Location = New System.Drawing.Point(90, 31)
        Me.radWord.Name = "radWord"
        Me.radWord.Size = New System.Drawing.Size(63, 21)
        Me.radWord.TabIndex = 15
        Me.radWord.Text = "Word"
        Me.radWord.UseVisualStyleBackColor = True
        '
        'radPdf
        '
        Me.radPdf.AutoSize = True
        Me.radPdf.Checked = True
        Me.radPdf.Location = New System.Drawing.Point(22, 31)
        Me.radPdf.Name = "radPdf"
        Me.radPdf.Size = New System.Drawing.Size(50, 21)
        Me.radPdf.TabIndex = 14
        Me.radPdf.TabStop = True
        Me.radPdf.Text = "Pdf"
        Me.radPdf.UseVisualStyleBackColor = True
        '
        'frmLtrParam
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(303, 352)
        Me.Controls.Add(Me.grpExport)
        Me.Controls.Add(Me.grpPrint)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.txtFrom)
        Me.Controls.Add(Me.txtPolicy)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLtrParam"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLtrParam"
        Me.grpPrint.ResumeLayout(False)
        Me.grpPrint.PerformLayout()
        Me.grpExport.ResumeLayout(False)
        Me.grpExport.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtPolicy As TextBox
    Friend WithEvents txtFrom As DateTimePicker
    Friend WithEvents txtTo As DateTimePicker
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents grpPrint As GroupBox
    Friend WithEvents radChi As RadioButton
    Friend WithEvents radEng As RadioButton
    Friend WithEvents grpExport As GroupBox
    Friend WithEvents radExcel As RadioButton
    Friend WithEvents radWord As RadioButton
    Friend WithEvents radPdf As RadioButton
End Class
