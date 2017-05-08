using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Prometheus.Client.Owin;
using WebApiApplication;

[assembly: OwinStartup(typeof(Startup))]
namespace WebApiApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UsePrometheusServer(new PrometheusOptions()
            {
                MapPath = "api/metrics"
            });
            app.UseWebApi(config);
          
        }
    }
}