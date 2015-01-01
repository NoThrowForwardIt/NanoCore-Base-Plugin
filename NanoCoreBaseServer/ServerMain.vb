Module ServerMain

#Region "   Plugin Hosts    "
    ' Declare plugin host types.
    ' Please remember to create an instance of these.
    Public NetworkHost As IServerNetworkHost
    Public UIHost As IServerUIHost
    Public LoggingHost As IServerLoggingHost
#End Region

#Region "   Declare Controls   "
    ' Declare the controls that you want to use.
    ' Always create a new instance of these.
    Public NewExampleControl As New ExampleControl
#End Region

    ' This is my SendCommand subroutine, this will send a command to the client.
    Public Sub SendCommand(Client As IClient, ParamArray params As Object())
        ' Client: This can easily be accessed using the ClickedCallback.
        ' Pipename: This is the pipename you want to use, usually nothing.
        ' Compress: Only set this to false if you precompress your command.
        ' params: These are the parameters that you want to send to the client
        ' The first parameter should always be the command type.
        Try
            NetworkHost.SendToClient(Client, Nothing, True, params)
        Catch ex As Exception
            ' Catch any exceptions when sending the command, this is useful for bug testing.
            LoggingHost.LogServerException(ex, "Sending Command")
        End Try
    End Sub

    Public Sub InitializePlugin()
        Try

            ' When creating UI elements make sure to create a new instance of the IServerUIHost and give it a global variable.
            ' For example: Public UIHost As IServerUIHost (UIHost is what I will be using as my global variable)
            ' For more information on creating new instances of hosts check out the ServerPlugin class.

            ' Context entries are the best way of selecting which commands to send to the client.
            UIHost.AddContextEntry(New ContextEntry With {.Name = "Example", .Icon = "YourIcon.png", .Children = {New ContextEntry With {.Name = "Child Entry Example", .ClickedCallback = AddressOf SendExampleCommand}}})

            ' Tab entries will create a new tab with a usercontrol, it's essential to create a new instance of your user controls.
            UIHost.AddTabEntry(New TabEntry With {.Name = "Example TAB", .Icon = "YourIcon.png", .CategoryName = "Example", .UserControl = NewExampleControl})

            ' ColumnEntries will create columns that you can edit with information about the client.
            ' Check out ClientReadPacket for more information on columns and what they can do.
            UIHost.AddClientColumnEntry(New ColumnEntry With {.Name = "ExampleColumnEntry", .Width = 105, .AllowGrouping = True})

        Catch ex As Exception
            LoggingHost.LogServerException(ex, "UI")
        End Try
    End Sub

End Module
