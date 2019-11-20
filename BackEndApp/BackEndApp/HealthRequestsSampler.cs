using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndApp
{
    public class HealthRequestsSampler : ISampler
    {
        private ISampler _sampler;

        public HealthRequestsSampler(ISampler chainedSampler)
        {
            _sampler = chainedSampler;
        }

        public string Description { get; } = "HealthRequestSampler";

        public Decision ShouldSample(SpanContext parentContext, ActivityTraceId traceId, ActivitySpanId spanId, string name,
            IEnumerable<Link> links)
        {
            if (name == "/health")
            {
                return new Decision(false);
            }

            return _sampler.ShouldSample(parentContext, traceId, spanId, name, links);
        }
    }
}
