using Abp.Authorization;
using PhapY.Authorization.Roles;
using PhapY.Authorization.Users;

namespace PhapY.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
