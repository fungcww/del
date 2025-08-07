'********************************************************************
' Created By:  Peter Lam
' Date:         12 Jan 2016
' Project:      ITSR236
' Ref:          PL20160112
' Changes:      Display Minimum Required Premium of LifeAsia policy via BO
'********************************************************************
' Amend By:  Kay Tsang
' Date:         11 Nov 2016
' Project:      ITSR492
' Ref:          KT20161111
' Changes:      Add visit CS flag panel
'********************************************************************
' Amend By:  Claudia Lai
' Date:         29 Apr 2021
' Project:      ITSR2040
' Ref:          CL20210429
' Changes:      add systemInUse property in ShowPolicyRecord
'********************************************************************

Public Class ctrl_CRSPolicyGeneral_Information
    Private strPolicyNo As String = ""

    Private dsCurr As New DataSet
    Private clsCRS As New LifeClientInterfaceComponent.clsCRS
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Dim dsReceData As New DataSet
    Private dsPolicyHead As New DataSet 'Policy head dataset
    Private EffDate As String = ""  'Effective date  
    Private StrSystemInUse As String
#Region " DBLogon Properties"
    Public Property dbHeader() As Utility.Utility.ComHeader
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
    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property
#End Region

    Public Property SystemInUse() As String
        Get
            Return StrSystemInUse
        End Get
        Set(ByVal value As String)
            StrSystemInUse = value
            Ctrl_POS_PolicyClient1.SystemInUse = StrSystemInUse
        End Set
    End Property
    Public Sub showPolicyGeneralInfo()


        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strMainStartTime As String = Now
        Dim strMainEndTime As String = Now
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now
        strFuncStartTime = Now
        Try
            objMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
            ShowPolicyRecord()

            If dsPolicyHead.Tables.Count > 0 Then
                showPolicyClient()
                'Ctrl_POS_PolicyClient1.EffDateInuse = EffDate
                'Ctrl_POS_PolicyClient1.PolicyNoInuse = strPolicyNo.Trim
                ''Ctrl_POS_PolicyClient1.DBHeader = Me.DBHeader
                'Ctrl_POS_PolicyClient1.MQQueuesHeader = Me.MQQueuesHeader
                'Ctrl_POS_PolicyClient1.sPolicyStatus = dsPolicyHead.Tables(0).Rows(0)("Risk_Sts")
                'Ctrl_POS_PolicyClient1.modeinuse = Utility.Utility.ModeName.Enquiry   'POSCommCtrl.Ctrl_POS_PolicyClient.ModeName.Enquiry
                'Ctrl_POS_PolicyClient1.showClnRelation()
                Ctrl_BillingInfo1.strPolicyNo = strPolicyNo.Trim
                Ctrl_BillingInfo1.MQQueuesHeader = Me.MQQueuesHeader
                Ctrl_BillingInfo1.DBHeader = Me.objDBHeader

                strFuncStartTime = Now
                Ctrl_BillingInfo1.showBillingInfo()
                strFuncEndTime = Now
                SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "showPolicyGeneralInfo", "Ctrl_BillingInfo1.showBillingInfo", strFuncStartTime, strFuncEndTime, strPolicyNo.Trim, "", "")

            End If

        Catch ex As Exception

        Finally
            strMainEndTime = Now
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "showPolicyGeneralInfo", "", strMainStartTime, strMainEndTime, strPolicyNo.Trim, "", "")

        End Try
        'objMQQueHeader.QueueManager = My.Settings.Qman
        'objMQQueHeader.RemoteQueue = My.Settings.WinRemoteQ
        'objMQQueHeader.ReplyToQueue = My.Settings.LAReplyQ
        'objMQQueHeader.LocalQueue = My.Settings.WinLocalQ

    End Sub

    'Public Function passCustomer() As String
    '    passCustomer = "" 'Ctrl_POS_PolicyClient1
    'End Function

    Private Sub ShowPolicyRecord()

        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strMainStartTime As String = Now
        Dim strMainEndTime As String = Now
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now
        strFuncStartTime = Now
        Try
            Ctrl_POS_Scrn_Head1.policyInuse = strPolicyNo.Trim
            Ctrl_POS_Scrn_Head1.dbHeader = Me.dbHeader
            Ctrl_POS_Scrn_Head1.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_POS_Scrn_Head1.SystemInuse = "CRS" 'ITSR2040 WOP by Claudia Lai CL20210429
            Ctrl_POS_Scrn_Head1.ShowPolicyRcd()


            dsPolicyHead = Ctrl_POS_Scrn_Head1.currdsInuse
            If dsPolicyHead.Tables.Count > 0 Then
                EffDate = Format(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), "MMM dd, yyyy")
            End If
        Catch ex As Exception
        Finally
            strMainEndTime = Now
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "ShowPolicyRecord", "", strMainStartTime, strMainEndTime, strPolicyNo.Trim, "", "")

        End Try
        'Try
        'Catch ex As Exception
        '    SysEventLog.CiwHeader = objDBHeader
        '    SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub showPolicyClient()

        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim strMainStartTime As String = Now
        Dim strMainEndTime As String = Now
        Dim strFuncStartTime As String = Now
        Dim strFuncEndTime As String = Now
        strFuncStartTime = Now
        Try
            Ctrl_POS_PolicyClient1.EffDateInuse = EffDate
            'Ctrl_POS_PolicyClient1.PolicyNoInuse = strPolicyNo
            Ctrl_POS_PolicyClient1.PolicyNoInuse = strPolicyNo
            'Ctrl_POS_PolicyClient1.DBHeader = Me.DBHeader
            Ctrl_POS_PolicyClient1.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_POS_PolicyClient1.DBHeader = Me.dbHeader
            Ctrl_POS_PolicyClient1.sPolicyStatus = dsPolicyHead.Tables(0).Rows(0)("Risk_Sts")
            Ctrl_POS_PolicyClient1.modeinuse = Utility.Utility.ModeName.Enquiry  'POSCommCtrl.Ctrl_POS_PolicyClient.ModeName.Enquiry
            Ctrl_POS_PolicyClient1.showClnRelation()

            ' ---- ITSR236 - Show Minimum Required Premium Info
            With Me.Ctrl_POS_MRP1
                .PolicyNo = strPolicyNo
                .EffectiveDate = EffDate
                .dbHeader = Me.dbHeader
                .MQQueuesHeader = Me.MQQueuesHeader
                .ShowPolicyMRP()
            End With

            'KT20161111
            If Ctrl_POS_PolicyClient1.MQQueuesHeader.CompanyID = "ING" Or Ctrl_POS_PolicyClient1.DBHeader.CompanyID = "ING" Then
                With Me.Ctrl_VisitCS1
                    .PolicyNo = strPolicyNo
                    .CustomerID = Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse
                    .Lang = "en"
                    .UpdateUser2 = dbHeader.UserID
                    .checkVisitCSFlag()
                End With
            Else
                Me.Ctrl_VisitCS1.Visible = False
            End If


        Catch ex As Exception
        Finally
            strMainEndTime = Now
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "showPolicyClient", "", strMainStartTime, strMainEndTime, strPolicyNo.Trim, "", "")

        End Try

    End Sub


End Class
