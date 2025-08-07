'********************************************************************
' Admended By: Chopard Chan
' Add Function: Auto & Regular Withdrawal Form & History Enquiry
' Date: 06 July 2020
' Description: Auto & Regular Withdrawal
' Ref No.: ITSR 1607 & 1792
'********************************************************************
' Amendment     : ITSR 3257 ACH account name extension
' Created Date  : 10 Feb 2022
' Created By    : Marco Chan Wan Ho
' ITSR          : ITSR 3257
' Ref           : MC20220210
'******************************************************************

Public Class ctrl_AutoRegularWithdrawal

    Private Const DateFormat = "MM/dd/yyyy"

    Dim strErr As String = ""

    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private strPolicyInUse As String

    Private strAWDStatus As String
    Private strRWDStatus As String
    Private strAWDStatusCode As String
    Private strRWDStatusCode As String

    Private strLAClientNo As String
    Private strCIWClientNo As String
    Private strCIWClientName As String

    Private strOwnerNo As String

    Private enumWithdrawalType As WithdrawalTypeEnum

    Private objPOS As LifeClientInterfaceComponent.clsPOS

    'Marco Chan Wan Ho MC20220210: Get A/C Name and Join A/C Name through MQ
    Dim strJoinACName_splitter As String = "/"
    'End Marco Chan Wan Ho MC20220210: Get A/C Name and Join A/C Name through MQ
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
    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyInUse
        End Get
        Set(ByVal value As String)
            strPolicyInUse = value
        End Set
    End Property
    Public Property OwnerNoInUse() As String
        Get
            Return strOwnerNo
        End Get
        Set(ByVal value As String)
            strOwnerNo = value
        End Set
    End Property
#Region "Enum Type"

    Public Enum ARWStatusEnum As Integer
        Not_Register = 0
        New_ = 1
        Auto_Term = 2
        Manual_Term = 3
        Change_Policy = 4
        None = 5
    End Enum

    Public Enum WithdrawalTypeEnum As Integer
        Auto = 0
        Regular = 1
        NA = 2
    End Enum

