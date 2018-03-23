using System;
using System.Threading.Tasks;
using Prometheus.Client.Collectors;
#if NETSTANDARD
using Microsoft.AspNetCore.Builder;
#else
using Owin;
#endif

namespace Prometheus.Client.Owin
{
    /// <summary>
    ///     PrometheusExtensions
    /// </summary>
    public static class PrometheusExtensions
    {
#if NETSTANDARD
/// <summary>
///     Add PrometheusServer request execution pipeline.
/// </summary>
        public static IApplicationBuilder UsePrometheusServer(this IApplicationBuilder app)
        {
            return UsePrometheusServer(app, new PrometheusOptions());
        }

        /// <summary>
        ///     Add PrometheusServer request execution pipeline.
        /// </summary>
        public static IApplicationBuilder UsePrometheusServer(this IApplicationBuilder app, PrometheusOptions options)
        {        
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (!options.MapPath.StartsWith("/"))
                options.MapPath = "/" + options.MapPath;
            
            RegisterCollectors(options);

            app.Map(options.MapPath, coreapp =>
            {
                coreapp.Run(async context =>
                {
                    var req = context.Request;
                    var response = context.Response;

                    req.Headers.TryGetValue("Accept", out var acceptHeaders);
                    var contentType = ScrapeHandler.GetContentType(acceptHeaders);

                    response.ContentType = contentType;

                    using (var outputStream = response.Body)
                    {
                        var collected = options.CollectorRegistryInstance.CollectAll();
                        ScrapeHandler.ProcessScrapeRequest(collected, contentType, outputStream);
                    }

                    await Task.FromResult(0).ConfigureAwait(false);
                });
            });

            return app;
        }

#else

        /// <summary>
        ///     Add PrometheusServer request execution pipeline.
        /// </summary>
        public static IAppBuilder UsePrometheusServer(this IAppBuilder app)
        {
            return UsePrometheusServer(app, new PrometheusOptions());
        }

        /// <summary>
        ///     Add PrometheusServer request execution pipeline.
        /// </summary>
        public static IAppBuilder UsePrometheusServer(this IAppBuilder app, PrometheusOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            
            if (!options.MapPath.StartsWith("/"))
                options.MapPath = "/" + options.MapPath;
            
            RegisterCollectors(options);

            app.Map(options.MapPath, coreapp =>
            {
                coreapp.Run(async context =>
                {
                    var req = context.Request;
                    var response = context.Response;

                    var acceptHeader = req.Headers.Get("Accept");
                    var acceptHeaders = acceptHeader?.Split(',');
                    var contentType = ScrapeHandler.GetContentType(acceptHeaders);

                    response.ContentType = contentType;

                    using (var outputStream = response.Body)
                    {
                        var collected = options.CollectorRegistryInstance.CollectAll();
                        ScrapeHandler.ProcessScrapeRequest(collected, contentType, outputStream);
                    }

                    await Task.FromResult(result: 0).ConfigureAwait(continueOnCapturedContext: false);
                });
            });

            return app;
        }
#endif

        private static void RegisterCollectors(PrometheusOptions options)
        {
            if (options.UseDefaultCollectors)
            {
                var metricFactory = Metrics.DefaultFactory;
                if (options.CollectorRegistryInstance != CollectorRegistry.Instance)
                {
                    metricFactory = new MetricFactory(options.CollectorRegistryInstance);
                }

                options.Collectors.AddRange(DefaultCollectors.Get(metricFactory));
            }

            options.CollectorRegistryInstance.RegisterOnDemandCollectors(options.Collectors);
        }
    }
}