using TransaccionesNTT.Data;
using TransaccionesNTT.Interfaces;
using TransaccionesNTT.Models;
using Microsoft.EntityFrameworkCore;
using TransaccionesNTT.Dtos;

namespace TransaccionesNTT.Repository
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> ObtenerPersonasAsync()
        {
            List<Persona> personas = await _context.Persona.ToListAsync();
            List<ClientePersonaDto> clientes = (from p in personas
                                               select new ClientePersonaDto {
                                                   nombre = p.nombre
                                               }).ToList();
            return personas;
        }
    }
}
