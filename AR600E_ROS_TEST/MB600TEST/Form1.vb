Imports System.Threading.Thread
Imports System.Threading
Imports System.IO
Imports System.Runtime.InteropServices

Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.VisualBasic


Public Class Form1
    Public MB As New MB600E.MB
    Dim ini As New iniFile.IniFile
    Dim temp_var As Integer
    Dim dbg_ind As Integer

    ' TCP Server vars
    Dim TCPServer As New TCPServer_class_type()
    Dim IsMessageReceived As Boolean
    Dim Receivedmessage As Message
    Dim Transmittedmessage As Message


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        TCPServer.Close()
        MB.CLOSE()
    End Sub

    <DllImport("ntdll.dll", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Sub NtSetTimerResolution(DesiredResolution As UInteger, SetResolution As Boolean, ByRef CurrentResolution As UIntPtr)
    End Sub

    Public arrNomb_joint(71) As Integer
    Dim Nomb_buff(71) As Integer
    Dim Nomb_Revers_Bool(71) As Boolean
    Public Nomb_Revers(71) As Integer
    Dim c_type_list(5) As Int16
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TCPServer.Initialization()
        Transmittedmessage.value = -5
        temp_var = 0
        dbg_ind = 0

        Dim c As UIntPtr = 0
        NtSetTimerResolution(10000, True, c)

        For i As Integer = 0 To 70
            Nomb_Revers(i) = 1
            Nomb_Revers_Bool(i) = False
        Next




        ini.Load("config.ini")
        Dim m As Integer = 1
        Dim nomber, buf_adr, s_name, maxpos, minpos, s_torqe, s_ilim, s_stif, s_dump As String

        Dim c_type As String
        Dim s_Rev As Boolean
        Try
            ' While ini.GetKeyValue("NOMB_MOTOR", m) <> ""
            For m = 1 To 50
                If ini.GetKeyValue("DEVICE_NOMB", m) <> "" Then
                    'While ini.GetKeyValue("DEVICE_NOMB", m) <> ""
                    'MessageBox.Show(ini.GetKeyValue("NOMB_MOTOR", m))
                    nomber = ini.GetKeyValue("DEVICE_NOMB", m)
                    c_type = ini.GetKeyValue("DEVICE_TYPE", m)
                    buf_adr = ini.GetKeyValue("DEVICE_NOMB_BUFFER", m)
                    s_name = ini.GetKeyValue("DEVICE_NAME", m)
                    s_torqe = ini.GetKeyValue("TORQE_MOTOR", m)
                    s_ilim = ini.GetKeyValue("ILIM_MOTOR", m) 'Center Pos
                    s_stif = ini.GetKeyValue("STIF_MOTOR", m)
                    s_dump = ini.GetKeyValue("DUMP_MOTOR", m)
                    maxpos = ini.GetKeyValue("MAXPOS_MOTOR", m)
                    minpos = ini.GetKeyValue("MINPOS_MOTOR", m)


                    If ini.GetKeyValue("REVERS_MOTOR", m) <> "" Then
                        s_Rev = ini.GetKeyValue("REVERS_MOTOR", m)
                        If s_Rev Then
                            Nomb_Revers(nomber) = -1
                            Nomb_Revers_Bool(nomber) = True
                        End If
                    End If


                    arrNomb_joint(nomber) = buf_adr
                    MB.arrNomb_joint(buf_adr) = nomber
                    If c_type = 0 Then
                        Dim row As String() = New String() {nomber, buf_adr, s_name, 0, s_torqe, s_ilim, s_stif, s_dump, minpos, maxpos, s_Rev, c_type}
                        DataGridView1.Rows.Add(row)

                    ElseIf c_type = 1 Then
                        c_type_list(1) = nomber
                        
                        Dim row As String() = New String() {nomber, buf_adr, s_name, 0, 0, 0, 0, 0, 0, 0, c_type}
                        DataGridView2.Rows.Add(row)
                    ElseIf c_type = 2 Then
                        c_type_list(2) = nomber
                        Dim UCH0, UCH1, UCH2, UCH3 As Int16
                        UCH0 = ini.GetKeyValue("FORCE_SENSOR_LEFT_FOOT", "UCH0")
                        UCH1 = ini.GetKeyValue("FORCE_SENSOR_LEFT_FOOT", "UCH1")
                        UCH2 = ini.GetKeyValue("FORCE_SENSOR_LEFT_FOOT", "UCH2")
                        UCH3 = ini.GetKeyValue("FORCE_SENSOR_LEFT_FOOT", "UCH3")
                        Dim row As String() = New String() {nomber, buf_adr, s_name, 0, 0, 0, UCH0, UCH1, UCH2, UCH3, c_type}
                        DataGridView2.Rows.Add(row)

                    ElseIf c_type = 3 Then
                        c_type_list(3) = nomber
                        Dim UCH0, UCH1, UCH2, UCH3 As Int16
                        UCH0 = ini.GetKeyValue("FORCE_SENSOR_RIGHT_FOOT", "UCH0")
                        UCH1 = ini.GetKeyValue("FORCE_SENSOR_RIGHT_FOOT", "UCH1")
                        UCH2 = ini.GetKeyValue("FORCE_SENSOR_RIGHT_FOOT", "UCH2")
                        UCH3 = ini.GetKeyValue("FORCE_SENSOR_RIGHT_FOOT", "UCH3")
                        Dim row As String() = New String() {nomber, buf_adr, s_name, 0, 0, 0, UCH0, UCH1, UCH2, UCH3, c_type}
                        DataGridView2.Rows.Add(row)
                    End If

                    'm += 1
                    ' End While
                End If
            Next
            Dim btn As New DataGridViewButtonColumn()
            DataGridView1.Columns.Add(btn)
            With btn
                .HeaderText = "Save"
                .Text = "Save"
                .Name = "Col_btn"
                .UseColumnTextForButtonValue = True
            End With

            Dim btn2 As New DataGridViewButtonColumn()
            DataGridView2.Columns.Add(btn2)
            With btn2
                .HeaderText = "Save"
                .Text = "Save"
                .Name = "Col_btn"
                .UseColumnTextForButtonValue = True
            End With

        Catch ex As Exception
            Sleep(10)
        End Try
        Sleep(100)
        Update_information()
        Sleep(100)

        'MB.ON61()
        'MB.ON62()
        MB.ON12()
        Sleep(50)


        MB.START()
        Sleep(59)
        tbPOSMIN.Text = MB.POSMIN(NOMB.Value)
        tbPOSMAX.Text = MB.POSMAX(NOMB.Value)
        TbDAMP.Text = MB.DAMP(NOMB.Value)
        tbSTIFF.Text = MB.STIFF(NOMB.Value)
        ' tbILIM.Text = MB.ILIM(NOMB.Value)
        lblIMOT.Text = MB.IMOT(NOMB.Value)
        lblUBATT.Text = MB.UBATT(NOMB.Value)
        Timer1.Enabled = True

    End Sub




    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Label26.Text = TrackBar1.Value


        Label2.Text = (MB.U(0) / 1000).ToString("F02")
        Label5.Text = (MB.U(1) / 1000).ToString("F02")
        Label9.Text = (MB.U(2) / 1000).ToString("F02")
        Label13.Text = (MB.U(3) / 1000).ToString("F02")
        Label17.Text = (MB.U(4) / 1000).ToString("F02")
        Label21.Text = (MB.U(5) / 100).ToString("F02")

        Label3.Text = (MB.I(0) / 1000).ToString("F02")
        Label8.Text = (MB.I(1) / 1000).ToString("F02")
        Label12.Text = (MB.I(2) / 1000).ToString("F02")
        Label16.Text = (MB.I(3) / 1000).ToString("F02")
        Label20.Text = (MB.I(4) / 1000).ToString("F02")
        Label24.Text = (MB.I(5) / 100).ToString("F02")
        'lblCPOS.Text = MB.CPOS(NOMB.Value)
        'lblUBATT.Text = (MB.UBATT(NOMB.Value) / 100).ToString("F01")


        '  DataGridView2.Rows
        'Dim m_stat As Byte = MB.MOT_STAT(NOMB.Value)


        'If Nomb_Revers(NOMB.Value) Then
        '    lblCPOS.Text = -MB.CPOS(Nom_Joint)
        'Else
        lblCPOS.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NOMB.Value)
        'End If
        lblUBATT.Text = (MB.UBATT(Nom_Joint) / 100).ToString("F01")



        Dim m_stat As Byte = MB.MOT_STAT(Nom_Joint)

        Label25.Text = m_stat

        'Sleep(1)
        If (m_stat And 0) = 0 Then
            'stop
            l_BRK.BackColor = Color.White
        Else
            l_BRK.BackColor = Color.Gray
        End If

        If (m_stat And 1) = 1 Then
            'stop
            l_DT.BackColor = Color.White
        Else
            l_DT.BackColor = Color.Gray
        End If

        If (m_stat And 2) = 2 Then
            'stop
            l_RELAX.BackColor = Color.White
            l_BRK.BackColor = Color.Gray
        Else
            l_RELAX.BackColor = Color.Gray

        End If

        If (m_stat And 3) = 3 Then
            'stop
            l_TRACE.BackColor = Color.White
            l_BRK.BackColor = Color.Gray
            l_DT.BackColor = Color.Gray
            l_RELAX.BackColor = Color.Gray

        Else
            l_TRACE.BackColor = Color.Gray
        End If


        If (m_stat And 16) = 16 Then
            'stop
            tbPOSMIN.BackColor = Color.Red
        Else
            tbPOSMIN.BackColor = Color.White
        End If
        If (m_stat And 32) = 32 Then
            'stop
            tbPOSMAX.BackColor = Color.Red
        Else
            tbPOSMAX.BackColor = Color.White
        End If

        If (m_stat And 128) = 128 Then
            'stop
            l_REV.BackColor = Color.Red
        Else
            l_REV.BackColor = Color.White
        End If
        
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MB.ON61()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MB.OFF61()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MB.ON62()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MB.OFF62()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        MB.ON81()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MB.OFF81()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        MB.ON82()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        MB.OFF82()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        MB.ON12()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        MB.OFF12()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        MB.ON48()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        MB.OFF48()
    End Sub

    Private Sub tbPOSMIN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbPOSMIN.KeyDown
        If e.KeyCode = Keys.Enter Then
            MB.POSMIN(Nom_Joint) = tbPOSMIN.Text
            Sleep(100)
            tbPOSMIN.Text = MB.POSMIN(Nom_Joint)
        End If
    End Sub

    Private Sub tbPOSMAX_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbPOSMAX.KeyDown
        If e.KeyCode = Keys.Enter Then
            MB.POSMAX(Nom_Joint) = tbPOSMAX.Text
            Sleep(100)
            tbPOSMAX.Text = MB.POSMAX(Nom_Joint)
        End If
    End Sub
    Private Sub tbSTIFF_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbSTIFF.KeyDown
        If e.KeyCode = Keys.Enter Then
            MB.STIFF(Nom_Joint) = tbSTIFF.Text
            Sleep(100)
            tbSTIFF.Text = MB.STIFF(Nom_Joint)
        End If
    End Sub

    Private Sub tbDAMP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TbDAMP.KeyDown
        If e.KeyCode = Keys.Enter Then
            MB.DAMP(Nom_Joint) = TbDAMP.Text
            Sleep(100)
            TbDAMP.Text = MB.DAMP(Nom_Joint)
        End If
    End Sub
    'Private Sub tbILIM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        MB.ILIM(Nom_Joint) = tbILIM.Text
    '        Sleep(100)
    '        '   tbILIM.Text = MB.ILIM(NOMB.Value)
    '    End If
    'End Sub
    Dim Nom_Joint As Integer = 1
    Private Sub NOMB_ValueChanged(sender As Object, e As EventArgs) Handles NOMB.ValueChanged

        Nom_Joint = arrNomb_joint(NOMB.Value)
        Debug.Print(String.Format("{0}) NOMB.ValueChanged Event: Nom_Joint = arrNomb_joint(NOMB.Value)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}, NOMB.Value = {2}, arrNomb_joint(NOMB.Value) = {3}", dbg_ind, Nom_Joint, NOMB.Value, arrNomb_joint(NOMB.Value)))
        dbg_ind += 1
        tbPOSMIN.Text = MB.POSMIN(Nom_Joint)
        tbPOSMAX.Text = MB.POSMAX(Nom_Joint)
        TbDAMP.Text = MB.DAMP(Nom_Joint)
        tbSTIFF.Text = MB.STIFF(Nom_Joint)
        ' tbILIM.Text = MB.ILIM(NOMB.Value)
        lblIMOT.Text = MB.IMOT(Nom_Joint)
        lblUBATT.Text = (MB.UBATT(Nom_Joint) / 100).ToString("F01")


        'tbPOSMIN.Text = MB.POSMIN(NOMB.Value)
        'tbPOSMAX.Text = MB.POSMAX(NOMB.Value)
        'TbDAMP.Text = MB.DAMP(NOMB.Value)
        'tbSTIFF.Text = MB.STIFF(NOMB.Value)
        '' tbILIM.Text = MB.ILIM(NOMB.Value)
        'lblIMOT.Text = MB.IMOT(NOMB.Value)
        'lblUBATT.Text = (MB.UBATT(NOMB.Value) / 100).ToString("F01")
    End Sub



    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim row As String() = New String() {1, 0, 0, 1, 1, 1, 200, 1, -500, 500, False, 0}
        DataGridView1.Rows.Add(row)

    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 12 Then
            'ini.Load("config2.ini")
            Dim ind As Integer = DataGridView1.Rows.Item(e.RowIndex).Cells(0).Value 'e.RowIndex + 1

            ini.AddSection("DEVICE_NOMB").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(0).Value
            ini.AddSection("DEVICE_NOMB_BUFFER").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(1).Value
            ini.AddSection("DEVICE_NAME").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(2).Value
            ini.AddSection("TORQE_MOTOR").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(4).Value
            ini.AddSection("ILIM_MOTOR").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(5).Value
            ini.AddSection("STIF_MOTOR").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(6).Value
            ini.AddSection("DUMP_MOTOR").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(7).Value
            ini.AddSection("MINPOS_MOTOR").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(8).Value
            ini.AddSection("MAXPOS_MOTOR").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(9).Value
            ini.AddSection("REVERS_MOTOR").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(10).Value
            ini.AddSection("DEVICE_TYPE").AddKey(ind).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(11).Value

            'MsgBox(("Row : " + e.RowIndex.ToString & "  Col : ") + e.ColumnIndex.ToString)
            'ini.AddSection("DEVICE_NOMB").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(0).Value
            'ini.AddSection("DEVICE_NOMB_BUFFER").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(1).Value
            'ini.AddSection("DEVICE_NAME").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(2).Value
            'ini.AddSection("TORQE_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(4).Value
            'ini.AddSection("ILIM_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(5).Value
            'ini.AddSection("STIF_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(6).Value
            'ini.AddSection("DUMP_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(7).Value
            'ini.AddSection("MINPOS_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(8).Value
            'ini.AddSection("MAXPOS_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(9).Value
            'ini.AddSection("REVERS_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(10).Value

            ini.Save("config.ini")
            'ini.Save("config2.ini")
            'ini.Save("test.ini")
            ini.Save("test2.ini")
        End If

    End Sub


    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Update_information()
    End Sub
    Private Sub Update_information()
        Dim ind As Integer = 0
        Dim mot_nom As Integer
        Dim buff_adr As Integer
        Dim posmin, posmax As Integer
        Try
            While DataGridView1.Rows.Item(ind).Cells(0).Value <> ""
                mot_nom = DataGridView1.Rows.Item(ind).Cells(0).Value
                buff_adr = DataGridView1.Rows.Item(ind).Cells(1).Value
                MB.MOT_STOP_BR(buff_adr)
                'ini.AddSection("TORQE_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(3).Value
                'ini.AddSection("ILIM_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(4).Value
                'ini.AddSection("STIF_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(5).Value
                'ini.AddSection("DUMP_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(6).Value
                'ini.AddSection("MINPOS_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(7).Value
                'ini.AddSection("MAXPOS_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(8).Value
                MB.adress_update(mot_nom, buff_adr)
                MB.ILIM(buff_adr) = DataGridView1.Rows.Item(ind).Cells(5).Value
                MB.STIFF(buff_adr) = DataGridView1.Rows.Item(ind).Cells(6).Value
                MB.DAMP(buff_adr) = DataGridView1.Rows.Item(ind).Cells(7).Value

                If DataGridView1.Rows.Item(ind).Cells(10).Value = False Then
                    MB.ArPosmin(mot_nom) = DataGridView1.Rows.Item(ind).Cells(8).Value - 30
                    MB.ArPosmax(mot_nom) = DataGridView1.Rows.Item(ind).Cells(9).Value + 30

                    MB.POSMIN(buff_adr) = DataGridView1.Rows.Item(ind).Cells(8).Value
                    MB.POSMAX(buff_adr) = DataGridView1.Rows.Item(ind).Cells(9).Value
                Else
                    posmin = DataGridView1.Rows.Item(ind).Cells(8).Value
                    posmax = DataGridView1.Rows.Item(ind).Cells(9).Value
                    MB.ArPosmin(mot_nom) = -posmax + 30
                    MB.ArPosmax(mot_nom) = -posmin - 30

                    MB.POSMIN(buff_adr) = -DataGridView1.Rows.Item(ind).Cells(9).Value
                    MB.POSMAX(buff_adr) = -DataGridView1.Rows.Item(ind).Cells(8).Value
                End If
                MB.MOT_STOP_BR(buff_adr)
                'MB.ILIM(mot_nom) = DataGridView1.Rows.Item(ind).Cells(5).Value
                'MB.STIFF(mot_nom) = DataGridView1.Rows.Item(ind).Cells(6).Value
                'MB.DAMP(mot_nom) = DataGridView1.Rows.Item(ind).Cells(7).Value
                'MB.POSMIN(mot_nom) = DataGridView1.Rows.Item(ind).Cells(8).Value
                'MB.POSMAX(mot_nom) = DataGridView1.Rows.Item(ind).Cells(9).Value
                ind += 1
            End While

            ind = 0
            While DataGridView2.Rows.Item(ind).Cells(0).Value <> ""
                mot_nom = DataGridView2.Rows.Item(ind).Cells(0).Value
                buff_adr = DataGridView2.Rows.Item(ind).Cells(1).Value
                ' MB.MOT_STOP_BR(buff_adr)
                'ini.AddSection("TORQE_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(3).Value
                'ini.AddSection("ILIM_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(4).Value
                'ini.AddSection("STIF_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(5).Value
                'ini.AddSection("DUMP_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(6).Value
                'ini.AddSection("MINPOS_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(7).Value
                'ini.AddSection("MAXPOS_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(ind).Cells(8).Value
                MB.adress_update(mot_nom, buff_adr)
                'MB.ILIM(buff_adr) = DataGridView1.Rows.Item(ind).Cells(5).Value
                'MB.STIFF(buff_adr) = DataGridView1.Rows.Item(ind).Cells(6).Value
                'MB.DAMP(buff_adr) = DataGridView1.Rows.Item(ind).Cells(7).Value

                'If DataGridView1.Rows.Item(ind).Cells(10).Value = False Then
                '    MB.ArPosmin(mot_nom) = DataGridView1.Rows.Item(ind).Cells(8).Value - 30
                '    MB.ArPosmax(mot_nom) = DataGridView1.Rows.Item(ind).Cells(9).Value + 30

                '    MB.POSMIN(buff_adr) = DataGridView1.Rows.Item(ind).Cells(8).Value
                '    MB.POSMAX(buff_adr) = DataGridView1.Rows.Item(ind).Cells(9).Value
                'Else
                '    posmin = DataGridView1.Rows.Item(ind).Cells(8).Value
                '    posmax = DataGridView1.Rows.Item(ind).Cells(9).Value
                '    MB.ArPosmin(mot_nom) = -posmax + 30
                '    MB.ArPosmax(mot_nom) = -posmin - 30

                '    MB.POSMIN(buff_adr) = -DataGridView1.Rows.Item(ind).Cells(9).Value
                '    MB.POSMAX(buff_adr) = -DataGridView1.Rows.Item(ind).Cells(8).Value
                'End If
                'MB.MOT_STOP_BR(buff_adr)
                'MB.ILIM(mot_nom) = DataGridView1.Rows.Item(ind).Cells(5).Value
                'MB.STIFF(mot_nom) = DataGridView1.Rows.Item(ind).Cells(6).Value
                'MB.DAMP(mot_nom) = DataGridView1.Rows.Item(ind).Cells(7).Value
                'MB.POSMIN(mot_nom) = DataGridView1.Rows.Item(ind).Cells(8).Value
                'MB.POSMAX(mot_nom) = DataGridView1.Rows.Item(ind).Cells(9).Value
                ind += 1
            End While
        Catch ex As Exception
            Sleep(10)
        End Try
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        'If Nomb_Revers(NOMB.Value) Then
        '    MB.ANGLE(Nom_Joint) = -TrackBar1.Value
        'Else
        '    MB.ANGLE(Nom_Joint) = TrackBar1.Value
        'End If
        Dim pos As Integer = TrackBar1.Value * Nomb_Revers(NOMB.Value)
        Debug.Print(String.Format("{0}) TrackBar1.Scroll Event: Dim pos As Integer = TrackBar1.Value * Nomb_Revers(NOMB.Value)", dbg_ind))
        Debug.Print(String.Format("{0}) NOMB.Value = {1}, Nomb_Revers(NOMB.Value) = {2}, TrackBar1.Value ={3}, pos = {4}", dbg_ind, NOMB.Value, Nomb_Revers(NOMB.Value), TrackBar1.Value, pos))
        MB.ANGLE(Nom_Joint) = pos
        Debug.Print(String.Format("{0}) MB.ANGLE(Nom_Joint) = pos", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}, pos = {2}", dbg_ind, Nom_Joint, pos))
        dbg_ind += 1
    End Sub

    Private Sub bSTOP_Click(sender As Object, e As EventArgs) Handles bSTOP.Click
        ' MB.MOT_STOP(NOMB.Value)
        MB.MOT_STOP_BR(Nom_Joint)
    End Sub

    Private Sub bBRAKE_Click(sender As Object, e As EventArgs) Handles bBRAKE.Click
        'MB.MOT_STOP_BR(NOMB.Value)
        MB.MOT_STOP(Nom_Joint)
    End Sub

    Private Sub bRELAX_Click(sender As Object, e As EventArgs) Handles bRELAX.Click
        MB.MOT_RELAX(Nom_Joint)
    End Sub

    Private Sub bTRACE_Click(sender As Object, e As EventArgs) Handles bTRACE.Click

        MB.ANGLE(Nom_Joint) = MB.CPOS(Nom_Joint)
        Debug.Print(String.Format("{0}) bTRACE.Click Event: MB.ANGLE(Nom_Joint) = MB.CPOS(Nom_Joint)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}, MB.CPOS(Nom_Joint) = {2}", dbg_ind, Nom_Joint, MB.CPOS(Nom_Joint)))
        'If Nomb_Revers(NOMB.Value) Then
        '    TrackBar1.Value = -MB.CPOS(Nom_Joint)
        'Else
        '    'MB.ANGLE(Nom_Joint) = MB.CPOS(Nom_Joint)
        TrackBar1.Value = MB.CPOS(Nom_Joint) * Nomb_Revers(NOMB.Value)
        Debug.Print(String.Format("{0}) TrackBar1.Value = MB.CPOS(Nom_Joint) * Nomb_Revers(NOMB.Value)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}, MB.CPOS(Nom_Joint) = {2}, NOMB.Value = {3}, Nomb_Revers(NOMB.Value) = {4}, TrackBar1.Value = {5}", dbg_ind, Nom_Joint, MB.CPOS(Nom_Joint), NOMB.Value, Nomb_Revers(NOMB.Value), TrackBar1.Value))
        'End If
        MB.MOT_TRACE(Nom_Joint)
        Debug.Print(String.Format("{0}) MB.MOT_TRACE(Nom_Joint)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}", dbg_ind, Nom_Joint))
        dbg_ind += 1
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Dim ind As Integer = 0
        Dim mot_nom As Integer = 0

        ' DataGridView1.Rows.Item(Nom_Joint - 1).Cells(5).Value = MB.CPOS(Nom_Joint)
        While DataGridView1.Rows.Item(ind).Cells(0).Value <> ""
            mot_nom = DataGridView1.Rows.Item(ind).Cells(1).Value
            If mot_nom = Nom_Joint Then
                DataGridView1.Rows.Item(ind).Cells(5).Value = 0
                Sleep(50)
                Update_information()
                Sleep(50)
                DataGridView1.Rows.Item(ind).Cells(5).Value = MB.CPOS(Nom_Joint)
            End If
            ind += 1
        End While
        Sleep(50)
        Update_information()
        Sleep(50)
        '   MB.MOT_CENTER(Nom_Joint)
    End Sub
    Dim cnt_RX As Integer = 0
    Dim cnt_TX As Integer = 0


    Private Sub Button15_Click(sender As Object, e As EventArgs)
        MB.ANGLE(1) = 0
        MB.ANGLE(2) = 0
        'TrackBar1.Value = MB.CPOS(NOMB.Value)
        MB.MOT_TRACE(1)
        MB.MOT_TRACE(2)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs)
        MB.MOT_STOP_BR(1)
        MB.MOT_STOP_BR(2)
    End Sub


    Private Sub bREVON_Click(sender As Object, e As EventArgs) Handles bREVON.Click
        MB.MOT_SET_REVERS(Nom_Joint)
    End Sub

    Private Sub bREVOFF_Click(sender As Object, e As EventArgs) Handles bREVOFF.Click
        MB.MOT_CLR_REVERS(Nom_Joint)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        MessageBox.Show(DataGridView1.Rows.Item(0).Cells(10).Value)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs)
        'Dim d As Integer = 0
        'If (d > -10) And (d < 10) Then
        '    MessageBox.Show("ok")
        'End If
        For i As Integer = 1 To 12
            MB.MOT_RELAX(i)
        Next
    End Sub

    Dim ptrF As IntPtr
    Dim ptrF2 As IntPtr
    Dim ptrG As IntPtr
    Dim arr_graph(40) As IntPtr


    Private m_Rnd As New Random
    Public Function RandomRGBColor() As Color
        Return Color.FromArgb(255, _
            m_Rnd.Next(0, 255), _
            m_Rnd.Next(0, 255), _
            m_Rnd.Next(0, 255))
    End Function
    Public Function Interpol(arr() As Integer, new_lenght As Integer) As Integer()
        Dim i, j, L As Integer
        Dim scale, x As Double

        L = arr.Length
        If L >= new_lenght Then
            Return arr
        End If

        Dim new_arr(new_lenght) As Integer
        scale = (L - 1) / (new_lenght - 1)

        new_arr(0) = arr(0)
        For i = 1 To new_lenght - 2
            x = x + scale
            j = Math.Truncate(x)
            new_arr(i) = arr(j) + ((arr(j + 1) - arr(j)) * (x - j))
        Next
        new_arr(new_lenght - 1) = arr(L - 1)
        Return new_arr

    End Function

    Private Sub Button20_Click(sender As Object, e As EventArgs)
        For i As Integer = 1 To 40
            MB.MOT_STOP_BR(i)
        Next
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs)
        Dim xLen As UInt16 = TextBox1.Text
        'ptrF = CreateForm("Hello, World!", xLen, fp)
        'ptrF = graph.CreateForm("Hello, World!", xLen)
        'graph.ShowForm(ptrF)
        ''sendThread = New Thread(AddressOf MOT_GO)
        'sendThread = New Thread(AddressOf motion_play_new_2)
        'sendThread.Start()

    End Sub
    Dim sendThread As Threading.Thread
    Private Sub MOT_GO(nom As Integer, tm As Integer, pos As Integer)

        Dim new_arr() As Integer
        Dim val_test() As Integer = {MB.CPOS(arrNomb_joint(nom)), pos}


        new_arr = Interpol(val_test, tm * 10)

        MB.ANGLE(arrNomb_joint(nom)) = new_arr(0)
        MB.MOT_TRACE(arrNomb_joint(nom))

        For k As Integer = 0 To new_arr.Length - 2
            MB.ANGLE(arrNomb_joint(nom)) = new_arr(k)
            Sleep(5)
        Next

        Sleep(150)
        'MB.MOT_STOP_BR(Nom_Joint)
        'result = BitConverter.ToInt32(bData, 0)
        ' Dim delay As Integer
        'Sleep(10)
    End Sub
    ' Private Sub MOT_GO(nomb As Integer, tm As Integer, pos As Integer)
    Private Sub MOT_GO_Thread()

        Dim new_arr() As Integer
        Dim val_test() As Integer = {MB.CPOS(Nom_Joint), TextBox2.Text}


        new_arr = Interpol(val_test, TextBox1.Text)

        MB.ANGLE(Nom_Joint) = new_arr(0)
        MB.MOT_TRACE(Nom_Joint)

        For k As Integer = 0 To new_arr.Length - 2
            MB.ANGLE(Nom_Joint) = new_arr(k)
            Sleep(5)
        Next

        Sleep(150)
        'MB.MOT_STOP_BR(Nom_Joint)
        'result = BitConverter.ToInt32(bData, 0)
        ' Dim delay As Integer
        'Sleep(10)
    End Sub
    Private Sub MOT_GO_track()

        Dim new_arr() As Integer
        Dim val_test() As Integer = {MB.CPOS(Nom_Joint), TextBox2.Text}

        new_arr = Interpol(val_test, TextBox1.Text * 10)

        MB.ANGLE(Nom_Joint) = new_arr(0)
        MB.MOT_TRACE(Nom_Joint)

        For k As Integer = 0 To new_arr.Length - 2
           MB.ANGLE(Nom_Joint) = new_arr(k)
            Sleep(5)
        Next

        Sleep(150)
        'MB.MOT_STOP_BR(Nom_Joint)
        'result = BitConverter.ToInt32(bData, 0)
        ' Dim delay As Integer
        'Sleep(10)
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        'group_setVal(1, TextBox1.Text, TextBox2.Text, TextBox2.Text)
        'group_setVal(2, TextBox1.Text, TextBox2.Text, TextBox2.Text)
        'Motion_start()
    End Sub

    Private Sub standard_leg()
        group_setVal(1, 1500, 0, 0, 0, 0, 0, 0)
        group_setVal(2, 1500, 0, 0, 0, 0, 0, 0)
        Motion_start()
    End Sub
    Private Sub standard_hand()
        group_setVal(3, 2000, 0, 0, 0, 0, 0)
        group_setVal(4, 2000, 0, 0, 0, 0, 0)
        Motion_start()
    End Sub
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        standard_leg()
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim pos As Integer = 500
        group_setVal(1, 1000, pos, pos, pos, pos, pos, pos)
        group_setVal(2, 1000, pos, pos, pos, pos, pos, pos)
        Motion_start()
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        standard_leg()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Dim pos As Integer = -500
        group_setVal(1, 1000, pos, pos, pos, pos)
        group_setVal(2, 1000, pos, pos, pos, pos)
        Motion_start()
    End Sub



    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        group_setVal(1, 2000, , 3000, -6000, 3500, )
        group_setVal(2, 2000, , 3000, -6000, 3500, )
        Motion_start()
        Sleep(2000)
        standard_leg()
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        standard_hand()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim pos As Integer = 1000
        group_setVal(3, 1000, pos, pos, pos, pos, pos)
        group_setVal(4, 1000, pos, pos, pos, pos, pos)

        pos = 0
        group_setVal(3, 1000, pos, pos, pos, pos, pos)
        group_setVal(4, 1000, pos, pos, pos, pos, pos)
        Motion_start()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim pos As Integer = -500
        group_setVal(3, 1000, pos, pos, pos, 0, pos)
        group_setVal(4, 1000, pos, pos, pos, 0, pos)
        Motion_start()
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        'Dim tim As Integer = 1500
        ''ROBOT.GROUP_TIME(group_setVal(3, tim, tim, tim, tim, tim))
        ''ROBOT.GROUP_TIME(group_setVal(7, tim, tim, tim))
        ''Sleep(10)
        'group_setVal(3, tim, 0, 1900, 900, 300, 3400)
        ''ROBOT.GROUP_GO(group_setVal(3, 0, 0, 0, 0, 0))
        ''ROBOT.GROUP_GO(group_setVal(7, 0))
        'Motion_start()
        present_P2()
    End Sub

    Private Sub present_P2()
        'standard_Hands()
        'Sleep(2400)
        Dim tim As Int16 = 1100
        Dim pause As Integer = 1800
        'ROBOT.GROUP_TIME(group_setVal(3, tim, tim, tim, tim, tim))
        'ROBOT.GROUP_TIME(group_setVal(4, tim, tim, tim, tim, tim))
        group_setVal(3, tim, 0, 1100, 0, 0, 7600)
        group_setVal(4, tim, 0, 1100, 0, 0, 7600)
        ' Motion_start()

        group_setVal(3, tim, , 5000, , , 10000)
        group_setVal(4, tim, , 5000, , , 10000)
        ' Motion_start()


     
        group_setVal(3, tim, , 10000, , , 4800)
        group_setVal(4, tim, , 10000, , , 4800)
        ' Motion_start()
     

        'присел

        'tim = 180
        'ROBOT.GROUP_TIME(group_setVal(1, tim, tim, tim, tim - 20, tim))
        'ROBOT.GROUP_TIME(group_setVal(2, tim, tim, tim, tim - 20, tim))
        'Sleep(10)
        'ROBOT.GROUP_TPOS(group_setVal(1, 4000, 0, -8000, 4600, 0))
        'ROBOT.GROUP_TPOS(group_setVal(2, 4000, 0, -8000, 4600, 0))
        'ROBOT.GROUP_GO(group_setVal(1, 0, 0, 0, 0, ))
        'ROBOT.GROUP_GO(group_setVal(2, 0, 0, 0, 0, ))


        'ROBOT.GROUP_TIME(group_setVal(3, tim, tim, tim, tim, tim))
        'ROBOT.GROUP_TIME(group_setVal(4, tim, tim, tim, tim, tim))
        'ROBOT.GROUP_TPOS(group_setVal(3, 0, 7500, -4000, 2500, 800))
        'ROBOT.GROUP_TPOS(group_setVal(4, 0, 7500, -4000, 2500, 800))
        'ROBOT.GROUP_GO(group_setVal(3, 0, 0, 0, 0, 0))
        'ROBOT.GROUP_GO(group_setVal(4, 0, 0, 0, 0, 0))
        'fist_left()
        'fist_right()
        'Sleep(2500)

        'руки всторону
  
        ' Sleep(400)
        tim = 1200
        'ROBOT.GROUP_TIME(group_setVal(3, tim, tim, tim, tim, tim))
        'ROBOT.GROUP_TIME(group_setVal(4, tim, tim, tim, tim, tim))
        group_setVal(3, tim, 0, -500, 0, 7000, 1500)
        group_setVal(4, tim, 0, -500, 0, 7000, 1500)
        Motion_start()
        'ROBOT.GROUP_GO(group_setVal(3, 0, 0, 0, 0, 0))
        'ROBOT.GROUP_GO(group_setVal(4, 0, 0, 0, 0, 0))
        Sleep(1000)

        standard_hand()
        Sleep(3500)
        'STOP_Hands()
        'Legs_STOP()
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs)
        Dim pos As Integer = 500
        group_setVal(1, 1000, pos)
        ' group_setVal(4, 1000, pos, pos, pos, pos, pos)

        pos = -500
        group_setVal(1, 1000, pos)
        'group_setVal(4, 1000, pos, pos, pos, pos, pos)

        pos = 0
        group_setVal(1, 1000, pos)
        Motion_start()
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        balance_left_leg()
    End Sub
    Private Sub balance_left_leg()
        Dim tim As Integer = 1500
        'ROBOT.GROUP_TIME(group_setVal(1, tim, tim, tim, tim, tim))
        'ROBOT.GROUP_TIME(group_setVal(2, tim, tim, tim, tim, tim))
        'Sleep(10)
        group_setVal(2, tim, -950, , , , 1050)
        group_setVal(1, tim, 1000, , , , -950)
        'ROBOT.GROUP_GO(group_setVal(1, , 0, , , 0))
        'ROBOT.GROUP_GO(group_setVal(2, , 0, , , 0))
        Motion_start()
        Sleep(500)

        'ROBOT.GROUP_TPOS(group_setVal(2, 2000, , -4000, 2000, ))
        'ROBOT.GROUP_GO(group_setVal(2, 0, , 0, 0, ))
        group_setVal(2, tim, , 2000, -4000, 2000, )
        Motion_start()
        Sleep(1500)


        tim = 1200

        'руки
        'ROBOT.GROUP_TPOS(group_setVal(3, 0, 1500, 0, 6000, 0))
        'ROBOT.GROUP_TPOS(group_setVal(4, 0, 1500, 0, 6000, 0))
        group_setVal(3, tim, 0, 1500, 0, 6000, 0)
        group_setVal(4, tim, 0, 1500, 0, 6000, 0)
        Motion_start()
        'Sleep(500)

        tim = 800
        'ROBOT.GROUP_TPOS(group_setVal(3, 0, 1500, 0, 6700, 0))
        'ROBOT.GROUP_TPOS(group_setVal(4, 0, 1500, 0, 5300, 0))
        group_setVal(3, tim, 0, 1500, 0, 6800, 0)
        group_setVal(4, tim, 0, 1500, 0, 5100, 0)
        'Motion_start()
        'Sleep(450)

        'ROBOT.GROUP_TPOS(group_setVal(4, 0, 1500, 0, 6700, 0))
        'ROBOT.GROUP_TPOS(group_setVal(3, 0, 1500, 0, 5300, 0))
        group_setVal(4, tim, 0, 1500, 0, 6800, 0)
        group_setVal(3, tim, 0, 1500, 0, 5100, 0)
        'Motion_start()
        'Sleep(450)

        'ROBOT.GROUP_TPOS(group_setVal(3, 0, 1500, 0, 6700, 0))
        'ROBOT.GROUP_TPOS(group_setVal(4, 0, 1500, 0, 5300, 0))
        group_setVal(3, tim, 0, 1500, 0, 6800, 0)
        group_setVal(4, tim, 0, 1500, 0, 5100, 0)
        'Motion_start()
        'Sleep(450)

        'ROBOT.GROUP_TPOS(group_setVal(4, 0, 1500, 0, 6700, 0))
        'ROBOT.GROUP_TPOS(group_setVal(3, 0, 1500, 0, 5300, 0))
        group_setVal(4, tim, 0, 1500, 0, 6800, 0)
        group_setVal(3, tim, 0, 1500, 0, 5100, 0)
        'Motion_start()
        'Sleep(450)

        'ROBOT.GROUP_TPOS(group_setVal(3, 0, 1500, 0, 6000, 0))
        'ROBOT.GROUP_TPOS(group_setVal(4, 0, 1500, 0, 6000, 0))
        group_setVal(3, tim, 0, 1500, 0, 6000, 0)
        group_setVal(4, tim, 0, 1500, 0, 6000, 0)
        Motion_start()
        Sleep(200)


        tim = 1200
        'Sleep(2800)
        'ROBOT.GROUP_TPOS(group_setVal(2, 100, , -200, 100, ))
        'ROBOT.GROUP_GO(group_setVal(2, 0, , 0, 0, ))
        group_setVal(3, tim, 0, 0, 0, 0, 0)
        group_setVal(4, tim, 0, 0, 0, 0, 0)
        group_setVal(2, tim, , 150, -300, 150, )
        Motion_start()
        Sleep(100)
        'standard_hand()
        'Sleep(1500)

        tim = 1800
        'ROBOT.GROUP_TIME(group_setVal(1, tim, tim, tim, tim, tim))
        'ROBOT.GROUP_TIME(group_setVal(2, tim, tim, tim, tim, tim))
        group_setVal(1, tim, 0, 0, 0, 0, 0)
        group_setVal(2, tim, 0, 0, 0, 0, 0)
        Motion_start()

        Sleep(2000)
        'Legs_STOP()
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.ColumnIndex = 11 Then

            ' ini.Load("config2.ini")
            Dim ind As Integer = DataGridView2.Rows.Item(e.RowIndex).Cells(0).Value
            'MsgBox(("Row : " + e.RowIndex.ToString & "  Col : ") + e.ColumnIndex.ToString)

            ini.AddSection("DEVICE_NOMB").AddKey(ind).Value = DataGridView2.Rows.Item(e.RowIndex).Cells(0).Value
            ini.AddSection("DEVICE_NOMB_BUFFER").AddKey(ind).Value = DataGridView2.Rows.Item(e.RowIndex).Cells(1).Value
            ini.AddSection("DEVICE_NAME").AddKey(ind).Value = DataGridView2.Rows.Item(e.RowIndex).Cells(2).Value
            ini.AddSection("DEVICE_TYPE").AddKey(ind).Value = DataGridView2.Rows.Item(e.RowIndex).Cells(10).Value
            'ini.AddSection("TORQE_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(4).Value
            'ini.AddSection("ILIM_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(5).Value
            If c_type_list(2) = ind Then
                ini.AddSection("FORCE_SENSOR_LEFT_FOOT").AddKey("UCH0").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(6).Value
                ini.AddSection("FORCE_SENSOR_LEFT_FOOT").AddKey("UCH1").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(7).Value
                ini.AddSection("FORCE_SENSOR_LEFT_FOOT").AddKey("UCH2").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(8).Value
                ini.AddSection("FORCE_SENSOR_LEFT_FOOT").AddKey("UCH3").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(9).Value
            ElseIf c_type_list(3) = ind Then
                'ini.AddSection("DEVICE_NOMB").AddKey(ind).Value = DataGridView2.Rows.Item(e.RowIndex).Cells(0).Value
                'ini.AddSection("DEVICE_NOMB_BUFFER").AddKey(ind).Value = DataGridView2.Rows.Item(e.RowIndex).Cells(1).Value
                'ini.AddSection("DEVICE_NAME").AddKey(ind).Value = DataGridView2.Rows.Item(e.RowIndex).Cells(2).Value
                'ini.AddSection("TORQE_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(4).Value
                'ini.AddSection("ILIM_MOTOR").AddKey(e.RowIndex + 1).Value = DataGridView1.Rows.Item(e.RowIndex).Cells(5).Value
                ini.AddSection("FORCE_SENSOR_RIGHT_FOOT").AddKey("UCH0").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(6).Value
                ini.AddSection("FORCE_SENSOR_RIGHT_FOOT").AddKey("UCH1").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(7).Value
                ini.AddSection("FORCE_SENSOR_RIGHT_FOOT").AddKey("UCH2").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(8).Value
                ini.AddSection("FORCE_SENSOR_RIGHT_FOOT").AddKey("UCH3").Value = DataGridView2.Rows.Item(e.RowIndex).Cells(9).Value
            End If
            ini.Save("config.ini")
            ''ini.Save("test.ini")
            'ini.Save("test2.ini")
        End If
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        Dim row As String() = New String() {1, 0, "Name", 0, 0, 0, 0, 0, 0, 0}
        DataGridView2.Rows.Add(row)
    End Sub



  

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        'MessageBox.Show(ini.GetKeyValue("NOMB_MOTOR", m))
        Dim m As String = NumericUpDown1.Value.ToString
        ini.RemoveKey("DEVICE_NOMB", m)
        ini.RemoveKey("DEVICE_TYPE", m)
        ini.RemoveKey("DEVICE_NOMB_BUFFER", m)
        ini.RemoveKey("DEVICE_NAME", m)
        ini.RemoveKey("TORQE_MOTOR", m)
        ini.RemoveKey("ILIM_MOTOR", m) 'Center Pos
        ini.RemoveKey("STIF_MOTOR", m)
        ini.RemoveKey("DUMP_MOTOR", m)
        ini.RemoveKey("MAXPOS_MOTOR", m)
        ini.RemoveKey("MINPOS_MOTOR", m)
        ini.RemoveKey("DEVICE_TYPE", m)
        ini.Save("config.ini")
    End Sub

  
    Private Sub Button15_Click_1(sender As Object, e As EventArgs) Handles Button15.Click
        For i As Integer = 1 To 40
            MB.MOT_STOP_BR(i)
        Next
    End Sub

    Private Sub TCPTimer_Tick(sender As Object, e As EventArgs) Handles TCPTimer.Tick
        ' Read and Process all TCP Messages from Client
        Do ' Read All Messages from Client
            ' Catch the IOException generated if the 
            ' read timeout.
            IsMessageReceived = TCPServer.Read()
            If IsMessageReceived Then
                Receivedmessage = TCPServer.GetReceivedMessage()
                tb_header.Text = Receivedmessage.header
                tb_value.Text = Receivedmessage.value
                ' Process Received Message
                Select Receivedmessage.header
                    Case JointType.L_shoulder_1
                        st_l_shoulder1.Text = Receivedmessage.value
                        Debug.Print(String.Format("{0}) Receivedmessage TCPTimer.Tick Event: st_l_shoulder1.Text = Receivedmessage.value", dbg_ind))
                        Debug.Print(String.Format("{0}) Receivedmessage.value = {1}, st_l_shoulder1.Text = {2}", dbg_ind, Receivedmessage.value, st_l_shoulder1.Text))
                        dbg_ind += 1
                    Case JointType.L_shoulder_2
                        st_l_shoulder2.Text = Receivedmessage.value
                    Case JointType.L_elbow_1
                        st_l_elbow1.Text = Receivedmessage.value
                    Case JointType.L_elbow_2
                        st_l_elbow2.Text = Receivedmessage.value
                    Case JointType.L_forearm
                        st_l_forearm.Text = Receivedmessage.value
                    Case JointType.R_shoulder_1
                        st_r_shoulder1.Text = Receivedmessage.value
                    Case JointType.R_shoulder_2
                        st_r_shoulder2.Text = Receivedmessage.value
                    Case JointType.R_elbow_1
                        st_r_elbow1.Text = Receivedmessage.value
                    Case JointType.R_elbow_2
                        st_r_elbow2.Text = Receivedmessage.value
                    Case JointType.R_forearm
                        st_r_forearm.Text = Receivedmessage.value
                    Case Else
                        'Debug.WriteLine("Not between 1 and 10, inclusive")
                End Select
            End If
        Loop Until (Not IsMessageReceived) Or TCPServer.IsDisconnected()

        ' Write Joint Setpoints TO Robot
        'chbx_SendSetpoint.Checked
        Dim NombValue As Integer
        If chbx_SendSetpoint.Checked Then

            'NombValue = 36 ' l_shoulder1
            'Nom_Joint = arrNomb_joint(NombValue)
            'Debug.Print(String.Format("{0}) Write Joint Setpoints TO Robot TCPTimer.Tick Event: Nom_Joint = arrNomb_joint(NombValue)", dbg_ind))
            'Debug.Print(String.Format("{0}) NombValue = {1},arrNomb_joint(NombValue) = {2}, Nom_Joint = {3}", dbg_ind, NombValue, arrNomb_joint(NombValue), Nom_Joint))
            'MB.ANGLE(Nom_Joint) = st_l_shoulder1.Text
            'Debug.Print(String.Format("{0}) MB.ANGLE(Nom_Joint) = st_l_shoulder1.Text", dbg_ind))
            'Debug.Print(String.Format("{0}) Nom_Joint = {1}, st_l_shoulder1.Text = {2}", dbg_ind, Nom_Joint, st_l_shoulder1.Text))
            'dbg_ind += 1

            NOMB.Value = 36
            Nom_Joint = arrNomb_joint(NOMB.Value)
            Dim pos As Integer = Convert.ToInt32(st_l_shoulder1.Text) * Nomb_Revers(NOMB.Value)
            Debug.Print(String.Format("{0}) Write Joint Setpoints TO Robot TCPTimer.Tick Event: Dim pos As Integer = Convert.ToInt32(st_l_shoulder1.Text) * Nomb_Revers(NOMB.Value)", dbg_ind))
            Debug.Print(String.Format("{0}) NOMB.Value = {1}, Nomb_Revers(NOMB.Value) = {2}, Convert.ToInt32(st_l_shoulder1.Text) ={3}, pos = {4}", dbg_ind, NOMB.Value, Nomb_Revers(NOMB.Value), Convert.ToInt32(st_l_shoulder1.Text), pos))
            MB.ANGLE(Nom_Joint) = pos
            Debug.Print(String.Format("{0}) MB.ANGLE(Nom_Joint) = pos", dbg_ind))
            Debug.Print(String.Format("{0}) Nom_Joint = {1}, pos = {2}", dbg_ind, Nom_Joint, pos))
            dbg_ind += 1

            'NombValue = 35 ' l_shoulder2
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_l_shoulder2.Text
            'NombValue = 34 ' l_elbow1
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_l_elbow1.Text
            'NombValue = 33 ' l_elbow2
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_l_elbow2.Text
            'NombValue = 37 ' l_forearm
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_l_forearm.Text

            'NombValue = 20 ' r_shoulder1
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_r_shoulder1.Text
            'NombValue = 19 ' r_shoulder2
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_r_shoulder2.Text
            'NombValue = 18 ' r_elbow1
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_r_elbow1.Text
            'NombValue = 17 ' r_elbow2
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_r_elbow2.Text
            'NombValue = 21 ' r_forearm
            'Nom_Joint = arrNomb_joint(NombValue)
            'MB.ANGLE(Nom_Joint) = st_r_forearm.Text

        End If

        ' Write all Update TCP Messages to Client
        If Not TCPServer.IsDisconnected() Then
            'Read Joint State Position FROM Robot
            'Dim NombValue As Integer

            NombValue = 36 ' l_shoulder1
            Nom_Joint = arrNomb_joint(NombValue)
            l_shoulder1.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'l_shoulder1.Text = 1
            NombValue = 35 ' l_shoulder2
            Nom_Joint = arrNomb_joint(NombValue)
            l_shoulder2.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'l_shoulder2.Text = 2
            NombValue = 34 ' l_elbow1
            Nom_Joint = arrNomb_joint(NombValue)
            l_elbow1.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'l_elbow1.Text = 3
            NombValue = 33 ' l_elbow2
            Nom_Joint = arrNomb_joint(NombValue)
            l_elbow2.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'l_elbow2.Text = 4
            NombValue = 37 ' l_forearm
            Nom_Joint = arrNomb_joint(NombValue)
            l_forearm.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'l_forearm.Text = 5

            NombValue = 20 ' r_shoulder1
            Nom_Joint = arrNomb_joint(NombValue)
            r_shoulder1.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'r_shoulder1.Text = 6
            NombValue = 19 ' r_shoulder2
            Nom_Joint = arrNomb_joint(NombValue)
            r_shoulder2.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'r_shoulder2.Text = 7
            NombValue = 18 ' r_elbow1
            Nom_Joint = arrNomb_joint(NombValue)
            r_elbow1.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'r_elbow1.Text = 8
            NombValue = 17 ' r_elbow2
            Nom_Joint = arrNomb_joint(NombValue)
            r_elbow2.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'r_elbow2.Text = 9
            NombValue = 21 ' r_forearm
            Nom_Joint = arrNomb_joint(NombValue)
            r_forearm.Text = MB.CPOS(Nom_Joint) * Nomb_Revers(NombValue)
            'r_forearm.Text = 10

            ' Write all Messages to Client
            Transmittedmessage.header = JointType.L_shoulder_1
            For index As Integer = 1 To 10
                Select Case Transmittedmessage.header
                    Case JointType.L_shoulder_1
                        Transmittedmessage.value = l_shoulder1.Text
                    Case JointType.L_shoulder_2
                        Transmittedmessage.value = l_shoulder2.Text
                    Case JointType.L_elbow_1
                        Transmittedmessage.value = l_elbow1.Text
                    Case JointType.L_elbow_2
                        Transmittedmessage.value = l_elbow2.Text
                    Case JointType.L_forearm
                        Transmittedmessage.value = l_forearm.Text
                    Case JointType.R_shoulder_1
                        Transmittedmessage.value = r_shoulder1.Text
                    Case JointType.R_shoulder_2
                        Transmittedmessage.value = r_shoulder2.Text
                    Case JointType.R_elbow_1
                        Transmittedmessage.value = r_elbow1.Text
                    Case JointType.R_elbow_2
                        Transmittedmessage.value = r_elbow2.Text
                    Case JointType.R_forearm
                        Transmittedmessage.value = r_forearm.Text
                    Case Else
                        'Debug.WriteLine("Not between 1 and 10, inclusive")
                End Select

                TCPServer.SetTransmittedMessage(Transmittedmessage)
                TCPServer.Write()
                Transmittedmessage.header = Transmittedmessage.header + 1
            Next
            'temp_var = temp_var + 1
            'System.Threading.Thread.Sleep(100)
        End If

    End Sub

    Private Sub btn_TCP_Start_Click(sender As Object, e As EventArgs) Handles btn_TCP_Start.Click
        NOMB.Value = 36
        Nom_Joint = arrNomb_joint(NOMB.Value)
        Debug.Print(String.Format("{0}) btn_TCP_Start.Click Event: Nom_Joint = arrNomb_joint(NOMB.Value)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}, NOMB.Value = {2}, arrNomb_joint(NOMB.Value) = {3}", dbg_ind, Nom_Joint, NOMB.Value, arrNomb_joint(NOMB.Value)))
        tbPOSMIN.Text = MB.POSMIN(Nom_Joint)
        tbPOSMAX.Text = MB.POSMAX(Nom_Joint)
        TbDAMP.Text = MB.DAMP(Nom_Joint)
        tbSTIFF.Text = MB.STIFF(Nom_Joint)
        ' tbILIM.Text = MB.ILIM(NOMB.Value)
        lblIMOT.Text = MB.IMOT(Nom_Joint)
        lblUBATT.Text = (MB.UBATT(Nom_Joint) / 100).ToString("F01")

        MB.ANGLE(Nom_Joint) = MB.CPOS(Nom_Joint)
        Debug.Print(String.Format("{0}) MB.ANGLE(Nom_Joint) = MB.CPOS(Nom_Joint)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}, MB.CPOS(Nom_Joint) = {2}", dbg_ind, Nom_Joint, MB.CPOS(Nom_Joint)))
        TrackBar1.Value = MB.CPOS(Nom_Joint) * Nomb_Revers(NOMB.Value)
        Debug.Print(String.Format("{0}) TrackBar1.Value = MB.CPOS(Nom_Joint) * Nomb_Revers(NOMB.Value)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}, MB.CPOS(Nom_Joint) = {2}, NOMB.Value = {3}, Nomb_Revers(NOMB.Value) = {4}, TrackBar1.Value = {5}", dbg_ind, Nom_Joint, MB.CPOS(Nom_Joint), NOMB.Value, Nomb_Revers(NOMB.Value), TrackBar1.Value))
        MB.MOT_TRACE(Nom_Joint)
        Debug.Print(String.Format("{0}) MB.MOT_TRACE(Nom_Joint)", dbg_ind))
        Debug.Print(String.Format("{0}) Nom_Joint = {1}", dbg_ind, Nom_Joint))
        dbg_ind += 1
        ' Block execution and wait for TCP Client
        TCPServer.WaitForClient()

        TCPTimer.Enabled = True
    End Sub

    Private Sub btn_TCP_Stop_Click(sender As Object, e As EventArgs) Handles btn_TCP_Stop.Click
        TCPTimer.Enabled = False
    End Sub

    Private Sub lblCPOS_Click(sender As Object, e As EventArgs) Handles lblCPOS.Click

    End Sub

    Private Sub l_shoulder1_TextChanged(sender As Object, e As EventArgs) Handles l_shoulder1.TextChanged

    End Sub

    Private Sub chbx_SendSetpoint_CheckedChanged(sender As Object, e As EventArgs) Handles chbx_SendSetpoint.CheckedChanged

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub
End Class
