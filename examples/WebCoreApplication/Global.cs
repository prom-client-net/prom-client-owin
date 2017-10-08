using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prometheus.Client.Collectors;

namespace WebCoreApplication
{
    public static class Global
    {
       public static ICollectorRegistry MyCollectorRegister = new CollectorRegistry();
    }
}
