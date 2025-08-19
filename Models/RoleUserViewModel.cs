using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class RoleUsersViewModel
    {
        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(50, ErrorMessage = "Role name must be less than 50 characters.")]
        public string RoleName { get; set; }
        public System.Collections.Generic.List<UserRoleViewModel> Users { get; set; }
    }
}
