Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmRptClaims
    Inherits System.Windows.Forms.Form

    Dim dsReport As New DataSet
    Dim oRpt As ReportDocument

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
        'dsReport.Dispose()
        'clsRpt = Nothing
        'oRpt.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtClaimNo As System.Windows.Forms.TextBox
    Friend WithEvents txtClaimOccur As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtClaimNo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtClaimOccur = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Claim Number"
        '
        'txtClaimNo
        '
        Me.txtClaimNo.Location = New System.Drawing.Point(92, 12)
        Me.txtClaimNo.Name = "txtClaimNo"
        Me.txtClaimNo.Size = New System.Drawing.Size(96, 20)
        Me.txtClaimNo.TabIndex = 1
        Me.txtClaimNo.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Claim Occur"
        '
        'txtClaimOccur
        '
        Me.txtClaimOccur.Location = New System.Drawing.Point(92, 40)
        Me.txtClaimOccur.Name = "txtClaimOccur"
        Me.txtClaimOccur.Size = New System.Drawing.Size(96, 20)
        Me.txtClaimOccur.TabIndex = 3
        Me.txtClaimOccur.Text = ""
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(20, 76)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(76, 24)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(100, 76)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        '
        'frmRptClaims
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(204, 117)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtClaimOccur)
        Me.Controls.Add(Me.txtClaimNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmRptClaims"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Claims Breakdown Report"
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    '    Dim strClaimNo As String
    '    Dim strClaimOccur As String
    '    Dim blnIsLastRpt As Boolean = False

    '    strClaimNo = txtClaimNo.Text
    '    strClaimOccur = txtClaimOccur.Text
    '    clsRpt.GenClaimsBreakdownReport(strClaimNo, strClaimOccur, dsReport, blnIsLastRpt)

    '    ' Add the parameter to the parameter fields collection.
    '    oRpt = New ReportDocument
    '    oRpt.Load(Application.StartupPath & "\ClaimBreakdownCharges.rpt")
    '    oRpt.SetParameterValue("ClaimNo", strClaimNo)
    '    oRpt.SetParameterValue("ClaimOccur", strClaimOccur)
    '    oRpt.SetParameterValue("UserName", gsUser)
    '    oRpt.SetParameterValue("IsLastRpt", blnIsLastRpt)

    '    'Generate report into Crystal Report Viewer
    '    oRpt.SetDataSource(dsReport)
    '    crvReport.ReportSource = oRpt

    'End Sub
End Class
