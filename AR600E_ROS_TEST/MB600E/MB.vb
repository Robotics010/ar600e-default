Imports System.Net.Sockets
Imports System.Net
Imports System.Text
Imports System.Threading.Thread
Imports System.Threading
Imports System.IO
Imports System
Imports Microsoft.VisualBasic
Public Class MB
    Dim WRBUFFER(1471) As [Byte]
    Dim RXBUFF(1471) As [Byte]
    Dim BYTES(1) As [Byte]

    Public ArPosmax(71) As Integer
    Public ArPosmin(71) As Integer
    Public arrNomb_joint(71) As Integer


    Dim _UDPServer As UdpClient
    Dim iep As IPEndPoint = Nothing
    Dim server As New UdpClient(10001)

    Private LINK_RX As Thread
    Private LINK_TX As Thread
    Dim EN As Boolean = True

    Public cnt_RX As Integer = 0
    Public cnt_tX As Integer = 0

    '================================================================================================================================
    Public Function START() As Boolean
        '  Dim i, j As Int16
        Try
            _UDPServer = New UdpClient
            _UDPServer.Connect("192.169.1.10", 10001) '("192.169.1.3", 10001)//  0.10
            _UDPServer.DontFragment = True
            _UDPServer.DontFragment = True

        
            ' WRBUFFER(16) = 1


            ' WRBUFFER(0) = 0
            'WRBUFFER(1) = 3
            'WRBUFFER(2) = 4
            'WRBUFFER(3) = 5
            'WRBUFFER(4) = 6
            'WRBUFFER(5) = 7
            'WRBUFFER(6) = 8
            'WRBUFFER(7) = 9
            'WRBUFFER(8) = 10
            'WRBUFFER(9) = 11
            'WRBUFFER(10) = 12
            'WRBUFFER(11) = 13
            'WRBUFFER(12) = 14
            'WRBUFFER(13) = 15
            'WRBUFFER(14) = 16
            'WRBUFFER(15) = 17

            '  WRBUFFER(16) = 1
            'WRBUFFER(17) = 3
            'WRBUFFER(18) = 4
            'WRBUFFER(19) = 5
            'WRBUFFER(20) = 6
            'WRBUFFER(21) = 7
            'WRBUFFER(22) = 8
            'WRBUFFER(23) = 9
            'WRBUFFER(24) = 10
            'WRBUFFER(25) = 11
            'WRBUFFER(26) = 12
            'WRBUFFER(27) = 13
            'WRBUFFER(28) = 14
            'WRBUFFER(29) = 15
            'WRBUFFER(30) = 16
            'WRBUFFER(31) = 17

            'WRBUFFER(32) = 2
            'WRBUFFER(48) = 3
            'WRBUFFER(64) = 4
            'WRBUFFER(80) = 5
            'WRBUFFER(400) = 49
            'WRBUFFER(784) = 49


            'WRBUFFER(36) = 6
            'WRBUFFER(37) = 7
            'WRBUFFER(38) = 8
            'WRBUFFER(39) = 9
            'WRBUFFER(40) = 10
            'WRBUFFER(41) = 11
            'WRBUFFER(42) = 12
            'WRBUFFER(43) = 13
            'WRBUFFER(44) = 14
            'WRBUFFER(45) = 15
            'WRBUFFER(46) = 16
            'WRBUFFER(47) = 17

            'WRBUFFER(48) = 3

            LINK_TX = New Thread(AddressOf TRANS)
            ' LINK_TX.Priority = ThreadPriority.AboveNormal
            LINK_TX.Start()

            LINK_RX = New Thread(AddressOf RECV)
            '  LINK_RX.Priority = ThreadPriority.AboveNormal
            LINK_RX.Start()
            'EN = True
            START = True
        Catch
            START = False
        End Try


    End Function
    Public Sub adress_update(nomb As Integer, buff_adr As Integer)
        WRBUFFER(buff_adr * 16) = nomb
    End Sub
    Public Sub CLOSE()
        LINK_TX.Abort()
        LINK_RX.Abort()
        _UDPServer.Close()
        server.Close()
        _UDPServer = Nothing
        server = Nothing
    End Sub
    Private Sub TRANS()
  
