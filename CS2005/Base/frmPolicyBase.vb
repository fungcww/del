Imports System.Data.SqlClient
Public Class frmPolicyBase
	Inherits System.Windows.Forms.Form

    Private dtPAYH, dtCOUH, dtAPLH, dtTRNH As DataTable
    Private lngErr As Long = 0
    Private strErr As String = ""
    Private strPolicy, strCustID As String
    Private WithEvents w As New clsMQWorker
    Private dtPolSum, dtPolMisc, dtPolAddr, dtCoverage(1), dtCustHist, dtCCDR, dtDDA, dtDCAR, dtUWInf, dtNA As DataTable
    Private blnPOINFO As Boolean = False
    Private blnExit, blnSrvLogLoad, blnAgtInfoLoad, blnUWLoad, blnShowSrvLog, blnCanClose As Boolean

	Public CompanyID As String = "ING"
    Public isAssurance As Boolean = False ' Added by Hugo Chan on 2021-05-14, "CRS - First Level of Access", declare this flag for skipping some life asia and capsil function calls
    Public sCompanyID As String = "MCU"
    Public isLifeAsia As Boolean = False
    Public isProposal As Boolean = False
    Public dtCashV() As DataTable    
    Public objMQQueHeader As Utility.Utility.MQHeader
    Public objDBHeader As Utility.Utility.ComHeader
    Public blnCanView As Boolean = False
    
    Friend WithEvents txtPolicy As System.Windows.Forms.TextBox
    Friend WithEvents txtEPolicy As System.Windows.Forms.TextBox
    Friend WithEvents Ctrl_ChgComponent1 As POSCommCtrl.Ctrl_ChgComponent


    'Public Property PolicyNo(ByVal strTitle As String, ByVal strLastName As String, ByVal strFirstName As String, _
    '        ByVal strChiName As String, ByVal strProduct As String, ByVal strStatus As String) As String
    Public Property PolicyAccountID(Optional ByVal blnShow As Boolean = False) As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            strLastPolicy = Value
            strPolicy = Value
            blnShowSrvLog = blnShow
            Me.txtPolicy.Text = Value
            'If Not IsDBNull(strTitle) Then Me.txtTitle.Text = strTitle
            'If Not IsDBNull(strLastName) Then Me.txtLastName.Text = strLastName
            'If Not IsDBNull(strFirstName) Then Me.txtFirstName.Text = strFirstName
            'If Not IsDBNull(strChiName) Then Me.txtChiName.Text = strChiName
            'If Not IsDBNull(strProduct) Then Me.txtProduct.Text = strProduct
            'If Not IsDBNull(strStatus) Then Me.txtStatus.Text = strStatus

            'Alex Th Lee 20201013 [ITSR2281 - ePolicy]
            loadEPolicyFlag(Value) '
        End Set
    End Property

    Public Property CustomerID() As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            strCustID = Value
        End Set
    End Property

    Public ReadOnly Property NoRecord() As Boolean
        Get
            Return blnExit
        End Get
    End Property

    Private Sub loadEPolicyFlag(ByVal sPolicy As String)
        Dim POSDB As String = gcPOS
        Try
            Dim ds As DataSet = New DataSet("ePolicy")
            Dim sqlconnect As New SqlConnection
            Dim sqlda As SqlDataAdapter

            sqlconnect.ConnectionString = GetConnectionStringByCompanyID(CompanyID)
            Dim command = New SqlCommand("select ePolicyIndicator, PolicyId from " & POSDB & "PolicyEstatement nolock where PolicyId = @sPolicy1", sqlconnect)
            command.Parameters.AddWithValue("@sPolicy1", sPolicy)
            sqlda = New SqlDataAdapter(command)
            Try
                ds.Tables.Remove("ePolicy")
            Catch ex As Exception

            End Try
            sqlda.Fill(ds, "ePolicy")

            If ds.Tables("ePolicy").Rows.Count > 0 Then
                If ds.Tables("ePolicy").Rows(0)("ePolicyIndicator").ToString().ToUpper().Equals("E") Then
                    txtEPolicy.Text = "Y"
                ElseIf ds.Tables("ePolicy").Rows(0)("ePolicyIndicator").ToString().ToUpper().Equals("S") Then
                    txtEPolicy.Text = "N (Slim)"
                ElseIf ds.Tables("ePolicy").Rows(0)("ePolicyIndicator").ToString().ToUpper().Equals("F") Then
                    txtEPolicy.Text = "N (Full)"
                Else
                    txtEPolicy.Text = "N/A"
                End If
            Else
                txtEPolicy.Text = "N/A"
            End If
        Catch ex As Exception
            txtEPolicy.Text = "N/A"
        End Try
    End Sub
End Class
