Public Class frmIconManager
    Dim AppPath As String
    Dim Selected As String

    Private Sub Buttons_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAdd.MouseEnter, lblDel.MouseEnter
        sender.Image = pcbButton.Image
    End Sub

    Private Sub Buttons_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAdd.MouseLeave, lblDel.MouseLeave
        Dim nopic As New PictureBox
        sender.Image = nopic.Image
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AppPath = CurDir()
        lblDel.Enabled = False
        LoadIcons()
    End Sub

    Private Sub LoadIcons()
        For Each Icon As PictureBox In FlowPanel.Controls
            Icon.Dispose()
        Next
        File1.Path = AppPath
        Dim pcbLoad As New PictureBox
        For i = 0 To File1.Items.Count - 1
            pcbLoad.Load(File1.Path & "\" & File1.Items(i))
            Dim Picture As New PictureBox With {.BackgroundImageLayout = ImageLayout.Zoom, .Tag = File1.Path & "\" & File1.Items(i), _
                                                .BackgroundImage = pcbLoad.Image, .BackColor = Color.Transparent}


            Picture.Size = New Size(Picture.BackgroundImage.Width, Picture.BackgroundImage.Height)

repeat:
            If Picture.Size.Width > FlowPanel.Size.Width Then
                Dim oHeight As Double = Picture.Height
                Dim oWidth As Double = Picture.Width

                Picture.Size = New Size(FlowPanel.Width - 12, Picture.Height)
                Dim nHeight As Double
                nHeight = oHeight / (oWidth / Picture.Width)
                Picture.Size = New Size(FlowPanel.Width - 12, nHeight)
                GoTo repeat
            End If

            If Picture.Size.Height > FlowPanel.Size.Height Then
                Dim oHeight As Double = Picture.Height
                Dim oWidth As Double = Picture.Width

                Picture.Size = New Size(Picture.Width, FlowPanel.Height - 12)
                Dim nWidth As Double
                nWidth = oWidth / (oHeight / Picture.Height)
                Picture.Size = New Size(nWidth, FlowPanel.Height - 12)
                GoTo repeat
            End If

            AddHandler Picture.Click, AddressOf Icon_Selected
            FlowPanel.Controls.Add(Picture)
        Next
    End Sub

    Private Sub Icon_Selected(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each Icon As PictureBox In FlowPanel.Controls
            Icon.BackColor = Color.Transparent
            Icon.BorderStyle = BorderStyle.None
        Next
        Dim cc As New ColorConverter
        sender.BackColor = cc.ConvertFromString("#6fa1d9")
        sender.BorderStyle = BorderStyle.FixedSingle
        lblDel.Enabled = True
        Selected = sender.tag
    End Sub

    Private Sub lblDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDel.Click
        On Error GoTo yo
        For Each Icon As PictureBox In FlowPanel.Controls
            If Icon.Tag = Selected Then
                Icon.Dispose()
                IO.File.Delete(Icon.Tag)
                Selected = ""
                lblDel.Enabled = False
            End If
        Next
        Exit Sub
yo:
        MsgBox(Err.Description, MsgBoxStyle.Exclamation, "IconManager")
    End Sub

    Private Sub lblAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAdd.Click
        Dim ofd1 As New OpenFileDialog
        ofd1.Filter = "PNG Format (*.png)|*.png"
        If ofd1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblDel.Enabled = False
            AddNew(Add(ofd1.FileName))
        End If
    End Sub

    Private Sub AddNew(ByVal Path As String)
        Dim pcbLoad As New PictureBox
        pcbLoad.Load(File1.Path & "\" & Path)
        Dim Picture As New PictureBox With {.BackgroundImageLayout = ImageLayout.Zoom, .Tag = File1.Path & "\" & Path, _
                                    .BackgroundImage = pcbLoad.Image, .BackColor = Color.Transparent}


        Picture.Size = New Size(Picture.BackgroundImage.Width, Picture.BackgroundImage.Height)

repeat:
        If Picture.Size.Width > FlowPanel.Size.Width Then
            Dim oHeight As Double = Picture.Height
            Dim oWidth As Double = Picture.Width

            Picture.Size = New Size(FlowPanel.Width - 12, Picture.Height)
            Dim nHeight As Double
            nHeight = oHeight / (oWidth / Picture.Width)
            Picture.Size = New Size(FlowPanel.Width - 12, nHeight)
            GoTo repeat
        End If

        If Picture.Size.Height > FlowPanel.Size.Height Then
            Dim oHeight As Double = Picture.Height
            Dim oWidth As Double = Picture.Width

            Picture.Size = New Size(Picture.Width, FlowPanel.Height - 12)
            Dim nWidth As Double
            nWidth = oWidth / (oHeight / Picture.Height)
            Picture.Size = New Size(nWidth, FlowPanel.Height - 12)
            GoTo repeat
        End If

        AddHandler Picture.Click, AddressOf Icon_Selected
        FlowPanel.Controls.Add(Picture)
    End Sub

    Private Function Add(ByVal Path As String)
        Dim File As String
        Dim ReturnPath As String
        For i = Path.Length To 1 Step -1
            If Mid(Path, i, 1) <> "\" Then
                File = File & Mid(Path, i, 1)
            Else
                Exit For
            End If
        Next
        Dim File2 As String = File
        File = ""
        For x = File2.Length To 1 Step -1
            File = File & Mid(File2, x, 1)
        Next
        If IO.File.Exists(AppPath & "\" & File) = False Then
            IO.File.Copy(Path, AppPath & "\" & File)
            ReturnPath = File
        Else
repeat:
            Randomize()
            Dim ri As String = (Rnd() * 100000) + 1 & ".png"
            Try
                IO.File.Copy(Path, AppPath & "\" & ri)
                ReturnPath = ri
            Catch
                GoTo repeat
            End Try
        End If
        Return ReturnPath
    End Function
End Class
