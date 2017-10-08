using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class ChartCollectionDeserializer
    {
        public ObjectId _id { get; set; }
        public string ChartId { get; set; }
        public float[][] Data { get; set; }
        public string Svg { get; set; }
    }
}
