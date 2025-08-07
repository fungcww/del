Imports CRS_Ctrl
Imports CRS_Util

Partial Public Class clsReportLogicMcu
    ''' <summary>
    ''' Report: Suggestions/grievances report
    ''' Lubin 2022-11-01 Move from HK Report logics to Macau.
    ''' </summary>
    Public Function ACC_REPORT()

        Dim strSQL, strError As String
        Dim dtResult As DataTable
        Dim frmParam As New frmPremCallRpt
        Dim strFromDate As String
        Dim strToDate As String

        blnCancel = True

        frmParam.CheckBox1.Visible = False
        frmParam.TextBox1.Visible = False
        'frmParam.Text = "Agent Call to Customer Hotline Report"
        frmParam.Text = "Suggestions/Grievances Report"
        frmParam.txtPath.Text = "I:\Functional Log"

        frmParam.dtP1From.Format = DateTimePickerFormat.Custom
        frmParam.dtP1From.CustomFormat = "MM/dd/yyyy HH:mm:ss"
        frmParam.dtP1To.Format = DateTimePickerFormat.Custom
        frmParam.dtP1To.CustomFormat = "MM/dd/yyyy HH:mm:ss"

        If frmParam.ShowDialog() = DialogResult.OK Then
            strFromDate = Format(frmParam.dtP1From.Value, "MM/dd/yyyy HH:mm:ss")
            strToDate = Format(frmParam.dtP1To.Value, "MM/dd/yyyy HH:mm:ss")
            frmParam.Dispose()
        Else
            frmParam.Dispose()
            Exit Function
        End If

        If DateDiff(DateInterval.Month, frmParam.dtP1From.Value, frmParam.dtP1To.Value) > 1 Then
            If MsgBox("Date range over 2 months, sure to proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Function
            End If
        End If

        wndMain.Cursor = Cursors.WaitCursor
        Try
            Dim expCnt As Integer=0 ' Export result count.
            CRS_Component.WaitWndFun.Execute(ParentFrm, "Try to read database schema.",
                               Function()

                               'get mcu db name at first.
                               Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "CUR_DB_NAME", New Dictionary(Of String, String)() From {})
                                   Dim mcuDbName As String = retDs.Tables(0).Rows(0).Item(0)

                                   Dim serverPrefix As String = cSQL3_Mcu & "." & mcuDbName & ".dbo."

                                   CRS_Component.WaitWndFun.ShowMessage("Read data from CRSAPI Server...")
                                   retDs = APIServiceBL.CallAPIBusi(g_Comp, "ACC_REPORT", New Dictionary(Of String, String)() From {
                                   {"EventInitialDateTimeStr", strFromDate & "~" & strToDate},
                                   {"DbStart", serverPrefix}
                                })

                                   dtResult = retDs.Tables(0).Copy
                                   CRS_Component.WaitWndFun.ShowMessage("Export the csv to folder...")
                                   ExportCSV(frmParam.txtPath.Text & "\ACCRpt_" & Format(Today, "MMddyyyy") & ".csv", dtResult,expCnt)

                               End Function)
            If(expCnt>0) Then
                MsgBox("Report generated to " & frmParam.txtPath.Text & " sucessfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            Else
                MsgBox("No data is returned in the result.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
        End Try
        frmParam.Dispose()
        wndMain.Cursor = Cursors.Default
    End Function
End Class
