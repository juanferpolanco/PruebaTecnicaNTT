using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientosNTT.Models
{
    [Table("Cuenta")]
    public class Cuenta
    {
        [Key]
        public string numeroCuenta { get; set; }
        public string tipoCuenta { get; set; }
        public decimal saldoInicial { get; set; }
        public bool estado { get; set; }
        public string nombreCliente { get; set; }

        public ICollection<Movimientos> Movimientos { get; }
    }
}
