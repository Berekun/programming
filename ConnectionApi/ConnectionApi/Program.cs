using System.Text;
using System.Text.Json;
using static ConnectionApi.Pokemon.Ability;

namespace ConnectionApi
{
    public class Pokemon
    {
        public Ability[] abilities { get; set; }
        public Source[] forms { get; set; }

        public int base_experience { get; set; }

        public class Ability
        {
            public bool is_hidden { get; set; }
            public int slot { get; set; }

            public Source ability { get; set; }

            public class Source
            {
                public string name { get; set; }
                public string url { get; set; }
            }
        }
    }

    public class ResponseTest
    {
        public Dictionary<string, string> args { get; set; }
        public string data { get; set; }
        public Dictionary<string, string> headers { get; set; }
        public string origin { get; set; }
        public string url { set; get; }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HttpClient client = new HttpClient();

                StringContent stringContent = new StringContent("{}", Encoding.UTF8, "application/json");

                var response = client.PostAsync("https://httpbin.org/post?key=value", stringContent).GetAwaiter().GetResult();
                string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                ResponseTest test = JsonSerializer.Deserialize<ResponseTest>(content);

                string json = JsonSerializer.Serialize<ResponseTest>(test);

                //var response = client.GetAsync("https://pokeapi.co/api/v2/pokemon/charmander").GetAwaiter().GetResult();

                //string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                //Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(content);
            }
            catch
            {

            }
        }
    }
}