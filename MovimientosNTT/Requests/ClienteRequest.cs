using MovimientosNTT.Dtos;
using MovimientosNTT.Interfaces;
using System.Text.Json;

namespace MovimientosNTT.Requests
{
    public class ClienteRequest : IClienteRequest
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClienteRequest(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ClientePersonaObtenerDto?> ObtenerCliente(string? idCliente)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("ClientesClient");
                HttpResponseMessage response = await client.GetAsync($"api/clientes/get-cliente/{idCliente}");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                ClientePersonaObtenerDto cliente = JsonSerializer.Deserialize<ClientePersonaObtenerDto>(json)!;

                return cliente;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
