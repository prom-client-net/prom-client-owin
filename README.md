# Prometheus.Client.Owin

[![MyGet](https://img.shields.io/myget/phnx47-beta/vpre/Prometheus.Client.Owin.svg)](https://www.myget.org/feed/phnx47-beta/package/nuget/Prometheus.Client.Owin)
[![NuGet](https://img.shields.io/nuget/v/Prometheus.Client.Owin.svg)](https://www.nuget.org/packages/Prometheus.Client.Owin)
[![NuGet](https://img.shields.io/nuget/dt/Prometheus.Client.Owin.svg)](https://www.nuget.org/packages/Prometheus.Client.Owin)

[![Build status](https://ci.appveyor.com/api/projects/status/mi4ylkkw9j3ovvo9/branch/master?svg=true)](https://ci.appveyor.com/project/PrometheusClientNet/prometheus-client-owin/branch/master)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/de67f364079b4af09fe7b5ae4bc4faa5)](https://www.codacy.com/app/phnx47/Prometheus.Client.Owin?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=phnx47/Prometheus.Client.Owin&amp;utm_campaign=Badge_Grade)  
[![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT)

Extension for [Prometheus.Client](https://github.com/PrometheusClientNet/Prometheus.Client)

#### Installation:

     dotnet add package Prometheus.Client.Owin

#### Quik start:

There are [Examples](https://github.com/PrometheusClientNet/Prometheus.Client.Examples/tree/master/Middleware/WebOwin_4.5)

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
