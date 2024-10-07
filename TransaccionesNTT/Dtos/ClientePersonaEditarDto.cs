using System.ComponentModel.DataAnnotations;

namespace TransaccionesNTT.Dtos
{
    public class ClientePersonaEditarDto
    {
        // Persona
        [Required(ErrorMessage = "genero requerido")]
        public string genero { get; set; }

        [Required(ErrorMessage = "edad requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "mayor a 0")]
        public int edad { get; set; }

        [Required(ErrorMessage = "direccion requerida")]
        public string direccion { get; set; }

        [Required(ErrorMessage = "telefono requerido")]
        public string telefono { get; set; }

        // Cliente
        [Required(ErrorMessage = "contrasena requerida")]
        public string contrasena { get; set; }
    }
}
