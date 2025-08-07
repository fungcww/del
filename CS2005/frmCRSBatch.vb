Imports System.Data.SqlClient
Imports System.Data.Odbc

Public Class frmCRSBatch
    Inherits System.Windows.Forms.Form

    Dim dtRelCode As New DataTable
    Dim sw As IO.StreamWriter

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'frmCRSBatch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Name = "frmCRSBatch"
        Me.Text = "frmCRSBatch"

    End Sub

#End Region

    Private Sub frmCRSBatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'oliver 2024-5-3 added comment for Table_Relocate_Sprint13,It will not call because obsolate function 
        Dim datProc As Date
        Dim lngErrNo As Long
        Dim strErrMsg As String

        datProc = Today
        datProc = DateAdd(DateInterval.Day, -28, datProc)
        BuildFamilyTree(datProc, gsUser, lngErrNo, strErrMsg)
        UpdateCustomerInfo(datProc, gsUser, lngErrNo, strErrMsg)

    End Sub

    Private Function BuildFamilyTree(ByVal datPrc As Date, ByVal strUserID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean

        Dim strSQL As String
        Dim dtResult As New DataTable
        Dim dtICR As New DataTable
        Dim dtCIW As New DataTable
        Dim blnValidID As Boolean
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Dim strInsured, strRel, strCheck As String
        Dim strPerson1, strPerson2 As String

        ' Open log file
        sw = New IO.StreamWriter("FamilyTreeLog_" & Format(Today, "yyyyMMdd") & ".log", True, System.Text.Encoding.Default)
        sw.WriteLine("Family Tree generation starts at " & Now)
        sw.Flush()

        ' Load Relationship table
        strSQL = "Select cswrld_relationship, cswrld_desc From " & serverPrefix & "csw_customer_rel_code"
        dtRelCode = ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)

        If lngErrNo = -1 Then
            sw.WriteLine("Error retrieving relationship code table:  " & strErrMsg)
            sw.Flush()
            Exit Function
        End If

        ' Get New Business / Rider
        Dim strPolicy As String

        strSQL = "Select PolicyAccountID, Trailer, ProductID, CustomerID  " & _
            " From coveragedetail " & _
            " Where issuedate >= '" & Format(datPrc, "yyyyMMdd") & "'" & _
            " And (Trailer = 1 Or ProductID like '_RCTR%' or ProductID like '_RSTR%') " & _
            " Order by policyaccountid, trailer"
        dtResult = ExecuteScript(strSQL, "CIW2", lngErrNo, strErrMsg)

        If lngErrNo = -1 Then
            sw.WriteLine("Error retrieving policy information: " & strErrMsg)
            sw.Flush()
            Exit Function
        End If

        For Each row As DataRow In dtResult.Rows
            strPerson2 = ""
            strRel = ""
            blnValidID = False

            ' For each policy 
            If strPolicy <> row.Item("PolicyAccountID") Then

                lngErrNo = 0
                strPolicy = Trim(row.Item("PolicyAccountID"))
                sw.WriteLine("<" & Now & "> Generating Family tree for policy " & strPolicy)
                sw.Flush()

                strSQL = _
                    " Select isnull(icrali_aidno,'') as icrali_aidno, icrali_adob, isnull(icrali_asex,'') as icrali_asex, isnull(icrali_apassno,'') as icrali_apassno, " & _
                    "        isnull(icrali_iidno,'') as icrali_iidno, icrali_idob, isnull(icrali_isex,'') as icrali_isex, isnull(icrali_ipassno,'') as icrali_ipassno, " & _
                    "        isnull(icrali_arelate,'') as icrali_arelate, isnull(icrali_arelspc,'') as icrali_arelspc, " & _
                    "        isnull(icrali_bidno1,'') as icrali_bidno1, isnull(icrali_brel1,'') as icrali_brel1, " & _
                    "        isnull(icrali_bidno2,'') as icrali_bidno2, isnull(icrali_brel2,'') as icrali_brel2, " & _
                    "        isnull(icrali_bidno3,'') as icrali_bidno3, isnull(icrali_brel3,'') as icrali_brel3, " & _
                    "        isnull(icrali_icustid,0) as icrali_icustid, isnull(icrali_acustid,0) as icrali_acustid, " & _
                    "        isnull(icrali_bcustid1,0) as icrali_bcustid1, isnull(icrali_bcustid2,0) as icrali_bcustid2, " & _
                    "        isnull(icrali_bcustid3,0) as icrali_bcustid3 " & _
                    " From icr_appl_life_ins " & _
                    " Where icrali_lfpono = '" & strPolicy & "'"
                dtICR = ExecuteScript(strSQL, "ICR", lngErrNo, strErrMsg)

                If lngErrNo = -1 Then
                    sw.WriteLine("Error retrieving ICR information: " & strErrMsg)
                    sw.Flush()
                    GoTo NextPolicy
                End If

                If dtICR Is Nothing OrElse dtICR.Rows.Count = 0 Then
                    sw.WriteLine("ICR information not found")
                    sw.Flush()
                    GoTo NextPolicy
                End If

                ' Get Insured and Beneficiary
                strSQL = "Select r.CustomerID, PolicyAccountID, PolicyRelateCode, DateOfBirth, isnull(Gender,'') as Gender, " & _
                    "            isnull(GovernmentIDCard,'') as GovernmentIDCard, isnull(PassportNumber,'') as PassportNumber " & _
                    " From csw_poli_rel r, customer c, PolicyAccountRelationCodes " & _
                    " Where PolicyAccountID = '" & strPolicy & "'" & _
                    " And PolicyRelateCode IN ('PH', 'PI', 'BE', 'SP') " & _
                    " And r.CustomerID = c.CustomerID " & _
                    " And PolicyAccountRelationCode = r.PolicyRelateCode " & _
                    " Order By SortingSeq"
                dtCIW = ExecuteScript(strSQL, "CIW2", lngErrNo, strErrMsg)

                If lngErrNo = -1 Then
                    sw.WriteLine("Error retrieving policy relation information: " & strErrMsg)
                    sw.Flush()
                    GoTo NextPolicy
                End If

                If dtCIW.Rows.Count > 0 AndAlso dtCIW.Rows(0).Item("PolicyRelateCode") = "PH" Then
                    strPerson1 = dtCIW.Rows(0).Item("CustomerID")
                Else
                    sw.WriteLine("PH information not found for policy " & strPolicy)
                    sw.Flush()
                    strPerson1 = ""
                    GoTo NextPolicy
                End If

            End If

            If strPerson1 <> "" Then
                ' NB - Check beneficiary and insured relation
                If row.Item("Trailer") = 1 Then

                    If dtICR.Rows.Count > 0 Then

                        'If Trim(dtCIW.Rows(0).Item("GovernmentIDCard")) = "" OrElse Trim(dtICR.Rows(0).Item("icrali_aidno")) = "" Then
                        '    If Trim(dtCIW.Rows(0).Item("PassportNumber")) = "" OrElse Trim(dtICR.Rows(0).Item("icrali_apassno")) = "" Then
                        '        sw.WriteLine("Invalid HKID/Passport No.")
                        '        sw.Flush()
                        '    Else
                        '        If Trim(dtCIW.Rows(0).Item("PassportNumber")) = Trim(dtICR.Rows(0).Item("icrali_apassno")) Then
                        '            blnValidID = True
                        '        End If
                        '    End If
                        'Else
                        '    If Trim(dtCIW.Rows(0).Item("GovernmentIDCard")) = Trim(dtICR.Rows(0).Item("icrali_aidno")) Then
                        '        blnValidID = True
                        '    End If
                        'End If

                        ' Make sure the PH is correct
                        ' dtCIW.Rows(0).Item("DateOfBirth") = dtICR.Rows(0).Item("icrali_adob") AndAlso
                        '.Rows(0).Item("PolicyRelateCode") = "PH" AndAlso blnValidID AndAlso _
                        'Trim(dtCIW.Rows(0).Item("Gender")) = Trim(dtICR.Rows(0).Item("icrali_asex")) Then

                        If strPerson1 = dtICR.Rows(0).Item("icrali_acustid") Then

                            For i As Integer = 1 To dtCIW.Rows.Count - 1

                                Select Case Trim(dtCIW.Rows(i).Item("PolicyRelateCode"))
                                    Case "PI"
                                        'blnValidID = False
                                        'If Trim(dtCIW.Rows(i).Item("GovernmentIDCard")) = "" OrElse Trim(dtICR.Rows(0).Item("icrali_aidno")) = "" Then
                                        '    If Trim(dtCIW.Rows(i).Item("PassportNumber")) = "" OrElse Trim(dtICR.Rows(0).Item("icrali_apassno")) = "" Then
                                        '        sw.WriteLine("Invalid HKID/Passport No.")
                                        '        sw.Flush()
                                        '    Else
                                        '        If Trim(dtCIW.Rows(i).Item("PassportNumber")) = Trim(dtICR.Rows(0).Item("icrali_apassno")) Then
                                        '            blnValidID = True
                                        '        End If
                                        '    End If
                                        'Else
                                        '    If Trim(dtCIW.Rows(i).Item("GovernmentIDCard")) = Trim(dtICR.Rows(0).Item("icrali_aidno")) Then
                                        '        blnValidID = True
                                        '    End If
                                        'End If

                                        ' dtCIW.Rows(i).Item("DateOfBirth") = dtICR.Rows(0).Item("icrali_idob") AndAlso
                                        'blnValidID AndAlso Trim(dtCIW.Rows(i).Item("Gender")) = Trim(dtICR.Rows(0).Item("icrali_isex"))
                                        If dtCIW.Rows(i).Item("CustomerID") = dtICR.Rows(0).Item("icrali_icustid") Then
                                            strPerson2 = dtCIW.Rows(i).Item("CustomerID")

                                            If strPerson1 <> strPerson2 Then
                                                strRel = LookupRelation("PI", dtICR.Rows(0).Item("icrali_arelate"), dtICR.Rows(0).Item("icrali_arelspc"))
                                            Else
                                                strRel = "SELF"
                                            End If

                                            If strRel = "" Then
                                                strRel = AddRelationType(dtICR.Rows(0).Item("icrali_arelate"))
                                            End If
                                        Else
                                            sw.WriteLine("CustomerID of PI unmatch between ICR and CIW")
                                            sw.Flush()
                                            GoTo NextRel
                                        End If

                                    Case "BE"
                                        'If Trim(dtCIW.Rows(i).Item("GovernmentIDCard")) = Trim(dtICR.Rows(0).Item("icrali_bidno1")) Then
                                        If dtCIW.Rows(i).Item("CustomerID") = dtICR.Rows(0).Item("icrali_bcustid1") Then
                                            strPerson2 = dtCIW.Rows(i).Item("CustomerID")

                                            If strPerson2 = "" Then
                                                strRel = "EMPTY"
                                            Else
                                                strRel = LookupRelation("BE", dtICR.Rows(0).Item("icrali_brel1"))
                                            End If

                                            If strRel = "" Then
                                                strRel = AddRelationType(dtICR.Rows(0).Item("icrali_brel1"))
                                            End If
                                            'ElseIf Trim(dtCIW.Rows(i).Item("GovernmentIDCard")) = Trim(dtICR.Rows(0).Item("icrali_bidno2")) Then
                                        ElseIf dtCIW.Rows(i).Item("CustomerID") = dtICR.Rows(0).Item("icrali_bcustid2") Then
                                            strPerson2 = dtCIW.Rows(i).Item("CustomerID")

                                            If strPerson2 = "" Then
                                                strRel = "EMPTY"
                                            Else
                                                strRel = LookupRelation("BE", dtICR.Rows(0).Item("icrali_brel2"))
                                            End If

                                            strRel = LookupRelation("BE", dtICR.Rows(0).Item("icrali_brel2"))
                                            If strRel = "" Then
                                                strRel = AddRelationType(dtICR.Rows(0).Item("icrali_brel2"))
                                            End If
                                            'ElseIf Trim(dtCIW.Rows(i).Item("GovernmentIDCard")) = Trim(dtICR.Rows(0).Item("icrali_bidno3")) Then
                                        ElseIf dtCIW.Rows(i).Item("CustomerID") = dtICR.Rows(0).Item("icrali_bcustid3") Then
                                            strPerson2 = dtCIW.Rows(i).Item("CustomerID")

                                            If strPerson2 = "" Then
                                                strRel = "EMPTY"
                                            Else
                                                strRel = LookupRelation("BE", dtICR.Rows(0).Item("icrali_brel3"))
                                            End If

                                            If strRel = "" Then
                                                strRel = AddRelationType(dtICR.Rows(0).Item("icrali_brel3"))
                                            End If
                                        Else
                                            sw.WriteLine("Beneficiary information not found in ICR " & dtCIW.Rows(i).Item("CustomerID"))
                                            sw.Flush()
                                            GoTo NextRel
                                        End If

                                    Case "SP"
                                        strPerson2 = dtCIW.Rows(i).Item("CustomerID")
                                        strRel = "SP"
                                End Select
                                If strRel <> "" Then
                                    If strRel <> "SELF" AndAlso strRel <> "EMPTY" Then
                                        If strPerson1 <> strPerson2 Then
                                            Call UpdateRelation(strPerson1, strPerson2, strRel, dtCIW.Rows(i).Item("PolicyRelateCode"))
                                        Else
                                            sw.WriteLine("<CHECK> Person1 ID is the same as Person2 ID.")
                                            sw.Flush()
                                        End If
                                    Else
                                        sw.WriteLine("SELF relationship, no need to generate.")
                                        sw.Flush()
                                    End If
                                Else
                                    sw.WriteLine("Error insert customer relationship (Rel. Code empty): " & strPerson1 & "," & strPerson2)
                                    sw.Flush()
                                End If
