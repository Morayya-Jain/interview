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
    private readonly List<DeviceEvent> _events = [];
    private readonly HashSet<string> _eventIds = [];
    private readonly Dictionary<string, int> _eventTypes = [];
    private readonly Dictionary<string, List<DeviceEvent>> _deviceEvents = [];

    public bool AddEvent(DeviceEvent deviceEvent)
    {
        // Checking for duplicate event IDs
        if (_eventIds.Contains(deviceEvent.Id)) return false;
        
        _events.Add(deviceEvent);
        _eventIds.Add(deviceEvent.Id);
        
        // Used for summing events by type
        if (!_eventTypes.ContainsKey(deviceEvent.Type)) _eventTypes[deviceEvent.Type] = 0;
        _eventTypes[deviceEvent.Type]++;

        // Used to allocate events to devices
        if (!_deviceEvents.ContainsKey(deviceEvent.DeviceId)) _deviceEvents[deviceEvent.DeviceId] = [];
        _deviceEvents[deviceEvent.DeviceId].Add(deviceEvent);
        
        return true;
    }

    public List<DeviceEvent> GetEventsForDevice(string deviceId)
    {
        if (!_deviceEvents.ContainsKey(deviceId)) return [];
        return _deviceEvents[deviceId].OrderBy(deviceEvent => deviceEvent.Timestamp).ToList();
    }

    public int CountEventsByType(string type)
    {
        if (!_eventTypes.ContainsKey(type)) return 0;
        return _eventTypes[type];
    }
}