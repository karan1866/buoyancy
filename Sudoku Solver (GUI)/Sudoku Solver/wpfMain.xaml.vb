Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation
Imports Microsoft.VisualBasic

Partial Public Class wpfMain

#Region "Declarations"

    Dim selected As Integer = 1
    Dim cDown As Boolean    'Close Button
    Dim mDown As Boolean    'Minimize Button
    Dim dDown As Boolean    'Dragger
    Dim Table(8, 8) As String
    Dim result As Integer

#End Region

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
    End Sub

#Region "Box Events"

    Private Sub Checked(ByVal sender As Box)
        If sender.ID.Length = 2 Then            'ie it is a part of the table
            For Each Panel As WrapPanel In InnerTable.Children
                For Each Box As Box In Panel.Children
                    If Box.ID = sender.ID Then
                        Box.Label.Foreground = New SolidColorBrush(Color.FromRgb(34, 34, 34))
                        Box.Label.Content = selected
                    End If
                Next
            Next
        ElseIf sender.ID.Length = 1 Then        'ie it is a part of digits
            selected = Convert.ToInt16(sender.Label.Content)
            For Each Box As Box In Digits.Children
                If Val(Box.Label.Content) <> Val(sender.Label.Content) Then
                    Box.Leave()
                End If
            Next
        End If
    End Sub

#End Region

#Region "Initialize Boxes"

    Private Sub CreateBoard()
        Dim i As Integer
        Dim b As Integer
        For Each Panel As WrapPanel In InnerTable.Children
            b += 1
            For i = 1 To 9
                Dim box As New Box With {.Height = 40, .Width = 40, .ID = i & b}
                box.Label.Content = ""

                AddHandler box.Checked, AddressOf Checked

                Panel.Children.Add(box)
            Next
        Next
    End Sub

    Private Sub CreateDigits()
        Dim i As Integer
        For i = 1 To 9
            Dim box As New Box With {.Height = 40, .Width = 40, .ID = i.ToString, .Digit = True}
            box.Label.Content = i
            box.Label.Foreground = New SolidColorBrush(Color.FromRgb(68, 68, 68))
            If i = 1 Then
                box.Radius.Height = 5
                box.Cell.Margin = New Thickness(-1)
                box.Cell.Background = New SolidColorBrush(box.color)
                box.down = True
            End If
            AddHandler box.Checked, AddressOf Checked

            Digits.Children.Add(box)
        Next
    End Sub

#End Region

#Region "Buttons"

    Private Sub Solve_Click() Handles Solve.Click
        GetBoard()
        Dim logic As New Logic

        Select Case logic.Start(Table, 1)
            Case 1
                PrintBoard(False)
            Case -2
                PrintBoard(True)
                Dim blur As New Effects.BlurBitmapEffect With {.KernelType = Effects.KernelType.Gaussian, .Radius = 7}
                Sudoku.BitmapEffect = blur

                Text.Text = "Invalid Entry !"

                Message.Visibility = Windows.Visibility.Visible
                Brute.Visibility = Windows.Visibility.Hidden

                Solve.Visibility = Windows.Visibility.Hidden
                Clear.Visibility = Windows.Visibility.Hidden

                result = -2
            Case 0
                Dim blur As New Effects.BlurBitmapEffect With {.KernelType = Effects.KernelType.Gaussian, .Radius = 7}
                Sudoku.BitmapEffect = blur

                Text.Text = "More than one solution !"

                Message.Visibility = Windows.Visibility.Visible
                Brute.Visibility = Windows.Visibility.Visible

                Solve.Visibility = Windows.Visibility.Hidden
                Clear.Visibility = Windows.Visibility.Hidden

                result = 0
            Case -1
                Dim blur As New Effects.BlurBitmapEffect With {.KernelType = Effects.KernelType.Gaussian, .Radius = 7}
                Sudoku.BitmapEffect = blur

                Text.Text = "Invalid Sudoku !"

                Message.Visibility = Windows.Visibility.Visible
                Brute.Visibility = Windows.Visibility.Hidden

                Solve.Visibility = Windows.Visibility.Hidden
                Clear.Visibility = Windows.Visibility.Hidden

                result = -1
        End Select

    End Sub

    Private Sub Clear_Click() Handles Clear.Click
        Dim solved As Boolean
        For Each Panel As WrapPanel In InnerTable.Children
            For Each Box As Box In Panel.Children
                If Box.Label.Content.ToString <> "" Then
                    Dim brush As SolidColorBrush = Box.Label.Foreground
                    If brush.Color <> Color.FromRgb(34, 34, 34) Then
                        Box.Label.Content = ""
                        Box.Label.Foreground = brush
                        solved = True
                    End If
                End If
            Next
        Next

        If solved = False Then
            For Each Panel As WrapPanel In InnerTable.Children
                For Each Box As Box In Panel.Children

                    Box.Label.Content = ""

                Next
            Next
        End If
    End Sub

    Private Sub ok_Click() Handles ok.Click
        Sudoku.BitmapEffect = Nothing
        Solve.Visibility = Windows.Visibility.Visible
        Clear.Visibility = Windows.Visibility.Visible
        Message.Visibility = Windows.Visibility.Hidden
        Brute.Visibility = Windows.Visibility.Hidden

        If result = 0 Then
            Select Case True
                Case Op1.IsChecked      'Brute Force Normal
                    Dim logic As New Logic
                    logic.Start(Table, 2)
                    PrintBoard(False)
                Case Op2.IsChecked      'Brute Force Random
                    Dim logic As New Logic
                    logic.Start(Table, 3)
                    PrintBoard(False)
                Case Op3.IsChecked      'No Action

            End Select
        End If

        Keylogger.Focus()
    End Sub

