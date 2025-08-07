Imports System.Data.SqlClient

Public Class uclUTRH
    Inherits System.Windows.Forms.UserControl

    Dim strPolicy As String
    Dim dtUNITS, dtUNITSTX As DataTable
    Dim WithEvents bm As BindingManagerBase

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
        CType(Me.grdUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUnitTx, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.grdUnit.Size = New System.Drawing.Size(404, 108)
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
        Me.grdUnitTx.Location = New System.Drawing.Point(4, 180)
        Me.grdUnitTx.Name = "grdUnitTx"
        Me.grdUnitTx.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdUnitTx.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdUnitTx.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdUnitTx.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUnitTx.Size = New System.Drawing.Size(672, 180)
        Me.grdUnitTx.TabIndex = 3
        '
        'txtFDate
        '
        Me.txtFDate.Location = New System.Drawing.Point(104, 4)
        Me.txtFDate.Name = "txtFDate"
        Me.txtFDate.Size = New System.Drawing.Size(132, 20)
        Me.txtFDate.TabIndex = 82
        '
        'cmdGO
        '
        Me.cmdGO.Location = New System.Drawing.Point(240, 4)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(28, 20)
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
        Me.Label1.Size = New System.Drawing.Size(102, 16)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Enquiry From Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(280, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 16)
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
        Me.txtStatus.Text = ""
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
        Me.Label3.Size = New System.Drawing.Size(91, 16)
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
        Me.Label4.Location = New System.Drawing.Point(8, 164)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(87, 16)
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
        Me.Label5.Location = New System.Drawing.Point(416, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 16)
        Me.Label5.TabIndex = 91
        Me.Label5.Text = "Total Balance:"
        '
        'txtTtlBal
        '
        Me.txtTtlBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTtlBal.Location = New System.Drawing.Point(500, 136)
        Me.txtTtlBal.Name = "txtTtlBal"
        Me.txtTtlBal.ReadOnly = True
        Me.txtTtlBal.Size = New System.Drawing.Size(108, 20)
        Me.txtTtlBal.TabIndex = 90
        Me.txtTtlBal.Text = ""
        '
        'uclUTRH
        '
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
        Me.Controls.Add(Me.grdUnitTx)
        Me.Controls.Add(Me.grdUnit)
        Me.Name = "uclUTRH"
        Me.Size = New System.Drawing.Size(684, 364)
        CType(Me.grdUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUnitTx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WriteOnly Property PolicyAccountID() As String
        Set(ByVal Value As String)
            strPolicy = Value
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
        cs.Width = 120
        cs.MappingName = "FundBalance"
        cs.HeaderText = "Fund Balance"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

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
        cs.Width = 80
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

    Private Sub GetUTRHRecord()

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim blnBUI_U, blnBUI_Tx As Boolean
        Dim dtUTRHtemp(1) As DataTable

        wndMain.Cursor = Cursors.WaitCursor

        dtUTRHtemp = objCS.GetUTRH(strPolicy, txtFDate.Value, lngErrNo, strErrMsg)

#If UAT <> 0 Then
        'dtUTRHtemp = objCS.GetUTRH("5010242383", txtFDate.Value, lngErrNo, strErrMsg)
#End If

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If Not dtUTRHtemp Is Nothing Then

                ' Status
                If dtUTRHtemp(0).Rows.Count > 0 Then
                    txtStatus.Text = dtUTRHtemp(0).Rows(0).Item("Status")
                    txtTtlBal.Text = Format(dtUTRHtemp(0).Rows(0).Item("TotalBalance"), gNumFormat)
                Else
                    MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "UTRH")
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

                Dim dr As DataRow
                Dim ar() As Object
                For Each dr In dtUTRHtemp(1).Rows
                    ar = dr.ItemArray
                    dtUNITS.Rows.Add(ar)
                Next

                If blnBUI_U Then Call BuildUI_Unit()

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

                If blnBUI_Tx Then Call BuildUI_UTx()

                Call Me.CheckLimit()

                'Else
                '    MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "UTRH")
                'End If

            Else
            MsgBox("No UTRH Record found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "UTRH")
        End If
        Else
        MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

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
    End Sub

    Private Sub uclUTRH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFDate.Value = Today
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

End Class
