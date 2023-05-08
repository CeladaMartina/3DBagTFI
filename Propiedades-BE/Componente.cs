using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public abstract class Componente
    {
        private string _nom;

        public string Nombre
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public abstract IList<Componente> Hijos { get; }
        public abstract void AgregarHijo(Componente C);
        public abstract void VaciarHijo();

        public abstract void EliminarHijo(Componente C);
        public TipoPermiso Permiso { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
