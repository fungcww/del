Public Class clsWindowPool

    Public wndPool() As Windows.Forms.Form
    Public wndCnt As Integer

    Public Sub New()
        wndCnt = -1
    End Sub

    Public Function NewWind(ByVal objWnd As Windows.Forms.Form) As Integer

        wndCnt += 1
        ReDim Preserve wndPool(wndCnt)

        wndPool(wndCnt) = objWnd
        NewWind = wndCnt
        objWnd.Show()

    End Function

    Public Sub CloseWind(ByVal intHandle As Integer)

        wndPool(intHandle).Close()
        wndPool(intHandle) = Nothing

    End Sub

End Class
