Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Imports System.IO

Module TCPServer_class

    Public Const MSG_SIZE As Integer = 8

    Enum JointType
        L_shoulder_1
        L_shoulder_2
        L_elbow_1
        L_elbow_2
        L_forearm
        R_shoulder_1
        R_shoulder_2
        R_elbow_1
        R_elbow_2
        R_forearm
        disconnect
    End Enum
    'Dim size As JointType
    'joint_type = JointType.L_elbow_1

    Public Structure Message
        Public header As JointType
        Public value As Short
    End Structure



    Public Class TCPServer_class_type
        Private server As TcpListener
        Private port As Int32
        Private localAddr As IPAddress
        Private client As TcpClient
        ' Get a stream object for reading and writing
        Private stream As NetworkStream

        Private i As Int32
        Private Tmessage As Message
        Private flatten_Tmessage() As Byte
        Private Rmessage As Message

        ' Buffer for reading data
        Dim bytes(MSG_SIZE - 1) As Byte
        'Dim data As String = Nothing

        Dim Disconnect As Boolean

        Public Sub New()
            server = Nothing
            port = 27016
            localAddr = IPAddress.Parse("127.0.0.1")
            server = New TcpListener(localAddr, port)
            'data = Nothing

            Tmessage.header = JointType.L_shoulder_1
            Tmessage.value = -50
            Rmessage.header = JointType.L_shoulder_1
            Rmessage.value = Int16.MinValue

            Disconnect = True

        End Sub

        Public Sub Initialization()
            ' Start listening for client requests.
            server.Start()
            Console.Write("TCPServer:Waiting for a connection... ")

        End Sub
        Public Sub WaitForClient()
            ' Perform a blocking call to accept requests.
            ' You could also user server.AcceptSocket() here.
            client = server.AcceptTcpClient()
            Console.WriteLine("TCPServer:Connected!")
            Disconnect = False
            stream = client.GetStream()
            stream.ReadTimeout = 1
            stream.WriteTimeout = 1
        End Sub

        Private Function SerializeMessageObject(ByVal msg As Message) As Byte()
            Dim StrtoBy() As Byte
            Dim Size As Integer
            Size = Marshal.SizeOf(msg)
            ReDim StrtoBy(Size - 1)
            Dim header_byte As Byte = msg.header
            Dim value_byteArray(1) As Byte
            value_byteArray = BitConverter.GetBytes(msg.value)
            StrtoBy(0) = header_byte
            StrtoBy(4) = value_byteArray(0)
            StrtoBy(5) = value_byteArray(1)
            Return StrtoBy
        End Function

        Private Function DeserializeMessageObject(ByVal bytes() As Byte) As Message
            Dim unflatten_msg As Message
            unflatten_msg.header = bytes(0)
            Dim value_byteArray(1) As Byte
            value_byteArray(0) = bytes(4)
            value_byteArray(1) = bytes(5)
            unflatten_msg.value = BitConverter.ToInt16(value_byteArray, 0)
            Return unflatten_msg
        End Function

        Public Function Read() As Boolean
            Dim Result As Boolean
            Try
                i = stream.Read(bytes, 0, MSG_SIZE)
            Catch ex As IOException
                i = 0
                Console.WriteLine("TCPServer:IOException in Read")
                Result = False
            End Try
            If (i <> 0) Then
                Console.WriteLine("TCPServer:Read True")
                Rmessage = DeserializeMessageObject(bytes)
                Result = True
            End If

            'check if disconnected
            If Rmessage.header = JointType.disconnect Then
                Disconnect = True
            End If
            Return Result
        End Function

        Public Function Write() As Boolean
            Dim Result As Boolean
            flatten_Tmessage = SerializeMessageObject(Tmessage)

            Try
                stream.Write(flatten_Tmessage, 0, MSG_SIZE)
                Console.WriteLine("TCPServer:Write")
                Result = True
            Catch ex As Exception
                Result = False
                Console.WriteLine("TCPServer:Write Failed")
                Disconnect = False
            End Try

            Return Result
        End Function

        Public Sub EndWithClient()
            ' Shutdown and end connection
            Console.WriteLine("TCPServer:EndWithClient")
            client.Close()
        End Sub

        Public Sub Close()
            Console.WriteLine("TCPServer:Close")
            server.Stop()
        End Sub

        Public Function GetReceivedMessage() As Message
            Return Rmessage
        End Function

        Public Sub SetTransmittedMessage(ByVal TransmittedMessage As Message)
            Tmessage = TransmittedMessage
        End Sub

        Public Function IsDisconnected() As Boolean
            Return Disconnect
        End Function
    End Class

End Module
