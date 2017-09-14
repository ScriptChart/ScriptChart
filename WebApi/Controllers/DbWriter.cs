﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Model;

namespace WebApi.Controllers
{
    public class DbWriter
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public async Task WriteAsync(string json)
        {
            _client = new MongoClient(MongoDbConnectionSettings.Instance.ConnectionString);
            _database = _client.GetDatabase(MongoDbConnectionSettings.Instance.DbName);
            var collection = _database.GetCollection<BsonDocument>(MongoDbConnectionSettings.Instance.CollectionName);

            var document = BsonDocument.Parse(json);
            await collection.InsertOneAsync(document);
        }
    }
}
