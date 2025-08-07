Public Class frmPRTS
    Inherits System.Windows.Forms.Form

    Dim dtResult As DataTable

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
    Friend WithEvents txtPlan As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRS As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSmoker As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPAR As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSEX As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTBL1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTBL2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtAge As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDur As System.Windows.Forms.TextBox
    Friend WithEvents grdPRTS As System.Windows.Forms.DataGrid
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPlan = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRS = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSmoker = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPAR = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSEX = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtTBL1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTBL2 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtAge = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtDur = New System.Windows.Forms.TextBox
        Me.grdPRTS = New System.Windows.Forms.DataGrid
        Me.cmdSearch = New System.Windows.Forms.Button
        CType(Me.grdPRTS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPlan
        '
        Me.txtPlan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPlan.Location = New System.Drawing.Point(40, 8)
        Me.txtPlan.MaxLength = 5
        Me.txtPlan.Name = "txtPlan"
        Me.txtPlan.Size = New System.Drawing.Size(56, 20)
        Me.txtPlan.TabIndex = 0
        Me.txtPlan.Text = "AL"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Plan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(104, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "RateScale"
        '
        'txtRS
        '
        Me.txtRS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRS.Location = New System.Drawing.Point(168, 8)
        Me.txtRS.MaxLength = 1
        Me.txtRS.Name = "txtRS"
        Me.txtRS.Size = New System.Drawing.Size(24, 20)
        Me.txtRS.TabIndex = 2
        Me.txtRS.Text = "1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(200, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Smoker"
        '
        'txtSmoker
        '
        Me.txtSmoker.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSmoker.Location = New System.Drawing.Point(248, 8)
        Me.txtSmoker.MaxLength = 1
        Me.txtSmoker.Name = "txtSmoker"
        Me.txtSmoker.Size = New System.Drawing.Size(24, 20)
        Me.txtSmoker.TabIndex = 4
        Me.txtSmoker.Text = "N"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(280, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Participate"
        '
        'txtPAR
        '
        Me.txtPAR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPAR.Location = New System.Drawing.Point(344, 8)
        Me.txtPAR.MaxLength = 1
        Me.txtPAR.Name = "txtPAR"
        Me.txtPAR.Size = New System.Drawing.Size(24, 20)
        Me.txtPAR.TabIndex = 6
        Me.txtPAR.Text = "P"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(376, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Sex"
        '
        'txtSEX
        '
        Me.txtSEX.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSEX.Location = New System.Drawing.Point(404, 8)
        Me.txtSEX.MaxLength = 1
        Me.txtSEX.Name = "txtSEX"
        Me.txtSEX.Size = New System.Drawing.Size(24, 20)
        Me.txtSEX.TabIndex = 8
        Me.txtSEX.Text = "F"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(436, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "TBL1"
        '
        'txtTBL1
        '
        Me.txtTBL1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTBL1.Location = New System.Drawing.Point(472, 8)
        Me.txtTBL1.MaxLength = 2
        Me.txtTBL1.Name = "txtTBL1"
        Me.txtTBL1.Size = New System.Drawing.Size(24, 20)
        Me.txtTBL1.TabIndex = 10
        Me.txtTBL1.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(504, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 16)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "TBL2"
        '
        'txtTBL2
        '
        Me.txtTBL2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTBL2.Location = New System.Drawing.Point(540, 8)
        Me.txtTBL2.MaxLength = 2
        Me.txtTBL2.Name = "txtTBL2"
        Me.txtTBL2.Size = New System.Drawing.Size(24, 20)
        Me.txtTBL2.TabIndex = 12
        Me.txtTBL2.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(572, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(24, 16)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Age"
        '
        'txtAge
        '
        Me.txtAge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAge.Location = New System.Drawing.Point(600, 8)
        Me.txtAge.MaxLength = 3
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(32, 20)
        Me.txtAge.TabIndex = 14
        Me.txtAge.Text = "0"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(640, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 16)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Duration"
        '
        'txtDur
        '
        Me.txtDur.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDur.Location = New System.Drawing.Point(692, 8)
        Me.txtDur.MaxLength = 3
        Me.txtDur.Name = "txtDur"
        Me.txtDur.Size = New System.Drawing.Size(32, 20)
        Me.txtDur.TabIndex = 16
        Me.txtDur.Text = "0"
        '
        'grdPRTS
        '
        Me.grdPRTS.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPRTS.BackColor = System.Drawing.Color.White
        Me.grdPRTS.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPRTS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPRTS.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPRTS.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPRTS.CaptionVisible = False
        Me.grdPRTS.DataMember = ""
        Me.grdPRTS.FlatMode = True
        Me.grdPRTS.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPRTS.ForeColor = System.Drawing.Color.Black
        Me.grdPRTS.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPRTS.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPRTS.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPRTS.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPRTS.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPRTS.Location = New System.Drawing.Point(8, 40)
        Me.grdPRTS.Name = "grdPRTS"
        Me.grdPRTS.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPRTS.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPRTS.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPRTS.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPRTS.Size = New System.Drawing.Size(716, 240)
        Me.grdPRTS.TabIndex = 18
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(648, 284)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.TabIndex = 19
        Me.cmdSearch.Text = "Search"
        '
        'frmPRTS
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(732, 313)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.grdPRTS)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtDur)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtAge)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTBL2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTBL1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtSEX)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPAR)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSmoker)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRS)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPlan)
        Me.Name = "frmPRTS"
        Me.Text = "Premium Rates"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdPRTS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub BuildUI()

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "PRDUR"
        cs.HeaderText = "Duration"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 40
        cs.MappingName = "PRAGE"
        cs.HeaderText = "Age"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PRRATE"
        cs.HeaderText = "Base Prem."
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PRADRT"
        cs.HeaderText = "AD Prem."
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PRWPRT"
        cs.HeaderText = "WP Prem."
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PRSECP"
        cs.HeaderText = "Second Base Prem."
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PRTHIP"
        cs.HeaderText = "Third Base Prem."
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PRFEE"
        cs.HeaderText = "Table Rating"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "PRADJ"
        cs.HeaderText = "NM Adj. Factor"
        cs.NullText = gNULLText
        cs.Format = gNumFormat
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "PRTS"
        grdPRTS.TableStyles.Add(ts)
        dtResult.DefaultView.Sort = "PRAGE ASC"

        grdPRTS.DataSource = dtResult
        grdPRTS.AllowDrop = False
        grdPRTS.ReadOnly = True

    End Sub

    Private Sub grdPRTS_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPRTS.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdPRTS.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdPRTS.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdPRTS.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dtTemp As DataTable
        Dim blnBUI As Boolean
        Dim intAge, intDur As Integer

        If IsNumeric(txtAge.Text) Then
            intAge = CInt(txtAge.Text)
        Else
            intAge = 0
        End If

        If IsNumeric(txtDur.Text) Then
            intDur = CInt(txtDur.Text)
        Else
            intDur = 0
        End If

        If txtPlan.Text = "" OrElse txtRS.Text = "" OrElse txtSmoker.Text = "" OrElse txtPAR.Text = "" OrElse txtSEX.Text = "" Then
            MsgBox("Please input required paremeter first.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor

        dtTemp = objCS.GetPRTS(Trim(txtPlan.Text), Trim(txtRS.Text), Trim(txtSmoker.Text), Trim(txtPAR.Text), _
            Trim(txtSEX.Text), Trim(txtTBL1.Text), Trim(txtTBL2.Text), intAge, intDur, lngErrNo, strErrMsg)

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If dtTemp Is Nothing Then
                MsgBox("Record not found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Else

                If dtResult Is Nothing Then
                    blnBUI = True
                    dtResult = dtTemp.Copy
                    dtResult.TableName = "PRTS"
                End If

                If blnBUI Then
                    Call BuildUI()
                Else
                    dtResult.Rows.Clear()

                    Dim dr As DataRow
                    Dim ar() As Object
                    For Each dr In dtTemp.Rows
                        ar = dr.ItemArray
                        dtResult.Rows.Add(ar)
                    Next

                End If

                If dtTemp.Rows.Count = 0 Then
                    MsgBox("Record not found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
                End If

            End If
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

    End Sub

End Class
