using System.ComponentModel.DataAnnotations;

namespace MovimientosNTT.Dtos
{
    public class CuentaCrearDto
    {
        [Required(ErrorMessage = "numeroCuenta requerido")]
        public string numeroCuenta { get; set; }

        [Required(ErrorMessage = "tipoCuenta requerido")]
        public string tipoCuenta { get; set; }

        [Required(ErrorMessage = "saldoInicial requerido")]
        [Range(0.0, double.MaxValue, ErrorMessage = "El saldo inicial no puede ser menor a 0")]
        public decimal saldoInicial { get; set; }

        [Required(ErrorMessage = "clientId requerido")]
        public string clienteId { get; set; }
    }
}
