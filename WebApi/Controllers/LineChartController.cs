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
using WebApi.Model;

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
        public async Task<JsonResult> PostAsync()
        {
            var headers = ConvertHeadersToDictionary(HttpContext.Request.Headers);
            dynamic result;

            try
            {
                var graphic = new GraphicGetter(DataConverter, LineChart);
                result = await graphic.GetResultAsync(headers, await GetBodyAsync());
            }
            catch (Exception exception)
            {
                return this.Json(HandleException(exception));
            }
            
            return this.Json(result);
        }

        private dynamic HandleException(Exception exception)
        {
            dynamic errorResult = new ExpandoObject();
            errorResult.Error = exception.Message;
            return errorResult;
        }

        private Dictionary<string, StringValues> ConvertHeadersToDictionary(IHeaderDictionary headerDictionary)
        {
            Dictionary<string, StringValues> result = new Dictionary<string, StringValues>();
            foreach(KeyValuePair<string, StringValues> element in headerDictionary)
            {
                result.Add(element.Key, element.Value);
            }

            return result;
        }

        private async Task<string> GetBodyAsync()
        {
            using (var stream = HttpContext.Request.Body)
            {
                using (var sr = new StreamReader(stream))
                {
                    return await sr.ReadToEndAsync();
                }
            }
        }


    }
}
