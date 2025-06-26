using System.ComponentModel.DataAnnotations;

namespace AccountingPro.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
        
        [Required(ErrorMessage = "وصف الصنف مطلوب")]
        [StringLength(200, ErrorMessage = "وصف الصنف يجب ألا يتجاوز 200 حرف")]
        public string Description { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "الكمية يجب أن تكون أكبر من صفر")]
        public decimal Quantity { get; set; } = 1;
        
        [StringLength(20, ErrorMessage = "الوحدة يجب ألا تتجاوز 20 حرف")]
        public string? Unit { get; set; } = "قطعة";
        
        [Range(0.01, double.MaxValue, ErrorMessage = "سعر الوحدة يجب أن يكون أكبر من صفر")]
        public decimal UnitPrice { get; set; }
        
        public decimal DiscountPercentage { get; set; } = 0;
        
        public decimal DiscountAmount => (UnitPrice * Quantity) * (DiscountPercentage / 100);
        
        public decimal TotalPrice => (UnitPrice * Quantity) - DiscountAmount;
        
        [StringLength(500, ErrorMessage = "الملاحظات يجب ألا تتجاوز 500 حرف")]
        public string? Notes { get; set; }
    }
}

