Imports System.Data.SqlClient

Public Class frmPolicyAlert
    Inherits System.Windows.Forms.Form

    Private sqldt As New DataTable
    Private ds As DataSet = New DataSet("PolicyAlert")
    Private bm As BindingManagerBase
    Private lngErr As Long = 0
    Private strErr As String = ""
    Private strCurMode As String = "P"

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
    Friend WithEvents chkPolicy As System.Windows.Forms.RadioButton
    Friend WithEvents chkCust As System.Windows.Forms.RadioButton
    Friend WithEvents grdAlert As System.Windows.Forms.DataGrid
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents chkValid As System.Windows.Forms.CheckBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents lblID As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdAlert = New System.Windows.Forms.DataGrid
        Me.chkPolicy = New System.Windows.Forms.RadioButton
        Me.chkCust = New System.Windows.Forms.RadioButton
        Me.cmdClose = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.chkValid = New System.Windows.Forms.CheckBox
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.lblID = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdEdit = New System.Windows.Forms.Button
        CType(Me.grdAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdAlert
        '
        Me.grdAlert.AlternatingBackColor = System.Drawing.Color.White
        Me.grdAlert.BackColor = System.Drawing.Color.White
        Me.grdAlert.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAlert.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAlert.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAlert.CaptionVisible = False
        Me.grdAlert.DataMember = ""
        Me.grdAlert.FlatMode = True
        Me.grdAlert.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdAlert.ForeColor = System.Drawing.Color.Black
        Me.grdAlert.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAlert.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdAlert.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdAlert.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAlert.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAlert.Location = New System.Drawing.Point(4, 28)
        Me.grdAlert.Name = "grdAlert"
        Me.grdAlert.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAlert.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAlert.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAlert.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAlert.Size = New System.Drawing.Size(648, 164)
        Me.grdAlert.TabIndex = 0
        '
        'chkPolicy
        '
        Me.chkPolicy.Checked = True
        Me.chkPolicy.Location = New System.Drawing.Point(56, 4)
        Me.chkPolicy.Name = "chkPolicy"
        Me.chkPolicy.Size = New System.Drawing.Size(76, 24)
        Me.chkPolicy.TabIndex = 1
        Me.chkPolicy.TabStop = True
        Me.chkPolicy.Text = "Policy No."
        '
        'chkCust
        '
        Me.chkCust.Location = New System.Drawing.Point(144, 4)
        Me.chkCust.Name = "chkCust"
        Me.chkCust.Size = New System.Drawing.Size(88, 24)
        Me.chkCust.TabIndex = 2
        Me.chkCust.Text = "Customer ID"
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(428, 356)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "&Close"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Sort by:"
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(88, 200)
        Me.txtID.Name = "txtID"
        Me.txtID.TabIndex = 5
        Me.txtID.Text = ""
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(88, 228)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(564, 52)
        Me.txtMessage.TabIndex = 6
        Me.txtMessage.Text = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(88, 288)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(564, 52)
        Me.txtRemarks.TabIndex = 7
        Me.txtRemarks.Text = ""
        '
        'chkValid
        '
        Me.chkValid.Location = New System.Drawing.Point(420, 200)
        Me.chkValid.Name = "chkValid"
        Me.chkValid.Size = New System.Drawing.Size(64, 24)
        Me.chkValid.TabIndex = 8
        Me.chkValid.Text = "Valid"
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(292, 200)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.TabIndex = 9
        Me.txtUser.Text = ""
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(8, 204)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(55, 16)
        Me.lblID.TabIndex = 10
        Me.lblID.Text = "Policy No."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 236)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 16)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Message"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 292)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Remarks"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(240, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 16)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "User ID"
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(8, 356)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.TabIndex = 14
        Me.cmdAdd.Text = "&Add"
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(176, 356)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Text = "&Save"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(260, 356)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 16
        Me.cmdCancel.Text = "&Cancel"
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(344, 356)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.TabIndex = 17
        Me.cmdDelete.Text = "&Delete"
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(92, 356)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.TabIndex = 18
        Me.cmdEdit.Text = "&Edit"
        '
        'frmPolicyAlert
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(688, 389)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkPolicy)
        Me.Controls.Add(Me.chkValid)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.chkCust)
        Me.Controls.Add(Me.grdAlert)
        Me.Name = "frmPolicyAlert"
        Me.Text = "Policy Alert"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmPolicyAlert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter

        strSQL = "Select * from csw_customer_policy_alert "

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(ds, "csw_customer_policy_alert")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            sqlconnect.Dispose()
        End Try

        ' Default sorting sequence
        ds.Tables("csw_customer_policy_alert").DefaultView.Sort = "cswcpa_id"

        If strCurMode = "C" Then
            ds.Tables("csw_customer_policy_alert").DefaultView.RowFilter = "cswcpa_type = 'C'"
        Else
            ds.Tables("csw_customer_policy_alert").DefaultView.RowFilter = "cswcpa_type = 'P'"
        End If

        bm = Me.BindingContext(ds.Tables("csw_customer_policy_alert"))
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "cswcpa_id"
        cs.HeaderText = "Policy / Customer ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 300
        cs.MappingName = "cswcpa_message"
        cs.HeaderText = "Display Message"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 300
        cs.MappingName = "cswcpa_remark"
        cs.HeaderText = "Remarks"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "csw_customer_policy_alert"
        grdAlert.TableStyles.Add(ts)
        grdAlert.DataSource = ds.Tables("csw_customer_policy_alert")
        grdAlert.AllowDrop = False
        grdAlert.ReadOnly = True

        txtID.DataBindings.Add("text", ds.Tables("csw_customer_policy_alert"), "cswcpa_id")
        txtUser.DataBindings.Add("text", ds.Tables("csw_customer_policy_alert"), "cswcpa_usrid")
        txtMessage.DataBindings.Add("text", ds.Tables("csw_customer_policy_alert"), "cswcpa_message")
        txtRemarks.DataBindings.Add("text", ds.Tables("csw_customer_policy_alert"), "cswcpa_remark")

    End Sub

    Private Sub DataGrid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAlert.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAlert.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAlert.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAlert.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub chkPolicy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPolicy.CheckedChanged
        If strCurMode <> "P" Then
            strCurMode = "P"
            lblID.Text = "Policy No."
            ds.Tables("csw_customer_policy_alert").DefaultView.RowFilter = "cswcpa_type = 'P'"
            grdAlert.Refresh()
        End If
    End Sub

    Private Sub chkCust_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCust.CheckedChanged
        If strCurMode <> "C" Then
            strCurMode = "C"
            lblID.Text = "Customer ID"
            ds.Tables("csw_customer_policy_alert").DefaultView.RowFilter = "cswcpa_type = 'C'"
            grdAlert.Refresh()
        End If
    End Sub

    Private Sub grdAlert_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAlert.CurrentCellChanged
        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            chkValid.Checked = IIf(drI.Item("cswcpa_valid") = "T", True, False)
        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

    End Sub

End Class
