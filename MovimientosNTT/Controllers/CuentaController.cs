using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MovimientosNTT.Dtos;
using MovimientosNTT.Interfaces;
using System.Net;

namespace MovimientosNTT.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;

        public CuentaController(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        [HttpGet("get-cuentas")]
        public async Task<IActionResult> ObtenerCuentas()
        {
            try
            {
                var cuentas = await _cuentaRepository.ObtenerCuentasAsync();

                if (cuentas is null || cuentas.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(cuentas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-cuenta/{id}")]
        public async Task<IActionResult> ObtenerCuenta([FromRoute] string id)
        {
            try
            {
                var cuenta = await _cuentaRepository.ObtenerCuentaPorIdAsync(id);

                if (cuenta is null)
                {
                    return NoContent();
                }

                return Ok(cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("post-cuenta")]
        public async Task<IActionResult> CrearCuenta([FromBody] CuentaCrearDto cuentaCrearDto)
        {
            try
            {
                string cuenta = await _cuentaRepository.CrearCuentaAsync(cuentaCrearDto);
                string[] parametros = cuenta.Split('|');

                return StatusCode(Int32.Parse(parametros[0]), parametros[1]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("patch-cuenta-estado/{id}/{estado}")]
        public async Task<IActionResult> ActualizarCliente([FromRoute] string id, [FromRoute] string estado)
        {
            try
            {
                bool resultado = await _cuentaRepository.ActualizarCuentaAsync(id, estado);

                if (!resultado)
                {
                    return NotFound("Cuenta no existe");
                }

                return Ok("Cuenta actualizada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-cuenta/{id}")]
        public async Task<IActionResult> EliminarCuenta([FromRoute] string id)
        {
            try
            {
                bool resultado = await _cuentaRepository.EliminarCuentaAsync(id);

                if (!resultado)
                {
                    return NotFound("Cuenta no existe");
                }

                return Ok("Cuenta eliminada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
