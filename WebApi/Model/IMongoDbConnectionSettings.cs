namespace WebApi.Model
{
    public interface IMongoDbConnectionSettings
    {
        string CollectionName { get; }
        string ConnectionString { get; }
        string DbName { get; }
    }
}