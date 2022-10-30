
using Application;
using Application.Common.Behaviours.Authorization;
using Application.Common.Behaviours.Validation;
using Application.Common.Utilities;
using Application.Services;
using Autofac;
using AutoMapper;
using Infrastructure.Email;
using Infrastructure.Security;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Persistence.Repositories.EntityFramework.Contexts;
using System.Reflection;
using Module = Autofac.Module;

namespace WebAPI.Configuration.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var PersistenceAssmbly = Assembly.GetAssembly(typeof(BaseDbContext));
            var ApplicationAssembly = Assembly.GetAssembly(typeof(ApplicationServiceRegistration));

            builder.RegisterAssemblyTypes(PersistenceAssmbly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(PersistenceAssmbly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var val = builder.RegisterAssemblyTypes(ApplicationAssembly).Where(x => x.Name.EndsWith("Profiles")).As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ApplicationAssembly).Where(x => x.Name.EndsWith("BusinessRules")).InstancePerLifetimeScope();

            builder.RegisterMediatR(ApplicationAssembly);


            builder.RegisterGeneric(typeof(AuthorizationBehavior<,>)).As((typeof(IPipelineBehavior<,>))).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(RequestValidationBehavior<,>)).As((typeof(IPipelineBehavior<,>))).InstancePerLifetimeScope();



            builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerLifetimeScope();
            builder.RegisterType<EmailHelper>().As<IEmailHelper>().InstancePerLifetimeScope();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerLifetimeScope();





        }
    }
}
