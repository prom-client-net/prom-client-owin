using System;
using Owin;
using Prometheus.Client.Collectors;

namespace Prometheus.Client.Owin;

/// <summary>
/// Extension methods for <see cref="IAppBuilder"/> to configure Prometheus metrics middleware.
/// </summary>
public static class AppBuilderExtensions
{
    /// <summary>
    /// Adds Prometheus metrics middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IAppBuilder"/> to configure.</param>
    /// <returns>The <see cref="IAppBuilder"/>.</returns>
    public static IAppBuilder UsePrometheusServer(this IAppBuilder app)
    {
        return app.UsePrometheusServer(null);
    }

    /// <summary>
    /// Adds Prometheus metrics middleware to the application pipeline with custom options.
    /// </summary>
    /// <param name="app">The <see cref="IAppBuilder"/> to configure.</param>
    /// <param name="setupOptions">An <see cref="Action{PrometheusOptions}"/> to configure options.</param>
    /// <returns>The <see cref="IAppBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="app"/> is <c>null</c>.</exception>
    public static IAppBuilder UsePrometheusServer(this IAppBuilder app, Action<PrometheusOptions> setupOptions)
    {
        if (app == null)
            throw new ArgumentNullException(nameof(app));

        var options = new PrometheusOptions();
        setupOptions?.Invoke(options);

        if (!options.MapPath.StartsWith("/"))
            options.MapPath = "/" + options.MapPath;

        if (options.UseDefaultCollectors)
            options.CollectorRegistry.UseDefaultCollectors(options.MetricPrefixName);

        var contentType = options.ResponseEncoding != null
            ? $"{Defaults.ContentType}; charset={options.ResponseEncoding.BodyName}"
            : Defaults.ContentType;

        app.Map(options.MapPath, coreapp =>
        {
            coreapp.Run(async context =>
            {
                var response = context.Response;
                response.ContentType = contentType;

                using var outputStream = response.Body;
                await ScrapeHandler.ProcessAsync(options.CollectorRegistry, outputStream).ConfigureAwait(false);
            });
        });

        return app;
    }
}
