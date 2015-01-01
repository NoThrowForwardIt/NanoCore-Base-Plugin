Public Class ServerPlugin

    Implements IServerNetwork

    ' Use Sub New to specify which plugin host types to create an instance of.
    ' These should be stored in a global variable for later use
    ' In this example, we'll create an instance of NetworkHost, LoggingHost, and UIHost
    ' -----
    ' Find more in the NanoCore.ServerPluginHost or NanoCore.ClientPluginHost namespaces.
    Sub New(_networkHost As IServerNetworkHost, _loggingHost As IServerLoggingHost, _uiHost As IServerUIHost)
        Try
            NetworkHost = _networkHost
            LoggingHost = _loggingHost
            UIHost = _uiHost

            InitializePlugin()
        Catch ex As Exception
        End Try
    End Sub

    ' This is called when a new connection pipe is created.
    ' -----
    ' For example, when you start streaming a clients screen
    ' A new connection pipe will be opened so that clients will
    ' not lag when connecting.
    Public Sub ClientPipeCreated(client As IClient, pipeName As String) Implements IServerNetwork.ClientPipeCreated
        ' Add the notification to the event manager, including the pipename.
        NewExampleControl.EventManager.Items.Add("Pipe Created").SubItems.AddRange({"""" & pipeName & """ has been created.", TimeValue(Now)})
    End Sub

    ' This is called when a connection pipe is closed.
    ' For example when you stop streaming a clients screen.
    Public Sub ClientPipeClosed(client As IClient, pipeName As String) Implements IServerNetwork.ClientPipeClosed
        ' Add the notification to the event manager, including the pipename.
        NewExampleControl.EventManager.Items.Add("Pipe Created").SubItems.AddRange({"""" & pipeName & """ has been closed.", TimeValue(Now)})
    End Sub

    ' ClientReadPacket is called when the server reads a packet sent by the client.
    ' -----
    ' Every command recieved will be passed through here and directed to what you want the server to do.
    ' 
    ' client As IClient: This is the client that sent the command, this is useful for client specific information.
    ' pipeName As String: If a pipe is used for the initial connection the name will be given here. (This can be useful for network heavy operations)
    ' params() As Object: These are the command parameters, params(0) should always be the command type.

    Public Sub ClientReadPacket(client As IClient, pipeName As String, params() As Object) Implements IServerNetwork.ClientReadPacket
        ' This select case will be getting the command and executing the code that you want.
        ' params(0) should always be the command type. So we are using that.
        Select Case DirectCast(params(0), Commands)
            Case Commands.editColumn
                ' If the command is editColumn then we are going to edit the column entry.
                ' params(1) and params(2) were set client side before being sent over.
                UIHost.SetClientColumnValue(client, params(1), Nothing, params(2))
        End Select
    End Sub

    ' This is called when the client state is changed
    ' For example when the client connects and disconnects from the server.
    ' -----
    ' The "Connected" boolean is used to determain whether or not the 
    ' client is connected When the subroutine is called.
    Public Sub ClientStateChanged(client As IClient, connected As Boolean) Implements IServerNetwork.ClientStateChanged
        ' If the ClientState has been changed too connected then...
        If (connected) Then
            ' Add the notification to the event manager, including the client name.
            NewExampleControl.EventManager.Items.Add("Client State").SubItems.AddRange({client.Id.ToString & " has connected!", TimeValue(Now)})
        Else
            NewExampleControl.EventManager.Items.Add("Client State").SubItems.AddRange({client.Id.ToString & " has disconnected!", TimeValue(Now)})
        End If
    End Sub

    ' ListenerAdded is called when a port is added.
    Public Sub ListenerAdded(listener As IListener) Implements IServerNetwork.ListenerAdded
        NewExampleControl.EventManager.Items.Add("Listner Added").SubItems.AddRange({listener.PortNumber & " has been added with " & listener.Connections & " connections.", TimeValue(Now)})
    End Sub

    ' ListenerFailed is called when a port fails to listen.
    ' For example if the port is blocked via firewall.
    Public Sub ListenerFailed(listener As IListener) Implements IServerNetwork.ListenerFailed
        NewExampleControl.EventManager.Items.Add("Listner Failed").SubItems.AddRange({listener.PortNumber & " has been failed with " & listener.Connections & " connections.", TimeValue(Now)})
    End Sub

    ' ListenerRemoved is called when a port has been removed.
    Public Sub ListenerRemoved(listener As IListener) Implements IServerNetwork.ListenerRemoved
        NewExampleControl.EventManager.Items.Add("Listner Removed").SubItems.AddRange({listener.PortNumber & " has been removed with " & listener.Connections & " connections.", TimeValue(Now)})
    End Sub

    ' ListenerStateChanged is called when the port starts listening or stop listening.
    Public Sub ListenerStateChanged(listener As IListener) Implements IServerNetwork.ListenerStateChanged
        ' If the port has been disabled then add the event.
        If listener.Status = ListenerStatus.Disabled Then
            NewExampleControl.EventManager.Items.Add("Listner Disabled").SubItems.AddRange({listener.PortNumber & " has been disabled with " & listener.Connections & " connections.", TimeValue(Now)})
            ' If the port has started listning then add the event.
        ElseIf listener.Status = ListenerStatus.Listening Then
            NewExampleControl.EventManager.Items.Add("Listner Disabled").SubItems.AddRange({listener.PortNumber & " is listning for connections.", TimeValue(Now)})
        End If
    End Sub
End Class
