'**************************************************************************
'   Amendment      : Life/Asia
'   Date                 : 2016-03-01
'   Changed by       : Thomas Yung
'   ITSR No.          : 
'   Desc. : ITDYMH 20150229 Post-Sales Call
'**************************************************************************

Imports System.Data.SqlClient

Public Class frmSelectMsgTemp
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
    Private blnShowChi As Boolean = False
    Private strOutputType As String = ""
    Private dsCurr As New DataSet

    Private strSelectedText As String = ""


    Private WithEvents objCI As LifeClientInterfaceComponent.clsPOS
    Private clsPOS As New LifeClientInterfaceComponent.clsPOS
    Private SysEventLog As New SysEventLog.clsEventLog


    Private objDBHeader As Utility.Utility.ComHeader          'DBHeader includes MSSQL conn. pararmeters

    'added by ITDYMH 20150229 Post-Sales Call
    Private sqlConn As New SqlConnection
    Private dsSMS As New DataSet
    Public strPolicyID As String
    Private daSMSmsg As New SqlDataAdapter


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
    Public Property ShowChi() As Boolean
        Get
            Return blnShowChi
        End Get
        Set(ByVal value As Boolean)
            blnShowChi = value
        End Set
    End Property

    Public Property SelectedText() As String
        Get
            Return strSelectedText
        End Get
        Set(ByVal value As String)
            strSelectedText = value
        End Set
    End Property

    Public Property OutputType() As String
        Get
            Return strOutputType
        End Get
        Set(ByVal value As String)
            strOutputType = value
        End Set
    End Property


    Private Sub FormateDgSelCo(ByVal dt As DataTable)
        Try
            Dim gdStyle As New DataGridTableStyle
            gdStyle.MappingName = dt.TableName

            Dim aCol1 As New DataGridTextBoxColumn
            Dim aCol2 As New DataGridTextBoxColumn



            dgSelCo.TableStyles.Clear()
            With aCol1

                If blnShowChi Then
                    .MappingName = "ChiText"
                    .HeaderText = "Message Text (Chi)"
                Else
                    .MappingName = "EngText"
                    .HeaderText = "Message Text (Eng)"
                End If
                .Width = 500
                .NullText = ""
            End With


            With gdStyle.GridColumnStyles
                .Add(aCol1)
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

    Private Sub frmSelectMsgTemp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim s_Sql As String
            sqlConn.ConnectionString = strCIWConn
            'oliver 2024-7-11 added for Table_Relocate_Sprint 14
            Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)
            Select Case strOutputType.ToUpper
                Case "SMS"
                    s_Sql = "select Description as EngText, Value as ChiText from " & serverPrefix & " CodeTable where Code = 'MsgTemp'"
                Case "EMAIL"
                    s_Sql = "select Description as EngText, Value as ChiText from " & serverPrefix & " CodeTable where Code = 'EmailTemp'"
            End Select


            Try
                'Fill the datatables in dataset
                If dsSMS.Tables.Contains("MsgTemplate") Then
                    dsSMS.Tables.Remove("MsgTemplate")
                End If
                daSMSmsg = New SqlDataAdapter(s_Sql, sqlConn)
                daSMSmsg.Fill(dsSMS, "MsgTemplate")

            Catch ex As Exception
                MsgBox("Fail to get Message template")
            End Try


            Dim dt_Msg As New DataTable

            dt_Msg = dsSMS.Tables("MsgTemplate")

            'dt_Msg.Columns.Add("Description", Type.GetType("System.String"))
            'dt_Msg.Columns.Add("Value", Type.GetType("System.String"))

            'Dim dr_New As DataRow = dt_Msg.NewRow
            'dr_New("Description") = "富衛人壽：多謝閣下購買投資相連壽險計劃。我們將發出信件通知您有關保單的重要資料。如在未來兩星期仍未收到，請致電31233123與我們聯絡。"
            'dr_New("Value") = "FWD Life: Thank you for purchasing Investment linked Assurance Scheme policy. A letter containing further important information will be posted to you. Please contact us at 31233123 if you do not receive it within the next 2 weeks."

            'dt_Msg.Rows.Add(dr_New)
            'dt_Msg.AcceptChanges()

            dgSelCo.DataSource = dt_Msg
            FormateDgSelCo(dt_Msg)

        Catch ex As Exception
            SysEventLog.CiwHeader = objDBHeader
            SysEventLog.ProcEventLog("Error", Now, objDBHeader.UserID, objDBHeader.ProjectAlias, objDBHeader.MachineID, ex.Message, ex.StackTrace, "", "", "", True)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If dgSelCo.Item(dgSelCo.CurrentRowIndex, 0).ToString.Trim <> "" Then
                strSelectedText = dgSelCo.Item(dgSelCo.CurrentRowIndex, 0).ToString()
            End If
            SelectMode = False
            Me.Close()
        Catch ex As Exception
            
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        SelectMode = False
        strSelectedText = String.Empty
        Me.Close()
    End Sub
End Class