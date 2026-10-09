Imports Microsoft.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence
Namespace KafiPro.Infrastructure.Persistence

    ''' <summary>
    ''' مسؤول عن توفير اتصالات قاعدة البيانات
    ''' </summary>
    Public NotInheritable Class DBConnection

        ' ==========================================
        '  ⚠️ عدّل القيم دي حسب إعداداتك
        ' ==========================================
        Private Const ServerName As String = "."
        Private Const DatabaseName As String = "DBKafiPro"

        Private Const UseSqlAuth As Boolean = False
        Private Const SqlUser As String = "sa"
        Private Const SqlPassword As String = ""

        Private Sub New()
        End Sub

        ''' <summary>
        ''' بيبني الـ Connection String
        ''' </summary>
        Public Shared Function GetConnectionString() As String

            Dim builder As New SqlConnectionStringBuilder()

            builder.DataSource = ServerName
            builder.InitialCatalog = DatabaseName
            builder.ConnectTimeout = 30
            builder.ApplicationName = "KafiPro"
            builder.TrustServerCertificate = True

            If UseSqlAuth Then
                builder.UserID = SqlUser
                builder.Password = SqlPassword
            Else
                builder.IntegratedSecurity = True
            End If

            Return builder.ConnectionString

        End Function

        ''' <summary>
        ''' بيفتح اتصال جديد بقاعدة البيانات
        ''' </summary>
        Public Shared Function GetConnection() As SqlConnection
            Dim conn As New SqlConnection(GetConnectionString())
            conn.Open()
            Return conn
        End Function

        ''' <summary>
        ''' اختبار الاتصال — بيرمي رسالة الخطأ الحقيقية
        ''' </summary>
        Public Shared Function TestConnection() As Boolean
            Using conn As SqlConnection = GetConnection()
                Return conn.State = ConnectionState.Open
            End Using
        End Function

    End Class

End Namespace