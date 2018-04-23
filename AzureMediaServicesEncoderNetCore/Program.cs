﻿using System;

namespace AzureMediaServicesEncoderNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // this is a big no-no in practice! never hard-code sensitive data or commit it to a repository
            // a better approach would be to use a ConfigurationBuilder instance: 
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration
            string tenantDomain = "-REPLACE ME-";
            string restApiUrl = "-REPLACE ME-";
            string clientId = "-REPLACE ME-";
            string clientSecret = "-REPLACE ME-";
        }
    }
}
