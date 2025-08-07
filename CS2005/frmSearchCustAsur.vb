'******************************************************************
' Description : Search GI customer
' Date		  : 7/30/2007
' Author	  : Eric Shu (ES01)
'******************************************************************
' Description : Modify table from CAPSIL to SQL
' Date		  : 11/30/2015
' Author	  : Harry Tan (HT01)
'******************************************************************
' Description : Customer Level Search Issue
' Display policy for all IdCards instead of just CustomerId
' Distinguish  all  displayed Service log base on whether the customer contains an agentcode field to filter
' Date		  : 13 Sep 2023
' Author	  : oliver ou 22834
'******************************************************************
' Description : HNW Expansion - Integrated Customer Search
' Date		  : 12 Jan 2025
' Author	  : Chrysan Cheng
'******************************************************************

Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text.RegularExpressions



Public Class frmSearchCustAsur
    Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' The max limit of searches after extended
    ''' </summary>
    Private Const EXTENDED_SEARCH_LIMIT As Integer = 400

    Private sqldt As DataTable
    Private bm As BindingManagerBase

    Friend WithEvents pnlRdoGrpCompany As System.Windows.Forms.Panel
    Friend WithEvents rbParallel As System.Windows.Forms.RadioButton
    Friend WithEvents rbMacau As System.Windows.Forms.RadioButton
    Friend WithEvents rbAssurance As System.Windows.Forms.RadioButton
    Friend WithEvents rbBermuda As System.Windows.Forms.RadioButton
    Friend WithEvents pnlSrhCriBemuda As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents sboxPlateNumber As CS2005.SelectBox_Asur
    Friend WithEvents sboxAgentLic As CS2005.SelectBox_Asur
    Friend WithEvents sboxFirstName As CS2005.SelectBox_Asur
    Friend WithEvents sboxAgentCode As CS2005.SelectBox_Asur
    Friend WithEvents sboxLastName As CS2005.SelectBox_Asur
    Friend WithEvents sboxIDCard As CS2005.SelectBox_Asur
    Friend WithEvents sboxCustID As CS2005.SelectBox_Asur
    Friend WithEvents sboxEmployeeCode As SelectBox_Asur
    Friend WithEvents sboxEBRefNo As SelectBox_Asur
    Friend WithEvents sboxEmail As CS2005.SelectBox_Asur
    Friend WithEvents sboxMobile As CS2005.SelectBox_Asur
    Friend WithEvents cbExtendLimit As CheckBox


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
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents grdCust As System.Windows.Forms.DataGrid
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdCust = New System.Windows.Forms.DataGrid()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cbExtendLimit = New System.Windows.Forms.CheckBox()
        Me.pnlSrhCriBemuda = New System.Windows.Forms.FlowLayoutPanel()
        Me.sboxLastName = New CS2005.SelectBox_Asur()
        Me.sboxFirstName = New CS2005.SelectBox_Asur()
        Me.sboxIDCard = New CS2005.SelectBox_Asur()
        Me.sboxCustID = New CS2005.SelectBox_Asur()
        Me.sboxAgentCode = New CS2005.SelectBox_Asur()
        Me.sboxAgentLic = New CS2005.SelectBox_Asur()
        Me.sboxPlateNumber = New CS2005.SelectBox_Asur()
        Me.sboxMobile = New CS2005.SelectBox_Asur()
        Me.sboxEmail = New CS2005.SelectBox_Asur()
        Me.sboxEBRefNo = New CS2005.SelectBox_Asur()
        Me.sboxEmployeeCode = New CS2005.SelectBox_Asur()
        Me.pnlRdoGrpCompany = New System.Windows.Forms.Panel()
        Me.rbMacau = New System.Windows.Forms.RadioButton()
        Me.rbParallel = New System.Windows.Forms.RadioButton()
        Me.rbAssurance = New System.Windows.Forms.RadioButton()
        Me.rbBermuda = New System.Windows.Forms.RadioButton()
        CType(Me.grdCust, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlSrhCriBemuda.SuspendLayout()
        Me.pnlRdoGrpCompany.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdCust
        '
        Me.grdCust.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCust.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCust.BackColor = System.Drawing.Color.White
        Me.grdCust.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCust.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCust.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCust.CaptionVisible = False
        Me.grdCust.DataMember = ""
        Me.grdCust.FlatMode = True
        Me.grdCust.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCust.ForeColor = System.Drawing.Color.Black
        Me.grdCust.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCust.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCust.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCust.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCust.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCust.Location = New System.Drawing.Point(8, 8)
        Me.grdCust.Name = "grdCust"
        Me.grdCust.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCust.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCust.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCust.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCust.Size = New System.Drawing.Size(1133, 309)
        Me.grdCust.TabIndex = 4
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Location = New System.Drawing.Point(1149, 48)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(88, 23)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "Cl&ose"
        '
        'cmdOpen
        '
        Me.cmdOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpen.Enabled = False
        Me.cmdOpen.Location = New System.Drawing.Point(1149, 16)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(88, 23)
        Me.cmdOpen.TabIndex = 0
        Me.cmdOpen.Text = "&Open"
        '
        'cmdClear
        '
        Me.cmdClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClear.Location = New System.Drawing.Point(1149, 128)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(88, 23)
        Me.cmdClear.TabIndex = 3
        Me.cmdClear.Text = "&Clear All"
        '
        'cmdSearch
        '
        Me.cmdSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSearch.Location = New System.Drawing.Point(1149, 96)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(88, 23)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.Text = "&Search"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdSearch)
        Me.Panel1.Controls.Add(Me.grdCust)
        Me.Panel1.Controls.Add(Me.cmdOpen)
        Me.Panel1.Controls.Add(Me.cmdClear)
        Me.Panel1.Controls.Add(Me.cmdClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1253, 328)
        Me.Panel1.TabIndex = 1
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 328)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1253, 3)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.cbExtendLimit)
        Me.Panel2.Controls.Add(Me.pnlSrhCriBemuda)
        Me.Panel2.Controls.Add(Me.pnlRdoGrpCompany)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 331)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1253, 532)
        Me.Panel2.TabIndex = 0
        '
        'cbExtendLimit
        '
        Me.cbExtendLimit.AutoSize = True
        Me.cbExtendLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExtendLimit.Location = New System.Drawing.Point(367, 9)
        Me.cbExtendLimit.Name = "cbExtendLimit"
        Me.cbExtendLimit.Size = New System.Drawing.Size(139, 17)
        Me.cbExtendLimit.TabIndex = 2
        Me.cbExtendLimit.Text = "Extend Search Limit"
        Me.cbExtendLimit.UseVisualStyleBackColor = True
        '
        'pnlSrhCriBemuda
        '
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxLastName)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxFirstName)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxIDCard)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxCustID)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxAgentCode)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxAgentLic)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxPlateNumber)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxMobile)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxEmail)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxEBRefNo)
        Me.pnlSrhCriBemuda.Controls.Add(Me.sboxEmployeeCode)
        Me.pnlSrhCriBemuda.Location = New System.Drawing.Point(6, 32)
        Me.pnlSrhCriBemuda.Name = "pnlSrhCriBemuda"
        Me.pnlSrhCriBemuda.Size = New System.Drawing.Size(699, 428)
        Me.pnlSrhCriBemuda.TabIndex = 1
        '
        'sboxLastName
        '
        Me.sboxLastName.DefaultOp = "LIKE"
        Me.sboxLastName.FieldName = "NameSuffix"
        Me.sboxLastName.getSelectedValue = "like"
        Me.sboxLastName.LabelText = "Last Name"
        Me.sboxLastName.Location = New System.Drawing.Point(3, 3)
        Me.sboxLastName.Name = "sboxLastName"
        Me.sboxLastName.Size = New System.Drawing.Size(688, 32)
        Me.sboxLastName.TabIndex = 0
        '
        'sboxFirstName
        '
        Me.sboxFirstName.DefaultOp = "LIKE"
        Me.sboxFirstName.FieldName = "FirstName"
        Me.sboxFirstName.getSelectedValue = "like"
        Me.sboxFirstName.LabelText = "First Name"
        Me.sboxFirstName.Location = New System.Drawing.Point(3, 41)
        Me.sboxFirstName.Name = "sboxFirstName"
        Me.sboxFirstName.Size = New System.Drawing.Size(688, 32)
        Me.sboxFirstName.TabIndex = 1
        '
        'sboxIDCard
        '
        Me.sboxIDCard.DefaultOp = "="
        Me.sboxIDCard.FieldName = "GovernmentIDCard"
        Me.sboxIDCard.getSelectedValue = "="
        Me.sboxIDCard.LabelText = "ID Card/Passport"
        Me.sboxIDCard.Location = New System.Drawing.Point(3, 79)
        Me.sboxIDCard.Name = "sboxIDCard"
        Me.sboxIDCard.Size = New System.Drawing.Size(688, 32)
        Me.sboxIDCard.TabIndex = 2
        '
        'sboxCustID
        '
        Me.sboxCustID.DefaultOp = "="
        Me.sboxCustID.FieldName = "CustomerID"
        Me.sboxCustID.getSelectedValue = "="
        Me.sboxCustID.LabelText = "Customer ID"
        Me.sboxCustID.Location = New System.Drawing.Point(3, 117)
        Me.sboxCustID.Name = "sboxCustID"
        Me.sboxCustID.Size = New System.Drawing.Size(688, 32)
        Me.sboxCustID.TabIndex = 3
        '
        'sboxAgentCode
        '
        Me.sboxAgentCode.DefaultOp = "="
        Me.sboxAgentCode.FieldName = "AgentCode"
        Me.sboxAgentCode.getSelectedValue = "="
        Me.sboxAgentCode.LabelText = "Agent Code"
        Me.sboxAgentCode.Location = New System.Drawing.Point(3, 155)
        Me.sboxAgentCode.Name = "sboxAgentCode"
        Me.sboxAgentCode.Size = New System.Drawing.Size(688, 32)
        Me.sboxAgentCode.TabIndex = 4
        '
        'sboxAgentLic
        '
        Me.sboxAgentLic.DefaultOp = "="
        Me.sboxAgentLic.FieldName = "camalt_license_no"
        Me.sboxAgentLic.getSelectedValue = "="
        Me.sboxAgentLic.LabelText = "Agent License"
        Me.sboxAgentLic.Location = New System.Drawing.Point(3, 193)
        Me.sboxAgentLic.Name = "sboxAgentLic"
        Me.sboxAgentLic.Size = New System.Drawing.Size(688, 32)
        Me.sboxAgentLic.TabIndex = 5
        '
        'sboxPlateNumber
        '
        Me.sboxPlateNumber.DefaultOp = "="
        Me.sboxPlateNumber.FieldName = "RegistrationNo"
        Me.sboxPlateNumber.getSelectedValue = "="
        Me.sboxPlateNumber.LabelText = "Plate Number"
        Me.sboxPlateNumber.Location = New System.Drawing.Point(3, 231)
        Me.sboxPlateNumber.Name = "sboxPlateNumber"
        Me.sboxPlateNumber.Size = New System.Drawing.Size(688, 32)
        Me.sboxPlateNumber.TabIndex = 6
        '
        'sboxMobile
        '
        Me.sboxMobile.DefaultOp = "="
        Me.sboxMobile.FieldName = "PhoneMobile"
        Me.sboxMobile.getSelectedValue = "="
        Me.sboxMobile.LabelText = "Mobile No."
        Me.sboxMobile.Location = New System.Drawing.Point(3, 269)
        Me.sboxMobile.Name = "sboxMobile"
        Me.sboxMobile.Size = New System.Drawing.Size(688, 32)
        Me.sboxMobile.TabIndex = 7
        '
        'sboxEmail
        '
        Me.sboxEmail.DefaultOp = "="
        Me.sboxEmail.FieldName = "EmailAddr"
        Me.sboxEmail.getSelectedValue = "="
        Me.sboxEmail.LabelText = "Email"
        Me.sboxEmail.Location = New System.Drawing.Point(3, 307)
        Me.sboxEmail.Name = "sboxEmail"
        Me.sboxEmail.Size = New System.Drawing.Size(688, 32)
        Me.sboxEmail.TabIndex = 8
        '
        'sboxEBRefNo
        '
        Me.sboxEBRefNo.DefaultOp = "="
        Me.sboxEBRefNo.FieldName = "ExternalPartyCode"
        Me.sboxEBRefNo.getSelectedValue = "="
        Me.sboxEBRefNo.LabelText = "EB Ref No."
        Me.sboxEBRefNo.Location = New System.Drawing.Point(3, 345)
        Me.sboxEBRefNo.Name = "sboxEBRefNo"
        Me.sboxEBRefNo.Size = New System.Drawing.Size(688, 32)
        Me.sboxEBRefNo.TabIndex = 9
        '
        'sboxEmployeeCode
        '
        Me.sboxEmployeeCode.DefaultOp = "="
        Me.sboxEmployeeCode.FieldName = "EmployeeCode"
        Me.sboxEmployeeCode.getSelectedValue = "="
        Me.sboxEmployeeCode.LabelText = "Employee code"
        Me.sboxEmployeeCode.Location = New System.Drawing.Point(3, 383)
        Me.sboxEmployeeCode.Name = "sboxEmployeeCode"
        Me.sboxEmployeeCode.Size = New System.Drawing.Size(688, 32)
        Me.sboxEmployeeCode.TabIndex = 10
        '
        'pnlRdoGrpCompany
        '
        Me.pnlRdoGrpCompany.Controls.Add(Me.rbMacau)
        Me.pnlRdoGrpCompany.Controls.Add(Me.rbParallel)
        Me.pnlRdoGrpCompany.Controls.Add(Me.rbAssurance)
        Me.pnlRdoGrpCompany.Controls.Add(Me.rbBermuda)
        Me.pnlRdoGrpCompany.Location = New System.Drawing.Point(10, 4)
        Me.pnlRdoGrpCompany.Name = "pnlRdoGrpCompany"
        Me.pnlRdoGrpCompany.Size = New System.Drawing.Size(351, 25)
        Me.pnlRdoGrpCompany.TabIndex = 0
        '
        'rbMacau
        '
        Me.rbMacau.AutoSize = True
        Me.rbMacau.Location = New System.Drawing.Point(195, 5)
        Me.rbMacau.Name = "rbMacau"
        Me.rbMacau.Size = New System.Drawing.Size(58, 17)
        Me.rbMacau.TabIndex = 2
        Me.rbMacau.Text = "Macau"
        Me.rbMacau.UseVisualStyleBackColor = True
        '
        'rbParallel
        '
        Me.rbParallel.AutoSize = True
        Me.rbParallel.Location = New System.Drawing.Point(259, 5)
        Me.rbParallel.Name = "rbParallel"
        Me.rbParallel.Size = New System.Drawing.Size(59, 17)
        Me.rbParallel.TabIndex = 3
        Me.rbParallel.Text = "Parallel"
        Me.rbParallel.UseVisualStyleBackColor = True
        '
        'rbAssurance
        '
        Me.rbAssurance.AutoSize = True
        Me.rbAssurance.Location = New System.Drawing.Point(114, 5)
        Me.rbAssurance.Name = "rbAssurance"
        Me.rbAssurance.Size = New System.Drawing.Size(75, 17)
        Me.rbAssurance.TabIndex = 1
        Me.rbAssurance.Text = "Assurance"
        Me.rbAssurance.UseVisualStyleBackColor = True
        '
        'rbBermuda
        '
        Me.rbBermuda.AutoSize = True
        Me.rbBermuda.Checked = True
        Me.rbBermuda.Location = New System.Drawing.Point(3, 5)
        Me.rbBermuda.Name = "rbBermuda"
        Me.rbBermuda.Size = New System.Drawing.Size(105, 17)
        Me.rbBermuda.TabIndex = 0
        Me.rbBermuda.TabStop = True
        Me.rbBermuda.Text = "Bermuda/Private"
        Me.rbBermuda.UseVisualStyleBackColor = True
        '
        'frmSearchCustAsur
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1253, 863)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmSearchCustAsur"
        Me.Text = "Search Customer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdCust, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlSrhCriBemuda.ResumeLayout(False)
        Me.pnlRdoGrpCompany.ResumeLayout(False)
        Me.pnlRdoGrpCompany.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        wndMain.StatusBarPanel1.Text = ""
        wndMain.Cursor = Cursors.WaitCursor
        cmdOpen.Enabled = False

        If sqldt IsNot Nothing Then sqldt.Clear()

        If rbBermuda.Checked OrElse rbParallel.Checked Then
            ShowCustomer(SearchCustomer("B", rbParallel.Checked))
        End If

        If rbAssurance.Checked OrElse rbParallel.Checked Then
            ShowCustomer(SearchCustomer("A", rbParallel.Checked))
        End If

        If rbMacau.Checked OrElse rbParallel.Checked Then
            ShowCustomer(SearchCustomer("M", rbParallel.Checked))
        End If

        If bm IsNot Nothing AndAlso bm.Count > 0 Then
            cmdOpen.Enabled = True
            cmdOpen.Focus()
            Me.AcceptButton = Me.cmdOpen
        End If

        wndMain.StatusBarPanel1.Text &= $"{If(String.IsNullOrEmpty(wndMain.StatusBarPanel1.Text), "0 records", "")} selected"
        wndMain.Cursor = Cursors.Default
    End Sub

    ''' <remarks>
    ''' <br>20230822 Oliver, Phase 3 Point A-1(CRS Enhancement) - which is Add new field “EB reference no.” and “Employee code” in search customer page</br>
    ''' <br>20250112 Chrysan Cheng, HNW Expansion - Integrated Customer Search</br>
    ''' </remarks>
    Private Function SearchCustomer(strType As String, Optional isParallelMode As Boolean = False) As DataTable
        Dim strEBRefNo, strEmployeeCode As String   ' Phase 3 Point A-1(CRS Enhancement)
        Dim strLN, strFN, strHKID, strCID, strAG, strLIC, strPlateNo, strMobile, strEmail As String
        Dim strErr As String = String.Empty
        Dim tmpdt As DataTable = Nothing
        Dim searchLimitNum As Integer = If(cbExtendLimit.Checked, EXTENDED_SEARCH_LIMIT, gSearchLimit)

        Try
            If Not ValidateCriteria(strLN, sboxLastName) Then Return Nothing
            If Not ValidateCriteria(strFN, sboxFirstName) Then Return Nothing
            If Not ValidateCriteria(strHKID, sboxIDCard) Then Return Nothing
            If Not ValidateCriteria(strAG, sboxAgentCode) Then Return Nothing
            If Not ValidateCriteria(strLIC, sboxAgentLic) Then Return Nothing
            If Not ValidateCriteria(strCID, sboxCustID) Then Return Nothing
            If Not ValidateCriteria(strPlateNo, sboxPlateNumber) Then Return Nothing
            If Not ValidateCriteria(strMobile, sboxMobile) Then Return Nothing
            If Not ValidateCriteria(strEmail, sboxEmail) Then Return Nothing
            If Not ValidateCriteria(strEBRefNo, sboxEBRefNo) Then Return Nothing ' Phase 3 Point A-1(CRS Enhancement)
            If Not ValidateCriteria(strEmployeeCode, sboxEmployeeCode) Then Return Nothing ' Phase 3 Point A-1(CRS Enhancement)


            ' get agent code automatically using agent license (if need)
            If strLIC <> "" AndAlso sboxAgentCode.TextBoxSearchInput1.Text.Trim = String.Empty Then
                strAG = GetAgentFromLicByAPI(strLIC, strType)
                sboxAgentCode.TextBoxSearchInput1.Text = strAG
                sboxAgentCode.ComboBoxCriteria.SelectedItem = "="
                strAG = " AgentCode = '" & strAG & "'"
            End If

            ' validate customer ID input
            If strCID <> "" AndAlso Not IsNumeric(sboxCustID.TextBoxSearchInput1.Text) Then
                MsgBox("Invalid CustomerID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                sboxCustID.TextBoxSearchInput1.Focus()
                Return Nothing
            End If

            ' validate criteria input cannot be empty
            If strLN = "" And strFN = "" And strHKID = "" And strCID = "" And strAG = "" And strPlateNo = "" And
                    strMobile = "" And strEmail = "" And strEBRefNo = "" And strEmployeeCode = "" Then
                MsgBox("Please enter a criteria to search for", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                sboxLastName.TextBoxSearchInput1.Focus()
                Return Nothing
            End If

            ' extend ID Card query criteria
            If strHKID <> "" Then
                strHKID = "(" & strHKID.Trim & " or " & Replace(strHKID.Trim, "GovernmentIDCard", "PassportNumber") & ")"
            End If

            ' remove semicolon for customer ID
            If Not InStr(strCID, "like") > 0 Then
                strCID = Replace(strCID, "'", "")
            End If


            ' do querying
            tmpdt = GetCustomerListByAPI(strLN, strFN, strHKID, strCID, strAG, strPlateNo, strType, strErr, strMobile, strEmail, strEBRefNo, strEmployeeCode)

            If Not String.IsNullOrEmpty(strErr) Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                Return Nothing
            End If

            If tmpdt Is Nothing OrElse tmpdt.Rows.Count = 0 Then
                MsgBox($"No Matching {GetCompanyIDNameByType(strType)} Record found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Search Customer")
                sboxLastName.TextBoxSearchInput1.Focus()
                Return Nothing
            ElseIf tmpdt.Rows.Count > searchLimitNum Then
                MsgBox($"Over {searchLimitNum} {GetCompanyIDNameByType(strType)} records returned, please re-define your criteria.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                sboxLastName.TextBoxSearchInput1.Focus()
                Return Nothing
            End If

            ' append count status text
            wndMain.StatusBarPanel1.Text &= $"{If(String.IsNullOrEmpty(wndMain.StatusBarPanel1.Text), "", " & ")}{tmpdt.Rows.Count} {GetCompanyIDNameByType(strType)} records"

            If isParallelMode Then
                ' add Company ID column for Parallel mode
                Dim newColumn As New DataColumn("CompanyID", GetType(String)) With {
                    .DefaultValue = GetCompanyIDNameByType(strType)
                }
                tmpdt.Columns.Add(newColumn)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        End Try

        Return tmpdt
    End Function

    ''' <remarks>
    ''' <br>20250112 Chrysan Cheng, HNW Expansion - Integrated Customer Search</br>
    ''' </remarks>
    Private Sub ShowCustomer(dtResult As DataTable)
        If sqldt Is Nothing Then sqldt = New DataTable("CustList")

        If dtResult IsNot Nothing Then sqldt.Merge(dtResult)

        grdCust.DataSource = sqldt
        bm = Me.BindingContext(grdCust.DataSource)
    End Sub

    ''' <remarks>
    ''' <br>20250112 Chrysan Cheng, HNW Expansion - Integrated Customer Search</br>
    ''' </remarks>
    Private Function ValidateCriteria(ByRef strCri As String, ByVal sbox As SelectBox_Asur, Optional ByVal strErrMsg As String = "") As Boolean
        ' get the Criteria value only when it is visible
        strCri = If(sbox.Visible, sbox.Criteria.Trim(), "")

        If strCri = gError Then
            If strErrMsg = "" Then
                MsgBox("Invalid " & sbox.LabelText, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            End If
            sbox.TextBoxSearchInput1.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ValidateCriteria(ByRef strCri As String, ByVal sbox As SelectDropDown, Optional ByVal strErrMsg As String = "") As Boolean
        ' get the Criteria value only when it is visible
        strCri = If(sbox.Visible, sbox.Criteria.Trim(), "")

        If strCri = gError Then
            If strErrMsg = "" Then
                MsgBox("Invalid " & sbox.LabelText, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Else
                MsgBox(strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            End If
            sbox.ComboBox2.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub buildUI()
        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        grdCust.TableStyles.Clear()
        cs = New DataGridTextBoxColumn
        cs.Width = 130
        cs.MappingName = "GovernmentIDCard"
        cs.HeaderText = "ID Card"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "NameSuffix"
        cs.HeaderText = "Last Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "FirstName"
        cs.HeaderText = "First Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "ChiName"
        cs.HeaderText = "Chi. Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "DateOfBirth"
        cs.HeaderText = "Date of Birth"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "Gender"
        cs.HeaderText = "Gender"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "AgentCode"
        cs.HeaderText = "Agent Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CustomerID"
        cs.HeaderText = "Customer ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CompanyID"
        cs.HeaderText = "Company ID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CustList"
        grdCust.TableStyles.Add(ts)


        grdCust.DataSource = sqldt
        grdCust.AllowDrop = False
        grdCust.ReadOnly = True


        AddHandler rbBermuda.CheckedChanged, AddressOf RdoGrpCompany_OnChange
        AddHandler rbAssurance.CheckedChanged, AddressOf RdoGrpCompany_OnChange
        AddHandler rbMacau.CheckedChanged, AddressOf RdoGrpCompany_OnChange
        AddHandler rbParallel.CheckedChanged, AddressOf RdoGrpCompany_OnChange

    End Sub

    Private Sub frmSearchCust_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildUI()
        wndMain.StatusBarPanel1.Text = ""
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Me.sboxLastName.Clear()
        Me.sboxFirstName.Clear()
        Me.sboxIDCard.Clear()
        Me.sboxAgentCode.Clear()
        Me.sboxCustID.Clear()
        Me.sboxAgentLic.Clear()
        Me.sboxPlateNumber.Clear()
        Me.sboxMobile.Clear()
        Me.sboxEmail.Clear()
        Me.sboxEBRefNo.Clear()
        Me.sboxEmployeeCode.Clear()

        wndMain.StatusBarPanel1.Text = ""
        Me.cbExtendLimit.Checked = False

        cmdOpen.Enabled = False
        Me.AcceptButton = cmdSearch

        Me.sboxLastName.TextBoxSearchInput1.Focus()

        Try
            If sqldt IsNot Nothing Then sqldt.Clear()
        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

        Dim strName, strCustID, strClientID, strCompanyID, strIDNumber As String
        Dim isBothCompany As Boolean = False

        If bm IsNot Nothing AndAlso bm.Count > 0 Then
            With CType(bm.Current, DataRowView).Row
                strName = Trim($"{Trim(.Item("NameSuffix"))} {Trim(.Item("FirstName"))}")
                strCustID = .Item("CustomerID")
                strClientID = .Item("ClientID")
                strCompanyID = GetCompanyIDNameWithoutEmpty(If(.Table.Columns.Contains("CompanyID"), .Item("CompanyID"), Nothing))
                strIDNumber = .Item("GovernmentIDCard")

                ' check whether this customer(IDCard/Passport) exists in multi company
                isBothCompany = .Table.Columns.Contains("CompanyID") AndAlso Not String.IsNullOrEmpty(strIDNumber) AndAlso
                    .Table.Compute("Count(CompanyID)", $"CompanyID = '{GetCompanyIDNameByType("B")}' and GovernmentIDCard = '{strIDNumber}'") > 0 AndAlso
                    .Table.Compute("Count(CompanyID)", $"CompanyID = '{GetCompanyIDNameByType("A")}' and GovernmentIDCard = '{strIDNumber}'") > 0
            End With
        End If

        If String.IsNullOrEmpty(strCustID) Then
            Return
        End If

        ' show customer detail screen
        Dim bl As New CustomerSearchBL()
        bl.ShowCustomerCentric(strName, strCustID, strClientID, strCompanyID, strIDNumber, "Customer Search", isBothCompany)

        Me.AcceptButton = Me.cmdSearch
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub grdCust_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCust.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdCust.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdCust.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdCust.Select(hti.Row)
        End If
    End Sub

    Private Sub grdCust_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCust.DoubleClick
        cmdOpen.PerformClick()
    End Sub

    Private Sub frmSearchCust_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        sboxLastName.TextBoxSearchInput1.Focus()
    End Sub

    Private Sub SelectBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles sboxLastName.Enter, sboxFirstName.Enter, sboxIDCard.Enter,
                                                                                            sboxCustID.Enter, sboxAgentCode.Enter, sboxAgentLic.Enter,
                                                                                            sboxPlateNumber.Enter, sboxMobile.Enter, sboxEmail.Enter,
                                                                                            sboxEBRefNo.Enter, sboxEmployeeCode.Enter
        Me.AcceptButton = Me.cmdSearch
    End Sub

    Private Sub RdoGrpCompany_OnChange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim rb As RadioButton = CType(sender, RadioButton)
        If Not rb.Checked Then Return

        wndMain.StatusBarPanel1.Text = ""
        cmdOpen.Enabled = False
        Me.AcceptButton = cmdSearch

        Dim isParallelMode As Boolean = False

        Select Case rb.Name
            Case "rbBermuda"
                HideCriteriaControls()
            Case "rbAssurance"
                HideCriteriaControls(sboxEBRefNo, sboxEmployeeCode)
            Case "rbMacau"
                HideCriteriaControls(sboxEBRefNo, sboxEmployeeCode)
            Case "rbParallel"
                isParallelMode = True
                HideCriteriaControls(sboxEBRefNo, sboxEmployeeCode, sboxAgentCode, sboxAgentLic, sboxPlateNumber)
            Case Else
                HideCriteriaControls()
        End Select

        If sqldt IsNot Nothing Then
            sqldt.Clear()

            ' toggle the visibility of column CompanyID according to the mode
            If isParallelMode Then
                If Not sqldt.Columns.Contains("CompanyID") AndAlso sqldt.Columns.Count > 0 Then sqldt.Columns.Add("CompanyID", GetType(String))
            Else
                If sqldt.Columns.Contains("CompanyID") Then sqldt.Columns.Remove("CompanyID")
            End If
        End If
    End Sub

    Private Sub HideCriteriaControls(ParamArray targetsToBeHidden As Control())
        ' hide only the target criteria controls
        For Each sbox As Control In pnlSrhCriBemuda.Controls
            sbox.Visible = Not targetsToBeHidden.Contains(sbox)
        Next
    End Sub

    Private Sub AlphanumericOnly(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TypeOf (sender) Is TextBox Then
            sender.Text = Regex.Replace(sender.Text, "[^\da-zA-Z]", "")
        End If
    End Sub

    Private Function GetCompanyIDNameByType(strType As String) As String
        Select Case strType
            Case "B"
                Return "Bermuda/Private"
            Case "A"
                Return "Assurance"
            Case "M"
                Return "Macau"
            Case Else
                Return Nothing
        End Select
    End Function

    Private Function GetCompanyIDNameWithoutEmpty(companyIDNameWithEmpty As String) As String
        If String.IsNullOrEmpty(companyIDNameWithEmpty) Then
            ' if there is empty (non-Parallel mode), the decision is based on the currently selected RadioButton
            Return GetCompanyIDNameByType(If(rbAssurance.Checked, "A", If(rbMacau.Checked, "M", "B")))
        Else
            Return companyIDNameWithEmpty
        End If
    End Function

End Class
