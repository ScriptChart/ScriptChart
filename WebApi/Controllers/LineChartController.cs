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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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
            var headers = this.ConvertHeadersToDictionary(HttpContext.Request.Headers);
            dynamic result;
       
            try
            {
                var graphic = new GraphicGetter(DataConverter, LineChart);
                result = await graphic.GetResultAsync(headers, await this.GetBodyAsync(HttpContext.Request.Body));
            }
            catch (Exception exception)
            {
                return this.Json(this.HandleException(exception));
            }
       
            var dbw = new DbWriter();
            await dbw.WriteAsync(JsonConvert.SerializeObject(result));
       
            return this.Json(result);
        }
    }
}
