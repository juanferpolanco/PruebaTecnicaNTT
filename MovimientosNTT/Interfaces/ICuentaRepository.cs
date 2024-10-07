using MovimientosNTT.Dtos;

namespace MovimientosNTT.Interfaces
{
    public interface ICuentaRepository
    {
        Task<List<CuentaObtenerDto>> ObtenerCuentasAsync();
        Task<CuentaObtenerDto> ObtenerCuentaPorIdAsync(string? idCuenta);
        Task<string> CrearCuentaAsync(CuentaCrearDto cuentaCrearDto);
        Task<bool> ActualizarCuentaAsync(string idCuenta, string estadoCliente);
        Task<bool> EliminarCuentaAsync(string idCuenta);
    }
}
