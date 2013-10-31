using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel kernel;

        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<ProductRepository>();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;

            return (IController)kernel.Get(controllerType);
        }
    }
}