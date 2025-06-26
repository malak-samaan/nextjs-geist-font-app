using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingPro.Models
{
    public class Account
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "رقم الحساب مطلوب")]
        [StringLength(20, ErrorMessage = "رقم الحساب يجب ألا يتجاوز 20 رقم")]
        public string AccountNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "اسم الحساب مطلوب")]
        [StringLength(100, ErrorMessage = "اسم الحساب يجب ألا يتجاوز 100 حرف")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "الوصف يجب ألا يتجاوز 500 حرف")]
        public string? Description { get; set; }
        
        public AccountType Type { get; set; }
        
        public int? ParentAccountId { get; set; }
        public Account? ParentAccount { get; set; }
        
        public List<Account> SubAccounts { get; set; } = new();
        
        public decimal Balance { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? LastModified { get; set; }
    }
    
    public enum AccountType
    {
        Asset = 1,          // أصول
        Liability = 2,      // خصوم
        Equity = 3,         // حقوق الملكية
        Revenue = 4,        // إيرادات
        Expense = 5         // مصروفات
    }
}
