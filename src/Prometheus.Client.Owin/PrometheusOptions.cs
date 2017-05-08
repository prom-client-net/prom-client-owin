using System.Collections.Generic;
using Prometheus.Client.Advanced;

namespace Prometheus.Client.Owin
{
    public class PrometheusOptions
    {
        public string MapPath { get; set; } = "metrics";

        public ICollectorRegistry CollectorRegistryInstance { get; set; } = CollectorRegistry.Instance;

        public List<IOnDemandCollector> Collectors { get; set; } = new List<IOnDemandCollector>();

        public CollectorLocator CollectorLocator { get; set; } = new CollectorLocator();
    }
}