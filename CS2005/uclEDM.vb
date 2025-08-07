Imports System.Threading
Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.Office.Interop

Public Class uclEDM
    Inherits System.Windows.Forms.UserControl

    Dim gdtEmail As DataTable
    Dim gdtCampaign As DataTable
    Dim gdtChannel As DataTable
    Dim firstPass, blnCanSend As Boolean
    Dim gfilepath As String
    Dim strMailSeparator As String = ";"    
    Dim smtpIP As String = ConfigurationSettings.AppSettings.Item("SMTPServer")

    Dim gifpath(1) As String
    Dim mailContent As String
    Dim blnSending, blnPause As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents cmdSend As System.Windows.Forms.Button
    Friend WithEvents cboCampaign As System.Windows.Forms.ComboBox
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents lnkTo As System.Windows.Forms.LinkLabel
    Friend WithEvents cboChannel As System.Windows.Forms.ComboBox
    Friend WithEvents cmdMailBrowse As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents webBrowser As AxSHDocVw.AxWebBrowser
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents txtFromEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtHTML As System.Windows.Forms.TextBox
    Friend WithEvents txtImage As System.Windows.Forms.TextBox
    Friend WithEvents radChi As System.Windows.Forms.RadioButton
    Friend WithEvents radEng As System.Windows.Forms.RadioButton
    Friend WithEvents txtPersonal As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtSchedule As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(uclEDM))
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFrom = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.cmdSend = New System.Windows.Forms.Button
        Me.cboCampaign = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTo = New System.Windows.Forms.TextBox
        Me.lnkTo = New System.Windows.Forms.LinkLabel
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboChannel = New System.Windows.Forms.ComboBox
        Me.cmdMailBrowse = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.webBrowser = New AxSHDocVw.AxWebBrowser
        Me.cmdTest = New System.Windows.Forms.Button
        Me.txtFromEmail = New System.Windows.Forms.TextBox
        Me.txtHTML = New System.Windows.Forms.TextBox
        Me.txtImage = New System.Windows.Forms.TextBox
        Me.radChi = New System.Windows.Forms.RadioButton
        Me.radEng = New System.Windows.Forms.RadioButton
        Me.txtPersonal = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.lblProgress = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtSchedule = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.webBrowser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 16)
        Me.Label5.TabIndex = 84
        Me.Label5.Text = "From:"
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(68, 32)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(168, 20)
        Me.txtFrom.TabIndex = 83
        Me.txtFrom.Text = "Corp.Communication"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 16)
        Me.Label1.TabIndex = 88
        Me.Label1.Text = "Subject:"
        '
        'txtSubject
        '
        Me.txtSubject.BackColor = System.Drawing.Color.White
        Me.txtSubject.Location = New System.Drawing.Point(68, 80)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(504, 20)
        Me.txtSubject.TabIndex = 87
        Me.txtSubject.Text = ""
        '
        'cmdSend
        '
        Me.cmdSend.Enabled = False
        Me.cmdSend.Location = New System.Drawing.Point(496, 108)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.TabIndex = 89
        Me.cmdSend.Text = "Send Now"
        '
        'cboCampaign
        '
        Me.cboCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCampaign.Items.AddRange(New Object() {"我愛ING安泰曼聯盃", "Refundable Hospital Income"})
        Me.cboCampaign.Location = New System.Drawing.Point(68, 4)
        Me.cboCampaign.Name = "cboCampaign"
        Me.cboCampaign.Size = New System.Drawing.Size(420, 21)
        Me.cboCampaign.TabIndex = 91
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 16)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "Campaign:"
        '
        'txtTo
        '
        Me.txtTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTo.Location = New System.Drawing.Point(68, 56)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.ReadOnly = True
        Me.txtTo.Size = New System.Drawing.Size(504, 20)
        Me.txtTo.TabIndex = 93
        Me.txtTo.Text = ""
        '
        'lnkTo
        '
        Me.lnkTo.AutoSize = True
        Me.lnkTo.Location = New System.Drawing.Point(40, 60)
        Me.lnkTo.Name = "lnkTo"
        Me.lnkTo.Size = New System.Drawing.Size(21, 16)
        Me.lnkTo.TabIndex = 92
        Me.lnkTo.TabStop = True
        Me.lnkTo.Text = "To:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(500, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 16)
        Me.Label4.TabIndex = 98
        Me.Label4.Text = "Channel:"
        '
        'cboChannel
        '
        Me.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChannel.Items.AddRange(New Object() {"Email", "Outbound Call"})
        Me.cboChannel.Location = New System.Drawing.Point(556, 4)
        Me.cboChannel.Name = "cboChannel"
        Me.cboChannel.Size = New System.Drawing.Size(96, 21)
        Me.cboChannel.TabIndex = 97
        '
        'cmdMailBrowse
        '
        Me.cmdMailBrowse.Location = New System.Drawing.Point(264, 108)
        Me.cmdMailBrowse.Name = "cmdMailBrowse"
        Me.cmdMailBrowse.Size = New System.Drawing.Size(144, 23)
        Me.cmdMailBrowse.TabIndex = 100
        Me.cmdMailBrowse.Text = "Browse Email Resource"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Multiselect = True
        '
        'webBrowser
        '
        Me.webBrowser.Enabled = True
        Me.webBrowser.Location = New System.Drawing.Point(8, 244)
        Me.webBrowser.OcxState = CType(resources.GetObject("webBrowser.OcxState"), System.Windows.Forms.AxHost.State)
        Me.webBrowser.Size = New System.Drawing.Size(644, 200)
        Me.webBrowser.TabIndex = 103
        '
        'cmdTest
        '
        Me.cmdTest.Enabled = False
        Me.cmdTest.Location = New System.Drawing.Point(416, 108)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.TabIndex = 104
        Me.cmdTest.Text = "Test"
        '
        'txtFromEmail
        '
        Me.txtFromEmail.Location = New System.Drawing.Point(244, 32)
        Me.txtFromEmail.Name = "txtFromEmail"
        Me.txtFromEmail.Size = New System.Drawing.Size(168, 20)
        Me.txtFromEmail.TabIndex = 105
        Me.txtFromEmail.Text = "CorpComm@ing.com.hk"
        '
        'txtHTML
        '
        Me.txtHTML.BackColor = System.Drawing.SystemColors.Window
        Me.txtHTML.Location = New System.Drawing.Point(68, 168)
        Me.txtHTML.Name = "txtHTML"
        Me.txtHTML.ReadOnly = True
        Me.txtHTML.Size = New System.Drawing.Size(584, 20)
        Me.txtHTML.TabIndex = 106
        Me.txtHTML.Text = ""
        '
        'txtImage
        '
        Me.txtImage.BackColor = System.Drawing.SystemColors.Window
        Me.txtImage.Location = New System.Drawing.Point(68, 192)
        Me.txtImage.Multiline = True
        Me.txtImage.Name = "txtImage"
        Me.txtImage.ReadOnly = True
        Me.txtImage.Size = New System.Drawing.Size(584, 20)
        Me.txtImage.TabIndex = 107
        Me.txtImage.Text = ""
        '
        'radChi
        '
        Me.radChi.Location = New System.Drawing.Point(420, 32)
        Me.radChi.Name = "radChi"
        Me.radChi.Size = New System.Drawing.Size(68, 20)
        Me.radChi.TabIndex = 108
        Me.radChi.Text = "Chinese"
        '
        'radEng
        '
        Me.radEng.Checked = True
        Me.radEng.Location = New System.Drawing.Point(488, 32)
        Me.radEng.Name = "radEng"
        Me.radEng.Size = New System.Drawing.Size(60, 20)
        Me.radEng.TabIndex = 109
        Me.radEng.TabStop = True
        Me.radEng.Text = "English"
        '
        'txtPersonal
        '
        Me.txtPersonal.Location = New System.Drawing.Point(68, 144)
        Me.txtPersonal.Name = "txtPersonal"
        Me.txtPersonal.Size = New System.Drawing.Size(584, 20)
        Me.txtPersonal.TabIndex = 110
        Me.txtPersonal.Text = "Dear"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 172)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 16)
        Me.Label6.TabIndex = 111
        Me.Label6.Text = "HTML:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(24, 196)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 16)
        Me.Label7.TabIndex = 112
        Me.Label7.Text = "Image:"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(0, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 28)
        Me.Label8.TabIndex = 113
        Me.Label8.Text = "Personalize text:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(68, 220)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(420, 16)
        Me.ProgressBar1.TabIndex = 114
        Me.ProgressBar1.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(528, 220)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(0, 16)
        Me.lblProgress.TabIndex = 115
        Me.lblProgress.Visible = False
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(576, 108)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 116
        Me.cmdCancel.Text = "Cancel"
        '
        'Timer1
        '
        Me.Timer1.Interval = 300000
        '
        'txtSchedule
        '
        Me.txtSchedule.CustomFormat = "MM/dd/yyyy HH:mm:ss"
        Me.txtSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtSchedule.Location = New System.Drawing.Point(68, 108)
        Me.txtSchedule.Name = "txtSchedule"
        Me.txtSchedule.Size = New System.Drawing.Size(148, 20)
        Me.txtSchedule.TabIndex = 117
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 16)
        Me.Label2.TabIndex = 118
        Me.Label2.Text = "Schedule:"
        '
        'uclEDM
        '
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSchedule)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPersonal)
        Me.Controls.Add(Me.radEng)
        Me.Controls.Add(Me.radChi)
        Me.Controls.Add(Me.txtImage)
        Me.Controls.Add(Me.txtHTML)
        Me.Controls.Add(Me.txtFromEmail)
        Me.Controls.Add(Me.cmdTest)
        Me.Controls.Add(Me.webBrowser)
        Me.Controls.Add(Me.cmdMailBrowse)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboChannel)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.lnkTo)
        Me.Controls.Add(Me.cboCampaign)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdSend)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSubject)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtFrom)
        Me.Name = "uclEDM"
        Me.Size = New System.Drawing.Size(660, 452)
        CType(Me.webBrowser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property dtMail() As DataTable
        Get
            Return gdtEmail
        End Get
        Set(ByVal Value As DataTable)
            gdtEmail = Value
        End Set
    End Property

    Public Function initEDM()
        firstPass = True
        BuildUI()
        firstPass = False
    End Function

    Private Sub BuildUI()

        loadCampaign()
        bindEmailTo()
        If Me.cboCampaign.Text <> "" Then
            Me.txtSubject.Text = Trim(Me.cboCampaign.Text)
        End If

    End Sub

    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As ComboBox, ByVal strCode As String, ByVal strName As String, ByVal strSQL As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim blnLoad As Boolean

        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            dt.Clear()
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(dt)
            blnLoad = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            blnLoad = False
        End Try

        If blnLoad Then
            cbo.DataSource = dt
            cbo.DisplayMember = strName
            cbo.ValueMember = strCode
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub loadCampaign()
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "select distinct crmcmp_campaign_id, crmcmp_campaign_name from " & serverPrefix & "crm_campaign "
        strSQL &= " Where crmcmp_status_id <> '03' order by crmcmp_campaign_name"

        LoadComboBox(gdtCampaign, cboCampaign, "crmcmp_campaign_id", "crmcmp_campaign_name", strSQL)
        loadChannel(cboCampaign.SelectedValue)

    End Sub

    Private Sub loadChannel(ByVal campID As String)
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'strSQL = "select crmcpc_channel_id, crmcpc_channel_id as crmcpc_channel_name from crm_campaign_channel where crmcpc_campaign_id = '" & campID & "' "
        strSQL = "select crmcpc_channel_id, crmcpt_channel_desc from " & serverPrefix & "crm_campaign_channel, " & serverPrefix & "crm_campaign_channel_type " & _
            " Where crmcpc_campaign_id = '" & campID & "' and crmcpc_channel_id = crmcpt_channel_id " & _
            " and crmcpc_channel_id = '04' "
        LoadComboBox(gdtChannel, cboChannel, "crmcpc_channel_id", "crmcpt_channel_desc", strSQL)

        If Me.cboChannel.Items.Count = 0 Then
            Me.lnkTo.Enabled = False
            Me.txtSubject.Enabled = False
            Me.cmdMailBrowse.Enabled = False
            Me.cmdSend.Enabled = False
        Else
            Me.lnkTo.Enabled = True
            Me.txtSubject.Enabled = True
            Me.cmdMailBrowse.Enabled = True
            'Me.cmdSend.Enabled = True
        End If
    End Sub

    Private Sub cboCampaign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCampaign.SelectedIndexChanged
        If Not firstPass Then
            loadChannel(cboCampaign.SelectedValue)
            Me.txtSubject.Text = Trim(Me.cboCampaign.Text)
            Me.gdtEmail.Clear()
        End If

    End Sub

    Private Sub lnkTo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkTo.LinkClicked
        Dim dtCust As New DataTable
        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        'Load All customer email for the selected campaign and channel
        strSQL = "select emailaddr, nameprefix, namesuffix, customerid, chilstnm+chifstnm as ChiName, crmcsl_mail_sent, crmcsl_seq " & _
            " From customer c, " & serverPrefix & "crm_campaign_sales_leads " & _
            " Where crmcsl_campaign_id = '" & Me.cboCampaign.SelectedValue & "' and crmcsl_channel_id = '" & Me.cboChannel.SelectedValue & "' " & _
            " And c.customerid = crmcsl_customer_id " & _
            " And rtrim(c.EmailAddr) <> ''"

        Try
            dtCust = New DataTable
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(dtCust)
        Catch ex As Exception
        End Try

        'Display the Email dialog
        Dim f As New frmChooseEmail
        Dim bkEmail As New DataTable
        bkEmail = gdtEmail.Copy
        f.OriUnSel = dtCust
        f.OriAdded = gdtEmail
        f.ShowDialog()

        If f.DialogResult = DialogResult.OK Then
            Me.Cursor = Cursors.WaitCursor
            gdtEmail = f.OriAdded
            f.OriUnSel = Nothing
            f.OriAdded = Nothing
            f.UnSel = Nothing

            bindEmailTo()
            Me.Cursor = Cursors.Default
        Else
            gdtEmail = bkEmail
        End If

    End Sub

    Private Sub bindEmailTo()
        If gdtEmail Is Nothing Then
            gdtEmail = New DataTable
            gdtEmail.Columns.Add("emailaddr", Type.GetType("System.String"))
            gdtEmail.Columns.Add("nameprefix", Type.GetType("System.String"))
            gdtEmail.Columns.Add("namesuffix", Type.GetType("System.String"))
            gdtEmail.Columns.Add("customerid", Type.GetType("System.String"))
            gdtEmail.Columns.Add("ChiName", Type.GetType("System.String"))
            gdtEmail.Columns.Add("crmcsl_mail_sent", Type.GetType("System.String"))
            gdtEmail.Columns.Add("crmcsl_seq", Type.GetType("System.UInt16"))

            Dim key(1) As DataColumn
            key(0) = gdtEmail.Columns(3)
            gdtEmail.PrimaryKey = key
        End If

        txtTo.Text = ""
        'bind current selected email to the To field
        If gdtEmail.Rows.Count > 0 Then
            For i As Integer = 0 To Math.Min(gdtEmail.Rows.Count - 1, 5)
                txtTo.Text = txtTo.Text & gdtEmail.Rows(i).Item(0) & strMailSeparator
            Next
            txtTo.Text = txtTo.Text.Substring(0, txtTo.Text.Length - 1)
        End If
    End Sub

    Private Sub cboChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboChannel.SelectedIndexChanged
        If Not firstPass Then
            gdtEmail.Clear()
            txtTo.Text = ""
        End If    
    End Sub

    Private Sub cmdMailBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMailBrowse.Click

        OpenFileDialog1.Multiselect = True
        Me.OpenFileDialog1.Filter = "All files (*.*)|*.*|HTML files (*.html)|*.html|Gif files (*.gif)|*.gif|JPEG files (*.jpg)|*.jpg"
        If Me.OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            If OpenFileDialog1.FileNames.Length > 0 Then
                txtImage.Text = ""
                gCrmMode = "EDM"
                cboCampaign.Enabled = False
                cboChannel.Enabled = False
                cmdTest.Enabled = True
                cmdCancel.Enabled = True
                For Each finame As String In Me.OpenFileDialog1.FileNames
                    If InStr(finame, ".htm") > 0 Then
                        txtHTML.Text = finame
                        webBrowser.Navigate(finame)
                    Else
                        txtImage.Text &= finame & ";"
                    End If
                Next
            End If
        End If
        'Dim img As Image
        'gfilepath = Me.OpenFileDialog1.FileName()
        'img = New Bitmap(gfilepath)

        'If Not img Is Nothing Then
        '    Me.picEmail.Image = img
        'End If

        '    If OpenFileDialog1.FileNames.Length > 0 Then
        '        ReDim Preserve gifpath(OpenFileDialog1.FileNames.Length)
        '        Dim i As Integer
        '        i = 0

        '        For Each finame As String In Me.OpenFileDialog1.FileNames
        '            If InStr(finame, ".htm") > 0 Then
        '                webBrowser.Navigate(finame)
        '                'read the html content into the mailContent
        '                Try
        '                    Dim fs As IO.FileStream
        '                    Dim sr As IO.StreamReader

        '                    fs = System.IO.File.OpenRead(finame)
        '                    sr = New IO.StreamReader(fs)
        '                    mailContent = sr.ReadToEnd()
        '                    sr.Close()
        '                    fs.Close()

        '                Catch ex As Exception
        '                    MsgBox(ex.Message.ToString)
        '                    Exit Sub
        '                End Try

        '            Else
        '                'save the path for the other attachments
        '                gifpath(i) = finame
        '                i = i + 1
        '            End If

        '            Me.lblFiles.Text = Me.lblFiles.Text & vbCrLf & finame.Substring(finame.LastIndexOf("\") + 1)
        '        Next

        '    End If
        'End If
    End Sub

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        'Dim mOutLookApp As Outlook.Application
        'Dim mNameSpace As Outlook.NameSpace

        'mOutLookApp = New Outlook.Application
        'mNameSpace = mOutLookApp.GetNamespace("MAPI")
        'mNameSpace.Logon(, , False, True)
        'composeEmail(mOutLookApp)
        'resetEmail()

        blnCanSend = False
        If txtHTML.Text <> "" AndAlso txtTo.Text <> "" Then

            If Now >= txtSchedule.Value Then
                blnCanSend = True
            Else
                Timer1.Enabled = True
            End If

            While Not blnCanSend
                Application.DoEvents()
                If Timer1.Enabled = False Then
                    Exit Sub
                End If
            End While
            Timer1.Enabled = False

            ProgressBar1.Visible = True
            ProgressBar1.Maximum = gdtEmail.Rows.Count
            ProgressBar1.Value = 0
            lblProgress.Visible = True
            lblProgress.Text = ""
            'blnSending = True

            If sendEDM() Then
                MsgBox("All EDM sent to recipients succesfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Else
                MsgBox("Problem sending EDM to customer.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            End If
            'resetEmail()
        Else
                MsgBox("Please choose recipients of this EDM first.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
        End If

    End Sub

    Private Function sendEDM() As Boolean

        Dim strSender As String
        Dim strRecipient As String
        Dim strCustomer As String
        Dim intTTL As Integer

        If Not gdtEmail Is Nothing Then
            If gdtEmail.Rows.Count > 0 Then

                intTTL = gdtEmail.Rows.Count

                gdtEmail.DefaultView.RowFilter = "crmcsl_mail_sent = 'Y'"

                ' send again
                If gdtEmail.DefaultView.Count = gdtEmail.Rows.Count Then
                    If MsgBox("EDM already sent for this channel, reset sent flag?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        If resetflag() = False Then
                            sendEDM = False
                            Exit Function
                        End If
                    End If
                    gdtEmail.DefaultView.RowFilter = ""
                Else
                    ' Continue
                    If gdtEmail.DefaultView.Count > 0 Then
                        If MsgBox("Last send operation was not completed, continue from last unsend EDM?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            If resetflag() = False Then
                                sendEDM = False
                                Exit Function
                            End If
                            gdtEmail.DefaultView.RowFilter = ""
                        End If
                    Else
                        gdtEmail.DefaultView.RowFilter = ""
                    End If
                End If

                Dim strLog As String
                strLog &= "Send EDM for campaign " & cboCampaign.SelectedValue & ", channel " & cboChannel.SelectedValue
                strLog &= " Send by " & gsUser & " at " & Now
                strLog &= " Subject of EDM is " & Trim(txtSubject.Text) & ", using email address " & txtFromEmail.Text

                If AddAuditTrail("0", "Send EDM", strLog) = False Then
                    MsgBox("Error writing audit trail entry.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                End If

                Try
                    'For Each row As DataRow In gdtEmail.Rows
                    Dim row As DataRow
                    Dim strRefNo As String
                    For j As Integer = 0 To gdtEmail.DefaultView.Count - 1
                        row = gdtEmail.DefaultView.Item(j).Row
                        strRefNo = row.Item("crmcsl_seq").ToString
                        If strRefNo.Length < 5 Then
                            strRefNo = strRefNo.PadLeft(5, "0")
                        End If

                        strRecipient = Trim(row.Item(0))
                        If radEng.Checked Then
                            strCustomer = Trim(row.Item(1)) & " " & Trim(row.Item(2))
                        Else
                            strCustomer = Trim(row.Item(3))
                        End If
                        'i += 1
                        lblProgress.Text = "Sending " & j + 1 & " of " & intTTL
                        ProgressBar1.Value = j + 1
                        If SendMail(strRecipient, strCustomer, strRefNo) = False Then
                            sendEDM = False
                            Exit Function
                        End If
                        Application.DoEvents()
                        updateMailSent(cboCampaign.SelectedValue, row.Item(3).ToString)
                        Thread.Sleep(2000)
                        Application.DoEvents()
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    sendEDM = False
                    Exit Function
                End Try
            End If
        End If

        sendEDM = True

    End Function

    Private Sub resetEmail()

        gCrmMode = ""
        blnSending = False
        blnPause = False
        cmdSend.Enabled = False
        cboCampaign.Enabled = True
        cboChannel.Enabled = True
        Me.cmdMailBrowse.Enabled = True
        cmdTest.Enabled = False
        cmdCancel.Enabled = False
        Timer1.Enabled = False
        blnCanSend = False

        ProgressBar1.Visible = False
        lblProgress.Visible = False

        Me.txtTo.Text = ""
        Me.txtHTML.Text = ""
        Me.txtImage.Text = ""
        'Me.webBrowser.Navigate("")
        Me.txtSubject.Text = Me.cboCampaign.Text
        Me.gdtEmail.Clear()
        gifpath = Nothing
    End Sub

    Private Function resetflag() As Boolean
        Dim sqlconnect As New SqlConnection
        Dim strExec As String
        Dim sqlCmd As SqlCommand
        sqlCmd = New SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strExec = "update " & serverPrefix & "crm_campaign_sales_leads set crmcsl_mail_sent = '' " & _
                  "where crmcsl_campaign_id = '" & cboCampaign.SelectedValue & "' And crmcsl_channel_id = '" & Me.cboChannel.SelectedValue & "'"

        Try
            sqlconnect.ConnectionString = strCIWConn
            sqlCmd.CommandText = strExec
            sqlCmd.Connection = sqlconnect
            sqlconnect.Open()

            sqlCmd.ExecuteNonQuery()

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            resetflag = False
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            resetflag = False
        Finally
            sqlconnect.Close()
            resetflag = True
        End Try

    End Function

    Private Function updateMailSent(ByVal inCamp As String, ByVal inCust As String) As Boolean
        Dim sqlconnect As New SqlConnection
        Dim strExec As String
        Dim sqlCmd As SqlCommand
        sqlCmd = New SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strExec = "update " & serverPrefix & "crm_campaign_sales_leads set crmcsl_mail_sent = 'Y' " & _
                  "where crmcsl_campaign_id = '" & inCamp & "' and crmcsl_channel_id = '" & Me.cboChannel.SelectedValue & _
                  "' and crmcsl_customer_id = '" & inCust & "'"

        Try
            sqlconnect.ConnectionString = strCIWConn
            sqlCmd.CommandText = strExec
            sqlCmd.Connection = sqlconnect
            sqlconnect.Open()

            sqlCmd.ExecuteNonQuery()

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            updateMailSent = False
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            updateMailSent = False
        Finally
            sqlconnect.Close()
            updateMailSent = True
        End Try

    End Function

    Private Function SendMail(ByVal strEmail As String, ByVal strRecepient As String, Optional ByVal strRefNo As String = "") As Boolean

        Dim strCmd, strHTMLContent, strHTMLNew, strTmpPath, strTmpPath1, strSubject As String
        Dim aryImage() As String
        Dim intCmdRtn As Integer

        Dim sr As IO.StreamReader
        Dim sw As IO.StreamWriter

        If Trim(txtHTML.Text) = "" Then
            MsgBox("Please choose a HTML file to send.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Exit Function
        End If

        strTmpPath = Mid(txtHTML.Text, 1, txtHTML.Text.LastIndexOf("\") + 1) & "tempEmail.html"
        'strTmpPath1 = Mid(txtHTML.Text, 1, txtHTML.Text.LastIndexOf("\") + 1) & "sendmail.bat"

        sr = New IO.StreamReader(txtHTML.Text, System.Text.Encoding.Default)
        strHTMLContent = sr.ReadToEnd
        sr.Close()

        strHTMLNew = Replace(strHTMLContent, "<@P@>", Trim(txtPersonal.Text) & " " & strRecepient)

        sw = New IO.StreamWriter(strTmpPath, False, System.Text.Encoding.Default)
        sw.WriteLine(strHTMLNew)
        sw.Flush()
        sw.Close()

        strSubject = txtSubject.Text.Trim
        If strRefNo <> "" Then strSubject &= "    (Ref#: " & strRefNo & ")"

        strCmd = "Blat """ & strTmpPath & """" & _
            " -server " & smtpIP & _
            " -f " & RTrim(txtFromEmail.Text) & _
            " -t """ & RTrim(strRecepient) & "<" & RTrim(strEmail) & ">""" & _
            " -s """ & strSubject & """" & _
            " -html -charset BIG5 -r "

        aryImage = Strings.Split(txtImage.Text, ";")
        For i As Integer = 0 To aryImage.Length - 1
            If Trim(aryImage(i)) <> "" Then
                strCmd &= " -attach """ & aryImage(i) & """"
            End If
        Next

        'sw = New IO.StreamWriter(strTmpPath1, False, System.Text.Encoding.Default)
        'sw.WriteLine(strCmd)
        'sw.Flush()
        'sw.Close()

        Try
            intCmdRtn = Shell(strCmd, AppWinStyle.Hide, True)
        Catch ex As Exception
            SendMail = False
        End Try

        SendMail = True
        'If intCmdRtn = 0 Then
        '    Return True
        'Else
        '    Return False
        'End If

        Try
            Call System.IO.File.Delete(strTmpPath)
        Catch ex As Exception

        End Try
        'Call System.IO.File.Delete(strTmpPath1)

        'body_AgentReport.txt -server 10.10.18.35 -t eric.tm.shu@ing.com.hk -f eric.
        'tm.shu@ing.com.hk -s "Report Softcopy - PDF Conversion Schdule Job" -r
        'objMail = Server.CreateObject("CDONTS.Newmail")
        'objMail.MailFormat = 0
        'objMail.Importance = 1
        'objMail.Subject = "ING E-Card"
        'objMail.From = sender_mail_var
        'objMail.To = Replace(receive_mail_var, ",", ";")
        ''response.write receive_mail_var & Server.MapPath(ecardPath) & "\" & file_name & " <br>"
        ''response.write replace(receive_mail_var, "," , ";") & "<br>"
        'objMail.MailFormat = 0
        'objMail.BodyFormat = 0
        'attachedfile = Server.MapPath(ecardPath) & "\" & file_name
        'objMail.Body = content_str
        'objMail.AttachURL(attachedfile, "ecard.html")
        'objMail.Send()
        'objMail = Nothing

    End Function

    Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click

        Dim strTmpEmail As String

        strTmpEmail = InputBox("Please Enter an Email address for testing", "EDM")
        If strTmpEmail <> "" Then
            Call SendMail(strTmpEmail, "Test Print")

            If MsgBox("Is the Email receive and display correctly?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.cmdSend.Enabled = True
            End If
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        If blnSending Then
            blnPause = Not blnPause
        Else
            Call resetEmail()
        End If

    End Sub

    Private Sub composeEmail(ByRef oApp As Outlook.Application)
        Dim mItem As Outlook.MailItem

        Dim strSender As String
        Dim strRecipient As String
        Dim strCustomer As String

        If Not gdtEmail Is Nothing Then
            If gdtEmail.Rows.Count > 0 Then

                Try

                    For Each row As DataRow In gdtEmail.Rows
                        mItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)

                        'Add attachments to the mail message
                        If gifpath.GetUpperBound(0) > 0 Then
                            For i As Integer = 0 To Me.gifpath.GetUpperBound(0)
                                If gifpath(i) <> "" And (Not gifpath(i) Is Nothing) Then
                                    mItem.Attachments.Add(gifpath(i))
                                End If
                            Next
                        Else
                            'No attachment, html content only
                            If mailContent = "" Then
                                MsgBox("Empty Content. Please select email content.")
                                Exit Sub
                            End If
                        End If

                        strRecipient = Trim(row.Item(0))
                        strRecipient = "Jonathan.KH.Ng@ing.com.hk"
                        strCustomer = "Dear " & Trim(row.Item(1)) & " " & Trim(row.Item(2)) & ","

                        With mItem
                            .To = strRecipient
                            .Subject = txtSubject.Text & " (Customer ID:" & row.Item(3).ToString & ")"

                            If mailContent <> "" Then
                                Dim content As String
                                Dim findBody As String

                                'insert customer title into the email
                                findBody = "<body>"
                                mailContent = mailContent.Insert(InStr(mailContent, findBody) + findBody.Length, strCustomer & "<br><br>")

                                .HTMLBody = mailContent
                            Else

                                If gifpath.GetUpperBound(0) > 0 Then
                                    'has attachment but no emai content, use the attachment as the email content
                                    Dim fname As String
                                    fname = gifpath(0).Substring(gifpath(0).LastIndexOf("\") + 1)

                                    .HTMLBody = "<html><body><font face=Times New Roman>" & strCustomer & "</font>" & _
                                                "<br><br><img src=" & fname & " ></body></html>"
                                End If

                            End If

                            .Sensitivity = Outlook.OlSensitivity.olNormal
                            .Importance = Outlook.OlImportance.olImportanceNormal
                            .ReadReceiptRequested = True

                        End With

                        mItem.Send()
                        updateMailSent(cboCampaign.SelectedValue, row.Item(3).ToString)
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    Exit Sub
                End Try
            End If
        End If


    End Sub

    Private Sub composeMail()
        Dim strSender As String
        Dim strRecipient As String
        Dim strCustomer As String
        Dim tfilename As String
        Dim attachFile As Web.Mail.MailAttachment
        Dim msg As New Web.Mail.MailMessage
        Dim mailSent As Integer

        mailSent = 0

        If Not gdtEmail Is Nothing Then
            If gdtEmail.Rows.Count > 0 Then
                Try
                    attachFile = New Web.Mail.MailAttachment(gfilepath)
                    msg.Attachments.Add(attachFile)
                    tfilename = msg.Attachments(0).Filename()
                    tfilename = tfilename.Substring(InStrRev(tfilename, "\"))

                    'loop through all the customers
                    For Each row As DataRow In gdtEmail.Rows
                        strRecipient = Trim(row.Item(0))
                        strRecipient = "Jonathan.KH.Ng@ing.com.hk"
                        'strRecipient = "Johnny.A.Zhang@ing.com.hk"                    

                        strCustomer = "Dear " & Trim(row.Item(1)) & " " & Trim(row.Item(2)) & ","

                        With msg
                            .From = txtFrom.Text
                            .To = strRecipient
                            .BodyFormat = Web.Mail.MailFormat.Html
                            .Subject = txtSubject.Text
                            .Body = "<html><body><iframe name=""test"" width=100%, height=100% src=""http://10.10.18.238/testuat/TestCRS.asp?ID=3""></iframe><font face=Times New Roman>" & strCustomer & "</font>" & _
                                "<br><br><img src=" & tfilename & " ></body></html>"
                            '.Body = "<html><body><iframe name=""test"" width=100%, height=100% src=""http://10.10.18.77/ECG/Test/GetPara.asp?ID=999""></iframe><font face=Times New Roman>" & strCustomer & "</font>" & _
                            '    "<br><br></body></html>"                        

                        End With

                        System.Web.Mail.SmtpMail.Send(msg)
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    Exit Sub
                End Try

                mailSent = gdtEmail.Rows.Count
            End If
        End If

        MsgBox(mailSent & " mail has been sent.")

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Now >= txtSchedule.Value Then
            blnCanSend = True
        End If
    End Sub

End Class
