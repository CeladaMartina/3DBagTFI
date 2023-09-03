using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Articulo
    {
        private int _idarticulo;

        public int IdArticulo
        {
            get { return _idarticulo; }
            set { _idarticulo = value; }
        }
        private int _idlocalidad;

        public int IdLocalidad
        {
            get { return _idlocalidad; }
            set { _idlocalidad = value; }
        }
        private int _codprod;

        public int CodProd
        {
            get { return _codprod; }
            set { _codprod = value; }
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
        private string _material;

        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }
        private int _talle;

        public int Talle
        {
            get { return _talle; }
            set { _talle = value; }
        }

        private string _nombrel;

        public string NombreLocalidad
        {
            get { return _nombrel; }
            set { _nombrel = value; }
        }

        private int _stock;

        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        private decimal _punit;

        public decimal PUnit
        {
            get { return _punit; }
            set { _punit = value; }
        }

        private string _imagen;

        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }

        private bool _baja;

        public bool BajaLogica
        {
            get { return _baja; }
            set { _baja = value; }
        }

        private int _dvh;

        public int DVH
        {
            get { return _dvh; }
            set { _dvh = value; }
        }
    }
}
