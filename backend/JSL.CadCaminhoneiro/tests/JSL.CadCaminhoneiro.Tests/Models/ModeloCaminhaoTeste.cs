using System;

namespace JSL.CadCaminhoneiro.Tests.Models
{
    public class ModeloCaminhaoTeste
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Ano { get; set; }
        public Guid MarcaCaminhaoId { get; set; }
    }
}
