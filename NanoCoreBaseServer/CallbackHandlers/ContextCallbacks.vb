Module ContextCallbacks

    ' This is an example command that will be sent to the client.
    ' Clients As IClient(): These are the clients that were selected when the context entry was clicked.
    ' Checked As Boolean: This will be used for checkboxes within context entries.
    Public Sub SendExampleCommand(Clients As IClient(), Checked As Boolean)

        ' If no clients were selected then don't try send any command.
        If Not Clients.Length > 0 Then Return

        ' Loop over every selected client and send the command.
        For Each Client As IClient In Clients
            SendCommand(Client, Commands.ExampleCommand, "Testing...")
        Next

    End Sub


End Module
