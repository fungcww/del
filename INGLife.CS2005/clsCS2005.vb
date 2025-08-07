Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.EnterpriseServices
Imports Interop
Imports INGLife.Interface
Imports System.Threading

<Assembly: ApplicationName("CS2005")> 
<Assembly: ApplicationActivation(ActivationOption.Server)> 
<Assembly: ApplicationAccessControl(True)> 

<TransactionAttribute(TransactionOption.Supported)> _
Public Class CS2005
    Inherits ServicedComponent

    Implements ICS2005

    Private itfMQ As IMQConnect
    Private objMQ As MQConnect
    Private strErr As String, lngErrNo As Long
    Private lngSize As Long
    Private objSec As ISecurity

    Private cCAPSIL, cCIWLIB, cCIWDB As String
    Private ORDUPO, ORDUPOP2, ORDUCO, ORDURL, ORDUPH, ORDUET, DDAFORM, CCDREG, ORDUAG, BANKFIL, ORDUCH, ORDUNA, ORDCNA, ORDUMC, ORDUNAL3 As String
    Private ORDUPR, ORDUIR, ORDUUV, SFTLCK, POS029W1, POS029W2, USERPF As String
    Private strCIWConn, strCAPSILConn, strCAPENQConn, strUPSConn, strPOSConn, strICRConn, strQryTimeOut As String

    ' MQ connection info.
    Private cNTQMgr As String
    Private cNTRecvQ As String
    Private cCAPReplyQ As String
    Private cNTReplyQ As String
    Private cMQBufferSize As Integer

    Private cGINTRecvQ As String
    Private cGICAPReplyQ As String
    Private cGINTReplyQ As String


    Public Sub New()

        objMQ = New MQConnect

        objSec = New INGLife.DBAccess.Database


#If UAT = 0 Then
        strCIWConn = Me.objSec.ConnStr("CS2005", "CIW", "CSUSER")
        strCAPSILConn = Me.objSec.ConnStr("CS2005", "ENQ400", "CSUSER")
        strCAPENQConn = Me.objSec.ConnStr("CS2005", "ENQ400", "CSUSER")
        strICRConn = Me.objSec.ConnStr("CS2005", "ICR", "CRSBATCH")
#Else
        strCIWConn = objSec.ConnStr("LAS", "INGCIWS202", "LASUPDATE")
        strCAPSILConn = objSec.ConnStr("CS2005", "ENQ40085", "CSUSER")
        strCAPENQConn = objSec.ConnStr("CS2005", "ENQ40085", "CSUSER")
        strICRConn = objSec.ConnStr("LAS", "INGICRS202", "LASUPDATE")
#End If

        strUPSConn = strCIWConn
        strPOSConn = strCIWConn

        objSec = Nothing

#If UAT = 0 Then
        cCAPSIL = "EAADTA"
        cCIWLIB = "CIWLIB"
        cCIWDB = "CIWLIB"
#Else
        cCAPSIL = "LDDCPS85DB"
        cCIWLIB = "CSDCIWSBP"
        cCIWDB = "CSDCWH85DB"
