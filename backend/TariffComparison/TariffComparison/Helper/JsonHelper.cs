using Newtonsoft.Json;

namespace TariffComparison.Helper
{
    public class JsonHelper
    {
        public readonly IConfiguration _configuration;

        public JsonHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<dynamic>? LoadJsonData()
        {
            var addressPath = "E:\\providers.json";
            using StreamReader r = new(@addressPath);
            string json = r.ReadToEnd();

            return JsonConvert.DeserializeObject<List<dynamic>>(json);
        }
    }
}