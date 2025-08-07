Imports System.Data.SqlClient
Imports CRS_Ctrl
Imports CRS_Util

Partial Public Class clsReportLogicMcu
    'Service_Medium_CSR() - Prepare data for Service Medium by Initiator report

    ''' <summary>
    ''' Report: Service medium by CSR report
    ''' Lubin 2022-11-01 Move from HK Report logics to Macau.
    ''' </summary>
    Public Sub Service_Medium_CSR_rpt()
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
               Dim retDs As DataSet   = APIServiceBL.CallAPIBusi(g_McuComp, "REPORT_SERVICE_MEDIUM_CSR", New Dictionary(Of String, String)() From {
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

        Try
            rpt.SetDataSource(dsSrvLog)
            rpt.SetParameterValue("FromDate", strFromDate)
            rpt.SetParameterValue("ToDate", strToDate)
        Catch e As Exception
            MsgBox("Exception: " & e.Message, MsgBoxStyle.Exclamation, "Generating Report")
        End Try

        blnCancel = False

    End Sub
   
End Class
