using Newtonsoft.Json;

namespace TariffComparison.Helper
{
    public class JsonHelper
    {
        public readonly IConfiguration Configuration;

        public JsonHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<dynamic>? LoadJsonData()
        {
            var addressPath = "E:\\providers.json";// Configuration["Settings:AddressPath"];
            using StreamReader r = new(@addressPath);
            string json = r.ReadToEnd();

            return JsonConvert.DeserializeObject<List<dynamic>>(json);
        }
    }
}