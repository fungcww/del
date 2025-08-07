Imports System.Threading
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.tcp
Imports System.Runtime.Remoting.Channels.http

Public Class clsMQWorker

    Public Event Finish(ByVal strFunc As String, ByVal lngErrNo As Long, ByVal strErrMsg As String, ByVal dt As DataTable())

#If STRESS <> 0 Then
    Public Sub QueuedExec(ByVal strPolicy As String, ByVal datEnqFrom As Date, ByVal datHstFrom As Date, ByVal strClientList As String, ByVal writer As IO.StreamWriter)
#Else
    Public Sub QueuedExec(ByVal strPolicy As String, ByVal datEnqFrom As Date, ByVal datHstFrom As Date, ByVal strClientList As String)
#End If
        'Dim blnStop As Boolean = False
        '#If RPC = "DCOM" Then
        '        dim objCS1 as comCS2005.cs2005 = New comCS2005.CS2005
        '#Else
        '        Dim channel As New HttpChannel
        '        ChannelServices.RegisterChannel(channel)

        '#If RPC = "HTTP" Then
        '        Dim remoteobj As Object = Activator.GetObject(GetType(comCS2005.CS2005), "http://10.10.18.237/CS2005Service/comCS2005.CS2005.soap")
        '#ElseIf RPC = "TCP" Then
        '        Dim remoteobj As Object = Activator.GetObject(GetType(comCS2005.CS2005), "tcp://10.10.18.237:13101/comCS2005.CS2005.soap")
        '#End If

        '        Dim objCS1 As comCS2005.CS2005 = CType(remoteobj, comCS2005.CS2005)
        '#End If

        Dim dt(1) As DataTable
        Dim lngErrNo As Long
        Dim strErrMsg As String

        Try
            'lngErrNo = 0
            'strErrMsg = ""
            'dt = objCS.GetPolicyAddress(strPolicy, lngErrNo, strErrMsg)
            'RaiseEvent Finish(cPOADDR, lngErrNo, strErrMsg, dt)

            'AC - Change to use configuration setting - start
            'If UAT <> 0 Then
            '    'objCS.Env = giEnv
            'End If
            'AC - Change to use configuration setting - end


            lngErrNo = 0
            strErrMsg = ""
            dt(0) = objCS.GetORDUNA(strClientList, lngErrNo, strErrMsg)
            RaiseEvent Finish(cORDUNA, lngErrNo, strErrMsg, dt)



            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            Dim ts, tf As TimeSpan
            ts = New TimeSpan(Now.Ticks)
#End If
            dt = objCS.GetCoverage(strPolicy, lngErrNo, strErrMsg)
#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("POLIC-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cCOINFO, lngErrNo, strErrMsg, dt)



            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If
            dt(0) = objCS.GetPaymentHistory(strPolicy, datEnqFrom, lngErrNo, strErrMsg)
#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("PAYH-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cPAYH, lngErrNo, strErrMsg, dt)



            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If
            dt(0) = objCS.GetCouponHistory(strPolicy, datHstFrom, lngErrNo, strErrMsg)
#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("COUH-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cCOUHST, lngErrNo, strErrMsg, dt)



            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If
            dt(0) = objCS.GetAPLHistory(strPolicy, datHstFrom, lngErrNo, strErrMsg)
#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("APLH-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cAPLH, lngErrNo, strErrMsg, dt)




            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If
            dt(0) = objCS.GetCCDR(strPolicy, lngErrNo, strErrMsg)
#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("CCDR-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cCCDR, lngErrNo, strErrMsg, dt)



            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If
            dt(0) = objCS.GetDDA(strPolicy, lngErrNo, strErrMsg)

            ' CUP DDA - Start
            Dim dtDDAR As DataTable = dt(0)
            Dim drDDAFORM As DataRow()

            dtDDAR.Columns.Add("DDACollectPath", GetType(String))
            dtDDAR.Columns.Add("DDACUPBankCode", GetType(String))
            dtDDAR.Columns.Add("DDACUPAccNum", GetType(String))
            dtDDAR.Columns.Add("DDACUPBankName", GetType(String))
            dtDDAR.Columns.Add("DDACUPProvince", GetType(String))
            dtDDAR.Columns.Add("DDACUPCity", GetType(String))
            dtDDAR.Columns.Add("DDACUPBranch", GetType(String))

            Dim dtDDAFORM As DataTable = GetDDAFORM(strPolicy)

            For Each drDDAR As DataRow In dtDDAR.Rows
                drDDAFORM = dtDDAFORM.Select("DSEQ=" & drDDAR("DDASeqNo"))
                If drDDAFORM.Length <> 1 Then
                    Throw New Exception("Fail to map CUP DDAFORM information.")
                End If

                drDDAR("DDACollectPath") = drDDAFORM(0)("DCCLPT")
                drDDAR("DDACUPBankCode") = drDDAFORM(0)("DCBKCD")
                drDDAR("DDACUPAccNum") = drDDAFORM(0)("DCACNO")
                drDDAR("DDACUPBankName") = ""
                drDDAR("DDACUPProvince") = drDDAFORM(0)("DCPRV")
                drDDAR("DDACUPCity") = drDDAFORM(0)("DCCTY")
                drDDAR("DDACUPBranch") = drDDAFORM(0)("DCRBNK")
            Next

            ' CUP DDA - End

