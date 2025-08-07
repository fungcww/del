Imports System.Data.SqlClient

Public Class frmCSR
    Inherits System.Windows.Forms.Form

    Dim sqlda As SqlDataAdapter
    Dim sqlconnect As New SqlConnection
    Dim dtCSR As New DataTable
    Dim sqlcb As SqlCommandBuilder
    Dim strSQL, strMode As String

    Dim bm As BindingManagerBase

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
    Friend WithEvents grdCSR As System.Windows.Forms.DataGrid
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtID_400 As System.Windows.Forms.TextBox
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.grdCSR = New System.Windows.Forms.DataGrid
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtID_400 = New System.Windows.Forms.TextBox
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.chkActive = New System.Windows.Forms.CheckBox
        CType(Me.grdCSR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.SystemColors.Window
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(116, 236)
        Me.txtID.MaxLength = 10
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(56, 20)
        Me.txtID.TabIndex = 1
        Me.txtID.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "CSRID (Windows)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 268)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Name"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(52, 264)
        Me.txtName.MaxLength = 25
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(124, 20)
        Me.txtName.TabIndex = 3
        Me.txtName.Text = ""
        '
        'grdCSR
        '
        Me.grdCSR.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCSR.BackColor = System.Drawing.Color.White
        Me.grdCSR.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCSR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCSR.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCSR.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCSR.CaptionVisible = False
        Me.grdCSR.DataMember = ""
        Me.grdCSR.FlatMode = True
        Me.grdCSR.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCSR.ForeColor = System.Drawing.Color.Black
        Me.grdCSR.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCSR.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCSR.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCSR.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCSR.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCSR.Location = New System.Drawing.Point(8, 8)
        Me.grdCSR.Name = "grdCSR"
        Me.grdCSR.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCSR.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCSR.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCSR.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCSR.Size = New System.Drawing.Size(520, 220)
        Me.grdCSR.TabIndex = 18
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(192, 296)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(60, 23)
        Me.cmdAdd.TabIndex = 19
        Me.cmdAdd.Text = "Add"
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(464, 296)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(60, 23)
        Me.cmdDelete.TabIndex = 22
        Me.cmdDelete.Text = "Delete"
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(328, 296)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(60, 23)
        Me.cmdSave.TabIndex = 20
        Me.cmdSave.Text = "Save"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(396, 296)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(60, 23)
        Me.cmdCancel.TabIndex = 21
        Me.cmdCancel.Text = "Cancel"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(196, 268)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 16)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Chinese Name"
        '
        'txtCName
        '
        Me.txtCName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCName.Location = New System.Drawing.Point(284, 264)
        Me.txtCName.MaxLength = 25
        Me.txtCName.Name = "txtCName"
        Me.txtCName.Size = New System.Drawing.Size(124, 20)
        Me.txtCName.TabIndex = 4
        Me.txtCName.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(184, 240)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 16)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "CSRID (CAPSIL)"
        '
        'txtID_400
        '
        Me.txtID_400.BackColor = System.Drawing.SystemColors.Window
        Me.txtID_400.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID_400.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID_400.Location = New System.Drawing.Point(284, 236)
        Me.txtID_400.MaxLength = 10
        Me.txtID_400.Name = "txtID_400"
        Me.txtID_400.Size = New System.Drawing.Size(56, 20)
        Me.txtID_400.TabIndex = 2
        Me.txtID_400.Text = ""
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(260, 296)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(60, 23)
        Me.cmdEdit.TabIndex = 26
        Me.cmdEdit.Text = "Edit"
        '
        'chkActive
        '
        Me.chkActive.Location = New System.Drawing.Point(380, 236)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(60, 24)
        Me.chkActive.TabIndex = 27
        Me.chkActive.Text = "Active"
        '
        'frmCSR
        '
        Me.AcceptButton = Me.cmdAdd
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(532, 325)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtID_400)
        Me.Controls.Add(Me.txtCName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.grdCSR)
        Me.Name = "frmCSR"
        Me.Text = "CSR Maintenance"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdCSR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WriteOnly Property Editable() As Boolean
        Set(ByVal Value As Boolean)
            Me.txtID.ReadOnly = True
            Me.txtID_400.ReadOnly = Not Value
            Me.txtName.ReadOnly = Not Value
            Me.txtCName.ReadOnly = Not Value
            chkActive.Enabled = Value
            'txtID.Enabled = Value
            'txtID_400.Enabled = Value
            'txtName.Enabled = Value
            'txtCName.Enabled = Value
            'chkActive.Enabled = Value

            Me.cmdAdd.Enabled = Not Value
            Me.cmdEdit.Enabled = Not Value
            Me.cmdDelete.Enabled = Not Value
            Me.cmdSave.Enabled = Value
            Me.cmdCancel.Enabled = Value
        End Set
    End Property

    Private Sub frmIRTS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim lngErrNo As Long
        Dim strErrMsg As String

        'dtCSR = objCS.GetCSR("", "", "", lngErrNo, strErrMsg)
        dtCSR = SelectRecord()
        dtCSR.TableName = "CSR"
        bindData()
        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        'If ExternalUser Then
        '    cmdAdd.Enabled = False
        '    cmdEdit.Enabled = False
        '    cmdSave.Enabled = False
        '    cmdCancel.Enabled = False
        '    cmdDelete.Enabled = False
        'End If
    End Sub

    Private Sub bindData()

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CSRID"
        cs.HeaderText = "CSRID(W)"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "CSRID_400"
        cs.HeaderText = "CSRID(C)"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "Name"
        cs.HeaderText = "Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CName"
        cs.HeaderText = "Chinese Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "Active"
        cs.HeaderText = "Active"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CSR"
        grdCSR.TableStyles.Add(ts)

        grdCSR.DataSource = dtCSR
        grdCSR.AllowDrop = False
        grdCSR.ReadOnly = True

        bm = Me.BindingContext(dtCSR)
        RefreshCSR()

        Me.Editable = False
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
    End Sub

    Private Sub RefreshCSR()

        strMode = ""

        txtID.Text = CType(bm.Current, DataRowView).Row.Item("CSRID")
        txtID_400.Text = CType(bm.Current, DataRowView).Row.Item("CSRID_400")
        txtName.Text = CType(bm.Current, DataRowView).Row.Item("Name")
        If Not IsDBNull(CType(bm.Current, DataRowView).Row.Item("CName")) Then
            txtCName.Text = CType(bm.Current, DataRowView).Row.Item("CName")
        Else
            txtCName.Text = ""
        End If

        If Not IsDBNull(CType(bm.Current, DataRowView).Row.Item("Active")) Then
            If Trim(CType(bm.Current, DataRowView).Row.Item("Active")) = "Y" Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If
        Else
            chkActive.Checked = False
        End If

    End Sub

    Private Function SelectRecord() As DataTable

        Dim dtTemp As New DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "select CSRID, CSRID_400, Name, CName, Active from " & serverPrefix & "csr"
        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dtTemp)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        sqlconnect.Dispose()
        Return dtTemp

    End Function

    Private Sub grdCSR_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCSR.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdCSR.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdCSR.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdCSR.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Me.txtID.Text = ""
        Me.txtID_400.Text = ""
        Me.txtName.Text = ""
        Me.txtCName.Text = ""
        chkActive.Checked = True
        Me.Editable = True
        Me.txtID.ReadOnly = False
        strMode = "A"
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        If MsgBox("Do you want to delete the selected record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gSystem) = MsgBoxResult.Yes Then
            CType(bm.Current, DataRowView).Row.Delete()

            'update database
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlcb = New SqlCommandBuilder(sqlda)
            Try
                sqlda.Update(dtCSR)
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try

            MsgBox("Record deleted successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "CSR Maintenance")
            wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

        End If

    End Sub

    Private Sub grdCSR_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCSR.CurrentCellChanged
        Call RefreshCSR()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim strSQL As String
        Dim lngErrNo As Long
        Dim strErrMsg As String
        Dim sqlcmd As SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'validate missing input
        If txtID.Text = "" OrElse txtName.Text = "" OrElse txtID_400.Text = "" Then
            MsgBox("Missing Information.", MsgBoxStyle.OKOnly + MsgBoxStyle.Exclamation, "Error")
            If txtID.Text = "" Then
                'txtID.Focus()
            ElseIf txtID_400.Text = "" Then
                txtID_400.Focus()
            Else
                txtName.Focus()
            End If
            Exit Sub
        End If

        'check if the record already exists
        If strMode = "A" Then
            For Each dr As DataRow In dtCSR.Rows
                If dr.Item("CSRID").toUpper = txtID.Text OrElse dr.Item("CSRID_400").toUpper = txtID_400.Text Then
                    MsgBox("Record already exists.", MsgBoxStyle.OKOnly + MsgBoxStyle.Exclamation, "Error")
                    Exit Sub
                End If
            Next

            'SQL2008
            strSQL = "Insert into " & serverPrefix & "CSR (CSRID, CSRID_400, Name, CName, Active) " &
                " Select '" & Trim(txtID.Text) & "','" & Trim(txtID_400.Text) & "','" & Trim(txtName.Text) & "',N'" & Trim(txtCName.Text) &
                "','" & IIf(chkActive.Checked = True, "Y", "") & "'"
        Else
            'SQL2008
            strSQL = "Update " & serverPrefix & "CSR " &
                " Set CSRID_400 = '" & Trim(txtID_400.Text) & "', " &
                "     Name = '" & Trim(txtName.Text) & "', " &
                "     CName = N'" & Trim(txtCName.Text) & "', " &
                "     Active = '" & IIf(chkActive.Checked = True, "Y", "") & "' " &
                " Where csrid = '" & Trim(txtID.Text) & "'"
        End If

        Try
            sqlconnect.ConnectionString = strCIWConn
            sqlconnect.Open()
            sqlcmd = New SqlCommand(strSQL, sqlconnect)
            sqlcmd.ExecuteNonQuery()

        Catch sqlex As SqlException
            lngErrNo = -1
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            lngErrNo = -1
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            sqlconnect.Close()
        End Try

        'Me.grdCSR.Refresh()
        Dim dtTemp As DataTable
        dtCSR.Clear()
        dtTemp = SelectRecord()
        Dim dr1 As DataRow
        Dim ar() As Object
        For Each dr1 In dtTemp.Rows
            ar = dr1.ItemArray
            dtCSR.Rows.Add(ar)
        Next
        dtCSR.AcceptChanges()

        Me.Editable = False
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

        '' Update to CAPSIL USERPF
        'If lngErrNo = 0 Then
        '    wndMain.Cursor = Cursors.WaitCursor
        '    If Not objCS.UpdateCSR(txtID.Text, txtName.Text, txtCName.Text, lngErrNo, strErrMsg) Then
        '        MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        '    Else
        '        MsgBox("Record saved successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "CSR Maintenance")
        '    End If
        '    wndMain.Cursor = Cursors.Default
        'End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Me.Editable = False
        Call RefreshCSR()

    End Sub

    Private Sub txtID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.LostFocus
        If txtID_400.Text = "" Then
            txtID_400.Text = txtID.Text
        End If
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        Me.Editable = True
        strMode = "E"
    End Sub

End Class
