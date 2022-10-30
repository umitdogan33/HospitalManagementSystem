using Application.Repositories.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependency
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IHttpContextAccessor _accessor;

        public ServiceFactory(IHttpContextAccessor accessor)
        {
            _accessor=accessor;
        }

        public T CreateService<T>()
        {
            var services = _accessor.HttpContext.RequestServices;
            var mainService = (T)services.GetService(typeof(T));
            return mainService;
        }
    }
}
