using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhapY.Authorization.Users.Dto;
using PhapY.Users.Dto;

namespace PhapY.Authorization.Users
{
    public interface ICustomUserAppService : IApplicationService
    {
        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IEntityDto<long> input);
        Task UpdateUserPermissions(UpdateUserPermissionsInput input);
        Task DeleteUser(long id);
        Task<UserDto> Create(CreateUserDto input);
        Task Update(UpdateUserDto input);
    }
}
