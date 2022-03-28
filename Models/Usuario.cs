using System;

namespace DesejosWebAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataInserido { get; set; }
        public DateTime? DataExcluido { get; set; }
    }
}
