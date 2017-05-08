# Prometheus.Client.Owin

[![Build status](https://ci.appveyor.com/api/projects/status/vo97s13rworqfn27?svg=true)](https://ci.appveyor.com/project/phnx47/prometheus-client-owin) [![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT) [![NuGet Badge](https://buildstats.info/nuget/Prometheus.Client.Owin)](https://www.nuget.org/packages/Prometheus.Client.Owin/) 

Extension for [Prometheus.Client](https://github.com/phnx47/Prometheus.Client)

## Quik start


#### Install:

    PM> Install-Package Prometheus.Client.Owin

#### .Net Core:

```csharp

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
{
    app.UsePrometheusServer();
}

```


```csharp

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
{
    var options = new PrometheusOptions();
    options.MapPath = "metrics"; // ovverride route
    options.Collectors.Add(new DotNetStatsCollector()); // yours collector
    app.UsePrometheusServer(options);
}

```

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