Imports System.Data.SqlClient

Public Class frmCSPolicy
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdExport As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkInForce As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCSPolicy))
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdExport = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.Button1 = New System.Windows.Forms.Button
        Me.chkInForce = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 16)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "Path:"
        '
        'txtPath
        '
        Me.txtPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtPath.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPath.Location = New System.Drawing.Point(48, 8)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(280, 20)
        Me.txtPath.TabIndex = 42
        Me.txtPath.Text = ""
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(192, 72)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.TabIndex = 51
        Me.cmdClose.Text = "&Close"
        '
        'cmdExport
        '
        Me.cmdExport.Location = New System.Drawing.Point(108, 72)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.TabIndex = 50
        Me.cmdExport.Text = "&Export"
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button1.Location = New System.Drawing.Point(332, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(28, 24)
        Me.Button1.TabIndex = 55
        '
        'chkInForce
        '
        Me.chkInForce.Checked = True
        Me.chkInForce.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInForce.Location = New System.Drawing.Point(52, 36)
        Me.chkInForce.Name = "chkInForce"
        Me.chkInForce.TabIndex = 56
        Me.chkInForce.Text = "In-force Policy"
        '
        'frmCSPolicy
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(376, 101)
        Me.Controls.Add(Me.chkInForce)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdExport)
        Me.Name = "frmCSPolicy"
        Me.Text = "CS Policy List"
        Me.ResumeLayout(False)

    End Sub

#End Region

    'oliver 2024-4-24 added for Table_Relocate_Sprint13
    Private Function GetFinal(ByVal IsInForce As Boolean) As DataTable
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try

            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_FINAL",
                        New Dictionary(Of String, String) From {
                        {"IsInForce", If(IsInForce, 1, 0)}
                        })
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If

        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI Retrieve Error." & vbCrLf & ex.Message)
        End Try
        Return dt

    End Function

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click

        'oliver 2024-4-30 commented for Table_Relocate_Sprint13
        'Dim strSQL As String
        'Dim sqlConn As New SqlConnection
        'Dim sqlda As SqlDataAdapter
        'Dim dtFinal As New DataTable
        'Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        ''" And a.unitcode = '00802' "
        'strSQL = "Select a.unitcode, a.agentcode, rtrim(csa.namesuffix)+' '+rtrim(csa.NameSuffix)+' '+rtrim(csa.firstname) as SA_Name, " &
        '        " cswcpl_csr as CSRID, cswcpl_remark as remark, name as CSR_Name, cswcpl_policy_no as Policy_No, rph.customerid, " &
        '        " rtrim(cph.namesuffix)+' '+rtrim(cph.NameSuffix)+' '+rtrim(cph.firstname) as PH_Name, " &
        '        " cph.dateofbirth, p.accountstatuscode, pa.cswpad_add1 as Addr1, pa.cswpad_add2 as Addr2, pa.cswpad_add3 as Addr3, pa.cswpad_city as City, " &
        '        " pa.cswpad_tel1 as Tel1, pa.cswpad_tel2 as Tel2" &
        '        " From csw_cs_policy_list, csw_poli_rel rph, customer cph, csw_policy_address pa, csw_poli_rel rsa, customer csa, agentcodes a, policyaccount p, " & serverPrefix & "csr " &
        '        " Where cswcpl_policy_no = rph.policyaccountid " &
        '        " And rph.policyrelatecode = 'PH' " &
        '        " And rph.customerid = cph.customerid " &
        '        " And cswcpl_policy_no = rsa.policyaccountid " &
        '        " And rsa.policyrelatecode = 'SA' " &
        '        " And rsa.customerid = csa.customerid " &
        '        " And csa.agentcode = a.agentcode " &
        '        " And cswcpl_policy_no = pa.cswpad_poli_id " &
        '        " And cswcpl_policy_no = p.policyaccountid " &
        '        " And cswcpl_csr <> '' " &
        '        " And cswcpl_csr = csrid "

        'If chkInForce.Checked = True Then
        '    strSQL = strSQL & " And p.accountstatuscode in ('1','2','3','4','V')"
        'End If

        'strSQL = strSQL & " Order by a.unitcode, a.agentcode, cswcpl_csr, cswcpl_policy_no"

        'Try
        '    sqlConn.ConnectionString = strCIWConn
        '    sqlda = New SqlDataAdapter(strSQL, sqlConn)
        '    sqlda.Fill(dtFinal)

        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        'oliver 2024-4-30 added for Table_Relocate_Sprint13
        Dim dtFinal As New DataTable
        dtFinal = GetFinal(chkInForce.Checked)

        If txtPath.Text <> "" AndAlso dtFinal.Rows.Count > 0 Then

            wndMain.Cursor = Cursors.WaitCursor

            'Open the CSV file.
            Dim csvStream As New IO.StreamWriter(txtPath.Text)

            'Use a string builder for efficiency.
            Dim line As New System.Text.StringBuilder

            Dim index As Integer

            'Iterate over all the columns in the table and get the column names.
            For index = 0 To dtFinal.Columns.Count - 1
                If index > 0 Then
                    'Put a comma between column names.
                    line.Append(","c)
                End If

                'Precede and follow each column name with a double quote
                'and escape each double quote with another double quote.
                line.Append(""""c)
                line.Append(dtFinal.Columns(index).ColumnName.Replace("""", """"""))
                line.Append(""""c)
            Next

            'Write the line to the file.
            csvStream.WriteLine(line.ToString())

            'Iterate over all the rows in the table.
            For Each row As DataRow In dtFinal.Rows
                'Clear the line of text.
                line.Remove(0, line.Length)

                'Iterate over all the fields in the row and get the field values.
                For index = 0 To dtFinal.Columns.Count - 1
                    If index > 0 Then
                        'Put a comma between field values.
                        line.Append(","c)
                    End If

                    'Precede and follow each field value with a double quote
                    'and escape each double quote with another double quote.
                    line.Append(""""c)
                    line.Append(row(index).ToString().Replace("""", """"""))
                    line.Append(""""c)
                Next

                'Write the line to the file.
                csvStream.WriteLine(line.ToString())
            Next

            csvStream.Flush()
            csvStream.Close()

            wndMain.Cursor = Cursors.Default
            MsgBox("CS Policy List export completed successfully", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)

        End If

        'sqlda.Dispose()
        'sqlConn.Dispose()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        SaveFileDialog1.Title = "Export to Excel"
        SaveFileDialog1.InitialDirectory = "C:\"
        SaveFileDialog1.DefaultExt = ".csv"
        SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv"
        SaveFileDialog1.RestoreDirectory = True

        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPath.Text = SaveFileDialog1.FileName
        End If

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
End Class
