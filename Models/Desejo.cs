using System;

namespace DesejosWebAPI.Models
{
    public class Desejo
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataInserido { get; set; }
        public DateTime? DataExcluido { get; set; }
    }
}
