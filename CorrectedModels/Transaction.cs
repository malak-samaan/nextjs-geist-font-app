using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingPro.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "رقم المعاملة مطلوب")]
        [StringLength(50, ErrorMessage = "رقم المعاملة يجب ألا يتجاوز 50 حرف")]
        public string TransactionNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "تاريخ المعاملة مطلوب")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "وصف المعاملة مطلوب")]
        [StringLength(200, ErrorMessage = "وصف المعاملة يجب ألا يتجاوز 200 حرف")]
        public string Description { get; set; } = string.Empty;
        
        public TransactionType Type { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "المبلغ يجب أن يكون أكبر من صفر")]
        public decimal Amount { get; set; }
        
        // Account References
        public int DebitAccountId { get; set; }
        public Account DebitAccount { get; set; } = null!;
        
        public int CreditAccountId { get; set; }
        public Account CreditAccount { get; set; } = null!;
        
        // Optional References
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        
        public int? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        
        [StringLength(50, ErrorMessage = "رقم المرجع يجب ألا يتجاوز 50 حرف")]
        public string? ReferenceNumber { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? LastModified { get; set; }
        
        [StringLength(500, ErrorMessage = "الملاحظات يجب ألا تتجاوز 500 حرف")]
        public string? Notes { get; set; }
    }
    
    public enum TransactionType
    {
        Receipt = 1,        // قبض
        Payment = 2,        // دفع
        SalesInvoice = 3,   // فاتورة بيع
        PurchaseInvoice = 4, // فاتورة شراء
        JournalEntry = 5    // قيد يومية
    }
}
