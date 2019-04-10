using AutoMapper;
using Domain;
using IService;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utility;
using System.Linq;

namespace PandaPrime.Controllers
{
    public class DefaultPageController : Controller
    {
        public IPPermissionService _ppermissionService;
        public DefaultPageController(IPPermissionService ppermissionService)
        {
            _ppermissionService = ppermissionService;
        }

        // GET: DefaultPage
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LogOut()
        {
            Session.Clear();
            int flag = 1;
            string msg = "操作成功！";
            return Json(Tools.Alert(flag, msg));
        }

        /// <summary>
        /// 获取顶部菜单栏
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTopPermissions()
        {
            List<PPermission> all = _ppermissionService.GetAll();
            List<PPermission> list = new List<PPermission>();
            if (all.Count == 0)
            {
                //数据库中没有目录
                list.AddRange(new PPermission[] {
                    new PPermission{
                    Name = "综合管理",
                    Level = 0,
                    Icon = "ios-keypad",
                    Order = 1,
                    IsDelete = false,
                    SerialNumber = 1,
                    AddTime = DateTime.Now.Date
                    },
                    new PPermission{
                        Name="控制台",
                        Level=1,
                        Order=1,
                        IsDelete=false,
                        SerialNumber=7,
                        ParentSN=1,
                        Icon="ios-desktop",
                        AddTime=DateTime.Now.Date
                    },
                    new PPermission{
                        Name="权限管理",
                        Level=1,
                        Order=2,
                        IsDelete=false,
                        SerialNumber=3,
                        ParentSN=1,
                        Icon="ios-key",
                        AddTime=DateTime.Now.Date
                    },
                    new PPermission{
                        Name="目录管理",
                        Level=2,
                        Order=1,
                        IsDelete=false,
                        SerialNumber=4,
                        ParentSN=3,
                        AddTime=DateTime.Now.Date
                    },
                    new PPermission{
                        Name="角色管理",
                        Level=2,
                        Order=2,
                        IsDelete=false,
                        SerialNumber=5,
                        ParentSN=3,
                        AddTime=DateTime.Now.Date
                    },
                    new PPermission{
                        Name="角色1",
                        Level=3,
                        Order=1,
                        IsDelete=false,
                        SerialNumber=6,
                        ParentSN=5,
                        AddTime=DateTime.Now.Date
                    },
                    new PPermission{
                    Name = "系统设置",
                    Level = 0,
                    Icon = "ios-cog",
                    Order = 2,
                    IsDelete = false,
                    SerialNumber = 2,
                    AddTime = DateTime.Now.Date
                    }
                });
                _ppermissionService.BatchInsert(list);
                _ppermissionService.SaveChanges();
            }
            else
            {
                list = _ppermissionService.GetListOrder(r => r.Level == 0, r => r.Order);
            }
            List<PPermissionVm> listVm = Mapper.Map<List<PPermission>, List<PPermissionVm>>(list);
            return Json(listVm);
        }

        [HttpPost]
        public JsonResult GetSidePermissions(int sn)
        {
            List<PPermissionVm> list = new List<PPermissionVm>();
            list = WhileCalcute(sn);
            return Json(list);
        }

        /// <summary>
        /// 迭代查询
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        private List<PPermissionVm> WhileCalcute(int sn)
        {
            List<PPermission> pl = _ppermissionService.GetListOrder(r => r.ParentSN == sn, r => r.Order);
            List<PPermissionVm> plVm = Mapper.Map<List<PPermission>, List<PPermissionVm>>(pl);
            foreach (PPermissionVm vm in plVm)
            {

                if (_ppermissionService.Exist(r => r.ParentSN == vm.SerialNumber))
                {
                    List<PPermission> spl = _ppermissionService.GetListOrder(r => r.ParentSN == sn, r => r.Order);
                    List<PPermissionVm> splVm = Mapper.Map<List<PPermission>, List<PPermissionVm>>(spl);
                    vm.Child = WhileCalcute(vm.SerialNumber);
                }
            }
            return plVm;
        }

        [HttpPost]
        public JsonResult SelectSideMenu(int sn)
        {
            List<PPermission> allPermissions = _ppermissionService.GetAll();
            PPermission slt = allPermissions.Find(r => r.SerialNumber.Equals(sn));
            List<int> openNames = new List<int>();
            List<string> nav = new List<string>();
            string returnStr = "";
            if (slt.ParentSN != null && slt.Level != 0)
            {
                returnStr += slt.FilePath;
                openNames.Add(slt.ParentSN.Value);
                nav.Add(slt.Name);
                WhileParent(allPermissions, slt, openNames, nav);
            }
            returnStr += string.Format(@"?openNames={0}&activeSide={1}", string.Join(",", openNames), sn);
            return Json(returnStr);
        }

        private static void WhileParent(List<PPermission> allPermissions, PPermission slt, List<int> openNames, List<string> nav)
        {
            PPermission prt = allPermissions.Find(r => r.SerialNumber.Equals(slt.ParentSN));
            if (prt.ParentSN != null && prt.Level != 0)
            {
                openNames.Add(prt.ParentSN.Value);
                nav.Add(prt.Name);
                WhileParent(allPermissions, prt, openNames, nav);
            }
        }

        [HttpPost]
        public JsonResult GetPermissionNav(string openNames, int aS)
        {
            string[] arr = openNames.Split(',');
            List<int> li = new List<int>();
            foreach (string sn in arr)
            {
                li.Add(int.Parse(sn));
            }
            li.Add(aS);
            List<string> result = _ppermissionService.GetList(p => li.Contains(p.SerialNumber)).Select(r => r.Name).ToList();
            return Json(result);
        }
    }
}