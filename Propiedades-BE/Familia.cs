using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Familia : Componente
    {
        private string _nomfam;

        public string NombreFamilia
        {
            get { return _nomfam; }
            set { _nomfam = value; }
        }

        private IList<Componente> _hijos;
        public Familia() : base()
        {
            _hijos = new List<Componente>();
        }

        public override IList<Componente> Hijos
        {
            get { return _hijos.ToArray(); }
        }

        public override void VaciarHijo()
        {
            _hijos = new List<Componente>();
        }

        public override void AgregarHijo(Componente C)
        {
            _hijos.Add(C);
        }

        public override void EliminarHijo(Componente C)
        {
            _hijos.Remove(C);
        }
    }
}
