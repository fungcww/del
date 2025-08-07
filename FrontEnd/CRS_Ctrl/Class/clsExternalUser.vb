Imports Utility.Utility

Public Module clsExternalUser


    Public ExternalUser As Boolean = False
    Private objComHeader As New ComHeader
    Private objMqHeader As New MQHeader

    Public Function MaskExternalUserData(ByVal type As MaskData, ByVal strVal As String) As String

        If strVal.Trim.Length = 0 Then
            MaskExternalUserData = String.Empty
            Exit Function
        End If
        strVal = strVal.Trim

        Select Case type

            Case MaskData.HKID
                strVal = "*" + strVal.Remove(0, 1)
                Return strVal.Remove(strVal.Length - 4, 4) + "****"

            Case MaskData.CREDIT_CARAD_NO, MaskData.BANK_ACCOUNT_NO
                Return strVal.Remove(strVal.Length - 4, 4) + "****"

        End Select

    End Function


    Public Enum MaskData
        BANK_ACCOUNT_NO = 0
        CREDIT_CARAD_NO = 1
        HKID = 2
    End Enum

    Public Function CheckExtranalUser(ByVal pstrUserName As String, ByRef IsExtranalUser As Boolean, ByRef strErr As String) As Boolean
        Dim objEU As New LifeClientInterfaceComponent.clsExternalUser
        objEU.ComHeader = EUComHeaderInUse
        Return objEU.GetCIWSysValue(pstrUserName, IsExtranalUser, strErr)
    End Function

    Public Property EUComHeaderInUse() As ComHeader
        Get
            Return objComHeader
        End Get
        Set(ByVal value As ComHeader)
            objComHeader = value
        End Set
    End Property

    Public Function MaskDTsource(ByRef dtInput As DataTable, ByVal strColumnName As String, ByVal type As MaskData, _
                    Optional ByVal strParentTableName As String = "", _
                    Optional ByVal strParentColumnName As String = "", _
                    Optional ByVal strChildTableName As String = "", _
                    Optional ByVal strChildcolumnName As String = "") As DataTable

        For row As Integer = 0 To dtInput.Rows.Count - 1

            For col As Integer = 0 To dtInput.Columns.Count - 1
                If strColumnName.Trim.ToUpper = dtInput.Columns(col).ColumnName().Trim.ToUpper And _
                       (Not String.IsNullOrEmpty(Convert.ToString(dtInput.Rows(row)(col)))) Then
                    dtInput.Rows(row)(col) = MaskExternalUserData(type, dtInput.Rows(row)(col))
                End If
            Next
        Next

        'Mask Parent DataSet
        For Each parentrel As DataRelation In dtInput.ParentRelations
            For Each dtparent As DataTable In parentrel.DataSet.Tables
                If dtparent.TableName.Trim.ToUpper = strParentTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtparent.Rows.Count - 1
                        For col As Integer = 0 To dtparent.Columns.Count - 1

                            If strParentColumnName.Trim.ToUpper = dtparent.Columns(col).ColumnName().Trim.ToUpper And _
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtparent.Rows(row)(col)))) Then
                                dtparent.Rows(row)(col) = MaskExternalUserData(type, dtparent.Rows(row)(col))
                            End If

                        Next
                    Next

                End If
            Next
        Next

        'Mask Child DataSet
        For Each childrel As DataRelation In dtInput.ChildRelations
            For Each dtchild As DataTable In childrel.DataSet.Tables
                If dtchild.TableName.Trim.ToUpper = strChildTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtchild.Rows.Count - 1
                        For col As Integer = 0 To dtchild.Columns.Count - 1

                            If strChildcolumnName.Trim.ToUpper = dtchild.Columns(col).ColumnName().Trim.ToUpper And _
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtchild.Rows(row)(col)))) Then
                                dtchild.Rows(row)(col) = MaskExternalUserData(type, dtchild.Rows(row)(col))
                            End If

                        Next
                    Next

                End If
            Next
        Next

        Return dtInput

    End Function

    Public Function MaskedCreditCardIn64(ByVal creditCardNumber As String) As String
        If creditCardNumber IsNot Nothing Then
            creditCardNumber = creditCardNumber.Trim
            Dim len As Integer = creditCardNumber.Length
            If len > 15 Then
                Dim first6 As String = creditCardNumber.Substring(0, 6)
                Dim last4 As String = creditCardNumber.Substring(len - 4, 4)
                Dim masked As String = String.Empty
                If len = 16 Then
                    'Handle no space
                    masked = "XXXXXX"
                ElseIf len = 19 Then
                    'Handle with space 
                    masked = "XXX XXXX "
                End If

                creditCardNumber = String.Concat(first6, masked, last4)
            End If
            Return creditCardNumber
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Add by ITDSCH on 2016-12-15
    ''' </summary>
    ''' <param name="creditCardNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MaskedCreditCard(ByVal creditCardNumber As String) As String
        If creditCardNumber IsNot Nothing Then
            creditCardNumber = creditCardNumber.Trim
            Dim len As Integer = creditCardNumber.Length
            If len > 4 Then
                Dim i As Integer = 0
                Dim number As String = ""
                For i = 0 To len - 5
                    number = number + "X"
                Next
                number = number + creditCardNumber.Substring(len - 4, 4)
                Return number
            End If
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Add by ITDSCH on 2016-12-15
    ''' </summary>
    ''' <param name="dtInput"></param>
    ''' <param name="strColumnName"></param>
    ''' <param name="type"></param>
    ''' <param name="strParentTableName"></param>
    ''' <param name="strParentColumnName"></param>
    ''' <param name="strChildTableName"></param>
    ''' <param name="strChildcolumnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MaskDTCreditCard(ByRef dtInput As DataTable, ByVal strColumnName As String, ByVal matchDt As DataTable, _
                    Optional ByVal strParentTableName As String = "", _
                    Optional ByVal strParentColumnName As String = "", _
                    Optional ByVal strChildTableName As String = "", _
                    Optional ByVal strChildcolumnName As String = "") As DataTable

        For row As Integer = 0 To dtInput.Rows.Count - 1

            For col As Integer = 0 To dtInput.Columns.Count - 1
                If strColumnName.Trim.ToUpper = dtInput.Columns(col).ColumnName().Trim.ToUpper And _
                       (Not String.IsNullOrEmpty(Convert.ToString(dtInput.Rows(row)(col)))) Then
                    For m As Integer = 0 To matchDt.Rows.Count - 1
                        If dtInput.Rows(row)(col).Trim = matchDt.Rows(m)("Acct_No").ToString.Trim Then
                            Dim factorHse As String = matchDt.Rows(m)("Factor_Hse").ToString.Trim
                            If factorHse = "02" OrElse factorHse = "03" OrElse factorHse = "04" OrElse factorHse = "22" OrElse factorHse = "26" Then
                                dtInput.Rows(row)(col) = MaskedCreditCard(dtInput.Rows(row)(col))
                            End If
                        End If
                    Next
                End If
            Next
        Next

        'Mask Parent DataSet
        For Each parentrel As DataRelation In dtInput.ParentRelations
            For Each dtparent As DataTable In parentrel.DataSet.Tables
                If dtparent.TableName.Trim.ToUpper = strParentTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtparent.Rows.Count - 1
                        For col As Integer = 0 To dtparent.Columns.Count - 1

                            If strParentColumnName.Trim.ToUpper = dtparent.Columns(col).ColumnName().Trim.ToUpper And _
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtparent.Rows(row)(col)))) Then
                                For m As Integer = 0 To matchDt.Rows.Count - 1
                                    If dtInput.Rows(row)(col).Trim = matchDt.Rows(m)("Acct_No").ToString.Trim Then
                                        Dim factorHse As String = matchDt.Rows(m)("Factor_Hse").ToString.Trim
                                        If factorHse = "02" OrElse factorHse = "03" OrElse factorHse = "04" OrElse factorHse = "22" OrElse factorHse = "26" Then
                                            dtparent.Rows(row)(col) = MaskedCreditCard(dtparent.Rows(row)(col))
                                        End If
                                    End If
                                Next
                            End If

                        Next
                    Next

                End If
            Next
        Next

        'Mask Child DataSet
        For Each childrel As DataRelation In dtInput.ChildRelations
            For Each dtchild As DataTable In childrel.DataSet.Tables
                If dtchild.TableName.Trim.ToUpper = strChildTableName.Trim.ToUpper Then

                    For row As Integer = 0 To dtchild.Rows.Count - 1
                        For col As Integer = 0 To dtchild.Columns.Count - 1

                            If strChildcolumnName.Trim.ToUpper = dtchild.Columns(col).ColumnName().Trim.ToUpper And _
                                                 (Not String.IsNullOrEmpty(Convert.ToString(dtchild.Rows(row)(col)))) Then
                                For m As Integer = 0 To matchDt.Rows.Count - 1
                                    If dtInput.Rows(row)(col).Trim = matchDt.Rows(m)("Acct_No").ToString.Trim Then
                                        Dim factorHse As String = matchDt.Rows(m)("Factor_Hse").ToString.Trim
                                        If factorHse = "02" OrElse factorHse = "03" OrElse factorHse = "04" OrElse factorHse = "22" OrElse factorHse = "26" Then
                                            dtchild.Rows(row)(col) = MaskedCreditCard(dtchild.Rows(row)(col))
                                        End If
                                    End If

                                Next
                            End If

                        Next
                    Next

                End If
            Next
        Next

        Return dtInput

    End Function

End Module