#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("DDAR-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cDDA, lngErrNo, strErrMsg, dt)


            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If

            'AC - Change to use configuration setting - start
            'If UAT <> 0 Then
            '    dt(0) = Nothing
            '    'dt(0) = objCS.GetDISC(strPolicy, "", lngErrNo, strErrMsg)
            '    'dt(0) = objCS.GetBANK("000", "000", lngErrNo, strErrMsg)
            '    'dt(0) = objCS.GetBANK("003", "", lngErrNo, strErrMsg)
            '    'dt(0) = objCS.GetBANK("003", "251", lngErrNo, strErrMsg)
            '    'dt(0) = objCS.GetPRTS("AL", "1", "N", "P", "M", "01", "", 10, 0, lngErrNo, strErrMsg)
            '    'dt(0) = objCS.GetUVAL("AL", "1", "N", "P", "M", "", "", 10, "CSV", lngErrNo, strErrMsg)
            '    'dt(0) = objCS.GetIRTS("APL", "1", #1/1/2000#, 0, 0, 0, lngErrNo, strErrMsg)
            '    'dt = objCS.GetUTRH("5010198909", #1/1/2000#, lngErrNo, strErrMsg)
            'Else
            '    dt(0) = objCS.GetDCAR(strPolicy, lngErrNo, strErrMsg)
            'End If
            dt(0) = objCS.GetDCAR(strPolicy, lngErrNo, strErrMsg)
            'AC - Change to use configuration setting - start

#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("DCAR-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cDCAR, lngErrNo, strErrMsg, dt)



            lngErrNo = 0
            strErrMsg = ""
#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If
            dt(0) = objCS.GetClientHistory(strPolicy, "", "", datHstFrom, "", "", "", "", "B", "", #1/1/1900#, "", "", "", "", "", lngErrNo, strErrMsg)
#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("HICL-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cHICL, lngErrNo, strErrMsg, dt)



#If STRESS <> 0 Then
            ts = New TimeSpan(Now.Ticks)
#End If
            lngErrNo = 0
            strErrMsg = ""
            '' datEnqFrom = Today!
            dt(0) = objCS.GetTrnHistory(strPolicy, datHstFrom, datEnqFrom, lngErrNo, strErrMsg)
#If STRESS <> 0 Then
            tf = New TimeSpan(Now.Ticks)
            tf = tf.Subtract(ts)
            Call WriteLog("TRHN-1: " & tf.Seconds.ToString() & "." & tf.Milliseconds.ToString(), writer)
#End If
            RaiseEvent Finish(cTRNH, lngErrNo, strErrMsg, dt)
        Catch ex As Exception
            RaiseEvent Finish("MAIN", -1, ex.ToString, Nothing)
        End Try

    End Sub

    Private Function GetDDAFORM(ByVal strPolicyNo As String) As DataTable
        Dim dt As DataTable
        Dim strErr As String = Nothing

        Using wsCCR As New CCRWS.Service()
            wsCCR.DBSOAPHeaderValue = GetCCRWSDBHeader()
            wsCCR.MQSOAPHeaderValue = GetCCRWSMQHeader()
            wsCCR.Url = Utility.Utility.GetWebServiceURL("CCRWS", gobjDBHeader, gobjMQQueHeader)
            wsCCR.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsCCR.Timeout = 10000000


            If Not wsCCR.GetDDAFORM(gsUser, g_Comp.Trim() & g_Env.Trim(), strPolicyNo, dt, strErr) Then
                Throw New Exception("Failed to get DDAForm record." & vbCrLf & strErr)
            End If
        End Using

        Return dt
    End Function

End Class
