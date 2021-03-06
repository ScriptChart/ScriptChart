﻿using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Model;

namespace WebApi.Controllers
{
    public class DbWriter : IDbWriter
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public async Task WriteAsync(string json)
        {
            _client = new MongoClient(MongoDbConnectionSettings.Instance.ConnectionString);
            _database = _client.GetDatabase(MongoDbConnectionSettings.Instance.DbName);
            var collection = _database.GetCollection<ChartCollectionDeserializer>(MongoDbConnectionSettings.Instance.CollectionName);

            var document =  Newtonsoft.Json.JsonConvert.DeserializeObject<ChartCollectionDeserializer>(json);
            await collection.InsertOneAsync(document);
        }
    }
}
