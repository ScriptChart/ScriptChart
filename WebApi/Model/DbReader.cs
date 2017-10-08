using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Model;
using System.Collections;
using MongoDB.Bson.Serialization;

namespace WebApi.Controllers
{
    public class DbReader : IDbReader
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public float[][] ReadChart(string chartId)
        {
            _client = new MongoClient(MongoDbConnectionSettings.Instance.ConnectionString);
            _database = _client.GetDatabase(MongoDbConnectionSettings.Instance.DbName);
            var collection = _database.GetCollection<ChartCollectionDeserializer>(MongoDbConnectionSettings.Instance.CollectionName);

            var result = collection.Find<ChartCollectionDeserializer>(x => x.ChartId.Equals(chartId));
            var data = result.FirstOrDefault();
            return data?.Data;
        }
    }
}
