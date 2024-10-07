using Microsoft.AspNetCore.Mvc;
using TransaccionesNTT.Dtos;
using TransaccionesNTT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TransaccionesNTT.Models;

namespace TransaccionesNTT.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IPersonaRepository personaRepository, IClienteRepository clienteRepository)
        {
            _personaRepository = personaRepository;
            _clienteRepository = clienteRepository;
        }

        [HttpGet("get-clientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            try
            {
                var clientes = await _clienteRepository.ObtenerClientesAsync();

                if (clientes is null || clientes.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-cliente/{id}")]
        public async Task<IActionResult> ObtenerCliente([FromRoute] int id)
        {
            try
            {
                var cliente = await _clienteRepository.ObtenerClientePorIdAsync(id);

                if (cliente is null)
                {
                    return NoContent();
                }
                
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("post-cliente")]
        public async Task<IActionResult> CrearCliente([FromBody] ClientePersonaCrearDto clienteCrearDto)
        {
            try
            {
                bool resultado = await _clienteRepository.CrearClienteAsync(clienteCrearDto);

                if (!resultado)
                {
                    return Conflict("Cliente ya existe");
                }

                return StatusCode(201, "Cliente creado"); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("patch-cliente-estado/{id}/{estado}")]
        public async Task<IActionResult> ActualizarCliente([FromRoute] string id, [FromRoute] string estado)
        {
            try
            {
                bool resultado = await _clienteRepository.ActualizarClienteAsync(id, estado);

                if (!resultado)
                {
                    return NotFound("Cliente no existe");
                }

                return Ok("Cliente actualizado correctamente"); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("patch-cliente/{id}")]
        public async Task<IActionResult> EditarCliente([FromRoute] string id, [FromBody] ClientePersonaEditarDto clienteEditarDto)
        {
            try
            {
                bool resultado = await _clienteRepository.EditarClienteAsync(id, clienteEditarDto);

                if (!resultado)
                {
                    return NotFound("Cliente no existe");
                }

                return Ok("Cliente editado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-cliente/{id}")]
        public async Task<IActionResult> EliminarCliente([FromRoute] string id)
        {
            try
            {
                bool resultado = await _clienteRepository.EliminarClienteAsync(id);

                if (!resultado)
                {
                    return NotFound("Cliente no existe");
                }

                return Ok("Cliente eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
