public class DeviceEvent
{
    public string Id { get; set; }
    public string DeviceId { get; set; }
    public string Type { get; set; }
    public DateTime Timestamp { get; set; }

    public DeviceEvent(
        string id,
        string deviceId,
        string type,
        DateTime timestamp)
    {
        Id = id;
        DeviceId = deviceId;
        Type = type;
        Timestamp = timestamp;
    }
}

public class DeviceEventStore
{
    private readonly List<DeviceEvent> _events = new();
    private readonly HashSet<string> _eventIds = new();

    public bool AddEvent(DeviceEvent deviceEvent)
    {
        // TODO
        throw new NotImplementedException();
    }

    public List<DeviceEvent> GetEventsForDevice(string deviceId)
    {
        // TODO
        throw new NotImplementedException();
    }

    public int CountEventsByType(string type)
    {
        // TODO
        throw new NotImplementedException();
    }
}