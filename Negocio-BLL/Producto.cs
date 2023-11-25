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
        Acceso_DAL.Producto producto = new Acceso_DAL.Producto();
        Acceso_DAL.Seguridad Seguridad = new Acceso_DAL.Seguridad();
        long DV = 0;

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

        public List<Propiedades_BE.Articulo> consultarCodProd(int CodProd)
        {
            return Mapper.consultarCodProd(CodProd);
        }

        //public List<Propiedades_BE.Articulo> ListarTopProductos()
        //{
        //    List<Propiedades_BE.Articulo> Lista = Mapper.ListarTopProductos();
        //    return Lista;
        //}

        public List<Propiedades_BE.Articulo> TopListaProd()
        {
            return Mapper.ListarTopProductos();
        }

        public List<Propiedades_BE.Articulo> ProdMasBarato()
        {
            return Mapper.ProdMasBarato();
        }

        public int Alta(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int Stock, decimal PUnit, byte[] Imagen, int DVH)
        {
            ArticuloTemp.IdArticulo = IdArticulo;
            ArticuloTemp.CodProd = CodProd;
            ArticuloTemp.Nombre = Nombre;
            ArticuloTemp.Descripcion = Descripcion;
            ArticuloTemp.Material = Material;                    
            ArticuloTemp.Stock = Stock;
            ArticuloTemp.PUnit = PUnit;
            ArticuloTemp.ImagenByte = Imagen;
            ArticuloTemp.DVH = DVH;
            
            int i = Mapper.Alta(ArticuloTemp);

            DV = Seguridad.CalcularDVH("select * from Articulo where IdArticulo = (select TOP 1 IdArticulo from Articulo order by IdArticulo desc)", "Articulo");
            producto.EjecutarConsulta("Update Articulo set DVH= '" + DV + "' where IdArticulo = (select TOP 1 IdArticulo from Articulo order by IdArticulo desc)");
            Seguridad.ActualizarDVV("Articulo", Seguridad.SumaDVV("Articulo"));
            return i;
        }

        public int Modificar(int IdArticulo, int CodProd, string Nombre, string Descripcion, string Material, int Stock, decimal PUnit, int DVH)
        {
            ArticuloTemp.IdArticulo = IdArticulo;
            ArticuloTemp.CodProd = CodProd;
            ArticuloTemp.Nombre = Nombre;
            ArticuloTemp.Descripcion = Descripcion;
            ArticuloTemp.Material = Material;                       
            ArticuloTemp.Stock = Stock;
            ArticuloTemp.PUnit = PUnit;           
            ArticuloTemp.DVH = DVH;

            int i = Mapper.Modificar(ArticuloTemp);
            DV = Seguridad.CalcularDVH("select * from Articulo where IdArticulo= " + ArticuloTemp.IdArticulo + "", "Articulo");
            producto.EjecutarConsulta("Update Articulo set DVH= '" + DV + "' where IdArticulo=" + ArticuloTemp.IdArticulo + "");
            Seguridad.ActualizarDVV("Articulo", Seguridad.SumaDVV("Articulo"));
            return i;
        }

        public int Baja(int IdArticulo)
        {
            ArticuloTemp.IdArticulo = IdArticulo;
            int i = Mapper.Baja(ArticuloTemp);
            
            DV = Seguridad.CalcularDVH("select * from Articulo where IdArticulo= " + ArticuloTemp.IdArticulo + "", "Articulo");
            producto.EjecutarConsulta("Update Articulo set DVH= '" + DV + "' where IdArticulo=" + ArticuloTemp.IdArticulo + "");
            Seguridad.ActualizarDVV("Articulo", Seguridad.SumaDVV("Articulo"));
            
            return i;
        }

        public int GuardarImagenProd(int IdArticulo, byte[] Imagen)
        {
            ArticuloTemp.IdArticulo = IdArticulo;
            ArticuloTemp.ImagenByte = Imagen;
            int i = Mapper.GuardarImagenProd(ArticuloTemp);

            DV = Seguridad.CalcularDVH("select * from Articulo where IdArticulo= " + ArticuloTemp.IdArticulo + "", "Articulo");
            producto.EjecutarConsulta("Update Articulo set DVH= '" + DV + "' where IdArticulo=" + ArticuloTemp.IdArticulo + "");
            Seguridad.ActualizarDVV("Articulo", Seguridad.SumaDVV("Articulo"));

            return i;
        }
        #region verificacion integral 
        public string VerificarIntegridadProducto(int GlobalIdUsuario)
        {
            return Mapper.VerificarIntegridadProducto(GlobalIdUsuario);
        }

        public void RecalcularDVH()
        {
            Mapper.RecalcularDVH();
        }

        #endregion
    }
}
