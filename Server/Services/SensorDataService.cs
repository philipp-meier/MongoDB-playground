using Server.Models;
using MongoDB.Driver;
using Server.Extensions;
using Microsoft.Extensions.Options;

namespace Server.Services;

public class SensorDataService
{
    private readonly IMongoCollection<SensorData> _sensorDataCollection;

    public SensorDataService(IOptions<DatabaseSettings> settingsOption)
    {
        var settings = settingsOption.Value;
        var mongoClient = new MongoClient(settings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

        var collectionName = settings.CollectionName;
        if (!CollectionExists(mongoDatabase, collectionName))
        {
            // Create time series collection.
            mongoDatabase.CreateCollection(collectionName, new CreateCollectionOptions
            {
                TimeSeriesOptions = new TimeSeriesOptions("timestamp", "sensorId", TimeSeriesGranularity.Seconds)
            });
        }

        _sensorDataCollection = mongoDatabase.GetCollection<SensorData>(settings.CollectionName);
    }
    private bool CollectionExists(IMongoDatabase database, string name)
        => database.ListCollectionNames().ToList().Contains(name);

    public async Task ImportAsync(SensorData[] data, CancellationToken cancellationToken)
    {
        var entries = data.Select(x => new InsertOneModel<SensorData>(x));
        await _sensorDataCollection.BulkWriteAsync(entries, new BulkWriteOptions { IsOrdered = false }, cancellationToken);
    }

    public async Task<SensorStatistic> GetStatistic(string sensorId, DateTime from, DateTime to, CancellationToken ct)
    {
        var filter = Builders<SensorData>.Filter
            .Where(r => r.Metadata.SensorId == sensorId && r.Timestamp >= from && r.Timestamp <= to);

        var collection = _sensorDataCollection;
        return new SensorStatistic
        {
            SensorId = sensorId,
            Count = await collection.CountDocumentsAsync(filter, new CountOptions { Limit = null }, ct),
            From = (await collection.MinBy(filter, "Timestamp", ct))?.Timestamp,
            To = (await collection.MaxBy(filter, "Timestamp", ct))?.Timestamp,
            // Measures
            Temp = await collection.CreateMeasureStatisticFor<float?>("Temperature", filter, ct),
            A = await collection.CreateMeasureStatisticFor<int?>("A", filter, ct),
            B = await collection.CreateMeasureStatisticFor<int?>("B", filter, ct),
            C = await collection.CreateMeasureStatisticFor<int?>("C", filter, ct),
            D = await collection.CreateMeasureStatisticFor<int?>("D", filter, ct),
            E = await collection.CreateMeasureStatisticFor<int?>("E", filter, ct),
            F = await collection.CreateMeasureStatisticFor<int?>("F", filter, ct),
            G = await collection.CreateMeasureStatisticFor<int?>("G", filter, ct),
            H = await collection.CreateMeasureStatisticFor<int?>("H", filter, ct),
            I = await collection.CreateMeasureStatisticFor<int?>("I", filter, ct),
            J = await collection.CreateMeasureStatisticFor<int?>("J", filter, ct),
            K = await collection.CreateMeasureStatisticFor<int?>("K", filter, ct),
            L = await collection.CreateMeasureStatisticFor<int?>("L", filter, ct),
        };
    }
}
