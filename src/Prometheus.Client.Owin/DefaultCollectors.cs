using System.Collections.Generic;
using Prometheus.Client.Collectors;

namespace Prometheus.Client.Owin
{
    /// <summary>
    ///     All default Collector
    /// </summary>
    public static class DefaultCollectors
    {
        /// <summary>
        ///     Get default Collector
        /// </summary>
        public static IEnumerable<IOnDemandCollector> Get(MetricFactory metricFactory)
        {
            yield return new DotNetStatsCollector(metricFactory);
            yield return new WindowsDotNetStatsCollector(metricFactory);
        }
    }
}