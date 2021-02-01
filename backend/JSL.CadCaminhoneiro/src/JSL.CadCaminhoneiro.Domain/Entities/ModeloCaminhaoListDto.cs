using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class ModeloCaminhaoListDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Ano { get; set; }
        public MarcaCaminhaoListDto MarcaCaminhaoListDto { get; set; }
    }
}
