Imports System.Linq

Public Class frmTest

#Region "Event Function"

    Private Sub frmTest_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        gUAT = True
        gLoginEnvStr = tbEnvStr.Text.Trim
        gsUser = tbUserStr.Text.Trim
        EnvSetup(gLoginEnvStr)

        'LifeAsia global setting
        gobjDBHeader.UserID = gsUser
        gobjDBHeader.EnvironmentUse = g_Env
        gobjDBHeader.ProjectAlias = "LAS" '"LAS"
        gobjDBHeader.CompanyID = g_Comp '"ING"
        gobjDBHeader.UserType = "LASUPDATE" '"LASUPDATE"

        gobjMQQueHeader.QueueManager = g_Qman
        gobjMQQueHeader.RemoteQueue = g_WinRemoteQ
        gobjMQQueHeader.ReplyToQueue = g_LAReplyQ
        gobjMQQueHeader.LocalQueue = g_WinLocalQ
        gobjMQQueHeader.UserID = gsUser
        gobjMQQueHeader.CompanyID = g_Comp
        gobjMQQueHeader.Timeout = 90000000 'My.Settings.Timeout
        ' for logging to csw_mq_log when MQ error
        gobjMQQueHeader.EnvironmentUse = g_Env
        gobjMQQueHeader.ProjectAlias = "LAS" '"LAS"
        gobjMQQueHeader.UserType = "LASUPDATE" '"LASUPDATE"
        gobjMQQueHeader.ConnectionAlias = g_Comp & "CIW" & g_Env
    End Sub

    Private Sub BtnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        TestAPICall()

        'TestUserRight(3)
    End Sub

#End Region

    Private Sub TestAPICall()
        Dim ds As DataSet = Nothing
        Dim interval As Long

        Try
            Dim bl As New CoverageDetailsAsyncBL(gobjDBHeader, gobjMQQueHeader)
            Dim watcher As New Stopwatch()

            watcher.Start()
            bl.GetCoverageDataAsync("11022663", "2023-03-14", "12",
                Sub(t)
                    If Not t.IsFaulted Then
                        ds = t.Result
                    End If
                End Sub
            ).Wait()
            watcher.Stop()

            interval = watcher.ElapsedMilliseconds
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        If ds IsNot Nothing Then MsgBox($"Count: {ds.Tables.Count}, Interval(ms): {interval}")
    End Sub

    Private Sub TestUserRight(targetPoint As Integer)
        Dim targetGroupDict As New Dictionary(Of String, String)

        Try
            Dim dtGroup As DataTable = APIServiceBL.CallAPIBusi(getCompanyCode(), "CHECK_USER_RIGHT_2", New Dictionary(Of String, String)() From {{"condition", " "}}).Tables(0)

            For Each row As DataRow In dtGroup.Rows
                Dim rightStr As String = ConvertRightString(row("upsugt_usr_right"))

                If Mid(rightStr, targetPoint, 1) = "1" Then
                    targetGroupDict.Item(row("upsugt_usr_grp_no").ToString.Trim) = row("upsugt_usr_grp_name").ToString.Trim
                End If
            Next

            MsgBox(targetGroupDict.Count)
            Debug.WriteLine(String.Join(",", targetGroupDict.Values.Select(Function(str) $"'{str}'")))
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function ConvertRightString(accessRight As Decimal) As String
        Dim rightStr As String = Nothing
        Dim ary As Char() = DECtoBIN(0).ToCharArray
        Dim tempAry As Char() = StrReverse(DECtoBIN(accessRight)).ToCharArray()
        ary = OrAuthority(ary, tempAry)
        For i As Integer = 0 To ary.Length - 1
            rightStr &= ary(i)
        Next
        Return rightStr
    End Function

    Private Function OrAuthority(ByRef origAry As Array, ByRef orAry As Array) As Array
        Dim returnAry As Integer()
        ReDim returnAry(origAry.Length - 1) ' 93 length

        For i As Integer = 0 To origAry.Length - 1
            Dim a = Integer.Parse(origAry(i))
            Dim b = Integer.Parse(orAry(i))
            returnAry(i) = a Or b
        Next

        'convert integer array to char array
        Dim temp As String = ""

        For i As Integer = 0 To returnAry.Length - 1
            temp = temp & returnAry(i)
        Next

        origAry = temp.ToCharArray()

        Return origAry
    End Function

End Class
