Public Class frmCRM_Asur
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.TabControl1.Controls.Clear()
        If CheckUPSAccess("Campaign Detail") Then Me.TabControl1.Controls.Add(Me.TabPage1)
        If CheckUPSAccess("Sales Leads") Then Me.TabControl1.Controls.Add(Me.TabPage2)
        If CheckUPSAccess("Campaign Execution") Then Me.TabControl1.Controls.Add(Me.TabPage3)
        If CheckUPSAccess("EDM") Then Me.TabControl1.Controls.Add(Me.TabPage4)

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
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents UclCampaign1 As CS2005.uclCampaign
    Friend WithEvents UclQuery1 As CS2005.uclQuery
    Friend WithEvents UclExecute2 As CS2005.uclExecute_Asur
    Friend WithEvents UclEDM1 As CS2005.uclEDM
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.UclCampaign1 = New CS2005.uclCampaign
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.UclQuery1 = New CS2005.uclQuery
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.UclExecute2 = New CS2005.uclExecute_Asur
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.UclEDM1 = New CS2005.uclEDM
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(728, 477)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.UclCampaign1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(720, 451)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Tag = "CampaignDetail"
        Me.TabPage1.Text = "Campaign Detail"
        '
        'UclCampaign1
        '
        Me.UclCampaign1.Location = New System.Drawing.Point(0, 0)
        Me.UclCampaign1.Name = "UclCampaign1"
        Me.UclCampaign1.Size = New System.Drawing.Size(712, 452)
        Me.UclCampaign1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.UclQuery1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(720, 451)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Tag = "SalesLeads"
        Me.TabPage2.Text = "Sales Leads"
        Me.TabPage2.Visible = False
        '
        'UclQuery1
        '
        Me.UclQuery1.Location = New System.Drawing.Point(0, 0)
        Me.UclQuery1.Name = "UclQuery1"
        Me.UclQuery1.Size = New System.Drawing.Size(720, 468)
        Me.UclQuery1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.UclExecute2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(720, 451)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Tag = "CampaignExec"
        Me.TabPage3.Text = "Campaign Execution"
        Me.TabPage3.Visible = False
        '
        'UclExecute2
        '
        Me.UclExecute2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UclExecute2.Location = New System.Drawing.Point(0, 0)
        Me.UclExecute2.Name = "UclExecute2"
        Me.UclExecute2.Size = New System.Drawing.Size(708, 452)
        Me.UclExecute2.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.UclEDM1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(720, 451)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Tag = "EDM"
        Me.TabPage4.Text = "EDM"
        Me.TabPage4.Visible = False
        '
        'UclEDM1
        '
        Me.UclEDM1.dtMail = Nothing
        Me.UclEDM1.Location = New System.Drawing.Point(0, 0)
        Me.UclEDM1.Name = "UclEDM1"
        Me.UclEDM1.Size = New System.Drawing.Size(664, 452)
        Me.UclEDM1.TabIndex = 0
        '
        'frmCRM
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(728, 477)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmCRM"
        Me.Text = "Campaign Management"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmCRM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' to fix the Webbrowser control problem
        TabPage4.Show()
        TabPage3.Show()
        TabPage1.Show()

        gCrmMode = ""

        Try
            Call Me.UclCampaign1.InitControl()
            Call Me.UclEDM1.initEDM()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

        If TabControl1.SelectedTab.Tag = "CampaignDetail" Then
            If gCrmMode = "" Then
                UclCampaign1.Enabled = True
            Else
                If gCrmMode <> "CampaignDetail" Then
                    UclCampaign1.Enabled = False
                End If
            End If
        End If

        If TabControl1.SelectedTab.Tag = "SalesLeads" Then
            If gCrmMode = "" Then
                UclQuery1.Enabled = True
                Call UclQuery1.InitControl()
            Else
                If gCrmMode <> "SalesLeads" Then
                    UclQuery1.Enabled = False
                End If
            End If
        End If

        If TabControl1.SelectedTab.Tag = "CampaignExec" Then
            If gCrmMode = "" Then
                UclExecute2.Enabled = True
                Call Me.UclExecute2.InitControl()
            Else
                If gCrmMode <> "CampaignExec" Then
                    UclExecute2.Enabled = False
                End If
            End If
        End If

        If TabControl1.SelectedTab.Tag = "EDM" Then
            If gCrmMode = "" Then
                UclEDM1.Enabled = True
                Call Me.UclEDM1.initEDM()
            Else
                If gCrmMode <> "EDM" Then
                    UclEDM1.Enabled = False
                End If
            End If
        End If
    End Sub

End Class
