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

        public async Task<float[][]> ReadChartAsync(string chartId)
        {
            _client = new MongoClient(MongoDbConnectionSettings.Instance.ConnectionString);
            _database = _client.GetDatabase(MongoDbConnectionSettings.Instance.DbName);
            var collection = _database.GetCollection<ChartCollectionDeserializer>(MongoDbConnectionSettings.Instance.CollectionName);

            var result = await collection.FindAsync(x => x.ChartId.Equals(chartId));
            var data = await result.FirstOrDefaultAsync();
            return data.Data;
        }
    }
}
