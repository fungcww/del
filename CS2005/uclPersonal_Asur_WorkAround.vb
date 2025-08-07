Imports System.Data.SqlClient

''' <remarks>
''' Added by Hugo Chan on 2021-05-14
''' Project: CRS - First Level of Access
''' </remarks>
Public Class uclPersonal_Asur_WorkAround
    Inherits System.Windows.Forms.UserControl

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If CheckUPSAccess("Save Demographic Data") = False Then
            Me.cmdSave.Visible = False
            Me.cmdCancel.Visible = False
            Me.txtRemark.ReadOnly = True
            Me.txtRating.ReadOnly = True
        Else
        End If

    End Sub

    Private ds As DataSet = New DataSet("Personal")
    Private strCustID, strClientID As String
    Private dr, dr1 As DataRow
    Private blnEditable As Boolean
    Private blnCovSmoker As Boolean

    Public Property CustID(ByVal srcDT As DataTable, ByVal srcDT_Ext As DataTable) As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                If Value.Length > 0 Then
                    strCustID = Value
                    srcDT.TableName = "Customer"
                    If ds.Tables.Contains(srcDT.TableName) Then
                        ds.Tables(srcDT.TableName).Constraints.Clear()
                        ds.Relations.Clear()
                        ds.Tables.Remove(srcDT.TableName)
                    End If
                    ds.Tables.Add(srcDT)

                    srcDT_Ext.TableName = "Customer_Ext"
                    If ds.Tables.Contains(srcDT_Ext.TableName) Then
                        ds.Tables(srcDT_Ext.TableName).Constraints.Clear()
                        ds.Relations.Clear()
                        ds.Tables.Remove(srcDT_Ext.TableName)
                    End If
                    ds.Tables.Add(srcDT_Ext)

                    Call buildUI()
                End If
            End If
        End Set
    End Property

    'Clear All Data in the control
    Public Sub ClearTextBox()
        Dim ctl As Control

        For Each ctl In Me.Controls
            Select Case TypeName(ctl).ToUpper
                Case "TEXTBOX"
                    CType(ctl, TextBox).Text = ""
                Case "COMBOBOX"
                    CType(ctl, ComboBox).SelectedIndex = -1
                Case "CHECKBOX"
                    CType(ctl, CheckBox).Checked = False
            End Select
        Next
    End Sub

    Public Sub EnableButtons(ByVal blnCanEdit As Boolean)
        blnEditable = blnCanEdit
        Me.cmdSave.Enabled = blnEditable
        Me.cmdCancel.Enabled = blnEditable
    End Sub

    Private Sub buildUI()

        If ds.Tables("Customer").Rows.Count > 0 Then
            With ds.Tables("Customer").Rows(0)

                If Not IsDBNull(.Item("Title")) Then Me.txtTitle.Text = .Item("Title")
                If Not IsDBNull(.Item("Name")) Then Me.txtName.Text = .Item("Name")
                If Not IsDBNull(.Item("Customer_Type")) Then Me.txtCustType.Text = .Item("Customer_Type")
                If Not IsDBNull(.Item("Status")) Then Me.txtStatus.Text = .Item("Status")
                If Not IsDBNull(.Item("ID_Card")) Then
                    ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                    'If ExternalUser Then
                    '    Me.txtIDCard.Text = MaskExternalUserData(MaskData.HKID, .Item("ID_Card"))
                    'Else
                    '    Me.txtIDCard.Text = .Item("ID_Card")
                    'End If
                    Me.txtIDCard.Text = .Item("ID_Card")
                End If
                If Not IsDBNull(.Item("Contact_No")) Then Me.txtTel.Text = .Item("Contact_No")
                If Not IsDBNull(.Item("Mobile_No")) Then Me.txtMobile.Text = .Item("Mobile_No")
                If Not IsDBNull(.Item("Sex")) Then Me.txtSex.Text = .Item("Sex")
                If Not IsDBNull(.Item("Date_of_Birth")) Then Me.txtDOB.Text = .Item("Date_of_Birth")
                If Not IsDBNull(.Item("Age")) Then Me.txtAge.Text = .Item("Age")
                If Not IsDBNull(.Item("Place_of_Birth")) Then Me.txtPOB.Text = .Item("Place_of_Birth")
                If Not IsDBNull(.Item("Marital_Status")) Then Me.txtMarital.Text = .Item("Marital_Status")
                If Not IsDBNull(.Item("Occupation")) Then Me.txtOccup.Text = .Item("Occupation")
                If Not IsDBNull(.Item("Email")) Then Me.txtEmail.Text = .Item("Email")
                If Not IsDBNull(.Item("Nationality")) Then Me.txtCountry.Text = .Item("Nationality")
                If Not IsDBNull(.Item("Language")) Then Me.txtLang.Text = .Item("Language")
                If Not IsDBNull(.Item("Education")) Then Me.txtEduLevel.Text = .Item("Education")
                If Not IsDBNull(.Item("ID_copy")) Then
                    If .Item("ID_copy") = "Y" Then Me.chkAgeAdm.Checked = True
                End If
                If Not IsDBNull(.Item("Personal_Income")) Then Me.txtPersonalIncome.Text = .Item("Personal_Income")

            End With

        Else
            Call ClearTextBox()
        End If

        If ds.Tables("Customer_Ext").Rows.Count > 0 Then
            With ds.Tables("Customer_Ext").Rows(0)

                Me.txtRating.Text = IIf(.IsNull("Rating"), gNULLText, .Item("Rating"))
                Me.txtRemark.Text = IIf(.IsNull("Remark"), gNULLText, .Item("Remark"))

            End With
        Else
            Me.txtRating.Text = gNULLText
            Me.txtRemark.Text = gNULLText
        End If

        ''CRS 7x24 Changes - Start
        'If ExternalUser Then
        '    cmdSave.Enabled = False
        '    cmdCancel.Enabled = False
        'End If
        ''CRS 7x24 Changes - End
    End Sub

    Private Function Validation() As Boolean
        Dim strErrMsg As String
        Dim strRating As String

        Validation = False

        'Check Input
        strRating = Me.txtRating.Text.Trim
        If strRating.Length > 0 Then
            If Not IsNumeric(strRating) Then
                MsgBox("Rating should be a integer.", MsgBoxStyle.Critical, "Error")
                txtRating.Focus()
                Exit Function
            ElseIf strRating > 999999999 Then
                MsgBox("Rating is out of range.", MsgBoxStyle.Critical, "Error")
                txtRating.Focus()
                Exit Function
            ElseIf Int(strRating) <> strRating Then
                MsgBox("Rating should be a integer.", MsgBoxStyle.Critical, "Error")
                txtRating.Focus()
                Exit Function
            End If
        End If

        Validation = True

    End Function

    Private Sub SaveExt()
        Dim sqlConn As New SqlConnection
        Dim sqlCmd As SqlCommand
        Dim dtData As DataTable
        Dim drData As DataRow
        Dim strErrMsg As String

        Dim strSQL, strInsertSQL, strUpdSQL As String
        Dim strRating, strRemark As String


        If Me.txtRating.Text.Trim.Length > 0 Then
            strRating = Me.txtRating.Text.Trim
        Else
            strRating = "Null"
        End If
        strRemark = Me.txtRemark.Text.Trim


        strInsertSQL = "Insert Into Cust_Ext_Asur (UCID, Rating, Remark)"
        strInsertSQL &= " Values ('" & strCustID.Replace("'", "''") & "', " & strRating & ", N'" & strRemark.Replace("'", "''") & "')"

        strUpdSQL = "Update Cust_Ext_Asur Set Rating = " & strRating & ","
        strUpdSQL &= " Remark = N'" & strRemark.Replace("'", "''") & "'"
        strUpdSQL &= " Where UCID = '" & strCustID.Replace("'", "''") & "'"

        strSQL = "If exists (Select UCID From Cust_Ext_Asur Where UCID = '" & strCustID.Replace("'", "''") & "')"
        strSQL &= " " & strUpdSQL & " else " & strInsertSQL

        Try
            sqlConn.ConnectionString = strCIWConn
            sqlConn.Open()
            sqlCmd = sqlConn.CreateCommand
            sqlCmd.Connection = sqlConn

            'Insert/Update Campaign Tracking
            sqlCmd.CommandText = strSQL
            sqlCmd.ExecuteNonQuery()

            MsgBox("Data is saved.", MsgBoxStyle.Information, "Information")

            'Update DataTable for Cancel Button
            If ds.Tables("Customer_Ext").Rows.Count = 0 Then
                drData = ds.Tables("Customer_Ext").NewRow
                drData.Item("UCID") = strCustID
            Else
                drData = ds.Tables("Customer_Ext").Rows(0)
            End If
            If Me.txtRating.Text.Trim.Length > 0 Then
                drData.Item("Rating") = Me.txtRating.Text.Trim
            Else
                drData.Item("Rating") = DBNull.Value
            End If
            drData.Item("Remark") = strRemark

            ds.Tables("Customer_Ext").LoadDataRow(drData.ItemArray, True)

        Catch ex As Exception
            LogInformation("Error", "", "uclPersonal_Asur - SaveExt", ex.Message, ex)

            Throw ex
        Finally
            sqlConn.Close()
            sqlConn.Dispose()
            sqlCmd.Dispose()
        End Try

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If Validation() Then
                Call SaveExt()
            End If
        Catch sqlex As SqlClient.SqlException
            LogInformation("Error", "", "uclPersonal_Asur - cmdSave_Click", sqlex.Message, sqlex)

            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            LogInformation("Error", "", "uclPersonal_Asur - cmdSave_Click", ex.Message, ex)

            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Try
            Call buildUI()
        Catch ex As Exception
            LogInformation("Error", "", "uclPersonal_Asur - cmdCancel_Click", ex.Message, ex)

            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

    End Sub

End Class
