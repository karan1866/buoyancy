Public Class Flash

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.Opacity = 0 Then
            Me.Close()
            My.Forms.Capture.tmr1.Enabled = True
        End If
        Me.Opacity -= 0.02
    End Sub

    Private Sub Flash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Forms.Capture.tmr1.Enabled = False
    End Sub
End Class