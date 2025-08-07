<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchPolicyMcu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    Try
    '        If disposing AndAlso components IsNot Nothing Then
    '            components.Dispose()
    '        End If
    '    Finally
    '        MyBase.Dispose(disposing)
    '    End Try
    'End Sub

    'Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()> _
    'Private Sub InitializeComponent()
    '    Me.grdPolicy = New System.Windows.Forms.DataGrid()
    '    Me.Button1 = New System.Windows.Forms.Button()
    '    Me.sboxPolicy = New CS2005.SelectBox()
    '    Me.cmdSearch = New System.Windows.Forms.Button()
    '    Me.cmdClear = New System.Windows.Forms.Button()
    '    Me.cmdOpen = New System.Windows.Forms.Button()
    '    Me.cmdClose = New System.Windows.Forms.Button()
    '    Me.Panel1 = New System.Windows.Forms.Panel()
    '    Me.Splitter1 = New System.Windows.Forms.Splitter()
    '    Me.Panel2 = New System.Windows.Forms.Panel()
    '    CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
    '    Me.Panel1.SuspendLayout()
    '    Me.Panel2.SuspendLayout()
    '    Me.SuspendLayout()
    '    '
    '    'grdPolicy
    '    '
    '    Me.grdPolicy.AlternatingBackColor = System.Drawing.Color.White
    '    Me.grdPolicy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
    '                Or System.Windows.Forms.AnchorStyles.Left) _
    '                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.grdPolicy.BackColor = System.Drawing.Color.White
    '    Me.grdPolicy.BackgroundColor = System.Drawing.Color.Ivory
    '    Me.grdPolicy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.grdPolicy.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
    '    Me.grdPolicy.CaptionForeColor = System.Drawing.Color.Lavender
    '    Me.grdPolicy.CaptionVisible = False
    '    Me.grdPolicy.DataMember = ""
    '    Me.grdPolicy.FlatMode = True
    '    Me.grdPolicy.Font = New System.Drawing.Font("Tahoma", 8.0!)
    '    Me.grdPolicy.ForeColor = System.Drawing.Color.Black
    '    Me.grdPolicy.GridLineColor = System.Drawing.Color.Wheat
    '    Me.grdPolicy.HeaderBackColor = System.Drawing.Color.CadetBlue
    '    Me.grdPolicy.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
    '    Me.grdPolicy.HeaderForeColor = System.Drawing.Color.Black
    '    Me.grdPolicy.LinkColor = System.Drawing.Color.DarkSlateBlue
    '    Me.grdPolicy.Location = New System.Drawing.Point(13, 12)
    '    Me.grdPolicy.Name = "grdPolicy"
    '    Me.grdPolicy.ParentRowsBackColor = System.Drawing.Color.Ivory
    '    Me.grdPolicy.ParentRowsForeColor = System.Drawing.Color.Black
    '    Me.grdPolicy.SelectionBackColor = System.Drawing.Color.Wheat
    '    Me.grdPolicy.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
    '    Me.grdPolicy.Size = New System.Drawing.Size(576, 234)
    '    Me.grdPolicy.TabIndex = 3
    '    '
    '    'Button1
    '    '
    '    Me.Button1.Location = New System.Drawing.Point(627, 70)
    '    Me.Button1.Name = "Button1"
    '    Me.Button1.Size = New System.Drawing.Size(120, 34)
    '    Me.Button1.TabIndex = 10
    '    Me.Button1.Text = "Quic&k Open"
    '    Me.Button1.Visible = False
    '    '
    '    'sboxPolicy
    '    '
    '    Me.sboxPolicy.DefaultOp = "like"
    '    Me.sboxPolicy.FieldName = "PolicyAccountID"
    '    Me.sboxPolicy.LabelText = "Policy No."
    '    Me.sboxPolicy.Location = New System.Drawing.Point(13, 12)
    '    Me.sboxPolicy.Name = "sboxPolicy"
    '    Me.sboxPolicy.Size = New System.Drawing.Size(1101, 46)
    '    Me.sboxPolicy.TabIndex = 1
    '    '
    '    'cmdSearch
    '    '
    '    Me.cmdSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.cmdSearch.Location = New System.Drawing.Point(601, 140)
    '    Me.cmdSearch.Name = "cmdSearch"
    '    Me.cmdSearch.Size = New System.Drawing.Size(141, 34)
    '    Me.cmdSearch.TabIndex = 6
    '    Me.cmdSearch.Text = "&Search"
    '    '
    '    'cmdClear
    '    '
    '    Me.cmdClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.cmdClear.Location = New System.Drawing.Point(601, 187)
    '    Me.cmdClear.Name = "cmdClear"
    '    Me.cmdClear.Size = New System.Drawing.Size(141, 34)
    '    Me.cmdClear.TabIndex = 7
    '    Me.cmdClear.Text = "&Clear All"
    '    '
    '    'cmdOpen
    '    '
    '    Me.cmdOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.cmdOpen.Enabled = False
    '    Me.cmdOpen.Location = New System.Drawing.Point(601, 23)
    '    Me.cmdOpen.Name = "cmdOpen"
    '    Me.cmdOpen.Size = New System.Drawing.Size(141, 34)
    '    Me.cmdOpen.TabIndex = 8
    '    Me.cmdOpen.Text = "&Open"
    '    '
    '    'cmdClose
    '    '
    '    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.cmdClose.Location = New System.Drawing.Point(601, 70)
    '    Me.cmdClose.Name = "cmdClose"
    '    Me.cmdClose.Size = New System.Drawing.Size(141, 34)
    '    Me.cmdClose.TabIndex = 9
    '    Me.cmdClose.Text = "Cl&ose"
    '    '
    '    'Panel1
    '    '
    '    Me.Panel1.Controls.Add(Me.cmdSearch)
    '    Me.Panel1.Controls.Add(Me.cmdClear)
    '    Me.Panel1.Controls.Add(Me.cmdOpen)
    '    Me.Panel1.Controls.Add(Me.cmdClose)
    '    Me.Panel1.Controls.Add(Me.grdPolicy)
    '    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    '    Me.Panel1.Name = "Panel1"
    '    Me.Panel1.Size = New System.Drawing.Size(768, 257)
    '    Me.Panel1.TabIndex = 10
    '    '
    '    'Splitter1
    '    '
    '    Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.Splitter1.Location = New System.Drawing.Point(0, 257)
    '    Me.Splitter1.Name = "Splitter1"
    '    Me.Splitter1.Size = New System.Drawing.Size(768, 5)
    '    Me.Splitter1.TabIndex = 11
    '    Me.Splitter1.TabStop = False
    '    '
    '    'Panel2
    '    '
    '    Me.Panel2.AutoScroll = True
    '    Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    '    Me.Panel2.Controls.Add(Me.sboxPolicy)
    '    Me.Panel2.Controls.Add(Me.Button1)
    '    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.Panel2.Location = New System.Drawing.Point(0, 262)
    '    Me.Panel2.Name = "Panel2"
    '    Me.Panel2.Size = New System.Drawing.Size(768, 191)
    '    Me.Panel2.TabIndex = 12
    '    '
    '    'frmSearchPolicyMac
    '    '
    '    Me.AutoScaleBaseSize = New System.Drawing.Size(8, 19)
    '    Me.AutoScroll = True
    '    Me.ClientSize = New System.Drawing.Size(768, 453)
    '    Me.Controls.Add(Me.Panel2)
    '    Me.Controls.Add(Me.Splitter1)
    '    Me.Controls.Add(Me.Panel1)
    '    Me.Name = "frmSearchPolicyMac"
    '    Me.Text = "Search Policy Macau"
    '    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    '    CType(Me.grdPolicy, System.ComponentModel.ISupportInitialize).EndInit()
    '    Me.Panel1.ResumeLayout(False)
    '    Me.Panel2.ResumeLayout(False)
    '    Me.ResumeLayout(False)

    'End Sub
End Class
