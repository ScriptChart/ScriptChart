namespace WebApi.Model
{
    public class ChartCollectionDeserializer
    {
        public string _id { get; set; }
        public float[][] Data { get; set; }
        public string Svg { get; set; }
        public string ChartId { get; set; }
    }
}
