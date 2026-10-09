Imports Microsoft.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports KafiPro10.Solution.KafiPro.Infrastructure.Persistence

Public Class WithdrawalRepository

    ' ═══════════════════════════════════════════════════════════
    '  GetNextNo
    ' ═══════════════════════════════════════════════════════════
    Public Function GetNextNo() As String
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_GetNextNo]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                Return CStr(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Create
    ' ═══════════════════════════════════════════════════════════
    Public Function Create(dto As CreateWithdrawalDto) As (WithdrawalID As Long, WithdrawalNo As String)
        Dim no As String = GetNextNo()

        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_Insert]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalNo", no)
                cmd.Parameters.AddWithValue("@WithdrawalDate", dto.WithdrawalDate)
                cmd.Parameters.AddWithValue("@WarehouseID", dto.WarehouseID)
                cmd.Parameters.AddWithValue("@ShiftID", If(dto.ShiftID.HasValue, CObj(dto.ShiftID.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@ProductionLineID", If(dto.ProductionLineID.HasValue, CObj(dto.ProductionLineID.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(dto.Notes), CObj(DBNull.Value), dto.Notes))
                cmd.Parameters.AddWithValue("@CreatedBy", dto.CreatedBy)

                Dim tvp = cmd.Parameters.AddWithValue("@Details", BuildDetailTvp(dto.Details))
                tvp.SqlDbType = SqlDbType.Structured
                tvp.TypeName = "[Production].[WithdrawalDetailType]"

                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        Dim wid As Long = rd.GetInt64(0)
                        Dim wno As String = rd.GetString(1)
                        rd.Close()
                        Return (wid, wno)
                    End If
                End Using
            End Using
        End Using

        Throw New Exception("فشل إنشاء الطلب.")
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Update
    ' ═══════════════════════════════════════════════════════════
    Public Sub Update(WithdrawalID As Long, dto As CreateWithdrawalDto)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_Update]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalID", WithdrawalID)
                cmd.Parameters.AddWithValue("@WithdrawalDate", dto.WithdrawalDate)
                cmd.Parameters.AddWithValue("@WarehouseID", dto.WarehouseID)
                cmd.Parameters.AddWithValue("@ShiftID", If(dto.ShiftID.HasValue, CObj(dto.ShiftID.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@ProductionLineID", If(dto.ProductionLineID.HasValue, CObj(dto.ProductionLineID.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(dto.Notes), CObj(DBNull.Value), dto.Notes))
                cmd.Parameters.AddWithValue("@ModifiedBy", dto.CreatedBy)

                Dim tvp = cmd.Parameters.AddWithValue("@Details", BuildDetailTvp(dto.Details))
                tvp.SqlDbType = SqlDbType.Structured
                tvp.TypeName = "[Production].[WithdrawalDetailType]"

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  Workflow Actions
    ' ═══════════════════════════════════════════════════════════
    Public Sub Submit(WithdrawalID As Long, UserID As Integer)
        ExecWorkflowSP("[Production].[usp_WithdrawalOrder_Submit]", WithdrawalID, UserID)
    End Sub

    Public Sub Receive(WithdrawalID As Long, UserID As Integer)
        ExecWorkflowSP("[Production].[usp_WithdrawalOrder_Receive]", WithdrawalID, UserID)
    End Sub

    Public Sub Approve(WithdrawalID As Long, UserID As Integer)
        ExecWorkflowSP("[Production].[usp_WithdrawalOrder_Approve]", WithdrawalID, UserID)
    End Sub

    Public Sub Delete(WithdrawalID As Long, UserID As Integer)
        ExecWorkflowSP("[Production].[usp_WithdrawalOrder_Delete]", WithdrawalID, UserID)
    End Sub

    Public Sub Reject(WithdrawalID As Long, Reason As String, UserID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_Reject]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalID", WithdrawalID)
                cmd.Parameters.AddWithValue("@RejectReason", Reason)
                cmd.Parameters.AddWithValue("@RejectedBy", UserID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Cancel(WithdrawalID As Long, Reason As String, UserID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_Cancel]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalID", WithdrawalID)
                cmd.Parameters.AddWithValue("@CancelReason", Reason)
                cmd.Parameters.AddWithValue("@CancelledBy", UserID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub ExecWorkflowSP(spName As String, WithdrawalID As Long, UserID As Integer)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand(spName, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalID", WithdrawalID)

                ' ابعته بأسماء صريحة لكل SP
                Select Case spName
                    Case "[Production].[usp_WithdrawalOrder_Submit]"
                        cmd.Parameters.AddWithValue("@SubmittedBy", UserID)
                    Case "[Production].[usp_WithdrawalOrder_Receive]"
                        cmd.Parameters.AddWithValue("@ReceivedBy", UserID)
                    Case "[Production].[usp_WithdrawalOrder_Approve]"
                        cmd.Parameters.AddWithValue("@ApprovedBy", UserID)
                    Case "[Production].[usp_WithdrawalOrder_Delete]"
                        cmd.Parameters.AddWithValue("@DeletedBy", UserID)
                    Case Else
                        cmd.Parameters.AddWithValue("@UserID", UserID)
                End Select

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    'Private Function GetUserParamName(spName As String) As String
    '    If spName.EndsWith("_Submit") Then Return "SubmittedBy"
    '    If spName.EndsWith("_Receive") Then Return "ReceivedBy"
    '    If spName.EndsWith("_Approve") Then Return "ApprovedBy"
    '    If spName.EndsWith("_Delete") Then Return "DeletedBy"
    '    Return "UserID"
    'End Function

    ' ═══════════════════════════════════════════════════════════
    '  Issue (الصرف من المخزن)
    ' ═══════════════════════════════════════════════════════════
    Public Sub Issue(dto As IssueWithdrawalDto)
        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_Issue]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalID", dto.WithdrawalID)
                cmd.Parameters.AddWithValue("@IssuedBy", dto.IssuedBy)

                Dim dt As New DataTable()
                dt.Columns.Add("ItemID", GetType(Integer))
                dt.Columns.Add("UnitID", GetType(Integer))
                dt.Columns.Add("RequestedQty", GetType(Decimal))
                dt.Columns.Add("IssuedQty", GetType(Decimal))
                dt.Columns.Add("Notes", GetType(String))

                For Each d In dto.Details
                    dt.Rows.Add(d.ItemID,
                                If(d.UnitID.HasValue, CObj(d.UnitID.Value), DBNull.Value),
                                d.RequestedQty,
                                d.IssuedQty,
                                If(String.IsNullOrEmpty(d.Notes), CObj(DBNull.Value), d.Notes))
                Next

                Dim tvp = cmd.Parameters.AddWithValue("@Details", dt)
                tvp.SqlDbType = SqlDbType.Structured
                tvp.TypeName = "[Production].[WithdrawalDetailType]"

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' ═══════════════════════════════════════════════════════════
    '  GetAll
    ' ═══════════════════════════════════════════════════════════
    Public Function GetAll(Optional fromDate As Date? = Nothing,
                           Optional toDate As Date? = Nothing,
                           Optional statusID As Integer? = Nothing,
                           Optional warehouseID As Integer? = Nothing) As List(Of WithdrawalOrderDto)

        Dim result As New List(Of WithdrawalOrderDto)

        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_GetAll]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@FromDate", If(fromDate.HasValue, CObj(fromDate.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@ToDate", If(toDate.HasValue, CObj(toDate.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@StatusID", If(statusID.HasValue, CObj(statusID.Value), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@WarehouseID", If(warehouseID.HasValue, CObj(warehouseID.Value), CObj(DBNull.Value)))

                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(MapHeader(rd))
                    End While
                End Using
            End Using
        End Using

        Return result
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  GetById
    ' ═══════════════════════════════════════════════════════════
    Public Function GetById(WithdrawalID As Long) As WithdrawalOrderDto
        Dim result As WithdrawalOrderDto = Nothing

        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_GetById]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalID", WithdrawalID)

                Using ds As New DataSet()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using

                    If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim r As DataRow = ds.Tables(0).Rows(0)
                        result = New WithdrawalOrderDto With {
                            .WithdrawalID = CLng(r("WithdrawalID")),
                            .WithdrawalNo = CStr(r("WithdrawalNo")),
                            .WithdrawalDate = CDate(r("WithdrawalDate")),
                            .WarehouseID = CInt(r("WarehouseID")),
                            .WarehouseName = CStr(r("WarehouseNameAr")),
                            .ShiftID = If(IsDBNull(r("ShiftID")), Nothing, CInt(r("ShiftID"))),
                            .ShiftName = If(IsDBNull(r("ShiftNameAr")), Nothing, CStr(r("ShiftNameAr"))),
                            .ProductionLineID = If(IsDBNull(r("ProductionLineID")), Nothing, CInt(r("ProductionLineID"))),
                            .StatusID = CInt(r("StatusID")),
                            .StatusCode = CStr(r("StatusCode")),
                            .StatusNameAr = CStr(r("StatusNameAr")),
                            .Notes = If(IsDBNull(r("Notes")), Nothing, CStr(r("Notes"))),
                            .SubmittedAt = If(IsDBNull(r("SubmittedAt")), Nothing, CDate(r("SubmittedAt"))),
                            .SubmittedBy = If(IsDBNull(r("SubmittedBy")), Nothing, CInt(r("SubmittedBy"))),
                            .IssuedAt = If(IsDBNull(r("IssuedAt")), Nothing, CDate(r("IssuedAt"))),
                            .IssuedBy = If(IsDBNull(r("IssuedBy")), Nothing, CInt(r("IssuedBy"))),
                            .ReceivedAt = If(IsDBNull(r("ReceivedAt")), Nothing, CDate(r("ReceivedAt"))),
                            .ReceivedBy = If(IsDBNull(r("ReceivedBy")), Nothing, CInt(r("ReceivedBy"))),
                            .ApprovedAt = If(IsDBNull(r("ApprovedAt")), Nothing, CDate(r("ApprovedAt"))),
                            .ApprovedBy = If(IsDBNull(r("ApprovedBy")), Nothing, CInt(r("ApprovedBy"))),
                            .RejectReason = If(IsDBNull(r("RejectReason")), Nothing, CStr(r("RejectReason"))),
                            .CancelReason = If(IsDBNull(r("CancelReason")), Nothing, CStr(r("CancelReason"))),
                            .CreatedBy = If(IsDBNull(r("CreatedBy")), Nothing, CInt(r("CreatedBy"))),
                            .CreatedAt = CDate(r("CreatedAt"))
                        }

                        If ds.Tables.Count > 1 Then
                            For Each dr As DataRow In ds.Tables(1).Rows
                                result.Details.Add(New WithdrawalOrderDetailDto With {
                                    .DetailID = CLng(dr("DetailID")),
                                    .WithdrawalID = result.WithdrawalID,
                                    .ItemID = CInt(dr("ItemID")),
                                    .ItemCode = CStr(dr("ItemCode")),
                                    .ItemName = CStr(dr("ItemNameAr")),
                                    .UnitID = If(IsDBNull(dr("UnitID")), Nothing, CInt(dr("UnitID"))),
                                    .UnitName = If(IsDBNull(dr("UnitNameAr")), Nothing, CStr(dr("UnitNameAr"))),
                                    .RequestedQty = CDec(dr("RequestedQty")),
                                    .IssuedQty = CDec(dr("IssuedQty")),
                                    .ReceivedQty = CDec(dr("ReceivedQty")),
                                    .Notes = If(IsDBNull(dr("Notes")), Nothing, CStr(dr("Notes")))
                                })
                            Next
                        End If
                    End If
                End Using
            End Using
        End Using

        Return result
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  GetHistory (Audit Log)
    ' ═══════════════════════════════════════════════════════════
    Public Function GetHistory(WithdrawalID As Long) As List(Of WithdrawalAuditLogDto)
        Dim result As New List(Of WithdrawalAuditLogDto)

        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand("[Production].[usp_WithdrawalOrder_GetHistory]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@WithdrawalID", WithdrawalID)

                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(New WithdrawalAuditLogDto With {
                            .LogID = CLng(rd("LogID")),
                            .WithdrawalID = WithdrawalID,
                            .Action = CStr(rd("Action")),
                            .FromStatusID = If(IsDBNull(rd("FromStatusID")), Nothing, CInt(rd("FromStatusID"))),
                            .FromStatusName = If(IsDBNull(rd("FromStatusName")), Nothing, CStr(rd("FromStatusName"))),
                            .ToStatusID = If(IsDBNull(rd("ToStatusID")), Nothing, CInt(rd("ToStatusID"))),
                            .ToStatusName = If(IsDBNull(rd("ToStatusName")), Nothing, CStr(rd("ToStatusName"))),
                            .UserID = CInt(rd("UserID")),
                            .UserName = If(IsDBNull(rd("UserName")), "", CStr(rd("UserName"))),
                            .Notes = If(IsDBNull(rd("Notes")), Nothing, CStr(rd("Notes"))),
                            .CreatedAt = CDate(rd("CreatedAt"))
                        })
                    End While
                End Using
            End Using
        End Using

        Return result
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Lookups
    ' ═══════════════════════════════════════════════════════════
    Public Function GetStatusLookup() As List(Of WithdrawalStatusLookup)
        Dim result As New List(Of WithdrawalStatusLookup)

        Using conn = DBConnection.GetConnection()
            Using cmd As New SqlCommand(
                "SELECT StatusID, StatusCode, StatusNameAr FROM [Production].[WithdrawalOrderStatus] ORDER BY DisplayOrder", conn)
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(New WithdrawalStatusLookup With {
                            .StatusID = CInt(rd("StatusID")),
                            .StatusCode = CStr(rd("StatusCode")),
                            .StatusNameAr = CStr(rd("StatusNameAr"))
                        })
                    End While
                End Using
            End Using
        End Using

        Return result
    End Function

    ' ═══════════════════════════════════════════════════════════
    '  Helpers
    ' ═══════════════════════════════════════════════════════════
    Private Function BuildDetailTvp(details As List(Of CreateWithdrawalDetailDto)) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ItemID", GetType(Integer))
        dt.Columns.Add("UnitID", GetType(Integer))
        dt.Columns.Add("RequestedQty", GetType(Decimal))
        dt.Columns.Add("IssuedQty", GetType(Decimal))
        dt.Columns.Add("Notes", GetType(String))

        For Each d In details
            dt.Rows.Add(d.ItemID,
                        If(d.UnitID.HasValue, CObj(d.UnitID.Value), DBNull.Value),
                        d.RequestedQty,
                        DBNull.Value,
                        If(String.IsNullOrEmpty(d.Notes), CObj(DBNull.Value), d.Notes))
        Next

        Return dt
    End Function

    Private Function MapHeader(rd As SqlDataReader) As WithdrawalOrderDto
        Dim dto As New WithdrawalOrderDto With {
            .WithdrawalID = CLng(rd("WithdrawalID")),
            .WithdrawalNo = CStr(rd("WithdrawalNo")),
            .WithdrawalDate = CDate(rd("WithdrawalDate")),
            .WarehouseID = CInt(rd("WarehouseID")),
            .WarehouseName = CStr(rd("WarehouseNameAr")),
            .ShiftID = If(IsDBNull(rd("ShiftID")), Nothing, CInt(rd("ShiftID"))),
            .ShiftName = If(IsDBNull(rd("ShiftNameAr")), Nothing, CStr(rd("ShiftNameAr"))),
            .StatusID = CInt(rd("StatusID")),
            .StatusCode = CStr(rd("StatusCode")),
            .StatusNameAr = CStr(rd("StatusNameAr")),
            .Notes = If(IsDBNull(rd("Notes")), Nothing, CStr(rd("Notes"))),
            .SubmittedAt = If(IsDBNull(rd("SubmittedAt")), Nothing, CDate(rd("SubmittedAt"))),
            .IssuedAt = If(IsDBNull(rd("IssuedAt")), Nothing, CDate(rd("IssuedAt"))),
            .ReceivedAt = If(IsDBNull(rd("ReceivedAt")), Nothing, CDate(rd("ReceivedAt"))),
            .ApprovedAt = If(IsDBNull(rd("ApprovedAt")), Nothing, CDate(rd("ApprovedAt"))),
            .CreatedAt = CDate(rd("CreatedAt")),
            .ItemsCount = CInt(rd("DetailsCount")),
            .TotalRequestedQty = CDec(rd("TotalRequestedQty")),
            .TotalIssuedQty = CDec(rd("TotalIssuedQty"))
        }
        Return dto
    End Function

End Class