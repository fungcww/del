Public Class uclPolicyAlt_Asur
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
    Friend WithEvents cmdHistory As System.Windows.Forms.Button
    Friend WithEvents txtMarkinID As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtInDate As System.Windows.Forms.TextBox
    Friend WithEvents txtOutDate As System.Windows.Forms.TextBox
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdPolAlt = New System.Windows.Forms.DataGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtMarkinID = New System.Windows.Forms.TextBox
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtInDate = New System.Windows.Forms.TextBox
        Me.txtOutDate = New System.Windows.Forms.TextBox
        Me.txtType = New System.Windows.Forms.TextBox
        Me.txtUser = New System.Windows.Forms.TextBox
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
        Me.grdPolAlt.Size = New System.Drawing.Size(712, 188)
        Me.grdPolAlt.TabIndex = 70
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtMarkinID)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Controls.Add(Me.txtInDate)
        Me.GroupBox1.Controls.Add(Me.txtOutDate)
        Me.GroupBox1.Controls.Add(Me.txtType)
        Me.GroupBox1.Controls.Add(Me.txtUser)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label89)
        Me.GroupBox1.Controls.Add(Me.Label90)
        Me.GroupBox1.Controls.Add(Me.Label91)
        Me.GroupBox1.Controls.Add(Me.Label92)
        Me.GroupBox1.Controls.Add(Me.Label93)
        Me.GroupBox1.Controls.Add(Me.Label94)
        Me.GroupBox1.Controls.Add(Me.Label102)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 200)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(712, 88)
        Me.GroupBox1.TabIndex = 71
        Me.GroupBox1.TabStop = False
        '
        'txtMarkinID
        '
        Me.txtMarkinID.AcceptsReturn = True
        Me.txtMarkinID.AutoSize = False
        Me.txtMarkinID.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarkinID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMarkinID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMarkinID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMarkinID.Location = New System.Drawing.Point(72, 12)
        Me.txtMarkinID.MaxLength = 0
        Me.txtMarkinID.Name = "txtMarkinID"
        Me.txtMarkinID.ReadOnly = True
        Me.txtMarkinID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMarkinID.Size = New System.Drawing.Size(84, 20)
        Me.txtMarkinID.TabIndex = 82
        Me.txtMarkinID.Text = ""
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.AutoSize = False
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStatus.Location = New System.Drawing.Point(212, 12)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(220, 20)
        Me.txtStatus.TabIndex = 81
        Me.txtStatus.Text = ""
        '
        'txtInDate
        '
        Me.txtInDate.AcceptsReturn = True
        Me.txtInDate.AutoSize = False
        Me.txtInDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtInDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtInDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInDate.Location = New System.Drawing.Point(72, 36)
        Me.txtInDate.MaxLength = 0
        Me.txtInDate.Name = "txtInDate"
        Me.txtInDate.ReadOnly = True
        Me.txtInDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInDate.Size = New System.Drawing.Size(84, 20)
        Me.txtInDate.TabIndex = 80
        Me.txtInDate.Text = ""
        '
        'txtOutDate
        '
        Me.txtOutDate.AcceptsReturn = True
        Me.txtOutDate.AutoSize = False
        Me.txtOutDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOutDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOutDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOutDate.Location = New System.Drawing.Point(236, 36)
        Me.txtOutDate.MaxLength = 0
        Me.txtOutDate.Name = "txtOutDate"
        Me.txtOutDate.ReadOnly = True
        Me.txtOutDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutDate.Size = New System.Drawing.Size(81, 20)
        Me.txtOutDate.TabIndex = 79
        Me.txtOutDate.Text = ""
        '
        'txtType
        '
        Me.txtType.AcceptsReturn = True
        Me.txtType.AutoSize = False
        Me.txtType.BackColor = System.Drawing.SystemColors.Window
        Me.txtType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtType.Location = New System.Drawing.Point(488, 12)
        Me.txtType.MaxLength = 0
        Me.txtType.Name = "txtType"
        Me.txtType.ReadOnly = True
        Me.txtType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtType.Size = New System.Drawing.Size(212, 20)
        Me.txtType.TabIndex = 78
        Me.txtType.Text = ""
        '
        'txtUser
        '
        Me.txtUser.AcceptsReturn = True
        Me.txtUser.AutoSize = False
        Me.txtUser.BackColor = System.Drawing.SystemColors.Window
        Me.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtUser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUser.Location = New System.Drawing.Point(72, 60)
        Me.txtUser.MaxLength = 0
        Me.txtUser.Name = "txtUser"
        Me.txtUser.ReadOnly = True
        Me.txtUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUser.Size = New System.Drawing.Size(84, 20)
        Me.txtUser.TabIndex = 77
        Me.txtUser.Text = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(392, 36)
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
        Me.Label90.Location = New System.Drawing.Point(172, 16)
        Me.Label90.Name = "Label90"
        Me.Label90.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label90.Size = New System.Drawing.Size(36, 16)
        Me.Label90.TabIndex = 95
        Me.Label90.Text = "Status"
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.BackColor = System.Drawing.SystemColors.Control
        Me.Label91.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label91.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label91.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label91.Location = New System.Drawing.Point(24, 40)
        Me.Label91.Name = "Label91"
        Me.Label91.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label91.Size = New System.Drawing.Size(41, 16)
        Me.Label91.TabIndex = 94
        Me.Label91.Text = "In Date"
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.BackColor = System.Drawing.SystemColors.Control
        Me.Label92.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label92.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label92.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label92.Location = New System.Drawing.Point(180, 40)
        Me.Label92.Name = "Label92"
        Me.Label92.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label92.Size = New System.Drawing.Size(49, 16)
        Me.Label92.TabIndex = 93
        Me.Label92.Text = "Out Date"
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.BackColor = System.Drawing.SystemColors.Control
        Me.Label93.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label93.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label93.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label93.Location = New System.Drawing.Point(452, 16)
        Me.Label93.Name = "Label93"
        Me.Label93.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label93.Size = New System.Drawing.Size(29, 16)
        Me.Label93.TabIndex = 92
        Me.Label93.Text = "Type"
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.BackColor = System.Drawing.SystemColors.Control
        Me.Label94.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label94.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label94.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label94.Location = New System.Drawing.Point(24, 64)
        Me.Label94.Name = "Label94"
        Me.Label94.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label94.Size = New System.Drawing.Size(42, 16)
        Me.Label94.TabIndex = 91
        Me.Label94.Text = "User ID"
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.BackColor = System.Drawing.SystemColors.Control
        Me.Label102.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label102.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label102.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label102.Location = New System.Drawing.Point(336, 40)
        Me.Label102.Name = "Label102"
        Me.Label102.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label102.Size = New System.Drawing.Size(49, 16)
        Me.Label102.TabIndex = 83
        Me.Label102.Text = "Remarks"
        '
        'cmdHistory
        '
        Me.cmdHistory.Enabled = False
        Me.cmdHistory.Location = New System.Drawing.Point(560, 296)
        Me.cmdHistory.Name = "cmdHistory"
        Me.cmdHistory.TabIndex = 97
        Me.cmdHistory.Text = "History..."
        '
        'uclPolicyAlt
        '
        Me.Controls.Add(Me.grdPolAlt)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdHistory)
        Me.Name = "uclPolicyAlt"
        Me.Size = New System.Drawing.Size(728, 332)
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

        'dtPOLALT = objCS.GetMarkin(strPolicy, lngErrNo, strErrMsg)
        Dim retDs1 As DataSet = APIServiceBL.CallAPIBusi(strCompany, "CHECK_MARK_IN", New Dictionary(Of String, String)() From {{"strInPolicy", strPolicy}})
        If Not IsNothing(retDs1) Then
            If retDs1.Tables(0).Rows.Count > 0 Then
                dtPOLALT = retDs1.Tables(0)
            Else
                dtPOLALT = New DataTable
            End If
        End If

        'ITSR933 FG R3 Policy Number Change Start
        'Dim strPolicyCap As String = GetCapsilPolicyNo(strPolicy)
        'If strPolicyCap <> "" AndAlso strPolicyCap.Trim <> strPolicy.Trim Then
        '    'Dim dt As DataTable = objCS.GetMarkin(strPolicyCap, lngErrNo, strErrMsg)
        '    'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    '    dtPOLALT.Merge(dt.Copy)
        '    'End If
        '    retDs1 = New DataSet
        '    retDs1 = APIServiceBL.CallAPIBusi(getCompanyCode(), "CHECK_MARK_IN", New Dictionary(Of String, String)() From {{"strInPolicy", strPolicyCap}})
        '    If Not IsNothing(retDs1) Then
        '     If retDs1.Tables(0).Rows.Count > 0 Then
        '            dtPOLALT.Merge(retDs1.Tables(0).Copy)
        '     End If
        '    End If
        'End If
        'ITSR933 FG R3 Policy Number Change End

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
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Dim bInDate As Binding = New Binding("text", dtPOLALT, "Markin_Date")
        Me.txtInDate.DataBindings.Add(bInDate)
        AddHandler bInDate.Format, AddressOf FormatDate

        Dim bOutDate As Binding = New Binding("text", dtPOLALT, "Markout_Date")
        Me.txtOutDate.DataBindings.Add(bOutDate)
        AddHandler bOutDate.Format, AddressOf FormatDate

        txtMarkinID.DataBindings.Add("text", dtPOLALT, "Markin_ID")
        txtStatus.DataBindings.Add("text", dtPOLALT, "Status")
        txtType.DataBindings.Add("text", dtPOLALT, "Type")
        txtRemarks.DataBindings.Add("text", dtPOLALT, "Remark")
        txtUser.DataBindings.Add("text", dtPOLALT, "User_ID")

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Markin_ID"
        cs.HeaderText = "Markin ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "Status"
        cs.HeaderText = "Status"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 160
        cs.MappingName = "Type"
        cs.HeaderText = "Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Markin_Date"
        cs.HeaderText = "In Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Markout_Date"
        cs.HeaderText = "Out Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 160
        cs.MappingName = "Remark"
        cs.HeaderText = "Remarks"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "User_ID"
        cs.HeaderText = "User ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "POLALT"
        grdPolAlt.TableStyles.Add(ts)

        grdPolAlt.DataSource = dtPOLALT
        grdPolAlt.AllowDrop = False
        grdPolAlt.ReadOnly = True

    End Sub

    Private Sub grdDISC_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPolAlt.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdPolAlt.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdPolAlt.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdPolAlt.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHistory.Click

        Dim f As New frmMarkinHist
        f.PolicyAccountID = strPolicy
        f.ShowDialog()

    End Sub

End Class
