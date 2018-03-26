using System.Collections.Generic;
using PhapY.Roles.Dto;
using PhapY.Users.Dto;

namespace PhapY.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}