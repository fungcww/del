Public Class frmChooseEmail
    Inherits System.Windows.Forms.Form

    Private oriList As New DataTable
    Private addList As New DataTable
    Private unSelList As New DataTable

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
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdAddAll As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveAll As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents lstUnselected As System.Windows.Forms.ListBox
    Friend WithEvents lstPending As System.Windows.Forms.ListBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdValidate As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstUnselected = New System.Windows.Forms.ListBox
        Me.lstPending = New System.Windows.Forms.ListBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdAddAll = New System.Windows.Forms.Button
        Me.cmdRemoveAll = New System.Windows.Forms.Button
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdValidate = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lstUnselected
        '
        Me.lstUnselected.Items.AddRange(New Object() {"eric.tm.shu@ing.com.hk", "johnny.a.zhang@ing.com.hk", "jason.kh.iu@ing.com.hk"})
        Me.lstUnselected.Location = New System.Drawing.Point(8, 4)
        Me.lstUnselected.Name = "lstUnselected"
        Me.lstUnselected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstUnselected.Size = New System.Drawing.Size(160, 199)
        Me.lstUnselected.TabIndex = 0
        '
        'lstPending
        '
        Me.lstPending.Location = New System.Drawing.Point(236, 4)
        Me.lstPending.Name = "lstPending"
        Me.lstPending.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstPending.Size = New System.Drawing.Size(160, 199)
        Me.lstPending.TabIndex = 1
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(180, 20)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(44, 23)
        Me.cmdAdd.TabIndex = 2
        Me.cmdAdd.Text = ">"
        '
        'cmdAddAll
        '
        Me.cmdAddAll.Location = New System.Drawing.Point(180, 52)
        Me.cmdAddAll.Name = "cmdAddAll"
        Me.cmdAddAll.Size = New System.Drawing.Size(44, 23)
        Me.cmdAddAll.TabIndex = 3
        Me.cmdAddAll.Text = ">>"
        '
        'cmdRemoveAll
        '
        Me.cmdRemoveAll.Location = New System.Drawing.Point(180, 84)
        Me.cmdRemoveAll.Name = "cmdRemoveAll"
        Me.cmdRemoveAll.Size = New System.Drawing.Size(44, 23)
        Me.cmdRemoveAll.TabIndex = 4
        Me.cmdRemoveAll.Text = "<<"
        '
        'cmdRemove
        '
        Me.cmdRemove.Location = New System.Drawing.Point(180, 116)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(44, 23)
        Me.cmdRemove.TabIndex = 5
        Me.cmdRemove.Text = "<"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(232, 224)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.TabIndex = 6
        Me.cmdSave.Text = "OK"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(316, 224)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdValidate
        '
        Me.cmdValidate.Location = New System.Drawing.Point(12, 224)
        Me.cmdValidate.Name = "cmdValidate"
        Me.cmdValidate.Size = New System.Drawing.Size(88, 23)
        Me.cmdValidate.TabIndex = 8
        Me.cmdValidate.Text = "Validate Email"
        '
        'frmChooseEmail
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(404, 253)
        Me.Controls.Add(Me.cmdValidate)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdRemoveAll)
        Me.Controls.Add(Me.cmdAddAll)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.lstPending)
        Me.Controls.Add(Me.lstUnselected)
        Me.Name = "frmChooseEmail"
        Me.Text = "Choose Email"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property OriUnSel() As DataTable
        Get
            Return oriList
        End Get
        Set(ByVal Value As DataTable)
            oriList = Value
        End Set
    End Property

    Public Property OriAdded() As DataTable
        Get
            Return addList
        End Get
        Set(ByVal Value As DataTable)
            addList = Value
        End Set
    End Property    

    Public Property UnSel() As DataTable
        Get
            Return unSelList
        End Get
        Set(ByVal Value As DataTable)
            unSelList = Value
        End Set
    End Property

    Private Sub formLists()
        'form the two lists
        Dim key(1) As DataColumn
        key(0) = oriList.Columns(3)
        oriList.PrimaryKey = key

        unSelList = New DataTable
        unSelList.Columns.Add("emailaddr", Type.GetType("System.String"))
        unSelList.Columns.Add("nameprefix", Type.GetType("System.String"))
        unSelList.Columns.Add("namesuffix", Type.GetType("System.String"))
        unSelList.Columns.Add("customerid", Type.GetType("System.String"))
        unSelList.Columns.Add("ChiName", Type.GetType("System.String"))
        unSelList.Columns.Add("crmcsl_mail_sent", Type.GetType("System.String"))
        unSelList.Columns.Add("crmcsl_seq", Type.GetType("System.UInt16"))

        Dim ukey(1) As DataColumn
        ukey(0) = unSelList.Columns(3)
        unSelList.PrimaryKey = ukey

        For Each row As DataRow In OriUnSel.Rows
            If OriAdded.Rows.Count <> 0 Then
                Dim find As String
                find = row.Item(3)

                If addList.Rows.Find(find) Is Nothing Then
                    Dim nr As DataRow
                    nr = unSelList.NewRow
                    nr.Item(0) = row.Item(0)
                    nr.Item(1) = row.Item(1)
                    nr.Item(2) = row.Item(2)
                    nr.Item(3) = row.Item(3)
                    nr.Item(4) = row.Item(4)
                    nr.Item(5) = row.Item(5)
                    nr.Item(6) = row.Item(6)
                    unSelList.Rows.Add(nr)
                End If

            Else
                unSelList = OriUnSel
                Exit For
            End If
        Next
    End Sub

    Private Sub frmChooseEmail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        formLists()
        bindUnSelList()
        bindPendList()
    End Sub

    Public Sub bindUnSelList()
        'bind the unselect list
        Me.lstUnselected.Items.Clear()

        For i As Integer = 0 To unSelList.Rows.Count - 1
            Me.lstUnselected.Items.Add(unSelList.Rows(i).Item(0).ToString)
        Next
    End Sub

    Public Sub bindPendList()
        'bind the selected list
        Me.lstPending.Items.Clear()

        For i As Integer = 0 To addList.Rows.Count - 1
            Me.lstPending.Items.Add(addList.Rows(i).Item(0).ToString)
        Next
    End Sub

    Private Sub addRemove(ByRef list As ListBox, ByRef fromdt As DataTable, ByRef todt As DataTable)
        Dim key(list.SelectedIndices.Count) As String
        For i As Integer = 0 To list.SelectedIndices.Count - 1

            Dim nr As DataRow
            nr = todt.NewRow
            nr.Item(0) = fromdt.Rows(list.SelectedIndices(i)).Item(0)
            nr.Item(1) = fromdt.Rows(list.SelectedIndices(i)).Item(1)
            nr.Item(2) = fromdt.Rows(list.SelectedIndices(i)).Item(2)
            nr.Item(3) = fromdt.Rows(list.SelectedIndices(i)).Item(3)
            nr.Item(4) = fromdt.Rows(list.SelectedIndices(i)).Item(4)
            nr.Item(5) = fromdt.Rows(list.SelectedIndices(i)).Item(5)
            nr.Item(6) = fromdt.Rows(list.SelectedIndices(i)).Item(6)
            key(i) = nr.Item(3)
            todt.Rows.Add(nr)

        Next
        For i As Integer = 0 To key.GetUpperBound(0) - 1
            fromdt.Rows.Remove(fromdt.Rows.Find(key(i)))
        Next
    End Sub

    Private Sub addRemoveAll(ByRef fromdt As DataTable, ByRef todt As DataTable)
        Dim key(fromdt.Rows.Count) As String
        For i As Integer = 0 To fromdt.Rows.Count - 1

            Dim nr As DataRow
            nr = todt.NewRow
            nr.Item(0) = fromdt.Rows(i).Item(0)
            nr.Item(1) = fromdt.Rows(i).Item(1)
            nr.Item(2) = fromdt.Rows(i).Item(2)
            nr.Item(3) = fromdt.Rows(i).Item(3)
            nr.Item(4) = fromdt.Rows(i).Item(4)
            nr.Item(5) = fromdt.Rows(i).Item(5)
            nr.Item(6) = fromdt.Rows(i).Item(6)
            key(i) = nr.Item(3)
            todt.Rows.Add(nr)

        Next
        For i As Integer = 0 To key.GetUpperBound(0) - 1
            fromdt.Rows.Remove(fromdt.Rows.Find(key(i)))
        Next
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        addRemove(lstUnselected, unSelList, addList)
        bindUnSelList()
        bindPendList()
    End Sub

    Private Sub cmdAddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAll.Click
        addRemoveAll(unSelList, addList)
        bindPendList()
        bindUnSelList()

    End Sub

    Private Sub cmdRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveAll.Click
        addRemoveAll(addList, unSelList)
        bindUnSelList()
        bindPendList()

    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        addRemove(lstPending, addList, unSelList)
        bindPendList()
        bindUnSelList()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdValidate.Click

        Dim strEmail As String
        Dim blnValid As Boolean

        For i As Integer = 0 To addList.Rows.Count - 1
            strEmail = Trim(addList.Rows(i).Item(0))
            Do
                blnValid = System.Text.RegularExpressions.Regex.IsMatch(strEmail, "^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If blnValid = False Then
                    strEmail = InputBox("Invalid email address " & strEmail & ", please correct it", "Email Validation", strEmail)

                    If strEmail = "" Then
                        MsgBox("Validation cancelled", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
                        Exit For
                    End If

                End If
            Loop While Not blnValid

            addList.Rows(i).Item(0) = strEmail
            addList.AcceptChanges()
        Next

        bindPendList()

    End Sub

End Class
