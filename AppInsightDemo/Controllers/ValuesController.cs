using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppInsightDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ILogger _logger;
        private TelemetryClient _telemetry;
        public ValuesController(ILogger<ValuesController> logger, TelemetryClient telemetry)
        {
            _logger = logger;
            _telemetry = telemetry;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _telemetry.TrackEvent("api/values/get"); // Will be available in CustomEvents table in Azure Logs(Analytics)

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            _telemetry.GetMetric("AggregateMetricId").TrackValue(id);  //Aggregate by common Id  and sent automaticaly every 1 min.

            //TrackTrace Demo
            _telemetry.TrackTrace("Message_A");
            _telemetry.TrackTrace("Message_B", Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Warning);
            _telemetry.TrackTrace(new TraceTelemetry("Message_C"));

            var properties = new Dictionary<string, string> {{"name", "ValueDemo"}, {"id", $"{id}"}};

            _telemetry.TrackTrace("Message_D", properties);


            //TrackException

            try
            {
                var div = 18 / (666 - id);
            }
            catch (Exception ex)
            {
                var measurements = new Dictionary<string, double> {{"tid", this.HttpContext.Items.Count}}; //for test purpose only!

                _telemetry.TrackException(ex, properties, measurements);
            }


            _logger.LogWarning($" Only warning or higher are automartically collected as default. Call: api/values/get/{id}");

            _logger.LogInformation($"Now also collected by applicationInsight (see Program.cs). Call: api/values/get/{id}");

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
