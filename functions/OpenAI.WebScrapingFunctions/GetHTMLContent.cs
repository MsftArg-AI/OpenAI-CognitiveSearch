using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace Boti.MVP.Functions
{
    public static class GetHTMLContent
    {
        [FunctionName("GetHTMLContent")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Especificar la URL de la página web
            string url = req.Query["url"];
            string htmlContent = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realizar la solicitud HTTP y obtener la respuesta
                    HttpResponseMessage response = await client.GetAsync(url);
                    // Verificar si la respuesta fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer el contenido HTML de la respuesta
                        htmlContent = await response.Content.ReadAsStringAsync();
                        //Mostrar el contenido HTML en la consola
                    }
                }
                catch (Exception)
                {
                }
            }

            return new OkObjectResult(htmlContent);
        }
    }
}
