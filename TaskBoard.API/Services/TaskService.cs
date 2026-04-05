using Microsoft.EntityFrameworkCore;
using TaskBoard.API.Data.Context;
using TaskBoard.API.Models;
using TaskBoard.API.DTOs;

namespace TaskBoard.API.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponse>> GetUserTasksAsync(int userId, string? status = null);
        Task<TaskResponse?> GetTaskByIdAsync(int taskId, int userId);
        Task<TaskResponse> CreateTaskAsync(TaskCreateRequest request, int userId);
        Task<TaskResponse?> UpdateTaskAsync(int taskId, TaskUpdateRequest request, int userId);
        Task<bool> DeleteTaskAsync(int taskId, int userId);
        
        // Admin methods
        Task<IEnumerable<AdminTaskResponse>> GetAllTasksAsync();
        Task<bool> DeleteAnyTaskAsync(int taskId);
    }

    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskResponse>> GetUserTasksAsync(int userId, string? status = null)
        {
            var query = _context.Tasks
                .Where(t => t.UserId == userId);

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status == status);
            }

            var tasks = await query
                .Include(t => t.User)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return tasks.Select(t => new TaskResponse
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                UserId = t.UserId,
                UserName = t.User.Name
            });
        }

        public async Task<TaskResponse?> GetTaskByIdAsync(int taskId, int userId)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return null;

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                UserId = task.UserId,
                UserName = task.User.Name
            };
        }

        public async Task<TaskResponse> CreateTaskAsync(TaskCreateRequest request, int userId)
        {
            var task = new Models.Task
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Get user info for response
            var user = await _context.Users.FindAsync(userId);

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                UserId = task.UserId,
                UserName = user?.Name ?? string.Empty
            };
        }

        public async Task<TaskResponse?> UpdateTaskAsync(int taskId, TaskUpdateRequest request, int userId)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return null;

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                UserId = task.UserId,
                UserName = task.User.Name
            };
        }

        public async Task<bool> DeleteTaskAsync(int taskId, int userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

        // Admin methods
        public async Task<IEnumerable<AdminTaskResponse>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks
                .Include(t => t.User)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return tasks.Select(t => new AdminTaskResponse
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                UserId = t.UserId,
                UserName = t.User.Name,
                UserEmail = t.User.Email,
                UserRole = t.User.Role
            });
        }

        public async Task<bool> DeleteAnyTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
