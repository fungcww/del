Imports CRS_Ctrl
Imports CRS_Util

Partial Public Class clsReportLogicMcu

    ''' <summary>
    '''Report: FCR Report
    ''' Lubin 2022-11-01 Move from HK Report logics to Macau.
    ''' </summary>
    Public Sub FCR_Report()

        Dim strSQL, strError As String
        Dim dtResult As DataTable
        Dim frmParam As New frmPremCallRpt

        blnCancel = True

        frmParam.CheckBox1.Visible = False
        frmParam.TextBox1.Visible = False
        frmParam.Text = "FCR Report"
        frmParam.txtPath.Text = "I:\Functional Log"

        If frmParam.ShowDialog() = DialogResult.OK Then

            If DateDiff(DateInterval.Month, frmParam.dtP1From.Value, frmParam.dtP1To.Value) > 1 Then
                If MsgBox("Date range over 2 months, sure to proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            Dim strFromDate As String = Format(frmParam.dtP1From.Value, "MM/dd/yyyy")
            Dim strToDate As String = Format(DateAdd(DateInterval.Day, 1, frmParam.dtP1To.Value), "MM/dd/yyyy")
            Try
                Dim expCnt As Integer = 0 ' Export result count.
                CRS_Component.WaitWndFun.Execute(ParentFrm, "Try to read database schema.",
                               Function()

                                   Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "CUR_DB_NAME", New Dictionary(Of String, String)() From {})
                                   Dim mcuDbName As String = retDs.Tables(0).Rows(0).Item(0)

                                   Dim serverPrefix As String = cSQL3_Mcu & "." & mcuDbName & ".dbo."

                                   CRS_Component.WaitWndFun.ShowMessage("Read data from CRSAPI Server...")
                                   retDs = APIServiceBL.CallAPIBusi(g_Comp, "REPORT_FCR_REPORT", New Dictionary(Of String, String)() From {
                                   {"EventInitialDateTimeStr", strFromDate & "~" & strToDate},
                                   {"DbStart", serverPrefix}
                                })

                                   dtResult = retDs.Tables(0).Copy
                                   CRS_Component.WaitWndFun.ShowMessage("Export the csv to folder...")
                                   ExportCSV(frmParam.txtPath.Text & "\FCRRpt_" & Format(Today, "MMddyyyy") & ".csv", dtResult, expCnt)
                               End Function)
                If (expCnt > 0) Then
                    MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                Else
                    MsgBox("No data is returned in the result.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End Try
            frmParam.Dispose()
            wndMain.Cursor = Cursors.Default

        Else
            frmParam.Dispose()
            Exit Sub
        End If

    End Sub
End Class