NextRel:
                            Next
                        Else
                            sw.WriteLine("PolicyHolder information unmatch between ICR and CIW")
                            sw.Flush()
                        End If

                    Else
                        sw.WriteLine("Record not found in ICR")
                        sw.Flush()
                    End If

                Else
                    ' Child / Spouse term rider
                    Select Case Mid(row.Item("ProductID"), 2, 4)
                        Case "RCTR"     ' Child term
                            strPerson2 = row.Item("CustomerID")
                            Call UpdateRelation(strPerson1, strPerson2, "PR", row.Item("ProductID"))

                        Case "RSTR"     ' Spouse term
                            strPerson2 = row.Item("CustomerID")
                            Call UpdateRelation(strPerson1, strPerson2, "SP", row.Item("ProductID"))

                    End Select
                End If
            End If
NextPolicy:
        Next

        sw.WriteLine("Family Tree generation completed at " & Now)
        sw.Flush()
        sw.Close()

    End Function

    Private Function AddRelationType(ByVal strRel As String) As String

        Dim strSQL, strID As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand

        Dim lngErrNo As Long
        Dim strErrMsg As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        ' Get Last ID 
        strSQL = "Select cswsi_last_relcode From " & serverPrefix & "csw_system_info"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()
        sqlcmd.Connection = sqlconnect

        sqlcmd.CommandText = strSQL
        Try
            strID = sqlcmd.ExecuteScalar
        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.ToString
            lngErrNo = -1
        Catch ex As Exception
            strErrMsg = ex.ToString
            lngErrNo = -1
        End Try

        If lngErrNo = -1 Then
            sw.WriteLine("Error retrieving last ID from csw_system_info.")
            sw.WriteLine(strErrMsg)
            sw.Flush()
            Exit Function
        End If

        ' Update the code
        strID = CStr(CInt(strID) + 1).PadLeft(4, "0")
        strSQL = "Update " & serverPrefix & "csw_system_info Set cswsi_last_relcode = '" & strID & "'"
        sqlcmd.CommandText = strSQL
        Try
            sqlcmd.ExecuteNonQuery()
        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.ToString
            lngErrNo = -1
        Catch ex As Exception
            strErrMsg = ex.ToString
            lngErrNo = -1
        End Try

        If lngErrNo = -1 Then
            sw.WriteLine("Error update latest ID to csw_system_info.")
            sw.WriteLine(strErrMsg)
            sw.Flush()
            Exit Function
        End If

        ' Create the relationship
        strSQL = "Insert Into " & serverPrefix & "csw_customer_rel_code " &
            " (cswrld_relationship, cswrld_desc, cswrld_create_user, cswrld_create_date, cswrld_update_user, cswrld_update_date) " &
            " Select '" & strID & "','" & Trim(strRel) & "','" & gsUser & "', GETDATE(), '" & gsUser & "', GETDATE()"

        sqlcmd.CommandText = strSQL
        Try
            sqlcmd.ExecuteNonQuery()
        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.ToString
            lngErrNo = -1
        Catch ex As Exception
            strErrMsg = ex.ToString
            lngErrNo = -1
        End Try

        AddRelationType = strID
        sqlconnect.Close()

        If lngErrNo = -1 Then
            sw.WriteLine("Error insert new relationship type: " & strRel)
            sw.WriteLine(strErrMsg)
            sw.Flush()
        Else
            sw.WriteLine("<NEW> New relationship type created: " & strRel & " (" & strID & ")")
            sw.Flush()
        End If

    End Function

    Private Function LookupRelation(ByVal strType As String, ByVal strRelCode As String, Optional ByVal strSpcRel As String = "") As String

        strRelCode = Trim(strRelCode)
        strSpcRel = Trim(strSpcRel)

        If strType = "PI" Then
            Select Case strRelCode
                Case "P"
                    LookupRelation = "PT"
                Case "D"
                    LookupRelation = "DS"
                Case "E"
                    LookupRelation = "ER"
                Case "S"
                    LookupRelation = "SP"
                Case Else
                    If UCase(strSpcRel) = "SELF" Then
                        LookupRelation = "SELF"
                    Else
                        LookupRelation = ""
                    End If
            End Select
        End If

        If strType = "BE" OrElse (strType = "PI" AndAlso strRelCode = "O") Then
            If strType = "BE" Then
                dtRelCode.DefaultView.RowFilter = "cswrld_desc = '" & strRelCode & "'"
            Else
                dtRelCode.DefaultView.RowFilter = "cswrld_desc = '" & strSpcRel & "'"
            End If

            If dtRelCode.DefaultView.Count > 0 Then
                LookupRelation = Trim(dtRelCode.DefaultView.Item(0).Item("cswrld_relationship"))
            Else
                LookupRelation = ""
                sw.WriteLine("Relationship not found in csw_customer_rel_code table: " & strRelCode & strSpcRel)
                sw.Flush()
            End If
        End If

    End Function

    Private Function UpdateRelation(ByVal strPerson1_ID As String, ByVal strPerson2_ID As String, ByVal strRelCode As String, ByVal strSourceType As String) As Boolean

        Dim strSQL, strCheck As String
        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        Dim lngErrNo As Long
        Dim strErrMsg As String

        ' Check if relationship already exist
        strSQL = "Select 'A' " &
            " From " & serverPrefix & "csw_customer_rel " &
            " Where cswcrl_person1_id = " & Trim(strPerson1_ID) &
            " And   cswcrl_person2_id = " & Trim(strPerson2_ID) &
            " And   cswcrl_relationship = '" & Trim(strRelCode) & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()
        sqlcmd.Connection = sqlconnect

        sqlcmd.CommandText = strSQL
        Try
            strCheck = sqlcmd.ExecuteScalar
        Catch sqlex As SqlClient.SqlException
            strErrMsg = sqlex.ToString
            lngErrNo = -1
        Catch ex As Exception
            strErrMsg = ex.ToString
            lngErrNo = -1
        End Try

        If lngErrNo = -1 Then
            sw.WriteLine("[" & strSourceType & "]" & " Error checking relationship: " & strPerson1_ID & "," & strRelCode & "," & strPerson2_ID)
            sw.WriteLine(strErrMsg)
            sw.Flush()
            Exit Function
        End If

        If strCheck = "" Then
            strSQL = "Insert Into " & serverPrefix & "csw_customer_rel " &
                "(cswcrl_person1_id, cswcrl_relationship, cswcrl_person2_id, cswcrl_create_user, cswcrl_create_date, cswcrl_update_user, cswcrl_update_date) " &
                "Select '" & Trim(strPerson1_ID) & "','" & Trim(strRelCode) & "','" & Trim(strPerson2_ID) & "','" &
                gsUser & "', GETDATE(), '" & gsUser & "', GETDATE()"

            sqlcmd.CommandText = strSQL

            Try
                strCheck = sqlcmd.ExecuteNonQuery
            Catch sqlex As SqlClient.SqlException
                strErrMsg = sqlex.ToString
                lngErrNo = -1
            Catch ex As Exception
                strErrMsg = ex.ToString
                lngErrNo = -1
            End Try
        Else
            sw.WriteLine("[" & strSourceType & "]" & " Customer Relation already exist: " & strPerson1_ID & "," & strRelCode & "," & strPerson2_ID)
            sw.Flush()
            sqlconnect.Close()
            Exit Function
        End If

        sqlconnect.Close()

        If lngErrNo = -1 Then
            sw.WriteLine("[" & strSourceType & "]" & " Error insert customer relationship: " & strPerson1_ID & "," & strRelCode & "," & strPerson2_ID)
            sw.WriteLine(strErrMsg)
            sw.Flush()
        Else
            sw.WriteLine("[" & strSourceType & "]" & " Customer relationship: " & strPerson1_ID & "," & strRelCode & "," & strPerson2_ID & " created successfully")
            sw.Flush()

            ' Reload relationship table
            strSQL = "Select cswrld_relationship, cswrld_desc From " & serverPrefix & "csw_customer_rel_code"
            dtRelCode = ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)
        End If

    End Function

    Private Function UpdateCustomerInfo(ByVal datPrc As Date, ByVal strUserID As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As Boolean

        Dim intCnt As Integer
        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim dtResult As New DataTable
        Dim dtICR As New DataTable
        Dim sqlcmd As New SqlCommand

        Dim strPolicy, strCust As String

        sw = New IO.StreamWriter("FamilyTreeLog_" & Format(Today, "yyyyMMdd") & ".log", True, System.Text.Encoding.Default)
        sw.WriteLine("Update Customer Information starts at " & Now)
        sw.Flush()

        '        strSQL = "Select DISTINCT p.PolicyAccountID, r.CustomerID " & _
        '            " From PolicyAccount p, Csw_Poli_Rel r " & _
        '            " Where CommencementDate >= '" & datPrc & "'" & _
        '            " And p.PolicyAccountID = r.PolicyAccountID " & _
        '            " And r.PolicyRelateCode = 'PH' "
        '        dtResult = ExecuteScript(strSQL, "CIW2", lngErrNo, strErrMsg)

        '        If lngErrNo = -1 Then
        '            sw.WriteLine("Error retrieving policy information: " & strErrMsg)
        '            sw.Flush()
        '            Exit Function
        '        End If

        '        For Each row As DataRow In dtResult.Rows

        '            lngErrNo = 0
        '            strPolicy = row.Item("PolicyAccountID")
        '            strCust = row.Item("CustomerID")

        '            sw.WriteLine("<" & Now & "> Update customer information for policy " & strPolicy)
        '            sw.Flush()

        '            ' Update from Agent Report
        '            strSQL = _
        '                "Select * From icr_agt_rpt Where icrarp_appl_no >= '" & strPolicy & "'"
        '            dtICR = ExecuteScript(strSQL, "ICR", lngErrNo, strErrMsg)

        '            If lngErrNo = -1 Then
        '                sw.WriteLine("Error retrieving policy information: " & strErrMsg)
        '                sw.Flush()
        '                GoTo NextPolicy
        '            End If

        '            ' Check each record
        '            If dtICR.Rows.Count > 0 Then
        '                For i As Integer = 0 To dtICR.Rows.Count - 1
        '                    With dtICR.Rows(i)

        '                        strSQL = "Update Csw_Demographic " & _
        '                            " Set cswdgm_mar_stat = '" & .Item("icrarp_q0503") & "', " & _
        '                            "     cswdgm_no_of_dep = '" & .Item("icrcsv_q2601") & "', " & _
        '                            "     cswdgm_edu_level = '" & .Item("icrcsv_q2901") & "', " & _
        '                            "     cswdgm_ann_sal = '" & .Item("icrcsv_q2701") & "', " & _
        '                            "     cswdgm_household_income = '" & .Item("icrcsv_q2801") & "', " & _
        '                            "     cswdgm_occupation = '" & .Item("icrcsv_q3001") & "' " & _
        '                            " Where cswdgm_cust_id = " & .Item("icrcsv_holder_id")

        '                        sqlcmd.CommandText = strSQL
        '                        Try
        '                            intCnt = sqlcmd.ExecuteNonQuery()
        '                        Catch sqlex As SqlClient.SqlException
        '                            strErrMsg = sqlex.ToString
        '                            lngErrNo = -1
        '                        Catch ex As Exception
        '                            strErrMsg = ex.ToString
        '                            lngErrNo = -1
        '                        End Try

        '                        If intCnt = 0 Then
        '                            strSQL = "Insert Csw_Demographic (cswdgm_mar_stat, cswdgm_no_of_child, " & _
        '                                " cswdgm_edu_level, cswdgm_ann_sal, cswdgm_household_income, cswdgm_occupation) " & _
        '                                " Values ('" & .Item("icrcsv_q2501") & "','" & _
        '                                .Item("icrcsv_q2601") & "','" & _
        '                                .Item("icrcsv_q2901") & "','" & _
        '                                .Item("icrcsv_q2701") & "','" & _
        '                                .Item("icrcsv_q2801") & "','" & _
        '                                .Item("icrcsv_q3001") & "'"

        '                            sqlcmd.CommandText = strSQL
        '                            Try
        '                                intCnt = sqlcmd.ExecuteNonQuery()
        '                            Catch sqlex As SqlClient.SqlException
        '                                strErrMsg = sqlex.ToString
        '                                lngErrNo = -1
        '                            Catch ex As Exception
        '                                strErrMsg = ex.ToString
        '                                lngErrNo = -1
        '                            End Try

        '                            If lngErrNo = -1 Then
        '                                sw.WriteLine("Error creating customer inforamtion - " & strCust)
        '                                sw.WriteLine(strErrMsg)
        '                                sw.Flush()
        '                            Else
        '                                sw.WriteLine("Customer inforamtion created successfully - " & strCust)
        '                                sw.Flush()
        '                            End If

        '                        Else

        '                            If lngErrNo = -1 Then
        '                                sw.WriteLine("Error updating customer inforamtion - " & strCust)
        '                                sw.WriteLine(strErrMsg)
        '                                sw.Flush()
        '                            Else
        '                                sw.WriteLine("Customer inforamtion updated successfully - " & strCust)
        '                                sw.Flush()
        '                            End If
        '                        End If
        '                    End With
        '                Next
        '            Else
        '                sw.WriteLine("Agent Report not found")
        '                sw.Flush()
        '            End If
        'NextPolicy:
        '        Next

        ' Update from Welcome Kit Survey
        strSQL = _
            " Select icrcsv_holder_id, isnull(icrcsv_q2501,0) as icrcsv_q2501, isnull(icrcsv_q2601,0) as icrcsv_q2601, " & _
            "        isnull(icrcsv_q2901,0) as icrcsv_q2901, " & _
            "        isnull(icrcsv_q2701,0) as icrcsv_q2701, isnull(icrcsv_q2801,0) as icrcsv_q2801, " & _
            "        isnull(icrcsv_q3001,0) as icrcsv_q3001, icrcsv_is_upd_form " & _
            " from icr_cust_survey where icrcsv_ExportDate >= '" & Format(datPrc, "yyyyMMdd") & "'"
        dtResult = ExecuteScript(strSQL, "ICR", lngErrNo, strErrMsg)

        If lngErrNo = -1 Then
            sw.WriteLine("Error retrieving Welcome Kit survey information: " & strErrMsg)
            sw.Flush()
            Exit Function
        End If

        ' Check each record
        If dtResult.Rows.Count > 0 Then

            sqlconnect.ConnectionString = strCIWConn
            sqlconnect.Open()
            sqlcmd.Connection = sqlconnect

            For i As Integer = 0 To dtResult.Rows.Count - 1
                lngErrNo = 0
                strSQL = ""
                strCust = dtResult.Rows(i).Item("icrcsv_holder_id")

                With dtResult.Rows(i)

                    If Not IsDBNull(.Item("icrcsv_q2501")) AndAlso .Item("icrcsv_q2501") <> "" Then
                        If strSQL = "" Then
                            strSQL = "cswdgm_mar_stat = '" & .Item("icrcsv_q2501") & "'"
                        End If
                    End If

                    'If Not IsDBNull(.Item("icrcsv_q1101")) And .Item("icrcsv_q1101") <> "" Then
                    '    If strSQL = "" Then
                    '        strSQL = "cswdgm_parents = '" & .Item("icrcsv_q1101") & "'"
                    '    Else
                    '        strSQL &= ", cswdgm_parents = '" & .Item("icrcsv_q1101") & "'"
                    '    End If
                    'End If

                    If Not IsDBNull(.Item("icrcsv_q2601")) AndAlso .Item("icrcsv_q2601") <> "" Then
                        If strSQL = "" Then
                            strSQL = "cswdgm_no_of_dep = '" & .Item("icrcsv_q2601") & "'"
                        Else
                            strSQL &= ", cswdgm_no_of_dep = '" & .Item("icrcsv_q2601") & "'"
                        End If
                    End If

                    If Not IsDBNull(.Item("icrcsv_q2901")) AndAlso .Item("icrcsv_q2901") <> "" Then
                        If strSQL = "" Then
                            strSQL = "cswdgm_edu_level = '" & .Item("icrcsv_q2901") & "'"
                        Else
                            strSQL &= ", cswdgm_edu_level = '" & .Item("icrcsv_q2901") & "'"
                        End If
                    End If

                    If Not IsDBNull(.Item("icrcsv_q2701")) AndAlso .Item("icrcsv_q2701") <> "" Then
                        If strSQL = "" Then
                            strSQL = "cswdgm_ann_sal = '" & .Item("icrcsv_q2701") & "'"
                        Else
                            strSQL &= ", cswdgm_ann_sal = '" & .Item("icrcsv_q2701") & "'"
                        End If
                    End If

                    If Not IsDBNull(.Item("icrcsv_q2801")) AndAlso .Item("icrcsv_q2801") <> "" Then
                        If strSQL = "" Then
                            strSQL = "cswdgm_household_income = '" & .Item("icrcsv_q2801") & "'"
                        Else
                            strSQL &= ", cswdgm_household_income = '" & .Item("icrcsv_q2801") & "'"
                        End If
                    End If

                    If Not IsDBNull(.Item("icrcsv_q3001")) AndAlso .Item("icrcsv_q3001") <> "" Then
                        If strSQL = "" Then
                            strSQL = "cswdgm_occupation = '" & .Item("icrcsv_q3001") & "'"
                        Else
                            strSQL &= ", cswdgm_occupation = '" & .Item("icrcsv_q3001") & "'"
                        End If
                    End If

                    If strSQL <> "" Then
                        strSQL = "Update Csw_Demographic Set " & strSQL & _
                            " Where cswdgm_cust_id = " & .Item("icrcsv_holder_id")

                        sqlcmd.CommandText = strSQL
                        Try
                            intCnt = sqlcmd.ExecuteNonQuery()
                        Catch sqlex As SqlClient.SqlException
                            strErrMsg = sqlex.ToString
                            lngErrNo = -1
                        Catch ex As Exception
                            strErrMsg = ex.ToString
                            lngErrNo = -1
                        End Try
                    Else
                        ' All are NULL
                        sw.WriteLine("Nothing to update - " & strCust)
                        sw.Flush()
                        GoTo nextcust
                    End If

                    If lngErrNo = -1 Then
                        sw.WriteLine("Error updating customer inforamtion - " & strCust)
                        sw.WriteLine(strErrMsg)
                        sw.Flush()
                        GoTo NextCust
                    End If

                    If intCnt = 0 Then
                        strSQL = "Insert Csw_Demographic (cswdgm_cust_id, cswdgm_mar_stat, cswdgm_no_of_dep, " & _
                            " cswdgm_edu_level, cswdgm_ann_sal, cswdgm_household_income, cswdgm_occupation) " & _
                            " Values ('" & strCust & "','" & _
                            .Item("icrcsv_q2501") & "','" & _
                            .Item("icrcsv_q2601") & "','" & _
                            .Item("icrcsv_q2901") & "','" & _
                            .Item("icrcsv_q2701") & "','" & _
                            .Item("icrcsv_q2801") & "','" & _
                            .Item("icrcsv_q3001") & "')"

                        sqlcmd.CommandText = strSQL
                        Try
                            intCnt = sqlcmd.ExecuteNonQuery()
                        Catch sqlex As SqlClient.SqlException
                            strErrMsg = sqlex.ToString
                            lngErrNo = -1
                        Catch ex As Exception
                            strErrMsg = ex.ToString
                            lngErrNo = -1
                        End Try

                        If lngErrNo = -1 Then
                            sw.WriteLine("Error creating customer inforamtion - " & strCust)
                            sw.WriteLine(strErrMsg)
                            sw.Flush()
                            GoTo NextCust
                        Else
                            sw.WriteLine("Customer inforamtion created successfully - " & strCust)
                            sw.Flush()
                        End If
                    Else
                        sw.WriteLine("Customer inforamtion updated successfully - " & strCust)
                        sw.Flush()
                    End If
                End With
NextCust:
            Next
        End If
        sqlconnect.Close()

        sw.WriteLine("Customer information update completed at " & Now)
        sw.Flush()
        sw.Close()

    End Function

    Public Function ExecuteScript(ByVal strScript As String, ByVal strSource As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As System.Data.DataTable

        Dim sqlda As SqlDataAdapter
        Dim sqlConnect As SqlConnection

        Dim odbcda As OdbcDataAdapter
        Dim odbcConnect As OdbcConnection

        Dim dtTempResult As DataTable
        Dim strCIWConn1, strICRConn1 As String

        strCIWConn1 = strCIWConn
        strICRConn1 = strICRConn

        'AC - Change to use configuration setting - start
        'If UAT <> 0 Then
        '    If Trim(strSource) = "CIW2" Then
        '        strCIWConn1 = "server=EAASQLPRD1;database=vantive;Network=DBMSSOCN;uid=ittms1;password=Eric0007"
        '    End If
        '    strICRConn1 = "server=HKALSQLPRD3;database=ICR;Network=DBMSSOCN;uid=ittms1;password=Eric0007"
        'End If
        'AC - Change to use configuration setting - end

        Select Case RTrim(strSource)

            Case "CAPSIL", "CAPSIL1", "CAPSIL2", "CAPSIL3"
                'odbcConnect = New OdbcConnection
                'odbcConnect.ConnectionString = strCAPSILConn & ";Connect Timeout=240"
                'odbcda = New OdbcDataAdapter(strScript, odbcConnect)
                'dtTempResult = New DataTable

                'Try
                '    odbcda.Fill(dtTempResult)
                'Catch sqlex As OdbcException
                '    lngErrNo = -1
                '    strErrMsg = sqlex.ToString
                'Catch ex As Exception
                '    lngErrNo = -1
                '    strErrMsg = ex.ToString
                'End Try

            Case "CAM", "CAM1"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strCIWConn
                sqlda = New SqlDataAdapter(strScript, sqlConnect)
                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "CIW", "CUST", "CIW2", "CIW3", "CIW4", "CIW5"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strCIWConn1 & ";Connect Timeout=240"
                sqlda = New SqlDataAdapter(strScript, sqlConnect)
                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "ICR"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strICRConn1 & ";Connect Timeout=240"
                sqlda = New SqlDataAdapter(strScript, sqlConnect)
                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

        End Select

        sqlda = Nothing
        odbcda = Nothing
        sqlConnect = Nothing
        odbcConnect = Nothing

        Return dtTempResult

    End Function

End Class
