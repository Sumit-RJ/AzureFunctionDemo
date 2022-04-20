using azFuncGetGitRepositories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace azFuncGetGitRepositories
{

    public static class GetGitRepositories
    {
        [FunctionName("GetGitReposByUser")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                HttpWebRequest request = WebRequest.Create("https://api.github.com/users/Sumit-RJ/repos") as HttpWebRequest;
                request.UserAgent = "TestApp";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    var content1 = reader.ReadToEnd();
                    dynamic data = JsonConvert.DeserializeObject(content1);
                    log.LogInformation("C# HTTP trigger function processed  successfully.");
                    return new OkObjectResult(data);
                }
            }
            catch (Exception ex)
            {
                log.LogInformation("C# HTTP trigger function request failed.");
                return new BadRequestObjectResult(ex.Message);
            }

        }
    }
}
