using Newtonsoft.Json;

namespace WPF_UP.Support;

public static class Serdeser
{
    public static string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}