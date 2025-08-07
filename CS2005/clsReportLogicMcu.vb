'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'001    06 Jun 2023     Gavin Wu    ITSR4101            Add new logic for Post Sales Call follow up letter

Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Microsoft.Office.Interop
Imports CRS_Component
''' <summary>
''' Macau Report manager form.
''' Lubin 2022-11-01 Move from HK Report logics to Macau.
''' Changes: Add new method APIServiceBL.CallAPIBusi
''' </summary>
Public Class clsReportLogicMcu
    Inherits LetterCommonBL '001

    Private rpt As ReportDocument
    Private strDest As PrintDest   '3b
    Private blnCancel As Boolean
    '001s
    Public outputFilePath As String
    Public Const LINEBREAK As String = "--lineBreak--"
    '001e
    Public objDBHeader As Utility.Utility.ComHeader
    Private configEndPoint_Url As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ConfigEndPoint")

    Private _policyNo As String
    Public Property PolicyNo() As String
        Get
            Return _policyNo
        End Get
        Set(ByVal value As String)
            _policyNo = value
        End Set
    End Property
  

    Public ReadOnly Property CancelPrint() As Boolean
        Get
            Return blnCancel
        End Get
    End Property

    Public WriteOnly Property CR_Rpt()
        Set(ByVal Value)
            rpt = Value
        End Set
    End Property

    Public Property Destination()
        Get
            Return strDest
        End Get
        Set(ByVal value)
            strDest = value
        End Set
    End Property



    'For LifeAsia's Connection
    Private Function ConnectDB(ByRef obj As Object, ByVal strProject As String, ByVal strConnAlias As String, ByVal strUser As String, ByRef strErr As String) As Boolean
        Try

            If Not obj Is Nothing Then
                obj = New DBLogon_NET.DBLogon.DBlogonNet
                obj.Project = strProject
                obj.ConnectionAlias = strConnAlias 'strComp + strProject + strEnv
                obj.User = strUser
                If obj.Connect() Then
                    Return True
                Else
                    strErr = obj.RecentErrorMessage
                    Return False
                End If
            End If

        Catch ex As Exception
            strErr = ex.Message
        End Try
    End Function

    'levy
    Private WithEvents objCI As New LifeClientInterfaceComponent.clsPOS

    'Private Function GetMQQueHeader() As Utility.Utility.MQHeader
    '    Dim objMQQueHeader As Utility.Utility.MQHeader
    '    objMQQueHeader.UserID = gsUser
    '    objMQQueHeader.QueueManager = g_Qman '"LACSQMGR1" '"WINTEL"
    '    objMQQueHeader.RemoteQueue = g_WinRemoteMcuQ  '"LACSSIT02.TO.LA400SIT02" '"LIFEASIA.RQ1"
    '    objMQQueHeader.ReplyToQueue = g_LAReplyMcuQ  '"LA400SIT02.TO.LACSSIT02" '"WINTEL.RQ1"
    '    objMQQueHeader.LocalQueue = g_WinLocalMcuQ  '"LACSSIT02.QUEUE1.LCL" '"WINTEL.LQ1"
    '    objMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
    '    objMQQueHeader.EnvironmentUse = g_McuEnv
    '    objMQQueHeader.CompanyID = g_McuComp
    '    Return objMQQueHeader
    'End Function

    'Private Function GetMComHeader() As Utility.Utility.ComHeader
    '    Dim objDBHeader As Utility.Utility.ComHeader
    '    objDBHeader.UserID = gsUser
    '    objDBHeader.EnvironmentUse = g_McuEnv '"SIT02"
    '    objDBHeader.ProjectAlias = "LAS" '"LAS"
    '    objDBHeader.CompanyID = g_McuComp  '"ING"
    '    objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"
    '    Return objDBHeader
    'End Function

    Dim strRptHeader As String = String.Empty
    Public Property ReportHeader() As String
        Set(ByVal value As String)
            strRptHeader = value
        End Set
        Get
            Return strRptHeader
        End Get
    End Property
    ''' <summary>
    ''' Lubin 2022-10-25 Add ParentFrm property to the report.
    ''' </summary>
    ''' <returns>Parent Form</returns>
    Public Property ParentFrm As Form

#Region "ILAS Notification Letter Enhancement"
    Public Function ILAS_NotificationLetter()

        Dim objMQQueHeaderMcu As Utility.Utility.MQHeader
        Dim objDBHeaderMcu As Utility.Utility.ComHeader

        Dim frmParam As New frmPAYHRpt
        Dim strCIWConn1 As String = strCIWMcuConn
        Dim strSQL As String = String.Empty
        Dim strPolicy As String = String.Empty
        Dim strErr As String = String.Empty

        objMQQueHeaderMcu = gobjMQQueHeader
        objMQQueHeaderMcu.CompanyID = g_McuComp
        objMQQueHeaderMcu.EnvironmentUse = g_McuEnv
        objMQQueHeaderMcu.RemoteQueue = g_WinRemoteMcuQ
        objMQQueHeaderMcu.ReplyToQueue = g_LAReplyMcuQ
        objMQQueHeaderMcu.LocalQueue = g_WinLocalMcuQ
        objDBHeaderMcu = gobjDBHeader
        objDBHeaderMcu.CompanyID = g_McuComp
        objDBHeaderMcu.EnvironmentUse = g_McuEnv

        'objMQQueHeaderMcu = GetMQQueHeader()
        'objDBHeaderMcu = GetMComHeader()

        Dim dtGeneralInfo As New DataTable
        Dim dtCompanyLogo As New DataTable

        blnCancel = True
        frmParam.Text = ReportHeader
        frmParam.txtPolicy.Text = strLastPolicy
        frmParam.txtFrom.Enabled = False
        frmParam.txtTo.Enabled = False
        frmParam.chkChi.Visible = False
        frmParam.chkEng.Visible = False


        If frmParam.ShowDialog() = DialogResult.OK Then
            strPolicy = frmParam.PolicyNo
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        'Load High Risk Fund Option Window
        Dim dtFundcoll As New DataTable
        If Not Me.LoadFundPopup(strPolicy, strCIWConn1, dtFundcoll, strErr) Then
            MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
        End If
        dtFundcoll.TableName = "rpt_ILAS_Fund_Desc"

        'Dim iMasterPlan As New System.Collections.Generic.List(Of String)
        'Dim iKnowUSinglePremiumPlan As New System.Collections.Generic.List(Of String)
        'Dim iKnowURegularPlan As New System.Collections.Generic.List(Of String)
        'Dim HorizonPlan As New System.Collections.Generic.List(Of String)

        'Dim HorizonUpfrontCharge As New System.Collections.Generic.List(Of String)
        'Dim iKnowSPUpfrontCharge As New System.Collections.Generic.List(Of String)

        ''1. Get iMaster Plan
        'Dim dtTmp As New DataTable
        'strSQL = "select Value  from CodeTable where code in ('CRS_IMAS','CRS_IWEA','CRS_VINT')"
        'Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        'Me.TableToList(dtTmp, iMasterPlan)


        ''2. iKnowU Single Premium Plan
        'dtTmp.Clear()
        'strSQL = "select Value  from CodeTable where code in ('CRS_IKUSP')"
        'Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        'Me.TableToList(dtTmp, iKnowUSinglePremiumPlan)

        ''3. iKnowU Reqular Premium Plan
        'dtTmp.Clear()
        'strSQL = "select Value  from CodeTable where code in ('CRS_IKU')"
        'Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        'Me.TableToList(dtTmp, iKnowURegularPlan)

        ''4. Horizon plan
        'dtTmp.Clear()
        'strSQL = "select Value  from CodeTable where code in ('CRS_HORI')"
        'Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        'Me.TableToList(dtTmp, HorizonPlan)

        ''5. Get Horizon Upfront Charge Value
        'dtTmp.Clear()
        'strSQL = "select Value  from CodeTable where code in ('CRS_HUF')"
        'Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        'Me.TableToList(dtTmp, HorizonUpfrontCharge)

        ''6. Get i.Know Single Premium Upfrom Charge Value 
        'dtTmp.Clear()
        'strSQL = "select Value  from CodeTable where code in ('CRS_iKSPUF')"
        'Me.ExcecuteSQL(strSQL, strCIWConn1, dtTmp)
        'Me.TableToList(dtTmp, iKnowSPUpfrontCharge)

        'strSQL = "Select Pa.PolicyAccountID, Product.ProductID, Product.[Description] As ProductDescription, Product_Chi.ChineseDescription,pa.PolicyCurrency, " & _
        '            " Isnull(cswpad_add1,'') as Address1,Isnull(cswpad_add2,'') as Address2,Isnull(cswpad_add3,'') as Address3,Isnull(cswpad_city,'') As City," & _
        '            " Isnull(poliAddress.cswpad_tel1,'') as TelePhone1,IsNull(poliAddress.cswpad_tel2,'') as TelePhone2," & _
        '            " Isnull(customer.FirstName,'') as FirstName, Isnull(customer.NameSuffix,'') as LastName, Isnull(customer.ChiFstNm,'') as ChiFstNm, " & _
        '            " Isnull(customer.ChiLstNm,'') as ChiLstNm,Isnull(customer.PhoneMobile,'') as PhoneMobile, " & _
        '            " Isnull(rrt.Camrrt_loc_code,'') as LocationCode , Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') as AgentLastName, '' as AgentFirstName, " & _
        '            " case when len(Isnull(Agent.camaib_chi_name,''))>0 then Isnull(Agent.camaib_chi_name,'') else Replace(Replace(Replace(isnull(camaib_last_name,'')+isnull(camaib_first_name,''),' ','[ ]'),'][ ',''),'[ ]',' ') end as AgentChiName, pa.Mode,M.ModeDesc, SPACE(15) as CooloffDate,pa.ModalPremium, Cov.ModalPremium as OneOffModalPremium, " & _
        '            " DATEDIFF(YEAR, cov.IssueDate, cov.PREMIUMCESSATIONDATE) as Period  from PolicyAccount as PA " & _
        '            " Inner Join  csw_poli_rel as poliRel on PA.PolicyAccountID = poliRel.PolicyAccountID and poliRel.PolicyRelateCode ='PH' " & _
        '            " Inner Join Coverage as Cov on pa.PolicyAccountID = cov.PolicyAccountID  and Cov.Trailer = '1' " & _
        '            " Left Join csw_policy_address as poliAddress on poliRel.PolicyAccountID = poliAddress.cswpad_poli_id " & _
        '            " Left Join customer on poliRel.CustomerID = customer.CustomerID   " & _
        '            " Left Join csw_poli_rel as PoliRelWA on pa.PolicyAccountID = PoliRelWA.PolicyAccountID and PoliRelWA.PolicyRelateCode= 'WA' " & _
        '            " Left Join customer as CustWA on PoliRelWA.CustomerID = custwa.CustomerID  " & _
        '            " Left Join Product on PA.ProductID = product.ProductID  " & _
        '            " Left Join Product_Chi on Pa.ProductID  =Product_Chi.ProductID  " & _
        '            " Left Join ModeCodes M on pa.Mode = M.ModeCode " & _
        '            " Left Join  {1}.dbo.cam_agent_info_basic as Agent  on CustWA.AgentCode = agent.camaib_agent_no " & _
        '            " Inner Join {1}.dbo.cam_agent_info_dirmgr as  aid on Agent.camaib_agent_no = aid.camaid_agent_no " & _
        '            " Left Join {1}.dbo.cam_rdbu_rel_tab as rrt on aid.camaid_sort_key = rrt.Camrrt_agency_code and aid.camaid_section_no = rrt.Camrrt_section_no " & _
        '            " where PA.PolicyAccountID ='{0}' "


        'strSQL = String.Format(strSQL, strPolicy, g_CAM_Database)
        'Me.ExcecuteSQL(strSQL, strCIWConn1, dtGeneralInfo)
        Dim dsRptILASNotificationLetterInfo As DataSet = New DataSet()
        Dim strListiMasterPlan As New List(Of String)
        Dim strListiKnowURegularPlan As New List(Of String)
        Dim strListiKnowUSinglePremiumPlan As New List(Of String)
        Dim strListHorizonPlan As New List(Of String)
        Dim striKnowSPUpfrontCharge As String = String.Empty
        Dim strHorizonUpfrontCharge As String = String.Empty

        If GetRptILASNotificationLetterInfo(getCompanyCode(g_McuComp), strPolicy, dsRptILASNotificationLetterInfo) Then
            'For Each dt As DataTable In dsRptILASNotificationLetterInfo.Tables
            '    dsMisc.Tables.Add(dt.Copy)
            'Next

            dtGeneralInfo = dsRptILASNotificationLetterInfo.Tables("rpt_ILAS_Notifiation_Letter")
            dtCompanyLogo = dsRptILASNotificationLetterInfo.Tables("dtLogo")

            For Each DtRow As DataRow In dsRptILASNotificationLetterInfo.Tables("dtIMasterPlan").Rows
                If DtRow("Value") IsNot Nothing Then
                    strListiMasterPlan.Add(DtRow("Value").ToString)
                End If
            Next

            For Each DtRow As DataRow In dsRptILASNotificationLetterInfo.Tables("dtIKnowURegularPlan").Rows
                If DtRow("Value") IsNot Nothing Then
                    strListiKnowURegularPlan.Add(DtRow("Value").ToString)
                End If
            Next

            For Each DtRow As DataRow In dsRptILASNotificationLetterInfo.Tables("dtIKnowUSinglePlan").Rows
                If DtRow("Value") IsNot Nothing Then
                    strListiKnowUSinglePremiumPlan.Add(DtRow("Value").ToString)
                End If
            Next

            For Each DtRow As DataRow In dsRptILASNotificationLetterInfo.Tables("dtHorizonPlan").Rows
                If DtRow("Value") IsNot Nothing Then
                    strListHorizonPlan.Add(DtRow("Value").ToString)
                End If
            Next

            striKnowSPUpfrontCharge = dsRptILASNotificationLetterInfo.Tables("dtIKnowSPUpfrontCharge").Rows(0).Item(0).ToString()
            strHorizonUpfrontCharge = dsRptILASNotificationLetterInfo.Tables("dtHorizonUpfrontCharge").Rows(0).Item(0).ToString()

        End If

        If dtGeneralInfo.Rows.Count > 0 Then
            'Get cooling off Date 
            Dim strBasicPlan As String = String.Empty

            If Not String.IsNullOrEmpty(dtGeneralInfo.Rows(0)("ProductID")) Then
                strBasicPlan = Convert.ToString(dtGeneralInfo.Rows(0)("ProductID")).Trim
            End If

            Dim dsSendData As New DataSet
            Dim dtSendData As New DataTable

            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("PolicyNo")
            dr("PolicyNo") = strPolicy
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)

            Dim dsCurr As New DataSet
            Dim strTime As String = ""

            Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
            clsPOS.MQQueuesHeader = objMQQueHeaderMcu
            clsPOS.DBHeader = objDBHeaderMcu
            If Not clsPOS.GetPolicy(dsSendData, dsCurr, strTime, strErr) Then
                MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
            End If
            strErr = String.Empty

            If dsCurr.Tables.Count > 0 Then
                If dsCurr.Tables(0).Rows.Count > 0 Then
                    For Each drGeneralInfo As DataRow In dtGeneralInfo.Rows
                        drGeneralInfo("CooloffDate") = dsCurr.Tables(0).Rows(0)("CooloffDate")
                        If String.IsNullOrEmpty(strBasicPlan) Then
                            strBasicPlan = drGeneralInfo("ProductID")
                        End If
                    Next
                End If
            End If
            'dtGeneralInfo.TableName = "rpt_ILAS_Notifiation_Letter"

            'Get Plan type 
            iKnowUReg = False
            iKnowUSP = False
            Horizon = False
            iMaster = False
            'Me.GetPlanType(strBasicPlan, New List(Of String)(Data.iMasterPlan), New List(Of String)(Data.iKnowURegularPlan), New List(Of String)(Data.iKnowUSinglePremiumPlan), New List(Of String)(Data.HorizonPlan))
            Me.GetPlanType(strBasicPlan, strListiMasterPlan, strListiKnowURegularPlan, strListiKnowUSinglePremiumPlan, strListHorizonPlan)

            'Get Suitability Option
            Dim strSuitabilityOption As String = String.Empty

            'Get Company Logo 
            'Dim dtCompanyLogo As New DataTable
            'dtCompanyLogo = ConvertToDataTable(Of CRSWS.RptCompanyLogo)(Data.CompanyLogo)
            'dtCompanyLogo.TableName = "Company_Logo"

            rpt.Database.Tables("rpt_ILAS_Notifiation_Letter").SetDataSource(dtGeneralInfo)
            rpt.Subreports("rpt_ILAS_Fund_Desc").Database.Tables("rpt_ILAS_Fund_Desc").SetDataSource(dtFundcoll)
            rpt.Subreports("rpt_ILAS_Fund_Desc_Chi").Database.Tables("rpt_ILAS_Fund_Desc").SetDataSource(dtFundcoll)
            rpt.Subreports("Company_Logo").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)
            rpt.Subreports("ING_Address").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)
            rpt.Subreports("ING_Phone").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)

            Dim type As Boolean = False
            If ReportHeader.Contains("(uncertain replies)") Then
                type = True 'UnCertain replies
            Else
                type = False
            End If

            rpt.SetParameterValue("iKnowUReg", iKnowUReg)
            rpt.SetParameterValue("iKnowUSP", iKnowUSP)
            rpt.SetParameterValue("Horizon", Horizon)
            rpt.SetParameterValue("iMaster", iMaster)
            rpt.SetParameterValue("DocTypeUC", type)
            rpt.SetParameterValue("SuitabilityOption", strSuitabilityOption)
            rpt.SetParameterValue("iKnowSPUpfrontCharge", striKnowSPUpfrontCharge)
            rpt.SetParameterValue("HorizonUpfrontCharge", strHorizonUpfrontCharge)

            blnCancel = False
        Else
            MsgBox("Can't find the detail for given policy. Policy No - " + strPolicy, MsgBoxStyle.Exclamation, ReportHeader)
        End If


        'Using wsCRS As New CRSWS.CRSWS()
        '    Dim response As New CRSWS.WSResponseOfRptILASNotificationLetterInfo
        '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
        '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
        '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
        '    End If
        '    'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
        '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
        '    wsCRS.Timeout = 10000000

        '    response = wsCRS.GetRptILASNotificationLetterInfo(getCompanyCode(g_McuComp), getEnvCode(), strPolicy, g_McuCAM_Database)

        '    If response Is Nothing Or response.Success = False Then
        '        MsgBox("Fail to GetRptILASNotificationLetterInfo :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
        '    Else
        '        Dim data As CRSWS.RptILASNotificationLetterInfo = response.Data
        '        dtGeneralInfo = ConvertToDataTable(Of CRSWS.RptILASNotificationLetter)(data.RptILASNotificationLetterList)

        '        If dtGeneralInfo.Rows.Count > 0 Then
        '            'Get cooling off Date 
        '            Dim strBasicPlan As String = String.Empty

        '            If Not String.IsNullOrEmpty(dtGeneralInfo.Rows(0)("ProductID")) Then
        '                strBasicPlan = Convert.ToString(dtGeneralInfo.Rows(0)("ProductID")).Trim
        '            End If

        '            Dim dsSendData As New DataSet
        '            Dim dtSendData As New DataTable

        '            Dim dr As DataRow = dtSendData.NewRow()
        '            dtSendData.Columns.Add("PolicyNo")
        '            dr("PolicyNo") = strPolicy
        '            dtSendData.Rows.Add(dr)
        '            dsSendData.Tables.Add(dtSendData)

        '            Dim dsCurr As New DataSet
        '            Dim strTime As String = ""

        '            Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        '            clsPOS.MQQueuesHeader = objMQQueHeaderMcu
        '            clsPOS.DBHeader = objDBHeaderMcu
        '            If Not clsPOS.GetPolicy(dsSendData, dsCurr, strTime, strErr) Then
        '                MsgBox(strErr, MsgBoxStyle.Exclamation, ReportHeader)
        '            End If
        '            strErr = String.Empty

        '            If dsCurr.Tables.Count > 0 Then
        '                If dsCurr.Tables(0).Rows.Count > 0 Then
        '                    For Each drGeneralInfo As DataRow In dtGeneralInfo.Rows
        '                        drGeneralInfo("CooloffDate") = dsCurr.Tables(0).Rows(0)("CooloffDate")
        '                        If String.IsNullOrEmpty(strBasicPlan) Then
        '                            strBasicPlan = drGeneralInfo("ProductID")
        '                        End If
        '                    Next
        '                End If
        '            End If
        '            dtGeneralInfo.TableName = "rpt_ILAS_Notifiation_Letter"

        '            'Get Plan type 
        '            iKnowUReg = False
        '            iKnowUSP = False
        '            Horizon = False
        '            iMaster = False
        '            Me.GetPlanType(strBasicPlan, New List(Of String)(data.iMasterPlan), New List(Of String)(data.iKnowURegularPlan), New List(Of String)(data.iKnowUSinglePremiumPlan), New List(Of String)(data.HorizonPlan))

        '            'Get Suitability Option
        '            Dim strSuitabilityOption As String = data.SuitabilityOption

        '            'Get Company Logo 
        '            Dim dtCompanyLogo As New DataTable
        '            dtCompanyLogo = ConvertToDataTable(Of CRSWS.RptCompanyLogo)(data.CompanyLogo)
        '            dtCompanyLogo.TableName = "Company_Logo"

        '            rpt.Database.Tables("rpt_ILAS_Notifiation_Letter").SetDataSource(dtGeneralInfo)
        '            rpt.Subreports("rpt_ILAS_Fund_Desc").Database.Tables("rpt_ILAS_Fund_Desc").SetDataSource(dtFundcoll)
        '            rpt.Subreports("rpt_ILAS_Fund_Desc_Chi").Database.Tables("rpt_ILAS_Fund_Desc").SetDataSource(dtFundcoll)
        '            rpt.Subreports("Company_Logo").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)
        '            rpt.Subreports("ING_Address").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)
        '            rpt.Subreports("ING_Phone").Database.Tables("Company_Logo").SetDataSource(dtCompanyLogo)

        '            Dim type As Boolean = False
        '            If ReportHeader.Contains("(uncertain replies)") Then
        '                type = True 'UnCertain replies
        '            Else
        '                type = False
        '            End If

        '            rpt.SetParameterValue("iKnowUReg", iKnowUReg)
        '            rpt.SetParameterValue("iKnowUSP", iKnowUSP)
        '            rpt.SetParameterValue("Horizon", Horizon)
        '            rpt.SetParameterValue("iMaster", iMaster)
        '            rpt.SetParameterValue("DocTypeUC", type)
        '            rpt.SetParameterValue("SuitabilityOption", strSuitabilityOption)
        '            rpt.SetParameterValue("iKnowSPUpfrontCharge", data.iKnowSPUpfrontCharge)
        '            rpt.SetParameterValue("HorizonUpfrontCharge", data.HorizonUpfrontCharge)

        '            blnCancel = False
        '        Else
        '            MsgBox("Can't find the detail for given policy. Policy No - " + strPolicy, MsgBoxStyle.Exclamation, ReportHeader)
        '        End If
        '    End If

        'End Using



    End Function

    Private iMaster As Boolean = False
    Private iKnowUReg As Boolean = False
    Private iKnowUSP As Boolean = False
    Private Horizon As Boolean = False

    Private Sub GetPlanType(ByVal strBasicPlan As String, _
                                ByVal lstiMaster As System.Collections.Generic.List(Of String), _
                                ByRef lstiKnowU As System.Collections.Generic.List(Of String), _
                                ByRef lstiKnowUSP As System.Collections.Generic.List(Of String), _
                                ByRef lsthorizon As System.Collections.Generic.List(Of String))

        If lstiMaster.Contains(strBasicPlan) Then
            iMaster = True
            Exit Sub
        ElseIf lstiKnowU.Contains(strBasicPlan) Then
            iKnowUReg = True
            Exit Sub
        ElseIf lstiKnowUSP.Contains(strBasicPlan) Then
            iKnowUSP = True
            Exit Sub
        ElseIf lsthorizon.Contains(strBasicPlan) Then
            Horizon = True
            Exit Sub
        End If

    End Sub

    Private Function LoadFundPopup(ByVal strPolicyNo As String, ByVal strConn As String, ByRef dtFundcoll As DataTable, ByRef strMsg As String) As Boolean
        LoadFundPopup = False
        Dim objFundCollPopup As New frmHighRiskFund()

        Try

            'Dim strSQL As String = "select distinct  fa.cswcfa_fund_code as FundCode ,FundDesc.mpfinv_chi_desc as FundDescription,FundDesc.mpfinv_chi_name as FundChiDesc from csw_fund_allocation fa " & _
            '                        "Inner Join (Select  cswcfa_policy_no,cswcfa_fund_code,cswcfa_coverage_no,Max(cswcfa_eff_date) as Eff_Date from csw_fund_allocation where cswcfa_policy_no = '{0}' " & _
            '                        "Group   By cswcfa_policy_no,cswcfa_fund_code,cswcfa_coverage_no) as DistFund On  " & _
            '                        "fa.cswcfa_policy_no = DistFund.cswcfa_policy_no And fa.cswcfa_fund_code = DistFund.cswcfa_fund_code " & _
            '                        "and fa.cswcfa_coverage_no = DistFund.cswcfa_coverage_no and fa.cswcfa_eff_date = DistFund.Eff_Date " & _
            '                        "Inner Join cswvw_mpf_investment as FundDesc  On  fa.cswcfa_fund_code = FundDesc.mpfinv_code " & _
            '                        "where fa.cswcfa_policy_no = '{0}'  "

            'strSQL = String.Format(strSQL, strPolicyNo)
            'Me.ExcecuteSQL(strSQL, strConn, dtFundcoll)

            Using wsCRS As New CRSWS.CRSWS()
                Dim response As New CRSWS.WSResponseOfListOfRptILASFundDesc
                wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
                If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
                    wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
                End If
                'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
                wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
                wsCRS.Timeout = 10000000

                response = wsCRS.LoadFundPopup(getCompanyCode(g_McuComp), getEnvCode(), strPolicyNo)

                If response Is Nothing Or response.Success = False Then
                    MsgBox("Fail to LoadFundPopUp :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
                Else
                    dtFundcoll = ConvertToDataTable(Of CRSWS.RptILASFundDesc)(response.Data)
                End If

            End Using

            objFundCollPopup.FundColl = dtFundcoll
            If dtFundcoll.Rows.Count = 0 Then
                strMsg = "There is no fund exist for given Policy No : " + strPolicyNo
            Else
                objFundCollPopup.ShowDialog()
                dtFundcoll.Clear()
                dtFundcoll = objFundCollPopup.FundColl
                LoadFundPopup = True
            End If

        Catch ex As Exception
            strMsg = ex.Message
        Finally
            objFundCollPopup = Nothing
        End Try

    End Function

#End Region

#Region "Premium reminder call report"
    Public Sub PremCallRpt()

        Dim strSQL, strError As String
        Dim dtResult As DataTable
        Dim frmParam As New frmPremCallRpt

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then

            If DateDiff(DateInterval.Month, frmParam.dtP1From.Value, frmParam.dtP1To.Value) > 1 Then
                If MsgBox("Date range over 2 months, sure to proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            'strSQL = "Select p.policyaccountid, p.productid, description, p.paidtodate, p.mode, p.aplindicator, p.modalpremium, p.posamt, p.billingtype, cph.nameprefix, cph.namesuffix, cph.firstname, " & _
            '    " cph.phonemobile, cph.phonepager, cswpad_tel1, cswpad_tel2, cswpad_add1, cswpad_add2, cswpad_add3, cswpad_city, " & _
            '    " a.unitcode, a.agentcode, a.locationcode, csa.nameprefix, csa.namesuffix, csa.firstname , case when p.premiumstatus = 'PH' then 'Y' else 'N' end as [Is Prem. Holiday] " & _
            '    " from policyaccount p, csw_poli_rel rph, customer cph, csw_poli_rel rsa, customer csa, agentcodes a, csw_policy_address pa, product pd " & _
            '    " where p.policyaccountid = rph.policyaccountid " & _
            '    " and p.productid = pd.productid " & _
            '    " and rph.policyrelatecode = 'PH' " & _
            '    " and rph.customerid = cph.customerid " & _
            '    " and p.policyaccountid = rsa.policyaccountid " & _
            '    " and rsa.policyrelatecode = 'SA' " & _
            '    " and rSA.customerid = csa.customerid " & _
            '    " and csa.agentcode = a.agentcode " & _
            '    " and p.policyaccountid = cswpad_poli_id " & _
            '    " and (p.accountstatuscode in ('1') or p.aplindicator = 'Y') " & _
            '    " and p.companyid in ('EAA','ING') " & _
            '    " and p.paidtodate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' "
            ''Sky 20180109  end

            '' For specific bank channel
            'If frmParam.CheckBox1.Checked Then
            '    strSQL &= " and a.locationcode in (" & Trim(frmParam.TextBox1.Text) & ") "
            'End If

            'strSQL &= " order by p.paidtodate, a.unitcode, a.agentcode "

            wndMain.Cursor = Cursors.WaitCursor

            Using wsCRS As New CRSWS.CRSWS()
                Dim response As New CRSWS.WSResponseOfListOfPremCallRpt
                wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
                If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
                    wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
                End If
                'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
                wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
                wsCRS.Timeout = 10000000

                response = wsCRS.GetPremCallRpt(getCompanyCode(g_McuComp), getEnvCode(), Format(frmParam.dtP1From.Value, "MM/dd/yyyy"), Format(frmParam.dtP1To.Value, "MM/dd/yyyy"), frmParam.CheckBox1.Checked, Trim(frmParam.TextBox1.Text))

                If response Is Nothing Or response.Success = False Then
                    MsgBox("Fail to PremCallRpt :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
                Else
                    dtResult = ConvertToDataTable(Of CRSWS.PremCallRpt)(response.Data)

                    If frmParam.CheckBox1.Checked Then
                        If Not ExportCSV(frmParam.txtPath.Text & "\Macau_PremCallRpt_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                        End If
                    Else
                        ' All
                        If Not ExportCSV(frmParam.txtPath.Text & "\Macau_PremCallRpt_all_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                        End If
                        ' Agency only
                        dtResult.DefaultView.RowFilter = "unitcode >= '00800' and unitcode <= '69999'"
                        If Not ExportCSV(frmParam.txtPath.Text & "\Macau_PremCallRpt_agency_" & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                        End If

                    End If

                    MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                End If

                frmParam.Dispose()
                wndMain.Cursor = Cursors.Default

            End Using
        Else
            frmParam.Dispose()
            Exit Sub
        End If
    End Sub
#End Region

#Region "ILAS Product ?Post Sale Call Report"
    Public Function HKFI_Report()

        Dim strSQL, strError, strType, strOrderType As String
        Dim dtResult As DataTable
        Dim frmParam As New frmHKFI_Para

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then

            If Format(frmParam.dtP1From.Value, "MM/dd/yyyy") > Format(frmParam.dtP1To.Value, "MM/dd/yyyy") Then
                MsgBox("Invalid date range!", MsgBoxStyle.Question + MsgBoxStyle.OkOnly)
                wndMain.Cursor = Cursors.Default
                Exit Function
            End If

            'If frmParam.rdoUL.Checked = True Then

            '    strType = "UL_"

            '    strSQL = "Select DISTINCT 'UL' as RptType, cswpof_Policy as Policy_No, description as Plan_Name, RTRIM(cswpof_Value) as Risk_Level, exhibitinforcedate as Inforce_Date, " & _
            '        " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " & _
            '        "   where cswpof_policy = f.cswpof_policy " & _
            '        "   and cswpof_form_code = 'FNA' " & _
            '        "   and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " & _
            '        " c.customerid, "
            '    strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " & _
            '        " where r1.policyaccountid = p1.policyaccountid " & _
            '        " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" & _
            '        " and r1.policyrelatecode = 'PH' " & _
            '        " and r1.customerid = c.customerid " & _
            '        " and cv.exhibitinforcedate between '" & Format(DateAdd(DateInterval.Month, -1, frmParam.dtP1To.Value), "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " & _
            '        " and p1.accountstatuscode not in ('6','7','8','9','0') " & _
            '        " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
            '    strSQL &= "c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " & _
            '        " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location " & _
            '        " From csw_policy_form f, product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " & _
            '        " ON p.policyaccountid = cswpuw_poli_id " & _
            '        " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " & _
            '        " where cswpof_Form_Code = 'FNA' " & _
            '        " and cswpof_Item_name = 'SUITABILITY' " & _
            '        " and RTRIM(cswpof_Value) in ('A','B','C') " & _
            '        " and cswpof_policy = p.policyaccountid " & _
            '        " and p.productid = pd.productid " & _
            '        " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " & _
            '        " and cswpof_policy = r.policyaccountid " & _
            '        " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " & _
            '        " and r.policyaccountid = r1.policyaccountid " & _
            '        " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " & _
            '        " and c1.agentcode = a.agentcode " & _
            '        " and cv.policyaccountid = p.policyaccountid and cv.trailer = 1 " & _
            '        " and accountstatuscode in ('1','2','3','4','V') " & _
            '        " and p.companyid in ('ING','EAA') "
            'Else

            '    strType = "TL_"

            '    strSQL = "Select DISTINCT 'TL' as RptType, p.policyaccountid as Policy_No, description as Plan_Name, RTRIM(ISNULL(cswpof_Value,'')) as Risk_Level, exhibitinforcedate as Inforce_Date, " & _
            '        " (select case when cswpof_value = 'T' then 'Y' else '' end from csw_policy_form " & _
            '        " where cswpof_policy = p.policyaccountid " & _
            '        " and cswpof_form_code = 'FNA' " & _
            '        " and cswpof_item_name = 'VULNERABILITY') as Vulnerability, cswpuw_flook_dline as Cooling_Off_Date, cswpuw_podl_date as Delivery_Date, p.mode, p.modalpremium, " & _
            '        " c.customerid, "
            '    strSQL &= "(select count(*) from csw_poli_rel r1, policyaccount p1, coverage cv " & _
            '        " where(r1.policyaccountid = p1.policyaccountid) " & _
            '        " and r1.policyaccountid = cv.policyaccountid and cv.trailer = 1" & _
            '        " and r1.policyrelatecode = 'PH' " & _
            '        " and r1.customerid = c.customerid " & _
            '        " and cv.exhibitinforcedate between '" & Format(DateAdd(DateInterval.Month, -1, frmParam.dtP1To.Value), "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " & _
            '        " and p1.accountstatuscode not in ('6','7','8','9','0') " & _
            '        " and p1.policyaccountid <> p.policyaccountid) as Courtesy_Call_Made, "
            '    strSQL &= " c.nameprefix as PH_Nameprefix, c.namesuffix as PH_Namesuffix, c.firstname as PH_Firstname, c.phonemobile as Tel1, c.phonepager as Tel2, cswpad_add1 as Address1, cswpad_add2 as Address2, cswpad_add3 as Address3, cswpad_city as Address4, " & _
            '        " c1.nameprefix as SA_Nameprefix, c1.namesuffix as SA_Namesuffix, c1.firstname as SA_Firstname, a.locationcode as Location " & _
            '        " From product pd, csw_poli_rel r, customer c, csw_poli_rel r1, customer c1, agentcodes a, coverage cv, policyaccount p LEFT JOIN csw_policy_uw " & _
            '        " ON p.policyaccountid = cswpuw_poli_id " & _
            '        " LEFT JOIN csw_policy_form " & _
            '        " ON p.policyaccountid = cswpof_policy " & _
            '        " and cswpof_Form_Code = 'FNA' " & _
            '        " and cswpof_Item_name = 'SUITABILITY' " & _
            '        " LEFT JOIN csw_policy_address ON cswpad_poli_id = p.policyaccountid " & _
            '        " Where(cv.productid = pd.productid) " & _
            '        " and exhibitinforcedate between '" & Format(frmParam.dtP1From.Value, "MM/dd/yyyy") & "' and '" & Format(frmParam.dtP1To.Value, "MM/dd/yyyy") & "' " & _
            '        " and p.policyaccountid = r.policyaccountid " & _
            '        " and r.policyrelatecode = 'PH' and r.customerid = c.customerid " & _
            '        " and r.policyaccountid = r1.policyaccountid " & _
            '        " and r1.policyrelatecode = 'SA' and r1.customerid = c1.customerid " & _
            '        " and c1.agentcode = a.agentcode " & _
            '        " and cv.policyaccountid = p.policyaccountid " & _
            '        " and (cv.trailer = 1 or (cv.productid like '_RFUR1' and cv.coveragestatus in ('1','2','3','4','V'))) " & _
            '        " and accountstatuscode in ('1','2','3','4','V') " & _
            '        " and p.companyid in ('ING','EAA') "
            'End If

            If frmParam.rdoCODay.Checked Then
                'strSQL = strSQL & " order by cswpuw_flook_dline "
                strOrderType = "CODay"
            End If

            If frmParam.rdoRL.Checked Then
                'strSQL = strSQL & " order by Class "
                strOrderType = "RL"
            End If

            If frmParam.rdoUL.Checked = True Then
                strType = "UL_"
            Else
                strType = "TL_"
            End If

            wndMain.Cursor = Cursors.WaitCursor

            Using wsCRS As New CRSWS.CRSWS()
                Dim response As New CRSWS.WSResponseOfListOfHKFIRpt
                wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
                If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
                    wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
                End If
                'wsCRS.Url = "http://localhost:20562/CRSWebService/CRSWS.asmx"
                wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
                wsCRS.Timeout = 10000000
                'g_McuComp
                response = wsCRS.GetHKFIReport(getCompanyCode(g_McuComp), getEnvCode(), Format(frmParam.dtP1From.Value, "MM/dd/yyyy"), Format(frmParam.dtP1To.Value, "MM/dd/yyyy"), strType, strOrderType)

                If response Is Nothing Or response.Success = False Then
                    MsgBox("Fail to PremCallRpt :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
                Else
                    dtResult = ConvertToDataTable(Of CRSWS.HKFIRpt)(response.Data)

                    If Not ExportCSV(frmParam.txtPath.Text & "\Macau_PostSaleCall_" & strType & Format(Today, "MMddyyyy") & ".csv", dtResult) Then
                    End If
                    MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                End If

            End Using

            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Function
        End If

    End Function
#End Region

#Region "Policy Letter"
    ' ES007 begin
    Public Function LA_PolicyDetail()

        Dim frmParam As New frmPAYHRpt
        Dim strFromDate As String
        Dim strToDate As String
        Dim strSQL, strActivity, strPolicy As String
        Dim blnAll As Boolean
        Dim dsMisc As New DataSet
        Dim sqlda As SqlDataAdapter
        Dim sqlConn As New SqlConnection
        Dim strCIWConn1 As String = strCIWMcuConn

        'AC - Change to use configuration setting - start
        '#If UAT = 1 Then
        '        strCIWConn1 = strCIWConn
        '#Else
        '        strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        '#End If
        strCIWConn1 = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "MCReportConnection").DecryptString()
        'If gUAT Then
        '    strCIWConn1 = strCIWMcuConn
        'Else
        '    strCIWConn1 = "server=HKSQLVS1;database=VANTIVE;Network=DBMSSOCN;uid=com_apf_vantive;password=mocnav22;Connect Timeout=0"
        'End If
        'AC - Change to use configuration setting - end


        sqlConn.ConnectionString = strCIWConn1

        blnCancel = True
        frmParam.Text = "Policy Status Letter"
        frmParam.txtPolicy.Text = strLastPolicy
        frmParam.txtFrom.Enabled = False
        frmParam.txtTo.Enabled = False
        frmParam.chkChi.Enabled = True
        frmParam.chkEng.Enabled = True

        If frmParam.ShowDialog() = DialogResult.OK Then
            strPolicy = frmParam.PolicyNo
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        Dim dtClientInfo As DataTable = New DataTable
        Dim dtPolicyInfo As DataTable = New DataTable
        Dim dtCoRider As DataTable = New DataTable

        Dim strPolicyNo As String = String.Empty
        Dim dtPolicyInfoResult As New DataTable
        Dim tempDsPolicyInfo As DataSet = New DataSet()

        If GetPolicyInfo(getCompanyCode(g_McuComp), strPolicy, tempDsPolicyInfo) Then
            For Each dt As DataTable In tempDsPolicyInfo.Tables
                dsMisc.Tables.Add(dt.Copy)
            Next
        End If

        ' Policy Info Request
        'Using wsCRS As New CRSWS.CRSWS()
        '    Dim response As New CRSWS.WSResponseOfPolicyInfo
        '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
        '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
        '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
        '    End If
        '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
        '    wsCRS.Timeout = 10000000

        '    response = wsCRS.GetPolicyInfo(getCompanyCode(g_McuComp), getEnvCode(), strPolicy)

        '    If response Is Nothing Or response.Success = False Then
        '        MsgBox("Fail to GetPolicyInfo :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
        '    Else
        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.DDA)(response.Data.DDA)
        '        dtPolicyInfoResult.TableName = "DDA"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.CCDR)(response.Data.CCDR)
        '        dtPolicyInfoResult.TableName = "CCDR"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.BillingTypeCodes)(response.Data.BillingTypeCodes)
        '        dtPolicyInfoResult.TableName = "BillingTypeCodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.AccountStatusCodes)(response.Data.AccountStatusCodes)
        '        dtPolicyInfoResult.TableName = "AccountStatusCodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.ModeCodes)(response.Data.ModeCodes)
        '        dtPolicyInfoResult.TableName = "ModeCodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.DDAStatusCodes)(response.Data.DDAStatusCodes)
        '        dtPolicyInfoResult.TableName = "DDAStatusCodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.CCDRStatusCodes)(response.Data.CCDRStatusCodes)
        '        dtPolicyInfoResult.TableName = "CCDRStatusCodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.PolicyAccount)(response.Data.PolicyAccount)
        '        dtPolicyInfoResult.TableName = "PolicyAccount"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.csw_policy_value)(response.Data.csw_policy_value)
        '        dtPolicyInfoResult.TableName = "csw_policy_value"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.agentcodes)(response.Data.agentcodes)
        '        dtPolicyInfoResult.TableName = "agentcodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.csw_ing_logo_table)(response.Data.csw_ing_logo_table)
        '        dtPolicyInfoResult.TableName = "csw_ing_logo_table"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.product_type)(response.Data.product_type)
        '        dtPolicyInfoResult.TableName = "product_type"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.couponoptioncodes)(response.Data.couponoptioncodes)
        '        dtPolicyInfoResult.TableName = "couponoptioncodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.dividendoptioncodes)(response.Data.dividendoptioncodes)
        '        dtPolicyInfoResult.TableName = "dividendoptioncodes"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtPolicyInfoResult = ConvertToDataTable(Of CRSWS.Product_Chi)(response.Data.Product_Chi)
        '        dtPolicyInfoResult.TableName = "Product_Chi"
        '        dsMisc.Tables.Add(dtPolicyInfoResult)

        '        dtClientInfo = ConvertToDataTable(Of CRSWS.cswsp_clientinfo)(response.Data.cswsp_clientinfo)
        '        dtClientInfo.TableName = "cswsp_clientinfo"
        '        dsMisc.Tables.Add(dtClientInfo)

        '        dtPolicyInfo = ConvertToDataTable(Of CRSWS.cswsp_PolicyInfo)(response.Data.cswsp_PolicyInfo)
        '        dtPolicyInfo.TableName = "cswsp_PolicyInfo"
        '        dsMisc.Tables.Add(dtPolicyInfo)

        '        dtCoRider = ConvertToDataTable(Of CRSWS.cswsp_corider2)(response.Data.cswsp_corider2)
        '        dtCoRider.TableName = "cswsp_corider2"
        '        dsMisc.Tables.Add(dtCoRider)


        '    End If

        'End Using

        If dsMisc.Tables("PolicyAccount").Rows.Count = 0 Then
            MsgBox("Policy not found - " & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
            Exit Function
        End If

        If dsMisc.Tables("product_type").Rows.Count > 0 Then
            If IsDBNull(dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport")) Then
                MsgBox("Policy Letter is not available for this product yet." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
                Exit Function
            ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "A" Then
                For Each dr As DataRow In dtPolicyInfo.Rows
                    If Mid(dr.Item("ProductID"), 2, 4) = "RE15" OrElse Mid(dr.Item("ProductID"), 2, 4) = "RE20" Then
                        MsgBox("Policy Letter is not available, HRE15/HRE20/URE15/URE20 is attached." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
                        Exit Function
                    End If
                Next
            ElseIf dsMisc.Tables("product_type").Rows(0).Item("PrintValueReport") = "Q" Then
                MsgBox("Policy value quotation may be needed." & strPolicy, MsgBoxStyle.Exclamation, "Policy Letter")
            End If
        End If

        If dsMisc.Tables("PolicyAccount").Rows(0).Item("CompanyID") = "EAA" Then
            Dim objDB As Object
            Dim objRS As ADODB.Recordset
            objDB = CreateObject("Dbsecurity.Database")
            Call objDB.Connect(gsUser, strValProj, strValConn)

            'AC - Change advance compilation option to configuration file - start
            '#If UAT = 1 Then
            '            strSQL = "call CSDCIWSBP.policyval('" & strPolicy & "')"
            '#Else
            '            strSQL = "call CIWLIB.policyval('" & strPolicy & "')"
            '#End If
            If gUAT Then
                strSQL = "call CSDCIWSBP.policyval('" & strPolicy & "')"
            Else
                strSQL = "call CIWLIB.policyval('" & strPolicy & "')"
            End If
            'AC - Change advance compilation option to configuration file - end


            objRS = objDB.executestatement(strSQL)

            If objRS.RecordCount > 0 Then
                Dim drVal As DataRow = dsMisc.Tables("csw_policy_value").NewRow
                drVal("cswval_id") = -1
                For i As Integer = 0 To objRS.Fields.Count - 1
                    If objRS.Fields(i).Type = ADODB.DataTypeEnum.adChar Or objRS.Fields(i).Type = ADODB.DataTypeEnum.adVarChar Then
                        drVal.Item("cswval_" & objRS.Fields(i).Name) = RTrim(objRS.Fields(i).Value)
                    Else
                        drVal.Item("cswval_" & objRS.Fields(i).Name) = objRS.Fields(i).Value
                    End If
                Next
                drVal("cswval_TPOLID") = strPolicy
                drVal("cswval_PUASITotal") = dsMisc.Tables("PolicyAccount").Rows(0).Item("AdditionDeathCvr")
                dsMisc.Tables("csw_policy_value").Rows.Add(drVal)
            End If
        Else
            ' Call Web service to get value
            Dim dtVAL As DataTable
            Dim strErr As String
            Dim dblTotal As Double

            dtVAL = GetLAPolicyValueMcu(strPolicy, dsMisc.Tables("product_type").Rows(0).Item("ProductPolValueFunc"), dblTotal, strErr)
            If strErr = "" Then
                If dtVAL IsNot Nothing AndAlso dtVAL.Rows.Count > 0 Then
                    With dtVAL
                        Dim drVal As DataRow = dsMisc.Tables("csw_policy_value").NewRow
                        drVal("cswval_id") = -1
                        drVal("cswval_TFLOID") = "Y"
                        drVal("cswval_TPOLID") = strPolicy
                        drVal("cswval_TASADT") = CStr(CInt(Format(Today, "yyyyMMdd")) - 18000000)
                        drVal("cswval_TPADDT") = DBNull.Value
                        drVal("cswval_TCSHVL") = .Rows(0).Item("TotalSurVal")
                        drVal("cswval_TBSCSV") = .Rows(0).Item("BaseCashValue")
                        drVal("cswval_TVLPUA") = .Rows(0).Item("PUACashValue")
                        drVal("cswval_TDIVDP") = .Rows(0).Item("DivOnDeposit")
                        drVal("cswval_TDEPIN") = .Rows(0).Item("DivDepositInt")
                        drVal("cswval_TPDF") = .Rows(0).Item("PDFAmount")
                        drVal("cswval_TPDFIN") = .Rows(0).Item("PDFInt")
                        drVal("cswval_TPRMRD") = .Rows(0).Item("PremRefund")
                        drVal("cswval_TLONAT") = .Rows(0).Item("Loan")
                        drVal("cswval_TLONIT") = .Rows(0).Item("LoanInt")
                        drVal("cswval_TAPLAT") = .Rows(0).Item("APL")
                        drVal("cswval_TAPLIT") = .Rows(0).Item("APLInt")
                        drVal("cswval_TMAXLN") = DBNull.Value
                        drVal("cswval_TDSCLN") = DBNull.Value
                        drVal("cswval_TBSELN") = DBNull.Value
                        drVal("cswval_TDSCFR") = DBNull.Value
                        drVal("cswval_TINRRB") = DBNull.Value
                        drVal("cswval_TRDCHV") = DBNull.Value
                        drVal("cswval_TCOUDP") = .Rows(0).Item("Coupon")
                        drVal("cswval_TCOUIT") = .Rows(0).Item("CouponInt")
                        drVal("cswval_TERRFG") = DBNull.Value
                        drVal("cswval_TOSPRM") = DBNull.Value
                        drVal("cswval_TREAMT") = DBNull.Value
                        drVal("cswval_DivYear") = .Rows(0).Item("DivYear")
                        drVal("cswval_CouYear") = .Rows(0).Item("CouponYear")
                        drVal("cswval_DivDeclare") = .Rows(0).Item("DivDeclare")
                        drVal("cswval_PremSusp") = .Rows(0).Item("PremSuspense")
                        drVal("cswval_PremRefund") = DBNull.Value
                        drVal("cswval_PUASITotal") = .Rows(0).Item("PaidUpAddition")
                        drVal("cswval_PUASICurrent") = .Rows(0).Item("CurrentPaidUp")
                        drVal("cswval_CouOpt") = .Rows(0).Item("CouponOpt")
                        drVal("cswval_DivDepositInt") = DBNull.Value
                        drVal("cswval_DivOpt") = .Rows(0).Item("DivOpt")
                        drVal("cswval_MiscSusp") = .Rows(0).Item("MiscSuspense")
                        drVal("cswval_CouDelcare") = .Rows(0).Item("TotalCouponDeclare")
                        dsMisc.Tables("csw_policy_value").Rows.Add(drVal)
                    End With
                Else
                    With dtVAL
                        Dim drVal As DataRow = dsMisc.Tables("csw_policy_value").NewRow
                        drVal("cswval_id") = -1
                        drVal("cswval_TFLOID") = "Y"
                        drVal("cswval_TPOLID") = strPolicy
                        drVal("cswval_TASADT") = CStr(CInt(Format(Today, "yyyyMMdd")) - 18000000)
                        drVal("cswval_TCSHVL") = dblTotal
                        dsMisc.Tables("csw_policy_value").Rows.Add(drVal)
                    End With
                End If
            Else
                MsgBox(strErr, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End If
        End If

        'For Each dt As DataTable In dsMisc.Tables
        '    MsgBox(dt.TableName & "=" & dt.Rows.Count)
        'Next
        For i As Integer = 0 To dsMisc.Tables("PolicyAccount").Rows.Count - 1
            dsMisc.Tables("PolicyAccount").Rows(i)("BillNo") = ""
        Next
        Try
            rpt.Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            rpt.Database.Tables("csw_ing_logo_table").SetDataSource(dsMisc.Tables("csw_ing_logo_table"))
            rpt.Subreports("ClientInfo.rpt").Database.Tables("cswsp_ClientInfo;1").SetDataSource(dtClientInfo)
            rpt.Subreports("ClientInfo.rpt - 01").Database.Tables("cswsp_ClientInfo;1").SetDataSource(dtClientInfo)
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("CCDR").SetDataSource(dsMisc.Tables("CCDR"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("DDA").SetDataSource(dsMisc.Tables("DDA"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("BillingTypeCodes").SetDataSource(dsMisc.Tables("BillingTypeCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("ModeCodes").SetDataSource(dsMisc.Tables("ModeCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("AccountStatusCodes").SetDataSource(dsMisc.Tables("AccountStatusCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("CCDRStatusCodes").SetDataSource(dsMisc.Tables("CCDRStatusCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("DDAStatusCodes").SetDataSource(dsMisc.Tables("DDAStatusCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("DividendOptionCodes").SetDataSource(dsMisc.Tables("DividendOptionCodes"))
            rpt.Subreports("PolicyInfo.rpt").Database.Tables("CouponOptionCodes").SetDataSource(dsMisc.Tables("CouponOptionCodes"))

            rpt.Subreports("PolicyInfo.rpt - 01 - 02").Database.Tables("cswsp_policyinfo;1").SetDataSource(dtPolicyInfo)

            rpt.Subreports("PolicyInfo.rpt - 01 - 01 - 01").Database.Tables("cswsp_corider2;1").SetDataSource(dtCoRider)
            rpt.Subreports("PolicyInfo.rpt - 01 - 01 - 01").Database.Tables("Product_Chi").SetDataSource(dsMisc.Tables("Product_Chi"))

            rpt.Subreports("PolicyValue.rpt").Database.Tables("csw_policy_value").SetDataSource(dsMisc.Tables("csw_policy_value"))
            rpt.Subreports("PolicyValue.rpt").Database.Tables("PolicyAccount").SetDataSource(dsMisc.Tables("PolicyAccount"))
            'rpt.Subreports("PolicyValue.rpt").Database.Tables("CouponOptionCodes").SetDataSource(dsMisc.Tables("csw_policy_value"))
            'rpt.Subreports("PolicyValue.rpt").Database.Tables("DividendOptionCodes").SetDataSource(dsMisc.Tables("csw_policy_value"))

            rpt.SetParameterValue("PolicyID", strPolicy)

            If frmParam.chkChi.Checked = True Then
                rpt.SetParameterValue("Lang", "chi")
            End If

            If frmParam.chkEng.Checked = True Then
                rpt.SetParameterValue("Lang", "eng")
            End If

            rpt.SetParameterValue("UFVal", "-99")
            rpt.SetParameterValue("TotalPremiumPaid", "0.00")
            rpt.SetParameterValue("ULink", "N")
            rpt.SetParameterValue("ReqID", "-1")
            rpt.SetParameterValue("AgentName", dsMisc.Tables("agentcodes").Rows(0).Item("AgName"))
            rpt.SetParameterValue("AgentPhone", dsMisc.Tables("agentcodes").Rows(0).Item("PhoneNumber"))

            '20171103 Levy
            Dim LevyInsurance As Double = 0
            Dim LevySuspense As Double = 0
            ' Dim LevyTotalModalPremium As Double = 0
            Dim premium As Double = 0
            Dim policyCurrency As String = ""
            Dim paidToDate As Date = New Date

            Try

                LevySuspense = GetLevySuspense(strPolicy)
                ' LevyInsurance = GetLevyAmountOutstanding(strPolicy)
                premium = dsMisc.Tables("PolicyAccount").Rows(0)("ModalPremium")
                policyCurrency = dsMisc.Tables("PolicyAccount").Rows(0)("PolicyCurrency")
                paidToDate = dsMisc.Tables("PolicyAccount").Rows(0)("PaidToDate")
                LevyInsurance = GetLevyAmountQuotation(strPolicy, policyCurrency, premium, paidToDate)
                'LevyTotalModalPremium =
            Catch ex As Exception
                'do nothing
            End Try

            Try
                rpt.SetParameterValue("LevyInsurance", LevyInsurance.ToString("0.00"))
                rpt.SetParameterValue("LevySuspense", LevySuspense.ToString("0.00"))
                rpt.SetParameterValue("LevyTotalModalPremium", String.Format("{0:n}", premium + LevyInsurance))
            Catch ex As Exception
                'do nothing
            End Try

        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Function
    ' ES007 end

    Private Function GetLevySuspense(ByVal policyNo As String)

        Dim levySuspense As Double = 0
        Dim strErr As String = ""

        Dim objMQQueHeaderMcu As Utility.Utility.MQHeader
        Dim objDBHeaderMcu As Utility.Utility.ComHeader


        objMQQueHeaderMcu = gobjMQQueHeader
        objMQQueHeaderMcu.CompanyID = g_McuComp
        objMQQueHeaderMcu.EnvironmentUse = g_McuEnv
        objMQQueHeaderMcu.RemoteQueue = g_WinRemoteMcuQ
        objMQQueHeaderMcu.ReplyToQueue = g_LAReplyMcuQ
        objMQQueHeaderMcu.LocalQueue = g_WinLocalMcuQ
        objDBHeaderMcu = gobjDBHeader
        objDBHeaderMcu.CompanyID = g_McuComp

        objDBHeaderMcu.EnvironmentUse = g_McuEnv
        'objCI.MQQueuesHeader = GetMQQueHeader()
        'objCI.DBHeader = GetMComHeader()
        objCI.MQQueuesHeader = gobjMQQueHeader
        objCI.DBHeader = gobjDBHeader
        objCI.CiwHeader = objCI.DBHeader
        objCI.GetLevyAmountSuspense(policyNo, levySuspense, strErr)
        If (strErr <> "") Then
            levySuspense = 0
        End If

        Return levySuspense
    End Function

    Private Function GetLevyAmountQuotation(ByVal policyNumber As String, _
                                                    ByVal currency As String, _
                                                    ByVal premiumAmount As Double, ByVal paidToDate As DateTime) As Double

        Dim strErr As String = ""
        Dim premiumAllocatedAmountDue As String = "0.00"
        Dim premiumAmountDue As String = "0.00"
        Dim levyQuotationAmount As String = "00.0"

        Dim objMQQueHeaderMcu As Utility.Utility.MQHeader
        Dim objDBHeaderMcu As Utility.Utility.ComHeader


        objMQQueHeaderMcu = gobjMQQueHeader
        objMQQueHeaderMcu.CompanyID = g_McuComp
        objMQQueHeaderMcu.EnvironmentUse = g_McuEnv
        objMQQueHeaderMcu.RemoteQueue = g_WinRemoteMcuQ
        objMQQueHeaderMcu.ReplyToQueue = g_LAReplyMcuQ
        objMQQueHeaderMcu.LocalQueue = g_WinLocalMcuQ
        objDBHeaderMcu = gobjDBHeader
        objDBHeaderMcu.CompanyID = g_McuComp
        objDBHeaderMcu.EnvironmentUse = g_McuEnv

        'objCI.MQQueuesHeader = GetMQQueHeader()
        'objCI.DBHeader = GetMComHeader()
        objCI.MQQueuesHeader = gobjMQQueHeader
        objCI.DBHeader = gobjDBHeader
        objCI.CiwHeader = objCI.DBHeader
        objCI.GetLevyQuotation(policyNumber, currency, premiumAmount, paidToDate, paidToDate, False, "R", "CAP", premiumAllocatedAmountDue, premiumAmountDue, levyQuotationAmount, strErr)

        If (strErr <> "") Then
            Return 0
        End If

        Return CDbl(levyQuotationAmount)

    End Function
#End Region

#Region "Payment Letter"

    Public Sub Payment_Rpt()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
        Dim ds1 As New DataSet
        ds1.ReadXmlSchema(My.Application.Info.DirectoryPath & "\payment.xsd") '"C:\View\BCH_LACRS_UAT\E_TEAM\CS2005\INGLife.LifeAsia\CS2005\payment.xsd")
        Dim IsLARec As Boolean = False
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end

        Dim ds As New DataSet("dsPaymentHistory")
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter


        Dim strPolicy, strErrMsg As String
        Dim lngErrNo As Long
        Dim intCnt As Integer
        Dim datFrom, datStart, datEnd, datLast As Date
        Dim dtPAYH, dtPAYH_All, dtPoList, dtORDUNA, dtPolMisc As DataTable
        Dim blnCont As Boolean = True

        Dim frmInput As New frmPAYHRpt
        frmInput.Text = "Payment History Letter"
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Text = DateSerial(Year(Today), 1, 1)
        frmInput.txtTo.Text = Today
        frmInput.RadioButton1.Enabled = False
        frmInput.RadioButton2.Enabled = False
        frmInput.ShowDialog()

        blnCancel = True
        If frmInput.DialogResult = DialogResult.Cancel Then
            Exit Sub
        End If

        strPolicy = frmInput.PolicyNo
        datFrom = frmInput.ToDate
        datStart = datFrom
        datEnd = frmInput.FromDate

        If strPolicy = "" Then
            MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            blnCancel = True
            Exit Sub
        End If

        If frmInput.chkChi.Checked = False AndAlso frmInput.chkEng.Checked = False Then
            MsgBox("Please choose Eng/Chi version of letter.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            blnCancel = True
            Exit Sub
        End If
        'strPolicy = "2046300010"
        'datFrom = #1/1/2006#

        wndMain.Cursor = Cursors.WaitCursor

        '#If UAT <> 0 Then
        '        strPolicy = "U9611330"
        '#End If

        Dim drs As DataRowView
        Dim strPI, strPH, strAG As String


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
        Dim dsPolicySend As New DataSet
        Dim dsPolicyCurr As New DataSet
        Dim objMQQueHeaderMcu As Utility.Utility.MQHeader
        Dim strTime As String = ""
        Dim strerr As String = ""
        Dim blnGetPolicy As Boolean
        Dim dr As DataRow
        Dim dtSendData As New DataTable
        Dim objDBHeaderMcu As Utility.Utility.ComHeader

        dtSendData.Columns.Add("PolicyNo")
        dr = dtSendData.NewRow
        dr("PolicyNo") = RTrim(strPolicy)
        dtSendData.Rows.Add(dr)

        dsPolicySend.Tables.Add(dtSendData)
        'objMQQueHeaderMcu.UserID = gsUser
        'objMQQueHeaderMcu.QueueManager = g_Qman '"LACSQMGR1" '"WINTEL"
        'objMQQueHeaderMcu.RemoteQueue = g_WinRemoteQ '"LACSSIT02.TO.LA400SIT02" '"LIFEASIA.RQ1"
        'objMQQueHeaderMcu.ReplyToQueue = g_LAReplyQ '"LA400SIT02.TO.LACSSIT02" '"WINTEL.RQ1"
        'objMQQueHeaderMcu.LocalQueue = g_WinLocalQ  '"LACSSIT02.QUEUE1.LCL" '"WINTEL.LQ1"
        'objMQQueHeaderMcu.Timeout = 90000000 'My.Settings.Timeout     

        objMQQueHeaderMcu = gobjMQQueHeader
        objMQQueHeaderMcu.UserID = gsUser
        objMQQueHeaderMcu.QueueManager = g_Qman
        objMQQueHeaderMcu.CompanyID = g_McuComp
        objMQQueHeaderMcu.EnvironmentUse = g_McuEnv
        objMQQueHeaderMcu.RemoteQueue = g_WinRemoteMcuQ
        objMQQueHeaderMcu.ReplyToQueue = g_LAReplyMcuQ
        objMQQueHeaderMcu.LocalQueue = g_WinLocalMcuQ
        objMQQueHeaderMcu.Timeout = 90000000 'My.Settings.Timeout

        'objDBHeaderMcu.UserID = gsUser
        'objDBHeaderMcu.EnvironmentUse = g_Env '"SIT02"
        'objDBHeaderMcu.ProjectAlias = "LAS" '"LAS"
        'objDBHeaderMcu.CompanyID = g_Comp '"ING"
        'objDBHeaderMcu.UserType = "LASUPDATE" '"LASUPDATE"
        objDBHeaderMcu.UserID = gsUser
        objDBHeaderMcu = gobjDBHeader
        objDBHeaderMcu.CompanyID = g_McuComp
        objDBHeaderMcu.EnvironmentUse = g_McuEnv
        objDBHeaderMcu.ProjectAlias = "LAS"
        objDBHeaderMcu.UserType = "LASUPDATE"

        If My.Settings.LAReady = True Then
            Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
            clsPOS.MQQueuesHeader = objMQQueHeaderMcu
            clsPOS.DBHeader = objDBHeaderMcu

            dsPolicyCurr.Tables.Clear()

            blnGetPolicy = clsPOS.GetPolicy(dsPolicySend, dsPolicyCurr, strTime, strerr)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                    ds1.Tables(0).Columns.Remove("ClientID")
                    Dim dr1 As DataRow
                    dr1 = ds1.Tables(0).NewRow()
                    For i As Integer = 0 To ds1.Tables(0).Columns.Count - 1
                        Select Case ds1.Tables(0).Columns(i).ToString
                            Case "PolicyAccountID"
                                dr1(i) = RTrim(strPolicy)
                            Case "PaidToDate"
                                dr1(i) = dsPolicyCurr.Tables(0).Rows(0)("Paid_To_Date")
                            Case "POAGCY"
                                dr1(i) = dsPolicyCurr.Tables(0).Rows(0)("S_Agent_No")
                            Case Else
                                Select Case ds1.Tables(0).Columns(i).DataType.ToString
                                    Case "System.String"
                                        dr1(i) = ""
                                    Case "System.Decimal", "System.Int16"
                                        dr1(i) = 0
                                    Case "System.DateTime"
                                    Case Else
                                        dr1(i) = ""
                                End Select
                        End Select
                    Next
                    IsLARec = True
                    ds1.Tables(0).Rows.Add(dr1)
                    dtPoList = ds1.Tables(0).Copy
                    ds.Tables.Add(dtPoList)
                End If
            End If

        End If

        If IsLARec Then
            If dtPoList.Rows.Count > 0 Then
                strAG = Right(dtPoList.Rows(0).Item("POAGCY").ToString.Trim, 5)
                For i As Integer = 0 To dtPoList.Rows.Count - 1
                    dtPoList.Rows(i).Item("POAGCY") = strAG
                Next
            End If
            'Get Data WS 1 PI, PH            
            Dim dtCustomerIDResult As New DataTable
            'Using wsCRS As New CRSWS.CRSWS()
            '    Dim response As New CRSWS.WSResponseOfCustomerID
            '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
            '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
            '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
            '    End If
            '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
            '    wsCRS.Timeout = 10000000

            '    response = wsCRS.GetPIPHCustomerID(getCompanyCode(g_McuComp), getEnvCode(), strPolicy)

            '    If response Is Nothing Or response.Success = False Then
            '        MsgBox("Fail to GetCustomerID :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
            '    Else
            '        strPI = response.Data.PI
            '        If (Not String.IsNullOrEmpty(response.Data.PH)) Then
            '            strPH = response.Data.PH
            '            dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
            '            dtPoList.Rows(0).Item("ClientID") = response.Data.PH
            '        End If
            '    End If
            'End Using
            Dim retDs2 As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_McuComp), "GET_PI_PH_CUSTOMERID", New Dictionary(Of String, String)() From {{"strInPolicy", strPolicy}})
            If Not IsNothing(retDs2) Then
	            If retDs2.Tables(0).Rows.Count > 0 Then
                    If Not String.IsNullOrEmpty(retDs2.Tables(0).Rows(0)("PI"))
                        strPI = retDs2.Tables(0).Rows(0)("PI")
                    End If
                    If Not String.IsNullOrEmpty(retDs2.Tables(0).Rows(0)("PH"))
                        strPH = retDs2.Tables(0).Rows(0)("PH")
                        dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
                        dtPoList.Rows(0).Item("ClientID") = strPH
                    End If
	            End If
            End If


            'strSQL = "select AgentCode from Customer where customerid in (select customerid from csw_poli_rel where policyaccountid = '" & strPolicy & "' and policyrelatecode in ('SA'))"
            'If sqlrdr.Read Then
            '    strPI = sqlrdr("customerID")
            'End If
            'sqlrdr.Close()
            'sqlcmd.Dispose()

            'dtPolMisc.DefaultView.RowFilter = "PolicyRelateCode = 'PH'"
            'If dtPolMisc.DefaultView.Count > 0 Then
            ' drs = dtPolMisc.DefaultView.Item(0)
            'strPH = drs.Item("ClientID")
            'dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
            'dtPoList.Rows(0).Item("ClientID") = drs.Item("ClientID")
            'End If

            'Get Data WS 2 csw_policy_address GetCustomerAndAgentInfo
            Dim dtCustomerInfoResult As New DataTable
            'Using wsCRS As New CRSWS.CRSWS()
            '    Dim response As New CRSWS.WSResponseOfListOfCustomerInfo
            '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
            '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
            '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
            '    End If
            '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
            '    wsCRS.Timeout = 10000000

            '    response = wsCRS.GetCustomerAndAgentInfo(getCompanyCode(g_McuComp), getEnvCode(), strPolicy)

            '    If response Is Nothing Or response.Success = False Then
            '        MsgBox("Fail to GetCustomerInfo :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
            '    Else
            '        dtCustomerInfoResult = ConvertToDataTable(Of CRSWS.CustomerInfo)(response.Data)
            '    End If

            'End Using
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_McuComp), "GET_CUSTOMER_AGENT_INFO", New Dictionary(Of String, String)() From {{"strInPolicy", strPolicy}})
            If Not IsNothing(retDs) Then
	            If retDs.Tables(0).Rows.Count > 0 Then
                    dtCustomerInfoResult = retDs.Tables(0).Copy
                End If
            End If

            '            sqlconnect.ConnectionString = strCIWConn



            'sqlda.Fill(ds1, "ORDUNA")
            ds1.Tables.Remove("ORDUNA")
            dtCustomerInfoResult.TableName = "ORDUNA"
            ds1.Tables.Add(dtCustomerInfoResult)
            ds.Tables.Add(ds1.Tables("ORDUNA").Copy)
            dtORDUNA = ds1.Tables("ORDUNA").Copy


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
            Dim clsCRS As New LifeClientInterfaceComponent.clsCRS
            dtSendData = New DataTable
            dsPolicySend.Tables.RemoveAt(0)
            dsPolicyCurr.Tables.RemoveAt(0)
            dtSendData.Columns.Add("Policy_No")
            'dr = dtSendData.NewRow
            dtSendData.Columns.Add("FromDate")
            'dr = dtSendData.NewRow
            'dtSendData.Rows.Add(dr)
            dtSendData.Columns.Add("ToDate")
            dr = dtSendData.NewRow
            dtSendData.Rows.Add(dr)
            'dtSendData.Rows(0)("FromDate") = datFrom
            'dtSendData.Rows(0)("ToDate") = datEnd
            dtSendData.Rows(0)("ToDate") = datFrom
            dtSendData.Rows(0)("FromDate") = datEnd
            dtSendData.Rows(0)("Policy_No") = RTrim(strPolicy)
            'dtSendData.Rows.Add(dr)
            dsPolicySend.Tables.Add(dtSendData)
            clsCRS.DBHeader = objDBHeaderMcu   ' Fix UAT wrong URL problem
            clsCRS.MQQueuesHeader = objMQQueHeaderMcu
            blnGetPolicy = clsCRS.getPaymentHist(dsPolicySend, dsPolicyCurr, strerr)
            If dsPolicyCurr.Tables.Count > 0 Then
                If dsPolicyCurr.Tables(0).Rows.Count > 0 Then
                    'ds1.Tables("PAYH").Columns.Remove("Lang")
                    ds1.Tables("PAYH").Columns.Remove("Lang") 'Payment History
                    Dim dr1 As DataRow
                    For j As Integer = 0 To dsPolicyCurr.Tables(1).Rows.Count - 1
                        dr1 = ds1.Tables("PAYH").NewRow()
                        For i As Integer = 0 To ds1.Tables("PAYH").Columns.Count - 1
                            Select Case ds1.Tables("PAYH").Columns(i).ToString
                                Case "PaymentType"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("PayTypeCode")
                                    'dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("PayTypeDesc")
                                    '                       Case "ClientID"
                                    '                            dr1(i) = dsPolicyCurr.Tables(0).Rows(0)("Life_No")
                                Case "Date"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("Payment_Date")
                                Case "Currency"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("Curr")
                                Case "ReceivedAmount"
                                    dr1(i) = dsPolicyCurr.Tables(1).Rows(j)("RecAmt")
                                Case "PolicyAccountID"
                                    dr1(i) = RTrim(strPolicy)
                                Case Else
                                    Select Case ds1.Tables("PAYH").Columns(i).DataType.ToString
                                        Case "System.String"
                                            dr1(i) = ""
                                        Case "System.Decimal", "System.Int16"
                                            dr1(i) = 0
                                        Case "System.DateTime"
                                        Case Else
                                            dr1(i) = ""
                                    End Select
                            End Select
                        Next
                        ds1.Tables("PAYH").Rows.Add(dr1)
                    Next
                    dtPAYH = ds1.Tables("PAYH").Copy
                    'dtPAYH_All = dtPAYH.Clone 
                    lngErrNo = 0
                    Dim blnEng, blnChi As Boolean

                    blnEng = frmInput.chkEng.Checked
                    blnChi = frmInput.chkChi.Checked
                    '                    ds.Tables.Add(ds1.Tables("PAYH").Copy)
                    If lngErrNo = 0 Then

                        If dtPAYH_All Is Nothing Then
                            dtPAYH_All = dtPAYH.Clone
                            dtPAYH_All.Columns.Add("Lang", System.Type.GetType("System.String"))
                        End If

                        Dim intColCnt As Integer = dtPAYH.Columns.Count
                        Dim ar() As Object
                        Dim i As Integer
                        intCnt = dtPAYH.Rows.Count - 1
                        If dtPAYH.Rows.Count > 0 Then
                            If dtPAYH.Rows(0).Item("ContFlag") = "Y" Then
                                datFrom = dtPAYH.Rows(intCnt).Item("Date")
                                datLast = datFrom
                                blnCont = True
                            Else
                                datLast = #1/1/1900#
                                blnCont = False
                            End If
                        Else
                            datLast = #1/1/1900#
                            blnCont = False
                        End If
                        For i = 0 To intCnt
                            If dtPAYH.Rows(i).Item("Date") <> datLast And dtPAYH.Rows(i).Item("Date") >= datEnd Then
                                ar = dtPAYH.Rows(i).ItemArray
                                ReDim Preserve ar(intColCnt)
                                'If Not blnChiOnly Then
                                If blnEng Then
                                    ar(intColCnt) = "E"
                                    dtPAYH_All.Rows.Add(ar)
                                End If
                                If blnChi Then
                                    ar(intColCnt) = "C"
                                    dtPAYH_All.Rows.Add(ar)
                                End If
                            End If
                        Next
                    Else
                        MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        wndMain.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If
            End If
            'not IsLARec 
        Else

            dtPoList = objCS.GetPolicySummary(strPolicy, lngErrNo, strErrMsg)
            If Not dtPoList Is Nothing Then
                dtPoList.TableName = "POLINF"
                If lngErrNo = 0 Then
                    ds.Tables.Add(dtPoList)
                End If
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end
            dtPolMisc = objCS.GetPolicyMisc(strPolicy, lngErrNo, strErrMsg)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
            ' ''If lngErrNo <> 0 Then
            ' ''    Dim dt1 As New DataTable
            ' ''    Dim dr1 As DataRow
            ' ''    Dim dc1 As DataColumn
            ' ''    dc1 = New DataColumn()
            ' ''    dc1.ColumnName = "PolicyRelateCode"
            ' ''    dc1.DataType = System.Type.GetType("System.String")
            ' ''    dt1.Columns.Add(dc1)
            ' ''    dc1 = New DataColumn()
            ' ''    dc1.ColumnName = "ClientID"
            ' ''    dc1.DataType = System.Type.GetType("System.String")
            ' ''    dt1.Columns.Add(dc1)
            ' ''    dr1 = dt1.NewRow()
            ' ''    For i As Integer = 0 To dt1.Columns.Count - 1
            ' ''        Select Case dt1.Columns(i).ToString
            ' ''            Case "PolicyRelateCode"
            ' ''                dr1(i) = "PH"
            ' ''            Case "ClientID"
            ' ''                dr1(i) = "50001527"
            ' ''            Case Else
            ' ''                Select Case dt1.Columns(i).DataType.ToString
            ' ''                    Case "System.String"
            ' ''                        dr1(i) = ""
            ' ''                    Case "System.Decimal", "System.Int16"
            ' ''                        dr1(i) = 0
            ' ''                    Case "System.DateTime"
            ' ''                    Case Else
            ' ''                        dr1(i) = ""
            ' ''                End Select
            ' ''        End Select
            ' ''    Next

            ' ''    dt1.Rows.Add(dr1)
            ' ''    dtPolMisc = dt1.Copy
            ' ''    lngErrNo = 0
            ' ''    strErrMsg = ""
            ' ''    'ds.Tables.Add(ds1.Tables(1).Copy)
            ' ''End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end
            If lngErrNo = 0 Then
                dtPolMisc.DefaultView.RowFilter = "PolicyRelateCode = 'PH'"
                If dtPolMisc.DefaultView.Count > 0 Then
                    drs = dtPolMisc.DefaultView.Item(0)
                    strPH = drs.Item("ClientID")
                    dtPoList.Columns.Add("ClientID", Type.GetType("System.String"))
                    dtPoList.Rows(0).Item("ClientID") = drs.Item("ClientID")
                End If

                dtPolMisc.DefaultView.RowFilter = "PolicyRelateCode = 'PI'"
                If dtPolMisc.DefaultView.Count > 0 Then
                    For i As Integer = 0 To dtPolMisc.DefaultView.Count - 1
                        drs = dtPolMisc.DefaultView.Item(i)
                        If drs.Item("COTRAI") > 1 Then
                            Exit For
                        End If
                        If strPI = "" Then
                            strPI &= drs.Item("ClientID")
                        Else
                            strPI &= "', '" & drs.Item("ClientID")
                        End If
                    Next
                End If
            End If

            If dtPoList.Rows.Count > 0 Then
                strAG = Right(dtPoList.Rows(0).Item("POAGCY"), 5)
                For i As Integer = 0 To dtPoList.Rows.Count - 1
                    dtPoList.Rows(i).Item("POAGCY") = strAG
                    'If dtPoList.Rows(i).Item("PolicyRelateCode") = "PI" Then
                    '    strPI = dtPoList.Rows(i).Item("ClientID")
                    'End If
                    'If dtPoList.Rows(i).Item("PolicyRelateCode") = "PH" Then
                    '    strPH = dtPoList.Rows(i).Item("ClientID")
                    'End If
                Next

                dtORDUNA = objCS.GetORDUNA("'" & strPI & "','" & strPH & "','" & "00000" & strAG & "'", lngErrNo, strErrMsg)

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''start
                ' ''If lngErrNo <> 0 Then
                ' ''    Dim dr1 As DataRow
                ' ''    dr1 = ds1.Tables(1).NewRow()
                ' ''    For i As Integer = 0 To ds1.Tables(1).Columns.Count - 1
                ' ''        Select Case ds1.Tables(1).Columns(i).ToString
                ' ''            Case "NamePrefix"
                ' ''                dr1(i) = "TestingPrefix"
                ' ''            Case "Namesuffix"
                ' ''                dr1(i) = "TestingSuffix"
                ' ''            Case "ClientID"
                ' ''                dr1(i) = "50001527"
                ' ''            Case Else
                ' ''                Select Case ds1.Tables(1).Columns(i).DataType.ToString
                ' ''                    Case "System.String"
                ' ''                        dr1(i) = ""
                ' ''                    Case "System.Decimal", "System.Int16"
                ' ''                        dr1(i) = 0
                ' ''                    Case "System.DateTime"
                ' ''                    Case Else
                ' ''                        dr1(i) = ""
                ' ''                End Select
                ' ''        End Select
                ' ''    Next
                ' ''    ds1.Tables(1).Rows.Add(dr1)
                ' ''    dtPoList = ds1.Tables(1).Copy
                ' ''    ds.Tables.Add(ds1.Tables(1).Copy)
                ' ''End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''end
                GetChiAddr(dtORDUNA)
                If lngErrNo = 0 Then
                    ds.Tables.Add(dtORDUNA)
                End If
            Else
                MsgBox("Policy information not found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If

            Dim blnChiOnly As Boolean = False
            dtORDUNA.DefaultView.RowFilter = "ClientID = '" & strPH & "'"
            If dtORDUNA.DefaultView.Count > 0 Then
                drs = dtORDUNA.DefaultView.Item(0)
                If drs.Item("USECHIIND") = "Y" Or (drs.Item("ADDRESSLINE1") = "" And drs.Item("ADDRESSLINE2") = "" And drs.Item("ADDRESSLINE3") = "" And drs.Item("AddressCity") = "") Then
                    blnChiOnly = True
                End If
            End If

            Dim blnEng, blnChi As Boolean

            blnEng = frmInput.chkEng.Checked
            blnChi = frmInput.chkChi.Checked

            If blnEng = True AndAlso blnChiOnly Then
                If MsgBox("Only Chinese version is available, print it instead?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Error") = MsgBoxResult.No Then
                    blnCancel = True
                    Exit Sub
                Else
                    blnChi = True
                    blnEng = False
                End If
            End If

            While blnCont
                dtPAYH = objCS.GetPaymentHistory(strPolicy, datFrom, lngErrNo, strErrMsg)

                If lngErrNo = 0 Then

                    If dtPAYH_All Is Nothing Then
                        dtPAYH_All = dtPAYH.Clone
                        dtPAYH_All.Columns.Add("Lang", System.Type.GetType("System.String"))
                    End If

                    Dim intColCnt As Integer = dtPAYH.Columns.Count
                    Dim ar() As Object
                    Dim i As Integer
                    intCnt = dtPAYH.Rows.Count - 1

                    If dtPAYH.Rows(0).Item("ContFlag") = "Y" Then
                        datFrom = dtPAYH.Rows(intCnt).Item("Date")
                        datLast = datFrom
                        blnCont = True
                    Else
                        datLast = #1/1/1900#
                        blnCont = False
                    End If

                    For i = 0 To intCnt
                        If dtPAYH.Rows(i).Item("Date") <> datLast And dtPAYH.Rows(i).Item("Date") >= datEnd Then
                            ar = dtPAYH.Rows(i).ItemArray
                            ReDim Preserve ar(intColCnt)
                            'If Not blnChiOnly Then
                            If blnEng Then
                                ar(intColCnt) = "E"
                                dtPAYH_All.Rows.Add(ar)
                            End If
                            If blnChi Then
                                ar(intColCnt) = "C"
                                dtPAYH_All.Rows.Add(ar)
                            End If
                        End If
                    Next
                Else
                    MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                    wndMain.Cursor = Cursors.Default
                    Exit Sub
                End If
            End While
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''LA rec modify
        If dtPAYH_All.Rows.Count = 0 Then
            MsgBox("No Payment History record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            ds.Tables.Add(dtPAYH_All)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
        'Get Data WS 3 PaymentInfo
        Dim dtPaymentInfoResult As New DataTable
        'Using wsCRS As New CRSWS.CRSWS()
        '    Dim response As New CRSWS.WSResponseOfPaymentInfo
        '    wsCRS.Url = Utility.Utility.GetWebServiceURL("CRSWS", gobjDBHeader, gobjMQQueHeader) 
        '    If System.Configuration.ConfigurationManager.AppSettings("Utility") = "Y" Then
        '        wsCRS.Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "WebServiceURL")
        '    End If
        '    wsCRS.Credentials = System.Net.CredentialCache.DefaultCredentials
        '    wsCRS.Timeout = 10000000

        '    response = wsCRS.GetPaymentInfo(getCompanyCode(g_McuComp), getEnvCode(), strPolicy, strAG)

        '    If response Is Nothing Or response.Success = False Then
        '        MsgBox("Fail to GetPaymentInfo :" + response.ErrorMsg, MsgBoxStyle.Exclamation, ReportHeader)
        '    Else
        '        'dtPaymentInfoResult = ConvertToDataTable(Of CRSWS.PaymentInfo)(response.Data)
        '        dtPaymentInfoResult = ConvertToDataTable(Of CRSWS.PaymentTypeCodes)(response.Data.PaymentTypeCodes)
        '        dtPaymentInfoResult.TableName = "PaymentTypeCodes"
        '        ds.Tables.Add(dtPaymentInfoResult)
        '        dtPaymentInfoResult = ConvertToDataTable(Of CRSWS.cswvw_cam_Agent_info)(response.Data.cswvw_cam_Agent_info)
        '        dtPaymentInfoResult.TableName = "cswvw_cam_Agent_info"
        '        ds.Tables.Add(dtPaymentInfoResult)
        '        dtPaymentInfoResult = ConvertToDataTable(Of CRSWS.DDARejectReasonCodes)(response.Data.DDARejectReasonCodes)
        '        dtPaymentInfoResult.TableName = "DDARejectReasonCodes"
        '        ds.Tables.Add(dtPaymentInfoResult)
        '        dtPaymentInfoResult = ConvertToDataTable(Of CRSWS.CCDRRejectReasonCodes)(response.Data.CCDRRejectReasonCodes)
        '        dtPaymentInfoResult.TableName = "CCDRRejectReasonCodes"
        '        ds.Tables.Add(dtPaymentInfoResult)
        '        dtPaymentInfoResult = ConvertToDataTable(Of CRSWS.agentcodes)(response.Data.agentcodes)
        '        dtPaymentInfoResult.TableName = "agentcodes"
        '        ds.Tables.Add(dtPaymentInfoResult)
        '        dtPaymentInfoResult = ConvertToDataTable(Of CRSWS.Logo)(response.Data.Logo)
        '        dtPaymentInfoResult.TableName = "Logo"
        '        ds.Tables.Add(dtPaymentInfoResult)
        '    End If

        'End Using
        Dim retDs1 As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(g_McuComp), "GET_PAYMENT_INFO",
                                                            New Dictionary(Of String, String)() From {
                                                                {"strInPolicy", strPolicy},
                                                                {"agentList", strAG}
                                                            })
        If Not IsNothing(retDs1) Then
	        If retDs1.Tables.Count > 0 Then
                For Each dt As DataTable In retDs1.Tables
                    ds.Tables.Add(dt.Copy)
                Next
	        End If
        End If




        'If IsLARec Then
        '    Dim objDB As New Object
        '    Dim ConnectionAlias As String = g_Comp + "CIW" + g_Env
        '    ConnectDB(objDB, g_ProjectAlias, ConnectionAlias, g_UserType, strerr)
        '    sqlconnect.ConnectionString = objDB.getDBString 'strCIWConn
        'Else

        'End If



        '#If UAT <> 0 Then
        '        Dim filename As String = "payment1.xsd"
        '        Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
        '        Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
        '        ds.WriteXmlSchema(MyXmlTextWriter)
        '        MyXmlTextWriter.Close()
        '#End If

        'MsgBox(ds.Tables("POLINF").Rows.Count)
        'MsgBox(dtPAYH.Rows.Count)
        'MsgBox(dtORDUNA.Rows.Count)
        'MsgBox(dtORDUNA.Rows(0)("ClientID"))
        'MsgBox(dtORDUNA.Rows(1)("ClientID"))

        dtORDUNA.DefaultView.RowFilter = "ClientID = '" & "00000" & strAG & "'"
        Dim strAgtName As String
        If dtORDUNA.DefaultView.Count > 0 Then
            drs = dtORDUNA.DefaultView.Item(0)
            strAgtName = Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
        Else
            strAgtName = ""
        End If

        dtORDUNA.DefaultView.RowFilter = "ClientID IN ('" & strPI & "')"
        Dim strInsName As String
        If dtORDUNA.DefaultView.Count > 0 Then
            For i As Integer = 0 To dtORDUNA.DefaultView.Count - 1
                drs = dtORDUNA.DefaultView.Item(i)
                If strInsName = "" Then
                    strInsName &= Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
                Else
                    strInsName &= ", " & Trim(drs.Item("NameSuffix")) & " " & Trim(drs.Item("FirstName"))
                End If
            Next
        Else
            strInsName = ""
        End If

        Dim strAgPhone As String
        If Not ds.Tables("agentcodes") Is Nothing AndAlso ds.Tables("agentcodes").Rows.Count > 0 Then
            strAgPhone = Trim(ds.Tables("agentcodes").Rows(0).Item("PhoneNumber"))
            strAgPhone = Left(strAgPhone, 4) + " " + Right(strAgPhone, 4)
        Else
            strAgPhone = ""
        End If

        wndMain.Cursor = Cursors.Default

        Try
            rpt.SetDataSource(ds)
            rpt.Subreports("Address").SetDataSource(ds)
            rpt.SetParameterValue("strCSR", gsCSRName)
            rpt.SetParameterValue("strChiCSR", gsCSRChiName)
            rpt.SetParameterValue("strSAName", strAgtName)
            rpt.SetParameterValue("strInsured", strInsName)
            rpt.SetParameterValue("strStart", CDate(frmInput.FromDate))
            rpt.SetParameterValue("strEnd", CDate(frmInput.ToDate))
            rpt.SetParameterValue("strPhone", strAgPhone)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        blnCancel = False

    End Sub
#End Region


    Public Class AppLoan
        Public agent As String
    End Class

   

    Private Function ExcecuteSQL(ByVal strSQL As String, ByVal strConn As String, ByRef dtResult As DataTable) As Boolean
        ExcecuteSQL = False

        Dim connection As SqlConnection = New SqlConnection(strConn)
        Dim command As SqlCommand = New SqlCommand(strSQL, connection)
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)

        adapter.Fill(dtResult)

        ExcecuteSQL = True
    End Function

    Private Function ExcecuteSql(ByVal command As SqlCommand, ByRef dtResult As DataTable) As Boolean
        ExcecuteSql = False

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)

        adapter.Fill(dtResult)

        ExcecuteSql = True
    End Function

    Private Sub TableToList(ByVal dtTable As DataTable, ByRef destList As System.Collections.Generic.List(Of String))

        For Each tmpRow As DataRow In dtTable.Rows
            destList.Add(tmpRow(0).ToString())
        Next

    End Sub

#Region "001"

    Public Sub PSCFollowLetter()
        blnCancel = True
        Try
            '1.Pop-up frmLtrParam box
            Dim frmInput As frmLtrParam = GetfrmLtrParam("PSC follow letter")
            frmInput.grpPrint.Enabled = False
            If Destination = PrintDest.pdExport Then
                frmInput.grpExport.Enabled = True
            End If
            frmInput.ShowDialog()
            If frmInput.DialogResult = DialogResult.Cancel Then
                Exit Sub
            End If

            '2.get input data and validate
            Dim policyNo As String = frmInput.PolicyNo
            If String.IsNullOrWhiteSpace(policyNo) Then
                MsgBox("Please Enter a Policy No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If

            '3.get data about the Word template by policyNo
            Dim dsRegion As New DataSet
            Dim dtField As DataTable = GetPSCFollowLetterData(policyNo, dsRegion)

            '4.prepare data to be inserted into the CCM DB
            Dim request As CCMWS.LetterRequest = SetupLetterRequest("PSCFollowMC", "O", "MCU", "MCU", policyNo)
            Dim fieldList As CCMWS.LetterField() = DataMapToLetterField(dtField)
            Dim regionSet As CCMWS.LetterRegion()() = DataMapToLetterRegion(dsRegion)

            '5.insert data to CCMS DB
            Dim insertResult As CCMWS.BaseResponseOfLetterInfo = CCMWebService.InsertLetterData(request, fieldList, regionSet)
            If insertResult.IsSuccess = False Then
                MsgBox(String.Format("Insert data to CCMS DB error: {0}", insertResult.ErrorMsg), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If

            '6.generate report and upolad it to CM
            Dim generateInput As CCMWS.LetterGenerateInput = New CCMWS.LetterGenerateInput()
            generateInput.LtrRequestId = insertResult.Value.LtrRequestId
            generateInput.DocType = CCMWS.DocTypeEnum.LFCEDoc
            generateInput.CMEvn = If(gUAT, "MCU", "MCUPRD")
            generateInput.IsUploadFile = If(Destination = PrintDest.pdPrinter, True, False)
            generateInput.FileFormat = If(frmInput.radPdf.Checked = True, CCMWS.FileFormat.pdf, If(frmInput.radWord.Checked = True, CCMWS.FileFormat.docx, CCMWS.FileFormat.xlsx))
            Dim generateResult As CCMWS.BaseResponseOfLetterGenerateInfo = CCMWebService.GenerateFile(generateInput)

            If generateResult.IsSuccess = False Then
                MsgBox(String.Format("Generate report or upload it to CM error: {0}", generateResult.ErrorMsg), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If
            outputFilePath = WriteByteToFile(generateResult.Value.fileByte, generateResult.Value.outputFileName)
            blnCancel = False
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Function GetfrmLtrParam(ByVal titleTxt As String) As frmLtrParam
        Dim frmInput As New frmLtrParam
        frmInput.Text = titleTxt
        frmInput.txtPolicy.Text = strLastPolicy
        frmInput.txtFrom.Text = DateSerial(Year(Today), 1, 1)
        frmInput.txtTo.Text = Today
        frmInput.txtFrom.Enabled = False
        frmInput.txtTo.Enabled = False
        frmInput.grpExport.Enabled = False
        Return frmInput
    End Function

    Private Function GetPSCFollowLetterData(ByVal policyNo As String, ByRef dsRegion As DataSet) As DataTable
        Dim dtFields As DataTable = GetPSCFollowLetterFields()
        Dim dtCommon As DataTable = GetCommonField(policyNo, strCIWMcuConn)
        dtFields.Rows.Add(dtFields.NewRow())
        dtFields.Rows(0)("PolicyNo") = policyNo
        dtFields.Rows(0)("LetterDate") = dtCommon.Rows(0)("LetterDateEng").ToString()
        dtFields.Rows(0)("ContactAddress1") = dtCommon.Rows(0)("ContactAddress1").ToString()
        dtFields.Rows(0)("ContactAddress2") = dtCommon.Rows(0)("ContactAddress2").ToString()
        dtFields.Rows(0)("ContactAddress3") = dtCommon.Rows(0)("ContactAddress3").ToString()
        dtFields.Rows(0)("ContactAddress4") = dtCommon.Rows(0)("ContactAddress4").ToString()
        dtFields.Rows(0)("ContactFullName") = dtCommon.Rows(0)("OwnerPrefixEng").ToString() & " " & dtCommon.Rows(0)("OwnerNameEng").ToString()
        dtFields.Rows(0)("AgentNameEng") = dtCommon.Rows(0)("AgentNameEng").ToString()
        dtFields.Rows(0)("AgentNameChi") = dtCommon.Rows(0)("AgentNameChi").ToString()
        dtFields.Rows(0)("AgentLocation") = dtCommon.Rows(0)("AgentLocation").ToString()

        'get cooling off date -s
        Dim dtContractDtl As DataTable = GetContractDetail(policyNo, g_McuComp)
        Dim coolingOffDate As String = dtContractDtl.Rows(0)("CooloffDate")
        If Not String.IsNullOrWhiteSpace(coolingOffDate) Then
            dtFields.Rows(0)("CoolingOffDateEng") = DateTime.ParseExact(coolingOffDate, "MM/dd/yyyy", Nothing).ToString("dd MMM yyyy")
            dtFields.Rows(0)("CoolingOffDateChi") = DateTime.ParseExact(coolingOffDate, "MM/dd/yyyy", Nothing).ToString("yyyy年M月d日")
        End If
        'get cooling off date -e

        'get plan name -s
        Dim dtPolicyAccount As DataTable = GetPolicyAccountInfo(policyNo, strCIWMcuConn)
        Dim productId As String = dtPolicyAccount.Rows(0)("ProductID").ToString.Trim()
        dtFields.Rows(0)("PlanNameEng") = GetPlanName(productId, strCIWMcuConn)
        dtFields.Rows(0)("PlanNameChi") = GetPlanNameChi(productId, strCIWMcuConn)
        'get plan name -e

        'get payment mode -s
        Dim mode As String = dtPolicyAccount.Rows(0)("Mode").ToString().Trim()
        Dim paymentMode As String = GetPaymentMode(mode, strCIWMcuConn)
        dtFields.Rows(0)("PayModeEng") = paymentMode
        dtFields.Rows(0)("PayModeChi") = GetPaymentModeChi(paymentMode)
        'get payment mode -e

        'get event detail -s
        Dim dtServiceLog As DataTable = GetServiceLogInfo_MCU(policyNo)
        Dim medium As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("EventSourceMedium").ToString().Trim())
        Dim eventCategory As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("cswecc_desc").ToString().Trim())
        Dim eventDetail As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("EventTypeDesc").ToString().Trim())
        'Dim eventTypeDetail As String = If(IsNothing(dtServiceLog) Or dtServiceLog.Rows.Count = 0, "", dtServiceLog.Rows(0)("csw_event_typedtl_desc").ToString().Trim())
        'get event detail -e

        'setup PSC_content -s
        Dim pscContentEng As String = String.Empty
        Dim pscContentChi As String = String.Empty
        '(Event Category: Courtesy Call - Vulnerable customer; Event Detail: Uncertain) or (Event Category: Courtesy Call - Suitability mismatch)
        If ("Courtesy Call - Vulnerable customer".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) And "Uncertain".Equals(eventDetail, StringComparison.OrdinalIgnoreCase)) Or
            "Courtesy Call – Suitability mismatch".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) Then

            pscContentEng = "As required by the AUTORIDADE MONETARIA DE MACAU (""AMCM"") to protect your rights, we are required to conduct a post-sale call with you after the issuance of the Policy. " &
                "As such, we have contacted you at your provided contact numbers """ & dtCommon.Rows(0)("OwnerPhone").ToString() & """ on the policy application form. " &
                "During the call, you indicated that you have queries on some of the matters related to the policy. " &
                "We have therefore informed our insurance agent/the broker (the ""Sales Representative"") to follow up with you for clarification." & vbCrLf & vbCrLf &
                "A Policy contract will be/has been delivered to you by our insurance agent/the Sales Representative. " &
                "Please read it carefully once you have received it because it contains important information and facts which you should know."

            pscContentChi = "根據澳門金融管理局的指引，為保障您的權益，我們需要於上述保單繕發後，致電給您進行售後電話跟進服務。" &
                "據此，我們較早前按照您於保單申請書上所提供的聯絡電話：""" & dtCommon.Rows(0)("OwnerPhone").ToString() & """ 與您聯絡。" &
                "對話當中，我們理解您對上述保單有疑問，我們遂要求我們的銷售代表/經紀(""理財顧問"")與您聯絡，以釐清您的疑問並作出解釋。" & vbCrLf & vbCrLf &
                "我們的銷售代表/經紀(""理財顧問"")將會/已經把您的保單合約送交給您。" &
                "請您收到後小心細閱有關文件的內容，因為當中列明了您必須知道的重要資料及概要。"
        Else
            Dim eventDetailEng As String = "."
            Dim eventDetailChi As String = "。"
            '<<CRS service log, medium=Letter, and, Event detail=Unwilling>>
            '<<CRS service log, medium=Letter, and, Event detail=Uncertain >> 
            If "Letter".Equals(medium, StringComparison.OrdinalIgnoreCase) And "Unwilling".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Then
                eventDetailEng = " on occasions but in vain."
                eventDetailChi = "，惟未能成功與您聯繫。"
            ElseIf "Letter".Equals(medium, StringComparison.OrdinalIgnoreCase) And "Uncertain".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Then
                eventDetailEng = ". During the call, you indicated that you have queries on some of the matters related to the policy. " &
                    "We have therefore informed our insurance agent/the broker (the ""Sales Representative"") to follow up with you for clarification."
                eventDetailChi = "。對話當中，我們理解您對上述保單有疑問，我們遂要求我們的銷售代表/經紀(""理財顧問"")與您聯絡，以釐清您的疑問並作出解釋。"
            End If

            pscContentEng = "As required by the AUTORIDADE MONETARIA DE MACAU (""AMCM"") to protect your rights, we are required to conduct a post-sale call with you after the issuance of the Policy. " &
                "As such, we have tried to contacted you at your provided contact numbers """ & dtCommon.Rows(0)("OwnerPhone").ToString() & """ on the policy application form" & eventDetailEng & "" & vbCrLf & vbCrLf &
                "A Policy contract will be/has been delivered to you by our insurance agent/the broker (the ""Sales Representative""). " &
                "Please read it carefully once you have received it because it contains important information and facts which you should know."

            pscContentChi = "根據澳門金融管理局的指引，為保障您的權益，我們需要於上述保單繕發後，致電給您進行售後電話跟進服務。" &
                "據此，我們較早前按照您於保單申請書上所提供的聯絡電話：""" & dtCommon.Rows(0)("OwnerPhone").ToString() & """ 嘗試與您聯絡" & eventDetailChi & "" & vbCrLf & vbCrLf &
                "我們的銷售代表/經紀(""理財顧問"")將會/已經把您的保單合約送交給您。" &
                "請您收到後小心細閱有關文件的內容，因為當中列明了您必須知道的重要資料及概要。"
        End If
        dtFields.Rows(0)("PSCContentEng") = pscContentEng
        dtFields.Rows(0)("PSCContentChi") = pscContentChi
        'setup PSC_content -e

        'setup Medical_plan Elite_term_plan Ex_policy_term -s
        Dim dtProductType As DataTable = GetCIWPRProductTypeInfo(productId, strCIWMcuConn)
        Dim isMedical As String = dtProductType.Rows(0)("CIWPT_IsMedical").ToString().Trim()
        Dim isTerm As String = dtProductType.Rows(0)("CIWPT_IsTerm").ToString().Trim()
        Dim isSinglePremium As String = dtProductType.Rows(0)("CIWPT_IsSinglePremium").ToString().Trim()
        If isMedical = "Y" Then
            dtFields.Rows(0)("Rmk_MedicalPlanEng") = LINEBREAK & "*It is the first year premium payable. Premium rates for each renewal are determined based on the age of the Insured at the next birthday, are not guaranteed are subject to change." & LINEBREAK
            dtFields.Rows(0)("Rmk_MedicalPlanChi") = LINEBREAK & "*此資料只顯示首年保費金額，續期保費率依被保人之下次生日年齡釐定，續期保費率並非保證不變且可能有所更改。" & LINEBREAK
        End If
        If isTerm = "Y" Then
            dtFields.Rows(0)("Rmk_EliteTermPlanEng") = LINEBREAK & "*It is the premium payable for the first 20 policy years. Staring from the 21st policy year, the premium rate will be determined annually at the sole discretion of the Company based on the age of the next birthday of the Insured at the time of renewal." & LINEBREAK
            dtFields.Rows(0)("Rmk_EliteTermPlanChi") = LINEBREAK & "*此資料只顯示首二十個保單年度應繳保費之金額，於第二十一個保單年度開始，每年續期保費將會按照受保人於續期時之下次生日年齡，並由本公司全權酌情訂定。" & LINEBREAK
        End If
        If isSinglePremium = "N" Then
            dtFields.Rows(0)("Rmk_ExPolicyTermEng") = LINEBREAK & "If you do not intend or are unable to pay the premium for the whole Policy term, you should not acquire the Policy as you may suffer a loss if you terminate the Policy early or cease paying premium early." & LINEBREAK
            dtFields.Rows(0)("Rmk_ExPolicyTermChi") = LINEBREAK & "假如您在投保期間不打算或無法支付全期保費，您便不應購買此保單。因為提早終止此保單或提前停止繳付保費，可能令您蒙受損失。" & LINEBREAK
        End If
        'setup Medical_plan Elite_term_plan Ex_policy_term -e

        'setup Sui_title Sui_content -s
        'If "Courtesy Call – Suitability mismatch".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) Then
        '    dtFields.Rows(0)("SuiTitleEng") = "Suitability Mismatch"
        '    dtFields.Rows(0)("SuiTitleChi") = "不合適配對"
        '    dtFields.Rows(0)("SuiContentEng") = "It is your intention and desire to proceed with the application despite the product objective(s) of the product(s) selected by you as per below table may not be suitable for your disclosed current needs as indicated in the Financial Needs and Investor Profile Analysis form."
        '    dtFields.Rows(0)("SuiContentChi") = "經管根據「財務需要及投資取向分析」，您所選擇的以下產品可能並不合適您的選購目標，您仍然選擇投保上述保單。"
        'Else
        '    dtFields.Rows(0)("SuiTitleEng") = "Suitability"
        '    dtFields.Rows(0)("SuiTitleChi") = "合適配對"
        '    dtFields.Rows(0)("SuiContentEng") = "Product objective(s) of the product(s) selected by you as per below table are suitable for you based on your disclosed current needs as indicated in the Financial Needs and Investor Profile Analysis form."
        '    dtFields.Rows(0)("SuiContentChi") = "根據「財務需要及投資取向分析」，您所選擇以下的產品既適合您亦可達至您的選購目標。"
        'End If
        'setup Sui_title Sui_content -s

        'setup NGB_content -s
        Dim ngbTitleEng As String = String.Empty
        Dim ngbTitleChi As String = String.Empty
        Dim ngbContentEng As String = String.Empty
        Dim ngbContentChi As String = String.Empty
        '(Event Category: Courtesy Call - Vulnerable customer and Event Detail: Uncertain or Unreachable or Unwilling) or Product type: Life(participating)/Universal life/Crisis(participating)
        'Life(participating) : CIWPT_IsLife
        'Universal life : CIWPT_IsUniversalLife
        'Crisis(participating) : CIWPT_IsCrisis
        Dim isLife As String = dtProductType.Rows(0)("CIWPT_IsLife").ToString().Trim()
        Dim isUniversalLife As String = dtProductType.Rows(0)("CIWPT_IsUniversalLife").ToString().Trim()
        Dim isCrisis As String = dtProductType.Rows(0)("CIWPT_IsCrisis").ToString().Trim()
        Dim productType As String = dtFields.Rows(0)("PlanNameEng")
        If ("Courtesy Call - Vulnerable customer".Equals(eventCategory, StringComparison.OrdinalIgnoreCase) And
            ("Uncertain".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Or
            "Unreachable".Equals(eventDetail, StringComparison.OrdinalIgnoreCase) Or
            "Unwilling".Equals(eventDetail, StringComparison.OrdinalIgnoreCase))) And
            (isLife = "Y" Or isUniversalLife = "Y" Or isCrisis = "Y") Then

            ngbTitleEng = LINEBREAK & "Non-Guaranteed Benefit"
            ngbContentEng = LINEBREAK & "The projected non-guaranteed benefits included in the illustration are based on the Company’s dividend / bonus (if any) scales determined under current assumed investment return and are not guaranteed. " &
                            "The actual amount payable may change at any time with the values being higher or lower than those illustrated. " &
                            "As another example, the possible potential impact of a change in the company’s current assumed investment return on the Total Surrender Benefit and the Total Death Benefit in illustration. Under some circumstances, the non-guaranteed benefits may be zero." & LINEBREAK

            ngbTitleChi = LINEBREAK & "非保證利益"
            ngbContentChi = LINEBREAK & "預計的非保證金額乃根據本公司現時假設投資回報而計算，該金額並非保證。實際獲發之金額或會比所示者較高或較低。" &
                            "建議書內說明因本公司現時假設的投資回報轉變而對退保價值及身故權益可能造成的影響。在某些情況下，非保證金額可能為零。" & LINEBREAK
        End If
        dtFields.Rows(0)("Rmk_NGBTitleEng") = ngbTitleEng
        dtFields.Rows(0)("Rmk_NGBTitleChi") = ngbTitleChi
        dtFields.Rows(0)("Rmk_NGBContentEng") = ngbContentEng
        dtFields.Rows(0)("Rmk_NGBContentChi") = ngbContentChi
        'setup NGB_content -e

        'setup Fee_charge -s
        Dim feeTitleEng As String = String.Empty
        Dim feeTitleChi As String = String.Empty
        Dim feeChargeEng As String = String.Empty
        Dim feeChargeChi As String = String.Empty
        'Product type:  Basic Plus / Basic Plus ll / Basic Plus Junior / Basic Plus Junior ll / i.Ulife Plus / Flexi-Growth / FlexiGrowth Single premium / Easy Plus / For Your Interest
        If productType.StartsWith("Basic Plus ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("i.Ulife Plus ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Flexi-Growth ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Easy Plus ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("For Your Interest ", StringComparison.OrdinalIgnoreCase) Then

            feeTitleEng = LINEBREAK & "Fee and Charge"
            feeChargeEng = LINEBREAK & "Fee and Charge are involved in the policy including Policy Fee, Management Charge, Cost of insurance and Withdrawal/Surrender Charge if applicable." & LINEBREAK

            feeTitleChi = LINEBREAK & "費用及收費"
            feeChargeChi = LINEBREAK & "有關費用及收費包括保單費用，行政管理費，人壽保險費及提款/退保手續費(如適用)在保單生效期內收取。" & LINEBREAK
        End If

        'Product type: Global fortune / Noble fortune / Regal fortune / Universal fortune / Glorious fortune product series
        If productType.StartsWith("Global fortune", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Noble fortune", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Regal fortune ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Universal fortune ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Glorious fortune ", StringComparison.OrdinalIgnoreCase) Then

            feeTitleEng = LINEBREAK & "Fee and Charge"
            feeChargeEng = LINEBREAK & "Fee and Charge are involved in the policy including Premium Charge, Administration Charge, Cost of insurance and Surrender Charge if applicable." & LINEBREAK

            feeTitleChi = LINEBREAK & "費用及收費"
            feeChargeChi = LINEBREAK & "有關費用及收費包括保費費用，行政費用，人壽保險費及退保手續費(如適用)在保單生效期內收取。" & LINEBREAK
        End If

        'Product type: i.Ulife Select / Flexi-Growth Premier product series
        If productType.StartsWith("i.Ulife Select ", StringComparison.OrdinalIgnoreCase) Or
            productType.StartsWith("Flexi-Growth Premier ", StringComparison.OrdinalIgnoreCase) Then

            feeTitleEng = LINEBREAK & "Fee and Charge"
            feeChargeEng = LINEBREAK & "Fee and Charge are involved in the policy including Policy Fee, Administration Charge and Surrender Charge if applicable." & LINEBREAK

            feeTitleChi = LINEBREAK & "費用及收費"
            feeChargeChi = LINEBREAK & "有關費用及收費包括保費費用，行政費用及提款退保手續費(如適用)在保單生效期內收取。" & LINEBREAK
        End If
        dtFields.Rows(0)("Rmk_FeeTitleEng") = feeTitleEng
        dtFields.Rows(0)("Rmk_FeeTitleChi") = feeTitleChi
        dtFields.Rows(0)("Rmk_FeeChargeEng") = feeChargeEng
        dtFields.Rows(0)("Rmk_FeeChargeChi") = feeChargeChi
        'setup Fee_charge -e

        'region -s
        Dim dtTbl As DataTable = New DataTable("RegionTbl")
        dtTbl.Columns.Add("Tbl_PlanNameEng", GetType(String))
        dtTbl.Columns.Add("Tbl_PlanNameChi", GetType(String))
        dtTbl.Columns.Add("Tbl_CurrencyEng", GetType(String))
        dtTbl.Columns.Add("Tbl_CurrencyChi", GetType(String))
        dtTbl.Columns.Add("Tbl_AmountEng", GetType(String))
        dtTbl.Columns.Add("Tbl_AmountChi", GetType(String))
        dtTbl.Columns.Add("Tbl_PolicyTermEng", GetType(String))
        dtTbl.Columns.Add("Tbl_PolicyTermChi", GetType(String))

        'Dim dtObj As DataTable = New DataTable("RegionObj")
        'dtObj.Columns.Add("Tsm_PlanNameEng", GetType(String))
        'dtObj.Columns.Add("Tsm_PlanNameChi", GetType(String))
        'dtObj.Columns.Add("Tsm_ObjectiveEng", GetType(String))
        'dtObj.Columns.Add("Tsm_ObjectiveChi", GetType(String))

        Dim dtRisk As DataTable = New DataTable("RegionRisk")
        dtRisk.Columns.Add("Tpr_PlanNameEng", GetType(String))
        dtRisk.Columns.Add("Tpr_PlanNameChi", GetType(String))
        dtRisk.Columns.Add("Tpr_RisksEng", GetType(String))
        dtRisk.Columns.Add("Tpr_RisksChi", GetType(String))

        'LAS logic s
        Dim dtCoverage As DataTable = GetCOSelWithCustNo(policyNo, g_McuComp)
        Dim paidToDate As Date = Date.ParseExact(dtContractDtl.Rows(0).Item("Paid_To_Date"), "MM/dd/yyyy", Globalization.CultureInfo.InvariantCulture)
        Dim strFreq As String = Format(Val(dtContractDtl.Rows(0).Item("Freq")), "00")
        Dim dtPremiumRoutine As DataTable = GetPremiumRoutine(policyNo, paidToDate, strFreq, g_McuComp)

        For Each dr As DataRow In dtCoverage.Rows
            dtPremiumRoutine.DefaultView.RowFilter = "LIFE = '" & dr("Life") & "' and COVERAGE = '" & dr("Cov") & "' and RIDER = '" & dr("Rider") & "' "

            If dtPremiumRoutine.DefaultView.Count > 0 Then

                Dim booster As Decimal = Val(dtPremiumRoutine.DefaultView(0).Item("ZBINSTPREM"))
                Dim inst As Decimal = Val(dtPremiumRoutine.DefaultView(0).Item("INSTPREM"))
                Dim modalPremium As Decimal
                If inst > 0 Then
                    modalPremium = inst
                Else
                    modalPremium = booster
                End If

                If booster <> 0.00 Then

                    Dim tblRow As DataRow = dtTbl.NewRow()

                    productId = dr("Cov_Code").ToString().Trim()
                    dtProductType = GetCIWPRProductTypeInfo(productId, strCIWMcuConn)
                    isMedical = dtProductType.Rows(0)("CIWPT_IsMedical").ToString().Trim()
                    Dim tblPlanNameEng As String = GetPlanName(productId, strCIWMcuConn)
                    Dim tblPlanNameChi As String = GetPlanNameChi(productId, strCIWMcuConn)

                    tblRow("Tbl_PlanNameEng") = tblPlanNameEng
                    tblRow("Tbl_PlanNameChi") = tblPlanNameChi
                    Dim policyCurrency As String = dtPolicyAccount.Rows(0)("PolicyCurrency").ToString().Trim()
                    tblRow("Tbl_CurrencyEng") = policyCurrency
                    tblRow("Tbl_CurrencyChi") = GetCurrencyChi(policyCurrency)
                    tblRow("Tbl_AmountEng") = GetMoneyFormat(modalPremium) & If(isMedical = "Y", " *", "")
                    tblRow("Tbl_AmountChi") = GetMoneyFormat(modalPremium) & If(isMedical = "Y", " *", "")

                    Dim policyTerm As String = dtPolicyAccount.Rows(0)("PolicyTermDate").ToString().Trim()
                    If Not String.IsNullOrWhiteSpace(policyTerm) Then
                        tblRow("Tbl_PolicyTermEng") = CDate(policyTerm).ToString("dd MMM yyyy") & " years / One-Off Premium"
                        tblRow("Tbl_PolicyTermChi") = CDate(policyTerm).ToString("yyyy年M月d日") & " 年 / 一次過繳付保費"
                    End If
                    dtTbl.Rows.Add(tblRow)

                    Dim dtPSCPS As DataTable = GetPostSalesCallProductSetting(productId, strCIWMcuConn)
                    Dim objectiveEng As String = String.Empty
                    Dim objectiveChi As String = String.Empty
                    Dim riskEng As String = String.Empty
                    Dim riskChi As String = String.Empty
                    If dtPSCPS.Rows.Count > 0 Then
                        'objectiveEng = dtPSCPS.Rows(0)("cswpsd_product_objective_eng").ToString().Trim()
                        'objectiveChi = dtPSCPS.Rows(0)("cswpsd_product_objective_chi").ToString().Trim()
                        riskEng = dtPSCPS.Rows(0)("cswpsd_risk_eng").ToString().Trim()
                        riskChi = dtPSCPS.Rows(0)("cswpsd_risk_chi").ToString().Trim()
                    End If

                    'Dim objRow As DataRow = dtObj.NewRow()
                    'objRow("Tsm_PlanNameEng") = tblPlanNameEng
                    'objRow("Tsm_PlanNameChi") = tblPlanNameChi
                    'objRow("Tsm_ObjectiveEng") = objectiveEng
                    'objRow("Tsm_ObjectiveChi") = objectiveChi
                    'dtObj.Rows.Add(objRow)

                    Dim riskRow As DataRow = dtRisk.NewRow()
                    riskRow("Tpr_PlanNameEng") = tblPlanNameEng
                    riskRow("Tpr_PlanNameChi") = tblPlanNameChi
                    riskRow("Tpr_RisksEng") = riskEng
                    riskRow("Tpr_RisksChi") = riskChi
                    dtRisk.Rows.Add(riskRow)
                End If

            End If
        Next
        'LAS logic e

        dsRegion.Tables.Add(dtTbl)
        'dsRegion.Tables.Add(dtObj)
        dsRegion.Tables.Add(dtRisk)
        'region -e

        Return dtFields
    End Function

    Private Function GetPSCFollowLetterFields() As DataTable
        Dim dtFields As DataTable = New DataTable
        dtFields.Columns.Add("PolicyNo", GetType(String))
        dtFields.Columns.Add("LetterDate", GetType(String))
        dtFields.Columns.Add("ContactAddress1", GetType(String))
        dtFields.Columns.Add("ContactAddress2", GetType(String))
        dtFields.Columns.Add("ContactAddress3", GetType(String))
        dtFields.Columns.Add("ContactAddress4", GetType(String))
        dtFields.Columns.Add("ContactFullName", GetType(String))
        dtFields.Columns.Add("AgentNameEng", GetType(String))
        dtFields.Columns.Add("AgentNameChi", GetType(String))
        dtFields.Columns.Add("AgentLocation", GetType(String))
        dtFields.Columns.Add("CoolingOffDateEng", GetType(String))
        dtFields.Columns.Add("CoolingOffDateChi", GetType(String))
        dtFields.Columns.Add("PlanNameEng", GetType(String))
        dtFields.Columns.Add("PlanNameChi", GetType(String))
        dtFields.Columns.Add("PayModeEng", GetType(String))
        dtFields.Columns.Add("PayModeChi", GetType(String))
        dtFields.Columns.Add("PSCContentEng", GetType(String))
        dtFields.Columns.Add("PSCContentChi", GetType(String))
        dtFields.Columns.Add("Rmk_MedicalPlanEng", GetType(String))
        dtFields.Columns.Add("Rmk_MedicalPlanChi", GetType(String))
        dtFields.Columns.Add("Rmk_EliteTermPlanEng", GetType(String))
        dtFields.Columns.Add("Rmk_EliteTermPlanChi", GetType(String))
        dtFields.Columns.Add("Rmk_ExPolicyTermEng", GetType(String))
        dtFields.Columns.Add("Rmk_ExPolicyTermChi", GetType(String))
        dtFields.Columns.Add("Rmk_NGBTitleEng", GetType(String))
        dtFields.Columns.Add("Rmk_NGBTitleChi", GetType(String))
        dtFields.Columns.Add("Rmk_NGBContentEng", GetType(String))
        dtFields.Columns.Add("Rmk_NGBContentChi", GetType(String))
        dtFields.Columns.Add("Rmk_FeeTitleEng", GetType(String))
        dtFields.Columns.Add("Rmk_FeeTitleChi", GetType(String))
        dtFields.Columns.Add("Rmk_FeeChargeEng", GetType(String))
        dtFields.Columns.Add("Rmk_FeeChargeChi", GetType(String))
        Return dtFields
    End Function

#End Region


End Class

