using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
