Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmPolicyProj
    Inherits System.Windows.Forms.Form

    Dim dtProjection As New DataTable("Projection")
    Dim dtRptTable As DataTable
    Private lngErr As Long = 0
    Private strErr As String = ""
    Private strPolicyNo As String

    Dim strProductID, strSumIns, strDIVOpt, strCouOpt As String
    Dim strScrType, intyear As Integer
    Dim strSubTitle, strChiProductName, strLang As String
    Dim blnPrintRpt As Boolean = False
    Dim blnPolFound As Boolean = False
    Dim blnEnd As Boolean

    'report
    Private rpt As ReportDocument
    ' oliver updated 2023-12-15 for Switch Over Code from Assurance to Bermuda 
    'Private objRptLogic As New clsReportLogic
    Private objRptLogic

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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grdProj As System.Windows.Forms.DataGrid
    Friend WithEvents txtPolicyNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents txtPolicyTitle As System.Windows.Forms.TextBox
    Friend WithEvents cmdPrev As System.Windows.Forms.Button
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdProj = New System.Windows.Forms.DataGrid
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.txtPolicyTitle = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdPrev = New System.Windows.Forms.Button
        Me.cmdNext = New System.Windows.Forms.Button
        CType(Me.grdProj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicyNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPolicyNo.Location = New System.Drawing.Point(68, 8)
        Me.txtPolicyNo.MaxLength = 16
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(112, 20)
        Me.txtPolicyNo.TabIndex = 0
        Me.txtPolicyNo.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Policy No."
        '
        'grdProj
        '
        Me.grdProj.AlternatingBackColor = System.Drawing.Color.White
        Me.grdProj.BackColor = System.Drawing.Color.White
        Me.grdProj.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdProj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdProj.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdProj.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdProj.CaptionVisible = False
        Me.grdProj.DataMember = ""
        Me.grdProj.FlatMode = True
        Me.grdProj.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdProj.ForeColor = System.Drawing.Color.Black
        Me.grdProj.GridLineColor = System.Drawing.Color.Wheat
        Me.grdProj.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdProj.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdProj.HeaderForeColor = System.Drawing.Color.Black
        Me.grdProj.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdProj.Location = New System.Drawing.Point(8, 128)
        Me.grdProj.Name = "grdProj"
        Me.grdProj.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdProj.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdProj.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdProj.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdProj.Size = New System.Drawing.Size(716, 232)
        Me.grdProj.TabIndex = 18
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(188, 8)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(75, 20)
        Me.cmdRefresh.TabIndex = 20
        Me.cmdRefresh.Text = "Refresh"
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(268, 8)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(75, 20)
        Me.cmdPrint.TabIndex = 21
        Me.cmdPrint.Text = "Print"
        '
        'txtPolicyTitle
        '
        Me.txtPolicyTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicyTitle.Location = New System.Drawing.Point(68, 32)
        Me.txtPolicyTitle.Name = "txtPolicyTitle"
        Me.txtPolicyTitle.Size = New System.Drawing.Size(344, 20)
        Me.txtPolicyTitle.TabIndex = 22
        Me.txtPolicyTitle.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(269, 16)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Notes:   1. Values below are for the basic policy only."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(603, 16)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "2. Values are based on the current dividend scale and accumulation rate that are " & _
        "not guaranteed and subject to change."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(48, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(343, 16)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "3. Values are based on assumption that premiums are paid to date. "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(48, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(219, 16)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "4. All figures shown are for illustration only."
        '
        'cmdPrev
        '
        Me.cmdPrev.Location = New System.Drawing.Point(8, 364)
        Me.cmdPrev.Name = "cmdPrev"
        Me.cmdPrev.Size = New System.Drawing.Size(96, 28)
        Me.cmdPrev.TabIndex = 27
        Me.cmdPrev.Text = "<< Prev. 10 Yrs"
        '
        'cmdNext
        '
        Me.cmdNext.Location = New System.Drawing.Point(628, 364)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(96, 28)
        Me.cmdNext.TabIndex = 28
        Me.cmdNext.Text = "Next 10 Yrs >>"
        '
        'frmPolicyProj
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(740, 401)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdPrev)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPolicyTitle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPolicyNo)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.grdProj)
        Me.Name = "frmPolicyProj"
        Me.Text = "Policy Value Projection"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdProj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property PolicyNo() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal Value As String)
            strPolicyNo = Value
        End Set
    End Property


    Private Sub grdProj_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdProj.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdProj.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdProj.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdProj.Select(hti.Row)
        End If
    End Sub

    Private Sub frmPolicyProj_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'objCS.Env = giEnv
        'End If
        'AC - Change to use configuration setting - end

        wndMain.StatusBarPanel1.Text = ""

        'oliver 2023-12-4 added for Switch Over Code from Assurance to Bermuda 
        If IsAssurance Then
            objRptLogic = New clsReportLogic_Asur
        Else
            objRptLogic = New clsReportLogic
        End If

    End Sub

    Private Sub init()
        strCouOpt = ""
        strDIVOpt = ""
        strProductID = ""
        strSumIns = ""
        PolicyNo = ""
        strSubTitle = ""
        strScrType = 0
        blnPolFound = False
        blnPrintRpt = False
        grdProj.TableStyles.Clear()
        grdProj.DataSource = Nothing
        dtProjection = Nothing
        lngErr = 0
        strErr = ""

        Me.txtPolicyTitle.Text = ""
        Me.cmdPrev.Enabled = True
        Me.cmdNext.Enabled = True
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        intyear = 10

        Me.Cursor = Cursors.WaitCursor
        init()
        getInfo()
        If blnPolFound Then
            getProjection()

            If blnEnd Then
                MsgBox("No Projection record found.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
            End If
        Else
            MsgBox("No policy projection is available for this policy.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub getInfo()
        Dim dtPolSum As New DataTable
        Dim dtCov() As DataTable

        'Get the basic information of the corresponding policy
        If Not Me.txtPolicyNo.Text = "" Then
            PolicyNo = Me.txtPolicyNo.Text

            wndMain.Cursor = Cursors.WaitCursor
            dtPolSum = objCS.GetPolicySummary(PolicyNo, lngErr, strErr)
            wndMain.Cursor = Cursors.Default

            If lngErr <> 0 Then
                MsgBox(strErr, MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
                Exit Sub
            End If

            If Not dtPolSum Is Nothing Then
                If dtPolSum.Rows.Count > 0 Then

                    If dtPolSum.Rows(0).Item("AccountStatusCode") = "V" Then
                        MsgBox("Policy already in VPO state, no policy projection will be provided.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
                        Exit Sub
                    End If

                    strCouOpt = dtPolSum.Rows(0).Item("CouponOption")
                    strDIVOpt = dtPolSum.Rows(0).Item("DividendOption")

                    wndMain.Cursor = Cursors.WaitCursor
                    dtCov = objCS.GetCoverage(PolicyNo, lngErr, strErr)
                    wndMain.Cursor = Cursors.Default

                    If lngErr <> 0 Then
                        MsgBox(strErr, MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
                        Exit Sub
                    End If

                    If Not dtCov Is Nothing Then
                        If dtCov(0).Rows.Count > 0 Then

                            Dim drTemp() As DataRow
                            drTemp = dtCov(0).Select("RiderNumber = 1")
                            If drTemp.Length > 0 Then
                                For Each row As DataRow In drTemp
                                    strProductID = row.Item("ProductID")
                                    strSumIns = row.Item("SumInsured")
                                Next
                            End If
                        End If
                    End If

                End If
            End If

            'MsgBox("coupon value = " & strCouOpt)
            'MsgBox("dividend value = " & strDIVOpt)
            'MsgBox("product id = " & strProductID)
            'MsgBox("sum insured = " & strSumIns)

            If strProductID = "" Then
                MsgBox("Record not found.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
                blnPolFound = False

                'Project Leo G2M2 - provide for Loving Kids product
                'ElseIf strProductID Like "?EWL *" Or strProductID Like "?AL  *" Or strProductID Like "?AL20*" Or strProductID Like "?ASU *" Or strProductID Like "?AS  *" Or strProductID Like "?I368*" Or strProductID Like "?I152*" Or strProductID Like "?ASM *" Or strProductID Like "?AEL *" Or strProductID Like "?ASEN*" Or strProductID Like "?WL60*" Or strProductID Like "?WL65*" Or strProductID Like "?WL85*" Then
            ElseIf strProductID Like "?EWL *" Or strProductID Like "?AL  *" Or strProductID Like "?AL20*" Or strProductID Like "?ASU *" Or strProductID Like "?AS  *" Or strProductID Like "?I368*" Or strProductID Like "?I152*" Or strProductID Like "?ASM *" Or strProductID Like "?AEL *" Or strProductID Like "?ASEN*" Or strProductID Like "?WL60*" Or strProductID Like "?WL65*" Or strProductID Like "?WL85*" Or strProductID Like "?KEF *" Then
                blnPolFound = True
                setChiProductName(strProductID)

                'define the policy projection screen type
                defineScreenType()
            End If
        Else
            MsgBox("Please enter a Policy No.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
            Me.txtPolicyNo.Focus()
            blnPolFound = False
        End If

        dtPolSum = Nothing
        dtCov = Nothing
    End Sub

    Private Sub setChiProductName(ByVal ProdID As String)
        If ProdID Like "?EWL *" Then
            strChiProductName = "眃关繧璸购"
        ElseIf ProdID Like "?AL  *" Then
            strChiProductName = "﹜眃关繧璸购"
        ElseIf ProdID Like "?AL20*" Then
            strChiProductName = "﹜眃20关繧璸购"
        ElseIf ProdID Like "?ASU *" Then
            strChiProductName = "﹜碔腳关繧璸购"
        ElseIf ProdID Like "?AS  *" Then
            strChiProductName = "﹜眃腳关繧璸购"
        ElseIf ProdID Like "?I368*" Then
            strChiProductName = "i.368 关繧璸购"
        ElseIf ProdID Like "?I152*" Then
            strChiProductName = "i.15/20 关繧璸购"
        ElseIf ProdID Like "?ASM *" Then
            strChiProductName = "玊关繧璸购"
        ElseIf ProdID Like "?AEL *" Then
            strChiProductName = "眃┦关繧璸购"
        ElseIf ProdID Like "?ASEN*" Then
            strChiProductName = "贾关繧璸购"
        ElseIf ProdID Like "?KEF *" Then
            strChiProductName = "克贾毙▅膀璸购"
        ElseIf ProdID Like "?WL60*" Or ProdID Like "?WL65*" Or ProdID Like "?WL85*" Then
            strChiProductName = "CONVERTIBLE WHOLE OF LIFE (WITH BONUS)"
        End If
    End Sub

    Private Sub defineScreenType()
        'Set the Screen Type base on the Dividend and Coupon Option
        Select Case strDIVOpt
            Case "1"
                If strCouOpt = "1" Then
                    strScrType = 8
                    strSubTitle = " (Cash Dividend and Cash Coupon Projection) "
                ElseIf strCouOpt = "2" Then
                    strScrType = 7
                    strSubTitle = " (Cash Dividend and Coupon on Deposit Projection) "
                Else
                    strScrType = 2
                    strSubTitle = " (Cash Dividend Projection) "
                End If
            Case "3"
                If strCouOpt = "1" Then
                    strScrType = 10
                    strSubTitle = " (PUA with Cash Coupon Projection) "
                ElseIf strCouOpt = "2" Then
                    strScrType = 6
                    strSubTitle = " (PUA with Coupon on Deposit Projection) "
                Else
                    strScrType = 4
                    strSubTitle = " (PUA Projection) "
                End If
            Case "4"
                If strCouOpt = "1" Then
                    strScrType = 9
                    strSubTitle = " (Dividend on Deposit and Cash Coupon Projection) "
                ElseIf strCouOpt = "2" Then
                    strScrType = 5
                    strSubTitle = " (Dividend and Coupon on Deposit Projection) "
                Else
                    strScrType = 3
                    strSubTitle = " (Dividend on Deposit Projection) "
                End If
            Case "R"
                If strCouOpt <> "1" And strCouOpt <> "2" Then
                    strScrType = 1
                    strSubTitle = " (RB Projection) "
                End If
        End Select

        Me.txtPolicyTitle.Text = "Policy Projection " & strSubTitle

    End Sub

    Private Sub getProjection()

        Dim intCurYear As Integer

        If blnPrintRpt Then
            intCurYear = 10
        Else
            intCurYear = intyear
        End If

        lngErr = 0
        'Get Projection
        wndMain.Cursor = Cursors.WaitCursor
        dtProjection = objCS.GetPoProjection(PolicyNo.PadRight(10, " "), intCurYear, lngErr, strErr)
        wndMain.Cursor = Cursors.Default

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
            Exit Sub
        End If

        Me.cmdNext.Enabled = True
        Me.cmdPrev.Enabled = True

        If intyear <= 10 Then
            Me.cmdPrev.Enabled = False
        End If

        If Not dtProjection Is Nothing Then
            If dtProjection.Rows.Count > 0 Then
                'set the DataGrid
                setGridLayout()

                'set the report recordset
                If blnPrintRpt Then
                    setRptTable()
                End If
            Else
                MsgBox("No Projection record found.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
            End If
        End If
    End Sub

    Private Sub setGridLayout()
        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn
        Dim Result As String
        Dim dtShow As New DataTable("Show")

        dtShow.Clear()
        grdProj.TableStyles.Clear()
        grdProj.DataSource = Nothing
        Result = ""
        blnEnd = False

        Select Case strScrType
            Case 1
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("AccPrem", Type.GetType("System.Decimal"))
                    .Add("BSumIns", Type.GetType("System.Decimal"))
                    .Add("RBAmt", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("RBCashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                End With

                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow()
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("AccPrem") = FormatNumber(Mid(Result, 6, 11) / 100, 0)
                        .Item("BSumIns") = FormatNumber(strSumIns, 0)
                        .Item("RBAmt") = FormatNumber(Mid(Result, 116, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("RBCashVal") = FormatNumber(Mid(Result, 127, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)

                    '1st column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 50
                    cs.MappingName = "Year"
                    cs.HeaderText = "Year"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '2nd column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "AccPrem"
                    cs.HeaderText = "Accumulated Premium"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '3rd column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "BSumIns"
                    cs.HeaderText = "Basic Sum Insured"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '4th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "RBAmt"
                    cs.HeaderText = "RB Amount"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '5th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "TtlCov"
                    cs.HeaderText = "Total Coverage"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '6th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "GCashVal"
                    cs.HeaderText = "Guaranteed Cash Value"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '7th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "RBCashVal"
                    cs.HeaderText = "RB Cash Value"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '8th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "TtlCashVal"
                    cs.HeaderText = "Total Cash Value"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    ts.MappingName = "Show"
                    grdProj.TableStyles.Add(ts)
                    grdProj.DataSource = dtShow

                    grdProj.AllowDrop = False
                    grdProj.ReadOnly = True
                Next



            Case 2
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("AccPrem", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("CashDiv", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                End With

                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("AccPrem") = FormatNumber(Mid(Result, 6, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("CashDiv") = FormatNumber(Mid(Result, 39, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)

                    '1st column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 50
                    cs.MappingName = "Year"
                    cs.HeaderText = "Year"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '2nd column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "AccPrem"
                    cs.HeaderText = "Accumulated Premium"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '3rd column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "TtlCov"
                    cs.HeaderText = "Total Coverage"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '4th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "GCashVal"
                    cs.HeaderText = "Guaranteed Cash Value"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '5th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "CashDiv"
                    cs.HeaderText = "Cash Dividend"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    '6th column
                    cs = New DataGridTextBoxColumn
                    cs.Width = 100
                    cs.MappingName = "TtlCashVal"
                    cs.HeaderText = "Total Cash Value"
                    cs.NullText = gNULLText
                    ts.GridColumnStyles.Add(cs)

                    ts.MappingName = "Show"
                    grdProj.TableStyles.Add(ts)
                    grdProj.DataSource = dtShow
                    grdProj.AllowDrop = False
                    grdProj.ReadOnly = True
                Next

            Case 3
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("AccPrem", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("CashDiv", Type.GetType("System.Decimal"))
                    .Add("AccDiv", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If Mid(Result, 1, 4) = "0000" Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("AccPrem") = FormatNumber(Mid(Result, 6, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("CashDiv") = FormatNumber(Mid(Result, 39, 11) / 100, 0)
                        .Item("AccDiv") = FormatNumber(Mid(Result, 50, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "AccPrem"
                cs.HeaderText = "Acc. Premium"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashVal"
                cs.HeaderText = "Guar. Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "CashDiv"
                cs.HeaderText = "Cash Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "AccDiv"
                cs.HeaderText = "Acc. Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

            Case 4
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("AccPrem", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                    .Add("PaidUpAdd", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("PUACashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("AccPrem") = FormatNumber(Mid(Result, 6, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                        .Item("PaidUpAdd") = FormatNumber(Mid(Result, 72, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("PUACashVal") = FormatNumber(Mid(Result, 83, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "AccPrem"
                cs.HeaderText = "Accumulated Premium"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "PaidUpAdd"
                cs.HeaderText = "Paid Up Addition"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashVal"
                cs.HeaderText = "Guaranteed Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "PUACashVal"
                cs.HeaderText = "PUA Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

            Case 5
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("GCashCou", Type.GetType("System.Decimal"))
                    .Add("ACashCou", Type.GetType("System.Decimal"))
                    .Add("CashDiv", Type.GetType("System.Decimal"))
                    .Add("AccDiv", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("GCashCou") = FormatNumber(Mid(Result, 94, 11) / 100, 0)
                        .Item("ACashCou") = FormatNumber(Mid(Result, 105, 11) / 100, 0)
                        .Item("CashDiv") = FormatNumber(Mid(Result, 39, 11) / 100, 0)
                        .Item("AccDiv") = FormatNumber(Mid(Result, 50, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashCou"
                cs.HeaderText = "Guaranteed Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "ACashCou"
                cs.HeaderText = "Accumulated Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "CashDiv"
                cs.HeaderText = "Cash Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "AccDiv"
                cs.HeaderText = "Accumulated Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

            Case 6
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("GCashCou", Type.GetType("System.Decimal"))
                    .Add("ACashCou", Type.GetType("System.Decimal"))
                    .Add("PaidUpAdd", Type.GetType("System.Decimal"))
                    .Add("PUACashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("GCashCou") = FormatNumber(Mid(Result, 94, 11) / 100, 0)
                        .Item("ACashCou") = FormatNumber(Mid(Result, 105, 11) / 100, 0)
                        .Item("PaidUpAdd") = FormatNumber(Mid(Result, 72, 11) / 100, 0)
                        .Item("PUACashVal") = FormatNumber(Mid(Result, 83, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashCou"
                cs.HeaderText = "Guaranteed Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "ACashCou"
                cs.HeaderText = "Accumulated Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "PaidUpAdd"
                cs.HeaderText = "Paid Up Additions"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "PUACashVal"
                cs.HeaderText = "PUA Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

            Case 7
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("GCashCou", Type.GetType("System.Decimal"))
                    .Add("ACashCou", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("CashDiv", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("GCashCou") = FormatNumber(Mid(Result, 94, 11) / 100, 0)
                        .Item("ACashCou") = FormatNumber(Mid(Result, 105, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("CashDiv") = FormatNumber(Mid(Result, 39, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashCou"
                cs.HeaderText = "Guaranteed Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "ACashCou"
                cs.HeaderText = "Accumulated Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashVal"
                cs.HeaderText = "Guaranteed Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "CashDiv"
                cs.HeaderText = "Cash Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

            Case 8
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("GCashCou", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("CashDiv", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("GCashCou") = FormatNumber(Mid(Result, 94, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("CashDiv") = FormatNumber(Mid(Result, 39, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashCou"
                cs.HeaderText = "Guaranteed Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashVal"
                cs.HeaderText = "Guaranteed Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "CashDiv"
                cs.HeaderText = "Cash Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

            Case 9
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("GCashCou", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("CashDiv", Type.GetType("System.Decimal"))
                    .Add("AccDiv", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("GCashCou") = FormatNumber(Mid(Result, 94, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("CashDiv") = FormatNumber(Mid(Result, 39, 11) / 100, 0)
                        .Item("AccDiv") = FormatNumber(Mid(Result, 50, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashCou"
                cs.HeaderText = "Guaranteed Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashVal"
                cs.HeaderText = "Guaranteed Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "CashDiv"
                cs.HeaderText = "Cash Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "AccDiv"
                cs.HeaderText = "Accumulated Dividend"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

            Case 10
                'set column headers
                With dtShow.Columns
                    .Add("Year", Type.GetType("System.UInt16"))
                    .Add("GCashCou", Type.GetType("System.Decimal"))
                    .Add("GCashVal", Type.GetType("System.Decimal"))
                    .Add("PaidUpAdd", Type.GetType("System.Decimal"))
                    .Add("PUACashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCashVal", Type.GetType("System.Decimal"))
                    .Add("TtlCov", Type.GetType("System.Decimal"))
                End With


                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If Mid(Result, 1, 4) = "0000" Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtShow.NewRow
                    With dr
                        .Item("Year") = Mid(Result, 1, 4)
                        .Item("GCashCou") = FormatNumber(Mid(Result, 94, 11) / 100, 0)
                        .Item("GCashVal") = FormatNumber(Mid(Result, 28, 11) / 100, 0)
                        .Item("PaidUpAdd") = FormatNumber(Mid(Result, 72, 11) / 100, 0)
                        .Item("PUACashVal") = FormatNumber(Mid(Result, 83, 11) / 100, 0)
                        .Item("TtlCashVal") = FormatNumber(Mid(Result, 61, 11) / 100, 0)
                        .Item("TtlCov") = FormatNumber(Mid(Result, 17, 11) / 100, 0)
                    End With
                    dtShow.Rows.Add(dr)
                Next

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "Year"
                cs.HeaderText = "Year"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashCou"
                cs.HeaderText = "Guaranteed Cash Coupon"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "GCashVal"
                cs.HeaderText = "Guaranteed Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "PaidUpAdd"
                cs.HeaderText = "Paid Up Additions"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "PUACashVal"
                cs.HeaderText = "PUA Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCashVal"
                cs.HeaderText = "Total Cash Value"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "TtlCov"
                cs.HeaderText = "Total Coverage"
                cs.NullText = gNULLText
                cs.Format = gNumFormat
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "Show"
                grdProj.TableStyles.Add(ts)

                grdProj.DataSource = dtShow
                grdProj.AllowDrop = False
                grdProj.ReadOnly = True

        End Select

        If blnEnd Then
            Me.cmdNext.Enabled = False
        End If
    End Sub

    Private Sub setRptTable()

        Dim blnEnd As Boolean
        Dim Result As String
        Result = ""
        blnEnd = False

        If Not dtRptTable Is Nothing Then
            dtRptTable = Nothing
        End If
        dtRptTable = New DataTable("RptTable")

        'set column headers
        With dtRptTable.Columns
            .Add("Field1", Type.GetType("System.UInt16"))
            .Add("Field2", Type.GetType("System.Decimal"))
            .Add("Field3", Type.GetType("System.Decimal"))
            .Add("Field4", Type.GetType("System.Decimal"))
            .Add("Field5", Type.GetType("System.Decimal"))
            .Add("Field6", Type.GetType("System.Decimal"))
            .Add("Field7", Type.GetType("System.Decimal"))
            .Add("Field8", Type.GetType("System.Decimal"))
        End With

        Select Case strScrType
            Case 1
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow()
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 6, 9), 0)
                        .Item(2) = FormatNumber(strSumIns, 0)
                        .Item(3) = FormatNumber(Mid(Result, 96, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 15, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 105, 9), 0)
                        .Item(7) = FormatNumber(Mid(Result, 51, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 2
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 6, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 15, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 33, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 51, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 3
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If Mid(Result, 1, 4) = "0000" Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 6, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 15, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 33, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 42, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 51, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 4
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 6, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 15, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 60, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 69, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 51, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 5
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 78, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 87, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 33, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 42, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 51, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 15, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 6
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 78, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 87, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 60, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 69, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 51, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 15, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 7
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 78, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 87, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 33, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 51, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 15, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 8
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 78, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 33, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 51, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 15, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 9
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If ((Result = "") Or (Mid(Result, 1, 4) = "0000")) Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 78, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 33, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 42, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 51, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 15, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next

            Case 10
                For i As Integer = 1 To 10
                    Result = dtProjection.Rows(0).Item(i Mod 10)
                    If Mid(Result, 1, 4) = "0000" Then
                        blnEnd = True
                        Exit For
                    End If

                    Dim dr As DataRow
                    dr = dtRptTable.NewRow
                    With dr
                        .Item(0) = Mid(Result, 1, 4)
                        .Item(1) = FormatNumber(Mid(Result, 78, 9), 0)
                        .Item(2) = FormatNumber(Mid(Result, 24, 9), 0)
                        .Item(3) = FormatNumber(Mid(Result, 60, 9), 0)
                        .Item(4) = FormatNumber(Mid(Result, 69, 9), 0)
                        .Item(5) = FormatNumber(Mid(Result, 51, 9), 0)
                        .Item(6) = FormatNumber(Mid(Result, 15, 9), 0)
                    End With
                    dtRptTable.Rows.Add(dr)
                Next
        End Select

        If blnEnd Then
            Me.cmdNext.Enabled = False
        End If

    End Sub

    Private Sub cmdPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        If PolicyNo <> "" Then
            Me.cmdNext.Enabled = True
            intyear = intyear - 10
            If intyear < 10 Then
                intyear = intyear + 10
            Else
                Me.Cursor = Cursors.WaitCursor
                getProjection()
                Me.Cursor = Cursors.Default
            End If
        Else
            MsgBox("Please enter a Policy No.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
            Me.txtPolicyNo.Focus()
        End If
    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        If PolicyNo <> "" Then
            Me.cmdPrev.Enabled = True
            intyear = intyear + 10
            Me.Cursor = Cursors.WaitCursor
            getProjection()
            Me.Cursor = Cursors.Default
        Else
            MsgBox("Please enter a Policy No.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
            Me.txtPolicyNo.Focus()
        End If
    End Sub

    Private Sub txtPolicyNo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNo.Enter
        Me.AcceptButton = Me.cmdRefresh
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        init()
        blnPrintRpt = True

        wndMain.Cursor = Cursors.WaitCursor
        getInfo()
        If blnPolFound Then
            getProjection()

            If blnEnd Then
                MsgBox("No Projection record found.", MsgBoxStyle.OKOnly + MsgBoxStyle.Information, "Policy Projection")
                Exit Sub
            End If

            If LoadReport() AndAlso PrepareData() Then

                Call PrintReport(rpt, PrintDest.pdPreview)

                'If strLang = "E" Then
                '    rpt.SetParameterValue("strLangC", "C")
                '    rpt.SetParameterValue("strCSR", gsCSRChiName)
                '    rpt.SetParameterValue("strPolName", strChiProductName)
                '    Call PrintReport(rpt, PrintDest.pdPreview)
                'End If

            End If
        End If
        wndMain.Cursor = Cursors.Default
    End Sub

    Private Function LoadReport() As Boolean
        Dim strPath As String

        strPath = Application.StartupPath
        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    If InStr(strPath, "\bin") > 0 Then
        '        strPath = Replace(strPath, "\bin", "")
        '    End If
        'End If
        'AC - Change to use configuration setting - end
        strPath &= "\Report\" & "PolicyProjection.rpt"


        rpt = New ReportDocument
        rpt.Load(strPath)

        Return True
    End Function

    Private Function PrepareData() As Boolean
        objRptLogic.CR_Rpt = rpt
        objRptLogic.Projection_Rpt(PolicyNo, strScrType, dtRptTable, Val(strSumIns), strChiProductName, strLang)

        Return True
    End Function

    Private Sub PrintReport(ByVal rpt As ReportDocument, ByVal strDest As PrintDest)

        Select Case strDest

            Case PrintDest.pdPrinter
                Try
                    rpt.PrintToPrinter(1, True, 0, 0)
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Reports")
                    Exit Sub
                End Try
                MsgBox("Report Printed Successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Reports")

            Case PrintDest.pdPreview
                Try
                    Dim f As New frmProjRpt
                    f.CrystalReportViewer1.ReportSource = rpt
                    f.ShowDialog()

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Reports")
                End Try

            Case PrintDest.pdFax
                'rpt.PrintOptions.PrinterName = dr(0).Item("cswrel_fax_name")
                'rpt.PrintToPrinter(1, True, 0, 0)

        End Select

    End Sub
End Class
