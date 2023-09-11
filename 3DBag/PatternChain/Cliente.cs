using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DBag.PatternChain
{
    public class Cliente : Aprobador
    {
        public override string Procesar(Usuario usuario)
        {
            return "Cliente";
        }
    }
}