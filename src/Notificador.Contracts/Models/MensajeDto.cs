namespace Notificador.Contracts.Models
{
    public class MensajeDto
    {
        public int Hilos { get; set; }
        public int MensajesCount { get; set; }
        public string Partida { get; set; }
        public string Tipo { get; set; }
    }
}