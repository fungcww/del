Public Class uclFundTxHist

    Dim strPolicy As String

    Public WriteOnly Property PolicyAccountID() As String
        Set(ByVal Value As String)
            strPolicy = Value
        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objDT, objFT As DataTable
        Dim strError As String

        If GetFundTransaction(strPolicy, objDT, strError) Then
            objDT.DefaultView.Sort = "Trans. Date"
            dgHist.DataSource = objDT

            dgHist.Columns(0).Visible = False
            dgHist.Columns(1).Visible = False
            dgHist.Columns(4).Visible = True
            dgHist.Columns(5).Visible = False
            dgHist.Columns(2).Width = 150
            dgHist.Columns(3).Width = 90
            dgHist.Columns(4).Width = 80
        End If

        'Call GetFundTransactionDetails("ULB", "2093", "LA", "11050473", objFT, strError)
        'Call GetFundTransactionDetails("ULB", "2176", "LA", "11182558", objFT, strError)
        'Call GetFundTransactionDetails("ULB", "2119", "", "2050005282", objFT, strError)

    End Sub

    Private Sub dgHist_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgHist.SelectionChanged

        Dim strTxID As String
        Dim strTxType As String
        Dim strLA As String
        Dim strError As String
        Dim dsFT As DataSet

        wndMain.Cursor = Cursors.WaitCursor

        If Not Me.dgHist.CurrentRow.Cells("Trans_id").Value Is Nothing AndAlso Not IsDBNull(Me.dgHist.CurrentRow.Cells("Trans_id").Value) Then
            strTxID = Me.dgHist.CurrentRow.Cells("Trans_id").Value
            'strTxType = Me.dgHist.CurrentRow.Cells("Trans. Type").Value
            strTxType = Me.dgHist.CurrentRow.Cells("Source").Value
            strLA = Me.dgHist.CurrentRow.Cells("postq_backend_sys").Value

            If GetFundTransactionDetails(strTxType, strTxID, "LA", strPolicy, dsFT, strError) Then

                If strTxType = "ULB" Then
                    lblSWO.Text = "Switch Out Details"
                    dgSwitchOut.DataSource = dsFT.Tables("SwitchOut")
                    dgSwitchOut.Columns(0).HeaderText = "Fund Code" : dgSwitchOut.Columns(0).Width = 90
                    dgSwitchOut.Columns(1).HeaderText = "Fund Name" : dgSwitchOut.Columns(1).Width = 150
                    dgSwitchOut.Columns(2).HeaderText = "Switch Out %" : dgSwitchOut.Columns(2).Width = 100
                    dgSwitchOut.Columns(3).HeaderText = "Units Hold" : dgSwitchOut.Columns(3).Width = 80
                    dgSwitchOut.Columns(4).HeaderText = "Switch Out Units" : dgSwitchOut.Columns(4).Width = 90
                    dgSwitchOut.Columns(5).HeaderText = "Currency" : dgSwitchOut.Columns(5).Width = 70
                    dgSwitchOut.Columns(6).HeaderText = "Unit Price" : dgSwitchOut.Columns(6).Width = 80
                    dgSwitchOut.Columns(7).HeaderText = "Valuation Date" : dgSwitchOut.Columns(7).Width = 80
                    dgSwitchOut.Columns(8).HeaderText = "Total (Fund Ccy)" : dgSwitchOut.Columns(8).Width = 100

                    dgSwitchIn.DataSource = dsFT.Tables("SwitchIn")
                    dgSwitchIn.Columns(0).HeaderText = "Fund Code" : dgSwitchIn.Columns(0).Width = 90
                    dgSwitchIn.Columns(1).HeaderText = "Fund Name" : dgSwitchIn.Columns(1).Width = 150
                    dgSwitchIn.Columns(2).HeaderText = "Allocation %" : dgSwitchIn.Columns(2).Width = 90
                    dgSwitchIn.Columns(3).HeaderText = "Currency" : dgSwitchIn.Columns(3).Width = 70
                    dgSwitchIn.Columns(4).HeaderText = "Unit Price" : dgSwitchIn.Columns(4).Width = 80
                    dgSwitchIn.Columns(5).HeaderText = "Valuation Date" : dgSwitchIn.Columns(5).Width = 80
                    dgSwitchIn.Columns(6).HeaderText = "No. of Units" : dgSwitchIn.Columns(6).Width = 90
                    dgSwitchIn.Columns(7).HeaderText = "Total (Fund Ccy)" : dgSwitchIn.Columns(7).Width = 100

                    lblSWI.Visible = True
                    dgSwitchIn.Visible = True
                Else
                    lblSWO.Text = "Fund Allocation Details"
                    dgSwitchOut.DataSource = dsFT.Tables("FundAlloc")

                    dgSwitchOut.Columns(0).HeaderText = "Fund Code"
                    dgSwitchOut.Columns(1).HeaderText = "Fund Name" : dgSwitchOut.Columns(1).Width = 300
                    dgSwitchOut.Columns(2).HeaderText = "Allocation%" : dgSwitchOut.Columns(2).Width = 100

                    dgSwitchIn.DataSource = Nothing

                    lblSWI.Visible = False
                    dgSwitchIn.Visible = False
                End If

            End If
        End If

        wndMain.Cursor = Cursors.Default

    End Sub

End Class
