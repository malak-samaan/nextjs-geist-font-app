using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "رقم الموظف مطلوب")]
        [StringLength(50, ErrorMessage = "رقم الموظف يجب ألا يتجاوز {1} حرفاً")]
        public string EmployeeNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهوية مطلوب")]
        [StringLength(20, ErrorMessage = "رقم الهوية يجب ألا يتجاوز {1} حرفاً")]
        public string NationalId { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم الأول يجب ألا يتجاوز {1} حرفاً")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم الأخير مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم الأخير يجب ألا يتجاوز {1} حرفاً")]
        public string LastName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "الهاتف مطلوب")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "القسم مطلوب")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "المنصب مطلوب")]
        [StringLength(100)]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ التوظيف مطلوب")]
        public DateTime HireDate { get; set; } = DateTime.Now;

        public DateTime? TerminationDate { get; set; }

        [Required(ErrorMessage = "الراتب الأساسي مطلوب")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "الراتب الأساسي يجب أن يكون أكبر من الصفر")]
        public decimal BaseSalary { get; set; }

        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

        [StringLength(50)]
        public string? BankAccountNumber { get; set; }

        [StringLength(50)]
        public string? BankName { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; } = null!;
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        // Calculated Properties
        public string FullName => $"{FirstName} {LastName}";

        public string StatusText => Status switch
        {
            EmployeeStatus.Active => "نشط",
            EmployeeStatus.OnLeave => "في إجازة",
            EmployeeStatus.Suspended => "موقوف",
            EmployeeStatus.Resigned => "مستقيل",
            _ => "غير محدد"
        };

        public int YearsOfService => DateTime.Now.Year - HireDate.Year;
        public bool IsOnProbation => DateTime.Now.Subtract(HireDate).Days <= 90;
    }

    public enum EmployeeStatus
    {
        Active = 1,     // نشط
        OnLeave = 2,    // في إجازة
        Suspended = 3,  // موقوف
        Resigned = 4    // مستقيل
    }
}

