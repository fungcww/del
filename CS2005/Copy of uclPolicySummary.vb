Imports System.Data.SqlClient

Public Class PolicySummary
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents grdPolRel As System.Windows.Forms.DataGrid
    Friend WithEvents grdPolRmk As System.Windows.Forms.DataGrid
    Friend WithEvents txtProdCat As System.Windows.Forms.TextBox
    Friend WithEvents txtBillNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPolCurr As System.Windows.Forms.TextBox
    Friend WithEvents txtTermDate As System.Windows.Forms.TextBox
    Friend WithEvents txtPolPrem As System.Windows.Forms.TextBox
    Friend WithEvents txtProdType As System.Windows.Forms.TextBox
    Friend WithEvents txtCommDate As System.Windows.Forms.TextBox
    Friend WithEvents txtSumInsured As System.Windows.Forms.TextBox
    Friend WithEvents txtPTD As System.Windows.Forms.TextBox
    Friend WithEvents txtPolEffDate As System.Windows.Forms.TextBox
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtProdCat = New System.Windows.Forms.TextBox
        Me.txtBillNo = New System.Windows.Forms.TextBox
        Me.txtPolCurr = New System.Windows.Forms.TextBox
        Me.txtTermDate = New System.Windows.Forms.TextBox
        Me.txtPolPrem = New System.Windows.Forms.TextBox
        Me.txtProdType = New System.Windows.Forms.TextBox
        Me.txtCommDate = New System.Windows.Forms.TextBox
        Me.txtSumInsured = New System.Windows.Forms.TextBox
        Me.txtPTD = New System.Windows.Forms.TextBox
        Me.txtPolEffDate = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.grdPolRel = New System.Windows.Forms.DataGrid
        Me.grdPolRmk = New System.Windows.Forms.DataGrid
        Me.cmdOpen = New System.Windows.Forms.Button
        CType(Me.grdPolRel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPolRmk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtProdCat
        '
        Me.txtProdCat.AcceptsReturn = True
        Me.txtProdCat.AutoSize = False
        Me.txtProdCat.BackColor = System.Drawing.SystemColors.Window
        Me.txtProdCat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProdCat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtProdCat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProdCat.Location = New System.Drawing.Point(92, 4)
        Me.txtProdCat.MaxLength = 0
        Me.txtProdCat.Name = "txtProdCat"
        Me.txtProdCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProdCat.Size = New System.Drawing.Size(197, 20)
        Me.txtProdCat.TabIndex = 47
        Me.txtProdCat.Text = ""
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.AutoSize = False
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBillNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBillNo.Location = New System.Drawing.Point(408, 32)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(117, 20)
        Me.txtBillNo.TabIndex = 46
        Me.txtBillNo.Text = ""
        '
        'txtPolCurr
        '
        Me.txtPolCurr.AcceptsReturn = True
        Me.txtPolCurr.AutoSize = False
        Me.txtPolCurr.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolCurr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPolCurr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPolCurr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPolCurr.Location = New System.Drawing.Point(300, 32)
        Me.txtPolCurr.MaxLength = 0
        Me.txtPolCurr.Name = "txtPolCurr"
        Me.txtPolCurr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPolCurr.Size = New System.Drawing.Size(45, 20)
        Me.txtPolCurr.TabIndex = 45
        Me.txtPolCurr.Text = ""
        '
        'txtTermDate
        '
        Me.txtTermDate.AcceptsReturn = True
        Me.txtTermDate.AutoSize = False
        Me.txtTermDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTermDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTermDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTermDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTermDate.Location = New System.Drawing.Point(472, 104)
        Me.txtTermDate.MaxLength = 0
        Me.txtTermDate.Name = "txtTermDate"
        Me.txtTermDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTermDate.Size = New System.Drawing.Size(81, 20)
        Me.txtTermDate.TabIndex = 44
        Me.txtTermDate.Text = ""
        '
        'txtPolPrem
        '
        Me.txtPolPrem.AcceptsReturn = True
        Me.txtPolPrem.AutoSize = False
        Me.txtPolPrem.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolPrem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPolPrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPolPrem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPolPrem.Location = New System.Drawing.Point(256, 72)
        Me.txtPolPrem.MaxLength = 0
        Me.txtPolPrem.Name = "txtPolPrem"
        Me.txtPolPrem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPolPrem.Size = New System.Drawing.Size(81, 20)
        Me.txtPolPrem.TabIndex = 43
        Me.txtPolPrem.Text = ""
        '
        'txtProdType
        '
        Me.txtProdType.AcceptsReturn = True
        Me.txtProdType.AutoSize = False
        Me.txtProdType.BackColor = System.Drawing.SystemColors.Window
        Me.txtProdType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProdType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtProdType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProdType.Location = New System.Drawing.Point(332, 4)
        Me.txtProdType.MaxLength = 0
        Me.txtProdType.Name = "txtProdType"
        Me.txtProdType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProdType.Size = New System.Drawing.Size(221, 20)
        Me.txtProdType.TabIndex = 42
        Me.txtProdType.Text = ""
        '
        'txtCommDate
        '
        Me.txtCommDate.AcceptsReturn = True
        Me.txtCommDate.AutoSize = False
        Me.txtCommDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCommDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCommDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCommDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCommDate.Location = New System.Drawing.Point(112, 32)
        Me.txtCommDate.MaxLength = 0
        Me.txtCommDate.Name = "txtCommDate"
        Me.txtCommDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCommDate.Size = New System.Drawing.Size(81, 20)
        Me.txtCommDate.TabIndex = 41
        Me.txtCommDate.Text = ""
        '
        'txtSumInsured
        '
        Me.txtSumInsured.AcceptsReturn = True
        Me.txtSumInsured.AutoSize = False
        Me.txtSumInsured.BackColor = System.Drawing.SystemColors.Window
        Me.txtSumInsured.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSumInsured.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSumInsured.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSumInsured.Location = New System.Drawing.Point(72, 72)
        Me.txtSumInsured.MaxLength = 0
        Me.txtSumInsured.Name = "txtSumInsured"
        Me.txtSumInsured.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSumInsured.Size = New System.Drawing.Size(81, 20)
        Me.txtSumInsured.TabIndex = 40
        Me.txtSumInsured.Text = ""
        '
        'txtPTD
        '
        Me.txtPTD.AcceptsReturn = True
        Me.txtPTD.AutoSize = False
        Me.txtPTD.BackColor = System.Drawing.SystemColors.Window
        Me.txtPTD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPTD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPTD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPTD.Location = New System.Drawing.Point(284, 104)
        Me.txtPTD.MaxLength = 0
        Me.txtPTD.Name = "txtPTD"
        Me.txtPTD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPTD.Size = New System.Drawing.Size(81, 20)
        Me.txtPTD.TabIndex = 39
        Me.txtPTD.Text = ""
        '
        'txtPolEffDate
        '
        Me.txtPolEffDate.AcceptsReturn = True
        Me.txtPolEffDate.AutoSize = False
        Me.txtPolEffDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolEffDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPolEffDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPolEffDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPolEffDate.Location = New System.Drawing.Point(112, 104)
        Me.txtPolEffDate.MaxLength = 0
        Me.txtPolEffDate.Name = "txtPolEffDate"
        Me.txtPolEffDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPolEffDate.Size = New System.Drawing.Size(81, 20)
        Me.txtPolEffDate.TabIndex = 38
        Me.txtPolEffDate.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(92, 16)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Product Category"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(300, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(29, 16)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(114, 16)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Commencement Date"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(384, 108)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(91, 16)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "Termination Date"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(208, 108)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(72, 16)
        Me.Label13.TabIndex = 53
        Me.Label13.Text = "Paid-To-Date"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(4, 108)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(108, 16)
        Me.Label14.TabIndex = 52
        Me.Label14.Text = "Policy Effective Date"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(176, 76)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(83, 16)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "Policy Premium"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(4, 76)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(68, 16)
        Me.Label16.TabIndex = 50
        Me.Label16.Text = "Sum Insured"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(368, 36)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(40, 16)
        Me.Label17.TabIndex = 49
        Me.Label17.Text = "Bill No."
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(216, 36)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(83, 16)
        Me.Label18.TabIndex = 48
        Me.Label18.Text = "Policy Currency"
        '
        'grdPolRel
        '
        Me.grdPolRel.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPolRel.BackColor = System.Drawing.Color.White
        Me.grdPolRel.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPolRel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPolRel.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolRel.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPolRel.CaptionVisible = False
        Me.grdPolRel.DataMember = ""
        Me.grdPolRel.FlatMode = True
        Me.grdPolRel.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPolRel.ForeColor = System.Drawing.Color.Black
        Me.grdPolRel.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPolRel.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPolRel.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPolRel.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPolRel.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolRel.Location = New System.Drawing.Point(4, 136)
        Me.grdPolRel.Name = "grdPolRel"
        Me.grdPolRel.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPolRel.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPolRel.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPolRel.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolRel.Size = New System.Drawing.Size(632, 76)
        Me.grdPolRel.TabIndex = 58
        '
        'grdPolRmk
        '
        Me.grdPolRmk.AlternatingBackColor = System.Drawing.Color.White
        Me.grdPolRmk.BackColor = System.Drawing.Color.White
        Me.grdPolRmk.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdPolRmk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdPolRmk.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolRmk.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdPolRmk.CaptionVisible = False
        Me.grdPolRmk.DataMember = ""
        Me.grdPolRmk.FlatMode = True
        Me.grdPolRmk.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdPolRmk.ForeColor = System.Drawing.Color.Black
        Me.grdPolRmk.GridLineColor = System.Drawing.Color.Wheat
        Me.grdPolRmk.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdPolRmk.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdPolRmk.HeaderForeColor = System.Drawing.Color.Black
        Me.grdPolRmk.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolRmk.Location = New System.Drawing.Point(4, 220)
        Me.grdPolRmk.Name = "grdPolRmk"
        Me.grdPolRmk.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdPolRmk.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPolRmk.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdPolRmk.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdPolRmk.Size = New System.Drawing.Size(632, 76)
        Me.grdPolRmk.TabIndex = 59
        '
        'cmdOpen
        '
        Me.cmdOpen.Location = New System.Drawing.Point(640, 140)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(64, 23)
        Me.cmdOpen.TabIndex = 60
        Me.cmdOpen.Text = "&Open"
        '
        'PolicySummary
        '
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.grdPolRmk)
        Me.Controls.Add(Me.grdPolRel)
        Me.Controls.Add(Me.txtProdCat)
        Me.Controls.Add(Me.txtBillNo)
        Me.Controls.Add(Me.txtPolCurr)
        Me.Controls.Add(Me.txtTermDate)
        Me.Controls.Add(Me.txtPolPrem)
        Me.Controls.Add(Me.txtProdType)
        Me.Controls.Add(Me.txtCommDate)
        Me.Controls.Add(Me.txtSumInsured)
        Me.Controls.Add(Me.txtPTD)
        Me.Controls.Add(Me.txtPolEffDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label18)
        Me.Name = "PolicySummary"
        Me.Size = New System.Drawing.Size(720, 304)
        CType(Me.grdPolRel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPolRmk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private ds As DataSet = New DataSet("PolicySummary")
    Private strPolicy As String
    Private dr, dr1 As DataRow
    Private bm As BindingManagerBase

    Public Property PolicyAccountID() As String
        Get
            Return strPolicy
        End Get
        Set(ByVal Value As String)
            strPolicy = Value
        End Set
    End Property

    Public WriteOnly Property srcDTPolSum(ByVal dtNA As DataTable)
        Set(ByVal Value)
            If Not Value Is Nothing Then
                ds.Tables.Add(Value)
                ds.Tables.Add(dtNA)
                Call BuildUI()
            End If
        End Set
    End Property

    Private Sub BuildUI()

        Dim sqlconnect As New SqlConnection
        Dim sqlcmd As New SqlCommand
        Dim strSQL As String
        Dim lngCnt As Long
        Dim sqlda As SqlDataAdapter
        Dim sqldt As DataTable

        Try
            strSQL = "Select ProductTypeCode, ProductTypeDescription from ProductTypeCodes; "
            strSQL &= "Select ProductCategory, ProductCategoryDescription from ProductCategoryCodes; "
            strSQL &= "Select PolicyAccountRelationCode, PolicyAccountRelationDesc, SortingSeq from PolicyAccountRelationCodes; "
            strSQL &= "Select * from PolicyRemarks where PolicyAccountID = '" & Trim(strPolicy) & "'"

            Try
                sqlconnect.ConnectionString = objSec.ConnStr("CSW", "CS2005", "CIW")
                sqlconnect.Open()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            Catch sqlex As SqlClient.SqlException
                MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
            End Try

            sqlda = New SqlDataAdapter(strSQL, sqlconnect)
            sqlda.MissingSchemaAction = MissingSchemaAction.AddWithKey
            sqlda.MissingMappingAction = MissingMappingAction.Passthrough
            sqlda.TableMappings.Add("ProductTypeCodes1", "ProductCategoryCodes")
            sqlda.TableMappings.Add("ProductTypeCodes2", "PolicyAccountRelationCodes")
            sqlda.TableMappings.Add("ProductTypeCodes3", "PolicyRemarks")
            sqlda.Fill(ds, "ProductTypeCodes")

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Dim relProductType As New Data.DataRelation("ProductType", ds.Tables("ProductTypeCodes").Columns("ProductTypeCode"), _
            ds.Tables("POLINF").Columns("ProductTypeCode"), True)
        Dim relProductCat As New Data.DataRelation("ProductCat", ds.Tables("ProductCategoryCodes").Columns("ProductCategory"), _
            ds.Tables("POLINF").Columns("ProductCategory"), True)
        Dim relPolicyAcRel As New Data.DataRelation("PolicyAcRel", ds.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationCode"), _
            ds.Tables("POLINF").Columns("PolicyRelateCode"), True)
        Dim relCust As New Data.DataRelation("CustRel", ds.Tables("ORDUNA").Columns("ClientID"), _
            ds.Tables("POLINF").Columns("ClientID"), True)

        Try
            ds.Relations.Add(relProductType)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relProductCat)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relPolicyAcRel)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        Try
            ds.Relations.Add(relCust)
        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        sqlconnect.Close()

        If ds.Tables("POLINF").Rows.Count > 0 Then
            With ds.Tables("POLINF").Rows(0)
                If Not IsDBNull(.Item("CommencementDate")) Then Me.txtCommDate.Text = Format(.Item("CommencementDate"), gDateFormat)
                If Not IsDBNull(.Item("PolicyCurrency")) Then Me.txtPolCurr.Text = .Item("PolicyCurrency")
                If Not IsDBNull(.Item("BillNo")) Then Me.txtBillNo.Text = .Item("BillNo")
                If Not IsDBNull(.Item("SumInsured")) Then Me.txtSumInsured.Text = .Item("SumInsured")
                If Not IsDBNull(.Item("PolicyPremium")) Then Me.txtPolPrem.Text = .Item("PolicyPremium")
                If Not IsDBNull(.Item("PolicyEffDate")) Then Me.txtPolEffDate.Text = Format(.Item("PolicyEffDate"), gDateFormat)
                If Not IsDBNull(.Item("PolicyTermDate")) Then Me.txtTermDate.Text = Format(.Item("PolicyTermDate"), gDateFormat)
                If Not IsDBNull(.Item("PaidToDate")) Then Me.txtPTD.Text = Format(.Item("PaidToDate"), gDateFormat)

                'dr = ds.Tables("Product").Rows(0)
                ''dr1 = dr.GetParentRow("ProductType")
                ''Me.txtProdType.Text = dr1.Item("ProductTypeDescription")

                'dr1 = dr.GetParentRow("ProductCat")
                'Me.txtProdCat.Text = dr1.Item("ProductCategoryDescription")
                Me.txtProdCat.Text = GetRelationValue(ds.Tables("POLINF").Rows(0), "ProductCat", "ProductCategoryDescription")

                With ds.Tables("POLINF")
                    .Columns.Add("PolicyAccountRelationDesc", GetType(String))
                    .Columns.Add("FirstName", GetType(String))
                    .Columns.Add("NameSuffix", GetType(String))
                    .Columns.Add("ChiName", GetType(String))
                    .Columns.Add("GovernmentIDCar", GetType(String))
                    .Columns.Add("PassportNumber", GetType(String))
                    .Columns.Add("CustomerID", GetType(String))
                End With

                Dim ts As New clsDataGridTableStyle
                Dim cs As DataGridTextBoxColumn

                cs = New JoinTextBoxColumn("PolicyAcRel", ds.Tables("PolicyAccountRelationCodes").Columns("PolicyAccountRelationDesc"))
                cs.Width = 150
                cs.MappingName = "PolicyAccountRelationDesc"
                cs.HeaderText = "Role"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                'cs = New DataGridTextBoxColumn
                cs = New JoinTextBoxColumn("CustRel", ds.Tables("ORDUNA").Columns("FirstName"))
                cs.Width = 150
                cs.MappingName = "FirstName"
                cs.HeaderText = "First Name"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                'cs = New DataGridTextBoxColumn
                cs = New JoinTextBoxColumn("CustRel", ds.Tables("ORDUNA").Columns("NameSuffix"))
                cs.Width = 150
                cs.MappingName = "NameSuffix"
                cs.HeaderText = "Last Name"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                'cs = New DataGridTextBoxColumn
                cs = New JoinTextBoxColumn("CustRel", ds.Tables("ORDUNA").Columns("ChiName"))
                cs.Width = 150
                cs.MappingName = "ChiName"
                cs.HeaderText = "Chinese Name"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                'cs = New DataGridTextBoxColumn
                cs = New JoinTextBoxColumn("CustRel", ds.Tables("ORDUNA").Columns("GovernmentIDCar"))
                cs.Width = 100
                cs.MappingName = "GovernmentIDCar"
                cs.HeaderText = "ID Card"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                'cs = New DataGridTextBoxColumn
                cs = New JoinTextBoxColumn("CustRel", ds.Tables("ORDUNA").Columns("PassportNumber"))
                cs.Width = 100
                cs.MappingName = "PassportNumber"
                cs.HeaderText = "Passport No."
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                'cs = New DataGridTextBoxColumn
                cs = New JoinTextBoxColumn("CustRel", ds.Tables("ORDUNA").Columns("CustomerID"))
                cs.Width = 100
                cs.MappingName = "CustomerID"
                cs.HeaderText = "CustomerID"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "POLINF"
                grdPolRel.TableStyles.Add(ts)

                bm = Me.BindingContext(ds.Tables("POLINF"))

                grdPolRel.DataSource = ds.Tables("POLINF")
                grdPolRel.AllowDrop = False
                grdPolRel.ReadOnly = True

            End With
        End If

        ' Policy Remark grid
        If ds.Tables("PolicyRemarks").Rows.Count > 0 Then
            With ds.Tables("PolicyRemarks").Rows(0)

                Dim ts As New clsDataGridTableStyle
                Dim cs As DataGridTextBoxColumn

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "UserID"
                cs.HeaderText = "User ID"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "InputDate"
                cs.HeaderText = "Input Date"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                cs = New DataGridTextBoxColumn
                cs.Width = 150
                cs.MappingName = "PolicyRemarks"
                cs.HeaderText = "Remarks"
                cs.NullText = gNULLText
                ts.GridColumnStyles.Add(cs)

                ts.MappingName = "PolicyRemarks"
                grdPolRmk.TableStyles.Add(ts)
                grdPolRmk.DataSource = ds.Tables("PolicyRemarks")
                grdPolRmk.AllowDrop = False
                grdPolRmk.ReadOnly = True

            End With

        End If

    End Sub

    Private Sub grdPolRel_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPolRel.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdPolRel.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdPolRel.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdPolRel.Select(hti.Row)
        End If
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

        Dim strName, strCustID As String

        With CType(bm.Current, DataRowView).Row
            strCustID = .Item("CustomerID")
            strName = Trim(.Item("NameSuffix")) & " " & Trim(.Item("FirstName")) & " (" & strCustID & ")"
        End With

        Dim w As New frmCustomer
        w.CustID = strCustID
        w.Text = "Customer " & strName

        ShowWindow(w, wndMain, strCustID)

        'Dim fs() As Form = wndMain.MdiChildren()
        'Dim f As Form
        'Dim blnWinFound As Boolean

        'For Each f In fs
        '    If InStr(f.Text, strCustID) <> 0 Then
        '        blnWinFound = True
        '        Exit For
        '    End If
        'Next

        'If blnWinFound Then
        '    f.Focus()
        'Else
        '    Dim w As New frmCustomer
        '    w.CustID = strCustID
        '    w.Text = "Customer " & strName
        '    'w.MdiParent = wndMain
        '    'w.Show()
        '    If Not OpenWindow(w, wndMain) Then
        '        w.Dispose()
        '    End If
        'End If

    End Sub
End Class
