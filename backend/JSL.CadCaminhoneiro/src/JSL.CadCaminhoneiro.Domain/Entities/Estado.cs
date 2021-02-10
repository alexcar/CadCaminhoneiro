using System;
using System.Collections.Generic;
using System.Text;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class Estado
    {
        public Estado()
        {

        }
        //protected Estado() { }
        public string Uf { get; set; }
        public string Descricao { get; set; }
    }
}
