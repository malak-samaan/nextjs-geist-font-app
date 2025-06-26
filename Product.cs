using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    // نموذج المنتج - Product Model
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "رمز المنتج مطلوب.")]
        [StringLength(50, ErrorMessage = "يجب ألا يتجاوز رمز المنتج {1} حرفًا.")]
        public string ProductCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم المنتج مطلوب.")]
        [StringLength(200, ErrorMessage = "يجب ألا يتجاوز اسم المنتج {1} حرفًا.")]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "يجب ألا يتجاوز الوصف {1} حرفًا.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "الوحدة مطلوبة.")]
        [StringLength(20, ErrorMessage = "يجب ألا تتجاوز الوحدة {1} حرفًا.")]
        public string Unit { get; set; } = "قطعة";

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentStock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MinimumStock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaximumStock { get; set; }

        public bool IsActive { get; set; } = true;
        public bool TrackInventory { get; set; } = true;

        [StringLength(100)]
        public string? Category { get; set; }

        [StringLength(100)]
        public string? Brand { get; set; }

        [StringLength(50)]
        public string? Barcode { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }

        // Navigation Properties
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();
    }

    // نموذج حركة المخزون - Inventory Movement Model
    public class InventoryMovement
    {
        [Key]
        public int MovementId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "نوع الحركة مطلوب.")]
        public InventoryMovementType MovementType { get; set; }

        [Required(ErrorMessage = "الكمية مطلوبة.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }

        [Required(ErrorMessage = "تاريخ الحركة مطلوب.")]
        public DateTime MovementDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(50)]
        public string? ReferenceNumber { get; set; }

        public int? InvoiceId { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; } = null!;
        public virtual Invoice? Invoice { get; set; }
    }

    // أنواع حركة المخزون
    public enum InventoryMovementType
    {
        Purchase = 1,      // شراء
        Sale = 2,          // بيع
        Adjustment = 3,    // تسوية
        Transfer = 4,      // نقل
        Return = 5,        // مرتجع
        Damage = 6,        // تالف
        Opening = 7        // رصيد افتتاحي
    }
}

