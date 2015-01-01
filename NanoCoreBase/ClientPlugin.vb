Public Class ClientPlugin

    Implements IClientNetwork

    ' Use Sub New to specify which plugin host types to create an instance of.
    ' These should be stored in a global variable for later use
    ' In this example, we'll create an instance of NetworkHost and LoggingHost.
    ' -----
    ' Find more in the NanoCore.ServerPluginHost or NanoCore.ClientPluginHost namespaces.
    Sub New(_networkhost As IClientNetworkHost, _loggingHost As IClientLoggingHost)

        NetworkHost = _networkhost
        LoggingHost = _loggingHost
        InitializePlugin()

    End Sub

    ' Called when the client is building its remote host cache.
    Public Sub BuildingHostCache() Implements IClientNetwork.BuildingHostCache
        ' I'm not 100% sure what use this has...
    End Sub

    ' This is called when the client fails to connect.
    Public Sub ConnectionFailed(host As String, port As UShort) Implements IClientNetwork.ConnectionFailed
        LoggingHost.LogClientMessage(String.Format("[Base Plugin] Failed to connect to: {0}:{1}", host, port))
    End Sub

    ' This will be called when the client either connects or disconnects.
    Public Sub ConnectionStateChanged(connected As Boolean) Implements IClientNetwork.ConnectionStateChanged
        If connected Then
            ' Send a command back to the server telling it to edit the column entry.
            ' param(0): This should always be the command type, in this case "editColumn"
            ' param(1): This is the column name, in this case "ExampleColumnEntry"
            ' param(2): This is the text you want the column entry to say. (E.g. "Working!")
            SendCommand(Commands.editColumn, "ExampleColumnEntry", "Working!")
        Else
            ' The client has been disconnected, if you want to do something when it does then do it here.
        End If
    End Sub

    ' This is called when a pipe is closed, for example when the server stops streaming the clients screen.
    Public Sub PipeClosed(pipeName As String) Implements IClientNetwork.PipeClosed
        LoggingHost.LogClientMessage(String.Format("[Base Plugin] The pipe {0} has been closed.", pipeName))
    End Sub

    ' This is called when a pipe is created, for example when the server starts streaming the clients screen.
    Public Sub PipeCreated(pipeName As String) Implements IClientNetwork.PipeCreated
        LoggingHost.LogClientMessage(String.Format("[Base Plugin] The pipe {0} has been created.", pipeName))
    End Sub

    ' ReadPacket is called when the client reads a packet sent by the server.
    ' -----
    ' Every command recieved will be passed through here and directed to what you want the server to do.
    ' 
    ' pipeName As String: If a pipe is used for the initial connection the name will be given here. (This can be useful for network heavy operations)
    ' params() As Object: These are the command parameters, params(0) should always be the command type.
    Public Sub ReadPacket(pipeName As String, params() As Object) Implements IClientNetwork.ReadPacket
        Select Case DirectCast(params(0), Commands)
            Case Commands.ExampleCommand
                LoggingHost.LogClientMessage("[Base Plugin] Command recieved! Message: " & params(1))
        End Select
    End Sub
End Class
