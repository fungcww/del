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
' Amend By:     Chrysan Cheng
' Date:         17 Feb 2025
' Changes:      CRS performance 2 - PaymentHistory and MinorClaimsHistory
'********************************************************************

Imports System.Threading

Public Class ctrl_AutoRegularWithdrawal

    Private Const DATE_FORMAT = "MM/dd/yyyy"
    Private Const JOIN_AC_NAME_SPLITTER As String = "/" 'Marco Chan Wan Ho MC20220210: Get A/C Name and Join A/C Name through MQ

    Private objPOS As LifeClientInterfaceComponent.clsPOS
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
    Private strBankAcName As String
    Private strBankJoinAcName As String

    Private enumWithdrawalType As WithdrawalTypeEnum = WithdrawalTypeEnum.NA


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
            Return strLAClientNo
        End Get
        Set(ByVal value As String)
            strLAClientNo = value
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

    Public Sub ShowARWRecord()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            'CRS Only
            Ctrl_AutoRegWithdrawal.btnDefault_Auto.Visible = False
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.btnBankSel.Visible = False 'Marco Chan Wan Ho MC20220210: New control DirectCreditAccountV2 
            Ctrl_AutoRegWithdrawal.btnDefault_Reg.Visible = False

            ' Set obj header
            objPOS = New LifeClientInterfaceComponent.clsPOS With {
                .MQQueuesHeader = Me.objMQQueHeader,
                .DBHeader = Me.objDBHeader
            }
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.MQQueuesHeader = Me.objMQQueHeader 'Marco Chan Wan Ho MC20220210: New control DirectCreditAccountV2 
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.DBHeader = Me.objDBHeader
            Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.SetPolicyNumber = strPolicyInUse

            strAWDStatus = ""
            strRWDStatus = ""

            ' disable the control before async loading is complete
            Ctrl_AutoRegWithdrawal.Enabled = False

            ' async load data, the control will be re-enable after loading complete
            ThreadPool.QueueUserWorkItem(AddressOf ShowARWRecordAsync)

        Catch ex As Exception
            HandleExceptionTryCatch(ex)
        Finally
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_AutoRegularWithdrawal", "ShowARWRecord", "", mainStartTime, Now, strPolicyInUse,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub

    ''' <summary>
    ''' Async loading in thread
    ''' </summary>
    Private Sub ShowARWRecordAsync()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Dim dsARWData As New DataSet

        Try
            ' get all related data first
            GetAllRelatedData(dsARWData)

            ' no need further action if control non-Ready/closed
            If Me.Disposing OrElse Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            ' show UI in main thread
            Me.Invoke(
                Sub()
                    ShowUI(dsARWData)
                End Sub
            )

        Catch ex As Exception
            HandleExceptionTryCatch(ex)
        Finally
            ' finally, re-enable the control if control is not closed
            If Not (Me.Disposing OrElse Me.IsDisposed) Then
                Me.Invoke(Sub() Ctrl_AutoRegWithdrawal.Enabled = True)
            End If

            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_AutoRegularWithdrawal", "ShowARWRecordAsync", "", mainStartTime, Now, strPolicyInUse,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub

    ''' <summary>
    ''' Do any time-consuming get data operation
    ''' <br><b>(Do not perform any UI assignment operations in here!!!)</b></br>
    ''' </summary>
    Private Sub GetAllRelatedData(ByRef dsARWData As DataSet)
        Dim strErr As String = String.Empty

        ' get ARW related data
        If Not GetARWRecord(strPolicyInUse, dsARWData, strErr) OrElse Not String.IsNullOrEmpty(strErr) Then
            Throw New Exception(strErr)
        End If

        ' get customer ID
        GetCiwNoByClntNum(strLAClientNo, strCIWClientNo, strCIWClientName)

        ' get A/C Name and Join A/C Name through MQ
        GetDirectCreditAccountDetail(strPolicyInUse, strBankAcName, strBankJoinAcName)
    End Sub

    ''' <summary>
    ''' Do any non-time-consuming UI assignment operation
    ''' </summary>
    Private Sub ShowUI(dsARWData As DataSet)
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim funStartTime As Date = Now

        Dim strDTNameAWDDEP As String = "AWDENQDEP"
        Dim strDTNameAWDBAS As String = "AWDENQBAS"
        Dim strDTNameRWD As String = "RWDENQ"

        Dim bIsAWDReg As Boolean = False
        Dim bIsRWDReg As Boolean = False

        'RWD
        If dsARWData IsNot Nothing AndAlso dsARWData.Tables.Contains(strDTNameRWD) AndAlso dsARWData.Tables(strDTNameRWD).Rows.Count > 0 Then
            funStartTime = Now
            Dim dtRWD As DataTable = dsARWData.Tables(strDTNameRWD).Copy
            PopulateRWDTab(dtRWD)
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_AutoRegularWithdrawal", "ShowUI", "PopulateRWDTab", funStartTime, Now, strPolicyInUse,
                (Now - funStartTime).TotalSeconds, Math.Ceiling((Now - funStartTime).TotalSeconds))
            bIsRWDReg = If(Not dtRWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register
        End If

        'AWD - Check is Dependent registered first
        If dsARWData IsNot Nothing AndAlso dsARWData.Tables.Contains(strDTNameAWDDEP) AndAlso dsARWData.Tables(strDTNameAWDDEP).Rows.Count > 0 Then
            funStartTime = Now
            Dim dtAWD As DataTable = Nothing

            If Not dsARWData.Tables(strDTNameAWDDEP).Rows(0)("Status") = " " Then
                dtAWD = dsARWData.Tables(strDTNameAWDDEP).Copy
                PopulateAWDTab(dtAWD)
                bIsAWDReg = If(Not dtAWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register
            Else
                'AWD - Check is Basic registered second
                If dsARWData IsNot Nothing AndAlso dsARWData.Tables.Contains(strDTNameAWDBAS) AndAlso dsARWData.Tables(strDTNameAWDBAS).Rows.Count > 0 Then

                    dtAWD = dsARWData.Tables(strDTNameAWDBAS).Copy
                    PopulateAWDTab(dtAWD)
                    bIsAWDReg = If(Not dtAWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register

                End If
            End If
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_AutoRegularWithdrawal", "ShowUI", "PopulateAWDTab", funStartTime, Now, strPolicyInUse,
                (Now - funStartTime).TotalSeconds, Math.Ceiling((Now - funStartTime).TotalSeconds))
        End If

        'Default Tab index
        If Not bIsAWDReg AndAlso bIsRWDReg Then
            Ctrl_AutoRegWithdrawal.TabControl1.SelectedIndex = 1
            enumWithdrawalType = WithdrawalTypeEnum.Regular
        Else
            Ctrl_AutoRegWithdrawal.TabControl1.SelectedIndex = 0
            enumWithdrawalType = WithdrawalTypeEnum.Auto
        End If

    End Sub

    Private Function GetARWRecord(policyNo As String, ByRef dsReceData As DataSet, ByRef strErr As String) As Boolean
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Try
            Return objPOS.EnquiryARW(policyNo, dsReceData, strErr)
        Finally
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_AutoRegularWithdrawal", "GetARWRecord", "", mainStartTime, Now, strPolicyInUse,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
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
                Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.CustomFormat = DATE_FORMAT
                Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.Text = CDate(drAWD("EffDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpEffDate_Auto.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.Format = DateTimePickerFormat.Custom
            If Not drAWD("TerminationDate") Is DBNull.Value AndAlso Not CDate(drAWD("TerminationDate")).Year = 9999 AndAlso Not CDate(drAWD("TerminationDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.CustomFormat = DATE_FORMAT
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.Text = CDate(drAWD("TerminationDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Auto.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.txtLastWithdrawalAmt_Auto.Text = drAWD("WithdrawalAmt").ToString.Trim

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
                Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.CustomFormat = DATE_FORMAT
                Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.Text = CDate(drRWD("StaDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpStaDate_Reg.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.Format = DateTimePickerFormat.Custom
            If Not drRWD("TerminationDate") Is DBNull.Value AndAlso Not CDate(drRWD("TerminationDate")).Year = 9999 AndAlso Not CDate(drRWD("TerminationDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.CustomFormat = DATE_FORMAT
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.Text = CDate(drRWD("TerminationDate"))
            Else
                Ctrl_AutoRegWithdrawal.dtpTerminationDate_Reg.CustomFormat = " "
            End If

            Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.Format = DateTimePickerFormat.Custom
            If Not drRWD("EndDate") Is DBNull.Value AndAlso Not CDate(drRWD("EndDate")).Year = 9999 AndAlso Not CDate(drRWD("EndDate")).Year = 1900 Then
                Ctrl_AutoRegWithdrawal.dtpEndDate_Reg.CustomFormat = DATE_FORMAT
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

        'Default payee to owner
        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.SetLAClientNo = strLAClientNo
        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.CIWClientNo = strCIWClientNo

        'Marco Chan Wan Ho MC20220210: get AC Name and Join AC Name from MQ
        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbACName.Text = strBankAcName
        Ctrl_AutoRegWithdrawal.DirectCreditAccountV2.tbJoinACName.Text = strBankJoinAcName

        'Ctrl_AutoRegWithdrawal.cboWithdrawalFreq_Reg.Text = drRWD("WithdrawalFreq")
        'If (drRWD("WithdrawalFreq").ToString().Equals("12")) Then
        '    Ctrl_AutoRegWithdrawal.cboWithdrawalFreq_Reg.Text = "Monthly"
        'End If
        'Ctrl_AutoRegWithdrawal.cboWithdrawalFreq_Reg.Enabled = False

    End Sub

    Private Sub GetCiwNoByClntNum(clntNum As String, ByRef ciwNo As String, ByRef payeeName As String)
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Try
            Dim ds As New DataSet
            Dim strErr As String = ""
            If Not String.IsNullOrEmpty(clntNum) Then
                If objPOS.GetCIWMAPByClntNum(ds, clntNum, strErr) Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ciwNo = ds.Tables(0).Rows(0).Item("CIW_NO")
                        payeeName = ds.Tables(0).Rows(0).Item("firstName") & " " & ds.Tables(0).Rows(0).Item("NameSuffix")
                        Return
                    End If
                End If
            End If

            ciwNo = ""
            payeeName = ""
        Finally
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_AutoRegularWithdrawal", "GetCiwNoByClntNum", "", mainStartTime, Now, strPolicyInUse,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
    End Sub

    'Marco Chan Wan Ho MC20220210: Get A/C Name and Join A/C Name through MQ
    Private Sub GetDirectCreditAccountDetail(policyNo As String, ByRef acName As String, ByRef joinAcName As String)
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now
        Try
            Dim ds As New DataSet
            Dim strErr As String = String.Empty
            If Not String.IsNullOrEmpty(policyNo) Then
                If objPOS.DirectCredit("Enquiry", policyNo, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, ds, strErr) Then
                    If ds IsNot Nothing AndAlso ds.Tables.Count > 1 AndAlso ds.Tables(1).Rows.Count > 0 Then
                        If ds.Tables(1).Rows(0).Item("BankAccountDesc").IndexOf(JOIN_AC_NAME_SPLITTER) > -1 Then
                            acName = ds.Tables(1).Rows(0).Item("BankAccountDesc").ToString.Split(JOIN_AC_NAME_SPLITTER)(0)
                            joinAcName = ds.Tables(1).Rows(0).Item("BankAccountDesc").ToString.Split(JOIN_AC_NAME_SPLITTER)(1)
                        Else
                            acName = ds.Tables(1).Rows(0).Item("BankAccountDesc")
                            joinAcName = Nothing
                        End If
                    End If
                End If
            End If
        Finally
            SysEventLog.WritePerLog(DBHeader.UserID, "CRS_Ctrl.ctrl_AutoRegularWithdrawal", "GetDirectCreditAccountDetail", "", mainStartTime, Now, strPolicyInUse,
                (Now - mainStartTime).TotalSeconds, Math.Ceiling((Now - mainStartTime).TotalSeconds))
        End Try
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
    Private Sub HandleExceptionTryCatch(ex As Exception)
        HandleExceptionTryCatch($"{ex.Message}{Environment.NewLine}{ex.StackTrace}")
    End Sub

    Private Sub HandleExceptionTryCatch(strErr As String)
        If Me.Disposing OrElse Me.IsDisposed Then Return

        If Me.InvokeRequired Then
            Me.Invoke(New Action(Of String)(AddressOf HandleExceptionTryCatch), strErr)
        Else
            MsgBox(strErr, MsgBoxStyle.Critical, "Error")
        End If
    End Sub
#End Region

End Class
