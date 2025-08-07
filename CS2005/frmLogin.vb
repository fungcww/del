'********************************************************************
' Admended By: Flora Leung
' Admended Function: frmLogin_Load
' Date: 14 Feb 2012
' Project: Project Leo Goal 3 Capsil 
'
' Admended By: Peter Lam
' Admended Function: frmLogin_Load
' Date: 22 Feb 2012
' Project: Project Leo Goal 1 Phase 1
'
' Admended By: Kay Tsang KT20150615
' Admended Function: GetUPSGroup, cmdLogin_Click, OrAuthority (new)
' Date: 15 Jun 2015
' Project: cater for multiple user group authority
'
' Admended By: Oliver Ou
' Admended Function: 
' Date: 15 Jun 2023
' Project: Switch Over Code from Assurance to Bermuda 
'********************************************************************

Imports System.Data.SqlClient
Imports System.Security.Principal
Imports System.Runtime.InteropServices
'Imports System.Runtime.Remoting
Imports System.Configuration
Imports System.DirectoryServices.AccountManagement
Imports System.DirectoryServices

Public Class frmLogin
    Inherits System.Windows.Forms.Form

    Declare Function LogonUserA Lib "advapi32.dll" (ByVal lpszUsername As String, ByVal lpszDomain As String, ByVal lpszPassword As String, ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, ByRef phToken As IntPtr) As Integer
    Declare Auto Function DuplicateToken Lib "advapi32.dll" (ByVal ExistingTokenHandle As IntPtr, ByVal ImpersonationLevel As Integer, ByRef DuplicateTokenHandle As IntPtr) As Integer
    Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long
    Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long
    Private LOGON32_LOGON_INTERACTIVE As Integer = 2
    Private LOGON32_PROVIDER_DEFAULT As Integer = 0
    Friend WithEvents cboEnv As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Private impersonationContext As WindowsImpersonationContext

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtPwd As System.Windows.Forms.TextBox
    Friend WithEvents cmdLogin As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    '<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtPwd = New System.Windows.Forms.TextBox
        Me.cmdLogin = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.cboEnv = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboCompany = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Password:"
        '
        'txtName
        '
        Me.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtName.Location = New System.Drawing.Point(114, 92)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(100, 20)
        Me.txtName.TabIndex = 2
        '
        'txtPwd
        '
        Me.txtPwd.Location = New System.Drawing.Point(114, 124)
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwd.Size = New System.Drawing.Size(100, 20)
        Me.txtPwd.TabIndex = 3
        '
        'cmdLogin
        '
        Me.cmdLogin.Location = New System.Drawing.Point(51, 169)
        Me.cmdLogin.Name = "cmdLogin"
        Me.cmdLogin.Size = New System.Drawing.Size(75, 23)
        Me.cmdLogin.TabIndex = 4
        Me.cmdLogin.Text = "&Login"
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(139, 169)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "&Cancel"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'cboEnv
        '
        Me.cboEnv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEnv.FormattingEnabled = True
        Me.cboEnv.Location = New System.Drawing.Point(114, 61)
        Me.cboEnv.Name = "cboEnv"
        Me.cboEnv.Size = New System.Drawing.Size(100, 21)
        Me.cboEnv.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Environment:"
        '
        'cboCompany
        '
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(114, 34)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(100, 21)
        Me.cboCompany.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Company:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 205)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Version: "
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(76, 205)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(0, 13)
        Me.lblVersion.TabIndex = 12
        '
        'frmLogin
        '
        Me.AcceptButton = Me.cmdLogin
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(245, 227)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboEnv)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboCompany)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdLogin)
        Me.Controls.Add(Me.txtPwd)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CS2005 System Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'oliver 2023-11-30 added for Switch Over Code from Assurance to Bermuda
        If Not IsNothing(ConfigurationManager.AppSettings("IsAssurance")) AndAlso ConfigurationManager.AppSettings("IsAssurance") = "1" Then
            IsAssurance = True
        End If

        'objCOM = CreateObject("COM.ComClsGetSysInfo")
        'gsUser = objCOM.GetNTUserName()
        Me.Text = gSystem & " System Login"

        'If My.User.Name <> "" Then
        If Environment.UserName <> "" Then
            'Dim strUser As String = My.User.Name
            Dim strUser As String = Environment.UserName
            txtName.Text = strUser.Substring(InStr(strUser, "\")).ToUpper
            If Not "".Equals(GetUatXml("USERID")) Then
                txtName.Text = GetUatXml("USERID").Trim
            End If
            'txtName.Enabled = False
        End If

        'Initialize Company Combo Box
        cboCompany.Items.Clear()
        cboCompany.Items.Add(New ListItem("HKG", "ING"))
        cboCompany.Items.Add(New ListItem("Macau", "MCU"))
        cboCompany.Items.Add(New ListItem("HKL", "HKL"))

#If HKL = 1 Then
        cboCompany.SelectedIndex = 2
#Else
        cboCompany.SelectedIndex = 0
#End If


        cboEnv.Items.Clear()
        cboEnv.Items.Add("PRD01")
        'AC - Remove obsolated environment
        'cboEnv.Items.Add("UAT00")
        'cboEnv.Items.Add("UAT01")
        'cboEnv.Items.Add("UAT02")
        'cboEnv.Items.Add("UAT03")
        'cboEnv.Items.Add("UAT04")
        'cboEnv.Items.Add("UAT07")
        'cboEnv.Items.Add("SIT00")
        'cboEnv.Items.Add("SIT01")
        'cboEnv.Items.Add("SIT02")
        'cboEnv.Items.Add("SIT03")
        'cboEnv.Items.Add("SIT04")
        'cboEnv.Items.Add("SIT07")
        'cboEnv.Items.Add("DEV00")
        'cboEnv.Items.Add("DEV01")
        'cboEnv.Items.Add("DEV02")
        'cboEnv.Items.Add("DEV03")
        'cboEnv.Items.Add("DEV04")
        'cboEnv.Items.Add("DEV07")        
        'AC - Remove obsolated environment

        ' Peter Lam, other new Environment Setup, 22-Feb-2012 Start
        cboEnv.Items.Add("D101")
        cboEnv.Items.Add("S101")
        cboEnv.Items.Add("U101")
        cboEnv.Items.Add("D102")
        cboEnv.Items.Add("S102")
        cboEnv.Items.Add("U102")

        'DC -  Capsil Conversion
        cboEnv.Items.Add("D106")
        cboEnv.Items.Add("S106")
        cboEnv.Items.Add("U106")
        cboEnv.Items.Add("T106")

        'ITSR933 FG R4 EnvSetup Gary Lei Start
        'Iteration
        cboEnv.Items.Add("ITRU01")
        cboEnv.Items.Add("ITRM01")
        'ITSR933 FG R4 EnvSetup Gary Lei End

        'AC - Remove not exists  environment
        'cboEnv.Items.Add("D103")
        'cboEnv.Items.Add("S103")
        'cboEnv.Items.Add("U103")
        'AC - Remove not exists  environment

        cboEnv.Items.Add("D201")
        cboEnv.Items.Add("S201")
        cboEnv.Items.Add("U201")
        ' Peter Lam, other new Environment Setup, 22-Feb-2012 End
        ' Flora Leung, Project Leo Goal 3 Capsil Environment Setup, 14-Feb-2012 Start
        cboEnv.Items.Add("D202")
        cboEnv.Items.Add("S202")
        cboEnv.Items.Add("U202")
        ' Flora Leung, Project Leo Goal 3 Capsil Environment Setup, 14-Feb-2012 End
        ' Peter Lam, other new Environment Setup, 22-Feb-2012 Start
        cboEnv.Items.Add("D203")
        cboEnv.Items.Add("S203")
        cboEnv.Items.Add("U203")

        cboEnv.Items.Add("D103")
        cboEnv.Items.Add("U103")
        cboEnv.Items.Add("U104")
        cboEnv.Items.Add("U105")
        cboEnv.Items.Add("S105")
        cboEnv.Items.Add("D105")

        cboEnv.Items.Add("D301")
        cboEnv.Items.Add("S301")
        cboEnv.Items.Add("U301")
        cboEnv.Items.Add("U401")

        'oliver 2023-11-30 added for Switch Over Code from Assurance to Bermuda
        If Not IsAssurance Then
            cboEnv.Items.Add("U402")
        End If

        ' Peter Lam, other new Environment Setup, 22-Feb-2012 End

        'AC - Change to use configuration setting
        '#If UAT = 0 Then
        '        cboEnv.SelectedIndex = 0
        '#Else
        '        cboEnv.SelectedIndex = 2
        '#End If

        gUAT = False
        ' VS will run in \bin directory
        Dim strConfigPath As String = Application.StartupPath & "\cs2005.exe.config"

        ' Read from configuration files instead
        'RemotingConfiguration.Configure(strConfigPath)


        For i As Int16 = 0 To ConfigurationSettings.AppSettings.Count - 1
            If ConfigurationSettings.AppSettings.Keys(i) = "UAT" And ConfigurationSettings.AppSettings.Item(i).ToString = "1" Then
                gUAT = True
            End If
        Next


        'AC - Change to use configuration setting - start
        If gUAT = False Then 'Production 
            cboEnv.SelectedIndex = 0
            cboCompany.Enabled = True
            cboEnv.Enabled = False
            'If ConfigurationSettings.AppSettings.Item("userInput") = "0" Then
            If Not "Y".Equals(GetUatXml("USERINPUT")) Then
                txtName.Enabled = False
            Else
                txtName.Enabled = True
            End If
            txtPwd.Enabled = False

            If ConfigurationSettings.AppSettings.Item("Company") = "HKL" Then
                cboCompany.Enabled = False
                txtName.Enabled = True
                txtPwd.Enabled = True
            End If
        Else
            For i As Int16 = 0 To cboEnv.Items.Count - 1
                If cboEnv.Items(i).ToString = ConfigurationSettings.AppSettings.Item("DefaultEnvironment").ToString Then
                    cboEnv.SelectedIndex = i
                    Exit For
                End If
            Next

            'If ConfigurationSettings.AppSettings.Item("userInput") = "0" Then
            If Not "Y".Equals(GetUatXml("USERINPUT")) Then
                cboCompany.Enabled = False
                cboEnv.Enabled = False
                txtName.Enabled = False
            Else
                cboCompany.Enabled = True
                cboEnv.Enabled = True
                txtName.Enabled = True
            End If

            txtPwd.Enabled = False
        End If
        'AC - Change to use configuration setting

        ' SSO Changes
        'Me.txtName.Text = System.Environment.UserName
        'Call RPC_Setup()
        ' End Changes
        '#If UAT = 0 Then
        '        cboCompany.Enabled = True
        '        cboEnv.Enabled = False

        '        If ConfigurationSettings.AppSettings.Item("userInput") = "0" Then
        '            txtName.Enabled = False
        '        Else
        '            txtName.Enabled = True
        '        End If

        '        txtPwd.Enabled = False
        '#End If

        '#If HKL = 1 And UAT = 0 Then
        '    cboCompany.Enabled = false
        '    txtName.Enabled = True
        '    txtPwd.Enabled = true
        '#End If

        lblVersion.Text = Microsoft.VisualBasic.Strings.Left(Application.ProductVersion, InStrRev(Application.ProductVersion, ".0") - 1)

    End Sub

    ' Start SSO changes
    Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        '***
        'enviroment setting
        If cboCompany.Text.Trim = "" Or cboEnv.Text.Trim = "" Then
            MsgBox("Incomplete input.")
            Exit Sub
        End If
        gLoginEnvStr = cboCompany.SelectedItem.Value.trim & cboEnv.Text.Trim
        AsyncDbLogger.LogInfo(Me.txtName.Text, "Login", "Login Start")  'Vincent Log 0120
        EnvSetup(gLoginEnvStr)
        '***

        Call RPC_Setup()
        AsyncDbLogger.LogInfo(Me.txtName.Text, "Login", "Env Setup completed")  'Vincent Log 0120

        Dim strErr, strDomain As String
        Dim blnPass As Boolean = True

        wndMain.Cursor = Cursors.AppStarting

        'strDomain = SystemInformation.UserDomainName()
        'strDomain = ConfigurationSettings.AppSettings.Item("Domain")
        strDomain = "EAANT2DM"

        If g_Comp = "ING" Then
            blnPass = True
        End If

        If blnPass Then
            gsUser = Me.txtName.Text

            'If g_Comp = "HKL" Then gsUser = "POLDSL"

            Call RPC_Setup2()

            AsyncDbLogger.LogInfo(Me.txtName.Text, "Login", "RPC_Setup2 completed") 'Vincent Log 0120

            Dim dt As New DataTable() ' KT20150615
            strErr = String.Empty
            Try

                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(getCompanyCode(), "CHECK_UPS_GROUP", New Dictionary(Of String, String)() From {
                                                                {"strSystem", gUPSystem},
                                                                {"strUserID", gsUser}
                                                                })
                If retDs IsNot Nothing AndAlso retDs.Tables.Count > 0 AndAlso retDs.Tables(0).Rows.Count > 0 Then
                    dt = retDs.Tables(0)
                End If

                AsyncDbLogger.LogInfo(Me.txtName.Text, "Login", "CHECK_UPS_GROUP completed")    'Vincent Log 0120
            Catch ex As Exception
                strErr = ex.ToString()
                'MsgBox("Error : " & ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            End Try

            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            'CheckCRSExternalUser(gsUser, ExternalUser)
            ''CRS 7x24 Changes - End

            'AC - Change to use configuration setting
            If gUAT Then
                If Not GetBusinessDate(gBusDate) Then
                    gBusDate = Today
                    'oliver 2023-11-30 uncommented for Switch Over Code from Assurance to Bermuda
                Else
                    gBusDate = Today
                End If
            Else
                gBusDate = Today
            End If
            'AC - Change to use configuration setting

            'MsgBox("Group=" & giUPSGrp & ",Error=" & strErr)

            AsyncDbLogger.LogInfo(Me.txtName.Text, "Login", "GetBusinessDate completed")    'Vincent Log 0120

            If Not String.IsNullOrEmpty(strErr) OrElse dt.Rows.Count = 0 Then ' KT20150615
                MsgBox("Error retrieving user group records, please contact Administrator. Click <OK> to quit." & vbCrLf & "Additional Error: " & strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                End
            Else
                'KT20150615
                Dim ary As Char() = DECtoBIN(0).ToCharArray
                For Each row As DataRow In dt.Rows
                    Dim tempAry As Char() = Microsoft.VisualBasic.Strings.StrReverse(DECtoBIN(row.Item("upsugt_usr_right"))).ToCharArray()
                    ary = OrAuthority(ary, tempAry)
                Next
                For i As Integer = 0 To ary.Length - 1
                    strUPSMenuCtrl = strUPSMenuCtrl & ary(i)
                Next
                'strUPSMenuCtrl = Microsoft.VisualBasic.Strings.StrReverse(DECtoBIN(giUPSGrp))
                'KT20150615
            End If

            Try
                ' ITSR-4063 
                Dim adGroupList As List(Of String)
                adGroupList = GetAdGroupsForUser(gsUser)

                Dim prefix As String
                If gUAT Then
                    prefix = "UAT"
                Else
                    prefix = "PRD"
                End If

                For Each str As String In adGroupList
                    If str.Contains(prefix + "CRS_UHNW_HK") Then
                        isUHNWMember = True
                    ElseIf str.Contains(prefix + "CRS_UHNW_MC") Then
                        isUHNWMemberMcu = True
                    End If
                Next

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            AsyncDbLogger.LogInfo(Me.txtName.Text, "Login", "GetAdGroupsForUser completed") 'Vincent Log 0120
#Region "Testing"
            '' For hard code testing
            'isUHNWMember = True
            'isUHNWMemberMcu = True
#End Region

            wndMain.Show()
            wndMain.StatusBarPanel2.Text = gsUser
            wndMain.StatusBarPanel3.Text = "Version " & Microsoft.VisualBasic.Strings.Left(Application.ProductVersion, InStrRev(Application.ProductVersion, ".0") - 1)

            wndMain.StatusBarPanel4.Text = cboCompany.SelectedItem.Text.Trim & cboEnv.Text.Trim

#If RPC = "DCOM" Then
            wndMain.StatusBarPanel5.Text = "DCOM"
#Else
            wndMain.StatusBarPanel5.Text = "HTTP"
#End If

            Me.Hide()

        Else
            MsgBox("Invalid Username or Password!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Me.txtPwd.Focus()
        End If

        AsyncDbLogger.LogInfo(Me.txtName.Text, "Login", "Login completed")  'Vincent Log 0120

        wndMain.Cursor = Cursors.Default

    End Sub

    'KT20150615
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
    'KT20150615


    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        End
    End Sub


    <Obsolete>
    Private Function impersonateValidUser(ByVal userName As String, ByVal domain As String, ByVal password As String) As Boolean

        Dim tempWindowsIdentity As WindowsIdentity
        Dim token As IntPtr = IntPtr.Zero
        Dim tokenDuplicate As IntPtr = IntPtr.Zero
        impersonateValidUser = False

        If RevertToSelf() Then
            If LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
                If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                    tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                    impersonationContext = tempWindowsIdentity.Impersonate()
                    If Not impersonationContext Is Nothing Then
                        impersonateValidUser = True
                    End If
                End If
            End If
        End If
        If Not tokenDuplicate.Equals(IntPtr.Zero) Then
            CloseHandle(tokenDuplicate)
        End If
        If Not token.Equals(IntPtr.Zero) Then
            CloseHandle(token)
        End If
    End Function

    <Obsolete>
    Private Sub undoImpersonation()
        impersonationContext.Undo()
    End Sub

    Private Sub frmLogin_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        'Me.txtPwd.Focus()
        Me.txtName.Focus()
    End Sub

    Public Function GetAdGroupsForUser(ByVal userName As String) As List(Of String)

        Dim result As New List(Of String)
        Dim domainName As String = "EAANT2DM"

        Using domainContext As PrincipalContext = New PrincipalContext(ContextType.Domain, domainName)
            Using user As UserPrincipal = UserPrincipal.FindByIdentity(domainContext, userName)
                If Not IsNothing(user) Then
                    Using searcher = New DirectorySearcher(New DirectoryEntry("LDAP://" + domainContext.Name))
                        searcher.Filter = String.Format("(&(objectCategory=group)(member={0}))", user.DistinguishedName)
                        searcher.SearchScope = SearchScope.Subtree
                        searcher.PropertiesToLoad.Add("cn")

                        For Each entry As SearchResult In searcher.FindAll()
                            If (entry.Properties.Contains("cn")) Then
                                result.Add(entry.Properties("cn")(0).ToString())
                            End If
                        Next
                    End Using
                End If
            End Using
        End Using

        result.Sort()
        Return result

    End Function

    Private Class ListItem
        Private m_value As String = String.Empty
        Private m_display As String = String.Empty
        Public Sub New(ByVal sdisplay As String, ByVal svalue As String)
            m_display = sdisplay
            m_value = svalue
        End Sub

        Public Overrides Function ToString() As String
            Return Me.m_display
        End Function

        Public Property Value() As String
            Get
                Return Me.m_value
            End Get
            Set(ByVal value As String)
                Me.m_value = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return Me.m_display
            End Get
            Set(ByVal value As String)
                Me.m_display = value
            End Set
        End Property
    End Class


End Class


