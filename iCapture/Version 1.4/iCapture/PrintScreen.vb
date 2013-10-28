Module PrintScreen

    Declare Function GetAsyncKeyState Lib "user32" Alias "GetAsyncKeyState" (ByVal vKey As Long) As Short
    Declare Function keybd_event Lib "user32" Alias "keybd_event" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Long, ByVal dwExtraInfo As Long) As Long


    Public Sub PrintScreen()
        If Flash.Visible = True Then
            Flash.Opacity = 0
            Flash.Close()
        End If

        keybd_event(44, 0, 0, 0)
        keybd_event(44, 0, 2, 0)
        While GetAsyncKeyState(44) <> 0
            keybd_event(44, 0, 2, 0)

        End While
    End Sub

    Private Sub timerwait()

    End Sub

End Module
