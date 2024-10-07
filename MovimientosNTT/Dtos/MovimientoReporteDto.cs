using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovimientosNTT.Dtos
{
    public class MovimientoReporteDto
    {
        [JsonProperty("Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }

        [JsonProperty("Cliente")]
        public string Cliente { get; set; }

        [JsonProperty("Numero Cuenta")]
        public string NumeroCuenta { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }
    
        [JsonProperty("Saldo Inicial")]
        public decimal SaldoInicial { get; set; }

        [JsonProperty("Estado")]
        public bool Estado { get; set; }

        [JsonProperty("Movimiento")]
        public string Movimiento { get; set; }

        [JsonProperty("Saldo Disponible")]
        public decimal SaldoDisponible { get; set; }
    }
}
