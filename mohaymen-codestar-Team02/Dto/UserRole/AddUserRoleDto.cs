using System.ComponentModel.DataAnnotations;

namespace mohaymen_codestar_Team02.Dto.UserRole;

public class AddUserRoleDto
{
    [Required] public string RoleType { get; set; }
    [Required] public string Username { get; set; }
}