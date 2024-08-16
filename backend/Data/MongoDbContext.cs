using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDb"));
        _database = client.GetDatabase("wayni"); //nombre de la db en Mongodb
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users"); //documento de la db
}
