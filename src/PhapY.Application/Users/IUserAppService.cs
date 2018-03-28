using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhapY.Roles.Dto;
using PhapY.Users.Dto;
using PhapY.Authorization.Users.Dto;

namespace PhapY.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IEntityDto<long> input);
        Task UpdateUserPermissions(UpdateUserPermissionsInput input);
    }
}