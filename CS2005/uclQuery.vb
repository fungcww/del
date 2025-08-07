Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration

Public Class uclQuery
    Inherits System.Windows.Forms.UserControl

    Private Const gQueryCnt = 500
    Dim cboOperator As New ComboBox
    Dim cboLogicOp As New ComboBox
    Dim dtQuery, dtCategory, dtFieldList, dtCampaign, dtChannel, dtProd As DataTable
    Dim dtCriteriaList As New DataTable("CRITERIA")
    Dim dtOutput As New DataTable("OUTPUT")
    Dim dtOverlap As New DataTable("OVERLAP")
    Dim dtFinal As New DataTable("FINAL")
    Dim WithEvents bm As BindingManagerBase
    Dim bmResult As BindingManagerBase
    Dim strDataMode, strSource As String
    Dim blnLoading As Boolean = True
    Dim blnCancelQ, blnLocked As Boolean
    Dim strLog As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        cmdExportList.Visible = CheckUPSAccess("Export Client List")

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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents grdCriteria As System.Windows.Forms.DataGrid
    Friend WithEvents grdResult As System.Windows.Forms.DataGrid
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tvAllFields As System.Windows.Forms.TreeView
    Friend WithEvents cmdExportList As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveClient As System.Windows.Forms.Button
    Friend WithEvents txtOverlap As System.Windows.Forms.TextBox
    Friend WithEvents cmdImportList As System.Windows.Forms.Button
    Friend WithEvents cmdGenerate As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveCr As System.Windows.Forms.Button
    Friend WithEvents cmdAddCr As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveFld As System.Windows.Forms.Button
    Friend WithEvents cmdAddFld As System.Windows.Forms.Button
    Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radPol As System.Windows.Forms.RadioButton
    Friend WithEvents radCUST As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents radPI As System.Windows.Forms.RadioButton
    Friend WithEvents radPH As System.Windows.Forms.RadioButton
    Friend WithEvents chkDISTINCT As System.Windows.Forms.CheckBox
    Friend WithEvents cboCampaign As System.Windows.Forms.ComboBox
    Friend WithEvents cboChannel As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSaveResult As System.Windows.Forms.Button
    Friend WithEvents cmdSaveQry As System.Windows.Forms.Button
    Friend WithEvents cmdLoadQry As System.Windows.Forms.Button
    Friend WithEvents cmdDelResult As System.Windows.Forms.Button
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdOverlap As System.Windows.Forms.Button
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents lstProduct As System.Windows.Forms.ListBox
    Friend WithEvents txtPlanCode As System.Windows.Forms.TextBox
    Friend WithEvents cmdBuildList As System.Windows.Forms.Button
    Friend WithEvents txtViewSQL As System.Windows.Forms.TextBox
    Friend WithEvents cmdClearList As System.Windows.Forms.Button
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents txtCustom As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lblCount = New System.Windows.Forms.Label
        Me.cmdDelResult = New System.Windows.Forms.Button
        Me.cmdExportList = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdRemoveClient = New System.Windows.Forms.Button
        Me.cmdOverlap = New System.Windows.Forms.Button
        Me.txtOverlap = New System.Windows.Forms.TextBox
        Me.cmdImportList = New System.Windows.Forms.Button
        Me.cmdSaveResult = New System.Windows.Forms.Button
        Me.grdResult = New System.Windows.Forms.DataGrid
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.txtViewSQL = New System.Windows.Forms.TextBox
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.cmdClearList = New System.Windows.Forms.Button
        Me.cmdBuildList = New System.Windows.Forms.Button
        Me.txtPlanCode = New System.Windows.Forms.TextBox
        Me.lstProduct = New System.Windows.Forms.ListBox
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.txtCustom = New System.Windows.Forms.TextBox
        Me.tvAllFields = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.cmdClear = New System.Windows.Forms.Button
        Me.cmdLoadQry = New System.Windows.Forms.Button
        Me.cmdSaveQry = New System.Windows.Forms.Button
        Me.chkDISTINCT = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.radPI = New System.Windows.Forms.RadioButton
        Me.radPH = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.radCUST = New System.Windows.Forms.RadioButton
        Me.radPol = New System.Windows.Forms.RadioButton
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdGenerate = New System.Windows.Forms.Button
        Me.cmdRemoveCr = New System.Windows.Forms.Button
        Me.cmdAddCr = New System.Windows.Forms.Button
        Me.grdCriteria = New System.Windows.Forms.DataGrid
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.cmdDown = New System.Windows.Forms.Button
        Me.cmdUp = New System.Windows.Forms.Button
        Me.cmdRemoveFld = New System.Windows.Forms.Button
        Me.cmdAddFld = New System.Windows.Forms.Button
        Me.lstOutput = New System.Windows.Forms.ListBox
        Me.cboCampaign = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboChannel = New System.Windows.Forms.ComboBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(4, 204)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(716, 244)
        Me.TabControl1.TabIndex = 23
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblCount)
        Me.TabPage1.Controls.Add(Me.cmdDelResult)
        Me.TabPage1.Controls.Add(Me.cmdExportList)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.cmdRemoveClient)
        Me.TabPage1.Controls.Add(Me.cmdOverlap)
        Me.TabPage1.Controls.Add(Me.txtOverlap)
        Me.TabPage1.Controls.Add(Me.cmdImportList)
        Me.TabPage1.Controls.Add(Me.cmdSaveResult)
        Me.TabPage1.Controls.Add(Me.grdResult)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(708, 218)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Result"
        '
        'lblCount
        '
        Me.lblCount.ForeColor = System.Drawing.Color.Blue
        Me.lblCount.Location = New System.Drawing.Point(612, 180)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(88, 24)
        Me.lblCount.TabIndex = 42
        '
        'cmdDelResult
        '
        Me.cmdDelResult.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.cmdDelResult.Location = New System.Drawing.Point(608, 68)
        Me.cmdDelResult.Name = "cmdDelResult"
        Me.cmdDelResult.Size = New System.Drawing.Size(96, 23)
        Me.cmdDelResult.TabIndex = 41
        Me.cmdDelResult.Text = "Delete Result"
        '
        'cmdExportList
        '
        Me.cmdExportList.ForeColor = System.Drawing.Color.Brown
        Me.cmdExportList.Location = New System.Drawing.Point(608, 140)
        Me.cmdExportList.Name = "cmdExportList"
        Me.cmdExportList.Size = New System.Drawing.Size(96, 32)
        Me.cmdExportList.TabIndex = 40
        Me.cmdExportList.Text = "Export Client List"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 176)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 32)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Overlap Active Campaign"
        '
        'cmdRemoveClient
        '
        Me.cmdRemoveClient.Enabled = False
        Me.cmdRemoveClient.Location = New System.Drawing.Point(608, 4)
        Me.cmdRemoveClient.Name = "cmdRemoveClient"
        Me.cmdRemoveClient.Size = New System.Drawing.Size(96, 23)
        Me.cmdRemoveClient.TabIndex = 38
        Me.cmdRemoveClient.Text = "Exclude Overlap"
        '
        'cmdOverlap
        '
        Me.cmdOverlap.Location = New System.Drawing.Point(612, 208)
        Me.cmdOverlap.Name = "cmdOverlap"
        Me.cmdOverlap.Size = New System.Drawing.Size(92, 23)
        Me.cmdOverlap.TabIndex = 37
        Me.cmdOverlap.Text = "Check Overlap"
        Me.cmdOverlap.Visible = False
        '
        'txtOverlap
        '
        Me.txtOverlap.Location = New System.Drawing.Point(92, 172)
        Me.txtOverlap.Multiline = True
        Me.txtOverlap.Name = "txtOverlap"
        Me.txtOverlap.Size = New System.Drawing.Size(508, 40)
        Me.txtOverlap.TabIndex = 36
        Me.txtOverlap.Text = ""
        '
        'cmdImportList
        '
        Me.cmdImportList.ForeColor = System.Drawing.Color.Brown
        Me.cmdImportList.Location = New System.Drawing.Point(608, 104)
        Me.cmdImportList.Name = "cmdImportList"
        Me.cmdImportList.Size = New System.Drawing.Size(96, 32)
        Me.cmdImportList.TabIndex = 35
        Me.cmdImportList.Text = "Import Client List"
        '
        'cmdSaveResult
        '
        Me.cmdSaveResult.Enabled = False
        Me.cmdSaveResult.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.cmdSaveResult.Location = New System.Drawing.Point(608, 40)
        Me.cmdSaveResult.Name = "cmdSaveResult"
        Me.cmdSaveResult.Size = New System.Drawing.Size(96, 23)
        Me.cmdSaveResult.TabIndex = 34
        Me.cmdSaveResult.Text = "Save Result"
        '
        'grdResult
        '
        Me.grdResult.AlternatingBackColor = System.Drawing.Color.White
        Me.grdResult.BackColor = System.Drawing.Color.White
        Me.grdResult.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdResult.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdResult.CaptionVisible = False
        Me.grdResult.DataMember = ""
        Me.grdResult.FlatMode = True
        Me.grdResult.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdResult.ForeColor = System.Drawing.Color.Black
        Me.grdResult.GridLineColor = System.Drawing.Color.Wheat
        Me.grdResult.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdResult.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdResult.HeaderForeColor = System.Drawing.Color.Black
        Me.grdResult.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.Location = New System.Drawing.Point(0, 0)
        Me.grdResult.Name = "grdResult"
        Me.grdResult.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdResult.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdResult.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdResult.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdResult.Size = New System.Drawing.Size(600, 168)
        Me.grdResult.TabIndex = 24
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtViewSQL)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(708, 218)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "View Log"
        '
        'txtViewSQL
        '
        Me.txtViewSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtViewSQL.Location = New System.Drawing.Point(0, 0)
        Me.txtViewSQL.Multiline = True
        Me.txtViewSQL.Name = "txtViewSQL"
        Me.txtViewSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtViewSQL.Size = New System.Drawing.Size(708, 218)
        Me.txtViewSQL.TabIndex = 0
        Me.txtViewSQL.Text = ""
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.cmdClearList)
        Me.TabPage6.Controls.Add(Me.cmdBuildList)
        Me.TabPage6.Controls.Add(Me.txtPlanCode)
        Me.TabPage6.Controls.Add(Me.lstProduct)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(708, 218)
        Me.TabPage6.TabIndex = 2
        Me.TabPage6.Text = "Value Builder"
        '
        'cmdClearList
        '
        Me.cmdClearList.Location = New System.Drawing.Point(400, 136)
        Me.cmdClearList.Name = "cmdClearList"
        Me.cmdClearList.TabIndex = 3
        Me.cmdClearList.Text = "Clear"
        '
        'cmdBuildList
        '
        Me.cmdBuildList.Location = New System.Drawing.Point(320, 136)
        Me.cmdBuildList.Name = "cmdBuildList"
        Me.cmdBuildList.TabIndex = 2
        Me.cmdBuildList.Text = "Build"
        '
        'txtPlanCode
        '
        Me.txtPlanCode.Location = New System.Drawing.Point(320, 4)
        Me.txtPlanCode.Multiline = True
        Me.txtPlanCode.Name = "txtPlanCode"
        Me.txtPlanCode.Size = New System.Drawing.Size(384, 128)
        Me.txtPlanCode.TabIndex = 1
        Me.txtPlanCode.Text = ""
        '
        'lstProduct
        '
        Me.lstProduct.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProduct.ItemHeight = 15
        Me.lstProduct.Location = New System.Drawing.Point(4, 4)
        Me.lstProduct.Name = "lstProduct"
        Me.lstProduct.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstProduct.Size = New System.Drawing.Size(312, 214)
        Me.lstProduct.TabIndex = 0
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.txtCustom)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(708, 218)
        Me.TabPage7.TabIndex = 3
        Me.TabPage7.Text = "Custom"
        '
        'txtCustom
        '
        Me.txtCustom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustom.Location = New System.Drawing.Point(0, 0)
        Me.txtCustom.Multiline = True
        Me.txtCustom.Name = "txtCustom"
        Me.txtCustom.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCustom.Size = New System.Drawing.Size(708, 218)
        Me.txtCustom.TabIndex = 0
        Me.txtCustom.Text = ""
        '
        'tvAllFields
        '
        Me.tvAllFields.ImageIndex = -1
        Me.tvAllFields.Location = New System.Drawing.Point(4, 52)
        Me.tvAllFields.Name = "tvAllFields"
        Me.tvAllFields.SelectedImageIndex = -1
        Me.tvAllFields.Size = New System.Drawing.Size(192, 148)
        Me.tvAllFields.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 16)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Fields Available:"
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.HotTrack = True
        Me.TabControl2.Location = New System.Drawing.Point(200, 36)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(520, 164)
        Me.TabControl2.TabIndex = 31
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.cmdClear)
        Me.TabPage5.Controls.Add(Me.cmdLoadQry)
        Me.TabPage5.Controls.Add(Me.cmdSaveQry)
        Me.TabPage5.Controls.Add(Me.chkDISTINCT)
        Me.TabPage5.Controls.Add(Me.GroupBox2)
        Me.TabPage5.Controls.Add(Me.GroupBox1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(512, 138)
        Me.TabPage5.TabIndex = 2
        Me.TabPage5.Text = "Setup"
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(428, 108)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(80, 23)
        Me.cmdClear.TabIndex = 38
        Me.cmdClear.Text = "Clear All"
        '
        'cmdLoadQry
        '
        Me.cmdLoadQry.Location = New System.Drawing.Point(340, 108)
        Me.cmdLoadQry.Name = "cmdLoadQry"
        Me.cmdLoadQry.Size = New System.Drawing.Size(80, 23)
        Me.cmdLoadQry.TabIndex = 37
        Me.cmdLoadQry.Text = "Load Query"
        '
        'cmdSaveQry
        '
        Me.cmdSaveQry.Location = New System.Drawing.Point(252, 108)
        Me.cmdSaveQry.Name = "cmdSaveQry"
        Me.cmdSaveQry.Size = New System.Drawing.Size(80, 23)
        Me.cmdSaveQry.TabIndex = 36
        Me.cmdSaveQry.Text = "Save Query"
        '
        'chkDISTINCT
        '
        Me.chkDISTINCT.Checked = True
        Me.chkDISTINCT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDISTINCT.Location = New System.Drawing.Point(320, 12)
        Me.chkDISTINCT.Name = "chkDISTINCT"
        Me.chkDISTINCT.Size = New System.Drawing.Size(120, 24)
        Me.chkDISTINCT.TabIndex = 35
        Me.chkDISTINCT.Text = "DISTINCT record"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.radPI)
        Me.GroupBox2.Controls.Add(Me.radPH)
        Me.GroupBox2.Location = New System.Drawing.Point(164, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(144, 76)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Policy Based On"
        '
        'radPI
        '
        Me.radPI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radPI.Location = New System.Drawing.Point(8, 44)
        Me.radPI.Name = "radPI"
        Me.radPI.Size = New System.Drawing.Size(124, 24)
        Me.radPI.TabIndex = 34
        Me.radPI.Text = "Policy Insured (PI)"
        '
        'radPH
        '
        Me.radPH.Checked = True
        Me.radPH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radPH.Location = New System.Drawing.Point(8, 20)
        Me.radPH.Name = "radPH"
        Me.radPH.Size = New System.Drawing.Size(124, 24)
        Me.radPH.TabIndex = 33
        Me.radPH.TabStop = True
        Me.radPH.Text = "Policy Holder (PH)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radCUST)
        Me.GroupBox1.Controls.Add(Me.radPol)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(156, 76)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Please Select a Type"
        '
        'radCUST
        '
        Me.radCUST.Location = New System.Drawing.Point(8, 44)
        Me.radCUST.Name = "radCUST"
        Me.radCUST.Size = New System.Drawing.Size(140, 24)
        Me.radCUST.TabIndex = 1
        Me.radCUST.Text = "Customer Based"
        '
        'radPol
        '
        Me.radPol.Checked = True
        Me.radPol.Location = New System.Drawing.Point(8, 20)
        Me.radPol.Name = "radPol"
        Me.radPol.TabIndex = 0
        Me.radPol.TabStop = True
        Me.radPol.Text = "Policy Based"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.cmdCancel)
        Me.TabPage3.Controls.Add(Me.cmdGenerate)
        Me.TabPage3.Controls.Add(Me.cmdRemoveCr)
        Me.TabPage3.Controls.Add(Me.cmdAddCr)
        Me.TabPage3.Controls.Add(Me.grdCriteria)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(512, 138)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Criteria"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(448, 108)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(60, 23)
        Me.cmdCancel.TabIndex = 29
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdGenerate
        '
        Me.cmdGenerate.Location = New System.Drawing.Point(448, 60)
        Me.cmdGenerate.Name = "cmdGenerate"
        Me.cmdGenerate.Size = New System.Drawing.Size(60, 23)
        Me.cmdGenerate.TabIndex = 28
        Me.cmdGenerate.Text = "Generate"
        '
        'cmdRemoveCr
        '
        Me.cmdRemoveCr.Location = New System.Drawing.Point(448, 32)
        Me.cmdRemoveCr.Name = "cmdRemoveCr"
        Me.cmdRemoveCr.Size = New System.Drawing.Size(60, 24)
        Me.cmdRemoveCr.TabIndex = 27
        Me.cmdRemoveCr.Text = "Remove"
        '
        'cmdAddCr
        '
        Me.cmdAddCr.Location = New System.Drawing.Point(448, 4)
        Me.cmdAddCr.Name = "cmdAddCr"
        Me.cmdAddCr.Size = New System.Drawing.Size(60, 24)
        Me.cmdAddCr.TabIndex = 26
        Me.cmdAddCr.Text = "Add"
        '
        'grdCriteria
        '
        Me.grdCriteria.AllowDrop = True
        Me.grdCriteria.AlternatingBackColor = System.Drawing.Color.White
        Me.grdCriteria.BackColor = System.Drawing.Color.White
        Me.grdCriteria.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCriteria.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCriteria.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdCriteria.CaptionVisible = False
        Me.grdCriteria.DataMember = ""
        Me.grdCriteria.Dock = System.Windows.Forms.DockStyle.Left
        Me.grdCriteria.FlatMode = True
        Me.grdCriteria.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCriteria.ForeColor = System.Drawing.Color.Black
        Me.grdCriteria.GridLineColor = System.Drawing.Color.Wheat
        Me.grdCriteria.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdCriteria.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCriteria.HeaderForeColor = System.Drawing.Color.Black
        Me.grdCriteria.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCriteria.Location = New System.Drawing.Point(0, 0)
        Me.grdCriteria.Name = "grdCriteria"
        Me.grdCriteria.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdCriteria.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdCriteria.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdCriteria.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdCriteria.Size = New System.Drawing.Size(444, 138)
        Me.grdCriteria.TabIndex = 23
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmdDown)
        Me.TabPage4.Controls.Add(Me.cmdUp)
        Me.TabPage4.Controls.Add(Me.cmdRemoveFld)
        Me.TabPage4.Controls.Add(Me.cmdAddFld)
        Me.TabPage4.Controls.Add(Me.lstOutput)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(512, 138)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Additional Output Fields"
        '
        'cmdDown
        '
        Me.cmdDown.Location = New System.Drawing.Point(388, 104)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(20, 23)
        Me.cmdDown.TabIndex = 31
        Me.cmdDown.Text = "D"
        '
        'cmdUp
        '
        Me.cmdUp.Location = New System.Drawing.Point(388, 72)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(20, 23)
        Me.cmdUp.TabIndex = 30
        Me.cmdUp.Text = "U"
        '
        'cmdRemoveFld
        '
        Me.cmdRemoveFld.Location = New System.Drawing.Point(388, 32)
        Me.cmdRemoveFld.Name = "cmdRemoveFld"
        Me.cmdRemoveFld.Size = New System.Drawing.Size(60, 24)
        Me.cmdRemoveFld.TabIndex = 29
        Me.cmdRemoveFld.Text = "Remove"
        '
        'cmdAddFld
        '
        Me.cmdAddFld.Location = New System.Drawing.Point(388, 4)
        Me.cmdAddFld.Name = "cmdAddFld"
        Me.cmdAddFld.Size = New System.Drawing.Size(60, 24)
        Me.cmdAddFld.TabIndex = 28
        Me.cmdAddFld.Text = "Add"
        '
        'lstOutput
        '
        Me.lstOutput.AllowDrop = True
        Me.lstOutput.Dock = System.Windows.Forms.DockStyle.Left
        Me.lstOutput.Location = New System.Drawing.Point(0, 0)
        Me.lstOutput.MultiColumn = True
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.Size = New System.Drawing.Size(384, 134)
        Me.lstOutput.TabIndex = 0
        '
        'cboCampaign
        '
        Me.cboCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCampaign.Location = New System.Drawing.Point(72, 4)
        Me.cboCampaign.Name = "cboCampaign"
        Me.cboCampaign.Size = New System.Drawing.Size(420, 21)
        Me.cboCampaign.TabIndex = 35
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 16)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Campaign:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(504, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 16)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Channel:"
        '
        'cboChannel
        '
        Me.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChannel.Location = New System.Drawing.Point(560, 4)
        Me.cboChannel.Name = "cboChannel"
        Me.cboChannel.Size = New System.Drawing.Size(96, 21)
        Me.cboChannel.TabIndex = 38
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(668, 4)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(48, 23)
        Me.cmdLoad.TabIndex = 40
        Me.cmdLoad.Text = "Load"
        '
        'uclQuery
        '
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboChannel)
        Me.Controls.Add(Me.cboCampaign)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.tvAllFields)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "uclQuery"
        Me.Size = New System.Drawing.Size(724, 452)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.grdCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Function InitControl()
        Call loadCampaign()
    End Function

    Private Sub uclQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call BuildTree()

        AddHandler cboOperator.TextChanged, AddressOf Ctrls_TextChanged
        cboOperator.Name = "cboOperator"
        cboOperator.Visible = False
        cboOperator.Items.Clear()
        cboOperator.Items.Add("=")
        cboOperator.Items.Add("<>")
        cboOperator.Items.Add(">")
        cboOperator.Items.Add("<")
        cboOperator.Items.Add(">=")
        cboOperator.Items.Add("<=")
        cboOperator.Items.Add("IN")
        cboOperator.Items.Add("LIKE")
        cboOperator.Items.Add("BETWEEN")
        grdCriteria.Controls.Add(cboOperator)

        AddHandler cboLogicOp.TextChanged, AddressOf Ctrls_TextChanged
        cboLogicOp.Name = "cboLogicOp"
        cboLogicOp.Visible = False
        cboLogicOp.Items.Clear()
        cboLogicOp.Items.Add("AND")
        cboLogicOp.Items.Add("OR")
        grdCriteria.Controls.Add(cboLogicOp)

        dtCriteriaList.Columns.Add("ID", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("Criteria", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("Op", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("Value", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("ValueM", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("Parameter", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("ParameterM", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("source", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("exectype", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("level", Type.GetType("System.String"))
        dtCriteriaList.Columns.Add("logicop", Type.GetType("System.String"))

        dtOutput.Columns.Add("ID", Type.GetType("System.String"))
        dtOutput.Columns.Add("Criteria", Type.GetType("System.String"))
        dtOutput.Columns.Add("WithCri", Type.GetType("System.String"))
        dtOutput.Columns.Add("Done", Type.GetType("System.String"))

        Dim ts As New clsDataGridTableStyle
        Dim cs As DataGridTextBoxColumn

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "Criteria"
        cs.HeaderText = "Criteria"
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Op"
        cs.HeaderText = "Operator"
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Value"
        cs.HeaderText = "Value"
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "Parameter"
        cs.HeaderText = "Parameter"
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 80
        cs.MappingName = "LogicOp"
        cs.HeaderText = "Logical Op."
        cs.NullText = ""
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CRITERIA"
        grdCriteria.TableStyles.Add(ts)
        grdCriteria.DataSource = dtCriteriaList
        bm = Me.BindingContext(dtCriteriaList)

        Call loadCampaign()
        Call BuildProductList()

        blnLoading = False

    End Sub

    Function LoadComboBox(ByRef dt As DataTable, ByRef cbo As ComboBox, ByVal strCode As String, ByVal strName As String, ByVal strSQL As String, Optional ByVal blnAllowNull As Boolean = False) As Boolean
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim blnLoad As Boolean

        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            dt.Clear()
            sqlconnect.ConnectionString = strCIWConn
            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.Fill(dt)
            blnLoad = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
            blnLoad = False
        End Try

        If blnLoad Then
            cbo.DataSource = dt
            cbo.DisplayMember = strName
            cbo.ValueMember = strCode
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub loadCampaign()
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select distinct crmcmp_campaign_id, crmcmp_campaign_name " & _
            " From " & serverPrefix & "crm_campaign where crmcmp_status_id <> '03' order by crmcmp_campaign_name"
        LoadComboBox(dtCampaign, cboCampaign, "crmcmp_campaign_id", "crmcmp_campaign_name", strSQL)
        loadChannel(cboCampaign.SelectedValue)

    End Sub

    Private Sub loadChannel(ByVal campID As String)

        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select crmcpc_channel_id, crmcpt_channel_desc " & _
            " From " & serverPrefix & "crm_campaign_channel, " & serverPrefix & "crm_campaign_channel_type " & _
            " Where crmcpc_campaign_id = '" & campID & "' and crmcpc_channel_id = crmcpt_channel_id and crmcpc_status_id <> '03'"
        LoadComboBox(dtChannel, cboChannel, "crmcpc_channel_id", "crmcpt_channel_desc", strSQL)

    End Sub

    Private Sub tvAllFields_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvAllFields.ItemDrag
        Dim myNode As TreeNode = DirectCast(e.Item, TreeNode)
        If Not CStr(myNode.Tag).EndsWith("C") Then
            DoDragDrop(myNode.Text & "~" & myNode.Tag, DragDropEffects.Copy)
        End If
    End Sub

    Private Sub grdCriteria_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles grdCriteria.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub grdCriteria_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles grdCriteria.DragDrop

        Dim dr As DataRow
        Dim strDragItem() As String
        Dim strID As String

        strDragItem = CStr(e.Data.GetData(DataFormats.Text)).Split("~")

        If strDragItem(1).EndsWith("P") Then
            strID = Mid(strDragItem(1), 1, Len(strDragItem(1)) - 1)
        Else
            strID = strDragItem(1)
        End If

        If CheckDup(strID, "C") Then
            Exit Sub
        End If

        dr = dtCriteriaList.NewRow
        dr.Item("ID") = strID
        dr.Item("Criteria") = strDragItem(0)
        dr.Item("Op") = ""
        dr.Item("Value") = ""
        dr.Item("Parameter") = ""
        dr.Item("source") = ""
        dr.Item("exectype") = ""
        dr.Item("level") = ""
        dr.Item("logicop") = "AND"
        dtCriteriaList.Rows.Add(dr)

        If lstOutput.FindString(strDragItem(0)) = -1 Then
            dr = dtOutput.NewRow
            dr.Item("ID") = strID
            dr.Item("Criteria") = strDragItem(0)
            dr.Item("WithCri") = "Y"
            dr.Item("Done") = "N"
            dtOutput.Rows.Add(dr)
            lstOutput.Items.Add(strDragItem(0))
        End If

    End Sub

    Private Sub ListBox1_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstOutput.DragDrop
        Dim strDragItem() As String
        Dim dr As DataRow

        strDragItem = CStr(e.Data.GetData(DataFormats.Text)).Split("~")
        If Not strDragItem(1).EndsWith("P") Then

            If CheckDup(strDragItem(1), "F") Then
                Exit Sub
            End If

            dr = dtOutput.NewRow
            dr.Item("ID") = strDragItem(1)
            dr.Item("Criteria") = strDragItem(0)
            dr.Item("WithCri") = "N"
            dr.Item("Done") = "N"
            dtOutput.Rows.Add(dr)
            lstOutput.Items.Add(strDragItem(0))
        End If

    End Sub

    Private Sub ListBox1_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstOutput.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub grdCriteria_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles grdCriteria.Paint
        If grdCriteria.CurrentCell.ColumnNumber = 1 Then
            cboOperator.Width = grdCriteria.GetCurrentCellBounds.Width
        End If
        If grdCriteria.CurrentCell.ColumnNumber = 4 Then
            cboLogicOp.Width = grdCriteria.GetCurrentCellBounds.Width
        End If
    End Sub

    Private Sub Ctrls_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If grdCriteria.CurrentCell.ColumnNumber = 1 Then
            cboOperator.Visible = False
            'If grdCriteria.Item(grdCriteria.CurrentCell) & "" = "" Then
            '    SendKeys.Send("*")
            'End If

            grdCriteria.Item(grdCriteria.CurrentCell) = cboOperator.Text

            If grdCriteria.CurrentRowIndex <= dtCriteriaList.Rows.Count - 1 Then
                With dtCriteriaList.Rows(grdCriteria.CurrentRowIndex)
                    If IsDBNull(.Item("Value")) OrElse RTrim(.Item("Value")) = "" Then
                        Select Case cboOperator.Text
                            Case "BETWEEN"
                                .Item("Value") = "10 AND 20"
                            Case "IN"
                                .Item("Value") = "('M','F')"
                            Case "LIKE"
                                .Item("Value") = "ABC%"
                            Case Else
                                .Item("Value") = ""
                        End Select
                    End If
                End With
            End If
        End If

        If grdCriteria.CurrentCell.ColumnNumber = 4 Then
            cboLogicOp.Visible = False
            'If grdCriteria.Item(grdCriteria.CurrentCell) & "" = "" Then
            '    SendKeys.Send("*")
            'End If
            grdCriteria.Item(grdCriteria.CurrentCell) = cboLogicOp.Text
        End If

    End Sub

    Private Sub grdCriteria_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCriteria.CurrentCellChanged
        If grdCriteria.CurrentCell.ColumnNumber = 1 Then
            cboOperator.Visible = False
            cboOperator.Width = 0
            cboOperator.Left = grdCriteria.GetCurrentCellBounds.Left
            cboOperator.Top = grdCriteria.GetCurrentCellBounds.Top
            cboOperator.Text = grdCriteria.Item(grdCriteria.CurrentCell) & ""
            cboOperator.Visible = True
        ElseIf grdCriteria.CurrentCell.ColumnNumber = 4 Then
            cboLogicOp.Visible = False
            cboLogicOp.Width = 0
            cboLogicOp.Left = grdCriteria.GetCurrentCellBounds.Left
            cboLogicOp.Top = grdCriteria.GetCurrentCellBounds.Top
            cboLogicOp.Text = grdCriteria.Item(grdCriteria.CurrentCell) & ""
            cboLogicOp.Visible = True
        Else
            cboOperator.Visible = False
            cboOperator.Width = 0
            cboLogicOp.Visible = False
            cboLogicOp.Width = 0
        End If
    End Sub

    Private Sub grdCriteria_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCriteria.Click
        cboOperator.Visible = False
        cboOperator.Width = 0
    End Sub

    Private Sub grdCriteria_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCriteria.Scroll
        cboOperator.Visible = False
        cboOperator.Width = 0
    End Sub

    Private Function GetFieldRel(ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim dtQuery As New DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select * " & _
                " From " & serverPrefix & "crm_campaign_query_fields " & _
                " Order by crmcpf_category"

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(dtQuery)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = -1
            strErrMsg = sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = ex.ToString
        End Try

        Return dtQuery

    End Function

    Private Function GetFieldList(ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim dtQuery As New DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select * " & _
                " From " & serverPrefix & "crm_campaign_query_fields " & _
                " Order by crmcpf_category, crmcpf_order"

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(dtQuery)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = -1
            strErrMsg = sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = ex.ToString
        End Try

        Return dtQuery

    End Function

    Private Function GetCategoryList(ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        Dim strSQL As String
        Dim sqlconnect As New SqlConnection
        Dim sqlda As SqlDataAdapter
        Dim dtCategory As New DataTable
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        strSQL = "Select * " & _
                " From " & serverPrefix & "crm_campaign_query_category " & _
                " Order by crmcqc_category"

        sqlconnect.ConnectionString = strCIWConn

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
        sqlda.MissingMappingAction = MissingMappingAction.Passthrough

        Try
            sqlda.Fill(dtCategory)
        Catch sqlex As SqlClient.SqlException
            lngErrNo = -1
            strErrMsg = sqlex.ToString
        Catch ex As Exception
            lngErrNo = -1
            strErrMsg = ex.ToString
        End Try

        Return dtCategory

    End Function

    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click

        Dim drCriteria As DataRow()
        Dim strFieldList As String()
        Dim strSource As String()
        Dim strFROM As String()
        Dim strWHERE As String()

        Dim strBASE As String()
        Dim strSOut As DataTable()
        Dim intSourceCnt, intResultCnt, intSOutCnt As Integer

        Dim dtResultSet As DataTable()

        strDataMode = "C"

        blnCancelQ = False

        ' Lock to current user
        If SetLock(True) = False Then
            blnLocked = False
            MsgBox("Other user is running query, please try again later", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Exit Sub
        Else
            blnLocked = True
        End If

        ' ===== REWRITE ========

        ' Check accountstatuscode, companyID
        ' Check criteria, if the data type of value/parameter is acceptable


        ' Find each criteria's Source, exectype, and level
        ' Group according to logic operator => A and B or C and D => A and (B or C) and D
        Dim strCurOp, strPrvOp As String
        Dim strGroup As String()
        Dim intGrpCnt As Integer
        Dim blnOK As Boolean = True
        Dim blnBase As Boolean

        txtViewSQL.Text = ""
        intGrpCnt = -1
        blnOK = True
        blnBase = False

        For i As Integer = 0 To dtOutput.Rows.Count - 1
            dtOutput.Rows(i).Item("Done") = "N"
        Next
        dtOutput.AcceptChanges()

        For i As Integer = 0 To dtCriteriaList.Rows.Count - 1
            With dtCriteriaList.Rows(i)
                drCriteria = dtQuery.Select("crmcpf_field_id = " & .Item("ID"))
                If drCriteria.Length > 0 Then
                    .Item("source") = drCriteria(0).Item("crmcpf_source")
                    .Item("exectype") = drCriteria(0).Item("crmcpf_exec_type")
                    .Item("level") = drCriteria(0).Item("crmcpf_level")
                    .AcceptChanges()
                Else
                    MsgBox("System error, criteria not found!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                    Exit Sub
                End If

                gCrmMode = "SalesLeads"
                Call EnableButton(True)

                If IsDBNull(.Item("Value")) Then .Item("Value") = ""

                .Item("Value") = UCase(.Item("Value"))
                .Item("ValueM") = .Item("Value")

                If InStr(.Item("ValueM"), "'") AndAlso Trim(.Item("Op")) <> "IN" AndAlso Trim(.Item("Op")) <> "BETWEEN" Then
                    .Item("ValueM") = Replace(UCase(.Item("ValueM")), "'", "''")
                End If

                If Trim(drCriteria(0).Item("crmcpf_data_type")) <> "" AndAlso Trim(drCriteria(0).Item("crmcpf_data_type")) <> "X" Then
                    ' Check criteria data type

                    If Trim(.Item("Op")) <> "IN" And Trim(.Item("Op")) <> "BETWEEN" Then
                        blnOK = CheckDataType(.Item("Value"), drCriteria(0).Item("crmcpf_data_type"))
                    Else
                        ' Use regular expression to check IN and BETWEEN
                        If Trim(.Item("Op")) = "IN" Then
                            If drCriteria(0).Item("crmcpf_data_type") = "C" Then
                                blnOK = System.Text.RegularExpressions.Regex.IsMatch(.Item("Value"), "^\(('\w*',?)*\)$", _
                                           System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            End If
                            If drCriteria(0).Item("crmcpf_data_type") = "N" Then
                                blnOK = System.Text.RegularExpressions.Regex.IsMatch(.Item("Value"), "^\((\d+,?)*\)$", _
                                   System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            End If
                        End If

                        If Trim(.Item("Op")) = "BETWEEN" Then
                            If drCriteria(0).Item("crmcpf_data_type") = "C" Then
                                blnOK = System.Text.RegularExpressions.Regex.IsMatch(.Item("Value"), "^'\w*' and '\w*'$", _
                                           System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            End If
                            If drCriteria(0).Item("crmcpf_data_type") = "N" Then
                                blnOK = System.Text.RegularExpressions.Regex.IsMatch(.Item("Value"), "^\d+ and \d+$", _
                                           System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            End If
                        End If
                    End If

                    'If Trim(.ItemArray("Value")) = "'" Then
                    '    blnOK = False
                    'End If

                    If blnOK = False Then
                        MsgBox("Invalid Data Type - " & .Item("Criteria") & ", please check!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                        Exit Sub
                    Else
                        'If drCriteria(0).Item("crmcpf_source") Like "CAPSIL*" AndAlso drCriteria(0).Item("crmcpf_data_type") = "D" Then
                        '    .Item("ValueM") = "'" & CPSDate(.Item("Value"), 0) & "'"
                        'End If

                        If drCriteria(0).Item("crmcpf_data_type") = "N" Then
                            If Trim(.Item("Op")) <> "IN" And Trim(.Item("Op")) <> "BETWEEN" Then
                                .Item("ValueM") = CDec(.Item("ValueM"))
                            End If
                        End If

                        If drCriteria(0).Item("crmcpf_data_type") = "C" Then
                            If Trim(.Item("Op")) <> "IN" And Trim(.Item("Op")) <> "BETWEEN" Then
                                .Item("ValueM") = "'" & .Item("ValueM") & "'"
                            End If
                        End If

                        If drCriteria(0).Item("crmcpf_data_type") = "D" Then
                            If Trim(.Item("Op")) <> "IN" And Trim(.Item("Op")) <> "BETWEEN" Then
                                If drCriteria(0).Item("crmcpf_source") Like "CAPSIL*" Then
                                    .Item("ValueM") = "'" & CPSDate(.Item("Value"), 0) & "'"
                                Else
                                    .Item("ValueM") = "'" & .Item("ValueM") & "'"
                                End If
                            End If
                        End If
                    End If

                    ' Check operator
                    If Trim(.Item("Op")) <> "" Then
                        If cboOperator.Items.IndexOf(Trim(.Item("Op"))) = -1 Then
                            blnOK = False
                            MsgBox("Invalid Operator - " & .Item("Criteria") & ", please check!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                            Exit Sub
                        End If
                    Else
                        blnOK = False
                        MsgBox("Invalid Operator - " & .Item("Criteria") & ", please check!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                        Exit Sub
                    End If
                End If

                ' Check parameter data type
                If Not IsDBNull(drCriteria(0).Item("crmcpf_para_type")) AndAlso Trim(drCriteria(0).Item("crmcpf_para_type")) <> "" Then
                    .Item("ParameterM") = .Item("Parameter")
                    If Trim(.Item("parameter")) <> "" Then
                        blnOK = CheckDataType(.Item("parameter"), drCriteria(0).Item("crmcpf_para_type"))
                        'If Trim(.ItemArray("parameter")) = "'" Then
                        '    blnOK = False
                        'End If
                        If blnOK = False Then
                            MsgBox("Invalid Parameter Data Type - " & .Item("Criteria") & ", please check!" & vbCrLf & _
                            "Parameter: " & drCriteria(0).Item("crmcpf_para_msg"), MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                            Exit Sub
                        Else
                            If drCriteria(0).Item("crmcpf_para_type") = "C" Then
                                '.Item("ParameterM") = "'" & CPSDate(.Item("Parameter"), 0) & "'"
                                .Item("ParameterM") = "'" & .Item("Parameter") & "'"
                            End If
                            If drCriteria(0).Item("crmcpf_para_type") = "D" Then
                                If drCriteria(0).Item("crmcpf_source") Like "CAPSIL*" Then
                                    .Item("ParameterM") = "'" & CPSDate(.Item("Parameter"), 0) & "'"
                                Else
                                    .Item("ParameterM") = "'" & .Item("Parameter") & "'"
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Criteria '" & .Item("Criteria") & "' requires a parameter, please check!" & vbCrLf & _
                            "Parameter: " & drCriteria(0).Item("crmcpf_para_msg"), MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                        Exit Sub
                    End If
                End If

                ' Check logical operator
                If i < dtCriteriaList.Rows.Count - 1 Then
                    If Trim(.Item("logicop")) = "" Then
                        blnOK = False
                        MsgBox("Invalid Logical Operator - " & .Item("Criteria") & ", please check!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                        Exit Sub
                    Else
                        If cboLogicOp.Items.IndexOf(Trim(.Item("logicop"))) = -1 Then
                            blnOK = False
                            MsgBox("Invalid Logical Operator - " & .Item("Criteria") & ", please check!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                            Exit Sub
                        End If
                    End If
                End If

                If drCriteria(0).Item("crmcpf_category") = "01" OrElse drCriteria(0).Item("crmcpf_category") = "03" OrElse drCriteria(0).Item("crmcpf_category") = "07" Then
                    blnBase = True
                End If

                If blnOK = False Then
                    Exit Sub
                End If

                strCurOp = IIf(IsDBNull(.Item("logicop")), "", .Item("logicop"))
                If i > 0 Then
                    strPrvOp = IIf(IsDBNull(dtCriteriaList.Rows(i - 1).Item("LogicOp")), "AND", dtCriteriaList.Rows(i - 1).Item("LogicOp"))
                Else
                    strPrvOp = ""
                End If

                If strPrvOp = "" Or strPrvOp = "AND" Then
                    intGrpCnt += 1
                    ReDim Preserve strGroup(intGrpCnt)
                    strGroup(intGrpCnt) = .Item("ID")
                End If

                If strPrvOp = "OR" Then
                    strGroup(intGrpCnt) &= "," & .Item("ID")
                End If

            End With
        Next

        ' If no criteria from base group, reject
        If blnBase = False Then
            MsgBox("No Criteria from base group, please re-define.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
            Exit Sub
        End If

        ' Process each group
        ' Process each source within group
        ' Process Field or UDF together within same source
        ' Process Script alone
        ' If more than one item in a group, apply OR	

        Dim dtGroupRlt(intGrpCnt) As DataTable
        Dim dtSubGrpRlt As DataTable()
        Dim intFinal As Integer

        Dim drFind As DataRow()
        Dim drTemp As DataRow
        Dim strIDList As String
        Dim strORWhere, strORField, strORBase, strORSource As String()
        Dim blnValid As Boolean

        intFinal = -1
        strIDList = ""
        ' 1st time run OR, 2nd time run AND
        For lg As Integer = 0 To 1

            If lg = 0 Then
                For g As Integer = 0 To intGrpCnt
                    If strGroup(g) Like "*,*" Then
                        strIDList = strGroup(g)
                        'intFinal += 1
                        'dtGroupRlt(intFinal) = RunCriteria1("OR", strIDList, dtCriteriaList, strORSource, strORField, strORWhere, strORBase, blnValid)
                        Call RunCriteria1("OR", strIDList, dtCriteriaList, strORSource, strORField, strORWhere, strORBase, blnValid)
                        dtCriteriaList.DefaultView.RowFilter = ""
                        dtCriteriaList.DefaultView.Sort = ""
                    End If
                Next

                ' If no OR criteria
                If strIDList = "" Then
                    blnValid = True
                End If
            End If

            If lg = 1 Then
                strIDList = ""
                If blnValid Then
                    For g As Integer = 0 To intGrpCnt
                        If Not strGroup(g) Like "*,*" Then
                            If strIDList = "" Then
                                strIDList = strGroup(g)
                            Else
                                strIDList &= "," & strGroup(g)
                            End If
                        End If
                    Next
                    'If strIDList <> "" Then
                    '    intFinal += 1
                    '    dtGroupRlt(intFinal) = RunCriteria1("AND", strIDList, dtCriteriaList, strORWhere, strORField)
                    'End If
                    intFinal += 1
                    dtGroupRlt(intFinal) = RunCriteria1("AND", strIDList, dtCriteriaList, strORSource, strORField, strORWhere, strORBase, blnValid)
                    dtCriteriaList.DefaultView.RowFilter = ""
                    dtCriteriaList.DefaultView.Sort = ""
                Else
                    MsgBox("Criteria is not valid for the 'OR' operator.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                    blnCancelQ = True
                End If
            End If

            If blnCancelQ = True Then
                MsgBox("Sales Leads selection is cancelled.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                Exit Sub
            End If

        Next

        ' Combine result using AND within each group				
        If dtGroupRlt.Length <= 0 Then
            Exit Sub
        End If

        dtFinal = New DataTable("FINAL")
        dtFinal = dtGroupRlt(0).Copy

        If intFinal > 0 Then
            For j As Integer = 1 To intFinal
                'copy column
                For k As Integer = 0 To dtGroupRlt(j).Columns.Count - 1
                    Try
                        dtFinal.Columns.Add(dtGroupRlt(j).columns(k).columnname, dtGroupRlt(j).columns(k).datatype)
                    Catch ex As Exception
                        ' duplicate column name
                        ' nothing to do!
                    End Try
                Next
            Next

            ' Check if key match in all tables
            Dim blnMatch As Boolean
            For j As Integer = 0 To dtFinal.Rows.Count - 1
                blnMatch = True
                For k As Integer = 1 To intFinal
                    drFind = dtGroupRlt(k).select("CustomerID = " & dtFinal.Rows(j).item("CustomerID"))
                    If drFind.Length = 0 Then
                        blnMatch = False
                    End If
                Next

                ' if all match
                If blnMatch = True Then
                    'drTemp = dtFinal.NewRow
                    ' copy value for each table
                    For k As Integer = 1 To intFinal
                        drFind = dtGroupRlt(k).select("CustomerID = " & dtFinal.Rows(j).item("CustomerID"))
                        If drFind.Length <> 0 Then
                            For l As Integer = 0 To dtGroupRlt(k).columns.count - 1
                                'drTemp.Item(dtGroupRlt(k).columns(l).columnname) = drFind(0).Item(l)
                                dtFinal.Rows(j).Item(dtGroupRlt(k).columns(l).columnname) = drFind(0).Item(l)
                            Next
                        End If
                    Next
                    'dtGroupRlt(j).rows.add(drTemp)
                Else
                    dtFinal.Rows(j).Delete()
                End If
            Next
            dtFinal.AcceptChanges()
        End If

        ' Process additional fields not in the criteria list
        Dim drQuery() As DataRow
        Dim strOField() As String
        Dim strOBase() As String
        Dim strOSource() As String
        Dim strOCat() As String

        Dim intOutCnt As Integer = -1
        Dim intFLoc As Integer

        dtOutput.DefaultView.RowFilter = "WithCri = 'N' and Done = 'N'"
        strIDList = ""
        For i As Integer = 0 To dtOutput.DefaultView.Count - 1
            If strIDList = "" Then
                strIDList = dtOutput.DefaultView.Item(i).Item("ID")
            Else
                strIDList &= ", " & dtOutput.DefaultView.Item(i).Item("ID")
            End If
        Next

        If strIDList <> "" Then
            dtQuery.DefaultView.RowFilter = "crmcpf_field_id IN (" & strIDList & ")"
            dtQuery.DefaultView.Sort = "crmcpf_source, crmcpf_exec_type"

            With dtQuery.DefaultView
                For i As Integer = 0 To .Count - 1
                    'Get criteria details
                    If .Item(i).Item("crmcpf_category") = "02" OrElse .Item(i).Item("crmcpf_category") = "05" OrElse _
                            .Item(i).Item("crmcpf_category") = "06" OrElse .Item(i).Item("crmcpf_category") = "08" OrElse .Item(i).Item("crmcpf_category") = "09" Then

                        If intOutCnt < 0 OrElse strOSource(intOutCnt) <> .Item(i).Item("crmcpf_source") Then
                            intOutCnt += 1
                            ReDim Preserve strOField(intOutCnt)
                            ReDim Preserve strOBase(intOutCnt)
                            ReDim Preserve strOSource(intOutCnt)
                            ReDim Preserve strOCat(intOutCnt)

                            ' Init.
                            strOSource(intOutCnt) = .Item(i).Item("crmcpf_source")
                            drCriteria = dtCategory.Select("crmcqc_category=" & .Item(i).Item("crmcpf_category"))
                            strOBase(intOutCnt) = drCriteria(0).Item("crmcqc_base_sql")
                            strOField(intOutCnt) = ""
                            strOCat(intOutCnt) = .Item(i).Item("crmcpf_category")
                        End If

                        ' Update existing sql
                        Select Case .Item(i).Item("crmcpf_exec_type")
                            Case "F"
                                strOField(intOutCnt) &= ", " & RTrim(.Item(i).Item("crmcpf_table")) & "." & RTrim(.Item(i).Item("crmcpf_field_name"))
                            Case "U"
                                ' If the criteria can have an output
                                If Not IsDBNull(.Item(i).Item("crmcpf_field_name")) AndAlso RTrim(.Item(i).Item("crmcpf_field_name")) <> "" Then
                                    strOField(intOutCnt) &= ", " & RTrim(.Item(i).Item("crmcpf_field_detail1")) & _
                                        " " & RTrim(.Item(i).Item("crmcpf_field_detail2")) & " As " & RTrim(.Item(i).Item("crmcpf_field_name"))
                                End If
                        End Select
                    End If
                Next
            End With
        End If

        ' Append information to the final table (agent/survey/geographic)
        Dim strSQL, strErrMsg, strCList, strPList, strSAList, strWAList As String
        Dim lngErrNo As Long
        Dim intLoop, intCurRec As Integer

        intLoop = dtFinal.Rows.Count \ gQueryCnt

        If intOutCnt >= 0 AndAlso dtFinal.Rows.Count > 0 Then
            ReDim dtSubGrpRlt(intOutCnt)
            For l As Integer = 0 To intLoop
                ' Build list
                For k As Integer = 1 To gQueryCnt
                    If l * gQueryCnt + k > dtFinal.Rows.Count Then
                        Exit For
                    End If
                    '2 -policy, 5 - agent, 6 - customerid
                    Try
                        intCurRec = l * gQueryCnt + k - 1
                        If strCList = "" Then
                            strCList = "'" & Trim(dtFinal.Rows(intCurRec).Item("CustomerID")) & "'"
                            strPList = "'" & Trim(dtFinal.Rows(intCurRec).Item("PolicyAccountID")) & "'"
                            strSAList = "'" & Trim(dtFinal.Rows(intCurRec).Item("POAGCY")) & "'"
                            strWAList = "'" & Trim(dtFinal.Rows(intCurRec).Item("POWAGT")) & "'"
                        Else
                            strCList &= ", '" & Trim(dtFinal.Rows(intCurRec).Item("CustomerID")) & "'"
                            strPList &= ", '" & Trim(dtFinal.Rows(intCurRec).Item("PolicyAccountID")) & "'"
                            strSAList = "'" & Trim(dtFinal.Rows(intCurRec).Item("POAGCY")) & "'"
                            strWAList = "'" & Trim(dtFinal.Rows(intCurRec).Item("POWAGT")) & "'"
                        End If
                    Catch ex As Exception
                    End Try
                Next

                For i As Integer = 0 To intOutCnt
                    strSQL = strOBase(i)
                    strSQL = Replace(strSQL, "<field>", strOField(i))
                    Select Case strOCat(i)
                        Case "02"
                            'strSQL &= " And fld0004 IN (" & strPList & ")"
                            strSQL &= " And RPH.PolicyAccountID IN (" & strPList & ")"
                        Case "05"
                            If Trim(strOSource(i)) = "CAM" Then
                                If strSAList = "" Then
                                    MsgBox("Missing servicing agent information, please add it first.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                                    Exit Sub
                                Else
                                    strSQL &= " And camaid_agent_no IN (" & strSAList & ")"
                                End If
                            End If
                            If Trim(strOSource(i)) = "CAM1" Then
                                If strSAList = "" Then
                                    MsgBox("Missing writing agent information, please add it first.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                                    Exit Sub
                                Else
                                    strSQL &= " And camaid_agent_no IN (" & strWAList & ")"
                                End If
                            End If
                        Case "06"
                            strSQL &= " IN (" & strCList & ")"
                        Case "08"
                            strSQL &= " And Customer.CustomerID IN (" & strCList & ")"
                        Case "09"
                            strSQL &= " And rph.CustomerID IN (" & Replace(strCList, "'", "") & ")"
                    End Select
                    lngErrNo = 0
                    strErrMsg = ""
                    'dtSubGrpRlt(i) = ExecuteScript(strSQL, strOSource(i), lngErrNo, strErrMsg)
                    wndMain.Cursor = Cursors.WaitCursor
                    ' ES0001
                    dtSubGrpRlt(i) = ExecuteScript(strSQL, strOSource(i), lngErrNo, strErrMsg)
                    wndMain.Cursor = Cursors.Default
                    txtViewSQL.Text &= "==============================" & vbCrLf
                    txtViewSQL.Text &= strSQL & vbCrLf

                    If lngErrNo = -1 Then
                        MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                        Exit Sub
                    End If
                Next

                ' First time, append column
                If l = 0 Then
                    ' Append Delete & Overlap Column
                    'Try
                    '    dtFinal.Columns.Add("DeleteFlag", Type.GetType("System.String"))
                    'Catch ex As Exception
                    'End Try

                    'Try
                    '    dtFinal.Columns.Add("OverlapFlag", Type.GetType("System.String"))
                    'Catch ex As Exception
                    'End Try
                    Call PrepareDT(dtFinal)

                    For j As Integer = 0 To intOutCnt
                        'copy column
                        For k As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
                            Try
                                dtFinal.Columns.Add(dtSubGrpRlt(j).columns(k).columnname, dtSubGrpRlt(j).columns(k).datatype)
                            Catch ex As Exception
                                ' duplicate column name
                                ' nothing to do!
                            End Try
                        Next
                    Next
                End If

                ' Remove on 6/21/2007
                '''' Also Check overlap campaign
                ''''Dim rowToCheck As DataRow()
                ''''strSQL = "Select crmcmp_campaign_id, crmcmp_campaign_name, crmcsl_customer_id " & _
                ''''    " From crm_campaign, crm_campaign_channel, crm_campaign_sales_leads " & _
                ''''    " Where crmcpc_campaign_id = crmcsl_campaign_id " & _
                ''''    " And crmcmp_campaign_id = crmcpc_campaign_id " & _
                ''''    " And crmcpc_channel_id = crmcsl_channel_id " & _
                ''''    " And crmcpc_status_id <> '03' " & _
                ''''    " And crmcsl_customer_id IN (" & strCList & ")"
                '''''dtOverlap = ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)
                ''''wndMain.Cursor = Cursors.WaitCursor
                ''''' ES0001
                ''''dtOverlap = ExecuteScript(strSQL, "CIW", lngErrNo, strErrMsg)
                ''''wndMain.Cursor = Cursors.Default
                ''''txtViewSQL.Text &= "==============================" & vbCrLf
                ''''txtViewSQL.Text &= strSQL & vbCrLf

                ''''If lngErrNo = -1 Then
                ''''    MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                ''''    Exit Sub
                ''''End If

                ''''If Not dtOverlap Is Nothing Then
                ''''    For i As Integer = 0 To dtOverlap.Rows.Count - 1
                ''''        'rowToCheck = dtFinal.Rows.Find(dtOverlap.Rows(i).Item("crmcsl_customer_id"))
                ''''        rowToCheck = dtFinal.Select("CustomerID = " & dtOverlap.Rows(i).Item("crmcsl_customer_id"))
                ''''        If rowToCheck.Length > 0 Then
                ''''            For j As Integer = 0 To rowToCheck.Length - 1
                ''''                rowToCheck(j).Item("Overlap") = "Y"
                ''''            Next
                ''''        End If
                ''''    Next
                ''''End If

                ' Append value
                For i As Integer = 0 To intOutCnt
                    For j As Integer = 0 To dtSubGrpRlt(i).Rows.Count - 1
                        Select Case strOCat(i)
                            Case "02"
                                dtFinal.DefaultView.RowFilter = "PolicyAccountID = '" & dtSubGrpRlt(i).Rows(j).Item("PolicyAccountID") & "'"
                            Case "05"
                                dtFinal.DefaultView.RowFilter = "POAGCY = '" & dtSubGrpRlt(i).Rows(j).Item("AgentNo") & "'"
                            Case "06", "08", "09"
                                dtFinal.DefaultView.RowFilter = "CustomerID = '" & dtSubGrpRlt(i).Rows(j).Item("CustomerID") & "'"
                        End Select
                        For f As Integer = 0 To dtFinal.DefaultView.Count - 1
                            For k As Integer = 0 To dtSubGrpRlt(I).Columns.Count - 1
                                dtFinal.DefaultView.Item(f).Item(dtSubGrpRlt(i).Columns(k).ColumnName) = dtSubGrpRlt(i).Rows(j).Item(k)
                            Next
                        Next
                    Next
                Next
                dtFinal.AcceptChanges()
            Next
        End If

        dtFinal.DefaultView.RowFilter = ""
        dtFinal.DefaultView.Sort = "Overlap DESC"
        dtCriteriaList.DefaultView.RowFilter = ""
        dtCriteriaList.DefaultView.Sort = ""

        grdResult.DataSource = dtFinal
        lblCount.Text = "Records: " & dtFinal.Rows.Count

        If Not dtFinal Is Nothing Then bmResult = Me.BindingContext(dtFinal)

        ' unLock for current user
        blnLocked = False
        If SetLock(False) = False Then
            MsgBox("Problem unlock for current user, please contact IT.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Exit Sub
        End If

    End Sub

    Private Function CheckDataType(ByVal strVal As String, ByVal strDataType As String) As Boolean

        Select Case strDataType
            Case "C"
                Return True
            Case "N"
                Return IsNumeric(strVal)
            Case "D"
                Return IsDate(strVal)
        End Select

    End Function

    Private Function RunCriteria1(ByVal strCurLType As String, ByVal strIDList As String, ByVal dtCriteriaList As DataTable, _
        ByRef strSource_OR() As String, ByRef strField_OR() As String, ByRef strWhere_OR() As String, ByRef strBASE_OR() As String, ByRef blnValid As Boolean) As DataTable

        Dim drCriteria As DataRow()
        Dim drQuery As DataRow()

        Dim strFieldList As String()
        Dim strSource As String()
        Dim strFROM As String()
        Dim strWHERE As String()
        Dim strBASE As String()
        Dim intSourceCnt As Integer

        Dim dtResultSet As DataTable
        Dim strCurOp, strPrvOp As String
        Dim dtSubGrpRlt As DataTable()
        Dim drFind As DataRow()
        Dim drTemp As DataRow

        Dim strOP As String
        Dim blnOR As Boolean

        ' Process List of ID together
        If strIDList = "" Then strIDList = "-1"
        dtCriteriaList.DefaultView.RowFilter = "ID IN (" & strIDList & ")"
        dtCriteriaList.DefaultView.Sort = "source, exectype"

        intSourceCnt = -1
        blnOR = False
        blnValid = True

        If strCurLType = "OR" Then
            strOP = " OR "
        Else
            strOP = " AND "
        End If

        ' Process Criteria
        With dtCriteriaList.DefaultView
            For i As Integer = 0 To .Count - 1

                'Get criteria details
                drQuery = dtQuery.Select("crmcpf_field_id = " & .Item(i).Item("ID"))

                If intSourceCnt < 0 OrElse strSource(intSourceCnt) <> .Item(i).Item("source") Then
                    If drQuery(0).Item("crmcpf_exec_type") = "F" Or drQuery(0).Item("crmcpf_exec_type") = "U" Then

                        intSourceCnt += 1
                        ReDim Preserve strSource(intSourceCnt)
                        ReDim Preserve strBASE(intSourceCnt)
                        ReDim Preserve strFieldList(intSourceCnt)
                        ReDim Preserve strWHERE(intSourceCnt)

                        strSource(intSourceCnt) = .Item(i).Item("source")
                        drCriteria = dtCategory.Select("crmcqc_category=" & drQuery(0).Item("crmcpf_category"))
                        strBASE(intSourceCnt) = drCriteria(0).Item("crmcqc_base_sql")

                        ' Confirm later
                        If drQuery(0).Item("crmcpf_category") = "03" OrElse drQuery(0).Item("crmcpf_category") = "01" Then
                            'If radPH.Checked Then strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'O'")
                            If radPH.Checked Then strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'PH'")
                            If radPI.Checked Then
                                'strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'I'")
                                strBASE(intSourceCnt) = Replace(drCriteria(0).Item("crmcqc_base_sql1"), "<@1>", "'PI'")
                                ' If base on insured, need to join with rider no.
                                'strBASE(intSourceCnt) &= " And COTRNU = FLD0005 "
                            End If
                        End If

                        If drQuery(0).Item("crmcpf_category") = "01" OrElse drQuery(0).Item("crmcpf_category") = "03" OrElse drQuery(0).Item("crmcpf_category") = "07" Then
                            If chkDISTINCT.Checked = True Then
                                strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "SELECT ", "SELECT DISTINCT ")
                            End If
                        End If

                        strFieldList(intSourceCnt) = ""
                        strWHERE(intSourceCnt) = ""
                    End If
                Else
                    If Trim(strBASE(intSourceCnt)) = "" Then
                        drCriteria = dtCategory.Select("crmcqc_category=" & drQuery(0).Item("crmcpf_category"))
                        strBASE(intSourceCnt) = drCriteria(0).Item("crmcqc_base_sql")

                        ' Confirm later
                        If drQuery(0).Item("crmcpf_category") = "03" OrElse drQuery(0).Item("crmcpf_category") = "01" Then
                            'If radPH.Checked Then strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'O'")
                            If radPH.Checked Then strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'PH'")
                            If radPI.Checked Then
                                'strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'I'")
                                strBASE(intSourceCnt) = Replace(drCriteria(0).Item("crmcqc_base_sql1"), "<@1>", "'PI'")
                                ' If base on insured, need to join with rider no.
                                'strBASE(intSourceCnt) &= " And COTRNU = FLD0005 "
                            End If
                        End If

                        If drQuery(0).Item("crmcpf_category") = "01" OrElse drQuery(0).Item("crmcpf_category") = "03" OrElse drQuery(0).Item("crmcpf_category") = "07" Then
                            If chkDISTINCT.Checked = True Then
                                strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "SELECT ", "SELECT DISTINCT ")
                            End If
                        End If
                    End If
                End If

                With drQuery(0)
                    Select Case .Item("crmcpf_exec_type")
                        Case "F"
                            ' First Where Clause, no logical operator
                            If strWHERE(intSourceCnt) = "" Then
                                strWHERE(intSourceCnt) &= RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name")) & _
                                        " " & RTrim(dtCriteriaList.DefaultView.Item(i).Item("op")) & " " & Trim(dtCriteriaList.DefaultView.Item(i).Item("valueM"))
                            Else
                                strWHERE(intSourceCnt) &= strOP & RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name")) & _
                                        " " & RTrim(dtCriteriaList.DefaultView.Item(i).Item("op")) & " " & Trim(dtCriteriaList.DefaultView.Item(i).Item("valueM"))
                            End If

                            ' Append to field list as well
                            strFieldList(intSourceCnt) &= ", " & RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name"))
                        Case "U"
                            ' First Where clause, no logical operator
                            If strWHERE(intSourceCnt) = "" Then
                                strWHERE(intSourceCnt) &= RTrim(.Item("crmcpf_field_detail1")) & " " & RTrim(.Item("crmcpf_field_detail2"))
                            Else
                                strWHERE(intSourceCnt) &= strOP & RTrim(.Item("crmcpf_field_detail1")) & " " & RTrim(.Item("crmcpf_field_detail2"))
                            End If

                            ' If there is an operator & value to check
                            If Not IsDBNull(.Item("crmcpf_data_type")) AndAlso RTrim(.Item("crmcpf_data_type")) <> "" Then
                                strWHERE(intSourceCnt) &= " " & RTrim(dtCriteriaList.DefaultView.Item(i).Item("op")) & " " & Trim(dtCriteriaList.DefaultView.Item(i).Item("valueM"))
                            End If

                            ' If the criteria can have an output
                            If Not IsDBNull(.Item("crmcpf_field_name")) AndAlso RTrim(.Item("crmcpf_field_name")) <> "" Then
                                strFieldList(intSourceCnt) &= ", " & RTrim(.Item("crmcpf_field_detail1")) & _
                                    " " & RTrim(.Item("crmcpf_field_detail2")) & " As " & RTrim(.Item("crmcpf_field_name"))
                            End If

                            ' Apply parameter to the clause
                            If Not IsDBNull(.Item("crmcpf_para_type")) AndAlso RTrim(.Item("crmcpf_para_type")) <> "" Then
                                strWHERE(intSourceCnt) = Replace(strWHERE(intSourceCnt), "<@1>", dtCriteriaList.DefaultView.Item(i).Item("parameter"))
                                strFieldList(intSourceCnt) = Replace(strFieldList(intSourceCnt), "<@1>", dtCriteriaList.DefaultView.Item(i).Item("parameter"))
                            End If
                        Case "S"
                            blnValid = False
                    End Select
                End With
            Next
        End With

        ' Process additional fields not in the criteria list
        'Dim strOField() As String
        'Dim strOBase() As String
        'Dim strOSource() As String
        Dim drOutput() As DataRow

        Dim intOutCnt As Integer = -1

        drOutput = dtOutput.Select("WithCri = 'N' and Done = 'N'")
        For i As Integer = 0 To drOutput.Length - 1
            'Get criteria details
            drQuery = dtQuery.Select("crmcpf_field_id = " & drOutput(i).Item("ID"))

            Dim intFLoc As Integer
            intFLoc = -1
            For c As Integer = 0 To strSource.Length - 1
                If Trim(drQuery(0).Item("crmcpf_source")) = Trim(strSource(c)) Then
                    intFLoc = c
                End If
            Next

            If intFLoc >= 0 Then
                With drQuery(0)
                    ' Update existing sql
                    Select Case .Item("crmcpf_exec_type")
                        Case "F"
                            strFieldList(intFLoc) &= ", " & RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name"))
                            dtOutput.DefaultView.RowFilter = "ID = " & drOutput(i).Item("ID")
                            dtOutput.DefaultView.Item(0).Item("Done") = "Y"
                            dtOutput.AcceptChanges()

                        Case "U"
                            ' If the criteria can have an output
                            If Not IsDBNull(.Item("crmcpf_field_name")) AndAlso RTrim(.Item("crmcpf_field_name")) <> "" Then
                                strFieldList(intFLoc) &= ", " & RTrim(.Item("crmcpf_field_detail1")) & _
                                    " " & RTrim(.Item("crmcpf_field_detail2")) & " As " & RTrim(.Item("crmcpf_field_name"))
                            End If
                            dtOutput.DefaultView.RowFilter = "ID = " & drOutput(i).Item("ID")
                            dtOutput.DefaultView.Item(0).Item("Done") = "Y"
                            dtOutput.AcceptChanges()

                            '' Apply parameter to the clause
                            'If Not IsDBNull(.Item("crmcpf_para_type")) AndAlso RTrim(.Item("crmcpf_para_type")) <> "" Then
                            '    strFieldList(intFLoc) = Replace(strFieldList(intSourceCnt), "<@1>", dtCriteriaList.DefaultView.Item(i).Item("parameter"))
                            'End If
                    End Select
                End With
            End If
        Next
        dtOutput.DefaultView.RowFilter = ""

        Dim strSQL, strSQL1, strErrMsg As String
        Dim lngErrNo As Long

        ' Return inforamtion to caller if it is OR
        ' All criteria within OR must be the same source and no 'S' type
        If strCurLType = "OR" Then
            If intSourceCnt = 0 AndAlso blnValid = True Then
                Dim intORCnt As Integer
                If strSource_OR Is Nothing Then
                    intORCnt = 0
                Else
                    intORCnt = strSource_OR.Length
                End If

                ReDim Preserve strSource_OR(intORCnt)
                ReDim Preserve strBASE_OR(intORCnt)
                ReDim Preserve strField_OR(intORCnt)
                ReDim Preserve strWhere_OR(intORCnt)

                strSource_OR(intORCnt) = strSource(0)
                strBASE_OR(intORCnt) = strBASE(0)
                strField_OR(intORCnt) = strFieldList(0)
                strWhere_OR(intORCnt) = strWHERE(0)
                blnValid = True
            Else
                blnValid = False
            End If
            Exit Function
        Else
            Dim blnNoAND As Boolean = False
            ' If only have OR condition, copy it as first 'AND'
            If intSourceCnt < 0 AndAlso Not strBASE_OR Is Nothing AndAlso strBASE_OR.Length > 0 Then
                intSourceCnt = strBASE_OR.Length - 1
                ReDim Preserve strSource(intSourceCnt)
                ReDim Preserve strBASE(intSourceCnt)
                ReDim Preserve strFieldList(intSourceCnt)
                ReDim Preserve strWHERE(intSourceCnt)
                strSource = strSource_OR
                strBASE = strBASE_OR
                strFieldList = strField_OR
                strWHERE = strWhere_OR
                blnNoAND = True
            End If

            ' Check if any criteria need to run
            If intSourceCnt >= 0 Then
                ReDim dtSubGrpRlt(intSourceCnt)
                For i As Integer = 0 To intSourceCnt
                    ' If there is AND, and also OR, append OR to AND
                    If blnNoAND = False AndAlso Not strSource_OR Is Nothing Then
                        Dim strTmpWhere, strTmpField As String
                        For j As Integer = 0 To strSource_OR.Length - 1
                            If strSource_OR(j) = strSource(i) Then
                                strTmpWhere &= " AND (" & strWhere_OR(j) & ")"
                                strTmpField &= strField_OR(j)
                            End If
                        Next
                        strSQL = strBASE(i) & " AND (" & strWHERE(i) & ")" & strTmpWhere
                        strSQL = Replace(strSQL, "<field>", strFieldList(i) & strTmpField)
                    Else
                        strSQL = strBASE(i) & " AND (" & strWHERE(i) & ")"
                        strSQL = Replace(strSQL, "<field>", strFieldList(i))
                    End If

                    lngErrNo = 0
                    strErrMsg = ""

                    strSQL1 = "Select COUNT('') " & Mid(strSQL, InStr(UCase(strSQL), " <FROM> "))
                    strSQL1 = Replace(strSQL1, " <From> ", " FROM ")
                    'dtSubGrpRlt(i) = ExecuteScript(strSQL1, strSource(i), lngErrNo, strErrMsg)
                    wndMain.Cursor = Cursors.WaitCursor

                    Try
                        If Trim(strSource(i)) = "CAPSIL" Then
                            dtSubGrpRlt(i) = objCS.ExecuteScript(strSQL1, strSource(i), lngErrNo, strErrMsg)
                        Else
                            ' ES0001
                            dtSubGrpRlt(i) = ExecuteScript(strSQL1, strSource(i), lngErrNo, strErrMsg)
                        End If

                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try

                    txtViewSQL.Text &= "==============================" & vbCrLf
                    txtViewSQL.Text &= strSQL1 & vbCrLf
                    wndMain.Cursor = Cursors.Default

                    If lngErrNo = 0 Then
                        If dtSubGrpRlt(i).Rows(0)(0) = 0 Then
                            MsgBox("No Record match your criteria, please re-define.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                            blnCancelQ = True
                            Exit Function
                        End If
                        If dtSubGrpRlt(i).Rows(0)(0) > ConfigurationSettings.AppSettings.Item("QLimit") Then
                            MsgBox("Record count is " & dtSubGrpRlt(i).Rows(0)(0) & ", exceed the record limit of " & ConfigurationSettings.AppSettings.Item("QLimit") & ", please re-define your criteria.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                            blnCancelQ = True
                            Exit Function
                        End If
                        If MsgBox("Estimated record count is " & dtSubGrpRlt(i).Rows(0)(0) & ", continue to process?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            blnCancelQ = True
                            Exit Function
                        End If
                    Else
                        MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                        blnCancelQ = True
                        Exit Function
                    End If

                    'dtSubGrpRlt(i) = ExecuteScript(strSQL, strSource(i), lngErrNo, strErrMsg)
                    wndMain.Cursor = Cursors.WaitCursor

                    strSQL = Replace(strSQL, " <From> ", " FROM ")

                    Try
                        If Trim(strSource(i)) = "CAPSIL" Then
                            dtSubGrpRlt(i) = objCS.ExecuteScript(strSQL, strSource(i), lngErrNo, strErrMsg)
                        Else
                            ' ES0001
                            dtSubGrpRlt(i) = ExecuteScript(strSQL, strSource(i), lngErrNo, strErrMsg)
                        End If
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try

                    wndMain.Cursor = Cursors.Default
                    txtViewSQL.Text &= "==============================" & vbCrLf
                    txtViewSQL.Text &= strSQL & vbCrLf

                    If lngErrNo = -1 Then
                        MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                        Exit Function
                    End If
                Next
            End If
        End If

        ' Exec-Type is Script need to run separately
        Dim strScript As String

        With dtCriteriaList.DefaultView
            For i As Integer = 0 To .Count - 1
                ' Get criteria details
                drQuery = dtQuery.Select("crmcpf_field_id = " & .Item(i).Item("ID"))

                If drQuery(0).Item("crmcpf_exec_type") = "S" Then
                    intSourceCnt += 1
                    ReDim Preserve dtSubGrpRlt(intSourceCnt)

                    If .Item(i).Item("ID") = "64" Then
                        strScript = txtCustom.Text
                    Else
                        strScript = drQuery(0).Item("crmcpf_field_detail1") & drQuery(0).Item("crmcpf_field_detail2")
                    End If

                    If Not IsDBNull(drQuery(0).Item("crmcpf_data_type")) AndAlso RTrim(drQuery(0).Item("crmcpf_data_type")) <> "" Then
                        strScript &= " " & RTrim(.Item(i).Item("op")) & " " & Trim(.Item(i).Item("valueM"))
                    End If

                    ' Set parameter
                    If Not IsDBNull(drQuery(0).Item("crmcpf_para_type")) AndAlso RTrim(drQuery(0).Item("crmcpf_para_type")) <> "" Then
                        strScript = Replace(strScript, "<@1>", .Item(i).Item("parameter"))
                    End If

                    lngErrNo = 0
                    strErrMsg = ""
                    'dtSubGrpRlt(intSourceCnt) = ExecuteScript(strScript, drQuery(0).Item("crmcpf_source"), lngErrNo, strErrMsg)

                    wndMain.Cursor = Cursors.WaitCursor

                    If Trim(drQuery(0).Item("crmcpf_source")) = "CAPSIL" Then
                        dtSubGrpRlt(intSourceCnt) = objCS.ExecuteScript(strScript, drQuery(0).Item("crmcpf_source"), lngErrNo, strErrMsg)
                    Else
                        ' ES0001
                        dtSubGrpRlt(intSourceCnt) = ExecuteScript(strScript, drQuery(0).Item("crmcpf_source"), lngErrNo, strErrMsg)
                    End If

                    wndMain.Cursor = Cursors.Default
                    txtViewSQL.Text &= "==============================" & vbCrLf
                    txtViewSQL.Text &= strScript & vbCrLf

                    If lngErrNo = -1 Then
                        MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                        Exit Function
                    End If
                End If
            Next
        End With

        If Not dtSubGrpRlt Is Nothing AndAlso dtSubGrpRlt.Length > 0 Then
            dtResultSet = dtSubGrpRlt(0).Copy

            If intSourceCnt > 0 Then
                If strCurLType = "OR" Then
                    ' Combine result using OR within each group				
                    For j As Integer = 1 To intSourceCnt
                        ' copy column
                        For k As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
                            Try
                                dtResultSet.Columns.Add(dtSubGrpRlt(j).Columns(k).columnname, dtSubGrpRlt(j).Columns(k).datatype)
                            Catch ex As Exception
                                ' Duplicate column name
                                ' Do nothing
                            End Try
                        Next
                        ' Check key and copy value
                        For k As Integer = 0 To dtSubGrpRlt(j).Rows.Count - 1
                            drFind = dtResultSet.Select("CustomerID = " & dtSubGrpRlt(j).Rows(k).item("CustomerID"))
                            If drFind.Length = 0 Then
                                drTemp = dtResultSet.NewRow

                                For col As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
                                    drTemp.Item(dtSubGrpRlt(j).Columns(col).ColumnName) = dtSubGrpRlt(j).Rows(k).item(col)
                                Next
                                dtResultSet.Rows.Add(drTemp)
                            Else
                                For col As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
                                    drFind(0).Item(dtSubGrpRlt(j).Columns(col).ColumnName) = dtSubGrpRlt(j).Rows(k).item(col)
                                Next
                            End If
                        Next
                        dtResultSet.AcceptChanges()
                    Next
                Else
                    ' ** AND **
                    For j As Integer = 1 To intSourceCnt
                        'copy column
                        For k As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
                            Try
                                dtResultSet.Columns.Add(dtSubGrpRlt(j).columns(k).columnname, dtSubGrpRlt(j).columns(k).datatype)
                            Catch ex As Exception
                                ' duplicate column name
                                ' nothing to do!
                            End Try
                        Next
                    Next

                    ' Check if key match in all tables
                    Dim blnMatch As Boolean
                    For j As Integer = 0 To dtResultSet.Rows.Count - 1
                        blnMatch = True
                        For k As Integer = 1 To intSourceCnt
                            drFind = dtSubGrpRlt(k).select("CustomerID = " & dtResultSet.Rows(j).item("CustomerID"))
                            If drFind.Length = 0 Then
                                blnMatch = False
                            End If
                        Next

                        ' if all match
                        If blnMatch = True Then
                            'drTemp = dtResultSet.NewRow
                            ' copy value for each table
                            For k As Integer = 1 To intSourceCnt
                                drFind = dtSubGrpRlt(k).select("CustomerID = " & dtResultSet.Rows(j).item("CustomerID"))
                                If drFind.Length <> 0 Then
                                    For l As Integer = 0 To dtSubGrpRlt(k).columns.count - 1
                                        'drTemp.Item(dtSubGrpRlt(k).columns(l).columnname) = drFind(0).Item(l)
                                        dtResultSet.Rows(j).Item(dtSubGrpRlt(k).columns(l).columnname) = drFind(0).Item(l)
                                    Next
                                End If
                            Next
                            'dtSubGrpRlt(j).rows.add(drTemp)
                        Else
                            dtResultSet.Rows(j).Delete()
                        End If
                    Next
                    dtResultSet.AcceptChanges()
                End If
            End If

            ' Append product name
            Dim blnAppProd As Boolean = False

            drOutput = dtOutput.Select("WithCri = 'N' and Done = 'N'")
            For i As Integer = 0 To drOutput.Length - 1
                'Get criteria details
                drQuery = dtQuery.Select("crmcpf_field_id = " & drOutput(i).Item("ID"))

                If drQuery(0).Item("crmcpf_exec_type") = "S" Then
                    intSourceCnt += 1
                    ReDim Preserve dtSubGrpRlt(intSourceCnt)

                    wndMain.Cursor = Cursors.WaitCursor
                    strScript = drQuery(0).Item("crmcpf_field_detail1") & drQuery(0).Item("crmcpf_field_detail2")

                    If Trim(drQuery(0).Item("crmcpf_source")) = "CAPSIL" Then
                        dtSubGrpRlt(intSourceCnt) = objCS.ExecuteScript(strScript, drQuery(0).Item("crmcpf_source"), lngErrNo, strErrMsg)
                    Else
                        ' ES0001
                        dtSubGrpRlt(intSourceCnt) = ExecuteScript(strScript, drQuery(0).Item("crmcpf_source"), lngErrNo, strErrMsg)
                    End If

                    blnAppProd = True

                    wndMain.Cursor = Cursors.Default
                    txtViewSQL.Text &= "==============================" & vbCrLf
                    txtViewSQL.Text &= strScript & vbCrLf

                    If lngErrNo = -1 Then
                        MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                        Exit Function
                    End If

                    dtOutput.DefaultView.RowFilter = "ID = " & drOutput(i).Item("ID")
                    dtOutput.DefaultView.Item(0).Item("Done") = "Y"
                    dtOutput.AcceptChanges()
                End If
            Next
            dtOutput.DefaultView.RowFilter = ""

            If blnAppProd Then
                For i As Integer = 0 To dtSubGrpRlt(intSourceCnt).Columns.Count - 1
                    Try
                        dtResultSet.Columns.Add(dtSubGrpRlt(intSourceCnt).Columns(i).columnname, dtSubGrpRlt(intSourceCnt).Columns(i).datatype)
                    Catch ex As Exception
                    End Try
                Next

                For i As Integer = 0 To dtResultSet.Rows.Count - 1
                    drFind = dtSubGrpRlt(intSourceCnt).Select("ProductID = '" & dtResultSet.Rows(i).item("ProductID") & "'")
                    If drFind.Length <> 0 Then
                        dtResultSet.Rows(i).Item("ProdName") = drFind(0).Item("ProdName")
                    End If
                Next
                dtResultSet.AcceptChanges()
            End If

        End If

        Return dtResultSet

    End Function

    'Private Function RunCriteria(ByVal strCurLType As String, ByVal strIDList As String, ByVal dtCriteriaList As DataTable) As DataTable

    '    Dim drCriteria As DataRow()
    '    Dim drQuery As DataRow()

    '    Dim strFieldList As String()
    '    Dim strSource As String()
    '    Dim strFROM As String()
    '    Dim strWHERE As String()
    '    Dim strBASE As String()
    '    Dim intSourceCnt As Integer

    '    Dim dtResultSet As DataTable
    '    Dim strCurOp, strPrvOp As String
    '    Dim dtSubGrpRlt As DataTable()
    '    Dim drFind As DataRow()
    '    Dim drTemp As DataRow

    '    Dim strOP As String
    '    Dim blnOR As Boolean

    '    ' Process List of ID together
    '    dtCriteriaList.DefaultView.RowFilter = "ID IN (" & strIDList & ")"
    '    dtCriteriaList.DefaultView.Sort = "source, exectype"

    '    intSourceCnt = -1
    '    blnOR = False

    '    If strCurLType = "OR" Then
    '        strOP = " OR "
    '    Else
    '        strOP = " AND "
    '    End If

    '    ' Process Criteria
    '    With dtCriteriaList.DefaultView
    '        For i As Integer = 0 To .Count - 1

    '            'Get criteria details
    '            drQuery = dtQuery.Select("crmcpf_field_id = " & .Item(i).Item("ID"))

    '            If intSourceCnt < 0 OrElse strSource(intSourceCnt) <> .Item(i).Item("source") Then
    '                If drQuery(0).Item("crmcpf_exec_type") = "F" Or drQuery(0).Item("crmcpf_exec_type") = "U" Then

    '                    intSourceCnt += 1
    '                    ReDim Preserve strSource(intSourceCnt)
    '                    ReDim Preserve strBASE(intSourceCnt)
    '                    ReDim Preserve strFieldList(intSourceCnt)
    '                    ReDim Preserve strWHERE(intSourceCnt)

    '                    strSource(intSourceCnt) = .Item(i).Item("source")
    '                    drCriteria = dtCategory.Select("crmcqc_category=" & drQuery(0).Item("crmcpf_category"))
    '                    strBASE(intSourceCnt) = drCriteria(0).Item("crmcqc_base_sql")

    '                    ' Confirm later
    '                    If drQuery(0).Item("crmcpf_category") = "03" Then
    '                        If radPH.Checked Then strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'O'")
    '                        If radPI.Checked Then
    '                            strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'I'")
    '                            ' If base on insured, need to join with rider no.
    '                            strBASE(intSourceCnt) &= " And COTRNU = FLD0005 "
    '                        End If
    '                    End If

    '                    If drQuery(0).Item("crmcpf_category") = "03" OrElse drQuery(0).Item("crmcpf_category") = "07" Then
    '                        If chkDISTINCT.Checked = True Then
    '                            strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "SELECT ", "SELECT DISTINCT ")
    '                        End If
    '                    End If

    '                    strFieldList(intSourceCnt) = ""
    '                    strWHERE(intSourceCnt) = ""
    '                End If
    '            Else
    '                If Trim(strBASE(intSourceCnt)) = "" Then
    '                    drCriteria = dtCategory.Select("crmcqc_category=" & drQuery(0).Item("crmcpf_category"))
    '                    strBASE(intSourceCnt) = drCriteria(0).Item("crmcqc_base_sql")

    '                    ' Confirm later
    '                    If drQuery(0).Item("crmcpf_category") = "03" Then
    '                        If radPH.Checked Then strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'O'")
    '                        If radPI.Checked Then
    '                            strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "<@1>", "'I'")
    '                            ' If base on insured, need to join with rider no.
    '                            strBASE(intSourceCnt) &= " And COTRNU = FLD0005 "
    '                        End If
    '                    End If

    '                    If drQuery(0).Item("crmcpf_category") = "03" OrElse drQuery(0).Item("crmcpf_category") = "07" Then
    '                        If chkDISTINCT.Checked = True Then
    '                            strBASE(intSourceCnt) = Replace(strBASE(intSourceCnt), "SELECT ", "SELECT DISTINCT ")
    '                        End If
    '                    End If
    '                End If
    '            End If

    '            With drQuery(0)
    '                Select Case .Item("crmcpf_exec_type")
    '                    Case "F"
    '                        ' First Where Clause, no logical operator
    '                        If strWHERE(intSourceCnt) = "" Then
    '                            strWHERE(intSourceCnt) &= RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name")) & _
    '                                    " " & RTrim(dtCriteriaList.DefaultView.Item(i).Item("op")) & " " & Trim(dtCriteriaList.DefaultView.Item(i).Item("valueM"))
    '                        Else
    '                            strWHERE(intSourceCnt) &= strOP & RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name")) & _
    '                                    " " & RTrim(dtCriteriaList.DefaultView.Item(i).Item("op")) & " " & Trim(dtCriteriaList.DefaultView.Item(i).Item("valueM"))
    '                        End If

    '                        ' Append to field list as well
    '                        strFieldList(intSourceCnt) &= ", " & RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name"))
    '                    Case "U"
    '                        ' First Where clause, no logical operator
    '                        If strWHERE(intSourceCnt) = "" Then
    '                            strWHERE(intSourceCnt) &= RTrim(.Item("crmcpf_field_detail1")) & " " & RTrim(.Item("crmcpf_field_detail2"))
    '                        Else
    '                            strWHERE(intSourceCnt) &= strOP & RTrim(.Item("crmcpf_field_detail1")) & " " & RTrim(.Item("crmcpf_field_detail2"))
    '                        End If

    '                        ' If there is an operator & value to check
    '                        If Not IsDBNull(.Item("crmcpf_data_type")) AndAlso RTrim(.Item("crmcpf_data_type")) <> "" Then
    '                            strWHERE(intSourceCnt) &= " " & RTrim(dtCriteriaList.DefaultView.Item(i).Item("op")) & " " & Trim(dtCriteriaList.DefaultView.Item(i).Item("valueM"))
    '                        End If

    '                        ' If the criteria can have an output
    '                        If Not IsDBNull(.Item("crmcpf_field_name")) AndAlso RTrim(.Item("crmcpf_field_name")) <> "" Then
    '                            strFieldList(intSourceCnt) &= ", " & RTrim(.Item("crmcpf_field_detail1")) & _
    '                                " " & RTrim(.Item("crmcpf_field_detail2")) & " As " & RTrim(.Item("crmcpf_field_name"))
    '                        End If

    '                        ' Apply parameter to the clause
    '                        If Not IsDBNull(.Item("crmcpf_para_type")) AndAlso RTrim(.Item("crmcpf_para_type")) <> "" Then
    '                            strWHERE(intSourceCnt) = Replace(strWHERE(intSourceCnt), "<@1>", dtCriteriaList.DefaultView.Item(i).Item("parameter"))
    '                            strFieldList(intSourceCnt) = Replace(strFieldList(intSourceCnt), "<@1>", dtCriteriaList.DefaultView.Item(i).Item("parameter"))
    '                        End If

    '                End Select
    '            End With
    '        Next
    '    End With

    '    ' Process additional fields not in the criteria list
    '    Dim strOField() As String
    '    Dim strOBase() As String
    '    Dim strOSource() As String
    '    Dim drOutput() As DataRow

    '    Dim intOutCnt As Integer = -1

    '    drOutput = dtOutput.Select("WithCri = 'N' and Done = 'N'")
    '    For i As Integer = 0 To drOutput.Length - 1
    '        'Get criteria details
    '        drQuery = dtQuery.Select("crmcpf_field_id = " & drOutput(i).Item("ID"))

    '        Dim intFLoc As Integer
    '        intFLoc = -1
    '        For c As Integer = 0 To strSource.Length - 1
    '            If Trim(drQuery(0).Item("crmcpf_source")) = Trim(strSource(c)) Then
    '                intFLoc = c
    '            End If
    '        Next

    '        If intFLoc >= 0 Then
    '            With drQuery(0)
    '                ' Update existing sql
    '                Select Case .Item("crmcpf_exec_type")
    '                    Case "F"
    '                        strFieldList(intFLoc) &= ", " & RTrim(.Item("crmcpf_table")) & "." & RTrim(.Item("crmcpf_field_name"))
    '                    Case "U"
    '                        ' If the criteria can have an output
    '                        If Not IsDBNull(.Item("crmcpf_field_name")) AndAlso RTrim(.Item("crmcpf_field_name")) <> "" Then
    '                            strFieldList(intFLoc) &= ", " & RTrim(.Item("crmcpf_field_detail1")) & _
    '                                " " & RTrim(.Item("crmcpf_field_detail2")) & " As " & RTrim(.Item("crmcpf_field_name"))
    '                        End If

    '                        '' Apply parameter to the clause
    '                        'If Not IsDBNull(.Item("crmcpf_para_type")) AndAlso RTrim(.Item("crmcpf_para_type")) <> "" Then
    '                        '    strFieldList(intFLoc) = Replace(strFieldList(intSourceCnt), "<@1>", dtCriteriaList.DefaultView.Item(i).Item("parameter"))
    '                        'End If
    '                End Select
    '            End With
    '            dtOutput.DefaultView.RowFilter = "ID = " & drOutput(i).Item("ID")
    '            dtOutput.DefaultView.Item(0).Item("Done") = "Y"
    '            dtOutput.AcceptChanges()
    '        End If
    '    Next

    '    Dim strSQL, strSQL1, strErrMsg As String
    '    Dim lngErrNo As Long

    '    ' Check if any criteria need to run
    '    If intSourceCnt >= 0 Then
    '        ReDim dtSubGrpRlt(intSourceCnt)
    '        For i As Integer = 0 To intSourceCnt
    '            strSQL = strBASE(i) & " AND (" & strWHERE(i) & ")"
    '            strSQL = Replace(strSQL, "<field>", strFieldList(i))
    '            lngErrNo = 0
    '            strErrMsg = ""

    '            strSQL1 = "Select COUNT('') " & Mid(strSQL, InStr(UCase(strSQL), " FROM "))
    '            'dtSubGrpRlt(i) = ExecuteScript(strSQL1, strSource(i), lngErrNo, strErrMsg)
    '            wndMain.Cursor = Cursors.WaitCursor
    '            dtSubGrpRlt(i) = objCS.ExecuteScript(strSQL1, strSource(i), lngErrNo, strErrMsg)
    '            wndMain.Cursor = Cursors.Default

    '            If lngErrNo = 0 Then
    '                If dtSubGrpRlt(i).Rows(0)(0) = 0 Then
    '                    MsgBox("No Record match your criteria, please re-define.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
    '                    blnCancelQ = True
    '                    Exit Function
    '                End If
    '                If MsgBox("Estimated record count is " & dtSubGrpRlt(i).Rows(0)(0) & ", continue to process?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
    '                    blnCancelQ = True
    '                    Exit Function
    '                End If
    '            Else
    '                MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
    '                Exit Function
    '            End If

    '            'dtSubGrpRlt(i) = ExecuteScript(strSQL, strSource(i), lngErrNo, strErrMsg)
    '            wndMain.Cursor = Cursors.WaitCursor
    '            dtSubGrpRlt(i) = objCS.ExecuteScript(strSQL, strSource(i), lngErrNo, strErrMsg)
    '            wndMain.Cursor = Cursors.Default
    '            txtViewSQL.Text &= "==============================" & vbCrLf
    '            txtViewSQL.Text &= strSQL & vbCrLf

    '            If lngErrNo = -1 Then
    '                MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
    '                Exit Function
    '            End If
    '        Next
    '    End If

    '    ' Exec-Type is Script need to run separately
    '    Dim strScript As String

    '    With dtCriteriaList.DefaultView
    '        For i As Integer = 0 To .Count - 1
    '            ' Get criteria details
    '            drQuery = dtQuery.Select("crmcpf_field_id = " & .Item(i).Item("ID"))

    '            If drQuery(0).Item("crmcpf_exec_type") = "S" Then
    '                intSourceCnt += 1
    '                ReDim Preserve dtSubGrpRlt(intSourceCnt)

    '                If .Item(i).Item("ID") = "64" Then
    '                    strScript = txtCustom.Text
    '                Else
    '                    strScript = drQuery(0).Item("crmcpf_field_detail1") & drQuery(0).Item("crmcpf_field_detail2")
    '                End If

    '                If Not IsDBNull(drQuery(0).Item("crmcpf_data_type")) AndAlso RTrim(drQuery(0).Item("crmcpf_data_type")) <> "" Then
    '                    strScript &= " " & RTrim(.Item(i).Item("op")) & " " & Trim(.Item(i).Item("valueM"))
    '                End If

    '                ' Set parameter
    '                If Not IsDBNull(drQuery(0).Item("crmcpf_para_type")) AndAlso RTrim(drQuery(0).Item("crmcpf_para_type")) <> "" Then
    '                    strScript = Replace(strScript, "<@1>", .Item(i).Item("parameter"))
    '                End If

    '                lngErrNo = 0
    '                strErrMsg = ""
    '                'dtSubGrpRlt(intSourceCnt) = ExecuteScript(strScript, drQuery(0).Item("crmcpf_source"), lngErrNo, strErrMsg)
    '                wndMain.Cursor = Cursors.WaitCursor
    '                dtSubGrpRlt(intSourceCnt) = objCS.ExecuteScript(strScript, drQuery(0).Item("crmcpf_source"), lngErrNo, strErrMsg)
    '                wndMain.Cursor = Cursors.Default
    '                txtViewSQL.Text &= "==============================" & vbCrLf
    '                txtViewSQL.Text &= strScript & vbCrLf

    '                If lngErrNo = -1 Then
    '                    MsgBox("Error running criteria, additional error message:" & vbCrLf & strErrMsg, MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
    '                    Exit Function
    '                End If
    '            End If
    '        Next
    '    End With

    '    If Not dtSubGrpRlt Is Nothing AndAlso dtSubGrpRlt.Length > 0 Then
    '        dtResultSet = dtSubGrpRlt(0).Copy

    '        If intSourceCnt > 0 Then
    '            If strCurLType = "OR" Then
    '                ' Combine result using OR within each group				
    '                For j As Integer = 1 To intSourceCnt
    '                    ' copy column
    '                    For k As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
    '                        Try
    '                            dtResultSet.Columns.Add(dtSubGrpRlt(j).Columns(k).columnname, dtSubGrpRlt(j).Columns(k).datatype)
    '                        Catch ex As Exception
    '                            ' Duplicate column name
    '                            ' Do nothing
    '                        End Try
    '                    Next
    '                    ' Check key and copy value
    '                    For k As Integer = 0 To dtSubGrpRlt(j).Rows.Count - 1
    '                        drFind = dtResultSet.Select("CustomerID = " & dtSubGrpRlt(j).Rows(k).item("CustomerID"))
    '                        If drFind.Length = 0 Then
    '                            drTemp = dtResultSet.NewRow

    '                            For col As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
    '                                drTemp.Item(dtSubGrpRlt(j).Columns(col).ColumnName) = dtSubGrpRlt(j).Rows(k).item(col)
    '                            Next
    '                            dtResultSet.Rows.Add(drTemp)
    '                        Else
    '                            For col As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
    '                                drFind(0).Item(dtSubGrpRlt(j).Columns(col).ColumnName) = dtSubGrpRlt(j).Rows(k).item(col)
    '                            Next
    '                        End If
    '                    Next
    '                    dtResultSet.AcceptChanges()
    '                Next
    '            Else
    '                ' ** AND **
    '                For j As Integer = 1 To intSourceCnt
    '                    'copy column
    '                    For k As Integer = 0 To dtSubGrpRlt(j).Columns.Count - 1
    '                        Try
    '                            dtResultSet.Columns.Add(dtSubGrpRlt(j).columns(k).columnname, dtSubGrpRlt(j).columns(k).datatype)
    '                        Catch ex As Exception
    '                            ' duplicate column name
    '                            ' nothing to do!
    '                        End Try
    '                    Next
    '                Next

    '                ' Check if key match in all tables
    '                Dim blnMatch As Boolean
    '                For j As Integer = 0 To dtResultSet.Rows.Count - 1
    '                    blnMatch = True
    '                    For k As Integer = 1 To intSourceCnt
    '                        drFind = dtSubGrpRlt(k).select("CustomerID = " & dtResultSet.Rows(j).item("CustomerID"))
    '                        If drFind.Length = 0 Then
    '                            blnMatch = False
    '                        End If
    '                    Next

    '                    ' if all match
    '                    If blnMatch = True Then
    '                        'drTemp = dtResultSet.NewRow
    '                        ' copy value for each table
    '                        For k As Integer = 1 To intSourceCnt
    '                            drFind = dtSubGrpRlt(k).select("CustomerID = " & dtResultSet.Rows(j).item("CustomerID"))
    '                            If drFind.Length <> 0 Then
    '                                For l As Integer = 0 To dtSubGrpRlt(k).columns.count - 1
    '                                    'drTemp.Item(dtSubGrpRlt(k).columns(l).columnname) = drFind(0).Item(l)
    '                                    dtResultSet.Rows(j).Item(dtSubGrpRlt(k).columns(l).columnname) = drFind(0).Item(l)
    '                                Next
    '                            End If
    '                        Next
    '                        'dtSubGrpRlt(j).rows.add(drTemp)
    '                    Else
    '                        dtResultSet.Rows(j).Delete()
    '                    End If
    '                Next
    '                dtResultSet.AcceptChanges()
    '            End If
    '        End If
    '    End If
    '    Return dtResultSet

    'End Function

    Private Function ExecuteScript(ByVal strScript As String, ByVal strSource As String, ByRef lngErrNo As Long, ByRef strErrMsg As String) As DataTable

        '''''''Dim oledbconnect As New OleDbConnection
        ''''Dim daExec As OleDbDataAdapter
        ''''Dim strSQL, strUser, strPwd As String
        ''''Dim dtExec As DataTable

        ''''Dim strConn As String()
        ''''strConn = strCAPSILConn.Split(";")

        ''''For i As Integer = 0 To strConn.Length - 1
        ''''    If strConn(i) Like "UID*" Then
        ''''        strUser = Mid(strConn(i), InStr(strConn(i), "=") + 1)
        ''''    End If
        ''''    If strConn(i) Like "PWD*" Then
        ''''        strPwd = Mid(strConn(i), InStr(strConn(i), "=") + 1)
        ''''    End If
        ''''Next

        '''''''oledbconnect.ConnectionString = _
        '''''''    "Provider=IBMDA400.DataSource.1;Persist Security Info=False;" & _
        '''''''    "User ID=" & strUser & ";" & _
        '''''''    "Data Source=" & ConfigurationSettings.AppSettings.Item("AS400") & ";" & _
        '''''''    "Password=" & strPwd & ";" & _
        '''''''    "Initial Catalog='';Transport Product=Client Access;SSL=DEFAULT;"

        ''''strScript = Replace(strScript, "ORDUPO", ORDUPO)
        ''''strScript = Replace(strScript, "ORDUCO", ORDUCO)
        ''''strScript = Replace(strScript, "ORDUNA", ORDUNA)
        ''''strScript = Replace(strScript, "ORDCNA", ORDCNA)
        ''''strScript = Replace(strScript, "ORDURL", ORDURL)
        ''''strScript = Replace(strScript, "ORDUET", ORDUET)

        ''''Dim objDBS As Object
        ''''Dim rst As ADODB.Recordset
        ''''Dim str400 As String = ConfigurationSettings.AppSettings.Item("AS400")

        ''''Try
        ''''    objDBS = CreateObject("dbsecurity.database")
        ''''    objDBS.connect(str400, "POL", "ENQ400LIFE", "ENQUIRY")
        ''''    rst = objDBS.executestatement(strScript)
        ''''Catch ex As Exception
        ''''    lngErrNo = -1
        ''''    strErrMsg = ex.ToString
        ''''    Return Nothing
        ''''Finally
        ''''    If Not objDBS Is Nothing Then
        ''''        If objDBS.isconnected = True Then
        ''''            objDBS.disconnect()
        ''''        End If
        ''''        objDBS = Nothing
        ''''    End If
        ''''End Try

        ''''Try
        ''''    '''daExec = New OleDbDataAdapter(strScript, oledbconnect)
        ''''    'daExec.MissingSchemaAction = MissingSchemaAction.AddWithKey
        ''''    'daExec.MissingMappingAction = MissingMappingAction.Passthrough
        ''''    daExec = New OleDbDataAdapter
        ''''    dtExec = New DataTable
        ''''    daExec.Fill(dtExec, rst)

        ''''Catch sqlex As OdbcException
        ''''    lngErrNo = -1
        ''''    strErrMsg = sqlex.ToString
        ''''    Return Nothing
        ''''Catch ex As Exception
        ''''    lngErrNo = -1
        ''''    strErrMsg = ex.ToString
        ''''    Return Nothing
        ''''End Try

        ''''Return dtExec
        Dim sqlda As SqlDataAdapter
        Dim sqlConnect As SqlConnection

        Dim odbcda As OdbcDataAdapter
        Dim odbcConnect As OdbcConnection

        Dim dtTempResult As DataTable

        wndMain.Cursor = Cursors.WaitCursor

        Select Case RTrim(strSource)

            Case "CAPSIL", "CAPSIL1", "CAPSIL2", "CAPSIL3"
                odbcConnect = New OdbcConnection
                odbcConnect.ConnectionString = strCAPSILConn & ";Connect Timeout=240"
                odbcda = New OdbcDataAdapter(strScript, odbcConnect)
                dtTempResult = New DataTable

                Try
                    odbcda.Fill(dtTempResult)
                Catch sqlex As OdbcException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "CAM", "CAM1"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strCIWConn
                sqlda = New SqlDataAdapter(strScript, sqlConnect)
                sqlda.SelectCommand.CommandTimeout = 0

                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "CIW", "CUST", "CIW2", "CIW3", "CIW4", "CIW5"
                sqlConnect = New SqlConnection
                sqlConnect.ConnectionString = strCIWConn & ";Connect Timeout=240"
                sqlda = New SqlDataAdapter(strScript, sqlConnect)
                sqlda.SelectCommand.CommandTimeout = 0

                dtTempResult = New DataTable

                Try
                    sqlda.Fill(dtTempResult)
                Catch sqlex As SqlException
                    lngErrNo = -1
                    strErrMsg = sqlex.ToString
                Catch ex As Exception
                    lngErrNo = -1
                    strErrMsg = ex.ToString
                End Try

            Case "MCS", "MCS1"

        End Select

        sqlda = Nothing
        odbcda = Nothing
        sqlConnect = Nothing
        odbcConnect = Nothing

        wndMain.Cursor = Cursors.Default

        Return dtTempResult

    End Function

    Private Sub cmdAddCr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCr.Click

        Dim dr As DataRow
        Dim strID As String

        If Not CStr(tvAllFields.SelectedNode.Tag).EndsWith("C") Then

            If CStr(tvAllFields.SelectedNode.Tag).EndsWith("P") Then
                strID = Mid(tvAllFields.SelectedNode.Tag, 1, Len(tvAllFields.SelectedNode.Tag) - 1)
            Else
                strID = tvAllFields.SelectedNode.Tag
            End If

            If CheckDup(strID, "C") Then
                Exit Sub
            End If

            dr = dtCriteriaList.NewRow
            dr.Item("ID") = strID
            dr.Item("Criteria") = tvAllFields.SelectedNode.Text
            dr.Item("Op") = ""
            dr.Item("Value") = ""
            dr.Item("Parameter") = ""
            dr.Item("source") = ""
            dr.Item("exectype") = ""
            dr.Item("level") = ""
            dr.Item("logicop") = "AND"
            dtCriteriaList.Rows.Add(dr)
            dtCriteriaList.AcceptChanges()

            If lstOutput.FindString(tvAllFields.SelectedNode.Text) = -1 Then
                dr = dtOutput.NewRow
                dr.Item("ID") = strID
                dr.Item("Criteria") = tvAllFields.SelectedNode.Text
                dr.Item("WithCri") = "Y"
                dr.Item("Done") = "N"
                dtOutput.Rows.Add(dr)
                lstOutput.Items.Add(tvAllFields.SelectedNode.Text)
                dtOutput.AcceptChanges()
            End If

        End If

    End Sub

    Private Sub cmdRemoveCr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveCr.Click

        Dim intFldID As Integer

        intFldID = CType(bm.Current, DataRowView).Row.Item("ID")

        For i As Integer = 0 To dtOutput.Rows.Count - 1
            If dtOutput.Rows(i).Item("ID") = intFldID Then
                dtOutput.Rows(i).Delete()
                lstOutput.Items.RemoveAt(i)
                dtOutput.AcceptChanges()
                Exit For
            End If
        Next
        CType(bm.Current, DataRowView).Row.Delete()
        dtCriteriaList.AcceptChanges()

    End Sub

    Private Sub cmdAddFld_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFld.Click

        If Not CStr(tvAllFields.SelectedNode.Tag).EndsWith("C") Then
            If Not CStr(tvAllFields.SelectedNode.Tag).EndsWith("P") Then

                If CheckDup(tvAllFields.SelectedNode.Tag, "F") Then
                    Exit Sub
                End If

                Dim dr As DataRow
                dr = dtOutput.NewRow

                dr.Item("ID") = tvAllFields.SelectedNode.Tag
                dr.Item("Criteria") = tvAllFields.SelectedNode.Text
                dr.Item("WithCri") = "N"
                dr.Item("Done") = "N"
                dtOutput.Rows.Add(dr)
                dtOutput.AcceptChanges()
                lstOutput.Items.Add(tvAllFields.SelectedNode.Text)

            End If
        End If
    End Sub

    Private Sub cmdRemoveFld_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveFld.Click

        For i As Integer = 0 To dtOutput.Rows.Count - 1
            If dtOutput.Rows(i).Item("Criteria") = lstOutput.SelectedItem Then
                dtOutput.Rows(i).Delete()
                lstOutput.Items.RemoveAt(i)
                dtOutput.AcceptChanges()
                Exit For
            End If
        Next
        'lstOutput.Items.RemoveAt(lstOutput.SelectedIndex)

    End Sub

    Private Sub cmdImportList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportList.Click

        Dim f As New frmImportList
        Dim strPath As String

        strDataMode = "I"

        OpenFileDialog1.Title = "Import Client List"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.DefaultExt = ".csv"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Filter = "Import files (*.csv)|*.csv"

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = OpenFileDialog1.FileName
        Else
            Exit Sub
        End If

        strSource = InputBox("Please Enter the source of information", "Import Client List")

        If strSource = "" Then
            MsgBox("Invalid Source of Information!", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
            Exit Sub
        End If

        Dim DataFile As FileInfo = New FileInfo(strPath)
        Dim cnCSV As OdbcConnection
        Dim daCSV As OdbcDataAdapter
        Dim dt As DataTable = New DataTable
        Dim blnValidFmt As Boolean = True

        cnCSV = New OdbcConnection("Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" & DataFile.Directory.FullName & ";")
        daCSV = New OdbcDataAdapter("SELECT * FROM [" & DataFile.Name & "]", cnCSV)
        daCSV.Fill(dt)

        If dt.Columns("CustomerID") Is Nothing Then blnValidFmt = False
        If dt.Columns("HKID") Is Nothing Then blnValidFmt = False
        If dt.Columns("LastName") Is Nothing Then blnValidFmt = False
        If dt.Columns("FirstName") Is Nothing Then blnValidFmt = False
        If dt.Columns("Email") Is Nothing Then blnValidFmt = False
        If dt.Columns("DOB") Is Nothing Then blnValidFmt = False
        If dt.Columns("Gender") Is Nothing Then blnValidFmt = False

        If blnValidFmt Then
            dtFinal = dt.Copy
            Call PrepareDT(dtFinal)
        Else
            MsgBox("Invalid import file layout. The file must include the following columns:" & vbCrLf & _
                "CustomerID, HKID, LastName, FirstName, Email, DOB, Gender", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
            dtFinal = Nothing
            Exit Sub
        End If

        grdResult.DataSource = dtFinal
        lblCount.Text = "Records: " & dtFinal.Rows.Count

        If Not dtFinal Is Nothing Then bmResult = Me.BindingContext(dtFinal)

        ' For audit trail
        strLog = "Import result for Campaign " & cboCampaign.SelectedValue & ", channel " & cboChannel.SelectedValue & vbCrLf
        strLog &= "Total no. of records: " & dtFinal.Rows.Count & vbCrLf
        strLog &= "Import from file: " & strPath & vbCrLf
        strLog &= "Source of information: " & strSource


    End Sub

    Private Sub cmdExportList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExportList.Click

        Dim strFile As String
        Dim csvStream As IO.StreamWriter
        Dim dtFinal1 As DataTable

        If bmResult Is Nothing OrElse bmResult.Count = 0 Then
            MsgBox("Please Run a Query First!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Exit Sub
        End If

        If MsgBox("This list may contain sensitive customer information (e.g. HKID, DOB), and you should export needed information only." & vbCrLf & "Are you sure to export the list?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, gSystem) = MsgBoxResult.No Then
            Exit Sub
        End If

        SaveFileDialog1.Title = "Save Client List"
        SaveFileDialog1.InitialDirectory = "C:\"
        SaveFileDialog1.DefaultExt = ".csv"
        SaveFileDialog1.Filter = "Export files (*.csv)|*.csv"
        SaveFileDialog1.RestoreDirectory = True

        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strFile = SaveFileDialog1.FileName

            'Open the CSV file.
            Try
                csvStream = New IO.StreamWriter(strFile)
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
                Exit Sub
            End Try

            'Use a string builder for efficiency.
            Dim line As New System.Text.StringBuilder

            Dim index As Integer
            'Iterate over all the columns in the table and get the column names.
            'For index = 0 To dtFinal.Columns.Count - 1
            For index = 0 To lstOutput.Items.Count - 1
                If index > 0 Then
                    'Put a comma between column names.
                    line.Append(","c)
                End If

                'Precede and follow each column name with a double quote
                'and escape each double quote with another double quote.
                line.Append(""""c)
                'line.Append(dtFinal.Columns(strCol(0).Item("crmcpf_field_name")).ColumnName.Replace("""", """"""))
                line.Append(lstOutput.Items(index).ToString.Replace("""", """"""))
                line.Append(""""c)
            Next
            'Next

            'Write the line to the file.
            csvStream.WriteLine(line.ToString())

            dtFinal1 = dtFinal.Copy
            dtFinal1.Columns.Add("Dup", Type.GetType("System.String"))

            ' Remove duplicate using HKID, get latest policy if field exist
            If dtFinal.Rows.Count > 0 AndAlso Me.chkDISTINCT.Checked = True Then

                Dim strSort, strCurKey1, strCurKey2, strPrvKey1, strPrvKey2 As String
                Dim blnPol As Boolean

                If Not dtFinal.Columns("PH_HKID") Is Nothing Then
                    strSort = "PH_HKID"
                    If Not dtFinal.Columns("IssueDate") Is Nothing Then
                        strSort &= ",IssueDate DESC"
                    ElseIf Not dtFinal.Columns("IF_Date") Is Nothing Then
                        strSort &= ",IF_Date DESC"
                    End If
                Else
                    strSort = ""
                End If

                If Not dtFinal.Columns("PolicyAccountID") Is Nothing AndAlso strSort <> "" Then

                    blnPol = True
                    dtFinal1.DefaultView.Sort = strSort
                    dtFinal1.DefaultView.RowFilter = ""

                    strPrvKey1 = dtFinal1.DefaultView.Item(0).Item("PH_HKID")
                    strPrvKey2 = dtFinal1.DefaultView.Item(0).Item("PolicyAccountID")

                    Dim dr As DataRowView
                    'For i As Integer = 1 To dtFinal.DefaultView.Count - 1
                    For Each dr In dtFinal1.DefaultView

                        'strCurKey1 = dtFinal.DefaultView.Item(i).Item("PH_HKID")
                        'strCurKey2 = dtFinal.DefaultView.Item(i).Item("PolicyAccountID")
                        strCurKey1 = dr.Item("PH_HKID")
                        strCurKey2 = dr.Item("PolicyAccountID")
                        dr.Item("Dup") = "N"

                        If strPrvKey1 <> strCurKey1 Then
                            'strPrvKey1 = dtFinal.DefaultView.Item(i).Item("PH_HKID")
                            'strPrvKey2 = dtFinal.DefaultView.Item(i).Item("PolicyAccountID")
                            strPrvKey1 = dr.Item("PH_HKID")
                            strPrvKey2 = dr.Item("PolicyAccountID")
                        Else
                            If strPrvKey2 <> strCurKey2 Then
                                'dtFinal.DefaultView.Item(i).Delete()
                                dr.Item("Dup") = "Y"
                            End If
                        End If
                    Next
                End If
            End If

            dtFinal1.DefaultView.RowFilter = "Dup <> 'Y' or Dup is NULL"
            dtFinal1.DefaultView.Sort = ""

            Dim strCol As DataRow()
            Dim strColName As String
            'Iterate over all the rows in the table.
            For Each row As DataRowView In dtFinal1.DefaultView
                'Clear the line of text.
                line.Remove(0, line.Length)

                'Iterate over all the fields in the row and get the field values.
                'For index = 0 To dtFinal.Columns.Count - 1
                For index = 0 To lstOutput.Items.Count - 1
                    If index > 0 Then
                        'Put a comma between field values.
                        line.Append(","c)
                    End If

                    'Precede and follow each field value with a double quote
                    'and escape each double quote with another double quote.
                    line.Append(""""c)
                    For i As Integer = 0 To dtOutput.Rows.Count - 1
                        If dtOutput.Rows(i).Item("Criteria") = lstOutput.Items(index) Then
                            strCol = dtQuery.Select("crmcpf_field_ID = " & dtOutput.Rows(i).Item("ID"))
                            Exit For
                        End If
                    Next

                    If strCol.Length = 0 Then
                        MsgBox("Error writing audit trail entry.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                        Exit Sub
                    Else
                        strColName = strCol(0).Item("crmcpf_field_name")
                    End If

                    ' Mask HKID
                    'If dtFinal.Columns(index).ColumnName = "PH_HKID" OrElse dtFinal.Columns(index).ColumnName = "CNAID" Then
                    If strColName = "PH_HKID" OrElse strColName = "HKID" Then
                        Dim strHKID As String
                        'strHKID = Strings.Left(row.Item(strColName).ToString, 5) & "***"
                        strHKID = "*****"
                        line.Append(strHKID)
                        'line.Append(row(index).ToString().Replace("""", """"""))
                    Else
                        line.Append(row.Item(strColName).ToString().Replace("""", """"""))
                    End If

                    line.Append(""""c)
                Next

                'Write the line to the file.
                csvStream.WriteLine(line.ToString())

            Next

            MsgBox("Export Client List completed successfully.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)

            csvStream.Flush()
            csvStream.Close()

            strLog = "Export result of Campaign " & cboCampaign.SelectedValue & ", channel " & cboChannel.SelectedValue & vbCrLf
            strLog &= "Total no. of records: " & dtFinal.Rows.Count & vbCrLf
            strLog &= "Export to file " & strFile

            If AddAuditTrail("0", "Export List", strLog) = False Then
                MsgBox("Error writing audit trail entry.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
            End If

        End If

    End Sub

    Private Sub cmdSaveResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveResult.Click

        Dim strSQL As String
        Dim strVal As String
        Dim sqlCmd As New SqlCommand
        Dim sqlconnect As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If bmResult Is Nothing OrElse bmResult.Count = 0 Then
            MsgBox("Please Run a Query First!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            Exit Sub
        End If

        If Trim(cboCampaign.SelectedValue) = "" Or Trim(cboChannel.SelectedValue) = "" Then
            MsgBox("Invalid Campaign / Channel, please check", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, gSystem)
            Exit Sub
        End If

        wndMain.Cursor = Cursors.WaitCursor

        ' Delete previous result (if any)
        If DeleteResult() = False Then
            wndMain.Cursor = Cursors.Default
            Exit Sub
        End If

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()
        sqlCmd.Connection = sqlconnect

        If Me.strDataMode = "I" Then
            ' Delete existing temp records
            strSQL = "Delete From " & serverPrefix & "crm_campaign_sl_temp " & _
                " Where crmcpt_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                " And crmcpt_channel_id = '" & cboChannel.SelectedValue & "'"

            sqlCmd.CommandText = strSQL

            Try
                sqlCmd.ExecuteNonQuery()
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try


            ' Add new records to crm_campaign_sl_temp to improve performance
            strSQL = "Insert " & serverPrefix & "crm_campaign_sl_temp " & _
                " (crmcpt_campaign_id, crmcpt_channel_id, crmcpt_customerid, crmcpt_hkid, crmcpt_lastname, " & _
                "  crmcpt_firstname, crmcpt_title, crmcpt_email, crmcpt_dob, crmcpt_gender) " & _
                " Values "

            For i As Integer = 0 To dtFinal.Rows.Count - 1
                strVal = "('" & cboCampaign.SelectedValue & "','" & cboChannel.SelectedValue & "'," & dtFinal.Rows(i).Item("CustomerID") & ",'" & _
                    dtFinal.Rows(i).Item("HKID") & "','" & dtFinal.Rows(i).Item("LastName") & "','" & dtFinal.Rows(i).Item("FirstName") & "','','" & _
                    dtFinal.Rows(i).Item("Email") & "','" & dtFinal.Rows(i).Item("DOB") & "','" & dtFinal.Rows(i).Item("Gender") & "')"

                sqlCmd.CommandText = strSQL & strVal

                Try
                    sqlCmd.ExecuteNonQuery()
                Catch sqlex As SqlClient.SqlException
                    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Sub
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Sub
                End Try
            Next

            ' Find CustomerID first using hkid+dob+gender (CLient)
            strSQL = "Update " & serverPrefix & "crm_campaign_sl_temp " & _
                " Set crmcpt_customerid = customer.customerid " & _
                " From " & serverPrefix & "crm_campaign_sl_temp left join customer " & _
                " On crmcpt_hkid = customer.governmentidcard " & _
                " And crmcpt_dob = customer.dateofbirth " & _
                " And crmcpt_gender = customer.gender " & _
                " And CustomerTypeCode = 'CL' " & _
                " Where crmcpt_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                " And crmcpt_channel_id = '" & cboChannel.SelectedValue & "'" & _
                " And crmcpt_customerid = 0 " & _
                " and customer.customerid is not null"

            sqlCmd.CommandText = strSQL
            sqlCmd.Connection = sqlconnect

            Try
                sqlCmd.ExecuteNonQuery()
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try

            ' Find CustomerID first using hkid+dob+gender (PC)
            strSQL = "Update " & serverPrefix & "crm_campaign_sl_temp " & _
                " Set crmcpt_customerid = customer.customerid " & _
                " From " & serverPrefix & "crm_campaign_sl_temp left join customer " & _
                " On crmcpt_hkid = customer.governmentidcard " & _
                " And crmcpt_dob = customer.dateofbirth " & _
                " And crmcpt_gender = customer.gender " & _
                " And CustomerTypeCode = 'PC' " & _
                " Where crmcpt_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                " And crmcpt_channel_id = '" & cboChannel.SelectedValue & "'" & _
                " And crmcpt_customerid = 0 " & _
                " and customer.customerid is not null"

            sqlCmd.CommandText = strSQL
            sqlCmd.Connection = sqlconnect

            Try
                sqlCmd.ExecuteNonQuery()
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try

            ' Create customer if not found
            Dim sqlreader As SqlDataReader
            strSQL = "Select * " & _
                " From " & serverPrefix & "crm_campaign_sl_temp " & _
                " Where crmcpt_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                " And crmcpt_channel_id = '" & cboChannel.SelectedValue & "'" & _
                " And crmcpt_customerid = 0"

            sqlCmd.CommandText = strSQL
            sqlCmd.Connection = sqlconnect

            Try
                sqlreader = sqlCmd.ExecuteReader
            Catch sqlex As SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try

            If sqlreader.HasRows Then

                Dim sqlconnect1 As New SqlConnection
                Dim sqlcmd1 As New SqlCommand

                sqlconnect1.ConnectionString = strCIWConn
                sqlconnect1.Open()

                While sqlreader.Read
                    ' Get customerID
                    ' Write to Customer, type = 'PC'
                    strSQL = "Declare @custID as numeric " & _
                        " Select @custID = max(customerid)+1 from customer where customerid > 90000000 and customerid < 99999999 " & _
                        " Insert Into Customer (CustomerID, CustomerTypeCode, NamePrefix, FirstName, NameSuffix, DateOfBirth, Gender, GovernmentIDCard, " & _
                        "                       DateofLastChange, EmailAddr, CreateDate, CreateUser, LstUpdDate, LstUpdUser, CompanyID) " & _
                        " Select @custID, 'PC', '" & sqlreader.Item("crmcpt_title") & "', '" & _
                        sqlreader.Item("crmcpt_lastname") & "', '" & _
                        sqlreader.Item("crmcpt_firstname") & "', '" & _
                        sqlreader.Item("crmcpt_dob") & "', '" & _
                        sqlreader.Item("crmcpt_gender") & "', '" & _
                        sqlreader.Item("crmcpt_hkid") & "', GETDATE(), '" & _
                        sqlreader.Item("crmcpt_email") & "', GETDATE(), '" & gsUser & "', GETDATE(), '" & gsUser & "', 'EAA'"

                    strSQL &= " Insert Into " & serverPrefix & "csw_misc_info (cswmif_type, cswmif_customer_id, cswmif_provider, cswmif_create_user, cswmif_create_date, cswmif_update_user, cswmif_update_date) " &
                        " Select 'EXTCL', @custID, '" & RTrim(strSource) & "','" & gsUser & "', GETDATE(), '" & gsUser & "', GETDATE()"

                    sqlcmd1.CommandText = strSQL
                    sqlcmd1.Connection = sqlconnect1

                    Try
                        sqlcmd1.ExecuteNonQuery()
                    Catch sqlex As SqlClient.SqlException
                        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                        Exit Sub
                    Catch ex As Exception
                        MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                        Exit Sub
                    End Try

                    ' For new customer, also write to csw_misc_info the source of information

                End While
                sqlconnect1.Close()
                sqlcmd1 = Nothing
                sqlconnect1 = Nothing
            End If
            sqlreader.Close()

            ' Update again after Customer creation
            ' Find CustomerID first using hkid+dob+gender (PC)
            strSQL = "Update " & serverPrefix & "crm_campaign_sl_temp " & _
                " Set crmcpt_customerid = customer.customerid " & _
                " From " & serverPrefix & "crm_campaign_sl_temp left join customer " & _
                " On crmcpt_hkid = customer.governmentidcard " & _
                " And crmcpt_dob = customer.dateofbirth " & _
                " And crmcpt_gender = customer.gender " & _
                " And CustomerTypeCode = 'PC' " & _
                " Where crmcpt_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                " And crmcpt_channel_id = '" & cboChannel.SelectedValue & "'" & _
                " And crmcpt_customerid = 0 " & _
                " and customer.customerid is not null"

            sqlCmd.CommandText = strSQL

            Try
                sqlCmd.ExecuteNonQuery()
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try

            ' Insert into crm_campaign_sales_leads
            strSQL = "Insert Into " & serverPrefix & "crm_campaign_sales_leads " & _
                " (crmcsl_campaign_id, crmcsl_channel_id, crmcsl_customer_id, crmcsl_status, crmcsl_last_call, crmcsl_mail_sent, " & _
                " crmcsl_mail_received, crmcsl_create_user, crmcsl_create_date, crmcsl_update_user, crmcsl_update_date) " & _
                " Select crmcpt_campaign_id, crmcpt_channel_id, crmcpt_customerid, '', '', '', '', '" & gsUser & "'," & _
                " GETDATE(), '" & gsUser & "', GETDATE() " & _
                " From " & serverPrefix & "crm_campaign_sl_temp " & _
                " Where crmcpt_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                " And crmcpt_channel_id = '" & cboChannel.SelectedValue & "'"

            sqlCmd.CommandText = strSQL

            Try
                sqlCmd.ExecuteNonQuery()
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try

            If AddAuditTrail("0", "Import List", strLog) = False Then
                MsgBox("Error writing audit trail entry.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
            End If
            strLog = ""
        Else

            ' Add record to crm_campaign_sales_leads
            strSQL = "Insert " & serverPrefix & "crm_campaign_sales_leads " & _
                " (crmcsl_campaign_id, crmcsl_channel_id, crmcsl_customer_id, crmcsl_status, crmcsl_last_call, crmcsl_mail_sent, " & _
                " crmcsl_mail_received, crmcsl_create_user, crmcsl_create_date, crmcsl_update_user, crmcsl_update_date) " & _
                " Values "

            For i As Integer = 0 To dtFinal.Rows.Count - 1
                strVal = "('" & cboCampaign.SelectedValue & "','" & cboChannel.SelectedValue & "'," & dtFinal.Rows(i).Item("CustomerID") & _
                    ", '', '', '', '', '" & gsUser & "',GETDATE(), '" & gsUser & "', GETDATE()) "

                sqlCmd.CommandText = strSQL & strVal
                sqlCmd.Connection = sqlconnect

                Try
                    sqlCmd.ExecuteNonQuery()
                Catch sqlex As SqlClient.SqlException
                    If sqlex.Number <> 2627 Then
                        MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                        Exit Sub
                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Sub
                End Try
            Next

        End If

        '' Save record to csw_misc_info as well
        'strSQL &= " Insert Into csw_misc_info (cswmif_type, cswmif_customer_id, cswmif_provider, cswmif_create_user, cswmif_create_date, cswmif_update_user, cswmif_update_date) " & _
        '    " Select 'OTHER', crmcsl_customer_id, '" & RTrim(strSource) & "','" & gsUser & "', GETDATE(), '" & gsUser & "', GETDATE() " & _
        '    " From crm_campaign_sales_leads " & _
        '    " Where crmcsl_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
        '    " And crmcsl_channel_id = '" & cboChannel.SelectedValue & "'"

        'sqlCmd.CommandText = strSQL
        'sqlCmd.Connection = sqlconnect

        'Try
        '    sqlCmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'Catch sqlex As SqlClient.SqlException
        '    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        'End Try

        ' Add record to crm_campaign_snapshot
        Dim dtSS As New DataTable
        Dim drSS As DataRow
        Dim sqlda As SqlDataAdapter
        Dim sqlcb As SqlCommandBuilder
        Dim sr As StreamReader
        Dim strFile As String = GenSnapshotFile()

        strSQL = "Select * from " & serverPrefix & "crm_campaign_snapshot where 1=2"
        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        sqlcb = New SqlCommandBuilder(sqlda)

        Try
            sqlda.Fill(dtSS)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        End Try

        drSS = dtSS.NewRow()
        drSS.Item("crmcps_campaign_id") = cboCampaign.SelectedValue
        drSS.Item("crmcps_channel_id") = cboChannel.SelectedValue


        sr = New StreamReader(strFile & ".xml")
        drSS.Item("crmcps_content") = sr.ReadToEnd()
        sr.Close()

        sr = New StreamReader(strFile & ".xsd")
        drSS.Item("crmcps_schema") = sr.ReadToEnd
        sr.Close()

        drSS.Item("crmcps_create_user") = gsUser
        drSS.Item("crmcps_create_date") = Today
        dtSS.Rows.Add(drSS)

        Try
            sqlda.Update(dtSS)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        End Try

        MsgBox("Record saved successfully", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
        wndMain.Cursor = Cursors.Default

        sqlconnect.Close()
        sqlda = Nothing
        sqlCmd = Nothing
        sqlcb = Nothing

        gCrmMode = ""
        Call EnableButton(False)

    End Sub

    Private Sub cmdDelResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelResult.Click
        Call DeleteResult()
    End Sub

    Private Function DeleteResult() As Boolean

        Dim strSQL As String
        Dim intCnt As Long
        Dim sqlCmd As New SqlCommand
        Dim sqlconnect As New SqlConnection
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()

        DeleteResult = True

        strSQL = "Select COUNT(*) as CNT From " & serverPrefix & "crm_campaign_sales_leads " & _
            " Where crmcsl_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
            " And crmcsl_channel_id = '" & cboChannel.SelectedValue & "'"

        sqlCmd = New SqlCommand
        sqlCmd.CommandText = strSQL
        sqlCmd.Connection = sqlconnect

        Try
            intCnt = sqlCmd.ExecuteScalar
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
        End Try

        If intCnt > 0 Then
            If MsgBox("Are you sure to delete campaign sales leads and tracking records?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gSystem) = MsgBoxResult.Yes Then
                strSQL = "Delete From " & serverPrefix & "crm_campaign_sales_leads " & _
                    " Where crmcsl_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                    " And crmcsl_channel_id = '" & cboChannel.SelectedValue & "'"
                strSQL &= " Delete From " & serverPrefix & "crm_campaign_snapshot " & _
                    " Where crmcps_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                    " And crmcps_channel_id = '" & cboChannel.SelectedValue & "'"
                strSQL &= " Delete From " & serverPrefix & "crm_campaign_tracking " & _
                    " Where crmctk_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
                    " And crmctk_channel_id = '" & cboChannel.SelectedValue & "'"
                sqlCmd.CommandText = strSQL
                sqlCmd.Connection = sqlconnect

                Try
                    sqlCmd.ExecuteNonQuery()
                Catch sqlex As SqlClient.SqlException
                    MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Function
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                    Exit Function
                End Try

                'strLog = "Delete result of Campaign " & cboCampaign.SelectedValue & ", channel " & cboChannel.SelectedValue & vbCrLf

                'If AddAuditTrail("0", "Delete result", strLog) = False Then
                '    MsgBox("Error writing audit trail entry.", MsgBoxStyle.Exclamation + MsgBoxStyle.OKOnly)
                'End If

                sqlconnect.Close()
                sqlCmd.Dispose()
                sqlconnect.Dispose()
            Else
                DeleteResult = False
                sqlconnect.Close()
                sqlCmd.Dispose()
                sqlconnect.Dispose()
            End If
        End If

    End Function

    Private Sub cmdRemoveClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveClient.Click
        'CType(bmResult.Current, DataRowView).Row.Delete()
        ' Update SQL Image table

        If MsgBox("Remove all overlapped clients?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, gSystem) = MsgBoxResult.No Then
            Exit Sub
        End If

        If Not bmResult Is Nothing AndAlso bmResult.Count > 0 Then
            'If CType(bm.Current, DataRowView).Row.Item("DeleteFlag") = "Y" Then
            '    CType(bm.Current, DataRowView).Row.Item("DeleteFlag") = "Y"
            'Else
            '    CType(bm.Current, DataRowView).Row.Item("DeleteFlag") = ""
            'End If
            Dim drDelete As DataRow()
            drDelete = dtFinal.Select("Overlap = 'Y'")
            If Not drDelete Is Nothing AndAlso drDelete.Length > 0 Then
                For i As Integer = 0 To drDelete.Length - 1
                    drDelete(i).Delete()
                Next
                dtFinal.AcceptChanges()
                lblCount.Text = "Records: " & dtFinal.Rows.Count
            End If
        Else
            MsgBox("Please Run a Query First!", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
        End If

    End Sub

    Private Sub PrepareDT(ByRef dtResult As DataTable)

        ' Append Delete & Overlap Column
        'Try
        '    dtResult.Columns.Add("DeleteFlag", Type.GetType("System.String"))
        'Catch ex As Exception
        'End Try

        'Try
        '    dtResult.Columns.Add("OverlapFlag", Type.GetType("System.String"))
        'Catch ex As Exception
        'End Try

        'dtResult.PrimaryKey = New DataColumn() {dtResult.Columns("CustomerID")}

        'Dim keyCols(0) As DataColumn 'array of primary keys
        'Dim col As DataColumn 'datacolumn

        ''create the first datacolumn for username
        'col = New DataColumn
        'col.DataType = System.Type.GetType("System.String") 'set datatype for column
        'col.ColumnName = "CUSTOMERID"
        'keyCols(0) = col 'add column to primary key array
        'dtResult.PrimaryKey = keyCols 'make columns in the array primary keys for the table

    End Sub

    Private Sub radPol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radPol.Click
        If MsgBox("Change this option will cause system to remove all existing criterias, proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            radPI.Checked = False
            radPH.Checked = True
            radPI.Enabled = True
            radPH.Enabled = True
            chkDISTINCT.Enabled = True

            Call ClearAll()
            Call BuildTree()
        End If
    End Sub

    Private Sub radCUST_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radCUST.Click
        If MsgBox("Change this option will cause system to remove all existing criterias, proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            radPI.Checked = False
            radPH.Checked = False
            radPI.Enabled = False
            radPH.Enabled = False
            chkDISTINCT.Enabled = True

            Call ClearAll()
            Call BuildTree()
        End If
    End Sub

    Private Sub cboCampaign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCampaign.SelectedIndexChanged
        If Not blnLoading AndAlso cboCampaign.SelectedIndex >= 0 Then
            loadChannel(cboCampaign.SelectedValue)
        End If
    End Sub

    Private Sub cmdLoadQry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadQry.Click

        Dim dsTemp As New DataSet("Query")
        Dim strFile As String
        Dim dr As DataRow
        Dim ar() As Object

        OpenFileDialog1.Title = "Load Query"
        'OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.InitialDirectory = Application.StartupPath
        OpenFileDialog1.DefaultExt = ".xml"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Filter = "XML File (*.xml)|*.xml"

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

            strFile = OpenFileDialog1.FileName
            dsTemp.ReadXmlSchema(Replace(strFile, ".xml", ".xsd"))
            dsTemp.ReadXml(strFile)

            Try
                ' dtCriteriaList
                dtCriteriaList.Rows.Clear()
                For Each dr In dsTemp.Tables("CRITERIA").Rows
                    ar = dr.ItemArray
                    dtCriteriaList.Rows.Add(ar)
                Next

                '' dtFinal
                'dsTemp.Tables("FINAL").Rows.Clear()
                'For Each dr In dsTemp.Tables("FINAL").Rows
                '    ar = dr.ItemArray
                '    dtFinal.Rows.Add(ar)
                'Next

                ' dtOutput
                dtOutput.Rows.Clear()
                lstOutput.Items.Clear()

                For Each dr In dsTemp.Tables("OUTPUT").Rows
                    ar = dr.ItemArray
                    dtOutput.Rows.Add(ar)

                    ' also setup listbox
                    lstOutput.Items.Add(ar(1))
                Next

                ' Options
                For Each dr In dsTemp.Tables("OPTION").Rows
                    If dr.Item("QType") = "P" Then
                        If radPol.Checked = False Then
                            radPol.Checked = True
                            Call BuildTree()
                        End If
                    Else
                        If radCUST.Checked = False Then
                            radCUST.Checked = True
                            Call BuildTree()
                        End If
                    End If

                    If dr.Item("QBase") = "O" Then
                        radPH.Checked = True
                    Else
                        radPI.Checked = True
                    End If

                    If dr.Item("QDistinct") = "Y" Then
                        chkDISTINCT.Checked = True
                    Else
                        chkDISTINCT.Checked = False
                    End If
                Next

            Catch ex As Exception
                MsgBox("Invalid file format/content.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
            End Try
        End If

    End Sub

    Private Sub cmdSaveQry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveQry.Click

        Dim dsTemp As New DataSet
        Dim strFile As String

        Dim dtOption As New DataTable("OPTION")
        dtOption.Columns.Add("QType", Type.GetType("System.String"))
        dtOption.Columns.Add("QBase", Type.GetType("System.String"))
        dtOption.Columns.Add("QDistinct", Type.GetType("System.String"))

        Dim dr As DataRow
        dr = dtOption.NewRow
        dr.Item("QType") = IIf(radPol.Checked, "P", "C")
        dr.Item("QBase") = IIf(radPH.Checked, "O", "I")
        dr.Item("QDistinct") = IIf(chkDISTINCT.Checked, "Y", "N")
        dtOption.Rows.Add(dr)
        dtOption.AcceptChanges()

        dsTemp.Tables.Add(dtCriteriaList)
        'dsTemp.Tables.Add(dtFinal)
        dsTemp.Tables.Add(dtOutput)
        dsTemp.Tables.Add(dtOption)

        SaveFileDialog1.Title = "Save Query"
        SaveFileDialog1.InitialDirectory = "C:\"
        SaveFileDialog1.DefaultExt = ".xml"
        SaveFileDialog1.Filter = "XML files (*.xml)|*.xml"
        SaveFileDialog1.RestoreDirectory = True

        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strFile = SaveFileDialog1.FileName

            Try
                ' Schema
                Dim filename As String = Replace(strFile, ".xml", ".xsd")
                Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
                Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
                dsTemp.WriteXmlSchema(MyXmlTextWriter)
                MyXmlTextWriter.Close()

                ' Data
                filename = strFile
                myFileStream = New System.IO.FileStream(filename, System.IO.FileMode.Create)
                MyXmlTextWriter = New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
                dsTemp.WriteXml(MyXmlTextWriter)
                MyXmlTextWriter.Close()
            Catch ex As Exception
                MsgBox("Error saving query - " & ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
                Exit Sub
            End Try

            MsgBox("Query saved successfully", MsgBoxStyle.Information + MsgBoxStyle.OKOnly)

            dsTemp.Tables.Remove("OUTPUT")
            dsTemp.Tables.Remove("CRITERIA")
            dsTemp.Tables.Remove("OPTION")

        End If

    End Sub

    Private Function GenSnapshotFile() As String

        Dim dsTemp As New DataSet
        Dim strFile As String = "Save_Snapshot"

        dsTemp.Tables.Add(dtFinal)

        Try
            ' Schema
            Dim filename As String = strFile & ".xsd"
            'Dim myFileStream As New System.IO.FileStream(filename, System.IO.FileMode.Create)
            'Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
            'dsTemp.WriteXmlSchema(MyXmlTextWriter)
            'MyXmlTextWriter.Close()
            dsTemp.WriteXmlSchema(filename)

            ' Data
            filename = strFile & ".xml"
            'myFileStream = New System.IO.FileStream(filename, System.IO.FileMode.Create)
            'MyXmlTextWriter = New System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode)
            'dsTemp.WriteXml(MyXmlTextWriter)
            'MyXmlTextWriter.Close()
            dsTemp.WriteXml(filename)
        Catch ex As Exception
            MsgBox("Error saving query - " & ex.ToString, MsgBoxStyle.Information + MsgBoxStyle.OKOnly)
            Exit Function
        End Try

        dsTemp.Tables.Remove(dtFinal)
        Return strFile

    End Function

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Call ClearAll()
    End Sub

    Private Sub grdResult_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdResult.CurrentCellChanged

        Dim strSQL As String
        Dim drI As DataRow = CType(bmResult.Current, DataRowView).Row()

        Dim sqlCmd As New SqlCommand
        Dim sqlconnect As New SqlConnection
        Dim sqlreader As SqlDataReader
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If Not IsDBNull(drI.Item("Overlap")) AndAlso drI.Item("Overlap") = "Y" Then

            sqlconnect.ConnectionString = strCIWConn
            sqlconnect.Open()

            strSQL = "Select crmcmp_campaign_id, crmcmp_campaign_name, crmcsl_customer_id " & _
                " From " & serverPrefix & "crm_campaign, " & serverPrefix & "crm_campaign_channel, " & serverPrefix & "crm_campaign_sales_leads " & _
                " Where crmcpc_campaign_id = crmcsl_campaign_id " & _
                " And crmcmp_campaign_id = crmcpc_campaign_id " & _
                " And crmcpc_channel_id = crmcsl_channel_id " & _
                " And crmcpc_status_id <> '03' " & _
                " And crmcsl_customer_id = " & drI.Item("CustomerID")

            sqlCmd = New SqlCommand
            sqlCmd.CommandText = strSQL
            sqlCmd.Connection = sqlconnect

            Try
                sqlreader = sqlCmd.ExecuteReader()
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
                Exit Sub
            End Try

            If sqlreader.HasRows Then
                txtOverlap.Text = ""
                While sqlreader.Read()
                    txtOverlap.Text &= sqlreader.Item("crmcmp_campaign_name") & ";"
                End While
            Else
                txtOverlap.Text = ""
            End If

            sqlconnect.Close()
            sqlCmd.Dispose()
            sqlconnect.Dispose()
        Else
            txtOverlap.Text = ""
        End If

    End Sub

    Private Sub BuildProductList()

        Dim strSQL As String
        Dim sqlda As SqlDataAdapter
        Dim dtProd As New DataTable
        Dim sqlconnect As New SqlConnection
        Dim sqlreader As SqlDataReader

        sqlconnect.ConnectionString = strCIWConn

        strSQL = "Select distinct substring(productid,2,4) as ProductID, substring(productid,2,4) + '  ' + Description as Description " & _
            " From Product " & _
            " Where ProductCategory = 'IL' Order by description"

        sqlda = New SqlDataAdapter(strSQL, sqlconnect)
        Try
            sqlda.Fill(dtProd)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        End Try

        lstProduct.DataSource = dtProd
        lstProduct.DisplayMember = "Description"
        lstProduct.ValueMember = "ProductID"

    End Sub

    Private Sub cmdBuildList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuildList.Click

        If lstProduct.SelectedItems.Count > 0 Then
            For i As Integer = 0 To lstProduct.SelectedItems.Count - 1
                If txtPlanCode.Text = "" Then
                    txtPlanCode.Text = "'" & RTrim(lstProduct.SelectedItems.Item(i)(0)) & "'"
                Else
                    txtPlanCode.Text &= ", '" & RTrim(lstProduct.SelectedItems.Item(i)(0)) & "'"
                End If
            Next
        Else
            Me.txtPlanCode.Text = ""
        End If

    End Sub

    Private Sub cmdClearList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearList.Click
        txtPlanCode.Text = ""
    End Sub

    Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click

        Dim dsTemp As New DataSet
        Dim dtSS As New DataTable
        Dim sqlda As SqlDataAdapter
        Dim sqlconnect As New SqlConnection
        Dim strSQL As String
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        If cboCampaign.SelectedIndex < 0 OrElse cboChannel.SelectedIndex < 0 Then
            Exit Sub
        End If

        strSQL = "Select * from " & serverPrefix & "crm_campaign_snapshot " & _
            " Where crmcps_campaign_id = '" & cboCampaign.SelectedValue & "'" & _
            " And crmcps_channel_id = '" & cboChannel.SelectedValue & "'"

        sqlconnect.ConnectionString = strCIWConn
        sqlda = New SqlDataAdapter(strSQL, sqlconnect)

        Try
            sqlda.Fill(dtSS)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Sub
        End Try

        If dtSS.Rows.Count > 0 Then
            Dim dr As DataRow
            Dim ar() As Object

            dsTemp.ReadXmlSchema(New StringReader(dtSS.Rows(0).Item("crmcps_schema").ToString))
            dsTemp.ReadXml(New StringReader(dtSS.Rows(0).Item("crmcps_content").ToString))

            ' dtFinal
            'dsTemp.Tables("FINAL").Rows.Clear()
            'dtFinal.Rows.Clear()
            'For Each dr In dsTemp.Tables(0).Rows
            '    ar = dr.ItemArray
            '    dtFinal.Rows.Add(ar)
            'Next
            dtFinal = dsTemp.Tables(0).Copy
            grdResult.DataSource = dtFinal
            If Not dtFinal Is Nothing Then bmResult = Me.BindingContext(dtFinal)
            dtFinal.DefaultView.Sort = "OverLap DESC"

            MsgBox("Records Loaded successfully", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)

        Else
            MsgBox("No saved snapshot found for the selected campaign.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            dtFinal.Rows.Clear()
        End If

        lblCount.Text = "Records: " & dtFinal.Rows.Count

    End Sub

    Private Sub BuildTree()

        Dim lngErrNo As Long
        Dim strErrMsg As String
        Dim containerNode, searchNode, subNode As TreeNode

        tvAllFields.Nodes.Clear()

        ' Fill Category first
        dtCategory = GetCategoryList(lngErrNo, strErrMsg)

        If radPol.Checked = True Then
            dtCategory.DefaultView.RowFilter = "crmcqc_category IN ('01','02','03','04','05')"
        ElseIf radCUST.Checked = True Then
            dtCategory.DefaultView.RowFilter = "crmcqc_category IN ('06','07','08','09')"
        End If

        If lngErrNo = 0 Then
            If Not dtCategory Is Nothing Then
                For i As Integer = 0 To dtCategory.DefaultView.Count - 1
                    containerNode = tvAllFields.Nodes.Add(dtCategory.DefaultView.Item(i).Item("crmcqc_desc"))
                    containerNode.Tag = dtCategory.DefaultView.Item(i).Item("crmcqc_category") & "C"
                Next
            End If
        End If

        ' Get available fields and add to corresponding node
        dtQuery = GetFieldList(lngErrNo, strErrMsg)

        If lngErrNo = 0 Then
            If Not dtQuery Is Nothing Then
                For i As Integer = 0 To dtQuery.Rows.Count - 1
                    For Each searchNode In tvAllFields.Nodes
                        If CStr(searchNode.Tag).EndsWith("C") Then
                            If Not IsDBNull(dtQuery.Rows(i).Item("crmcpf_category")) Then
                                If Val(searchNode.Tag) = Val(dtQuery.Rows(i).Item("crmcpf_category")) Then
                                    subNode = searchNode.Nodes.Add(dtQuery.Rows(i).Item("crmcpf_field_desc"))
                                    subNode.Tag = dtQuery.Rows(i).Item("crmcpf_field_id")

                                    If Not IsDBNull(dtQuery.Rows(i).Item("crmcpf_para_type")) Then
                                        If Trim(dtQuery.Rows(i).Item("crmcpf_para_type")) <> "" Then
                                            subNode.Tag &= "P"
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End If

        'tvAllFields.ExpandAll()

    End Sub

    Private Sub ClearAll()
        lstOutput.Items.Clear()
        dtFinal.Rows.Clear()
        dtOutput.Rows.Clear()
        dtCriteriaList.Rows.Clear()
        dtFinal.DefaultView.RowFilter = ""
        dtFinal.DefaultView.Sort = ""
        dtCriteriaList.DefaultView.RowFilter = ""
        dtCriteriaList.DefaultView.Sort = ""
        dtOutput.DefaultView.RowFilter = ""
        dtOutput.DefaultView.Sort = ""
        txtViewSQL.Text = ""
        txtCustom.Text = ""
        strLog = ""
    End Sub

    Private Function CheckDup(ByVal strID As String, ByVal strType As String) As Boolean

        Dim dr() As DataRow

        If strType = "C" Then
            dr = dtQuery.Select("crmcpf_field_id = " & strID)
            If Not dr Is Nothing AndAlso dr.Length > 0 Then
                If Trim(dr(0).Item("crmcpf_data_type")) = "" Then Return True
            End If
            dr = dtCriteriaList.Select("ID = " & strID)
        Else
            dr = dtOutput.Select("ID = " & strID)
        End If

        If dr Is Nothing Or dr.Length = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub EnableButton(ByVal blnValue)

        cmdLoad.Enabled = Not blnValue
        cmdImportList.Enabled = Not blnValue
        cmdDelResult.Enabled = Not blnValue

        cmdCancel.Enabled = blnValue
        cmdRemoveClient.Enabled = blnValue
        cmdSaveResult.Enabled = blnValue
        'cmdExportList.Enabled = Not blnValue

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        gCrmMode = ""
        Call EnableButton(False)
    End Sub

    Private Function SetLock(ByVal blnVal As Boolean) As Boolean

        Dim strSQL As String
        Dim sqlCmd As New SqlCommand
        Dim sqlconnect As New SqlConnection
        Dim intCnt As Integer
        Dim serverPrefix As String = ConcatServerDB(gcCRSServer, gcCRSDB)

        SetLock = False

        sqlconnect.ConnectionString = strCIWConn
        sqlconnect.Open()
        sqlCmd.Connection = sqlconnect

        If blnVal = True Then
            strSQL = "Update " & serverPrefix & "csw_system_info set cswsi_query_user = '" & gsUser & "' where (cswsi_query_user = '' or cswsi_query_user = '" & gsUser & "')"
        Else
            strSQL = "Update " & serverPrefix & "csw_system_info set cswsi_query_user = '' where cswsi_query_user = '" & gsUser & "'"
        End If

        sqlCmd.CommandText = strSQL

        Try
            intCnt = sqlCmd.ExecuteNonQuery()
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Exit Function
        Finally
            sqlconnect.Close()
            sqlconnect.Dispose()
            sqlCmd.Dispose()
        End Try

        If intCnt > 0 Then
            SetLock = True
        End If

    End Function

    Private Sub cmdGenerate_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdGenerate.MouseUp
        If blnLocked Then
            If SetLock(False) = False Then
                MsgBox("Problem unlock for current user, please contact IT.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            End If
            blnLocked = False
        End If
    End Sub

    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
        If lstOutput.SelectedIndex > 0 Then
            Dim strTmp As String
            strTmp = lstOutput.Items(lstOutput.SelectedIndex - 1)
            lstOutput.Items(lstOutput.SelectedIndex - 1) = lstOutput.Items(lstOutput.SelectedIndex)
            lstOutput.Items(lstOutput.SelectedIndex) = strTmp
            lstOutput.SelectedIndex -= 1
        End If
    End Sub

    Private Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click
        If lstOutput.SelectedIndex < lstOutput.Items.Count - 1 Then
            Dim strTmp As String
            strTmp = lstOutput.Items(lstOutput.SelectedIndex + 1)
            lstOutput.Items(lstOutput.SelectedIndex + 1) = lstOutput.Items(lstOutput.SelectedIndex)
            lstOutput.Items(lstOutput.SelectedIndex) = strTmp
            lstOutput.SelectedIndex += 1
        End If
    End Sub

    Private Sub uclQuery_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        If blnLocked Then
            If SetLock(False) = False Then
                MsgBox("Problem unlock for current user, please contact IT.", MsgBoxStyle.Information + MsgBoxStyle.OKOnly, gSystem)
            End If
            blnLocked = False
        End If
    End Sub

End Class
