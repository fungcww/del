Public Class uclQuestion

    Private policyNo As String
    Private questId As Integer

    Public Property QuestionID() As Integer
        Get
            Return questId
        End Get
        Set(ByVal value As Integer)
            questId = value
        End Set
    End Property

    Public Property QuestionNo() As String
        Get
            Return lblQuestionNo.Text
        End Get
        Set(ByVal value As String)
            lblQuestionNo.Text = value
        End Set
    End Property

    Public Property Question() As String
        Get
            Return txtQuestion.Text
        End Get
        Set(ByVal value As String)
            txtQuestion.Text = value.Replace(vbLf, vbNewLine)
        End Set
    End Property

    Public Property AnswerValue() As String
        Get
            If rdoAns1.Checked Then
                Return "Y"
            ElseIf rdoAns2.Checked Then
                Return "U"
            ElseIf rdoAns3.Checked Then
                Return "X"
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            Select Case value
                Case "Y"
                    rdoAns1.Checked = True
                Case "U"
                    rdoAns2.Checked = True
                Case "X"
                    rdoAns3.Checked = True
                Case String.Empty
                    rdoAns1.Checked = False
                    rdoAns2.Checked = False
                    rdoAns3.Checked = False
            End Select
        End Set
    End Property

    Private _readOnly As Boolean = False
    Public Property [ReadOnly]() As Boolean
        Get
            Return _readOnly
        End Get
        Set(ByVal value As Boolean)
            rdoAns1.Enabled = Not value
            rdoAns2.Enabled = Not value
            rdoAns3.Enabled = Not value
            _readOnly = value
        End Set
    End Property

End Class
