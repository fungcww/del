Public Class frmGIPolicy
    Inherits System.Windows.Forms.Form

    Private strPolicy, strCustID, strRenewal, strStatus, strCurType As String
    Private blnExit, blnGrpMed As Boolean
    Private dtPayH, dtInsured, dtFieldMap As DataTable
    Private WithEvents bm As BindingManagerBase

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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtInsured As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCOVTdt As System.Windows.Forms.TextBox
    Friend WithEvents txtFin As System.Windows.Forms.TextBox
    Friend WithEvents txtCOVFdt As System.Windows.Forms.TextBox
    Friend WithEvents txtPolicyPD As System.Windows.Forms.TextBox
    Friend WithEvents txtDisc As System.Windows.Forms.TextBox
    Friend WithEvents txtAdmin As System.Windows.Forms.TextBox
    Friend WithEvents txtFIDt As System.Windows.Forms.TextBox
    Friend WithEvents txtSTR As System.Windows.Forms.TextBox
    Friend WithEvents txtRenI As System.Windows.Forms.TextBox
    Friend WithEvents txtBill As System.Windows.Forms.TextBox
    Friend WithEvents txtPayFq As System.Windows.Forms.TextBox
    Friend WithEvents txtOcc As System.Windows.Forms.TextBox
    Friend WithEvents txtCurr As System.Windows.Forms.TextBox
    Friend WithEvents txtCommDt As System.Windows.Forms.TextBox
    Friend WithEvents txtEffDt As System.Windows.Forms.TextBox
    Friend WithEvents txtTxCode As System.Windows.Forms.TextBox
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtMainClass As System.Windows.Forms.TextBox
    Friend WithEvents txtEndor As System.Windows.Forms.TextBox
    Friend WithEvents txtRen As System.Windows.Forms.TextBox
    Friend WithEvents txtPPolicy As System.Windows.Forms.TextBox
    Friend WithEvents txtPolicy As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtChiName As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents grdGI As System.Windows.Forms.DataGrid
    Friend WithEvents grdIns As System.Windows.Forms.DataGrid
    Friend WithEvents txtPol As System.Windows.Forms.TextBox
    Friend WithEvents txtProd As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtCOVTdt = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtFin = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtCOVFdt = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtPolicyPD = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDisc = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtAdmin = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtFIDt = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtSTR = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtRenI = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBill = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtPayFq = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtOcc = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCurr = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCommDt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEffDt = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTxCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMainClass = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEndor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRen = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPPolicy = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPolicy = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.grdIns = New System.Windows.Forms.DataGrid()
        Me.txtInsured = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.grdGI = New System.Windows.Forms.DataGrid()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPol = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtProd = New System.Windows.Forms.TextBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtChiName = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdIns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdGI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 82)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(764, 377)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.txtCOVTdt)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.txtFin)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.txtCOVFdt)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.txtPolicyPD)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.txtDisc)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.txtAdmin)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.txtFIDt)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.txtSTR)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.txtRenI)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.txtBill)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.txtPayFq)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.txtOcc)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.txtCurr)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.txtCommDt)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtEffDt)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.txtTxCode)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.txtProduct)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtMainClass)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtEndor)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtRen)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtPPolicy)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtPolicy)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(756, 344)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Policy Summary"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(339, 234)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(14, 20)
        Me.Label22.TabIndex = 184
        Me.Label22.Text = "-"
        '
        'txtCOVTdt
        '
        Me.txtCOVTdt.Location = New System.Drawing.Point(358, 228)
        Me.txtCOVTdt.Name = "txtCOVTdt"
        Me.txtCOVTdt.Size = New System.Drawing.Size(135, 26)
        Me.txtCOVTdt.TabIndex = 183
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(518, 234)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(135, 20)
        Me.Label21.TabIndex = 182
        Me.Label21.Text = "Financial Interest:"
        '
        'txtFin
        '
        Me.txtFin.Location = New System.Drawing.Point(678, 228)
        Me.txtFin.Name = "txtFin"
        Me.txtFin.Size = New System.Drawing.Size(500, 26)
        Me.txtFin.TabIndex = 181
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(45, 234)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(124, 20)
        Me.Label20.TabIndex = 180
        Me.Label20.Text = "Covering Period:"
        '
        'txtCOVFdt
        '
        Me.txtCOVFdt.Location = New System.Drawing.Point(198, 228)
        Me.txtCOVFdt.Name = "txtCOVFdt"
        Me.txtCOVFdt.Size = New System.Drawing.Size(135, 26)
        Me.txtCOVFdt.TabIndex = 179
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(896, 199)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(128, 20)
        Me.Label19.TabIndex = 178
        Me.Label19.Text = "Policy Print Date:"
        '
        'txtPolicyPD
        '
        Me.txtPolicyPD.Location = New System.Drawing.Point(1050, 193)
        Me.txtPolicyPD.Name = "txtPolicyPD"
        Me.txtPolicyPD.Size = New System.Drawing.Size(128, 26)
        Me.txtPolicyPD.TabIndex = 177
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(608, 199)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(137, 20)
        Me.Label18.TabIndex = 176
        Me.Label18.Text = "Discount/Loading:"
        '
        'txtDisc
        '
        Me.txtDisc.Location = New System.Drawing.Point(768, 193)
        Me.txtDisc.Name = "txtDisc"
        Me.txtDisc.Size = New System.Drawing.Size(115, 26)
        Me.txtDisc.TabIndex = 175
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(365, 199)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(98, 20)
        Me.Label17.TabIndex = 174
        Me.Label17.Text = "Admin Fees:"
        '
        'txtAdmin
        '
        Me.txtAdmin.Location = New System.Drawing.Point(480, 193)
        Me.txtAdmin.Name = "txtAdmin"
        Me.txtAdmin.Size = New System.Drawing.Size(115, 26)
        Me.txtAdmin.TabIndex = 173
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(32, 199)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(144, 20)
        Me.Label16.TabIndex = 172
        Me.Label16.Text = "1st Inception Date:"
        '
        'txtFIDt
        '
        Me.txtFIDt.Location = New System.Drawing.Point(198, 193)
        Me.txtFIDt.Name = "txtFIDt"
        Me.txtFIDt.Size = New System.Drawing.Size(135, 26)
        Me.txtFIDt.TabIndex = 171
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(896, 164)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(131, 20)
        Me.Label15.TabIndex = 170
        Me.Label15.Text = "Short Term Rate:"
        '
        'txtSTR
        '
        Me.txtSTR.Location = New System.Drawing.Point(1050, 158)
        Me.txtSTR.Name = "txtSTR"
        Me.txtSTR.Size = New System.Drawing.Size(70, 26)
        Me.txtSTR.TabIndex = 169
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(602, 164)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(141, 20)
        Me.Label14.TabIndex = 168
        Me.Label14.Text = "Renewal Indicator:"
        '
        'txtRenI
        '
        Me.txtRenI.Location = New System.Drawing.Point(768, 158)
        Me.txtRenI.Name = "txtRenI"
        Me.txtRenI.Size = New System.Drawing.Size(83, 26)
        Me.txtRenI.TabIndex = 167
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(346, 164)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(112, 20)
        Me.Label13.TabIndex = 166
        Me.Label13.Text = "Billing Method:"
        '
        'txtBill
        '
        Me.txtBill.Location = New System.Drawing.Point(480, 158)
        Me.txtBill.Name = "txtBill"
        Me.txtBill.Size = New System.Drawing.Size(115, 26)
        Me.txtBill.TabIndex = 165
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 164)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(154, 20)
        Me.Label12.TabIndex = 164
        Me.Label12.Text = "Payment Frequency:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPayFq
        '
        Me.txtPayFq.Location = New System.Drawing.Point(198, 158)
        Me.txtPayFq.Name = "txtPayFq"
        Me.txtPayFq.Size = New System.Drawing.Size(135, 26)
        Me.txtPayFq.TabIndex = 163
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(38, 129)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(94, 20)
        Me.Label11.TabIndex = 162
        Me.Label11.Text = "Occupation:"
        '
        'txtOcc
        '
        Me.txtOcc.Location = New System.Drawing.Point(154, 123)
        Me.txtOcc.Name = "txtOcc"
        Me.txtOcc.Size = New System.Drawing.Size(576, 26)
        Me.txtOcc.TabIndex = 161
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(768, 94)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 20)
        Me.Label10.TabIndex = 160
        Me.Label10.Text = "Policy Currency:"
        '
        'txtCurr
        '
        Me.txtCurr.Location = New System.Drawing.Point(915, 88)
        Me.txtCurr.Name = "txtCurr"
        Me.txtCurr.Size = New System.Drawing.Size(83, 26)
        Me.txtCurr.TabIndex = 159
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(358, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(169, 20)
        Me.Label9.TabIndex = 158
        Me.Label9.Text = "Commencement Date:"
        '
        'txtCommDt
        '
        Me.txtCommDt.Location = New System.Drawing.Point(557, 88)
        Me.txtCommDt.Name = "txtCommDt"
        Me.txtCommDt.Size = New System.Drawing.Size(173, 26)
        Me.txtCommDt.TabIndex = 157
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 94)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(114, 20)
        Me.Label8.TabIndex = 156
        Me.Label8.Text = "Effective Date:"
        '
        'txtEffDt
        '
        Me.txtEffDt.Location = New System.Drawing.Point(154, 88)
        Me.txtEffDt.Name = "txtEffDt"
        Me.txtEffDt.Size = New System.Drawing.Size(172, 26)
        Me.txtEffDt.TabIndex = 155
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(877, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 20)
        Me.Label7.TabIndex = 154
        Me.Label7.Text = "Transaction Code:"
        '
        'txtTxCode
        '
        Me.txtTxCode.Location = New System.Drawing.Point(1043, 53)
        Me.txtTxCode.Name = "txtTxCode"
        Me.txtTxCode.Size = New System.Drawing.Size(71, 26)
        Me.txtTxCode.TabIndex = 153
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(480, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 20)
        Me.Label6.TabIndex = 152
        Me.Label6.Text = "Product:"
        '
        'txtProduct
        '
        Me.txtProduct.Location = New System.Drawing.Point(563, 53)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.Size = New System.Drawing.Size(295, 26)
        Me.txtProduct.TabIndex = 151
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(45, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 20)
        Me.Label4.TabIndex = 150
        Me.Label4.Text = "Main Class:"
        '
        'txtMainClass
        '
        Me.txtMainClass.Location = New System.Drawing.Point(154, 53)
        Me.txtMainClass.Name = "txtMainClass"
        Me.txtMainClass.Size = New System.Drawing.Size(313, 26)
        Me.txtMainClass.TabIndex = 149
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(941, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 20)
        Me.Label3.TabIndex = 148
        Me.Label3.Text = "Endor No.:"
        '
        'txtEndor
        '
        Me.txtEndor.Location = New System.Drawing.Point(1043, 18)
        Me.txtEndor.Name = "txtEndor"
        Me.txtEndor.Size = New System.Drawing.Size(71, 26)
        Me.txtEndor.TabIndex = 147
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(723, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 20)
        Me.Label2.TabIndex = 146
        Me.Label2.Text = "Renewal No.:"
        '
        'txtRen
        '
        Me.txtRen.Location = New System.Drawing.Point(845, 18)
        Me.txtRen.Name = "txtRen"
        Me.txtRen.Size = New System.Drawing.Size(70, 26)
        Me.txtRen.TabIndex = 145
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(346, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 20)
        Me.Label1.TabIndex = 144
        Me.Label1.Text = "Previous Policy No.:"
        '
        'txtPPolicy
        '
        Me.txtPPolicy.Location = New System.Drawing.Point(525, 18)
        Me.txtPPolicy.Name = "txtPPolicy"
        Me.txtPPolicy.Size = New System.Drawing.Size(173, 26)
        Me.txtPPolicy.TabIndex = 143
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(51, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 20)
        Me.Label5.TabIndex = 142
        Me.Label5.Text = "Policy No.:"
        '
        'txtPolicy
        '
        Me.txtPolicy.Location = New System.Drawing.Point(154, 18)
        Me.txtPolicy.Name = "txtPolicy"
        Me.txtPolicy.Size = New System.Drawing.Size(172, 26)
        Me.txtPolicy.TabIndex = 141
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.grdIns)
        Me.TabPage2.Controls.Add(Me.txtInsured)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(1214, 557)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Insured Coverage"
        '
        'grdIns
        '
        Me.grdIns.AlternatingBackColor = System.Drawing.Color.White
        Me.grdIns.BackColor = System.Drawing.Color.White
        Me.grdIns.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdIns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdIns.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdIns.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdIns.CaptionVisible = False
        Me.grdIns.DataMember = ""
        Me.grdIns.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdIns.FlatMode = True
        Me.grdIns.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdIns.ForeColor = System.Drawing.Color.Black
        Me.grdIns.GridLineColor = System.Drawing.Color.Wheat
        Me.grdIns.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdIns.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdIns.HeaderForeColor = System.Drawing.Color.Black
        Me.grdIns.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdIns.Location = New System.Drawing.Point(0, 0)
        Me.grdIns.Name = "grdIns"
        Me.grdIns.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdIns.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdIns.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdIns.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdIns.Size = New System.Drawing.Size(1214, 193)
        Me.grdIns.TabIndex = 34
        '
        'txtInsured
        '
        Me.txtInsured.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInsured.Location = New System.Drawing.Point(0, 205)
        Me.txtInsured.Multiline = True
        Me.txtInsured.Name = "txtInsured"
        Me.txtInsured.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtInsured.Size = New System.Drawing.Size(1210, 416)
        Me.txtInsured.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.grdGI)
        Me.TabPage3.Location = New System.Drawing.Point(4, 29)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1214, 557)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Payment History"
        '
        'grdGI
        '
        Me.grdGI.AlternatingBackColor = System.Drawing.Color.White
        Me.grdGI.BackColor = System.Drawing.Color.White
        Me.grdGI.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdGI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdGI.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGI.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdGI.CaptionVisible = False
        Me.grdGI.DataMember = ""
        Me.grdGI.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdGI.FlatMode = True
        Me.grdGI.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdGI.ForeColor = System.Drawing.Color.Black
        Me.grdGI.GridLineColor = System.Drawing.Color.Wheat
        Me.grdGI.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdGI.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdGI.HeaderForeColor = System.Drawing.Color.Black
        Me.grdGI.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGI.Location = New System.Drawing.Point(0, 0)
        Me.grdGI.Name = "grdGI"
        Me.grdGI.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdGI.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdGI.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdGI.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdGI.Size = New System.Drawing.Size(1214, 357)
        Me.grdGI.TabIndex = 33
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 47)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(81, 20)
        Me.Label23.TabIndex = 144
        Me.Label23.Text = "Policy No.:"
        '
        'txtPol
        '
        Me.txtPol.Location = New System.Drawing.Point(115, 41)
        Me.txtPol.Name = "txtPol"
        Me.txtPol.Size = New System.Drawing.Size(173, 26)
        Me.txtPol.TabIndex = 143
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(691, 47)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(68, 20)
        Me.Label24.TabIndex = 154
        Me.Label24.Text = "Product:"
        '
        'txtProd
        '
        Me.txtProd.Location = New System.Drawing.Point(774, 41)
        Me.txtProd.Name = "txtProd"
        Me.txtProd.Size = New System.Drawing.Size(378, 26)
        Me.txtProd.TabIndex = 153
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Location = New System.Drawing.Point(173, 6)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(128, 26)
        Me.txtTitle.TabIndex = 161
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(13, 12)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(135, 20)
        Me.Label25.TabIndex = 160
        Me.Label25.Text = "Serving Customer"
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastName.Location = New System.Drawing.Point(307, 6)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.Size = New System.Drawing.Size(282, 26)
        Me.txtLastName.TabIndex = 159
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstName.Location = New System.Drawing.Point(595, 6)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.Size = New System.Drawing.Size(301, 26)
        Me.txtFirstName.TabIndex = 158
        '
        'txtChiName
        '
        Me.txtChiName.BackColor = System.Drawing.SystemColors.Window
        Me.txtChiName.Location = New System.Drawing.Point(902, 6)
        Me.txtChiName.Name = "txtChiName"
        Me.txtChiName.ReadOnly = True
        Me.txtChiName.Size = New System.Drawing.Size(148, 26)
        Me.txtChiName.TabIndex = 157
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(307, 47)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(56, 20)
        Me.Label26.TabIndex = 156
        Me.Label26.Text = "Status"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Location = New System.Drawing.Point(371, 41)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(301, 26)
        Me.txtStatus.TabIndex = 155
        '
        'frmGIPolicy
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(8, 19)
        Me.ClientSize = New System.Drawing.Size(764, 461)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.txtChiName)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtProd)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.txtPol)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmGIPolicy"
        Me.Text = "frmGIPolicy"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdIns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.grdGI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property PolicyAccountID(ByVal Renewal As String, ByVal CustID As String, ByVal Status As String) As String
        Set(ByVal Value As String)
            strPolicy = Trim(Value)
            strCustID = Trim(CustID)
            strRenewal = Trim(Renewal)
            strStatus = Status
        End Set
    End Property

    Public ReadOnly Property NoRecord() As Boolean
        Get
            Return blnExit
        End Get
    End Property

    Private Sub frmGIPolicy_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim lngErrNo As Long
        Dim strErrMsg, strSQL As String
        Dim dtResult(), dtCust As DataTable
        Dim objTB As TextBox

        '#If UAT <> 0 Then
        '        strPolicy = "500004707"
        '        strRenewal = "000"
        '        strCustID = "10526336"
        '#End If

        dtResult = objCS.GetGIPSEAPolicy(strPolicy, strRenewal, strCustID, "P", lngErrNo, strErrMsg)

        If lngErrNo <> 0 Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
            blnExit = True
            Exit Sub
        End If

        If dtResult Is Nothing Then
            MsgBox("Record not found", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
            blnExit = True
            Exit Sub
        End If

        strSQL = "Select cswmgf_func, cswmgf_field_name, cswmgf_field_desc, cswmgf_disp_cs, cswmgf_disp_web " & _
            " From csw_mq_gipsea_fprop Order By cswmgf_seq"
        dtFieldMap = objCS.ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)

        If lngErrNo <> 0 Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            blnExit = True
            Exit Sub
        End If

        strSQL = "Select NamePrefix, FirstName, NameSuffix, rtrim(ChiLstNm) + rtrim(ChiFstNm) as ChiName " & _
            " From Customer " & _
            " Where Customerid = " & strCustID
        dtCust = objCS.ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)

        If lngErrNo <> 0 Then
            MsgBox(strErrMsg, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            blnExit = True
            Exit Sub
        Else
            If dtCust.Rows.Count > 0 Then
                txtTitle.Text = dtCust.Rows(0).Item("NamePrefix")
                txtLastName.Text = dtCust.Rows(0).Item("NameSuffix")
                txtFirstName.Text = dtCust.Rows(0).Item("FirstName")
                txtChiName.Text = IIf(IsDBNull(dtCust.Rows(0).Item("ChiName")), "", dtCust.Rows(0).Item("ChiName"))
                txtStatus.Text = strStatus
            End If
        End If

        For i As Integer = 0 To dtResult.Length - 1

            If Not dtResult(i) Is Nothing Then
                strCurType = Trim(dtResult(i).Rows(0).Item("MsgType"))

                Select Case strCurType
                    Case "GIX01"
                        Call UpdatePolicy(dtResult(i))
                    Case "GIP01"
                        Call UpdatePayment(dtResult(i))
                    Case "GIT01"
                        'objTB = txtInsured
                    Case "GII01", "GII02", "GII03"
                        Call UpdateInsured(dtResult(i))
                        'objTB = txtInsured
                    Case "GIC01"
                        'objTB = txtInsured
                    Case "GIA01"
                        'objTB = txtInsured
                End Select

            End If

        Next

        Me.Text = "GI Policy " & strPolicy

    End Sub

    Private Sub UpdateInsured(ByVal dtInsd As DataTable)

        Dim strPrefix As String
        strPrefix = Strings.Right(Trim(strCurType), 3)

        For i As Integer = 0 To dtInsd.Rows.Count - 1
            With dtInsd.Rows(i)
                .Item(strPrefix & "IED") = Format(Chg2Date(.Item(strPrefix & "IED")), gDateFormat)
                .Item(strPrefix & "IXD") = Format(Chg2Date(.Item(strPrefix & "IXD")), gDateFormat)
                .Item(strPrefix & "SIC") = .Item(strPrefix & "SIC") / 100
            End With
        Next

        If dtInsured Is Nothing Then
            dtInsured = dtInsd.Copy
            dtInsured.TableName = "GI"

            Dim ts As New clsDataGridTableStyle
            Dim cs As DataGridTextBoxColumn

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = strPrefix & "IED"
            cs.HeaderText = "Eff. Date From"
            cs.Format = "dd-MMM-yyyy"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = strPrefix & "IXD"
            cs.HeaderText = "Eff. Date To"
            cs.Format = "dd-MMM-yyyy"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = strPrefix & "CTD"
            cs.HeaderText = "Cover Type"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = strPrefix & "CLD"
            cs.HeaderText = "Cover Level"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = strPrefix & "SIC"
            cs.HeaderText = "Sum Insured"
            cs.Format = gNumFormat
            ts.GridColumnStyles.Add(cs)

            ts.MappingName = "GI"
            grdIns.TableStyles.Add(ts)

            grdIns.DataSource = dtInsured
            grdIns.AllowDrop = False
            grdIns.ReadOnly = True

            bm = Me.BindingContext(dtInsured)
            Call ParseMsg()

        Else
            Dim dr As DataRow
            Dim ar() As Object
            For Each dr In dtInsd.Rows
                ar = dr.ItemArray
                dtInsured.Rows.Add(ar)
            Next
        End If

    End Sub

    Private Sub UpdatePayment(ByVal dtPay As DataTable)

        If dtPayH Is Nothing Then
            dtPayH = dtPay.Copy
            dtPayH.TableName = "GI"

            For i As Integer = 0 To dtPayH.Rows.Count - 1
                With dtPayH.Rows(i)
                    .Item("P01TDT") = Format(Chg2Date(.Item("P01TDT")), gDateFormat)
                    .Item("P01CJD") = Format(Chg2Date(.Item("P01CJD")), gDateFormat)
                    .Item("P01MIB") = .Item("P01MIB") / 100
                    .Item("P01PMD") = .Item("P01PMD") / 100
                End With
            Next

            Dim ts As New clsDataGridTableStyle
            Dim cs As DataGridTextBoxColumn

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01TDT"
            cs.HeaderText = "Transaction Date"
            cs.Format = "dd-MMM-yyyy"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01TRC"
            cs.HeaderText = "Transaction Code"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01REN"
            cs.HeaderText = "Renewal Count"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01END"
            cs.HeaderText = "Endorsement Count"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01DCN"
            cs.HeaderText = "Document No."
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01OST"
            cs.HeaderText = "Offset Status"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01PFR"
            cs.HeaderText = "Payment Frequency"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01MIB"
            cs.HeaderText = "MIB Amount"
            cs.Format = gNumFormat
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01PMD"
            cs.HeaderText = "Premium Due Amount"
            cs.Format = gNumFormat
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01CJD"
            cs.HeaderText = "Contract Date"
            cs.Format = "dd-MMM-yyyy"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01RNO"
            cs.HeaderText = "Receipt Number"
            ts.GridColumnStyles.Add(cs)

            cs = New DataGridTextBoxColumn
            cs.Width = 100
            cs.MappingName = "P01PBY"
            cs.HeaderText = "Pay By"
            ts.GridColumnStyles.Add(cs)

            ts.MappingName = "GI"
            grdGI.TableStyles.Add(ts)

            grdGI.DataSource = dtPayH
            grdGI.AllowDrop = False
            grdGI.ReadOnly = True

        Else
            Dim dr As DataRow
            Dim ar() As Object
            For Each dr In dtPay.Rows
                ar = dr.ItemArray
                dtPayH.Rows.Add(ar)
            Next
        End If

    End Sub

    Private Sub UpdatePolicy(ByVal dtPol As DataTable)

        If dtPol.Rows.Count > 0 Then
            With dtPol.Rows(0)
                Me.txtPolicy.Text = .Item("X01POL")
                Me.txtPol.Text = .Item("X01POL")
                Me.txtPPolicy.Text = .Item("X01PVN")
                Me.txtRen.Text = .Item("X01REN")
                Me.txtEndor.Text = .Item("X01END")
                Me.txtMainClass.Text = .Item("X01MCC")
                Me.txtProduct.Text = .Item("X01PDD")
                Me.txtProd.Text = .Item("X01PDD")
                Me.txtTxCode.Text = .Item("X01TRC")
                Me.txtEffDt.Text = Format(Chg2Date(.Item("X01PCD")), gDateFormat)
                Me.txtCommDt.Text = Format(Chg2Date(.Item("X01PCD")), gDateFormat)
                Me.txtCurr.Text = .Item("X01CUR")
                Me.txtOcc.Text = .Item("X01OCP")
                Me.txtPayFq.Text = .Item("X01PFR")
                Me.txtBill.Text = .Item("X01BIL")
                Me.txtRenI.Text = .Item("X01RNI")
                Me.txtSTR.Text = .Item("X01SRI")
                Me.txtFIDt.Text = Format(Chg2Date(.Item("X01ICD")), gDateFormat)
                Me.txtAdmin.Text = Format(.Item("X01PAF") / 100, gNumFormat)
                Me.txtDisc.Text = Format((.Item("X01DSL") / 100), gNumFormat)
                Me.txtPolicyPD.Text = Format(Chg2Date(.Item("X01DPD")), gDateFormat)
                Me.txtCOVFdt.Text = Format(Chg2Date(.Item("X01PCD")), gDateFormat)
                Me.txtCOVTdt.Text = Format(Chg2Date(.Item("X01PXD")), gDateFormat)
                Me.txtFin.Text = .Item("X01FIN")

                If .Item("X01GRP") = "Y" Then
                    blnGrpMed = True
                End If
            End With
        End If

    End Sub

    Private Function Chg2Date(ByVal strDate As String) As Date

        Dim strTmp As String
        If Len(strDate) = 8 Then
            strTmp = Strings.Left(strDate, 4) + "/" + Mid(strDate, 5, 2) + "/" + Strings.Right(strDate, 2)
            Try
                Chg2Date = CDate(strTmp)
            Catch ex As Exception
                Chg2Date = #1/1/1900#
            End Try

        End If

    End Function

    Private Sub ParseMsg()

        Dim drI As DataRow

        drI = CType(bm.Current, DataRowView).Row()
        txtInsured.Text = ""

        For j As Integer = 0 To dtInsured.Columns.Count - 1
            dtFieldMap.DefaultView.RowFilter = "cswmgf_func = '" & strCurType & "' AND cswmgf_field_name = '" & dtInsured.Columns(j).ColumnName & "'"

            With dtFieldMap.DefaultView
                If .Count > 0 Then
                    If .Item(0).Item("cswmgf_disp_cs") = "Y" Then
                        txtInsured.Text &= .Item(0).Item("cswmgf_field_desc") & ": " & drI.Item(j) & vbCrLf
                    End If
                End If
            End With
        Next

    End Sub

    Private Sub bm_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bm.CurrentChanged
        ParseMsg()
    End Sub

    Private Sub grdIns_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdIns.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdIns.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdIns.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdIns.Select(hti.Row)
        End If
    End Sub

    Private Sub grdGI_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdGI.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdGI.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdGI.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdGI.Select(hti.Row)
        End If
    End Sub

End Class
