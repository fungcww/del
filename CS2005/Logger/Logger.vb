Imports log4net
Public Class Logger
	Public logger As log4net.ILog
	Sub New()
		logger = log4net.LogManager.GetLogger("CS2005Logger")
		logger.Info("logger() - Start")
		'logger.Debug("logger() - Code Implementation goes here......")
		'logger.Error("Error Log")
	End Sub

End Class
