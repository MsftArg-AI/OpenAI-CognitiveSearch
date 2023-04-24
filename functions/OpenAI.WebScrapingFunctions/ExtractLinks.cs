using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace Boti_MVP
{
    public static class ExtractLinks
    {
        [FunctionName("ExtractLinks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string host = req.Query["host"];
            string path = req.Query["path"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync(); 
            // Get the input text from the request
            string inputText = requestBody; 
            if (inputText == null) {
                return new BadRequestObjectResult("Please pass a text in the request body."); 
            } 
            var regex = new Regex(@"\[(.*?)\]"); 
            var matches = regex.Matches(inputText); 
            if(path == null)
                return new OkObjectResult(matches.Where(m => m.Groups[1].Value.StartsWith("/") || m.Groups[1].Value.StartsWith(host)).Select(m => m.Groups[1].Value.Replace(host,"")));
            else
                return new OkObjectResult(matches.Where(m => m.Groups[1].Value.StartsWith(path) || m.Groups[1].Value.StartsWith(host+path)).Select(m => m.Groups[1].Value.Replace(host, "")));

        }
    }
}
