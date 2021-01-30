using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class CaminhaoListDto
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public byte Eixo { get; set; }
        public string Observacao { get; set; }
        public ModeloCaminhaoListDto MarcaCaminhaoListDto { get; set; }
        public ModeloCaminhaoListDto ModeloCaminhaoListDto { get; set; }
    }
}