M1:

        Try
            EN = False
            _UDPServer.Send(WRBUFFER, WRBUFFER.Length)
            EN = True
        Catch
        End Try
        Sleep(10)
        cnt_tX += 1
        GoTo M1
    End Sub
    Private Sub RECV()

M2:
        RXBUFF = server.Receive(iep)
        Sleep(1)
        cnt_RX += 1
        GoTo M2
    End Sub
    Public Sub ON61()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) Or 1
   
    End Sub
    Public Sub ON62()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) Or 2
    End Sub
    Public Sub ON81()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) Or 4
    End Sub
    Public Sub ON82()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) Or 8
    End Sub
    Public Sub ON12()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) Or 16
    End Sub
    Public Sub ON48()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) Or 32
    End Sub
    Public Sub OFF61()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) And (255 - 1)
    End Sub
    Public Sub OFF62()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) And (255 - 2)
    End Sub
    Public Sub OFF81()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) And (255 - 4)
    End Sub
    Public Sub OFF82()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) And (255 - 8)
    End Sub
    Public Sub OFF12()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) And (255 - 16)
    End Sub
    Public Sub OFF48()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) And (255 - 32)
    End Sub
    Public Sub MUTE_ON()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) Or 128
    End Sub
    Public Sub MUTE_OFF()
        While EN = False
        End While
        WRBUFFER(1409) = WRBUFFER(1409) And (255 - 128)
    End Sub
    Public Sub Save()
        While EN = False
        End While

    End Sub

#Region "Sensor"
    Public ReadOnly Property SENSOR_YAW(NOMB As Int16) As Int16
        Get
            SENSOR_YAW = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 2)
        End Get
    End Property
    Public ReadOnly Property SENSOR_PITCH(NOMB As Int16) As Int16
        Get
            SENSOR_PITCH = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 4)
        End Get
    End Property
    Public ReadOnly Property SENSOR_ROLL(NOMB As Int16) As Int16
        Get
            SENSOR_ROLL = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 6)
        End Get
    End Property

    Public ReadOnly Property GYRO_MAGN(NOMB As Int16) As Int16
        Get
            GYRO_MAGN = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 2)
        End Get
    End Property
    Public ReadOnly Property GYRO_X(NOMB As Int16) As Int16
        Get
            GYRO_X = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 4)
        End Get
    End Property
    Public ReadOnly Property GYRO_Y(NOMB As Int16) As Int16
        Get
            GYRO_Y = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 6)
        End Get
    End Property
    Public ReadOnly Property GYRO_Z(NOMB As Int16) As Int16
        Get
            GYRO_Z = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 8)
        End Get
    End Property

    Public ReadOnly Property GYRO_ACCX(NOMB As Int16) As Int16
        Get
            GYRO_ACCX = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 10)
        End Get
    End Property
    Public ReadOnly Property GYRO_ACCY(NOMB As Int16) As Int16
        Get
            GYRO_ACCY = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 12)
        End Get
    End Property
    Public ReadOnly Property GYRO_ACCZ(NOMB As Int16) As Int16
        Get
            GYRO_ACCZ = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 14)
        End Get
    End Property
#End Region

