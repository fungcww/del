Imports System.Text
Imports System.Threading.Tasks
Imports CRS_Util.Model.Claims

''' <summary>
''' Business logic for claims
''' </summary>
''' <remarks>
''' <br>20250217 Chryasn Cheng, CRS performance 2 - PaymentHistory and MinorClaimsHistory</br>
''' </remarks>
Public Class ClaimsBL

    Public Const BUSINESS_TYPE As String = "Claims/search"

    Private ReadOnly SysEventLog As New SysEventLog.clsEventLog

    Protected objDBHeader As Utility.Utility.ComHeader
    Protected objMQHeader As Utility.Utility.MQHeader

    ''' <summary>
    ''' Company ID Code
    ''' </summary>
    Public ReadOnly Property CompanyID As String
        Get
            Return objDBHeader.CompanyID.Trim.ToUpper
        End Get
    End Property

    Public Sub New(objDBHeader As Utility.Utility.ComHeader, objMQHeader As Utility.Utility.MQHeader)
        Me.objDBHeader = objDBHeader
        Me.objMQHeader = objMQHeader
    End Sub

    ''' <summary>
    ''' Get FPS status for claims
    ''' </summary>
    Public Function GetFPSStatus(strClaimsNum As String, strClaimsOccur As String, ByRef dtResult As DataTable, ByRef strErr As String) As Boolean
        Try
            dtResult = APIServiceBL.QueryFirstTableWithSearchAPI(Of MinorClaimFPSStatus)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimFPSStatus",
                New Dictionary(Of String, String) From {
                    {"claimNo", strClaimsNum},
                    {"claimOccur", strClaimsOccur}
            })

            Return True
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, "CRS", objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            strErr = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Async get all minor claims related data
    ''' </summary>
    ''' <param name="policyNo">Policy No</param>
    ''' <param name="mapPolicyNo">The mapping policy No</param>
    ''' <param name="callback">An action to run when get all data completes.</param>
    ''' <returns>The task object for <paramref name="callback"/></returns>
    Public Function GetAllMinorClaimsDataAsync(policyNo As String, mapPolicyNo As String, callback As Action(Of Task(Of DataSet))) As Task
        Dim funcStartTime As Date = Now

        Dim taskList As New List(Of Task)

        ' mcsvw_claim_header_details
        Dim dtMaster As New DataTable("mcsvw_claim_header_details")
        taskList.Add(GetMinorClaimMasterAsync(policyNo, mapPolicyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimsBL", "GetAllMinorClaimsDataAsync", "GetMinorClaimMasterAsync", funcStartTime, Now, policyNo,
                    (Now - funcStartTime).TotalSeconds, Math.Ceiling((Now - funcStartTime).TotalSeconds))
                dtMaster = If(t.IsFaulted, dtMaster, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' mcsvw_cheque_info
        Dim dtChequeInfo As New DataTable("mcsvw_cheque_info")
        taskList.Add(GetMinorClaimChequeInfoAsync(policyNo, mapPolicyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimsBL", "GetAllMinorClaimsDataAsync", "GetMinorClaimChequeInfoAsync", funcStartTime, Now, policyNo,
                    (Now - funcStartTime).TotalSeconds, Math.Ceiling((Now - funcStartTime).TotalSeconds))
                dtChequeInfo = If(t.IsFaulted, dtChequeInfo, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' mcsvw_claim_bene_hist
        Dim dtBenefit As New DataTable("mcsvw_claim_bene_hist")
        taskList.Add(GetMinorClaimBenefitAsync(policyNo, mapPolicyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimsBL", "GetAllMinorClaimsDataAsync", "GetMinorClaimBenefitAsync", funcStartTime, Now, policyNo,
                    (Now - funcStartTime).TotalSeconds, Math.Ceiling((Now - funcStartTime).TotalSeconds))
                dtBenefit = If(t.IsFaulted, dtBenefit, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' mcsvw_claim_pending
        Dim dtPending As New DataTable("mcsvw_claim_pending")
        taskList.Add(GetMinorClaimPendingAsync(policyNo, mapPolicyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimsBL", "GetAllMinorClaimsDataAsync", "GetMinorClaimPendingAsync", funcStartTime, Now, policyNo,
                    (Now - funcStartTime).TotalSeconds, Math.Ceiling((Now - funcStartTime).TotalSeconds))
                dtPending = If(t.IsFaulted, dtPending, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' TheOneClaimBalance
        Dim dtBalance As New DataTable("TheOneClaimBalance")
        taskList.Add(GetMinorClaimBalanceAsync(policyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimsBL", "GetAllMinorClaimsDataAsync", "GetMinorClaimBalanceAsync", funcStartTime, Now, policyNo,
                    (Now - funcStartTime).TotalSeconds, Math.Ceiling((Now - funcStartTime).TotalSeconds))
                dtBalance = If(t.IsFaulted, dtBalance, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' Total_Claim
        Dim dtTotalClaim As New DataTable("Total_Claim")
        taskList.Add(GetMinorClaimTotalClaimAsync(policyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimsBL", "GetAllMinorClaimsDataAsync", "GetMinorClaimTotalClaimAsync", funcStartTime, Now, policyNo,
                    (Now - funcStartTime).TotalSeconds, Math.Ceiling((Now - funcStartTime).TotalSeconds))
                dtTotalClaim = If(t.IsFaulted, dtTotalClaim, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' when all data get done, callback
        Return Task.Factory.StartNew(
            Function()
                Try
                    ' wait all get data tasks
                    Task.WaitAll(taskList.ToArray())

                    ' integrate all data
                    Dim dsClaim As New DataSet()
                    dsClaim.Tables.Add(dtMaster)
                    dsClaim.Tables.Add(dtChequeInfo)
                    dsClaim.Tables.Add(dtBenefit)
                    dsClaim.Tables.Add(dtPending)
                    dsClaim.Tables.Add(dtBalance)
                    dsClaim.Tables.Add(dtTotalClaim)

                    Return dsClaim
                Catch aggEx As AggregateException
                    ' re-throw the valid exception (if any)
                    Dim sb As New StringBuilder()
                    For Each ex As Exception In aggEx.InnerExceptions
                        sb.AppendLine(ex.Message)
                    Next

                    Throw New Exception(sb.ToString())
                End Try
            End Function
        ).ContinueWith(callback)
    End Function

    Private Function GetMinorClaimMasterAsync(policyNo As String, mapPolicyNo As String, callback As Action(Of Task(Of DataTable))) As Task
        If CompanyID = "MCU" OrElse CompanyID = "LAC" OrElse CompanyID = "LAH" Then
            Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimMasterMcu)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimMaster_MCU",
                New Dictionary(Of String, String) From {
                    {"strInPolicy1", policyNo},
                    {"strInPolicy2", mapPolicyNo}
            }).ContinueWith(callback)
        Else
            Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimMaster)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimMaster",
                New Dictionary(Of String, String) From {
                    {"strInPolicy1", policyNo},
                    {"strInPolicy2", mapPolicyNo}
            }).ContinueWith(callback)
        End If
    End Function

    Private Function GetMinorClaimChequeInfoAsync(policyNo As String, mapPolicyNo As String, callback As Action(Of Task(Of DataTable))) As Task
        Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimChequeInfo)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimChequeInfo",
            New Dictionary(Of String, String) From {
                {"chequeInfoTable", If(CompanyID = "LAC" OrElse CompanyID = "LAH", "mcsvw_cheque_info_asur", "mcsvw_cheque_info")},
                {"strInPolicy1", policyNo},
                {"strInPolicy2", mapPolicyNo}
        }).ContinueWith(callback)
    End Function

    Private Function GetMinorClaimBenefitAsync(policyNo As String, mapPolicyNo As String, callback As Action(Of Task(Of DataTable))) As Task
        If CompanyID = "LAC" OrElse CompanyID = "LAH" Then
            Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimBenefitAsur)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimBenefit_Asur",
                New Dictionary(Of String, String) From {
                    {"strInPolicy1", policyNo},
                    {"strInPolicy2", mapPolicyNo}
            }).ContinueWith(callback)
        Else
            Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimBenefit)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimBenefit",
                New Dictionary(Of String, String) From {
                    {"strInPolicy1", policyNo},
                    {"strInPolicy2", mapPolicyNo}
            }).ContinueWith(callback)
        End If
    End Function

    Private Function GetMinorClaimPendingAsync(policyNo As String, mapPolicyNo As String, callback As Action(Of Task(Of DataTable))) As Task
        If CompanyID = "MCU" OrElse CompanyID = "LAC" OrElse CompanyID = "LAH" Then
            Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimPendingAsur)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimPending_Asur",
                New Dictionary(Of String, String) From {
                    {"strInPolicy1", policyNo},
                    {"strInPolicy2", mapPolicyNo}
            }).ContinueWith(callback)
        Else
            Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimPending)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimPending",
                New Dictionary(Of String, String) From {
                    {"strInPolicy1", policyNo},
                    {"strInPolicy2", mapPolicyNo}
            }).ContinueWith(callback)
        End If
    End Function

    Private Function GetMinorClaimBalanceAsync(policyNo As String, callback As Action(Of Task(Of DataTable))) As Task
        Return APIServiceBL.QueryFirstTableWithSearchAPIAsync(Of MinorClaimBalance)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimBalance",
            New Dictionary(Of String, String) From {
                {"strInPolicy1", policyNo}
        }).ContinueWith(callback)
    End Function

    Private Function GetMinorClaimTotalClaimAsync(policyNo As String, callback As Action(Of Task(Of DataTable))) As Task
        Dim funcStartTime As Date = Now

        ' get savingBal amount first
        Return Task.Factory.StartNew(Function() GetMediSavingAmount(policyNo, False)).ContinueWith(
            Function(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.ClaimsBL", "GetAllMinorClaimsDataAsync", "GetMinorClaimTotalClaimAsync.GetMediSavingAmount", funcStartTime, Now, policyNo,
                    (Now - funcStartTime).TotalSeconds, Math.Ceiling((Now - funcStartTime).TotalSeconds))

                If Not t.IsFaulted Then
                    ' after the async pre-action is complete, get MinorClaimTotalClaim data in sync
                    Return APIServiceBL.QueryFirstTableWithSearchAPI(Of MinorClaimTotalClaim)(getCompanyCode(CompanyID), BUSINESS_TYPE, "MinorClaimTotalClaim",
                        New Dictionary(Of String, String) From {
                            {"strInPolicy1", policyNo},
                            {"savingBal", t.Result},
                            {"companyID", CompanyID}
                    })
                Else
                    Throw t.Exception.InnerException    ' faulted result need to be thrown
                End If
            End Function
        ).ContinueWith(callback)
    End Function

End Class
