'Imports System.Text.RegularExpressions
Imports System.ComponentModel

<DefaultProperty("LabelText")>
Public Class SelectBox_Asur
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
    Friend WithEvents LabelPolicyNo As System.Windows.Forms.Label
    Friend WithEvents TextBoxSearchInput1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxSearchInput2 As System.Windows.Forms.TextBox
    Friend WithEvents LabelCriteria As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LabelPolicyNo = New System.Windows.Forms.Label()
        Me.TextBoxSearchInput1 = New System.Windows.Forms.TextBox()
        Me.ComboBoxCriteria = New System.Windows.Forms.ComboBox()
        Me.TextBoxSearchInput2 = New System.Windows.Forms.TextBox()
        Me.LabelCriteria = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LabelPolicyNo
        '
        Me.LabelPolicyNo.Location = New System.Drawing.Point(4, 8)
        Me.LabelPolicyNo.Name = "LabelPolicyNo"
        Me.LabelPolicyNo.Size = New System.Drawing.Size(120, 16)
        Me.LabelPolicyNo.TabIndex = 0
        Me.LabelPolicyNo.Text = "Policy No."
        '
        'TextBoxSearchInput1
        '
        Me.TextBoxSearchInput1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxSearchInput1.Location = New System.Drawing.Point(210, 4)
        Me.TextBoxSearchInput1.Name = "TextBoxSearchInput1"
        Me.TextBoxSearchInput1.Size = New System.Drawing.Size(208, 20)
        Me.TextBoxSearchInput1.TabIndex = 2
        '
        'ComboBoxCriteria
        '
        Me.ComboBoxCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCriteria.Items.AddRange(New Object() {"=", ">", "<", ">=", "<=", "<>", "in", "like", "between"})
        Me.ComboBoxCriteria.Location = New System.Drawing.Point(125, 4)
        Me.ComboBoxCriteria.Name = "ComboBoxCriteria"
        Me.ComboBoxCriteria.Size = New System.Drawing.Size(80, 21)
        Me.ComboBoxCriteria.TabIndex = 1
        '
        'TextBoxSearchInput2
        '
        Me.TextBoxSearchInput2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxSearchInput2.Location = New System.Drawing.Point(408, 4)
        Me.TextBoxSearchInput2.Name = "TextBoxSearchInput2"
        Me.TextBoxSearchInput2.Size = New System.Drawing.Size(208, 20)
        Me.TextBoxSearchInput2.TabIndex = 3
        Me.TextBoxSearchInput2.Visible = False
        '
        'LabelCriteria
        '
        Me.LabelCriteria.AutoSize = True
        Me.LabelCriteria.Location = New System.Drawing.Point(376, 8)
        Me.LabelCriteria.Name = "LabelCriteria"
        Me.LabelCriteria.Size = New System.Drawing.Size(25, 13)
        Me.LabelCriteria.TabIndex = 4
        Me.LabelCriteria.Text = "and"
        Me.LabelCriteria.Visible = False
        '
        'SelectBox_Asur
        '
        Me.Controls.Add(Me.LabelCriteria)
        Me.Controls.Add(Me.TextBoxSearchInput2)
        Me.Controls.Add(Me.ComboBoxCriteria)
        Me.Controls.Add(Me.TextBoxSearchInput1)
        Me.Controls.Add(Me.LabelPolicyNo)
        Me.Name = "SelectBox_Asur"
        Me.Size = New System.Drawing.Size(672, 28)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private strField As String
    Private strDefOp As String
    Public a As String

    Public Sub Clear()
        TextBoxSearchInput1.Text = ""
        TextBoxSearchInput2.Text = ""
        ComboBoxCriteria.Text = strDefOp
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCriteria.SelectedIndexChanged

        If ComboBoxCriteria.SelectedItem = "between" Then
            TextBoxSearchInput2.Visible = True
            LabelCriteria.Visible = True
        Else
            TextBoxSearchInput2.Visible = False
            LabelCriteria.Visible = False
        End If

        If TextBoxSearchInput1.Text = "" Then
            Select Case ComboBoxCriteria.SelectedItem
                Case "in"
                    TextBoxSearchInput1.Text = "x, y"
                    'Case "LIKE"
                    '    TextBoxSearchInput1.Text = "x%"
            End Select
        End If

    End Sub

    <Category("SelectBox_Asur Configuration"),
    Description("Selected Condition")>
    Public Property getSelectedValue() As String
        Get
            Return ComboBoxCriteria.SelectedItem
        End Get
        Set(ByVal value As String)
            ComboBoxCriteria.SelectedItem = value
        End Set
    End Property

    <Category("SelectBox_Asur Configuration"),
    Description("Default Operator")>
    Public Property DefaultOp() As String
        Get
            Return strDefOp
        End Get
        Set(ByVal Value As String)
            strDefOp = Value
            ComboBoxCriteria.Text = Value
        End Set
    End Property

    <Category("SelectBox_Asur Configuration"),
    Description("Name displayed on left")>
    Public Property LabelText() As String
        Get
            LabelText = LabelPolicyNo.Text
        End Get
        Set(ByVal Value As String)
            LabelPolicyNo.Text = Value
            ComboBoxCriteria.Text = strDefOp
        End Set
    End Property

    <Category("SelectBox_Asur Configuration"),
    Description("Default value for the field")>
    Public WriteOnly Property InputText()
        Set(ByVal Value)
            TextBoxSearchInput1.Text = Value
        End Set
    End Property

    <Category("SelectBox_Asur Configuration"),
    Description("SQL Field name")>
    Public Property FieldName() As String
        Get
            Return strField
        End Get
        Set(ByVal Value As String)
            strField = Value
        End Set
    End Property

    'updated at 2023-9-25 by oliver for Customer Level Search Issue
    <Category("SelectBox_Asur Configuration"),
    Description("SQL Criteria")>
    Public ReadOnly Property Criteria(Optional ByVal blnNoField As String = "0") As String
        Get
            Dim txtS1, txtS2, strCriteria As String
            'Dim regex As New Regex(gRegEx)

            If blnNoField = "2" Then Return TextBoxSearchInput1.Text

            If ComboBoxCriteria.SelectedItem = "between" Then
                If TextBoxSearchInput1.Text = "" Or TextBoxSearchInput2.Text = "" Then
                    Criteria = ""
                    Exit Property
                ElseIf strField = "FirstName" OrElse strField = "NameSuffix" OrElse strField = "PhoneMobile" OrElse strField = "GovernmentIDCard" OrElse strField = "AgentCode" OrElse strField = "camalt_license_no" OrElse strField = "CustomerID" OrElse strField = "GovernmentIDCard" Then
                    If Not ValidateStringIsContainsSpecialSymbols(TextBoxSearchInput1.Text) Then
                        Criteria = gError
                        Exit Property
                    End If
                    txtS1 = "'" & Trim(Replace(TextBoxSearchInput1.Text, "'", "''")) & "'"
                    If Not ValidateStringIsContainsSpecialSymbols(TextBoxSearchInput2.Text) Then
                        Criteria = gError
                        Exit Property
                    End If
                    txtS2 = "'" & Trim(Replace(TextBoxSearchInput2.Text, "'", "''")) & "'"
                Else
                    txtS1 = "'" & Trim(Replace(TextBoxSearchInput1.Text, "'", "''")) & "'"
                    txtS2 = "'" & Trim(Replace(TextBoxSearchInput2.Text, "'", "''")) & "'"
                End If

            Else
                If TextBoxSearchInput1.Text = "" Then
                    Criteria = ""
                    Exit Property
                ElseIf strField = "FirstName" OrElse strField = "NameSuffix" OrElse strField = "PhoneMobile" OrElse strField = "GovernmentIDCard" OrElse strField = "AgentCode" OrElse strField = "camalt_license_no" OrElse strField = "CustomerID" OrElse strField = "GovernmentIDCard" Then
                    Select Case ComboBoxCriteria.SelectedItem
                        Case "in"
                            Dim ary() As String = TextBoxSearchInput1.Text.Split(",")
                            Dim i As Integer
                            If Not ValidateStringIsContainsSpecialSymbols(ary(i)) Then
                                Criteria = gError
                                Exit Property
                            End If
                            txtS1 = ""
                            txtS1 &= "'" & Replace(Trim(ary(i)), "'", "''") & "'"
                            For i = 1 To ary.Length - 1
                                If Not ValidateStringIsContainsSpecialSymbols(ary(i)) Then
                                    Criteria = gError
                                    Exit Property
                                End If
                                txtS1 &= ",'" & Replace(Trim(ary(i)), "'", "''") & "'"
                            Next
                            txtS1 = "(" & txtS1 & ")"
                        Case "like"
                            If Not ValidateStringIsContainsSpecialSymbols(TextBoxSearchInput1.Text) Then
                                Criteria = gError
                                Exit Property
                            End If
                            txtS1 = "'" & Replace(trim(TextBoxSearchInput1.Text), "'", "''") & "%'"
                        Case Else
                            If Not ValidateStringIsContainsSpecialSymbols(TextBoxSearchInput1.Text) Then
                                Criteria = gError
                                Exit Property
                            End If
                            txtS1 = "'" & Replace(Trim(TextBoxSearchInput1.Text), "'", "''") & "'"
                    End Select
                Else
                    Select Case ComboBoxCriteria.SelectedItem
                        Case "in"
                            Dim ary() As String = TextBoxSearchInput1.Text.Split(",")
                            Dim i As Integer
                            txtS1 = ""
                            txtS1 &= "'" & Replace(Trim(ary(i)), "'", "''") & "'"
                            For i = 1 To ary.Length - 1
                                txtS1 &= ",'" & Replace(Trim(ary(i)), "'", "''") & "'"
                            Next
                            txtS1 = "(" & txtS1 & ")"
                        Case "like"
                            txtS1 = "'" & Replace(Trim(TextBoxSearchInput1.Text), "'", "''") & "%'"
                        Case Else
                            txtS1 = "'" & Replace(Trim(TextBoxSearchInput1.Text), "'", "''") & "'"
                    End Select
                End If

            End If

            If blnNoField = "1" Then
                Return txtS1
            End If

            strCriteria = strField & " " & ComboBoxCriteria.SelectedItem & " " & txtS1
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
