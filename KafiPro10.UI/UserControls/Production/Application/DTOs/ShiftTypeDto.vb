Namespace KafiPro.Production.Application.DTOs

    Public Class ShiftTypeDto
        Public Property ShiftTypeID As Integer
        Public Property ShiftCode As String
        Public Property ShiftNameAr As String
        Public Property ShiftNameEn As String
        Public Property StartTime As TimeSpan?
        Public Property EndTime As TimeSpan?
        Public Property IsOvertime As Boolean
        Public Property IsActive As Boolean
    End Class

End Namespace