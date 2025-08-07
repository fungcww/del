Imports System.Threading.Tasks

''' <summary>
''' Async get data business logic for coverage details screen
''' </summary>
Public Class CoverageDetailsAsyncBL
    Inherits AsyncBaseBL

    Public Const BUSI_TYPE_COVERAGE_DETAILS_STR As String = "CoverageDetails"

    Public Sub New(objDBHeader As Utility.Utility.ComHeader, objMQHeader As Utility.Utility.MQHeader)
        MyBase.New(objDBHeader, objMQHeader)
    End Sub

    Public Function GetCoverageDataAsync(policyNo As String, paidToDate As Date, strFreq As String, callback As Action(Of Task(Of DataSet))) As Task
        Return GetDataSetAsync(BUSI_TYPE_COVERAGE_DETAILS_STR, "QueryCoverageDetails",
            New Dictionary(Of String, String) From {
                {"PolicyNo", policyNo.Trim},
                {"PaidToDate", If(paidToDate = Date.MinValue, String.Empty, paidToDate.ToShortDateString)},
                {"StrFreq", strFreq}
            }).ContinueWith(callback)
    End Function

    Public Function GetComponentDataAsync(callback As Action(Of Task(Of DataSet))) As Task
        Return GetDataSetAsync(BUSI_TYPE_COVERAGE_DETAILS_STR, "QueryComponentData", New Dictionary(Of String, String)).
                ContinueWith(callback)
    End Function

    Public Function GetWholeDataDictionaryAsync(policyNo As String, dsPolicyHeader As DataSet, callback As Action(Of Task, Dictionary(Of String, DataSet))) As Task
        Dim SysEventLog As New SysEventLog.clsEventLog
        Dim funStartTime As Date = Now

        ' get paidToDate and strFreq from policyHeaderData first
        Dim paidToDate As Date = Nothing
        Dim strFreq As String = Nothing
        If dsPolicyHeader IsNot Nothing AndAlso dsPolicyHeader.Tables.Count > 0 Then
            paidToDate = CDate(dsPolicyHeader.Tables(0).Rows(0).Item("paid_to_date"))
            strFreq = Format(Val(dsPolicyHeader.Tables(0).Rows(0).Item("Freq")), "00")
        End If

        Dim taskList As New List(Of Task)

        ' get coverage data
        Dim dsCoverageData As New DataSet
        SysEventLog.WritePerLog(gsUser, "cs2005.CoverageDetailsAsyncBL", "GetAllDataAsync", "GetCoverageDataAsync.Start", funStartTime, Now, policyNo, "", "")
        taskList.Add(GetCoverageDataAsync(policyNo, paidToDate, strFreq,
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "cs2005.CoverageDetailsAsyncBL", "GetAllDataAsync", "GetCoverageDataAsync.End", funStartTime, Now, policyNo, "", "")
                dsCoverageData = If(t.IsFaulted, dsCoverageData, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' get various component related data
        Dim dsVariousData As New DataSet
        funStartTime = Now
        SysEventLog.WritePerLog(gsUser, "cs2005.CoverageDetailsAsyncBL", "GetAllDataAsync", "GetComponentDataAsync.Start", funStartTime, Now, policyNo, "", "")
        taskList.Add(GetComponentDataAsync(
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "cs2005.CoverageDetailsAsyncBL", "GetAllDataAsync", "GetComponentDataAsync.End", funStartTime, Now, policyNo, "", "")
                dsVariousData = If(t.IsFaulted, dsVariousData, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' get UW system info data
        Dim dsUWSysInfo As New DataSet
        Dim sysInfoBL As New SystemInfoAsyncBL(objDBHeader, objMQHeader)
        funStartTime = Now
        SysEventLog.WritePerLog(gsUser, "cs2005.CoverageDetailsAsyncBL", "GetAllDataAsync", "SystemInfoAsyncBL.GetUWSysInfoAsync.Start", funStartTime, Now, policyNo, "", "")
        taskList.Add(sysInfoBL.GetUWSysInfoAsync(
            Sub(t)
                SysEventLog.WritePerLog(gsUser, "cs2005.CoverageDetailsAsyncBL", "GetAllDataAsync", "SystemInfoAsyncBL.GetUWSysInfoAsync.End", funStartTime, Now, policyNo, "", "")
                dsUWSysInfo = If(t.IsFaulted, dsUWSysInfo, t.Result)
                If t.IsFaulted Then Throw t.Exception.InnerException    ' faulted result need to be thrown
            End Sub
        ))

        ' when all data get done, callback
        Return WhenAll(taskList.ToArray()).ContinueWith(
            Sub(t)
                callback?.Invoke(t, New Dictionary(Of String, DataSet) From {
                    {"dsCoverageData", dsCoverageData},
                    {"dsVariousData", dsVariousData},
                    {"dsUWSysInfo", dsUWSysInfo}
                })
            End Sub
        )
    End Function

End Class
