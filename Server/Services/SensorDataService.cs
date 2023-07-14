using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.Models;

namespace Server.Services;

public class SensorDataService
{
    private readonly IMongoCollection<SensorData> _sensorDataCollection;

    public SensorDataService(
        IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _sensorDataCollection = mongoDatabase.GetCollection<SensorData>(
            databaseSettings.Value.CollectionName);
    }

    public async Task<List<SensorData>> GetAsync()
        => await _sensorDataCollection.Find(_ => true).ToListAsync();

    public async Task<SensorData> GetAsync(string id)
        => await _sensorDataCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(SensorData sensorData)
        => await _sensorDataCollection.InsertOneAsync(sensorData);

    public async Task UpdateAsync(string id, SensorData updateSensorData)
        => await _sensorDataCollection.ReplaceOneAsync(x => x.Id == id, updateSensorData);

    public async Task RemoveAsync(string id)
        => await _sensorDataCollection.DeleteOneAsync(x => x.Id == id);
}