<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceLogRetrieve
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dpkStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dpkEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbomedium = New System.Windows.Forms.ComboBox()
        Me.cboeventcat = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chknoncust = New System.Windows.Forms.CheckBox()
        Me.btnretriveserlog = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtcustid = New System.Windows.Forms.TextBox()
        Me.RadioMC = New System.Windows.Forms.RadioButton()
        Me.RadioBermuda = New System.Windows.Forms.RadioButton()
        Me.RadioAssurance = New System.Windows.Forms.RadioButton()
        Me.RadioGI = New System.Windows.Forms.RadioButton()
        Me.RadioEB = New System.Windows.Forms.RadioButton()
        Me.RadioALL = New System.Windows.Forms.RadioButton()
        Me.RadioHnw = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Start Date"
        '
        'dpkStartDate
        '
        Me.dpkStartDate.Location = New System.Drawing.Point(148, 9)
        Me.dpkStartDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dpkStartDate.Name = "dpkStartDate"
        Me.dpkStartDate.Size = New System.Drawing.Size(322, 26)
        Me.dpkStartDate.TabIndex = 1
        '
        'dpkEndDate
        '
        Me.dpkEndDate.Location = New System.Drawing.Point(148, 54)
        Me.dpkEndDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dpkEndDate.Name = "dpkEndDate"
        Me.dpkEndDate.Size = New System.Drawing.Size(322, 26)
        Me.dpkEndDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 63)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "End Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 106)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Medium"
        '
        'cbomedium
        '
        Me.cbomedium.FormattingEnabled = True
        Me.cbomedium.Location = New System.Drawing.Point(148, 94)
        Me.cbomedium.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbomedium.Name = "cbomedium"
        Me.cbomedium.Size = New System.Drawing.Size(322, 28)
        Me.cbomedium.TabIndex = 5
        '
        'cboeventcat
        '
        Me.cboeventcat.FormattingEnabled = True
        Me.cboeventcat.Location = New System.Drawing.Point(148, 137)
        Me.cboeventcat.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboeventcat.Name = "cboeventcat"
        Me.cboeventcat.Size = New System.Drawing.Size(322, 28)
        Me.cboeventcat.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 149)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Event Category"
        '
        'chknoncust
        '
        Me.chknoncust.AutoSize = True
        Me.chknoncust.Location = New System.Drawing.Point(21, 300)
        Me.chknoncust.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chknoncust.Name = "chknoncust"
        Me.chknoncust.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chknoncust.Size = New System.Drawing.Size(251, 24)
        Me.chknoncust.TabIndex = 14
        Me.chknoncust.Text = "Include Non-Customer Enquiry"
        Me.chknoncust.UseVisualStyleBackColor = True
        '
        'btnretriveserlog
        '
        Me.btnretriveserlog.Location = New System.Drawing.Point(323, 283)
        Me.btnretriveserlog.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnretriveserlog.Name = "btnretriveserlog"
        Me.btnretriveserlog.Size = New System.Drawing.Size(147, 55)
        Me.btnretriveserlog.TabIndex = 15
        Me.btnretriveserlog.Text = "Retrieve"
        Me.btnretriveserlog.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 198)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 20)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Customer ID"
        '
        'txtcustid
        '
        Me.txtcustid.Location = New System.Drawing.Point(148, 188)
        Me.txtcustid.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtcustid.Name = "txtcustid"
        Me.txtcustid.Size = New System.Drawing.Size(322, 26)
        Me.txtcustid.TabIndex = 8
        '
        'RadioMC
        '
        Me.RadioMC.AutoSize = True
        Me.RadioMC.Location = New System.Drawing.Point(255, 245)
        Me.RadioMC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioMC.Name = "RadioMC"
        Me.RadioMC.Size = New System.Drawing.Size(82, 24)
        Me.RadioMC.TabIndex = 11
        Me.RadioMC.TabStop = True
        Me.RadioMC.Text = "Macau"
        Me.RadioMC.UseVisualStyleBackColor = True
        '
        'RadioBermuda
        '
        Me.RadioBermuda.AutoSize = True
        Me.RadioBermuda.Checked = True
        Me.RadioBermuda.Location = New System.Drawing.Point(30, 245)
        Me.RadioBermuda.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioBermuda.Name = "RadioBermuda"
        Me.RadioBermuda.Size = New System.Drawing.Size(99, 24)
        Me.RadioBermuda.TabIndex = 9
        Me.RadioBermuda.TabStop = True
        Me.RadioBermuda.Text = "Bermuda"
        Me.RadioBermuda.UseVisualStyleBackColor = True
        '
        'RadioAssurance
        '
        Me.RadioAssurance.AutoSize = True
        Me.RadioAssurance.Location = New System.Drawing.Point(137, 245)
        Me.RadioAssurance.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioAssurance.Name = "RadioAssurance"
        Me.RadioAssurance.Size = New System.Drawing.Size(110, 24)
        Me.RadioAssurance.TabIndex = 10
        Me.RadioAssurance.TabStop = True
        Me.RadioAssurance.Text = "Assurance"
        Me.RadioAssurance.UseVisualStyleBackColor = True
        '
        'RadioGI
        '
        Me.RadioGI.AutoSize = True
        Me.RadioGI.Location = New System.Drawing.Point(345, 245)
        Me.RadioGI.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioGI.Name = "RadioGI"
        Me.RadioGI.Size = New System.Drawing.Size(52, 24)
        Me.RadioGI.TabIndex = 12
        Me.RadioGI.TabStop = True
        Me.RadioGI.Text = "GI"
        Me.RadioGI.UseVisualStyleBackColor = True
        '
        'RadioEB
        '
        Me.RadioEB.AutoSize = True
        Me.RadioEB.Location = New System.Drawing.Point(414, 245)
        Me.RadioEB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioEB.Name = "RadioEB"
        Me.RadioEB.Size = New System.Drawing.Size(56, 24)
        Me.RadioEB.TabIndex = 13
        Me.RadioEB.TabStop = True
        Me.RadioEB.Text = "EB"
        Me.RadioEB.UseVisualStyleBackColor = True
        '
        'RadioALL
        '
        Me.RadioALL.AutoSize = True
        Me.RadioALL.Location = New System.Drawing.Point(567, 245)
        Me.RadioALL.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioALL.Name = "RadioALL"
        Me.RadioALL.Size = New System.Drawing.Size(63, 24)
        Me.RadioALL.TabIndex = 14
        Me.RadioALL.TabStop = True
        Me.RadioALL.Text = "ALL"
        Me.RadioALL.UseVisualStyleBackColor = True
        '
        'RadioHnw
        '
        Me.RadioHnw.AutoSize = True
        Me.RadioHnw.Location = New System.Drawing.Point(477, 245)
        Me.RadioHnw.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioHnw.Name = "RadioALL"
        Me.RadioHnw.Size = New System.Drawing.Size(63, 24)
        Me.RadioHnw.TabIndex = 14
        Me.RadioHnw.TabStop = True
        Me.RadioHnw.Text = "Private"
        Me.RadioHnw.UseVisualStyleBackColor = True
        '
        'frmServiceLogRetrieve
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 557)
        Me.Controls.Add(Me.RadioHnw)
        Me.Controls.Add(Me.RadioALL)
        Me.Controls.Add(Me.RadioEB)
        Me.Controls.Add(Me.RadioGI)
        Me.Controls.Add(Me.RadioAssurance)
        Me.Controls.Add(Me.RadioMC)
        Me.Controls.Add(Me.RadioBermuda)
        Me.Controls.Add(Me.txtcustid)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnretriveserlog)
        Me.Controls.Add(Me.chknoncust)
        Me.Controls.Add(Me.cboeventcat)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cbomedium)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dpkEndDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dpkStartDate)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmServiceLogRetrieve"
        Me.Text = "Service Log Retrieve"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dpkStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpkEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbomedium As System.Windows.Forms.ComboBox
    Friend WithEvents cboeventcat As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chknoncust As System.Windows.Forms.CheckBox
    Friend WithEvents btnretriveserlog As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtcustid As System.Windows.Forms.TextBox
    Friend WithEvents RadioMC As System.Windows.Forms.RadioButton
    Friend WithEvents RadioBermuda As System.Windows.Forms.RadioButton
    Friend WithEvents RadioAssurance As System.Windows.Forms.RadioButton
    Friend WithEvents RadioGI As System.Windows.Forms.RadioButton
    Friend WithEvents RadioEB As System.Windows.Forms.RadioButton
    Friend WithEvents RadioHnw As System.Windows.Forms.RadioButton
    Friend WithEvents RadioALL As System.Windows.Forms.RadioButton
End Class
