Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

Public Class Db

    Public Shared Function Connexion() As SqlConnection
        Return New SqlConnection(ConfigurationManager.ConnectionStrings("BilletBusDB").ConnectionString)
    End Function

    ' SELECT -> DataTable
    Public Shared Function Table(sql As String, ParamArray p() As SqlParameter) As DataTable
        Dim dt As New DataTable()
        Using cn As SqlConnection = Connexion()
            Using da As New SqlDataAdapter(sql, cn)
                If p IsNot Nothing Then da.SelectCommand.Parameters.AddRange(p)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ' INSERT / UPDATE / DELETE
    Public Shared Function Executer(sql As String, ParamArray p() As SqlParameter) As Integer
        Using cn As SqlConnection = Connexion()
            Using cmd As New SqlCommand(sql, cn)
                If p IsNot Nothing Then cmd.Parameters.AddRange(p)
                cn.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ' Première colonne de la première ligne
    Public Shared Function Scalaire(sql As String, ParamArray p() As SqlParameter) As Object
        Using cn As SqlConnection = Connexion()
            Using cmd As New SqlCommand(sql, cn)
                If p IsNot Nothing Then cmd.Parameters.AddRange(p)
                cn.Open()
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    ' SHA-256 en hexadécimal majuscule (même format que HASHBYTES côté SQL)
    Public Shared Function Hacher(motDePasse As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim octets() As Byte = sha.ComputeHash(Encoding.UTF8.GetBytes(motDePasse))
            Return BitConverter.ToString(octets).Replace("-", "")
        End Using
    End Function

End Class