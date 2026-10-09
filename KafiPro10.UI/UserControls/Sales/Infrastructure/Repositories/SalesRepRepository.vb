Imports System.Data
Imports System.Data.SqlClient
Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro10.Solution.KafiPro.Sales.Application.DTOs
Imports KafiPro10.Solution.KafiPro10.Solution.KafiPro.Sales.Infrastructure.Repositories.Interfaces
Imports Microsoft.Data.SqlClient

Namespace KafiPro10.Solution.KafiPro.Sales.Infrastructure.Repositories
    Public Class SalesRepRepository
        Inherits BaseRepository
        Implements ISalesRepRepository
        Public Function GetAll() As List(Of SalesRepDto) _
            Implements ISalesRepRepository.GetAll
            Dim result As New List(Of SalesRepDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Sales.usp_Rep_GetAll")
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapRep(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        Public Function GetById(repId As Integer) As SalesRepDto _
            Implements ISalesRepRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Sales.usp_Rep_GetById")
                    cmd.Parameters.AddWithValue("@RepID", repId)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then Return MapRep(reader)
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function Insert(rep As SalesRepDto, userId As Integer) As Integer _
            Implements ISalesRepRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Sales.usp_Rep_Insert")
                    With cmd.Parameters
                        .AddWithValue("@RepCode", rep.RepCode)
                        .AddWithValue("@RepName", rep.RepName)
                        .AddWithValue("@Phone", If(String.IsNullOrEmpty(rep.Phone), CObj(DBNull.Value), rep.Phone))
                        .AddWithValue("@BranchID", rep.BranchID)
                        .AddWithValue("@RouteID", rep.RouteID)
                        .AddWithValue("@CommissionRate", rep.CommissionRate)
                        .AddWithValue("@MonthlyTarget", rep.MonthlyTarget)
                        .AddWithValue("@IsActive", rep.IsActive)
                        .AddWithValue("@CreatedBy", userId)
                    End With
                    Dim newId = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        Public Sub Update(rep As SalesRepDto, userId As Integer) _
            Implements ISalesRepRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Sales.usp_Rep_Update")
                    With cmd.Parameters
                        .AddWithValue("@RepID", rep.RepID)
                        .AddWithValue("@RepCode", rep.RepCode)
                        .AddWithValue("@RepName", rep.RepName)
                        .AddWithValue("@Phone", If(String.IsNullOrEmpty(rep.Phone), CObj(DBNull.Value), rep.Phone))
                        .AddWithValue("@BranchID", rep.BranchID)
                        .AddWithValue("@RouteID", rep.RouteID)
                        .AddWithValue("@CommissionRate", rep.CommissionRate)
                        .AddWithValue("@MonthlyTarget", rep.MonthlyTarget)
                        .AddWithValue("@IsActive", rep.IsActive)
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub Delete(repId As Integer, userId As Integer) _
            Implements ISalesRepRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Sales.usp_Rep_Delete")
                    cmd.Parameters.AddWithValue("@RepID", repId)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function GetBranches() As List(Of BranchDto) _
            Implements ISalesRepRepository.GetBranches

            Dim result As New List(Of BranchDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Sales.usp_Branch_GetAll")
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(New BranchDto With {
                                .BranchID = reader.GetInt32(0),
                                .BranchCode = GetString(reader, 1),
                                .BranchName = GetString(reader, 2),
                                .RegionName = GetString(reader, 3),
                                .IsActive = reader.GetBoolean(4)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        Public Function GetRoutes() As List(Of RouteDto) _
            Implements ISalesRepRepository.GetRoutes

            Dim result As New List(Of RouteDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Sales.usp_Route_GetAll")
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(New RouteDto With {
                                .RouteID = reader.GetInt32(0),
                                .RouteCode = GetString(reader, 1),
                                .RouteName = GetString(reader, 2),
                                .BranchID = reader.GetInt32(3),
                                .VisitDays = GetString(reader, 4),
                                .IsActive = reader.GetBoolean(5)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        Private Function MapRep(reader As SqlDataReader) As SalesRepDto
            Return New SalesRepDto With {
                .RepID = reader.GetInt32(0),
                .RepCode = GetString(reader, 1),
                .RepName = GetString(reader, 2),
                .Phone = GetString(reader, 3),
                .BranchID = reader.GetInt32(4),
                .BranchName = GetString(reader, 5),
                .RouteID = reader.GetInt32(6),
                .RouteName = GetString(reader, 7),
                .CommissionRate = If(reader.IsDBNull(8), 0D, reader.GetDecimal(8)),
                .MonthlyTarget = If(reader.IsDBNull(9), 0D, reader.GetDecimal(9)),
                .IsActive = reader.GetBoolean(10)
            }
        End Function

    End Class

End Namespace