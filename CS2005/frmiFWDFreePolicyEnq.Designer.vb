<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmiFWDFreePolicyEnq
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lbl_HKID = New System.Windows.Forms.Label
        Me.txt_HKID = New System.Windows.Forms.TextBox
        Me.lbl_PolicyNo = New System.Windows.Forms.Label
        Me.txt_PolicyNo = New System.Windows.Forms.TextBox
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.grdPolicy = New System.Windows.Forms.DataGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_PolicyInfo = New System.Windows.Forms.Label
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.grdInsured = New System.Windows.Forms.DataGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbl_InsuredInfo = New System.Windows.Forms.Label
        Me.grdBeneficiary = New System.Windows.Forms.DataGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lbl_BeneficiaryInfo = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.grdInsured, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.grdBeneficiary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnClear)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_HKID)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_HKID)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_PolicyNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_PolicyNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 573)
        Me.SplitContainer1.SplitterDistance = 67
        Me.SplitContainer1.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(236, 33)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lbl_HKID
        '
        Me.lbl_HKID.AutoSize = True
        Me.lbl_HKID.Location = New System.Drawing.Point(12, 38)
        Me.lbl_HKID.Name = "lbl_HKID"
        Me.lbl_HKID.Size = New System.Drawing.Size(33, 13)
        Me.lbl_HKID.TabIndex = 3
        Me.lbl_HKID.Text = "HKID"
        '
        'txt_HKID
        '
        Me.txt_HKID.Location = New System.Drawing.Point(109, 35)
        Me.txt_HKID.Name = "txt_HKID"
        Me.txt_HKID.Size = New System.Drawing.Size(100, 20)
        Me.txt_HKID.TabIndex = 2
        '
        'lbl_PolicyNo
        '
        Me.lbl_PolicyNo.AutoSize = True
        Me.lbl_PolicyNo.Location = New System.Drawing.Point(12, 12)
        Me.lbl_PolicyNo.Name = "lbl_PolicyNo"
        Me.lbl_PolicyNo.Size = New System.Drawing.Size(55, 13)
        Me.lbl_PolicyNo.TabIndex = 1
        Me.lbl_PolicyNo.Text = "Policy No."
        '
        'txt_PolicyNo
        '
        Me.txt_PolicyNo.Location = New System.Drawing.Point(109, 9)
        Me.txt_PolicyNo.Name = "txt_PolicyNo"
        Me.txt_PolicyNo.Size = New System.Drawing.Size(100, 20)
        Me.txt_PolicyNo.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.grdPolicy)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(792, 502)
        Me.SplitContainer2.SplitterDistance = 162
        Me.SplitContainer2.TabIndex = 0
        '
        'grdPolicy
        '
        Me.grdPolicy.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPolicy.BackColor = System.Drawing.Color.White
        Me.grdPolicy.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPolicy.CaptionVisible = False
        Me.grdPolicy.DataMember = ""
        Me.grdPolicy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPolicy.FlatMode = True
        Me.grdPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPolicy.ForeColor = System.Drawing.Color.Black
        Me.grdPolicy.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPolicy.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.Location = New System.Drawing.Point(0, 24)
        Me.grdPolicy.Name = "grdPolicy"
        Me.grdPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPolicy.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPolicy.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolicy.Size = New System.Drawing.Size(792, 138)
        Me.grdPolicy.TabIndex = 21
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_PolicyInfo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(792, 24)
        Me.Panel1.TabIndex = 0
        '
        'lbl_PolicyInfo
        '
        Me.lbl_PolicyInfo.AutoSize = True
        Me.lbl_PolicyInfo.Location = New System.Drawing.Point(3, 4)
        Me.lbl_PolicyInfo.Name = "lbl_PolicyInfo"
        Me.lbl_PolicyInfo.Size = New System.Drawing.Size(90, 13)
        Me.lbl_PolicyInfo.TabIndex = 0
        Me.lbl_PolicyInfo.Text = "Policy Information"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.grdInsured)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.grdBeneficiary)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Panel3)
        Me.SplitContainer3.Size = New System.Drawing.Size(792, 336)
        Me.SplitContainer3.SplitterDistance = 161
        Me.SplitContainer3.TabIndex = 0
        '
        'grdInsured
        '
        Me.grdInsured.AlternatingBackColor = System.Drawing.Color.White
        Me.grdInsured.BackColor = System.Drawing.Color.White
        Me.grdInsured.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdInsured.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdInsured.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInsured.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdInsured.CaptionVisible = False
        Me.grdInsured.DataMember = ""
        Me.grdInsured.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdInsured.FlatMode = True
        Me.grdInsured.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdInsured.ForeColor = System.Drawing.Color.Black
        Me.grdInsured.GridLineColor = System.Drawing.Color.Wheat
        Me.grdInsured.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdInsured.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdInsured.HeaderForeColor = System.Drawing.Color.Black
        Me.grdInsured.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInsured.Location = New System.Drawing.Point(0, 24)
        Me.grdInsured.Name = "grdInsured"
        Me.grdInsured.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdInsured.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdInsured.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdInsured.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdInsured.Size = New System.Drawing.Size(792, 137)
        Me.grdInsured.TabIndex = 23
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_InsuredInfo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(792, 24)
        Me.Panel2.TabIndex = 22
        '
        'lbl_InsuredInfo
        '
        Me.lbl_InsuredInfo.AutoSize = True
        Me.lbl_InsuredInfo.Location = New System.Drawing.Point(3, 4)
        Me.lbl_InsuredInfo.Name = "lbl_InsuredInfo"
        Me.lbl_InsuredInfo.Size = New System.Drawing.Size(97, 13)
        Me.lbl_InsuredInfo.TabIndex = 0
        Me.lbl_InsuredInfo.Text = "Insured Information"
        '
        'grdBeneficiary
        '
        Me.grdBeneficiary.AlternatingBackColor = System.Drawing.Color.White
        Me.grdBeneficiary.BackColor = System.Drawing.Color.White
        Me.grdBeneficiary.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdBeneficiary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdBeneficiary.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBeneficiary.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdBeneficiary.CaptionVisible = False
        Me.grdBeneficiary.DataMember = ""
        Me.grdBeneficiary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdBeneficiary.FlatMode = True
        Me.grdBeneficiary.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdBeneficiary.ForeColor = System.Drawing.Color.Black
        Me.grdBeneficiary.GridLineColor = System.Drawing.Color.Wheat
        Me.grdBeneficiary.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdBeneficiary.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdBeneficiary.HeaderForeColor = System.Drawing.Color.Black
        Me.grdBeneficiary.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBeneficiary.Location = New System.Drawing.Point(0, 24)
        Me.grdBeneficiary.Name = "grdBeneficiary"
        Me.grdBeneficiary.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdBeneficiary.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdBeneficiary.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdBeneficiary.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdBeneficiary.Size = New System.Drawing.Size(792, 147)
        Me.grdBeneficiary.TabIndex = 23
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lbl_BeneficiaryInfo)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(792, 24)
        Me.Panel3.TabIndex = 22
        '
        'lbl_BeneficiaryInfo
        '
        Me.lbl_BeneficiaryInfo.AutoSize = True
        Me.lbl_BeneficiaryInfo.Location = New System.Drawing.Point(3, 4)
        Me.lbl_BeneficiaryInfo.Name = "lbl_BeneficiaryInfo"
        Me.lbl_BeneficiaryInfo.Size = New System.Drawing.Size(114, 13)
        Me.lbl_BeneficiaryInfo.TabIndex = 0
        Me.lbl_BeneficiaryInfo.Text = "Beneficiary Information"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(317, 33)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "&Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmiFWDFreePolicyEnq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmiFWDFreePolicyEnq"
        Me.Text = "iFWD Free Policy Enquiry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.grdInsured, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.grdBeneficiary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lbl_HKID As System.Windows.Forms.Label
    Friend WithEvents txt_HKID As System.Windows.Forms.TextBox
    Friend WithEvents lbl_PolicyNo As System.Windows.Forms.Label
    Friend WithEvents txt_PolicyNo As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_PolicyInfo As System.Windows.Forms.Label
    Friend WithEvents grdPolicy As System.Windows.Forms.DataGrid
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents grdInsured As System.Windows.Forms.DataGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_InsuredInfo As System.Windows.Forms.Label
    Friend WithEvents grdBeneficiary As System.Windows.Forms.DataGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lbl_BeneficiaryInfo As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
