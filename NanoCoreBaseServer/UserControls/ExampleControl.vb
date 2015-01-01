Public Class ExampleControl

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UIHost.ShowToastNotification(Nothing, "Example.", "This is an example toast", "YourIcon.png", 1000, "", Nothing)
    End Sub
End Class
