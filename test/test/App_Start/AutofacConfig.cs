using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Mvc;
using test.AuthCustom;
using Autofac;
using System.Web.Mvc;
using Data;
using IService;

namespace test.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов            
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<AdminService>().As<IAdminService>();
            builder.RegisterType<AuthorizeManager>().As<IAuthorizeManager>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }        
    }
}