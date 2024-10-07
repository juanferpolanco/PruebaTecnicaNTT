using Microsoft.EntityFrameworkCore;
using System;
using TransaccionesNTT.Data;
using TransaccionesNTT.Dtos;
using TransaccionesNTT.Interfaces;
using TransaccionesNTT.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransaccionesNTT.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientePersonaDto>> ObtenerClientesAsync()
        {
            List<Cliente> clientes = await _context.Cliente.ToListAsync();
            List<ClientePersonaDto> clientePersonaDtos = 
                (from p in await _context.Persona.ToListAsync()
                join c in clientes on p.identificacion equals c.personaId
                select new ClientePersonaDto
                {
                    nombre = p.nombre,
                    genero = p.genero,
                    edad = p.edad,
                    identificacion = p.identificacion,
                    direccion = p.direccion,
                    telefono = p.telefono,
                    estado = c.estado == true ? "Activo" : "Inactivo",
                }).ToList();

            return clientePersonaDtos;
        }

        public async Task<ClientePersonaDto?> ObtenerClientePorIdAsync(int idCliente)
        {
            Cliente? cliente = await _context.Cliente.Where(x => x.idCliente == idCliente).FirstOrDefaultAsync();
            if (cliente == null) return null;
            Persona? persona = await _context.Persona.Where(x => x.identificacion == cliente.personaId).FirstOrDefaultAsync();
            if (persona == null) return null;

            ClientePersonaDto? clientePersonaDto = 
                new ClientePersonaDto
                {
                    nombre = persona.nombre,
                    genero = persona.genero,
                    edad = persona.edad,
                    identificacion = persona.identificacion,
                    direccion = persona.direccion,
                    telefono = persona.telefono,
                    estado = cliente.estado == true ? "Activo" : "Inactivo",
                };

            return clientePersonaDto;
        }

        public async Task<bool> CrearClienteAsync(ClientePersonaCrearDto clienteCrearDto)
        {
            Persona? personaGet = await _context.Persona.Where(x => x.identificacion == clienteCrearDto.identificacion).FirstOrDefaultAsync();
            if (personaGet != null) return false;

            await _context.Persona.AddAsync(
                new Persona
                {
                    nombre = clienteCrearDto.nombre,
                    genero = clienteCrearDto.genero,
                    edad = clienteCrearDto.edad,
                    identificacion = clienteCrearDto.identificacion,
                    direccion = clienteCrearDto.direccion,
                    telefono = clienteCrearDto.telefono
                });
            await _context.SaveChangesAsync();

            await _context.Cliente.AddAsync(
                new Cliente
                {
                    contrasena = clienteCrearDto.contrasena,
                    estado = true,
                    personaId = clienteCrearDto.identificacion
                });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActualizarClienteAsync(string idCliente, string estadoCliente)
        {
            Cliente? clienteGet = await _context.Cliente.FirstOrDefaultAsync(x => x.idCliente == Int32.Parse(idCliente));
            if (clienteGet == null) return false;

            //bool estadoBool = estadoCliente == "Activo" ? true : false;

            clienteGet.estado = bool.Parse(estadoCliente);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditarClienteAsync(string idCliente, ClientePersonaEditarDto clienteEditarDto)
        {
            Cliente? clienteGet = await _context.Cliente.FirstOrDefaultAsync(x => x.idCliente == Int32.Parse(idCliente));
            if (clienteGet == null) return false;

            Persona? personaGet = await _context.Persona.FirstOrDefaultAsync(x => x.identificacion == clienteGet.personaId);

            clienteGet.contrasena = clienteEditarDto.contrasena;

            personaGet.edad = clienteEditarDto.edad;
            personaGet.direccion = clienteEditarDto.direccion;
            personaGet.telefono = clienteEditarDto.telefono;
            personaGet.genero = clienteEditarDto.genero;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarClienteAsync(string idCliente)
        {
            Cliente? clienteGet = await _context.Cliente.FirstOrDefaultAsync(x => x.idCliente == Int32.Parse(idCliente));
            if (clienteGet == null) return false;

            Persona? personaGet = await _context.Persona.FirstOrDefaultAsync(x => x.identificacion == clienteGet.personaId);

            _context.Remove(clienteGet);
            _context.Remove(personaGet);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
