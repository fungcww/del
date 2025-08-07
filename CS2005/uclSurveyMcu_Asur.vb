Imports System.Data.SqlClient

Public Class uclSurveyMcu_Asur
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents cmdDisResult As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents cboSurvey As ComboBox
    Friend WithEvents grdSurvey As DataGrid

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdDisResult = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboSurvey = New System.Windows.Forms.ComboBox()
        Me.grdSurvey = New System.Windows.Forms.DataGrid()
        CType(Me.grdSurvey, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdDisResult
        '
        Me.cmdDisResult.Location = New System.Drawing.Point(645, 16)
        Me.cmdDisResult.Name = "cmdDisResult"
        Me.cmdDisResult.Size = New System.Drawing.Size(92, 34)
        Me.cmdDisResult.TabIndex = 37
        Me.cmdDisResult.Text = "Display Result"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(61, 20)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Survey:"
        '
        'cboSurvey
        '
        Me.cboSurvey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSurvey.Items.AddRange(New Object() {"S0002 - 2003 Welcome Kit Survey ", "S0003 - 2004 Welcome Kit Survey ", "S0004 - 2004 Customer Satisfaction Survey", "S0005 - 2005 Customer Satisfaction Survey"})
        Me.cboSurvey.Location = New System.Drawing.Point(83, 20)
        Me.cboSurvey.Name = "cboSurvey"
        Me.cboSurvey.Size = New System.Drawing.Size(556, 28)
        Me.cboSurvey.TabIndex = 35
        '
        'grdSurvey
        '
        Me.grdSurvey.AlternatingBackColor = System.Drawing.Color.White
        Me.grdSurvey.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSurvey.BackColor = System.Drawing.Color.White
        Me.grdSurvey.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdSurvey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdSurvey.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdSurvey.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdSurvey.CaptionVisible = False
        Me.grdSurvey.DataMember = ""
        Me.grdSurvey.FlatMode = True
        Me.grdSurvey.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdSurvey.ForeColor = System.Drawing.Color.Black
        Me.grdSurvey.GridLineColor = System.Drawing.Color.Wheat
        Me.grdSurvey.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdSurvey.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdSurvey.HeaderForeColor = System.Drawing.Color.Black
        Me.grdSurvey.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdSurvey.Location = New System.Drawing.Point(16, 65)
        Me.grdSurvey.Name = "grdSurvey"
        Me.grdSurvey.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdSurvey.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdSurvey.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdSurvey.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdSurvey.Size = New System.Drawing.Size(985, 361)
        Me.grdSurvey.TabIndex = 34
        '
        'uclSurveyMcu
        '
        Me.Controls.Add(Me.cmdDisResult)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboSurvey)
        Me.Controls.Add(Me.grdSurvey)
        Me.Name = "uclSurveyMcu"
        Me.Size = New System.Drawing.Size(1017, 443)
        CType(Me.grdSurvey, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private gdtSurvey, gdtCustSurvey As DataTable
    Private strCustID, strClientID As String

    Public Property CustID() As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                If Value.Length > 0 Then
                    strCustID = Value
                    Call buildUI()
                End If
            End If
        End Set
    End Property

    Public Function resetDS()
        Me.gdtCustSurvey.Clear()
        Me.grdSurvey.TableStyles.Clear()
    End Function

    Private Function buildUI()
        'fill the survey combobox
        fillSurvey()

        If gdtCustSurvey Is Nothing Then
            gdtCustSurvey = New DataTable("CustSurvey")
            gdtCustSurvey.Columns.Add("Question", Type.GetType("System.String"))
            gdtCustSurvey.Columns.Add("Answer", Type.GetType("System.String"))
            gdtCustSurvey.Columns.Add("UserVal", Type.GetType("System.UInt16"))
        End If

        'set the Grid Style
        If Me.grdSurvey.TableStyles.Count = 0 Then
            buildGridStyle()
        End If

    End Function
    ''' <summary>
    ''' LoadComboBox by businessid
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' Changes: use busiId
    ''' </summary>
    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As ComboBox, ByVal strCode As String, ByVal strName As String, ByVal busiId As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean
        Dim blnLoad As Boolean

        If dt Is Nothing Then

            Try
                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, busiId,
                                                       New Dictionary(Of String, String)() From {
                                                       })
                dt = retDs.Tables(0).Copy
                blnLoad = True
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                blnLoad = False
            End Try
        End If

        If blnLoad Then
            cbo.DataSource = dt
            cbo.DisplayMember = strName
            cbo.ValueMember = strCode
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Fill Survey combobox
    ''' Lubin 2022-11-17 ITSR-3487 Move the Macau query logic to the server side.
    ''' Changes: use busiId
    ''' </summary>
    Private Sub fillSurvey()
        'strSQL = "select distinct crmsur_survey_id, crmsur_survey_title from crm_survey"
        LoadComboBox(gdtSurvey, cboSurvey, "crmsur_survey_id", "crmsur_survey_title", "FRM_SURVEY")
    End Sub

    Private Sub buildGridStyle()
        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 400
        cs.MappingName = "Question"
        cs.HeaderText = "Question Text"
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "Answer"
        cs.HeaderText = "Answer"
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 50
        cs.MappingName = "UserVal"
        cs.HeaderText = "Value"
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CustSurvey"
        grdSurvey.TableStyles.Add(ts)

        grdSurvey.DataSource = gdtCustSurvey
        grdSurvey.AllowDrop = False
        grdSurvey.ReadOnly = True
    End Sub
    ''' <summary>
    ''' Display result 
    ''' Lubin 2022-11-17 ITSR-3487 Move the Macau query logic to the server side.
    ''' </summary>
    Private Sub cmdDisResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim strSQL As String
        'Dim sqlconnect As New SqlConnection
        'Dim sqlda As SqlDataAdapter
        Dim surveyID As String
        Dim dtUserTemp As New DataTable

        gdtCustSurvey.Clear()
        surveyID = Me.cboSurvey.SelectedValue.ToString

        'strSQL = "select * from crm_survey_response where crmsrp_survey_id = '" & surveyID & "' and crmsrp_customer_id = '" & strCustID & "' "

        Try
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "FRM_SURVEY_RESPONSE",
             New Dictionary(Of String, String)() From {
            {"surveyID", surveyID},
            {"strCustID", strCustID}
            })
            dtUserTemp = retDs.Tables(0).Copy
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        'if user has response to the corresponding survey
        If dtUserTemp.Rows.Count > 0 Then
            Dim ansField, strAns As String

            'select the questions that the user has response from the database
            'For i As Integer = 1 To 50
            '    If i < 10 Then
            '        ansField = "ans00" & i
            '    Else
            '        ansField = "ans0" & i
            '    End If
            '    If dtUserTemp.Select(ansField & " = '1' ").Length <> 0 Then
            '        strAns = strAns & "'" & ansField & "',"
            '    End If
            'Next
            'For i As Integer = 0 To dtUserTemp.Columns.Count - 1
            '    If UCase(dtUserTemp.Columns(i).ColumnName) Like "ANS*" Then
            '        If Not IsDBNull(dtUserTemp.Rows(0).Item(i)) AndAlso dtUserTemp.Rows(0).Item(i) >= 1 Then
            '            strAns = strAns & "'" & dtUserTemp.Columns(i).ColumnName & "',"
            '        End If
            '    End If
            'Next

            'If strAns.Length > 0 Then
            '    strAns = strAns.Substring(0, strAns.Length - 1)
            'End If

            'select the corresponding questions and answers
            'strSQL = "Select crmsuq_question_text as 'Question',  crmsua_answer_text as 'Answer', crmsqa_map_col " & _
            '         "From crm_survey_question_answer, crm_survey_question, crm_survey_answer " & _
            '         "Where crmsqa_question_id=crmsuq_question_id and crmsqa_answer_id=crmsua_answer_id and crmsqa_survey_id = '" & surveyID & "' " & _
            '         "And crmsqa_map_col in (" & strAns & ") " & _
            '         "Order by crmsqa_map_col"
            'strSQL = "Select crmsuq_question_text as 'Question',  crmsua_answer_text as 'Answer', crmsqa_map_col " & _
            '         "From crm_survey_question_answer, crm_survey_question, crm_survey_answer " & _
            '         "Where crmsqa_question_id=crmsuq_question_id and crmsqa_answer_id=crmsua_answer_id and crmsqa_survey_id = '" & surveyID & "' " & _
            '         "Order by crmsqa_map_col"

            Try
                Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "FRM_SURVEY_QUESTION_ANSWER",
                     New Dictionary(Of String, String)() From {
                    {"surveyID", surveyID}
                    })
                gdtCustSurvey = retDs.Tables(0).Copy
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            End Try

            'Fill the value
            If gdtCustSurvey.Rows.Count > 0 Then
                'gdtCustSurvey.Columns.Add("UserVal", Type.GetType("System.UInt16"))
                For i As Integer = 0 To gdtCustSurvey.Rows.Count - 1
                    With gdtCustSurvey.Rows(i)
                        If Not IsDBNull(dtUserTemp.Rows(0).Item(.Item("crmsqa_map_col"))) AndAlso
                                dtUserTemp.Rows(0).Item(.Item("crmsqa_map_col")) >= 1 Then
                            .Item("UserVal") = dtUserTemp.Rows(0).Item(.Item("crmsqa_map_col"))
                        Else
                            .Delete()
                        End If
                    End With
                Next
                gdtCustSurvey.AcceptChanges()
            End If
        End If

    End Sub
End Class
