namespace GSP.API.Core.Helpers.ExtensionMethods
{
    public static class JsonExtensions
    {
        public static string ObjectToJson<T>(this T obj) => Newtonsoft.Json.JsonConvert.SerializeObject(obj);

        public static T JsonToObject<T>(this string json) => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }
}
