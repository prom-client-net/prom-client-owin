using System;
using System.Collections.Generic;
using Prometheus.Client.Collectors;

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
#if !COREFX
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                yield return new WindowsDotNetStatsCollector();
#endif
        }
    }
}