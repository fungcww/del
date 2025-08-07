Public Class frmUVAL
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
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents grdUVAL As System.Windows.Forms.DataGrid
    Friend WithEvents cboRecType As System.Windows.Forms.ComboBox
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
        Me.grdUVAL = New System.Windows.Forms.DataGrid
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.cboRecType = New System.Windows.Forms.ComboBox
        CType(Me.grdUVAL, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.txtAge.Text = "1"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 36)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 16)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Record Type"
        '
        'grdUVAL
        '
        Me.grdUVAL.AlternatingBackColor = System.Drawing.Color.White
        Me.grdUVAL.BackColor = System.Drawing.Color.White
        Me.grdUVAL.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdUVAL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdUVAL.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUVAL.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdUVAL.CaptionVisible = False
        Me.grdUVAL.DataMember = ""
        Me.grdUVAL.FlatMode = True
        Me.grdUVAL.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdUVAL.ForeColor = System.Drawing.Color.Black
        Me.grdUVAL.GridLineColor = System.Drawing.Color.Wheat
        Me.grdUVAL.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdUVAL.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdUVAL.HeaderForeColor = System.Drawing.Color.Black
        Me.grdUVAL.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUVAL.Location = New System.Drawing.Point(8, 60)
        Me.grdUVAL.Name = "grdUVAL"
        Me.grdUVAL.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdUVAL.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdUVAL.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdUVAL.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUVAL.Size = New System.Drawing.Size(628, 220)
        Me.grdUVAL.TabIndex = 18
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(560, 284)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.TabIndex = 19
        Me.cmdSearch.Text = "Search"
        '
        'cboRecType
        '
        Me.cboRecType.Items.AddRange(New Object() {"CSV", "DVP", "DVR", "NSP", "NSQ", "MOR", "DX", "MX", "TXP", "TXR", "MBG", "MBR", "ANR", "QX", "SIV", "RB"})
        Me.cboRecType.Location = New System.Drawing.Point(84, 32)
        Me.cboRecType.Name = "cboRecType"
        Me.cboRecType.Size = New System.Drawing.Size(72, 21)
        Me.cboRecType.TabIndex = 20
        Me.cboRecType.Text = "CSV"
        '
        'frmUVAL
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(640, 309)
        Me.Controls.Add(Me.cboRecType)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.grdUVAL)
        Me.Controls.Add(Me.Label9)
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
        Me.Name = "frmUVAL"
        Me.Text = "Unit Values"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdUVAL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub BuildUI()

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Duration"
        cs.HeaderText = "Duration"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 160
        cs.MappingName = "ValPerUnit"
        cs.HeaderText = "Value Per Unit"
        cs.NullText = gNULLText
        cs.Format = "#,##0.0000000"
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "UVAL"
        grdUVAL.TableStyles.Add(ts)

        grdUVAL.DataSource = dtResult
        grdUVAL.AllowDrop = False
        grdUVAL.ReadOnly = True

    End Sub

    Private Sub grdUVAL_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdUVAL.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdUVAL.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdUVAL.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdUVAL.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dtTemp As DataTable
        Dim blnBUI As Boolean = False
        Dim intAge As Integer


        If Not IsNumeric(txtAge.Text) OrElse CInt(txtAge.Text) = 0 Then
            MsgBox("Invalid age, please input again.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Exit Sub
        Else
            intAge = CInt(txtAge.Text)
        End If

        If txtPlan.Text = "" OrElse txtRS.Text = "" OrElse txtSmoker.Text = "" OrElse txtPAR.Text = "" OrElse _
                txtSEX.Text = "" OrElse cboRecType.SelectedItem = "" Then
            MsgBox("Please input required paremeter first.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor
        'dt(0) = objCS.GetUVAL("AL", "1", "N", "P", "M", "", "", 10, "CSV", lngErrNo, strErrMsg)
        dtTemp = objCS.GetUVAL(Trim(txtPlan.Text), Trim(txtRS.Text), Trim(txtSmoker.Text), Trim(txtPAR.Text), _
            Trim(txtSEX.Text), Trim(txtTBL1.Text), Trim(txtTBL2.Text), intAge, Trim(cboRecType.SelectedItem), lngErrNo, strErrMsg)
        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If dtTemp Is Nothing Then
                MsgBox("Record not found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Else

                If dtTemp.Rows.Count = 0 Then
                    MsgBox("Record not found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
                    Exit Sub
                End If

                If dtResult Is Nothing Then
                    blnBUI = True

                    dtResult = New DataTable("UVAL")
                    dtResult.Columns.Add("Duration", Type.GetType("System.UInt16"))
                    dtResult.Columns.Add("ValPerUnit", Type.GetType("System.Decimal"))

                End If

                dtResult.Rows.Clear()

                Dim strFld As String
                Dim intIndex As String = 16
                Dim dr As DataRow

                For i As Integer = dtTemp.Rows(0).Item("FLD0012") To dtTemp.Rows(0).Item("FLD0013")
                    strFld = "FLD" + Microsoft.VisualBasic.Strings.Right(10000 + intIndex, 4)
                    intIndex += 1
                    dr = dtResult.NewRow
                    dr.Item("Duration") = i
                    dr.Item("ValPerUnit") = dtTemp.Rows(0).Item(strFld)
                    dtResult.Rows.Add(dr)
                Next

                If blnBUI Then Call BuildUI()

            End If
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

    End Sub

End Class
