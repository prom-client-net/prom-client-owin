using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Prometheus.Client.Collectors;
using Prometheus.Client.Collectors.DotNetStats;
using Prometheus.Client.Collectors.ProcessStats;
using Xunit;

namespace Prometheus.Client.Owin.Tests;

public class AppBuilderExtensionsTests
{
    private readonly ICollectorRegistry _registry = new CollectorRegistry();

    [Fact]
    public void AppBuilderIsNull_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ((AppBuilder)null).UsePrometheusServer());
    }

    [Fact]
    public void UseDefaultCollectors_True_Register_DefaultCollectors()
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q => q.CollectorRegistry = _registry);

        _registry.TryGet(nameof(ProcessCollector), out var processCollector);
        Assert.NotNull(processCollector);

        _registry.TryGet(nameof(GCTotalMemoryCollector), out var gcTotalMemoryCollector);
        Assert.NotNull(gcTotalMemoryCollector);

        _registry.TryGet(nameof(GCCollectionCountCollector), out var gcCollectionCountCollector);
        Assert.NotNull(gcCollectionCountCollector);
    }

    [Fact]
    public void UseDefaultCollectors_False_NotRegister_DefaultCollectors()
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q =>
        {
            q.CollectorRegistry = _registry;
            q.UseDefaultCollectors = false;
        });

        _registry.TryGet(nameof(ProcessCollector), out var processCollector);
        Assert.Null(processCollector);

        _registry.TryGet(nameof(GCTotalMemoryCollector), out var gcTotalMemoryCollector);
        Assert.Null(gcTotalMemoryCollector);

        _registry.TryGet(nameof(GCCollectionCountCollector), out var gcCollectionCountCollector);
        Assert.Null(gcCollectionCountCollector);
    }

    [Fact]
    public void Custom_CollectorRegistry()
    {
        var app = new AppBuilder();
        var customRegistry = new CollectorRegistry();
        app.UsePrometheusServer(q => q.CollectorRegistry = customRegistry);

        _registry.TryGet(nameof(ProcessCollector), out var collector);
        Assert.Null(collector);

        customRegistry.TryGet(nameof(ProcessCollector), out collector);
        Assert.NotNull(collector);
    }

    [Fact]
    public async Task Default_Path_Return_200()
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q =>
        {
            q.CollectorRegistry = _registry;
            q.UseDefaultCollectors = false;
        });

        var context = CreateContext(Defaults.MapPath);
        await Invoke(app, context);

        Assert.Equal(200, context.Response.StatusCode);
    }

    [Fact]
    public async Task Default_ContentType()
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q =>
        {
            q.CollectorRegistry = _registry;
            q.UseDefaultCollectors = false;
        });

        var context = CreateContext(Defaults.MapPath);
        await Invoke(app, context);

        Assert.Equal(Defaults.ContentType, context.Response.ContentType);
    }

    [Theory]
    [InlineData("/path")]
    [InlineData("/test")]
    [InlineData("/test1")]
    public async Task Custom_Path_Return_200(string path)
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q =>
        {
            q.CollectorRegistry = _registry;
            q.UseDefaultCollectors = false;
            q.MapPath = path;
        });

        var context = CreateContext(path);
        await Invoke(app, context);

        Assert.Equal(200, context.Response.StatusCode);
    }

    [Theory]
    [InlineData("path")]
    [InlineData("test")]
    [InlineData("test1")]
    public async Task Custom_Path_Prepend_Slash_Return_200(string path)
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q =>
        {
            q.CollectorRegistry = _registry;
            q.UseDefaultCollectors = false;
            q.MapPath = path;
        });

        var context = CreateContext("/" + path);
        await Invoke(app, context);

        Assert.Equal(200, context.Response.StatusCode);
    }

    [Theory]
    [InlineData("/wrong")]
    [InlineData("/wr1")]
    [InlineData("/test")]
    public async Task Wrong_Path_Return_404(string path)
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q =>
        {
            q.CollectorRegistry = _registry;
            q.UseDefaultCollectors = false;
        });

        var context = CreateContext(path);
        await Invoke(app, context);

        Assert.Equal(404, context.Response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(GetEncodings))]
    public async Task Custom_ResponseEncoding_Return_ContentType_With_Encoding(Encoding encoding)
    {
        var app = new AppBuilder();
        app.UsePrometheusServer(q =>
        {
            q.CollectorRegistry = _registry;
            q.UseDefaultCollectors = false;
            q.ResponseEncoding = encoding;
        });

        var context = CreateContext(Defaults.MapPath);
        await Invoke(app, context);

        Assert.Equal($"{Defaults.ContentType}; charset={encoding.BodyName}", context.Response.ContentType);
    }

    public static IEnumerable<object[]> GetEncodings()
    {
        yield return [Encoding.UTF8];
        yield return [Encoding.Unicode];
        yield return [Encoding.ASCII];
        yield return [Encoding.UTF32];
    }

    private static OwinContext CreateContext(string path)
    {
        var context = new OwinContext
        {
            Request =
            {
                Scheme = "http",
                Host = new HostString("localhost"),
                PathBase = new PathString(""),
                Path = new PathString(path)
            },
            Response =
            {
                Body = new MemoryStream()
            }
        };

        return context;
    }

    private static async Task Invoke(AppBuilder app, OwinContext context)
    {
        var pipeline = app.Build();
        await (Task)pipeline.DynamicInvoke(context.Environment);
    }
}
