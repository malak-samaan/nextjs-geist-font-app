using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "الشهر يجب أن يكون بين 1 و 12")]
        public int Month { get; set; }

        [Required]
        [Range(2020, 2050, ErrorMessage = "السنة غير صحيحة")]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "الراتب الأساسي يجب أن يكون أكبر من الصفر")]
        public decimal BaseSalary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Allowances { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Overtime { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonus { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossSalary { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSalary { get; set; } = 0;

        public SalaryStatus Status { get; set; } = SalaryStatus.Pending;

        public DateTime? PaymentDate { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }

        // Navigation Properties
        public virtual Employee Employee { get; set; } = null!;
        public virtual User? CreatedByUser { get; set; }
        public virtual User? ApprovedByUser { get; set; }

        // Calculated Properties
        public string StatusText => Status switch
        {
            SalaryStatus.Pending => "معلق",
            SalaryStatus.Approved => "موافق عليه",
            SalaryStatus.Paid => "مدفوع",
            SalaryStatus.Rejected => "مرفوض",
            _ => "غير محدد"
        };

        public string MonthYearText => $"{GetMonthName(Month)} {Year}";

        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "يناير",
                2 => "فبراير",
                3 => "مارس",
                4 => "أبريل",
                5 => "مايو",
                6 => "يونيو",
                7 => "يوليو",
                8 => "أغسطس",
                9 => "سبتمبر",
                10 => "أكتوبر",
                11 => "نوفمبر",
                12 => "ديسمبر",
                _ => "غير محدد"
            };
        }
    }

    public enum SalaryStatus
    {
        Pending = 1,    // معلق
        Approved = 2,   // موافق عليه
        Paid = 3,       // مدفوع
        Rejected = 4    // مرفوض
    }
}

