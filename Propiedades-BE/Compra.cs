using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Compra
    {
        private int _idcompra;

        public int IdCompra
        {
            get { return _idcompra; }
            set { _idcompra = value; }
        }

        private int _numcompra;

        public int NumCompra
        {
            get { return _numcompra; }
            set { _numcompra = value; }
        }

        private int _idproveedor;

        public int IdProveedor
        {
            get { return _idproveedor; }
            set { _idproveedor = value; }
        }

        private string _cuit;

        public string CUIT
        {
            get { return _cuit; }
            set { _cuit = value; }
        }

        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private decimal _monto;

        public decimal Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }
    }
}
