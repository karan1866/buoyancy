Imports System.Windows.Media.Animation

Partial Public Class wpfDock
    Dim AutoHide As New Windows.Forms.Timer
    Dim down As Boolean
    Dim SettingViewed As Boolean
    Dim up As Boolean
    Dim mup As Boolean
    Dim CloseTimer As New Windows.Forms.Timer

    Private Sub Window1_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        AppPath = CurDir()

        If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp & "\sprintdock.path") = False Then
            IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\sprintdock.path", AppPath)
        Else
            AppPath = IO.File.ReadAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\sprintdock.path")
        End If


        'Hide Form for Loading and Startup Effect
        Me.Opacity = 0


        'AutoHide Timer
        AutoHide.Interval = 1000
        AutoHide.Enabled = True
        AddHandler AutoHide.Tick, AddressOf AutoHide_Tick


        CloseTimer.Enabled = False
        CloseTimer.Interval = 1
        AddHandler CloseTimer.Tick, AddressOf CloseTimer_Tick


        'AutoHide/Show Controler Timer
        Dim Auto As New Windows.Forms.Timer
        Auto.Enabled = True
        Auto.Interval = 1
        AddHandler Auto.Tick, AddressOf Auto_Tick


        LoadIcons()
        Setting()


        Dim a As New ContextMenu
        Dim mnuExit As New MenuItem
        Dim mnuDockSettings As New MenuItem
        Dim mnuIconSettings As New MenuItem
        Dim sep As New Separator


        'Add Item
        mnuExit.Header = "Exit"
        mnuDockSettings.Header = "Dock Settings"
        mnuIconSettings.Header = "Icon Settings"
        AddHandler mnuExit.Click, AddressOf mnuExit_Click
        AddHandler mnuDockSettings.Click, AddressOf mnuDockSettings_Click
        AddHandler mnuIconSettings.Click, AddressOf mnuIconSettings_CLick


        'Dock Context Menu Strip
        a.Items.Add(mnuDockSettings)
        a.Items.Add(mnuIconSettings)
        a.Items.Add(sep)
        a.Items.Add(mnuExit)
        imgLeft.ContextMenu = a
        imgRight.ContextMenu = a
        Icons.ContextMenu = a
        imgCenter.ContextMenu = a
        AddHandler a.Opened, AddressOf ContextMenu_Opened
        AddHandler a.Closed, AddressOf ContextMenu_LostFocus


        'Set Perfect Height
        Me.Height = 114



        'Centering
        Me.Left = (My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2)
        Me.Top = -25

        'Startup Effect
        AutoHideShow()
    End Sub

    Private Sub MyIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        If Mouse.LeftButton = MouseButtonState.Pressed = True Then
            mup = False
            If up = True Then
                down = True
                Dim a As New Windows.Media.Effects.OuterGlowBitmapEffect
                a.GlowColor = Windows.Media.Colors.WhiteSmoke
                a.GlowSize = 0
                sender.BitmapEffect = a


                Dim da As New DoubleAnimation
                da.From = 7
                da.To = 5
                da.Duration = TimeSpan.FromMilliseconds(150)

                Dim da2 As New ColorAnimation
                da2.From = Colors.WhiteSmoke
                da2.To = Colors.Black
                da2.Duration = TimeSpan.FromMilliseconds(150)


                sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowColorProperty, da2)
                sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowSizeProperty, da)
            Else
                down = True
                Dim a As New Windows.Media.Effects.OuterGlowBitmapEffect
                a.GlowColor = Windows.Media.Colors.WhiteSmoke
                a.GlowSize = 0
                sender.BitmapEffect = a


                Dim da As New DoubleAnimation
                da.From = 0
                da.To = 5
                da.Duration = TimeSpan.FromMilliseconds(150)

                Dim da2 As New ColorAnimation
                da2.From = Colors.Transparent
                da2.To = Colors.Black
                da2.Duration = TimeSpan.FromMilliseconds(150)


                sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowColorProperty, da2)
                sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowSizeProperty, da)
            End If
        End If
    End Sub

    Private Sub MyIcon_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs)
        up = True
        mup = False
        If Mouse.LeftButton = MouseButtonState.Pressed = False Then
            Dim a As New Windows.Media.Effects.OuterGlowBitmapEffect
            a.GlowColor = Windows.Media.Colors.WhiteSmoke
            a.GlowSize = 0
            sender.BitmapEffect = a


            Dim da As New DoubleAnimation
            da.From = 0
            da.To = 7
            da.Duration = TimeSpan.FromMilliseconds(150)

            Dim da2 As New ColorAnimation
            da2.From = Colors.Transparent
            da2.To = Colors.WhiteSmoke
            da2.Duration = TimeSpan.FromMilliseconds(150)


            sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowColorProperty, da2)
            sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowSizeProperty, da)
        Else
            Dim a As New Windows.Media.Effects.OuterGlowBitmapEffect
            a.GlowColor = Windows.Media.Colors.Black
            sender.BitmapEffect = a
            Dim da As New DoubleAnimation
            da.From = 0
            da.To = 5
            da.Duration = TimeSpan.FromMilliseconds(150)

            Dim da2 As New ColorAnimation
            da2.From = Windows.Media.Colors.Transparent
            da2.To = a.GlowColor
            da2.Duration = TimeSpan.FromMilliseconds(150)

            sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowColorProperty, da2)
            sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowSizeProperty, da)
            down = True
        End If
    End Sub

    Private Sub MyIcon_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs)
        If mup = True Then
            Exit Sub
        End If


        Dim da As New DoubleAnimation
        Dim da2 As New ColorAnimation


        Dim a As New Windows.Media.Effects.OuterGlowBitmapEffect
        If down = True Then
            a.GlowSize = 5
            a.GlowColor = Windows.Media.Colors.Black
            da.Duration = TimeSpan.FromMilliseconds(300)
            da2.Duration = da.Duration
        Else
            a.GlowSize = 7
            a.GlowColor = Windows.Media.Colors.WhiteSmoke
            da.Duration = TimeSpan.FromMilliseconds(150)
            da2.Duration = da.Duration
        End If




        sender.BitmapEffect = a


        da.From = a.GlowSize
        da.To = 0



        da2.From = a.GlowColor
        da2.To = Windows.Media.Colors.Transparent



        sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowColorProperty, da2)
        sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowSizeProperty, da)
        up = False
        down = False
    End Sub

    Private Sub MyIcon_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        'On Error GoTo yo
        If down = True Then
            Dim a As New Windows.Media.Effects.OuterGlowBitmapEffect
            a.GlowSize = 5
            a.GlowColor = Windows.Media.Colors.Black
            sender.BitmapEffect = a
            Dim da As New DoubleAnimation
            da.From = a.GlowSize
            da.To = 0
            da.Duration = TimeSpan.FromMilliseconds(300)

            Dim da2 As New ColorAnimation
            da2.From = a.GlowColor
            da2.To = Windows.Media.Colors.Transparent
            da2.Duration = TimeSpan.FromMilliseconds(300)

            sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowColorProperty, da2)
            sender.BitmapEffect.BeginAnimation(Windows.Media.Effects.OuterGlowBitmapEffect.GlowSizeProperty, da)
            mup = True

            Try
                Process.Start(sender.tag)
            Catch ex As Exception
                Shell(sender.tag)
            End Try
        End If
        down = False
        up = False
        Exit Sub
