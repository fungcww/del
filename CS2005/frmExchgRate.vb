Imports System.Data.SqlClient


Public Class frmExchgRate

    Dim ds As New DataSet
    Dim strFtype, strFCcy, strTCcy As String

    Private Sub frmExchgRate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter

        Try

            'AC - Change to use configuration setting - start
            'strSQL = "select * from rmlife.dbo.Rlf_ccy_code; "
            If gUAT = True Then
                strSQL = "select * from " & gcRMLife & "Rlf_ccy_code; "
            Else
                strSQL = "select * from rmlife.dbo.Rlf_ccy_code; "
            End If
            'AC - Change to use configuration setting - end

            strSQL &= "select 'Rate Type'=Rlfcxr_rate_type, " & _
                       " 'Effictive Date'= Rlfcxr_eff_date," & _
                       " 'From Ccy'=Rlfcxr_from_ccy ," & _
                       " 'To Ccy'=Rlfcxr_to_ccy , " & _
                       " 'Buy Rate'=Rlfcxr_buy_rate ," & _
                       " 'Sell Rate'=Rlfcxr_sell_rate ," & _
                       " 'Capsil Date'=convert(varchar(10),Rlfcxr_capsil_date,103) ," & _
                       " 'Cheque Buy Rate'=Rlfcxr_chq_buy_rate ," & _
                       " 'Cheque Sell Rate'=Rlfcxr_chq_sell_rate ," & _
                       " 'Approved'=Rlfcxr_apprv_flag  ," & _
                       " 'Update'=Rlfcxr_update_flag ," & _
                       " 'Entered By'=Rlfcxr_enter_by ," & _
                       " 'Entered Date'= convert(varchar(10),Rlfcxr_enter_date,103)+' '+convert(varchar(8),Rlfcxr_enter_date,108)," & _
                       " 'Approved By'=Rlfcxr_apprv_by ," & _
                       " 'Approved Date'=convert(varchar(10),Rlfcxr_apprv_date,103)+' '+convert(varchar(8),Rlfcxr_apprv_date,108)"

            'AC - Change to use configuration setting - start
            If gUAT = True Then
                strSQL &= " from " & gcRMLife & "Rlf_central_ex_rate Where rlfcxr_update_flag = 'Y' and rlfcxr_apprv_flag = 'Y'"
            Else
                strSQL &= " from RMLIFE.dbo.Rlf_central_ex_rate Where rlfcxr_update_flag = 'Y' and rlfcxr_apprv_flag = 'Y'"
            End If
            'AC - Change to use configuration setting - end

            strSQL &= " order by Rlfcxr_eff_date  desc "

            'AC - Change advance compilation option to configuration file - start
            '#If UAT = 1 Then
            '            strCIWConn = "server=HKSQLUAT1;database=INGCIWU202;Network=DBMSSOCN;uid=dev_las_ing;password=ingladev;Connect Timeout=0;Connect Timeout=0"
            '#End If
            'AC - Change advance compilation option to configuration file - start

            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.TableMappings.Add("Rlf_ccy_code1", "ExchangeRate")
            sqlda.Fill(ds, "Rlf_ccy_code")

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Dispose()
        End Try

        DataGridView1.DataSource = ds.Tables("ExchangeRate").DefaultView
        cboFrom.DataSource = ds.Tables("Rlf_ccy_code").Copy
        cboFrom.DisplayMember = "Rlfccy_desc"
        cboFrom.ValueMember = "Rlfccy_code"
        cboTo.DataSource = ds.Tables("Rlf_ccy_code").Copy
        cboTo.DisplayMember = "Rlfccy_desc"
        cboTo.ValueMember = "Rlfccy_code"
        cboType.SelectedItem = "BOK"
        ds.Tables("ExchangeRate").DefaultView.Sort = "[Effictive Date] DESC"

        For i As Integer = 0 To cboFrom.Items.Count - 1
            If cboFrom.Items(i)(0) = "USD" Then cboFrom.SelectedIndex = i
            If cboTo.Items(i)(0) = "HKD" Then cboTo.SelectedIndex = i
        Next

    End Sub

    Private Sub cboFrom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFrom.SelectedIndexChanged
        strFCcy = cboFrom.Items(cboFrom.SelectedIndex)(0)
        SetFilter()
    End Sub

    Private Sub cboTo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTo.SelectedIndexChanged
        strTCcy = cboTo.Items(cboTo.SelectedIndex)(0)
        SetFilter()
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        strFtype = cboType.Items(cboType.SelectedIndex)
        SetFilter()
    End Sub

    Private Sub SetFilter()
        ds.Tables("ExchangeRate").DefaultView.RowFilter = "[Rate Type] = '" & strFtype & "' and [From Ccy] = '" & strFCcy & "' and [To Ccy] = '" & strTCcy & "'"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class