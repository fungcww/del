Imports System.Data.SqlClient
Imports System.IO

Public Class SMSiLAS
    Inherits System.Windows.Forms.UserControl

#Region "Property"

    Dim strMsgCategory As String = "iLASSMS"
    Dim strPolicy As String
    Dim strCustomerID As String
    Dim dr As DataRow

    Public Property PolicyAccountID() As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            strPolicy = Value
            If Not Value Is Nothing Then Call buildUI()
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

#End Region

#Region "Control Events"
    Private Sub btnSendSMSiLAS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMSiLAS.Click
        Dim strErMsg As String = String.Empty
        wndMain.Cursor = Cursors.WaitCursor

        If Me.lblPolicyHdrMobile.Text.Trim = "" Then
            wndMain.Cursor = Cursors.Default
            MsgBox("No mobile no. for this customer", MsgBoxStyle.Exclamation, gSystem)
            Exit Sub
        End If

        If Not PendingSMS(strPolicy) Then
            Me.SendSMS(strPolicy, strErMsg)

            If strErMsg <> String.Empty Then
                wndMain.Cursor = Cursors.Default
                MsgBox(strErMsg, MsgBoxStyle.Exclamation, gSystem)
            Else
                wndMain.Cursor = Cursors.Default
                MsgBox("SMS Sent", MsgBoxStyle.Information, gSystem)

                wndMain.Cursor = Cursors.WaitCursor
                'Refresh DataGrid
                Me.InitDataset()
                wndMain.Cursor = Cursors.Default
            End If

        Else
            wndMain.Cursor = Cursors.Default
            MsgBox("SMS is already pending", MsgBoxStyle.Exclamation, gSystem)
        End If

    End Sub

    Private Sub dgSMS_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgSMSLog.CurrentCellChanged
        wndMain.Cursor = Cursors.WaitCursor
        Me.UpdateSMSStatus()
        wndMain.Cursor = Cursors.Default
    End Sub

#End Region

