using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Configuration;

namespace McupInLetter
{
    class Program
    {
        static void Main(string[] args)
        {

            var templateDocumentPath = ConfigurationSettings.AppSettings["templateDocumentPath"].ToString();
            var txtFilePath = ConfigurationSettings.AppSettings["mcupInLetterHKPath"].ToString();
            var logPath = ConfigurationSettings.AppSettings["logPath"].ToString();
            var printerName = ConfigurationSettings.AppSettings["printerName"].ToString();
            var i = 0;

            try
            {
                writeLog(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "---MCUPINLETTER START");
                //var megeWordings = GetMergeWording();
                var megeWordings = GetMergeWording(txtFilePath);

                if (!File.Exists(templateDocumentPath))
                    throw new Exception("Template document is not existed ");

                if (!File.Exists(txtFilePath))
                    throw new Exception("MCUPINLetterHK.TXT is not existed ");
            

                foreach (var megeWording in megeWordings)
                {
                    MailMerge(megeWording, templateDocumentPath, i.ToString(), printerName);
                    i++;
                }

                writeLog(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "---Done. Total: " + megeWordings.Count() + " records.");

            }
            catch (Exception ex)
            {
                writeLog(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "---" + ex.ToString());
            }
            finally {
                writeLog(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "---MCUPINLETTER End");
            }
        }

