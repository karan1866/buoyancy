Imports System.Windows.Media.Animation

Partial Public Class wpfDock
    Dim AutoHide As New Windows.Forms.Timer
    Dim down As Boolean
    Dim SettingViewed As Boolean
    Dim up As Boolean
    Dim mup As Boolean
    Dim CloseTimer As New Windows.Forms.Timer
    Dim ManualShut As Boolean
    Dim TopAssign_Int As Integer
    Dim TopAssign_Dock As New Windows.Forms.DockStyle
    Dim IconContext As New ContextMenu
    Dim SelectedIconID As String

    Private Sub wpfDock_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If ManualShut = True Then
            e.Cancel = True
            Dim da As New DoubleAnimation
            AddHandler da.Completed, AddressOf AnimationFinished
            da.From = 1
            da.To = 0
            da.Duration = TimeSpan.FromMilliseconds(150)
            Me.BeginAnimation(wpfDock.OpacityProperty, da)
        End If
    End Sub

    Private Sub AnimationFinished()
        End
    End Sub

    Private Sub Icon_ContextMenuOpening(ByVal sender As Object, ByVal e As System.Windows.Controls.ContextMenuEventArgs)
        SelectedIconID = sender.Name
    End Sub

    Private Sub wpfDock_DragEnter(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effects = DragDropEffects.Copy
        Else
            e.Effects = DragDropEffects.None
        End If
    End Sub

    Private Sub DragDrop(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles imgLeft.Drop, imgCenter.Drop, imgRight.Drop
        If Me.Opacity = 1 Then
            Dim ProcessedIndex As Integer


            Select Case sender.Name
                Case "imgCenter"
                    ProcessedIndex = -1
                Case "imgLeft"
                    ProcessedIndex = 0
                Case "imgRight"
                    ProcessedIndex = -1
                Case Else
                    ProcessedIndex = Convert.ToInt16(Mid(sender.Name, 4, sender.Name.ToString.Length - 3))
            End Select


            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim filePaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
                For Each fileLoc As String In filePaths
                    If fileLoc.EndsWith(".lnk") Then
                        Dim Obj As Object
                        Obj = CreateObject("WScript.Shell")
                        Dim Shortcut As Object
                        Shortcut = Obj.CreateShortcut(fileLoc)
                        If IO.Directory.Exists(fileLoc) = False Then
                            ProcessDrop(Shortcut.TargetPath, ProcessedIndex)
                        Else
                            ProcessDrop(fileLoc, ProcessedIndex)
                        End If
                    Else
                        ProcessDrop(fileLoc, ProcessedIndex)
                    End If
                Next fileLoc
            Else
                MsgBox("File Type Not Supported.", MsgBoxStyle.Exclamation, "")
            End If
        End If
    End Sub

    Private Sub ProcessDrop(ByVal Path As String, ByVal Index As Integer)
        Dim i As Integer
        Dim str1 As String = Path
        Dim str2 As String = Nothing
        For i = str1.Length To 1 Step -1
            If Mid(str1, i, 1) = "\" Then
                Exit For
            Else
                str2 = str2 & Mid(str1, i, 1)
            End If
        Next

        str1 = String.Empty

        For i = str2.Length To 1 Step -1
            str1 = str1 & Mid(str2, i, 1)
        Next

        Dim Data As String = IO.File.ReadAllText(AppPath & "\Settings\icons.dat")

        If Index = -1 Then
            Data = Data & "<none|" & Path & "?" & str1 & ">"
        ElseIf Index = 0 Then
            Data = "<none|" & Path & "?" & str1 & ">" & Data
        Else
            Dim Recorder_1 As String = Nothing
            Dim Recorded As Integer
            Dim Recorder_2 As String = Nothing
            Dim Break As Boolean

            For i = 1 To Data.Length
                Select Case Mid(Data, i, 1)
                    Case "<"
                        If Recorded = Index Then
                            Break = True
                        End If
                    Case ">"
                        Recorded = Recorded + 1
                End Select
                If Break = False Then
                    Recorder_1 = Recorder_1 & Mid(Data, i, 1)
                Else
                    Recorder_2 = Recorder_2 & Mid(Data, i, 1)
                End If
            Next
            Data = Recorder_1 & "<none|" & Path & "?" & str1 & ">" & Recorder_2
        End If

        IO.File.WriteAllText(AppPath & "\Settings\icons.dat", Data)
        LoadIcons()
        Setting()
    End Sub

    Private Sub LoadIconContext()
        Dim mnuExit As New MenuItem
        Dim mnuDockSettings As New MenuItem
        Dim mnuIconSettings As New MenuItem
        Dim mnuDelete As New MenuItem
        Dim mnuChangeIcon As New MenuItem
        Dim mnuRefresh As New MenuItem
        Dim sep As New Separator
        Dim sep2 As New Separator


        'Add Item
        mnuExit.Header = "Exit"
        mnuDockSettings.Header = "Dock Settings"
        mnuIconSettings.Header = "Icon Settings"
        mnuDelete.Header = "Delete"
        mnuChangeIcon.Header = "Change Icon"
        mnuRefresh.Header = "Refresh"
        AddHandler mnuExit.Click, AddressOf mnuExit_Click
        AddHandler mnuDockSettings.Click, AddressOf mnuDockSettings_Click
        AddHandler mnuIconSettings.Click, AddressOf mnuIconSettings_Click
        AddHandler mnuRefresh.Click, AddressOf mnuRefresh_Click
        AddHandler mnuDelete.Click, AddressOf mnuDelete_Click
        AddHandler mnuChangeIcon.Click, AddressOf mnuChangeIcon_Click
        AddHandler IconContext.Opened, AddressOf ContextMenu_Opened
        AddHandler IconContext.Closed, AddressOf ContextMenu_LostFocus


        'Dock Context Menu Strip
        IconContext.Items.Add(mnuChangeIcon)
        IconContext.Items.Add(mnuDelete)
        IconContext.Items.Add(sep)
        IconContext.Items.Add(mnuDockSettings)
        IconContext.Items.Add(mnuIconSettings)
        IconContext.Items.Add(mnuRefresh)
        IconContext.Items.Add(sep2)
        IconContext.Items.Add(mnuExit)
    End Sub

    Private Sub mnuDelete_Click()
        Dim setting As String = IO.File.ReadAllText(AppPath & "\Settings\icons.dat")
        Dim i As Integer
        Dim selected As Integer = Convert.ToInt16(Mid(SelectedIconID, 4, SelectedIconID.Length - 3))
        Dim count As Integer = 0
        Dim Record As Boolean
        Dim Recorder As String = Nothing
        For i = 1 To setting.Length
            Select Case Mid(setting, i, 1)
                Case "<"
                    If selected = count Then
                        Record = False
                    Else
                        Record = True
                    End If
                Case ">"
                    count = count + 1
            End Select
            If Record = True Then
                Recorder = Recorder & Mid(setting, i, 1)
            End If
        Next
        IO.File.WriteAllText(AppPath & "\Settings\icons.dat", Recorder)

        LoadIcons()
        Me.Setting()
    End Sub

    Private Sub Window1_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        AppPath = Mid(System.AppDomain.CurrentDomain.BaseDirectory, 1, System.AppDomain.CurrentDomain.BaseDirectory.Length - 1)

        Dim ic As New ImageSourceConverter


        LoadIconContext()

        'Hide Form for Loading and Startup Effect
        Me.Opacity = 0


        'Allow DragDrop


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
        Dim mnuRefresh As New MenuItem
        Dim sep As New Separator



        'Me.Width = My.Computer.Screen.Bounds.Width
        'Me.Height = My.Computer.Screen.Bounds.Height



        'Add Item
        mnuExit.Header = "Exit"
        mnuDockSettings.Header = "Dock Settings"
        mnuIconSettings.Header = "Icon Settings"
        mnuRefresh.Header = "Refresh"
        AddHandler mnuExit.Click, AddressOf mnuExit_Click
        AddHandler mnuDockSettings.Click, AddressOf mnuDockSettings_Click
        AddHandler mnuIconSettings.Click, AddressOf mnuIconSettings_Click
        AddHandler mnuRefresh.Click, AddressOf mnuRefresh_Click


        'Dock Context Menu Strip
        a.Items.Add(mnuDockSettings)
        a.Items.Add(mnuIconSettings)
        a.Items.Add(mnuRefresh)
        a.Items.Add(sep)
        a.Items.Add(mnuExit)
        imgLeft.ContextMenu = a
        imgRight.ContextMenu = a
        imgCenter.ContextMenu = a
        AddHandler a.Opened, AddressOf ContextMenu_Opened
        AddHandler a.Closed, AddressOf ContextMenu_LostFocus


        'Set Perfect Height
        Me.Height = 114



        'Centering
        TopAssign_Int = Me.Margin.Top




        'Startup Effect
        AutoHideShow()
        CheckAutoHide()
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

            On Error GoTo yo
            Process.Start(sender.tag)
        End If
        down = False
        up = False
        Exit Sub
yo:
        TryShell(sender.tag)
    End Sub

    Private Sub TryShell(ByVal path As String)
        On Error GoTo yo
        Shell(path)

        Exit Sub
yo:
        MsgBox("Could not load file. The file is either invalid or corrupt.", MsgBoxStyle.Exclamation, "Erro Opening File")
    End Sub

    Private Sub mnuExit_Click()
        ManualShut = True
        Me.Close()
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
                Me.BeginAnimation(Grid.OpacityProperty, da)
                AutoHide.Enabled = False
            End If
            If Me.Opacity = 0 Then
                Dim da As New DoubleAnimation
                da.From = 0
                da.To = Me.Opacity + 1
                da.Duration = TimeSpan.FromMilliseconds(250)
                Me.BeginAnimation(Grid.OpacityProperty, da)
            End If
            AutoHide.Enabled = False
        End If
    End Sub

    Private Sub Auto_Tick()
        Dim a As Boolean
        If Dock.PreviewEx = True Then
            Preview()
        End If

        If Me.Margin.Top <> TopAssign_Int Then
            Me.Margin = New Thickness(Me.Margin.Left, TopAssign_Int, Me.Margin.Right, Me.Margin.Bottom)
        End If

        If TopAssign_Dock = Forms.DockStyle.Top Then
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
                Y = Windows.Forms.Cursor.Position.Y - Me.Top - 25
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
        ElseIf TopAssign_Dock = Forms.DockStyle.Bottom Then
            If Me.Opacity = 0 Then
                Dim X As Integer
                Dim Y As Integer
                X = Windows.Forms.Cursor.Position.X - Me.Left
                Y = Windows.Forms.Cursor.Position.Y + 2
                If X <= Me.Width And X >= 0 And Y >= My.Computer.Screen.Bounds.Height Then
                    AutoHideShow()
                End If
            End If
            If Me.Opacity = 1 Then
                Dim X As Integer
                Dim Y As Integer
                X = Windows.Forms.Cursor.Position.X - Me.Left
                Y = Windows.Forms.Cursor.Position.Y - Me.Top - 25
                If Y < 25 Then
                    a = True
                End If
                If X > Me.Width Or X < 0 Then
                    a = True
                End If
                If a = True Then
                    AutoHide.Enabled = True
                End If
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

    Private Sub mnuIconSettings_Click()
        On Error Resume Next
        AutoHideOption = False
        SettingViewed = True
        Dim box As Window = New wpfIconSettings
        If box.ShowDialog = Windows.Forms.DialogResult.OK Then
            SettingViewed = False
            LoadIcons()
            Setting()
            CheckAutoHide()
        Else
            SettingViewed = False
            LoadIcons()
            Setting()
            CheckAutoHide()
        End If
    End Sub

    Private Sub ContextMenu_Opened()
        AutoHideOption = False
    End Sub

    Public Sub Setting()
        On Error Resume Next
        Dim margin As String

        imgCenter.Opacity = IO.File.ReadAllText(AppPath & "\Settings\op.h")
        imgRight.Opacity = IO.File.ReadAllText(AppPath & "\Settings\op.h")
        imgLeft.Opacity = IO.File.ReadAllText(AppPath & "\Settings\op.h")

        Dim effect2 As New Windows.Media.Effects.BlurBitmapEffect
        effect2.Radius = IO.File.ReadAllText(AppPath & "\Settings\blur.h")

        Dim effect As New Windows.Media.Effects.OuterGlowBitmapEffect
        effect.GlowColor = ColorConverter.ConvertFromString(IO.File.ReadAllText(AppPath & "\Settings\gcol.h"))
        effect.GlowSize = IO.File.ReadAllText(AppPath & "\Settings\gsize.h")

        grdMain.BitmapEffect = effect
        grdBG.BitmapEffect = effect2


        Icons.Margin = New Thickness(0, 0, 0, 9)


        Icons.Opacity = IO.File.ReadAllText(AppPath & "\Settings\iop.h")

        Dim ieffect2 As New Windows.Media.Effects.BlurBitmapEffect
        ieffect2.Radius = IO.File.ReadAllText(AppPath & "\Settings\iblur.h")

        Dim ieffect As New Windows.Media.Effects.OuterGlowBitmapEffect
        ieffect.GlowColor = ColorConverter.ConvertFromString(IO.File.ReadAllText(AppPath & "\Settings\igcol.h"))
        ieffect.GlowSize = IO.File.ReadAllText(AppPath & "\Settings\igsize.h")


        Icons.BitmapEffect = ieffect
        Icons2.BitmapEffect = ieffect2



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




        PreviewEx = False


        Dim newWidth As Double = imgLeft.Width + imgRight.Width + 12 + Icons.Width
        Dim newWidthChange As Double

        If Me.Width > newWidth Then
            newWidthChange = Me.Width - newWidth
        Else
            newWidthChange = newWidth - Me.Width
        End If



        Me.Width = newWidth

        Me.Left = (My.Computer.Screen.Bounds.Width / 2) - (Me.Width / 2)
        imgCenter.Width = newWidth - (imgLeft.Width + imgRight.Width)



        'Dim Ani As New ThicknessAnimation
        'Dim AnimatedMargin As New Thickness
        'AnimatedMargin.Top = Me.Margin.Top
        'AnimatedMargin.Left = Me.Margin.Left + ((Me.Width - newWidth) / 2)
        'AnimatedMargin.Bottom = Me.Margin.Bottom
        'AnimatedMargin.Right = Me.Margin.Right + ((Me.Width - newWidth) / 2)
        'Ani.From = Me.Margin
        'Ani.To = AnimatedMargin
        'Ani.Duration = TimeSpan.FromMilliseconds(250)
        'Ani.DecelerationRatio = 1
        'Me.BeginAnimation(Grid.MarginProperty, Ani)


        'Dim Ani2 As New DoubleAnimation
        'Ani2.From = Me.Width - (imgLeft.Width + imgRight.Width)
        'Ani2.To = newWidth - (imgLeft.Width + imgRight.Width)
        'Ani2.Duration = TimeSpan.FromMilliseconds(250)
        'Ani2.DecelerationRatio = 1
        'imgCenter.BeginAnimation(Image.WidthProperty, Ani2)




        If IO.File.ReadAllText(AppPath & "\Settings\align.h") = "Top" Then
            TopAssign_Int = -25
            TopAssign_Dock = Forms.DockStyle.Top


            Me.Top = -25


            Dim flip As New ScaleTransform
            flip.ScaleY = 1
            flip.CenterY = 32
            imgRight.RenderTransform = flip

            Dim flip1 As New ScaleTransform
            flip1.ScaleY = 1
            flip1.CenterY = 32
            imgCenter.RenderTransform = flip1

            Dim flip2 As New ScaleTransform
            flip2.ScaleY = 1
            flip2.CenterY = 32
            imgLeft.RenderTransform = flip2
        ElseIf IO.File.ReadAllText(AppPath & "\Settings\align.h") = "Bottom" Then
            TopAssign_Int = My.Computer.Screen.WorkingArea.Height - 64 - 25
            TopAssign_Dock = Forms.DockStyle.Bottom


            Me.Top = My.Computer.Screen.WorkingArea.Height - 64 - 25


            Dim flip As New ScaleTransform
            flip.ScaleY = -1
            flip.CenterY = 32
            imgRight.RenderTransform = flip

            Dim flip1 As New ScaleTransform
            flip1.ScaleY = -1
            flip1.CenterY = 32
            imgCenter.RenderTransform = flip1

            Dim flip2 As New ScaleTransform
            flip2.ScaleY = -1
            flip2.CenterY = 32
            imgLeft.RenderTransform = flip2
        ElseIf IO.File.ReadAllText(AppPath & "\Settings\align.h") = "Super Bottom" Then
            TopAssign_Int = My.Computer.Screen.Bounds.Height - 64 - 25
            TopAssign_Dock = Forms.DockStyle.Bottom


            Me.Top = TopAssign_Int


            Dim flip As New ScaleTransform
            flip.ScaleY = -1
            flip.CenterY = 32
            imgRight.RenderTransform = flip

            Dim flip1 As New ScaleTransform
            flip1.ScaleY = -1
            flip1.CenterY = 32
            imgCenter.RenderTransform = flip1

            Dim flip2 As New ScaleTransform
            flip2.ScaleY = -1
            flip2.CenterY = 32
            imgLeft.RenderTransform = flip2
        End If
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


            Dim ieffect2 As New Windows.Media.Effects.BlurBitmapEffect
            ieffect2.Radius = iBlurEx

            Dim ieffect As New Windows.Media.Effects.OuterGlowBitmapEffect
            ieffect.GlowColor = iGlowColorEx.Color
            ieffect.GlowSize = iGlowSizeEx

            Icons.BitmapEffect = ieffect
            Icons2.BitmapEffect = ieffect2


            Icons.Opacity = iOpacityEx


            Icons.Margin = New Thickness(0, 0, 0, 9)


            If AlignEx = "Top" Then
                TopAssign_Int = -25

                Me.Top = -25


                Dim flip As New ScaleTransform
                flip.ScaleY = 1
                flip.CenterY = 32
                imgRight.RenderTransform = flip

                Dim flip1 As New ScaleTransform
                flip1.ScaleY = 1
                flip1.CenterY = 32
                imgCenter.RenderTransform = flip1

                Dim flip2 As New ScaleTransform
                flip2.ScaleY = 1
                flip2.CenterY = 32
                imgLeft.RenderTransform = flip2
            ElseIf AlignEx = "Bottom" Then
                TopAssign_Int = My.Computer.Screen.WorkingArea.Height - 64 - 25

                Me.Top = My.Computer.Screen.WorkingArea.Height - 64 - 25


                Dim flip As New ScaleTransform
                flip.ScaleY = -1
                flip.CenterY = 32
                imgRight.RenderTransform = flip

                Dim flip1 As New ScaleTransform
                flip1.ScaleY = -1
                flip1.CenterY = 32
                imgCenter.RenderTransform = flip1

                Dim flip2 As New ScaleTransform
                flip2.ScaleY = -1
                flip2.CenterY = 32
                imgLeft.RenderTransform = flip2
            ElseIf AlignEx = "Super Bottom" Then
                TopAssign_Int = My.Computer.Screen.Bounds.Height - 64 - 25
                TopAssign_Dock = Forms.DockStyle.Bottom


                Me.Top = TopAssign_Int


                Dim flip As New ScaleTransform
                flip.ScaleY = -1
                flip.CenterY = 32
                imgRight.RenderTransform = flip

                Dim flip1 As New ScaleTransform
                flip1.ScaleY = -1
                flip1.CenterY = 32
                imgCenter.RenderTransform = flip1

                Dim flip2 As New ScaleTransform
                flip2.ScaleY = -1
                flip2.CenterY = 32
                imgLeft.RenderTransform = flip2
            End If


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


            PreviewEx = False

            Dim newWidth As Double = imgLeft.Width + imgRight.Width + 12 + Icons.Width
            Dim newWidthChange As Double

            If Me.Width > newWidth Then
                newWidthChange = Me.Width - newWidth
            Else
                newWidthChange = newWidth - Me.Width
            End If


            Me.Width = newWidth

            Me.Left = (My.Computer.Screen.Bounds.Width / 2) - (Me.Width / 2)
            imgCenter.Width = newWidth - (imgLeft.Width + imgRight.Width)

            imgCenter.Width = Me.ActualWidth - (imgLeft.Width + imgRight.Width)
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
            con.Visibility = Windows.Visibility.Hidden
        Next
        Icons.Children.Clear()
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        ims.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
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
                        If IO.File.Exists(AppPath & "\Icons\" & lsticon.Items.Item(i)) = True Then
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\" & lsticon.Items.Item(i))
                        Else
                            ims.Source = b.ConvertFromString(AppPath & "\Icons\data\Help.png")
                        End If
                        ims.HorizontalAlignment = Windows.HorizontalAlignment.Left
                        ims.Name = "ims" & i
                        ims.AllowDrop = True
                        AddHandler ims.MouseDown, AddressOf MyIcon_MouseDown
                        AddHandler ims.MouseEnter, AddressOf MyIcon_MouseEnter
                        AddHandler ims.MouseUp, AddressOf MyIcon_MouseUp
                        AddHandler ims.MouseLeave, AddressOf MyIcon_MouseLeave
                        AddHandler ims.ContextMenuOpening, AddressOf Icon_ContextMenuOpening
                        AddHandler ims.Drop, AddressOf DragDrop
                        Icons.ContextMenu = IconContext
                        Icons.Children.Add(ims)
                End Select
            Next
            Icons.Width = (lsticon.Items.Count * 48) + ((lsticon.Items.Count * 6) - 6)
        End If
    End Sub

    'Private Sub HideClose()
    '    Dim da As New DoubleAnimation
    '    da.From = 1
    '    da.To = 0
    '    da.Duration = TimeSpan.FromMilliseconds(500)
    '    Me.BeginAnimation(Grid.OpacityProperty, da)
    '    AutoHide.Enabled = False
    'End Sub

    Private Sub CloseTimer_Tick()
        If Me.Opacity = 0 Then

            My.Application.Shutdown()
            Shell(AppPath & "\FizzDock.exe")
        End If
    End Sub

    Private Sub mnuChangeIcon_Click()
        AutoHideOption = False
        Dim SenderIndex As Integer
        SenderIndex = Convert.ToInt16(Mid(SelectedIconID, 4, SelectedIconID.Length - 3))
        Dim ofd As New Windows.Forms.OpenFileDialog
        ofd.Filter = "PNG Format (*.png)|*.png"
        If ofd.ShowDialog = Forms.DialogResult.OK Then
            Dim i As Integer
            Dim File1 As String
            File1 = ofd.FileName
            Dim File2 As String = Nothing
            For i = File1.Length To 1 Step -1
                If Mid(File1, i, 1) = "\" Then
                    Exit For
                Else
                    File2 = File2 & Mid(File1, i, 1)
                End If
            Next

            File1 = String.Empty

            For i = File2.Length To 1 Step -1
                File1 = File1 & Mid(File2, i, 1)
            Next


retry:
            If IO.File.Exists(AppPath & "\Icons\" & File1) = False Then
                IO.File.Copy(ofd.FileName, AppPath & "\Icons\" & File1)
            Else
                Randomize()
                File1 = (Rnd() * 10000) + 1 & ".png"
                GoTo retry
            End If

            Dim Data As String

            Data = IO.File.ReadAllText(AppPath & "\Settings\icons.dat")


            Dim Recorder As String = Nothing
            Dim Record As Boolean
            Dim Recorded As Integer
            For i = 1 To Data.Length
                Select Case Mid(Data, i, 1)
                    Case "<"
                        If Recorded = SenderIndex Then
                            Record = False
                        Else
                            Record = True
                        End If
                    Case "|"
                        If Recorded = SenderIndex Then
                            Record = True
                            Recorder = Recorder & "<" & File1
                        End If
                    Case "?"

                    Case ">"
                        Record = True
                        Recorded = Recorded + 1
                End Select
                If Record = True Then
                    Recorder = Recorder & Mid(Data, i, 1)
                End If
            Next
            IO.File.WriteAllText(AppPath & "\Settings\icons.dat", Recorder)


            LoadIcons()
            Setting()
            CheckAutoHide()
        End If
    End Sub

    Private Sub mnuRefresh_Click()
        LoadIcons()
        Setting()
    End Sub
End Class