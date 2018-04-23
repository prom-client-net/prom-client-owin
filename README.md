# Prometheus.Client.Owin

[![NuGet Badge](https://buildstats.info/nuget/Prometheus.Client.Owin)](https://www.nuget.org/packages/Prometheus.Client.Owin/)
[![Build status](https://ci.appveyor.com/api/projects/status/mi4ylkkw9j3ovvo9/branch/master?svg=true)](https://ci.appveyor.com/project/PrometheusClientNet/prometheus-client-owin/branch/master)
[![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT)  
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/de67f364079b4af09fe7b5ae4bc4faa5)](https://www.codacy.com/app/phnx47/Prometheus.Client.Owin?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=phnx47/Prometheus.Client.Owin&amp;utm_campaign=Badge_Grade)  

Extension for [Prometheus.Client](https://github.com/PrometheusClientNet/Prometheus.Client)

## Quik start


#### Install:

    PM> Install-Package Prometheus.Client.Owin

#### .Net 4.5:

```csharp

public void Configuration(IAppBuilder app)
{  
    app.UsePrometheusServer(new PrometheusOptions()
        {
            MapPath = "api/metrics"
        });         
}
```