Imports System.Data.SqlClient

Public Class AddressSelectMcu
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
    Friend WithEvents chkBadAddr As System.Windows.Forms.CheckBox
    Friend WithEvents txtAddrType As System.Windows.Forms.TextBox
    Friend WithEvents grdAddress As System.Windows.Forms.DataGrid
    Friend WithEvents txtPostal As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtAddr1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddr2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddr3 As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFax1 As System.Windows.Forms.TextBox
    Friend WithEvents txtFax2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Mobile As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCnt As System.Windows.Forms.Label
    Friend WithEvents lblAddrProof As System.Windows.Forms.Label
    Friend WithEvents cboAddrProof As System.Windows.Forms.ComboBox
    Friend WithEvents txtAddr As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.chkBadAddr = New System.Windows.Forms.CheckBox()
        Me.txtAddrType = New System.Windows.Forms.TextBox()
        Me.grdAddress = New System.Windows.Forms.DataGrid()
        Me.txtPostal = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtAddr1 = New System.Windows.Forms.TextBox()
        Me.txtAddr2 = New System.Windows.Forms.TextBox()
        Me.txtAddr3 = New System.Windows.Forms.TextBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.txtPhone1 = New System.Windows.Forms.TextBox()
        Me.txtPhone2 = New System.Windows.Forms.TextBox()
        Me.txtFax1 = New System.Windows.Forms.TextBox()
        Me.txtFax2 = New System.Windows.Forms.TextBox()
        Me.lblCnt = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Mobile = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAddr = New System.Windows.Forms.TextBox()
        Me.lblAddrProof = New System.Windows.Forms.Label()
        Me.cboAddrProof = New System.Windows.Forms.ComboBox()
        CType(Me.grdAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkBadAddr
        '
        Me.chkBadAddr.Location = New System.Drawing.Point(274, 110)
        Me.chkBadAddr.Name = "chkBadAddr"
        Me.chkBadAddr.Size = New System.Drawing.Size(75, 24)
        Me.chkBadAddr.TabIndex = 185
        Me.chkBadAddr.Text = "Bad Addr"
        '
        'txtAddrType
        '
        Me.txtAddrType.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddrType.Location = New System.Drawing.Point(68, 112)
        Me.txtAddrType.Name = "txtAddrType"
        Me.txtAddrType.ReadOnly = True
        Me.txtAddrType.Size = New System.Drawing.Size(110, 20)
        Me.txtAddrType.TabIndex = 165
        '
        'grdAddress
        '
        Me.grdAddress.AllowSorting = False
        Me.grdAddress.AlternatingBackColor = System.Drawing.Color.White
        Me.grdAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAddress.BackColor = System.Drawing.Color.White
        Me.grdAddress.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAddress.CaptionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAddress.CaptionForeColor = System.Drawing.Color.Lavender
        Me.grdAddress.CaptionVisible = False
        Me.grdAddress.DataMember = ""
        Me.grdAddress.FlatMode = True
        Me.grdAddress.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdAddress.ForeColor = System.Drawing.Color.Black
        Me.grdAddress.GridLineColor = System.Drawing.Color.Wheat
        Me.grdAddress.HeaderBackColor = System.Drawing.Color.CadetBlue
        Me.grdAddress.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdAddress.HeaderForeColor = System.Drawing.Color.Black
        Me.grdAddress.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAddress.Location = New System.Drawing.Point(4, 20)
        Me.grdAddress.Name = "grdAddress"
        Me.grdAddress.ParentRowsBackColor = System.Drawing.Color.Ivory
        Me.grdAddress.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdAddress.SelectionBackColor = System.Drawing.Color.Wheat
        Me.grdAddress.SelectionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.grdAddress.Size = New System.Drawing.Size(660, 84)
        Me.grdAddress.TabIndex = 184
        '
        'txtPostal
        '
        Me.txtPostal.AcceptsReturn = True
        Me.txtPostal.BackColor = System.Drawing.SystemColors.Window
        Me.txtPostal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPostal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPostal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPostal.Location = New System.Drawing.Point(68, 212)
        Me.txtPostal.MaxLength = 0
        Me.txtPostal.Name = "txtPostal"
        Me.txtPostal.ReadOnly = True
        Me.txtPostal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPostal.Size = New System.Drawing.Size(265, 20)
        Me.txtPostal.TabIndex = 171
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCity.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCity.Location = New System.Drawing.Point(68, 208)
        Me.txtCity.MaxLength = 0
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCity.Size = New System.Drawing.Size(265, 20)
        Me.txtCity.TabIndex = 170
        Me.txtCity.Visible = False
        '
        'txtAddr1
        '
        Me.txtAddr1.AcceptsReturn = True
        Me.txtAddr1.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAddr1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr1.Location = New System.Drawing.Point(68, 136)
        Me.txtAddr1.MaxLength = 0
        Me.txtAddr1.Name = "txtAddr1"
        Me.txtAddr1.ReadOnly = True
        Me.txtAddr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr1.Size = New System.Drawing.Size(265, 20)
        Me.txtAddr1.TabIndex = 167
        Me.txtAddr1.Visible = False
        '
        'txtAddr2
        '
        Me.txtAddr2.AcceptsReturn = True
        Me.txtAddr2.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAddr2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr2.Location = New System.Drawing.Point(68, 160)
        Me.txtAddr2.MaxLength = 0
        Me.txtAddr2.Name = "txtAddr2"
        Me.txtAddr2.ReadOnly = True
        Me.txtAddr2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr2.Size = New System.Drawing.Size(265, 20)
        Me.txtAddr2.TabIndex = 168
        Me.txtAddr2.Visible = False
        '
        'txtAddr3
        '
        Me.txtAddr3.AcceptsReturn = True
        Me.txtAddr3.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAddr3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr3.Location = New System.Drawing.Point(68, 184)
        Me.txtAddr3.MaxLength = 0
        Me.txtAddr3.Name = "txtAddr3"
        Me.txtAddr3.ReadOnly = True
        Me.txtAddr3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr3.Size = New System.Drawing.Size(265, 20)
        Me.txtAddr3.TabIndex = 169
        Me.txtAddr3.Visible = False
        '
        'txtMobile
        '
        Me.txtMobile.AcceptsReturn = True
        Me.txtMobile.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMobile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMobile.Location = New System.Drawing.Point(392, 112)
        Me.txtMobile.MaxLength = 0
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReadOnly = True
        Me.txtMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobile.Size = New System.Drawing.Size(221, 20)
        Me.txtMobile.TabIndex = 166
        Me.txtMobile.Visible = False
        '
        'txtPhone1
        '
        Me.txtPhone1.AcceptsReturn = True
        Me.txtPhone1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPhone1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone1.Location = New System.Drawing.Point(392, 136)
        Me.txtPhone1.MaxLength = 0
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.ReadOnly = True
        Me.txtPhone1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone1.Size = New System.Drawing.Size(221, 20)
        Me.txtPhone1.TabIndex = 172
        '
        'txtPhone2
        '
        Me.txtPhone2.AcceptsReturn = True
        Me.txtPhone2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPhone2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone2.Location = New System.Drawing.Point(392, 160)
        Me.txtPhone2.MaxLength = 0
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.ReadOnly = True
        Me.txtPhone2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone2.Size = New System.Drawing.Size(221, 20)
        Me.txtPhone2.TabIndex = 173
        '
        'txtFax1
        '
        Me.txtFax1.AcceptsReturn = True
        Me.txtFax1.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFax1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFax1.Location = New System.Drawing.Point(392, 184)
        Me.txtFax1.MaxLength = 0
        Me.txtFax1.Name = "txtFax1"
        Me.txtFax1.ReadOnly = True
        Me.txtFax1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax1.Size = New System.Drawing.Size(221, 20)
        Me.txtFax1.TabIndex = 174
        '
        'txtFax2
        '
        Me.txtFax2.AcceptsReturn = True
        Me.txtFax2.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtFax2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFax2.Location = New System.Drawing.Point(392, 208)
        Me.txtFax2.MaxLength = 0
        Me.txtFax2.Name = "txtFax2"
        Me.txtFax2.ReadOnly = True
        Me.txtFax2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax2.Size = New System.Drawing.Size(221, 20)
        Me.txtFax2.TabIndex = 175
        '
        'lblCnt
        '
        Me.lblCnt.AutoSize = True
        Me.lblCnt.BackColor = System.Drawing.SystemColors.Control
        Me.lblCnt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblCnt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCnt.Location = New System.Drawing.Point(4, 4)
        Me.lblCnt.Name = "lblCnt"
        Me.lblCnt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCnt.Size = New System.Drawing.Size(87, 13)
        Me.lblCnt.TabIndex = 183
        Me.lblCnt.Text = "Address - 0 items"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(32, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 182
        Me.Label8.Text = "Type"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(20, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 181
        Me.Label7.Text = "Address"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(0, 216)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 179
        Me.Label5.Text = "Postal Code"
        '
        'Mobile
        '
        Me.Mobile.AutoSize = True
        Me.Mobile.BackColor = System.Drawing.SystemColors.Control
        Me.Mobile.Cursor = System.Windows.Forms.Cursors.Default
        Me.Mobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Mobile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Mobile.Location = New System.Drawing.Point(352, 116)
        Me.Mobile.Name = "Mobile"
        Me.Mobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Mobile.Size = New System.Drawing.Size(38, 13)
        Me.Mobile.TabIndex = 178
        Me.Mobile.Text = "Mobile"
        Me.Mobile.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(352, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 177
        Me.Label3.Text = "Phone"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(364, 188)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "Fax"
        '
        'txtAddr
        '
        Me.txtAddr.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtAddr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr.Location = New System.Drawing.Point(68, 136)
        Me.txtAddr.Multiline = True
        Me.txtAddr.Name = "txtAddr"
        Me.txtAddr.ReadOnly = True
        Me.txtAddr.Size = New System.Drawing.Size(264, 72)
        Me.txtAddr.TabIndex = 186
        '
        'lblAddrProof
        '
        Me.lblAddrProof.AutoSize = True
        Me.lblAddrProof.Location = New System.Drawing.Point(180, 116)
        Me.lblAddrProof.Name = "lblAddrProof"
        Me.lblAddrProof.Size = New System.Drawing.Size(57, 13)
        Me.lblAddrProof.TabIndex = 187
        Me.lblAddrProof.Text = "Addr Proof"
        '
        'cboAddrProof
        '
        Me.cboAddrProof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAddrProof.FormattingEnabled = True
        Me.cboAddrProof.Items.AddRange(New Object() {"N", "Y"})
        Me.cboAddrProof.Location = New System.Drawing.Point(235, 110)
        Me.cboAddrProof.Name = "cboAddrProof"
        Me.cboAddrProof.Size = New System.Drawing.Size(35, 21)
        Me.cboAddrProof.TabIndex = 188
        '
        'AddressSelect
        '
        Me.Controls.Add(Me.cboAddrProof)
        Me.Controls.Add(Me.lblAddrProof)
        Me.Controls.Add(Me.txtAddr)
        Me.Controls.Add(Me.chkBadAddr)
        Me.Controls.Add(Me.txtAddrType)
        Me.Controls.Add(Me.grdAddress)
        Me.Controls.Add(Me.txtPostal)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.txtAddr1)
        Me.Controls.Add(Me.txtAddr2)
        Me.Controls.Add(Me.txtAddr3)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.txtPhone1)
        Me.Controls.Add(Me.txtPhone2)
        Me.Controls.Add(Me.txtFax1)
        Me.Controls.Add(Me.txtFax2)
        Me.Controls.Add(Me.lblCnt)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Mobile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Name = "AddressSelect"
        Me.Size = New System.Drawing.Size(672, 236)
        CType(Me.grdAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private strSQL1, strRowFilter As String
    Private ds As DataSet = New DataSet("Address")
    Private dr, dr1 As DataRow
    Private bm As BindingManagerBase
    Private strAdd1, strAdd2, strAdd3, strAdd4 As String
    Private blnChiFld As Boolean

    Public WriteOnly Property srcDTAddr(Optional ByVal ChiFld As Boolean = False) As DataTable
        Set(ByVal Value As DataTable)
            If Not Value Is Nothing Then
                ' Row filter disappear after adding to DS
                strRowFilter = Value.DefaultView.RowFilter
                ds.Tables.Add(Value)
                blnChiFld = ChiFld
                Call buildUI()
            End If
        End Set
    End Property

    Public Function resetDS()
        'function to clear everything while reload person
        ds = Nothing
        ds = New DataSet("Address")
        grdAddress.TableStyles.Clear()
        grdAddress.Refresh()
        Me.txtPostal.DataBindings.Clear()
        Me.txtPhone1.DataBindings.Clear()
        Me.txtPhone2.DataBindings.Clear()
        Me.txtFax1.DataBindings.Clear()
        Me.txtFax2.DataBindings.Clear()

        Me.txtAddrType.Text = ""
        Me.txtAddr.Text = ""
        Me.txtPostal.Text = ""
        Me.txtMobile.Text = ""
        Me.txtPhone1.Text = ""
        Me.txtPhone2.Text = ""
        Me.txtFax1.Text = ""
        Me.txtFax2.Text = ""
        Me.SetAddrProofValues(False)
        Me.cboAddrProof.SelectedItem = "N"

    End Function

    ''' <summary>
    ''' Build ui.
    ''' Lubin 2022-11-07 ITSR-3487 Move the Macau query logic to the server side.
    ''' Changes: Move the query to the business server side.
    ''' </summary>
    Private Function buildUI()

        Try
            'strSQL = "select AddressTypeCode, AddressType from AddressTypeCodes"
            Dim retDs As DataSet = APIServiceBL.CallAPIBusi(g_McuComp, "FRM_ADDRESSTYPECODES",
                                                            New Dictionary(Of String, String)() From {
                                                            })
            ds.Tables.Add(retDs.Tables(0).Copy)
            'sqlda.Fill(ds, "AddressTypeCodes")

        Catch sqlex As SqlClient.SqlException
            MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try

        Dim relAddress As New Data.DataRelation("Address", ds.Tables("AddressTypeCodes").Columns("AddressTypeCode"),
            ds.Tables("CustomerAddress").Columns("AddressTypeCode"), True)

        Try
            ds.Relations.Add(relAddress)
        Catch sqlex As SqlClient.SqlException
            'MsgBox(sqlex.Number & sqlex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        Catch ex As Exception
            'MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, "Error")
        End Try

        With ds.Tables("CustomerAddress")
            .Columns.Add("AddressType", GetType(String))
        End With

        ds.Tables("CustomerAddress").DefaultView.RowFilter = strRowFilter
        strAdd1 = "AddressLine1"
        strAdd2 = "AddressLine2"
        strAdd3 = "AddressLine3"
        strAdd4 = "AddressCity"

        ' Check if CAddress fields exists
        If Me.blnChiFld Then
            If ds.Tables("CustomerAddress").DefaultView.Count > 0 Then
                With ds.Tables("CustomerAddress").DefaultView.Item(0)
                    If (.Item(strAdd1) = "" And .Item(strAdd2) = "" And .Item(strAdd3) = "" And .Item(strAdd4) = "") Or .Item("UseChiInd") = "Y" Then
                        strAdd1 = "CAddressLine1"
                        strAdd2 = "CAddressLine2"
                        strAdd3 = "CAddressLine3"
                        strAdd4 = "CAddressCity"
                    End If
                End With
            End If
        End If

        Dim ts As New clsDataGridTableStyle
        'Dim cs As DataGridColumnStyle
        Dim cs As DataGridTextBoxColumn

        cs = New JoinTextBoxColumn("Address", ds.Tables("AddressTypeCodes").Columns("AddressType"))
        cs.Width = 150
        cs.MappingName = "AddressType"
        cs.HeaderText = "Type"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 70
        cs.MappingName = "BadAddress"
        cs.HeaderText = "Bad Addr."
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 70
        cs.MappingName = "AddrProof"
        cs.HeaderText = "AddrProof"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = strAdd1
        cs.HeaderText = "Address - 1"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = strAdd2
        cs.HeaderText = "Address - 2"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 150
        cs.MappingName = strAdd3
        cs.HeaderText = "Address - 3"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = strAdd4
        cs.HeaderText = "City"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "AddressPostalCo"
        cs.HeaderText = "Postal Code"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "PhoneNumber1"
        cs.HeaderText = "Phone"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "FaxNumber1"
        cs.HeaderText = "Fax"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        cs = New DataGridTextBoxColumn
        cs.Width = 100
        cs.MappingName = "EMailID"
        cs.HeaderText = "Email"
        cs.NullText = gNULLText
        ts.GridColumnStyles.Add(cs)

        ts.MappingName = "CustomerAddress"
        grdAddress.TableStyles.Add(ts)

        grdAddress.DataSource = ds.Tables("CustomerAddress")
        grdAddress.AllowDrop = False
        grdAddress.ReadOnly = True
        'ds.Tables("CustomerAddress").DefaultView.RowFilter = strRowFilter
        bm = Me.BindingContext(ds.Tables("CustomerAddress"))

        'Me.txtAddr1.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "AddressLine1")
        'Me.txtAddr2.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "AddressLine2")
        'Me.txtAddr3.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "AddressLine3")
        'Me.txtCity.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "AddressCity")        
        Me.txtPostal.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "AddressPostalCode")
        Me.txtPhone1.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "PhoneNumber1")
        Me.txtPhone2.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "PhoneNumber2")
        Me.txtFax1.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "FaxNumber1")
        Me.txtFax2.DataBindings.Add("Text", ds.Tables("CustomerAddress"), "FaxNumber2")
        lblCnt.Text = "Address - " & ds.Tables("CustomerAddress").DefaultView.Count & " items"

        Call UpdateAT()

    End Function

    Private Sub UpdateAT()

        Dim drI As DataRow

        If bm.Count = 0 Then
            Exit Sub
        End If

        drI = CType(bm.Current, DataRowView).Row()

        If Not drI Is Nothing Then
            Me.txtAddrType.Text = GetRelationValue(drI, "Address", "AddressType")
            If Not IsDBNull(drI.Item("BadAddress")) Then
                If drI.Item("BadAddress") = "Y" Then
                    Me.chkBadAddr.Checked = True
                Else
                    Me.chkBadAddr.Checked = False
                End If
            End If

            txtAddr.Text = ""
            Dim strAdd As String
            strAdd = Trim(drI.Item(strAdd1))
            If strAdd <> "" Then
                txtAddr.Text = strAdd & vbCrLf
            End If
            strAdd = Trim(drI.Item(strAdd2))
            If strAdd <> "" Then
                txtAddr.Text &= strAdd & vbCrLf
            End If
            strAdd = Trim(drI.Item(strAdd3))
            If strAdd <> "" Then
                txtAddr.Text &= strAdd & vbCrLf
            End If
            txtAddr.Text &= drI.Item(strAdd4)

            If String.IsNullOrEmpty(drI.Item("AddrProof").ToString.Trim) Then
                Me.SetAddrProofValues(True)
            Else
                Me.SetAddrProofValues(False)
            End If

            cboAddrProof.SelectedItem = drI.Item("AddrProof")
        End If

    End Sub

    Private Sub grdAddress_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAddress.CurrentCellChanged
        Call UpdateAT()
    End Sub

    Private Sub grdAddress_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAddress.MouseUp
        Dim pt = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = grdAddress.HitTest(pt)

        If hti.Type = DataGrid.HitTestType.Cell Then
            grdAddress.CurrentCell = New DataGridCell(hti.Row, hti.Column)
            grdAddress.Select(hti.Row)
        End If
    End Sub

    Private Sub SetAddrProofValues(ByVal isIncludeBlankValue As Boolean)

        Me.cboAddrProof.Items.Clear()

        If isIncludeBlankValue Then
            Me.cboAddrProof.Items.Add("")
        End If

        Me.cboAddrProof.Items.Add("N")
        Me.cboAddrProof.Items.Add("Y")

    End Sub

End Class
