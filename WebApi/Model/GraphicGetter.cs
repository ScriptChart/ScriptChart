using DataConv;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using SvgChart;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class GraphicGetter : IGraphicGetter
    {
        private IDataConverter _dataConverter { get; set; } = new DataConverter();
        private ILineChart _lineChart { get; set; } = new SvgLineChart();

        private const string XHeader = "X-JPATH-FOR-X";
        private const string YHeader = "X-JPATH-FOR-Y";

        public GraphicGetter(IDataConverter dataConverter, ILineChart lineChart)
        {
            _dataConverter = dataConverter;
            _lineChart = lineChart;
        }

        public async Task<dynamic> GetResultAsync(Dictionary<string, StringValues> headers, string body)
        {
            return await Task.Factory.StartNew<dynamic>(() =>
            {
                string xAxisPath = string.Empty;
                string yAxisPath = string.Empty;
                string result = string.Empty;

                string hashId = new HashidsNet.Hashids().EncodeLong(new long[] { new Random().Next(), DateTime.Now.Ticks });

                StringValues xJpathCollection;
                StringValues yJpathCollection;

                JToken jtoken = JToken.Parse(body);

                if (headers.TryGetValue(YHeader, out yJpathCollection))
                {
                    yAxisPath = yJpathCollection.FirstOrDefault();
                }
                else
                {
                    throw new HttpRequestException($"{YHeader} header has not been found.");
                }

                if (headers.TryGetValue(XHeader, out xJpathCollection))
                {
                    xAxisPath = xJpathCollection.FirstOrDefault();
                    var data = _dataConverter.ConvertData(jtoken, xAxisPath, yAxisPath);
                    result = _lineChart.CreateLineChart(data);
                }
                else
                {
                    var data = _dataConverter.ConvertData(jtoken, yAxisPath);
                    result = _lineChart.CreateLineChart(data);
                }

                dynamic resultObj = new ExpandoObject();

                resultObj.Svg = result;
                resultObj.ChartId = hashId;
                return resultObj;
            });
        }
    }
}
