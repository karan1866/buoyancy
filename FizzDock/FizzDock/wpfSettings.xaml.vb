Partial Public Class wpfSettings
    Dim down As Boolean

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


        IO.File.WriteAllText(AppPath & "\Settings\iop.h", sliiOpacity.Value / 100)
        IO.File.WriteAllText(AppPath & "\Settings\iblur.h", sliiBlur.Value / 10)
        IO.File.WriteAllText(AppPath & "\Settings\igcol.h", bgiColor.Background.ToString)
        IO.File.WriteAllText(AppPath & "\Settings\igsize.h", sliiGlow.Value / 10)


        IO.File.WriteAllText(AppPath & "\Settings\align.h", cmbAlign.Text)


        If chkStartup.IsChecked = True Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", "FizzDock", AppPath & "\FizzDock.exe", Microsoft.Win32.RegistryValueKind.String)
        Else
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", "FizzDock", "", Microsoft.Win32.RegistryValueKind.String)
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Window1_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        AutoHideOption = False
        Load()
    End Sub

    Private Sub sliRGB_ValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.RoutedPropertyChangedEventArgs(Of System.Double)) Handles sliR.ValueChanged, sliG.ValueChanged, sliB.ValueChanged
        Dim brush As New SolidColorBrush
        brush.Color = Color.FromRgb(sliR.Value, sliG.Value, sliB.Value)
        bgColor.Background = brush
        Preview()
    End Sub

    Private Sub sliiRGB_ValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.RoutedPropertyChangedEventArgs(Of System.Double)) Handles sliiR.ValueChanged, sliiG.ValueChanged, sliiB.ValueChanged
        Dim brush As New SolidColorBrush
        brush.Color = Color.FromRgb(sliiR.Value, sliiG.Value, sliiB.Value)
        bgiColor.Background = brush
        Preview()
    End Sub

    Private Sub Preview() Handles sliBlur.ValueChanged, sliGlow.ValueChanged, sliOpacity.ValueChanged, cmbSkin.SelectionChanged, sliiBlur.ValueChanged, sliiOpacity.ValueChanged, cmbAlign.SelectionChanged
        On Error Resume Next
        Dim Align As String = Nothing

        Select Case cmbAlign.SelectedIndex
            Case 0
                Align = "Top"
            Case 1
                Align = "Bottom"
            Case 2
                Align = "Super Bottom"
        End Select

        Dock.Preview(sliOpacity.Value / 100, sliBlur.Value / 10, sliGlow.Value / 10, cmbSkin.SelectedItem, bgColor.Background, Align, sliiOpacity.Value / 100, sliiBlur.Value / 10, sliiGlow.Value / 10, bgiColor.Background)
    End Sub

    Private Sub Load()
        Dim dir As New DirListBox
        Dim i As Integer

        If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", "FizzDock", "") = AppPath & "\FizzDock.exe" Then
            chkStartup.IsChecked = True
        Else
            chkStartup.IsChecked = False
        End If

        cmbSkin.SelectedItem = IO.File.ReadAllText(AppPath & "\Settings\skin.h")

        dir.Path = AppPath & "\Skins"
        dir.SelectedItem = AppPath & "\Skins"

        For i = 1 To dir.Count
            dir.Index = i - 1
            cmbSkin.Items.Add(dir.SelectedItem)
        Next

        sliR.Value = 324234
        sliiR.Value = 324234

        Dim myCol As Color = ColorConverter.ConvertFromString(IO.File.ReadAllText(AppPath & "\Settings\gcol.h"))
        sliR.Value = myCol.R
        sliG.Value = myCol.G
        sliB.Value = myCol.B


        sliGlow.Value = IO.File.ReadAllText(AppPath & "\Settings\gsize.h") * 10
        sliOpacity.Value = IO.File.ReadAllText(AppPath & "\Settings\op.h") * 100
        sliBlur.Value = IO.File.ReadAllText(AppPath & "\Settings\blur.h") * 10



        myCol = ColorConverter.ConvertFromString(IO.File.ReadAllText(AppPath & "\Settings\igcol.h"))
        sliiR.Value = myCol.R
        sliiG.Value = myCol.G
        sliiB.Value = myCol.B

        sliiGlow.Value = IO.File.ReadAllText(AppPath & "\Settings\igsize.h") * 10
        sliiOpacity.Value = IO.File.ReadAllText(AppPath & "\Settings\iop.h") * 100
        sliiBlur.Value = IO.File.ReadAllText(AppPath & "\Settings\iblur.h") * 10


        chkAutoHide.IsChecked = IO.File.ReadAllText(AppPath & "\Settings\ahide.h")

        Select Case IO.File.ReadAllText(AppPath & "\Settings\align.h")
            Case "Top"
                cmbAlign.SelectedIndex = 0
            Case "Bottom"
                cmbAlign.SelectedIndex = 1
            Case "Super Bottom"
                cmbAlign.SelectedIndex = 2
        End Select
    End Sub

    Private Sub Move_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Move.MouseDown
        down = True
    End Sub

    Private Sub Move_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles Move.MouseUp
        down = False
    End Sub

    Private Sub Move_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles Move.MouseMove
        If down = True Then
            If e.LeftButton = MouseButtonState.Pressed Then
                Me.DragMove()
            End If
        End If
    End Sub
End Class
