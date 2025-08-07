Imports System.Threading

Public Class frmFax
    Inherits System.Windows.Forms.Form

    Dim strFaxDoc As String
    Dim strDocName As String

    Public WriteOnly Property FaxDoc(ByVal strName As String) As String
        Set(ByVal Value As String)
            strFaxDoc = Value
            strDocName = strName
        End Set
    End Property

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
    Friend WithEvents cmdFax As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFaxNo As System.Windows.Forms.TextBox
    Friend WithEvents chkCover As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCO As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSubj As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtUsrID As System.Windows.Forms.TextBox
    Friend WithEvents lblFaxing As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdFax = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFaxNo = New System.Windows.Forms.TextBox
        Me.chkCover = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCO = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTo = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtSubj = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtUsrID = New System.Windows.Forms.TextBox
        Me.lblFaxing = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdFax
        '
        Me.cmdFax.Location = New System.Drawing.Point(100, 188)
        Me.cmdFax.Name = "cmdFax"
        Me.cmdFax.TabIndex = 6
        Me.cmdFax.Text = "Fax"
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(180, 188)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Fax No."
        '
        'txtFaxNo
        '
        Me.txtFaxNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFaxNo.Location = New System.Drawing.Point(68, 64)
        Me.txtFaxNo.MaxLength = 0
        Me.txtFaxNo.Name = "txtFaxNo"
        Me.txtFaxNo.Size = New System.Drawing.Size(116, 20)
        Me.txtFaxNo.TabIndex = 3
        Me.txtFaxNo.Text = ""
        '
        'chkCover
        '
        Me.chkCover.Checked = True
        Me.chkCover.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCover.Location = New System.Drawing.Point(68, 160)
        Me.chkCover.Name = "chkCover"
        Me.chkCover.TabIndex = 5
        Me.chkCover.Text = "Fax Cover?"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Co./Dept."
        '
        'txtCO
        '
        Me.txtCO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCO.Location = New System.Drawing.Point(68, 40)
        Me.txtCO.MaxLength = 0
        Me.txtCO.Name = "txtCO"
        Me.txtCO.Size = New System.Drawing.Size(276, 20)
        Me.txtCO.TabIndex = 2
        Me.txtCO.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "To:"
        '
        'txtTo
        '
        Me.txtTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTo.Location = New System.Drawing.Point(68, 16)
        Me.txtTo.MaxLength = 0
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(220, 20)
        Me.txtTo.TabIndex = 1
        Me.txtTo.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Subject"
        '
        'txtSubj
        '
        Me.txtSubj.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubj.Location = New System.Drawing.Point(68, 88)
        Me.txtSubj.MaxLength = 0
        Me.txtSubj.Name = "txtSubj"
        Me.txtSubj.Size = New System.Drawing.Size(276, 20)
        Me.txtSubj.TabIndex = 4
        Me.txtSubj.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 116)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 16)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "User ID"
        '
        'txtUsrID
        '
        Me.txtUsrID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUsrID.Location = New System.Drawing.Point(68, 112)
        Me.txtUsrID.MaxLength = 0
        Me.txtUsrID.Name = "txtUsrID"
        Me.txtUsrID.Size = New System.Drawing.Size(116, 20)
        Me.txtUsrID.TabIndex = 13
        Me.txtUsrID.Text = ""
        '
        'lblFaxing
        '
        Me.lblFaxing.ForeColor = System.Drawing.Color.Blue
        Me.lblFaxing.Location = New System.Drawing.Point(68, 140)
        Me.lblFaxing.Name = "lblFaxing"
        Me.lblFaxing.Size = New System.Drawing.Size(184, 23)
        Me.lblFaxing.TabIndex = 15
        Me.lblFaxing.Text = "FAX in progress, please wait!"
        Me.lblFaxing.Visible = False
        '
        'frmFax
        '
        Me.AcceptButton = Me.cmdFax
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(360, 217)
        Me.Controls.Add(Me.lblFaxing)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtUsrID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtSubj)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCO)
        Me.Controls.Add(Me.chkCover)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFaxNo)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdFax)
        Me.Name = "frmFax"
        Me.Text = "Fax Document"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFax.Click

        Dim strDocPath, strCMD As String
        Dim k As Integer
        Dim blnFaxSuccess As Boolean

        strDocPath = Application.StartupPath

        If Me.txtFaxNo.Text = "" OrElse gFaxSrv = "" OrElse Me.txtUsrID.Text = "" Then
            MsgBox("No Fax Produce because Fax No. / Server is missing.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Reports")
            Exit Sub
        End If

        If Dir(strDocPath & "\submitfax.exe") = "" Then
            MsgBox("No Fax Produce because a system file is missing.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Reports")
            Exit Sub
        End If

        If Dir(strDocPath & "\outgoingfax", vbDirectory) = "" Then
            MkDir(strDocPath & "\outgoingfax")
        End If

        ' Remove FAXPRESS log files
        If Dir(strDocPath & "\outgoingfax\*.que") <> "" Then
            Kill(strDocPath & "\outgoingfax\*.que")
        End If

        If Dir(strDocPath & "\outgoingfax\*.job") <> "" Then
            Kill(strDocPath & "\outgoingfax\*.job")
        End If

        If Dir(strDocPath & "\outgoingfax\*.err") <> "" Then
            Kill(strDocPath & "\outgoingfax\*.err")
        End If

        If Dir(strDocPath & "\outgoingfax\*.ok") <> "" Then
            Kill(strDocPath & "\outgoingfax\*.ok")
        End If

        Me.txtCO.ReadOnly = True
        Me.txtFaxNo.ReadOnly = True
        Me.txtSubj.ReadOnly = True
        Me.txtUsrID.ReadOnly = True
        Me.txtTo.ReadOnly = True
        Me.chkCover.Enabled = False
        Me.cmdFax.Enabled = False
        Me.cmdCancel.Enabled = False

        strCMD = "SUBMITFAX.EXE /S " & gFaxSrv

        strCMD &= " /U " & Trim(Me.txtUsrID.Text) & " "

        strCMD &= " /A " & strFaxDoc
        strCMD &= " /R " & Trim(txtTo.Text) & "@" & Trim(txtCO.Text) & "@" & Trim(txtFaxNo.Text)

        If chkCover.Checked = False Then
            strCMD &= " /C NO COVER PAGE"
        End If

        strCMD &= " /B " & txtSubj.Text
        strCMD &= " /O " & strDocPath & "\outgoingfax /XFULL"

        lblFaxing.Visible = True

        Shell(strCMD, vbHide)
        k = 0

        wndMain.Cursor = Cursors.WaitCursor

        Do While k <= gFAXSLEEP
            Thread.Sleep(500)
            k = k + 500
            If Dir(strDocPath & "\outgoingfax\*.ok") <> "" Then
                blnFaxSuccess = True
                Exit Do
            End If
            If Dir(strDocPath & "\outgoingfax\*.err") <> "" Then
                blnFaxSuccess = False
                Exit Do
            End If
            Application.DoEvents()

            If k >= gFAXSLEEP Then
                If MsgBox("Timeout waiting for FAX result, click 'Retry' to continue to wait.", MsgBoxStyle.Information + MsgBoxStyle.RetryCancel.RetryCancel, "Fax Document") = MsgBoxResult.Retry Then
                    k = 0
                Else
                    Exit Do
                End If
            End If
        Loop

        wndMain.Cursor = Cursors.Default

        If blnFaxSuccess Then
            MsgBox("Document Fax to customer successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Reports")
        Else
            MsgBox("There is a problem faxing the document to customer.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Reports")
        End If

        Me.Close()

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmFax_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSubj.Text = strDocName
        txtUsrID.Text = gsUser
    End Sub

End Class
