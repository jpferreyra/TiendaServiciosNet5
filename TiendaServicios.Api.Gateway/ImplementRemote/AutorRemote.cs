using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.ImplementRemote
{
    public class AutorRemote : IAutorRemote
    {
        private readonly ILogger<AutorRemote> logger;
        private readonly IHttpClientFactory httpClient;

        public AutorRemote(ILogger<AutorRemote> logger, IHttpClientFactory httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
        }
        public async Task<(bool resultado, AutorModeloRemote autor, string ErrorMessage)> GetAutor(Guid autorId)
        {
            try
            {
                var client = httpClient.CreateClient("AutorService"); //--> Startup.cs
                var response = await client.GetAsync($"/autor/{autorId}");

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };   

                    //mappeo to c#
                    var resultado = JsonSerializer.Deserialize<AutorModeloRemote>(contenido, options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false,null,ex.Message);                
            }
        }
    }
}
