using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common.MediatR.Behaviors
{
    public class PerformanceTraceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> logger;
        private readonly Stopwatch _stopwatch;
        private readonly PerformanceBehaviorOptions _optionValue;

        public PerformanceTraceBehavior(ILogger<TRequest> logger,
             IOptions<PerformanceBehaviorOptions> options)
        {
            this.logger = logger;
            _stopwatch = new Stopwatch();
            _optionValue = options.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _stopwatch.Start();

            var response = await next();

            _stopwatch.Stop();

            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

            if (!_optionValue.RequestOptions.Any())
            {
                if (elapsedMilliseconds >= _optionValue.DefaultThresholdInMs)
                    this.logger.LogWarning($"Request {typeof(TRequest).Name} has ellapsed ({elapsedMilliseconds}) milliseconds  ");
            }
            else
            {
                foreach (var item in _optionValue.RequestOptions)
                {
                    if (item.RequestType == typeof(TRequest))
                    {
                        if (elapsedMilliseconds >= item.ThresholdInMs)
                            this.logger.LogWarning($"Request {item.RequestType} has ellapsed ({elapsedMilliseconds}) milliseconds. Threshold is set to: {item.ThresholdInMs} ");
                    }
                }
            }

            return response;
        }
    }
}
