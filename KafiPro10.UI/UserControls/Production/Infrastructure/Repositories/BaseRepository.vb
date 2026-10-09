Imports Microsoft.Data.SqlClient
Imports System.Data

Namespace KafiPro.Infrastructure.Repositories

    ''' <summary>
    ''' كلاس أساسي لكل الـ Repositories
    ''' </summary>
    Public MustInherit Class BaseRepository

        Protected ReadOnly ConnectionString As String

        Protected Sub New()
            ConnectionString = Persistence.DBConnection.GetConnectionString()
        End Sub

        Protected Function CreateConnection() As SqlConnection
            Dim conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn
        End Function

        Protected Function CreateCommand(conn As SqlConnection, spName As String) As SqlCommand
            Return New SqlCommand(spName, conn) With {
                .CommandType = CommandType.StoredProcedure,
                .CommandTimeout = 30
            }
        End Function

        ' ============================================
        ' Helpers للقراءة الآمنة من SqlDataReader
        ' ============================================

        Protected Shared Function GetString(reader As SqlDataReader, ordinal As Integer) As String
            If reader.IsDBNull(ordinal) Then Return Nothing
            Return reader.GetString(ordinal)
        End Function

        Protected Shared Function GetInt(reader As SqlDataReader, ordinal As Integer) As Integer?
            If reader.IsDBNull(ordinal) Then Return Nothing
            Return reader.GetInt32(ordinal)
        End Function

        Protected Shared Function GetLong(reader As SqlDataReader, ordinal As Integer) As Long?
            If reader.IsDBNull(ordinal) Then Return Nothing
            Return reader.GetInt64(ordinal)
        End Function

        Protected Shared Function GetDecimal(reader As SqlDataReader, ordinal As Integer) As Decimal?
            If reader.IsDBNull(ordinal) Then Return Nothing
            Return reader.GetDecimal(ordinal)
        End Function

        Protected Shared Function GetDate(reader As SqlDataReader, ordinal As Integer) As DateTime?
            If reader.IsDBNull(ordinal) Then Return Nothing
            Return reader.GetDateTime(ordinal)
        End Function

        Protected Shared Function GetTimeSpan(reader As SqlDataReader, ordinal As Integer) As TimeSpan?
            If reader.IsDBNull(ordinal) Then Return Nothing
            Return reader.GetTimeSpan(ordinal)
        End Function

    End Class

End Namespace
