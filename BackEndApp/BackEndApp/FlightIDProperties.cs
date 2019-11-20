using OpenTelemetry.Trace;
using OpenTelemetry.Trace.Export;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndApp
{
    public class FlightIDProperties : SpanProcessor
    {
        public override void OnStart(Span span)
        {
            foreach (var b in Activity.Current.Baggage)
            {
                span.SetAttribute(b.Key, b.Value);
            }
        }

        public override void OnEnd(Span span)
        {
        }

        public override Task ShutdownAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
