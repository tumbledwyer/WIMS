﻿using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using WhereIsMyShit.App_Start;
using WhereIsMyShit.Controllers;

namespace WhereIsMyShit
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var windsorContainer = Init();
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(windsorContainer));
        }


        private IWindsorContainer Init()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IItemRepository>().ImplementedBy<ItemRepository>());

            var contollers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in contollers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }

            return container;
        }
    }

    public class ControllerFactory : DefaultControllerFactory
    {
        public ControllerFactory(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            Container = container;
        }

        public IWindsorContainer Container { get; protected set; }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            var controllerInstance = Container.Resolve(controllerType) as IController;
            return controllerInstance;
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;
            disposableController?.Dispose();

            Container.Release(controller);
        }
    }
}