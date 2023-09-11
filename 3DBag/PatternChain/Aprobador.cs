using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DBag.PatternChain
{
    public abstract class Aprobador
    {
        protected Aprobador _siguiente;

        public void AgregarSiguiente(Aprobador aprobador)
        {
            _siguiente = aprobador;
        }

        public abstract string Procesar(Usuario usuario);
    }
}