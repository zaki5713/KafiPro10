Imports KafiPro10.Solution.KafiPro.Infrastructure.Repositories
Imports KafiPro10.Solution.KafiPro.Production.Application.DTOs
Imports KafiPro10.Solution.KafiPro.Production.Infrastructure.Repositories.Interfaces
Imports System.Data
Imports Microsoft.Data.SqlClient


Namespace KafiPro.Production.Infrastructure.Repositories

    Public Class FinalProductTransferRepository
        Inherits BaseRepository
        Implements IFinalProductTransferRepository

        ' ══════════════════════════════════════════════════════
        '  Lookups
        ' ══════════════════════════════════════════════════════
        Public Function GetStatuses() As List(Of FinalProductTransferStatusDto) _
            Implements IFinalProductTransferRepository.GetStatuses

            Dim result As New List(Of FinalProductTransferStatusDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransferStatus_GetAll")
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(New FinalProductTransferStatusDto With {
                                .StatusID = reader.GetInt32(0),
                                .StatusCode = GetString(reader, 1),
                                .StatusNameAr = GetString(reader, 2),
                                .StatusNameEn = GetString(reader, 3),
                                .DisplayOrder = reader.GetInt32(4)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        Public Function GetNextTransferNo() As String _
            Implements IFinalProductTransferRepository.GetNextTransferNo

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_GetNextNo")
                    Dim result As Object = cmd.ExecuteScalar()
                    Return If(result Is Nothing OrElse IsDBNull(result), "", result.ToString())
                End Using
            End Using
        End Function

        ' ══════════════════════════════════════════════════════
        '  Header — GetAll
        ' ══════════════════════════════════════════════════════
        Public Function GetAll(Optional fromDate As Date? = Nothing,
                               Optional toDate As Date? = Nothing,
                               Optional statusId As Integer? = Nothing) As List(Of FinalProductTransferDto) _
            Implements IFinalProductTransferRepository.GetAll

            Dim result As New List(Of FinalProductTransferDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_GetAll")
                    cmd.Parameters.AddWithValue("@FromDate", If(fromDate.HasValue, CObj(fromDate.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ToDate", If(toDate.HasValue, CObj(toDate.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@StatusID", If(statusId.HasValue, CObj(statusId.Value), CObj(DBNull.Value)))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapTransfer(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        ' ══════════════════════════════════════════════════════
        '  Header — GetById
        ' ══════════════════════════════════════════════════════
        Public Function GetById(transferId As Integer) As FinalProductTransferDto _
            Implements IFinalProductTransferRepository.GetById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_GetById")
                    cmd.Parameters.AddWithValue("@TransferID", transferId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New FinalProductTransferDto With {
                                .TransferID = reader.GetInt32(0),
                                .TransferNo = GetString(reader, 1),
                                .TransferDate = reader.GetDateTime(2),
                                .ShiftTypeID = reader.GetInt32(3),
                                .ProductionLineID = reader.GetInt32(4),
                                .SupervisorID = reader.GetInt32(5),
                                .StorekeeperID = If(reader.IsDBNull(6), CType(Nothing, Integer?), reader.GetInt32(6)),
                                .StatusID = reader.GetInt32(7),
                                .Notes = GetString(reader, 8),
                                .ReceivedAt = If(reader.IsDBNull(9), CType(Nothing, DateTime?), reader.GetDateTime(9)),
                                .ReceivedBy = If(reader.IsDBNull(10), CType(Nothing, Integer?), reader.GetInt32(10)),
                                .ApprovedAt = If(reader.IsDBNull(11), CType(Nothing, DateTime?), reader.GetDateTime(11)),
                                .ApprovedBy = If(reader.IsDBNull(12), CType(Nothing, Integer?), reader.GetInt32(12)),
                                .CreatedBy = If(reader.IsDBNull(13), CType(Nothing, Integer?), reader.GetInt32(13)),
                                .CreatedAt = reader.GetDateTime(14),
                                .ModifiedBy = If(reader.IsDBNull(15), CType(Nothing, Integer?), reader.GetInt32(15)),
                                .ModifiedAt = If(reader.IsDBNull(16), CType(Nothing, DateTime?), reader.GetDateTime(16))
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ══════════════════════════════════════════════════════
        '  Header — Insert
        ' ══════════════════════════════════════════════════════
        Public Function Insert(item As FinalProductTransferDto, userId As Integer) As Integer _
            Implements IFinalProductTransferRepository.Insert

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_Insert")
                    With cmd.Parameters
                        .AddWithValue("@TransferNo", item.TransferNo)
                        .AddWithValue("@TransferDate", item.TransferDate)
                        .AddWithValue("@ShiftTypeID", item.ShiftTypeID)
                        .AddWithValue("@ProductionLineID", item.ProductionLineID)
                        .AddWithValue("@SupervisorID", item.SupervisorID)
                        .AddWithValue("@StorekeeperID", If(item.StorekeeperID.HasValue, CObj(item.StorekeeperID.Value), CObj(DBNull.Value)))
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@CreatedBy", userId)
                    End With

                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ══════════════════════════════════════════════════════
        '  Header — Update
        ' ══════════════════════════════════════════════════════
        Public Sub Update(item As FinalProductTransferDto, userId As Integer) _
            Implements IFinalProductTransferRepository.Update

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_Update")
                    With cmd.Parameters
                        .AddWithValue("@TransferID", item.TransferID)
                        .AddWithValue("@TransferNo", item.TransferNo)
                        .AddWithValue("@TransferDate", item.TransferDate)
                        .AddWithValue("@ShiftTypeID", item.ShiftTypeID)
                        .AddWithValue("@ProductionLineID", item.ProductionLineID)
                        .AddWithValue("@SupervisorID", item.SupervisorID)
                        .AddWithValue("@StorekeeperID", If(item.StorekeeperID.HasValue, CObj(item.StorekeeperID.Value), CObj(DBNull.Value)))
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════════
        '  Header — Delete
        ' ══════════════════════════════════════════════════════
        Public Sub Delete(transferId As Integer, userId As Integer) _
            Implements IFinalProductTransferRepository.Delete

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_Delete")
                    cmd.Parameters.AddWithValue("@TransferID", transferId)
                    cmd.Parameters.AddWithValue("@DeletedBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════════
        '  Workflow — Approve
        ' ══════════════════════════════════════════════════════
        Public Sub Approve(transferId As Integer, approvedBy As Integer) _
            Implements IFinalProductTransferRepository.Approve

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_Approve")
                    cmd.Parameters.AddWithValue("@TransferID", transferId)
                    cmd.Parameters.AddWithValue("@ApprovedBy", approvedBy)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════════
        '  Workflow — Receive
        ' ══════════════════════════════════════════════════════
        Public Sub Receive(transferId As Integer, receivedBy As Integer) _
            Implements IFinalProductTransferRepository.Receive

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_Receive")
                    cmd.Parameters.AddWithValue("@TransferID", transferId)
                    cmd.Parameters.AddWithValue("@WarehouseID", DBNull.Value)  ' ← NULL = W01
                    cmd.Parameters.AddWithValue("@ReceivedBy", receivedBy)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════════
        '  Details — GetByTransferId
        ' ══════════════════════════════════════════════════════
        Public Function GetDetails(transferId As Integer) As List(Of FinalProductTransferDetailDto) _
            Implements IFinalProductTransferRepository.GetDetails

            Dim result As New List(Of FinalProductTransferDetailDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransferDetail_GetByTransferId")
                    cmd.Parameters.AddWithValue("@TransferID", transferId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(MapDetail(reader))
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        ' ══════════════════════════════════════════════════════
        '  Details — GetById
        ' ══════════════════════════════════════════════════════
        Public Function GetDetailById(detailId As Integer) As FinalProductTransferDetailDto _
            Implements IFinalProductTransferRepository.GetDetailById

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransferDetail_GetById")
                    cmd.Parameters.AddWithValue("@DetailID", detailId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New FinalProductTransferDetailDto With {
                                .DetailID = reader.GetInt32(0),
                                .TransferID = reader.GetInt32(1),
                                .DailyLogID = reader.GetInt64(2),
                                .ItemID = reader.GetInt32(3),
                                .TransferredQty = reader.GetDecimal(4),
                                .RejectedQty = reader.GetDecimal(5),
                                .BatchNo = GetString(reader, 6),
                                .ExpiryDate = If(reader.IsDBNull(7), CType(Nothing, Date?), reader.GetDateTime(7)),
                                .Notes = GetString(reader, 8)
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ' ══════════════════════════════════════════════════════
        '  Details — Insert
        ' ══════════════════════════════════════════════════════
        Public Function InsertDetail(item As FinalProductTransferDetailDto, userId As Integer) As Integer _
            Implements IFinalProductTransferRepository.InsertDetail

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransferDetail_Insert")
                    With cmd.Parameters
                        .AddWithValue("@TransferID", item.TransferID)
                        .AddWithValue("@DailyLogID", item.DailyLogID)
                        .AddWithValue("@ItemID", item.ItemID)
                        .AddWithValue("@TransferredQty", item.TransferredQty)
                        .AddWithValue("@RejectedQty", item.RejectedQty)
                        .AddWithValue("@BatchNo", If(String.IsNullOrEmpty(item.BatchNo), CObj(DBNull.Value), item.BatchNo))
                        .AddWithValue("@ExpiryDate", If(item.ExpiryDate.HasValue, CObj(item.ExpiryDate.Value), CObj(DBNull.Value)))
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@CreatedBy", userId)
                    End With

                    Dim newId As Object = cmd.ExecuteScalar()
                    Return If(newId Is Nothing OrElse IsDBNull(newId), 0, Convert.ToInt32(newId))
                End Using
            End Using
        End Function

        ' ══════════════════════════════════════════════════════
        '  Details — Update
        ' ══════════════════════════════════════════════════════
        Public Sub UpdateDetail(item As FinalProductTransferDetailDto, userId As Integer) _
            Implements IFinalProductTransferRepository.UpdateDetail

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransferDetail_Update")
                    With cmd.Parameters
                        .AddWithValue("@DetailID", item.DetailID)
                        .AddWithValue("@TransferredQty", item.TransferredQty)
                        .AddWithValue("@RejectedQty", item.RejectedQty)
                        .AddWithValue("@BatchNo", If(String.IsNullOrEmpty(item.BatchNo), CObj(DBNull.Value), item.BatchNo))
                        .AddWithValue("@ExpiryDate", If(item.ExpiryDate.HasValue, CObj(item.ExpiryDate.Value), CObj(DBNull.Value)))
                        .AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), item.Notes))
                        .AddWithValue("@ModifiedBy", userId)
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════════
        '  Details — Delete
        ' ══════════════════════════════════════════════════════
        Public Sub DeleteDetail(detailId As Integer) _
            Implements IFinalProductTransferRepository.DeleteDetail

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransferDetail_Delete")
                    cmd.Parameters.AddWithValue("@DetailID", detailId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ' ══════════════════════════════════════════════════════
        '  ⭐ GetAvailable — الأهم
        ' ══════════════════════════════════════════════════════
        Public Function GetAvailableProductions(Optional fromDate As Date? = Nothing,
                                                 Optional toDate As Date? = Nothing,
                                                 Optional shiftTypeId As Integer? = Nothing,
                                                 Optional productionLineId As Integer? = Nothing,
                                                 Optional itemId As Integer? = Nothing) As List(Of AvailableProductionDto) _
            Implements IFinalProductTransferRepository.GetAvailableProductions

            Dim result As New List(Of AvailableProductionDto)()
            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_DailyProductionLog_GetAvailable")
                    cmd.Parameters.AddWithValue("@FromDate", If(fromDate.HasValue, CObj(fromDate.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ToDate", If(toDate.HasValue, CObj(toDate.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ShiftTypeID", If(shiftTypeId.HasValue, CObj(shiftTypeId.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ProductionLineID", If(productionLineId.HasValue, CObj(productionLineId.Value), CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@ItemID", If(itemId.HasValue, CObj(itemId.Value), CObj(DBNull.Value)))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(New AvailableProductionDto With {
                                .LogID = reader.GetInt64(0),
                                .ProductionDate = reader.GetDateTime(1),
                                .ShiftTypeID = reader.GetInt32(2),
                                .ShiftNameAr = GetString(reader, 3),
                                .ProductionLineID = reader.GetInt32(4),
                                .ProductionLineNameAr = GetString(reader, 5),
                                .ItemID = reader.GetInt32(6),
                                .ItemCode = GetString(reader, 7),
                                .ItemNameAr = GetString(reader, 8),
                                .ItemNameEn = GetString(reader, 9),
                                .UnitName = GetString(reader, 10),
                                .ProducedQty = reader.GetDecimal(11),
                                .TotalTransferredQty = reader.GetDecimal(12),
                                .AvailableQty = reader.GetDecimal(13),
                                .RejectedFGQty = reader.GetDecimal(14),
                                .SupervisorNameAr = GetString(reader, 15)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return result
        End Function

        Public Sub CancelTransfer(transferId As Integer, userId As Integer) _
            Implements IFinalProductTransferRepository.CancelTransfer

            Using conn = CreateConnection()
                Using cmd = CreateCommand(conn, "Production.usp_FinalProductTransfer_Cancel")
                    cmd.Parameters.AddWithValue("@TransferID", transferId)
                    cmd.Parameters.AddWithValue("@CancelledBy", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub
        ' ══════════════════════════════════════════════════════
        '  Helper — Map Header (GetAll — 20 عمود)
        ' ══════════════════════════════════════════════════════
        Private Function MapTransfer(reader As SqlDataReader) As FinalProductTransferDto
            Return New FinalProductTransferDto With {
                .TransferID = reader.GetInt32(0),
                .TransferNo = GetString(reader, 1),
                .TransferDate = reader.GetDateTime(2),
                .ShiftTypeID = reader.GetInt32(3),
                .ShiftNameAr = GetString(reader, 4),
                .ProductionLineID = reader.GetInt32(5),
                .ProductionLineNameAr = GetString(reader, 6),
                .SupervisorID = reader.GetInt32(7),
                .SupervisorNameAr = GetString(reader, 8),
                .StorekeeperID = If(reader.IsDBNull(9), CType(Nothing, Integer?), reader.GetInt32(9)),
                .StatusID = reader.GetInt32(10),
                .StatusCode = GetString(reader, 11),
                .StatusNameAr = GetString(reader, 12),
                .Notes = GetString(reader, 13),
                .ReceivedAt = If(reader.IsDBNull(14), CType(Nothing, DateTime?), reader.GetDateTime(14)),
                .ApprovedAt = If(reader.IsDBNull(15), CType(Nothing, DateTime?), reader.GetDateTime(15)),
                .CreatedAt = reader.GetDateTime(16),
                .DetailsCount = reader.GetInt32(17),
                .TotalTransferredQty = reader.GetDecimal(18)
            }
        End Function

        ' ══════════════════════════════════════════════════════
        '  Helper — Map Detail (14 عمود)
        ' ══════════════════════════════════════════════════════
        Private Function MapDetail(reader As SqlDataReader) As FinalProductTransferDetailDto
            Return New FinalProductTransferDetailDto With {
                .DetailID = reader.GetInt32(0),
                .TransferID = reader.GetInt32(1),
                .DailyLogID = reader.GetInt64(2),
                .ItemID = reader.GetInt32(3),
                .ItemCode = GetString(reader, 4),
                .ItemNameAr = GetString(reader, 5),
                .ItemNameEn = GetString(reader, 6),
                .TransferredQty = reader.GetDecimal(7),
                .RejectedQty = reader.GetDecimal(8),
                .BatchNo = GetString(reader, 9),
                .ExpiryDate = If(reader.IsDBNull(10), CType(Nothing, Date?), reader.GetDateTime(10)),
                .Notes = GetString(reader, 11),
                .ProductionDate = If(reader.IsDBNull(12), CType(Nothing, Date?), reader.GetDateTime(12)),
                .ProducedQty = If(reader.IsDBNull(13), CType(Nothing, Decimal?), reader.GetDecimal(13))
            }
        End Function


    End Class

End Namespace