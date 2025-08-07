'**************************************************************************
'   Amendment      : Life/Asia
'   Date                 : Jun 22, 2007
'   Changed by       : Fiona Cheung (FC)
'   ITSR No.          : ITSR0707114
'**************************************************************************

Public Class frmSelectCO
    Private strPolicyNo As String = ""
    Private strUserId As String = ""
    Private strMachID As String = ""
    Private strSystem As String = ""
    Private strEnv As String = ""
    Private strComp As String = ""
    Private strBasicPlan As String = ""
    Private strMach As String = ""
    Private strErr As String = ""
    Private strMsg As String = ""
    Private blnSel As Boolean = False
    Private dsCurr As New DataSet


    Private WithEvents objCI As LifeClientInterfaceComponent.clsPOS
    Private objMQQueHeader As Utility.Utility.MQHeader
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Private SysEventLog As New SysEventLog.clsEventLog


    Private objDBHeader As Utility.Utility.ComHeader          'DBHeader includes MSSQL conn. pararmeters

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

    Public Property policyInuse() As String
        Get
            Return strPolicyNo
        End Get
        Set(ByVal value As String)
            strPolicyNo = value
        End Set
    End Property
    Public Property userInuse() As String
        Get
            Return strUserId
        End Get
        Set(ByVal value As String)
            strUserId = value
        End Set
    End Property
    Public Property MachineInuse() As String
        Get
            Return strMach
        End Get
        Set(ByVal value As String)
            strMach = value
        End Set
    End Property
    Public Property SystemInuse() As String
        Get
            Return strSystem
        End Get
        Set(ByVal value As String)
            strSystem = value
        End Set
    End Property
    Public Property EnvInuse() As String
        Get
            Return strEnv
        End Get
        Set(ByVal value As String)
            strEnv = value
        End Set
    End Property
    Public Property CompanyInuse() As String
        Get
            Return strComp
        End Get
        Set(ByVal value As String)
            strComp = value
        End Set
    End Property
    Public Property CurrdsInuse()
        Get
            Return dsCurr
        End Get
        Set(ByVal value)
            dsCurr = value
        End Set
    End Property
    Public Property BasicPlan() As String
        Get
            Return strBasicPlan
        End Get
        Set(ByVal value As String)
            strBasicPlan = value
        End Set
    End Property
    Public Property SelectMode() As Boolean
        Get
            Return blnSel
        End Get
        Set(ByVal value As Boolean)
            blnSel = value
        End Set
    End Property
    Private Sub FormateDgSelCo(ByVal dt As DataTable)
        Try
            Dim gdStyle As New DataGridTableStyle
            gdStyle.MappingName = dt.TableName

            Dim aCol1 As New DataGridTextBoxColumn
            Dim aCol2 As New DataGridTextBoxColumn
            Dim aCol3 As New DataGridTextBoxColumn
            Dim aCol4 As New DataGridTextBoxColumn
            Dim aCol5 As New DataGridTextBoxColumn

            Dim aCol6 As New DataGridTextBoxColumn
            Dim aCol7 As New DataGridTextBoxColumn
            Dim aCol8 As New DataGridTextBoxColumn
            Dim aCol9 As New DataGridTextBoxColumn
            Dim aCol10 As New DataGridTextBoxColumn
            Dim aCol11 As New DataGridTextBoxColumn
            Dim aCol12 As New DataGridTextBoxColumn


            dgSelCo.TableStyles.Clear()

            With aCol1
                .MappingName = "Life"
                .HeaderText = "Life"
                .Width = 50
                .NullText = ""
            End With

            With aCol2
                .MappingName = "Name"
                .HeaderText = "Name"
                .Width = 100
                .NullText = ""
            End With

            With aCol3
                .MappingName = "Cov"
                .HeaderText = "Coverage"
                .Width = 50
                .NullText = ""
            End With

            With aCol4
                .MappingName = "Rider"
                .HeaderText = "Rider"
                .Width = 50
                .NullText = ""
            End With

            With aCol5
                .MappingName = "Cov_Desc"
                .HeaderText = "Description"
                .Width = 300
                .NullText = ""
            End With

            With aCol6
                .MappingName = "Risk_Sts"
                .HeaderText = "Risk Status"
                .Width = 50
                .NullText = ""
            End With

            With aCol7
                .MappingName = "Prem_sts"
                .HeaderText = "Prem status"
                .Width = 70
                .NullText = ""
            End With

            With aCol8
                .MappingName = "SI"
                .HeaderText = "Sum assured"
                .Width = 0
                .NullText = ""
            End With

            With aCol9
                .MappingName = "Premium"
                .HeaderText = "Premium"
                .Width = 0
                .NullText = ""
            End With

            With aCol10
                .MappingName = "RCD"
                .HeaderText = "RCD"
                .Width = 0
                .NullText = ""
            End With

            With aCol11
                .MappingName = "Risk_Cess_D"
                .HeaderText = "RED"
                .Width = 0
                .NullText = ""
            End With

            With aCol12
                .MappingName = "PCD"
                .HeaderText = "PCD"
                .Width = 0
                .NullText = ""
            End With
            With gdStyle.GridColumnStyles
                .Add(aCol1)
                .Add(aCol2)
                .Add(aCol3)
                .Add(aCol4)
                .Add(aCol5)
                .Add(aCol6)
                .Add(aCol7)
                .Add(aCol8)
                .Add(aCol9)
                .Add(aCol10)
                .Add(aCol11)
                .Add(aCol12)
            End With


            'Adding styple for table
            dgSelCo.TableStyles.Add(gdStyle)

            'Setting scollbar
            dgSelCo.Controls(0).Enabled = True
            dgSelCo.Controls(1).Enabled = True

            'setting grid to read only 
            dgSelCo.ReadOnly = True


            With dgSelCo.TableStyles(0).GridColumnStyles.Item(0)

                .Width = .Width + 1
                .Width = .Width - 1

            End With

        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmSelectCO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim strErr As String = ""
            clsPOS.MQQueuesHeader = Me.objMQQueHeader

            'create dataset
            Dim dsSendData As New DataSet
            Dim dtSendData As New DataTable
            Dim dsReceData As New DataSet
            Dim dr As DataRow = dtSendData.NewRow()
            dtSendData.Columns.Add("PolicyNo")
            dr("PolicyNo") = strPolicyNo
            dtSendData.Rows.Add(dr)
            dsSendData.Tables.Add(dtSendData)

            If clsPOS.GetCOSelRecord(dsSendData, dsReceData, strErr) Then
                If dsReceData.Tables(1).Rows.Count > 0 Then
                    If dsCurr.Tables.Count > 0 Then
                        For i As Integer = 0 To dsCurr.Tables.Count - 1
                            dsCurr.Tables.RemoveAt(0)
                        Next
                    End If
                    dsCurr = dsReceData.Copy
                    dgSelCo.DataSource = dsCurr.Tables(1)
                    FormateDgSelCo(dsCurr.Tables(1))
                End If
            Else
                If Trim(strErr) <> "" Then MsgBox(strErr)
                strErr = ""
            End If
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            Dim nRows() As DataRow
            strBasicPlan = ""

            nRows = dsCurr.Tables(1).Select("Life = '01' and Cov = '01' and Rider = '00'")

            If nRows.Length > 0 Then
                strBasicPlan = nRows(0).Item("Cov_Code")
            End If

            If dgSelCo.Item(dgSelCo.CurrentRowIndex, 3).ToString.Trim <> "00" Then
                MsgBox("Invalid selection!!", MsgBoxStyle.Information)
                Exit Sub
            End If

            dsCurr.Tables(1).DefaultView.RowFilter = "Life = '" & dgSelCo.Item(dgSelCo.CurrentRowIndex, 0) & "' and Cov = '" & dgSelCo.Item(dgSelCo.CurrentRowIndex, 2) & "' and Rider = '" & dgSelCo.Item(dgSelCo.CurrentRowIndex, 3) & "'"

            SelectMode = True
            Me.Close()
        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SelectMode = False
        Me.Close()
    End Sub
End Class