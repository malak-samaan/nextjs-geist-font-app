using System.ComponentModel.DataAnnotations;

namespace AccountingPro.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "رقم الفاتورة مطلوب")]
        [StringLength(50, ErrorMessage = "رقم الفاتورة يجب ألا يتجاوز 50 حرف")]
        public string InvoiceNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "تاريخ الفاتورة مطلوب")]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        
        public DateTime? DueDate { get; set; }
        
        public InvoiceType Type { get; set; }
        
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
        
        // Customer or Supplier
        public int? CustomerId { get; set; }
        
        public int? SupplierId { get; set; }
        
        // Foreign Keys
        public int? ProjectId { get; set; }
        
        [StringLength(500, ErrorMessage = "الوصف يجب ألا يتجاوز 500 حرف")]
        public string? Description { get; set; }
        
        public decimal SubTotal { get; set; }
        
        public decimal TaxRate { get; set; } = 15; // VAT 15%
        
        public decimal TaxAmount { get; set; }
        
        public decimal DiscountAmount { get; set; } = 0;
        
        public decimal TotalAmount { get; set; }
        
        public decimal PaidAmount { get; set; } = 0;
        
        public decimal RemainingAmount => TotalAmount - PaidAmount;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? LastModified { get; set; }
        
        [StringLength(500, ErrorMessage = "الملاحظات يجب ألا تتجاوز 500 حرف")]
        public string? Notes { get; set; }
        
        // Navigation Properties
        public virtual Customer? Customer { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Project? Project { get; set; }
        public List<InvoiceItem> Items { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();
    }
    
    public enum InvoiceType
    {
        Sales = 1,      // فاتورة بيع
        Purchase = 2    // فاتورة شراء
    }
    
    public enum InvoiceStatus
    {
        Draft = 1,      // مسودة
        Sent = 2,       // مرسلة
        Paid = 3,       // مدفوعة
        Overdue = 4,    // متأخرة
        Cancelled = 5   // ملغية
    }
}

