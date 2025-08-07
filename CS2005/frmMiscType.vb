Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.io

Public Class frmMiscType
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
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents grdMisc As System.Windows.Forms.DataGrid
    Friend WithEvents cmdImport As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.txtID = New System.Windows.Forms.TextBox
        Me.grdMisc = New System.Windows.Forms.DataGrid
        Me.cmdImport = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        CType(Me.grdMisc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(88, 240)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.TabIndex = 6
        Me.cmdEdit.Text = "&Edit"
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(340, 240)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.TabIndex = 9
        Me.cmdDelete.Text = "&Delete"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(256, 240)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "&Cancel"
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(172, 240)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "&Save"
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(4, 240)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.TabIndex = 5
        Me.cmdAdd.Text = "&Add"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 180)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 16)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 204)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 16)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesc.Location = New System.Drawing.Point(80, 200)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(416, 20)
        Me.txtDesc.TabIndex = 3
        Me.txtDesc.Text = ""
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.SystemColors.Window
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(80, 176)
        Me.txtID.MaxLength = 10
        Me.txtID.Name = "txtID"
        Me.txtID.TabIndex = 1
        Me.txtID.Text = ""
        '
        'grdMisc
        '
        Me.grdMisc.AlternatingBackColor = System.Drawing.Color.White
        Me.grdMisc.BackColor = System.Drawing.Color.White
        Me.grdMisc.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdMisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdMisc.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdMisc.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdMisc.CaptionVisible = False
        Me.grdMisc.DataMember = ""
        Me.grdMisc.FlatMode = True
        Me.grdMisc.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdMisc.ForeColor = System.Drawing.Color.Black
        Me.grdMisc.GridLineColor = System.Drawing.Color.Wheat
        Me.grdMisc.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdMisc.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdMisc.HeaderForeColor = System.Drawing.Color.Black
        Me.grdMisc.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdMisc.Location = New System.Drawing.Point(4, 4)
        Me.grdMisc.Name = "grdMisc"
        Me.grdMisc.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdMisc.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdMisc.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdMisc.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdMisc.Size = New System.Drawing.Size(492, 164)
        Me.grdMisc.TabIndex = 19
        '
        'cmdImport
        '
        Me.cmdImport.Location = New System.Drawing.Point(424, 240)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.TabIndex = 10
        Me.cmdImport.Text = "&Import"
        '
        'frmMiscType
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(504, 265)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.grdMisc)
        Me.Name = "frmMiscType"
        Me.Text = "Miscellaneous Types"
        CType(Me.grdMisc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim sqlda As SqlDataAdapter
    Dim sqlconnect As New SqlConnection
    Dim dtMisc As New DataTable
    Dim strSQL As String
    Dim sqlcb As SqlCommandBuilder

    Dim bm As BindingManagerBase
    Dim blnAdd As Boolean = True

    Public WriteOnly Property Editable() As Boolean
        Set(ByVal Value As Boolean)
            Me.cmdAdd.Enabled = Not Value
            Me.cmdEdit.Enabled = Not Value
            Me.cmdSave.Enabled = Value
            Me.cmdCancel.Enabled = Value
            Me.cmdDelete.Enabled = Not Value
            Me.cmdImport.Enabled = Not Value
            Me.grdMisc.Enabled = Not Value
            Me.txtID.ReadOnly = Not Value
            Me.txtDesc.ReadOnly = Not Value
        End Set
    End Property

    Private Sub frmIRTS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        dtMisc = selectRecord()
        dtMisc.TableName = "MISC"
        bindData()

    End Sub

    Private Function selectRecord() As DataTable
        Dim dtTemp As New DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "select cswmit_type, cswmit_desc from " & serverPrefix & "csw_misc_info_type order by 1"
        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dtTemp)
        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        sqlconnect.Dispose()
        Return dtTemp
    End Function

    Private Sub bindData()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "cswmit_type"
        cs.HeaderText = "Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 300
        cs.MappingName = "cswmit_desc"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "MISC"
        grdMisc.TableStyles.Add(ts)

        grdMisc.DataSource = dtMisc
        grdMisc.AllowDrop = False
        grdMisc.ReadOnly = True

        dtMisc.DefaultView.Sort = "cswmit_type"

        wndMain.StatusBarPanel1.Text = ""

        bm = BindingContext(dtMisc)

        Call updateDisplay()
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

        'enable/disable the corresponding buttons
        Me.Editable = False
    End Sub

    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click

        Dim f As New frmImportList
        Dim strPath As String        

        OpenFileDialog1.Title = "Import Client List"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.DefaultExt = ".csv"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Filter = "Import files (*.csv)|*.csv"

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = OpenFileDialog1.FileName
        Else
            Exit Sub
        End If

        Dim DataFile As FileInfo = New FileInfo(strPath)
        Dim cnCSV As OdbcConnection
        Dim daCSV As OdbcDataAdapter
        Dim dt As DataTable = New DataTable
        Dim blnValidFmt As Boolean = True

        cnCSV = New OdbcConnection("Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" & DataFile.Directory.FullName & ";")
        daCSV = New OdbcDataAdapter("SELECT * FROM [" & DataFile.Name & "]", cnCSV)

        Try
            daCSV.Fill(dt)
        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        End Try

        If dt.Columns("Type") Is Nothing Then blnValidFmt = False
        If dt.Columns("CustomerID") Is Nothing Then blnValidFmt = False
        If dt.Columns("StartDate") Is Nothing Then blnValidFmt = False
        If dt.Columns("EndDate") Is Nothing Then blnValidFmt = False
        If dt.Columns("Provider") Is Nothing Then blnValidFmt = False
        If dt.Columns("RenewalDate") Is Nothing Then blnValidFmt = False
        If dt.Columns("Desc") Is Nothing Then blnValidFmt = False
        If dt.Columns("Status") Is Nothing Then blnValidFmt = False
        If dt.Columns("Rating") Is Nothing Then blnValidFmt = False

        Dim dr As DataRow
        Dim strSQL, strErrMsg As String
        Dim intBatch, intCnt As Integer
        Dim lngErrNo As Long
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If blnValidFmt Then
            If dt.Rows.Count > 0 Then

                If MsgBox(dt.Rows.Count & " record(s) will import to CRS, proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gSystem) = MsgBoxResult.No Then
                    Exit Sub
                End If

                Dim sqlCmd As New SqlCommand
                sqlconnect.ConnectionString = strCIWConn
                sqlconnect.Open()
                sqlCmd.Connection = sqlconnect

                ' Create batch
                strSQL = "Insert " & serverPrefix & "csw_misc_info_batch (cswmib_desc, cswmib_delete, cswmib_user, cswmib_date) " &
                    "Values('" & strPath & "','','" & gsUser & "', GETDATE()); " &
                    "Select MAX(cswmib_id) as BatchID from " & serverPrefix & "csw_misc_info_batch"

                sqlCmd.CommandText = strSQL
                Try
                    intBatch = sqlCmd.ExecuteScalar()
                Catch sqlex As SqlException
                    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Sub
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Sub
                End Try

                For Each dr In dt.Rows
                    lngErrNo = 0

                    strSQL = "Insert " & serverPrefix & "csw_misc_info (cswmif_type, cswmif_customer_id, cswmif_start_date, cswmif_end_date, " &
                        "cswmif_provider, cswmif_renewal_date, cswmif_desc, cswmif_status, cswmif_batch, cswmif_create_user, " &
                        "cswmif_create_date, cswmif_update_user, cswmif_update_date) " &
                        "Values (" &
                        "'" & dr.Item("Type") & "'," &
                        "'" & dr.Item("CustomerID") & "'," &
                        "'" & dr.Item("StartDate") & "'," &
                        "'" & dr.Item("EndDate") & "'," &
                        "'" & Replace(dr.Item("Provider"), "'", "''") & "'," &
                        "'" & dr.Item("RenewalDate") & "'," &
                        "'" & Replace(dr.Item("Desc"), "'", "''") & "'," &
                        "'" & dr.Item("Status") & "'," & intBatch & "," &
                        "'" & gsUser & "', GETDATE(), '" & gsUser & "', GETDATE())"
                    sqlCmd.CommandText = strSQL
                    Try
                        sqlCmd.ExecuteNonQuery()
                    Catch sqlex As SqlException
                        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                        Exit Sub
                    Catch ex As Exception
                        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                        Exit Sub
                    End Try

                    Dim strRating As String
                    strRating = IIf(IsDBNull(dr.Item("Rating")), 0, dr.Item("Rating"))
                    If Not IsNumeric(strRating) Then strRating = "0"

                    'oliver 2024-5-3 added for Table_Relocate_Sprint13
                    strSQL = "Update " & serverPrefix & " csw_demographic " &
                        " Set cswdgm_rating = isnull(cswdgm_rating,0) + " & strRating &
                        " Where cswdgm_cust_id = '" & dr.Item("CustomerID") & "'"
                    intCnt = objCS.UpdateData(strSQL, "N", "CIW", lngErrNo, strErrMsg)

                    If lngErrNo <> 0 Then
                        MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
                        Exit Sub
                    Else
                        If intCnt = 0 Then
                            'oliver 2024-5-3 added for Table_Relocate_Sprint13
                            strSQL = "Insert " & serverPrefix & " csw_demographic " &
                                "   (cswdgm_cust_id, cswdgm_email_addr, cswdgm_mar_stat, cswdgm_edu_level, cswdgm_no_of_dep, " &
                                "    cswdgm_ann_sal, cswdgm_optout_email, cswdgm_optout_call, cswdgm_rating, cswdgm_household_income, cswdgm_remark, cswdgm_occupation) " &
                                " Values ('" & dr.Item("CustomerID") & "'," &
                                "'', '', 0, 0, 0, 'N', 'N', " & strRating & ", 0, '', 0) "
                            intCnt = objCS.UpdateData(strSQL, "N", "CIW", lngErrNo, strErrMsg)
                            If lngErrNo <> 0 Then
                                MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, gSystem)
                                Exit Sub
                            End If
                        End If
                    End If
                Next
                sqlconnect.Close()
                sqlconnect.Dispose()

                MsgBox(dt.Rows.Count & " records imported successfully.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)

            End If
        Else
            MsgBox("Invalid import file layout. The file must include the following columns:" & vbCrLf & _
                "Type, CustomerID, StartDate, EndDate, Provider, RenewalDate, Desc, Status, Rating", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
            Exit Sub
        End If

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Me.txtID.Text = ""
        Me.txtDesc.Text = ""
        Me.Editable = True
        Me.txtID.Focus()
    End Sub

    Private Sub updateDisplay()

        If bm.Count > 0 Then
            txtID.Text = CType(bm.Current, DataRowView).Row.Item("cswmit_type")
            txtDesc.Text = CType(bm.Current, DataRowView).Row.Item("cswmit_desc")
        End If

    End Sub

    Private Sub grdAlert_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMisc.CurrentCellChanged
        updateDisplay()
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        If MsgBox("Are you sure to delete the selected record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gSystem) = MsgBoxResult.Yes Then

            Dim sqlDEL, strSQL As String
            Dim sqlCmd As New SqlCommand
            Dim intCnt As Integer
            Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

            sqlDEL = "delete from " & serverPrefix & "csw_misc_info_type where cswmit_type = '" & CType(bm.Current, DataRowView).Row.Item("cswmit_type") & "'"

            strSQL = "Select COUNT(*) From " & serverPrefix & "csw_misc_info Where cswmif_type = '" & CType(bm.Current, DataRowView).Row.Item("cswmit_type") & "'"

            Try
                sqlconnect.ConnectionString = strCIWConn
                sqlCmd.CommandText = strSQL
                sqlCmd.Connection = sqlconnect
                sqlconnect.Open()

                intCnt = sqlCmd.ExecuteScalar

                If intCnt = 0 Then
                    sqlCmd.CommandText = sqlDEL
                    sqlCmd.ExecuteNonQuery()
                Else
                    MsgBox("Type is in use, delete is not allowed.", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
                End If
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Finally
                sqlconnect.Close()
                sqlconnect.Dispose()
            End Try

            Dim dtTemp As DataTable
            Me.dtMisc.Clear()
            dtTemp = selectRecord()
            Dim dr As DataRow
            Dim ar() As Object
            For Each dr In dtTemp.Rows
                ar = dr.ItemArray
                dtMisc.Rows.Add(ar)
            Next

            updateDisplay()
            MsgBox("Record deleted successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
        End If
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        Me.Editable = True
        blnAdd = False
        txtDesc.Focus()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Editable = False
        blnAdd = True
        updateDisplay()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        wndMain.Cursor = Cursors.WaitCursor

        If Me.txtID.Text <> "" AndAlso Me.txtDesc.Text <> "" Then
            If blnAdd Then
                addRecord()
            Else
                editRecord()
            End If
        Else
            MsgBox("Please enter both Type and Description.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If
        wndMain.Cursor = Cursors.Default

    End Sub

    Private Sub addRecord()

        Dim strInsert As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strInsert = "Insert into " & serverPrefix & "csw_misc_info_type (cswmit_type, cswmit_desc, cswmit_create_user, cswmit_create_date, " &
            " cswmit_update_user, cswmit_update_date) " &
            " values ('" & txtID.Text & "', '" & Replace(txtDesc.Text, "'", "''") & "','" & gsUser & "', GETDATE(), '" &
            gsUser & "',GETDATE())"

        Dim sqlCmd As SqlCommand
        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()
        sqlCmd = New SqlCommand(strInsert, sqlconnect)

        Try
            sqlCmd.ExecuteNonQuery()
        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
        End Try

        Dim dtTemp As DataTable
        Me.dtMisc.Clear()
        dtTemp = selectRecord()
        Dim dr1 As DataRow
        Dim ar() As Object
        For Each dr1 In dtTemp.Rows
            ar = dr1.ItemArray
            dtMisc.Rows.Add(ar)
        Next

        'bm = BindingContext(dtAlert)
        updateDisplay()
        Me.Editable = False
        MsgBox("Record saved successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Alert Maintenance")
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

    End Sub

    Private Sub editRecord()
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Dim sqlUpdate = "Update " & serverPrefix & "csw_misc_info_type set cswmit_desc = '" & Replace(Me.txtDesc.Text, "'", "''") & "'," &
                " cswca_update_user = '" & gsUser & "',cswca_update_date = GETDATE() " &
                " Where cswmit_type = '" & CType(bm.Current, DataRowView).Row.Item("cswmit_type") & "'"

        Dim sqlCmd As SqlCommand
        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()
        sqlCmd = New SqlCommand(sqlUpdate, sqlconnect)

        Try
            sqlCmd.ExecuteNonQuery()
        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
        End Try

        Dim dtTemp As DataTable
        Me.dtMisc.Clear()
        dtTemp = selectRecord()
        Dim dr As DataRow
        Dim ar() As Object
        For Each dr In dtTemp.Rows
            ar = dr.ItemArray
            dtMisc.Rows.Add(ar)
        Next

        updateDisplay()
        Me.Editable = False
        blnAdd = True
        MsgBox("Record saved successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Alert Maintenance")
        wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

    End Sub

    Private Sub grdMisc_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMisc.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdMisc.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdMisc.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdMisc.Select(hti.Row)
        End If
    End Sub

End Class
