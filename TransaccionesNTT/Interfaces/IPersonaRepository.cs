using TransaccionesNTT.Dtos;
using TransaccionesNTT.Models;

namespace TransaccionesNTT.Interfaces
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> ObtenerPersonasAsync();
    }
}
