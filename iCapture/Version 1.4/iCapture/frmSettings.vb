Imports System.Windows.Forms

Public Class frmSettings

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If IO.Directory.Exists(txtPath.Text) = False Then
            MsgBox("The path does not exist.", MsgBoxStyle.Exclamation, "iCapture")
            Exit Sub
        End If

        If txtBaseText.Text = "" Then
            If MsgBox("The base text is empty. Are you sure you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        SaveSettings()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtBaseText.Text = My.Forms.Capture.base
        cmbFormat.Text = My.Forms.Capture.format
        txtPath.Text = My.Forms.Capture.path

        chkFlash.Checked = My.Forms.Capture.FlashToolStripMenuItem.Checked
        chkSave.Checked = My.Forms.Capture.AutoSaveToolStripMenuItem.Checked
        chkAuto.Checked = My.Forms.Capture.AutoStartToolStripMenuItem.Checked
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        Dim fd As New FolderBrowserDialog
        fd.SelectedPath = txtPath.Text
        fd.ShowNewFolderButton = True

        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtPath.Text = fd.SelectedPath
        End If
    End Sub

    Private Sub SaveSettings()
        'Register Flash
        My.Forms.Capture.FlashToolStripMenuItem.Checked = chkFlash.Checked
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "flash", chkFlash.Checked, Microsoft.Win32.RegistryValueKind.String)


        'Register Auto Save
        My.Forms.Capture.AutoSaveToolStripMenuItem.Checked = chkSave.Checked
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "autosave", chkSave.Checked, Microsoft.Win32.RegistryValueKind.String)


        'Register Auto Start
        My.Forms.Capture.AutoStartToolStripMenuItem.Checked = chkAuto.Checked
        Dim path As String = ""
        If chkAuto.Checked = True Then
            path = Process.GetCurrentProcess.MainModule.FileName
        End If
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "iCapture", path, Microsoft.Win32.RegistryValueKind.String)


        'Register Path
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "path", txtPath.Text, Microsoft.Win32.RegistryValueKind.String)
        My.Forms.Capture.path = txtPath.Text


        'Register Format
        My.Forms.Capture.format = cmbFormat.Text
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "format", cmbFormat.Text, Microsoft.Win32.RegistryValueKind.String)


        'Register Base
        My.Forms.Capture.base = txtBaseText.Text
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\K15\iCapture", "base", txtBaseText.Text, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub txtBaseText_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBaseText.KeyPress
        Dim keys As String = "\/:*?" & Chr(34) & "<>|"
        If keys.Contains(e.KeyChar) = True Then
            Beep()
            e.KeyChar = ""
        End If
    End Sub

    Private Sub txtBaseText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBaseText.TextChanged
        lblEg.Text = txtBaseText.Text & cmbFormat.Text & "," & Chr(13) & txtBaseText.Text & " 1" & cmbFormat.Text
    End Sub

    Private Sub cmbFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFormat.SelectedIndexChanged
        lblEg.Text = txtBaseText.Text & cmbFormat.Text & "," & Chr(13) & txtBaseText.Text & " 1" & cmbFormat.Text
    End Sub
End Class
