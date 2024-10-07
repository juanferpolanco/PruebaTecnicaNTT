using System.ComponentModel.DataAnnotations;

namespace TransaccionesNTT.Dtos
{
    public class ClientePersonaCrearDto
    {
        //Persona
        [Required(ErrorMessage = "nombre requerido")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "genero requerido")]
        public string genero { get; set; }

        [Required(ErrorMessage = "edad requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "mayor a 0")]
        public int edad { get; set; }

        [Required(ErrorMessage = "identificacion requerido")]
        public string identificacion { get; set; }

        [Required(ErrorMessage = "direccion requerido")]    
        public string direccion { get; set; }

        [Required(ErrorMessage = "telefono requerido")]
        public string telefono { get; set; }

        //Cliente
        [Required(ErrorMessage = "contrasña requerida")]
        public string contrasena { get; set; }
    }
}
