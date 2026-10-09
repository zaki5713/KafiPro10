Imports System.Data
Imports System.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class ShiftTypeRepository
        Inherits BaseRepository
        Implements IShiftTypeRepository

        Public Function GetAll() As List(Of ShiftTypeDto) _
            Implements IShiftTypeRepository.GetAll

            Dim result As New List(Of ShiftTypeDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ShiftType_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapShift(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        Public Function GetById(id As Integer) As ShiftTypeDto _
            Implements IShiftTypeRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ShiftType_GetById")
                    cmd.Parameters.AddWithValue("@ShiftTypeID", id)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then Return MapShift(reader)
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function Insert(item As ShiftTypeDto, userId As Integer) As Integer _
            Implements IShiftTypeRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ShiftType_Insert")
                    With cmd.Parameters
                        .AddWithValue("@ShiftCode", item.ShiftCode)
                        .AddWithValue("@ShiftNameAr", item.ShiftNameAr)
                        .AddWithValue("@ShiftNameEn", item.ShiftNameEn)
                        .AddWithValue("@StartTime", If(item.StartTime.HasValue, CObj(item.StartTime.Value), CObj(DBNull.Value)))
                        .AddWithValue("@EndTime", If(item.EndTime.HasValue, CObj(item.EndTime.Value), CObj(DBNull.Value)))
                        .AddWithValue("@IsOvertime", item.IsOvertime)
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@CreatedBy", userId)
                    End With
                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        Public Sub Update(item As ShiftTypeDto, userId As Integer) _
            Implements IShiftTypeRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ShiftType_Update")
                    With cmd.Parameters
                        .AddWithValue("@ShiftTypeID", item.ShiftTypeID)
                        .AddWithValue("@ShiftCode", item.ShiftCode)
                        .AddWithValue("@ShiftNameAr", item.ShiftNameAr)
                        .AddWithValue("@ShiftNameEn", item.ShiftNameEn)
                        .AddWithValue("@StartTime", If(item.StartTime.HasValue, CObj(item.StartTime.Value), CObj(DBNull.Value)))
                        .AddWithValue("@EndTime", If(item.EndTime.HasValue, CObj(item.EndTime.Value), CObj(DBNull.Value)))
                        .AddWithValue("@IsOvertime", item.IsOvertime)
                        .AddWithValue("@IsActive", item.IsActive)
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub Delete(id As Integer, userId As Integer) _
            Implements IShiftTypeRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_ShiftType_Delete")
                    cmd.Parameters.AddWithValue("@ShiftTypeID", id)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Private Function MapShift(reader As SqlDataReader) As ShiftTypeDto
            Dim st As TimeSpan? = Nothing
            Dim en As TimeSpan? = Nothing
            If Not reader.IsDBNull(4) Then st = reader.GetTimeSpan(4)
            If Not reader.IsDBNull(5) Then en = reader.GetTimeSpan(5)

            Return New ShiftTypeDto With {
                .ShiftTypeID = reader.GetInt32(0),
                .ShiftCode = GetString(reader, 1),
                .ShiftNameAr = GetString(reader, 2),
                .ShiftNameEn = GetString(reader, 3),
                .StartTime = st,
                .EndTime = en,
                .IsOvertime = reader.GetBoolean(6),
                .IsActive = reader.GetBoolean(7)
            }
        End Function

    End Class

End Namespace