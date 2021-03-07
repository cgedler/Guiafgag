Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Module M_CNN
#Region "Variables Publicas del Sistema"
    'Conexión a las Base de Datos
    Public cnn1 As SqlClient.SqlConnection
    Public cnn2 As SqlClient.SqlConnection
#End Region
#Region "Conexión a la Base de Datos"
    Public Sub open_conection1()
        Try
            cnn1 = New SqlClient.SqlConnection
            cnn1.ConnectionString = My.Settings.cnn1
            cnn1.Open()
        Catch ex As Exception
            MsgBoxError(mensaje:="No se encuentra la conexión del Sistema!", titulo:="ERROR: Conexión a la Base de Datos - Sistema")
        End Try
    End Sub
    Public Sub open_conection2()
        Try
            cnn2 = New SqlClient.SqlConnection
            cnn2.ConnectionString = My.Settings.cnn2
            cnn2.Open()
        Catch ex As Exception
            MsgBoxError(mensaje:="No se encuentra la conexión del Sistema!", titulo:="ERROR: Conexión a la Base de Datos - Sistema")
        End Try
    End Sub
#End Region
End Module
