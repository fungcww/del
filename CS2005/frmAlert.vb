'**************************
'Date : Nov 1, 2023
'Author : Oliver Ou 222834
'Purpose : CRS Enhancement(General Enhance Ph4) Point A-10
'1. Customer alert maintenance
'2. Enable boardcast prompt up message under customer level + all policies under same customer (PH Or PI)
'**************************
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports PdfEdit.Pdf.IO

Public Class frmAlert
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents chkValid As System.Windows.Forms.CheckBox
    Friend WithEvents grdAlert As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Details As GroupBox
    Friend WithEvents Search As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtSearchID As TextBox
    Friend WithEvents cmdSearch As Button
    Friend WithEvents cmdClear As Button
    Friend WithEvents txtCustID As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.chkValid = New System.Windows.Forms.CheckBox()
        Me.grdAlert = New System.Windows.Forms.DataGrid()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustID = New System.Windows.Forms.TextBox()
        Me.Details = New System.Windows.Forms.GroupBox()
        Me.Search = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSearchID = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        CType(Me.grdAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Details.SuspendLayout()
        Me.Search.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(970, 156)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(120, 33)
        Me.cmdDelete.TabIndex = 9
        Me.cmdDelete.Text = "&Delete"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(970, 112)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(120, 33)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "&Cancel"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(970, 68)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(120, 33)
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "&Save"
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(970, 25)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(120, 33)
        Me.cmdAdd.TabIndex = 5
        Me.cmdAdd.Text = "&Add"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(143, 20)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "HKID/Passport No."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 20)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Message"
        '
        'txtMessage
        '
        Me.txtMessage.BackColor = System.Drawing.SystemColors.Window
        Me.txtMessage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMessage.Location = New System.Drawing.Point(155, 116)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(773, 117)
        Me.txtMessage.TabIndex = 3
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.SystemColors.Window
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(155, 39)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(160, 26)
        Me.txtID.TabIndex = 1
        '
        'chkValid
        '
        Me.chkValid.Location = New System.Drawing.Point(356, 36)
        Me.chkValid.Name = "chkValid"
        Me.chkValid.Size = New System.Drawing.Size(121, 35)
        Me.chkValid.TabIndex = 4
        Me.chkValid.Text = "Prompt?"
        '
        'grdAlert
        '
        Me.grdAlert.AlternatingBackColor = System.Drawing.Color.White
        Me.grdAlert.BackColor = System.Drawing.Color.White
        Me.grdAlert.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAlert.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAlert.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAlert.CaptionVisible = False
        Me.grdAlert.DataMember = ""
        Me.grdAlert.FlatMode = True
        Me.grdAlert.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdAlert.ForeColor = System.Drawing.Color.Black
        Me.grdAlert.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAlert.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdAlert.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdAlert.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAlert.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAlert.Location = New System.Drawing.Point(17, 41)
        Me.grdAlert.Name = "grdAlert"
        Me.grdAlert.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAlert.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAlert.ReadOnly = True
        Me.grdAlert.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAlert.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAlert.Size = New System.Drawing.Size(1109, 240)
        Me.grdAlert.TabIndex = 19
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(970, 200)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(120, 33)
        Me.cmdClose.TabIndex = 10
        Me.cmdClose.Text = "&Close"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 20)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Alert List:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 20)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "CustomerID"
        '
        'txtCustID
        '
        Me.txtCustID.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustID.Location = New System.Drawing.Point(155, 78)
        Me.txtCustID.Name = "txtCustID"
        Me.txtCustID.Size = New System.Drawing.Size(160, 26)
        Me.txtCustID.TabIndex = 2
        '
        'Details
        '
        Me.Details.Controls.Add(Me.Label5)
        Me.Details.Controls.Add(Me.Label2)
        Me.Details.Controls.Add(Me.txtCustID)
        Me.Details.Controls.Add(Me.Label3)
        Me.Details.Controls.Add(Me.txtMessage)
        Me.Details.Controls.Add(Me.txtID)
        Me.Details.Controls.Add(Me.cmdClose)
        Me.Details.Controls.Add(Me.cmdDelete)
        Me.Details.Controls.Add(Me.cmdCancel)
        Me.Details.Controls.Add(Me.cmdSave)
        Me.Details.Controls.Add(Me.cmdAdd)
        Me.Details.Controls.Add(Me.chkValid)
        Me.Details.Location = New System.Drawing.Point(18, 404)
        Me.Details.Name = "Details"
        Me.Details.Size = New System.Drawing.Size(1108, 259)
        Me.Details.TabIndex = 42
        Me.Details.TabStop = False
        Me.Details.Text = "Details"
        '
        'Search
        '
        Me.Search.Controls.Add(Me.cmdClear)
        Me.Search.Controls.Add(Me.Label4)
        Me.Search.Controls.Add(Me.txtSearchID)
        Me.Search.Controls.Add(Me.cmdSearch)
        Me.Search.Location = New System.Drawing.Point(18, 304)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(1108, 81)
        Me.Search.TabIndex = 43
        Me.Search.TabStop = False
        Me.Search.Text = "Search"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 20)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "HKID/Passport No."
        '
        'txtSearchID
        '
        Me.txtSearchID.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearchID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearchID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSearchID.Location = New System.Drawing.Point(155, 39)
        Me.txtSearchID.Name = "txtSearchID"
        Me.txtSearchID.Size = New System.Drawing.Size(160, 26)
        Me.txtSearchID.TabIndex = 1
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(824, 29)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(120, 33)
        Me.cmdSearch.TabIndex = 5
        Me.cmdSearch.Text = "&Search"
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(970, 29)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(120, 33)
        Me.cmdClear.TabIndex = 33
        Me.cmdClear.Text = "&Clear"
        '
        'frmAlert
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(8, 19)
        Me.ClientSize = New System.Drawing.Size(1239, 707)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdAlert)
        Me.Controls.Add(Me.Details)
        Me.Name = "frmAlert"
        Me.Text = "Alert Maintenance"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Details.ResumeLayout(False)
        Me.Details.PerformLayout()
        Me.Search.ResumeLayout(False)
        Me.Search.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    'Dim sqlda As SqlDataAdapter
    'Dim sqlconnect As New SqlConnection
    Dim dsAlert As New DataSet
    'Dim strSQL As String
    'Dim sqlcb As SqlCommandBuilder
    Dim isClickAddButton As Boolean = False
    Dim bm As BindingManagerBase
    Dim blnAdd As Boolean = True

    Public WriteOnly Property Editable() As Boolean
        Set(ByVal Value As Boolean)
            Me.cmdAdd.Enabled = Not Value
            Me.cmdSave.Enabled = Value
            Me.cmdCancel.Enabled = Value
            Me.cmdDelete.Enabled = Not Value

            Me.txtID.ReadOnly = Not Value
            Me.txtCustID.ReadOnly = Not Value
            Me.txtMessage.ReadOnly = Not Value
            Me.chkValid.Enabled = Value
        End Set
    End Property

    Private Sub frmAlert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            InitDataTable()
            InitForm()
            InitBindData()
            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            'If ExternalUser Then
            '    cmdAdd.Enabled = False
            '    cmdSave.Enabled = False
            '    cmdDelete.Enabled = False
            '    cmdSearch.Enabled = False
            'End If
        Catch ex As Exception
            cmdAdd.Enabled = False
            cmdSave.Enabled = False
            cmdDelete.Enabled = False
            cmdCancel.Enabled = False
            cmdSearch.Enabled = False
            cmdClear.Enabled = False
            Exit Sub
        End Try
    End Sub

    Private Sub InitDataTable(Optional ByVal searchID As String = "")
        Dim ds As DataSet = New DataSet()
        Try
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_ALL_CUSTOMER_ALERT",
                                     New Dictionary(Of String, String) From {
                                    {"cswca_hkid", searchID}
                                    })
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI GET_ALL_CUSTOMER_ALERT Retrieve Error." & vbCrLf & ex.Message)
            Throw
        Finally
            Cursor = Cursors.Default
        End Try
        dsAlert = ds
        Dim key(0) As DataColumn
        key(0) = dsAlert.Tables("AllCustomerAlert").Columns("cswca_alert_no")
        dsAlert.Tables("AllCustomerAlert").PrimaryKey = key
        'Dim dtTemp As New DataTable
        'strSQL = "select * from csw_customer_alert order by 1"
        'sqlconnect.ConnectionString = strCIWConn

        'sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        'Try
        '    sqlda.Fill(dtTemp)
        'Catch sqlex As SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'End Try

        'sqlconnect.Dispose()
        'Return dtTemp
    End Sub

    Private Sub InitForm()
        Try
            Dim ts As New clsDataGridTableStyle(True)
            Dim cs As DataGridTextBoxColumn

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "cswca_hkid"
            cs.HeaderText = "HKID"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "cswca_customerid"
            cs.HeaderText = "CustomerID"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 200
            cs.MappingName = "cswca_message"
            cs.HeaderText = "Message"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 80
            cs.MappingName = "cswca_prompt"
            cs.HeaderText = "Prompt"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "cswca_update_user"
            cs.HeaderText = "Update User"
            cs.NullText = gNULLText
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 150
            cs.MappingName = "cswca_update_date"
            cs.HeaderText = "Update Date"
            cs.NullText = gNULLText
            cs.Format = gDateTimeFormat
            ts.GridColumnStyles.Add(cs)

            ts.MappingName = "AllCustomerAlert"
            grdAlert.TableStyles.Add(ts)

            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            'If ExternalUser Then
            '    grdAlert.DataSource = MaskDTsource(dsAlert.Tables("AllCustomerAlert"), "cswca_hkid", MaskData.HKID)
            'Else
            '    grdAlert.DataSource = dsAlert.Tables("AllCustomerAlert")
            'End If
            grdAlert.DataSource = dsAlert.Tables("AllCustomerAlert")

            dsAlert.Tables("AllCustomerAlert").DefaultView.Sort = "cswca_alert_no"
            wndMain.StatusBarPanel1.Text = ""
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
            Throw
        End Try

    End Sub

    Private Sub InitBindData()
        Try
            Dim b As Binding
            b = New Binding("Checked", dsAlert.Tables("AllCustomerAlert"), "cswca_prompt")
            AddHandler b.Format, AddressOf YNtoTF
            AddHandler b.Parse, AddressOf TFtoYN
            chkValid.DataBindings.Add(b)

            txtID.DataBindings.Add("Text", dsAlert.Tables("AllCustomerAlert"), "cswca_hkid")
            txtCustID.DataBindings.Add("Text", dsAlert.Tables("AllCustomerAlert"), "cswca_customerid")
            txtMessage.DataBindings.Add("Text", dsAlert.Tables("AllCustomerAlert"), "cswca_message")

            wndMain.StatusBarPanel1.Text = dsAlert.Tables("AllCustomerAlert").Rows.Count & " records selected"
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
            Throw
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        'Me.txtID.Text = ""
        'Me.txtCustID.Text = ""
        'Me.txtMessage.Text = ""
        'Me.chkValid.Checked = True
        'Me.Editable = True
        'Me.txtID.Focus()
        Dim dr As DataRow
        isClickAddButton = True
        dr = dsAlert.Tables("AllCustomerAlert").NewRow
        dr.Item("cswca_alert_no") = 0
        dr.Item("cswca_hkid") = ""
        dr.Item("cswca_customerid") = 0
        dr.Item("cswca_message") = ""
        dr.Item("cswca_prompt") = "Y"
        dr.Item("cswca_update_user") = gsUser
        dr.Item("cswca_update_date") = DateTime.MinValue
        dsAlert.Tables("AllCustomerAlert").Rows.Add(dr)
        Me.cmdCancel.Enabled = True
        Me.cmdAdd.Enabled = False
        Me.cmdDelete.Enabled = False
        Me.BindingContext(dsAlert.Tables("AllCustomerAlert")).Position = dsAlert.Tables("AllCustomerAlert").Rows.Count - 1
    End Sub

    Private Sub updateDisplay()

        'If bm.Count > 0 Then
        '    If ExternalUser Then
        '        txtID.Text = MaskExternalUserData(MaskData.HKID, CType(bm.Current, DataRowView).Row.Item("cswca_hkid"))
        '    Else
        '        txtID.Text = CType(bm.Current, DataRowView).Row.Item("cswca_hkid")
        '    End If
        '    txtCustID.Text = CType(bm.Current, DataRowView).Row.Item("cswca_customerid")
        '    txtMessage.Text = CType(bm.Current, DataRowView).Row.Item("cswca_message")

        '    If CType(bm.Current, DataRowView).Row.Item("cswca_prompt") = "Y" Then
        '        Me.chkValid.Checked = True
        '    Else
        '        Me.chkValid.Checked = False
        '    End If
        'End If

    End Sub

    Private Sub grdAlert_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAlert.CurrentCellChanged
        Me.BindingContext(dsAlert.Tables("AllCustomerAlert")).EndCurrentEdit()

        If dsAlert.HasChanges Then

            If isClickAddButton Then
                isClickAddButton = False
            Else
                If MsgBox("Do you want to save the modified data? ", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then

                    Dim dr As DataRow
                    Dim insertedDataTable = dsAlert.Tables("AllCustomerAlert").GetChanges(DataRowState.Added)
                    If Not IsNothing(insertedDataTable) Then
                        For Each dr In insertedDataTable.Rows
                            InsertCustomerAlert(dr)
                        Next
                    End If
                    Dim updatedDataTable = dsAlert.Tables("AllCustomerAlert").GetChanges(DataRowState.Modified)
                    If Not IsNothing(updatedDataTable) Then
                        For Each dr In updatedDataTable.Rows
                            UpdateCustomerAlert(dr)
                        Next
                    End If

                End If
            End If

        End If
    End Sub

    Private Sub InsertCustomerAlert(ByVal dr As DataRow)
        Dim hkId As String = ""
        Dim customerId As String = ""
        Dim message As String = ""

        If Not IsDBNull(dr) Then
            hkId = dr.Item("cswca_hkid").ToString()
            customerId = dr.Item("cswca_customerid").ToString()
            message = dr.Item("cswca_message").ToString()
        End If

        If Trim(hkId) = "" OrElse Trim(message) = "" Then
            MsgBox("Missing Information.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            RefreshAlertCustomer()
            Exit Sub
        End If
        'Dim dt As DataTable = GetCustomerAlertTableByHkId(hkId)
        'If dt.Rows.Count > 0 Then
        '    MsgBox("This HKID/Passport No. " & vbCrLf & hkId & " Record Already Exists !" & vbCrLf & vbCrLf & "The existing record have been Automatically navigated ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        '    RefreshAlertCustomer()
        '    Dim existDr As DataRow = dsAlert.Tables("AllCustomerAlert").Rows.Find(dt.Rows(0).Item("cswca_alert_no"))
        '    Me.BindingContext(dsAlert.Tables("AllCustomerAlert")).Position = existDr.Item("row_num") - 1
        '    Exit Sub
        'End If

        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(), "INSERT_CUSTOMER_ALERT",
                                New Dictionary(Of String, String) From {
                                {"cswca_hkid", Trim(hkId)},
                                {"cswca_customerid", Trim(customerId)},
                                {"cswca_message", Message},
                                {"cswca_prompt", IIf(chkValid.Checked, "Y", "N")},
                                {"cswca_update_user", IIf(Not String.IsNullOrEmpty(gsUser), gsUser, " ")}
                                })
            MsgBox("Insert Data Success !", , "Insert Data Success")
            RefreshAlertCustomer()
        Catch ex As Exception
            HandleGlobalException(ex, "Error occurs when inserting the comments. Error:  " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateCustomerAlert(ByVal dr As DataRow)

        If Trim(dr.Item("cswca_hkid").ToString()) = "" OrElse Trim(dr.Item("cswca_message").ToString()) = "" Then
            MsgBox("Missing Information.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        If Not ValidateCustomerAlertUpdateTime(dr) Then
            RefreshAlertCustomer()
            Exit Sub
        End If

        Try
            APIServiceBL.ExecAPIBusi(getCompanyCode(), "UPDATE_CUSTOMER_ALERT_BY_NO",
                                New Dictionary(Of String, String) From {
                                {"cswca_hkid", Trim(dr.Item("cswca_hkid").ToString())},
                                {"cswca_customerid", Trim(dr.Item("cswca_customerid").ToString())},
                                {"cswca_message", dr.Item("cswca_message").ToString()},
                                {"cswca_prompt", IIf(chkValid.Checked, "Y", "N")},
                                {"cswca_update_user", IIf(Not String.IsNullOrEmpty(gsUser), gsUser, " ")},
                                {"cswca_alert_no", dr.Item("cswca_alert_no").ToString()}
                                })
            MsgBox("Update Data Success !", , "Insert Data Success")
            RefreshAlertCustomer()
        Catch ex As Exception
            HandleGlobalException(ex, "Error occurs when updating the comments. Error:  " & ex.Message)
        End Try

    End Sub

    Private Function ValidateCustomerAlertUpdateTime(ByVal dr As DataRow) As Boolean
        Try
            Dim lastUpdateTimeActual As DateTime = GetCustomerAlertUpdateTimeByAlerNo(dr.Item("cswca_alert_no"))
            Dim lastUpdateTime As DateTime = dr.Item("cswca_update_date")
            If DateTime.Compare(lastUpdateTimeActual, lastUpdateTime) Then
                MsgBox("The record is updated by another user on " & vbCrLf & lastUpdateTimeActual & vbCrLf & "Please wait and then click the row again when the page refreshed", MsgBoxStyle.Exclamation, "Concurrency Error")
                Return False
            End If
            Return True
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
            Return False
        End Try

    End Function

    Private Function GetCustomerAlertUpdateTimeByAlerNo(alertNo As String) As DateTime
        Try
            Dim lastUpdateTimeActual As DateTime
            Dim dt As DataTable = GetCustomerAlertTableByAlertNo(alertNo)
            If dt.Rows.Count > 0 Then
                lastUpdateTimeActual = CType(dt.Rows(0).Item("cswca_update_date"), DateTime)
            End If
            Return lastUpdateTimeActual
        Catch ex As Exception
            HandleGlobalException(ex, ex.Message)
            Throw
        End Try

    End Function

    Private Function GetCustomerAlertTableByAlertNo(alertNo As String) As DataTable
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_ALL_CUSTOMER_ALERT",
                                    New Dictionary(Of String, String) From {
                                    {"cswca_alert_no", alertNo}
                                    })
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI GET_ALL_CUSTOMER_ALERT Retrieve Error." & vbCrLf & ex.Message)
            Throw
        Finally
            Cursor = Cursors.Default
        End Try

    End Function

    Private Sub RefreshAlertCustomer(Optional ByVal searchID As String = "")
        dsAlert.Tables("AllCustomerAlert").Clear()
        InitDataTable(searchID)

        ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
        'If ExternalUser Then
        '    grdAlert.DataSource = MaskDTsource(dsAlert.Tables("AllCustomerAlert"), "cswca_hkid", MaskData.HKID)
        'Else
        '    grdAlert.DataSource = dsAlert.Tables("AllCustomerAlert")
        'End If
        grdAlert.DataSource = dsAlert.Tables("AllCustomerAlert")

        chkValid.DataBindings.Remove(chkValid.DataBindings.Item("Checked"))
        txtID.DataBindings.Remove(txtID.DataBindings.Item("Text"))
        txtCustID.DataBindings.Remove(txtCustID.DataBindings.Item("Text"))
        txtMessage.DataBindings.Remove(txtMessage.DataBindings.Item("Text"))

        InitBindData()
        Me.cmdCancel.Enabled = False
        Me.cmdAdd.Enabled = True
        Me.cmdDelete.Enabled = True
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If MsgBox("Do you want to delete the selected record?", MsgBoxStyle.YesNo, gSystem) = MsgBoxResult.Yes Then
            bm = Me.BindingContext(dsAlert.Tables("AllCustomerAlert"))
            Try
                APIServiceBL.ExecAPIBusi(getCompanyCode(), "DELETE_CUSTOMER_ALERT_BY_NO",
                                    New Dictionary(Of String, String) From {
                                    {"cswca_alert_no", CType(bm.Current, DataRowView).Row.Item("cswca_alert_no")}
                                    })
                MsgBox("Delete Data Success !", , "Insert Data Success")
                RefreshAlertCustomer()
            Catch ex As Exception
                HandleGlobalException(ex, "Error occurs when deleting the comments. Error:  " & ex.Message)
            End Try
        End If
        'If MsgBox("Do you want to delete the selected record?", MsgBoxStyle.YesNo, gSystem) = MsgBoxResult.Yes Then

        '    Dim sqldel As String
        '    Dim sqlCmd As SqlCommand

        '    sqldel = "delete csw_customer_alert where cswca_alert_no = " & CType(bm.Current, DataRowView).Row.Item("cswca_alert_no")

        '    sqlconnect.ConnectionString = strCIWConn
        '    sqlconnect.Open()
        '    sqlCmd = New SqlCommand(sqldel, sqlconnect)

        '    Try
        '        sqlCmd.ExecuteNonQuery()
        '        sqlconnect.Close()
        '        sqlconnect.Dispose()
        '    Catch sqlex As SqlException
        '        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '        Exit Sub
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '        Exit Sub
        '    End Try

        '    Dim dtTemp As DataTable
        '    Me.dtAlert.Clear()
        '    Me.dtAlert = selectRecord()
        '    Me.grdAlert.DataSource = dtAlert
        '    Me.grdAlert.Refresh()
        '    dtTemp = selectRecord()
        '    Dim dr As DataRow
        '    Dim ar() As Object
        '    For Each dr In dtTemp.Rows
        '        ar = dr.ItemArray
        '        dtAlert.Rows.Add(ar)
        '    Next
        '    updateDisplay()
        '    MsgBox("Record deleted successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Alert Maintenance")
        '    wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
        'End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        RefreshAlertCustomer()
        txtSearchID.Text = String.Empty
        cmdAdd.Focus()
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        RefreshAlertCustomer()
        txtSearchID.Text = String.Empty
        txtSearchID.Focus()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Me.BindingContext(dsAlert.Tables("AllCustomerAlert")).EndCurrentEdit()
        If dsAlert.HasChanges Then
            If MsgBox("Do you want to save the modified data? ", MsgBoxStyle.YesNo, "Confirm Save") = MsgBoxResult.Yes Then

                Dim dr As DataRow
                Dim insertedDataTable = dsAlert.Tables("AllCustomerAlert").GetChanges(DataRowState.Added)
                If Not IsNothing(insertedDataTable) Then
                    For Each dr In insertedDataTable.Rows
                        InsertCustomerAlert(dr)
                    Next
                End If
                Dim updatedDataTable = dsAlert.Tables("AllCustomerAlert").GetChanges(DataRowState.Modified)
                If Not IsNothing(updatedDataTable) Then
                    For Each dr In updatedDataTable.Rows
                        UpdateCustomerAlert(dr)
                    Next
                End If

            End If
        Else
            MsgBox("There is no insert or update data to save!")
        End If

        'wndMain.Cursor = Cursors.WaitCursor

        'If (Me.txtID.Text <> "" OrElse Me.txtCustID.Text <> "") AndAlso Me.txtMessage.Text <> "" Then
        '    If checkExist(Me.txtID.Text, Me.txtCustID.Text) Then
        '        If blnAdd Then
        '            addRecord()
        '        Else
        '            editRecord()
        '        End If
        '    Else
        '        MsgBox("HKID/CustomerID Not found.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Save")
        '        wndMain.Cursor = Cursors.Default
        '        Exit Sub
        '    End If
        'Else
        '    MsgBox("Missing Information.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
        '    If Me.txtID.Text = "" Then
        '        Me.txtID.Focus()
        '    ElseIf Me.txtCustID.Text = "" Then
        '        Me.txtCustID.Focus()
        '    Else
        '        Me.txtMessage.Focus()
        '    End If
        '    wndMain.Cursor = Cursors.Default
        '    Exit Sub
        'End If
        'wndMain.Cursor = Cursors.Default

    End Sub

    Private Function GetCustomerAlertTableByHkId(hkId As String) As DataTable
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Try
            ds = APIServiceBL.CallAPIBusi(getCompanyCode(), "GET_ALL_CUSTOMER_ALERT",
                                    New Dictionary(Of String, String) From {
                                    {"cswca_hkid", hkId}
                                    })
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            HandleGlobalException(ex, "CRSAPI GET_ALL_CUSTOMER_ALERT Retrieve Error." & vbCrLf & ex.Message)
            Return dt
        Finally
            Cursor = Cursors.Default
        End Try
        Return dt

    End Function

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Dim searchID = Trim(Me.txtSearchID.Text)
        RefreshAlertCustomer(searchID)
    End Sub

    Private Sub addRecord()

        'Dim strInsert As String

        ''sqlconnect.ConnectionString = strCIWConn

        ''Dim nr As DataRow
        ''nr = dtAlert.NewRow
        ''With nr
        ''    .Item("cswca_hkid") = Me.txtID.Text
        ''    .Item("cswca_customerid") = IIf(Me.txtCustID.Text <> "", Me.txtCustID.Text, 0)
        ''    .Item("cswca_message") = Me.txtMessage.Text
        ''    .Item("cswca_prompt") = IIf(Me.chkValid.Checked, "Y", "N")
        ''    .Item("cswca_update_user") = System.Environment.UserName
        ''    .Item("cswca_update_date") = FormatSQLDate(Now, gDateTimeFormat)
        ''End With
        'strInsert = "Insert into csw_customer_alert (cswca_hkid, cswca_customerid, cswca_message, cswca_prompt, cswca_update_user, cswca_update_date) " &
        '    " values ('" & txtID.Text & "', " & IIf(Me.txtCustID.Text <> "", Me.txtCustID.Text, 0) & ", '" &
        '    Me.txtMessage.Text & "','" & IIf(Me.chkValid.Checked, "Y", "N") & "','" &
        '    gsUser & "',GETDATE())"

        'Dim sqlCmd As SqlCommand
        'sqlconnect.ConnectionString = strCIWConn
        'sqlconnect.Open()
        'sqlCmd = New SqlCommand(strInsert, sqlconnect)

        'Try
        '    'dtAlert.Rows.Add(nr)

        '    'write back to database

        '    'sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        '    'sqlcb = New SqlCommandBuilder(sqlda)
        '    'sqlda.Update(dtAlert)
        '    'sqlconnect.Dispose()
        '    sqlCmd.ExecuteNonQuery()
        '    sqlconnect.Close()
        '    sqlconnect.Dispose()
        'Catch sqlex As SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    Exit Sub
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    Exit Sub
        'End Try

        ''dtAlert.AcceptChanges()

        'Dim dtTemp As DataTable
        'Me.dtAlert.Clear()
        ''Me.dtAlert = selectRecord()
        ''Me.grdAlert.DataSource = dtAlert
        ''Me.grdAlert.Refresh()
        'dtTemp = selectRecord()
        'Dim dr1 As DataRow
        'Dim ar() As Object
        'For Each dr1 In dtTemp.Rows
        '    ar = dr1.ItemArray
        '    dtAlert.Rows.Add(ar)
        'Next

        ''bm = BindingContext(dtAlert)
        'updateDisplay()
        'Me.Editable = False
        'MsgBox("Record saved successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Alert Maintenance")
        'wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

    End Sub

    Private Sub editRecord()

        'Dim sqlUpdate = "update csw_customer_alert set cswca_hkid = '" & Me.txtID.Text & "'," &
        '        "cswca_customerid = " & Me.txtCustID.Text & ",cswca_message = '" & Replace(Me.txtMessage.Text, "'", "''") & "',cswca_prompt = '" &
        '        IIf(Me.chkValid.Checked, "Y", "N") & "',cswca_update_user = '" & System.Environment.UserName & "',cswca_update_date = " &
        '        FormatSQLDate(Now, gDateTimeFormat) & " where cswca_alert_no = " & CType(bm.Current, DataRowView).Row.Item("cswca_alert_no")

        'Dim sqlCmd As SqlCommand
        'sqlconnect.ConnectionString = strCIWConn
        'sqlconnect.Open()
        'sqlCmd = New SqlCommand(sqlUpdate, sqlconnect)

        'Try
        '    sqlCmd.ExecuteNonQuery()
        '    sqlconnect.Close()
        '    sqlconnect.Dispose()
        'Catch sqlex As SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    Exit Sub
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        '    Exit Sub
        'End Try

        'Dim dtTemp As DataTable
        'Me.dtAlert.Clear()
        ''Me.dtAlert = selectRecord()
        ''Me.grdAlert.DataSource = dtAlert
        ''Me.grdAlert.Refresh()
        'dtTemp = selectRecord()
        'Dim dr As DataRow
        'Dim ar() As Object
        'For Each dr In dtTemp.Rows
        '    ar = dr.ItemArray
        '    dtAlert.Rows.Add(ar)
        'Next

        ''bm = BindingContext(dtAlert)
        'updateDisplay()
        'Me.Editable = False
        'blnAdd = True
        'MsgBox("Record saved successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Alert Maintenance")
        'wndMain.StatusBarPanel1.Text = bm.Count & " records selected"

    End Sub

    Private Function checkExist(ByRef strHKID As String, ByRef strClientID As String) As Boolean

        'Dim strEnq, strWhere As String
        'Dim blnFind As Boolean = False
        'Dim sqlCmd As SqlCommand
        'Dim reader As SqlDataReader

        'strEnq = "select customerID,governmentidcard from customer "
        'If strHKID <> "" AndAlso strClientID <> "" Then
        '    strWhere = " where customerID = '" & strClientID & "' and governmentidcard = '" & strHKID & "'"
        'Else
        '    If strClientID <> "" Then
        '        strWhere = " where customerID = '" & strClientID & "'"
        '    End If
        '    If strHKID <> "" Then
        '        strWhere = " where governmentidcard = '" & strHKID & "'"
        '    End If
        'End If
        'strEnq &= strWhere

        'sqlconnect.ConnectionString = strCIWConn
        'sqlconnect.Open()
        'sqlCmd = New SqlCommand(strEnq, sqlconnect)

        'Try
        '    reader = sqlCmd.ExecuteReader()
        '    If reader.HasRows Then
        '        reader.Read()
        '        blnFind = True
        '        strHKID = reader.Item("governmentidcard")
        '        strClientID = reader.Item("customerID")
        '        Return blnFind
        '    End If
        '    reader.Close()
        'Catch sqlex As SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        'Finally
        '    sqlconnect.Close()
        'End Try

        'sqlconnect.Dispose()
        'Return blnFind

    End Function

    Private Sub grdAlert_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAlert.MouseUp
        'Dim pt = New Point(e.X, e.Y)
        'Dim hti As DataGrid.HitTestInfo = grdAlert.HitTest(pt)

        'If hti.Type = DataGrid.HitTestType.Cell Then
        '    grdAlert.CurrentCell = New DataGridCell(hti.Row, hti.Column)
        '    grdAlert.Select(hti.Row)
        'End If
    End Sub


End Class
