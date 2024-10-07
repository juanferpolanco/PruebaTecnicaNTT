using MovimientosNTT.Data;
using MovimientosNTT.Dtos;
using Microsoft.EntityFrameworkCore;
using MovimientosNTT.Interfaces;
using MovimientosNTT.Models;
using MovimientosNTT.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MovimientosNTT.Repository
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IClienteRequest _clienteRequest;

        public CuentaRepository(ApplicationDbContext context, IClienteRequest clienteRequest)
        {
            _context = context;
            _clienteRequest = clienteRequest;
        }

        public async Task<List<CuentaObtenerDto>> ObtenerCuentasAsync()
        {
            List<CuentaObtenerDto> cuentasDtos =
                (from c in await _context.Cuenta.ToListAsync()
                 select new CuentaObtenerDto
                 {
                     numeroCuenta = c.numeroCuenta,
                     tipoCuenta = c.tipoCuenta,
                     saldoInicial = c.saldoInicial,
                     nombreCliente = c.nombreCliente,
                     estado = c.estado == true ? "Activo" : "Inactivo",
                 }).ToList();

            return cuentasDtos;
        }

        public async Task<CuentaObtenerDto> ObtenerCuentaPorIdAsync(string idCuenta)
        {
            Cuenta? cuenta = await _context.Cuenta.FirstOrDefaultAsync(x => x.numeroCuenta == idCuenta);
            if (cuenta == null) return null;

            CuentaObtenerDto cuentaDto =
                new CuentaObtenerDto
                {
                    numeroCuenta = cuenta.numeroCuenta,
                    tipoCuenta = cuenta.tipoCuenta,
                    saldoInicial = cuenta.saldoInicial,
                    nombreCliente = cuenta.nombreCliente,
                    estado = cuenta.estado == true ? "Activo" : "Inactivo",
                };

            return cuentaDto;
        }

        public async Task<string> CrearCuentaAsync(CuentaCrearDto cuentaCrearDto)
        {
            Cuenta? cuenta = await _context.Cuenta.FirstOrDefaultAsync(x => x.numeroCuenta == cuentaCrearDto.numeroCuenta);
            if (cuenta != null) return "409|Cuenta ya existe";

            ClientePersonaObtenerDto cliente = await _clienteRequest.ObtenerCliente(cuentaCrearDto.clienteId);
            if (cliente == null) return "409|El cliente no se encuentra";
            if (cliente.estado == "Inactivo") return "409|El cliente se encuentra inactivo";

            await _context.Cuenta.AddAsync(
                new Cuenta
                {
                    numeroCuenta = cuentaCrearDto.numeroCuenta,
                    tipoCuenta = cuentaCrearDto.tipoCuenta,
                    saldoInicial = cuentaCrearDto.saldoInicial,
                    estado = true,
                    nombreCliente = cliente.nombre
                });
            await _context.SaveChangesAsync();

            return "201|Cuenta creada";
        }

        public async Task<bool> ActualizarCuentaAsync(string idCuenta, string estadoCliente)
        {
            Cuenta? cuentaGet = await _context.Cuenta.FirstOrDefaultAsync(x => x.numeroCuenta == idCuenta);
            if (cuentaGet == null) return false;

            cuentaGet.estado = bool.Parse(estadoCliente);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarCuentaAsync(string idCuenta)
        {
            Cuenta? cuentaGet = await _context.Cuenta.FirstOrDefaultAsync(x => x.numeroCuenta == idCuenta);
            if (cuentaGet == null) return false;

            _context.Remove(cuentaGet);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
