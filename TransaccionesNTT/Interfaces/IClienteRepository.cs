using TransaccionesNTT.Dtos;

namespace TransaccionesNTT.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<ClientePersonaDto>> ObtenerClientesAsync();
        Task<ClientePersonaDto> ObtenerClientePorIdAsync(int idCliente);
        Task<bool> CrearClienteAsync(ClientePersonaCrearDto cliente);
        Task<bool> ActualizarClienteAsync(string idCliente, string estadoCliente);
        Task<bool> EditarClienteAsync(string idCliente, ClientePersonaEditarDto clienteEditarDto);
        Task<bool> EliminarClienteAsync(string idCliente);
    }
}
