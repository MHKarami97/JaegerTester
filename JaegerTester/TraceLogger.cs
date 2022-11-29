using System;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace JaegerTester
{
    public sealed class TraceLogger : IDisposable
    {
        private Activity _activity;
        public static readonly ActivitySource ActivitySource = new ActivitySource("Jaeger.Test");

        public TraceLogger(string actionName)
        {
            _activity = ActivitySource.StartActivity(actionName);
        }

        public void Dispose()
        {
            _activity?.Dispose();
        }

        public static void InitOpenTelemetry(string serviceName, string jaegerAgentHost, int jaegerAgentPort, Func<TracerProviderBuilder, TracerProviderBuilder> custom = null)
        {
            try
            {
                var sdk = Sdk.CreateTracerProviderBuilder()
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                    .AddSource("Jaeger.Test")
                    .AddAspNetInstrumentation();

                if (custom != null)
                {
                    sdk = custom(sdk);
                }

                sdk.AddJaegerExporter(options =>
                {
                    options.AgentHost = jaegerAgentHost;
                    options.AgentPort = jaegerAgentPort;
                });

                sdk.AddConsoleExporter();

                sdk.Build();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}