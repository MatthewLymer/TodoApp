using NHibernate;
using TodoApp.Core.Persistence;
using TodoApp.Core.Repositories;
using TodoApp.Core.Repositories.Impl;
using TodoApp.Web.Persistence;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TodoApp.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TodoApp.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TodoApp.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Strapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Strapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Strapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IConnectionStringProvider>().To<WebConfigConnectionStringProvider>();
            kernel.Bind<MsSqlSessionFactory>().ToSelf().InSingletonScope();
            kernel.Bind<ISession>().ToMethod(x => x.Kernel.Get<MsSqlSessionFactory>().Create()).InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>();
        }        
    }
}
