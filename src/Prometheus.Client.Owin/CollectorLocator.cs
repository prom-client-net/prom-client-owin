using System.Collections.Generic;
using Prometheus.Client.Advanced;

namespace Prometheus.Client.Owin
{
    /// <summary>
    ///     All default Collector
    /// </summary>
    public class CollectorLocator
    {
        /// <summary>
        ///     Get default Collector
        /// </summary>
        public IEnumerable<IOnDemandCollector> Get()
        {
            yield return new DotNetStatsCollector();
        }
    }
}