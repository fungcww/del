Public Class frmPostSalesCallList

    Public objDBHeader As Utility.Utility.ComHeader
    Private clsCRS As LifeClientInterfaceComponent.clsCRS
    Public sCompId As String = "HK"

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        If txtFileName.Text.Trim() = String.Empty Then
            MsgBox("Please input file name.", MsgBoxStyle.Exclamation, "Message")
            Return
        End If

        GenerateCallList()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmPostSalesCallListParm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFrom.Value = DateTime.Now.AddDays(-7)
        dtpTo.Value = DateTime.Now

        'check the access right
        If Not CheckUPSAccessFunc("Generate Post-Sales Call List(Macau)") Then
            RadioMC.Enabled = False
        Else
            RadioMC.Checked = True
        End If

        If Not CheckUPSAccessFunc("Generate Post-Sales Call List(HK)") Then
            RadioHK.Enabled = False
        Else
            RadioHK.Checked = True
        End If

        If "BMU".Equals(sCompId) Then
            RadioHK.Checked = True
            RadioMC.Enabled = False
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        If RadioMC.Checked Then
            SaveFileDialog1.FileName = String.Format("Macau_PostSaleCall{0}.xlsx", Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture))
        Else
            SaveFileDialog1.FileName = String.Format("PostSaleCall{0}.xlsx", Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture))
        End If

        If SaveFileDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK Then Return

        txtFileName.Text = SaveFileDialog1.FileName
    End Sub


    Private Sub GenerateCallList()
        Dim strMethodName As String = "PostSalesCallList"

        'oliver 2023-12-4 updated for Switch Over Code from Assurance to Bermuda 
        'Dim rptLogic As New clsReportLogic()
        Dim rptLogic
        If IsAssurance Then
            rptLogic = New clsReportLogic_Asur()
        Else
            rptLogic = New clsReportLogic()
        End If

        rptLogic.objDBHeader = objDBHeader
        If "BMU".Equals(sCompId) Then
            rptLogic.PostSalesCallList(dtpFrom.Value, dtpTo.Value, txtFileName.Text.Trim(), sCompId)
        ElseIf RadioMC.Checked Then
            rptLogic.PostSalesCallList(dtpFrom.Value, dtpTo.Value, txtFileName.Text.Trim(), "MC")
        Else
            rptLogic.PostSalesCallList(dtpFrom.Value, dtpTo.Value, txtFileName.Text.Trim(), "HK")
        End If
    End Sub


End Class