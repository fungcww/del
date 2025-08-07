Imports CRS_Util
Imports CRS_Util.ExtensionMethods
Imports System.Configuration

Module Module1

    Sub Main()
        Dim strerr As String = ""
        Dim ds As DataSet = Utility.getEnvironmentSettingsDataSet("INGU105", System.Configuration.ConfigurationManager.AppSettings("ConfigFilePath"), strerr)


    End Sub

End Module
