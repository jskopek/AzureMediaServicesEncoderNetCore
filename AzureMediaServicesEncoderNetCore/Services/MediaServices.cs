using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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
    }
}
