using JSL.CadCaminhoneiro.Core.DomainObjects;
using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class Caminhao : Entity
    {
        public Caminhao(
            string placa,
            byte eixo, // sql server tinyint
            string observacao,
            MarcaCaminhao marcaCaminhao,
            ModeloCaminhao modeloCaminhao)
        {
            Placa = placa;
            Eixo = eixo;
            Observacao = observacao;
            MarcaCaminhao = marcaCaminhao;
            ModeloCaminhao = modeloCaminhao;
        }
        
        protected Caminhao() { }
        public string Placa { get; private set; }
        public byte Eixo { get; private set; }
        public string Observacao { get; private set; }
        
        public Guid MotoristaId { get; private set; }
        public Motorista Motorista { get; private set; }
        
        public Guid MarcaCaminhaoId { get; private set; }
        public MarcaCaminhao MarcaCaminhao { get; private set; }

        public Guid ModeloCaminhaoId { get; private set; }
        public ModeloCaminhao ModeloCaminhao { get; private set; }

        public void Incluir(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }

        public void Alterar(
            string placa,
            byte eixo,
            string observacao,
            MarcaCaminhao marcaCaminhao,
            ModeloCaminhao modeloCaminhao,
            DateTime? dataAlteracao)
        {
            Placa = placa;
            Eixo = eixo;
            Observacao = observacao;
            MarcaCaminhao = marcaCaminhao;
            ModeloCaminhao = modeloCaminhao;
            DataAlteracao = dataAlteracao;
        }
    }
}
