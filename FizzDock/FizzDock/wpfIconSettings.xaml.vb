﻿Partial Public Class wpfIconSettings
    Dim ImSConverter As New ImageSourceConverter
    Dim _MoveDown As Boolean

    Private Sub Window1_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim a As New ImageSourceConverter
        imgPreview.Source = a.ConvertFromString(AppPath & "\Icons\data\Help.png")
        Load()
        Icons()
    End Sub

    Private Sub Icons()
        Dim file1 As New FileListBox
        file1.Path = AppPath & "\Icons"
        lstIcons.Items.Clear()
        For i = 0 To file1.Count - 1
            Dim a As New ListBoxItem
            Dim b As New MyList
            file1.SelectedIndex = i
            b.Cont = file1.SelectedItem
            b.ImageSource = ImSConverter.ConvertFromString(file1.Path & "\" & b.Cont)
            lstIcons.Items.Add(b)
        Next
    End Sub

    Private Sub Load()
        Dim setting As String
        Dim lsticon As New ListBox
        Dim lstpath As New ListBox
        Dim name As String = ""
        Dim ico As String = ""
        Dim path As String = ""
        Dim i As Integer
        Dim r As Integer = 0
        setting = IO.File.ReadAllText(AppPath & "\Settings\icons.dat")
        For i = 1 To setting.Length
            Select Case Mid(setting, i, 1)
                Case "<"
                    r = 1
                    i = i + 1
                Case "|"
                    r = 2
                    i = i + 1
                Case "?"
                    r = 3
                    i = i + 1
                Case ">"
                    Dim a As New ListBoxItem
                    a.Content = name
                    a.Template = Me.Resources.Item("ListBoxTemplate")
                    If IO.File.Exists(AppPath & "\Icons\" & ico) = True Then
                        img2.Source = ImSConverter.ConvertFromString(AppPath & "\Icons\" & ico)
                    Else
                        img2.Source = ImSConverter.ConvertFromString(AppPath & "\Icons\data\Help.png")
                    End If
                    Me.lstIconList.Items.Add(ico)
                    ico = ""
                    Me.lstPath.Items.Add(path)
                    path = ""
                    Me.lstName.Items.Add(a)
                    name = ""
                    r = 0
            End Select
            If r = 1 Then
                ico = ico & Mid(setting, i, 1)
            ElseIf r = 2 Then
                path = path & Mid(setting, i, 1)
            ElseIf r = 3 Then
                name = name & Mid(setting, i, 1)
            End If
        Next
    End Sub

    Private Sub lstName_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles lstName.SelectionChanged
        On Error GoTo go
        lstIconList.SelectedIndex = lstName.SelectedIndex
        lstPath.SelectedIndex = lstName.SelectedIndex
        If IO.File.Exists(AppPath & "\Icons\" & lstIconList.Items(lstIconList.SelectedIndex)) = True Then
            img.Source = ImSConverter.ConvertFromString(AppPath & "\Icons\" & lstIconList.Items(lstIconList.SelectedIndex))
        Else
            img.Source = ImSConverter.ConvertFromString(AppPath & "\Icons\data\Help.png")
            grdIcons.IsEnabled = True
            Exit Sub
        End If
        Dim i As Integer
        Dim name As String = lstIconList.Items(lstIconList.SelectedIndex)
        For i = 0 To lstIcons.Items.Count - 1
            If lstIcons.Items(i).Cont = name Then

                lstIcons.SelectedIndex = i
                Exit For
            End If
        Next
        grdIcons.IsEnabled = True
go:
    End Sub

    Private Sub lstIcons_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles lstIcons.SelectionChanged
        'On Error Resume Next
        If lstName.SelectedIndex <> -1 Then
            Dim a As Integer
            Dim i As Integer
            Dim nLst As New ListBox
            a = lstIconList.SelectedIndex
            For i = 0 To lstIconList.Items.Count - 1
                If a = i Then
                    nLst.Items.Add(lstIcons.SelectedItem.Cont)
                Else
                    nLst.Items.Add(lstIconList.Items(i))
                End If
            Next
            lstIconList.Items.Clear()
            For i = 0 To nLst.Items.Count - 1
                lstIconList.Items.Add(nLst.Items(i))
            Next
            lstIconList.SelectedIndex = a
            On Error Resume Next
            img.Source = ImSConverter.ConvertFromString(AppPath & "\Icons\" & lstIconList.Items(lstIconList.SelectedIndex))
        End If
        Save()
    End Sub

    Private Sub Input()
        Dim str As String

        str = InputBox("Enter a name:", "Rename", lstName.Items(lstName.SelectedIndex).Content)
        If str <> "" Then
            lstName.SelectedItem.Content = str
        End If
        Save()
    End Sub

    Private Sub Del()
        Dim str As String
        Dim a As Integer = lstIconList.SelectedIndex


        str = lstName.SelectedItem.Content
        If MsgBox("Are you sure you want to delete '" & str & "'?", MsgBoxStyle.YesNo, "Confirm Delete") = MessageBoxResult.Yes Then
            lstIconList.Items.Remove(lstIconList.Items(a))
            lstName.Items.Remove(lstName.Items(a))
            lstPath.Items.Remove(lstPath.Items(a))
            If a = 0 Then
                If lstName.Items.Count <> a Then
                    lstName.SelectedIndex = 0
                Else
                    grdIcons.IsEnabled = False
                End If
            Else
                lstName.SelectedIndex = a - 1
            End If
        End If
        Save()
    End Sub

    Private Sub Save()
        Dim setting As String = ""
        Dim name As String = ""
        Dim ico As String = ""
        Dim path As String = ""
        Dim i As Integer
        Dim r As Integer = 0


        For i = 0 To lstIconList.Items.Count - 1
            Dim text As String = "<" & lstIconList.Items(i).ToString & "|" & lstPath.Items(i) & "?" & lstName.Items(i).Content & ">"
            setting = setting & text
        Next

        IO.File.WriteAllText(AppPath & "\Settings\icons.dat", setting)

    End Sub

    Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles txtName.GotFocus, txtTarget.GotFocus
        Dim b As New SolidColorBrush
        b.Color = Colors.Black
        If sender.Name = "txtName" Then
            Label1.Foreground = b
        Else
            lblTarget.Foreground = b
        End If
    End Sub

    Private Sub cmdAddIcon_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdAddIcon.Click
        Dim Perfect As Boolean = True
        If txtName.Text = "" Then
            Dim b As New SolidColorBrush
            b.Color = ColorConverter.ConvertFromString("#ff3434")
            Label1.Foreground = b
            Perfect = False
        End If
        If txtTarget.Text = "" Then
            Dim b As New SolidColorBrush
            b.Color = ColorConverter.ConvertFromString("#ff3434")
            lblTarget.Foreground = b
            Perfect = False
        End If
        If Perfect = True Then
            Dim b As New SolidColorBrush
            b.Color = ColorConverter.ConvertFromString("#FFFFFFFF")
            lblTarget.Foreground = b
            Label1.Background = b
        End If
        If Perfect = True Then
            Dim a As New ListBoxItem
            a.Content = txtName.Text
            a.Template = Me.Resources("ListBoxTemplate")
            lstName.Items.Add(a)
            '            If IO.File.Exists(txtTarget.Text) = True Then
            '                Dim str As String
            'repeat:
            '                Randomize()
            '                str = "data\img" & (Rnd() * 10000) + 1
            '                If IO.File.Exists(AppPath & "\Icons\" & str) = False Then
            '                    System.Drawing.Icon.ExtractAssociatedIcon(txtTarget.Text).Save(IO.File.OpenWrite(AppPath & "\Icons\" & str & ".ico"))
            '                    'Dim sa As New ImageSourceConverter
            '                    'imgPreview.Source = sa.ConvertFromString(AppPath & "\Icons\" & str & ".png")
            '                    Icons()
            '                Else
            '                    GoTo repeat
            '                End If
            '                img2.Source = imgPreview.Source
            '                lstIconList.Items.Add(str & ".ico")
            '                lstPath.Items.Add(txtTarget.Text)
            '            Else
            '                Dim sd As New ImageSourceConverter
            '                'imgPreview.Source = sd.ConvertFromString(AppPath & "\Icons\data\Help.png")
            '                'img2.Source = imgPreview.Source
            '                lstIconList.Items.Add("data\Help.png")
            '                lstPath.Items.Add(txtTarget.Text)
            '            End If
            lstIconList.Items.Add("data\Help.png")
            lstPath.Items.Add(txtTarget.Text)
            txtTarget.Text = ""
            txtName.Text = ""
        End If
        Save()
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdBrowse.Click
        Dim add As Window = New wpfMsgBox
        If add.ShowDialog <> Forms.DialogResult.Cancel Then
            If Dock.DialogResult <> "" Then
                txtTarget.Text = Dock.DialogResult
            End If
        End If
    End Sub

    Private Sub txtTarget_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtTarget.KeyUp
        '        On Error GoTo yo
        '        If IO.File.Exists(txtTarget.Text) = True Then
        '            Dim str As String
        'repeat:
        '            Randomize()
        '            str = (Rnd() * 10000) + 1
        '            If IO.File.Exists(AppPath & "\Icons\data\" & str & ".ico") = False Then
        '                Dim ico As New Windows.Forms.Form
        '                ico.Icon = System.Drawing.Icon.ExtractAssociatedIcon(txtTarget.Text)
        '                Dim sw As System.IO.StreamWriter = System.IO.File.CreateText(AppPath & "\Icons\data\" & str & ".ico")
        '                ico.Icon.Save(sw.BaseStream)
        '                ico.Dispose()
        '                sw.Close()
        '                Dim a As New ImageSourceConverter
        '                imgPreview.Source = a.ConvertFromString(AppPath & "\Icons\data\" & str & ".ico")
        '                Icons()
        '            Else
        '                GoTo repeat
        '            End If
        '        Else
        '            Dim sd As New ImageSourceConverter
        '            imgPreview.Source = sd.ConvertFromString(AppPath & "\Icons\data\Help.png")
        '        End If
        '        Exit Sub
        'yo:
        '        MsgBox(Err.Description)
        '        Dim s As New ImageSourceConverter
        '        imgPreview.Source = s.ConvertFromString(AppPath & "\Icons\data\Help.png")
    End Sub

    '    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdAdd.Click
    '        Dim add As New Windows.Forms.OpenFileDialog
    '        add.Filter = "PNG Format (*.png)|*.png"
    '        If add.ShowDialog = Forms.DialogResult.OK Then
    '            Dim File As String = ""
    '            For i = add.FileName.Length To 1 Step -1
    '                If Mid(add.FileName, i, 1) <> "\" Then
    '                    File = File & Mid(add.FileName, i, 1)
    '                Else
    '                    Exit For
    '                End If
    '            Next
    '            Dim File2 As String = File
    '            File = ""
    '            For x = File2.Length To 1 Step -1
    '                File = File & Mid(File2, x, 1)
    '            Next
    'repeat:
    '            Try
    '                IO.File.Copy(add.FileName, AppPath & "\Icons\" & File)
    '                AddIcon(File)
    '            Catch
    '                If Err.Number = 57 Then
    '                    Randomize()
    '                    Dim ri As String = (Rnd() * 100000) + 1 & ".png"
    '                    Try
    '                        IO.File.Copy(add.FileName, AppPath & "\Icons\" & ri)
    '                        AddIcon(ri)
    '                    Catch
    '                        GoTo repeat
    '                    End Try
    '                End If
    '            End Try
    '        End If
    '        Save()
    '    End Sub

    '    Private Sub AddIcon(ByVal path As String)
    '        Dim a As New ListBoxItem
    '        Dim b As New MyList
    '        b.Cont = path
    '        b.ImageSource = ImSConverter.ConvertFromString(AppPath & "\Icons\" & b.Cont)
    '        lstIcons.Items.Add(b)
    '    End Sub

    Private Sub PathChanged()
        Dim add As Window = New wpfMsgBox
        If add.ShowDialog <> Forms.DialogResult.Cancel Then
            If Dock.DialogResult <> "" Then
                Dim x As Integer = lstPath.SelectedIndex
                Dim bckList As New ListBox
                For i = 0 To lstPath.Items.Count - 1
                    bckList.Items.Add(lstPath.Items(i))
                Next
                lstPath.Items.Clear()
                For i = 0 To bckList.Items.Count - 1
                    If i <> x Then
                        lstPath.Items.Add(bckList.Items(i))
                    Else
                        lstPath.Items.Add(Dock.DialogResult)
                    End If
                Next
                lstName.Items.Refresh()
                lstName.SelectedIndex = x
            End If
        End If
        Save()
    End Sub

    Private Sub MoveUp()
        Dim index As Integer = lstName.SelectedIndex
        If lstName.SelectedIndex <> 0 Then
            Dim lst1 As New ListBox
            Dim lst2 As New ListBox
            Dim lst3 As New ListBox


            For i = 0 To lstName.Items.Count - 1
                lst1.Items.Add(lstName.Items(i).Content)
                lst2.Items.Add(lstIconList.Items(i))
                lst3.Items.Add(lstPath.Items(i))
            Next

            lstIconList.Items.Clear()
            lstName.Items.Clear()
            lstPath.Items.Clear()

            For i = 0 To lst1.Items.Count - 1
                Dim item As New ListBoxItem With {.Template = Me.Resources("ListBoxTemplate")}
                If i <> index - 1 Then
                    item.Content = (lst1.Items(i))
                    lstName.Items.Add(item)
                    lstIconList.Items.Add(lst2.Items(i))
                    lstPath.Items.Add(lst3.Items(i))
                Else
                    item.Content = (lst1.Items(i + 1))
                    lstName.Items.Add(item)
                    lstIconList.Items.Add(lst2.Items(i + 1))
                    lstPath.Items.Add(lst3.Items(i + 1))

                    Dim item2 As New ListBoxItem With {.Template = Me.Resources("ListBoxTemplate")}

                    item2.Content = (lst1.Items(i))
                    lstName.Items.Add(item2)
                    lstIconList.Items.Add(lst2.Items(i))
                    lstPath.Items.Add(lst3.Items(i))

                    i = i + 1
                End If
            Next
            index = index - 1
        End If
        Save()
    End Sub

    Private Sub MoveDown()
        Dim index As Integer = lstName.SelectedIndex
        If lstName.SelectedIndex <> lstName.Items.Count - 1 Then
            Dim lst1 As New ListBox
            Dim lst2 As New ListBox
            Dim lst3 As New ListBox


            For i = 0 To lstName.Items.Count - 1
                lst1.Items.Add(lstName.Items(i).Content)
                lst2.Items.Add(lstIconList.Items(i))
                lst3.Items.Add(lstPath.Items(i))
            Next

            lstIconList.Items.Clear()
            lstName.Items.Clear()
            lstPath.Items.Clear()

            For i = 0 To lst1.Items.Count - 1
                Dim item As New ListBoxItem With {.Template = Me.Resources("ListBoxTemplate")}
                If i <> index Then
                    item.Content = (lst1.Items(i))
                    lstName.Items.Add(item)
                    lstIconList.Items.Add(lst2.Items(i))
                    lstPath.Items.Add(lst3.Items(i))
                Else
                    item.Content = (lst1.Items(i + 1))
                    lstName.Items.Add(item)
                    lstIconList.Items.Add(lst2.Items(i + 1))
                    lstPath.Items.Add(lst3.Items(i + 1))

                    Dim item2 As New ListBoxItem With {.Template = Me.Resources("ListBoxTemplate")}

                    item2.Content = (lst1.Items(i))
                    lstName.Items.Add(item2)
                    lstIconList.Items.Add(lst2.Items(i))
                    lstPath.Items.Add(lst3.Items(i))

                    i = i + 1
                End If
            Next
            index = index + 1
        End If
        Save()
    End Sub

    Private Sub Close_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cls.Click
        Me.Close()
    End Sub

    Private Sub Move_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Move.MouseDown
        _MoveDown = True
    End Sub

    Private Sub Move_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles Move.MouseMove
        If _MoveDown = True Then
            If e.LeftButton = MouseButtonState.Pressed Then
                Me.DragMove()
            End If
        End If
    End Sub

    Private Sub Move_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Move.MouseUp
        _MoveDown = False
    End Sub

    Private Sub rdbTarget_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdbTarget.Checked
        On Error Resume Next
        lblTarget.IsEnabled = True
        txtTarget.IsEnabled = True
        cmdBrowse.IsEnabled = True
        lblFolder.IsEnabled = False
        txtFolder.IsEnabled = False
        cmdBrowse2.IsEnabled = False
    End Sub

    Private Sub rdbTarget_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdbTarget.Unchecked
        On Error Resume Next
        lblTarget.IsEnabled = False
        txtTarget.IsEnabled = False
        cmdBrowse.IsEnabled = False
        lblFolder.IsEnabled = True
        txtFolder.IsEnabled = True
        cmdBrowse2.IsEnabled = True
    End Sub

    Private Sub cmdBrowse2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdBrowse2.Click
        Dim a As New Windows.Forms.FolderBrowserDialog
        a.ShowNewFolderButton = True
        If a.ShowDialog = Forms.DialogResult.OK Then
            txtFolder.Text = a.SelectedPath
        End If
    End Sub
End Class
