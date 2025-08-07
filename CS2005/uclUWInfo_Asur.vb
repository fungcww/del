Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class uclUWInfo_Asur

    Inherits System.Windows.Forms.UserControl

    Private strPolicy As String
    Friend WithEvents txtUWS As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label

#Region " Windows Form Designer generated code "
    Public Sub New(init As Boolean)
        MyBase.New()
    End Sub
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
    Friend WithEvents grdUW As System.Windows.Forms.DataGrid
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtOutstand As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdUW = New System.Windows.Forms.DataGrid()
        Me.txtOutstand = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtUWS = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        CType(Me.grdUW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdUW
        '
        Me.grdUW.AlternatingBackColor = System.Drawing.Color.White
        Me.grdUW.BackColor = System.Drawing.Color.White
        Me.grdUW.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdUW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdUW.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUW.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdUW.CaptionVisible = False
        Me.grdUW.DataMember = ""
        Me.grdUW.FlatMode = True
        Me.grdUW.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdUW.ForeColor = System.Drawing.Color.Black
        Me.grdUW.GridLineColor = System.Drawing.Color.Wheat
        Me.grdUW.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdUW.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdUW.HeaderForeColor = System.Drawing.Color.Black
        Me.grdUW.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUW.Location = New System.Drawing.Point(4, 8)
        Me.grdUW.Name = "grdUW"
        Me.grdUW.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdUW.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdUW.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdUW.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdUW.Size = New System.Drawing.Size(684, 126)
        Me.grdUW.TabIndex = 24
        '
        'txtOutstand
        '
        Me.txtOutstand.AcceptsReturn = True
        Me.txtOutstand.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutstand.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOutstand.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOutstand.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOutstand.Location = New System.Drawing.Point(84, 140)
        Me.txtOutstand.MaxLength = 0
        Me.txtOutstand.Name = "txtOutstand"
        Me.txtOutstand.ReadOnly = True
        Me.txtOutstand.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutstand.Size = New System.Drawing.Size(316, 26)
        Me.txtOutstand.TabIndex = 28
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(8, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(99, 20)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Outstanding"
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStatus.Location = New System.Drawing.Point(456, 140)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(81, 26)
        Me.txtStatus.TabIndex = 30
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(412, 144)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(57, 20)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Status"
        '
        'txtRemark
        '
        Me.txtRemark.AcceptsReturn = True
        Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemark.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRemark.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemark.Location = New System.Drawing.Point(84, 164)
        Me.txtRemark.MaxLength = 0
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = True
        Me.txtRemark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRemark.Size = New System.Drawing.Size(604, 26)
        Me.txtRemark.TabIndex = 32
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 168)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(67, 20)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "Remark"
        '
        'txtUWS
        '
        Me.txtUWS.AcceptsReturn = True
        Me.txtUWS.BackColor = System.Drawing.SystemColors.Window
        Me.txtUWS.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUWS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtUWS.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUWS.Location = New System.Drawing.Point(84, 190)
        Me.txtUWS.MaxLength = 0
        Me.txtUWS.Multiline = True
        Me.txtUWS.Name = "txtUWS"
        Me.txtUWS.ReadOnly = True
        Me.txtUWS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUWS.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtUWS.Size = New System.Drawing.Size(604, 316)
        Me.txtUWS.TabIndex = 33
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(8, 193)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(70, 121)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Underwriting Worksheet"
        '
        'uclUWInfo_Asur
        '
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtUWS)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtOutstand)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.grdUW)
        Me.Name = "uclUWInfo_Asur"
        Me.Size = New System.Drawing.Size(720, 519)
        CType(Me.grdUW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public WriteOnly Property srcDTUWInf()
        Set(ByVal Value)
            If Not Value Is Nothing Then
                strPolicy = Value
                'ds.Tables.Add(Value)
                Call buildUI()
            End If
        End Set
    End Property

    ' **** ES009 begin ****
    Private Sub buildUI()

        Try
            'uw-comments/retrieve
            'oliver added 2024-6-27 for Assurance Production Version hot fix
            Dim api_Url = CRS_Component.EnvironmentUtility.getEnvironmentSetting(gLoginEnvStr, "NBAPIEndPoint")
            'Dim api_Url As String = String.Format(tempAPIUrl, "hk", "uw-comments/retrieve")

            Dim query = New CRS_Component.NBAPIQuery("", New Dictionary(Of String, String) From {{"policyNumber", strPolicy}}, "hk", String.Concat("CRS-", gsUser))

            Dim sJSON As String = JsonConvert.SerializeObject(query)
            Dim responseObject As JObject = CRS_Component.APICallHelper.CallAPIWithResponse(Of JObject)(api_Url, query)

            Dim resultList As JToken = responseObject.GetValue("result")
            Dim displayString As String = String.Empty

            For Each arr As JToken In resultList
                Dim item = JsonConvert.DeserializeObject(Of CRS_Component.NBResult)(arr.ToString(Formatting.None))
                Dim uwDate As DateTime = Convert.ToDateTime(item.strDate)

                displayString &= item.remarkUser & ": " & uwDate.ToString("MM/dd/yyyy hh:mm:ss tt") & Environment.NewLine
                displayString &= item.description & Environment.NewLine
                displayString &= "=============================================================" & Environment.NewLine
            Next

            txtUWS.Text = displayString
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

    End Sub

End Class
