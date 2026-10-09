Imports System.Data
Imports System.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class ProductionWorkerRepository
        Inherits BaseRepository
        Implements IProductionWorkerRepository

        ' ============================================================
        '  Get All
        ' ============================================================
        Public Function GetAll() As List(Of ProductionWorkerDto) _
            Implements IProductionWorkerRepository.GetAll

            Dim result As New List(Of ProductionWorkerDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionWorker_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapWorker(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        ' ============================================================
        '  Get By Id
        ' ============================================================
        Public Function GetById(id As Integer) As ProductionWorkerDto _
            Implements IProductionWorkerRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionWorker_GetById")
                    cmd.Parameters.AddWithValue("@WorkerID", id)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then Return MapWorker(reader)
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ============================================================
        '  Get Supervisors — للمشرفين فقط
        ' ============================================================
        Public Function GetSupervisors() As List(Of ProductionWorkerDto) _
            Implements IProductionWorkerRepository.GetSupervisors

            Dim result As New List(Of ProductionWorkerDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionWorker_GetSupervisors")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapWorker(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        ' ============================================================
        '  Insert
        ' ============================================================
        Public Function Insert(item As ProductionWorkerDto, userId As Integer) As Integer _
            Implements IProductionWorkerRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionWorker_Insert")
                    With cmd.Parameters
                        .AddWithValue("@WorkerCode", item.WorkerCode)
                        .AddWithValue("@WorkerNameAr", item.WorkerNameAr)
                        .AddWithValue("@WorkerNameEn", If(String.IsNullOrEmpty(item.WorkerNameEn), CObj(DBNull.Value), item.WorkerNameEn))
                        .AddWithValue("@ProductionRole", item.ProductionRole)
                        .AddWithValue("@PhoneNumber", If(String.IsNullOrEmpty(item.PhoneNumber), CObj(DBNull.Value), item.PhoneNumber))
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@CreatedBy", userId)
                    End With
                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ============================================================
        '  Update
        ' ============================================================
        Public Sub Update(item As ProductionWorkerDto, userId As Integer) _
            Implements IProductionWorkerRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionWorker_Update")
                    With cmd.Parameters
                        .AddWithValue("@WorkerID", item.WorkerID)
                        .AddWithValue("@WorkerCode", item.WorkerCode)
                        .AddWithValue("@WorkerNameAr", item.WorkerNameAr)
                        .AddWithValue("@WorkerNameEn", If(String.IsNullOrEmpty(item.WorkerNameEn), CObj(DBNull.Value), item.WorkerNameEn))
                        .AddWithValue("@ProductionRole", item.ProductionRole)
                        .AddWithValue("@PhoneNumber", If(String.IsNullOrEmpty(item.PhoneNumber), CObj(DBNull.Value), item.PhoneNumber))
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ============================================================
        '  Delete
        ' ============================================================
        Public Sub Delete(id As Integer, userId As Integer) _
            Implements IProductionWorkerRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ProductionWorker_Delete")
                    cmd.Parameters.AddWithValue("@WorkerID", id)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ============================================================
        '  Helper: تحويل صف Reader → DTO
        '  ⚠️ ترتيب الأعمدة لازم يطابق كل الـ SPs (GetAll / GetById / GetSupervisors)
        ' ============================================================
        Private Function MapWorker(reader As SqlDataReader) As ProductionWorkerDto
            Return New ProductionWorkerDto With {
                .WorkerID = reader.GetInt32(0),
                .WorkerCode = GetString(reader, 1),
                .WorkerNameAr = GetString(reader, 2),
                .WorkerNameEn = GetString(reader, 3),
                .ProductionRole = GetString(reader, 4),
                .PhoneNumber = GetString(reader, 5),
                .IsActive = reader.GetBoolean(6)
            }
        End Function

    End Class

End Namespace