#End If


        ORDUPO = cCAPSIL + ".ORDUPO"
        ORDUPOP2 = cCAPSIL + ".ORDUPOP2"
        ORDUCO = cCAPSIL + ".ORDUCO"
        ORDURL = cCAPSIL + ".ORDURL"
        ORDUPH = cCAPSIL + ".ORDUPH"
        ORDUET = cCAPSIL + ".ORDUET"
        DDAFORM = cCAPSIL + ".DDAFORM"
        CCDREG = cCAPSIL + ".CCDREG"
        BANKFIL = cCAPSIL + ".BANKFIL"
        ORDUCH = cCAPSIL + ".ORDUCH"
        ORDUNA = cCAPSIL + ".ORDUNA"
        ORDUNAL3 = cCAPSIL + ".ORDUNAL3"
        ORDCNA = cCAPSIL + ".ORDCNA"
        ORDUAG = cCAPSIL + ".ORDUAG"
        ORDUMC = cCAPSIL + ".ORDUMC"
        ORDUPR = cCAPSIL + ".ORDUPR"
        ORDUIR = cCAPSIL + ".ORDUIR"
        ORDUUV = cCAPSIL + ".ORDUUV"
        SFTLCK = cCAPSIL + ".SFTLCK"
        POS029W1 = cCAPSIL + ".POS029W1"
        POS029W2 = cCAPSIL + ".POS029W2"
        USERPF = cCAPSIL + ".USERPF"


    End Sub

    Public WriteOnly Property BufferSize() As Integer Implements ICS2005.BufferSize
        Set(ByVal Value As Integer)
            lngSize = Value
        End Set
    End Property

    Public Property Env() As Integer Implements ICS2005.Env
        Get
            ' Property is not applicable to SingleCall objects, which is stateless.
            Return 0
        End Get
        Set(ByVal Value As Integer)

            ' Property is not applicable to SingleCall objects, which is stateless.
        End Set

    End Property

    Private Sub SetMQVar()
        Dim dtInfo As DataTable
        dtInfo = GetSystemInfo(lngErrNo, strErr)

        If Not dtInfo Is Nothing Then
            If dtInfo.Rows.Count > 0 Then
                cNTQMgr = dtInfo.Rows(0).Item("cswsi_NT_queue_mgr")
                cNTRecvQ = dtInfo.Rows(0).Item("cswsi_NT_recv_queue")
                cCAPReplyQ = dtInfo.Rows(0).Item("cswsi_CAP_reply_queue")
                cNTReplyQ = dtInfo.Rows(0).Item("cswsi_NT_reply_queue")
                cMQBufferSize = dtInfo.Rows(0).Item("cswsi_message_size")

                cGINTRecvQ = dtInfo.Rows(0).Item("cswsi_GINT_recv_queue")
                cGICAPReplyQ = dtInfo.Rows(0).Item("cswsi_GICAP_reply_queue")
                cGINTReplyQ = dtInfo.Rows(0).Item("cswsi_GINT_reply_queue")
            End If
        End If
    End Sub

    'Public Function SearchPolicy(ByVal strCriteria As String, ByRef strErrNo As Long, ByVal strErrMsg As String) As DataTable

    '    Dim strSQL As String

    '    Dim sqlConnect As SqlConnection
    '    Dim sqlDA As SqlDataAdapter
    '    Dim sqlDT As New DataTable
    '    Dim sqlDR As DataRow

    '    If strCriteria = "" Then
    '        strErrNo = -1
    '        strErrMsg = "Invalid Criteria"
    '        Exit Function
    '    End If

    '    strSQL = "select Description, policyaccountid, PolicyEffDate, PolicyCurrency " & _
    '        " from policyaccount a, product p " & _
    '        " where a.productid = p.productid " & _
    '        " and " & strCriteria

    '    Try
    '        sqlConnect = New SqlConnection(objUtl.ConnStr("CSW", "CS2005", "CIW"))
    '        sqlDA = New SqlDataAdapter(strSQL, sqlConnect)
    '        sqlDA.MissingSchemaAction = MissingSchemaAction.Add
    '        sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
    '        sqlDA.FillSchema(sqlDT, SchemaType.Source)
    '        sqlDA.Fill(sqlDT)

    '    Catch ex As Exception
    '        strErrNo = -1
    '        strErrMsg = ex.ToString
    '        Return Nothing

    '    Catch sqlex As SqlException
    '        strErrNo = sqlex.Number
    '        strErrMsg = sqlex.ToString
    '        Return Nothing

    '    Finally
    '        sqlConnect.Close()
    '    End Try

    'End Function

    Public Function GetPaymentHistory(ByVal strPolicy As String, ByVal datFrom As Date, _
            ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetPaymentHistory

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New PAYH
        stMQ.strFunc_5 = cPAYH
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strAccNo_16 = ""
        stMQ.strDate_6 = CPSDate(datFrom).Substring(1)
        stMQ.strOption_1 = "B"
        stMQ.strPaymentType_4 = ""
        stMQ.strRemarkCode_1 = ""
        stMQ.strUser_3 = ""

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)
        'dt = MQMsg2DT("PAYH", strErrNo, strErrMsg)

        Call SetMQVar()

       
        Try

            'itfMQ.Init(lngSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("PAYH", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 1, intCurLoc, lngErrNo, strErrMsg, 999)
            
            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
            'dt = MQMsg2DT(cPAYH, lngErrNo, strErrMsg)

            'If itfMQ.MQReply() = 0 Then
            'MQFill(dt, strErrNo, strErrMsg)
            'itfMQ.CloseQueue()
            'End If
            'itfMQ.DisConnect()
        Catch mqex As MQException
            strErrMsg = cPAYH & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        'Return dt
        Return MQdt(0)

    End Function

    Public Function GetCouponHistory(ByVal strPolicy As String, ByVal datFrom As Date, _
        ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetCouponHistory

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New COUH
        stMQ.strFunc_5 = cCOUH
        stMQ.strMonth_2 = CStr(Month(datFrom)).PadLeft(2, "0")
        stMQ.strYear_2 = Right(CStr(Year(datFrom)), 2)
        stMQ.strPolicy_10 = RTrim(strPolicy)

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)
        'dt = MQMsg2DT("PAYH", strErrNo, strErrMsg)

        Call SetMQVar()

        Try

            'itfMQ.Init(lngSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()
            'dt = MQMsg2DT(cCOUH, lngErrNo, strErrMsg)

            MQdt = GenMQDT("COUH", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 1, intCurLoc, lngErrNo, strErrMsg, 999)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cCOUH & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Private Function MQMsg2DT(ByVal strFunc As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim strMQ As String = itfMQ.MQReplyMSG
        Dim sqlConnect As SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim strSQL As String
        Dim MQdt As New DataTable(strFunc)
        Dim dtTemplate As New DataTable
        Dim MQdr As DataRow

        ' Create empty table first so that we can return to caller even MQ calls failed
        Try
            sqlConnect = New SqlConnection(strCIWConn)
            strSQL = "select * from csw_mq_" & strFunc
            sqlDA = New SqlDataAdapter(strSQL, sqlConnect)
            sqlDA.MissingSchemaAction = MissingSchemaAction.Add
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.FillSchema(dtTemplate, SchemaType.Source)
            sqlDA.Fill(dtTemplate)

        Catch sqlex As SqlException
            lngErrNo = sqlex.Number
            strErrMsg = sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = strFunc & " - " & ex.ToString
            Return Nothing
        End Try

        sqlConnect.Dispose()

        Dim ary As Object = dtTemplate.Rows(0).ItemArray
        Dim i, curloc, len, ttllen, colcnt, startcol As Integer
        Dim strTmp As String

        colcnt = dtTemplate.Columns.Count - 1
        curloc = 1
        startcol = 0
        If UCase(dtTemplate.Columns(0).ColumnName) = "CONTFLAG" Then
            curloc = 2
            startcol = 1
        End If
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
                MQdt.Columns.Add(dtTemplate.Columns(i).ColumnName, Type.GetType(strType))
            Next

            ' if mq return msg, and msg is okay, parse the message
            If itfMQ.MQReply = 0 And ttllen > 0 Then
                If strMQ.Substring(0, 1) = "N" Then
                    While curloc < ttllen
                        MQdr = MQdt.NewRow
                        For i = startcol To colcnt
                            len = dtTemplate.Columns(i).MaxLength()
                            strTmp = strMQ.Substring(curloc, len)
                            Select Case Trim(ary(i))
                                Case "C"
                                    MQdr(i) = strTmp
                                Case "N"
                                    If Right(Trim(strTmp), 1) = "-" Then
                                        MQdr(i) = CDec(Replace(strTmp, "-", "")) * -1
                                    Else
                                        MQdr(i) = CDec(strTmp)
                                    End If
                                Case "D"
                                    MQdr(i) = VBDate(strTmp)
                                Case "I"
                                    MQdr(i) = CInt(strTmp)
                            End Select
                            Console.WriteLine(strTmp)
                            curloc += len
                        Next
                        MQdt.Rows.Add(MQdr)
                    End While
                Else
                    'strErrNo = itfMQ.MQReply
                    lngErrNo = -1
                    strErrMsg = strFunc & " - " & strMQ.Substring(1)
                End If
            End If
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = strFunc & " - " & ex.ToString
            MQdt = Nothing
        End Try

        If startcol = 1 AndAlso lngErrNo = 0 AndAlso ttllen > 0 Then
            MQdt.Rows(0).Item(0) = strMQ.Substring(1, 1)
        End If

        Return MQdt

    End Function

    Private Function GenMQDT(ByVal strFunc As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, _
            ByRef dsTemplate As DataSet) As DataTable()

        Dim sqlConnect As SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim strSQL As String
        Dim MQdt() As DataTable
        Dim MQdr As DataRow
        Dim intLoop As Integer


#If UAT <> 0 Then
            'Dim strCIWConn As String
            'strCIWConn = "data source=hksqluat1;persist security info=True;initial catalog=vantive;user id=vantiveuser;password=holy321"
#End If

        ' Create empty table first so that we can return to caller even MQ calls failed
        Try
            sqlConnect = New SqlConnection(strCIWConn)
            strSQL = "select * from " & CSW_MQ_FUNC_HEADER & " where cswmfh_func = '" & strFunc & "' order by cswmfh_seq; " & _
                "select * from " & CSW_MQ_FUNC_DETAIL & " where cswmfd_func = '" & strFunc & "' order by cswmfd_func_seq, cswmfd_field_seq"

            sqlDA = New SqlDataAdapter(strSQL, sqlConnect)
            sqlDA.MissingSchemaAction = MissingSchemaAction.Add
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.TableMappings.Add("csw_mq_func_header1", "csw_mq_func_detail")
            sqlDA.Fill(dsTemplate, "csw_mq_func_header")

        Catch sqlex As SqlException
            lngErrNo = sqlex.Number
            strErrMsg = sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = strFunc & " - " & ex.ToString
            Return Nothing
        End Try

        sqlConnect.Dispose()

        If dsTemplate.Tables("csw_mq_func_detail").Rows.Count > 0 Then
            With dsTemplate.Tables("csw_mq_func_detail")
                Dim intSeq As Integer
                Dim strType As String

                intSeq = -1
                For j As Integer = 0 To .Rows.Count - 1

                    If .Rows(j).Item("cswmfd_func_seq") <> intSeq Then

                        intSeq = .Rows(j).Item("cswmfd_func_seq")
                        ReDim Preserve MQdt(intSeq - 1)
                        If intSeq = 1 Then
                            MQdt(intSeq - 1) = New DataTable(strFunc)
                        Else
                            MQdt(intSeq - 1) = New DataTable(strFunc & Trim(intSeq))
                        End If

                    End If

                    Select Case Trim(.Rows(j).Item("cswmfd_field_type"))
                        Case "C"
                            strType = "System.String"
                        Case "N", "O"   ' GI
                            strType = "System.Decimal"
                        Case "D", "E"   ' GI
                            strType = "System.DateTime"
                        Case "I"
                            strType = "System.Int16"
                    End Select
                    MQdt(intSeq - 1).Columns.Add(.Rows(j).Item("cswmfd_field_name"), Type.GetType(strType))
                Next
            End With
        End If

        Return MQdt

    End Function

    Private Function ParseMQMsg(ByRef MQdt As DataTable, ByVal drTemplate() As DataRow, ByVal intStartCol As Integer, _
            ByRef intStartPos As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String, _
            Optional ByRef intRepeat As Integer = 1, Optional ByVal strDelimiter As String = "", Optional ByVal strMsg As String = "")

        Dim strMQMsg As String
        Dim intColCnt, intFldLen, intCurLoc, intTtlLen As Integer
        Dim strTmp As String
        Dim MQdr As DataRow

        If strMsg = "" Then
            strMQMsg = itfMQ.MQReplyMSG()
        Else
            strMQMsg = strMsg
        End If

        intTtlLen = strMQMsg.Length - 1
        intCurLoc = intStartPos

        If itfMQ.MQReply = 0 AndAlso intTtlLen > 0 AndAlso strMQMsg.Substring(0, 1) = "N" Then

            For i As Integer = 1 To intRepeat

                MQdr = MQdt.NewRow
                For j As Integer = intStartCol To drTemplate.Length - 1
                    intFldLen = drTemplate(j).Item("cswmfd_field_size")
                    strTmp = strMQMsg.Substring(intCurLoc, intFldLen)
                    Select Case Trim(drTemplate(j).Item("cswmfd_field_type"))
                        Case "C"
                            MQdr(j) = strTmp
                        Case "N"
                            If Trim(strTmp) = "" Then
                                MQdr(j) = 0.0
                            Else
                                If Right(Trim(strTmp), 1) = "-" Then
                                    MQdr(j) = CDec(Replace(strTmp, "-", "")) * -1
                                Else
                                    MQdr(j) = CDec(strTmp)
                                End If
                            End If
                        Case "D"
                            MQdr(j) = VBDate(strTmp)
                        Case "O"        ' Numeric for GIPSEA
                            If Trim(strTmp) = "" Then
                                MQdr(j) = 0.0
                            Else
                                If Right(Trim(strTmp), 1) = "-" Then
                                    MQdr(j) = CDec(Replace(strTmp, "-", "")) * -1
                                Else
                                    MQdr(j) = CDec(Replace(strTmp, "+", ""))
                                End If
                            End If
                        Case "E"        ' Date for GIPSEA
                            MQdr(j) = VBGIDate(strTmp)
                        Case "I"
                            If Trim(strTmp) = "" Then
                                MQdr(j) = 0
                            Else
                                MQdr(j) = CInt(strTmp)
                            End If
                    End Select
                    intCurLoc += intFldLen
                Next
                MQdt.Rows.Add(MQdr)

                If intCurLoc >= intTtlLen Then
                    Exit For
                End If

                If strDelimiter <> "" AndAlso strMQMsg.Substring(intCurLoc, 1) = strDelimiter Then
                    intCurLoc += 1
                    Exit For
                End If
            Next

            intStartPos = intCurLoc
        Else
            lngErrNo = -1
            strErrMsg = drTemplate(0).Item("cswmfd_func") & " - " & strMQMsg.Substring(1)
        End If

    End Function

    Private Function MQMsg2DT_Ext(ByVal strFunc As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, _
            ByRef intStopPos As Integer, Optional ByVal intStartPos As Integer = 0, Optional ByVal intRepTimes As Integer = 999) As DataTable

        Dim strMQ As String = itfMQ.MQReplyMSG.Substring(intStartPos)
        Dim sqlConnect As SqlConnection
        Dim sqlDA As SqlDataAdapter
        Dim strSQL As String
        Dim MQdt As New DataTable(strFunc)
        Dim dtTemplate As New DataTable
        Dim MQdr As DataRow
        Dim intLoop As Integer

        ' Create empty table first so that we can return to caller even MQ calls failed
        Try
            sqlConnect = New SqlConnection(strCIWConn)
            strSQL = "select * from csw_mq_" & strFunc
            sqlDA = New SqlDataAdapter(strSQL, sqlConnect)
            sqlDA.MissingSchemaAction = MissingSchemaAction.Add
            sqlDA.MissingMappingAction = MissingMappingAction.Passthrough
            sqlDA.FillSchema(dtTemplate, SchemaType.Source)
            sqlDA.Fill(dtTemplate)

        Catch sqlex As SqlException
            lngErrNo = sqlex.Number
            strErrMsg = sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = strFunc & " - " & ex.ToString
            Return Nothing
        End Try

        sqlConnect.Dispose()

        Dim ary As Object = dtTemplate.Rows(0).ItemArray
        Dim i, curloc, len, ttllen, colcnt, startcol As Integer
        Dim strTmp As String

        colcnt = dtTemplate.Columns.Count - 1
        curloc = 1
        startcol = 0
        If UCase(dtTemplate.Columns(0).ColumnName) = "CONTFLAG" Then
            curloc = 2
            startcol = 1
        End If
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
                MQdt.Columns.Add(dtTemplate.Columns(i).ColumnName, Type.GetType(strType))
            Next

            intLoop = 0
            ' if mq return msg, and msg is okay, parse the message
            If itfMQ.MQReply = 0 Then
                If strMQ.Substring(0, 1) = "N" Then
                    While (curloc < ttllen) Or (intLoop < intRepTimes)
                        MQdr = MQdt.NewRow
                        For i = startcol To colcnt
                            len = dtTemplate.Columns(i).MaxLength()
                            strTmp = strMQ.Substring(curloc, len)
                            Select Case Trim(ary(i))
                                Case "C"
                                    MQdr(i) = strTmp
                                Case "N"
                                    If Right(Trim(strTmp), 1) = "-" Then
                                        MQdr(i) = CDec(Replace(strTmp, "-", "")) * -1
                                    Else
                                        MQdr(i) = CDec(strTmp)
                                    End If
                                Case "D"
                                    MQdr(i) = VBDate(strTmp)
                                Case "I"
                                    MQdr(i) = CInt(strTmp)
                            End Select
                            Console.WriteLine(strTmp)
                            curloc += len
                        Next
                        MQdt.Rows.Add(MQdr)
                        intLoop += 1
                    End While
                Else
                    'strErrNo = itfMQ.MQReply
                    lngErrNo = -1
                    strErrMsg = strFunc & " - " & strMQ.Substring(1)
                End If
            End If
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = strFunc & " - " & ex.ToString
            MQdt = Nothing
        End Try

        intStopPos = curloc

        If startcol = 1 AndAlso lngErrNo = 0 Then
            MQdt.Rows(0).Item(0) = strMQ.Substring(1, 1)
        End If

        Return MQdt

    End Function
    'Public Function GetCapsilHistory(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable() Implements ICS2005.GetCapsilHistory

    '    Dim oledbconnect As New OleDbConnection
    '    Dim daDDA, daCCDR, daHICL As OleDbDataAdapter
    '    Dim strSQL As String
    '    Dim dtCAPSIL(2) As DataTable

    '    oledbconnect.ConnectionString = _
    '        "Provider=IBMDA400.DataSource.1;Persist Security Info=False;User ID=TSTUSR;Data Source=10.10.1.101;" & _
    '        "Password=TSTUSR000;Initial Catalog='';Transport Product=Client Access;SSL=DEFAULT;"
    '    Try
    '        ' DDA
    '        strSQL = " Select TRIM(DPONO) AS PolicyAccountID, DSEQ as DDASeqNo, TRIM(DSTS) as DDAStatus, "
    '        strSQL &= " TRIM(DLSTS) as DDALastStatus, PODRAW as DDADrawDate, DSUBMT as DDASubmitDate, DSTCHG as DDAStsChgDate, "
    '        strSQL &= " DEFF as DDAEffectiveDate, DEND as DDAEndDate, DLMNT as DDALastMaintDate, TRIM(DCOMMT) as DDAComments, "
    '        strSQL &= " TRIM(DOPR) as DDAOperator, DFUDTE as DDAFollowUpDate, TRIM(DFUOPR) as DDAFollowUpOpr, "
    '        strSQL &= " TRIM(DFURMK) as DDARemarks, TRIM(DPAYOR) as DDAPayorInfo, TRIM(DBANK)||TRIM(DBRCH)||TRIM(DACCT) as DDABankAccountNo, "
    '        strSQL &= " TRIM(BFBKNM) as DDABankerName, TRIM(DBANK) as DDABankCode, TRIM(DBRCH) as DDABranchCode, "
    '        strSQL &= " DSIMPI as DDASIMPIND, "
    '        strSQL &= " DPAYID as DDAPAYERID, DIDTYP as DDAPAYERIDTYPE, DDBLMT as DDADEBITLIMIT, "
    '        strSQL &= " TRIM(DREFNO) as DREFNO, TRIM(DLREF) as DLREF, DINREJ, TRIM(DLOIN) as DLOIN "
    '        strSQL &= " From lddcpsldb.DDAFORM, lddcpsldb.BANKFIL, lddcpsldb.ORDUPO "
    '        strSQL &= " Where DPONO = '" & strPolicy & "'"
    '        strSQL &= " AND DBANK = BFBANK"
    '        strSQL &= " And DBRCH = BFBRCH"
    '        strSQL &= " And DPONO = POPONO"
    '        daDDA = New OleDbDataAdapter(strSQL, oledbconnect)
    '        daDDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
    '        daDDA.MissingMappingAction = MissingMappingAction.Passthrough
    '        dtCAPSIL(0) = New DataTable("DDA")
    '        daDDA.Fill(dtCAPSIL(0))

    '        ' CCDR
    '        strSQL = " Select TRIM(CPONO) as PolicyAccountID,  CSEQ as CCDRSeqNo, TRIM(CSTS) as CCDRStatus, "
    '        strSQL &= " TRIM(CLSTS) as CCDRLastStatus, PODRAW as CCDRDrawDate, CSUBMT as CCDRSubmitDate,"
    '        strSQL &= " CSTCHG as CCDRStsChgDate, CEFF as CCDREffectiveDate, CEND as CCDREndDate, CLMNT as CCDRLastMaintDate,"
    '        strSQL &= " TRIM(CCOMMT) as CCDRComments, TRIM(COPR) as CCDROperator, CFUDTE as CCDRFollowUpDate,"
    '        strSQL &= " TRIM(CFUOPR) as CCDRFollowUpOpr, TRIM(CFURMK) as CCDRRemarks, TRIM(CCDHLD) as CCDRCardHolderName,"
    '        strSQL &= " TRIM(CCDNO) as CCDRCardNumber, CINREJ as CCDRIniRej, CEXPDA as CCDRExpiryDate "
    '        strSQL &= " From " & CCDREG & ", " & ORDUPO
    '        strSQL &= " Where CPONO = '" & strPolicy & "' "
    '        strSQL &= " And CPONO = POPONO"
    '        daCCDR = New OleDbDataAdapter(strSQL, oledbconnect)
    '        daCCDR.MissingSchemaAction = MissingSchemaAction.AddWithKey
    '        daCCDR.MissingMappingAction = MissingMappingAction.Passthrough
    '        dtCAPSIL(1) = New DataTable("CCDR")
    '        daCCDR.Fill(dtCAPSIL(1))

    '        ' HICL
    '        Dim strPolN1, strPolN2 As String
    '        strPolN1 = Left(strPolicy, 9)
    '        strPolN2 = IIf(strPolicy.Length > 9, Mid(strPolicy, 10, 1), "")

    '        strSQL = " Select * "
    '        strSQL &= " From " & ORDUCH
    '        strSQL &= " Where CHPON1 = '" & strPolN1 & "'"
    '        strSQL &= " And CHPON2 = '" & strPolN2 & "'"
    '        daHICL = New OleDbDataAdapter(strSQL, oledbconnect)
    '        daHICL.MissingSchemaAction = MissingSchemaAction.AddWithKey
    '        daHICL.MissingMappingAction = MissingMappingAction.Passthrough
    '        dtCAPSIL(2) = New DataTable("HICL")
    '        daHICL.Fill(dtCAPSIL(2))

    '    Catch sqlex As OdbcException
    '        lngErrNo = -1
    '        strErrMsg = "GetCapsilHistory - " & sqlex.ToString
    '        Return Nothing
    '    Catch ex As Exception
    '        lngErrNo = -1
    '        strErrMsg = "GetCapsilHistory - " & ex.ToString
    '        Return Nothing
    '    End Try

    '    Return dtCAPSIL

    'End Function

    Public Function GetORDUNA(ByVal strNANO As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetORDUNA

        'Dim oledbconnect As New OleDbConnection
        'Dim daNA As OleDbDataAdapter
        Dim oledbconnect As New OdbcConnection
        Dim daNA As OdbcDataAdapter
        Dim strSQL As String
        Dim dtCAPSIL As DataTable

        oledbconnect.ConnectionString = strCAPSILConn

        Try
            ' Name & Address
            'strSQL = "Select NATIT as NamePrefix, NAFNAM as FirstName, NALNM1||NALNM2||NALNM3 as NameSuffix, " & _
            '    " TRIM(CNALNM)||TRIM(CNAFNM) as ChiName, NABYY, NABMM, NABDD, NASEX as Gender, " & _
            '    " 'U' as MaritalStatusCode, NASMK as SmokerFlag, NALANG as LanguageCode, " & _
            '    " CNAID as GovernmentIDCard, '0' as OptOutFlag, " & _
            '    " NACMP1||NACMP2 as CoName, CNACNM as CoCName, CNAEMA as EmailAddr, CNAUSE as UseChiInd, " & _
            '    " LEFT(CNAEAT,1) as AddressTypeCode, NAADD1 as AddressLine1, NAADD2 as AddressLine2, NAADD3 as AddressLine3, " & _
            '    " NACITY as AddressCity, NAPOST as AddressPostalCode, NABTEL as PhoneNumber1, NARTEL as PhoneNumber2, " & _
            '    " CNAFAX as FaxNumber1, NABADD as BadAddress, NACCDE as CountryCode, CNACIW as CustomerID, NANO as ClientID, " & _
            '    " 'A' as CustomerStatusCode, 'CL' as CustomerTypeCode, 'S' as MaritalStatusCode, " & _
            '    " SUBSTRING(AGNO,2,5) as AgentCode, AGAGCY as Agency " & _
            '    " From " & ORDCNA & " c1, " & ORDCNA & " c2, " & ORDUNA & " LEFT JOIN " & ORDUAG & _
            '    " ON NANO = AGHNAD " & _
            '    " And LEFT(AGNO,1) = 'H' " & _
            '    " Where NANO = CNANO " & _
            '    " And NANO IN (" & strNANO & ") " & _
            '    " And CNACO = '  ' and NACOMP = '  ' "
            'strSQL &= "Select NATIT as NamePrefix, NAFNAM as FirstName, NALNM1||NALNM2||NALNM3 as NameSuffix, " & _
            '    " TRIM(CNALNM)||TRIM(CNAFNM) as ChiName, NABYY, NABMM, NABDD, NASEX as Gender, " & _
            '    " 'U' as MaritalStatusCode, NASMK as SmokerFlag, NALANG as LanguageCode, " & _
            '    " CNAID as GovernmentIDCard, '0' as OptOutFlag, " & _
            '    " NACMP1||NACMP2 as CoName, CNACNM as CoCName, CNAEMA as EmailAddr, CNAUSE as UseChiInd, " & _
            '    " LEFT(CNACAT,1) as AddressTypeCode, CNAAD1 as AddressLine1, CNAAD2 as AddressLine2, CNAAD3 as AddressLine3, " & _
            '    " CNAAD4 as AddressCity, NAPOST as AddressPostalCode, NABTEL as PhoneNumber1, NARTEL as PhoneNumber2, " & _
            '    " CNAFAX as FaxNumber1, NABADD as BadAddress, NACCDE as CountryCode, CNACIW as CustomerID, NANO as ClientID, " & _
            '    " 'A' as CustomerStatusCode, 'CL' as CustomerTypeCode, " & _
            '    " SUBSTRING(AGNO,2,5) as AgentCode, AGAGCY as Agency, CNAEAT, CNACAT " & _
            '    " From " & ORDCNA & ", " & ORDUNA & " LEFT JOIN " & ORDUAG & _
            '    " ON NANO = AGHNAD " & _
            '    " And LEFT(AGNO,1) = 'H' " & _
            '    " Where NANO = CNANO " & _
            '    " And NANO IN (" & strNANO & ") " & _
            '    " And CNACO = '  ' and NACOMP = '  ' "
            ' Remove duplicate
            '''strSQL = "Select NATIT as NamePrefix, NAFNAM as FirstName, NALNM1||NALNM2||NALNM3 as NameSuffix, " & _
            '''    " TRIM(c2.CNALNM)||TRIM(c2.CNAFNM) as ChiName, NABYY, NABMM, NABDD, NASEX as Gender, " & _
            '''    " 'U' as MaritalStatusCode, NASMK as SmokerFlag, NALANG as LanguageCode, " & _
            '''    " c2.CNAID as GovernmentIDCard, '0' as OptOutFlag, " & _
            '''    " NACMP1||NACMP2 as CoName, c2.CNACNM as CoCName, c2.CNAEMA as EmailAddr, c2.CNAUSE as UseChiInd, " & _
            '''    " LEFT(c2.CNAEAT,1) as AddressTypeCode, NAADD1 as AddressLine1, NAADD2 as AddressLine2, NAADD3 as AddressLine3, " & _
            '''    " NACITY as AddressCity, NAPOST as AddressPostalCode, NARTEL as PhoneNumber1, NABTEL as PhoneNumber2, " & _
            '''    " c2.CNAFAX as FaxNumber1, NABADD as BadAddress, NACCDE as CountryCode, c2.CNACIW as CustomerID, NANO as ClientID, " & _
            '''    " 'A' as CustomerStatusCode, 'CL' as CustomerTypeCode, " & _
            '''    " CASE WHEN LEFT(NANO,5) = '00000' THEN SUBSTRING(NANO,6,5) ELSE '     ' END as AgentCode, c2.CNAEAT, c2.CNACAT, " & _
            '''    " c2.CNAAD1 as CAddressLine1, c2.CNAAD2 as CAddressLine2, c2.CNAAD3 as CAddressLine3, c2.CNAAD4 as CAddressCity " & _
            '''    " From " & ORDCNA & " c1, " & ORDCNA & " c2, " & ORDUNA & _
            '''    " Where c1.CNANO IN (" & strNANO & ") " & _
            '''    " And c1.CNACO = '  ' and NACOMP = '  ' and c2.CNACO = '  ' " & _
            '''    " And c1.CNACIW = c2.CNACIW and c2.CNANO = NANO "
            ' Enhancement for PDPO - Add OptOutOtherFlag at the end of the result.
            strSQL = "Select NATIT as NamePrefix, NAFNAM as FirstName, NALNM1||NALNM2||NALNM3 as NameSuffix, " & _
                " TRIM(c2.CNALNM)||TRIM(c2.CNAFNM) as ChiName, NABYY, NABMM, NABDD, NASEX as Gender, " & _
                " 'U' as MaritalStatusCode, NASMK as SmokerFlag, NALANG as LanguageCode, " & _
                " c2.CNAID as GovernmentIDCard, '0' as OptOutFlag, " & _
                " NACMP1||NACMP2 as CoName, c2.CNACNM as CoCName, c2.CNAEMA as EmailAddr, c2.CNAUSE as UseChiInd, " & _
                " LEFT(c2.CNAEAT,1) as AddressTypeCode, NAADD1 as AddressLine1, NAADD2 as AddressLine2, NAADD3 as AddressLine3, " & _
                " NACITY as AddressCity, NAPOST as AddressPostalCode, NARTEL as PhoneNumber1, NABTEL as PhoneNumber2, " & _
                " c2.CNAFAX as FaxNumber1, NABADD as BadAddress, NACCDE as CountryCode, c2.CNACIW as CustomerID, NANO as ClientID, " & _
                " 'A' as CustomerStatusCode, 'CL' as CustomerTypeCode, " & _
                " CASE WHEN LEFT(NANO,5) = '00000' THEN SUBSTRING(NANO,6,5) ELSE '     ' END as AgentCode, c2.CNAEAT, c2.CNACAT, " & _
                " c2.CNAAD1 as CAddressLine1, c2.CNAAD2 as CAddressLine2, c2.CNAAD3 as CAddressLine3, c2.CNAAD4 as CAddressCity, '0' as OptOutOtherFlag " & _
                " From " & ORDCNA & " c2, " & ORDUNA & _
                " Where c2.CNANO IN (" & strNANO & ") " & _
                " And NACOMP = '  ' and c2.CNACO = '  ' " & _
                " And c2.CNANO = NANO "
            '" And c1.CNACO = '  ' and NACOMP = '  ' and c2.CNACO = '  ' "
            'strSQL &= "UNION ALL "
            'strSQL &= "Select NATIT as NamePrefix, NAFNAM as FirstName, NALNM1||NALNM2||NALNM3 as NameSuffix, " & _
            '    " TRIM(c2.CNALNM)||TRIM(c2.CNAFNM) as ChiName, NABYY, NABMM, NABDD, NASEX as Gender, " & _
            '    " 'U' as MaritalStatusCode, NASMK as SmokerFlag, NALANG as LanguageCode, " & _
            '    " c2.CNAID as GovernmentIDCard, '0' as OptOutFlag, " & _
            '    " NACMP1||NACMP2 as CoName, c2.CNACNM as CoCName, c2.CNAEMA as EmailAddr, c2.CNAUSE as UseChiInd, " & _
            '    " LEFT(c2.CNACAT,1) as AddressTypeCode, c2.CNAAD1 as AddressLine1, c2.CNAAD2 as AddressLine2, c2.CNAAD3 as AddressLine3, " & _
            '    " c2.CNAAD4 as AddressCity, NAPOST as AddressPostalCode, NARTEL as PhoneNumber1, NABTEL as PhoneNumber2, " & _
            '    " c2.CNAFAX as FaxNumber1, NABADD as BadAddress, NACCDE as CountryCode, c2.CNACIW as CustomerID, NANO as ClientID, " & _
            '    " 'A' as CustomerStatusCode, 'CL' as CustomerTypeCode, " & _
            '    " SUBSTRING(AGNO,2,5) as AgentCode, AGAGCY as Agency, c2.CNAEAT, c2.CNACAT " & _
            '    " From " & ORDCNA & " c1, " & ORDCNA & " c2, " & ORDUNA & " LEFT JOIN " & ORDUAG & _
            '    " ON NANO = AGHNAD " & _
            '    " And LEFT(AGNO,1) = 'H' " & _
            '    " Where c1.CNANO IN (" & strNANO & ") " & _
            '    " And c1.CNACO = '  ' and NACOMP = '  ' and c2.CNACO = '  ' " & _
            '    " And c1.CNACIW = c2.CNACIW and c2.CNANO = NANO "
            'daNA = New OleDbDataAdapter(strSQL, oledbconnect)
            daNA = New OdbcDataAdapter(strSQL, oledbconnect)
            'daNA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'daNA.MissingMappingAction = MissingMappingAction.Passthrough
            dtCAPSIL = New DataTable("ORDUNA")
            daNA.Fill(dtCAPSIL)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetORDUNA - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetORDUNA - " & ex.ToString
            Return Nothing
        End Try

        ' Format the result
        ' PolicyRelateCode - Change to CIW format
        With dtCAPSIL.Columns
            .Add("DateOfBirth", Type.GetType("System.DateTime"))
            .Add("PhoneMobile", Type.GetType("System.String"))
            .Add("PhonePager", Type.GetType("System.String"))
            .Add("PassportNumber", Type.GetType("System.String"))
            .Add("Occupation", Type.GetType("System.String"))
            .Add("BirthPlace", Type.GetType("System.String"))
            .Add("FaxNumber2", Type.GetType("System.String"))
        End With

        Dim intRow As Integer = dtCAPSIL.Rows.Count
        Dim i As Integer
        Dim strTel As String

        For i = 0 To intRow - 1
            With dtCAPSIL.Rows(i)
                .Item("DateOfBirth") = DateSerial(.Item("NABYY") + 1800, .Item("NABMM"), .Item("NABDD"))
                If .Item("DateOfBirth") <= #1/1/1800# Then
                    .Item("DateOfBirth") = DBNull.Value
                End If
                If Not IsDBNull(.Item("AgentCode")) AndAlso Trim(.Item("AgentCode")) <> "" Then
                    .Item("CustomerTypeCode") = "AG"
                End If
                If .Item("AddressTypeCode") = "B" Or .Item("AddressTypeCode") = "J" Then      ' swap tel1 & tel2
                    strTel = .Item("PhoneNumber1")
                    .Item("PhoneNumber1") = .Item("PhoneNumber2")
                    .Item("PhoneNumber2") = strTel
                End If
            End With
        Next

        Return dtCAPSIL

    End Function

    'Public Function GetCapsilPolicy(ByVal strPolicy As String, ByVal intCustID As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable() Implements ICS2005.GetCapsilPolicy

    '    Dim oledbconnect As New OleDbConnection
    '    Dim daPOLINF, daCOINFO, daNA, daCOPI As OleDbDataAdapter
    '    Dim strSQL As String
    '    Dim dtCAPSIL(3) As DataTable

    '    oledbconnect.ConnectionString = _
    '        "Provider=IBMDA400.DataSource.1;Persist Security Info=False;User ID=TSTUSR;Data Source=10.10.1.101;" & _
    '        "Password=TSTUSR000;Initial Catalog='';Transport Product=Client Access;SSL=DEFAULT;"
    '    Try
    '        ' Policy Summary (PI of COV=01)
    '        strSQL = "Select * " & _
    '            " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUPH & " h " & _
    '            " Where r.FLD0004 = '" & strPolicy & "'" & _
    '            " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
    '            " And r.FLD0004 = POPONO " & _
    '            " And POBTBL = h.FLD0002 and POBRTE = h.FLD0003 and h.FLD0001 = '  ' " & _
    '            " And r.FLD0005 in ('00','01') "
    '        daPOLINF = New OleDbDataAdapter(strSQL, oledbconnect)
    '        daPOLINF.MissingSchemaAction = MissingSchemaAction.AddWithKey
    '        daPOLINF.MissingMappingAction = MissingMappingAction.Passthrough
    '        dtCAPSIL(1) = New DataTable("POLINF")
    '        daPOLINF.Fill(dtCAPSIL(0))

    '        ' Coverage
    '        strSQL = "Select * " & _
    '            " From " & ORDUCO & ", " & ORDUPO & _
    '            " Where POPONO = '" & strPolicy & "' " & _
    '            " And POPONO = COPOLI"
    '        daCOINFO = New OleDbDataAdapter(strSQL, oledbconnect)
    '        daCOINFO.MissingSchemaAction = MissingSchemaAction.AddWithKey
    '        daCOINFO.MissingMappingAction = MissingMappingAction.Passthrough
    '        dtCAPSIL(2) = New DataTable("COINFO")
    '        daCOINFO.Fill(dtCAPSIL(1))

    '        ' Policy Insured
    '        strSQL = "Select * " & _
    '            " From " & ORDUCO & ", " & ORDURL & ", " & ORDUNA & _
    '            " Where COPOLI = '" & strPolicy & "' " & _
    '            " And COPOLI = FLD0004 " & _
    '            " And FLD0001 = '  ' and FLD0003 = 'CAPS-I-L' " & _
    '            " And FLD0006 = 'I' " & _
    '            " And FLD0005 = COTRNU " & _
    '            " And FLD0002 = NANO "
    '        daCOPI = New OleDbDataAdapter(strSQL, oledbconnect)
    '        daCOPI.MissingSchemaAction = MissingSchemaAction.AddWithKey
    '        daCOPI.MissingMappingAction = MissingMappingAction.Passthrough
    '        dtCAPSIL(3) = New DataTable("COPI")
    '        daCOPI.Fill(dtCAPSIL(2))

    '        ' Name & Address
    '        strSQL = "Select * " & _
    '            " From " & ORDUNA & ", " & ORDCNA & _
    '            " Where CNACIW = " & intCustID & _
    '            " And CNANO = NANO And NACOMP = '  '"
    '        daNA = New OleDbDataAdapter(strSQL, oledbconnect)
    '        daNA.MissingSchemaAction = MissingSchemaAction.AddWithKey
    '        daNA.MissingMappingAction = MissingMappingAction.Passthrough
    '        dtCAPSIL(0) = New DataTable("NA")
    '        daNA.Fill(dtCAPSIL(3))

    '    Catch sqlex As OdbcException
    '        lngErrNo = -1
    '        strErrMsg = "GetCapsilPolicy - " & sqlex.ToString
    '        Return Nothing
    '    Catch ex As Exception
    '        lngErrNo = -1
    '        strErrMsg = "GetCapsilPolicy - " & ex.ToString
    '        Return Nothing
    '    End Try

    '    Return dtCAPSIL

    'End Function

    Public Function GetPolicyList(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String, _
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer, _
            Optional ByVal blnCntOnly As Boolean = False) As DataTable Implements ICS2005.GetPolicyList

        'Dim oledbconnect As New OleDbConnection
        'Dim daPOLST, daAGTLST As OleDbDataAdapter
        Dim oledbconnect As New OdbcConnection
        Dim daPOLST, daAGTLST As OdbcDataAdapter
        Dim strSQL, strSEL, strSELC As String
        Dim dtPOLST, dtAGTLST As DataTable
        Dim intTmpCnt As Integer

        oledbconnect.ConnectionString = strCAPSILConn


        strSELC = "Select count(*) "

        Try
            ' Policy List (by policy)
            If strPolicy <> "" Then
                strSEL = "Select RTRIM(POPONO) as PolicyAccountid, TRIM(h.ETVAL) as ProductID, TRIM(h.ETDESC) as Description, " & _
                        " TRIM(r.FLD0006) as FLD0006, '  ' as PolicyRelateCode, " & _
                        " TRIM(POSTS) as AccountStatusCode, TRIM(POCOCD) as POCOCD, '   ' as PolicyCurrency, " & _
                        " POIMM, POIDD, POIYY, POMODX AS PaidMode, POAGCY, POPAGT, POWAGT, SMCODE, POPAYY, POPAMM, POPADD, POMPEM as ModalPremium "

                strSQL = _
                    " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUET & " h, " & ORDUCO & _
                    " Where r.FLD0004 LIKE " & strPolicy & _
                    " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
                    " And r.FLD0004 = POPONO " & _
                    " And POCOMP = '  ' " & _
                    " And POBTBL||POBRTE = h.ETVAL and ETTPE = 'PWRD' and ETCOMP = '  ' " & _
                    " And POPONO = COPOLI And COCOCO = '  ' And COTRAI=1 "
            End If

            ' Policy List (by customer id)
            '" And POBTBL = h.FLD0002 and POBRTE = h.FLD0003 and h.FLD0001 = '  ' "
            If strCustID <> "" Then
                strSEL = "Select DISTINCT RTRIM(POPONO) as PolicyAccountid, TRIM(h.ETVAL) as ProductID, TRIM(h.ETDESC) as Description, " & _
                        " TRIM(r.FLD0006) as FLD0006, '  ' as PolicyRelateCode, " & _
                        " TRIM(POSTS) as AccountStatusCode, TRIM(POCOCD) as POCOCD, '   ' as PolicyCurrency, " & _
                        " POIMM, POIDD, POIYY, POMODX AS PaidMode, POAGCY, POPAGT, POWAGT, SMCODE, POPAYY, POPAMM, POPADD, POMPEM as ModalPremium "

                strSQL = _
                    " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUET & " h, " & ORDUCO & _
                    " Where r.FLD0004 = POPONO " & _
                    " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
                    " And FLD0002 = '" & strCustID & "' " & _
                    " And POBTBL||POBRTE = h.ETVAL and ETTPE = 'PWRD' and ETCOMP = '  '" & _
                    " And POPONO = COPOLI And COCOCO = '  ' And COTRAI=1 "
            End If

            If strRel <> "" Then
                strSQL &= " And r.FLD0006 = '" & strRel & "'"
            Else
                'strSQL &= " And r.FLD0005 in ('00', '01') "
                strSQL &= " And (r.FLD0005 IN ('00','01') OR r.FLD0006 = 'I') "
            End If

            If blnCntOnly Then
                oledbconnect.Open()
                'Dim cmd As OleDbCommand = New OleDbCommand(strSQL, oledbconnect)
                Dim cmd As OdbcCommand = New OdbcCommand(strSELC & strSQL, oledbconnect)
                intTmpCnt = cmd.ExecuteScalar
                oledbconnect.Close()
            End If

            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                'daPOLST = New OleDbDataAdapter(strSQL, oledbconnect)
                daPOLST = New OdbcDataAdapter(strSEL & strSQL, oledbconnect)
                'daPOLST.MissingSchemaAction = MissingSchemaAction.AddWithKey
                'daPOLST.MissingMappingAction = MissingMappingAction.Passthrough
                dtPOLST = New DataTable(strTable)
                daPOLST.Fill(dtPOLST)
                intCnt = dtPOLST.Rows.Count
            End If

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & ex.ToString
            Return Nothing
        End Try

        ' Format the result
        ' PolicyRelateCode - Change to CIW format
        With dtPOLST.Columns
            .Add("PolicyEffDate", Type.GetType("System.DateTime"))
            .Add("PaidToDate", Type.GetType("System.DateTime"))
        End With

        Dim intRow As Integer = dtPOLST.Rows.Count
        Dim i As Integer
        For i = 0 To intRow - 1
            With dtPOLST.Rows(i)

                Select Case Trim(dtPOLST.Rows(i).Item("FLD0006"))
                    Case "A"
                        .Item("PolicyRelateCode") = "AS"
                    Case "B"
                        .Item("PolicyRelateCode") = "BE"
                    Case "I"
                        .Item("PolicyRelateCode") = "PI"
                    Case "O"
                        .Item("PolicyRelateCode") = "PH"
                    Case "P"
                        .Item("PolicyRelateCode") = "PR"
                    Case "S"
                        .Item("PolicyRelateCode") = "SP"
                End Select

                Select Case Trim(dtPOLST.Rows(i).Item("POCOCD"))
                    Case "BA"
                        .Item("PolicyCurrency") = "AUD"
                    Case "BH", "HK"
                        .Item("PolicyCurrency") = "HKD"
                    Case "BM", "US"
                        .Item("PolicyCurrency") = "USD"
                End Select

                .Item("PolicyEffDate") = DateSerial(.Item("POIYY") + 1800, .Item("POIMM"), .Item("POIDD"))
                .Item("PaidToDate") = DateSerial(.Item("POPAYY") + 1800, .Item("POPAMM"), .Item("POPADD"))
            End With
        Next

        Return dtPOLST

    End Function

    Public Function GetPolicyList1(ByVal strCustID As String, ByVal strPolicy As String, ByVal strRel As String, _
            ByVal strTable As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable = New DataTable(strTable)

        Try
            strSQL = "Select a.Productid, c.PolicyAccountid, c.PolicyRelateCode, PolicyEffDate, a.AccountStatusCode, " & _
                "            PolicyCurrency, ISNULL(rtrim(ChiLstNm)+rtrim(ChiFstNm),'') as ChiName, Title, NamePrefix, " & _
                "            FirstName, NameSuffix, Description as PDescription " & _
                " From csw_poli_rel c, policyaccount a, customer r, product p " & _
                " Where c.policyaccountid = a.policyaccountid " & _
                " And c.customerid = r.customerid " & _
                " And a.productid = p.productid "

            If strCustID = "" And strPolicy = "" Then
                MsgBox("Please pass either Customer ID or Policy#")
                Return Nothing
            End If

            ' CustID - all related policy
            ' Policy - Specific policy (PH)
            If strCustID <> "" Then
                strSQL &= " and c.customerid = '" & Trim(strCustID) & "' "
            Else
                strSQL &= " and a.PolicyAccountid LIKE " & Trim(strPolicy)
                strSQL &= " and PolicyRelatecode = '" & Trim(strRel) & "'"
            End If

            strSQL &= " ORDER BY c.PolicyAccountid"

            'sqlconnect.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
            sqlconnect.ConnectionString = strCIWConn

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough

            sqlda.Fill(sqldt)

        Catch sqlex As SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetPolicyList - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyList - " & ex.ToString
            Return Nothing
        End Try

        Return sqldt

    End Function

    Public Function GetPolicySummary(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetPolicySummary

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        Dim stMQ As New POVAL
        stMQ.strFunc_5 = cPOVAL
        stMQ.strPolicy_10 = RTrim(strPolicy)

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("POVAL", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cPOVAL & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Private Function GetPolicySummary1(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        'Dim sqlconnect As New SqlConnection
        'Dim sqlcmd As New SqlCommand
        'Dim strSQL As String
        'Dim lngCnt As Long
        'Dim sqlda As SqlDataAdapter
        'Dim sqldt As DataTable = New DataTable("POLINF")

        'Try
        '    strSQL = "select *, ISNULL(rtrim(ChiLstNm)+rtrim(ChiFstNm),'') as ChiName " & _
        '            " from product p, customer c, policyaccount a, csw_poli_rel r " & _
        '            " where r.policyaccountid = '" & strPolicy & "' " & _
        '            " and r.customerid = c.customerid " & _
        '            " and r.policyaccountid = a.policyaccountid " & _
        '            " and a.ProductID = p.ProductID "
        '    sqlconnect.ConnectionString = strCIWConn

        '    sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        '    sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        '    sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        '    sqlda.Fill(sqldt)

        'Catch sqlex As SqlException
        '    lngErrNo = sqlex.Number
        '    strErrMsg = "GetPolicySummary - " & sqlex.ToString
        '    Return Nothing
        'Catch ex As Exception
        '    lngErrNo = -1
        '    strErrMsg = "GetPolicySummary - " & ex.ToString
        '    Return Nothing
        'End Try

        'Return sqldt

        'Dim oledbconnect As New OleDbConnection
        'Dim oledbda As OleDbDataAdapter
        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL, strSEL As String
        Dim dtCAPSIL As DataTable

        oledbconnect.ConnectionString = strCAPSILConn

        strSEL = "Select TRIM(h.ETVAL) as ProductID, FLD0005, TRIM(h.ETDESC) as Description, TRIM(FLD0006) as FLD0006, " & _
                " CASE WHEN FLD0006 = 'A' THEN 'AS' " & _
                "      WHEN FLD0006 = 'B' THEN 'BE' " & _
                "      WHEN FLD0006 = 'I' THEN 'PI' " & _
                "      WHEN FLD0006 = 'O' THEN 'PH' " & _
                "      WHEN FLD0006 = 'P' THEN 'PR' " & _
                "      WHEN FLD0006 = 'S' THEN 'SP' ELSE '' END as PolicyRelateCode, " & _
                " TRIM(POSTS) as AccountStatusCode, TRIM(POCOCD) as POCOCD, " & _
                " CASE WHEN POCOCD = 'BA' THEN 'AUD' " & _
                "      WHEN POCOCD = 'BH' THEN 'HKD' " & _
                "      WHEN POCOCD = 'BM' THEN 'USD' " & _
                "      WHEN POCOCD = 'HK' THEN 'HKD' " & _
                "      WHEN POCOCD = 'US' THEN 'USD' ELSE '' END as PolicyCurrency, " & _
                " POIMM, POIDD, POIYY, " & _
                " CASE WHEN POSTS IN ('1','2','3','4','V','Z') THEN 0 ELSE POSDTE END as POSDTE, POPAYY, POPAMM, POPADD, " & _
                " TRIM(BILNO) as BillNo, SUINSU as SumInsured, 0 as PolicyPremium, " & _
                " 'IL' as ProductCategory, '  ' as ProductTypeCode, POMETH as BillingType, " & _
                " CASE WHEN POMODX=1 THEN 'M' " & _
                "      WHEN POMODX=6 THEN 'H' ELSE 'A' END as Mode, " & _
                " POBDTE, POSAMT, POMPEM as ModalPremium, POLMPR as LastModalPremium, 0 as DueAmount, 0 as NotYetDueAmount, " & _
                " RTRIM(PORMRN) as NoticeRecord, POFR15, POPMSP as PremiumSuspense, POUACH as MiscSuspense, TRIM(PODOPT) as DividendOption, " & _
                " TRIM(PORGSA) as CouponOption, PODVTL as TotalDeclareValue, PODVTY as CurrDeclVal, PODVPU as AdditionDeathCvr, " & _
                " PODBAM as Disbursement, RTRIM(FLD0002) as ClientID, POAGCY, POPAGT, POWAGT, POCFCT as CurModeFactor, POPONO as PolicyAccountID, " & _
                " PAGCO1, PAGCO2, PAGCO3, PODVDR, POFELK, POAPOP, POCPDR, PODVCP "

        Try
            ' Policy Summary (PI of COV=01)
            strSQL = strSEL & _
                " From " & ORDURL & " r, " & ORDUPO & ", " & ORDUET & " h, " & ORDUCO & ", " & ORDUPOP2 & _
                " Where POPONO = '" & strPolicy & "' " & _
                " And POCOMP = '  ' and COCOCO = '  ' " & _
                " And r.FLD0001 = '  ' and r.FLD0003 = 'CAPS-I-L' " & _
                " And POPONO = FLD0004 And POPONO = COPOLI And COTRAI=1 " & _
                " And POPONO = POLNO " & _
                " And POBTBL||POBRTE = h.ETVAL and ETTPE = 'PWRD' and ETCOMP = '  ' ORDER BY FLD0005"
            '" And FLD0005 IN ('00','01') "
            'oledbda = New OleDbDataAdapter(strSQL, oledbconnect)
            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            'oledbda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'oledbda.MissingMappingAction = MissingMappingAction.Passthrough
            dtCAPSIL = New DataTable("POLINF")
            oledbda.Fill(dtCAPSIL)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetPolicySummary - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicySummary - " & ex.ToString
            Return Nothing
        End Try

        ' Format the result
        ' PolicyRelateCode - Change to CIW format
        With dtCAPSIL.Columns
            .Add("CommencementDate", Type.GetType("System.DateTime"))
            .Add("PolicyEffDate", Type.GetType("System.DateTime"))
            .Add("PolicyTermDate", Type.GetType("System.DateTime"))
            .Add("PaidToDate", Type.GetType("System.DateTime"))
            .Add("BillToDate", Type.GetType("System.DateTime"))
            .Add("RCD", Type.GetType("System.DateTime"))
            .Add("APLStartDate", Type.GetType("System.DateTime"))
        End With

        Dim intRow As Integer = dtCAPSIL.Rows.Count
        Dim i As Integer
        For i = 0 To intRow - 1
            With dtCAPSIL.Rows(i)
                'Select Case Trim(.Item("Mode"))
                '    Case "1"
                '        .Item("Mode") = "M"
                '    Case "6"
                '        .Item("Mode") = "H"
                '    Case "12"
                '        .Item("Mode") = "A"
                'End Select

                'Select Case Trim(dtCAPSIL.Rows(i).Item("FLD0006"))
                '    Case "A"
                '        .Item("PolicyRelateCode") = "AS"
                '    Case "B"
                '        .Item("PolicyRelateCode") = "BE"
                '    Case "I"
                '        .Item("PolicyRelateCode") = "PI"
                '    Case "O"
                '        .Item("PolicyRelateCode") = "PH"
                '    Case "P"
                '        .Item("PolicyRelateCode") = "PR"
                '    Case "S"
                '        .Item("PolicyRelateCode") = "SP"
                'End Select

                'Select Case Trim(dtCAPSIL.Rows(i).Item("POCOCD"))
                '    Case "BA"
                '        .Item("PolicyCurrency") = "AUD"
                '    Case "BH", "HK"
                '        .Item("PolicyCurrency") = "HKD"
                '    Case "BM", "US"
                '        .Item("PolicyCurrency") = "USD"
                'End Select

                .Item("CommencementDate") = DateSerial(.Item("POIYY") + 1800, .Item("POIMM"), .Item("POIDD"))
                .Item("PolicyEffDate") = DateSerial(.Item("POIYY") + 1800, .Item("POIMM"), .Item("POIDD"))
                .Item("RCD") = DateSerial(.Item("POIYY") + 1800, .Item("POIMM"), .Item("POIDD"))
                .Item("PolicyTermDate") = VBDate(.Item("POSDTE"))
                .Item("PaidToDate") = DateSerial(.Item("POPAYY") + 1800, .Item("POPAMM"), .Item("POPADD"))
                .Item("BillToDate") = VBDate(.Item("POBDTE"))

            End With
        Next

        'Dim ar() As Object
        'Dim newrow As DataRow
        'Dim rel(,) As String = {{"POAGCY", "SA"}, {"POPAGT", "PA"}, {"POWAGT", "WA"}}
        'ar = dtCAPSIL.Rows(0).ItemArray()

        'For i = 0 To UBound(rel, 1)
        '    newrow = dtCAPSIL.NewRow
        '    With dtCAPSIL.Rows(0)
        '        If Not IsDBNull(.Item(rel(i, 0))) AndAlso .Item(rel(i, 0)) <> "" Then
        '            newrow.Item("PolicyRelateCode") = rel(i, 1)
        '            newrow.Item("ClientID") = "00000" & Right(.Item(rel(i, 0)), 5)
        '            dtCAPSIL.Rows.Add(newrow)
        '        End If
        '    End With
        'Next

        Return dtCAPSIL

    End Function

    Public Function GetPolicyAddress(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetPolicyAddress

        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable = New DataTable("CustomerAddress")

        Try
            strSQL = "Select cswpad_addr_type as AddressTypeCode, cswpad_add1 as AddressLine1, cswpad_add2 as AddressLine2, " & _
                " cswpad_add3 as AddressLine3, cswpad_city as AddressCity, cswpad_tel1 as PhoneNumber1, " & _
                " cswpad_tel2 as PhoneNumber2, cswpad_fax1 as FaxNumber1, cswpad_fax2 as FaxNumber2, " & _
                " cswpad_country as CountryCode, cswpad_post_code as AddressPostalCode, 'N' as BadAddress " & _
                " from csw_policy_address " & _
                " where cswpad_poli_id = '" & strPolicy & "' "
            sqlconnect.ConnectionString = strCIWConn

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough

            sqlda.Fill(sqldt)

        Catch sqlex As SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetPolicyAddress - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyAddress - " & ex.ToString
            Return Nothing
        End Try

        Return sqldt

    End Function

    Public Function GetCoverage(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable() Implements ICS2005.GetCoverage

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New POLIC
        stMQ.strFunc_5 = cPOLIC
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strRider_2 = "00"

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("POLIC", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            While intCurLoc < intTtlLen

                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
                ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg)

                Dim intAgtCnt As Integer
                If lngErrNo = 0 Then
                    If Not MQdt(0) Is Nothing AndAlso MQdt(0).Rows.Count > 0 Then
                        intAgtCnt = MQdt(0).Rows(MQdt(0).Rows.Count - 1).Item("AgentCount")
                        dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 2")
                        ParseMQMsg(MQdt(1), dr, 0, intCurLoc, lngErrNo, strErrMsg, intAgtCnt)
                    End If
                Else
                    Exit While
                End If

            End While

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cPOLIC & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt

    End Function

    Private Function GetCoverage1(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        'Dim sqlconnect As New SqlConnection
        'Dim sqlcmd As New SqlCommand
        'Dim strSQL As String
        'Dim lngCnt As Long
        'Dim sqlda As SqlDataAdapter
        'Dim sqldt As DataTable = New DataTable("Coverage")

        'Try
        '    strSQL = "Select TRIM(COPOLI) as PolicyAccountID, COTRAI as Trailer, RPLFIR||RPLSEC||RPLTHI||COFI01 as ProductID, " & _
        '        " TRIM(COTRTY) as CoverageStatus, TRIM(PACODE) as Participate, SUINSU as SumInsured, RMURAA as Multiple, " & _
        '        " TRIM(RRASCL) as RateScale, 0 as DrivingExp" & _
        '        " FEDURA as FlatExtra, MEYEAR, MEMONT, MEDAY, RMOPRE as ModalPremium, " & _
        '        " RISMON, RISDAY, RISYEA, PCYEAR, PCMONT, PCDAY, EXISDA, RASEX, RRAGE, TRIM(SMCODE) as SmokerFlag " & _
        '        " TRIM(RCLNU1), TRIM(RCLNU2), TRIM(RCLNU3), TRIM(RCLNU4), TRIM(RCLNU5) " & _
        '        " From " & ORDUCO & _
        '        " Where COPOLI = '" & strPolicy & "'"

        '    strSQL = "Select *, ISNULL(rtrim(ChiLstNm)+rtrim(ChiFstNm),'') as ChiName " & _
        '        " from coverage c, coveragedetail d, customer r " & _
        '        " where c.policyaccountid = d.policyaccountid " & _
        '        " and c.trailer = d.trailer " & _
        '        " and c.policyaccountid = '" & strPolicy & "' " & _
        '        " and d.customerid = r.customerid"
        '    sqlconnect.ConnectionString = strCIWConn

        '    sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        '    sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        '    sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        '    sqlda.Fill(sqldt)

        'Catch sqlex As SqlException
        '    lngErrNo = sqlex.Number
        '    strErrMsg = "GetCoverage - " & sqlex.ToString
        '    Return Nothing
        'Catch ex As Exception
        '    lngErrNo = -1
        '    strErrMsg = "GetCoverage - " & ex.ToString
        '    Return Nothing
        'End Try

        'Return sqldt

        'Dim oledbconnect As New OleDbConnection
        'Dim oledbda As OleDbDataAdapter
        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL, strSEL As String
        Dim dtCAPSIL As DataTable

        oledbconnect.ConnectionString = strCAPSILConn

        Try
            strSQL = "Select RTRIM(COPOLI) as PolicyAccountID, COTRAI as Trailer, TRIM(ETVAL) as ProductID, " & _
                " TRIM(ETDESC) as Description, " & _
                " TRIM(COTRTY) as CoverageStatus, TRIM(PACODE) as Participate, SUINSU as SumInsured, RMURAA as Multiple, " & _
                " TRIM(RRASCL) as RateScale, 0 as DrivingExp, RAAMOU as CovFaceAmt, RRAAGE as InsuredAge, " & _
                " FEDURA as FlatExtra, MEYEAR, MEMONT, MEDAY, RMOPRE as ModalPremium, " & _
                " RISMON, RISDAY, RISYEA, PCYEAR, PCMONT, PCDAY, EXISDA, RASEX, TRIM(SMCODE) as SmokerFlag, " & _
                " TRIM(RCLNU1) as RCLNU1, TRIM(RCLNU2) as RCLNU2, TRIM(RCLNU3) as RCLNU3, TRIM(RCLNU4) as RCLNU4, TRIM(RCLNU5) as RCLNU5, " & _
                " WPMULT as WPMultiplier, RWPPRE as WPPremium, ADMULT as ADMultiplier, RADPRE as ADPremium, " & _
                " ADFAAM as ADFaceAmount " & _
                " From " & ORDUCO & ", " & ORDUET & _
                " Where COPOLI = '" & strPolicy & "' " & _
                " And RPLFIR||RPLSEC||RPLTHI||COFI01||RRASCL = ETVAL and ETTPE = 'PWRD' and ETCOMP = '  ' " & _
                " And COCOCO = '  ' "
            'oledbda = New OleDbDataAdapter(strSQL, oledbconnect)
            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            'oledbda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'oledbda.MissingMappingAction = MissingMappingAction.Passthrough
            dtCAPSIL = New DataTable("Coverage")
            oledbda.Fill(dtCAPSIL)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetCoverage - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetCoverage - " & ex.ToString
            Return Nothing
        End Try

        ' Format the result
        ' PolicyRelateCode - Change to CIW format
        With dtCAPSIL.Columns
            .Add("IssueDate", Type.GetType("System.DateTime"))
            .Add("ExpiryDate", Type.GetType("System.DateTime"))
            .Add("PremiumChangeDate", Type.GetType("System.DateTime"))
            .Add("ExhibitInforceDate", Type.GetType("System.DateTime"))
            .Add("OccpatClsCode", Type.GetType("System.String"))
        End With

        Dim intRow As Integer = dtCAPSIL.Rows.Count
        Dim i As Integer
        For i = 0 To intRow - 1
            With dtCAPSIL.Rows(i)
                .Item("IssueDate") = DateSerial(.Item("RISYEA") + 1800, .Item("RISMON"), .Item("RISDAY"))
                .Item("ExpiryDate") = DateSerial(.Item("MEYEAR") + 1800, .Item("MEMONT"), .Item("MEDAY"))
                If .Item("PCYEAR") = 0 Then
                    .Item("PremiumChangeDate") = #1/1/1900#
                Else
                    .Item("PremiumChangeDate") = DateSerial(.Item("PCYEAR") + 1800, .Item("PCMONT"), .Item("PCDAY"))
                End If

                .Item("ExhibitInforceDate") = VBDate(.Item("EXISDA"))

                Dim strProd As String
                strProd = .Item("ProductID")
                If Mid(strProd, 2, 1) = "D" Then
                    If Mid(strProd, 3, 1) = "A" Then
                        .Item("OccpatClsCode") = Mid(strProd, 3, 2)
                    Else
                        .Item("OccpatClsCode") = Mid(strProd, 4, 2)
                    End If
                Else
                    .Item("OccpatClsCode") = ""
                End If
                If Mid(strProd, 2, 3) = "RPA" Or Mid(strProd, 2, 3) = "RAI" Or Mid(strProd, 2, 3) = "RHS" Or _
                        Mid(strProd, 2, 3) = "RHI" Or Mid(strProd, 2, 3) = "RHP" Then
                    .Item("OccpatClsCode") = Right(strProd, 1)
                End If
            End With
        Next

        Return dtCAPSIL

    End Function

    Public Function GetClientHistory(ByVal strPolicy As String, ByVal strType As String, ByVal strClientID As String, _
            ByVal datEnq As Date, ByVal strSeq As String, ByVal strBrUser As String, ByVal strCurDept As String, _
            ByVal strCurUsrID As String, ByVal strOption As String, ByVal strCode As String, ByVal datFollow As Date, _
            ByVal strFollowInit As String, ByVal strFollowAction As String, ByVal strComment1 As String, _
            ByVal strComment2 As String, ByVal strComment3 As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetClientHistory

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New HICL

        stMQ.strFunc_5 = cHICL

        If strOption = "B" Then         ' Browse
            stMQ.strType_1 = "B"
            stMQ.strPolicy_10 = strPolicy
            stMQ.strClient_10 = ""
            stMQ.strDate_7 = CPSDate(datEnq)
            stMQ.strSeq_2 = "00"
            stMQ.strBUsr_3 = ""
            stMQ.strCDept_3 = ""
            stMQ.strCUsr_3 = ""
            stMQ.strOption_1 = "B"
            stMQ.strCode_5 = ""
            stMQ.strFDate_7 = ""
            stMQ.strFInit_3 = ""
            stMQ.strFAction_5 = ""
            stMQ.strComment1_50 = ""
            stMQ.strComment2_50 = ""
            stMQ.strComment3_50 = ""
        ElseIf strOption = "C" Then     ' Create
            stMQ.strType_1 = "H"
            stMQ.strPolicy_10 = strPolicy
            stMQ.strClient_10 = strClientID
            stMQ.strDate_7 = CPSDate(datEnq)
            stMQ.strSeq_2 = "00"
            stMQ.strBUsr_3 = Left(Trim(strBrUser), 3)
            stMQ.strCDept_3 = Left(Trim(strCurDept), 3)
            stMQ.strCUsr_3 = Left(Trim(strCurUsrID), 3)
            stMQ.strOption_1 = "C"
            stMQ.strCode_5 = Left(Trim(strCode), 5)
            stMQ.strFDate_7 = IIf(datFollow = #1/1/1900#, "", CPSDate(datFollow))
            stMQ.strFInit_3 = Left(Trim(strFollowInit), 3)
            stMQ.strFAction_5 = Left(Trim(strFollowAction), 5)
            stMQ.strComment1_50 = Left(Trim(strComment1), 50)
            stMQ.strComment2_50 = Left(Trim(strComment2), 50)
            stMQ.strComment3_50 = Left(Trim(strComment3), 50)
        ElseIf strOption = "M" Then     ' Maintenance
            stMQ.strType_1 = "H"
            stMQ.strPolicy_10 = strPolicy
            stMQ.strClient_10 = strClientID
            stMQ.strDate_7 = CPSDate(datEnq)
            stMQ.strSeq_2 = strSeq
            stMQ.strBUsr_3 = Left(Trim(strBrUser), 3)
            stMQ.strCDept_3 = Left(Trim(strCurDept), 3)
            stMQ.strCUsr_3 = Left(Trim(strCurUsrID), 3)
            stMQ.strOption_1 = "M"
            stMQ.strCode_5 = Left(Trim(strCode), 5)
            stMQ.strFDate_7 = IIf(datFollow = #1/1/1900#, "", CPSDate(datFollow))
            stMQ.strFInit_3 = Left(Trim(strFollowInit), 3)
            stMQ.strFAction_5 = Left(Trim(strFollowAction), 5)
            stMQ.strComment1_50 = Left(Trim(strComment1), 50)
            stMQ.strComment2_50 = Left(Trim(strComment2), 50)
            stMQ.strComment3_50 = Left(Trim(strComment3), 50)
        End If

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            If strOption = "B" Then
                MQdt = GenMQDT("HICL", lngErrNo, strErrMsg, dsTemplate)

                Dim intTtlLen, intCurLoc As Integer
                intTtlLen = itfMQ.MQReplyMSG.Length - 1

                If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                    intCurLoc = 2
                Else
                    intCurLoc = 1
                End If

                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
                ParseMQMsg(MQdt(0), dr, 1, intCurLoc, lngErrNo, strErrMsg, 999)

                If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                    If MQdt(0).Rows.Count > 0 Then
                        MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                        MQdt(0).AcceptChanges()
                    End If
                End If
            ElseIf strOption = "C" Or strOption = "M" Then

                Dim strUpdMsg As String
                strUpdMsg = itfMQ.MQReplyMSG

                ReDim MQdt(0)
                MQdt(0) = Nothing

                If Left(strUpdMsg, 1) = "N" Then
                    ' no error, just return Nothing
                    '"RECORD IS CREATED SUCCESSFULLY"
                    '"RECORD IS CHANGED SUCCESSFULLY"
                Else
                    lngErrNo = -1
                    strErrMsg = cHICL & " - " & strUpdMsg.Substring(1)
                End If

            End If

        Catch mqex As MQException
            strErrMsg = cHICL & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)
        'Dim oledbconnect As New OdbcConnection
        'Dim oledbda As OdbcDataAdapter
        'Dim strSQL As String
        'Dim sqldt As DataTable = New DataTable("CustHist")

        'Try
        '    Dim strPolN1, strPolN2 As String
        '    strPolN1 = Left(strPolicy, 9)
        '    strPolN2 = IIf(strPolicy.Length > 9, Mid(strPolicy, 10, 1), "")

        '    strSQL = " Select TRIM(CHENDT) as CHENDT, TRIM(CHCLNO) as ClientID, TRIM(CHSQNO) as SeqNo, "
        '    strSQL &= "       TRIM(CHACCD) as Activity, TRIM(CHCOM1)||TRIM(CHCOM2)||TRIM(CHCOM3) as Remarks, "
        '    strSQL &= "       TRIM(CHUSID) as UserID, TRIM(CHTYPE) as Dept "
        '    strSQL &= " From " & ORDUCH
        '    strSQL &= " Where CHPON1 = '" & strPolN1 & "'"
        '    strSQL &= " And CHPON2 = '" & strPolN2 & "'"
        '    strSQL &= " And CHCOCD = '   '"

        '    oledbconnect.ConnectionString = strCAPSILConn
        '    'oledbda = New OleDbDataAdapter(strSQL, oledbconnect)
        '    oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
        '    'oledbda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        '    'oledbda.MissingMappingAction = MissingMappingAction.Passthrough
        '    oledbda.Fill(sqldt)

        'Catch sqlex As OdbcException
        '    lngErrNo = -1
        '    strErrMsg = "GetClientHistory - " & sqlex.ToString
        '    Return Nothing
        'Catch ex As Exception
        '    lngErrNo = -1
        '    strErrMsg = "GetClientHistory - " & ex.ToString
        '    Return Nothing
        'End Try

        'With sqldt.Columns
        '    .Add("Date", Type.GetType("System.DateTime"))
        'End With

        'Dim intRow As Integer = sqldt.Rows.Count
        'Dim i As Integer
        'For i = 0 To intRow - 1
        '    sqldt.Rows(i).Item("Date") = VBDate(sqldt.Rows(i).Item("CHENDT"))
        'Next

        'Return sqldt

    End Function

    Public Function GetDDA(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetDDA

        Dim stMQ As New DDAR
        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String

        itfMQ = objMQ
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strFunc_5 = cDDAR
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            'itfMQ.Init(lngSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()
            'dt = MQMsg2DT(cDDAR, lngErrNo, strErrMsg)

            MQdt = GenMQDT("DDAR", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, intCurLoc - 1, intCurLoc, lngErrNo, strErrMsg, 999)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cDDAR & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Public Function GetCCDR(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetCCDR

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New CCDR
        stMQ.strFunc_5 = cCCDR
        stMQ.strPolicy_10 = RTrim(strPolicy)

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)
        'dt = MQMsg2DT("PAYH", strErrNo, strErrMsg)

        Call SetMQVar()

        Try

            'itfMQ.Init(lngSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()
            'dt = MQMsg2DT(cCCDR, lngErrNo, strErrMsg)

            MQdt = GenMQDT("CCDR", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg, 999)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cCCDR & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Public Function GetUWInfo(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetUWInfo

        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable = New DataTable("UWInfo")

        Try
            strSQL = "Select * from nbrvw_uw_advice, csw_policy_uw " & _
                " Where cswpuw_poli_id = nbruad_policy_no " & _
                " And nbruad_policy_no = '" & strPolicy & "' "
            sqlconnect.ConnectionString = strCIWConn

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            'sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            'sqlda.MissingMappingAction = MissingMappingAction.Passthrough

            sqlda.Fill(sqldt)

        Catch sqlex As SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetUWInfo - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetUWInfo - " & ex.ToString
            Return Nothing
        End Try

        Return sqldt

    End Function

    Public Function GetPolicyVal(ByVal strPolicy As String, ByVal datVal As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable() Implements [Interface].ICS2005.GetPolicyVal

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New CASHV
        stMQ.strFunc_5 = cCASHV
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strDate_7 = CPSDate(datVal)
        stMQ.strRider_2 = "00"

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("CASHV", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg)

            If lngErrNo = 0 Then
                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 2")
                ParseMQMsg(MQdt(1), dr, 0, intCurLoc, lngErrNo, strErrMsg, 4)
            End If

            If lngErrNo = 0 Then
                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 3")
                ParseMQMsg(MQdt(2), dr, 0, intCurLoc, lngErrNo, strErrMsg)
            End If

            If lngErrNo = 0 AndAlso MQdt(2).Rows.Count > 0 Then
                Dim intCnt As Integer
                intCnt = MQdt(2).Rows(MQdt(2).Rows.Count - 1).Item("NoOfRiders")

                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 4")
                ParseMQMsg(MQdt(3), dr, 0, intCurLoc, lngErrNo, strErrMsg, intCnt)
            End If

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = "ModeChangeQuote" & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        If MQdt(2).Rows.Count > 0 AndAlso MQdt(0).Rows.Count > 0 Then
            MQdt(2).Columns.Add("CashValue", Type.GetType("System.Decimal"))
            MQdt(2).Rows(0).Item("CashValue") = MQdt(0).Rows(0).Item("CashValue")
        End If

        Return MQdt

    End Function

    Public Function GetPolicyVal1(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim connect As New OdbcConnection
        Dim Cmd As OdbcCommand
        Dim reader As OdbcDataReader
        Dim da As OdbcDataAdapter

        Dim strSQL As String
        Dim dtPolVal As New DataTable("PolicyVal")

        'connect.ConnectionString = "DSN=TEST;UID=LDDPOL;PWD=MONITOR"
        connect.ConnectionString = strCAPENQConn

        strSQL = "CALL " & cCIWLIB & ".PolicyVal('" & strPolicy & "')"

        Try
            da = New OdbcDataAdapter(strSQL, connect)
            da.Fill(dtPolVal)
        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetPolicyVal - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyVal - " & ex.ToString
            Return Nothing

        End Try

        Return dtPolVal
        ''Dim sqlconnect As New SqlConnection
        ''Dim sqlcmd As New SqlCommand
        ''Dim strSQL As String
        ''Dim lngCnt As Long
        ''Dim sqlda As SqlDataAdapter
        ''Dim sqldt As DataTable = New DataTable("PolicyVal")

        ''Try
        ''    strSQL = "Select * from PolicyAccount " & _
        ''        " Where PolicyAccountID = '" & strPolicy & "' "
        ''    sqlconnect.ConnectionString = strCIWConn

        ''    sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        ''    sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        ''    sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        ''    sqlda.Fill(sqldt)

        ''Catch sqlex As SqlException
        ''    lngErrNo = sqlex.Number
        ''    strErrMsg = "GetPolicyVal - " & sqlex.ToString
        ''    Return Nothing
        ''Catch ex As Exception
        ''    lngErrNo = -1
        ''    strErrMsg = "GetPolicyVal - " & ex.ToString
        ''    Return Nothing
        ''End Try

        ''Return sqldt

    End Function

    Private Function VBDate(ByVal strDate As String) As Date
        Dim d As String

        Try
            If strDate.Length() = 7 And CInt(strDate) <> 0 Then
                VBDate = DateSerial(CInt(strDate.Substring(0, 3)) + 1800, CInt(strDate.Substring(3, 2)), CInt(strDate.Substring(5, 2)))
                
            Else
                VBDate = #1/1/1900#
            End If
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            'Throw New MQException(strErr)
        End Try

    End Function

    Private Function VBGIDate(ByVal strDate As String) As Date

        Dim d As String

        Try
            If strDate.Length() = 8 And CInt(strDate) <> 0 Then
                VBGIDate = DateSerial(CInt(strDate.Substring(0, 4)), CInt(strDate.Substring(4, 2)), CInt(strDate.Substring(6, 2)))
            Else
                VBGIDate = #1/1/1900#
            End If
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            'Throw New MQException(strErr)
        End Try

    End Function

    Private Function CPSDate(ByVal datVal As Date, Optional ByVal intBase As Integer = 1800) As String
        Dim y As String

        Try
            Return CStr(CInt(datVal.ToString("yyyyMMdd")) - intBase * 10000)
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            'Throw New MQException(strErr)
        End Try

    End Function

    Private Function MQStruToString(ByVal objStru As Object) As String

        Dim t As Type = objStru.GetType
        Dim fi() As FieldInfo = t.GetFields
        Dim f As FieldInfo
        Dim strName, strVal, strMSG As String
        Dim intLen As Integer

        Try
            For Each f In fi
                intLen = CInt(f.Name.Substring(f.Name.LastIndexOf("_") + 1))
                strVal = t.InvokeMember(f.Name, BindingFlags.GetField, Nothing, objStru, Nothing)
                strMSG &= strVal.PadRight(intLen, " ")
            Next
        Catch ex As Exception

        End Try

        Return strMSG

    End Function

    Public Function GetUPSGroup(ByVal strSystem As String, ByVal strUser As String, ByRef lngErrNo As Long, _
        ByRef strErrMsg As String) As Long Implements ICS2005.GetUPSGroup

        Dim strSQL As String
        Dim lngGrpNo As Long
        Dim sqlconnect As New SqlConnection

        Try

            strSQL = "Select upsugt_usr_right from " & UPS_USER_LIST_TAB & ", " & UPS_USER_GROUP_TAB & _
                " Where upsult_sys_abv = '" & strSystem & "'" & _
                " And upsult_usr_id = '" & strUser & "'" & _
                " And upsult_usr_grp = upsugt_usr_grp_no"

            sqlconnect.ConnectionString = strUPSConn
            Dim sqlcmd As New SqlCommand(strSQL, sqlconnect)
            sqlconnect.Open()
            lngGrpNo = sqlcmd.ExecuteScalar

        Catch err As SqlClient.SqlException
            lngErrNo = err.Number
            strErrMsg = err.ToString()
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetUPSGroup - " & ex.ToString
        Finally
            sqlconnect.Close()
        End Try

        Return lngGrpNo

    End Function

    Public Function GetPrivRS(ByVal intGroupID As Integer, ByVal strCtrl As String, ByRef lngErrNo As Long, _
            ByRef strErrMsg As String, Optional ByVal strType As String = "") As DataTable Implements ICS2005.GetPrivRS

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As New SqlDataAdapter
        'Dim sqldt As DataTable = New DataTable(CSW_USER_PRIVS)
        Dim sqldt As New DataTable

        Try
            strSQL = "Select * from " & UPS_MENU_ITEM_TAB & _
                " Where upsmit_sys_abv = '" & strCtrl & "'"

            If intGroupID <> 0 Then
                strSQL &= " And upsmit_class = " & intGroupID
            End If
            'strSQL = "select * from " & CSW_USER_PRIVS & _
            '    " where cswup_group_id = " & intGroupID & _
            '    " and cswup_parent = '" & strCtrl.Trim & "'"

            'If strType <> "" Then
            '    strSQL &= " and cswup_type = '" & strType.Trim & "'"
            'End If

            sqlconnect.ConnectionString = strUPSConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.Add
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.FillSchema(sqldt, SchemaType.Source)
            sqlda.Fill(sqldt)

        Catch err As SqlClient.SqlException
            lngErrNo = err.Number
            strErrMsg = "GetPrivRS - " & err.ToString()
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPrivRS - " & ex.ToString
            Return Nothing
        Finally
            sqlconnect.Close()
        End Try

        Return sqldt

    End Function

    Public Function GetSystemInfo(ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements ICS2005.GetSystemInfo

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dt As New DataTable

        strSQL = "Select * from csw_system_info"

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dt)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetSystemInfo - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetSystemInfo - " & ex.ToString
        End Try

        Return dt

    End Function

    <AutoComplete()> _
    Public Function UpdatePolicyAlert()

        ' Update csw_customer_policy_alert
        ' Any exception will abort the transaction
        Try

        Catch sqlex As SqlException
        Catch ex As Exception

        End Try


    End Function

    <AutoComplete()> _
    Public Function UpdateAuthority()


    End Function

    Public Function GetTrnHistory(ByVal strPolicy As String, ByVal datFrom As Date, ByVal datTo As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements ICS2005.GetTrnHistory

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New TRNH
        stMQ.strFunc_5 = cTRNH
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strFYear_3 = (Year(datFrom) - 1800).ToString
        stMQ.strFMth_2 = Month(datFrom).ToString("00")
        stMQ.strFDay_2 = Day(datFrom).ToString("00")
        stMQ.strTYear_3 = (Year(datTo) - 1800).ToString
        stMQ.strTMth_2 = Month(datTo).ToString("00")
        stMQ.strTDay_2 = Day(datTo).ToString("00")

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("TRNH", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 1, intCurLoc, lngErrNo, strErrMsg, 999)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If

        Catch mqex As MQException
            strErrMsg = cTRNH & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Public Function GetAPLHistory(ByVal strPolicy As String, ByVal datFrom As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements ICS2005.GetAPLHistory

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New APLH
        stMQ.strFunc_5 = cAPLH
        stMQ.strMonth_2 = CStr(Month(datFrom)).PadLeft(2, "0")
        stMQ.strYear_2 = Right(CStr(Year(datFrom)), 2)
        stMQ.strPolicy_10 = RTrim(strPolicy)

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try
            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()
            'dt = MQMsg2DT(cAPLH, lngErrNo, strErrMsg)
            MQdt = GenMQDT("APLH", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 1, intCurLoc, lngErrNo, strErrMsg, 999)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If

        Catch mqex As MQException
            strErrMsg = cAPLH & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Public Function GetClientID(ByVal strCustID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As String Implements ICS2005.GetClientID

        Dim strSQL, strID As String
        'Dim sqlconnect As New OleDbConnection
        'Dim sqlcmd As New OleDbCommand
        Dim sqlconnect As New OdbcConnection
        Dim sqlcmd As New OdbcCommand

        strSQL = "Select CNANO from " & ORDCNA & " Where CNACIW = " & strCustID

        sqlconnect.ConnectionString = strCAPSILConn
        sqlconnect.Open()

        sqlcmd.CommandText = strSQL
        sqlcmd.Connection = sqlconnect

        Try
            strID = sqlcmd.ExecuteScalar()
        Catch err As OdbcException
            lngErrNo = -1
            strErrMsg = "GetClientID - " & err.ToString()
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetClientID - " & ex.ToString
        Finally
            sqlconnect.Close()
        End Try

        sqlcmd.Dispose()
        sqlconnect.Dispose()

        Return strID

    End Function

    Public Function GetCustomerList(ByVal strLastName As String, ByVal strFirstName As String, ByVal strHKID As String, _
            ByVal strCustID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String, ByRef intCnt As Integer, _
            Optional ByVal blnCntOnly As Boolean = False) As System.Data.DataTable Implements ICS2005.GetCustomerList

        'Dim sqlconnect As New OleDbConnection
        Dim sqlconnect As New OdbcConnection
        Dim sqldt As DataTable

        Dim strSEL, strSELC, strSQL, strCR, strCR1, strCR2 As String
        Dim intTmpCnt As Integer
        'NALNM1||NALNM2||NALNM3
        strSELC = "Select count(*) "
        strSEL = "Select NATIT as NamePrefix, NAFNAM as FirstName, NALNM1||NALNM2||NALNM3 as NameSuffix, " & _
            " TRIM(CNALNM)||TRIM(CNAFNM) as ChiName, " & _
            " CNAID as GovernmentIDCard, NABYY, NABMM, NABDD, NASEX as Gender, " & _
            " NACMP1||NACMP2 as CoName, CNACNM as CoCName, CNACIW as CustomerID, NANO as ClientID, " & _
            " CASE WHEN LEFT(NANO,5) = '00000' THEN SUBSTRING(NANO,6,5) ELSE '     ' END as AgentCode "
        '" SUBSTRING(AGNO,2,5) as AgentCode, AGAGCY as Agency "

        strSQL = " From " & ORDCNA & ", " & ORDUNA & _
            " Where NANO = CNANO "

        If strLastName <> "" Then
            strCR1 &= " and " & strLastName
        End If

        If strFirstName <> "" Then
            strCR1 &= " and " & strFirstName
        End If

        If strHKID <> "" Then
            strCR2 &= " and " & strHKID
        End If

        'strCR = Replace(strCustID, "'", "")
        'strCR = strCustID
        If strCustID <> "" Then
            strCR2 &= " and " & strCustID
        End If

        ' Add criteria to speedup the search if by name or NANO (agent): CNACO, NACOMP
        If strCR1 <> "" Or InStr(strHKID, "NANO") > 0 Then
            strSQL &= " And CNACO = '  ' And NACOMP = '  ' "
        End If
        sqlconnect.ConnectionString = strCAPSILConn
        sqlconnect.Open()
        If strCR1 = "" And strCR2 = "" Then
            lngErrNo = -1
            strErrMsg = "GetCustomerList - Invalid Criteria"
            Exit Function
        Else
            '    If strCR1 <> "" And strCR2 = "" Then
            '        Dim strNASQL, strNALst As String
            '        Dim dr As OdbcDataReader

            '        strNASQL = "Select NANO " & _
            '            " From " & ORDUNA & _
            '            " Where 1=1 " & strCR1
            '        Dim sqlcmd1 As New OdbcCommand
            '        sqlcmd1.CommandText = strNASQL
            '        sqlcmd1.Connection = sqlconnect
            '        dr = sqlcmd1.ExecuteReader()
            '        While dr.Read()
            '            If Not IsDBNull(dr.Item("NANO")) Then
            '                strNALst &= "'" & Trim(dr.Item("NANO")) & "',"
            '            End If
            '        End While
            '        dr.Close()
            '        sqlcmd1.Dispose()
            '        If strNALst <> "" Then
            '            strCR1 = " And NANO IN (" & strNALst & "'~~~')"
            '        End If
            '    End If

            strSQL &= strCR1 & strCR2
            'If strCR1 <> "" Then
            '    strSQL = "Select NANO From " & ORDUNA & _
            '        " Where NACOMP = '  ' " & strCR1
            'End If
            'If strCR2 <> "" Then
            '    strSQL = "Select CNANO From " & ORDCNA & _
            '        " Where 1 = 1 " & strCR2
            'End If
        End If

        Try
            If blnCntOnly Then
                'Dim sqlcmd As New OleDbCommand
                Dim sqlcmd As New OdbcCommand
                sqlcmd.CommandText = strSELC & strSQL
                sqlcmd.Connection = sqlconnect
                intTmpCnt = sqlcmd.ExecuteScalar
                sqlconnect.Close()
            End If
            If blnCntOnly AndAlso intTmpCnt > intCnt Then
                intCnt = intTmpCnt
                Exit Function
            Else
                'Dim sqlda As OleDbDataAdapter
                Dim sqlda As OdbcDataAdapter
                sqldt = New DataTable("CustList")
                'sqlda = New OleDbDataAdapter(strSEL & strSQL, sqlconnect)
                sqlda = New OdbcDataAdapter(strSEL & strSQL, sqlconnect)
                sqlda.Fill(sqldt)
                intCnt = sqldt.Rows.Count
            End If

        Catch err As OdbcException
            lngErrNo = -1
            strErrMsg = "GetCustomerList - " & err.ToString()
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetCustomerList - " & ex.ToString
        End Try

        'If blnCntOnly Then
        '    sqlconnect.Close()
        '    sqlconnect.Dispose()
        '    Exit Function
        'End If

        With sqldt.Columns
            .Add("DateOfBirth", Type.GetType("System.DateTime"))
        End With

        Dim intRow As Integer = sqldt.Rows.Count
        Dim i As Integer

        For i = 0 To intRow - 1
            With sqldt.Rows(i)
                If .Item("NABMM") >= 1 And .Item("NABMM") <= 12 And .Item("NABDD") >= 1 And .Item("NABDD") <= 31 And .Item("NABYY") > 0 Then
                    Dim dteDOB As DateTime
                    Dim intYear As Integer = .Item("NABYY") + 1800
                    Dim strMonth As String = Format(.Item("NABMM"), "00")
                    Dim strDay As String = Format(.Item("NABDD"), "00")

                    If IsDate(intYear & "/" & strMonth & "/" & strDay) Then
                        If DateSerial(intYear, CInt(strMonth), CInt(strDay)) <= #1/1/1800# Then
                            .Item("DateOfBirth") = DBNull.Value
                        Else
                            .Item("DateOfBirth") = New DateTime(intYear, CInt(strMonth), CInt(strDay))
                        End If

                    End If
                Else
                    .Item("DateOfBirth") = DBNull.Value
                End If
            End With
        Next

        Return sqldt

    End Function

    Public Function GetModeChangeInfo(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetModeChangeInfo

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        'Dim dt As DataTable
        Dim stMQ As New CASHV
        stMQ.strFunc_5 = cCASHV
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strDate_7 = CPSDate(Today)
        stMQ.strRider_2 = "01"

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("CASHV", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg)

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 2")
            ParseMQMsg(MQdt(1), dr, 0, intCurLoc, lngErrNo, strErrMsg, 4)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = "ModeChangeQuote" & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(1)

    End Function

    Public Function GetModeChangeInfo1(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable

        'Dim oledbconnect As New OleDbConnection
        'Dim oledbda As OleDbDataAdapter
        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL As String
        Dim sqldt As DataTable = New DataTable("ModeChange")

        Try
            strSQL = _
                " Select COPOLI, COTRAI, POMODX, FEPREM, FEDURA, RMURAA, MURADU, ANLIPR, RADPRE, " & _
                "        RWPPRE, RAREFE, RAREMU, POCFCT, RMOPRE, FLD0047 AS MF_S, FLD0049 AS MF_M, POGPRM, RPOFEE, " & _
                "        TRIM(CHAR(POPAMM))||'/'||TRIM(CHAR(POPADD))||'/'||CHAR(INT(POPAYY)+1800) AS PAIDTODATE, " & _
                "        TRIM(CHAR(RISMON))||'/'||TRIM(CHAR(RISDAY))||'/'||CHAR(INT(RISYEA)+1800) AS ISSUEDATE, " & _
                "        POSAMT AS SUNDRYAMT, POMODX, ORPLFI || ORPLRE || RRASCL as RiderCode, RAAMOU as FACEAMOUNT " & _
                " From " & ORDUPO & ", " & ORDUCO & ", " & ORDUPH & _
                " Where POPONO = '" & strPolicy & "' " & _
                " And POPONO = COPOLI " & _
                " And FLD0003 = RRASCL " & _
                " And FLD0002 = ORPLFI || ORPLRE " & _
                " Order By COTRAI"

            oledbconnect.ConnectionString = strCAPSILConn
            'oledbda = New OleDbDataAdapter(strSQL, oledbconnect)
            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            oledbda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            oledbda.MissingMappingAction = MissingMappingAction.Passthrough
            oledbda.Fill(sqldt)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetModeChangeInfo - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetModeChangeInfo - " & ex.ToString
            Return Nothing
        End Try

        Return sqldt

    End Function

    Public Function PrintPolicyStsRpt(ByVal strPolicy As String, ByVal strUser As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean Implements ICS2005.PrintPolicyStsRpt

        Dim strCapsilDt, strUserName, strProd, strDeptName As String

        Dim connect As New OdbcConnection
        Dim Cmd As OdbcCommand

        Dim strSQL As String

        PrintPolicyStsRpt = False
        connect.ConnectionString = strCAPENQConn
        connect.Open()

        'strSQL = "Select FLD0003 from " & ORDUMC
        strSQL = "Select POBTBL From " & ORDUPO & _
            " Where POPONO = '" & strPolicy & "' And POCOMP = '  '"

        Try
            Cmd = New OdbcCommand(strSQL, connect)
            strProd = Cmd.ExecuteScalar
        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "POLE_POLSTS - " & sqlex.ToString
            Exit Function
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "POLE_POLSTS - " & ex.ToString
            Exit Function
        End Try

        'AC - Remove advance compilation option - start
        '#If UAT = 0 Then
        '        strUserName = UCase(Right(strUser, 3))
        '        strDeptName = UCase(Left(strUser, 3))
        '#Else
        '        strUserName = UCase(Right(strUser, 3))
        '        strDeptName = UCase(Left(strUser, 3))
        '        'strUserName = "NST"         ' UAT, print to CSPOLEQ
        '        'strDeptName = "CSR"
        '#End If
        strUserName = UCase(Right(strUser, 3))
        strDeptName = UCase(Left(strUser, 3))
        'AC - Remove advance compilation option - end


        If strProd <> "" Then
            'strSQL = "CALL " & cPOLE & "POS024CL('" & strPolicy.PadRight(10, " ") & "', '" & strCapsilDt & "','" & strUserName & "','Y')"
            If Mid(strProd, 2, 1) = "D" Then
                strSQL = "CALL " & cPOLE & "POS258RP('" & strPolicy & "', 'ST', '" & strUserName & "')"
            Else
                strSQL = "CALL " & cPOLE & "POS257RP('" & strPolicy & "', 'ST', '" & strDeptName & "','" & strUserName & "','" & CPSDate(Today) & "')"
            End If

            Try
                Cmd = New OdbcCommand(strSQL, connect)
                Cmd.ExecuteNonQuery()
            Catch sqlex As OdbcException
                lngErrNo = -1
                strErrMsg = "POLE_POLSTS - " & sqlex.ToString
                Exit Function
            Catch ex As Exception
                lngErrNo = -1
                strErrMsg = "POLE_POLSTS - " & ex.ToString
                Exit Function
            End Try
        Else
            lngErrNo = -1
            strErrMsg = "Policy/Plan code not found - " & strPolicy & "."
            Exit Function
        End If
        connect.Close()
        connect.Dispose()
        PrintPolicyStsRpt = True

    End Function

    Public Function PrintSurrenderRpt(ByVal strPolicy As String, ByVal strUser As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean Implements ICS2005.PrintSurrenderRpt

        Dim strCapsilDt, strProdID, strUserName, strDeptName As String
        Dim connect As New OdbcConnection
        Dim Cmd As OdbcCommand

        Dim strSQL As String

        PrintSurrenderRpt = False

        connect.ConnectionString = strCAPENQConn
        connect.Open()

        'strSQL = "Select FLD0003 from " & ORDUMC
        'Try
        '    Cmd = New OdbcCommand(strSQL, connect)
        '    strCapsilDt = Cmd.ExecuteScalar
        'Catch sqlex As OdbcException
        '    lngErrNo = -1
        '    strErrMsg = "POLE_SURR - " & sqlex.ToString
        '    Exit Function
        'Catch ex As Exception
        '    lngErrNo = -1
        '    strErrMsg = "POLE_SURR - " & ex.ToString
        '    Exit Function
        'End Try

        'AC - Remove advance compilation option - start
        strUserName = UCase(Right(strUser, 3))
        strDeptName = UCase(Left(strUser, 3))
        '#If UAT = 0 Then
        '        strUserName = UCase(Right(strUser, 3))
        '        strDeptName = UCase(Left(strUser, 3))
        '#Else
        '        strUserName = UCase(Right(strUser, 3))
        '        strDeptName = UCase(Left(strUser, 3))
        '        'strUserName = "NST"         ' UAT, print to CSPOLEQ
        '        'strDeptName = "CSR"
        '#End If
        'AC - Remove advance compilation option - end

        'strSQL = "CALL " & cPOLE & "POS028CL('" & strPolicy.PadRight(10, " ") & "', '" & strCapsilDt & "','" & strUserName & "','Y')"
        strSQL = "CALL " & cPOLE & "POS257RP('" & strPolicy & "', 'SR', '" & strDeptName & "','" & strUserName & "','" & CPSDate(Today) & "')"
        Try
            Cmd = New OdbcCommand(strSQL, connect)
            Cmd.ExecuteNonQuery()
        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "POLE_SURR - " & sqlex.ToString
            Exit Function
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "POLE_SURR - " & ex.ToString
            Exit Function
        End Try
        connect.Close()
        connect.Dispose()

        PrintSurrenderRpt = True

    End Function

    Public Function GetCFHistory(ByVal strPolicy As String, ByVal strRider As String, ByVal datFrom As Date, ByVal datTo As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetCFHistory

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        Dim dt As DataTable
        Dim stMQ As New POLIH
        stMQ.strFunc_5 = cPOLIH
        stMQ.strPolicy_10 = strPolicy
        stMQ.strRider_2 = strRider.PadLeft(2, "0")
        stMQ.strFDate_7 = CPSDate(datFrom)
        stMQ.strTDate_7 = CPSDate(datTo)

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("POLIH", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 1, intCurLoc, lngErrNo, strErrMsg, 999)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cPOLIH & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Public Function GetPolicyMisc(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetPolicyMisc

        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL As String
        Dim dtPOLMisc As DataTable

        oledbconnect.ConnectionString = strCAPSILConn
        'RPLFIR||RPLSEC||RPLTHI
        Try

            ' Problem in ORDUET, some plan code cannot be found under 'PWRD'
            'strSQL = "Select FLD0006, FLD0002 as ClientID, FLD0005, COTRAI, POFR15, PAGCO1, PAGCO2, PAGCO3, " & _
            '        " RCLNU1, RCLNU2, RCLNU3, RCLNU4, RCLNU5, RWPPRE as WPPremium, RADPRE ADPremium, " & _
            '        " RMURAA as Multiple, ADFAAM as ADFaceAmount, POIYY, POIMM, POIDD, " & _
            '        " CASE WHEN POSTS IN ('1','2','3','4','V','Z') THEN 0 ELSE POSDTE END AS POSDTE, " & _
            '        " CASE WHEN FLD0006 = 'A' THEN 'AS' " & _
            '        "      WHEN FLD0006 = 'B' THEN 'BE' " & _
            '        "      WHEN FLD0006 = 'I' THEN 'PI' " & _
            '        "      WHEN FLD0006 = 'O' THEN 'PH' " & _
            '        "      WHEN FLD0006 = 'P' THEN 'PR' " & _
            '        "      WHEN FLD0006 = 'S' THEN 'SP' ELSE '' END AS POLICYRELATECODE, TRIM(ETDESC) as Description, " & _
            '        "      ORPLFI || ORPLRE || RRASCL as ProductID, PODVDR, ANLIPR as AnnPrem, RPOFEE as PolFee,  " & _
            '        "      RAREFE as FRR, RAREMU as MRR " & _
            '        " From " & ORDUPO & ", " & ORDUCO & ", " & ORDURL & ", " & ORDUET & _
            '        " Where POPONO = '" & strPolicy & "'" & _
            '        " And POCOMP = '  ' AND COCOCO = '  ' " & _
            '        " And FLD0001 = '  ' AND FLD0003 = 'CAPS-I-L' " & _
            '        " And POPONO = FLD0004 AND POPONO = COPOLI " & _
            '        " And (FLD0005 IN ('00','01') OR FLD0006 = 'I') " & _
            '        " And ORPLFI || ORPLRE || RRASCL = ETVAL " & _
            '        " And ETTPE = 'PWRD' And ETCOMP = '  ' order by COTRAI"
            strSQL = "Select FLD0006, FLD0002 as ClientID, FLD0005, COTRAI, POFR15, PAGCO1, PAGCO2, PAGCO3, " & _
                    " RCLNU1, RCLNU2, RCLNU3, RCLNU4, RCLNU5, RWPPRE as WPPremium, RADPRE ADPremium, " & _
                    " RMURAA as Multiple, ADFAAM as ADFaceAmount, POIYY, POIMM, POIDD, " & _
                    " CASE WHEN POSTS IN ('1','2','3','4','V','Z') THEN 0 ELSE POSDTE END AS POSDTE, " & _
                    " CASE WHEN FLD0006 = 'A' THEN 'AS' " & _
                    "      WHEN FLD0006 = 'B' THEN 'BE' " & _
                    "      WHEN FLD0006 = 'I' THEN 'PI' " & _
                    "      WHEN FLD0006 = 'O' THEN 'PH' " & _
                    "      WHEN FLD0006 = 'P' THEN 'PR' " & _
                    "      WHEN FLD0006 = 'S' THEN 'SP' ELSE '' END AS POLICYRELATECODE, TRIM(ETDESC) as Description, " & _
                    "      ORPLFI || ORPLRE || RRASCL as ProductID, PODVDR, ANLIPR as AnnPrem, RPOFEE as PolFee,  " & _
                    "      RAREFE as FRR, RAREMU as MRR, COTRTY as CStatus " & _
                    " From " & ORDUPO & ", " & ORDURL & ", " & ORDUCO & " LEFT JOIN " & ORDUET & _
                    " ON ORPLFI || ORPLRE || RRASCL = ETVAL " & _
                    " And ETTPE = 'PWRD' And ETCOMP = '  ' " & _
                    " Where POPONO = '" & strPolicy & "'" & _
                    " And POCOMP = '  ' AND COCOCO = '  ' " & _
                    " And FLD0001 = '  ' AND FLD0003 = 'CAPS-I-L' " & _
                    " And POPONO = FLD0004 AND POPONO = COPOLI " & _
                    " And (FLD0005 IN ('00','01') OR FLD0006 = 'I') " & _
                    " ORDER BY COTRAI "

            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            dtPOLMisc = New DataTable("POLMisc")
            oledbda.Fill(dtPOLMisc)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetPolicyMisc SqlException: " & sqlex.ToString()
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPolicyMisc Exception: " & ex.Message.ToString()
            Return Nothing
        End Try

        ' Format the result
        ' PolicyRelateCode - Change to CIW format
        With dtPOLMisc.Columns
            .Add("PolicyTermDate", Type.GetType("System.DateTime"))
            .Add("PolicyEffDate", Type.GetType("System.DateTime"))
        End With

        Dim intRow As Integer = dtPOLMisc.Rows.Count
        For i As Integer = 0 To intRow - 1
            With dtPOLMisc.Rows(i)
                .Item("PolicyTermDate") = VBDate(.Item("POSDTE"))
                .Item("PolicyEffDate") = DateSerial(.Item("POIYY") + 1800, .Item("POIMM"), .Item("POIDD"))
            End With
        Next

        Return dtPOLMisc

    End Function

    Public Function GetULHistory(ByVal strPolicy As String, ByVal datEnq As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable() Implements [Interface].ICS2005.GetULHistory

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String
        Dim dt As DataTable
        Dim stMQ As New POLIU
        stMQ.strFunc_5 = cPOLIU
        stMQ.strPolicy_10 = strPolicy
        stMQ.strYear_3 = CStr(Year(datEnq) - 1800)
        stMQ.strMonth_2 = Format(Month(datEnq), "00")
        stMQ.strDay_2 = "00"

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("POLIU", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg)

            If lngErrNo = 0 Then
                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 2")
                ParseMQMsg(MQdt(1), dr, 0, intCurLoc, lngErrNo, strErrMsg, 999)
            End If

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cPOLIH & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt

    End Function

    Public Function GetDCAR(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetDCAR

        Dim stMQ As New DCAR

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String

        itfMQ = objMQ
        stMQ.strFunc_5 = cDCAR
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strOption_1 = "I"
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            'itfMQ.Init(lngSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()
            'dt = MQMsg2DT(cDDAR, lngErrNo, strErrMsg)

            MQdt = GenMQDT("DCAR", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, intCurLoc - 1, intCurLoc, lngErrNo, strErrMsg, 999)

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cDCAR & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt(0)

    End Function

    Public Function GetBANK(ByVal strBankCode As String, ByVal strBranchCode As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetBANK

        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL As String
        Dim dtBANK As DataTable

        oledbconnect.ConnectionString = strCAPSILConn

        ' Bank code empty, return all bank
        If strBankCode = "" OrElse strBankCode = "000" Then
            strSQL = "Select DISTINCT BFBANK as Code, BFBKNM as Name " & _
                " From " & BANKFIL
        End If

        ' Bank code not empty, return all branches for a bank
        If strBankCode <> "" And strBankCode <> "000" Then
            strSQL = "Select BFBRCH as Code, BFBRNM as Name, BFIND, BFBKNM " & _
                " From " & BANKFIL & _
                " Where BFBANK = '" & strBankCode & "'"

            If strBranchCode <> "" Then
                strSQL &= " And BFBRCH = '" & strBranchCode & "'"
            End If
        End If

        Try

            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            dtBANK = New DataTable("BANK")
            oledbda.Fill(dtBANK)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetBANK - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetBANK - " & ex.ToString
            Return Nothing
        End Try

        Return dtBANK

    End Function

    Public Function GetDISC(ByVal strPolicy As String, ByVal strCov As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetDISC

        Dim stMQ As New DISC

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String

        itfMQ = objMQ
        stMQ.strFunc_5 = cDISC
        stMQ.strOption_1 = "B"
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strCov_2 = strCov

        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("DISC", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                intCurLoc = 2
            Else
                intCurLoc = 1
            End If

            Dim strReply As String
            strReply = itfMQ.MQReplyMSG
            If strReply.Substring(intCurLoc, 1) = "|" Then
                Return Nothing
                Exit Function
            End If

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg, 999, "|")

            If lngErrNo = 0 Then
                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 2")
                ParseMQMsg(MQdt(1), dr, 0, intCurLoc, lngErrNo, strErrMsg, 1)
            End If

            If lngErrNo = 0 AndAlso intCurLoc < intTtlLen Then
                dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 3")
                ParseMQMsg(MQdt(2), dr, 0, intCurLoc, lngErrNo, strErrMsg, 999)
            End If

            If dsTemplate.Tables("csw_mq_func_header").Rows(0).Item("cswmfh_cont_flag") = "Y" Then
                If MQdt(0).Rows.Count > 0 Then
                    MQdt(0).Rows(0).Item("ContFlag") = itfMQ.MQReplyMSG.Substring(1, 1)
                    MQdt(0).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cDISC & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        If strCov = "" Then
            Return MQdt(0)
        Else
            Return MQdt(2)
        End If

    End Function

    Public Function GetIRTS(ByVal strPlan As String, ByVal strRS As String, ByVal strEffDate As Date, _
            ByVal intMTS As Integer, ByVal intDays As Integer, ByVal intMaxAmt As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetIRTS

        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL As String
        Dim dtIRTS As DataTable

        If strPlan = "" OrElse strRS = "" Then
            lngErrNo = -1
            strErrMsg = "Record not found"
            Return Nothing
        End If

        oledbconnect.ConnectionString = strCAPSILConn

        Try

            strSQL = "Select * " & _
                " From " & ORDUIR & _
                " Where FLD0001 = '  ' " & _
                " And FLD0002 = '" & strPlan & "' " & _
                " And FLD0003 = '" & strRS & "' " & _
                " And FLD0008 <= '" & CPSDate(strEffDate) & "'"

            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            dtIRTS = New DataTable("IRTS")
            oledbda.Fill(dtIRTS)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetIRTS - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetIRTS - " & ex.ToString
            Return Nothing
        End Try

        With dtIRTS.Columns
            .Add("EffDate", Type.GetType("System.DateTime"))
        End With

        Dim intRow As Integer = dtIRTS.Rows.Count - 1
        Dim i As Integer
        Dim strTel As String

        For i = 0 To intRow
            With dtIRTS.Rows(i)
                .Item("EffDate") = VBDate(.Item("FLD0008"))
            End With
        Next

        Return dtIRTS

    End Function

    Public Function GetPRTS(ByVal strPlan As String, ByVal strRS As String, ByVal strSmoke As String, ByVal strPar As String, _
            ByVal strSex As String, ByVal strTBL1 As String, ByVal strTBL2 As String, ByVal intAge As Integer, _
            ByVal intDur As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetPRTS

        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL As String
        Dim dtPRTS As DataTable

        If strPlan = "" OrElse strRS = "" OrElse strSmoke = "" OrElse strPar = "" OrElse strSex = "" Then
            lngErrNo = -1
            strErrMsg = "Record not found"
            Return Nothing
        End If

        oledbconnect.ConnectionString = strCAPSILConn

        Try

            strSQL = "Select * " & _
                " From " & ORDUPR & _
                " Where PRCOMP = '  ' " & _
                " And PRCODE = '" & strPlan & "' " & _
                " And PRSCLE = '" & strRS & "' " & _
                " And PRSMK = '" & strSmoke & "' " & _
                " And PRPAR = '" & strPar & "' " & _
                " And PRSEX = '" & strSex & "' " & _
                " And PRSTB1 = '" & strTBL1 & "' " & _
                " And PRSTB2 = '" & strTBL2 & "' " & _
                " And PRDUR = " & intDur
            If intAge > 0 Then
                strSQL &= " And PRAGE = " & intAge
            End If

            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            dtPRTS = New DataTable("PRTS")
            oledbda.Fill(dtPRTS)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetPRTS - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetPRTS - " & ex.ToString
            Return Nothing
        End Try

        Return dtPRTS

    End Function

    Public Function GetUTRH(ByVal strPolicy As String, ByVal datEnq As Date, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable() Implements [Interface].ICS2005.GetUTRH

        Dim stMQ As New UTRH

        Dim MQdt() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")

        Dim strMsg As String

        itfMQ = objMQ
        stMQ.strFunc_5 = cUTRH
        stMQ.strPolicy_10 = RTrim(strPolicy)
        stMQ.strDate_7 = CPSDate(datEnq)

        'AC - Remove advance compilation option - Start
        '#If UAT <> 0 Then
        '                stMQ.strPriceDate_7 = CPSDate(#12/1/2006#)
        '#Else
        '        stMQ.strPriceDate_7 = CPSDate(Today)
        '#End If
        stMQ.strPriceDate_7 = CPSDate(#12/1/2006#)
        'AC - Remove advance compilation option - end

        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cNTRecvQ, cCAPReplyQ, cNTReplyQ)
            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)
            itfMQ.RecvMsg()

            MQdt = GenMQDT("UTRH", lngErrNo, strErrMsg, dsTemplate)

            Dim intTtlLen, intCurLoc As Integer
            intTtlLen = itfMQ.MQReplyMSG.Length - 1

            Dim strReply As String
            strReply = itfMQ.MQReplyMSG

            intCurLoc = 1

            dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
            ParseMQMsg(MQdt(0), dr, 0, intCurLoc, lngErrNo, strErrMsg)

            If lngErrNo = 0 Then
                If MQdt(0).Rows(0).Item("FundCount") > 0 Then
                    dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 2")
                    ParseMQMsg(MQdt(1), dr, 0, intCurLoc, lngErrNo, strErrMsg, MQdt(0).Rows(0).Item("FundCount"))
                Else
                    Return Nothing
                    Exit Function
                End If
            End If

            Dim strCont As String = "N"
            If lngErrNo = 0 AndAlso intCurLoc < intTtlLen Then
                If strReply.Substring(intCurLoc, 1) = "N" Then
                    strCont = strReply.Substring(intCurLoc + 1, 1)
                    intCurLoc += 2
                    dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 3")
                    ParseMQMsg(MQdt(2), dr, 1, intCurLoc, lngErrNo, strErrMsg, 999)
                End If
            End If

            If strCont = "Y" Then
                If MQdt(2).Rows.Count > 0 Then
                    MQdt(2).Rows(0).Item("ContFlag") = "Y"
                    MQdt(2).AcceptChanges()
                End If
            End If
        Catch mqex As MQException
            strErrMsg = cUTRH & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Return MQdt

    End Function

    Public Function GetUVAL(ByVal strPlan As String, ByVal strRS As String, ByVal strSmoke As String, ByVal strPar As String, _
            ByVal strSex As String, ByVal strTBL1 As String, ByVal strTBL2 As String, ByVal intAge As Integer, _
            ByVal strRecType As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetUVAL

        Dim oledbconnect As New OdbcConnection
        Dim oledbda As OdbcDataAdapter
        Dim strSQL As String
        Dim dtUVAL As DataTable

        If strPlan = "" OrElse strRS = "" OrElse strSmoke = "" OrElse strPar = "" OrElse strSex = "" OrElse strRecType = "" Then
            lngErrNo = -1
            strErrMsg = "Record not found"
            Return Nothing
        End If

        oledbconnect.ConnectionString = strCAPSILConn

        Try

            strSQL = "Select * " & _
                " From " & ORDUUV & _
                " Where FLD0001 = '  ' " & _
                " And FLD0002 = '" & strPlan & "' " & _
                " And FLD0003 = '" & strRS & "' " & _
                " And FLD0004 = '" & strSmoke & "' " & _
                " And FLD0005 = '" & strPar & "' " & _
                " And FLD0006 = '" & strSex & "' " & _
                " And FLD0007 = '" & strTBL1 & "' " & _
                " And FLD0008 = '" & strTBL2 & "' " & _
                " And FLD0010 = " & intAge & _
                " And FLD0009 = '" & strRecType & "' "

            oledbda = New OdbcDataAdapter(strSQL, oledbconnect)
            dtUVAL = New DataTable("UVAL")
            oledbda.Fill(dtUVAL)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetUVAL - " & sqlex.ToString
            Return Nothing
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetUVAL - " & ex.ToString
            Return Nothing
        End Try

        Return dtUVAL

    End Function

    Public Function GetPoProjection(ByVal strPolicy As String, ByVal intYear As Integer, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetPoProjection
        Dim odbcconnect As New OdbcConnection
        Dim odbcda As OdbcDataAdapter
        Dim strSQL As String
        Dim dtProjection As New DataTable

        Try
            odbcconnect.ConnectionString = strCAPSILConn
            strSQL = "CALL " & cPROJ & ".gen038rp('" & strPolicy & "','" & intYear & "')"
            odbcda = New OdbcDataAdapter(strSQL, odbcconnect)
            odbcda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            odbcda.MissingMappingAction = MissingMappingAction.Passthrough
            odbcda.Fill(dtProjection)

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetProjection - " & sqlex.ToString
            Return Nothing

        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetProjection - " & ex.ToString
            Return Nothing

        End Try

        Return dtProjection

    End Function

    Public Function GetMarkin(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetMarkin

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dt As New DataTable

        strSQL = "Select convert(integer,Cswreg_markin_id) as 'Markin_ID', Cswreg_policy_no as 'Policy_no', Cswttc_desc as 'Type',  " & _
            " Cswreg_markin_date as 'Markin_Date', Cswreg_markout_date as 'Markout_Date', Cswreg_system as 'System', " & _
            " Cswtsc_desc as 'Status', Cswreg_rmk as 'Remark', Cswreg_upd_usr as 'User_ID' " & _
            " From csw_req_reg, csw_txn_type_code, csw_txn_status_code " & _
            " Where Cswreg_txn_type = Cswttc_txn_type " & _
            " And Cswreg_status = Cswtsc_txn_status " & _
            " and Cswreg_policy_no = '" & strPolicy & "' " & _
            " Order by Cswreg_markin_id desc "

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dt)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetMarkin - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetMarkin - " & ex.ToString
        End Try

        Return dt

    End Function

    Public Function GetMarkinHist(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetMarkinHist

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dt As New DataTable

        strSQL = "Select Cswregl_log_id as 'Log_id', Cswregl_action as 'Action_Code', convert(integer,Cswregl_markin_id) as 'Markin_ID', " & _
            " Cswregl_policy_no as 'Policy_no', Cswttc_desc as 'Type', Cswregl_markin_date as 'Markin_Date', " & _
            " Cswregl_markout_date as 'Markout_Date', Cswregl_system as 'System', Cswregl_status as 'Status', " & _
            " Cswregl_rmk as 'Remark', Cswregl_crt_usr as 'Create_User', Cswregl_crt_date as 'Create_date', " & _
            " Cswregl_upd_usr as 'Last_Update_User', Cswregl_upd_date as 'Last_Update_Date' " & _
            " From csw_req_reg_log, csw_txn_type_code " & _
            " Where Cswregl_txn_type = Cswttc_txn_type " & _
            " And Cswregl_policy_no = '" & strPolicy & "'"

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dt)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetMarkin - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetMarkin - " & ex.ToString
        End Try

        Return dt

    End Function

    Public Function GetPendingMarkin(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetPendingMarkin

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dt As New DataTable

        strSQL = "Select posp_markin_id, posp_policy_no, posp_indate, posp_pending_date, posp_seqno, posp_pending_code, " & _
            " posp_pending_desc, posp_resolve_code, posp_resolve_desc, posp_resolve_indicator, posp_resolve_date, " & _
            " posp_deadline, posp_remark, posp_internal_remarks, posp_first_rem_date, posp_final_rem_date " & _
            " From " & cPOS & "pos_pending " & _
            " Where posp_policy_no = '" & strPolicy & "' " & _
            " Order By posp_markin_id desc, posp_seqno desc"

        sqlconnect.ConnectionString = strPOSConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dt)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetMarkin - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetMarkin - " & ex.ToString
        End Try

        Return dt

    End Function

    Public Function GetPendingMarkinHist(ByVal strPolicy As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetPendingMarkinHist

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dt As New DataTable

        strSQL = "Select * from " & cPOS & "pos_pending_log " & _
            " Where pospl_policy_no='" & strPolicy & "'"

        sqlconnect.ConnectionString = strPOSConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dt)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetMarkin - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetMarkin - " & ex.ToString
        End Try

        Return dt

    End Function

    Public Function GetRPUQuote(ByVal strPolicy As String, ByVal strUser As String, ByVal strLastName As String, _
            ByVal strCurCode As String, ByRef dblAmount As Double, ByVal strEffDate As Date, _
            ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.GetRPUQuote

        Dim odbcConnect As New OdbcConnection
        Dim odbcCmd As OdbcCommand
        Dim strSQL As String
        Dim dtRPUQuote As DataTable
        Dim odbcDA As OdbcDataAdapter

        odbcConnect.ConnectionString = strCAPSILConn

        strSQL = "Delete from " & POS029W2 & _
            " Where oca01 = '" & RTrim(Left(strPolicy, 9)) & "' and oca01s = '" & RTrim(IIf(Len(strPolicy) = 10, Right(strPolicy, 1), "")) & "'"

        Try
            odbcConnect.Open()
            odbcCmd = New OdbcCommand
            odbcCmd.Connection = odbcConnect
            odbcCmd.CommandText = strSQL
            odbcCmd.CommandType = CommandType.Text
            odbcCmd.ExecuteNonQuery()

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "GetRPUQuote - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetRPUQuote - " & ex.ToString
        End Try

        'With rstData
        '    .AddNew()
        '    .Fields("strCode") = "Y"
        '    .Fields("strPolicyNo").Value = strPolicyNo
        '    .Fields("strLastName").Value = Left(strLastName, 4)
        '    .Fields("strCurCode").Value = CapsilCurrCode(strCurCode)
        '    .Fields("strAmount").Value = ""
        '    .Fields("strResp1").Value = "NO"
        '    .Fields("strChequeIndicator").Value = "N"
        '    .Fields("dtEffDate").Value = Format(datEffDate, "mm/dd/yyyy")
        '    '.Fields("dtMEDate").Value = "       "
        '    .Fields("strTaxWithheld").Value = "0000000.00"
        '    .Fields("strOverrideIndicator").Value = "N"
        '    .Fields("strTaxPenalty").Value = "0000000.00"
        '    .Update()
        'End With

        If lngErrNo = 0 Then

            strSQL = "Insert into " & POS029W1 & " (ica01,ica01s,ITRNID,ita1j,ipuamt,ireccd,itq1jd," & _
                     "iresp1,itb1j,iefdat,imedat,ichqin,itxhel,itxtyp,itxpen, ITBAL) values (" & _
                     "'" & RTrim(Left(strPolicy, 9)) & "', " & _
                     "'" & RTrim(IIf(Len(strPolicy) = 10, Right(strPolicy, 1), "")) & "', " & _
                     "'" & Right$(Trim(strUser), 3) & "', " & _
                     "'" & Left(strLastName, 4) & "', " & _
                     "'', " & _
                     "'Y', " & _
                     "'', " & _
                     "'NO', " & _
                     "'', " & _
                     "'" & UCase(Format(strEffDate, "ddMMMyyyy")) & "', " & _
                     "'', " & _
                     "'N'," & _
                     "'0000000.00'," & _
                     "'N'," & _
                     "'0000000.00','00000000000.00')"

            Try
                odbcCmd.CommandText = strSQL
                odbcCmd.ExecuteNonQuery()

            Catch sqlex As OdbcException
                lngErrNo = -1
                strErrMsg = "GetRPUQuote - " & sqlex.ToString
            Catch ex As Exception
                lngErrNo = -1
                strErrMsg = "GetRPUQuote - " & ex.ToString
            End Try

            Thread.Sleep(1500)

            If lngErrNo = 0 Then
                strSQL = "select rtrim(OERRSW) as strErrBit, rtrim(oerr1) as strErrMsg1, " & _
                         "TRIM(oerr2) as strErrMsg2, rtrim(otb1j) as strRPUQuote, " & _
                         "Trim(OREFN) as strCSRefund, Trim(OPUAMT) as strRBAmt " & _
                         "From " & POS029W2 & " WHERE OCA01 = '" & RTrim(Left(strPolicy, 9)) & "' AND OCA01S = '" & RTrim(IIf(Len(strPolicy) = 10, Right(strPolicy, 1), "")) & "'"

                odbcDA = New OdbcDataAdapter(strSQL, odbcConnect)
                dtRPUQuote = New DataTable

                Try
                    odbcDA.Fill(dtRPUQuote)
                Catch sqlex As OdbcException
                    lngErrNo = -1
                    strErrMsg = "GetRPUQuote - " & sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = "GetRPUQuote - " & ex.ToString
                End Try
            End If

        End If

        odbcConnect.Close()
        odbcConnect.Dispose()

        Return dtRPUQuote

    End Function

    <AutoComplete(True)> _
    Public Function LockPolicy(ByVal strPolicy As String, ByVal strUser As String, ByVal strFunc As String, ByVal strTermID As String, _
            ByVal strTransType As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean Implements [Interface].ICS2005.LockPolicy

        Dim odbcConnect As New OdbcConnection
        'Dim odbcTrans As OdbcTransaction
        Dim odbcCmd As OdbcCommand
        Dim strSQL As String
        Dim drSoftLock As OdbcDataReader

        LockPolicy = False
        odbcConnect.ConnectionString = strCAPSILConn

        strSQL = "SELECT trim(SLPONO) as SLPONO,trim(SLUSER) as SLUSER,trim(SLLKTM) as SLLKTM,trim(SLTRNT) as SLTRNT, " & _
            " trim(SLSYSM) as SLSYSM, trim(SLLKDT) as SLLKDT, trim(SLTMID) as SLTMID " & _
            " FROM " & SFTLCK & " Where SLPONO = '" & strPolicy & "'"
        Try
            odbcConnect.Open()
            'odbcTrans = odbcConnect.BeginTransaction

            odbcCmd = New OdbcCommand
            odbcCmd.Connection = odbcConnect
            odbcCmd.CommandText = strSQL
            odbcCmd.CommandType = CommandType.Text
            'odbcCmd.Transaction = odbcTrans
            drSoftLock = odbcCmd.ExecuteReader

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "LockPolicy - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "LockPolicy - " & ex.ToString
        End Try

        If lngErrNo = 0 Then

            If drSoftLock.HasRows Then

                If (Trim(drSoftLock.Item("SLUSER")) = strUser And Trim(drSoftLock.Item("SLSYSM")) = gSystem) _
                        Or DateDiff("D", Date.ParseExact(drSoftLock.Item("SLLKDT"), "yyyyMMdd", Nothing), Now) >= 1 Then

                    drSoftLock.Close()

                    strSQL = "Update " & SFTLCK & " Set SLLKTM = '" & Format(Now, "HHmmss") & "',"
                    strSQL &= "SLLKDT = '" & Format(Now, "yyyyMMdd") & "', SLTMID = '" & strTermID & "', "
                    strSQL &= "SLUSER = '" & strUser & "', SLTRNT = '" & strTransType & "', "
                    strSQL &= "SLSYSM = '" & gSystem & "' where SLPONO = '" & strPolicy & "'"

                    odbcCmd.CommandText = strSQL

                    Try
                        odbcCmd.ExecuteNonQuery()
                        'odbcTrans.Commit()

                    Catch sqlex As OdbcException
                        'odbcTrans.Rollback()
                        lngErrNo = -1
                        strErrMsg = "LockPolicy - " & sqlex.ToString
                    Catch ex As Exception
                        lngErrNo = -1
                        strErrMsg = "LockPolicy - " & ex.ToString
                    End Try

                    If lngErrNo = 0 Then
                        LockPolicy = True
                    End If

                Else
                    lngErrNo = -1
                    strErrMsg = drSoftLock.Item("SLTMID") & " (" & drSoftLock.Item("SLUSER") & "), " & drSoftLock.Item("SLSYSM") & " ," & _
                        drSoftLock.Item("SLTRNT") & ", " & Date.ParseExact(drSoftLock.Item("SLLKDT"), "yyyyMMdd", Nothing) & " " & Date.ParseExact(drSoftLock.Item("SLLKTM"), "HHmmss", Nothing).ToString("HH:mm:ss")
                    drSoftLock.Close()
                End If
            Else
                strSQL = "Insert into " & SFTLCK & " ( SLPONO , SLTMID , SLUSER , SLSYSM , SLLKDT , "
                strSQL &= " SLLKTM , SLTRNT  ) values ( '" & strPolicy & "' , '" & strTermID & "' , '" & strUser & "' , "
                strSQL &= " '" & gSystem & "' , '" & Format(Now, "yyyyMMdd") & "' , '" & Format(Now, "HHmmss") & "' , '" & strTransType & "'  ) "

                drSoftLock.Close()

                odbcCmd.CommandText = strSQL

                Try
                    odbcCmd.ExecuteNonQuery()
                    'odbcTrans.Commit()

                Catch sqlex As OdbcException
                    'odbcTrans.Rollback()
                    lngErrNo = -1
                    strErrMsg = "LockPolicy - " & sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = "LockPolicy - " & ex.ToString
                End Try

                If lngErrNo = 0 Then
                    LockPolicy = True
                End If

            End If
        End If

        odbcConnect.Close()
        odbcConnect.Dispose()

    End Function

    Public Function UnLockPolicy(ByVal strPolicy As String, ByVal strUser As String, ByVal strFunc As String, ByVal strTermID As String, ByVal strTransType As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean Implements [Interface].ICS2005.UnLockPolicy

        Dim odbcConnect As New OdbcConnection
        Dim odbcCmd As OdbcCommand
        Dim strSQL As String

        UnLockPolicy = False
        odbcConnect.ConnectionString = strCAPSILConn

        strSQL = "Delete from " & SFTLCK & _
            " Where LTRIM(RTRIM(SLPONO)) = '" & Trim(UCase(strPolicy)) & " ' and SLSYSM = '" & gSystem & _
            "' and LTRIM(RTRIM(UPPER(SLTMID))) = '" & Trim(UCase(strTermID)) & "'"

        Try
            odbcConnect.Open()
            odbcCmd = New OdbcCommand
            odbcCmd.Connection = odbcConnect
            odbcCmd.CommandText = strSQL
            odbcCmd.CommandType = CommandType.Text
            odbcCmd.ExecuteNonQuery()

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "UnLockPolicy - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "UnLockPolicy - " & ex.ToString
        End Try

        If lngErrNo = 0 Then
            UnLockPolicy = True
        End If

        odbcConnect.Close()
        odbcConnect.Dispose()

    End Function

    Public Function GetCSR(ByVal strCSRID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements [Interface].ICS2005.GetCSR

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dt As New DataTable

        strSQL = "select * from CSR "

        If strCSRID <> "" Then
            strSQL &= "Where CSRID = '" & Trim(strCSRID) & "'"
        End If

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dt)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = sqlex.Number
            strErrMsg = "GetCSR - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "GetCSR - " & ex.ToString
        End Try

        Return dt

    End Function

    Public Function UpdateCSR(ByVal strCSRID As String, ByVal strCSRName As String, ByVal strCSRCName As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean Implements [Interface].ICS2005.UpdateCSR

        Dim odbcConnect As New OdbcConnection
        Dim odbcCmd As OdbcCommand
        Dim strSQL As String
        Dim strExist As String

        UpdateCSR = False
        odbcConnect.ConnectionString = strCAPSILConn

        strSQL = "Select URINIT " & _
            " From " & USERPF & _
            " Where URINIT = '" & Right(strCSRID, 3) & "' " & _
            " And URDEPT = 'CLS'"

        Try
            odbcConnect.Open()
            odbcCmd = New OdbcCommand
            odbcCmd.Connection = odbcConnect
            odbcCmd.CommandText = strSQL
            odbcCmd.CommandType = CommandType.Text
            strExist = odbcCmd.ExecuteScalar

        Catch sqlex As OdbcException
            lngErrNo = -1
            strErrMsg = "UpdateCSR - " & sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = "UpdateCSR - " & ex.ToString
        End Try

        If lngErrNo = 0 Then

            If IsDBNull(strExist) OrElse strExist Is Nothing OrElse strExist = "" Then
                strSQL = "Insert into " & USERPF & " (URINIT, URDEPT, URNAME, URTITL) " & _
                    " Values ('" & Right(strCSRID, 3) & "','CLS','" & strCSRName & "','CUSTOMER SERVICE REPRESENTATIVE')"

                odbcCmd.CommandText = strSQL

                Try
                    odbcCmd.ExecuteNonQuery()
                Catch sqlex As OdbcException
                    lngErrNo = -1
                    strErrMsg = "UpdateCSR - " & sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = "UpdateCSR - " & ex.ToString
                End Try

                If lngErrNo = 0 Then
                    UpdateCSR = True
                End If
            Else
                UpdateCSR = True
            End If

        End If

        odbcConnect.Close()
        odbcConnect.Dispose()

    End Function

    Public Function ExecuteScript(ByVal strScript As String, ByVal strSource As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable Implements [Interface].ICS2005.ExecuteScript

        Dim dtInfo As DataTable
        dtInfo = GetSystemInfo(lngErrNo, strErr)
        strQryTimeOut = dtInfo.Rows(0).Item("cswsi_query_timeout")

        Dim sqlda As SqlDataAdapter
        Dim sqlConnect As SqlConnection

        Dim odbcda As OdbcDataAdapter
        Dim odbcConnect As OdbcConnection

        Dim sqlcmd As SqlCommand

        Dim dtTempResult As DataTable

        Select Case RTrim(strSource)

            Case "CAPSIL", "CAPSIL1", "CAPSIL2", "CAPSIL3"
                odbcConnect = New OdbcConnection
                odbcConnect.ConnectionString = strCAPSILConn & ";QueryTimeOut=0"
                odbcda = New OdbcDataAdapter(strScript, odbcConnect)
                dtTempResult = New DataTable

                Try
                    odbcda.Fill(dtTempResult)
                Catch sqlex As OdbcException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "CAM", "CAM1"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strCIWConn
                'sqlda = New SqlDataAdapter(strScript, sqlConnect)
                sqlda = New SqlDataAdapter
                sqlcmd = New SqlCommand
                sqlcmd.CommandTimeout = strQryTimeOut
                sqlcmd.CommandText = strScript
                sqlcmd.Connection = sqlConnect
                sqlda.SelectCommand = sqlcmd

                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "CIW", "CUST", "CIW2", "CIW3", "CIW4", "CIW5"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strCIWConn & ";Connect Timeout=900"
                'sqlda = New SqlDataAdapter(strScript, sqlConnect)
                sqlda = New SqlDataAdapter
                sqlcmd = New SqlCommand
                sqlcmd.CommandTimeout = strQryTimeOut
                sqlcmd.CommandText = strScript
                sqlcmd.Connection = sqlConnect
                sqlda.SelectCommand = sqlcmd
                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "MCS", "MCS1"

            Case "ICR", "ICR1"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strICRConn & ";Connect Timeout=900"
                'sqlda = New SqlDataAdapter(strScript, sqlConnect)
                sqlda = New SqlDataAdapter
                sqlcmd = New SqlCommand
                sqlcmd.CommandTimeout = strQryTimeOut
                sqlcmd.CommandText = strScript
                sqlcmd.Connection = sqlConnect
                sqlda.SelectCommand = sqlcmd
                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try
        End Select

        sqlda = Nothing
        odbcda = Nothing
        sqlConnect = Nothing
        odbcConnect = Nothing

        Return dtTempResult

    End Function

    Public Function UpdateData(ByVal strScript As String, ByVal strUType As String, ByVal strSource As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As String Implements [Interface].ICS2005.UpdateData

        Dim dtInfo As DataTable
        dtInfo = GetSystemInfo(lngErrNo, strErr)
        strQryTimeOut = dtInfo.Rows(0).Item("cswsi_query_timeout")

        Dim strVal As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        Select Case Trim(strSource)
            Case "CAPSIL"
                sqlconnect.ConnectionString = strCAPSILConn
            Case "ICR"
                sqlconnect.ConnectionString = strICRConn
            Case Else
                sqlconnect.ConnectionString = strCIWConn & ";Connect Timeout=900"
        End Select

        sqlconnect.Open()
        sqlcmd.Connection = sqlconnect
        sqlcmd.CommandTimeout = strQryTimeOut
        sqlcmd.CommandText = strScript
        Try
            If strUType = "S" Then
                strVal = sqlcmd.ExecuteScalar
            Else
                strVal = sqlcmd.ExecuteNonQuery
            End If
        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.ToString
            lngErrNo = -1
        Catch ex As Exception
            strErrMsg = ex.ToString
            lngErrNo = -1
        Finally
            sqlconnect.Close()
            sqlconnect = Nothing
            sqlcmd = Nothing
        End Try

        UpdateData = strVal

    End Function

    Function GetGIPSEAPolicy(ByVal strPolicy As String, ByVal strReCnt As String, ByVal strClient As String, ByVal strType As String, _
            ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable() Implements [Interface].ICS2005.GetGIPSEAPolicy

        Dim MQdt(), dtTmp() As DataTable
        Dim dr() As DataRow
        Dim dsTemplate As New DataSet("MQTemplate")
        Dim strMQMsg() As String
        Dim blnExit = False
        Dim intMQMsgCnt As Integer = -1

        Dim strMsg As String
        Dim stMQ As New GIPSEA

        stMQ.strPolicy_16 = RTrim(strPolicy)
        stMQ.strRen_3 = Trim(strReCnt).PadLeft(3, "0")
        stMQ.strClient_10 = Trim(strClient).PadLeft(10, "0")
        stMQ.strType_1 = strType

        itfMQ = objMQ
        strMsg = MQStruToString(stMQ)

        Call SetMQVar()

        Try

            itfMQ.Init(cMQBufferSize, cNTQMgr, cGINTRecvQ, cGICAPReplyQ, cGINTReplyQ)

            itfMQ.ConnectMQ()
            itfMQ.SendMsg(strMsg)

            Do
                itfMQ.RecvMsg()
                intMQMsgCnt += 1
                ReDim Preserve strMQMsg(intMQMsgCnt)
                strMQMsg(intMQMsgCnt) = itfMQ.MQReplyMSG

                If Left(strMQMsg(intMQMsgCnt), 2) = "EJ" Then
                    intMQMsgCnt -= 1
                    blnExit = True
                End If
                itfMQ.Init(cMQBufferSize, cNTQMgr, cGINTRecvQ, cGICAPReplyQ, cGINTReplyQ)
            Loop While Not blnExit

        Catch mqex As MQException
            strErrMsg = "GetGIPSEAPolicy" & " - " & mqex.ToString
            lngErrNo = -1
            Exit Function
        Finally
            itfMQ.DisConnect()
        End Try

        Dim strSQL As String
        Dim dtGIMsgSize As DataTable

        strSQL = "Select cswmfd_func, sum(cswmfd_field_size) as size " & _
            " From csw_mq_func_detail " & _
            " Where cswmfd_func like 'GI%' " & _
            " Group by cswmfd_func " & _
            " Order by cswmfd_func"
        dtGIMsgSize = ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)

        If lngErrNo = 0 Then
            For i As Integer = 0 To intMQMsgCnt

                ReDim Preserve MQdt(i)

                dsTemplate = New DataSet("MQTemplate")
                dtGIMsgSize.DefaultView.RowFilter = "Size = " & strMQMsg(i).Length

                If dtGIMsgSize.DefaultView.Count > 0 Then

                    dtTmp = GenMQDT(dtGIMsgSize.DefaultView.Item(0).Item("cswmfd_func"), lngErrNo, strErrMsg, dsTemplate)

                    Dim intTtlLen, intCurLoc As Integer
                    intTtlLen = strMQMsg(i).Length

                    intCurLoc = 1

                    dr = dsTemplate.Tables("csw_mq_func_detail").Select("cswmfd_func_seq = 1")
                    ParseMQMsg(dtTmp(0), dr, 0, intCurLoc, lngErrNo, strErrMsg, 1, "", "N" & strMQMsg(i))

                    If lngErrNo = 0 Then
                        MQdt(i) = dtTmp(0).Copy
                        MQdt(i).Columns.Add("MsgType", Type.GetType("System.String"))
                        MQdt(i).Rows(0).Item("MsgType") = dtGIMsgSize.DefaultView.Item(0).Item("cswmfd_func")
                        MQdt(i).AcceptChanges()
                    Else
                        MQdt(i) = Nothing
                    End If
                Else
                    MQdt(i) = Nothing
                End If

            Next
        Else
            MQdt = Nothing
        End If

        Return MQdt

    End Function

End Class
