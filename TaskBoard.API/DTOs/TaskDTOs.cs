using System.ComponentModel.DataAnnotations;

namespace TaskBoard.API.DTOs
{
    // Task Create Request
    public class TaskCreateRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string Status { get; set; } = "To Do";
    }

    // Task Update Request
    public class TaskUpdateRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
    }

    // Task Response
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    // Admin Task Response (includes user info)
    public class AdminTaskResponse : TaskResponse
    {
        public string UserEmail { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}
