Imports System.Data.SqlClient

Public Class uclAdditionalMcu
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        grpButton.Visible = CheckUPSAccess("Misc Info")

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
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents grdAdditional As System.Windows.Forms.DataGrid
    Friend WithEvents txtMiscProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtMiscProvider As System.Windows.Forms.TextBox
    Friend WithEvents cboMiscType As System.Windows.Forms.ComboBox
    Friend WithEvents txtMiscEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtMiscRenewal As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtMiscStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCreateUser As System.Windows.Forms.TextBox
    Friend WithEvents txtCreateDate As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUpdDate As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUpdUser As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents grpButton As System.Windows.Forms.GroupBox
    Friend WithEvents cmdMiscEdit As System.Windows.Forms.Button
    Friend WithEvents cmdMiscSave As System.Windows.Forms.Button
    Friend WithEvents cmdMiscCancel As System.Windows.Forms.Button
    Friend WithEvents cmdMiscDelete As System.Windows.Forms.Button
    Friend WithEvents cmdMiscAdd As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtMiscProduct = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtMiscProvider = New System.Windows.Forms.TextBox
        Me.cboMiscType = New System.Windows.Forms.ComboBox
        Me.grdAdditional = New System.Windows.Forms.DataGrid
        Me.txtMiscEnd = New System.Windows.Forms.DateTimePicker
        Me.txtMiscRenewal = New System.Windows.Forms.DateTimePicker
        Me.txtMiscStart = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCreateUser = New System.Windows.Forms.TextBox
        Me.txtCreateDate = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUpdDate = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtUpdUser = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboStatus = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.grpButton = New System.Windows.Forms.GroupBox
        Me.cmdMiscEdit = New System.Windows.Forms.Button
        Me.cmdMiscSave = New System.Windows.Forms.Button
        Me.cmdMiscCancel = New System.Windows.Forms.Button
        Me.cmdMiscDelete = New System.Windows.Forms.Button
        Me.cmdMiscAdd = New System.Windows.Forms.Button
        CType(Me.grdAdditional, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpButton.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(544, 140)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(54, 16)
        Me.Label18.TabIndex = 126
        Me.Label18.Text = "End Date:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(352, 140)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(58, 16)
        Me.Label16.TabIndex = 124
        Me.Label16.Text = "Start Date:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(520, 164)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(78, 16)
        Me.Label17.TabIndex = 122
        Me.Label17.Text = "Renewal Date:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(4, 188)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 16)
        Me.Label15.TabIndex = 120
        Me.Label15.Text = "Description:"
        '
        'txtMiscProduct
        '
        Me.txtMiscProduct.BackColor = System.Drawing.SystemColors.Window
        Me.txtMiscProduct.Location = New System.Drawing.Point(72, 184)
        Me.txtMiscProduct.Multiline = True
        Me.txtMiscProduct.Name = "txtMiscProduct"
        Me.txtMiscProduct.ReadOnly = True
        Me.txtMiscProduct.Size = New System.Drawing.Size(652, 44)
        Me.txtMiscProduct.TabIndex = 119
        Me.txtMiscProduct.Text = ""
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(16, 164)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(50, 16)
        Me.Label14.TabIndex = 118
        Me.Label14.Text = "Provider:"
        '
        'txtMiscProvider
        '
        Me.txtMiscProvider.BackColor = System.Drawing.SystemColors.Window
        Me.txtMiscProvider.Location = New System.Drawing.Point(72, 160)
        Me.txtMiscProvider.Name = "txtMiscProvider"
        Me.txtMiscProvider.ReadOnly = True
        Me.txtMiscProvider.Size = New System.Drawing.Size(428, 20)
        Me.txtMiscProvider.TabIndex = 117
        Me.txtMiscProvider.Text = ""
        '
        'cboMiscType
        '
        Me.cboMiscType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMiscType.Items.AddRange(New Object() {"Other Insurance", "ING Family Club Member"})
        Me.cboMiscType.Location = New System.Drawing.Point(72, 136)
        Me.cboMiscType.Name = "cboMiscType"
        Me.cboMiscType.Size = New System.Drawing.Size(272, 21)
        Me.cboMiscType.TabIndex = 115
        '
        'grdAdditional
        '
        Me.grdAdditional.AlternatingBackColor = System.Drawing.Color.White
        Me.grdAdditional.BackColor = System.Drawing.Color.White
        Me.grdAdditional.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAdditional.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAdditional.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAdditional.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAdditional.CaptionVisible = False
        Me.grdAdditional.DataMember = ""
        Me.grdAdditional.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdAdditional.FlatMode = True
        Me.grdAdditional.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdAdditional.ForeColor = System.Drawing.Color.Black
        Me.grdAdditional.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAdditional.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdAdditional.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdAdditional.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAdditional.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAdditional.Location = New System.Drawing.Point(0, 0)
        Me.grdAdditional.Name = "grdAdditional"
        Me.grdAdditional.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAdditional.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAdditional.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAdditional.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAdditional.Size = New System.Drawing.Size(744, 128)
        Me.grdAdditional.TabIndex = 114
        '
        'txtMiscEnd
        '
        Me.txtMiscEnd.CustomFormat = "dd-MMM-yyyy"
        Me.txtMiscEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMiscEnd.Location = New System.Drawing.Point(604, 136)
        Me.txtMiscEnd.Name = "txtMiscEnd"
        Me.txtMiscEnd.Size = New System.Drawing.Size(120, 20)
        Me.txtMiscEnd.TabIndex = 132
        '
        'txtMiscRenewal
        '
        Me.txtMiscRenewal.CustomFormat = "dd-MMM-yyyy"
        Me.txtMiscRenewal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMiscRenewal.Location = New System.Drawing.Point(604, 160)
        Me.txtMiscRenewal.Name = "txtMiscRenewal"
        Me.txtMiscRenewal.Size = New System.Drawing.Size(120, 20)
        Me.txtMiscRenewal.TabIndex = 133
        '
        'txtMiscStart
        '
        Me.txtMiscStart.CustomFormat = "dd-MMM-yyyy"
        Me.txtMiscStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMiscStart.Location = New System.Drawing.Point(416, 136)
        Me.txtMiscStart.Name = "txtMiscStart"
        Me.txtMiscStart.Size = New System.Drawing.Size(120, 20)
        Me.txtMiscStart.TabIndex = 134
        Me.txtMiscStart.Value = New Date(2006, 3, 3, 12, 25, 49, 807)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 16)
        Me.Label1.TabIndex = 136
        Me.Label1.Text = "Create User:"
        '
        'txtCreateUser
        '
        Me.txtCreateUser.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreateUser.Location = New System.Drawing.Point(72, 236)
        Me.txtCreateUser.Name = "txtCreateUser"
        Me.txtCreateUser.ReadOnly = True
        Me.txtCreateUser.TabIndex = 137
        Me.txtCreateUser.Text = ""
        '
        'txtCreateDate
        '
        Me.txtCreateDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreateDate.Location = New System.Drawing.Point(220, 236)
        Me.txtCreateDate.Name = "txtCreateDate"
        Me.txtCreateDate.ReadOnly = True
        Me.txtCreateDate.TabIndex = 139
        Me.txtCreateDate.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(184, 240)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 16)
        Me.Label2.TabIndex = 138
        Me.Label2.Text = "Date:"
        '
        'txtUpdDate
        '
        Me.txtUpdDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtUpdDate.Location = New System.Drawing.Point(580, 236)
        Me.txtUpdDate.Name = "txtUpdDate"
        Me.txtUpdDate.ReadOnly = True
        Me.txtUpdDate.TabIndex = 143
        Me.txtUpdDate.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(544, 240)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 16)
        Me.Label3.TabIndex = 142
        Me.Label3.Text = "Date:"
        '
        'txtUpdUser
        '
        Me.txtUpdUser.BackColor = System.Drawing.SystemColors.Window
        Me.txtUpdUser.Location = New System.Drawing.Point(432, 236)
        Me.txtUpdUser.Name = "txtUpdUser"
        Me.txtUpdUser.ReadOnly = True
        Me.txtUpdUser.TabIndex = 141
        Me.txtUpdUser.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(356, 240)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 16)
        Me.Label4.TabIndex = 140
        Me.Label4.Text = "Update User:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 16)
        Me.Label5.TabIndex = 144
        Me.Label5.Text = "Type:"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Items.AddRange(New Object() {"Enrolled", "Withdrew", "Participated"})
        Me.cboStatus.Location = New System.Drawing.Point(72, 264)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(100, 21)
        Me.cboStatus.TabIndex = 145
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 268)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 16)
        Me.Label7.TabIndex = 147
        Me.Label7.Text = "Status:"
        '
        'grpButton
        '
        Me.grpButton.Controls.Add(Me.cmdMiscEdit)
        Me.grpButton.Controls.Add(Me.cmdMiscSave)
        Me.grpButton.Controls.Add(Me.cmdMiscCancel)
        Me.grpButton.Controls.Add(Me.cmdMiscDelete)
        Me.grpButton.Controls.Add(Me.cmdMiscAdd)
        Me.grpButton.Location = New System.Drawing.Point(328, 264)
        Me.grpButton.Name = "grpButton"
        Me.grpButton.Size = New System.Drawing.Size(408, 40)
        Me.grpButton.TabIndex = 148
        Me.grpButton.TabStop = False
        '
        'cmdMiscEdit
        '
        Me.cmdMiscEdit.Location = New System.Drawing.Point(88, 12)
        Me.cmdMiscEdit.Name = "cmdMiscEdit"
        Me.cmdMiscEdit.TabIndex = 136
        Me.cmdMiscEdit.Text = "&Edit"
        '
        'cmdMiscSave
        '
        Me.cmdMiscSave.Location = New System.Drawing.Point(248, 12)
        Me.cmdMiscSave.Name = "cmdMiscSave"
        Me.cmdMiscSave.TabIndex = 135
        Me.cmdMiscSave.Text = "&Save"
        '
        'cmdMiscCancel
        '
        Me.cmdMiscCancel.Location = New System.Drawing.Point(328, 12)
        Me.cmdMiscCancel.Name = "cmdMiscCancel"
        Me.cmdMiscCancel.TabIndex = 134
        Me.cmdMiscCancel.Text = "&Cancel"
        '
        'cmdMiscDelete
        '
        Me.cmdMiscDelete.Location = New System.Drawing.Point(168, 12)
        Me.cmdMiscDelete.Name = "cmdMiscDelete"
        Me.cmdMiscDelete.TabIndex = 133
        Me.cmdMiscDelete.Text = "&Delete"
        '
        'cmdMiscAdd
        '
        Me.cmdMiscAdd.Location = New System.Drawing.Point(8, 12)
        Me.cmdMiscAdd.Name = "cmdMiscAdd"
        Me.cmdMiscAdd.TabIndex = 132
        Me.cmdMiscAdd.Text = "&Add"
        '
        'uclAdditional
        '
        Me.Controls.Add(Me.grpButton)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtUpdDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtUpdUser)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCreateDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCreateUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMiscStart)
        Me.Controls.Add(Me.txtMiscRenewal)
        Me.Controls.Add(Me.txtMiscEnd)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtMiscProduct)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtMiscProvider)
        Me.Controls.Add(Me.cboMiscType)
        Me.Controls.Add(Me.grdAdditional)
        Me.Name = "uclAdditional"
        Me.Size = New System.Drawing.Size(744, 312)
        CType(Me.grdAdditional, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpButton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private gdtAddType, gdtAdditional As DataTable
    Private strCustID As String
    Private gUpdateMode As String
    Dim WithEvents bm As BindingManagerBase

    Public Property CustID() As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                If Value.Length > 0 Then
                    strCustID = Value
                    Call buildUI()
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property editMode() As Boolean
        Set(ByVal Value As Boolean)
            Me.cmdMiscAdd.Enabled = Not Value

            If gUpdateMode = "" AndAlso (gdtAdditional Is Nothing OrElse gdtAdditional.Rows.Count = 0) Then
                Me.cmdMiscEdit.Enabled = Value
                Me.cmdMiscDelete.Enabled = Value
            Else
                Me.cmdMiscEdit.Enabled = Not Value
                Me.cmdMiscDelete.Enabled = Not Value
            End If

            Me.cmdMiscSave.Enabled = Value
            Me.cmdMiscCancel.Enabled = Value

            Me.cboMiscType.Enabled = Value
            Me.txtMiscStart.Enabled = Value
            Me.txtMiscEnd.Enabled = Value
            Me.txtMiscRenewal.Enabled = Value
            cboStatus.Enabled = Value
            'Me.txtMiscProvider.Enabled = Value
            'Me.txtMiscProduct.Enabled = Value
            Me.txtMiscProvider.ReadOnly = Not Value
            Me.txtMiscProduct.ReadOnly = Not Value

            Me.grdAdditional.Enabled = Not Value
        End Set
    End Property

    Public Function resetDS()
        Me.gdtAdditional.Clear()
        Me.grdAdditional.TableStyles.Clear()
        Me.editMode = False
        gUpdateMode = ""
    End Function


    Public Sub buildUI()
        'fill the type combobox
        fillType()

        If gdtAdditional Is Nothing Then
            gdtAdditional = New DataTable("Additional")
            gdtAdditional.Columns.Add("TypeID", Type.GetType("System.String"))
            gdtAdditional.Columns.Add("Type", Type.GetType("System.String"))
            gdtAdditional.Columns.Add("Provider", Type.GetType("System.String"))
            gdtAdditional.Columns.Add("StartDate", Type.GetType("System.DateTime"))
            gdtAdditional.Columns.Add("EndDate", Type.GetType("System.DateTime"))
            gdtAdditional.Columns.Add("RenewalDate", Type.GetType("System.DateTime"))
            gdtAdditional.Columns.Add("Product", Type.GetType("System.String"))
        End If

        'set the Grid Style
        If Me.grdAdditional.TableStyles.Count = 0 Then
            buildGridStyle()
        End If

        fillDT()
        Me.editMode = False

    End Sub

    Private Sub buildGridStyle()
        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "Type"
        cs.HeaderText = "Type"
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "Provider"
        cs.HeaderText = "Provider"
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "Additional"
        grdAdditional.TableStyles.Add(ts)

        grdAdditional.DataSource = gdtAdditional
        grdAdditional.AllowDrop = False
        grdAdditional.ReadOnly = True

    End Sub
    ''' <summary>
    ''' Fill datatable
    ''' Lubin 2022-11-17 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Private Sub fillDT()
        Try
            'strSQL = "select cswmit_type as 'TypeID', cswmit_desc as 'Type', cswmif_provider as 'Provider', cswmif_start_date as 'StartDate', " & _
            '         "cswmif_end_date as 'EndDate',  cswmif_renewal_date as 'RenewalDate',  cswmif_desc as 'Product', cswmif_id as 'ID', " & _
            '         "cswmif_create_user, cswmif_create_date, cswmif_update_user, cswmif_update_date, cswmif_status " & _
            '         "from csw_misc_info, csw_misc_info_type " & _
            '         "where cswmif_customer_id = '" & strCustID & "' " & _
            '         "and cswmit_type = cswmif_type "

            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "FRM_CUST_MISC_INFO_TYPE",
             New Dictionary(Of String, String)() From {
            {"strCustID", strCustID}
            })
            gdtAdditional = retDs.Tables(0).Copy

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        bm = Me.BindingContext(gdtAdditional)

        UpdatePT()
    End Sub

    Private Sub UpdatePT()
        If bm.Position >= 0 Then
            Dim drI As DataRow = CType(bm.Current, DataRowView).Row()
            If Not drI Is Nothing Then
                Me.cboMiscType.Text = Trim(drI.Item("Type"))

                Dim dtStart As DateTime
                dtStart = IIf(IsDBNull(drI.Item("StartDate")), #1/1/1900#, drI.Item("StartDate"))
                If Not IsDBNull(dtStart) Then
                    Me.txtMiscStart.Text = Format(dtStart, gDateFormat)
                Else
                    Me.txtMiscStart.Text = "Nil"
                End If

                Dim dtEnd As DateTime
                dtEnd = IIf(IsDBNull(drI.Item("EndDate")), #1/1/1900#, drI.Item("EndDate"))
                If Not IsDBNull(dtEnd) Then
                    Me.txtMiscEnd.Text = Format(dtEnd, gDateFormat)
                Else
                    Me.txtMiscEnd.Text = "Nil"
                End If

                Dim dtRenewal As DateTime
                dtRenewal = IIf(IsDBNull(drI.Item("RenewalDate")), #1/1/1900#, drI.Item("RenewalDate"))
                If Not IsDBNull(dtRenewal) Then
                    Me.txtMiscRenewal.Text = Format(dtRenewal, gDateFormat)
                Else
                    Me.txtMiscRenewal.Text = "Nil"
                End If

                Me.txtMiscProvider.Text = Trim(IIf(IsDBNull(drI.Item("Provider")), "", drI.Item("Provider")))
                Me.txtMiscProduct.Text = Trim(IIf(IsDBNull(drI.Item("Product")), "", drI.Item("Product")))

                txtCreateUser.Text = Trim(IIf(IsDBNull(drI.Item("cswmif_create_user")), "", drI.Item("cswmif_create_user")))
                txtCreateDate.Text = Trim(IIf(IsDBNull(drI.Item("cswmif_create_date")), "", Format(drI.Item("cswmif_create_date"), gDateFormat)))
                txtUpdUser.Text = Trim(IIf(IsDBNull(drI.Item("cswmif_update_user")), "", drI.Item("cswmif_update_user")))
                txtUpdDate.Text = Trim(IIf(IsDBNull(drI.Item("cswmif_update_date")), "", Format(drI.Item("cswmif_update_date"), gDateFormat)))
                Select Case drI.Item("cswmif_status")
                    Case "E"
                        cboStatus.SelectedItem = "Enrolled"
                    Case "W"
                        cboStatus.SelectedItem = "Withdrew"
                    Case "P"
                        cboStatus.SelectedItem = "Participated"
                End Select
            End If
        Else
            cboStatus.SelectedIndex = -1
            Me.cboMiscType.Text = ""
            Me.txtMiscStart.Text = ""
            Me.txtMiscEnd.Text = ""
            Me.txtMiscRenewal.Text = ""
            Me.txtMiscProvider.Text = ""
            Me.txtMiscProduct.Text = ""
            txtCreateUser.Text = ""
            txtCreateDate.Text = ""
            txtUpdUser.Text = ""
            txtUpdDate.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' LoadComboBox by busiId
    ''' Lubin 2022-11-17 ITSR-3487 Move the Macau query logic to the server side.
    ''' Changes: change sql to busiId
    ''' </summary>
    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As ComboBox, ByVal strCode As String,
                          ByVal strName As String, ByVal busiId As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean
        Dim blnLoad As Boolean

        If dt Is Nothing Then
            Try
                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, busiId,
                 New Dictionary(Of String, String)() From {
                 })

                dt = retDs.Tables(0).Copy
                blnLoad = True
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                blnLoad = False
            End Try
        End If

        If blnLoad Then
            cbo.DataSource = dt
            cbo.DisplayMember = strName
            cbo.ValueMember = strCode
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Fill info type
    ''' Lubin 2022-11-17 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Private Sub fillType()
        'strSQL = "select distinct cswmit_type, cswmit_desc from csw_misc_info_type"
        LoadComboBox(gdtAddType, cboMiscType, "cswmit_type", "cswmit_desc", "FRM_MISC_INFO_TYPE")
    End Sub

    Private Sub grdAdditional_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAdditional.CurrentCellChanged
        UpdatePT()
    End Sub

    Private Sub cmdMiscAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMiscAdd.Click
        Me.txtMiscStart.Text = ""
        Me.txtMiscEnd.Text = ""
        Me.txtMiscRenewal.Text = ""
        Me.txtMiscProvider.Text = ""
        Me.txtMiscProduct.Text = ""
        txtCreateUser.Text = ""
        txtCreateDate.Text = ""
        txtUpdUser.Text = ""
        txtUpdDate.Text = ""
        cboStatus.SelectedIndex = -1

        gUpdateMode = "A"
        Me.editMode = True
    End Sub

    Private Sub cmdMiscEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMiscEdit.Click
        gUpdateMode = "E"
        Me.editMode = True
    End Sub

    Private Sub cmdMiscDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMiscDelete.Click
        If MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            DeleteRecord()
            Me.gdtAdditional.Clear()
            fillDT()
        End If
    End Sub

    Private Function checkDate(ByRef incontrol As TextBox) As Boolean
        Dim OK As Boolean = False

        If incontrol.Text.Length > 0 And incontrol.Text.Length <> 11 Then
            OK = False
        Else
            If (incontrol.Text.Substring(2, 1) <> "-") Or (incontrol.Text.Substring(6, 1) <> "-") Then
                OK = False
            End If

            Try
                Convert.ToDateTime(incontrol.Text)
                OK = True
            Catch ex As Exception
                OK = False
            End Try
        End If

        If Not OK Then
            MessageBox.Show("Invalid Date format. It should be [dd-MMM-yyyy].", "Warning", MessageBoxButtons.OK)
            incontrol.Text = ""
            incontrol.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub cmdMiscSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMiscSave.Click
        Dim dateOK, textOK, updateOK As Boolean

        'Validate the Date(s) being entered
        'If checkDate(Me.txtMiscStart.Text) And checkDate(Me.txtMiscEnd) And checkDate(Me.txtMiscRenewal) Then
        '    dateOK = True
        'Else
        '    dateOK = False
        'End If
        dateOK = True

        'Validate the Textfield(s) being entered
        If Me.txtMiscProvider.Text <> "" And Me.txtMiscProduct.Text <> "" Then
            textOK = True
        Else
            MessageBox.Show("Missing Provider/ Product.", "Warning", MessageBoxButtons.OK)
            textOK = False
        End If

        If dateOK And textOK Then
            If gUpdateMode <> "" Then
                updateOK = InsertRecord(gUpdateMode)
            End If

            If updateOK Then
                Me.gUpdateMode = ""
                Me.editMode = False
                Me.gdtAdditional.Clear()
                fillDT()
            End If
        End If

    End Sub

    Private Sub cmdMiscCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMiscCancel.Click
        Me.gUpdateMode = ""
        Me.editMode = False
        UpdatePT()
    End Sub

    ''' <summary>
    ''' Insert msc_info.
    ''' Lubin 2022-11-17 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Private Function InsertRecord(ByVal inMode As String) As Boolean
        Dim strExec, strStatus As String
        Dim sqlconnect As New SqlConnection
        Dim sqlCmd As SqlCommand
        sqlCmd = New SqlCommand

        Select Case cboStatus.SelectedItem
            Case "Enrolled"
                strStatus = "E"
            Case "Withdrew"
                strStatus = "W"
            Case "Participated"
                strStatus = "P"
        End Select

        strExec = ""
        Try
            If inMode = "A" Then
                APIServiceBL.ExecAPIBusi(g_McuComp,
                                                "FRM_INST_MISC_INFO",
                                                New Dictionary(Of String, String) From {
                                                {"cswmifType", cboMiscType.SelectedValue.ToString},
                                                {"strCustID", strCustID},
                                                {"miscStart", txtMiscStart.Text},
                                                {"miscEnd", txtMiscEnd.Text},
                                                {"cswmifProvider", txtMiscProvider.Text},
                                                {"renewalDate", txtMiscRenewal.Text},
                                                {"cswmifDesc", txtMiscProduct.Text},
                                                {"createUser", gsUser},
                                                {"strStatus", If(String.IsNullOrEmpty(strStatus), DBNull.Value, strStatus)}
                                                })
                'strExec = "insert into csw_misc_info (cswmif_type, cswmif_customer_id, cswmif_start_date, cswmif_end_date, cswmif_provider, " & _
                '    "cswmif_renewal_date, cswmif_desc, cswmif_create_user, cswmif_create_date, cswmif_update_user, cswmif_update_date, cswmif_status) " & _
                '    "values('" & cboMiscType.SelectedValue.ToString.Replace("'", "''") & "','" & strCustID & "','" &
                '    txtMiscStart.Text & "', " & _
                '    "'" & txtMiscEnd.Text & "','" & txtMiscProvider.Text.Replace("'", "''") & "',
                '    '" & txtMiscRenewal.Text & "','" & txtMiscProduct.Text.Replace("'", "''") & "','" & gsUser & "', " & _
                '    "getdate(),'" & gsUser & "', getdate(), '" & strStatus & "') "

            ElseIf inMode = "E" Then
                If bm.Position <> -1 Then
                    APIServiceBL.ExecAPIBusi(g_McuComp,
                                                "FRM_UPDATE_MISC_INFO",
                                                New Dictionary(Of String, String) From {
                                                {"cswmifType", cboMiscType.SelectedValue.ToString},
                                                {"strCustID", strCustID},
                                                {"miscStart", txtMiscStart.Text},
                                                {"miscEnd", txtMiscEnd.Text},
                                                {"cswmifProvider", txtMiscProvider.Text},
                                                {"renewalDate", txtMiscRenewal.Text},
                                                {"cswmifDesc", txtMiscProduct.Text},
                                                {"createUser", gsUser},
                                                {"strStatus", If(String.IsNullOrEmpty(strStatus), DBNull.Value, strStatus)},
                                                {"cswmifId", CType(bm.Current, DataRowView).Row.Item("ID")}
                                                })
                    'strExec = "update csw_misc_info set cswmif_type = '" & cboMiscType.SelectedValue.ToString.Replace("'", "''") & "', cswmif_customer_id = '" & strCustID & "', cswmif_start_date = '" & txtMiscStart.Text & "', " & _
                    '        "cswmif_end_date = '" & txtMiscEnd.Text & "', cswmif_provider = '" & txtMiscProvider.Text.Replace("'", "''") & "', cswmif_renewal_date = '" & txtMiscRenewal.Text & "', cswmif_desc = '" & txtMiscProduct.Text.Replace("'", "''") & "', " & _
                    '        "cswmif_update_user = '" & gsUser & "', cswmif_update_date = getdate(), cswmif_status = '" & strStatus & "' " & _
                    '        "Where cswmif_id = " & CType(bm.Current, DataRowView).Row.Item("ID")
                Else
                    Exit Function
                End If

            End If

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Return False
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Return False
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Delete msc_info.
    ''' Lubin 2022-11-17 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Private Function DeleteRecord() As Boolean

        'strDel = "delete csw_misc_info where cswmif_id = "
        If bm.Position <> -1 Then

            Try
                APIServiceBL.ExecAPIBusi(g_McuComp,
                                                 "FRM_DEL_MISC_INFO",
                                                 New Dictionary(Of String, String) From {
                                                 {"cswmifId", CType(bm.Current, DataRowView).Row.Item("ID")}
                                                 })

            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Return False
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Return False
            End Try

            Return True
        End If

    End Function

    Private Sub DataGrid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAdditional.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAdditional.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAdditional.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAdditional.Select(hti.Row)
        End If
    End Sub

End Class
