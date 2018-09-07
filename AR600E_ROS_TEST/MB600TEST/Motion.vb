Imports System.Threading.Thread
Module Motion
    Dim scan_wait As Boolean = True
    Dim group(10, 10) As Int16
    Dim wait_m(0 To 63, 0) As Integer
    Dim count_m As Integer = 0

    Dim group1() As Integer = {1, 2, 3, 4, 5, 6} 'левая ноа
    Dim group2() As Integer = {7, 8, 9, 10, 11, 12} '{44, 45, 40, 47, 34, 52} 'правая нога

    Dim group3() As Integer = {21, 17, 18, 19, 20} 'правая рука
    Dim group4() As Integer = {37, 33, 34, 35, 36} 'левая рука

    Dim group5() As Integer = {22, 23, 24, 25, 26} 'правая кисть
    Dim group6() As Integer = {38, 39, 40, 41, 42} 'левая кисть
    Dim group7() As Integer = {44, 45, 46} 'голова

    Dim Motor_Pos(50, 100001) As Integer
    Dim Motor_Go(50) As Boolean
    Dim count_Point(50) As Integer
    Dim count_PointEnd(50) As Integer
    Public Function group_setVal(ByVal group_nom As Integer, ByVal tm As Integer, Optional ByRef motor1 As Int16 = 1, Optional ByVal motor2 As Int16 = 1, _
                                 Optional ByVal motor3 As Int16 = 1, Optional ByVal motor4 As Int16 = 1, Optional ByVal motor5 As Int16 = 1, _
                                 Optional ByVal motor6 As Int16 = 1, Optional ByVal motor7 As Int16 = 1, Optional ByVal motor8 As Int16 = 1, _
                             Optional ByVal motor9 As Int16 = 1, Optional ByVal motor10 As Int16 = 1) As Int16()

        ' Dim gr As Integer
        Dim gr_2 As Integer = 0
        Dim count_m As Integer = 0
        Dim wrByte(47) As Int16
        'Dim m As String
        'For gr = 0 To 4

        group(group_nom, 0) = motor1
        group(group_nom, 1) = motor2
        group(group_nom, 2) = motor3
        group(group_nom, 3) = motor4
        group(group_nom, 4) = motor5
        group(group_nom, 5) = motor6
        group(group_nom, 6) = motor7
        group(group_nom, 7) = motor8
        group(group_nom, 8) = motor9
        group(group_nom, 9) = motor10

        Dim stepMOT As Integer = 0
        Dim RetSTR As String = ""

        If group_nom = 1 Then
            For stepMOT = 0 To 9
                If group(group_nom, stepMOT) <> 1 Then

                    Motor_Go(group1(stepMOT)) = True
                    Update_Pos(group1(stepMOT), tm, group(group_nom, stepMOT))
                   
                Else
                End If
            Next
        ElseIf group_nom = 2 Then
            For stepMOT = 0 To 5
                If group(group_nom, stepMOT) <> 1 Then
                    Motor_Go(group2(stepMOT)) = True
                    Update_Pos(group2(stepMOT), tm, group(group_nom, stepMOT))
                Else
                    'RetSTR = RetSTR + "XXXX"
                End If
            Next
        ElseIf group_nom = 3 Then
            For stepMOT = 0 To 4
                If group(group_nom, stepMOT) <> 1 Then
                    Motor_Go(group3(stepMOT)) = True
                    Update_Pos(group3(stepMOT), tm, group(group_nom, stepMOT))
                Else
                    'RetSTR = RetSTR + "XXXX"
                End If
            Next
        ElseIf group_nom = 4 Then
            For stepMOT = 0 To 4
                If group(group_nom, stepMOT) <> 1 Then
                    Motor_Go(group4(stepMOT)) = True
                    Update_Pos(group4(stepMOT), tm, group(group_nom, stepMOT))
                Else
                    'RetSTR = RetSTR + "XXXX"
                End If
            Next
        ElseIf group_nom = 5 Then
            For stepMOT = 0 To 4
                If group(group_nom, stepMOT) <> 1 Then
                    wrByte(group5(stepMOT) - 1) = group(group_nom, stepMOT)
                Else
                    'RetSTR = RetSTR + "XXXX"
                End If
            Next
        ElseIf group_nom = 6 Then
            For stepMOT = 0 To 4
                If group(group_nom, stepMOT) <> 1 Then
                    wrByte(group6(stepMOT) - 1) = group(group_nom, stepMOT)
                Else
                    'RetSTR = RetSTR + "XXXX"
                End If
            Next
        ElseIf group_nom = 7 Then
            For stepMOT = 0 To 2
                If group(group_nom, stepMOT) <> 1 Then
                    wrByte(group7(stepMOT) - 1) = group(group_nom, stepMOT)
                    'RetSTR = RetSTR.Remove((group3(stepMOT) - 1) * 4, 4)
                    'RetSTR = RetSTR.Insert((group3(stepMOT) - 1) * 4, group(group_nom, stepMOT).ToString("X4"))
                Else
                    'RetSTR = RetSTR + "XXXX"
                End If
            Next
        End If
        Return wrByte
    End Function

    Private Sub Update_Pos(nom As Integer, tm As Integer, pos As Integer)

        Dim new_arr() As Integer
        'Dim val_test() As Integer = {Form1.MB.CPOS(Form1.arrNomb_joint(nom)) * Form1.Nomb_Revers(nom), pos}
        Dim val_test(1) As Integer
        If count_Point(nom) = 0 Then
            val_test(0) = Form1.MB.CPOS(Form1.arrNomb_joint(nom)) * Form1.Nomb_Revers(nom)
            val_test(1) = pos
        Else
            val_test(0) = Motor_Pos(nom, count_Point(nom))
            val_test(1) = pos
        End If

        new_arr = Form1.Interpol(val_test, tm / 2)
        For k As Integer = 0 To new_arr.Length - 2
            Motor_Pos(nom, count_Point(nom) + k) = new_arr(k)
        Next
        count_Point(nom) += new_arr.Length - 2
    End Sub

    Public Sub Motion_start()
        Dim fStat As Boolean = True
        For i As Integer = 0 To 49
            If Motor_Go(i) = True Then
                Form1.MB.ANGLE(Form1.arrNomb_joint(i)) = Motor_Pos(i, 0) * Form1.Nomb_Revers(i)
                Form1.MB.MOT_TRACE(Form1.arrNomb_joint(i))
            End If
            count_PointEnd(i) = 0

        Next


        While fStat
            fStat = False
            For i As Integer = 0 To 49
                If count_PointEnd(i) < count_Point(i) Then
                    If Motor_Go(i) = True Then
                        Form1.MB.ANGLE(Form1.arrNomb_joint(i)) = Motor_Pos(i, count_PointEnd(i)) * Form1.Nomb_Revers(i)
                    End If
                    count_PointEnd(i) += 1
                    fStat = True
                Else
                    Motor_Go(i) = False
                End If
            Next
            Sleep(2)
        End While

        For i As Integer = 0 To 49
            For j As Integer = 0 To 100000
                Motor_Pos(i, j) = 0
            Next
            Motor_Go(i) = False
            count_Point(i) = 0
        Next
    End Sub
End Module
