Partial Public Class wpfMsgBox

    Private Sub cmdFile_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdFile.Click
        Dim a As New Windows.Forms.OpenFileDialog
        a.Filter = "All Files (*.*)|*.*"
        If a.ShowDialog = Forms.DialogResult.OK Then
            Dock.DialogResult = a.FileName
            Me.DialogResult = Forms.DialogResult.Yes
            Me.Close()
        Else
            Me.DialogResult = Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub cmdFolder_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdFolder.Click
        Dim a As New Windows.Forms.FolderBrowserDialog
        a.ShowNewFolderButton = True
        If a.ShowDialog = Forms.DialogResult.OK Then
            Dock.DialogResult = a.SelectedPath
            Me.DialogResult = Forms.DialogResult.No
            Me.Close()
        Else
            Me.DialogResult = Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub Window1_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        grdMain.Height = 47
        grdMain.Width = 183
    End Sub
End Class
