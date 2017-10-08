using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

namespace WebCoreApplication.Controllers
{
    [Route("api/[controller]")]
    public class CounterController : Controller
    {
        readonly Counter _counter = Metrics.CreateCounter("myCounter", "some help about this");
        private static readonly MetricFactory MetricFactory = Metrics.WithCustomRegistry(Global.MyCollectorRegister);
        private readonly Counter _customCounter = MetricFactory.CreateCounter("customCounter", "test");


        [HttpGet]
        public IActionResult Get()
        {
            _counter.Inc();
            _customCounter.Inc(5);
            return Ok(_counter.Name + " increment +1");
        }
    }
}
