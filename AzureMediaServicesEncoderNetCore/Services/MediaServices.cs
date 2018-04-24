using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AzureMediaServicesEncoderNetCore.Services
{
    class MediaServices
    {
        private string _tenantDomain;
        private string _restApiUrl;
        private string _clientId;
        private string _clientSecret;
        private HttpClient _httpClient;

        public MediaServices(string tenantDomain, string restApiUrl, string clientId, string clientSecret)
        {
            _tenantDomain = tenantDomain;
            _restApiUrl = restApiUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;

            _httpClient = new HttpClient { BaseAddress = new Uri(restApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("x-ms-version", "2.15");
            _httpClient.DefaultRequestHeaders.Add("DataServiceVersion", "3.0");
            _httpClient.DefaultRequestHeaders.Add("MaxDataServiceVersion", "3.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
        }

        public async Task InitializeAccessTokenAsync()
        {
            // based on code from Shawn Mclean's azure-media-services-core codebase: https://github.com/shawnmclean/azure-media-services-core
            // generate access token
            var body = $"resource={HttpUtility.UrlEncode("https://rest.media.azure.net")}&client_id={_clientId}&client_secret={HttpUtility.UrlEncode(_clientSecret)}&grant_type=client_credentials";
            var httpContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _httpClient.PostAsync($"https://login.microsoftonline.com/{_tenantDomain}/oauth2/token", httpContent);
            if (!response.IsSuccessStatusCode) throw new Exception();
            var resultBody = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(resultBody);

            // set internal httpClient authorization headers to access token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", obj["access_token"].ToString());
        }
    }
}
