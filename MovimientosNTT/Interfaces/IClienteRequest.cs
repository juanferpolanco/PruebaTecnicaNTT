using MovimientosNTT.Dtos;

namespace MovimientosNTT.Interfaces
{
    public interface IClienteRequest
    {
        Task<ClientePersonaObtenerDto?> ObtenerCliente(string idCliente);
    }
}
