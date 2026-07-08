namespace Somnix.DTO.Alertas;

public class AlertaResponse
{
    public string Id { get; set; } = string.Empty;
    public string UsuarioId { get; set; } = string.Empty;
    public string RutaId { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public bool Atendida { get; set; }
    public DateTime FechaRegistro { get; set; }
}