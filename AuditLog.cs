using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    // نموذج السجل الزمني للأحداث - Audit Log Model
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "نوع العملية مطلوب.")]
        public AuditAction Action { get; set; }

        [Required(ErrorMessage = "اسم الجدول مطلوب.")]
        [StringLength(100)]
        public string TableName { get; set; } = string.Empty;

        public int? RecordId { get; set; }

        [StringLength(200)]
        public string? RecordDescription { get; set; }

        [Column(TypeName = "ntext")]
        public string? OldValues { get; set; }

        [Column(TypeName = "ntext")]
        public string? NewValues { get; set; }

        [Column(TypeName = "ntext")]
        public string? ChangedFields { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [StringLength(45)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        [StringLength(1000)]
        public string? AdditionalInfo { get; set; }

        public AuditSeverity Severity { get; set; } = AuditSeverity.Information;

        [StringLength(100)]
        public string? Module { get; set; }

        [StringLength(100)]
        public string? Feature { get; set; }

        public bool IsSuccessful { get; set; } = true;

        [StringLength(500)]
        public string? ErrorMessage { get; set; }

        // Navigation Properties
        public virtual User User { get; set; } = null!;

        // Calculated Properties
        public string ActionText => Action switch
        {
            AuditAction.Create => "إنشاء",
            AuditAction.Read => "عرض",
            AuditAction.Update => "تحديث",
            AuditAction.Delete => "حذف",
            AuditAction.Login => "تسجيل دخول",
            AuditAction.Logout => "تسجيل خروج",
            AuditAction.Export => "تصدير",
            AuditAction.Import => "استيراد",
            AuditAction.Print => "طباعة",
            AuditAction.Approve => "موافقة",
            AuditAction.Reject => "رفض",
            AuditAction.Cancel => "إلغاء",
            AuditAction.Restore => "استرداد",
            AuditAction.Archive => "أرشفة",
            AuditAction.Configure => "إعداد",
            AuditAction.Backup => "نسخ احتياطي",
            AuditAction.Restore_Backup => "استرداد نسخة احتياطية",
            _ => "غير محدد"
        };

        public string SeverityText => Severity switch
        {
            AuditSeverity.Information => "معلومات",
            AuditSeverity.Warning => "تحذير",
            AuditSeverity.Error => "خطأ",
            AuditSeverity.Critical => "حرج",
            _ => "غير محدد"
        };

        public string SeverityColor => Severity switch
        {
            AuditSeverity.Information => "text-info",
            AuditSeverity.Warning => "text-warning",
            AuditSeverity.Error => "text-danger",
            AuditSeverity.Critical => "text-danger fw-bold",
            _ => "text-muted"
        };
    }

    // أنواع العمليات في السجل الزمني
    public enum AuditAction
    {
        Create = 1,           // إنشاء
        Read = 2,             // عرض
        Update = 3,           // تحديث
        Delete = 4,           // حذف
        Login = 5,            // تسجيل دخول
        Logout = 6,           // تسجيل خروج
        Export = 7,           // تصدير
        Import = 8,           // استيراد
        Print = 9,            // طباعة
        Approve = 10,         // موافقة
        Reject = 11,          // رفض
        Cancel = 12,          // إلغاء
        Restore = 13,         // استرداد
        Archive = 14,         // أرشفة
        Configure = 15,       // إعداد
        Backup = 16,          // نسخ احتياطي
        Restore_Backup = 17   // استرداد نسخة احتياطية
    }

    // مستويات الخطورة في السجل الزمني
    public enum AuditSeverity
    {
        Information = 1,      // معلومات
        Warning = 2,          // تحذير
        Error = 3,            // خطأ
        Critical = 4          // حرج
    }

    // نموذج إعدادات النظام - System Settings Model
    public class SystemSetting
    {
        [Key]
        public int SettingId { get; set; }

        [Required(ErrorMessage = "مفتاح الإعداد مطلوب.")]
        [StringLength(100)]
        public string SettingKey { get; set; } = string.Empty;

        [Required(ErrorMessage = "قيمة الإعداد مطلوبة.")]
        [Column(TypeName = "ntext")]
        public string SettingValue { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        [StringLength(50)]
        public string? DataType { get; set; } = "string";

        public bool IsSystemSetting { get; set; } = false;
        public bool IsEncrypted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public int? ModifiedByUserId { get; set; }

        // Navigation Properties
        public virtual User? ModifiedByUser { get; set; }
    }

    // نموذج النسخ الاحتياطية - Backup Model
    public class Backup
    {
        [Key]
        public int BackupId { get; set; }

        [Required(ErrorMessage = "اسم النسخة الاحتياطية مطلوب.")]
        [StringLength(200)]
        public string BackupName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime BackupDate { get; set; } = DateTime.Now;

        [Required]
        public BackupType BackupType { get; set; }

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [Column(TypeName = "bigint")]
        public long FileSize { get; set; }

        public bool IsCompressed { get; set; } = true;
        public bool IsEncrypted { get; set; } = false;

        [Required]
        public BackupStatus Status { get; set; } = BackupStatus.InProgress;

        public DateTime? CompletedDate { get; set; }

        [StringLength(500)]
        public string? ErrorMessage { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual User CreatedByUser { get; set; } = null!;

        // Calculated Properties
        public string FileSizeFormatted
        {
            get
            {
                if (FileSize < 1024) return $"{FileSize} بايت";
                if (FileSize < 1024 * 1024) return $"{FileSize / 1024:F1} كيلوبايت";
                if (FileSize < 1024 * 1024 * 1024) return $"{FileSize / (1024 * 1024):F1} ميجابايت";
                return $"{FileSize / (1024 * 1024 * 1024):F1} جيجابايت";
            }
        }
        public string BackupTypeText => BackupType switch
        {
            BackupType.Manual => "نسخة يدوية",
            BackupType.Automatic => "نسخة تلقائية",
            BackupType.Scheduled => "نسخة مجدولة",
            BackupType.Restore => "استعادة",
            _ => "غير محدد"
        };

        public string StatusText => Status switch
        {
            BackupStatus.InProgress => "قيد التنفيذ",
            BackupStatus.Completed => "مكتملة",
            BackupStatus.Failed => "فشلت",
            BackupStatus.Cancelled => "ملغية",
            _ => "غير محدد"
        };

        public string StatusColor => Status switch
        {
            BackupStatus.InProgress => "text-warning",
            BackupStatus.Completed => "text-success",
            BackupStatus.Failed => "text-danger",
            BackupStatus.Cancelled => "text-secondary",
            _ => "text-muted"
        };
    }

    // أنواع النسخ الاحتياطية


}

