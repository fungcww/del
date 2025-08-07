Imports System.EnterpriseServices
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Runtime.Serialization
Imports Interop
Imports System

<Serializable()> _
Public Class MQException

    Inherits System.ApplicationException

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

    Public Overrides Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.GetObjectData(info, context)
    End Sub

End Class

Public Interface IMQConnect
    ReadOnly Property MQReplyMSG() As String
    ReadOnly Property MQReply() As Long
    ReadOnly Property MQMsgID() As String
    Sub Init(ByVal msgSize As Long, ByVal NTQMgr As String, ByVal NTRecvQ As String, _
            ByVal CAPReplyQ As String, ByVal NTReplyQ As String)
    Sub ConnectMQ()
    Sub SendMsg(ByVal strMsg As String)
    Sub RecvMsg(Optional ByVal pMsgID As String = "")
    Sub CloseQueue()
    Sub DisConnect()
End Interface

Public Class MQConnect
    Inherits ServicedComponent

    Implements IMQConnect

    Public c As System.IO.BinaryReader

    Private a As MQSendRec.MQServerSendRecv
    Private b As MQSendRec.IMQSendRecv
    Private rtn, rtnobj, rtnoutobj, lngRet, reply, lngMsgSize As Long
    Private strNTQMgr, strNTRecvQ, strCAPReplyQ, strNTReplyQ, strMsgID, strSendMSG, strReplyMSG As String
    Private strFunc, strErr As String
    Private lngErrNo As Long

    Public Sub Init(ByVal msgSize As Long, ByVal NTQMgr As String, ByVal NTRecvQ As String, _
            ByVal CAPReplyQ As String, ByVal NTReplyQ As String) Implements IMQConnect.Init

        strNTQMgr = NTQMgr
        strNTRecvQ = NTRecvQ
        strCAPReplyQ = CAPReplyQ
        strNTReplyQ = NTReplyQ

        'strNTQMgr = scQMgr
        'strNTRecvQ = scRecvQ
        'strCAPReplyQ = scReplyQ
        'strNTReplyQ = scNTReplyQ

        lngMsgSize = msgSize
    End Sub

    Public Sub ConnectMQ() Implements IMQConnect.ConnectMQ

        a = CreateObject("MQSendRec.MQServerSendRecv")
        b = a

        Try
            Call b.Connect(rtn, strNTQMgr)
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            Throw New MQException(strErr)
        End Try

    End Sub

    Public Sub SendMsg(ByVal strMsg As String) Implements IMQConnect.SendMsg

        If strMsg.Length <> 0 Then
            strFunc = strMsg.Substring(0, 5).Trim
        End If

        Try
            lngRet = b.OpenQueue(rtn, strNTRecvQ, MQSendRec.OpenType.opmOutput, rtnobj)
            'lngRet = b.Send(rtn, rtnobj, strMsg, strMsgID, , strCAPReplyQ, MQSendRec.CodedChar.codeChin, MQSendRec.VersionNum.ver1, MQSendRec.MsgType.mstNone)
            lngRet = b.Send(rtn, rtnobj, strMsg, strMsgID, , strCAPReplyQ, MQSendRec.CodedChar.codeDefault, MQSendRec.VersionNum.ver1, MQSendRec.MsgType.mstNone)
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            Throw New MQException(strErr)
        End Try

    End Sub

    Public Sub RecvMsg(Optional ByVal pMsgID As String = "") Implements IMQConnect.RecvMsg

        Dim lngTSize As Long
        Dim CompleteCode As Long

        lngTSize = lngMsgSize

        ' if no msgid, use current msgid
        If pMsgID = "" Then
            pMsgID = strMsgID
        End If

        Try
            'Call b.OpenQueue(rtn, strNTReplyQ, MQSendRec.OpenType.opmInputDefault, rtnoutobj, MQSendRec.VersionNum.ver2, reply)
            CompleteCode = b.OpenQueue(rtn, strNTReplyQ, MQSendRec.OpenType.opmInputDefault, rtnoutobj, MQSendRec.VersionNum.ver2, reply)

            If CompleteCode <> 0 Then
                Call CloseQueue()
                strErr = "Cannot Connect to MQ: " & strNTReplyQ & "," & CStr(reply)
                Throw New MQException(strErr)
            End If

            'Call b.Receive(rtn, rtnoutobj, pMsgID, strReplyMSG, lngMsgSize, MQSendRec.MatchOption.CorrID, MQSendRec.CodedChar.codeChin, MQSendRec.VersionNum.ver2, reply)
            'Call b.Receive(rtn, rtnoutobj, pMsgID, strReplyMSG, lngTSize, MQSendRec.MatchOption.CorrID, MQSendRec.CodedChar.codeDefault, MQSendRec.VersionNum.ver2, reply)
            CompleteCode = b.Receive(rtn, rtnoutobj, pMsgID, strReplyMSG, lngTSize, MQSendRec.MatchOption.CorrID, MQSendRec.CodedChar.codeDefault, MQSendRec.VersionNum.ver2, reply)

            If CompleteCode <> 0 Then
                Call CloseQueue()
                strErr = "Cannot Connect to MQ: " & strNTReplyQ & "," & CStr(reply)
                Throw New MQException(strErr)
            End If

        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = reply
            Throw New MQException(strErr)
        Finally
            Call CloseQueue()
        End Try

        ' If reply <>0, MQ system problem
        If reply <> 0 Then
            lngErrNo = reply
            Select Case reply
                Case 2033   ' no msg
                    'Throw New MQException(reply & ": No Message to retrieve.")
                    ' For MQ Error 2033, it may be caused by MQ too busy, try to receive again before throw an exception.
                    reply = 0
                    strReplyMSG = ""
                    lngTSize = lngMsgSize

                    Try
                        Call b.OpenQueue(rtn, strNTReplyQ, MQSendRec.OpenType.opmInputDefault, rtnoutobj, MQSendRec.VersionNum.ver2, reply)
                        'Call b.Receive(rtn, rtnoutobj, pMsgID, strReplyMSG, lngMsgSize, MQSendRec.MatchOption.CorrID, MQSendRec.CodedChar.codeChin, MQSendRec.VersionNum.ver2, reply)
                        Call b.Receive(rtn, rtnoutobj, pMsgID, strReplyMSG, lngTSize, MQSendRec.MatchOption.CorrID, MQSendRec.CodedChar.codeDefault, MQSendRec.VersionNum.ver2, reply)
                    Catch ex As Exception
                        strErr = ex.ToString
                        lngErrNo = reply
                        Throw New MQException(strErr)
                    Finally
                        Call CloseQueue()
                    End Try

                    If reply = 2033 Then
                        Throw New MQException(reply & ": No Message to retrieve.")
                    End If

                Case 2080   ' buffer too small
                    Throw New MQException(reply & ": Buffer too small.")
                Case Else
                    Throw New MQException("MQ Error: " & reply)
            End Select
        Else
            Call CloseQueue()
        End If

        'If strReplyMSG.Substring(0, 1) <> "N" Then
        '    lngErrNo = reply
        '    strErr = strReplyMSG.Substring(1)
        '    Throw New MQException(strErr)
        '    Exit Sub
        'End If

    End Sub

    Public Sub CloseQueue() Implements IMQConnect.CloseQueue

        Try
            lngRet = b.CloseQueue(rtn, rtnobj)
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            Throw New MQException(strErr)
        End Try

    End Sub

    Public Sub DisConnect() Implements IMQConnect.DisConnect

        Try
            Call b.Disconnect(rtn)
            a = Nothing
            b = Nothing
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            Throw New MQException(strErr)
        End Try

    End Sub

    Public ReadOnly Property MQReplyMSG() As String Implements IMQConnect.MQReplyMSG
        Get
            'Return Me.MQMsgToDT
            Return strReplyMSG
        End Get
    End Property

    Public ReadOnly Property MQReply() As Long Implements IMQConnect.MQReply
        Get
            Return reply
        End Get
    End Property

    Public ReadOnly Property MQMsgID() As String Implements IMQConnect.MQMsgID
        Get
            Return strMsgID
        End Get
    End Property

End Class