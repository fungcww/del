'---------------------------------------------------------------------------------------------------------------------------------------
'VER    DATE            AUTH        Ref No.             Description
'001    06 Jun 2023     Gavin Wu    ITSR3162 & 4101     Print & Save letter

Imports System.Configuration
Imports System.IO
Imports PdfPrintingNet

Public Class LetterPrintBL

    ''' <summary>
    ''' Print file
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="printerName"></param>
    ''' <returns></returns>
    Public Function PrintFile(ByVal filePath As String, ByVal printerName As String) As Boolean
        Dim printResult As Boolean = False
        Try
            If Not File.Exists(filePath) Then
                Throw New Exception(String.Format("The file path does not exist, please check it. -{0}", filePath))
            End If

            Dim companyName As String = ConfigurationManager.AppSettings("PdfPrintCompanyName").ToString()
            Dim licenseKey As String = ConfigurationManager.AppSettings("PdfPrintLicenseKey").ToString()

            Dim pdfPrint As PdfPrint = New PdfPrint(companyName, licenseKey)
            pdfPrint.IsAutoRotate = True

            If String.IsNullOrWhiteSpace(printerName) Then
                pdfPrint.PrinterName = "Microsoft Print to PDF"
            End If

            Dim pdfPrintStatus As PdfPrint.Status = pdfPrint.Print(filePath, "", Path.GetFileNameWithoutExtension(filePath))
            If pdfPrintStatus = PdfPrint.Status.OK Then
                printResult = True
                File.Delete(filePath)
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("Fail to print file: {0}", ex.Message))
        End Try

        Return printResult
    End Function

    ''' <summary>
    ''' Save file as ...
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    Public Function SaveFileTo(ByVal filePath As String) As Boolean
        Dim result As Boolean = False
        Try
            If Not File.Exists(filePath) Then
                Throw New Exception(String.Format("The file path does not exist, please check it. -{0}", filePath))
            End If

            Dim fileExt As String = System.IO.Path.GetExtension(filePath).Replace(".", "")
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            sfd.DefaultExt = fileExt

            Dim saveType As String = String.Empty
            Select Case fileExt
                Case "pdf"
                    saveType = "PDF(*.pdf)|*.pdf"
                Case "xlsx"
                    saveType = "Excel Workbook (*.xlsx)|*.xlsx"
                Case "docx"
                    saveType = "Word Document (*.docx)|*.docx"
            End Select
            sfd.Filter = saveType

            Dim status = sfd.ShowDialog()
            Dim path As String = sfd.FileName
            If status = DialogResult.OK Then
                File.Move(filePath, path)
                result = True
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("Fail to print file: {0}", ex.Message))
        End Try

        Return result
    End Function

End Class
