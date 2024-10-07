using Microsoft.EntityFrameworkCore;
using MovimientosNTT.Data;
using MovimientosNTT.Dtos;
using MovimientosNTT.Interfaces;
using MovimientosNTT.Models;
using System.Collections.Generic;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovimientosNTT.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly ApplicationDbContext _context;

        public MovimientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovimientoReporteDto>> ObtenerMovimeintosAsync(string rangoFechas)
        {
            string[] fechas = rangoFechas.Split("..");

            var query =
                 from m in _context.Movimientos
                 join c in _context.Cuenta on m.CuentaId equals c.numeroCuenta
                 where m.Fecha >= Convert.ToDateTime(fechas[0]) && m.Fecha <= Convert.ToDateTime(fechas[1])
                 select new
                 {
                     Fecha = (DateTime)m.Fecha,
                     Cliente = c.nombreCliente,
                     NumeroCuenta = c.numeroCuenta,
                     Tipo = c.tipoCuenta,
                     SaldoInicial = c.saldoInicial,
                     Estado = c.estado,
                     Movimiento = m.TipoMovimiento,
                     SaldoDisponible = (decimal)m.Saldo
                 };

            //var list = await query.ToListAsync().ConfigureAwait(false);
            var lista = await query.ToListAsync();

            List<MovimientoReporteDto> movimientos = lista
                .Select(r => new MovimientoReporteDto()
                {
                    Fecha = r.Fecha,
                    Cliente = r.Cliente,
                    NumeroCuenta = r.NumeroCuenta,
                    Tipo = r.Tipo,
                    SaldoInicial = r.SaldoInicial,
                    Estado = r.Estado,
                    Movimiento = r.Movimiento,
                    SaldoDisponible = r.SaldoDisponible
                }).ToList();

            return movimientos;
        }

        public async Task<string> CrearMovimeintoAsync(MovimientoCrearDto movimientoCrearDto)
        {
            Cuenta? cuentaGet = await _context.Cuenta.FirstOrDefaultAsync(x => x.numeroCuenta == movimientoCrearDto.cuentaId);

            if (cuentaGet == null) // cuenta no existe
            {
                return "404|La cuenta seleccionada no existe";
            }

            var saldoQuery =
                (from m in _context.Movimientos
                 join c in _context.Cuenta on m.CuentaId equals c.numeroCuenta
                 where c.numeroCuenta == movimientoCrearDto.cuentaId
                 select new
                 {
                     Saldo = m.Saldo,
                     Fecha = m.Fecha
                 });

            decimal? saldo = await saldoQuery.OrderByDescending(x => x.Fecha).Select(x => x.Saldo).FirstOrDefaultAsync();
            decimal saldoDisponible = saldo ?? 0;

            string mensaje = (movimientoCrearDto.valor.CompareTo(decimal.Zero) < 0) ? "Retiro de " : "Deposito de ";

            if (saldo == null) // movimiento desde saldo inicial
            {
                if (movimientoCrearDto.valor >= 0 || cuentaGet.saldoInicial.CompareTo(decimal.Abs(movimientoCrearDto.valor)) >= 0) // saldo inicial mayor/igual que valor a retirar
                {
                    await _context.Movimientos.AddAsync(
                        new Movimientos
                        {
                            CuentaId = movimientoCrearDto.cuentaId,
                            Fecha = DateTime.Now,
                            TipoMovimiento = mensaje + decimal.Abs(movimientoCrearDto.valor),
                            Valor = movimientoCrearDto.valor,
                            Saldo = cuentaGet.saldoInicial + movimientoCrearDto.valor
                        });

                    await _context.SaveChangesAsync();

                    return "200|Transacción exitosa";
                }
                else
                {
                    return "409|Saldo no disponible";
                }
            }
            else if (movimientoCrearDto.valor >= 0 || saldoDisponible.CompareTo(decimal.Abs(movimientoCrearDto.valor)) >= 0) // movimiento desde saldo disponible
            {
                await _context.Movimientos.AddAsync(
                        new Movimientos
                        {
                            CuentaId = movimientoCrearDto.cuentaId,
                            Fecha = DateTime.Now,
                            TipoMovimiento = mensaje + decimal.Abs(movimientoCrearDto.valor),
                            Valor = movimientoCrearDto.valor,
                            Saldo = saldoDisponible + movimientoCrearDto.valor
                        });

                await _context.SaveChangesAsync();

                return "200|Transacción exitosa";
            }
            else
            {
                return "409|Saldo no disponible";
            }
        }

    }
}
