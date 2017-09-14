using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DataConv
{
    public class DataConverter : IDataConverter
    {
        public List<float> ConvertData(JToken jToken, string jpath)
        {
            // jpath = $..DownloadSpeedInBits

            var jtokens = jToken.SelectTokens(jpath);
            var jtokenArray = jtokens as JToken[] ?? jtokens.ToArray();
            List<float> result = new List<float>(jtokenArray.Length);
            foreach (JToken jtoken in jtokenArray)
            {
                result.Add(jtoken.Value<float>());
            }

            return result;
        }

        public List<Tuple<float, float>> ConvertData(JToken jtoken, string jpathX, string jpathY)
        {
            List<float> x = ConvertData(jtoken, jpathX);
            List<float> y = ConvertData(jtoken, jpathY);

            List<Tuple<float, float>> xy = new List<Tuple<float, float>>();

            for (int i = 0; i < x.Count; i++)
            {
                xy.Add(new Tuple<float, float>(x[i], y[i]));
            }

            return xy;
        }
    }
}