        static void writeLog(string path, string message) {

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(message);
                }
            }
        }

        static List<mergeWording> GetMergeWording(string txtFile)
        {
            var megeWordings = new List<mergeWording>();
            var i = 0;

            string[] lines = System.IO.File.ReadAllLines(@txtFile, System.Text.Encoding.GetEncoding(950));


            foreach (var line in lines)
            {
                if (i == 0 && line.Trim() != "!$PINLetter")
                    throw new Exception("Header: !$PINLetter is not existed");

                if (i == 1 && line.Trim() != "%%PROG: C:\\ctrsys\\MCUPINLetter.JDT")
                    throw new Exception("Header: %%PROG: C:\\ctrsys\\MCUPINLetter.JDT is not existed");

                if (i == lines.Length -1 && line.Trim() != "%%EOJ")
                    throw new Exception("Footer: %%EOJ is not existed");

                // Use a tab to indent each line of the file.
                if (i != 0 && i != 1 && i != lines.Length -1)
                {
                    var megeWordingInLines = line.Split('|');
                    var megeWording = new mergeWording();

                    if (megeWordingInLines[2].ToString().Trim() == "") megeWording.F_83_name = " "; else megeWording.F_83_name = megeWordingInLines[2].ToString();
                    if (megeWordingInLines[4].ToString().Trim() == "") megeWording.F_63_addr1 = " "; else megeWording.F_63_addr1 = megeWordingInLines[4].ToString();
                    if (megeWordingInLines[5].ToString().Trim() == "") megeWording.F_14_addr2 = " "; else megeWording.F_14_addr2 = megeWordingInLines[5].ToString();
                    if (megeWordingInLines[6].ToString().Trim() == "") megeWording.F_7_addr3 = " "; else megeWording.F_7_addr3 = megeWordingInLines[6].ToString();
                    if (megeWordingInLines[7].ToString().Trim() == "") megeWording.F_43_addr4 = " "; else megeWording.F_43_addr4 = megeWordingInLines[7].ToString();
                    if (megeWordingInLines[5].ToString().Trim() == "") megeWording.F_57_PrintDate = " " ; else megeWording.F_57_PrintDate = megeWordingInLines[5].ToString();
                    if (megeWordingInLines[2].ToString().Trim() == "") megeWording.F_91_name = " " ; else megeWording.F_91_name = megeWordingInLines[2].ToString();
                    if (megeWordingInLines[3].ToString().Trim() == "") megeWording.F_19_pin = " " ; else megeWording.F_19_pin =megeWordingInLines[3].ToString();
                    if (megeWordingInLines[2].ToString().Trim() == "") megeWording.F_58_name = " "; else megeWording.F_58_name = megeWordingInLines[2].ToString();
                    if (megeWordingInLines[3].ToString().Trim() == "") megeWording.F_60_pin = " ";  else megeWording.F_60_pin = megeWordingInLines[3].ToString();

                    
                    //megeWording.F_83_name = megeWordingInLines[2].ToString().Trim();
                    //megeWording.F_63_addr1 = megeWordingInLines[4].ToString();
                    //megeWording.F_14_addr2 =  megeWordingInLines[5].ToString();
                    //megeWording.F_7_addr3 =  megeWordingInLines[6].ToString();
                    //megeWording.F_43_addr4 = megeWordingInLines[7].ToString();
                    //megeWording.F_57_PrintDate = megeWordingInLines[5].ToString();
                    //megeWording.F_91_name = megeWordingInLines[2].ToString();
                    //megeWording.F_19_pin = megeWordingInLines[3].ToString();
                    //megeWording.F_58_name = megeWordingInLines[2].ToString();
                    //megeWording.F_60_pin = megeWordingInLines[3].ToString();

                    megeWordings.Add(megeWording);
                }
                i++;
            }

            return megeWordings;
        }

        static List<mergeWording> GetMergeWording()
        {
            var megeWordings = new List<mergeWording>();
            var megeWording1 = new mergeWording();
            var megeWording2 = new mergeWording();

            megeWording1.F_83_name = "Carlos";
            megeWording1.F_63_addr1 = "Address1";
            megeWording1.F_14_addr2 = "Address2";
            megeWording1.F_7_addr3 = "Address3";
            megeWording1.F_43_addr4 = "Address4";
            megeWording1.F_57_PrintDate = "Address5";
            megeWording1.F_91_name = "Address6";
            megeWording1.F_19_pin = "Address7";
            megeWording1.F_58_name = "Address8";
            megeWording1.F_60_pin = "Address9";

            megeWordings.Add(megeWording1);

            megeWording2.F_83_name = "CarlosAY";
            megeWording2.F_63_addr1 = "vAddress2";
            megeWording2.F_14_addr2 = "cAddress2";
            megeWording2.F_7_addr3 = "cAddress3";
            megeWording2.F_43_addr4 = "cAddress4";
            megeWording2.F_57_PrintDate = "vAddress5";
            megeWording2.F_91_name = "cAddress6";
            megeWording2.F_19_pin = "cAddress7";
            megeWording2.F_58_name = "cAddress8";
            megeWording2.F_60_pin = "cAddress9";
            megeWordings.Add(megeWording2);

            return megeWordings;
        }



        static void MailMerge(mergeWording megeWording, string templatePath, string saveFilePath, string printerName)
        {
            Object oMissing = System.Reflection.Missing.Value;

            var applicaiton = new Word.Application();
            var document = new Word.Document();

            try
            {
                document = applicaiton.Documents.Add(Template: @templatePath);
                //Display word
                //applicaiton.Visible = true;

                applicaiton.Visible = false;

                foreach (Microsoft.Office.Interop.Word.Field field in document.Fields)
                {
                    if (field.Code.Text.Contains("F_83_name"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_83_name);
                    }
                    else if (field.Code.Text.Contains("F_63_addr1"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_63_addr1);
                    }
                    else if (field.Code.Text.Contains("F_14_addr2"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_14_addr2);
                    }
                    else if (field.Code.Text.Contains("F_7_addr3"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_7_addr3);
                    }
                    else if (field.Code.Text.Contains("F_43_addr4"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_43_addr4);
                    }
                    else if (field.Code.Text.Contains("F_57_PrintDate"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_57_PrintDate);
                    }
                    else if (field.Code.Text.Contains("F_91_name"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_91_name);
                    }
                    else if (field.Code.Text.Contains("F_19_pin"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_19_pin);
                    }
                    else if (field.Code.Text.Contains("F_58_name"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_58_name);
                    }
                    else if (field.Code.Text.Contains("F_60_pin"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_60_pin);
                    }
                    else if (field.Code.Text.Contains("F_83_name"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_83_name);
                    }
                    else if (field.Code.Text.Contains("F_63_addr1"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_63_addr1);
                    }
                    else if (field.Code.Text.Contains("F_14_addr2"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_14_addr2);
                    }
                    else if (field.Code.Text.Contains("F_7_addr3"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_7_addr3);
                    }
                    else if (field.Code.Text.Contains("F_43_addr4"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_43_addr4);
                    }
                    else if (field.Code.Text.Contains("F_57_PrintDate"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_57_PrintDate);
                    }
                    else if (field.Code.Text.Contains("F_91_name"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_91_name);
                    }
                    else if (field.Code.Text.Contains("F_19_pin"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_19_pin);
                    }
                    else if (field.Code.Text.Contains("F_58_name"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_58_name);
                    }
                    else if (field.Code.Text.Contains("F_60_pin"))
                    {
                        field.Select();
                        applicaiton.Selection.TypeText(megeWording.F_60_pin);
                    }
                }

                //document.SaveAs2(FileName: @saveFilePath);
                document.Application.ActivePrinter = printerName;
                document.PrintOut(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                //Print without save documnet
                document.Close(false);
                //document.Close(oMissing, oMissing, oMissing);
                applicaiton.Quit(oMissing, oMissing, oMissing);
                
                 // Release the objects.
                 System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
                 System.Runtime.InteropServices.Marshal.ReleaseComObject(applicaiton);
            }
           
        }




    }
}
