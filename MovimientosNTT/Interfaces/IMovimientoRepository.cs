using MovimientosNTT.Dtos;

namespace MovimientosNTT.Interfaces
{
    public interface IMovimientoRepository
    {
        Task<List<MovimientoReporteDto>> ObtenerMovimeintosAsync(string rangoFechas);
        Task<string> CrearMovimeintoAsync(MovimientoCrearDto movimientoCrearDto);
    }
}
