Public Class uclParSur

    Private strPolicyNo As String
    Private strUserID As String
    Private strSystem As String
    Private strEnv As String
    Private strComp As String

    Private capDate As String = ""
    Private strErr As String = ""
    Private strMsg As String = ""
    Private HasChanged As Boolean = False
    Private strCovCurr As String = ""
    Private OrgAmt As Decimal = 0

    Private dsPolicyHead As New DataSet
    Private dsPFundDetails As New DataSet
    Private dsParSurHist As New DataSet
    Private dsSurAlloc As New DataSet

    Private WithEvents objCI As New LifeClientInterfaceComponent.clsPOS

    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private objPOSHeader As Utility.Utility.POSHeader
    Private objUtility As Utility.Utility
    Private SysEventLog As New SysEventLog.clsEventLog
    Private bAllowUpdate As Boolean
    Private gPlan As String

    Private dsReceClaim As New DataSet      ''Dual curr 202091115
    Private dsSendclaim As New DataSet      ''Dual curr 202091115


#Region "MQ Properties"
    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
        End Set
    End Property
#End Region
#Region " DBLogon Properties"
    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
#End Region

#Region " POS Properties"
    Public Property POSHeader() As Utility.Utility.POSHeader
        Get
            Return objPOSHeader
        End Get
        Set(ByVal value As Utility.Utility.POSHeader)
            objPOSHeader = value
        End Set
    End Property
#End Region

#Region " Authority Setting"
    Public Property AllowUpdate() As Boolean
        Get
            Return bAllowUpdate
        End Get
        Set(ByVal value As Boolean)
            bAllowUpdate = value
        End Set
    End Property
#End Region

#Region "Policy No"
    Public Property PolicyNumber() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property
