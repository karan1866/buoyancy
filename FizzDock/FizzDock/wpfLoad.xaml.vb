Partial Public Class wpfLoad
    Dim wait As New Forms.Timer With {.Interval = 3000, .Enabled = True}

    Private Sub wpfLoad_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim ic As New ImageSourceConverter

        Me.Opacity = 0
        Dim PositionTimer As New Windows.Forms.Timer With {.Interval = 1, .Enabled = True}
        AddHandler PositionTimer.Tick, AddressOf Position

        Me.Left = Forms.Cursor.Position.X
        Me.Top = Forms.Cursor.Position.Y

        Dim da As New Windows.Media.Animation.DoubleAnimation With {.From = 0, .To = 1, .Duration = TimeSpan.FromMilliseconds(250)}
        AddHandler da.Completed, AddressOf OpenCompleted

        Me.BeginAnimation(wpfLoad.OpacityProperty, da)
    End Sub

    Private Sub Position()
        If Me.Left <> Forms.Cursor.Position.X Or Me.Top <> Forms.Cursor.Position.Y Then
            Me.Left = Forms.Cursor.Position.X
            Me.Top = Forms.Cursor.Position.Y
        End If
    End Sub

    Private Sub OpenCompleted()
        AddHandler wait.Tick, AddressOf WaitCompleted
    End Sub

    Private Sub WaitCompleted()
        wait.Enabled = False
        Dim da As New Windows.Media.Animation.DoubleAnimation With {.From = 1, .To = 0, .Duration = TimeSpan.FromMilliseconds(250)}
        AddHandler da.Completed, AddressOf CloseCompleted

        Me.BeginAnimation(wpfLoad.OpacityProperty, da)
    End Sub

    Private Sub CloseCompleted()
        Dim wpfDock As Window = New wpfDock
        wpfDock.Show()
        Me.Close()
    End Sub
End Class
