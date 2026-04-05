using Microsoft.EntityFrameworkCore;
using TaskBoard.API.Data.Context;
using TaskBoard.API.Models;
using TaskBoard.API.DTOs;

namespace TaskBoard.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse?> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserRoleAsync(UserRoleUpdateRequest request);
        Task<DashboardStatsResponse> GetDashboardStatsAsync();
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserResponse?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return null;

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<bool> UpdateUserRoleAsync(UserRoleUpdateRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return false;

            // Validate role
            if (request.NewRole != "User" && request.NewRole != "Admin")
                return false;

            user.Role = request.NewRole;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<DashboardStatsResponse> GetDashboardStatsAsync()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalTasks = await _context.Tasks.CountAsync();
            var toDoTasks = await _context.Tasks.CountAsync(t => t.Status == "To Do");
            var inProgressTasks = await _context.Tasks.CountAsync(t => t.Status == "In Progress");
            var doneTasks = await _context.Tasks.CountAsync(t => t.Status == "Done");

            return new DashboardStatsResponse
            {
                TotalUsers = totalUsers,
                TotalTasks = totalTasks,
                ToDoTasks = toDoTasks,
                InProgressTasks = inProgressTasks,
                DoneTasks = doneTasks
            };
        }
    }
}
