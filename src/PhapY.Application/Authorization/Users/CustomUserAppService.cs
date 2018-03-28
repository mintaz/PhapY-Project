using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNet.Identity;
using PhapY.Authorization.Dto;
using PhapY.Authorization.Roles;
using PhapY.Authorization.Users.Dto;
using PhapY.Users.Dto;

namespace PhapY.Authorization.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class CustomUserAppService : PhapYAppServiceBase, ICustomUserAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _iPermissionManager;
        private readonly IRepository<Model.NhanVien> _nhanvienRepository;

        public CustomUserAppService(
            RoleManager roleManager,
            IPermissionManager iPermissionManager,
            IRepository<Model.NhanVien> nhanvienRepository)
        {
            _roleManager = roleManager;
            _iPermissionManager = iPermissionManager;
            _nhanvienRepository = nhanvienRepository;
        }

        public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IEntityDto<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var permissions = _iPermissionManager.GetAllPermissions();
            var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(user);

            return new GetUserPermissionsForEditOutput
            {
                Permissions = permissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
        public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var grantedPermissions = _iPermissionManager.GetPermissionsFromNamesByValidating(input.GrantedPermissionNames);
            await UserManager.SetGrantedPermissionsAsync(user, grantedPermissions);
        }
        public async Task DeleteUser(long id)
        {
            if (id == AbpSession.GetUserId())
            {
                throw new UserFriendlyException(L("YouCanNotDeleteOwnAccount"));
            }

            var user = await UserManager.GetUserByIdAsync(id);
            CheckErrors(await UserManager.DeleteAsync(user));
            var nhanvien =
                await _nhanvienRepository.FirstOrDefaultAsync(x =>
                    x.TaiKhoanId.HasValue && x.TaiKhoanId.Value.Equals(id));
            if (nhanvien != null)
            {
                nhanvien.TaiKhoanId = null;
                await _nhanvienRepository.UpdateAsync(nhanvien);
            }
        }
        public async Task<UserDto> Create(CreateUserDto input)
        {
            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await UserManager.CreateAsync(user));

            return ObjectMapper.Map<UserDto>(user);
        }

        public async Task Update(UpdateUserDto input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            ObjectMapper.Map(input, user);

            if (!input.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.Password));
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await UserManager.SetRoles(user, input.RoleNames));
            }
        }
    }
}
