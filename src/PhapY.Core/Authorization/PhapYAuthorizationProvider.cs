using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace PhapY.Authorization
{
    public class PhapYAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            var pageNhanVien = context.CreatePermission(PermissionNames.Pages_NhanVien_Read, L("XemDanhSachNhanVien"));
            pageNhanVien.CreateChildPermission(PermissionNames.Pages_NhanVien_Create, L("TaoMoiNhanVien"));
            pageNhanVien.CreateChildPermission(PermissionNames.Pages_NhanVien_Update, L("CapNhatNhanVien"));
            pageNhanVien.CreateChildPermission(PermissionNames.Pages_NhanVien_Delete, L("XoaNhanVien"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PhapYConsts.LocalizationSourceName);
        }
    }
}