#Region "Functions"

    Private Sub buildUI()
        wndMain.Cursor = Cursors.WaitCursor
        Me.InitPolicyInfo()
        Me.InitDataset()
        wndMain.Cursor = Cursors.Default
    End Sub

    Private Sub InitPolicyInfo()
        Try
            dsPolicyHolder = GetCustomerByPolicyNo(strPolicy, "PH")
            dsPolicyInsured = GetCustomerByPolicyNo(strPolicy, "PI")

            If dsPolicyHolder.Tables.Count > 0 AndAlso dsPolicyHolder.Tables(0).Rows.Count > 0 Then

                CustomerID = dsPolicyHolder.Tables(0).Rows(0).Item("customerID").ToString.Trim

                Dim strPolicyHolderName As String = String.Empty
                Dim strPolicyHolderMobile As String = String.Empty

                If dsPolicyHolder.Tables(0).Rows(0).Item("NameSuffix").ToString.Trim <> "" And dsPolicyHolder.Tables(0).Rows(0).Item("NameSuffix").ToString.Trim <> "" Then
                    strPolicyHolderName = dsPolicyHolder.Tables(0).Rows(0).Item("NameSuffix").ToString.Trim & " " & dsPolicyHolder.Tables(0).Rows(0).Item("FirstName").ToString.Trim
                End If

                If dsPolicyHolder.Tables(0).Rows(0).Item("ChiLstNm").ToString.Trim <> "" And dsPolicyHolder.Tables(0).Rows(0).Item("ChiFstNm").ToString.Trim <> "" Then
                    strPolicyHolderName = strPolicyHolderName & " (" & dsPolicyHolder.Tables(0).Rows(0).Item("ChiLstNm").ToString.Trim & dsPolicyHolder.Tables(0).Rows(0).Item("ChiFstNm").ToString.Trim & ")"
                End If

                strPolicyHolderMobile = dsPolicyHolder.Tables(0).Rows(0).Item("PhoneMobileAreaCode").ToString.Trim & dsPolicyHolder.Tables(0).Rows(0).Item("PhoneMobile").ToString.Trim


                Me.lblPolicyHolderName.Text = strPolicyHolderName
                Me.lblPolicyHdrMobile.Text = strPolicyHolderMobile
            End If


            If dsPolicyInsured.Tables.Count > 0 AndAlso dsPolicyInsured.Tables(0).Rows.Count > 0 Then

                Dim strPolicyInsureName As String = String.Empty

                If dsPolicyInsured.Tables(0).Rows(0).Item("NameSuffix").ToString.Trim <> "" And dsPolicyInsured.Tables(0).Rows(0).Item("NameSuffix").ToString.Trim <> "" Then
                    strPolicyInsureName = dsPolicyInsured.Tables(0).Rows(0).Item("NameSuffix").ToString.Trim & " " & dsPolicyInsured.Tables(0).Rows(0).Item("FirstName").ToString.Trim
                End If

                If dsPolicyInsured.Tables(0).Rows(0).Item("ChiLstNm").ToString.Trim <> "" And dsPolicyInsured.Tables(0).Rows(0).Item("ChiFstNm").ToString.Trim <> "" Then
                    strPolicyInsureName = strPolicyInsureName & " (" & dsPolicyInsured.Tables(0).Rows(0).Item("ChiLstNm").ToString.Trim & dsPolicyInsured.Tables(0).Rows(0).Item("ChiFstNm").ToString.Trim & ")"
                End If

                Me.lblInsuredName.Text = strPolicyInsureName
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function GetCustomerByPolicyNo(ByVal strPolicyNo As String, ByVal strRelCode As String) As DataSet
        Dim ds As New DataSet
        Dim strSQL As String

        sqlConn.ConnectionString = strCIWConn

        strSQL = "select rel.PolicyRelateCode, rel.customerID, cus.NameSuffix, cus.FirstName, cus.ChiLstNm, cus.ChiFstNm, cus.LanguageCode, cus.PhoneMobileAreaCode, cus.PhoneMobile from PolicyAccount poli" & _
                " inner join csw_poli_rel rel on rel.PolicyAccountID = poli.PolicyAccountID and rel.PolicyRelateCode ='" & strRelCode & "'" & _
                " inner join customer cus on cus.CustomerID = rel.CustomerID" & _
                " where poli.PolicyAccountID = '" & strPolicyNo & "'"

        daPolicyHolder = New SqlDataAdapter(strSQL, sqlConn)
        daPolicyHolder.Fill(ds)

        Return ds
    End Function

    Private Sub InitDataset()
        Dim strSQL As String

        sqlConn.ConnectionString = strCIWConn
        'oliver 2024-7-5 added for Table_Relocate_Sprint 14
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
        strSQL = " Select smsmsd_create_date,smsmsd_create_by, smsmsd_mob_no, smsmsd_msg_content, smsmsg_msg_desc " &
                 " From sms_message_details dtl " &
                 " Inner join " & serverPrefix & " sms_msg_status sts on sts.smsmsg_msg_status= dtl.smsmsd_msg_status " &
                 " Where smsmsd_poli_no = '" & strPolicy & "'" &
                 " and smsmsd_category='" & strMsgCategory & "' "

        Try
            dsSMS.Tables.Clear()

            'Fill the datatables in dataset
            daSMSmsg = New SqlDataAdapter(strSQL, sqlConn)
            daSMSmsg.MissingSchemaAction = MissingSchemaAction.AddWithKey
            daSMSmsg.MissingMappingAction = MissingMappingAction.Passthrough
            daSMSmsg.Fill(dsSMS, "sms_message_details")

            dgSMSLog.DataSource = dsSMS.Tables("sms_message_details").DefaultView
            dgSMSLog.AutoGenerateColumns = False
            dgSMSLog.Refresh()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub UpdateSMSStatus()
        If dgSMSLog.CurrentRow IsNot Nothing AndAlso dgSMSLog.CurrentRow.Index >= 0 Then
            dr = dsSMS.Tables("sms_message_details").Rows(dgSMSLog.CurrentRow.Index)

            Me.txtMobileNo.Text = dr.Item("smsmsd_mob_no")
            Me.txtSentDate.Text = CDate(dr.Item("smsmsd_create_date")).ToString("MM/dd/yyyy")
            Me.txtSentUser.Text = dr.Item("smsmsd_create_by")
            Me.txtMsgSts.Text = dr.Item("smsmsg_msg_desc")
            Me.txtMsgContents.Text = dr.Item("smsmsd_msg_content")
        End If
    End Sub

    Private Function PendingSMS(ByVal strPolicyNo As String) As Boolean
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim strSQL As String

        sqlConn.ConnectionString = strCIWConn

        strSQL = "select * from sms_message_details (nolock) " &
                    " where smsmsd_category='" & strMsgCategory & "' " &
                    " and smsmsd_msg_status ='Q' " &
                    " and smsmsd_exp_date <= CONVERT(varchar,CONVERT(date, GETDATE() , 121)) + ' 23:59:59.000' and smsmsd_poli_no ='" & strPolicyNo & "'"

        da = New SqlDataAdapter(strSQL, sqlConn)
        da.Fill(ds)

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

            If ds.Tables(0).Rows.Count > 0 Then
                Return True  'Has SMS Pending to send
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Private Sub SendSMS(ByVal strPolicy As String, ByRef strErrMsg As String)
        Dim strLangFlag As String
        Dim strMobileNo As String
        Dim strHolderName As String

        If Me.rbtnEng.Checked Then
            strLangFlag = "1"
        ElseIf Me.rbtnChi.Checked Then
            strLangFlag = "0"
        End If

        strMobileNo = Me.lblPolicyHdrMobile.Text
        strHolderName = Me.lblPolicyHolderName.Text

        SendiLASSMS(gsUser, CustomerID, strLangFlag, strMobileNo, Now.ToString("yyyy-MM-dd 00:00:00"), Now.ToString("yyyy-MM-dd 23:59:59"), strMsgCategory, "D:20015 & " & PolicyAccountID & " &" & strHolderName & ";", "", PolicyAccountID, strErrMsg)

    End Sub

