Imports System.Data.SqlClient

Public Class frmDelMiscBatch
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lvBatch As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.lvBatch = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(141, 168)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Delete"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(229, 168)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Cancel"
        '
        'lvBatch
        '
        Me.lvBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvBatch.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvBatch.FullRowSelect = True
        Me.lvBatch.HideSelection = False
        Me.lvBatch.Location = New System.Drawing.Point(0, 0)
        Me.lvBatch.MultiSelect = False
        Me.lvBatch.Name = "lvBatch"
        Me.lvBatch.Size = New System.Drawing.Size(444, 160)
        Me.lvBatch.TabIndex = 3
        Me.lvBatch.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Batch No."
        Me.ColumnHeader1.Width = 77
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Source"
        Me.ColumnHeader2.Width = 184
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "User "
        Me.ColumnHeader3.Width = 68
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Date"
        Me.ColumnHeader4.Width = 86
        '
        'frmDelMiscBatch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(444, 197)
        Me.Controls.Add(Me.lvBatch)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmDelMiscBatch"
        Me.Text = "Delete Misc. Information Batch"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDelMiscBatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call LoadRecord()

    End Sub

    Private Function LoadRecord() As DataTable

        Dim sqlCmd As New SqlCommand
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim strsql As String
        Dim sqldt As New DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        ' Select batch
        strsql = "Select cswmib_id, cswmib_desc, cswmib_user, cswmib_date " &
            "From " & serverPrefix & "csw_misc_info_batch " &
            "Where cswmib_delete <> 'Y'"

        sqlconnect.ConnectionString = strCIWConn
        sqlda = New SqlDataAdapter(strsql, sqlconnect)

        Try
            sqlda.Fill(sqldt)
        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
        End Try

        lvBatch.Items.Clear()
        Me.lvBatch.Columns.Add("Batch", -2, HorizontalAlignment.Left)
        Me.lvBatch.Columns.Add("Source", -2, HorizontalAlignment.Left)
        Me.lvBatch.Columns.Add("User", -2, HorizontalAlignment.Left)
        Me.lvBatch.Columns.Add("Date", -2, HorizontalAlignment.Left)

        If Not sqldt Is Nothing Then
            If sqldt.Rows.Count > 0 Then
                For Each row As DataRow In sqldt.Rows
                    Dim vItem As New ListViewItem(row.Item(0).ToString)
                    vItem.SubItems.Add(row.Item(1).ToString)
                    vItem.SubItems.Add(row.Item(2).ToString)
                    vItem.SubItems.Add(row.Item(3).ToString)
                    lvBatch.Items.Add(vItem)
                Next
            End If
        End If

    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim sqlconnect As SqlConnection

        If lvBatch.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        If MsgBox("Are you sure to delete the selected record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gSystem) = MsgBoxResult.Yes Then

            Dim sqlDEL, strSQL As String
            Dim sqlCmd As New SqlCommand
            Dim intCnt As Integer
            Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

            sqlconnect = New SqlConnection
            sqlconnect.ConnectionString = strCIWConn
            sqlconnect.Open()

            sqlDEL = "delete from " & serverPrefix & "csw_misc_info " &
                " Where cswmif_batch = " & lvBatch.SelectedItems(0).Text &
                " Update " & serverPrefix & "csw_misc_info_batch " &
                " Set cswmib_delete = 'Y' " &
                " Where cswmib_id = " & lvBatch.SelectedItems(0).Text

            Try
                sqlCmd.Connection = sqlconnect
                sqlCmd.CommandText = sqlDEL
                sqlCmd.ExecuteNonQuery()
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Finally
                sqlconnect.Close()
                sqlconnect.Dispose()
            End Try

            Call LoadRecord()

            MsgBox("Record deleted successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)

        End If

    End Sub
End Class
