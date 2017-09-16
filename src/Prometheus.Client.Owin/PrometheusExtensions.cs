using System.Linq;
using System.Threading.Tasks;
using Prometheus.Client.Collectors;

#if NETSTANDART13
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
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
#if NETSTANDART13

        /// <summary>
        ///     Add PrometheusServer request execution pipeline.
        /// </summary>
        public static IApplicationBuilder UsePrometheusServer(this IApplicationBuilder app)
        {
            return UsePrometheusServer(app, null);
        }
        
        /// <summary>
        ///     Add PrometheusServer request execution pipeline.
        /// </summary>
        public static IApplicationBuilder UsePrometheusServer(this IApplicationBuilder app, PrometheusOptions options)
        {
            if (options == null)
                options = new PrometheusOptions();

            if (options.CollectorRegistryInstance == CollectorRegistry.Instance)
            {
                if (options.Collectors != null && !options.Collectors.Any() && options.UseDefaultCollectors)
                    options.Collectors.AddRange(DefaultCollectors.Get());

                CollectorRegistry.Instance.RegisterOnDemandCollectors(options.Collectors);
            }

            app.Map(string.Format("/{0}", options.MapPath), coreapp =>
            {
                coreapp.Run(async context =>
                {
                    var req = context.Request;
                    var response = context.Response;

                    StringValues acceptHeaders;
                    req.Headers.TryGetValue("Accept", out acceptHeaders);
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
        public static IAppBuilder UsePrometheusServer(this IAppBuilder app, PrometheusOptions options = null)
        {
            if (options == null)
                options = new PrometheusOptions();

            if (options.CollectorRegistryInstance == CollectorRegistry.Instance)
            {
                if (options.Collectors != null && !options.Collectors.Any() && options.UseDefaultCollectors)
                    options.Collectors.AddRange(DefaultCollectors.Get());

                CollectorRegistry.Instance.RegisterOnDemandCollectors(options.Collectors);
            }

            app.Map($"/{options.MapPath}", coreapp =>
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

                    await Task.FromResult(0).ConfigureAwait(false);
                });
            });

            return app;
        }
#endif
    }
}