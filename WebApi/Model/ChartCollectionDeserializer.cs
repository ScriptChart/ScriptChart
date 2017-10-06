using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class ChartCollectionDeserializer
    {
        public float[][] Data { get; set; }
        public string Svg { get; set; }
        public string ChartId { get; set; }
    }
}
