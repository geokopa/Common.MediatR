using System;
using System.Collections.Generic;

namespace Common.MediatR
{
    public class PerformanceBehaviorOptions
    {
        public ushort DefaultThresholdInMs { get; set; }

        //TODO: discover more efficent data structure
        public List<OptionItem> Settings { get; set; } = new List<OptionItem>();
    }

    public class OptionItem
    {
        public ushort ThresholdInMs { get; set; }
        public Type RequestType { get; set; }
    }
}
