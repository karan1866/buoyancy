Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation

Partial Public Class Box

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
    End Sub

#Region "Declarations"

    Public color As New Windows.Media.Color With {.A = 255, .R = 158, .G = 206, .B = 255}
    Public white As New Windows.Media.Color With {.A = 255, .R = 255, .G = 255, .B = 255}
    Dim _ID As String
    Dim toHeight As Integer = 5
    Public down As Boolean
    Dim fromHeight As Integer
    Dim Selected As Object
    Public Event Checked(ByVal sender As Box)
    Public Digit As Boolean = False         'Digit Box or Normal Box
    Public MouseEvents As Boolean = True

#End Region

#Region "Cell MouseEvents"

    Private Sub Cell_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Cell.MouseDown
        If e.LeftButton = Input.MouseButtonState.Pressed Then
            If Digit = True Then
                If down = False Then
                    down = True
                    Cell.Background = New SolidColorBrush(color)
                    RaiseEvent Checked(Me)
                End If
            Else
                RaiseEvent Checked(Me)
            End If
        ElseIf e.RightButton = Input.MouseButtonState.Pressed Then
            If Digit = False Then
                Label.Content = ""
            End If
        End If
    End Sub

    Public Sub Cell_MouseEnter() Handles Cell.MouseEnter
        If MouseEvents = True Then
            If down = False Then
                GoTo animation
            End If
        Else
            GoTo animation
        End If

        Exit Sub
animation:

        Dim animate As New DoubleAnimation
        animate.From = Radius.Height 'fromHeight
        animate.To = toHeight
        animate.Duration = TimeSpan.FromMilliseconds(150)

        Dim animate2 As New ThicknessAnimation
        animate2.From = Cell.Margin 'New Thickness(1)
        animate2.To = New Thickness(-1)
        animate2.Duration = TimeSpan.FromMilliseconds(150)

        Cell.BeginAnimation(Border.MarginProperty, animate2)
        Radius.BeginAnimation(Grid.HeightProperty, animate)

        MouseEvents = True
    End Sub

    Private Sub Cell_MouseLeave() Handles Cell.MouseLeave
        If down = False Then
            Dim animate As New DoubleAnimation
            animate.From = toHeight
            animate.To = fromHeight
            animate.Duration = TimeSpan.FromMilliseconds(500)

            Dim animate2 As New ThicknessAnimation
            animate2.From = New Thickness(-1)
            animate2.To = New Thickness(1)
            animate2.Duration = TimeSpan.FromMilliseconds(500)

            Cell.BeginAnimation(Border.MarginProperty, animate2)
            Radius.BeginAnimation(Grid.HeightProperty, animate)
        End If
    End Sub

#End Region

#Region "Box Properties & Functions"

    Public Property ID() As String
        Get
            ID = _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Sub Leave()
        If down = True Then
            down = False
            Cell.Background = New SolidColorBrush(white)
            Cell_MouseLeave()
        End If
    End Sub

#End Region

End Class
