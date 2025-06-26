using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingPro.Models
{
    // نموذج المستخدم - User Model
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "اسم المستخدم مطلوب.")]
        [StringLength(50, ErrorMessage = "يجب ألا يتجاوز اسم المستخدم {1} حرفًا.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم الكامل مطلوب.")]
        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز الاسم الكامل {1} حرفًا.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح.")]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsEmailConfirmed { get; set; } = false;
        public bool MustChangePassword { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public DateTime? LastModified { get; set; }

        public int? CreatedByUserId { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual User? CreatedByUser { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();

        // Calculated Properties
        public string RoleNames => string.Join(", ", UserRoles.Select(ur => ur.Role.RoleName));
        public bool IsOnline => UserSessions.Any(s => s.IsActive && s.LastActivity > DateTime.Now.AddMinutes(-30));
    }

    // نموذج الدور - Role Model
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "اسم الدور مطلوب.")]
        [StringLength(50, ErrorMessage = "يجب ألا يتجاوز اسم الدور {1} حرفًا.")]
        public string RoleName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        public bool IsSystemRole { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        // Calculated Properties
        public int UserCount => UserRoles.Count(ur => ur.User.IsActive);
        public string PermissionNames => string.Join(", ", RolePermissions.Select(rp => rp.Permission.PermissionName));
    }

    // نموذج الصلاحية - Permission Model
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "اسم الصلاحية مطلوب.")]
        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز اسم الصلاحية {1} حرفًا.")]
        public string PermissionName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "المجموعة مطلوبة.")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        public bool IsSystemPermission { get; set; } = false;
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

    // نموذج ربط المستخدم بالدور - User Role Model
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public int? AssignedByUserId { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual User? AssignedByUser { get; set; }
    }

    // نموذج ربط الدور بالصلاحية - Role Permission Model
    public class RolePermission
    {
        [Key]
        public int RolePermissionId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int PermissionId { get; set; }

        public DateTime GrantedDate { get; set; } = DateTime.Now;
        public int? GrantedByUserId { get; set; }

        // Navigation Properties
        public virtual Role Role { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
        public virtual User? GrantedByUser { get; set; }
    }

    // نموذج جلسة المستخدم - User Session Model
    public class UserSession
    {
        [Key]
        public int SessionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string SessionToken { get; set; } = string.Empty;

        public DateTime LoginTime { get; set; } = DateTime.Now;
        public DateTime LastActivity { get; set; } = DateTime.Now;
        public DateTime? LogoutTime { get; set; }

        [StringLength(45)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        public bool IsActive { get; set; } = true;
        public bool RememberMe { get; set; } = false;

        // Navigation Properties
        public virtual User User { get; set; } = null!;

        // Calculated Properties
        public TimeSpan SessionDuration => (LogoutTime ?? DateTime.Now) - LoginTime;
        public bool IsExpired => LastActivity < DateTime.Now.AddHours(-24);
    }
}

