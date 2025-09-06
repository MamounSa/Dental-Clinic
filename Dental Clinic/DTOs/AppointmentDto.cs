using System.ComponentModel.DataAnnotations;


/// <summary>
/// DTO for displaying appointment information.
/// </summary>
public class AppointmentDto
{
    public int Id { get; set; }

    public DateTime Start { get; set; }  // بداية الموعد
    public DateTime End { get; set; }    // نهاية الموعد

    public string Status { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }
}

/// <summary>
/// DTO for creating a new appointment.
/// </summary>
public class CreateAppointmentDto
{
    [Required]
    public DateTime Start { get; set; }  // بداية الموعد

    [Required]
    public DateTime End { get; set; }    // نهاية الموعد

    [Required]
    public string Status { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int PatientId { get; set; }
}

/// <summary>
/// DTO for updating an existing appointment.
/// </summary>
public class UpdateAppointmentDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime Start { get; set; }  // بداية الموعد

    [Required]
    public DateTime End { get; set; }    // نهاية الموعد

    [Required]
    public string Status { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int PatientId { get; set; }
}
public class AppointmentsReportDto
{
    public int TotalAppointments { get; set; }
    public int Completed { get; set; }
    public int Cancelled { get; set; }
    public int NoShows { get; set; }
    public string? DoctorName { get; set; }
    public string? DateRange { get; set; }
}

public class WeeklyCalendarDto
{
    public DateTime WeekStart { get; set; }
    public DateTime WeekEnd { get; set; }
    public Dictionary<DateTime, List<AppointmentDto>> AppointmentsByDay { get; set; }
}

public class AppointmentFilterDto
{
    public int? DoctorId { get; set; }
    public int? PatientId { get; set; }
    public string? Status { get; set; }

    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public TimeSpan? FromTime { get; set; }
    public TimeSpan? ToTime { get; set; }

    public string? SortBy { get; set; }
    public bool Desc { get; set; } = false;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class UpdateAttendanceDto
{
    public int AppointmentId { get; set; }
    public AttendanceStatus AttendanceStatus { get; set; }
}