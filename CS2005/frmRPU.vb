Public Class frmPHDR
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents txtPlan As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRS As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPlanTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLAge As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents txtHAge As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPlan = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRS = New System.Windows.Forms.TextBox
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPlanTitle = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtLAge = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.txtHAge = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtPlan
        '
        Me.txtPlan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPlan.Location = New System.Drawing.Point(40, 8)
        Me.txtPlan.MaxLength = 5
        Me.txtPlan.Name = "txtPlan"
        Me.txtPlan.Size = New System.Drawing.Size(56, 20)
        Me.txtPlan.TabIndex = 0
        Me.txtPlan.Text = "HAL"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Plan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(104, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "RateScale"
        '
        'txtRS
        '
        Me.txtRS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRS.Location = New System.Drawing.Point(168, 8)
        Me.txtRS.MaxLength = 1
        Me.txtRS.Name = "txtRS"
        Me.txtRS.Size = New System.Drawing.Size(24, 20)
        Me.txtRS.TabIndex = 2
        Me.txtRS.Text = "1"
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(212, 8)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(56, 20)
        Me.cmdSearch.TabIndex = 19
        Me.cmdSearch.Text = "Search"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtHAge)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TextBox5)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.TextBox4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TextBox3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtLAge)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtPlanTitle)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(300, 256)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 16)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Plan Title"
        '
        'txtPlanTitle
        '
        Me.txtPlanTitle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPlanTitle.Location = New System.Drawing.Point(64, 16)
        Me.txtPlanTitle.MaxLength = 5
        Me.txtPlanTitle.Name = "txtPlanTitle"
        Me.txtPlanTitle.Size = New System.Drawing.Size(180, 20)
        Me.txtPlanTitle.TabIndex = 28
        Me.txtPlanTitle.Text = "AECONOLIFE"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 16)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Issue Age"
        '
        'txtLAge
        '
        Me.txtLAge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLAge.Location = New System.Drawing.Point(64, 40)
        Me.txtLAge.MaxLength = 5
        Me.txtLAge.Name = "txtLAge"
        Me.txtLAge.Size = New System.Drawing.Size(28, 20)
        Me.txtLAge.TabIndex = 30
        Me.txtLAge.Text = "01"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 16)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Min.Face Amount"
        '
        'TextBox2
        '
        Me.TextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox2.Location = New System.Drawing.Point(108, 64)
        Me.TextBox2.MaxLength = 5
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(92, 20)
        Me.TextBox2.TabIndex = 32
        Me.TextBox2.Text = "120,000"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 16)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Max. Face Amount"
        '
        'TextBox3
        '
        Me.TextBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox3.Location = New System.Drawing.Point(108, 88)
        Me.TextBox3.MaxLength = 5
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(92, 20)
        Me.TextBox3.TabIndex = 34
        Me.TextBox3.Text = "999,999,999"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 116)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 16)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Mode Factor (S)"
        '
        'TextBox4
        '
        Me.TextBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox4.Location = New System.Drawing.Point(108, 112)
        Me.TextBox4.MaxLength = 5
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(92, 20)
        Me.TextBox4.TabIndex = 36
        Me.TextBox4.Text = "0.520000"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 16)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Mode Factor (M)"
        '
        'TextBox5
        '
        Me.TextBox5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox5.Location = New System.Drawing.Point(108, 136)
        Me.TextBox5.MaxLength = 5
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(92, 20)
        Me.TextBox5.TabIndex = 38
        Me.TextBox5.Text = "0.090000"
        '
        'txtHAge
        '
        Me.txtHAge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHAge.Location = New System.Drawing.Point(108, 40)
        Me.txtHAge.MaxLength = 5
        Me.txtHAge.Name = "txtHAge"
        Me.txtHAge.Size = New System.Drawing.Size(28, 20)
        Me.txtHAge.TabIndex = 44
        Me.txtHAge.Text = "70"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(96, 44)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(8, 16)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "-"
        '
        'frmPHDR
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(320, 297)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRS)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPlan)
        Me.Name = "frmPHDR"
        Me.Text = "Plan Header"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub frmIRTS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim dtIRTS As New DataTable("IRTS")
        dtIRTS.Columns.Add("EffDate", Type.GetType("System.DateTime"))
        dtIRTS.Columns.Add("Months", Type.GetType("System.String"))
        dtIRTS.Columns.Add("Days", Type.GetType("System.String"))
        dtIRTS.Columns.Add("MaxAmount", Type.GetType("System.String"))
        dtIRTS.Columns.Add("Initial", Type.GetType("System.String"))
        dtIRTS.Columns.Add("Rollover", Type.GetType("System.String"))
        dtIRTS.Columns.Add("MVAdj", Type.GetType("System.String"))

        Dim dr As DataRow
        dr = dtIRTS.NewRow
        dr.Item("EffDate") = #6/2/2003#
        dr.Item("Months") = "000"
        dr.Item("Days") = "000"
        dr.Item("MaxAmount") = "999999999"
        dr.Item("Initial") = "9.0000"
        dr.Item("Rollover") = "0.0000"
        dr.Item("MVAdj") = "0.0000"
        dtIRTS.Rows.Add(dr)

        dr = dtIRTS.NewRow
        dr.Item("EffDate") = #1/1/2002#
        dr.Item("Months") = "000"
        dr.Item("Days") = "000"
        dr.Item("MaxAmount") = "999999999"
        dr.Item("Initial") = "9.5000"
        dr.Item("Rollover") = "0.0000"
        dr.Item("MVAdj") = "0.0000"
        dtIRTS.Rows.Add(dr)

        dr = dtIRTS.NewRow
        dr.Item("EffDate") = #1/1/2001#
        dr.Item("Months") = "000"
        dr.Item("Days") = "000"
        dr.Item("MaxAmount") = "999999999"
        dr.Item("Initial") = "11.0000"
        dr.Item("Rollover") = "0.0000"
        dr.Item("MVAdj") = "0.0000"
        dtIRTS.Rows.Add(dr)

        dr = dtIRTS.NewRow
        dr.Item("EffDate") = #2/8/1999#
        dr.Item("Months") = "000"
        dr.Item("Days") = "000"
        dr.Item("MaxAmount") = "999999999"
        dr.Item("Initial") = "11.2500"
        dr.Item("Rollover") = "0.0000"
        dr.Item("MVAdj") = "0.0000"
        dtIRTS.Rows.Add(dr)

        dr = dtIRTS.NewRow
        dr.Item("EffDate") = #1/1/1980#
        dr.Item("Months") = "000"
        dr.Item("Days") = "000"
        dr.Item("MaxAmount") = "999999999"
        dr.Item("Initial") = "11.5.0000"
        dr.Item("Rollover") = "0.0000"
        dr.Item("MVAdj") = "0.0000"
        dtIRTS.Rows.Add(dr)

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "EffDate"
        cs.HeaderText = "Eff.Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Months"
        cs.HeaderText = "Months"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Days"
        cs.HeaderText = "Days"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "MaxAmount"
        cs.HeaderText = "Max.Amount"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Initial"
        cs.HeaderText = "Initial"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Rollover"
        cs.HeaderText = "Rollover"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "MVAdj"
        cs.HeaderText = "M/V Adj."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

    End Sub

End Class
