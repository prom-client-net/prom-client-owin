using System;
using Microsoft.Owin.Builder;
using Xunit;

namespace Prometheus.Client.Owin.Tests;

public class AppBuilderExtensionsTests
{
    [Fact]
    public void AppBuilderIsNull_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ((AppBuilder)null).UsePrometheusServer());
    }
}
