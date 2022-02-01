using System;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.RemoteModels;

namespace TiendaServicios.Api.Carrito.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool result, LibroRemote libroRemote, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
