Public Class frmBANK
    Inherits System.Windows.Forms.Form

    Dim dtResult, dtResult1 As DataTable
    Dim WithEvents bm As BindingManagerBase

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
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents grdBANK As System.Windows.Forms.DataGrid
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents txtBranch As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents grdBranch As System.Windows.Forms.DataGrid
    Friend WithEvents Label3 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtBank = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBranch = New System.Windows.Forms.TextBox
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.grdBANK = New System.Windows.Forms.DataGrid
        Me.txtName = New System.Windows.Forms.TextBox
        Me.grdBranch = New System.Windows.Forms.DataGrid
        Me.Label3 = New System.Windows.Forms.Label
        CType(Me.grdBANK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtBank
        '
        Me.txtBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBank.Location = New System.Drawing.Point(44, 8)
        Me.txtBank.MaxLength = 0
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(36, 20)
        Me.txtBank.TabIndex = 0
        Me.txtBank.Text = "000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Bank"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(104, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Branch"
        '
        'txtBranch
        '
        Me.txtBranch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBranch.Location = New System.Drawing.Point(148, 8)
        Me.txtBranch.MaxLength = 0
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.Size = New System.Drawing.Size(32, 20)
        Me.txtBranch.TabIndex = 2
        Me.txtBranch.Text = ""
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(492, 8)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(52, 20)
        Me.cmdSearch.TabIndex = 19
        Me.cmdSearch.Text = "Search"
        '
        'grdBANK
        '
        Me.grdBANK.AlternatingBackColor = System.Drawing.Color.White
        Me.grdBANK.BackColor = System.Drawing.Color.White
        Me.grdBANK.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdBANK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdBANK.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBANK.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdBANK.CaptionVisible = False
        Me.grdBANK.DataMember = ""
        Me.grdBANK.FlatMode = True
        Me.grdBANK.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdBANK.ForeColor = System.Drawing.Color.Black
        Me.grdBANK.GridLineColor = System.Drawing.Color.Wheat
        Me.grdBANK.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdBANK.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdBANK.HeaderForeColor = System.Drawing.Color.Black
        Me.grdBANK.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBANK.Location = New System.Drawing.Point(8, 56)
        Me.grdBANK.Name = "grdBANK"
        Me.grdBANK.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdBANK.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdBANK.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdBANK.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBANK.Size = New System.Drawing.Size(536, 176)
        Me.grdBANK.TabIndex = 20
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(188, 8)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(292, 20)
        Me.txtName.TabIndex = 21
        Me.txtName.Text = ""
        '
        'grdBranch
        '
        Me.grdBranch.AlternatingBackColor = System.Drawing.Color.White
        Me.grdBranch.BackColor = System.Drawing.Color.White
        Me.grdBranch.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdBranch.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBranch.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdBranch.CaptionVisible = False
        Me.grdBranch.DataMember = ""
        Me.grdBranch.FlatMode = True
        Me.grdBranch.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdBranch.ForeColor = System.Drawing.Color.Black
        Me.grdBranch.GridLineColor = System.Drawing.Color.Wheat
        Me.grdBranch.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdBranch.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdBranch.HeaderForeColor = System.Drawing.Color.Black
        Me.grdBranch.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBranch.Location = New System.Drawing.Point(8, 236)
        Me.grdBranch.Name = "grdBranch"
        Me.grdBranch.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdBranch.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdBranch.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdBranch.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBranch.Size = New System.Drawing.Size(536, 176)
        Me.grdBranch.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(192, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 16)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "'000' to list all banks"
        '
        'frmBANK
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(556, 417)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.grdBranch)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.grdBANK)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBranch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBank)
        Me.Name = "frmBANK"
        Me.Text = "Bank / Branch"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdBANK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub BuildUI()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "Code"
        cs.HeaderText = "Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 300
        cs.MappingName = "Name"
        cs.HeaderText = "Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "BANK"
        grdBANK.TableStyles.Add(ts)

        bm = Me.BindingContext(dtResult)

        grdBANK.DataSource = dtResult
        grdBANK.AllowDrop = False
        grdBANK.ReadOnly = True

    End Sub

    Private Sub BuildUI1()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "Code"
        cs.HeaderText = "Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 300
        cs.MappingName = "Name"
        cs.HeaderText = "Name"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "BFIND"
        cs.HeaderText = "C/A"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "BRANCH"
        grdBranch.TableStyles.Add(ts)

        grdBranch.DataSource = dtResult1
        grdBranch.AllowDrop = False
        grdBranch.ReadOnly = True

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dtTemp As DataTable
        Dim blnBUI, blnBUI1 As Boolean
        Dim strBank, strBrch As String

        If txtBank.Text = "" Then
            strBank = "000"
        Else
            strBank = Trim(txtBank.Text)
        End If

        strBrch = Trim(txtBranch.Text)

        wndMain.Cursor = Cursors.WaitCursor

        dtTemp = objCS.GetBANK(strBank, strBrch, lngErrNo, strErrMsg)

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If dtTemp Is Nothing Then
                MsgBox("Record not found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
            Else

                If strBank = "" Or strBank = "000" Then
                    If dtResult Is Nothing Then
                        blnBUI = True
                        dtResult = dtTemp.Copy
                        dtResult.TableName = "BANK"
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

                    If Not dtResult1 Is Nothing Then
                        dtResult1.Rows.Clear()
                    End If

                    Me.txtName.Text = ""

                Else
                    If dtResult1 Is Nothing Then
                        blnBUI1 = True
                        dtResult1 = dtTemp.Copy
                        dtResult1.TableName = "BRANCH"
                    End If

                    If blnBUI1 Then
                        Call BuildUI1()
                    Else
                        dtResult1.Rows.Clear()

                        Dim dr As DataRow
                        Dim ar() As Object
                        For Each dr In dtTemp.Rows
                            ar = dr.ItemArray
                            dtResult1.Rows.Add(ar)
                        Next
                    End If

                    If Not dtTemp Is Nothing Then
                        If dtTemp.Rows.Count > 0 Then
                            Me.txtName.Text = dtTemp.Rows(0).Item("BFBKNM")
                        End If
                    End If

                End If

                If dtTemp.Rows.Count = 0 Then
                    MsgBox("Record not found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Enquiry")
                End If

            End If
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End If

    End Sub

    Private Sub grdBANK_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdBANK.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdBANK.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdBANK.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdBANK.Select(hti.Row)
        End If
    End Sub

    Private Sub grdBRANCH_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdBranch.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdBranch.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdBranch.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdBranch.Select(hti.Row)
        End If
    End Sub

    Private Sub grdBANK_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBANK.CurrentCellChanged

        Dim drI As DataRow = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            txtBank.Text = drI.Item("Code")
            txtName.Text = drI.Item("Name")
        End If

    End Sub

End Class
