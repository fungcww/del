Imports System.Data.SqlClient

Public Class uclCustomerRel
    Inherits System.Windows.Forms.UserControl

    Dim sqlda As SqlDataAdapter
    Dim sqlconnect As New SqlConnection
    Dim sqlcb As SqlCommandBuilder
    Dim strSQL, strMode As String

    Dim strHolderID, strHolderName, strMbrID, strMbrLName, strMbrFName, strMbrCName, strRel, strHKID, strRelCode, strGender
    Dim datDOB As DateTime
    Dim dtRel As DataTable
    Dim blnNew As Boolean = False
    Dim blnSearch As Boolean = False
    Dim bm As BindingManagerBase

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        grpButton.Visible = CheckUPSAccess("Customer Relationship")

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.Label
    Friend WithEvents cboRel As System.Windows.Forms.ComboBox
    Friend WithEvents cboGender As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDOB As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtHKID As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtMbrID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents grpButton As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.cboGender = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDOB = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtHKID = New System.Windows.Forms.TextBox()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtMbrID = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboRel = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpButton = New System.Windows.Forms.GroupBox()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout
        Me.grpButton.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtSource)
        Me.GroupBox1.Controls.Add(Me.cboGender)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtDOB)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtFirstName)
        Me.GroupBox1.Controls.Add(Me.cmdSearch)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtHKID)
        Me.GroupBox1.Controls.Add(Me.txtLastName)
        Me.GroupBox1.Controls.Add(Me.txtMbrID)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cboRel)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(790, 193)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Relationship"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(8, 158)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 32)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "Source of Information:"
        '
        'txtSource
        '
        Me.txtSource.BackColor = System.Drawing.SystemColors.Window
        Me.txtSource.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSource.Location = New System.Drawing.Point(106, 154)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ReadOnly = true
        Me.txtSource.Size = New System.Drawing.Size(678, 26)
        Me.txtSource.TabIndex = 69
        '
        'cboGender
        '
        Me.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGender.Enabled = false
        Me.cboGender.Items.AddRange(New Object() {"M", "F", "U"})
        Me.cboGender.Location = New System.Drawing.Point(391, 122)
        Me.cboGender.Name = "cboGender"
        Me.cboGender.Size = New System.Drawing.Size(56, 28)
        Me.cboGender.TabIndex = 68
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(318, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 20)
        Me.Label7.TabIndex = 74
        Me.Label7.Text = "Gender:"
        '
        'txtDOB
        '
        Me.txtDOB.Enabled = false
        Me.txtDOB.Location = New System.Drawing.Point(106, 122)
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.Size = New System.Drawing.Size(200, 26)
        Me.txtDOB.TabIndex = 67
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(8, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 20)
        Me.Label1.TabIndex = 72
        Me.Label1.Text = "First Name:"
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFirstName.Location = New System.Drawing.Point(106, 91)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = true
        Me.txtFirstName.Size = New System.Drawing.Size(373, 26)
        Me.txtFirstName.TabIndex = 66
        '
        'cmdSearch
        '
        Me.cmdSearch.Enabled = false
        Me.cmdSearch.Location = New System.Drawing.Point(387, 24)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(92, 27)
        Me.cmdSearch.TabIndex = 70
        Me.cmdSearch.Text = "Search"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(8, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 20)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "DOB:"
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(214, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 20)
        Me.Label8.TabIndex = 68
        Me.Label8.Text = "CustID:"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(8, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 20)
        Me.Label6.TabIndex = 67
        Me.Label6.Text = "HKID:"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(8, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 20)
        Me.Label5.TabIndex = 66
        Me.Label5.Text = "Last Name:"
        '
        'txtHKID
        '
        Me.txtHKID.BackColor = System.Drawing.SystemColors.Window
        Me.txtHKID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHKID.Location = New System.Drawing.Point(106, 24)
        Me.txtHKID.Name = "txtHKID"
        Me.txtHKID.ReadOnly = true
        Me.txtHKID.Size = New System.Drawing.Size(84, 26)
        Me.txtHKID.TabIndex = 63
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLastName.Location = New System.Drawing.Point(106, 58)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = true
        Me.txtLastName.Size = New System.Drawing.Size(104, 26)
        Me.txtLastName.TabIndex = 65
        '
        'txtMbrID
        '
        Me.txtMbrID.BackColor = System.Drawing.SystemColors.Window
        Me.txtMbrID.Location = New System.Drawing.Point(287, 24)
        Me.txtMbrID.Name = "txtMbrID"
        Me.txtMbrID.ReadOnly = true
        Me.txtMbrID.Size = New System.Drawing.Size(80, 26)
        Me.txtMbrID.TabIndex = 64
        '
        'txtName
        '
        Me.txtName.AutoSize = true
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Location = New System.Drawing.Point(488, 91)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(51, 22)
        Me.txtName.TabIndex = 34
        Me.txtName.Text = "<PH>"
        Me.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136,Byte))
        Me.Label3.Location = New System.Drawing.Point(484, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 20)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "is"
        '
        'cboRel
        '
        Me.cboRel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRel.Enabled = false
        Me.cboRel.Items.AddRange(New Object() {"Parent (PR)", "Daughter/Son (DS)", "Spouse (SP)", "Employer (ER)"})
        Me.cboRel.Location = New System.Drawing.Point(514, 55)
        Me.cboRel.Name = "cboRel"
        Me.cboRel.Size = New System.Drawing.Size(160, 28)
        Me.cboRel.TabIndex = 70
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136,Byte))
        Me.Label2.Location = New System.Drawing.Point(680, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 20)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "of"
        '
        'grpButton
        '
        Me.grpButton.Controls.Add(Me.cmdAdd)
        Me.grpButton.Controls.Add(Me.cmdDelete)
        Me.grpButton.Controls.Add(Me.cmdCancel)
        Me.grpButton.Controls.Add(Me.cmdSave)
        Me.grpButton.Controls.Add(Me.cmdEdit)
        Me.grpButton.Location = New System.Drawing.Point(386, 203)
        Me.grpButton.Name = "grpButton"
        Me.grpButton.Size = New System.Drawing.Size(408, 62)
        Me.grpButton.TabIndex = 96
        Me.grpButton.TabStop = false
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(8, 16)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 37)
        Me.cmdAdd.TabIndex = 96
        Me.cmdAdd.Text = "&Add"
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(168, 16)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(75, 37)
        Me.cmdDelete.TabIndex = 97
        Me.cmdDelete.Text = "&Delete"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = false
        Me.cmdCancel.Location = New System.Drawing.Point(328, 16)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 37)
        Me.cmdCancel.TabIndex = 98
        Me.cmdCancel.Text = "&Cancel"
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = false
        Me.cmdSave.Location = New System.Drawing.Point(248, 16)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 37)
        Me.cmdSave.TabIndex = 99
        Me.cmdSave.Text = "&Save"
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(88, 16)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(75, 37)
        Me.cmdEdit.TabIndex = 100
        Me.cmdEdit.Text = "&Edit"
        '
        'uclCustomerRel
        '
        Me.Controls.Add(Me.grpButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "uclCustomerRel"
        Me.Size = New System.Drawing.Size(805, 275)
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.grpButton.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

#End Region

    Public WriteOnly Property Editable() As Boolean
        Set(ByVal Value As Boolean)
        End Set
    End Property

    Public WriteOnly Property HolderID(ByVal HolderName As String, ByVal MbrID As String, ByVal MbrLastName As String, ByVal MbrFirstName As String, _
            ByVal MbrCName As String, ByVal Rel As String, ByVal HKID As String, ByVal DOB As DateTime, ByVal Gender As String) As String
        Set(ByVal Value As String)
            strHolderID = Value
            strHolderName = HolderName
            strMbrID = MbrID
            strMbrLName = MbrLastName
            strMbrFName = MbrFirstName
            strMbrCName = MbrCName
            strRel = Rel
            strHKID = HKID
            strGender = Gender
            datDOB = DOB
            LoadDefault()
        End Set
    End Property

    Private Sub EnableFields(ByVal blnStatus)

        Me.cboRel.Enabled = Not blnStatus
        Me.cmdSave.Enabled = Not blnStatus
        Me.cmdCancel.Enabled = Not blnStatus
        Me.cmdSearch.Enabled = Not blnStatus
        Me.cmdAdd.Enabled = blnStatus
        Me.cmdEdit.Enabled = blnStatus
        Me.cmdDelete.Enabled = blnStatus

        If strMode = "A" Then
            Me.txtHKID.ReadOnly = blnStatus
            Me.txtMbrID.ReadOnly = blnStatus
            Me.txtLastName.ReadOnly = blnStatus
            Me.txtFirstName.ReadOnly = blnStatus
            Me.txtSource.ReadOnly = blnStatus
            Me.cboGender.Enabled = Not blnStatus
            Me.txtSource.ReadOnly = blnStatus
            Me.txtDOB.Enabled = Not blnStatus
        End If

    End Sub

    Private Sub LoadDefault()

        ' special relation, disable all function
        If UCase(strRel) = "SELF" Then
            cmdAdd.Enabled = True
            cmdEdit.Enabled = False
            cmdDelete.Enabled = False

            Me.txtDOB.Text = ""
            Me.txtHKID.Text = ""
            Me.txtMbrID.Text = ""
            Me.txtLastName.Text = ""
            Me.txtFirstName.Text = ""
            Me.txtSource.Text = ""
            Me.cboRel.SelectedIndex = 0

        Else
            Me.txtDOB.Value = datDOB

            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            ''Me.txtHKID.Text = strHKID
            'If ExternalUser And Not IsDBNull(strHKID) And String.IsNullOrEmpty(strHKID) Then
            '    txtHKID.Text = MaskExternalUserData(MaskData.HKID, strHKID)
            'Else
            '    txtHKID.Text = strHKID
            'End If
            ''CRS 7x24 Changes - End
            txtHKID.Text = strHKID

            Me.txtMbrID.Text = strMbrID
            Me.txtLastName.Text = strMbrLName
            Me.txtFirstName.Text = strMbrFName

            For i As Integer = 0 To cboGender.Items.Count - 1
                If cboGender.Items(i)(0) = strGender Then
                    cboGender.SelectedIndex = i
                End If
            Next

            For i As Integer = 0 To cboRel.Items.Count - 1
                If cboRel.Items(i)(1) = strRel Then
                    cboRel.SelectedIndex = i
                    strRelCode = cboRel.Items(i)(0)
                    Exit For
                End If
            Next
            'Me.cboRel.SelectedItem = strRel
            EnableFields(True)
        End If

        Me.txtName.Text = strHolderName

    End Sub

    Private Sub FillCombobox()

        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select 'SELF' as cswrld_relationship,'SELF' as cswrld_desc UNION ALL Select cswrld_relationship, cswrld_desc From " & serverPrefix & "csw_customer_rel_code"

        Try
            If dtRel Is Nothing Then
                dtRel = New DataTable
            End If

            dtRel.Clear()
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(dtRel)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        dtRel.TableName = "csw_customer_rel_code"
        cboRel.DataSource = dtRel
        cboRel.DisplayMember = "cswrld_desc"
        cboRel.ValueMember = "cswrld_relationship"

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        strMode = "E"
        blnNew = False
        blnSearch = False
        EnableFields(False)
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Dim strSQL As String

        If MsgBox("Do you want to delete the selected record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gSystem) = MsgBoxResult.Yes Then

            strSQL = "Delete From " & serverPrefix & "csw_customer_rel " &
                " Where cswcrl_person1_id = " & strHolderID &
                " And cswcrl_relationship = '" & cboRel.SelectedValue & "'" &
                " And cswcrl_person2_id = " & strMbrID

            sqlconnect.ConnectionString = strCIWConn
            sqlconnect.Open()

            sqlcmd = New SqlCommand
            sqlcmd.Connection = sqlconnect
            sqlcmd.CommandText = strSQL

            Try
                sqlcmd.ExecuteNonQuery()
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Finally
                sqlconnect.Close()
                sqlcmd.Dispose()
            End Try

            Dim strLog As String
            strLog = "Delete customer relation of client " & strHolderID & ", Name = " & strHolderName & vbCrLf
            strLog &= "CustomerId=" & strMbrID & ", Name=" & Trim(strMbrLName) & " " & Trim(strMbrFName) & ", HKID = " & strHKID & ", Gender=" & strGender & ", DOB=" & CStr(datDOB) & ", Rel=" & strRel & vbCrLf

            If AddAuditTrail(strHolderID, "Delete Customer Relationship", strLog) = False Then
                MsgBox("Error writing audit trail entry", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
            End If

            MsgBox("Record deleted successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            CType(ParentForm, frmCustomer_Asur).FillCust()

        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim sqlconnect As New SqlConnection
        Dim sqlreader As SqlDataReader
        Dim sqlcmd As New SqlCommand
        Dim strLog As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If cboRel.SelectedValue = "SELF" Then
            MsgBox("Invalid Relationship - SELF.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gSystem)
            Exit Sub
        End If

        If Not blnSearch AndAlso Not blnNew Then
            MsgBox("Please search the customer first", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gSystem)
            Exit Sub
        Else
            sqlconnect.ConnectionString = strCIWConn

            If strMode = "A" Then

                If blnNew Then
                    strSQL = "Declare @custID as numeric " &
                        " Select @custID = max(customerid)+1 from customer where customerid > 90000000 and customerid < 99999999 " &
                        " Insert Into Customer (CustomerID, CustomerTypeCode, NamePrefix, FirstName, NameSuffix, DateOfBirth, Gender, GovernmentIDCard, " &
                        "                       DateofLastChange, EmailAddr, CreateDate, CreateUser, LstUpdDate, LstUpdUser, CompanyID) " &
                        " Select @custID, 'PC', '', '" &
                        Replace(txtLastName.Text, "'", "''") & "', '" &
                        Replace(txtFirstName.Text, "'", "''") & "', '" &
                        Format(txtDOB.Value, "MM/dd/yyyy") & "', '" &
                        cboGender.SelectedItem & "', '" &
                        txtHKID.Text & "', GETDATE(), '', GETDATE(), '" & gsUser & "', GETDATE(), '" & gsUser & "', 'EAA'"

                    strSQL &= " Insert Into " & serverPrefix & "csw_misc_info (cswmif_type, cswmif_customer_id, cswmif_provider, cswmif_create_user, cswmif_create_date, cswmif_update_user, cswmif_update_date) " &
                        " Select 'EXTCL', @custID, '" & Replace(RTrim(txtSource.Text), "'", "''") & "','" & gsUser & "', GETDATE(), '" & gsUser & "', GETDATE()"

                    strSQL &= " Insert into " & serverPrefix & "csw_customer_rel " &
                        " (cswcrl_person1_id, cswcrl_relationship, cswcrl_person2_id, cswcrl_create_user, cswcrl_create_date, cswcrl_update_user, cswcrl_update_date) " &
                        " Values (" & strHolderID & ",'" & cboRel.SelectedValue & "',@custID,'" & gsUser & "', GETDATE(), '" & gsUser & "', GETDATE())"
                Else
                    strSQL = "Insert into " & serverPrefix & "csw_customer_rel " &
                        " (cswcrl_person1_id, cswcrl_relationship, cswcrl_person2_id, cswcrl_create_user, cswcrl_create_date, cswcrl_update_user, cswcrl_update_date) " &
                        " Values (" & strHolderID & ",'" & cboRel.SelectedValue & "'," & txtMbrID.Text & ",'" & gsUser & "', GETDATE(), '" & gsUser & "', GETDATE())"
                End If

                strLog = "Add customer relation to client " & strHolderID & ", Name = " & strHolderName & vbCrLf
                strLog &= "CustomerId=" & txtMbrID.Text & ", Name=" & Trim(Me.txtLastName.Text) & " " & Trim(txtFirstName.Text) & ", HKID = " & txtHKID.Text & ", Gender=" & Me.cboGender.SelectedItem & ", DOB=" & CStr(txtDOB.Text)
                strLog &= ", Rel=" & cboRel.SelectedItem(1) & vbCrLf
            Else
                strSQL = "Update " & serverPrefix & "csw_customer_rel " &
                    " Set cswcrl_relationship = '" & cboRel.SelectedValue & "', " &
                    " cswcrl_person2_id = " & txtMbrID.Text & ", " &
                    " cswcrl_update_user = '" & gsUser & "', " &
                    " cswcrl_update_date = GETDATE() " &
                    " Where cswcrl_person1_id = " & strHolderID &
                    " And cswcrl_relationship = '" & strRelCode & "'" &
                    " And cswcrl_person2_id = " & strMbrID

                If strMbrID <> txtMbrID.Text Or strRel <> cboRel.SelectedValue Then
                    strLog = "Edit customer relation of client " & strHolderID & ", Name " & strHolderName & vbCrLf
                    strLog &= "Original Value:" & vbCrLf
                    strLog &= "CustomerId=" & strMbrID & ", Name=" & Trim(strMbrLName) & " " & Trim(strMbrFName) & ", HKID = " & strHKID & ", Gender=" & strGender & ", DOB=" & CStr(datDOB) & ", Rel=" & strRel & vbCrLf
                    strLog &= "New Value:" & vbCrLf
                End If

                If strMbrID <> txtMbrID.Text Then
                    strLog &= "CustomerId=" & txtMbrID.Text & ", Name=" & Trim(Me.txtLastName.Text) & " " & Trim(txtFirstName.Text) & ", HKID = " & txtHKID.Text & ", Gender=" & Me.cboGender.SelectedItem & ", DOB=" & CStr(txtDOB.Text) & ", "
                End If

                If strRel <> cboRel.SelectedValue Then
                    strLog &= "Rel=" & cboRel.SelectedItem(1) & vbCrLf
                End If

            End If

            Try
                sqlconnect.Open()
                sqlcmd = New SqlCommand(strSQL, sqlconnect)
                sqlcmd.ExecuteNonQuery()
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Finally
                sqlconnect.Close()
                sqlcmd.Dispose()
            End Try

            If AddAuditTrail(strHolderID, "Amend Customer Relationship", strLog) = False Then
                MsgBox("Error writing audit trail entry", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            EnableFields(True)
            CType(ParentForm, frmCustomer_Asur).FillCust()

        End If

    End Sub

    Private Sub cmdCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        LoadDefault()
        EnableFields(True)
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        strMode = "A"
        blnNew = False
        blnSearch = False
        Me.txtDOB.Value = #1/1/1900#
        Me.txtHKID.Text = ""
        Me.txtMbrID.Text = ""
        Me.txtLastName.Text = ""
        Me.txtFirstName.Text = ""
        Me.cboGender.SelectedIndex = -1
        Me.cboRel.SelectedItem = -1
        EnableFields(False)
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        Dim sqlconnect As New SqlConnection
        Dim sqlreader As SqlDataReader
        Dim sqlcmd As SqlCommand

        'txtLastName.Text = ""
        'txtFirstName.Text = ""
        'txtDOB.Text = ""
        'cboGender.SelectedIndex = 0
        'txtDOB.Text = ""
        blnNew = False

        If RTrim(txtHKID.Text) = "" AndAlso RTrim(txtMbrID.Text) = "" Then
            MsgBox("Please input HKID or CustomerID to search.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Exit Sub
        Else
            sqlconnect.ConnectionString = strCIWConn

            If RTrim(txtHKID.Text) <> "" Then
                strSQL = " Where GovernmentIDCard = '" & Replace(RTrim(txtHKID.Text), "'", "''") & "'"

                If cboGender.SelectedIndex > 0 Then
                    strSQL &= " And Gender = '" & cboGender.SelectedItem & "'"
                End If

                If txtDOB.Value <> #1/1/1900# Then
                    strSQL &= " And DateOfBirth = '" & Format(txtDOB.Value, "MM/dd/yyyy") & "'"
                End If
            Else
                If RTrim(txtMbrID.Text) <> "" Then
                    strSQL = " Where CustomerID = " & Replace(RTrim(txtMbrID.Text), "'", "''")
                End If
            End If


            If strSQL <> "" Then
                strSQL = "Select CustomerID, GovernmentIDCard, DateOfBirth, " & _
                    " NamePrefix, NameSuffix, FirstName, Gender " & _
                    " From customer " & strSQL
            End If

            Try
                sqlconnect.Open()
                sqlcmd = New SqlCommand(strSQL, sqlconnect)
                sqlreader = sqlcmd.ExecuteReader

                If sqlreader.HasRows Then
                    sqlreader.Read()
                    With sqlreader

                        If .Item("CustomerID") = strHolderID Then
                            MsgBox("Searching customer is policy holder", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
                        Else
                            txtMbrID.Text = .Item("CustomerID")
                            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                            'If ExternalUser And Not IsDBNull(.Item("GovernmentIDCard")) And String.IsNullOrEmpty(.Item("GovernmentIDCard")) Then
                            '    txtHKID.Text = MaskExternalUserData(MaskData.HKID, .Item("GovernmentIDCard"))
                            'Else
                            '    txtHKID.Text = .Item("GovernmentIDCard")
                            'End If
                            txtHKID.Text = .Item("GovernmentIDCard")
                            txtLastName.Text = IIf(IsDBNull(.Item("NameSuffix")), "", .Item("NameSuffix"))
                            txtFirstName.Text = IIf(IsDBNull(.Item("FirstName")), "", .Item("FirstName"))
                            cboGender.SelectedItem = IIf(IsDBNull(.Item("Gender")), "", .Item("Gender"))
                            txtDOB.Text = IIf(IsDBNull(.Item("DateOfBirth")), "", .Item("DateOfBirth"))
                            blnSearch = True
                        End If

                    End With
                Else
                    If MsgBox("Customer not found, create new customer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gSystem) = MsgBoxResult.Yes Then

                        If txtSource.Text = "" OrElse txtLastName.Text = "" OrElse txtFirstName.Text = "" OrElse cboGender.SelectedIndex < 0 Then
                            MsgBox("Please fill in all new customer information", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
                        Else
                            txtMbrID.Text = "<New>"
                            blnNew = True
                        End If
                    Else
                        blnNew = False
                    End If
                End If
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Finally
                sqlreader.Close()
                sqlconnect.Close()
                sqlcmd.Dispose()
            End Try
        End If
    End Sub

    Private Sub uclCustomerRel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If strCIWConn <> "" Then
            FillCombobox()
        End If

        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    cmdSearch.Enabled = False
        '    cmdAdd.Enabled = False
        '    cmdEdit.Enabled = False
        '    cmdDelete.Enabled = False
        '    cmdSave.Enabled = False
        '    cmdCancel.Enabled = False
        'End If
        ''CRS 7x24 Changes - End
    End Sub
End Class
