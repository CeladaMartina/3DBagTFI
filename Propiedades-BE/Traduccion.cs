using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Traduccion
    {
        private int _idtraduccion;

        public int IdTraduccion
        {
            get { return _idtraduccion; }
            set { _idtraduccion = value; }
        }

        private int _ididioma;

        public int IdIdioma
        {
            get { return _ididioma; }
            set { _ididioma = value; }
        }

        private int _IdOriginal;

        public int IdOriginal
        {
            get { return _IdOriginal; }
            set { _IdOriginal = value; }
        }


        private string _original;

        public string Original
        {
            get { return _original; }
            set { _original = value; }
        }

        private string _traducido;

        public string Traducido
        {
            get { return _traducido; }
            set { _traducido = value; }
        }

        private string _nombreidioma;

        public string NombreIdioma
        {
            get { return _nombreidioma; }
            set { _nombreidioma = value; }
        }
    }
}
