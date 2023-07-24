using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Venta
    {
        private int _idventa;

        public int IdVenta
        {
            get { return _idventa; }
            set { _idventa = value; }
        }
        private int _idcliente;

        public int IdCliente
        {
            get { return _idcliente; }
            set { _idcliente = value; }
        }

        private int _numventa;

        public int NumVenta
        {
            get { return _numventa; }
            set { _numventa = value; }
        }

        private string _dnicliente;

        public string DNICliente
        {
            get { return _dnicliente; }
            set { _dnicliente = value; }
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
