Public Class ctrl_FundTranHist

    Private clsCRS As New LifeClientInterfaceComponent.clsCRS
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private strPolicyNo As String
    Private dtFundHeader, dtSO, dtSI As DataTable
    Private dsReceData As New DataSet
    Private strTxID As String
    Private strTxType As String
    Private strLA As String

#Region "MQ Properties"
    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
            clsCRS.MQQueuesHeader = objMQQueHeader
        End Set
    End Property

    Public Property ComHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
            clsCRS.DBHeader = objDBHeader
        End Set
    End Property
#End Region
#Region " Property Setting"
    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property

    Public Property TxID(ByVal TxType As String, ByVal TxLA As String) As String
        Get
            Return strTxID
        End Get
        Set(ByVal value As String)
            strTxID = value
            strTxType = TxType
            strLA = TxLA
        End Set
    End Property
#End Region

    Private Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim strErr As String = ""

            'Getting Transaction History
            If clsCRS.GetFundTxHist(strPolicyNo, "", "", "", dsReceData, strErr) Then
                If dsReceData.Tables.Contains("FundTran") Then
                    dtFundHeader = dsReceData.Tables("FundTran")
                    dtFundHeader.DefaultView.Sort = "postq_post_on DESC"
                    dgHist.DataSource = dtFundHeader

                    'dgHist.Columns(0).Visible = False
                    'dgHist.Columns(1).Visible = True
                    'dgHist.Columns(2).Visible = True
                    'dgHist.Columns(3).Visible = False
                    'dgHist.Columns(4).Visible = True

                    dgHist.Columns(0).HeaderText = "Trans. ID" : dgHist.Columns(0).Width = 80
                    dgHist.Columns(1).HeaderText = "Date" : dgHist.Columns(1).Width = 120
                    dgHist.Columns(2).HeaderText = "Type" : dgHist.Columns(2).Width = 100
                    dgHist.Columns(3).HeaderText = "Status" : dgHist.Columns(3).Width = 100
                    dgHist.Columns(4).HeaderText = "System" : dgHist.Columns(4).Width = 80 : dgHist.Columns(4).Visible = False
                Else
                    MsgBox("No Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                End If
            Else
                MsgBox(strErr)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgHist_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgHist.SelectionChanged

        'Dim strTxID As String
        'Dim strTxType As String
        'Dim strLA As String

        If Not Me.dgHist.CurrentRow.Cells("postq_trans_id").Value Is Nothing AndAlso Not IsDBNull(Me.dgHist.CurrentRow.Cells("postq_trans_id").Value) Then

            strTxID = Me.dgHist.CurrentRow.Cells("postq_trans_id").Value
            strTxType = Me.dgHist.CurrentRow.Cells("postq_trans_type").Value
            strLA = Me.dgHist.CurrentRow.Cells("postq_backend_sys").Value

            Call ShowDetail()

        End If

    End Sub

    Private Sub ShowDetail()

        Dim strErr As String = ""

        If clsCRS.GetFundTxHist(strPolicyNo, strTxType, strTxID, strLA, dsReceData, strErr) Then
            If strTxType = "ULB" Or strTxType = "ULS" Or strTxType = "Fund Switching" Or strTxType = "Partial Surrender" Then

                If dsReceData.Tables.Contains("SwitchOut") Then

                    lblSWO.Text = "Switch Out Details"
                    dgSwitchOut.DataSource = dsReceData.Tables("SwitchOut")
                    dgSwitchOut.Columns(0).HeaderText = "Fund Code" : dgSwitchOut.Columns(0).Width = 90
                    dgSwitchOut.Columns(1).HeaderText = "Fund Name" : dgSwitchOut.Columns(1).Width = 150
                    dgSwitchOut.Columns(2).HeaderText = "Switch Out %" : dgSwitchOut.Columns(2).Width = 100
                    ' concurrent fund trade, hide this column
                    dgSwitchOut.Columns(3).HeaderText = "Units Hold" : dgSwitchOut.Columns(3).Width = 80 : dgSwitchOut.Columns(3).Visible = False
                    dgSwitchOut.Columns(4).HeaderText = "Switch Out Units" : dgSwitchOut.Columns(4).Width = 90
                    dgSwitchOut.Columns(5).HeaderText = "Currency" : dgSwitchOut.Columns(5).Width = 70
                    dgSwitchOut.Columns(6).HeaderText = "Unit Price" : dgSwitchOut.Columns(6).Width = 80
                    dgSwitchOut.Columns(7).HeaderText = "Valuation Date" : dgSwitchOut.Columns(7).Width = 80
                    dgSwitchOut.Columns(8).HeaderText = "Total (Fund Ccy)" : dgSwitchOut.Columns(8).Width = 100
                    dgSwitchOut.Columns(9).HeaderText = "Total (PO Ccy)" : dgSwitchOut.Columns(9).Width = 100
                    dgSwitchOut.Columns(10).HeaderText = "Ex. Rate" : dgSwitchOut.Columns(10).Width = 100
                End If

                If dsReceData.Tables.Contains("SwitchIn") Then
                    dgSwitchIn.DataSource = dsReceData.Tables("SwitchIn")
                    dgSwitchIn.Columns(0).HeaderText = "Fund Code" : dgSwitchIn.Columns(0).Width = 90
                    dgSwitchIn.Columns(1).HeaderText = "Fund Name" : dgSwitchIn.Columns(1).Width = 150
                    dgSwitchIn.Columns(2).HeaderText = "Allocation %" : dgSwitchIn.Columns(2).Width = 90
                    dgSwitchIn.Columns(3).HeaderText = "Currency" : dgSwitchIn.Columns(3).Width = 70
                    dgSwitchIn.Columns(4).HeaderText = "Unit Price" : dgSwitchIn.Columns(4).Width = 80
                    dgSwitchIn.Columns(5).HeaderText = "Valuation Date" : dgSwitchIn.Columns(5).Width = 80
                    dgSwitchIn.Columns(6).HeaderText = "No. of Units" : dgSwitchIn.Columns(6).Width = 90
                    dgSwitchIn.Columns(7).HeaderText = "Total (Fund Ccy)" : dgSwitchIn.Columns(7).Width = 100
                    dgSwitchIn.Columns(8).HeaderText = "Total (PO Ccy)" : dgSwitchIn.Columns(8).Width = 100
                    dgSwitchIn.Columns(9).HeaderText = "Ex. Rate" : dgSwitchIn.Columns(9).Width = 100
                    lblSWI.Visible = True
                    dgSwitchIn.Visible = True
                Else
                    lblSWI.Visible = False
                    dgSwitchIn.Visible = False
                End If

            Else
                If dsReceData.Tables.Contains("FundAlloc") Then
                    lblSWO.Text = "Fund Allocation Details"
                    dgSwitchOut.DataSource = dsReceData.Tables("FundAlloc")

                    dgSwitchOut.Columns(0).HeaderText = "Fund Code"
                    dgSwitchOut.Columns(1).HeaderText = "Fund Name" : dgSwitchOut.Columns(1).Width = 300
                    dgSwitchOut.Columns(2).HeaderText = "Allocation%" : dgSwitchOut.Columns(2).Width = 100

                    dgSwitchIn.DataSource = Nothing

                    lblSWI.Visible = False
                    dgSwitchIn.Visible = False
                End If
            End If

            'ATHL20210303 BAUL check
            'Dim bMsg As Boolean
            'bMsg = False
            'Try
            '    For Each r As DataGridViewRow In dgSwitchOut.Rows
            '        If r.Cells(0).Value.ToString.Trim.Equals("BAUL") Then
            '            bMsg = True
            '        End If
            '    Next
            'Catch ex As Exception

            'End Try
            'Try
            '    For Each r As DataGridViewRow In dgSwitchIn.Rows
            '        If r.Cells(0).Value.ToString.Trim.Equals("BAUL") Then
            '            bMsg = True
            '        End If
            '    Next
            'Catch ex As Exception

            'End Try
            'If bMsg Then
            '    MessageBox.Show("Due to system constrain cannot show 5 digits of unit fund price. Fund BAUL ""Barings Umbrella Fund plc Barings USD Liquidity Fund - Tranche G USD Acc"" 霸菱傘子基金公眾有限公司霸菱美元流動基金-G 類別美元累積the Fund unit display in 10 times than original fund unit, Fund price display in 1/10 of original fund price in CRS & eService system, it will resume once system constrain fixed.")
            'End If
        Else
            MsgBox(strErr)
        End If
    End Sub

    Public Sub ShowRecord()

        If strTxID <> "" Then
            GroupBox1.Visible = False
            GroupBox2.Left = 0
            GroupBox2.Top = 0
            Call ShowDetail()
        End If

    End Sub

End Class
