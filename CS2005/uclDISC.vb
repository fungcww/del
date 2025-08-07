Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class DISC
    Inherits System.Windows.Forms.UserControl

    Dim strPolicy As String
    Dim dtDISC As DataTable

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
    Friend WithEvents grdDISC As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cboCov As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdDISC = New System.Windows.Forms.DataGrid
        Me.cboCov = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.grdDISC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdDISC
        '
        Me.grdDISC.AlternatingBackColor = System.Drawing.Color.White
        Me.grdDISC.BackColor = System.Drawing.Color.White
        Me.grdDISC.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdDISC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdDISC.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdDISC.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdDISC.CaptionVisible = False
        Me.grdDISC.DataMember = ""
        Me.grdDISC.FlatMode = True
        Me.grdDISC.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdDISC.ForeColor = System.Drawing.Color.Black
        Me.grdDISC.GridLineColor = System.Drawing.Color.Wheat
        Me.grdDISC.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdDISC.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdDISC.HeaderForeColor = System.Drawing.Color.Black
        Me.grdDISC.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdDISC.Location = New System.Drawing.Point(4, 32)
        Me.grdDISC.Name = "grdDISC"
        Me.grdDISC.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdDISC.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdDISC.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdDISC.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdDISC.Size = New System.Drawing.Size(560, 144)
        Me.grdDISC.TabIndex = 1
        '
        'cboCov
        '
        Me.cboCov.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCov.Location = New System.Drawing.Point(64, 4)
        Me.cboCov.Name = "cboCov"
        Me.cboCov.Size = New System.Drawing.Size(96, 21)
        Me.cboCov.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Coverage"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(175, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(31, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Go"
        '
        'DISC
        '
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCov)
        Me.Controls.Add(Me.grdDISC)
        Me.Name = "DISC"
        Me.Size = New System.Drawing.Size(572, 184)
        CType(Me.grdDISC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property PolicyAccountID() As String
        Set(ByVal Value As String)
            strPolicy = Value
        End Set
    End Property

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
        cs.Width = 120
        cs.MappingName = "HospitalInDate"
        cs.HeaderText = "Hospital In Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "DateRange"
        cs.HeaderText = "Date Range"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "DiscFlag"
        cs.HeaderText = "Discount Flag"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "UserID"
        cs.HeaderText = "User"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "DISC"
        grdDISC.TableStyles.Add(ts)

        grdDISC.DataSource = dtDISC
        grdDISC.AllowDrop = False
        grdDISC.ReadOnly = True

    End Sub

    Private Sub GetDISCRecord()

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dtCov As DataTable

        wndMain.Cursor = Cursors.WaitCursor

        dtCov = objCS.GetDISC(strPolicy, "", lngErrNo, strErrMsg)

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If dtCov Is Nothing Then
                MsgBox("No DISC Record found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "DISC")
            Else
                If dtCov.Rows.Count > 0 Then
                    For i As Integer = 0 To dtCov.Rows.Count - 1
                        cboCov.Items.Add(dtCov.Rows(i).Item(0) & " - " & dtCov.Rows(i).Item(1))
                    Next
                    cboCov.SelectedIndex = 0
                End If
            End If
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

    End Sub

    Private Sub grdDISC_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDISC.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdDISC.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdDISC.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdDISC.Select(hti.Row)
        End If
    End Sub

    Private Sub cboCov_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCov.SelectedIndexChanged

        Dim strErrMsg As String
        Dim lngErrNo As Long
        Dim dtDISCtemp As DataTable
        Dim blnBUI As Boolean

        wndMain.Cursor = Cursors.WaitCursor

        dtDISCtemp = objCS.GetDISC(strPolicy, Microsoft.VisualBasic.Strings.Left(cboCov.SelectedItem, 2), lngErrNo, strErrMsg)

        wndMain.Cursor = Cursors.Default

        If lngErrNo = 0 Then
            If Not dtDISCtemp Is Nothing Then
                ' If strCov is empty, just fill the Coverage combobox, else, fill the grid
                'If dtDISCtemp.Rows.Count > 0 Then

                If dtDISC Is Nothing Then
                    blnBUI = True
                    dtDISC = New DataTable
                    dtDISC = dtDISCtemp.Clone
                    dtDISC.TableName = "DISC"
                End If

                dtDISC.Rows.Clear()

                Dim dr As DataRow
                Dim ar() As Object
                For Each dr In dtDISCtemp.Rows
                    ar = dr.ItemArray
                    If ar(1) = #1/1/1900# Then ar(1) = System.DBNull.Value
                    dtDISC.Rows.Add(ar)
                Next

                If blnBUI Then Call BuildUI()
                'Else
                '    MsgBox("No DISC Record found", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "DISC")
                'End If
            Else
                MsgBox("No DISC Record found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "DISC")
            End If
        Else
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cboCov.Items.Clear()
        Call GetDISCRecord()
    End Sub
End Class
