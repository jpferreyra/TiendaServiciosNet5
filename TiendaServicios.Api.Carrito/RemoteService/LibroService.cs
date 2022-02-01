using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.RemoteInterface;
using TiendaServicios.Api.Carrito.RemoteModels;

namespace TiendaServicios.Api.Carrito.RemoteService
{
    public class LibroService : ILibroService
    {
        private readonly ILogger<LibroService> logger;
        private readonly IHttpClientFactory httpClient;

        public LibroService(ILogger<LibroService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClient = httpClientFactory;
        }

        public async Task<(bool result, LibroRemote libroRemote, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            //objectivo: consumir desde carrito de compras el servicio de libre
            //se comunica con HttpClient
            try
            {
                var cliente = this.httpClient.CreateClient("Libros"); //Url base de la microservice de libro
                var response = await cliente.GetAsync($"api/libro/{LibroId}"); //api/controller
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var libroRemoteResult = JsonSerializer.Deserialize<LibroRemote>(content, options);
                    return (true, libroRemoteResult, "");
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString()); 
                return (false, null, ex.Message);
            }
        }
    }
}
