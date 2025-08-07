' ****************************************************************
' Admended By: Flora Leung
' Admended Function: dgvMandateList_SelectionChanged
'                    FormatDgvMandateList
' Added Function:   EnqClientBank
'                   PrepareSendEnqMandateList
'                   PrepareReceEnqMandateList
'                   PrepareReceNewMandateList
' Date: 12 Jan 2012
' Project: Project Leo Goal 3
'********************************************************************


Public Class ctrl_DirectDebitEnq
    Private strClientNo As String = ""
    Private strPolicyNo As String = ""

    Private dsCurr As New DataSet
    Private clsCRS As New LifeClientInterfaceComponent.clsCRS
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
    Private clsComm As New LifeClientInterfaceComponent.CommonControl
    ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objdbHeader As Utility.Utility.ComHeader
    Private objUtility As New Utility.Utility


    'ITDYKT CUP DDA BEGIN
    Const FACTORHOUSE_CUP As String = "22"
    'ITDYKT CUP DDA END

    'Add by ITDSCH on 2016-12-14 Begin
    Private dtPriv As New DataTable
    Private strUPSMenuCtrl As String
    'Add by ITDSCH on 2016-12-14 End


#Region " DBLogon Properties"
    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
#End Region
#Region "MQ Properties"
    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
        End Set
    End Property
#End Region
#Region " Property Setting"
    Public Property dsCurrInUse() As DataSet
        Get
            Return dsCurr
        End Get
        Set(ByVal value As DataSet)
            dsCurr = value
        End Set
    End Property
    Public Property ClientNoInUse() As String
        Get
            Return strClientNo
        End Get
        Set(ByVal value As String)
            strClientNo = value
        End Set
    End Property

    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property
