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
using System.IO;
using Microsoft.Extensions.Primitives;
using System.Dynamic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LineChartController : Controller
    {
        IDataConverter DataConverter { get; set; } = new DataConverter();
        ILineChart LineChart { get; set; } = new SvgLineChart();

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
        public JsonResult Post()
        {
            
            string xAxisPath = string.Empty;
            string yAxisPath = string.Empty;
            string result = string.Empty;

            StringValues xJpathCollection;
            StringValues yJpathCollection;

            string body;
            using (var stream = HttpContext.Request.Body)
            {
                using (var sr = new StreamReader(stream))
                {
                    body = sr.ReadToEnd();
                }
            }

            JToken jtoken;
            try
            {
                jtoken = JToken.Parse(body);
            }
            catch (Exception exception)
            {
                dynamic errorResult = new ExpandoObject();
                errorResult.Error = exception.Message;
                return Json(errorResult);
            }

            if (HttpContext.Request.Headers.TryGetValue("X-JPATH-FOR-Y", out yJpathCollection))
            {
                yAxisPath = yJpathCollection.FirstOrDefault();
            }
            else
            {
                throw new HttpRequestException("X-JPATH-FOR-Y header has not been found.");
            }

            if (HttpContext.Request.Headers.TryGetValue("X-JPATH-FOR-X", out xJpathCollection))
            {
                xAxisPath = xJpathCollection.FirstOrDefault();
                var data = DataConverter.ConvertData(jtoken, xAxisPath, yAxisPath);
                result = LineChart.CreateLineChart(data);
            }
            else
            {
                var data = DataConverter.ConvertData(jtoken, yAxisPath);
                result = LineChart.CreateLineChart(data);
            }

            dynamic resultObj = new ExpandoObject();
            resultObj.Svg = result;
            return this.Json(resultObj);
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
