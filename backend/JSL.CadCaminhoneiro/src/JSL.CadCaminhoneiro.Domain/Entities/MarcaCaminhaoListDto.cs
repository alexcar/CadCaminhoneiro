using System;
using System.Collections.Generic;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class MarcaCaminhaoListDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        // public ICollection<ModeloCaminhaoListDto> ModelosCaminhaoListDto { get; set; }
    }
}
