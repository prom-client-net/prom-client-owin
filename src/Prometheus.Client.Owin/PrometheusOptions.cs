using Prometheus.Client.Collectors.Abstractions;

namespace Prometheus.Client.Owin
{
    /// <summary>
    ///     Options for Prometheus
    /// </summary>
    public class PrometheusOptions
    {
        /// <summary>
        ///     Url, default = "/metrics"
        /// </summary>
        public string MapPath { get; set; } = "/metrics";

        /// <summary>
        ///     CollectorRegistry instance
        /// </summary>
        public ICollectorRegistry CollectorRegistryInstance { get; set; } = Metrics.DefaultCollectorRegistry;
        
        /// <summary>
        ///     Use default collectors
        /// </summary>
        public bool UseDefaultCollectors { get; set; } = true;
    }
}
