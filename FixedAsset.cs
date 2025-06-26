using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    // الأصول الثابتة
    public class FixedAsset
    {
        [Key]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "رقم الأصل مطلوب.")]
        [StringLength(50, ErrorMessage = "يجب ألا يتجاوز رقم الأصل {1} حرفًا.")]
        public string AssetNumber { get; set; } = "";

        [Required(ErrorMessage = "اسم الأصل مطلوب.")]
        [StringLength(200, ErrorMessage = "يجب ألا يتجاوز اسم الأصل {1} حرفًا.")]
        public string AssetName { get; set; } = "";

        [StringLength(500, ErrorMessage = "يجب ألا يتجاوز الوصف {1} حرفًا.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "فئة الأصل مطلوبة.")]
        public AssetCategory Category { get; set; }

        [Required(ErrorMessage = "تاريخ الشراء مطلوب.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "تكلفة الشراء مطلوبة.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "تكلفة الشراء يجب أن تكون أكبر من الصفر.")]
        public decimal PurchaseCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalvageValue { get; set; } = 0; // القيمة المتبقية

        [Required(ErrorMessage = "العمر الإنتاجي مطلوب.")]
        [Range(1, 100, ErrorMessage = "العمر الإنتاجي يجب أن يكون بين 1 و 100 سنة.")]
        public int UsefulLifeYears { get; set; }

        [Required(ErrorMessage = "طريقة الاستهلاك مطلوبة.")]
        public DepreciationMethod DepreciationMethod { get; set; }

        [StringLength(200, ErrorMessage = "يجب ألا يتجاوز الموقع {1} حرفًا.")]
        public string? Location { get; set; }

        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز الرقم التسلسلي {1} حرفًا.")]
        public string? SerialNumber { get; set; }

        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز اسم المورد {1} حرفًا.")]
        public string? SupplierName { get; set; }

        public AssetStatus Status { get; set; } = AssetStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // الخصائص المحسوبة
        public decimal AccumulatedDepreciation => CalculateAccumulatedDepreciation();
        public decimal BookValue => PurchaseCost - AccumulatedDepreciation;
        public decimal AnnualDepreciation => CalculateAnnualDepreciation();

        // العلاقات
        public virtual ICollection<DepreciationEntry> DepreciationEntries { get; set; } = new List<DepreciationEntry>();

        // الطرق
        private decimal CalculateAnnualDepreciation()
        {
            return DepreciationMethod switch
            {
                DepreciationMethod.StraightLine => (PurchaseCost - SalvageValue) / UsefulLifeYears,
                DepreciationMethod.DoubleDeclining => BookValue * (2m / UsefulLifeYears),
                _ => 0
            };
        }

        private decimal CalculateAccumulatedDepreciation()
        {
            var yearsElapsed = (decimal)((DateTime.Now - PurchaseDate).Days / 365.25);
            var depreciation = Math.Min(yearsElapsed, (decimal)UsefulLifeYears) * AnnualDepreciation;
            return Math.Min(depreciation, PurchaseCost - SalvageValue);
        }
    }

    // قيود الاستهلاك
    public class DepreciationEntry
    {
        [Key]
        public int EntryId { get; set; }

        [Required]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "تاريخ القيد مطلوب.")]
        public DateTime EntryDate { get; set; }

        [Required(ErrorMessage = "مبلغ الاستهلاك مطلوب.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DepreciationAmount { get; set; }

        [StringLength(500, ErrorMessage = "يجب ألا يتجاوز الوصف {1} حرفًا.")]
        public string? Description { get; set; }

        public bool IsReversed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // العلاقات
        public virtual FixedAsset Asset { get; set; } = null!;
    }

    public enum AssetStatus
    {
        Active = 1,      // نشط
        Inactive = 2,    // غير نشط
        Disposed = 3,    // تم التخلص منه
        UnderMaintenance = 4  // تحت الصيانة
    }

    public enum AssetCategory
    {
        Equipment = 1,   // معدات
        Furniture = 2,   // أثاث
        Vehicles = 3,    // مركبات
        Buildings = 4,   // مباني
        Technology = 5,  // تقنية
        Other = 6        // أخرى
    }

    public enum DepreciationMethod
    {
        StraightLine = 1,        // القسط الثابت
        DoubleDeclining = 2,     // القسط المتناقص المضاعف
        UnitsOfProduction = 3    // وحدات الإنتاج
    }

    public enum PayrollStatus
    {
        Draft = 1,       // مسودة
        Approved = 2,    // معتمد
        Paid = 3,        // مدفوع
        Cancelled = 4    // ملغي
    }

    public enum LeaveType
    {
        Annual = 1,      // سنوية
        Sick = 2,        // مرضية
        Emergency = 3,   // طارئة
        Maternity = 4,   // أمومة
        Paternity = 5,   // أبوة
        Unpaid = 6       // بدون راتب
    }

    public enum LeaveStatus
    {
        Pending = 1,     // في الانتظار
        Approved = 2,    // معتمدة
        Rejected = 3,    // مرفوضة
        Cancelled = 4    // ملغية
    }
}

