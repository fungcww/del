Public Class frmLevyOutstandingForm

    Public policyNumber As String = ""
    Public isNew As Boolean = True

    Public means As String = ""
    Public status As Boolean = False
    Public count As String = ""
    Public remarks As String = ""
    Public overdueId As String = ""
    Public zlvysysForLevyOverdue As String = ""
    Public ccdateForLevyOverdue As String = ""
    Public tranNoForLevyOverdue As String = ""


    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Public objMQQueHeader As Utility.Utility.MQHeader      'MQQueHeader includes MQ conn. parameters
    Public objDBHeader As Utility.Utility.ComHeader         'DBHeader includes MSSQL conn. pararmeters
    Public objPOSHeader As Utility.Utility.POSHeader       'POSHeader indicates transaction id and type

    Private Sub frmLevyOutstandingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        clsPOS.MQQueuesHeader = objMQQueHeader
        clsPOS.DBHeader = objDBHeader
        clsPOS.CiwHeader = objDBHeader

        setLevyOverdueFollowUpType()
        setLevyCboStatus()
        initalData()

        txtPolicyNumber.Text = policyNumber

        If (Not isNew) Then
            SetupdateData()
        End If

    End Sub

    Private Sub SetUpdateData()

        txtPolicyNumber.Text = policyNumber

        cboMeans.SelectedValue = means

        If (status) Then
            cboStatus.SelectedIndex = 0
        Else
            cboStatus.SelectedIndex = 1
        End If

        lbOverdueId.Text = overdueId
        txtRemarks.Text = remarks
        txtCount.Text = count

    End Sub

    Private Sub initalData()

        txtPolicyNumber.Text = ""
        cboMeans.SelectedIndex = 0
        cboStatus.SelectedIndex = 0
        lbOverdueId.Text = ""
        txtRemarks.Text = ""
        txtCount.Text = ""

    End Sub

    Private Sub setLevyOverdueFollowUpType()

        Dim ds As New DataSet
        Dim strErr As String = ""

        clsPOS.GetLevyOverdueFollowUpType(ds, strErr)
        Me.cboMeans.DataSource = ds.Tables(0)
        Me.cboMeans.DisplayMember = "OverdueFollowUpTypeName"
        Me.cboMeans.ValueMember = "OverdueFollowUpTypeId"

    End Sub

    Private Sub setLevyCboStatus()

        Dim cboStatusSource As New Dictionary(Of String, String)()
        cboStatusSource.Add("Success", "Success")
        cboStatusSource.Add("Fail", "Fail")

        Me.cboStatus.DataSource = New BindingSource(cboStatusSource, Nothing)
        Me.cboStatus.DisplayMember = "Value"
        Me.cboStatus.ValueMember = "Key"
        Me.cboStatus.SelectedValue = "Success"

    End Sub

  

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()

    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim strErr As String = ""

        If (isNew) Then
            clsPOS.InsertLevyOverdueFollowUp(zlvysysForLevyOverdue, policyNumber, _
                                                ccdateForLevyOverdue, tranNoForLevyOverdue, _
                                                cboMeans.SelectedValue, txtRemarks.Text, GetCboStatus(), strErr)
        Else
            clsPOS.UpdateLevyOverdueFollowUp(policyNumber, lbOverdueId.Text, cboMeans.SelectedValue, txtRemarks.Text, GetCboStatus(), strErr)
        End If

        If (strErr.Trim <> "") Then
            MsgBox(strErr)
        End If

        MsgBox("Levy overdue follow up record is saved")
        Me.Close()

    End Sub

    Private Function GetCboStatus() As Boolean

        If (cboStatus.SelectedValue = "Success") Then
            Return True
        End If

        Return False

    End Function



End Class