using System.ComponentModel.DataAnnotations;

namespace MovimientosNTT.Dtos
{
    public class MovimientoCrearDto
    {
        [Required(ErrorMessage = "valor requerido")]
        public decimal valor { get; set; }

        [Required(ErrorMessage = "cuenta requerida")]
        public string cuentaId { get; set; }
    }
}
