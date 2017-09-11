using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json.Linq;

namespace DataConv
{
    public interface IDataConverter
    {
        List<float> ConvertData(JToken jtoken, string jpath);
        List<Tuple<float, float>> ConvertData(JToken jtoken, string jpathX, string jpathY);
    }
}