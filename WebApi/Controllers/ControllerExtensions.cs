using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public static class ControllerExtensions
    {
        public static dynamic HandleException(this LineChartController ctrl, Exception exception)
        {
            dynamic errorResult = new ExpandoObject();
            errorResult.Error = exception.Message;
            return errorResult;
        }

        public static Dictionary<string, StringValues> ConvertHeadersToDictionary(this LineChartController ctrl, IHeaderDictionary headerDictionary)
        {
            Dictionary<string, StringValues> result = new Dictionary<string, StringValues>();
            foreach (KeyValuePair<string, StringValues> element in headerDictionary)
            {
                result.Add(element.Key, element.Value);
            }

            return result;
        }

        public static async Task<string> GetBodyAsync(this LineChartController ctrl, Stream body)
        {
            using (var stream = body)
            {
                using (var sr = new StreamReader(stream))
                {
                    return await sr.ReadToEndAsync();
                }
            }
        }
    }
}
