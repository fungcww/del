Imports System.Data.SqlClient

Public Class DCAR
    Inherits System.Windows.Forms.UserControl

    Private bm As BindingManagerBase
    Private bm2 As BindingManagerBase
    Dim ds As DataSet = New DataSet("DirectCredit")
    Dim dr, dr1 As DataRow

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
    Friend WithEvents txtStatusChgDate As System.Windows.Forms.TextBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents txtBankerName As System.Windows.Forms.TextBox
    Friend WithEvents txtAC3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAC2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAC1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPayorInfo As System.Windows.Forms.TextBox
    Friend WithEvents txtEffDate As System.Windows.Forms.TextBox
    Friend WithEvents txtEndDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLastStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtSTatus As System.Windows.Forms.TextBox
    Friend WithEvents txtSeqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdAC As System.Windows.Forms.DataGrid
    Friend WithEvents txtCrDay As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtStatusChgDate = New System.Windows.Forms.TextBox
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.txtBankerName = New System.Windows.Forms.TextBox
        Me.txtAC3 = New System.Windows.Forms.TextBox
        Me.txtAC2 = New System.Windows.Forms.TextBox
        Me.txtAC1 = New System.Windows.Forms.TextBox
        Me.txtPayorInfo = New System.Windows.Forms.TextBox
        Me.txtEffDate = New System.Windows.Forms.TextBox
        Me.txtEndDate = New System.Windows.Forms.TextBox
        Me.txtCrDay = New System.Windows.Forms.TextBox
        Me.txtLastStatus = New System.Windows.Forms.TextBox
        Me.txtSTatus = New System.Windows.Forms.TextBox
        Me.txtSeqNo = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdAC = New System.Windows.Forms.DataGrid
        CType(Me.grdAC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtStatusChgDate
        '
        Me.txtStatusChgDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatusChgDate.Location = New System.Drawing.Point(108, 140)
        Me.txtStatusChgDate.Name = "txtStatusChgDate"
        Me.txtStatusChgDate.ReadOnly = True
        Me.txtStatusChgDate.Size = New System.Drawing.Size(80, 20)
        Me.txtStatusChgDate.TabIndex = 111
        Me.txtStatusChgDate.Text = ""
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.SystemColors.Window
        Me.txtComments.Location = New System.Drawing.Point(108, 244)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(432, 48)
        Me.txtComments.TabIndex = 107
        Me.txtComments.Text = ""
        '
        'txtBankerName
        '
        Me.txtBankerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankerName.Location = New System.Drawing.Point(108, 192)
        Me.txtBankerName.Name = "txtBankerName"
        Me.txtBankerName.ReadOnly = True
        Me.txtBankerName.Size = New System.Drawing.Size(432, 20)
        Me.txtBankerName.TabIndex = 105
        Me.txtBankerName.Text = ""
        '
        'txtAC3
        '
        Me.txtAC3.BackColor = System.Drawing.SystemColors.Window
        Me.txtAC3.Location = New System.Drawing.Point(188, 168)
        Me.txtAC3.Name = "txtAC3"
        Me.txtAC3.ReadOnly = True
        Me.txtAC3.Size = New System.Drawing.Size(184, 20)
        Me.txtAC3.TabIndex = 103
        Me.txtAC3.Text = ""
        '
        'txtAC2
        '
        Me.txtAC2.Location = New System.Drawing.Point(148, 168)
        Me.txtAC2.Name = "txtAC2"
        Me.txtAC2.Size = New System.Drawing.Size(40, 20)
        Me.txtAC2.TabIndex = 102
        Me.txtAC2.Text = ""
        '
        'txtAC1
        '
        Me.txtAC1.Location = New System.Drawing.Point(108, 168)
        Me.txtAC1.Name = "txtAC1"
        Me.txtAC1.Size = New System.Drawing.Size(40, 20)
        Me.txtAC1.TabIndex = 101
        Me.txtAC1.Text = ""
        '
        'txtPayorInfo
        '
        Me.txtPayorInfo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayorInfo.Location = New System.Drawing.Point(108, 216)
        Me.txtPayorInfo.Name = "txtPayorInfo"
        Me.txtPayorInfo.ReadOnly = True
        Me.txtPayorInfo.Size = New System.Drawing.Size(432, 20)
        Me.txtPayorInfo.TabIndex = 99
        Me.txtPayorInfo.Text = ""
        '
        'txtEffDate
        '
        Me.txtEffDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEffDate.Location = New System.Drawing.Point(248, 116)
        Me.txtEffDate.Name = "txtEffDate"
        Me.txtEffDate.ReadOnly = True
        Me.txtEffDate.Size = New System.Drawing.Size(80, 20)
        Me.txtEffDate.TabIndex = 91
        Me.txtEffDate.Text = ""
        '
        'txtEndDate
        '
        Me.txtEndDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEndDate.Location = New System.Drawing.Point(400, 116)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.ReadOnly = True
        Me.txtEndDate.Size = New System.Drawing.Size(80, 20)
        Me.txtEndDate.TabIndex = 85
        Me.txtEndDate.Text = ""
        '
        'txtCrDay
        '
        Me.txtCrDay.BackColor = System.Drawing.SystemColors.Window
        Me.txtCrDay.Location = New System.Drawing.Point(560, 116)
        Me.txtCrDay.Name = "txtCrDay"
        Me.txtCrDay.ReadOnly = True
        Me.txtCrDay.Size = New System.Drawing.Size(80, 20)
        Me.txtCrDay.TabIndex = 83
        Me.txtCrDay.Text = ""
        '
        'txtLastStatus
        '
        Me.txtLastStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastStatus.Location = New System.Drawing.Point(488, 140)
        Me.txtLastStatus.Name = "txtLastStatus"
        Me.txtLastStatus.ReadOnly = True
        Me.txtLastStatus.Size = New System.Drawing.Size(152, 20)
        Me.txtLastStatus.TabIndex = 81
        Me.txtLastStatus.Text = ""
        '
        'txtSTatus
        '
        Me.txtSTatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTatus.Location = New System.Drawing.Point(248, 140)
        Me.txtSTatus.Name = "txtSTatus"
        Me.txtSTatus.ReadOnly = True
        Me.txtSTatus.Size = New System.Drawing.Size(152, 20)
        Me.txtSTatus.TabIndex = 79
        Me.txtSTatus.Text = ""
        '
        'txtSeqNo
        '
        Me.txtSeqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeqNo.Location = New System.Drawing.Point(108, 116)
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.ReadOnly = True
        Me.txtSeqNo.Size = New System.Drawing.Size(44, 20)
        Me.txtSeqNo.TabIndex = 77
        Me.txtSeqNo.Text = ""
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(12, 144)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(87, 16)
        Me.Label17.TabIndex = 110
        Me.Label17.Text = "Status Chg Date"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(40, 248)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(59, 16)
        Me.Label15.TabIndex = 106
        Me.Label15.Text = "Comments"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(24, 196)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 16)
        Me.Label14.TabIndex = 104
        Me.Label14.Text = "Banker Name"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(24, 172)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 16)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Bank A/C No."
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 220)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(93, 16)
        Me.Label12.TabIndex = 98
        Me.Label12.Text = "A/C Holder Name"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(164, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 16)
        Me.Label8.TabIndex = 90
        Me.Label8.Text = "Effective Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(340, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 16)
        Me.Label5.TabIndex = 84
        Me.Label5.Text = "End Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(492, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 16)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "Credit Day"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(416, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 16)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "Last Status"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(204, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 16)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "Status"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Location = New System.Drawing.Point(24, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 16)
        Me.Label1.TabIndex = 76
        Me.Label1.Text = "Sequence No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdAC
        '
        Me.grdAC.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAC.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAC.CaptionFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAC.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAC.CaptionVisible = False
        Me.grdAC.DataMember = ""
        Me.grdAC.FlatMode = True
        Me.grdAC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAC.ForeColor = System.Drawing.Color.Black
        Me.grdAC.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAC.HeaderBackColor = System.Drawing.Color.Teal
        Me.grdAC.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAC.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAC.Location = New System.Drawing.Point(4, 4)
        Me.grdAC.Name = "grdAC"
        Me.grdAC.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAC.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAC.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAC.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAC.Size = New System.Drawing.Size(576, 104)
        Me.grdAC.TabIndex = 74
        '
        'DCAR
        '
        Me.Controls.Add(Me.txtStatusChgDate)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.txtBankerName)
        Me.Controls.Add(Me.txtAC3)
        Me.Controls.Add(Me.txtAC2)
        Me.Controls.Add(Me.txtAC1)
        Me.Controls.Add(Me.txtPayorInfo)
        Me.Controls.Add(Me.txtEffDate)
        Me.Controls.Add(Me.txtEndDate)
        Me.Controls.Add(Me.txtCrDay)
        Me.Controls.Add(Me.txtLastStatus)
        Me.Controls.Add(Me.txtSTatus)
        Me.Controls.Add(Me.txtSeqNo)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdAC)
        Me.Name = "DCAR"
        Me.Size = New System.Drawing.Size(652, 324)
        CType(Me.grdAC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WriteOnly Property srcDCAR() As DataTable
        Set(ByVal Value As DataTable)
            If Not Value Is Nothing Then
                ds.Tables.Add(Value)
            End If
            Call buildUI()
        End Set
    End Property

    Private Sub buildUI()

        Dim strSQL As String
        Dim lngCnt As Long

        If ds.Tables("DCAR").Rows.Count > 0 Then
            With ds.Tables("DCAR").Rows(0)

                Dim ts As New clsDataGridTableStyle
                Dim cs As DataGridTextBoxColumn

                cs = New DataGridTextBoxColumn
                cs.Width = 50
                cs.MappingName = "SeqNo"
                cs.HeaderText = "Seq. No."
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 80
                cs.MappingName = "BankCode"
                cs.HeaderText = "Bank Code"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 80
                cs.MappingName = "BranchCode"
                cs.HeaderText = "Branch Code"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 100
                cs.MappingName = "BankAccNo"
                cs.HeaderText = "A/C No."
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "BankName"
                cs.HeaderText = "Bank"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "BranchName"
                cs.HeaderText = "Branch"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "DCAR"
                grdAC.TableStyles.Add(ts)

                grdAC.DataSource = ds.Tables("DCAR")
                grdAC.AllowDrop = False
                grdAC.ReadOnly = True

            End With

            bm = Me.BindingContext(ds.Tables("DCAR"))
            ds.Tables("DCAR").DefaultView.Sort = "SeqNo DESC"

            Dim bStsCDate As Binding = New Binding("Text", ds.Tables("DCAR"), "StsChgDate")
            Me.txtStatusChgDate.DataBindings.Add(bStsCDate)
            AddHandler bStsCDate.Format, AddressOf FormatDate

            Dim bEndDate As Binding = New Binding("Text", ds.Tables("DCAR"), "EndDate")
            Me.txtEndDate.DataBindings.Add(bEndDate)
            AddHandler bEndDate.Format, AddressOf FormatDate

            Dim bEffDate As Binding = New Binding("Text", ds.Tables("DCAR"), "EffDate")
            Me.txtEffDate.DataBindings.Add(bEffDate)
            AddHandler bEffDate.Format, AddressOf FormatDate

            txtSeqNo.DataBindings.Add("Text", ds.Tables("DCAR"), "SeqNo")
            txtCrDay.DataBindings.Add("Text", ds.Tables("DCAR"), "CreditDay")
            txtPayorInfo.DataBindings.Add("Text", ds.Tables("DCAR"), "AccName")
            txtAC1.DataBindings.Add("Text", ds.Tables("DCAR"), "BankCode")
            txtAC2.DataBindings.Add("Text", ds.Tables("DCAR"), "BranchCode")

            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            'If ExternalUser Then
            '    MaskDTsource(ds.Tables("DCAR"), "BankAccNo", MaskData.BANK_ACCOUNT_NO)
            'End If
            ''CRS 7x24 Changes - End
            txtAC3.DataBindings.Add("Text", ds.Tables("DCAR"), "BankAccNo")

            txtComments.DataBindings.Add("Text", ds.Tables("DCAR"), "Comments")

            Call UpdatePT()

        End If

    End Sub

    Private Sub grdAC_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAC.CurrentCellChanged
        Call UpdatePT()
    End Sub

    Private Sub UpdatePT()

        Dim drI As DataRow
        drI = CType(bm.Current, DataRowView).Row()
        If Not drI Is Nothing Then
            txtSTatus.Text = DCARStatus(drI.Item("Status"))
            txtLastStatus.Text = DCARStatus(drI.Item("LStatus"))
            txtBankerName.Text = Trim(drI.Item("BankName")) & " (" & Trim(drI.Item("BranchName")) & ")"
        End If

    End Sub

    Private Sub grdAC_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAC.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAC.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAC.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAC.Select(hti.Row)
        End If
    End Sub

End Class
