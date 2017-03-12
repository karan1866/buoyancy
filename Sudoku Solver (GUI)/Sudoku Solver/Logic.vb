Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class Logic

#Region "Declarations"

    ''\Done = 1         'Solved
    '' \Bad = 0          'More than 1 solution
    ''  \BadEntry = -2    'Invalid Entry
    ''   \Invalid = -1    'Invalid Sudoku

    Dim Table(8, 8) As String
    Dim Complete(8, 8) As String
    Dim BoxDone(2, 2) As String
    Dim Total As Integer
    Dim solved As Boolean


#End Region

    Public Function Start(ByRef Sudoku(,) As String, ByVal Method As Integer) As Integer      '1=normal      2=brute     3=random brute
        Dim x As Integer
        Dim y As Integer

        Dim result As Integer
        For x = 0 To 8
            For y = 0 To 8
                Table(x, y) = Sudoku(x, y)
            Next
        Next

        Select Case Method
            Case 1
                result = Solve()
            Case 2
                result = BruteForce(False)
            Case 3
                result = BruteForce(True)
        End Select

        For x = 0 To 8
            For y = 0 To 8
                Sudoku(x, y) = Table(x, y)
            Next
        Next
        Return result
    End Function

#Region "Logic : Human"

    Private Function Solve() As Integer
        Dim x As Integer
        Dim y As Integer

        Total = 0
        For x = 0 To 8
            For y = 0 To 8
                Complete(x, y) = ""
            Next
        Next
        For x = 0 To 2
            For y = 0 To 2
                BoxDone(x, y) = "123456789"
            Next
        Next

        solved = False

        If ErrCheck() = True Then
            Return -2           'invalid entry
        End If

        Elimination()

        If solved = False Then
            While pEliminate() = True
                Elimination()
            End While
            'tElimination()
        End If

        For x = 0 To 8
            For y = 0 To 8
                If Table(x, y) = "" Then
                    Return -1
                End If
            Next
        Next

        If solved = False Then
            Return 0
        End If

        Return 1
    End Function

    Private Function ErrCheck() As Boolean
        Dim x As Integer
        Dim y As Integer
        Dim i As Integer
        Dim j As Integer

        Dim err As Boolean
        For x = 0 To 8
            For y = 0 To 8

                If Table(x, y).ToString.Length = 1 Then     'Start elimination process only if it is a defined digit

                    Dim a As Integer = x    'x coordinate of box
                    Dim b As Integer = y    'y coordinate of box

                    a = Box_Point(a, b).X      '\get the point of the relative box
                    b = Box_Point(a, b).Y      ' \

                    'Elimination Inside Box
                    For i = 0 To 2
                        For j = 0 To 2
                            If New Drawing.Point(a + i, b + j) <> New Drawing.Point(x, y) Then      'check if its not the same box

                                If Table(a + i, b + j) = Table(x, y) Then       'if there are 2 same digits in 1 box
                                    Complete(a + i, b + j) = Table(a + i, b + j)        '\add those points to the error list
                                    Complete(x, y) = Table(x, y)                        ' \     {complete is just a name}
                                    err = True
                                End If
                            End If
                        Next
                    Next

                    'Elimination Inside Row
                    For i = 0 To 8
                        If i <> x Then   'check if its not the same box - in the x row

                            If Table(i, y) = Table(x, y) Then       'if there are 2 same digits in the row
                                Complete(i, y) = Table(i, y)        '\add those points to the error list
                                Complete(x, y) = Table(x, y)        ' \     {complete is just a name}
                                err = True
                            End If
                        End If

                        If i <> y Then   'check if its not the same box - in the y row

                            If Table(x, i) = Table(x, y) Then       'if there are 2 same digits in the row
                                Complete(x, i) = Table(x, i)        '\add those points to the error list
                                Complete(x, y) = Table(x, y)        ' \     {complete is just a name}
                                err = True
                            End If
                        End If
                    Next
                End If

            Next
        Next

        If err = True Then
            For x = 0 To 8
                For y = 0 To 8
                    Table(x, y) = Complete(x, y)
                Next
            Next
            Return True
        Else
            Return False
        End If
    End Function

    Private Function Progress(ByVal x As Integer, ByVal y As Integer, Optional ByVal value As String = "-1") As String
        If value = "-1" Then            'if the value is default then a value is being expected
            Return Complete(x, y)
        Else
            Complete(x, y) = value      'if value is not default so set the vaule
            If value <> "" Then
                Total += 1
            End If
            If Total = 81 Then          'all the numbers are entered ie if the sudoku is solved
                solved = True
            End If
        End If
        Return Nothing
    End Function

    Private Function Box_Point(ByVal x As Integer, ByVal y As Integer) As Drawing.Point        'Returns point for comparision
        x = Int(x / 3) * 3      '\the box coordinates are divisible by 3 so int is to remove the remainder
        y = Int(y / 3) * 3      ' \can also be written as i = (i-(i mod 3))/3

        Return New Drawing.Point(x, y)
    End Function

    Private Sub Elimination()
        Dim x As Integer
        Dim y As Integer
        Dim i As Integer
        Dim j As Integer
        If solved = True Then
            Exit Sub
        End If

        Dim Elimination_End As Boolean = True

        'Eliminatoin in Box and Row
        While Elimination_End = True
            Elimination_End = False
            For x = 0 To 8
                For y = 0 To 8

                    If Table(x, y).ToString.Length = 1 Then     'Start elimination process only if it is a defined digit

                        If Progress(x, y) = Nothing Then
                            Elimination_End = True

                            Progress(x, y, Table(x, y))     'add the digit to the completed list

                            If solved = True Then
                                Exit Sub
                            End If

                            Dim a As Integer = x    'x coordinate of box
                            Dim b As Integer = y    'y coordinate of box

                            a = Box_Point(a, b).X      '\get the point of the relative box
                            b = Box_Point(a, b).Y      ' \

                            'Elimination Inside Box
                            For i = 0 To 2
                                For j = 0 To 2
                                    If New Drawing.Point(a + i, b + j) <> New Drawing.Point(x, y) Then      'check if its not the same box
                                        Table(a + i, b + j) = Table(a + i, b + j).Replace(Table(x, y), "")      'replace the possibility with nothing, ""
                                    End If
                                Next
                            Next

                            'Elimination Inside Row
                            For i = 0 To 8
                                If i <> x Then   'check if its not the same box - in the x row
                                    Table(i, y) = Table(i, y).Replace(Table(x, y), "")    'replace the possibility with nothing, ""
                                End If

                                If i <> y Then   'check if its not the same box - in the y row
                                    Table(x, i) = Table(x, i).Replace(Table(x, y), "")    'replace the possibility with nothing, ""
                                End If
                            Next
                        End If

                    End If

                Next
            Next

        End While

        If solved = True Then
            Exit Sub
        End If

        Determining()
    End Sub

    Private Sub Determining()   'Determining Numbers
        Dim x As Integer
        Dim y As Integer
        Dim a As Integer
        Dim b As Integer
        Dim i As Integer
        Dim c As Integer
        If solved = True Then
            Exit Sub
        End If

        Dim determined As Boolean

        'Per Box
        For x = 0 To 2          '\For each Box
            For y = 0 To 2      ' \

                Dim box_x As Integer = x * 3    'x coordinate of box
                Dim box_y As Integer = y * 3    'y coordinate of box

                For i = 1 To 9      'for all numbers
                    Dim count As Integer = 0
                    Dim point As New Drawing.Point With {.X = -1, .Y = -1}

                    For a = 0 To 2          '\For each number in the box
                        For b = 0 To 2      ' \

                            If Table(box_x + a, box_y + b).ToString.Length <> 1 Then        'if there are possibilities in the box
                                Dim asd As String = Table(box_x + a, box_y + b).Replace(i, "")
                                If Table(box_x + a, box_y + b).Replace(i, "") <> Table(box_x + a, box_y + b) Then       'the string will change after replacement of the digit from the string
                                    count += 1          'add 1 to total number of possibilities

                                    point.X = box_x + a     '\obviously the number will be determined if the count=1
                                    point.Y = box_y + b     ' \and if count stays 1 this point will be determined
                                End If
                            End If

                        Next
                    Next

                    If count = 1 Then       'if there is only one possibility then determine

                        Table(point.X, point.Y) = i     'replace that point with the number
                        determined = True
                    End If
                Next

            Next
        Next

        'Per Row
        For c = 0 To 8

            For i = 1 To 9      'for all numbers
                Dim hor As Integer      'no of possibilities in Horizontal rows
                Dim ver As Integer      'no of possibilities in Vertical rows
                Dim point_h As Drawing.Point    'Point in horizontal rows
                Dim point_v As Drawing.Point    'Point in vertical rows

                For a = 0 To 8
                    'If Table(c, a).ToString.Length <> 1 Then
                    If Table(c, a).Replace(i.ToString, "") <> Table(c, a) Then      '\Horizontal Rows\
                        hor += 1                                                    ' \               \
                        point_h.X = c                                               '  \               \
                        point_h.Y = a                                               '   \               \
                    End If
                    'End If

                    'If Table(a, c).ToString.Length <> 1 Then
                    If Table(a, c).Replace(i.ToString, "") <> Table(a, c) Then      '\Vertical Rows\
                        ver += 1                                                    ' \             \
                        point_v.X = a                                               '  \             \
                        point_v.Y = c                                               '   \             \
                    End If
                    'End If
                Next

                If hor = 1 Then         'if there is only 1 possibility of a number in the row then determine
                    If Table(point_h.X, point_h.Y).ToString.Length > 1 Then
                        Table(point_h.X, point_h.Y) = i
                        determined = True

                    End If
                    hor = 0
                End If

                If ver = 1 Then         'if there is only 1 possibility of a number in the row then determine
                    If Table(point_v.X, point_v.Y).ToString.Length > 1 Then

                        Table(point_v.X, point_v.Y) = i
                        determined = True
                    End If
                    ver = 0
                End If
            Next

        Next

        If solved = True Then
            Exit Sub
        End If

        If determined = True Then       'if a number is determined then there can be elimination
            Elimination()
            Exit Sub
        End If

    End Sub

    Private Function pEliminate() As Boolean        'Elimination by Possibility
        Dim x As Integer
        Dim y As Integer
        Dim i As Integer
        Dim j As Integer
        Dim a As Integer
        Dim b As Integer
        Dim Eliminated As Boolean = False
        For x = 0 To 2          '\Loop for each box
            For y = 0 To 2      ' \

                Dim box_x As Integer = x * 3    'x coordinate of box
                Dim box_y As Integer = y * 3    'y coordinate of box

                For i = 1 To 9      'for all numbers

                    Dim point As New Drawing.Point With {.X = -1, .Y = -1}
                    Dim axis As String = ""      'Digit is repeated in which axis

                    If BoxDone(x, y).Replace(i, "") <> BoxDone(x, y) Then
                        For a = 0 To 2          '\Loop for numbers inside the box
                            For b = 0 To 2      ' \
                                'Dim asd As String = Table(box_x + a, box_y + b)
                                'Dim bsd As String = Table(point.X, point.Y)

                                If Table(box_x + a, box_y + b).Replace(i, "") <> Table(box_x + a, box_y + b) Then       'the string will change after replacement of the digit from the string
                                    If Table(box_x + a, box_y + b).ToString.Length <> 1 Then        'if there are possibilities in the box
                                        If point.X = -1 Then        'if this is the first possibility detected

                                            point.X = box_x + a     '\record the point
                                            point.Y = box_y + b     ' \coz either of the coordinate will be same
                                        Else


                                            If point.X = box_x + a Then     'if repeated in x row
                                                axis = axis & "y"
                                                b = 2
                                            ElseIf point.Y = box_y + b Then     'if repeated in y row
                                                axis = axis & "x"
                                            Else        'if repeated somewhere else
                                                point.X = -1
                                                GoTo ExitLoop
                                            End If

                                            If axis.Length > 1 Then         'if repeated in x & y rows both
                                                point.X = -1
                                                GoTo ExitLoop
                                            End If

                                        End If

                                    End If
                                End If
                            Next
                        Next
                    End If

