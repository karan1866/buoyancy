Public Class frmCapture
    Dim visi As Boolean


    Private Sub frmCapture_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Focus()
    End Sub

    Private Sub pcbMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcbMain.Click
        On Error Resume Next

        frmFullScreen.BackgroundImage = pcbMain.BackgroundImage
        frmFullScreen.Show()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        On Error Resume Next
        Dim ofd As New SaveFileDialog
        ofd.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            pcbMain.BackgroundImage.Save(ofd.FileName)
            Me.Close()
        End If
    End Sub
End Class
