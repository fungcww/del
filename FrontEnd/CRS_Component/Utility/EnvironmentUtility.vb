Imports System.Linq
Imports System.Reflection
Imports System.Xml

Public Class EnvironmentUtility
    ''' <summary>
    ''' Add exception handle with wrong key.
    ''' </summary>
    ''' <param name="Env"></param>
    ''' <param name="settingName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getEnvironmentSetting(ByVal Env As String, Optional ByVal settingName As String = "WebServiceURL") As String
        Dim result As New Dictionary(Of String, String)
        result = getEnvironmentSettings(Env)
        If Not result.ContainsKey(settingName) Then
            Throw New Exception("System can't find settingName=" & settingName)
        End If
        Return result.Item(settingName)
    End Function

    Public Shared Function getEnvironmentSettings(ByVal Env As String) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        Dim cfgPath = System.Configuration.ConfigurationManager.AppSettings("ConfigFilePath")
        Try

            'Load the XML file in XmlDocument.
            Dim doc As New XmlDocument()

            doc.Load(cfgPath)
            Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(doc.NameTable)
            nsmgr.AddNamespace("env", "crs:env")
            'Fetch the specific Nodes by Name.
            Dim node As XmlNode = doc.SelectSingleNode(String.Format("//env:Environment[@Alias='{0}']", Env), nsmgr)

            For Each child As XmlNode In node.ChildNodes
                If result.ContainsKey(child.Name) = False Then
                    result.Add(child.Name, child.InnerText)
                End If
            Next
        Catch ex As Exception
            Throw New Exception("read config failed,Env=" & Env & ",error=" & ex.Message & ", file path=" & cfgPath)
        End Try
        Return result
    End Function

    Public Shared Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As New DataTable()
        Dim props() As PropertyInfo = GetType(T).GetProperties()
        For Each prop As PropertyInfo In props
            If prop.GetCustomAttributes(True).Length > 0 Then
                Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                If element Is Nothing Then
                    table.Columns.Add(prop.Name, prop.PropertyType)
                Else
                    table.Columns.Add(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName), If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
                End If
            Else
                table.Columns.Add(prop.Name, prop.PropertyType)
            End If
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each prop As PropertyInfo In props
                If prop.GetCustomAttributes(True).Length > 0 Then
                    Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                    If element Is Nothing Then
                        row(prop.Name) = prop.GetValue(item, Nothing)
                    Else
                        'If (Nullable.GetUnderlyingType(prop.PropertyType) Is GetType(DateTime)) Then
                        '    row(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName)) = If(prop.GetValue(item, Nothing) Is Nothing, DBNull.Value, prop.GetValue(item, Nothing))
                        'End If
                        row(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName)) = If(prop.GetValue(item, Nothing) Is Nothing, DBNull.Value, prop.GetValue(item, Nothing))
                    End If
                Else
                    row(prop.Name) = prop.GetValue(item, Nothing)
                End If
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function

    ''' <summary>
    ''' Convert a model object list to a DataTable
    ''' </summary>
    ''' <param name="list">The model object list</param>
    ''' <param name="itemType">The type of the list item</param>
    ''' <returns>DataTable</returns>
    Public Shared Function ConvertToDataTable(list As IList, itemType As Type) As DataTable
        Dim table As New DataTable()
        Dim props() As PropertyInfo = itemType.GetProperties()
        For Each prop As PropertyInfo In props
            If prop.GetCustomAttributes(True).Length > 0 Then
                Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                If element Is Nothing Then
                    table.Columns.Add(prop.Name, prop.PropertyType)
                Else
                    table.Columns.Add(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName), If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
                End If
            Else
                table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
            End If
        Next
        For Each item As Object In list
            Dim row As DataRow = table.NewRow()
            For Each prop As PropertyInfo In props
                If prop.GetCustomAttributes(True).Length > 0 Then
                    Dim element = TryCast(prop.GetCustomAttributes(True)(0), System.Xml.Serialization.XmlElementAttribute)
                    If element Is Nothing Then
                        row(prop.Name) = prop.GetValue(item, Nothing)
                    Else
                        row(IIf(String.IsNullOrEmpty(element.ElementName), prop.Name, element.ElementName)) = If(prop.GetValue(item, Nothing) Is Nothing, DBNull.Value, prop.GetValue(item, Nothing))
                    End If
                Else
                    Dim val As Object = prop.GetValue(item, Nothing)
                    If val IsNot Nothing Then
                        row(prop.Name) = val
                    End If
                End If
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function

    ''' <summary>
    ''' Convert a large model with multiple model object lists to a DataSet
    ''' </summary>
    ''' <typeparam name="T">The large model class</typeparam>
    ''' <param name="multiListObj">The large model object</param>
    ''' <returns>DataSet</returns>
    Public Shared Function ConvertToDataSet(Of T)(multiListObj As T) As DataSet
        Dim ds As New DataSet()

        Dim propsList As IEnumerable(Of PropertyInfo) =
            GetType(T) _
            .GetProperties(BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.SetProperty) _
            .Where(Function(p) p.PropertyType.IsArray OrElse
                   (p.PropertyType.IsGenericType AndAlso
                   (p.PropertyType.GetGenericTypeDefinition().Equals(GetType(List(Of ))) OrElse
                       p.PropertyType.GetGenericTypeDefinition().Equals(GetType(IEnumerable(Of ))) OrElse
                       p.PropertyType.GetGenericTypeDefinition().Equals(GetType(ICollection(Of ))))))

        For Each p As PropertyInfo In propsList
            Dim list As Object = p.GetValue(multiListObj, Nothing)
            Dim listItemType As Type = If(p.PropertyType.IsArray, p.PropertyType.GetElementType(), p.PropertyType.GetGenericArguments().FirstOrDefault())
            Dim dt As DataTable = ConvertToDataTable(list, listItemType)
            dt.TableName = p.Name
            ds.Tables.Add(dt)
        Next

        Return ds
    End Function

End Class
