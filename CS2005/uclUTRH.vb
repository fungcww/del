Imports System.Data.SqlClient

Public Class uclUTRH
    Inherits System.Windows.Forms.UserControl

    Dim strPolicy, strProdID As String
    Dim dtUNITS, dtUNITSTX, dtFA As DataTable
    Dim datEnqFrom As Date
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
    Friend WithEvents txtFDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdGO As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdUnit As System.Windows.Forms.DataGrid
    Friend WithEvents grdUnitTx As System.Windows.Forms.DataGrid
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblLimit As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTtlBal As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDCAmt As System.Windows.Forms.TextBox
    Friend WithEvents grdFA As System.Windows.Forms.DataGrid
    Friend WithEvents Label7 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdUnit = New System.Windows.Forms.DataGrid
        Me.grdUnitTx = New System.Windows.Forms.DataGrid
        Me.txtFDate = New System.Windows.Forms.DateTimePicker
        Me.cmdGO = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblLimit = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtTtlBal = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDCAmt = New System.Windows.Forms.TextBox
        Me.grdFA = New System.Windows.Forms.DataGrid
        Me.Label7 = New System.Windows.Forms.Label
        CType(Me.grdUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUnitTx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdFA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdUnit
        '
        Me.grdUnit.AlternatingBackColor = System.Drawing.Color.White
        Me.grdUnit.BackColor = System.Drawing.Color.White
        Me.grdUnit.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdUnit.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUnit.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdUnit.CaptionVisible = False
        Me.grdUnit.DataMember = ""
        Me.grdUnit.FlatMode = True
        Me.grdUnit.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdUnit.ForeColor = System.Drawing.Color.Black
        Me.grdUnit.GridLineColor = System.Drawing.Color.Wheat
        Me.grdUnit.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdUnit.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdUnit.HeaderForeColor = System.Drawing.Color.Black
        Me.grdUnit.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUnit.Location = New System.Drawing.Point(4, 52)
        Me.grdUnit.Name = "grdUnit"
        Me.grdUnit.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdUnit.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdUnit.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdUnit.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUnit.Size = New System.Drawing.Size(500, 108)
        Me.grdUnit.TabIndex = 2
        '
        'grdUnitTx
        '
        Me.grdUnitTx.AlternatingBackColor = System.Drawing.Color.White
        Me.grdUnitTx.BackColor = System.Drawing.Color.White
        Me.grdUnitTx.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdUnitTx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdUnitTx.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUnitTx.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdUnitTx.CaptionVisible = False
        Me.grdUnitTx.DataMember = ""
        Me.grdUnitTx.FlatMode = True
        Me.grdUnitTx.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdUnitTx.ForeColor = System.Drawing.Color.Black
        Me.grdUnitTx.GridLineColor = System.Drawing.Color.Wheat
        Me.grdUnitTx.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdUnitTx.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdUnitTx.HeaderForeColor = System.Drawing.Color.Black
        Me.grdUnitTx.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUnitTx.Location = New System.Drawing.Point(4, 312)
        Me.grdUnitTx.Name = "grdUnitTx"
        Me.grdUnitTx.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdUnitTx.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdUnitTx.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdUnitTx.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUnitTx.Size = New System.Drawing.Size(708, 180)
        Me.grdUnitTx.TabIndex = 3
        '
        'txtFDate
        '
        Me.txtFDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFDate.Location = New System.Drawing.Point(104, 4)
        Me.txtFDate.Name = "txtFDate"
        Me.txtFDate.Size = New System.Drawing.Size(132, 20)
        Me.txtFDate.TabIndex = 82
        '
        'cmdGO
        '
        Me.cmdGO.Location = New System.Drawing.Point(240, 4)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(34, 20)
        Me.cmdGO.TabIndex = 81
        Me.cmdGO.Text = "Go"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Enquiry From Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(280, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "Status"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Location = New System.Drawing.Point(320, 4)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(36, 20)
        Me.txtStatus.TabIndex = 83
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 85
        Me.Label3.Text = "Total No. of Units"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 296)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Unit Transaction"
        '
        'lblLimit
        '
        Me.lblLimit.ForeColor = System.Drawing.Color.Blue
        Me.lblLimit.Location = New System.Drawing.Point(280, 28)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(396, 16)
        Me.lblLimit.TabIndex = 89
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(512, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 91
        Me.Label5.Text = "Total Balance:"
        '
        'txtTtlBal
        '
        Me.txtTtlBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlBal.Location = New System.Drawing.Point(600, 92)
        Me.txtTtlBal.Name = "txtTtlBal"
        Me.txtTtlBal.ReadOnly = True
        Me.txtTtlBal.Size = New System.Drawing.Size(108, 20)
        Me.txtTtlBal.TabIndex = 90
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(512, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 32)
        Me.Label6.TabIndex = 93
        Me.Label6.Text = "Death Coverage Amount"
        '
        'txtDCAmt
        '
        Me.txtDCAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtDCAmt.Location = New System.Drawing.Point(600, 120)
        Me.txtDCAmt.Name = "txtDCAmt"
        Me.txtDCAmt.ReadOnly = True
        Me.txtDCAmt.Size = New System.Drawing.Size(108, 20)
        Me.txtDCAmt.TabIndex = 92
        '
        'grdFA
        '
        Me.grdFA.AlternatingBackColor = System.Drawing.Color.White
        Me.grdFA.BackColor = System.Drawing.Color.White
        Me.grdFA.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdFA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdFA.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdFA.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdFA.CaptionVisible = False
        Me.grdFA.DataMember = ""
        Me.grdFA.FlatMode = True
        Me.grdFA.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdFA.ForeColor = System.Drawing.Color.Black
        Me.grdFA.GridLineColor = System.Drawing.Color.Wheat
        Me.grdFA.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdFA.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdFA.HeaderForeColor = System.Drawing.Color.Black
        Me.grdFA.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdFA.Location = New System.Drawing.Point(4, 184)
        Me.grdFA.Name = "grdFA"
        Me.grdFA.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdFA.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdFA.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdFA.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdFA.Size = New System.Drawing.Size(708, 104)
        Me.grdFA.TabIndex = 94
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(8, 168)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 95
        Me.Label7.Text = "Fund Allocation"
        '
        'uclUTRH
        '
        Me.Controls.Add(Me.grdUnitTx)
        Me.Controls.Add(Me.grdFA)
        Me.Controls.Add(Me.grdUnit)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtDCAmt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtTtlBal)
        Me.Controls.Add(Me.lblLimit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtFDate)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.Label1)
        Me.Name = "uclUTRH"
        Me.Size = New System.Drawing.Size(716, 496)
        CType(Me.grdUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUnitTx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdFA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property PolicyAccountID(ByVal EnqFrom As Date, ByVal ProdID As String) As String
        Set(ByVal Value As String)
            strProdID = ProdID
            strPolicy = Value
            datEnqFrom = EnqFrom
        End Set
    End Property

    Private Sub BuildUI_Unit()

        ' POFUND part
        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "FundCode"
        cs.HeaderText = "Fund Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ' ES01 begin
        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "FundName"
        cs.HeaderText = "Fund Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Currency"
        cs.HeaderText = "Currency"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)
        ' ES01 end

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Unit"
        cs.HeaderText = "Units"
        cs.NullText = gNULLText
        cs.Format = "#,##0.0000"
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "FundPrice"
        cs.HeaderText = "Fund Price"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 110
        cs.MappingName = "FundBalance"
        cs.HeaderText = "Fund Balance"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "ValuationDate"
        cs.HeaderText = "Valuation Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "ExchangeRate"
        cs.HeaderText = "Ex. Date"
        cs.NullText = gNULLText
        cs.Format = "#,##0.000000000000"
        ts.GridColumnStyles.Add(cs)

        ' Add 9/20/2006, 3 more fields for i.KnowU
        ' insert csw_mq_func_detail select 'UTRH',3,75,'AdminCharge',9, 'N'
        ' insert csw_mq_func_detail select 'UTRH',2,50,'ValuationDate',7, 'D'
        ' insert csw_mq_func_detail select 'UTRH',2,60,'ExchangeRate',14, 'N'

        ' Return by MQ now
        'cs = New DataGridTextBoxColumn
        'cs.Width = 80
        'cs.MappingName = "PriceDate"
        'cs.HeaderText = "Price Date"
        'cs.NullText = gNULLText
        'cs.Format = gDateFormat
        'ts.GridColumnStyles.Add(cs)

        ts.MappingName = "UNITS"
        grdUnit.TableStyles.Add(ts)

        grdUnit.DataSource = dtUNITS
        grdUnit.AllowDrop = False
        grdUnit.ReadOnly = True

    End Sub

    Private Sub BuildUI_UTx()
        ' UTRHST part
        Dim ts1 As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "FundCode"
        cs.HeaderText = "Fund Code"
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 40
        cs.MappingName = "Rider"
        cs.HeaderText = "Cov"
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        ' ES01 begin
        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PriceDate"
        cs.HeaderText = "Valuation Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "BuyPrice"
        cs.HeaderText = "Buy Price"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "SellPrice"
        cs.HeaderText = "Sell Price"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts1.GridColumnStyles.Add(cs)
        ' ES01 end

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "TxDate"
        cs.HeaderText = "Tran. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "TradeDate"
        cs.HeaderText = "Trade Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "AlloDate"
        cs.HeaderText = "Alloc. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Amount"
        cs.HeaderText = "Amount"
        cs.NullText = gNULLText
        cs.Format = "#,##0.00"
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "AdminCharge"
        cs.HeaderText = "Admin. Charge"
        cs.NullText = gNULLText
        cs.Format = "#,##0.00"
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Amount_ccy"
        cs.HeaderText = "Amount(CCY)"
        cs.NullText = gNULLText
        cs.Format = "#,##0.00"
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Units"
        cs.HeaderText = "Units"
        cs.NullText = gNULLText
        cs.Format = "#,##0.0000"
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PayMethod"
        cs.HeaderText = "Payment"
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "RecvFlag"
        cs.HeaderText = "Reverse"
        cs.NullText = gNULLText
        cs.Format = "#,##0.0000"
        ts1.GridColumnStyles.Add(cs)

        ts1.MappingName = "UNITSTX"
        grdUnitTx.TableStyles.Add(ts1)

        bm = Me.BindingContext(dtUNITSTX)

        grdUnitTx.DataSource = dtUNITSTX
        grdUnitTx.AllowDrop = False
        grdUnitTx.ReadOnly = True

    End Sub

    Private Sub BuildUI_FA()
        ' Fund Allocation
        Dim ts1 As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 40
        cs.MappingName = "cswcfa_coverage_no"
        cs.HeaderText = "Cov."
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "Description"
        cs.HeaderText = "Description"
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "cswcfa_eff_date"
        cs.HeaderText = "Eff. Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "cswcfa_fund_code"
        cs.HeaderText = "Fund Code"
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "mpfinv_chi_desc"
        cs.HeaderText = "Fund Name"
        cs.NullText = gNULLText
        ts1.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "cswcfa_allocation"
        cs.HeaderText = "Allocation%"
        cs.NullText = gNULLText
        cs.Format = "#,##0"
        ts1.GridColumnStyles.Add(cs)

        ts1.MappingName = "FA"
        grdFA.TableStyles.Add(ts1)

        grdFA.DataSource = dtFA
        grdFA.AllowDrop = False
        grdFA.ReadOnly = True

    End Sub

    Private Sub GetUTRHRecord()

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim blnBUI_U, blnBUI_Tx, blnBUI_FA As Boolean
        Dim dtUTRHtemp(1) As DataTable
        Dim dtTempU As New DataTable
        Dim dr As DataRow
        Dim ar() As Object

        'ATHL20210303 BAUL check
        'Dim bBAUL As Boolean = False

        wndMain.Cursor = Cursors.WaitCursor

        dtUTRHtemp = objCS.GetUTRH(strPolicy, txtFDate.Value, lngErrNo, strErrMsg)

        'ATHL20210303 BAUL check
        'For i As Integer = 0 To dtUTRHtemp(0).Rows.Count - 1
        '    If dtUTRHtemp(0).Rows(i)("FundCode").ToString.Trim.ToUpper.Equals("BAUL") Then
        '        bBAUL = True
        '    End If
        'Next

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'dtUTRHtemp = objCS.GetUTRH("5010242383", txtFDate.Value, lngErrNo, strErrMsg)
        'End If
        'AC - Change to use configuration setting - end

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If Not dtUTRHtemp Is Nothing Then

                ' Status
                If dtUTRHtemp(0).Rows.Count > 0 Then

                    txtStatus.Text = dtUTRHtemp(0).Rows(0).Item("Status")
                    txtTtlBal.Text = Format(dtUTRHtemp(0).Rows(0).Item("TotalBalance"), gNumFormat)

                    ' Add 7/11/2006 - Index Link Investment Plan
                    ' Add two row to csw_mq_func_detail table
                    ' insert csw_mq_func_detail select 'UTRH', 1, 50, 'DeathCovFlag', 1, 'C'
                    ' insert csw_mq_func_detail select 'UTRH', 1, 60, 'DeathCovAmt', 18, 'N'
                    txtDCAmt.Text = ""
                    Try
                        If dtUTRHtemp(0).Rows(0).Item("DeathCovFlag") = "Y" Then
                            txtDCAmt.Text = Format(dtUTRHtemp(0).Rows(0).Item("DeathCovAmt"), gNumFormat)
                        End If
                    Catch ex As Exception
                    End Try
                    ' End Add
                Else
                    MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "UTRH")
                End If

                ' POFUND part
                'If dtUTRHtemp(1).Rows.Count > 0 Then

                If dtUNITS Is Nothing Then
                    blnBUI_U = True
                    dtUNITS = New DataTable
                    dtUNITS = dtUTRHtemp(1).Clone
                    dtUNITS.TableName = "UNITS"
                End If

                dtUNITS.Rows.Clear()

                For Each dr In dtUTRHtemp(1).Rows
                    ar = dr.ItemArray
                    dtUNITS.Rows.Add(ar)
                Next

                Dim dtPriceDate As DataTable
                Dim strSQL As String

                ' ES01 begin
                'strSQL = "Select mpfval_invest_fund, max(mpfval_to_date) as PriceDate " & _
                '    " from cswvw_mpf_valuation group by mpfval_invest_fund"
                strSQL = "Select * from cswvw_mpf_investment"
                dtPriceDate = objCS.ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)

                If lngErrNo = 0 Then
                    If blnBUI_U Then
                        'dtUNITS.Columns.Add("PriceDate", Type.GetType("System.DateTime"))
                        dtUNITS.Columns.Add("FundName", Type.GetType("System.String"))
                        dtUNITS.Columns.Add("Currency", Type.GetType("System.String"))
                    End If

                    For Each dr In dtUNITS.Rows
                        dtPriceDate.DefaultView.RowFilter = "mpfinv_code = '" & dr.Item("FundCode") & "'"
                        If dtPriceDate.DefaultView.Count > 0 Then
                            dr.Item("FundName") = dtPriceDate.DefaultView.Item(0).Item("mpfinv_chi_desc")
                            dr.Item("Currency") = dtPriceDate.DefaultView.Item(0).Item("mpfinv_curr")
                            'dr.Item("Currency") = "xx"
                        End If
                    Next
                    dtUNITS.AcceptChanges()
                    If blnBUI_U Then Call BuildUI_Unit()
                Else
                    MsgBox("Error getting the latest price date." & vbCrLf & strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "UTRH")
                End If
                ' ES01 end

                'strSQL = "Select mpfval_invest_fund, max(mpfval_to_date) as PriceDate " & _
                '    " from cswvw_mpf_valuation group by mpfval_invest_fund"
                'dtPriceDate = objCS.ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)

                ''If lngErrNo = 0 Then
                ''    If blnBUI_U Then dtUNITS.Columns.Add("PriceDate", Type.GetType("System.DateTime"))
                ''    'dtUNITS.Columns.Add("PriceDate", Type.GetType("System.DateTime"))
                ''    For Each dr In dtUNITS.Rows
                ''        dtPriceDate.DefaultView.RowFilter = "mpfval_invest_fund = '" & dr.Item("FundCode") & "'"
                ''        If dtPriceDate.DefaultView.Count > 0 Then
                ''            dr.Item("PriceDate") = dtPriceDate.DefaultView.Item(0).Item("PriceDate")
                ''        End If
                ''    Next
                ''    dtUNITS.AcceptChanges()
                ''If blnBUI_U Then Call BuildUI_Unit()
                ''Else
                ''    MsgBox("Error getting the latest price date." & vbCrLf & strErrMsg, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "UTRH")
                ''End If

                'Else
                '    MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "UTRH")
                'End If

                ' UTRHST part
                'If dtUTRHtemp(2).Rows.Count > 0 Then
                If dtUNITSTX Is Nothing Then
                    blnBUI_Tx = True
                    dtUNITSTX = New DataTable
                    dtUNITSTX = dtUTRHtemp(2).Clone
                    dtUNITSTX.TableName = "UNITSTX"
                End If

                dtUNITSTX.Rows.Clear()

                For Each dr In dtUTRHtemp(2).Rows
                    ar = dr.ItemArray
                    dtUNITSTX.Rows.Add(ar)
                Next

                ' ES01 begin
                If blnBUI_Tx Then
                    dtUNITSTX.Columns.Add("PriceDate", Type.GetType("System.DateTime"))
                    dtUNITSTX.Columns.Add("BuyPrice", Type.GetType("System.Decimal"))
                    dtUNITSTX.Columns.Add("SellPrice", Type.GetType("System.Decimal"))
                    dtUNITSTX.DefaultView.Sort = "PriceDate DESC, FundCode"
                End If

                For Each dr In dtUNITSTX.Rows

                    ' Daily trade
                    If strProdID Like "?JFS*" Or strProdID Like "?IW*" Or strProdID Like "?RJF*" Then
                        strSQL = "Select * from cswvw_mpf_valuation " & _
                            " Where mpfval_invest_fund = '" & dr.Item("FundCode") & "'" & _
                            " And mpfval_to_date > '" & dr.Item("TradeDate") & "'"
                    Else
                        strSQL = "Select * from cswvw_mpf_valuation " & _
                            " Where mpfval_invest_fund = '" & dr.Item("FundCode") & "'" & _
                            " And mpfval_to_date >= '" & dr.Item("TradeDate") & "'"
                    End If

                    dtPriceDate = objCS.ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)

                    If dtPriceDate.Rows.Count > 0 Then
                        dr.Item("PriceDate") = dtPriceDate.Rows(0).Item("mpfval_to_date")
                        dr.Item("BuyPrice") = dtPriceDate.Rows(0).Item("mpfval_unit_price_buy")
                        dr.Item("SellPrice") = dtPriceDate.Rows(0).Item("mpfval_unit_price")
                    End If
                Next
                dtUNITSTX.AcceptChanges()

                If blnBUI_Tx Then Call BuildUI_UTx()
                ' ES01 end

                Call Me.CheckLimit()

                'Else
                '    MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "UTRH")
                'End If

            Else
                ' Try to find UTRH records
                If GetUnitTx(strPolicy, dtTempU, strErrMsg) = False Then
                    MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Else

                    If dtTempU Is Nothing AndAlso dtTempU.Rows.Count = 0 Then
                        MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "UTRH")
                    Else
                        If dtUNITSTX Is Nothing Then
                            blnBUI_Tx = True
                            dtUNITSTX = New DataTable
                            dtUNITSTX = dtTempU.Clone
                            dtUNITSTX.TableName = "UNITSTX"
                        End If

                        For Each dr In dtTempU.Rows
                            ar = dr.ItemArray
                            dtUNITSTX.Rows.Add(ar)
                        Next

                        If blnBUI_Tx Then Call BuildUI_UTx()

                    End If
                End If

                'MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "UTRH")

            End If
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

        ' Get Fund Allocation
        Dim dtTemp As New DataTable

        If GetFundAllocation(strPolicy, Today, dtTemp, strErrMsg) Then

            If dtFA Is Nothing Then
                blnBUI_FA = True
                dtFA = New DataTable
                dtFA = dtTemp.Clone
                dtFA.TableName = "FA"
            End If

            dtFA.Rows.Clear()

            For Each dr In dtTemp.Rows
                ar = dr.ItemArray
                dtFA.Rows.Add(ar)
            Next
            If blnBUI_FA Then Call Me.BuildUI_FA()

        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

        'ATHL20210303 BAUL check
        'For i As Integer = 0 To dtUNITSTX.Rows.Count - 1
        '    If dtUNITSTX.Rows(i)("FundCode").ToString.Trim.ToUpper.Equals("BAUL") Then
        '        bBAUL = True
        '    End If
        'Next
        'For i As Integer = 0 To dtFA.Rows.Count - 1
        '    If dtFA.Rows(i)("cswcfa_fund_code").ToString.Trim.ToUpper.Equals("BAUL") Then
        '        bBAUL = True
        '    End If
        'Next
        'If bBAUL Then
        '    MessageBox.Show("Due to system constrain cannot show 5 digits of unit fund price. Fund BAUL ""Barings Umbrella Fund plc Barings USD Liquidity Fund - Tranche G USD Acc"" 霸菱傘子基金公眾有限公司霸菱美元流動基金-G 類別美元累積the Fund unit display in 10 times than original fund unit, Fund price display in 1/10 of original fund price in CRS & eService system, it will resume once system constrain fixed.")
        'End If
    End Sub

    Private Sub grdUnit_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdUnit.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdUnit.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdUnit.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdUnit.Select(hti.Row)
        End If
    End Sub

    Private Sub grdUnitTx_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdUnitTx.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdUnitTx.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdUnitTx.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdUnitTx.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click
        Call GetUTRHRecord()
        'Label3.Text = "Total No. of Units (Unit price as of " & Format(GetLatestPriceDate(), gDateFormat) & ")"
    End Sub

    Private Sub uclUTRH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'AC - Change to use configuration setting - start
        '#If UAT = 0 Then
        'If UAT <> 0 Then
        '    txtFDate.Value = Today
        'Else
        '    txtFDate.Value = datEnqFrom
        'End If
        '#End If
        If Me.DesignMode Then
            Return
        End If
        txtFDate.Value = datEnqFrom
        'AC - Change to use configuration setting - end


    End Sub

    Private Sub grdUnitTx_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUnitTx.CurrentCellChanged
        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            txtFDate.Text = drI.Item("TxDate")
        End If

    End Sub

    Public Sub CheckLimit()
        lblLimit.Text = ""
        If Not dtUNITSTX Is Nothing Then
            If dtUNITSTX.Rows.Count > 0 Then
                If Not IsDBNull(dtUNITSTX.Rows(0).Item("ContFlag")) AndAlso dtUNITSTX.Rows(0).Item("ContFlag") = "Y" Then
                    lblLimit.Text = "More than 100 records returned, please change enquiry ""From Date"" to view later records."
                End If
            End If
        End If
    End Sub

    Private Sub grdFA_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdFA.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdFA.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdFA.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdFA.Select(hti.Row)
        End If
    End Sub

End Class
