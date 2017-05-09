using System.Collections.Generic;
using Prometheus.Client.Advanced;

namespace Prometheus.Client.Owin
{
    /// <summary>
    ///     Options for Prometheus
    /// </summary>
    public class PrometheusOptions
    {
        /// <summary>
        ///     Url, default = "metrics"
        /// </summary>
        public string MapPath { get; set; } = "metrics";

        /// <summary>
        ///     CollectorRegistry intance
        /// </summary>
        public ICollectorRegistry CollectorRegistryInstance { get; set; } = CollectorRegistry.Instance;

        /// <summary>
        ///     IOnDemandCollectors
        /// </summary>
        public List<IOnDemandCollector> Collectors { get; set; } = new List<IOnDemandCollector>();

        /// <summary>
        ///     CollectorLocator for default Collectors
        /// </summary>
        public CollectorLocator CollectorLocator { get; set; } = new CollectorLocator();
    }
}