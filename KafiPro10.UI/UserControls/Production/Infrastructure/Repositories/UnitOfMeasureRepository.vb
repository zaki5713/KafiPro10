Imports System.Data
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports KafiPro10.Solution.KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class UnitOfMeasureRepository
        Inherits BaseRepository
        Implements IUnitOfMeasureRepository

        ' ══════════════════════════════════════════════
        '  GetAll
        ' ══════════════════════════════════════════════
        Public Function GetAll() As List(Of UnitOfMeasureDto) _
            Implements IUnitOfMeasureRepository.GetAll

            Dim result As New List(Of UnitOfMeasureDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_UnitOfMeasure_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapUnit(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        ' ══════════════════════════════════════════════
        '  GetById
        ' ══════════════════════════════════════════════
        Public Function GetById(id As Integer) As UnitOfMeasureDto _
            Implements IUnitOfMeasureRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_UnitOfMeasure_GetById")
                    cmd.Parameters.AddWithValue("@UnitOfMeasureID", id)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then Return MapUnit(reader)
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ══════════════════════════════════════════════
        '  Insert
        ' ══════════════════════════════════════════════
        Public Function Insert(item As UnitOfMeasureDto) As Integer _
            Implements IUnitOfMeasureRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_UnitOfMeasure_Insert")
                    cmd.Parameters.AddWithValue("@UnitNameAr", item.UnitNameAr)
                    cmd.Parameters.AddWithValue("@UnitNameEn", item.UnitNameEn)

                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ══════════════════════════════════════════════
        '  Update
        ' ══════════════════════════════════════════════
        Public Sub Update(item As UnitOfMeasureDto) _
            Implements IUnitOfMeasureRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_UnitOfMeasure_Update")
                    cmd.Parameters.AddWithValue("@UnitOfMeasureID", item.UnitOfMeasureID)
                    cmd.Parameters.AddWithValue("@UnitNameAr", item.UnitNameAr)
                    cmd.Parameters.AddWithValue("@UnitNameEn", item.UnitNameEn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════
        '  Delete
        ' ══════════════════════════════════════════════
        Public Sub Delete(id As Integer) _
            Implements IUnitOfMeasureRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Core.usp_UnitOfMeasure_Delete")
                    cmd.Parameters.AddWithValue("@UnitOfMeasureID", id)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════
        '  Helper
        ' ══════════════════════════════════════════════
        Private Function MapUnit(reader As SqlDataReader) As UnitOfMeasureDto
            Return New UnitOfMeasureDto With {
                .UnitOfMeasureID = reader.GetInt32(0),
                .UnitNameAr = GetString(reader, 1),
                .UnitNameEn = GetString(reader, 2)
            }
        End Function

    End Class

End Namespace