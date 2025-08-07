Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.EnterpriseServices
Imports System.Security.Cryptography
Imports ItfCS2005
Imports INGLife.Security

Public Class clsUtility
    Inherits ServicedComponent

    Implements IUtility

    Private strErr As String
    Private lngErrNo As Long
    Private objSec As ISecurity

    Public Sub New()
        objSec = New INGLife.Security.DbSec
    End Sub

    Public Function VBDate(ByVal strDate As String) As Date Implements IUtility.VBDate
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
            Throw New MQException(strErr)
        End Try

    End Function

    Public Function CPSDate(ByVal datVal As Date, Optional ByVal intBase As Integer = 1800) As String Implements IUtility.CPSDate
        Dim y As String

        Try
            Return CStr(CInt(datVal.ToString("yyyyMMdd")) - intBase * 10000).Substring(1)
        Catch ex As Exception
            strErr = ex.ToString
            lngErrNo = -1
            Throw New MQException(strErr)
        End Try

    End Function

    Public Function MQStruToString(ByVal objStru As Object) As String Implements IUtility.MQStruToString

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
        ByRef strErrMsg As String) As Integer Implements IUtility.GetUPSGroup

        Dim strSQL As String
        Dim intGrpNo As Integer
        Dim sqlconnect As New SqlConnection

        Try
            strSQL = "select upsult_usr_grp from " & UPS_USER_LIST_TAB & _
                " where upsult_sys_abv = '" & strSystem & "'" & _
                " and upsult_usr_id = '" & strUser & "'"

            'sqlconnect.ConnectionString = ConnStr("CSW", "CS2005", "CIW")
            sqlconnect.ConnectionString = "packet size=4096;user id=acsowner;data source=hksqldev3;persist security info=True;initial catalog=profile;password=ownpapa"
            Dim sqlcmd As New SqlCommand(strSQL, sqlconnect)
            sqlconnect.Open()
            intGrpNo = sqlcmd.ExecuteScalar

        Catch err As SqlClient.SqlException
            lngErrNo = err.Number
            strErrMsg = err.ToString()

        Finally
            sqlconnect.Close()

        End Try

        Return intGrpNo

    End Function

    Public Function GetPrivRS(ByVal intGroupID As Integer, ByVal strCtrl As String, ByRef lngErrNo As Long, _
            ByRef strErrMsg As String, Optional ByVal strType As String = "") As DataTable Implements IUtility.GetPrivRS

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As New SqlDataAdapter
        Dim sqldt As DataTable = New DataTable(CSW_USER_PRIVS)

        Try
            strSQL = "select * from " & CSW_USER_PRIVS & _
                " where cswup_group_id = " & intGroupID & _
                " and cswup_parent = '" & strCtrl.Trim & "'"

            If strType <> "" Then
                strSQL &= " and cswup_type = '" & strType.Trim & "'"
            End If

            sqlconnect.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.Add
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.FillSchema(sqldt, SchemaType.Source)
            sqlda.Fill(sqldt)

        Catch err As SqlClient.SqlException
            lngErrNo = err.Number
            strErrMsg = err.ToString()
        Finally
            sqlconnect.Close()
        End Try

        Return sqldt

    End Function

    Public Function GetSystemInfo(ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable Implements IUtility.GetSystemInfo

        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dt As New DataTable

        strSQL = "Select * from csw_system_info"

        sqlconnect.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dt)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = sqlex.Number
            strErrMsg = sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = ex.ToString
        End Try

        Return dt

    End Function

End Class