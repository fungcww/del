Public Class uclCashFlow
    Inherits System.Windows.Forms.UserControl

    Private dtCashFlow, dtPolMisc, dtCov, dtPolVal(), dtULHist As DataTable
    Private datEnqFrom As Date
    Private strPolicy As String
    Private intCovSel As Integer = 0
    Private blnBuildUI, blnBuildUI_UL As Boolean
    Private WithEvents bm As BindingManagerBase

#Region " Windows Form Designer generated code "
    Public Sub New(init As Boolean)
        MyBase.New()
    End Sub
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdGO As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCashFlow As System.Windows.Forms.TextBox
    Friend WithEvents txtWO As System.Windows.Forms.TextBox
    Friend WithEvents txtAccVal As System.Windows.Forms.TextBox
    Friend WithEvents txtCurVal As System.Windows.Forms.TextBox
    Friend WithEvents txtPlan As System.Windows.Forms.TextBox
    Friend WithEvents txtSurrVal As System.Windows.Forms.TextBox
    Friend WithEvents txtSurrChrg As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlUnit As System.Windows.Forms.TextBox
    Friend WithEvents grdCashFlow As System.Windows.Forms.DataGrid
    Friend WithEvents cboCov As System.Windows.Forms.ComboBox
    Friend WithEvents lblLimit As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grdULHist As System.Windows.Forms.DataGrid
    Friend WithEvents txtTtlCov As System.Windows.Forms.TextBox
    Friend WithEvents txtDB As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlDed As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlCSV As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlAccVal As System.Windows.Forms.TextBox
    Friend WithEvents txtTtlSI As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtCashFlow = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtWO = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtAccVal = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCurVal = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPlan = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSurrVal = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSurrChrg = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTtlUnit = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFDate = New System.Windows.Forms.DateTimePicker
        Me.cmdGO = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.grdCashFlow = New System.Windows.Forms.DataGrid
        Me.cboCov = New System.Windows.Forms.ComboBox
        Me.lblLimit = New System.Windows.Forms.Label
        Me.txtTtlCov = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtDB = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtTtlDed = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtTtlCSV = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtTtlAccVal = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtTtlSI = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.grdULHist = New System.Windows.Forms.DataGrid
        CType(Me.grdCashFlow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdULHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(8, 40)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(48, 16)
        Me.Label28.TabIndex = 113
        Me.Label28.Text = "Cov. No."
        '
        'txtCashFlow
        '
        Me.txtCashFlow.AcceptsReturn = True
        Me.txtCashFlow.AutoSize = False
        Me.txtCashFlow.BackColor = System.Drawing.SystemColors.Window
        Me.txtCashFlow.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCashFlow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCashFlow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCashFlow.Location = New System.Drawing.Point(188, 36)
        Me.txtCashFlow.MaxLength = 0
        Me.txtCashFlow.Name = "txtCashFlow"
        Me.txtCashFlow.ReadOnly = True
        Me.txtCashFlow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCashFlow.Size = New System.Drawing.Size(76, 20)
        Me.txtCashFlow.TabIndex = 114
        Me.txtCashFlow.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(124, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 115
        Me.Label1.Text = "Cash Flow"
        '
        'txtWO
        '
        Me.txtWO.AcceptsReturn = True
        Me.txtWO.AutoSize = False
        Me.txtWO.BackColor = System.Drawing.SystemColors.Window
        Me.txtWO.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtWO.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWO.Location = New System.Drawing.Point(304, 36)
        Me.txtWO.MaxLength = 0
        Me.txtWO.Name = "txtWO"
        Me.txtWO.ReadOnly = True
        Me.txtWO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWO.Size = New System.Drawing.Size(40, 20)
        Me.txtWO.TabIndex = 116
        Me.txtWO.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(276, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(24, 16)
        Me.Label2.TabIndex = 117
        Me.Label2.Text = "WO"
        '
        'txtAccVal
        '
        Me.txtAccVal.AcceptsReturn = True
        Me.txtAccVal.AutoSize = False
        Me.txtAccVal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccVal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAccVal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAccVal.Location = New System.Drawing.Point(444, 36)
        Me.txtAccVal.MaxLength = 0
        Me.txtAccVal.Name = "txtAccVal"
        Me.txtAccVal.ReadOnly = True
        Me.txtAccVal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccVal.Size = New System.Drawing.Size(80, 20)
        Me.txtAccVal.TabIndex = 118
        Me.txtAccVal.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(364, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(74, 16)
        Me.Label3.TabIndex = 119
        Me.Label3.Text = "Accum. Value"
        '
        'txtCurVal
        '
        Me.txtCurVal.AcceptsReturn = True
        Me.txtCurVal.AutoSize = False
        Me.txtCurVal.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurVal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCurVal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCurVal.Location = New System.Drawing.Point(620, 36)
        Me.txtCurVal.MaxLength = 0
        Me.txtCurVal.Name = "txtCurVal"
        Me.txtCurVal.ReadOnly = True
        Me.txtCurVal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurVal.Size = New System.Drawing.Size(88, 20)
        Me.txtCurVal.TabIndex = 120
        Me.txtCurVal.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(532, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(81, 16)
        Me.Label4.TabIndex = 121
        Me.Label4.Text = "Cur. Unit Value"
        '
        'txtPlan
        '
        Me.txtPlan.AcceptsReturn = True
        Me.txtPlan.AutoSize = False
        Me.txtPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPlan.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlan.Location = New System.Drawing.Point(60, 60)
        Me.txtPlan.MaxLength = 0
        Me.txtPlan.Name = "txtPlan"
        Me.txtPlan.ReadOnly = True
        Me.txtPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlan.Size = New System.Drawing.Size(80, 20)
        Me.txtPlan.TabIndex = 122
        Me.txtPlan.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(27, 16)
        Me.Label5.TabIndex = 123
        Me.Label5.Text = "Plan"
        '
        'txtSurrVal
        '
        Me.txtSurrVal.AcceptsReturn = True
        Me.txtSurrVal.AutoSize = False
        Me.txtSurrVal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurrVal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurrVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSurrVal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSurrVal.Location = New System.Drawing.Point(256, 60)
        Me.txtSurrVal.MaxLength = 0
        Me.txtSurrVal.Name = "txtSurrVal"
        Me.txtSurrVal.ReadOnly = True
        Me.txtSurrVal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurrVal.Size = New System.Drawing.Size(88, 20)
        Me.txtSurrVal.TabIndex = 124
        Me.txtSurrVal.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(188, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(61, 16)
        Me.Label6.TabIndex = 125
        Me.Label6.Text = "Surr. Value"
        '
        'txtSurrChrg
        '
        Me.txtSurrChrg.AcceptsReturn = True
        Me.txtSurrChrg.AutoSize = False
        Me.txtSurrChrg.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurrChrg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurrChrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSurrChrg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSurrChrg.Location = New System.Drawing.Point(444, 60)
        Me.txtSurrChrg.MaxLength = 0
        Me.txtSurrChrg.Name = "txtSurrChrg"
        Me.txtSurrChrg.ReadOnly = True
        Me.txtSurrChrg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurrChrg.Size = New System.Drawing.Size(80, 20)
        Me.txtSurrChrg.TabIndex = 126
        Me.txtSurrChrg.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(368, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(69, 16)
        Me.Label7.TabIndex = 127
        Me.Label7.Text = "Surr. Charge"
        '
        'txtTtlUnit
        '
        Me.txtTtlUnit.AcceptsReturn = True
        Me.txtTtlUnit.AutoSize = False
        Me.txtTtlUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlUnit.Location = New System.Drawing.Point(620, 60)
        Me.txtTtlUnit.MaxLength = 0
        Me.txtTtlUnit.Name = "txtTtlUnit"
        Me.txtTtlUnit.ReadOnly = True
        Me.txtTtlUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlUnit.Size = New System.Drawing.Size(88, 20)
        Me.txtTtlUnit.TabIndex = 128
        Me.txtTtlUnit.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(556, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(58, 16)
        Me.Label8.TabIndex = 129
        Me.Label8.Text = "Total Units"
        '
        'txtFDate
        '
        Me.txtFDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.txtFDate.Location = New System.Drawing.Point(108, 8)
        Me.txtFDate.Name = "txtFDate"
        Me.txtFDate.Size = New System.Drawing.Size(132, 20)
        Me.txtFDate.TabIndex = 133
        '
        'cmdGO
        '
        Me.cmdGO.Location = New System.Drawing.Point(244, 8)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(28, 20)
        Me.cmdGO.TabIndex = 132
        Me.cmdGO.Text = "Go"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(102, 16)
        Me.Label9.TabIndex = 131
        Me.Label9.Text = "Enquiry From Date:"
        '
        'grdCashFlow
        '
        Me.grdCashFlow.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCashFlow.BackColor = System.Drawing.Color.White
        Me.grdCashFlow.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCashFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCashFlow.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCashFlow.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCashFlow.CaptionVisible = False
        Me.grdCashFlow.DataMember = ""
        Me.grdCashFlow.FlatMode = True
        Me.grdCashFlow.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCashFlow.ForeColor = System.Drawing.Color.Black
        Me.grdCashFlow.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCashFlow.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCashFlow.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCashFlow.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCashFlow.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCashFlow.Location = New System.Drawing.Point(4, 84)
        Me.grdCashFlow.Name = "grdCashFlow"
        Me.grdCashFlow.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCashFlow.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCashFlow.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCashFlow.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCashFlow.Size = New System.Drawing.Size(712, 136)
        Me.grdCashFlow.TabIndex = 130
        '
        'cboCov
        '
        Me.cboCov.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCov.Location = New System.Drawing.Point(60, 36)
        Me.cboCov.Name = "cboCov"
        Me.cboCov.Size = New System.Drawing.Size(52, 21)
        Me.cboCov.TabIndex = 134
        '
        'lblLimit
        '
        Me.lblLimit.ForeColor = System.Drawing.Color.Red
        Me.lblLimit.Location = New System.Drawing.Point(276, 12)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(440, 16)
        Me.lblLimit.TabIndex = 135
        '
        'txtTtlCov
        '
        Me.txtTtlCov.AcceptsReturn = True
        Me.txtTtlCov.AutoSize = False
        Me.txtTtlCov.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlCov.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlCov.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlCov.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlCov.Location = New System.Drawing.Point(68, 232)
        Me.txtTtlCov.MaxLength = 0
        Me.txtTtlCov.Name = "txtTtlCov"
        Me.txtTtlCov.ReadOnly = True
        Me.txtTtlCov.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlCov.Size = New System.Drawing.Size(76, 20)
        Me.txtTtlCov.TabIndex = 136
        Me.txtTtlCov.Text = ""
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 236)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(56, 16)
        Me.Label10.TabIndex = 137
        Me.Label10.Text = "Total Cov."
        '
        'txtDB
        '
        Me.txtDB.AcceptsReturn = True
        Me.txtDB.AutoSize = False
        Me.txtDB.BackColor = System.Drawing.SystemColors.Window
        Me.txtDB.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDB.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDB.Location = New System.Drawing.Point(184, 232)
        Me.txtDB.MaxLength = 0
        Me.txtDB.Name = "txtDB"
        Me.txtDB.ReadOnly = True
        Me.txtDB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDB.Size = New System.Drawing.Size(76, 20)
        Me.txtDB.TabIndex = 138
        Me.txtDB.Text = ""
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(160, 236)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(20, 16)
        Me.Label11.TabIndex = 139
        Me.Label11.Text = "DB"
        '
        'txtTtlDed
        '
        Me.txtTtlDed.AcceptsReturn = True
        Me.txtTtlDed.AutoSize = False
        Me.txtTtlDed.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlDed.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlDed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlDed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlDed.Location = New System.Drawing.Point(372, 232)
        Me.txtTtlDed.MaxLength = 0
        Me.txtTtlDed.Name = "txtTtlDed"
        Me.txtTtlDed.ReadOnly = True
        Me.txtTtlDed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlDed.Size = New System.Drawing.Size(84, 20)
        Me.txtTtlDed.TabIndex = 140
        Me.txtTtlDed.Text = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(284, 236)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(84, 16)
        Me.Label12.TabIndex = 141
        Me.Label12.Text = "Total Deduction"
        '
        'txtTtlCSV
        '
        Me.txtTtlCSV.AcceptsReturn = True
        Me.txtTtlCSV.AutoSize = False
        Me.txtTtlCSV.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlCSV.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlCSV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlCSV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlCSV.Location = New System.Drawing.Point(524, 232)
        Me.txtTtlCSV.MaxLength = 0
        Me.txtTtlCSV.Name = "txtTtlCSV"
        Me.txtTtlCSV.ReadOnly = True
        Me.txtTtlCSV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlCSV.Size = New System.Drawing.Size(76, 20)
        Me.txtTtlCSV.TabIndex = 142
        Me.txtTtlCSV.Text = ""
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(464, 236)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(56, 16)
        Me.Label13.TabIndex = 143
        Me.Label13.Text = "Total CSV"
        '
        'txtTtlAccVal
        '
        Me.txtTtlAccVal.AcceptsReturn = True
        Me.txtTtlAccVal.AutoSize = False
        Me.txtTtlAccVal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlAccVal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlAccVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlAccVal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlAccVal.Location = New System.Drawing.Point(184, 256)
        Me.txtTtlAccVal.MaxLength = 0
        Me.txtTtlAccVal.Name = "txtTtlAccVal"
        Me.txtTtlAccVal.ReadOnly = True
        Me.txtTtlAccVal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlAccVal.Size = New System.Drawing.Size(76, 20)
        Me.txtTtlAccVal.TabIndex = 144
        Me.txtTtlAccVal.Text = ""
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(76, 260)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(102, 16)
        Me.Label14.TabIndex = 145
        Me.Label14.Text = "Total Accum. Value"
        '
        'txtTtlSI
        '
        Me.txtTtlSI.AcceptsReturn = True
        Me.txtTtlSI.AutoSize = False
        Me.txtTtlSI.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlSI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTtlSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTtlSI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTtlSI.Location = New System.Drawing.Point(372, 256)
        Me.txtTtlSI.MaxLength = 0
        Me.txtTtlSI.Name = "txtTtlSI"
        Me.txtTtlSI.ReadOnly = True
        Me.txtTtlSI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTtlSI.Size = New System.Drawing.Size(84, 20)
        Me.txtTtlSI.TabIndex = 146
        Me.txtTtlSI.Text = ""
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(268, 260)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(97, 16)
        Me.Label15.TabIndex = 147
        Me.Label15.Text = "Total Sum Insured"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdULHist
        '
        Me.grdULHist.AlternatingBackColor = System.Drawing.Color.White
        Me.grdULHist.BackColor = System.Drawing.Color.White
        Me.grdULHist.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdULHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdULHist.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdULHist.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdULHist.CaptionVisible = False
        Me.grdULHist.DataMember = ""
        Me.grdULHist.FlatMode = True
        Me.grdULHist.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdULHist.ForeColor = System.Drawing.Color.Black
        Me.grdULHist.GridLineColor = System.Drawing.Color.Wheat
        Me.grdULHist.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdULHist.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdULHist.HeaderForeColor = System.Drawing.Color.Black
        Me.grdULHist.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdULHist.Location = New System.Drawing.Point(8, 280)
        Me.grdULHist.Name = "grdULHist"
        Me.grdULHist.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdULHist.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdULHist.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdULHist.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdULHist.Size = New System.Drawing.Size(712, 84)
        Me.grdULHist.TabIndex = 148
        '
        'uclCashFlow
        '
        Me.Controls.Add(Me.grdULHist)
        Me.Controls.Add(Me.txtTtlSI)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtTtlAccVal)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtTtlCSV)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtTtlDed)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtDB)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtTtlCov)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblLimit)
        Me.Controls.Add(Me.cboCov)
        Me.Controls.Add(Me.txtFDate)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.grdCashFlow)
        Me.Controls.Add(Me.txtTtlUnit)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSurrChrg)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtSurrVal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPlan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtCurVal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtAccVal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtWO)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCashFlow)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label28)
        Me.Name = "uclCashFlow"
        Me.Size = New System.Drawing.Size(724, 368)
        CType(Me.grdCashFlow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdULHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property PolicyAccountID(ByVal dt As DataTable, ByVal dt1 As DataTable, ByVal dtFrom As Date)
        Get
            PolicyAccountID = strPolicy
        End Get
        Set(ByVal Value)
            If Not Value Is Nothing Then
                strPolicy = Value
                dtPolMisc = dt
                dtCov = dt1
                datEnqFrom = dtFrom
            End If
        End Set
    End Property

    Private Sub buildUI_UL()

        'UL History
        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 35
        cs.MappingName = "RiderNumber"
        cs.HeaderText = "Cov."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 25
        cs.MappingName = "StatusCode"
        cs.HeaderText = "CS"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CoSumInsured"
        cs.HeaderText = "Sum Insured"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CoNetAmtAtRisk"
        cs.HeaderText = "Net Amount"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 65
        cs.MappingName = "CoGuarCOI"
        cs.HeaderText = "Guar-COI"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CoMortChg"
        cs.HeaderText = "Mort-Charge"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 70
        cs.MappingName = "CoPeriodicLoad"
        cs.HeaderText = "Per-Loads"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "CoADPremium"
        cs.HeaderText = "AD Prem"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "CoWPPremium"
        cs.HeaderText = "WP Prem"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "CoTotalDeduction"
        cs.HeaderText = "Tot Deduct"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CoAmountDrawn"
        cs.HeaderText = "Amt. Drawn"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CoFundValue"
        cs.HeaderText = "Fund Value"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLIU2"
        grdULHist.TableStyles.Add(ts)

        grdULHist.DataSource = dtULHist
        grdULHist.AllowDrop = False
        grdULHist.ReadOnly = True

    End Sub

    Private Sub buildUI()

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 40
        cs.MappingName = "Seq"
        cs.HeaderText = "Seq."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 70
        cs.MappingName = "Amount"
        cs.HeaderText = "Amount"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 85
        cs.MappingName = "NetAmount"
        cs.HeaderText = "Net Amount"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "Status"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 40
        cs.MappingName = "Type"
        cs.HeaderText = "Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "Reason"
        cs.HeaderText = "Reason"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CurrentAmount"
        cs.HeaderText = "Current Amount"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "EffectiveDate"
        cs.HeaderText = "Eff. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 70
        cs.MappingName = "InterestRate"
        cs.HeaderText = "Int. Rate"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "NextDate"
        cs.HeaderText = "Next Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLIH"
        grdCashFlow.TableStyles.Add(ts)

        grdCashFlow.DataSource = dtCashFlow
        grdCashFlow.AllowDrop = False
        grdCashFlow.ReadOnly = True

        bm = Me.BindingContext(dtCashFlow)

    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click

        Dim lngErr As Long
        Dim strErr As String
        Dim dt, dt1() As DataTable

        wndMain.Cursor = Cursors.WaitCursor

        ' Cash Flow History
        dt = objCS.GetCFHistory(strPolicy, CStr(intCovSel + 1), datEnqFrom, txtFDate.Value, lngErr, strErr)
        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

        If Not dt Is Nothing Then
            If dtCashFlow Is Nothing Then
                dtCashFlow = dt
            Else
                dtCashFlow.Rows.Clear()
                Dim dr As DataRow
                Dim ar() As Object

                For Each dr In dt.Rows
                    ar = dr.ItemArray
                    dtCashFlow.Rows.Add(ar)
                Next
            End If

            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("ContFlag") = "Y" Then
                lblLimit.Text = "More than 100 records returned, please change ""From Date"" to view previous data."
            Else
                lblLimit.Text = ""
            End If
        End If

        If Not blnBuildUI Then
            blnBuildUI = True
            Call buildUI()
        End If

        ' UL History
        lngErr = 0
        strErr = ""
        dt1 = objCS.GetULHistory(strPolicy, txtFDate.Value, lngErr, strErr)
        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

        If Not dt1 Is Nothing Then
            If dtULHist Is Nothing Then
                dtULHist = dt1(1)
            Else
                dtULHist.Rows.Clear()
                Dim dr As DataRow
                Dim ar() As Object

                For Each dr In dt1(1).Rows
                    ar = dr.ItemArray
                    dtULHist.Rows.Add(ar)
                Next
            End If
        End If

        If Not blnBuildUI_UL Then
            blnBuildUI_UL = True
            Call buildUI_UL()
        End If

        If dt1.Length > 0 AndAlso dt1(0).Rows.Count > 0 Then
            With dt1(0).Rows(0)
                Me.txtTtlCov.Text = .Item("TotalCov")
                Me.txtDB.Text = .Item("DeathBenefitOpt")
                Me.txtTtlDed.Text = Format(.Item("TotalDeduction"), gNumFormat)
                Me.txtTtlCSV.Text = Format(.Item("TotalCSV"), gNumFormat)
                Me.txtTtlAccVal.Text = Format(.Item("TotalAccumValue"), gNumFormat)
                Me.txtTtlSI.Text = Format(.Item("TotalSumInsured"), gNumFormat)
            End With
        End If

        wndMain.Cursor = Cursors.Default

    End Sub

    Private Sub uclCashFlow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'AC - Change to use configuration setting - start
        '#If UAT = 0 Then
        'AC - Change to use configuration setting - end
        If Me.DesignMode Then
            Return
        End If

        Dim lngErr As Long
        Dim strErr As String

        If Not dtPolMisc Is Nothing Then
            For i As Integer = 0 To dtPolMisc.Rows.Count - 1
                If dtPolMisc.Rows(i).Item("PolicyRelateCode") <> "PH" Then
                    dtPolMisc.Rows(i).Delete()
                End If
            Next
            dtPolMisc.AcceptChanges()
        End If

        'set a new binding context for the combobox 
        Me.cboCov.BindingContext = New BindingContext
        Me.cboCov.DataSource = dtPolMisc
        Me.cboCov.DisplayMember = "COTRAI"

        wndMain.Cursor = Cursors.WaitCursor

        Try
            dtPolVal = objCS.GetPolicyVal(strPolicy, Today, lngErr, strErr)
        Catch ex As Exception

        End Try

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Else
            Me.txtAccVal.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COAccumVal"), gNumFormat)
            Me.txtCurVal.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COEquityLinkVal"), gNumFormat)
            Me.txtSurrVal.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COSurrenderAmt"), gNumFormat)
            Me.txtSurrChrg.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COSurrendercharge"), gNumFormat)
            Me.txtTtlUnit.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COTotalUnit"), gNumFormat)
        End If

        wndMain.Cursor = Cursors.Default

        'AC - Change to use configuration setting - start
        '#End If
        'AC - Change to use configuration setting - end

    End Sub

    Private Sub cboCov_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCov.SelectedIndexChanged

        intCovSel = cboCov.SelectedIndex
        If intCovSel >= 0 Then
            Me.txtPlan.Text = dtPolMisc.Rows(intCovSel).Item("ProductID")

            Me.txtCashFlow.Text = dtCov.Rows(intCovSel).Item("CashFlowEnt")
            Me.txtWO.Text = dtCov.Rows(intCovSel).Item("WO")

            ' VS2005 upgrade change
            'If dtPolVal.Length > 0 Then
            If Not dtPolVal Is Nothing AndAlso dtPolVal.Length > 0 Then
                Me.txtAccVal.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COAccumVal"), gNumFormat)
                Me.txtCurVal.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COEquityLinkVal"), gNumFormat)
                Me.txtSurrVal.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COSurrenderAmt"), gNumFormat)
                Me.txtSurrChrg.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COSurrendercharge"), gNumFormat)
                Me.txtTtlUnit.Text = Format(dtPolVal(3).Rows(intCovSel).Item("COTotalUnit"), gNumFormat)
            End If
        End If

    End Sub

    Private Sub DataGrid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCashFlow.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdCashFlow.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdCashFlow.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdCashFlow.Select(hti.Row)
        End If
    End Sub

    Private Sub grdULHist_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdULHist.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdULHist.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdULHist.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdULHist.Select(hti.Row)
        End If
    End Sub

    Private Sub grdCashFlow_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCashFlow.CurrentCellChanged

        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()
        txtFDate.Text = drI.Item("EffectiveDate")

    End Sub

End Class
