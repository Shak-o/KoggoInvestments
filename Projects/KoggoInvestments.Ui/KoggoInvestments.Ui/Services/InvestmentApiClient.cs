using KoggoInvestments.Ui.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TodoREST;

namespace KoggoInvestments.Ui.Services
{
    public class InvestmentApiClient : IInvestmentApiClient
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<CheckStatusResponse> Items { get; private set; }

        public InvestmentApiClient()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _client = new HttpClient(insecureHandler);
#else
            _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public List<CheckStatusResponse> GetMarketInfoAsync()
        {
            Items = new List<CheckStatusResponse>();

            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = _client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    Items = JsonSerializer.Deserialize<List<CheckStatusResponse>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }
    }
}