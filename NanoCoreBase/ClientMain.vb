Module ClientMain

#Region "   Plugin Hosts    "
    ' Declare plugin host types.
    ' Please remember to create an instance of these.
    Public LoggingHost As IClientLoggingHost
    Public NetworkHost As IClientNetworkHost
#End Region

    Public Sub SendCommand(ParamArray params As Object())
        Try
            ' Pipename: This is the pipename you want to use, usually nothing.
            ' Compress: Only set this to false if you precompress your command.
            ' params: These are the parameters that you want to send to the client
            ' The first parameter should always be the command type.
            NetworkHost.SendToServer(Nothing, True, params)
        Catch ex As Exception
            ' Catch any exceptions when sending the command, this is useful for bug testing.
            LoggingHost.LogClientException(ex, "Sending Command")
        End Try
    End Sub

    Public Sub InitializePlugin()
        ' Empty for now, I may add some stuff here later.
    End Sub

End Module