#End Region

#Region "Close and Minimize Buttons"

    Private Sub Close_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles _Close.MouseDown
        cDown = True
    End Sub

    Private Sub Close_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles _Close.MouseUp
        If cDown = True Then
            Me.Close()
        End If
        cDown = False
    End Sub

    Private Sub Min_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Min.MouseDown
        mDown = True
    End Sub

    Private Sub Min_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Min.MouseUp
        If mDown = True Then
            Me.WindowState = Windows.WindowState.Minimized
        End If
        mDown = False
    End Sub

#End Region

#Region "Window Drag"

    Private Sub DragSpot_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles DragSpot.MouseDown
        dDown = True
    End Sub

    Private Sub DragSpot_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles DragSpot.MouseLeave
        dDown = False
    End Sub

    Private Sub DragSpot_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles DragSpot.MouseMove
        If e.LeftButton = Input.MouseButtonState.Pressed Then
            If dDown = True Then
                Me.DragMove()
            End If
        End If
    End Sub

    Private Sub DragSpot_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles DragSpot.MouseUp
        dDown = False
    End Sub

#End Region

#Region "Key Press"

    Private Sub Keylogger_TextChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles Keylogger.TextChanged
        If Val(Keylogger.Text) > 0 Then
            Keys(Val(Keylogger.Text))
        End If
        Keylogger.Text = ""
    End Sub

    Private Sub Keys(ByVal x As Integer)

        For Each Box As Box In Digits.Children
            If Val(Box.Label.Content) <> x Then
                Box.Leave()
            Else
                Box.Radius.Height = 5
                Box.MouseEvents = True
                Box.Cell.Background = New SolidColorBrush(Box.color)
                Box.Cell_MouseEnter()
                Box.down = True
                Checked(Box)
            End If
        Next
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfMain_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Keylogger.Focus()
        CreateBoard()
        CreateDigits()
    End Sub

    Private Sub wpfMain_Deactivated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivated
        Shadow.Opacity = 0.6
    End Sub

    Private Sub wpfMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Shadow.Opacity = 1
    End Sub

#End Region

#Region "Functions"

    Private Sub GetBoard()
        Dim i As Integer, j As Integer
        Dim bPoint As New System.Drawing.Point 'Box Point
        Dim dPoint As New System.Drawing.Point 'Digit Point
        For Each Panel As WrapPanel In InnerTable.Children
            bPoint = New System.Drawing.Point(i Mod 3, Int(i / 3))
            For Each Box As Box In Panel.Children
                dPoint = New System.Drawing.Point((j Mod 3) + (bPoint.X * 3), (Int(j / 3)) + (bPoint.Y * 3))

                If Box.Label.Content.ToString <> "" Then
                    Dim brush As SolidColorBrush = Box.Label.Foreground
                    If brush.Color = Color.FromRgb(34, 34, 34) Then
                        Table(dPoint.X, dPoint.Y) = Box.Label.Content.ToString
                    End If

                Else
                    Table(dPoint.X, dPoint.Y) = "123456789"
                End If
                j += 1

                If j = 9 Then
                    j = 0
                End If
            Next
            i += 1

            If i = 9 Then
                i = 0
            End If
        Next
    End Sub

    Private Sub PrintBoard(ByVal Errors As Boolean)
        Dim i As Integer, j As Integer
        Dim bPoint As New System.Drawing.Point 'Box Point
        Dim point As New System.Drawing.Point 'Digit Point
        For Each Panel As WrapPanel In InnerTable.Children
            bPoint = New System.Drawing.Point(i Mod 3, Int(i / 3))
            For Each Box As Box In Panel.Children

                point = New System.Drawing.Point((j Mod 3) + (bPoint.X * 3), (Int(j / 3)) + (bPoint.Y * 3))

                If Errors = False Then

                    If Box.Label.Content.ToString = "" Then
                        Box.Label.Foreground = New SolidColorBrush(Color.FromRgb(136, 136, 136))
                    Else
                        Box.Label.Foreground = New SolidColorBrush(Color.FromRgb(34, 34, 34))
                    End If
                    If Table(point.X, point.Y).ToCharArray.Length = 1 Then
                        Box.Label.Content = Table(point.X, point.Y)
                    End If

                Else

                    If Table(point.X, point.Y).ToString.Length = 1 Then
                        Box.Label.Foreground = New SolidColorBrush(Color.FromRgb(220, 0, 0))
                        Box.Label.Content = Table(point.X, point.Y)
                    End If
                End If


                j += 1
                If j = 9 Then
                    j = 0
                End If

            Next

            i += 1
            If i = 9 Then
                i = 0
            End If
        Next
    End Sub

#End Region

End Class
