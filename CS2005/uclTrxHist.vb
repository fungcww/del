Imports System.Data.SqlClient

Public Class TrxHist
    'oliver 2024-5-3 added comment for Table_Relocate_Sprint13,It will not call if life asia policy
    Inherits System.Windows.Forms.UserControl

    Dim objDS As DataSet = New DataSet("TRNHistory")
    Dim dr, dr1 As DataRow
    Dim da As SqlDataAdapter
    Dim sqlConnect As New SqlConnection
    Dim datFrom, datTo As Date
    Dim strPolicy As String
    Dim dtTRNH As DataTable
    Dim WithEvents bm As BindingManagerBase

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
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLimit As System.Windows.Forms.Label
    Friend WithEvents txtFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdGO As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grdTRNHist As System.Windows.Forms.DataGrid
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents txtCurr As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtAcc As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtScr As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtTDate As System.Windows.Forms.DateTimePicker
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.txtCurr = New System.Windows.Forms.TextBox
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.txtAcc = New System.Windows.Forms.TextBox
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.txtScr = New System.Windows.Forms.TextBox
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdTRNHist = New System.Windows.Forms.DataGrid
        Me.lblLimit = New System.Windows.Forms.Label
        Me.txtFDate = New System.Windows.Forms.DateTimePicker
        Me.cmdGO = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtTDate = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        CType(Me.grdTRNHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDate.Location = New System.Drawing.Point(72, 296)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(81, 20)
        Me.txtDate.TabIndex = 21
        '
        'txtCurr
        '
        Me.txtCurr.AcceptsReturn = True
        Me.txtCurr.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCurr.Location = New System.Drawing.Point(212, 296)
        Me.txtCurr.MaxLength = 0
        Me.txtCurr.Name = "txtCurr"
        Me.txtCurr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurr.Size = New System.Drawing.Size(81, 20)
        Me.txtCurr.TabIndex = 20
        '
        'txtAmount
        '
        Me.txtAmount.AcceptsReturn = True
        Me.txtAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmount.Location = New System.Drawing.Point(352, 296)
        Me.txtAmount.MaxLength = 0
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmount.Size = New System.Drawing.Size(81, 20)
        Me.txtAmount.TabIndex = 19
        '
        'txtAcc
        '
        Me.txtAcc.AcceptsReturn = True
        Me.txtAcc.BackColor = System.Drawing.SystemColors.Window
        Me.txtAcc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAcc.Location = New System.Drawing.Point(492, 296)
        Me.txtAcc.MaxLength = 0
        Me.txtAcc.Name = "txtAcc"
        Me.txtAcc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcc.Size = New System.Drawing.Size(81, 20)
        Me.txtAcc.TabIndex = 18
        '
        'txtDesc
        '
        Me.txtDesc.AcceptsReturn = True
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesc.Location = New System.Drawing.Point(72, 324)
        Me.txtDesc.MaxLength = 0
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesc.Size = New System.Drawing.Size(221, 20)
        Me.txtDesc.TabIndex = 17
        '
        'txtScr
        '
        Me.txtScr.AcceptsReturn = True
        Me.txtScr.BackColor = System.Drawing.SystemColors.Window
        Me.txtScr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScr.Location = New System.Drawing.Point(352, 324)
        Me.txtScr.MaxLength = 0
        Me.txtScr.Name = "txtScr"
        Me.txtScr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScr.Size = New System.Drawing.Size(81, 20)
        Me.txtScr.TabIndex = 16
        '
        'txtUser
        '
        Me.txtUser.AcceptsReturn = True
        Me.txtUser.BackColor = System.Drawing.SystemColors.Window
        Me.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUser.Location = New System.Drawing.Point(492, 324)
        Me.txtUser.MaxLength = 0
        Me.txtUser.Name = "txtUser"
        Me.txtUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUser.Size = New System.Drawing.Size(81, 20)
        Me.txtUser.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(40, 300)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(29, 14)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Date"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(164, 300)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 14)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Currency"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(308, 300)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(44, 14)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Amount"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(444, 300)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(48, 14)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Account"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 328)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Description"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(308, 328)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Screen"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(460, 328)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(30, 14)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "User"
        '
        'grdTRNHist
        '
        Me.grdTRNHist.AlternatingBackColor = System.Drawing.Color.White
        Me.grdTRNHist.BackColor = System.Drawing.Color.White
        Me.grdTRNHist.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdTRNHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdTRNHist.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdTRNHist.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdTRNHist.CaptionVisible = False
        Me.grdTRNHist.DataMember = ""
        Me.grdTRNHist.FlatMode = True
        Me.grdTRNHist.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdTRNHist.ForeColor = System.Drawing.Color.Black
        Me.grdTRNHist.GridLineColor = System.Drawing.Color.Wheat
        Me.grdTRNHist.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdTRNHist.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdTRNHist.HeaderForeColor = System.Drawing.Color.Black
        Me.grdTRNHist.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdTRNHist.Location = New System.Drawing.Point(4, 52)
        Me.grdTRNHist.Name = "grdTRNHist"
        Me.grdTRNHist.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdTRNHist.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdTRNHist.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdTRNHist.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdTRNHist.Size = New System.Drawing.Size(640, 236)
        Me.grdTRNHist.TabIndex = 29
        '
        'lblLimit
        '
        Me.lblLimit.ForeColor = System.Drawing.Color.Blue
        Me.lblLimit.Location = New System.Drawing.Point(112, 32)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(600, 16)
        Me.lblLimit.TabIndex = 88
        '
        'txtFDate
        '
        Me.txtFDate.CustomFormat = ""
        Me.txtFDate.Enabled = False
        Me.txtFDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFDate.Location = New System.Drawing.Point(112, 4)
        Me.txtFDate.Name = "txtFDate"
        Me.txtFDate.Size = New System.Drawing.Size(132, 20)
        Me.txtFDate.TabIndex = 87
        '
        'cmdGO
        '
        Me.cmdGO.Enabled = False
        Me.cmdGO.Location = New System.Drawing.Point(412, 4)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(31, 20)
        Me.cmdGO.TabIndex = 86
        Me.cmdGO.Text = "Go"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(6, 8)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(97, 13)
        Me.Label20.TabIndex = 85
        Me.Label20.Text = "Enquiry From Date:"
        '
        'txtTDate
        '
        Me.txtTDate.CustomFormat = ""
        Me.txtTDate.Enabled = False
        Me.txtTDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtTDate.Location = New System.Drawing.Point(276, 4)
        Me.txtTDate.Name = "txtTDate"
        Me.txtTDate.Size = New System.Drawing.Size(132, 20)
        Me.txtTDate.TabIndex = 89
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(252, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(20, 13)
        Me.Label8.TabIndex = 90
        Me.Label8.Text = "To"
        '
        'TrxHist
        '
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTDate)
        Me.Controls.Add(Me.lblLimit)
        Me.Controls.Add(Me.txtFDate)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.grdTRNHist)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtCurr)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.txtAcc)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtScr)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "TrxHist"
        Me.Size = New System.Drawing.Size(720, 352)
        CType(Me.grdTRNHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Property PolicyAccountID()
        Get
            PolicyAccountID = strPolicy
        End Get
        Set(ByVal Value)
            If Not Value Is Nothing Then
                strPolicy = Value
            End If
        End Set
    End Property

    Public WriteOnly Property DateFrom(ByVal DateTo As Date) As Date
        Set(ByVal Value As Date)
            datFrom = Value
            datTo = DateTo
        End Set
    End Property

    Public WriteOnly Property srcDT()
        Set(ByVal Value)
            If Not Value Is Nothing Then
                dtTRNH = Value
                objDS.Tables.Add(dtTRNH)
                Call BuildUI()
            End If
        End Set
    End Property

    Private Sub BuildUI()

        Dim strSQL As String
        Dim strErrNo As Integer
        Dim strErrMsg As String

        Try
            strSQL = "Select * from csw_currency_codes where cswcc_capsil_CurrencyCode <> '' "
            sqlConnect.ConnectionString = GetConnectionStringByCompanyID(strCompany)
            da = New SqlDataAdapter(strSQL, sqlConnect)
            da.MissingSchemaAction = MissingSchemaAction.Add
            da.MissingMappingAction = MissingMappingAction.Passthrough
            da.Fill(objDS, "csw_currency_codes")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        sqlConnect.Dispose()

        Dim objCurr As New Data.DataRelation("Curr", objDS.Tables("csw_currency_codes").Columns("cswcc_capsil_CurrencyCode"),
            objDS.Tables("TRNH").Columns("Currency"), True)

        Try
            objDS.Relations.Add(objCurr)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Date"
        cs.HeaderText = "Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New JoinTextBoxColumn("Curr", objDS.Tables("csw_currency_codes").Columns("cswcc_ciw_CurrencyCode"))
        cs.Width = 75
        cs.MappingName = "cswcc_ciw_CurrencyCode"
        cs.HeaderText = "Currency"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Amount"
        cs.HeaderText = "Amount"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Account"
        cs.HeaderText = "Account"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 140
        cs.MappingName = "Description"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "Screen"
        cs.HeaderText = "Screen"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "User"
        cs.HeaderText = "User"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "TRNH"
        grdTRNHist.TableStyles.Add(ts)

        objDS.Tables("TRNH").DefaultView.Sort = "Date ASC"
        grdTRNHist.DataSource = objDS.Tables("TRNH")
        grdTRNHist.AllowDrop = False
        grdTRNHist.ReadOnly = True
        bm = Me.BindingContext(objDS.Tables("TRNH"))
        Call CheckLimit()

        Dim bDate As Binding = New Binding("text", objDS.Tables("TRNH"), "Date")
        Me.txtDate.DataBindings.Add(bDate)
        AddHandler bDate.Format, AddressOf FormatDate

        txtAcc.DataBindings.Add("text", objDS.Tables("TRNH"), "Account")
        txtDesc.DataBindings.Add("text", objDS.Tables("TRNH"), "Description")
        txtScr.DataBindings.Add("text", objDS.Tables("TRNH"), "Screen")
        txtUser.DataBindings.Add("text", objDS.Tables("TRNH"), "User")

        Call UpdatePT()
        txtFDate.Value = datFrom
        txtTDate.Value = datTo
        If bm.Count > 0 Then
            txtFDate.Enabled = True
            txtTDate.Enabled = True
            cmdGO.Enabled = True
        End If

    End Sub

    Private Sub UpdatePT()

        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            txtCurr.Text = GetRelationValue(drI, "Curr", "cswcc_ciw_CurrencyCode")
            txtAmount.Text = Format(drI.Item("Amount"), gNumFormat)
        End If

    End Sub

    Private Sub grdAPLHist_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTRNHist.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdTRNHist.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdTRNHist.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdTRNHist.Select(hti.Row)
        End If
    End Sub

    Private Sub grdAPLHist_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTRNHist.CurrentCellChanged
        Call UpdatePT()
    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click
        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dt As DataTable

        If txtFDate.Value > txtTDate.Value Then
            MsgBox("'From Date' cannot be earlier than 'To Date'", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor

        dt = objCS.GetTrnHistory(strPolicy, CDate(txtFDate.Value), CDate(txtTDate.Value), lngErrNo, strErrMsg)
        If lngErrNo = 0 Or (lngErrNo = -1 And InStr(UCase(strErrMsg), "NO RECORD FOUND") > 0) Then
            dtTRNH.Rows.Clear()

            Dim dr As DataRow
            Dim ar() As Object
            For Each dr In dt.Rows
                ar = dr.ItemArray
                dtTRNH.Rows.Add(ar)
            Next
            Call CheckLimit()
        End If

        If lngErrNo = -1 Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

        wndMain.Cursor = Cursors.Default

    End Sub

    Public Sub CheckLimit()
        lblLimit.Text = ""
        If Not dtTRNH Is Nothing Then
            If dtTRNH.Rows.Count > 0 Then
                If dtTRNH.Rows(0).Item("ContFlag") = "Y" Then
                    lblLimit.Text = "More than 100 records returned, please change enquiry ""From/To Date"" to view other records."
                End If
            End If
        End If
    End Sub

End Class
