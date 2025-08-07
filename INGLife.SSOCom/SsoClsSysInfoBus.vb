Imports System.Data.SqlClient
Imports System.EnterpriseServices
Imports System.Runtime.Serialization
Imports INGLife.Interface

<Assembly: ApplicationName("SSOComNet")> 
<Assembly: ApplicationActivation(ActivationOption.Server)> 
<Assembly: ApplicationAccessControl(True)> 

Public Class SsoClsSysInfoBus

    Inherits ServicedComponent

    Implements ISSOCom

    Private objSec As INGLife.Interface.ISecurity
    Private lngErrNo As Long
    Private strErr As String


    Public Function CheckAppPassword(ByVal strUser As String, ByRef strMessage As String, ByVal strApp As String, ByVal strPwd As String) As Boolean Implements [Interface].ISSOCom.CheckAppPassword

        Dim HashedPwd As Object
        Dim strSQL, strSSOConn, strDBPwd As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable = New DataTable("UPS")

        HashedPwd = CreateObject("CAPICOM.HashedData")
        CheckAppPassword = False

        If strUser Is Nothing OrElse Trim(strUser) = "" Then
            strMessage = "Username is Empty."
            Exit Function
        End If

        If strPwd Is Nothing OrElse Trim(strPwd) = "" Then
            strMessage = "Password is Empty."
            Exit Function
        End If

        If strApp Is Nothing OrElse Trim(strApp) = "" Then
            strMessage = "Application Name is Empty."
            Exit Function
        End If

        strSQL = "SELECT * FROM hksqluat3.profile.dbo.ups_user_psw_tab " & _
            " WHERE upsupt_usr_id = '" & UCase(strUser) & "' " & _
            " AND upsupt_sys_abv = '" & UCase(strApp) & "'"

        Try
            objSec = New INGLife.DBAccess.Database
            strSSOConn = objSec.ConnStr("CS2005", "CIW", "CSUSER")

            'HashedPwd.Algorithm = CAPICOM.CAPICOM_HASH_ALGORITHM.CAPICOM_HASH_ALGORITHM_SHA1
            HashedPwd.Algorithm = 0
            HashedPwd.Hash(strPwd)

            sqlconnect.ConnectionString = strSSOConn

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough

            sqlda.Fill(sqldt)

        Catch ex As Exception
            strMessage = ex.ToString
        Finally
            sqlconnect.Close()
        End Try

        sqlda.Dispose()
        sqlconnect.Dispose()

        If Not sqldt Is Nothing AndAlso sqldt.Rows.Count = 0 Then
            strMessage = "No such user " & strUser & _
                         " is registered for application " & strApp & _
                         vbCrLf & strMessage
        Else
            If HashedPwd.Value = sqldt.Rows(0).Item("upsupt_usr_psw") Then
                CheckAppPassword = True
            End If
        End If

        HashedPwd = Nothing

    End Function
End Class
