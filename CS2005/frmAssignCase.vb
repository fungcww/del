Imports System.Data.SqlClient

Public Class frmAssignCase
    Inherits System.Windows.Forms.Form

    Dim dtCSRF, dtCSRR, dtResult As DataTable
    Dim strCampID, strChannelID As String

    Public Property Campaign(ByVal Channel As String) As String
        Get
        End Get
        Set(ByVal Value As String)
            strCampID = Value
            strChannelID = Channel
            Call LoadResult()
        End Set
    End Property

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
    Friend WithEvents grdResult As System.Windows.Forms.DataGrid
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents radCSR As System.Windows.Forms.RadioButton
    Friend WithEvents cboCSRFtr As System.Windows.Forms.ComboBox
    Friend WithEvents cmdShow As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAssign As System.Windows.Forms.Button
    Friend WithEvents lstCSR As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstDetail As System.Windows.Forms.ListBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents lblSelected As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdResult = New System.Windows.Forms.DataGrid
        Me.radAll = New System.Windows.Forms.RadioButton
        Me.radCSR = New System.Windows.Forms.RadioButton
        Me.cboCSRFtr = New System.Windows.Forms.ComboBox
        Me.cmdShow = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdAssign = New System.Windows.Forms.Button
        Me.lstCSR = New System.Windows.Forms.ListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lstDetail = New System.Windows.Forms.ListBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.lblSelected = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        CType(Me.grdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdResult
        '
        Me.grdResult.AlternatingBackColor = System.Drawing.Color.White
        Me.grdResult.BackColor = System.Drawing.Color.White
        Me.grdResult.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdResult.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdResult.CaptionVisible = False
        Me.grdResult.DataMember = ""
        Me.grdResult.FlatMode = True
        Me.grdResult.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdResult.ForeColor = System.Drawing.Color.Black
        Me.grdResult.GridLineColor = System.Drawing.Color.Wheat
        Me.grdResult.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdResult.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdResult.HeaderForeColor = System.Drawing.Color.Black
        Me.grdResult.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.Location = New System.Drawing.Point(4, 40)
        Me.grdResult.Name = "grdResult"
        Me.grdResult.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdResult.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdResult.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdResult.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.Size = New System.Drawing.Size(356, 328)
        Me.grdResult.TabIndex = 103
        '
        'radAll
        '
        Me.radAll.Checked = True
        Me.radAll.Location = New System.Drawing.Point(8, 4)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(104, 32)
        Me.radAll.TabIndex = 110
        Me.radAll.TabStop = True
        Me.radAll.Text = "All Sales Leads Records"
        '
        'radCSR
        '
        Me.radCSR.Location = New System.Drawing.Point(132, 8)
        Me.radCSR.Name = "radCSR"
        Me.radCSR.Size = New System.Drawing.Size(100, 24)
        Me.radCSR.TabIndex = 111
        Me.radCSR.Text = "Specific CSRID"
        '
        'cboCSRFtr
        '
        Me.cboCSRFtr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCSRFtr.Location = New System.Drawing.Point(236, 8)
        Me.cboCSRFtr.Name = "cboCSRFtr"
        Me.cboCSRFtr.Size = New System.Drawing.Size(104, 21)
        Me.cboCSRFtr.TabIndex = 112
        '
        'cmdShow
        '
        Me.cmdShow.Location = New System.Drawing.Point(344, 8)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.Size = New System.Drawing.Size(40, 20)
        Me.cmdShow.TabIndex = 113
        Me.cmdShow.Text = "Show"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblSelected)
        Me.GroupBox1.Controls.Add(Me.cmdAssign)
        Me.GroupBox1.Controls.Add(Me.lstCSR)
        Me.GroupBox1.Location = New System.Drawing.Point(368, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(252, 196)
        Me.GroupBox1.TabIndex = 115
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CSR List"
        '
        'cmdAssign
        '
        Me.cmdAssign.Location = New System.Drawing.Point(12, 160)
        Me.cmdAssign.Name = "cmdAssign"
        Me.cmdAssign.Size = New System.Drawing.Size(116, 23)
        Me.cmdAssign.TabIndex = 116
        Me.cmdAssign.Text = "Auto Assign"
        '
        'lstCSR
        '
        Me.lstCSR.Location = New System.Drawing.Point(8, 20)
        Me.lstCSR.Name = "lstCSR"
        Me.lstCSR.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstCSR.Size = New System.Drawing.Size(236, 134)
        Me.lstCSR.TabIndex = 115
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstDetail)
        Me.GroupBox2.Location = New System.Drawing.Point(372, 244)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(248, 164)
        Me.GroupBox2.TabIndex = 119
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Assignment Details"
        '
        'lstDetail
        '
        Me.lstDetail.Location = New System.Drawing.Point(8, 20)
        Me.lstDetail.Name = "lstDetail"
        Me.lstDetail.Size = New System.Drawing.Size(232, 134)
        Me.lstDetail.TabIndex = 0
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(12, 380)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Save"
        '
        'lblSelected
        '
        Me.lblSelected.Location = New System.Drawing.Point(136, 164)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(100, 16)
        Me.lblSelected.TabIndex = 117
        Me.lblSelected.Text = "Label1"
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(96, 380)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.TabIndex = 120
        Me.cmdClose.Text = "Close"
        '
        'frmAssignCase
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(624, 413)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdShow)
        Me.Controls.Add(Me.cboCSRFtr)
        Me.Controls.Add(Me.radCSR)
        Me.Controls.Add(Me.radAll)
        Me.Controls.Add(Me.grdResult)
        Me.Controls.Add(Me.cmdSave)
        Me.Name = "frmAssignCase"
        Me.Text = "Assign Customer to CSR"
        CType(Me.grdResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub enablecontrols(ByVal blnVal As Boolean)

        radAll.Enabled = blnVal
        radCSR.Enabled = blnVal
        cboCSRFtr.Enabled = Not blnVal
        grdResult.Enabled = blnVal
        lstCSR.Enabled = blnVal
        cmdShow.Enabled = Not blnVal
        cmdAssign.Enabled = blnVal
        cmdSave.Enabled = Not blnVal

    End Sub

    Function LoadResult() As Boolean

        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim dsTemp As New DataSet
        Dim strSQL As String
        Dim blnLoad As Boolean = True
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select crmcsl_customer_id, crmcsl_csrid, name " &
            " From " & serverPrefix & "crm_campaign_sales_leads left join " & serverPrefix & "csr " &
            " ON crmcsl_csrid = csrid " &
            " Where crmcsl_campaign_id = '" & strCampID & "' " &
            " And crmcsl_channel_id = '" & strChannelID & "' " &
            " Order by crmcsl_customer_id; "

        strSQL &= "Select crmcsl_csrid, name, count(*) as Cnt " &
            " From " & serverPrefix & "crm_campaign_sales_leads left join " & serverPrefix & "csr " &
            " ON crmcsl_csrid = csrid " &
            " Where crmcsl_campaign_id = '" & strCampID & "' " &
            " And crmcsl_channel_id = '" & strChannelID & "' " &
            " Group by crmcsl_csrid, name"
        Try
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(dstemp)
            blnLoad = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
            blnLoad = False
        End Try

        'If dtResult Is Nothing Then
        '    dtResult = New DataTable("Result")
        'End If
        'dtResult.Clear()
        dtResult = dsTemp.Tables(0).Copy

        If dsTemp.Tables(0).Rows.Count > 0 Then
            lstDetail.Items.Clear()
            lstDetail.Items.Add("Records: " & dtResult.DefaultView.Count)
        End If

        If dsTemp.Tables(1).Rows.Count > 0 Then
            If IsDBNull(dsTemp.Tables(1).Rows(0).Item("crmcsl_csrid")) OrElse dsTemp.Tables(1).Rows(0).Item("crmcsl_csrid") = "" Then
                lstDetail.Items.Add("No CSR assigned")
            Else
                For i As Integer = 0 To dsTemp.Tables(1).Rows.Count - 1
                    lstDetail.Items.Add(dsTemp.Tables(1).Rows(i).Item("Cnt") & " customers assigned to " & dsTemp.Tables(1).Rows(i).Item("Name"))
                Next
            End If
        End If

        'lblSLCnt.Text = "Records: " & dtResult.DefaultView.Count

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "crmcsl_customer_id"
        cs.HeaderText = "CustomerID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "crmcsl_csrid"
        cs.HeaderText = "CSRID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Name"
        cs.HeaderText = "Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "Result"
        Me.grdResult.TableStyles.Clear()
        Me.grdResult.TableStyles.Add(ts)

        grdResult.DataSource = dtResult
        grdResult.AllowDrop = False
        grdResult.ReadOnly = True

        Return blnLoad

    End Function

    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As Object, ByVal strCode As String, ByVal strName As String, ByVal strSQL As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean

        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim blnLoad As Boolean

        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            dt.Clear()
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(dt)
            blnLoad = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
            blnLoad = False
        End Try

        If blnLoad Then
            cbo.DataSource = dt
            cbo.DisplayMember = strName
            cbo.ValueMember = strCode
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub frmAssignCase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select distinct csrid, name From " & serverPrefix & "CSR Where active = 'Y' order by name"

        Call enablecontrols(True)
        LoadComboBox(dtCSRF, cboCSRFtr, "csrid", "name", strSQL)
        LoadComboBox(dtCSRR, lstCSR, "csrid", "name", strSQL)

    End Sub

    Private Sub radAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radAll.Click
        cboCSRFtr.Enabled = False
        cmdShow.Enabled = False
        dtResult.DefaultView.RowFilter = ""
        'lblSLCnt.Text = "Records: " & dtResult.DefaultView.Count
    End Sub

    Private Sub radCSR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radCSR.Click
        cboCSRFtr.Enabled = True
        cmdShow.Enabled = True
    End Sub

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        dtResult.DefaultView.RowFilter = "crmcsl_csrid = '" & cboCSRFtr.SelectedValue & "'"
        'lblSLCnt.Text = "Records: " & dtResult.DefaultView.Count
    End Sub

    Private Sub cmdAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAssign.Click

        Dim intBatch, intLstCnt, intSLCnt, intStart, intEnd As Integer
        Dim strSQL As String

        Dim sqlConn As New SqlConnection
        Dim sqlCmd As New SqlCommand

        intLstCnt = lstCSR.SelectedItems.Count
        intSLCnt = dtResult.DefaultView.Count

        If intLstCnt > 0 Then
            If intSLCnt > 0 Then
                If intLstCnt <= intSLCnt Then

                    intBatch = intSLCnt \ intLstCnt
                    lstDetail.Items.Clear()

                    For i As Integer = 0 To intLstCnt - 1
                        If i = intLstCnt - 1 Then
                            lstDetail.Items.Add(intSLCnt - intBatch * i & " customers will assign to " & lstCSR.SelectedItems.Item(i)(1))
                        Else
                            lstDetail.Items.Add(intBatch & " customers will assign to " & lstCSR.SelectedItems.Item(i)(1))
                        End If
                    Next
                    cmdSave.Enabled = True
                Else
                    MsgBox("No. of CSR selected is greater than the no. of sales leads records.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
                End If
            Else
                MsgBox("Please select sales leads records to assign.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            End If
        Else
            MsgBox("Please choose one ore more CSR to assign to.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
        End If

    End Sub

    Private Sub lstCSR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCSR.SelectedIndexChanged
        lblSelected.Text = lstCSR.SelectedItems.Count & " CSR Selected"
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim intBatch, intLstCnt, intSLCnt, intStart, intEnd As Integer
        Dim strSQL As String

        Dim sqlConn As New SqlConnection
        Dim sqlCmd As New SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        intLstCnt = lstCSR.SelectedItems.Count
        intSLCnt = dtResult.DefaultView.Count
        
        If intLstCnt > 0 Then
            If intSLCnt > 0 Then

                If MsgBox("Save CSR assignment?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gSystem) = MsgBoxResult.No Then
                    Exit Sub
                End If

                If intLstCnt <= intSLCnt Then
                    dtResult.DefaultView.Sort = "crmcsl_customer_id"

                    intBatch = intSLCnt \ intLstCnt

                    sqlConn.ConnectionString = strCIWConn
                    sqlConn.Open()
                    sqlCmd.Connection = sqlConn

                    For i As Integer = 0 To intLstCnt - 1
                        intStart = dtResult.DefaultView.Item(i * intBatch).Item("crmcsl_customer_id")

                        If i = intLstCnt - 1 Then
                            intEnd = dtResult.DefaultView.Item(intSLCnt - 1).Item("crmcsl_customer_id")
                        Else
                            intEnd = dtResult.DefaultView.Item(i * intBatch + intBatch - 1).Item("crmcsl_customer_id")
                        End If

                        Try
                            strSQL = "Update " & serverPrefix & "crm_campaign_sales_leads " & _
                                " Set crmcsl_csrid = '" & lstCSR.SelectedItems.Item(i)(0) & "', " & _
                                "     crmcsl_update_user = '" & gsUser & "', " & _
                                "     crmcsl_update_date = GETDATE() " & _
                                " Where crmcsl_campaign_id = '" & strCampID & "'" & _
                                " And crmcsl_channel_id = '" & strChannelID & "' " & _
                                " And crmcsl_customer_id BETWEEN " & intStart & " AND " & intEnd

                            sqlCmd.CommandText = strSQL
                            sqlCmd.ExecuteNonQuery()

                        Catch sqlex As SqlClient.SqlException
                            MsgBox(sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                        Catch ex As Exception
                            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                        End Try
                    Next
                    sqlConn.Close()
                    sqlConn.Dispose()

                    Call LoadResult()
                Else
                    MsgBox("No. of CSR selected is greater than the no. of sales leads records.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
                End If
            Else
                MsgBox("Please select sales leads records to assign.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            End If
        Else
            MsgBox("Please choose one ore more CSR to assign to.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

End Class
