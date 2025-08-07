<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WaitForm
    Inherits System.Windows.Forms.Form
    
        Private WithEvents mainPanel As System.Windows.Forms.Panel
        Private WithEvents tipTitleLab As System.Windows.Forms.Label
        Private WithEvents label1 As System.Windows.Forms.Label
        Private WithEvents tipMessageLab As System.Windows.Forms.Label

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WaitForm))
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.timePassedLab = New System.Windows.Forms.Label()
        Me.tipMessageLab = New System.Windows.Forms.Label()
        Me.tipTitleLab = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.localTimer = New System.Windows.Forms.Timer(Me.components)
        Me.mainPanel.SuspendLayout
        Me.SuspendLayout
        '
        'mainPanel
        '
        Me.mainPanel.Controls.Add(Me.timePassedLab)
        Me.mainPanel.Controls.Add(Me.tipMessageLab)
        Me.mainPanel.Controls.Add(Me.tipTitleLab)
        Me.mainPanel.Controls.Add(Me.label1)
        Me.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainPanel.Location = New System.Drawing.Point(0, 0)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(713, 164)
        Me.mainPanel.TabIndex = 0
        '
        'timePassedLab
        '
        Me.timePassedLab.AutoSize = true
        Me.timePassedLab.BackColor = System.Drawing.Color.Transparent
        Me.timePassedLab.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.timePassedLab.ForeColor = System.Drawing.Color.Coral
        Me.timePassedLab.Location = New System.Drawing.Point(659, 12)
        Me.timePassedLab.Name = "timePassedLab"
        Me.timePassedLab.Size = New System.Drawing.Size(25, 25)
        Me.timePassedLab.TabIndex = 2
        Me.timePassedLab.Text = "1"
        '
        'tipMessageLab
        '
        Me.tipMessageLab.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tipMessageLab.Location = New System.Drawing.Point(168, 60)
        Me.tipMessageLab.Name = "tipMessageLab"
        Me.tipMessageLab.Size = New System.Drawing.Size(533, 95)
        Me.tipMessageLab.TabIndex = 1
        '
        'tipTitleLab
        '
        Me.tipTitleLab.AutoSize = true
        Me.tipTitleLab.Font = New System.Drawing.Font("Microsoft YaHei", 21!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134,Byte))
        Me.tipTitleLab.Location = New System.Drawing.Point(302, 9)
        Me.tipTitleLab.Name = "tipTitleLab"
        Me.tipTitleLab.Size = New System.Drawing.Size(197, 37)
        Me.tipTitleLab.TabIndex = 0
        Me.tipTitleLab.Text = "Please wait..."
        '
        'label1
        '
        Me.label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.label1.Image = CType(resources.GetObject("label1.Image"),System.Drawing.Image)
        Me.label1.Location = New System.Drawing.Point(0, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(176, 164)
        Me.label1.TabIndex = 0
        '
        'localTimer
        '
        '
        'WaitForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(241,Byte),Integer), CType(CType(206,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(713, 164)
        Me.Controls.Add(Me.mainPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "WaitForm"
        Me.ShowInTaskbar = false
        Me.Text = "WaitForm"
        Me.mainPanel.ResumeLayout(false)
        Me.mainPanel.PerformLayout
        Me.ResumeLayout(false)

End Sub

    Private WithEvents timePassedLab As Label
    Friend WithEvents localTimer As Timer
End Class


