using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientosNTT.Models
{
    [Table("Movimientos")]
    public class Movimientos
    {
        [Key]
        [Column("movimientosId")]
        public int MovimientosId { get; set; }

        [Column("fecha")]
        public DateTime? Fecha { get; set; }

        [Column("tipoMovimiento")]
        public string TipoMovimiento { get; set; }

        [Column("valor")]
        public decimal? Valor { get; set; }

        [Column("saldo")]
        public decimal? Saldo { get; set; }

        [Column("cuentaId")]
        public string CuentaId { get; set; }

        public Cuenta Cuenta { get; set; }
    }
}
