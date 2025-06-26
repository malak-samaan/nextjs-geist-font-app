using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;

        public DateTime Date { get; set; }

        public TimeOnly? CheckInTime { get; set; }

        public TimeOnly? CheckOutTime { get; set; }

        public TimeSpan? WorkingHours { get; set; }

        public TimeSpan? OvertimeHours { get; set; }

        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }

        // Calculated Properties
        public string StatusText => Status switch
        {
            AttendanceStatus.Present => "حاضر",
            AttendanceStatus.Absent => "غائب",
            AttendanceStatus.Late => "متأخر",
            AttendanceStatus.EarlyLeave => "انصراف مبكر",
            AttendanceStatus.Holiday => "عطلة",
            AttendanceStatus.Sick => "مرضي",
            AttendanceStatus.Vacation => "إجازة",
            AttendanceStatus.OnLeave => "في إجازة",
            _ => "غير محدد"
        };

        public decimal TotalHours => (decimal)(WorkingHours?.TotalHours + (OvertimeHours?.TotalHours ?? 0) ?? 0);
    }

    public enum AttendanceStatus
    {
        Present = 1,
        Absent = 2,
        Late = 3,
        EarlyLeave = 4,
        Holiday = 5,
        Sick = 6,
        Vacation = 7,
        OnLeave = 8
    }
}

