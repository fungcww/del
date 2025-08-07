Imports System.Threading.Tasks

''' <summary>
''' Async get data business logic for policy summary screen
''' </summary>
Public Class PolicySummaryAsyncBL
    Inherits AsyncBaseBL

    Public Const BUSI_TYPE_POLICY_HEADER_STR As String = "PolicyHeader"
    Public Const BUSI_TYPE_CLIENT_ROLE_STR As String = "ClientRole"

    Public Sub New(objDBHeader As Utility.Utility.ComHeader, objMQHeader As Utility.Utility.MQHeader)
        MyBase.New(objDBHeader, objMQHeader)
    End Sub

    Public Function GetPolicyHeaderAsync(policyNo As String, callback As Action(Of Task(Of DataSet))) As Task
        Return GetDataSetAsync(BUSI_TYPE_POLICY_HEADER_STR, "QueryPolicyHeader", New Dictionary(Of String, String) From {{"PolicyNo", policyNo.Trim}}).
            ContinueWith(callback)
    End Function

    Public Function GetClientRoleAsync(policyNo As String, callback As Action(Of Task(Of DataSet))) As Task
        Return GetDataSetAsync(BUSI_TYPE_CLIENT_ROLE_STR, "QueryClientRole", New Dictionary(Of String, String) From {{"PolicyNo", policyNo.Trim}}).
            ContinueWith(callback)
    End Function

    Public Function GetPolicyClientSysInfoAsync(callback As Action(Of Task(Of DataSet))) As Task
        Return GetDataSetAsync(BUSI_TYPE_CLIENT_ROLE_STR, "QueryPolicyClientSysInfo", New Dictionary(Of String, String)).
            ContinueWith(callback)
    End Function

    Public Function GetWholeDataDictionaryAsync(policyNo As String, callback As Action(Of Task, Dictionary(Of String, DataSet))) As Task
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim funcStartTime As Date = Now

        Dim taskList As New List(Of Task)

        ' get policy header and related data
        SysEventLog.WritePerLog(gsUser, "CS2005.PolicySummaryAsyncBL", "GetWholeDataDictionaryAsync", "GetPolicyHeaderAsync.Start", funcStartTime, Now, policyNo, "", "")
        Dim dsPolicyData As New DataSet
        taskList.Add(GetPolicyHeaderAsync(policyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySummaryAsyncBL", "GetWholeDataDictionaryAsync", "GetPolicyHeaderAsync.End", funcStartTime, Now, policyNo, "", "")
                dsPolicyData = If(t.IsFaulted, dsPolicyData, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' get client role
        SysEventLog.WritePerLog(gsUser, "CS2005.PolicySummaryAsyncBL", "GetWholeDataDictionaryAsync", "GetClientRoleAsync.Start", funcStartTime, Now, policyNo, "", "")
        Dim dsClientRole As New DataSet
        taskList.Add(GetClientRoleAsync(policyNo,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySummaryAsyncBL", "GetWholeDataDictionaryAsync", "GetClientRoleAsync.End", funcStartTime, Now, policyNo, "", "")
                dsClientRole = If(t.IsFaulted, dsClientRole, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' get policy client system info data
        SysEventLog.WritePerLog(gsUser, "CS2005.PolicySummaryAsyncBL", "GetWholeDataDictionaryAsync", "GetPolicyClientSysInfoAsync.Start", funcStartTime, Now, policyNo, "", "")
        Dim dsPolicyClientSysInfo As New DataSet
        taskList.Add(GetPolicyClientSysInfoAsync(
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "CS2005.PolicySummaryAsyncBL", "GetWholeDataDictionaryAsync", "GetPolicyClientSysInfoAsync.End", funcStartTime, Now, policyNo, "", "")
                If Not t.IsFaulted AndAlso dsComponentSysTable IsNot Nothing AndAlso dsComponentSysTable.Tables.Count > 0 Then
                    dsPolicyClientSysInfo = t.Result
                    ' only some special system info tables were obtained, add other NB system table
                    dsPolicyClientSysInfo.Tables.Add(dsComponentSysTable.Tables("SysMortCode").Copy)
                    dsPolicyClientSysInfo.Tables.Add(dsComponentSysTable.Tables("SysSpecTermCode").Copy)
                    dsPolicyClientSysInfo.Tables.Add(dsComponentSysTable.Tables("SysUsualResidence").Copy)
                    dsPolicyClientSysInfo.Tables.Add(dsComponentSysTable.Tables("dtMortMapping").Copy)
                    dsPolicyClientSysInfo.Tables.Add(dsComponentSysTable.Tables("SysBeneRelaCode").Copy)
                    dsPolicyClientSysInfo.Tables.Add(dsComponentSysTable.Tables("SysSoftCode").Copy)
                Else
                    If t.IsFaulted Then
                        Throw t.Exception.InnerException    ' faulted result need to be thrown
                    Else
                        Throw New Exception("Cannot get NB system info config DataSet.")
                    End If
                End If
            End Sub
        ))

        ' when all data get done, callback
        Return WhenAll(taskList.ToArray()).ContinueWith(
            Sub(t)
                callback?.Invoke(t, New Dictionary(Of String, DataSet) From {
                    {"dsPolicyData", dsPolicyData},
                    {"dsClientRole", dsClientRole},
                    {"dsPolicyClientSysInfo", dsPolicyClientSysInfo}
                })
            End Sub
        )
    End Function

End Class
