using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Common.MediatR.Behaviors;

namespace Common.MediatR
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMediator(this IServiceCollection services,
            Action<MediatROptions> customSetup,
            Action<MediatRServiceConfiguration> defaultSetup,
            params Assembly[] assemblies)
        {
            var options = new MediatROptions();

            customSetup(options);

            if (options != null)
            {
                if (options.EnableTracingPipeline)
                {
                    //TODO: Could be refactored. Don't like the way I use
                    services.Configure(options.SetupPrfBehavior);

                    /*
                     * options.PerformanceBehaviorOption is already populated. How to pass as a IOption<PerformanceBehaviorOption>
                     */

                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceTraceBehavior<,>));
                    //services.Configure<PerformanceBehaviorOptions>(settings =>
                    //{
                    //    settings.
                    //});
                    services.Configure<PerformanceBehaviorOptions>(settings =>
                    {
                        
                    });
                }

                if (options.EnableValidationPipeline)
                {
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                }
            }

            services.AddMediatR(x =>
            {
                defaultSetup?.Invoke(x);
            }, assemblies.Length > 0 ? assemblies : new[] { Assembly.GetEntryAssembly() });

            return services;
        }


        #region Method Chaining
        /// <summary>
        ///  
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the services to.</param>
        /// <param name="customSetup">The action used to configure the options.</param>
        /// <param name="assemblies"></param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection RegisterMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            return RegisterMediator(services, _ => { }, _ => { }, assemblies);
        }



        /// <summary>
        ///  
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the services to.</param>
        /// <param name="customSetup">The action used to configure the options.</param>
        /// <param name="assemblies"></param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection RegisterMediator(this IServiceCollection services,
            Action<MediatROptions> customSetup,
            params Assembly[] assemblies)
        {
            return RegisterMediator(services, customSetup, _ => { }, assemblies);
        }
        #endregion
    }
}
