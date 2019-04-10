using AutoMapper;
using Domain;
using IService;
using Model;
using System;
using System.Web.Mvc;
using Utility;

namespace PandaPrime.Controllers
{
    public class LoginController : Controller
    {
        public IPAccountService _paccountService;

        public LoginController(IPAccountService paccountService)
        {
            _paccountService = paccountService;
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (!_paccountService.Exist(u => u.Account.Equals("admin")))
            {
                PAccount user = new PAccount()
                {
                    Account = "admin",
                    UserName = "超级管理员",
                    Password = MD5.Encode("setup"),
                    IsActive = true,
                    NickName = "超级",
                    CreateTime = DateTime.Now.Date
                };
                _paccountService.Add(user);
                _paccountService.SaveChanges();
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(PAccountVm vm)
        {
            PAccount pa = Mapper.Map<PAccount>(vm);
            DateTime t = DateTime.Now.Date;

            int flag = 0;
            string msg = "登录失败！";
            if (vm.ValidateCode.ToUpper() == Session["ValidateCode"].ToString().ToUpper())
            {
                try
                {
                    string pwd = MD5.Encode(vm.Password);
                    bool exist = _paccountService.GetList(r => r.Account == vm.Account && r.Password == pwd).Count > 0 ? true : false;
                    if (exist)
                    {
                        flag = 1;
                        PAccount account = _paccountService.FirstOrDefault(a => a.Account == vm.Account);
                        Session["UserID"] = account.ID;
                    }
                    else
                    {
                        msg = "账户或密码错误！";
                    }
                }
                catch (Exception ex)
                {
                    ValidateCode();
                }
            }
            else
            {
                msg = "验证码错误！";
            }
            return Json(Tools.Alert(flag, msg));
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string vCode = validateCode.RandCode();
            Session["ValidateCode"] = vCode;
            byte[] bytes = validateCode.CreateImage(vCode);
            return File(bytes, @"image/jpg");
        }
    }
}