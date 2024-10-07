using System.Xml.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransaccionesNTT.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("clienteId")]
        public int idCliente { get; set; }
        //@Column(name = "tipoCuenta", nullable = true)
        public string? contrasena { get; set; }
        //@Column(name = "saldoInicial", nullable = true)
        public bool estado { get; set; }
        //@Column(name = "nombreCliente", nullable = true)
        public string personaId { get; set; }
        public Persona Persona { get; set; }
    }
}
