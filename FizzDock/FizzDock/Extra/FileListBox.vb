Public Class FileListBox

    Public Property Path() As String
        Get
            Path = file1.Path
        End Get
        Set(ByVal value As String)
            file1.Path = value
        End Set
    End Property

    Public Property Count() As Integer
        Get
            Count = file1.Items.Count
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Property SelectedItem() As String
        Get
            SelectedItem = file1.SelectedItem
        End Get
        Set(ByVal value As String)
            file1.SelectedItem = value
        End Set
    End Property

    Public Property SelectedIndex() As Integer
        Get
            SelectedIndex = file1.Items.Count
        End Get
        Set(ByVal value As Integer)
            file1.SelectedIndex = value
        End Set
    End Property

    Private Sub FileListBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
