using System.Collections.Generic;
using Abp.Application.Services.Dto;
using PhapY.Authorization.Dto;

namespace PhapY.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput : EntityDto<long>
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}