using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;

using System;

namespace PTP.Core.Common.DI
{
    public sealed class IoC
    {
        private static readonly object SingletonLock = new object();
        private static IoC _instance;

        public static IoC Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SingletonLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new IoC();
                        }
                    }
                }

                return _instance;
            }
        }

        private IoC()
        {
            var containerBuilder = new ContainerBuilder();
        }

        public static IServiceProvider Register(IServiceCollection serviceCollection, Action<ContainerBuilder> registerServices = null)
        {
            var containerBuilder = new ContainerBuilder();

            if (serviceCollection != null)
            {
                containerBuilder.Populate(serviceCollection);
            }

            registerServices?.Invoke(containerBuilder);

            Instance.Container = containerBuilder.Build();

            return new AutofacServiceProvider(Instance.Container);
        }

        public T Reslove<T>()
        {
            ILifetimeScope scope = Container.BeginLifetimeScope();
            return scope.Resolve<T>();
        }

        public bool HasImplementation<T>()
        {
            return Container.IsRegistered<T>();
        }

        private IContainer Container { get; set; }
    }

}
