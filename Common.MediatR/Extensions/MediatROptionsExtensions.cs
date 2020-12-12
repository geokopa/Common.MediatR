using Microsoft.Extensions.Configuration;
using System;

namespace Common.MediatR
{
    public static class MediatROptionsExtensions
    {
        public static MediatROptions ConfigurePerformanceBehavior(this MediatROptions options,
            Action<PerformanceBehaviorOptions> setupAction)
        {
            // Create a default configuration object
            var o = new PerformanceBehaviorOptions();

            // Let the action modify it
            setupAction(o);

            options.PerformanceBehaviorOption = o;

            //options.SetupPrfBehavior = setupAction;


            return options;
        }
    }
}
