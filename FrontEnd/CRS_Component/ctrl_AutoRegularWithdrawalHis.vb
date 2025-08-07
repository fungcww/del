'********************************************************************
' Admended By: Chopard Chan
' Add Function: Auto & Regular Withdrawal Form & History Enquiry
' Date: 06 July 2020
' Description: Auto & Regular Withdrawal
' Ref No.: ITSR 1607 & 1792
'********************************************************************

Public Class ctrl_AutoRegularWithdrawalHis

    Private SysEventLog As New SysEventLog.clsEventLog
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private objDBHeader As Utility.Utility.ComHeader
    Private strPolicyInUse As String
    Private strPolicyNoBas As String
    Private strPolicyNoDep As String
    Private datFromDate As DateTime
    Private datToDate As DateTime

    Private WithEvents objPOS As New LifeClientInterfaceComponent.clsPOS     'For calling clsPOS class

    Dim strErr As String = ""

#Region "MQ Properties"
    Public Property MQQueuesHeader() As Utility.Utility.MQHeader
        Get
            Return objMQQueHeader
        End Get
        Set(ByVal value As Utility.Utility.MQHeader)
            objMQQueHeader = value
        End Set
    End Property
#End Region
#Region " DBLogon Properties"
    Public Property DBHeader() As Utility.Utility.ComHeader
        Get
            Return objDBHeader
        End Get
        Set(ByVal value As Utility.Utility.ComHeader)
            objDBHeader = value
        End Set
    End Property
#End Region
    Public Property PolicyNoInUse() As String
        Get
            Return strPolicyInUse
        End Get
        Set(ByVal value As String)
            strPolicyInUse = value
        End Set
    End Property

    Public Property PolicyNoBasic() As String
        Get
            Return strPolicyNoBas
        End Get
        Set(ByVal value As String)
            strPolicyNoBas = value
        End Set
    End Property

    Public Property PolicyNoDependent() As String
        Get
            Return strPolicyNoDep
        End Get
        Set(ByVal value As String)
            strPolicyNoDep = value
        End Set
    End Property

    Private Sub ctrl_AutoRegularWithdrawalHis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AutoRegularWithdrawalHisLoad()

    End Sub

    Private Sub AutoRegularWithdrawalHisLoad()

        Try
            ' Check if policy is already registered
            Ctrl_AutoRegWithdrawalHis.Init()

            '' Set obj header  
            objPOS = New LifeClientInterfaceComponent.clsPOS()
            objPOS.MQQueuesHeader = objMQQueHeader
            objPOS.DBHeader = objDBHeader

            Dim dsReceData As New DataSet
            Dim dtAWD As DataTable = Nothing
            Dim dtRWD As DataTable = Nothing

            Dim bIsAWDReg As Boolean = False
            Dim bIsRWDReg As Boolean = False

            strErr = ""

            If Not objPOS.EnquiryARW(strPolicyInUse, dsReceData, strErr) Then
                HandleExceptionTryCatch(strErr)
            Else

                Dim strDTNameAWDDEP As String = "AWDENQDEP"
                Dim strDTNameAWDBAS As String = "AWDENQBAS"
                Dim strDTNameRWD As String = "RWDENQ"

                If Not String.IsNullOrEmpty(strErr) Then
                    'Err Msg Mapping?
                    MsgBox(strErr, MsgBoxStyle.Information, "Alert")
                Else

                    'RWD
                    If Not dsReceData Is Nothing AndAlso dsReceData.Tables.Contains(strDTNameRWD) AndAlso dsReceData.Tables(strDTNameRWD).Rows.Count > 0 Then
                        dtRWD = dsReceData.Tables(strDTNameRWD).Copy
                        bIsRWDReg = If(Not dtRWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register
                    End If


                    'AWD - Check is Dependent registered first
                    If Not dsReceData Is Nothing AndAlso dsReceData.Tables.Contains(strDTNameAWDDEP) AndAlso dsReceData.Tables(strDTNameAWDDEP).Rows.Count > 0 Then

                        If Not dsReceData.Tables(strDTNameAWDDEP).Rows(0)("Status") = " " Then
                            dtAWD = dsReceData.Tables(strDTNameAWDDEP).Copy
                            bIsAWDReg = If(Not dtAWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register
                        Else
                            'AWD - Check is Basic registered second
                            If Not dsReceData Is Nothing AndAlso dsReceData.Tables.Contains(strDTNameAWDBAS) AndAlso dsReceData.Tables(strDTNameAWDBAS).Rows.Count > 0 Then

                                dtAWD = dsReceData.Tables(strDTNameAWDBAS).Copy
                                bIsAWDReg = If(Not dtAWD.Rows(0)("Status") = " ", True, False) ' Status "" = not register

                            End If
                        End If

                    End If

                    'Default radio button index
                    If Not bIsAWDReg AndAlso bIsRWDReg Then
                        Ctrl_AutoRegWithdrawalHis.RadioARW2_Click()
                        'Ctrl_AutoRegWithdrawalHis.PolicyNoBasic = dtRWD.Rows(0)("PolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.PolicyNoBasic = dtRWD.Rows(0)("PolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.txtBasicPolicyNo.Text = dtRWD.Rows(0)("PolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.rdbAutoReg_1.Enabled = False
                    Else
                        Ctrl_AutoRegWithdrawalHis.RadioARW1_Click()
                        'Ctrl_AutoRegWithdrawalHis.PolicyNoBasic = dtAWD.Rows(0)("BasicPolicyNo")
                        'Ctrl_AutoRegWithdrawalHis.PolicyNoDependent = dtAWD.Rows(0)("DependentPolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.PolicyNoBasic = dtAWD.Rows(0)("BasicPolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.PolicyNoDependent = dtAWD.Rows(0)("DependentPolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.txtBasicPolicyNo.Text = dtAWD.Rows(0)("BasicPolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.txtDependentPolicyNo.Text = dtAWD.Rows(0)("DependentPolicyNo")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.rdbAutoReg_2.Enabled = False
                    End If

                    ' Not registered alert
                    If Not bIsAWDReg AndAlso Not bIsRWDReg Then
                        MsgBox("Auto or Regular Withdrawal are not registered yet.", MsgBoxStyle.Information, "Alert")
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.rdbAutoReg_1.Enabled = True
                        Ctrl_AutoRegWithdrawalHis.AutoRegWithdrawalHisEnq.rdbAutoReg_2.Enabled = True
                    End If


                End If ' Else end - If Not String.IsNullOrEmpty(strErr) Then

                'Me.Ctrl_AutoRegWithdrawalHis.PolicyNo = Me.strPolicyNo

            End If

            'Ctrl_DBSOPaymentHist.DBHeader = Me.DBHeader
            Ctrl_AutoRegWithdrawalHis.MQQueuesHeader = Me.MQQueuesHeader
            Ctrl_AutoRegWithdrawalHis.DBHeader = Me.DBHeader

        Catch ex As Exception
            HandleExceptionTryCatch(ex)
        End Try

    End Sub

#Region "HandleException"
    Private Sub HandleExceptionTryCatch(ByVal ex As Exception)
        strErr = ex.Message + "\n" + ex.StackTrace
        MsgBox(strErr, MsgBoxStyle.Critical)
    End Sub

    Private Sub HandleExceptionTryCatch(ByVal strErr As String)
        MsgBox(strErr, MsgBoxStyle.Critical)
    End Sub
#End Region


End Class
