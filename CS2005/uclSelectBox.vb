'Imports System.Text.RegularExpressions
Imports System.ComponentModel

<DefaultProperty("LabelText")>
Public Class SelectBox
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
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Policy No."
        '
        'TextBox1
        '
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Location = New System.Drawing.Point(261, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(208, 26)
        Me.TextBox1.TabIndex = 2
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Items.AddRange(New Object() {"=", ">", "<", ">=", "<=", "<>", "in", "like", "between"})
        Me.ComboBox1.Location = New System.Drawing.Point(176, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(80, 28)
        Me.ComboBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox2.Location = New System.Drawing.Point(459, 0)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(208, 26)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(427, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "and"
        Me.Label2.Visible = False
        '
        'SelectBox
        '
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "SelectBox"
        Me.Size = New System.Drawing.Size(672, 28)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private strField As String
    Private strDefOp As String
    Public a As String

    Public Sub Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = strDefOp
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.SelectedItem = "between" Then
            TextBox2.Visible = True
            Label2.Visible = True
        Else
            TextBox2.Visible = False
            Label2.Visible = False
        End If

        If TextBox1.Text = "" Then
            Select Case ComboBox1.SelectedItem
                Case "in"
                    TextBox1.Text = "x, y"
                    'Case "LIKE"
                    '    TextBox1.Text = "x%"
            End Select
        End If

    End Sub

    <Category("SelectBox Configuration"),
    Description("Selected Condition")>
    Public Property getSelectedValue() As String
        Get
            Return ComboBox1.SelectedItem
        End Get
        Set(ByVal value As String)
            ComboBox1.SelectedItem = value
        End Set
    End Property

    <Category("SelectBox Configuration"),
    Description("Default Operator")>
    Public Property DefaultOp() As String
        Get
            Return strDefOp
        End Get
        Set(ByVal Value As String)
            strDefOp = Value
            ComboBox1.Text = Value
        End Set
    End Property

    <Category("SelectBox Configuration"),
    Description("Name displayed on left")>
    Public Property LabelText() As String
        Get
            LabelText = Label1.Text
        End Get
        Set(ByVal Value As String)
            Label1.Text = Value
            ComboBox1.Text = strDefOp
        End Set
    End Property

    <Category("SelectBox Configuration"),
    Description("Default value for the field")>
    Public WriteOnly Property InputText()
        Set(ByVal Value)
            TextBox1.Text = Value
        End Set
    End Property

    <Category("SelectBox Configuration"),
    Description("SQL Field name")>
    Public Property FieldName() As String
        Get
            Return strField
        End Get
        Set(ByVal Value As String)
            strField = Value
        End Set
    End Property

    <Category("SelectBox Configuration"),
    Description("SQL Criteria")>
    Public ReadOnly Property Criteria(Optional ByVal blnNoField As String = "0") As String
        Get
            Dim txtS1, txtS2, strCriteria As String
            'Dim regex As New Regex(gRegEx)

            If blnNoField = "2" Then Return TextBox1.Text

            If ComboBox1.SelectedItem = "between" Then
                If TextBox1.Text = "" Or TextBox2.Text = "" Then
                    Criteria = ""
                Else
                    txtS1 = "'" & Replace(TextBox1.Text, "'", "''") & "'"
                    txtS2 = "'" & Replace(TextBox2.Text, "'", "''") & "'"
                End If
            Else
                If TextBox1.Text = "" Then
                    Criteria = ""
                    Exit Property
                Else
                    Select Case ComboBox1.SelectedItem
                        Case "in"
                            Dim ary() As String = TextBox1.Text.Split(",")
                            Dim i As Integer

                            txtS1 = ""
                            txtS1 &= "'" & Replace(Trim(ary(i)), "'", "''") & "'"
                            For i = 1 To ary.Length - 1
                                txtS1 &= ",'" & Replace(Trim(ary(i)), "'", "''") & "'"
                            Next
                            txtS1 = "(" & txtS1 & ")"
                        Case "like"
                            txtS1 = "'" & Replace(TextBox1.Text, "'", "''") & "%'"
                        Case Else
                            txtS1 = "'" & Replace(TextBox1.Text, "'", "''") & "'"
                    End Select
                End If
            End If

            If blnNoField = "1" Then
                Return txtS1
            End If

            strCriteria = strField & " " & ComboBox1.SelectedItem & " " & txtS1
            If txtS2 <> "" Then
                strCriteria &= " AND " & txtS2
            End If

            'If ComboBox1.SelectedItem <> "between" Then
            '    If regex.IsMatch(strCriteria) Then
            '        Criteria = strCriteria
            '    Else
            '        Criteria = gError
            '    End If
            'Else
            '    Criteria = strCriteria
            'End If
            Criteria = strCriteria

        End Get

    End Property

End Class
