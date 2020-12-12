using System;

namespace Common.MediatR
{
    public class MediatROptions
    {
        public bool EnableValidationPipeline { get; set; }
        public bool EnableTracingPipeline { get; set; }


        public PerformanceBehaviorOptions PerformanceBehaviorOption { get; set; }
        public Action<PerformanceBehaviorOptions> SetupPrfBehavior { get; internal set; }

    }
}
