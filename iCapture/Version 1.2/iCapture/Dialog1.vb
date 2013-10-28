Imports System.Windows.Forms

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        My.Forms.Capture.delay = NumericUpDown1.Value * 1000
        My.Forms.Capture.DoTimedSnap()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub

    Private Sub Dialog1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        Dim del As String = My.Computer.Registry.CurrentUser.OpenSubKey("Software\K15\iCapture\").GetValue("delay")
        If del <> "" Then
            NumericUpDown1.Value = Convert.ToInt16(del) / 1000
        End If
    End Sub
End Class
