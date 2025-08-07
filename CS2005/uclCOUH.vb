Imports System.Data.SqlClient

Public Class COUH
    Inherits System.Windows.Forms.UserControl

    Dim objDS As DataSet = New DataSet("CouponHistory")
    Dim dr, dr1 As DataRow
    Dim da As SqlDataAdapter
    Dim sqlConnect As New SqlConnection
    Dim datFrom As Date
    Dim strPolicy As String
    Dim dt, dtPolicy As DataTable
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAPLoan As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtInt As System.Windows.Forms.TextBox
    Friend WithEvents txtDep As System.Windows.Forms.TextBox
    Friend WithEvents txtOption As System.Windows.Forms.TextBox
    Friend WithEvents txtOperator As System.Windows.Forms.TextBox
    Friend WithEvents txtAction As System.Windows.Forms.TextBox
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents grdCoupon As System.Windows.Forms.DataGrid
    Friend WithEvents txtIntCap As System.Windows.Forms.TextBox
    Friend WithEvents txtErrDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdCoupon = New System.Windows.Forms.DataGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtAPLoan = New System.Windows.Forms.TextBox
        Me.txtIntCap = New System.Windows.Forms.TextBox
        Me.txtTotal = New System.Windows.Forms.TextBox
        Me.txtInt = New System.Windows.Forms.TextBox
        Me.txtDep = New System.Windows.Forms.TextBox
        Me.txtOption = New System.Windows.Forms.TextBox
        Me.txtOperator = New System.Windows.Forms.TextBox
        Me.txtAction = New System.Windows.Forms.TextBox
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.txtErrDesc = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        CType(Me.grdCoupon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdCoupon
        '
        Me.grdCoupon.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCoupon.BackColor = System.Drawing.Color.White
        Me.grdCoupon.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCoupon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCoupon.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCoupon.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCoupon.CaptionVisible = False
        Me.grdCoupon.DataMember = ""
        Me.grdCoupon.FlatMode = True
        Me.grdCoupon.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCoupon.ForeColor = System.Drawing.Color.Black
        Me.grdCoupon.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCoupon.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCoupon.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCoupon.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCoupon.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCoupon.Location = New System.Drawing.Point(4, 4)
        Me.grdCoupon.Name = "grdCoupon"
        Me.grdCoupon.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCoupon.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCoupon.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCoupon.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCoupon.Size = New System.Drawing.Size(668, 240)
        Me.grdCoupon.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 260)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(156, 260)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Action"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(276, 260)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Operator"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(416, 260)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Coupon Option"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(236, 288)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 16)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Interest Rate"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 288)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 16)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Deposit / Withdrawal"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(432, 288)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 16)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Coupon Declared"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 316)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Interest Capital"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(212, 316)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 16)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "APL / Loan"
        '
        'txtAPLoan
        '
        Me.txtAPLoan.BackColor = System.Drawing.SystemColors.Window
        Me.txtAPLoan.Location = New System.Drawing.Point(280, 312)
        Me.txtAPLoan.Name = "txtAPLoan"
        Me.txtAPLoan.ReadOnly = True
        Me.txtAPLoan.TabIndex = 10
        Me.txtAPLoan.Text = ""
        '
        'txtIntCap
        '
        Me.txtIntCap.BackColor = System.Drawing.SystemColors.Window
        Me.txtIntCap.Location = New System.Drawing.Point(96, 312)
        Me.txtIntCap.Name = "txtIntCap"
        Me.txtIntCap.ReadOnly = True
        Me.txtIntCap.TabIndex = 11
        Me.txtIntCap.Text = ""
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Location = New System.Drawing.Point(532, 284)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.TabIndex = 12
        Me.txtTotal.Text = ""
        '
        'txtInt
        '
        Me.txtInt.BackColor = System.Drawing.SystemColors.Window
        Me.txtInt.Location = New System.Drawing.Point(316, 284)
        Me.txtInt.Name = "txtInt"
        Me.txtInt.ReadOnly = True
        Me.txtInt.TabIndex = 13
        Me.txtInt.Text = ""
        '
        'txtDep
        '
        Me.txtDep.BackColor = System.Drawing.SystemColors.Window
        Me.txtDep.Location = New System.Drawing.Point(124, 284)
        Me.txtDep.Name = "txtDep"
        Me.txtDep.ReadOnly = True
        Me.txtDep.TabIndex = 14
        Me.txtDep.Text = ""
        '
        'txtOption
        '
        Me.txtOption.BackColor = System.Drawing.SystemColors.Window
        Me.txtOption.Location = New System.Drawing.Point(504, 256)
        Me.txtOption.Name = "txtOption"
        Me.txtOption.ReadOnly = True
        Me.txtOption.TabIndex = 15
        Me.txtOption.Text = ""
        '
        'txtOperator
        '
        Me.txtOperator.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperator.Location = New System.Drawing.Point(336, 256)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.ReadOnly = True
        Me.txtOperator.Size = New System.Drawing.Size(64, 20)
        Me.txtOperator.TabIndex = 16
        Me.txtOperator.Text = ""
        '
        'txtAction
        '
        Me.txtAction.BackColor = System.Drawing.SystemColors.Window
        Me.txtAction.Location = New System.Drawing.Point(200, 256)
        Me.txtAction.Name = "txtAction"
        Me.txtAction.ReadOnly = True
        Me.txtAction.Size = New System.Drawing.Size(60, 20)
        Me.txtAction.TabIndex = 17
        Me.txtAction.Text = ""
        '
        'txtDate
        '
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.Location = New System.Drawing.Point(44, 256)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.TabIndex = 18
        Me.txtDate.Text = ""
        '
        'txtErrDesc
        '
        Me.txtErrDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtErrDesc.Location = New System.Drawing.Point(96, 340)
        Me.txtErrDesc.Name = "txtErrDesc"
        Me.txtErrDesc.ReadOnly = True
        Me.txtErrDesc.Size = New System.Drawing.Size(540, 20)
        Me.txtErrDesc.TabIndex = 20
        Me.txtErrDesc.Text = ""
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 344)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 16)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Error Description"
        '
        'COUH
        '
        Me.Controls.Add(Me.txtErrDesc)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtAction)
        Me.Controls.Add(Me.txtOperator)
        Me.Controls.Add(Me.txtOption)
        Me.Controls.Add(Me.txtDep)
        Me.Controls.Add(Me.txtInt)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtIntCap)
        Me.Controls.Add(Me.txtAPLoan)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdCoupon)
        Me.Name = "COUH"
        Me.Size = New System.Drawing.Size(720, 390)
        CType(Me.grdCoupon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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

    Public Property DateFrom()
        Get
            DateFrom = datFrom
        End Get
        Set(ByVal Value)
            datFrom = Value
        End Set
    End Property

    Public WriteOnly Property srcDT()
        Set(ByVal Value)
            dt = Value
            Call BuildUI()
        End Set
    End Property

    Private Sub BuildUI()

        Dim strSQL As String
        Dim strErrNo As Integer
        Dim strErrMsg As String

        'Try
        '    strSQL = "Select PaymentTypeCode, PaymentTypeDesc from PaymentTypeCodes; "
        '    strSQL &= "Select BillingTypeCode, BillingTypeDesc from BillingTypeCodes; "
        '    strSQL &= "Select ModeCode, ModeDesc from ModeCodes"
        '    sqlConnect.ConnectionString = objUtl.ConnStr("CSW", "CS2005", "CIW")
        '    sqlConnect.Open()
        '    da = New SqlDataAdapter(strSQL, sqlConnect)
        '    da.MissingSchemaAction = MissingSchemaAction.Add
        '    da.MissingMappingAction = MissingMappingAction.Passthrough
        '    da.TableMappings.Add("PaymentTypeCodes1", "BillingTypeCodes")
        '    da.TableMappings.Add("PaymentTypeCodes2", "ModeCodes")
        '    da.Fill(objDS, "PaymentTypeCodes")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        If Not dt Is Nothing Then objDS.Tables.Add(dt)
        If Not dtPolicy Is Nothing Then objDS.Tables.Add(dtPolicy)

        'Dim objDataRel As New Data.DataRelation("Payment_Type", objDS.Tables("PaymentTypeCodes").Columns("PaymentTypeCode"), _
        '    objDS.Tables("PAYH").Columns("PaymentType"), True)
        'Dim objBillType As New Data.DataRelation("BillType", objDS.Tables("BillingTypeCodes").Columns("BillingTypeCode"), _
        '    objDS.Tables("Product").Columns("BillingType"), True)
        'Dim objMode As New Data.DataRelation("Mode", objDS.Tables("ModeCodes").Columns("ModeCode"), _
        '    objDS.Tables("Product").Columns("Mode"), True)

        'Try
        '    objDS.Relations.Add(objDataRel)
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        'Try
        '    objDS.Relations.Add(objBillType)
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        'Try
        '    objDS.Relations.Add(objMode)
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        'sqlConnect.Close()

        'DataGrid1.DataSource = objDS.Tables("PAYH")

        'With objDS.Tables("COUH")
        '    .Columns.Add("PaymentTypeDesc", GetType(String))
        '    .Columns.Add("Unsuccess", GetType(String))
        'End With

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "ActiveDate"
        cs.HeaderText = "Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "Action"
        cs.HeaderText = "Action"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 75
        cs.MappingName = "Operator"
        cs.HeaderText = "Operator"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "CouponOption"
        cs.HeaderText = "Coupon Option"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "CouponDeclared"
        cs.HeaderText = "Coupon Declared"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "InterestRate"
        cs.HeaderText = "Interest Rate"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 180
        cs.MappingName = "TotalDepositWithdrawal"
        cs.HeaderText = "Total Deposit/Withdrawal"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "InterestCapital"
        cs.HeaderText = "Interest Capital"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 120
        cs.MappingName = "TotalLoanandAPL"
        cs.HeaderText = "Total Loan & APL"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "COUH"
        grdCoupon.TableStyles.Add(ts)

        objDS.Tables("COUH").DefaultView.Sort = "ActiveDate DESC, Action DESC"
        grdCoupon.DataSource = objDS.Tables("COUH")
        grdCoupon.AllowDrop = False
        grdCoupon.ReadOnly = True
        bm = Me.BindingContext(objDS.Tables("COUH"))

        'txtDate.DataBindings.Add("text", objDS.Tables("COUH"), "Mode")
        Dim bDate As Binding = New Binding("text", objDS.Tables("COUH"), "ActiveDate")
        Me.txtDate.DataBindings.Add(bDate)
        AddHandler bDate.Format, AddressOf FormatDate

        txtAction.DataBindings.Add("text", objDS.Tables("COUH"), "Action")
        txtOperator.DataBindings.Add("text", objDS.Tables("COUH"), "Operator")
        txtOption.DataBindings.Add("text", objDS.Tables("COUH"), "CouponOption")

        ' Add IE20 new field
        If blnIE20 Then
            Me.txtErrDesc.DataBindings.Add("text", objDS.Tables("COUH"), "ErrDesc")
        End If
        ' End Add

        Call UpdatePT()

    End Sub

    Private Sub UpdatePT()

        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            txtDep.Text = Format(drI.Item("TotalDepositWithdrawal"), gNumFormat)
            txtInt.Text = Format(drI.Item("InterestRate"), gNumFormat)
            txtTotal.Text = Format(drI.Item("CouponDeclared"), gNumFormat)
            txtIntCap.Text = Format(drI.Item("InterestCapital"), gNumFormat)
            txtAPLoan.Text = Format(drI.Item("TotalLoanandAPL"), gNumFormat)
        End If

    End Sub

    Private Sub grdCoupon_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCoupon.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdCoupon.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdCoupon.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdCoupon.Select(hti.Row)
        End If
    End Sub

    Private Sub grdCoupon_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdCoupon.CurrentCellChanged
        Call UpdatePT()
    End Sub

End Class
