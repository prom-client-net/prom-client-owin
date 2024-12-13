# Prometheus.Client.Owin

[![ci](https://img.shields.io/github/actions/workflow/status/prom-client-net/prom-client-owin/ci.yml?branch=main&label=ci&logo=github&style=flat-square)](https://github.com/prom-client-net/prom-client-owin/actions/workflows/ci.yml)
[![nuget](https://img.shields.io/nuget/v/Prometheus.Client.Owin?logo=nuget&style=flat-square)](https://www.nuget.org/packages/Prometheus.Client.Owin)
[![nuget](https://img.shields.io/nuget/dt/Prometheus.Client.Owin?logo=nuget&style=flat-square)](https://www.nuget.org/packages/Prometheus.Client.Owin)
[![license](https://img.shields.io/github/license/prom-client-net/prom-client-owin?style=flat-square)](https://github.com/prom-client-net/prom-client-owin/blob/main/LICENSE)
<!-- [![codecov](https://img.shields.io/codecov/c/github/prom-client-net/prom-client-owin?logo=codecov&style=flat-square)](https://app.codecov.io/gh/prom-client-net/prom-client-owin) -->

Extension for [Prometheus.Client](https://github.com/PrometheusClientNet/Prometheus.Client)

## Installation

```sh
dotnet add package Prometheus.Client.Owin
```

## Use

[Examples](https://github.com/prom-client-net/prom-examples)

```c#
public void Configuration(IAppBuilder app)
{
    app.UsePrometheusServer();
}
```

```c#
public void Configuration(IAppBuilder app)
{
    app.UsePrometheusServer(q =>
    {
        q.MapPath = "/api/metrics";
    });
}
```

## Contribute

Contributions to the package are always welcome!

* Report any bugs or issues you find on the [issue tracker](https://github.com/prom-client-net/prom-client-owin/issues).
* You can grab the source code at the package's [git repository](https://github.com/prom-client-net/prom-client-owin).

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
