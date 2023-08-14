using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio_BLL
{
    public class Producto
    {
        Acceso_DAL.Producto Mapper = new Acceso_DAL.Producto();
        Propiedades_BE.Articulo ArticuloTemp = new Propiedades_BE.Articulo();

        public void EjecutarConsulta(string Consulta)
        {
            Mapper.EjecutarConsulta(Consulta);
        }

        public int SeleccionarIdArticulo(int CodProd)
        {
            return Mapper.SeleccionarIdArticulo(CodProd);
        }

        public int SeleccionarCodArticulo(string DescripcionProducto)
        {
            return Mapper.SeleccionarCodArticulo(DescripcionProducto);
        }

        public string SeleccionarNombreArt(int CodProd)
        {
            return Mapper.SeleccionarNombreArt(CodProd);
        }

        public int VerificarCantStock(int IdArticulo)
        {
            return Mapper.VerificarCantStock(IdArticulo);
        }

        public decimal SeleccionPUnit(int CodProd)
        {
            return Mapper.SeleccionPUnit(CodProd);
        }

        public List<string> CodProdArticulo()
        {
            return Mapper.CodProdArticulo();
        }

        public List<string> DescripcionProd()
        {
            return Mapper.DescripcionProd();
        }


        public int SeleccionarStock(int CodProd)
        {
            return Mapper.SeleccionarStock(CodProd);
        }

        public List<Propiedades_BE.Articulo> Listar()
        {
            List<Propiedades_BE.Articulo> Lista = Mapper.Listar();
            return Lista;
        }

        public List<Propiedades_BE.Articulo> ListarTopProductos()
        {
            List<Propiedades_BE.Articulo> Lista = Mapper.ListarTopProductos();
            return Lista;
        }

        public int Alta(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int IdLocalidad, int Talle, int Stock, decimal PUnit)
        {
            ArticuloTemp.IdArticulo = IdArticulo;
            ArticuloTemp.CodProd = CodProd;
            ArticuloTemp.Nombre = Nombre;
            ArticuloTemp.Descripcion = Descripcion;
            ArticuloTemp.Material = Material;
            ArticuloTemp.IdLocalidad = IdLocalidad;
            ArticuloTemp.Talle = Talle;
            ArticuloTemp.Stock = Stock;
            ArticuloTemp.PUnit = PUnit;

            return Mapper.Alta(ArticuloTemp);
        }

        public int Modificar(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int IdLocalidad, int Talle, int Stock, decimal PUnit)
        {
            ArticuloTemp.IdArticulo = IdArticulo;
            ArticuloTemp.CodProd = CodProd;
            ArticuloTemp.Nombre = Nombre;
            ArticuloTemp.Descripcion = Descripcion;
            ArticuloTemp.Material = Material;
            ArticuloTemp.IdLocalidad = IdLocalidad;
            ArticuloTemp.Talle = Talle;
            ArticuloTemp.Stock = Stock;
            ArticuloTemp.PUnit = PUnit;

            return Mapper.Modificar(ArticuloTemp);
        }

        public int Baja(int IdArticulo)
        {
            ArticuloTemp.IdArticulo = IdArticulo;

            return Mapper.Baja(ArticuloTemp);
        }
    }
}
