
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Configuration

Namespace App.Config
    Public Class AppSettings
        Private Property _ConfigJson As JObject
        Private Property _Environment As Environment
        Private Property _Env As Env
        Private Property _Company As Company
        Private Property _ConfigFile As String
        Private Property _LAHDBHeader As DBHeader = New DBHeader
        Private Property _LAHMQHeader As MQHeader = New MQHeader
        Private Property _LACDBHeader As DBHeader = New DBHeader
        Private Property _LACMQHeader As MQHeader = New MQHeader

        Public ReadOnly Property LAHDBHeader As DBHeader
            Get
                _LAHDBHeader.UserID = ConfigJson("LAH_LAUser").ToString()
                _LAHDBHeader.EnvironmentUse = ConfigJson("LAH_Env").ToString()
                _LAHDBHeader.ProjectAlias = ConfigJson("LAH_ProjectAlias").ToString()
                _LAHDBHeader.CompanyID = ConfigJson("LAH_Comp").ToString()
                _LAHDBHeader.UserType = ConfigJson("LASUPDATE").ToString()
                Return _LAHDBHeader
            End Get
        End Property

        Public ReadOnly Property LAHMQHeader As MQHeader
            Get
                Try
                    _LAHMQHeader.UserID = ConfigJson("LAH_LAUser").ToString()
                    _LAHMQHeader.QueueManager = ConfigJson("LAH_Qman").ToString()
                    _LAHMQHeader.RemoteQueue = ConfigJson("LAH_WinRemoteQ").ToString()
                    _LAHMQHeader.ReplyToQueue = ConfigJson("LAH_LAReplyQ").ToString()
                    _LAHMQHeader.LocalQueue = ConfigJson("LAH_WinLocalQ").ToString()
                    _LAHMQHeader.EnvironmentUse = ConfigJson("LAH_Env").ToString()
                    _LAHMQHeader.CompanyID = ConfigJson("LAH_Comp").ToString()
                    _LAHMQHeader.Timeout = ConfigJson("LAH_Timeout").ToString()
                    _LAHMQHeader.ProjectAlias = ConfigJson("LAH_ProjectAlias").ToString()
                    _LAHMQHeader.UserType = ConfigJson("LAH_UserType").ToString()
                    _LAHMQHeader.ConnectionAlias = ConfigJson("LAH_Comp").ToString() & "CIW" & ConfigJson("LAH_Env").ToString()
                    Return _LAHMQHeader
                Catch ex As Exception
                    Return _LAHMQHeader
                End Try
            End Get
        End Property

        Public ReadOnly Property LACDBHeader As DBHeader
            Get
                _LACDBHeader.UserID = ConfigJson("LAC_LAUser").ToString()
                _LACDBHeader.EnvironmentUse = ConfigJson("LAC_Env").ToString()
                _LACDBHeader.ProjectAlias = ConfigJson("LAC_ProjectAlias").ToString()
                _LACDBHeader.CompanyID = ConfigJson("LAC_Comp").ToString()
                _LACDBHeader.UserType = ConfigJson("LASUPDATE").ToString()
                Return _LACDBHeader
            End Get
        End Property
        Public ReadOnly Property LACMQHeader As MQHeader
            Get
                _LACMQHeader.UserID = ConfigJson("LAC_LAUser").ToString()
                _LACMQHeader.QueueManager = ConfigJson("LAC_Qman").ToString()
                _LACMQHeader.RemoteQueue = ConfigJson("LAC_WinRemoteQ").ToString()
                _LACMQHeader.ReplyToQueue = ConfigJson("LAC_LAReplyQ").ToString()
                _LACMQHeader.LocalQueue = ConfigJson("LAC_WinLocalQ").ToString()
                _LACMQHeader.EnvironmentUse = ConfigJson("LAC_Env").ToString()
                _LACMQHeader.CompanyID = ConfigJson("LAC_Comp").ToString()
                _LACMQHeader.Timeout = ConfigJson("LAC_Timeout").ToString()
                _LACMQHeader.ProjectAlias = ConfigJson("LAC_ProjectAlias").ToString()
                _LACMQHeader.UserType = ConfigJson("LAC_UserType").ToString()
                _LACMQHeader.ConnectionAlias = ConfigJson("LAC_Comp").ToString() & "CIW" & ConfigJson("LAC_Env").ToString()
                Return _LACMQHeader
            End Get
        End Property

        Public Property ConfigJson As JObject
            Get
                Return _ConfigJson
            End Get
            Set(ByVal value As JObject)
                _ConfigJson = value
            End Set
        End Property

        Public ReadOnly Property Logger As Logger
            Get
                Return _Logger
            End Get
        End Property

        Sub New()
            If Not (String.IsNullOrEmpty(ConfigurationManager.AppSettings("Environment"))) Then
                ConfigFile = ConfigurationManager.AppSettings("Environment")
            End If
            ConfigJson = ReadJsonData(Me.ConfigPath, Me.ConfigFile)
        End Sub
        Public Function ReadJsonData(ByVal configPath As String, ByVal configFile As String) As JObject
            Try
                Dim Data As JObject = JObject.Parse(File.ReadAllText(IO.Path.Combine(configPath, configFile)))
                Return Data
            Catch ex As Exception

            End Try
        End Function

        Public ReadOnly Property Company As Company
            Get
                Try
                    If (Not String.IsNullOrEmpty(ConfigJson("Company"))) Then
                        _Company = ConfigJson("Company").ToString
                    Else
                        Throw New System.Exception("Company is not set.")
                    End If
                Catch ex As Exception
                    _Company = Company.ING
                    _Logger.logger.Error(ex.Message)
                End Try
            End Get

        End Property

        Public Property ConfigFile As String
            Get
                Try
                    Return _ConfigFile
                Catch ex As Exception
                    Return ""
                End Try
            End Get
            Set(value As String)
                If String.IsNullOrEmpty(value) = False Then
                    _ConfigFile = String.Format("appSetting.{0}.json", value)
                    ConfigJson = ReadJsonData(Me.ConfigPath, Me.ConfigFile)
                End If
            End Set
        End Property

        Public Property Environment As Environment
            Get
                Try
                    Return _Environment
                Catch ex As Exception
                    Return ""
                End Try
            End Get
            Set(value As Environment)
                _Environment = value
            End Set
        End Property

        Public Property Env As Env
            Get
                Try
                    Return _Env
                Catch ex As Exception
                    Return ""
                End Try
            End Get
            Set(value As Env)
                _Env = value
            End Set
        End Property
        Public ReadOnly Property ConfigPath As String
            Get
                Try
                    If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("JsonConfigPath")) Then
                        Return ConfigurationManager.AppSettings("JsonConfigPath").ToString
                    Else
                        Return Directory.GetCurrentDirectory()
                    End If
                Catch ex As Exception
                    Return ""
                End Try
            End Get

        End Property

        Public ReadOnly Property gSQL3 As String
            Get
                Try
                    Return ConfigJson("cSQL3").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property cSQL3 As String
            Get
                Try
                    Return ConfigJson("cSQL3").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property CSW_USER_PRIVS
            Get
                Try
                    Return ConfigJson("CSW_USER_PRIVS").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property UPS_USER_GROUP_TAB
            Get
                Try
                    Return ConfigJson("UPS_USER_GROUP_TAB").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property UPS_USER_LIST_TAB
            Get
                Try
                    Return ConfigJson("UPS_USER_LIST_TAB").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property CIC_FULLNAME_MAPPING
            Get
                Try
                    Return ConfigJson("CIC_FULLNAME_MAPPING").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_LAUser
            Get
                Try
                    Return ConfigJson("g_LAUser").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Qman
            Get
                Try
                    Return ConfigJson("g_Qman").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_WinRemoteQ
            Get
                Try
                    Return ConfigJson("g_WinRemoteQ").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_WinRemoteMcuQ
            Get
                Try
                    Return ConfigJson("g_WinRemoteMcuQ").ToString
                Catch
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_LAReplyQ
            Get
                Try
                    Return ConfigJson("g_LAReplyQ").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_LAReplyMcuQ
            Get
                Try
                    Return ConfigJson("g_LAReplyMcuQ").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_WinLocalQ
            Get
                Try
                    Return ConfigJson("g_WinLocalQ").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_WinLocalMcuQ
            Get
                Try
                    Return ConfigJson("g_WinLocalMcuQ").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Env
            Get
                Try
                    Return ConfigJson("g_Env").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_McuEnv
            Get
                Try
                    Return ConfigJson("g_McuEnv").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Comp
            Get
                Try
                    Return ConfigJson("g_Comp").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_McuComp
            Get
                Try
                    Return ConfigJson("g_McuComp").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_ProjectAlias
            Get
                Try
                    Return ConfigJson("g_ProjectAlias").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_UserType
            Get
                Try
                    Return ConfigJson("g_UserType").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_CIW
            Get
                Try
                    Return ConfigJson("g_Connection_CIW").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_McuCIW
            Get
                Try
                    Return ConfigJson("g_Connection_McuCIW").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_UPS
            Get
                Try
                    Return ConfigJson("g_Connection_UPS").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_MCS
            Get
                Try
                    Return ConfigJson("g_Connection_MCS").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_ICR
            Get
                Try
                    Return ConfigJson("g_Connection_ICR").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_UserType_ICR
            Get
                Try
                    Return ConfigJson("g_UserType_ICR").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_ProjectAlias_MJC
            Get
                Try
                    Return ConfigJson("g_ProjectAlias_MJC").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_MJC
            Get
                Try
                    Return ConfigJson("g_Connection_MJC").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_CAM
            Get
                Try
                    Return ConfigJson("g_Connection_CAM").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Capsil_Project
            Get
                Try
                    Return ConfigJson("g_Capsil_Project").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Capsil_Connection
            Get
                Try
                    Return ConfigJson("g_Capsil_Connection").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Capsil_UserType
            Get
                Try
                    Return ConfigJson("g_Capsil_UserType").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Capsil_Lib
            Get
                Try
                    Return ConfigJson("g_Capsil_Lib").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_CAM_Database
            Get
                Try
                    Return ConfigJson("g_CAM_Database").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_McuCAM_Database
            Get
                Try
                    Return ConfigJson("g_McuCAM_Database").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_McuNBR_Database
            Get
                Try
                    Return ConfigJson("g_McuNBR_Database").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcNBR
            Get
                Try
                    Return ConfigJson("gcNBR").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property UAT
            Get
                Try
                    Return ConfigJson("UAT").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property strLASCAM
            Get
                Try
                    Return ConfigJson("strLASCAM").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property strValProj
            Get
                Try
                    Return ConfigJson("strValProj").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property strValConn
            Get
                Try
                    Return ConfigJson("strValConn").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property g_Connection_CNB
            Get
                Try
                    Return ConfigJson("g_Connection_CNB").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcCIW
            Get
                Try
                    Return ConfigJson("gcCIW").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcMcuCIW
            Get
                Try
                    Return ConfigJson("gcMcuCIW").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcMCS
            Get
                Try
                    Return ConfigJson("gcMCS").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcMcuMCS
            Get
                Try
                    Return ConfigJson("gcMcuMCS").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcRMLife
            Get
                Try
                    Return ConfigJson("gcRMLife").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcPOS
            Get
                Try
                    Return ConfigJson("gcPOS").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property
        Public ReadOnly Property gcMcuPOS
            Get
                Try
                    Return ConfigJson("gcMcuPOS").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property

        Public ReadOnly Property giEnv
            Get
                Try
                    Return Integer.Parse(ConfigJson("giEnv").ToString())
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property

        Public ReadOnly Property g_UserType_MJC
            Get
                Try
                    Return ConfigJson("g_UserType_MJC").ToString
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property

    End Class

    Public Enum Environment
        LOCAL
        SIT
        UAT
        REG
        PRD
    End Enum

    Public Enum Env
        U101
        U105
        S106
        T106
        U201
        U202
        U301
    End Enum

    Public Enum Company
        ING
        MCU
        LAC
        LAH
    End Enum

    Public Class MQHeader
        Public Property UserID As String
        Public Property QueueManager As String
        Public Property RemoteQueue As String
        Public Property ReplyToQueue As String
        Public Property LocalQueue As String
        Public Property EnvironmentUse As String
        Public Property CompanyID As String
        Public Property Timeout As String
        Public Property ProjectAlias As String
        Public Property UserType As String
        Public Property ConnectionAlias As String
    End Class

    Public Class DBHeader
        Public Property UserID As String
        Public Property EnvironmentUse As String
        Public Property ProjectAlias As String
        Public Property CompanyID As String
        Public Property UserType As String

    End Class

    Public Class DBSOAPHeader
        Public Property Comp As String
        Public Property Project As String
        Public Property UserType As String
        Public Property Env As String
        Public Property ConnectionAlias As String

    End Class

End Namespace