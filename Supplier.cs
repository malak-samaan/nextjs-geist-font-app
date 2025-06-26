using System.ComponentModel.DataAnnotations;

namespace AccountingPro.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "اسم المورد مطلوب")]
        [StringLength(100, ErrorMessage = "اسم المورد يجب ألا يتجاوز 100 حرف")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "اسم الشركة يجب ألا يتجاوز 100 حرف")]
        public string? CompanyName { get; set; }
        
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [StringLength(100, ErrorMessage = "البريد الإلكتروني يجب ألا يتجاوز 100 حرف")]
        public string? Email { get; set; }
        
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        [StringLength(20, ErrorMessage = "رقم الهاتف يجب ألا يتجاوز 20 رقم")]
        public string? Phone { get; set; }
        
        [StringLength(20, ErrorMessage = "رقم الجوال يجب ألا يتجاوز 20 رقم")]
        public string? Mobile { get; set; }
        
        [StringLength(200, ErrorMessage = "العنوان يجب ألا يتجاوز 200 حرف")]
        public string? Address { get; set; }
        
        [StringLength(50, ErrorMessage = "المدينة يجب ألا يتجاوز 50 حرف")]
        public string? City { get; set; }
        
        [StringLength(20, ErrorMessage = "الرمز البريدي يجب ألا يتجاوز 20 رقم")]
        public string? PostalCode { get; set; }
        
        [StringLength(50, ErrorMessage = "الدولة يجب ألا يتجاوز 50 حرف")]
        public string? Country { get; set; } = "المملكة العربية السعودية";
        
        [StringLength(20, ErrorMessage = "الرقم الضريبي يجب ألا يتجاوز 20 رقم")]
        public string? TaxNumber { get; set; }
        
        [StringLength(100, ErrorMessage = "نوع النشاط يجب ألا يتجاوز 100 حرف")]
        public string? BusinessType { get; set; }
        
        public decimal CreditLimit { get; set; } = 0;
        
        public decimal CurrentBalance { get; set; } = 0;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? LastModified { get; set; }
        
        [StringLength(500, ErrorMessage = "الملاحظات يجب ألا تتجاوز 500 حرف")]
        public string? Notes { get; set; }
        
        // Navigation Properties
        public List<Invoice> PurchaseInvoices { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();
    }
}

