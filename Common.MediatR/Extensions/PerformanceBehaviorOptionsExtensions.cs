namespace Common.MediatR
{
    public static class PerformanceBehaviorOptionsExtensions
    {
        public static PerformanceBehaviorOptions AddThreshold<T>(this PerformanceBehaviorOptions options, ushort thresholdInMs)
            where T : class //TODO: should implement concrete constraints e.g IRequest, IRequest<T>
        {
            var type = typeof(T);

            options.RequestOptions.RemoveAll(x => x.RequestType == type);
            options.RequestOptions.Add(new OptionItem { RequestType = type, ThresholdInMs = thresholdInMs });

            return options;
        }
    }
}
