using System;
using System.Threading.Tasks;
using Prometheus.Client.Collectors;
using Owin;

namespace Prometheus.Client.Owin
{
    /// <summary>
    ///     PrometheusExtensions
    /// </summary>
    public static class PrometheusExtensions
    {
        /// <summary>
        ///     Add PrometheusServer request execution pipeline.
        /// </summary>
        public static IAppBuilder UsePrometheusServer(this IAppBuilder app)
        {
            return UsePrometheusServer(app, null);
        }

        /// <summary>
        ///     Add PrometheusServer request execution pipeline.
        /// </summary>
        public static IAppBuilder UsePrometheusServer(this IAppBuilder app, Action<PrometheusOptions> setupOptions)
        {
            var options = new PrometheusOptions();
            setupOptions?.Invoke(options);
            
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            
            if (!options.MapPath.StartsWith("/"))
                throw new ArgumentException($"MapPath '{options.MapPath}' should start with '/'");
            
            if (options.UseDefaultCollectors)
                options.CollectorRegistryInstance.UseDefaultCollectors();

            app.Map(options.MapPath, coreapp =>
            {
                coreapp.Run(async context =>
                {
                    var response = context.Response;
                    response.ContentType = "text/plain; version=0.0.4";
                    
                    using (var outputStream = response.Body)
                    {
                        await ScrapeHandler.ProcessAsync(options.CollectorRegistryInstance, outputStream);
                    }

                    await Task.FromResult(0).ConfigureAwait(false);
                });
            });

            return app;
        }
    }
}
