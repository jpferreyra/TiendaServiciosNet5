using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.MessageHandler
{
    public class LibroHandler : DelegatingHandler
    {
        private readonly ILogger<LibroHandler> logger;
        private readonly IAutorRemote autorRemote;

        public LibroHandler(ILogger<LibroHandler> logger, IAutorRemote autorRemote)
        {
            this.logger = logger;
            this.autorRemote = autorRemote;
        }

        //Handler para interceptar llamadas http entre el usuario y mi Gateway
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tiempo = Stopwatch.StartNew();
            this.logger.LogInformation("Inicia el request");
            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive  =true};

                var resul = JsonSerializer.Deserialize<LibroModeloRemote>(data, options);

                var reponseAutor = await autorRemote.GetAutor(resul.AutorLibro ?? Guid.Empty);
                if (reponseAutor.resultado)
                {
                    var Autor = reponseAutor.autor;
                    resul.AutorData = Autor;
                    var resultadoStr = JsonSerializer.Serialize(resul); 

                    //inclusion de data del autor
                    response.Content = new StringContent("", Encoding.UTF8, "application/json");
                }
            }
            


            this.logger.LogInformation($"Este proceso se hizo en {tiempo.ElapsedMilliseconds}ms");
            return response;
        }
    }
}
