using Autofac;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;

namespace NLayer.Web.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            // Servis ve Repository'lerimizin yer aldığı Assembly'leri alalım
            var webAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(webAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();  // AddScoped

            builder.RegisterAssemblyTypes(webAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service"))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();  // AddScoped

            builder.RegisterType<ProductServiceWithNoCaching>().As<IProductService>();
        }
    }
}
