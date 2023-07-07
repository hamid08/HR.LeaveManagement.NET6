using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LeaveManagementIdentityDbContext _db;
        public UserService(UserManager<ApplicationUser> userManager, LeaveManagementIdentityDbContext db)
        {
            _userManager = userManager;
            _db = db;

        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee
            {
                Id = q.Id,
                Email = q.Email,
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }

        public async Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user)
        {
            await _db.UserRefreshTokens.AddAsync(user);
            return user;
        }

        public async Task DeleteUserRefreshTokens(string userId, string refreshToken)
        {
            var item = await _db.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _db.UserRefreshTokens.Remove(item);
            }
        }

        public async Task<UserRefreshToken> GetSavedRefreshTokens(string userId, string refreshToken)
        {
            return await _db.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public async Task SaveCommit()
        {
            await _db.SaveChangesAsync();
        }


    }
}
