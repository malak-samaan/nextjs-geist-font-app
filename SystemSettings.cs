using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    public class SystemSettings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Value { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        public SettingCategory Category { get; set; }

        public bool IsEditable { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
    }

    public enum SettingCategory
    {
        General = 1,
        Security = 2,
        Backup = 3,
        Email = 4,
        Report = 5,
        Invoice = 6,
        Notification = 7,
        Appearance = 8
    }
}

