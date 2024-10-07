using System.Xml.Linq;
using System.Xml;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransaccionesNTT.Models
{
    [Table("Persona")]
    public class Persona
    {
        //@Id
        //@Column(name = "identificacion", nullable = false, unique = true)
        [Key]
        public string? identificacion { get; set; }
        //@Column(name = "nombre", nullable = true)
        public string? nombre { get; set; }
        //@Column(name = "genero", nullable = true)
        public string? genero { get; set; }
        //@Column(name = "edad", nullable = true)
        public int edad { get; set; }
        //@Column(name = "direccion", nullable = true)
        public string? direccion { get; set; }
        //@Column(name = "telefono", nullable = true)
        public string? telefono { get; set; }

        public ICollection<Cliente> Clientes { get; }
    }
}
