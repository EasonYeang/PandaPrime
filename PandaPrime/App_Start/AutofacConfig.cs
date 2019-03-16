using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace PandaPrime.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            //第一步： 构造一个AutoFac的builder容器  
            ContainerBuilder builder = new ContainerBuilder();

            Assembly controllerAss = Assembly.Load("PandaPrime");

            builder.RegisterControllers(controllerAss);

            //加载数据仓储层PandaPrime.Repository这个程序集。 
            Assembly repositoryAss = Assembly.Load("Repository");

            //反射扫描这个PandaPrime.Repository程序集中所有的类，得到这个程序集中所有类的集合。
            Type[] rtypes = repositoryAss.GetTypes();

            //告诉AutoFac容器，创建rtypes这个集合中所有类的对象实例
            builder.RegisterTypes(rtypes)
                .AsImplementedInterfaces(); //指明创建的rtypes这个集合中所有类的对象实例，以其接口的形式保存  

            //加载业务逻辑层PX.Service这个程序集。  
            Assembly servicesAss = Assembly.Load("Service");

            //反射扫描这个PX.Service程序集中所有的类，得到这个程序集中所有类的集合。  
            Type[] stypes = servicesAss.GetTypes();

            //告诉AutoFac容器，创建stypes这个集合中所有类的对象实例  
            builder.RegisterTypes(stypes)
                .AsImplementedInterfaces(); //指明创建的stypes这个集合中所有类的对象实例，以其接口的形式保存  

            //创建一个真正的AutoFac的工作容器  
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}