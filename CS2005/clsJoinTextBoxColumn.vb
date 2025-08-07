Class JoinTextBoxColumn
    Inherits DataGridTextBoxColumn

    '
    ' Store the information to get to a field in the referenced table.
    '
    Private m_RelationName As String
    Private m_ParentField As DataColumn

    Public Sub New(ByVal RelationName As String, ByVal ParentField As DataColumn)
        m_RelationName = RelationName
        m_ParentField = ParentField
        MyBase.ReadOnly = True    ' this column's base style is read only
    End Sub

    Protected Overrides Function GetColumnValueAtRow(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Object
        '
        ' Get the current DataRow from the CurrencyManager.
        ' Use the GetParentRow and the DataRelation name to get the parent row.
        ' Return the field value from the parent row.
        '
        Try
            Dim dr As DataRow = CType(cm.List, DataView).Item(RowNum).Row
            Dim drParent As DataRow = dr.GetParentRow(m_RelationName)
            Return drParent(m_ParentField).ToString()
        Catch
            Return "" ' handles NullReferenceException case when adding record
        End Try
    End Function

    Protected Overrides Function Commit(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Boolean
        '
        ' Dummy implementation because it is read-only.
        '
        Return False
    End Function

    Public Shadows ReadOnly Property [ReadOnly]() As Boolean
        '
        ' Shadow the base property so it cannot be set.
        ' Return TRUE so the DataGrid cannot allow edits.
        '
        Get
            Return True
        End Get
    End Property
End Class