using System.ComponentModel.DataAnnotations;

namespace AccountingPro.Models
{
    // نموذج مقاول الباطن - Subcontractor Model
    public class Subcontractor
    {
        [Key]
        public int SubcontractorId { get; set; }

        [Required(ErrorMessage = "اسم مقاول الباطن مطلوب.")]
        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز الاسم {1} حرفًا.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب.")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Mobile { get; set; }

        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح.")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(50)]
        public string? Country { get; set; } = "المملكة العربية السعودية";

        // معلومات الترخيص والتأهيل
        [StringLength(50)]
        public string? LicenseNumber { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        [StringLength(100)]
        public string? Specialization { get; set; }

        [StringLength(50)]
        public string? TaxNumber { get; set; }

        [StringLength(50)]
        public string? CommercialRegister { get; set; }

        // معلومات التقييم
        [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5.")]
        public int Rating { get; set; } = 3;

        [StringLength(1000)]
        public string? Notes { get; set; }

        public SubcontractorStatus Status { get; set; } = SubcontractorStatus.Active;

        // معلومات مالية
        public decimal TotalContractValue { get; set; } = 0;
        public decimal TotalPaidAmount { get; set; } = 0;
        public decimal RemainingAmount => TotalContractValue - TotalPaidAmount;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<SubcontractorContract> Contracts { get; set; } = new List<SubcontractorContract>();
        public virtual ICollection<SubcontractorPayment> Payments { get; set; } = new List<SubcontractorPayment>();

        // Calculated Properties
        public string StatusText => Status switch
        {
            SubcontractorStatus.Active => "نشط",
            SubcontractorStatus.Inactive => "غير نشط",
            SubcontractorStatus.Suspended => "موقوف",
            SubcontractorStatus.Blacklisted => "محظور",
            _ => "غير محدد"
        };

        public string RatingText => Rating switch
        {
            1 => "ضعيف جداً",
            2 => "ضعيف",
            3 => "متوسط",
            4 => "جيد",
            5 => "ممتاز",
            _ => "غير مقيم"
        };

        public bool HasActiveContracts => Contracts.Any(c => c.Status == ContractStatus.Active);
        public bool IsLicenseExpired => LicenseExpiryDate.HasValue && LicenseExpiryDate.Value < DateTime.Now;
    }

    // نموذج عقد مقاول الباطن - Subcontractor Contract Model
    public class SubcontractorContract
    {
        [Key]
        public int ContractId { get; set; }

        [Required]
        public int SubcontractorId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "رقم العقد مطلوب.")]
        [StringLength(50)]
        public string ContractNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "وصف العقد مطلوب.")]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "قيمة العقد مطلوبة.")]
        [Range(0, double.MaxValue, ErrorMessage = "قيمة العقد يجب أن تكون أكبر من الصفر.")]
        public decimal ContractValue { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        public ContractStatus Status { get; set; } = ContractStatus.Draft;

        [StringLength(1000)]
        public string? Terms { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        // معلومات الدفع
        public decimal PaidAmount { get; set; } = 0;
        public decimal RemainingAmount => ContractValue - PaidAmount;
        public PaymentTerms PaymentTerms { get; set; } = PaymentTerms.Net30;

        // معلومات الإنجاز
        [Range(0, 100)]
        public decimal CompletionPercentage { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual Subcontractor Subcontractor { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<SubcontractorPayment> Payments { get; set; } = new List<SubcontractorPayment>();

        // Calculated Properties
        public bool IsOverdue => EndDate < DateTime.Now && Status != ContractStatus.Completed;
        public TimeSpan Duration => EndDate - StartDate;
        public TimeSpan? ActualDuration => ActualEndDate.HasValue ? ActualEndDate.Value - StartDate : DateTime.Now - StartDate;
    }

    // نموذج دفعة مقاول الباطن - Subcontractor Payment Model
    public class SubcontractorPayment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int SubcontractorId { get; set; }

        [Required]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "مبلغ الدفعة مطلوب.")]
        [Range(0, double.MaxValue, ErrorMessage = "مبلغ الدفعة يجب أن يكون أكبر من الصفر.")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [StringLength(100)]
        public string? ReferenceNumber { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public bool IsApproved { get; set; } = false;
        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual Subcontractor Subcontractor { get; set; } = null!;
        public virtual SubcontractorContract Contract { get; set; } = null!;
        public virtual User? ApprovedByUser { get; set; }
        public virtual User? CreatedByUser { get; set; }
    }

    // Enums
    public enum SubcontractorStatus
    {
        Active = 1,         // نشط
        Inactive = 2,       // غير نشط
        Suspended = 3,      // موقوف
        Blacklisted = 4     // محظور
    }

    public enum ContractStatus
    {
        Draft = 1,          // مسودة
        Active = 2,         // نشط
        Completed = 3,      // مكتمل
        Terminated = 4,     // منتهي
        Cancelled = 5       // ملغي
    }

    public enum PaymentTerms
    {
        Immediate = 1,      // فوري
        Net15 = 2,          // خلال 15 يوم
        Net30 = 3,          // خلال 30 يوم
        Net60 = 4,          // خلال 60 يوم
        Net90 = 5           // خلال 90 يوم
    }

    public enum PaymentStatus
    {
        Pending = 1,        // معلق
        Approved = 2,       // موافق عليه
        Paid = 3,           // مدفوع
        Rejected = 4,       // مرفوض
        Cancelled = 5       // ملغي
    }
}

