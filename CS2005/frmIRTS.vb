Public Class frmIRTS
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
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTMts As System.Windows.Forms.TextBox
    Friend WithEvents txtTDays As System.Windows.Forms.TextBox
    Friend WithEvents grdIRTS As System.Windows.Forms.DataGrid
    Friend WithEvents txtEffDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMaxAmt As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtPlan = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRS = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtTMts = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtTDays = New System.Windows.Forms.TextBox
        Me.grdIRTS = New System.Windows.Forms.DataGrid
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.txtEffDate = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMaxAmt = New System.Windows.Forms.TextBox
        CType(Me.grdIRTS, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.txtPlan.Text = "APL"
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
        Me.Label3.Size = New System.Drawing.Size(45, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Eff.Date"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(384, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 16)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Term:Months"
        '
        'txtTMts
        '
        Me.txtTMts.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTMts.Location = New System.Drawing.Point(460, 8)
        Me.txtTMts.MaxLength = 3
        Me.txtTMts.Name = "txtTMts"
        Me.txtTMts.Size = New System.Drawing.Size(32, 20)
        Me.txtTMts.TabIndex = 14
        Me.txtTMts.Text = "000"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(500, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 16)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Days"
        '
        'txtTDays
        '
        Me.txtTDays.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTDays.Location = New System.Drawing.Point(536, 8)
        Me.txtTDays.MaxLength = 3
        Me.txtTDays.Name = "txtTDays"
        Me.txtTDays.Size = New System.Drawing.Size(32, 20)
        Me.txtTDays.TabIndex = 16
        Me.txtTDays.Text = "000"
        '
        'grdIRTS
        '
        Me.grdIRTS.AlternatingBackColor = System.Drawing.Color.White
        Me.grdIRTS.BackColor = System.Drawing.Color.White
        Me.grdIRTS.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdIRTS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdIRTS.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdIRTS.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdIRTS.CaptionVisible = False
        Me.grdIRTS.DataMember = ""
        Me.grdIRTS.FlatMode = True
        Me.grdIRTS.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdIRTS.ForeColor = System.Drawing.Color.Black
        Me.grdIRTS.GridLineColor = System.Drawing.Color.Wheat
        Me.grdIRTS.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdIRTS.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdIRTS.HeaderForeColor = System.Drawing.Color.Black
        Me.grdIRTS.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdIRTS.Location = New System.Drawing.Point(8, 40)
        Me.grdIRTS.Name = "grdIRTS"
        Me.grdIRTS.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdIRTS.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdIRTS.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdIRTS.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdIRTS.Size = New System.Drawing.Size(716, 240)
        Me.grdIRTS.TabIndex = 18
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(648, 284)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.TabIndex = 19
        Me.cmdSearch.Text = "Search"
        '
        'txtEffDate
        '
        Me.txtEffDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.txtEffDate.Location = New System.Drawing.Point(252, 8)
        Me.txtEffDate.Name = "txtEffDate"
        Me.txtEffDate.Size = New System.Drawing.Size(124, 20)
        Me.txtEffDate.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(576, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 16)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Max. Amount"
        '
        'txtMaxAmt
        '
        Me.txtMaxAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMaxAmt.Location = New System.Drawing.Point(652, 8)
        Me.txtMaxAmt.MaxLength = 3
        Me.txtMaxAmt.Name = "txtMaxAmt"
        Me.txtMaxAmt.Size = New System.Drawing.Size(72, 20)
        Me.txtMaxAmt.TabIndex = 21
        Me.txtMaxAmt.Text = "999999999"
        '
        'frmIRTS
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(732, 309)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtMaxAmt)
        Me.Controls.Add(Me.txtEffDate)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.grdIRTS)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtTDays)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTMts)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRS)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPlan)
        Me.Name = "frmIRTS"
        Me.Text = "Interest Rates"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdIRTS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub BuildUI()

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "EffDate"
        cs.HeaderText = "Eff.Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 60
        cs.MappingName = "FLD0005"
        cs.HeaderText = "Months"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "FLD0006"
        cs.HeaderText = "Days"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "FLD0007"
        cs.HeaderText = "Max.Amount"
        cs.NullText = gNULLText
        cs.Format = "#,###"
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "FLD0011"
        cs.HeaderText = "Initial"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "FLD0012"
        cs.HeaderText = "Rollover"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "FLD0013"
        cs.HeaderText = "M/V Adj."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "IRTS"
        grdIRTS.TableStyles.Add(ts)
        dtResult.DefaultView.Sort = "EffDate DESC"

        grdIRTS.DataSource = dtResult
        grdIRTS.AllowDrop = False
        grdIRTS.ReadOnly = True

    End Sub

    Private Sub grdIRTS_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdIRTS.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdIRTS.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdIRTS.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdIRTS.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dtTemp As DataTable
        Dim blnBUI As Boolean = False
        Dim intMTS, intDays, intMaxAmt As Integer

        If txtPlan.Text = "" OrElse txtRS.Text = "" Then
            MsgBox("Please input required paremeter first.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Exit Sub
        End If

        If txtTMts.Text = "" Then
            intMTS = 0
        End If

        If txtTDays.Text = "" Then
            intDays = 0
        End If

        If txtMaxAmt.Text = "" Then
            intMaxAmt = 999999999
        End If

        wndMain.Cursor = Cursors.WaitCursor

        'dt(0) = objCS.GetIRTS("APL", "1", #1/1/2000#, 0, 0, 0, lngErrNo, strErrMsg)
        dtTemp = objCS.GetIRTS(Trim(txtPlan.Text), Trim(txtRS.Text), txtEffDate.Value, intMTS, intDays, _
            intMaxAmt, lngErrNo, strErrMsg)

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If dtTemp Is Nothing Then
                MsgBox("Record not found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Else

                If dtResult Is Nothing Then
                    blnBUI = True
                    dtResult = dtTemp.Copy
                    dtResult.TableName = "IRTS"
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
