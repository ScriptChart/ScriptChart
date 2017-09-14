using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public sealed class MongoDbConnectionSettings : IMongoDbConnectionSettings
    {
        private static volatile IMongoDbConnectionSettings instance;
        private static object syncRoot = new Object();

        private string _connectionString;
        private string _dbname;
        private string _collectionName;

        private MongoDbConnectionSettings()
        {
            _connectionString = GetConnectionString();
        }

        public static IMongoDbConnectionSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MongoDbConnectionSettings();
                    }
                }

                return instance;
            }
        }

        public string ConnectionString { get { return _connectionString; } }
        public string DbName
        {
            get { return _dbname; }
            private set { _dbname = value; }
        }
        public string CollectionName
        {
            get { return _collectionName; }
            private set { _collectionName = value; }
        }

        private string GetConnectionString()
        {

#if DEBUG
            var secret = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\ScriptChart\\secret.json");
            var secretObj = JsonConvert.DeserializeObject<dynamic>(secret);
            DbName = secretObj.DbName;
            CollectionName = secretObj.CollectionName;
            return secretObj.ConnectionString;
#endif

#pragma warning disable CS0162 // Unreachable code detected
            // the code block is only unreachable in debug mode.
            // should be overriden with local secret.json.
            DbName = GetEnvVarValueOrThrowException("MONGODB_DBNAME");
#pragma warning restore CS0162 // Unreachable code detected
            CollectionName = GetEnvVarValueOrThrowException("MONGODB_COLLECTION_NAME");

            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")))
                return Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");

            string host = GetEnvVarValueOrThrowException("MONGODB_HOST");
            string username = GetEnvVarValueOrThrowException("MONGODB_USERNAME");
            string password = GetEnvVarValueOrThrowException("MONGODB_PASSWORD");

            return $"mongodb://{username}:{password}@{host}";
        }

        private string GetEnvVarValueOrThrowException(string envvarname)
        {
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(envvarname)))
                return Environment.GetEnvironmentVariable(envvarname);

            throw new ArgumentException($"<{envvarname}> environment variable has not been found.");
        }
    }
}
