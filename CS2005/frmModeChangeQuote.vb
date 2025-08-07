Imports System.data.SqlClient
Imports System.Data.OleDb

Public Class frmModeChangeQuote
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtAnnually As System.Windows.Forms.Label
    Friend WithEvents txtMP_M As System.Windows.Forms.TextBox
    Friend WithEvents txtMP_H As System.Windows.Forms.TextBox
    Friend WithEvents txtMP_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtDD_M As System.Windows.Forms.TextBox
    Friend WithEvents txtDD_H As System.Windows.Forms.TextBox
    Friend WithEvents txtDD_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtPD_M As System.Windows.Forms.TextBox
    Friend WithEvents txtPD_H As System.Windows.Forms.TextBox
    Friend WithEvents txtPD_Y As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDP_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtDP_H As System.Windows.Forms.TextBox
    Friend WithEvents txtRF_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtRF_H As System.Windows.Forms.TextBox
    Friend WithEvents txtSA_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtSA_H As System.Windows.Forms.TextBox
    Friend WithEvents txtRF_M As System.Windows.Forms.TextBox
    Friend WithEvents txtSA_M As System.Windows.Forms.TextBox
    Friend WithEvents txtDP_M As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositDT As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMF_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtMF_H As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtLevy_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtLevy_H As System.Windows.Forms.TextBox
    Friend WithEvents txtLevy_M As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtChangeModePrem_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtChangeModePrem_H As System.Windows.Forms.TextBox
    Friend WithEvents txtChangeModePrem_M As System.Windows.Forms.TextBox
    Friend WithEvents txtMF_M As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtAnnually = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtMP_M = New System.Windows.Forms.TextBox
        Me.txtMP_H = New System.Windows.Forms.TextBox
        Me.txtMP_Y = New System.Windows.Forms.TextBox
        Me.txtDD_M = New System.Windows.Forms.TextBox
        Me.txtDD_H = New System.Windows.Forms.TextBox
        Me.txtDD_Y = New System.Windows.Forms.TextBox
        Me.txtPD_M = New System.Windows.Forms.TextBox
        Me.txtPD_H = New System.Windows.Forms.TextBox
        Me.txtPD_Y = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtDepositDT = New System.Windows.Forms.TextBox
        Me.txtDP_M = New System.Windows.Forms.TextBox
        Me.txtSA_M = New System.Windows.Forms.TextBox
        Me.txtRF_M = New System.Windows.Forms.TextBox
        Me.txtSA_H = New System.Windows.Forms.TextBox
        Me.txtSA_Y = New System.Windows.Forms.TextBox
        Me.txtRF_H = New System.Windows.Forms.TextBox
        Me.txtRF_Y = New System.Windows.Forms.TextBox
        Me.txtDP_H = New System.Windows.Forms.TextBox
        Me.txtDP_Y = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtMF_Y = New System.Windows.Forms.TextBox
        Me.txtMF_H = New System.Windows.Forms.TextBox
        Me.txtMF_M = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtLevy_Y = New System.Windows.Forms.TextBox
        Me.txtLevy_H = New System.Windows.Forms.TextBox
        Me.txtLevy_M = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtChangeModePrem_Y = New System.Windows.Forms.TextBox
        Me.txtChangeModePrem_H = New System.Windows.Forms.TextBox
        Me.txtChangeModePrem_M = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Half-Annually"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(288, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "If change the payment mode, the modal premium should be:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Monthly"
        '
        'txtAnnually
        '
        Me.txtAnnually.AutoSize = True
        Me.txtAnnually.Location = New System.Drawing.Point(8, 128)
        Me.txtAnnually.Name = "txtAnnually"
        Me.txtAnnually.Size = New System.Drawing.Size(47, 13)
        Me.txtAnnually.TabIndex = 4
        Me.txtAnnually.Text = "Annually"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(237, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Next Due Date"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(320, 40)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 28)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Premium to due date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMP_M
        '
        Me.txtMP_M.BackColor = System.Drawing.SystemColors.Window
        Me.txtMP_M.Location = New System.Drawing.Point(152, 68)
        Me.txtMP_M.Name = "txtMP_M"
        Me.txtMP_M.ReadOnly = True
        Me.txtMP_M.Size = New System.Drawing.Size(80, 20)
        Me.txtMP_M.TabIndex = 11
        Me.txtMP_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMP_H
        '
        Me.txtMP_H.BackColor = System.Drawing.SystemColors.Window
        Me.txtMP_H.Location = New System.Drawing.Point(152, 96)
        Me.txtMP_H.Name = "txtMP_H"
        Me.txtMP_H.ReadOnly = True
        Me.txtMP_H.Size = New System.Drawing.Size(80, 20)
        Me.txtMP_H.TabIndex = 12
        Me.txtMP_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMP_Y
        '
        Me.txtMP_Y.BackColor = System.Drawing.SystemColors.Window
        Me.txtMP_Y.Location = New System.Drawing.Point(152, 124)
        Me.txtMP_Y.Name = "txtMP_Y"
        Me.txtMP_Y.ReadOnly = True
        Me.txtMP_Y.Size = New System.Drawing.Size(80, 20)
        Me.txtMP_Y.TabIndex = 13
        Me.txtMP_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDD_M
        '
        Me.txtDD_M.BackColor = System.Drawing.SystemColors.Window
        Me.txtDD_M.Location = New System.Drawing.Point(237, 68)
        Me.txtDD_M.Name = "txtDD_M"
        Me.txtDD_M.ReadOnly = True
        Me.txtDD_M.Size = New System.Drawing.Size(76, 20)
        Me.txtDD_M.TabIndex = 14
        Me.txtDD_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDD_H
        '
        Me.txtDD_H.BackColor = System.Drawing.SystemColors.Window
        Me.txtDD_H.Location = New System.Drawing.Point(237, 96)
        Me.txtDD_H.Name = "txtDD_H"
        Me.txtDD_H.ReadOnly = True
        Me.txtDD_H.Size = New System.Drawing.Size(76, 20)
        Me.txtDD_H.TabIndex = 15
        Me.txtDD_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDD_Y
        '
        Me.txtDD_Y.BackColor = System.Drawing.SystemColors.Window
        Me.txtDD_Y.Location = New System.Drawing.Point(237, 124)
        Me.txtDD_Y.Name = "txtDD_Y"
        Me.txtDD_Y.ReadOnly = True
        Me.txtDD_Y.Size = New System.Drawing.Size(76, 20)
        Me.txtDD_Y.TabIndex = 16
        Me.txtDD_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPD_M
        '
        Me.txtPD_M.BackColor = System.Drawing.SystemColors.Window
        Me.txtPD_M.Location = New System.Drawing.Point(316, 68)
        Me.txtPD_M.Name = "txtPD_M"
        Me.txtPD_M.ReadOnly = True
        Me.txtPD_M.Size = New System.Drawing.Size(80, 20)
        Me.txtPD_M.TabIndex = 17
        Me.txtPD_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPD_H
        '
        Me.txtPD_H.BackColor = System.Drawing.SystemColors.Window
        Me.txtPD_H.Location = New System.Drawing.Point(316, 96)
        Me.txtPD_H.Name = "txtPD_H"
        Me.txtPD_H.ReadOnly = True
        Me.txtPD_H.Size = New System.Drawing.Size(80, 20)
        Me.txtPD_H.TabIndex = 18
        Me.txtPD_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPD_Y
        '
        Me.txtPD_Y.BackColor = System.Drawing.SystemColors.Window
        Me.txtPD_Y.Location = New System.Drawing.Point(316, 125)
        Me.txtPD_Y.Name = "txtPD_Y"
        Me.txtPD_Y.ReadOnly = True
        Me.txtPD_Y.Size = New System.Drawing.Size(80, 20)
        Me.txtPD_Y.TabIndex = 19
        Me.txtPD_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(152, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Modal Premium"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(484, 154)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 21
        Me.cmdClose.Text = "&Close"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDepositDT)
        Me.GroupBox1.Controls.Add(Me.txtDP_M)
        Me.GroupBox1.Controls.Add(Me.txtSA_M)
        Me.GroupBox1.Controls.Add(Me.txtRF_M)
        Me.GroupBox1.Controls.Add(Me.txtSA_H)
        Me.GroupBox1.Controls.Add(Me.txtSA_Y)
        Me.GroupBox1.Controls.Add(Me.txtRF_H)
        Me.GroupBox1.Controls.Add(Me.txtRF_Y)
        Me.GroupBox1.Controls.Add(Me.txtDP_H)
        Me.GroupBox1.Controls.Add(Me.txtDP_Y)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(581, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 148)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MC/CB/IL Policy"
        Me.GroupBox1.Visible = False
        '
        'txtDepositDT
        '
        Me.txtDepositDT.Location = New System.Drawing.Point(88, 16)
        Me.txtDepositDT.Name = "txtDepositDT"
        Me.txtDepositDT.Size = New System.Drawing.Size(76, 20)
        Me.txtDepositDT.TabIndex = 13
        '
        'txtDP_M
        '
        Me.txtDP_M.Location = New System.Drawing.Point(12, 60)
        Me.txtDP_M.Name = "txtDP_M"
        Me.txtDP_M.Size = New System.Drawing.Size(80, 20)
        Me.txtDP_M.TabIndex = 12
        Me.txtDP_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSA_M
        '
        Me.txtSA_M.Location = New System.Drawing.Point(180, 60)
        Me.txtSA_M.Name = "txtSA_M"
        Me.txtSA_M.Size = New System.Drawing.Size(80, 20)
        Me.txtSA_M.TabIndex = 11
        Me.txtSA_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRF_M
        '
        Me.txtRF_M.Location = New System.Drawing.Point(96, 60)
        Me.txtRF_M.Name = "txtRF_M"
        Me.txtRF_M.Size = New System.Drawing.Size(80, 20)
        Me.txtRF_M.TabIndex = 10
        Me.txtRF_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSA_H
        '
        Me.txtSA_H.Location = New System.Drawing.Point(180, 88)
        Me.txtSA_H.Name = "txtSA_H"
        Me.txtSA_H.Size = New System.Drawing.Size(80, 20)
        Me.txtSA_H.TabIndex = 9
        Me.txtSA_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSA_Y
        '
        Me.txtSA_Y.Location = New System.Drawing.Point(180, 116)
        Me.txtSA_Y.Name = "txtSA_Y"
        Me.txtSA_Y.Size = New System.Drawing.Size(80, 20)
        Me.txtSA_Y.TabIndex = 8
        Me.txtSA_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRF_H
        '
        Me.txtRF_H.Location = New System.Drawing.Point(96, 88)
        Me.txtRF_H.Name = "txtRF_H"
        Me.txtRF_H.Size = New System.Drawing.Size(80, 20)
        Me.txtRF_H.TabIndex = 7
        Me.txtRF_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRF_Y
        '
        Me.txtRF_Y.Location = New System.Drawing.Point(96, 116)
        Me.txtRF_Y.Name = "txtRF_Y"
        Me.txtRF_Y.Size = New System.Drawing.Size(80, 20)
        Me.txtRF_Y.TabIndex = 6
        Me.txtRF_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDP_H
        '
        Me.txtDP_H.Location = New System.Drawing.Point(12, 88)
        Me.txtDP_H.Name = "txtDP_H"
        Me.txtDP_H.Size = New System.Drawing.Size(80, 20)
        Me.txtDP_H.TabIndex = 5
        Me.txtDP_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDP_Y
        '
        Me.txtDP_Y.Location = New System.Drawing.Point(12, 116)
        Me.txtDP_Y.Name = "txtDP_Y"
        Me.txtDP_Y.Size = New System.Drawing.Size(80, 20)
        Me.txtDP_Y.TabIndex = 4
        Me.txtDP_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Deposit Date"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(104, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Regular Face"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(176, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Sundry Amount"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Deposit Amount"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(80, 52)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Modal Factor"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMF_Y
        '
        Me.txtMF_Y.BackColor = System.Drawing.SystemColors.Window
        Me.txtMF_Y.Location = New System.Drawing.Point(84, 124)
        Me.txtMF_Y.Name = "txtMF_Y"
        Me.txtMF_Y.ReadOnly = True
        Me.txtMF_Y.Size = New System.Drawing.Size(64, 20)
        Me.txtMF_Y.TabIndex = 25
        Me.txtMF_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMF_H
        '
        Me.txtMF_H.BackColor = System.Drawing.SystemColors.Window
        Me.txtMF_H.Location = New System.Drawing.Point(84, 96)
        Me.txtMF_H.Name = "txtMF_H"
        Me.txtMF_H.ReadOnly = True
        Me.txtMF_H.Size = New System.Drawing.Size(64, 20)
        Me.txtMF_H.TabIndex = 24
        Me.txtMF_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMF_M
        '
        Me.txtMF_M.BackColor = System.Drawing.SystemColors.Window
        Me.txtMF_M.Location = New System.Drawing.Point(84, 68)
        Me.txtMF_M.Name = "txtMF_M"
        Me.txtMF_M.ReadOnly = True
        Me.txtMF_M.Size = New System.Drawing.Size(64, 20)
        Me.txtMF_M.TabIndex = 23
        Me.txtMF_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(402, 39)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 26)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Levy to Due " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Date"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLevy_Y
        '
        Me.txtLevy_Y.BackColor = System.Drawing.SystemColors.Window
        Me.txtLevy_Y.Location = New System.Drawing.Point(399, 124)
        Me.txtLevy_Y.Name = "txtLevy_Y"
        Me.txtLevy_Y.ReadOnly = True
        Me.txtLevy_Y.Size = New System.Drawing.Size(80, 20)
        Me.txtLevy_Y.TabIndex = 29
        Me.txtLevy_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLevy_H
        '
        Me.txtLevy_H.BackColor = System.Drawing.SystemColors.Window
        Me.txtLevy_H.Location = New System.Drawing.Point(399, 96)
        Me.txtLevy_H.Name = "txtLevy_H"
        Me.txtLevy_H.ReadOnly = True
        Me.txtLevy_H.Size = New System.Drawing.Size(80, 20)
        Me.txtLevy_H.TabIndex = 28
        Me.txtLevy_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLevy_M
        '
        Me.txtLevy_M.BackColor = System.Drawing.SystemColors.Window
        Me.txtLevy_M.Location = New System.Drawing.Point(399, 68)
        Me.txtLevy_M.Name = "txtLevy_M"
        Me.txtLevy_M.ReadOnly = True
        Me.txtLevy_M.Size = New System.Drawing.Size(80, 20)
        Me.txtLevy_M.TabIndex = 27
        Me.txtLevy_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(485, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 26)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "Change Mode" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Premium"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtChangeModePrem_Y
        '
        Me.txtChangeModePrem_Y.BackColor = System.Drawing.SystemColors.Window
        Me.txtChangeModePrem_Y.Location = New System.Drawing.Point(485, 124)
        Me.txtChangeModePrem_Y.Name = "txtChangeModePrem_Y"
        Me.txtChangeModePrem_Y.ReadOnly = True
        Me.txtChangeModePrem_Y.Size = New System.Drawing.Size(80, 20)
        Me.txtChangeModePrem_Y.TabIndex = 34
        Me.txtChangeModePrem_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtChangeModePrem_H
        '
        Me.txtChangeModePrem_H.BackColor = System.Drawing.SystemColors.Window
        Me.txtChangeModePrem_H.Location = New System.Drawing.Point(485, 96)
        Me.txtChangeModePrem_H.Name = "txtChangeModePrem_H"
        Me.txtChangeModePrem_H.ReadOnly = True
        Me.txtChangeModePrem_H.Size = New System.Drawing.Size(80, 20)
        Me.txtChangeModePrem_H.TabIndex = 33
        Me.txtChangeModePrem_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtChangeModePrem_M
        '
        Me.txtChangeModePrem_M.BackColor = System.Drawing.SystemColors.Window
        Me.txtChangeModePrem_M.Location = New System.Drawing.Point(485, 68)
        Me.txtChangeModePrem_M.Name = "txtChangeModePrem_M"
        Me.txtChangeModePrem_M.ReadOnly = True
        Me.txtChangeModePrem_M.Size = New System.Drawing.Size(80, 20)
        Me.txtChangeModePrem_M.TabIndex = 32
        Me.txtChangeModePrem_M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmModeChangeQuote
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(569, 189)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtChangeModePrem_Y)
        Me.Controls.Add(Me.txtChangeModePrem_H)
        Me.Controls.Add(Me.txtChangeModePrem_M)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtLevy_Y)
        Me.Controls.Add(Me.txtLevy_H)
        Me.Controls.Add(Me.txtLevy_M)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtMF_Y)
        Me.Controls.Add(Me.txtMF_H)
        Me.Controls.Add(Me.txtMF_M)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPD_Y)
        Me.Controls.Add(Me.txtPD_H)
        Me.Controls.Add(Me.txtPD_M)
        Me.Controls.Add(Me.txtDD_Y)
        Me.Controls.Add(Me.txtDD_H)
        Me.Controls.Add(Me.txtDD_M)
        Me.Controls.Add(Me.txtMP_Y)
        Me.Controls.Add(Me.txtMP_H)
        Me.Controls.Add(Me.txtMP_M)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtAnnually)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmModeChangeQuote"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mode Change Quote"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private strPolicy As String
    Private dtPolInfo As DataTable
    Private lngErr As Long = 0
    Private strErr As String = ""

    Public Property PolicyAccountID(ByVal dt As DataTable) As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            strPolicy = RTrim(Value)
            dtPolInfo = dt
        End Set
    End Property

    Private Sub frmModeChangeQuote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not dtPolInfo Is Nothing Then
            If dtPolInfo.Rows.Count > 0 Then
                'Call ModeChange()
                Call ChangeModeQuote(strPolicy)
                wndMain.Cursor = Cursors.Default
            End If
        End If
    End Sub

    'Private Sub ModeChange()

    '    Dim oledbDa As OleDbDataAdapter
    '    Dim oledbConnect As New OleDbConnection

    '    Dim strSQL As String
    '    Dim dblMF_H, dblMF_M, dblMF_Cur, dblGrossAnnlPrem, dblPolicyFee, dblAnnl, dblHalfAnnl, dblMonthly As Double
    '    Dim dtMode As DataTable = New DataTable("ModeChange")

    '    ' Calculate modal premium for M, H, A respectively
    '    strSQL = " Select COPOLI, COTRAI, FEPREM, FEDURA, RMURAA, MURADU, ANLIPR, RADPRE, " & _
    '             "        RWPPRE, RAREFE, RAREMU, POCFCT, RMOPRE, FLD0047 AS MF_S, FLD0049 AS MF_M, POGPRM, RPOFEE " & _
    '             " from " & ORDUPO & ", " & ORDUCO & ", " & ORDUPH & _
    '             " where POPONO = '" & strPolicy & "' " & _
    '             " and POPONO = COPOLI " & _
    '             " and FLD0003 = RRASCL " & _
    '             " and FLD0002 = ORPLFI || ORPLRE " & _
    '             " and COTRAI = 1"
    '    Try
    '        oledbConnect.ConnectionString = strCAPSILConn
    '        oledbDa = New OleDbDataAdapter(strSQL, oledbConnect)
    '        oledbDa.Fill(dtMode)
    '    Catch sqlex As SqlClient.SqlException
    '        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
    '    End Try

    '    oledbConnect.Dispose()

    '    If Not dtMode Is Nothing Then
    '        If dtMode.Rows.Count > 0 Then
    '            With dtMode.Rows(0)
    '                dblMF_H = CDbl(.Item("MF_S")) 'half-annually mode factor
    '                dblMF_M = CDbl(.Item("MF_M")) 'monthly mode factor
    '                dblMF_Cur = CDbl(.Item("POCFCT")) ' current mode factor
    '                dblGrossAnnlPrem = CDbl(.Item("POGPRM")) ' gross annual premium
    '                dblPolicyFee = CDbl(.Item("RPOFEE"))  ' policy fee
    '                dblAnnl = dblGrossAnnlPrem
    '                dblHalfAnnl = (dblGrossAnnlPrem - dblPolicyFee) * dblMF_H + (dblPolicyFee / 2)
    '                dblMonthly = (dblGrossAnnlPrem - dblPolicyFee) * dblMF_M + (dblPolicyFee / 12)
    '            End With

    '            txtMP_Y.Text = dblAnnl
    '            txtMP_H.Text = dblHalfAnnl
    '            txtMP_M.Text = dblMonthly

    '        End If
    '    End If

    '    Dim datIssDT, datPTD As DateTime
    '    Dim dd, mm, yy As Integer
    '    Dim dblAnnlP, dblHalfP As Double

    '    ' Calculate premium to due date
    '    If Not IsDBNull(dblMF_Cur) Then
    '        With dtPolInfo.Rows(0)
    '            datIssDT = .Item("PolicyEffDate")
    '            dd = Microsoft.VisualBasic.DateAndTime.Day(datIssDT)
    '            mm = Microsoft.VisualBasic.DateAndTime.Month(datIssDT)
    '            datPTD = .Item("PaidToDate")
    '            yy = Microsoft.VisualBasic.DateAndTime.Year(datPTD)
    '        End With
    '    End If

    '    Dim datNxtPTD_h, datNxtPTD_m, datNxtPTD_a As DateTime
    '    Dim dblTtlD_y, dblTtlD As Integer

    '    With dtPolInfo.Rows(0)
    '        If Not (IsDBNull(datIssDT) And IsDBNull(datPTD)) Then
    '            If .Item("Mode") = "A" Then
    '                datNxtPTD_h = DateAdd("m", 6, datPTD)
    '                datNxtPTD_m = DateAdd("m", 1, datPTD)

    '                Me.txtDD_Y.Text = DateAdd(DateInterval.Year, 1, datNxtPTD_a)
    '                Me.txtDD_Y.ForeColor = System.Drawing.Color.Red
    '                Me.txtDD_H.Text = datNxtPTD_h
    '                Me.txtDD_M.Text = datNxtPTD_m

    '                Me.txtPD_Y.Text = dblAnnl
    '                Me.txtPD_Y.ForeColor = System.Drawing.Color.Red
    '                Me.txtPD_H.Text = dblHalfAnnl
    '                Me.txtPD_M.Text = dblMonthly

    '            ElseIf .Item("mode") = "H" Then
    '                If Month(datPTD) = Month(datIssDT) Then
    '                    datNxtPTD_a = DateAdd("m", 12, datPTD)
    '                Else
    '                    datNxtPTD_a = DateAdd("m", 6, datPTD)
    '                End If
    '                datNxtPTD_m = DateAdd("m", 1, datPTD)

    '                dblTtlD_y = CPS_SUB_DATE(datPTD, DateAdd("m", 12, datPTD))
    '                dblTtlD = CPS_SUB_DATE(datPTD, datNxtPTD_a)
    '                dblAnnlP = Math.Round(dblAnnl / dblTtlD_y * dblTtlD, 2)

    '                Me.txtDD_Y.Text = datNxtPTD_a
    '                Me.txtDD_H.Text = DateAdd("m", 6, datNxtPTD_h)
    '                Me.txtDD_H.ForeColor = System.Drawing.Color.Red
    '                Me.txtDD_M.Text = datNxtPTD_m

    '                Me.txtPD_Y.Text = dblAnnlP
    '                Me.txtPD_H.Text = dblHalfAnnl
    '                Me.txtPD_H.ForeColor = System.Drawing.Color.Red
    '                Me.txtPD_M.Text = dblMonthly

    '            ElseIf .Item("Mode") = "M" Then
    '                datNxtPTD_a = DateSerial(yy, mm, dd)
    '                If datPTD >= datNxtPTD_a Then
    '                    datNxtPTD_a = DateAdd("yyyy", 1, datNxtPTD_a)
    '                End If
    '                datNxtPTD_h = DateAdd("m", -6, datNxtPTD_a)
    '                If datPTD >= datNxtPTD_h Then
    '                    datNxtPTD_h = DateAdd("m", 6, datNxtPTD_h)
    '                End If

    '                dblTtlD_y = CPS_SUB_DATE(datPTD, DateAdd("m", 12, datPTD))
    '                dblTtlD = CPS_SUB_DATE(datPTD, datNxtPTD_a)
    '                dblAnnlP = Math.Round(dblAnnl / dblTtlD_y * dblTtlD, 2)

    '                dblTtlD_y = CPS_SUB_DATE(datPTD, DateAdd("m", 6, datPTD))
    '                dblTtlD = CPS_SUB_DATE(datPTD, datNxtPTD_h)
    '                dblHalfP = Math.Round(dblHalfAnnl / dblTtlD_y * dblTtlD, 2)

    '                Me.txtDD_Y.Text = datNxtPTD_a
    '                Me.txtDD_H.Text = datNxtPTD_h
    '                Me.txtDD_M.Text = DateAdd(DateInterval.Month, 1, datNxtPTD_m)
    '                Me.txtDD_M.ForeColor = System.Drawing.Color.Red

    '                Me.txtPD_Y.Text = dblAnnlP
    '                Me.txtPD_H.Text = dblHalfP
    '                Me.txtPD_M.Text = dblMonthly
    '                Me.txtPD_M.ForeColor = System.Drawing.Color.Red

    '            End If

    '        End If
    '    End With

    'End Sub


    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Public Sub ChangeModeQuote(ByVal strPolicyNo As String)

        'Dim oledbDa As OleDbDataAdapter
        'Dim oledbConnect As New OleDbConnection
        'Dim oleCmd As OleDbCommand

        'Dim strSQL As String
        Dim dtMode As DataTable = New DataTable("ModeChange")

        ''(modal premium)
        'Dim h_ModeFactor As Double  'half-annually mode factor
        'Dim m_ModeFactor As Double  'monthly mode factor
        'Dim dbl_curModeFactor As Double    ' current mode factor
        'Dim dblGrossAnnlPrem As Double     ' gross annual premium
        'Dim dblPolicyFee As Double  ' policy fee
        'Dim issdt, ptd As Date
        'Dim dd, mm, yy As Integer
        'Dim td_y As Double
        'Dim td As Integer

        ''Premium Quote
        'Dim dblAnnlM As Double
        'Dim dblAnnlA As Double
        'Dim dblAnnlH As Double
        ''Next Due Date
        'Dim nextptd_h As Date
        'Dim nextptd_m As Date
        'Dim nextptd_a As Date
        ''(Premium to due date)
        'Dim quote_a As Double
        'Dim quote_h As Double
        'Dim quote_m As Double

        'Dim dtDeposit, dtEffDate As Date
        'dtDeposit = #1/1/1900#

        '' Calculate modal premium for M, H, A respectively
        'strSQL = _
        '    " Select COPOLI, COTRAI, POMODX, FEPREM, FEDURA, RMURAA, MURADU, ANLIPR, RADPRE, " & _
        '    "        RWPPRE, RAREFE, RAREMU, POCFCT, RMOPRE, FLD0047 AS MF_S, FLD0049 AS MF_M, POGPRM, RPOFEE, " & _
        '    "        TRIM(CHAR(POPAMM))||'/'||TRIM(CHAR(POPADD))||'/'||CHAR(INT(POPAYY)+1800) AS PAIDTODATE, " & _
        '    "        TRIM(CHAR(RISMON))||'/'||TRIM(CHAR(RISDAY))||'/'||CHAR(INT(RISYEA)+1800) AS ISSUEDATE, " & _
        '    "        POSAMT AS SUNDRYAMT, POMODX, ORPLFI || ORPLRE || RRASCL as RiderCode " & _
        '    " From " & ORDUPO & ", " & ORDUCO & ", " & ORDUPH & _
        '    " Where POPONO = '" & strPolicyNo & "' " & _
        '    " And POPONO = COPOLI " & _
        '    " And FLD0003 = RRASCL " & _
        '    " And FLD0002 = ORPLFI || ORPLRE " & _
        '    " Order By COTRAI"

        'Try
        '    oledbConnect.ConnectionString = strCAPSILConn
        '    oledbDa = New OleDbDataAdapter(strSQL, oledbConnect)
        '    oledbDa.Fill(dtMode)
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        'oledbConnect.Dispose()

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    'objCS.Env = giEnv
        'End If
        'AC - Change to use configuration setting - end

        dtMode = objCS.GetModeChangeInfo(strPolicy, lngErr, strErr)

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        End If

        For i As Integer = 0 To dtMode.Rows.Count - 1
            With dtMode.Rows(i)
                Select Case .Item("Mode")
                    Case "01"
                        txtMP_M.Text = Format(.Item("ModePremium"), gNumFormat)
                        txtDD_M.Text = Format(.Item("NextDueDate"), gDateFormat)
                        txtPD_M.Text = Format(.Item("PremtoDueDate"), gNumFormat)
                        txtMF_M.Text = Format(.Item("ModeFactor"), gMfFormat)
                        txtLevy_M.Text = Format(.Item("LevyToDueDate"), gNumFormat)
                        txtChangeModePrem_M.Text = Format(.Item("ChangeModePremium"), gNumFormat)
                    Case "06"
                        txtMP_H.Text = Format(.Item("ModePremium"), gNumFormat)
                        txtDD_H.Text = Format(.Item("NextDueDate"), gDateFormat)
                        txtPD_H.Text = Format(.Item("PremtoDueDate"), gNumFormat)
                        txtMF_H.Text = Format(.Item("ModeFactor"), gMfFormat)
                        txtLevy_H.Text = Format(.Item("LevyToDueDate"), gNumFormat)
                        txtChangeModePrem_H.Text = Format(.Item("ChangeModePremium"), gNumFormat)
                    Case "12"
                        txtMP_Y.Text = Format(.Item("ModePremium"), gNumFormat)
                        txtDD_Y.Text = Format(.Item("NextDueDate"), gDateFormat)
                        txtPD_Y.Text = Format(.Item("PremtoDueDate"), gNumFormat)
                        txtMF_Y.Text = Format(.Item("ModeFactor"), gMfFormat)
                        txtLevy_Y.Text = Format(.Item("LevyToDueDate"), gNumFormat)
                        txtChangeModePrem_Y.Text = Format(.Item("ChangeModePremium"), gNumFormat)
                End Select
            End With
        Next

        ''With dtMode.Rows(0)
        ''    If CInt(.Item("COTRAI")) = 1 Then
        ''        h_ModeFactor = CDbl(.Item("MF_S"))  'half-annually mode factor
        ''        m_ModeFactor = CDbl(.Item("MF_M"))  'monthly mode factor
        ''        dbl_curModeFactor = CDbl(.Item("POCFCT"))   ' current mode factor
        ''        dblGrossAnnlPrem = CDbl(.Item("POGPRM"))    ' gross annual premium
        ''        dblPolicyFee = CDbl(.Item("RPOFEE"))        ' policy fee
        ''        dblAnnlA = dblGrossAnnlPrem

        ''        ''' AMP product, exclude policy fee
        ''        ''Dim strBasPlan, strAMP As String
        ''        ''strBasPlan = dtMode.Rows(0).Item("RiderCode")

        ''        ''strAMP = strBasPlan.Substring(1, 3)
        ''        ''If strAMP = "CCB" Or strAMP = "ART" Then
        ''        ''    dblPolicyFee = 0
        ''        ''End If

        ''        ''strAMP = strBasPlan.Substring(1, 4)
        ''        ''If strAMP = "JP21" Or strAMP = "PL65" Or strAMP = "WLRB" Or strAMP = "WL95" Then
        ''        ''    dblPolicyFee = 0
        ''        ''End If

        ''        ''strAMP = strBasPlan.Substring(1, 5)
        ''        ''If strAMP = "PL201" Or strAMP = "JP651" Then
        ''        ''    dblPolicyFee = 0
        ''        ''End If
        ''    End If
        ''End With

        '''' Find Effective date
        '''Dim dtCapsilDate As Date
        '''Dim strCapsil As String

        '''strSQL = "Select FLD0003 from " & ORDUMC
        '''oledbConnect.Open()
        '''oleCmd = New OleDbCommand(strSQL, oledbConnect)
        '''Try
        '''    strCapsil = oleCmd.ExecuteScalar()
        '''Catch sqlex As SqlClient.SqlException
        '''    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        '''Catch ex As Exception
        '''    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        '''Finally
        '''    oledbConnect.Close()
        '''End Try

        '''get plan type and check if sufficient money for change mode
        ''Dim blnCB, blnMCIL As Boolean
        ''Dim strPlanType As String
        ''Dim dblRegular As Double

        ''dblAnnlA = 0
        ''dblAnnlH = 0
        ''dblAnnlM = 0

        ''blnCB = False
        ''blnMCIL = False

        ''strPlanType = Trim(GetPOSPlanType(dtMode.Rows(0).Item("RiderCode")))        ' *** Get plan type
        ''If strPlanType = "MC" Or strPlanType = "IL" Then blnMCIL = True
        ''If strPlanType = "CB" Then blnCB = True

        ''If blnMCIL Then
        ''    Dim dr As DataRow
        ''    Dim strProd As String
        ''    For Each dr In dtMode.Rows
        ''        strProd = Mid(dr.Item("RiderCode"), 2, 4)
        ''        If strProd = "RYCR" Or strProd = "RGCR" Or strProd = "RDCR" Or strProd = "RILR" Or strProd = "RULR" Then
        ''            dblRegular = dr.Item("FaceAmount")
        ''            Exit For
        ''        End If
        ''    Next
        ''End If

        ''With dtMode.Rows(0)
        ''    issdt = .Item("IssueDate")
        ''    dd = Microsoft.VisualBasic.Day(issdt)
        ''    mm = Microsoft.VisualBasic.Month(issdt)
        ''    ptd = .Item("PaidToDate")
        ''    yy = Year(ptd)

        ''    'dtCapsilDate = VBDate(strCapsil)
        ''    'dtEffDate = DateAdd("m", DateDiff("m", ptd, dtCapsilDate), ptd)

        ''End With

        ''If Not blnCB Then
        ''    dblAnnlA = dblGrossAnnlPrem
        ''    dblAnnlH = ((dblGrossAnnlPrem - dblPolicyFee) * h_ModeFactor) + (dblPolicyFee / 2)
        ''    dblAnnlM = ((dblGrossAnnlPrem - dblPolicyFee) * m_ModeFactor) + (dblPolicyFee / 12)

        ''    If Not (IsDBNull(issdt) And IsDBNull(ptd)) Then
        ''        Select Case dtMode.Rows(0).Item("POMODX")
        ''            Case "12"
        ''                nextptd_h = DateAdd("m", 6, ptd) 'Next Paid to date to half-annual mode
        ''                nextptd_m = DateAdd("m", 1, ptd) 'Next Paid to date to monthly mode
        ''                nextptd_a = DateAdd("m", 12, ptd) 'Next Paid to date to annual mode

        ''                quote_a = dblAnnlA
        ''                quote_h = dblAnnlH
        ''                quote_m = dblAnnlM

        ''            Case "6"
        ''                nextptd_a = DateSerial(yy, mm, dd)
        ''                If DateDiff("d", ptd, nextptd_a) <= 0 Then
        ''                    nextptd_a = DateAdd("yyyy", 1, nextptd_a)
        ''                End If
        ''                nextptd_m = DateAdd("m", 1, nextptd_a)
        ''                nextptd_h = DateAdd("m", 6, ptd) 'Next Paid to date to half-annual mode

        ''                ' dblAnnla
        ''                td_y = 365
        ''                'td = DateDiff("d", dtEffDate, nextptd_a)
        ''                td = CPS_SUB_DATE(ptd, nextptd_a)

        ''                quote_a = dblAnnlA / td_y * td
        ''                If blnMCIL = True And dblRegular > 0 Then
        ''                    quote_a += (DateDiff("m", ptd, nextptd_a) / 6 * dblRegular)
        ''                End If

        ''                quote_h = dblAnnlH
        ''                quote_m = dblAnnlM

        ''                'If DateAdd("m", 6, dtEffDate) = nextptd_a Then
        ''                '    quote_a = 0
        ''                'End If

        ''            Case "1"
        ''                nextptd_a = DateSerial(yy, mm, dd)
        ''                If DateDiff("d", ptd, nextptd_a) <= 0 Then
        ''                    nextptd_a = DateAdd("yyyy", 1, nextptd_a)
        ''                End If

        ''                nextptd_h = DateAdd("m", -6, nextptd_a)
        ''                If DateDiff("d", ptd, nextptd_h) <= 0 Then
        ''                    nextptd_h = DateAdd("m", 6, nextptd_h)
        ''                End If

        ''                nextptd_m = DateAdd("m", 1, ptd) 'Next Paid to date to monthly mode

        ''                'quote annual
        ''                td_y = 365
        ''                'td = DateDiff("d", dtEffDate, nextptd_a)
        ''                td = CPS_SUB_DATE(ptd, nextptd_a)
        ''                quote_a = Math.Round(dblAnnlA / td_y * td, 2)

        ''                'quote half-annual
        ''                'td_y = CPS_SUB_DATE(dtEffDate, DateAdd("m", 6, dtEffDate))
        ''                'td = CPS_SUB_DATE(dtEffDate, nextptd_h)
        ''                td_y = CPS_SUB_DATE(ptd, DateAdd("m", 6, ptd))
        ''                td = CPS_SUB_DATE(ptd, nextptd_h)

        ''                quote_h = Math.Round(dblAnnlH / td_y * td, 2)

        ''                If blnMCIL = True And dblRegular > 0 Then
        ''                    quote_a += DateDiff("m", ptd, nextptd_a) * dblRegular
        ''                    quote_h += DateDiff("m", ptd, nextptd_a) * dblRegular
        ''                End If

        ''                quote_m = dblAnnlM

        ''                'If DateAdd("m", 6, dtEffDate) = nextptd_h Then
        ''                '    quote_h = 0
        ''                'End If

        ''                'If DateAdd("m", 12, dtEffDate) = nextptd_a Then
        ''                '    quote_a = 0
        ''                'End If

        ''        End Select
        ''    End If
        ''Else
        ''    'CB policy quote
        ''    Dim noOfMonth As Long

        ''    If Not (IsDBNull(issdt) And IsDBNull(ptd)) Then
        ''        'current mode = 'Annual
        ''        Select Case dtMode.Rows(0).Item("POMODX")
        ''            Case "12"
        ''                nextptd_h = DateAdd("m", 6, ptd) 'Next Paid to date to half-annual mode
        ''                nextptd_m = DateAdd("m", 1, ptd) 'Next Paid to date to monthly mode
        ''                nextptd_a = DateAdd("m", 12, ptd) 'Next Paid to date to annual mode

        ''                quote_a = 0
        ''                quote_h = 0
        ''                quote_m = 0

        ''            Case "6"
        ''                nextptd_a = DateSerial(yy, mm, dd)
        ''                If DateDiff("d", ptd, nextptd_a) < 0 Then
        ''                    nextptd_a = DateAdd("yyyy", 1, nextptd_a)
        ''                End If
        ''                nextptd_m = DateAdd("m", 1, nextptd_a)
        ''                nextptd_h = DateAdd("m", 6, ptd) 'Next Paid to date to half-annual mode

        ''                td = DateDiff("m", ptd, nextptd_a)
        ''                quote_h = 0
        ''                quote_m = 0

        ''            Case "1"
        ''                nextptd_a = DateSerial(yy, mm, dd)
        ''                If DateDiff("d", ptd, nextptd_a) < 0 Then
        ''                    nextptd_a = DateAdd("yyyy", 1, nextptd_a)
        ''                End If

        ''                nextptd_h = DateAdd("m", -6, nextptd_a)
        ''                If DateDiff("d", ptd, nextptd_h) < 0 Then
        ''                    nextptd_h = DateAdd("m", 6, nextptd_h)
        ''                End If

        ''                nextptd_m = DateAdd("m", 1, ptd) 'Next Paid to date to monthly mode

        ''                'quote annual
        ''                td = DateDiff("m", ptd, nextptd_a)
        ''                'dblDeposit = dtMode.Rows(0).Item("SundryAmt").Value * td

        ''                'quote half-annual
        ''                td = DateDiff("m", ptd, nextptd_h)
        ''                quote_m = 0

        ''        End Select
        ''    End If
        ''End If

        ''txtMP_M.Text = Format(dblAnnlM, gNumFormat)
        ''txtMP_H.Text = Format(dblAnnlH, gNumFormat)
        ''txtMP_Y.Text = Format(dblAnnlA, gNumFormat)

        ''txtDD_M.Text = Format(nextptd_m, gDateFormat)
        ''txtDD_H.Text = Format(nextptd_h, gDateFormat)
        ''txtDD_Y.Text = Format(nextptd_a, gDateFormat)

        ''txtPD_M.Text = Format(quote_m, gNumFormat)
        ''txtPD_H.Text = Format(quote_h, gNumFormat)
        ''txtPD_Y.Text = Format(quote_a, gNumFormat)

        ''txtMF_M.Text = Format(m_ModeFactor, gMfFormat)
        ''txtMF_H.Text = Format(h_ModeFactor, gMfFormat)
        ''txtMF_Y.Text = "1.0000000"

        ''Dim strNewMode As String
        ''Dim dblFaceAmount_A, dblSundryAmt_A, dblDeposit_A As Double
        ''Dim dblFaceAmount_H, dblSundryAmt_H, dblDeposit_H As Double
        ''Dim dblFaceAmount_M, dblSundryAmt_M, dblDeposit_M As Double

        ''With dtMode.Rows(0)
        ''    'dtDeposit = Format(dtDeposit, "mm/dd/yyyy")
        ''    If blnCB = True Or blnMCIL = True Then
        ''        dtDeposit = Today
        ''        If blnMCIL = True And dblRegular > 0 Then
        ''            If .Item("pomodx") = "12" Then
        ''                dblFaceAmount_M = dblRegular / 12
        ''                dblFaceAmount_H = dblRegular / 2
        ''                dblFaceAmount_A = dblRegular
        ''            ElseIf .Item("pomodx") = "6" Then
        ''                dblFaceAmount_M = dblRegular / 6
        ''                dblFaceAmount_H = dblRegular
        ''                dblFaceAmount_A = dblRegular * 2
        ''            ElseIf .Item("pomodx") = "1" Then
        ''                dblFaceAmount_M = dblRegular
        ''                dblFaceAmount_H = dblRegular * 6
        ''                dblFaceAmount_A = dblRegular * 12
        ''            End If
        ''        End If

        ''        txtDepositDT.Text = Format(dtDeposit, gDateFormat)
        ''        txtRF_Y.Text = Format(dblFaceAmount_A, gNumFormat)
        ''        txtRF_H.Text = Format(dblFaceAmount_H, gNumFormat)
        ''        txtRF_M.Text = Format(dblFaceAmount_M, gNumFormat)
        ''    End If

        ''    dblDeposit_M = 0
        ''    dblDeposit_H = 0
        ''    dblDeposit_A = 0
        ''    If blnCB = True Then
        ''        If .Item("pomodx") = "12" Then
        ''            dblSundryAmt_M = .Item("SundryAmt") / 12
        ''            dblSundryAmt_H = .Item("SundryAmt") / 2
        ''            dblSundryAmt_A = .Item("SundryAmt")

        ''        ElseIf .Item("pomodx") = "6" Then
        ''            dblSundryAmt_M = .Item("SundryAmt") / 6
        ''            dblSundryAmt_H = .Item("SundryAmt")
        ''            dblSundryAmt_A = .Item("SundryAmt") * 2
        ''            dblDeposit_A = .Item("SundryAmt") * DateDiff("m", ptd, nextptd_a) / 6

        ''        ElseIf .Item("pomodx") = "1" Then
        ''            dblSundryAmt_M = .Item("SundryAmt")
        ''            dblDeposit_M = 0
        ''            dblSundryAmt_H = .Item("SundryAmt") * 6
        ''            dblDeposit_H = .Item("SundryAmt") * DateDiff("m", ptd, nextptd_h)
        ''            dblSundryAmt_A = .Item("SundryAmt") * 12
        ''            dblDeposit_A = .Item("SundryAmt") * DateDiff("m", ptd, nextptd_a)
        ''        End If
        ''    End If
        ''    txtDP_Y.Text = Format(dblDeposit_A, gNumFormat)
        ''    txtDP_H.Text = Format(dblDeposit_H, gNumFormat)
        ''    txtDP_M.Text = Format(dblDeposit_M, gNumFormat)

        ''    txtSA_Y.Text = Format(dblSundryAmt_A, gNumFormat)
        ''    txtSA_H.Text = Format(dblSundryAmt_H, gNumFormat)
        ''    txtSA_M.Text = Format(dblSundryAmt_M, gNumFormat)
        ''End With

    End Sub

End Class
