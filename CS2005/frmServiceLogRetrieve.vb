Public Class frmServiceLogRetrieve

    Dim ServiceLogBL As New ServiceLogBL
    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Dim isNBMPolicy As Boolean = False

    'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
    Public Property IsNBM() As Boolean
        Get
            Return isNBMPolicy
        End Get
        Set(ByVal Value As Boolean)
            Me.Text = "Service Log Retrieve(NBM)"
            RadioAssurance.Enabled = False
            RadioMC.Enabled = False
            RadioGI.Enabled = False
            RadioEB.Enabled = False
            RadioALL.Enabled = False
            isNBMPolicy = Value
        End Set
    End Property

    Private Sub frmServiceLogRetrive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'oliver 2023-12-4 updated for Switch Over Code from Assurance to Bermuda 
        If Not IsAssurance Then
            RadioALL.Enabled = False
        End If

        Dim retriveDate As Date = DateSerial(Now().Year, Now().Month, 1)
        Dim startDate As Date = DateSerial(retriveDate.AddMonths(-1).Year, retriveDate.AddMonths(-1).Month, 1)
        Dim endDate As Date = retriveDate.AddDays(-1)
        dpkStartDate.Value = startDate
        dpkEndDate.Value = endDate
        Initialize_DefaultMedium()
        Initialize_EventCategory()

        'check the access right
        If Not CheckUPSAccessFunc("Service Log Retrieve(Macau)") Then
            RadioMC.Enabled = False
        Else
            RadioMC.Checked = True
        End If

        If Not CheckUPSAccessFunc("Service Log Retrieve(HK)") Then
            RadioBermuda.Enabled = False
            RadioAssurance.Enabled = False
            RadioGI.Enabled = False
            RadioEB.Enabled = False
        Else
            RadioBermuda.Checked = True
        End If
    End Sub

    ''' <summary>
    ''' Oliver 2023-8-8 Updated,add 'All' radio button which can select all.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnretriveserlog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnretriveserlog.Click
        Dim dsresult As New DataSet
        Dim strErr As String = String.Empty
        Dim enddate As Date = dpkEndDate.Value
        enddate = enddate.AddDays(1)

        Dim type As String = String.Empty
        Dim dataName As String = String.Empty
        If RadioBermuda.Checked Then
            type = "Bermuda"
            dataName = "Bermuda_ServiceLog"
        ElseIf RadioAssurance.Checked Then
            type = "Assurance"
            dataName = "Assurance_ServiceLog"
        ElseIf RadioMC.Checked Then
            type = "Macau"
            dataName = "Macau_ServiceLog"
        ElseIf RadioGI.Checked Then
            type = "GI"
            dataName = "GI_ServiceLog"
        ElseIf RadioEB.Checked Then
            type = "EB"
            dataName = "EB_ServiceLog"

            'Oliver 2023-8-8 Updated
        ElseIf RadioALL.Checked Then
            type = "ALL"
            dataName = "ALL_ServiceLog"

            'oliver 2024-8-2 added for Com 6
        ElseIf RadioHnw.Checked Then
            If Not CheckUserRight("CRS_HNW_USER") Then
                MsgBox($"{gsUser} has no Private permission , You’re not authorized to access.")
                Exit Sub
            End If
            type = "BMU"
            dataName = "Private_ServiceLog"
        End If

        ' oliver 2023-12-15 commented for Switch Over Code from Assurance to Bermuda 
        ' oliver 2023-8-10 Updated
        'If Not type = "ALL" Then
        '    If Not ServiceLogBL.GetSingleSerLogDataSetByAPI(type, dpkStartDate.Value, enddate, cbomedium.SelectedValue, cboeventcat.SelectedValue, txtcustid.Text.Trim, chknoncust.Checked, dsresult, strErr) Then
        '        MessageBox.Show(strErr)
        '        Exit Sub
        '    End If
        'Else
        '    If Not ServiceLogBL.GetAllSerLogDataSetByAPI(dpkStartDate.Value, enddate, cbomedium.SelectedValue, cboeventcat.SelectedValue, txtcustid.Text.Trim, chknoncust.Checked, dsresult, strErr) Then
        '        MessageBox.Show(strErr)
        '        Exit Sub
        '    End If
        'End If

        'oliver 2023-12-4 updated for Switch Over Code from Assurance to Bermuda 
        If IsAssurance Then
            'Oliver 2023-8-10 Updated
            If Not type = "ALL" Then
                If Not ServiceLogBL.GetSingleSerLogDataSetByAPI(type, dpkStartDate.Value, enddate, cbomedium.SelectedValue, cboeventcat.SelectedValue, txtcustid.Text.Trim, chknoncust.Checked, dsresult, strErr, isNBMPolicy) Then
                    MessageBox.Show(strErr)
                    Exit Sub
                End If
            Else
                If Not ServiceLogBL.GetAllSerLogDataSetByAPI(dpkStartDate.Value, enddate, cbomedium.SelectedValue, cboeventcat.SelectedValue, txtcustid.Text.Trim, chknoncust.Checked, dsresult, strErr) Then
                    MessageBox.Show(strErr)
                    Exit Sub
                End If
            End If
            ServiceLogBL.GenerateCsv(dataName, dsresult)
        Else
            'oliver 2024-01-11 added for ITSR5061 Retention Offer Campaign 
            If isNBMPolicy Then
                If Not ServiceLogBL.GetSingleSerLogDataSetByAPI(type, dpkStartDate.Value, enddate, cbomedium.SelectedValue, cboeventcat.SelectedValue, txtcustid.Text.Trim, chknoncust.Checked, dsresult, strErr, isNBMPolicy) Then
                    MessageBox.Show(strErr)
                    Exit Sub
                End If
            Else
                If Not ServiceLogBL.GetSerLogbycriteriaNew(dpkStartDate.Value, enddate, cbomedium.SelectedValue, cboeventcat.SelectedValue, txtcustid.Text.Trim, chknoncust.Checked, False, dsresult, strErr, type) Then
                    MessageBox.Show("btnretriveserlog_Click : " & strErr)
                    Exit Sub
                End If
            End If
            ServiceLogBL.GenerateCsv(dataName, dsresult)
        End If

    End Sub

    Private Sub Initialize_DefaultMedium()
        Dim dtMedium As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetMedium(dtMedium, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            dtMedium.Rows.Add()
            dtMedium.Rows(dtMedium.Rows.Count - 1)("EventSourceMediumCode") = ""
            dtMedium.Rows(dtMedium.Rows.Count - 1)("Medium") = ""
            dtMedium.AcceptChanges()
            cbomedium.SelectedItem = String.Empty
            cbomedium.Items.Clear()
            cbomedium.DataSource = dtMedium
            cbomedium.ValueMember = "EventSourceMediumCode"
            cbomedium.DisplayMember = "Medium"
            cbomedium.SelectedValue = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub Initialize_EventCategory()
        Dim dtEvtCat As New DataTable
        Dim strErr As String = String.Empty
        Try
            ServiceLogBL.GetEventCategory(dtEvtCat, strErr)
            If strErr <> String.Empty Then
                MessageBox.Show(strErr, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) : Exit Sub
            End If
            'oliver 2024-6-21 updated for Table_Relocate_Sprint13
            dtEvtCat.Rows.Add("", "")
            'dtEvtCat.Rows(dtEvtCat.Rows.Count - 1)("cswecc_code") = ""
            'dtEvtCat.Rows(dtEvtCat.Rows.Count - 1)("EventCat") = ""
            dtEvtCat.AcceptChanges()
            cboeventcat.DataSource = dtEvtCat
            cboeventcat.ValueMember = "cswecc_code"
            cboeventcat.DisplayMember = "EventCat"
            cboeventcat.SelectedValue = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message & " </br>    " & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub
End Class