ExitLoop:
                    'Elimination from possibility
                    If point.X <> -1 Then
                        BoxDone(x, y) = BoxDone(x, y).Replace(i, "")
                        For j = 0 To 8      'Numbers in a row
                            If axis = "y" Then      'x is constant and y is the j variable
                                If Box_Point(point.X, point.Y) <> Box_Point(point.X, j) Then      'if the point in the row is not of the same box
                                    If Table(point.X, j).ToString.Length <> 1 Then      'if the that point is not already determined
                                        'Dim asd As String = Table(point.X, j)
                                        'Dim psd As String = Table(point.X, j).Replace(i, "")
                                        If Table(point.X, j) <> Table(point.X, j).Replace(i, "") Then        'if elimination hasnt already been done 
                                            Table(point.X, j) = Table(point.X, j).Replace(i, "")      'replace the possibility with nothing, ""
                                            Eliminated = True       'Because there will be elimination if the condition is true
                                        End If
                                    End If
                                End If
                            End If

                            If axis = "x" Then      'y is constant and x is the j variable
                                If Box_Point(point.X, point.Y) <> Box_Point(j, point.Y) Then      'if the point in the row is not of the same box
                                    If Table(j, point.Y).ToString.Length <> 1 Then      'if the that point is not already determined

                                        If Table(j, point.Y) <> Table(j, point.Y).Replace(i, "") Then        'if elimination hasnt already been done 
                                            Table(j, point.Y) = Table(j, point.Y).Replace(i, "")      'replace the possibility with nothing, ""
                                            Eliminated = True       'Because there will be elimination if the condition is true
                                        End If
                                    End If
                                End If
                            End If

                        Next

                    End If

                Next
            Next
        Next

        tElimination()

        If Eliminated = True Then
            Return True
        End If
    End Function

    Private Sub tElimination()
        Dim Eliminated As Boolean
        Dim x As Integer
        Dim y As Integer
        Dim i As Integer
        Dim a As Integer
        Dim k As Integer

        For x = 0 To 2        '\for all 9 boxes
            For y = 0 To 2      ' \

                For i = 0 To 8      'for all 9 small boxes in the big box
                    Dim point1 As New Drawing.Point(i Mod 3, Int(i / 3))

                    If (Table(point1.X + (3 * x), point1.Y + (3 * y)).ToString.Length > 1 And _
                        Table(point1.X + (3 * x), point1.Y + (3 * y)).ToString.Length < 5) = True Then      'if the no of possibilities are 2,3,4

                        Dim point(2) As Drawing.Point
                        Dim c As String = ""
                        Dim p As String = Table(point1.X + (3 * x), point1.Y + (3 * y))
                        Dim common As Boolean
                        Dim n As Integer = 0

                        For a = 0 To 8      'for all 9 small boxes in the big box
                            c = Table((a Mod 3) + (3 * x), Int(a / 3) + (3 * y))        'c = current (small) box
                            If i <> a Then          'if it is not the same point
                                If (c.Length > 1 And c.Length < 5) = True Then      'if the other no of possibilities are 2,3,4

                                    For k = 1 To p.Length           'for each possibility
                                        If c.Replace(Mid(p, k, 1), "") <> c Then        'it will be replaced then there is a possibility in common
                                            common = True
                                            c = c.Replace(Mid(p, k, 1), "")         'remove the possibility from the string so that the extra possibility is detected
                                        End If
                                    Next



                                    If common = True Then
                                        If p.Length + c.Length < 5 Then         'if the total no of possibilities (including the extra possibilities) < 5, because telimination will happen when no of possibilities = no of boxes
                                            point(n).X = a Mod 3            '\record the box
                                            point(n).Y = Int(a / 3)         ' \
                                            n += 1
                                            p = p & c
                                        End If
                                        common = False
                                    End If

                                    If n + 1 = p.Length Then    'if no of boxes = no of possibilities | (n+1)= no of boxes
                                        Exit For
                                    End If

                                End If
                            End If
                        Next

                        If n + 1 = p.Length Then    'if no of boxes = no of possibilities | (n+1)= no of boxes

                            For a = 0 To 8      'for all (small) boxes
                                c = Table((a Mod 3) + (3 * x), Int(a / 3) + (3 * y))
                                Select Case New Drawing.Point(a Mod 3, Int(a / 3))
                                    Case point1
                                        'nothing happens
                                    Case point(0)
                                        'nothing happens
                                    Case point(1)
                                        'nothing happens
                                    Case point(2)
                                        'nothing happens
                                    Case Else
                                        For k = 1 To p.Length                           '\Eliminate all the possibilites
                                            If c <> c.Replace(Mid(p, k, 1), "") Then
                                                c = c.Replace(Mid(p, k, 1), "")
                                                Eliminated = True
                                            End If
                                        Next
                                        Table((a Mod 3) + (3 * x), Int(a / 3) + (3 * y)) = c
                                End Select
                            Next
                        End If
                    End If

                Next

            Next
        Next

        If Eliminated = True Then
            Elimination()
        End If

    End Sub

