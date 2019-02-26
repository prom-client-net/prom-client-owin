# Prometheus.Client.Owin

[![NuGet](https://img.shields.io/nuget/v/Prometheus.Client.Owin.svg)](https://www.nuget.org/packages/Prometheus.Client.Owin)
[![NuGet](https://img.shields.io/nuget/dt/Prometheus.Client.Owin.svg)](https://www.nuget.org/packages/Prometheus.Client.Owin)
[![Build status](https://ci.appveyor.com/api/projects/status/mi4ylkkw9j3ovvo9/branch/master?svg=true)](https://ci.appveyor.com/project/PrometheusClientNet/prometheus-client-owin/branch/master)
[![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT)

Extension for [Prometheus.Client](https://github.com/PrometheusClientNet/Prometheus.Client)

#### Installation:

     dotnet add package Prometheus.Client.Owin

#### Use

There are [Examples](https://github.com/PrometheusClientNet/Prometheus.Client.Examples/tree/net45-support/Middleware/WebOwin_4.5)

```csharp

public void Configuration(IAppBuilder app)
{  
    app.UsePrometheusServer();         
}
```
or
```csharp
public void Configuration(IAppBuilder app)
{ 
    app.UsePrometheusServer(q =>
    {
        q.MapPath = "/api/metrics";
    });
}
 ```
