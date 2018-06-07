using NLog;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace EndPoint
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new SimpleInjector.Container();
            container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();


            container.Register<IAccountService, AccountService>(Lifestyle.Transient);
            container.RegisterInstance<ILogger>(LogManager.GetCurrentClassLogger());
            container.RegisterWebApiControllers(config);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            config.Filters.Add(new ExceptionFilter());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
        }
    }
}