#End Region

    '    Private Sub frmParSur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Public Sub InitInfo()
        'init setting
        Try
            'dtpEffDate.Enabled = False '@IDa-111108

            Ctrl_POS_Fund_SWOut.modeinuse = 1

            'Setup parameter
            objCI.MQQueuesHeader = Me.MQQueuesHeader
            objCI.DBHeader = Me.DBHeader
            objCI.POSHeader = Me.POSHeader

            'check if transaction no. exist or not
            If objPOSHeader.TransID <> 0 Then

                'Get partial surrender data
                If objCI.GetAllParSurrRecord(objPOSHeader.TransID, dsParSurHist, strErr) = False Then
                    If Trim(strErr) <> "" Then MsgBox(strErr)
                    strErr = ""
                    Exit Sub
                End If

                'Show policy information
                If dsParSurHist.Tables.Count = 0 Then
                    Exit Sub
                End If
                Me.PolicyNumber = dsParSurHist.Tables(0).Rows(0)("posph_policy_no")
                ShowPolicyRecord()

                'Show surrender allocation
                Dim strProductType As String = ""
                If dsPolicyHead.Tables.Count > 0 Then
                    strProductType = IIf(dsPolicyHead.Tables(0).Rows.Count <= 0, "", dsPolicyHead.Tables(0).Rows(0)("Code").ToString.Trim)
                End If

                If objCI.GetSurrRecord(Me.PolicyNumber, strProductType, dsParSurHist, dsSurAlloc, strErr) = False Then
                    If Trim(strErr) <> "" Then MsgBox(strErr)
                    strErr = ""
                    Exit Sub
                End If

                Dim dsFundList As New Data.DataSet

                'get fund currency from reporting module
                If Not objCI.GetAvailableFundList(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), dsPolicyHead.Tables(0).Rows(0)("Curr"), dsFundList, strErr) Then
                    If strErr.Trim = "" Then
                        MsgBox("Cannot get fund list table!", MsgBoxStyle.Critical)
                    Else
                        MsgBox(strErr, MsgBoxStyle.Critical)
                    End If
                    strErr = ""
                    Exit Sub
                Else
                    If dsFundList.Tables.Count < 2 Then
                        MsgBox("Incorrect fund list table", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If

                Ctrl_POS_Fund_SWOut.ProductTypeInuse = strProductType
                Ctrl_POS_Fund_SWOut.DBHeader = Me.objDBHeader
                Ctrl_POS_Fund_SWOut.currdsInuse = dsSurAlloc
                Ctrl_POS_Fund_SWOut.FundListdsInuse = dsFundList
                Ctrl_POS_Fund_SWOut.ShowPFundDetailsRcd()
                dsPFundDetails = Ctrl_POS_Fund_SWOut.currdsInuse


                'init setting
                capDate = Format(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), "MMM dd, yyyy")
                dtpEffDate.Text = capDate

                If objPOSHeader.TranStatus = "COMPLETED" Then
                    objPOSHeader.TransID = 0
                    btnChange.Enabled = False
                    btn_Quote.Enabled = False       ''Added 20091231

                Else
                    Call ChangeInfo()
                End If

            Else
                Call InitInfoTranID0()
            End If

            'LeoG1P1_S
            Dim tPlan = dsPolicyHead.Tables(0).Rows(0)("Code").ToString.Trim
            gPlan = tPlan
            'If tPlan <> "GFT" And tPlan <> "UFT" Then
            Me.RB_NA.Checked = True
            Select Case tPlan
                Case "GFT", "UFT"
                    Me.RB_NA.Enabled = False : Me.RB_P.Enabled = True : Me.RB_F.Enabled = True
                Case Else
                    Me.RB_NA.Enabled = True : Me.RB_P.Enabled = False : Me.RB_F.Enabled = False
            End Select
            'End If     'LeoG1P1_E

            ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
            ''CRS 7x24 Changes - Start
            'If ExternalUser Then
            '    btnRefresh.Enabled = False
            '    btnChange.Enabled = False
            '    btn_Quote.Enabled = False
            'End If
            ''CRS 7x24 Changes - End
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Call ChangeInfo()
    End Sub

    Private Sub ChangeInfo()
        Try
            Dim blnAllowchange As Boolean = False
            If objPOSHeader.TransID <> 0 Then
                'Check status
                If objCI.GetTransQStatus(objPOSHeader.TransID, objPOSHeader.TranType, blnAllowchange, strErr) = False Then
                    If Trim(strErr) <> "" Then MsgBox(strErr)
                    strErr = ""
                    Exit Sub
                End If
                If blnAllowchange = False Then
                    Ctrl_POS_Fund_SWOut.modeinuse = 1
                    MsgBox("This record is in used!")
                Else
                    Ctrl_POS_Fund_SWOut.modeinuse = 0
                    btnChange.Enabled = False
                    btn_Quote.Enabled = True

                    HasChanged = True
                End If
            Else
                Ctrl_POS_Fund_SWOut.modeinuse = 0
                btnChange.Enabled = False
                btn_Quote.Enabled = True
                HasChanged = True
            End If

        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub txtPolicyNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) '20090602 Jones Handles txtPolicyNo.LostFocus
    Private Sub InitInfoTranID0()
        Try
            'Call btnCancel_Click(sender, e)

            objPOSHeader.TransID = 0

            'Show Policy header info
            Ctrl_POS_Scrn_Head.policyInuse = Me.PolicyNumber
            Ctrl_POS_Scrn_Head.MQQueuesHeader = Me.objMQQueHeader
            Ctrl_POS_Scrn_Head.dbHeader = Me.DBHeader
            Ctrl_POS_Scrn_Head.ShowPolicyRcd()
            dsPolicyHead = Ctrl_POS_Scrn_Head.currdsInuse

            If dsParSurHist.Tables.Count > 0 Then
                For i As Integer = 0 To dsParSurHist.Tables.Count - 1
                    dsParSurHist.Tables(i).Clear()
                Next
            End If

            For i As Integer = 0 To dsSurAlloc.Tables.Count - 1
                dsSurAlloc.Tables.RemoveAt(0)
            Next

            'Show surrender allocation
            Ctrl_POS_Fund_SWOut.policyInuse = Me.PolicyNumber
            Dim strProductType As String = ""
            If dsPolicyHead.Tables.Count > 0 Then
                strProductType = IIf(dsPolicyHead.Tables(0).Rows.Count <= 0, "", dsPolicyHead.Tables(0).Rows(0)("Code").ToString.Trim)
            End If

            If objCI.GetSurrRecord(Me.PolicyNumber, strProductType, dsParSurHist, dsSurAlloc, strErr) = False Then
                If Trim(strErr) <> "" Then MsgBox(strErr)
                strErr = ""
                Exit Sub
            End If

            ''dual curr Added 20091209 HH +
            'If Not objCI.GetClaimsParSurAmt(txtPolicyNo.Text, dsSurAlloc, dsReceClaim, strErr) Then
            '    If Trim(strErr).Length > 0 Then MsgBox(strErr)
            '    strErr = ""
            'Else
            '    If dsReceClaim.Tables.Count > 0 AndAlso dsReceClaim.Tables(0).Rows.Count > 0 Then
            '        Me.Ctrl_POS_Fund_SWOut.TotalCalmableAmt = 0
            '        Me.Ctrl_POS_Fund_SWOut.TotalCalmAmt = 0
            '        Me.Ctrl_POS_Fund_SWOut.SurCharge = 0
            '    End If
            'End If
            'updateClaimValue()
            ''dual curr Added 20091209 HH -

            Dim dsFundList As New Data.DataSet

            'get fund currency from reporting module
            If Not objCI.GetAvailableFundList(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), dsPolicyHead.Tables(0).Rows(0)("Curr"), dsFundList, strErr) Then
                If strErr.Trim = "" Then
                    MsgBox("Cannot get fund list table!", MsgBoxStyle.Critical)
                Else
                    MsgBox(strErr, MsgBoxStyle.Critical)
                End If
                strErr = ""
                Exit Sub
            Else
                If dsFundList.Tables.Count < 2 Then
                    MsgBox("Incorrect fund list table", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            Ctrl_POS_Fund_SWOut.ProductTypeInuse = strProductType
            Ctrl_POS_Fund_SWOut.DBHeader = Me.objDBHeader
            Ctrl_POS_Fund_SWOut.currdsInuse = dsSurAlloc
            Ctrl_POS_Fund_SWOut.FundListdsInuse = dsFundList
            Ctrl_POS_Fund_SWOut.ShowPFundDetailsRcd()
            dsPFundDetails = Ctrl_POS_Fund_SWOut.currdsInuse
            If dsPolicyHead.Tables.Count > 0 Then
                capDate = Format(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), "MMM dd, yyyy")
            End If
            dtpEffDate.Text = capDate

            btnChange.Enabled = True
            'If bAllowUpdate = True Then
            '    btnChange.Enabled = True
            'Else
            '    btnChange.Enabled = False
            'End If
            btn_Quote.Enabled = False       ''Added 20091231

        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PreparePSSynDetailDS(ByVal dtPSDetail As Data.DataTable, ByVal dtParSurrAlloc As Data.DataTable, ByRef strErr As String) As Boolean
        Try
            'Create partial surrender table 
            objCI.CreateParSurrSynDetailDT(dtPSDetail)

            'Create surrender allocation table
            objCI.CreateParSurrAllocDT(dtParSurrAlloc)

            'Fill in table
            dtPSDetail.Rows(0)("posph_trans_id") = objPOSHeader.TransID
            dtPSDetail.Rows(0)("posph_user_id") = objDBHeader.UserID
            dtPSDetail.Rows(0)("posph_trans_datetime") = Format(Now, "MM/dd/yyyy HH:mm:ss")
            If IsDate(capDate) Then
                dtPSDetail.Rows(0)("posph_capsil_date") = Format(CDate(capDate), "MM/dd/yyyy")
            Else
                dtPSDetail.Rows(0)("posph_capsil_date") = System.DBNull.Value
            End If
            If IsDate(dtpEffDate.Text) Then
                dtPSDetail.Rows(0)("posph_eff_date") = Format(CDate(dtpEffDate.Text), "MM/dd/yyyy")
            Else
                dtPSDetail.Rows(0)("posph_eff_date") = System.DBNull.Value
            End If
            dtPSDetail.Rows(0)("posph_policy_no") = Me.PolicyNumber
            dtPSDetail.Rows(0)("posph_policy_cur") = dsPolicyHead.Tables(0).Rows(0)("Curr")
            dtPSDetail.Rows(0)("posph_ori_bal") = Ctrl_POS_Fund_SWOut.dblOriBal
            dtPSDetail.Rows(0)("posph_new_bal") = Ctrl_POS_Fund_SWOut.dblNewBal
            dtPSDetail.Rows(0)("posph_surr_amount") = Ctrl_POS_Fund_SWOut.dblSurrAmt
            dtPSDetail.Rows(0)("posph_type") = "0"
            dtPSDetail.Rows(0)("posph_status") = "Completed"
            For i As Integer = 1 To 10
                dtPSDetail.Rows(0)("posph_fund" & CStr(i)) = ""
                dtPSDetail.Rows(0)("posph_unit_price" & CStr(i)) = 0
            Next

            Dim WDMeth As String = ""  '@LeoG1P1
            If Me.RB_F.Checked = True Then
                WDMeth = "F"
            ElseIf Me.RB_P.Checked = True Then
                WDMeth = "P" '@LeoG1P1
            End If

            For i As Integer = 0 To dsPFundDetails.Tables(0).Rows.Count - 1
                Dim dr As DataRow = dtParSurrAlloc.NewRow
                dr("pospf_fund") = IIf(IsDBNull(dsPFundDetails.Tables(0).Rows(i)("Fund").ToString), "", dsPFundDetails.Tables(0).Rows(i)("Fund").ToString.Trim)
                dr("pospf_fund_type") = IIf(IsDBNull(dsPFundDetails.Tables(0).Rows(i)("Fund_Type").ToString), "", dsPFundDetails.Tables(0).Rows(i)("Fund_Type").ToString.Trim)
                dr("pospf_unit_bal") = IIf(IsDBNull(dsPFundDetails.Tables(0).Rows(i)("Unit")), 0, Format(Val(dsPFundDetails.Tables(0).Rows(i)("Unit")), "0.00"))
                dr("pospf_unit_price") = IIf(IsDBNull(dsPFundDetails.Tables(0).Rows(i)("UnitPrice")), 0, Format(Val(dsPFundDetails.Tables(0).Rows(i)("UnitPrice")), "0.00"))
                dr("pospf_sur_unit") = IIf(IsDBNull(dsPFundDetails.Tables(0).Rows(i)("SurUnit")), 0, Format(Val(dsPFundDetails.Tables(0).Rows(i)("SurUnit")), "0.00000"))
                dr("pospf_posph_id") = objPOSHeader.TransID
                dr("pospf_sur_amt") = IIf(IsDBNull(dsPFundDetails.Tables(0).Rows(i)("SurAmt")), 0, Format(Val(dsPFundDetails.Tables(0).Rows(i)("SurAmt")), "0.00"))
                dr("pospf_WDMethod") = WDMeth
                dtParSurrAlloc.Rows.Add(dr)
            Next

            If objPOSHeader.TransID = 0 Then
                dtPSDetail.Rows(0)("posph_crt_Usr") = objDBHeader.UserID
            Else
                dtPSDetail.Rows(0)("posph_crt_Usr") = dsParSurHist.Tables(0).Rows(0)("posph_crt_Usr")
            End If
            dtPSDetail.Rows(0)("posph_Upd_Usr") = objDBHeader.UserID
            dtPSDetail.Rows(0)("posph_Upd_Date") = Format(Now, "MM/dd/yyyy HH:mm:ss")
            dtPSDetail.Rows(0)("posph_Printed") = ""
            dtPSDetail.Rows(0)("posph_Po_sts") = ""
            dtPSDetail.Rows(0)("posph_po_substs") = ""
            dtPSDetail.Rows(0)("posph_switchin_id") = 0
            dtPSDetail.Rows(0)("posph_value_id") = 0
            dtPSDetail.Rows(0)("posph_adj_amount") = IIf(IsNumeric(Ctrl_POS_Fund_SWOut.dblAdjAmt), Ctrl_POS_Fund_SWOut.dblAdjAmt, 0)


            Return True
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub ShowPolicyRecord()
        Try
            Ctrl_POS_Scrn_Head.policyInuse = Me.PolicyNumber
            Ctrl_POS_Scrn_Head.dbHeader = Me.DBHeader
            Ctrl_POS_Scrn_Head.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_POS_Scrn_Head.ShowPolicyRcd()
            dsPolicyHead = Ctrl_POS_Scrn_Head.currdsInuse
            capDate = Format(CDate(dsPolicyHead.Tables(0).Rows(0)("Sys_Bus_Date")), "MMM dd, yyyy")
            dtpEffDate.Text = capDate
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Function PrepareParSurDS(ByRef dtSendData As Data.DataTable, ByRef strErr As String) As Boolean

    '    Try

    '        dtSendData.Columns.Add("PolicyNo")
    '        dtSendData.Columns.Add("Effective Date")
    '        dtSendData.Columns.Add("currency")
    '        For i As Integer = 1 To 10
    '            dtSendData.Columns.Add("Fund" & i)
    '            dtSendData.Columns.Add("Allocation" & i)
    '        Next

    '        dtSendData.Rows(0)("PolicyNo") = txtPolicyNo.Text.Trim
    '        dtSendData.Rows(0)("Effective Date") = capDate
    '        dtSendData.Rows(0)("currency") = strCovCurr
    '        For i As Integer = 1 To 10
    '            dtSendData.Rows(0)("Fund" & i) = dsPFundDetails.Tables(0).Rows(i - 1)("Fund").ToString
    '            dtSendData.Rows(0)("Allocation" & i) = dsPFundDetails.Tables(0).Rows(i - 1)("SurUnit").ToString
    '        Next

    '        Return True

    '    Catch ex As Exception
    '        strErr = ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Sub New()
        Try
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Controls.Add(Ctrl_POS_Fund_SWOut)
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'Call txtPolicyNo_LostFocus(sender, e)
        Call InitInfo()
    End Sub

    ''dual Curr Add 20091211 HH +
    Private Sub updateClaimValue()

        Dim tRefDate As String = Format(CDate(dtpEffDate.Text), "yyyyMMdd") '@IDa+111109
        Dim tWDMethod As String = " "
        If Me.RB_F.Checked = True Then
            tWDMethod = "F"
        ElseIf Me.RB_P.Checked = True Then
            tWDMethod = "P"
        End If
        Select Case gPlan
            Case "GFT", "UFT"
                If tWDMethod = " " Then
                    MsgBox("Please check/select Withdrawal Method!")
                    Exit Sub
                End If
            Case Else
        End Select

        If Not objCI.GetClaimsParSurAmt(Me.PolicyNumber, dsSurAlloc, dsReceClaim, strErr, tRefDate, tWDMethod) Then   'LeoG1P1
            If Trim(strErr).Length > 0 Then MsgBox(strErr)
            strErr = ""
        Else
            If dsReceClaim.Tables.Count > 0 AndAlso dsReceClaim.Tables(0).Rows.Count > 0 Then
                Me.Ctrl_POS_Fund_SWOut.TotalCalmableAmt = dsReceClaim.Tables(0).Rows(0).Item("TotClaimableAmt")
                Me.Ctrl_POS_Fund_SWOut.TotalCalmAmt = dsReceClaim.Tables(0).Rows(0).Item("TotClamAmt")
                Me.Ctrl_POS_Fund_SWOut.SurCharge = dsReceClaim.Tables(0).Rows(0).Item("SurCharge")
                Me.Ctrl_POS_Fund_SWOut.Principle = dsReceClaim.Tables(0).Rows(0).Item("Principle") '@IDa+111102
                Me.Ctrl_POS_Fund_SWOut.Interest = dsReceClaim.Tables(0).Rows(0).Item("Interest") '@IDa+111102
                Me.Ctrl_POS_Fund_SWOut.NewSI = dsReceClaim.Tables(0).Rows(0).Item("NewSI") 'LeoG1P1
            Else
                Me.Ctrl_POS_Fund_SWOut.TotalCalmableAmt = 0
                Me.Ctrl_POS_Fund_SWOut.TotalCalmAmt = 0
                Me.Ctrl_POS_Fund_SWOut.SurCharge = 0
                Me.Ctrl_POS_Fund_SWOut.Principle = 0 '@IDa+111102
                Me.Ctrl_POS_Fund_SWOut.Interest = 0 '@IDa+111102
                Me.Ctrl_POS_Fund_SWOut.NewSI = 0 'LeoG1P1
            End If
            Me.Ctrl_POS_Fund_SWOut.UpdateClaimAmt()
        End If

    End Sub
    ''dual Curr Add 20091211 HH +

    Private Sub btn_Quote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Quote.Click
        updateClaimValue()
    End Sub


End Class
