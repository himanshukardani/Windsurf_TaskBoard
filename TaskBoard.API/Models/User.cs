using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "User"; // User or Admin

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for tasks
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }

    public enum UserRole
    {
        User = 0,
        Admin = 1
    }
}
