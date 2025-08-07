Imports System.Data.SqlClient

Public Class APLHist
    Inherits System.Windows.Forms.UserControl

    Dim objDS As DataSet = New DataSet("APLHistory")
    Dim dr, dr1 As DataRow
    Dim da As SqlDataAdapter
    Dim sqlConnect As New SqlConnection
    Dim datFrom As Date
    Dim strPolicy As String
    Dim dtAPLH As DataTable
    Dim WithEvents bm As BindingManagerBase

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
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdAPLHist As System.Windows.Forms.DataGrid
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents txtAction As System.Windows.Forms.TextBox
    Friend WithEvents txtOTDate As System.Windows.Forms.TextBox
    Friend WithEvents txtNTDate As System.Windows.Forms.TextBox
    Friend WithEvents txtBT As System.Windows.Forms.TextBox
    Friend WithEvents txtMode As System.Windows.Forms.TextBox
    Friend WithEvents txtOPTD As System.Windows.Forms.TextBox
    Friend WithEvents txtNPTD As System.Windows.Forms.TextBox
    Friend WithEvents txtMP As System.Windows.Forms.TextBox
    Friend WithEvents txtOldSts As System.Windows.Forms.TextBox
    Friend WithEvents txtSts As System.Windows.Forms.TextBox
    Friend WithEvents txtAPLGen As System.Windows.Forms.TextBox
    Friend WithEvents txtOAPL As System.Windows.Forms.TextBox
    Friend WithEvents txtNAPL As System.Windows.Forms.TextBox
    Friend WithEvents txtIntGen As System.Windows.Forms.TextBox
    Friend WithEvents txtIntCap As System.Windows.Forms.TextBox
    Friend WithEvents txtOInt As System.Windows.Forms.TextBox
    Friend WithEvents txtNInt As System.Windows.Forms.TextBox
    Friend WithEvents txtOpr As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblLimit As System.Windows.Forms.Label
    Friend WithEvents txtFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdGO As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.txtAction = New System.Windows.Forms.TextBox
        Me.txtOTDate = New System.Windows.Forms.TextBox
        Me.txtNTDate = New System.Windows.Forms.TextBox
        Me.txtBT = New System.Windows.Forms.TextBox
        Me.txtMode = New System.Windows.Forms.TextBox
        Me.txtOPTD = New System.Windows.Forms.TextBox
        Me.txtNPTD = New System.Windows.Forms.TextBox
        Me.txtMP = New System.Windows.Forms.TextBox
        Me.txtOldSts = New System.Windows.Forms.TextBox
        Me.txtSts = New System.Windows.Forms.TextBox
        Me.txtAPLGen = New System.Windows.Forms.TextBox
        Me.txtOAPL = New System.Windows.Forms.TextBox
        Me.txtNAPL = New System.Windows.Forms.TextBox
        Me.txtIntGen = New System.Windows.Forms.TextBox
        Me.txtIntCap = New System.Windows.Forms.TextBox
        Me.txtOInt = New System.Windows.Forms.TextBox
        Me.txtNInt = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdAPLHist = New System.Windows.Forms.DataGrid
        Me.txtOpr = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblLimit = New System.Windows.Forms.Label
        Me.txtFDate = New System.Windows.Forms.DateTimePicker
        Me.cmdGO = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        CType(Me.grdAPLHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.AutoSize = False
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDate.Location = New System.Drawing.Point(96, 220)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(81, 20)
        Me.txtDate.TabIndex = 54
        Me.txtDate.Text = ""
        '
        'txtAction
        '
        Me.txtAction.AcceptsReturn = True
        Me.txtAction.AutoSize = False
        Me.txtAction.BackColor = System.Drawing.SystemColors.Window
        Me.txtAction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAction.Location = New System.Drawing.Point(232, 220)
        Me.txtAction.MaxLength = 0
        Me.txtAction.Name = "txtAction"
        Me.txtAction.ReadOnly = True
        Me.txtAction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAction.Size = New System.Drawing.Size(81, 20)
        Me.txtAction.TabIndex = 53
        Me.txtAction.Text = ""
        '
        'txtOTDate
        '
        Me.txtOTDate.AcceptsReturn = True
        Me.txtOTDate.AutoSize = False
        Me.txtOTDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOTDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOTDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOTDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOTDate.Location = New System.Drawing.Point(428, 220)
        Me.txtOTDate.MaxLength = 0
        Me.txtOTDate.Name = "txtOTDate"
        Me.txtOTDate.ReadOnly = True
        Me.txtOTDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOTDate.Size = New System.Drawing.Size(81, 20)
        Me.txtOTDate.TabIndex = 52
        Me.txtOTDate.Text = ""
        '
        'txtNTDate
        '
        Me.txtNTDate.AcceptsReturn = True
        Me.txtNTDate.AutoSize = False
        Me.txtNTDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtNTDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNTDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNTDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNTDate.Location = New System.Drawing.Point(612, 220)
        Me.txtNTDate.MaxLength = 0
        Me.txtNTDate.Name = "txtNTDate"
        Me.txtNTDate.ReadOnly = True
        Me.txtNTDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNTDate.Size = New System.Drawing.Size(81, 20)
        Me.txtNTDate.TabIndex = 51
        Me.txtNTDate.Text = ""
        '
        'txtBT
        '
        Me.txtBT.AcceptsReturn = True
        Me.txtBT.AutoSize = False
        Me.txtBT.BackColor = System.Drawing.SystemColors.Window
        Me.txtBT.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBT.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBT.Location = New System.Drawing.Point(96, 244)
        Me.txtBT.MaxLength = 0
        Me.txtBT.Name = "txtBT"
        Me.txtBT.ReadOnly = True
        Me.txtBT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBT.Size = New System.Drawing.Size(81, 20)
        Me.txtBT.TabIndex = 50
        Me.txtBT.Text = ""
        '
        'txtMode
        '
        Me.txtMode.AcceptsReturn = True
        Me.txtMode.AutoSize = False
        Me.txtMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMode.Location = New System.Drawing.Point(232, 244)
        Me.txtMode.MaxLength = 0
        Me.txtMode.Name = "txtMode"
        Me.txtMode.ReadOnly = True
        Me.txtMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMode.Size = New System.Drawing.Size(125, 20)
        Me.txtMode.TabIndex = 49
        Me.txtMode.Text = ""
        '
        'txtOPTD
        '
        Me.txtOPTD.AcceptsReturn = True
        Me.txtOPTD.AutoSize = False
        Me.txtOPTD.BackColor = System.Drawing.SystemColors.Window
        Me.txtOPTD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOPTD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOPTD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOPTD.Location = New System.Drawing.Point(428, 244)
        Me.txtOPTD.MaxLength = 0
        Me.txtOPTD.Name = "txtOPTD"
        Me.txtOPTD.ReadOnly = True
        Me.txtOPTD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOPTD.Size = New System.Drawing.Size(81, 20)
        Me.txtOPTD.TabIndex = 48
        Me.txtOPTD.Text = ""
        '
        'txtNPTD
        '
        Me.txtNPTD.AcceptsReturn = True
        Me.txtNPTD.AutoSize = False
        Me.txtNPTD.BackColor = System.Drawing.SystemColors.Window
        Me.txtNPTD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNPTD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNPTD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNPTD.Location = New System.Drawing.Point(612, 244)
        Me.txtNPTD.MaxLength = 0
        Me.txtNPTD.Name = "txtNPTD"
        Me.txtNPTD.ReadOnly = True
        Me.txtNPTD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNPTD.Size = New System.Drawing.Size(81, 20)
        Me.txtNPTD.TabIndex = 47
        Me.txtNPTD.Text = ""
        '
        'txtMP
        '
        Me.txtMP.AcceptsReturn = True
        Me.txtMP.AutoSize = False
        Me.txtMP.BackColor = System.Drawing.SystemColors.Window
        Me.txtMP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMP.Location = New System.Drawing.Point(96, 268)
        Me.txtMP.MaxLength = 0
        Me.txtMP.Name = "txtMP"
        Me.txtMP.ReadOnly = True
        Me.txtMP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMP.Size = New System.Drawing.Size(81, 20)
        Me.txtMP.TabIndex = 46
        Me.txtMP.Text = ""
        '
        'txtOldSts
        '
        Me.txtOldSts.AcceptsReturn = True
        Me.txtOldSts.AutoSize = False
        Me.txtOldSts.BackColor = System.Drawing.SystemColors.Window
        Me.txtOldSts.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOldSts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOldSts.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOldSts.Location = New System.Drawing.Point(260, 268)
        Me.txtOldSts.MaxLength = 0
        Me.txtOldSts.Name = "txtOldSts"
        Me.txtOldSts.ReadOnly = True
        Me.txtOldSts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOldSts.Size = New System.Drawing.Size(81, 20)
        Me.txtOldSts.TabIndex = 45
        Me.txtOldSts.Text = ""
        '
        'txtSts
        '
        Me.txtSts.AcceptsReturn = True
        Me.txtSts.AutoSize = False
        Me.txtSts.BackColor = System.Drawing.SystemColors.Window
        Me.txtSts.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSts.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSts.Location = New System.Drawing.Point(428, 268)
        Me.txtSts.MaxLength = 0
        Me.txtSts.Name = "txtSts"
        Me.txtSts.ReadOnly = True
        Me.txtSts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSts.Size = New System.Drawing.Size(81, 20)
        Me.txtSts.TabIndex = 44
        Me.txtSts.Text = ""
        '
        'txtAPLGen
        '
        Me.txtAPLGen.AcceptsReturn = True
        Me.txtAPLGen.AutoSize = False
        Me.txtAPLGen.BackColor = System.Drawing.SystemColors.Window
        Me.txtAPLGen.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAPLGen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAPLGen.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAPLGen.Location = New System.Drawing.Point(96, 292)
        Me.txtAPLGen.MaxLength = 0
        Me.txtAPLGen.Name = "txtAPLGen"
        Me.txtAPLGen.ReadOnly = True
        Me.txtAPLGen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAPLGen.Size = New System.Drawing.Size(81, 20)
        Me.txtAPLGen.TabIndex = 43
        Me.txtAPLGen.Text = ""
        '
        'txtOAPL
        '
        Me.txtOAPL.AcceptsReturn = True
        Me.txtOAPL.AutoSize = False
        Me.txtOAPL.BackColor = System.Drawing.SystemColors.Window
        Me.txtOAPL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOAPL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOAPL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOAPL.Location = New System.Drawing.Point(260, 292)
        Me.txtOAPL.MaxLength = 0
        Me.txtOAPL.Name = "txtOAPL"
        Me.txtOAPL.ReadOnly = True
        Me.txtOAPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOAPL.Size = New System.Drawing.Size(81, 20)
        Me.txtOAPL.TabIndex = 42
        Me.txtOAPL.Text = ""
        '
        'txtNAPL
        '
        Me.txtNAPL.AcceptsReturn = True
        Me.txtNAPL.AutoSize = False
        Me.txtNAPL.BackColor = System.Drawing.SystemColors.Window
        Me.txtNAPL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNAPL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNAPL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNAPL.Location = New System.Drawing.Point(428, 292)
        Me.txtNAPL.MaxLength = 0
        Me.txtNAPL.Name = "txtNAPL"
        Me.txtNAPL.ReadOnly = True
        Me.txtNAPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNAPL.Size = New System.Drawing.Size(81, 20)
        Me.txtNAPL.TabIndex = 41
        Me.txtNAPL.Text = ""
        '
        'txtIntGen
        '
        Me.txtIntGen.AcceptsReturn = True
        Me.txtIntGen.AutoSize = False
        Me.txtIntGen.BackColor = System.Drawing.SystemColors.Window
        Me.txtIntGen.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIntGen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtIntGen.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIntGen.Location = New System.Drawing.Point(96, 316)
        Me.txtIntGen.MaxLength = 0
        Me.txtIntGen.Name = "txtIntGen"
        Me.txtIntGen.ReadOnly = True
        Me.txtIntGen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIntGen.Size = New System.Drawing.Size(81, 20)
        Me.txtIntGen.TabIndex = 40
        Me.txtIntGen.Text = ""
        '
        'txtIntCap
        '
        Me.txtIntCap.AcceptsReturn = True
        Me.txtIntCap.AutoSize = False
        Me.txtIntCap.BackColor = System.Drawing.SystemColors.Window
        Me.txtIntCap.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIntCap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtIntCap.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIntCap.Location = New System.Drawing.Point(260, 316)
        Me.txtIntCap.MaxLength = 0
        Me.txtIntCap.Name = "txtIntCap"
        Me.txtIntCap.ReadOnly = True
        Me.txtIntCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIntCap.Size = New System.Drawing.Size(81, 20)
        Me.txtIntCap.TabIndex = 39
        Me.txtIntCap.Text = ""
        '
        'txtOInt
        '
        Me.txtOInt.AcceptsReturn = True
        Me.txtOInt.AutoSize = False
        Me.txtOInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtOInt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOInt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOInt.Location = New System.Drawing.Point(428, 316)
        Me.txtOInt.MaxLength = 0
        Me.txtOInt.Name = "txtOInt"
        Me.txtOInt.ReadOnly = True
        Me.txtOInt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOInt.Size = New System.Drawing.Size(81, 20)
        Me.txtOInt.TabIndex = 38
        Me.txtOInt.Text = ""
        '
        'txtNInt
        '
        Me.txtNInt.AcceptsReturn = True
        Me.txtNInt.AutoSize = False
        Me.txtNInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtNInt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNInt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNInt.Location = New System.Drawing.Point(612, 316)
        Me.txtNInt.MaxLength = 0
        Me.txtNInt.Name = "txtNInt"
        Me.txtNInt.ReadOnly = True
        Me.txtNInt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNInt.Size = New System.Drawing.Size(81, 20)
        Me.txtNInt.TabIndex = 37
        Me.txtNInt.Text = ""
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(64, 224)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(28, 16)
        Me.Label18.TabIndex = 72
        Me.Label18.Text = "Date"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(192, 224)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(36, 16)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "Action"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(344, 224)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(78, 16)
        Me.Label16.TabIndex = 70
        Me.Label16.Text = "Old Tran. Date"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(524, 224)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(83, 16)
        Me.Label15.TabIndex = 69
        Me.Label15.Text = "New Tran. Date"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(44, 248)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(48, 16)
        Me.Label14.TabIndex = 68
        Me.Label14.Text = "Bill Type"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(192, 248)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(33, 16)
        Me.Label13.TabIndex = 67
        Me.Label13.Text = "Mode"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(376, 248)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(48, 16)
        Me.Label12.TabIndex = 66
        Me.Label12.Text = "Old PTD"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(556, 248)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(52, 16)
        Me.Label11.TabIndex = 65
        Me.Label11.Text = "New PTD"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(196, 272)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(57, 16)
        Me.Label10.TabIndex = 64
        Me.Label10.Text = "Old Status"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(388, 272)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(36, 16)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = "Status"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(8, 272)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(83, 16)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "Modal Premium"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(16, 296)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(73, 16)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "APL Gen/Pay"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(208, 296)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(46, 16)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "Old APL"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(372, 296)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(51, 16)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "New APL"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(20, 320)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(67, 16)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Int. Gen/Pay"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(212, 320)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(44, 16)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Int. Cap"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(380, 320)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 16)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Old Int."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(560, 320)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(46, 16)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "New Int."
        '
        'grdAPLHist
        '
        Me.grdAPLHist.AlternatingBackColor = System.Drawing.Color.White
        Me.grdAPLHist.BackColor = System.Drawing.Color.White
        Me.grdAPLHist.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAPLHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAPLHist.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAPLHist.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAPLHist.CaptionVisible = False
        Me.grdAPLHist.DataMember = ""
        Me.grdAPLHist.FlatMode = True
        Me.grdAPLHist.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdAPLHist.ForeColor = System.Drawing.Color.Black
        Me.grdAPLHist.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAPLHist.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdAPLHist.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdAPLHist.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAPLHist.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAPLHist.Location = New System.Drawing.Point(4, 28)
        Me.grdAPLHist.Name = "grdAPLHist"
        Me.grdAPLHist.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAPLHist.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAPLHist.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAPLHist.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAPLHist.Size = New System.Drawing.Size(700, 184)
        Me.grdAPLHist.TabIndex = 73
        '
        'txtOpr
        '
        Me.txtOpr.AcceptsReturn = True
        Me.txtOpr.AutoSize = False
        Me.txtOpr.BackColor = System.Drawing.SystemColors.Window
        Me.txtOpr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOpr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOpr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOpr.Location = New System.Drawing.Point(612, 268)
        Me.txtOpr.MaxLength = 0
        Me.txtOpr.Name = "txtOpr"
        Me.txtOpr.ReadOnly = True
        Me.txtOpr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOpr.Size = New System.Drawing.Size(81, 20)
        Me.txtOpr.TabIndex = 74
        Me.txtOpr.Text = ""
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(556, 272)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(49, 16)
        Me.Label19.TabIndex = 75
        Me.Label19.Text = "Operator"
        '
        'lblLimit
        '
        Me.lblLimit.ForeColor = System.Drawing.Color.Red
        Me.lblLimit.Location = New System.Drawing.Point(272, 8)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(440, 16)
        Me.lblLimit.TabIndex = 84
        '
        'txtFDate
        '
        Me.txtFDate.CustomFormat = "MMM-yyyy"
        Me.txtFDate.Enabled = False
        Me.txtFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFDate.Location = New System.Drawing.Point(104, 4)
        Me.txtFDate.Name = "txtFDate"
        Me.txtFDate.Size = New System.Drawing.Size(132, 20)
        Me.txtFDate.TabIndex = 83
        '
        'cmdGO
        '
        Me.cmdGO.Enabled = False
        Me.cmdGO.Location = New System.Drawing.Point(240, 4)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(28, 20)
        Me.cmdGO.TabIndex = 82
        Me.cmdGO.Text = "Go"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(4, 8)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(102, 16)
        Me.Label20.TabIndex = 81
        Me.Label20.Text = "Enquiry From Date:"
        '
        'APLHist
        '
        Me.Controls.Add(Me.lblLimit)
        Me.Controls.Add(Me.txtFDate)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtOpr)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.grdAPLHist)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtAction)
        Me.Controls.Add(Me.txtOTDate)
        Me.Controls.Add(Me.txtNTDate)
        Me.Controls.Add(Me.txtBT)
        Me.Controls.Add(Me.txtMode)
        Me.Controls.Add(Me.txtOPTD)
        Me.Controls.Add(Me.txtNPTD)
        Me.Controls.Add(Me.txtMP)
        Me.Controls.Add(Me.txtOldSts)
        Me.Controls.Add(Me.txtSts)
        Me.Controls.Add(Me.txtAPLGen)
        Me.Controls.Add(Me.txtOAPL)
        Me.Controls.Add(Me.txtNAPL)
        Me.Controls.Add(Me.txtIntGen)
        Me.Controls.Add(Me.txtIntCap)
        Me.Controls.Add(Me.txtOInt)
        Me.Controls.Add(Me.txtNInt)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "APLHist"
        Me.Size = New System.Drawing.Size(720, 390)
        CType(Me.grdAPLHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property PolicyAccountID()
        Get
            PolicyAccountID = strPolicy
        End Get
        Set(ByVal Value)
            If Not Value Is Nothing Then
                strPolicy = Value
            End If
        End Set
    End Property

    Public Property DateFrom()
        Get
            DateFrom = datFrom
        End Get
        Set(ByVal Value)
            datFrom = Value
        End Set
    End Property

    Public WriteOnly Property srcDT()
        Set(ByVal Value)
            If Not Value Is Nothing Then
                dtAPLH = Value
                objDS.Tables.Add(dtAPLH)
                Call BuildUI()
            End If
        End Set
    End Property

    Private Sub BuildUI()

        Dim strSQL As String
        Dim strErrNo As Integer
        Dim strErrMsg As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        'oliver added 2024-6-27 for Assurance Production Version hot fix
        If strCompany = "LAC" OrElse strCompany = "LAH" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBAsur)
        ElseIf strCompany = "MCU" Then
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDBMcu)
        Else
            serverPrefix = ConcatServerDB(gcCRSServer, gcCRSDB)
        End If

        Try
            strSQL = "Select cswmc_capsil_ModeCode, cswmc_ModeDesc from " & serverPrefix & "csw_mode_codes; "
            strSQL &= "Select BillingTypeCode, BillingTypeDesc from " & gcPOS & "vw_billingtypecodes "
            sqlConnect.ConnectionString = GetConnectionStringByCompanyID(strCompany)
            da = New SqlDataAdapter(strSQL, sqlConnect)
            da.MissingSchemaAction = MissingSchemaAction.Add
            da.MissingMappingAction = MissingMappingAction.Passthrough
            da.TableMappings.Add("csw_mode_codes1", "BillingTypeCodes")
            da.Fill(objDS, "csw_mode_codes")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        sqlConnect.Dispose()

        Dim objMode As New Data.DataRelation("Mode", objDS.Tables("csw_mode_codes").Columns("cswmc_capsil_ModeCode"), _
            objDS.Tables("APLH").Columns("cswapl_apl_mode"), True)
        Dim objBillType As New Data.DataRelation("BillType", objDS.Tables("BillingTypeCodes").Columns("BillingTypeCode"), _
            objDS.Tables("APLH").Columns("cswapl_bill_type"), True)

        Try
            objDS.Relations.Add(objMode)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            objDS.Relations.Add(objBillType)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "cswapl_act_date"
        cs.HeaderText = "Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 65
        cs.MappingName = "cswapl_act_code"
        cs.HeaderText = "Action"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 70
        cs.MappingName = "cswapl_opr_code"
        cs.HeaderText = "Operator"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "cswapl_txn_odt"
        cs.HeaderText = "Old Tran. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 90
        cs.MappingName = "cswapl_txn_ndt"
        cs.HeaderText = "New Tran. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "cswapl_sts_old"
        cs.HeaderText = "Old Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 65
        cs.MappingName = "cswapl_sts_new"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "cswapl_mode_prem"
        cs.HeaderText = "Modal Premium"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "APLH"
        grdAPLHist.TableStyles.Add(ts)

        objDS.Tables("APLH").DefaultView.Sort = "cswapl_act_date DESC, cswapl_txn_odt ASC"
        grdAPLHist.DataSource = objDS.Tables("APLH")
        grdAPLHist.AllowDrop = False
        grdAPLHist.ReadOnly = True
        bm = Me.BindingContext(objDS.Tables("APLH"))
        lblLimit.Text = CheckLimit(dtAPLH)

        Dim bDate As Binding = New Binding("text", objDS.Tables("APLH"), "cswapl_act_date")
        Me.txtDate.DataBindings.Add(bDate)
        AddHandler bDate.Format, AddressOf FormatDate

        'txtOTDate.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_txn_odt")
        Dim bOTDate As Binding = New Binding("text", objDS.Tables("APLH"), "cswapl_txn_odt")
        Me.txtOTDate.DataBindings.Add(bOTDate)
        AddHandler bOTDate.Format, AddressOf FormatDate

        'txtNTDate.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_txn_ndt")
        Dim bNTDate As Binding = New Binding("text", objDS.Tables("APLH"), "cswapl_txn_ndt")
        Me.txtNTDate.DataBindings.Add(bNTDate)
        AddHandler bNTDate.Format, AddressOf FormatDate

        'txtOPTD.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_ptd_old")
        Dim bOPTDate As Binding = New Binding("text", objDS.Tables("APLH"), "cswapl_ptd_old")
        Me.txtOPTD.DataBindings.Add(bOPTDate)
        AddHandler bOPTDate.Format, AddressOf FormatDate

        'txtNPTD.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_ptd_new")
        Dim bNPTDate As Binding = New Binding("text", objDS.Tables("APLH"), "cswapl_ptd_new")
        Me.txtNPTD.DataBindings.Add(bNPTDate)
        AddHandler bNPTDate.Format, AddressOf FormatDate

        txtAction.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_act_code")
        txtOpr.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_opr_code")
        txtOldSts.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_sts_old")
        txtSts.DataBindings.Add("text", objDS.Tables("APLH"), "cswapl_sts_new")

        Call UpdatePT()

        If bm.Count > 0 Then
            txtFDate.Enabled = True
            cmdGO.Enabled = True
        End If

    End Sub

    Private Sub UpdatePT()

        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            txtMode.Text = GetRelationValue(drI, "Mode", "cswmc_ModeDesc")
            txtBT.Text = GetRelationValue(drI, "BillType", "BillingTypeDesc")

            txtMP.Text = Format(drI.Item("cswapl_mode_prem"), gNumFormat)
            txtAPLGen.Text = Format(drI.Item("cswapl_amt_gen"), gNumFormat)
            txtOAPL.Text = Format(drI.Item("cswapl_apl_amt_old"), gNumFormat)
            txtNAPL.Text = Format(drI.Item("cswapl_apl_amt_new"), gNumFormat)
            txtIntGen.Text = Format(drI.Item("cswapl_int_gen"), gNumFormat)
            txtIntCap.Text = Format(drI.Item("cswapl_int_cap"), gNumFormat)
            txtOInt.Text = Format(drI.Item("cswapl_int_old"), gNumFormat)
            txtNInt.Text = Format(drI.Item("cswapl_int_new"), gNumFormat)

            txtFDate.Text = drI.Item("cswapl_act_date")

        End If

    End Sub

    Private Sub grdAPLHist_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAPLHist.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAPLHist.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAPLHist.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAPLHist.Select(hti.Row)
        End If
    End Sub

    Private Sub grdAPLHist_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAPLHist.CurrentCellChanged
        Call UpdatePT()
    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click
        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dt As DataTable

        wndMain.Cursor = Cursors.WaitCursor

        dt = objCS.GetAPLHistory(strPolicy, CDate(txtFDate.Text), lngErrNo, strErrMsg)
        If lngErrNo = 0 Or (lngErrNo = -1 And InStr(UCase(strErrMsg), "NO APL HISTORY RECORD FOUND") > 0) Then
            dtAPLH.Rows.Clear()

            Dim dr As DataRow
            Dim ar() As Object
            For Each dr In dt.Rows
                ar = dr.ItemArray
                dtAPLH.Rows.Add(ar)
            Next
            lblLimit.Text = CheckLimit(dtAPLH)
        End If

        If lngErrNo = -1 Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

        wndMain.Cursor = Cursors.Default

    End Sub

    'Public Sub CheckLimit()
    '    lblLimit.Text = ""
    '    If Not dtAPLH Is Nothing Then
    '        If dtAPLH.Rows.Count > 0 Then
    '            If dtAPLH.Rows(0).Item("ContFlag") = "Y" Then
    '                lblLimit.Text = "More than 100 records returned, please change ""From Date"" to view previous data."
    '            End If
    '        End If
    '    End If
    'End Sub

End Class