#End Region

#Region " Windows Form Designer generated code "
    Public Sub New(init As Boolean)
        MyBase.New()
    End Sub
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub


    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sqlConn As New SqlConnection
    Friend WithEvents daSMSmsg As New SqlDataAdapter
    Friend WithEvents daSMSstatus As New SqlDataAdapter
    Friend WithEvents daPolicyHolder As New SqlDataAdapter
    Friend WithEvents daPolicyInsured As New SqlDataAdapter
    Friend WithEvents dgSMSLog As System.Windows.Forms.DataGridView
    Friend WithEvents dsSMS As New DataSet
    Friend WithEvents dsPolicyHolder As New DataSet
    Friend WithEvents dsPolicyInsured As New DataSet

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtMsgSts = New System.Windows.Forms.TextBox
        Me.txtMsgContents = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.rbtnEng = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblPolicyHdrMobile = New System.Windows.Forms.Label
        Me.lblInsuredName = New System.Windows.Forms.Label
        Me.lblPolicyHolderName = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSentUser = New System.Windows.Forms.TextBox
        Me.txtSentDate = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtMobileNo = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.dgSMSLog = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnSendSMSiLAS = New System.Windows.Forms.Button
        Me.rbtnChi = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgSMSLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(11, 261)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(90, 13)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Message Content"
        '
        'txtMsgSts
        '
        Me.txtMsgSts.Location = New System.Drawing.Point(104, 222)
        Me.txtMsgSts.Name = "txtMsgSts"
        Me.txtMsgSts.ReadOnly = True
        Me.txtMsgSts.Size = New System.Drawing.Size(308, 20)
        Me.txtMsgSts.TabIndex = 6
        '
        'txtMsgContents
        '
        Me.txtMsgContents.Location = New System.Drawing.Point(104, 260)
        Me.txtMsgContents.Multiline = True
        Me.txtMsgContents.Name = "txtMsgContents"
        Me.txtMsgContents.ReadOnly = True
        Me.txtMsgContents.Size = New System.Drawing.Size(570, 59)
        Me.txtMsgContents.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Policy Holder "
        '
        'rbtnEng
        '
        Me.rbtnEng.AutoSize = True
        Me.rbtnEng.Checked = True
        Me.rbtnEng.Location = New System.Drawing.Point(10, 20)
        Me.rbtnEng.Name = "rbtnEng"
        Me.rbtnEng.Size = New System.Drawing.Size(59, 17)
        Me.rbtnEng.TabIndex = 0
        Me.rbtnEng.TabStop = True
        Me.rbtnEng.Text = "English"
        Me.rbtnEng.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPolicyHdrMobile)
        Me.GroupBox1.Controls.Add(Me.lblInsuredName)
        Me.GroupBox1.Controls.Add(Me.lblPolicyHolderName)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(679, 85)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Policy Info"
        '
        'lblPolicyHdrMobile
        '
        Me.lblPolicyHdrMobile.AutoSize = True
        Me.lblPolicyHdrMobile.Location = New System.Drawing.Point(342, 29)
        Me.lblPolicyHdrMobile.Name = "lblPolicyHdrMobile"
        Me.lblPolicyHdrMobile.Size = New System.Drawing.Size(0, 13)
        Me.lblPolicyHdrMobile.TabIndex = 6
        '
        'lblInsuredName
        '
        Me.lblInsuredName.AutoSize = True
        Me.lblInsuredName.Location = New System.Drawing.Point(85, 59)
        Me.lblInsuredName.Name = "lblInsuredName"
        Me.lblInsuredName.Size = New System.Drawing.Size(0, 13)
        Me.lblInsuredName.TabIndex = 5
        '
        'lblPolicyHolderName
        '
        Me.lblPolicyHolderName.AutoSize = True
        Me.lblPolicyHolderName.Location = New System.Drawing.Point(85, 29)
        Me.lblPolicyHolderName.Name = "lblPolicyHolderName"
        Me.lblPolicyHolderName.Size = New System.Drawing.Size(0, 13)
        Me.lblPolicyHolderName.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 59)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Insured"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(278, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Mobile No."
        '
        'txtSentUser
        '
        Me.txtSentUser.Location = New System.Drawing.Point(506, 189)
        Me.txtSentUser.Name = "txtSentUser"
        Me.txtSentUser.ReadOnly = True
        Me.txtSentUser.Size = New System.Drawing.Size(100, 20)
        Me.txtSentUser.TabIndex = 7
        '
        'txtSentDate
        '
        Me.txtSentDate.Location = New System.Drawing.Point(312, 189)
        Me.txtSentDate.Name = "txtSentDate"
        Me.txtSentDate.ReadOnly = True
        Me.txtSentDate.Size = New System.Drawing.Size(100, 20)
        Me.txtSentDate.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(11, 229)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Message Status"
        '
        'txtMobileNo
        '
        Me.txtMobileNo.Location = New System.Drawing.Point(104, 189)
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.ReadOnly = True
        Me.txtMobileNo.Size = New System.Drawing.Size(100, 20)
        Me.txtMobileNo.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(446, 195)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 13)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Sent User"
        '
        'dgSMSLog
        '
        Me.dgSMSLog.AllowUserToAddRows = False
        Me.dgSMSLog.AllowUserToDeleteRows = False
        Me.dgSMSLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSMSLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column5, Me.Column3, Me.Column4})
        Me.dgSMSLog.Location = New System.Drawing.Point(10, 29)
        Me.dgSMSLog.Name = "dgSMSLog"
        Me.dgSMSLog.ReadOnly = True
        Me.dgSMSLog.Size = New System.Drawing.Size(664, 144)
        Me.dgSMSLog.TabIndex = 6
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "smsmsd_create_date"
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column1.HeaderText = "Sent Date"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "smsmsd_mob_no"
        Me.Column2.HeaderText = "Mobile No."
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 85
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "smsmsd_msg_content"
        Me.Column5.FillWeight = 200.0!
        Me.Column5.HeaderText = "Message"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 300
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "smsmsg_msg_desc"
        Me.Column3.HeaderText = "Message Status"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 250
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "smsmsd_create_by"
        Me.Column4.HeaderText = "Sent User"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 80
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 192)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Mobile No."
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnSendSMSiLAS)
        Me.GroupBox3.Controls.Add(Me.rbtnChi)
        Me.GroupBox3.Controls.Add(Me.rbtnEng)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 442)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(679, 50)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Send iLAS SMS"
        '
        'btnSendSMSiLAS
        '
        Me.btnSendSMSiLAS.Location = New System.Drawing.Point(552, 19)
        Me.btnSendSMSiLAS.Name = "btnSendSMSiLAS"
        Me.btnSendSMSiLAS.Size = New System.Drawing.Size(118, 23)
        Me.btnSendSMSiLAS.TabIndex = 2
        Me.btnSendSMSiLAS.Text = "Send iLAS SMS"
        Me.btnSendSMSiLAS.UseVisualStyleBackColor = True
        '
        'rbtnChi
        '
        Me.rbtnChi.AutoSize = True
        Me.rbtnChi.Location = New System.Drawing.Point(100, 20)
        Me.rbtnChi.Name = "rbtnChi"
        Me.rbtnChi.Size = New System.Drawing.Size(63, 17)
        Me.rbtnChi.TabIndex = 1
        Me.rbtnChi.Text = "Chinese"
        Me.rbtnChi.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.dgSMSLog)
        Me.GroupBox2.Controls.Add(Me.txtMsgContents)
        Me.GroupBox2.Controls.Add(Me.txtSentUser)
        Me.GroupBox2.Controls.Add(Me.txtMsgSts)
        Me.GroupBox2.Controls.Add(Me.txtSentDate)
        Me.GroupBox2.Controls.Add(Me.txtMobileNo)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 94)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(679, 333)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SMS Details"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(239, 189)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Sent Date"
        '
        'SMSiLAS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "SMSiLAS"
        Me.Size = New System.Drawing.Size(687, 504)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgSMSLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMsgSts As System.Windows.Forms.TextBox
    Friend WithEvents txtMsgContents As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rbtnEng As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPolicyHdrMobile As System.Windows.Forms.Label
    Friend WithEvents lblInsuredName As System.Windows.Forms.Label
    Friend WithEvents lblPolicyHolderName As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSentUser As System.Windows.Forms.TextBox
    Friend WithEvents txtSentDate As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSendSMSiLAS As System.Windows.Forms.Button
    Friend WithEvents rbtnChi As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label

#End Region


End Class
