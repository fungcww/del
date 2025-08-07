'obsolete function
'Alex TH Lee 20240507
Imports System.Data.SqlClient
Imports CRS_Component

Public Class frmReprintCard
    Inherits System.Windows.Forms.Form

    Dim sqlconnect As New SqlConnection
    Dim sqlCmd As SqlCommand
    Friend WithEvents cmdUnlock As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents grdHist As System.Windows.Forms.DataGrid
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Dim reader As SqlDataReader

    Private configEndPoint_Url As String = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "ConfigEndPoint")

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCustID As System.Windows.Forms.TextBox
    Friend WithEvents cboPrintCard As System.Windows.Forms.ComboBox
    Friend WithEvents cboPrintLetter As System.Windows.Forms.ComboBox
    Friend WithEvents txtLastUp As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPrtCountL As System.Windows.Forms.TextBox
    Friend WithEvents txtPrtCountC As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstPrt As System.Windows.Forms.TextBox
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtCustID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdUnlock = New System.Windows.Forms.Button
        Me.cmdReset = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboPrintCard = New System.Windows.Forms.ComboBox
        Me.cboPrintLetter = New System.Windows.Forms.ComboBox
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.txtLastUp = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPrtCountL = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPrtCountC = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtFirstPrt = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.grdHist = New System.Windows.Forms.DataGrid
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCustID
        '
        Me.txtCustID.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustID.Location = New System.Drawing.Point(80, 8)
        Me.txtCustID.MaxLength = 9
        Me.txtCustID.Name = "txtCustID"
        Me.txtCustID.Size = New System.Drawing.Size(72, 20)
        Me.txtCustID.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "CustomerID"
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(164, 8)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(56, 20)
        Me.cmdSearch.TabIndex = 19
        Me.cmdSearch.Text = "Search"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdUnlock)
        Me.GroupBox1.Controls.Add(Me.cmdReset)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cboPrintCard)
        Me.GroupBox1.Controls.Add(Me.cboPrintLetter)
        Me.GroupBox1.Controls.Add(Me.cmdPrint)
        Me.GroupBox1.Controls.Add(Me.txtLastUp)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPrtCountL)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPrtCountC)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtFirstPrt)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(221, 256)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        '
        'cmdUnlock
        '
        Me.cmdUnlock.Location = New System.Drawing.Point(109, 224)
        Me.cmdUnlock.Name = "cmdUnlock"
        Me.cmdUnlock.Size = New System.Drawing.Size(75, 23)
        Me.cmdUnlock.TabIndex = 53
        Me.cmdUnlock.Text = "Unlock"
        Me.cmdUnlock.UseVisualStyleBackColor = True
        '
        'cmdReset
        '
        Me.cmdReset.Enabled = False
        Me.cmdReset.Location = New System.Drawing.Point(-53, 224)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(75, 23)
        Me.cmdReset.TabIndex = 52
        Me.cmdReset.Text = "Reset"
        Me.cmdReset.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 192)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Print Letter?"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Print Card?"
        '
        'cboPrintCard
        '
        Me.cboPrintCard.BackColor = System.Drawing.SystemColors.Window
        Me.cboPrintCard.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPrintCard.Items.AddRange(New Object() {"YES", "NO"})
        Me.cboPrintCard.Location = New System.Drawing.Point(108, 156)
        Me.cboPrintCard.MaxDropDownItems = 2
        Me.cboPrintCard.Name = "cboPrintCard"
        Me.cboPrintCard.Size = New System.Drawing.Size(92, 21)
        Me.cboPrintCard.TabIndex = 49
        '
        'cboPrintLetter
        '
        Me.cboPrintLetter.BackColor = System.Drawing.SystemColors.Window
        Me.cboPrintLetter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPrintLetter.Items.AddRange(New Object() {"YES", "NO"})
        Me.cboPrintLetter.Location = New System.Drawing.Point(108, 188)
        Me.cboPrintLetter.MaxDropDownItems = 2
        Me.cboPrintLetter.Name = "cboPrintLetter"
        Me.cboPrintLetter.Size = New System.Drawing.Size(92, 21)
        Me.cboPrintLetter.TabIndex = 48
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(28, 224)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(75, 23)
        Me.cmdPrint.TabIndex = 46
        Me.cmdPrint.Text = "Print"
        '
        'txtLastUp
        '
        Me.txtLastUp.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastUp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLastUp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastUp.Location = New System.Drawing.Point(108, 44)
        Me.txtLastUp.MaxLength = 5
        Me.txtLastUp.Name = "txtLastUp"
        Me.txtLastUp.Size = New System.Drawing.Size(92, 20)
        Me.txtLastUp.TabIndex = 44
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 132)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Status"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStatus.Location = New System.Drawing.Point(108, 128)
        Me.txtStatus.MaxLength = 5
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(92, 20)
        Me.txtStatus.TabIndex = 36
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Letter Print Count"
        '
        'txtPrtCountL
        '
        Me.txtPrtCountL.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrtCountL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrtCountL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrtCountL.Location = New System.Drawing.Point(108, 100)
        Me.txtPrtCountL.MaxLength = 5
        Me.txtPrtCountL.Name = "txtPrtCountL"
        Me.txtPrtCountL.Size = New System.Drawing.Size(92, 20)
        Me.txtPrtCountL.TabIndex = 34
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Card Print Count"
        '
        'txtPrtCountC
        '
        Me.txtPrtCountC.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrtCountC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrtCountC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrtCountC.Location = New System.Drawing.Point(108, 72)
        Me.txtPrtCountC.MaxLength = 5
        Me.txtPrtCountC.Name = "txtPrtCountC"
        Me.txtPrtCountC.Size = New System.Drawing.Size(92, 20)
        Me.txtPrtCountC.TabIndex = 32
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Last Update Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "First Print Date"
        '
        'txtFirstPrt
        '
        Me.txtFirstPrt.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstPrt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFirstPrt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFirstPrt.Location = New System.Drawing.Point(108, 16)
        Me.txtFirstPrt.MaxLength = 5
        Me.txtFirstPrt.Name = "txtFirstPrt"
        Me.txtFirstPrt.Size = New System.Drawing.Size(92, 20)
        Me.txtFirstPrt.TabIndex = 28
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(350, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 13)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "P - First Print, R - Reprint"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.Location = New System.Drawing.Point(304, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 13)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "NOTE:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(477, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(204, 13)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Y - Success, N - Fail, A - Address Problem"
        '
        'grdHist
        '
        Me.grdHist.AlternatingBackColor = System.Drawing.Color.White
        Me.grdHist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdHist.BackColor = System.Drawing.Color.White
        Me.grdHist.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdHist.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdHist.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdHist.CaptionVisible = False
        Me.grdHist.DataMember = ""
        Me.grdHist.FlatMode = True
        Me.grdHist.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdHist.ForeColor = System.Drawing.Color.Black
        Me.grdHist.GridLineColor = System.Drawing.Color.Wheat
        Me.grdHist.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdHist.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdHist.HeaderForeColor = System.Drawing.Color.Black
        Me.grdHist.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdHist.Location = New System.Drawing.Point(235, 40)
        Me.grdHist.Name = "grdHist"
        Me.grdHist.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdHist.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdHist.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdHist.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdHist.Size = New System.Drawing.Size(446, 205)
        Me.grdHist.TabIndex = 32
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(235, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 13)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Print History"
        '
        'frmReprintCard
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(693, 297)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.grdHist)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCustID)
        Me.Name = "frmReprintCard"
        Me.Text = "Reprint Customer Card/PIN"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property Editable() As Boolean
        Set(ByVal Value As Boolean)
            Me.txtFirstPrt.ReadOnly = Not Value
            Me.txtLastUp.ReadOnly = Not Value
            Me.txtPrtCountC.ReadOnly = Not Value
            Me.txtPrtCountL.ReadOnly = Not Value
        End Set
    End Property

    Private Sub frmReprintCard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Editable = False
        Me.txtCustID.Focus()
        Me.cmdPrint.Enabled = False
        wndMain.StatusBarPanel1.Text = ""
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim strSQL As String

        If Not IsNumeric(Me.txtCustID.Text) Then
            MsgBox("Invalid CustomerID.", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, gSystem)
            txtCustID.Focus()
            Exit Sub
        End If

        If Not Me.txtCustID.Text = "" Then

            ' Make sure is PH
            Dim dtPH As New DataTable
            Dim strErr As String

            strSQL = "Select 'A' from csw_poli_rel where policyrelatecode = 'PH' and customerid = '" & Me.txtCustID.Text & "'"
            If GetDT(strSQL, strCIWConn, dtPH, strErr) Then
                If dtPH.Rows.Count = 0 Then
                    MsgBox("Customer is not an existing PH, please check again.")
                    Exit Sub
                End If
            Else
                MsgBox(strErr)
                Exit Sub
            End If

            Me.txtStatus.Text = GetCustStatus(txtCustID.Text)

            sqlconnect.ConnectionString = strCIWConn

            If txtStatus.Text = "" Then
                If MsgBox("No such customer PIN record found, do you want to create it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gSystem) = MsgBoxResult.Yes Then

                    strSQL = "select NameSuffix, FirstName from customer where customerid = " & Me.txtCustID.Text

                    Try
                        sqlconnect.Open()
                        sqlCmd = New SqlCommand(strSQL, sqlconnect)
                        reader = sqlCmd.ExecuteReader

                        If reader.HasRows Then

                            Dim CTR As String
                            Dim strPass, strResult As String
                            Static RandomNumGen As New System.Random

                            strPass = "L" + Mid(txtCustID.Text * RandomNumGen.Next(3, 9), 2, 3) + "bB"

                            'AC - Change to use configuration setting - start
                            '#If UAT = 0 Then
                            '                            CTR = "PRD_CTR_LIFECTR"
                            '#Else
                            '                            CTR = "UAT_CTR_LIFEAGT"
                            '#End If
                            If gUAT = False Then
                                CTR = "PRD_CTR_LIFECTR"
                            Else
                                CTR = "UAT_CTR_LIFEAGT"
                            End If
                            'AC - Change to use configuration setting - end


                            reader.Read()
                            strResult = InsUserPro(Trim(txtCustID.Text), strPass, Trim(reader.Item("namesuffix")) & " " & Trim(reader.Item("firstname")))
                            If strResult <> "" Then
                                MsgBox(strResult, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                            Else
                                strResult = InsUserGrp(Trim(txtCustID.Text), Trim(CTR))
                                If strResult <> "" Then
                                    MsgBox(strResult, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                                End If
                            End If

                            reader.Close()

                            MsgBox("Record created, please load the record again.", MsgBoxStyle.OkOnly, gSystem)

                        Else
                            MsgBox("Customer record not found.", MsgBoxStyle.OkOnly, gSystem)
                        End If
                            Debug.Print("abc")
                            Me.txtStatus.Text = GetCustStatus(txtCustID.Text)

                    Catch sqlex As SqlException
                        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                    Catch ex As Exception
                        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                    Finally
                        sqlconnect.Close()
                        sqlCmd.Dispose()
                    End Try
                Else
                    cleardata()
                    Exit Sub
                End If
            End If

            strSQL = "select * from csw_print_control where cswprc_cust_id = " & Me.txtCustID.Text

            Try
                sqlconnect.Open()
                sqlCmd = New SqlCommand(strSQL, sqlconnect)
                reader = sqlCmd.ExecuteReader

                cleardata()

                If txtStatus.Text = "LOCKED" Then
                    cmdUnlock.Enabled = True
                End If

                If reader.HasRows Then

                    reader.Read()
                    With reader
                        If Not IsDBNull(.Item("cswprc_createdate")) Then
                            Me.txtFirstPrt.Text = Format(.Item("cswprc_createdate"), gDateFormat)
                        Else
                            Me.txtFirstPrt.Text = ""
                        End If
                        If Not IsDBNull(.Item("cswprc_lstprtdate")) Then
                            Me.txtLastUp.Text = Format(.Item("cswprc_lstprtdate"), gDateFormat)
                        Else
                            Me.txtLastUp.Text = ""
                        End If

                        Me.txtPrtCountC.Text = .Item("cswprc_printcount_card")
                        Me.txtPrtCountL.Text = .Item("cswprc_printcount_letter")

                        If .GetString(1) = "Y" Then
                            Me.cboPrintCard.Text = "YES"
                        Else
                            Me.cboPrintCard.Text = "NO"
                        End If

                        If .GetString(2) = "Y" Then
                            Me.cboPrintLetter.Text = "YES"
                        Else
                            Me.cboPrintLetter.Text = "NO"
                        End If

                        If Me.cboPrintCard.Text = "NO" Or Me.cboPrintLetter.Text = "NO" Then
                            Me.cmdPrint.Enabled = True
                        End If

                        cmdReset.Enabled = True

                        Call FillHistory()

                    End With

                Else
                    Call InsPrintControl(Trim(txtCustID.Text))
                    MsgBox("Please load the record again.", MsgBoxStyle.OkOnly, gSystem)
                    'MsgBox("No such customer.", MsgBoxStyle.OkOnly, gSystem)
                    Me.txtCustID.Text = ""
                    Me.txtCustID.Focus()
                End If
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Finally
                reader.Close()
                sqlconnect.Close()
                sqlCmd.Dispose()
            End Try
        Else
            MsgBox("Customer ID cannot be empty.", MsgBoxStyle.OkOnly, gSystem)
            cleardata()
            Me.txtCustID.Text = ""
            Me.txtCustID.Focus()
        End If
    End Sub

    Private Sub InsPrintControl(ByVal strCustID As String)

        Dim strUpdate As String
        Dim conn As New SqlConnection

        strUpdate = "insert into csw_print_control select '" & strCustID & "', 'N', 'N', getdate(), getdate(), 1, 1"

        conn.ConnectionString = strCIWConn
        sqlCmd = New SqlCommand(strUpdate, conn)

        Try
            conn.Open()
            sqlCmd.ExecuteNonQuery()
        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Finally
            conn.Close()
            sqlCmd.Dispose()
        End Try

    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Dim strUpdate As String
        Dim strUpdRpt As String
        Dim strEnq As String

        If Me.txtStatus.Text = "" Then
            MsgBox("Status is empty!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        sqlconnect.ConnectionString = strCIWConn

        'Update the csw_print_control table
        If Me.cboPrintCard.Text = "YES" OrElse Me.cboPrintLetter.Text = "YES" Then
            strUpdate = "update csw_print_control set "
            strUpdate &= "cswprc_card_ind = '" & IIf(Me.cboPrintCard.Text = "YES", "Y", "N") & "',"
            strUpdate &= "cswprc_letter_ind = '" & IIf(Me.cboPrintLetter.Text = "YES", "Y", "N") & "'"
            strUpdate &= " where cswprc_cust_id = " & Me.txtCustID.Text
        End If

        'Check if record exists in csw_print_cardletter_report
        strEnq = "select cswpcr_prtedletter ,cswpcr_prtedcard from csw_print_cardletter_report where cswpcr_cid = " & Me.txtCustID.Text & _
                 " and (cswpcr_prtidxletter = 'R' or cswpcr_prtidxcard = 'R')" & _
                 " and (cswpcr_prtedletter = 'N' or cswpcr_prtedcard = 'N')"

        Try
            sqlconnect.Open()
            sqlCmd = New SqlCommand(strEnq, sqlconnect)
            reader = sqlCmd.ExecuteReader

            If Not reader.HasRows Then
                'No match found                

                If Me.cboPrintLetter.Text = "YES" And Me.cboPrintCard.Text = "YES" Then
                    strUpdRpt &= "cswpcr_prtedletter,cswpcr_prtedcard,cswpcr_user_letter, cswpcr_user_card"
                    strUpdRpt &= ") values(" & Me.txtCustID.Text & ",getdate(),'" & IIf(Me.cboPrintLetter.Text = "YES", "R", "N") & "','" & IIf(Me.cboPrintCard.Text = "YES", "R", "N") & "','"
                    strUpdRpt &= "N','N','" & gsUser & "','" & System.Environment.UserName
                Else
                    If Me.cboPrintCard.Text = "YES" Then
                        strUpdRpt &= "cswpcr_prtedcard, cswpcr_user_card"
                        strUpdRpt &= ") values(" & Me.txtCustID.Text & ",getdate(),'" & IIf(Me.cboPrintLetter.Text = "YES", "R", "N") & "','" & IIf(Me.cboPrintCard.Text = "YES", "R", "N") & "','"
                        strUpdRpt &= "N','" & gsUser
                    End If
                    If Me.cboPrintLetter.Text = "YES" Then
                        strUpdRpt &= "cswpcr_prtedletter, cswpcr_user_letter"
                        strUpdRpt &= ") values(" & Me.txtCustID.Text & ",getdate(),'" & IIf(Me.cboPrintLetter.Text = "YES", "R", "N") & "','" & IIf(Me.cboPrintCard.Text = "YES", "R", "N") & "','"
                        strUpdRpt &= "N','" & gsUser
                    End If
                End If

                If strUpdRpt <> "" Then
                    strUpdRpt = "insert into csw_print_cardletter_report (cswpcr_cid,cswpcr_crtdate,cswpcr_prtidxletter,cswpcr_prtidxcard," & strUpdRpt
                    strUpdRpt &= "')"
                End If
            Else
                'match found
                reader.Read()
                If reader.Item("cswpcr_prtedletter") = "N" Then
                    If Me.cboPrintCard.Text = "YES" Then
                        strUpdRpt = "update csw_print_cardletter_report " & _
                                    "set cswpcr_prtedcard = 'N', cswpcr_prtidxcard = 'R', cswpcr_user_card ='" & gsUser & "' where " & _
                                    "cswpcr_prtidxletter = 'R' and " & _
                                    "cswpcr_cid = " & Me.txtCustID.Text & " and cswpcr_prtedletter = 'N'"
                    End If
                End If
                If reader.Item("cswpcr_prtedcard") = "N" Then
                    If Me.cboPrintLetter.Text = "YES" Then
                        strUpdRpt = "update csw_print_cardletter_report " & _
                                    "set cswpcr_prtedletter = 'N', cswpcr_prtidxletter = 'R', cswpcr_user_card ='" & gsUser & "' where " & _
                                    "cswpcr_prtidxcard = 'R' and " & _
                                    "cswpcr_cid = " & Me.txtCustID.Text & " and cswpcr_prtedcard = 'N'"
                    End If
                End If
            End If

        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            reader.Close()
        End Try

        Try
            'Update csw_print_control table            
            If strUpdate <> "" Then
                sqlCmd.CommandText = strUpdate
                sqlCmd.ExecuteNonQuery()
            End If

            'Update csw_print_cardletter_report table
            If strUpdRpt <> "" Then
                sqlCmd.CommandText = strUpdRpt
                sqlCmd.ExecuteNonQuery()
            End If

        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Finally
            sqlconnect.Close()
            sqlCmd.Dispose()
        End Try

        cleardata()
        Me.txtCustID.Text = ""
        Me.txtCustID.Focus()

    End Sub

    Private Sub cleardata()

        Me.txtFirstPrt.Text = ""
        Me.txtLastUp.Text = ""
        Me.txtPrtCountC.Text = ""
        Me.txtPrtCountL.Text = ""
        Me.cboPrintCard.Text = ""
        Me.cboPrintCard.SelectedIndex = -1
        Me.cboPrintLetter.Text = ""
        Me.cboPrintLetter.SelectedIndex = -1
        Me.cboPrintLetter.Enabled = True
        Me.cmdPrint.Enabled = False

    End Sub

    Private Sub cboPrintCard_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPrintCard.SelectedIndexChanged
        If Me.cboPrintCard.Text = "YES" Then
            Me.cboPrintLetter.Text = "YES"
            Me.cboPrintLetter.Enabled = False
        Else
            Me.cboPrintLetter.Enabled = True
        End If
    End Sub

    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click

        Dim strSQL As String

        If MsgBox("Are you sure to reset the card print flag for this customer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Reprint card") = MsgBoxResult.Yes Then
            sqlconnect.ConnectionString = strCIWConn
            sqlCmd = New SqlCommand

            strSQL = "Update csw_print_cardletter_report " & _
                " Set cswpcr_prtedcard = 'N' " & _
                " Where cswpcr_prtidxcard = 'P' and cswpcr_cid = " & Me.txtCustID.Text

            Try
                sqlconnect.Open()
                'Update csw_print_cardletter_report table
                sqlCmd.CommandText = strSQL
                sqlCmd.Connection = sqlconnect
                sqlCmd.ExecuteNonQuery()
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Finally
                sqlconnect.Close()
                sqlCmd.Dispose()
            End Try
        End If

    End Sub

    Public Function InsUserPro(ByVal CustID As String, ByVal Pin As String, ByVal Name As String) As String

        Dim strSql, strReturn As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim drCTR As SqlDataReader

        Try
            conn = New SqlConnection

            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            conn.ConnectionString = "SERVER=" & gSQL3 & ";UID=MTS_CTRSECCON;PWD=people2soft;DATABASE=SECURITY;Network=DBMSSOCN"
            '#Else
            '            conn.ConnectionString = "SERVER=HKSQLUAT3;UID=ctrmpf;PWD=ctrsaga;DATABASE=SECURITY;Network=DBMSSOCN"
            '#End If
            conn.ConnectionString = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "SecurityConnection").DecryptString()
            'If gUAT = False Then
            '    conn.ConnectionString = "SERVER=" & gSQL3 & ";UID=MTS_CTRSECCON;PWD=people2soft;DATABASE=SECURITY;Network=DBMSSOCN"
            'Else
            '    conn.ConnectionString = "SERVER=HKSQLUAT3;UID=ctrmpf;PWD=ctrsaga;DATABASE=SECURITY;Network=DBMSSOCN"
            'End If
            'AC - Change to use configuration setting - end


            conn.Open()

            strSql = "Declare @message varchar(50), @Name varchar(50), @Desc Varchar(90) " & _
                 " Execute Security.dbo.secsp_get_user_profile '" + CustID + "', @Name Out, " & _
                 " @Desc Out, @message Out Select @Name, @Desc , @message"
            cmd = New SqlCommand(strSql, conn)
            drCTR = cmd.ExecuteReader

            If drCTR.HasRows Then
                drCTR.Read()
                If IsDBNull(drCTR.Item(0)) Then
                    drCTR.Close()
                    strSql = "Declare @message varchar(50) Execute security.dbo.secsp_ins_user_profile '" & _
                        CustID + " ', '" + Pin + "', '" + Replace(Name, "'", "''") + "', '', @message out Select @message"

                    cmd.CommandText = strSql
                    InsUserPro = cmd.ExecuteScalar
                Else
                    drCTR.Close()
                    strSql = "SELECT ''"

                    cmd.CommandText = strSql
                    InsUserPro = cmd.ExecuteScalar
                End If
            Else
                drCTR.Close()
            End If

        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")

        Finally
            cmd.Dispose()
            conn.Close()
            conn.Dispose()
        End Try


    End Function

    Public Function InsUserGrp(ByVal CustID As String, ByVal Group As String) As String

        Dim strSql, strReturn As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim drCTR As SqlDataReader

        Try
            conn = New SqlConnection

            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            conn.ConnectionString = "SERVER=" & gSQL3 & ";UID=MTS_CTRSECCON;PWD=people2soft;DATABASE=SECURITY;Network=DBMSSOCN"
            '#Else
            '            conn.ConnectionString = "SERVER=HKSQLUAT3;UID=ctrmpf;PWD=ctrsaga;DATABASE=SECURITY;Network=DBMSSOCN"
            '#End If
            conn.ConnectionString = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "Security2Connection").DecryptString()
            'If gUAT = False Then
            '    conn.ConnectionString = "SERVER=" & gSQL3 & ";UID=MTS_CTRSECCON;PWD=people2soft;DATABASE=SECURITY;Network=DBMSSOCN"
            'Else
            '    conn.ConnectionString = "SERVER=HKSQLUAT3;UID=ctrmpf;PWD=ctrsaga;DATABASE=SECURITY;Network=DBMSSOCN"
            'End If
            'AC - Change to use configuration setting - end

            conn.Open()

            strSql = "Declare @message varchar(50) Execute security.dbo.secsp_chk_user_in_group '" _
                     + CustID + "', '" + Group + "', @message out Select @message"
            cmd = New SqlCommand(strSql, conn)
            drCTR = cmd.ExecuteReader

            If drCTR.HasRows Then
                drCTR.Read()
                If Trim(drCTR.Item(0)) = "" Then
                    drCTR.Close()
                    strSql = "SELECT ''"

                    cmd.CommandText = strSql
                    InsUserGrp = cmd.ExecuteScalar
                Else
                    drCTR.Close()
                    strSql = "Declare @message varchar(50) Execute security.dbo.secsp_ins_user_group '" + _
                              CustID + "', '" + Group + "', @message out Select @message"

                    cmd.CommandText = strSql
                    InsUserGrp = cmd.ExecuteScalar
                End If
            Else
                drCTR.Close()
            End If

        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")

        Finally
            cmd.Dispose()
            conn.Close()
            conn.Dispose()
        End Try

    End Function

    Public Function FillHistory()

        Dim dtTemp As New DataTable
        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim sqlconnect1 As New SqlConnection

        strSQL = "Select cswpcr_crtdate as Req_Date, cswpcr_prtidxletter as PIN, cswpcr_prtidxcard as Card, " & _
            " cswpcr_lstprtdateletter as PIN_Date, cswpcr_lstprtdatecard as Card_Date, " & _
            " cswpcr_prtedletter as PIN_Result, cswpcr_prtedcard as Card_Result, " & _
            " cswpcr_user_letter PIN_User, cswpcr_user_card as Card_User " & _
            " From csw_print_cardletter_report " & _
            " Where cswpcr_cid = '" & txtCustID.Text & "'" & _
            " Order by cswpcr_crtdate DESC"

        sqlconnect1.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect1)

        Try
            sqlda.Fill(dtTemp)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        sqlconnect.Close()
        sqlconnect.Dispose()
        sqlda.Dispose()
        grdHist.DataSource = dtTemp
        grdHist.ReadOnly = True

    End Function

    Private Sub cmdUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnlock.Click

        Dim strSql, strReturn As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand

        Try
            conn = New SqlConnection

            'AC - Change to use configuration setting - start
            '#If UAT = 0 Then
            '            conn.ConnectionString = "SERVER=" & gSQL3 & ";UID=MTS_CTRSECCON;PWD=people2soft;DATABASE=SECURITY;Network=DBMSSOCN"
            '#Else
            '            conn.ConnectionString = "SERVER=HKSQLUAT3;UID=ctrmpf;PWD=ctrsaga;DATABASE=SECURITY;Network=DBMSSOCN"
            '#End If
            conn.ConnectionString = CRS_Component.APICallHelper.GetConnectionConfig(configEndPoint_Url, "Security2Connection").DecryptString()
            'If gUAT = False Then
            '    conn.ConnectionString = "SERVER=" & gSQL3 & ";UID=MTS_CTRSECCON;PWD=people2soft;DATABASE=SECURITY;Network=DBMSSOCN"
            'Else
            '    conn.ConnectionString = "SERVER=HKSQLUAT3;UID=ctrmpf;PWD=ctrsaga;DATABASE=SECURITY;Network=DBMSSOCN"
            'End If
            'AC - Change to use configuration setting - end

            conn.Open()

            strSql = "Update sec_user_profile set secsup_lock_flag = 0 where secsup_user_id = '" & txtCustID.Text & "' "

            cmd = New SqlCommand(strSql, conn)
            strReturn = cmd.ExecuteNonQuery

        Catch sqlex As SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")

        Finally
            cmd.Dispose()
            conn.Close()
            conn.Dispose()
        End Try

        cleardata()

    End Sub

End Class
