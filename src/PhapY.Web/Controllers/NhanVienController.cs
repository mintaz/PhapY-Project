using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Web.Mvc.Authorization;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PhapY.Authorization;
using PhapY.ChucVu.Dto;
using PhapY.NhanVien.Dto;
using PhapY.TrinhDo.Dto;

namespace PhapY.Web.Controllers
{
    [AbpMvcAuthorize]
    public class NhanVienController : PhapYControllerBase
    {
        // GET: NhanViens
        private readonly IRepository<Model.NhanVien> _nhanvienRepository;

        private readonly IRepository<Model.ChucVu> _chucvuRepository;
        private readonly IRepository<Model.TrinhDo> _trinhdoRepository;
        private readonly IObjectMapper _objectMapper;

        private void GetChucVus()
        {
            ViewBag.DSChucVu = _chucvuRepository.GetAll()
                .Select(x => new ChucVuDto
                {
                    Id = x.Id,
                    TenChucVu = x.TenChucVu
                })
                .ToArray();
        }

        private void GetTrinhDos()
        {
            ViewBag.DSTrinhDo = _trinhdoRepository.GetAll()
                .Select(x => new TrinhDoDto
                {
                    Id = x.Id,
                    TenTrinhDo = x.TenTrinhDo
                })
                .ToArray();
        }

        public NhanVienController(IRepository<Model.NhanVien> nhanvienRepository,
            IRepository<Model.ChucVu> chucvuRepository,
            IRepository<Model.TrinhDo> trinhdoRepository,
            IObjectMapper objectMapper)
        {
            _nhanvienRepository = nhanvienRepository;
            _chucvuRepository = chucvuRepository;
            _trinhdoRepository = trinhdoRepository;
            _objectMapper = objectMapper;
        }

        [AbpMvcAuthorize(PermissionNames.Pages_NhanVien_Read)]
        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(PermissionNames.Pages_NhanVien_Create)]
        public ActionResult Add()
        {
            var model = new NhanVienDto { NgaySinh = DateTime.Now.AddYears(-20).Date, ChucVuId = null, TrinhDoId = null };
            GetChucVus();
            GetTrinhDos();

            return PartialView("~/Views/NhanVien/Add.cshtml", model);
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Pages_NhanVien_Create)]
        [WrapResult(WrapOnSuccess = false, WrapOnError = true)]
        public async Task<JsonResult> Add(NhanVienDto model)
        {
            var msg = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    var nhanVien = _objectMapper.Map<Model.NhanVien>(model);
                    //nhanVien.NgaySinh = DateTime.Now.Date;
                    using (var unitOfWork = UnitOfWorkManager.Begin())
                    {
                        //Create Nhân viên
                        var nhanVienId = await _nhanvienRepository.InsertAndGetIdAsync(nhanVien);
                        nhanVien.MaNv = model.GenerateMaNv(nhanVienId.ToString());
                        nhanVien.TaiKhoanId = model.UserId;
                        await _nhanvienRepository.UpdateAsync(nhanVien);

                        await unitOfWork.CompleteAsync();
                        return Json(new
                        {
                            success = true,
                            type = "create",
                            responseText = "success"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return Json(new
            {
                success = false,
                responseText = msg
            }, JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_NhanVien_Update)]
        public ActionResult Edit(int id)
        {
            var model = _nhanvienRepository.GetAll().Where(x => x.Id.Equals(id)).Select(x => new NhanVienDto()
            {
                Id = x.Id,
                ChucVuId = x.ChucVuId,
                DiaChi = x.DiaChi,
                BsChinh = x.BsChinh,
                HoTen = x.HoTen,
                NgaySinh = x.NgaySinh,
                SoDt = x.SoDt,
                UserId = x.TaiKhoanId,
                TrinhDoId = x.TrinhDoId
            }).FirstOrDefault();
            GetChucVus();
            GetTrinhDos();

            return PartialView("~/Views/NhanVien/Edit.cshtml", model);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_NhanVien_Update)]
        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = true)]
        public async Task<JsonResult> Edit(NhanVienDto model)
        {
            var msg = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    var nhanVien = _objectMapper.Map<Model.NhanVien>(model);
                    using (var unitOfWork = UnitOfWorkManager.Begin())
                    {
                        nhanVien.TaiKhoanId = model.UserId;

                        await _nhanvienRepository.UpdateAsync(nhanVien);

                        await unitOfWork.CompleteAsync();
                        return Json(new
                        {
                            success = true,
                            type = "update",
                            responseText = "success"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return Json(new
            {
                success = false,
                responseText = msg
            }, JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize(PermissionNames.Pages_NhanVien_Read)]
        [WrapResult(WrapOnSuccess = false, WrapOnError = true)]
        public async Task<ActionResult> GetNhanViens([DataSourceRequest] DataSourceRequest request)
        {
            var data = await _nhanvienRepository.GetAll()
                .Include(x => x.TaiKhoan)
                .Select(x => new NhanVienDto
                {
                    Id = x.Id,
                    HoTen = x.HoTen,
                    NgaySinh = x.NgaySinh,
                    DiaChi = x.DiaChi,
                    SoDt = x.SoDt,
                    UserName = x.TaiKhoan.UserName
                })
                .ToListAsync();
            return Json(data.ToDataSourceResult(request));
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = true)]
        [AcceptVerbs(HttpVerbs.Post)]
        [AbpMvcAuthorize(PermissionNames.Pages_NhanVien_Delete)]
        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, int id)
        {
            using (var uniOfWork = UnitOfWorkManager.Begin())
            {
                if (id != 0)
                {
                    var nhanVien = await _nhanvienRepository.FirstOrDefaultAsync(x => x.Id == id);
                    if (nhanVien != null)
                    {
                        nhanVien.TaiKhoanId = null;
                        await _nhanvienRepository.UpdateAsync(nhanVien);
                        await _nhanvienRepository.DeleteAsync(nhanVien);
                        await uniOfWork.CompleteAsync();
                        return Json(new
                        {
                            success = true,
                            responseText = "success"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new
                {
                    success = false,
                    responseText = "fail"
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}