using System.ComponentModel.DataAnnotations;

namespace AccountingPro.Models
{
    // نموذج المشروع - Project Model
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "اسم المشروع مطلوب.")]
        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز اسم المشروع {1} حرفًا.")]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم المشروع مطلوب.")]
        [StringLength(50, ErrorMessage = "يجب ألا يتجاوز رقم المشروع {1} حرفًا.")]
        public string ProjectNumber { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "تاريخ البداية مطلوب.")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        [Required(ErrorMessage = "الميزانية المخططة مطلوبة.")]
        [Range(0, double.MaxValue, ErrorMessage = "الميزانية يجب أن تكون أكبر من الصفر.")]
        public decimal PlannedBudget { get; set; }

        public decimal ActualCost { get; set; } = 0;

        public decimal RemainingBudget => PlannedBudget - ActualCost;

        [Range(0, 100, ErrorMessage = "نسبة الإنجاز يجب أن تكون بين 0 و 100.")]
        public decimal CompletionPercentage { get; set; } = 0;

        public ProjectStatus Status { get; set; } = ProjectStatus.Planning;

        public ProjectPriority Priority { get; set; } = ProjectPriority.Medium;

        // معلومات العميل
        public int? CustomerId { get; set; }

        // معلومات مدير المشروع
        public int? ProjectManagerId { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual Customer? Customer { get; set; }
        public virtual User? ProjectManager { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
        public virtual ICollection<ProjectExpense> Expenses { get; set; } = new List<ProjectExpense>();
        public virtual ICollection<SubcontractorContract> SubcontractorContracts { get; set; } = new List<SubcontractorContract>();

        // Calculated Properties
        public string StatusText => Status switch
        {
            ProjectStatus.Planning => "التخطيط",
            ProjectStatus.InProgress => "قيد التنفيذ",
            ProjectStatus.OnHold => "متوقف مؤقتاً",
            ProjectStatus.Completed => "مكتمل",
            ProjectStatus.Cancelled => "ملغي",
            _ => "غير محدد"
        };

        public string PriorityText => Priority switch
        {
            ProjectPriority.Low => "منخفضة",
            ProjectPriority.Medium => "متوسطة",
            ProjectPriority.High => "عالية",
            ProjectPriority.Critical => "حرجة",
            _ => "غير محدد"
        };

        public bool IsOverBudget => ActualCost > PlannedBudget;
        public bool IsOverdue => EndDate.HasValue && DateTime.Now > EndDate.Value && Status != ProjectStatus.Completed;
        public TimeSpan? Duration => EndDate.HasValue ? EndDate.Value - StartDate : null;
        public TimeSpan? ActualDuration => ActualEndDate.HasValue ? ActualEndDate.Value - StartDate : DateTime.Now - StartDate;
    }

    // نموذج مهمة المشروع - Project Task Model
    public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "اسم المهمة مطلوب.")]
        [StringLength(200, ErrorMessage = "يجب ألا يتجاوز اسم المهمة {1} حرفًا.")]
        public string TaskName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        [Range(0, 100)]
        public decimal CompletionPercentage { get; set; } = 0;

        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public int? AssignedToUserId { get; set; }
        public decimal EstimatedHours { get; set; } = 0;
        public decimal ActualHours { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual Project Project { get; set; } = null!;
        public virtual User? AssignedToUser { get; set; }
        public virtual User? CreatedByUser { get; set; }
    }

    // نموذج مصروفات المشروع - Project Expense Model
    public class ProjectExpense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "وصف المصروف مطلوب.")]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "المبلغ مطلوب.")]
        [Range(0, double.MaxValue, ErrorMessage = "المبلغ يجب أن يكون أكبر من الصفر.")]
        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        public ExpenseCategory Category { get; set; } = ExpenseCategory.Other;

        [StringLength(100)]
        public string? Vendor { get; set; }

        [StringLength(50)]
        public string? ReceiptNumber { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public bool IsApproved { get; set; } = false;
        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual Project Project { get; set; } = null!;
        public virtual User? ApprovedByUser { get; set; }
        public virtual User? CreatedByUser { get; set; }
    }

    // Enums
    public enum ProjectStatus
    {
        Planning = 1,       // التخطيط
        InProgress = 2,     // قيد التنفيذ
        OnHold = 3,         // متوقف مؤقتاً
        Completed = 4,      // مكتمل
        Cancelled = 5       // ملغي
    }

    public enum ProjectPriority
    {
        Low = 1,        // منخفضة
        Medium = 2,     // متوسطة
        High = 3,       // عالية
        Critical = 4    // حرجة
    }

    public enum TaskStatus
    {
        NotStarted = 1,     // لم تبدأ
        InProgress = 2,     // قيد التنفيذ
        Completed = 3,      // مكتملة
        OnHold = 4,         // متوقفة مؤقتاً
        Cancelled = 5       // ملغية
    }

    public enum TaskPriority
    {
        Low = 1,        // منخفضة
        Medium = 2,     // متوسطة
        High = 3,       // عالية
        Critical = 4    // حرجة
    }

    public enum ExpenseCategory
    {
        Materials = 1,      // مواد
        Labor = 2,          // عمالة
        Equipment = 3,      // معدات
        Transportation = 4, // نقل
        Utilities = 5,      // مرافق
        Professional = 6,   // خدمات مهنية
        Other = 7          // أخرى
    }
}

