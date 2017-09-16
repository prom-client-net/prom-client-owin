using System;
using System.Collections.Generic;
using Prometheus.Client.Collectors;

namespace Prometheus.Client.Owin
{
    /// <summary>
    ///     All default Collector
    /// </summary>
    public class DefaultCollectors
    {
        /// <summary>
        ///     Get default Collector
        /// </summary>
        public static IEnumerable<IOnDemandCollector> Get()
        {
            yield return new DotNetStatsCollector();
#if !NETSTANDART13
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                yield return new WindowsDotNetStatsCollector();
#endif
        }
    }
}