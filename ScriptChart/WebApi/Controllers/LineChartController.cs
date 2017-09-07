using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataConv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SvgChart;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LineChartController : Controller
    {
        // GET api/linechart
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/linechart/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/linechart
        [HttpPost]
        public ContentResult Post([FromBody]JToken req)
        {
            IDataConverter dataConverter = new DataConverter();
            List<float> data = dataConverter.ConvertData(req, "$..DownloadSpeedInBits");
            ILineChart lineChart = new SvgLineChart();
            string result = lineChart.CreateLineChart(data);

            return this.Content(result, "application/xml");
        }

        // PUT api/linechart/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/linechart/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
