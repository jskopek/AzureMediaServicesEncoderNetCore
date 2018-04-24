using AzureMediaServicesEncoderNetCore.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureMediaServicesEncoderNetCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // this is a big no-no in practice! never hard-code sensitive data or commit it to a repository
            // a better approach would be to use a ConfigurationBuilder instance: 
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration
            string tenantDomain = "-REPLACE ME-";
            string restApiUrl = "-REPLACE ME-";
            string clientId = "-REPLACE ME-";
            string clientSecret = "-REPLACE ME-";

            MediaServices mediaService = new MediaServices(tenantDomain, restApiUrl, clientId, clientSecret);
            await mediaService.InitializeAccessTokenAsync();

            // generate access policy, asset, and locator instances required for file upload
            string accessPolicyId = await mediaService.GenerateAccessPolicy("TestAccessPolicy", 100, 2);
            Asset asset = await mediaService.GenerateAsset("TestAsset", "your-azure-storage-name");
            Locator locator = await mediaService.GenerateLocator(accessPolicyId, asset.Id, DateTime.Now.AddMinutes(-5), 1);

            // generate a file stream for a video
            FileStream fileStream = new FileStream("sample-video.mp4", FileMode.Open);
            StreamContent content = new StreamContent(fileStream);

            // upload the file to azure and generate the asset's file info
            await mediaService.UploadBlobToLocator(content, locator, "sample-video-file.mp4");
            await mediaService.GenerateFileInfo(asset.Id);
        }
    }
}
