Imports System.Data.SqlClient
Imports System.EnterpriseServices

' regsvcs /u abc.dll
' regsvcs abc.dll
' gacutil /i abc.dll
' <assembly: applicationid("")> COM+ application
' <assembly: guid("")>  type library

Public Class Class1
    Inherits ServicedComponent

    Protected Sub Enquiry()
        Dim strConn As String = "Connection String"
        Dim objConn As New SqlConnection(strConn)
        Dim strSQL As String = "select * from customer"
        Dim objCmd As New SqlCommand(strSQL, objConn)
        Dim objReader As SqlDataReader

        Dim strReturn As String

        Try
            objconn.open()
            strReturn = objCmd.ExecuteScalar()
            objReader = objCmd.ExecuteReader

            objConn.Close()

        Catch objexception As Exception

        End Try

        Dim objAdapter As SqlDataAdapter = New SqlDataAdapter
        objAdapter.SelectCommand = objCmd
        Dim objDataSet As DataSet = New DataSet("Customer")
        Try
            objAdapter.Fill(objDataSet, "Customer")
        Catch ex As Exception
            Console.Write(ex.ToString)
        End Try

    End Sub

    Protected Sub DataSet()
        Dim strConn As String = "Connection String"
        Dim objConn As New SqlConnection(strConn)

        Dim strSQL As String = "select * from customer"
        Dim objCustomer = New SqlCommand(strSQL, objConn)
        strSQL = "select * from policyaccount"
        Dim objPolicy = New SqlCommand(strSQL, objConn)

        Dim objAdapter As SqlDataAdapter
        Dim objDataSet As Data.DataSet
        Dim custBuilder As SqlCommandBuilder
        Try
            objConn.Open()
            objAdapter.SelectCommand = objCustomer

            ' build other sql
            'custBuilder = New SqlCommandBuilder(objAdapter)

            objAdapter.Fill(objDataSet, "Customer")
            objAdapter.SelectCommand = objPolicy
            objAdapter.Fill(objDataSet, "Policy")
            objConn.Close()

            Dim objDataRel As New Data.DataRelation("CustPolicy", objDataSet.Tables("Customer").Columns("CustomerID"), _
                objDataSet.Tables("Policy").Columns("CustomerID"), True)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub Command()
        Dim strConn As String = "Connection String"
        Dim objConn As New SqlConnection(strConn)

        Dim objAdapter As SqlDataAdapter = New SqlDataAdapter("Insert into Customer (Field1, Fields) " & _
            " values (@F1, @F2)", objConn)
        Dim workParam As SqlParameter
        workParam = objAdapter.InsertCommand.Parameters.Add _
            (New SqlParameter("@F1", SqlDbType.Int))
        workParam.SourceColumn = "F1"
        workParam.SourceVersion = DataRowVersion.Current

        Dim objDataSet As DataSet = New DataSet
        objAdapter.Fill(objDataSet, "Customer")
        Dim newRow As DataRow = objDataSet.Tables("Customer").NewRow
        newRow("F1") = "abc"
        newRow("F2") = "def"
        objDataSet.Tables("Customer").Rows.Add(newRow)
        Try
            objAdapter.Update(objDataSet, "Customer")
            objDataSet.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    <AutoComplete()> _
    Public Function Tx()
        Try
            ContextUtil.SetComplete()
        Catch ex As Exception
            ContextUtil.SetAbort()
        End Try

        ContextUtil.DeactivateOnReturn = True
        ContextUtil.MyTransactionVote = TransactionVote.Abort
        ContextUtil.MyTransactionVote = TransactionVote.Commit

    End Function
End Class
