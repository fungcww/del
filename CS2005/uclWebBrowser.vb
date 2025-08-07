Public Class uclWebBrowser
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents webBrowser As AxSHDocVw.AxWebBrowser
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(uclWebBrowser))
        Me.webBrowser = New AxSHDocVw.AxWebBrowser
        CType(Me.webBrowser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'webBrowser
        '
        Me.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webBrowser.Enabled = True
        Me.webBrowser.Location = New System.Drawing.Point(0, 0)
        Me.webBrowser.OcxState = CType(resources.GetObject("webBrowser.OcxState"), System.Windows.Forms.AxHost.State)
        Me.webBrowser.Size = New System.Drawing.Size(252, 224)
        Me.webBrowser.TabIndex = 0
        '
        'uclWebBrowser
        '
        Me.Controls.Add(Me.webBrowser)
        Me.Name = "uclWebBrowser"
        Me.Size = New System.Drawing.Size(252, 224)
        CType(Me.webBrowser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub browser_DocumentComplete(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent) Handles webBrowser.DocumentComplete
        '/// clean up temporary files...
        If IO.File.Exists(Application.StartupPath & "WordDoc.HTML") Then
            IO.File.Delete(Application.StartupPath & "WordDoc.HTML") '/// remove the temp file.
        End If
        If IO.Directory.Exists(Application.StartupPath & "WordDoc_files") Then
            Dim files As String() = IO.Directory.GetFiles(Application.StartupPath & "WordDoc_files")
            Dim file As String
            For Each file In files
                IO.File.Delete(file)
            Next
            IO.Directory.Delete(Application.StartupPath & "WordDoc_files") '/// remove the temp folder.
        End If
    End Sub

End Class
