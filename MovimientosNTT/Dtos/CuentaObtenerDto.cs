namespace MovimientosNTT.Dtos
{
    public class CuentaObtenerDto
    {
        public string? numeroCuenta { get; set; }
        public string? tipoCuenta { get; set; }
        public decimal saldoInicial { get; set; }
        public string? estado { get; set; }
        public string? nombreCliente { get; set; }
    }
}
