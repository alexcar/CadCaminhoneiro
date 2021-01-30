using System;

namespace JSL.CadCaminhoneiro.Core.DomainObjects
{
    public class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataAlteracao { get; protected set; }
    }
}
