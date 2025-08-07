Public Class ctrl_FinancialDtl
    Private strPolicy As String = ""
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objdbHeader As Utility.Utility.ComHeader
    Private WithEvents objCI As New LifeClientInterfaceComponent.clsPOS
    Private dsReinstateEnq As New DataSet
    Private Mode As ModeName        'Indicate state

    Public Enum ModeName
        Enquiry
        Update
        Search
    End Enum
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
    Public Property modeinuse() As ModeName
        Get
            Return Mode
        End Get
        Set(ByVal value As ModeName)
            Mode = value
            Call RefreshButton()
        End Set
    End Property
    Public Property PolicyNoInUse() As String
        Get
            Return strPolicy
        End Get
        Set(ByVal value As String)
            strPolicy = value
        End Set
    End Property
    Public Sub showrestatmentInfo()
        Dim strErr As String = ""
        Dim dsSendData As New DataSet
        Dim dtSendData As New DataTable
        Dim dr As DataRow
        dtSendData.Columns.Add("PolicyNo")

        dtSendData.Columns.Add("EffectiveDate", GetType(System.DateTime))

        dr = dtSendData.NewRow
        dr("PolicyNo") = strPolicy.Trim
        'dtSendData.Rows.Add(dr)

        'dr = dtSendData.NewRow
        'dr("EffectiveDate") = Format(Now, "yyyy/MM/dd") 'Format(Now, "yyyyMMdd") 'Format(Now, "MMddyyyy")
        If dtpEffDate.Text = "" Then
            dr("EffectiveDate") = Format(Now, "yyyy/MM/dd")
            dtpEffDate.Text = Format(Now, "MMM dd, yyyy")
        Else
            dr("EffectiveDate") = Format(CDate(dtpEffDate.Text), "yyyy/MM/dd")
        End If
        'dr("EffectiveDate") = Format(Now, "yyyy/MM/dd") 'Format(Now, "yyyyMMdd") 'Format(Now, "MMddyyyy")
        'dr("EffectiveDate") = Format(CDate(dtpEffDate.Text), "MMddyyyy")
        dtSendData.Rows.Add(dr)

        dsSendData.Tables.Add(dtSendData)
        objCI.MQQueuesHeader = Me.objMQQueHeader
        objCI.DBHeader = Me.objdbHeader
        If objCI.GetReinstateEnquiry(dsSendData, dsReinstateEnq, strErr) = False Then
            If Trim(strErr) <> "" Then
                MsgBox(strErr)
                Exit Sub
            End If

        End If

        txtRegPrem.Text = Format(CDec(dsReinstateEnq.Tables(0).Rows(0)("Reg_Prem")), "#.##")
        dtpPTD.Text = Format(getPaidToDate(), "MMM dd, yyyy") 'Format(CDate(Ctrl_POS_Scrn_Head.currdsInuse.Tables(0).Rows(0)("Paid_To_Date")), "MMM dd, yyyy")
        txtBillingCurr.Text = dsReinstateEnq.Tables(0).Rows(0)("Bill_Curr")
        dtpBTD.Text = Format(CDate(dsReinstateEnq.Tables(0).Rows(0)("BTD")), "MMM dd,yyyy")
        dptProjPTD.Text = Format(CDate(dsReinstateEnq.Tables(0).Rows(0)("Proj_PTD")), "MMM dd,yyyy")
        txtBillCurrSuspense.Text = Format(CDec(dsReinstateEnq.Tables(0).Rows(0)("Suspense_B")), "#.##")
        txtIntRate.Text = dsReinstateEnq.Tables(0).Rows(0)("Int_Rate")

        txtTotOutStdPrem.Text = dsReinstateEnq.Tables(0).Rows(0)("Out_Prem_B")
        txtReinstFee.Text = Format(CDec(dsReinstateEnq.Tables(0).Rows(0)("Re_Fee_B")), "#.##")
        txtAdjAmt.Text = Format(CDec(dsReinstateEnq.Tables(0).Rows(0)("Adj_Amt_B")), "#.##")
        txtTotal.Text = Format(CDec(dsReinstateEnq.Tables(0).Rows(0)("Total_B")), "#.##")
        txtTolerance.Text = Format(CDec(dsReinstateEnq.Tables(0).Rows(0)("Tolerance_B")), "#.##")

    End Sub

    Public Function getPaidToDate() As Date
        Dim dsSendData As New DataSet
        Dim dsCurr As New DataSet
        Dim strTime As String = ""
        Dim strerr As String = ""
        Dim clsPOS As New LifeClientInterfaceComponent.clsPOS
        Dim blnGetPolicy As Boolean
        blnGetPolicy = clsPOS.GetPolicy(dsSendData, dsCurr, strTime, strErr)
        If blnGetPolicy Then
            getPaidToDate = dsCurr.Tables(0).Rows(0)("Paid_To_Date")
        Else
            getPaidToDate = Now
        End If
    End Function
    Public Sub RefreshButton()
        If Mode = ModeName.Enquiry Then
            Dim cControl As Control
            Dim cControltb As TextBox
            For Each cControl In Me.Controls 'InMe.Controls            
                If (TypeOf cControl Is TextBox) Then
                    cControltb = cControl
                    cControltb.ReadOnly = True
                ElseIf (TypeOf cControl Is DateTimePicker) Then
                    cControl.Enabled = False
                End If
            Next cControl
        ElseIf Mode = ModeName.Search Then
            Dim cControl As Control
            Dim cControltb As TextBox
            For Each cControl In Me.Controls 'InMe.Controls            
                If (TypeOf cControl Is TextBox) Then
                    cControltb = cControl
                    cControltb.ReadOnly = True

                ElseIf (TypeOf cControl Is DateTimePicker) Then
                    If cControl.Name <> "dtpEffDate" Then
                        cControl.Enabled = False
                    Else
                        cControl.Enabled = True
                    End If
                End If
            Next cControl

            'Dim cControl As Control
            'For Each cControl In Me.Controls 'InMe.Controls            
            '    'If (TypeOf cControl Is TextBox) Then
            '    cControl.Enabled = False
            '    'End If
            'Next cControl
        End If
    End Sub

    Private Sub dtpEffDate_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpEffDate.CloseUp
        showrestatmentInfo()
    End Sub

    Private Sub dtpEffDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpEffDate.Leave

    End Sub

End Class
