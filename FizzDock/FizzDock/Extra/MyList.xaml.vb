Partial Public Class MyList

    Public Property ImageSource() As ImageSource
        Get
            ImageSource = Image.Source
        End Get
        Set(ByVal value As ImageSource)
            Image.Source = value
        End Set
    End Property

    Public Property Cont() As String
        Get
            Cont = Text.Text
        End Get
        Set(ByVal value As String)
            Text.Text = value
        End Set
    End Property
End Class
