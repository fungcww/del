Public Class frmPAYHRpt
    Inherits System.Windows.Forms.Form

    Private datFrom, datTo As Date
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Private strPolicy As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPolicy As System.Windows.Forms.TextBox
    Friend WithEvents txtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents chkChi As System.Windows.Forms.CheckBox
    Friend WithEvents chkEng As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPolicy = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFrom = New System.Windows.Forms.DateTimePicker
        Me.txtTo = New System.Windows.Forms.DateTimePicker
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.chkChi = New System.Windows.Forms.CheckBox
        Me.chkEng = New System.Windows.Forms.CheckBox
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'txtPolicy
        '
        Me.txtPolicy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPolicy.Location = New System.Drawing.Point(84, 24)
        Me.txtPolicy.MaxLength = 10
        Me.txtPolicy.Name = "txtPolicy"
        Me.txtPolicy.Size = New System.Drawing.Size(116, 20)
        Me.txtPolicy.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Policy No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Date From"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(60, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "to"
        '
        'txtFrom
        '
        Me.txtFrom.CustomFormat = "MM/dd/yyyy HH:mm:ss"
        Me.txtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFrom.Location = New System.Drawing.Point(84, 56)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(128, 20)
        Me.txtFrom.TabIndex = 6
        '
        'txtTo
        '
        Me.txtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtTo.Location = New System.Drawing.Point(84, 88)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(128, 20)
        Me.txtTo.TabIndex = 7
        '
        'cmdOK
        '
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Location = New System.Drawing.Point(40, 178)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 8
        Me.cmdOK.Text = "OK"
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(124, 178)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 9
        Me.cmdCancel.Text = "Cancel"
        '
        'chkChi
        '
        Me.chkChi.Location = New System.Drawing.Point(124, 116)
        Me.chkChi.Name = "chkChi"
        Me.chkChi.Size = New System.Drawing.Size(88, 24)
        Me.chkChi.TabIndex = 10
        Me.chkChi.Text = "Print Chi."
        '
        'chkEng
        '
        Me.chkEng.Checked = True
        Me.chkEng.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEng.Location = New System.Drawing.Point(19, 116)
        Me.chkEng.Name = "chkEng"
        Me.chkEng.Size = New System.Drawing.Size(84, 24)
        Me.chkEng.TabIndex = 11
        Me.chkEng.Text = "Print Eng."
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Enabled = False
        Me.RadioButton1.Location = New System.Drawing.Point(19, 146)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(97, 17)
        Me.RadioButton1.TabIndex = 12
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "RPU Quotation"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Enabled = False
        Me.RadioButton2.Location = New System.Drawing.Point(122, 146)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(171, 17)
        Me.RadioButton2.TabIndex = 13
        Me.RadioButton2.Text = "RPU Projection (Life/Asia only)"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'frmPAYHRpt
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(297, 213)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.chkEng)
        Me.Controls.Add(Me.chkChi)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.txtFrom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPolicy)
        Me.Name = "frmPAYHRpt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public ReadOnly Property FromDate() As Date
        Get
            Return datFrom
        End Get
    End Property

    Public ReadOnly Property ToDate() As Date
        Get
            Return datTo
        End Get
    End Property

    Public ReadOnly Property PolicyNo() As String
        Get
            Return strPolicy
        End Get
    End Property

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        datFrom = CDate(txtFrom.Text)
        datTo = CDate(txtTo.Text)
        strPolicy = txtPolicy.Text
        Me.Close()
    End Sub

    Private Sub frmPAYHRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
