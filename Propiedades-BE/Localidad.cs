using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Localidad
    {
        private int _idlocalidad;

        public int IdLocalidad
        {
            get { return _idlocalidad; }
            set { _idlocalidad = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private int _codpostal;

        public int CodPostal
        {
            get { return _codpostal; }
            set { _codpostal = value; }
        }
        private string _partido;

        public string Partido
        {
            get { return _partido; }
            set { _partido = value; }
        }
        private bool _baja;

        public bool BajaLogica
        {
            get { return _baja; }
            set { _baja = value; }
        }
    }
}
