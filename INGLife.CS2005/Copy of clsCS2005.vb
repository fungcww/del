Imports System.EnterpriseServices
Imports System.Data.SqlClient
Imports Interop
Imports System.Reflection

<Assembly: ApplicationName("CS2005")> 
<Assembly: ApplicationActivation(ActivationOption.Server)> 
<Assembly: ApplicationAccessControl(False)> 

<TransactionAttribute(TransactionOption.RequiresNew)> _
Public Class CS2005
    Inherits ServicedComponent

    Dim itfMQ As IMQConnect
    Dim objMQ As New MQConnect
    Dim objUtl As New clsUtility
    Private strErr As String, lngErrNo As Long
    Dim lngSize As Long

    Public Property BufferSize()
        Get
            BufferSize = lngSize
        End Get
        Set(ByVal Value)
            lngSize = Value
        End Set
    End Property

    Public Function SearchPolicy(ByVal strCriteria As String, ByRef strErrNo As Long, ByVal strErrMsg As String) As DataTable

        Dim strSQL As String

        Dim sqlConnect As SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim sqlDT As New DataTable
        Dim sqlDR As DataRow

        If strCriteria = "" Then
            strErrNo = -1
            strErrMsg = "Invalid Criteria"
            Exit Function
        End If

        strSQL = "select Description, policyaccountid, PolicyEffDate, PolicyCurrency " & _
            " from policyaccount a, product p " & _
            " where a.productid = p.productid " & _
            " and " & strCriteria

        Try
            sqlConnect = New SqlConnection(objUtl.ConnStr("CSW", "CS2005", "CIW"))
            sqlDA = New SqlDataAdapter(strSQL, sqlConnect)
            sqlDA.MissingSchemaAction = MissingSchemaAction.Add
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.FillSchema(sqlDT, SchemaType.Source)
            sqlDA.Fill(sqlDT)

        Catch ex As Exception
            strErrNo = -1
            strErrMsg = ex.ToString
            Return Nothing

        Catch sqlex As SqlException
            strErrNo = sqlex.Number
            strErrMsg = sqlex.ToString
            Return Nothing

        Finally
            sqlConnect.Close()
        End Try

    End Function

    Public Function GetPaymentHistory(ByVal strPolicy As String, ByVal datFrom As Date, _
            ByRef strErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim strMsg As String
        Dim dt As DataTable
        Dim stMQ As New PAYH
        stMQ.strFunc_5 = "PAYH"
        stMQ.strPolicy_10 = strPolicy
        stMQ.strAccNo_16 = ""
        stMQ.strDate_6 = objUtl.CPSDate(datFrom)
        stMQ.strOption_1 = "B"
        stMQ.strPaymentType_4 = ""
        stMQ.strRemarkCode_1 = ""
        stMQ.strUser_3 = ""

        itfMQ = objMQ
        strMsg = objUtl.MQStruToString(stMQ)

        Try

            itfMQ.Init(lngSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg("")
            If itfMQ.MQReply() = 0 Then
                dt = Me.MQMsg2DT("PAYH", itfMQ.MQReplyMSG, strErrNo, strErrMsg)
                itfMQ.CloseQueue()
            End If
            itfMQ.DisConnect()
        Catch mqex As MQException
            strErrMsg = mqex.ToString
            strErrNo = -1
        End Try

        Return dt

    End Function

    Private Function MQMsg2DT(ByVal strFunc As String, ByVal strMQ As String, _
            ByRef strErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim sqlConnect As SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim strSQL As String
        Dim MQdt As New DataTable(strFunc)
        Dim MQdr As DataRow

        Try
            sqlConnect = New SqlConnection(objUtl.ConnStr("CSW", "CS2005", "CIW"))
            strSQL = "select * from csw_mq_" & strFunc
            sqlDA = New SqlDataAdapter(strSQL, sqlConnect)
            sqlDA.MissingSchemaAction = MissingSchemaAction.Add
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.FillSchema(MQdt, SchemaType.Source)
            sqlDA.Fill(MQdt)

        Catch ex As Exception
            strErrNo = -1
            strErrMsg = ex.ToString
            Return Nothing
        Catch sqlex As SqlException
            strErrNo = sqlex.Number
            strErrMsg = sqlex.ToString
            Return Nothing
        Finally
            sqlConnect.Close()
        End Try

        Dim ary As Object = MQdt.Rows(0).ItemArray
        Dim i, curloc, len, ttllen, colcnt As Integer
        Dim strTmp As String

        colcnt = MQdt.Columns.Count - 1
        curloc = 1
        ttllen = strMQ.Length

        Try
            For i = 0 To colcnt
                Dim strType As String
                Select Case Trim(ary(i))
                    Case "C"
                        strType = "System.String"
                    Case "N"
                        strType = "System.Decimal"
                    Case "D"
                        strType = "System.DateTime"
                    Case "I"
                        strType = "System.UInt16"
                End Select
                MQdt.Columns.Add(MQdt.Columns(i).ColumnName, Type.GetType(strType))
            Next

            While curloc < ttllen
                MQdr = MQdt.NewRow
                For i = 0 To colcnt
                    len = MQdt.Columns(i).MaxLength()
                    strTmp = strMQ.Substring(curloc, len)
                    Select Case Trim(ary(i))
                        Case "C"
                            MQdr(i) = strTmp
                        Case "N"
                            MQdr(i) = CDec(strTmp)
                        Case "D"
                            MQdr(i) = objUtl.VBDate(strTmp)
                        Case "I"
                            MQdr(i) = CInt(strTmp)
                    End Select
                    Console.WriteLine(strTmp)
                    curloc += len
                Next
                MQdt.Rows.Add(MQdr)
            End While
        Catch ex As Exception
            strErrNo = -1
            strErrMsg = ex.ToString
            MQdt = Nothing
        End Try

        Return MQdt

    End Function

End Class
