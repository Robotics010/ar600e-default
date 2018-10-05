
Module CSVLog_class

    Dim logfile As System.IO.StreamWriter
    Dim strLine As String
    Dim strDate As String
    Dim strTime As String
    Dim strMTime As String
    Dim strPC As String
    Dim dtDateTime As Date

    Public Class CSVLog_class_type
        Public Sub New(ByVal pathfile As String)
            logfile = My.Computer.FileSystem.OpenTextFileWriter(pathfile, True)
            strPC = "VB"
        End Sub
        Public Sub Log(ByVal logmodule As String, ByVal logfunction As String, ByVal logmessage As String)
            dtDateTime = DateTime.Now
            strDate = dtDateTime.ToString("dd.MM.yyyy")
            strTime = dtDateTime.ToString("HH:mm:ss")
            strMTime = dtDateTime.ToString("fff")
            strLine = strDate & ";" & strTime & ";" & strMTime & ";" & strPC & ";" & logmodule & ";" & logfunction & ";" & logmessage
            logfile.WriteLine(strLine)
        End Sub
        Public Sub Close()
            logfile.Close()
        End Sub
    End Class

End Module
