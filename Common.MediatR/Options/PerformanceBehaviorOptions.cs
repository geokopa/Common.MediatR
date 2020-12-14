using System;
using System.Collections.Generic;

namespace Common.MediatR
{
    public class PerformanceBehaviorOptions
    {
        public const string OptionName = nameof(PerformanceBehaviorOptions);
        public ushort DefaultThresholdInMs { get; set; }
        public List<OptionItem> RequestOptions { get; set; } = new List<OptionItem>();
    }

    public class OptionItem
    {
        public ushort ThresholdInMs { get; set; }
        public Type RequestType { get; set; }
    }
}
