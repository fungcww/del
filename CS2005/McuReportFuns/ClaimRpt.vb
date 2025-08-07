Imports CRS_Ctrl
Imports CRS_Util

Partial Public Class clsReportLogicMcu
    Private Function GetDefaultDR(ds As DataSet) As DataRow
        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)
        Else

            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Report: Minor Claim Breakdown Charges
    ''' Lubin 2022-11-01 Move from HK Report logics to Macau.
    ''' </summary>
    Public Sub Claim_rpt()
        Dim frmParam As New frmRptClaims
        Dim dsClaimRpt As New DataSet
        Dim blnIsLastRpt As Boolean
        Dim strClaimNo As String
        Dim strClaimOccur As String

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            strClaimNo = frmParam.txtClaimNo.Text
            strClaimOccur = frmParam.txtClaimOccur.Text
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        If strClaimNo = "" Or strClaimOccur = "" Then
            MsgBox("Please input both Claim No. and Claim Occur.", MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
            Exit Sub
        End If

        Dim serverPrefix As String = cSQL3_Mcu
        CRS_Component.WaitWndFun.Execute(ParentFrm, "Loading data from backend...",
         Function()
             try
             CRS_Component.WaitWndFun.ShowMessage("Read data from CRSAPI Server...")
             Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "REPORT_CLAIM_STATUS", New Dictionary(Of String, String)() From {
                 {"strClaimNo", strClaimNo},
                 {"strClaimOccur", strClaimOccur},
                 {"DbStart", serverPrefix}
              })
             Dim retDr As DataRow = GetDefaultDR(retDs)
             If Not IsNothing(retDr) Then
                 If IsDBNull(retDr.Item("mcschd_clm_status")) Then
                     MsgBox("Claim Breakdown Charges report with the claim no " & strClaimNo & " and claim occur " & strClaimOccur & " cannot be generated as criteria not match.", MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
                     Exit Function
                 ElseIf (retDr.Item("mcschd_clm_status") <> "WFW" And retDr.Item("mcschd_clm_status") <> "FIN") Or
                         (retDr.Item("mcschd_converted") <> 0) Or (Not IsDBNull(retDr.Item("mcsmsl_message_no")) AndAlso retDr.Item("mcsmsl_message_no") = "F402") Then
                     MsgBox("Claim Breakdown Charges report with the claim no " & strClaimNo & " and claim occur " & strClaimOccur & " cannot be generated as criteria not match.", MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
                     Exit Function
                 End If
             Else
                 MsgBox("Cannot find the claim information with the claim no " & strClaimNo & " and claim occur " & strClaimOccur, MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
                 Exit Function
             End If


             CRS_Component.WaitWndFun.ShowMessage("Read  max(mcschd_claim_occur) from CRSAPI Server...")
             retDs = APIServiceBL.CallAPIBusi(g_McuComp, "REPORT_CLAIM_MAX_OCCUR", New Dictionary(Of String, String)() From {
                 {"strClaimNo", strClaimNo},
                 {"DbStart", serverPrefix}
              })

             retDr = GetDefaultDR(retDs)
             If IsNothing(retDr) Then
                 MsgBox("Cannot find the claim information with the claim no " & strClaimNo & " and claim occur " & strClaimOccur, MsgBoxStyle.Exclamation, "Claim Breakdown Charges")
                 Exit Function
             End If
             retDs = APIServiceBL.CallAPIBusi(g_McuComp, "LOG_POLICY", New Dictionary(Of String, String)() From {
            {"PolicyNo", "-99999"}
            })
             dsClaimRpt.Tables.Add(retDs.Tables(0).Copy())

             If String.Equals(Convert.ToString(retDr.Item(0)), strClaimOccur) Then
                 blnIsLastRpt = True
             Else
                 blnIsLastRpt = False
                 MsgBox("Remain balance will not be displayed as " + strClaimOccur + " is not the latest claim occur for claim no " + strClaimNo + ".", MsgBoxStyle.Information, "Claim Breakdown Charges")
                 Exit Function
             End If

             'Find product name (mcu?)
             CRS_Component.WaitWndFun.ShowMessage("Read products from CRSAPI Server...")
             retDs = APIServiceBL.CallAPIBusi(g_McuComp, "REPORT_PRODUCT", New Dictionary(Of String, String)() From {})
             dsClaimRpt.Tables.Add(retDs.Tables(0).Copy)

             'Preparing report data
             CRS_Component.WaitWndFun.ShowMessage("Preparing report data from CRSAPI Server...")
             retDs = APIServiceBL.CallAPIBusi(g_McuComp, "REPORT_CLAIM_BREAKDOWN", New Dictionary(Of String, String)()  From {
                 {"strClaimNo",strClaimNo},
                 {"strClaimOccur",strClaimOccur}
                 })
             dsClaimRpt.Tables.Add(retDs.Tables(0).Copy)
             'Set parameter information
             rpt.SetDataSource(dsClaimRpt)
             rpt.SetParameterValue("ClaimNo", strClaimNo)
             rpt.SetParameterValue("ClaimOccur", strClaimOccur)
             rpt.SetParameterValue("UserName", gsUser)
             rpt.SetParameterValue("IsLastRpt", blnIsLastRpt)
             blnCancel = False

             Catch ex As Exception
                  MsgBox(ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
             End try
         End Function)

    End Sub
End Class
