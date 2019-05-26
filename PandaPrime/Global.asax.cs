using PandaPrime.AM;
using PandaPrime.App_Start;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Utility;

namespace PandaPrime
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //...注册Autofac
            AutofacConfig.Register();
            //...注册AutoMapper
            Configuration.Configure();
            //数据库不存在时，初始化
            Database.SetInitializer<PrimeContext>(new DataInit());
        }
    }
}
