using System;

namespace Notificador.Core.Models
{
    public class Mensaje
    {
        public int Hilos { get; set; }
        public int MensajesCount { get; set; }
        public string Partida { get; set; }
        public string Tipo { get; set; }
    }
}