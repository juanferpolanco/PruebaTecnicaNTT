using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using TransaccionesNTT.Data;
using TransaccionesNTT.Dtos;
using TransaccionesNTT.Models;
using TransaccionesNTT.Repository;

namespace TransaccionesNTT.Test
{
    public class CLienteRepositoryTest
    {
        private ApplicationDbContext _context;
        private ClienteRepository _clienteRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _clienteRepository = new ClienteRepository(_context);
        }

        [Test]
        public async Task ObtenerClientesAsync_ReturnsListOfClientePersonaDto_WhenClientsAndPersonsExist()
        {
            // Arrange
            var persona = new Persona
            {
                identificacion = "123456",
                nombre = "Juan",
                genero = "M",
                edad = 30,
                direccion = "Calle Falsa 123",
                telefono = "123456789"
            };

            var cliente = new Cliente
            {
                idCliente = 1,
                personaId = "123456",
                estado = true // Activo
            };

            _context.Persona.Add(persona);
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _clienteRepository.ObtenerClientesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ClientePersonaDto>>(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Juan", result[0].nombre);
            Assert.AreEqual("Activo", result[0].estado);
        }

        [Test]
        public async Task ObtenerClientesAsync_ReturnsEmptyList_WhenNoClientsOrPersonsExist()
        {
            // Act
            var result = await _clienteRepository.ObtenerClientesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task ActualizarClienteAsync_ReturnsFalse_WhenClienteNotFound()
        {
            string idCliente = "1"; 
            string estadoCliente = "Activo";

            var result = await _clienteRepository.ActualizarClienteAsync(idCliente, estadoCliente);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EliminarClienteAsync_ReturnsFalse_WhenClienteNotFound()
        {
            string idCliente = "1"; 

            var result = await _clienteRepository.EliminarClienteAsync(idCliente);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EliminarClienteAsync_DeletesClienteAndPersona_WhenBothExist()
        {
            string idCliente = "1";

            var persona = new Persona { identificacion = "123456", nombre = "Test", genero = "M", edad = 30 };
            var cliente = new Cliente { idCliente = 1, personaId = "123456" };

            _context.Persona.Add(persona);
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            var result = await _clienteRepository.EliminarClienteAsync(idCliente);

            Assert.IsTrue(result);

            Assert.IsNull(await _context.Cliente.FirstOrDefaultAsync(c => c.idCliente == 1));
            Assert.IsNull(await _context.Persona.FirstOrDefaultAsync(p => p.identificacion == "123456"));
        }
    }
}