#End Region

    Public Sub ShowARWRecord(Optional ByVal bBtnCancelFlag As Boolean = False)

        Try
            'CRS Only
            Ctrl_AutoRegWithdrawal.btnDefault_Auto.Visible = False
            'Marco Chan Wan Ho MC20220210: New control DirectCreditAccountV2 
            'Ctrl_AutoRegWithdrawal.DirectCreditAccount.btnPayeeSel.Visible = False
            'Ctrl_AutoRegWithdrawal.DirectCreditAccount.btnBankSel.Visible = False
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.btnBankSel.Visible = False
            'End Marco Chan Wan Ho MC20220210: New control DirectCreditAccountV2 
            Ctrl_AutoRegWithdrawal.btnDefault_Reg.Visible = False

            '' Set obj header  
            objPOS = New LifeClientInterfaceComponent.clsPOS()
            objPOS.MQQueuesHeader = Me.objMQQueHeader
            objPOS.DBHeader = Me.objDBHeader

            'Marco Chan Wan Ho MC20220210: New control DirectCreditAccountV2 
            'Ctrl_AutoRegWithdrawal.DirectCreditAccount.MQQueuesHeader = Me.objMQQueHeader
            'Ctrl_AutoRegWithdrawal.DirectCreditAccount.DBHeader = Me.objDBHeader
            'Ctrl_AutoRegWithdrawal.DirectCreditAccount.SetPolicyNumber = strPolicyInUse
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.DBHeader = Me.objDBHeader
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.SetPolicyNumber = strPolicyInUse
            'End Marco Chan Wan Ho MC20220210: New control DirectCreditAccountV2 
            '' Set obj header

            Dim dsReceData As New DataSet
            Dim dtAWD As DataTable = Nothing
            Dim dtRWD As DataTable = Nothing

            Dim bIsAWDReg As Boolean = False
            Dim bIsRWDReg As Boolean = False

            strErr = ""
            strAWDStatus = ""
            strRWDStatus = ""

            If GetARWRecord(dsReceData, strErr) Then

                Dim strDTNameAWDDEP As String = "AWDENQDEP"
                Dim strDTNameAWDBAS As String = "AWDENQBAS"
                Dim strDTNameRWD As String = "RWDENQ"

                If Not String.IsNullOrEmpty(strErr) Then
                    'Err Msg Mapping?
                    MsgBox(strErr, MsgBoxStyle.Information, "Alert")
                Else

                    'RWD
                    If Not dsReceData Is Nothing AndAlso dsReceData.Tables.Contains(strDTNameRWD) AndAlso dsReceData.Tables(strDTNameRWD).Rows.Count > 0 Then
                        dtRWD = dsReceData.Tables(strDTNameRWD).Copy
                        PopulateRWDTab(dtRWD)
                        bIsRWDReg = If(Not dtRWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register
                    End If


                    'AWD - Check is Dependent registered first
                    If Not dsReceData Is Nothing AndAlso dsReceData.Tables.Contains(strDTNameAWDDEP) AndAlso dsReceData.Tables(strDTNameAWDDEP).Rows.Count > 0 Then

                        If Not dsReceData.Tables(strDTNameAWDDEP).Rows(0)("Status") = " " Then
                            dtAWD = dsReceData.Tables(strDTNameAWDDEP).Copy
                            PopulateAWDTab(dtAWD)
                            bIsAWDReg = If(Not dtAWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register
                        Else
                            'AWD - Check is Basic registered second
                            If Not dsReceData Is Nothing AndAlso dsReceData.Tables.Contains(strDTNameAWDBAS) AndAlso dsReceData.Tables(strDTNameAWDBAS).Rows.Count > 0 Then

                                dtAWD = dsReceData.Tables(strDTNameAWDBAS).Copy
                                PopulateAWDTab(dtAWD)
                                bIsAWDReg = If(Not dtAWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register

                            End If
                        End If

                    End If

                    'Default Tab index
                    If Not bBtnCancelFlag Then ' True = Button cancel has been clicked
                        If Not bIsAWDReg AndAlso bIsRWDReg Then
                            Ctrl_AutoRegWithdrawal.TabControl1.SelectedIndex = 1
                            enumWithdrawalType = WithdrawalTypeEnum.Regular
                        Else
                            Ctrl_AutoRegWithdrawal.TabControl1.SelectedIndex = 0
                            enumWithdrawalType = WithdrawalTypeEnum.Auto
                        End If
                    Else
                        enumWithdrawalType = WithdrawalTypeEnum.NA
                    End If


                End If ' Else end - If Not String.IsNullOrEmpty(strErr) Then

            Else
                ' Prompt Error
                MsgBox(strErr, MsgBoxStyle.Critical, "Error")
            End If


            'Dim objSchema As New BOSchema.ClnRelation_ContInsured()
        Catch ex As Exception
            HandleExceptionTryCatch(ex)
        End Try


    End Sub

    Private Function GetARWRecord(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean

        Return EnquiryARW(dsReceData, strErr)

    End Function

    Private Function EnquiryARW(ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean

        Return objPOS.EnquiryARW(strPolicyInUse, dsReceData, strErr)

    End Function

    Private Sub PopulateAWDTab(ByVal dtAWD As DataTable)
        Dim drAWD As DataRow = dtAWD.Rows(0)

        strAWDStatusCode = drAWD("Status").ToString.Trim
        Ctrl_AutoRegWithdrawal.txtStatus_Auto.Text = AWDStatusMapping()

        Ctrl_AutoRegWithdrawal.txtBasicPolicyNo_Auto.Text = drAWD("BasicPolicyNo").ToString.Trim
        Ctrl_AutoRegWithdrawal.txtDependentPolicyNo_Auto.Text = drAWD("DependentPolicyNo").ToString.Trim

        'Hidden controls
        Ctrl_AutoRegWithdrawal.txtBasicPlanCode_Auto.Text = drAWD("BasicPlanCode").ToString.Trim
        Ctrl_AutoRegWithdrawal.txtDependentPlanCode_Auto.Text = drAWD("DependentPlanCode").ToString.Trim

        ' strAWDStatusCode = "" equal unregister
        If strAWDStatusCode.Trim.Length > 0 Then

            Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.Format = DateTimePickerFormat.Custom
            If Not drAWD("EffDate") Is DBNull.Value AndAlso Not CDate(drAWD("EffDate")).Year = 9999 AndAlso Not CDate(drAWD("EffDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.CustomFormat = DateFormat
                Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.Text = CDate(drAWD("EffDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.Format = DateTimePickerFormat.Custom
            If Not drAWD("TerminationDate") Is DBNull.Value AndAlso Not CDate(drAWD("TerminationDate")).Year = 9999 AndAlso Not CDate(drAWD("TerminationDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.CustomFormat = DateFormat
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.Text = CDate(drAWD("TerminationDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.txtLastWithdrawalAmt_Auto.Text = drAWD("WithdrawalAmt").ToString.Trim


            ' local var
            'strPolicyNo = txtBasicPolicyNo_Auto.Text
            'strPolicyNoDependent = txtDependentPolicyNo_Auto.Text
            'strPlanCode = txtBasicPlanCode_Auto.Text
            'strPlanCodeDependent = txtDependentPlanCode_Auto.Text

        Else

            Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.Format = DateTimePickerFormat.Custom
            Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.CustomFormat = " "
            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.Format = DateTimePickerFormat.Custom
            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.CustomFormat = " "
            Ctrl_AutoRegWithdrawal.txtLastWithdrawalAmt_Auto.Text = ""
            Ctrl_AutoRegWithdrawal.txtBasicPlanCode_Auto.Text = ""
            Ctrl_AutoRegWithdrawal.txtDependentPlanCode_Auto.Text = ""

        End If

    End Sub

    Private Sub PopulateRWDTab(ByVal dtRWD As DataTable)
        Dim drRWD As DataRow = dtRWD.Rows(0)
        Dim strBankCode As String = ""
        Dim strBankBranchCode As String = ""

        strRWDStatusCode = drRWD("Status").ToString.Trim
        Ctrl_AutoRegWithdrawal.txtStatus_Reg.Text = RWDStatusMapping()

        Ctrl_AutoRegWithdrawal.txtPolicyNo_Reg.Text = drRWD("PolicyNo").ToString.Trim

        'Hidden controls
        Ctrl_AutoRegWithdrawal.txtPlanCode_Reg.Text = drRWD("PlanCode").ToString.Trim

        ' strRWD_Status_Code = "" equal unregister
        If strRWDStatusCode.Trim.Length > 0 Then

            Ctrl_AutoRegWithdrawal.txtWithdrawalAmt_Reg.Text = drRWD("WithdrawalAmt").ToString.Trim

            Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.Format = DateTimePickerFormat.Custom
            If Not drRWD("StaDate") Is DBNull.Value AndAlso Not CDate(drRWD("StaDate")).Year = 9999 AndAlso Not CDate(drRWD("StaDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.CustomFormat = DateFormat
                Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.Text = CDate(drRWD("StaDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.Format = DateTimePickerFormat.Custom
            If Not drRWD("TerminationDate") Is DBNull.Value AndAlso Not CDate(drRWD("TerminationDate")).Year = 9999 AndAlso Not CDate(drRWD("TerminationDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.CustomFormat = DateFormat
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.Text = CDate(drRWD("TerminationDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.Format = DateTimePickerFormat.Custom
            If Not drRWD("EndDate") Is DBNull.Value AndAlso Not CDate(drRWD("EndDate")).Year = 9999 AndAlso Not CDate(drRWD("EndDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.CustomFormat = DateFormat
                Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.Text = CDate(drRWD("EndDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.txtPeriod_Reg.Text = drRWD("Duration").ToString.Trim

            Try
                strBankCode = drRWD("BankKey").ToString.Trim.Substring(0, 3)
                strBankBranchCode = drRWD("BankKey").ToString.Trim.Substring(3)
            Catch ex As Exception

            End Try

            'Marco Chan Wan Ho MC20220210: split Client name to AC name and Join AC Name
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbBankCode.Text = strBankCode
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbBankBranchCode.Text = strBankBranchCode
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbBankAccountNo.Text = drRWD("BankAcc").ToString.Trim
            'End Marco Chan Wan Ho MC20220210: split Client name to AC name and Join AC Name

            ' local var
            'strPolicyNo = txtPolicyNo_Reg.Text
            'strPolicyNoDependent = txtPlanCode_Reg.Text

        Else
            Ctrl_AutoRegWithdrawal.txtWithdrawalAmt_Reg.Text = ""
            Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.Format = DateTimePickerFormat.Custom
            Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.CustomFormat = " "
            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.Format = DateTimePickerFormat.Custom
            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.CustomFormat = " "
            Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.Format = DateTimePickerFormat.Custom
            Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.CustomFormat = " "
            Ctrl_AutoRegWithdrawal.txtPeriod_Reg.Text = ""

            'Marco Chan Wan Ho MC20220210: To be updated
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbBankCode.Text = ""
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbBankBranchCode.Text = ""
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbBankAccountNo.Text = ""
            'End Marco Chan Wan Ho MC20220210: To be updated

        End If

        'Marco Chan Wan Ho MC20220210: get AC Name and Join AC Name from MQ
        'Default payee to owner - Sta
        strLAClientNo = strOwnerNo
        'strCIWClientNo = ""
        'strCIWClientName = ""
        GetCiwNoByClntNum(strLAClientNo, strCIWClientNo, strCIWClientName)

        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.SetLAClientNo = strLAClientNo
        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.CIWClientNo = strCIWClientNo
        GetDirectCreditAccountDetail()
        'End Marco Chan Wan Ho MC20220210: get AC Name and Join AC Name from MQ
        'Default payee to owner - End

        Ctrl_AutoRegWithdrawal.cboWithdrawalFreq_Reg.Text = drRWD("WithdrawalFreq")
        Ctrl_AutoRegWithdrawal.cboWithdrawalFreq_Reg.Enabled = False

    End Sub

    Private Sub GetCiwNoByClntNum(ByVal clntNum As String, ByRef ciwNo As String, ByRef payeeName As String)
        Dim ds As New DataSet
        Dim strErr As String = ""
        If clntNum <> "" Then
            If objPOS.GetCIWMAPByClntNum(ds, clntNum, strErr) Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ciwNo = ds.Tables(0).Rows(0).Item("CIW_NO")
                    payeeName = ds.Tables(0).Rows(0).Item("firstName") & " " & ds.Tables(0).Rows(0).Item("NameSuffix")
                End If
            End If
        Else
            ciwNo = ""
            payeeName = ""
        End If

    End Sub

    'Marco Chan Wan Ho MC20220210: Get A/C Name and Join A/C Name through MQ
    Private Sub GetDirectCreditAccountDetail()
        Dim ds As New DataSet
        Dim strErr As String = String.Empty
        If strPolicyInUse <> String.Empty Then
            If objPOS.DirectCredit("Enquiry", strPolicyInUse, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, ds, strErr) Then
                If ds.Tables.Count > 1 AndAlso ds.Tables(1) IsNot Nothing AndAlso ds.Tables(1).Rows.Count > 0 Then
                    If ds.Tables(1).Rows(0).Item("BankAccountDesc").IndexOf(strJoinACName_splitter) > -1 Then
                        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbACName.Text = ds.Tables(1).Rows(0).Item("BankAccountDesc").split(strJoinACName_splitter)(0)
                        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbJoinACName.Text = ds.Tables(1).Rows(0).Item("BankAccountDesc").split(strJoinACName_splitter)(1)
                    Else
                        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbACName.Text = ds.Tables(1).Rows(0).Item("BankAccountDesc")
                    End If
                End If
            End If
        End If
    End Sub
    'End Marco Chan Wan Ho MC20220210: Get A/C Name and Join A/C Name through MQ

#Region "Mapping"

    Private Function AWDStatusMapping() As String
        Dim strRes As String = ""

        Select Case strAWDStatusCode
            Case ""
                strRes = "Not Register"
                strAWDStatus = ARWStatusEnum.Not_Register.ToString
            Case "N"
                strRes = "New"
                strAWDStatus = ARWStatusEnum.New_.ToString
            Case "A"
                strRes = "Auto-Term"
                strAWDStatus = ARWStatusEnum.Auto_Term.ToString
            Case "M"
                strRes = "Manual Term"
                strAWDStatus = ARWStatusEnum.Manual_Term.ToString
            Case "C"
                strRes = "Change Policy"
                strAWDStatus = ARWStatusEnum.Change_Policy.ToString
        End Select

        Return strRes
    End Function

    Private Function RWDStatusMapping() As String
        Dim strRes As String = ""

        Select Case strRWDStatusCode
            Case ""
                strRes = "Not Register"
                strRWDStatus = ARWStatusEnum.Not_Register.ToString
            Case "N"
                strRes = "New"
                strRWDStatus = ARWStatusEnum.New_.ToString
            Case "A"
                strRes = "Auto-Term"
                strRWDStatus = ARWStatusEnum.Auto_Term.ToString
            Case "M"
                strRes = "Manual Term"
                strRWDStatus = ARWStatusEnum.Manual_Term.ToString
        End Select

        Return strRes
    End Function

    Private Function GetARWStatusCode() As String
        Dim strRes As String = ""

        Select Case strAWDStatus

            Case ARWStatusEnum.Not_Register.ToString
                strRes = ""
            Case ARWStatusEnum.New_.ToString
                strRes = "N"
            Case ARWStatusEnum.Auto_Term.ToString
                strRes = "A"
            Case ARWStatusEnum.Manual_Term.ToString
                strRes = "M"
            Case ARWStatusEnum.Change_Policy.ToString
                strRes = "C"
        End Select

        Return strRes

    End Function

#End Region

    Private Sub TabSelecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs)

        If e.TabPageIndex = 0 Then

            If strRWDStatus = "" Then
                e.Cancel = True
                Exit Sub
            End If

            If strRWDStatus = ARWStatusEnum.New_.ToString Then
                MsgBox("Cannot change to Auto Withdrawal while Regular Withdrawal is active", MsgBoxStyle.Information, "Alert")
                e.Cancel = True
                Exit Sub
            End If

            enumWithdrawalType = WithdrawalTypeEnum.Auto

        ElseIf e.TabPageIndex = 1 Then

            If strAWDStatus = "" Then
                e.Cancel = True
                Exit Sub
            End If

            If strAWDStatus = ARWStatusEnum.New_.ToString Then
                MsgBox("Cannot change to Regular Withdrawal while Auto Withdrawal is active", MsgBoxStyle.Information, "Alert")
                e.Cancel = True
                Exit Sub
            End If

            enumWithdrawalType = WithdrawalTypeEnum.Regular

        End If
    End Sub

    Private Sub UC_TabControl1_Selecting(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ctrl_AutoRegWithdrawal.UC_TabControl1_Selecting
        TabSelecting(sender, e)
    End Sub

#Region "HandleException"
    Private Sub HandleExceptionTryCatch(ByVal ex As Exception)
        strErr = ex.Message + "\n" + ex.StackTrace
        MsgBox(strErr, MsgBoxStyle.Critical)
    End Sub

    Private Sub HandleExceptionTryCatch(ByVal strErr As String)
        MsgBox(strErr, MsgBoxStyle.Critical)
    End Sub
#End Region

End Class
