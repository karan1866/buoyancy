Public Class DirListBox

    Public Property Path() As String
        Get
            Path = dir1.Path
        End Get
        Set(ByVal value As String)
            dir1.Path = value
        End Set
    End Property

    Public Property Count() As Integer
        Get
            Count = dir1.DirListCount
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Property SelectedItem() As String
        Get
            SelectedItem = dir1.SelectedItem
        End Get
        Set(ByVal value As String)
            dir1.SelectedItem = value
        End Set
    End Property

    Public Property Index() As Integer
        Get
            Index = dir1.DirListIndex
        End Get
        Set(ByVal value As Integer)
            dir1.DirListIndex = value
        End Set
    End Property
End Class
