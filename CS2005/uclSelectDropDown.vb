'Imports System.Text.RegularExpressions
Imports System.ComponentModel

<DefaultProperty("LabelText")> _
Public Class SelectDropDown
    Inherits System.Windows.Forms.UserControl

    Private strField As String
    Private strDefOp As String
    Private objDefList As Object
    Public a As String

    Public Sub Clear()
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        ComboBox1.Text = strDefOp
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.SelectedItem = "between" Then
            ComboBox3.Visible = True
            Label2.Visible = True
        Else
            ComboBox3.Visible = False
            Label2.Visible = False
        End If
    End Sub

    <Category("SelectDropDown Configuration"), _
    Description("Default Operator")> _
    Public Property DefaultOp() As String
        Get
            Return strDefOp
        End Get
        Set(ByVal Value As String)
            strDefOp = Value
            ComboBox1.Text = Value
        End Set
    End Property

    <Category("SelectDropDown Configuration"), _
    Description("Name displayed on left")> _
    Public Property LabelText() As String
        Get
            LabelText = Label1.Text
        End Get
        Set(ByVal Value As String)
            Label1.Text = Value
            ComboBox1.Text = strDefOp
        End Set
    End Property

    <Category("SelectDropDown Configuration"), _
    Description("Default value for the field")> _
    Public WriteOnly Property InputText()
        Set(ByVal Value)
            ComboBox2.Text = Value
        End Set
    End Property

    <Category("SelectDropDown Configuration"), _
    Description("SQL Field name")> _
    Public Property FieldName() As String
        Get
            Return strField
        End Get
        Set(ByVal Value As String)
            strField = Value
        End Set
    End Property

    <Category("SelectDropDown Configuration"), _
    Description("Default Data Source")> _
    Public Property DataSource() As String()
        Get
            Return objDefList
        End Get
        Set(ByVal Value As String())
            objDefList = Value
            ComboBox2.DataSource = Value
            ComboBox3.DataSource = Value
        End Set
    End Property

    <Category("SelectDropDown Configuration"), _
    Description("SQL Criteria")> _
    Public ReadOnly Property Criteria(Optional ByVal blnNoField As String = "0") As String
        Get
            Dim txtS1, txtS2, strCriteria As String
            'Dim regex As New Regex(gRegEx)

            If blnNoField = "2" Then Return ComboBox2.Text

            If ComboBox1.SelectedItem = "between" Then
                If ComboBox2.Text = "" Or ComboBox3.Text = "" Then
                    Criteria = ""
                Else
                    txtS1 = "'" & Replace(ComboBox2.SelectedValue, "'", "''") & "'"
                    txtS2 = "'" & Replace(ComboBox3.SelectedValue, "'", "''") & "'"
                End If
            Else
                If ComboBox2.Text = "" Then
                    Criteria = ""
                    Exit Property
                Else
                    txtS1 = "'" & Replace(ComboBox2.SelectedValue, "'", "''") & "'"
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

    Public Sub SetDataSource(ByVal DataSource As DataTable, ByVal DisplayMember As String, ByVal ValueMember As String)
        objDefList = DataSource

        ComboBox2.DataSource = DataSource
        ComboBox2.DisplayMember = DisplayMember
        ComboBox2.ValueMember = ValueMember

        ComboBox3.DataSource = DataSource
        ComboBox3.DisplayMember = DisplayMember
        ComboBox3.ValueMember = ValueMember
    End Sub

    Public Sub SetDataSource(ByVal DataSource As String())
        objDefList = DataSource

        ComboBox2.DataSource = DataSource
        ComboBox3.DataSource = DataSource
    End Sub

End Class
