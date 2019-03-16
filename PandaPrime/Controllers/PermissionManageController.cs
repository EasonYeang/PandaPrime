using AutoMapper;
using Domain;
using IService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetTableData()
        {
            List<PPermission> result = _ppermissionService.GetList(r => !r.IsDelete.Value && r.Level > 0);
            List<PPermissionDto> resultDto = Mapper.Map<List<PPermission>, List<PPermissionDto>>(result);
            List<TableColumn> tc = new List<TableColumn>() {
                new TableColumn{ type="selection",width=60,align="center" },
                new TableColumn{ title="目录名称",key="Title" },
                new TableColumn{ title="路径",key="Path" }
            };

            var json = new { cols = tc, tableData = resultDto, total = resultDto.Count };
            return Json(json);
        }
    }
}