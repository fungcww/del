Imports System.Data.SqlClient

Public Class uclMPF
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCurrency As System.Windows.Forms.TextBox
    Friend WithEvents txtSCD As System.Windows.Forms.TextBox
    Friend WithEvents txtEmpName As System.Windows.Forms.TextBox
    Friend WithEvents txtAccNo As System.Windows.Forms.TextBox
    Friend WithEvents txtAgtName As System.Windows.Forms.TextBox
    Friend WithEvents txtAgtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDOM As System.Windows.Forms.TextBox
    Friend WithEvents txtDOE As System.Windows.Forms.TextBox
    Friend WithEvents txtMemNo As System.Windows.Forms.TextBox
    Friend WithEvents grdPension As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtCurrency = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtSCD = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtEmpName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtAccNo = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtAgtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtAgtCode = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDOM = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDOE = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtMemNo = New System.Windows.Forms.TextBox
        Me.grdPension = New System.Windows.Forms.DataGrid
        CType(Me.grdPension, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(312, 196)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 16)
        Me.Label12.TabIndex = 116
        Me.Label12.Text = "Currency"
        '
        'txtCurrency
        '
        Me.txtCurrency.Location = New System.Drawing.Point(372, 192)
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.Size = New System.Drawing.Size(92, 20)
        Me.txtCurrency.TabIndex = 115
        Me.txtCurrency.Text = "HKD"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 196)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(162, 16)
        Me.Label10.TabIndex = 112
        Me.Label10.Text = "Scheme Commencement Date:"
        '
        'txtSCD
        '
        Me.txtSCD.Location = New System.Drawing.Point(180, 192)
        Me.txtSCD.Name = "txtSCD"
        Me.txtSCD.Size = New System.Drawing.Size(108, 20)
        Me.txtSCD.TabIndex = 111
        Me.txtSCD.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(248, 168)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 16)
        Me.Label9.TabIndex = 110
        Me.Label9.Text = "Employer Name:"
        '
        'txtEmpName
        '
        Me.txtEmpName.Location = New System.Drawing.Point(344, 164)
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.Size = New System.Drawing.Size(324, 20)
        Me.txtEmpName.TabIndex = 109
        Me.txtEmpName.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 168)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 16)
        Me.Label7.TabIndex = 106
        Me.Label7.Text = "Account No.:"
        '
        'txtAccNo
        '
        Me.txtAccNo.Location = New System.Drawing.Point(88, 164)
        Me.txtAccNo.Name = "txtAccNo"
        Me.txtAccNo.Size = New System.Drawing.Size(140, 20)
        Me.txtAccNo.TabIndex = 105
        Me.txtAccNo.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(216, 224)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 16)
        Me.Label6.TabIndex = 104
        Me.Label6.Text = "Agent Name:"
        '
        'txtAgtName
        '
        Me.txtAgtName.Location = New System.Drawing.Point(296, 220)
        Me.txtAgtName.Name = "txtAgtName"
        Me.txtAgtName.Size = New System.Drawing.Size(372, 20)
        Me.txtAgtName.TabIndex = 103
        Me.txtAgtName.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 224)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 16)
        Me.Label1.TabIndex = 102
        Me.Label1.Text = "Agent Code:"
        '
        'txtAgtCode
        '
        Me.txtAgtCode.Location = New System.Drawing.Point(88, 220)
        Me.txtAgtCode.Name = "txtAgtCode"
        Me.txtAgtCode.Size = New System.Drawing.Size(108, 20)
        Me.txtAgtCode.TabIndex = 101
        Me.txtAgtCode.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(448, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 16)
        Me.Label3.TabIndex = 100
        Me.Label3.Text = "Date of Membership:"
        '
        'txtDOM
        '
        Me.txtDOM.Location = New System.Drawing.Point(568, 136)
        Me.txtDOM.Name = "txtDOM"
        Me.txtDOM.TabIndex = 99
        Me.txtDOM.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(208, 140)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 16)
        Me.Label2.TabIndex = 98
        Me.Label2.Text = "Date of Employment:"
        '
        'txtDOE
        '
        Me.txtDOE.Location = New System.Drawing.Point(328, 136)
        Me.txtDOE.Name = "txtDOE"
        Me.txtDOE.Size = New System.Drawing.Size(108, 20)
        Me.txtDOE.TabIndex = 97
        Me.txtDOE.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 16)
        Me.Label5.TabIndex = 96
        Me.Label5.Text = "Member No.:"
        '
        'txtMemNo
        '
        Me.txtMemNo.Location = New System.Drawing.Point(88, 136)
        Me.txtMemNo.Name = "txtMemNo"
        Me.txtMemNo.Size = New System.Drawing.Size(108, 20)
        Me.txtMemNo.TabIndex = 95
        Me.txtMemNo.Text = ""
        '
        'grdPension
        '
        Me.grdPension.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPension.BackColor = System.Drawing.Color.White
        Me.grdPension.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPension.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPension.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPension.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPension.CaptionVisible = False
        Me.grdPension.DataMember = ""
        Me.grdPension.FlatMode = True
        Me.grdPension.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPension.ForeColor = System.Drawing.Color.Black
        Me.grdPension.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPension.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPension.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPension.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPension.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPension.Location = New System.Drawing.Point(8, 8)
        Me.grdPension.Name = "grdPension"
        Me.grdPension.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPension.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPension.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPension.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPension.Size = New System.Drawing.Size(664, 120)
        Me.grdPension.TabIndex = 117
        '
        'uclMPF
        '
        Me.Controls.Add(Me.grdPension)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtCurrency)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtSCD)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtEmpName)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtAccNo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtAgtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAgtCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDOM)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDOE)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtMemNo)
        Me.Name = "uclMPF"
        Me.Size = New System.Drawing.Size(680, 256)
        CType(Me.grdPension, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private ds As DataSet = New DataSet("MPF")
    Private gdtMPF As DataTable
    Private strCustID, strClientID As String
    Dim WithEvents bm As BindingManagerBase

    Public Property CustID(ByVal strIn As String) As String
        Get
            Return strCustID
        End Get
        Set(ByVal Value As String)
            If Not Value Is Nothing Then
                If Value.Length > 0 Then
                    strCustID = strIn
                    strClientID = Value
                    Call buildUI()
                End If
            End If
        End Set
    End Property

    Public Function resetDS()
        If Not Me.gdtMPF Is Nothing Then Me.gdtMPF.Clear()
    End Function

    Public Sub buildUI()
        If gdtMPF Is Nothing Then
            gdtMPF = New DataTable("MPF")
            gdtMPF.Columns.Add("type", Type.GetType("System.String"))
            gdtMPF.Columns.Add("member_number", Type.GetType("System.String"))
            gdtMPF.Columns.Add("company_name", Type.GetType("System.String"))
        End If

        'set the Grid Style
        If Me.grdPension.TableStyles.Count = 0 Then
            buildGridStyle()
        End If

        fillDT()
    End Sub

    Private Sub buildGridStyle()
        Dim ts As New clsDataGridTableStyle(True)
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = "type"
        cs.HeaderText = "Type"
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 200
        cs.MappingName = "member_number"
        cs.HeaderText = "MemberNo"
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 250
        cs.MappingName = "company_name"
        cs.HeaderText = "EmployerName"
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "MPF"
        grdPension.TableStyles.Add(ts)

        grdPension.DataSource = gdtMPF
        grdPension.AllowDrop = False
        grdPension.ReadOnly = True
    End Sub

    Private Sub fillDT()
        Dim sqlconnect As New SqlConnection
        Dim strMPF, strSEP, strMORSO, strORSO As String
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter

        Try
            strMPF = "select 'MPF' as type, mpfmem_employer_code as employer_code, mpfmem_fund_code+'-'+mpfmem_employer_code+'-'+convert(varchar,mpfmem_member_number) as member_number,mpfmem_date_join_company as doe, " & _
                     "mpfmem_date_join_scheme as dom, mpfmem_fund_code+mpfmem_employer_code+'-'+mpfmem_reporting_centre as acc_no, " & _
                     "mpfemp_company_name as company_name, mpfemp_sch_comm_date as sch_comm_date, mpfmem_agent_code as agent_code, mpfmem_agent_name as agent_name " & _
                     "from mpf_members, mpf_employers " & _
                     "where mpfmem_hkid = '" & strClientID & "' and mpfemp_emp_class = 'M' and left(mpfmem_employer_code,1) = 2 " & _
                     "and mpfmem_fund_code = mpfemp_fund_code and mpfmem_employer_code = mpfemp_employer_code and " & _
                     "mpfmem_reporting_centre = mpfemp_reporting_centre "

            strSEP = "select 'SEP' as type, mpfmem_employer_code as employer_code, mpfmem_fund_code+'-'+mpfmem_employer_code+'-'+convert(varchar,mpfmem_member_number) as member_number,mpfmem_date_join_company as doe, " & _
                     "mpfmem_date_join_scheme as dom, mpfmem_fund_code+mpfmem_employer_code+'-'+mpfmem_reporting_centre as acc_no, " & _
                     "mpfemp_company_name as company_name, mpfemp_sch_comm_date as sch_comm_date, mpfmem_agent_code as agent_code, mpfmem_agent_name as agent_name " & _
                     "from mpf_members, mpf_employers " & _
                     "where mpfmem_hkid = '" & strClientID & "' and mpfemp_emp_class = 'M' and left(mpfmem_employer_code,1) = 3 " & _
                     "and mpfmem_fund_code = mpfemp_fund_code and mpfmem_employer_code = mpfemp_employer_code and " & _
                     "mpfmem_reporting_centre = mpfemp_reporting_centre "

            strMORSO = "select 'ORSO' as type, mpfmem_employer_code as employer_code, mpfmem_fund_code+'-'+mpfmem_employer_code+'-'+convert(varchar,mpfmem_member_number) as member_number,mpfmem_date_join_company as doe, " & _
                       "mpfmem_date_join_scheme as dom, mpfmem_fund_code+mpfmem_employer_code+'-'+mpfmem_reporting_centre as acc_no, " & _
                       "mpfemp_company_name as company_name, mpfemp_sch_comm_date as sch_comm_date, mpfmem_agent_code as agent_code, mpfmem_agent_name as agent_name " & _
                       "from mpf_members, mpf_employers " & _
                       "where mpfmem_hkid = '" & strClientID & "' and mpfemp_emp_class = 'O' " & _
                       "and mpfmem_fund_code = mpfemp_fund_code and mpfmem_employer_code = mpfemp_employer_code and " & _
                       "mpfmem_reporting_centre = mpfemp_reporting_centre "

            strORSO = "select 'ORSO' as type, mpfmal_employer_code as employer_code, mpfmal_fund_code+'-'+mpfmal_employer_code+'-'+convert(varchar,mpfmal_member_number) as member_number,mpfmal_date_join_company as doe, " & _
                      "mpfmal_date_join_scheme as dom, mpfmal_fund_code+mpfmal_employer_code+'-'+mpfmal_reporting_centre as acc_no, " & _
                      "mpfeal_company_name as company_name, mpfeal_sch_comm_date as sch_comm_date, mpfmal_agent_code as agent_code, mpfmal_agent_name as agent_name " & _
                      "from mpf_members_alhk, mpf_employers_alhk " & _
                      "where mpfmal_hkid = '" & strClientID & "' and mpfmal_fund_code = mpfeal_fund_code and mpfmal_employer_code = mpfeal_employer_code and " & _
                      "mpfmal_reporting_centre = mpfeal_reporting_centre "

            strSQL = strMPF & " union " & strSEP & " union " & strMORSO & _
                     " union " & _
                     strORSO & _
                     "order by type "

            sqlconnect.ConnectionString = strMPFConn

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(gdtMPF)

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        bm = Me.BindingContext(gdtMPF)

        UpdatePT()
    End Sub

    Private Sub UpdatePT()
        If bm.Position <> -1 Then
            Dim drI As DataRow = CType(bm.Current, DataRowView).Row()
            If Not drI Is Nothing Then
                Me.txtMemNo.Text = Trim(drI.Item("member_number"))

                Dim dtDOE As DateTime
                dtDOE = drI.Item("doe")
                If Not IsDBNull(dtDOE) Then
                    Me.txtDOE.Text = Format(dtDOE, gDateFormat)
                Else
                    Me.txtDOE.Text = "Nil"
                End If

                Dim dtDOM As DateTime
                dtDOM = drI.Item("dom")
                If Not IsDBNull(dtDOM) Then
                    Me.txtDOM.Text = Format(dtDOM, gDateFormat)
                Else
                    Me.txtDOM.Text = "Nil"
                End If

                ' 20250205, HNW Expansion - Obsolete ExternalUser related logic
                ''CRS 7x24 Changes - Start
                'If ExternalUser Then
                '    Me.txtAccNo.Text = MaskExternalUserData(MaskData.BANK_ACCOUNT_NO, Trim(drI.Item("acc_no")))
                'Else
                '    Me.txtAccNo.Text = Trim(drI.Item("acc_no"))
                'End If
                ''CRS 7x24 Changes - End
                Me.txtAccNo.Text = Trim(drI.Item("acc_no"))

                Me.txtEmpName.Text = Trim(drI.Item("company_name"))

                Dim dtSCD As DateTime
                dtSCD = drI.Item("sch_comm_date")
                If Not IsDBNull(dtSCD) Then
                    Me.txtSCD.Text = Format(dtSCD, gDateFormat)
                Else
                    Me.txtSCD.Text = "Nil"
                End If

                Me.txtAgtCode.Text = Trim(drI.Item("agent_code"))
                Me.txtAgtName.Text = Trim(drI.Item("agent_name"))
            End If
        Else

        End If
    End Sub

    Private Sub grdPension_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPension.CurrentCellChanged
        UpdatePT()
    End Sub

End Class
