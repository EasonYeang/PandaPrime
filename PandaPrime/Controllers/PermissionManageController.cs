using AutoMapper;
using Domain;
using IService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace PandaPrime.Controllers
{
    public class PermissionManageController : Controller
    {
        public IPPermissionService _ppermissionService;
        public PermissionManageController(IPPermissionService ppermissionService)
        {
            _ppermissionService = ppermissionService;
        }

        // GET: PermissionManage
        public ActionResult Index()
        {
            return null;
        }

        [HttpPost]
        public JsonResult GetTableData()
        {
            List<PPermission> result = _ppermissionService.GetList(r => !r.IsDelete.Value && r.Level > 0);
            List<PPermissionDto> resultDto = Mapper.Map<List<PPermission>, List<PPermissionDto>>(result);

            return Json(resultDto);
        }

        [HttpPost]
        public JsonResult OnDelete(int id)
        {
            int flag = 0;
            string msg = "删除失败！";
            PPermission permission = _ppermissionService.GetSingleById(id);
            permission.IsDelete = true;
            _ppermissionService.AddOrUpdate(permission);
            int r = _ppermissionService.SaveChanges();
            if (r > 0)
            {
                flag = 1;
                msg = "删除成功！";
            }
            return Json(Tools.Alert(flag, msg));
        }

        [HttpPost]
        public JsonResult GetParentPermissionList(int parentLevel)
        {
            List<PPermission> list = _ppermissionService.GetList(r => r.Level == parentLevel);
            List<DropDownListModel> dpms = list.Select(r => new DropDownListModel { value = r.ID.ToString(), label = r.Name }).ToList();
            return Json(dpms);
        }

        [HttpPost]
        public JsonResult AddOrUpdate(PPermissionDto pm, int id)
        {
            int flag = 0;
            string msg = "操作失败！";
            if (id.Equals(0))
            {
                PPermission entity = Mapper.Map<PPermissionDto, PPermission>(pm);
                entity.Level = entity.Level.Value + 1;
                entity.IsDelete = false;
                entity.AddTime = DateTime.Now.Date;
                _ppermissionService.AddOrUpdate(entity);
                int r = _ppermissionService.SaveChanges();
                if (r > 0)
                {
                    flag = 1;
                    msg = "添加成功！";
                }
            }
            return Json(Tools.Alert(flag, msg));
        }
    }
}