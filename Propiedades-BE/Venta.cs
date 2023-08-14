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
        private int _idusuario;

        public int IdUsuario
        {
            get { return _idusuario; }
            set { _idusuario = value; }
        }

        private int _numventa;

        public int NumVenta
        {
            get { return _numventa; }
            set { _numventa = value; }
        }

        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }        

        private decimal _monto;

        public decimal Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }
    }
}