#Region "MOTOR"
    Public ReadOnly Property IMOT(NOMB As Int16) As Int16
        Get
            IMOT = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 6)
        End Get
    End Property
    Public ReadOnly Property UBATT(NOMB As Int16) As Int16
        Get
            UBATT = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 4)
        End Get
    End Property
    Public ReadOnly Property CPOS(NOMB As Int16) As Int16
        Get
            CPOS = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 2)
        End Get
    End Property
    Public WriteOnly Property ANGLE(NOMB As Int16) As Int16
        Set(value As Int16)
            Debug.Print(String.Format("MB) ANGLE Sub: If (value > ArPosmin(arrNomb_joint(NOMB))) And (value < ArPosmax(arrNomb_joint(NOMB)))"))
            Debug.Print(String.Format("MB) value = {0}, ArPosmin(arrNomb_joint(NOMB)) = {1}, ArPosmax(arrNomb_joint(NOMB)) = {2}", value, ArPosmin(arrNomb_joint(NOMB)), ArPosmax(arrNomb_joint(NOMB))))
            If (value > ArPosmin(arrNomb_joint(NOMB))) And (value < ArPosmax(arrNomb_joint(NOMB))) Then
                BYTES = BitConverter.GetBytes(value)
                While EN = False
                End While
                WRBUFFER(NOMB * 16 + 3) = BYTES(1)
                WRBUFFER(NOMB * 16 + 2) = BYTES(0)
                Debug.Print(String.Format("MB) WRBUFFER(NOMB * 16 + 3) = BYTES(1), WRBUFFER(NOMB * 16 + 2) = BYTES(0)"))
                Debug.Print(String.Format("MB) BYTES(1) = {0}, BYTES(0) = {1}", BYTES(1), BYTES(0)))
            End If
        End Set
    End Property
    Public WriteOnly Property ILIM(NOMB As Int16) As Int16

        Set(value As Int16)
            BYTES = BitConverter.GetBytes(value)
            While EN = False
            End While
            WRBUFFER(NOMB * 16 + 7) = BYTES(1)
            WRBUFFER(NOMB * 16 + 6) = BYTES(0)
        End Set
    End Property
    Public Property STIFF(NOMB As Int16) As Int16
        Get
            STIFF = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 8)
        End Get
        Set(value As Int16)
            BYTES = BitConverter.GetBytes(value)
            While EN = False
            End While
            WRBUFFER(NOMB * 16 + 9) = BYTES(1)
            WRBUFFER(NOMB * 16 + 8) = BYTES(0)
        End Set
    End Property
    Public Property DAMP(NOMB As Int16) As Int16
        Get
            DAMP = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 10)
        End Get
        Set(value As Int16)
            BYTES = BitConverter.GetBytes(value)
            While EN = False
            End While
            WRBUFFER(NOMB * 16 + 11) = BYTES(1)
            WRBUFFER(NOMB * 16 + 10) = BYTES(0)
        End Set
    End Property

    Public ReadOnly Property MOT_STAT(NOMB As Int16) As Byte
        Get
            MOT_STAT = RXBUFF(NOMB * 16 + 1)
        End Get

    End Property
    Public Property POSMIN(NOMB As Int16) As Int16
        Get
            POSMIN = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 12)
        End Get
        Set(value As Int16)
            BYTES = BitConverter.GetBytes(value)
            While EN = False
            End While
            WRBUFFER(NOMB * 16 + 13) = BYTES(1)
            WRBUFFER(NOMB * 16 + 12) = BYTES(0)
        End Set
    End Property
    Public Property POSMAX(NOMB As Int16) As Int16
        Get
            POSMAX = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 14)
        End Get
        Set(value As Int16)
            BYTES = BitConverter.GetBytes(value)
            While EN = False
            End While
            WRBUFFER(NOMB * 16 + 15) = BYTES(1)
            WRBUFFER(NOMB * 16 + 14) = BYTES(0)
        End Set
    End Property

    Public ReadOnly Property U(NOMB As Int16) As Int16
        Get
            U = BitConverter.ToInt16(RXBUFF, NOMB * 2 + 1408)
        End Get
    End Property
    Public ReadOnly Property I(NOMB As Int16) As Int16
        Get
            I = BitConverter.ToInt16(RXBUFF, NOMB * 2 + 12 + 1408)
        End Get
    End Property


    Public Sub MOT_CENTER(NOMB As Int16)
        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 7) = RXBUFF(NOMB * 16 + 3)
        WRBUFFER(NOMB * 16 + 6) = RXBUFF(NOMB * 16 + 2)
    End Sub
    Public Sub MOT_STOP(NOMB As Int16)
        While EN = False
        End While

        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) And (255 - 3)
        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) Or 1
    End Sub
    Public Sub MOT_TRACE(NOMB As Int16)
        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) Or 3
        Debug.Print(String.Format("MB) MOT_TRACE Sub: WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) Or 3"))
        Debug.Print(String.Format("MB) NOMB * 16 + 1 = {0}, WRBUFFER(NOMB * 16 + 1) = {1}", NOMB * 16 + 1, WRBUFFER(NOMB * 16 + 1)))
    End Sub
    Public Sub MOT_RELAX(NOMB As Int16)
        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) And (255 - 3)
        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) Or 2

    End Sub
    Public Sub MOT_STOP_BR(NOMB As Int16)
        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) And (255 - 3)
    End Sub

    Public Sub MOT_SET_REVERS(NOMB As Int16)
        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) Or 128
    End Sub
    Public Sub MOT_CLR_REVERS(NOMB As Int16)
        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 1) = WRBUFFER(NOMB * 16 + 1) And (255 - 128)
    End Sub

