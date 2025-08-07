Imports System.Data.SqlClient

Public Class frmCustRel
    '    Inherits System.Windows.Forms.Form

    '    Dim sqlda As SqlDataAdapter
    '    Dim sqlconnect As New SqlConnection
    '    Dim sqlcb As SqlCommandBuilder
    '    Dim strSQL, strCurCust As String

    '    Dim bm As BindingManagerBase

    '#Region " Windows Form Designer generated code "

    '    Public Sub New()
    '        MyBase.New()

    '        'This call is required by the Windows Form Designer.
    '        InitializeComponent()

    '        'Add any initialization after the InitializeComponent() call

    '    End Sub

    '    'Form overrides dispose to clean up the component list.
    '    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '        If disposing Then
    '            If Not (components Is Nothing) Then
    '                components.Dispose()
    '            End If
    '        End If
    '        MyBase.Dispose(disposing)
    '    End Sub

    '    'Required by the Windows Form Designer
    '    Private components As System.ComponentModel.IContainer

    '    'NOTE: The following procedure is required by the Windows Form Designer
    '    'It can be modified using the Windows Form Designer.  
    '    'Do not modify it using the code editor.
    '    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    '    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    '    Friend WithEvents Label4 As System.Windows.Forms.Label
    '    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    '    Friend WithEvents Label8 As System.Windows.Forms.Label
    '    Friend WithEvents Label6 As System.Windows.Forms.Label
    '    Friend WithEvents Label5 As System.Windows.Forms.Label
    '    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    '    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    '    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    '    Friend WithEvents Label1 As System.Windows.Forms.Label
    '    Friend WithEvents Label3 As System.Windows.Forms.Label
    '    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    '    Friend WithEvents Label2 As System.Windows.Forms.Label
    '    Friend WithEvents Button1 As System.Windows.Forms.Button
    '    Friend WithEvents Button2 As System.Windows.Forms.Button
    '    Friend WithEvents Button6 As System.Windows.Forms.Button
    '    Friend WithEvents Button3 As System.Windows.Forms.Button
    '    Friend WithEvents Button4 As System.Windows.Forms.Button
    '    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    '        Me.GroupBox1 = New System.Windows.Forms.GroupBox
    '        Me.Panel1 = New System.Windows.Forms.Panel
    '        Me.Label4 = New System.Windows.Forms.Label
    '        Me.TextBox3 = New System.Windows.Forms.TextBox
    '        Me.Label8 = New System.Windows.Forms.Label
    '        Me.Label6 = New System.Windows.Forms.Label
    '        Me.Label5 = New System.Windows.Forms.Label
    '        Me.TextBox4 = New System.Windows.Forms.TextBox
    '        Me.TextBox2 = New System.Windows.Forms.TextBox
    '        Me.TextBox1 = New System.Windows.Forms.TextBox
    '        Me.Label1 = New System.Windows.Forms.Label
    '        Me.Label3 = New System.Windows.Forms.Label
    '        Me.ComboBox1 = New System.Windows.Forms.ComboBox
    '        Me.Label2 = New System.Windows.Forms.Label
    '        Me.Button1 = New System.Windows.Forms.Button
    '        Me.Button2 = New System.Windows.Forms.Button
    '        Me.Button6 = New System.Windows.Forms.Button
    '        Me.Button3 = New System.Windows.Forms.Button
    '        Me.Button4 = New System.Windows.Forms.Button
    '        Me.GroupBox1.SuspendLayout()
    '        Me.Panel1.SuspendLayout()
    '        Me.SuspendLayout()
    '        '
    '        'GroupBox1
    '        '
    '        Me.GroupBox1.Controls.Add(Me.Panel1)
    '        Me.GroupBox1.Controls.Add(Me.Label1)
    '        Me.GroupBox1.Controls.Add(Me.Label3)
    '        Me.GroupBox1.Controls.Add(Me.ComboBox1)
    '        Me.GroupBox1.Controls.Add(Me.Label2)
    '        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
    '        Me.GroupBox1.Name = "GroupBox1"
    '        Me.GroupBox1.Size = New System.Drawing.Size(600, 116)
    '        Me.GroupBox1.TabIndex = 96
    '        Me.GroupBox1.TabStop = False
    '        Me.GroupBox1.Text = "Relationship"
    '        '
    '        'Panel1
    '        '
    '        Me.Panel1.Controls.Add(Me.Label4)
    '        Me.Panel1.Controls.Add(Me.TextBox3)
    '        Me.Panel1.Controls.Add(Me.Label8)
    '        Me.Panel1.Controls.Add(Me.Label6)
    '        Me.Panel1.Controls.Add(Me.Label5)
    '        Me.Panel1.Controls.Add(Me.TextBox4)
    '        Me.Panel1.Controls.Add(Me.TextBox2)
    '        Me.Panel1.Controls.Add(Me.TextBox1)
    '        Me.Panel1.Location = New System.Drawing.Point(8, 16)
    '        Me.Panel1.Name = "Panel1"
    '        Me.Panel1.Size = New System.Drawing.Size(260, 92)
    '        Me.Panel1.TabIndex = 44
    '        '
    '        'Label4
    '        '
    '        Me.Label4.AutoSize = True
    '        Me.Label4.Location = New System.Drawing.Point(12, 68)
    '        Me.Label4.Name = "Label4"
    '        Me.Label4.Size = New System.Drawing.Size(32, 16)
    '        Me.Label4.TabIndex = 53
    '        Me.Label4.Text = "DOB:"
    '        '
    '        'TextBox3
    '        '
    '        Me.TextBox3.Location = New System.Drawing.Point(52, 64)
    '        Me.TextBox3.Name = "TextBox3"
    '        Me.TextBox3.TabIndex = 52
    '        Me.TextBox3.Text = ""
    '        '
    '        'Label8
    '        '
    '        Me.Label8.AutoSize = True
    '        Me.Label8.ForeColor = System.Drawing.Color.Red
    '        Me.Label8.Location = New System.Drawing.Point(4, 16)
    '        Me.Label8.Name = "Label8"
    '        Me.Label8.Size = New System.Drawing.Size(42, 16)
    '        Me.Label8.TabIndex = 51
    '        Me.Label8.Text = "CustID:"
    '        '
    '        'Label6
    '        '
    '        Me.Label6.AutoSize = True
    '        Me.Label6.ForeColor = System.Drawing.Color.Red
    '        Me.Label6.Location = New System.Drawing.Point(144, 16)
    '        Me.Label6.Name = "Label6"
    '        Me.Label6.Size = New System.Drawing.Size(19, 16)
    '        Me.Label6.TabIndex = 49
    '        Me.Label6.Text = "ID:"
    '        '
    '        'Label5
    '        '
    '        Me.Label5.AutoSize = True
    '        Me.Label5.Location = New System.Drawing.Point(8, 40)
    '        Me.Label5.Name = "Label5"
    '        Me.Label5.Size = New System.Drawing.Size(38, 16)
    '        Me.Label5.TabIndex = 48
    '        Me.Label5.Text = "Name:"
    '        '
    '        'TextBox4
    '        '
    '        Me.TextBox4.Location = New System.Drawing.Point(168, 12)
    '        Me.TextBox4.Name = "TextBox4"
    '        Me.TextBox4.Size = New System.Drawing.Size(84, 20)
    '        Me.TextBox4.TabIndex = 46
    '        Me.TextBox4.Text = ""
    '        '
    '        'TextBox2
    '        '
    '        Me.TextBox2.Location = New System.Drawing.Point(52, 36)
    '        Me.TextBox2.Name = "TextBox2"
    '        Me.TextBox2.Size = New System.Drawing.Size(200, 20)
    '        Me.TextBox2.TabIndex = 45
    '        Me.TextBox2.Text = ""
    '        '
    '        'TextBox1
    '        '
    '        Me.TextBox1.Location = New System.Drawing.Point(52, 12)
    '        Me.TextBox1.Name = "TextBox1"
    '        Me.TextBox1.Size = New System.Drawing.Size(80, 20)
    '        Me.TextBox1.TabIndex = 44
    '        Me.TextBox1.Text = ""
    '        '
    '        'Label1
    '        '
    '        Me.Label1.AutoSize = True
    '        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '        Me.Label1.Location = New System.Drawing.Point(440, 48)
    '        Me.Label1.Name = "Label1"
    '        Me.Label1.Size = New System.Drawing.Size(87, 19)
    '        Me.Label1.TabIndex = 34
    '        Me.Label1.Text = "CHAN TAI MAN"
    '        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '        '
    '        'Label3
    '        '
    '        Me.Label3.AutoSize = True
    '        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
    '        Me.Label3.Location = New System.Drawing.Point(272, 52)
    '        Me.Label3.Name = "Label3"
    '        Me.Label3.Size = New System.Drawing.Size(13, 16)
    '        Me.Label3.TabIndex = 30
    '        Me.Label3.Text = "is"
    '        '
    '        'ComboBox1
    '        '
    '        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    '        Me.ComboBox1.Items.AddRange(New Object() {"Parent (PR)", "Daughter/Son (DS)", "Spouse (SP)", "Employer (ER)"})
    '        Me.ComboBox1.Location = New System.Drawing.Point(292, 48)
    '        Me.ComboBox1.Name = "ComboBox1"
    '        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
    '        Me.ComboBox1.TabIndex = 29
    '        '
    '        'Label2
    '        '
    '        Me.Label2.AutoSize = True
    '        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
    '        Me.Label2.Location = New System.Drawing.Point(420, 52)
    '        Me.Label2.Name = "Label2"
    '        Me.Label2.Size = New System.Drawing.Size(14, 16)
    '        Me.Label2.TabIndex = 28
    '        Me.Label2.Text = "of"
    '        '
    '        'Button1
    '        '
    '        Me.Button1.Location = New System.Drawing.Point(208, 128)
    '        Me.Button1.Name = "Button1"
    '        Me.Button1.TabIndex = 97
    '        Me.Button1.Text = "&Add"
    '        '
    '        'Button2
    '        '
    '        Me.Button2.Location = New System.Drawing.Point(368, 128)
    '        Me.Button2.Name = "Button2"
    '        Me.Button2.TabIndex = 98
    '        Me.Button2.Text = "&Delete"
    '        '
    '        'Button6
    '        '
    '        Me.Button6.Location = New System.Drawing.Point(288, 128)
    '        Me.Button6.Name = "Button6"
    '        Me.Button6.TabIndex = 101
    '        Me.Button6.Text = "&Edit"
    '        '
    '        'Button3
    '        '
    '        Me.Button3.Location = New System.Drawing.Point(528, 128)
    '        Me.Button3.Name = "Button3"
    '        Me.Button3.TabIndex = 99
    '        Me.Button3.Text = "&Cancel"
    '        '
    '        'Button4
    '        '
    '        Me.Button4.Location = New System.Drawing.Point(448, 128)
    '        Me.Button4.Name = "Button4"
    '        Me.Button4.TabIndex = 100
    '        Me.Button4.Text = "&Save"
    '        '
    '        'frmCustRel
    '        '
    '        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    '        Me.ClientSize = New System.Drawing.Size(608, 273)
    '        Me.Controls.Add(Me.GroupBox1)
    '        Me.Controls.Add(Me.Button1)
    '        Me.Controls.Add(Me.Button2)
    '        Me.Controls.Add(Me.Button6)
    '        Me.Controls.Add(Me.Button3)
    '        Me.Controls.Add(Me.Button4)
    '        Me.Name = "frmCustRel"
    '        Me.Text = "Customer Relationship Maintenance"
    '        Me.GroupBox1.ResumeLayout(False)
    '        Me.Panel1.ResumeLayout(False)
    '        Me.ResumeLayout(False)

    '    End Sub

    '#End Region

    '    Public WriteOnly Property Editable() As Boolean
    '        Set(ByVal Value As Boolean)
    '        End Set
    '    End Property

    '    Public WriteOnly Property CustomerID() As String
    '        Set(ByVal Value As String)
    '            strCurCust = Value
    '        End Set
    '    End Property

    '    Private Sub bindData()

    '        Dim ts As New clsDataGridTableStyle
    '        Dim cs As DataGridTextBoxColumn

    '        cs = New DataGridTextBoxColumn
    '        cs.Width = 80
    '        cs.MappingName = "CSRID"
    '        cs.HeaderText = "CSRID(W)"
    '        cs.NullText = gNULLText
    '        ts.GridColumnStyles.Add(cs)

    '        cs = New DataGridTextBoxColumn
    '        cs.Width = 80
    '        cs.MappingName = "CSRID_400"
    '        cs.HeaderText = "CSRID(C)"
    '        cs.NullText = gNULLText
    '        ts.GridColumnStyles.Add(cs)

    '        cs = New DataGridTextBoxColumn
    '        cs.Width = 130
    '        cs.MappingName = "Name"
    '        cs.HeaderText = "Name"
    '        cs.NullText = gNULLText
    '        ts.GridColumnStyles.Add(cs)

    '        cs = New DataGridTextBoxColumn
    '        cs.Width = 130
    '        cs.MappingName = "CName"
    '        cs.HeaderText = "Chinese Name"
    '        cs.NullText = gNULLText
    '        ts.GridColumnStyles.Add(cs)

    '        ts.MappingName = "CSR"
    '        grdCSR.TableStyles.Add(ts)

    '        grdCSR.DataSource = dtCSR
    '        grdCSR.AllowDrop = False
    '        grdCSR.ReadOnly = True

    '        bm = Me.BindingContext(dtCSR)
    '        txtID.Text = CType(bm.Current, DataRowView).Row.Item("CSRID")
    '        txtID_400.Text = CType(bm.Current, DataRowView).Row.Item("CSRID_400")
    '        txtName.Text = CType(bm.Current, DataRowView).Row.Item("Name")
    '        If Not IsDBNull(CType(bm.Current, DataRowView).Row.Item("CName")) Then
    '            txtCName.Text = CType(bm.Current, DataRowView).Row.Item("CName")
    '        Else
    '            txtCName.Text = ""
    '        End If

    '        Me.Editable = False
    '        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
    '    End Sub

    '    Private Function SelectRecord() As DataTable

    '        Dim dtTemp As New DataTable

    '        strSQL = "select CSRID, CSRID_400, Name, CName from csr"
    '        sqlconnect.ConnectionString = strCIWConn

    '        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

    '        Try
    '            sqlda.Fill(dtTemp)
    '        Catch sqlex As SqlClient.SqlException
    '            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '        Catch ex As Exception
    '            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '        End Try

    '        sqlconnect.Dispose()
    '        Return dtTemp

    '    End Function

    '    Private Sub grdCSR_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCSR.MouseUp
    '        Dim pt = New Point(e.X, e.Y)
    '        Dim hti As DataGrid.HitTestInfo = grdCSR.HitTest(pt)

    '        If hti.Type = DataGrid.HitTestType.Cell Then
    '            grdCSR.CurrentCell = New DataGridCell(hti.Row, hti.Column)
    '            grdCSR.Select(hti.Row)
    '        End If
    '    End Sub

    '    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
    '        Me.txtID.Text = ""
    '        Me.txtID_400.Text = ""
    '        Me.txtName.Text = ""
    '        Me.txtCName.Text = ""
    '        Me.Editable = True
    '    End Sub

    '    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

    '        If MsgBox("Do you want to delete the selected record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gSystem) = MsgBoxResult.Yes Then
    '            CType(bm.Current, DataRowView).Row.Delete()

    '            'update database
    '            sqlconnect.ConnectionString = strCIWConn
    '            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
    '            sqlcb = New SqlCommandBuilder(sqlda)
    '            Try
    '                sqlda.Update(dtCSR)
    '            Catch sqlex As SqlException
    '                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '                Exit Sub
    '            Catch ex As Exception
    '                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '                Exit Sub
    '            End Try

    '            MsgBox("Record deleted successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "CSR Maintenance")
    '            wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

    '        End If

    '    End Sub

    '    Private Sub grdCSR_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCSR.CurrentCellChanged

    '        txtID.Text = CType(bm.Current, DataRowView).Row.Item("CSRID")
    '        txtID_400.Text = CType(bm.Current, DataRowView).Row.Item("CSRID_400")
    '        txtName.Text = CType(bm.Current, DataRowView).Row.Item("Name")
    '        If Not IsDBNull(CType(bm.Current, DataRowView).Row.Item("CName")) Then
    '            txtCName.Text = CType(bm.Current, DataRowView).Row.Item("CName")
    '        Else
    '            txtCName.Text = ""
    '        End If

    '    End Sub

    '    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

    '        Dim dr As DataRow
    '        Dim lngErrNo As Long
    '        Dim strErrMsg As String

    '        'validate missing input
    '        If txtID.Text = "" OrElse txtName.Text = "" OrElse txtID_400.Text = "" Then
    '            MsgBox("Missing Information.", MsgBoxStyle.OKOnly + MsgBoxStyle.Exclamation, "Error")
    '            If txtID.Text = "" Then
    '                txtID.Focus()
    '            ElseIf txtID_400.Text = "" Then
    '                txtID_400.Focus()
    '            Else
    '                txtName.Focus()
    '            End If
    '            Exit Sub
    '        End If

    '        'check if the record already exists
    '        For Each dr In dtCSR.Rows
    '            If dr.Item("CSRID").toUpper = txtID.Text OrElse dr.Item("CSRID_400").toUpper = txtID_400.Text Then
    '                MsgBox("Record already exists.", MsgBoxStyle.OKOnly + MsgBoxStyle.Exclamation, "Error")
    '                Exit Sub
    '            End If
    '        Next

    '        Dim nr As DataRow = dtCSR.NewRow()
    '        nr.Item("CSRID") = txtID.Text
    '        nr.Item("CSRID_400") = txtID_400.Text
    '        nr.Item("Name") = txtName.Text
    '        nr.Item("CName") = txtCName.Text
    '        Try
    '            dtCSR.Rows.Add(nr)

    '            'write back to database
    '            sqlconnect.ConnectionString = strCIWConn
    '            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
    '            sqlcb = New SqlCommandBuilder(sqlda)
    '            sqlda.Update(dtCSR)
    '        Catch sqlex As SqlException
    '            lngErrNo = -1
    '            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '        Catch ex As Exception
    '            lngErrNo = -1
    '            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '        End Try

    '        Me.grdCSR.Refresh()
    '        Me.Editable = False
    '        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

    '        '' Update to CAPSIL USERPF
    '        'If lngErrNo = 0 Then
    '        '    wndMain.Cursor = Cursors.WaitCursor
    '        '    If Not objCS.UpdateCSR(txtID.Text, txtName.Text, txtCName.Text, lngErrNo, strErrMsg) Then
    '        '        MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '        '    Else
    '        '        MsgBox("Record saved successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "CSR Maintenance")
    '        '    End If
    '        '    wndMain.Cursor = Cursors.Default
    '        'End If

    '    End Sub

    '    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

    '        Me.Editable = False
    '        txtID.Text = CType(bm.Current, DataRowView).Row.Item("CSRID")
    '        txtID_400.Text = CType(bm.Current, DataRowView).Row.Item("CSRID_400")
    '        txtName.Text = CType(bm.Current, DataRowView).Row.Item("Name")
    '        If Not IsDBNull(CType(bm.Current, DataRowView).Row.Item("CName")) Then
    '            txtCName.Text = CType(bm.Current, DataRowView).Row.Item("CName")
    '        Else
    '            txtCName.Text = ""
    '        End If

    '    End Sub

    '    Private Sub txtID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.LostFocus
    '        If txtID_400.Text = "" Then
    '            txtID_400.Text = txtID.Text
    '        End If
    '    End Sub

End Class
