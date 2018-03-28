using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using PhapY.Authorization;
using PhapY.Users;
using PhapY.Models.Users;
using PhapY.Users.Dto;
using PhapY.Web.Models.Users;

namespace PhapY.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : PhapYControllerBase
    {
        private readonly UserAppService _userAppService;
        private readonly IRepository<Model.NhanVien> _nhanvienRepository;

        public UsersController(UserAppService userAppService,
            IRepository<Model.NhanVien> nhanvienRepository)
        {
            _userAppService = userAppService;
            _nhanvienRepository = nhanvienRepository;
        }

        public async Task<ActionResult> Index()
        {
            var users = (await _userAppService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue })).Items; //Paging not implemented yet
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }

        public async Task<ActionResult> EditUserModal(long userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;

            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return View("_EditUserModal", model);
        }
        public async Task<PartialViewResult> PermissionsModal(long id)
        {
            var user = await UserManager.GetUserByIdAsync(id);
            var output = await _userAppService.GetUserPermissionsForEdit(new EntityDto<long>(id));
            var viewModel = new UserPermissionsEditViewModel(output, user);

            return PartialView("_PermissionsModal", viewModel);
        }
        [WrapResult(WrapOnSuccess = false, WrapOnError = true)]
        public async Task<ActionResult> AccountLogin_Read(long? userId)
        {
            var nhanVien = await _nhanvienRepository.GetAll()
                .Where(x => x.TaiKhoanId.HasValue)
                .Select(x => x.TaiKhoanId).ToListAsync();
            var query = UserManager.Users
                .Where(x => x.UserName != "admin");
            query = userId == 0 ? query.Where(x => !nhanVien.Contains(x.Id)) : query.Where(x => !nhanVien.Contains(x.Id) || x.Id == userId);

            var accList = await query.Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName
            })
            .ToListAsync();

            return Json(accList, JsonRequestBehavior.AllowGet);
        }
    }
}