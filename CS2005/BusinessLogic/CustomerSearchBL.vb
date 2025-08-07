''' <summary>
''' Customer search related business logic
''' </summary>
''' <remarks>
''' <br>20250112 Chrysan Cheng, HNW Expansion - Integrated Customer Search</br>
''' </remarks>
Public Class CustomerSearchBL

    ''' <summary>
    ''' Verify and show customer detail info screen (Customer-Centric View)
    ''' </summary>
    ''' <param name="strName">Customer full name</param>
    ''' <param name="strCustID">Customer ID</param>
    ''' <param name="strClientID">Agent code</param>
    ''' <param name="strCompanyID">Company name, e.g. Bermuda, Bermuda/Private, Assurance, Macau</param>
    ''' <param name="strIDNumber">IDCard/Passport, if provided, all customer records using this IDNumber will be queried</param>
    ''' <param name="sourceLogName">The access source name used to write some log</param>
    ''' <param name="isBothCompany">Indicate whether this customer(IDCard/Passport) exists in multi company</param>
    Public Sub ShowCustomerCentric(strName As String, strCustID As String, strClientID As String, strCompanyID As String,
                                   strIDNumber As String, sourceLogName As String, isBothCompany As Boolean)

        ' trim as standard CompanyID first
        strCompanyID = strCompanyID.Replace("/Private", String.Empty)
        strCompanyID = If(String.IsNullOrEmpty(strCompanyID), "Bermuda", strCompanyID)

        Dim strErr As String = String.Empty
        Dim companyCode As String = GetCompanyCodeByName(strCompanyID)

        ' Do access permission checking
        If Not CheckUPSAccessFunc($"Search Customer ({strCompanyID})") Then
            MsgBox($"{gsUser} has no {strCompanyID} permission , You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
            Exit Sub
        End If


        ' Do checking for UHNW(VVIP), ITSR-4063
        Dim isUHNWMemberFlag As Boolean = If(companyCode = g_McuComp, isUHNWMemberMcu, isUHNWMember)
        Dim isUHNWCustomer As Boolean = False
        Dim retDs As DataSet = APIServiceBL.CallAPIBusi(companyCode, "VERIFY_UHNWCUSTOMER", New Dictionary(Of String, String)() From {{"CustomerID", strCustID}})

        If retDs IsNot Nothing AndAlso retDs.Tables.Count > 0 AndAlso retDs.Tables(0).Rows.Count > 0 AndAlso
            Convert.ToInt32(retDs.Tables(0).Rows(0).Item("Count")) > 0 Then
            isUHNWCustomer = True
            InsertVVIPLog("", strCustID, "", sourceLogName, isUHNWMemberFlag, companyCode)
        End If

        If isUHNWMemberFlag AndAlso isUHNWCustomer Then
            MsgBox("VVVIP -Ultra High Net Worth customer, need handle with Care on Advisor and Customer enquiries.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
        ElseIf (Not isUHNWMemberFlag) AndAlso isUHNWCustomer Then
            MsgBox("VVVIP -Ultra High Net Worth customer. You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
            Exit Sub
        End If


        ' Do checking for HNW(Private), oliver 2024-8-2 added for Com 6
        Dim isHnwPolicy As Boolean = False
        If companyCode = g_Comp Then    ' only for Bermuda/Private
            Dim strCustCriteria As String = String.Empty
            If (Not String.IsNullOrEmpty(strIDNumber)) AndAlso (Not strIDNumber.ToUpper.StartsWith("XXX")) Then
                ' search all policy under the same IDCard/Passport
                strCustCriteria = $" select customerid from customer where (GovernmentIDCard = '{strIDNumber}' or PassportNumber = '{strIDNumber}') "
            End If

            Dim dtPolicyList As DataTable = GetAsurPolicyListCIW(strCustID, "", "", "POLST", 0, strErr, 0, False, "ING", "", strCustCriteria)
            If Not String.IsNullOrEmpty(strErr) Then
                MsgBox(strErr, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If
            If dtPolicyList IsNot Nothing AndAlso dtPolicyList.Rows.Count > 0 AndAlso dtPolicyList.Columns.Contains("CompanyID") Then
                For i As Integer = 0 To dtPolicyList.Rows.Count - 1
                    If dtPolicyList.Rows(i).Item("CompanyID") = "BMU" Then
                        isHnwPolicy = True
                        Exit For
                    End If
                Next
            End If
            If isHnwPolicy AndAlso Not CheckUserRight("CRS_HNW_USER") Then
                MsgBox($"{gsUser} has no Private permission , You’re not authorized to access.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Warning")
                Exit Sub
            End If
        End If


        ' show customer detail screen
        If companyCode <> g_McuComp Then
            OpenCustomerAsur(strName, strCustID, strClientID, strCompanyID, strIDNumber, isBothCompany, isUHNWCustomer, isHnwPolicy)
        Else
            OpenCustomerMcu(strName, strCustID, strClientID, strCompanyID, isUHNWCustomer)
        End If
    End Sub

    ''' <summary>
    ''' Open customer detail info screen for Bermuda/Private/Assurance
    ''' </summary>
    Public Shared Sub OpenCustomerAsur(strName As String, strCustID As String, strClientID As String, strCompanyID As String,
                                       strIDNumber As String, isBothCompany As Boolean, isUHNWCustomer As Boolean, isHnwPolicy As Boolean)

        Dim w As New frmCustomer_Asur With {
            .IsHnwPolicy = isHnwPolicy,     ' oliver 2024-8-6 added for Com 6
            .IsGI = False,      ' ES01
            .Text = $"Customer {strName} ({strCustID}-{strCompanyID})",
            .UCID = strCustID,
            .isUHNWCustomer = isUHNWCustomer,
            .IDNumber = strIDNumber,
            .IsAgent = Not String.IsNullOrEmpty(strClientID.Trim),  'added at 2023-9-13 by oliver for Customer Level Search Issue
            .CompanyID = strCompanyID,
            .IsBothComp = isBothCompany
        }
        w.CustID(strClientID) = strCustID

        ShowWindow(w, wndMain, w.Text)
    End Sub

    ''' <summary>
    ''' Open customer detail info screen for Macau
    ''' </summary>
    Public Shared Sub OpenCustomerMcu(strName As String, strCustID As String, strClientID As String, strCompanyID As String, isUHNWCustomer As Boolean)

        Dim w As New frmCustomerMcu With {
            .IsGI = False,      ' ES01
            .Text = $"Customer {strName} ({strCustID}-{strCompanyID})",
            .isUHNWCustomer = isUHNWCustomer
        }
        'w.UCID = strCustID
        w.CustID(strClientID) = strCustID

        ShowWindow(w, wndMain, w.Text)
    End Sub

End Class
