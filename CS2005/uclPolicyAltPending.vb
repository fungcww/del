Public Class PolicyAltPending
    Inherits System.Windows.Forms.UserControl

    Dim strPolicy As String
    Dim dtPOLALT As DataTable

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents grdPolAlt As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFinalRemind As System.Windows.Forms.TextBox
    Friend WithEvents txt1stRemind As System.Windows.Forms.TextBox
    Friend WithEvents txtDeadline As System.Windows.Forms.TextBox
    Friend WithEvents txtResolveDate As System.Windows.Forms.TextBox
    Friend WithEvents txtMarkinID As System.Windows.Forms.TextBox
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents txtPendingDate As System.Windows.Forms.TextBox
    Friend WithEvents txtResolveCode As System.Windows.Forms.TextBox
    Friend WithEvents txtIntRemark As System.Windows.Forms.TextBox
    Friend WithEvents txtPendingCode As System.Windows.Forms.TextBox
    Friend WithEvents cmdHistory As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdPolAlt = New System.Windows.Forms.DataGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtFinalRemind = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt1stRemind = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDeadline = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtResolveDate = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMarkinID = New System.Windows.Forms.TextBox
        Me.txtReason = New System.Windows.Forms.TextBox
        Me.txtPendingDate = New System.Windows.Forms.TextBox
        Me.txtResolveCode = New System.Windows.Forms.TextBox
        Me.txtIntRemark = New System.Windows.Forms.TextBox
        Me.txtPendingCode = New System.Windows.Forms.TextBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.Label89 = New System.Windows.Forms.Label
        Me.Label90 = New System.Windows.Forms.Label
        Me.Label91 = New System.Windows.Forms.Label
        Me.Label92 = New System.Windows.Forms.Label
        Me.Label93 = New System.Windows.Forms.Label
        Me.Label94 = New System.Windows.Forms.Label
        Me.Label102 = New System.Windows.Forms.Label
        Me.cmdHistory = New System.Windows.Forms.Button
        CType(Me.grdPolAlt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPolAlt
        '
        Me.grdPolAlt.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPolAlt.BackColor = System.Drawing.Color.White
        Me.grdPolAlt.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPolAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPolAlt.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolAlt.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPolAlt.CaptionVisible = False
        Me.grdPolAlt.DataMember = ""
        Me.grdPolAlt.FlatMode = True
        Me.grdPolAlt.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPolAlt.ForeColor = System.Drawing.Color.Black
        Me.grdPolAlt.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPolAlt.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPolAlt.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPolAlt.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPolAlt.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolAlt.Location = New System.Drawing.Point(8, 8)
        Me.grdPolAlt.Name = "grdPolAlt"
        Me.grdPolAlt.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPolAlt.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPolAlt.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPolAlt.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolAlt.Size = New System.Drawing.Size(712, 144)
        Me.grdPolAlt.TabIndex = 70
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtFinalRemind)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txt1stRemind)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtDeadline)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtResolveDate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtMarkinID)
        Me.GroupBox1.Controls.Add(Me.txtReason)
        Me.GroupBox1.Controls.Add(Me.txtPendingDate)
        Me.GroupBox1.Controls.Add(Me.txtResolveCode)
        Me.GroupBox1.Controls.Add(Me.txtIntRemark)
        Me.GroupBox1.Controls.Add(Me.txtPendingCode)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label89)
        Me.GroupBox1.Controls.Add(Me.Label90)
        Me.GroupBox1.Controls.Add(Me.Label91)
        Me.GroupBox1.Controls.Add(Me.Label92)
        Me.GroupBox1.Controls.Add(Me.Label93)
        Me.GroupBox1.Controls.Add(Me.Label94)
        Me.GroupBox1.Controls.Add(Me.Label102)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 156)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(712, 160)
        Me.GroupBox1.TabIndex = 71
        Me.GroupBox1.TabStop = False
        '
        'txtFinalRemind
        '
        Me.txtFinalRemind.AcceptsReturn = True
        Me.txtFinalRemind.AutoSize = False
        Me.txtFinalRemind.BackColor = System.Drawing.SystemColors.Window
        Me.txtFinalRemind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFinalRemind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFinalRemind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFinalRemind.Location = New System.Drawing.Point(292, 132)
        Me.txtFinalRemind.MaxLength = 0
        Me.txtFinalRemind.Name = "txtFinalRemind"
        Me.txtFinalRemind.ReadOnly = True
        Me.txtFinalRemind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFinalRemind.Size = New System.Drawing.Size(84, 20)
        Me.txtFinalRemind.TabIndex = 103
        Me.txtFinalRemind.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(188, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(98, 16)
        Me.Label4.TabIndex = 104
        Me.Label4.Text = "Final Remind Date"
        '
        'txt1stRemind
        '
        Me.txt1stRemind.AcceptsReturn = True
        Me.txt1stRemind.AutoSize = False
        Me.txt1stRemind.BackColor = System.Drawing.SystemColors.Window
        Me.txt1stRemind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt1stRemind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txt1stRemind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txt1stRemind.Location = New System.Drawing.Point(292, 108)
        Me.txt1stRemind.MaxLength = 0
        Me.txt1stRemind.Name = "txt1stRemind"
        Me.txt1stRemind.ReadOnly = True
        Me.txt1stRemind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txt1stRemind.Size = New System.Drawing.Size(84, 20)
        Me.txt1stRemind.TabIndex = 101
        Me.txt1stRemind.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(188, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "1st Remind Date"
        '
        'txtDeadline
        '
        Me.txtDeadline.AcceptsReturn = True
        Me.txtDeadline.AutoSize = False
        Me.txtDeadline.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeadline.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeadline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDeadline.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeadline.Location = New System.Drawing.Point(292, 84)
        Me.txtDeadline.MaxLength = 0
        Me.txtDeadline.Name = "txtDeadline"
        Me.txtDeadline.ReadOnly = True
        Me.txtDeadline.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeadline.Size = New System.Drawing.Size(84, 20)
        Me.txtDeadline.TabIndex = 99
        Me.txtDeadline.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(188, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(49, 16)
        Me.Label2.TabIndex = 100
        Me.Label2.Text = "Deadline"
        '
        'txtResolveDate
        '
        Me.txtResolveDate.AcceptsReturn = True
        Me.txtResolveDate.AutoSize = False
        Me.txtResolveDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtResolveDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResolveDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtResolveDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResolveDate.Location = New System.Drawing.Point(92, 108)
        Me.txtResolveDate.MaxLength = 0
        Me.txtResolveDate.Name = "txtResolveDate"
        Me.txtResolveDate.ReadOnly = True
        Me.txtResolveDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResolveDate.Size = New System.Drawing.Size(84, 20)
        Me.txtResolveDate.TabIndex = 97
        Me.txtResolveDate.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Resolve Date"
        '
        'txtMarkinID
        '
        Me.txtMarkinID.AcceptsReturn = True
        Me.txtMarkinID.AutoSize = False
        Me.txtMarkinID.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarkinID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMarkinID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMarkinID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMarkinID.Location = New System.Drawing.Point(92, 12)
        Me.txtMarkinID.MaxLength = 0
        Me.txtMarkinID.Name = "txtMarkinID"
        Me.txtMarkinID.ReadOnly = True
        Me.txtMarkinID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMarkinID.Size = New System.Drawing.Size(84, 20)
        Me.txtMarkinID.TabIndex = 82
        Me.txtMarkinID.Text = ""
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.AutoSize = False
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReason.Location = New System.Drawing.Point(292, 12)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.ReadOnly = True
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(412, 44)
        Me.txtReason.TabIndex = 81
        Me.txtReason.Text = ""
        '
        'txtPendingDate
        '
        Me.txtPendingDate.AcceptsReturn = True
        Me.txtPendingDate.AutoSize = False
        Me.txtPendingDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPendingDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPendingDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPendingDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPendingDate.Location = New System.Drawing.Point(92, 36)
        Me.txtPendingDate.MaxLength = 0
        Me.txtPendingDate.Name = "txtPendingDate"
        Me.txtPendingDate.ReadOnly = True
        Me.txtPendingDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPendingDate.Size = New System.Drawing.Size(84, 20)
        Me.txtPendingDate.TabIndex = 80
        Me.txtPendingDate.Text = ""
        '
        'txtResolveCode
        '
        Me.txtResolveCode.AcceptsReturn = True
        Me.txtResolveCode.AutoSize = False
        Me.txtResolveCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtResolveCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResolveCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtResolveCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResolveCode.Location = New System.Drawing.Point(92, 84)
        Me.txtResolveCode.MaxLength = 0
        Me.txtResolveCode.Name = "txtResolveCode"
        Me.txtResolveCode.ReadOnly = True
        Me.txtResolveCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResolveCode.Size = New System.Drawing.Size(84, 20)
        Me.txtResolveCode.TabIndex = 79
        Me.txtResolveCode.Text = ""
        '
        'txtIntRemark
        '
        Me.txtIntRemark.AcceptsReturn = True
        Me.txtIntRemark.AutoSize = False
        Me.txtIntRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtIntRemark.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIntRemark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtIntRemark.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIntRemark.Location = New System.Drawing.Point(476, 84)
        Me.txtIntRemark.MaxLength = 0
        Me.txtIntRemark.Name = "txtIntRemark"
        Me.txtIntRemark.ReadOnly = True
        Me.txtIntRemark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIntRemark.Size = New System.Drawing.Size(228, 64)
        Me.txtIntRemark.TabIndex = 78
        Me.txtIntRemark.Text = ""
        '
        'txtPendingCode
        '
        Me.txtPendingCode.AcceptsReturn = True
        Me.txtPendingCode.AutoSize = False
        Me.txtPendingCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPendingCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPendingCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPendingCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPendingCode.Location = New System.Drawing.Point(92, 60)
        Me.txtPendingCode.MaxLength = 0
        Me.txtPendingCode.Name = "txtPendingCode"
        Me.txtPendingCode.ReadOnly = True
        Me.txtPendingCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPendingCode.Size = New System.Drawing.Size(84, 20)
        Me.txtPendingCode.TabIndex = 77
        Me.txtPendingCode.Text = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(292, 60)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReadOnly = True
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(308, 20)
        Me.txtRemarks.TabIndex = 69
        Me.txtRemarks.Text = ""
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.BackColor = System.Drawing.SystemColors.Control
        Me.Label89.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label89.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label89.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label89.Location = New System.Drawing.Point(12, 16)
        Me.Label89.Name = "Label89"
        Me.Label89.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label89.Size = New System.Drawing.Size(53, 16)
        Me.Label89.TabIndex = 96
        Me.Label89.Text = "Markin ID"
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.BackColor = System.Drawing.SystemColors.Control
        Me.Label90.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label90.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label90.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label90.Location = New System.Drawing.Point(188, 16)
        Me.Label90.Name = "Label90"
        Me.Label90.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label90.Size = New System.Drawing.Size(88, 16)
        Me.Label90.TabIndex = 95
        Me.Label90.Text = "Pending Reason"
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.BackColor = System.Drawing.SystemColors.Control
        Me.Label91.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label91.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label91.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label91.Location = New System.Drawing.Point(12, 40)
        Me.Label91.Name = "Label91"
        Me.Label91.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label91.Size = New System.Drawing.Size(73, 16)
        Me.Label91.TabIndex = 94
        Me.Label91.Text = "Pending Date"
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.BackColor = System.Drawing.SystemColors.Control
        Me.Label92.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label92.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label92.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label92.Location = New System.Drawing.Point(12, 88)
        Me.Label92.Name = "Label92"
        Me.Label92.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label92.Size = New System.Drawing.Size(75, 16)
        Me.Label92.TabIndex = 93
        Me.Label92.Text = "Resolve Code"
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.BackColor = System.Drawing.SystemColors.Control
        Me.Label93.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label93.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label93.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label93.Location = New System.Drawing.Point(384, 88)
        Me.Label93.Name = "Label93"
        Me.Label93.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label93.Size = New System.Drawing.Size(85, 16)
        Me.Label93.TabIndex = 92
        Me.Label93.Text = "Internal Remark"
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.BackColor = System.Drawing.SystemColors.Control
        Me.Label94.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label94.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label94.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label94.Location = New System.Drawing.Point(12, 64)
        Me.Label94.Name = "Label94"
        Me.Label94.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label94.Size = New System.Drawing.Size(76, 16)
        Me.Label94.TabIndex = 91
        Me.Label94.Text = "Pending Code"
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.BackColor = System.Drawing.SystemColors.Control
        Me.Label102.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label102.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label102.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label102.Location = New System.Drawing.Point(188, 64)
        Me.Label102.Name = "Label102"
        Me.Label102.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label102.Size = New System.Drawing.Size(49, 16)
        Me.Label102.TabIndex = 83
        Me.Label102.Text = "Remarks"
        '
        'cmdHistory
        '
        Me.cmdHistory.Enabled = False
        Me.cmdHistory.Location = New System.Drawing.Point(564, 324)
        Me.cmdHistory.Name = "cmdHistory"
        Me.cmdHistory.TabIndex = 97
        Me.cmdHistory.Text = "History..."
        '
        'PolicyAltPending
        '
        Me.Controls.Add(Me.grdPolAlt)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdHistory)
        Me.Name = "PolicyAltPending"
        Me.Size = New System.Drawing.Size(728, 360)
        CType(Me.grdPolAlt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WriteOnly Property PolicyAccountID() As String
        Set(ByVal Value As String)
            strPolicy = Value
            Call BuildUI()
        End Set
    End Property

    Private Sub BuildUI()

        Dim strErrMsg As String
        Dim lngErrNo As Long

        wndMain.Cursor = Cursors.WaitCursor

        'dtPOLALT = objCS.GetPendingMarkin(strPolicy, lngErrNo, strErrMsg)
        Dim retDs1 As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(strCompany), "GET_PENDING_MARK_IN", New Dictionary(Of String, String)() From {{"strInPolicy", strPolicy}})
        If Not IsNothing(retDs1) Then
	        If retDs1.Tables(0).Rows.Count > 0 Then
		        dtPOLALT = retDs1.Tables(0)
            Else
                dtPOLALT = New DataTable
	        End If
        End If
        dtPOLALT.TableName = "POLALT"

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If Not dtPOLALT Is Nothing Then
                If dtPOLALT.Rows.Count > 0 Then
                    cmdHistory.Enabled = True
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        End If

        Dim bPDate As Binding = New Binding("text", dtPOLALT, "posp_pending_date")
        Me.txtPendingDate.DataBindings.Add(bPDate)
        AddHandler bPDate.Format, AddressOf FormatDate

        Dim bRDate As Binding = New Binding("text", dtPOLALT, "posp_resolve_date")
        Me.txtResolveDate.DataBindings.Add(bRDate)
        AddHandler bRDate.Format, AddressOf FormatDate

        Dim bDDate As Binding = New Binding("text", dtPOLALT, "posp_deadline")
        Me.txtDeadline.DataBindings.Add(bDDate)
        AddHandler bDDate.Format, AddressOf FormatDate

        Dim b1RDate As Binding = New Binding("text", dtPOLALT, "posp_first_rem_date")
        Me.txt1stRemind.DataBindings.Add(b1RDate)
        AddHandler b1RDate.Format, AddressOf FormatDate

        Dim bFRDate As Binding = New Binding("text", dtPOLALT, "posp_final_rem_date")
        Me.txtFinalRemind.DataBindings.Add(bFRDate)
        AddHandler bFRDate.Format, AddressOf FormatDate

        txtMarkinID.DataBindings.Add("text", dtPOLALT, "posp_markin_id")
        txtPendingCode.DataBindings.Add("text", dtPOLALT, "posp_pending_code")
        txtResolveCode.DataBindings.Add("text", dtPOLALT, "posp_resolve_desc")
        txtReason.DataBindings.Add("text", dtPOLALT, "posp_pending_desc")
        txtRemarks.DataBindings.Add("text", dtPOLALT, "posp_remark")
        txtIntRemark.DataBindings.Add("text", dtPOLALT, "posp_internal_remarks")

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "posp_markin_id"
        cs.HeaderText = "Markin ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "posp_pending_date"
        cs.HeaderText = "Pending Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "posp_pending_code"
        cs.HeaderText = "Pending Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "posp_pending_desc"
        cs.HeaderText = "Pending Reason"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "posp_internal_remarks"
        cs.HeaderText = "Internal Remark"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "posp_resolve_desc"
        cs.HeaderText = "Resolve Desc."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "posp_deadline"
        cs.HeaderText = "Deadline"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "posp_resolve_code"
        cs.HeaderText = "Resolve Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "posp_first_rem_date"
        cs.HeaderText = "1st Remind Date"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "posp_final_rem_date"
        cs.HeaderText = "Final Remind Date"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLALT"
        grdPolAlt.TableStyles.Add(ts)

        grdPolAlt.DataSource = dtPOLALT
        grdPolAlt.AllowDrop = False
        grdPolAlt.ReadOnly = True

    End Sub

    Private Sub cmdHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHistory.Click

        Dim f As New frmPendingHist
        f.PolicyAccountID = strPolicy
        f.ShowDialog()

    End Sub

    Private Sub grdPolAlt_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPolAlt.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdPolAlt.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdPolAlt.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdPolAlt.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
