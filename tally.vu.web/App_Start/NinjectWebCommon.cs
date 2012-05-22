[assembly: WebActivator.PreApplicationStartMethod(typeof(tallyvu.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(tallyvu.App_Start.NinjectWebCommon), "Stop")]

namespace tallyvu.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using tallyvu.DataAccess;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new BindingsModule());
        }

        public class BindingsModule : Ninject.Modules.NinjectModule
        {
            public override void Load()
            {
                //Bind<ControllerActions>()
                //    .ToMethod(x => ControllerActions.DiscoverControllerActions())
                //    .InSingletonScope();

                //Bind<IRouteGenerator>().To<RouteGenerator>().InSingletonScope();

                Bind<DataContext>().ToSelf().InRequestScope()
                    .OnDeactivation(x => x.SaveChanges());

                Bind<IRepository>().To<Repository>().InRequestScope()
                    .WithConstructorArgument("isSharedContext", true);
            }
        }
    }
}

