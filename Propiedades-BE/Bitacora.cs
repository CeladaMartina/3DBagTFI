using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Bitacora
    {
        private int _idbitacora;

        public int IdBitacora
        {
            get { return _idbitacora; }
            set { _idbitacora = value; }
        }

        private int _idusuario;

        public int IdUsuario
        {
            get { return _idusuario; }
            set { _idusuario = value; }
        }

        private string _nickus;

        public string NickUs
        {
            get { return _nickus; }
            set { _nickus = value; }
        }

        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private string _criticidad;

        public string Criticidad
        {
            get { return _criticidad; }
            set { _criticidad = value; }
        }

        private int _dvh;
        public int DVH
        {
            get { return _dvh; }
            set { _dvh = value; }
        }

        private long _fechaB;

        public long FechaB
        {
            get { return _fechaB; }
            set { _fechaB = value; }
        }
    }
}
