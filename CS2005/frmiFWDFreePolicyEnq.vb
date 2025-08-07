Public Class frmiFWDFreePolicyEnq

    Private sqldt As DataTable
    Private sqldt_policyInsured As DataTable
    Private sqldt_policyBene As DataTable
    Private ds As DataSet = New DataSet("PolicyList")
    Private bm As BindingManagerBase
    Private bm1 As BindingManagerBase
    Private bm2 As BindingManagerBase
    Private lngErr As Long = 0
    Private strErr As String = ""

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim s_PolicyNo As String
        Dim s_HKID As String
        Dim lngCnt As Long

        wndMain.StatusBarPanel1.Text = ""

        s_HKID = txt_HKID.Text
        s_PolicyNo = txt_PolicyNo.Text

        s_HKID = s_HKID.Replace("'", "")
        s_PolicyNo = s_PolicyNo.Replace("'", "")

        sqldt = Nothing
        sqldt_policyInsured = Nothing
        sqldt_policyBene = Nothing
        grdPolicy.DataSource = Nothing
        grdInsured.DataSource = Nothing
        grdBeneficiary.DataSource = Nothing


        lngErr = 0
        strErr = ""
        lngCnt = gSearchLimit
        If s_HKID.Length > 0 Or s_PolicyNo.Length > 0 Then
            sqldt = GetPolicyListECommerce(s_HKID, s_PolicyNo, "POLST", lngErr, strErr, lngCnt)
        End If

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If


        If Not sqldt Is Nothing Then
            If ds.Tables.Contains("POLST") Then
                ds.Tables.Remove("POLST")
            End If
            ds.Tables.Add(sqldt)
            ds.Tables("POLST").DefaultView.Sort = "policy_number, created_date"
            bm = Me.BindingContext(ds.Tables("POLST"))

            'by default select the first one
            If bm.Count > 0 Then

                Call build_grdPolicy_UI()

                Dim i As Integer
                i = grdPolicy.CurrentRowIndex
                s_PolicyNo = grdPolicy.Item(i, 0)
                RefreshDataSource_Insured(s_PolicyNo)
                If bm1.Count > 0 Then
                    'Dim s_InsuredID As String = ""
                    's_InsuredID = grdInsured.Item(0, 0)
                    RefreshDataSource_Bene(s_PolicyNo, "")
                End If
                wndMain.StatusBarPanel1.Text = bm.Count & " records selected"
            End If



        End If
        wndMain.Cursor = Cursors.Default

    End Sub
    Private Sub build_grdPolicy_UI()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn
        Dim cb As DataGridBoolColumn

        grdPolicy.TableStyles.Clear()

        Dim strSEL As String = "SELECT p.id, p.policy_number, p.created_date, " & _
                "p.commencement_date, p.expiry_date, p.user_name, p.promo_code, " & _
                "c.full_name, c.id_number, c.mobile, c.email, " & _
                "isnull(c.opt_in1, 0) as Opt_In_FWD, isnull(c.opt_in2, 0) as Opt_In_3Pty  "

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "policy_number"
        cs.HeaderText = "Policy No."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "created_date"
        cs.HeaderText = "Application Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "commencement_date"
        cs.HeaderText = "Departure Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "expiry_date"
        cs.HeaderText = "Return Date"
        cs.NullText = gNULLText
        cs.Format = gDateFormat
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "full_name"
        cs.HeaderText = "Applicant Name."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "id_number"
        cs.HeaderText = "Applicant HKID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "mobile"
        cs.HeaderText = "Mobile Number"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "email"
        cs.HeaderText = "Email Address"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "user_name"
        cs.HeaderText = "Username"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "promo_code"
        cs.HeaderText = "Prom.Code Apply"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cb = New DataGridBoolColumn
        cb.Width = 100
        cb.MappingName = "Opt_In_FWD"
        cb.HeaderText = "Opt-out FWD"
        cb.NullValue = False
        ts.GridColumnStyles.Add(cb)

        cb = New DataGridBoolColumn
        cb.Width = 100
        cb.MappingName = "Opt_In_3Pty"
        cb.HeaderText = "Opt-out 3rd Party"
        cb.NullValue = False
        ts.GridColumnStyles.Add(cb)

        ts.MappingName = "POLST"
        grdPolicy.TableStyles.Add(ts)

        grdPolicy.DataSource = sqldt
        grdPolicy.AllowDrop = False
        grdPolicy.ReadOnly = True

    End Sub

    Private Sub build_grdInsured_UI()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        grdInsured.TableStyles.Clear()

        Dim strSEL As String = "SELECT gpi.policy_id, c.full_name, c.id_number, gpi.relationship_code, itd.item_desc "


        cs = New DataGridTextBoxColumn
        cs.Width = 0
        cs.MappingName = "id"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "full_name"
        cs.HeaderText = "Insured Name."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "id_number"
        cs.HeaderText = "Insured HKID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "item_desc"
        cs.HeaderText = "Insured Relationship"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "INSPOLST"
        grdInsured.TableStyles.Add(ts)

        grdInsured.DataSource = sqldt_policyInsured
        grdInsured.AllowDrop = False
        grdInsured.ReadOnly = True

    End Sub

    Private Sub build_grdBeneficiary_UI()

        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        grdBeneficiary.TableStyles.Clear()

        Dim strSEL As String = "SELECT gpi.policy_id, c.full_name, c.id_number, gpi.relationship_code, itd.item_desc "

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "full_name"
        cs.HeaderText = "Beneficiary Name."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "id_number"
        cs.HeaderText = "Beneficiary HKID"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "item_desc"
        cs.HeaderText = "Beneficiary Relationship"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "BENEPOLST"
        grdBeneficiary.TableStyles.Add(ts)

        grdBeneficiary.DataSource = sqldt_policyBene
        grdBeneficiary.AllowDrop = False
        grdBeneficiary.ReadOnly = True

    End Sub

    Private Sub grdPolicy_CurCellChange(ByVal sender As System.Object, ByVal e As EventArgs) Handles grdPolicy.CurrentCellChanged
        Dim i As Integer
        i = grdPolicy.CurrentRowIndex
        Dim s_PolicyNo As String = ""

        sqldt_policyInsured = Nothing
        sqldt_policyBene = Nothing
        grdInsured.DataSource = Nothing
        grdBeneficiary.DataSource = Nothing

        If bm.Count > 0 Then

            s_PolicyNo = grdPolicy.Item(i, 0)
            RefreshDataSource_Insured(s_PolicyNo)

            RefreshDataSource_Bene(s_PolicyNo, "")

            wndMain.Cursor = Cursors.Default

        End If

    End Sub


    Private Sub grdInsured_CurCellChange(ByVal sender As System.Object, ByVal e As EventArgs) Handles grdInsured.CurrentCellChanged
        Dim i, j As Integer
        j = grdPolicy.CurrentRowIndex
        i = grdInsured.CurrentRowIndex
        Dim s_PolicyNo As String = ""
        Dim s_InsuredID As String = ""

        sqldt_policyBene = Nothing
        grdBeneficiary.DataSource = Nothing

        If bm.Count > 0 And bm1.Count > 0 Then

            s_PolicyNo = grdPolicy.Item(j, 0)
            s_InsuredID = grdInsured.Item(i, 0)
            RefreshDataSource_Bene(s_PolicyNo, s_InsuredID)

            wndMain.Cursor = Cursors.Default

        End If

    End Sub

    Private Sub RefreshDataSource_Insured(ByVal s_PolicyNo As String)

        Dim lngCnt As Long

        '********************************************
        'Build Insured Grid
        wndMain.StatusBarPanel1.Text = ""
        lngErr = 0
        strErr = ""
        lngCnt = gSearchLimit
        sqldt_policyInsured = GetPolicyInsuredInfoECommerce(s_PolicyNo, "INSPOLST", lngErr, strErr, lngCnt)

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If


        If lngCnt = 0 Then

        Else
            If Not sqldt_policyInsured Is Nothing Then
                If ds.Tables.Contains("INSPOLST") Then
                    ds.Tables.Remove("INSPOLST")
                End If
                ds.Tables.Add(sqldt_policyInsured)
            End If

        End If

        ' Default sorting sequence
        ds.Tables("INSPOLST").DefaultView.Sort = "full_name"
        bm1 = Me.BindingContext(ds.Tables("INSPOLST"))

        Call build_grdInsured_UI()

    End Sub

    Private Sub RefreshDataSource_Bene(ByVal s_PolicyNo As String, ByVal s_InsuredID As String)

        Dim lngCnt As Long

        '********************************************
        'Build Beneficiary Grid
        wndMain.StatusBarPanel1.Text = ""

        lngErr = 0
        strErr = ""
        lngCnt = gSearchLimit
        sqldt_policyBene = GetPolicyBeneInfoECommerce(s_PolicyNo, s_InsuredID, "BENEPOLST", lngErr, strErr, lngCnt)

        If lngErr <> 0 Then
            MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If


        If lngCnt = 0 Then

        Else
            If Not sqldt_policyInsured Is Nothing Then
                If ds.Tables.Contains("BENEPOLST") Then
                    ds.Tables.Remove("BENEPOLST")
                End If
                ds.Tables.Add(sqldt_policyBene)
            End If

        End If

        ' Default sorting sequence
        ds.Tables("BENEPOLST").DefaultView.Sort = "full_name"
        bm2 = Me.BindingContext(ds.Tables("BENEPOLST"))

        Call build_grdBeneficiary_UI()

    End Sub



    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        txt_HKID.Text = ""
        txt_PolicyNo.Text = ""
        sqldt = Nothing
        sqldt_policyInsured = Nothing
        sqldt_policyBene = Nothing
        grdPolicy.DataSource = Nothing
        grdInsured.DataSource = Nothing
        grdBeneficiary.DataSource = Nothing


    End Sub
End Class