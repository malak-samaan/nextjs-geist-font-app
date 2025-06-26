using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    public class BackupRestore
    {
        [Key]
        public int BackupId { get; set; }

        [Required]
        [StringLength(200)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; } = string.Empty;

        public BackupType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FileSize { get; set; } // in bytes

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; }

        public BackupStatus Status { get; set; } = BackupStatus.InProgress;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsAutomatic { get; set; } = false;

        public DateTime? RestoredAt { get; set; }

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        // Calculated Properties
        public string TypeText => Type switch
        {
            BackupType.Manual => "يدوي",
            BackupType.Automatic => "تلقائي",
            BackupType.Scheduled => "مجدول",
            BackupType.Restore => "استعادة",
            _ => "غير محدد"
        };

        public string StatusText => Status switch
        {
            BackupStatus.InProgress => "قيد التنفيذ",
            BackupStatus.Completed => "مكتمل",
            BackupStatus.Failed => "فشل",
            BackupStatus.Cancelled => "ملغي",
            _ => "غير محدد"
        };
    }

    public enum BackupType
    {
        Manual = 1,
        Automatic = 2,
        Scheduled = 3,
        Restore = 4
    }

    public enum BackupStatus
    {
        InProgress = 1,
        Completed = 2,
        Failed = 3,
        Cancelled = 4
    }
}

