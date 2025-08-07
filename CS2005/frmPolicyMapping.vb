Public Class frmPolicyMapping
    Public sCompanyID As String = ""

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub frmPolicyMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objDBHeader As Utility.Utility.ComHeader

        If sCompanyID.Equals("MCU") Then
            'MCU
            objDBHeader.UserID = gsUser
            objDBHeader.EnvironmentUse = g_McuEnv
            objDBHeader.ProjectAlias = "LAS" '"LAS"
            objDBHeader.CompanyID = g_McuComp
            objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"
        Else
            'ING
            objDBHeader.UserID = gsUser
            objDBHeader.EnvironmentUse = g_Env '"SIT02"
            objDBHeader.ProjectAlias = "LAS" '"LAS"
            objDBHeader.CompanyID = g_Comp '"ING"
            objDBHeader.UserType = "LASUPDATE" '"LASUPDATE"
        End If

        Me.ctlPolicyMapping.DBHeader = objDBHeader
    End Sub
End Class