#End Region

    Private Sub FormatDgvMandateList(ByVal dtCust As DataTable)
        Try
            dgvMandateList.Columns.Clear()
            ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
            Dim aCol0 As New Windows.Forms.DataGridViewTextBoxColumn
            ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End
            Dim aCol1 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol2 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol3 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol4 As New Windows.Forms.DataGridViewTextBoxColumn
            Dim aCol5 As New Windows.Forms.DataGridViewTextBoxColumn

            ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
            With aCol0
                .DataPropertyName = "Policy_No"
                .Name = "Policy_No"
                .HeaderText = "Policy No."
                .Width = 100
                .ReadOnly = False
            End With
            ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End

            With aCol1
                .DataPropertyName = "Mandate_Ref"
                .Name = "Mandate_Ref"
                .HeaderText = "Mandate Ref"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol2
                .DataPropertyName = "Eff_Date"
                .Name = "Eff_Date"
                .HeaderText = "Effective Date"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol3
                .DataPropertyName = "Bank_Key"
                .Name = "Bank_Key"
                .HeaderText = "Bank Key"
                .Width = 100
                .ReadOnly = False
            End With

            With aCol4
                .DataPropertyName = "Acct_Num"
                .HeaderText = "Account No."
                .Name = "Acct_Num"
                .Width = 200
                .ReadOnly = False
            End With

            With aCol5
                .DataPropertyName = "Status_Cd"
                .Name = "Status_Cd"
                .HeaderText = "Status"
                .Width = 200
                .ReadOnly = False
            End With


            With dgvMandateList.Columns
                ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
                .Add(aCol0)
                ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End
                .Add(aCol1)
                .Add(aCol2)
                .Add(aCol3)
                .Add(aCol4)
                .Add(aCol5)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function PrepareSendMandateListPolicy(ByRef dsSendData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("Policy_No")
            dr("Policy_No") = strPolicyNo
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceMandateListPolicy(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Client_No")
            dtReceData.Columns.Add("Effective From")
            dtReceData.Columns.Add("Effective To")
            dtReceData.Columns.Add("Name")
            dtReceData.Columns.Add("Code")
            dtReceData.Columns.Add("Description")

            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareSendPayerMandateList(ByRef dsSendData As DataSet, ByRef dsReceDataPolicy As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtSendData As New DataTable
            dtSendData.Columns.Add("Client_No")
            For i As Integer = 0 To dsReceDataPolicy.Tables(1).Rows.Count - 1
                If GetRole(dsReceDataPolicy.Tables(1).Rows(i)("Role").ToString) = "P" Then
                    Dim dr As DataRow = dtSendData.NewRow()
                    dr("Client_No") = dsReceDataPolicy.Tables(1).Rows(i)("Client_No").ToString
                    strClientNo = dsReceDataPolicy.Tables(1).Rows(i)("Client_No").ToString()
                    dtSendData.Rows.Add(dr)
                End If
            Next
            dsSendData.Tables.Add(dtSendData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Function GetRole(ByVal strRole) As String
        Try
            Dim charRole As Char = ""
            Select Case strRole.ToString.ToUpper.Trim
                Case "ASSIGNEE"
                    Return "A"
                Case "OWNER"
                    Return "O"
                Case "BENEFICIARY"
                    Return "B"
                Case "PAYER"
                    Return "P"
                Case "DESP ADDR"
                    Return "D"
                Case "LIFE ASSRD"
                    Return "I"
                Case Else
                    'MsgBox("No role is matched!")
                    Return "E"
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
            Return "E"
        End Try
    End Function

    Private Function PrepareSendMandateList(ByRef dsSendData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("Client_No")
            dr("Client_No") = strClientNo
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceMandateList(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Eff_Date")
            dtReceData.Columns.Add("Bank_Key")
            dtReceData.Columns.Add("Acct_Num")
            dtReceData.Columns.Add("Mandate_Ref")
            dtReceData.Columns.Add("Status_Cd")

            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function


    Private Function PrepareSendDirDebitEnq(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            Dim dr As DataRow = dtReceData.NewRow()
            dtReceData.Columns.Add("Client_No")
            dtReceData.Columns.Add("Mandate_Ref")
            dr("Client_No") = strClientNo
            dr("Mandate_Ref") = dgvMandateList.Item("Mandate_Ref", dgvMandateList.CurrentRow.Index).ToString
            dtReceData.Rows.Add(dr)

            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareReceDirDebitEnq(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Payor_No")
            dtReceData.Columns.Add("Payor_Name")
            dtReceData.Columns.Add("Mandate_Ref")
            dtReceData.Columns.Add("Status_Cd")
            dtReceData.Columns.Add("Status_Desc")
            dtReceData.Columns.Add("Times_To_Use")

            dtReceData.Columns.Add("Eff_Date")
            dtReceData.Columns.Add("Bank_Cd")
            dtReceData.Columns.Add("Bank_Loc")
            dtReceData.Columns.Add("Bank_name")
            dtReceData.Columns.Add("Acct_No")
            dtReceData.Columns.Add("Acct_name")

            dtReceData.Columns.Add("Currency")
            dtReceData.Columns.Add("Curr Desc")
            dtReceData.Columns.Add("Factor_Hse")
            dtReceData.Columns.Add("Factor_Hse_Desc")
            dtReceData.Columns.Add("Mandate_Type")
            dtReceData.Columns.Add("Mandate_Amt")
            dtReceData.Columns.Add("Appr_No")
            dtReceData.Columns.Add("Detail_Deb_Amt")

            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Sub ShowMandateListRcd(ByVal _dtPriv As DataTable, ByVal _strUPSMenuCtrl As String)
        Try

            'Update by ITDSCH on 2016-12-14 Begin
            dtPriv = _dtPriv
            strUPSMenuCtrl = _strUPSMenuCtrl
            'Update by ITDSCH on 2016-12-14 End


            Dim strErr As String = ""

            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            clsPOS.MQQueuesHeader = Me.objMQQueHeader
            'create send dataset
            Dim dsSendData As New DataSet
            Dim dsReceData As New DataSet
            If False Then   'this part use for Client No.
                'Prepare tables for transaction
                If PrepareSendMandateList(dsSendData, strErr) = False Then
                    MsgBox(strErr)
                End If
                If PrepareReceMandateList(dsReceData, strErr) = False Then
                    MsgBox(strErr)
                End If

                'Getting Mandate List
                clsCRS.MQQueuesHeader = Me.objMQQueHeader
                If clsCRS.getMandateList(dsSendData, dsReceData, strErr) = False Then
                    MsgBox(strErr)
                End If
            Else

                'Prepare tables for transaction
                Dim dsSendDataPolicy As New DataSet
                Dim dsReceDataPolicy As New DataSet
                If PrepareSendMandateListPolicy(dsSendDataPolicy, strErr) = False Then
                    MsgBox(strErr)
                End If
                If PrepareReceMandateListPolicy(dsReceDataPolicy, strErr) = False Then
                    MsgBox(strErr)
                End If

                'Getting Client List
                clsPOS.MQQueuesHeader = Me.objMQQueHeader
                clsPOS.DBHeader = Me.DBHeader
                If clsPOS.GetClientRole(dsSendDataPolicy, dsReceDataPolicy, strErr) = False Then
                    MsgBox(strErr)
                End If

                If PrepareSendPayerMandateList(dsSendData, dsReceDataPolicy, strErr) = False Then
                    MsgBox(strErr)
                End If
                If PrepareReceMandateList(dsReceData, strErr) = False Then
                    MsgBox(strErr)
                End If

                'Getting Mandate List
                clsCRS.MQQueuesHeader = Me.objMQQueHeader
                If clsCRS.getMandateList(dsSendData, dsReceData, strErr) = False Then
                    MsgBox(strErr)
                End If
            End If

            ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start

            'dgvMandateList.DataSource = Nothing
            'If dsReceData.Tables(0).Rows.Count > 0 Then
            '    'dgvMandateList.DataMember = "Table1"
            '    FormatDgvMandateList(dsReceData.Tables(1))
            '    dgvMandateList.DataSource = dsReceData.Tables(1)
            '    dgvMandateList.Columns("Mandate_Ref").DisplayIndex = 0
            '    dgvMandateList.Columns("Eff_Date").DisplayIndex = 1
            '    dgvMandateList.Columns("Bank_Key").DisplayIndex = 2
            '    dgvMandateList.Columns("Acct_Num").DisplayIndex = 3
            '    dgvMandateList.Columns("Status_Cd").DisplayIndex = 4
            'End If

            Dim dsReceNewData As New DataSet
            PrepareReceNewMandateList(dsReceNewData, strErr)

            If dsReceData.Tables(0).Rows.Count > 0 Then
                For Each oRow As DataRow In dsReceData.Tables(1).Rows

                    Dim dsSendEnqData As New DataSet
                    Dim dsReceEnqData As New DataSet

                    'Prepare tables for transaction
                    PrepareSendEnqMandateList(dsSendEnqData, strClientNo, oRow.Item("Mandate_Ref").ToString())
                    PrepareReceEnqMandateList(dsReceEnqData)

                    'Getting Enq Mandate List
                    clsComm.MQHeader = Me.objMQQueHeader
                    If clsComm.GetMandate(dsSendEnqData, dsReceEnqData, strErr) = False Then
                        MsgBox(strErr)
                    End If

                    Dim dr As DataRow = dsReceNewData.Tables(0).NewRow()

                    If dsReceEnqData.Tables(0).Rows.Count > 0 Then
                        dr.Item("Policy_No") = dsReceEnqData.Tables(0).Rows(0).Item("CHDRNUM").ToString.Trim
                    Else
                        dr.Item("Policy_No") = ""
                    End If

                    dr.Item("Eff_Date") = oRow.Item("Eff_Date")
                    dr.Item("Bank_Key") = oRow.Item("Bank_Key")
                    dr.Item("Acct_Num") = MaskedCreditCardIn64(oRow.Item("Acct_Num").ToString.Trim) 
                    dr.Item("Mandate_Ref") = oRow.Item("Mandate_Ref")
                    dr.Item("Status_Cd") = oRow.Item("Status_Cd")

                    dsReceNewData.Tables(0).Rows.Add(dr)
                Next
            End If

            dgvMandateList.DataSource = Nothing

            If dsReceNewData.Tables(0).Rows.Count > 0 Then
                FormatDgvMandateList(dsReceNewData.Tables(0))

                'Update by ITDSCH on 2016-12-15 Begin
                'CRS 7x24 Changes - Start
                'dgvMandateList.DataSource = dsReceNewData.Tables(0)
                'EUComHeaderInUse = Me.objdbHeader
                'CheckExtranalUser(Me.objdbHeader.UserID, ExternalUser, strErr)
                'If ExternalUser Then
                '    dgvMandateList.DataSource = MaskDTsource(dsReceNewData.Tables(0), "Acct_Num", MaskData.BANK_ACCOUNT_NO)
                'Else
                '    dgvMandateList.DataSource = dsReceNewData.Tables(0)
                'End If
                'CRS 7x24 Changes - End

                If CheckUPSAccess("CRS_PCIDSS_CREDITCARD") Then
                    dgvMandateList.DataSource = dsReceNewData.Tables(0)
                Else
                    clsCRS.MQQueuesHeader = Me.objMQQueHeader


                    

                    Dim dtMatchData As New DataTable
                    dtMatchData.Columns.Add("Acct_No")
                    dtMatchData.Columns.Add("Factor_Hse")

                    For i As Integer = 0 To dsReceNewData.Tables(0).Rows.Count - 1
                        Dim newdsReceData As New DataSet
                        Dim newdsSendData As New DataSet
                        Dim newdtSendData As New DataTable
                        newdtSendData.Columns.Add("Client_No")
                        newdtSendData.Columns.Add("Mandate_Ref")

                        Dim dr As DataRow = newdtSendData.NewRow()

                        dr("Client_No") = strClientNo
                        dr("Mandate_Ref") = dsReceNewData.Tables(0).Rows(i)("Mandate_Ref")
                        newdtSendData.Rows.Add(dr)
                        newdsSendData.Tables.Add(newdtSendData)

                        If clsCRS.getDirDebitEnq(newdsSendData, newdsReceData, strErr) = False Then
                            MsgBox(strErr)
                            Exit Sub
                        End If

                        Dim drMatchData As DataRow = dtMatchData.NewRow()
                        drMatchData("Acct_No") = newdsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim
                        drMatchData("Factor_Hse") = newdsReceData.Tables(0).Rows(0)("Factor_Hse").ToString.Trim
                        dtMatchData.Rows.Add(drMatchData)

                    Next

                    
                    dgvMandateList.DataSource = MaskDTCreditCard(dsReceNewData.Tables(0), "Acct_Num", dtMatchData)
                End If
                'Update by ITDSCH on 2016-12-15 End

                dgvMandateList.Columns("Policy_No").DisplayIndex = 0
                dgvMandateList.Columns("Mandate_Ref").DisplayIndex = 1
                dgvMandateList.Columns("Eff_Date").DisplayIndex = 2
                dgvMandateList.Columns("Bank_Key").DisplayIndex = 3
                dgvMandateList.Columns("Acct_Num").DisplayIndex = 4
                dgvMandateList.Columns("Status_Cd").DisplayIndex = 5
            End If

            ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End  

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub dgvMandateList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMandateList.SelectionChanged
        Try
            Dim cControl As Control
            For Each cControl In Me.Controls 'InMe.Controls            
                If (TypeOf cControl Is TextBox) Then
                    cControl.Text = ""
                End If
            Next cControl
            Dim i As Integer = 0

            Dim cm As CurrencyManager
            cm = CType(Me.BindingContext(dgvMandateList.DataSource), CurrencyManager)

            Dim strErr As String = ""
            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            'create send dataset
            Dim dsSendData As New DataSet
            Dim dsReceData As New DataSet

            'Prepare tables for transaction
            Dim dtSendData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("Client_No")
            dtSendData.Columns.Add("Mandate_Ref")
            dr("Client_No") = strClientNo
            dr("Mandate_Ref") = cm.Current.row.Item("Mandate_Ref")
            dtSendData.Rows.Add(dr)

            dsSendData.Tables.Add(dtSendData)

            'If PrepareReceDirDebitEnq(dsReceData, strErr) = False Then
            ' MsgBox(strErr)
            ' End If

            'Direct Debit Enquiry
            clsCRS.MQQueuesHeader = Me.objMQQueHeader
            If clsCRS.getDirDebitEnq(dsSendData, dsReceData, strErr) = False Then
                MsgBox(strErr)
                Exit Sub
            End If

            If dsReceData.Tables(0).Rows.Count > 0 Then
                txtPayorNo.Text = dsReceData.Tables(0).Rows(0)("Payor_No").ToString.Trim
                txtPayorName.Text = dsReceData.Tables(0).Rows(0)("Payor_Name").ToString.Trim
                txtMandateRef.Text = dsReceData.Tables(0).Rows(0)("Mandate_Ref").ToString.Trim
                txtStatusCd.Text = dsReceData.Tables(0).Rows(0)("Status_Cd").ToString.Trim
                txtStatusDesc.Text = dsReceData.Tables(0).Rows(0)("Status_Desc").ToString.Trim
                dtpEffDate.Value = dsReceData.Tables(0).Rows(0)("Eff_Date")
                dtpSubmitDate.Value = dsReceData.Tables(0).Rows(0)("submit_Date")
                dtpEndDate.Value = dsReceData.Tables(0).Rows(0)("End_Date")
                txtBankCd.Text = dsReceData.Tables(0).Rows(0)("Bank_Cd").ToString.Trim
                txtBankLoc.Text = dsReceData.Tables(0).Rows(0)("Bank_Loc").ToString.Trim
                txtBankName.Text = dsReceData.Tables(0).Rows(0)("Bank_Name").ToString.Trim

                'Update by ITDSCH on 2016-12-14 Begin
                'CRS 7x24 Changes - Start
                'txtBankAcct.Text = dsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim
                'If ExternalUser And Convert.ToString(dsReceData.Tables(0).Rows(0)("Acct_No")).Trim() <> "" Then
                '    txtBankAcct.Text = MaskExternalUserData(MaskData.BANK_ACCOUNT_NO, dsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim)
                'Else
                '    txtBankAcct.Text = dsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim
                'End If
                'CRS 7x24 Changes - End

                If CheckUPSAccess("CRS_PCIDSS_CREDITCARD") Then
                    txtBankAcct.Text = dsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim
                Else
                    Dim factorHse As String = dsReceData.Tables(0).Rows(0)("Factor_Hse").ToString.Trim
                    If factorHse = "02" OrElse factorHse = "03" OrElse factorHse = "04" OrElse factorHse = "22" OrElse factorHse = "26" Then
                        txtBankAcct.Text = MaskedCreditCard(dsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim)
                    End If
                End If
                'Update by ITDSCH on 2016-12-14 End

                ' Force to mask credit card number
                txtBankAcct.Text = MaskedCreditCardIn64(dsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim)

                txtCurr.Text = dsReceData.Tables(0).Rows(0)("Currency").ToString.Trim
                txtMandateAmt.Text = dsReceData.Tables(0).Rows(0)("Mandate_Amt").ToString.Trim

                ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
                ' txtMandateType.Text = dsReceData.Tables(0).Rows(0)("Mandate_Type").ToString.Trim
                Dim strExpMM As String = ""
                Dim strExpYY As String = ""

                If EnqClientBank(strClientNo, dsReceData.Tables(0).Rows(0)("Acct_No").ToString.Trim, strExpMM, strExpYY, strErr) Then
                    txtMM.Text = strExpMM
                    txtYY.Text = strExpYY
                Else
                    txtMM.Text = ""
                    txtYY.Text = ""
                End If


                'ITDYKT CUP DDA BEGIN
                txtFactorHse.Text = dsReceData.Tables(0).Rows(0)("Factor_Hse").ToString.Trim

                If txtFactorHse.Text = FACTORHOUSE_CUP Then

                    txtCUPPayorName.Text = GetPayorChineseName(dsReceData.Tables(0).Rows(0)("Payor_No").ToString.Trim)
                    If ExternalUser And Convert.ToString(dsReceData.Tables(0).Rows(0)("CUPAccountNumber")).Trim() <> "" Then
                        txtCUPAcctNo.Text = MaskExternalUserData(MaskData.BANK_ACCOUNT_NO, dsReceData.Tables(0).Rows(0)("CUPAccountNumber").ToString.Trim)
                    Else
                        txtCUPAcctNo.Text = dsReceData.Tables(0).Rows(0)("CUPAccountNumber").ToString.Trim
                    End If

                    txtCUPBranch.Text = dsReceData.Tables(0).Rows(0)("CUPBankBranch").ToString.Trim
                    txtCUPCity.Text = dsReceData.Tables(0).Rows(0)("CUPCity").ToString.Trim
                    txtCUPProvince.Text = dsReceData.Tables(0).Rows(0)("CUPProvince").ToString.Trim

                End If
                SetFieldControls()
                'ITDYKT CUP DDA END


                'Prepare Reject Reason Code tables for transaction
                Dim dsSendRejData As New DataSet
                Dim dsReceRejData As New DataSet
                Dim dtSendRejData As New DataTable
                Dim drRej As DataRow = dtSendRejData.NewRow()

                dtSendRejData.Columns.Add("Policy_No")
                dtSendRejData.Columns.Add("Mandate_Ref")
                drRej("Policy_No") = strPolicyNo
                drRej("Mandate_Ref") = cm.Current.row.Item("Mandate_Ref").ToString.Trim
                dtSendRejData.Rows.Add(drRej)

                dsSendRejData.Tables.Add(dtSendRejData)

                clsCRS.DBHeader = Me.objdbHeader
                If clsCRS.GetDDRRejectReason(dsSendRejData, dsReceRejData, strErr) = False Then
                    MsgBox(strErr)
                    Exit Sub
                End If

                If IsNothing(dsSendRejData.Tables(0)) = False Then
                    If dsReceRejData.Tables(0).Rows.Count > 0 Then
                        txtInitialRejectCode.Text = dsReceRejData.Tables(0).Rows(0).Item("Rej_Code")
                        txtInitialRejectDesc.Text = dsReceRejData.Tables(0).Rows(0).Item("Rej_Code_Desc")
                    Else
                        txtInitialRejectCode.Text = ""
                        txtInitialRejectDesc.Text = ""
                    End If
                Else
                    txtInitialRejectCode.Text = ""
                    txtInitialRejectDesc.Text = ""
                End If



                ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End
                End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'ITDYKT CUP DDA BEGIN
    Private Sub SetFieldControls()

        If txtFactorHse.Text.Trim = FACTORHOUSE_CUP Then
            Me.txtCUPPayorName.Visible = True
            Me.txtCUPAcctNo.Visible = True
            Me.txtCUPBranch.Visible = True
            Me.txtCUPCity.Visible = True
            Me.txtCUPProvince.Visible = True
            Me.lblCUPBranch.Visible = True
            Me.lblCUPProvince.Visible = True

            Me.txtPayorName.Visible = False
            Me.txtBankAcct.Visible = False
            Me.txtBankLoc.Visible = False

        Else
            Me.txtCUPPayorName.Visible = False
            Me.txtCUPAcctNo.Visible = False
            Me.txtCUPBranch.Visible = False
            Me.txtCUPCity.Visible = False
            Me.txtCUPProvince.Visible = False
            Me.lblCUPBranch.Visible = False
            Me.lblCUPProvince.Visible = False

            Me.txtPayorName.Visible = True
            Me.txtBankAcct.Visible = True
            Me.txtBankLoc.Visible = True
        End If
    End Sub

    Private Function GetPayorChineseName(ByVal strClientID As String) As String
        Dim strChineseName As String = ""

        Try

            clsComm.MQHeader = Me.MQQueuesHeader
            Dim dtCust As New DataTable

            dtCust = clsComm.GetCustomerByLaId(Me.DBHeader.CompanyID, Me.DBHeader.UserID, Me.DBHeader.EnvironmentUse, strClientID)


            If dtCust IsNot Nothing AndAlso dtCust.Rows.Count > 0 Then
                strChineseName = dtCust.Rows(0)("ChiLstNm") & dtCust.Rows(0)("ChiFstNm") & ""
            End If


            Return strChineseName

        Catch ex As Exception
            MsgBox(ex)
        Finally

        End Try

    End Function
    'ITDYKT CUP DDA END


    Private Sub ctrl_DirectDebitEnq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 Start
    ' Show Expiry Date
    Private Function EnqClientBank(ByVal strClientNumber As String, ByVal strBankAccNo As String, ByRef strExpMM As String, ByRef strExpYY As String, ByRef strErr As String) As Boolean
        Try
            Dim dsSendDataClientBank As New DataSet
            Dim dtSendDataClientBank As New DataTable
            Dim dsReceDataClientBank As New DataSet

            dtSendDataClientBank.Columns.Add("ClientNo")
            Dim drClientBank As DataRow = dtSendDataClientBank.NewRow()
            drClientBank("ClientNo") = strClientNumber
            dtSendDataClientBank.Rows.Add(drClientBank)
            dsSendDataClientBank.Tables.Add(dtSendDataClientBank)

            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("RowCount")
            dtReceData.Columns.Add("ClientNo")
            dtReceData.Columns.Add("FactoryHouse")
            dtReceData.Columns.Add("BankCode")
            dtReceData.Columns.Add("BankAccountNo")
            dtReceData.Columns.Add("SecurityCode")
            dtReceData.Columns.Add("BankAccountDesc")
            dtReceData.Columns.Add("BankAccType")
            dtReceData.Columns.Add("AccCurr")
            dtReceData.Columns.Add("EffFrom")
            dtReceData.Columns.Add("EffTo")
            dtReceData.Columns.Add("ExpiryMM")
            dtReceData.Columns.Add("ExpiryYY")
            dsReceDataClientBank.Tables.Add(dtReceData)

            Dim objCCL As New LifeClientInterfaceComponent.CommonControl
            objCCL.MQHeader = Me.objMQQueHeader
            If objCCL.EnqClientBank(dsSendDataClientBank, dsReceDataClientBank, strErr) = False Then
                strErr = "GetCreditCardExpiry False : " & strErr
                Return False
            End If

            If dsReceDataClientBank.Tables.Count > 1 Then
                If dsReceDataClientBank.Tables(1).Rows.Count > 0 Then
                    Dim nRows() As DataRow
                    nRows = dsReceDataClientBank.Tables(1).Select("ClientNo = '" & strClientNumber & "' and BankAccountNo = '" & strBankAccNo & "'")

                    If nRows.Length > 0 Then
                        strExpMM = nRows(0).Item("ExpiryMM")
                        strExpYY = nRows(0).Item("ExpiryYY")
                    End If
                End If

            End If

            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    '  Show Policy No in Mandate List
    Private Function PrepareSendEnqMandateList(ByRef dsSendData As DataSet, ByVal strClientNo As String, ByVal strMandateRef As String) As Boolean
        Try
            Dim dtSendData As New DataTable

            dtSendData.Columns.Add("Client_No")
            dtSendData.Columns.Add("Mandate_Ref")

            dtSendData.TableName = "SendEnqMandateList"
            dsSendData.Tables.Add(dtSendData)


            ' Dim dtSendEnqData As New DataTable
            Dim dr As DataRow = dtSendData.NewRow()
            dr("Client_No") = strClientNo
            dr("Mandate_Ref") = strMandateRef
            dtSendData.Rows.Add(dr)

            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Function PrepareReceEnqMandateList(ByRef dsReceData As DataSet) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Acct_Desc")
            dtReceData.Columns.Add("Acct_Num")
            dtReceData.Columns.Add("Bank_Desc")
            dtReceData.Columns.Add("Bank_Key")
            dtReceData.Columns.Add("Branch_Desc")
            dtReceData.Columns.Add("Cur_Desc")

            dtReceData.Columns.Add("Cur_Code")
            dtReceData.Columns.Add("DetlSumm")
            dtReceData.Columns.Add("Eff_Date")
            dtReceData.Columns.Add("End_Date")
            dtReceData.Columns.Add("Fact_House")
            dtReceData.Columns.Add("MandateAmt")

            dtReceData.Columns.Add("MANDAMTTYP")
            dtReceData.Columns.Add("MANDREF")
            dtReceData.Columns.Add("MANDSTAT")
            dtReceData.Columns.Add("Owner_Name")
            dtReceData.Columns.Add("Payer_Num")
            dtReceData.Columns.Add("STATDETS")
            dtReceData.Columns.Add("TIMEINUSE")
            dtReceData.Columns.Add("SDDA")
            dtReceData.Columns.Add("Submission_Date")
            dtReceData.Columns.Add("BKCARDNUM")
            dtReceData.Columns.Add("FHDESC")
            dtReceData.Columns.Add("CHDRNUM")
            dtReceData.Columns.Add("ZDDAREFQA")

            dtReceData.TableName = "ReceEnqMandateList"
            dsReceData.Tables.Add(dtReceData)

            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Function PrepareReceNewMandateList(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Try
            Dim dtReceData As New DataTable
            dtReceData.Columns.Add("Policy_No")
            dtReceData.Columns.Add("Eff_Date")
            dtReceData.Columns.Add("Bank_Key")
            dtReceData.Columns.Add("Acct_Num")
            dtReceData.Columns.Add("Mandate_Ref")
            dtReceData.Columns.Add("Status_Cd")

            dtReceData.TableName = "NewMandateList"
            dsReceData.Tables.Add(dtReceData)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    ' Flora Leung, Project Leo Goal 3, 12-Jan-2012 End

    ''' <summary>
    ''' Add by ITDSCH on 2016-12-14
    ''' </summary>
    ''' <param name="strTabName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckUPSAccess(ByVal strTabName As String) As Boolean
        Dim dr() As DataRow = dtPriv.Select("upsmit_desc = '" & strTabName & "'")
        If dr.Length > 0 Then
            If Mid(strUPSMenuCtrl, dr(0).Item("upsmit_seq_no"), 1) = "1" Then
                Return True
            End If
        End If
        Return False
    End Function

End Class
