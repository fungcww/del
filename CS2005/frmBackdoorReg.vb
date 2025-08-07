
Public Class frmBackdoorReg
    Dim strFullName As String
    Dim strLocation As String
    Dim strGeneratedKeyOutput As String

    Private Sub frmBackdoorReg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Initialize()
    End Sub
    Private Sub Initialize()
        strGeneratedKeyOutput = ""
        strLocation = "HK"
        txtGeneratedKey.Text = strGeneratedKeyOutput
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnGenerateKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateKey.Click
        Dim objBackdoorRegResponse As CRS_Util.clsJSONBusinessObj.clsBackdoorRegResponse
        strFullName = txtFullName.Text
        objBackdoorRegResponse = CRS_Util.clsJSONTool.CallBackdoorRegGenKey(strFullName, strLocation)

        If Not objBackdoorRegResponse Is Nothing Then
            If objBackdoorRegResponse.status.Equals("error") Then
                strGeneratedKeyOutput = objBackdoorRegResponse.error.message
            End If
            If objBackdoorRegResponse.status.Equals("success") Then
                strGeneratedKeyOutput = objBackdoorRegResponse.data.unlocKey
            End If
        Else
            strGeneratedKeyOutput = ""
        End If
        txtGeneratedKey.Text = strGeneratedKeyOutput

    End Sub

    Private Sub hkLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hkLocation.CheckedChanged
        strLocation = "HK"
    End Sub

    Private Sub moLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moLocation.CheckedChanged
        strLocation = "MO"
    End Sub
End Class