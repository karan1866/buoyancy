Public Class Capture
    Dim pic As Image
    Declare Function GetAsyncKeyState Lib "user32" Alias "GetAsyncKeyState" (ByVal vKey As Long) As Short
    Public delay As Integer
    Public img As Image
    Public base As String = "Screenshot"
    Public format As String = ".png"
    Public path As String = My.Computer.FileSystem.SpecialDirectories.Desktop
    Dim timed As String

    Dim _snapsLeft As Integer

    Public Property snapsLeft() As Integer
        Get
            Return _snapsLeft
        End Get
        Set(ByVal value As Integer)
            _snapsLeft = value
            If _snapsLeft = 0 Then
                InfoSeparator.Visible = False
                InfoToolStripMenuItem.Visible = False
                InfoToolStripMenuItem.Text = ""
            Else
                InfoSeparator.Visible = True
                InfoToolStripMenuItem.Visible = True

                If InfoToolStripMenuItem.Text <> "Stop Timed Snap" Then
                    InfoToolStripMenuItem.Text = _snapsLeft & " left @ " & delay / 1000 & "s/snap"
                End If
                InfoToolStripMenuItem.Tag = _snapsLeft & " left @ " & delay / 1000 & "s/snap"
            End If
        End Set
    End Property

    Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean

    Public Sub SetProcessWorkingSetSize()
        Try
            'Dim Mem As Process
            'Mem = Process.GetCurrentProcess()
            SetProcessWorkingSetSize(Me.Handle, -1, -1)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tmr1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr1.Tick
        'On Error GoTo err
        'If Me.WindowState <> FormWindowState.Minimized Then
        '    Me.WindowState = FormWindowState.Minimized
        'End If
        If Flash.Opacity > 0 Then
            tmr1.Enabled = False
        End If
        GC.Collect()
        SetProcessWorkingSetSize()
        If Me.Visible = True Then
            Me.Visible = False
        End If
        If Flash.Visible = True Then
            Exit Sub
        End If
        If GetAsyncKeyState(44) <> 0 Then
            Dim a As Integer = GetAsyncKeyState(16)
            Dim s As Integer = GetAsyncKeyState(17)
            If GetAsyncKeyState(16) <> 0 Or GetAsyncKeyState(160) <> 0 Then
                'SetProcessWorkingSetSize()
                CropSnapToolStripMenuItem_Click(CropSnapToolStripMenuItem, Nothing)
                Exit Sub
            End If
            If GetAsyncKeyState(17) <> 0 Or GetAsyncKeyState(170) <> 0 Then
                'SetProcessWorkingSetSize()

                Me.CaptureToolStripMenuItem_Click(CaptureToolStripMenuItem, Nothing)
                Exit Sub
            End If
            If FlashToolStripMenuItem.Checked = True Then

                Flash.Opacity = 1
                Flash.Show()
                GC.Collect()
            End If
            If AutoSaveToolStripMenuItem.Checked = True Then
                pic = Clipboard.GetImage 'GetImage()
                Save(path & "\" & base, pic)

            Else
                frmCapture.pcbMain.BackgroundImage = Clipboard.GetImage 'GetImage()
                frmCapture.Show()
            End If
            'While (GetAsyncKeyState(44) <> 0)

            'End While
        End If
        '        Exit Sub
        'err:
        '        MsgBox("1")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        NotifyIcon1.Visible = False
        NotifyIcon1.Icon.Dispose()
        End
    End Sub

    'Declare Function keybd_event Lib "user32" Alias "keybd_event" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Long, ByVal dwExtraInfo As Long) As Long

    Private Sub Capture_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'keybd_event(44, 0, 2, 0)

        Me.Location = New Drawing.Point(-1000, -1000)
        On Error Resume Next
        Dim fla As String = My.Computer.Registry.CurrentUser.OpenSubKey("Software\K15\iCapture\").GetValue("flash")
        If fla <> "" Then
            FlashToolStripMenuItem.Checked = Convert.ToBoolean(fla)
        End If
        Dim base_str As String = My.Computer.Registry.CurrentUser.OpenSubKey("Software\K15\iCapture\").GetValue("base")
        If base_str <> "" Then
            base = base_str
        End If
        Dim form As String = My.Computer.Registry.CurrentUser.OpenSubKey("Software\K15\iCapture\").GetValue("format")
        If form <> "" Then
            format = form
        End If
        Dim start As String = My.Computer.Registry.CurrentUser.OpenSubKey("Software\microsoft\windows\currentversion\run").GetValue("iCapture")
        If start <> "" Then
            AutoStartToolStripMenuItem.Checked = True
        End If
        Dim pth As String = My.Computer.Registry.CurrentUser.OpenSubKey("Software\K15\iCapture\").GetValue("path")
        If pth <> "" Then
            If IO.Directory.Exists(pth) = True Then
                path = pth
            End If

        End If
        Dim auto As String = My.Computer.Registry.CurrentUser.OpenSubKey("Software\K15\iCapture\").GetValue("autosave")
        If auto <> "" Then
            AutoSaveToolStripMenuItem.Checked = Convert.ToBoolean(auto)
        End If

        Dim mypath As String = ""
        If AutoStartToolStripMenuItem.Checked = True Then
            mypath = Process.GetCurrentProcess.MainModule.FileName
        End If
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "iCapture", mypath, Microsoft.Win32.RegistryValueKind.String)


        NotifyIcon1.Icon = Me.Icon
        tmr1.Enabled = True
    End Sub

    Private Sub FlashToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlashToolStripMenuItem.Click
        On Error Resume Next
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "flash", FlashToolStripMenuItem.Checked, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub AutoSaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoSaveToolStripMenuItem.Click
        On Error Resume Next
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "autosave", AutoSaveToolStripMenuItem.Checked, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub CaptureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CaptureToolStripMenuItem.Click
        Dialog1.Show()
        Dialog1.Focus()
    End Sub

    Public Sub DoTimedSnap()
        Timer1.Interval = delay
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "snapcount", snapsLeft, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "delay", delay, Microsoft.Win32.RegistryValueKind.String)
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PrintScreen.PrintScreen()

        pic = Clipboard.GetImage


        If FlashToolStripMenuItem.Checked = True Then
            Flash.Opacity = 1
            Flash.Show()
        End If
        If AutoSaveToolStripMenuItem.Checked = True Then


            Save(path & "\" & base, pic)
        Else
            frmCapture.pcbMain.BackgroundImage = Clipboard.GetImage 'GetImage()
            frmCapture.Show()
            frmCapture.Focus()
        End If
        snapsLeft -= 1
        If snapsLeft = 0 Then
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub AutoStartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoStartToolStripMenuItem.Click
        On Error Resume Next
        Dim path As String = ""
        If AutoStartToolStripMenuItem.Checked = True Then
            path = Process.GetCurrentProcess.MainModule.FileName
        End If
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "iCapture", path, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub CropSnapToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CropSnapToolStripMenuItem.Click
        'On Error GoTo yo
        'On Error Resume Next

        If frmCrop.Visible = False Then

            If frmCrop.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            frmCrop.Close()
            frmCrop.Dispose()
            If FlashToolStripMenuItem.Checked = True Then
                Flash.Opacity = 1
                Flash.Show()
            End If
            If AutoSaveToolStripMenuItem.Checked = True Then
                pic = img
                Save(path & "\" & base, pic)
            Else
                frmCapture.pcbMain.BackgroundImage = img 'GetImage()
                frmCapture.Show()
                frmCapture.Focus()
            End If
        End If
        Exit Sub
yo:
        'MsgBox("CropSnapToolStripMenuItem_Click" & Chr(13) & Err.Description)
    End Sub

    Private Sub ChangePathToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePathToolStripMenuItem.Click
        Dim fd As New FolderBrowserDialog
        fd.SelectedPath = path
        fd.ShowNewFolderButton = True

        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "path", fd.SelectedPath, Microsoft.Win32.RegistryValueKind.String)
            path = fd.SelectedPath
        End If
    End Sub

    'Private Function GetImage()
    '    Return Clipboard.GetImage
    'End Function

    Private Sub Save(ByVal url As String, ByVal pic As Image)
        On Error GoTo errHandler
        Dim x As Integer = 1
        Dim url2 As String = url
        While (IO.File.Exists(url2 & format) = True)
            url2 = url & " " & x
            x += 1
        End While
        url2 = url2 & format
        'On Error GoTo errHandler

        Select Case format
            Case ".png"
                pic.Save(url2, Drawing.Imaging.ImageFormat.Png)
            Case ".bmp"
                pic.Save(url2, Drawing.Imaging.ImageFormat.Bmp)
            Case ".jpg"
                pic.Save(url2, Drawing.Imaging.ImageFormat.Jpeg)
        End Select
        Exit Sub
errHandler:
        'MessageBox.Show("Could not save to path '" & url2 & "'.", "iCapture", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        frmAbout.Show()
        frmAbout.Focus()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        frmSettings.Show()
        frmSettings.Focus()
    End Sub

    Private Sub InfoToolStripMenuItem_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.MouseEnter
        InfoToolStripMenuItem.Text = "Stop Timed Snap"
    End Sub

    Private Sub InfoToolStripMenuItem_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.MouseLeave
        InfoToolStripMenuItem.Text = InfoToolStripMenuItem.Tag
    End Sub

    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.Click
        Timer1.Enabled = False
        snapsLeft = 0
    End Sub
End Class