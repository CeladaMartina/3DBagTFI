using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Pedido
    {
        private int _idpedido;

        public int IdPedido
        {
            get { return _idpedido; }
            set { _idpedido = value; }
        }

        private int _idarticulo;

        public int IdArticulo
        {
            get { return _idarticulo; }
            set { _idarticulo = value; }
        }

        private int _codProd;

        public int CodProd
        {
            get { return _codProd; }
            set { _codProd = value; }
        }

        private string _NombreProd;

        public string NombreProd
        {
            get { return _NombreProd; }
            set { _NombreProd = value; }
        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
    }
}
