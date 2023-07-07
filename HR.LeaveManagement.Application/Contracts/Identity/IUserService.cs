using HR.LeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);

        Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user);

        Task<UserRefreshToken> GetSavedRefreshTokens(string userId, string refreshtoken);

        Task DeleteUserRefreshTokens(string userId, string refreshToken);

        Task SaveCommit();
    }
}
