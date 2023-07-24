using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Detalle_Venta
    {
        private int _iddetalle;

        public int IdDetalle
        {
            get { return _iddetalle; }
            set { _iddetalle = value; }
        }

        private int _idventa;

        public int IdVenta
        {
            get { return _idventa; }
            set { _idventa = value; }
        }

        private int _idarticulo;

        public int IdArticulo
        {
            get { return _idarticulo; }
            set { _idarticulo = value; }
        }

        private int _codprod;

        public int CodProd
        {
            get { return _codprod; }
            set { _codprod = value; }
        }

        private int _cant;

        public int Cant
        {
            get { return _cant; }
            set { _cant = value; }
        }

        private string _desc;

        public string Descrip
        {
            get { return _desc; }
            set { _desc = value; }
        }

        private decimal _punit;

        public decimal PUnit
        {
            get { return _punit; }
            set { _punit = value; }
        }
        private int _dvh;

        public int DVH
        {
            get { return _dvh; }
            set { _dvh = value; }
        }
    }
}
