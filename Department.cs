using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "اسم القسم مطلوب")]
        [StringLength(100, ErrorMessage = "اسم القسم يجب ألا يتجاوز {1} حرفاً")]
        public string Name { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "كود القسم يجب ألا يتجاوز {1} أحرف")]
        public string? Code { get; set; }

        [StringLength(500, ErrorMessage = "الوصف يجب ألا يتجاوز {1} حرفاً")]
        public string? Description { get; set; }

        public int? ManagerId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "الميزانية يجب أن تكون أكبر من أو تساوي الصفر")]
        public decimal Budget { get; set; } = 0;

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [StringLength(100)]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public int? CreatedByUserId { get; set; }

        // Navigation Properties
        public virtual Employee? Manager { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // Calculated Properties
        public int EmployeeCount => Employees?.Count ?? 0;
        public decimal TotalSalaries => Employees?.Sum(e => e.BaseSalary) ?? 0;
        public string StatusText => IsActive ? "نشط" : "غير نشط";
    }
}

