using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovimientosNTT.Dtos;
using MovimientosNTT.Interfaces;

namespace MovimientosNTT.Controllers
{
    [Route("movimientos")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientosController(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        [HttpGet("reportes")]
        public async Task<IActionResult> ObtenerMovimientos([FromQuery] string fecha)
        {
            return Ok(await _movimientoRepository.ObtenerMovimeintosAsync(fecha));
        }

        [HttpPost("post-movimiento")]
        public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoCrearDto movimientoCrearDto)
        {
            string resultado = await _movimientoRepository.CrearMovimeintoAsync(movimientoCrearDto);
            string[] parametros = resultado.Split('|');

            return StatusCode(Int32.Parse(parametros[0]), parametros[1]);
        }

    }
}
