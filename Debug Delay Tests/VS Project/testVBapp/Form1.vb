Public Class Form1

    ' TCP Server vars
    Dim TCPServer As New TCPServer_class_type()
    ' LOGGER USAGE
    Dim Logger As New CSVLog_class_type(CurDir() & "\vb_log.csv")

    Dim IsMessageReceived As Boolean
    Dim Receivedmessage As Message
    Dim Transmittedmessage As Message

    Dim temp_var As Integer
    Dim dbg_ind As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TCPServer.Initialization()
        Transmittedmessage.value = -5
        temp_var = 0
        dbg_ind = 0

        l_shoulder1.Text = -1000
        l_shoulder2.Text = -2000
        l_elbow1.Text = -3000
        l_elbow2.Text = -4000
        l_forearm.Text = -5000
        r_shoulder1.Text = -6000
        r_shoulder2.Text = -7000
        r_elbow1.Text = -8000
        r_elbow2.Text = -9000
        r_forearm.Text = -10000
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' LOGGER USAGE
        ' Logger.Log("Form1.vb", "Form1_FormClosing", "test message 2")

        TCPServer.Close()
        ' LOGGER USAGE
        Logger.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TCPServer.WaitForClient()

        TCPTimer.Enabled = True
    End Sub

    Private Sub TCPTimer_Tick(sender As Object, e As EventArgs) Handles TCPTimer.Tick
        ' Read and Process all TCP Messages from Client
        For index As Integer = 1 To 10
            'Do ' Read All Messages from Client
            ' Catch the IOException generated if the 
            ' read timeout.
            IsMessageReceived = TCPServer.Read()
            If IsMessageReceived Then
                Receivedmessage = TCPServer.GetReceivedMessage()
                ' Process Received Message
                Select Case Receivedmessage.header
                    Case JointType.L_shoulder_1
                        Logger.Log("Form1.vb", "Form1_Load", "receive L_shoulder_1 setpoint=" + CStr(Receivedmessage.value))
                        st_l_shoulder1.Text = Receivedmessage.value
                    Case JointType.L_shoulder_2
                        Logger.Log("Form1.vb", "Form1_Load", "receive L_shoulder_2 setpoint=" + CStr(Receivedmessage.value))
                        st_l_shoulder2.Text = Receivedmessage.value
                    Case JointType.L_elbow_1
                        Logger.Log("Form1.vb", "Form1_Load", "receive L_elbow_1 setpoint=" + CStr(Receivedmessage.value))
                        st_l_elbow1.Text = Receivedmessage.value
                    Case JointType.L_elbow_2
                        Logger.Log("Form1.vb", "Form1_Load", "receive L_elbow_2 setpoint=" + CStr(Receivedmessage.value))
                        st_l_elbow2.Text = Receivedmessage.value
                    Case JointType.L_forearm
                        Logger.Log("Form1.vb", "Form1_Load", "receive L_forearm setpoint=" + CStr(Receivedmessage.value))
                        st_l_forearm.Text = Receivedmessage.value
                    Case JointType.R_shoulder_1
                        Logger.Log("Form1.vb", "Form1_Load", "receive R_shoulder_1 setpoint=" + CStr(Receivedmessage.value))
                        st_r_shoulder1.Text = Receivedmessage.value
                    Case JointType.R_shoulder_2
                        Logger.Log("Form1.vb", "Form1_Load", "receive R_shoulder_2 setpoint=" + CStr(Receivedmessage.value))
                        st_r_shoulder2.Text = Receivedmessage.value
                    Case JointType.R_elbow_1
                        Logger.Log("Form1.vb", "Form1_Load", "receive R_elbow_1 setpoint=" + CStr(Receivedmessage.value))
                        st_r_elbow1.Text = Receivedmessage.value
                    Case JointType.R_elbow_2
                        Logger.Log("Form1.vb", "Form1_Load", "receive R_elbow_2 setpoint=" + CStr(Receivedmessage.value))
                        st_r_elbow2.Text = Receivedmessage.value
                    Case JointType.R_forearm
                        Logger.Log("Form1.vb", "Form1_Load", "receive R_forearm setpoint=" + CStr(Receivedmessage.value))
                        st_r_forearm.Text = Receivedmessage.value
                    Case Else
                End Select
            End If
            'Loop Until (Not IsMessageReceived) Or TCPServer.IsDisconnected()
        Next

        ' Write Joint Setpoints TO Robot
        If False Then
            ' ---Write to Robot via driver
        End If

        ' Write all Update TCP Messages to Client
        If Not TCPServer.IsDisconnected() Then
            'Read Joint State Position FROM Robot
            ' CInt CStr
            '---Read from Robot via driver
            l_shoulder1.Text = CInt(l_shoulder1.Text) - 1
            l_shoulder2.Text = CInt(l_shoulder2.Text) - 1
            l_elbow1.Text = CInt(l_elbow1.Text) - 1
            l_elbow2.Text = CInt(l_elbow2.Text) - 1
            l_forearm.Text = CInt(l_forearm.Text) - 1
            r_shoulder1.Text = CInt(r_shoulder1.Text) - 1
            r_shoulder2.Text = CInt(r_shoulder2.Text) - 1
            r_elbow1.Text = CInt(r_elbow1.Text) - 1
            r_elbow2.Text = CInt(r_elbow2.Text) - 1
            r_forearm.Text = CInt(r_forearm.Text) - 1

            ' Write all Messages to Client
            Transmittedmessage.header = JointType.L_shoulder_1
            For index As Integer = 1 To 10
                Select Case Transmittedmessage.header
                    Case JointType.L_shoulder_1
                        Transmittedmessage.value = l_shoulder1.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send L_shoulder_1 position=" + CStr(Transmittedmessage.value))
                    Case JointType.L_shoulder_2
                        Transmittedmessage.value = l_shoulder2.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send L_shoulder2 position=" + CStr(Transmittedmessage.value))
                    Case JointType.L_elbow_1
                        Transmittedmessage.value = l_elbow1.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send L_elbow1 position=" + CStr(Transmittedmessage.value))
                    Case JointType.L_elbow_2
                        Transmittedmessage.value = l_elbow2.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send L_elbow2 position=" + CStr(Transmittedmessage.value))
                    Case JointType.L_forearm
                        Transmittedmessage.value = l_forearm.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send L_forearm position=" + CStr(Transmittedmessage.value))
                    Case JointType.R_shoulder_1
                        Transmittedmessage.value = r_shoulder1.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send R_shoulder1 position=" + CStr(Transmittedmessage.value))
                    Case JointType.R_shoulder_2
                        Transmittedmessage.value = r_shoulder2.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send R_shoulder2 position=" + CStr(Transmittedmessage.value))
                    Case JointType.R_elbow_1
                        Transmittedmessage.value = r_elbow1.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send R_elbow1 position=" + CStr(Transmittedmessage.value))
                    Case JointType.R_elbow_2
                        Transmittedmessage.value = r_elbow2.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send R_elbow2 position=" + CStr(Transmittedmessage.value))
                    Case JointType.R_forearm
                        Transmittedmessage.value = r_forearm.Text
                        Logger.Log("Form1.vb", "Form1_Load", "send R_forearm position=" + CStr(Transmittedmessage.value))
                    Case Else
                        'Debug.WriteLine("Not between 1 and 10, inclusive")
                End Select

                TCPServer.SetTransmittedMessage(Transmittedmessage)
                TCPServer.Write()
                Transmittedmessage.header = Transmittedmessage.header + 1
            Next
            'temp_var = temp_var + 1
            'System.Threading.Thread.Sleep(100)
        Else
            TCPTimer.Enabled = False
            Me.Close()
        End If


    End Sub

End Class
