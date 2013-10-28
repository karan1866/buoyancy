Public NotInheritable Class frmAbout

    Private Sub frmAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.Close()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        On Error Resume Next
        Process.Start("https://facebook.com/karan1866")
    End Sub
End Class