yo:
        MsgBox("Could not load file. The file is either invalid or corrupt.")
    End Sub

    Private Sub mnuExit_Click()
        End
    End Sub

    Private Sub AutoHide_Tick()
        If Me.Opacity = 1 Then
            AutoHideShow()
        End If
    End Sub

    Private Sub wpfDock_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles Me.MouseLeave
        If Me.Opacity = 1 Then
            AutoHide.Enabled = True
        End If
    End Sub

    Private Sub wpfDock_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles Me.MouseMove
        If Me.Opacity = 1 Then
            AutoHide.Enabled = False
        End If
    End Sub

    Private Sub AutoHideShow()
        If AutoHideOption = True Then
            If Me.Opacity = 1 Then
                Dim da As New DoubleAnimation
                da.From = 1
                da.To = 0
                da.Duration = TimeSpan.FromMilliseconds(500)
                Me.BeginAnimation(wpfDock.OpacityProperty, da)
                AutoHide.Enabled = False
            End If
            If Me.Opacity = 0 Then
                Dim da As New DoubleAnimation
                da.From = 0
                da.To = Me.Opacity + 1
                da.Duration = TimeSpan.FromMilliseconds(250)
                Me.BeginAnimation(wpfDock.OpacityProperty, da)
            End If
            AutoHide.Enabled = False
        End If
    End Sub

    Private Sub Auto_Tick()
        Dim a As Boolean
        If Dock.PreviewEx = True Then
            Preview()
        End If
        If Me.Opacity = 0 Then
            Dim X As Integer
            Dim Y As Integer
            X = Windows.Forms.Cursor.Position.X - Me.Left
            Y = Windows.Forms.Cursor.Position.Y
            If X <= Me.Width And X >= 0 And Y = 0 Then
                AutoHideShow()
            End If
        End If
        If Me.Opacity = 1 Then
            Dim X As Integer
            Dim Y As Integer
            X = Windows.Forms.Cursor.Position.X - Me.Left
            Y = Windows.Forms.Cursor.Position.Y - Me.Top
            If Y > Me.Height - 50 Then
                a = True
            End If
            If X > Me.Width Or X < 0 Then
                a = True
            End If
            If a = True Then
                AutoHide.Enabled = True
            End If
        End If
    End Sub

    Private Sub mnuDockSettings_Click()
        On Error Resume Next
        AutoHideOption = False
        SettingViewed = True
        Dim box As Window = New wpfSettings
        If box.ShowDialog = Windows.Forms.DialogResult.OK Then
            SettingViewed = False
            CheckAutoHide()
        Else
            SettingViewed = False
            Setting()
            CheckAutoHide()
        End If
    End Sub

    Private Sub mnuIconSettings_CLick()
        On Error Resume Next
        AutoHideOption = False
        SettingViewed = True
        Dim box As Window = New wpfIconSettings
        If box.ShowDialog = Windows.Forms.DialogResult.OK Then
            SettingViewed = False
            HideClose()
            CloseTimer.Enabled = True
            CheckAutoHide()
        Else
            SettingViewed = False
            HideClose()
            CloseTimer.Enabled = True
            CheckAutoHide()
        End If
    End Sub

    Private Sub ContextMenu_Opened()
        AutoHideOption = False
    End Sub

    Public Sub Setting()
        Dim margin As String

        imgCenter.Opacity = Int(IO.File.ReadAllText(AppPath & "\Settings\op.h"))
        imgRight.Opacity = Int(IO.File.ReadAllText(AppPath & "\Settings\op.h"))
        imgLeft.Opacity = Int(IO.File.ReadAllText(AppPath & "\Settings\op.h"))

        Dim effect As New Windows.Media.Effects.OuterGlowBitmapEffect
        effect.GlowSize = Int(IO.File.ReadAllText(AppPath & "\Settings\gsize.h"))
        effect.GlowColor = ColorConverter.ConvertFromString(IO.File.ReadAllText(AppPath & "\Settings\gcol.h"))

        Dim effect2 As New Windows.Media.Effects.BlurBitmapEffect
        effect2.Radius = Int(IO.File.ReadAllText(AppPath & "\Settings\blur.h"))


        grdMain.BitmapEffect = effect
        grdBG.BitmapEffect = effect2



        Dim isc As New ImageSourceConverter
        imgLeft.Source = isc.ConvertFromString(AppPath & "\Skins\" & IO.File.ReadAllText(AppPath & "\Settings\skin.h") & "\left.png")
        imgRight.Source = isc.ConvertFromString(AppPath & "\Skins\" & IO.File.ReadAllText(AppPath & "\Settings\skin.h") & "\right.png")
        imgCenter.Source = isc.ConvertFromString(AppPath & "\Skins\" & IO.File.ReadAllText(AppPath & "\Settings\skin.h") & "\bg.png")
        imgRight.HorizontalAlignment = Windows.HorizontalAlignment.Right


        'Load Theme Settings
        margin = IO.File.ReadAllText(AppPath & "\Skins\" & IO.File.ReadAllText(AppPath & "\Settings\skin.h") & "\settings.ini")


        'Theme Settings
        imgLeft.Width = Int(Mid(margin, 1, 3))
        imgRight.Width = Int(Mid(margin, margin.Length - 2, 3))


        Me.Width = imgLeft.Width + imgRight.Width + 12 + Icons.Width

        Dim a As New Thickness
        a.Left = imgLeft.Width + 6
        a.Top = 0
        a.Bottom = 9
        a.Right = imgRight.Width + 6
        Icons.Margin = a
        Me.Left = (My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2)
        imgCenter.Width = Me.Width - (imgLeft.Width + imgRight.Width)
    End Sub

    Private Sub Preview()
        If PreviewEx = True Then
            On Error Resume Next
            Dim margin As String

            imgCenter.Opacity = OpacityEx
            imgRight.Opacity = OpacityEx
            imgLeft.Opacity = OpacityEx

            Dim effect2 As New Windows.Media.Effects.BlurBitmapEffect
            effect2.Radius = BlurEx

            Dim effect As New Windows.Media.Effects.OuterGlowBitmapEffect
            effect.GlowColor = GlowColorEx.Color
            effect.GlowSize = GlowSizeEx

            grdMain.BitmapEffect = effect
            grdBG.BitmapEffect = effect2


            Dim isc As New ImageSourceConverter
            imgLeft.Source = isc.ConvertFromString(AppPath & "\Skins\" & SkinEx & "\left.png")
            imgRight.Source = isc.ConvertFromString(AppPath & "\Skins\" & SkinEx & "\right.png")
            imgCenter.Source = isc.ConvertFromString(AppPath & "\Skins\" & SkinEx & "\bg.png")
            imgRight.HorizontalAlignment = Windows.HorizontalAlignment.Right


            'Load Theme Settings
            margin = IO.File.ReadAllText(AppPath & "\Skins\" & SkinEx & "\settings.ini")


            'Theme Settings
            imgLeft.Width = Int(Mid(margin, 1, 3))
            imgRight.Width = Int(Mid(margin, margin.Length - 2, 3))

            Me.Width = imgLeft.Width + imgRight.Width + 12 + Icons.Width

            Dim a As New Thickness
            a.Left = imgLeft.Width + 6
            a.Top = 0
            a.Bottom = 9
            a.Right = imgRight.Width + 6
            Icons.Margin = a
            PreviewEx = False

            Me.Left = (My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2)

            imgCenter.Width = Me.Width - (imgLeft.Width + imgRight.Width)
        End If
    End Sub

    Private Sub ContextMenu_LostFocus()
        If SettingViewed = False Then
            CheckAutoHide()
        End If
    End Sub

    Dim lsticon As New ListBox
    Dim lstpath As New ListBox

    Private Sub LoadIcons()
        On Error Resume Next
        Dim setting As String
        Dim lsticon As New ListBox
        Dim lstpath As New ListBox
        Dim ico As String = ""
        Dim path As String = ""
        Dim i As Integer
        Dim r As Integer = 0
        setting = IO.File.ReadAllText(AppPath & "\Settings\icons.dat")
        For Each con As Image In Icons.Children
            Dim ss As New ImageSourceConverter
            con.Source = ss.ConvertFrom("")
            Icons.Children.Remove(con)
        Next
        For i = 1 To setting.Length
            Select Case Mid(setting, i, 1)
                Case "<"
                    r = 1
                    i = i + 1
                Case "|"
                    r = 2
                    i = i + 1
                Case ">"
                    lsticon.Items.Add(ico)
                    ico = ""
                    lstpath.Items.Add(path)
                    path = ""
                    r = 0
                Case "?"
                    r = 3
            End Select
            If r = 1 Then
                ico = ico & Mid(setting, i, 1)
            ElseIf r = 2 Then
                path = path & Mid(setting, i, 1)
            End If
        Next
        Icons.Width = 0
        If lsticon.Items.Count <> 0 Then
            For i = 0 To lsticon.Items.Count - 1
                Select Case lstpath.Items(i)
                    Case "My Computer"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "My Documents"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "My Network Places"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = "::{208D2C60-3AEA-1069-A2D7-08002B30309D}"
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "My Pictures"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = My.Computer.FileSystem.SpecialDirectories.MyPictures
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "My Music"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = My.Computer.FileSystem.SpecialDirectories.MyMusic
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "Control Panel"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}"
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "Shutdown Command"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = "Shutdown -s -t 0"
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "Restart Command"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = "Shutdown -r -t 0"
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case "Log Off Command"
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = "Shutdown -l -t 0"
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                    Case Else
                        Dim ims As New Image
                        Dim a As New Thickness
                        Dim b As New ImageSourceConverter
                        a.Left = (i * 6) + (i * 48)
                        a.Top = 0
                        ims.Margin = a
                        ims.Height = 48
                        ims.Width = 48
                        ims.Tag = lstpath.Items.Item(i)
                        ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        Icons.Children.Add(ims)
                End Select
            Next
            Icons.Width = (lsticon.Items.Count * 48) + ((lsticon.Items.Count * 6) - 6)
        End If
    End Sub

    Private Sub HideClose()
        Dim da As New DoubleAnimation
        da.From = 1
        da.To = 0
        da.Duration = TimeSpan.FromMilliseconds(500)
        Me.BeginAnimation(wpfDock.OpacityProperty, da)
        AutoHide.Enabled = False
    End Sub

    Private Sub CloseTimer_Tick()
        If Me.Opacity = 0 Then
            Dim renew As Window = New wpfDock
            Me.Close()
            renew.Show()
            CloseTimer.Enabled = False
        End If
    End Sub
End Class