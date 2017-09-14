using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApi.Controllers
{
    public class DbWriter
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public void Write(string json)
        {
            _client = new MongoClient("mongodb://root:xxxx@xxx.xxx:27017");
            //_database = _client.GetDatabase("scriptchart");

            _database = _client.GetDatabase("admin");

            var cols = _database.ListCollections();

            var collection = _database.GetCollection<BsonDocument>("charts");

            var document = BsonDocument.Parse(json);
            collection.InsertOne(document);

        }

    }
}
