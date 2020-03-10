using RestSharp;

namespace Football_API.Models
{
    public class API
    {
        public static string apiKEY = "5bf676a981mshe3181cdd166ef43p158898jsne9c7702a6c2d";

        public string callAPIService(string URL)
        {
            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", apiKEY);
            IRestResponse response = client.Execute(request);
            string JsonStr = response.Content.ToString();
            if (!JsonStr.StartsWith("["))
            {
                JsonStr = "[" + JsonStr;
            }
            if (!JsonStr.EndsWith("]"))
            {
                JsonStr = JsonStr + "]";
            }
            return JsonStr;
        }
    }

}

