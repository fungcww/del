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
' Amend By:     Chrysan Cheng
' Date:         12 Nov 2024
' Changes:      CRS performer slowness - Policy Summary
'********************************************************************
' Amend By:     Chrysan Cheng
' Date:         07 Mar 2025
' Changes:      INC0325215 - Fix ClientMaintain screen UHNW indicator issue
'********************************************************************

Public Class ctrl_CRSPolicyGeneral_Information
    Private strPolicyNo As String = ""

    Private clsCRS As New LifeClientInterfaceComponent.clsCRS
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Dim dsReceData As New DataSet
    Private dsPolicyHead As New DataSet     ' Policy head dataset
    Private EffDate As String = ""          ' Effective date
    Private PolicyStatus As String = ""     ' Policy status
    Private OwnerCustomerID As String = ""  ' Owner CIW NO
    Private StrSystemInUse As String
    Private blnIsTAD2C As Boolean
    Private IsAsyncLoadForm As Boolean = False                      ' CRS performer slowness - Indicates whether this control will be loaded asynchronously
    Private countDownSemaphore As Threading.Semaphore = Nothing     ' CRS performer slowness - Count down completed threads in asynchronous mode
    Private Const COUNT_DOWN_THREAD_NUM As Integer = 4              ' CRS performer slowness - Total number of started threads -1

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
    ''' <summary>
    ''' The policy header data that is currently in use
    ''' </summary>
    Public Property dsCurrInUse() As DataSet
        Get
            Return dsPolicyHead
        End Get
        Set(ByVal value As DataSet)
            dsPolicyHead = value
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

    Public WriteOnly Property IsTAD2C() As Boolean
        Set(ByVal Value As Boolean)
            blnIsTAD2C = Value
        End Set
    End Property

    ''' <summary>
    ''' Indicate whether the current login user has UHNW(VVIP) right (isUHNWMember)
    ''' </summary>
    Public Property UHNWRightFlag As Boolean

    ''' <summary>
    ''' Event raised when all sub contorls have loaded the data
    ''' </summary>
    Public Event LoadDataCompleted As EventHandler

    ''' <summary>
    ''' Set policy summary page related data externally (if have)
    ''' </summary>
    Public Sub SetPolicyDataForAsync(dsPolicyHeader As DataSet, dataDict As Dictionary(Of String, DataSet))
        Me.IsAsyncLoadForm = dsPolicyHeader IsNot Nothing AndAlso dsPolicyHeader.Tables.Count > 0

        ' set related data externally
        Ctrl_POS_Scrn_Head1.PolicyDataInuse.dsPolicy = dsPolicyHeader

        Dim dsPolicyData As DataSet = Nothing
        If dataDict?.TryGetValue("dsPolicyData", dsPolicyData) Then
            Ctrl_POS_Scrn_Head1.PolicyDataInuse.dsAgentInfo = TrySeparateDataSet(dsPolicyData, "AgentInfo")
            Ctrl_POS_Scrn_Head1.PolicyDataInuse.dsLevyNextQuotation = TrySeparateDataSet(dsPolicyData, "LevyQuotation")
            Ctrl_POS_Scrn_Head1.PolicyDataInuse.dsCIWOwnerInsured = TrySeparateDataSet(dsPolicyData, "OwnerInsured")
        End If

        If dataDict?.ContainsKey("dsClientRole") Then
            Ctrl_POS_PolicyClient1.OriginalClientRoleData = dataDict.Item("dsClientRole")
        End If

        If dataDict?.ContainsKey("dsPolicyClientSysInfo") Then
            Ctrl_POS_PolicyClient1.PolicyClientSysInfoData = dataDict.Item("dsPolicyClientSysInfo")
        End If

        ' set policy header dependencies in advance
        If Me.IsAsyncLoadForm Then
            dsPolicyHead = dsPolicyHeader
            EffDate = Format(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), "MMM dd, yyyy")
            PolicyStatus = dsPolicyHead.Tables(0).Rows(0)("Risk_Sts")
            OwnerCustomerID = GetOwnerCiwNoFromTargetDataSet(Ctrl_POS_Scrn_Head1.PolicyDataInuse.dsCIWOwnerInsured)
        End If
    End Sub

    Private Function TrySeparateDataSet(dsData As DataSet, tableName As String) As DataSet
        ' Get a DataSet by name from target DataSet without error (Return nothing if the table not exist)
        If dsData IsNot Nothing AndAlso dsData.Tables.Contains(tableName) Then
            Dim ds As New DataSet()
            ds.Tables.Add(dsData.Tables(tableName).Copy())
            Return ds
        Else
            Return Nothing
        End If
    End Function

    Private Sub ShowPolicyGeneralInfoAsync()
        ' init a countDown Semaphore, initCount = 0, and the maxCount should be the total number of threads started below -1
        countDownSemaphore = New Threading.Semaphore(0, COUNT_DOWN_THREAD_NUM)

        ' Contorls will be across threads accessed!
        Threading.ThreadPool.QueueUserWorkItem(AddressOf ShowPolicyRecord)
        Threading.ThreadPool.QueueUserWorkItem(AddressOf ShowPolicyClient)
        Threading.ThreadPool.QueueUserWorkItem(AddressOf ShowPolicyMRP)
        Threading.ThreadPool.QueueUserWorkItem(AddressOf ShowVisitCS)
        Threading.ThreadPool.QueueUserWorkItem(AddressOf ShowBillingInfo)
    End Sub

    Public Sub showPolicyGeneralInfo()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            objMQQueHeader.Timeout = 90000000 'My.Settings.Timeout

            If IsAsyncLoadForm Then
                ' async multi-thread loading each controls
                ShowPolicyGeneralInfoAsync()
            Else
                ' old mode, sync loading
                ShowPolicyRecord()

                If dsPolicyHead.Tables.Count > 0 Then
                    ShowPolicyClient()

                    ShowPolicyMRP()

                    ShowVisitCS()

                    ShowBillingInfo()
                End If

                ' raise load data completed event
                OnLoadDataCompleted()
            End If

        Catch ignore As Exception
        Finally
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "showPolicyGeneralInfo", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
        End Try
    End Sub

    Private Sub ShowPolicyRecord()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            Ctrl_POS_Scrn_Head1.policyInuse = strPolicyNo.Trim
            Ctrl_POS_Scrn_Head1.dbHeader = Me.dbHeader
            Ctrl_POS_Scrn_Head1.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_POS_Scrn_Head1.SystemInuse = "CRS" 'ITSR2040 WOP by Claudia Lai CL20210429
            Ctrl_POS_Scrn_Head1.ShowPolicyRcd()

            If Not IsAsyncLoadForm Then
                dsPolicyHead = Ctrl_POS_Scrn_Head1.currdsInuse
                If dsPolicyHead.Tables.Count > 0 Then
                    EffDate = Format(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), "MMM dd, yyyy")
                    PolicyStatus = dsPolicyHead.Tables(0).Rows(0)("Risk_Sts")
                    OwnerCustomerID = Me.Ctrl_POS_Scrn_Head1.iOwnerCIWNoInuse
                End If
            End If

        Catch ignore As Exception
        Finally
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "ShowPolicyRecord", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
            ' it is assumed that PolicyHeader will finish last, so this thread waits for another thread
            If IsAsyncLoadForm Then WaitForRaiseCompletedEvent()
        End Try
    End Sub

    Private Sub ShowPolicyClient()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            Ctrl_POS_PolicyClient1.UHNW_Enabled = "Y"                       ' INC0325215
            Ctrl_POS_PolicyClient1.UHNW_Right = If(UHNWRightFlag, "Y", "N") ' INC0325215
            Ctrl_POS_PolicyClient1.EffDateInuse = EffDate
            Ctrl_POS_PolicyClient1.PolicyNoInuse = strPolicyNo
            Ctrl_POS_PolicyClient1.PolicyInUse = dsPolicyHead   ' CRS performer slowness - Set policy header data externally
            Ctrl_POS_PolicyClient1.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_POS_PolicyClient1.DBHeader = Me.dbHeader
            Ctrl_POS_PolicyClient1.sPolicyStatus = PolicyStatus
            Ctrl_POS_PolicyClient1.modeinuse = Utility.Utility.ModeName.Enquiry  'POSCommCtrl.Ctrl_POS_PolicyClient.ModeName.Enquiry
            Ctrl_POS_PolicyClient1.showClnRelation()

        Catch ex As Exception
        Finally
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "showPolicyClient", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
            ' signal this thread has ended (if in asynchronous mode)
            If IsAsyncLoadForm Then countDownSemaphore?.Release()
        End Try
    End Sub

    Private Sub ShowPolicyMRP()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            ' ---- ITSR236 - Show Minimum Required Premium Info
            With Me.Ctrl_POS_MRP1
                .PolicyNo = strPolicyNo
                .EffectiveDate = EffDate
                .dbHeader = Me.dbHeader
                .MQQueuesHeader = Me.MQQueuesHeader
                .ShowPolicyMRP()
            End With

        Catch ex As Exception
        Finally
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "ShowPolicyMRP", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
            ' signal this thread has ended (if in asynchronous mode)
            If IsAsyncLoadForm Then countDownSemaphore?.Release()
        End Try
    End Sub

    Private Sub ShowVisitCS()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            'KT20161111
            If Me.MQQueuesHeader.CompanyID = "ING" OrElse Me.dbHeader.CompanyID = "ING" Then
                With Me.Ctrl_VisitCS1
                    .PolicyNo = strPolicyNo
                    .CustomerID = OwnerCustomerID
                    .Lang = "en"
                    .UpdateUser2 = dbHeader.UserID
                    .IsTAD2C = blnIsTAD2C
                    .checkVisitCSFlag()
                End With
            Else
                Me.Ctrl_VisitCS1.Visible = False
            End If

        Catch ex As Exception
        Finally
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "ShowVisitCS", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
            ' signal this thread has ended (if in asynchronous mode)
            If IsAsyncLoadForm Then countDownSemaphore?.Release()
        End Try
    End Sub

    Private Sub ShowBillingInfo()
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim mainStartTime As Date = Now

        Try
            Ctrl_BillingInfo1.strPolicyNo = strPolicyNo.Trim
            Ctrl_BillingInfo1.PolicyHeaderData = dsPolicyHead   ' CRS performer slowness - Set policy header data externally
            Ctrl_BillingInfo1.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_BillingInfo1.DBHeader = Me.objDBHeader
            Ctrl_BillingInfo1.showBillingInfo()

        Catch ex As Exception
        Finally
            SysEventLog.WritePerLog(dbHeader.UserID, "CRS_Ctrl.ctrl_CRSPolicyGeneralInformation", "ShowBillingInfo", "", mainStartTime, Now, strPolicyNo.Trim, "", "")
            ' signal this thread has ended (if in asynchronous mode)
            If IsAsyncLoadForm Then countDownSemaphore?.Release()
        End Try
    End Sub

    Private Function GetOwnerCiwNoFromTargetDataSet(dsOwnerInsured As DataSet) As Long
        If dsOwnerInsured IsNot Nothing AndAlso dsOwnerInsured.Tables.Count > 0 Then
            ' follow LAS.POSCommCtrl.Ctrl_POS_Scrn_Head.iOwnerCIWNo logic, take the last one
            Dim rows As DataRow() = dsOwnerInsured.Tables(0).Select("PolicyRelateCode = 'PH'")
            Return If(rows.Length = 0, 0, rows(rows.Length - 1).Item("CustomerID"))
        Else
            Return 0
        End If
    End Function

    Private Sub WaitForRaiseCompletedEvent()
        Try
            ' wait until other threads finish
            For i As Integer = 1 To COUNT_DOWN_THREAD_NUM
                countDownSemaphore?.WaitOne()
            Next
        Catch ignore As Exception
        Finally
            ' all other threads are finished, dispose first
            countDownSemaphore?.Close()
            countDownSemaphore = Nothing

            ' raise load data completed event
            OnLoadDataCompleted()
        End Try
    End Sub

    Private Sub OnLoadDataCompleted()
        If Me.Disposing OrElse Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return  ' no need raise if control non-Ready/closed

        If InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf OnLoadDataCompleted))
        Else
            RaiseEvent LoadDataCompleted(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub btnSLHistory_Click(sender As Object, e As EventArgs) Handles btnSLHistory.Click
        Try
            Dim w As New frmSmartLegacyHistory
            w.MQQueuesHeader = Me.MQQueuesHeader
            w.DBHeader = Me.dbHeader
            w.PolicyNoInUse = strPolicyNo
            w.ShowSLHRecord()
            If Not OpenWindow(w) Then
                w.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Function OpenWindow(ByVal w As Form) As Boolean
        w.WindowState = FormWindowState.Normal
        w.Show()
        Return True
    End Function

End Class
