Imports CRS_Ctrl
Imports CRS_Util

Partial Public Class clsReportLogicMcu
    'Medium_Rpt() - Prepare data for Service Nature Medium Report

    ''' <summary>
    ''' Report: Service nature medium report
    ''' Lubin 2022-11-01 Move from HK Report logics to Macau.
    ''' </summary>
    Public Sub Service_Nature_Medium_Rpt()
        Dim frmParam As New frmRptSrvAdmin
        Dim strFromDate As String
        Dim strToDate As String
        Dim dsSrvLog As New DataSet

        blnCancel = True
        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtFrom.Value, "yyyy-MM-dd")
            strToDate = Format(frmParam.dtTo.Value, "yyyy-MM-dd")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Sub
        End If

        CRS_Component.WaitWndFun.Execute(ParentFrm, "Try to read data from backend.",
       Function()
           Try

               CRS_Component.WaitWndFun.ShowMessage("Read data from CRSAPI Server...")
               Dim retDs As DataSet   = APIServiceBL.CallAPIBusi(g_McuComp, "REPORT_SERVICE_NATURE_MEDIUM", New Dictionary(Of String, String)() From {
                     {"EventInitialDateTimeStr", strFromDate & "~" & strToDate}
                 })

               dsSrvLog=retDs

                retDs  = APIServiceBL.CallAPIBusi(g_McuComp, "LOG_POLICY", New Dictionary(Of String, String)() From {
                {"PolicyNo", "9999999"}
                })
               dsSrvLog.Tables.Add(retDs.Tables(0).Copy())

           Catch ex As Exception
               MsgBox(ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
           End Try
       End Function)
         If dsSrvLog.Tables("csw_service_log_count").Rows.Count=0 Then
                   MsgBox("There result is empty", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                   Exit Sub
         End If
        Try
            rpt.SetDataSource(dsSrvLog)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        dsSrvLog.Dispose()

        blnCancel = False

    End Sub

End Class
