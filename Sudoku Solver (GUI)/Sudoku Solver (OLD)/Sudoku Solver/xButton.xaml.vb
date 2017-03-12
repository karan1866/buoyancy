Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation

Partial Public Class xButton

    Public Event Click()

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
    End Sub

#Region "Properties"

    Public Property Brush_Normal() As Brush
        Get
            Brush_Normal = Button.Background
        End Get
        Set(ByVal value As Brush)
            Button.Background = value
        End Set
    End Property

    Public Property Brush_Up() As Brush
        Get
            Brush_Up = Up.Background
        End Get
        Set(ByVal value As Brush)
            Up.Background = value
        End Set
    End Property

    Public Property Brush_Down() As Brush
        Get
            Brush_Down = Down.Background
        End Get
        Set(ByVal value As Brush)
            Down.Background = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Text = Content.Content.ToString
        End Get
        Set(ByVal value As String)
            Content.Content = value
        End Set
    End Property

#End Region

#Region "Mouse Events"

    Private Sub Button_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles xButton.MouseDown
        Up.Visibility = Windows.Visibility.Hidden
        Down.Visibility = Windows.Visibility.Visible

    End Sub

    Private Sub Button_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles xButton.MouseEnter
        If e.LeftButton = Input.MouseButtonState.Pressed Then
            Up.Visibility = Windows.Visibility.Hidden
            Down.Visibility = Windows.Visibility.Visible
        Else
            Up.Visibility = Windows.Visibility.Visible
            Down.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub Button_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles xButton.MouseLeave
        Up.Visibility = Windows.Visibility.Hidden
        Down.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub Button_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles xButton.MouseUp
        Up.Visibility = Windows.Visibility.Visible
        Down.Visibility = Windows.Visibility.Hidden
        RaiseEvent Click()
    End Sub

#End Region

End Class
