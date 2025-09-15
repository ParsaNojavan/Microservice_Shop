using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddMediatR(Assembly.GetExecutingAssembly());
            Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandledExceptionBehaviour<,>));
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return Services;
        }
    }
}
