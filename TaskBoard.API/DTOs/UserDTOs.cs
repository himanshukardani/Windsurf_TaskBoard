using System.ComponentModel.DataAnnotations;

namespace TaskBoard.API.DTOs
{
    // User Response
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // User Role Update Request
    public class UserRoleUpdateRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string NewRole { get; set; } = string.Empty;
    }

    // Dashboard Stats Response
    public class DashboardStatsResponse
    {
        public int TotalUsers { get; set; }
        public int TotalTasks { get; set; }
        public int ToDoTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int DoneTasks { get; set; }
    }
}
