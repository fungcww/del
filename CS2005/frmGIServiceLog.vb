Imports System.Data.SqlClient

Public Class frmGIServiceLog

    Private strPolicyAccountID As String
    Private strCustomerID As String
    Private strPolicyType As String

    Public Property PolicyAccountID() As String
        Get
            Return strPolicyAccountID
        End Get
        Set(ByVal Value As String)
            strPolicyAccountID = Value
        End Set
    End Property

    Public Property CustomerID() As String
        Get
            Return strCustomerID
        End Get
        Set(ByVal Value As String)
            strCustomerID = Value
        End Set
    End Property

    Public Property PolicyType() As String
        Get
            Return strPolicyType
        End Get
        Set(ByVal Value As String)
            strPolicyType = Value
        End Set
    End Property

    Private Sub frmGIServiceLog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.UclServiceLog1.PendingSave = True Then
            MsgBox("There are unsaved service log records, please save it first.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gSystem)
            e.Cancel = True
        Else
            wndMain.StatusBarPanel1.Text = ""
            wndMain.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub frmGIServiceLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.UclServiceLog1.PolicyAccountID(strCustomerID) = strPolicyAccountID

        Me.UclServiceLog1.PolicyType = strPolicyType
        If Microsoft.VisualBasic.Left(strPolicyAccountID, 2) = "GL" Then
            Me.txtPolicyNo.Text = Microsoft.VisualBasic.Left(Mid(strPolicyAccountID, 3), 12)
        Else
            Me.txtPolicyNo.Text = Microsoft.VisualBasic.Left(strPolicyAccountID, 12)
        End If
        PolicyAlert()

    End Sub

    Private Sub PolicyAlert()
        Dim strAlertTitle As String
        Dim strSQL As String
        Dim sqlConn As New SqlConnection
        Dim dtBDay As DateTime
        Dim strCSR As String
        Dim strPolicyNo As String = strPolicyAccountID
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If strPolicyType = "GL" AndAlso Microsoft.VisualBasic.Left(strPolicyNo, 2) = "12" Or Microsoft.VisualBasic.Left(strPolicyNo, 2) = "22" _
                          Or Microsoft.VisualBasic.Left(strPolicyNo, 2) = "13" Or Microsoft.VisualBasic.Left(strPolicyNo, 2) = "23" Then
            strPolicyNo = strPolicyType & strPolicyNo
        End If


        sqlConn.ConnectionString = strCIWConn
        sqlConn.Open()
        strSQL = "Select s.AlertNotes, s.EventInitialDatetime, s.MasterCSRID, cs.Name " &
                 "From ServiceEventDetail s left outer join " & serverPrefix & "CSR cs " &
                 "on s.MasterCSRID = cs.CSRID " &
                 "Where s.policyaccountid = '" & RTrim(strPolicyNo) & "' and s.IsPolicyAlert = 'Y' " &
                 "Order by s.EventInitialDatetime desc "
        Dim sqlCmd As New SqlCommand(strSQL, sqlConn)

        ' 7/17/2006 - increase timeout period
        sqlCmd.CommandTimeout = gQryTimeOut
        ' End

        Try
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            While sqlReader.Read()
                If IsDBNull(sqlReader.Item("Name")) Then
                    strCSR = "unknown user (" & sqlReader.Item("MasterCSRID") & ")"
                Else
                    strCSR = sqlReader.Item("Name")
                End If
                strAlertTitle = "Alert from " & strCSR & " on " & Format(sqlReader.Item("EventInitialDatetime"), "dd MMM yyyy") & ". "
                MsgBox(sqlReader.Item("AlertNotes"), MsgBoxStyle.Information, strAlertTitle)
            End While
        Catch sqlEx As SqlException
            MsgBox("SQL Exception: " & CStr(sqlEx.Number) & " - " & sqlEx.Message, MsgBoxStyle.Exclamation, gSystem)
        Catch ex As Exception
            MsgBox("Exception: " & CStr(ex.Message), MsgBoxStyle.Exclamation, gSystem)
        End Try

        sqlConn.Dispose()
        sqlCmd.Dispose()

    End Sub
End Class