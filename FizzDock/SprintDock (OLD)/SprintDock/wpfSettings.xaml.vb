Partial Public Class wpfSettings

    Private Sub Menu_Unselected(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim blur As New Windows.Media.Effects.BlurBitmapEffect
        blur.Radius = 4
        sender.BitmapEffect = blur
    End Sub

    Private Sub wpfSettings_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        CheckAutoHide()
    End Sub

    Private Sub cmdInstall_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim ofd1 As New Windows.Forms.OpenFileDialog
        ofd1.Filter = "SprintDock Skin (*.sds)|*.sds"
        If ofd1.ShowDialog = Forms.DialogResult.OK Then
            On Error GoTo yo
            Shell(ofd1.FileName)
        End If
        Exit Sub
yo:
        MsgBox("Could not load file. The file is either invalid or corrupt.")
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Preview()
        IO.File.WriteAllText(AppPath & "\Settings\op.h", sliOpacity.Value / 100)
        IO.File.WriteAllText(AppPath & "\Settings\ahide.h", chkAutoHide.IsChecked.ToString)
        IO.File.WriteAllText(AppPath & "\Settings\blur.h", sliBlur.Value / 10)
        IO.File.WriteAllText(AppPath & "\Settings\gcol.h", bgColor.Background.ToString)
        IO.File.WriteAllText(AppPath & "\Settings\gsize.h", sliGlow.Value / 10)
        IO.File.WriteAllText(AppPath & "\Settings\skin.h", cmbSkin.Text)
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Window1_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        AutoHideOption = False
        Load()
    End Sub

    Private Sub sliR_ValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.RoutedPropertyChangedEventArgs(Of System.Double)) Handles sliR.ValueChanged, sliG.ValueChanged, sliB.ValueChanged
        Dim brush As New SolidColorBrush
        brush.Color = Color.FromRgb(sliR.Value, sliG.Value, sliB.Value)
        bgColor.Background = brush
        Preview()
    End Sub

    Private Sub cmdGlowColor_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdGlowColor.Click
        If ColorDialog.Visibility = Windows.Visibility.Hidden Then
            ColorDialog.Visibility = Windows.Visibility.Visible
        Else
            ColorDialog.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub Preview() Handles sliBlur.ValueChanged, sliGlow.ValueChanged, sliOpacity.ValueChanged, cmbSkin.SelectionChanged
        On Error Resume Next
        Dock.Preview(sliOpacity.Value / 100, sliBlur.Value / 10, sliGlow.Value / 10, cmbSkin.SelectedItem, bgColor.Background)
    End Sub

    Private Sub Load()
        Dim dir As New DirListBox
        Dim i As Integer


        cmbSkin.SelectedItem = IO.File.ReadAllText(AppPath & "\Settings\skin.h")

        dir.Path = AppPath & "\Skins"
        dir.SelectedItem = AppPath & "\Skins"

        For i = 1 To dir.Count
            dir.Index = i - 1
            cmbSkin.Items.Add(dir.SelectedItem)
        Next

        sliR.Value = 324234
        Dim myCol As Color = ColorConverter.ConvertFromString(IO.File.ReadAllText(AppPath & "\Settings\gcol.h"))
        sliR.Value = myCol.R
        sliG.Value = myCol.G
        sliB.Value = myCol.B

        sliGlow.Value = IO.File.ReadAllText(AppPath & "\Settings\gsize.h") * 10
        sliOpacity.Value = IO.File.ReadAllText(AppPath & "\Settings\op.h") * 100
        sliBlur.Value = IO.File.ReadAllText(AppPath & "\Settings\blur.h") * 10

        chkAutoHide.IsChecked = IO.File.ReadAllText(AppPath & "\Settings\ahide.h")
    End Sub
End Class