#End Region

#Region "Sensor"
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public ReadOnly Property UCH0(NOMB As Int16) As Int16
        Get
            UCH0 = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 8)
        End Get
    End Property
    Public ReadOnly Property UCH1(NOMB As Int16) As Int16
        Get
            UCH1 = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 10)
        End Get
    End Property
    Public ReadOnly Property UCH2(NOMB As Int16) As Int16
        Get
            UCH2 = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 12)
        End Get
    End Property
    Public ReadOnly Property UCH3(NOMB As Int16) As Int16
        Get
            UCH3 = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 14)
        End Get
    End Property
    Public ReadOnly Property TX(NOMB As Int16) As Int16
        Get
            TX = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 2)
        End Get
    End Property
    Public ReadOnly Property TY(NOMB As Int16) As Int16
        Get
            TY = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 4)
        End Get
    End Property
    Public ReadOnly Property FZ(NOMB As Int16) As Int16
        Get
            FZ = BitConverter.ToInt16(RXBUFF, NOMB * 16 + 6)
        End Get
    End Property

    Public Sub OFFSET(NOMB As Int16)

        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 8) = RXBUFF(NOMB * 16 + 8)
        WRBUFFER(NOMB * 16 + 9) = RXBUFF(NOMB * 16 + 9)

        WRBUFFER(NOMB * 16 + 10) = RXBUFF(NOMB * 16 + 10)
        WRBUFFER(NOMB * 16 + 11) = RXBUFF(NOMB * 16 + 11)

        WRBUFFER(NOMB * 16 + 12) = RXBUFF(NOMB * 16 + 12)
        WRBUFFER(NOMB * 16 + 13) = RXBUFF(NOMB * 16 + 13)

        WRBUFFER(NOMB * 16 + 14) = RXBUFF(NOMB * 16 + 14)
        WRBUFFER(NOMB * 16 + 15) = RXBUFF(NOMB * 16 + 15)


    End Sub
    Public Sub XY_OFFSET(NOMB As Int16)

        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 2) = RXBUFF(NOMB * 16 + 2)
        WRBUFFER(NOMB * 16 + 3) = RXBUFF(NOMB * 16 + 3)

        WRBUFFER(NOMB * 16 + 4) = RXBUFF(NOMB * 16 + 4)
        WRBUFFER(NOMB * 16 + 5) = RXBUFF(NOMB * 16 + 5)



    End Sub
    Public Sub Z_OFFSET(NOMB As Int16)

        While EN = False
        End While
        WRBUFFER(NOMB * 16 + 6) = RXBUFF(NOMB * 16 + 6)
        WRBUFFER(NOMB * 16 + 7) = RXBUFF(NOMB * 16 + 7)

    End Sub

#End Region


End Class
