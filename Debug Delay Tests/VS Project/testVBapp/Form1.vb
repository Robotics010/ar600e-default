Public Class Form1

    ' TCP Server vars
    Dim TCPServer As New TCPServer_class_type()
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
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        TCPServer.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TCPServer.WaitForClient()

        TCPTimer.Enabled = True
    End Sub

    Private Sub TCPTimer_Tick(sender As Object, e As EventArgs) Handles TCPTimer.Tick
        ' Read and Process all TCP Messages from Client
        Do ' Read All Messages from Client
            ' Catch the IOException generated if the 
            ' read timeout.
            IsMessageReceived = TCPServer.Read()
            If IsMessageReceived Then
                Receivedmessage = TCPServer.GetReceivedMessage()
                ' Process Received Message
                Select Case Receivedmessage.header
                    Case JointType.L_shoulder_1
                        st_l_shoulder1.Text = Receivedmessage.value
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
                End Select
            End If
        Loop Until (Not IsMessageReceived) Or TCPServer.IsDisconnected()

        ' Write Joint Setpoints TO Robot
        If False Then
            ' ---Write to Robot via driver
        End If

        ' Write all Update TCP Messages to Client
        If Not TCPServer.IsDisconnected() Then
            'Read Joint State Position FROM Robot
            '---Read from Robot via driver
            l_shoulder1.Text = 1
            l_shoulder2.Text = 2
            l_elbow1.Text = 3
            l_elbow2.Text = 4
            l_forearm.Text = 5
            r_shoulder1.Text = 6
            r_shoulder2.Text = 7
            r_elbow1.Text = 8
            r_elbow2.Text = 9
            r_forearm.Text = 10

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

End Class
