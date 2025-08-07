'********************************************************************
' Created By:  Kay Tsang
' Date:         11 Nov 2016
' Project:      ITSR492
' Ref:          KT20161111
' Changes:      Add visit CS flag panel
'********************************************************************
Public Class ctrl_VisitCS

    Private objDBHeader As Utility.Utility.ComHeader
    Private strPolicyNo As String = ""
    Private strCustomerID As String = ""
    Private strLang As String = "en"
    Private strCsFlag As String = "N/A"
    Private strUpdDate As String = ""
    Private strUpdUser As String = "" 'from check response
    Private strUpdUser2 As String = "" 'for update request

#Region " Property Setting"
    Public Property dbHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property
#End Region
    'input
    Public Property PolicyNo() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property

    Public Property CustomerID() As String
        Get
            Return strCustomerID
        End Get
        Set(ByVal value As String)
            strCustomerID = value
        End Set
    End Property

    Public Property Lang() As String
        Get
            Return strLang
        End Get
        Set(ByVal value As String)
            strLang = value
        End Set
    End Property

    'output
    Public Property CSFlag() As String
        Get
            Return strCsFlag
        End Get
        Set(ByVal value As String)
            strCsFlag = value
        End Set
    End Property

    Public Property UpdateDate() As String
        Get
            Return strUpdDate
        End Get
        Set(ByVal value As String)
            strUpdDate = value
        End Set
    End Property

    Public Property UpdateUser() As String
        Get
            Return strUpdUser
        End Get
        Set(ByVal value As String)
            strUpdUser = value
        End Set
    End Property

    Public Property UpdateUser2() As String
        Get
            Return strUpdUser2
        End Get
        Set(ByVal value As String)
            strUpdUser2 = value
        End Set
    End Property

    Public Sub checkVisitCSFlag()
        strUpdUser2 = Me.UpdateUser2
        Dim objVisitCSResponse As CRS_Util.clsJSONBusinessObj.clsVisitCSFlagResponse = CRS_Util.clsJSONTool.CallCheckVisitCSWS(Me.PolicyNo.Trim, Me.CustomerID.Trim, Me.Lang)
        If objVisitCSResponse.msg.resultCode = "0" Then 'success
            Dim Flag As String = objVisitCSResponse.visitCSFlag
            Dim dt As String = IIf(objVisitCSResponse.updateDate Is Nothing, Nothing, objVisitCSResponse.updateDate)
            Dim updUser As String = IIf(objVisitCSResponse.updateUser Is Nothing, Nothing, objVisitCSResponse.updateUser)
            strCsFlag = Flag
            If Not dt Is Nothing Then
                strUpdDate = Format(CDate(dt), "yyyy-MM-dd")
            Else
                strUpdDate = ""
            End If
            If Not updUser Is Nothing Then
                strUpdUser = updUser
            Else
                strUpdUser = ""
            End If
        Else
            strUpdDate = Me.UpdateDate
            strUpdUser = Me.UpdateUser
            strCsFlag = Me.CSFlag
        End If
        RefreshDisplay()
    End Sub

    Public Function updateVisitCSFlag() As Boolean
        Dim boolResult = False
        Dim Flag As String = IIf(Me.cboCsFlag.SelectedItem = "N/A", "U", Me.cboCsFlag.SelectedItem)
        Dim objVisitCSResponse As CRS_Util.clsJSONBusinessObj.clsVisitCSFlagResponse = CRS_Util.clsJSONTool.CallUpdateVisitCSWS(Me.PolicyNo.Trim, Me.CustomerID.Trim, Flag, Me.Lang, Me.UpdateUser2.Trim)
        If objVisitCSResponse.msg.resultCode = "0" Then ''success
            boolResult = True
        Else
            boolResult = False
        End If
        checkVisitCSFlag()
        Return boolResult
    End Function

    Public Sub RefreshDisplay()
        Me.lblUpdDateValue.Text = Me.UpdateDate
        Me.lblUpdUserValue.Text = Me.UpdateUser
        Me.cboCsFlag.SelectedItem = IIf(Me.CSFlag = "U", "N/A", Me.CSFlag)
        Dim boolLAS = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings.Item("IsLAS"))
        Me.btnChange.Enabled = IIf(boolLAS, False, True) 'enable for CRS, disable for LAS
    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Me.btnChange.Enabled = False
        Me.btnUpdate.Enabled = True
        Me.cboCsFlag.Enabled = True
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Me.btnUpdate.Enabled = False
        Me.cboCsFlag.Enabled = False
        Me.btnChange.Enabled = True
        updateVisitCSFlag()
    End Sub
End Class
