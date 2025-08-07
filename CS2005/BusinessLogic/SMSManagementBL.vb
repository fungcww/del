Imports System.IO
Imports System.Text
Imports System
Imports System.Data.SqlClient

Public Class SMSManagementBL

    Dim sqlConn As New SqlConnection

    Public Function GetSMSTemplate(ByRef dtSmsTemp As DataTable, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWConn
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        Dim strSQL As String = "select Uuid, Description as [EngText], Value as [ChiText] from " & serverPrefix & "CodeTable where Code = 'MsgTemp'"
        Try
            Dim daSmsTemplate As New SqlDataAdapter(strSQL, sqlConn)
            sqlConn.Open()
            daSmsTemplate.Fill(dtSmsTemp)
            If (dtSmsTemp Is Nothing) Then
                Throw New Exception("Cannot get SMS Template")
                Return False
            End If
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function InsertSMSTemplate(ByVal strEngMsg As String, ByVal strChiMsg As String, ByVal strUser As String, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = String.Empty
        strErr = String.Empty
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        strSQL = "insert into " & serverPrefix & "CodeTable(Code,Description,Value,CreateDate,CreateUser,LstUpdDate,LstUpdUser) values('MSGTEMP', "
        strSQL += "'" + strEngMsg.Trim + "',N'" + strChiMsg.Trim + "',GETDATE(),'" + strUser + "',GETDATE(),'" + strUser + "')"
        Try
            Dim sqlCommand As New SqlCommand(strSQL, sqlConn)
            sqlConn.Open()
            sqlCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

    Public Function UpdateSMSTemplate(ByVal ChangeAction As String, ByVal strEngMsg As String, ByVal strChiMsg As String, ByVal strUser As String, ByVal strUuid As String, ByRef strErr As String) As Boolean
        sqlConn.ConnectionString = strCIWConn
        Dim strSQL As String = String.Empty
        strErr = String.Empty
        'oliver 2024-7-11 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        strSQL += "Update " & serverPrefix & " CodeTable "
        If ChangeAction.ToUpper() = "UPDATE" Then
            strSQL += "set Description=N'" + strEngMsg.Trim + "',Value=N'" + strChiMsg.Trim + "',"
        Else
            'Delete SMS template is only don't let the SMS page selected the message but the SMS template is still kept in DB 
            strSQL += "Set Code='MSGTEMPOLD', "
        End If
        strSQL += "LstUpdDate=GETDATE(),LstUpdUser='" + strUser + "' "
        strSQL += "where uuid='" + strUuid.Trim + "'"
        Try
            Dim sqlCommand As New SqlCommand(strSQL, sqlConn)
            sqlConn.Open()
            sqlCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            strErr = ex.Message
            Return False
        Finally
            sqlConn.Close()
        End Try
    End Function

End Class