#End Region

#Region "Logic : Brute Force"

    Private Function BruteForce(ByVal Random As Boolean) As Integer
        Dim x As Integer
        Dim y As Integer
        Dim i As Integer
        Dim k As Integer

        Dim original(8, 8) As String
        Dim c As String
        Dim possibles As New List(Of Drawing.Point)

        For x = 0 To 8
            For y = 0 To 8
                If Table(x, y).ToString.Length > 1 Then
                    possibles.Add(New Drawing.Point(x, y))
                End If
                'original(x, y) = Table(x, y)
            Next
        Next

        For i = 0 To possibles.Count - 1
            c = Table(possibles(i).X, possibles(i).Y)
            If c.Length > 1 Then
                Dim l As Integer = c.Length
                For k = 1 To l

                    For x = 0 To 8
                        For y = 0 To 8
                            original(x, y) = Table(x, y)
                        Next
                    Next

                    Randomize()

                    If Random = True Then
                        Dim rand As Integer = Int((Rnd() * c.Length) + 1)
                        Dim r As Char = Mid(c, rand, 1)
                        c = c.Replace(r, "")

                        Table(possibles(i).X, possibles(i).Y) = r 'Mid(c, k, 1)
                    Else
                        Table(possibles(i).X, possibles(i).Y) = Mid(c, k, 1)
                    End If
                    Select Case Solve()
                        Case 1
                            GoTo done
                        Case 0
                            Exit For
                        Case -1
                            For x = 0 To 8
                                For y = 0 To 8
                                    Table(x, y) = original(x, y)
                                    If original(x, y).ToString.Length > 1 Then
                                        Complete(x, y) = Nothing
                                    End If
                                Next
                            Next

                    End Select
                Next
            End If
        Next

        Return -1

        Exit Function

done:
        Return 1

    End Function

#End Region

End Class