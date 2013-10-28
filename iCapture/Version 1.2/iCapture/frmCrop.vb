Imports System.Drawing


Public Class frmCrop


    Dim p1 As Point
    Dim p2 As Point
    Dim down As Boolean
    Public result As DialogResult

    Private Sub frmCrop_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.DialogResult = result
    End Sub

    Private Sub frmCrop_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(13) And cropper.Visible = True Then
            Me.Opacity = 0
            'Dim a As New ScreenCap
            'Dim scr As Image = a.CaptureScreen
yo:
            PrintScreen.PrintScreen()
            Dim scr As Image = Clipboard.GetImage
            Dim crop As Bitmap = New Bitmap(cropper.Width, cropper.Height)
            Try
                Dim s As Integer = scr.Height
            Catch
                GoTo yo
            End Try
            Dim bit As Bitmap = New Bitmap(scr, scr.Width, scr.Height)
            Dim g As Graphics = Graphics.FromImage(crop)
            g.DrawImage(bit, 0, 0, New Drawing.RectangleF(cropper.Location.X, cropper.Location.Y, cropper.Width, cropper.Height), GraphicsUnit.Pixel)
            My.Forms.Capture.img = crop
            Clipboard.SetImage(crop)
            result = Windows.Forms.DialogResult.OK
            Me.Close()
        ElseIf e.KeyChar = Chr(32) Then
            cropper.Visible = False
        ElseIf e.KeyChar = Chr(27) Then
            result = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub


    Private Sub frmCrop_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        p1 = MousePosition
        down = True
    End Sub

    Private Sub frmCrop_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If down = True Then
            'Me.Visible = False

            p2 = MousePosition
            Dim np1, np2 As Point
            If p1.X < p2.X Then
                np1.X = p1.X
                np2.X = p2.X
            Else
                np1.X = p2.X
                np2.X = p1.X
            End If
            If p1.Y < p2.Y Then
                np1.Y = p1.Y
                np2.Y = p2.Y
            Else
                np1.Y = p2.Y
                np2.Y = p1.Y
            End If
            'nsize.X = np2.X - np1.X
            'nsize.Y = np2.Y - np1.Y
            'cropper.Size = nsize
            'cropper.Location = np1
            'cropper.Size = nsize
            Me.Padding = New Windows.Forms.Padding(np1.X, np1.Y, Me.Width - np2.X, Me.Height - np2.Y)
            cropper.Visible = True
        End If
    End Sub

    Private Sub frmCrop_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        down = False
    End Sub

    Private Sub frmCrop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Focus()
    End Sub
End Class